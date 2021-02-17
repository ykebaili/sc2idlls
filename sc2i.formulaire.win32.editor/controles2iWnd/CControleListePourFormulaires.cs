using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using sc2i.common;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.formulaire.win32.controles2iWnd;


namespace sc2i.formulaire.win32
{
	public delegate void AfficheEntiteFromListeForFormulaireDelegate ( object entite );

	
	public partial class CControleListePourFormulaires : UserControl
	{
		private C2iWndListe m_2iList = null;
		private object m_objectSource = null;
        private CWndFor2iListe m_controlFor2iWnd = null;

		public CControleListePourFormulaires()
		{
			InitializeComponent();
		}

		public void Init( 
            CWndFor2iListe ctrlForListe,
            C2iWndListe liste2i )
		{
            m_controlFor2iWnd = ctrlForListe;
			m_2iList = liste2i;
			m_grid.ColumnHeadersVisible = liste2i.HeaderVisible;
			m_grid.BorderStyle = liste2i.ShowBorder ? BorderStyle.Fixed3D : BorderStyle.None;
			return;
		}

		//-------------------------------------------------
		public void SetElementEdite ( object objet )
		{
			if (objet == null || !objet.Equals(m_objectSource))
			{
				m_objectSource = objet;
				if (m_2iList.FillOnDemand)
				{
					m_lnkRemplir.Visible = true;
					m_lnkRemplir.SendToBack();
					m_grid.DataSource = null;
				}
				else
					Remplir();

               
			}
		}

		//-------------------------------------------------
        private object m_lastObjetSourceRemplissage = DBNull.Value;
        public void Remplir()
		{
			m_lnkRemplir.Visible = false;
			m_lblERREUR.Visible = false;
            if (m_2iList != null && m_2iList.OptimizeRefresh &&  m_objectSource == m_lastObjetSourceRemplissage)
                return;
            m_lastObjetSourceRemplissage = m_objectSource;
			try
			{
				CContexteEvaluationExpression contexte = CUtilControlesWnd.GetContexteEval(m_controlFor2iWnd, m_objectSource);
				IEnumerable datas = m_2iList.GetListeSource(contexte);
				DataTable table = m_2iList.GetDataTable(datas);
				m_grid.DataSource = table;
				UpdateGridStyle();
				m_grid.Refresh();
				return;
			}
			catch
			{
				m_lblERREUR.Visible = true;
			}
			m_grid.DataSource = null;
			m_grid.Refresh();
		}

		private void m_lnkRemplir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Remplir();
		}

        [DynamicMethod("Recalculate list")]
        public void Recalculate()
        {
            m_lastObjetSourceRemplissage = DBNull.Value;
            Remplir();
        }

		public Color? GetBackColor(int nRow)
		{
			try
			{
				DataRow row = ((DataTable)m_grid.DataSource).Rows[nRow];
				if (row[m_2iList.GetNomColObject()] == DBNull.Value)//total
					return m_2iList.TotalBackColor;
			}
			catch
			{
			}
			return null;
		}

