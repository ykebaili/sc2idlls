using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormEditColonnePivot.
	/// </summary>
	public class CFormEditColonnePivot : System.Windows.Forms.Form
	{
		private CColonneePivot m_colonne;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblChampSource;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtPrefix;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iTextBox m_txtValeursSystematiques;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditColonnePivot()
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblChampSource = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtValeursSystematiques = new sc2i.win32.common.C2iTextBox();
            this.m_txtPrefix = new sc2i.win32.common.C2iTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source fields|20010";
            // 
            // m_lblChampSource
            // 
            this.m_lblChampSource.BackColor = System.Drawing.Color.White;
            this.m_lblChampSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblChampSource.Location = new System.Drawing.Point(144, 8);
            this.m_lblChampSource.Name = "m_lblChampSource";
            this.m_lblChampSource.Size = new System.Drawing.Size(352, 23);
            this.m_lblChampSource.TabIndex = 1;
            this.m_lblChampSource.Text = "label2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Field prefix|20011";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(136, 272);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(232, 272);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 32);
            this.label2.TabIndex = 8;
            this.label2.Text = "Systematical values|20012";
            // 
            // m_txtValeursSystematiques
            // 
            this.m_txtValeursSystematiques.AcceptsReturn = true;
            this.m_txtValeursSystematiques.Location = new System.Drawing.Point(144, 72);
            this.m_txtValeursSystematiques.LockEdition = false;
            this.m_txtValeursSystematiques.Multiline = true;
            this.m_txtValeursSystematiques.Name = "m_txtValeursSystematiques";
            this.m_txtValeursSystematiques.Size = new System.Drawing.Size(344, 192);
            this.m_txtValeursSystematiques.TabIndex = 9;
            // 
            // m_txtPrefix
            // 
            this.m_txtPrefix.Location = new System.Drawing.Point(144, 40);
            this.m_txtPrefix.LockEdition = false;
            this.m_txtPrefix.Name = "m_txtPrefix";
            this.m_txtPrefix.Size = new System.Drawing.Size(100, 20);
            this.m_txtPrefix.TabIndex = 5;
            // 
            // CFormEditColonnePivot
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(516, 311);
            this.ControlBox = false;
            this.Controls.Add(this.m_txtValeursSystematiques);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_txtPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lblChampSource);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormEditColonnePivot";
            this.Text = "Pivot column|107";
            this.Load += new System.EventHandler(this.CFormEditColonnePivot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public static bool EditPivot ( CColonneePivot colonne )
		{
			CFormEditColonnePivot form = new CFormEditColonnePivot();
			form.m_colonne = colonne;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		private void CFormEditColonnePivot_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

            m_lblChampSource.Text = m_colonne.NomChamp;
			m_txtPrefix.Text = m_colonne.Prefixe;
			m_txtValeursSystematiques.Text = "";
			foreach ( string strValeur in m_colonne.ValeursSystematiques )
				m_txtValeursSystematiques.Text += strValeur+"\r\n";
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			m_colonne.Prefixe = m_txtPrefix.Text;
			string[] strValeurs = m_txtValeursSystematiques.Text.Split('\n');
			ArrayList lstValeurs = new ArrayList();
			string strCarAuto = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
			foreach ( string strValeur in strValeurs )
			{
				string strNewVal = "";
				foreach ( char car in strValeur )
					if ( strCarAuto.IndexOf ( car ) >= 0 )
						strNewVal += car;
				if ( strNewVal.Trim() != "" )
					lstValeurs.Add ( strNewVal );
			}
			m_colonne.ValeursSystematiques = (string[])lstValeurs.ToArray(typeof(string));
				
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}





		
	}
}
