using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace sc2i.win32.common
{

	public partial class CCtrlUpDownListView : CCtrlUpDownBase
	{
		public event EventHandler AvantRenumeration;
		public event EventHandler ApresRenumeration;

		private ListView m_listeGeree = null;
		private string m_strProprieteNumero = "";

		public CCtrlUpDownListView()
		{
		}

		//-------------------------------------------
		public virtual ListView ListeGeree
		{
			get { return m_listeGeree; }
			set
			{
				m_listeGeree = value;
			}
		}

		//----------------------------------------------------------
		public string ProprieteNumero
		{
			get { return m_strProprieteNumero; }
			set { m_strProprieteNumero = value; }
		}

		//----------------------------------------------------------
		public override void Descendre()
		{
			OnClic();

			if (m_listeGeree.SelectedItems.Count == 1)
			{
				OnClicPourDescendre();

				ListViewItem item = m_listeGeree.SelectedItems[0];
				int nIndex = item.Index;
				if (nIndex < m_listeGeree.Items.Count - 1)
				{
					m_listeGeree.Items.Remove(item);
					m_listeGeree.Items.Insert(nIndex + 1, item);
					Renumerotte();
				}
			}
		}

		//----------------------------------------------------------
		
		public override void Monter()
		{
			OnClic();
			if (m_listeGeree.SelectedItems.Count == 1)
			{
				OnClicPourMonter();

				ListViewItem item = m_listeGeree.SelectedItems[0];
				int nIndex = item.Index;
				if (nIndex > 0)
				{
					m_listeGeree.Items.Remove(item);
					m_listeGeree.Items.Insert(nIndex - 1, item);
					Renumerotte();
				}
			}
		}




		

		//----------------------------------------------------------
		private void Renumerotte()
		{
			if (AvantRenumeration != null)
				AvantRenumeration(this, new EventArgs());

			int nIndex = 0;
			foreach (ListViewItem item in m_listeGeree.Items)
				SetNumero(item, nIndex++);


			if (ApresRenumeration != null)
				ApresRenumeration(this, new EventArgs());
		}

		//----------------------------------------------------------
		private void SetNumero(ListViewItem item, int nValeur)
		{
			if (m_strProprieteNumero != "")
			{
				object tag = item.Tag;
				if (tag == null)
					return;
				PropertyInfo info = tag.GetType().GetProperty(m_strProprieteNumero);
				if (info != null)
				{
					MethodInfo method = info.GetSetMethod();
					if (method != null)
					{

						try { method.Invoke(tag, new object[] { nValeur }); }
						catch { }
					}
				}
			}
		}
	}
}
