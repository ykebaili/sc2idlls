using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.win32.data.navigation;
using sc2i.data.dynamic;
using System.Collections;

namespace sc2i.win32.process.workflow.bloc
{
    public partial class CPanelEditeRestrictionsBlocWorkflowFormulaire : UserControl, IControlALockEdition
    {
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = new CListeRestrictionsUtilisateurSurType();
        private CRestrictionUtilisateurSurType m_restrictionAffichee = null;

        public CPanelEditeRestrictionsBlocWorkflowFormulaire()
        {
            InitializeComponent();
        }

        public void Init(CListeRestrictionsUtilisateurSurType liste)
        {
            m_listeRestrictions = liste;
            CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass(typeof(TableAttribute));
            m_cmbType.Init ( classes );
            FillListeTypes();
            m_restrictionAffichee = null;
            m_panelDetailType.Visible = false;
        }

        //---------------------------------------------------------------
        public CListeRestrictionsUtilisateurSurType GetListeRestrictions()
        {
            ValideRestrictionEnCours();
            m_listeRestrictions = new CListeRestrictionsUtilisateurSurType();
            //m_listeRestrictions.SeuilAnnulationPriorites = m_txtSeuil.IntValue;
            foreach (ListViewItem item in m_wndListeTypes.Items)
            {
                CRestrictionUtilisateurSurType rest = item.Tag as CRestrictionUtilisateurSurType;
                if (rest != null)
                    m_listeRestrictions.AddRestriction(rest);
            }
            return m_listeRestrictions;
        }

        //---------------------------------------------------------------
        private void FillListeTypes()
        {
            m_wndListeTypes.BeginUpdate();
            m_wndListeTypes.Items.Clear();
            foreach (CRestrictionUtilisateurSurType rest in m_listeRestrictions.ToutesLesRestrictionsAffectees)
            {
                ListViewItem item = new ListViewItem();
                InitItem(item, rest);
                m_wndListeTypes.Items.Add(item);
            }
            m_wndListeTypes.EndUpdate();
        }

        //---------------------------------------------------------------
        private void InitItem(ListViewItem item, CRestrictionUtilisateurSurType rest)
        {

            item.Text = DynamicClassAttribute.GetNomConvivial(rest.TypeAssocie);
            item.Tag = rest;
        }

        //-------------------------------------------------------------------------
        private ListViewItem GetItemForType(Type tp)
        {
            foreach (ListViewItem item in m_wndListeTypes.Items)
            {
                CRestrictionUtilisateurSurType rest = item.Tag as CRestrictionUtilisateurSurType;
                if (rest != null && rest.TypeAssocie == tp)
                    return item;
            }
            return null;
        }


        //-------------------------------------------------------------------------
        private void ValideRestrictionEnCours()
        {
            if (m_restrictionAffichee == null)
                return;
            foreach (ListViewItem item in m_wndListeChamps.Items)
            {
                string strProp = (string)item.Tag;
                ERestriction rest = ERestriction.Aucune;
                if (item.ImageIndex == 1)
                    rest = ERestriction.ReadOnly;
                if (item.ImageIndex == 2)
                    rest = ERestriction.Hide;
                m_restrictionAffichee.SetRestrictionLocale(strProp, rest);
            }
            //m_restrictionAffichee.Priorite = m_txtSeuil.IntValue != null?m_txtSeuil.IntValue.Value:0;
            ListViewItem itemToModify = GetItemForType(m_restrictionAffichee.TypeAssocie);
            if (itemToModify != null)
            {
                InitItem(itemToModify, m_restrictionAffichee);
            }
       }

        //-------------------------------------------------------------------------
        private int GetIndexImage(ERestriction rest)
        {
            if ((rest & ERestriction.Hide) == ERestriction.Hide)
                return 2;
            if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                return 1;
            return 0;
        }

        //-------------------------------------------------------------------------
        private Image GetImage(ERestriction rest)
        {
            return m_imagesDroits.Images[GetIndexImage(rest)];
        }

