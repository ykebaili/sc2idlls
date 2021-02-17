using System;
using System.Collections.Generic;
using System.Text;
using sc2i.formulaire.win32;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.data;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.formulaire.win32.controles2iWnd;
using System.Collections;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;

namespace sc2i.win32.data.dynamic.controlesFor2iWnd
{
    
    [AutoExec("Autoexec")]
    class CWndFor2iTextBoxFiltreRapide : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
    {
        private ISelectionneurElementListeObjetsDonnees m_selectionneur = null;
        
        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTextBoxFiltreRapide), typeof(CWndFor2iTextBoxFiltreRapide));
        }

        public CWndFor2iTextBoxFiltreRapide()
        {
			
        }

		protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseur)
        {
            if(WndFiltreRapide != null)
            {
                if (WndFiltreRapide.UseCombo)
                    m_selectionneur = new CComboBoxListeObjetsDonnees();
                else
                    m_selectionneur = new C2iTextBoxFiltreRapide();
                m_selectionneur.ElementSelectionneChanged += new EventHandler(CWndFor2iTextBoxFiltreRapide_ElementSelectionneChanged);

                 CCreateur2iFormulaireV2.AffecteProprietesCommunes(WndFiltreRapide, (Control)m_selectionneur);
				 parent.Controls.Add((Control)m_selectionneur);
                 
            }
            
        }

		//----------------------------------------------
		public C2iWndTextBoxFiltreRapide WndFiltreRapide
		{
			get
			{
				return WndAssociee as C2iWndTextBoxFiltreRapide;
			}
		}

		protected override void OnChangeElementEdite(object element)
        {
            if (element != null && 
				WndFiltreRapide != null )
            {
                CFiltreData filtreData;
                if (WndFiltreRapide.Filter != null)
                {
                    CElementAVariablesDynamiques eltAVar = new CElementAVariablesDynamiques();
                    CVariableDynamiqueStatique var = new CVariableDynamiqueStatique(eltAVar);
                    var.Nom = "EditedElement";
                    var.IdVariable = "0";
                    var.SetTypeDonnee(new CTypeResultatExpression(element.GetType(), false));
                    eltAVar.AddVariable(var);
                    eltAVar.SetValeurChamp(var, element);
                    CFiltreDynamique filtreDyn = WndFiltreRapide.Filter;
                    filtreDyn.ElementAVariablesExterne = eltAVar;
                    filtreData = (CFiltreData)filtreDyn.GetFiltreData().Data;
                }
                else
                    filtreData = null;
                Type typePropriete = null;
                if (WndFiltreRapide.Property != null)
                    typePropriete = WndFiltreRapide.Property.TypeDonnee.TypeDotNetNatif;
                else if (WndFiltreRapide.Filter != null)
                    typePropriete = WndFiltreRapide.Filter.TypeElements;
                if (typePropriete != null)
                {
                    string strDesc = DescriptionFieldAttribute.GetDescriptionField(typePropriete, "DescriptionElement");
                    C2iTextBoxFiltreRapide txtRapide = m_selectionneur as C2iTextBoxFiltreRapide;
                    if (txtRapide != null)
                        txtRapide.InitAvecFiltreDeBase(typePropriete, strDesc, filtreData, true);
                    else
                    {
                        CComboBoxListeObjetsDonnees cmb = m_selectionneur as CComboBoxListeObjetsDonnees;
                        cmb.NullAutorise = true;
                        cmb.TextNull = I.T("None|19");
                        if (cmb != null)
                            cmb.Init(
                                typePropriete,
                                filtreData,
                                strDesc,
                                true);
                    }
                    if (WndFiltreRapide.Property != null)
                    {
                        CObjetDonnee valeur = CInterpreteurProprieteDynamique.GetValue(EditedElement, WndFiltreRapide.Property).Data as CObjetDonnee;
                        m_selectionneur.ElementSelectionne = valeur;
                    }
                }
            }
            
        }

		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            if (EditedElement != null)
            {
                if (WndFiltreRapide != null && WndFiltreRapide.Property != null && !LockEdition)
                {
					return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndFiltreRapide.Property, m_selectionneur.ElementSelectionne);
                }
            }
            return CResultAErreur.True;

        }


		protected override void MyUpdateValeursCalculees()
		{
		}

     

        //---------------------------------------------------------------
        public override Control Control
        {
            get
            {
                return m_selectionneur as Control;
            }
        }


		//---------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (EditedElement != null &&
                m_selectionneur != null)
            {
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                C2iWndTextBoxFiltreRapide wndTxt = WndAssociee as C2iWndTextBoxFiltreRapide;
                if (wndTxt != null)
                {
                    CDefinitionProprieteDynamique def = wndTxt.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }
                if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                    gestionnaireReadOnly.SetReadOnly(m_selectionneur, true);

            }
        }
   
        private void CWndFor2iTextBoxFiltreRapide_ElementSelectionneChanged(object sender, EventArgs e)
        {
            C2iWndTextBoxFiltreRapide txt = WndAssociee as C2iWndTextBoxFiltreRapide;
            if (txt != null && txt.AutoSetValue)
                MajChamps(false);
            if ( m_grid != null )
                m_grid.NotifyCurrentCellDirty(true);
            CUtilControlesWnd.DeclencheEvenement(C2iWndTextBoxFiltreRapide.c_strIdEvenementValueChanged, this);
        }

        public object Source
        {
            set
            {
                CListeObjetsDonnees listeObjets = value as CListeObjetsDonnees;
                if (listeObjets == null)
                {
                    IEnumerable en = value as IEnumerable;
                    List<int> lstIds = new List<int>();
                    Type typeObjets = null;
                    CContexteDonnee ctx = null;
                    foreach (object obj in en)
                    {
                        CObjetDonneeAIdNumerique objId = obj as CObjetDonneeAIdNumerique;
                        if (objId != null)
                        {
                            if (typeObjets == null)
                            {
                                typeObjets = objId.GetType();
                                ctx = objId.ContexteDonnee;
                            }
                        }
                        if (typeObjets.IsAssignableFrom(objId.GetType()))
                            lstIds.Add(objId.Id);
                    }
                    listeObjets = CListeObjetsDonnees.GetListeFromIds(ctx, typeObjets, lstIds.ToArray());
                    //listeObjets.InterditLectureInDB = true;
                    //listeObjets.AssureLectureFaite();
                    //listeObjets.InterditLectureInDB = false;
                }
                if (listeObjets != null)
                {
                    C2iTextBoxFiltreRapide txtFiltre = m_selectionneur as C2iTextBoxFiltreRapide;
                    string strDesc = DescriptionFieldAttribute.GetDescriptionField(listeObjets.TypeObjets, "DescriptionElement");
                    if (txtFiltre != null)
                        txtFiltre.InitAvecFiltreDeBase(listeObjets.TypeObjets, strDesc,
                            CFiltreData.GetAndFiltre(listeObjets.FiltrePrincipal, listeObjets.Filtre),
                            true);
                    else
                    {
                        CComboBoxListeObjetsDonnees cmb = m_selectionneur as CComboBoxListeObjetsDonnees;
                        cmb.Init(listeObjets, strDesc, true);
                    }
                }
                
                

            }
        }

        private DataGridView m_grid = null;
        public DataGridView DataGrid
        {
            get
            {
                return m_grid;
            }
            set
            {
                m_grid = value;
            }
        }

        public void SelectAll()
        {
            if (m_selectionneur != null)
                m_selectionneur.SelectAll();
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (m_selectionneur != null)
                return m_selectionneur.WantsInputKey(keyData, dataGridViewWantsInputKey);
            return false;
        }




        
    }
}
