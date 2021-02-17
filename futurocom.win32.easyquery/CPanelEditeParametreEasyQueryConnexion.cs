using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.win32.common;
using sc2i.common;
using sc2i.expression;

namespace futurocom.win32.easyquery
{
    public partial class CPanelEditeParametreEasyQueryConnexion : UserControl, IControlALockEdition
    {
        private CParametreEasyQueryConnexion m_parametre;
        
        //---------------------------------------------------------
        public CPanelEditeParametreEasyQueryConnexion()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------
        public void Init ( CParametreEasyQueryConnexion parametre, CObjetPourSousProprietes source,  IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            if ( parametre == null )
                m_parametre = new CParametreEasyQueryConnexion();
            else
                m_parametre = CCloner2iSerializable.CloneGeneric<CParametreEasyQueryConnexion>(parametre);
            m_txtNomConnexion.Text = m_parametre.NomConnexion;
            FillComboTypesConnexions();
            m_cmbTypeConnexion.SelectedValue = parametre.IdTypeConnexion;
            m_txtNomConnexion.Text = parametre.NomConnexion;
            m_wndFormulesParametres.Init(parametre.FormulesParametres.ToArray(), source, fournisseurProprietes );
        }

        private class CInfoTypeConnexion
        {
            public string TypeId { get; set; }
            public string Libelle { get; set; }

            public CInfoTypeConnexion(IEasyQueryConnexion cnx)
            {
               TypeId = cnx.ConnexionTypeId;
                Libelle = cnx.ConnexionTypeName;

            }
        }

        //---------------------------------------------------------
        private void FillComboTypesConnexions()
        {
            m_cmbTypeConnexion.DisplayMember = "ConnexionTypeName";
            List<CInfoTypeConnexion> lst = new List<CInfoTypeConnexion>();
            foreach ( IEasyQueryConnexion cnx in CAllocateurEasyQueryConnexions.GetConnexionsPossibles())
                lst.Add ( new CInfoTypeConnexion(cnx));
            m_cmbTypeConnexion.ListDonnees = lst;

        }

        //---------------------------------------------------------
        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;
    }
}
