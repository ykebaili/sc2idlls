using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.common;
using System.Drawing;
using System.Collections;
using System.Data;
using sc2i.expression;
using futurocom.easyquery.CAML;
using futurocom.easyquery;
using sc2i.data.dynamic.EasyQuery;


namespace sc2i.data.dynamic.easyquery
{
    [Serializable]
    public class CODEQTableFromTableFramework :
        CODEQBase,
        IObjetDeEasyQuery,
        IODEQTableFromFramework
    {
        //Liste des colonnes utilisées dans la table
        private List<IColumnDeEasyQuery> m_listeColonnes = new List<IColumnDeEasyQuery>();

        private CFiltreDynamique m_filtre = null;

        private ITableDefinition m_definitionTable;

        //-------------------------------------------------------
        public CODEQTableFromTableFramework()
            : base()
        {
            Size = new Size(80, 150);
        }

        //-------------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Object table source|20067"); }
        }

        //-------------------------------------------------------
        public CODEQTableFromTableFramework(ITableDefinition definition)
            : base()
        {
            NomFinal = definition.TableName;
            Size = new Size(80, 150);
            m_definitionTable = definition;
        }


        //-------------------------------------------------------
        public ITableDefinition TableDefinition
        {
            get
            {
                return m_definitionTable;
            }
        }

        //-------------------------------------------------------
        public CFiltreDynamique FiltreDynamique
        {
            get
            {
                if (m_filtre == null)
                {
                    m_filtre = new CFiltreDynamique();
                }
                CContexteDonnee ctxDonnee = Query.IContexteDonnee as CContexteDonnee;
                if (ctxDonnee != null)
                    m_filtre.ElementAVariablesExterne = new CElementAVariablesDynamiquesAvecContexteFromIElementAVariablesDynamiques(Query, ctxDonnee);
                m_filtre.TypeElements = TypeSource;
                return m_filtre;
            }
            set
            {
                m_filtre = value;
            }
        }

        //-------------------------------------------------------
        public Type TypeSource
        {
            get
            {
                CTableDefinitionFramework table = m_definitionTable as CTableDefinitionFramework;
                if (table != null)
                    return table.TypeSource;
                return null;
            }
        }
                

        //-------------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CEasyQuery query = Query;
            CResultAErreur result = CResultAErreur.True;

            if (result && TypeSource == null)
            {
                result.EmpileErreur(I.T("Table object must be specified|20068"));
            }
            if (query == null || sources == null)
            {
                result.EmpileErreur(I.T("Query needs a source to provide datas|20069"));
            }

            C2iRequeteAvancee requete = new C2iRequeteAvancee();
            requete.TableInterrogee = CContexteDonnee.GetNomTableForType(TypeSource);

            result = FiltreDynamique.GetFiltreData();
            if (!result)
                return result;
            if (result.Data is CFiltreData)
                requete.FiltreAAppliquer = result.Data as CFiltreData;
            foreach (IColumnDeEasyQuery col in m_listeColonnes)
            {
                CColumnDeEasyQueryChampDeRequete colR = col as CColumnDeEasyQueryChampDeRequete;
                requete.ListeChamps.Add(colR);
            }
            DataTable table = null;
            if (requete.ListeChamps.Count > 0)
            {
                result = requete.ExecuteRequete(CContexteDonneeSysteme.GetInstance().IdSession);
                if (!result)
                {
                    result.EmpileErreur(I.T("Error on table @1|20070",NomFinal));
                    return result;
                }
                table = result.Data as DataTable;
            }
            else
                table = new DataTable();
            table.TableName = NomFinal;
            return result;
        }

        //---------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            return m_listeColonnes.AsReadOnly();
        }
        //---------------------------------------------------
        public void SetColonnesOrCalculees(IEnumerable<IColumnDeEasyQuery> cols)
        {
            m_listeColonnes = new List<IColumnDeEasyQuery>(cols);
        }

        //--------------------------------------------------
        public void AddColonneDeRequete(CColumnDeEasyQueryChampDeRequete col)
        {
            m_listeColonnes.Add(col);
        }

        //---------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> ColonnesOrCalculees
        {
            get
            {
                return m_listeColonnes.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        /// <summary>
        /// Retourne la colonne finale associé à une colonne de la base
        /// </summary>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public IColumnDeEasyQuery GetColonneFor(IColumnDefinition colonne)
        {
            return m_listeColonnes.FirstOrDefault(c => c is CColumnEQFromSource && ((CColumnEQFromSource)c).IdColumnSource == colonne.Id);
        }

        //-------------------------------------------------------
        public IColumnDefinition GetColumnDefinitionFor(IColumnDeEasyQuery colonne)
        {

            CColumnEQFromSource colFromSource = colonne as CColumnEQFromSource;
            if (m_definitionTable != null && colFromSource != null)
            {
                return m_definitionTable.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
            }
            return null;
        }



        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.MySerialize(serializer);
            if (!result)
                return result;

            result = serializer.TraiteObject<ITableDefinition>(ref m_definitionTable);
            if (!result)
                return result;
            serializer.TraiteListe<IColumnDeEasyQuery>(m_listeColonnes);
            serializer.TraiteObject<CFiltreDynamique>(ref m_filtre);
            return result;
        }

        //---------------------------------------------------------------------------
        public Type TypeElements
        {
            get 
            {
                return TypeSource;
            }
        }

        //---------------------------------------------------------------------------
        public IEnumerable<CColumnDeEasyQueryChampDeRequete> ChampsDeRequete
        {
            get {
                return from c in m_listeColonnes where c is CColumnDeEasyQueryChampDeRequete select c as CColumnDeEasyQueryChampDeRequete;
            }
        }
    }
}
