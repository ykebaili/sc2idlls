using System;
using System.Windows.Forms;
using System.Collections;

using sc2i.expression;

namespace sc2i.formulaire.win32
{
	/// <summary>
	/// Description résumée de CRemplisseurArbreStructureDynamique.
	/// </summary>
	/// 

	public delegate void AfterCreateNode ( TreeNode node, CDefinitionProprieteDynamique def );
	public delegate void BeforeAddNode ( CDefinitionProprieteDynamique def, TreeNode nodeParent, ref bool bAjouter );
	public delegate void AfterCreateNodesFils ( TreeNode nodeParent );

	public class CRemplisseurArbreStructureDynamique
	{
		private TreeView m_arbre;
		private CObjetPourSousProprietes m_objetPourSousProprietesRacine;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes;
		private AfterCreateNode m_afterCreateNode = null;
		public AfterCreateNodesFils m_afterCreateNodesFils = null;
		private CDefinitionProprieteDynamique m_definitionRacineDeChamps;

		public CRemplisseurArbreStructureDynamique(
			TreeView arbre,
			CObjetPourSousProprietes objetPourSousProprietesRacine,
			IFournisseurProprietesDynamiques fournisseur,
			AfterCreateNode afterCreateNode,
			CDefinitionProprieteDynamique definitionRacineDeChamps
			)
		{
			m_arbre = arbre;
            m_objetPourSousProprietesRacine = objetPourSousProprietesRacine;
			m_fournisseurProprietes = fournisseur;
			m_afterCreateNode = afterCreateNode;
			m_arbre.AfterExpand += new TreeViewEventHandler ( m_arbre_AfterExpand );
			m_definitionRacineDeChamps = definitionRacineDeChamps;
		}

		///////////////////////////////////////////////////////////////////////////
		public void FillArbreChamps()
		{
			m_arbre.Nodes.Clear();
            if (m_objetPourSousProprietesRacine == null || m_fournisseurProprietes == null)
				return;
            if (m_objetPourSousProprietesRacine != null)
			{
                CreateNodesType(m_objetPourSousProprietesRacine, m_arbre.Nodes, null);
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public void FillArbreChamps ( TreeNode nodeParent )
		{
			nodeParent.Nodes.Clear();
            if (m_objetPourSousProprietesRacine == null || m_fournisseurProprietes == null)
				return;
            if (m_objetPourSousProprietesRacine != null)
			{
                CreateNodesType(m_objetPourSousProprietesRacine, nodeParent.Nodes, nodeParent);
			}
		}

		/////////////////////////////////////////////////////////////////////////////
		private void CreateNodesType ( CObjetPourSousProprietes objetPourSousProprietes, TreeNodeCollection nodes, TreeNode nodeParent )//, Hashtable tablesBranche, Hashtable tableTotale )
		{
			CDefinitionProprieteDynamique[] defs = null;
			CDefinitionProprieteDynamique defParente = null;
			if ( nodeParent != null && nodeParent.Tag is CDefinitionProprieteDynamique )
				defParente = (CDefinitionProprieteDynamique)nodeParent.Tag;
			if ( nodeParent == null )
				defParente = m_definitionRacineDeChamps;
			defs = m_fournisseurProprietes.GetDefinitionsChamps(objetPourSousProprietes,defParente);
			CreateNodes ( defs, nodes, nodeParent );
		}

		public event BeforeAddNode m_beforeAddNode;
		/////////////////////////////////////////////////////////////////////////////
		private void CreateNodes ( CDefinitionProprieteDynamique[] defs, TreeNodeCollection nodes, TreeNode nodeParent )
		{
			Hashtable tableNodeToRubrique = new Hashtable();
			foreach ( CDefinitionProprieteDynamique def in defs )
			{
				bool bAjouter = true;
				if ( m_beforeAddNode != null )
					m_beforeAddNode ( def, nodeParent, ref bAjouter );
				if ( bAjouter )
				{
					TreeNodeCollection listeNodes = nodes;
					if ( def.Rubrique != "" )
					{
						TreeNode nodeRubrique = (TreeNode)tableNodeToRubrique[def.Rubrique];
						if ( nodeRubrique == null )
						{
							nodeRubrique = new TreeNode(def.Rubrique, 3, 3);
							listeNodes.Insert ( 0,  nodeRubrique );
							tableNodeToRubrique[def.Rubrique]=nodeRubrique;
						}
						listeNodes = nodeRubrique.Nodes;
					}
					string strNom = def.Nom;
					TreeNode nodeFils = listeNodes.Add ( strNom );
					nodeFils.Tag = def;
					
					bool bHasSubs = def.HasSubProperties;//m_fournisseurProprietes.GetDefinitionsChamps(def.TypeDonnee.TypeDotNetNatif, 0).Length > 0;
					if ( def.TypeDonnee.IsArrayOfTypeNatif )
						nodeFils.ImageIndex = bHasSubs?1:2;
					else
						nodeFils.ImageIndex = bHasSubs?0:2;
					nodeFils.SelectedImageIndex = nodeFils.ImageIndex;
					if ( bHasSubs )
					{
						TreeNode nodeBidon = nodeFils.Nodes.Add ( "" );
						nodeBidon.Tag = null;
					}
					
					if ( m_afterCreateNode != null )
						m_afterCreateNode ( nodeFils, def );
				}
			}
			if ( m_afterCreateNodesFils != null && nodeParent != null)
				m_afterCreateNodesFils ( nodeParent );
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public void FillNodesFils ( TreeNode node )
		{
			object tag = node.Tag;
			if ( node.Tag is CDefinitionProprieteDynamique )
			{
				CDefinitionProprieteDynamique def = (CDefinitionProprieteDynamique)node.Tag;
				if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null )
				{
					node.Nodes.Clear();
					if (def!=null)
						CreateNodesType ( def.TypeDonnee.TypeDotNetNatif, node.Nodes, node);
					node.Expand();
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public void RemplitFilsSiBesoin(TreeNode node)
		{
			FillNodesFils ( node );
		}
		
		////////////////////////////////////////////////////////////////////////////////////////
		private void m_arbre_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			RemplitFilsSiBesoin ( node );
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietesRacine;
			}
			set
			{
                m_objetPourSousProprietesRacine = value;
			}
		}

	}
}
