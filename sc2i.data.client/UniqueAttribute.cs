using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

using sc2i.common;

namespace sc2i.data
{
	[AttributeUsage ( AttributeTargets.Class, AllowMultiple=true, Inherited=true )]
	public class UniqueAttribute : Attribute
	{
		public string[] Champs;
		public bool CreateContrainte;
		public string Message;

		public UniqueAttribute(
			bool bCreateConstraint, 
			string strMessage,
			params string[] strChamps)
		{
			CreateContrainte = bCreateConstraint;
			Message = strMessage;
			Champs = strChamps;
		}

		//Type->List<CInfoFiltreUnique>
		private static Hashtable m_tableFiltresParType = new Hashtable();

		
		//-------------------------------------------------------------------
		private class CInfoFiltreUnique
		{
			public readonly UniqueAttribute Attribute;
			public readonly CFiltreData Filtre;

			public CInfoFiltreUnique(UniqueAttribute attr, CFiltreData filtre)
			{
				Attribute = attr;
				Filtre = filtre;
			}
		}
		
		//-------------------------------------------------------------------
		private static List<CInfoFiltreUnique> GetFiltres ( Type typeObjets )
		{
			object existant = m_tableFiltresParType[typeObjets];
			if ( existant != null )
				return (List<CInfoFiltreUnique>)existant;

			List<CInfoFiltreUnique> lst = new List<CInfoFiltreUnique>();
			m_tableFiltresParType[typeObjets] = lst;

			CResultAErreur  result = CResultAErreur.True;
			if ( !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom (typeObjets) )//ne marche que sur les objets à id numérique
				return lst;

			//Récupère le champ clé
			object[] attrs = typeObjets.GetCustomAttributes ( typeof(TableAttribute), true );
			string strChampCle = "";
			if ( attrs.Length == 0 )
				return lst;
			strChampCle = ((TableAttribute)attrs[0]).ChampsId[0];			
			
			attrs = typeObjets.GetCustomAttributes(typeof(UniqueAttribute), true);
			if (attrs != null && attrs.Length > 0)
			{
				foreach (UniqueAttribute unique in attrs)
				{
					string strFiltre = strChampCle + "<>@1 and ";
					int nIndex = 2;
					foreach (string strChamp in unique.Champs)
					{
						strFiltre += strChamp + "=@" + (nIndex++) + " and ";
					}
					strFiltre = strFiltre.Substring(0, strFiltre.Length-5);
					lst.Add(new CInfoFiltreUnique(unique, new CFiltreData(strFiltre)));
				}
			}
			return lst;
		}

		/// <summary>
		/// Vérifie qu'un objet respecte bien ses règles d'unicité
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static CResultAErreur VerifieUnicite(CObjetDonnee objet)
		{
			if ( objet == null )
				return CResultAErreur.True;
            CResultAErreur result = VerifieUnicite ( objet.Row, GetFiltres ( objet.GetType()) , objet.GetType() );
            if ( !result )
                result.EmpileErreur(I.T("Unicity error on @1|20004",objet.DescriptionElement));
            return result;
		}

		/// <summary>
		/// Vérifie qu'une dataRow vérifie ses règles d'unicité
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		private static CResultAErreur VerifieUnicite(DataRow row, List<CInfoFiltreUnique> listeFiltres, Type typeObjet)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( row.RowState == DataRowState.Detached || 
				row.RowState == DataRowState.Deleted )
				return result;
			
			foreach (CInfoFiltreUnique info in listeFiltres)
			{
				CFiltreData filtre = new CFiltreData(info.Filtre.Filtre);
				filtre.Parametres.Add(row[row.Table.PrimaryKey[0]]);
				int nIndex = 2;
				List<string> lstValeurs = new List<String>();
				foreach (string strChamp in info.Attribute.Champs)
				{
					if (!row.Table.Columns.Contains(strChamp))
						return result;
					if (row[strChamp] == DBNull.Value)
					{
						filtre.Filtre.Replace("=@" + nIndex, " is null");
						filtre.Parametres.Add("");
					}
					else
						filtre.Parametres.Add(row[strChamp]);
					lstValeurs.Add(row[strChamp].ToString());
				}
				CListeObjetsDonnees liste = new CListeObjetsDonnees((CContexteDonnee)row.Table.DataSet, typeObjet);
				liste.Filtre = filtre;
				if (liste.Count > 0)
				{
					result.EmpileErreur(I.TT(typeObjet, info.Attribute.Message, lstValeurs.ToArray()));
					return result;
				}
			}
			return result;
		}


		public static CResultAErreur VerifieUnicite(
			CContexteDonnee contexte, 
			Type typeObjets,
			DataRowState states )
		{
			CResultAErreur  result = CResultAErreur.True;
			DataTable table = contexte.Tables[CContexteDonnee.GetNomTableForType(typeObjets)];
			List<CInfoFiltreUnique> lstFiltres = GetFiltres ( typeObjets );
			if ( lstFiltres.Count == 0 )
				return result;
			if (table != null)
			{
				ArrayList lstCopie = new ArrayList(table.Rows);
				foreach (DataRow row in table.Rows)
				{
					if ((row.RowState & states) != 0)
					{
						result = VerifieUnicite(row, lstFiltres, typeObjets);
						if (!result)
							return result;
					}
				}
			}
			return result;
		}
	}
}
