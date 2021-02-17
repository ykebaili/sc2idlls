using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de InputBox.
	/// </summary>
	public class InputBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox m_textBox;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InputBox()
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
            this.m_textBox = new System.Windows.Forms.TextBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_textBox
            // 
            this.m_textBox.Location = new System.Drawing.Point(8, 8);
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(280, 20);
            this.m_textBox.TabIndex = 0;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(58, 40);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(154, 40);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_btnAnnuler.TabIndex = 2;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // InputBox
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(292, 71);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		public static bool GetString ( ref string strChaine, string strTitre )
		{
			InputBox box = new InputBox();
			box.Text = strTitre;
			box.m_textBox.Text = strChaine;
			bool bResult = box.ShowDialog() == DialogResult.OK;
			if ( bResult )
				strChaine = box.m_textBox.Text;
			box.Dispose();
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

        private void InputBox_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            CWin32Traducteur.Translate(this);

        }

	}
}
