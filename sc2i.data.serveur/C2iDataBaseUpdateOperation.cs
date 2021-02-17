using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using sc2i.common;
using sc2i.multitiers.server;

namespace sc2i.data.serveur
{
    //---------------------------------------------------------------------------------
	public class C2iDataBaseUpdateOperationList : List<C2iDataBaseUpdateOperation>
	{
		public C2iDataBaseUpdateOperationList()
		{
		}

		public void Add(Type t)
		{
			Add(new C2iDataBaseUpdateOperationTable(t));
		}
		public void Add(Type t, bool bSuppression)
		{
			Add(new C2iDataBaseUpdateOperationTable(t, bSuppression));
		}
		public void Add(Type t, params string[] strChampsAutorisesANull)
		{
			Add(new C2iDataBaseUpdateOperationTable(t, strChampsAutorisesANull));
		}
		public void Add(DelegueMethodeMAJ fonction)
		{
			Add(new C2iDataBaseUpdateOperationFonction(fonction));
		}
	}

    //---------------------------------------------------------------------------------
	public abstract class  C2iDataBaseUpdateOperation
	{
		public abstract CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur);
		public abstract string DescriptionOperation { get;}
	}

    //---------------------------------------------------------------------------------
	public class C2iDataBaseUpdateOperationTable : C2iDataBaseUpdateOperation
	{
		public C2iDataBaseUpdateOperationTable(Type t, params string[] strChampsAutorisesANull)
		{
			m_type = t;
			m_strChampsAutorisesANull = strChampsAutorisesANull;
		}
		public C2iDataBaseUpdateOperationTable(Type t, bool bSuppression)
		{
			m_type = t;
			m_bSuppression = bSuppression;
		}

		string[] m_strChampsAutorisesANull = null;

		Type m_type;
		private bool m_bSuppression = false;
		public bool SuppressionTable
		{
			get
			{
				return m_bSuppression;
			}
		}
		/// <summary>
		/// Liste des champs pour lesquels on force AllowDBNull
		/// </summary>
		public string[] ChampsAutorisesANull
		{
			get
			{
				if (m_strChampsAutorisesANull == null)
					return new string[0];
				return m_strChampsAutorisesANull;
			}
		}
		public Type Type
		{
			get
			{
				return m_type;
			}
		}

		public override string DescriptionOperation
		{
			get 
			{
				if (m_bSuppression)
					return I.T("Deleting table @1 |30000",DynamicClassAttribute.GetNomConvivial(m_type));
				else
					return I.T("Updating table @1 |30001",DynamicClassAttribute.GetNomConvivial(m_type));
			}
		}

		public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
		{
			IDataBaseCreator creator = connection.GetDataBaseCreator();
			string strNomTable = DynamicClassAttribute.GetNomConvivial(m_type);

			if (!m_bSuppression)
			{
				#if PDA
					return COracleTableCreator.CreationOuUpdateTableFromType(tp, m_nIdSession);
				#else
				return creator.CreationOuUpdateTableFromType(m_type, new ArrayList(m_strChampsAutorisesANull));
				#endif
			}
			else
			{
				#if PDA
					return CSQLCeTableCreator.SuppressionTable(strNomTable, m_nIdSession);
				#else
					return creator.DeleteTable(CStructureTable.GetStructure(m_type).NomTableInDb);
				#endif
			}
		}

	}

    //---------------------------------------------------------------------------------
    public class C2iDataBaseUpdateOperationDeleteChamp : C2iDataBaseUpdateOperation
    {
        private string m_strNomTable = "";
        private string m_strNomChamp = "";

        public C2iDataBaseUpdateOperationDeleteChamp(string strNomTable, string strNomChamp)
        {
            m_strNomTable = strNomTable;
            m_strNomChamp = strNomChamp;
        }

        public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
        {
            if (connection.GetDataBaseCreator().ChampExists(m_strNomTable, m_strNomChamp))
                return connection.GetDataBaseCreator().DeleteChamp(m_strNomTable, m_strNomChamp);
            return CResultAErreur.True;
        }

        public override string DescriptionOperation
        {
            get { return "Delete field " + m_strNomTable + "/" + m_strNomChamp; }
        }
    }



    //---------------------------------------------------------------------------------
	public class C2iDataBaseUpdateOperationFonction : C2iDataBaseUpdateOperation
	{
		public C2iDataBaseUpdateOperationFonction(DelegueMethodeMAJ method)
		{
			m_delegueMethod = method;
			object[] attributsDescriptions = method.Method.GetCustomAttributes(typeof(DescriptionFonctionMAJ), false);
			if (attributsDescriptions.Length == 1)
			{
				DescriptionFonctionMAJ attDescripFct = (DescriptionFonctionMAJ)attributsDescriptions[0];
				m_nomFonction = attDescripFct.NomFonction;
				m_descripFonction = attDescripFct.DescriptionFonction;
			}
			else
			{
				m_nomFonction = method.Method.Name;
				m_descripFonction = I.T("unknown or masked|30002");
			}
		}

		DelegueMethodeMAJ m_delegueMethod;
		string m_nomFonction;
		string m_descripFonction;

		public DelegueMethodeMAJ Delegue
		{
			get
			{
				return m_delegueMethod;
			}
		}
		public string NomFonction
		{
			get
			{
				return m_nomFonction;
			}
		}
		public string DescriptionFonction
		{
			get
			{
				return m_descripFonction;
			}
		}

		public override string DescriptionOperation
		{
			get { return I.T("Function execution @1 |30003", m_nomFonction); }
		}
		public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
		{
			return Delegue(connection.GetDataBaseCreator());
		}
	}

    //---------------------------------------------------------------------------------
	public class C2iDataBaseUpdateOperationNoSetVersionBase : C2iDataBaseUpdateOperation
	{

		public C2iDataBaseUpdateOperationNoSetVersionBase()
		{
		}
		public override string DescriptionOperation
		{
			get { return I.T("Blocking database version update|30004"); }
		}
		public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
		{
			return CResultAErreur.True;
		}
	}

    //---------------------------------------------------------------------------------
    public class C2iDataBaseUpdateOperationDeleteTable : C2iDataBaseUpdateOperation
    {
        private string m_strNomTable = "";
        public C2iDataBaseUpdateOperationDeleteTable(string strNomTable)
        {
            m_strNomTable = strNomTable;
        }
        public override string DescriptionOperation
        {
            get { return I.T("Delete a table @1 from db|20006", m_strNomTable); }
        }
        public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            IDataBaseCreator createur = connection.GetDataBaseCreator();
            if ( createur.TableExists ( m_strNomTable ) )
                result = createur.DeleteTable(m_strNomTable);
            return result;
        }
    }

	public delegate CResultAErreur DelegueMethodeMAJ(IDataBaseCreator creator);

    //---------------------------------------------------------------------------------
	[global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	sealed class DescriptionFonctionMAJ : Attribute
	{
		readonly string _descriptionFct;
		readonly string _nomFct;

		public DescriptionFonctionMAJ(string nomFonction, string descriptionMethode)
		{
			_nomFct = nomFonction;
			_descriptionFct = descriptionMethode;

		}
		public string NomFonction
		{
			get
			{
				return _nomFct;
			}
		}
		public string DescriptionFonction
		{
			get
			{
				return _descriptionFct;
			}
		}
	}

    public class C2iDatabaseUpdateOperationCalculeIdUniverselsManquants : C2iDataBaseUpdateOperation
    {
        private Type m_type = null;

        public C2iDatabaseUpdateOperationCalculeIdUniverselsManquants(Type tp)
        {
            m_type = tp;
        }


        //--------------------------------------------------------------------------------------------------------------------
        public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            if (!typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(m_type) || !CObjetDonnee.TypeManageIdUniversel(m_type) )
            {
                result.EmpileErreur(m_type.ToString() + " can not have Universal Id");
            }

            string strNomTable = CContexteDonnee.GetNomTableForType(m_type);

            CStructureTable structure = CStructureTable.GetStructure(m_type);
            string strChampId = structure.ChampsId[0].NomChamp;

            C2iRequeteAvancee requete = new C2iRequeteAvancee();
            requete.FiltreAAppliquer = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1", "");
            requete.TableInterrogee = CContexteDonnee.GetNomTableForType(m_type);
            requete.ListeChamps.Add(new C2iChampDeRequete(strChampId, new CSourceDeChampDeRequete(strChampId),
                typeof(int),
                OperationsAgregation.None, true));
            Console.WriteLine("Update Universal id on " + strNomTable);
            result = requete.ExecuteRequete(connection.IdSession);
            if (!result)
                return result;
            string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
            DataTable table = result.Data as DataTable;
            int nNb = 0;
            bool bWasInTrans = connection.IsInTrans();
            if (bWasInTrans)
                connection.CommitTrans();
            DateTime dt = DateTime.Now;
            if (table.Rows.Count > 0)
            {
                if (table.Rows.Count > 10000 && connection is COracleDatabaseConnexion)
                {
                    string strQuery = "Update " + strNomTableInDb + " set " + CObjetDonnee.c_champIdUniversel +
                        "=CONCAT('" + strNomTableInDb + "'," + strChampId + ")";
                    result = connection.RunStatement(strQuery);
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string strQuery = "Update " + strNomTableInDb + " set " +
                            CObjetDonnee.c_champIdUniversel + "='" + CUniqueIdentifier.GetNew() + "' where " +
                            strChampId + "=" + row[0].ToString();
                        result = connection.ExecuteScalar(strQuery);
                        if (!result)
                            break;
                        nNb++;
                        if (nNb % 100 == 0)
                        {
                            TimeSpan sp = DateTime.Now - dt;
                            double fVal = (double)sp.TotalSeconds / (double)nNb * (double)(table.Rows.Count - nNb);
                            Console.WriteLine(strNomTableInDb + " " + nNb + "/" + table.Rows.Count + " reste " + fVal.ToString("0s"));
                        }
                    }
                }
            }
            if (bWasInTrans)
                connection.BeginTrans();
            return result;
            /*

            //Trouve tous les éléments du type qui n'ont pas d'id universel
            using (CContexteDonnee ctx = new CContexteDonnee(connection.IdSession, true, false))
            {
                CListeObjetsDonnees lst = new CListeObjetsDonnees(ctx, m_type, false);
                lst.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1", "");
                lst.Filtre.IgnorerVersionDeContexte = true;
                lst.AppliquerFiltreAffichage = false;

                foreach (IObjetDonnee objet in lst.ToArrayList())
                {
                    objet.Row[CObjetDonnee.c_champIdUniversel] = CUniqueIdentifier.GetNew();
                }
                string strNomTable = CContexteDonnee.GetNomTableForType(m_type);
                CObjetServeur objetServeur = CContexteDonnee.GetTableLoader(strNomTable, null, connection.IdSession) as CObjetServeur;
                int nCount = ctx.Tables[strNomTable].Rows.Count;
                DataTable tableSource = ctx.Tables[strNomTable];

                 List<string> lstChampsExclus = new List<string>();
                HashSet<string> lstIDs = new HashSet<string>();
                CStructureTable structure = CStructureTable.GetStructure(m_type);
                foreach (CInfoChampTable info in structure.ChampsId)
                    lstIDs.Add(info.NomChamp);

                foreach (CInfoChampTable champ in structure.Champs)
                    if (champ.NomChamp != CObjetDonnee.c_champIdUniversel && !lstIDs.Contains(champ.NomChamp))
                        lstChampsExclus.Add(champ.NomChamp);
                
                IDataAdapter adapter = objetServeur.GetDataAdapter(DataRowState.Modified, lstChampsExclus.ToArray());

                for (int nRow = 0; nRow < nCount; nRow += 5000)
                {
                    
                    using (DataSet dsCopie = new DataSet())
                    {
                        dsCopie.EnforceConstraints = false;
                        DataTable tableCopie = ctx.Tables[strNomTable].Clone();
                        tableCopie.BeginLoadData();
                        dsCopie.Tables.Add(tableCopie);
                        int nMax = Math.Min(nRow + 5000, nCount);
                        DateTime dt = DateTime.Now;
                        for (int n = nRow; n < nMax; n++)
                        {
                            tableCopie.ImportRow(tableSource.Rows[n]);
                        }
                        TimeSpan sp = DateTime.Now - dt;
                        Console.WriteLine("Write 1" + strNomTable + " " + nRow + "/" + nCount+"  "+sp.TotalSeconds.ToString());
                        adapter.Update(dsCopie);
                        sp = DateTime.Now - dt;
                        Console.WriteLine("Write 2" + strNomTable + " " + nRow + "/" + nCount + "  " + sp.TotalSeconds.ToString());
                    }
                }
            }
            return result;*/
        }

        public override string DescriptionOperation
        {
            get { return "Universal id of " + m_type.ToString(); }
        }
    }

    public class C2iDatabaseUpdateOperationRemplaceIdParDbKey : C2iDataBaseUpdateOperation
    {
        private Type m_type = null;
        private string m_strChampIdObjet = "";
        private string m_strChampDbKey = "";
        private string m_strChampTypeObjet = "";
        private Type m_typeObjetFixe = null;

        ///-------------------------------------------------------
        public C2iDatabaseUpdateOperationRemplaceIdParDbKey(
            Type tp,
            string strOldChampContenantId, 
            string strChampTypeObjet,
            string strNewChampContenantDbKey)
        {
            m_type = tp;
            m_strChampDbKey = strNewChampContenantDbKey;
            m_strChampIdObjet = strOldChampContenantId;
            m_strChampTypeObjet = strChampTypeObjet;
            m_typeObjetFixe = null;
        }

        ///-------------------------------------------------------
        public C2iDatabaseUpdateOperationRemplaceIdParDbKey(
            Type tp,
            string strOldChampContenantId, 
            Type typeObjetFixe,
            string strNewChampContenantDbKey)
        {
            m_type = tp;
            m_strChampDbKey = strNewChampContenantDbKey;
            m_strChampIdObjet = strOldChampContenantId;
            m_strChampTypeObjet = null;
            m_typeObjetFixe = typeObjetFixe;
        }

        //--------------------------------------------------------------------------------------------------------------------
        public override CResultAErreur ExecuterOperation(IDatabaseConnexion connection, IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            IDataBaseCreator createur = connection.GetDataBaseCreator();
            string strNomTableInContexte = CContexteDonnee.GetNomTableForType(m_type);
            string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTableInContexte);
            createur.CreationOuUpdateTableFromType(m_type);
            string strChampIdObjet = m_strChampIdObjet;
            if (m_strChampIdObjet.StartsWith("#SQL#"))
                strChampIdObjet = m_strChampIdObjet.Substring("#SQL#".Length);
            CObjetServeur.ClearCacheSchemas();
            if (createur.ChampExists(strNomTableInDb, strChampIdObjet) && 
                createur.ChampExists( strNomTableInDb, m_strChampDbKey) )
            {
                using ( CContexteDonnee ctx = new CContexteDonnee(connection.IdSession, true, false ) )
                {
                    C2iRequeteAvancee requete = new C2iRequeteAvancee();
                    requete.TableInterrogee = strNomTableInDb;
                    CStructureTable structure = CStructureTable.GetStructure(m_type);
                    /*
                    requete.ListeChamps.Add ( new C2iChampDeRequete ( 
                        "ID",
                        new CSourceDeChampDeRequete(structure.ChampsId[0].NomChamp),
                        typeof(int),
                        OperationsAgregation.None, 
                        true ));*/
                    requete.ListeChamps.Add ( new C2iChampDeRequete(
                        "IDOBJET",
                        new CSourceDeChampDeRequete(m_strChampIdObjet),
                        typeof(int),
                        OperationsAgregation.None,
                        true));
                    if ( m_typeObjetFixe == null )
                    {
                        requete.ListeChamps.Add(new C2iChampDeRequete(
                            "TYPEOBJET",
                            new CSourceDeChampDeRequete(m_strChampTypeObjet),
                            typeof(string),
                            OperationsAgregation.None,
                            true));
                    }
                    result = requete.ExecuteRequete ( connection.IdSession);
                    if ( !result )
                        return result;
                    DataTable table = result.Data as DataTable;
                    Dictionary<int, int?> dicIdToIdObjet = new Dictionary<int,int?>();
                    string strFieldIdObjetInTable = m_strChampIdObjet.Replace("#SQL#", "");
                    foreach ( DataRow row in table.Rows )
                    {
                        object val = row["IDOBJET"];
                        int? nValId = val == DBNull.Value?null:(int?)val;
                        if (nValId != null && nValId >= 0)
                        {
                            CDbKey key = null;
                            Type tp = m_typeObjetFixe;
                            if (tp == null)//Type non fixe
                            {
                                string strType = (string)row["TYPEOBJET"];
                                tp = CActivatorSurChaine.GetType(strType);
                            }
                            if (tp != null)
                            {
                                CObjetDonneeAIdNumerique objPointe = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, ctx);
                                if (objPointe.ReadIfExists(nValId.Value))
                                    key = objPointe.DbKey;
                            }
                            if (key != null)
                            {
                                string strRequete = "Update " + strNomTableInDb + " set " +
                                    m_strChampDbKey + "=" + connection.GetStringForRequete(key.StringValue) + " where " +
                                    strFieldIdObjetInTable + "=" + nValId.Value;
                                if (m_typeObjetFixe == null)
                                    strRequete += " and " + m_strChampTypeObjet + "='" +
                                        row["TYPEOBJET"].ToString() + "'";
                                connection.RunStatement(strRequete);
                            }
                        }
                    }
                }
                //Supprime le champ
                createur.DeleteChamp(strNomTableInDb, strChampIdObjet);
            }
            return result;
        }

        public override string DescriptionOperation
        {
            get { return "Replace Id by Key for " + DynamicClassAttribute.GetNomConvivial(m_type); }
        }
    }


}
