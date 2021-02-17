using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using System.Data;

namespace sc2i.data.dynamic.EasyQuery
{
    public class CDefinitionJeuDonneesEasyQuery : IDefinitionJeuDonnees
    {
        private CEasyQuery m_query = new CEasyQuery();
        private CListeQuerySource m_listeSources = new CListeQuerySource();
        private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesExternes = null;
        private List<string> m_listeTablesARetourner = new List<string>();

        private CContexteDonnee m_contexteDonnee = null;

        //------------------------------------------------
        public CDefinitionJeuDonneesEasyQuery()
        {
        }

        //------------------------------------------------
        public string LibelleTypeDefinitionJeuDonnee
        {
            get { return I.T("Easy Query|20071"); }
        }

        //------------------------------------------------
        public CEasyQuery EasyQueryAvecSource
        {
            get
            {
                m_query.Sources = m_listeSources.Sources;
                m_query.ElementAVariablesExternes = ElementAVariablesExterne;
                m_query.IContexteDonnee = ContexteDonnee;
                return m_query;
            }
            set
            {
                m_query = value;
                m_listeSources.ClearSources();
                if (m_query != null)
                {
                    foreach (CEasyQuerySource source in m_query.ListeSources.Sources)
                        m_listeSources.AddSource(source);
                }
            }
        }

        //------------------------------------------------
        public IEnumerable<string> IdTablesARetourner
        {
            get
            {
                return m_listeTablesARetourner.AsReadOnly();
            }
            set
            {
                m_listeTablesARetourner.Clear();
                if (value != null)
                    m_listeTablesARetourner.AddRange(value);
            }
        }

        //------------------------------------------------
        public CResultAErreur GetDonnees(IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, CListeObjetsDonnees listeDonnees, sc2i.common.IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            if (indicateur != null)
                indicateur.SetBornesSegment(0, IdTablesARetourner.Count());
                
            DataSet ds = new DataSet();
            int nNbTables = 0;
            if (m_query != null)
                m_query.ClearCache();
            foreach ( string strIdTable in IdTablesARetourner )
            {
                foreach ( object obj in m_query.Childs )
                {
                    IObjetDeEasyQuery q = obj as IObjetDeEasyQuery;
                    if (q.Id == strIdTable )
                    {
                        if (indicateur != null)
                            indicateur.SetInfo(q.NomFinal);
                        result = q.GetDatas ( m_query.ListeSources );
                        if (result)
                        {
                            DataTable table = result.Data as DataTable;
                            if (table.DataSet != null)
                            {
                                DataTable newTable = table.Clone();
                                newTable.BeginLoadData();
                                foreach (DataRow row in table.Rows)
                                    newTable.ImportRow(row);
                                newTable.EndLoadData();
                                table = newTable;
                            }
                            ds.Tables.Add(table);
                        }
                        else
                            return result;
                    }
                }
                if (indicateur != null)
                    indicateur.SetValue(nNbTables++);
            }
            if (indicateur != null)
                indicateur.SetValue(IdTablesARetourner.Count());

            result.Data = ds;

            return result;
        }

        //------------------------------------------------
        public IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesExterne
        {
            get
            {
                return m_elementAVariablesExternes;
            }
            set
            {
                m_elementAVariablesExternes = value;
                if (m_query != null)
                    m_query.ElementAVariablesExternes = value;
            }
        }

        //------------------------------------------------
        public CContexteDonnee ContexteDonnee
        {
            get
            {
                return m_contexteDonnee;
            }
            set
            {
                m_contexteDonnee = value;
                if (m_query != null)
                    m_query.IContexteDonnee = value;
            }

        }

        //------------------------------------------------
        public Type TypeDonneesEntree
        {
            get { return null; }
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }
        //------------------------------------------------
        public CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);

            if (!result)
                return result;

            result = serializer.TraiteObject<CEasyQuery>(ref m_query);
            if (result)
                result = serializer.TraiteObject<CListeQuerySource>(ref m_listeSources);
            if ( !result )
                return result;
            int nNbTables = m_listeTablesARetourner.Count;
            serializer.TraiteInt(ref nNbTables);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strTable in m_listeTablesARetourner)
                    {
                        string strTmp = strTable;
                        serializer.TraiteString(ref strTmp);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_listeTablesARetourner.Clear();
                    for (int n = 0; n < nNbTables; n++)
                    {
                        string strTmp = "";
                        serializer.TraiteString(ref strTmp);
                        m_listeTablesARetourner.Add(strTmp);
                    }
                    break;
            }
            return result;

        }

    }
}
