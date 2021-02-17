using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using System.Collections;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.drawing;
using sc2i.formulaire.win32.controles2iWnd.datagrid;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.datagrid
{
	[AutoExec("Autoexec")]
	public class CWndFor2iDataGrid : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
        
        private CDataGridForFormulaire m_grid = null;
        C2iWndDataGrid m_wndGrid = null;
        private object m_source = null;
        private IFournisseurProprietesDynamiques m_fournisseur = new CFournisseurGeneriqueProprietesDynamiques();

        public CWndFor2iDataGrid()
		{
            m_grid = new CDataGridForFormulaire();
            m_grid.ActiveElementChanged += new EventHandler(m_grid_ActiveElementChanged);
		}

        void m_grid_ActiveElementChanged(object sender, EventArgs e)
        {
            CUtilControlesWnd.DeclencheEvenement(C2iWndDataGrid.c_strSelectionChanged, this);
        }

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndDataGrid), typeof(CWndFor2iDataGrid));
		}

       
		//-------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
            m_wndGrid = wnd as C2iWndDataGrid;
            if (m_wndGrid == null)
				return;
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(m_wndGrid, m_grid);
                
                
			parent.Controls.Add(m_grid);
		}


		//-------------------------------------------
		public override Control Control
		{
			get
			{
                return m_grid;
			}
		}


        //-------------------------------------------
        public object Source
        {
            get
            {
                return m_source;
            }
            set
            {
                m_source = value;
                UpdateFromSource();
            }
        }

		//-------------------------------------------
		protected override void  OnChangeElementEdite(object element)
		{
            if (m_wndGrid != null)
            {
                CContexteEvaluationExpression ctx = CUtilControlesWnd.GetContexteEval(this, element);
                if (m_wndGrid.SourceFormula != null )
                {
                    CResultAErreur result = m_wndGrid.SourceFormula.Eval(ctx);
                    if (result)
                    {
                        Source = result.Data;
                        CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
                    }
                }
            }
		}

        //-------------------------------------------
        private void UpdateFromSource()
        {
            object datas = Source;
            List<object> lstObjets = new List<object>();

            IPreloadableFromArbreProprietesDynamiques preloadable = Source as IPreloadableFromArbreProprietesDynamiques;
            if (preloadable != null && m_wndGrid.PreloadDatas)
            {
                CArbreDefinitionsDynamiques arbre = new CArbreDefinitionsDynamiques(null);
                if (m_wndGrid != null)
                {
                    foreach (C2iWndDataGridColumn col in m_wndGrid.Columns)
                        if (col.Control != null)
                            col.Control.FillArbreProprietesAccedees(arbre);
                }
                preloadable.Preload(arbre);
            }



            if (datas != null)
            {
                IEnumerable collection = datas as IEnumerable;
                if (collection != null)
                {
                    foreach (object obj in collection)
                        lstObjets.Add(obj);
                }
                else
                {
                    lstObjets.Add(datas);
                }
                m_grid.Init(this, m_wndGrid, EditedElement, lstObjets, m_fournisseur);
                
            }
            CUtilControlesWnd.DeclencheEvenement(C2iWnd.c_strIdEvenementOnInit, this);

        }

		//-------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//-------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}
		
		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            if (m_grid != null)
                m_grid.InitRestrictions(listeRestrictions);
		}

        //---------------------------------------------
        public object LastAddedElement
        {
            get
            {
                if (m_grid != null)
                    return m_grid.LastAddedElement;
                return null;
            }
        }

        //---------------------------------------------
        public object ActiveElement
        {
            get{
                if ( m_grid!= null )
                {
                    return m_grid.ActiveElement;
                }
                return null;
            }
        }

	}
}
