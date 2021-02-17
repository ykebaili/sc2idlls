using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.data;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionAssocierComportement : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private string m_lastStringFormuleCombo = "";
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.CComboboxAutoFilled m_comboComportement;
        private SplitContainer m_splitContainer;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionAssocierComportement()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionAssocierComportement), typeof(CFormEditActionAssocierComportement));
		}


		public CActionAssocierComportement ActionAssocier
		{
			get
			{
				return (CActionAssocierComportement)ObjetEdite;
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboComportement = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
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
            this.panel2.Size = new System.Drawing.Size(554, 285);
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
            this.m_txtFormule.Location = new System.Drawing.Point(8, 25);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(335, 249);
            this.m_txtFormule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Associated element|101";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(192, 281);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(8, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Behavior |102";
            // 
            // m_comboComportement
            // 
            this.m_comboComportement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_comboComportement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboComportement.IsLink = false;
            this.m_comboComportement.ListDonnees = null;
            this.m_comboComportement.Location = new System.Drawing.Point(96, 292);
            this.m_comboComportement.LockEdition = false;
            this.m_comboComportement.Name = "m_comboComportement";
            this.m_comboComportement.NullAutorise = true;
            this.m_comboComportement.ProprieteAffichee = null;
            this.m_comboComportement.Size = new System.Drawing.Size(423, 21);
            this.m_comboComportement.TabIndex = 4;
            this.m_comboComportement.TextNull = I.T("(none)|30033");
            this.m_comboComportement.Tri = true;
            this.m_comboComportement.Enter += new System.EventHandler(this.m_comboComportement_Enter);
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
            this.m_splitContainer.Panel1.Controls.Add(this.m_txtFormule);
            this.m_splitContainer.Panel1.Controls.Add(this.label1);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(554, 285);
            this.m_splitContainer.SplitterDistance = 354;
            this.m_splitContainer.TabIndex = 3;
            // 
            // CFormEditActionAssocierComportement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(554, 371);
            this.Controls.Add(this.m_comboComportement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionAssocierComportement";
            this.Text = "Associate a behavior|100";
            this.Load += new System.EventHandler(this.CFormEditActionAssocierComportement_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_comboComportement, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
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
			ActionAssocier.FormuleElementAAssocier = (C2iExpression)result.Data;

            if (m_comboComportement.SelectedValue is CComportementGenerique)
                ActionAssocier.DbKeyComportement = ((CComportementGenerique)m_comboComportement.SelectedValue).DbKey;
            else
                ActionAssocier.DbKeyComportement = null;
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			if ( ActionAssocier.FormuleElementAAssocier != null )
			{
				m_txtFormule.Text = ActionAssocier.FormuleElementAAssocier.GetString();
			}
			InitComboComportement();
		}

		/// //////////////////////////////////////////
		private void InitComboComportement()
		{
			if ( m_lastStringFormuleCombo == m_txtFormule.Text )
				return;
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess) );
			CResultAErreur result = new CAnalyseurSyntaxiqueExpression(contexte).AnalyseChaine(m_txtFormule.Text );
			if ( !result )
				m_comboComportement.ListDonnees = new ArrayList();
			else
			{
				CListeObjetsDonnees liste = new CListeObjetsDonnees( ObjetEdite.Process.ContexteDonnee,
					typeof(CComportementGenerique ) );
				liste.Filtre = new CFiltreData ( 
					CComportementGenerique.c_champTypeElementCible+"=@1",
					((C2iExpression)result.Data).TypeDonnee.TypeDotNetNatif.ToString());
				m_comboComportement.ListDonnees = liste;
				m_comboComportement.ProprieteAffichee = "Libelle";
                CComportementGenerique cpt = new CComportementGenerique( CSc2iWin32DataClient.ContexteCourant );
                if ( cpt.ReadIfExists(ActionAssocier.DbKeyComportement ) )
                    m_comboComportement.SelectedValue = cpt;
		}
			m_lastStringFormuleCombo = m_txtFormule.Text;
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}



		private void m_comboComportement_Enter(object sender, System.EventArgs e)
		{
			InitComboComportement();
		}

        private void CFormEditActionAssocierComportement_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }
		
		

		




	}
}

