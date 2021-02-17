using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using sc2i.common;
using sc2i.data.serveur;
using sc2i.multitiers.client;


namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CRelationElementAChamp_ChampCustomServeur.
	/// </summary>
    [AutoExec("Autoexec")]
	public abstract class CRelationElementAChamp_ChampCustomServeur:CObjetServeur
	{
#if PDA
		public CRelationElementAChamp_ChampCustomServeur()
			:base()
		{
			
		}
#endif
		//-------------------------------------------------------------------
		public CRelationElementAChamp_ChampCustomServeur(int nIdSession)
			:base(nIdSession)
		{
			
		}

        private static bool m_bAutoexecRun = false;
        public static void Autoexec()
        {
            if ( m_bAutoexecRun )
                return;
            m_bAutoexecRun = true;
            CObjetServeur.BeforeSaveExterne += new BeforeOrAfterSaveExterneDelegate(BeforeSaveExterneChampCustom);
            CObjetServeur.AfterSaveExterne += new BeforeOrAfterSaveExterneDelegate(AfterSaveExterneChampCustom);
        }

		//-------------------------------------------------------------------
		public override IJournaliseurDonneesObjet JournaliseurChamps
		{
			get
			{
                if (typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
                    return null;
				return new CJournaliseurValeursChampsCustom();
			}
		}

		//-------------------------------------------------------------------
		public static CResultAErreur VerifieDonneesRelation(CRelationElementAChamp_ChampCustom relation)
		{
			CResultAErreur result = CResultAErreur.True;
			if (relation.ChampCustom == null)
				result.EmpileErreur(I.T("The field cannot be null|136"));
			if (!result)
				return result;
			result = relation.ChampCustom.IsCorrectValue(relation.Valeur);
			if (!result)
				result.EmpileErreur(I.T("The field value '@1' is incorrect|137", relation.ChampCustom.Nom));
			return result;
		}

		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationElementAChamp_ChampCustom relation = (CRelationElementAChamp_ChampCustom)objet;
				result = VerifieDonneesRelation(relation);
				
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = base.TraitementAvantSauvegarde(contexte);

			CSessionClient session = CSessionClient.GetSessionForIdSession ( IdSession );
			//Annule les modifications sur les champs interdits en modification
			DataTable table = contexte.Tables[GetNomTable()];
			//Objet témoin pour vérifier les valeurs non affectées
			CRelationElementAChamp_ChampCustom relTemoin = null;

			if (IdVersionDeTravail != null)//Dans les versions, on ne stocke pas les créations de valeurs nulles
			{
				relTemoin = (CRelationElementAChamp_ChampCustom)contexte.GetNewObjetForTable(table);
				relTemoin.CreateNewInCurrentContexte();
			}
			if ( table != null && session != null)
			{
				IInfoUtilisateur user = session.GetInfoUtilisateur();
				if ( user != null )
				{
					ArrayList lst = new ArrayList( table.Rows );
					IElementAChamps lastElt = null;
                    CRestrictionUtilisateurSurType restDeBase = null;
					CRestrictionUtilisateurSurType restAppliquee = null;
					foreach ( DataRow row in lst )
					{
						CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)contexte.GetNewObjetForRow ( row );
						if (row.RowState == DataRowState.Added && relTemoin != null)
						{
							if (relTemoin.Valeur != null && relTemoin.Valeur.Equals(rel.Valeur) ||
								 relTemoin.Valeur == null && relTemoin.Valeur == null)
							{
								rel.CancelCreate();
							}
						}
                        

						if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added )
						{
                            if (restDeBase == null)
                            {
                                Type tp = rel.ElementAChamps.GetType();
                                restDeBase = user.GetRestrictionsSur(tp, contexte.IdVersionDeTravail);
                            }
                            if (restDeBase != null && rel.ElementAChamps.Equals(lastElt))
                                restAppliquee = restDeBase.Clone() as CRestrictionUtilisateurSurType;
							//Vérifie qu'on a le droit
                            if (!rel.ElementAChamps.Equals(lastElt) && restAppliquee != null)
                            {
                                restAppliquee.ApplyToObjet(rel.ElementAChamps);
                            }
							lastElt = rel.ElementAChamps;
							if (restAppliquee != null)
							{
								ERestriction restChamp = restAppliquee.GetRestriction(rel.ChampCustom.CleRestriction);
								if ((restChamp & ERestriction.ReadOnly) == ERestriction.ReadOnly)
									row.RejectChanges();
							}
						}
					}
				}
			}
			if (relTemoin != null)
				relTemoin.CancelCreate();
			return result;
		}

		//----------------------------------------------------------
		public override CResultAErreur BeforeSave(CContexteSauvegardeObjetsDonnees contexte, IDataAdapter adapter, DataRowState etatsAPrendreEnCompte)
		{
			CResultAErreur result = base.BeforeSave(contexte, adapter, etatsAPrendreEnCompte );
			if (!result)
				return result;
			if ( IdVersionDeTravail == null || (etatsAPrendreEnCompte & DataRowState.Added) != DataRowState.Added )
				return result;

			//Si on affecte un champ dans une version, il faut qu'il soit affecté dans le référentiel,
			//sinon, on n'a pas d'id original pour le CRelationElementAChamp_ChampCustom
			DataTable table = contexte.ContexteDonnee.Tables[GetNomTable()];
			if (table == null)
				return result;

			Dictionary<DataRow, DataRow> mapRowToRowOriginale = new Dictionary<DataRow,DataRow>();
			
			ArrayList lstRows = new ArrayList(table.Rows);
			foreach (DataRow row in lstRows)
			{
				if (row.RowState == DataRowState.Added && row[CSc2iDataConst.c_champIdVersion] != DBNull.Value)
				{
					if (row[CSc2iDataConst.c_champOriginalId] == DBNull.Value)
					{
						//Le champ n'a pas de valeur dans le référentiel, lui affecte la valeur
						//par défault, sauf si l'élément parent n'appartient pas au référentiel,
						//il faut trouver à quelle version appartient l'élément parent
						int? nIdVersionCreationDuParent = null;
						CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)contexte.ContexteDonnee.GetNewObjetForRow(row);
						CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)rel.ElementAChamps;
						nIdVersionCreationDuParent = (int?)objet.Row[CSc2iDataConst.c_champIdVersion, true];
						if ((int)row[CSc2iDataConst.c_champIdVersion] != nIdVersionCreationDuParent)
						{
							CRelationElementAChamp_ChampCustom newRel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(GetTypeObjets(), contexte.ContexteDonnee);
							newRel.CreateNewInCurrentContexte();
							newRel.ElementAChamps = rel.ElementAChamps;
							newRel.ChampCustom = rel.ChampCustom;
							newRel.Valeur = rel.ChampCustom.ValeurParDefaut;
							newRel.IdVersionDatabase = nIdVersionCreationDuParent;
							mapRowToRowOriginale[rel.Row.Row] = newRel.Row.Row;
						}
					}
				}
			}
			//Stocke le mapRow dans le contexte, pour s'en resservir après update
			contexte.ContexteDonnee.ExtendedProperties[GetType().ToString()+"_MAPORG"] = mapRowToRowOriginale;
			return result;
		}

		//----------------------------------------------------------
		public override CResultAErreur AfterSave(CContexteSauvegardeObjetsDonnees contexte, IDataAdapter adapter, DataRowState etatsAPrendreEnCompte)
		{
			CResultAErreur result = base.AfterSave(contexte, adapter, etatsAPrendreEnCompte);
			if (!result || IdVersionDeTravail == null || (etatsAPrendreEnCompte & DataRowState.Added )!= DataRowState.Added )
				return result;
			Dictionary<DataRow, DataRow> mapRowToRowOriginale = (Dictionary<DataRow, DataRow>)contexte.ContexteDonnee.ExtendedProperties[GetType().ToString()+"_MAPORG"];
			if (mapRowToRowOriginale != null && mapRowToRowOriginale.Count > 0)
			{
				string strPrimKey = "";
				foreach (KeyValuePair<DataRow, DataRow> pair in mapRowToRowOriginale)
				{
					DataRow rowVersion = pair.Key;
					DataRow rowRef = pair.Value;
					if (strPrimKey.Length == 0)
						strPrimKey = rowVersion.Table.PrimaryKey[0].ColumnName;
					rowVersion[CSc2iDataConst.c_champOriginalId] = rowRef[strPrimKey];
				}
				adapter.Update(contexte.ContexteDonnee);
			}
			contexte.ContexteDonnee.ExtendedProperties.Remove(GetType().ToString()+"_MAPORG");
			return result;
		}

        //----------------------------------------------------------
        /// <summary>
        /// Stock les anciens ids provisoire des éléments anciens
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableParametres"></param>
        /// <param name="result"></param>
        public static void BeforeSaveExterneChampCustom(DataTable table, Hashtable tableParametres, ref CResultAErreur result)
        {
            if ( !result )
                return;
            //stock la liste des ids nouveaux qu'il faut remapper après
            CContexteDonnee contexte = (CContexteDonnee)table.DataSet;
            Type tpObjetsSauves = CContexteDonnee.GetTypeForTable(table.TableName);
            if (!typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpObjetsSauves))
                return ;
            Dictionary<DataRow, int> dicElementsNouveaux = new Dictionary<DataRow, int>();
            if (table.PrimaryKey.Length > 0)
            {
                string strPrimKey = table.PrimaryKey[0].ColumnName;
                foreach (DataRow row in table.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                        dicElementsNouveaux[row] = (int)row[strPrimKey];
                }
                tableParametres[typeof(CRelationElementAChamp_ChampCustomServeur).ToString()] = dicElementsNouveaux;
            }
        }

        /// <summary>
        /// Traite les ajouts d'éléments avec des valeurs de champs customs
        /// associés aux nouveaux éléments
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableParametres"></param>
        /// <param name="result"></param>
        public static void AfterSaveExterneChampCustom(DataTable table, Hashtable tableParametres, ref CResultAErreur result)
        {
            if (!result)
                return;
            CContexteDonnee contexte = (CContexteDonnee)table.DataSet;
            Dictionary<DataRow, int> dicElementsNouveaux = tableParametres[typeof(CRelationElementAChamp_ChampCustomServeur).ToString()] as Dictionary<DataRow, int>;
            if ( dicElementsNouveaux == null || dicElementsNouveaux.Count == 0 )
                return;
            Type tp = CContexteDonnee.GetTypeForTable ( table.TableName );
            string strPrimKey = table.PrimaryKey[0].ColumnName;
            //Trouve tous les types qui implémentent CRelationElementAChamp_ChampCustom
            CInfoClasseDynamique[] types = DynamicClassAttribute.GetAllDynamicClassHeritant(typeof(CRelationElementAChamp_ChampCustom));
            foreach ( CInfoClasseDynamique infoClasse in types )
            {
                string strNomTable = CContexteDonnee.GetNomTableForType(infoClasse.Classe);
                DataTable tableValeurs = contexte.Tables[strNomTable];
                if ( tableValeurs != null )
                {
                    foreach ( KeyValuePair<DataRow, int> rowToId in dicElementsNouveaux )
                    {
                    DataRow[] rowsValeurs = tableValeurs.Select ( CRelationElementAChamp_ChampCustom.c_champValeurString+"='"+
                        tp.ToString()+"' and "+
                        CRelationElementAChamp_ChampCustom.c_champValeurInt+"="+
                        rowToId.Value );
                        foreach ( DataRow rowValeur in rowsValeurs )
                            rowValeur[CRelationElementAChamp_ChampCustom.c_champValeurInt] = rowToId.Key[strPrimKey];
                    }
                }
            }
        }

	}
}
