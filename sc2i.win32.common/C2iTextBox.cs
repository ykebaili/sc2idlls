using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iTextBox.
	/// </summary>
    [AutoExec("Autoexec")]
	public class C2iTextBox : TextBox, IControlALockEdition
	{
		private bool m_bLockEdition = false;
		private Timer m_timerLock;
		private IContainer components;
		private Bitmap m_imageMiniLoupe = null;
		private Bitmap m_imageLock = null;
        private string m_strTexteSiVide = "";

		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		public C2iTextBox()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();
            GereUserPaint();
			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

        public static void Autoexec()
        {
            CWin32Traducteur.AddProprieteATraduire(typeof(C2iTextBox), "EmptyText");
        }

        public string EmptyText
        {
            get
            {
                return m_strTexteSiVide;
            }
            set
            {
                m_strTexteSiVide = value;
                GereUserPaint();
            }
        }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (m_imageMiniLoupe != null)
				m_imageMiniLoupe.Dispose();
			if (m_imageLock != null)
				m_imageLock.Dispose();
			m_imageMiniLoupe = null;
		}

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_timerLock = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_timerLock
            // 
            this.m_timerLock.Interval = 150;
            this.m_timerLock.Tick += new System.EventHandler(this.m_timerLock_Tick);
            // 
            // C2iTextBox
            // 
            this.TextChanged += new System.EventHandler(this.C2iTextBox_TextChanged);
            this.MouseLeave += new System.EventHandler(this.C2iTextBox_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C2iTextBox_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C2iTextBox_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C2iTextBox_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C2iTextBox_MouseDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.C2iTextBox_KeyPress);
            this.MouseEnter += new System.EventHandler(this.C2iTextBox_MouseEnter);
            this.ResumeLayout(false);

		}
		#endregion
		
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				//Enabled = !m_bLockEdition;
                GereUserPaint();
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				Refresh();
			}
		}

        	

        //-------------------------------------------
        private Font m_originalFont = null;
        protected void GereUserPaint()
        {
            if (DesignMode)
                return;
            if (LockEdition || (m_strTexteSiVide.Length > 0 && Text.Length == 0))
            {
                if (!GetStyle(ControlStyles.UserPaint))
                {
                    m_originalFont = Font;
                    SetStyle(ControlStyles.UserPaint, true);
                    Refresh();
                }
            }
            else if (GetStyle(ControlStyles.UserPaint))
            {
                
                SetStyle(ControlStyles.UserPaint, false);
                //Car le passage à UserPaint = false perd la fonte.
                //Je ne sais pas pourquoi
                if (m_originalFont != null)
                    Font = m_originalFont;
                Refresh();
            }
        }
		
		//-------------------------------------------
		private string GetTexte()
		{
			if (PasswordChar!='\0')
			{
				string strRetour = "";
				for ( int n = 0; n < Text.Length; n++ )
					strRetour += PasswordChar;
				return strRetour;
			}
			return Text;
		}

		/// ///////////////////////////////////////////////////
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			Rectangle rct = ClientRectangle;
			Brush br = new SolidBrush(BackColor);
			pevent.Graphics.FillRectangle(br, rct);

			br.Dispose();
		}
		/// ///////////////////////////////////////////////////
		protected override void OnPaint(PaintEventArgs e)
		{
			TextFormatFlags flags = TextFormatFlags.Default;
			switch (TextAlign)
			{
				case HorizontalAlignment.Left:
					flags = TextFormatFlags.Left;
					break;
				case HorizontalAlignment.Center:
					flags = TextFormatFlags.HorizontalCenter;
					break;
				case HorizontalAlignment.Right:
					flags = TextFormatFlags.Right;
					break;
			}
			if (WordWrap)
				flags |= TextFormatFlags.WordBreak;
			flags |= TextFormatFlags.HidePrefix;
            string strTexte = GetTexte();
            Color c = ForeColor;
            if (strTexte.Length == 0)
            {
                strTexte = m_strTexteSiVide;
                c = Color.LightGray;
            }
			TextRenderer.DrawText(e.Graphics, strTexte, Font, ClientRectangle, c, BackColor, flags);
		}

		private void C2iTextBox_MouseMove(object sender, MouseEventArgs e)
		{
			if ( LockEdition && m_rectImage.Contains (new Point(e.X, e.Y) ))
				Cursor = Cursors.Hand;
			else
				Cursor = Cursors.IBeam;
		}

		private Image ImageLock
		{
			get
			{
				if (m_imageLock == null)
				{
					m_imageLock = Properties.Resources._lock;
				}
				return m_imageLock;
			}
		}

		private Image ImageMiniLoupe
		{
			get
			{
				if (m_imageMiniLoupe == null)
				{
					try
					{
						m_imageMiniLoupe = Properties.Resources.loupe_TextBox;
					}
					catch
					{
					}
				}
				return m_imageMiniLoupe;
			}
		}

		Rectangle m_rectImage = new Rectangle(0, 0, 0, 0);
		private bool m_bTextTropGrand = false;
		private void C2iTextBox_MouseEnter(object sender, EventArgs e)
		{
			if (LockEdition && m_bTextTropGrand)
			{
				Graphics g = CreateGraphics();
				Image img = ImageMiniLoupe;
				if (img != null)
				{
					Rectangle rc = ClientRectangle;
					rc.Location = new Point(rc.Right - img.Width, rc.Bottom - img.Height);
					rc.Size = img.Size;
					g.DrawImage(img, rc.Location);
					m_rectImage = rc;
				}
				g.Dispose();
			}
			else
				m_rectImage = new Rectangle(0, 0, 0, 0);
		}

		private void C2iTextBox_MouseLeave(object sender, EventArgs e)
		{
			m_rectImage = new Rectangle(0, 0, 0, 0);
			if (LockEdition)
			{
				Refresh();
			}
		}

		private void C2iTextBox_MouseClick(object sender, MouseEventArgs e)
		{
			Point pt = new Point(e.X,e.Y );
			if (LockEdition && m_rectImage.Contains(pt))
			{
				CFormZoomTextFloat.Show(Text, Width, LockEdition, BackColor);
			}
		}

		private void C2iTextBox_TextChanged(object sender, EventArgs e)
		{
			Graphics g = CreateGraphics();
			SizeF sz = g.MeasureString(Text, Font, Width-8);
			m_bTextTropGrand = sz.Height > Height-4;
            GereUserPaint();
			g.Dispose();
		}

		private void C2iTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (LockEdition &&
				e.KeyCode != Keys.Up &&
					e.KeyCode != Keys.Down &&
					e.KeyCode != Keys.Right &&
					e.KeyCode != Keys.Left &&
					e.KeyCode != Keys.PageDown &&
					e.KeyCode != Keys.PageUp &&
					e.KeyCode != Keys.End &&
					e.KeyCode != Keys.Home && 
					e.KeyCode != Keys.Shift && 
					e.KeyCode != Keys.LShiftKey && 
					e.KeyCode != Keys.RShiftKey &&
					e.KeyCode != Keys.ShiftKey &&
					e.KeyCode != Keys.Alt && 
                    (e.KeyCode != Keys.C || !e.Control) &&
                    (e.KeyCode != Keys.Insert || !e.Control ) )
			{
				e.Handled = true;
				ShowLock();
			}
		}

	

		private void C2iTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (LockEdition)
			{
                if (e.KeyChar != 3 || ModifierKeys != Keys.Control)
                {
                    e.Handled = true;
                    ShowLock();
                }
			}
		}

		private void ShowLock()
		{
			m_nNbLocks = 5;
			m_timerLock.Enabled = false;
			m_timerLock.Enabled = true;
		}

		private int m_nNbLocks = 0;
		private void m_timerLock_Tick(object sender, EventArgs e)
		{
            try
            {
                if (m_nNbLocks % 2 == 0)
                    Refresh();
                else
                {
                    Graphics g = CreateGraphics();
                    Image img = ImageLock;
                    if (img != null)
                        g.DrawImage(img, new Point(ClientRectangle.Right - img.Width, 0));
                    g.Dispose();
                }
                m_nNbLocks--;
                if (m_nNbLocks < 0)
                    m_timerLock.Enabled = false;
            }
            catch
            {
                m_timerLock.Enabled = false;
            }
		}

        private void C2iTextBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        
	}
}
