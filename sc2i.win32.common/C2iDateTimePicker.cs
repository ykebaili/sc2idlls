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
	public class C2iDateTimePicker : DateTimePicker, IControlALockEdition
	{
		private bool m_bLockEdition = false;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>

		public C2iDateTimePicker()
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
			// 
			// C2iDateTimePicker
			// 
			this.Value = DateTime.Now;
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
				SetStyle(ControlStyles.UserPaint, !Enabled);
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				Refresh();
			}
		}

		/// ///////////////////////////////////////////////////
		private string GetTexte()
		{
			switch ( Format )
			{
				case DateTimePickerFormat.Custom :
					return Value.ToString(CustomFormat);
				case DateTimePickerFormat.Long :
					return Value.ToString("D");
				case DateTimePickerFormat.Short :
					return Value.ToString("d");
				case DateTimePickerFormat.Time :
					return Value.ToString("T");
			}
			return Value.ToString();
		}


		/// ///////////////////////////////////////////////////
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			Rectangle rct = ClientRectangle;
			Brush br = new SolidBrush(BackColor);
			pevent.Graphics.FillRectangle(br, rct);
			br.Dispose();
		}
		/// ///////////////////////////////////////////////////
		protected override void OnPaint(PaintEventArgs e)
		{
			TextFormatFlags flags = TextFormatFlags.Default | TextFormatFlags.HidePrefix;
			TextRenderer.DrawText(e.Graphics, GetTexte(), Font, ClientRectangle, ForeColor, BackColor, flags);
		}




	}
}
