using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;	

namespace sc2i.win32.expression
{
	/// <summary>
	/// Description résumée de CFormStdEditeFormule.
	/// </summary>
	public class CFormStdEditeFormule : System.Windows.Forms.Form
	{
		private string m_strFormule;
        bool m_bNullAutorise = false;

		private CObjetPourSousProprietes m_objetAnalyse;
		private IFournisseurProprietesDynamiques m_fournisseur;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormStdEditeFormule()
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
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(555, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(198, 418);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(550, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 418);
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
            this.panel1.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(278, 8);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.Location = new System.Drawing.Point(174, 8);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(550, 378);
            this.m_txtFormule.TabIndex = 4;
            // 
            // CFormStdEditeFormule
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(753, 418);
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_wndAideFormule);
            this.Name = "CFormStdEditeFormule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formula edition|101";
            this.Load += new System.EventHandler(this.CFormStdEditeFormule_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public static C2iExpression EditeFormule(string strFormule,
			IFournisseurProprietesDynamiques fournisseur,
			Type tp)
		{
			return EditeFormule(strFormule,
				fournisseur,
				new CObjetPourSousProprietes(tp));
		}

		public static C2iExpression EditeFormule ( string strFormule, 
			IFournisseurProprietesDynamiques fournisseur,
			CObjetPourSousProprietes objetAnalyse )
		{
			CFormStdEditeFormule form = new CFormStdEditeFormule();
			form.m_strFormule = strFormule;
			form.m_objetAnalyse = objetAnalyse;
			form.m_fournisseur = fournisseur;
			C2iExpression formule = null;
            if (form.ShowDialog() == DialogResult.OK)
            {
                formule = form.m_txtFormule.Formule;
            }
            else
            {
                CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression(fournisseur, objetAnalyse);
                CResultAErreur result = new CAnalyseurSyntaxiqueExpression(ctx).AnalyseChaine(strFormule);
                if (result)
                    formule = result.Data as C2iExpression;
            }
			form.Dispose();
			return formule;
		}

        public static bool EditeFormule(ref C2iExpression formule,
            IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse,
            bool bAutoriseNull)
        {
            CFormStdEditeFormule form = new CFormStdEditeFormule();
            form.m_strFormule = formule == null?"":formule.GetString();
            form.m_objetAnalyse = objetAnalyse;
            form.m_fournisseur = fournisseur;
            form.m_bNullAutorise = bAutoriseNull;
            bool bRetour = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                formule = form.m_txtFormule.Formule;
                bRetour = true;
            }
            form.Dispose();
            return bRetour;
        }

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void CFormStdEditeFormule_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_txtFormule.Init(m_fournisseur, m_objetAnalyse);
			m_txtFormule.Text = m_strFormule;
			m_wndAideFormule.FournisseurProprietes = m_fournisseur;
			m_wndAideFormule.ObjetInterroge = m_objetAnalyse;
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
			DialogResult = DialogResult.OK;
			Close();
		}


	}
}
