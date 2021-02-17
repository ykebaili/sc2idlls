using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using System.Collections;
using sc2i.win32.common;
using System.IO;
using sc2i.common;

namespace sc2i.win32.expression
{
    public partial class CControlEditListeFormulesNommees : UserControl, IControlALockEdition
    {
        CControleEditeFormuleNommee m_textBoxSel = null;
        CObjetPourSousProprietes m_objetAnalyse = null;
        IFournisseurProprietesDynamiques m_fournisseurProps = null;
        Type m_typeFormules = typeof(CFormuleNommee);
        private bool m_bHideNomFormule = false;
        private bool m_bHasAddButton = true;
        private bool m_bHasDeleteButton = true;

        public CControlEditListeFormulesNommees()
        {
            InitializeComponent();
            LockEdition = m_gestionnaireModeEdition.ModeEdition;
        }

        //-----------------------------------------
        public bool HideNomFormule
        {
            get
            {
                return m_bHideNomFormule;
            }
            set
            {
                m_bHideNomFormule = value;
            }
        }


        //-----------------------------------------
        public string HeaderTextForName
        {
            get
            {
                return m_lblLibelle.Text;
            }
            set
            {
                m_lblLibelle.Text = value;
                UpdateVisiHeader();
            }
        }

        //-----------------------------------------
        public string HeaderTextForFormula
        {
            get
            {
                return m_lblFormule.Text;
            }
            set
            {
                m_lblFormule.Text = value;
                UpdateVisiHeader();
            }
        }

        //-----------------------------------------
        public bool HasHadButton
        {
            get
            {
                return m_bHasAddButton;
            }
            set
            {
                m_bHasAddButton = value;
                m_lnkAjouter.Visible = value;
            }
        }

        //-----------------------------------------
        public bool HasDeleteButton
        {
            get
            {
                return m_bHasDeleteButton;
            }
            set
            {
                m_bHasDeleteButton = value;
                m_lnkSupprimer.Visible = value;
            }
        }

        //-----------------------------------------
        private void UpdateVisiHeader()
        {
            m_panelHeader.Visible = HeaderTextForFormula.Trim().Length > 0 || HeaderTextForName.Trim().Length > 0;
        }

        //-----------------------------------------
        public void Init(CFormuleNommee[] formules, CObjetPourSousProprietes objetAnalyse, IFournisseurProprietesDynamiques fournisseurProps)
        {
            this.SuspendDrawing();
            if ( fournisseurProps == null )
                fournisseurProps = new CFournisseurGeneriqueProprietesDynamiques();
            m_fournisseurProps = fournisseurProps;
            m_objetAnalyse = objetAnalyse;
            foreach (Control ctrl in new ArrayList(m_panelFormules.Controls))
            {
                CControleEditeFormuleNommee textBox = ctrl as CControleEditeFormuleNommee;
                if (textBox != null)
                {
                    textBox.Visible = false;
                    m_panelFormules.Controls.Remove(textBox);
                    textBox.Dispose();
                }
            }
            m_textBoxSel = null;
            foreach (CFormuleNommee formule in formules)
            {
                CControleEditeFormuleNommee textBox = CreateTextBoxFormule();
                textBox.FormuleNommee = formule;
            }
            this.ResumeDrawing();
        }

        private CControleEditeFormuleNommee CreateTextBoxFormule()
        {
            CControleEditeFormuleNommee textBox = new CControleEditeFormuleNommee();
            textBox.HideNomFormule = HideNomFormule;
            textBox.TypeFormulesNommees = m_typeFormules;
            m_panelFormules.Controls.Add(textBox);
            textBox.Dock = DockStyle.Top;
            textBox.Height = 44;
            textBox.BringToFront();
            textBox.Init(m_fournisseurProps, m_objetAnalyse);
            textBox.Enter += new EventHandler(textBox_Enter);
            textBox.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
            textBox.TabIndex = m_panelFormules.Controls.Count;
            return textBox;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            if ( m_textBoxSel != null )
                m_textBoxSel.BackColor = Color.White;
            m_textBoxSel = sender as CControleEditeFormuleNommee;
            if (m_textBoxSel != null)
                m_textBoxSel.BackColor = Color.LightGreen;
        }

        private class CTabOrderSorter : IComparer<CControleEditeFormuleNommee>

        {
            #region IComparer<CControleEditeFormuleNommee> Membres

            public int Compare(CControleEditeFormuleNommee x, CControleEditeFormuleNommee y)
            {
                return x.TabIndex.CompareTo(y.TabIndex);
            }

            #endregion
        }

        public Type TypeFormuleNomme
        {
            get
            {
                return m_typeFormules;
            }
            set
            {
                m_typeFormules = value;
            }
        }

        public CFormuleNommee[] GetFormules()
        {
            List<CFormuleNommee> lstFormules = new List<CFormuleNommee>();
            List<CControleEditeFormuleNommee> lstControlesOrdonnees = new List<CControleEditeFormuleNommee>();
            foreach (Control ctrl in m_panelFormules.Controls)
            {
                CControleEditeFormuleNommee textBox = ctrl as CControleEditeFormuleNommee;
                if (textBox != null)
                    lstControlesOrdonnees.Add(textBox);
            }
            lstControlesOrdonnees.Sort ( new CTabOrderSorter());
            foreach (CControleEditeFormuleNommee textBox in lstControlesOrdonnees)
            {
                if (textBox != null)
                {
                    if (textBox.FormuleNommee != null)
                        lstFormules.Add(textBox.FormuleNommee);
                }
            }
            return lstFormules.ToArray();
        }

        private void m_lnkAjouter_LinkClicked(object sender, EventArgs e)
        {
            SuspendLayout();
            CControleEditeFormuleNommee newTextBox = CreateTextBoxFormule();
            newTextBox.Focus();
        }

        private void m_lnkSupprimer_LinkClicked(object sender, EventArgs e)
        {
            if (m_textBoxSel != null)
            {
                m_panelFormules.Controls.Remove(m_textBoxSel);
                m_textBoxSel.Visible = false;
                m_textBoxSel.Dispose();
            }
            m_textBoxSel = null;
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                m_panelTop.Visible = m_gestionnaireModeEdition.ModeEdition;
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        private void m_btnCopy_Click(object sender, EventArgs e)
        {
            List<CFormuleNommee> lst = new List<CFormuleNommee>();
            lst.AddRange(GetFormules());
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
            CResultAErreur result = ser.TraiteListe<CFormuleNommee>(lst);
            if (result)
                Clipboard.SetData(typeof(List<CFormuleNommee>).ToString(), stream.GetBuffer());
            stream.Close();
            writer.Close();
            stream.Dispose();
        }

        private void m_btnPaste_Click(object sender, EventArgs e)
        {
            byte[] data = Clipboard.GetData(typeof(List<CFormuleNommee>).ToString()) as byte[];
            if (data != null)
            {
                try
                {
                    MemoryStream stream = new MemoryStream(data);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire ser = new CSerializerReadBinaire(reader);
                    List<CFormuleNommee> lst = new List<CFormuleNommee>();
                    if (ser.TraiteListe<CFormuleNommee>(lst))
                    {
                        Init(lst.ToArray(), m_typeFormules, m_fournisseurProps);
                    }
                    reader.Close();
                    stream.Close();
                    stream.Dispose();
                }
                catch { }
            }
        }
    }
}
