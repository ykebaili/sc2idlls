using System;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CDefinitionChampFiltreAvance.
	/// </summary>
	public class CDefinitionChampFiltreAvance : IDefinitionChampExpression
	{
		private string m_strNomChamp;

		public CDefinitionChampFiltreAvance( string strNomChamp )
		{
			m_strNomChamp = strNomChamp;
		}

		public string Nom
		{
			get
			{
				return m_strNomChamp;
			}
		}
	}
}
