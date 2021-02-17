using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common.customizableList;
using sc2i.formulaire.inspiration;

namespace sc2i.formulaire.win32.inspiration
{
    public partial class CPanelEditParametreInspiration : CCustomizableListControl
    {
        private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;

        //------------------------------------------------------
        public CPanelEditParametreInspiration()
        {
            InitializeComponent();
            m_fournisseurProprietes = new CFournisseurGeneriqueProprietesDynamiques();
            m_cmbType.Init();
        }


        //------------------------------------------------------
        public void Init(IFournisseurProprietesDynamiques fournisseurDeProprietes)
        {
        }

        //------------------------------------------------------
        public CParametreInspirationProprieteDeType ParametreEdite
        {
            get
            {
                return CurrentItem != null?CurrentItem.Tag as CParametreInspirationProprieteDeType:null;
            }
        }


        //--------------------------------------------------------------------------------
        private bool m_bIsInitializing = false;
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            m_bIsInitializing = true;
            CResultAErreur result = base.MyInitChamps(item);
            CParametreInspirationProprieteDeType parametre = ParametreEdite;
            if (result && parametre != null)
            {
                m_cmbType.TypeSelectionne = parametre.Type;
                InitListeChamps();
                m_cmbChamp.SelectedValue = parametre.Champ;
            }
            m_bIsInitializing = false;
            return result;
        }

        //-----------------------------------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result = base.MyMajChamps();
            if (result)
            {
                CParametreInspirationProprieteDeType parametre = ParametreEdite;
                if (parametre != null)
                {
                    parametre.Type = m_cmbType.TypeSelectionne;
                    parametre.Champ = m_cmbChamp.SelectedValue as CDefinitionProprieteDynamique;

                    result = parametre.VerifieDonnees();
                    if (!result)
                        return result;
                }
            }
            return result;
        }

        //--------------------------------------------------------------------------------
        private void InitListeChamps()
        {
            Type tp = m_cmbType.TypeSelectionne;
            if ( tp == null )
                m_cmbChamp.Visible = false;
            if ( m_fournisseurProprietes != null  && tp != null)
            {
                m_cmbChamp.Visible = true;
                List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>();
                foreach ( CDefinitionProprieteDynamique def in m_fournisseurProprietes.GetDefinitionsChamps(m_cmbType.TypeSelectionne) )
                {
                    if ( def.TypeDonnee != null && def.TypeDonnee.TypeDotNetNatif == typeof(string) && 
                        !def.TypeDonnee.IsArrayOfTypeNatif )
                    {
                        lstDefs.Add ( def );
                    }
                }

                lstDefs.Sort((x,y)=>x.Nom.CompareTo(y.Nom));
                m_cmbChamp.ListDonnees = lstDefs;
                m_cmbChamp.ProprieteAffichee = "Nom";
            }
        }

        //-----------------------------------------------------------------------------
        private void m_cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!m_bIsInitializing)
            {
                InitListeChamps();
                HasChange = true;
            }
        }





    }
}
