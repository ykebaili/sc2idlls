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
	/// Editeur pour les tables d'export calculées indépendantes
	/// </summary>
    [EditeurTableExport(typeof(C2iTableExportUnion))]
	public partial class CPanelEditTableUnion : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExportUnion m_tableUnion = null;

		//---------------------------------------------------------------------------------
		public CPanelEditTableUnion()
		{
			InitializeComponent();
		}


        //----------------------------------------------------------------------------------
        public ITableExport TableEditee
        {
            get { return m_tableUnion; }
        }

		//----------------------------------------------------------------------------------
		public CResultAErreur InitChamps(
            ITableExport table, 
            ITableExport tableParente,
            C2iStructureExport structure,
            IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesDynamiques)
		{
            CResultAErreur result = CResultAErreur.True;

            m_tableUnion = table as C2iTableExportUnion;

            if (m_tableUnion == null)
                return CResultAErreur.False;

			m_txtNomTable.Text = m_tableUnion.NomTable;
			if (m_tableUnion.ChampOrigine is CDefinitionProprieteDynamiqueThis)
				m_lblType.Text = DynamicClassAttribute.GetNomConvivial(m_tableUnion.ChampOrigine.TypeDonnee.TypeDotNetNatif);
			else if (m_tableUnion.ChampOrigine != null)
				m_lblType.Text = m_tableUnion.ChampOrigine.Nom;
			else
				m_lblType.Text = "";
			m_chkSupprimerTablesTravail.Checked = m_tableUnion.SupprimerTablesTravail;
				

			m_tableUnion.RecalcStructure();
			FillListeChamps();

            
            return result;
		}

		//-------------------------------------------------------------------------
		private void FillListeChamps()
		{
			m_wndListeChamps.Items.Clear();
			m_tableUnion.RecalcStructure();
			Dictionary<string,bool> champsCles = new Dictionary<string,bool>();
			foreach (string strChampCle in m_tableUnion.ChampsClesExplicites)
				champsCles[strChampCle] = true;
			foreach (IChampDeTable champ in m_tableUnion.Champs)
			{
				ListViewItem item = new ListViewItem(champ.NomChamp);
				item.Tag = champ;
				item.ImageIndex = champsCles.ContainsKey(champ.NomChamp) ? 1 : 0;
				m_wndListeChamps.Items.Add(item);
			}
		}

        //-------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            m_tableUnion.NomTable = m_txtNomTable.Text;
			m_tableUnion.SupprimerTablesTravail = m_chkSupprimerTablesTravail.Checked;
			List<string> champsCles = new List<string>();
			foreach (ListViewItem item in m_wndListeChamps.Items)
			{
				if (item.ImageIndex == 1)
					champsCles.Add(((IChampDeTable)item.Tag).NomChamp);
			}
			m_tableUnion.ChampsClesExplicites = champsCles.ToArray();
            return result;
        }

        


		//---------------------------------------------------------------------------
		private void CPanelEditTableUnion_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
		}

        #region IControlALockEdition Membres

        //-------------------------------------------------------------------------------
        public bool LockEdition
        {
			get
            {
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

		//---------------------------------------------------------------------------
		private void m_wndListeChamps_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ListViewHitTestInfo info = m_wndListeChamps.HitTest(new Point(e.X, e.Y));
				if (info != null && info.Item != null)
				{
					if (info.Item.ImageIndex == 0)
						info.Item.ImageIndex = 1;
					else
						info.Item.ImageIndex = 0;
				}
			}
		}

 
    }
}
