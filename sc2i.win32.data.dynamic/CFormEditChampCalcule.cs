using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableFiltreCalculee.
	/// </summary>
	public class CFormEditChampCalcule : System.Windows.Forms.Form
	{
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = new CFournisseurPropDynStd ( true );
		private Type m_typeDonnees = null;
		private C2iChampExport m_champ = null;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.TextBox m_txtNomChamp;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
        protected CExtStyle m_ExtStyle1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditChampCalcule()
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
            this.m_txtNomChamp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.c2iPanelOmbre2 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.c2iPanelOmbre2.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomChamp);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(472, 80);
            this.m_ExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 18;
            // 
            // m_txtNomChamp
            // 
            this.m_txtNomChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomChamp.Location = new System.Drawing.Point(112, 28);
            this.m_txtNomChamp.Name = "m_txtNomChamp";
            this.m_txtNomChamp.Size = new System.Drawing.Size(336, 20);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomChamp.TabIndex = 2;
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Champ calculé|100";
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(488, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(216, 302);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAide.TabIndex = 19;
			this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.c2iPanelOmbre2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.c2iPanelOmbre1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 302);
            this.m_ExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 20;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(256, 272);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 22;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(128, 272);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 21;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // c2iPanelOmbre2
            // 
            this.c2iPanelOmbre2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre2.Controls.Add(this.m_txtFormule);
            this.c2iPanelOmbre2.Controls.Add(this.label3);
            this.c2iPanelOmbre2.Location = new System.Drawing.Point(8, 88);
            this.c2iPanelOmbre2.LockEdition = false;
            this.c2iPanelOmbre2.Name = "c2iPanelOmbre2";
            this.c2iPanelOmbre2.Size = new System.Drawing.Size(472, 176);
            this.m_ExtStyle1.SetStyleBackColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre2.TabIndex = 20;
            this.c2iPanelOmbre2.Paint += new System.Windows.Forms.PaintEventHandler(this.c2iPanelOmbre2_Paint);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(440, 128);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormule.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 3;
            this.label3.Text = "Formula|102";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(485, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 302);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // CFormEditChampCalcule
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(704, 302);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_wndAide);
            this.Name = "CFormEditChampCalcule";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Computed field|100";
            this.Load += new System.EventHandler(this.CFormEditVariableFiltreCalculee_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.c2iPanelOmbre2.ResumeLayout(false);
            this.c2iPanelOmbre2.PerformLayout();
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
            
            m_wndAide.FournisseurProprietes = m_fournisseurProprietes != null ? m_fournisseurProprietes : new CFournisseurPropDynStd(true);
			m_wndAide.ObjetInterroge = m_typeDonnees;
			m_txtNomChamp.Text = m_champ.NomChamp;
			m_txtFormule.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtFormule.Text = ((C2iOrigineChampExportExpression)m_champ.Origine).Expression!=null?((C2iOrigineChampExportExpression)m_champ.Origine).Expression.GetString():"";
		}

		/// //////////////////////////////////////////////////////
		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAide.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////////////////////
		private void Init ( C2iChampExport champ, Type typeDonnees )
		{
			m_champ = champ;
			m_typeDonnees = typeDonnees;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeChamp ( C2iChampExport champ, Type typeDonnees, IFournisseurProprietesDynamiques fournisseur )
		{
			CFormEditChampCalcule  form = new CFormEditChampCalcule();
			if ( fournisseur != null )
				form.m_fournisseurProprietes = fournisseur;
			form.Init ( champ, typeDonnees );
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		/// //////////////////////////////////////////////////////
		protected C2iExpression GetExpression()
		{
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression( m_fournisseurProprietes, m_typeDonnees );
			CAnalyseurSyntaxique analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			CResultAErreur result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return null;
			}
			return (C2iExpression)result.Data;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_txtNomChamp.Text.Trim() == "" )
			{
				CFormAlerte.Afficher(I.T("Enter a field name|30008"), EFormAlerteType.Exclamation);
				return;
			}
			C2iExpression expression = GetExpression();
			if ( expression == null )
				return;
			m_champ.NomChamp = m_txtNomChamp.Text.Replace(" ","_").Trim();
			((C2iOrigineChampExportExpression)m_champ.Origine).Expression = expression;
			DialogResult = DialogResult.OK;
			Close();

		}
	}
}
