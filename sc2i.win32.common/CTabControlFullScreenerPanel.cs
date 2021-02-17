using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public partial class CTabControlFullScreenerPanel : UserControl
    {
        private C2iTabControl m_tabControl = null;

        private Crownwood.Magic.Controls.TabPage m_pageZoom = null;

        private Dictionary<Control, Point> m_dicSaveLocations = new Dictionary<Control, Point>();
        private Dictionary<Control, Size> m_dicSaveSizes = new Dictionary<Control, Size>();

        private Size m_sizeButtonPetit = new Size(12, 12);

        public CTabControlFullScreenerPanel()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------------------------------
        public C2iTabControl TabControl
        {
            get
            {
                return m_tabControl;
            }
            set
            {
                m_tabControl = value;
                if (!DesignMode && value != null)
                {
                    m_tabControl.TabPagesChanged += m_tabControl_TabPagesChanged;
                    m_tabControl.SelectionChanged += m_tabControl_SelectionChanged;
                    m_tabControl.SizeChanged += m_tabControl_SizeChanged;
                    HookActiveTabPage();
                    Visible = false;
                }
            }
        }

        //---------------------------------------------------------------------------------------------
        public Color TitleBackColor
        {
            get { return m_panelTop.BackColor; }
            set
            {
                m_panelTop.BackColor = value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public Color TitleForeColor
        {
            get { return m_panelTop.ForeColor; }
            set
            {
                m_panelTop.ForeColor= value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public Font TitleFont
        {
            get { return m_lblTitle.Font; }
            set
            {
                m_lblTitle.Font = value;
            }
        }

        void m_tabControl_SizeChanged(object sender, EventArgs e)
        {
            HookActiveTabPage();
        }


        //---------------------------------------------------------------------------------------------
        void m_tabControl_TabPagesChanged(object sender, EventArgs e)
        {
            if (!(m_btnFullScreen.Parent is Crownwood.Magic.Controls.TabPage))
                HookActiveTabPage();
        }

        //----------------------------------------------------------------------
        void m_tabControl_SelectionChanged(object sender, EventArgs e)
        {
            
                HookActiveTabPage();
            
        }
        //---------------------------------------------------------------------------------------------
        private void HookActiveTabPage()
        {
            if (m_tabControl.SelectedTab != null)
            {
                m_tabControl.SelectedTab.SuspendDrawing();
                m_btnFullScreen.Parent = m_tabControl.SelectedTab;
                m_btnFullScreen.BringToFront();
                m_btnFullScreen.Visible = true;
                CalculPositionBouton();
                m_tabControl.SelectedTab.ResumeDrawing();
            }
        }



        //----------------------------------------------------------------------
        private void m_btnFullScreen_Click(object sender, EventArgs e)
        {
            if (m_pageZoom == null)
            {
                Crownwood.Magic.Controls.TabPage page = m_tabControl.SelectedTab;
                if (page != null )
                {
                    Crownwood.Magic.Controls.TabControl otherTab=null;
                    //Vérifie s'il y a un seul control qui est un tab control
                    //(mais il y a aussi le bouton FullScreen c'est pourquoi
                    //il faut balayer)
                    foreach ( Control child in page.Controls )
                    {
                        if ( child is Crownwood.Magic.Controls.TabControl )
                            otherTab = child as Crownwood.Magic.Controls.TabControl;
                        else if ( child == m_btnFullScreen )
                                continue;
                        else if (otherTab != null )
                        {
                            otherTab = null;
                            break;
                           
                        }
                    }
                    if (otherTab != null && otherTab.SelectedTab != null)
                        page = otherTab.SelectedTab;
                }

                ShowFullScreen(page);
            }
        }

        private void ShowFullScreen ( Crownwood.Magic.Controls.TabPage page )
        {
            m_pageZoom = page;
            page.SuspendDrawing();
            m_panelControles.SuspendDrawing();
            m_panelControles.SuspendLayout();
            m_panelControles.BackColor = page.BackColor;
            List<Control> lst = new List<Control>();
            foreach (Control ctrl in page.Controls)
                lst.Add(ctrl);
            foreach( Control ctrl in lst )
            {
                m_dicSaveLocations[ctrl] = ctrl.Location;
                m_dicSaveSizes[ctrl] = ctrl.Size;
                ctrl.Parent = m_panelControles;
            }
            m_lblTitle.Text = page.Title;
            m_btnFullScreen.Visible = false;
            m_panelControles.ResumeLayout();
            m_panelControles.ResumeDrawing();
            page.ResumeDrawing();
            m_tabControl.Visible = false;
            Visible = true;
            BringToFront();
        }

        //----------------------------------------------------------------------
        private void AnnuleFullScreen()
        {
            m_pageZoom.SuspendDrawing();
            m_pageZoom.SuspendLayout();
            m_panelControles.SuspendDrawing();
            List<Control> lst = new List<Control>();
            foreach (Control ctrl in m_panelControles.Controls)
                lst.Add(ctrl);
            foreach( Control ctrl in lst )
            {
                Point pt;
                Size sz;
                if ( m_dicSaveLocations.TryGetValue(ctrl, out pt ) && 
                    m_dicSaveSizes.TryGetValue(ctrl, out sz))
                {
                    ctrl.Parent = m_pageZoom;
                    ctrl.Location = pt;
                    ctrl.Size = sz;
                }
            }
            m_dicSaveSizes.Clear();
            m_dicSaveLocations.Clear();
            m_pageZoom.ResumeLayout();
            m_pageZoom.ResumeDrawing();
            m_panelControles.ResumeDrawing();
            Visible = false;
            m_tabControl.Visible = true;
            m_btnFullScreen.Visible = true;
            m_btnFullScreen.Size = m_sizeButtonPetit;
            CalculPositionBouton();
            m_pageZoom = null;
        }

        private void m_btnFullScreen_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void m_btnFullScreen_ParentChanged(object sender, EventArgs e)
        {
            CalculPositionBouton();
        }

        private void CalculPositionBouton()
        {
            if (m_btnFullScreen.Parent != null)
            {
                m_btnFullScreen.Location = new Point(m_btnFullScreen.Parent.ClientSize.Width - m_btnFullScreen.Width, 0);
                m_btnFullScreen.BringToFront();
            }
        }

        private void m_btnFullScreen_MouseEnter(object sender, EventArgs e)
        {
            m_btnFullScreen.Size = m_btnNoFullScreen.Size;
            CalculPositionBouton();
            
        }

        private void m_btnFullScreen_MouseLeave(object sender, EventArgs e)
        {
            m_btnFullScreen.Size = m_sizeButtonPetit;
            CalculPositionBouton();
        }

        private void m_btnNoFullScreen_Click(object sender, EventArgs e)
        {
            AnnuleFullScreen();
        }

       
    }
}

