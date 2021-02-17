using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using System.Drawing.Imaging;
using System.Drawing;
using sc2i.common;
using sc2i.drawing;


namespace sc2i.win32.common
{
    public class C2iSelectImage : PictureBox, IControlALockEdition
    {
        private bool m_bLockEdition = false;

        public C2iSelectImage()
            : base()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
        }


        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // C2iSelectImage
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Click += new System.EventHandler(this.C2iSelectImage_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.C2iSelectImage_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.C2iSelectImage_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void C2iSelectImage_Click(object sender, EventArgs e)
        {
            
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                if (m_bLockEdition)
                    Cursor = Cursors.Default;
                else
                    Cursor = Cursors.Hand;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        private void C2iSelectImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_bLockEdition)
                return;
            if (e.Button == MouseButtons.Left)
            {
                string strFiltre = I.T("Image file|*.bmp;*.gif;*.ico;*.jpg;.tif;*.png|All files|*.*|20000");
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = strFiltre;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(dlg.FileName);
                        this.Image = img;
                    }
                    catch (Exception ex)
                    {
                        CResultAErreur result = CResultAErreur.True;
                        result.EmpileErreur(new CErreurException(ex));
                        CFormAlerte.Afficher(result.Erreur);
                    }
                }
            }
            else
                Image = null;
        }

        private void C2iSelectImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rct = ClientRectangle;
            Brush br = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(br, rct);
            br.Dispose();
            if (Image != null)
            {
                Size sz = CUtilImage.GetSizeAvecRatio(Image, rct.Size);
                e.Graphics.DrawImage(Image, (rct.Width - sz.Width) / 2, (rct.Height - sz.Height) / 2,
                    sz.Width, sz.Height);
            }
        }
    }
}
