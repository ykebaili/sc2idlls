using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
/*using sc2i.data;
using sc2i.data.dynamic;*/
using sc2i.win32.common;
using sc2i.formulaire;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Boite de dialogue modale affichant un formulaire et modifiant un élément
	/// </summary>
	public class CFormFormulairePopup : System.Windows.Forms.Form
	{
		private object m_elementEdite = null;
		private C2iWnd m_formulaire = null;
        private sc2i.formulaire.win32.editor.CPanelFormulaireSurElement m_panelFormulaire;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormFormulairePopup()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormFormulairePopup));
            this.m_panelFormulaire = new sc2i.formulaire.win32.editor.CPanelFormulaireSurElement();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelFormulaire
            // 
            this.m_panelFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormulaire.ElementEdite = null;
            this.m_panelFormulaire.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormulaire.LockEdition = false;
            this.m_panelFormulaire.Name = "m_panelFormulaire";
            this.m_panelFormulaire.Size = new System.Drawing.Size(464, 222);
            this.m_panelFormulaire.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 48);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(239, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(185, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormFormulairePopup
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(464, 270);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelFormulaire);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CFormFormulairePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.Load += new System.EventHandler(this.CFormFormulairePopup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Fait sélectionner les valeurs à l'utilisateur
		/// </summary>
		/// <param name="filtre"></param>
		/// <returns></returns>
		public static bool EditeElement( C2iWnd formulaire, object elementEdite, string strTitre )
		{
			using (CFormFormulairePopup form = new CFormFormulairePopup())
			{
				form.m_elementEdite = elementEdite;
				form.m_formulaire = formulaire;
				if ( strTitre != "" )
					form.Text = strTitre;
				if ( form.ShowDialog() == DialogResult.OK )
				{
					return true;
				}
				return false;
			}
		}


        public bool EditeElement(IWin32Window owner , C2iWnd formulaire, object elementEdite, string strTitre)
        {
            m_elementEdite = elementEdite;
            m_formulaire = formulaire;
            if (strTitre != "")
                Text = strTitre;
            if (ShowDialog(owner) == DialogResult.OK)
            {
                return true;
            }
            return false;
            
        }

		/// <summary>
		/// ////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CFormFormulairePopup_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_panelFormulaire.InitPanel(m_formulaire, m_elementEdite);
			
			//Ajuste la taille au formulaire
			Size szEcart = new Size ( 
				Width - m_panelFormulaire.Width,
				Height - m_panelFormulaire.Height );
			this.Size = new Size ( 
				m_formulaire.Size.Width + szEcart.Width,
				m_formulaire.Size.Height + szEcart.Height );
		}

		/// ////////////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = m_panelFormulaire.AffecteValeursToElement();
			if ( !result )
			{
				CFormAlerte.Afficher(result) ;
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		/// ////////////////////////////////////////////////////////////////////
		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		
	}
}
