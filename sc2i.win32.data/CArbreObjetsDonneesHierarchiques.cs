using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CArbreObjetsDonneesHierarchiques.
	/// </summary>
	public class CArbreObjetsDonneesHierarchiques : System.Windows.Forms.TreeView
	{
		protected class CInfoNode
		{
			public bool AreChildsLoaded = false;
			public CObjetDonneeAIdNumeriqueAuto Objet = null;
		}

        private C2iExpression m_formuleLibelle = null;
		protected Type m_typeObjets;
		protected string m_strProprieteListeFils = "";
		protected string m_strChampParent = "";
        private string m_strDefProprieteAffichee = "";
		protected string m_strProprieteAffichee = "";
		protected CObjetDonneeAIdNumeriqueAuto m_objetRoot = null;
		
		//Si vrai, il est possible de sélectionner les fils des éléments appartenant au filtre
		protected bool m_bAutoriserFilsDesAutorises = true;
		
		//Filtre à appliquer aux fils et aux éléments sélectionnés
		private CFiltreData m_filtre = null;
		
		//Filtre à appliquer à la racine
		private CFiltreData m_filtreRacine = null;

        private bool m_bAddRootForAll = false;
        private string m_strRootLabel = "Root";


		private System.Drawing.Color m_couleurNonSelectionnable = Color.DarkGray;

		/// <summary>
		///Code systeme de l'élément->bool : indique qu'un élément peut être affiché.
		///Cette table est utilisée en cas d'application de filtre
		///Si l'arbre affiche les données avec un filtre,
		///le système commence par récuperer tous les éléments correspondant
		/// à ce filtre, et ajoute leur code hiérarchique et le code de leur parent 
		/// dans le m_tableCodesVisibles, puis
		/// lorsqu'un élément est suceptible d'être vu, on regarde dans la 
		/// table s'il y a le code de l'élément
		/// s'il y est, on affiche l'élément, sinon, non
        /// Utilisé en cas d'objet hiérarchique à code
		/// </summary>
		private Hashtable m_tableCodesVisibles = null;

        /// <summary>
        /// tableaux des ids visibles et valides
        /// Utilisés en cas d'objetHierarchique sans code
        /// </summary>
        private HashSet<int> m_tableIdsElementsVisibles = new HashSet<int>();
        private HashSet<int> m_tableIdsElementsSelectionnables = new HashSet<int>();

		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		public CArbreObjetsDonneesHierarchiques()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // CArbreObjetsDonneesHierarchiques
            // 
            this.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.CArbreObjetsDonneesHierarchiques_BeforeCheck);
            this.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.CArbreObjetsDonneesHierarchiques_AfterExpand);
            this.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.CArbreObjetsDonneesHierarchiques_ItemDrag);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Initialise l'arbre
		/// </summary>
		/// <param name="typeObjets">Type des objets contenus dans l'abre</param>
		/// <param name="strProprieteListeFils">Propriete à appeler sur un élément pour obtenir ses fils</param>
		/// <param name="strChampParent">Champ de la table contenant l'id du parent</param>
		/// <param name="strProprieteAffichee">Propriete affichée dans l'abre</param>
		/// <returns></returns>
		public CResultAErreur Init(
			Type typeObjets,
			string strProprieteListeFils,
			string strChampParent,
			string strProprieteAffichee,
			CFiltreData filtre,
			CFiltreData filtreRacine
			)
		{
			m_objetRoot = null;
			m_typeObjets = typeObjets;
			m_strProprieteListeFils = strProprieteListeFils;
			m_strChampParent = strChampParent;
            m_strDefProprieteAffichee = strProprieteAffichee;
            SetupFormuleLibelle();
			m_filtre = null;
			m_filtreRacine = filtreRacine;
			if (filtre != null &&
				filtre.HasFiltre)
				m_filtre = filtre;
			PrepareFiltre();
			Nodes.Clear();
            TreeNode nodeRoot = null;

            if (m_bAddRootForAll)
            {
                nodeRoot = new TreeNode();
                FillNode(nodeRoot, null);
                nodeRoot.Text = m_strRootLabel;
                Nodes.Add(nodeRoot);
            }
            FillNodes(nodeRoot, null);
			
			return CResultAErreur.True;
		}

        //----------------------------------------------------------------------
        private void SetupFormuleLibelle()
        {
            string[] strProps = m_strDefProprieteAffichee.Split('|');
            m_strProprieteAffichee = strProps[0];
            if ( strProps.Length > 1 && CFormulesGlobaleParametrage.GetDefinition(strProps[1]) != null)
                m_formuleLibelle = CFormulesGlobaleParametrage.GetFormule(CSc2iWin32DataClient.ContexteCourant.IdSession, strProps[1]);
            else
                m_formuleLibelle = null;
        }

        //----------------------------------------------------------------------
        public bool AddRootForAll
        {
            get
            {
                return m_bAddRootForAll;
            }
            set{
                m_bAddRootForAll = value;
            }
        }

        public string RootLabel
        {
            get
            {
                return m_strRootLabel;
            }
            set
            {
                m_strRootLabel = value;
            }
        }

        public bool AutoriserFilsDesAutorises
        {
            get
            {
                return m_bAutoriserFilsDesAutorises;
            }
            set
            {
                m_bAutoriserFilsDesAutorises = value;
            }
        }

		/// <summary>
		/// Enregistre dans la table m_tableCodesVisibles
		/// les ids des éléments qui peuvent être affichés
		/// </summary>
		private void PrepareFiltre()
		{
			if (m_filtre == null || !m_filtre.HasFiltre)
			{
				m_tableCodesVisibles = null;
                m_tableIdsElementsVisibles = null;
				return;
			}
			m_tableCodesVisibles = new Hashtable (2000, 0.5f);
			CListeObjetsDonnees liste = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, m_typeObjets);
			liste.Filtre = m_filtre;
			Hashtable tableTmp = new Hashtable();
            if (typeof(IObjetHierarchiqueACodeHierarchique).IsAssignableFrom(m_typeObjets))
            {
                foreach (IObjetHierarchiqueACodeHierarchique objet in liste)
                {
                    string strCode = objet.CodeSystemeComplet;
                    bool bForceTrue = true;
                    while (strCode.Length > 0)
                    {
                        if (bForceTrue)
                            m_tableCodesVisibles[strCode] = true;
                        else
                            if (!m_tableCodesVisibles.Contains(strCode))
                                m_tableCodesVisibles[strCode] = false;
                        bForceTrue = false;
                        strCode = strCode.Substring(0, strCode.Length - objet.NbCarsParNiveau);
                    }
                }
            }
            else
            {
                m_tableIdsElementsSelectionnables.Clear();
                string strChampParent = null;
                foreach (IObjetDonneeAutoReference objet in liste)
                {
                    m_tableIdsElementsVisibles.Add(objet.Id);
                    m_tableIdsElementsSelectionnables.Add(objet.Id);
                    strChampParent = objet.ChampParent;
                }
                //lecture des parents
                if (strChampParent != null)
                {
                    int nCount = liste.Count;
                    CStructureTable structure = CStructureTable.GetStructure(m_typeObjets);
                    CInfoRelation relToParent = null;
                    foreach (CInfoRelation rel in structure.RelationsParentes)
                    {
                        if (rel.ChampsFille[0] == strChampParent && rel.TableParente == rel.TableFille)
                            relToParent = rel;
                    }

                    CListeObjetsDonnees lst = liste.GetDependances(relToParent);
                    while (lst.Count != 0)
                    {
                        foreach (IObjetDonneeAutoReference objet in lst)
                        {
                            m_tableIdsElementsVisibles.Add(objet.Id);
                        }
                        lst = lst.GetDependances(relToParent);
                    }
                }
            }
		}

		/// <summary>
		/// Initialise l'arbre
		/// </summary>
		/// <param name="typeObjets">Type des objets contenus dans l'abre</param>
		/// <param name="strProprieteListeFils">Propriete à appeler sur un élément pour obtenir ses fils</param>
		/// <param name="strChampParent">Champ de la table contenant l'id du parent</param>
		/// <param name="strProprieteAffichee">Propriete affichée dans l'abre</param>
		/// <returns></returns>
		public CResultAErreur Init(
			CObjetDonneeAIdNumeriqueAuto objetRoot,
			string strProprieteListeFils,
			string strChampParent,
			string strProprieteAffichee,
			CFiltreData filtre)
		{
			m_typeObjets = objetRoot.GetType();;
			m_objetRoot = objetRoot;
			m_strProprieteListeFils = strProprieteListeFils;
			m_strChampParent = strChampParent;
			m_strDefProprieteAffichee = strProprieteAffichee;
            SetupFormuleLibelle();
			if (filtre != null &&
				filtre.HasFiltre)
				m_filtre = filtre;
			PrepareFiltre();
			Nodes.Clear();
			FillNodes ( null, null );
			return CResultAErreur.True;
		}

		/// <summary>
		/// ////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="nodes"></param>
		/// <param name="objetParent"></param>
		/// <param name="nNbSousNiveauToFill"></param>
		protected virtual void FillNodes ( TreeNode nodeParent, CObjetDonneeAIdNumeriqueAuto objetParent )
		{
			TreeNodeCollection nodes = null;
			if ( nodeParent == null )
				nodes = Nodes;
			else
			{
				if ( ((CInfoNode)nodeParent.Tag).AreChildsLoaded )
					return;
				nodes = nodeParent.Nodes;
				((CInfoNode)nodeParent.Tag).AreChildsLoaded = true;
			}
			nodes.Clear();
			CListeObjetsDonnees listeFils = null;
			if (objetParent == null)
			{
				CContexteDonnee contexte = CSc2iWin32DataClient.ContexteCourant;
				if (m_objetRoot != null)
					contexte = m_objetRoot.ContexteDonnee;
				listeFils = new CListeObjetsDonnees(contexte, m_typeObjets);
				if (m_objetRoot != null)
					listeFils.Filtre = new CFiltreData(m_objetRoot.GetChampId() + "=@1", m_objetRoot.Id);
				else
					listeFils.Filtre = CFiltreData.GetAndFiltre(m_filtreRacine, new CFiltreData(m_strChampParent + " is null"));
			}
			else
			{
				listeFils = (CListeObjetsDonnees)CInterpreteurTextePropriete.GetValue(objetParent, m_strProprieteListeFils);
				if ( !m_bAutoriserFilsDesAutorises || !IsInFiltre ( objetParent ) )
					listeFils.Filtre = CFiltreData.GetAndFiltre(listeFils.Filtre, m_filtreRacine);
			}
			foreach ( CObjetDonneeAIdNumeriqueAuto objetFils in listeFils )
			{
				bool bVoir = IsVisible(objetFils);
				/*if ( m_tableCodesVisibles != null && objetFils is IObjetHierarchiqueACodeHierarchique )
					bVoir = m_tableCodesVisibles.Contains(((IObjetHierarchiqueACodeHierarchique)objetFils).CodeSystemeComplet);*/
				if (m_bAutoriserFilsDesAutorises && !bVoir)
				{
					if (objetParent != null && IsInFiltre(objetParent))
					{
                        if ( objetFils is IObjetHierarchiqueACodeHierarchique && m_tableCodesVisibles != null)
						    m_tableCodesVisibles.Add(((IObjetHierarchiqueACodeHierarchique)objetFils).CodeSystemeComplet, true);
						bVoir = true;
					}
				}
				if (bVoir)
				{
					TreeNode node = new TreeNode();
					FillNode(node, objetFils);
					
					nodes.Add(node);
					node.Nodes.Add(new TreeNode("__"));//Noeud bidon
					if (!IsInFiltre(objetFils))
					{
						node.ForeColor = ForeColorNonSelectionnable;
						node.Expand();
					}
				}
			}
		}

        /// ////////////////////////////////////////////////////////////
        private bool IsVisible(IObjetDonneeAIdNumerique objet)
        {
            if (objet is IObjetHierarchiqueACodeHierarchique)
            {
                if (m_tableCodesVisibles == null)
                    return true;
                return m_tableCodesVisibles.Contains(((IObjetHierarchiqueACodeHierarchique)objet).CodeSystemeComplet);
            }
            if (m_tableIdsElementsVisibles == null)
                return true;

            return m_tableIdsElementsVisibles.Contains(objet.Id);
        }

		/// ////////////////////////////////////////////////////////////
        public void FillNode(TreeNode node, CObjetDonneeAIdNumeriqueAuto objet)
        {
            if (objet != null)
            {
                string strText = "";
                if (m_formuleLibelle != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(objet);
                    CResultAErreur result = m_formuleLibelle.Eval(ctx);
                    if (result && result.Data != null)
                        strText = result.Data.ToString();
                }
                if (strText.Length == 0)
                    strText = CInterpreteurTextePropriete.GetStringValue(objet, m_strProprieteAffichee, "Non def");
                node.Text = strText;
            }
            CInfoNode info;
            if (node.Tag is CInfoNode)
                info = (CInfoNode)node.Tag;
            else
                info = new CInfoNode();
            info.Objet = objet;
            node.Tag = info;
        }

		/// ////////////////////////////////////////////////////////////
		public CObjetDonnee GetObjetInNode ( TreeNode node )
		{
			if ( node == null )
				return null;
			return ((CInfoNode)node.Tag).Objet;
		}

		/// ////////////////////////////////////////////////////////////
		protected void CArbreObjetsDonneesHierarchiques_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			CInfoNode info = (CInfoNode)node.Tag;
			if ( !info.AreChildsLoaded )
				FillNodes ( node, info.Objet );
		}

		/// ////////////////////////////////////////////////////////////
		public CObjetDonnee ElementSelectionne
		{
			get
			{
				TreeNode node = SelectedNode;
				if ( node == null )
					return null;
				if ( node.Tag == null )
					return null;
				CInfoNode info = (CInfoNode)node.Tag;
				if (info != null && IsInFiltre(info.Objet))
					return info.Objet;
				else
					return null;
			}
			set
			{
				if ( value == null )
					SelectedNode = null;
				else
				{
					TreeNode node = GetNodeFor ( value );
					SelectedNode = node;
					if ( node != null )
						node.EnsureVisible();
				}
			}
		}

		/// //////////////////////////////////////////////
		public TreeNode GetNodeFor ( CObjetDonnee objet )
		{
			return GetNodeFor ( Nodes, objet );
		}

		/// //////////////////////////////////////////////
		public TreeNode GetNodeFor ( TreeNodeCollection nodes, CObjetDonnee objet )
		{
			//Recherche dans les noeuds de la collect en premier (pour ne pas charger trop de noeuds)
			foreach ( TreeNode node in nodes )
			{
				if ( ((CInfoNode)node.Tag).Objet == objet )
					return node;
			}
			foreach ( TreeNode node in nodes )
			{
				FillNodes ( node, ((CInfoNode)node.Tag).Objet);
				TreeNode result = GetNodeFor ( node.Nodes, objet );
				if ( result != null )
					return result;
			}
			return null;
		}

		/// //////////////////////////////////////////////
		public bool IsInFiltre(IObjetDonneeAIdNumerique objet)
		{
			if (m_tableCodesVisibles == null)//Pas de filtre
				return true;
            if (m_tableCodesVisibles != null && objet is IObjetHierarchiqueACodeHierarchique)
            {
                string strCode = ((IObjetHierarchiqueACodeHierarchique)objet).CodeSystemeComplet;
                object val = m_tableCodesVisibles[strCode];
                if (val != null && val is bool && (bool)val)
                    return true;
            }
            else
            {
                return m_tableIdsElementsSelectionnables.Contains(objet.Id);
            }

			return false;
		}

		/// //////////////////////////////////////////////
		private void CArbreObjetsDonneesHierarchiques_BeforeCheck(object sender, TreeViewCancelEventArgs e)
		{
			if (!IsInFiltre(((CInfoNode)e.Node.Tag).Objet))
				e.Cancel = true;
		}

		/// //////////////////////////////////////////////
		private void FillCheckedElements(TreeNodeCollection nodes, ArrayList lst)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Checked)
					lst.Add(((CInfoNode)node.Tag).Objet);
				FillCheckedElements(node.Nodes, lst);
			}
		}

		////////////////////////////////////////////////
		public Color ForeColorNonSelectionnable
		{
			get
			{
				return m_couleurNonSelectionnable;
			}
			set
			{
				m_couleurNonSelectionnable = value;
			}
		}

		////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des élements checkés
		/// </summary>
		public CObjetDonnee[] ElementsSelectionnes
		{
			get
			{
				//Trouve les noeuds sélectionnes
				ArrayList lst = new ArrayList();
				FillCheckedElements(Nodes, lst);
				return (CObjetDonnee[])lst.ToArray(typeof(CObjetDonnee));
			}
            set
            {
                foreach (CObjetDonnee objet in ElementsSelectionnes)
                    SetChecked(objet, false);
                if (value != null)
                    foreach (CObjetDonnee obj in value)
                        SetChecked(obj, true);
            }
		}

		///////////////////////////////////////////////////////////////
		public void SetChecked(CObjetDonnee objet, bool bChecked)
		{
			TreeNode node = GetNodeFor(objet);
			if (node != null)
				node.Checked = true;
		}

		///////////////////////////////////////////////////////////////
		public void EnsureVisible(CObjetDonnee objet)
		{
			TreeNode node = GetNodeFor(objet);
			TreeNode parent = node.Parent;
			while (parent != null)
			{
				parent.Expand();
				parent = parent.Parent;
			}
		}

        ///////////////////////////////////////////////////////////////
        private void CArbreObjetsDonneesHierarchiques_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;
            CInfoNode info = node != null?node.Tag as CInfoNode:null;
            CObjetDonneeAIdNumerique objet = info != null?info.Objet:null;
            if ( objet != null )
            {
                DoDragDrop ( new CReferenceObjetDonneeDragDropData(objet),
                    DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Copy );
            }
        }

		


	}
}
