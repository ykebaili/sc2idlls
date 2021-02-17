using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.data;
using sc2i.win32.common;
using sc2i.data.dynamic;
using sc2i.common;
using System.Collections;
using sc2i.multitiers.client;

namespace sc2i.win32.data.navigation
{
	public partial class CControleDetailVersion : UserControl
	{
        private bool m_bCanRestore = false;

		private const string c_strFake = "FAKE";
		private const int c_nImageAdd = 1;
		private const int c_nImageEdit = 3;
		private const int c_nImageDelete = 2;
		private const int c_nImageUnchanged = 0;
		
		private const int c_nImageHistory = 1;
		private const int c_nImageActuel = 0;
		private const int c_nImagePlus = 2;
		private const int c_nImageMoins = 3;

		private string m_strColChamp = "Field|120";
		private string m_strColValeurHistorique = "History|121";
		private string m_strColValeurCourante = "Current|122";
        private string m_strColChecked = "CHECKED";
        private string m_strColObjetHistorique = "HISTORY";


		private CObjetDonneeAIdNumerique m_objetDepart = null;
        
		private CVersionDonnees m_version = null;
		
		//Si version prévisionnelle, indique toutes les versions parentes de celle-ci
		private int[] m_nIdsVersionsSuccessives = new int[0];

		private CContexteDonnee m_contexteDonnees = null;

		Dictionary<CObjetDonneeAIdNumerique, CValeursHistorique> m_tableValeurs = new Dictionary<CObjetDonneeAIdNumerique,CValeursHistorique>();

        private CValeursHistorique m_valeursAffichees = null;
        private CObjetDonneeAIdNumerique m_objetAffiche = null;

		public CControleDetailVersion()
		{
			InitializeComponent();
		}


		//------------------------------------------------------
		public void InitForObjet(CObjetDonneeAIdNumerique objet, CVersionDonnees version)
		{
            CSessionClient session = CSessionClient.GetSessionForIdSession(objet.ContexteDonnee.IdSession);
            IInfoUtilisateur info = session.GetInfoUtilisateur();
            if (info != null)
                m_bCanRestore = info.GetDonneeDroit(CDroitDeBaseSC2I.c_droitAdministration) != null;
            m_bCanRestore &= version != null && version.TypeVersion.Code == CTypeVersion.TypeVersion.Archive;
            m_panelRestore.Visible = m_bCanRestore;

			if ( m_contexteDonnees != null )
				m_contexteDonnees.Dispose();
			m_tableValeurs.Clear();
			m_objetDepart = null;
			m_version = null;
			m_arbreObjet.Nodes.Clear();
			if (objet == null || version == null)
				return;		
			m_contexteDonnees = new CContexteDonnee ( objet.ContexteDonnee.IdSession, true, false );
			m_contexteDonnees.SetVersionDeTravail(-1, false);//Travaille sur toutes les versions
			m_objetDepart = (CObjetDonneeAIdNumerique)m_contexteDonnees.GetObjetInThisContexte ( objet );
			m_version = (CVersionDonnees)m_contexteDonnees.GetObjetInThisContexte ( version );

			List<int> lstIds = new List<int>();
			if (m_version.TypeVersion.Code == CTypeVersion.TypeVersion.Previsionnelle)
			{
				CVersionDonnees verTmp = m_version.VersionParente;
				while (verTmp != null)
				{
					lstIds.Add(verTmp.Id);
					verTmp = verTmp.VersionParente;
				}
			}
			m_nIdsVersionsSuccessives = lstIds.ToArray();

			CreateNodeObjet(m_arbreObjet.Nodes, m_objetDepart);
		}

		//------------------------------------------------------
		private CValeursHistorique GetValeursHistorique ( CObjetDonneeAIdNumerique objet )
		{
			if ( !m_tableValeurs.ContainsKey ( objet ) )
				m_tableValeurs[objet] = m_version.GetValeursModifiees(objet,null);
			return m_tableValeurs[objet];
		}

