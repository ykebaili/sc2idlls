using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.process;
using sc2i.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionCreerListeObjetsDonnee : sc2i.win32.process.CFormEditActionFonction
	{
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private sc2i.win32.data.dynamic.CPanelEditFiltreDynamique m_panelFiltre;
		private System.Windows.Forms.CheckBox m_chkCompterUniquement;
		private System.Windows.Forms.CheckBox m_chkFiltreDefaut;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionCreerListeObjetsDonnee()
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

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionCreerListeObjetsDonnee), typeof(CFormEditActionCreerListeObjetsDonnee));
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
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_chkFiltreDefaut = new System.Windows.Forms.CheckBox();
            this.m_chkCompterUniquement = new System.Windows.Forms.CheckBox();
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.c2iPanelOmbre1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Size = new System.Drawing.Size(144, 16);
            this.m_lblStockerResIn.Text = "Store the result in|112";
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
            this.c2iTabControl1.TabIndex = 2;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Controls.Add(this.m_chkFiltreDefaut);
            this.c2iPanelOmbre1.Controls.Add(this.m_chkCompterUniquement);
            this.c2iPanelOmbre1.Controls.Add(this.m_panelFiltre);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 32);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(664, 296);
            this.c2iPanelOmbre1.TabIndex = 4003;
            // 
            // m_chkFiltreDefaut
            // 
            this.m_chkFiltreDefaut.Location = new System.Drawing.Point(280, 264);
            this.m_chkFiltreDefaut.Name = "m_chkFiltreDefaut";
            this.m_chkFiltreDefaut.Size = new System.Drawing.Size(213, 16);
            this.m_chkFiltreDefaut.TabIndex = 5;
            this.m_chkFiltreDefaut.Text = "Apply default filter|116";
            // 
            // m_chkCompterUniquement
            // 
            this.m_chkCompterUniquement.Location = new System.Drawing.Point(8, 264);
            this.m_chkCompterUniquement.Name = "m_chkCompterUniquement";
            this.m_chkCompterUniquement.Size = new System.Drawing.Size(312, 16);
            this.m_chkCompterUniquement.TabIndex = 4;
            this.m_chkCompterUniquement.Text = "Only count the elements|115";
            this.m_chkCompterUniquement.CheckedChanged += new System.EventHandler(this.m_chkCompterUniquement_CheckedChanged);
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFiltre.BackColor = System.Drawing.Color.White;
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeFiltreExpression = false;
            this.m_panelFiltre.ModeSansType = false;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(648, 280);
            this.m_panelFiltre.TabIndex = 3;
            this.m_panelFiltre.OnChangeTypeElements += new sc2i.win32.data.dynamic.ChangeTypeElementsEventHandler(this.m_panelFiltre_OnChangeTypeElements);
            // 
            // CFormEditActionCreerListeObjetsDonnee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 374);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormEditActionCreerListeObjetsDonnee";
            this.Text = "Creates an Object List|114";
            this.Load += new System.EventHandler(this.CFormEditActionCreerListeObjetsDonnee_Load);
            this.Controls.SetChildIndex(this.c2iPanelOmbre1, 0);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		private void m_panelFiltre_OnChangeTypeElements(object sender, System.Type typeSelectionne)
		{
			ActionCreerListe.Filtre = m_panelFiltre.FiltreDynamique;
			OnChangeTypeRetour();	
		}


		/// ////////////////////////////////////////////////////
		private CActionCreerListeObjetsDonnee ActionCreerListe
		{
			get
			{
				return (CActionCreerListeObjetsDonnee)ObjetEdite;
			}
		}

		/// ////////////////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_panelFiltre.InitSansVariables ((CFiltreDynamique)ActionCreerListe.Filtre.Clone());
			m_chkCompterUniquement.Checked = ActionCreerListe.CompterUniquement;
			m_chkCompterUniquement.BringToFront();
			m_chkFiltreDefaut.Checked = ActionCreerListe.AppliquerFiltreParDefaut;
			m_chkFiltreDefaut.BringToFront();
		}

		/// ////////////////////////////////////////////////////
		protected override CResultAErreur MAJ_Champs()
		{
			ActionCreerListe.Filtre = (CFiltreDynamique)m_panelFiltre.FiltreDynamique.Clone();
			ActionCreerListe.CompterUniquement = m_chkCompterUniquement.Checked;
			ActionCreerListe.AppliquerFiltreParDefaut = m_chkFiltreDefaut.Checked;
			CResultAErreur result = base.MAJ_Champs();
			return result;
		}

		private void m_chkCompterUniquement_CheckedChanged(object sender, System.EventArgs e)
		{
			ActionCreerListe.CompterUniquement = m_chkCompterUniquement.Checked;
			OnChangeTypeRetour();	
		}

        private void CFormEditActionCreerListeObjetsDonnee_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
        }




	}
}

