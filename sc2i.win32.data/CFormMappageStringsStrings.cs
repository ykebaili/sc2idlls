using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Ce formulaire permet d'éditer un CMappageStringsStrings
	/// 2 Constructeurs existent :
	/// le premier permet de passer 2 listes de strings directement
	/// Le second permet de passer des objets avec le nom de la propriété du string
	/// </summary>
	public partial class CFormMappageStringsStrings : Form
	{
		private bool m_bModeSimple = true;
		private ArrayList m_listeA = new ArrayList();
		private ArrayList m_listeB = new ArrayList();
		private string m_strProprieteA = "";
		private string m_strProprieteB = "";
		private CMappageStringsStrings m_mappage;

		public CFormMappageStringsStrings()
		{
			InitializeComponent();
		}
		public CFormMappageStringsStrings(List<string> stringsA, List<string> stringsB, CMappageStringsStrings mappage)
		{
			m_mappage = mappage;
			m_stringsA = stringsA;
			m_stringsB = stringsB;

			InitializeComponent();

		}


		public static bool MapperStrings(List<string> stringsA, List<string> stringsB, CMappageStringsStrings mappage)
		{
			CFormMappageStringsStrings form = new CFormMappageStringsStrings(stringsA, stringsB, mappage);
			form.m_bModeSimple = true;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}
		public static bool MapperStringsAvecObjets(ArrayList listeA, string strProprieteLibelleA, ArrayList listeB,	string strProprieteLibelleB, CMappageStringsStrings mappage)
		{
			CFormMappageStringsStrings form = new CFormMappageStringsStrings();
			form.m_bModeSimple = false;
			form.m_listeA = listeA;
			form.m_listeB = listeB;
			form.m_strProprieteA = strProprieteLibelleA;
			form.m_strProprieteB = strProprieteLibelleB;
			form.m_mappage = mappage;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		private void RepositionneFleche()
		{
			m_imgFleche.Width = m_imgFleche.Parent.ClientSize.Width;
			m_imgFleche.Visible = false;
			m_wndLigneA.Visible = false;
			m_wndLigneB.Visible = false;
			if (m_wndListeA.SelectedIndices.Count == 1 &&
				m_wndListeB.SelectedIndices.Count == 1)
			{
				ListViewItem item = m_wndListeA.SelectedItems[0];
				Point pt = item.Position;
				pt = m_wndListeA.PointToScreen(pt);
				m_wndLigneA.Location = new Point(0, m_wndLigneA.Parent.PointToClient(pt).Y - 1);
				m_wndLigneB.Location = new Point(0, m_wndLigneA.Location.Y + item.Bounds.Height + 1);
				pt = m_imgFleche.Parent.PointToClient(pt);
				m_imgFleche.Location = new Point(0, pt.Y);
				m_imgFleche.Height = item.Bounds.Height;
				m_wndLigneA.Width = m_wndLigneB.Width = m_panelTotal.ClientSize.Width;
				m_imgFleche.Visible = true;
				m_wndLigneA.Visible = m_wndLigneB.Visible = true;
			}
		}

		private Dictionary<string, object> m_mappageStringsA = new Dictionary<string, object>();
		private Dictionary<string, object> m_mappageStringsB = new Dictionary<string, object>();

		private List<string> m_stringsA = new List<string>();
		private List<string> m_stringsB = new List<string>();
		//------------------------------------------------------
		private void CFormMappage_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
			if (!m_bModeSimple)
			{
				m_stringsA = GetStringsName(m_listeA, m_strProprieteA, m_mappageStringsA);
				m_stringsB = GetStringsName(m_listeB, m_strProprieteB, m_mappageStringsB);
			}
			m_mappage.InitMappage(m_stringsA, m_stringsB);
			FillListe(m_wndListeA, m_listeA, m_mappage.StringsA, m_mappageStringsA);
			FillListe(m_wndListeB, m_listeB, m_mappage.StringsB, m_mappageStringsB);
		}
		//------------------------------------------------------
		private void m_btnOk_Click(object sender, EventArgs e)
		{
			//Refait les listes
			m_listeA.Clear();
			List<string> stringsA = new List<string>();
			foreach (ListViewItem item in m_wndListeA.Items)
			{
				stringsA.Add(item.Text);
				if (!m_bModeSimple)
					m_listeA.Add(item.Tag);
			}
			m_mappage.StringsA = stringsA;

			m_listeB.Clear();
			List<string> stringsB = new List<string>();
			foreach (ListViewItem item in m_wndListeB.Items)
			{
				stringsB.Add(item.Text);
				if (!m_bModeSimple)
					m_listeB.Add(item.Tag);
			}
			m_mappage.StringsB = stringsB;

			DialogResult = DialogResult.OK;
			Close();
		}
		//------------------------------------------------------
		private void m_btnAnnuler_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		//------------------------------------------------------
		private bool m_bInSelectedIndexChangeListeA = false;
		private void m_wndListe1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_bInSelectedIndexChangeListeA)
				return;
			m_bInSelectedIndexChangeListeA = true;
			if (m_wndListeA.SelectedIndices.Count == 1)
			{
				int nIndex = m_wndListeA.SelectedIndices[0];
				m_wndListeB.SelectedIndices.Clear();
				if (nIndex < m_wndListeB.Items.Count)
					m_wndListeB.SelectedIndices.Add(nIndex);
			}
			RepositionneFleche();
			m_bInSelectedIndexChangeListeA = false;
		}

		//------------------------------------------------------
		private bool m_bInSelectedIndexChangeListeB = false;
		private void m_wndListe2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_bInSelectedIndexChangeListeB)
				return;
			m_bInSelectedIndexChangeListeB = true;
			if (m_wndListeB.SelectedIndices.Count == 1)
			{
				int nIndex = m_wndListeB.SelectedIndices[0];
				m_wndListeA.SelectedIndices.Clear();
				if (nIndex < m_wndListeA.Items.Count)
				{
					m_wndListeA.SelectedIndices.Add(nIndex);
				}
			}
			RepositionneFleche();
			m_bInSelectedIndexChangeListeB = false;
		}

		//------------------------------------------------------
		private List<string> GetStringsName(ArrayList liste, string strProp, Dictionary<string, object> mappage)
		{
			List<string> lstNoms = new List<string>();

			foreach (object obj in liste)
			{
				string strVal = null;
				if (obj != null)
				{
					PropertyInfo info = obj.GetType().GetProperty(strProp);
					if (info != null)
					{
						try
						{
							strVal = info.GetGetMethod().Invoke(obj, new object[0]).ToString();
							lstNoms.Add(strVal);
							mappage.Add(strVal, obj);
						}
						catch
						{
						}
					}
					if (strVal == null)
						strVal = obj.ToString();
				}
			}

			List<string> lstNomsOrdonnes = new List<string>();

			return lstNoms;
		}
		private void FillListe(ListView wndListe, ArrayList liste, List<string> elements, Dictionary<string, object> mappage)
		{
			wndListe.Items.Clear();
			int nCpt = 0;
			foreach (string strVal in elements)
			{
				ListViewItem item = new ListViewItem(strVal);
				if(!m_bModeSimple)
					item.Tag = mappage[strVal];
				wndListe.Items.Add(item);
				nCpt++;
			}
		}
	}
}