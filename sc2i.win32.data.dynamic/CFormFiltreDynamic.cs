using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.common;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormFiltreDynamic.
	/// </summary>
	public class CFormFiltreDynamic : System.Windows.Forms.Form
	{
		private CFiltreDynamique m_filtreDyn = null;
        private sc2i.formulaire.win32.editor.CPanelFormulaireSurElement m_panelFormulaire;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormFiltreDynamic()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormFiltreDynamic));
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
            // CFormFiltreDynamic
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(464, 270);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelFormulaire);
            this.Controls.Add(this.panel1);
            this.Name = "CFormFiltreDynamic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic filter|158";
            this.Load += new System.EventHandler(this.CFormFiltreDynamic_Load);
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
		public static CFiltreData GetFiltreData ( CFiltreDynamique filtre )
		{
			using (CFormFiltreDynamic form = new CFormFiltreDynamic())
			{
				form.m_filtreDyn = filtre;
				//Vérifie qu'il y a qq chose dans le formulaire !
				bool bNeedDialog = false;
				foreach ( IVariableDynamique variable in filtre.ListeVariables )
					if ( variable.IsChoixUtilisateur() )
					{
						bNeedDialog = true;
						break;
					}
			
				if ( !bNeedDialog || form.ShowDialog() == DialogResult.OK )
				{
					CResultAErreur result = CResultAErreur.True;
					result = filtre.GetFiltreData();
					if ( !result )
					{
						CFormAlerte.Afficher ( result);
					}
					else
						return ( CFiltreData ) result.Data;
				}
			}
			return null;
		}

        /// ////////////////////////////////////////////////////////////////////
        ///Affecte les valeurs du formulaire au filtre
        public static bool SetValeursFiltre(CFiltreDynamique filtre)
        {
            using (CFormFiltreDynamic form = new CFormFiltreDynamic())
            {
                form.m_filtreDyn = filtre;
                if ( form.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// ////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CFormFiltreDynamic_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_panelFormulaire.InitPanel(m_filtreDyn.FormulaireEdition, m_filtreDyn);
			//Ajuste la taille au formulaire
			Size szEcart = new Size ( 
				Width - m_panelFormulaire.Width,
				Height - m_panelFormulaire.Height );
			this.Size = new Size ( 
				m_filtreDyn.FormulaireEdition.Size.Width + szEcart.Width,
				m_filtreDyn.FormulaireEdition.Size.Height + szEcart.Height );
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
