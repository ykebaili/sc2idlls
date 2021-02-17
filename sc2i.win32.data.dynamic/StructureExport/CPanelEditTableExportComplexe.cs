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
	[EditeurTableExport(typeof(C2iTableExport))]
	public partial class CPanelEditTableExportComplexe : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExport m_tableExport = null;
		private C2iStructureExport m_structureExport = null;
		private ITableExport m_tableParente = null;
		private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesPourFiltre = null;

		//------------------------------------------
		public CPanelEditTableExportComplexe()
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
			m_tableExport = table as C2iTableExport;
			m_tableParente = tableParente;
			m_structureExport = structure;
			m_elementAVariablesPourFiltre = eltAVariablesPourFiltre;
			if (m_tableExport == null)
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			InitChamps();
			return result;
		}

		//------------------------------------------
		public ITableExport TableEditee
		{
			get
			{
				return m_tableExport;
			}
		}

		
		//------------------------------------------
		private void InitChamps()
		{
			m_txtNomTable.Text = m_tableExport.NomTable;

			//Indicateur de filtre
			m_imageFiltre.Image = m_imagesFiltre.Images[m_tableExport.FiltreAAppliquer == null ? 1 : 0];

			m_wndListeChamps.Items.Clear();
			m_wndListeChamps.BeginUpdate();
			
			if (m_tableExport.ChampOrigine is CDefinitionProprieteDynamiqueFormule)
				m_imageFormuleTable.Visible = true;
			else
				m_imageFormuleTable.Visible = false;
			m_imageFiltre.Visible = m_imageFiltre.Visible && !m_imageFormuleTable.Visible;

			foreach (C2iChampExport champ in m_tableExport.Champs)
			{
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
			m_wndListeChamps.EndUpdate();

			//Peut-on appliquer un filtre sur cette table ?
			m_imageFiltre.Visible = false;
			if (m_structureExport.IsStructureComplexe && m_tableExport.ChampOrigine != null && 
                m_tableExport.ChampOrigine is CDefinitionProprieteDynamiqueDonneeCumulee ||
                m_tableExport.ChampOrigine is CDefinitionProprieteDynamiqueRelationTypeId)
				m_imageFiltre.Visible = true;
			if (!m_imageFiltre.Visible && m_tableParente != null)
			{
				Type typeParent = m_structureExport.TypeSource;
				if (m_tableParente != null && m_tableParente.ChampOrigine != null)
				{
					typeParent = m_tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				}
				string strProp = m_tableExport.ChampOrigine.NomPropriete;
				string[] strProps = strProp.Split('.');
				PropertyInfo info = null;
				string strTmp = "";
				foreach (string strSousProp in strProps)
				{
					string strSousPropDecomposee = strSousProp;
					CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strSousProp, ref strTmp, ref strSousPropDecomposee);
					info = typeParent.GetProperty(strSousPropDecomposee);
					if (info != null)
						typeParent = info.PropertyType;
					else
						break;
				}
				if (info != null)
					m_imageFiltre.Visible = typeof(CListeObjetsDonnees).IsAssignableFrom(info.PropertyType);
			}

            // Est-ce une structure simple ?
            if (!m_structureExport.IsStructureComplexe)
            {
                m_imageFiltre.Visible = false;
                Type tp = m_structureExport.TypeSource;
                if (m_tableExport.ChampOrigine != null)
                    tp = m_tableExport.ChampOrigine.TypeDonnee.TypeDotNetNatif;
                CStructureTable structure = CStructureTable.GetStructure(tp);
                m_wndListeChamps.BeginUpdate();
                foreach (CInfoChampTable info in structure.Champs)
                {
                    ListViewItem item = new ListViewItem(info.NomChamp);
                    item.Tag = info;
                    m_wndListeChamps.Items.Add(item);
                }
                m_wndListeChamps.EndUpdate();
            }

		}

		//------------------------------------------
		public CResultAErreur MajChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_tableExport == null )
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			/*if ( !m_gestionnaireModeEdition.ModeEdition )
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}*/
			m_tableExport.NomTable = m_txtNomTable.Text;
            // Table normale
			if ( m_tableExport is C2iTableExport )
			{
				C2iTableExport tableExport = (C2iTableExport)m_tableExport;
				tableExport.ClearChamps();
				foreach ( ListViewItem item in m_wndListeChamps.Items )
				{
					if ( item.Tag is C2iChampExport )
					{
						C2iChampExport champ = (C2iChampExport )item.Tag;
						tableExport.AddChamp ( champ );
					}
				}
			}
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
		private void CPanelEditTableExportComplexe_Load(object sender, EventArgs e)
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
            if (!m_structureExport.IsStructureComplexe)
			{
				bool bCanEdit = false;
				if (m_wndListeChamps.SelectedItems.Count == 1)
				{
					ListViewItem item = m_wndListeChamps.SelectedItems[0];
					if (item != null)
					{

						if (item.Tag is C2iChampExport &&
							((C2iChampExport)item.Tag).Origine is C2iOrigineChampExportChampCustom)
							bCanEdit = true;
					}
				}
				m_btnDetail.Visible = bCanEdit;
				m_btnSupprimer.Visible = bCanEdit;
			}
		}

		//---------------------------------------------------------------------------------------------
		private void m_imageFormuleTable_Click(object sender, EventArgs e)
		{
			if (m_tableExport == null || (!(m_tableExport is C2iTableExport)))
				return;
			if (!m_structureExport.IsStructureComplexe)
				return;

			Type tpParent = m_structureExport.TypeSource;

			if (m_tableParente == null && m_tableParente.ChampOrigine != null)
				tpParent = m_tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			try
			{
				
				if (!(m_tableExport.ChampOrigine is CDefinitionProprieteDynamiqueFormule))
					return;
				C2iExpression formule = ((CDefinitionProprieteDynamiqueFormule)m_tableExport.ChampOrigine).Formule;
				C2iExpression exTmp = CFormStdEditeFormule.EditeFormule(formule.GetString(), new CFournisseurPropDynStd(true), tpParent);
				if (exTmp != null)
				{
					if (!exTmp.TypeDonnee.Equals(formule.TypeDonnee))
					{
						CFormAlerte.Afficher(I.T("Impossible to modify the return value type of the formula|30005"), EFormAlerteType.Erreur);
						return;
					}
					((C2iTableExport)m_tableExport).ChampOrigine = new CDefinitionProprieteDynamiqueFormule(exTmp);
					InitChamps();
				}
			}
			catch
			{ }
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
			if (m_tableExport == null ||
				(!(m_tableExport is C2iTableExport) ))
			{
				return;
			}
			ITableExport tableExport = (ITableExport)m_tableExport;
			if (tableExport.ChampOrigine == null)
				return;
			CFiltreDynamique filtre = ((ITableExport)m_tableExport).FiltreAAppliquer;
			if (filtre == null)
			{
				filtre = new CFiltreDynamique();
				filtre.TypeElements = tableExport.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			}
			filtre.ElementAVariablesExterne = m_elementAVariablesPourFiltre;
			if (CFormEditFiltreDynamique.EditeFiltre(filtre, true, true, tableExport.ChampOrigine))
			{
				tableExport.FiltreAAppliquer = filtre;
				InitChamps();
			}
		}

		//---------------------------------------------------------------------------------------------
		private void NePasFiltrer()
		{
			if (m_tableExport != null )
			{
				if (m_tableExport.FiltreAAppliquer != null)
				{
					if (CFormAlerte.Afficher(I.T("Cancel the filter ?|30006"),
						EFormAlerteType.Question) == DialogResult.Yes)
					{
						((C2iTableExport)m_tableExport).FiltreAAppliquer = null;
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

		//-----------------------------------------------------------------
		private void m_menuAjouterChamp_Popup(object sender, EventArgs e)
		{
			if (m_structureExport.IsStructureComplexe)
				m_menuAjouterChampsPersonnalisés.Visible = false;
			else
			{
				m_menuAjouterChampsPersonnalisés.Visible = true;
				m_menuAjouterChampDonnee.Visible = false;
			}
		}

		//-------------------------------------------------------------------------
		private bool EditChamp(C2iChampExport champ)
		{
			if (champ.Origine is C2iOrigineChampExportExpression ||
				champ.Origine is C2iOrigineChampExportChampCustom)
			{
				Type tp = m_structureExport.TypeSource;
				if (m_tableExport is C2iTableExport)
				{
					C2iTableExport table = (C2iTableExport)m_tableExport;
					if (table.ChampOrigine != null)
						tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
					if (champ.Origine is C2iOrigineChampExportExpression)
						return CFormEditChampCalcule.EditeChamp(champ, tp, m_elementAVariablesPourFiltre != null ? (IFournisseurProprietesDynamiques)m_elementAVariablesPourFiltre : new CFournisseurPropDynStd(true));
					if (champ.Origine is C2iOrigineChampExportChampCustom)
						return CFormEditOrigineChampCustom.EditeChamp(champ, tp);
				}
			}
			return false;
		}

		//--------------------------------------------------------------------------
		private void m_menuAjouterChampDonnee_Click(object sender, EventArgs e)
		{
			Type tp = m_structureExport.TypeSource;
			if (m_tableExport == null || !(m_tableExport is C2iTableExport))
				return;
			C2iTableExport table = (C2iTableExport)m_tableExport;
			if (m_tableExport != null && table.ChampOrigine != null)
				tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes(tp, CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent, table.ChampOrigine);
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

		//--------------------------------------------------------------------------
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

		//--------------------------------------------------------------------------
		private void m_menuAjouterChampsPersonnalisés_Click(object sender, EventArgs e)
		{
			C2iChampExport champ = new C2iChampExport();
			champ.Origine = new C2iOrigineChampExportChampCustom();
			champ.NomChamp = "CUSTOM_FIELDS";
			if (EditChamp(champ))
			{
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
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
		
	}
}
