using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.data.dynamic.StructureImport;
using sc2i.data;

namespace sc2i.win32.data.dynamic.StructureImport
{
    public partial class CControlMappageImport : UserControl
    {
        private C2iOrigineChampImport m_origine;
        private HashSet<CDefinitionProprieteDynamique> m_setProprietesPouvantEtreCle = new HashSet<CDefinitionProprieteDynamique>();
        
        //----------------------------------------------------
        public CControlMappageImport()
        {
            InitializeComponent();
        }

        //----------------------------------------------------
        public void Init(
            C2iOrigineChampImport origine,
            CDefinitionProprieteDynamique champDest,
            List<CDefinitionProprieteDynamique> lstChampsDest,
            HashSet<CDefinitionProprieteDynamique> setDefinitionsPouvantEtreCle)
        {
            m_origine = origine;
            m_lblChampSource.Text = origine.NomChamp;
            m_setProprietesPouvantEtreCle = setDefinitionsPouvantEtreCle;

            IEnumerable<CDefinitionProprieteDynamique> lstDefs = 
                from field in lstChampsDest where
                //Inclu les champs assignable
                field.TypeDonnee.TypeDotNetNatif.IsAssignableFrom(origine.TypeDonnee) || 
                //et si l'origine est un entier, les objet à id numérique auto parents
                origine.TypeDonnee == typeof(int) && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(field.TypeDonnee.TypeDotNetNatif)
                select field;

            m_cmbChampDest.ListDonnees = lstDefs;
            
            m_cmbChampDest.SelectedValue = champDest;
        }

        //----------------------------------------------------
        public C2iOrigineChampImport Origine
        {
            get
            {
                return m_origine;
            }
        }


        //----------------------------------------------------
        public CDefinitionProprieteDynamique ChampDest
        {
            get
            {
                return m_cmbChampDest.SelectedValue as CDefinitionProprieteDynamique;
            }
        }

        //----------------------------------------------------
        public bool IsKey
        {
            get
            {
                CDefinitionProprieteDynamique def = ChampDest;
                return def != null && m_setProprietesPouvantEtreCle.Contains(def) && m_chkKey.Checked;
            }
        }

        //----------------------------------------------------
        private void m_cmbChampDest_SelectedValueChanged(object sender, EventArgs e)
        {
            CDefinitionProprieteDynamique def = m_cmbChampDest.SelectedValue as CDefinitionProprieteDynamique;
            if (def != null && m_setProprietesPouvantEtreCle.Contains(def))
                m_panelKey.Visible = true;
            else
                m_panelKey.Visible = false;

        }
    }

}
