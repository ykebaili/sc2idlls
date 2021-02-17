using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire.win32
{
    public partial class CPanelEditColonneMultiSelect : UserControl
    {
        private Type m_typeElements = typeof(string);
        private int m_nIndex = 0;

        private C2iWndMultiSelect.CColonneMultiSelect m_colonne = null;
        public CPanelEditColonneMultiSelect()
        {
            InitializeComponent();
        }

        public void Init(
            int nIndex,
            C2iWndMultiSelect.CColonneMultiSelect colonne,
            Type typeElement)
        {
            m_nIndex = nIndex;
            m_colonne = colonne;
            m_txtLargeur.IntValue = colonne.Largeur;
            m_txtNom.Text = colonne.Nom;
            m_typeElements = typeElement;
            m_txtFormule.Init(new CFournisseurGeneriqueProprietesDynamiques(),
                typeElement);
            m_txtFormule.Formule = colonne.Formule;
        }

        public int Index
        {
            get
            {
                return m_nIndex;
            }
            set
            {
                m_nIndex = value;
            }
        }

        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_txtLargeur.IntValue != null)
                m_colonne.Largeur = m_txtLargeur.IntValue.Value;
            m_colonne.Nom = m_txtNom.Text;
            m_colonne.Formule = m_txtFormule.Formule;
            return result;
        }

        public event EventHandler OnDeleteClick;

        private void m_btnDelete_LinkClicked(object sender, EventArgs e)
        {
            if (OnDeleteClick != null)
            {
                OnDeleteClick(this, null);
            }
        }

        public C2iWndMultiSelect.CColonneMultiSelect Colonne
        {
            get
            {
                return m_colonne;
            }
        }

    }
}
