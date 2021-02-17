using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Journalise les valeurs des champs en base d'un objet
	/// </summary>
	public class CJournaliseurVersionObjetChampsDb : IJournaliseurDonneesObjet
	{

		//Journalise les données d'un élement
		public CVersionDonneesObjet JournaliseDonnees(System.Data.DataRow row, CVersionDonnees version)
		{
			CVersionDonneesObjet versionObjet = null;
			if (row.RowState != DataRowState.Added && 
				row.RowState != DataRowState.Modified &&
				row.RowState != DataRowState.Deleted)
				return null;
			Type typeElement = CContexteDonnee.GetTypeForTable(row.Table.TableName);
			versionObjet = version.GetVersionObjetAvecCreation(row);
			if (versionObjet == null)
				return null;
			if (row.HasVersion (DataRowVersion.Original ) )
			{
				CStructureTable structure = CStructureTable.GetStructure(typeElement);

				foreach (CInfoChampTable info in structure.Champs)
				{
					if (info.m_bIsInDB)
					{
						new CJournaliseurChampDb().JournaliseDonneeInContexte(versionObjet, info.NomChamp, row);
					}
				}
			}
			return versionObjet;
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
			DataRow[] rows)
		{
			CResultAErreur result = CResultAErreur.True;
			if (rows.Length == 0)
				return new Dictionary<int,Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>>();

			//Crée la liste des ids
			string strPrimKey = rows[0].Table.PrimaryKey[0].ColumnName;
			StringBuilder blIds = new StringBuilder();
			foreach (DataRow row in rows)
			{
				//Les modifications portent toujours sur l'élément le plus proche du référentiel, 
				//Donc sur l'original Id ou l'id s'il n'y a pas d'original
				if (row[CSc2iDataConst.c_champOriginalId] == DBNull.Value)
					blIds.Append(row[strPrimKey].ToString());
				else
					blIds.Append(row[CSc2iDataConst.c_champOriginalId].ToString());
				blIds.Append(",");
			}
			blIds.Remove(blIds.Length - 1, 1);

			C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
			requete.TableInterrogee = CVersionDonneesObjetOperation.c_nomTable;
			requete.ListeChamps.Add(new C2iChampDeRequete("Id",
				new CSourceDeChampDeRequete(CVersionDonneesObjet.c_nomTable + "." + CVersionDonneesObjet.c_champIdElement),
				typeof(int),
				OperationsAgregation.None,
				true));
			requete.ListeChamps.Add(new C2iChampDeRequete("IdVersion",
				new CSourceDeChampDeRequete(CVersionDonneesObjet.c_nomTable + "." + CVersionDonnees.c_champId),
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
			CFiltreData filtre = new CFiltreDataAvance(CVersionDonneesObjetOperation.c_nomTable,
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonnees.c_champId + " in (" + strIdsVersionsConcernees + ") and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonneesObjet.c_champIdElement + " in (" + blIds.ToString() + ") and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonneesObjet.c_champTypeElement + "=@1 and "+
				CVersionDonneesObjetOperation.c_champTypeChamp+"=@2",
				typeElements.ToString(),
				CChampPourVersionInDb.c_TypeChamp);
			filtre.IgnorerVersionDeContexte = true;
			requete.FiltreAAppliquer = filtre;


			Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> dicoChampsModifies = new Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>>();
			result = requete.ExecuteRequete(nIdSession);
			if (!result)
                return dicoChampsModifies;
			foreach (DataRow rowModif in ((DataTable)result.Data).Rows)
			{
				int nIdVersion = (int)rowModif["IdVersion"];
				Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoChampsDeVersion = null;
				if (!dicoChampsModifies.TryGetValue(nIdVersion, out dicoChampsDeVersion))
				{
					dicoChampsDeVersion = new Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>();
					dicoChampsModifies[nIdVersion] = dicoChampsDeVersion;
				}

				Dictionary<CReferenceChampPourVersion, bool> dicoChampsDeId = null;
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

		/// <summary>
		/// Répercute les modifications d'un élément sur une copie prévisionnelle de cet élément
		/// en ne modifiant pas les champs modifiés par la version
		/// </summary>
		/// <param name="rowReference"></param>
		/// <param name="rowCopie"></param>
		/// <param name="dicoDesChampsModifies"></param>
		public void RepercuteModifsSurVersionFuture(
			DataRow rowReference, 
			DataRow rowCopie, 
			Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoDesChampsModifiesParId)
		{
			Dictionary<CReferenceChampPourVersion, bool> dicoDesChampsModifies = null;
			dicoDesChampsModifiesParId.TryGetValue((int)rowReference[rowReference.Table.PrimaryKey[0]], out dicoDesChampsModifies);
			if (dicoDesChampsModifies == null)
				dicoDesChampsModifies = new Dictionary<CReferenceChampPourVersion, bool>();
			foreach (DataColumn col in rowReference.Table.Columns)
			{
				if (col.ColumnName != rowReference.Table.PrimaryKey[0].ColumnName &&
					col.ColumnName != CSc2iDataConst.c_champIdVersion &&
					col.ColumnName != CSc2iDataConst.c_champOriginalId && 
					col.ColumnName != CSc2iDataConst.c_champIsDeleted
					&& rowCopie.Table.Columns.Contains(col.ColumnName))
				{
					CReferenceChampPourVersion refChamp = new CReferenceChampPourVersion(CChampPourVersionInDb.c_TypeChamp, col.ColumnName);
					if (!dicoDesChampsModifies.ContainsKey(refChamp))
						rowCopie[col.ColumnName] = rowReference[col.ColumnName];
				}
			}
		}

		public bool IsVersionObjetLinkToElement
		{
			get { return true; }
		}

		public DataRow GetRowObjetPourDataVersionObject(DataRow row)
		{
			return row;
		}

	}
}
