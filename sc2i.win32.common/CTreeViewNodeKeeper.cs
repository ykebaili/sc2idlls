using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public class CTreeViewNodeKeeper : Dictionary<string, CTreeViewNodeKeeper>
    {
        private string[] m_strSelectionPath = new string[0];
        //------------------------------------
        public CTreeViewNodeKeeper()
        {
        }

        public string[] SelectionPath
        {
            get
            {
                return m_strSelectionPath;
            }
            set
            {
                m_strSelectionPath = value;
            }
        }

        //------------------------------------
        public CTreeViewNodeKeeper(TreeView view)
        {
            List<string> lst = new List<string>();
            TreeNode node = view.SelectedNode;
            while ( node != null )
            {
                lst.Add(node.Text);
                node = node.Parent;
            }
            lst.Reverse();
            m_strSelectionPath = lst.ToArray();
            KeepNodes(view.Nodes);
        }

        //------------------------------------
        public void KeepNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.IsExpanded && !ContainsKey(node.Text))
                {
                    CTreeViewNodeKeeper keep = new CTreeViewNodeKeeper();
                    Add(node.Text, keep);
                    keep.KeepNodes(node.Nodes);
                }
            }
        }

        //------------------------------------
        public void Apply(TreeView view)
        {
            view.SuspendDrawing();
            Apply(view.Nodes);
            view.ResumeDrawing();
            TreeNodeCollection nodes = view.Nodes;
            foreach (string strText in m_strSelectionPath)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Text == strText)
                    {
                        view.SelectedNode = node;
                        node.Expand();
                        nodes = node.Nodes;
                        break;
                    }
                }
            }

        }

        //------------------------------------
        public void Apply(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                CTreeViewNodeKeeper keeper = null;
                if (TryGetValue(node.Text, out keeper))
                {
                    node.Expand();
                    keeper.Apply(node.Nodes);
                }
            }
        }
    }
}
