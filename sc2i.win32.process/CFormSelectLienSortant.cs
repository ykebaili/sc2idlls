using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.process;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CFormSelectLienSortant.
	/// </summary>
	public class CFormSelectLienSortant : System.Windows.Forms.Form
	{
		private CLienAction m_lienSelectionne = null;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.common.CListLink m_lstLiens;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormSelectLienSortant()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectLienSortant));
            this.m_lstLiens = new sc2i.win32.common.CListLink();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lstLiens
            // 
            this.m_lstLiens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lstLiens.ColorActive = System.Drawing.Color.Red;
            this.m_lstLiens.ColorDesactive = System.Drawing.Color.Blue;
            this.m_lstLiens.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline);
            this.m_lstLiens.ForeColor = System.Drawing.Color.Blue;
            this.m_lstLiens.FullRowSelect = true;
            this.m_lstLiens.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lstLiens.Location = new System.Drawing.Point(0, 0);
            this.m_lstLiens.MultiSelect = false;
            this.m_lstLiens.Name = "m_lstLiens";
            this.m_lstLiens.Size = new System.Drawing.Size(272, 224);
            this.m_lstLiens.TabIndex = 0;
            this.m_lstLiens.UseCompatibleStateImageBehavior = false;
            this.m_lstLiens.View = System.Windows.Forms.View.Details;
            this.m_lstLiens.ItemClick += new sc2i.win32.common.ListLinkItemClickEventHandler(this.m_lstLiens_ItemClick);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(112, 224);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 4;
            // 
            // CFormSelectLienSortant
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(272, 264);
            this.ControlBox = false;
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_lstLiens);
            this.Name = "CFormSelectLienSortant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select the link|169";
            this.Load += new System.EventHandler(this.CFormSelectLienSortant_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_lstLiens_ItemClick(System.Windows.Forms.ListViewItem item)
		{
			m_lienSelectionne = (CLienAction)item.Tag;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void Init ( CLienAction[] liens )
		{
			foreach ( CLienAction lien in liens )
			{
				ListViewItem item = new ListViewItem();
				item.Text = lien.Libelle;
				item.Tag = lien;
				m_lstLiens.Items.Add ( item );
			}
		}

		public static CLienAction SelectLien ( CLienAction[] liens, Point ptSourisAbsolu )
		{
			CFormSelectLienSortant form = new CFormSelectLienSortant();
			form.Location = ptSourisAbsolu;
			if ( form.Right > Screen.PrimaryScreen.WorkingArea.Width )
				form.Left = Screen.PrimaryScreen.WorkingArea.Width-form.Width;
			if ( form.Bottom > Screen.PrimaryScreen.WorkingArea.Height )
				form.Top = Screen.PrimaryScreen.WorkingArea.Height-form.Height;
			CLienAction lien = null;
			form.Init(liens);
			if ( form.ShowDialog() == DialogResult.OK )
				lien = form.m_lienSelectionne;
			form.Dispose();
			return lien;
		}

        private void CFormSelectLienSortant_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }


	}
}
