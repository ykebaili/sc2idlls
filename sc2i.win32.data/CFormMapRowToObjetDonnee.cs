using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CFormMapRowToObjetDonnee.
	/// </summary>
	public class CFormMapRowToObjetDonnee : System.Windows.Forms.Form
	{
		private CListeObjetsDonnees m_listePossibles;
		private DataRow m_rowToMap;
		private CObjetDonneeAIdNumeriqueAuto m_objetSel;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.GlacialList m_wndListeElements;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel m_lnkSelectionner;
		private System.Windows.Forms.DataGrid m_gridObjetAMapper;
		private System.Windows.Forms.DataGrid m_gridObjetPropose;
		private System.Windows.Forms.LinkLabel m_btnAucun;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormMapRowToObjetDonnee()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormMapRowToObjetDonnee));
            this.m_gridObjetAMapper = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndListeElements = new sc2i.win32.common.GlacialList();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_gridObjetPropose = new System.Windows.Forms.DataGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lnkSelectionner = new System.Windows.Forms.LinkLabel();
            this.m_btnAucun = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridObjetAMapper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridObjetPropose)).BeginInit();
            this.SuspendLayout();
            // 
            // m_gridObjetAMapper
            // 
            this.m_gridObjetAMapper.DataMember = "";
            this.m_gridObjetAMapper.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridObjetAMapper.Location = new System.Drawing.Point(0, 16);
            this.m_gridObjetAMapper.Name = "m_gridObjetAMapper";
            this.m_gridObjetAMapper.Size = new System.Drawing.Size(296, 280);
            this.m_gridObjetAMapper.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Element to map|104";
            // 
            // m_wndListeElements
            // 
            this.m_wndListeElements.AllowColumnResize = true;
            this.m_wndListeElements.AllowMultiselect = false;
            this.m_wndListeElements.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeElements.AlternatingColors = false;
            this.m_wndListeElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeElements.AutoHeight = true;
            this.m_wndListeElements.AutoSort = true;
            this.m_wndListeElements.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeElements.CanChangeActivationCheckBoxes = false;
            this.m_wndListeElements.CheckBoxes = false;
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Column1";
            glColumn1.Propriete = "DescriptionElement";
            glColumn1.Text = "Description|40";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 300;
            this.m_wndListeElements.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1});
            this.m_wndListeElements.ContexteUtilisation = "";
            this.m_wndListeElements.EnableCustomisation = true;
            this.m_wndListeElements.FocusedItem = null;
            this.m_wndListeElements.FullRowSelect = true;
            this.m_wndListeElements.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_wndListeElements.GridColor = System.Drawing.Color.Gray;
            this.m_wndListeElements.HeaderHeight = 22;
            this.m_wndListeElements.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeElements.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeElements.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeElements.HeaderVisible = true;
            this.m_wndListeElements.HeaderWordWrap = false;
            this.m_wndListeElements.HotColumnIndex = -1;
            this.m_wndListeElements.HotItemIndex = -1;
            this.m_wndListeElements.HotTracking = false;
            this.m_wndListeElements.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeElements.ImageList = null;
            this.m_wndListeElements.ItemHeight = 17;
            this.m_wndListeElements.ItemWordWrap = false;
            this.m_wndListeElements.ListeSource = null;
            this.m_wndListeElements.Location = new System.Drawing.Point(8, 320);
            this.m_wndListeElements.MaxHeight = 17;
            this.m_wndListeElements.Name = "m_wndListeElements";
            this.m_wndListeElements.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeElements.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeElements.ShowBorder = true;
            this.m_wndListeElements.ShowFocusRect = false;
            this.m_wndListeElements.Size = new System.Drawing.Size(592, 256);
            this.m_wndListeElements.SortIndex = 0;
            this.m_wndListeElements.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeElements.TabIndex = 4;
            this.m_wndListeElements.Text = "glacialList1";
            this.m_wndListeElements.TrierAuClicSurEnteteColonne = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Possible elements|106";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.Location = new System.Drawing.Point(204, 592);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(324, 592);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_gridObjetPropose
            // 
            this.m_gridObjetPropose.DataMember = "";
            this.m_gridObjetPropose.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridObjetPropose.Location = new System.Drawing.Point(304, 16);
            this.m_gridObjetPropose.Name = "m_gridObjetPropose";
            this.m_gridObjetPropose.Size = new System.Drawing.Size(296, 280);
            this.m_gridObjetPropose.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(304, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Existing element proposed|105";
            // 
            // m_lnkSelectionner
            // 
            this.m_lnkSelectionner.Location = new System.Drawing.Point(8, 576);
            this.m_lnkSelectionner.Name = "m_lnkSelectionner";
            this.m_lnkSelectionner.Size = new System.Drawing.Size(88, 16);
            this.m_lnkSelectionner.TabIndex = 11;
            this.m_lnkSelectionner.TabStop = true;
            this.m_lnkSelectionner.Text = "Select|20";
            this.m_lnkSelectionner.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkSelectionner_LinkClicked);
            // 
            // m_btnAucun
            // 
            this.m_btnAucun.Location = new System.Drawing.Point(464, 0);
            this.m_btnAucun.Name = "m_btnAucun";
            this.m_btnAucun.Size = new System.Drawing.Size(80, 16);
            this.m_btnAucun.TabIndex = 12;
            this.m_btnAucun.TabStop = true;
            this.m_btnAucun.Text = "None|19";
            this.m_btnAucun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnAucun_LinkClicked);
            // 
            // CFormMapRowToObjetDonnee
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(608, 621);
            this.Controls.Add(this.m_btnAucun);
            this.Controls.Add(this.m_lnkSelectionner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_gridObjetPropose);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_wndListeElements);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_gridObjetAMapper);
            this.Name = "CFormMapRowToObjetDonnee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mappage|103";
            this.Load += new System.EventHandler(this.CFormMapRowToObjetDonnee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridObjetAMapper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridObjetPropose)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// ////////////////////////////////////////////////
		public static CObjetDonneeAIdNumeriqueAuto MappeRow ( DataRow row, CListeObjetsDonnees listePossibles, ref bool bCancel, bool bResolutionAutomatique )
		{
			CFormMapRowToObjetDonnee form = new CFormMapRowToObjetDonnee();
			if ( form.Init ( row, listePossibles, bResolutionAutomatique ) )
				return form.m_objetSel;
			bCancel = form.ShowDialog() != DialogResult.OK;
			CObjetDonneeAIdNumeriqueAuto objet = form.m_objetSel;
			form.Dispose();
			return objet;
		}

		/// ////////////////////////////////////////////////
		private bool Init ( DataRow row, CListeObjetsDonnees listePossibles, bool bResolutionAutomatique )
		{
			m_rowToMap = row;
			m_listePossibles = listePossibles;

			DisplayElement ( row, m_gridObjetAMapper );

			//regarde dans la liste s'il y a un élément existant avec le même id
			CFiltreData oldFiltre = m_listePossibles.Filtre;
			CFiltreData newFiltre = CFiltreData.GetAndFiltre ( 
				oldFiltre, 
				new CFiltreData ( row.Table.PrimaryKey[0].ColumnName+"=@1", row[row.Table.PrimaryKey[0]] ));
			m_listePossibles.Filtre = newFiltre;
			m_listePossibles.Refresh();
			if ( m_listePossibles.Count > 0 )
			{
				m_objetSel = (CObjetDonneeAIdNumeriqueAuto)m_listePossibles[0];
				if ( bResolutionAutomatique )
				{
					DataRow rowObjet = m_objetSel.Row.Row;
					//Compare toutes les valeurs, si elles sont toutes égales, on sélectionne automatique
					//L'élément
					bool bAllOk = true;
					CStructureTable structure = CStructureTable.GetStructure(m_objetSel.GetType());
					foreach ( CInfoChampTable info in structure.Champs )
					{
						if ( info.NomChamp != CSc2iDataConst.c_champIdSynchro  && !m_rowToMap[info.NomChamp].Equals(rowObjet[info.NomChamp] ) )
						{
							bAllOk = false;
							break;
						}
					}
					if ( bAllOk )
						return true;
				}
				DisplayElement ( m_objetSel.Row.Row, m_gridObjetPropose );
			}
			else
				DisplayElement ( null, m_gridObjetPropose );
			m_listePossibles.Filtre = oldFiltre;
			m_listePossibles.Refresh();
			
			
			
			m_wndListeElements.ListeSource = m_listePossibles;
			return false;
		}

		/// ////////////////////////////////////////////////
		private void DisplayElement ( DataRow row, DataGrid grid )
		{
			if ( row == null )
			{
				grid.DataSource = null;
				grid.Refresh();
				return;
			}
			DataTable tablePresentation = new DataTable("Presentation");
			tablePresentation.Columns.Add ( "NOM", typeof(string));
			tablePresentation.Columns.Add ( "VALEUR", typeof(string));
			Type tp = CContexteDonnee.GetTypeForTable ( row.Table.TableName );
			CStructureTable structure = CStructureTable.GetStructure(tp);
			foreach ( CInfoChampTable champ in  structure.Champs )
			{
				DataRow rowPres = tablePresentation.NewRow();
				rowPres["NOM"] = champ.NomConvivial;
				object val = row[champ.NomChamp];
				rowPres["VALEUR"] = val==null?"NULL":val.ToString();
				tablePresentation.Rows.Add ( rowPres );
			}
			grid.DataSource = tablePresentation;
			DataGridTableStyle style = grid.TableStyles[tablePresentation.TableName];
			if ( style == null )
			{
				style = new DataGridTableStyle();
				style.MappingName = tablePresentation.TableName;
				DataGridColumnStyle col = new DataGridTextBoxColumn();
				col.MappingName = "NOM";
				col.Width = grid.Width/3;
				style.GridColumnStyles.Add ( col );
				col = new DataGridTextBoxColumn();
				col.MappingName = "VALEUR";
				col.Width = grid.Width*2/3;
				style.GridColumnStyles.Add ( col );
				grid.TableStyles.Add ( style );
			}
			grid.Refresh();
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnAucun_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_objetSel = null;
			DisplayElement ( null, m_gridObjetPropose );
		}

		private void m_lnkSelectionner_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_wndListeElements.SelectedItems.Count == 1 )
			{
				m_objetSel = (CObjetDonneeAIdNumeriqueAuto)m_wndListeElements.SelectedItems[0];
				DisplayElement ( m_objetSel.Row.Row, m_gridObjetPropose );
			}
		}

        private void CFormMapRowToObjetDonnee_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }


	}
}
