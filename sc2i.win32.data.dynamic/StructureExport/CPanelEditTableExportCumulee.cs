using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;
using System.Reflection;
using sc2i.data;
using sc2i.expression;
using sc2i.win32.expression; 

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Editeur pour les tables d'export complexes
	/// </summary>
	[EditeurTableExport(typeof(C2iTableExportCumulee))]
	public partial class CPanelEditTableExportCumulee : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExportCumulee m_tableExportCumulee = null;
		private C2iStructureExport m_structureExport = null;
		private ITableExport m_tableParente = null;
		private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesDynamiques = null;

		//------------------------------------------
		public CPanelEditTableExportCumulee()
		{
			InitializeComponent();
		}

        //------------------------------------------
        public ITableExport TableEditee
        {
            get { return m_tableExportCumulee; }
        }

		//------------------------------------------
		public CResultAErreur InitChamps(
			ITableExport table, 
			ITableExport tableParente, 
			C2iStructureExport structure,
			IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesDynamiques)
		{
			CResultAErreur result = CResultAErreur.True;
			m_tableExportCumulee = table as C2iTableExportCumulee;
			if (m_tableExportCumulee == null)
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			m_tableParente = tableParente;
			m_structureExport = structure;
            m_elementAVariablesDynamiques = eltAVariablesDynamiques;

            InitChamps();

            return result;
		}

        //----------------------------------------------------------------------------------
        private void InitChamps()
        {
            m_txtNomTable.Text = m_tableExportCumulee.NomTable;

            m_wndListeChamps.Items.Clear();
            m_wndListeChamps.LabelEdit = true;
            m_wndListeChamps.BeginUpdate();
            m_imageFiltre.Image = m_imagesFiltre.Images[m_tableExportCumulee.FiltreAAppliquer == null ? 1 : 0];
            foreach (C2iChampDeRequete champ in m_tableExportCumulee.Champs)
            {
                ListViewItem item = new ListViewItem();
                FillItemForChamp(item, champ);
                m_wndListeChamps.Items.Add(item);
            }
            m_wndListeChamps.EndUpdate();

            m_chkCroiser.Checked = m_tableExportCumulee.TableauCroise != null;
            m_lnkTableauCroise.Enabled = m_chkCroiser.Checked && m_gestionnaireModeEdition.ModeEdition;
        }

        //------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_tableExportCumulee == null)
            {
                result.EmpileErreur(I.T("Bad table format|20003"));
                return result;
            }

            m_tableExportCumulee.NomTable = m_txtNomTable.Text;
            // Table cumulée
            if (m_tableExportCumulee != null)
            {
                m_tableExportCumulee.Requete.ListeChamps.Clear();
                foreach (ListViewItem item in m_wndListeChamps.Items)
                {
                    C2iChampDeRequete champ = item.Tag as C2iChampDeRequete;
                    if(champ != null)
                        m_tableExportCumulee.Requete.ListeChamps.Add(champ);
                    
                }
                if (!m_chkCroiser.Checked)
                    m_tableExportCumulee.TableauCroise = null;
            }

            return result;
        }

        //-------------------------------------------------------------------------
        private void FillItemForChamp(ListViewItem item, C2iChampDeRequete champ)
        {
            item.Text = champ.NomChamp;
            item.ImageIndex = (champ.GroupBy ? 3 : 2);
            item.Tag = champ;
        }

		//------------------------------------------
		private void CPanelEditTableExportCumulee_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
		}

		//------------------------------------------
		private void m_wndListeChamps_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.Items[e.Item];
			if (e.Label == null || item.Tag == null || !(item.Tag is C2iChampExport))
			{
				e.CancelEdit = true;
				return;
			}
			((C2iChampExport)item.Tag).NomChamp = e.Label;
		}

		//------------------------------------------
		private void m_wndListeChamps_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.Items[e.Item];
			if (item.Tag == null || !(item.Tag is C2iChampExport))
				e.CancelEdit = true;
		}

		//------------------------------------------
		private ListViewItem m_lastItemTooltip = null;
		private void m_wndListeChamps_MouseMove(object sender, MouseEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.GetItemAt(e.X, e.Y);
			if (item != null && item != m_lastItemTooltip)
			{
				if (item.Tag is C2iChampExport)
				{
					C2iChampExport champ = (C2iChampExport)item.Tag;
					if (champ.Origine is C2iOrigineChampExportChamp)
					{
						m_tooltip.SetToolTip(m_wndListeChamps, ((C2iOrigineChampExportChamp)champ.Origine).ChampOrigine.Nom);
						return;
					}
					if (champ.Origine is C2iOrigineChampExportExpression)
					{
						C2iExpression formule = ((C2iOrigineChampExportExpression)champ.Origine).Expression;
						if (formule != null)
						{
							m_tooltip.SetToolTip(m_wndListeChamps, formule.GetString());
							return;
						}
					}
				}
				else if (item.Tag is CInfoChampTable)
				{
					m_tooltip.SetToolTip(m_wndListeChamps, ((CInfoChampTable)item.Tag).NomConvivial);
					return;
				}
				else if (item.Tag is C2iChampDeRequete)
				{
					C2iChampDeRequete champ = (C2iChampDeRequete)item.Tag;
					string strInfo = "Origin : " + champ.GetStringSql() + "\r\n" +
						"Operation : " + champ.OperationAgregation.ToString() + "\r\n" +
						"Group by : " + (champ.GroupBy ? "Yes" : "No");
					m_tooltip.SetToolTip(m_wndListeChamps, strInfo);
					return;
				}
				m_lastItemTooltip = item;
			}
			m_tooltip.SetToolTip(m_wndListeChamps, "");
		}
        


		//---------------------------------------------------------------------------------------------
		private void m_imageFiltre_MouseUp(object sender, MouseEventArgs e)
		{
			if ((e.Button & (MouseButtons.Left)) > 0)
				FiltrerTable();
			else if ((e.Button & (MouseButtons.Right)) > 0)
			{
				NePasFiltrer();
			}
		}


		//---------------------------------------------------------------------------------------------
		private void FiltrerTable()
		{
			if (m_tableExportCumulee == null)
			{
				return;
			}
            Type tp = m_structureExport.TypeSource;
            if (m_tableExportCumulee.ChampOrigine != null)
                tp = m_tableExportCumulee.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			CFiltreDynamique filtre = ((ITableExport)m_tableExportCumulee).FiltreAAppliquer;
			if (filtre == null)
			{
				filtre = new CFiltreDynamique();
				filtre.TypeElements = tp;
			}
			filtre.ElementAVariablesExterne = m_elementAVariablesDynamiques;
			if (CFormEditFiltreDynamique.EditeFiltre(filtre, true, true, m_tableExportCumulee.ChampOrigine))
			{
				m_tableExportCumulee.FiltreAAppliquer = filtre;
				InitChamps();
			}
		}

		//---------------------------------------------------------------------------------------------
		private void NePasFiltrer()
		{
			if (m_tableExportCumulee != null )
			{
				if (m_tableExportCumulee.FiltreAAppliquer != null)
				{
					if (CFormAlerte.Afficher(I.T("Remove Filter ?|30007"),
						EFormAlerteType.Question) == DialogResult.Yes)
					{
						m_tableExportCumulee.FiltreAAppliquer = null;
						InitChamps();
					}
				}
			}
		}

		//-----------------------------------------------------------------
		private void m_btnAjouter_LinkClicked(object sender, EventArgs e)
		{
            OnAddChampCumule();
		}

        //-------------------------------------------------------------------------
        private void OnAddChampCumule()
        {
            if (!(m_tableExportCumulee is C2iTableExportCumulee))
                return;

            Type tp = m_structureExport.TypeSource;
            C2iTableExportCumulee table = (C2iTableExportCumulee)m_tableExportCumulee;
            if (m_tableExportCumulee != null && table.ChampOrigine != null)
                tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
            CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes
                (
                tp,
                CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent |
                CFormSelectChampPourStructure.TypeSelectionAttendue.ChampFille |
                CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees |
                CFormSelectChampPourStructure.TypeSelectionAttendue.InclureChampsCustom,
                table.ChampOrigine);


            // Créé le nouveau champ de requete
            if (defs.Length > 0)
            {
                C2iChampDeRequete champUnique = new C2iChampDeRequete();
                CDefinitionProprieteDynamique def1 = defs[0];
                champUnique.NomChamp = def1.Nom;
                champUnique.TypeDonneeAvantAgregation = def1.TypeDonnee.TypeDotNetNatif;

                List<CSourceDeChampDeRequete> listeSources = new List<CSourceDeChampDeRequete>();
                foreach (CDefinitionProprieteDynamique def in defs)
                {
                    CSourceDeChampDeRequete source = new CSourceDeChampDeRequete(def.NomChampCompatibleCComposantFiltreChamp);
                    listeSources.Add(source);
                }
                champUnique.Sources = listeSources.ToArray();
                ListViewItem item = new ListViewItem();
                m_wndListeChamps.Items.Add(item);
                if (champUnique != null && EditChamp(champUnique))
                    FillItemForChamp(item, champUnique);

            }

        }

		//-----------------------------------------------------------------
		private void m_btnDetail_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
                if (item.Tag is C2iChampDeRequete)
                {
                    if (EditChamp((C2iChampDeRequete)item.Tag))
                        FillItemForChamp(item, ((C2iChampDeRequete)item.Tag));
                }
            }
		}

		//----------------------------------------------------------------------
		private void m_btnSupprimer_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if (CFormAlerte.Afficher(I.T("Remove selected field ?|241"),
					EFormAlerteType.Question) == DialogResult.Yes)
					m_wndListeChamps.Items.Remove(item);
			}
		}

        //-------------------------------------------------------------------------
        private bool EditChamp(C2iChampDeRequete champ)
        {
            Type tp = m_structureExport.TypeSource;
            if (m_tableExportCumulee.ChampOrigine != null)
                tp = m_tableExportCumulee.ChampOrigine.TypeDonnee.TypeDotNetNatif;
            return CFormEditChampDeRequete.EditeChamp(
                champ,
                tp,
                m_tableExportCumulee.ChampOrigine);
        }


		//-------------------------------------------------------------------
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
					OnChangeLockEdition(this, null);
			}
		}

		//----------------------------------------------
		public event EventHandler  OnChangeLockEdition;

		//----------------------------------------------
		private void m_btnHaut_Click(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				int nIndex = item.Index;
				if (nIndex > 0)
				{
					m_wndListeChamps.Items.RemoveAt(nIndex);
					m_wndListeChamps.Items.Insert(nIndex - 1, item);
				}
			}
		}

		//----------------------------------------------------------------------
		private void m_btnBas_Click(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				int nIndex = item.Index;
				if (nIndex < m_wndListeChamps.Items.Count - 1)
				{
					m_wndListeChamps.Items.RemoveAt(nIndex);
					if (nIndex + 1 >= m_wndListeChamps.Items.Count - 1)
						m_wndListeChamps.Items.Add(item);
					else
						m_wndListeChamps.Items.Insert(nIndex + 1, item);
				}
			}
		}

        //----------------------------------------------------------------------
        private void m_chkCroiser_CheckedChanged(object sender, EventArgs e)
        {
            m_lnkTableauCroise.Enabled = m_chkCroiser.Checked && m_gestionnaireModeEdition.ModeEdition;
        }

        //-------------------------------------------------------------------------------
        private void m_lnkTableauCroise_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (!(m_tableExportCumulee is C2iTableExportCumulee))
                return;
            MajChamps();
            C2iTableExportCumulee tableCumulee = (C2iTableExportCumulee)m_tableExportCumulee;
            CTableauCroise tableau = tableCumulee.TableauCroise;
            if (tableau == null)
            {
                tableau = new CTableauCroise();
            }
            DataTable table = new DataTable();
            tableCumulee.TableauCroise = null;
            m_tableExportCumulee.InsertColonnesInTable(table);
            tableCumulee.TableauCroise = tableau;
            CFormEditTableauCroise.EditeTableau(tableau, table);
        }
		
	}
}
