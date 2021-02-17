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
	public class CFormEditActionDeclencherEvenementStatiques : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariableElement;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionDeclencherEvenementStatiques()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionDeclencherEvenementStatiques), typeof(CFormEditActionDeclencherEvenementStatiques));
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
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.m_comboVariableElement);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(464, 104);
            this.c2iPanelOmbre1.TabIndex = 4003;
            this.c2iPanelOmbre1.TabStop = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "This action triggers creation events which have not been triggered yet and modifi" +
                "cation events which have undefined initial value and have not been launched.|119" +
                "";
            // 
            // m_comboVariableElement
            // 
            this.m_comboVariableElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariableElement.IsLink = false;
            this.m_comboVariableElement.ListDonnees = null;
            this.m_comboVariableElement.Location = new System.Drawing.Point(195, 8);
            this.m_comboVariableElement.LockEdition = false;
            this.m_comboVariableElement.Name = "m_comboVariableElement";
            this.m_comboVariableElement.NullAutorise = false;
            this.m_comboVariableElement.ProprieteAffichee = null;
            this.m_comboVariableElement.Size = new System.Drawing.Size(224, 21);
            this.m_comboVariableElement.TabIndex = 5;
            this.m_comboVariableElement.TextNull = I.T("(to affect)|30017");
            this.m_comboVariableElement.Tri = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Trigger the event for this element |118";
            // 
            // CFormEditActionDeclencherEvenementStatiques
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(464, 149);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormEditActionDeclencherEvenementStatiques";
            this.Text = "Trigger static events|117";
            this.Load += new System.EventHandler(this.CFormEditActionDeclencherEvenementStatiques_Load);
            this.Controls.SetChildIndex(this.c2iPanelOmbre1, 0);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		/// ////////////////////////////////////////////////////
		private CActionDeclencherEvenementStatiques ActionDeclencher
		{
			get
			{
				return (CActionDeclencherEvenementStatiques)ObjetEdite;
			}
		}

		/// ////////////////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			FillListeVariablesElement();
			m_comboVariableElement.SelectedValue = ActionDeclencher.VariableElement;
		}

		/// ////////////////////////////////////////////////////
		protected override CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if ( !result )
				return result;
			if ( m_comboVariableElement.SelectedValue is CVariableDynamique )
				ActionDeclencher.VariableElement = (CVariableDynamique)m_comboVariableElement.SelectedValue;
			return result;
		}

		/// //////////////////////////////////////////
		protected void FillListeVariablesElement()
		{
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ActionDeclencher.Process.ListeVariables )
			{
				if ( variable.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) ))
					lstVariables.Add ( variable );
			}
			lstVariables.Sort();
			m_comboVariableElement.ProprieteAffichee = "Nom";
			m_comboVariableElement.ListDonnees = lstVariables;
		}

        private void CFormEditActionDeclencherEvenementStatiques_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

	}
}

