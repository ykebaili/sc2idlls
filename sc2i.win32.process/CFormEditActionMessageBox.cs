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
	public class CFormEditActionMessageBox : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iComboBox m_cmbTypeBoite;
        private CheckBox m_chkAutoClose;
        private Panel m_panelAutoClose;
        private Label label3;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtSecondesMax;
        private Label label4;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionMessageBox()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionMessageBox), typeof(CFormEditActionMessageBox));
		}


		public CActionMessageBox ActionMessageBox
		{
			get
			{
				return (CActionMessageBox)ObjetEdite;
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
            this.m_cmbTypeBoite = new sc2i.win32.common.C2iComboBox();
            this.m_chkAutoClose = new System.Windows.Forms.CheckBox();
            this.m_panelAutoClose = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtSecondesMax = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.m_panelAutoClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_panelAutoClose);
            this.panel2.Controls.Add(this.m_chkAutoClose);
            this.panel2.Controls.Add(this.m_txtFormule);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 248);
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
            this.m_txtFormule.Size = new System.Drawing.Size(368, 194);
            this.m_txtFormule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message|142";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(384, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 248);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Buttons|141";
            // 
            // m_cmbTypeBoite
            // 
            this.m_cmbTypeBoite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeBoite.IsLink = false;
            this.m_cmbTypeBoite.Location = new System.Drawing.Point(88, 8);
            this.m_cmbTypeBoite.LockEdition = false;
            this.m_cmbTypeBoite.Name = "m_cmbTypeBoite";
            this.m_cmbTypeBoite.Size = new System.Drawing.Size(192, 21);
            this.m_cmbTypeBoite.TabIndex = 4;
            // 
            // m_chkAutoClose
            // 
            this.m_chkAutoClose.Location = new System.Drawing.Point(8, 223);
            this.m_chkAutoClose.Name = "m_chkAutoClose";
            this.m_chkAutoClose.Size = new System.Drawing.Size(120, 24);
            this.m_chkAutoClose.TabIndex = 3;
            this.m_chkAutoClose.Text = "Auto close|20013";
            this.m_chkAutoClose.UseVisualStyleBackColor = true;
            this.m_chkAutoClose.CheckedChanged += new System.EventHandler(this.m_chkAutoClose_CheckedChanged);
            // 
            // m_panelAutoClose
            // 
            this.m_panelAutoClose.Controls.Add(this.label4);
            this.m_panelAutoClose.Controls.Add(this.m_txtSecondesMax);
            this.m_panelAutoClose.Controls.Add(this.label3);
            this.m_panelAutoClose.Location = new System.Drawing.Point(134, 223);
            this.m_panelAutoClose.Name = "m_panelAutoClose";
            this.m_panelAutoClose.Size = new System.Drawing.Size(242, 22);
            this.m_panelAutoClose.TabIndex = 4;
            this.m_panelAutoClose.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "After|20014";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_txtSecondesMax
            // 
            this.m_txtSecondesMax.Arrondi = 0;
            this.m_txtSecondesMax.DecimalAutorise = false;
            this.m_txtSecondesMax.IntValue = 0;
            this.m_txtSecondesMax.Location = new System.Drawing.Point(77, 0);
            this.m_txtSecondesMax.LockEdition = false;
            this.m_txtSecondesMax.Name = "m_txtSecondesMax";
            this.m_txtSecondesMax.NullAutorise = false;
            this.m_txtSecondesMax.SelectAllOnEnter = true;
            this.m_txtSecondesMax.Size = new System.Drawing.Size(45, 20);
            this.m_txtSecondesMax.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(125, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Sec|20015";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CFormEditActionMessageBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 326);
            this.Controls.Add(this.m_cmbTypeBoite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionMessageBox";
            this.Text = "Message dialog box|140";
            this.Load += new System.EventHandler(this.CFormEditActionMessageBox_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_cmbTypeBoite, 0);
            this.panel2.ResumeLayout(false);
            this.m_panelAutoClose.ResumeLayout(false);
            this.m_panelAutoClose.PerformLayout();
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
			ActionMessageBox.FormuleMessage = (C2iExpression)result.Data;
			ActionMessageBox.TypeBox = (CActionMessageBox.TypeMessageBox)m_cmbTypeBoite.SelectedValue;
            if (m_chkAutoClose.Checked && m_txtSecondesMax.IntValue != null)
                ActionMessageBox.SecondesMaxiAffichage = m_txtSecondesMax.IntValue.Value;
            else
                ActionMessageBox.SecondesMaxiAffichage = 0;
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			DataTable table = new DataTable ( );
			table.Columns.Add ( "TEXT", typeof(string));
			table.Columns.Add ( "VAL", typeof(CActionMessageBox.TypeMessageBox) );
			DataRow row = table.NewRow();
			row["TEXT"] = I.T("Ok|10");
			row["VAL"] = CActionMessageBox.TypeMessageBox.OK;
			table.Rows.Add ( row );

			row = table.NewRow();
			row["TEXT"] = I.T("Ok / Cancel|30015");
			row["VAL"] = CActionMessageBox.TypeMessageBox.OKAnnuler;
			table.Rows.Add ( row );

			row = table.NewRow();
			row["TEXT"] = I.T("Yes / No|30016");
			row["VAL"] = CActionMessageBox.TypeMessageBox.OuiNon;
			table.Rows.Add ( row );

			

			m_cmbTypeBoite.DataSource = table;
			m_cmbTypeBoite.ValueMember = "VAL";
			m_cmbTypeBoite.DisplayMember = "TEXT";

			m_cmbTypeBoite.SelectedValue = ActionMessageBox.TypeBox;

			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = true;

			if ( ActionMessageBox.FormuleMessage != null )
			{
				m_txtFormule.Text = ActionMessageBox.FormuleMessage.GetString();
			}
            if (ActionMessageBox.SecondesMaxiAffichage > 0)
            {
                m_chkAutoClose.Checked = true;
                m_panelAutoClose.Visible = true;
                m_txtSecondesMax.IntValue = ActionMessageBox.SecondesMaxiAffichage;
            }
            else
            {
                m_chkAutoClose.Checked = false;
                m_panelAutoClose.Visible = false;
                m_txtSecondesMax.IntValue = null;
            }
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

        private void CFormEditActionMessageBox_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

        private void m_chkAutoClose_CheckedChanged(object sender, EventArgs e)
        {
            m_panelAutoClose.Visible = m_chkAutoClose.Checked;
        }

		
		

		




	}
}

