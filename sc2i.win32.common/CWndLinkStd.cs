using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	
	public enum TypeLinkStd
	{
		Modification = 0,
		Ajout,
		Suppression,
		Filtre,
        Custom
	}

	/// <summary>
	/// Description résumée de CWndLinkStd.
	/// </summary>
	[DefaultEvent("LinkClicked")]
    [AutoExec("Autoexec")]
	public class CWndLinkStd : System.Windows.Forms.UserControl
	{		
		public event System.EventHandler LinkClicked;

		private TypeLinkStd m_typeLink = TypeLinkStd.Custom;

        private bool m_bShortMode = false;
        private int m_nWidthNotShort = 100;

		private System.Windows.Forms.LinkLabel m_link;
		private System.Windows.Forms.ImageList m_listeImages;
		private System.Windows.Forms.PictureBox m_image;
		private System.ComponentModel.IContainer components;

		/// ///////////////////////////////////////////////////
		public CWndLinkStd()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm
			OnChangeTypeLien();

		}

        /// ///////////////////////////////////////////////////
        public static void Autoexec()
        {
            CWin32Traducteur.AddProprieteATraduire(typeof(CWndLinkStd), "CustomText");
        }

		/// ///////////////////////////////////////////////////
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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CWndLinkStd));
            this.m_link = new System.Windows.Forms.LinkLabel();
            this.m_listeImages = new System.Windows.Forms.ImageList(this.components);
            this.m_image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_image)).BeginInit();
            this.SuspendLayout();
            // 
            // m_link
            // 
            this.m_link.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_link.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_link.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.m_link.Location = new System.Drawing.Point(24, 0);
            this.m_link.Name = "m_link";
            this.m_link.Size = new System.Drawing.Size(88, 24);
            this.m_link.TabIndex = 0;
            this.m_link.TabStop = true;
            this.m_link.Text = "Detail|19";
            this.m_link.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_link_LinkClicked);
            this.m_link.SizeChanged += new System.EventHandler(this.m_link_SizeChanged);
            // 
            // m_listeImages
            // 
            this.m_listeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_listeImages.ImageStream")));
            this.m_listeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.m_listeImages.Images.SetKeyName(0, "document_edit.png");
            this.m_listeImages.Images.SetKeyName(1, "document_add.png");
            this.m_listeImages.Images.SetKeyName(2, "document_delete.png");
            this.m_listeImages.Images.SetKeyName(3, "document_filter.png");
            // 
            // m_image
            // 
            this.m_image.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_image.Image = global::sc2i.win32.common.Properties.Resources.document_add;
            this.m_image.Location = new System.Drawing.Point(0, 0);
            this.m_image.Name = "m_image";
            this.m_image.Size = new System.Drawing.Size(24, 24);
            this.m_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_image.TabIndex = 1;
            this.m_image.TabStop = false;
            this.m_image.Click += new System.EventHandler(this.m_image_Click);
            // 
            // CWndLinkStd
            // 
            this.Controls.Add(this.m_link);
            this.Controls.Add(this.m_image);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "CWndLinkStd";
            this.Size = new System.Drawing.Size(112, 24);
            ((System.ComponentModel.ISupportInitialize)(this.m_image)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        /// ///////////////////////////////////////////////////
        public bool ShortMode
        {
            get
            {
                return m_bShortMode;
            }
            set
            {
                m_bShortMode = value;
                if (m_bShortMode)
                    Width = m_image.Width;
                else
                    Width = m_nWidthNotShort;
            }
        }

        /// ///////////////////////////////////////////////////
        public Image CustomImage
        {
            get
            {
                return m_image.Image;
            }
            set
            {
                if ( TypeLien == TypeLinkStd.Custom )
                    m_image.Image = value;
            }
        }

        /// ///////////////////////////////////////////////////
        public string CustomText
        {
            get
            {
                return m_link.Text;
            }
            set
            {
                if (TypeLien == TypeLinkStd.Custom)
                    m_link.Text = value;
            }
        }

		/// ///////////////////////////////////////////////////
		public TypeLinkStd TypeLien
		{
			get
			{
				return m_typeLink;
			}
			set
			{
				if ( m_typeLink != value )
				{
					m_typeLink = value;
					OnChangeTypeLien();
				}

			}
		}

		/// ///////////////////////////////////////////////////
		public void OnChangeTypeLien()
		{
			switch (m_typeLink)
			{
				case TypeLinkStd.Ajout :
					m_link.Text = I.T("Add|18");
					break;
				case TypeLinkStd.Modification :
					m_link.Text = I.T("Detail|19");
					break;
				case TypeLinkStd.Suppression :
					m_link.Text = I.T("Remove|20");
					break;
				case TypeLinkStd.Filtre :
					m_link.Text = I.T("Filter|21");
					break;
			}
            if ( (int)m_typeLink < m_listeImages.Images.Count )
			    m_image.Image = m_listeImages.Images[(int)m_typeLink];
		}

		/// //////////////////////////////////////////////////////////////////////////
		private void m_link_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( LinkClicked != null )
			{
				LinkClicked( sender, new EventArgs());
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		private void m_image_Click(object sender, System.EventArgs e)
		{
			if ( LinkClicked != null )
			{
				LinkClicked( sender, new EventArgs());
			}
		}
		/// //////////////////////////////////////////////////////////////////////////
		public Color LinkColor
		{
			get
			{
				return m_link.LinkColor;
			}
			set
			{
				m_link.LinkColor = value;
			}
		}

        /// //////////////////////////////////////////////////////////////////////////
        private void m_link_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bShortMode)
                m_nWidthNotShort = Size.Width;
            else if (Size.Width != m_image.Width)
                m_bShortMode = false;
        }


		/// //////////////////////////////////////////////////////////////////////////

	}
}
