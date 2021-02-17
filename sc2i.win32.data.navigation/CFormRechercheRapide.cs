using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CFormRechercheRapide.
	/// </summary>
	public class CFormRechercheRapide : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox m_txtFiltre;
		private System.Windows.Forms.Panel panel1;
        protected sc2i.win32.common.CExtStyle m_ExtStyle1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormRechercheRapide()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtFiltre = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.BackColor = System.Drawing.Color.White;
            this.m_btnOk.Location = new System.Drawing.Point(88, 40);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(72, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.BackColor = System.Drawing.Color.White;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(168, 40);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(72, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 2;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter on|102";
            // 
            // m_txtFiltre
            // 
            this.m_txtFiltre.Location = new System.Drawing.Point(83, 8);
            this.m_txtFiltre.Name = "m_txtFiltre";
            this.m_txtFiltre.Size = new System.Drawing.Size(224, 20);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFiltre.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_txtFiltre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 80);
            this.m_ExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4;
            // 
            // CFormRechercheRapide
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(312, 80);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CFormRechercheRapide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Filter the records|30007 ";
            this.Load += new System.EventHandler(this.CFormRechercheRapide_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public static string GetTexteFiltre ( Point positionScreen )
		{
			CFormRechercheRapide form = new CFormRechercheRapide();
			form.Location = positionScreen;
			string strText = null;
			if ( form.ShowDialog()==DialogResult.OK )
			{
				strText = form.m_txtFiltre.Text;
			}
			form.Dispose();
			return strText;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

        private void CFormRechercheRapide_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

	}
}
