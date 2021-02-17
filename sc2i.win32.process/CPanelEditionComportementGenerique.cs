using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Windows.Forms;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.win32.data.navigation;
using sc2i.common;
using sc2i.win32.data;
using sc2i.win32.common;
using sc2i.process;



namespace sc2i.win32.process
{
	public class CPanelEditionComportementGenerique : UserControl, IControlALockEdition
	{
        private CComportementGenerique m_comportement = null;

        private CExtLinkField m_extLinkField;
        private CExtStyle m_extStyle;
        private CExtModeEdition m_gestionnaireModeEdition;
		
		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.C2iTextBox m_txtLibelle;
		private sc2i.win32.common.ListViewAutoFilled m_listViewRelationsComptabilites;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn8;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.ComponentModel.IContainer components = null;

		private sc2i.win32.common.ListViewAutoFilled m_listViewRelationElement;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn5;
		private sc2i.win32.common.C2iTabControl m_tab;
		private Crownwood.Magic.Controls.TabPage m_tabPageToRemove;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private System.Windows.Forms.Label label2;
		private CPanelDefinisseurEvenements m_panelEvenements;
		private Crownwood.Magic.Controls.TabPage m_pageEvenements;

		public CPanelEditionComportementGenerique()
			:base()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}
		
		//-------------------------------------------------------------------------

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

