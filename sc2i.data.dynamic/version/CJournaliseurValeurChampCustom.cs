using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using sc2i.common;
using System.Collections;
using sc2i.expression;

namespace sc2i.data.dynamic
{

	[AutoExec("RegisterJournaliseur")]
	public class CJournaliseurValeurChampCustom : IJournaliseurDonneesChamp
	{
		public static void RegisterJournaliseur()
		{
			CGestionnaireAChampPourVersion.RegisterJournaliseur(new CJournaliseurValeurChampCustom());
		}

		public string TypeChamp
		{
			get { return CChampCustomPourVersion.c_typeChamp; }
		}

		//---------------------------------------------------------------
		public IChampPourVersion GetChamp(IElementAChampPourVersion element)
		{
			if (element.TypeChamp == CChampCustomPourVersion.c_typeChamp)
			{
				try
				{
					int nIdChamp = Int32.Parse(element.FieldKey);
					CChampCustom champ = new CChampCustom(element.ContexteDonnee);
					if (champ.ReadIfExists(nIdChamp))
						return new CChampCustomPourVersion(champ);
				}
				catch { }
			}
			return null;
		}

		//---------------------------------------------------------------
		public CVersionDonneesObjetOperation JournaliseDonneeInContexte(CVersionDonneesObjet version, CChampCustom champ, DataRow row)
		{
			if (row.RowState != DataRowState.Modified && row.RowState != DataRowState.Added && row.RowState != DataRowState.Deleted)
				return null;
			Type tp = CContexteDonnee.GetTypeForTable(row.Table.TableName);
			if (!typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
				return null;
			CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(tp, new object[] { row });
			object valeurAvant = DBNull.Value;
			object valeurApres = DBNull.Value;

			if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
			{
				if (row.HasVersion(DataRowVersion.Original))
				{
					rel.VersionToReturn = DataRowVersion.Original;
					valeurAvant = rel.ValeurInt;
					rel.VersionToReturn = DataRowVersion.Current;
				}
				if (row.HasVersion(DataRowVersion.Current))
				{
					valeurApres = rel.ValeurInt;
				}
			}
			else
			{
				valeurAvant = DBNull.Value;
				if (row.HasVersion(DataRowVersion.Original))
				{
					rel.VersionToReturn = DataRowVersion.Original;
					valeurAvant = rel.Valeur;
					rel.VersionToReturn = DataRowVersion.Current;
				}
				if (row.HasVersion(DataRowVersion.Current))
					valeurApres = rel.Valeur;
			}

			CVersionDonneesObjetOperation verData = null;
			///Stef optim 13082008 : ne passe plus par une liste pour trouver l'objet
			//Ancien code
			/*
			CListeObjetsDonnees datas = version.Modifications;
			datas.Filtre = new CFiltreData(CVersionDonneesObjetOperation.c_champTypeChamp + "=@1 and " +
				CVersionDonneesObjetOperation.c_champChamp + "=@2",
				CChampCustomPourVersion.c_typeChamp,
				champ.Id.ToString());
			datas.InterditLectureInDB = true;
			
			if (datas.Count != 0)
				verData = (CVersionDonneesObjetOperation)datas[0];
			 * */
			//Nouveau code
			object dummy = null;
			if ( ! version.IsDependanceChargee ( CVersionDonneesObjetOperation.c_nomTable, CVersionDonneesObjet.c_champId ))
				dummy = version.Modifications;
			DataTable table = version.ContexteDonnee.GetTableSafe ( CVersionDonneesObjetOperation.c_nomTable );
			StringBuilder bl = new StringBuilder();
			bl.Append(CVersionDonneesObjet.c_champId);
			bl.Append("=");
			bl.Append(version.Id);
			bl.Append(" and ");
			bl.Append(CVersionDonneesObjetOperation.c_champTypeChamp);
			bl.Append("='");
			bl.Append(CChampCustomPourVersion.c_typeChamp);
			bl.Append("' and ");
			bl.Append(CVersionDonneesObjetOperation.c_champChamp);
			bl.Append("='");
			bl.Append(champ.Id);
			bl.Append("'");
			DataRow[] rows = table.Select ( bl.ToString() );
			if ( rows.Length > 0 )
				verData = new CVersionDonneesObjetOperation ( rows[0] );
			//fin du nouveau code
			else
			{
				verData = new CVersionDonneesObjetOperation(version.ContexteDonnee);
				verData.CreateNewInCurrentContexte();
				verData.VersionObjet = version;
				verData.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
			}
			verData.FieldKey = champ.Id.ToString();

			//Pour les prévisionnelles, stocke la nouvelle valeur
			if (version.VersionDonnees.TypeVersion.Code == CTypeVersion.TypeVersion.Previsionnelle)
				verData.SetValeurStd(valeurApres);
			else
				//Pour les archives, stocke l'ancienne valeur
				verData.SetValeurStd(valeurAvant);
			verData.TypeChamp = CChampCustomPourVersion.c_typeChamp;
			if (version != null && version.TypeOperation == CTypeOperationSurObjet.TypeOperation.Aucune)
				version.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
			return verData;

		}

		//---------------------------------------------------------------
		public object GetValeur(CVersionDonneesObjetOperation version)
		{
			IChampPourVersion champ = GetChamp(version);
			if (champ is CChampCustomPourVersion)
			{
				CChampCustom champCustom = ((CChampCustomPourVersion)champ).ChampCustom;
				object valeur = version.GetValeurStd();
				if (champCustom.TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
				{
					Type tp = champCustom.TypeObjetDonnee;
					if (tp != null && valeur is int)
					{
						CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { version.ContexteDonnee });
						if (objet != null && objet.ReadIfExists((int)valeur))
							return objet;
					}
					return null;
				}
				return valeur;
			}
			return null;
		}

		public object GetValeur(CObjetDonneeAIdNumerique obj, IChampPourVersion champ)
		{
			CChampCustomPourVersion champCustom = (CChampCustomPourVersion)champ;
			return ((IElementAChamps)obj).GetValeurChamp(champCustom.ChampCustom.DbKey.StringValue);
		}

		//---------------------------------------------------------------
		public CResultAErreur AppliqueValeur(int? nIdVersion, IChampPourVersion champ, CObjetDonneeAIdNumerique objet, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!(champ is CChampCustomPourVersion))
			{
				result.EmpileErreur(I.T("Cannot apply value for field @1 (bad type)|243", champ.NomConvivial));
				return result;
			}
			CChampCustom champCustom = ((CChampCustomPourVersion)champ).ChampCustom;
			try
			{
				if (objet is IElementAVariables)
				{
					((IElementAVariables)objet).SetValeurChamp(champCustom.DbKey.StringValue, valeur);
					return result;
				}

			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			result.EmpileErreur(I.T("Cannot apply value for field @1 (bad destination)|244", champ.NomConvivial));
			return result;
		}



		#region IJournaliseurDonneesChamp Membres


		public List<IChampPourVersion> GetChampsJournalisables(CObjetDonneeAIdNumerique objet)
		{
			List<IChampPourVersion> champs = new List<IChampPourVersion>();

			if (objet is IElementAChamps)
			{
				IElementAChamps eleAChamps = (IElementAChamps)objet;
				IDefinisseurChampCustom[] definisseurs = eleAChamps.DefinisseursDeChamps;
				Dictionary<CChampCustom, bool> dicChamps = new Dictionary<CChampCustom,bool>();
				if (definisseurs != null)
				{
					foreach (IDefinisseurChampCustom definisseur in definisseurs)
					{
						foreach (CChampCustom champ in definisseur.TousLesChampsAssocies)
							dicChamps[champ] = true;
					}
				}
				foreach (CChampCustom champ in dicChamps.Keys)
				{
					champs.Add(new CChampCustomPourVersion(champ));
				}
			}

			return champs;
		}

		#endregion
	}
}
