using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.data;
using System.Data;
using sc2i.formulaire;

namespace sc2i.data.dynamic
{
    [Serializable]
    public class CParametreVisuChampTableauCroise : I2iSerializable
    {
        #region Objet pour calculer les formules de header
        public class CDummyForHeaderFormula : CElementAVariablesDynamiques
        {
            private const string c_nomVariable = "Value";
            private CVariableDynamiqueSysteme m_variable = null;

            public CDummyForHeaderFormula(Type tp)
            {
                CreateVariable(tp);
            }

            public CVariableDynamique Variable
            {
                get
                {
                    return m_variable;
                }
            }

            public CDummyForHeaderFormula(object valeur)
            {
                if (valeur != null)
                {
                    CreateVariable(valeur.GetType());
                    SetValeurChamp(m_variable, valeur);
                }
            }
            

            private void CreateVariable(Type typeDonnee)
            {
                m_variable = new CVariableDynamiqueSysteme(this);
                m_variable.IdVariable = "0";
                m_variable.Nom = c_nomVariable;
                m_variable.SetTypeDonnee(new CTypeResultatExpression(typeDonnee, false));
                AddVariable(m_variable);
            }

            
        }
        #endregion

        #region Objet pour les formules de données
        public class CDummyForDataFormula : CElementAVariablesDynamiques
        {
            private const string c_nomVariable = "Value";
            private CVariableDynamiqueSysteme m_variable = null;
            public CDummyForDataFormula(Type typeDonnee)
            {
                CreateVariable(typeDonnee);
            }

            public CDummyForDataFormula(object valeur)
            {
                Type tp = typeof(double);
                if (valeur != null)
                    tp = valeur.GetType();
                CreateVariable(tp);
                SetValeurChamp(m_variable, valeur);
            }

            public CVariableDynamique Variable
            {
                get
                {
                    return m_variable;
                }
            }

            private void CreateVariable(Type typeDonnee )
            {
                m_variable = new CVariableDynamiqueSysteme(this);
                m_variable.IdVariable = "0";
                m_variable.Nom = c_nomVariable;
                m_variable.SetTypeDonnee( new CTypeResultatExpression ( typeDonnee, false ) );
                AddVariable(m_variable);
            }

            
        }
        #endregion

        #region variable de filtre déportée
        [Serializable]
        public class CVariableDeFiltreDeportee : CVariableDynamiqueSysteme
        {
            public string ChampAssocieAuFiltre{get;set;}//Nom de champ auquel est lié le filtre

            public CVariableDeFiltreDeportee()
            {
            }

            public CVariableDeFiltreDeportee(IElementAVariablesDynamiques elt )
                :base ( elt )
            {
            }


            private int GetNumVersion()
            {
                return 0;
            }

            public override CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;
                result = base.Serialize(serializer);
                if (!result)
                    return result;

                string strChamp = ChampAssocieAuFiltre;
                serializer.TraiteString(ref strChamp);
                ChampAssocieAuFiltre = strChamp;

                return result;
            }

            public override string Rubrique
            {
                get
                {
                    return I.T("Filter @1|20040", ChampAssocieAuFiltre);
                }
                set
                {
                }
            }



        }
        #endregion

        private const string c_nomVariableValue = "CellValue";

        CChampFinalDeTableauCroise m_champFinal = null;
        private CFormatChampTableauCroise m_formatParDefaut = new CFormatChampTableauCroise();
        private CFormatChampTableauCroise m_formatHeader = new CFormatChampTableauCroise();
 
        private C2iExpression m_formuleData = null;
        private C2iExpression m_formuleHeader = null;

        private CActionSur2iLink m_actionSurClick = null;

        private int? m_nSortOrder = null;
        private bool m_bTriDecroissant=  false;

