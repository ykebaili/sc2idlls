using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.common;
using System.Drawing;
using sc2i.expression;
using System.Data;
using sc2i.expression.datatable;
using System.Collections;

namespace futurocom.easyquery
{
    [Serializable]
    [DynamicClass("Runnable easy query")]
    public class CEasyQuery : C2iObjetGraphique,
        IElementADataTableDynamique,
        IRunnableEasyQuery,
        IElementAVariablesDynamiquesAGestionParInstance,
        IObjetAIContexteDonnee,
        I2iCloneableAvecTraitementApresClonage
    {
        private string m_strLibelle = "";
        private CListeQuerySource m_listeSources = new CListeQuerySource();
        private List<I2iObjetGraphique> m_listeObjets = new List<I2iObjetGraphique>();

        private List<CVariableDynamique> m_listeVariables = new List<CVariableDynamique>();
        private Dictionary<string, object> m_dicValeursVariables = new Dictionary<string, object>();

        private Dictionary<Type, object> m_dicObjetsAttaches = new Dictionary<Type, object>();

        private IElementAVariablesDynamiquesBase m_elementAVariablesExternes = null;

        /// <summary>
        /// Avant Timos 4.0.1.4 les tableaux croisés avaient comme nom "TABLE1", "TABLE2", ...
        /// Ca a été corrigé dans la version 4.0.1.4 mais crystal attend toujours les fichiers
        /// </summary>
        private bool m_bModeCompatibilitéTimos4_0_1_3 = false;

        [NonSerialized]
        private IContexteDonnee m_contexteDonnee = null;

        [NonSerialized]
        private bool m_bUseRuntimeCache = true;

        //---------------------------------------------------
        public CEasyQuery()
        {
            Size = new Size(3000, 3000);
        }

        //---------------------------------------------------
        public CEasyQuery(CEasyQuerySource source)
        {
            Size = new Size(3000, 3000);
            m_listeSources.AddSource(source);
        }

        //---------------------------------------------------
        public IEnumerable<CEasyQuerySource> Sources
        {
            get
            {
                return m_listeSources.Sources;
            }
            set
            {
                m_listeSources = new CListeQuerySource();
                if (value != null)
                    foreach (CEasyQuerySource source in value)
                        m_listeSources.AddSource(source);
            }
        }

        //---------------------------------------------------
        public CListeQuerySource ListeSources
        {
            get
            {
                return m_listeSources;
            }
        }

        //---------------------------------------------------
        //En mode compatibilité, les tables generées par des tableaux croisés se nomme TABLE1, TABLE2, ...
        public bool ModeCompatibilteTimos4_0_1_3
        {
            get
            {
                return m_bModeCompatibilitéTimos4_0_1_3;
            }
            set
            {
                m_bModeCompatibilitéTimos4_0_1_3 = value;
            }
        }

        //---------------------------------------------------
        public void AddSource(CEasyQuerySource source)
        {
            m_listeSources.AddSource(source);
        }

        //---------------------------------------------------
        public void RemoveSource(CEasyQuerySource source)
        {
            m_listeSources.RemoveSource(source);
        }

        //---------------------------------------------------
        public virtual string Libelle
        {
            get
            {
                return m_strLibelle;
            }
            set
            {
                m_strLibelle = value;
            }
        }

        //---------------------------------------------------
        public bool UseRuntimeCache
        {
            get
            {
                return m_bUseRuntimeCache;
            }
            set
            {
                m_bUseRuntimeCache = value;
                if (!value)
                    ClearCache();
            }
        }

        //---------------------------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return true;
            }
        }

        //---------------------------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get { return m_listeObjets.ToArray(); }
        }

        //---------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            m_listeObjets.Add(child);
            return true;
        }

        //---------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return m_listeObjets.Contains(child);
        }

        //---------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            m_listeObjets.Remove(child);
        }

        //---------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            m_listeObjets.Remove(child);
            m_listeObjets.Add(child);
        }

        //---------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            m_listeObjets.Remove(child);
            m_listeObjets.Insert(0, child);
        }

        //---------------------------------------------------
        public override bool AutoExpandFromChildren
        {
            get
            {
                return true;
            }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 3;
            //1 : ajout des variables
            //2 : stockage de l'id de next variable !
            //3 : ajout du mode compatiblité Timos 4.0.1.3
        }

        //---------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<I2iObjetGraphique>(m_listeObjets);
            if (!result)
                return result;
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                foreach (I2iObjetGraphique objet in m_listeObjets)
                    objet.Parent = this;
            }
            serializer.TraiteString(ref m_strLibelle);

            if (nVersion >= 1)
            {
                result = serializer.TraiteListe<CVariableDynamique>(m_listeVariables);
                if (!result)
                    return result;
            }
            else
                m_listeVariables = new List<CVariableDynamique>();
            if (nVersion ==2)
            {
                int nTmp = 0;
                serializer.TraiteInt(ref nTmp);
            }
            if (nVersion >= 3)
                serializer.TraiteBool(ref m_bModeCompatibilitéTimos4_0_1_3);
            else
                m_bModeCompatibilitéTimos4_0_1_3 = true;
            return result;
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {

        }

        //---------------------------------------------------
        public virtual DataTable GetTable(string strNomTable, CListeQuerySource sources)
        {
            foreach (I2iObjetGraphique objet in Childs)
            {
                IObjetDeEasyQuery ob = objet as IObjetDeEasyQuery;
                if (ob != null && ob.NomFinal == strNomTable)
                {
                    CResultAErreur result = ob.GetDatas(sources);
                    if (result)
                        return result.Data as DataTable;
                }
            }
            return null;
        }

        //---------------------------------------------------
        public virtual DataTable GetTable(string strNomTable)
        {
            return GetTable(strNomTable, m_listeSources);
        }

        //---------------------------------------------------
        public virtual IObjetDeEasyQuery GetObjet(string strId)
        {
            foreach (I2iObjetGraphique objet in Childs)
            {
                IObjetDeEasyQuery ob = objet as IObjetDeEasyQuery;
                if (ob != null && ob.Id == strId)
                    return ob;
            }
            return null;
        }

        //-------------------------------------------------------------
        public virtual CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            foreach (I2iObjetGraphique objet in Childs)
            {
                CODEQBase instance = objet as CODEQBase;
                if (instance != null)
                {
                    DataTable table = new DataTable();
                    foreach (IColumnDeEasyQuery col in instance.Columns)
                    {
                        try
                        {
                            table.Columns.Add(col.ColumnName, col.DataType);
                        }
                        catch { }
                    }
                    lst.Add(new CDefinitionProprieteDynamiqueDataTable(instance.NomFinal, table));
                }
            }
            foreach (IVariableDynamique variable in ListeVariables)
            {
                bool bHasSubs = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
                if (variable is CVariableDynamique)
                {
                    CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable as CVariableDynamique, bHasSubs);
                    lst.Add(def);
                }
            }
            return lst.ToArray();
        }

        //-------------------------------------------------------------
        public bool FindSource(IColumnDeEasyQuery colonne,
            IObjetDeEasyQuery objetContenantLaColonne,
            out ITableDefinition tableSource,
            out IColumnDefinition colSource)
        {
            CColumnEQFromSource colFromSource = colonne as CColumnEQFromSource;
            tableSource = null;
            colSource = null;
            if (colFromSource == null)
                return false;

            CODEQFromObjetsSource objASource = objetContenantLaColonne as CODEQFromObjetsSource;
            if (objASource != null)
            {
                foreach (IObjetDeEasyQuery source in objASource.ElementsSource)
                {
                    //Certaines tables n'ont pas leur propres colonnes (les filtres par exemple, mais contiennent directement
                    //les colonnes de leur source. Il faut donc également chercher dans les colonnes source qui
                    //ont l'id de la colonne.
                    IColumnDeEasyQuery colDeSource = source.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource || c.Id == colFromSource.Id);
                    if (colDeSource != null)
                        return FindSource(colDeSource, source, out tableSource, out colSource);
                }

            }
            CODEQTableFromBase objFromBase = objetContenantLaColonne as CODEQTableFromBase;
            if (objFromBase != null)
            {
                tableSource = objFromBase.TableDefinition;
                colSource = tableSource.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
                return colSource != null;
            }
            return false;
        }

        //---------------------------------------------------------------------------
        [DynamicMethod("Returns a table", "Table name")]
        public CDynamicDataTable GetTableData(string strTableName)
        {
            foreach (IObjetDeEasyQuery objet in Childs)
            {
                if (objet.NomFinal.ToUpper() == strTableName.ToUpper())
                {
                    CResultAErreur result = objet.GetDatas(m_listeSources);
                    if (!result || !(result.Data is DataTable))
                        return null;
                    CDynamicDataTable table = new CDynamicDataTable(result.Data as DataTable);
                    return table;
                }
            }
            return null;
        }

        //---------------------------------------------------------------------------
        [DynamicMethod("Returns a data source from its name", "Source name")]
        public CEasyQuerySource GetSourceFromName(string strSourceName)
        {
            if (m_listeSources != null)
                return m_listeSources.GetSourceFromName(strSourceName);
            return null;
        }

        //---------------------------------------------------------------------------
        [DynamicMethod("Returns a data source from its ID", "Source Id")]
        public CEasyQuerySource GetSourceFromId(string strSourceId)
        {
            if (m_listeSources != null)
                return m_listeSources.GetSourceFromId(strSourceId);
            return null;
        }

        //---------------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.SetValeurChamp(variable, valeur);
            CResultAErreur result = CResultAErreur.True;
            if (variable != null)
                return SetValeurChamp(variable.IdVariable, valeur);
            return result;
        }

        //---------------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.SetValeurChamp(strIdVariable, valeur);
            m_dicValeursVariables[strIdVariable] = valeur;
            return CResultAErreur.True;
        }

        //---------------------------------------------------------------------------
        public object GetValeurChamp(IVariableDynamique variable)
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.GetValeurChamp(variable);
            if (variable != null)
                return GetValeurChamp(variable.IdVariable);
            return null;
        }

        //---------------------------------------------------------------------------
        public object GetValeurChamp(string strIdVariable)
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.GetValeurChamp(strIdVariable);
            object ret = null;
            if (!m_dicValeursVariables.ContainsKey(strIdVariable))
            {
                IVariableDynamique variable = GetVariable(strIdVariable);
                IVariableDynamiqueAValeurParDefaut variableADefaut = variable as IVariableDynamiqueAValeurParDefaut;
                if (variableADefaut != null)
                    return variableADefaut.GetValeurParDefaut();
                IVariableDynamiqueCalculee variableCalculee = variable as IVariableDynamiqueCalculee;
                if (variableCalculee != null && variableCalculee.FormuleDeCalcul != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(this);
                    CResultAErreur result = variableCalculee.FormuleDeCalcul.Eval(ctx);
                    if (result)
                        return result.Data;
                }
            }
            
            m_dicValeursVariables.TryGetValue(strIdVariable, out ret);
            return ret;
        }

        //---------------------------------------------------------------------------
        public string GetNewIdForVariable()
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.GetNewIdForVariable();
            return CUniqueIdentifier.GetNew();
        }

        //---------------------------------------------------------------------------
        public void OnChangeVariable(IVariableDynamique variable)
        {
        }

        //---------------------------------------------------------------------------
        public IVariableDynamique[] ListeVariables
        {
            get
            {
                if (m_elementAVariablesExternes != null)
                    return m_elementAVariablesExternes.ListeVariables;
                return m_listeVariables.ToArray();
            }
        }

        //---------------------------------------------------------------------------
        public void AddVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExternes != null)
            {
                m_elementAVariablesExternes.AddVariable(variable);
                return;
            }
            if (variable is CVariableDynamique)
                m_listeVariables.Add(variable as CVariableDynamique);
        }

        //---------------------------------------------------------------------------
        public void RemoveVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExternes != null)
            {
                m_elementAVariablesExternes.RemoveVariable(variable);
                return;
            }
            m_listeVariables.Remove(variable as CVariableDynamique);
        }

        //---------------------------------------------------------------------------
        public bool IsVariableUtilisee(IVariableDynamique variable)
        {
            return false;
        }

        //---------------------------------------------------------------------------
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            if (m_elementAVariablesExternes != null)
                return m_elementAVariablesExternes.GetVariable(strIdVariable);
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return variable;
            return null;
        }


        //---------------------------------------------------------------------------
        [DynamicMethod("Returns a query variable value", "Variable name")]
        public object GetVariableValue(string strVariableName)
        {
            foreach (IVariableDynamique variable in ListeVariables)
            {
                if (variable.Nom.ToUpper() == strVariableName.ToUpper())
                    return GetValeurChamp(variable);
            }
            return null;
        }

        //---------------------------------------------------------------------------
        [DynamicMethod("Set a variable value", "Variable name", "value")]
        public void SetVariableValue(string strVariableName, object value)
        {
            foreach (IVariableDynamique variable in ListeVariables)
            {
                if (variable.Nom.ToUpper() == strVariableName.ToUpper())
                {
                    SetValeurChamp(variable, value);
                    return;
                }
            }
        }

        //---------------------------------------------------------------------------
        public IElementAVariablesDynamiquesBase ElementAVariablesExternes
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

        #region IObjetAIContexteDonnee Membres

        //------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return m_contexteDonnee;
            }
            set
            {
                m_contexteDonnee = value;
            }
        }


        #endregion

        #region I2iCloneableAvecTraitementApresClonage Membres

        //------------------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            IContexteDonnee = ((IObjetAIContexteDonnee)source).IContexteDonnee;
        }

        #endregion

        //------------------------------------------------------------------
        [DynamicMethod("Clear data cache")]
        public void ClearCache()
        {
            foreach (I2iObjetGraphique objetGraphique in Childs)
            {
                IObjetDeEasyQuery obj = objetGraphique as IObjetDeEasyQuery;
                if ( obj != null )
                    obj.ClearCache();
            }
        }


    }
}
