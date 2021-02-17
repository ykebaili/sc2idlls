using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;

using sc2i.common;
using System.Text;


namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de C2iDataAdapterForClasseAutoReferencee.
	/// </summary>
	public class C2iDataAdapterForClasseAutoReferencee : IDbDataAdapter
	{
		private Type m_typeDonnees;
		private IDbDataAdapter m_adapter;
		/// <summary>
		/// /////////////////////////////////////////////////////////////////
		/// </summary>
		public C2iDataAdapterForClasseAutoReferencee( Type tp, IDbDataAdapter adapterOrigine )
		{
			m_typeDonnees = tp;
			m_adapter = adapterOrigine;
		}

		/// /////////////////////////////////////////////////////////////////
		public IDbDataAdapter DataAdapterUtilise
		{
			get
			{
				return m_adapter;
			}
		}

        public delegate void RowInsertedEventHandler(object sender, DataRow row);


        /// /////////////////////////////////////////////////////////////////
        public event RowInsertedEventHandler RowInserted;

		/// /////////////////////////////////////////////////////////////////
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return m_adapter.MissingMappingAction;
			}
			set
			{
				m_adapter.MissingMappingAction = value;
			}
		}
		
		/// /////////////////////////////////////////////////////////////////
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return m_adapter.MissingSchemaAction;
			}
			set
			{
				m_adapter.MissingSchemaAction = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public ITableMappingCollection TableMappings
		{
			get
			{
				return m_adapter.TableMappings;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public int Fill	 ( DataSet ds )
		{
			return m_adapter.Fill(  ds );
		}

		/// /////////////////////////////////////////////////////////////////
		public DataTable[] FillSchema ( 
			DataSet ds,
			SchemaType schema )
		{
			return m_adapter.FillSchema ( ds, schema);
		}

		/// /////////////////////////////////////////////////////////////////
		public IDataParameter[] GetFillParameters()
		{
			return m_adapter.GetFillParameters();
		}

		/// /////////////////////////////////////////////////////////////////
		public IDbCommand DeleteCommand
		{
			get
			{
				return m_adapter.DeleteCommand;
			}
			set
			{
				m_adapter.DeleteCommand = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public IDbCommand InsertCommand
		{
			get
			{
				return m_adapter.InsertCommand;
			}
			set
			{
				m_adapter.InsertCommand = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public IDbCommand UpdateCommand
		{
			get
			{
				return m_adapter.UpdateCommand;
			}
			set
			{
				m_adapter.UpdateCommand = value;
			}
		}
		/// /////////////////////////////////////////////////////////////////
		public IDbCommand SelectCommand
		{
			get
			{
				return m_adapter.SelectCommand;
			}
			set
			{
				m_adapter.SelectCommand = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		private int ExecuteCommande ( IDbCommand command, DataRow row )
		{
			foreach ( IDataParameter parametre in command.Parameters )
			{
				parametre.Value = row[parametre.SourceColumn, parametre.SourceVersion];
			}
			int nRetour = 0;
			IDataReader reader = null;
			try
			{
                int nNbRetry = 3;
                while (nNbRetry > 0)
                {
                    try
                    {
                        reader = command.ExecuteReader();
                        break;
                    }
                    catch (Exception e)
                    {
                        GC.Collect();
                        nNbRetry--;
                        if (nNbRetry == 0)
                            throw e;
                    }
                }
				if ( reader != null && reader.Read() )
				{
					for ( int nField = 0; nField < reader.FieldCount; nField++ )
					{
						if ( row.Table.Columns[reader.GetName(nField)] != null )
							row[reader.GetName(nField)] = reader.GetValue(nField);
					}
				}
			}
			catch ( Exception e )
			{
				throw e;
			}
			finally
			{
				if (reader != null)
					nRetour = reader.RecordsAffected;
				if ( reader !=  null )
					reader.Close();
			}
			return nRetour;
		}

		/// /////////////////////////////////////////////////////////////////
		protected virtual int ExecuteAdd(IDbCommand commande, DataRow row)
		{
            int nNb = ExecuteCommande(commande, row);
            if (RowInserted != null)
                RowInserted(this, row);
            return nNb;
		}

		/// /////////////////////////////////////////////////////////////////
		protected virtual int ExecuteUpdate(IDbCommand commande, DataRow row)
		{
			int nNb = ExecuteCommande(commande, row);
            return nNb;
		}

		/// /////////////////////////////////////////////////////////////////
		protected virtual int ExecuteDelete(IDbCommand commande, DataRow row)
		{
			return ExecuteCommande ( commande, row );
		}

		private class CArbreDependance
		{
			private static int m_nId = 0;
			public readonly DataRow Row;
			private CArbreDependance m_arbreParent;
			private List<CArbreDependance> m_arbresFils = new List<CArbreDependance>();

			public CArbreDependance(DataRow row)
			{
				m_nId++;
				Row = row;
			}

			public CArbreDependance ArbreParent
			{
				get
				{
					return m_arbreParent;
				}
				set
				{
					if (m_arbreParent != null)
						m_arbreParent.m_arbresFils.Remove(this);
					m_arbreParent = value;
					value.m_arbresFils.Add(this);
				}
			}

			public void InsertFillesIntoArrayList(ArrayList lst)
			{
				foreach (CArbreDependance arbre in m_arbresFils)
				{
                    if (arbre.ArbreParent == null ||
                        arbre.ArbreParent.Row == null ||
                        lst.Contains(arbre.ArbreParent.Row))
                    {
                        if (!lst.Contains(arbre.Row))
                        {
                            lst.Add(arbre.Row);
                        }
                    }
					arbre.InsertFillesIntoArrayList(lst);
				}
			}

		}

		/// /////////////////////////////////////////////////////////////////
		public int Update ( DataSet ds )
		{
			DataTable table = ds.Tables[CContexteDonnee.GetNomTableForType(m_typeDonnees)];
			ArrayList listeRelationsAutoReference = new ArrayList();
			foreach ( DataRelation relation in table.ParentRelations )
			{
				if ( relation.ParentTable == table &&
					relation.ChildTable == table )
				{
					listeRelationsAutoReference.Add ( relation );
					if ( relation.ParentColumns.Length != 1 ||
						relation.ChildColumns.Length != 1 )
					throw new Exception(I.T("Auto referred tables on several keys aren't allowed|102"));
				}
			}
			if ( listeRelationsAutoReference.Count == 0 )
				return m_adapter.Update ( ds );
			if ( table == null )
				return 0;
			int nNb = 0;

			//Trie les éléments pour modifs et ajouts
			//Il faut modifier en premier les lignes qui sont dépendantes des autres
			/*Pour trier : On regarde  les fils de chaque ligne à modifier. On place
			 * Chaque ligne après ses fils dans la liste. A la fin, on inverse la liste
			 * */
			ArrayList listeToAddOrUpdate = new ArrayList();
			CArbreDependance arbreTotal = new CArbreDependance(null);
			Hashtable tableRowToArbre = new Hashtable();
			foreach ( DataRow row in table.Rows )
			{
				if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
				{
					foreach (DataRelation relation in listeRelationsAutoReference)
					{
                        CArbreDependance arbre = tableRowToArbre[row] as CArbreDependance;
                        if ( arbre == null )
						    arbre = new CArbreDependance(row);
						tableRowToArbre[row] = arbre;

						//Cherche l'arbreParent;
						DataRow rowParente = row.GetParentRow(relation);
						CArbreDependance arbreParent = arbreTotal;
						if (rowParente != null)
						{
							arbreParent = (CArbreDependance)tableRowToArbre[rowParente];
							if (arbreParent == null)
								arbreParent = arbreTotal;
						}
						arbre.ArbreParent = arbreParent;
						foreach (DataRow rowFille in row.GetChildRows(relation))
						{
							CArbreDependance arbreFille = (CArbreDependance)tableRowToArbre[rowFille];
                            if (arbreFille != null)
                            {
                                arbreFille.ArbreParent = arbre;
                            }
						}
					}
				}
			}
			arbreTotal.InsertFillesIntoArrayList(listeToAddOrUpdate);
			
			foreach ( DataRow row in listeToAddOrUpdate )
			{
				string strLastId = row[0].ToString();
				IDbCommand command = null;
				if ( row.RowState == DataRowState.Added )
					command = InsertCommand;
				if ( row.RowState == DataRowState.Modified )
					command = UpdateCommand;
				if ( command != null )
				{
					bool bForceOpen = false;
					if ( command.Connection.State != ConnectionState.Open )
					{
						bForceOpen = true;
						command.Connection.Open();
					}
					int nUpdateOrAdd = 0;
                    if (row.RowState == DataRowState.Added)
                    {
                        if (command.CommandText.ToUpper().Contains("INSERT"))
                            nUpdateOrAdd = ExecuteAdd(command, row);
                        else
                            nUpdateOrAdd++;
                    }
                    else
                    {
                        if (command.CommandText.ToUpper().Contains("UPDATE"))
                            nUpdateOrAdd = ExecuteUpdate(command, row);
                        else
                            nUpdateOrAdd++;
                    }
					
					if (nUpdateOrAdd != 1)
                    {
                        StringBuilder bl = new StringBuilder();
                        bl.Append(CObjetDonnee.GetMessageAccesConccurentiel(row));
                            //I.T("Another program has modified the data.|103"));
                        bl.Append("\r\n");
                        if ( row.HasVersion (DataRowVersion.Original) )
                        {
                            
                            foreach ( DataColumn col in row.Table.Columns )
                            {
                                try
                                {
                                    bl.Append ( row[col,DataRowVersion.Original].ToString() );
                                    bl.Append(" / ");
                                    bl.Append ( row[col].ToString());
                                    bl.Append("\r\n");
                                }
                                catch
                                {
                                }
                            }
                        }
						throw new Exception(bl.ToString());
                    }
					nNb += nUpdateOrAdd;
					if ( bForceOpen )
						command.Connection.Close();
				}
			}

			//traite les suppression
			//Il faut supprimer en premier les lignes qui ne sont pas dépendantes des autres
			/*Pour trier : On regarde pour les fils de chaque ligne à supprimer. On place
			 * Chaque ligne après ses fils dans la liste
			 * */
            /*Stef 25/03 : changement de l'algo : utilisation d'un arbre
             * */
			ArrayList listeToDelete = new ArrayList();
            arbreTotal = new CArbreDependance(null);
            tableRowToArbre = new Hashtable();
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    foreach (DataRelation relation in listeRelationsAutoReference)
                    {
                        CArbreDependance arbre = tableRowToArbre[row] as CArbreDependance;
                        if (arbre == null)
                            arbre = new CArbreDependance(row);
                        tableRowToArbre[row] = arbre;

                        //Cherche l'arbreParent;
                        DataRow rowParente = row.GetParentRow(relation, DataRowVersion.Original);
                        CArbreDependance arbreParent = arbreTotal;
                        if (rowParente != null)
                        {
                            arbreParent = (CArbreDependance)tableRowToArbre[rowParente];
                            if (arbreParent == null)
                                arbreParent = arbreTotal;
                        }
                        arbre.ArbreParent = arbreParent;
                        foreach (DataRow rowFille in row.GetChildRows(relation, DataRowVersion.Original))
                        {
                            CArbreDependance arbreFille = (CArbreDependance)tableRowToArbre[rowFille];
                            if (arbreFille != null)
                            {
                                arbreFille.ArbreParent = arbre;
                            }
                        }
                    }
                }
            }
            arbreTotal.InsertFillesIntoArrayList(listeToDelete);
            listeToDelete.Reverse();
			/*foreach ( DataRow row in table.Rows )
			{
				if ( row.RowState == DataRowState.Deleted )
				{
					Hashtable tableFilles = new Hashtable();
					foreach ( DataRelation relation in listeRelationsAutoReference )
					{
						DataRow[] childs = table.Select ( relation.ChildColumns[0]+"="+
							row[relation.ParentColumns[0], DataRowVersion.Original], null, DataViewRowState.Deleted );
						foreach ( DataRow rowFille in childs )
							tableFilles[rowFille] = true;
					}
					int nPosInsert = 0;
					for ( int nLook = 0; nLook < listeToDelete.Count && tableFilles.Count > 0; nLook++ )
					{
						if ( tableFilles[listeToDelete[nLook]] != null )
						{
							nPosInsert = nLook+1;
							tableFilles.Remove(listeToDelete[nLook]);
						}
					}
					listeToDelete.Insert ( nPosInsert, row );
				}
			}*/
			IDbCommand deleteCommand = DeleteCommand;
            if (deleteCommand.CommandText.ToUpper().Contains("DELETE"))
            {
                foreach (DataRow row in listeToDelete)
                {
                    if ( row.RowState == DataRowState.Deleted )
                        nNb += ExecuteDelete(deleteCommand, row);
                }
            }
            else
                nNb += listeToDelete.Count;
			return nNb;
		}
	}
}
