using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;

namespace sc2i.win32.data
{
    public partial class CControlEditeParametreDeFiltreData : UserControl, IControlALockEdition
    {
        internal enum ETypeObjet
        {
            Texte,
            Entier,
            Decimal,
            Date,
            Bool,
        }

        private int m_nNumParametre = 1;
        private Control m_controleEdition = null;


        public CControlEditeParametreDeFiltreData()
        {
            InitializeComponent();
            m_cmbType.Items.Add(ETypeObjet.Texte);
            m_cmbType.Items.Add(ETypeObjet.Entier);
            m_cmbType.Items.Add(ETypeObjet.Decimal);
            m_cmbType.Items.Add(ETypeObjet.Date);
            m_cmbType.Items.Add(ETypeObjet.Bool);
            m_cmbType.SelectedItem = ETypeObjet.Texte;
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
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        private void m_cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TypeObjet = (ETypeObjet)m_cmbType.SelectedItem;
        }

        private ETypeObjet TypeObjet
        {
            get
            {
                if (m_cmbType.SelectedItem == null)
                    return ETypeObjet.Texte;
                return (ETypeObjet)m_cmbType.SelectedItem;
            }
            set
            {
                m_cmbType.SelectedItem = value;
                switch (TypeObjet)
                {
                    case ETypeObjet.Texte:
                        m_controleEdition = m_txtBox;
                        m_txtBox.Text = "";
                        break;
                    case ETypeObjet.Entier:
                        m_controleEdition = m_txtBoxInt;
                        m_txtBoxInt.IntValue = 0;
                        break;
                    case ETypeObjet.Decimal:
                        m_controleEdition = m_txtBoxDouble;
                        m_txtBoxDouble.DoubleValue = 0;
                        break;
                    case ETypeObjet.Date:
                        m_controleEdition = m_dtPicker;
                        m_dtPicker.Value = DateTime.Now;
                        break;
                    case ETypeObjet.Bool:
                        m_controleEdition = m_checkBox;
                        m_checkBox.Checked = false;
                        break;

                    default:
                        break;
                }

                m_checkBox.Visible = false;
                m_txtBox.Visible = false;
                m_txtBoxInt.Visible = false;
                m_txtBoxDouble.Visible = false;
                m_dtPicker.Visible = false;
                m_controleEdition.Visible = true;
                m_controleEdition.Dock = DockStyle.Fill;
            }

        }

        public object Valeur
        {
            get
            {
                if (m_controleEdition == m_txtBoxDouble)
                    return m_txtBoxDouble.DoubleValue;
                if (m_controleEdition == m_txtBoxInt)
                    return m_txtBoxInt.IntValue;
                if (m_controleEdition == m_checkBox)
                    return m_checkBox.Checked;
                if (m_controleEdition == m_dtPicker)
                    return m_dtPicker.Value;
                if (m_controleEdition == m_txtBox)
                    return m_txtBox.Text;
                return "";
            }
            set
            {
                if (value is int)
                {
                    TypeObjet = ETypeObjet.Entier;
                    m_txtBoxInt.IntValue = (int)value;
                }
                else if (value is double)
                {
                    TypeObjet = ETypeObjet.Decimal;
                    m_txtBoxDouble.DoubleValue = (double)value;
                }
                else if (value is bool)
                {
                    TypeObjet = ETypeObjet.Bool;
                    m_checkBox.Checked = (bool)value;
                }
                else
                {
                    TypeObjet = ETypeObjet.Texte;
                    if (value != null)
                        m_txtBox.Text = value.ToString();
                }
            }
        }

        public int NumParametre
        {
            get
            {
                return m_nNumParametre;
            }
            set
            {
                m_nNumParametre = value;
                m_lblNumParametre.Text = "@" + m_nNumParametre;
            }
        }

        public event EventHandler OnDelete;

        private void m_btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDelete != null)
                OnDelete(this, e);
        }

    }
}
