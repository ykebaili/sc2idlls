using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data.dynamic.StructureImport;
using sc2i.common;
using sc2i.data;

namespace sc2i.win32.data.dynamic.StructureImport
{
    public partial class CAssistantImporterDonnees : Form
    {
        C2iStructureImport m_structure = new C2iStructureImport();
        public CAssistantImporterDonnees()
        {
            InitializeComponent();
        }

        private void CAssistantImporterDeDonnees_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_panelStructure.Init(m_structure);
        }

        public static void ImporterDonnees()
        {
            CAssistantImporterDonnees form = new CAssistantImporterDonnees();
            form.ShowDialog();
            form.Dispose();
        }

        private void m_lnkDemarrer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CResultAErreur result = CResultAErreur.True;
            
            try
            {
                C2iStructureImport structure = m_panelStructure.GetStructureFinale();
                if (structure.Mappages.Select(m => m.IsCle).Count() == 0)
                {
                    if (MessageBox.Show(I.T("Warning, You didn't select any identification field. Continue ?|20052"),
                        "",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                string strFichier = m_panelStructure.NomFichier;

                using (CContexteDonnee ctx = new CContexteDonnee(CSc2iWin32DataClient.ContexteCourant.IdSession, true, false))
                {
                    ctx.SetVersionDeTravail(CSc2iWin32DataClient.ContexteCourant.IdVersionDeTravail, false);
                    using (CWaitCursor waiter = new CWaitCursor())
                    {
                        result = structure.Importer(strFichier, ctx);
                    }
                    if (result)
                    {
                        DataTable table = ctx.GetTableSafe(CContexteDonnee.GetNomTableForType(structure.TypeCible));
                        int nNbUpdate = structure.NbUpdated;
                        int nNbCreate = structure.NbCreated;
                        if (CFormAlerte.Afficher(I.T("This opération will create @1 and update @2 elements. Do you confirm this operation ?|20050",
                            nNbCreate.ToString(),
                            nNbUpdate.ToString()), EFormAlerteType.Question ) == DialogResult.Yes)
                        {
                            result = ctx.SaveAll(true);
                            if (result)
                                CFormAlerte.Afficher(I.T("Import ended with success|20051"), EFormAlerteType.Info);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.EmpileErreur(new CErreurException(ex));
            }
            if (!result)
                CFormAfficheErreur.Show(result.Erreur);
        }

        private void m_panelStructure_Load(object sender, EventArgs e)
        {

        }


    }
}
