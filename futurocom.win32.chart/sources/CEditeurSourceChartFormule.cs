using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using futurocom.chart;	

namespace futurocom.win32.chart.sources
{
	/// <summary>
	/// Description résumée de CEditeurSourceChartFormule.
	/// </summary>
    [AutoExec("Autoexec")]
    public class CEditeurSourceChartFormule : Form, IFormEditSourceChart
	{
		private string m_strFormule;
        bool m_bNullAutorise = false;

        private CParametreSourceChartFormule m_parametreFormule = null;

		private CObjetPourSousProprietes m_objetAnalyse;
		private IFournisseurProprietesDynamiques m_fournisseur;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
        private CExtStyle cExtStyle1;
        private Panel panel2;
        private Label label1;
        private TextBox m_txtSourceName;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CEditeurSourceChartFormule()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
            CWin32Traducteur.Translate(this);
		}

        public static void Autoexec()
        {
            CEditeurSourceChart.RegisterEditeur(typeof(CParametreSourceChartFormule), typeof(CEditeurSourceChartFormule));
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
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtSourceName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(555, 39);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(198, 379);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(550, 39);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 379);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 40);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Image = global::futurocom.win32.chart.Resource1.cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(307, 8);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 1;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.Image = global::futurocom.win32.chart.Resource1.check;
            this.m_btnOk.Location = new System.Drawing.Point(203, 8);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 39);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(550, 339);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormule.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_txtSourceName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(753, 39);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source name|20002";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSourceName
            // 
            this.m_txtSourceName.Location = new System.Drawing.Point(140, 10);
            this.m_txtSourceName.Name = "m_txtSourceName";
            this.m_txtSourceName.Size = new System.Drawing.Size(319, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtSourceName, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtSourceName, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSourceName.TabIndex = 1;
            // 
            // CEditeurSourceChartFormule
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(753, 418);
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_wndAideFormule);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CEditeurSourceChartFormule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Formula chart source|20001";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
        public void InitChamps ( CParametreSourceChart parametre,
            IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse )
        {
            m_parametreFormule = parametre as CParametreSourceChartFormule;
            if (m_parametreFormule == null)
                return;
            m_txtFormule.Formule = m_parametreFormule.Formule;
            m_txtFormule.Init(fournisseur, objetAnalyse);
			m_objetAnalyse = objetAnalyse;
            m_fournisseur = fournisseur;
            m_wndAideFormule.FournisseurProprietes = m_fournisseur;
            m_wndAideFormule.ObjetInterroge = m_objetAnalyse;
            m_txtSourceName.Text = parametre.SourceName;
		}



		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
            if (m_txtFormule.Text.Trim() == "" && m_bNullAutorise)
                m_txtFormule.Formule = null;
            else
            {
                C2iExpression formule = m_txtFormule.Formule;
                if (formule == null)
                {
                    CFormAlerte.Afficher(m_txtFormule.ResultAnalyse);
                    return;
                }
            }
            m_parametreFormule.Formule = m_txtFormule.Formule;
            m_parametreFormule.SourceName = m_txtSourceName.Text;
			DialogResult = DialogResult.OK;
			Close();
		}


	}
}
