using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using sc2i.documents;
using sc2i.win32.common;
using sc2i.data;
using sc2i.win32.data;
using sc2i.win32.process.multipledoctoged;
using sc2i.process;
using sc2i.common;

namespace sc2i.win32.process
{
    public partial class CFormAddMultipleToGed : Form
    {
        private string m_strKeyRepertoire = "";
        public CFormAddMultipleToGed()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }


        public static IEnumerable<CActionCopierMultiLocalDansGed.CInfoFichierToGed> GetInfosToAdd(
            IEnumerable<int> lstIdsCategories)
        {
            using (CFormAddMultipleToGed frm = new CFormAddMultipleToGed())
            {
                List<CCategorieGED> lstCats = new List<CCategorieGED>();
                StringBuilder blKeyMultiEDM = new StringBuilder();
                if (lstIdsCategories != null)
                {
                    foreach (int nId in lstIdsCategories)
                    {
                        CCategorieGED categorie = new CCategorieGED(CSc2iWin32DataClient.ContexteCourant);
                        if (categorie.ReadIfExists(nId))
                        {
                            lstCats.Add(categorie);
                            blKeyMultiEDM.Append(nId);
                            blKeyMultiEDM.Append("_");
                        }
                    }
                }
                frm.m_strKeyRepertoire = "MEDOC_" + blKeyMultiEDM.ToString();
               
                string strRep = C2iRegistre.GetValueInRegistreApplication("Preferences", frm.m_strKeyRepertoire, "");
                if (strRep != "")
                {
                    frm.m_browser.StartUpDirectory = sc2i.win32.common.folderbrowser.SpecialFolders.Other;
                    frm.m_browser.StartUpDirectoryOther = strRep;
                }
                frm.Init(lstCats);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    strRep = frm.m_browser.CurrentFolderKey;
                    if ( strRep != null )
                        C2iRegistre.SetValueInRegistreApplication("Preferences", frm.m_strKeyRepertoire, strRep);
                    return frm.GetInfosToAdd();
                }
            }
            return null;
        }

        //-----------------------------------------------------------------------
        public IEnumerable<CActionCopierMultiLocalDansGed.CInfoFichierToGed> GetInfosToAdd()
        {
            List<CActionCopierMultiLocalDansGed.CInfoFichierToGed> lstFinale = new List<CActionCopierMultiLocalDansGed.CInfoFichierToGed>();
            foreach (Control ctrl in m_panelCategories.Controls)
            {
                CControlGedCategoryForDragFile c = ctrl as CControlGedCategoryForDragFile;
                if (c != null)
                {
                    IEnumerable<string> lstFichiers = c.SelectedFiles;
                    if (lstFichiers.Count() > 0)
                    {
                        foreach (string strFichier in lstFichiers)
                        {
                            CActionCopierMultiLocalDansGed.CInfoFichierToGed i = new CActionCopierMultiLocalDansGed.CInfoFichierToGed(
                                strFichier,
                                c.CategorieGed.Id);
                            lstFinale.Add(i);
                        }
                    }
                }
            }
            return lstFinale.AsReadOnly();
        }


        public void Init(IEnumerable<CCategorieGED> lstCategories)
        {
            m_panelCategories.SuspendDrawing();
            if (lstCategories == null || lstCategories.Count() == 0)
            {
                List<CCategorieGED> lstTmp = new List<CCategorieGED>();
                CListeObjetsDonnees lst = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, typeof(CCategorieGED));
                lstTmp = lst.ToList<CCategorieGED>();
                lstCategories = lstTmp;
            }
            //Prépare l'arborescence
            List<CCategorieGED> lstOrdonnee = new List<CCategorieGED>();
            lstOrdonnee.AddRange(lstCategories);
            lstOrdonnee.Sort((x, y) => x.CodeSystemeComplet.CompareTo(y.CodeSystemeComplet));
            HashSet<CCategorieGED> setFaites = new HashSet<CCategorieGED>();
            foreach (CCategorieGED cat in lstOrdonnee)
            {
                AddCat(cat, setFaites, lstOrdonnee, 0);
            }
        }

        private void AddCat(CCategorieGED cat, HashSet<CCategorieGED> setFaites, List<CCategorieGED> lstToAdd, int nNiveau)
        {
            if (setFaites.Contains(cat))
                return;
            setFaites.Add(cat);
            CControlGedCategoryForDragFile ctrl = new CControlGedCategoryForDragFile();
            m_panelCategories.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Dock = DockStyle.Top;
            ctrl.Init(cat, nNiveau);
            ctrl.OnClickAddButton += new EventHandler(ctrlCat_OnClickAddButton);
            int nIndex = lstToAdd.IndexOf(cat) + 1;
            while (nIndex < lstToAdd.Count && lstToAdd[nIndex].CodeSystemeComplet.StartsWith(cat.CodeSystemeComplet))
            {
                AddCat(cat, setFaites, lstToAdd, nNiveau + 1);
                nIndex++;
            }
        }

        

        //-----------------------------------------------------
        void ctrlCat_OnClickAddButton(object sender, EventArgs e)
        {
            CControlGedCategoryForDragFile ctrl = sender as CControlGedCategoryForDragFile;
            if (ctrl != null)
                ctrl.AddFiles(m_browser.SelectedFiles);
        }

        //-----------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //-----------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       
                    
    }
}
