using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.common.memorydb;
using System.Collections;

namespace sc2i.win32.common.MemoryDb
{
    public delegate string FonctionRetournantTexteNull();

    public enum EModeAffichageImageTextBoxRapide
    {
        Never = 0,
        Always = 1,
        OnSelection
    }

    public partial class C2iTextBoxSelectionEntiteMemoryDb : UserControl, IControlALockEdition
    {
        private bool m_bLockEdition = false;
        private bool m_bUseIntellisense = true;
        private string m_strOldText = "";
        private List<CConfigTextBoxSelectMemoryDb> m_listeConfigs = new List<CConfigTextBoxSelectMemoryDb>();
        private EModeAffichageImageTextBoxRapide m_modeIcone = EModeAffichageImageTextBoxRapide.Always;
        private Image m_imageSpecifique = null;
        private FonctionRetournantTexteNull m_fctTextNull = null;
        private string m_strTextNull = "";
        private CEntiteDeMemoryDb m_selectedEntity = null;
        private CConfigTextBoxSelectMemoryDb m_configDefaut = null;
        private CMemoryDb m_currentMemoryDb = null;

        public C2iTextBoxSelectionEntiteMemoryDb()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------
        public EModeAffichageImageTextBoxRapide ImageDisplayMode
        {
            get
            {
                return m_modeIcone;
            }
            set
            {
                m_modeIcone = value;
            }
        }

        //-------------------------------------------------------------------------------
        public Image SpecificImage
        {
            get
            {
                return m_imageSpecifique;
            }
            set
            {
                m_imageSpecifique = value;
            }
        }

        //------------------------------------------------------------------
        public FonctionRetournantTexteNull FonctionTextNull
        {
            get
            {
                return m_fctTextNull;
            }
            set
            {
                m_fctTextNull = value;
            }
        }

        //------------------------------------------------------------------
        public string TextNull
        {
            get
            {
                if (m_fctTextNull == null)
                    return m_strTextNull;
                return m_fctTextNull();
            }
            set
            {
                m_strTextNull = value;
            }
        }

        //------------------------------------------------------------------
        public bool UseIntellisense
        {
            get
            {
                return m_bUseIntellisense;
            }
            set
            {
                m_bUseIntellisense = value;
            }
        }

        //------------------------------------------------------------------
        public override string Text
        {
            get
            {
                return m_textBox.Text;
            }
            set
            {
                m_textBox.Text = value;
            }
        }

        //------------------------------------------------------------------
        public void Init(CMemoryDb dbMemory, Type typeObjets, string strProprieteAffichee, bool bForceInit)
        {
            InitAvecFiltreDeBase(dbMemory, typeObjets, strProprieteAffichee, null, bForceInit);
        }

        //------------------------------------------------------------------
        public void InitAvecFiltreDeBase(CMemoryDb dbMemory, Type typeObjets, string strProprieteAffichee, CFiltreMemoryDb filtreDeBase, bool bForceInit)
        {
            InitAvecFiltreDeBase(dbMemory, typeObjets, strProprieteAffichee, filtreDeBase, null, bForceInit);
        }

        //------------------------------------------------------------------
        public void InitAvecFiltreDeBase(CMemoryDb dbMemory, Type typeObjets, string strProprieteAffichee, CFiltreMemoryDb filtreDeBase, CFiltreMemoryDb filtreRapide, bool bForceInit)
        {
            if (!bForceInit && m_bIsInit)
                return;
            m_listeIntellisense = null;
            m_currentMemoryDb = dbMemory;
            m_listeConfigs.Clear();
            m_listeConfigs.Add(new CConfigTextBoxSelectMemoryDb(
                typeObjets,
                filtreDeBase,
                filtreRapide,
                strProprieteAffichee));
            m_bIsInit = true;
            UpdateImage();
        }

        //------------------------------------------------------------------
        public void InitMultiple(CConfigTextBoxSelectMemoryDb[] configs, bool bForceInit)
        {
            if (!bForceInit && m_bIsInit)
                return;
            m_listeConfigs.Clear();
            m_listeConfigs.AddRange(configs);
            m_bIsInit = true;
            UpdateImage();
        }

