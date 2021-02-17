using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace sc2i.win32.common
{
    public partial class CArbreChampsDotNet : UserControl
    {
        private Type m_typeSource = null;

        public CArbreChampsDotNet()
        {
            InitializeComponent();
        }

        public event EventHandler OkEvent;
        public event EventHandler CancelEvent;

        //--------------------------------------------------------------------------------
        private void m_lnkOk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_txtNomChamp.Text.Trim() != "")
            {
                if (OkEvent != null)
                    OkEvent(this, null);
            }
        }

        //--------------------------------------------------------------------------------
        public string ChampSelectionne
        {
            get
            {
                return m_txtNomChamp.Text;
            }
        }

        //--------------------------------------------------------------------------------
        private void m_lnkAnnuler_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CancelEvent != null)
                CancelEvent(this, null);
        }

        //--------------------------------------------------------------------------------
        public void Init(Type tpSource)
        {
            m_arbreChamps.BeginUpdate();
            m_arbreChamps.Nodes.Clear();
            if ( tpSource != null )
                FillNodes ( m_arbreChamps.Nodes, tpSource );
            m_arbreChamps.EndUpdate();
        }

        //--------------------------------------------------------------------------------
        private void FillNodes ( TreeNodeCollection nodes, Type tpSource )
        {
            List<PropertyInfo> lstProperties = new List<PropertyInfo>();
            lstProperties.AddRange( tpSource.GetProperties() );
            lstProperties.Sort ( (x,y)=>x.Name.CompareTo(y.Name));
            foreach ( PropertyInfo info in lstProperties )
            {
                TreeNode node = new TreeNode ( info.Name );
                node.Tag = info;
                if ( info.PropertyType != typeof(string) &&  info.PropertyType.GetProperties().Length > 0 )
                    node.Nodes.Add(new TreeNode());
                nodes.Add ( node );
            }
        }

        //--------------------------------------------------------------------------------
        private void m_arbreChamps_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if ( e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null )
            {
                PropertyInfo info = e.Node.Tag as PropertyInfo;
                if ( info != null )
                {
                    e.Node.Nodes.Clear();
                    FillNodes ( e.Node.Nodes, info.PropertyType );
                }
            }
        }

        private string GetName(TreeNode node)
        {
            if (node == null)
                return "";
            StringBuilder bl = new StringBuilder();
            GetName(node, bl);
            return bl.ToString();
        }

        private void GetName(TreeNode node, StringBuilder bl)
        {
            if (node.Parent != null)
                GetName(node.Parent, bl);
            if (bl.Length > 0)
                bl.Append('.');
            bl.Append(node.Text);
        }

        private void m_arbreChamps_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_txtNomChamp.Text = GetName(e.Node);
        }
    }
}
