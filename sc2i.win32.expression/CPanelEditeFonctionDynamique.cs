using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression.FonctionsDynamiques;
using sc2i.expression;
using sc2i.common;
using System.Collections;

namespace sc2i.win32.expression
{
    public partial class CPanelEditeFonctionDynamique : UserControl
    {
        private CFonctionDynamique m_fonction = null;
        private bool m_bAfficheNom = false;
        private Dictionary<string, Type> m_dicVariablesSpecifiques = new Dictionary<string,Type>();

        private CObjetPourSousProprietes m_objetPourSousProprietes = null;
        public CPanelEditeFonctionDynamique()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------

        //--------------------------------------------------------------------
        public void Init(
            CFonctionDynamique fonction,
            CObjetPourSousProprietes objetAnalyse,
            bool bAvecName)
        {
            m_fonction = fonction;
            m_objetPourSousProprietes = objetAnalyse;
            FillListeParametres();
            m_txtNomFonction.Text = fonction.Nom;
            m_txtFormule.Init(fonction,
                objetAnalyse);
            m_txtFormule.Formule = fonction.Formule;
            m_panelNom.Visible = bAvecName;
            m_bAfficheNom = bAvecName;
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// Permet d'ajouter au menu "Add" de variables, des types spécifiques
        /// pour simplifier l'utilisation par l'utilisateur qui utilise :-)
        /// </summary>
        /// <param name="strNom"></param>
        /// <param name="typeVariable"></param>
        public void AddSpecificVariableTypes(string strNom, Type typeVariable)
        {
            m_dicVariablesSpecifiques[strNom] = typeVariable;
        }


        //--------------------------------------------------------------------
        private void FillListeParametres()
        {
            m_wndListeParametres.Items.Clear();
            m_wndListeParametres.BeginUpdate();
            foreach (CParametreFonctionDynamique p in m_fonction.Parametres)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, p);
                m_wndListeParametres.Items.Add(item);
            }
            m_wndListeParametres.EndUpdate();
            UpdateParametres();
        }

        //--------------------------------------------------------------------
        private void m_lnkAddVar_LinkClicked(object sender, EventArgs e)
        {
            if (m_dicVariablesSpecifiques.Count > 0)
            {
                foreach (ToolStripMenuItem item in new ArrayList(m_menuAddVariable.Items))
                {
                    m_menuAddVariable.Items.Remove(item);
                    item.Dispose();
                }
                ToolStripMenuItem itemAddVariable = new ToolStripMenuItem(I.T("Parameter|20034"));
                itemAddVariable.Click += new EventHandler(itemAddVariable_Click);
                itemAddVariable.Tag = typeof(string);
                m_menuAddVariable.Items.Add(itemAddVariable);

                foreach (KeyValuePair<string, Type> kv in m_dicVariablesSpecifiques)
                {
                    itemAddVariable = new ToolStripMenuItem(kv.Key);
                    itemAddVariable.Tag = kv.Value;
                    itemAddVariable.Click += new EventHandler(itemAddVariable_Click);
                    m_menuAddVariable.Items.Add(itemAddVariable);
                }
                m_menuAddVariable.Show(m_lnkAddVar, new Point(0, m_lnkAddVar.Height));
                return;
            }
            AddVariable(typeof(string));
        }

        //--------------------------------------------------------------------
        private void itemAddVariable_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripMenuItem;
            Type tp = item != null ? item.Tag as Type : null;
            if (tp != null)
            {
                AddVariable(tp);
            }
        }

        //--------------------------------------------------------------------
        private void AddVariable ( Type typeVariable )
        {
            CParametreFonctionDynamique parametre = new CParametreFonctionDynamique();
            parametre.TypeResultatExpression = new CTypeResultatExpression ( typeVariable, false );
            parametre = CFormEditVariableFormule.EditeParametreFonction(parametre);
            if (parametre != null)
            {
                ListViewItem item = new ListViewItem();
                FillItem ( item, parametre );
                m_wndListeParametres.Items.Add ( item );
                UpdateParametres();
            }
        }

        //--------------------------------------------------------------------
        private void m_lnkRemoveVar_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeParametres.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeParametres.SelectedItems[0];
                CParametreFonctionDynamique parametre = item.Tag as CParametreFonctionDynamique;
                if (parametre != null)
                {
                    m_wndListeParametres.Items.Remove(item);
                    UpdateParametres();
                }

            }
        }

        //--------------------------------------------------------------------
        private void FillItem(ListViewItem item, CParametreFonctionDynamique parametre)
        {
            item.Tag = parametre;
            item.Text = parametre.Nom;
        }

        //--------------------------------------------------------------------
        private void UpdateParametres()
        {
            int nIndex = 0;
            List<CParametreFonctionDynamique> parametres = new List<CParametreFonctionDynamique>();
            foreach (ListViewItem item in m_wndListeParametres.Items)
            {
                CParametreFonctionDynamique parametre = item.Tag as CParametreFonctionDynamique;
                if (parametre != null)
                    parametre.NumParametre = nIndex++;
                parametres.Add(parametre);
            }
            m_fonction.Parametres = parametres;
            m_txtFormule.Init(m_fonction, m_objetPourSousProprietes);
        }

        //--------------------------------------------------------------------
        private void m_wndListeVariables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = m_wndListeParametres.HitTest(e.X, e.Y);
            if (info != null)
            {
                CParametreFonctionDynamique parametre = info.Item.Tag as CParametreFonctionDynamique;
                if (parametre != null)
                {
                    CParametreFonctionDynamique newParam = CFormEditVariableFormule.EditeParametreFonction(parametre);
                    if (newParam != null)
                    {
                        FillItem(info.Item, newParam);
                        UpdateParametres();
                    }
                }
            }
        }

        //---------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            C2iExpression formule = m_txtFormule.Formule;
            if (formule == null)
            {
                result = m_txtFormule.ResultAnalyse;
                return result;
            }
            string strNom = m_txtNomFonction.Text.Replace(" ","_");
            m_fonction.Formule = formule;
            if (m_bAfficheNom)
            {
                m_fonction.Nom = strNom;
            }
            return result;
        }
    }
}
