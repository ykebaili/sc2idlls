using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace sc2i.expression
{
	public class CFournisseurPropDynForDataTable : IFournisseurProprietesDynamiques
	{
		private DataTable m_table = null;

		public CFournisseurPropDynForDataTable(DataTable table)
		{
			m_table = table;
		}


		#region IFournisseurProprietesDynamiques Membres

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
		{
			return GetDefinitionsChamps(typeInterroge, nNbNiveaux, null);
		}

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			if (m_table == null)
				return new CDefinitionProprieteDynamique[0];
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach ( DataColumn col in m_table.Columns )
			{
				lst.Add ( new CDefinitionProprieteDynamiqueDataColumn ( col ));
			}
			return lst.ToArray();
		}

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			if (objet != null)
				return GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente);
			return new CDefinitionProprieteDynamique[0];
		}

		

		#endregion
	}
}
