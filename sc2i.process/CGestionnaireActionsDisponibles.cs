using System;
using System.Collections;
using System.Collections.Generic;
using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Décrit une classe d'action
	/// </summary>
	public class CInfoAction
	{
		public readonly string Categorie;
		public readonly string Nom;
		public string Description;
		public Type TypeAction;

		public CInfoAction ( string strNom, string strDesc, Type type, string strCat )
		{
			Nom = strNom;
			Description = strDesc;
			TypeAction = type;
			Categorie = strCat;
		}
	}

	/// <summary>
	/// Description résumée de CGestionnaireActionsDisponibles.
	/// </summary>
	/// 
	public class CGestionnaireActionsDisponibles
	{
		public static string c_categorieDonnees = I.T("Data|30002");
		public static string c_categorieDeroulement = I.T("Progress|30003");
		public static string c_categorieInterface = I.T("User interface|30004");
		public static string c_categorieComportement = I.T("Behavior|30005");
		public static string c_categorieMetier = I.T("Activity|30006");
		

		//Liste d'infos action
		private static ArrayList m_listeTypesActions = new ArrayList();
		
		/// /////////////////////////////////////////////////
		public static void RegisterTypeAction ( 
			string strNom,
			string strDesc,
			Type type,
			string strCategorie )
		{
			m_listeTypesActions.Add ( new CInfoAction ( strNom, strDesc, type, strCategorie ));
		}

		/// /////////////////////////////////////////////////
		public static CInfoAction[] TypesDisponibles
		{
			get
			{
				return (CInfoAction[])m_listeTypesActions.ToArray(typeof(CInfoAction));
			}
		}
	}
}
