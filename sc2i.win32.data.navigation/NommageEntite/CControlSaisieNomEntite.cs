using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic.NommageEntite;
using sc2i.common;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
    public partial class CControlSaisieNomEntite : UserControl
    {
        private CNommageEntite m_nommage;

        public CControlSaisieNomEntite()
        {
            InitializeComponent();
        }

        public CNommageEntite NommageEntite
        {
            get
            {
                return m_nommage;
            }
        }

        public void Init(CNommageEntite nom, int index)
        {
            m_nommage = nom;

            m_lblLabel.Text = I.T("Strong Name @1|10114", (index+1).ToString());
            m_txtNomFort.Text = m_nommage.NomFort;
        }

        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_txtNomFort.Text.Trim() == String.Empty)
            {
                result.EmpileErreur(I.T("Empty string not allowed|10115"));
                return result;
            }
            else
            {
                string strNomSaisi = m_txtNomFort.Text.Trim();
                // Recherche s'il y a une autre entité avec ce Nom, et proposede le réaffecter
                CListeObjetsDonnees lstNommages = new CListeObjetsDonnees(m_nommage.ContexteDonnee, typeof(CNommageEntite));
                //TESTDBKEYOK (SC)
                lstNommages.Filtre = new CFiltreData(
                    "("+CNommageEntite.c_champTypeEntite + " <> @1 or " +
                    CNommageEntite.c_champCleEntite + " <> @2) and " + 
                    CNommageEntite.c_champNomFort + " LIKE @3",
                    m_nommage.TypeEntityString,
                    m_nommage.CleEntiteString,
                    strNomSaisi);

                if (lstNommages.Count > 0)
                {
                    CNommageEntite nomExistant = lstNommages[0] as CNommageEntite;
                    if (nomExistant != null)
                    {
                        CObjetDonneeAIdNumerique objetDejaNomme = nomExistant.GetObjetNomme();
                        if (objetDejaNomme != null)
                        {
                            if (CFormAlerte.Afficher(I.T("The Strong Name @1 will be reassigned from @2 to @3. Continue ?|10117",
                                strNomSaisi, objetDejaNomme.DescriptionElement, m_nommage.GetObjetNomme().DescriptionElement),
                                EFormAlerteType.Question) == DialogResult.Yes)
                            {
                                nomExistant.Delete();
                                m_nommage.NomFort = strNomSaisi;
                            }
                            else
                            {
                                result.EmpileErreur(I.T("The Strong Name @1 already exist on @2|10118", strNomSaisi, objetDejaNomme.DescriptionElement));
                                return result;
                            }

                        }

                    }
                }

                m_nommage.NomFort = strNomSaisi;
            }
            return result;
        }

        //---------------------------------------------------------------------
        public event EventHandler DeleteNommageEventHandler;
        private void m_lnkDelete_LinkClicked(object sender, EventArgs e)
        {
            if (DeleteNommageEventHandler != null)
                DeleteNommageEventHandler(this, new EventArgs());
        }
    }
}
