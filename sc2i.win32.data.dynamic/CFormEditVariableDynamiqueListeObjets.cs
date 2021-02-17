using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	public class CFormEditVariableDynamiqueListeObjets : System.Windows.Forms.Form
	{
		private CVariableDynamiqueListeObjets m_variable = null;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.data.dynamic.CPanelEditFiltreDynamique m_panelFiltre;
		private sc2i.win32.common.C2iTabControl c2iTabControl2;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Label label2;
        private sc2i.win32.common.CExtStyle cExtStyle1;
		private System.ComponentModel.IContainer components = null;

		public CFormEditVariableDynamiqueListeObjets()
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


		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.c2iTabControl2 = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelFiltre.SuspendLayout();
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
            this.cExtStyle1.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 2;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFiltre.Controls.Add(this.c2iTabControl2);
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 56);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeSansType = false;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(553, 295);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltre.TabIndex = 4;
            // 
            // c2iTabControl2
            // 
            this.c2iTabControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.c2iTabControl2.BoldSelectedPage = true;
            this.c2iTabControl2.ControlBottomOffset = 16;
            this.c2iTabControl2.ControlRightOffset = 16;
            this.c2iTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iTabControl2.IDEPixelArea = false;
            this.c2iTabControl2.Location = new System.Drawing.Point(0, 0);
            this.c2iTabControl2.Name = "c2iTabControl2";
            this.c2iTabControl2.Ombre = true;
            this.c2iTabControl2.PositionTop = true;
            this.c2iTabControl2.Size = new System.Drawing.Size(553, 295);
            this.cExtStyle1.SetStyleBackColor(this.c2iTabControl2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.c2iTabControl2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl2.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(304, 319);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 24;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(176, 319);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 23;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomVariable);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 1);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(553, 56);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 25;
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomVariable.Location = new System.Drawing.Point(131, 8);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(398, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomVariable.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable name|143";
            // 
            // CFormEditVariableDynamiqueListeObjets
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(553, 348);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_panelFiltre);
            this.Name = "CFormEditVariableDynamiqueListeObjets";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Object list variable|144";
            this.Load += new System.EventHandler(this.CFormEditVariableDynamiqueListeObjets_Load);
            this.m_panelFiltre.ResumeLayout(false);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		private void m_panelFiltre_OnChangeTypeElements(object sender, System.Type typeSelectionne)
		{
			m_variable.FiltreDynamique = m_panelFiltre.FiltreDynamique;
		}


		
		/// ////////////////////////////////////////////////////
		protected void Init ( CVariableDynamiqueListeObjets variable, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables )
		{
			m_variable = variable;
			m_variable.FiltreDynamique.ElementAVariablesExterne = elementAVariables;
			m_panelFiltre.Init ( m_variable.FiltreDynamique );
			m_panelFiltre.MasquerFormulaire ( true );
			m_txtNomVariable.Text = m_variable.Nom;
		}

		/// ////////////////////////////////////////////////////
		protected CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;
			m_variable.FiltreDynamique = (CFiltreDynamique)m_panelFiltre.FiltreDynamique.Clone();
			m_variable.Nom = m_txtNomVariable.Text;
			return result;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeVariable ( CVariableDynamiqueListeObjets variable, IElementAVariablesDynamiquesAvecContexteDonnee element )
		{
			CFormEditVariableDynamiqueListeObjets form = new CFormEditVariableDynamiqueListeObjets();
			form.Init ( variable, element);
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		/// //////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_txtNomVariable.Text.Trim() == "" )
			{
				CFormAlerte.Afficher(I.T("Enter a variable name|30022"), EFormAlerteType.Exclamation);
				return;
			}
			CResultAErreur result = MAJ_Champs();
			if ( !result )
			{
				CFormAlerte.Afficher(result.Erreur.ToString() , EFormAlerteType.Erreur);
				return ;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

        private void CFormEditVariableDynamiqueListeObjets_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }





	}
}

