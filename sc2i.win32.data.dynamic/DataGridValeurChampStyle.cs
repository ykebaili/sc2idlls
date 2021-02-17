using System;

using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Runtime.Remoting.Lifetime;

using sc2i.data.dynamic;
using sc2i.data;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de DataGridValeurChampStyle.
	/// </summary>
	public class DataGridValeurChampStyle : DataGridColumnStyle
	{
		//Controles possibles selon le type de données
		ComboBox		m_combobox= new ComboBox();
		TextBox			m_textbox = new TextBox();
		DateTimePicker  m_datetimePicker = new DateTimePicker();
		C2iTextBoxNumerique	m_textBoxNumeric = new C2iTextBoxNumerique();
		CheckBox		m_checkBox = new CheckBox();

		Control		m_control = null;
		CChampCustom[]	m_listeChamps;
		bool[]		m_bIsErreur;

		private void InitControls()
		{
			m_textbox.AutoSize = false;
			m_control = m_textbox;
			m_textBoxNumeric.NullAutorise = true;
		}

		public DataGridValeurChampStyle(CChampCustom[] champs)
		{
			InitControls();
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


		protected override void ConcedeFocus()
		{
			if ( m_control != null )
			{
				m_control.Visible = true;
				m_control.Focus();
			}
			base.ConcedeFocus();
		}

		protected override void Abort(
			int rowNum
			)
		{
		}

		protected override  bool Commit(
			CurrencyManager dataSource,
			int rowNum
			)
		{
			if ( m_control!=null && m_control.Visible )
			{
				object objVal = null;
				CChampCustom champ = ((CChampCustom)m_listeChamps[rowNum]);
				if (champ.ListeValeurs.Count > 0)
					objVal = champ.ValueFromDisplay(m_combobox.Text);
				else
					objVal = GetValeurControle(m_control);;
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
				SetColumnValueAtRow(dataSource, rowNum, objVal);
			}
			m_control.Visible = false;
			return true;
		}

		protected override void Edit (  
			CurrencyManager cur, 
			int nLigne, 
			Rectangle bounds,
			bool bReadOnly,
			string strValue,
			bool bCellIsVisible)
		{
			if ( m_control != null )
				m_control.Visible = false;
			if ( strValue == null )
				strValue = GetColumnValueAtRow(cur, nLigne).ToString();
			CChampCustom champ = ((CChampCustom)m_listeChamps[nLigne]);
			if (champ.ListeValeurs.Count > 0)
				strValue = champ.DisplayFromValue ( strValue );

			if (champ.ListeValeurs.Count == 0)
			{
				if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tString)
				{
					//Champ libre 
					m_control = m_textbox;
					m_textbox.MaxLength = 1024;
				}
				else if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tDate)
				{
					m_control = m_datetimePicker;
				}
				else if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tEntier)
				{
					m_control = m_textBoxNumeric;
					m_textBoxNumeric.DecimalAutorise = false;;
				}
				else if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tDouble)
				{
					m_control = m_textBoxNumeric;
					m_textBoxNumeric.DecimalAutorise = false;
				}

				else if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tBool)
				{
					m_control = m_checkBox;
					m_checkBox.Text = champ.Nom;
				}
			}
			else
			{
				m_control = m_combobox;
				m_combobox.Items.Clear();
				foreach ( CValeurChampCustom valeur in ((CChampCustom)m_listeChamps[nLigne]).ListeValeurs )
					m_combobox.Items.Add ( valeur.Display );
			}

			if ( m_control == null )
				return;
			m_control.Name = m_control.GetType().ToString() + "_ChpCustom";
			
			m_control.Parent = this.DataGridTableStyle.DataGrid;
			m_control.Bounds = bounds;

			SetValeurControl ( m_control, strValue );
			if (bCellIsVisible && !this.DataGridTableStyle.DataGrid.ReadOnly)
			{
				m_control.BringToFront();
				m_control.Show();
				m_control.Focus();
			}
			else
				m_control.Hide();

			ColumnStartedEditing(m_control);
		}

		public void SetValeurControl ( Control ctrl, string strValue )
		{
			if ( ctrl is CheckBox )
			{
				ctrl.Text = "";
				((CheckBox)ctrl).Checked = strValue.ToLower()=="true";
			}
			else if ( ctrl is C2iTextBoxNumerique )
				{
					if(  strValue == "" )
						((C2iTextBoxNumerique)ctrl).DoubleValue = null;
					else
						ctrl.Text = strValue;
				}
			else
				ctrl.Text = strValue;
		}

		public object GetValeurControle ( Control ctrl )
		{
			if ( ctrl is CheckBox )
				return ((CheckBox)ctrl).Checked;
			else if ( ctrl is DateTimePicker )
				return ((DateTimePicker)ctrl).Value;
			else if ( ctrl is C2iTextBoxNumerique )
			{ 
				if ( !((C2iTextBoxNumerique)ctrl).IsSet() )
					return null;
				else
					return ((C2iTextBoxNumerique)ctrl).DoubleValue;
			}
			else return ctrl.Text;
		}

		protected override int GetMinimumHeight()
		{
			return 16;
		}

		protected override int GetPreferredHeight(
			Graphics g,
			object value
			)
		{
			return 20;
		}

		protected override Size GetPreferredSize(
			Graphics g,
			object value
			)
		{
			
			return new Size ( 30, 20 );
		}

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
			CChampCustom champ = ((CChampCustom)m_listeChamps[rowNum]);
			String strValeur = GetColumnValueAtRow ( source, rowNum ).ToString();
			if (champ.ListeValeurs.Count > 0)
				strValeur = champ.DisplayFromValue ( strValeur );
			if ( champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tBool )
				strValeur = strValeur.ToLower()=="true"?"Oui":"Non";
			SolidBrush brush;
			if ( m_bIsErreur[rowNum] )
				brush = new SolidBrush ( Color.LightPink );
			else
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

	}
}
