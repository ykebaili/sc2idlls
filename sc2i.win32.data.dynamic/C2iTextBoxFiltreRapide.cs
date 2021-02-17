using System;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.win32.common;
using sc2i.win32.data;
using System.Collections.Generic;


namespace sc2i.win32.data.dynamic
{
    /// <summary>
    /// Delegué pour remplacer la fenêtre de sélection d'élément
    /// </summary>
    /// <param name="typeObjets">Type des objets filtrés</param>
    /// <param name="filtre">Fitre de base à utiliser pour les objets</param>
    /// <param name="strRechercheRapide">Texte de recherche rapide</param>
    /// <param name="bSelectionPriseEnCharge">En sortie, true si le delegué a pris en charge la sélection</param>
    /// <returns>Retourne l'objet sélectionné</returns>
    public delegate CObjetDonnee SelectObjectFiltreRapideDelegate ( Type typeObjets, CFiltreData filtreDeBase, string strRechercheRapide, ref bool bSelectionPriseEnCharge );

    public delegate string FonctionRetournantTexteNull();

    public enum EModeAffichageImageTextBoxRapide
    {
        Never = 0,
        Always = 1,
        OnSelection
    }
	/// <summary>
	/// Description résumée de C2iTextBoxFiltreRapide.
	/// </summary>
    public class C2iTextBoxFiltreRapide : System.Windows.Forms.UserControl, IControlALockEdition, ISelectionneurElementListeObjetsDonnees
    {
        private static SelectObjectFiltreRapideDelegate m_globalSelectObjetDelegate = null;

        private static SelectObjectFiltreRapideDelegate m_mySelectObjetDelegate = null;

        private List<CConfigTextBoxFiltreRapide> m_listeConfigs = new List<CConfigTextBoxFiltreRapide>();

        private sc2i.win32.common.C2iTextBox m_textBox;
        private System.Windows.Forms.Button m_btn;

        private bool m_bUseIntellisense = true;

        private string m_strOldText = "";
        private CObjetDonnee m_selectedObject = null;

        private EModeAffichageImageTextBoxRapide m_modeIcone = EModeAffichageImageTextBoxRapide.Always;
        private Image m_imageSpecifique = null;

        private FonctionRetournantTexteNull m_fctTextNull = null;
        private string m_strTextNull = "";

        private CConfigTextBoxFiltreRapide m_configDefaut = null;

        

        private bool m_bLockEdition = false;
        private System.Windows.Forms.Button m_btnFlush;
        private ContextMenuStrip m_menuChooseType;
        private PictureBox m_picType;
        private Panel m_panelTotal;
        private IContainer components;

        //-------------------------------------------------------------------------------
        public C2iTextBoxFiltreRapide()
        {
            // Cet appel est requis par le Concepteur de formulaires Windows.Forms.
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------
        public static void SetGlobalSelectObjectDelegate(SelectObjectFiltreRapideDelegate delegue)
        {
            m_globalSelectObjetDelegate = delegue;
        }

        //-------------------------------------------------------------------------------
        public  void SetMySelectObjectDelegate(SelectObjectFiltreRapideDelegate delegue)
        {
            m_mySelectObjetDelegate = delegue;
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

        //-------------------------------------------------------------------------------
        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    if (m_imageSpecifique != null)
                        m_imageSpecifique.Dispose();
                    m_imageSpecifique = null;
                }
            }
            base.Dispose(disposing);
        }

