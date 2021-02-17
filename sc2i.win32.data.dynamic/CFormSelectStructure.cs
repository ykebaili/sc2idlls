using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.multitiers.client;
using sc2i.win32.common;
using sc2i.common;
using sc2i.data;
using sc2i.win32.data;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormSelectStructure.
	/// </summary>
	public class CFormSelectStructure : System.Windows.Forms.Form
	{
		private sc2i.win32.common.C2iLink m_linkStructure;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private sc2i.win32.data.CComboBoxListeObjetsDonnees m_cmbStructures;

		private sc2i.win32.data.dynamic.CPanelEditionStructureDonnee m_panelEditStructure;
		private Crownwood.Magic.Controls.TabPage tabPageStructure;
		private Crownwood.Magic.Controls.TabPage tabPageOptions;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private Crownwood.Magic.Controls.TabPage tabPage3;
		private Crownwood.Magic.Controls.TabPage tabPage4;
		private sc2i.win32.data.dynamic.CPanelEditMultiStructure m_panelMultiStructure;
		private sc2i.win32.data.dynamic.CPanelEditFormatExport m_panelOptionsExport;
        private CExtStyle cExtStyle1;
		private CListeObjetsDonnees m_listeObjets = null;

		public CFormSelectStructure()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
		}

		public CFormSelectStructure(CListeObjetsDonnees listeObjets)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_listeObjets = listeObjets;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectStructure));
            sc2i.data.dynamic.CExporteurDatasetXML cExporteurDatasetXML4 = new sc2i.data.dynamic.CExporteurDatasetXML();
            this.m_linkStructure = new sc2i.win32.common.C2iLink(this.components);
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_cmbStructures = new sc2i.win32.data.CComboBoxListeObjetsDonnees();
            this.m_panelEditStructure = new sc2i.win32.data.dynamic.CPanelEditionStructureDonnee();
            this.tabPageOptions = new Crownwood.Magic.Controls.TabPage();
            this.tabPageStructure = new Crownwood.Magic.Controls.TabPage();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage4 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelOptionsExport = new sc2i.win32.data.dynamic.CPanelEditFormatExport();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelMultiStructure = new sc2i.win32.data.dynamic.CPanelEditMultiStructure();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.tabPage1.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_linkStructure
            // 
            this.m_linkStructure.ClickEnabled = false;
            this.m_linkStructure.ColorLabel = System.Drawing.SystemColors.ControlText;
            this.m_linkStructure.ColorLinkDisabled = System.Drawing.Color.Blue;
            this.m_linkStructure.ColorLinkEnabled = System.Drawing.Color.Blue;
            this.m_linkStructure.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_linkStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.m_linkStructure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_linkStructure.Location = new System.Drawing.Point(20, 12);
            this.m_linkStructure.Name = "m_linkStructure";
            this.m_linkStructure.Size = new System.Drawing.Size(96, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_linkStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_linkStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_linkStructure.TabIndex = 0;
            this.m_linkStructure.Text = "Structure|169";
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOK.Location = new System.Drawing.Point(277, 342);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOK, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOK, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "Export|26";
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(357, 342);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Cancel|11";
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_cmbStructures
            // 
            this.m_cmbStructures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbStructures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbStructures.ElementSelectionne = null;
            this.m_cmbStructures.IsLink = false;
            this.m_cmbStructures.ListDonnees = null;
            this.m_cmbStructures.Location = new System.Drawing.Point(122, 9);
            this.m_cmbStructures.LockEdition = false;
            this.m_cmbStructures.Name = "m_cmbStructures";
            this.m_cmbStructures.NullAutorise = true;
            this.m_cmbStructures.ProprieteAffichee = null;
            this.m_cmbStructures.ProprieteParentListeObjets = null;
            this.m_cmbStructures.SelectionneurParent = null;
            this.m_cmbStructures.Size = new System.Drawing.Size(286, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbStructures, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbStructures, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbStructures.TabIndex = 6;
            this.m_cmbStructures.TextNull = "Structure temporaire";
            this.m_cmbStructures.Tri = true;
            this.m_cmbStructures.SelectedValueChanged += new System.EventHandler(this.m_cmbStructures_SelectedValueChanged);
            // 
            // m_panelEditStructure
            // 
            this.m_panelEditStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelEditStructure.ComboTypeLockEdition = false;
            this.m_panelEditStructure.ElementAVariablesPourFiltre = null;
            this.m_panelEditStructure.Location = new System.Drawing.Point(24, 40);
            this.m_panelEditStructure.LockEdition = false;
            this.m_panelEditStructure.Name = "m_panelEditStructure";
            this.m_panelEditStructure.Size = new System.Drawing.Size(288, 120);
            this.m_panelEditStructure.StructureExport = ((sc2i.data.dynamic.C2iStructureExport)(resources.GetObject("m_panelEditStructure.StructureExport")));
            this.cExtStyle1.SetStyleBackColor(this.m_panelEditStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelEditStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditStructure.TabIndex = 7;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Location = new System.Drawing.Point(0, 25);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Selected = false;
            this.tabPageOptions.Size = new System.Drawing.Size(660, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Title = "Export options|201";
            // 
            // tabPageStructure
            // 
            this.tabPageStructure.Location = new System.Drawing.Point(0, 25);
            this.tabPageStructure.Name = "tabPageStructure";
            this.tabPageStructure.Selected = false;
            this.tabPageStructure.Size = new System.Drawing.Size(660, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPageStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPageStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPageStructure.TabIndex = 0;
            this.tabPageStructure.Title = "Structure|230";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelEditStructure);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(296, 183);
            this.cExtStyle1.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(296, 183);
            this.cExtStyle1.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 32);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 1;
            this.c2iTabControl1.SelectedTab = this.tabPage4;
            this.c2iTabControl1.Size = new System.Drawing.Size(696, 304);
            this.cExtStyle1.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 7;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage3,
            this.tabPage4});
            this.c2iTabControl1.SelectionChanged += new System.EventHandler(this.c2iTabControl1_SelectionChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_panelOptionsExport);
            this.tabPage4.Location = new System.Drawing.Point(0, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Selected = false;
            this.tabPage4.Size = new System.Drawing.Size(680, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage4.TabIndex = 11;
            this.tabPage4.Title = "Options|231";
            // 
            // m_panelOptionsExport
            // 
            cExporteurDatasetXML4.ExporteStructureOnly = false;
            this.m_panelOptionsExport.Exporteur = cExporteurDatasetXML4;
            this.m_panelOptionsExport.Location = new System.Drawing.Point(0, 0);
            this.m_panelOptionsExport.Name = "m_panelOptionsExport";
            this.m_panelOptionsExport.SansFichier = false;
            this.m_panelOptionsExport.Size = new System.Drawing.Size(680, 256);
            this.cExtStyle1.SetStyleBackColor(this.m_panelOptionsExport, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelOptionsExport, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelOptionsExport.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_panelMultiStructure);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(680, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 10;
            this.tabPage3.Title = "Structure|230";
            // 
            // m_panelMultiStructure
            // 
            this.m_panelMultiStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelMultiStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelMultiStructure.FiltreDynamique = null;
            this.m_panelMultiStructure.Location = new System.Drawing.Point(0, 0);
            this.m_panelMultiStructure.LockEdition = false;
            this.m_panelMultiStructure.Name = "m_panelMultiStructure";
            this.m_panelMultiStructure.Size = new System.Drawing.Size(680, 264);
            this.cExtStyle1.SetStyleBackColor(this.m_panelMultiStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelMultiStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelMultiStructure.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelMultiStructure.TabIndex = 0;
            // 
            // CFormSelectStructure
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(690, 372);
            this.Controls.Add(this.c2iTabControl1);
            this.Controls.Add(this.m_cmbStructures);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_linkStructure);
            this.Name = "CFormSelectStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Structure selection|168";
            this.Load += new System.EventHandler(this.CFormSelectStructure_Load);
            this.tabPage1.ResumeLayout(false);
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void InitComboStructure()
		{
			if (m_listeObjets!=null)
				InitComboStructure( m_listeObjets.TypeObjets );
			m_panelMultiStructure.Init ( MutliStructureFromCombo );
		}


        //--------------------------------------------------------------------
        void CFormSelectStructure_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------------------------------
        private void InitComboStructure(Type typeObjet)
		{
			CListeObjetsDonnees listeStructures = new CListeObjetsDonnees( CSc2iWin32DataClient.ContexteCourant, typeof(C2iStructureExportInDB));
			listeStructures.Filtre = new CFiltreDataAvance(C2iStructureExportInDB.c_nomTable, "StringTypeElements = @1", typeObjet.ToString() );
			m_cmbStructures.Init(listeStructures, "Libelle", true);
			m_cmbStructures.AssureRemplissage();
			m_cmbStructures.SelectedValue = null;
			m_panelMultiStructure.Init ( MutliStructureFromCombo, true );
		}

		//--------------------------------------------------------------------
		private CMultiStructureExport MutliStructureFromCombo
		{
			get
			{
				CMultiStructureExport multi = m_cmbStructures.SelectedValue!=null?((C2iStructureExportInDB)m_cmbStructures.SelectedValue).MultiStructure:null;
				if ( multi != null )
					multi.ContexteDonnee = m_listeObjets.ContexteDonnee;
				else
					multi = new CMultiStructureExport( m_listeObjets.ContexteDonnee );
				return multi;
			
			}
		}

		

		public virtual IExporteurDataset Exporteur
		{
			get
			{
				return m_panelOptionsExport.Exporteur;
			}
			set
			{
				m_panelOptionsExport.Exporteur = value;
			}
		}

		private CMultiStructureExport m_multistructure = null;
		private IDestinationExport m_destinationExport = null;
		private IExporteurDataset m_exporteur = null;
		private void StartExport()
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				try
				{
					CFormProgression.Indicateur.SetBornesSegment ( 0, 100);
					CFormProgression.Indicateur.PushSegment(0, 80);
					CFormProgression.Indicateur.SetInfo(I.T("Generating data|232"));
				}
				catch{}
				result = m_multistructure.GetDataSet ( false, m_listeObjets, CFormProgression.Indicateur );
				try
				{
					CFormProgression.Indicateur.PopSegment();
					CFormProgression.Indicateur.PushSegment ( 80,100);
                    CFormProgression.Indicateur.SetInfo(I.T("Generating data|232"));
				}
				catch{}
				if (!result)
				{
					CFormAlerte.Afficher(result);
					return;
				}

				DataSet ds = (DataSet)result.Data;
			

				result = m_exporteur.Export(ds, m_destinationExport);
				try
				{
					CFormProgression.EndIndicateur(CFormProgression.Indicateur);
				}
				catch{}
				ds.Dispose();
				CFormAlerte.Afficher(I.T("Export completed with succes|225"));
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error during data export|226"));
			}
			if (!result)
				CFormAlerte.Afficher(result);
		}

		private void m_btnOK_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CResultAErreur.True;

			IDestinationExport destination = m_panelOptionsExport.DestinationExport;
			if ( destination == null || !destination.EstValidePourExport())
			{
				result.EmpileErreur(I.T("Export destination is not valid|227"));
				CFormAlerte.Afficher(result);
				return;
			}
			m_destinationExport = destination;

			m_exporteur = Exporteur;

			
			//C2iStructureExportInDB structure = (C2iStructureExportInDB)m_cmbStructures.SelectedValue;
			m_multistructure = m_panelMultiStructure.MultiStructure;
			if (m_multistructure == null)
			{
				result.EmpileErreur(I.T("Invalid structure|228"));
				CFormAlerte.Afficher(result);
				return;
			}
			m_multistructure.ContexteDonnee = CSc2iWin32DataClient.ContexteCourant;


			CFormProgression.StartThreadWithProgress (I.T("Exporting data|229"), new System.Threading.ThreadStart ( StartExport ) );

			/*

			result = structure.GetDataSet ( false, m_listeObjets, null );
			
			//result = structure.Export(m_listeObjets.ContexteDonnee.IdSession, m_listeObjets, ref ds, null);
			
			if (!result)
			{
				CFormAlerte.Afficher(result);
				return;
			}

			DataSet ds = (DataSet)result.Data;
			
			CDestinationExportFile dest = new CDestinationExportFile(m_panelOptions.FileName);

			result = Exporteur.Export(ds, dest);
			if (!result)
				CFormAlerte.Afficher(result);
			else
				CFormAlerte.Afficher("Exportation des données réussie");

			ds.Dispose();*/
			Close();
		}

		public static void Export(CListeObjetsDonnees liste)
		{
			CFormSelectStructure frm = new CFormSelectStructure(liste);
			frm.InitComboStructure();
			frm.ShowDialog();
		}

		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void m_cmbStructures_SelectedValueChanged(object sender, System.EventArgs e)
		{
			//m_panelEditStructure.ReloadTree(m_listeObjets.TypeObjets);
			if (m_cmbStructures.SelectedValue==null)
			{
				C2iStructureExport structure = new C2iStructureExport();
				structure.TypeSource = m_listeObjets.TypeObjets;
				m_panelEditStructure.StructureExport = structure;

			}
			else
			{
				m_panelMultiStructure.Init ( MutliStructureFromCombo );
			}
			///m_panelEditStructure.InitChamps();
			m_panelEditStructure.ComboTypeLockEdition = true;
		}

		private void c2iTabControl1_SelectionChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
