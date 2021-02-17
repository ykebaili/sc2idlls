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
	[EditeurTableExport(typeof(C2iTableExportVue))]
	public partial class CPanelEditTableExportVue : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExportVue m_tableVue = null;
		private C2iStructureExport m_structureExport = null;
		private ITableExport m_tableParente = null;
		private IElementAVariablesDynamiques m_elementAVariablesPourFiltre = null;

		private CFournisseurPropDynForDataTable m_fournisseur = null;

		//------------------------------------------
		public CPanelEditTableExportVue()
		{
			InitializeComponent();
		}

		//------------------------------------------
		public CResultAErreur InitChamps(
			ITableExport table, 
			ITableExport tableParente, 
			C2iStructureExport structure,
			IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesPourFiltre)
		{
			CResultAErreur result = CResultAErreur.True;
			m_tableVue = table as C2iTableExportVue;
			m_tableParente = tableParente;
			m_structureExport = structure;
			m_elementAVariablesPourFiltre = eltAVariablesPourFiltre;
			if (m_tableVue == null)
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			InitChamps();

			//Crée une table bidon avec tous les champs de la table fille de cette table
			DataTable tableBidon = new DataTable();
			if (m_tableVue.TablesFilles.Length != 0)
			{
				foreach (IChampDeTable champ in m_tableVue.TablesFilles[0].Champs)
				{
					Type tp = champ.TypeDonnee;
					if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
						tp = tp.GetGenericArguments()[0];
					DataColumn col = new DataColumn(champ.NomChamp, tp);
					tableBidon.Columns.Add(col);
				}
			}
			m_fournisseur = new CFournisseurPropDynForDataTable(tableBidon);
			return result;
		}

		//------------------------------------------
		public ITableExport TableEditee
		{
			get
			{
				return m_tableVue;
			}
		}

		
		//------------------------------------------
		private void InitChamps()
		{
			m_txtNomTable.Text = m_tableVue.NomTable;

			//Indicateur de filtre
			m_imageFiltre.Image = m_imagesFiltre.Images[m_tableVue.FormuleSelection == null ? 1 : 0];

			m_wndListeChamps.Items.Clear();
			m_wndListeChamps.BeginUpdate();
			
			foreach (C2iChampExport champ in m_tableVue.Champs)
			{
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
			m_wndListeChamps.EndUpdate();

			m_imageFiltre.Visible = true;

			if (m_tableVue.ChampOrigine is CDefinitionProprieteDynamiqueThis)
				m_lblType.Text = DynamicClassAttribute.GetNomConvivial(m_tableVue.ChampOrigine.TypeDonnee.TypeDotNetNatif);
			else if (m_tableVue.ChampOrigine != null)
				m_lblType.Text = m_tableVue.ChampOrigine.Nom;
			else
				m_lblType.Text = "";
			m_chkSupprimerTablesTravail.Checked = m_tableVue.SupprimerTablesTravail;
		
		}

		//------------------------------------------
		public CResultAErreur MajChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_tableVue == null )
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			
			m_tableVue.NomTable = m_txtNomTable.Text;
            // Table normale
			m_tableVue.ClearChamps();
			foreach ( ListViewItem item in m_wndListeChamps.Items )
			{
				if ( item.Tag is C2iChampExport )
				{
					C2iChampExport champ = (C2iChampExport )item.Tag;
					m_tableVue.AddChamp ( champ );
				}
			}
			m_tableVue.SupprimerTablesTravail = m_chkSupprimerTablesTravail.Checked;
           return result;
		}
		


		//-------------------------------------------------------------------------
		private void FillItemForChamp(ListViewItem item, C2iChampExport champ)
		{
			item.Text = champ.NomChamp;
			item.ImageIndex = (champ.Origine is C2iOrigineChampExportExpression) ? 1 : 0;
			item.Tag = champ;
		}

		//------------------------------------------
		private void CPanelEditTableExportVue_Load(object sender, EventArgs e)
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

		//------------------------------------------
		private void m_wndListeChamps_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool bCanEdit = false;
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if (item != null)
				{

					if (item.Tag is C2iChampExport )
						bCanEdit = true;
				}
			}
			m_btnDetail.Visible = bCanEdit;
			m_btnSupprimer.Visible = bCanEdit;
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
			string strFormule = "";
			if (m_tableVue.FormuleSelection != null)
				strFormule = m_tableVue.FormuleSelection.GetString();
			C2iExpression formule = CFormStdEditeFormule.EditeFormule( strFormule, m_fournisseur, typeof(DataTable));
			if ( formule != null )
				m_tableVue.FormuleSelection = formule;
			
			InitChamps();
		}

		//---------------------------------------------------------------------------------------------
		private void NePasFiltrer()
		{
			if (m_tableVue != null )
			{
				
				if (m_tableVue.FormuleSelection != null)
				{
					if (CFormAlerte.Afficher(I.T("Cancel the filter ?|30006"),
						EFormAlerteType.Question) == DialogResult.Yes)
					{
						m_tableVue.FormuleSelection = null;
						InitChamps();
					}
				}
			}
		}

		//-----------------------------------------------------------------
		private void m_btnAjouter_LinkClicked(object sender, EventArgs e)
		{
			m_menuAjouterChamp.Show(m_btnAjouter, new Point(0, m_btnAjouter.Height));
		}

		//-----------------------------------------------------------------
		private void m_btnDetail_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if (item.Tag is C2iChampExport)
				{
					if (EditChamp((C2iChampExport)item.Tag))
						FillItemForChamp(item, ((C2iChampExport)item.Tag));
				}
			}
		}

		//-----------------------------------------------------------------
		private void m_btnSupprimer_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeChamps.SelectedItems.Count == 1)
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if (CFormAlerte.Afficher(I.T("Remove the field ?|30007"),
					EFormAlerteType.Question) == DialogResult.Yes)
					m_wndListeChamps.Items.Remove(item);
			}
		}

		
		//-------------------------------------------------------------------------
		private bool EditChamp(C2iChampExport champ)
		{
			if (champ.Origine is C2iOrigineChampExportExpression )
			{
				if (champ.Origine is C2iOrigineChampExportExpression)
					return CFormEditChampCalcule.EditeChamp(champ, typeof(DataTable), m_fournisseur);
			}
			return false;
		}

		//----------------------------------------------
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

		//----------------------------------------------
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

		private void m_menuAjouterChampDonnee_Click(object sender, EventArgs e)
		{
			Type tp = m_structureExport.TypeSource;
			if (m_tableVue == null )
				return;
			if (m_tableVue.ChampOrigine != null)
				tp = m_tableVue.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes(
				tp, 
				CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent,
				m_tableVue.ChampOrigine,
				m_fournisseur);
			foreach (CDefinitionProprieteDynamique def in defs)
			{
				C2iChampExport champ = new C2iChampExport();
				champ.Origine = new C2iOrigineChampExportChamp(def);
				champ.NomChamp = def.Nom;
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
		}

		private void m_menuAjouterChampCalcule_Click(object sender, EventArgs e)
		{
			C2iChampExport champ = new C2iChampExport();
			champ.Origine = new C2iOrigineChampExportExpression();
			champ.NomChamp = "Champ " + m_wndListeChamps.Items.Count;
			if (EditChamp(champ))
			{
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
		}
		
	}
}
