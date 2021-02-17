using System;

using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Runtime.Remoting.Lifetime;
using System.Reflection;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de DataGridValeurChampStyle.
	/// </summary>
	public delegate string GetStringElementDelegate ( object source, object valeur );
	public delegate Color ? GetColorElementDelegate ( int nRow );
	public class C2iDataGridColumnStyleAControle : DataGridColumnStyle
	{
		//Controle utilisé pour éditer
		Control		m_control = null;
		private string m_strProprieteControl = "";
		private bool m_bEnEdition = false;

		private string m_strFormat = "";
		private HorizontalAlignment m_textAlignement = HorizontalAlignment.Left;

		private Color m_backColor = Color.Transparent;
		private Color m_foreColor = Color.Transparent;
		private Font m_font = null;

		/// ///////////////////////////////////////////////////////
		public C2iDataGridColumnStyleAControle
			(
			Control controleEdition,
			string strProprieteDeControle
			)
		{
			m_strProprieteControl = strProprieteDeControle;
			m_control = controleEdition;
			if (controleEdition!=null)
				this.Width = controleEdition.Width;
			AutoFormatAndAlignFromControl();
			controleEdition.TabStop = false;
		}

		/// ///////////////////////////////////////////////////////
		public C2iDataGridColumnStyleAControle
			(
			Control controleEdition,
			string strProprieteDeControle,
			string strMappingName
			)
		{
			m_strProprieteControl = strProprieteDeControle;
			m_control = controleEdition;
			if (controleEdition!=null)
				this.Width = controleEdition.Width;
			MappingName = strMappingName;
			AutoFormatAndAlignFromControl();
			controleEdition.TabStop = false;
		}

		/// ///////////////////////////////////////////////////////
		private void AutoFormatAndAlignFromControl()
		{
			if ( m_control is NumericUpDown )
			{
				NumericUpDown upDwn = (NumericUpDown)m_control;
				string strFormat = "0";
				if (upDwn.DecimalPlaces > 0 )
					strFormat+= "."+"".PadLeft(upDwn.DecimalPlaces, '0');
				Format = strFormat;
				TextAlign = upDwn.TextAlign;
			}
		}
				

		/// ///////////////////////////////////////////////////////
		public string Format
		{
			get
			{
				return m_strFormat;
			}
			set
			{
				m_strFormat = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public HorizontalAlignment TextAlign
		{
			get
			{
				return m_textAlignement;
			}
			set
			{
				m_textAlignement = value;
			}
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
		public Color BackColor
		{
			get
			{
				return m_backColor;
			}
			set
			{
				m_backColor = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public Color ForeColor
		{
			get
			{
				return m_foreColor;
			}
			set
			{
				m_foreColor = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public Font Font
		{
			get
			{
				return m_font;
			}
			set
			{
				m_font = value;
			}
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
			if ( m_control == null || !m_bEnEdition )
				return true;
			if (this.DataGridTableStyle.DataGrid.ReadOnly)
			{
				m_control.Hide();
				return true;
			}
			if (this.DataGridTableStyle.DataGrid.DataSource == null)
				return true;

			object valeur = CInterpreteurTextePropriete.GetValue ( m_control, m_strProprieteControl );
			try
			{
				SetColumnValueAtRow(dataSource, rowNum, valeur);
			}
			catch ( Exception e )
			{
				Console.WriteLine("\n\n\n**************************\nOn ne devrait pas passer là : C2iDataGridColumnStyleAControle : Erreur CommitEdit\n*****************************");
				Console.WriteLine(e.ToString());
				if ( dataSource.Position != rowNum )
				{
					dataSource.Refresh();
					dataSource.Position = rowNum;
					try
					{
						SetColumnValueAtRow(dataSource, rowNum, valeur);
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
			
			if ( m_control == null )
				return;
			if (this.DataGridTableStyle.DataGrid.ReadOnly)
			{
				m_control.Hide();
				return;
			}
			m_bEnEdition = true;
			try
			{
				object valeur = GetColumnValueAtRow(cur, nLigne);
				m_control.Parent = this.DataGridTableStyle.DataGrid;
				m_control.Bounds = bounds;
				if (BackColor.A==255 )
					m_control.BackColor = BackColor;
				else
					m_control.BackColor = this.DataGridTableStyle.DataGrid.BackColor;
				if (ForeColor.A==255 )
					m_control.ForeColor = ForeColor;
				else
					m_control.ForeColor = this.DataGridTableStyle.DataGrid.ForeColor;
				if (m_font != null)
					m_control.Font = Font;

				m_control.Show();
				try
				{
					CInterpreteurTextePropriete.SetValue ( m_control, m_strProprieteControl, valeur );
				}
				catch
				{
					try
					{
						//Tente en texte !
						CInterpreteurTextePropriete.SetValue ( m_control, m_strProprieteControl, GetString ( cur.Current, valeur ) );
					}
					catch 
					{
						//Rien à faire, on n'y arrive pas. Tant pis
					}
				}
				m_control.Focus();

				//Définit le controle qui doit éditer la valeur de la cellule
				ColumnStartedEditing(m_control);
			}
			catch{}
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

		public GetStringElementDelegate GetStringElement;
		public GetColorElementDelegate GetBackColorDelegate;
		public GetColorElementDelegate GetTextColorDelegate;

		private string GetString ( object source, object valeur )
		{
			String strValeur = null;
			if ( GetStringElement != null )
				strValeur = GetStringElement ( source, valeur );
			if ( strValeur == null )
			{
				if (valeur == null)
					strValeur = "";
				else
				{
				
					MethodInfo info = valeur.GetType().GetMethod("ToString", new Type[] { typeof(string) });
					if (info!=null && m_strFormat!="")
					{
						strValeur = info.Invoke(valeur, new object[] {m_strFormat} ).ToString();
					}
					else
						strValeur = valeur.ToString();
				}
			}
			return strValeur;
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
			string strValeur = GetString ( source.Current, obj );
			SolidBrush brush;
			Color cl = BackColor;
			if (GetBackColorDelegate != null)
			{
				Color? clTmp = GetBackColorDelegate(rowNum);
				if (clTmp != null)
					cl = clTmp.Value;
			}
			if (cl.A != 255)
			{
				if ((rowNum % 2) == 0)
					cl = this.DataGridTableStyle.DataGrid.BackColor;
				else
					cl = this.DataGridTableStyle.DataGrid.AlternatingBackColor;
			}
			brush = new SolidBrush(cl);
			g.FillRectangle ( brush, bounds );
			brush.Dispose();
			Font laFont = m_font;
			if ( laFont ==  null )
				laFont = new Font(this.DataGridTableStyle.DataGrid.Font, FontStyle.Regular);
			StringFormat sf = new StringFormat();
			switch ( TextAlign )
			{
				case HorizontalAlignment.Left :
					sf.Alignment = StringAlignment.Near;
					break;
				case HorizontalAlignment.Center :
					sf.Alignment = StringAlignment.Center;
					break;
				case HorizontalAlignment.Right :
					sf.Alignment = StringAlignment.Far;
					break;
			}
			cl = ForeColor;
			if (GetTextColorDelegate != null)
			{
				Color? clTmp = GetTextColorDelegate(rowNum);
				if (clTmp != null)
					cl = clTmp.Value;
			}
			if ( cl.A != 255 )
				cl = this.DataGridTableStyle.DataGrid.ForeColor;
			brush = new SolidBrush(cl);
			g.DrawString ( 
				strValeur, 
				laFont, 
				brush,
				bounds, sf); 
			if ( m_font == null )
				laFont.Dispose();
			brush.Dispose();
			
		}
		#endregion

	}
}
