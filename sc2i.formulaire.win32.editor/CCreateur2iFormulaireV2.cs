using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using sc2i.win32.common;
using System.Drawing;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32
{
	public delegate void AfterModifyControlEventHandler(Control control, C2iWnd wnd, object objetEdite);
	public class CCreateur2iFormulaireV2 : IDisposable
	{
        System.ComponentModel.IContainer m_componentContainer = new System.ComponentModel.Container();
		private bool m_bControlerValeursAvantValidation = false;

		/// <summary>
		/// element en cours d'édition
		/// </summary>
		private object m_elementEdite = null;

        private DateTime? m_lastDateInitChamps = null;

		/// <summary>
		/// Timer pour la mise à jour automatique des données calculées
		/// </summary>
		private System.Windows.Forms.Timer m_timer = null;

		///Indique si le formulaire est verouillé ou pas
		private bool m_bLockEdition = false;

		///Indique si le formulaire est en lecture seul. Dans ce cas,
		///le lock Edition n'a aucun effet
		private bool m_bReadOnly = false;

		//Contrôle principal de l'édition
		private List<IControleWndFor2iWnd> m_controlesPrincipaux = new List<IControleWndFor2iWnd>();


		//Fournisseur de proprietes
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;

        //ToolTip
        private ToolTip m_tooltip = null;


		//-----------------------------------------------------------
		public CCreateur2iFormulaireV2( )
		{
		}

        //-----------------------------------------------------------
        public void Dispose()
        {
            if (m_componentContainer != null)
                m_componentContainer.Dispose();
            m_componentContainer = null;
        }


		#region allocation dynamique d'objets
		/// <summary>
		/// Type de C2IWnd->Controle windows instanciant cet élément
		/// </summary>
		private static Dictionary<Type, Type> m_dicWndToEditeur = new Dictionary<Type, Type>();

		public static void RegisterEditeur(Type typeWnd, Type typeControle)
		{
            if (typeof(IWndIncluableDansDataGrid).IsAssignableFrom(typeWnd) &&
                !typeof(IControlIncluableDansDataGrid).IsAssignableFrom(typeControle))
            {
                throw new Exception(typeControle.ToString() + " should implement " + typeof(IControlIncluableDansDataGrid).ToString());
            }
			m_dicWndToEditeur[typeWnd] = typeControle;
		}


		//-----------------------------------------------
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseurProprietes;
			}
		}

		//-----------------------------------------------
		public virtual IControleWndFor2iWnd CreateControle(
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			m_fournisseurProprietes = fournisseurProprietes;
			if (wnd == null)
				return null;
            if (m_tooltip == null && m_componentContainer != null)
            {
                m_tooltip = new ToolTip(m_componentContainer);
            }
			Type tpControle = null;
			if (!m_dicWndToEditeur.TryGetValue(wnd.GetType(), out tpControle))
				return null;
			object objControle = Activator.CreateInstance(tpControle);
			IControleWndFor2iWnd ctrl = objControle as IControleWndFor2iWnd;
			if (ctrl == null)
				return null;
			ctrl.CreateControle(this, wnd, parent, fournisseurProprietes);
			if (ctrl != null && ctrl.Control != null)
				if (AfterCreateControl != null)
					AfterCreateControl(ctrl.Control, wnd, ElementEdite);
            if (ctrl.Control != null && ctrl.Control.Parent != null)
            {
                ctrl.Control.BringToFront();
                m_tooltip.SetToolTip(ctrl.Control, wnd.HelpText);
            }
			return ctrl;
		}

        //-----------------------------------------------
        public void SetTooltip(Control ctrl, string strText)
        {
            if (m_tooltip != null && ctrl != null)
                m_tooltip.SetToolTip(ctrl, strText);
        }

		//-----------------------------------------------
		public List<IControleWndFor2iWnd> ControlesPrincipaux
		{
			get
			{
				return m_controlesPrincipaux;
			}
		}

		//-----------------------------------------------
		public static void AffecteProprietesCommunes(C2iWnd wnd, Control ctrlEdit)
		{
			ctrlEdit.BackColor = wnd.BackColor;
			ctrlEdit.ForeColor = wnd.ForeColor;
			ctrlEdit.Font = wnd.Font;
			ctrlEdit.Size = wnd.Size;
			ctrlEdit.Location = wnd.Position;
			AnchorStyles anchorStyle = AnchorStyles.None;
			if (wnd.AnchorLeft)
				anchorStyle |= AnchorStyles.Left;
			if (wnd.AnchorRight)
				anchorStyle |= AnchorStyles.Right;
			if (wnd.AnchorTop)
				anchorStyle |= AnchorStyles.Top;
			if (wnd.AnchorBottom)
				anchorStyle |= AnchorStyles.Bottom;
			ctrlEdit.Anchor = anchorStyle;
			ctrlEdit.TabIndex = Math.Max(0,wnd.TabOrder);
		}

		//-----------------------------------------------
		private void PositionneAvecAncres(Control controlFils, Control controlParent,
			C2iWnd wndFils, C2iWnd wndParent)
		{
			if (controlFils != null && controlParent != null &&
				wndFils != null && wndParent != null)
			{
				int nX1, nX2, nY1, nY2;
				nX1 = wndFils.Position.X;
				nX2 = wndFils.Position.X + wndFils.Size.Width;
				nY1 = wndFils.Position.Y;
				nY2 = wndFils.Position.Y + wndFils.Size.Height;

				if (wndFils.AnchorRight)
				{
					int nDistance = wndParent.Size.Width - nX2;
					nX2 = controlParent.Size.Width - nDistance;
					if (!wndFils.AnchorLeft)
						nX1 = nX2 - wndFils.Size.Width;
				}
				if (wndFils.AnchorBottom)
				{
					int nDistance = wndParent.Size.Height - nY2;
					nY2 = controlParent.Size.Height - nDistance;
					if (!wndFils.AnchorTop)
						nY1 = nY2 - wndFils.Size.Height;
				}

				if ( !wndFils.AnchorLeft && !wndFils.AnchorRight )
				{
					try
					{
						nX1 = (int)(((double)controlParent.Size.Width/(double)wndParent.Size.Width)*(double)nX1);
						nX2 = nX1 + wndFils.Size.Width;
					}
					catch{}
				}
				if ( !wndFils.AnchorTop && !wndFils.AnchorBottom )
				{
					try
					{
						nY1 = (int)(((double)controlParent.Size.Height/(double)wndParent.Size.Height)*(double)nY1);
						nY2 = nY1 + wndFils.Size.Width;
					}
					catch
					{
					}
				}
				controlFils.Location = new Point(nX1, nY1);
				controlFils.Size = new Size(Math.Max(nX2 - nX1, 1), Math.Max(nY2 - nY1, 1));
			}
		}


			

		//-----------------------------------------------
		public virtual IControleWndFor2iWnd[] CreateChilds(C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			List<IControleWndFor2iWnd> controles = new List<IControleWndFor2iWnd>();
			foreach (C2iWnd fille in wnd.Childs)
			{
				IControleWndFor2iWnd ctrl = CreateControle(fille, parent, fournisseurProprietes);
				if (ctrl != null)
				{
					controles.Add(ctrl);
					if (ctrl.Control != null)
					{
						PositionneAvecAncres(ctrl.Control, parent, fille, wnd);
						Control control = ctrl.Control;
						switch (fille.DockStyle)
						{
							case EDockStyle.None:
								control.Dock = DockStyle.None;
								break;
							case EDockStyle.Top:
								control.Dock = DockStyle.Top;
								break;
							case EDockStyle.Bottom:
								control.Dock = DockStyle.Bottom;
								break;
							case EDockStyle.Left:
								control.Dock = DockStyle.Left;
								break;
							case EDockStyle.Right:
								control.Dock = DockStyle.Right;
								break;
							case EDockStyle.Fill:
								control.Dock = DockStyle.Fill;
								break;
						}
					}
				}
			}
			//parent.ResumeDrawing();
			return controles.ToArray();
		}
		#endregion

		//------------------------------------------------------------------------------
		/// <summary>
		/// Indique que les valeurs de formulaire doivent être vérifiées avant de valider
		/// le formulaire
		/// </summary>
		public virtual bool ControleValeursAvantValidation
		{
			get
			{
				return m_bControlerValeursAvantValidation;
			}
			set
			{
				m_bControlerValeursAvantValidation = value;
			}
		}

		//------------------------------------------------------------------------------
		/// <summary>
		/// Change l'élement édité, sans modifier l'affichage. A n'utiliser que
		/// dans des cas bien précis qui justifient ce comportement. Ca peut être
		/// dangereux !
		/// </summary>
		/// <param name="elt"></param>
		public virtual void SetElementEditeSansChangerLesValeursAffichees(object elt)
		{
			m_elementEdite = elt;
            foreach (IControleWndFor2iWnd ctrl in m_controlesPrincipaux)
            {
                ctrl.SetElementEditeSansChangerLesValeursAffichees(m_elementEdite);
                
            }
		}

		// /////////////////////////////////////////////////////
		/// <summary>
		/// Obtient ou définit l'élément édité. Le changement d'élement édité
		/// provoque la perte de toutes les données du formulaire. Il est
		/// important de faire un MajChamps avant d'appeller cette fonction
		/// </summary>
		public virtual object ElementEdite
		{
			get
			{
				return m_elementEdite;
			}
			set
			{
                if (m_elementEdite != value || m_lastDateInitChamps == null ||
                    ((TimeSpan)(DateTime.Now-m_lastDateInitChamps.Value)).TotalMilliseconds > 500)
                {
                    m_elementEdite = value;
                    InitChamps();
                    m_lastDateInitChamps = DateTime.Now;
                    if (AfterChangeElementEdite != null)
                    {
                        foreach (IControleWndFor2iWnd ctrl in m_controlesPrincipaux)
                        {
                            CallAfterChangeElementEdite(ctrl);
                        }
                    }
                    foreach (IControleWndFor2iWnd ctrl in m_controlesPrincipaux)
                        UpdateVisibilityEtEnable(ctrl, m_elementEdite);
                }
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private void CallAfterChangeElementEdite(IControleWndFor2iWnd controleForWnd)
		{
			if (controleForWnd != null)
			{
				if (controleForWnd.Control != null)
					AfterChangeElementEdite(controleForWnd.Control, controleForWnd.WndAssociee, ElementEdite);
				foreach (IControleWndFor2iWnd ctrlFils in controleForWnd.Childs)
					CallAfterChangeElementEdite(ctrlFils);
			}

		}

		//////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Met à jour les contrôles affichés avec l'élément en cours d'édition
		/// </summary>
		public virtual void InitChamps()
		{
			foreach ( IControleWndFor2iWnd ctrl in m_controlesPrincipaux )
				ctrl.SetElementEdite ( m_elementEdite );
		}

		//////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Se déclenche après chaque création de contrôle
		/// </summary>
		public static event AfterModifyControlEventHandler AfterCreateControl;
		/// <summary>
		/// se déclenche lorsque l'élément édité a changé
		/// </summary>
		public static event AfterModifyControlEventHandler AfterChangeElementEdite;
		
		// ////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Création des contrôles windows
		/// </summary>
		/// <param name="controlParent"></param>
		/// <param name="wndParent"></param>
		/// <param name="tooltip"></param>
		/// <returns></returns>
		public virtual CResultAErreur CreateControlePrincipalEtChilds(
			Control controlParent, 
			C2iWnd wndParent, 
			IFournisseurProprietesDynamiques fournisseur)
		{
            using (CWaitCursor waiter = new CWaitCursor())
            {
                controlParent.SuspendDrawing();
                CResultAErreur result = CResultAErreur.True;
                m_controlesPrincipaux.Clear();
                IControleWndFor2iWnd ctrlPrincipal = CreateControle(wndParent, controlParent, fournisseur);
                if (ctrlPrincipal != null &&
                    ctrlPrincipal.Control != null)
                {
                    C2iWndFenetre fen = wndParent as C2iWndFenetre;
                    if (fen != null && fen.AutoSize)
                        ctrlPrincipal.Control.Dock = DockStyle.Top;
                    else
                        ctrlPrincipal.Control.Dock = DockStyle.Fill;
                }
                if (ctrlPrincipal != null)
                    m_controlesPrincipaux.Add(ctrlPrincipal);
                controlParent.ResumeDrawing();
                return result;
            }
            
		}

		// ////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Création des contrôles windows
		/// </summary>
		/// <param name="controlParent"></param>
		/// <param name="wndParent"></param>
		/// <param name="tooltip"></param>
		/// <returns></returns>
		public virtual CResultAErreur CreateControlesEnSurimpression(
			Control controlParent,
			C2iWnd wndParent,
			IFournisseurProprietesDynamiques fournisseur)
		{
            CResultAErreur result = CResultAErreur.True;
            m_controlesPrincipaux.Clear();
            CWndFor2iWndFormulaireEnSurimpression ctrlPrincipal = new CWndFor2iWndFormulaireEnSurimpression(controlParent);
            ctrlPrincipal.CreateControle(this, wndParent, controlParent, fournisseur);
            /*if (ctrlPrincipal != null &&
                ctrlPrincipal.Control != null)
                ctrlPrincipal.Control.Dock = DockStyle.Fill;*/

            //Ajoute les contrôles externes
            foreach (C2iWndControleExterne ctrlEx in from wnd in wndParent.Childs
                                                     where wnd is C2iWndControleExterne
                                                     select wnd as C2iWndControleExterne)
            {
                //Cherche le contrôle dans le parent
                Control associe = FindControl(ctrlEx, controlParent);
                if (associe != null)
                {
                    IControleFormulaireExterne ex = CEncapsuleurControleToControleFormulaireExterne.GetControleFormulaireExterne(associe, controlParent);
                    if (ex != null)
                    {
                        CWndFor2iControleExterne newWnd = new CWndFor2iControleExterne(ex);
                        newWnd.CreateControle(this, ctrlEx, controlParent, fournisseur);
                        newWnd.WndContainer = ctrlPrincipal;
                        ctrlPrincipal.AddChild(newWnd);
                    }
                }                
            }

            if (ctrlPrincipal != null)
                m_controlesPrincipaux.Add(ctrlPrincipal);
            return result;
/*
			foreach ( C2iWnd wnd in wndParent.Childs )
			{
				IControleWndFor2iWnd ctrl = CreateControle ( wnd, controlParent, fournisseur );
				if ( ctrl != null )
				{
					m_controlesPrincipaux.Add ( ctrl );
				}
                
			}
			return result;*/
		}

        private Control FindControl(C2iWndControleExterne ctrlExterne, Control ctrlParent)
        {
            if (ctrlParent.Name == ctrlExterne.Name)
                return ctrlParent;
            foreach (Control fils in ctrlParent.Controls)
            {
                Control trouve = FindControl(ctrlExterne, fils);
                if (trouve != null)
                    return trouve;
            }
            return null;
        }

		//------------------------------------------------------------------
		/// <summary>
		/// Met à jour les contrôles à valeurs calculées du formulaire à intervalles de temps déterminé
		/// par le C2iWndFenetre parent
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void m_timerRefresh_Tick(object sender, EventArgs e)
		{
			m_timer.Stop();
			UpdateValeursFormules();
			try
			{
				m_timer.Start();
			}
			catch { }
		}

		//-------------------------------------------------------------
		/// <summary>
		/// Met à jour toutes les valeurs calculées du formulaire
		/// </summary>
		private void UpdateValeursFormules()
		{
			foreach ( IControleWndFor2iWnd ctrl in m_controlesPrincipaux )
				ctrl.UpdateValeursCalculees();
		}


		//-------------------------------------------------------------
		public virtual bool ReadOnly
		{
			get
			{
				return m_bReadOnly;
			}
			set
			{
				m_bReadOnly = value;
			}
		}

		public event EventHandler OnChangeLockEdition;
		//////////////////////////////////////////////////////////////////////////////////////
		public virtual bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				if (ReadOnly)
					m_bLockEdition = true;
				else
					m_bLockEdition = value;
				foreach ( IControleWndFor2iWnd ctrl in m_controlesPrincipaux )
				{
					ctrl.OnChangeParentModeEdition ( !value );
					UpdateVisibilityEtEnable(ctrl, m_elementEdite);
				}
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Met à jour les données de l'object édité avec les valeurs contenues dans le formulaire
		/// </summary>
		/// <returns></returns>
		public virtual CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_bLockEdition)
				return result;
			foreach ( IControleWndFor2iWnd ctrl in m_controlesPrincipaux )
				result += ctrl.MajChamps ( ControleValeursAvantValidation );
			return result;
		}

        //-----------------------------------------------
        public void AppliqueRestrictions(
            CListeRestrictionsUtilisateurSurType restrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (ElementEdite != null)
            {
                CRestrictionUtilisateurSurType restriction = restrictions.GetRestriction(ElementEdite.GetType());
                restriction.ApplyToObjet(ElementEdite);
                foreach (IControleWndFor2iWnd ctrl in m_controlesPrincipaux)
                {
                    ctrl.AppliqueRestriction(
                        restriction,
                        restrictions,
                        gestionnaireReadOnly);
                }
            }

        }

		//-----------------------------------------------
		protected virtual void UpdateVisibilityEtEnable ( object elementEdite )
		{
			foreach ( IControleWndFor2iWnd ctrl in m_controlesPrincipaux )
				UpdateVisibilityEtEnable ( ctrl, elementEdite );
		}

		//-----------------------------------------------
		public virtual void UpdateVisibilityEtEnable( IControleWndFor2iWnd ctrl, object elementEdite )
		{
			if ( m_fournisseurProprietes == null || ctrl == null)
				return;
			C2iWnd wnd = ctrl.WndAssociee as C2iWnd;
			if (wnd != null && ctrl.Control != null)
			{
                try
                {
                    CResultAErreur result = CResultAErreur.True;
                    CContexteEvaluationExpression contexteEvaluation = CUtilControlesWnd.GetContexteEval(
                        ctrl, elementEdite);
                    if (wnd.Visiblity != null)
                    {
                        result = wnd.Visiblity.Eval(contexteEvaluation);
                        bool bVisible = true;
                        if (result && result.Data != null)
                        {

                            if (result.Data.ToString() == "0" || result.Data.ToString().ToUpper() == "FALSE")
                                bVisible = false;
                        }
                        ctrl.Control.Visible = bVisible;

                    }
                    if (wnd.Enabled != null)
                    {
                        result = wnd.Enabled.Eval(contexteEvaluation);

                        bool bEnable = true;
                        if ( wnd.LockMode == C2iWnd.ELockMode.EnableOnEdit )
                            bEnable = !m_bLockEdition;
                        if (wnd.LockMode == C2iWnd.ELockMode.DisableOnEdit)
                            bEnable = m_bLockEdition;
                        if (result && result.Data != null)
                        {
                            if (result.Data.ToString() == "0" || result.Data.ToString().ToUpper() == "FALSE")
                                bEnable = false;
                        }
                        if (ctrl.Control is IControlALockEdition)
                            ((IControlALockEdition)ctrl.Control).LockEdition = !bEnable;
                        else
                            ctrl.Control.Enabled = bEnable;
                    }
                }
                catch { }
			}
			foreach (IControleWndFor2iWnd ctrlFils in ctrl.Childs)
				UpdateVisibilityEtEnable(ctrlFils, elementEdite);

		}



	}
}
