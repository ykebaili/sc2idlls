using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.Generic;

using sc2i.common;


namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iComboBox.
	/// </summary>
	public class C2iComboBox : ComboBox, IControlALockEdition
	{
		private bool m_bIsUpdating = false;
		private bool m_bLockEdition = false;
		private bool m_bIsLink = false;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>

		public C2iComboBox()
			:base()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();
            m_EditionDropDownStyle = DropDownStyle;
            m_fontOriginal = Font;
			//base.SelectedIndexChanged += new EventHandler(C2iComboBox_SelectedIndexChanged);
		}

		//private void C2iComboBox_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if(
		//}

		public event MouseEventHandler LinkClicked;
		

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // C2iComboBox
            // 
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C2iComboBox_MouseClick);
            this.DropDownStyleChanged += new System.EventHandler(this.C2iComboBox_DropDownStyleChanged);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C2iComboBox_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C2iComboBox_MouseDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C2iComboBox_KeyUp);
            this.DropDown += new System.EventHandler(this.C2iComboBox_DropDown);
            this.ResumeLayout(false);

		}
		#endregion
		public bool IsLink
		{
			get
			{
				return m_bIsLink;
			}
			set
			{
				m_bIsLink = value;
			}
		}

		/// <summary>
		/// Instancie le combobox avec les membres de l'EnumALibelle
		/// </summary>
		/// <param name="typeEnumALibelle"></param>
		public void FillWithEnumALibelle(Type typeEnumALibelle)
		{
			DataSource = null;
			Items.Clear();
			DisplayMember = "Libelle";
			ValueMember = "CodeInt";
			IEnumALibelle[] ienumalibelle = CUtilSurEnum.GetEnumsALibelle(typeEnumALibelle);
			DataSource = ienumalibelle;

		}


		public event EventHandler OnChangeLockEdition;

        Font m_fontOriginal = null;
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
                m_bIsChangingStyleOnLockEdition = true;
                if (value)
                    DropDownStyle = ComboBoxStyle.DropDownList;
                else
                    if (m_EditionDropDownStyle != null)
                        DropDownStyle = m_EditionDropDownStyle.Value;
				SetStyle(ControlStyles.UserPaint, m_bLockEdition);

                //Corrige le problème de perte de font
                //lors du passasge en owner draw (pb windows)
                if (!value && m_fontOriginal != null)
                {
                    Font = m_fontOriginal;
                    m_fontOriginal = null;
                }
                else
                {
                    if (m_fontOriginal == null)
                        m_fontOriginal = Font;
                }
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				Refresh();
			}
		}

		/// ///////////////////////////////////////////////////
		public bool IsUpdating()
		{
			return m_bIsUpdating;
		}

		/// ///////////////////////////////////////////////////
		public new void BeginUpdate()
		{
			m_bIsUpdating = true;
			base.BeginUpdate();
		}

		/// ///////////////////////////////////////////////////
		public new void EndUpdate()
		{
			m_bIsUpdating = false;
			base.EndUpdate();
		}

	
		/// ///////////////////////////////////////////////////
		private void C2iComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (LockEdition)
			{
				e.Handled = true;
				return;
			}
			if ( DropDownStyle == ComboBoxStyle.DropDownList && e.KeyCode == Keys.Space )
			{
				DroppedDown = true;
				e.Handled = true;
				return;
			}
			e.Handled = false;
			
		}

        

		//FT 29/08/08
		//On a surchargé le SelectedValue et le DataSource car lorsqu'un combobox n'est pas encore
		//initialisé dans un control (même avec CreateControl()) son DataManager ne se créé pas 
		//directement au Set de la DataSource (je ne c pas pour quelle raison) et on ne peux donc 
		//pas définir la SelectedValue par avance et c très très embétant pour l'initialisation
		//des formulaires de champs custom notament > Il faut donc l'ajouter dans une form temporairement

		/*public new virtual object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				if (FindForm() == null)
				{
					Form frm = new Form();
					frm.Controls.Add(this);
				}
				base.DataSource = value;
			}
		}*/

		//private object m_lastValSel = null;
		public new virtual object SelectedValue
		{
			get
			{
				/*if (base.SelectedValue == null && m_lastValSel != null)
					return m_lastValSel;*/
				return base.SelectedValue;
			}
			set
			{
				try
				{
					base.SelectedValue = value;
					//m_lastValSel = value;
				}
				catch
				{
				}
			}
		}
		private void C2iComboBox_MouseClick(object sender, MouseEventArgs e)
		{
			if ( m_bIsLink && m_bLockEdition )
				if ( LinkClicked != null )
					LinkClicked ( this, e );
		}

		protected override void WndProc(ref Message m)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.WndProc(ref m);
				return;
			}

			switch (m.Msg)
			{
				case (int)W32_WM.WM_LBUTTONDOWN :
				case (int)W32_WM.WM_RBUTTONDOWN :
					if (!LockEdition)
						base.WndProc(ref m);
					else
					{
						if (LinkClicked != null)
						{
							MouseEventArgs arg = new MouseEventArgs(
								m.Msg == (int)W32_WM.WM_LBUTTONDOWN 
									?MouseButtons.Left
									:MouseButtons.Right, 
									1, 0, 0, 0);
							LinkClicked(this, arg);
						}

					}
					break;
				case (int)W32_WM.WM_LBUTTONDBLCLK :
					if ( !LockEdition )
						base.WndProc(ref m);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}


		[DllImport("user32")]
		private static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32")]
		private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

		[StructLayout(LayoutKind.Sequential)]
		private struct ComboBoxInfo
		{
			public int cbSize;
			public RECT rcItem;
			public RECT rcButton;
			public IntPtr stateButton;
			public IntPtr hwndCombo;
			public IntPtr hwndEdit;
			public IntPtr hwndList;
		}

		[DllImport("user32")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if ( LockEdition )
				PaintCombo(e.Graphics);
		}


		/// ///////////////////////////////////////////////////
		protected void PaintCombo(Graphics g)
		{
			Rectangle rct = ClientRectangle;
			
			Brush br = new SolidBrush(BackColor);
			g.FillRectangle(br, rct);
			br.Dispose();

			Pen pen = Pens.Black;
			rct.Width--;
			rct.Height--;
			g.DrawRectangle(pen, rct);

			Font ft = Font;
			Color color = ForeColor;
			if (m_bIsLink)
			{
				color = Color.Blue;
				ft = new Font ( Font, FontStyle.Underline );
			}
			TextRenderer.DrawText(g, Text, ft, ClientRectangle, color, BackColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.HidePrefix);
			if (m_bIsLink)
				ft.Dispose();
		}

		private void C2iComboBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (LockEdition && m_bIsLink)
				Cursor = Cursors.Hand;
			else
				Cursor = Cursors.Arrow;
		}

		private void C2iComboBox_DropDown(object sender, EventArgs e)
		{

		}

		private void C2iComboBox_MouseDown(object sender, MouseEventArgs e)
		{
		}

        private bool m_bIsChangingStyleOnLockEdition = false;
        private ComboBoxStyle? m_EditionDropDownStyle;
        private void C2iComboBox_DropDownStyleChanged(object sender, EventArgs e)
        {
            if (!m_bIsChangingStyleOnLockEdition)
                m_EditionDropDownStyle = DropDownStyle;
        }

       
	}
}
