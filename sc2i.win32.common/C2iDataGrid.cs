using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iDataGrid.
	/// </summary>
	public class C2iDataGrid : DataGrid, IControlALockEdition
	{
		private DataGridTableStyle m_tableStyle = null;

		private bool m_bLockEdition = true;
		//----------------------------------------------------------------------------------
		public C2iDataGrid(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			this.PreferredRowHeight = 20;
			container.Add(this);
			InitializeComponent();
		}
		//----------------------------------------------------------------------------------
		public C2iDataGrid()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			this.PreferredRowHeight = 24;
			InitializeComponent();
		}
		//------------------------------------------------------------------
		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// C2iDataGrid
			// 
			this.DataSourceChanged += new System.EventHandler(this.C2iDataGrid_DataSourceChanged);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion
		
		public event EventHandler OnChangeLockEdition;
		//------------------------------------------------------------------
        public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				if (DataSource!=null)
				{
					this.ReadOnly = value;
				}
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		//------------------------------------------------------------------
		public override void Refresh()
		{
			try
			{
				foreach(Control ctrl in this.Controls)
				{
					ctrl.Hide();
				}	
				base.Refresh();
			}
			catch
			{
			}
		}

		//------------------------------------------------------------------
		private void C2iDataGrid_DataSourceChanged(object sender, System.EventArgs e)
		{
			if (DataSource == null)
				return;
			this.ReadOnly = m_bLockEdition;
			Form frm = FindForm();
			if (frm==null)
				return;
			CurrencyManager manager = (CurrencyManager) frm.BindingContext[DataSource];		
			if ( manager != null )
				manager.Refresh(); 
			
		}

		//------------------------------------------------------------------
		public override bool PreProcessMessage(ref Message msg)
		{

			return base.PreProcessMessage (ref msg);
		}


		//------------------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public DataGridTableStyle TableStyle
		{
			get
			{
				if (DataSource == null)
					return null;
				Form frm = FindForm();
				if (frm==null)
					return null;
				CurrencyManager manager = (CurrencyManager) frm.BindingContext[DataSource];
				if (m_tableStyle == null)
				{
					m_tableStyle = new DataGridTableStyle(manager);
					TableStyles.Add(m_tableStyle);
					m_tableStyle.GridColumnStyles.Clear();
				}
				return m_tableStyle;
			}
		}
		//------------------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public object CurrentElement
		{
			get
			{
				if ( DataSource == null )
					return null;
				if ( BindingContext[DataSource] == null)
					return null;
				try
				{
					return BindingContext[DataSource].Current;
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (DataSource == null)
					return;
				if ( BindingContext[DataSource] == null)
					return;
				CurrencyManager manager = (CurrencyManager) BindingContext[DataSource];
				if ( ((IList)DataSource).Contains(value) )
				{
                    manager.Position = ((IList)DataSource).IndexOf(value);
					this.CurrentRowIndex = manager.Position;
				}
			}
		}		
	}
}
