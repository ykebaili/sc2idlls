using System;
using System.Collections.Generic;
using System.Data;

using sc2i.common;
using System.Collections;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Tous les Objets donné qui ont un champ unique identifiant qui est un numérique auto
	/// </summary>
	[DynamicClass("<Entity with ID>")]
	public abstract class CObjetDonneeAIdNumerique : CObjetDonnee, IObjetDonneeAIdNumerique
	{
        private static Dictionary<string, string> m_cacheChampsId = new Dictionary<string, string>();

        

		/// //////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique( CContexteDonnee ctx)
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////////////////
        public CObjetDonneeAIdNumerique(DataRow row)
			:base(row)
		{
		}

        

		/// //////////////////////////////////////////////////////////////
		[DynamicField("Id",CObjetDonnee.c_categorieChampSystème)]
		public int Id
		{
			get
			{
                AssureRow();
                if (VersionToReturn == DataRowVersion.Original && (Row.RowState == DataRowState.Deleted ||
                    Row.RowState == DataRowState.Detached))
                {
                    try
                    {
                        return (int)m_row[GetChampId(), DataRowVersion.Original];
                    }
                    catch
                    {
                        return -1;
                    }
                }
                return (int)m_row[GetChampId(), VersionToReturn];
			}
			set
			{
				PointeSurLigne(value);
			}
		}

        public override CDbKey DbKey
        {
            get
            {
                if (ManageIdUniversel)
                    return base.DbKey;
                return CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(Id);
            }
        }

        /// //////////////////////////////////////////////////////////////
        public override object[] GetValeursCles()
        {
            return new object[] { Id };
        }

		/// //////////////////////////////////////////////////////////////
		public virtual string GetChampId()
		{
            string strChamp = null;
            if (!m_cacheChampsId.TryGetValue(GetNomTable(), out strChamp))
            {
                strChamp = base.GetChampsId()[0];
                m_cacheChampsId[GetNomTable()] = strChamp;
            }
            return strChamp;
		}

        /// //////////////////////////////////////////////////////////////
        public override string[] GetChampsId()
        {
            return new string[] { GetChampId() };
        }


		/// //////////////////////////////////////////////////////////////
        protected override void InitValeurDefaut()
        {
            base.InitValeurDefaut();
            IsDeleted = false;
        }


		/// //////////////////////////////////////////////////////////////
		public bool ReadIfExists ( int nId )
		{
			return ReadIfExists(nId, true);
		}

        


		/// //////////////////////////////////////////////////////////////
		public bool ReadIfExists(int nId, bool bAutoriseLectureInDb)
		{
			return ReadIfExists(new object[] { nId }, bAutoriseLectureInDb);
		}

        
		
		


		/// //////////////////////////////////////////////////////////////
		public CObjetDonnee GetParent ( string strChampFils, Type typeParent )
		{
			return base.GetParent( new string[]{strChampFils}, typeParent );
		}

		/// //////////////////////////////////////////////////////////////
		public void SetParent ( string strChampFils, CObjetDonnee parent )
		{
			base.SetParent ( new string[]{strChampFils}, parent );
		}

		/////////////////////////////////////////////
		///Retourne tous les éléments liés par une relationTypeId
		public CListeObjetsDonnees GetDependancesRelationTypeId(
			string strNomTableFille,
			string strNomChampTypeSurTableFille,
			string strNomChampIdSurTableFille,
			bool bProgressive)
		{
			return GetDependancesRelationTypeId(
				strNomTableFille,
				strNomChampTypeSurTableFille,
				strNomChampIdSurTableFille,
				bProgressive,
				false);
		}

		/////////////////////////////////////////////
		/// <summary>
		/// Retourne tous les éléments liés par une relationTypeId
		/// </summary>
		/// <param name="strNomTableFille"></param>
		/// <param name="strNomChampTypeSurTableFille"></param>
		/// <param name="strNomChampIdSurTableFille"></param>
		/// <returns></returns>
		public CListeObjetsDonnees GetDependancesRelationTypeId ( 
			string strNomTableFille, 
			string strNomChampTypeSurTableFille, 
			string strNomChampIdSurTableFille,
			bool bProgressive,
			bool bInterditLectureInDb )
		{
            string strColDep = RelationTypeIdAttribute.GetNomColDepLue(strNomTableFille);
            DataColumn col = Table.Columns[strColDep];
            if (col == null)
            {
                col = new DataColumn ( strColDep, typeof(bool));
                col.DefaultValue = false;
                col.AllowDBNull = false;
                Table.Columns.Add(col);
            }
            if ((bool)Row[strColDep] && !bProgressive)
                bInterditLectureInDb = true;
			Type typeFille = CContexteDonnee.GetTypeForTable ( strNomTableFille );
			CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, typeFille, false);
			liste.RemplissageProgressif = bProgressive;
			liste.InterditLectureInDB = bInterditLectureInDb;
			liste.PreserveChanges = true;
			liste.Filtre = new CFiltreData(
				strNomChampTypeSurTableFille+"=@1 and "+
				strNomChampIdSurTableFille+"=@2",
				GetType().ToString(),
				Id );
            if ( !bProgressive )
                CContexteDonnee.ChangeRowSansDetectionModification(Row, strColDep, true);
			return liste;
		}

		////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( bool bAuMomentDeLaSauvegarde )
		{
			CResultAErreur result = UniqueAttribute.VerifieUnicite(this);
			if (result)
				result = base.VerifieDonnees( bAuMomentDeLaSauvegarde );
			return result;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique s'il est possible de supprimer un élément
		/// </summary>
		/// <returns></returns>
		protected virtual CResultAErreur MyCanDelete ( )
		{
			return CResultAErreur.True;
		}

        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        public override CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi()
        {
            return DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(false);
        }

		////////////////////////////////////////////////////////////
		public override CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi( bool bDansContexteCourant)
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( ContexteDonnee, GetType(), false );
			liste.Filtre = new CFiltreData ( GetChampId()+"=@1", Id );
			return DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi ( liste, bDansContexteCourant );
		}

        ////////////////////////////////////////////////////////////
        public static CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(CListeObjetsDonnees liste)
        {
            return DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(liste, false);
        }


		////////////////////////////////////////////////////////////
		public static CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(CListeObjetsDonnees liste, bool bDansContexteCourant )
		{
			CResultAErreur result = CResultAErreur.True;
			bool bOldEnforce = liste.ContexteDonnee.EnforceConstraints;
			StringBuilder bl = new StringBuilder();
			foreach (CObjetDonneeAIdNumerique objetTmp in liste)
			{
                if (objetTmp.IsValide())
                {
                    bl.Append(objetTmp.Id);
                    bl.Append(',');
                }
			}
			if (bl.Length == 0)
				return result;
			bl.Remove(bl.Length - 1, 1);
			if ( IsUtiliseDansVersionFuture ( liste, bl.ToString() ))
			{
				result.EmpileErreur(I.T("Cannot delete these elements because they will be used in future versions|187"));
				return result;
			}
			try
			{
				liste.ContexteDonnee.EnforceConstraints = false;
				if (liste.Count == 0)
					return result;
				foreach (CObjetDonneeAIdNumerique objet in liste)
				{
                    if (objet.IsValide())
                    {
                        result = objet.MyCanDelete();
                        if (!result)
                            return result;
                    }
				}
				Type typeElements = liste.TypeObjets;
				string strListeIds = "";
                foreach (CObjetDonneeAIdNumerique objet in liste)
                {
                    if (objet.IsValide())
                        strListeIds += objet.Id.ToString() + ",";
                }
				strListeIds = strListeIds.Substring(0, strListeIds.Length - 1);
				string strNomTable = CContexteDonnee.GetNomTableForType(liste.TypeObjets);

				foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(strNomTable))
				{
					if (relation.TableParente == strNomTable)
					{
						//Car la dépendance doit être lue pour la suppression
						CListeObjetsDonnees listeDep = liste.GetDependancesFilles(relation);
						result = DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(listeDep, bDansContexteCourant);
						if (!result)
							return result;
					}
				}

                if (typeElements.GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true).Length == 0)
                {
                    //Peut-on supprimer les relationsTypeId
                    foreach (RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds)
                    {
                        Type tpLiens = CContexteDonnee.GetTypeForTable(relation.TableFille);
                        CListeObjetsDonnees listeTypeId = new CListeObjetsDonnees(liste.ContexteDonnee, tpLiens, false);
                        CFiltreData filtre = new CFiltreData(
                            relation.ChampType + "=@1 and " +
                            relation.ChampId + " in (" + strListeIds + ")",
                            typeElements.ToString());
                        listeTypeId.Filtre = filtre;
                        listeTypeId.PreserveChanges = true;
                        result = DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(listeTypeId, bDansContexteCourant);
                    }
                }
				foreach (CObjetDonneeAIdNumerique objet in liste.ToArrayList())
				{
                    if (objet.IsValide())
                    {
                        result = objet.DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(bDansContexteCourant);
                        if (!result)
                            return result;
                    }
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			finally
			{
				try
				{
					liste.ContexteDonnee.EnforceConstraints = bOldEnforce;
				}
				catch
				{
					if (liste.ContexteDonnee.HasErrors)
					{
						foreach (DataTable table in liste.ContexteDonnee.Tables)
						{
							if (table.HasErrors)
							{
								foreach (DataRow row in table.Rows)
								{
									if (row.HasErrors)
									{
										string strKey = "";
										foreach (DataColumn col in table.PrimaryKey)
											strKey += row[col].ToString() + "/";
										result.EmpileErreur("Error while deleting (" + table.TableName + "[" +
											strKey + "] : " + row.RowError);
									}
								}
							}
						}
					}
				}
				
			}
			return result;
		}

			

		////////////////////////////////////////////////////////////
		public static CResultAErreur CanDelete(CListeObjetsDonnees liste)
		{
			CResultAErreur result = CResultAErreur.True;
			if (liste.Count == 0)
				return result;
            bool bPasObjetAIdNumeriqueAuto = false;
			foreach (CObjetDonnee objet in liste)
			{
                if (!(objet is CObjetDonneeAIdNumerique))
                {
                    bPasObjetAIdNumeriqueAuto = true;
                    result = objet.CanDelete();
                }
                else
                    result = ((CObjetDonneeAIdNumerique)objet).MyCanDelete();
				if (!result)
					return result;
			}
            if (bPasObjetAIdNumeriqueAuto)
                return result;
			Type typeElements = liste.TypeObjets;
			string strListeIds = "";
			int nCount = liste.Count;
			int nTailleParBloc = 500;
			string strNomTable = CContexteDonnee.GetNomTableForType(liste.TypeObjets);
			//Copie de la liste, pour sécuriser le parcours
			List<CObjetDonneeAIdNumerique> lstElements = liste.ToList<CObjetDonneeAIdNumerique>();
			
			//Travaille par bloc de 500 enregistrements pour les contrôles
			for ( int nBloc = 0; nBloc < nCount; nBloc += nTailleParBloc )
			{
				StringBuilder bl = new StringBuilder();
				int nMin = Math.Min ( nBloc+nTailleParBloc, nCount );
				//Liste des éléments du bloc en cours
				for ( int nElement = nBloc; nElement < nMin; nElement++ )
				{
					bl.Append ( lstElements[nElement].Id );
					bl.Append(',');
				}
				if ( bl.Length > 0 )
				{
					bl.Remove ( bl.Length-1, 1);

					//Liste des objets du bloc en cours
					CListeObjetsDonnees listePartielle = new CListeObjetsDonnees(liste.ContexteDonnee, liste.TypeObjets, false);
					listePartielle.Filtre = new CFiltreData(
						liste.ContexteDonnee.GetTableSafe(strNomTable).PrimaryKey[0].ColumnName + " in (" +
						bl.ToString() + ")"); ;
					listePartielle.InterditLectureInDB = true;//Pas besoin de lire car les éléments sont déjà dans le contexte de données

                    listePartielle.Filtre.IntegrerLesElementsSupprimes = liste.Filtre != null?
                        liste.Filtre.IntegrerLesElementsSupprimes:
                        (liste.FiltrePrincipal!=null?liste.FiltrePrincipal.IntegrerLesElementsSupprimes:false);

                    listePartielle.Filtre.IgnorerVersionDeContexte = liste.Filtre != null ?
                        liste.Filtre.IgnorerVersionDeContexte:
                        (liste.FiltrePrincipal != null ? liste.FiltrePrincipal.IgnorerVersionDeContexte : false);

					strListeIds = bl.ToString();
					foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(strNomTable))
					{
						if (relation.TableParente == strNomTable)//Relation fille
						{
							if (!relation.Composition)//Ce n'est pas une composition, il ne faut pas qu'il y ait de fils
							{
								if (!relation.PasserLesFilsANullLorsDeLaSuppression)//Sinon, ce n'est pas grave !
								{
									IObjetServeur objServeur = liste.ContexteDonnee.GetTableLoader(relation.TableFille);
									//Ceux pour lesquels les dépendances sont chargés n'ont pas besoin de regarder la base
									StringBuilder strIdsAVoirDansLaBase = new StringBuilder();
									DataRelation dataRel = null;
									foreach (DataRelation childRel in liste.ContexteDonnee.GetTableSafe(relation.TableParente).ChildRelations)
									{
										bool bCestElle = false;
										if (childRel.ChildTable.TableName == relation.TableFille)
										{
											if (childRel.ChildColumns.Length == relation.ChampsFille.Length)
											{
												bCestElle = true;
												for (int nCol = 0; nCol < childRel.ChildColumns.Length; nCol++)
												{
													if (childRel.ChildColumns[nCol].ColumnName != relation.ChampsFille[nCol])
													{
														bCestElle = false;
														break;
													}
												}
											}
										}
										if (bCestElle)
										{
											dataRel = childRel;
											break;
										}
									}
									if (dataRel != null)
									{
										foreach (CObjetDonneeAIdNumerique objetTmp in listePartielle)
										{
											string strFKName = objetTmp.ContexteDonnee.GetForeignKeyName(dataRel);
											if (objetTmp.IsDependanceChargee(strFKName))
											{
												DataRow[] rowsFilles = objetTmp.Row.Row.GetChildRows(dataRel);
												if (rowsFilles.Length > 0)
												{
													result.EmpileErreur(I.T("Cannot delete element @1, @2 elements (@3) are linked|190",
														objetTmp.DescriptionElement,
														rowsFilles.Length.ToString(),
														relation.TableFille));
													return result;
												}
											}
											else
											{
												strIdsAVoirDansLaBase.Append(objetTmp.Id);
												strIdsAVoirDansLaBase.Append(',');
											}
										}
										if (strIdsAVoirDansLaBase.Length > 0)
											strIdsAVoirDansLaBase.Remove(strIdsAVoirDansLaBase.Length - 1, 1);
									}
									else
										strIdsAVoirDansLaBase.Append(strListeIds);
									if (strIdsAVoirDansLaBase.Length > 0)
									{
										CFiltreData filtre = new CFiltreData(relation.ChampsFille[0] + " in (" +
											strIdsAVoirDansLaBase.ToString() + ")");
										int nNb = objServeur.CountRecords(relation.TableFille, filtre);
										if (nNb != 0)
										{
											Type tp = CContexteDonnee.GetTypeForTable(relation.TableFille);
											result.EmpileErreur(I.T("Cannot delete '@1' elements because '@2' elements are dependent|160", DynamicClassAttribute.GetNomConvivial(typeElements), DynamicClassAttribute.GetNomConvivial(tp)));
											return result;
										}
									}
								}
							}
							else//C'est une composition
							{
								CListeObjetsDonnees listeDep = listePartielle.GetDependancesFilles(relation, liste.Filtre != null && liste.Filtre.IntegrerLesElementsSupprimes ||
                                    liste.FiltrePrincipal != null && liste.FiltrePrincipal.IntegrerLesElementsSupprimes);
								result = CanDelete(listeDep);
								if (!result)
									return result;
							}
						}
					}
					//Peut-on supprimer les relationsTypeId
					foreach (RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds)
					{
						Type tpLiens = CContexteDonnee.GetTypeForTable(relation.TableFille);
						if (relation.AppliquerContrainteIntegrite && !relation.CanDeleteToujours && relation.IsAppliqueToType(typeElements))
						{
							CListeObjetsDonnees listeTypeId = new CListeObjetsDonnees(liste.ContexteDonnee, tpLiens, false);
							CFiltreData filtre = new CFiltreData(
								relation.ChampType + "=@1 and " +
								relation.ChampId + " in (" + strListeIds + ")",
								typeElements.ToString());
							listeTypeId.Filtre = filtre;
							if (relation.Composition)
							{
								result = CanDelete(listeTypeId);
								if (!result)
									return result;
							}
							else
							{
								int nNb = listeTypeId.CountNoLoad;
								if (nNb != 0)
								{
									Type tp = CContexteDonnee.GetTypeForTable(relation.TableFille);
									result.EmpileErreur(I.T("Cannot delete the required elements because elements @1 exist in database|161", DynamicClassAttribute.GetNomConvivial(tpLiens)));
									return result;
								}
							}
						}
					}

					if (IsUtiliseDansVersionFuture(liste, strListeIds))
					{
						result.EmpileErreur(I.T("Cannot delete these elements because they will be used in future versions|187"));
						return result;
					}
				}
			}

			return result;

		}

		/// <summary>
		/// Retourne vrai si l'objet est utilisé dans une version future
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public bool IsUtiliseDansVersionFuture(CObjetDonneeAIdNumerique objet)
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees(objet.ContexteDonnee, objet.GetType(), false);
			liste.Filtre = new CFiltreData(objet.GetChampId() + "=@1", objet.Id);
			liste.InterditLectureInDB = true;
			return IsUtiliseDansVersionFuture(liste, objet.Id.ToString());
		}

		/// <summary>
		/// Retourne vrai si l'un des objets est utilisé dans une version future
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static bool IsUtiliseDansVersionFuture(CListeObjetsDonnees liste, string strListeIds)
		{
			CResultAErreur result = CResultAErreur.True;
			Type typeElements = liste.TypeObjets;
			if (!typeof(IObjetSansVersion).IsAssignableFrom(typeElements))
			{
				//Cet élément n'est-il pas utilisé dans une version future dependant de celle-ci
				int? nIdVersion = liste.ContexteDonnee.IdVersionDeTravail;
				if (nIdVersion == null)
				{
					//L'élément est-il présent dans une version future
					CListeObjetsDonnees listeFuturs = new CListeObjetsDonnees(liste.ContexteDonnee, liste.TypeObjets, false);
					listeFuturs.Filtre = new CFiltreData(
						CSc2iDataConst.c_champOriginalId + " in (" + strListeIds + ")");
					listeFuturs.Filtre.IgnorerVersionDeContexte = true;
					if (listeFuturs.CountNoLoad != 0)
					{
						return true;
					}
				}
				else
				{
					CVersionDonnees version = new CVersionDonnees(liste.ContexteDonnee);
					if (version.ReadIfExists((int)nIdVersion))
					{
						int[] lstIds = version.IdsVersionsDependantes;
						StringBuilder bl = new StringBuilder();
						foreach (int nId in lstIds)
						{
							bl.Append(nId);
							bl.Append(',');
						}
						if (bl.Length > 0)
						{
							bl.Remove(bl.Length - 1, 1);
							CListeObjetsDonnees listeFuturs = new CListeObjetsDonnees(liste.ContexteDonnee, liste.TypeObjets, false);
							listeFuturs.Filtre = new CFiltreData(
								CSc2iDataConst.c_champOriginalId + " in (" + strListeIds + ") and " +
								CSc2iDataConst.c_champIdVersion + " in (" + bl.ToString() + ")");
							listeFuturs.Filtre.IgnorerVersionDeContexte = true;
							if (listeFuturs.CountNoLoad != 0)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}




		////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne true si l'élément peut être supprimé
		/// </summary>
		/// <returns></returns>
		public sealed override CResultAErreur CanDelete()
		{
            if (Row.RowState == DataRowState.Deleted)
                return CResultAErreur.True; ;
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( ContexteDonnee, GetType(), false );
			liste.Filtre = new CFiltreData ( GetChampId()+"=@1",Id );
			return CanDelete ( liste );
		}

        public override CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison()
        {
            return DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(false);
        }

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Ajoute la suppression de relationsTypeId qui sont des compositions
		/// </summary>
		/// <returns></returns>
		public override CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(bool bDansContexteCourant)
		{
            if (Row.RowState != DataRowState.Deleted)
            {
                CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, GetType(), false);
                liste.Filtre = new CFiltreData(GetChampId() + "=@1", Id);
                liste.InterditLectureInDB = true;
                return DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(liste, bDansContexteCourant);
            }
            return CResultAErreur.True;
		}

        public static CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(CListeObjetsDonnees liste)
        {
            return DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(liste, false);
        }

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Ajoute la suppression de relationsTypeId qui sont des compositions
		/// </summary>
		/// <returns></returns>
		public static CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison( CListeObjetsDonnees liste, bool bDansContexteCourant )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( liste.Count == 0 )
				return result;

			Type typeElements = liste.TypeObjets;
			int nTailleBloc = 500; //Travaille par bloc de 500 éléments
			int nCount = liste.Count;
			for (int nBloc = 0; nBloc < nCount; nBloc += nTailleBloc)
			{
				StringBuilder blIds = new StringBuilder();
				int nMin = Math.Min(nBloc + nTailleBloc, nCount);
				for (int nElement = nBloc; nElement < nMin; nElement++)
				{
                    if (((CObjetDonneeAIdNumerique)liste[nElement]).IsValide())
                    {
                        blIds.Append(((CObjetDonneeAIdNumerique)liste[nElement]).Id);
                        blIds.Append(',');
                    }
				}
				if (blIds.Length > 0)
				{
					blIds.Remove(blIds.Length - 1, 1);
					{
						string strListeIds = blIds.ToString();
						//Supprime tous les fils de relation TypeId
						foreach (RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds)
						{
							if (relation.Composition)
							{
								Type tpLiens = CContexteDonnee.GetTypeForTable(relation.TableFille);
								if (relation.IsAppliqueToType(typeElements))
								{
									CListeObjetsDonnees listeTypeId = new CListeObjetsDonnees(liste.ContexteDonnee, tpLiens, false);
									CFiltreData filtre = new CFiltreData(
										relation.ChampType + "=@1 and " +
										relation.ChampId + " in (" + strListeIds + ")",
										typeElements.ToString());
									listeTypeId.Filtre = filtre;
                                    listeTypeId.PreserveChanges = true;
									result = DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(listeTypeId, bDansContexteCourant);
									if (!result)
										return result;
								}
							}
						}
					}
				}
			}
			List<CInfoRelation> relationsAMettreANull = new List<CInfoRelation>();
			string strNomTable = CContexteDonnee.GetNomTableForType ( liste.TypeObjets );
			foreach (CInfoRelation info in CContexteDonnee.GetListeRelationsTable(strNomTable))
			{
				if (info.PasserLesFilsANullLorsDeLaSuppression && info.TableParente == strNomTable)
				{
					relationsAMettreANull.Add(info);
				}
			}
			//Met à null ce qui doit l'être
			for ( int nBloc = 0; nBloc < nCount; nBloc += nTailleBloc )
			{
				int nMin = Math.Min ( nBloc+nTailleBloc, nCount );
				StringBuilder bl = new StringBuilder();
				for ( int nElt =nBloc; nElt < nMin; nElt++ )
				{
                    if (((CObjetDonneeAIdNumerique)liste[nElt]).IsValide())
                    {
                        bl.Append(((CObjetDonneeAIdNumerique)liste[nElt]).Id);
                        bl.Append(',');
                    }
				}
				bl.Remove (  bl.Length-1,1 );
                if ( bl.Length > 0 )
                {
                    foreach (CInfoRelation relationToNull in relationsAMettreANull)
                    {
                        CListeObjetsDonnees lstFils = new CListeObjetsDonnees(liste.ContexteDonnee,
                            CContexteDonnee.GetTypeForTable(relationToNull.TableFille));
                        lstFils.Filtre = new CFiltreData(relationToNull.ChampsFille[0] + " in(" +
                            bl.ToString() + ")");
                        lstFils.Filtre.IgnorerVersionDeContexte = true;
                        lstFils.Filtre.IntegrerLesElementsSupprimes = true;
                        lstFils.PreserveChanges = true;
                        foreach (CObjetDonnee fils in lstFils.ToArrayList())
                        {
                            fils.Row.Row.BeginEdit();
                            foreach (string strChamp in relationToNull.ChampsFille)
                                fils.Row.Row[strChamp] = DBNull.Value;
                            fils.Row.Row.EndEdit();
                        }
                    }
				}
			}
			foreach ( CObjetDonneeAIdNumerique objet in liste.ToArrayList() )
			{
				try
				{
                    if (objet.Row.RowState != DataRowState.Deleted && objet.Row.RowState != DataRowState.Detached)
                    {
                        objet.Row.Row.Sc2iDelete();
                    }
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
					return result;
				}
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////
		public override sealed CResultAErreur Delete ( bool bDansContexteCourant )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Row.RowState == DataRowState.Deleted )
				return CResultAErreur.True;
			if (Row.RowState == DataRowState.Detached)
			{
				result.EmpileErreur(I.T("Cannot delete a detached object|191"));
				return result;
			}
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( ContexteDonnee, GetType(), false );
			liste.Filtre = new CFiltreData ( GetChampId()+"=@1",Id );
			liste.Filtre.IgnorerVersionDeContexte = true;
			return Delete( liste, bDansContexteCourant );
		}

		/////////////////////////////////////////////////////////////////
		protected virtual CResultAErreur BeforeDelete()
		{
			return CResultAErreur.True;
		}

        /////////////////////////////////////////////////////////////////
        public static CResultAErreur Delete(CObjetDonneeAIdNumerique[] lst)
        {
            return Delete(lst, false);
        }

		/////////////////////////////////////////////////////////////////
		public static CResultAErreur Delete(CObjetDonneeAIdNumerique[] lst, bool bDansContexteCourant)
		{
			Type tp = null;
			string strIds = "";

			CResultAErreur result = CResultAErreur.True;
			if (lst.Length == 0)
				return result;

			foreach (CObjetDonneeAIdNumerique obj in lst)
			{
				if (tp != null && tp != obj.GetType())
				{
					result.EmpileErreur(I.T("Cannot delete the elements in the list because they are not of the same type|162"));
					return result;
				}
				tp = obj.GetType();
				strIds += obj.Id + ",";
			}
			strIds = strIds.Substring(0, strIds.Length - 1);
			CListeObjetsDonnees liste = new CListeObjetsDonnees(((CObjetDonneeAIdNumerique)lst[0]).ContexteDonnee, tp, false);
			liste.Filtre = new CFiltreData(((CObjetDonneeAIdNumerique)lst[0]).GetChampId() + " in (" + strIds + ")");
			return Delete(liste, bDansContexteCourant);
		}

        /////////////////////////////////////////////////////////////////
        public static CResultAErreur Delete(CListeObjetsDonnees liste)
        {
            return Delete(liste, false);
        }

		/////////////////////////////////////////////////////////////////
		public static CResultAErreur Delete ( CListeObjetsDonnees liste , bool bDansContexteCourant)
		{
			CResultAErreur result = CanDelete ( liste );
			if ( !result )
				return result;

			CContexteDonnee contexte = liste.ContexteDonnee;
			if ( liste.Count == 0 )
				return result;
			bool bEditAuto = !(contexte.IsEnEdition) && !contexte.IsModeDeconnecte && !bDansContexteCourant;
			if ( bEditAuto )
			{
				bEditAuto = contexte.BeginModeDeconnecte();
			}
            try
            {
                foreach (CObjetDonneeAIdNumerique obj in liste.ToArrayList())
                {
                    result = obj.BeforeDelete();
                    if (!result)
                        return result;
                }
                result = DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(liste, bDansContexteCourant);
                if (result)
                {
                    if (bEditAuto)
                    {
                        result = contexte.CommitModifsDeconnecte();
                    }
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Delete error|157"));
            }
            finally
            {
                if (bEditAuto && !result)
                    contexte.CancelModifsEtEndModeDeconnecte();
            }
			return result;
		}


        /// <summary>
        /// Si l'entité n'existe que dans une certaine version de données, indique l'id de cette version.
        /// </summary>
        [DynamicField("Database Version Id", CObjetDonnee.c_categorieChampSystème)]
		public int? IdVersionDatabase
		{
			get
			{
                if (Table.Columns.Contains(CSc2iDataConst.c_champIdVersion))
                    return (int?)Row[CSc2iDataConst.c_champIdVersion, true];
				return null;
			}
			set
			{
				if (Table.Columns.Contains(CSc2iDataConst.c_champIdVersion))
					Row[CSc2iDataConst.c_champIdVersion, true] = value;
			}
		}

		/// <summary>
		/// Indique que l'élément a été supprimé
		/// </summary>
        [DynamicField("Is deleted", CObjetDonnee.c_categorieChampSystème)]
		public bool IsDeleted
		{
			get
			{
				if (Table.Columns.Contains(CSc2iDataConst.c_champIsDeleted))
					return (bool)Row[CSc2iDataConst.c_champIsDeleted];
				return false;
			}
			set
			{
                if (Table.Columns.Contains(CSc2iDataConst.c_champIsDeleted))
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, CSc2iDataConst.c_champIsDeleted, value);
			}
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// Si l'entité n'existe que dans une version de données, indique cette version.
		/// </summary>
        [DynamicField("Database Version", CObjetDonnee.c_categorieChampSystème)]
		public CVersionDonnees VersionDatabase
		{
			get
			{
				if (IdVersionDatabase != null)
				{
					return (CVersionDonnees)GetParent(CSc2iDataConst.c_champIdVersion, typeof(CVersionDonnees));
				}
				return null;
			}
		}

        //-------------------------------------------------------------------------
        /*
        Youcef a ajouté cette prop le 07/07/08 car besoin d'Audilog de tester la version de travail 
        au début d'un process (voir même dans la condition de déclenchement)
        Donc on a l'Id de la version en faisant "Tatget_element.Work_Version_Id"
        La propriété IdVersionDatabase retourne toujours NULL même quand on est dans une Version
        */
        /// <summary>
        /// 
        /// </summary>
        [DynamicField("Work Version Id")]
        public int? IdVersionDeTravail
        {
            get
            {
                if (ContexteDonnee != null)
                    return ContexteDonnee.IdVersionDeTravail;
                return null;
            }
        }



		//-----------------------------------------------------------------------
		//////////////////////////////////////////////////
		public static bool IsUnique(CObjetDonnee objet, string strChamp, string strValeurChamp)
		{
			if (objet == null)
				return true;
			CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(objet.GetChampsId(), objet.Row);
			filtre.Filtre = strChamp + " like @" + (filtre.Parametres.Count + 1) + " and not(" + filtre.Filtre + ")";
			filtre.Parametres.Add(strValeurChamp);
			CListeObjetsDonnees liste = new CListeObjetsDonnees(objet.ContexteDonnee, objet.GetType());
			liste.Filtre = filtre;
			if (liste.CountNoLoad != 0)
				return false;
			return true;
		}

        /////////////////////////////////////////////////////////////////
        public override bool Equals(object obj)
        {
            if (!(obj is CObjetDonneeAIdNumerique))
                return false;
            return this == (CObjetDonneeAIdNumerique)obj;
        }

        /////////////////////////////////////////////////////////////////
        public static bool operator ==(CObjetDonneeAIdNumerique obj1, CObjetDonneeAIdNumerique obj2)
        {
            if (((object)obj1) == null && ((object)obj2) == null)
                return true;
            if (((object)obj1) == null || ((object)obj2) == null)
                return false;
            if (obj1.GetType() != obj2.GetType())
                return false;
            if (obj1.Row.RowState == DataRowState.Deleted || obj2.Row.RowState == DataRowState.Deleted)
                return false;
            return obj1.Id == obj2.Id;
        }

        /////////////////////////////////////////////////////////////////
        public static bool operator !=(CObjetDonneeAIdNumerique obj1, CObjetDonneeAIdNumerique obj2)
        {
            return !(obj1 == obj2);
        }

        /////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            string str = GetType().ToString() + "_" + Id.ToString() ;
            return str.GetHashCode();
        }
	}
}
