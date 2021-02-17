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
	[EditeurTableExport(typeof(C2iTableExportTableauCroise))]
	public partial class CPanelEditTableExportTableauCroise : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExportTableauCroise m_tableCroisee = null;
		private C2iStructureExport m_structureExport = null;
		private ITableExport m_tableParente = null;
		private IElementAVariablesDynamiques m_elementAVariablesPourFiltre = null;

		//------------------------------------------
		public CPanelEditTableExportTableauCroise()
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
			m_tableCroisee = table as C2iTableExportTableauCroise;
			m_tableParente = tableParente;
			m_structureExport = structure;
			m_elementAVariablesPourFiltre = eltAVariablesPourFiltre;
			if (m_tableCroisee == null)
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
				return m_tableCroisee;
			}
		}

		
		//------------------------------------------
		private void InitChamps()
		{
			m_txtNomTable.Text = m_tableCroisee.NomTable;
			if (m_tableCroisee.ChampOrigine is CDefinitionProprieteDynamiqueThis)
				m_lblType.Text = DynamicClassAttribute.GetNomConvivial(m_tableCroisee.ChampOrigine.TypeDonnee.TypeDotNetNatif);
			else if (m_tableCroisee.ChampOrigine != null)
				m_lblType.Text = m_tableCroisee.ChampOrigine.Nom;
			else
				m_lblType.Text = "";
			m_chkSupprimerTablesTravail.Checked = m_tableCroisee.SupprimerTablesTravail;
			//Crée une table bidon avec tous les champs de la table fille de cette table
			DataTable tableBidon = new DataTable();
			if (m_tableCroisee.TablesFilles.Length != 0)
			{
				foreach (IChampDeTable champ in m_tableCroisee.TablesFilles[0].Champs)
				{
					Type tp = champ.TypeDonnee;
					if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
						tp = tp.GetGenericArguments()[0];
					DataColumn col = new DataColumn(champ.NomChamp, tp);
					tableBidon.Columns.Add(col);
				}
			}
			m_panelTableauCroise.InitChamps(tableBidon, m_tableCroisee.TableauCroise);
		
		}

		//------------------------------------------
		public CResultAErreur MajChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_tableCroisee == null )
			{
				result.EmpileErreur(I.T("Bad table format|20003"));
				return result;
			}
			m_tableCroisee.NomTable = m_txtNomTable.Text;
			m_tableCroisee.SupprimerTablesTravail = m_chkSupprimerTablesTravail.Checked;
           return result;
		}
		

		//------------------------------------------
		private void CPanelEditTableExportTableauCroise_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
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
	
	}
}
