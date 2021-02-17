using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.win32.expression;
using sc2i.expression;
using sc2i.data;

namespace sc2i.win32.data.dynamic.SmartField
{
    [AutoExec("Autoexec")]
    public partial class CFormEditeSmartField : Form
    {
        private CSmartField m_smartField = null;

        //-----------------------------------------------------------------------
        public CFormEditeSmartField()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------
        public static void Autoexec()
        {
            CControlAideFormule.SetOnRightClickChampFunc ( new CControlAideFormule.OnRightClickChampDelegate(OnRightClickAideFormule) );
        }

        //-----------------------------------------------------------------------
        private class CTagMenu
        {
            public readonly Type TypeSource;
                public readonly CDefinitionProprieteDynamique DefProp;
            public readonly CSmartField SmartField;

            public CTagMenu ( Type typeSource, CDefinitionProprieteDynamique defProp, CSmartField smartField )
            {
                TypeSource = typeSource;
                DefProp = defProp;
                SmartField = smartField;
            }
        }


        //-----------------------------------------------------------------------
        public static void OnRightClickAideFormule(Type typeSource, CDefinitionProprieteDynamique defProp, Point screenPoint)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = null;
            CDefinitionProprieteDynamiqueSmartField def = defProp as CDefinitionProprieteDynamiqueSmartField;
            if (def != null)
            {
                CSmartField smartField = new CSmartField(CSc2iWin32DataClient.ContexteCourant);
                if (smartField.ReadIfExists(def.IdSmartField))
                {
                    CTagMenu tag = new CTagMenu(typeSource, defProp, smartField);
                    item = new ToolStripMenuItem(I.T("Edit smart field|20083"));
                    item.Tag = tag;
                    item.Click += new EventHandler(itemEditSmartField_Click);
                    menu.Items.Add(item);

                    item = new ToolStripMenuItem(I.T("Delete smart field|20084"));
                    item.Tag = tag;
                    item.Click += new EventHandler(itemDeleteSmartField_Click);
                    menu.Items.Add(item);
                }
            }
            else
            {
                item = new ToolStripMenuItem(I.T("Create smart field|20082"));
                item.Tag = new CTagMenu ( typeSource, defProp, null);
                item.Click += new EventHandler(itemCreateSmartField_Click);
                menu.Items.Add(item);
            }
            if (menu.Items.Count > 0)
            {
                menu.Show(screenPoint);
            }
        }
                
        //-----------------------------------------------------------------------
        private static void itemCreateSmartField_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CTagMenu tag = item != null ? item.Tag as CTagMenu : null;
            if (tag != null)
            {
                CSmartField smartField = new CSmartField(CSc2iWin32DataClient.ContexteCourant);
                smartField.CreateNew();
                smartField.TypeCible = tag.TypeSource;
                smartField.Definition = tag.DefProp;
                CFormEditeSmartField.EditeSmartFieldAndCommit(smartField);
            }
        }

        //-----------------------------------------------------------------------
        private static void itemDeleteSmartField_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CTagMenu tag = item != null ? item.Tag as CTagMenu : null;
            if (tag != null && tag.SmartField != null)
            {
                using (CContexteDonnee ctx = new CContexteDonnee(CSc2iWin32DataClient.ContexteCourant.IdSession, true, false))
                {
                    CSmartField smartField = tag.SmartField.GetObjetInContexte(ctx) as CSmartField;
                    if (smartField != null)
                    {
                        if (MessageBox.Show(I.T("Delete SmartField @1 ?|20085", smartField.Libelle),
                            "",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CResultAErreur result = smartField.Delete();
                            if (!result)
                                CFormAlerte.Afficher(result.Erreur);
                        }
                    }
                }
            }
        }

        //-----------------------------------------------------------------------
        private static void itemEditSmartField_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CTagMenu tag = item != null ? item.Tag as CTagMenu : null;
            if (tag != null && tag.SmartField != null)
            {
                CSmartField smartField = tag.SmartField.GetObjetInContexte(CSc2iWin32DataClient.ContexteCourant) as CSmartField;
                smartField.BeginEdit();
                if (smartField != null)
                    CFormEditeSmartField.EditeSmartFieldAndCommit(smartField);
            }
        }

        //-----------------------------------------------------------------------
        void CFormEditeSmartField_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_txtNomChamp.Text = m_smartField.Libelle;
            if (m_smartField.Definition != null)
                m_lblOriginal.Text = m_smartField.Definition.Nom;
            else
                m_lblOriginal.Text = "";
            m_txtCategorie.Text = m_smartField.Categorie;

        }

        //-----------------------------------------------------------------------
        public static bool EditeSmartFieldAndCommit(CSmartField field)
        {
            CFormEditeSmartField form = new CFormEditeSmartField();
            form.m_smartField = field;
            bool bResult = form.ShowDialog() == DialogResult.OK ;
            form.Dispose();
            return bResult;
        }

        //--------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            m_smartField.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_smartField.Libelle = m_txtNomChamp.Text;
            m_smartField.Categorie = m_txtCategorie.Text;
            CResultAErreur result = m_smartField.CommitEdit();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