        //Elements nécéssaires pour récuperer les données
        //Ces données sont initialisées par PrepareAffichageDonnees
        //Fonction qui met en cache ce qui peut être mis en cache
        //Clé de l'objet->Objet utilisé dans les formules header et data
        private Dictionary<int, CObjetDonnee> m_cacheObjetsValeurs = null;
        private CDummyForDataFormula m_dummyForData = null;
        private CDummyForHeaderFormula m_dummyForHeader = null;
        
        //Si non null, le header contient un objet donnée (colonne pivot)
        private Type m_typeObjetDonneeHeader = null;

        //si non null, les données contiennet un objet donnée (colonne clé)
        private Type m_typeObjetDonneeData = null;

        public CParametreVisuChampTableauCroise()
        {
        }


        public CParametreVisuChampTableauCroise( CChampFinalDeTableauCroise champFinal )
        {
            m_champFinal = champFinal;
        }

        public CChampFinalDeTableauCroise ChampFinal
        {
            get
            {
                return m_champFinal;
            }
            set
            {
                m_champFinal = value;
            }
        }

        public int? SortOrder
        {
            get
            {
                return m_nSortOrder;
            }
            set
            {
                m_nSortOrder = value;
            }
        }

        public bool TriDecroissant
        {
            get{
                return m_bTriDecroissant;
            }
            set
            {
                m_bTriDecroissant = value;
            }
        }

        public CFormatChampTableauCroise FormatParDefaut
        {
            get
            {
                if (m_formatParDefaut == null)
                    m_formatParDefaut = new CFormatChampTableauCroise();
                return m_formatParDefaut;
            }
            set
            {
                m_formatParDefaut = value;
            }
        }

        public CFormatChampTableauCroise FormatHeader
        {
            get
            {
                if (m_formatHeader == null)
                    m_formatHeader = new CFormatChampTableauCroise();
                return m_formatHeader;
            }
            set
            {
                m_formatHeader = value;
            }
        }

        public C2iExpression FormuleData
        {
            get
            {
                return m_formuleData;
            }
            set
            {
                m_formuleData = value;
            }
        }

        public C2iExpression FormuleHeader
        {
            get
            {
                return m_formuleHeader;
            }
            set
            {
                m_formuleHeader = value;
            }
        }

