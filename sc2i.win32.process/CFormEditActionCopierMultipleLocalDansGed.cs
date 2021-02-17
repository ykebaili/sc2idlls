using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using sc2i.data;
using sc2i.win32.process;
using sc2i.win32.data;

using sc2i.documents;
using sc2i.win32.common;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditCopierMultipleLocalDansGed : sc2i.win32.process.CFormEditActionFonction
    {
		private System.Windows.Forms.Label label4;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
        private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn2;
        private sc2i.win32.common.CExtStyle m_exStyle;
        private Label label3;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleLibelle;
        private Label label1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleCategories;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleCle;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleAssociation;
        private Label label2;
        private C2iPanelOmbre c2iPanel1;
		private System.ComponentModel.IContainer components = null;

		public CFormEditCopierMultipleLocalDansGed()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
            CWin32Traducteur.Translate(this);

            
			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
		}

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

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_txtFormuleAssociation = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFormuleCle = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_txtFormuleLibelle = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtFormuleCategories = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewAutoFilledColumn2 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.c2iPanel1 = new sc2i.win32.common.C2iPanelOmbre();
            this.c2iPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_exStyle.SetStyleBackColor(this.m_lblStockerResIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblStockerResIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblStockerResIn.Text = "Store the result in|30239";
            // 
            // m_txtFormuleAssociation
            // 
            this.m_txtFormuleAssociation.AllowGraphic = true;
            this.m_txtFormuleAssociation.AllowNullFormula = false;
            this.m_txtFormuleAssociation.AllowSaisieTexte = true;
            this.m_txtFormuleAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleAssociation.Formule = null;
            this.m_txtFormuleAssociation.Location = new System.Drawing.Point(11, 112);
            this.m_txtFormuleAssociation.LockEdition = false;
            this.m_txtFormuleAssociation.LockZoneTexte = false;
            this.m_txtFormuleAssociation.Name = "m_txtFormuleAssociation";
            this.m_txtFormuleAssociation.Size = new System.Drawing.Size(621, 62);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleAssociation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleAssociation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleAssociation.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 16);
            this.m_exStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 5;
            this.label2.Text = "Associated document to|20141";
            // 
            // m_txtFormuleCle
            // 
            this.m_txtFormuleCle.AllowGraphic = true;
            this.m_txtFormuleCle.AllowNullFormula = false;
            this.m_txtFormuleCle.AllowSaisieTexte = true;
            this.m_txtFormuleCle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleCle.Formule = null;
            this.m_txtFormuleCle.Location = new System.Drawing.Point(11, 280);
            this.m_txtFormuleCle.LockEdition = false;
            this.m_txtFormuleCle.LockZoneTexte = false;
            this.m_txtFormuleCle.Name = "m_txtFormuleCle";
            this.m_txtFormuleCle.Size = new System.Drawing.Size(621, 62);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleCle.TabIndex = 4;
            // 
            // m_txtFormuleLibelle
            // 
            this.m_txtFormuleLibelle.AllowGraphic = true;
            this.m_txtFormuleLibelle.AllowNullFormula = false;
            this.m_txtFormuleLibelle.AllowSaisieTexte = true;
            this.m_txtFormuleLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleLibelle.Formule = null;
            this.m_txtFormuleLibelle.Location = new System.Drawing.Point(11, 196);
            this.m_txtFormuleLibelle.LockEdition = false;
            this.m_txtFormuleLibelle.LockZoneTexte = false;
            this.m_txtFormuleLibelle.Name = "m_txtFormuleLibelle";
            this.m_txtFormuleLibelle.Size = new System.Drawing.Size(621, 62);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleLibelle.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 16);
            this.m_exStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document key|20140";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 16);
            this.m_exStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 0;
            this.label4.Text = "Document label|20139";
            // 
            // m_txtFormuleCategories
            // 
            this.m_txtFormuleCategories.AllowGraphic = true;
            this.m_txtFormuleCategories.AllowNullFormula = false;
            this.m_txtFormuleCategories.AllowSaisieTexte = true;
            this.m_txtFormuleCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleCategories.Formule = null;
            this.m_txtFormuleCategories.Location = new System.Drawing.Point(11, 31);
            this.m_txtFormuleCategories.LockEdition = false;
            this.m_txtFormuleCategories.LockZoneTexte = false;
            this.m_txtFormuleCategories.Name = "m_txtFormuleCategories";
            this.m_txtFormuleCategories.Size = new System.Drawing.Size(621, 62);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleCategories.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, -1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(625, 32);
            this.m_exStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 1;
            this.label3.Text = "Indicate available EDM categories for documents. The formula should return a list" +
                " of categories or a list of category id|20138";
            // 
            // listViewAutoFilledColumn2
            // 
            this.listViewAutoFilledColumn2.Field = "Libelle";
            this.listViewAutoFilledColumn2.PrecisionWidth = 0;
            this.listViewAutoFilledColumn2.ProportionnalSize = false;
            this.listViewAutoFilledColumn2.Text = "Category|30245";
            this.listViewAutoFilledColumn2.Visible = true;
            this.listViewAutoFilledColumn2.Width = 279;
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "Nom";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Name |164";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 176;
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel1.Controls.Add(this.m_txtFormuleAssociation);
            this.c2iPanel1.Controls.Add(this.m_txtFormuleCategories);
            this.c2iPanel1.Controls.Add(this.label2);
            this.c2iPanel1.Controls.Add(this.m_txtFormuleCle);
            this.c2iPanel1.Controls.Add(this.label3);
            this.c2iPanel1.Controls.Add(this.m_txtFormuleLibelle);
            this.c2iPanel1.Controls.Add(this.label4);
            this.c2iPanel1.Controls.Add(this.label1);
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanel1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanel1.Location = new System.Drawing.Point(0, 32);
            this.c2iPanel1.LockEdition = false;
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(669, 367);
            this.m_exStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanel1.TabIndex = 8;
            // 
            // CFormEditCopierMultipleLocalDansGed
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(669, 447);
            this.Controls.Add(this.c2iPanel1);
            this.Name = "CFormEditCopierMultipleLocalDansGed";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Transférer plusieurs fichiers dans la GED|20136";
            this.Controls.SetChildIndex(this.c2iPanel1, 0);
            this.c2iPanel1.ResumeLayout(false);
            this.c2iPanel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionCopierMultiLocalDansGed), typeof(CFormEditCopierMultipleLocalDansGed));
		}


        public CActionCopierMultiLocalDansGed CopierMultiLocalDansGed
		{
			get
			{
                return (CActionCopierMultiLocalDansGed)ObjetEdite;
			}
		}

		
	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();

            CopierMultiLocalDansGed.FormuleCle = m_txtFormuleCle.Formule;
            CopierMultiLocalDansGed.FormuleElementAssocie = m_txtFormuleAssociation.Formule;
            CopierMultiLocalDansGed.FormuleListeCategories = m_txtFormuleCategories.Formule;
            CopierMultiLocalDansGed.FormuleLibelleDocument = m_txtFormuleLibelle.Formule;
			return result;
		}

		

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
            m_txtFormuleCle.Init(ObjetEdite.Process, typeof(CActionCopierMultiLocalDansGed.CInfoFichierToGed));
            m_txtFormuleLibelle.Init(ObjetEdite.Process, typeof(CActionCopierMultiLocalDansGed.CInfoFichierToGed));
            m_txtFormuleAssociation.Init(ObjetEdite.Process, typeof(CActionCopierMultiLocalDansGed.CInfoFichierToGed));
            m_txtFormuleCategories.Init(ObjetEdite.Process, typeof(CProcess));

            m_txtFormuleCle.Formule = CopierMultiLocalDansGed.FormuleCle;
            m_txtFormuleAssociation.Formule = CopierMultiLocalDansGed.FormuleElementAssocie;
            m_txtFormuleCategories.Formule = CopierMultiLocalDansGed.FormuleListeCategories;
            m_txtFormuleLibelle.Formule = CopierMultiLocalDansGed.FormuleLibelleDocument;
		
		}

		
		
	}
}

