using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormGereColonnesListe.
	/// </summary>
	internal class CFormGereColonnesListe : System.Windows.Forms.Form
	{
		private GLColumnCollection m_listeColonnes = null;
		private System.Windows.Forms.Button m_btnHaut;
		private System.Windows.Forms.Button m_btnBas;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView m_wndListe;
		private System.Windows.Forms.Button m_btnAnnuler;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormGereColonnesListe()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormGereColonnesListe));
            this.m_wndListe = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_btnHaut = new System.Windows.Forms.Button();
            this.m_btnBas = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_wndListe
            // 
            this.m_wndListe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListe.FullRowSelect = true;
            this.m_wndListe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListe.HideSelection = false;
            this.m_wndListe.Location = new System.Drawing.Point(8, 0);
            this.m_wndListe.MultiSelect = false;
            this.m_wndListe.Name = "m_wndListe";
            this.m_wndListe.Size = new System.Drawing.Size(224, 224);
            this.m_wndListe.TabIndex = 0;
            this.m_wndListe.UseCompatibleStateImageBehavior = false;
            this.m_wndListe.View = System.Windows.Forms.View.Details;
            this.m_wndListe.SelectedIndexChanged += new System.EventHandler(this.m_wndListe_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 207;
            // 
            // m_btnHaut
            // 
            this.m_btnHaut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnHaut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHaut.Image")));
            this.m_btnHaut.Location = new System.Drawing.Point(152, 232);
            this.m_btnHaut.Name = "m_btnHaut";
            this.m_btnHaut.Size = new System.Drawing.Size(40, 32);
            this.m_btnHaut.TabIndex = 1;
            this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
            // 
            // m_btnBas
            // 
            this.m_btnBas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnBas.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBas.Image")));
            this.m_btnBas.Location = new System.Drawing.Point(192, 232);
            this.m_btnBas.Name = "m_btnBas";
            this.m_btnBas.Size = new System.Drawing.Size(40, 32);
            this.m_btnBas.TabIndex = 2;
            this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(8, 232);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(64, 32);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(80, 232);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(64, 32);
            this.m_btnAnnuler.TabIndex = 4;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // CFormGereColonnesListe
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.ControlBox = false;
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnBas);
            this.Controls.Add(this.m_btnHaut);
            this.Controls.Add(this.m_wndListe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormGereColonnesListe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Columns management|10012";
            this.Load += new System.EventHandler(this.CFormGereColonnesListe_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// /////////////////////////////////
		private void CFormGereColonnesListe_Load(object sender, System.EventArgs e)
		{
			foreach ( GLColumn col in m_listeColonnes )
			{
				if ( !col.IsCheckColumn )
				{
					ListViewItem item = new ListViewItem ( );
					item.Text = col.Text;
					m_wndListe.Items.Add ( item );
					item.Tag = col;
				}
			}

            CWin32Traducteur.Translate(this);
		}


		/// /////////////////////////////////
		public static bool ReorderColonnes ( GLColumnCollection colonnes )
		{
			CFormGereColonnesListe form = new CFormGereColonnesListe();
			form.m_listeColonnes = colonnes;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		/// /////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			GLColumn colCheck = null;
			if ( m_listeColonnes.Count > 0 && m_listeColonnes[0].IsCheckColumn )
				colCheck = m_listeColonnes[0];
			m_listeColonnes.Clear();
			if ( colCheck != null )
				m_listeColonnes.Add ( colCheck );
			foreach ( ListViewItem item in m_wndListe.Items )
				m_listeColonnes.Add ( (GLColumn)item.Tag );
			DialogResult = DialogResult.OK;
			Close();
		}


		/// /////////////////////////////////
		private void m_btnHaut_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListe.SelectedItems.Count != 1 )
				return;
			int nIndex = m_wndListe.SelectedIndices[0];
			if ( nIndex < 1 )
				return;
			ListViewItem item = m_wndListe.Items[nIndex];
			m_wndListe.Items.RemoveAt ( nIndex );
			m_wndListe.Items.Insert ( nIndex-1, item );
		}

		/// /////////////////////////////////
		private void m_btnBas_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListe.SelectedItems.Count != 1 )
				return;
			int nIndex = m_wndListe.SelectedIndices[0];
			if ( nIndex >= m_wndListe.Items.Count - 1 )
				return;
			ListViewItem item = m_wndListe.Items[nIndex];
			m_wndListe.Items.RemoveAt ( nIndex );
			m_wndListe.Items.Insert ( nIndex+1, item );
		}

        private void m_wndListe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

	}
}
