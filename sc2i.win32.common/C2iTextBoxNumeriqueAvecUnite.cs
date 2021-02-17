using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.unites;
using System.Drawing;
using sc2i.common;
using System.Windows.Forms;
using System.Collections;

namespace sc2i.win32.common
{
    [AutoExec("Autoexec")]
    public class C2iTextBoxNumeriqueAvecUnite : C2iTextBox
    {
        private System.Windows.Forms.ErrorProvider m_error;
        private System.ComponentModel.IContainer components;
        private string m_strDefaultFormat = "";
        private ContextMenuStrip m_menu;
        private ToolStripMenuItem m_menuAnnuler;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem m_menuCouper;
        private ToolStripMenuItem m_menuCopier;
        private ToolStripMenuItem m_menuColler;
        private ToolStripMenuItem m_menuSupprimer;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem m_menuSelectAll;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem m_menuConvertir;
        private bool m_bUseValueFormat = true;
        private bool m_bAllowNoUnite = false;


        //----------------------------------------------------------
        public C2iTextBoxNumeriqueAvecUnite()
            : base()
        {
            InitializeComponent();
            
            CWin32Traducteur.Translate(m_menu);
            m_menuAnnuler.Click += new EventHandler(m_menuAnnuler_Click);
            m_menuCouper.Click += new EventHandler(m_menuCouper_Click);
            m_menuCopier.Click += new EventHandler(m_menuCopier_Click);
            m_menuColler.Click += new EventHandler(m_menuColler_Click);
            m_menuSupprimer.Click += new EventHandler(m_menuSupprimer_Click);
            m_menuSelectAll.Click += new EventHandler(m_menuSelectAll_Click);
            
        }

        public static void Autoexec()
        {
            CWin32Traducteur.AddProprieteATraduire(typeof(C2iTextBoxNumeriqueAvecUnite), "EmptyText");
        }

        //----------------------------------------------------------
        public bool AllowNoUnit
        {
            get
            {
                return m_bAllowNoUnite;
            }
            set
            {
                m_bAllowNoUnite = value;
            }
        }

        //---------------------------------------------------------
        void m_menuSelectAll_Click(object sender, EventArgs e)
        {
            SelectionStart = 0;
            SelectionLength = Text.Length;
        }

        //---------------------------------------------------------
        void m_menuSupprimer_Click(object sender, EventArgs e)
        {
            if (SelectionLength > 0)
            {
                try
                {
                    Text = Text.Remove(SelectionStart, SelectionLength);
                }
                catch
                {
                }
            }
        }

        //---------------------------------------------------------
        void m_menuColler_Click(object sender, EventArgs e)
        {
            string strClip = Clipboard.GetData(DataFormats.Text) as string;
            if (strClip != null)
            {
                try
                {
                    if (SelectionLength == 0)
                        Text = Text.Insert(SelectionStart, strClip);
                    else
                    {
                        string strText = Text.Remove(SelectionStart, SelectionLength);
                        Text = strText.Insert(SelectionStart, strClip);
                    }
                }
                catch
                {
                }
            }
        }

        //----------------------------------------------------------
        void m_menuCopier_Click(object sender, EventArgs e)
        {
            if ( SelectionLength > 0 )
            {
                try{
                Clipboard.SetData ( DataFormats.Text, Text.Substring(SelectionStart, SelectionLength ));
                }
                catch{}
            }
        }

        //----------------------------------------------------------
        void m_menuCouper_Click(object sender, EventArgs e)
        {
            if ( SelectionLength > 0 )
            {
                try
                {
                Clipboard.SetData ( DataFormats.Text, Text.Substring(SelectionStart, SelectionLength ));
                    Text.Remove(SelectionStart, SelectionLength);
                }
                catch
                {
                }
            }
        }

        //----------------------------------------------------------
        void m_menuAnnuler_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------
        public string DefaultFormat
        {
            get
            {
                return m_strDefaultFormat;
            }
            set
            {
                m_strDefaultFormat = value;
                EmptyText = value;
                Refresh();
            }
        }

