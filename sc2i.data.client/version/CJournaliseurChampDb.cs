using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using sc2i.common;

namespace sc2i.data
{


	/// <summary>
	/// Implémente l'interface IJournaliseurChamps pour journaliser les champs
	/// de la base de données (TableField)
	/// </summary>
	[AutoExec("RegisterJournaliseur")]
	public class CJournaliseurChampDb : IJournaliseurDonneesChamp
	{
		public static void RegisterJournaliseur()
		{
			CGestionnaireAChampPourVersion.RegisterJournaliseur(new CJournaliseurChampDb());
		}

		public string TypeChamp
		{
			get { return CChampPourVersionInDb.c_TypeChamp; }
		}

		//---------------------------------------------------------------------
		/// <summary>
		/// Retourne le libellé d'un champ
		/// </summary>
		/// <param name="version"></param>
		/// <returns></returns>
		public IChampPourVersion GetChamp(IElementAChampPourVersion element)
		{
			if (element == null)
				return null;
			Type tp = element.TypeEntite;
			return new CChampPourVersionInDb(element.FieldKey, element.NomChampConvivial);
			//if (tp != null)
			//{
			//    //Cherche la propriété liée au champ
			//    foreach (PropertyInfo prop in tp.GetProperties())
			//    {
			//        object[] attribs = prop.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
			//        if (attribs.Length > 0)
			//        {
			//            string strNomChamp = ((TableFieldPropertyAttribute)attribs[0]).NomChamp;
			//            if (strNomChamp == element.FieldKey)
			//            {
			//                attribs = prop.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
			//                if (attribs.Length != 0)
			//                    return new CChampPourVersionInDb(element.FieldKey, ((DynamicFieldAttribute)attribs[0]).NomConvivial);
			//            }
			//        }
			//    }
			//}
			//return new CChampPourVersionInDb(element.FieldKey, element.FieldKey);
		}

		//---------------------------------------------------------------------
		public CVersionDonneesObjetOperation JournaliseDonneeInContexte(
			CVersionDonneesObjet version,
			string strChampDeTable,
			DataRow row)
		{
			if (version != null && row.Table.Columns.Contains(strChampDeTable))
			{
				object valeurAvant = DBNull.Value;
				object valeurApres = DBNull.Value;
				if (row.HasVersion(DataRowVersion.Original))
					valeurAvant = row[strChampDeTable, DataRowVersion.Original];
				if (row.HasVersion(DataRowVersion.Current))
					valeurApres = row[strChampDeTable, DataRowVersion.Current];
				if (!valeurAvant.Equals(valeurApres))
				{
					if (version.VersionDonnees.TypeVersion.Code == CTypeVersion.TypeVersion.Previsionnelle)
						JournaliseValeur(version, strChampDeTable, valeurApres);
					else
						JournaliseValeur(version, strChampDeTable, valeurAvant);

				}
			}
			return null;
		}

		//-------------------------------------------------------------------------------
		public CVersionDonneesObjetOperation JournaliseValeur(CVersionDonneesObjet version, string strChampDeTable, object valeur)
		{
			CVersionDonneesObjetOperation data = null;
			CListeObjetsDonnees listeDatas = version.Modifications;
			listeDatas.Filtre = new CFiltreData(
				CVersionDonneesObjetOperation.c_champTypeChamp + "=@1 and " +
				CVersionDonneesObjetOperation.c_champChamp + "=@2",
				CChampPourVersionInDb.c_TypeChamp,
				strChampDeTable);
			listeDatas.InterditLectureInDB = true;
			if (listeDatas.Count != 0)
				data = (CVersionDonneesObjetOperation)listeDatas[0];
			else
			{
				data = new CVersionDonneesObjetOperation(version.ContexteDonnee);
				data.CreateNewInCurrentContexte();
				data.VersionObjet = version;
			}
			data.TypeChamp = CChampPourVersionInDb.c_TypeChamp;
			data.FieldKey = strChampDeTable;
			data.SetValeurStd(valeur);
			data.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
			return data;
		}

