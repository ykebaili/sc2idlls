using System;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de SynchronizedFieldAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class SynchronizedFieldAttribute : Attribute
	{
		public readonly string Table1;
		public readonly string Table2;
		public readonly string CheminFrom1To2;
		public readonly string CheminFrom2To1;
		public readonly string FonctionCondition1;
		public readonly string FonctionCondition2;

		/// <summary>
		/// Permet la synchronisation entre deux champs de la base
		/// </summary>
		/// <param name="strTable1">Table 1</param>
		/// <param name="strTable2">Table 2</param>
		/// <param name="strCheminFrom1To2">Chemin permettant d'atteindre le champs de l'objet2 à partir de l'objet 1</param>
		/// <param name="strCheminFrom2To1"></param>
		/// <param name="strFonctionCondition1"></param>
		/// <param name="strFonctionCondition2"></param>
		public SynchronizedFieldAttribute( string strTable1, string strTable2,string strCheminFrom1To2, string strCheminFrom2To1, string strFonctionCondition1, string strFonctionCondition2)
		{
			Table1 = strTable1;
			Table2 = strTable2;
			CheminFrom1To2 = strCheminFrom1To2;
			CheminFrom2To1 = strCheminFrom2To1;
			FonctionCondition1 = strFonctionCondition1;
			FonctionCondition2 = strFonctionCondition2;
		}
	}
}
