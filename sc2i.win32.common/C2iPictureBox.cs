using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iPictureBox.
	/// </summary>
    public class C2iPictureBox : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.PictureBox m_wndImage;
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public C2iPictureBox()
        {
            // Cet appel est requis par le Concepteur de formulaires Windows.Forms.
            InitializeComponent();

            // TODO : ajoutez les initialisations après l'appel à InitializeComponent

        }

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_wndImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndImage)).BeginInit();
            this.SuspendLayout();
            // 
            // m_wndImage
            // 
            this.m_wndImage.Location = new System.Drawing.Point(40, 48);
            this.m_wndImage.Name = "m_wndImage";
            this.m_wndImage.Size = new System.Drawing.Size(80, 64);
            this.m_wndImage.TabIndex = 0;
            this.m_wndImage.TabStop = false;
            this.m_wndImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_wndImage_MouseClick);
            // 
            // C2iPictureBox
            // 
            this.Controls.Add(this.m_wndImage);
            this.Name = "C2iPictureBox";
            this.Load += new System.EventHandler(this.C2iPictureBox_Load);
            this.BackColorChanged += new System.EventHandler(this.C2iPictureBox_BackColorChanged);
            this.Resize += new System.EventHandler(this.C2iPictureBox_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.m_wndImage)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void C2iPictureBox_BackColorChanged(object sender, System.EventArgs e)
        {
            m_wndImage.BackColor = BackColor;
        }

        public Image Image
        {
            get
            {
                return m_wndImage.Image;
            }
            set
            {
                m_wndImage.Image = value;
                AjusteVisuel();
            }
        }

        private void AjusteVisuel()
        {
            Size sz = ClientSize;
            Rectangle rect = new Rectangle(0, 0, (int)sz.Width, (int)sz.Height);
            if (Image != null)
            {
                int nWidth = Image.Width;
                int nHeight = Image.Height;
                if (nWidth == 0 || nHeight == 0)
                    return;

                double fRatioImage = (double)nWidth / (double)nHeight;
                double fRatioSize = (double)rect.Width / (double)rect.Height;
                if (fRatioImage < fRatioSize)
                {
                    //Prend toute la hauteur
                    nHeight = rect.Height;
                    nWidth = (int)(rect.Height * fRatioImage);
                }
                else
                {
                    nWidth = rect.Width;
                    nHeight = (int)(rect.Width / fRatioImage);
                }
                m_wndImage.Visible = false;
                m_wndImage.SizeMode = PictureBoxSizeMode.StretchImage;
                m_wndImage.Left = (rect.Width - nWidth) / 2;
                m_wndImage.Top = (rect.Height - nHeight) / 2;
                m_wndImage.Height = nHeight;
                m_wndImage.Width = nWidth;
                m_wndImage.Visible = true;
            }
        }

        private void C2iPictureBox_Load(object sender, System.EventArgs e)
        {

        }

        private void C2iPictureBox_Resize(object sender, System.EventArgs e)
        {
            AjusteVisuel();
        }


        private void m_wndImage_MouseClick(object sender, MouseEventArgs e)
        {
            Point pt = new Point ( e.X, e.Y );
            pt = m_wndImage.PointToScreen ( pt );
            pt = this.PointToClient ( pt );
            MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta);
            OnMouseClick(args);
        }

    }
}
