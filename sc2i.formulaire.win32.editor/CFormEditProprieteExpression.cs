using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
	/// <summary>
	/// Description résumée de CFormEditProprieteExpression.
	/// </summary>
	public class CFormEditProprieteExpression : System.Windows.Forms.Form
	{
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
		private C2iExpression m_expression = null;
        private CObjetPourSousProprietes m_objetPourSousProprietes = null;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Panel m_panelTotal;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel m_panelGauche;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditProprieteExpression()
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
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_panelGauche = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelTotal.SuspendLayout();
            this.m_panelGauche.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOk.Location = new System.Drawing.Point(233, 346);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 24);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(324, 346);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(74, 24);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelTotal.Controls.Add(this.m_panelGauche);
            this.m_panelTotal.Controls.Add(this.splitter1);
            this.m_panelTotal.Controls.Add(this.m_wndAideFormule);
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(646, 340);
            this.m_panelTotal.TabIndex = 4;
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Controls.Add(this.m_txtFormule);
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(451, 340);
            this.m_panelGauche.TabIndex = 2;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 5);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(437, 328);
            this.m_txtFormule.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(451, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 340);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(455, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(191, 340);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditProprieteExpression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(648, 375);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelTotal);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnAnnuler);
            this.MinimizeBox = false;
            this.Name = "CFormEditProprieteExpression";
            this.Text = "Formula|140";
            this.Load += new System.EventHandler(this.CFormEditProprieteExpression_Load);
            this.m_panelTotal.ResumeLayout(false);
            this.m_panelGauche.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


		public C2iExpression EditeExpression(C2iExpression expression)
		{
			m_expression = expression;
			if ( ShowDialog() == DialogResult.OK )
				return m_expression;
			return expression;
		}

		private void CFormEditProprieteExpression_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_wndAideFormule.ObjetInterroge = m_objetPourSousProprietes;
			m_wndAideFormule.FournisseurProprietes = m_fournisseurProprietes;
			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
			if ( m_expression != null )
				m_txtFormule.Text = m_expression.GetString();
			else
				m_txtFormule.Text = "";
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_txtFormule.Text.Trim() == "" )
			{
				m_expression = null;
			}
			else
			{
				CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
				CResultAErreur result = new CAnalyseurSyntaxiqueExpression(ctx).AnalyseChaine ( m_txtFormule.Text );
				if ( !result )
				{
					CFormAlerte.Afficher ( result);
					return ;
				}
				m_expression = (C2iExpression)result.Data;
			}
			DialogResult = DialogResult.OK;
			Close();
		}


		public CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietes;
			}
			set
			{
				m_objetPourSousProprietes = value;
				m_wndAideFormule.ObjetInterroge = m_objetPourSousProprietes;
			}
		}

		public IFournisseurProprietesDynamiques Fournisseur
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
			}
		}
	}

	[AutoExec("Autoexec")]
	public class CEditeurExpressionPopup : IEditeurExpression
	{
		private CObjetPourSousProprietes m_objetPourSousProprietes = null;
		private IFournisseurProprietesDynamiques m_fournisseur = null;

		//---------------------------------------------------
		public CEditeurExpressionPopup()
		{
		}

		//---------------------------------------------------
		public CEditeurExpressionPopup ( CObjetPourSousProprietes objetPourSousProprietes)
		{
            m_objetPourSousProprietes = objetPourSousProprietes;
		}

		//---------------------------------------------------
		public static void Autoexec()
		{
			CProprieteExpressionEditor.SetTypeEditeur(typeof(CEditeurExpressionPopup));
		}

		//---------------------------------------------------
		public CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietes;
			}
			set
			{
                m_objetPourSousProprietes = value;
			}
		}

		//---------------------------------------------------
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseur;
			}
			set
			{
				m_fournisseur = value;
			}
		}

		//---------------------------------------------------
		public C2iExpression EditeExpression(C2iExpression expression)
		{
			CFormEditProprieteExpression form = new CFormEditProprieteExpression();
			form.ObjetPourSousProprietes = m_objetPourSousProprietes;
				if ( m_fournisseur != null )
			form.Fournisseur = m_fournisseur;
			C2iExpression exp = form.EditeExpression ( expression );
			form.Dispose();
			return exp;
		}
	}
}
