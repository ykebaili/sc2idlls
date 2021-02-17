using System;
using System.Windows.Forms;
using System.Reflection;

using sc2i.common;
using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CWndListeObjetsDonnees.
	/// </summary>
	public class CWndListeObjetsDonnees : GlacialList
	{
		private string m_strLastSort = "";
		private bool m_bSortAsc = true;

		/// /////////////////////////////////////////////////////////////
		public CWndListeObjetsDonnees()
			:base()
		{
		}

		/// /////////////////////////////////////////////////////////////
		///Implémente le tri d'une liste d'objets donnée
		/// <summary>
		/// Sort a column.
		public override void SortColumn( int nColumn )
		{
			if ( Count < 2 )			// nothing to sort
				return;

			if ( nColumn < 0 || nColumn > Columns.Count )
				return;

			if ( !(ListeSource is CListeObjetsDonnees ) )
			{
				base.SortColumn ( nColumn );
				return;
			}

			
			GLColumn col = Columns[nColumn];
			CListeObjetsDonnees listeDonnees = (CListeObjetsDonnees)ListeSource;
			
			string strProp = CInfoStructureDynamique.GetProprieteDotNet(col.Propriete);
			if ( strProp.IndexOf('.') < 0 )
			{
				//Trouve la propriété
				PropertyInfo info = listeDonnees.TypeObjets.GetProperty(strProp);
				if ( info != null )
				{
					//y-a-t-il un attribut TableField sur la propriété
					object[] attribs = info.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true );
					if ( attribs.Length > 0 )
					{
						if ( CheckedItems.Count != 0 && CheckBoxes)
							if ( CFormAlerte.Afficher(I.T("Warning, the sorting will uncheck all checked elements. Continue ?|139"),
								EFormAlerteType.Question ) == DialogResult.No )
								return;
						ResetCheck();
						TableFieldPropertyAttribute fieldAttr = (TableFieldPropertyAttribute)attribs[0];
						string strSort = fieldAttr.NomChamp;
						if ( strSort == m_strLastSort )
							m_bSortAsc = !m_bSortAsc;
						else
							m_bSortAsc = true;
						m_strLastSort = strSort;
						if (!m_bSortAsc )
							strSort += " desc";
						listeDonnees.Tri = strSort;
						listeDonnees.Refresh();
						Refresh();
						base.SortIndex = nColumn;
						Columns[nColumn].LastSortState = m_bSortAsc ? ColumnSortState.SortedDown : ColumnSortState.SortedUp;
						return;
					}
				}
			}
			CFormAlerte.Afficher(I.T("Sort on this field is impossible|138"), EFormAlerteType.Exclamation);
		}
	}
}