        private int GetNumVersion()
        {
            return 2;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = serializer.TraiteObject <CFormatChampTableauCroise>( ref m_formatParDefaut );
            if (result)
                result = serializer.TraiteObject<CFormatChampTableauCroise>(ref m_formatHeader);
            if ( result )
                result = serializer.TraiteObject<CChampFinalDeTableauCroise>(ref m_champFinal );
            if ( result )
                result = serializer.TraiteObject<C2iExpression> ( ref m_formuleData );
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleHeader);
            if (result && nVersion >= 1)
                result = serializer.TraiteObject<CActionSur2iLink>(ref m_actionSurClick);
            if (nVersion >= 2)
            {
                bool bHasVal = m_nSortOrder != null;
                serializer.TraiteBool(ref bHasVal);
                if (bHasVal)
                {
                    int nVal = 0;
                    if (serializer.Mode == ModeSerialisation.Ecriture)
                        nVal = m_nSortOrder.Value;
                    serializer.TraiteInt(ref nVal);
                    m_nSortOrder = nVal;
                }
                else
                    m_nSortOrder = null;
                serializer.TraiteBool ( ref m_bTriDecroissant);
            }
            return result;
        }

        //-------------------------------------------------------
        public CDummyForHeaderFormula GetObjetPourFormuleHeader(CParametreDonneeCumulee parametreDonnee)
        {
            string strNomChamp = m_champFinal.NomChamp;
            //regarde si c'est un champ clé
            
            CChampFinalDeTableauCroiseDonnee champDonnee = m_champFinal as CChampFinalDeTableauCroiseDonnee;
            if (champDonnee == null || champDonnee.Pivot == null)
                return null;
            if (parametreDonnee != null)
            {
                CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champDonnee.Pivot.NomChamp);
                if (cle != null && cle.TypeLie != null)
                {
                    if (cle.TypeLie != null)
                        return new CDummyForHeaderFormula(cle.TypeLie);
                }
            }
            return new CDummyForHeaderFormula("");
        }

        //-------------------------------------------------------
        public CActionSur2iLink ActionSurClick
        {
            get
            {
                return m_actionSurClick;
            }
            set
            {
                m_actionSurClick = value;
            }
        }

        /*//-------------------------------------------------------
        private object GetObjetPourFormuleHeader(
            object valeur)
        {
            string strNomChamp = m_champFinal.NomChamp;
            if (m_para != null)
            {
                CChampFinalDeTableauCroiseDonnee champFinal = ChampFinal as CChampFinalDeTableauCroiseDonnee;
                if (champFinal != null && champFinal.Pivot != null)
                {
                    CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champFinal.Pivot.NomChamp);
                    if (cle != null && cle.TypeLie != null)
                    {
                        CObjetDonneeAIdNumerique obj = Activator.CreateInstance(cle.TypeLie, new object[] { ctx }) as CObjetDonneeAIdNumerique;
                        try
                        {
                            int nVal = Int32.Parse(valeur.ToString());
                            if (obj.ReadIfExists(nVal))
                                return new CDummyForHeaderFormula(obj);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            CChampFinalDeTableauCroiseDonnee champDonnee = m_champFinal as CChampFinalDeTableauCroiseDonnee;
            if (champDonnee == null || champDonnee.Pivot == null)
                return null;
            return new CDummyForHeaderFormula(valeur);
        }*/

        //-------------------------------------------------------
        public CDummyForDataFormula GetObjetPourFormuleData(CParametreDonneeCumulee parametreDonnee)
        {
            CChampFinalDeTableauCroiseCle champFinalCle = ChampFinal as CChampFinalDeTableauCroiseCle;
            if (champFinalCle != null)
            {
                CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champFinalCle.NomChamp);
                if (cle != null && cle.TypeLie != null)
                    return new CDummyForDataFormula(cle.TypeLie);
            }                    
            return new CDummyForDataFormula(m_champFinal.TypeDonnee);
        }
        /*
        //-------------------------------------------------------
        private object GetObjetPourFormuleData(object valeur)
        {
            CChampFinalDeTableauCroiseCle champFinalCle = ChampFinal as CChampFinalDeTableauCroiseCle;
            if (champFinalCle != null)
            {
                CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champFinalCle.NomChamp);
                if (cle != null && cle.TypeLie != null)
                {
                    CObjetDonnee objet = Activator.CreateInstance(cle.TypeLie, new object[] { contexte }) as CObjetDonnee;
                    if (objet.ReadIfExists(new object[] { valeur }))    
                        valeur = objet;
                    else
                        valeur = null;
                }
            }
            return new CDummyForDataFormula(valeur);
        }*/

        public CResultAErreur PrepareAffichageDonnees(
            System.Data.DataTable table, 
            CContexteDonnee contexte, 
            CParametreDonneeCumulee parametreDonnee,
            CParametreVisuDonneePrecalculee parametreVisu)
        {
            CResultAErreur result = CResultAErreur.True;
            m_dummyForData = null;
            m_dummyForHeader = null;

            m_dummyForHeader = GetObjetPourFormuleHeader(parametreDonnee) as CDummyForHeaderFormula;
            CCleDonneeCumulee cle = null;
            #region Précharge les données nécéssaires pour les entetes de colonnes
            if (FormuleHeader != null)
            {
                //regarde si c'est un champ clé
                CChampFinalDeTableauCroiseDonnee champDonnee = m_champFinal as CChampFinalDeTableauCroiseDonnee;
                if (champDonnee != null && champDonnee.Pivot != null && parametreDonnee != null)
                {
                    cle = parametreDonnee.GetChampCle(champDonnee.Pivot.NomChamp);
                    if (cle != null && cle.TypeLie != null)
                    {
                        if (cle.TypeLie != null)
                        {
                            m_typeObjetDonneeHeader = cle.TypeLie;
                            List<int> lstCles = new List<int>();
                            //Précharge les éléments correspondants. On trouve les valeurs
                            //des éléments dans les données étendues de chaque colonne
                            StringBuilder bl = new StringBuilder();
                            foreach (DataColumn col in table.Columns)
                            {

                                CChampFinalDetableauCroiseDonneeAvecValeur champFinalDeCol = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDetableauCroiseDonneeAvecValeur;
                                if (champFinalDeCol != null && champFinalDeCol.NomChamp == ChampFinal.NomChamp)
                                {
                                    bl.Append(champFinalDeCol.ValeurPivot);
                                    bl.Append(',');
                                }
                            }
                            if (bl.Length > 0)
                            {
                                bl.Remove(bl.Length - 1, 1);
                                CListeObjetsDonnees lst = new CListeObjetsDonnees(contexte, cle.TypeLie);
                                //trouve la clé
                                CObjetDonneeAIdNumerique obj = Activator.CreateInstance(cle.TypeLie, new object[] { contexte }) as CObjetDonneeAIdNumerique;
                                lst.Filtre = new CFiltreData(obj.GetChampId() + " in (" +
                                    bl.ToString() + ")");
                                foreach (CObjetDonneeAIdNumerique objTmp in lst)
                                    parametreVisu.PutElementInCache(objTmp);
                            }
                        }
                    }
                }
            }
            #endregion

            m_dummyForData = GetObjetPourFormuleData ( parametreDonnee );
            //Si le champ final correspond à une clé, précharge toutes les valeurs
            //de clé si la clé correspond à un type
            cle = parametreDonnee.GetChampCle ( ChampFinal.NomChamp );
            if ( cle != null && FormuleData != null && cle.TypeLie != null)
            {
                m_typeObjetDonneeData = cle.TypeLie;
                StringBuilder bl = new StringBuilder();
                if (table.Columns.Contains(ChampFinal.NomChamp))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            int nId = Int32.Parse(row[ChampFinal.NomChamp].ToString());
                            bl.Append(nId);
                            bl.Append(',');
                        }
                        catch { }
                    }
                    if (bl.Length > 0)
                    {
                        bl.Remove(bl.Length - 1, 1);
                        CObjetDonneeAIdNumerique obj = Activator.CreateInstance(cle.TypeLie, new object[] { contexte }) as CObjetDonneeAIdNumerique;
                        CListeObjetsDonnees lst = new CListeObjetsDonnees(contexte, cle.TypeLie);
                        lst.Filtre = new CFiltreData(obj.GetChampId() + " in (" + bl.ToString() + ")");
                        foreach (CObjetDonneeAIdNumerique objTmp in lst)
                            parametreVisu.PutElementInCache(objTmp);
                    }
                }
            }
            return result;
        }


        public string GetValeurHeader(DataColumn col, CParametreVisuDonneePrecalculee parametre)
        {
            CChampFinalDetableauCroiseDonneeAvecValeur champFinal = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDetableauCroiseDonneeAvecValeur;
            object valeurPivot = null;
            if ( champFinal != null )
                valeurPivot = champFinal.ValeurPivot;
            if (FormuleHeader != null )
            {
                try
                {
                    if ( m_typeObjetDonneeHeader != null )
                    {
                        Int32 nVal = Int32.Parse(champFinal.ValeurPivot.ToString());
                        CObjetDonnee obj = parametre.GetFromCache(m_typeObjetDonneeHeader, nVal);
                        m_dummyForHeader.SetValeurChamp(m_dummyForHeader.Variable, obj);
                    }
                    else if (m_dummyForHeader != null)
                        m_dummyForHeader.SetValeurChamp ( m_dummyForHeader.Variable, valeurPivot);
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_dummyForHeader);
                    CResultAErreur result = FormuleHeader.Eval(ctx);
                    if (result)
                        return result.Data.ToString();
                }
                catch { }
            }
            return col.ColumnName;
        }

        public string GetValeurData(object valeur, CParametreVisuDonneePrecalculee parametre)
        {
            if (m_dummyForData != null && FormuleData != null)
            {
                if (m_typeObjetDonneeData != null)
                {
                    try
                    {
                        Int32 nVal = Int32.Parse(valeur.ToString());
                        CObjetDonnee obj = parametre.GetFromCache(m_typeObjetDonneeData, nVal);
                        m_dummyForData.SetValeurChamp(m_dummyForData.Variable, obj);
                    }
                    catch {
                        return valeur.ToString();
                    }
                }
                else
                    m_dummyForData.SetValeurChamp ( m_dummyForData.Variable, valeur );
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_dummyForData);
                CResultAErreur result = FormuleData.Eval ( ctx );
                if ( result )
                    return result.Data.ToString();
            }
            if (valeur != null)
                return valeur.ToString();
            return "";
        }

        public CFormatChampTableauCroise GetFormatData(object valeur, CParametreVisuDonneePrecalculee parametre)
        {
            if (m_dummyForData != null)
            {
                if (m_typeObjetDonneeData != null)
                {
                    try
                    {
                        Int32 nVal = Int32.Parse(valeur.ToString());
                        CObjetDonnee obj = parametre.GetFromCache ( m_typeObjetDonneeData, nVal );
                        m_cacheObjetsValeurs.TryGetValue(nVal, out obj);
                        m_dummyForData.SetValeurChamp(m_dummyForData.Variable, obj);
                    }
                    catch { }
                }
                else
                    m_dummyForData.SetValeurChamp(m_dummyForData.Variable, valeur);
                return FormatParDefaut.GetFormatDynamique(m_dummyForData);
            }
            return null;
        }

        public CElementAVariablesDynamiques GetObjetPourFormuleCellule(
            CParametreVisuDonneePrecalculee paramVisu)
        {
            return GetObjetPourFormuleCellule(paramVisu, null, null);
        }

        /// <summary>
        /// récupère un objet pour formule liée à la cellule
        /// 
        /// </summary>
        /// <param name="paramVisu"></param>
        /// <param name="row">Ligne contenant les données (peut être nul en design)</param>
        /// <param name="nCol">Numéro de colonne où se trouve la donnée (peut être null en design)</param>
        /// <returns></returns>
        public CElementAVariablesDynamiques GetObjetPourFormuleCellule
            ( 
            CParametreVisuDonneePrecalculee paramVisu,
            DataRow row,
            int? nCol)
        {
            CElementAVariablesDynamiques eltAVariables = new CElementAVariablesDynamiques();
            CTypeDonneeCumulee typeDonnee = paramVisu.GetTypeDonneeCumulee(CContexteDonneeSysteme.GetInstance());
            if (typeDonnee == null)
                return null;
            CParametreDonneeCumulee parametreDonnee = typeDonnee.Parametre;
            //Ajoute les clés
            CChampFinalDeTableauCroise[] champsFinaux = paramVisu.TableauCroise.ChampsFinaux;
            int nIdVariable = 0;
            foreach (CChampFinalDeTableauCroise champFinal in champsFinaux)
            {
                CChampFinalDeTableauCroiseCle champCle = champFinal as CChampFinalDeTableauCroiseCle;
                if (champCle != null)
                {
                    CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champCle.NomChamp);
                    if (cle != null)
                    {
                        CVariableDynamiqueSysteme variable = new CVariableDynamiqueSysteme(eltAVariables);
                        variable.Nom = champCle.NomChamp;
                        variable.IdVariable = nIdVariable.ToString();
                        nIdVariable++;
                        if (cle.TypeLie != null)
                            variable.SetTypeDonnee(new CTypeResultatExpression(cle.TypeLie, false));
                        else
                            variable.SetTypeDonnee(new CTypeResultatExpression(typeof(int), false));
                        eltAVariables.AddVariable(variable);
                        if (row != null && row.Table.Columns.Contains(champCle.NomChamp))
                        {
                            object val = row[champCle.NomChamp]; ;
                            if (cle.TypeLie != null)
                            {
                                try
                                {
                                    if (val != DBNull.Value)
                                        val = paramVisu.GetFromCache(cle.TypeLie, Int32.Parse((val.ToString())));
                                }
                                catch { }
                            }
                            else
                                if (val != DBNull.Value)
                                    val = val.ToString();
                            //TESTDBKEYOK (SC)
                            eltAVariables.SetValeurChamp(variable.IdVariable, val);
                        }
                    }
                }
            }
            //Ajoute la valeur de colonne pivot (s'il y a lieu )
            CChampFinalDeTableauCroiseDonnee champAvecPivot = ChampFinal as CChampFinalDeTableauCroiseDonnee;
            if (champAvecPivot != null && champAvecPivot.Pivot != null)
            {
                CCleDonneeCumulee cle = parametreDonnee.GetChampCle(champAvecPivot.Pivot.NomChamp);
                if (cle != null)
                {
                    CVariableDynamiqueSysteme variable = new CVariableDynamiqueSysteme(eltAVariables);
                    variable.Nom = champAvecPivot.Pivot.NomChamp;
                    variable.IdVariable = nIdVariable.ToString();
                    nIdVariable++;
                    if (cle.TypeLie != null)
                        variable.SetTypeDonnee(new CTypeResultatExpression(cle.TypeLie, false));
                    else
                        variable.SetTypeDonnee(new CTypeResultatExpression(typeof(string), false));
                    eltAVariables.AddVariable(variable);
                    if (row != null && nCol != null)
                    {
                        DataColumn col = row.Table.Columns[nCol.Value];
                        CChampFinalDetableauCroiseDonneeAvecValeur cv = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDetableauCroiseDonneeAvecValeur;
                        if (cv != null)
                        {
                            object val = cv.ValeurPivot;
                            if (cle.TypeLie != null)
                            {
                                try
                                {
                                    if (val != null)
                                        val = paramVisu.GetFromCache ( cle.TypeLie, Int32.Parse(val.ToString()));
                                }
                                catch
                                {
                                }
                            }
                            else
                                if (val != null)
                                    val = val.ToString();
                            eltAVariables.SetValeurChamp(variable.IdVariable, val);
                        }
                    }
                }
            }

            //Ajoute la valeur de la colonne
            CVariableDynamiqueSysteme variableValue = new CVariableDynamiqueSysteme(eltAVariables);
            variableValue.Nom = c_nomVariableValue;
            variableValue.IdVariable = nIdVariable.ToString();
            nIdVariable++;
            variableValue.SetTypeDonnee ( new CTypeResultatExpression (typeof(double), false) );
            eltAVariables.AddVariable(variableValue);
            try
            {
                eltAVariables.SetValeurChamp(variableValue.IdVariable, Convert.ToDouble(row[nCol.Value]));
            }
            catch
            {
            }

            //Ajoute les variables de filtre
            foreach (CFiltreDonneePrecalculee filtreDonnee in paramVisu.FiltresUtilisateur)
            {
                foreach (IVariableDynamique variable in filtreDonnee.Filtre.ListeVariables)
                {
                    CVariableDeFiltreDeportee v = new CVariableDeFiltreDeportee(eltAVariables);
                    v.Nom = variable.Nom;
                    v.SetTypeDonnee(variable.TypeDonnee);
                    v.ChampAssocieAuFiltre = filtreDonnee.ChampAssocie;
                    eltAVariables.AddVariable(v);
                    eltAVariables.SetValeurChamp(v.IdVariable, filtreDonnee.Filtre.GetValeurChamp(variable.IdVariable));
                }
            }
            return eltAVariables;
        }

    }
}
