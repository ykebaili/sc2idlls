using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionSupprimerEntite : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private SplitContainer m_splitContainer;
        private CheckBox m_chkEnCascade;
        private CheckBox m_chkPurge;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionSupprimerEntite()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionSupprimerEntite), typeof(CFormEditActionSupprimerEntite));
		}


		public CActionSupprimerEntite ActionSupprimer
		{
			get
			{
				return (CActionSupprimerEntite)ObjetEdite;
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
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_chkEnCascade = new System.Windows.Forms.CheckBox();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_chkPurge = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_splitContainer);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(579, 357);
            this.panel2.TabIndex = 2;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_chkPurge);
            this.m_splitContainer.Panel1.Controls.Add(this.m_chkEnCascade);
            this.m_splitContainer.Panel1.Controls.Add(this.m_txtFormule);
            this.m_splitContainer.Panel1.Controls.Add(this.label1);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(579, 357);
            this.m_splitContainer.SplitterDistance = 400;
            this.m_splitContainer.TabIndex = 3;
            // 
            // m_chkEnCascade
            // 
            this.m_chkEnCascade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkEnCascade.Location = new System.Drawing.Point(8, 330);
            this.m_chkEnCascade.Name = "m_chkEnCascade";
            this.m_chkEnCascade.Size = new System.Drawing.Size(201, 20);
            this.m_chkEnCascade.TabIndex = 3;
            this.m_chkEnCascade.Text = "Cascade delete child entities|207";
            this.m_chkEnCascade.UseVisualStyleBackColor = true;
            this.m_chkEnCascade.CheckedChanged += new System.EventHandler(this.m_chkEnCascade_CheckedChanged);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 28);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(383, 292);
            this.m_txtFormule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entity to delete|155";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(171, 353);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_chkPurge
            // 
            this.m_chkPurge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkPurge.Location = new System.Drawing.Point(215, 329);
            this.m_chkPurge.Name = "m_chkPurge";
            this.m_chkPurge.Size = new System.Drawing.Size(176, 20);
            this.m_chkPurge.TabIndex = 4;
            this.m_chkPurge.Text = "Administrator purge|20128";
            this.m_chkPurge.UseVisualStyleBackColor = true;
            // 
            // CFormEditActionSupprimerEntite
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(579, 403);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionSupprimerEntite";
            this.Text = "Delete an entity|154";
            this.Load += new System.EventHandler(this.CFormEditActionSupprimerEntite_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion



        //---------------------------------------------------------------------------------------
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
				return result;
			ActionSupprimer.ExpressionEntiteASupprimer = (C2iExpression)result.Data;
            ActionSupprimer.DeleteFillesEnCascade = m_chkEnCascade.Checked;
            ActionSupprimer.PurgeAdmin = m_chkPurge.Checked;
			return result;
		}


        //---------------------------------------------------------------------------------------
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			if ( ActionSupprimer.ExpressionEntiteASupprimer != null )
			{
				m_txtFormule.Text = ActionSupprimer.ExpressionEntiteASupprimer.GetString();
			}
            m_chkEnCascade.Checked = ActionSupprimer.DeleteFillesEnCascade;
            m_chkPurge.Checked = ActionSupprimer.PurgeAdmin;
		}

        //---------------------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

        //---------------------------------------------------------------------------------------
        private void CFormEditActionSupprimerEntite_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

        //---------------------------------------------------------------------------------------
        private void m_chkEnCascade_CheckedChanged(object sender, EventArgs e)
        {
            //**************** ATTENTION ********************
            if (m_chkEnCascade.Checked)
            {
                DialogResult reponse = CFormAlerte.Afficher(I.T("This action could be dangerous. All elements that have a parent relation with entity de delete, will be deleted. Are you sure to continue ?|212"), EFormAlerteType.Question);
                if (reponse == DialogResult.No)
                    m_chkEnCascade.Checked = false;
            }
        }

	}
}