        //------------------------------------------------------------------
        private void SynchroniseTextEtObjet()
        {
            if (m_selectedEntity == null)
                Text = TextNull;
            else
            {
                foreach (CConfigTextBoxSelectMemoryDb config in m_listeConfigs)
                {
                    if (config.TypeObjets != null && config.TypeObjets.IsAssignableFrom(m_selectedEntity.GetType()))
                    {
                        Text = CInterpreteurTextePropriete.GetStringValue(m_selectedEntity, config.ProprieteAffichee, "");
                        break;
                    }
                }
            }
            m_strOldText = Text;
            UpdateImage();
        }

        //-------------------------------------------------------------------------
        private void UpdateImage()
        {
            Image img = null;
            if (m_imageSpecifique != null)
                m_picType.Image = m_imageSpecifique;
            else
            {
                if (m_listeConfigs.Count == 1)
                {
                    img = DynamicClassAttribute.GetImage(m_listeConfigs[0].TypeObjets);
                }
                else
                {
                    if (SelectedObject != null)
                        img = DynamicClassAttribute.GetImage(SelectedObject.GetType());
                    else
                        img = null;
                }
            }
            m_picType.Image = img;
            switch (m_modeIcone)
            {
                case EModeAffichageImageTextBoxRapide.Never:
                    m_picType.Visible = false;
                    break;
                case EModeAffichageImageTextBoxRapide.Always:
                    m_picType.Visible = img != null;
                    break;
                case EModeAffichageImageTextBoxRapide.OnSelection:
                    m_picType.Visible = img != null && SelectedObject != null;
                    break;
                default:
                    break;
            }
        }

        //------------------------------------------------------------------
        public string LastValidatedText
        {
            get
            {
                return m_strOldText;
            }
        }

        //------------------------------------------------------------------
        public event EventHandler OnSelectedObjectChanged;
        public CEntiteDeMemoryDb SelectedObject
        {
            get
            {
                //Nécéssaire pour les grilles: SI le contrôle est dans une grille,
                //on peut interroger sa valeur SelectedObject avec que le lostFocus
                //ne soit détecté par le contrôle
                if (m_textBox.Focused && m_strOldText != Text)
                    SelectObject();
                return m_selectedEntity;
            }
            set
            {
                m_selectedEntity = value;
                SynchroniseTextEtObjet();
                if (OnSelectedObjectChanged != null)
                    OnSelectedObjectChanged(this, new EventArgs());
            }
        }

        //------------------------------------------------------------------
        public void SelectObject()
        {
            string strText = m_textBox.Text;
            HashSet<CReferenceEntiteMemoryDb> lstRefs = null;
            if (m_dicIntellisense != null && m_dicIntellisense.TryGetValue(strText.ToUpper(), out lstRefs))
            {
                if (lstRefs.Count == 1)
                {
                    if(m_currentMemoryDb != null)
                        SelectedObject = lstRefs.ElementAt(0).GetEntity(m_currentMemoryDb);
                    return;
                }
            }
            if (m_configDefaut == null && m_listeConfigs.Count == 1)
                m_configDefaut = m_listeConfigs[0];
            if (m_configDefaut == null)
            {
                foreach (IDisposable dis in new ArrayList(m_menuChooseType.Items))
                {
                    dis.Dispose();
                }
                m_menuChooseType.Items.Clear();
                foreach (CConfigTextBoxSelectMemoryDb config in m_listeConfigs)
                {
                    ToolStripMenuItem itemSelectType = new ToolStripMenuItem(config.LibelleConfig);
                    itemSelectType.Tag = config;
                    Image img = DynamicClassAttribute.GetImage(config.TypeObjets);
                    if (img != null)
                        itemSelectType.Image = img;
                    m_menuChooseType.Items.Add(itemSelectType);
                    itemSelectType.Click += new EventHandler(itemSelectType_Click);
                }
                if (m_menuChooseType.Items.Count > 0)
                    m_menuChooseType.Show(m_btn, new Point(0, m_btn.Height));
            }
            else
                SelectObject(m_configDefaut);
        }

