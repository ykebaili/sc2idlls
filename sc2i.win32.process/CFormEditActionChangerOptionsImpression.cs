using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.multitiers.client;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionChangerOptionsImpression : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule = null;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleClient;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel m_lnkSelectImprimanteClient;
		private System.Windows.Forms.LinkLabel m_lnkSelectImprimanteServeur;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleServeur;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionChangerOptionsImpression()
		{
			// Cet appel est requis par le Concepteur Windows Form.
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionChangerOptionsImpression), typeof(CFormEditActionChangerOptionsImpression));
		}


		public CActionChangerOptionsImpression ActionChangerOptions
		{
			get
			{
				return (CActionChangerOptionsImpression)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_lnkSelectImprimanteServeur = new System.Windows.Forms.LinkLabel();
            this.m_lnkSelectImprimanteClient = new System.Windows.Forms.LinkLabel();
            this.m_txtFormuleServeur = new sc2i.win32.expression.CControleEditeFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFormuleClient = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Controls.Add(this.m_lnkSelectImprimanteServeur);
            this.panel2.Controls.Add(this.m_lnkSelectImprimanteClient);
            this.panel2.Controls.Add(this.m_txtFormuleServeur);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.m_txtFormuleClient);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(568, 279);
            this.panel2.TabIndex = 2;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(392, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 279);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_lnkSelectImprimanteServeur
            // 
            this.m_lnkSelectImprimanteServeur.Location = new System.Drawing.Point(184, 131);
            this.m_lnkSelectImprimanteServeur.Name = "m_lnkSelectImprimanteServeur";
            this.m_lnkSelectImprimanteServeur.Size = new System.Drawing.Size(72, 16);
            this.m_lnkSelectImprimanteServeur.TabIndex = 6;
            this.m_lnkSelectImprimanteServeur.TabStop = true;
            this.m_lnkSelectImprimanteServeur.Text = "Select|20";
            this.m_lnkSelectImprimanteServeur.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkSelectImprimanteServeur_LinkClicked);
            // 
            // m_lnkSelectImprimanteClient
            // 
            this.m_lnkSelectImprimanteClient.Location = new System.Drawing.Point(184, 8);
            this.m_lnkSelectImprimanteClient.Name = "m_lnkSelectImprimanteClient";
            this.m_lnkSelectImprimanteClient.Size = new System.Drawing.Size(72, 16);
            this.m_lnkSelectImprimanteClient.TabIndex = 5;
            this.m_lnkSelectImprimanteClient.TabStop = true;
            this.m_lnkSelectImprimanteClient.Text = "Select|20";
            this.m_lnkSelectImprimanteClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkSelectImprimanteClient_LinkClicked);
            // 
            // m_txtFormuleServeur
            // 
            this.m_txtFormuleServeur.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleServeur.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleServeur.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleServeur.Formule = null;
            this.m_txtFormuleServeur.Location = new System.Drawing.Point(8, 152);
            this.m_txtFormuleServeur.LockEdition = false;
            this.m_txtFormuleServeur.Name = "m_txtFormuleServeur";
            this.m_txtFormuleServeur.Size = new System.Drawing.Size(376, 103);
            this.m_txtFormuleServeur.TabIndex = 4;
            this.m_txtFormuleServeur.Enter += new System.EventHandler(this.m_txtFormuleClient_Enter);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server printer*|107";
            // 
            // m_txtFormuleClient
            // 
            this.m_txtFormuleClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleClient.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleClient.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleClient.Formule = null;
            this.m_txtFormuleClient.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormuleClient.LockEdition = false;
            this.m_txtFormuleClient.Name = "m_txtFormuleClient";
            this.m_txtFormuleClient.Size = new System.Drawing.Size(376, 104);
            this.m_txtFormuleClient.TabIndex = 2;
            this.m_txtFormuleClient.Enter += new System.EventHandler(this.m_txtFormuleClient_Enter);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Client printer*|106";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "(*)Leave the zone blank to use the user\'s default printer|108";
            // 
            // CFormEditActionChangerOptionsImpression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 325);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionChangerOptionsImpression";
            this.Text = "Change printer options|105";
            this.Load += new System.EventHandler(this.CFormEditActionChangerOptionsImpression_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			if ( m_txtFormuleClient.Text.Trim() != "" )
			{
				result = analyseur.AnalyseChaine ( m_txtFormuleClient.Text );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in client printer formula|30005"));
					return result;
				}
				ActionChangerOptions.FormuleImprimanteClient = (C2iExpression)result.Data;
			}
			else
				ActionChangerOptions.FormuleImprimanteClient = null;

			if ( m_txtFormuleServeur.Text.Trim() != "" )
			{
				result = analyseur.AnalyseChaine ( m_txtFormuleServeur.Text );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in server printer formula|30006"));
					return result;
				}
				ActionChangerOptions.FormuleImprimanteServeur= (C2iExpression)result.Data;
			}
			else
				ActionChangerOptions.FormuleImprimanteServeur = null;

			result.Data = null;
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_txtFormuleClient.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
			m_txtFormuleServeur.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			if ( ActionChangerOptions.FormuleImprimanteServeur != null )
				m_txtFormuleServeur.Text = ActionChangerOptions.FormuleImprimanteServeur.GetString();
			if(  ActionChangerOptions.FormuleImprimanteClient != null )
				m_txtFormuleClient.Text = ActionChangerOptions.FormuleImprimanteClient.GetString();
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void CFormEditActionChangerOptionsImpression_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_txtFormule = m_txtFormuleClient;
			m_txtFormuleClient_Enter ( m_txtFormuleClient, new EventArgs()) ;
		}

		private void m_txtFormuleClient_Enter(object sender, System.EventArgs e)
		{
			if ( m_txtFormule != null )
				m_txtFormule.BackColor = Color.White;
			m_txtFormule = (sc2i.win32.expression.CControleEditeFormule)sender;
			m_txtFormule.BackColor = Color.LightGreen;

		}

		//-----------------------------------------------------
		private void m_lnkSelectImprimanteClient_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ContextMenu menu = new ContextMenu();
			foreach ( string strPrinter in PrinterSettings.InstalledPrinters )
			{
				MenuItem item = new MenuItem ( strPrinter );
				item.Click += new EventHandler(itemClient_Click);
				menu.MenuItems.Add ( item );
			}
			menu.Show ( m_lnkSelectImprimanteClient, new Point ( 0, m_lnkSelectImprimanteClient.Height ));
		}

		//-----------------------------------------------------
		private void itemClient_Click(object sender, EventArgs e)
		{
			m_txtFormuleClient.Text = "\""+((MenuItem)sender).Text+"\"";
		}

		private void m_lnkSelectImprimanteServeur_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ContextMenu menu = new ContextMenu();
			foreach ( string strPrinter in CFournisseurNomImprimantes.GetNomsImprimantesServeur ( CSessionClient.GetSessionUnique().IdSession ) )
			{
				MenuItem item = new MenuItem ( strPrinter );
				item.Click += new EventHandler(itemServeur_Click);
				menu.MenuItems.Add ( item );
			}
			menu.Show ( m_lnkSelectImprimanteServeur, new Point ( 0, m_lnkSelectImprimanteServeur.Height ));
		}

		//-----------------------------------------------------
		private void itemServeur_Click(object sender, EventArgs e)
		{
			m_txtFormuleServeur.Text = "\""+((MenuItem)sender).Text+"\"";
		}

		
	}
}

