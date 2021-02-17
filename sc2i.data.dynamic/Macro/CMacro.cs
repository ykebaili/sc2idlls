using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using System.Drawing;

namespace sc2i.data.dynamic.Macro
{
    public class CMacro : I2iSerializable, IElementAVariablesDynamiques
    {
        private List<IVariableDynamique> m_listeVariables = new List<IVariableDynamique>();
        private C2iWndFenetre m_formulaire = null;
        private Dictionary<IVariableDynamique, object> m_dicValeursVariables = new Dictionary<IVariableDynamique, object>();
        private int m_nIdNextVariable = 1;

        private int m_nIdVariableTargetElement = -1;

        private List<CMacroObjet> m_listeObjets = new List<CMacroObjet>();


        private CContexteDonnee m_contexteDonnee = null;

        //-------------------------------------------------------
        public CMacro()
        {
        }
        
        //-------------------------------------------------------
        public C2iWndFenetre Formulaire
        {
            get
            {
                if (m_formulaire == null)
                {
                    m_formulaire = new C2iWndFenetre();
                    m_formulaire.Size = new Size(300, 200);
                }
                return m_formulaire;
            }
            set
            {
                m_formulaire = value;
            }
        }


        //-------------------------------------------------------
        public IEnumerable<IVariableDynamique> Variables
        {
            get
            {
                return m_listeVariables.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        public IVariableDynamique VariableCible
        {
            get
            {
                return m_listeVariables.FirstOrDefault(v => v.Id == m_nIdVariableTargetElement);
            }
            set
            {
                if (value != null)
                    m_nIdVariableTargetElement = value.Id;
            }
        }

        //-------------------------------------------------------
        public int IdVariableTargetElement
        {
            get
            {
                return m_nIdVariableTargetElement;
            }
        }

        //-------------------------------------------------------
        public void AddVariable(IVariableDynamique variable)
        {
            m_listeVariables.Add(variable);
        }

        //-------------------------------------------------------
        public void RemoveVariable(IVariableDynamique variable)
        {
            m_listeVariables.Remove(variable);
        }

        //-------------------------------------------------------
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            m_dicValeursVariables[variable] = valeur;
            return CResultAErreur.True;
        }

        //-------------------------------------------------------
        public CResultAErreur SetValeurChamp(int nIdVariable, object valeur)
        {
            IVariableDynamique v = m_listeVariables.FirstOrDefault(var => var.Id == nIdVariable);
            if (v != null)
                return SetValeurChamp(v, valeur);
            return CResultAErreur.True;
        }

        //-------------------------------------------------------
        public object GetValeurChamp(IVariableDynamique variable)
        {
            object valeur = null;
            m_dicValeursVariables.TryGetValue(variable, out valeur);
            return valeur;
        }

        //-------------------------------------------------------
        public object GetValeurChamp(int nIdVariable)
        {
            IVariableDynamique v = m_listeVariables.FirstOrDefault(var => var.Id == nIdVariable);
            if (v != null)
                return GetValeurChamp(v);
            return null;
        }

        //-------------------------------------------------------
        public IEnumerable<CMacroObjet> Objets
        {
            get
            {
                return m_listeObjets.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        public void AddObjet(CMacroObjet macroObjet)
        {
            m_listeObjets.Add(macroObjet);
        }

        //-------------------------------------------------------
        public void RemoveObjet(CMacroObjet macroObjet)
        {
            m_listeObjets.Remove(macroObjet);
        }



        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nIdNextVariable);
            serializer.TraiteInt(ref m_nIdVariableTargetElement);
            serializer.AttacheObjet(typeof(IElementAVariablesDynamiques), this);
            List<I2iSerializable> lst = new List<I2iSerializable>(from v in m_listeVariables where v is I2iSerializable select (I2iSerializable)v);
            result = serializer.TraiteListe<I2iSerializable>(lst, new object[] { this });
            if (!result)
                return result;
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_listeVariables = new List<IVariableDynamique>(from v in lst where v is IVariableDynamique select (IVariableDynamique)v);
            result = serializer.TraiteObject<C2iWndFenetre>(ref m_formulaire);
            if (result)
                result = serializer.TraiteListe<CMacroObjet>(m_listeObjets, new object[]{this});
            if ( m_contexteDonnee != null && serializer.Mode == ModeSerialisation.Lecture)
                m_contexteDonnee = serializer.GetObjetAttache(typeof(CContexteDonnee)) as CContexteDonnee;
            serializer.DetacheObjet(typeof(IElementAVariablesDynamiques), this);
            return result;
        }


        //-------------------------------------------------------
        public static CResultAErreurType<CMacro> FromVersion(CVersionDonnees version)
        {
            CResultAErreurType<CMacro> result = new CResultAErreurType<CMacro>();
            using ( CContexteDonnee contexte = new CContexteDonnee ( version.ContexteDonnee.IdSession, true, false ) )
            {
                contexte.SetVersionDeTravail ( version.Id, false );
                CMacro macro = new CMacro();
                macro.m_contexteDonnee = version.ContexteDonnee;

                Dictionary<int, CMacroObjet> dicMacrosObjets = new Dictionary<int, CMacroObjet>();
                Dictionary<CObjetDonneeAIdNumerique, CMacroObjet> dicObjetToMacros = new Dictionary<CObjetDonneeAIdNumerique, CMacroObjet>();
                foreach (CVersionDonneesObjet vo in version.VersionsObjets)
                {
                    CMacroObjet mo = new CMacroObjet(macro);
                    mo.TypeObjet = vo.TypeElement;
                    mo.IdObjet = vo.IdElement;
                    mo.TypeOperation = vo.TypeOperation;
                    macro.AddObjet ( mo );
                    
                    dicMacrosObjets[vo.Id] = mo;
                    CObjetDonneeAIdNumerique objet = Activator.CreateInstance ( vo.TypeElement, new object[]{contexte}) as CObjetDonneeAIdNumerique;
                    if (objet.ReadIfExists(vo.IdElement))
                        dicObjetToMacros[objet] = mo;
                    mo.CreateVariable(objet);
                   
                }
                foreach ( CVersionDonneesObjet vo in version.VersionsObjets )
                {
                    CMacroObjet mo = dicMacrosObjets[vo.Id];
                    CResultAErreur resMo = CResultAErreur.True;
                    resMo = mo.FillFromVersion(vo, contexte, dicObjetToMacros);
                    if (!resMo)
                        result.EmpileErreur(resMo.Erreur);
                }
                result.DataType = macro;
                return result;
            }
            
        }
       
        //--------------------------------
        public int IdSession
        {
            get { return m_contexteDonnee != null?m_contexteDonnee.IdSession:0; }
        }

        //--------------------------------
        public CContexteDonnee ContexteDonnee
        {
            get { return m_contexteDonnee; }
            set
            {
                m_contexteDonnee = value;
            }
        }

        //--------------------------------
        public int GetNewIdForVariable()
        {
            return m_nIdNextVariable++;
        }

        //--------------------------------
        public void OnChangeVariable(IVariableDynamique variable)
        {
            foreach (CMacroObjet mo in Objets)
            {
                mo.OnChangeVariable(variable);
            }
        }

        //--------------------------------
        public IVariableDynamique[] ListeVariables
        {
            get { return Variables.ToArray(); }
        }

        //--------------------------------
        public bool IsVariableUtilisee(IVariableDynamique variable)
        {
            foreach ( CMacroObjet mac in Objets )
            {
                if ( mac.IsVariableUtilisee ( variable ) )
                    return true;
            }
            return false;
        }

        //--------------------------------
        public CVariableDynamique GetVariable(int nIdVariable)
        {
            return m_listeVariables.FirstOrDefault(v => v.Id == nIdVariable) as CVariableDynamique;
        }

        //--------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            foreach ( IVariableDynamique variable in Variables )
            {
                if ( variable is CVariableDynamique )
                    lst.Add ( new CDefinitionProprieteDynamiqueVariableDynamique((CVariableDynamique)variable) );
            }
            return lst.ToArray();

        }



        //------------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
        {
            return GetDefinitionsChamps(new CObjetPourSousProprietes(typeInterroge), null);
        }

        //------------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
        {
            return GetDefinitionsChamps(new CObjetPourSousProprietes(typeInterroge), null);
        }

        //------------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
        {
            return GetDefinitionsChamps(objet, null);
        }

        //------------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if ( objet.TypeAnalyse == typeof(CMacro) )
            {
                foreach ( CVariableDynamique variable in Variables )
                    lst.Add ( new CDefinitionProprieteDynamiqueVariableDynamique ( variable ));
            }
            else
                lst.AddRange ( new CFournisseurPropDynStd().GetDefinitionsChamps ( objet, defParente ));
            return lst.ToArray();
        }

    }
}