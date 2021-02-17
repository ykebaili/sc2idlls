using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.win32.common.customizableList;
using sc2i.win32.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.common;

namespace sc2i.win32.data.dynamic.import
{
    public partial class CControleViewSmartImportResultField : CCustomizableListControl
    {
        //--------------------------------------------------------------------
        public CControleViewSmartImportResultField()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        public override bool AspectDifferentEnInactif
        {
            get
            {
                return false;
            }
        }

        //--------------------------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = base.MyInitChamps(item);
            if ( !result )
                return result;
            CViewSmartImportResultItem si = item as CViewSmartImportResultItem;
            if (si != null)
            {
                m_lblChamp.BackColor = si.BackColor;
                m_lblMarge.BackColor = si.BackColor;
                if (si.Niveau > 0)
                {
                    m_lblMarge.Width = si.Niveau * 10;
                    m_lblMarge.Visible = true;
                }
                else
                    m_lblMarge.Visible = false;

                if (si.Propriete != null)
                    m_lblChamp.Text = si.Propriete.Nom;
                else
                    m_lblChamp.Text = "";

                BackColor = Color.White;
                ColorInactive = Parent.BackColor;

                object valeur = si.Valeur;
                m_imageEtat.Visible = false;
                ShowValeur(valeur, m_lblValeur);
                //ShowValeur(si.ValeurOriginale, m_lblValeurOriginale);
                m_lblValeurOriginale.Visible = false;

                if (valeur is IEnumerable<CValeursProprietes>)
                {
                    m_btnExpand.Visible = true;

                }
                else if (valeur is CValeursProprietes)
                {
                    m_btnExpand.Visible = true;
                    CValeursProprietes v = valeur as CValeursProprietes;
                    if (v.ObjetAssocie != null)
                    {
                        if (v.ObjetAssocie.Row.RowState == DataRowState.Added)
                        {
                            m_imageEtat.Visible = true;
                            m_imageEtat.Image = Properties.Resources._1402941834_Create;
                        }
                        if (v.ObjetAssocie.Row.RowState == DataRowState.Modified)
                        {
                            m_imageEtat.Visible = true;
                            m_imageEtat.Image = Properties.Resources._1402941874_Modify;
                        }
                    }
                }
                else
                {
                    m_btnExpand.Visible = false;
                }

                m_btnExpand.Text = si.IsCollapse ? "+" : "-";
            }
            else
                m_btnExpand.Visible = false;
            return result;
        }

        private void ShowValeur(object valeur, Label lbl)
        {
            if (valeur is IEnumerable<CValeursProprietes>)
            {
                lbl.Text = ((IEnumerable<CValeursProprietes>)valeur).Count().ToString();
                m_btnExpand.Visible = true;

            }
            else if (valeur is CValeursProprietes)
            {
                lbl.Text = ((CValeursProprietes)valeur).LibelleObjet;
                m_btnExpand.Visible = true;
                CValeursProprietes v = valeur as CValeursProprietes;
                if (v.ObjetAssocie != null)
                {
                    if (v.ObjetAssocie.Row.RowState == DataRowState.Added)
                    {
                        m_imageEtat.Visible = true;
                        m_imageEtat.Image = Properties.Resources._1402941834_Create;
                    }
                    if (v.ObjetAssocie.Row.RowState == DataRowState.Modified)
                    {
                        m_imageEtat.Visible = true;
                        m_imageEtat.Image = Properties.Resources._1402941874_Modify;
                    }
                }
            }
            else
            {
                if (valeur == null)
                    lbl.Text = "null";
                else
                    lbl.Text = valeur.ToString();
                m_btnExpand.Visible = false;
            }
        }

        //--------------------------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result = base.MyMajChamps();
            
            return result;
        }

        //--------------------------------------------------------------------
        public override bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        //--------------------------------------------------------------------
        private void m_btnExpand_Click(object sender, EventArgs e)
        {
            CViewSmartImportResultItem item = CurrentItem as CViewSmartImportResultItem;
            if ( item != null && item.IsCollapse )
                ((CControleViewSmartImportResult)AssociatedListControl).Expand(CurrentItem);
            else if (item != null && !item.IsCollapse)
                ((CControleViewSmartImportResult)AssociatedListControl).Collapse(CurrentItem);
            m_btnExpand.Text = item.IsCollapse ? "+" : "-";
        }

       


    }
}
