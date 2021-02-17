using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Linq;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.process;
using sc2i.data;
using sc2i.win32.common;
using sc2i.win32.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
    public class CFormEditActionClonerEntite : sc2i.win32.process.CFormEditActionFonction
	{
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private SplitContainer m_splitContainer;
        private CControleEditeFormule m_txtFormuleObjetSource;
        private Label label1;
        private TreeView m_wndArbreProprietes;
		private System.ComponentModel.IContainer components = null;
        private COptionsClonageEntite m_dicPropParType = null;
        private ImageList m_imageList;

        private struct CTagNode
        {
            public Type TypeAssocie;
            public CDefinitionProprieteDynamique Definition;
            public CTagNode(Type typeAssocie, CDefinitionProprieteDynamique def)
            {
                TypeAssocie = typeAssocie;
                Definition = def;
            }

            public string GetKey()
            {
                return TypeAssocie.ToString()+"/"+(Definition != null?Definition.NomPropriete:"");
            }
        }

        private Dictionary<string, List<TreeNode>> m_dicIdDefToNode = new Dictionary<string, List<TreeNode>>();

		public CFormEditActionClonerEntite()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionClonerEntite), typeof(CFormEditActionClonerEntite));
		}


		public CActionClonerEntite ActionClonerEntite
		{
			get
			{
				return (CActionClonerEntite)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditActionClonerEntite));
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_wndArbreProprietes = new System.Windows.Forms.TreeView();
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.m_txtFormuleObjetSource = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.panel2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Text = "Store result in :|112";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_splitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(788, 403);
            this.panel2.TabIndex = 2;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_wndArbreProprietes);
            this.m_splitContainer.Panel1.Controls.Add(this.m_txtFormuleObjetSource);
            this.m_splitContainer.Panel1.Controls.Add(this.label1);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(788, 403);
            this.m_splitContainer.SplitterDistance = 573;
            this.m_splitContainer.TabIndex = 6;
            // 
            // m_wndArbreProprietes
            // 
            this.m_wndArbreProprietes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndArbreProprietes.CheckBoxes = true;
            this.m_wndArbreProprietes.ImageIndex = 0;
            this.m_wndArbreProprietes.ImageList = this.m_imageList;
            this.m_wndArbreProprietes.Location = new System.Drawing.Point(6, 88);
            this.m_wndArbreProprietes.Name = "m_wndArbreProprietes";
            this.m_wndArbreProprietes.SelectedImageIndex = 0;
            this.m_wndArbreProprietes.Size = new System.Drawing.Size(553, 307);
            this.m_wndArbreProprietes.TabIndex = 7;
            this.m_wndArbreProprietes.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.m_wndArbreProprietes_AfterCheck);
            this.m_wndArbreProprietes.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_wndArbreProprietes_BeforeExpand);
            this.m_wndArbreProprietes.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_wndArbreProprietes_BeforeCheck);
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageList.Images.SetKeyName(0, "dossier.gif");
            this.m_imageList.Images.SetKeyName(1, "Table.gif");
            // 
            // m_txtFormuleObjetSource
            // 
            this.m_txtFormuleObjetSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleObjetSource.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleObjetSource.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleObjetSource.Formule = null;
            this.m_txtFormuleObjetSource.Location = new System.Drawing.Point(6, 22);
            this.m_txtFormuleObjetSource.LockEdition = false;
            this.m_txtFormuleObjetSource.Name = "m_txtFormuleObjetSource";
            this.m_txtFormuleObjetSource.Size = new System.Drawing.Size(553, 60);
            this.m_txtFormuleObjetSource.TabIndex = 6;
            this.m_txtFormuleObjetSource.Leave += new System.EventHandler(this.m_txtFormuleObjetDestination_Leave);
            this.m_txtFormuleObjetSource.Enter += new System.EventHandler(this.OnEnterTexteFormule);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Entity|208";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(207, 399);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditActionClonerEntite
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(788, 483);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionClonerEntite";
            this.Text = "Clone an entity|20035";
            this.Load += new System.EventHandler(this.CFormEditActionClonerEntite_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();

            m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
            // Init la formule de l'objet source
            m_txtFormuleObjetSource.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            if (ActionClonerEntite.FormuleElementACloner != null)
                m_txtFormuleObjetSource.Text = ActionClonerEntite.FormuleElementACloner.GetString();
            m_dicPropParType = ActionClonerEntite.OptionsClonage.GetClone();

            FillArbreProprietes();

		}

        /// //////////////////////////////////////////
        private void FillArbreProprietes()
        {
            m_dicIdDefToNode.Clear();
            m_wndArbreProprietes.Nodes.Clear();
            if (ActionClonerEntite.FormuleElementACloner == null)
                return;
            CTypeResultatExpression tp = ActionClonerEntite.FormuleElementACloner.TypeDonnee;
            if (!(typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(tp.TypeDotNetNatif))
                || tp.IsArrayOfTypeNatif)
                return;
            Type typePrincipal = tp.TypeDotNetNatif;
            TreeNode node = new TreeNode(DynamicClassAttribute.GetNomConvivial(typePrincipal));
            node.ImageIndex = 1;
            m_wndArbreProprietes.Nodes.Add(node);
            node.Tag = new CTagNode(typePrincipal, 
                new CDefinitionProprieteDynamiqueThis(
                    new CTypeResultatExpression(typePrincipal, false), true,false));
            CreateChilds(node);
        }

        /// //////////////////////////////////////////
        private bool IsCheckByDic ( TreeNode node )
        {
            CTagNode? tag = node.Tag as CTagNode?;
            if ( tag == null || tag.Value.Definition == null || node.Parent == null)
                return false;
            return m_dicPropParType.Contains ( tag.Value.TypeAssocie, tag.Value.Definition );
        }

        /// //////////////////////////////////////////
        private void CreateChilds(TreeNode node)
        {
            CTagNode? tag = node.Tag as CTagNode?;
            if (tag == null)
                return;

            Dictionary<string, TreeNode> dicFolders = new Dictionary<string, TreeNode>();

            Type tpParent = tag.Value.Definition.TypeDonnee.TypeDotNetNatif;

            IEnumerable<CDefinitionProprieteDynamique> defs =
                from def in new CFournisseurPropDynStd(false).GetDefinitionsChamps(tpParent)
                where def.GetDefinitionInverse(tpParent) != null
                select def;
            foreach ( CDefinitionProprieteDynamique def in defs )
            {
                CDefinitionProprieteDynamiqueDotNet defDotNet = def as CDefinitionProprieteDynamiqueDotNet;
                if (defDotNet != null)
                {
                    PropertyInfo info = tpParent.GetProperty(defDotNet.NomProprieteSansCleTypeChamp);
                    if (info != null && info.GetCustomAttributes(typeof(NonCloneableAttribute), true).Length != 0)
                        continue;
                }
                        
                TreeNode nodeFils = new TreeNode ( def.Nom );
                CTagNode tagFils = new CTagNode(tpParent, def);
                nodeFils.Tag = tagFils;
                TreeNodeCollection nodes = node.Nodes;
                if (def.Rubrique.Trim() != "")
                {
                    TreeNode parent = null;
                    if (!dicFolders.TryGetValue(def.Rubrique.Trim().ToUpper(), out parent))
                    {
                        parent = new TreeNode(def.Rubrique.Trim());
                        parent.Tag = node.Tag;
                        dicFolders[def.Rubrique.Trim().ToUpper()] = parent;
                        node.Nodes.Add(parent);
                        parent.ImageIndex = 0;
                    }
                    nodes = parent.Nodes;
                }
                nodes.Add ( nodeFils );
                nodeFils.ImageIndex = 1;
                List<TreeNode> lst = null;
                if ( !m_dicIdDefToNode.TryGetValue ( tagFils.GetKey(), out lst ) )
                {
                    lst = new List<TreeNode>();
                    m_dicIdDefToNode[tagFils.GetKey()] = lst;
                }
                lst.Add ( nodeFils );
                m_bInCheck = true;
                nodeFils.Checked = IsCheckByDic ( nodeFils );
                m_bInCheck = false;
                nodeFils.Nodes.Add ( new TreeNode());
            }
        }

        //----------------------------------------------------------------------------------
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			
            CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression(ObjetEdite.Process, typeof(CProcess));
            CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
            // Sauve la formule retournant l'objet source
            result = analyseur.AnalyseChaine(m_txtFormuleObjetSource.Text);
            if (!result)
                return result;
            ActionClonerEntite.FormuleElementACloner = (C2iExpression)result.Data;

            ActionClonerEntite.OptionsClonage = m_dicPropParType.GetClone() ;
           
			return result;
		}

        //----------------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormule != null )
				m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}
        
        //----------------------------------------------------------------------------------
        private void CFormEditActionClonerEntite_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }


        //--------------------------------------------------------------------------------
        private CControleEditeFormule m_txtFormule = null;
        private void OnEnterTexteFormule(object sender, System.EventArgs e)
        {
            if (m_txtFormule != null)
                m_txtFormule.BackColor = Color.White;
            if (sender is CControleEditeFormule)
            {
                m_txtFormule = (CControleEditeFormule)sender;
                m_txtFormule.BackColor = Color.LightGreen;
            }
        }

        //----------------------------------------------------------------------------------
        private void m_txtFormuleObjetDestination_Leave(object sender, EventArgs e)
        {
            CResultAErreur result = MAJ_Champs();
            FillArbreProprietes();
        }

        private void m_wndArbreProprietes_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null )
            {
                node.Nodes.Clear();
                CreateChilds ( node );
            }
        }

        //---------------------------------------------------------------------
        private bool m_bInCheck = false;
        private void m_wndArbreProprietes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if ( m_bInCheck )
                return;
            if ( e.Node.Parent == null )
                return;
            m_bInCheck = true;
            CTagNode? tag = e.Node.Tag as CTagNode?;
            if ( tag == null )
                    return;
            List<TreeNode> lst = null;
            if ( m_dicIdDefToNode.TryGetValue(tag.Value.GetKey(), out lst ))
            {
                foreach ( TreeNode node in lst )
                    if ( node != e.Node )
                        node.Checked = e.Node.Checked;
            }
            if ( e.Node.Checked )
                m_dicPropParType.AddDefinition ( tag.Value.TypeAssocie, tag.Value.Definition );
            else
                m_dicPropParType.RemoveDefinition(tag.Value.TypeAssocie, tag.Value.Definition);
            m_bInCheck = false;
        }

        private void m_wndArbreProprietes_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == 0)
                e.Cancel = true;
        }

        
 

	}
}

