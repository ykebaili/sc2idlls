using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic.StructureImport;
using sc2i.common;
using System.Collections;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic.StructureImport
{
    public partial class CPanelEditStructureImport : UserControl
    {
        private C2iStructureImport m_structureImport = null;
        private string m_strNomFichier = "";

        //---------------------------------------------------
        public CPanelEditStructureImport()
        {
            InitializeComponent();
        }

        //---------------------------------------------------
        public void Init(C2iStructureImport structure)
        {
            m_structureImport = CCloner2iSerializable.Clone(structure) as C2iStructureImport;

            //Remplit la liste des types
            CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass();
            ArrayList classesAIdAuto = new ArrayList();
            foreach (CInfoClasseDynamique classe in classes)
            {
                if (typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(classe.Classe))
                    classesAIdAuto.Add(classe);
            }

            classesAIdAuto.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("None|19")));

            m_cmbTypeEntite.DataSource = null;
            m_cmbTypeEntite.DataSource = classesAIdAuto;
            m_cmbTypeEntite.DisplayMember = "Nom";
            m_cmbTypeEntite.ValueMember = "Classe";

            m_cmbTypeEntite.SelectedValue = m_structureImport.TypeCible;

            m_chkOptionCreate.Checked = ((m_structureImport.OptionImport & EOptionImport.Create) == EOptionImport.Create);
            m_chkOptionUpdate.Checked = ((m_structureImport.OptionImport & EOptionImport.Update) == EOptionImport.Update);
            m_chkPrechargerLaCible.Checked = m_structureImport.ChargerTouteLaCible;

            FillControleMappage();
        }

        //---------------------------------------------------
        private void FillControleMappage()
        {
            if (m_structureImport.TypeCible == null ||
                m_structureImport.ParametreLecture == null)
            {
                m_lblIlFautDefinirLaSource.Visible = true;
                m_panelMappage.Visible = false;
                return;
            }
            m_lblIlFautDefinirLaSource.Visible = false;
            m_panelMappage.Visible = false;
            m_panelMappage.SuspendDrawing();
            foreach (Control ctrl in new ArrayList(m_panelMappage.Controls))
            {
                ctrl.Visible = false;
                m_panelMappage.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            //Crée la liste des champs
            CFournisseurPropDynStd four = new CFournisseurPropDynStd(false);
            List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>();
            HashSet<CDefinitionProprieteDynamique> setDefsPouvantEtreCle = new HashSet<CDefinitionProprieteDynamique>();
            foreach (CDefinitionProprieteDynamique def in four.GetDefinitionsChamps(m_structureImport.TypeCible))
            {
                if (!def.IsReadOnly && !def.TypeDonnee.IsArrayOfTypeNatif)
                {
                    lstDefs.Add(def);
                    if (CMappageChampImport.GetFiltreCle(m_structureImport.TypeCible, def, 0))
                        setDefsPouvantEtreCle.Add(def);
                }
                    
            }

            foreach (DataColumn col in m_structureImport.ParametreLecture.GetColonnes())
            {
                CControlMappageImport ctrl = new CControlMappageImport();
                C2iOrigineChampImport origine = new C2iOrigineChampImportDataColumn(col);
                CMappageChampImport mappage = m_structureImport.GetMappage(origine);
                ctrl.Init(
                    origine, 
                    mappage != null ? mappage.ProprieteDestination : null, 
                    lstDefs,
                    setDefsPouvantEtreCle);
                m_panelMappage.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Top;
                ctrl.BringToFront();
            }
            m_panelMappage.ResumeDrawing();
            m_panelMappage.Visible = true;
        }

        //---------------------------------------------------
        public C2iStructureImport GetStructureFinale()
        {
            m_structureImport.ClearMappages();
            foreach (Control ctrl in m_panelMappage.Controls)
            {
                CControlMappageImport map = ctrl as CControlMappageImport;
                if (map != null && map.ChampDest != null)
                {
                    CMappageChampImport mappage = new CMappageChampImport();
                    mappage.Origine = map.Origine;
                    mappage.ProprieteDestination = map.ChampDest;
                    mappage.IsCle = map.IsKey;
                    m_structureImport.AddMappage(mappage);
                }
            }
            EOptionImport option = EOptionImport.None;
            if (m_chkOptionUpdate.Checked)
                option |= EOptionImport.Update;
            if (m_chkOptionCreate.Checked)
                option |= EOptionImport.Create;
            m_structureImport.ChargerTouteLaCible = m_chkPrechargerLaCible.Checked;
            m_structureImport.OptionImport = option;
            return m_structureImport;
        }

        //---------------------------------------------------
        public string NomFichier
        {
            get
            {
                return m_strNomFichier;
            }
        }

        //---------------------------------------------------
        private void m_lnkParamètreSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_structureImport == null)
                m_structureImport = new C2iStructureImport();
            m_structureImport.ParametreLecture = CAssistantLectureFichier.CreateParametreLectureFichier(ref m_strNomFichier);
            FillControleMappage();
        }

        //---------------------------------------------------
        private void m_cmbTypeEntite_SelectedValueChanged(object sender, EventArgs e)
        {
            m_structureImport.TypeCible = m_cmbTypeEntite.SelectedValue as Type;
            FillControleMappage();
        }
    }
}
