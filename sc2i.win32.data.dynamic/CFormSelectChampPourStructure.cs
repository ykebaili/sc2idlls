using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using sc2i.expression;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormSelectChampPourStructure.
	/// </summary>
	public class CFormSelectChampPourStructure : System.Windows.Forms.Form
	{
		[Flags]
		public enum TypeSelectionAttendue
		{
			ChampParent = 1,//Inclure les champs parents (champs uniques)
			TableParente = 2,//Inclure les tables parentes (tables liées à N à 1)
			TableFille = 4,//Inclure les tables filles (tables liées 1 à N)
			ChampFille = 8,//champs multiples
			UniquementElementDeBaseDeDonnees = 16,//Uniquement les champs présents dans la BDD
			InclureChampsCustom = 32,//Avec champs custom
			AllowThis = 64, //L'utilisateur peut choisir l'élément this (celui à partir duquel on interroge)
			MonoSelection = 128//L'utilisateur ne peut choisir qu'un seul champ
		}

		public TypeSelectionAttendue m_typeSelection = TypeSelectionAttendue.ChampParent;
		private Type m_typePrincipal = null;
		private IFournisseurProprietesDynamiques m_fournisseur = new CFournisseurPropDynStd(false);
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.TreeView m_arbre;
		private System.Windows.Forms.Label m_lblTable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ImageList m_imageList;
		private System.ComponentModel.IContainer components;

		public CFormSelectChampPourStructure()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectChampPourStructure));
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
            this.m_arbre.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterCheck);
            this.m_arbre.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbre_BeforeCheck);
            this.m_arbre.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbre_BeforeExpand);
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
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
            this.m_lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTable.Location = new System.Drawing.Point(93, 0);
            this.m_lblTable.Name = "m_lblTable";
            this.m_lblTable.Size = new System.Drawing.Size(354, 24);
            this.m_lblTable.TabIndex = 4;
            this.m_lblTable.Text = "Table name|166";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Table|165";
            // 
            // CFormSelectChampPourStructure
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 445);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblTable);
            this.Controls.Add(this.m_arbre);
            this.Controls.Add(this.panel1);
            this.Name = "CFormSelectChampPourStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add fields|164";
            this.Load += new System.EventHandler(this.CFormSelectChampPourStructure_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//-----------------------------------------------------------------------
		private class DefSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				if ( x is CDefinitionProprieteDynamique && y is CDefinitionProprieteDynamique )
				{
					return ((CDefinitionProprieteDynamique)x).Nom.CompareTo ( ((CDefinitionProprieteDynamique)y).Nom);
				}
				return -1;
			}
		}

		//-----------------------------------------------------------------------
		//Retourne vrai si le champ doit apparaitre dans l'arbre
		private bool ShouldHadChamp ( CDefinitionProprieteDynamique def, Type typeParent )
		{
			bool bRetour = false;
			if ( (m_typeSelection & TypeSelectionAttendue.ChampParent)>0 )
				bRetour |= !def.TypeDonnee.IsArrayOfTypeNatif;
			if ( ( m_typeSelection & TypeSelectionAttendue.ChampFille) > 0 )
				bRetour |= def.TypeDonnee.IsArrayOfTypeNatif;
			if ( (m_typeSelection & TypeSelectionAttendue.TableParente)>0 )
				bRetour |= !def.TypeDonnee.IsArrayOfTypeNatif && def.HasSubProperties;
			if ( (m_typeSelection & TypeSelectionAttendue.TableFille)>0 )
				bRetour |= def.HasSubProperties;
			
			if ( bRetour && (m_typeSelection & TypeSelectionAttendue.UniquementElementDeBaseDeDonnees) > 0 )
			{
				/*Vérifie que la donnée est dans la base de données
				 attribut TableField, RelationField
				*/
				if (def is CDefinitionProprieteDynamique && (m_typeSelection & TypeSelectionAttendue.InclureChampsCustom) > 0)
					bRetour = true;
				else if ( !typeof ( sc2i.data.CListeObjetsDonnees).IsAssignableFrom(def.TypeDonnee.TypeDotNetNatif ) )
				{
					if ( def.TypeDonnee.TypeDotNetNatif.GetCustomAttributes(typeof(sc2i.data.TableAttribute), true).Length == 0 )
					{
						if ( typeParent != null )
						{
							//Trouve la propriété qui retourne le champ
							PropertyInfo info = typeParent.GetProperty ( def.NomProprieteSansCleTypeChamp );
							if ( info == null 
								|| info.GetCustomAttributes ( typeof(sc2i.data.TableFieldPropertyAttribute), true).Length == 0 )
								bRetour = false;
						}
					}
				}
			}
			return bRetour;
		}

		private bool ShouldHasFils ( CDefinitionProprieteDynamique def )
		{
			if ( !def.HasSubProperties )
				return false;
			return !def.TypeDonnee.IsArrayOfTypeNatif || (m_typeSelection & TypeSelectionAttendue.ChampFille)>0;
		}

		private bool CanCheck ( CDefinitionProprieteDynamique def )
		{
			bool bRetour = false;
			if ( (m_typeSelection & TypeSelectionAttendue.AllowThis)>0 && 
				def is CDefinitionProprieteDynamiqueThis )
				return true;
			if ( (m_typeSelection & TypeSelectionAttendue.ChampParent)>0 )
				bRetour |= !def.HasSubProperties;
			if ( (m_typeSelection & TypeSelectionAttendue.TableParente)>0 )
				bRetour |= def.HasSubProperties && !def.TypeDonnee.IsArrayOfTypeNatif;
			if ( (m_typeSelection & TypeSelectionAttendue.TableFille)>0 )
				bRetour |= def.HasSubProperties && def.TypeDonnee.IsArrayOfTypeNatif;
			return bRetour;
		}

		//-----------------------------------------------------------------------
		private void FillTree ( Type tp, TreeNodeCollection nodes, CDefinitionProprieteDynamique defParente )
		{
			ArrayList lst = new ArrayList( m_fournisseur.GetDefinitionsChamps ( tp, 0, defParente ) );
			lst.Sort ( new DefSorter() );
			//Trouve les catégories
			Hashtable tableCategories = new Hashtable();
			foreach ( CDefinitionProprieteDynamique def in lst )
			{

				if ( def.Rubrique != "" && ShouldHadChamp ( def, tp ) )
				{
					if ( tableCategories[def.Rubrique] == null )
					{
						TreeNode node = new TreeNode ( def.Rubrique );
						node.ImageIndex = 0;
						node.SelectedImageIndex = 0;
						nodes.Add ( node );
						tableCategories[def.Rubrique] = node;
					}
				}
			}
			foreach ( CDefinitionProprieteDynamique def in lst )
			{
				if ( ShouldHadChamp ( def, tp ) )
				{
					TreeNode node = new TreeNode ( def.Nom );
					node.ImageIndex = def.HasSubProperties?(def.TypeDonnee.IsArrayOfTypeNatif?3:1):2;
					node.SelectedImageIndex = node.ImageIndex;
					TreeNodeCollection nodesParents = nodes;
					if ( def.Rubrique != "" )
					{
						TreeNode nodeParent = (TreeNode)tableCategories[def.Rubrique];
						if ( nodeParent != null )
							nodesParents = nodeParent.Nodes;
					}
					node.Tag = def;
					nodesParents.Add ( node );
					if ( def.HasSubProperties && ShouldHasFils ( def ))
						node.Nodes.Add("*");
				}
			}
		}

		//-----------------------------------------------------------------------
		private void m_arbre_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if ( node.Tag is CDefinitionProprieteDynamique &&
				!CanCheck ( (CDefinitionProprieteDynamique)node.Tag) )
				e.Cancel = true;
		}

		//-----------------------------------------------------------------------
		private void GetSelectedFields ( ArrayList lst, TreeNodeCollection nodes, CDefinitionProprieteDynamique defParent )
		{
			foreach ( TreeNode node in nodes )
			{
				CDefinitionProprieteDynamique defProp = null;
				if ( node.Tag is CDefinitionProprieteDynamique )
				{
					defProp = (CDefinitionProprieteDynamique)node.Tag;
					defProp = defProp.Clone();
					if ( defParent != null && defParent.GetType() != typeof(CDefinitionProprieteDynamiqueThis))
						defProp.InsereParent ( defParent );
				}
				else if ( node.Tag == null )//C'est un folder
						defProp = defParent;
				if ( node.Checked && defProp != null)
				{
					lst.Add ( defProp );
				}
				GetSelectedFields ( lst, node.Nodes, defProp );
			}
		}

		//-----------------------------------------------------------------------
		public static CDefinitionProprieteDynamique[] SelectProprietes ( 
			Type tp, 
			TypeSelectionAttendue typeSelection, 
			CDefinitionProprieteDynamique defParente)
		{
			return SelectProprietes (tp, typeSelection, defParente, null );
		}

		//-----------------------------------------------------------------------
		public static CDefinitionProprieteDynamique[] SelectProprietes ( 
			Type tp, 
			TypeSelectionAttendue typeSelection, 
			CDefinitionProprieteDynamique defParente,
			IFournisseurProprietesDynamiques fournisseur)
		{
			CFormSelectChampPourStructure form = new CFormSelectChampPourStructure();
			if ( fournisseur != null )
				form.m_fournisseur = fournisseur;
			form.m_typePrincipal = tp;
			form.m_typeSelection = typeSelection;
			form.m_lblTable.Text = DynamicClassAttribute.GetNomConvivial(form.m_typePrincipal);

			TreeNodeCollection nodes = form.m_arbre.Nodes;
			if ((typeSelection & TypeSelectionAttendue.AllowThis) == TypeSelectionAttendue.AllowThis)
			{
				TreeNode nodeThis = new TreeNode(DynamicClassAttribute.GetNomConvivial(tp));
				nodeThis.Tag = new CDefinitionProprieteDynamiqueThis(new CTypeResultatExpression ( tp, false), true, true);
				form.m_arbre.Nodes.Add(nodeThis);
				nodes = nodeThis.Nodes;
			}

			form.FillTree ( tp, nodes, defParente );
			if (form.m_arbre.Nodes.Count == 1)
				form.m_arbre.Nodes[0].Expand();
			if ( form.ShowDialog() == DialogResult.OK )
			{
				ArrayList lst = new ArrayList();
				form.GetSelectedFields ( lst, form.m_arbre.Nodes, null );
				return ( CDefinitionProprieteDynamique[] )lst.ToArray ( typeof ( CDefinitionProprieteDynamique ) );
			}
			return new CDefinitionProprieteDynamique[0];
		}

		//-----------------------------------------------------------------------
		private void m_arbre_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( e.Node.Nodes.Count == 1 && 
				e.Node.Nodes[0].Text == "*")
			{
				CDefinitionProprieteDynamique def = (CDefinitionProprieteDynamique)e.Node.Tag;
				if ( def == null )
					return;
				e.Node.Nodes.Clear();
				FillTree ( def.TypeDonnee.TypeDotNetNatif, e.Node.Nodes, def );
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
			if ( e.Node.Parent == null || !(e.Node.Parent.Tag is CDefinitionProprieteDynamique) )
				m_lblTable.Text = DynamicClassAttribute.GetNomConvivial(m_typePrincipal);
			else
				m_lblTable.Text = ((CDefinitionProprieteDynamique)e.Node.Parent.Tag).NomProprieteSansCleTypeChamp;
		}

        private void CFormSelectChampPourStructure_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		private void m_arbre_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Checked && (m_typeSelection & TypeSelectionAttendue.MonoSelection) == TypeSelectionAttendue.MonoSelection)
			{
				UncheckAllBut( m_arbre.Nodes, e.Node);
			}

		}

		private void UncheckAllBut(TreeNodeCollection nodes, TreeNode node)
		{
			foreach (TreeNode nodeTest in nodes)
			{
				if (nodeTest.Checked && nodeTest != node)
					nodeTest.Checked = false;
			}
		}

	}
}
