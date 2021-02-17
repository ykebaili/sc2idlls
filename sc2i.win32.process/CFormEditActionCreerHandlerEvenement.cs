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
	public class CFormEditActionCreerHandlerEvenement : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private sc2i.win32.process.CPanelEditParametreDeclencheur m_panelParametreDeclenchement;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariableElement;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox m_txtLibelle;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iComboBox m_cmbGestionCode;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionCreerHandlerEvenement()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionCreerHandlerEvenement), typeof(CFormEditActionCreerHandlerEvenement));
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
            this.m_cmbGestionCode = new sc2i.win32.common.C2iComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtLibelle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_comboVariableElement = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelParametreDeclenchement = new sc2i.win32.process.CPanelEditParametreDeclencheur();
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
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbGestionCode);
            this.c2iPanelOmbre1.Controls.Add(this.label3);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtLibelle);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.m_comboVariableElement);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.m_panelParametreDeclenchement);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(760, 352);
            this.c2iPanelOmbre1.TabIndex = 4003;
            this.c2iPanelOmbre1.TabStop = true;
            // 
            // m_cmbGestionCode
            // 
            this.m_cmbGestionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbGestionCode.IsLink = false;
            this.m_cmbGestionCode.Location = new System.Drawing.Point(456, 32);
            this.m_cmbGestionCode.LockEdition = false;
            this.m_cmbGestionCode.Name = "m_cmbGestionCode";
            this.m_cmbGestionCode.Size = new System.Drawing.Size(208, 21);
            this.m_cmbGestionCode.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(360, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Code management|30009";
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Location = new System.Drawing.Point(136, 8);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(312, 20);
            this.m_txtLibelle.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Label|200";
            // 
            // m_comboVariableElement
            // 
            this.m_comboVariableElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariableElement.IsLink = false;
            this.m_comboVariableElement.ListDonnees = null;
            this.m_comboVariableElement.Location = new System.Drawing.Point(136, 32);
            this.m_comboVariableElement.LockEdition = false;
            this.m_comboVariableElement.Name = "m_comboVariableElement";
            this.m_comboVariableElement.NullAutorise = false;
            this.m_comboVariableElement.ProprieteAffichee = null;
            this.m_comboVariableElement.Size = new System.Drawing.Size(224, 21);
            this.m_comboVariableElement.TabIndex = 5;
            this.m_comboVariableElement.TextNull = "(to affect)";
            this.m_comboVariableElement.Tri = true;
            this.m_comboVariableElement.SelectedIndexChanged += new System.EventHandler(this.m_comboVariableElement_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Attach the event to|30008";
            // 
            // m_panelParametreDeclenchement
            // 
            this.m_panelParametreDeclenchement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelParametreDeclenchement.AutoriseSurCreation = false;
            this.m_panelParametreDeclenchement.AutoriseSurDate = true;
            this.m_panelParametreDeclenchement.AutoriseSurManuel = true;
            this.m_panelParametreDeclenchement.AutoriseSurModification = true;
            this.m_panelParametreDeclenchement.AutoriseSurSuppression = false;
            this.m_panelParametreDeclenchement.BackColor = System.Drawing.Color.White;
            this.m_panelParametreDeclenchement.Location = new System.Drawing.Point(0, 56);
            this.m_panelParametreDeclenchement.LockEdition = false;
            this.m_panelParametreDeclenchement.Name = "m_panelParametreDeclenchement";
            this.m_panelParametreDeclenchement.Size = new System.Drawing.Size(744, 280);
            this.m_panelParametreDeclenchement.TabIndex = 3;
            this.m_panelParametreDeclenchement.TypeCible = typeof(string);
            // 
            // CFormEditActionCreerHandlerEvenement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(760, 397);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormEditActionCreerHandlerEvenement";
            this.Text = "Event creation|30010";
            this.Controls.SetChildIndex(this.c2iPanelOmbre1, 0);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		/// ////////////////////////////////////////////////////
		private CActionCreerHandlerEvenement ActionCreerHandler
		{
			get
			{
				return (CActionCreerHandlerEvenement)ObjetEdite;
			}
		}

		/// ////////////////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_txtLibelle.Text = ActionCreerHandler.LibelleEvenement;
			m_panelParametreDeclenchement.Init ( ActionCreerHandler.ParametreDeclencheur );
			FillListeVariablesElement();
			m_comboVariableElement.SelectedValue = ActionCreerHandler.VariableElement;
			m_cmbGestionCode.DataSource = CUtilSurEnum.GetCouplesFromEnum(typeof(CActionCreerHandlerEvenement.TypeGestionCode) );
			foreach ( CUtilSurEnum.CCoupleEnumLibelle couple in (IEnumerable)m_cmbGestionCode.DataSource )
				if ( couple.Valeur == (int)ActionCreerHandler.TypeDeGestionDuCodeEvenement )
					m_cmbGestionCode.SelectedItem = couple;
			//m_chkCodeUnique.Checked = ActionCreerHandler.HandlerACodeUnique;
		}

		/// ////////////////////////////////////////////////////
		protected override CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if  (result)
				result =  m_panelParametreDeclenchement.MAJ_Champs();
			if ( !result )
				return result;
			ActionCreerHandler.LibelleEvenement = m_txtLibelle.Text;
			ActionCreerHandler.ParametreDeclencheur = m_panelParametreDeclenchement.ParametreDeclencheur;
			if ( m_comboVariableElement.SelectedValue is CVariableDynamique )
				ActionCreerHandler.VariableElement = (CVariableDynamique)m_comboVariableElement.SelectedValue;
			if ( m_cmbGestionCode.SelectedItem is CUtilSurEnum.CCoupleEnumLibelle )
				ActionCreerHandler.TypeDeGestionDuCodeEvenement = 
					(CActionCreerHandlerEvenement.TypeGestionCode)
					((CUtilSurEnum.CCoupleEnumLibelle)m_cmbGestionCode.SelectedValue).Valeur;
			//ActionCreerHandler.HandlerACodeUnique = m_chkCodeUnique.Checked;
			
			return result;
		}

		/// //////////////////////////////////////////
		protected void FillListeVariablesElement()
		{
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ActionCreerHandler.Process.ListeVariables )
			{
				if ( variable.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) ))
					lstVariables.Add ( variable );
			}
			lstVariables.Sort();
			m_comboVariableElement.ProprieteAffichee = "Nom";
			m_comboVariableElement.ListDonnees = lstVariables;
		}

		/// //////////////////////////////////////////
		private void m_comboVariableElement_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( m_comboVariableElement.SelectedValue is CVariableDynamique )
			{
				m_panelParametreDeclenchement.TypeCible = ((CVariableDynamique)m_comboVariableElement.SelectedValue).TypeDonnee.TypeDotNetNatif;
			}
		}





	}
}

