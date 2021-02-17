using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public delegate void ListLinkItemClickEventHandler(ListViewItem item);

	/// <summary>
	/// Description résumée de CListLink.
	/// </summary>
	public class CListLink : System.Windows.Forms.ListView
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ListViewItem m_itemEnCours = new ListViewItem();
		private Font m_fontLast = null;

		private Color m_activatedColor = Color.Red;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private Color m_deactivatedColor = Color.Blue;
		//--------------------------------------------------------------------------------------------------
		public CListLink()
			:base()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
			this.HideSelection = true;
			this.ForeColor = m_deactivatedColor;
		}
		//--------------------------------------------------------------------------------------------------
		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		//--------------------------------------------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		//--------------------------------------------------------------------------------------------------
		private void InitializeComponent()
		{
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			// 
			// CListLink
			// 
			this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																			  this.columnHeader1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline);
			this.FullRowSelect = true;
			this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.MultiSelect = false;
			this.Size = new System.Drawing.Size(300, 300);
			this.View = System.Windows.Forms.View.Details;
			this.Resize += new System.EventHandler(this.CListLink_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CListLink_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CListLink_MouseMove);

		}
		//--------------------------------------------------------------------------------------------------
		private void CListLink_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem item = new ListViewItem();
			item = this.GetItemAtEx(e.X,e.Y);

			if (item != m_itemEnCours)
			{
				if (m_itemEnCours!=null)
				{
					DeactivateItem(m_itemEnCours);
				}
				
				if (item!=null)
				{
					ActivateItem(item);
				}
				this.MouseLeave += new EventHandler( OnMouseLeave );

				m_itemEnCours = item;
			}
			if ( item != null ) 
				Cursor.Current = Cursors.Hand; 
			else
				Cursor.Current = this.Cursor;
		}
		//--------------------------------------------------------------------------------------------------
		private void ActivateItem(ListViewItem item)
		{
			item.ForeColor = m_activatedColor;
			m_fontLast = item.Font;
			item.Font = new Font(m_fontLast, m_fontLast.Style | FontStyle.Underline );
		}
		//--------------------------------------------------------------------------------------------------
		private void DeactivateItem(ListViewItem item)
		{
			item.ForeColor = m_deactivatedColor;
			item.Font = m_fontLast;
		}
		//--------------------------------------------------------------------------------------------------
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if ( m_itemEnCours != null )
				DeactivateItem(m_itemEnCours);
			m_itemEnCours =null;
		}
		//--------------------------------------------------------------------------------------------------
		private void CListLink_Resize(object sender, System.EventArgs e)
		{
			if ( Columns.Count ==1 )
			{
				Columns[0].Width = ClientSize.Width-10;
			}
		}
		//--------------------------------------------------------------------------------------------------
		private void CListLink_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem item = GetItemAtEx(e.X,e.Y);
			if (item!=null)
			{
				if (ItemClick!=null)
					ItemClick(item);
			}
		}
		//--------------------------------------------------------------------------------------------------
		private ListViewItem GetItemAtEx(int x, int y)
		{
			ListViewItem item = null;
			if (this.CheckBoxes && x<=16)
				item = null;
			else
				item = GetItemAt(x,y);
			return item;
		}

		public event ListLinkItemClickEventHandler ItemClick;
		//--------------------------------------------------------------------------------------------------
		public void CheckAll()
		{
			foreach(ListViewItem item in this.Items)
				if (!item.Checked)
					item.Checked = true;
		}
		//--------------------------------------------------------------------------------------------------
		public void UncheckAll()
		{
			foreach(ListViewItem item in this.Items)
				if (item.Checked)
					item.Checked = false;
		}
		//--------------------------------------------------------------------------------------------------
		public Color ColorActive
		{
			get
			{
				return m_activatedColor;
			}
			set
			{
				m_activatedColor = value;
			}
		}

		public Color ColorDesactive
		{
			get
			{
				return m_deactivatedColor;
			}
			set
			{
				m_deactivatedColor = value;
			}
		}
		
	}
}