        //-------------------------------------------------
        void itemSelectType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CConfigTextBoxSelectMemoryDb config = item != null ? item.Tag as CConfigTextBoxSelectMemoryDb : null;
            if (config != null)
            {
                SelectObject(config);
            }
        }

        //-------------------------------------------------
        private void SelectObject(CConfigTextBoxSelectMemoryDb config)
        {
            if (config != null)
                m_configDefaut = config;
            CEntiteDeMemoryDb obj = null;
            bool bSelectPriseEnChargeParDelegue = false;

            if (!bSelectPriseEnChargeParDelegue)
            {
                obj = CFormSelectUneEntiteMemoryDb.SelectionRechercheRapide(
                    m_currentMemoryDb,
                    "",
                    config.TypeObjets,
                    config.FiltreDeBase,
                    config.FiltreRapide,
                    m_textBox.Text,
                    config.ProprieteAffichee,
                    "");
                
            }
            if (obj != null)
                SelectedObject = obj;
            else
                SynchroniseTextEtObjet();
        }

        //------------------------------------------------------------------
        private bool m_bIsInit = false;

        //------------------------------------------------------------------
        private Dictionary<string, HashSet<CReferenceEntiteMemoryDb>> m_dicIntellisense = null;
        private List<string> m_listeIntellisense = null;
        private void InitIntellisense()
        {
            if (m_dicIntellisense == null)
            {
                m_dicIntellisense = new Dictionary<string, HashSet<CReferenceEntiteMemoryDb>>();
                HashSet<string> setMots = new HashSet<string>();
                try
                {
                    if (m_listeConfigs != null && m_bUseIntellisense)
                    {
                        foreach (CConfigTextBoxSelectMemoryDb config in m_listeConfigs)
                        {
                            try
                            {
                                CListeEntitesDeMemoryDbBase lstObjs = new CListeEntitesDeMemoryDbBase(m_currentMemoryDb, config.TypeObjets);
                                CFiltreMemoryDb filtre = config.FiltreDeBase;
                                lstObjs.Filtre = filtre;

                                if (lstObjs.Count() < 4000)
                                {
                                    foreach (CEntiteDeMemoryDb obj in lstObjs)
                                    {
                                        string strText = CInterpreteurTextePropriete.GetStringValue(obj, config.ProprieteAffichee, "");
                                        if (strText.Trim().Length > 0)
                                        {
                                            strText = strText.Trim();
                                            HashSet<string> lstStrings = new HashSet<string>();
                                            lstStrings.Add(strText);
                                            int nIndex = strText.IndexOf(' ');
                                            while (nIndex >= 0)
                                            {
                                                strText = strText.Substring(nIndex + 1).Trim();
                                                lstStrings.Add(strText);
                                                nIndex = strText.IndexOf(' ');
                                            }

                                            foreach (string strMotTmp in strText.Split(' '))
                                            {
                                                if (strMotTmp.Length > 2)
                                                {
                                                    if (strMotTmp.Trim().Length > 0)
                                                        lstStrings.Add(strMotTmp);
                                                }
                                            }
                                            CReferenceEntiteMemoryDb refObj = new CReferenceEntiteMemoryDb(obj);
                                            foreach (string strMot in lstStrings)
                                            {
                                                HashSet<CReferenceEntiteMemoryDb> lst = null;
                                                foreach (string strMotToutSeul in strMot.Split(' '))
                                                {
                                                    if (!m_dicIntellisense.TryGetValue(strMotToutSeul.ToUpper(), out lst))
                                                    {
                                                        lst = new HashSet<CReferenceEntiteMemoryDb>();
                                                        m_dicIntellisense[strMotToutSeul.ToUpper()] = lst;
                                                    }
                                                    lst.Add(refObj);
                                                }
                                                if (!m_dicIntellisense.TryGetValue(strMot.ToUpper(), out lst))
                                                {
                                                    lst = new HashSet<CReferenceEntiteMemoryDb>();
                                                    m_dicIntellisense[strMot.ToUpper()] = lst;
                                                }
                                                lst.Add(refObj);
                                                setMots.Add(strMot);
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    m_textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    m_textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    m_textBox.AutoCompleteCustomSource.Clear();
                    m_textBox.AutoCompleteCustomSource.AddRange(setMots.ToArray());
                }
                catch { }
            }
        }

        //------------------------------------------------------------------
        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                m_textBox.LockEdition = value;
                m_btn.Enabled = !value;
                m_btnFlush.Enabled = !value;
                m_btn.Visible = !value;
                m_btnFlush.Visible = !value;

                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //------------------------------------------------------------------
        private void m_btn_Click(object sender, EventArgs e)
        {
            m_configDefaut = null;
            SelectObject();
        }

        //------------------------------------------------------------------
        private void m_btnFlush_Click(object sender, EventArgs e)
        {
            SelectedObject = null;
        }

        //------------------------------------------------------------------
        private void m_textBox_Enter(object sender, System.EventArgs e)
        {
            m_strOldText = Text;
            if (m_listeIntellisense == null && m_bUseIntellisense && IsHandleCreated)
                BeginInvoke(new MethodInvoker(InitIntellisense));
        }

        //------------------------------------------------------------------
        private void m_textBox_Leave(object sender, System.EventArgs e)
        {
            if (Text != m_strOldText)
                SelectObject();
        }

        //-------------------------------------------------
        private void m_textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectObject();
            }
        }
    }


    public class CConfigTextBoxSelectMemoryDb
    {
        private string m_strLibelleConfig = "";
        private Type m_typeObjets;
        private CFiltreMemoryDb m_filtreDeBase = null;
        private CFiltreMemoryDb m_filtreRapide = null;
        private string m_strPropriete = "";

        //-------------------------------------------------
        public CConfigTextBoxSelectMemoryDb()
        {
        }

        //-------------------------------------------------
        public CConfigTextBoxSelectMemoryDb(Type typeObjets,
            CFiltreMemoryDb filtreDeBase,
            CFiltreMemoryDb filtreRapide,
            string strPropriete)
            : this(null, typeObjets, filtreDeBase, filtreRapide, strPropriete)
        { }


        //-------------------------------------------------
        public CConfigTextBoxSelectMemoryDb(
            string strLibelleConfig,
            Type typeObjets,
            CFiltreMemoryDb filtreDeBase,
            CFiltreMemoryDb filtreRapide,
            string strPropriete)
        {
            if (strLibelleConfig == null || strLibelleConfig.Trim().Length == 0)
                strLibelleConfig = DynamicClassAttribute.GetNomConvivial(typeObjets);
            m_strLibelleConfig = strLibelleConfig;
            m_typeObjets = typeObjets;
            m_filtreDeBase = filtreDeBase;
            m_filtreRapide = filtreRapide;
            m_strPropriete = strPropriete;
        }

        //-------------------------------------------------
        public Type TypeObjets
        {
            get
            {
                return m_typeObjets;
            }
            set
            {
                m_typeObjets = value;
            }
        }

        //-------------------------------------------------
        public CFiltreMemoryDb FiltreDeBase
        {
            get
            {
                return m_filtreDeBase;
            }
            set
            {
                m_filtreDeBase = value;
            }
        }

        //-------------------------------------------------
        public CFiltreMemoryDb FiltreRapide
        {
            get
            {
                return m_filtreRapide;
            }
            set
            {
                m_filtreRapide = value;
            }
        }

        //-------------------------------------------------
        public string ProprieteAffichee
        {
            get
            {
                return m_strPropriete;
            }
            set
            {
                m_strPropriete = value;
            }
        }

        //-------------------------------------------------
        public string LibelleConfig
        {
            get
            {
                return m_strLibelleConfig;
            }
            set
            {
                m_strLibelleConfig = value;
            }
        }


    }
}