		//---------------------------------------------------------------------
		public CResultAErreur AppliqueValeur(int? nIdVersion, IChampPourVersion champ, CObjetDonneeAIdNumerique objet, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (objet.Row.Table.Columns.Contains(champ.FieldKey))
			{
				try
				{
					if (valeur is byte[])
					{
						CDonneeBinaireInRow data = new CDonneeBinaireInRow(objet.ContexteDonnee.IdSession, objet.Row.Row, champ.FieldKey);
						data.Donnees = (byte[])valeur;
						objet.Row[champ.FieldKey] = data;
					}
					else if (valeur is IDifferencesBlob)
					{
						CContexteDonnee ctx = new CContexteDonnee(objet.ContexteDonnee.IdSession, true, false);
						
						result = ctx.SetVersionDeTravail(nIdVersion, false);
						if (!result)
							return result;
						CObjetDonneeAIdNumerique objetDansVersion = (CObjetDonneeAIdNumerique)Activator.CreateInstance(objet.GetType(), new object[] { ctx });
						if ( objetDansVersion.ReadIfExists ( objet.Id) )
						{
							CDonneeBinaireInRow dataInVersion = new CDonneeBinaireInRow(ctx.IdSession, objetDansVersion.Row.Row, champ.FieldKey);

							CDonneeBinaireInRow data = new CDonneeBinaireInRow(objet.ContexteDonnee.IdSession, objet.Row.Row, champ.FieldKey);
							data.Donnees = dataInVersion.Donnees;
							objet.Row[champ.FieldKey] = data;
						}
						else
							throw new Exception(I.T("Error|199"));
					}
					else
						objet.Row[champ.FieldKey] = valeur == null ? DBNull.Value : valeur;
				}
				catch
				{
					try
					{
						bool bOldEnforce = objet.ContexteDonnee.EnforceConstraints;
						objet.ContexteDonnee.EnforceConstraints = false;
						objet.Row[champ.FieldKey] = valeur == null ? DBNull.Value : valeur;
						objet.ContexteDonnee.AssureParents(objet.Row.Row);
					}
					catch (Exception e)
					{
						result.EmpileErreur(new CErreurException(e));
						result.EmpileErreur(I.T("Error while applying value for field @1|189", champ.NomConvivial));
					}
				}
			}
			return result;
		}

		//-------------------------------------------------------------
		public object GetValeur(CVersionDonneesObjetOperation versionData)
		{
			return versionData.GetValeurStd();
		}
		public object GetValeur(CObjetDonneeAIdNumerique obj, IChampPourVersion champ)
		{
			if (obj.Row.Table.Columns[champ.FieldKey].DataType == typeof(CDonneeBinaireInRow))
			{
				if ( obj.Row[champ.FieldKey] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(obj.ContexteDonnee.IdSession, obj.Row.Row, champ.FieldKey);
					CContexteDonnee.ChangeRowSansDetectionModification(obj.Row.Row, champ.FieldKey, donnee);
				}
				/*DataRow row = obj.Row;
				DataTable tb = row.Table;
				IObjetServeur loader = ((CContexteDonnee)tb.DataSet).GetTableLoader(tb.TableName);
				CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(tb.PrimaryKey, row);
				ArrayList lstKeys = new ArrayList();
				foreach (DataColumn col in tb.PrimaryKey)
					lstKeys.Add(row[col]);
				CResultAErreur result = loader.ReadBlob(champ.FieldKey, lstKeys.ToArray());
				if ( result )*/

			}
			return obj.Row[champ.FieldKey] == DBNull.Value ? null : obj.Row[champ.FieldKey];
		}



		#region IJournaliseurDonneesChamp Membres

		//Mise en cache des champs journalisables
		private static Dictionary<Type, List<IChampPourVersion>> m_dicTypeToChamps = new Dictionary<Type, List<IChampPourVersion>>();
		public List<IChampPourVersion> GetChampsJournalisables(CObjetDonneeAIdNumerique objet)
		{

			List<IChampPourVersion> champs = null;
			if ( !m_dicTypeToChamps.TryGetValue(objet.GetType(), out champs ))
			{
				champs = new List<IChampPourVersion>();

				CStructureTable structure = CStructureTable.GetStructure(objet.GetType());
				CInfoChampTable[] allchamps = structure.Champs;
				
				List<string> lstChampsSys = CSc2iDataConst.GetNomsChampsSysteme();
				foreach(CInfoChampTable c in allchamps)
					if(c.m_bIsInDB && !lstChampsSys.Contains(c.NomChamp))
						champs.Add(new CChampPourVersionInDb(c.NomChamp,c.NomConvivial));
				m_dicTypeToChamps[objet.GetType()] = champs;
			}
			return champs;
		}

		#endregion
	}

}
