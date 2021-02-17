using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Description résumée de CFormTestExpression.
	/// </summary>
	public class CFormTestExpression : System.Windows.Forms.Form
	{
		private object m_objetTest = null;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnEvaluer;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox m_txtResult;
		private System.Windows.Forms.TreeView m_arbreAnalyse;
		private System.Windows.Forms.TextBox m_txtFormule;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormTestExpression()
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
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_txtResult = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_arbreAnalyse = new System.Windows.Forms.TreeView();
            this.m_btnEvaluer = new System.Windows.Forms.Button();
            this.m_txtFormule = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(539, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(168, 411);
            this.m_wndAide.TabIndex = 0;
            this.m_wndAide.ObjetInterroge = null;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(536, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 411);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.m_btnEvaluer);
            this.panel1.Controls.Add(this.m_txtFormule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 411);
            this.panel1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 182);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(523, 223);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_txtResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(515, 197);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result|103";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_txtResult
            // 
            this.m_txtResult.AcceptsReturn = true;
            this.m_txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtResult.Location = new System.Drawing.Point(8, 8);
            this.m_txtResult.Multiline = true;
            this.m_txtResult.Name = "m_txtResult";
            this.m_txtResult.ReadOnly = true;
            this.m_txtResult.Size = new System.Drawing.Size(499, 183);
            this.m_txtResult.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_arbreAnalyse);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(432, 142);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analyse|104";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_arbreAnalyse
            // 
            this.m_arbreAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbreAnalyse.Location = new System.Drawing.Point(8, 8);
            this.m_arbreAnalyse.Name = "m_arbreAnalyse";
            this.m_arbreAnalyse.Size = new System.Drawing.Size(416, 128);
            this.m_arbreAnalyse.TabIndex = 0;
            // 
            // m_btnEvaluer
            // 
            this.m_btnEvaluer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEvaluer.Location = new System.Drawing.Point(8, 152);
            this.m_btnEvaluer.Name = "m_btnEvaluer";
            this.m_btnEvaluer.Size = new System.Drawing.Size(523, 24);
            this.m_btnEvaluer.TabIndex = 2;
            this.m_btnEvaluer.Text = "Evaluate|105";
            this.m_btnEvaluer.Click += new System.EventHandler(this.m_btnEvaluer_Click);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.Location = new System.Drawing.Point(8, 16);
            this.m_txtFormule.Multiline = true;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(523, 130);
            this.m_txtFormule.TabIndex = 0;
            this.m_txtFormule.Text = "textBox1";
            // 
            // CFormTestExpression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(707, 411);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_wndAide);
            this.Name = "CFormTestExpression";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formula|102";
            this.Load += new System.EventHandler(this.CFormTestExpression_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// ////////////////////////////////////////////////////////////////////////////
		private void CFormTestExpression_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_wndAide.OnSendCommande += new SendCommande(OnReceiveCommande);
		}

		/// ////////////////////////////////////////////////////
		public void OnReceiveCommande ( string strCommande, int nPosCurseur )
		{
			m_wndAide.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		/// ////////////////////////////////////////////////////
		public object ObjetTest
		{
			get
			{
				return m_objetTest;
			}
			set
			{
				m_objetTest = value;
				m_wndAide.ObjetInterroge = new CObjetPourSousProprietes(value);
			}
		}

		/// ////////////////////////////////////////////////////
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
				m_wndAide.FournisseurProprietes = m_fournisseurProprietes;
			}
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public static void AfficheForm ( object objetTeste, 
			IFournisseurProprietesDynamiques fournisseur )
		{
			CFormTestExpression form = new CFormTestExpression();
			form.FournisseurProprietes = fournisseur;
			form.ObjetTest = objetTeste;
			form.ShowDialog();
			form.Dispose();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnEvaluer_Click(object sender, System.EventArgs e)
		{
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression(m_fournisseurProprietes, m_objetTest.GetType());
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			CResultAErreur result = analyseur.AnalyseChaine(m_txtFormule.Text);
			m_arbreAnalyse.Nodes.Clear();
			if ( !result )
			{
				m_txtResult.Text = result.Erreur.ToString();
				return;
			}
			C2iExpression expression = (C2iExpression)result.Data;
			if ( expression == null )
			{
				m_txtResult.Text = "Analyzed expression is null|30001";
				return;
			}
            m_txtResult.Text = "Expression :" + Environment.NewLine + expression.GetString() + Environment.NewLine + "**********************";
			FillNodes ( m_arbreAnalyse.Nodes, expression );
            m_txtResult.Text += "Evaluation :" + Environment.NewLine;
			result = expression.Eval ( new CContexteEvaluationExpression(m_objetTest));
			if ( !result )
			{
				m_txtResult.Text += result.Erreur.ToString();
			}
			else
			{	
				m_txtResult.Text = result.Data.ToString();
			}
		}

		private void FillNodes ( TreeNodeCollection nodes, C2iExpression expression )
		{
			TreeNode node = nodes.Add ( expression.IdExpression+" : "+expression.GetString() );
			foreach ( C2iExpression parametre in expression.Parametres )
				FillNodes ( node.Nodes, parametre );
		}
				

	}
}
