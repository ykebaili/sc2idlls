using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Data;
using System.Reflection;

using sc2i.common;
using sc2i.expression;
using sc2i.multitiers.client;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iStructureExport.
	/// </summary>
	public interface I2iExporteurSurServeur
	{
		//le result du data contient le dataset de retour
		CResultAErreur ExportData ( 
			string strChaineSerializationListeObjetDonnee, 
			C2iStructureExport structure,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariableDynamiquesPourFiltre,
			IIndicateurProgression indicateur);
	}

	[Serializable]
	public class C2iStructureExport : I2iSerializable
	{
		public const string c_idFichier = "2I_EXPORT";

		private const string c_strPrefixeChampCustom = "CF_";

		private Type m_typeSource = typeof(CObjetDonneeAIdNumerique);
		private ArrayList m_listeTablesAutonomes = new ArrayList();

		[NonSerialized]
		private CCacheValeursProprietes m_cacheValeurs = null;

		private ITableExport m_table;
		private bool m_bTraitementSurServeur = true;

		
		
		//Si c'est une structure complexe, il peut y avoir des champs calculés
		private bool m_bIsStructureComplexe = true;
		//----------------------------------------------------------------------------------
		public C2iStructureExport()
		{
			m_table = new C2iTableExport();
		}

		//----------------------------------------------------------------------------------
		public Type TypeSource
		{
			get
			{
				return m_typeSource;
			}
            set
            {
                m_typeSource = value;
                if (m_typeSource != null)
                {
                    object[] attribs = m_typeSource.GetCustomAttributes(typeof(DynamicClassAttribute), false);
                    if (attribs != null && attribs.Length == 1)
                        if (m_table.NomTable == "")
                            m_table.NomTable = ((DynamicClassAttribute)attribs[0]).NomConvivial;
                }
            }
		}

		/// //////////////////////////////////////////////////////////////
		///Retourne la liste de toutes les definition propriete dynamique origines des
		///champs de cette structure
		public string[] GetDependancesDeTableSimple( CContexteDonnee contexteDonnee )
		{
			if ( Table != null )
			{
				Hashtable tableOrigines = new Hashtable();
                GetDependancesDeTableSimple(Table, tableOrigines, "");
				//Table.AddProprietesOrigineDesChampsToTable ( tableOrigines, "", contexteDonnee );
				string[] defs = new string[tableOrigines.Count];
				int nDef = 0;
				foreach ( string strDef in tableOrigines.Keys )
				{
					defs[nDef] = strDef;
					nDef++;
				}
				return defs;
			}
			else
				return new string[0];
		}

        //----------------------------------------------------------------------------------
        private void GetDependancesDeTableSimple ( ITableExport table, Hashtable tableOrigines, string strChemin )
        {
            foreach ( ITableExport tableFille in table.TablesFilles )
            {
                CDefinitionProprieteDynamique def = tableFille.ChampOrigine;
                if ( def!=null )
                {
                    string strChamp = strChemin;
                    if ( strChamp.Length > 0 )
                        strChamp += ".";
                    strChamp += def.NomPropriete;
                    tableOrigines[strChamp] = true;
                    GetDependancesDeTableSimple ( tableFille, tableOrigines, strChamp );
                }
            }
        }


	
		//----------------------------------------------------------------------------------
		//Indique si l'export se passe de préférence sur le serveur
		public bool TraiterSurServeur
		{
			get
			{
				return m_bTraitementSurServeur;
			}
			set
			{
				m_bTraitementSurServeur = value;
			}
		}

		//----------------------------------------------------------------------------------
		public ITableExport Table
		{
			get
			{
				return m_table;
			}
			set
			{
				m_table = value;
			}
		}

		//----------------------------------------------------------------------------------
		public C2iTableExportCalculee[] TablesCalculees
		{
			get
			{
				return (C2iTableExportCalculee[])m_listeTablesAutonomes.ToArray ( typeof(C2iTableExportCalculee) );
			}
		}

		//----------------------------------------------------------------------------------
		public void AddTableCalculee ( C2iTableExportCalculee table )
		{
			m_listeTablesAutonomes.Add ( table );
		}

		//----------------------------------------------------------------------------------
		public void RemoveTableCalculee ( C2iTableExportCalculee table )
		{
			m_listeTablesAutonomes.Remove ( table );
		}
	
		//----------------------------------------------------------------------------------
		private int GetNumVersion()
		{
			//Version 2 : Ajout du flag de traitement sur serveur ou sur client
			//Version 3 : Ajout de la liste de tables autonomes
			return 5;
			//Version 4 : Renomme les tables d'export simple par leur nom systèmes
			//Version 5 : la table principale n'est plus forcement une C2iTableExport, c'est
			//			  maintenant une ITableExport
		}

		private void RenommeNomTablesSimples()
		{
			if( !m_bIsStructureComplexe && Table != null)
			{
				string strNom = CContexteDonnee.GetNomTableForType ( m_typeSource );
				if ( strNom != null && strNom != "" )
					Table.NomTable = strNom;
				ArrayList lstTables = new ArrayList();
				lstTables.AddRange ( Table.GetToutesLesTablesFilles() );
				foreach ( ITableExport table in lstTables )
				{
					if ( table is C2iTableExport && table.ChampOrigine != null )
					{
						strNom = CContexteDonnee.GetNomTableForType ( table.ChampOrigine.TypeDonnee.TypeDotNetNatif );
						if ( strNom != null && strNom != "" )
							table.NomTable = strNom;
					}
				}
			}
		}


		//----------------------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteType ( ref m_typeSource );


			if ( serializer.Mode == ModeSerialisation.Ecriture && !m_bIsStructureComplexe)
				RenommeNomTablesSimples();

			I2iSerializable obj = m_table;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_table = (ITableExport)obj;
            if (m_table != null)
                m_table.TypeSource = TypeSource;

			if ( nVersion > 0 )
				serializer.TraiteBool ( ref m_bIsStructureComplexe );
			else
				m_bIsStructureComplexe = true;
			
			if ( nVersion > 1 )
				serializer.TraiteBool ( ref m_bTraitementSurServeur );
			else
				m_bTraitementSurServeur = true;

			if ( nVersion > 2 )
				result = serializer.TraiteArrayListOf2iSerializable ( m_listeTablesAutonomes );

			if ( nVersion < 4 && serializer.Mode == ModeSerialisation.Lecture )
				RenommeNomTablesSimples();


			return result;
		}

		/*//----------------------------------------------------------------------------------
		public CResultAErreur Export (int nIdSession, IEnumerable list, ref DataSet ds)
		{
			return Export ( nIdSession, list, ref ds, null );
		}*/
			
		//----------------------------------------------------------------------------------
		public CResultAErreur Export (int nIdSession, 
            IEnumerable list, 
            ref DataSet ds, 
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesPourFiltre )
		{
			return Export ( nIdSession, list, ref ds, elementAVariablesPourFiltre, null );
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur Export (int nIdSession, 
            IEnumerable list, 
            ref DataSet ds, 
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesPourFiltre, 
            IIndicateurProgression indicateur )
		{
			if ( list is CListeObjetsDonnees && m_bTraitementSurServeur )
			{
				CResultAErreur result = CResultAErreur.True;
				CStringSerializer serializer = new CStringSerializer ( ModeSerialisation.Ecriture );
				I2iSerializable listeToSerialize = (I2iSerializable)list;
				result = serializer.TraiteObject ( ref listeToSerialize );
				if ( !result )
					return result;
				I2iExporteurSurServeur exporteur = ( I2iExporteurSurServeur )C2iFactory.GetNewObjetForSession ( "C2iExporteurSurServeur", typeof(I2iExporteurSurServeur), nIdSession );
				
				try
				{
					result = exporteur.ExportData ( serializer.String, this, elementAVariablesPourFiltre, indicateur );
					if ( result )
						ds = (DataSet)result.Data;
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
					result.EmpileErreur(I.T("Error during the export on the server|101"));
				}
				return result;

			}
			if ( IsStructureComplexe )
				return ExportComplexe ( nIdSession, list, ref ds, elementAVariablesPourFiltre, indicateur );
			else
				return ExportSimple ( nIdSession, list, ref ds, indicateur );
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur ExportSimple ( int nIdSession, IEnumerable list, ref DataSet ds, IIndicateurProgression indicateur )
		{
			if (m_cacheValeurs == null)
				m_cacheValeurs = new CCacheValeursProprietes();
			DateTime dt = DateTime.Now;
			CResultAErreur result = CResultAErreur.True;
			if ( list != null && !(list is CListeObjetsDonnees ) )
			{
				result.EmpileErreur(I.T("Impossible to use a simple export with something other than a Data Object list|102"));
				return result;
			}
			CListeObjetsDonnees listeObjets = (CListeObjetsDonnees)list;
			CContexteDonnee contexteDestination = new CContexteDonnee ( nIdSession, true, false );
			result = contexteDestination.SetVersionDeTravail(listeObjets.ContexteDonnee.IdVersionDeTravail, false);
			if (!result)
				return result;
			contexteDestination.EnforceConstraints = false;//Ca gagne un temps fou !!!
			ds = contexteDestination;

			//Crée la table principale
			DataTable tablePrincipale = contexteDestination.GetTableSafe ( CContexteDonnee.GetNomTableForType(m_typeSource) );
			if ( tablePrincipale == null )
			{
				result.EmpileErreur(I.T("Error during the creation of the main table|103"));
				return result;
			}

			if ( listeObjets != null )
			{
				//Exporte les objets dans la table principale
				foreach ( CObjetDonnee objet in listeObjets )
				{
					tablePrincipale.ImportRow ( objet.Row.Row );
				}
			}
			

			foreach ( C2iChampExport champ in Table.Champs )
				//Crée les colonnes calculées et champs custom
			{
				result = CreateChampInTable ( champ, tablePrincipale );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error during field @1 creation|104",champ.NomChamp));
					return result;
				}
			}
			//Crée les autres tables
			foreach ( C2iTableExport tableFille in Table.TablesFilles )
				CreateTableSimpleInDataset ( tableFille, contexteDestination, m_typeSource );
			
			foreach ( DataTable table in contexteDestination.Tables )
			{
				ArrayList lstColsToDelete = new ArrayList();
				foreach ( DataColumn col in table.Columns )
					if ( col.DataType == typeof(CDonneeBinaireInRow) )
						lstColsToDelete.Add ( col );
				foreach ( DataColumn col in lstColsToDelete )
					table.Columns.Remove ( col );
			}

			if ( listeObjets == null )
				AddTablesCalculees(contexteDestination, null);

			if ( listeObjets == null )
				return result;


			CListeObjetsDonnees listeDestination = new CListeObjetsDonnees ( contexteDestination, m_typeSource, false );
			listeDestination.InterditLectureInDB = true;


			//Lit les dépendances dans le contexte
			//Récupère toutes les sous tables nécéssaires
			string[] definitionsOrigines = GetDependancesDeTableSimple(listeObjets.ContexteDonnee);
			Hashtable tableDependances = new Hashtable();
			foreach ( string def in definitionsOrigines )
			{
				tableDependances[def] = true;
			}
			string[] strDependances = new string[tableDependances.Count];
			int nDependance = 0;
			foreach ( string strDependance in tableDependances.Keys )
			{
				strDependances[nDependance] = strDependance;
				nDependance++;
			}

			listeDestination.ReadDependances(strDependances);
			//Et voila, le tour est joué, toutes les données sont dans le dataset de destination

			//Il manque juste les compositions des éléments nouveaux
			if ( listeObjets != null )
			{
				//Exporte les compositions des objets nouveaux 
				foreach ( CObjetDonnee objet in listeObjets )
					if ( objet.IsNew() )//Si nouveau, charge directement toutes ses données
						objet.ContexteDonnee.CopieRowTo ( objet.Row.Row, contexteDestination, true, false, false);
			}
			//S'assure que toutes les données sont lues
			foreach ( DataTable table in contexteDestination.Tables )
			{
				DataView view = new DataView ( table );
				view.RowFilter = CContexteDonnee.c_colIsToRead + "=1";
				foreach ( DataRowView rowView in view )
					contexteDestination.ReadRow ( rowView.Row );
			}

			

			//Ramène les champs calculés
			List<ITableExport> toutesLesTables = ToutesLesTables();
			m_cacheValeurs.CacheEnabled = true;
			int nNbTotalPrincipal = tablePrincipale.Rows.Count;

			//Récupère les champs par ID
			//Id->Nom du champ
			Dictionary<int, CChampCustom> dicoChamps = new Dictionary<int,CChampCustom>();
			CListeObjetsDonnees listeChamps = new CListeObjetsDonnees ( contexteDestination, typeof(CChampCustom ) );
			foreach ( CChampCustom champ in listeChamps )
				dicoChamps[champ.Id] = champ;
			
			//Liste des tables à conserver au final
			Hashtable tableTablesAConserver = new Hashtable();
			foreach ( C2iTableExport table in toutesLesTables )
			{
				string strNomTable = "";
				DataTable tableDestination = tablePrincipale;
				if ( table.ChampOrigine != null )
				{
					strNomTable = CContexteDonnee.GetNomTableForType ( table.ChampOrigine.TypeDonnee.TypeDotNetNatif );
					tableDestination = contexteDestination.Tables[strNomTable];
				}
				if ( tableDestination != null )
				{
					tableTablesAConserver[tableDestination.TableName] = true;
					List<C2iChampExport> champsCalcules = new List<C2iChampExport>();
					List<C2iChampExport> champsCustoms = new List<C2iChampExport>();
					foreach (C2iChampExport champ in table.Champs)
					{
						if (champ.Origine is C2iOrigineChampExportExpression)
							champsCalcules.Add(champ);
						if (champ.Origine is C2iOrigineChampExportChampCustom)
							champsCustoms.Add(champ);
					}

					if (champsCalcules.Count != 0)
					{
						Type tp = CContexteDonnee.GetTypeForTable ( tableDestination.TableName );
						foreach ( DataRow row in tableDestination.Rows )
						{
#if PDA
							CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance ( tp );
							objet.SetRow ( row );
#else
							object objet = Activator.CreateInstance ( tp, new object[]{row} );
#endif
							foreach (C2iChampExport chp in champsCalcules)
							{
								if (chp.Origine is C2iOrigineChampExportExpression)
								{
									object valeur = chp.GetValeur(objet, m_cacheValeurs, null);
									if (valeur == null)
										valeur = DBNull.Value;
									row[chp.NomChamp] = valeur;
								}
							}
							m_cacheValeurs.ResetCache (  );
						}
					}
					if (champsCustoms.Count > 0)
					{
						Type tp = CContexteDonnee.GetTypeForTable(tableDestination.TableName);
						if (typeof(IObjetDonneeAChamps).IsAssignableFrom ( tp ))
						{
							IObjetDonneeAChamps element = (IObjetDonneeAChamps)Activator.CreateInstance(tp, contexteDestination);
							string strTableValeursChamps = element.GetNomTableRelationToChamps();
							Type tpValeursChamps = CContexteDonnee.GetTypeForTable(strTableValeursChamps);
										
							//Travaille les valeurs de champs customs par paquet de 500;
							int nTaillePaquet = 500;
							int nMax = tableDestination.Rows.Count;
							string strChampId = tableDestination.PrimaryKey[0].ColumnName;
							for (int nPaquet = 0; nPaquet < nMax; nPaquet += nTaillePaquet)
							{
								StringBuilder bl = new StringBuilder();
								int nFin = Math.Min(nMax, nPaquet + nTaillePaquet);
								for (int nRow = nPaquet; nRow < nFin; nRow++)
								{
									bl.Append(tableDestination.Rows[nRow][strChampId]);
									bl.Append(',');
								}
								if (bl.Length > 0)
								{
									bl.Remove(bl.Length - 1, 1);
									string strIds = bl.ToString();
									foreach (C2iChampExport champACustom in champsCustoms)
									{
										C2iOrigineChampExportChampCustom origineChamp = (C2iOrigineChampExportChampCustom)champACustom.Origine;

										//C'est un élément à champ, donc, va requeter dans les champs customs pour avoir les
										//valeurs
										CListeObjetsDonnees listeValeurs = new CListeObjetsDonnees(contexteDestination, tpValeursChamps);
										StringBuilder blIdsChamps = new StringBuilder();
										foreach (int nId in origineChamp.IdsChampCustom)
										{
											blIdsChamps.Append(nId);
											blIdsChamps.Append(',');
										}
										if (blIdsChamps.Length > 0)
										{
											blIdsChamps.Remove(blIdsChamps.Length - 1, 1);
											listeValeurs.Filtre = new CFiltreData(
												CChampCustom.c_champId + " in (" + blIdsChamps.ToString() + ") and " +
												strChampId + " in (" + strIds + ")");
											listeValeurs.AssureLectureFaite();
											//On n'a plus qu'à rebasculer les valeurs dans la table
											foreach (CRelationElementAChamp_ChampCustom valeur in listeValeurs)
											{
												try
												{
													DataRow rowAttachee = tableDestination.Rows.Find(valeur.Row[strChampId]);
													if (rowAttachee != null)
													{
														object valTmp = valeur.Valeur;
														if (valTmp == null)
															valTmp = DBNull.Value;
														rowAttachee[c_strPrefixeChampCustom + valeur.ChampCustom.Nom] = valTmp;
													}
												}
												catch
												{ }
											}
										}
									}
								}
							}
						}
					}
				}
			}
			m_cacheValeurs.ResetCache();
			ArrayList lstTablesToDelete = new ArrayList();
			foreach ( DataTable table in contexteDestination.Tables )
			{
				if ( tableTablesAConserver[table.TableName] == null )
					lstTablesToDelete.Add ( table );
				
			}
			foreach ( DataTable table in lstTablesToDelete )
			{
				contexteDestination.SupprimeTableEtContraintes ( table );
			}

			AddTablesCalculees(contexteDestination, null);
			
			return result;
		}


		//----------------------------------------------------------------------------------
		private void AddTablesCalculees(DataSet ds, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesPourFiltre)
		{
			//Crée les tables calculees
			foreach ( C2iTableExportCalculee tableCalculee in m_listeTablesAutonomes )
			{
				DataTable tableAuto = tableCalculee.GetDataTable(elementAVariablesPourFiltre);
				int nId = 1;
				string strSource = tableAuto.TableName;
				while ( ds.Tables[tableAuto.TableName] != null )
				{
					tableAuto.TableName = strSource + "_"+nId;
					nId++;
				}
				ds.Tables.Add ( tableAuto );
			}
		}


		//----------------------------------------------------------------------------------
		public CResultAErreur CreateTableSimpleInDataset(C2iTableExport tableExport, CContexteDonnee contexteDest, Type typeParent)
		{
			CResultAErreur result = CResultAErreur.True;
			PropertyInfo property = typeParent.GetProperty ( tableExport.ChampOrigine.NomProprieteSansCleTypeChamp );
			if ( property == null )
			{
				result.EmpileErreur(I.T("The property @1 was not found in the @2 type|105",tableExport.ChampOrigine.NomPropriete,typeParent.ToString()));
				return result;
			}
			Object[] attribs = property.GetCustomAttributes ( typeof(RelationAttribute), true );
			Type typeObjet = null;
			DataTable tableDestination = null;
			if ( attribs.Length != 0 )
			{
				RelationAttribute relParente = (RelationAttribute)attribs[0];
				tableDestination = contexteDest.GetTableSafe ( relParente.TableMere );
				typeObjet = CContexteDonnee.GetTypeForTable ( relParente.TableMere );
			}
			else
			{
				attribs = property.GetCustomAttributes ( typeof(RelationFilleAttribute), true );
				if ( attribs.Length != 0 )
				{
					RelationFilleAttribute relFille = (RelationFilleAttribute)attribs[0];
					tableDestination = contexteDest.GetTableSafe ( CContexteDonnee.GetNomTableForType(relFille.TypeFille) );
					typeObjet = relFille.TypeFille;
				}
				else
				{
					result.EmpileErreur(I.T("The property @1 cannot be integrated into a simple export|106",tableExport.ChampOrigine.NomPropriete));
					return result;
				}
			}
			


			foreach ( C2iChampExport champ in tableExport.Champs )
				//Crée les colonnes calculées
			{
				result = CreateChampInTable ( champ, tableDestination );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error during field @1 creation|104",champ.NomChamp));
					return result;
				}
			}
			foreach ( C2iTableExport tableFille in tableExport.TablesFilles )
			{
				result &= CreateTableSimpleInDataset ( tableFille, contexteDest, typeObjet );
				if ( !result )
					return result;
			}
			return result;
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur ExportComplexe ( 
			int nIdSession,
			IEnumerable list,
			ref DataSet ds,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesPourFiltres, 
			IIndicateurProgression indicateur)
		{
			if (m_cacheValeurs == null)
				m_cacheValeurs = new CCacheValeursProprietes();
			C2iSponsor sponsor = new C2iSponsor();
			sponsor.Register ( indicateur );
            sponsor.Register(elementAVariablesPourFiltres);
			CResultAErreur result = CResultAErreur.True;
			try
			{
				indicateur = CConteneurIndicateurProgression.GetConteneur(indicateur);
				DateTime dt = DateTime.Now;


				//... Création des tables et des champs ...
				bool bIsOptimisable = (list is CListeObjetsDonnees &&
					typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(((CListeObjetsDonnees)list).TypeObjets))
					|| list == null;
				result = CreateTableComplexeInDataset(bIsOptimisable, this.TypeSource, this.m_table, ds, null);
				if (!result)
					return result;
				AddTablesCalculees(ds, elementAVariablesPourFiltres);
				if (list == null)
					return result;
				m_cacheValeurs.CacheEnabled = true;
				//ds.EnforceConstraints = false;
				try
				{
					CInterpreteurProprieteDynamique.AssociationsSupplementaires.AssocieObjet(null, elementAVariablesPourFiltres);
					CFournisseurGeneriqueProprietesDynamiques.AssocieObjetSupplementaire(null, new CObjetPourSousProprietes(elementAVariablesPourFiltres));
					result = m_table.InsertDataInDataSet(list, ds, null, 0, elementAVariablesPourFiltres, m_cacheValeurs, null, true, CConteneurIndicateurProgression.GetConteneur(indicateur));
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
				}
				finally
				{
					CInterpreteurProprieteDynamique.AssociationsSupplementaires.DissocieObjet(null, elementAVariablesPourFiltres);
					CFournisseurGeneriqueProprietesDynamiques.DissocieObjetSupplementaire(null, new CObjetPourSousProprietes(elementAVariablesPourFiltres));
				}
				//ds.EnforceConstraints = true;
				m_cacheValeurs.ResetCache();
				m_cacheValeurs.CacheEnabled = false;
				if (!result)
					return result;
				result = m_table.EndInsertData(ds);
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			finally
			{
				sponsor.Unregister(indicateur);
                sponsor.Unregister(elementAVariablesPourFiltres);
				sponsor.Dispose();
			}
			return result;
		}

		

		//----------------------------------------------------------------------------------
		public CResultAErreur CreateTableComplexeInDataset(bool bAvecOptim, Type typeSource, ITableExport tableExport, DataSet ds, DataColumn colParente)
		{
			CResultAErreur result = CResultAErreur.True;

			if (ds.Tables.Contains(tableExport.NomTable))
				return result;

			DataTable table = new DataTable(tableExport.NomTable);

			DataColumn colKey = CreateChampInTableAIdAuto(table, 1);

			tableExport.InsertColonnesInTable ( table );
			
			ds.Tables.Add(table);


			foreach(ITableExport tbl in tableExport.TablesFilles)
			{
				bool bAvecOptimForTable = bAvecOptim && tableExport.IsOptimisable(tbl, typeSource);
                if(tbl.ChampOrigine != null)
				    result = CreateTableComplexeInDataset(bAvecOptimForTable, tbl.ChampOrigine.TypeDonnee.TypeDotNetNatif, tbl, ds, colKey);
                else
                    result = CreateTableComplexeInDataset(bAvecOptimForTable, null, tbl, ds, colKey);

				if (!result)
					return result;
				//Gestion des relations filles et relations parentes
				if (tbl.ChampOrigine != null && (tbl.ChampOrigine.TypeDonnee.IsArrayOfTypeNatif || !bAvecOptimForTable ||
					tbl.ChampOrigine is CDefinitionProprieteDynamiqueThis))
					CreateForeignKeyInTable(table.PrimaryKey[0], ds.Tables[tbl.NomTable], 1 );
				else
					CreateForeignKeyInTable(ds.Tables[tbl.NomTable].PrimaryKey[0], table, 1 );
			}

			return result;
		}
		//----------------------------------------------------------------------------------
		public static DataColumn CreateForeignKeyInTable(DataColumn colParente, DataTable tableFille, int nIdKey)
		{
			if ( !tableFille.Columns.Contains("FK_"+ colParente.ColumnName + nIdKey.ToString()) )
			{
				if (colParente==null)
					return null;
				DataColumn col = new DataColumn("FK_"+ colParente.ColumnName + nIdKey.ToString(), typeof(int));
				tableFille.Columns.Add(col);
				DataRelation rel = CreateRelationInDataset(tableFille.DataSet,colParente,col,1);
				tableFille.DataSet.Relations.Add(rel);
				return col;
			}
			else
				return CreateForeignKeyInTable(colParente,tableFille, nIdKey+1);
  		}
		
		//----------------------------------------------------------------------------------
		private static DataRelation CreateRelationInDataset(DataSet ds, DataColumn colParente, DataColumn colFille, int nIdKey)
		{
			if ( !ds.Relations.Contains("REL_" + colParente.Table.TableName + "_" + colFille.Table.TableName + nIdKey.ToString()) )
			{
				DataRelation rel = new DataRelation("REL_" + colParente.Table.TableName + "_" + colFille.Table.TableName + nIdKey.ToString(), colParente, colFille);
				return rel;
			}
			else
				return CreateRelationInDataset(ds,colParente,colFille, nIdKey+1);
		}

		//----------------------------------------------------------------------------------
		public static DataColumn CreateChampInTableAIdAuto(DataTable table, int nIdKey)
		{
			if ( !table.Columns.Contains("Key_"+table.TableName + nIdKey.ToString()) )
			{
				DataColumn col = new DataColumn("Key_"+table.TableName + nIdKey.ToString(), typeof(int) );
				col.AutoIncrement = true;
				col.ReadOnly = true;
				table.Columns.Add(col);
				table.Constraints.Add(col.ColumnName, col, true);
				return col;
			}
			else
				return CreateChampInTableAIdAuto(table, nIdKey+1);
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;

			if (champExport is C2iChampExport && ((C2iChampExport)champExport).Origine is C2iOrigineChampExportChampCustom)
			{
				C2iOrigineChampExportChampCustom origineCustom = (C2iOrigineChampExportChampCustom)((C2iChampExport)champExport).Origine;
				//Récupère les noms et les types des champs custom
				CContexteDonnee ctx = null;
				if (table.DataSet is CContexteDonnee)
					ctx = (CContexteDonnee)table.DataSet;
				else
					ctx = new CContexteDonnee(0, true, false);
				//Lit les champs custom
				CListeObjetsDonnees listeChamps = new CListeObjetsDonnees(ctx, typeof(CChampCustom));
				listeChamps.AssureLectureFaite();

				foreach (int nIdChamp in origineCustom.IdsChampCustom)
				{
					CChampCustom champ = new CChampCustom(ctx);
					if (champ.ReadIfExists(nIdChamp))
					{
						if (champ.TypeDonneeChamp.TypeDonnee != TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
						{
							DataColumn colChamp = new DataColumn(c_strPrefixeChampCustom + champ.Nom, champ.TypeDonnee.TypeDotNetNatif);
							colChamp.AllowDBNull = true;
							table.Columns.Add(colChamp);
						}
					}
				}
				return result;
			}

			if (table.Columns.Contains(champExport.NomChamp))
				return result;
			DataColumn col = new DataColumn(champExport.NomChamp, champExport.TypeDonnee);
			table.Columns.Add(col);
			return result;
		}

		//----------------------------------------------------------------------------------
		public List<ITableExport> ToutesLesTables()
		{
			List<ITableExport> lst = new List<ITableExport>(Table.GetToutesLesTablesFilles());
			if ( m_table != null )
				lst.Add(m_table);
			return lst;
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			Hashtable tableNoms = new Hashtable();
			if ( IsStructureComplexe )
			{
				foreach( ITableExport table in ToutesLesTables() )
				{
					if ( !tableNoms.ContainsKey( table.NomTable ))
						tableNoms[table.NomTable] = true;
					else
					{
						if ( table.ChampOrigine != null )
						{
							table.NomTable = table.ChampOrigine.Nom;
						}
						if ( !tableNoms.ContainsKey( table.NomTable ))
							tableNoms[table.NomTable] = true;
						else
						{
							result.EmpileErreur(I.T("There are several tables having the name \"@1\"|108", table.NomTable));
							return result;
						}
					}
				}
			}
			
			return this.Table.VerifieDonnees();
		}

		//----------------------------------------------------------------------------------
		public bool IsStructureComplexe
		{
			get
			{
				return m_bIsStructureComplexe;
			}
			set
			{
				m_bIsStructureComplexe = value;
			}
		}


		//----------------------------------------------------------------------------------
		public override bool Equals ( object obj )
		{
			if ( obj == null )
				return false;
			if ( !(obj is C2iStructureExport) )
				return false;
			CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture );
			if ( Serialize ( ser ) )
			{
				string strThis = ser.String;
				ser = new CStringSerializer ( ModeSerialisation.Ecriture );
				if ( ((C2iStructureExport)obj).Serialize ( ser ) )
				{
					if ( strThis.Equals ( ser.String ) )
						return true;
				}
			}
			return false;
		}

		//----------------------------------------------------------------------------------
		public override int GetHashCode()
		{
			CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture );
			if ( Serialize ( ser ) )
			{
				return ser.String.GetHashCode();
			}
			return m_table.GetHashCode();
		}


		//----------------------------------------------------------------------------------
		public CResultAErreur ReadFromFile ( string strNomFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			FileStream stream = null;
			try
			{
				stream = new FileStream ( strNomFichier, FileMode.Open, FileAccess.Read, FileShare.Read );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while opening file|109"));
			}
			try
			{
				BinaryReader reader = new BinaryReader ( stream );
				string strId = reader.ReadString();
				if ( strId != c_idFichier )
				{
					result.EmpileErreur(I.T("The file doesn't contain a valid structure|111"));
					return result;
				}
				CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
				result = Serialize ( serializer );
                reader.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while reading the file|110"));
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch{}
			}
			return result;
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur SaveToFile ( string strNomFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			FileStream stream = null;
			try
			{
				stream = new FileStream ( strNomFichier, FileMode.Create, FileAccess.Write, FileShare.None);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while opening the file|109"));
			}
			try
			{
				BinaryWriter writer = new BinaryWriter ( stream );
				writer.Write ( c_idFichier );
				CSerializerSaveBinaire serializer = new CSerializerSaveBinaire ( writer );
				result = Serialize ( serializer );
                writer.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while writing the file|112"));
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch{}
			}
			return result;
		}
	}

	[Serializable]
	public class CStructureExportAvecFiltre : IDefinitionJeuDonnees
	{
		private C2iStructureExport m_structure = null;
		private CFiltreDynamique m_filtre = null;

		public CStructureExportAvecFiltre()
		{
		}

		/// //////////////////////////////////////////
		public C2iStructureExport Structure
		{
			get
			{
				return m_structure;
			}
			set
			{
				m_structure = value;
			}
		}

		/// /////////////////////////////////////////
		public CFiltreDynamique Filtre
		{
			get
			{
				return m_filtre;
			}
			set
			{
				m_filtre = value;
			}
		}
				
		/// /////////////////////////////////////////
		public string LibelleTypeDefinitionJeuDonnee
		{
			get
			{
				string strLibelle = "Structure ";
				if ( m_structure != null && m_structure.TypeSource != null )
					strLibelle += DynamicClassAttribute.GetNomConvivial ( m_structure.TypeSource );
				return strLibelle;
			}
		}

		/// /////////////////////////////////////////
		public CResultAErreur GetDonnees ( 
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, 
			CListeObjetsDonnees listeDonnees,
			IIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			CListeObjetsDonnees liste = listeDonnees;
			if ( (liste == null || liste.TypeObjets != Structure.TypeSource) && Structure.TypeSource != null)
			{
				liste = new CListeObjetsDonnees ( elementAVariables.ContexteDonnee, m_structure.TypeSource );
				if ( m_filtre != null )
				{
					m_filtre.ElementAVariablesExterne = elementAVariables;
					result = m_filtre.GetFiltreData();
					if ( !result )
						return result;
					liste.Filtre = (CFiltreData)result.Data;
				}
			}
			if (Structure.TypeSource == null)
				liste = null;

			DataSet ds = new DataSet();
			int nIdSession = -1;
			if ( elementAVariables != null && elementAVariables.ContexteDonnee != null )
				nIdSession = elementAVariables.ContexteDonnee.IdSession;
			else
				nIdSession = CSessionClient.GetSessionUnique().IdSession;
			result = m_structure.Export ( nIdSession, liste, ref ds, elementAVariables, indicateur );
			if ( !result )
				return result;
			result.Data = ds;
			return result;
		}


		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}


		/// /////////////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			I2iSerializable objet = m_structure;
			result = serializer.TraiteObject ( ref objet );
			m_structure = (C2iStructureExport)objet;

			objet = m_filtre;
			result = serializer.TraiteObject ( ref objet );
			m_filtre = (CFiltreDynamique)objet;

			return result;
		}

		/// //////////////////////////////////////////
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				if ( m_filtre != null )
					return m_filtre.ContexteDonnee;
				return null;
			}
			set
			{
				if ( m_filtre != null )
					m_filtre.ContexteDonnee = value;
			}
		}

		/// //////////////////////////////////////////
		public IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesExterne
		{
			get
			{
				if ( m_filtre != null )
					return m_filtre.ElementAVariablesExterne;
				return null;
			}
			set
			{
				if ( m_filtre != null )
					m_filtre.ElementAVariablesExterne = value;
			}
		}

		/// /////////////////////////////////////////////
		public Type TypeDonneesEntree
		{
			get
			{
				if ( m_structure != null )
					return m_structure.TypeSource;
				return null;
			}
		}

	}
}
