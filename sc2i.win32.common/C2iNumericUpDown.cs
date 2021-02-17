using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iComboBox.
	/// </summary>
	public class C2iNumericUpDown : NumericUpDown, IControlALockEdition
	{
		private bool m_bLockEdition = false;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>

		public C2iNumericUpDown()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}


		

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// C2iNumericUpDown
			// 
			this.Maximum = new System.Decimal(new int[] {
															100000,
															0,
															0,
															0});
			this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ThousandsSeparator = true;
			this.Enter += new System.EventHandler(this.C2iNumericUpDown_Enter);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		public event EventHandler OnChangeLockEdition; 

		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				Enabled = !m_bLockEdition;
				//SetStyle(ControlStyles.UserPaint, !Enabled);
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				Refresh();
			}
		}

		/// ///////////////////////////////////////////////////
		public int IntValue
		{
			get
			{
				return ( int )Value;
			}
			set
			{
				if ( (decimal)value < Minimum )
					Value = Minimum;
				else if ( (decimal)value > Maximum )
					Value = Maximum;
				else
					Value = (decimal) value;
			}
		}

		/// ///////////////////////////////////////////////////
		public double DoubleValue
		{
			get
			{
				return ( double )Value;
			}
			set
			{
				if ( (decimal)value < Minimum )
					Value = Minimum;
				else if ( (decimal)value > Maximum )
					Value = Maximum;
				else
					Value = (decimal) value;
			}
		}



		private void C2iNumericUpDown_Enter(object sender, System.EventArgs e)
		{
			Select(0,2000);
		}

		protected override void WndProc(ref Message m)
		{
			/*switch (m.Msg)
			{
				case (int)W32_WM.WM_PAINT:
					if (m_bLockEdition)
						return;*
					break;
			}*/

			base.WndProc(ref m);
		}


		/// ///////////////////////////////////////////////////
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			
		}
		/// ///////////////////////////////////////////////////
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rct = ClientRectangle;
			Brush br = new SolidBrush(BackColor);
			e.Graphics.FillRectangle(br, rct);

			br.Dispose();
			TextFormatFlags flags = TextFormatFlags.Default;
			switch (TextAlign)
			{
				case HorizontalAlignment.Left:
					flags = TextFormatFlags.Left;
					break;
				case HorizontalAlignment.Center:
					flags = TextFormatFlags.HorizontalCenter;
					break;
				case HorizontalAlignment.Right:
					flags = TextFormatFlags.Right;
					break;
			}
			flags |= TextFormatFlags.HidePrefix;
			TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, BackColor, flags);
		}


	}
}
