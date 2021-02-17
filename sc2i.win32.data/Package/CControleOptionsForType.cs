using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.win32.common.customizableList;
using sc2i.data.Package;
using sc2i.common;

namespace sc2i.win32.data.Package
{
    public partial class CControleOptionsForType : CCustomizableListControl
    {
        private CConfigurationRechercheEntites m_configuration = null;
        //--------------------------------------------------------
        public CControleOptionsForType()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------
        public void SetConfiguration ( CConfigurationRechercheEntites configuration )
        {
            m_configuration = configuration;
        }

        //--------------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = CResultAErreur.True;
            base.MyInitChamps(item);
            if (m_configuration == null)
                return result;
            Type tpElement = item != null ?
                item.Tag as Type:
                null;
            if ( tpElement != null )
            {
                m_lblTypeName.Text = DynamicClassAttribute.GetNomConvivial(tpElement);
                m_lblTypeName.BackColor = Color.White;
                m_chkForcer.Checked = false;
                m_chkIgnoreType.Checked = false;
                m_chkRecursive.Checked = false;
                if (m_configuration.IsIgnore(tpElement))
                    ShowIgnore();
                else
                {
                    COptionRechercheType option = m_configuration.GetOption(tpElement);
                    if (option != null)
                    {
                        ShowForced();
                        if (option.RecursiveSearch)
                            ShowRecursive();
                    }
                }

            }
            HasChange = false;
            return result;
        }

        private bool m_bIsUpdating = false;

        //--------------------------------------------
        private void ShowIgnore()
        {
            m_bIsUpdating = true;
            m_chkIgnoreType.Checked = true;
            m_chkForcer.Checked = false;
            m_chkRecursive.Checked = false;
            m_bIsUpdating = false;
            m_lblTypeName.BackColor = Color.LightGray;
        }

        //--------------------------------------------
        public void ShowForced()
        {
            m_bIsUpdating = true;
            m_chkIgnoreType.Checked = false;
            m_chkForcer.Checked = true;
            m_bIsUpdating = false;
        }

        //--------------------------------------------
        public void ShowRecursive()
        {
            ShowForced();
            m_bIsUpdating = true;
            m_chkRecursive.Checked = true;
            m_bIsUpdating = false;
            m_lblTypeName.BackColor = Color.Green;
        }

        //-----------------------------------------------------------------------
        private Type CurrentType
        {
            get
            {
                return CurrentItem != null ?
                    CurrentItem.Tag as Type :
                    null;
            }
        }

        //-----------------------------------------------------------------------
        private void m_chkIgnoreType_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsUpdating)
                return;
            HasChange = true;
            if ( m_chkIgnoreType.Checked )
            {
                ShowIgnore();
                Type tp = CurrentType;
                if (tp != null)
                    m_configuration.AddTypeIgnore(tp);
            }
        }

        //-----------------------------------------------------------------------
        private void m_chkForcer_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsUpdating)
                return;
            HasChange = true;
            if (m_chkForcer.Checked)
            {
                ShowForced();
                Type tp = CurrentType;
                if (tp != null)
                {
                    if (m_configuration.GetOption(tp) == null)
                    {
                        COptionRechercheType option = new COptionRechercheType(tp);
                        m_configuration.AddOption(option);
                    }
                }
            }
            else
            {
                m_bIsUpdating = true;
                m_chkRecursive.Checked = false;
                m_bIsUpdating = false;
                Type tp = CurrentType;
                if (tp != null)
                {
                    m_configuration.RemoveOptions(tp);
                }
            }
        }

        //-----------------------------------------------------------------------
        private void m_chkRecursive_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsUpdating)
                return;
            HasChange = true;
            if (m_chkRecursive.Checked)
            {
                ShowRecursive();
                Type tp = CurrentType;
                if (tp != null)
                {
                    COptionRechercheType option = m_configuration.GetOption(tp);
                    if ( option == null )
                    {
                        option = new COptionRechercheType(tp);
                        m_configuration.AddOption(option);
                    }
                    option.RecursiveSearch = true;
                }
            }
        }
    }
}
