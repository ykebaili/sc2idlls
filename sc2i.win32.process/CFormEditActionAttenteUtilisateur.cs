using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionAttenteUtilisateur : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.expression.CControleEditeFormule m_txtCodeAttente;
		private Label label2;

		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleEnCours = null;
		private Label label3;
        private sc2i.win32.expression.CControleEditeFormule m_txtIdUtilisateur;
        private Label label4;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionAttenteUtilisateur()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
			m_txtFormuleEnCours = m_txtFormule;

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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionAttenteUtilisateur), typeof(CFormEditActionAttenteUtilisateur));
		}


		public CActionAttenteUtilisateur ActionAttente
		{
			get
			{
				return (CActionAttenteUtilisateur)ObjetEdite;
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
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtCodeAttente = new sc2i.win32.expression.CControleEditeFormule();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_txtIdUtilisateur = new sc2i.win32.expression.CControleEditeFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_txtIdUtilisateur);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_txtCodeAttente);
            this.panel2.Controls.Add(this.m_txtFormule);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 287);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(8, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(398, 44);
            this.label3.TabIndex = 4;
            this.label3.Text = "WARNING : the waiting code must be unique in the whole database \n If a waiting ac" +
                "tion having the same code already exists it will be deleted|30004";
            // 
            // m_txtCodeAttente
            // 
            this.m_txtCodeAttente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCodeAttente.BackColor = System.Drawing.Color.White;
            this.m_txtCodeAttente.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCodeAttente.Formule = null;
            this.m_txtCodeAttente.Location = new System.Drawing.Point(8, 98);
            this.m_txtCodeAttente.LockEdition = false;
            this.m_txtCodeAttente.Name = "m_txtCodeAttente";
            this.m_txtCodeAttente.Size = new System.Drawing.Size(398, 59);
            this.m_txtCodeAttente.TabIndex = 3;
            this.m_txtCodeAttente.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(398, 57);
            this.m_txtFormule.TabIndex = 2;
            this.m_txtFormule.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Waiting code|30003";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message to display to the user|104";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(414, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 287);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_txtIdUtilisateur
            // 
            this.m_txtIdUtilisateur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtIdUtilisateur.BackColor = System.Drawing.Color.White;
            this.m_txtIdUtilisateur.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtIdUtilisateur.Formule = null;
            this.m_txtIdUtilisateur.Location = new System.Drawing.Point(11, 223);
            this.m_txtIdUtilisateur.LockEdition = false;
            this.m_txtIdUtilisateur.Name = "m_txtIdUtilisateur";
            this.m_txtIdUtilisateur.Size = new System.Drawing.Size(398, 59);
            this.m_txtIdUtilisateur.TabIndex = 6;
            this.m_txtIdUtilisateur.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(371, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "User id (let null for current user)|20115";
            // 
            // CFormEditActionAttenteUtilisateur
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(590, 333);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionAttenteUtilisateur";
            this.Text = "User waiting|103";
            this.Load += new System.EventHandler(this.CFormEditActionAttenteUtilisateur_Load);
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
			ActionAttente.FormuleMessage = (C2iExpression)result.Data;

			ActionAttente.FormuleCodeAttente = m_txtCodeAttente.Formule;
			if ( m_txtCodeAttente.Text.Trim() != "" && 
				m_txtCodeAttente.Formule == null )
				result = m_txtCodeAttente.ResultAnalyse;
            if (!result)
                return result;

            ActionAttente.FormuleUtilisateur = m_txtIdUtilisateur.Formule;
            if (m_txtIdUtilisateur.Text.Trim() != "" &&
                m_txtIdUtilisateur.Formule == null)
                result = m_txtIdUtilisateur.ResultAnalyse;
            if (!result)
                return result;

			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
			m_txtCodeAttente.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            m_txtIdUtilisateur.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);

			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = true;

			if ( ActionAttente.FormuleMessage != null )
			{
				m_txtFormule.Text = ActionAttente.FormuleMessage.GetString();
			}
			if (ActionAttente.FormuleCodeAttente != null)
			{
				m_txtCodeAttente.Text = ActionAttente.FormuleCodeAttente.GetString();
			}
            m_txtIdUtilisateur.Formule = ActionAttente.FormuleUtilisateur;
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormuleEnCours, nPosCurseur, strCommande );
		}

        private void CFormEditActionAttenteUtilisateur_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		private void m_txtFormule_Enter(object sender, EventArgs e)
		{
			if (sender is sc2i.win32.expression.CControleEditeFormule)
			{
				if (m_txtFormuleEnCours != null)
					m_txtFormuleEnCours.BackColor = Color.White;
				m_txtFormuleEnCours = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_txtFormuleEnCours.BackColor = Color.LightGreen;
			}
		}

		
		

		




	}
}

