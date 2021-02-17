using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditFiltreDynamique.
	/// </summary>
	public class CFormEditFiltreDynamique : System.Windows.Forms.Form
	{
		private sc2i.win32.data.dynamic.CPanelEditFiltreDynamique m_panelFiltre;
		private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private IContainer components;

		public CFormEditFiltreDynamique()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditFiltreDynamique));
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.SuspendLayout();
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeFiltreExpression = false;
            this.m_panelFiltre.ModeSansType = false;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(608, 403);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltre.TabIndex = 0;
            this.m_panelFiltre.Load += new System.EventHandler(this.m_panelFiltre_Load);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(319, 376);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(255, 376);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditFiltreDynamique
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(614, 414);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_panelFiltre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormEditFiltreDynamique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Filter|139";
            this.Load += new System.EventHandler(this.CFormEditFiltreDynamique_Load);
            this.ResumeLayout(false);

		}
		#endregion

		public static bool EditeFiltre ( CFiltreDynamique filtre, bool bSansType, bool bSansVariables, CDefinitionProprieteDynamique definitionRacineDeChamps )
		{
			CFormEditFiltreDynamique form = new CFormEditFiltreDynamique();
			form.m_panelFiltre.DefinitionRacineDeChampsFiltres = definitionRacineDeChamps;
			if ( bSansVariables )
				form.m_panelFiltre.InitSansVariables ( filtre );
			else
				form.m_panelFiltre.Init ( filtre );
			form.m_panelFiltre.ModeSansType = bSansType;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

        private void CFormEditFiltreDynamique_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
  
        }

		private void m_panelFiltre_Load(object sender, EventArgs e)
		{

		}



	}
}
