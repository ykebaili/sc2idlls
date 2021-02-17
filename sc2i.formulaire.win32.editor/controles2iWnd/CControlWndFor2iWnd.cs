using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common;
using sc2i.expression;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.common.Restrictions;




namespace sc2i.formulaire.win32
{
	/// <summary>
	/// Gère les fonctions de base pour les IControlWndFor2iWnd.
	/// Notamment, gère les fils et la transmission d'informations à ceux-ci,
	/// stocke le parent, l'élément édité et la WndAssociée
	/// </summary>
	public abstract class CControlWndFor2iWnd : IControleWndFor2iWnd
	{
		private IControleWndFor2iWnd m_ctrlWndParent = null;
        private IRuntimeFor2iWnd[] m_controlesFils = new IRuntimeFor2iWnd[0];
		private C2iWnd m_wndAssociee = null;
		private object m_elementEdite;
        private CCreateur2iFormulaireV2 m_createur;
        private IFournisseurProprietesDynamiques m_fournisseurProprietes;

        

		//---------------------------------------------
		public virtual bool LockEdition
		{
			get
			{
				IControlALockEdition ctrl = this.Control as IControlALockEdition;
				if (ctrl != null)
					return ctrl.LockEdition;
				else
					return this.Control == null ? false : !this.Control.Enabled;
			}
			set
			{
                bool bVal = value;
				IControlALockEdition ctrl = this.Control as IControlALockEdition;
				if (ctrl != null)
					ctrl.LockEdition = value;
				else if ( this.Control != null )
					this.Control.Enabled = !value;

				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		public event EventHandler OnChangeLockEdition;

        /// <summary>
        /// Indique dans quel mode d'édition se trouve mon parent
        /// </summary>
        /// <param name="bModeEdition"></param>
        public virtual void OnChangeParentModeEdition(bool bModeEdition)
        {
            bool bMyModeEdition = bModeEdition;
            if (m_wndAssociee != null && m_wndAssociee.LockMode == C2iWnd.ELockMode.DisableOnEdit)
                bMyModeEdition = !bModeEdition;
            if (WndAssociee != null && WndAssociee.LockMode != C2iWnd.ELockMode.Independant)
                LockEdition = !bMyModeEdition;
            foreach (IControleWndFor2iWnd ctrlFils in Childs)
                ctrlFils.OnChangeParentModeEdition(bMyModeEdition);
        }

		//---------------------------------------------
		/// <summary>
		/// Objet C2iWnd d'où a été créé ce contrôle
		/// </summary>
		public C2iWnd WndAssociee
		{
			get
			{
				return m_wndAssociee;
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Définit l'élément édité par le contrôle
		/// </summary>
		/// <param name="element"></param>
		public virtual void SetElementEdite(object element)
		{
			m_elementEdite = element;
			foreach (IControleWndFor2iWnd fils in Childs)
				fils.SetElementEdite(element);
			OnChangeElementEdite(element);
			CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
		}

        //---------------------------------------------
        public virtual void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            m_elementEdite = element;
            foreach (IControleWndFor2iWnd fils in Childs)
                fils.SetElementEditeSansChangerLesValeursAffichees(element);
        }

		//---------------------------------------------
		public object EditedElement
		{
			get
			{
				return m_elementEdite;
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Indique l'élement édité
		/// </summary>
		/// <param name="element"></param>
		protected abstract void OnChangeElementEdite(object element);


        //---------------------------------------------
        public virtual bool IsRacineForEvenements
        {
            get
            {
                return false;
            }
        }
		//---------------------------------------------
        public virtual IWndAContainer WndContainer
		{
			get
			{
				return m_ctrlWndParent;
			}

			set
			{
				m_ctrlWndParent = value as IControleWndFor2iWnd;
			}
		}


		//---------------------------------------------
		public virtual void CreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			m_wndAssociee = wnd;
            m_createur = createur;
            m_fournisseurProprietes = fournisseurProprietes;
			MyCreateControle(createur, wnd, parent, fournisseurProprietes);
			if ( Control != null )
			{
				m_controlesFils = createur.CreateChilds(wnd, Control, fournisseurProprietes);
				foreach (IControleWndFor2iWnd ctrl in Childs)
					ctrl.WndContainer = this;
				AfterCreateChilds();
			}
            OnChangeElementEdite(m_elementEdite);//!createur.LockEdition);
		}

		//---------------------------------------------
		/// <summary>
		/// Est executé lorsque les fils du contrôle ont été créés
		/// </summary>
		protected virtual void AfterCreateChilds()
		{
		}

		//---------------------------------------------
		/// <summary>
		/// Implémente la logique de construction du controle associé
		/// </summary>
		/// <param name="createur"></param>
		/// <param name="wnd"></param>
		/// <param name="parent"></param>
		/// <param name="fournisseur"></param>
		protected abstract void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseur);


		//---------------------------------------------
		/// <summary>
		/// Retourne le contrôle Win32 associé à cet objet
		/// </summary>
		public abstract Control Control{get;}

		//---------------------------------------------
		/// <summary>
		/// Met à jour l'objet avec le contrôle
		/// </summary>
		/// <param name="bControlerLesValeursAvantValidation"></param>
		/// <returns></returns>
		public virtual CResultAErreur MajChamps(bool bControlerLesValeursAvantValidation)
		{
			CResultAErreur result = MyMajChamps(bControlerLesValeursAvantValidation) ;
			foreach (IControleWndFor2iWnd fils in Childs)
				result += fils.MajChamps(bControlerLesValeursAvantValidation);
			return result;
		}

		//---------------------------------------------
		/// <summary>
		/// Met à jour l'objet édité avec par le contrôle
		/// </summary>
		/// <param name="bControlerLesValeursAvantValidation"></param>
		/// <returns></returns>
		protected abstract CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation);


        //---------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }
		//---------------------------------------------
		/// <summary>
		/// REcalcule éventuellement le contenu du contrôle
		/// </summary>
		public virtual void UpdateValeursCalculees()
		{
			MyUpdateValeursCalculees();
			foreach (IControleWndFor2iWnd fils in Childs)
				fils.UpdateValeursCalculees();
		}

		//---------------------------------------------
		protected abstract void MyUpdateValeursCalculees();

		//---------------------------------------------
		/// <summary>
		/// Retourne tous les fils de cet élément
		/// </summary>
        public virtual IRuntimeFor2iWnd[] Childs
		{
			get
			{
				return m_controlesFils;
			}
		}

        //---------------------------------------------
        public virtual IControleWndFor2iWnd Parent
        {
            get
            {
                return m_ctrlWndParent;
            }
        }

        //---------------------------------------------
        protected virtual void SetControlesFils(IRuntimeFor2iWnd[] childs)
        {
            m_controlesFils = childs;
        }


		//---------------------------------------------
		/// <summary>
		/// Applique les restrictions
		/// </summary>
		/// <param name="listeRestriction"></param>
		public void AppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
                IGestionnaireReadOnlySysteme gestionnaireReadOnly
                )
		{
			MyAppliqueRestriction(restrictionSurObjetEdite, listeRestrictions, gestionnaireReadOnly);
			foreach (IControleWndFor2iWnd child in Childs)
				child.AppliqueRestriction(restrictionSurObjetEdite, listeRestrictions, gestionnaireReadOnly);
		}

		//---------------------------------------------------------------
		protected abstract void MyAppliqueRestriction ( 
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly);

        //---------------------------------------------------------------
        public C2iWnd[] ChildControls
        {
            get
            {
                if (WndAssociee == null)
                    return new C2iWnd[0];
                List<C2iWnd> lst = new List<C2iWnd>();
                foreach (C2iWnd child in WndAssociee.Childs)
                    lst.Add(child);
                return lst.ToArray();
            }
            set
            {
                if (Control == null)
                    return;
                if (WndAssociee == null)
                    return;
                Control.SuspendDrawing();
                //Supprime tous les controles présents
                foreach (Control ctrl in Control.Controls)
                {
                    ctrl.Visible = false;
                    ctrl.Parent = null;
                    ctrl.Dispose();

                }
                Control.Controls.Clear();
                List<C2iWnd> lstTmp = new List<C2iWnd>();
                foreach (C2iWnd child in WndAssociee.Childs)
                    lstTmp.Add(child);
                foreach (C2iWnd child in lstTmp)
                    WndAssociee.RemoveChild(child);
                if (value == null)
                    return;
                foreach (C2iWnd child in value)
                {
                    WndAssociee.AddChild(child);
                    child.Parent = WndAssociee;
                }
                m_controlesFils = m_createur.CreateChilds(WndAssociee, Control, m_fournisseurProprietes);
                m_createur.UpdateVisibilityEtEnable(this, EditedElement);
                this.LockEdition = LockEdition;
                Control.ResumeDrawing();
                SetElementEdite(EditedElement);
                UpdateValeursCalculees();
            }
        }

        public IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees()
        {
            return new CEncaspuleurControleWndForFormules(this);
        }

        public ToolTip Tooltip { get; set; }

	}
		
}
