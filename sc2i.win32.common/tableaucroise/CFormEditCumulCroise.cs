using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormEditCumulCroise.
	/// </summary>
	public class CFormEditCumulCroise : System.Windows.Forms.Form
	{
		private CCumulCroise m_cumul;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblChampSource;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtPrefix;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbOperation;
		private System.Windows.Forms.CheckBox m_chkHorsPivot;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditCumulCroise()
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtPrefix = new sc2i.win32.common.C2iTextBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_cmbOperation = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_chkHorsPivot = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source field|132";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblChampSource
            // 
            this.m_lblChampSource.BackColor = System.Drawing.Color.White;
            this.m_lblChampSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblChampSource.Location = new System.Drawing.Point(105, 8);
            this.m_lblChampSource.Name = "m_lblChampSource";
            this.m_lblChampSource.Size = new System.Drawing.Size(329, 23);
            this.m_lblChampSource.TabIndex = 1;
            this.m_lblChampSource.Text = "label2";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Operation|133";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Field prefix|134";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtPrefix
            // 
            this.m_txtPrefix.EmptyText = "";
            this.m_txtPrefix.Location = new System.Drawing.Point(105, 72);
            this.m_txtPrefix.LockEdition = false;
            this.m_txtPrefix.Name = "m_txtPrefix";
            this.m_txtPrefix.Size = new System.Drawing.Size(184, 20);
            this.m_txtPrefix.TabIndex = 1;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(142, 137);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(238, 137);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 4;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_cmbOperation
            // 
            this.m_cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOperation.IsLink = false;
            this.m_cmbOperation.ListDonnees = null;
            this.m_cmbOperation.Location = new System.Drawing.Point(105, 40);
            this.m_cmbOperation.LockEdition = false;
            this.m_cmbOperation.Name = "m_cmbOperation";
            this.m_cmbOperation.NullAutorise = false;
            this.m_cmbOperation.ProprieteAffichee = null;
            this.m_cmbOperation.Size = new System.Drawing.Size(184, 21);
            this.m_cmbOperation.TabIndex = 0;
            this.m_cmbOperation.TextNull = "(empty)";
            this.m_cmbOperation.Tri = true;
            // 
            // m_chkHorsPivot
            // 
            this.m_chkHorsPivot.Location = new System.Drawing.Point(105, 97);
            this.m_chkHorsPivot.Name = "m_chkHorsPivot";
            this.m_chkHorsPivot.Size = new System.Drawing.Size(209, 24);
            this.m_chkHorsPivot.TabIndex = 2;
            this.m_chkHorsPivot.Text = "Cumulate out of pivot columns|135";
            // 
            // CFormEditCumulCroise
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(444, 176);
            this.ControlBox = false;
            this.Controls.Add(this.m_chkHorsPivot);
            this.Controls.Add(this.m_cmbOperation);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_txtPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblChampSource);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormEditCumulCroise";
            this.Text = "Cumulated field|131";
            this.Load += new System.EventHandler(this.CFormEditCumulCroise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public static bool EditeCumul ( CCumulCroise cumul )
		{
			CFormEditCumulCroise form = new CFormEditCumulCroise();
			form.m_cumul = cumul;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		private void CFormEditCumulCroise_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_lblChampSource.Text = m_cumul.NomChamp;
			m_txtPrefix.Text = m_cumul.PrefixFinal;
			m_cmbOperation.ValueMember = "Valeur";
			m_cmbOperation.DisplayMember = "Libelle";
            m_cmbOperation.Fill(CUtilSurEnum.GetEnumsALibelle(typeof(CEnumTypeCumulCroise)),
                "Libelle", false);
            m_cmbOperation.SelectedValue = new CEnumTypeCumulCroise(m_cumul.TypeCumul);
			m_chkHorsPivot.Checked = m_cumul.HorsPivot;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_cmbOperation.SelectedValue == null )
			{
				CFormAlerte.Afficher(I.T("Select an operation|30019"), EFormAlerteType.Exclamation);
				return;
			}
			m_cumul.PrefixFinal = m_txtPrefix.Text;
            m_cumul.TypeCumul = ((CEnumTypeCumulCroise)m_cmbOperation.SelectedValue).Code;
			m_cumul.HorsPivot = m_chkHorsPivot.Checked;
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