		//------------------------------------------------------
		private TreeNode CreateNodeObjet(TreeNodeCollection nodes, CObjetDonneeAIdNumerique objet)
		{
			if( objet == null )
			{
				TreeNode newNode = new TreeNode(I.T("No value|107"));
				newNode.Tag = null;
				newNode.ImageIndex = c_nImageUnchanged;
				newNode.SelectedImageIndex = c_nImageUnchanged;
				nodes.Add(newNode);
				return newNode;
			}
			int nImage = c_nImageUnchanged;
			
			if ( m_version.TypeVersion.Code == CTypeVersion.TypeVersion.Previsionnelle )
			{
				StringBuilder blIdsVersionsInteressantes = new StringBuilder();
				blIdsVersionsInteressantes.Append(m_version.Id);
				foreach (int nId in m_nIdsVersionsSuccessives)
				{
					blIdsVersionsInteressantes.Append(";");
					blIdsVersionsInteressantes.Append(nId);
				}
				//Version prévisionnelle
				//L'élément a-t-il été supprimé ?
				CFiltreData filtre = new CFiltreDataAvance ( 
					CVersionDonneesObjet.c_nomTable,
					CVersionDonneesObjet.c_champTypeOperation+"=@1 and "+
					CVersionDonneesObjet.c_champTypeElement+"=@2 and "+
					CVersionDonneesObjet.c_champIdElement+"=@3 and "+
					CVersionDonnees.c_champId+" in ("+blIdsVersionsInteressantes.ToString()+")",
					(int)CTypeOperationSurObjet.TypeOperation.Suppression,
					objet.GetType().ToString(),
					objet.Id );
				//si prévisionnelle, y a t il une création depuis la version de référence ?
				CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(m_contexteDonnees);
				if ( versionObjet.ReadIfExists ( filtre ) )
					nImage = c_nImageDelete;
				else
				{
					//Y a t-il un ajout ?
					filtre.Parametres[0] = (int)CTypeOperationSurObjet.TypeOperation.Ajout;
					if ( versionObjet.ReadIfExists ( filtre ) )
						nImage = c_nImageAdd;
					else
					{
						//Modification ?
						filtre.Parametres[0] = (int)CTypeOperationSurObjet.TypeOperation.Modification ;
						if ( versionObjet.ReadIfExists ( filtre ) )
							nImage = c_nImageEdit;
					}
				}
			}
			else //Version archive ou étiquette
			{
				if (objet.IdVersionDatabase != null)
				{
					//L'objet appartient à une version, il a donc été supprimé
					nImage = c_nImageDelete;
				}
				else
				{
					//Trouve t-on un ajout depuis la version de référence ?
					CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(m_contexteDonnees);
					CFiltreData filtre = new CFiltreDataAvance(
						CVersionDonneesObjet.c_nomTable,
						CVersionDonneesObjet.c_champTypeOperation + "=@1 and " +
						CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
						CVersionDonneesObjet.c_champIdElement + "=@3 and " +
						CVersionDonnees.c_champId + ">=@4 and " +
						CVersionDonnees.c_nomTable + "." +
						CVersionDonnees.c_champTypeVersion + "=@5",
						(int)CTypeOperationSurObjet.TypeOperation.Ajout,
						objet.GetType().ToString(),
						objet.Id,
						m_version.Id,
						(int)CTypeVersion.TypeVersion.Archive);
					if (versionObjet.ReadIfExists(filtre))
						nImage = c_nImageAdd;
					else
					{
						filtre.Parametres[0] = (int)CTypeOperationSurObjet.TypeOperation.Modification;
						if (versionObjet.ReadIfExists(filtre))
							nImage = c_nImageEdit;
					}
				}
			}
			TreeNode node = new TreeNode(objet.DescriptionElement);
			node.ImageIndex = nImage;
			node.SelectedImageIndex = nImage;
			node.Tag = objet;
			//Ajoute un faux fils
			nodes.Add(node);
			AddFakeNode(node);
			return node;
		}

