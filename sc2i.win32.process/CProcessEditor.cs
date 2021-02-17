using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.process;
using sc2i.drawing;	
using sc2i.data.dynamic;
using sc2i.win32.common;
using sc2i.win32.data.dynamic;
using sc2i.expression;
using sc2i.data;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CProcessEditor.
	/// </summary>
	public class CProcessEditor : System.Windows.Forms.UserControl, IControlALockEdition
	{


		#region Code généré par le Concepteur de composants

		private System.Windows.Forms.RadioButton m_chkCurseur;
		private System.Windows.Forms.Panel m_panelBoutons;
		private System.Windows.Forms.RadioButton m_chkCondition;
		private System.Windows.Forms.RadioButton m_chkZoom;
		private System.Windows.Forms.RadioButton m_chkLien;
		private System.Windows.Forms.Panel m_panelCadreEdition;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel m_panelInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_txtLibelle;
		private System.Windows.Forms.Button m_btnProprietes;
		private System.Windows.Forms.ListView m_wndListeLiens;
		private System.Windows.Forms.Label Label12;
		private System.Windows.Forms.ColumnHeader m_colSens;
		private System.Windows.Forms.ColumnHeader m_colNom;
		private sc2i.win32.common.CComboboxAutoFilled m_comboActions;
		private System.Windows.Forms.Panel m_panelHautDroit;
		private System.Windows.Forms.Panel m_panelBasDroit;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn2;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeVariables;
		private sc2i.win32.common.CWndLinkStd m_btnAjouter;
		private System.Windows.Forms.ContextMenu m_menuAjoutVariable;
		private System.Windows.Forms.MenuItem m_menuVariableSimple;
		private sc2i.win32.process.CControlEditionProcess m_panelEdition;
		private System.Windows.Forms.ContextMenu m_menuAjoutAction;
		private System.Windows.Forms.Panel m_panelFondDroite;
		private System.Windows.Forms.Panel m_panelEntete;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private System.Windows.Forms.MenuItem m_menuVariableCalculee;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		private System.Windows.Forms.RadioButton m_btnJonction;
		private System.Windows.Forms.MenuItem m_menuVariableEntite;
		private sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
		private sc2i.win32.common.CWndLinkStd m_lnkDetail;
		private System.Windows.Forms.CheckBox m_chkAssynchrone;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private System.Windows.Forms.RadioButton m_btnAction;
		private System.Windows.Forms.CheckBox m_chkTableau;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnCopy;
		private System.Windows.Forms.ContextMenu m_menuVariable;
		private System.Windows.Forms.MenuItem m_menuVariableDeRetour;
        private RadioButton m_rbntEntree;
        private CheckBox m_chkModeTransactionnel;
        /// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

	

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

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CProcessEditor));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_chkCurseur = new System.Windows.Forms.RadioButton();
            this.m_panelBoutons = new System.Windows.Forms.Panel();
            this.m_rbntEntree = new System.Windows.Forms.RadioButton();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_btnJonction = new System.Windows.Forms.RadioButton();
            this.m_chkCondition = new System.Windows.Forms.RadioButton();
            this.m_chkLien = new System.Windows.Forms.RadioButton();
            this.m_btnAction = new System.Windows.Forms.RadioButton();
            this.m_chkZoom = new System.Windows.Forms.RadioButton();
            this.m_panelCadreEdition = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_panelInfo = new System.Windows.Forms.Panel();
            this.m_panelBasDroit = new System.Windows.Forms.Panel();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkDetail = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.listViewAutoFilledColumn2 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelHautDroit = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_comboActions = new sc2i.win32.common.CComboboxAutoFilled();
            this.Label12 = new System.Windows.Forms.Label();
            this.m_btnProprietes = new System.Windows.Forms.Button();
            this.m_wndListeLiens = new System.Windows.Forms.ListView();
            this.m_colSens = new System.Windows.Forms.ColumnHeader();
            this.m_colNom = new System.Windows.Forms.ColumnHeader();
            this.m_txtLibelle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_menuAjoutVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSimple = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculee = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_menuVariableEntite = new System.Windows.Forms.MenuItem();
            this.m_menuAjoutAction = new System.Windows.Forms.ContextMenu();
            this.m_panelFondDroite = new System.Windows.Forms.Panel();
            this.m_panelEntete = new System.Windows.Forms.Panel();
            this.m_chkModeTransactionnel = new System.Windows.Forms.CheckBox();
            this.m_chkTableau = new System.Windows.Forms.CheckBox();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_chkAssynchrone = new System.Windows.Forms.CheckBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_menuVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableDeRetour = new System.Windows.Forms.MenuItem();
            this.m_panelEdition = new sc2i.win32.process.CControlEditionProcess();
            this.m_panelBoutons.SuspendLayout();
            this.m_panelCadreEdition.SuspendLayout();
            this.m_panelInfo.SuspendLayout();
            this.m_panelBasDroit.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelHautDroit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelFondDroite.SuspendLayout();
            this.m_panelEntete.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_chkCurseur
            // 
            this.m_chkCurseur.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkCurseur.Checked = true;
            this.m_chkCurseur.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkCurseur.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkCurseur.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkCurseur.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkCurseur.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkCurseur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkCurseur.Image = ((System.Drawing.Image)(resources.GetObject("m_chkCurseur.Image")));
            this.m_chkCurseur.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkCurseur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkCurseur.Name = "m_chkCurseur";
            this.m_chkCurseur.Size = new System.Drawing.Size(32, 32);
            this.m_chkCurseur.TabIndex = 0;
            this.m_chkCurseur.TabStop = true;
            this.m_chkCurseur.CheckedChanged += new System.EventHandler(this.m_chkCurseur_CheckedChanged);
            // 
            // m_panelBoutons
            // 
            this.m_panelBoutons.Controls.Add(this.m_rbntEntree);
            this.m_panelBoutons.Controls.Add(this.m_btnOpen);
            this.m_panelBoutons.Controls.Add(this.m_btnSave);
            this.m_panelBoutons.Controls.Add(this.m_btnPaste);
            this.m_panelBoutons.Controls.Add(this.m_btnCopy);
            this.m_panelBoutons.Controls.Add(this.m_btnJonction);
            this.m_panelBoutons.Controls.Add(this.m_chkCondition);
            this.m_panelBoutons.Controls.Add(this.m_chkLien);
            this.m_panelBoutons.Controls.Add(this.m_btnAction);
            this.m_panelBoutons.Controls.Add(this.m_chkZoom);
            this.m_panelBoutons.Controls.Add(this.m_chkCurseur);
            this.m_panelBoutons.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelBoutons.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelBoutons, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBoutons.Name = "m_panelBoutons";
            this.m_panelBoutons.Size = new System.Drawing.Size(32, 430);
            this.m_panelBoutons.TabIndex = 0;
            // 
            // m_rbntEntree
            // 
            this.m_rbntEntree.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbntEntree.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_rbntEntree.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_rbntEntree.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_rbntEntree.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_rbntEntree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_rbntEntree.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_rbntEntree.Image = ((System.Drawing.Image)(resources.GetObject("m_rbntEntree.Image")));
            this.m_rbntEntree.Location = new System.Drawing.Point(0, 192);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_rbntEntree, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_rbntEntree.Name = "m_rbntEntree";
            this.m_rbntEntree.Size = new System.Drawing.Size(32, 32);
            this.m_rbntEntree.TabIndex = 29;
            this.m_rbntEntree.CheckedChanged += new System.EventHandler(this.m_rbntEntree_CheckedChanged);
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(4, 398);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.m_btnOpen.TabIndex = 28;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(4, 374);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.m_btnSave.TabIndex = 27;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(4, 342);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.m_btnPaste.TabIndex = 26;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(4, 318);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.m_btnCopy.TabIndex = 25;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_btnJonction
            // 
            this.m_btnJonction.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnJonction.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnJonction.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_btnJonction.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btnJonction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_btnJonction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_btnJonction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnJonction.Image = ((System.Drawing.Image)(resources.GetObject("m_btnJonction.Image")));
            this.m_btnJonction.Location = new System.Drawing.Point(0, 160);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnJonction, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnJonction.Name = "m_btnJonction";
            this.m_btnJonction.Size = new System.Drawing.Size(32, 32);
            this.m_btnJonction.TabIndex = 5;
            this.m_btnJonction.CheckedChanged += new System.EventHandler(this.m_btnJonction_CheckedChanged);
            // 
            // m_chkCondition
            // 
            this.m_chkCondition.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkCondition.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkCondition.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkCondition.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkCondition.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkCondition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkCondition.Image = ((System.Drawing.Image)(resources.GetObject("m_chkCondition.Image")));
            this.m_chkCondition.Location = new System.Drawing.Point(0, 128);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkCondition, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkCondition.Name = "m_chkCondition";
            this.m_chkCondition.Size = new System.Drawing.Size(32, 32);
            this.m_chkCondition.TabIndex = 2;
            this.m_chkCondition.CheckedChanged += new System.EventHandler(this.m_chkCondition_CheckedChanged);
            // 
            // m_chkLien
            // 
            this.m_chkLien.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkLien.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkLien.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkLien.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkLien.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkLien.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkLien.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkLien.Image = ((System.Drawing.Image)(resources.GetObject("m_chkLien.Image")));
            this.m_chkLien.Location = new System.Drawing.Point(0, 96);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkLien, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkLien.Name = "m_chkLien";
            this.m_chkLien.Size = new System.Drawing.Size(32, 32);
            this.m_chkLien.TabIndex = 1;
            this.m_chkLien.CheckedChanged += new System.EventHandler(this.m_chkLien_CheckedChanged);
            // 
            // m_btnAction
            // 
            this.m_btnAction.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnAction.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_btnAction.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btnAction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_btnAction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnAction.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAction.Image")));
            this.m_btnAction.Location = new System.Drawing.Point(0, 64);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAction, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAction.Name = "m_btnAction";
            this.m_btnAction.Size = new System.Drawing.Size(32, 32);
            this.m_btnAction.TabIndex = 5;
            this.m_btnAction.Click += new System.EventHandler(this.m_btnAction_Click);
            // 
            // m_chkZoom
            // 
            this.m_chkZoom.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkZoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkZoom.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkZoom.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkZoom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkZoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkZoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkZoom.Image = ((System.Drawing.Image)(resources.GetObject("m_chkZoom.Image")));
            this.m_chkZoom.Location = new System.Drawing.Point(0, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkZoom, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkZoom.Name = "m_chkZoom";
            this.m_chkZoom.Size = new System.Drawing.Size(32, 32);
            this.m_chkZoom.TabIndex = 3;
            this.m_chkZoom.CheckedChanged += new System.EventHandler(this.m_chkZoom_CheckedChanged);
            // 
            // m_panelCadreEdition
            // 
            this.m_panelCadreEdition.BackColor = System.Drawing.Color.White;
            this.m_panelCadreEdition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelCadreEdition.Controls.Add(this.m_panelEdition);
            this.m_panelCadreEdition.Controls.Add(this.splitter1);
            this.m_panelCadreEdition.Controls.Add(this.m_panelInfo);
            this.m_panelCadreEdition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCadreEdition.Location = new System.Drawing.Point(0, 48);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCadreEdition, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCadreEdition.Name = "m_panelCadreEdition";
            this.m_panelCadreEdition.Size = new System.Drawing.Size(608, 382);
            this.m_panelCadreEdition.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(387, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 380);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // m_panelInfo
            // 
            this.m_panelInfo.Controls.Add(this.m_panelBasDroit);
            this.m_panelInfo.Controls.Add(this.splitter2);
            this.m_panelInfo.Controls.Add(this.m_panelHautDroit);
            this.m_panelInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelInfo.Location = new System.Drawing.Point(390, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelInfo, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelInfo.Name = "m_panelInfo";
            this.m_panelInfo.Size = new System.Drawing.Size(216, 380);
            this.m_panelInfo.TabIndex = 2;
            // 
            // m_panelBasDroit
            // 
            this.m_panelBasDroit.Controls.Add(this.m_lnkSupprimer);
            this.m_panelBasDroit.Controls.Add(this.m_lnkDetail);
            this.m_panelBasDroit.Controls.Add(this.m_btnAjouter);
            this.m_panelBasDroit.Controls.Add(this.m_wndListeVariables);
            this.m_panelBasDroit.Controls.Add(this.panel2);
            this.m_panelBasDroit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelBasDroit.Location = new System.Drawing.Point(0, 219);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelBasDroit, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBasDroit.Name = "m_panelBasDroit";
            this.m_panelBasDroit.Size = new System.Drawing.Size(216, 161);
            this.m_panelBasDroit.TabIndex = 5;
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(128, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.Size = new System.Drawing.Size(80, 16);
            this.m_lnkSupprimer.TabIndex = 15;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // m_lnkDetail
            // 
            this.m_lnkDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDetail.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDetail.Location = new System.Drawing.Point(72, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkDetail, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkDetail.Name = "m_lnkDetail";
            this.m_lnkDetail.Size = new System.Drawing.Size(56, 16);
            this.m_lnkDetail.TabIndex = 14;
            this.m_lnkDetail.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkDetail.LinkClicked += new System.EventHandler(this.m_lnkDetail_LinkClicked);
            // 
            // m_btnAjouter
            // 
            this.m_btnAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouter.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAjouter.Name = "m_btnAjouter";
            this.m_btnAjouter.Size = new System.Drawing.Size(56, 16);
            this.m_btnAjouter.TabIndex = 13;
            this.m_btnAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouter.LinkClicked += new System.EventHandler(this.m_btnAjouter_LinkClicked);
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn1,
            this.listViewAutoFilledColumn2});
            this.m_wndListeVariables.EnableCustomisation = false;
            this.m_wndListeVariables.FullRowSelect = true;
            this.m_wndListeVariables.Location = new System.Drawing.Point(8, 48);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(200, 110);
            this.m_wndListeVariables.TabIndex = 11;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.DoubleClick += new System.EventHandler(this.m_wndListeVariables_DoubleClick);
            this.m_wndListeVariables.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListeVariables_MouseUp);
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "LibelleType";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Type|204";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 67;
            // 
            // listViewAutoFilledColumn2
            // 
            this.listViewAutoFilledColumn2.Field = "Nom";
            this.listViewAutoFilledColumn2.PrecisionWidth = 0;
            this.listViewAutoFilledColumn2.ProportionnalSize = false;
            this.listViewAutoFilledColumn2.Text = "Name|205";
            this.listViewAutoFilledColumn2.Visible = true;
            this.listViewAutoFilledColumn2.Width = 349;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 24);
            this.panel2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Variables|199";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 216);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(216, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // m_panelHautDroit
            // 
            this.m_panelHautDroit.Controls.Add(this.panel1);
            this.m_panelHautDroit.Controls.Add(this.m_comboActions);
            this.m_panelHautDroit.Controls.Add(this.Label12);
            this.m_panelHautDroit.Controls.Add(this.m_btnProprietes);
            this.m_panelHautDroit.Controls.Add(this.m_wndListeLiens);
            this.m_panelHautDroit.Controls.Add(this.m_txtLibelle);
            this.m_panelHautDroit.Controls.Add(this.label2);
            this.m_panelHautDroit.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHautDroit.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelHautDroit, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelHautDroit.Name = "m_panelHautDroit";
            this.m_panelHautDroit.Size = new System.Drawing.Size(216, 216);
            this.m_panelHautDroit.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 24);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Properties|198";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_comboActions
            // 
            this.m_comboActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_comboActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboActions.IsLink = false;
            this.m_comboActions.ListDonnees = null;
            this.m_comboActions.Location = new System.Drawing.Point(0, 24);
            this.m_comboActions.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_comboActions, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_comboActions.Name = "m_comboActions";
            this.m_comboActions.NullAutorise = false;
            this.m_comboActions.ProprieteAffichee = null;
            this.m_comboActions.Size = new System.Drawing.Size(216, 21);
            this.m_comboActions.TabIndex = 8;
            this.m_comboActions.TextNull = "(empty)";
            this.m_comboActions.Tri = true;
            this.m_comboActions.SelectedValueChanged += new System.EventHandler(this.m_comboActions_SelectedValueChanged);
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(8, 104);
            this.m_gestionnaireModeEdition.SetModeEdition(this.Label12, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(96, 16);
            this.Label12.TabIndex = 7;
            this.Label12.Text = "Links|201";
            // 
            // m_btnProprietes
            // 
            this.m_btnProprietes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnProprietes.Location = new System.Drawing.Point(64, 72);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnProprietes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnProprietes.Name = "m_btnProprietes";
            this.m_btnProprietes.Size = new System.Drawing.Size(80, 24);
            this.m_btnProprietes.TabIndex = 3;
            this.m_btnProprietes.Text = "Properties|198";
            this.m_btnProprietes.Click += new System.EventHandler(this.m_btnProprietes_Click);
            // 
            // m_wndListeLiens
            // 
            this.m_wndListeLiens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeLiens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colSens,
            this.m_colNom});
            this.m_wndListeLiens.FullRowSelect = true;
            this.m_wndListeLiens.Location = new System.Drawing.Point(8, 128);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeLiens, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeLiens.Name = "m_wndListeLiens";
            this.m_wndListeLiens.Size = new System.Drawing.Size(200, 80);
            this.m_wndListeLiens.TabIndex = 6;
            this.m_wndListeLiens.UseCompatibleStateImageBehavior = false;
            this.m_wndListeLiens.View = System.Windows.Forms.View.Details;
            this.m_wndListeLiens.SelectedIndexChanged += new System.EventHandler(this.m_wndListeLiens_SelectedIndexChanged);
            this.m_wndListeLiens.DoubleClick += new System.EventHandler(this.m_wndListeLiens_DoubleClick);
            this.m_wndListeLiens.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_wndListeLiens_KeyDown);
            // 
            // m_colSens
            // 
            this.m_colSens.Text = "Direction|202";
            this.m_colSens.Width = 70;
            // 
            // m_colNom
            // 
            this.m_colNom.Text = "Action|203";
            this.m_colNom.Width = 400;
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLibelle.Location = new System.Drawing.Point(64, 48);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLibelle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(144, 20);
            this.m_txtLibelle.TabIndex = 2;
            this.m_txtLibelle.TextChanged += new System.EventHandler(this.m_txtLibelle_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Label|200";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_menuAjoutVariable
            // 
            this.m_menuAjoutVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSimple,
            this.m_menuVariableCalculee,
            this.m_menuVariableSelection,
            this.m_menuVariableEntite});
            // 
            // m_menuVariableSimple
            // 
            this.m_menuVariableSimple.Index = 0;
            this.m_menuVariableSimple.Text = "Entered variable|10004";
            this.m_menuVariableSimple.Click += new System.EventHandler(this.m_menuVariableSimple_Click);
            // 
            // m_menuVariableCalculee
            // 
            this.m_menuVariableCalculee.Index = 1;
            this.m_menuVariableCalculee.Text = "calculated variable|10005";
            this.m_menuVariableCalculee.Click += new System.EventHandler(this.m_menuVariableCalculee_Click);
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Entity selection|10006";
            this.m_menuVariableSelection.Click += new System.EventHandler(this.m_menuVariableSelection_Click);
            // 
            // m_menuVariableEntite
            // 
            this.m_menuVariableEntite.Index = 3;
            this.m_menuVariableEntite.Text = "Entity|10007";
            this.m_menuVariableEntite.Click += new System.EventHandler(this.m_menuVariableEntite_Click);
            // 
            // m_panelFondDroite
            // 
            this.m_panelFondDroite.Controls.Add(this.m_panelCadreEdition);
            this.m_panelFondDroite.Controls.Add(this.m_panelEntete);
            this.m_panelFondDroite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFondDroite.Location = new System.Drawing.Point(32, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFondDroite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFondDroite.Name = "m_panelFondDroite";
            this.m_panelFondDroite.Size = new System.Drawing.Size(608, 430);
            this.m_panelFondDroite.TabIndex = 3;
            // 
            // m_panelEntete
            // 
            this.m_panelEntete.Controls.Add(this.m_chkModeTransactionnel);
            this.m_panelEntete.Controls.Add(this.m_chkTableau);
            this.m_panelEntete.Controls.Add(this.m_cmbTypeElements);
            this.m_panelEntete.Controls.Add(this.label4);
            this.m_panelEntete.Controls.Add(this.m_chkAssynchrone);
            this.m_panelEntete.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelEntete.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEntete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelEntete.Name = "m_panelEntete";
            this.m_panelEntete.Size = new System.Drawing.Size(608, 48);
            this.m_panelEntete.TabIndex = 3;
            // 
            // m_chkModeTransactionnel
            // 
            this.m_chkModeTransactionnel.AutoSize = true;
            this.m_chkModeTransactionnel.Location = new System.Drawing.Point(320, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkModeTransactionnel, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkModeTransactionnel.Name = "m_chkModeTransactionnel";
            this.m_chkModeTransactionnel.Size = new System.Drawing.Size(157, 17);
            this.m_chkModeTransactionnel.TabIndex = 12;
            this.m_chkModeTransactionnel.Text = "Transactionnal mode|20036";
            this.m_chkModeTransactionnel.UseVisualStyleBackColor = true;
            this.m_chkModeTransactionnel.CheckedChanged += new System.EventHandler(this.m_chkModeTransactionnel_CheckedChanged);
            // 
            // m_chkTableau
            // 
            this.m_chkTableau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkTableau.Location = new System.Drawing.Point(502, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTableau, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTableau.Name = "m_chkTableau";
            this.m_chkTableau.Size = new System.Drawing.Size(90, 19);
            this.m_chkTableau.TabIndex = 11;
            this.m_chkTableau.Text = "Array|196";
            this.m_chkTableau.CheckedChanged += new System.EventHandler(this.m_chkTableau_CheckedChanged);
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.IsLink = false;
            this.m_cmbTypeElements.Location = new System.Drawing.Point(128, 6);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeElements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(368, 21);
            this.m_cmbTypeElements.TabIndex = 2;
            this.m_cmbTypeElements.SelectedValueChanged += new System.EventHandler(this.m_cmbTypeElements_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Elements type|195";
            // 
            // m_chkAssynchrone
            // 
            this.m_chkAssynchrone.Location = new System.Drawing.Point(128, 31);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkAssynchrone, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkAssynchrone.Name = "m_chkAssynchrone";
            this.m_chkAssynchrone.Size = new System.Drawing.Size(167, 16);
            this.m_chkAssynchrone.TabIndex = 10;
            this.m_chkAssynchrone.Text = "Asynchronous execution|197";
            this.m_chkAssynchrone.CheckedChanged += new System.EventHandler(this.m_chkAssynchrone_CheckedChanged);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // m_menuVariable
            // 
            this.m_menuVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableDeRetour});
            // 
            // m_menuVariableDeRetour
            // 
            this.m_menuVariableDeRetour.Index = 0;
            this.m_menuVariableDeRetour.Text = "Returned variable|10008";
            this.m_menuVariableDeRetour.Click += new System.EventHandler(this.m_menuVariableDeRetour_Click);
            // 
            // m_panelEdition
            // 
            this.m_panelEdition.AllowDrop = true;
            this.m_panelEdition.AutoScroll = true;
            this.m_panelEdition.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_panelEdition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEdition.Echelle = 1F;
            this.m_panelEdition.EffetAjoutSuppression = false;
            this.m_panelEdition.EffetFonduMenu = true;
            this.m_panelEdition.EnDeplacement = false;
            this.m_panelEdition.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_panelEdition.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_panelEdition.HauteurMinimaleDesObjets = 10;
            this.m_panelEdition.HistorisationActive = true;
            this.m_panelEdition.InfoActionACree = null;
            this.m_panelEdition.LargeurMinimaleDesObjets = 10;
            this.m_panelEdition.Location = new System.Drawing.Point(0, 0);
            this.m_panelEdition.LockEdition = false;
            this.m_panelEdition.Marge = 10;
            this.m_panelEdition.MaxZoom = 6F;
            this.m_panelEdition.MinZoom = 0.2F;
            this.m_panelEdition.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_panelEdition.ModeEdition = sc2i.win32.process.EModeEditeurProcess.Selection;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEdition, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelEdition.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_panelEdition.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_panelEdition.Name = "m_panelEdition";
            this.m_panelEdition.NoClipboard = false;
            this.m_panelEdition.NoDelete = false;
            this.m_panelEdition.NoDoubleClick = false;
            this.m_panelEdition.NombreHistorisation = 10;
            this.m_panelEdition.NoMenu = false;
            this.m_panelEdition.ObjetEdite = null;
            this.m_panelEdition.ProcessEdite = null;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_panelEdition.Profil = cProfilEditeurObjetGraphique1;
            this.m_panelEdition.RefreshSelectionChanged = true;
            this.m_panelEdition.SelectionVisible = true;
            this.m_panelEdition.Size = new System.Drawing.Size(387, 380);
            this.m_panelEdition.TabIndex = 4;
            this.m_panelEdition.ToujoursAlignerSelonLesControles = true;
            this.m_panelEdition.ToujoursAlignerSurLaGrille = false;
            this.m_panelEdition.AfterAddElementToProcess += new System.EventHandler(this.m_panelEdition_AfterAddElementToProcess);
            this.m_panelEdition.AfterModeEditionChanged += new System.EventHandler(this.m_panelEdition_AfterModeEditionChanged);
            this.m_panelEdition.DoubleClicSurElement += new System.EventHandler(this.m_panelEdition_DoubleClicSurElement);
            this.m_panelEdition.AfterRemoveObjetGraphique += new System.EventHandler(this.m_panelEdition_AfterRemoveObjetGraphique);
            // 
            // CProcessEditor
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_panelFondDroite);
            this.Controls.Add(this.m_panelBoutons);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.Name = "CProcessEditor";
            this.Size = new System.Drawing.Size(640, 430);
            this.Load += new System.EventHandler(this.CProcessEditor_Load);
            this.m_panelBoutons.ResumeLayout(false);
            this.m_panelCadreEdition.ResumeLayout(false);
            this.m_panelInfo.ResumeLayout(false);
            this.m_panelBasDroit.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_panelHautDroit.ResumeLayout(false);
            this.m_panelHautDroit.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.m_panelFondDroite.ResumeLayout(false);
            this.m_panelEntete.ResumeLayout(false);
            this.m_panelEntete.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public CPanelEditionObjetGraphique Editeur
		{
			get
			{
				return m_panelEdition;
			}
		}

		private bool m_bDisableTypeElement = false;
		private CAction m_actionEnEdition = null;
        private bool m_bForEvent = false;

		public CProcessEditor()
		{
			InitializeComponent();
		}
		/// //////////////////////////////////////////
		public CProcess Process
		{
			get
			{
				return m_panelEdition.ProcessEdite;
			}
			set
			{
				m_panelEdition.ProcessEdite = value;
				FillListeActions();
				FillListeVariables();
				InitComboBoxType();
				
				if ( value != null )
				{
					m_chkAssynchrone.Checked = value.ModeAsynchrone;
                    m_chkModeTransactionnel.Checked = value.ModeTransactionnel;
					m_cmbTypeElements.SelectedValue = value.TypeCible==null?typeof(DBNull):value.TypeCible;
					m_chkTableau.Checked = value.SurTableauDeTypeCible;
				}
				else
				{
					m_cmbTypeElements.SelectedValue = typeof(DBNull);
					m_chkTableau.Checked = false;
				}
			}
		}

        /// //////////////////////////////////////////
        public bool ForEvent
        {
            get
            {
                return m_bForEvent;
            }
            set
            {
                m_bForEvent = value;
            }
        }
            


		/// //////////////////////////////////////////
		private void UpdateBoutonsMode()
		{
			switch ( m_panelEdition.ModeEdition )
			{
				case EModeEditeurProcess.Selection :
					m_chkCurseur.Checked = true;
					break;
                case EModeEditeurProcess.Zoom:
                    m_chkZoom.Checked = true;
                    break;
				case EModeEditeurProcess.Condition :
					m_chkCondition.Checked = true;
					break;
				case EModeEditeurProcess.Lien1 :
				case EModeEditeurProcess.Lien2 :
					m_chkLien.Checked = true;
					break;
                case EModeEditeurProcess.Action :
                    m_btnAction.Checked = true;
                    break;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private void CProcessEditor_Load(object sender, System.EventArgs e)
		{
			m_panelEdition.ModeEdition = EModeEditeurProcess.Selection;
			m_panelEdition.Selection.SelectionChanged += new EventHandler(OnSelectionChanged);
			UpdateBoutonsMode();
		}

		/// ////////////////////////////////////////////////////////////////
		private void OnSelectionChanged ( object sender, EventArgs args )
		{
			I2iObjetGraphique obj = m_panelEdition.Selection.ControlReference;
			if ( obj != null && obj is IObjetDeProcess)
				ShowInfos ( (IObjetDeProcess)obj );
			else
				ShowInfos(null);
		}

		/// ////////////////////////////////////////////////////////////////
		public void ShowInfos ( IObjetDeProcess objet )
		{
			if ( objet is CAction )
			{
				CAction action = (CAction)objet;
				m_actionEnEdition = action;
				m_txtLibelle.Text = action.Libelle;
				m_txtLibelle.Enabled = true;
				FillListeLiens ( action );
			}
			else if ( objet is CLienAction )
			{
				m_actionEnEdition = null;
				m_txtLibelle.Enabled = false;
				m_txtLibelle.Text = "Link|217";
				FillListeLiens ( (CLienAction)objet );
			}
			else
			{
				m_actionEnEdition = null;
				m_txtLibelle.Text = "";
				m_txtLibelle.Enabled = false;
				m_wndListeLiens.Items.Clear();
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public void FillListeLiens ( CAction action )
		{
			m_wndListeLiens.Items.Clear();
			ListViewItem item;
			foreach ( CLienAction lien in Process.GetLiensForAction ( action, true ) )
			{
				item = new ListViewItem ( "De" );
				item.SubItems.Add ( lien.ActionDepart.Libelle );
				item.Tag = lien;
				m_wndListeLiens.Items.Add ( item );
			}
			foreach ( CLienAction lien in Process.GetLiensForAction ( action, false ) )
			{
				item = new ListViewItem ( "Vers" );
				item.SubItems.Add ( lien.ActionArrivee.Libelle );
				item.Tag = lien;
				m_wndListeLiens.Items.Add ( item );
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public void FillListeLiens ( CLienAction lien )
		{
			m_wndListeLiens.Items.Clear();
			ListViewItem item;
			item = new ListViewItem ( "De" );
			item.Tag = lien;
			item.SubItems.Add ( lien.ActionDepart.Libelle );
			m_wndListeLiens.Items.Add ( item );
			item = new ListViewItem ( "Vers" );
			item.Tag = lien;
			item.SubItems.Add ( lien.ActionArrivee.Libelle );
			m_wndListeLiens.Items.Add ( item );
		}


		/// ////////////////////////////////////////////////////////////////
		private void m_chkCurseur_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( m_chkCurseur.Checked )
				m_panelEdition.ModeEdition = EModeEditeurProcess.Selection;
		}

		/// ////////////////////////////////////////////////////////////////
		public class CMenuTypesAction : MenuItem
		{
			private CInfoAction m_info;
			public CMenuTypesAction ( CInfoAction info )
				:base ( info.Nom )
			{
				m_info = info;
			}

			public CInfoAction InfoAction
			{
				get
				{
					return m_info;
				}
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private void m_btnAction_Click(object sender, System.EventArgs e)
		{
			//Crée le menu
			m_menuAjoutAction.MenuItems.Clear();
			Hashtable tableMenuParCategorie = new Hashtable();
			foreach ( CInfoAction info in CGestionnaireActionsDisponibles.TypesDisponibles )
			{
				MenuItem menuParent = (MenuItem)tableMenuParCategorie[info.Categorie];
				if ( menuParent == null )
				{
					menuParent = new MenuItem(info.Categorie);
					m_menuAjoutAction.MenuItems.Add ( menuParent );
					tableMenuParCategorie[info.Categorie] = menuParent;
				}
				CMenuTypesAction menuFils = new CMenuTypesAction ( info );
				menuFils.Click += new EventHandler ( OnSelectTypeActionGenerique );
				menuParent.MenuItems.Add ( menuFils );
			}
			
			CTrieurDeMenu.Trier(m_menuAjoutAction);
			m_menuAjoutAction.Show ( m_btnAction, new Point ( m_btnAction.Width, 0 ) );
		}
		/// ////////////////////////////////////////////////////////////////
		private void OnSelectTypeActionGenerique ( object sender, EventArgs e )
		{
			if ( sender is CMenuTypesAction )
			{
				CInfoAction info = ((CMenuTypesAction)sender).InfoAction;
				m_panelEdition.ModeEdition = EModeEditeurProcess.Action;
				m_panelEdition.InfoActionACree = info;
                // Je check l'outil Attente pour rendre l'outil Selection "checkable"
                // L'outil Attent n'est plus utilisé, son bouton n'est donc pas visible. Toutefois il ne faut pas
                // le retirer du Form car il sert justement ici. YK 19/05/2008
                m_btnAction.Checked = true;

			}
		}

		/// ////////////////////////////////////////////////////////////////
		private void m_chkCondition_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( m_chkCondition.Checked )
				m_panelEdition.ModeEdition = EModeEditeurProcess.Condition;
        }

		/// ////////////////////////////////////////////////////////////////
		private void m_chkLien_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( m_chkLien.Checked )
				m_panelEdition.ModeEdition = EModeEditeurProcess.Lien1;
		}

		/// ////////////////////////////////////////////////////////////////
		private void m_panelEdition_AfterModeEditionChanged(object sender, System.EventArgs e)
		{
			UpdateBoutonsMode();
		}

		/// ////////////////////////////////////////////////////////////////
		private void m_txtLibelle_TextChanged(object sender, System.EventArgs e)
		{
			if ( m_actionEnEdition != null )
			{
				m_actionEnEdition.Libelle = m_txtLibelle.Text;
				m_panelEdition.RefreshDelayed();
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private void FillListeActions()
		{
			if ( Process != null )
			{
				m_comboActions.BeginUpdate();
				m_comboActions.Enabled = false;
				m_comboActions.ListDonnees = Process.Actions;
				m_comboActions.ProprieteAffichee = "Libelle";
				m_comboActions.Enabled = true;
				m_comboActions.EndUpdate();
			}
		}

		private void m_panelEdition_AfterAddElementToProcess(object sender, System.EventArgs e)
		{
			FillListeActions();
			FillListeVariables();
		}

		private void m_panelEdition_AfterRemoveObjetGraphique(object sender, System.EventArgs e)
		{
			FillListeActions();
		}

		private void m_comboActions_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if ( m_comboActions.Enabled && m_comboActions.SelectedValue is C2iObjetGraphique)
			{
				m_panelEdition.Selection.Clear();
				m_panelEdition.Selection.Add ( (C2iObjetGraphique)m_comboActions.SelectedValue );
				m_panelEdition.Refresh();
			}
		}

		private void m_wndListeLiens_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Delete )
			{
				if ( m_wndListeLiens.SelectedItems.Count == 0 )
					return;
				if ( CFormAlerte.Afficher(I.T("Remove these links ?|30035"), EFormAlerteType.Question) ==DialogResult.Yes )
				{
					ArrayList lst = new ArrayList();
					foreach ( ListViewItem item in m_wndListeLiens.SelectedItems )
						lst.Add ( item );
					foreach ( ListViewItem item in lst )
					{
						CLienAction lien = (CLienAction)item.Tag;
						Process.RemoveLien ( lien );
						m_wndListeLiens.Items.Remove ( item );
					}
					m_panelEdition.Refresh();
				}
			}
		}

		private void m_wndListeLiens_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void m_wndListeLiens_DoubleClick(object sender, System.EventArgs e)
		{
			if ( m_wndListeLiens.SelectedItems.Count != 1 )
				return;
			ListViewItem item = m_wndListeLiens.SelectedItems[0];
			CLienAction lien = (CLienAction)item.Tag;
			if ( CEditeurActionsEtLiens.EditeObjet ( lien ) )
			{
				m_panelEdition.Refresh();
			}
		}

		/// /////////////////////////////////////////////////////////////////////
		public bool EditeVariable ( CVariableDynamique variable )
		{
			if ( variable == null )
				return false;
			if ( variable is CVariableDynamiqueSaisie )
				return CFormEditVariableDynamiqueSaisie.EditeVariable ( (CVariableDynamiqueSaisie)variable, Process );
			if ( variable is CVariableDynamiqueCalculee )
				return CFormEditVariableFiltreCalculee.EditeVariable		 ((CVariableDynamiqueCalculee)variable, Process );
			if ( variable is CVariableDynamiqueSelectionObjetDonnee )
				return CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable ( (CVariableDynamiqueSelectionObjetDonnee)variable );
            if (variable is CVariableProcessTypeComplexe)
            {
                CVariableProcessTypeComplexe varComp = (CVariableProcessTypeComplexe)variable;
                string strNom = variable.Nom;
                CTypeResultatExpression type = variable.TypeDonnee;
                CDbKey dbKey = varComp.DbKeyElementInitial;
                if (CFormEditNomVariable.EditeNomVariable(ref strNom, ref type, ref dbKey, true))
                {
                    variable.Nom = strNom;
                    ((CVariableProcessTypeComplexe)variable).SetTypeDonnee(type);
                    varComp.SetTypeDonnee(type);
                    varComp.DbKeyElementInitial = dbKey;
                    return true;
                }
                return false;
            }
			return true;
		}

		/// /////////////////////////////////////////////////////////////////////
		private void FillListeVariables ()
		{
			if ( Process != null )
			{
				m_wndListeVariables.Remplir ( Process.ListeVariables );
                CVariableDynamique varRetour = Process.VariableDeRetour;
                foreach ( ListViewItem item in m_wndListeVariables.Items )
                {
                    IVariableDynamique var = item.Tag as IVariableDynamique;
                    if ( var != null )
                    {
                        if ( Process.IsVariableDeProcessParent ( var ) )
                            item.BackColor = Color.LightCyan;
                        if ( var.Equals ( varRetour ) )
                            item.BackColor = Color.LightGreen;
                    }
                }
            }
		}

		/// /////////////////////////////////////////////////////////////////////
		private void m_btnAjouter_LinkClicked(object sender, System.EventArgs e)
		{
			m_menuAjoutVariable.Show ( m_btnAjouter, new Point ( 0, m_btnAjouter.Height ) );
		}

		/// /////////////////////////////////////////////////////////////////////
		private void m_menuVariableSimple_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie(Process );
			if ( EditeVariable ( variable ) )
			{
                Process.AddVariable(variable);
				FillListeVariables();
			}
		}

		private void m_btnProprietes_Click(object sender, System.EventArgs e)
		{
			AfficherEditeurPropriete();
		}

		private void AfficherEditeurPropriete()
		{
			if (!LockEdition && m_panelEdition.Selection.ControlReference != null && m_panelEdition.Selection.ControlReference is IObjetDeProcess)
			{
				if (CEditeurActionsEtLiens.EditeObjet((IObjetDeProcess)m_panelEdition.Selection.ControlReference))
				{
					FillListeVariables();
					FillListeActions();
				}
			}
		}

		private CVariableDynamique VariableSelectionnee
		{
			get
			{
				if ( m_wndListeVariables.SelectedItems.Count == 0 )
					return null;
				return (CVariableDynamique)m_wndListeVariables.SelectedItems[0].Tag;
			
			}
		}

		/// /////////////////////////////////////////////////////////////////////
		private void m_btnDetail_LinkClicked(object sender, System.EventArgs e)
		{
			CVariableDynamique variable  = VariableSelectionnee;
			if ( EditeVariable ( variable ) )
			{
				FillListeVariables();
			}
		}


		/// /////////////////////////////////////////////////////////////////////
		private bool m_bComboTypeInitialized = false;
		private void InitComboBoxType( )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_bComboTypeInitialized )
				return;

			CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass(typeof(sc2i.data.TableAttribute));
			ArrayList lst = new ArrayList();
			lst.Add ( new CInfoClasseDynamique ( typeof(DBNull), I.T("<<General process>>|30037") ) );
            if ( !m_bForEvent )
                lst.Add ( new CInfoClasseDynamique ( typeof(CObjetDonneeAIdNumerique), I.T("All elements|20037")));
			foreach ( CInfoClasseDynamique info in classes )
				lst.Add ( info );
			m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";
			m_cmbTypeElements.DataSource = lst;
			

			m_bComboTypeInitialized = true;
		}

		/// /////////////////////////////////////////////////////////////////////
		private void m_cmbTypeElements_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if ( m_bComboTypeInitialized && Enabled && Process != null )
			{
				if ( m_cmbTypeElements.SelectedValue is Type )
				{
					Type tp = (Type)m_cmbTypeElements.SelectedValue;
					if ( tp == typeof(DBNull) )
						Process.TypeCible = null;
					else
						Process.TypeCible = (Type)m_cmbTypeElements.SelectedValue;
					FillListeVariables();
				}
			}

		}

		private void m_menuVariableCalculee_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(Process );
			if ( EditeVariable ( variable ) )
			{
                Process.AddVariable(variable);
				FillListeVariables();
			}
		}

		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee ( Process );
			if ( EditeVariable ( variable ) )
			{
                Process.AddVariable(variable);
				FillListeVariables();
			}
		}

		private void m_btnJonction_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( m_btnJonction.Checked )
				m_panelEdition.ModeEdition = EModeEditeurProcess.Jonction;
		}

		private void m_menuVariableEntite_Click(object sender, System.EventArgs e)
		{
			CVariableProcessTypeComplexe variable = new CVariableProcessTypeComplexe ( Process );
			if ( EditeVariable ( variable ) )
			{
                Process.AddVariable(variable);
				FillListeVariables();
			}
		}

		private void m_lnkDetail_LinkClicked(object sender, System.EventArgs e)
		{
			CVariableDynamique variable = VariableSelectionnee;
			if ( variable != null )
			{
				if ( EditeVariable ( variable ) )
					FillListeVariables();
			}
		}


		/// /////////////////////////////////////////////////////
		private void m_wndListeVariables_DoubleClick(object sender, System.EventArgs e)
		{
			CVariableDynamique variable = VariableSelectionnee;
			if ( variable != null )
			{
				if ( EditeVariable ( variable ) )
					FillListeVariables();
			}
		}

		/// /////////////////////////////////////////////////////
		private void m_lnkSupprimer_LinkClicked(object sender, System.EventArgs e)
		{
			CVariableDynamique variable = VariableSelectionnee;
			if  ( variable == null )
				return;
			Hashtable table = new Hashtable();
			foreach( CAction action in Process.Actions )
				action.AddIdVariablesNecessairesInHashtable ( table );
            if (table[variable.IdVariable] != null)
            {
                CFormAlerte.Afficher(I.T("This variable is in use and cannot be removed|30036"), EFormAlerteType.Erreur);
                return;
            }
			Process.RemoveVariable ( variable );
			FillListeVariables();
		}

		private void m_btnOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = I.T("Process (*.2iProcess)|*.2iProcess|All files (*.*)|*.*|30053");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				if ( CFormAlerte.Afficher(I.T("The data of the current process will be replaced. Continue ?|30038"),
					EFormAlerteType.Question) == DialogResult.No )
					return;
				CProcess newProcess = new CProcess (  Process.ContexteDonnee );
				CResultAErreur result = CSerializerObjetInFile.ReadFromFile ( newProcess, CProcess.c_idFichier, dlg.FileName );
				if ( !result )
					CFormAlerte.Afficher(result);
				else
				{
					Process = newProcess;
				}
			}
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog ();
            dlg.Filter = I.T("Process (*.2iProcess)|*.2iProcess|All files (*.*)|*.*|30053");
		    if ( dlg.ShowDialog()==DialogResult.OK )
			{
				string strNomFichier = dlg.FileName;
				CResultAErreur result = CSerializerObjetInFile.SaveToFile ( Process, CProcess.c_idFichier,  strNomFichier );
				if ( !result )
					CFormAlerte.Afficher( result);
				else
					CFormAlerte.Afficher (I.T("Save successful|30039"));
			}
		}

		private void m_chkAssynchrone_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( Process != null )
				Process.ModeAsynchrone = m_chkAssynchrone.Checked;
		}

		public bool DisableTypeElement
		{
			get
			{
				return m_bDisableTypeElement;
			}
			set
			{
				m_bDisableTypeElement = value;
				if ( value )
				{
					m_cmbTypeElements.LockEdition = true;
					m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.Autonome );
					m_chkTableau.Enabled = false;
					m_gestionnaireModeEdition.SetModeEdition ( m_chkTableau, TypeModeEdition.Autonome );
				}
				else
				{
					m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.EnableSurEdition );
					m_cmbTypeElements.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
					m_gestionnaireModeEdition.SetModeEdition ( m_chkTableau, TypeModeEdition.EnableSurEdition );
					m_chkTableau.Enabled = m_gestionnaireModeEdition.ModeEdition;
					
				}
			}
		}

		public void SetTypeElement ( Type tp )
		{
			if ( tp != null )
			{
				m_cmbTypeElements.SelectedValue = tp;
				if ( Process != null )
					if ( tp != Process.TypeCible )
						Process.TypeCible = tp;
			}
			else
			{
				m_cmbTypeElements.SelectedIndex = 0;
				if ( Process != null && Process.TypeCible != null )
					Process.TypeCible = null;
			}
		}
		public void SetSurTableauDeTypeCible ( bool bVal )
		{
			m_chkTableau.Checked = bVal;
            if (Process != null)
            {
                Process.SurTableauDeTypeCible = bVal;
                FillListeVariables();
            }
		}

		private void m_btnCopy_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CSerializerObjetInClipBoard.Copy ( Process, CProcess.c_idFichier );
			if ( !result) 
			{
				CFormAlerte.Afficher( result);
			}
		}
		private void m_btnPaste_Click(object sender, System.EventArgs e)
		{
			I2iSerializable objet = null;
			CResultAErreur result = CSerializerObjetInClipBoard.Paste ( ref objet, CProcess.c_idFichier );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}
			if ( CFormAlerte.Afficher(I.T("Action data will be replaced, continue|30040"),
				EFormAlerteType.Question) == DialogResult.No )
				return;
			CProcess newProcess = new CProcess (  Process.ContexteDonnee );
			Process = (CProcess)objet;
		}

		private void m_wndListeVariables_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right && m_gestionnaireModeEdition.ModeEdition )
			{
				ListViewItem item = m_wndListeVariables.GetItemAt ( e.X, e.Y );
				if ( item != null && item.Tag is CVariableDynamique)
				{
					CVariableDynamique variable = (CVariableDynamique)item.Tag;
					if ( Process.VariableDeRetour != null && 
						variable.IdVariable == Process.VariableDeRetour.IdVariable )
						m_menuVariableDeRetour.Checked = true;
					else
						m_menuVariableDeRetour.Checked =  false;
					item.Selected = true;
					m_menuVariable.Show ( m_wndListeVariables, new Point ( e.X, e.Y ) );
				}
			}
		}

		private void m_menuVariableDeRetour_Click(object sender, System.EventArgs e)
		{
			if( m_wndListeVariables.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeVariables.SelectedItems[0];
				if ( item.Tag is CVariableDynamique )
				{
					Process.VariableDeRetour = (CVariableDynamique)item.Tag;
					FillListeVariables();
				}
			}
		}

		private void m_panelEdition_DoubleClicSurElement(object sender, EventArgs e)
		{
			AfficherEditeurPropriete();
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
				if ( value == m_gestionnaireModeEdition.ModeEdition )
				{
					m_gestionnaireModeEdition.ModeEdition = !value;
					if ( OnChangeLockEdition !=  null )
						OnChangeLockEdition(this, new EventArgs() );
                    if (value)
                    {
                        m_panelEdition.ModeEdition = EModeEditeurProcess.Selection;
                        m_chkCurseur.Checked = true;
                    }
				}
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

        private void m_chkZoom_CheckedChanged(object sender, EventArgs e)
        {
            if ( m_chkZoom.Checked )
				m_panelEdition.ModeEdition = EModeEditeurProcess.Zoom;
        }

        private void m_rbntEntree_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rbntEntree.Checked)
                m_panelEdition.ModeEdition = EModeEditeurProcess.EntryPoint;
        }

        private void m_chkTableau_CheckedChanged(object sender, EventArgs e)
        {
            if ( m_gestionnaireModeEdition.ModeEdition )
                SetSurTableauDeTypeCible(m_chkTableau.Checked);
        }

        private void m_chkModeTransactionnel_CheckedChanged(object sender, EventArgs e)
        {
            if (Process != null)
                Process.ModeTransactionnel = m_chkModeTransactionnel.Checked;
        }
	}


	public static class CTrieurDeMenu
	{
		public static void Trier(MenuStrip mnu)
		{
		}
		public static void Trier(Menu mnu)
		{
			List<MenuItem> items = new List<MenuItem>();
			for (int n = mnu.MenuItems.Count; n > 0; n--)
			{
				MenuItem itm = mnu.MenuItems[n - 1];
				if (itm.MenuItems.Count > 0)
					Trier(itm);
				items.Add(itm);
			}

			items.Sort(new CCompareurMenu());
			mnu.MenuItems.Clear();
			foreach(MenuItem itm in items)
				mnu.MenuItems.Add(itm);
		}

		internal class CCompareurMenu : IComparer<MenuItem>
		{

			public int Compare(MenuItem x, MenuItem y)
			{
				return x.Text.CompareTo(y.Text);
			}
		}
	}
}
