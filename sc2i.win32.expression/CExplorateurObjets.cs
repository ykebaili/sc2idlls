using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using System.Drawing;
using System.Collections;
using sc2i.expression;

namespace sc2i.win32.expression
{
    public class CExplorateurObjets : TreeView
    {
        private class CTreeViewData
        {
            private object m_valeur;
            private CDefinitionProprieteDynamique m_propriete;
            private object m_source;
            private int? m_nIndex = null;

            public CTreeViewData(object source, CDefinitionProprieteDynamique definition, int? nIndex)
            {
                m_source = source;
                m_propriete = definition;
                m_nIndex = nIndex;
            }

            public object Valeur
            {
                get
                {
                    if (m_nIndex != null)
                        return m_source;
                    if (m_valeur == null)
                    {
                        CResultAErreur result = CInterpreteurProprieteDynamique.GetValue(m_source, m_propriete);
                        if (result)
                            m_valeur = result.Data;
                        else
                            m_valeur = DBNull.Value;
                    }
                    return m_valeur == DBNull.Value ? null : m_valeur;
                }
            }

            public CDefinitionProprieteDynamique Propriete
            {
                get
                {
                    return m_propriete;
                }
            }

            public int? Index
            {
                get
                {
                    return m_nIndex;
                }
            }
        }


        private IFournisseurProprietesDynamiques m_fournisseur = null;
        private object m_objet = null;
        private int m_nFieldSize = 100;

        //------------------------------------
        public CExplorateurObjets()
            : base()
        {
            InitializeComponent();
        }

        //------------------------------------
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CExplorateurObjets
            // 
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.LineColor = System.Drawing.Color.Black;
            this.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.CExplorateurObjets_DrawNode);
            this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.CExplorateurObjets_BeforeExpand);
            this.ResumeLayout(false);

        }

        //------------------------------------
        public void Init(object objet, IFournisseurProprietesDynamiques fournisseur)
        {
            BeginUpdate();
            Nodes.Clear();
            m_fournisseur = fournisseur;
            m_objet = objet;

            FillNodes(objet, Nodes);
            EndUpdate();
        }

        private void FillNodes(object objet, TreeNodeCollection nodes)
        {
            BeginUpdate();
            CObjetPourSousProprietes sp = new CObjetPourSousProprietes(objet);
            List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>(m_fournisseur.GetDefinitionsChamps(sp));
            lstDefs.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            foreach (CDefinitionProprieteDynamique def in lstDefs)
            {
                TreeNode node = new TreeNode();
                FillNode(node, objet, def, null);
                nodes.Add(node);
            }
            EndUpdate();
        }

        private void FillNode(TreeNode node, object objet, CDefinitionProprieteDynamique def, int? nIndex)
        {
            CTreeViewData data = new CTreeViewData(objet, def, nIndex);
            if (def != null)
                node.Text = def.Nom + (nIndex == null ? "" : " " + nIndex.Value);
            node.Tag = data;
            CTypeResultatExpression tp = def.TypeDonnee;
            if (tp.IsArrayOfTypeNatif || m_fournisseur.GetDefinitionsChamps(tp.TypeDotNetNatif).Length > 0)
                node.Nodes.Add(new TreeNode());
        }



        private void CExplorateurObjets_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle rctTotal = Rectangle.Intersect(ClientRectangle,
                new Rectangle(e.Bounds.Left, e.Bounds.Top, Size.Width, e.Bounds.Height));

            Rectangle rctText = new Rectangle(rctTotal.Left, rctTotal.Top,
                m_nFieldSize, rctTotal.Height);
            e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, rctText);

            CTreeViewData data = e.Node.Tag as CTreeViewData;
            if (data != null)
            {
                if (data.Valeur != null)
                {
                    object val = data.Valeur;
                    if (e.Node.Nodes.Count > 0)//complexe
                        val = DescriptionFieldAttribute.GetDescription(val);
                    Rectangle rct = new Rectangle(rctText.Right, rctText.Top, rctTotal.Width - rctText.Width, rctTotal.Height);
                    StringFormat format = new StringFormat();
                    e.Graphics.DrawString(val.ToString(), Font, Brushes.Black, rct, format);
                    format.Dispose();
                }
            }
            e.Graphics.DrawLine(Pens.LightGray, 0, rctTotal.Top , Size.Width, rctTotal.Top);
        }

        private void CExplorateurObjets_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
            {
                e.Node.Nodes.Clear();
                CTreeViewData data = e.Node.Tag as CTreeViewData;
                if (data != null)
                {
                    object valeur = data.Valeur;
                    if (!data.Propriete.TypeDonnee.IsArrayOfTypeNatif || data.Index != null)
                    {
                        if (valeur != null)
                        {
                            FillNodes(valeur, e.Node.Nodes);
                        }
                    }
                    else
                    {
                        IEnumerable en = valeur as IEnumerable;
                        if ( en != null )
                        {
                            int nIndex = 0;
                        foreach (object obj in en )
                        {
                            TreeNode node=  new TreeNode();
                            FillNode ( node, obj, data.Propriete, nIndex++);
                            e.Node.Nodes.Add ( node );
                        }
                        }
                    }

                }
            }
        }
    }
}
