using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionPurgeJournalisation : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionPurgeJournalisation()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionPurgeJournalisation), typeof(CFormEditActionPurgeJournalisation));
		}


		public CActionPurgeJournalisation ActionPurger
		{
			get
			{
				return (CActionPurgeJournalisation)ObjetEdite;
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
			this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
			this.label1 = new System.Windows.Forms.Label();
			this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.m_txtFormule);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.m_wndAideFormule);
			this.panel2.Location = new System.Drawing.Point(0, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(584, 347);
			this.panel2.TabIndex = 2;
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
			this.m_txtFormule.Size = new System.Drawing.Size(392, 319);
			this.m_txtFormule.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Process date|20004";
			// 
			// m_wndAideFormule
			// 
			this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
			this.m_wndAideFormule.FournisseurProprietes = null;
			this.m_wndAideFormule.Location = new System.Drawing.Point(408, 0);
			this.m_wndAideFormule.Name = "m_wndAideFormule";
			this.m_wndAideFormule.SendIdChamps = false;
			this.m_wndAideFormule.Size = new System.Drawing.Size(176, 347);
			this.m_wndAideFormule.TabIndex = 0;
			this.m_wndAideFormule.ObjetInterroge = null;
			this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
			// 
			// CFormEditActionPurgeJournalisation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 398);
			this.Controls.Add(this.panel2);
			this.Name = "CFormEditActionPurgeJournalisation";
			this.Text = "Clear Data history|20006";
			this.Load += new System.EventHandler(this.CFormEditActionPurgeJournalisation_Load);
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
			result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
				return result;
			ActionPurger.FormuleDateLimite = (C2iExpression)result.Data;
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = false;
			if ( ActionPurger.FormuleDateLimite != null )
			{
				m_txtFormule.Text = ActionPurger.FormuleDateLimite.GetString();
			}
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void CFormEditActionPurgeJournalisation_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

		}

		private void m_txtCleRegistre_TextChanged(object sender, EventArgs e)
		{

		}
		

		




	}
}

