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
using sc2i.win32.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionSelectionFichierClient : CFormEditActionFonction
	{
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleCourante;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleTitre;
        private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private CheckBox m_chkForSave;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleFiltre;
        private Label label2;
        private Label label3;
        private CControleEditeFormule m_txtFormuleRepertoireInitial;
        private Label label4;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionSelectionFichierClient()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionSelectionFichierClient), typeof(CFormEditActionSelectionFichierClient));
		}


		public CActionSelectionFichierClient ActionSelectionFichierClient
		{
			get
			{
				return (CActionSelectionFichierClient)ObjetEdite;
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
            this.m_chkForSave = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtFormuleFiltre = new sc2i.win32.expression.CControleEditeFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFormuleTitre = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtFormuleRepertoireInitial = new sc2i.win32.expression.CControleEditeFormule();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Text = "Store the result in";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_chkForSave);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_txtFormuleRepertoireInitial);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_txtFormuleFiltre);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.m_txtFormuleTitre);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 359);
            this.panel2.TabIndex = 2;
            // 
            // m_chkForSave
            // 
            this.m_chkForSave.AutoSize = true;
            this.m_chkForSave.Location = new System.Drawing.Point(10, 8);
            this.m_chkForSave.Name = "m_chkForSave";
            this.m_chkForSave.Size = new System.Drawing.Size(145, 17);
            this.m_chkForSave.TabIndex = 3;
            this.m_chkForSave.Text = "Select file for save|20021";
            this.m_chkForSave.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(90, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(381, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "(ex:File text|*.*|All files|*.*)|20024";
            // 
            // m_txtFormuleFiltre
            // 
            this.m_txtFormuleFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleFiltre.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleFiltre.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleFiltre.Formule = null;
            this.m_txtFormuleFiltre.Location = new System.Drawing.Point(9, 215);
            this.m_txtFormuleFiltre.LockEdition = false;
            this.m_txtFormuleFiltre.Name = "m_txtFormuleFiltre";
            this.m_txtFormuleFiltre.Size = new System.Drawing.Size(528, 52);
            this.m_txtFormuleFiltre.TabIndex = 4;
            this.m_txtFormuleFiltre.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter|20023";
            // 
            // m_txtFormuleTitre
            // 
            this.m_txtFormuleTitre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleTitre.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleTitre.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleTitre.Formule = null;
            this.m_txtFormuleTitre.Location = new System.Drawing.Point(8, 47);
            this.m_txtFormuleTitre.LockEdition = false;
            this.m_txtFormuleTitre.Name = "m_txtFormuleTitre";
            this.m_txtFormuleTitre.Size = new System.Drawing.Size(528, 148);
            this.m_txtFormuleTitre.TabIndex = 2;
            this.m_txtFormuleTitre.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title|20022";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(544, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 359);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Initial Directory|20025";
            // 
            // m_txtFormuleRepertoireInitial
            // 
            this.m_txtFormuleRepertoireInitial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleRepertoireInitial.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleRepertoireInitial.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleRepertoireInitial.Formule = null;
            this.m_txtFormuleRepertoireInitial.Location = new System.Drawing.Point(8, 289);
            this.m_txtFormuleRepertoireInitial.LockEdition = false;
            this.m_txtFormuleRepertoireInitial.Name = "m_txtFormuleRepertoireInitial";
            this.m_txtFormuleRepertoireInitial.Size = new System.Drawing.Size(528, 52);
            this.m_txtFormuleRepertoireInitial.TabIndex = 4;
            this.m_txtFormuleRepertoireInitial.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // CFormEditActionSelectionFichierClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(720, 437);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionSelectionFichierClient";
            this.Text = "Select file on client|20020";
            this.Load += new System.EventHandler(this.CFormEditActionSelectionFichierClient_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);

            // Formule Titre (Message)
            result = analyseur.AnalyseChaine ( m_txtFormuleTitre.Text );
			if ( !result )
				return result;
			ActionSelectionFichierClient.FormuleMessage = (C2iExpression)result.Data;
            // Formule Filtre à appliquer
            result = analyseur.AnalyseChaine(m_txtFormuleFiltre.Text);
            if (!result)
                return result;
            ActionSelectionFichierClient.FormuleFiltre = (C2iExpression)result.Data;
            // Formule répertoire initial
            result = analyseur.AnalyseChaine(m_txtFormuleRepertoireInitial.Text);
            if (!result)
                return result;
            ActionSelectionFichierClient.FormuleRepertoireInitial = (C2iExpression)result.Data;

            ActionSelectionFichierClient.ForSave = m_chkForSave.Checked;
            
            return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
            base.InitChamps ();

			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			m_txtFormuleTitre.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
            m_txtFormuleFiltre.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);

			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = true;

			if ( ActionSelectionFichierClient.FormuleMessage != null )
				m_txtFormuleTitre.Text = ActionSelectionFichierClient.FormuleMessage.GetString();
    
            if (ActionSelectionFichierClient.FormuleFiltre != null)
                m_txtFormuleFiltre.Text = ActionSelectionFichierClient.FormuleFiltre.GetString();

            if (ActionSelectionFichierClient.FormuleRepertoireInitial != null)
                m_txtFormuleRepertoireInitial.Text = ActionSelectionFichierClient.FormuleRepertoireInitial.GetString();


            m_chkForSave.Checked = ActionSelectionFichierClient.ForSave;
            
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
            if ( m_txtFormuleCourante != null )
			    m_wndAideFormule.InsereInTextBox ( m_txtFormuleCourante, nPosCurseur, strCommande );
		}

        private void CFormEditActionSelectionFichierClient_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            // La traduction est déjà appelée dans le Load du Form de base
            // sc2i.win32.common.CWin32Traducteur.Translate(this);
            m_txtFormule_Enter(m_txtFormuleTitre, new EventArgs());

        }

        private void m_txtFormule_Enter(object sender, EventArgs e)
        {
            if (m_txtFormuleCourante != null)
                m_txtFormuleCourante.BackColor = Color.White;
            if (sender is CControleEditeFormule)
            {
                m_txtFormuleCourante = sender as CControleEditeFormule;
                m_txtFormuleCourante.BackColor = Color.LightGreen;
            }
        }

		
		

		




	}
}

