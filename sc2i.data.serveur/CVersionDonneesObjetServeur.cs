using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.data;
using System.Data;
using System.Collections;
using sc2i.multitiers.client;


namespace sc2i.data.serveur
{
	public class CVersionDonneesObjetServeur : CObjetServeur, IVersionDonneesObjetServeur
	{
		public CVersionDonneesObjetServeur(int nIdSession)
			: base(nIdSession)
		{

		}

		//------------------------------------------------
		/// <summary>
		/// Pas de journalisation de ces données
		/// </summary>
		public override IJournaliseurDonneesObjet JournaliseurChamps
		{
			get
			{
				return null;
			}
		}

		//------------------------------------------------
		/// <summary>
		/// Travail toujours directement dans la base !
		/// </summary>
		public override int? IdVersionDeTravail
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		//-------------------------------------------------------------------------------
		public override string GetNomTable()
		{
			return CVersionDonneesObjet.c_nomTable;
		}

		//-------------------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			return CResultAErreur.True;
		}

		//-------------------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CVersionDonneesObjet);
		}

		//-------------------------------------------------------------------------------
		public override CResultAErreur SaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexteSauvegarde.ContexteDonnee.Tables[GetNomTable()];
			if (table != null)
			{
				ArrayList lstRows = new ArrayList(table.Rows);
				foreach (DataRow row in lstRows)
				{
					if (row.RowState == DataRowState.Modified)
					{
						CVersionDonneesObjet version = new CVersionDonneesObjet(row);
						version.CancelCreate();
					}
					if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
					{
						CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(row);
						if (versionObjet.CodeTypeOperation == (int)CTypeOperationSurObjet.TypeOperation.Ajout ||
							 versionObjet.CodeTypeOperation == (int)CTypeOperationSurObjet.TypeOperation.Suppression)
						{
							CListeObjetsDonnees datas = versionObjet.ToutesLesOperations;
							/*CListeObjetsDonnees datas = new CListeObjetsDonnees(versionObjet.ContexteDonnee, typeof(CVersionDonneesObjetOperation));
							datas.Filtre = new CFiltreData(CVersionDonneesObjet.c_champId + "=@1 and " +
								CVersionDonneesObjetOperation.c_champOperation += "@2",
								versionObjet.Id,
								(int)versionObjet.CodeTypeOperation);
							datas.InterditLectureInDB = true;*/
							datas.Filtre = new CFiltreData(CVersionDonneesObjetOperation.c_champOperation + "=@1",
								versionObjet.CodeTypeOperation);
							if (datas.Count == 0)
							{
								//Création du data. pour les ajouts et les suppressions,
								//il doit y avoir un data indiquant l'opération
								CVersionDonneesObjetOperation data = new CVersionDonneesObjetOperation(contexteSauvegarde.ContexteDonnee);
								data.CreateNewInCurrentContexte();
								data.VersionObjet = versionObjet;
								data.CodeTypeOperation = versionObjet.CodeTypeOperation;
								data.Champ = null;
							}
						}
					}

				}
			}
			return base.SaveAll(contexteSauvegarde, etatsAPrendreEnCompte);
		}

		//-------------------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result =  base.TraitementAvantSauvegarde(contexte);
			if (!result)
				return result;
			DataTable table = contexte.Tables[GetNomTable()];
			if (table != null)
			{
				ArrayList lstRows = new ArrayList(table.Rows);
				foreach (DataRow row in lstRows)
				{
					if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
					{
						CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(row);
						if (versionObjet.CodeTypeOperation == (int)CTypeOperationSurObjet.TypeOperation.Ajout ||
							 versionObjet.CodeTypeOperation == (int)CTypeOperationSurObjet.TypeOperation.Suppression)
						{
							CListeObjetsDonnees datas = versionObjet.Modifications;
							datas.Filtre = new CFiltreData(CVersionDonneesObjetOperation.c_champOperation + "=@1",
								versionObjet.CodeTypeOperation);
							if (datas.Count == 0)
							{
								//Création du data. pour les ajouts et les suppressions,
								//il doit y avoir un data indiquant l'opération
								CVersionDonneesObjetOperation data = new CVersionDonneesObjetOperation(contexte);
								data.CreateNewInCurrentContexte();
								data.VersionObjet = versionObjet;
								data.CodeTypeOperation = versionObjet.CodeTypeOperation;
								data.Champ = null;
							}
						}
					}
				}
			}
			return result;							
		}

		//-------------------------------------------------------------------------------
		//Annule les modifications réalisées sur l'objet
		public CResultAErreur AnnuleModificationsPrevisionnelles(int nIdVersionObjet)
		{
			CResultAErreur result = CResultAErreur.True;
			using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
			{
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				result = session.BeginTrans();
				if (!result)
					return result;
				CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(contexte);
				if (!versionObjet.ReadIfExists(nIdVersionObjet))
				{
					return result;//Rien à annuler
				}
				CVersionDonnees versionPrev = versionObjet.VersionDonnees;
				if (versionPrev.TypeVersion.Code != CTypeVersion.TypeVersion.Previsionnelle)
				{
					result.EmpileErreur(I.T("Cannot cancel archive operation|220"));
					return result;
				}
				result = AnnuleModificationPrevisionnelle(versionObjet);
				if (result)
				{
					result = contexte.SaveAll(true);
					if (result)
						result = session.CommitTrans();
					else
						result = session.RollbackTrans();
				}
			}
			return result;

		}

		//----------------------------------------------------------
		public CResultAErreur AnnuleModificationPrevisionnelle(CVersionDonneesObjet versionObjet)
		{
			CContexteDonnee contexte = versionObjet.ContexteDonnee;
			CResultAErreur result = CResultAErreur.True;
			int nIdObjet = versionObjet.IdElement;
			Type typeObjet = versionObjet.TypeElement;
			CVersionDonnees versionPrev = versionObjet.VersionDonnees;
			int nIdVersion = versionPrev.Id;
			//Vérifie que l'utilisateur peut travailler avec cette version
			result = versionPrev.EnregistreEvenement(CVersionDonnees.c_eventBeforeUtiliser, true);
			if (!result)
				return result;
			contexte.SetVersionDeTravail(-1, false);



			//Suppression de l'objet associé
			string strPrimKey = contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(typeObjet)).PrimaryKey[0].ColumnName;
			CListeObjetsDonnees listeTmp = new CListeObjetsDonnees(contexte, typeObjet);
			listeTmp.Filtre = new CFiltreData(
				CSc2iDataConst.c_champIdVersion + "=@1 and " +
				"(" + CSc2iDataConst.c_champOriginalId + "=@2 or " +
				strPrimKey + "=@2)",
				nIdVersion,
				nIdObjet);
			listeTmp.Filtre.IgnorerVersionDeContexte = true;
			result = CObjetDonneeAIdNumerique.Delete(listeTmp);
			if (!result)
				return result;

			//Suppression de la version objet
			result = versionObjet.Delete();
			if (!result)
				return result;

			//Force la modification de l'objet pour mettre à jour les versions suivantes
			CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(typeObjet, new object[] { contexte });
			if (objet.ReadIfExists(nIdObjet))
			{
				//Passe dans la version de l'objet
				result = contexte.SetVersionDeTravail(objet.IdVersionDatabase, false);
				if (!result)
					return result;
				objet.Row.Row.SetModified();
			}
			return result;
		}

		
				
	}
}
