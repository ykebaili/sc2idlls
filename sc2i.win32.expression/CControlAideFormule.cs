using System;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Description résumée de CControlChampsDataSet.
	/// </summary
	public delegate void SendCommande ( string strCommande, int nPosCurseur );

	public class CControlAideFormule : System.Windows.Forms.UserControl
	{
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
		private CObjetPourSousProprietes m_objetInterroge = null;
		private System.Windows.Forms.ImageList m_imageList;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnInsererChamp;
		private System.ComponentModel.IContainer components;
		
		private DataSet	m_dataset;
		private string	m_strTableDefaut;
		private bool	m_bSendIdChamps = false;//Indique que lors d'un double clic,
		//c'est l'id du champ qui est envoyée et non le libellé du champ
		private System.Windows.Forms.TabPage m_tabFonctions;
		private System.Windows.Forms.TabPage m_tabChamps;
		private System.Windows.Forms.ImageList m_imageForTab;
		private System.Windows.Forms.TreeView m_arbreChamps;
		private System.Windows.Forms.TreeView m_arbreFormules;
		private System.Windows.Forms.TabControl m_tab;
		private System.Windows.Forms.TextBox m_txtInfo;
		private System.Windows.Forms.CheckBox m_chkNomsComplets;
		private System.Windows.Forms.TabPage m_tabTypes;
		private System.Windows.Forms.TreeView m_arbreTypes;
		private System.Windows.Forms.Splitter m_splitterVert;

		//Evenement envoyé lorsque le controle veut envoyer du code à la fenêtre appelante
		public event SendCommande OnSendCommande;

		public CControlAideFormule()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
			OnSendCommande += new SendCommande ( OnSendCommandeDefaut );
			// TODO : ajoutez les initialisations après l'appel à InitForm

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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControlAideFormule));
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkNomsComplets = new System.Windows.Forms.CheckBox();
            this.m_txtInfo = new System.Windows.Forms.TextBox();
            this.m_btnInsererChamp = new System.Windows.Forms.Button();
            this.m_tab = new System.Windows.Forms.TabControl();
            this.m_tabFonctions = new System.Windows.Forms.TabPage();
            this.m_arbreFormules = new System.Windows.Forms.TreeView();
            this.m_tabChamps = new System.Windows.Forms.TabPage();
            this.m_arbreChamps = new System.Windows.Forms.TreeView();
            this.m_tabTypes = new System.Windows.Forms.TabPage();
            this.m_arbreTypes = new System.Windows.Forms.TreeView();
            this.m_imageForTab = new System.Windows.Forms.ImageList(this.components);
            this.m_splitterVert = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.m_tab.SuspendLayout();
            this.m_tabFonctions.SuspendLayout();
            this.m_tabChamps.SuspendLayout();
            this.m_tabTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.White;
            this.m_imageList.Images.SetKeyName(0, "");
            this.m_imageList.Images.SetKeyName(1, "");
            this.m_imageList.Images.SetKeyName(2, "");
            this.m_imageList.Images.SetKeyName(3, "");
            this.m_imageList.Images.SetKeyName(4, "");
            this.m_imageList.Images.SetKeyName(5, "");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkNomsComplets);
            this.panel1.Controls.Add(this.m_txtInfo);
            this.panel1.Controls.Add(this.m_btnInsererChamp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 80);
            this.panel1.TabIndex = 4;
            // 
            // m_chkNomsComplets
            // 
            this.m_chkNomsComplets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkNomsComplets.Checked = true;
            this.m_chkNomsComplets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkNomsComplets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_chkNomsComplets.Location = new System.Drawing.Point(83, 56);
            this.m_chkNomsComplets.Name = "m_chkNomsComplets";
            this.m_chkNomsComplets.Size = new System.Drawing.Size(119, 24);
            this.m_chkNomsComplets.TabIndex = 4;
            this.m_chkNomsComplets.Text = "Complete names|100";
            // 
            // m_txtInfo
            // 
            this.m_txtInfo.AcceptsReturn = true;
            this.m_txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfo.Location = new System.Drawing.Point(0, 0);
            this.m_txtInfo.Multiline = true;
            this.m_txtInfo.Name = "m_txtInfo";
            this.m_txtInfo.ReadOnly = true;
            this.m_txtInfo.Size = new System.Drawing.Size(206, 56);
            this.m_txtInfo.TabIndex = 3;
            // 
            // m_btnInsererChamp
            // 
            this.m_btnInsererChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnInsererChamp.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnInsererChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnInsererChamp.Location = new System.Drawing.Point(0, 56);
            this.m_btnInsererChamp.Name = "m_btnInsererChamp";
            this.m_btnInsererChamp.Size = new System.Drawing.Size(77, 24);
            this.m_btnInsererChamp.TabIndex = 2;
            this.m_btnInsererChamp.Text = "Insert|28";
            this.m_btnInsererChamp.UseVisualStyleBackColor = false;
            this.m_btnInsererChamp.Click += new System.EventHandler(this.m_btnInsererChamp_Click);
            // 
            // m_tab
            // 
            this.m_tab.Controls.Add(this.m_tabFonctions);
            this.m_tab.Controls.Add(this.m_tabChamps);
            this.m_tab.Controls.Add(this.m_tabTypes);
            this.m_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tab.ImageList = this.m_imageForTab;
            this.m_tab.Location = new System.Drawing.Point(0, 0);
            this.m_tab.Name = "m_tab";
            this.m_tab.SelectedIndex = 0;
            this.m_tab.Size = new System.Drawing.Size(206, 256);
            this.m_tab.TabIndex = 5;
            // 
            // m_tabFonctions
            // 
            this.m_tabFonctions.BackColor = System.Drawing.Color.Transparent;
            this.m_tabFonctions.Controls.Add(this.m_arbreFormules);
            this.m_tabFonctions.ImageIndex = 0;
            this.m_tabFonctions.Location = new System.Drawing.Point(4, 23);
            this.m_tabFonctions.Name = "m_tabFonctions";
            this.m_tabFonctions.Size = new System.Drawing.Size(198, 229);
            this.m_tabFonctions.TabIndex = 0;
            // 
            // m_arbreFormules
            // 
            this.m_arbreFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreFormules.ImageIndex = 0;
            this.m_arbreFormules.ImageList = this.m_imageList;
            this.m_arbreFormules.Location = new System.Drawing.Point(0, 0);
            this.m_arbreFormules.Name = "m_arbreFormules";
            this.m_arbreFormules.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_arbreFormules.SelectedImageIndex = 0;
            this.m_arbreFormules.ShowRootLines = false;
            this.m_arbreFormules.Size = new System.Drawing.Size(198, 229);
            this.m_arbreFormules.TabIndex = 5;
            this.m_arbreFormules.DoubleClick += new System.EventHandler(this.m_arbreFormules_DoubleClick);
            this.m_arbreFormules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreFormules_AfterSelect);
            this.m_arbreFormules.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_arbreFormules_KeyPress);
            // 
            // m_tabChamps
            // 
            this.m_tabChamps.Controls.Add(this.m_arbreChamps);
            this.m_tabChamps.ImageIndex = 1;
            this.m_tabChamps.Location = new System.Drawing.Point(4, 23);
            this.m_tabChamps.Name = "m_tabChamps";
            this.m_tabChamps.Size = new System.Drawing.Size(198, 229);
            this.m_tabChamps.TabIndex = 1;
            // 
            // m_arbreChamps
            // 
            this.m_arbreChamps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreChamps.FullRowSelect = true;
            this.m_arbreChamps.ImageIndex = 0;
            this.m_arbreChamps.ImageList = this.m_imageList;
            this.m_arbreChamps.Location = new System.Drawing.Point(0, 0);
            this.m_arbreChamps.Name = "m_arbreChamps";
            this.m_arbreChamps.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_arbreChamps.SelectedImageIndex = 0;
            this.m_arbreChamps.Size = new System.Drawing.Size(198, 229);
            this.m_arbreChamps.TabIndex = 4;
            this.m_arbreChamps.DoubleClick += new System.EventHandler(this.m_arbreChamps_DoubleClick);
            this.m_arbreChamps.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbreChamps_MouseUp);
            this.m_arbreChamps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreChamps_AfterSelect);
            this.m_arbreChamps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_arbreChamps_KeyPress);
            this.m_arbreChamps.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreChamps_AfterExpand);
            // 
            // m_tabTypes
            // 
            this.m_tabTypes.Controls.Add(this.m_arbreTypes);
            this.m_tabTypes.ImageIndex = 2;
            this.m_tabTypes.Location = new System.Drawing.Point(4, 23);
            this.m_tabTypes.Name = "m_tabTypes";
            this.m_tabTypes.Size = new System.Drawing.Size(198, 229);
            this.m_tabTypes.TabIndex = 2;
            // 
            // m_arbreTypes
            // 
            this.m_arbreTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreTypes.ImageIndex = 0;
            this.m_arbreTypes.ImageList = this.m_imageList;
            this.m_arbreTypes.Location = new System.Drawing.Point(0, 0);
            this.m_arbreTypes.Name = "m_arbreTypes";
            this.m_arbreTypes.SelectedImageIndex = 0;
            this.m_arbreTypes.Size = new System.Drawing.Size(198, 229);
            this.m_arbreTypes.TabIndex = 0;
            this.m_arbreTypes.DoubleClick += new System.EventHandler(this.m_arbreTypes_DoubleClick);
            this.m_arbreTypes.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreTypes_AfterExpand);
            // 
            // m_imageForTab
            // 
            this.m_imageForTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageForTab.ImageStream")));
            this.m_imageForTab.TransparentColor = System.Drawing.Color.White;
            this.m_imageForTab.Images.SetKeyName(0, "");
            this.m_imageForTab.Images.SetKeyName(1, "");
            this.m_imageForTab.Images.SetKeyName(2, "");
            // 
            // m_splitterVert
            // 
            this.m_splitterVert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitterVert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_splitterVert.Location = new System.Drawing.Point(0, 248);
            this.m_splitterVert.Name = "m_splitterVert";
            this.m_splitterVert.Size = new System.Drawing.Size(206, 8);
            this.m_splitterVert.TabIndex = 6;
            this.m_splitterVert.TabStop = false;
            // 
            // CControlAideFormule
            // 
            this.Controls.Add(this.m_splitterVert);
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.panel1);
            this.Name = "CControlAideFormule";
            this.Size = new System.Drawing.Size(206, 336);
            this.Load += new System.EventHandler(this.CControlChampsDataSet_Load);
            this.DoubleClick += new System.EventHandler(this.m_arbreTypes_DoubleClick);
            this.BackColorChanged += new System.EventHandler(this.CControlAideFormule_BackColorChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.m_tab.ResumeLayout(false);
            this.m_tabFonctions.ResumeLayout(false);
            this.m_tabChamps.ResumeLayout(false);
            this.m_tabTypes.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		///////////////////////////////////////////////////////////////////////////
		private void CControlChampsDataSet_Load(object sender, System.EventArgs e)
		{
			// Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
           
            FillListeTypes();
			FillArbreFormules();
			FillArbreChamps();
			if ( m_fournisseurProprietes == null || m_objetInterroge == null)
				m_tab.TabPages.Remove ( m_tabChamps );
		}

		///////////////////////////////////////////////////////////////////////////
		private void FillListeTypes()
		{
			try
			{
				m_arbreTypes.Nodes.Clear();
				foreach ( CInfoClasseDynamique info in DynamicClassAttribute.GetAllDynamicClass() )
				{
					TreeNode node = new TreeNode( info.Nom, 4, 4 );
					node.Tag = info.Classe;
					m_arbreTypes.Nodes.Add ( node );
					TreeNode dummy = new TreeNode();
					node.Nodes.Add ( dummy );
				}
				m_arbreTypes.Sorted = true;
			}
			catch
				//On est obligé, sinon, le contrôle ne peut pas être mis dans une fenêtre
				//La propriété design mode ne marche pas, je ne sais pas pourquoi
			{}
		}

		///////////////////////////////////////////////////////////////////////////
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
				RefillChamps();
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public void RefillChamps()
		{
			if ( !m_tab.TabPages.Contains(m_tabChamps) && m_objetInterroge!=null)
			{
				m_tab.TabPages.Add( m_tabChamps );
			}
			FillArbreChamps();
		}

		///////////////////////////////////////////////////////////////////////////
		public CObjetPourSousProprietes ObjetInterroge
		{
			get
			{
				return m_objetInterroge;
			}
			set
			{
				m_objetInterroge = value;
				if ( !m_tab.TabPages.Contains(m_tabChamps) && m_objetInterroge!=null)
				{
					m_tab.TabPages.Remove ( m_tabTypes );
					m_tab.TabPages.Add ( m_tabChamps );
					m_tab.TabPages.Add ( m_tabTypes );
				}
				if ( m_tab.TabPages.Contains(m_tabChamps) && m_objetInterroge == null )
					m_tab.TabPages.Remove ( m_tabChamps );

				FillArbreChamps();
			}
		}

		///////////////////////////////////////////////////////////////////////////
		private void FillArbreFormules()
		{
			Hashtable table = new Hashtable();
			CAllocateur2iExpression allocateur = new CAllocateur2iExpression();
			ArrayList liste;
			foreach ( C2iExpression exp in CAllocateur2iExpression.ToutesExpressions )
			{
				if ( exp is C2iExpressionAnalysable )
				{
					CInfo2iExpression info = ((C2iExpressionAnalysable)exp).GetInfos();
					if ( info != null && info.Categorie != "" )
					{
						liste = (ArrayList)table[info.Categorie];
						if ( liste == null )
						{
							liste = new ArrayList();
							table[info.Categorie] = liste;
						}
						liste.Add ( exp );
					}
				}
			}
            foreach (C2iExpressionConstanteDynamique exp in CAnalyseurSyntaxiqueExpression.GetConstantesDynamiques())
            {
                CInfo2iExpression info = exp.GetInfos();
                if (info != null && info.Categorie != "")
                {
                    liste = (ArrayList)table[info.Categorie];
                    if (liste == null)
                    {
                        liste = new ArrayList();
                        table[info.Categorie] = liste;
                    }
                    liste.Add(exp);
                }
            }

			m_arbreFormules.Sorted = true;
			foreach ( string cat in table.Keys )
			{
				TreeNode node = m_arbreFormules.Nodes.Add(cat);
				node.Tag = null;
				liste = (ArrayList)table[cat];
				foreach ( C2iExpressionAnalysable exp in liste )
				{
					TreeNode nodeFils = node.Nodes.Add ( exp.GetInfos().Texte );
					nodeFils.Tag = exp;
				}
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public void FillArbreChamps()
		{
			m_arbreChamps.Nodes.Clear();
			if ( m_objetInterroge == null || m_fournisseurProprietes == null)
				return;
			Hashtable tablesBranche = new Hashtable();
			Hashtable tablesTotales = new Hashtable();
			if ( m_objetInterroge != null )
			{
				CreateNodesType ( m_objetInterroge, m_arbreChamps.Nodes, null);//, tablesBranche, tablesTotales );
			}
		}


		/////////////////////////////////////////////////////////////////////////////
		private bool FiltreInterfacesArray ( Type tp, object filterCriteria )
		{
			if ( tp==typeof(IList) || tp==typeof(ICollection) )
				return true;
			return false;
		}
		/////////////////////////////////////////////////////////////////////////////
		private bool IsMultiple ( Type tp )
		{
			if ( tp.IsArray )
				return true;
			if ( tp.FindInterfaces(new System.Reflection.TypeFilter(FiltreInterfacesArray), null).Length!=0)
				return true;
			return false;
		}

       
		/////////////////////////////////////////////////////////////////////////////
		private void CreateNodesType ( CObjetPourSousProprietes objetInterroge, TreeNodeCollection nodes, CDefinitionProprieteDynamique defParente )//, Hashtable tablesBranche, Hashtable tableTotale )
		{
			if ( m_fournisseurProprietes == null )
				return;
			Hashtable tableRubriques = new Hashtable();
			CDefinitionProprieteDynamique[] defs = m_fournisseurProprietes.GetDefinitionsChamps(objetInterroge, defParente);
			foreach ( CDefinitionProprieteDynamique def in defs )
			{
				TreeNodeCollection collec = nodes;
				if ( def.Rubrique.Trim() != "" )
				{
                    string[] strRubs = def.Rubrique.Split('/');
                    string strFull = "";
                    foreach (string strRub in strRubs)
                    {
                        if (strRub.Trim().Length != 0)
                        {
                            strFull += "/" + strRub;
                            TreeNodeCollection tmp = (TreeNodeCollection)tableRubriques[strFull]; ;
                            if (tmp == null)
                            {
                                TreeNode node = new TreeNode(strRub, 5, 5);
                                collec.Insert(0, node);
                                tableRubriques[strFull] = node.Nodes;
                                collec = node.Nodes;
                            }
                            else
                                collec = tmp;
                        }
                    }
				}
				bool bIsMethod = def is CDefinitionMethodeDynamique || def is CDefinitionFonctionDynamique;
				string strNom = def.Nom;
				/*if ( bIsMethod )
				{
					strNom+="(";
					for ( int n = 1; n < ((CDefinitionMethodeDynamique)def).InfosParametres.Length; n++ )
						strNom+=";";
					strNom+=")";
				}*/
				TreeNode nodeFils = collec.Add ( strNom );
				nodeFils.Tag = def;
				bool bHasSubs = def.HasSubProperties;// m_fournisseurProprietes.GetDefinitionsChamps(def.TypeDonnee.TypeDotNetNatif, 0).Length > 0;
				if ( def.TypeDonnee.IsArrayOfTypeNatif )
					nodeFils.ImageIndex = bHasSubs?1:2;
				else
					nodeFils.ImageIndex = bHasSubs?0:2;
				if ( bIsMethod )
					nodeFils.ImageIndex = 3;
				nodeFils.SelectedImageIndex = nodeFils.ImageIndex;
				if ( bHasSubs/*&& tablesBranche[def.TypeDonnee] == null*/ )
				{
					TreeNode nodeBidon = nodeFils.Nodes.Add ( "" );
					nodeBidon.Tag = null;
				}
			}
		}

		///////////////////////////////////////////////////////////////////////////
		private void m_btnInsererChamp_Click(object sender, System.EventArgs e)
		{
			InsereElement();
		}

		///////////////////////////////////////////////////////////////////////////
		//Handler par défaut
		private void OnSendCommandeDefaut ( string strCommande, int nPos )
		{
		}

		///////////////////////////////////////////////////////////////////////////
		private void InsereElement ()
		{
			if ( m_tab.SelectedTab == m_tabFonctions )
			{
				TreeNode node = m_arbreFormules.SelectedNode;
				if ( node == null || node.Tag==null)
					return;
				C2iExpressionAnalysable exp = (C2iExpressionAnalysable)node.Tag;
				CInfo2iDefinitionParametres[] infos = exp.GetInfos().InfosParametres;
				CInfo2iExpression info = exp.GetInfos();
				string strEnvoi = info.Texte;
				int nPos = strEnvoi.Length;
				if ( info.Niveau == 0 && !typeof(C2iExpressionConstanteDynamique).IsAssignableFrom ( exp.GetType() ))
				{
					strEnvoi += " ( ";
					if ( infos.Length != 0 )
						for (int n = 1; n < infos[0].TypesDonnees.Length; n++ )
							strEnvoi += "; ";
					strEnvoi+=")";
					nPos += 3;

				}
				OnSendCommande ( strEnvoi, nPos );
			}
			else 
			{
				TreeView arbre = null;
				if ( m_tab.SelectedTab == m_tabChamps )
					arbre = m_arbreChamps;
				else
					arbre = m_arbreTypes;

				TreeNode node = arbre.SelectedNode;
				if ( node == null )
					return;
				if ( node.Tag is Type )
				{
					string strText = "\""+node.Text+"\"";
					OnSendCommande ( strText, strText.Length );
					return;
				}
				if ( !(node.Tag is CDefinitionProprieteDynamique ))
					return;
				string strChemin="";
				while (node != null )
				{
					string strTexte = node.Text;
					if ( node.Tag is CDefinitionProprieteDynamique )
					{
						CDefinitionProprieteDynamique def = (CDefinitionProprieteDynamique)node.Tag;
						if ( m_bSendIdChamps )
							strTexte = def.Nom;
						if ( arbre == m_arbreChamps  && !(def is CDefinitionMethodeDynamique) && !(def is CDefinitionFonctionDynamique))
							strChemin = "["+strTexte+"]."+strChemin;
						else
							strChemin = strTexte+"."+strChemin;
					}
					if ( m_chkNomsComplets.Checked )
						node = node.Parent;
					else 
						node = null;
				}
				if ( strChemin.Length > 0 )
					strChemin = strChemin.Substring(0, strChemin.Length-1);
				int nPos = strChemin.Length;
				OnSendCommande ( strChemin, nPos );
			}
		}

		///////////////////////////////////////////////////////////////////////////
		private void m_arbreChamps_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			m_txtInfo.Text = "";
			TreeNode node = m_arbreChamps.SelectedNode;
			if ( node.Tag is CDefinitionMethodeDynamique )
			{
				CDefinitionMethodeDynamique def = (CDefinitionMethodeDynamique)node.Tag;
				string strInfo = def.InfoMethode;
				int nParam = 1;
				foreach ( string strParam in def.InfosParametres )
				{
					strInfo+="\r\n"+nParam.ToString()+"-"+strParam;
					nParam++;
				}
				m_txtInfo.Text = strInfo;

			}
		}

		///////////////////////////////////////////////////////////////////////////
		private void SetInfos ( string strInfos )
		{
			m_txtInfo.Text = strInfos;
		}


		///////////////////////////////////////////////////////////////////////////
		public void SetParametres ( DataSet ds, string strTableDefaut )
		{
			if ( m_dataset != ds || m_strTableDefaut != strTableDefaut )
			{
				m_dataset = ds;
				m_strTableDefaut = strTableDefaut;
				FillArbreChamps();
			}
		}

		private void m_arbreChamps_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar=='+' )
				InsereElement();
		}

		private void m_arbreFormules_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			
			TreeNode node = m_arbreFormules.SelectedNode;
			if ( node == null || node.Tag==null )
				return;
			C2iExpressionAnalysable exp = (C2iExpressionAnalysable)node.Tag;
			SetInfos( exp.GetInfos().Description.Replace("\\n", Environment.NewLine));
		}

		private void m_arbreFormules_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar=='+' )
				InsereElement();
		}

		private void m_arbreChamps_DoubleClick(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreChamps.SelectedNode;
			if ( node != null && node.Tag is CDefinitionProprieteDynamique )
			{
				InsereElement();
			}
		}

		private void m_arbreFormules_DoubleClick(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreFormules.SelectedNode;
			if ( node != null && node.Tag!=null )
			{
				InsereElement();
			}
		}

		private void CControlAideFormule_BackColorChanged(object sender, System.EventArgs e)
		{
			m_tab.BackColor = BackColor;
			m_tabChamps.BackColor = BackColor;
			m_tabFonctions.BackColor = BackColor;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public bool SendIdChamps
		{
			get { return m_bSendIdChamps;}
			set { m_bSendIdChamps = value;}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public void InsereInTextBox ( TextBox txt, int nPosCurseur, string strTexte )
		{
			int nPos = txt.SelectionStart + nPosCurseur;
			txt.SelectedText = strTexte;
			txt.SelectionStart = nPos;
			txt.SelectionLength = 0;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public void InsereInTextBox ( RichTextBox txt, int nPosCurseur, string strTexte )
		{
			int nPos = txt.SelectionStart + nPosCurseur;
			txt.SelectedText = strTexte;
			txt.SelectionStart = nPos;
			txt.SelectionLength = 0;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public void InsereInTextBox ( CControleEditeFormule txt, int nPosCurseur, string strTexte )
		{
			InsereInTextBox ( txt.TextBox, nPosCurseur, strTexte );
		}

		////////////////////////////////////////////////////////////////////////////////////////
		private void m_arbreChamps_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			CDefinitionProprieteDynamique def = (CDefinitionProprieteDynamique)node.Tag;
			if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null && node.Nodes[0].Text == "" )
			{
				node.Nodes.Clear();
				CObjetPourSousProprietes objetAnalyse = def.GetObjetPourAnalyseSousProprietes();
				CreateNodesType(objetAnalyse, node.Nodes, def);
				node.Expand();
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		private void m_arbreTypes_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			if ( node.Tag is Type )
			{
				if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null )
				{
					node.Nodes.Clear();
					CreateNodesType ( (Type)node.Tag, node.Nodes, null);
					node.Expand();
				}
			}
			else if ( node.Tag is CDefinitionProprieteDynamique )
			{
				CDefinitionProprieteDynamique def = (CDefinitionProprieteDynamique)node.Tag;
				if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null )
				{
					node.Nodes.Clear();
					CreateNodesType ( def.GetObjetPourAnalyseSousProprietes(), node.Nodes, def);
					node.Expand();
				}
			}

		}

		private void m_arbreTypes_DoubleClick(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreChamps.SelectedNode;
			InsereElement();
		}

        public delegate void OnRightClickChampDelegate ( Type typeObjet, CDefinitionProprieteDynamique def, Point screenPoint);
        private static OnRightClickChampDelegate m_OnRightClickChampFunc;

        //--------------------------------------------------------------------------------
        public static void SetOnRightClickChampFunc(OnRightClickChampDelegate func)
        {
            m_OnRightClickChampFunc = func;
        }


        //--------------------------------------------------------------------------------
        private void m_arbreChamps_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right && m_arbreChamps.SelectedNode != null)
            {
                TreeNode node = m_arbreChamps.SelectedNode;
                CDefinitionProprieteDynamique def = node.Tag as CDefinitionProprieteDynamique;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    if (node.Tag is CDefinitionProprieteDynamique)
                        def.InsereParent((CDefinitionProprieteDynamique)node.Tag);
                }
                if (def != null && m_OnRightClickChampFunc != null)
                    m_OnRightClickChampFunc(m_objetInterroge.TypeAnalyse, def, m_arbreChamps.PointToScreen(new Point(e.X, e.Y)));
            }
        }

	}
}