        //-------------------------------------------------------------------------
        private string GetLibelleRestriction(ERestriction rest)
        {
            if ((rest & ERestriction.Hide) == ERestriction.Hide)
                return I.T("Mask|20101");
            if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                return I.T("Read only|20102");
            string strLibelle = I.T("Read/Write|20103");
            if ((rest & ERestriction.NoCreate) != ERestriction.NoCreate)
                strLibelle += I.T("Create|20104");
            if ((rest & ERestriction.NoDelete) != ERestriction.NoDelete)
                strLibelle += I.T("Delete|20105");
            return strLibelle;
        }

        //---------------------------------------------------------------
        private void ShowRestriction(CRestrictionUtilisateurSurType restriction)
        {
            ValideRestrictionEnCours();
            m_restrictionAffichee = restriction;
            if (restriction == null)
            {
                m_panelDetailType.Visible = false;
                return;
            }
            if (m_restrictionAffichee == null)
                return;

            //m_txtSeuil.IntValue = m_restrictionAffichee.Priorite;
            m_imageRestrictionGlobale.Image = GetImage(m_restrictionAffichee.RestrictionUtilisateur);
            m_lblRestrictionGlobale.Text = GetLibelleRestriction(m_restrictionAffichee.RestrictionUtilisateur);

            if ((m_restrictionAffichee.RestrictionUtilisateur & ERestriction.NoCreate) == ERestriction.NoCreate)
                m_imageNoAdd.Image = m_imagesDroits.Images[4];
            else
                m_imageNoAdd.Image = m_imagesDroits.Images[3];

            if ((m_restrictionAffichee.RestrictionUtilisateur & ERestriction.NoDelete) == ERestriction.NoDelete)
                m_imageNoDelete.Image = m_imagesDroits.Images[6];
            else
                m_imageNoDelete.Image = m_imagesDroits.Images[5];
            FillListeChamps(restriction);
            m_panelDetailType.Visible = true;
        }

        
        //-------------------------------------------------------------------------
		private void FillListeChamps ( CRestrictionUtilisateurSurType restriction )
		{
            m_wndListeChamps.BeginUpdate();
			m_wndListeChamps.Items.Clear();
			CInfoStructureDynamique info =  CInfoStructureDynamique.GetStructure ( restriction.TypeAssocie, 0 );
            List<ListViewItem> lstItems = new List<ListViewItem>();
			foreach ( CInfoChampDynamique champ in info.Champs )
			{
                string strTmp = "";
                CDefinitionProprieteDynamique def = CConvertisseurInfoStructureDynamiqueToDefinitionChamp.GetDefinitionProprieteDynamique(champ.NomPropriete, ref strTmp);
                //Uniquement les propriétés "classiques"
                //voir CTimosApp.GetStructure
                if (def != null && typeof(CDefinitionProprieteDynamiqueDotNet).IsAssignableFrom(def.GetType() ))
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = champ.NomChamp;
                    item.Tag = def.NomProprieteSansCleTypeChamp;
                    item.ImageIndex = GetIndexImage(restriction.GetRestriction(def.NomProprieteSansCleTypeChamp));
                    lstItems.Add(item);
                }
			}
            lstItems.Sort((x, y) => x.Text.CompareTo(y.Text));
			if (typeof(IElementAChamps).IsAssignableFrom(restriction.TypeAssocie))
			{
				CRoleChampCustom role = CRoleChampCustom.GetRoleForType(restriction.TypeAssocie);
				if (role != null)
				{
                    CListeObjetsDonnees listeChampsCustom = CChampCustom.GetListeChampsForRole(CContexteDonneeSysteme.GetInstance(), role.CodeRole);
					foreach (CChampCustom champ in listeChampsCustom)
					{
						ListViewItem item = new ListViewItem();
						item.Text = champ.Nom;
						item.Tag = champ.CleRestriction;
						item.ImageIndex = GetIndexImage(restriction.GetRestriction(champ.CleRestriction));
                        lstItems.Add(item);
					}
                    lstItems.Sort((x, y) => x.Text.CompareTo(y.Text));

					CListeObjetsDonnees listeFormulaires = new CListeObjetsDonnees(CContexteDonneeSysteme.GetInstance(), typeof(CFormulaire));
                    listeFormulaires.Filtre = CFormulaire.GetFiltreFormulairesForRole(role.CodeRole);
					foreach (CFormulaire formulaire in listeFormulaires)
					{
						ListViewItem item = new ListViewItem();
						item.Text = "{" + formulaire.Libelle + "}";
						item.Tag = formulaire.CleRestriction;
						item.ImageIndex = GetIndexImage(restriction.GetRestriction(formulaire.CleRestriction));
                        lstItems.Add(item);
					}
				}
			}
            m_wndListeChamps.Items.AddRange(lstItems.ToArray() );
            m_wndListeChamps.EndUpdate();
		}