        //-------------------------------------------------------------------------------
        #region Component Designer generated code
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C2iTextBoxFiltreRapide));
            this.m_textBox = new sc2i.win32.common.C2iTextBox();
            this.m_btn = new System.Windows.Forms.Button();
            this.m_btnFlush = new System.Windows.Forms.Button();
            this.m_menuChooseType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_picType = new System.Windows.Forms.PictureBox();
            this.m_panelTotal = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).BeginInit();
            this.m_panelTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_textBox
            // 
            this.m_textBox.AllowDrop = true;
            this.m_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_textBox.EmptyText = "";
            this.m_textBox.Location = new System.Drawing.Point(20, 0);
            this.m_textBox.LockEdition = false;
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(164, 20);
            this.m_textBox.TabIndex = 0;
            this.m_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragDrop);
            this.m_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyDown);
            this.m_textBox.Leave += new System.EventHandler(this.m_textBox_Leave);
            this.m_textBox.Enter += new System.EventHandler(this.m_textBox_Enter);
            this.m_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragEnter);
            this.m_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragOver);
            // 
            // m_btn
            // 
            this.m_btn.BackColor = System.Drawing.Color.White;
            this.m_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.m_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btn.Image = global::sc2i.win32.data.dynamic.Properties.Resources.search;
            this.m_btn.Location = new System.Drawing.Point(184, 0);
            this.m_btn.Name = "m_btn";
            this.m_btn.Size = new System.Drawing.Size(24, 20);
            this.m_btn.TabIndex = 1;
            this.m_btn.TabStop = false;
            this.m_btn.UseVisualStyleBackColor = false;
            this.m_btn.Click += new System.EventHandler(this.m_btn_Click);
            // 
            // m_btnFlush
            // 
            this.m_btnFlush.BackColor = System.Drawing.Color.White;
            this.m_btnFlush.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnFlush.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFlush.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.m_btnFlush.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnFlush.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btnFlush.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnFlush.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFlush.Image")));
            this.m_btnFlush.Location = new System.Drawing.Point(208, 0);
            this.m_btnFlush.Name = "m_btnFlush";
            this.m_btnFlush.Size = new System.Drawing.Size(24, 20);
            this.m_btnFlush.TabIndex = 2;
            this.m_btnFlush.TabStop = false;
            this.m_btnFlush.UseVisualStyleBackColor = false;
            this.m_btnFlush.Click += new System.EventHandler(this.m_btnFlush_Click);
            // 
            // m_menuChooseType
            // 
            this.m_menuChooseType.Name = "m_menuChooseType";
            this.m_menuChooseType.Size = new System.Drawing.Size(61, 4);
            // 
            // m_picType
            // 
            this.m_picType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_picType.Location = new System.Drawing.Point(0, 0);
            this.m_picType.Name = "m_picType";
            this.m_picType.Size = new System.Drawing.Size(20, 20);
            this.m_picType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picType.TabIndex = 3;
            this.m_picType.TabStop = false;
            this.m_picType.Visible = false;
            this.m_picType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseMove);
            this.m_picType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseDown);
            this.m_picType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseUp);
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Controls.Add(this.m_textBox);
            this.m_panelTotal.Controls.Add(this.m_btn);
            this.m_panelTotal.Controls.Add(this.m_picType);
            this.m_panelTotal.Controls.Add(this.m_btnFlush);
            this.m_panelTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(232, 20);
            this.m_panelTotal.TabIndex = 4;
            // 
            // C2iTextBoxFiltreRapide
            // 
            this.Controls.Add(this.m_panelTotal);
            this.Name = "C2iTextBoxFiltreRapide";
            this.Size = new System.Drawing.Size(232, 21);
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).EndInit();
            this.m_panelTotal.ResumeLayout(false);
            this.m_panelTotal.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        public event EventHandler OnChangeLockEdition;
        public event EventHandler OnSelectedObjectChanged;
        //------------------------------------------------------------------
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

        //--------------------------------------------------------------
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
        private void SynchroniseTextEtObjet()
        {
            if (m_selectedObject == null)
                Text = TextNull;
            else
            {
                foreach (CConfigTextBoxFiltreRapide config in m_listeConfigs)
                {
                    if (config.TypeObjets != null && config.TypeObjets.IsAssignableFrom(m_selectedObject.GetType()))
                    {
                        Text = CInterpreteurTextePropriete.GetStringValue(m_selectedObject, config.ProprieteAffichee, "");
                        break;
                    }
                }
            }
            m_strOldText = Text;
            UpdateImage();
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
        public CObjetDonnee SelectedObject
        {
            get
            {
                //Nécéssaire pour les grilles: SI le contrôle est dans une grille,
                //on peut interroger sa valeur SelectedObject avec que le lostFocus
                //ne soit détecté par le contrôle
                if (m_textBox.Focused && m_strOldText != Text)
                    SelectObject();
                return m_selectedObject;
            }
            set
            {
                m_selectedObject = value;
                SynchroniseTextEtObjet();
                if (OnSelectedObjectChanged != null)
                    OnSelectedObjectChanged(this, new EventArgs());
                if (ElementSelectionneChanged != null)
                    ElementSelectionneChanged(this, new EventArgs());
            }
        }
        //------------------------------------------------------------------
        private void m_textBox_Enter(object sender, System.EventArgs e)
        {
            m_strOldText = Text;
            if ( m_listeIntellisense == null && m_bUseIntellisense && IsHandleCreated  )
                BeginInvoke(new MethodInvoker(InitIntellisense));
        }
        //------------------------------------------------------------------
        private void m_textBox_Leave(object sender, System.EventArgs e)
        {
            if (Text != m_strOldText)
                SelectObject();
        }
        //------------------------------------------------------------------
        private void m_btn_Click(object sender, System.EventArgs e)
        {
            m_configDefaut = null;
            SelectObject();
        }
        //------------------------------------------------------------------
        private void m_btnFlush_Click(object sender, System.EventArgs e)
        {
            SelectedObject = null;
        }
        //------------------------------------------------------------------
        private bool m_bIsInit = false;

        //------------------------------------------------------------------
        private int GetIndexSeparateurNextWord (string strText )
        {
            string strSeparateurs = " /,";
            int nIndex = -1;
            foreach (char c in strSeparateurs)
            {
                int nTmp = strText.IndexOf(c);
                if (nTmp >= 0)
                {
                    if (nIndex < 0)
                        nIndex = nTmp;
                    else
                        nIndex = Math.Min(nIndex, nTmp);
                }
            }
            return nIndex;
        }

        //------------------------------------------------------------------
        private Dictionary<string, HashSet<CReferenceObjetDonnee>> m_dicIntellisense = null;
        private List<string> m_listeIntellisense = null;
        private void InitIntellisense()
        {
            if (m_dicIntellisense == null)
            {
                m_dicIntellisense = new Dictionary<string, HashSet<CReferenceObjetDonnee>>();
                HashSet<string> setMots = new HashSet<string>();
                try
                {
                    if (m_listeConfigs != null && m_bUseIntellisense)
                    {
                        foreach (CConfigTextBoxFiltreRapide config in m_listeConfigs)
                        {
                            try
                            {
                                /*if (!typeof(IObjetALectureTableComplete).IsAssignableFrom(config.TypeObjets))
                                    continue;//Ne met en intellisense que ce qui est en lectutre table complète*/
                                CListeObjetsDonnees lstObjs = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, config.TypeObjets);
                                CFiltreData filtre = config.FiltreDeBase;
                                lstObjs.Filtre = filtre;
                                
                                if( lstObjs.CountNoLoad < 4000 )
                                {
                                    foreach (CObjetDonnee obj in lstObjs)
                                    {
                                        string strText = CInterpreteurTextePropriete.GetStringValue(obj, config.ProprieteAffichee, "");
                                        if (strText.Trim().Length > 0)
                                        {
                                            strText = strText.Trim();
                                            HashSet<string> lstStrings = new HashSet<string>();
                                            lstStrings.Add(strText);
                                            int nIndex = GetIndexSeparateurNextWord(strText);
                                            // strText.IndexOf(' ');
                                            while (nIndex >= 0)
                                            {
                                                strText = strText.Substring(nIndex+1).Trim();
                                                lstStrings.Add(strText);
                                                nIndex = GetIndexSeparateurNextWord(strText);
                                                //nIndex = strText.IndexOf(' ');
                                            }
                                            
                                            foreach (string strMotTmp in strText.Split(' '))
                                            {
                                                if (strMotTmp.Length > 2)
                                                {
                                                    if (strMotTmp.Trim().Length > 0)
                                                        lstStrings.Add(strMotTmp);
                                                }
                                            }
                                            CReferenceObjetDonnee refObj = new CReferenceObjetDonnee(obj);
                                            foreach (string strMot in lstStrings)
                                            {
                                                HashSet<CReferenceObjetDonnee> lst = null;
                                                foreach (string strMotToutSeul in strMot.Split(' '))
                                                {
                                                    if (!m_dicIntellisense.TryGetValue(strMotToutSeul.ToUpper(), out lst))
                                                    {
                                                        lst = new HashSet<CReferenceObjetDonnee>();
                                                        m_dicIntellisense[strMotToutSeul.ToUpper()] = lst;
                                                    }
                                                    lst.Add(refObj);
                                                }
                                                if (!m_dicIntellisense.TryGetValue(strMot.ToUpper(), out lst))
                                                {
                                                    lst = new HashSet<CReferenceObjetDonnee>();
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
        public void Init ( Type typeObjets, string strProprieteAffichee, bool bForceInit )
        {
            InitAvecFiltreDeBase ( typeObjets, strProprieteAffichee, null, bForceInit );
        }
        
        //------------------------------------------------------------------
        public void InitAvecFiltreDeBase(Type typeObjets, string strProprieteAffichee, CFiltreData filtreDeBase, bool bForceInit)
        {
            if (!bForceInit && m_bIsInit)
                return;
            m_listeIntellisense = null;
            m_listeConfigs.Clear();
            m_listeConfigs.Add ( new CConfigTextBoxFiltreRapide(
                typeObjets,
                filtreDeBase,
                strProprieteAffichee ));
            m_bIsInit = true;
            UpdateImage();
        }

        //------------------------------------------------------------------
        public void InitMultiple(CConfigTextBoxFiltreRapide[] configs, bool bForceInit)
        {
            if (!bForceInit && m_bIsInit)
                return;
            m_listeConfigs.Clear();
            m_listeConfigs.AddRange(configs);
            m_bIsInit = true;
            UpdateImage();
        }

        //------------------------------------------------------
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
        public void SelectObject()
        {
            string strText = m_textBox.Text;
            HashSet<CReferenceObjetDonnee> lstRefs = null;
            if (m_dicIntellisense != null && strText.Trim().Length > 0 && m_dicIntellisense.TryGetValue(strText.ToUpper(), out lstRefs))
            {
                if (lstRefs.Count == 1)
                {
                    SelectedObject = lstRefs.ElementAt(0).GetObjet(CSc2iWin32DataClient.ContexteCourant);
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
                foreach (CConfigTextBoxFiltreRapide config in m_listeConfigs)
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
        private void SelectObject ( CConfigTextBoxFiltreRapide config )
        {
            if (config != null)
                m_configDefaut = config;
            CObjetDonnee obj = null;
            bool bSelectPriseEnChargeParDelegue = false;
            if (m_mySelectObjetDelegate != null)
            {
                obj = m_mySelectObjetDelegate.Invoke(config.TypeObjets, config.FiltreDeBase, m_textBox.Text, ref bSelectPriseEnChargeParDelegue);
            }
            if (m_globalSelectObjetDelegate != null && !bSelectPriseEnChargeParDelegue)
            {
                obj = m_globalSelectObjetDelegate.Invoke(config.TypeObjets, config.FiltreDeBase, m_textBox.Text, ref bSelectPriseEnChargeParDelegue);
            }
            if (!bSelectPriseEnChargeParDelegue)
            {
                obj = CFormSelectUnObjetDonnee.SelectionRechercheRapide(
                    "",
                    config.TypeObjets,
                    config.FiltreDeBase,
                    m_textBox.Text,
                    config.ProprieteAffichee,
                    "");
            }
            if (obj != null)
                SelectedObject = obj;
            else
                SynchroniseTextEtObjet();
        }

        //-------------------------------------------------
        void itemSelectType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CConfigTextBoxFiltreRapide config = item != null ? item.Tag as CConfigTextBoxFiltreRapide : null;
            if (config != null)
            {
                SelectObject(config);
            }
        }

        #region Membres de ISelectionneurElementListeObjetsDonnees

        public CObjetDonnee ElementSelectionne
        {
            get
            {
                return SelectedObject;
            }
            set
            {
                SelectedObject = value;
            }
        }

        public event System.EventHandler ElementSelectionneChanged;

        public bool IsUpdating()
        {
            // TODO : ajoutez l'implémentation de C2iTextBoxFiltreRapide.IsUpdating
            return false;
        }

        #endregion

        //-------------------------------------------------
        private void m_textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectObject();
            }
        }

        //--------------------------------------------------
        public CConfigTextBoxFiltreRapide[] Configs
        {
            get
            {
                return m_listeConfigs.ToArray();
            }
        }

        //--------------------------------------------------
        private void TesteDragDrop(DragEventArgs e)
        {
            if (LockEdition)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            CReferenceObjetDonnee refObj = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
            if (refObj != null)
            {
                foreach ( CConfigTextBoxFiltreRapide config in m_listeConfigs)
                {
                    if (config.TypeObjets != null && config.TypeObjets.IsAssignableFrom(refObj.TypeObjet))
                    {
                e.Effect = DragDropEffects.Link;
                        return;
                    }
                }
            }
            else
                e.Effect = DragDropEffects.None;
        }

        //--------------------------------------------------
        private void m_textBox_DragDrop(object sender, DragEventArgs e)
        {
            CReferenceObjetDonnee refObj = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
            foreach (CConfigTextBoxFiltreRapide config in m_listeConfigs)
            {
                if (config.TypeObjets != null && config.TypeObjets.IsAssignableFrom(refObj.TypeObjet))
                {
                    CObjetDonnee objet = refObj.GetObjet(CSc2iWin32DataClient.ContexteCourant);
                    if (objet != null)
                    {
                        if (config.FiltreDeBase == null)
                            ElementSelectionne = objet;
                        else
                        {
                            CFiltreData filtre = CFiltreData.GetAndFiltre(config.FiltreDeBase,
                                objet.GetFiltreCles(objet.GetValeursCles()));
                            CListeObjetsDonnees lst = new CListeObjetsDonnees(objet.ContexteDonnee, config.TypeObjets, filtre);
                            if (lst.Count == 1)
                                ElementSelectionne = objet;
                        }
                    }
                    return;
                }
            }
        }

        //--------------------------------------------------
        private void m_textBox_DragEnter(object sender, DragEventArgs e)
        {
            TesteDragDrop(e);
        }

        //--------------------------------------------------
        private void m_textBox_DragOver(object sender, DragEventArgs e)
        {
            TesteDragDrop(e);
        }

        //--------------------------------------------------
        public C2iTextBox TextBox
        {
            get
            {
                return m_textBox;
            }
        }

        //--------------------------------------------------
        public void SelectAll()
        {
            m_textBox.SelectAll();
        }

        //--------------------------------------------------
        public int SelectionStart
        {
            get
            {
                return m_textBox.SelectionStart;
            }
            set
            {
                m_textBox.SelectionStart = value;
            }
        }

        //--------------------------------------------------
        public int SelectionLength
        {
            get
            {
                return m_textBox.SelectionLength;
            }
            set
            {
                m_textBox.SelectionLength = value;
            }
        }


        //--------------------------------------------------
        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData)
            {
                case Keys.Shift:
                    return true;
                case Keys.Right:
                case Keys.End:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_textBox != null && m_textBox.SelectionStart == m_textBox.Text.Length &&
                        m_textBox.SelectionLength == 0)
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_textBox != null && m_textBox.SelectionStart == 0 && m_textBox.SelectionLength == 0)
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        private Point? m_ptStartDrag = null;
        private void m_picType_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_ptStartDrag = new Point(e.X, e.Y);
                m_picType.Capture = true;
            }
        }

        private void m_picType_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                ElementSelectionne != null &&
                m_ptStartDrag != null)
            {
                if (Math.Abs(m_ptStartDrag.Value.X - e.X) > 3 ||
                    Math.Abs(m_ptStartDrag.Value.Y - e.Y) > 3)
                {
                    CReferenceObjetDonneeDragDropData data = new CReferenceObjetDonneeDragDropData(ElementSelectionne);
                    m_picType.Capture = false;
                    m_ptStartDrag = null;
                    DoDragDrop(data, DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Copy);
                }
            }
        }

        private void m_picType_MouseUp(object sender, MouseEventArgs e)
        {
            m_ptStartDrag = null;
            m_picType.Capture = false;
        }

        

    }


    public class CConfigTextBoxFiltreRapide
    {
        private string m_strLibelleConfig = "";
        private Type m_typeObjets;
        private CFiltreData m_filtreDeBase = null;
        private string m_strPropriete = "";

        //-------------------------------------------------
        public CConfigTextBoxFiltreRapide()
        {
        }

        //-------------------------------------------------
        public CConfigTextBoxFiltreRapide(Type typeObjets,
            CFiltreData filtreDeBase,
            string strPropriete)
            : this(null, typeObjets, filtreDeBase, strPropriete)
        { }


        //-------------------------------------------------
        public CConfigTextBoxFiltreRapide(
            string strLibelleConfig,
            Type typeObjets,
            CFiltreData filtreDeBase,
            string strPropriete)
        {
            if (strLibelleConfig == null || strLibelleConfig.Trim().Length == 0)
                strLibelleConfig = DynamicClassAttribute.GetNomConvivial(typeObjets);
            m_strLibelleConfig = strLibelleConfig;
            m_typeObjets = typeObjets;
            m_filtreDeBase = filtreDeBase;
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
        public CFiltreData FiltreDeBase
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
            get{
                return m_strLibelleConfig;
            }
            set{
                m_strLibelleConfig = value;
            }
        }


    }



	
}
