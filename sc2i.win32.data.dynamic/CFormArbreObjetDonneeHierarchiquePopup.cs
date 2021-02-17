using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CFormArbreObjetDonneeHierarchiquePopup.
	/// </summary>
	internal class CFormArbreObjetDonneeHierarchiquePopup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.data.CArbreObjetsDonneesHierarchiques m_arbre;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAucun;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormArbreObjetDonneeHierarchiquePopup()
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
            this.m_arbre = new sc2i.win32.data.CArbreObjetsDonneesHierarchiques();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAucun = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_arbre.ElementSelectionne = null;
            this.m_arbre.ForeColorNonSelectionnable = System.Drawing.Color.DarkGray;
            this.m_arbre.HideSelection = false;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(190, 216);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(134, 216);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(56, 24);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOk.Location = new System.Drawing.Point(70, 216);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(56, 24);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_btnAucun);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_arbre);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 240);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAucun
            // 
            this.m_btnAucun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAucun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAucun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAucun.Location = new System.Drawing.Point(2, 216);
            this.m_btnAucun.Name = "m_btnAucun";
            this.m_btnAucun.Size = new System.Drawing.Size(56, 24);
            this.m_btnAucun.TabIndex = 2;
            this.m_btnAucun.Text = "None|19";
            this.m_btnAucun.UseVisualStyleBackColor = false;
            this.m_btnAucun.Click += new System.EventHandler(this.m_btnAucun_Click);
            // 
            // CFormArbreObjetDonneeHierarchiquePopup
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(192, 240);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CFormArbreObjetDonneeHierarchiquePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CFormArbreObjetDonneeHierarchiquePopup";
            this.Load += new System.EventHandler(this.CFormArbreObjetDonneeHierarchiquePopup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_arbre.ElementSelectionne == null )
				return;
			DialogResult = DialogResult.OK;
			Close();
		}



		public static  CObjetDonnee SelectObject 
			( 
			Rectangle position, 
			Type typeObjet,
			string strProprieteFilles,
			string strChampParent,
			string strProprieteAffichee,
			CObjetDonnee objetSelectionne,
			CFiltreData filtre,
			CFiltreData filtreRacine,
            string strTextBtnAucun,
            bool bAutoriserFilsDeAutorises,
			ref bool bCancel)
		{
			CFormArbreObjetDonneeHierarchiquePopup form = new CFormArbreObjetDonneeHierarchiquePopup();
			form.TopMost = true;
            form.m_arbre.AutoriserFilsDesAutorises = bAutoriserFilsDeAutorises;
			CResultAErreur result = form.m_arbre.Init ( 
				typeObjet, 
				strProprieteFilles, 
				strChampParent, 
				strProprieteAffichee,
				filtre,
				filtreRacine );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return null;
			}
			form.m_arbre.ElementSelectionne = objetSelectionne;
			form.Left = position.Left;
			form.Top = position.Top;
			form.Width = position.Width;
			form.Height = position.Height;
            if (form.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
            {
                form.Top = Screen.PrimaryScreen.WorkingArea.Bottom - form.Height;
            }

            if(strTextBtnAucun != "")
                form.m_btnAucun.Text = strTextBtnAucun;
			CObjetDonnee objSel = null;
			DialogResult dResult = form.ShowDialog();
			bCancel = true;
			if ( dResult == DialogResult.OK )
			{
				objSel = form.m_arbre.ElementSelectionne;
				bCancel = false;
			}
			if ( dResult == DialogResult.No )
			{
				bCancel = false;
			}
			form.Dispose();
			return objSel;
		}

		public override bool PreProcessMessage ( ref Message msg )
		{
			if ( msg.Msg == (int)WindowsMessages.WM_LBUTTONDOWN || 
				msg.Msg == (int)WindowsMessages.WM_RBUTTONDOWN )
			{
				IntPtr ipt = msg.LParam;
				Point pt = new Point();
				pt.X = (int)ipt.ToInt64() & 0xFFFF;
				pt.Y = (int)ipt.ToInt64() << 8;
				Control ctrl = Control.FromHandle ( msg.HWnd );
				if(  ctrl != null )
					pt = ctrl.PointToScreen ( pt );
				Rectangle rect = new Rectangle(0,0, Width, Height);
				rect = RectangleToScreen ( rect );
				if ( !rect.Contains ( pt ) )
				{
					ForceClosing();
					return true;
				}
			}
			return false;
		}

		private void ForceClosing()
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void CFormArbreObjetDonneeHierarchiquePopup_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_arbre.Focus();
		}

		private void m_btnAucun_Click(object sender, System.EventArgs e)
		{
			m_arbre.ElementSelectionne = null;
			DialogResult = DialogResult.No;
			Close();
		}

		private void m_arbre_AfterSelect(object sender, TreeViewEventArgs e)
		{
			m_btnOk.Enabled = m_arbre.ElementSelectionne != null;
		}

			
	}
}
