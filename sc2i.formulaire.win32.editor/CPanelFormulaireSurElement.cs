using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.expression;

using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire.win32;
using System.Collections.Generic;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Description résumée de CPanelShowFormulaire.
	/// </summary>
	public class CPanelFormulaireSurElement : System.Windows.Forms.UserControl, IControlALockEdition
	{
        public delegate void GetCreateurForObjectDelegate(object objet, ref CCreateur2iFormulaireV2 createur);

		private CCreateur2iFormulaireV2 m_createur = null;
		private Panel m_panelFormulaire = null;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.ComponentModel.IContainer components;

        private static List<GetCreateurForObjectDelegate> m_listGetCreateurs = new List<GetCreateurForObjectDelegate>();

		private bool m_bLockEdition = false;

        //----------------------------------------------------------------------
        public CPanelFormulaireSurElement()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

		}

        //----------------------------------------------------------------------
        public static void AddGetCreateur ( GetCreateurForObjectDelegate del )
        {
            m_listGetCreateurs.Add ( del );
        }

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
        //----------------------------------------------------------------------
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				if ( m_panelFormulaire != null )
				{
					m_panelFormulaire.Dispose();
					m_panelFormulaire = null;
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
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            // 
			// CPanelShowFormulaire
			// 
			this.Name = "CPanelShowFormulaire";
			this.Size = new System.Drawing.Size(424, 176);

		}
		#endregion




		public void InitPanel ( C2iWnd formulaire, object elementEdite )
		{
            if (elementEdite == null)
                return;

            C2iWndFenetre fenetre = formulaire as C2iWndFenetre;
            bool bAutoSize = false;
            if (fenetre != null)
                bAutoSize = fenetre.AutoSize;

			m_createur = null;
			C2iWnd wnd = formulaire;
			//Supprime le panel stockant les controls
			if ( m_panelFormulaire != null )
			{
				m_panelFormulaire.Visible = false;
				m_panelFormulaire.Dispose();
				m_panelFormulaire = null;
			}
			if ( wnd != null )
			{
				m_panelFormulaire = new Panel();
				m_panelFormulaire.Parent = this;
                if (bAutoSize)
                {
                    m_panelFormulaire.Dock = DockStyle.Top;
                    AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    AutoSize = true;
                    m_panelFormulaire.AutoSizeMode = AutoSizeMode;
                    m_panelFormulaire.AutoSize = AutoSize;
                }
                else
                {
                    m_panelFormulaire.Dock = DockStyle.Top;
                    m_panelFormulaire.AutoSize = false;
                    m_panelFormulaire.Height = formulaire.Size.Height;
                    
                }
				m_panelFormulaire.BackColor = wnd.BackColor;
				m_panelFormulaire.ForeColor = wnd.ForeColor;
				m_panelFormulaire.Visible = true;
				m_panelFormulaire.Font = wnd.Font;

                foreach ( GetCreateurForObjectDelegate del in m_listGetCreateurs )
                {
                    del ( elementEdite, ref m_createur );
                    if ( m_createur != null )
                        break;
                }
                if(  m_createur == null )
/*
                if (objetDonnee != null)
                    m_createur = new CCreateur2iFormulaireObjetDonnee(objetDonnee.ContexteDonnee.IdSession);
                else*/
                    m_createur = new CCreateur2iFormulaireV2();

				IFournisseurProprietesDynamiques fournisseur = m_createur.FournisseurProprietes;
                if ( elementEdite is IFournisseurProprietesDynamiques )
					fournisseur = (IFournisseurProprietesDynamiques)elementEdite;
                if (fournisseur == null)
                    fournisseur = new CFournisseurGeneriqueProprietesDynamiques();
				m_createur.ControleValeursAvantValidation = true;
				m_createur.CreateControlePrincipalEtChilds( m_panelFormulaire, wnd, fournisseur );
				m_createur.ElementEdite = elementEdite;
				m_createur.LockEdition = m_bLockEdition;
			}
		}

		//-----------------------------------------------------
		public CResultAErreur AffecteValeursToElement(object element)
		{
			m_createur.SetElementEditeSansChangerLesValeursAffichees ( element );
			return AffecteValeursToElement();
		}

		//-----------------------------------------------------
		public CResultAErreur AffecteValeursToElement (  )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_createur == null )
				return result;
			return m_createur.MAJ_Champs ( );
		}

		//-----------------------------------------------------
		public object ElementEdite
		{
			get
			{
				if ( m_createur == null )
					return null;
				return m_createur.ElementEdite;
			}
			set
			{
				if (m_createur != null)
					m_createur.ElementEdite = value;
			}
		}

		#region IControlALockEdition Membres

		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				if ( m_createur != null )
					m_createur.LockEdition = value;
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion
	}
}
