using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.process;
using sc2i.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionSupprimerHandlerEvenement : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariableElement;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox m_txtCode;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionSupprimerHandlerEvenement()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionSupprimerHandlerEvenement), typeof(CFormEditActionSupprimerHandlerEvenement));
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_comboVariableElement = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 32);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.Size = new System.Drawing.Size(712, 328);
            this.c2iTabControl1.TabIndex = 2;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtCode);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.m_comboVariableElement);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(440, 104);
            this.c2iPanelOmbre1.TabIndex = 4003;
            // 
            // m_txtCode
            // 
            this.m_txtCode.Location = new System.Drawing.Point(152, 8);
            this.m_txtCode.Name = "m_txtCode";
            this.m_txtCode.Size = new System.Drawing.Size(120, 20);
            this.m_txtCode.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Event code|157";
            // 
            // m_comboVariableElement
            // 
            this.m_comboVariableElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariableElement.IsLink = false;
            this.m_comboVariableElement.ListDonnees = null;
            this.m_comboVariableElement.Location = new System.Drawing.Point(152, 32);
            this.m_comboVariableElement.LockEdition = false;
            this.m_comboVariableElement.Name = "m_comboVariableElement";
            this.m_comboVariableElement.NullAutorise = false;
            this.m_comboVariableElement.ProprieteAffichee = null;
            this.m_comboVariableElement.Size = new System.Drawing.Size(208, 21);
            this.m_comboVariableElement.TabIndex = 5;
            this.m_comboVariableElement.TextNull = I.T("(to affect)|30017");
            this.m_comboVariableElement.Tri = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Delete event of|158";
            // 
            // CFormEditActionSupprimerHandlerEvenement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 149);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormEditActionSupprimerHandlerEvenement";
            this.Text = "Delete an event|156";
            this.Load += new System.EventHandler(this.CFormEditActionSupprimerHandlerEvenement_Load);
            this.Controls.SetChildIndex(this.c2iPanelOmbre1, 0);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		/// ////////////////////////////////////////////////////
		private CActionSupprimerHandlerEvenement ActionSupprimerHandler
		{
			get
			{
				return (CActionSupprimerHandlerEvenement)ObjetEdite;
			}
		}

		/// ////////////////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			FillListeVariablesElement();
			m_comboVariableElement.SelectedValue = ActionSupprimerHandler.VariableElement;
			m_txtCode.Text = ActionSupprimerHandler.CodeEvenement;
		}

		/// ////////////////////////////////////////////////////
		protected override CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if ( m_comboVariableElement.SelectedValue is CVariableDynamique )
				ActionSupprimerHandler.VariableElement = (CVariableDynamique)m_comboVariableElement.SelectedValue;
			ActionSupprimerHandler.CodeEvenement = m_txtCode.Text;
			
			return result;
		}

		/// //////////////////////////////////////////
		protected void FillListeVariablesElement()
		{
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ActionSupprimerHandler.Process.ListeVariables )
			{
				if ( variable.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) ))
					lstVariables.Add ( variable );
			}
			lstVariables.Sort();
			m_comboVariableElement.ProprieteAffichee = "Nom";
			m_comboVariableElement.ListDonnees = lstVariables;
		}

        private void CFormEditActionSupprimerHandlerEvenement_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		




	}
}

