using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.win32.common;
using sc2i.process;
using sc2i.win32.data.navigation;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CPanelDefinisseurEvenements.
	/// </summary>
	public class CPanelDefinisseurEvenements : System.Windows.Forms.UserControl, IControlALockEdition
	{
		private ArrayList m_listeComportements = new ArrayList();
		private IDefinisseurEvenements m_definisseur = null;
		private System.Windows.Forms.Panel m_panelComportementsInduits;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private sc2i.win32.common.CWndLinkStd m_lnkAjouter;
		private sc2i.win32.common.GlacialList m_wndListeComportements;
		private sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees m_cmbComportement;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
		private sc2i.win32.data.navigation.CPanelListeSpeedStandard m_panelEvenements;
        protected CExtStyle m_ExtStyle;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelDefinisseurEvenements()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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

        private CResultAErreur EditeEvenementPopup(CObjetDonnee objet)
        {
            CEvenement evenement = objet as CEvenement;
            if ( evenement != null )
                CFormEditionEvenementPopup.EditeEvenement(evenement);
            return CResultAErreur.True;

        }

        private CResultAErreur CreeEvenementPopup(CListeObjetsDonnees lstEvenements)
        {
            CResultAErreur result = CResultAErreur.True;
            CEvenement evenement = new CEvenement(m_definisseur.ContexteDonnee);
            evenement.CreateNew();
            evenement.Definisseur = m_definisseur;
            evenement.TypeCible = m_definisseur.TypesCibleEvenement[0];
            if (!CFormEditionEvenementPopup.EditeEvenement(evenement))
                evenement.CancelCreate();
            else
            {
                result = evenement.CommitEdit();
                if (!result)
                    CFormAlerte.Afficher(result.Erreur);
                InitPanelEvenements();
            }
            return result;
                
        }

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelDefinisseurEvenements));
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn2 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn3 = new sc2i.win32.common.GLColumn();
            this.m_panelComportementsInduits = new System.Windows.Forms.Panel();
            this.m_wndListeComportements = new sc2i.win32.common.GlacialList();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_cmbComportement = new sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelEvenements = new sc2i.win32.data.navigation.CPanelListeSpeedStandard();
            this.m_ExtStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelComportementsInduits.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelComportementsInduits
            // 
            this.m_panelComportementsInduits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelComportementsInduits.Controls.Add(this.m_wndListeComportements);
            this.m_panelComportementsInduits.Controls.Add(this.m_lnkSupprimer);
            this.m_panelComportementsInduits.Controls.Add(this.m_lnkAjouter);
            this.m_panelComportementsInduits.Controls.Add(this.m_cmbComportement);
            this.m_panelComportementsInduits.Controls.Add(this.m_cmbTypeElements);
            this.m_panelComportementsInduits.Controls.Add(this.label3);
            this.m_panelComportementsInduits.Controls.Add(this.label2);
            this.m_panelComportementsInduits.Controls.Add(this.label1);
            this.m_panelComportementsInduits.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelComportementsInduits.ForeColor = System.Drawing.Color.Black;
            this.m_panelComportementsInduits.Location = new System.Drawing.Point(449, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelComportementsInduits, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelComportementsInduits.Name = "m_panelComportementsInduits";
            this.m_panelComportementsInduits.Size = new System.Drawing.Size(344, 264);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelComportementsInduits, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelComportementsInduits, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelComportementsInduits.TabIndex = 4;
            // 
            // m_wndListeComportements
            // 
            this.m_wndListeComportements.AllowColumnResize = true;
            this.m_wndListeComportements.AllowMultiselect = false;
            this.m_wndListeComportements.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeComportements.AlternatingColors = false;
            this.m_wndListeComportements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeComportements.AutoHeight = true;
            this.m_wndListeComportements.AutoSort = true;
            this.m_wndListeComportements.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeComportements.CanChangeActivationCheckBoxes = false;
            this.m_wndListeComportements.CheckBoxes = false;
            this.m_wndListeComportements.CheckedItems = ((System.Collections.ArrayList)(resources.GetObject("m_wndListeComportements.CheckedItems")));
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Column1";
            glColumn1.Propriete = "Libelle";
            glColumn1.Text = "Behavior|821";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 230;
            glColumn2.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn2.ActiveControlItems")));
            glColumn2.BackColor = System.Drawing.Color.Transparent;
            glColumn2.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn2.ForColor = System.Drawing.Color.Black;
            glColumn2.ImageIndex = -1;
            glColumn2.IsCheckColumn = false;
            glColumn2.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn2.Name = "Column2";
            glColumn2.Propriete = "TypeCibleConvivial";
            glColumn2.Text = "Target|820";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 100;
            this.m_wndListeComportements.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1,
            glColumn2});
            this.m_wndListeComportements.ContexteUtilisation = "";
            this.m_wndListeComportements.EnableCustomisation = false;
            this.m_wndListeComportements.FocusedItem = null;
            this.m_wndListeComportements.FullRowSelect = true;
            this.m_wndListeComportements.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_wndListeComportements.GridColor = System.Drawing.Color.Gray;
            this.m_wndListeComportements.HasImages = false;
            this.m_wndListeComportements.HeaderHeight = 22;
            this.m_wndListeComportements.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeComportements.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeComportements.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeComportements.HeaderVisible = true;
            this.m_wndListeComportements.HeaderWordWrap = false;
            this.m_wndListeComportements.HotColumnIndex = -1;
            this.m_wndListeComportements.HotItemIndex = -1;
            this.m_wndListeComportements.HotTracking = false;
            this.m_wndListeComportements.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeComportements.ImageList = null;
            this.m_wndListeComportements.ItemHeight = 17;
            this.m_wndListeComportements.ItemWordWrap = false;
            this.m_wndListeComportements.ListeSource = null;
            this.m_wndListeComportements.Location = new System.Drawing.Point(8, 104);
            this.m_wndListeComportements.MaxHeight = 17;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeComportements, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeComportements.Name = "m_wndListeComportements";
            this.m_wndListeComportements.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeComportements.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeComportements.ShowBorder = true;
            this.m_wndListeComportements.ShowFocusRect = false;
            this.m_wndListeComportements.Size = new System.Drawing.Size(328, 152);
            this.m_wndListeComportements.SortIndex = 0;
            this.m_ExtStyle.SetStyleBackColor(this.m_wndListeComportements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_wndListeComportements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeComportements.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeComportements.TabIndex = 4009;
            this.m_wndListeComportements.Text = "glacialList1";
            this.m_wndListeComportements.TrierAuClicSurEnteteColonne = true;
            this.m_wndListeComportements.DoubleClick += new System.EventHandler(this.m_wndListeComportements_DoubleClick);
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(88, 78);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.Size = new System.Drawing.Size(72, 26);
            this.m_ExtStyle.SetStyleBackColor(this.m_lnkSupprimer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_lnkSupprimer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkSupprimer.TabIndex = 4008;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(8, 78);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.Size = new System.Drawing.Size(64, 24);
            this.m_ExtStyle.SetStyleBackColor(this.m_lnkAjouter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_lnkAjouter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAjouter.TabIndex = 4007;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // m_cmbComportement
            // 
            this.m_cmbComportement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbComportement.ComportementLinkStd = true;
            this.m_cmbComportement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbComportement.ElementSelectionne = null;
            this.m_cmbComportement.IsLink = true;
            this.m_cmbComportement.LinkProperty = "";
            this.m_cmbComportement.ListDonnees = null;
            this.m_cmbComportement.Location = new System.Drawing.Point(88, 56);
            this.m_cmbComportement.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbComportement, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbComportement.Name = "m_cmbComportement";
            this.m_cmbComportement.NullAutorise = true;
            this.m_cmbComportement.ProprieteAffichee = null;
            this.m_cmbComportement.ProprieteParentListeObjets = null;
            this.m_cmbComportement.SelectionneurParent = null;
            this.m_cmbComportement.Size = new System.Drawing.Size(248, 21);
            this.m_ExtStyle.SetStyleBackColor(this.m_cmbComportement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_cmbComportement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbComportement.TabIndex = 4006;
            this.m_cmbComportement.TextNull = "(Specific)";
            this.m_cmbComportement.Tri = true;
            this.m_cmbComportement.TypeFormEdition = null;
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.IsLink = false;
            this.m_cmbTypeElements.Location = new System.Drawing.Point(88, 32);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeElements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(248, 21);
            this.m_ExtStyle.SetStyleBackColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeElements.TabIndex = 4005;
            this.m_cmbTypeElements.SelectedValueChanged += new System.EventHandler(this.m_cmbTypeElements_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 58);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.m_ExtStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 2;
            this.label3.Text = "Behavior|821";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.m_ExtStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target|820";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 21);
            this.m_ExtStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Induced behaviors|819";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(446, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 264);
            this.m_ExtStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // m_panelEvenements
            // 
            this.m_panelEvenements.AllowArbre = true;
            this.m_panelEvenements.AllowCustomisation = true;
            glColumn3.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn3.ActiveControlItems")));
            glColumn3.BackColor = System.Drawing.Color.Transparent;
            glColumn3.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn3.ForColor = System.Drawing.Color.Black;
            glColumn3.ImageIndex = -1;
            glColumn3.IsCheckColumn = false;
            glColumn3.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn3.Name = "Name";
            glColumn3.Propriete = "Libelle";
            glColumn3.Text = "Label|50";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 400;
            this.m_panelEvenements.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn3});
            this.m_panelEvenements.ContexteUtilisation = "";
            this.m_panelEvenements.ControlFiltreStandard = null;
            this.m_panelEvenements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEvenements.ElementSelectionne = null;
            this.m_panelEvenements.EnableCustomisation = true;
            this.m_panelEvenements.FiltreDeBase = null;
            this.m_panelEvenements.FiltreDeBaseEnAjout = false;
            this.m_panelEvenements.FiltrePrefere = null;
            this.m_panelEvenements.FiltreRapide = null;
            this.m_panelEvenements.HasImages = false;
            this.m_panelEvenements.ListeObjets = null;
            this.m_panelEvenements.Location = new System.Drawing.Point(0, 0);
            this.m_panelEvenements.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEvenements, sc2i.win32.common.TypeModeEdition.DisableSurEdition);
            this.m_panelEvenements.ModeQuickSearch = false;
            this.m_panelEvenements.ModeSelection = false;
            this.m_panelEvenements.MultiSelect = false;
            this.m_panelEvenements.Name = "m_panelEvenements";
            this.m_panelEvenements.Navigateur = null;
            this.m_panelEvenements.ProprieteObjetAEditer = null;
            this.m_panelEvenements.QuickSearchText = "";
            this.m_panelEvenements.Size = new System.Drawing.Size(446, 264);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEvenements.TabIndex = 6;
            this.m_panelEvenements.TrierAuClicSurEnteteColonne = true;
            this.m_panelEvenements.UseCheckBoxes = false;
            this.m_panelEvenements.OnNewObjetDonnee += new sc2i.win32.data.navigation.OnNewObjetDonneeEventHandler(this.m_panelEvenements_OnNewObjetDonnee);
            // 
            // CPanelDefinisseurEvenements
            // 
            this.Controls.Add(this.m_panelEvenements);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelComportementsInduits);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelDefinisseurEvenements";
            this.Size = new System.Drawing.Size(793, 264);
            this.m_ExtStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelComportementsInduits.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public CResultAErreur InitChamps ( IDefinisseurEvenements definisseur )
		{
			CResultAErreur result = CResultAErreur.True;
			m_definisseur = definisseur;
            InitPanelEvenements();

			m_listeComportements = new ArrayList( definisseur.ComportementsInduits );
			m_wndListeComportements.ListeSource = m_listeComportements;
			m_wndListeComportements.Refresh();
			InitComboType();
			InitComboComportements();
			return result;
		}

        //-------------------------------------------------------------------------
        private void InitPanelEvenements()
        {
            CReferenceTypeFormBuiltIn refTypeForm = CFormFinder.GetRefFormToEdit(typeof(CEvenement)) as CReferenceTypeFormBuiltIn;
            m_panelEvenements.InitFromListeObjets(
                m_definisseur.Evenements,
                typeof(CEvenement),
                refTypeForm != null ? refTypeForm.TypeForm : null,
                null,
                null);

            //S'il n'y a de navigateur, tout se passe en popup
            if (CFormNavigateurPopup.FindNavigateur(this) == null)
            {
                m_gestionnaireModeEdition.SetModeEdition(m_panelEvenements, TypeModeEdition.EnableSurEdition);
                m_panelEvenements.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
                m_panelEvenements.AjouterElement = new CPanelListeSpeedStandard.AjouterElementDelegate(CreeEvenementPopup);
                m_panelEvenements.TraiterModificationElement = new CPanelListeSpeedStandard.ModifierElementDelegate(EditeEvenementPopup);
            }
        }

		//-------------------------------------------------------------------------
		private CResultAErreur InitComboType()
		{
			CResultAErreur result = CResultAErreur.True;
		
			CInfoClasseDynamique[] classes;
			Type[] types = m_definisseur.TypesCibleEvenement;
			classes = new CInfoClasseDynamique[types.Length];
			int nIndex = 0;
			foreach ( Type tp in types )
				classes[nIndex++] = new CInfoClasseDynamique(tp, DynamicClassAttribute.GetNomConvivial ( tp ) );
			m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.DataSource = classes;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";
			return result;
		}

		//-------------------------------------------------------------------------
		private void InitComboComportements()
		{
            CReferenceTypeFormBuiltIn refForm = CFormFinder.GetRefFormToEdit(typeof(CComportementGenerique)) as CReferenceTypeFormBuiltIn;
			if ( m_cmbTypeElements.SelectedValue is Type )
			{
                
				m_cmbComportement.Init (
					typeof(CComportementGenerique),
					new CFiltreData ( CComportementGenerique.c_champTypeElementCible+"=@1",
					m_cmbTypeElements.SelectedValue.ToString() ),
					"Libelle",
					refForm != null?refForm.TypeForm:null,
					true );
			}
			else
			{
				m_cmbComportement.Init ( 
					typeof(CComportementGenerique),
					new CFiltreDataImpossible(),
					"Libelle",
                    refForm != null ? refForm.TypeForm : null, 
					true );
				m_cmbComportement.AssureRemplissage();
			}

		}

		public CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;

			Hashtable tableComportementsAMettre = new Hashtable();
			foreach ( CComportementGenerique comportement in m_listeComportements )
				tableComportementsAMettre[comportement] = true;

			ArrayList lstToDelete = new ArrayList();
			foreach  ( CComportementGenerique comportement in m_definisseur.ComportementsInduits )
			{
				if ( tableComportementsAMettre[comportement] ==null )
					lstToDelete.Add ( comportement );
				else
					tableComportementsAMettre.Remove(comportement);
			}
			foreach ( CComportementGenerique comportement in lstToDelete )
				CUtilDefinisseurEvenement.RemoveComportementInduit ( m_definisseur, comportement );
			foreach ( CComportementGenerique comportement in tableComportementsAMettre.Keys )
				CUtilDefinisseurEvenement.AddComportementInduit ( m_definisseur, comportement );

			return result;
		}

		/// //////////////////////////////////////////////
		private void m_panelEvenements_OnNewObjetDonnee(object sender, sc2i.data.CObjetDonnee nouvelObjet, ref bool bCancel)
		{
			if ( nouvelObjet is CEvenement ) 
			{
				((CEvenement)nouvelObjet).Definisseur = m_definisseur;
				if ( m_definisseur.TypesCibleEvenement.Length > 0 )
					((CEvenement)nouvelObjet).TypeCible = m_definisseur.TypesCibleEvenement[0];
			}
		}

		/// //////////////////////////////////////////////
		private void m_lnkAjouter_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_cmbComportement.SelectedValue is CComportementGenerique )
			{
				CComportementGenerique comportement = (CComportementGenerique)m_cmbComportement.SelectedValue;
				if ( !m_listeComportements.Contains ( comportement ) )
				{
					m_listeComportements.Add ( comportement );
					m_wndListeComportements.ListeSource = m_listeComportements;
					m_wndListeComportements.Refresh();
				}
			}
		}

		/// //////////////////////////////////////////////
		private void m_lnkSupprimer_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_wndListeComportements.SelectedItems.Count != 0 )
			{
				m_listeComportements.Remove ( m_wndListeComportements.SelectedItems[0]);
				m_wndListeComportements.ListeSource = m_listeComportements;
				m_wndListeComportements.Refresh();

			}
		}

		private void m_cmbTypeElements_SelectedValueChanged(object sender, System.EventArgs e)
		{
			InitComboComportements();
		}

		private void m_wndListeComportements_DoubleClick(object sender, System.EventArgs e)
		{
			Point pt = Cursor.Position;
			pt = m_wndListeComportements.PointToClient ( pt );
			if( m_wndListeComportements.SelectedItems.Count == 1 )
			{
				CComportementGenerique comportement = (CComportementGenerique)m_wndListeComportements.SelectedItems[0];
                CReferenceTypeForm refForm = CFormFinder.GetRefFormToEdit(typeof(CComportementGenerique));
                if (refForm != null)
                {
                    CFormEditionStandard form = refForm.GetForm(comportement) as CFormEditionStandard;
                    if (form != null)
                        CSc2iWin32DataNavigation.Navigateur.AffichePage(form);
                }
			}
		}


		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs());
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion
	}
}
