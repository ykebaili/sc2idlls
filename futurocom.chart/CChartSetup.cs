using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;
using System.Collections;
using futurocom.chart.ChartArea;
using futurocom.chart.LegendArea;
using sc2i.formulaire;

namespace futurocom.chart
{

    public class CChartSetup : IElementAVariablesDynamiquesAGestionParInstance,
        I2iSerializable,
        IObjetAIContexteDonnee
    {
        /* TESTDBKEYOK (XL)*/

        private const string c_strCleVariableSourceGlobale = "0";
        private const string c_strNomVariableSourceElement = "Source element";

        private ESelectSerieAlignment m_selectSeriesAlignment = ESelectSerieAlignment.None;

        private bool m_bEnableZooming = true;
        private bool m_bEnable3D = true;

        private CDonneesDeChart m_donnees;
        private List<CParametreSerieDeChart> m_listeSeries = new List<CParametreSerieDeChart>();
        private List<CChartArea> m_listeAreas = new List<CChartArea>();
        private List<CLegendArea> m_listeLegendes = new List<CLegendArea>();

        //Gestion des variables
        private IElementAVariablesDynamiques m_elementAVariablesExternes = null;
        private List<CVariableDynamique> m_listeVariables = new List<CVariableDynamique>();
        private C2iWndFenetre m_formulaireFiltreAvance = new C2iWndFenetre();
        private C2iWndFenetre m_formulaireFiltreSimple = new C2iWndFenetre();

        
        //Non sérialisé, valeurs des champs
        private Dictionary<string, object> m_dicValeursChamps = new Dictionary<string, object>();


        private Type m_typeSourceGlobale = null;

        [NonSerialized]
        private IContexteDonnee m_contexteDonnee = null;

        //-----------------------------------------
        public CChartSetup()
        {
            m_donnees = new CDonneesDeChart(this);
            CChartArea area = new CChartArea();
            area.AreaName = "Area 1";
            m_listeAreas.Add(area);

            CLegendArea legende = new CLegendArea();
            legende.LegendName = "Legend 1";
            m_listeLegendes.Add(legende);
        }

        //-----------------------------------------
        public CDonneesDeChart ParametresDonnees
        {
            get
            {
                return m_donnees;
            }
            set
            {
                m_donnees = value;
            }
        }

        //-----------------------------------------
        public IEnumerable<CParametreSerieDeChart> Series
        {
            get
            {
                return m_listeSeries.AsReadOnly();
            }
            set
            {
                m_listeSeries = new List<CParametreSerieDeChart>();
                if (value != null)
                    m_listeSeries.AddRange(value);
            }
        }

        //-----------------------------------------
        public IEnumerable<CChartArea> Areas
        {
            get
            {
                return m_listeAreas.AsReadOnly();
            }
            set
            {
                m_listeAreas = new List<CChartArea>();
                if (value != null)
                    m_listeAreas.AddRange(value);
            }
        }

        //-----------------------------------------
        public IEnumerable<CLegendArea> Legends
        {
            get
            {
                return m_listeLegendes.AsReadOnly();
            }
            set
            {
                m_listeLegendes = new List<CLegendArea>();
                if (value != null)
                    m_listeLegendes.AddRange(value);
            }
        }

        //-----------------------------------------
        public  void ClearCache()
        {
            ParametresDonnees.ClearCache();
        }

        //-----------------------------------------
        private int GetNumVersion()
        {
            //return 1; // 1 : suppression de m_nIdNextVariable qui ne sert plus à rien
            //return 2; // 2 Ajout du Formulaire Filtre Simple toujours visible si pas null
            //return 3;//ajout de la possibilité de sélectionner les séries au runtime
            return 4;//ajout de enable zooming et enable 3D
        }

        //-----------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CDonneesDeChart>(ref m_donnees, this);
            if (!result)
                return result;
            if (result)
                result = serializer.TraiteListe<CParametreSerieDeChart>(m_listeSeries);
            if (result)
                result = serializer.TraiteListe<CChartArea>(m_listeAreas);
            if (result)
                result = serializer.TraiteListe<CLegendArea>(m_listeLegendes);
            if (!result)
                return result;
            bool bHasSource = m_typeSourceGlobale != null;
            serializer.TraiteBool(ref bHasSource);
            if ( bHasSource )
                serializer.TraiteType(ref m_typeSourceGlobale);

            if (nVersion < 1)
            {
                int nDummy = 10;
                serializer.TraiteInt(ref nDummy);
            }
            serializer.AttacheObjet(typeof(IElementAVariablesDynamiquesBase), this);
            result = serializer.TraiteListe<CVariableDynamique>(m_listeVariables, new object[]{this});
            serializer.DetacheObjet(typeof(IElementAVariablesDynamiquesBase), this);
            if (!result)
                return result;
            serializer.AttacheObjet(typeof(IElementAVariablesDynamiquesBase), this);
            result = serializer.TraiteObject<C2iWndFenetre>(ref m_formulaireFiltreAvance);
            if (!result)
                return result;

            if (nVersion >= 2)
                result = serializer.TraiteObject<C2iWndFenetre>(ref m_formulaireFiltreSimple);
            if ( !result )
                return result;


            if (nVersion >= 3)
            {
                serializer.TraiteEnum<ESelectSerieAlignment>(ref m_selectSeriesAlignment);
            }

            if ( nVersion >= 4)
            {
                serializer.TraiteBool(ref m_bEnable3D);
                serializer.TraiteBool(ref m_bEnableZooming);
            }

            
            return result;
        }