		public Color? GetTextColor(int nRow)
		{
			try
			{
				DataRow row = ((DataTable)m_grid.DataSource).Rows[nRow];
				if (row[m_2iList.GetNomColObject()] == DBNull.Value)//total
					return m_2iList.TotalTextColor;
			}
			catch
			{
			}
			return null;
		}
		private void UpdateGridStyle()
		{
			DataGridTableStyle style = m_grid.TableStyle;
			if (style == null)
			{
				style = new DataGridTableStyle();
				m_grid.TableStyles.Add(style);
			}
			style.GridColumnStyles.Clear();

			//General
			style.AllowSorting = false;
			m_grid.CaptionVisible = false;
			m_grid.FlatMode = true;
			m_grid.BorderStyle = m_2iList.ShowBorder ? BorderStyle.Fixed3D : BorderStyle.None;

			//Couleurs de la grille
			m_grid.BackgroundColor = m_2iList.BackColor;
			style.BackColor = m_2iList.LineBackColor1;
			m_grid.BackColor = m_2iList.LineBackColor1;
			m_grid.AlternatingBackColor = m_2iList.LineBackColor2;
			style.AlternatingBackColor = m_2iList.LineBackColor2;
			style.GridLineColor = m_2iList.ColorGrid;
			style.GridLineStyle = (System.Windows.Forms.DataGridLineStyle)m_2iList.LineStyle;
			style.ForeColor = m_2iList.ForeColor;

			//Column header
			style.ColumnHeadersVisible = m_2iList.HeaderVisible;
			style.HeaderBackColor = m_2iList.ColorHeaderBack;
			style.HeaderForeColor = m_2iList.ColorHeaderText;
			style.HeaderFont = m_2iList.HeaderFont;
			
			
			style.PreferredRowHeight = m_2iList.ItemHeight;
			style.SelectionBackColor = m_2iList.ColorSelection;

			style.RowHeaderWidth = 15;
			m_grid.RowHeaderWidth = 15;




			GetColorElementDelegate delGetCouleurFond = new GetColorElementDelegate(GetBackColor);
			GetColorElementDelegate delGetcouleurText = new GetColorElementDelegate(GetTextColor);

			if (m_grid.DataSource is DataTable)
			{
				DataTable table = (DataTable)m_grid.DataSource;
				foreach (C2iWndListe.CColonne col in m_2iList.Columns)
				{
					C2iDataGridColumnStyleAControle colStyle = null;
					if (col.ActionSurLink == null)
						colStyle = new C2iDataGridColumnStyleAControle(m_labelForGrid, "Text");
					else
						colStyle = new C2iDataGridColumnStyleAControle(m_linkForGrid, "Text");
					colStyle.MappingName = col.Title;
					colStyle.HeaderText = col.Title;
					colStyle.TextAlign = HorizontalAlignment.Left;
					colStyle.Width = col.Width;
					colStyle.BackColor = col.BackColor;
					colStyle.ForeColor = col.TextColor;
					colStyle.Font = col.Font;
					colStyle.GetBackColorDelegate = delGetCouleurFond;
					colStyle.GetTextColorDelegate = delGetcouleurText;

					style.GridColumnStyles.Add(colStyle);
				}
			}
			object lastSource = m_grid.DataSource;
			m_grid.DataSource = null;
			m_grid.DataSource = lastSource;
		}


		//-----------------------------------------------------------------------
		private void m_linkForGrid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if ( !(m_grid.DataSource is DataTable ) )
				return;
			DataTable table = (DataTable)m_grid.DataSource;
			int nRow = m_grid.CurrentCell.RowNumber;
			int nCol = m_grid.CurrentCell.ColumnNumber;
			if (nRow >= 0 && nRow < table.Rows.Count)
			{
				DataRow row = table.Rows[nRow];
				object obj = row[m_2iList.GetNomColObject()];
				if (obj == null)
					return;
				C2iWndListe.CColonne colonne = m_2iList.GetColonne(m_grid.TableStyle.GridColumnStyles[nCol].MappingName);
				if (colonne != null && colonne.ActionSurLink != null)
				{
					CResultAErreur result = CExecuteurActionSur2iLink.ExecuteAction(this, colonne.ActionSurLink, obj);
					if (!result)
						CFormAlerte.Afficher(result);
				}
			}
		}

		#region IControleWndFor2iWnd Membres
		
		public void UpdateValeursCalculees()
		{
			if (m_grid.DataSource != null)
				Remplir();
		}

		public IControleWndFor2iWnd[] Childs
		{
			get
			{
				return new IControleWndFor2iWnd[0];
			}
		}

		#endregion

		#region IControlALockEdition Membres

		public bool LockEdition
		{
			get
			{
				return false;
			}
			set
			{
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion

		public void AppliqueRestriction(CListeRestrictionsUtilisateurSurType listeRestrictions)
		{
		}

		private IControleWndFor2iWnd m_ctrlWndParent = null;
		public IControleWndFor2iWnd ControleWndParent
		{
			get
			{
				return m_ctrlWndParent;
			}
			set
			{
				m_ctrlWndParent = value;
			}
		}
	}
}
