using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.win32.common.customizableList;
using sc2i.win32.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.common;

namespace sc2i.win32.data.dynamic.import
{
    public partial class CControleSetupSmartImportField : CCustomizableListControl
    {
        //--------------------------------------------------------------------
        public CControleSetupSmartImportField()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        public DataTable SourceTable
        {
            get
            {
                return m_controleSource.SourceTable;
            }
            set
            {
                m_controleSource.SourceTable = value;
            }
        }

        //--------------------------------------------------------------------
        public override bool AspectDifferentEnInactif
        {
            get
            {
                return true;
            }
        }

        //--------------------------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = base.MyInitChamps(item);
            if ( !result )
                return result;
            CSetupSmartImportItem si = item as CSetupSmartImportItem;
            if (si != null)
            {
                m_lblChamp.BackColor = si.BackColor;
                m_lblMarge.BackColor = si.BackColor;
                if (si.Niveau > 0)
                {
                    m_lblMarge.Width = si.Niveau * 10;
                    m_lblMarge.Visible = true;
                }
                else
                    m_lblMarge.Visible = false;

                if (si.Propriete != null)
                    m_lblChamp.Text = si.Propriete.Nom;
                else
                    m_lblChamp.Text = "";

                if (si.ItemParent == null)
                    si.Source = new CSourceSmartImportObjet();
                BackColor = Color.White;
                ColorInactive = Parent.BackColor;
                m_wndOptionCreation.IsApplicable = false;

                m_lblValeur.Text = si.LibelleValeur;

                m_btnExpand.Visible = si.IsExpandable;
                m_btnExpand.Text = si.IsCollapse ? "+" : "-";

                if ( si.IsEntite )
                {
                    if (si.Index != this.AssociatedListControl.CurrentItemIndex)
                        BackColor = si.BackColor;
                    ColorInactive = si.BackColor;
                }
                m_wndOptionCreation.IsApplicable = si.CanHaveOptionCreation;
                m_wndOptionCreation.Option = si.OptionCreation;

                IEnumerable<Type> sourcesPossibles = si.SourcesPossibles;
                if ( sourcesPossibles.Count() > 0)
                {
                    m_controleSource.Visible = true;
                    m_controleSource.TypesSourcesValides = sourcesPossibles;
                    m_controleSource.Init(si.Source, si);
                    m_controleSource.DefaultValue = si.ValeurParDefaut;
                    m_controleSource.SetIsDrawingImage(IsCreatingImage);
                    m_imageSearchKey.Visible = true;
                }
                else
                {
                    m_controleSource.Visible = false;
                }


                UpdateVisuelKey();

            }
            else
                m_btnExpand.Visible = false;
            return result;
        }

        //--------------------------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result = base.MyMajChamps();
            CSetupSmartImportItem si = CurrentItem as CSetupSmartImportItem;
            if (result && si != null)
            {
                si.OptionCreation = m_wndOptionCreation.Option;
                si.Source = m_controleSource.Source;
            }
            return result;
        }

        //--------------------------------------------------------------------
        public override bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        //--------------------------------------------------------------------
        private void m_btnExpand_Click(object sender, EventArgs e)
        {
            CSetupSmartImportItem item = CurrentItem as CSetupSmartImportItem;
            if ( item != null && item.IsCollapse )
                ((CControleSetupSmartImport)AssociatedListControl).Expand(CurrentItem);
            else if (item != null && !item.IsCollapse)
                ((CControleSetupSmartImport)AssociatedListControl).Collapse(CurrentItem);
            m_btnExpand.Text = item.IsCollapse ? "+" : "-";
        }

       


        //--------------------------------------------------------------------
        private void m_wndOptionCreation_ValueChanged(object sender, EventArgs e)
        {
            HasChange = true;
        }

        //---------------------------------------------------------------
        private void m_controleSource_SourceChanged(object sender, EventArgs e)
        {
            HasChange = true;
        }

        //---------------------------------------------------------------
        private void m_controleSource_ValueChanged(object sender, EventArgs e)
        {
            HasChange = true;
        }

        //---------------------------------------------------------------
        private void m_imageSearchKey_Click(object sender, EventArgs e)
        {
            CSetupSmartImportItem si = CurrentItem as CSetupSmartImportItem;
            ToolStripDropDown menu = new ToolStripDropDown();
            ToolStripMenuItem itemKey = new ToolStripMenuItem(I.T("don't use this field to search entity|20117"));
            itemKey.Image = Properties.Resources.SearchKey_No;
            itemKey.Checked = si != null && !si.UseAsKey;
            itemKey.CheckOnClick = true;
            itemKey.Tag = false;
            itemKey.Click += itemKey_Click;
            menu.Items.Add(itemKey);
            itemKey = new ToolStripMenuItem(I.T("Use this field to search entity|20118"));
            itemKey.Image = Properties.Resources.SearchKey_Yes;
            itemKey.CheckOnClick = true;
            itemKey.Checked = si != null && !si.UseAsKey;
            itemKey.Tag = true;
            itemKey.Click += itemKey_Click;
            menu.Items.Add(itemKey);
            menu.Show(m_imageSearchKey, new Point(0, Height));
        }

        //-----------------------------------------------------------
        private void UpdateVisuelKey()
        {
            
            CSetupSmartImportItem si = CurrentItem as CSetupSmartImportItem;
            if ( si != null )
            {
                if ( si.CanBeUsedAsKey )
                {
                    m_imageSearchKey.Visible = true;
                    m_imageSearchKey.Image = si.UseAsKey ? Properties.Resources.SearchKey_Yes : Properties.Resources.SearchKey_No;
                }
                else
                    m_imageSearchKey.Visible = false;
            }
        }

        //-----------------------------------------------------------
        void itemKey_Click(object sender, EventArgs e)
        {
            CSetupSmartImportItem si = CurrentItem as CSetupSmartImportItem;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && item.Tag is bool && si != null)
            {
                si.UseAsKey = (bool)item.Tag;
                UpdateVisuelKey();
                HasChange = true;
            }
        }

        public bool Resource { get; set; }
    }
}
