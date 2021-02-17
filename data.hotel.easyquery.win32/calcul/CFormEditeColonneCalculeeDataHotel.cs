using data.hotel.easyquery.calcul;
using futurocom.easyquery;
using sc2i.common;
using sc2i.win32.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data.hotel.easyquery.win32.calcul
{
    public partial class CFormEditeColonneCalculeeDataHotel : Form
    {
        private CColonneCalculeeDataHotel m_colonne = null;
        private CODEQTableFromDataHotel m_table;
        private CEasyQuery m_query;
        private IDataHotelCalcul m_calculEnCours = null;
        private IEditeurCalculHotel m_editeurEnCours = null;

        //------------------------------------------------------
        public CFormEditeColonneCalculeeDataHotel()
        {
            InitializeComponent();
        }

        //------------------------------------------------------
        private void CFormEditeColonneCalculeeDataHotel_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_cmbTypeCalcul.NullAutorise = true;
            m_cmbTypeCalcul.TextNull = I.T("None|20048");
             IEnumerable<KeyValuePair<Type, string>> types = CAllocateurEditeurCalculDataHotel.TypesExistants;
             m_cmbTypeCalcul.ListDonnees = types;
            m_cmbTypeCalcul.ProprieteAffichee = "Value";
            m_cmbTypeCalcul.AssureRemplissage();
            Type tp = m_colonne.Calcul != null ? m_colonne.Calcul.GetType() : null;
            m_cmbTypeCalcul.SelectedItem = null;
            if ( tp != null )
            {
                foreach (KeyValuePair<Type, string> kv in types)
                    if (kv.Key == tp)
                        m_cmbTypeCalcul.SelectedValue = kv;
            }
            m_txtColumnName.Text = m_colonne.ColumnName;
            UpdateVisuel();
        }

        //------------------------------------------------------
        public static bool EditeColonne ( CColonneCalculeeDataHotel colonne,
            CODEQTableFromDataHotel table,
            CEasyQuery query )
        {
            using (CFormEditeColonneCalculeeDataHotel frm = new CFormEditeColonneCalculeeDataHotel())
            {
                frm.m_colonne = colonne;
                frm.m_table = table;
                frm.m_query = query;
                if (colonne.Calcul != null)
                    frm.m_calculEnCours = CCloner2iSerializable.Clone(colonne.Calcul) as IDataHotelCalcul;
                else
                    frm.m_calculEnCours = null;
                if ( frm.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }

        //------------------------------------------------------
        private void m_cmbTypeCalcul_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateVisuel();
        }

        //------------------------------------------------------
        private void UpdateVisuel()
        {
            KeyValuePair<Type, string>? kv = m_cmbTypeCalcul.SelectedValue as KeyValuePair<Type, string>?;
            if ( kv != null )
            {
                if ( m_calculEnCours == null || m_calculEnCours.GetType() != kv.Value.Key )
                {
                    m_calculEnCours = Activator.CreateInstance (kv.Value.Key, new object[0] ) as IDataHotelCalcul;
                }
                Type typeEditeur = CAllocateurEditeurCalculDataHotel.GetTypeEditeur(kv.Value.Key);
                m_panelCalcul.ClearAndDisposeControls();
                m_editeurEnCours = null;
                if ( typeEditeur != null )
                {
                    IEditeurCalculHotel editeur = Activator.CreateInstance(typeEditeur, new object[0]) as IEditeurCalculHotel;
                    if (editeur != null)
                    {
                        Control ctrl = editeur as Control;
                        CWin32Traducteur.Translate(ctrl);
                        ctrl.Parent = m_panelCalcul;
                        ctrl.Dock = DockStyle.Fill;
                        editeur.Init(m_calculEnCours, m_query, m_table);
                    }
                    m_editeurEnCours = editeur;
                }
            }
        }

        //-----------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( m_txtColumnName.Text.Trim().Length == 0)
            {
                result.EmpileErreur(I.T("Column name should not be empty|20049"));
            }
            CResultAErreurType<IDataHotelCalcul> res = null;
            if (m_editeurEnCours != null )
            {
                res = m_editeurEnCours.GetCalcul();
                if (!res)
                    result.EmpileErreur(res.Erreur);
            }
            if ( !result )
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            if ( res != null )
                m_colonne.Calcul = res.DataType;
            m_colonne.ColumnName = m_txtColumnName.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        //-----------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
