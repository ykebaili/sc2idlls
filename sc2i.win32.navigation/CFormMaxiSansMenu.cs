using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using sc2i.win32.common;

namespace sc2i.win32.navigation
{
	/// <summary>
	/// Description résumée de CFormMaxiSansMenu.
	/// </summary>
	public class CFormMaxiSansMenu : System.Windows.Forms.Form
	{
		private CFormNavigateur m_navigateur;
		protected sc2i.win32.common.CExtStyle m_extStyle;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormMaxiSansMenu()
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
			this.m_extStyle = new sc2i.win32.common.CExtStyle();
			this.SuspendLayout();
			// 
			// CFormMaxiSansMenu
			// 
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CFormMaxiSansMenu";
			this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
			this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
			this.Text = "CFormMaxiSansMenu";
			this.Load += new System.EventHandler(this.CFormMaxiSansMenu_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public CFormNavigateur Navigateur
		{
			get
			{
				return m_navigateur;
			}
			set
			{
				m_navigateur = value;
			}
		}

		private void CFormMaxiSansMenu_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            if (IsMdiChild)
			{	
				FormBorderStyle = FormBorderStyle.None;
				WindowState = FormWindowState.Normal;
				AgrandiAuMaxi();
			}

		}
		//---------------------------------------------------------------------------
		/////////////////////////////////////////////////////////////////////////////
		private void AgrandiAuMaxi()
		{
			AdapteTaille();
			/*if ( Parent != null && IsMdiChild )
				Parent.SizeChanged += new EventHandler(OnParentSizeChanged);*/
		}

		//---------------------------------------------------------------------------
		private void AdapteTaille()
		{
			if ( IsMdiChild && Parent != null )
			{
				if ( FormBorderStyle != FormBorderStyle.None )
					FormBorderStyle = FormBorderStyle.None;
				if ( WindowState != FormWindowState.Normal )
					WindowState = FormWindowState.Normal;
				Dock = DockStyle.Fill;
				/*Rectangle rect = Parent.ClientRectangle;
				Left = rect.Left;
				Top = rect.Top;
				Width = rect.Width;
				Height = rect.Height;*/
				
			}
		}

		/////////////////////////////////////////////////////////////////////////////
		private void OnParentSizeChanged ( object sender, EventArgs args )
		{
			if ( IsMdiChild )
				AdapteTaille();
		}

		/////////////////////////////////////////////////////////////////////////////
		public void Masquer()
		{
			Hide();
		}
	}
}