        //-------------------------------------------------------------
        public virtual CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.GetProprietesInstance();
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            foreach ( IVariableDynamique variable in ListeVariables )
            {
                CDefinitionProprieteDynamiqueVariableDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(
                    variable);
                lst.Add(def);
            }
            return lst.ToArray();
        }

        

        //---------------------------------------------------------------------------
        public IElementAVariablesDynamiques ElementAVariablesExternes
        {
            get
            {
                return m_elementAVariablesExternes;
            }
            set
            {
                m_elementAVariablesExternes = value;
            }
        }

        //---------------------------------------------------------------------------
        public Type TypeSourceGlobale
        {
            get
            {
                return m_typeSourceGlobale;
            }
            set
            {
                m_typeSourceGlobale = value;
                AssureVariableSourceGlobale();
            }
        }

        //-----------------------------------------
        public void SetRuntimeSource(object source)
        {
            if (source == null)
            {
                m_typeSourceGlobale = null;
            }
            else
            {
                m_typeSourceGlobale = source.GetType();
                SetValeurChamp(c_strCleVariableSourceGlobale, source);
            }
        }

        //---------------------------------------------------------------------------
        private void AssureVariableSourceGlobale()
        {
            CVariableDynamiqueSysteme varSource = null;
            foreach (IVariableDynamique variable in m_listeVariables)
            {
                if (variable.IdVariable == c_strCleVariableSourceGlobale)
                {
                    varSource = variable as CVariableDynamiqueSysteme;
                    break;
                }
            }
            if (m_typeSourceGlobale == null)
            {
                if (varSource != null)
                    m_listeVariables.Remove(varSource);
            }
            else
            {
                if (varSource == null)
                {
                    varSource = new CVariableDynamiqueSysteme(this);
                    varSource.IdVariable = c_strCleVariableSourceGlobale;
                    varSource.Nom = c_strNomVariableSourceElement;
                    m_listeVariables.Add(varSource);
                }
                varSource.SetTypeDonnee(new CTypeResultatExpression(m_typeSourceGlobale, false));
            }
        }






        #region IElementAVariablesDynamiquesBase Membres
        //--------------------------------------------------------------
        public string GetNewIdForVariable()
        {
            return CUniqueIdentifier.GetNew();
        }

        //--------------------------------------------------------------
        public void OnChangeVariable(IVariableDynamique variable)
        {
            
        }

        //--------------------------------------------------------------
        public IVariableDynamique[] ListeVariables
        {
            get
            {
                return m_listeVariables.ToArray();
            }
        }

        //--------------------------------------------------------------
        public void AddVariable(IVariableDynamique variable)
        {
            CVariableDynamique v = variable as CVariableDynamique;
            if ( v != null )
                m_listeVariables.Add(v);
        }

        //--------------------------------------------------------------
        public void RemoveVariable(IVariableDynamique variable)
        {
            m_listeVariables.Remove(variable as CVariableDynamique);
        }

        //--------------------------------------------------------------
        public bool IsVariableUtilisee(IVariableDynamique variable)
        {
            return false;
        }

        //--------------------------------------------------------------
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            return m_listeVariables.FirstOrDefault(v => v.IdVariable == strIdVariable) as CVariableDynamique;
        }

        //--------------------------------------------------------------
        public void SetValeurChampNom(string strNomChamp, object valeur)
        {
            string strUp = strNomChamp.ToUpper();
            foreach (IVariableDynamique variable in m_listeVariables)
                if (variable.Nom.ToUpper() == strUp)
                {
                    SetValeurChamp(variable.IdVariable, valeur);
                }
        }

        //--------------------------------------------------------------
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            if (variable != null)
                return SetValeurChamp(variable.IdVariable, valeur);
            return CResultAErreur.True;
        }

        //--------------------------------------------------------------
        public CResultAErreur SetValeurChamp(string strCleVariable, object valeur)
        {
            m_dicValeursChamps[strCleVariable] = valeur;
            return CResultAErreur.True;
        }

        //--------------------------------------------------------------
        public object GetValeurChamp(IVariableDynamique variable)
        {
            if (variable != null)
                return GetValeurChamp(variable.IdVariable);
            return null;
        }

        //--------------------------------------------------------------
        public object GetValeurChampNom(string strVariableName)
        {
            string strUp = strVariableName.ToUpper();
            foreach (IVariableDynamique variable in m_listeVariables)
                if (variable.Nom.ToUpper() == strUp)
                    return GetValeurChamp(variable.IdVariable);
            return null;
        }

        //--------------------------------------------------------------
        public object GetValeurChamp(string strCleVariable)
        {
            object val = null;
            m_dicValeursChamps.TryGetValue(strCleVariable, out val);
            if (val == null)
            {
                if (!m_dicValeursChamps.ContainsKey(strCleVariable))
                {
                    foreach (IVariableDynamique v in ListeVariables)
                    {
                        if (v.IdVariable == strCleVariable)
                        {
                            IVariableDynamiqueAValeurParDefaut vAvecDef = v as IVariableDynamiqueAValeurParDefaut;
                            if (vAvecDef != null)
                            {
                                val = vAvecDef.GetValeurParDefaut();
                            }
                            else
                            {
                                IVariableDynamiqueCalculee varCalc = v as IVariableDynamiqueCalculee;
                                if (varCalc != null)
                                {
                                    if (varCalc.FormuleDeCalcul != null)
                                    {
                                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(this);
                                        CResultAErreur res = varCalc.FormuleDeCalcul.Eval(ctx);
                                        if (res)
                                            return res.Data;
                                    }
                                    return null;
                                }
                            }
                            break;

                        }
                    }
                }
                m_dicValeursChamps[strCleVariable] = val;
            }
            return val;
        }
        #endregion

        //-----------------------------------------
        public C2iWndFenetre FormulaireFiltreAvance
        {
            get
            {
                if ( m_formulaireFiltreAvance == null )
                    m_formulaireFiltreAvance = new C2iWndFenetre();
                return m_formulaireFiltreAvance;
            }
            set
            {
                m_formulaireFiltreAvance = value;
            }
        }

        //-----------------------------------------
        public C2iWndFenetre FormulaireFiltreSimple
        {
            get
            {
                if (m_formulaireFiltreSimple == null)
                    m_formulaireFiltreSimple = new C2iWndFenetre();
                return m_formulaireFiltreSimple;
            }
            set
            {
                m_formulaireFiltreSimple = value;
            }
        }

        //-----------------------------------------
        public ESelectSerieAlignment SelectSeriesAlignment
        {
            get
            {
                return m_selectSeriesAlignment;
            }
            set
            {
                m_selectSeriesAlignment = value;
            }
        }

        //-----------------------------------------
        public bool AutoriserZoom
        {
            get
            {
                return m_bEnableZooming;
            }
            set
            {
                m_bEnableZooming = value;
            }
        }

        //-----------------------------------------
        public bool Autoriser3D
        {
            get
            {
                return m_bEnable3D;
            }
            set
            {
                m_bEnable3D = value;
            }
        }
    
        //----------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return m_contexteDonnee;
            }
            set
            {
                m_contexteDonnee = value;
                ParametresDonnees.IContexteDonnee = value;
            }
        }

    }

    //----------------------------------------------------------------------------
    public class CChartSetupConvertor : CGenericObjectConverter<CChartSetup>
    {
        public override string GetString(CChartSetup value)
        {
            return "Chart setup";
        }
    }

}
