using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data.dynamic.StructureImport.SmartImport;

namespace sc2i.win32.data.dynamic.import
{
    public partial class CControleOptionCreation : UserControl
    {
        private EOptionCreationElementImport m_option = EOptionCreationElementImport.None;
        
        
        //--------------------------------------------------
        public CControleOptionCreation()
        {
            InitializeComponent();
            m_image.Image = new COptionCreationElementImport(EOptionCreationElementImport.Automatic).GetImage();
        }

        //--------------------------------------------------
        public bool IsApplicable
        {
            get
            {
                return m_image.Visible;
            }
            set
            {
                m_image.Visible = value;
            }
        }

        //--------------------------------------------------
        public EOptionCreationElementImport Option
        {
            get
            {
                return m_option;
            }
            set
            {
                m_option = value;
                UpdateVisuel();
            }
        }

        //--------------------------------------------------
        private void UpdateVisuel()
        {
            COptionCreationElementImport o = new COptionCreationElementImport(m_option);
            m_image.Image = o.GetImage();
        }

        //--------------------------------------------------
        private void m_image_Click(object sender, EventArgs e)
        {
            ToolStripDropDown menu = new ToolStripDropDown();
            foreach ( EOptionCreationElementImport option in Enum.GetValues(typeof(EOptionCreationElementImport)))
            {
                if (option != EOptionCreationElementImport.Confirm)//SC 26 06 Masque le manuel car pas implémenté
                {
                    COptionCreationElementImport o = new COptionCreationElementImport(option);
                    ToolStripMenuItem itemOption = new ToolStripMenuItem(o.Libelle);
                    itemOption.Image = o.GetImage();
                    itemOption.Checked = option == m_option;
                    itemOption.Tag = option;
                    itemOption.Click += itemOption_Click;

                    menu.Items.Add(itemOption);
                }
            }
            menu.Show(this, new Point(0, Height));
        }

        //--------------------------------------------------
        public event EventHandler ValueChanged;

        //--------------------------------------------------
        void itemOption_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                m_option = (EOptionCreationElementImport)item.Tag;
                UpdateVisuel();
                if (ValueChanged != null)
                    ValueChanged(this, null);
            }
        }


    }
}