        //----------------------------------------------------------
        public bool UseValueFormat
        {
            get
            {
                return m_bUseValueFormat && UniteRacine != null;
            }
            set
            {
                m_bUseValueFormat = value;
            }
        }

        //----------------------------------------------------------
        public IUnite UniteRacine
        {
            get
            {
                foreach (string strUnite in DefaultFormat.Split(' '))
                {
                    if (strUnite.Trim().Length != 0)
                    {
                        return CGestionnaireUnites.GetUnite(strUnite.Trim());
                    }
                }
                return null;
            }
        }


        //----------------------------------------------------------
        public CValeurUnite UnitValue
        {
            get
            {
                CValeurUnite valeur = null;
                try
                {
                    valeur = CValeurUnite.FromString(Text);
                    if (valeur != null && valeur.Unite.Trim() == "")
                    {
                        IUnite uniteDemandee = UniteRacine;
                        if ( uniteDemandee != null )
                            valeur.Unite = uniteDemandee.GlobalId;
                    }
                    if (valeur != null)//Vérifie l'unité
                    {
                        IUnite unite = CGestionnaireUnites.GetUnite(valeur.Unite);
                        if (unite == null)
                            valeur = null;
                        else
                        {
                            IUnite uniteDemandee = UniteRacine;
                            if (uniteDemandee != null &&
                                uniteDemandee.Classe.GlobalId != unite.Classe.GlobalId)
                                valeur = null;
                        }
                    }
                    if (valeur != null)
                    {
                        valeur.Format = CValeurUnite.GetFormat(Text);
                    }
                    if (valeur == null && AllowNoUnit)
                    {
                        if (Text.Trim().Length > 0)
                        {
                            try
                            {
                                double fVal = CUtilDouble.DoubleFromString(Text);
                                valeur = new CValeurUnite(fVal, "");
                            }
                            catch { }
                        }
                    }
                    return valeur;
                    
                }
                catch
                {
                }
                return valeur;
            }
            set
            {
                if (value != null)
                {
                    try
                    {
                        if (!UseValueFormat)
                            Text = value.ToString(DefaultFormat);
                        else
                            Text = value.ToString();
                    }
                    catch
                    {
                        Text = "";
                    }
                }
                else
                    Text = "";
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_error = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuAnnuler = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuCouper = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuCopier = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuColler = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSupprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuConvertir = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.m_error)).BeginInit();
            this.m_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menu
            // 
            this.m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuAnnuler,
            this.toolStripSeparator1,
            this.m_menuCouper,
            this.m_menuCopier,
            this.m_menuColler,
            this.m_menuSupprimer,
            this.toolStripSeparator2,
            this.m_menuSelectAll,
            this.toolStripSeparator3,
            this.m_menuConvertir});
            this.m_menu.Name = "m_menu";
            this.m_menu.Size = new System.Drawing.Size(162, 176);
            this.m_menu.Opening += new System.ComponentModel.CancelEventHandler(this.m_menu_Opening);
            // 
            // m_menuAnnuler
            // 
            this.m_menuAnnuler.Name = "m_menuAnnuler";
            this.m_menuAnnuler.Size = new System.Drawing.Size(161, 22);
            this.m_menuAnnuler.Text = "Cancel|11";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // m_menuCouper
            // 
            this.m_menuCouper.Name = "m_menuCouper";
            this.m_menuCouper.Size = new System.Drawing.Size(161, 22);
            this.m_menuCouper.Text = "Cut|30042";
            // 
            // m_menuCopier
            // 
            this.m_menuCopier.Name = "m_menuCopier";
            this.m_menuCopier.Size = new System.Drawing.Size(161, 22);
            this.m_menuCopier.Text = "Copy|30043";
            // 
            // m_menuColler
            // 
            this.m_menuColler.Name = "m_menuColler";
            this.m_menuColler.Size = new System.Drawing.Size(161, 22);
            this.m_menuColler.Text = "Paste|30044";
            // 
            // m_menuSupprimer
            // 
            this.m_menuSupprimer.Name = "m_menuSupprimer";
            this.m_menuSupprimer.Size = new System.Drawing.Size(161, 22);
            this.m_menuSupprimer.Text = "Delete|20015";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // m_menuSelectAll
            // 
            this.m_menuSelectAll.Name = "m_menuSelectAll";
            this.m_menuSelectAll.Size = new System.Drawing.Size(161, 22);
            this.m_menuSelectAll.Text = "Select all|20016";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // m_menuConvertir
            // 
            this.m_menuConvertir.Name = "m_menuConvertir";
            this.m_menuConvertir.Size = new System.Drawing.Size(161, 22);
            this.m_menuConvertir.Text = "Convert|20017";
            // 
            // C2iTextBoxNumeriqueAvecUnite
            // 
            this.ContextMenuStrip = this.m_menu;
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.OnChangeLockEdition += new System.EventHandler(this.C2iTextBoxNumeriqueAvecUnite_OnChangeLockEdition);
            this.Validated += new System.EventHandler(this.C2iTextBoxNumeriqueAvecUnite_Validated);
            this.Enter += new System.EventHandler(this.C2iTextBoxNumeriqueAvecUnite_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.m_error)).EndInit();
            this.m_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void C2iTextBoxNumeriqueAvecUnite_Validated(object sender, EventArgs e)
        {
            if (Text.Trim().Length > 0 && UnitValue == null )
            {
                IUnite unite = UniteRacine;
                StringBuilder blUnites = new StringBuilder();
                if (unite != null)
                {
                    foreach (IUnite u in CGestionnaireUnites.GetUnites(unite.Classe))
                    {
                        blUnites.Append(u.LibelleCourt);
                        blUnites.Append(" ");
                    }
                    
                }
                else
                {
                    foreach (IClasseUnite classe in CGestionnaireUnites.Classes)
                    {
                        IEnumerable<IUnite> lstUnites = CGestionnaireUnites.GetUnites(classe);
                        if (lstUnites.Count() > 0)
                        {
                            blUnites.Append(Environment.NewLine);
                            blUnites.Append(classe.Libelle + " : ");
                            foreach (IUnite u in lstUnites)
                            {
                                blUnites.Append(u.LibelleCourt);
                                blUnites.Append(',');
                            }
                            blUnites.Remove(blUnites.Length - 1, 1);
                        }
                    }
                }
                m_error.SetError(this, I.T("Enter value using units @1|20014", blUnites.ToString()));
            }
            else
            {
                m_error.SetError(this, "");
                CValeurUnite valeur = UnitValue;
                if (valeur != null)
                    UnitValue = valeur;
            }
        }

        //---------------------------------------------------------------------------
        private void C2iTextBoxNumeriqueAvecUnite_OnChangeLockEdition(object sender, EventArgs e)
        {
            Refresh();
        }

        //---------------------------------------------------------------------------
        private void m_menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_menuCouper.Enabled = SelectionLength > 0;
            m_menuCopier.Enabled = SelectionLength > 0;
            m_menuColler.Enabled = Clipboard.GetData(DataFormats.Text) != null;
            m_menuSupprimer.Enabled = SelectionLength > 0;
            m_menuSelectAll.Enabled = Text.Length > 0;
            foreach (IDisposable dis in new ArrayList(m_menuConvertir.DropDownItems))
                dis.Dispose();
            m_menuConvertir.DropDownItems.Clear();
            IClasseUnite classe = null;
            if ( UniteRacine != null )
            {
                classe = UniteRacine.Classe;
            }
            else
            {
                CValeurUnite v = this.UnitValue;
                if ( v != null && v.Unite != null )
                {
                    IUnite uTmp = CGestionnaireUnites.GetUnite (v.Unite);
                    if ( uTmp != null )
                        classe = uTmp.Classe;
                }
            }

            m_menuConvertir.Enabled = classe != null ;
            

            if (classe != null && UnitValue != null)
            {
                foreach (IUnite unite in CGestionnaireUnites.GetUnites(classe))
                {
                    ToolStripMenuItem itemUnite = new ToolStripMenuItem(unite.LibelleLong);
                    itemUnite.Tag = unite;
                    itemUnite.Click += new EventHandler(itemUnite_Click);
                    m_menuConvertir.DropDownItems.Add(itemUnite);
                }
                ToolStripTextBox box = new ToolStripTextBox();
                m_menuConvertir.DropDownItems.Add(box);
                box.KeyDown += new KeyEventHandler(formatBox_KeyDown);
            }
    
        }

        void formatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
            {
                ToolStripTextBox box = sender as ToolStripTextBox;
                if (box != null && box.Text.Trim() != "" && UnitValue != null)
                {
                    try
                    {
                        Text = UnitValue.ToString(box.Text);
                    }
                    catch { }
                }
                m_menu.Hide();
            }
        }




        //---------------------------------------------------------------------------
        void itemUnite_Click(object sender, EventArgs e)
        {
            CValeurUnite valeur = UnitValue;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            IUnite unite = item != null ? item.Tag as IUnite : null;
            if (unite != null)
            {
                Text = valeur.ToString(unite.GlobalId);
                /*valeur = valeur.ConvertTo(unite.GlobalId);
                valeur.Format = "";
                UnitValue = valeur;*/
            }
        }

        //---------------------------------------------------------------------
        private void C2iTextBoxNumeriqueAvecUnite_Enter(object sender, EventArgs e)
        {
            SelectDecimalPart();
        }

        private void SelectDecimalPart()
        {
            int nEnd = 0;
            foreach (char c in Text)
            {
                if (" 0123456789.,".IndexOf(c) > 0)
                    nEnd++;
                else
                    break;
            }
            SelectionStart = 0;
            SelectionLength = nEnd;
        }

        
        //----------------------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == (int)WindowsMessages.WM_KEYDOWN)
            {
                if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
                {
                    if (OnKeyTab((keyData & Keys.Shift) != Keys.Shift ))
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //----------------------------------------------------------
        private bool OnKeyTab(bool bEnAvant)
        {
            List<CValeurUnite> valeurs = CValeurUnite.DecomposeChaineFormattée(Text);

            int nIndexDansFormat = 0;
            int nSel = SelectionStart;
            StringBuilder bl = new StringBuilder();
            List<int> nStartPos = new List<int>();
            foreach (CValeurUnite valeur in valeurs)
            {
                nStartPos.Add(bl.Length);
                bl.Append(valeur.ToString());
            }

            int nLastUnite = -1;

            List<string> strFormatsDef = new List<string>();
            strFormatsDef.AddRange(DefaultFormat.Split(' '));

            //Navigation dedans
            for (int nIndex = 0; nIndex < valeurs.Count; nIndex++)
            {
                string strTmp = valeurs[nIndex].ToString();

                int nTmp = strFormatsDef.IndexOf(valeurs[nIndex].Unite);
                if ( nTmp > nLastUnite )
                    nLastUnite = nTmp;

                if (nSel < strTmp.Length + nIndexDansFormat)
                {
                    Text = bl.ToString();
                    if (bEnAvant)
                    {
                        SelectionStart = nIndexDansFormat + strTmp.Length;
                        if (nIndex < valeurs.Count - 1)
                            SelectionLength = valeurs[nIndex + 1].Valeur.ToString().Length;
                        return true;
                    }
                    else
                    {
                        if (nIndex == 0)
                            return false;
                        SelectionStart = nStartPos[nIndex-1];
                        SelectionLength = valeurs[nIndex - 1].Valeur.ToString().Length;
                        return true;
                    }
                }
                nIndexDansFormat += strTmp.Length;
            }
            if ( bEnAvant )
            {
                if (nLastUnite + 1 < strFormatsDef.Count() && valeurs.Count > 0 && valeurs[valeurs.Count - 1].Unite.Trim() == "")
                {
                    Text += strFormatsDef[nLastUnite + 1];
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                    return true;
                }
            }
 

            
            
             
            return false;
        }

        

    }
}