		private void AddFakeNode(TreeNode node)
		{
			TreeNode fakeNode = new TreeNode(c_strFake);
			fakeNode.Tag = DBNull.Value;
			node.Nodes.Add(fakeNode);
		}
			

		private void m_arbreObjet_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			using (CWaitCursor waiter = new CWaitCursor())
			{
				if ( e.Node.Nodes.Count != 1 || e.Node.Nodes[0].Tag != DBNull.Value )
					return;//C'est un vrai noeud, on ne remplit pas
				e.Node.Nodes.Clear();

				if (e.Node.Tag is CObjetDonneeAIdNumerique)
				{
					CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)e.Node.Tag;
					CValeursHistorique valeursHistoriques = GetValeursHistorique(objet);

					CStructureTable structure = CStructureTable.GetStructure(objet.GetType());

					#region relations parentes
					//traitement des relations parentes
					foreach (CInfoRelation relation in structure.RelationsParentes)
					{
						Type tpParent = CContexteDonnee.GetTypeForTable ( relation.TableParente );
						if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpParent) && relation.Propriete != "")
						{
							int nImage = c_nImageUnchanged;
							object newValeur = objet.Row[relation.ChampsFille[0]];
							if (newValeur == null)//Pour éviter les problèmes de null
								newValeur = DBNull.Value;
							object oldValeur = newValeur;
							//Cherche si la valeur pour cette version a changé
							CChampPourVersionInDb champ = new CChampPourVersionInDb(relation.ChampsFille[0], relation.ChampsFille[0]);
							if (valeursHistoriques.ContainsKey(champ))
							{
								oldValeur = valeursHistoriques[champ];
								if (!newValeur.Equals(oldValeur))
									nImage = c_nImageEdit;
							}
							TreeNode node = new TreeNode(relation.NomConvivial);
							node.Tag = relation;
							node.ImageIndex = nImage;
							node.SelectedImageIndex = nImage;
							e.Node.Nodes.Add(node);
							
							//Ajoute la valeur actuelle
							CObjetDonneeAIdNumerique parent = (CObjetDonneeAIdNumerique)objet.GetParent(relation.ChampsFille[0], tpParent);
							TreeNode nodeParent = CreateNodeObjet(node.Nodes, parent);
							nodeParent.StateImageIndex = c_nImageActuel;

							//Ajoute valeur historique
							if (!newValeur.Equals(oldValeur))
							{
								parent = null;
								nodeParent = null;
								if (oldValeur is int)
								{
									//récupère l'ancien parent
									parent = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tpParent, new object[] { objet.ContexteDonnee });
									if (!parent.ReadIfExists((int)oldValeur))
										parent = null;
								}
								nodeParent = CreateNodeObjet(node.Nodes, parent);
								nodeParent.StateImageIndex = c_nImageHistory;
							}
						}
					}
					#endregion

					#region relations filles
					foreach (CInfoRelation relationFille in structure.RelationsFilles)
					{
						if (relationFille.NomConvivial != "")
						{
							
							Type tpFille = CContexteDonnee.GetTypeForTable(relationFille.TableFille);
							if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpFille))
							{
								int nImageNodeRelation = c_nImageUnchanged;
								TreeNode nodeRelation = new TreeNode(relationFille.NomConvivial);
								e.Node.Nodes.Add(nodeRelation);

								Hashtable tableOldIds = new Hashtable();
								foreach (int nId in m_version.GetIdsChildsHistoriques(objet, relationFille))
									tableOldIds.Add(nId, true);


								//Récupère la liste de tous les fils actuels
								C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
								requete.TableInterrogee = relationFille.TableFille;
								C2iChampDeRequete champDeRequete = new C2iChampDeRequete("ID",
									new CSourceDeChampDeRequete(objet.ContexteDonnee.GetTableSafe(relationFille.TableFille).PrimaryKey[0].ColumnName),
									typeof(int),
									OperationsAgregation.None,
									true);
								requete.ListeChamps.Add(champDeRequete);
								CFiltreData filtre = new CFiltreData(relationFille.ChampsFille[0] + "=@1 and " +
									CSc2iDataConst.c_champIdVersion + " is null",
									objet.Id);
								filtre.IgnorerVersionDeContexte = true;
								requete.FiltreAAppliquer = filtre;
								Hashtable tableNewIds = new Hashtable();
								CResultAErreur result = requete.ExecuteRequete(objet.ContexteDonnee.IdSession);
								if (result)
								{
									foreach (DataRow row in ((DataTable)result.Data).Rows)
										tableNewIds.Add(row[0], true);
								}
								//Ajoute les ids actuels
								foreach (int nId in tableNewIds.Keys)
								{
									CObjetDonneeAIdNumerique fils = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tpFille, new object[] { objet.ContexteDonnee });
									if (fils.ReadIfExists(nId))
									{
										int nImage = -1;
										if (!tableOldIds.ContainsKey(nId))
										{
											nImage = c_nImagePlus;
											nImageNodeRelation = c_nImageEdit;
										}
										TreeNode nodeFille = CreateNodeObjet(nodeRelation.Nodes, fils);
										if (nImage >= 0)
											nodeFille.StateImageIndex = nImage;
									}
								}
								//Ajoute les vieux ids
								foreach (int nId in tableOldIds.Keys)
								{
									if (!tableNewIds.ContainsKey(nId))
									{
										CObjetDonneeAIdNumerique fils = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tpFille, new object[] { objet.ContexteDonnee });
										if (fils.ReadIfExists(nId))
										{
											int nImage = c_nImageMoins;
											nImageNodeRelation = c_nImageEdit;
											TreeNode nodeFille = CreateNodeObjet(nodeRelation.Nodes, fils);
											nodeFille.StateImageIndex = nImage;
										}
									}
								}
								nodeRelation.ImageIndex = nImageNodeRelation;
								nodeRelation.SelectedImageIndex = nImageNodeRelation;
							}
						}
					}
					#endregion
				}
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		//------------------------------------------------
		private void m_arbreObjet_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is CObjetDonneeAIdNumerique)
			{
				ShowProprietes((CObjetDonneeAIdNumerique)e.Node.Tag);
				StringBuilder builder = new StringBuilder();
				builder.Append(e.Node.Text);
				builder.Append("\r\n");
				switch ( e.Node.ImageIndex )
				{
					case c_nImageAdd :
						builder.Append(I.T("Has been added since that version|108"));
						builder.Append("\r\n");
						break;
					case c_nImageDelete :
						builder.Append(I.T("Has been deleted since that version|110"));
						builder.Append("\r\n");
						break;
					case c_nImageEdit :
						builder.Append(I.T("Has been modified since that version|109"));
						builder.Append("\r\n");
						break;
					case c_nImageUnchanged :
						builder.Append(I.T("Is unchange since that version|111"));
						builder.Append("\r\n");
						break;
				}
				switch ( e.Node.StateImageIndex )
				{
					case c_nImageActuel :
						builder.Append(I.T("Is the current value|112"));
						break;
					case c_nImageHistory :
						builder.Append(I.T("Has this value for this version|113"));
						break;
					case c_nImagePlus :
						builder.Append(I.T("Has been added to the list|114"));
						break;
					case c_nImageMoins :
						builder.Append(I.T("Has been removed from the list|115"));
						break;
				}
				m_lblInfo.Text = builder.ToString();

			}
			else
			{
				m_grilleProprietes.DataSource = null;
				m_lblInfo.Text = "";
			}
			if (e.Node.StateImageIndex >= 0 && e.Node.StateImageIndex <= m_imagesHistoriqueActuel.Images.Count)
				m_imageState.Image = m_imagesHistoriqueActuel.Images[e.Node.StateImageIndex];
			else
				m_imageState.Image = null;
			if (e.Node.ImageIndex >= 0 && e.Node.ImageIndex <= m_listeImagesObjet.Images.Count)
				m_imageTypeOperation.Image = m_listeImagesObjet.Images[e.Node.ImageIndex];
			else
				m_imageTypeOperation.Image = null;


		}

		//---------------------------------------------
		private void ShowProprietes(CObjetDonneeAIdNumerique objet)
		{
			if (objet == null)
			{
				m_grilleProprietes.DataSource = null;
				return;
			}

			DataTable table = new DataTable();

			m_strColChamp = I.T("Field|120");
			m_strColValeurHistorique = I.T("History|121");
			m_strColValeurCourante = I.T("Current|122");


            DataColumn col = new DataColumn(m_strColChecked, typeof(bool));
            col.Caption = "";
            table.Columns.Add(col);
			
			table.Columns.Add(m_strColChamp);
			if (m_version.TypeVersion.Code != CTypeVersion.TypeVersion.Previsionnelle)
				table.Columns.Add(m_strColValeurHistorique);
			table.Columns.Add(m_strColValeurCourante);
			if (m_version.TypeVersion.Code == CTypeVersion.TypeVersion.Previsionnelle)
			{
				m_strColValeurHistorique = I.T("Version|123");
				table.Columns.Add(m_strColValeurHistorique);
			}
            table.Columns.Add(m_strColObjetHistorique, typeof(IChampPourVersion));

			CValeursHistorique valeurs = GetValeursHistorique(objet);
            m_valeursAffichees = valeurs;
            m_objetAffiche = objet;
			CStructureTable structure = CStructureTable.GetStructure(objet.GetType());
			foreach (CInfoChampTable champ in structure.Champs)
			{
				if (champ.Propriete != "")
				{
					DataRow row = table.NewRow();
                    row[m_strColChecked] = false;
					row[m_strColChamp] = champ.NomConvivial;
                    row[m_strColObjetHistorique] = DBNull.Value;

					if (champ.TypeDonnee == typeof(CDonneeBinaireInRow))
					{
						CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(objet.ContexteDonnee.IdSession, objet.Row.Row, champ.NomChamp);
						row[m_strColValeurCourante] = donnee.Donnees;
					}
					else
						row[m_strColValeurCourante] = objet.Row[champ.NomChamp];

					CChampPourVersionInDb champVersion = new CChampPourVersionInDb(champ.NomChamp, champ.NomConvivial);
					if (valeurs.ContainsKey(champVersion))
					{
                        row[m_strColObjetHistorique] = champVersion;
						object val = valeurs[champVersion];
						if (val == null)
							row[m_strColValeurHistorique] = DBNull.Value;
						else
							row[m_strColValeurHistorique] = val.ToString();
					}
					else
						row[m_strColValeurHistorique] = objet.Row[champ.NomChamp];
					table.Rows.Add(row);
				}
			}

			//Champs custom
			if (objet is IElementAChamps)
			{
				Dictionary<CChampCustom, bool> lstChamps = new Dictionary<CChampCustom, bool>();
				foreach (IDefinisseurChampCustom definisseur in ((IElementAChamps)objet).DefinisseursDeChamps)
				{
					foreach (CChampCustom champ in definisseur.TousLesChampsAssocies)
					{
						if (champ.TypeDonneeChamp.TypeDonnee != TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
							lstChamps[champ] = true;
					}
				}
				foreach (CChampCustom champCustom in lstChamps.Keys)
				{
					DataRow row = table.NewRow();
                    row[m_strColChecked] = false;
					row[m_strColChamp] = champCustom.LibelleConvivial;
                    row[m_strColObjetHistorique] = DBNull.Value;
					row[m_strColValeurCourante] = ((IElementAChamps)objet).GetValeurChamp(champCustom.Id);
					CChampCustomPourVersion chVersion = new CChampCustomPourVersion(champCustom);
					if (valeurs.ContainsKey(chVersion))
					{
                        row[m_strColObjetHistorique] = chVersion;
						object val = valeurs[chVersion];
						if (val == null)
							row[m_strColValeurHistorique] = DBNull.Value;
						else
							row[m_strColValeurHistorique] = val.ToString();
					}
					else
						row[m_strColValeurHistorique] = row[m_strColValeurCourante];
					table.Rows.Add(row);
				}
			}
			table.DefaultView.Sort = m_strColChamp;
			m_grilleProprietes.DataSource = table;
            m_grilleProprietes.ReadOnly = false;
            m_grilleProprietes.Columns[0].Width = 32;
            foreach (DataGridViewColumn colTmp in m_grilleProprietes.Columns)
                colTmp.ReadOnly = true;
            m_grilleProprietes.Columns[0].ReadOnly = false;
            if (!m_bCanRestore)
                m_grilleProprietes.Columns[0].Visible = false;
            m_grilleProprietes.Columns[m_grilleProprietes.Columns.Count - 1].Visible = false;
		}

		private void m_grilleProprietes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if ( e.RowIndex >= 0 && e.RowIndex < ((DataTable)m_grilleProprietes.DataSource).Rows.Count )
			{
				DataRowView row = ((DataTable)m_grilleProprietes.DataSource).DefaultView[e.RowIndex];
				if ( !row[m_strColValeurCourante].Equals(row[m_strColValeurHistorique] ))
					e.CellStyle.BackColor = Color.LightGreen;
			}
		}

		private void CControleDetailVersion_BackColorChanged(object sender, EventArgs e)
		{
			m_grilleProprietes.BackgroundColor = BackColor;
			m_lblInfo.BackColor = BackColor;
		}

		private void CControleDetailVersion_ForeColorChanged(object sender, EventArgs e)
		{
			m_lblInfo.ForeColor = ForeColor;
		}

        //--------------------------------------------------
        private void m_lnkRestore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!m_bCanRestore)
                return;
            DataTable table = m_grilleProprietes.DataSource as DataTable;
            if (table != null && m_objetAffiche != null && m_valeursAffichees != null)
            {
                using (CContexteDonnee contexte = new CContexteDonnee(m_objetAffiche.ContexteDonnee.IdSession, true, false))
                {
                    CObjetDonneeAIdNumerique objetAModifier = contexte.GetObjetInThisContexte(m_objetAffiche) as CObjetDonneeAIdNumerique;
                    List<DataRow> rowsSelected = new List<DataRow>();
                    foreach (DataRow row in table.Rows)
                        if ((bool)row[m_strColChecked])
                            rowsSelected.Add(row);
                    CResultAErreur result = CResultAErreur.True;
                    if (rowsSelected.Count > 0)
                    {
                        objetAModifier.BeginEdit();
                        foreach (DataRow row in rowsSelected)
                        {
                            IChampPourVersion champ = row[m_strColObjetHistorique] as IChampPourVersion;
                            if (champ != null)
                            {
                                IJournaliseurDonneesChamp journaliseur = CGestionnaireAChampPourVersion.GetJournaliseur(champ.TypeChampString);
                                if (journaliseur != null)
                                {
                                    result = journaliseur.AppliqueValeur(m_version.Id, champ, objetAModifier, m_valeursAffichees[champ]);
                                    if (!result)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (result)
                            result = objetAModifier.CommitEdit();
                        if (!result)
                        {
                            objetAModifier.CancelEdit();
                            CFormAlerte.Afficher(result.Erreur);
                        }
                        else
                            InitForObjet ( objetAModifier, m_version );
                    }
                }
            }
                            
        }
	}
}
