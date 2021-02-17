using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iDateTimeExPicker.
	/// </summary>
	public class C2iDateTimeExPicker : System.Windows.Forms.UserControl, IControlALockEdition
	{
		private string m_txtNull = I.T("None|117");
		private bool m_bLockEdition = false;
		private C2iDateTimePicker m_dtPicker;
		private System.Windows.Forms.CheckBox m_chkNotNull;
		private Label m_label;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event EventHandler OnValueChange;

		//------------------------------------------------------------
		public C2iDateTimeExPicker()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
			m_dtPicker.Value = DateTime.Now;

			// TODO : ajoutez les initialisations après l'appel à InitForm

		}
		//------------------------------------------------------------
		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		//------------------------------------------------------------
		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_chkNotNull = new System.Windows.Forms.CheckBox();
            this.m_label = new System.Windows.Forms.Label();
            this.m_dtPicker = new sc2i.win32.common.C2iDateTimePicker();
            this.SuspendLayout();
            // 
            // m_chkNotNull
            // 
            this.m_chkNotNull.Checked = true;
            this.m_chkNotNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkNotNull.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkNotNull.Location = new System.Drawing.Point(0, 0);
            this.m_chkNotNull.Name = "m_chkNotNull";
            this.m_chkNotNull.Size = new System.Drawing.Size(16, 20);
            this.m_chkNotNull.TabIndex = 1;
            this.m_chkNotNull.Leave += new System.EventHandler(this.m_chkNotNull_Leave);
            this.m_chkNotNull.Enter += new System.EventHandler(this.m_chkNotNull_Enter);
            this.m_chkNotNull.CheckedChanged += new System.EventHandler(this.m_chkNotNull_CheckedChanged);
            // 
            // m_label
            // 
            this.m_label.BackColor = System.Drawing.Color.White;
            this.m_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_label.Location = new System.Drawing.Point(16, 0);
            this.m_label.Name = "m_label";
            this.m_label.Size = new System.Drawing.Size(180, 20);
            this.m_label.TabIndex = 2;
            this.m_label.Visible = false;
            // 
            // m_dtPicker
            // 
            this.m_dtPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtPicker.Location = new System.Drawing.Point(16, 0);
            this.m_dtPicker.LockEdition = false;
            this.m_dtPicker.Name = "m_dtPicker";
            this.m_dtPicker.Size = new System.Drawing.Size(180, 20);
            this.m_dtPicker.TabIndex = 0;
            this.m_dtPicker.Value = new System.DateTime(2011, 10, 25, 10, 17, 55, 527);
            this.m_dtPicker.TextChanged += new System.EventHandler(this.C2iComboBox_TextChanged);
            this.m_dtPicker.ValueChanged += new System.EventHandler(this.C2iComboBox_TextChanged);
            // 
            // C2iDateTimeExPicker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_label);
            this.Controls.Add(this.m_dtPicker);
            this.Controls.Add(this.m_chkNotNull);
            this.Name = "C2iDateTimeExPicker";
            this.Size = new System.Drawing.Size(196, 20);
            this.BackColorChanged += new System.EventHandler(this.C2iDateTimeExPicker_BackColorChanged);
            this.ResumeLayout(false);

		}
		#endregion
		//------------------------------------------------------------
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
				UpdateVisibility();
				
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		//------------------------------------------------------------
		private string GetTexte()
		{

			if (Value==null)
				return TextNull;
			switch ( Format )
			{
				case DateTimePickerFormat.Custom :
					return ((DateTime)Value).ToString(CustomFormat);
				case DateTimePickerFormat.Long :
					return ((DateTime)Value).ToString("D");
				case DateTimePickerFormat.Short :
					return ((DateTime)Value).ToString("d");
				case DateTimePickerFormat.Time :
					return ((DateTime)Value).ToString("T");
			}
			return Value.ToString();
		}
		
		//------------------------------------------------------------
		private void C2iComboBox_TextChanged(object sender, System.EventArgs e)
		{
			if (OnValueChange != null)
				OnValueChange(this, new EventArgs());
			m_label.Text = GetTexte();
		}
		
		//------------------------------------------------------------
		private void m_chkNotNull_CheckedChanged(object sender, System.EventArgs e)
		{
			if (OnValueChange != null)
				OnValueChange(this, new EventArgs());
			UpdateVisibility();
			
		}

		//------------------------------------------------------------
		public DateTimePickerFormat Format
		{
			get
			{
				return m_dtPicker.Format;
			}
			set
			{
				m_dtPicker.Format = value;
			}
		}
		//------------------------------------------------------------
		public override string Text
		{
			get
			{
				return m_dtPicker.Text;
			}
			set
			{
				m_dtPicker.Text = value;
			}
		}
		//------------------------------------------------------------
		[DefaultValue("now")]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Content)]
		[Localizable(false)]
		public CDateTimeEx Value
		{
			get
			{
				if (!m_chkNotNull.Checked)
					return null;
				return (CDateTimeEx) m_dtPicker.Value;
			}
			set
			{
				m_chkNotNull.Checked = (value!=null);
				if (value!=null)
				{
                    DateTime val = (DateTime)value;
                    if (val < m_dtPicker.MinDate)
                        val = m_dtPicker.MinDate;
                    if(val > m_dtPicker.MaxDate)
                        val = m_dtPicker.MaxDate;
					m_dtPicker.Value = val;
					m_dtPicker.Refresh();
					m_dtPicker.Value = val;
				}
				m_label.Text = GetTexte();
			}
		}
		//------------------------------------------------------------
		public string TextNull
		{
			get
			{
				return m_txtNull;
			}
			set
			{
				m_txtNull = value;
			}
		}
		//------------------------------------------------------------
		public string CustomFormat
		{
			get
			{
				return m_dtPicker.CustomFormat;
			}
			set
			{
				m_dtPicker.CustomFormat = value;
			}
		}

		//------------------------------------------------------------
		private void UpdateVisibility()
		{
			this.SuspendDrawing();
			m_chkNotNull.Visible = !m_bLockEdition;
			m_dtPicker.Visible = !m_bLockEdition && m_chkNotNull.Checked;
			m_label.Visible = m_bLockEdition || !m_chkNotNull.Checked;
			if (m_label.Visible)
				m_label.Text = GetTexte();
            this.ResumeDrawing();
		}

		//------------------------------------------------------------
		public bool Checked
		{
			get
			{
				return m_chkNotNull.Checked;
			}
			set
			{
				m_chkNotNull.Checked = value;
				UpdateVisibility();
			}
		}

		private void C2iDateTimeExPicker_BackColorChanged(object sender, EventArgs e)
		{
			m_label.BackColor = BackColor;
		}


		private BorderStyle m_originalBorderStyle = BorderStyle.None;
		private void m_chkNotNull_Enter(object sender, EventArgs e)
		{
			m_originalBorderStyle = BorderStyle;
			switch (BorderStyle)
			{
				case BorderStyle.Fixed3D:
					BorderStyle = BorderStyle.FixedSingle;
					break;
				case BorderStyle.FixedSingle:
					BorderStyle = BorderStyle.Fixed3D;
					break;
				case BorderStyle.None:
					BorderStyle = BorderStyle.FixedSingle;
					break;
				default:
					break;
			}
		}

		private void m_chkNotNull_Leave(object sender, EventArgs e)
		{
			BorderStyle = m_originalBorderStyle;
		}
		
	}
}
