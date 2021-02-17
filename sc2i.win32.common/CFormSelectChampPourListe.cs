using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormSelectChampPourListe.
	/// </summary>
	public class CFormSelectChampParentPourStructure : System.Windows.Forms.Form
	{
        private CInfoChampDynamique m_champSel = null;
        private bool m_bMonoSelection = false;
		private Hashtable m_tableSelectionnes = new Hashtable();
		private CInfoStructureDynamique m_structurePrincipale = null;
		private ArrayList m_listeDecoches = new ArrayList();
		
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.TreeView m_arbre;
		private System.Windows.Forms.Label m_lblTable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ImageList m_imageList;
		private System.ComponentModel.IContainer components;

		public CFormSelectChampParentPourStructure()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectChampParentPourStructure));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.m_lblTable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 48);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(251, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 3;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(197, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_arbre
            // 
            this.m_arbre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbre.CheckBoxes = true;
            this.m_arbre.ImageIndex = 0;
            this.m_arbre.ImageList = this.m_imageList;
            this.m_arbre.Location = new System.Drawing.Point(0, 24);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.SelectedImageIndex = 0;
            this.m_arbre.Size = new System.Drawing.Size(488, 376);
            this.m_arbre.TabIndex = 3;
            this.m_arbre.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbre_BeforeExpand);
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
            this.m_arbre.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbre_BeforeCheck);
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.White;
            this.m_imageList.Images.SetKeyName(0, "");
            this.m_imageList.Images.SetKeyName(1, "");
            this.m_imageList.Images.SetKeyName(2, "");
            this.m_imageList.Images.SetKeyName(3, "");
            // 
            // m_lblTable
            // 
            this.m_lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTable.Location = new System.Drawing.Point(100, 0);
            this.m_lblTable.Name = "m_lblTable";
            this.m_lblTable.Size = new System.Drawing.Size(257, 24);
            this.m_lblTable.TabIndex = 4;
            this.m_lblTable.Text = "Nom de la table";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Table|109";
            // 
            // CFormSelectChampParentPourStructure
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 445);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblTable);
            this.Controls.Add(this.m_arbre);
            this.Controls.Add(this.panel1);
            this.Name = "CFormSelectChampParentPourStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add fields|108";
            this.Load += new System.EventHandler(this.CFormSelectChampParentPourStructure_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//-----------------------------------------------------------------------
		private class DefSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				if ( x is CInfoChampDynamique && y is CInfoChampDynamique )
				{
					return ((CInfoChampDynamique)x).NomChamp.CompareTo ( ((CInfoChampDynamique)y).NomChamp);
				}
				return -1;
			}
		}

		//-----------------------------------------------------------------------
		//Retourne vrai si le champ doit apparaitre dans l'arbre
		private bool ShouldHadChamp ( CInfoChampDynamique champ )
		{
			return true;
		}

		private bool ShouldHasFils ( CInfoChampDynamique champ )
		{
			if ( champ.StructureValeur == null )
				return false;
			return true;
		}

		private bool CanCheck ( CInfoChampDynamique champ )
		{
			if ( champ.StructureValeur != null )
				return false;
			return true;
		}

		//-----------------------------------------------------------------------
		private void FillTree ( CInfoStructureDynamique structure, TreeNodeCollection nodes, CInfoChampDynamique defParente )
		{
			ArrayList lst = new ArrayList( );
			lst.AddRange ( structure.Champs );
			lst.Sort ( new DefSorter() );

			//Trouve les catégories
			Hashtable tableCategories = new Hashtable();
			foreach ( CInfoChampDynamique info in lst )
			{
				
				if ( info.Rubrique != "" && ShouldHadChamp ( info ) )
				{
					if ( tableCategories[info.Rubrique] == null )
					{
						TreeNode node = new TreeNode ( info.Rubrique );
						node.ImageIndex = 0;
						node.SelectedImageIndex = 0;
						nodes.Add ( node );
						tableCategories[info.Rubrique] = node;
					}
				}
			}
			foreach ( CInfoChampDynamique info in lst )
			{
				if ( ShouldHadChamp ( info ) )
				{

					TreeNode node = new TreeNode ( info.NomChamp );
					node.ImageIndex = info.StructureValeur!=null?1:2;
					node.SelectedImageIndex = node.ImageIndex;
					TreeNodeCollection nodesParents = nodes;
					if ( info.Rubrique != "" )
					{
						TreeNode nodeParent = (TreeNode)tableCategories[info.Rubrique];
						if ( nodeParent != null )
							nodesParents = nodeParent.Nodes;
					}
					node.Tag = info;
					nodesParents.Add ( node );
					if ( info.StructureValeur != null && ShouldHasFils ( info ))
						node.Nodes.Add("*");
					CInfoChampDynamique newInfo = info.Clone();
					if ( defParente != null )
						newInfo.InsereParent ( defParente );
					if ( m_tableSelectionnes[newInfo.NomPropriete] != null )
						node.Checked = true;
				}
			}
		}

		//-----------------------------------------------------------------------
		private void m_arbre_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if ( node.Tag is CInfoChampDynamique &&
				!CanCheck ( (CInfoChampDynamique)node.Tag) )
				e.Cancel = true;
		}

		//-----------------------------------------------------------------------
		private void GetSelectedFields ( ArrayList lst, TreeNodeCollection nodes, CInfoChampDynamique defParent )
		{
			foreach ( TreeNode node in nodes )
			{
				CInfoChampDynamique defProp = null;
				if ( node.Tag is CInfoChampDynamique )
				{
					defProp = (CInfoChampDynamique)node.Tag;
					defProp = defProp.Clone();
					if ( defParent != null )
						defProp.InsereParent ( defParent );
				}
				else if ( node.Tag == null )//C'est un folder
						defProp = defParent;
				if ( node.Checked && defProp != null && m_tableSelectionnes[defProp.NomPropriete] == null )
				{
					lst.Add ( defProp );
				}
				else if ( !node.Checked && defProp != null )
				{
					//Elle a été décochée !!!
					m_listeDecoches.Add ( defProp );
				}

				GetSelectedFields ( lst, node.Nodes, defProp );
			}
		}

        //-----------------------------------------------------------------------
        public static CInfoChampDynamique SelectPropriete(CInfoStructureDynamique structure)
        {
            CFormSelectChampParentPourStructure form = new CFormSelectChampParentPourStructure();
            form.m_tableSelectionnes.Clear();
            form.m_structurePrincipale = structure;
            form.m_lblTable.Text = form.m_structurePrincipale.NomConvivial;
            form.FillTree(structure, form.m_arbre.Nodes, null);
            form.m_arbre.CheckBoxes = false;
            form.m_bMonoSelection = true;
            CInfoChampDynamique infoSel = null;
            if (form.ShowDialog() == DialogResult.OK)
            {
                infoSel = form.m_champSel;
            }
            form.Dispose();
            return infoSel;
        }

		//-----------------------------------------------------------------------
		public static CInfoChampDynamique[] SelectProprietes ( CInfoStructureDynamique structure, string[] listeSelectionnes, ref CInfoChampDynamique[] lstDecoches )
		{
			CFormSelectChampParentPourStructure form = new CFormSelectChampParentPourStructure();
			form.m_tableSelectionnes.Clear();
			foreach ( string strCol in listeSelectionnes )
				form.m_tableSelectionnes[strCol] = true;
			form.m_structurePrincipale = structure;
			form.m_lblTable.Text = form.m_structurePrincipale.NomConvivial;
			form.FillTree ( structure,form.m_arbre.Nodes, null );
            form.m_arbre.CheckBoxes = true;
            form.m_bMonoSelection = false;
			if ( form.ShowDialog() == DialogResult.OK )
			{
				ArrayList lst = new ArrayList();
				form.GetSelectedFields ( lst, form.m_arbre.Nodes, null );
				lstDecoches = (CInfoChampDynamique[])form.m_listeDecoches.ToArray(typeof(CInfoChampDynamique));
				//Lst contient la liste de ce qui a été coché en plus. Il faut ajouter à ça la
				//Liste de ce qui n'a pas été décoché
				return ( CInfoChampDynamique[] )lst.ToArray ( typeof ( CInfoChampDynamique ) );
			}
			return new CInfoChampDynamique[0];
		}

		//-----------------------------------------------------------------------
		private void m_arbre_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( e.Node.Nodes.Count == 1 && 
				e.Node.Nodes[0].Text == "*")
			{
				CInfoChampDynamique def = (CInfoChampDynamique)e.Node.Tag;
				if ( def == null )
					return;
				e.Node.Nodes.Clear();
				CInfoChampDynamique infoParente = null;
				if ( e.Node.Tag is CInfoChampDynamique )
					infoParente = (CInfoChampDynamique)e.Node.Tag;
				CInfoStructureDynamique newStruct = CInfoStructureDynamique.GetStructure ( def.TypeDonnee, 1 );
				FillTree ( newStruct, e.Node.Nodes, infoParente );
			}
		} 

		//-----------------------------------------------------------------------
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_arbre_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node.Parent == null || !(e.Node.Parent.Tag is CInfoChampDynamique) )
				m_lblTable.Text = m_structurePrincipale.NomConvivial;
			else
				m_lblTable.Text = ((CInfoChampDynamique)e.Node.Parent.Tag).NomPropriete;
            if (e.Node != null && e.Node.Tag is CInfoChampDynamique)
            {
                m_champSel = e.Node.Tag as CInfoChampDynamique;
                TreeNode node = e.Node.Parent;
                while (node != null)
                {
                    CInfoChampDynamique champParent = node.Tag as CInfoChampDynamique;
                    if (champParent != null)
                        m_champSel.InsereParent(champParent);
                    node = node.Parent;
                }
            }
		}

        private void CFormSelectChampParentPourStructure_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            CWin32Traducteur.Translate(this);

        }

	}
}
