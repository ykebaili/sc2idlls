using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.client;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.data.dynamic.NommageEntite;

namespace sc2i.win32.data.navigation
{
    public partial class CFormNommageEntite : Form
    {
        private CObjetDonneeAIdNumerique m_objet;
        private CResultAErreur m_result = CResultAErreur.True;
        private int m_nIndex = 0;
        private CContexteDonnee m_contexteLocal;

        public CFormNommageEntite()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        public static CResultAErreur NommerEntite(CObjetDonneeAIdNumerique objet)
        {
            if (objet == null)
            {
                CResultAErreur result = new CResultAErreur();
                result.EmpileErreur(I.T("Null object|10113"));
                return result;
            }

            CFormNommageEntite newForm = new CFormNommageEntite();

            using (CContexteDonnee contexte = objet.ContexteDonnee.GetContexteEdition())
            {
                newForm.m_objet = objet;
                newForm.m_contexteLocal = contexte;
                newForm.ShowDialog();
                newForm.Dispose();

            }
            return newForm.m_result;
        }

        //-------------------------------------------------------------------------
        private void CFormNommageEntite_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            if (m_objet != null)
                m_lblEntite.Text = m_objet.DescriptionElement;
            else
                m_lblEntite.Text = I.T("None|915");

            // Recherche la liste des CNommageEntite existants sur cet élément
            CListeObjetsDonnees lstNommages = new CListeObjetsDonnees(m_contexteLocal, typeof(CNommageEntite));
            //TESTDBKEYOK
            lstNommages.Filtre = new CFiltreData(
                CNommageEntite.c_champTypeEntite + " = @1 and " +
                CNommageEntite.c_champCleEntite + " = @2",
                m_objet.TypeString,
                m_objet.DbKey.StringValue);

            // Vide la liste des Controles
            foreach (Control ctrl in m_panelControlsSaisie.Controls)
            {
                if (ctrl is CControlSaisieNomEntite)
                {
                    ctrl.Visible = false;
                    ctrl.Parent = null;
                    m_panelControlsSaisie.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }

            // Remplie le liste des controles
            m_nIndex = 0;
            foreach (CNommageEntite nom in lstNommages)
            {
                if (nom.NomFort != String.Empty)
                    AjouterControlSaisieNommage(nom, m_nIndex++);
            }

        }

        //-------------------------------------------------------------------------
        private void AjouterControlSaisieNommage(CNommageEntite nom, int nIndex)
        {
            CControlSaisieNomEntite control = new CControlSaisieNomEntite();
            control.Dock = DockStyle.Top;
            control.DeleteNommageEventHandler += new EventHandler(control_DeleteNommageEventHandler);
            control.Init(nom, nIndex);
            m_panelControlsSaisie.Controls.Add(control);
            control.BringToFront();
        }

        //-------------------------------------------------------------------------
        void control_DeleteNommageEventHandler(object sender, EventArgs e)
        {
            CControlSaisieNomEntite controlASupprimer = sender as CControlSaisieNomEntite;
            if (controlASupprimer != null)
            {
                CNommageEntite nomASupprimer = controlASupprimer.NommageEntite;
                if (nomASupprimer != null)
                {
                    nomASupprimer.Delete();
                }
                controlASupprimer.Visible = false;
                Control parent = controlASupprimer.Parent;
                controlASupprimer.Parent = null;
                parent.Controls.Remove(controlASupprimer);
                controlASupprimer.Dispose();
            }
        }

        //-------------------------------------------------------------------------
        private void m_btnOK_Click(object sender, EventArgs e)
        {
            m_result = CResultAErreur.True;
            foreach (Control ctrl in m_panelControlsSaisie.Controls)
            {
                CControlSaisieNomEntite control = ctrl as CControlSaisieNomEntite;
                if (control != null)
                {
                    m_result += control.MajChamps();
                }
            }

            if (!m_result)
                CFormAlerte.Afficher(m_result.Erreur);
            else if (m_contexteLocal != null)
            {
                m_result = m_contexteLocal.CommitEdit();
                if (!m_result)
                    CFormAlerte.Afficher(m_result.Erreur);
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        //-------------------------------------------------------------------------
        private void m_lnkAjouter_LinkClicked(object sender, EventArgs e)
        {
            if(m_objet != null)
            {
                CNommageEntite nom = new CNommageEntite(m_contexteLocal);
                nom.CreateNewInCurrentContexte();
                nom.TypeEntite = m_objet.GetType();
                nom.CleEntite = m_objet.DbKey;

                AjouterControlSaisieNommage(nom, m_nIndex++);

            }
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            if (m_contexteLocal != null)
            {
                m_contexteLocal.CancelEdit();
            }
        }
    }
}
