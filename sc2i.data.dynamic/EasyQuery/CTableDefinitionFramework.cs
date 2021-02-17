using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using System.Data;
using System.Collections;

namespace sc2i.data.dynamic.easyquery
{
    [Serializable]
    public class CTableDefinitionFramework : CTableDefinitionBase
    {
        private Type m_typeSource = null;

        //--------------------------------------------------------
        public CTableDefinitionFramework()
        {
        }

        //--------------------------------------------------------
        public CTableDefinitionFramework(Type typeSource)
            :base()
        {
            m_typeSource = typeSource;
        }

        //--------------------------------------------------------
        public Type TypeSource
        {
            get
            {
                return m_typeSource;
            }
        }
        //--------------------------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSource)
        {
            return CResultAErreur.True;
        }

        //--------------------------------------------------------
        public CResultAErreur GetDonneesSource(CFiltreData filtre, params string[] strIdsColonnesSource)
        {
            CResultAErreur result = CResultAErreur.True;
            C2iRequeteAvancee requete = new C2iRequeteAvancee();
            requete.TableInterrogee = CContexteDonnee.GetNomTableForType(m_typeSource);
            requete.FiltreAAppliquer = filtre;
            List<string> lstIdsCols = new List<string>();
            HashSet<string> setToAdd = null;
            if (strIdsColonnesSource != null && strIdsColonnesSource.Length > 0)
            {
                setToAdd = new HashSet<string>();
                foreach (string strIdCol in strIdsColonnesSource)
                    setToAdd.Add(strIdCol);
            }

            string strChampId = "";

            //Lecture de la table de base
            foreach (IColumnDefinition col in Columns)
            {
                CColumnDefinitionChampDeTable colChamp = col as CColumnDefinitionChampDeTable;
                if (colChamp != null)
                {
                    if (setToAdd == null || setToAdd.Contains(colChamp.ColumnName))
                    {
                        C2iChampDeRequete champ = new C2iChampDeRequete(
                            col.ColumnName,
                            new CSourceDeChampDeRequete(col.ColumnName),
                            colChamp.DataType,
                            OperationsAgregation.None, 
                            false);
                        requete.ListeChamps.Add(champ);
                    }
                }
                
            }
            
            result = requete.ExecuteRequete(CContexteDonneeSysteme.GetInstance().IdSession);
            if (!result || !(result.Data is DataTable))
                return result;
            return result;
        }

        //--------------------------------------------------------
        public override string Id
        {
            get
            {
                if (m_typeSource != null)
                    return m_typeSource.ToString();
                return "?";
            }
        }

        //--------------------------------------------------------
        public override string TableName
        {
            get
            {
                if (m_typeSource != null)
                    return DynamicClassAttribute.GetNomConvivial(m_typeSource);
                return "?";
            }
            set
            {
                
            }
        }

        //--------------------------------------------------------
        public override IEnumerable<IColumnDefinition> Columns
        {
            get
            {
                if (base.Columns.Count() == 0)
                {
                    if (m_typeSource != null)
                    {
                        List<IColumnDefinition> lstCols = new List<IColumnDefinition>();
                        CStructureTable structure = CStructureTable.GetStructure(m_typeSource);
                        List<CInfoChampTable> lst = new List<CInfoChampTable>();
                        lst.AddRange(structure.Champs);
                        lst.Sort((x, y) => x.NomConvivial.CompareTo(y.NomConvivial));
                        HashSet<string> champsRelations = new HashSet<string>();
                        foreach (CInfoRelation relation in structure.RelationsParentes)
                        {
                            foreach (string strChamp in relation.ChampsFille)
                            {
                                champsRelations.Add(strChamp);
                            }
                        }

                        foreach (CInfoChampTable info in structure.Champs)
                        {
                            if (info.NomConvivial.Trim().Length > 0 || champsRelations.Contains ( info.NomChamp ))
                            {
                                CColumnDefinitionChampDeTable col = new CColumnDefinitionChampDeTable(
                                this,
                                info);
                                lstCols.Add ( col );
                            }
                        }

                        /*if (typeof(IElementAChamps).IsAssignableFrom(m_typeSource))
                        {
                            CRoleChampCustom role = CRoleChampCustom.GetRoleForType ( m_typeSource );
                            if ( role != null )
                            {
                            //Trouve les champs custom associés
                            CListeObjetsDonnees lstChamps = new CListeObjetsDonnees(CContexteDonneeSysteme.GetInstance(), typeof(CChampCustom));
                            lstChamps.Filtre = new CFiltreData(CChampCustom.c_champCodeRole + "=@1",
                                role.CodeRole );
                                foreach ( CChampCustom champ in lstChamps )
                                {
                                    if ( champ.TypeDonneeChamp != null && champ.TypeDonneeChamp.TypeDonnee != TypeDonnee.tObjetDonneeAIdNumeriqueAuto )
                                    {
                                        CColumnDefinitionChampCustom col = new CColumnDefinitionChampCustom(
                                            this, 
                                            champ);
                                        lstCols.Add ( col );
                                    }
                                }
                            }

                        }*/

                        foreach (IColumnDefinition col in lstCols)
                            AddColumn(col);
                    }
                }
                return base.Columns;
            }
        }

        //--------------------------------------------------------------------
        public override IObjetDeEasyQuery GetObjetDeEasyQueryParDefaut()
        {
            return new CODEQTableFromTableFramework(this);
        }

        //--------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteType(ref m_typeSource);
            return result;
        }
    }

}
