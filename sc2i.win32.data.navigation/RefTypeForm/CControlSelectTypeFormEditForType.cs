using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using System.Reflection;

namespace sc2i.win32.data.navigation.RefTypeForm
{
    public partial class CControlSelectTypeFormEditForType : UserControl
    {
        private CReferenceTypeForm m_typeSelectionne = null;
        private Type m_typeObjets = null;

        //----------------------------------------------------------------
        public CControlSelectTypeFormEditForType()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------------
        public void Init(Type typeObjetPourForm)
        {
            if (typeObjetPourForm == m_typeObjets)
                return;
            m_typeObjets = typeObjetPourForm;
            List<CReferenceTypeForm> lstRefsTypes = new List<CReferenceTypeForm>();

            // Init liste des formulaires standards
            List<CInfoClasseDynamique> lstForms = new List<CInfoClasseDynamique>();
            // Cherche tous les formulaires d'édition correspondants au type
            foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
            {
                foreach (Type tp in ass.GetTypes())
                {
                    if (!tp.IsAbstract && tp.IsSubclassOf(typeof(System.Windows.Forms.Form)))
                    {
                        Type tpEdite = null;
                        string strLibelle = "";
                        object[] attribs = tp.GetCustomAttributes(typeof(ObjectEditeur), true);
                        if (attribs.Length > 0)
                        {
                            ObjectEditeur attrib = (ObjectEditeur)attribs[0];
                            tpEdite = attrib.TypeEdite;
                            strLibelle = DynamicClassAttribute.GetNomConvivial(tpEdite) + " - " + attrib.Code;
                        }
                        if (tpEdite == typeObjetPourForm && strLibelle != "")
                        {
                            CReferenceTypeFormBuiltIn refBin = new CReferenceTypeFormBuiltIn(tp);
                            lstRefsTypes.Add ( refBin );
                        }
                    }
                }
            }
                  

            // Iinit liste des formulaires custom
            CListeObjetsDonnees lstFormulaires = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, typeof(CFormulaire));
            lstFormulaires.Filtre = new CFiltreData(
                CFormulaire.c_champTypeElementEdite + " = @1 AND " +
                CFormulaire.c_champCodeRole + " is null",
                typeObjetPourForm.ToString());
            foreach ( CFormulaire formulaire in lstFormulaires )
            {
                //Elimine les formulaires en surimpression
                if (formulaire.Libelle.Contains("."))
                {
                    Type tp = CActivatorSurChaine.GetType(formulaire.Libelle);
                    if (tp != null)
                        continue;
                }
                CReferenceTypeFormDynamic refDync = new CReferenceTypeFormDynamic(formulaire);
                lstRefsTypes.Add ( refDync );
            }


            m_comboRefTypeForm.Fill(
                lstRefsTypes,
                "Libelle",
                true);
            m_comboRefTypeForm.AssureRemplissage();
            if (m_typeSelectionne != null)
                m_comboRefTypeForm.SelectedValue = m_typeSelectionne;
        }

        //----------------------------------------------------------------
        public CReferenceTypeForm TypeSelectionne
        {
            get
            {
                return m_comboRefTypeForm.SelectedValue as CReferenceTypeForm;
            }
            set
            {
                m_typeSelectionne = value;
                m_comboRefTypeForm.SelectedValue = value;
                
            }
        }
    }
}
