using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using System.Collections;
using sc2i.win32.common;
using sc2i.win32.data.navigation;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.win32.navigation;
using sc2i.common.Restrictions;


namespace sc2i.win32.data.navigation.controlesFor2iWnd
{
	[AutoExec("Autoexec")]
    public partial class CWndFor2iListeSpeedStandard : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
        CPanelListeSpeedStandard m_panelListeSpeedStd = null;
        private Type m_typeElements = null;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;

        private List<CObjetDonnee> m_lstChecked = null;
        

		public CWndFor2iListeSpeedStandard(): base()
		{
            m_panelListeSpeedStd = new CPanelListeSpeedStandard();
            LockEdition = false;
		}

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndListeSpeedStandard), typeof(CWndFor2iListeSpeedStandard));
		}

		#region IControleWndFor2iWnd Membres

        protected CResultAErreur TraiterEditionPanel(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CContexteEvaluationExpression ctxEval = CUtilControlesWnd.GetContexteEval(this, objet);
            C2iWndListeSpeedStandard listeStd = this.WndAssociee as C2iWndListeSpeedStandard;
            if (listeStd != null)
            {
                C2iExpression formule = listeStd.AlternativeEditedElement;
                 result = formule.Eval(ctxEval);
                if (!result)
                    return result;
                CObjetDonneeAIdNumeriqueAuto objetToEdit = result.Data as CObjetDonneeAIdNumeriqueAuto;
                if (objetToEdit != null)
                {
                    CReferenceTypeForm refTypeForm = CFormFinder.GetRefFormToEdit(objetToEdit.GetType());
                    IFormNavigable frmToShow = refTypeForm.GetForm(objetToEdit) as IFormNavigable;
                    IFormNavigable frm = m_panelListeSpeedStd.FindForm() as IFormNavigable;
                    if (frm != null)
                        frm.Navigateur.AffichePage(frmToShow);
                    else
                        CFormNavigateurPopup.Show(frmToShow);
                }
                return result;
            }
            return result;
        }

        //---------------------------------------------------------------------
		protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseur)
		{
            C2iWndListeSpeedStandard listeStd = wnd as C2iWndListeSpeedStandard;
            if (listeStd == null)
				return;
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(wnd, m_panelListeSpeedStd);
			m_fournisseurProprietes = fournisseur;
            m_panelListeSpeedStd.BoutonAjouterVisible = listeStd.HasAddButton;
            m_panelListeSpeedStd.BoutonSupprimerVisible = listeStd.HasDeleteButton;
            m_panelListeSpeedStd.BoutonModifierVisible = listeStd.HasDetailButton;
            m_panelListeSpeedStd.BoutonFiltrerVisible = listeStd.HasFilterButton;
            m_panelListeSpeedStd.AllowCustomisation = listeStd.UserCustomizable;
            m_panelListeSpeedStd.OnChangeSelection += new EventHandler(m_panelListeSpeedStd_OnChangeSelection);
            m_panelListeSpeedStd.AffectationsPourNouveauxElements = listeStd.Affectations;
            m_panelListeSpeedStd.ObjetReferencePourAffectationsInitiales = EditedElement;
            if (listeStd.AlternativeEditedElement != null)
                m_panelListeSpeedStd.TraiterModificationElement = TraiterEditionPanel;
            // Initialisation des colonnes
            foreach (C2iWndListeSpeedStandard.CColonneListeSpeedStd col in WndListeStandard.Columns)
            {
                if ( col.InfoChampDynamique != null )
                    m_panelListeSpeedStd.AddColonne(col.Titre, col.InfoChampDynamique.NomPropriete, col.Width);
                //string strCle = "";
                //string strProp = "";
                //if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(col.InfoChampDynamique.NomPropriete, ref strCle, ref strProp))
                //    strProp = col.InfoChampDynamique.Nom;
                //if(col.InfoChampDynamique != null)
                //    m_panelListeSpeedStd.AddColonne(col.Titre, strProp, col.Width);
            }
            // Initialise le filtre
            if (WndListeStandard.Filter != null)
            {
                CDbKey dbKeyFiltreStandard = WndListeStandard.Filter.DbKey;
                CFiltreDynamiqueInDb filtre = new CFiltreDynamiqueInDb(CSc2iWin32DataClient.ContexteCourant);
                if (filtre.ReadIfExists(dbKeyFiltreStandard))
                {
                    m_panelListeSpeedStd.FiltrePrefere = filtre.Filtre;
                }
            }
            // Initialise le nom du controle pour le registre des colonnes
            string strNom = "CustomListeSpeedStandard_";
            if (WndListeStandard.SourceFormula != null)
            {
                strNom += WndListeStandard.SourceFormula.TypeDonnee.TypeDotNetNatif.ToString();
            }
            m_panelListeSpeedStd.Name = strNom;

            m_panelListeSpeedStd.UseCheckBoxes = WndListeStandard.UseCheckBoxes;


            parent.Controls.Add(m_panelListeSpeedStd);
		}

        //-----------------------------------------------------------------------------------
        void m_panelListeSpeedStd_OnChangeSelection(object sender, EventArgs e)
        {
            CUtilControlesWnd.DeclencheEvenement(
                C2iWndListeSpeedStandard.c_strIdEvenementSelectionChanged,
                this);
        }

        //-----------------------------------------------------------------------------------
        public CObjetDonnee SelectedElement
        {
            get
            {
                return m_panelListeSpeedStd.ElementSelectionne;
            }
        }

        //-------------------------------------------------------------------
        public IList CheckedElements
        {
            get
            {
                ArrayList lstFinale = new ArrayList();
                if (m_panelListeSpeedStd != null)
                {
                    CListeObjetsDonnees lst = m_panelListeSpeedStd.GetElementsCheckes();
                    if (lst != null)
                    {
                        foreach (CObjetDonnee obj in lst)
                            lstFinale.Add(obj);
                    }
                }
                return lstFinale;
            }
            set
            {
                if (m_panelListeSpeedStd != null)
                {

                    List<CObjetDonnee> lst = new List<CObjetDonnee>();
                    foreach (object obj in value)
                    {
                        CObjetDonnee objDonnee = obj as CObjetDonnee;
                        if (objDonnee != null)
                            lst.Add(objDonnee);
                    }
                    if (m_panelListeSpeedStd.ListeObjets == null)
                        m_lstChecked = lst;
                    else
                        m_panelListeSpeedStd.SetElementsCheckes(lst);
                }
            }
        }

        //-----------------------------------------------------------------------------------
        public IEnumerable SourceList
        {
            get
            {
                return m_panelListeSpeedStd.ListeObjets;
                
            }
            set
            {
                if ( m_panelListeSpeedStd == null )
                    return;
                CListeObjetsDonnees liste = value as CListeObjetsDonnees;
                if (liste != null)
                {
                    m_panelListeSpeedStd.ListeObjets = liste;
                    m_panelListeSpeedStd.Refresh();
                    return;
                }
                if (value != null && typeof(IEnumerable).IsAssignableFrom(value.GetType()))
                {
                    if (WndListeStandard.SourceFormula != null)
                    {
                        Type tpElements = WndListeStandard.SourceFormula.TypeDonnee.TypeDotNetNatif;

                        if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpElements))
                        {
                            CContexteDonnee ctx = CSc2iWin32DataClient.ContexteCourant;
                            IObjetAContexteDonnee objAContexte = EditedElement as IObjetAContexteDonnee;
                            if (objAContexte != null)
                                ctx = objAContexte.ContexteDonnee;
                            CListeObjetsDonnees listeSource = new CListeObjetsDonnees(ctx, tpElements);
                            //Trouve l'id auto
                            DataTable table = ctx.GetTableSafe(CContexteDonnee.GetNomTableForType(tpElements));
                            if (table != null)
                            {
                                string strPrimKey = table.PrimaryKey[0].ColumnName;
                                StringBuilder bl = new StringBuilder();
                                foreach (CObjetDonnee obj in value)
                                {
                                    if (obj != null && tpElements.IsAssignableFrom(obj.GetType()))
                                    {
                                        bl.Append(((CObjetDonneeAIdNumerique)obj).Id);
                                        bl.Append(",");
                                    }
                                }
                                CFiltreData filtre = new CFiltreDataImpossible();
                                if (bl.Length > 0)
                                {
                                    bl.Remove(bl.Length - 1, 1);
                                    filtre = new CFiltreData(strPrimKey + " in (" +
                                        bl.ToString() + ")");
                                }
                                listeSource.Filtre = filtre;
                                listeSource.InterditLectureInDB = true;
                                m_panelListeSpeedStd.ListeObjets = listeSource;
                                m_panelListeSpeedStd.Refresh();
                            }
                        }
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------------
        private C2iWndListeSpeedStandard WndListeStandard
		{
			get
			{
				return WndAssociee as C2iWndListeSpeedStandard;
			}
		}


       

        //---------------------------------------------------------------------
        public override Control Control
		{
            get { return m_panelListeSpeedStd; }
		}


		//---------------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
            UpdateValeursCalculees();
		}

        //---------------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        //---------------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(EditedElement);
            if ( m_panelListeSpeedStd != null )
                m_panelListeSpeedStd.ObjetReferencePourAffectationsInitiales = EditedElement;
            if (WndListeStandard != null &&  WndListeStandard.SourceFormula != null)
            {
                m_typeElements = WndListeStandard.SourceFormula.TypeDonnee.TypeDotNetNatif;
                CResultAErreur result = WndListeStandard.SourceFormula.Eval(ctx);
                if (result)
                {
                    CListeObjetsDonnees listeSource = result.Data as CListeObjetsDonnees;
                    if (listeSource != null)
                    {
                        m_panelListeSpeedStd.InitFromListeObjets(
                            listeSource,
                            m_typeElements,
							WndListeStandard.FormToUse as CReferenceTypeForm,
                            null,
                            "");
                        try
                        {
                            m_panelListeSpeedStd.RemplirGrilleSansTimer();
                            if (m_lstChecked != null)
                            {
                                m_panelListeSpeedStd.SetElementsCheckes(m_lstChecked);
                                m_lstChecked = null;
                            }
                        }
                        catch { }
                    }
                }

            }

		}

        //---------------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            if (m_panelListeSpeedStd != null)
                m_panelListeSpeedStd.AppliqueRestrictions(listeRestrictions, gestionnaireReadOnly);
        }


		#endregion
        public override bool LockEdition
        {
            get
            {
                return m_panelListeSpeedStd.LockEdition;
            }
            set
            {
                m_panelListeSpeedStd.LockEdition = value;
            }
        }

        		
	}
}