		#region Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtLibelle = new sc2i.win32.common.C2iTextBox();
            this.m_listViewRelationsComptabilites = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn8 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageEvenements = new Crownwood.Magic.Controls.TabPage();
            this.m_panelEvenements = new sc2i.win32.process.CPanelDefinisseurEvenements();
            this.m_tabPageToRemove = new Crownwood.Magic.Controls.TabPage();
            this.m_listViewRelationElement = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.listViewAutoFilledColumn5 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_tab.SuspendLayout();
            this.m_pageEvenements.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.m_extLinkField.SetLinkField(this.label1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.label1, false);
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 61;
            this.label1.Text = "Label|50";
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_extLinkField.SetLinkField(this.m_txtLibelle, "Libelle");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_txtLibelle, true);
            this.m_txtLibelle.Location = new System.Drawing.Point(88, 8);
            this.m_txtLibelle.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLibelle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(464, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelle.TabIndex = 2;
            this.m_txtLibelle.Text = "[Libelle]";
            // 
            // m_listViewRelationsComptabilites
            // 
            this.m_listViewRelationsComptabilites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_listViewRelationsComptabilites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn8});
            this.m_listViewRelationsComptabilites.EnableCustomisation = true;
            this.m_listViewRelationsComptabilites.FullRowSelect = true;
            this.m_listViewRelationsComptabilites.HideSelection = false;
            this.m_extLinkField.SetLinkField(this.m_listViewRelationsComptabilites, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_listViewRelationsComptabilites, false);
            this.m_listViewRelationsComptabilites.Location = new System.Drawing.Point(8, 40);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_listViewRelationsComptabilites, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_listViewRelationsComptabilites.Name = "m_listViewRelationsComptabilites";
            this.m_listViewRelationsComptabilites.Size = new System.Drawing.Size(360, 208);
            this.m_extStyle.SetStyleBackColor(this.m_listViewRelationsComptabilites, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_listViewRelationsComptabilites, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_listViewRelationsComptabilites.TabIndex = 3;
            this.m_listViewRelationsComptabilites.UseCompatibleStateImageBehavior = false;
            this.m_listViewRelationsComptabilites.View = System.Windows.Forms.View.Details;
            // 
            // listViewAutoFilledColumn8
            // 
            this.listViewAutoFilledColumn8.Field = "Comptabilite.Libelle";
            this.listViewAutoFilledColumn8.PrecisionWidth = 0;
            this.listViewAutoFilledColumn8.ProportionnalSize = false;
            this.listViewAutoFilledColumn8.Text = "Comptabilité.Libellé";
            this.listViewAutoFilledColumn8.Visible = true;
            this.listViewAutoFilledColumn8.Width = 300;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbTypeElements);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtLibelle);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.c2iPanelOmbre1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.c2iPanelOmbre1, false);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 3);
            this.c2iPanelOmbre1.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanelOmbre1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(634, 72);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 0;
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.IsLink = false;
            this.m_extLinkField.SetLinkField(this.m_cmbTypeElements, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_cmbTypeElements, false);
            this.m_cmbTypeElements.Location = new System.Drawing.Point(88, 32);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeElements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(464, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeElements.TabIndex = 4007;
            // 
            // label2
            // 
            this.m_extLinkField.SetLinkField(this.label2, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.label2, false);
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4006;
            this.label2.Text = "Target|20106";
            // 
            // m_tab
            // 
            this.m_tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.ControlBottomOffset = 16;
            this.m_tab.ControlRightOffset = 16;
            this.m_tab.ForeColor = System.Drawing.Color.Black;
            this.m_tab.IDEPixelArea = false;
            this.m_extLinkField.SetLinkField(this.m_tab, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_tab, false);
            this.m_tab.Location = new System.Drawing.Point(8, 81);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tab, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 0;
            this.m_tab.SelectedTab = this.m_tabPageToRemove;
            this.m_tab.Size = new System.Drawing.Size(822, 449);
            this.m_extStyle.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tab.TabIndex = 4003;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_tabPageToRemove,
            this.m_pageEvenements});
            this.m_tab.TextColor = System.Drawing.Color.Black;
            // 
            // m_pageEvenements
            // 
            this.m_pageEvenements.Controls.Add(this.m_panelEvenements);
            this.m_extLinkField.SetLinkField(this.m_pageEvenements, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_pageEvenements, false);
            this.m_pageEvenements.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageEvenements, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageEvenements.Name = "m_pageEvenements";
            this.m_pageEvenements.Selected = false;
            this.m_pageEvenements.Size = new System.Drawing.Size(806, 408);
            this.m_extStyle.SetStyleBackColor(this.m_pageEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageEvenements.TabIndex = 10;
            this.m_pageEvenements.Title = "Event|20108";
            // 
            // m_panelEvenements
            // 
            this.m_panelEvenements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_extLinkField.SetLinkField(this.m_panelEvenements, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelEvenements, false);
            this.m_panelEvenements.Location = new System.Drawing.Point(0, 0);
            this.m_panelEvenements.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEvenements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelEvenements.Name = "m_panelEvenements";
            this.m_panelEvenements.Size = new System.Drawing.Size(806, 408);
            this.m_extStyle.SetStyleBackColor(this.m_panelEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEvenements.TabIndex = 0;
            // 
            // m_tabPageToRemove
            // 
            this.m_extLinkField.SetLinkField(this.m_tabPageToRemove, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_tabPageToRemove, false);
            this.m_tabPageToRemove.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tabPageToRemove, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabPageToRemove.Name = "m_tabPageToRemove";
            this.m_tabPageToRemove.Size = new System.Drawing.Size(806, 408);
            this.m_extStyle.SetStyleBackColor(this.m_tabPageToRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_tabPageToRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tabPageToRemove.TabIndex = 2;
            this.m_tabPageToRemove.Title = "Page|20107";
            // 
            // m_listViewRelationElement
            // 
            this.m_listViewRelationElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_listViewRelationElement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn1,
            this.listViewAutoFilledColumn5});
            this.m_listViewRelationElement.EnableCustomisation = false;
            this.m_listViewRelationElement.FullRowSelect = true;
            this.m_listViewRelationElement.HideSelection = false;
            this.m_extLinkField.SetLinkField(this.m_listViewRelationElement, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_listViewRelationElement, false);
            this.m_listViewRelationElement.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_listViewRelationElement, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_listViewRelationElement.MultiSelect = false;
            this.m_listViewRelationElement.Name = "m_listViewRelationElement";
            this.m_listViewRelationElement.Size = new System.Drawing.Size(376, 272);
            this.m_extStyle.SetStyleBackColor(this.m_listViewRelationElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_listViewRelationElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_listViewRelationElement.TabIndex = 14;
            this.m_listViewRelationElement.UseCompatibleStateImageBehavior = false;
            this.m_listViewRelationElement.View = System.Windows.Forms.View.Details;
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "Libelle";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Label|50";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 203;
            // 
            // listViewAutoFilledColumn5
            // 
            this.listViewAutoFilledColumn5.Field = "TypeElementsConvivial";
            this.listViewAutoFilledColumn5.PrecisionWidth = 0;
            this.listViewAutoFilledColumn5.ProportionnalSize = false;
            this.listViewAutoFilledColumn5.Text = "Type|20109";
            this.listViewAutoFilledColumn5.Visible = true;
            this.listViewAutoFilledColumn5.Width = 156;
            // 
            // m_extLinkField
            // 
            this.m_extLinkField.SourceTypeString = "";
            // 
            // CPanelEditionComportementGenerique
            // 
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.m_extLinkField.SetLinkField(this, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this, false);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionComportementGenerique";
            this.Size = new System.Drawing.Size(830, 530);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CPanelEditionComportementGenerique_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageEvenements.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//-------------------------------------------------------------------------
		private CComportementGenerique Comportement
		{
			get
			{
                return m_comportement;
			}
		}
		//-------------------------------------------------------------------------
		public  CResultAErreur InitChamps( CComportementGenerique comportement)
		{
            m_comportement = comportement;
            CResultAErreur result = CResultAErreur.True;

            result = m_extLinkField.FillDialogFromObjet(Comportement);

			InitComboType();
			if ( Comportement.TypeCible != null )
				m_cmbTypeElements.SelectedValue = Comportement.TypeCible;

			m_panelEvenements.InitChamps ( Comportement );
			

		
			return result;
			
		}

	
		//------------------------------------------------------------------------
		public CResultAErreur MAJ_Champs() 
		{
            CResultAErreur result = m_extLinkField.FillObjetFromDialog(Comportement);
			if ( !result )
				return result;
			if ( m_cmbTypeElements.SelectedValue is Type && m_cmbTypeElements.SelectedValue != typeof(DBNull))
				Comportement.TypeCible = (Type)m_cmbTypeElements.SelectedValue;
			result = m_panelEvenements.MAJ_Champs();
			return result;
		}

		private void CPanelEditionComportementGenerique_Load(object sender, System.EventArgs e)
		{
			if ( m_tab.TabPages.Count > 0 && m_tab.TabPages[0] == m_tabPageToRemove )
				m_tab.TabPages.RemoveAt(0);
		}

		//-------------------------------------------------------------------------
		private CResultAErreur InitComboType()
		{
			CResultAErreur result = CResultAErreur.True;
		
			ArrayList classes = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
            classes.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("(None)|20111")));
            m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.DataSource = classes;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";
			return result;
        }







        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}

