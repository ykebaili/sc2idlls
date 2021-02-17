using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.drawing;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Description résumée de CPanelEditionFormulaire.
	/// </summary>
	public class CPanelEditionFormulaire : sc2i.win32.common.CPanelEditionObjetGraphique
	{
		private Dictionary<Type, object> m_dicObjetsPourSerializerCloner = new Dictionary<Type,object>();
		private Type m_typeEdite = null;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
		private object m_entiteeEditee = null;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelEditionFormulaire()
		{
			InitializeComponent();
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
			sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
			sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
			this.SuspendLayout();
			// 
			// CPanelEditionFormulaire
			// 
			this.AutoScroll = true;
			cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
			cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
			cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
			cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
			cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
			this.GrilleAlignement = cGrilleEditeurObjetGraphique1;
			this.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
			this.Name = "CPanelEditionFormulaire";
			cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
			cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
			cProfilEditeurObjetGraphique1.HistorisationActive = true;
			cProfilEditeurObjetGraphique1.Marge = 10;
			cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
			cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
			cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
			this.Profil = cProfilEditeurObjetGraphique1;
			this.ToujoursAlignerSurLaGrille = false;
			this.SelectionChanged += new System.EventHandler(this.CPanelEditionFormulaire_SelectionChanged);
			this.AfterAddElements += new sc2i.win32.common.EventHandlerPanelEditionGraphiqueSuppression(this.CPanelEditionFormulaire_AfterAddElements);
			this.BeforeDeleteElement += new sc2i.win32.common.EventHandlerPanelEditionGraphiqueSuppression(this.CPanelEditionFormulaire_BeforeDeleteElement);
			this.ResumeLayout(false);

		}

		//-----------------------------------------------------------------------
		public Type TypeEdite
		{
			get
			{
				return m_typeEdite;
			}
			set
			{
				m_typeEdite = value;
			}
		}

		//-----------------------------------------------------------------------
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

		//-----------------------------------------------------------------------
		public object EntiteEditee
		{
			get
			{
				return m_entiteeEditee;
			}
			set
			{
				m_entiteeEditee = value;
			}
		}

		//-----------------------------------------------------------------------
		public void Init(Type typeEdite, object entiteEditee, IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			TypeEdite = typeEdite;
			EntiteEditee = entiteEditee;
			FournisseurProprietes = fournisseurProprietes;
		}

		//-------------------------------------------------------
		private bool CPanelEditionFormulaire_BeforeDeleteElement(List<I2iObjetGraphique> objs)
		{
			for (int n = objs.Count; n > 0; n--)
			{
				I2iObjetGraphique ele = objs[n - 1];
				if (ele.IsLock)
					objs.RemoveAt(n - 1);
			}

			return true;
		}
		#endregion

		//-------------------------------------------------------
		private bool CPanelEditionFormulaire_AfterAddElements( List<I2iObjetGraphique> nouveaux)
		{
			foreach (I2iObjetGraphique objet in nouveaux)
			{
				C2iWnd wnd = objet as C2iWnd;
				if (wnd != null)
					wnd.OnDesignCreate(m_typeEdite);
			}
			return true;
		}

		private void CPanelEditionFormulaire_SelectionChanged(object sender, EventArgs e)
		{
			foreach (C2iWnd element in Selection)
				element.OnDesignSelect(TypeEdite, EntiteEditee, FournisseurProprietes );
		}

		//-------------------------------------------------------
		public override void AddObjectsForClonerSerializer(Dictionary<Type, object> dic)
		{
			foreach (KeyValuePair<Type, object> keyVal in m_dicObjetsPourSerializerCloner)
				dic[keyVal.Key] = keyVal.Value;
		}

		//-------------------------------------------------------
		/// <summary>
		/// Permet d'ajouter des objets au Serialzer de clonage
		/// </summary>
		/// <param name="type"></param>
		/// <param name="objet"></param>
		public void AddObjetForClonerSerializer(Type type, object objet)
		{
			m_dicObjetsPourSerializerCloner[type] = objet;
		}

        private ToolStripMenuItem m_menuItemEdit = null;

        protected override void AfficherMenuAdditonnel(ContextMenuStrip menu)
        {
            if(m_menuItemEdit == null)
            {
                m_menuItemEdit = new ToolStripMenuItem(I.T("Edit Child Zone|20010"));
                menu.Items.Add(m_menuItemEdit);
                m_menuItemEdit.Name = "m_menuItemEdit";
                m_menuItemEdit.Size = new System.Drawing.Size(194, 22);
                m_menuItemEdit.Click += new EventHandler(m_menuItemEdit_Click);
            }

            if (Selection.Count == 1)
            {
                m_menuItemEdit.Visible = false;
                C2iWndZoneMultiple objetSelectonne = Selection[0] as C2iWndZoneMultiple;
                if (objetSelectonne != null)
                    m_menuItemEdit.Visible = true;
                else
                    m_menuItemEdit.Visible = false;
            }
        }

        void m_menuItemEdit_Click(object sender, EventArgs e)
        {
            EditObjetDeFormulaire(Selection[0] as C2iWnd);
        }

        private void EditObjetDeFormulaire(C2iWnd objetAEditer)
        {
            if (objetAEditer != null)
            {
                // Child zone
                C2iWndZoneMultiple childZone = objetAEditer as C2iWndZoneMultiple;
                if (childZone != null)
                {
                    // Editer la child Zone
                    childZone.EditZoneMultiple();
                }
            }
        }
	}
}
