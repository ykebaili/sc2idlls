using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data.serveur
{
	public abstract class C2iDataBaseCreator : IDataBaseCreator
	{
		public C2iDataBaseCreator()
		{
		}

        /// <summary>
        /// Indique que le créateur sait gérer les indexs cluster
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaitGererLesIndexCluster()
        {
            return false;
        }



		//Outils
		public abstract IDatabaseConnexion Connection { get;}
		public abstract IDataBaseTypeMapper DataBaseTypesMappeur { get;}
		public abstract int NbTableInitialisation { get;}

		public abstract CResultAErreur InitialiserDataBase();

        //--------------------------------------------------------------------------------------
        //Retourne true si les chaines de tailles supérieures à la taille
        //Max d'une chaine de caractères sont automatiquement convertis en LongString
        public virtual bool AutoAdaptLongString
        {
            get
            {
                return false;
            }
        }

		//--------------------------------------------------------------------------------------
		public static bool IsGoodDataColumnType(DataColumn col, Type tp)
		{
			if (col.DataType == tp)
				return true;
			else if (col.AllowDBNull)
			{
				if ((col.DataType == typeof(Int32) && tp == typeof(Int32?))
					|| (col.DataType == typeof(bool) && tp == typeof(bool?))
					|| (col.DataType == typeof(double) && tp == typeof(double?))
					|| (col.DataType == typeof(DateTime) && tp == typeof(DateTime?))
                    || (col.DataType == typeof(DateTime) && tp == typeof(CDateTimeEx)))
					return true;
                if (tp == typeof(CDonneeBinaireInRow) && col.DataType == typeof(byte[]))
                    return true;

			}
			else if (col.DataType == typeof(Int32) && tp.IsEnum)
				return true;
            else if (tp == typeof(CDonneeBinaireInRow) && col.DataType == typeof(byte[]))
                return true;

			return false;
		}

        /// <summary>
        /// déclenché après création / suppression des champs d'une table
        /// </summary>
        /// <param name="strTable"></param>
        protected virtual void AfterChangeChamps(string strNomTableInDb)
        {
        }

        /// <summary>
        /// déclenché après modification des contraintes d'une table
        /// </summary>
        /// <param name="strNomTableInDb"></param>
        protected virtual void AfterChangeContraintes(string strNomTableInDb)
        {
        }

        /// <summary>
        /// Déclenché après changement d'indexs d'une table
        /// </summary>
        /// <param name="strTable"></param>
        protected virtual void AfterChangeIndexs(string strNomTableInDb)
        {
        }


		#region Operations sur la structure
		//DataBase
		virtual public CResultAErreur CreateDatabase()
		{
			return CResultAErreur.True;
		}

		#region Tables
		virtual public CResultAErreur CreateTable(CStructureTable structure)
		{
			CResultAErreur result = CResultAErreur.True;

			//Verification des noms
			result = CheckNomsStructureTable(structure);

			//Creation de la table
			result = result ? Connection.RunStatement(GetRequeteCreateTable(structure)) : result;
            AfterChangeChamps(structure.NomTableInDb);
				
			//Création des indexs
			result = result ? CreateTable_Indexs(structure) : result;

			//Creation Clé primaire
			result = result ? CreateTable_ClePrimaire(structure) : result;

			//Creation des contraintes de clés étrangères
			result = result ? CreateTable_ClesEtrangeres(structure) : result;

			if (!result)
				result.EmpileErreur(I.T("Error while creating table @1|125", structure.NomTableInDb));

			return result;
		}
		protected virtual CResultAErreur CreateTable_Indexs(CStructureTable structure)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (CInfoChampTable info in structure.Champs)
				if (info.IsIndex)
				{
					CResultAErreur resultCreateIdx = CreateIndex(structure.NomTableInDb, false, info.NomChamp);					
					if (!resultCreateIdx)
					{
						result.Erreur += resultCreateIdx.Erreur;
						result.Result = false;
					}
				}
			return result;
		}
		protected virtual CResultAErreur CreateTable_ClePrimaire(CStructureTable structure)
		{
			ArrayList lstChampsId = new ArrayList();
			foreach (CInfoChampTable champId in structure.ChampsId)
				lstChampsId.Add(champId.NomChamp);
			CResultAErreur result = Connection.RunStatement(GetRequeteCreateClefPrimaire(structure.NomTableInDb, (string[])lstChampsId.ToArray(typeof(string))));
            if (!result)
                result.EmpileErreur(I.T("Error while creating primary key of table @1|126", structure.NomTableInDb));
            else
                AfterChangeContraintes(structure.NomTable);
			return result;
		}
		protected virtual CResultAErreur CreateTable_ClesEtrangeres(CStructureTable structure)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (CInfoRelation rel in structure.RelationsParentes)
			{
                if (rel.IsInDb)
                {
                    string strRequeteRelation = GetRequeteCreateCleEtrangere(rel);
                    string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
                    result = Connection.RunStatement(strRequeteRelation);
                    if (result)
                        AfterChangeContraintes(strNomTableFilleInDb);
                    if (!result)
                        result.EmpileErreur(I.T("Error while creating foreign key of @1 (@2)|127", structure.NomTableInDb, strRequeteRelation));
                    else if (rel.Indexed && !IndexExists(strNomTableFilleInDb, rel.ChampsFille))
                    {
                        result = Connection.RunStatement(GetRequeteCreateIndex(strNomTableFilleInDb, rel.IsClustered, rel.ChampsFille));
                        if (!result)
                            result.EmpileErreur(I.T("Error while creating index in table @1|128", structure.NomTableInDb));
                        if (result)
                            AfterChangeIndexs(strNomTableFilleInDb);
                    }
                }
			}
			return result;
		}

		virtual public CResultAErreur CreationOuUpdateTableFromType(Type tp)
		{
			return CreationOuUpdateTableFromType(tp,new ArrayList());
		}
		virtual public CResultAErreur CreationOuUpdateTableFromType(Type tp, ArrayList strChampsAutorisesANull)
		{
			CResultAErreur result = CResultAErreur.True;

            

			CStructureTable structure = CStructureTable.GetStructure(tp);

            object objServeur = null;
            try
            {
                objServeur = CContexteDonnee.GetTableLoader(structure.NomTable, null, Connection.IdSession);
            }
            catch 
            {
            }
            if ( objServeur == null )
                return result;
            //Vérifie que le type est bien lié à cette connexion
            IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(
                Connection.IdSession,
                objServeur.GetType());
            if (connexion == null || connexion.ConnexionString != Connection.ConnexionString)
                return result;

			if (!result)
				return result;

			//S'assure que toutes les tables parentes existent
			foreach (CInfoRelation relation in structure.RelationsParentes)
				if (relation.TableParente != structure.NomTable && relation.IsInDb)
				{
					string strNomTableParenteInDb = CContexteDonnee.GetNomTableInDbForNomTable(relation.TableParente);
					if (!TableExists(strNomTableParenteInDb))
					{
						result = CreationOuUpdateTableFromType(CContexteDonnee.GetTypeForTable(relation.TableParente));
						if (!result)
							return result;
					}
				}

			if (TableExists(structure.NomTableInDb))
				result = UpdateTable(structure, strChampsAutorisesANull);
			else
				result = CreateTable(structure);

			return result;
		}

		
		virtual public CResultAErreur DeleteTable(string strNomTable)
		{
			return CResultAErreur.True;
		}
		virtual public CResultAErreur UpdateTable(CStructureTable structure)
		{
			return UpdateTable(structure, new ArrayList());
		}
		protected virtual CResultAErreur UpdateTable(CStructureTable structure, ArrayList strChampsAutorisesANull)
		{
			CResultAErreur result = CResultAErreur.True;

			ArrayList champsCrees = new ArrayList();
			DataTable dt = GetDataTableForUpdateTable(structure);

			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region M.A.J des champs de la table
			string strNomTable = structure.NomTableInDb;

			foreach (CInfoChampTable champ in structure.Champs)
			{
				bool bExists = false;

				bool bModifiedType = false;
				bool bModifiedNotNull = false;
				bool bModifiedLength = false;
                bool bModifiedIndex = false;

				if (strChampsAutorisesANull.Contains(champ.NomChamp))
					champ.NullAuthorized = true;

				// 1 - Verification d'existance de la colonne et des modifs à faire si elle existe
				foreach (DataColumn colonne in dt.Columns)
					if (champ.NomChamp == colonne.ColumnName)
					{
						bModifiedType = Champ_ModifiedType(champ, colonne);
						bModifiedLength = Champ_ModifiedLength(champ, colonne);
						bModifiedNotNull = Champ_ModifiedNotNull(champ, colonne, strChampsAutorisesANull);
                        bModifiedIndex = Champ_ModifiedIndex(strNomTable, champ);
                        
						bExists = true;
						break;
					}

				// 2 - Creation d'une colonne inexistante
				if (!bExists)
				{
					result = CreateChamp(structure.NomTableInDb, champ);
					if (result)
						champsCrees.Add(champ.NomChamp);
				}
				// 3 - Modification d'une colonne
				else if (bModifiedType || bModifiedNotNull || bModifiedLength)
				{
					result = UpdateChamp(strNomTable, champ, bModifiedNotNull, bModifiedLength, bModifiedType);
				}
				if (!result)
					return result;
                if (bModifiedIndex)
                {
                    if (champ.IsIndex)
                        result = CreateIndex(strNomTable, false, champ.NomChamp);
                    else
                        result = DeleteIndex(strNomTable, champ.NomChamp);
                    if (!result)
                        return result;
                }
			}

            
			// Suppression d'une colonne de la table
			foreach (DataColumn colonne in dt.Columns)
			{
				bool bExists = false;
				foreach (CInfoChampTable info in structure.Champs)
					if (info.NomChamp == colonne.ColumnName)
					{
						bExists = true;
						break;
					}

				if (!bExists)
				{
                    //SC le 17082009 : Plus de suppression automatique de colonne
                    //Par UpdateTable. Pour supprimer une colonne, il faut utiliser une opération
                    //de suppression de colonne
                    C2iEventLog.WriteInfo("Field "+colonne.ColumnName+" in table "+structure.NomTableInDb+" should be deleted");
					//result = DeleteChamp(structure.NomTableInDb, colonne.ColumnName);
					if (!result)
						return result;
				}
			}

			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region M.A.J des contraintes de la table
			List<CInfoRelation> relationsActuelles = new List<CInfoRelation>();
			result = GetRelationsExistantes(strNomTable, ref relationsActuelles);
			if (!result)
				return result;

			List<CInfoRelation> relationsVoulues = new List<CInfoRelation>();
            foreach (CInfoRelation r in structure.RelationsParentes)
            {
                if (r.IsInDb)
                    relationsVoulues.Add(r);
            }

			List<CInfoRelation> nouvellesRelations = GetRelationsInnexistantes(relationsActuelles, relationsVoulues);
			List<CInfoRelation> relationsObseletes = GetRelationsInnexistantes(relationsVoulues, relationsActuelles);

			foreach (CInfoRelation rel in nouvellesRelations)
			{
				foreach (string strChampFils in rel.ChampsFille)
					if (strChampsAutorisesANull.Contains(strChampFils))
						rel.Obligatoire = false;
				string strRequeteRelation = GetRequeteCreateCleEtrangere(rel);
				result = Connection.RunStatement(strRequeteRelation);
                if ( result )
                    AfterChangeContraintes ( CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableFille ));
				string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableFille );
				if (result && rel.Indexed)
                {
					result = CreateIndex(strNomTableFilleInDb, rel.IsClustered, rel.ChampsFille);
                    if ( result )
                        AfterChangeIndexs(CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableFille ));
                }

				if (!result)
				{
					result.EmpileErreur(I.T("Error while creating foreign key of @1 table (@2)|127", structure.NomTableInDb, strRequeteRelation));
					return result;
				}
			}
			foreach(CInfoRelation rel in relationsObseletes)
			{
				string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableFille );
				result = DeleteIndex(strNomTableFilleInDb, rel.ChampsFille);
				string strRequeteRelation = GetRequeteDeleteClefEtrangere(rel);
				result = Connection.RunStatement(strRequeteRelation);
                if ( result )
                {
                    AfterChangeContraintes ( strNomTableFilleInDb );
                }
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting foreign key of @1 table (@2)|212", structure.NomTableInDb, strRequeteRelation));
					return result;
				}
			}

            //Vérification des indexs des relations
            foreach (CInfoRelation rel in relationsVoulues)
            {
                if ( !nouvellesRelations.Contains ( rel )  )
                {
                    string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
                    if (rel.Indexed)
                    {
                        result = CreateIndex(strNomTableFilleInDb, rel.IsClustered, rel.ChampsFille);
                    }
                    else if ( ShouldDeleteIndexRelation ( strNomTableFilleInDb, rel ) )
                        result = DeleteIndex(strNomTableFilleInDb, rel.ChampsFille);
                    if (!result)
                        return result;

                }
            }

            //Gestion des attributs Index sur la classe
            foreach ( CInfoIndexTable info in structure.IndexSupplementaires )
            {
                result = CreateIndex(strNomTable, info.IsCluster, info.Champs);
                if (!result)
                    return result;
            }
			
			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			return result;
		}

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        protected virtual bool Champ_ModifiedIndex(string strNomTable, CInfoChampTable champ)
        {
            bool bIndexExists = IndexExists(strNomTable, champ.NomChamp);
            if (bIndexExists != champ.IsIndex && !champ.IsId)
                return true;
            return false;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        protected virtual bool ShouldDeleteIndexRelation ( string strNomTable, CInfoRelation infoRelation )
        {
            if ( !infoRelation.Indexed && IndexExists(strNomTable, infoRelation.ChampsFille ))
                return true;
            return false;
        }

        
       

		/// <summary>
		/// Doit retourner la liste des Relations actuellement existantes dans la base
		/// sous forme d'objet CInfoRelation
		/// Les propriétés suivantes doivent être remplies : 
		/// Table Fille
		/// Table Parente
		/// Champs Fils
		/// Champs Parents
		/// K
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <returns></returns>
		public abstract CResultAErreur GetRelationsExistantes(string strNomTable, ref List<CInfoRelation> relationsTable);


		private List<CInfoRelation> GetRelationsInnexistantes(List<CInfoRelation> relationsDeBase, List<CInfoRelation> relations)
		{
			List<CInfoRelation> relationsInnexistantes = new List<CInfoRelation>();
			foreach (CInfoRelation relVoulu in relations)
			{
				bool bRelExistante = false;
				foreach (CInfoRelation relActuelle in relationsDeBase)
				{
                    string strNomTable1 = relActuelle.TableParente;
                    string strNomTable2 = relVoulu.TableParente;
                    if (strNomTable1 == null)//La table n'existe plus
                        continue;
                    if (strNomTable2 == null)//La table n'existe plus
                        continue;
					//La relation voulu et la relation actuelle concernent les mêmes tables
                    if (strNomTable1 != strNomTable2)
                    {
                        strNomTable1 = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable1);
                        strNomTable2 = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable2);
                    }

					if (strNomTable1 == strNomTable2)
					{
						#region Verification que les champs fils sont identiques
						bool bChampsFilsIdentiques = false;
						foreach (string champFilsVoulu in relVoulu.ChampsFille)
						{
							foreach (string champFilsActuel in relActuelle.ChampsFille)
								if (champFilsActuel == champFilsVoulu)
								{
									bChampsFilsIdentiques = true;
									break;
								}
							if (!bChampsFilsIdentiques)
								break;
						}

						if (!bChampsFilsIdentiques)
							continue;
						#endregion

						#region Verification que les champs peres sont identiques
						bool bChampsParentsIdentiques = false;
						foreach (string champPereVoulu in relVoulu.ChampsParent)
						{
							foreach (string champPereActuel in relActuelle.ChampsParent)
								if (champPereActuel == champPereVoulu)
								{
									bChampsParentsIdentiques = true;
									break;
								}
							if (!bChampsParentsIdentiques)
								break;
						}

						bRelExistante = bChampsParentsIdentiques;
						#endregion
					}

					if (bRelExistante)
						break;
				}

				if (!bRelExistante)
					relationsInnexistantes.Add(relVoulu);
			}
			return relationsInnexistantes;

		}
		protected abstract DataTable GetDataTableForUpdateTable(CStructureTable structure);
		#endregion

		#region Champs
		public virtual CResultAErreur CreateChamp(string strNomTable, CInfoChampTable champ)
		{
			return CreateChamp(strNomTable, champ, false);
		}
		public virtual CResultAErreur CreateChamp(string strNomTable, CInfoChampTable champ, bool bForcerNull)
		{
			CResultAErreur result = CheckNomColonne(strNomTable, champ.NomChamp);
			if (!result.Result)
				return result;

			string strRequeteUpdate = "ALTER TABLE " + strNomTable + " ADD ";
			strRequeteUpdate += GetDeclarationChamp(champ, bForcerNull);

			result = Connection.RunStatement(strRequeteUpdate);
            if (result)
                AfterChangeChamps(strNomTable);

			if (!result)
				result.EmpileErreur(I.T("Error while fields updating of table @1|131", strNomTable));
			else if (champ.IsIndex)
				result = CreateIndex(strNomTable, false, champ.NomChamp);

			return result;

		}
		/// <summary>
		/// Supprime les dependances (DeleteChamp_Dependances)
		/// Execute la requete de suppression (GetRequeteDeleteChamp)
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="strChamp"></param>
		/// <returns></returns>
		public virtual CResultAErreur DeleteChamp(string strNomTable, string strChamp)
		{
            CResultAErreur result = CResultAErreur.True;
            if (!ChampExists(strNomTable, strChamp))
                return result;
			result = DeleteChamp_Dependances(strNomTable, strChamp);

			if (result)
				result = Connection.RunStatement(GetRequeteDeleteChamp(strNomTable,strChamp));
            if (result)
                AfterChangeChamps(strNomTable);

			if (!result)
				result.EmpileErreur(I.T("Error while deleting column @1 of table @2|132", strChamp, strNomTable));

			return result;
		}
		/// <summary>
		/// Supprime les dépendances (DeleteChamp_Dependances)
		/// Execute la requete d'update (GetRequeteUpdateChamp)
		/// Créé l'index si existant (CreateIndex)
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="champ"></param>
		/// <param name="bNotNull"></param>
		/// <param name="bLength"></param>
		/// <param name="bType"></param>
		/// <param name="bRelation"></param>
		/// <returns></returns>
		public virtual CResultAErreur UpdateChamp(string strNomTable, CInfoChampTable champ, bool bModifiedNotNull, bool bModifiedLength, bool bModifiedType)
		{
			//Suppression index
			CResultAErreur result = DeleteIndex(strNomTable, champ.NomChamp);

			//Mise à jour du champ
			if(result)
				result = Connection.RunStatement(GetRequeteUpdateChamp(strNomTable, champ, bModifiedNotNull, bModifiedLength, bModifiedType));
            if (result)
                AfterChangeChamps(strNomTable);

			//Creation de l'index
			if (result && champ.IsIndex)
				result = CreateIndex(strNomTable, false, champ.NomChamp);

			if (!result)
				result.EmpileErreur(I.T("Error while fields updating of table @1|131", strNomTable));

			return result;
		}

		public abstract CResultAErreur DeleteChamp_Dependances(string strNomTable, string strNomChamp);
		public abstract CResultAErreur DeleteChamp_ClesEtrangeres(string strNomTable, string strNomChamp);

		protected virtual bool Champ_ModifiedType(CInfoChampTable champ, DataColumn colonne)
		{
			if (champ.IsAutoId && !IsGoodDataColumnType(colonne, typeof(Int32)))
				return true;
			else if (!champ.IsAutoId && !IsGoodDataColumnType(colonne, champ.TypeDonnee))
				return true;
			else
				return false;
		}
		protected virtual bool Champ_ModifiedNotNull(CInfoChampTable champ, DataColumn colonne, ArrayList champsAutorisesANull)
		{
			return colonne.AllowDBNull != champ.NullAuthorized;
		}
		
        protected virtual bool Champ_ModifiedLength(CInfoChampTable champ, DataColumn colonne)
		{
			if (champ.IsLongString && colonne.MaxLength < DataBaseTypesMappeur.MinDBLongStringLength)
				return true;
            if ( AutoAdaptLongString && colonne.MaxLength > DataBaseTypesMappeur.MaxDBStringLength )
                return false;
            if (champ.Longueur > DataBaseTypesMappeur.MaxDBStringLength && !champ.IsLongString)
            {
                if (colonne.MaxLength != DataBaseTypesMappeur.MaxDBStringLength)
                    return true;
            }
            else
            {
                if (colonne.MaxLength != champ.Longueur && champ.TypeDonnee == typeof(string) && !champ.IsLongString)
                    return true;
            }
            return false;
		}

		#endregion

        /// <summary>
        /// Indique si l'index peut être créé dans la base de destination
        /// Par exemple, en access, on n'a pas le droit de créer d'index
        /// sur un champ mémo
        /// </summary>
        /// <param name="strNomTable"></param>
        /// <param name="strChamps"></param>
        /// <returns></returns>
        protected virtual bool IndexAllowed(string strNomTable, string[] strChamps)
        {
            return true;
        }

		//Index
		protected virtual CResultAErreur CreateIndex(string strNomTable, bool bClustered, params string[] strChamps)
		{
			CResultAErreur result = CResultAErreur.True;
            if (!IndexAllowed(strNomTable, strChamps))
                return result;
            if ( IndexExists ( strNomTable, strChamps ) && SaitGererLesIndexCluster())
            {
                if (bClustered != IsCluster(strNomTable, strChamps))
                    result = DeleteIndex(strNomTable, strChamps);
                if (!result)
                    return result;
            }

			if (!IndexExists(strNomTable, strChamps))
			{
				string strRequeteIndex = GetRequeteCreateIndex(strNomTable, bClustered, strChamps);
				result = Connection.RunStatement(strRequeteIndex);
                if (result)
                    AfterChangeIndexs(strNomTable);
				if(!result)
					result.EmpileErreur(I.T("Error while creating index in table @1|128", strNomTable));
			}
			return result;
		}
		protected virtual CResultAErreur DeleteIndex(string strNomTable, params string[] strChamps)
		{
			CResultAErreur result = CResultAErreur.True;

			if(!IndexExists(strNomTable,strChamps))
				return result;

			string strNomIndex = GetNomIndex(strNomTable, strChamps);
			result = Connection.RunStatement(GetRequeteDeleteIndex(strNomTable, strNomIndex));
            if (result)
                AfterChangeIndexs(strNomTable);

			if (!result)
				result.EmpileErreur(I.T("Error while deleting index @1 of the table @2|130", strNomIndex, strNomTable));

			return result;
		}

		#endregion

		#region Creation de Requetes
		/// <summary>
		/// Retourne la déclaration d'un colonne
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public virtual string GetDeclarationDefaultValueForType(Type tp)
		{
			if (tp.IsEnum)
				return "DEFAULT 0";
			else if (tp == typeof(DateTime) || tp == typeof(DateTime?) || tp == typeof(CDateTimeEx))
				return "DEFAULT GETDATE()";
			else if (tp == typeof(double) || tp == typeof(double?) || tp == typeof(int) || tp == typeof(int?))
				return "DEFAULT 0";
			else if (tp == typeof(string))
				return "DEFAULT ''";
			else if (tp == typeof(bool) ||tp == typeof(bool?))
				return "DEFAULT 0";
			else
				return "";
		}

		protected virtual string GetDeclarationChampIdForCreateTable(CStructureTable structure)
		{
			string strResult = "";
			if (structure.HasChampIdAuto)
			{
				strResult += GetNewNomClefPrimaire(structure.ChampsId) + " ";
				strResult += DataBaseTypesMappeur.GetStringDBTypeFromType(structure.ChampsId[0].TypeDonnee);
				strResult += " NOT NULL";
			}
			return strResult;

		}
        protected virtual string GetDeclarationChamp_TypeAndLength(CInfoChampTable champ)
        {
            string strType = DataBaseTypesMappeur.GetStringDBTypeFromType(champ.TypeDonnee);
            if (champ.TypeDonnee == typeof(string))
            {
                if (champ.IsLongString || (champ.Longueur > DataBaseTypesMappeur.MaxDBStringLength && AutoAdaptLongString))
                    strType = DataBaseTypesMappeur.DBLongStringDefinition;
                else
                {
                    strType += "(";

                    if (champ.Longueur > DataBaseTypesMappeur.MaxDBStringLength)
                        strType += DataBaseTypesMappeur.MaxDBStringLength.ToString();
                    else
                        strType += champ.Longueur.ToString();

                    strType += ")";
                }
            }
            return strType;
        }
		protected virtual string GetDeclarationChamp_NotNull(CInfoChampTable champ, bool bUpdate)
		{
			if (champ.IsAutoId)
				return "NOT NULL";
			else if (!champ.NullAuthorized && !bUpdate)
				return GetDeclarationDefaultValueForType(champ.TypeDonnee) + " NOT NULL" ;
			else if (!champ.NullAuthorized && bUpdate)
				return "NOT NULL ";
			else
				return "NULL";
		}

		/// <summary>
		/// Retourne la déclaration d'un champ pour le créer
		/// </summary>
		/// <param name="champ"></param>
		/// <returns></returns>
		protected virtual string GetDeclarationChamp(CInfoChampTable champ)
		{
			return GetDeclarationChamp(champ, false);
		}
		/// <summary>
		/// Retourne la déclaration d'un champ pour le créer
		/// </summary>
		/// <param name="champ"></param>
		/// <returns></returns>
		protected virtual string GetDeclarationChamp(CInfoChampTable champ, bool bForcerNull)
		{
			string strDeclaration = champ.NomChamp + " ";
			strDeclaration += GetDeclarationChamp_TypeAndLength(champ) + " ";
			strDeclaration = bForcerNull || champ.NullAuthorized? strDeclaration += "NULL": strDeclaration += GetDeclarationChamp_NotNull(champ, false);
			return strDeclaration;
		}
		protected virtual string GetDeclarationChampsForCreateTable(CStructureTable structure)
		{
			string strResult = "";
			foreach (CInfoChampTable champ in structure.Champs)
				if (!champ.IsId || !structure.HasChampIdAuto)
				{
					if(strResult != "")
						strResult += ",";
					strResult += GetDeclarationChamp(champ);
				}
			return strResult;
		}

		/// <summary>
		/// N'appelle pas GetDeclaration Champ mais GetDeclarationChamp_TypeAndLength puis
		/// GetDeclarationChamp_NotNull si le null à changé (sous oracle le redéclaration du null
		/// génère une erreur, sous SQL Serveur il n'est pas nécessaire de spécifier le null si ce
		/// dernier n'as pas changé)
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="champ"></param>
		/// <param name="bNotNull"></param>
		/// <param name="bLength"></param>
		/// <param name="bType"></param>
		/// <returns></returns>
		protected virtual string GetRequeteUpdateChamp(string strNomTable, CInfoChampTable champ, bool bModifiedNotNull, bool bModifiedLength, bool bModifiedType)
		{
			string strRequeteUpdate = "ALTER TABLE " + strNomTable + " ";
			strRequeteUpdate += "ALTER COLUMN " + champ.NomChamp + " ";

            strRequeteUpdate += GetDeclarationChamp_TypeAndLength(champ) + " ";
			if (!champ.NullAuthorized)
				strRequeteUpdate += GetDeclarationChamp_NotNull(champ, true);

			return strRequeteUpdate;
		}
		protected virtual string GetRequeteDeleteChamp(string strNomTable, string strChamp)
		{
			return "ALTER TABLE " + strNomTable + " DROP COLUMN " + strChamp;
		}
		protected virtual string GetRequeteCreateTable(CStructureTable structure)
		{
			string strRequeteCreation = "CREATE TABLE " + structure.NomTableInDb + " (";

			//Déclaration du champ ID
			string strDeclarationId = GetDeclarationChampIdForCreateTable(structure); 
			
			//Déclaration des champs
			string strDeclarationsChamps = GetDeclarationChampsForCreateTable(structure);

			if (strDeclarationId != "")
			{
				strRequeteCreation += strDeclarationId;
				if (strDeclarationsChamps != "")
					strRequeteCreation += ", " + strDeclarationsChamps;
			}
			else
				strRequeteCreation += strDeclarationsChamps;

			return strRequeteCreation + ")";
		}
		protected virtual string GetRequeteCreateClefPrimaire(string strNomTable, params string[] strFields)
		{
			string result = "ALTER TABLE " + strNomTable + " ";
			result += "ADD CONSTRAINT PK_" + strNomTable + " ";
			result += "PRIMARY KEY (";

			foreach (string champ in strFields)
				result += champ+ ",";

			return result.Substring(0, result.Length - 1) + ")";
		}
		protected virtual string GetRequeteDeleteClefPrimaire(CStructureTable structure)
		{
			string nomclepk = GetNomClefPrimaire(structure);
			if (nomclepk == "")
				return "";
			return "ALTER TABLE " + structure.NomTableInDb + " DROP CONSTRAINT " + nomclepk;
		}
		protected virtual string GetRequeteDeleteClefEtrangere(CInfoRelation relationToDelete)
		{
			string nomcleetr = GetNomClefEtrangere(relationToDelete);
			string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable ( relationToDelete.TableFille );
			if (nomcleetr == "")
				return "";
			return "ALTER TABLE " + strNomTableFilleInDb + " DROP CONSTRAINT " + nomcleetr;
		}
		protected virtual string GetRequeteDeleteIndex(string strNomTable, string strNomIndex)
		{
			return "DROP INDEX " + strNomIndex + " ON " + strNomTable;
		}


        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Retourne true si l'index demandé est un index cluster
        /// </summary>
        /// <param name="strNomTable"></param>
        /// <param name="strChamps"></param>
        /// <returns></returns>
        public abstract bool IsCluster(string strNomTable, params string[] strChamps);

		protected virtual string GetRequeteCreateIndex ( string strNomTable, bool bClustered, params string[] strFields )
		{
			string strNomIndex = GetNewNomIndex ( strNomTable, strFields );
			string strRequeteIndex = "CREATE INDEX " + strNomIndex + " on " + strNomTable+" (";

			foreach ( string strField in strFields )
				strRequeteIndex += strField + ",";

			return strRequeteIndex.Substring(0, strRequeteIndex.Length - 1) + ")";
		}
		protected virtual string GetRequeteCreateCleEtrangere(CInfoRelation rel)
		{
			string strTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableFille );
			string strTableParenteInDb = CContexteDonnee.GetNomTableInDbForNomTable ( rel.TableParente );
			string strRequeteRelation = "";
			strRequeteRelation += "ALTER TABLE " + strTableFilleInDb + " ";
			strRequeteRelation += "ADD CONSTRAINT " + GetNewNomClefEtrangere(rel);

			strRequeteRelation += " FOREIGN KEY (";
			foreach (string strChamp in rel.ChampsFille)
				strRequeteRelation += strChamp + ",";
			strRequeteRelation = strRequeteRelation.Substring(0, strRequeteRelation.Length - 1);
			strRequeteRelation += ") ";
			strRequeteRelation += "REFERENCES " + strTableParenteInDb + "(";
			foreach (string strChamp in rel.ChampsParent)
				strRequeteRelation += strChamp + ",";
			strRequeteRelation = strRequeteRelation.Substring(0, strRequeteRelation.Length - 1);
			strRequeteRelation += ")";
			return strRequeteRelation;
		}
		#endregion

		#region Nomage
		protected virtual string GetNomIndex(string strNomTable, params string[] strFields)
		{
			string strNomIndex = "IX_" + strNomTable;
			foreach (string strField in strFields)
				strNomIndex += "_" + strField;
			return strNomIndex;
		}
		protected virtual string GetNomClefPrimaire(CStructureTable structure)
		{
			string nom = "";
			if(structure.ChampsId.Length > 0)
				nom = structure.ChampsId[0].NomChamp;
			return nom;
		}
		protected virtual string GetNomClefEtrangere(CInfoRelation rel)
		{
			return rel.RelationKey;
		}
		protected virtual string GetNomTrigger()
		{
			return "";
		}
		protected virtual string GetNomSequence()
		{
			return "";
		}

		protected virtual string GetNewNomIndex(string strNomTable, params string[] strFields)
		{
			string strNomIndex = "IX_" + strNomTable;
			foreach (string strField in strFields)
				strNomIndex += "_" + strField;
			return strNomIndex;
		}
		protected virtual string GetNewNomClefPrimaire(CInfoChampTable[] champs)
		{
			return champs[0].NomChamp;
		}
		protected virtual string GetNewNomClefEtrangere(CInfoRelation rel)
		{
			return rel.RelationKey;
		}
		protected virtual string GetNewNomTrigger(string strNomTable)
		{
			return "";
		}
		protected virtual string GetNewNomSequence(string strNomTable)
		{
			return "";
		}
		#endregion
		#region Verification Nommage
		protected virtual CResultAErreur CheckNomsStructureTable(CStructureTable structure)
		{
			string strNomTable = structure.NomTableInDb;
			CResultAErreur result = CheckNomTable(strNomTable);
			foreach (CInfoChampTable info in structure.Champs)
			{
				CResultAErreur resultCol = CheckNomColonne(info.NomChamp, strNomTable);
				if (!result)
				{
					result.Erreur += resultCol.Erreur;
					resultCol.Result = false;
				}
			}
			return result;
		}
		protected virtual CResultAErreur CheckNomColonne(string strNomTable, string strNomCol)
		{
			return CResultAErreur.True;
		}
		protected virtual CResultAErreur CheckNomTable(string strNomTable)
		{
			return CResultAErreur.True;
		}
		#endregion

		#region Tests Existance
		public virtual bool TableExists(string strNomTableInDb)
		{
			string[] tableauNomsTable = Connection.TablesNames;
			foreach (string obj in tableauNomsTable)
			{
				if (obj.ToUpper().Trim() == strNomTableInDb.ToUpper().Trim())
					return true;
			}
			return false;
		}
		public abstract bool ChampExists(string strTableName, string strChampName);
		public abstract bool SequenceExists(string NomSequence);
		public abstract bool TriggerExists(string NomTrigger);
		public abstract bool IndexExists(string strNomTable, params string[] strChamps);

		public virtual CResultAErreur UpdateStructureTableRegistre()
		{
			CResultAErreur result = CResultAErreur.True;
			//Cherche la table de registre
			bool bExist = false;
			foreach (string strNomTable in Connection.TablesNames)
			{
				if (strNomTable == CDatabaseRegistre.c_nomTable)
				{
					bExist = true;
					break;
				}
			}
			if (!bExist)
			{//C'est une initialisation initiale
				return result;
			}
			else
			{
				//Vérifie que toutes les colonnes existent
				string[] strColonnes = Connection.GetColonnesDeTable(CDatabaseRegistre.c_nomTable);
				Dictionary<string, bool> tableColonnes = new Dictionary<string, bool>();
				foreach (string strColonne in strColonnes)
					tableColonnes.Add(strColonne, true);
				if (!tableColonnes.ContainsKey(CDatabaseRegistre.c_champCle))
				{
					CInfoChampTable info = new CInfoChampTable(
						CDatabaseRegistre.c_champCle,
						typeof(string),
						255,
						false,
						true,
						false,
						false,
						true);
					result = CreateChamp(CDatabaseRegistre.c_nomTable, info);
					if (!result)
						return result;
				}
				if (!tableColonnes.ContainsKey(CDatabaseRegistre.c_champValeur))
				{
					CInfoChampTable info = new CInfoChampTable(
						CDatabaseRegistre.c_champValeur,
						typeof(string),
						255,
						false,
						false,
						true,
						false,
						true);
					result = CreateChamp(CDatabaseRegistre.c_nomTable, info);
					if (!result)
						return result;
				}
				if (!tableColonnes.ContainsKey(CDatabaseRegistre.c_champBlob))
				{
					CInfoChampTable info = new CInfoChampTable(
						CDatabaseRegistre.c_champBlob,
						typeof(CDonneeBinaireInRow),
						255,
						false,
						false,
						true,
						false,
						true);
					result = CreateChamp(CDatabaseRegistre.c_nomTable, info);
					if (!result)
						return result;
				}
			}
			return result;
		}

		#endregion

	}
}
