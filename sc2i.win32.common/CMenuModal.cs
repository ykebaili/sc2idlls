using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common.Properties;

namespace sc2i.win32.common
{
    //Permet d'afficher un menu de manière modale
    public partial class CMenuModal : Form
    {
        Point m_posMenu = new Point(0, 0);
        private ToolStripItem m_selItem = null;



        public CMenuModal()
        {
            InitializeComponent();
        }

        public ToolStripItem ShowMenu(Point pt)
        {
            m_posMenu = pt;
            EventHandler onClick = new EventHandler(item_Click);

            ToolStripSeparator sep = new ToolStripSeparator();
            Items.Add(sep);

            PictureBox pic = new PictureBox();
            pic.Image = Resources.cancelx16;
            pic.Cursor = Cursors.Hand;
            pic.Anchor = AnchorStyles.None;
            pic.Click += new EventHandler(pic_Click);
            
            ToolStripControlHost itemCancel = new ToolStripControlHost(pic);
            Items.Add(itemCancel);


            foreach (ToolStripItem item in m_menu.Items)
                SetOnClick(item, onClick);
            ShowDialog();

            Items.Remove(sep);
            Items.Remove(itemCancel);


            foreach (ToolStripItem item in m_menu.Items)
                RemoveOnClick(item, onClick);
            return m_selItem;
        }

        void pic_Click(object sender, EventArgs e)
        {
            Menu.Hide();
        }

        private void SetOnClick(ToolStripItem item, EventHandler onClick)
        {
            ToolStripMenuItem menu = item as ToolStripMenuItem;
            if (menu != null)
            {
                foreach (ToolStripItem child in menu.DropDownItems)
                    SetOnClick(child, onClick);
                if (menu.DropDownItems.Count == 0)
                    item.Click += onClick;
            }

        }

        private void RemoveOnClick(ToolStripItem item, EventHandler onClick)
        {
            ToolStripMenuItem menu = item as ToolStripMenuItem;
            if (menu != null)
            {
                foreach (ToolStripItem child in menu.DropDownItems)
                    SetOnClick(child, onClick);
                if (menu.DropDownItems.Count == 0)
                    item.Click -= onClick;
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            m_selItem = sender as ToolStripItem;
        }


        private void CMenuModal_Load(object sender, EventArgs e)
        {


        }

        private void m_menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Close();
        }

        public new ContextMenuStrip Menu
        {
            get
            {
                return m_menu;
            }
        }

        public ToolStripItemCollection Items
        {
            get
            {
                return m_menu.Items;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            m_timer.Enabled = false;
            m_menu.Show(m_posMenu);
        }
    }
}
