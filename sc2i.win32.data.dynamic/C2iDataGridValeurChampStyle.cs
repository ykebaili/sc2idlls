using System;

using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Runtime.Remoting.Lifetime;

using sc2i.data.dynamic;
using sc2i.data;
using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iDataGridValeurChampStyle.
	/// </summary>
	public class C2iDataGridValeurChampStyle : DataGridColumnStyle
	{
		ComboBox		m_combobox= new ComboBox();
		TextBox			m_textbox = new TextBox();
		DateTimePicker  m_datetimePicker = new DateTimePicker();
		NumericUpDown	m_numericUpDown = new NumericUpDown();
		CheckBox		m_checkBox = new CheckBox();

		//Controle utilisé pour éditer
		Control		m_control = null;
		private bool m_bEnEdition = false;
		
		CChampCustom[]	m_listeChamps;
		bool[]		m_bIsErreur;

		public C2iDataGridValeurChampStyle(CChampCustom[] champs)
		{
			SetListeChamps ( champs );
		}

		public void SetListeChamps ( CChampCustom[] champs )
		{
			m_listeChamps = champs;
			m_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
			m_bIsErreur = new bool[m_listeChamps.Length];
			for ( int n = 0; n < m_listeChamps.Length; n++ )
				m_bIsErreur[n] = false;
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Annule les modifications apportées à la valeur de la source (cellule)
		/// </summary>
		protected override void Abort(
			int rowNum
			)
		{
			if ( m_control == null )
				return;
			m_control.Hide();
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Valide les modifications apportées à la valeur de la source
		/// </summary>
		protected override  bool Commit(
			CurrencyManager dataSource,
			int rowNum
			)
		{
			CChampCustom champ = ((CChampCustom)m_listeChamps[rowNum]);
			m_control = GetControl(champ.TypeDonneeChamp.TypeDonnee, champ.Precision, champ.Nom);

			if ( m_control == null || !m_bEnEdition )
				return true;
			if (this.DataGridTableStyle.DataGrid.ReadOnly)
			{
				m_control.Hide();
				return true;
			}
			if (this.DataGridTableStyle.DataGrid.DataSource == null)
				return true;

			object valeur = GetValeurControl( m_control, champ, rowNum );
			try
			{
				SetValeurColumn(dataSource, rowNum, valeur);
			}
			catch
			{
				Console.WriteLine("\n\n\n**************************\nOn ne devrait pas passer là : C2iDataGridValeurChampStyle : Erreur CommitEdit\n*****************************");
				if ( dataSource.Position != rowNum )
				{
					dataSource.Refresh();
					dataSource.Position = rowNum;
					try
					{
						SetValeurColumn(dataSource, rowNum, valeur);
					}
					catch 
					{
						throw new Exception("Y a plus rien a faire !!!");
					}
				}
			}
			m_control.Hide();
			m_bEnEdition = false;
			return true;
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Passe la cellule en édition grâce au contrôle correspondant
		/// </summary>
		protected override void Edit (  
			CurrencyManager cur, 
			int nLigne, 
			Rectangle bounds,
			bool bReadOnly,
			string strValue,
			bool bCellIsVisible)
		{
			CChampCustom champ = ((CChampCustom)m_listeChamps[nLigne]);
			if (champ.ListeValeurs.Count == 0)
				m_control = GetControl(champ.TypeDonneeChamp.TypeDonnee, champ.Precision, champ.Nom);
			else
			{
				m_control = m_combobox;
				m_combobox.Items.Clear();
				foreach ( CValeurChampCustom valeur in ((CChampCustom)m_listeChamps[nLigne]).ListeValeurs )
					m_combobox.Items.Add ( valeur.Display );
			}

			if ( m_control == null )
				return;
			if (this.DataGridTableStyle.DataGrid.ReadOnly)
			{
				m_control.Hide();
				return;
			}
			m_bEnEdition = true;

			if ( strValue == null )
				strValue = GetValeurColumn(cur, nLigne).ToString();
			if (champ.ListeValeurs.Count > 0)
				strValue = champ.DisplayFromValue ( strValue );
			
			m_control.Parent = this.DataGridTableStyle.DataGrid;
			m_control.Bounds = bounds;
			m_control.Show();
			SetValeurControl( m_control, strValue );
			m_control.Focus();

			//Définit le controle qui doit éditer la valeur de la cellule
			ColumnStartedEditing(m_control);
		}

		#region Préférences Height, Size
		protected override int GetMinimumHeight()
		{
			return 20;
		}

		protected override int GetPreferredHeight(
			Graphics g,
			object value
			)
		{
			return 24;
		}

		protected override Size GetPreferredSize(
			Graphics g,
			object value
			)
		{
			
			return new Size ( 30, 24 );
		}
		#endregion

		#region Méthodes Paint
		protected override void Paint(
			Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum			)
		{
			Paint ( g, bounds, source, rowNum, false );
		}

		protected override void Paint(
			Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum,
			bool alignToRight
			)
		{
			object obj = GetColumnValueAtRow ( source, rowNum );
			String strValeur;
			if (obj == null)
				strValeur = "";
			else
				strValeur = obj.ToString();
			SolidBrush brush;
			brush = new SolidBrush(this.DataGridTableStyle.DataGrid.BackColor);
			g.FillRectangle ( brush, bounds );
			brush.Dispose();
			Font laFont = new Font(this.DataGridTableStyle.DataGrid.Font, FontStyle.Regular);
			SizeF size = g.MeasureString ( strValeur, laFont );
			Rectangle rectToDraw = bounds;
			StringFormat sf = new StringFormat();
			brush = new SolidBrush(this.DataGridTableStyle.DataGrid.ForeColor);
			g.DrawString ( 
				strValeur, 
				laFont, 
				brush,
				bounds, sf); 
			laFont.Dispose();
			brush.Dispose();
			
		}
		#endregion

		//------------------------------------------------------------------------------
		private Control GetControl(TypeDonnee tp, int nPrecision, string strText)
		{
			Control tempControle = null;

			if (tp == TypeDonnee.tString)
			{
				//Champ libre 
				tempControle = m_textbox;
				m_textbox.MaxLength = 1024;
			}
			else if (tp == TypeDonnee.tDate)
			{
				tempControle = m_datetimePicker;
			}
			else if (tp == TypeDonnee.tEntier)
			{
				tempControle = m_numericUpDown;
				m_numericUpDown.Maximum = 1000000;
				m_numericUpDown.DecimalPlaces = 0;
			}
			else if (tp == TypeDonnee.tDouble)
			{
				tempControle = m_numericUpDown;
				m_numericUpDown.Maximum = 1000000;
				m_numericUpDown.DecimalPlaces = nPrecision;
			}
			else if (tp == TypeDonnee.tBool)
			{
				tempControle = m_checkBox;
				m_checkBox.Text = strText;
			}
			
			return tempControle;
		}

		private object GetValeurColumn(CurrencyManager cur, int nLigne)
		{
			return GetColumnValueAtRow(cur, nLigne);
		}

		private void SetValeurColumn(CurrencyManager dataSource, int rowNum, object valeur)
		{
			SetColumnValueAtRow(dataSource, rowNum, valeur);
		}

		private object GetValeurControl(Control ctrl, CChampCustom champ, int rowNum)
		{
			object objVal = null;

			if (champ.ListeValeurs.Count > 0)
				objVal = champ.ValueFromDisplay(m_combobox.Text);
			else
			{
				if ( ctrl is CheckBox )
					objVal = ((CheckBox)ctrl).Checked;
				else if ( ctrl is DateTimePicker )
					objVal = ((DateTimePicker)ctrl).Value;
				else if ( ctrl is NumericUpDown )
					objVal = ((NumericUpDown)ctrl).Value;
				else 
					objVal = ctrl.Text;
			}
			//Vérifie que la valeur est correcte
				
			if ( objVal == null )
				objVal = champ.ValeurParDefaut;
			CResultAErreur	 result = champ.IsCorrectValue ( objVal );
			if ( !result )
			{
				m_bIsErreur[rowNum] = true;
			}
			else
			{
				m_bIsErreur[rowNum] = false;
			}
			return objVal;
		}
	
		public void SetValeurControl ( Control ctrl, object valeur )
		{
			if ( ctrl is CheckBox )
			{
				ctrl.Text = "";
				((CheckBox)ctrl).Checked = valeur.ToString().ToLower()=="true";
			}
			else
			{
				ctrl.Text = valeur.ToString();
			}
		}

	}
}
