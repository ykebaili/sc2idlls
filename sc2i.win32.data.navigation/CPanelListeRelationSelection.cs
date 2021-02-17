using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.Design;
using System.Drawing.Design;

using sc2i.win32.data;
using sc2i.data;
using sc2i.common;
using sc2i.win32.navigation;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CPanelListeRelationSelection.
	/// La liste est contenue dans un panel car le controle ListView ne possède pas
	/// d'évènements Load
	/// </summary>
	public delegate void AssocieDataEventHandler ( CObjetDonnee objet, CObjetDonnee relation, ref object dataAAssocier );
	public delegate void ObjetSelecionneeChangedEventHandler ( CObjetDonnee objet, object dataAssocie );
	[Serializable]
	public class CPanelListeRelationSelection : System.Windows.Forms.UserControl, IControlALockEdition
	{
		//table allant de CObjetDonnee->donnees associées au listviewitem.
		//Le cobjetdonné est l'élément affiché dans la liste.
		//Les données sont générallement les données à associer à la relation à cet objet
		//Les données sont associées par le message AssocieData
		private Hashtable m_tableDataAssocie  = new Hashtable();
		private Font m_fontBold;
		private CListeObjetsDonnees m_listeObjetsSource;
		private CListeObjetsDonnees m_listeRelationsSelectionnees;
		private IObjetAContexteDonnee m_objetConcerne;
		private string m_strPropObjetConcerne;
		private string m_strProprieteRetournantObjetSecondaire;
		private bool m_bIsInitWithParentForm = false;
		
		/*Indique que les relations indiquent des désélection
		 * les éléments relation seront  créés pour les éléments non cochés
		 */
		private bool	m_bExclusionParException = false;

		public bool m_bLockEdition;

		private CContexteDonnee m_contexte;

		//Table objetassocie -> relation associant l'objet
		private Hashtable m_hashtableObjets = new Hashtable();

		protected sc2i.win32.common.ListViewAutoFilled m_listView;
		private System.Windows.Forms.Timer m_timer;
		private System.ComponentModel.IContainer components;

		public CPanelListeRelationSelection()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
			m_fontBold = new Font(m_listView.Font, FontStyle.Bold);
		}
		//-------------------------------------------------------------------
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
		//-------------------------------------------------------------------
		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_listView = new sc2i.win32.common.ListViewAutoFilled();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_listView
            // 
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listView.EnableCustomisation = true;
            this.m_listView.FullRowSelect = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(8, 8);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(368, 280);
            this.m_listView.TabIndex = 0;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            this.m_listView.SelectedIndexChanged += new System.EventHandler(this.m_listView_SelectedIndexChanged);
            this.m_listView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_listView_ItemCheck);
            // 
            // m_timer
            // 
            this.m_timer.Interval = 300;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // CPanelListeRelationSelection
            // 
            this.Controls.Add(this.m_listView);
            this.Name = "CPanelListeRelationSelection";
            this.Size = new System.Drawing.Size(384, 296);
            this.Load += new System.EventHandler(this.CPanelListeRelationSelection_Load);
            this.ResumeLayout(false);

		}
		#endregion
		//-------------------------------------------------------------------
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				m_listView.CheckBoxes = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition(this, new EventArgs());
			}
		}


		public bool ExclusionParException
		{
			get
			{
				return m_bExclusionParException;
			}
			set
			{
				m_bExclusionParException = value;
			}
		}
		//-------------------------------------------------------------------
		public bool ShouldSerializeColumns()
		{ return true; }
		public void ResetColums()
		{}

		//-------------------------------------------------------------------
		[
		Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
		]
		public ListViewAutoFilledColumnsCollection Columns
		{
			get
			{
				return m_listView.Colonnes;
			}
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Initialise la liste
		/// </summary>
		/// <param name="listeObjetsSource">Liste d'objet sélectionnables</param>
		/// <param name="listeRelationsSelectionnees">Liste renvoyant les objets sélectionnés</param>
		/// <param name="objetConcerne">Objet définissant les objets sélectionnés</param>
		/// <param name="strPropObjetConcerne">"Propriété de l'objet relation indiquant l'objet concerné</param>
		/// <param name="strProp2">"Propriété de l'objet relation indiquant l'objet sélectionné</param>
		public void Init(
			CListeObjetsDonnees listeObjetsSource, 
			CListeObjetsDonnees listeRelationsSelectionnees,
			IObjetAContexteDonnee objetConcerne,
			string strPropObjetConcerne,
			string strProprieteRetournantObjetSecondaire)
		{
			listeObjetsSource.AppliquerFiltreAffichage = true;
			if ( !m_bIsInitWithParentForm )
			{
				if ( ParentForm != null )
				{
					m_listView.ReadFromRegistre(new CSc2iWin32DataNavigationRegistre().GetKey("Preferences\\Panel_Listes\\" + this.ParentForm.GetType().Name + "_" + this.Name, true));

					this.ParentForm.Closing += new CancelEventHandler( ParentForm_ClosingAttitude );
					m_bIsInitWithParentForm = true;
				}
			}
			int nLastSelected = -1;
			if ( m_listView.SelectedItems.Count != 0 )
				nLastSelected = m_listView.SelectedItems[0].Index;
			m_listeObjetsSource = listeObjetsSource;
			m_listeRelationsSelectionnees = listeRelationsSelectionnees;
			m_objetConcerne = objetConcerne;
			m_strPropObjetConcerne = strPropObjetConcerne;
			m_strProprieteRetournantObjetSecondaire = strProprieteRetournantObjetSecondaire;
			m_contexte = objetConcerne.ContexteDonnee;

			if ( OnAssocieData !=  null )
			{
				foreach ( CObjetDonnee objet in listeObjetsSource )
				{
					object data = null;
					OnAssocieData ( objet, null, ref data );
					m_tableDataAssocie[objet] = data;
				}
			}

			CopyListToHashtable();
			if ( nLastSelected >= 0 && nLastSelected < m_listView.Items.Count )
				m_listView.Items[nLastSelected].Selected = true;
			m_listView_SelectedIndexChanged ( this, new EventArgs() );
			RemplirGrille();
		}

		//-------------------------------------------------------------------
		public event AssocieDataEventHandler OnAssocieData;

		//-------------------------------------------------------------------
		//retourne les données associées à l'objet
		public object GetDataAssocie ( CObjetDonnee objetParent )
		{
			if ( objetParent == null )
				return null;
			else
				return m_tableDataAssocie[objetParent];
		}

		//-------------------------------------------------------------------
		//retourne les données associées à l'objet
		public void SetDataAssocie( CObjetDonnee objetParent, object data )
		{
			m_tableDataAssocie[objetParent] = data;
		}

		//-------------------------------------------------------------------
		private void CopyListToHashtable()
		{
			m_hashtableObjets.Clear();
			if (m_listeRelationsSelectionnees.Count<1)
				return;
			foreach(CObjetDonnee rel in m_listeRelationsSelectionnees)
			{
				object obj = CInterpreteurTextePropriete.GetValue(rel, m_strProprieteRetournantObjetSecondaire);
				m_hashtableObjets[obj] = rel;
				if ( OnAssocieData != null )
				{
					object data = null;
					OnAssocieData ( (CObjetDonnee)obj, rel, ref data );
					m_tableDataAssocie[obj] = data;
				}
			}
		}
		//-------------------------------------------------------------------
		public void RemplirGrille()
		{
			m_listView.CheckBoxes = !LockEdition;
			m_listView.BeginUpdate();
			try
			{
				if ( m_listeObjetsSource != null )
				{
					CFiltreData oldFiltre = m_listeObjetsSource.Filtre;
					bool bIsAvance = oldFiltre is CFiltreDataAvance;
					if ( LockEdition )
					{
						string strIds = "";
						string strChampId = "";
						string strTable = "";
						foreach ( CObjetDonneeAIdNumeriqueAuto objet in m_hashtableObjets.Keys )
						{
							strIds += objet.Id.ToString()+(bIsAvance?";":",");
							if ( strChampId == "" )
							{
								strChampId = objet.GetChampId();
								strTable = objet.GetNomTable();
							}
						}
						if ( strIds.Length == 0 )
						{
							m_listView.Items.Clear();
							return;
						}
						strIds = strIds.Substring(0, strIds.Length-1);
						CFiltreData filtre;
						if ( bIsAvance )
							filtre = new CFiltreDataAvance ( 
								strTable,
								strChampId+" in {"+ strIds+"}");
						else
							filtre = new CFiltreData ( 
								strChampId+" in ("+strIds+")");
						m_listeObjetsSource.Filtre = CFiltreData.GetAndFiltre ( oldFiltre, filtre );
					}
					m_listView.Remplir(m_listeObjetsSource);
					if ( LockEdition )
					{
						m_listeObjetsSource.Filtre = oldFiltre;
						return;
					}

					foreach(ListViewItem item in m_listView.Items)
					{
						CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) item.Tag;
						CObjetDonnee relation = (CObjetDonnee)m_hashtableObjets[objet];
						bool bCheck = relation!=null;
						if ( ExclusionParException ) 
							bCheck = !bCheck;
						if (bCheck)
							item.Checked = true;
						else
						{
							if (m_listView.CheckBoxes==false)
								item.Remove();
						}
					}
				}
			}
			finally
			{
				m_listView.EndUpdate();
			}
		}

		//-------------------------------------------------------------------
		public event AssocieDataEventHandler OnValideRelation;
		//-------------------------------------------------------------------
		public void ApplyModifs()
		{
			if ( LockEdition )
				return;
			foreach(ListViewItem item in m_listView.Items)
			{
				CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) item.Tag;
				bool bCreate = item.Checked;
				if ( ExclusionParException )
					bCreate = !bCreate;
				if (bCreate)
				{
					CObjetDonnee relation = (CObjetDonnee)m_hashtableObjets[objet];
					if (relation == null)
					{
						relation = (CObjetDonnee) Activator.CreateInstance( m_listeRelationsSelectionnees.TypeObjets, new object[] {m_contexte});
						relation.CreateNewInCurrentContexte(null);
						CInterpreteurTextePropriete.SetValue(relation, m_strPropObjetConcerne, m_objetConcerne);
						CInterpreteurTextePropriete.SetValue(relation, m_strProprieteRetournantObjetSecondaire, objet);
						m_hashtableObjets[objet] = relation;
					} 
					if ( OnValideRelation != null )
					{
						object data = GetDataAssocie ( objet );
                        if ( OnSelectionChanged != null )
                            OnSelectionChanged(objet, data);
                        data = GetDataAssocie(objet);
						OnValideRelation ( objet, relation, ref data );
						SetDataAssocie ( objet, data );
					}
				}
				else
				{
					if (m_hashtableObjets[objet] != null)
					{
						((CObjetDonnee)m_hashtableObjets[objet]).Delete();
						m_hashtableObjets[objet] = null;
					}
				}
			}
		}
		//-------------------------------------------------------------------
		public void ReloadForEdition(CListeObjetsDonnees listeRelations, CContexteDonnee ctx)
		{
			m_listeRelationsSelectionnees = listeRelations;
            m_contexte = ctx;
			CopyListToHashtable();
			RemplirGrille();
		}
		//-------------------------------------------------------------------
		protected void CPanelListeRelationSelection_Load(object sender, System.EventArgs e)
		{
			/*if ( !DesignMode )
			{
				m_listView.ReadFromRegistre(new CSc2iWin32DataNavigationRegistre().GetKey("Preferences\\Panel_Listes\\" + this.ParentForm.GetType().Name + "_" + this.Name, true));

				this.ParentForm.Closing += new CancelEventHandler( ParentForm_ClosingAttitude );
			}*/
		}
		//---------------------------------------------------------------------------
		private void ParentForm_ClosingAttitude(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( ParentForm != null )
				m_listView.WriteToRegistre(new CSc2iWin32DataNavigationRegistre().GetKey("Preferences\\Panel_Listes\\" + this.ParentForm.GetType().Name + "_" + this.Name, true));
		}
		//-------------------------------------------------------------------
		public event EventHandler OnItemCheck;

		private void m_listView_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			m_timer.Enabled = true;
		}
		//-------------------------------------------------------------------
		private void m_timer_Tick(object sender, System.EventArgs e)
		{
			m_timer.Enabled = false;
			CorrigeModifsPossibles();
			if (OnItemCheck!=null)
				OnItemCheck(sender,e);
		}

		//-------------------------------------------------------------------
		private void CorrigeModifsPossibles()
		{
			foreach(ListViewItem item in m_listView.Items)
			{
				CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) item.Tag;
				bool bCreate = item.Checked;
				if ( ExclusionParException )
					bCreate = !bCreate;
				if (!bCreate)
				{
					if (m_hashtableObjets[objet] != null)
					{
						CObjetDonnee obj = (CObjetDonnee)m_hashtableObjets[objet];
						if ( !obj.CanDelete() )
						{
							item.Checked = !ExclusionParException;
						}
					}
				}
			}
		}

		public event ObjetSelecionneeChangedEventHandler OnSelectionChanged;
		//-------------------------------------------------------------------
		private void m_listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( OnSelectionChanged != null  && m_listView.SelectedItems.Count == 1)
			{
				ListViewItem item = m_listView.SelectedItems[0];
				CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) item.Tag;	
				if ( OnSelectionChanged != null )
					OnSelectionChanged ( objet, GetDataAssocie ( objet ) );
			}
		}

		//-------------------------------------------------------------------
		public bool EnableCustomisation
		{
			get
			{
				return  m_listView.EnableCustomisation;
			}
			set
			{
				m_listView.EnableCustomisation = value;
				
			}
		}
	}
}
