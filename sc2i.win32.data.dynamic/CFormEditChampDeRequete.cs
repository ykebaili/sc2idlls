using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;
using System.Collections.Generic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableFiltreCalculee.
	/// </summary>
	public class CFormEditChampDeRequete : System.Windows.Forms.Form
	{
		private Type m_typeSource = null;
		private CDefinitionProprieteDynamique m_defRacineDeChamps = null;
		private C2iChampDeRequete m_champ = null;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.TextBox m_txtNomChamp;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox m_chkGrouper;
		private sc2i.win32.common.C2iComboBox m_cmbOperation;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox m_txtFonctionSql;
		private System.Windows.Forms.Panel m_panelSources;
		private System.Windows.Forms.LinkLabel m_lnkAjouterSource;
        private CExtStyle m_ExtStyle1;
		private Label label6;
		private C2iComboBox m_cmbTypeFinal;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditChampDeRequete()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_cmbTypeFinal = new sc2i.win32.common.C2iComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_panelSources = new System.Windows.Forms.Panel();
            this.m_txtFonctionSql = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtNomChamp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lnkAjouterSource = new System.Windows.Forms.LinkLabel();
            this.m_cmbOperation = new sc2i.win32.common.C2iComboBox();
            this.m_chkGrouper = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbTypeFinal);
            this.c2iPanelOmbre1.Controls.Add(this.label6);
            this.c2iPanelOmbre1.Controls.Add(this.m_panelSources);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtFonctionSql);
            this.c2iPanelOmbre1.Controls.Add(this.label3);
            this.c2iPanelOmbre1.Controls.Add(this.label5);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomChamp);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.label4);
            this.c2iPanelOmbre1.Controls.Add(this.m_lnkAjouterSource);
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbOperation);
            this.c2iPanelOmbre1.Controls.Add(this.m_chkGrouper);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(504, 322);
            this.m_ExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 18;
            // 
            // m_cmbTypeFinal
            // 
            this.m_cmbTypeFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeFinal.IsLink = false;
            this.m_cmbTypeFinal.Location = new System.Drawing.Point(128, 281);
            this.m_cmbTypeFinal.LockEdition = false;
            this.m_cmbTypeFinal.Name = "m_cmbTypeFinal";
            this.m_cmbTypeFinal.Size = new System.Drawing.Size(208, 21);
            this.m_ExtStyle1.SetStyleBackColor(this.m_cmbTypeFinal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_cmbTypeFinal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeFinal.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 11;
            this.label6.Text = "Final data type|20006";
            // 
            // m_panelSources
            // 
            this.m_panelSources.AutoScroll = true;
            this.m_panelSources.BackColor = System.Drawing.Color.White;
            this.m_panelSources.Location = new System.Drawing.Point(128, 54);
            this.m_panelSources.Name = "m_panelSources";
            this.m_panelSources.Size = new System.Drawing.Size(349, 114);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelSources.TabIndex = 1;
            // 
            // m_txtFonctionSql
            // 
            this.m_txtFonctionSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFonctionSql.Location = new System.Drawing.Point(128, 176);
            this.m_txtFonctionSql.Multiline = true;
            this.m_txtFonctionSql.Name = "m_txtFonctionSql";
            this.m_txtFonctionSql.Size = new System.Drawing.Size(349, 48);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtFonctionSql, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtFonctionSql, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFonctionSql.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 8;
            this.label3.Text = "SQL function|105";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 5;
            this.label5.Text = "Operation|106";
            // 
            // m_txtNomChamp
            // 
            this.m_txtNomChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomChamp.Location = new System.Drawing.Point(128, 28);
            this.m_txtNomChamp.Name = "m_txtNomChamp";
            this.m_txtNomChamp.Size = new System.Drawing.Size(349, 20);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomChamp.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Field name|101";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cumulated field|103";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sources|104";
            // 
            // m_lnkAjouterSource
            // 
            this.m_lnkAjouterSource.Location = new System.Drawing.Point(8, 70);
            this.m_lnkAjouterSource.Name = "m_lnkAjouterSource";
            this.m_lnkAjouterSource.Size = new System.Drawing.Size(64, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lnkAjouterSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lnkAjouterSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAjouterSource.TabIndex = 0;
            this.m_lnkAjouterSource.TabStop = true;
            this.m_lnkAjouterSource.Text = "Add|24";
            this.m_lnkAjouterSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAjouterSource_LinkClicked);
            // 
            // m_cmbOperation
            // 
            this.m_cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOperation.IsLink = false;
            this.m_cmbOperation.Location = new System.Drawing.Point(128, 232);
            this.m_cmbOperation.LockEdition = false;
            this.m_cmbOperation.Name = "m_cmbOperation";
            this.m_cmbOperation.Size = new System.Drawing.Size(208, 21);
            this.m_ExtStyle1.SetStyleBackColor(this.m_cmbOperation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_cmbOperation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbOperation.TabIndex = 3;
            // 
            // m_chkGrouper
            // 
            this.m_chkGrouper.Location = new System.Drawing.Point(128, 259);
            this.m_chkGrouper.Name = "m_chkGrouper";
            this.m_chkGrouper.Size = new System.Drawing.Size(240, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_chkGrouper, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_chkGrouper, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkGrouper.TabIndex = 4;
            this.m_chkGrouper.Text = "Group the data on this field|106";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.c2iPanelOmbre1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 407);
            this.m_ExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 20;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(256, 378);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(136, 378);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(517, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 407);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // CFormEditChampDeRequete
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(520, 407);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditChampDeRequete";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Cumulated field|103";
            this.Load += new System.EventHandler(this.CFormEditVariableFiltreCalculee_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void c2iPanelOmbre2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void CFormEditVariableFiltreCalculee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

            m_cmbOperation.ValueMember = "Valeur";
			m_cmbOperation.DisplayMember = "Libelle";
			m_cmbOperation.DataSource = CUtilSurEnum.GetCouplesFromEnum(typeof(OperationsAgregation) );
			m_cmbOperation.SelectedValue = (int)m_champ.OperationAgregation;

			//Types finaux
			m_cmbTypeFinal.Items.Clear();
			List<CInfoClasseDynamique> lstTypes = new List<CInfoClasseDynamique>();
			lstTypes.Add(new CInfoClasseDynamique(typeof(DBNull), I.T("Default|20007")));
			lstTypes.Add(new CInfoClasseDynamique(typeof(int), C2iTypeDonnee.GetLibelleType(TypeDonnee.tEntier)));
			lstTypes.Add(new CInfoClasseDynamique(typeof(string), C2iTypeDonnee.GetLibelleType(TypeDonnee.tString)));
			lstTypes.Add(new CInfoClasseDynamique(typeof(bool), C2iTypeDonnee.GetLibelleType(TypeDonnee.tBool)));
			lstTypes.Add(new CInfoClasseDynamique(typeof(DateTime), C2iTypeDonnee.GetLibelleType(TypeDonnee.tDate)));
			lstTypes.Add(new CInfoClasseDynamique(typeof(double), C2iTypeDonnee.GetLibelleType(TypeDonnee.tDouble)));
			m_cmbTypeFinal.DataSource = lstTypes;
			m_cmbTypeFinal.ValueMember = "Classe";
			m_cmbTypeFinal.DisplayMember = "Nom";

			Type tp = m_champ.TypeDonneeFinalForce;
			if (tp == null)
				tp = typeof(DBNull);
			try
			{
				m_cmbTypeFinal.SelectedValue = tp;
			}
			catch
			{ }

			m_chkGrouper.Checked = m_champ.GroupBy;
			//m_lblSource.Text = m_champ.Source;
			m_txtNomChamp.Text = m_champ.NomChamp;
			m_txtFonctionSql.Text = m_champ.FonctionSql;

			int nIndex = 0;
			foreach ( CSourceDeChampDeRequete source in m_champ.Sources )
			{
				CPanelSourceDeRequete panel = new CPanelSourceDeRequete();
				panel.Dock = DockStyle.Top;
				m_panelSources.Controls.Add ( panel );
				panel.BringToFront();
				panel.Init ( source,nIndex++, m_typeSource, m_defRacineDeChamps );
			}
		}

		/// //////////////////////////////////////////////////////
		private void Init ( C2iChampDeRequete champ, Type typeSource, CDefinitionProprieteDynamique defRacineDeChamps )
		{
			m_champ = champ;
			m_defRacineDeChamps = defRacineDeChamps;
			m_typeSource = typeSource;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeChamp ( C2iChampDeRequete champ, Type typeSource, CDefinitionProprieteDynamique defRacineDeChamps )
		{
			CFormEditChampDeRequete  form = new CFormEditChampDeRequete();
			form.Init ( champ, typeSource, defRacineDeChamps );
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		/// //////////////////////////////////////////////////////
		private class CPanelSourceComparer : IComparer
		{
			#region Membres de IComparer

			public int Compare(object x, object y)
			{
				if ( (x is CPanelSourceDeRequete) && (y is CPanelSourceDeRequete) )
					return ((CPanelSourceDeRequete)x).Index.CompareTo ( ((CPanelSourceDeRequete)y).Index );
				return 0;
			}

			#endregion

		}


		

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_cmbOperation.SelectedValue == null )
			{
				CFormAlerte.Afficher(I.T("Select an operation|30009"), EFormAlerteType.Exclamation);
				return;
			}
			m_champ.GroupBy = m_chkGrouper.Checked;
			m_champ.OperationAgregation = (OperationsAgregation)m_cmbOperation.SelectedValue;
			if ( m_txtNomChamp.Text.Trim() == "" )
			{
				CFormAlerte.Afficher(I.T("Enter a field name|30008"), EFormAlerteType.Exclamation);
				return;
			}
			ArrayList lst = new ArrayList(m_panelSources.Controls);
			lst.Sort ( new CPanelSourceComparer() );
			ArrayList lstSources = new ArrayList();
			foreach ( Control ctrl in lst )
			{
				if ( ctrl is CPanelSourceDeRequete )
				{
					CPanelSourceDeRequete panel = (CPanelSourceDeRequete)ctrl;
					if ( panel.Source != null )
						lstSources.Add ( panel.Source );
				}
			}
			m_champ.Sources = (CSourceDeChampDeRequete[])lstSources.ToArray( typeof(CSourceDeChampDeRequete) );
			m_champ.FonctionSql = m_txtFonctionSql.Text.Trim();
			m_champ.NomChamp = m_txtNomChamp.Text.Replace(" ","_").Trim();
			Type tp = m_cmbTypeFinal.SelectedValue as Type;
			if (tp == typeof(DBNull))
				tp = null;
			m_champ.TypeDonneeFinalForce = tp;
			DialogResult = DialogResult.OK;
			Close();

		}

		private void m_lnkAjouterSource_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CSourceDeChampDeRequete source = new CSourceDeChampDeRequete("");
			CPanelSourceDeRequete panel = new CPanelSourceDeRequete();
			panel.Dock = DockStyle.Top;
			m_panelSources.Controls.Add ( panel );
			panel.BringToFront();

            // Initialise le nouveau control avec l'index incrémenté de 1
            int nIndexControls = m_panelSources.Controls.Count;
            panel.Init(
                source,
                nIndexControls > 0 ? nIndexControls - 1  : 0,
                m_typeSource,
                m_defRacineDeChamps);
			panel.Focus();
		}
	}
}
