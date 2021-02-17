using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using sc2i.common;
using System.Collections;

namespace sc2i.data.dynamic
{

	public class CJournaliseurValeursChampsCustom : IJournaliseurDonneesObjet
	{
		//-------------------------------------------------------------------------
		public CVersionDonneesObjet JournaliseDonnees(System.Data.DataRow row, CVersionDonnees version)
		{
			if (row.RowState == DataRowState.Modified ||
				row.RowState == DataRowState.Added ||
				row.RowState == DataRowState.Deleted )
			{
				Type tp = CContexteDonnee.GetTypeForTable(row.Table.TableName);
				if (!typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom ( tp ))
				{
					return null;
				}
				//Récupère l'objet associé
				DataRowVersion rowVersion = DataRowVersion.Current;
				if (row.RowState == DataRowState.Deleted)
				{
					if (row.HasVersion(DataRowVersion.Original))
						rowVersion = DataRowVersion.Original;
					else
						return null;
				}
				CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(tp, new object[] { row });
				rel.VersionToReturn = rowVersion;
				try
				{
					IElementAChamps objet = rel.ElementAChamps;
					//Si l'objet est ajouté, on n'archive pas !
					if (objet is CObjetDonneeAIdNumerique && 
						((CObjetDonneeAIdNumerique) objet).Row.RowState != DataRowState.Added)
					{
						CObjetDonneeAIdNumerique objetAId = (CObjetDonneeAIdNumerique)objet;
						if (objetAId != null)
						{
							//Récupère un version pour l'objet
							CVersionDonneesObjet versionObjet = version.GetVersionObjetAvecCreation(objetAId.Row.Row);
							if (versionObjet == null)
								return null;
							
							//Stef 14082008 : en cas d'ajout, on ne fait rien. le datarow de l'objet n'est pas added
							//car l'objet a déjà été sauvé quand on passe par là !
							if (versionObjet.CodeTypeOperation == (int)CTypeOperationSurObjet.TypeOperation.Ajout)
								return null;

							new CJournaliseurValeurChampCustom().JournaliseDonneeInContexte(versionObjet, rel.ChampCustom, row);
							return versionObjet;
						}
					}
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		//---------------------------------------------
		/// <summary>
		/// Retourne un dictionnaire contenant tous les champs modifiés pour
		/// des versions données et des objets donnés
		/// </summary>
		/// <returns></returns>
		public Dictionary<int,Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> GetDictionnaireChampsModifies(
			int nIdSession,
			string strIdsVersionsConcernees,
			Type typeElements,
			DataRow[] rowsConcernees)
		{
			
			if ( !typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom ( typeElements ) )
				return null;

			if ( rowsConcernees.Length == 0 )
				return new Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>>();
			
			//Récupère le type des éléments à champs
			CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance ( typeElements, new object[]{rowsConcernees[0]} );
			Type typeElementsAChamps = rel.GetTypeElementAChamps();

			//Récupère la clé des éléments à champ
			string strPrimKey = rel.ContexteDonnee.GetTableSafe(CContexteDonnee.GetNomTableForType(typeElementsAChamps)).PrimaryKey[0].ColumnName;


			//Crée la liste des ids pour lesquelles on cherche les valeurs
			Hashtable tableIds = new Hashtable();
			foreach (DataRow row in rowsConcernees)
			{
				rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(typeElements, new object[] { rowsConcernees[0] });
				CObjetDonnee element = (CObjetDonnee)rel.ElementAChamps;
				if (element.Row[CSc2iDataConst.c_champOriginalId] != DBNull.Value)
					tableIds[element.Row[CSc2iDataConst.c_champOriginalId]] = true;
				else
					tableIds[row[strPrimKey]] = true;
			}

			StringBuilder blIds = new StringBuilder();
			foreach ( int nId in tableIds.Keys )
			{
				blIds.Append ( nId.ToString() );
				blIds.Append(",");
			}
			blIds.Remove(blIds.Length-1, 1);				


			C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
			requete.TableInterrogee = CVersionDonneesObjetOperation.c_nomTable;
			requete.ListeChamps.Add(new C2iChampDeRequete("Id",
				new CSourceDeChampDeRequete(CVersionDonneesObjet.c_nomTable + "." + CVersionDonneesObjet.c_champIdElement),
				typeof(int),
				OperationsAgregation.None,
				true));
			requete.ListeChamps.Add(new C2iChampDeRequete("Field",
				new CSourceDeChampDeRequete(CVersionDonneesObjetOperation.c_champChamp),
				typeof(string),
				OperationsAgregation.None,
				true));
			requete.ListeChamps.Add(new C2iChampDeRequete("FieldType",
				new CSourceDeChampDeRequete(CVersionDonneesObjetOperation.c_champTypeChamp),
				typeof(string),
				OperationsAgregation.None,
				true));
			requete.ListeChamps.Add(new C2iChampDeRequete("IdVersion",
				new CSourceDeChampDeRequete(CVersionDonneesObjet.c_nomTable + "." + CVersionDonnees.c_champId),
				typeof(int),
				OperationsAgregation.None,
				true));
			CFiltreData filtre = new CFiltreDataAvance(CVersionDonneesObjetOperation.c_nomTable,
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonnees.c_champId + " in (" + strIdsVersionsConcernees + ") and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonneesObjet.c_champIdElement + " in (" + blIds + ") and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonneesObjet.c_champTypeElement + "=@1 and "+
				CVersionDonneesObjetOperation.c_champTypeChamp+"=@2",
				typeElementsAChamps.ToString(),
				CChampCustomPourVersion.c_typeChamp);
			filtre.IgnorerVersionDeContexte = true;
			requete.FiltreAAppliquer = filtre;

			Dictionary<int,Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> dicoChampsModifies = new Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>>();
			CResultAErreur result = requete.ExecuteRequete(nIdSession);
			if (!result)
				return null;
			foreach (DataRow rowModif in ((DataTable)result.Data).Rows)
			{
				int nIdVersion = (int)rowModif["IdVersion"];
				Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoChampsDeVersion = null;
				if (!dicoChampsModifies.TryGetValue(nIdVersion, out dicoChampsDeVersion))
				{
					dicoChampsDeVersion = new Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>();
					dicoChampsModifies[nIdVersion] = dicoChampsDeVersion;
				}
				Dictionary<CReferenceChampPourVersion, bool> dicoChampsDeId = new Dictionary<CReferenceChampPourVersion, bool>();
				if (!dicoChampsDeVersion.TryGetValue((int)rowModif["Id"], out dicoChampsDeId))
				{
					dicoChampsDeId = new Dictionary<CReferenceChampPourVersion, bool>();
					dicoChampsDeVersion[(int)rowModif["Id"]] = dicoChampsDeId;
				}
				dicoChampsDeId[
					new CReferenceChampPourVersion((string)rowModif["FieldType"], (string)rowModif["Field"])] = true;
			}
			return dicoChampsModifies;

		}

		//-------------------------------------------------------------------------
		public void RepercuteModifsSurVersionFuture(
			DataRow rowReference, 
			DataRow rowCopie, 
			Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoDesChampsModifiesParId)
		{
			Type tp = CContexteDonnee.GetTypeForTable(rowReference.Table.TableName);
			if (!typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
			{
				return;
			}
			CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(tp, new object[] { rowReference });
			int nId = rel.ElementAChamps.Id;
			Dictionary<CReferenceChampPourVersion, bool> dicoDesChampsModifies = null;
			dicoDesChampsModifiesParId.TryGetValue(nId, out dicoDesChampsModifies);
			if (dicoDesChampsModifies == null)
				dicoDesChampsModifies = new Dictionary<CReferenceChampPourVersion, bool>();
			CReferenceChampPourVersion refChamp = new CReferenceChampPourVersion(CChampCustomPourVersion.c_typeChamp, rel.Row[CChampCustom.c_champId].ToString());
			if (!dicoDesChampsModifies.ContainsKey(refChamp))
			{
				string[] strChamps = new string[] {
				CRelationElementAChamp_ChampCustom.c_champValeurBool,
				CRelationElementAChamp_ChampCustom.c_champValeurDate,
				CRelationElementAChamp_ChampCustom.c_champValeurDouble,
				CRelationElementAChamp_ChampCustom.c_champValeurInt,
				CRelationElementAChamp_ChampCustom.c_champValeurNull,
				CRelationElementAChamp_ChampCustom.c_champValeurString};
				//Répercute la valeur
				foreach (string strChamp in strChamps)
					rowCopie[strChamp] = rowReference[strChamp];
			}
		}

		public bool IsVersionObjetLinkToElement
		{
			get { return false; }
		}

		public DataRow GetRowObjetPourDataVersionObject(DataRow row)
		{
			Type tp = CContexteDonnee.GetTypeForTable(row.Table.TableName);
			if (!typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom ( tp ))
			{
				return null;
			}
			//Récupère l'objet associé
			DataRowVersion rowVersion = DataRowVersion.Current;
			if (row.RowState == DataRowState.Deleted)
			{
				if (row.HasVersion(DataRowVersion.Original))
					rowVersion = DataRowVersion.Original;
				else
					return null;
			}
			CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(tp, new object[] { row });
			rel.VersionToReturn = rowVersion;
			try
			{
				IElementAChamps objet = rel.ElementAChamps;
				return ((CObjetDonnee)objet).Row.Row;
			}
			catch
			{
				return null;
			}
		}
	}

}
