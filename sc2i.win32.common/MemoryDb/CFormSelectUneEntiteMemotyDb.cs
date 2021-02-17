using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.common.memorydb;
using System.Collections.Generic;

namespace sc2i.win32.common.MemoryDb
{
    /// <summary>
    /// Permet de sélectionner un objet donné du type demandé
    /// </summary>
    public class CFormSelectUneEntiteMemoryDb : System.Windows.Forms.Form
    {
        private CMemoryDb m_contexteMemoire = null;
        private string m_strInstructions = "";
        private string m_strProprieteAffichee = "";
        private CFiltreMemoryDb m_filtreRechercheRapide = null;
        private CListeEntitesDeMemoryDbBase m_listeObjets = null;
        private CFiltreMemoryDb m_filtre = null;
        private CEntiteDeMemoryDb m_objetSelectionne = null;
        private Type m_typeObjets = null;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnOK;
        private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
        private sc2i.win32.common.GlacialList m_wndListeElements;
        private System.Windows.Forms.Panel m_panelRecherche;
        private System.Windows.Forms.TextBox m_txtRecherche;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnFiltrer;
        private Panel panel1;
        private PictureBox m_imageType;
        private Panel panel2;
        private CExtStyle cExtStyle1;
        private Label m_lblInstructions;
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public CFormSelectUneEntiteMemoryDb()
        {
            //
            // Requis pour la prise en charge du Concepteur Windows Forms
            //
            InitializeComponent();

            //
            // TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
            //
        }

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
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectUneEntiteMemoryDb));
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_wndListeElements = new sc2i.win32.common.GlacialList();
            this.m_panelRecherche = new System.Windows.Forms.Panel();
            this.m_txtRecherche = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnFiltrer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lblInstructions = new System.Windows.Forms.Label();
            this.m_imageType = new System.Windows.Forms.PictureBox();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelRecherche.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageType)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "DescriptionElement";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Description";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 396;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(249, 8);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Cancel|11";
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOK.Location = new System.Drawing.Point(144, 8);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOK, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOK, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOK.TabIndex = 6;
            this.m_btnOK.Text = "Ok|10";
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_wndListeElements
            // 
            this.m_wndListeElements.AllowColumnResize = true;
            this.m_wndListeElements.AllowMultiselect = false;
            this.m_wndListeElements.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeElements.AlternatingColors = false;
            this.m_wndListeElements.AutoHeight = true;
            this.m_wndListeElements.AutoSort = true;
            this.m_wndListeElements.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeElements.CanChangeActivationCheckBoxes = false;
            this.m_wndListeElements.CheckBoxes = false;
            this.m_wndListeElements.CheckedItems = ((System.Collections.ArrayList)(resources.GetObject("m_wndListeElements.CheckedItems")));
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Column1";
            glColumn1.Propriete = "DescriptionElement";
            glColumn1.Text = "Description|250";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 420;
            this.m_wndListeElements.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1});
            this.m_wndListeElements.ContexteUtilisation = "";
            this.m_wndListeElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeElements.EnableCustomisation = true;
            this.m_wndListeElements.FocusedItem = null;
            this.m_wndListeElements.FullRowSelect = true;
            this.m_wndListeElements.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_wndListeElements.GridColor = System.Drawing.Color.Gray;
            this.m_wndListeElements.HasImages = false;
            this.m_wndListeElements.HeaderHeight = 22;
            this.m_wndListeElements.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeElements.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeElements.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeElements.HeaderVisible = true;
            this.m_wndListeElements.HeaderWordWrap = false;
            this.m_wndListeElements.HotColumnIndex = -1;
            this.m_wndListeElements.HotItemIndex = -1;
            this.m_wndListeElements.HotTracking = false;
            this.m_wndListeElements.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeElements.ImageList = null;
            this.m_wndListeElements.ItemHeight = 17;
            this.m_wndListeElements.ItemWordWrap = false;
            this.m_wndListeElements.ListeSource = null;
            this.m_wndListeElements.Location = new System.Drawing.Point(0, 66);
            this.m_wndListeElements.MaxHeight = 17;
            this.m_wndListeElements.Name = "m_wndListeElements";
            this.m_wndListeElements.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeElements.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeElements.ShowBorder = true;
            this.m_wndListeElements.ShowFocusRect = false;
            this.m_wndListeElements.Size = new System.Drawing.Size(461, 297);
            this.m_wndListeElements.SortIndex = 0;
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeElements.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeElements.TabIndex = 8;
            this.m_wndListeElements.Text = "glacialList1";
            this.m_wndListeElements.TrierAuClicSurEnteteColonne = true;
            // 
            // m_panelRecherche
            // 
            this.m_panelRecherche.BackColor = System.Drawing.Color.White;
            this.m_panelRecherche.Controls.Add(this.m_txtRecherche);
            this.m_panelRecherche.Controls.Add(this.label1);
            this.m_panelRecherche.Controls.Add(this.m_btnFiltrer);
            this.m_panelRecherche.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelRecherche.Location = new System.Drawing.Point(0, 0);
            this.m_panelRecherche.Name = "m_panelRecherche";
            this.m_panelRecherche.Size = new System.Drawing.Size(461, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_panelRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelRecherche.TabIndex = 9;
            // 
            // m_txtRecherche
            // 
            this.m_txtRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtRecherche.Location = new System.Drawing.Point(81, 0);
            this.m_txtRecherche.Name = "m_txtRecherche";
            this.m_txtRecherche.Size = new System.Drawing.Size(356, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtRecherche.TabIndex = 2;
            this.m_txtRecherche.Leave += new System.EventHandler(this.m_txtRecherche_Leave);
            this.m_txtRecherche.Enter += new System.EventHandler(this.m_txtRecherche_Enter);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search|171";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnFiltrer
            // 
            this.m_btnFiltrer.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFiltrer.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFiltrer.Image")));
            this.m_btnFiltrer.Location = new System.Drawing.Point(437, 0);
            this.m_btnFiltrer.Name = "m_btnFiltrer";
            this.m_btnFiltrer.Size = new System.Drawing.Size(24, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnFiltrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnFiltrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnFiltrer.TabIndex = 0;
            this.m_btnFiltrer.Click += new System.EventHandler(this.m_btnFiltrer_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnOK);
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 363);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 34);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_lblInstructions);
            this.panel2.Controls.Add(this.m_imageType);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 42);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 12;
            // 
            // m_lblInstructions
            // 
            this.m_lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblInstructions.Location = new System.Drawing.Point(42, 0);
            this.m_lblInstructions.Name = "m_lblInstructions";
            this.m_lblInstructions.Size = new System.Drawing.Size(419, 42);
            this.cExtStyle1.SetStyleBackColor(this.m_lblInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblInstructions.TabIndex = 4;
            this.m_lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_imageType
            // 
            this.m_imageType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_imageType.Location = new System.Drawing.Point(0, 0);
            this.m_imageType.Name = "m_imageType";
            this.m_imageType.Size = new System.Drawing.Size(42, 42);
            this.cExtStyle1.SetStyleBackColor(this.m_imageType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_imageType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_imageType.TabIndex = 3;
            this.m_imageType.TabStop = false;
            // 
            // CFormSelectUneEntiteMemoryDb
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(461, 397);
            this.Controls.Add(this.m_wndListeElements);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_panelRecherche);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormSelectUneEntiteMemoryDb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Object selection|170";
            this.Load += new System.EventHandler(this.CFormSelectUneEntiteMemoryDb_Load);
            this.m_panelRecherche.ResumeLayout(false);
            this.m_panelRecherche.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageType)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public static CEntiteDeMemoryDb SelectEntiteMemory(CMemoryDb dbMemory, string strInstructions, Type type)
        {
            return SelectObjetDonnee(dbMemory, strInstructions, type, null);
        }

        public static CEntiteDeMemoryDb SelectionRechercheRapide(
            CMemoryDb dbMemory,
            string strInstructions,
            Type type,
            CFiltreMemoryDb filtreDeBase,
            CFiltreMemoryDb filtreRapide,
            string strTexteRechercheRapide,
            string strChampAffiche,
            string strContexte)
        {
            if (type == null || !type.IsSubclassOf(typeof(CEntiteDeMemoryDb)))
            {
                CFormAlerte.Afficher(I.T("Can not select an element from this type|30035"), EFormAlerteType.Erreur);
                return null;
            }
            CFormSelectUneEntiteMemoryDb form = new CFormSelectUneEntiteMemoryDb();
            form.m_filtre = filtreDeBase;
            form.m_strInstructions = strInstructions;
            form.m_contexteMemoire = dbMemory;
            form.m_txtRecherche.Text = strTexteRechercheRapide;
            form.TypeObjets = type;
            form.m_filtreRechercheRapide = filtreRapide;
            if (strChampAffiche == "")
                strChampAffiche = DescriptionFieldAttribute.GetDescriptionField(type, "Libelle", true);
            form.m_strProprieteAffichee = strChampAffiche;
            form.m_wndListeElements.Columns[0].Propriete = form.m_strProprieteAffichee;
            CEntiteDeMemoryDb objet = null;
            form.RefreshListe();
            bool bOk = true;
            if (form.m_listeObjets == null || form.m_listeObjets.Count() != 1)
                bOk = form.ShowDialog() == DialogResult.OK;
            if (bOk)
            {
                objet = form.ObjetDonnee;
            }
            form.Dispose();
            return objet;
        }


        public static CEntiteDeMemoryDb SelectObjetDonnee(CMemoryDb dbMemory, string strInstructions, Type type, CFiltreMemoryDb filtre)
        {
            return SelectObjetDonnee(dbMemory, strInstructions, type, filtre, "DescriptionElement");
        }


        public static CEntiteDeMemoryDb SelectObjetDonnee(CMemoryDb dbMemory, string strInstructions, Type type, CFiltreMemoryDb filtre, string strPropriete)
        {
            if (!type.IsSubclassOf(typeof(CEntiteDeMemoryDb)))
            {
                CFormAlerte.Afficher(I.T("Impossible to select an element from this type|30035"), EFormAlerteType.Erreur);
                return null;
            }
            CFormSelectUneEntiteMemoryDb form = new CFormSelectUneEntiteMemoryDb();
            form.m_filtre = filtre;
            form.m_strInstructions = strInstructions;
            form.TypeObjets = type;
            form.m_strProprieteAffichee = strPropriete;
            form.m_contexteMemoire = dbMemory;
            form.m_wndListeElements.Columns[0].Propriete = strPropriete;
            CEntiteDeMemoryDb objet = null;
            if (form.ShowDialog() == DialogResult.OK)
                objet = form.ObjetDonnee;
            form.Dispose();
            return objet;
        }

        public static CEntiteDeMemoryDb SelectObjetDonnee(CMemoryDb dbMemory, string strInstructions, CListeEntitesDeMemoryDbBase liste, string strPropriete)
        {
            CFormSelectUneEntiteMemoryDb form = new CFormSelectUneEntiteMemoryDb();
            form.m_listeObjets = liste;
            form.m_strInstructions = strInstructions;
            form.m_filtre = liste.Filtre;
            form.TypeObjets = liste.TypeElements;
            form.m_strProprieteAffichee = strPropriete;
            form.m_contexteMemoire = dbMemory;
            form.m_wndListeElements.Columns[0].Propriete = strPropriete;
            CEntiteDeMemoryDb objet = null;
            if (form.ShowDialog() == DialogResult.OK)
                objet = form.ObjetDonnee;
            form.Dispose();
            return objet;
        }

        /// ////////////////////////////////////////////
        private void CFormSelectUneEntiteMemoryDb_Load(object sender, System.EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

            if (m_listeObjets != null)
                m_imageType.Image = DynamicClassAttribute.GetImage(m_listeObjets.TypeElements);
            m_lblInstructions.Text = m_strInstructions;


            Text = sc2i.common.DynamicClassAttribute.GetNomConvivial(m_typeObjets);
            //if ( m_filtreRechercheRapide == null && m_typeObjets != null)
            //	m_filtreRechercheRapide = CFournisseurFiltreRapide.GetFiltreRapideForType ( m_typeObjets );
            if (m_filtreRechercheRapide == null || !m_filtreRechercheRapide.HasFiltre)
                m_panelRecherche.Visible = false;
            RefreshListe();
        }

        /// ////////////////////////////////////////////
        private void RefreshListe()
        {
            CFiltreMemoryDb filtre = null;
            if (m_txtRecherche.Text != null)
            {
                filtre = m_filtreRechercheRapide;
                if (filtre != null)
                {
                    if (filtre.Parametres.Count == 0)
                        filtre.Parametres.Add(m_txtRecherche.Text);
                    else
                        filtre.Parametres[0] = "%" + m_txtRecherche.Text + "%";
                }
            }
            filtre = CFiltreMemoryDb.GetAndFiltre(filtre, m_filtre);

            if (m_listeObjets == null)
            {
                m_listeObjets = new CListeEntitesDeMemoryDbBase(m_contexteMemoire, m_typeObjets);
                m_listeObjets.Filtre = filtre;
            }
            else
                m_listeObjets.Filtre = filtre;

            m_wndListeElements.ListeSource = m_listeObjets;
            m_wndListeElements.Refresh();

            if (m_listeObjets.Count() == 1)
                m_objetSelectionne = (CEntiteDeMemoryDb)m_listeObjets[0];
        }

        /// ////////////////////////////////////////////
        private void m_btnOK_Click(object sender, System.EventArgs e)
        {
            OnOk();
        }

        /// ////////////////////////////////////////////
        private void OnOk()
        {
            if (m_wndListeElements.SelectedItems.Count != 1)
                return;

            m_objetSelectionne = (CEntiteDeMemoryDb)m_wndListeElements.SelectedItems[0];

            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_txtRecherche_Enter(object sender, System.EventArgs e)
        {
            AcceptButton = m_btnFiltrer;
        }

        private void m_txtRecherche_Leave(object sender, System.EventArgs e)
        {
            AcceptButton = m_btnOK;
        }

        private void m_btnFiltrer_Click(object sender, System.EventArgs e)
        {
            RefreshListe();
        }

        /// <summary>
        /// ////////////////////////////////////////////
        /// </summary>
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

        /// ////////////////////////////////////////////
        public CEntiteDeMemoryDb ObjetDonnee
        {
            get
            {
                return m_objetSelectionne;
            }
        }

        /// ////////////////////////////////////////////


    }
}