        //------------------------------------------------------------------------
        private void m_wndListeChamps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && m_extModeEdition.ModeEdition)
            {
                foreach (ListViewItem item in m_wndListeChamps.SelectedItems)
                {
                    string strTag = item.Tag.ToString();
                    item.ImageIndex = (item.ImageIndex + 1) % 3;
                }
            }
        }

        //----------------------------------------------------------------------------
        private void m_wndListeChamps_Click(object sender, EventArgs e)
        {
            Point pt = m_wndListeChamps.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            if (pt.X < 20 && pt.X > 0)
            {
                ListViewItem item = m_wndListeChamps.GetItemAt(pt.X, pt.Y);
                if (item != null)
                    item.ImageIndex = (item.ImageIndex + 1) % 3;
                if (m_wndListeChamps.SelectedItems.Count > 1)
                {
                    foreach (ListViewItem itemSel in m_wndListeChamps.SelectedItems)
                        itemSel.ImageIndex = item.ImageIndex;
                }
            }
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        private void m_wndAddType_LinkClicked(object sender, EventArgs e)
        {
            Type tp = m_cmbType.TypeSelectionne;
            if ( GetItemForType ( tp ) == null )
            {
                ListViewItem item = new ListViewItem();
                InitItem ( item, new CRestrictionUtilisateurSurType(tp));
                m_wndListeTypes.Items.Add ( item );
                item.Selected = true;
            }
        }

        private void m_wndListeTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListeTypes.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeTypes.SelectedItems[0];
                if (item != null)
                {
                    CRestrictionUtilisateurSurType rest = item.Tag as CRestrictionUtilisateurSurType;
                    if (rest != null)
                        ShowRestriction(rest);
                }
            }
        }

        //-------------------------------------------------------------------------
        private void m_imageRestrictionGlobale_Click(object sender, System.EventArgs e)
        {
            RotateDroitRestrictionSelectionnee();
        }

        //-------------------------------------------------------------------------
        private void RotateDroitRestrictionSelectionnee()
        {
            if (m_restrictionAffichee == null)
                return;
            if (m_restrictionAffichee.CanModifyType())
                m_restrictionAffichee.RestrictionUtilisateur = ERestriction.ReadOnly;
            else if (m_restrictionAffichee.CanShowType())
                m_restrictionAffichee.RestrictionUtilisateur = ERestriction.Hide;
            else
                m_restrictionAffichee.RestrictionUtilisateur = ERestriction.Aucune;
            ShowRestriction(m_restrictionAffichee);
        }

        //-------------------------------------------------------------------------
        private void m_imageNoAdd_Click(object sender, System.EventArgs e)
        {
            InverseFlagRestriction(ERestriction.NoCreate);
        }

        //-------------------------------------------------------------------------
        private void InverseFlagRestriction(ERestriction restriction)
        {
            if (m_restrictionAffichee == null)
                return;
            if ((m_restrictionAffichee.RestrictionUtilisateur & restriction) == restriction)
                m_restrictionAffichee.RestrictionUtilisateur -= (int)restriction;
            else
                m_restrictionAffichee.RestrictionUtilisateur += (int)restriction;
            ShowRestriction(m_restrictionAffichee);
        }

        //-------------------------------------------------------------------------
        private void m_imageNoDelete_Click(object sender, System.EventArgs e)
        {
            InverseFlagRestriction(ERestriction.NoDelete);
        }

        private void m_wndSupprimer_LinkClicked(object sender, EventArgs e)
        {
            foreach (ListViewItem itemSel in new ArrayList(m_wndListeTypes.SelectedItems))
            {
                CRestrictionUtilisateurSurType restSel = itemSel.Tag as CRestrictionUtilisateurSurType;
                if (restSel != null)
                {
                    itemSel.Tag = null;
                    m_wndListeTypes.Items.Remove(itemSel);
                    ShowRestriction(null);
                }
            }
        }
    }
}
