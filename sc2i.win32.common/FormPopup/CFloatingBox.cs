using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Globalization;


namespace sc2i.win32.common
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class CFloatingBox : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox txtFloatingBox;
		private CFloatingComboButton btnDownArrow;
		private IFloatingPopup iFloatingPopup;

		private System.ComponentModel.IContainer components;
	
		public event CancelEventHandler PopupHiding=null;
		public event CancelEventHandler PopupShowing=null;
		public event EventHandler PopupHidden=null;
		public event EventHandler PopupShown=null;
		
		public CFloatingBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			// TODO: Add any initialization after the InitComponent call
			iFloatingPopup=new CFloatingFormBase();

			this.txtFloatingBox.SelectionStart=1;
			
			iFloatingPopup.UserControl=this;
			iFloatingPopup.PopupHidden+=new EventHandler(iFloatingPopup_PopupHidden);
			iFloatingPopup.PopupHiding+=new CancelEventHandler(iFloatingPopup_PopupHiding);
			iFloatingPopup.PopupShowing+=new CancelEventHandler(iFloatingPopup_PopupShowing);
			iFloatingPopup.PopupShown+=new EventHandler(iFloatingPopup_PopupShown);			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtFloatingBox = new System.Windows.Forms.TextBox();
			this.btnDownArrow = new sc2i.win32.common.CFloatingComboButton(this.components);
			this.SuspendLayout();
			// 
			// txtFloatingBox
			// 
			this.txtFloatingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFloatingBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFloatingBox.Location = new System.Drawing.Point(0, 0);
			this.txtFloatingBox.Name = "txtFloatingBox";
			this.txtFloatingBox.Size = new System.Drawing.Size(81, 20);
			this.txtFloatingBox.TabIndex = 0;
			this.txtFloatingBox.Text = "";
			this.txtFloatingBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFloatingBox_KeyPress);
			this.txtFloatingBox.TextChanged += new System.EventHandler(this.txtFloatingBox_TextChanged);
			// 
			// btnDownArrow
			// 
			this.btnDownArrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDownArrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDownArrow.Location = new System.Drawing.Point(80, 0);
			this.btnDownArrow.Name = "btnDownArrow";
			this.btnDownArrow.Size = new System.Drawing.Size(20, 20);
			this.btnDownArrow.TabIndex = 1;
			this.btnDownArrow.Click += new System.EventHandler(this.btnDownArrow_Click);
			// 
			// FloatingBox
			// 
			this.Controls.Add(this.btnDownArrow);
			this.Controls.Add(this.txtFloatingBox);
			this.Name = "FloatingBox";
			this.Size = new System.Drawing.Size(102, 20);
			this.SizeChanged += new System.EventHandler(this.FloatingBox_SizeChanged);
			this.ParentChanged += new System.EventHandler(this.FloatingBox_ParentChanged);
			this.ResumeLayout(false);

		}
		#endregion

		private void FloatingBox_SizeChanged(object sender, System.EventArgs e)
		{
			if(this.Height!=this.btnDownArrow.Height)
				this.Height=this.btnDownArrow.Height;
			if(this.Width<this.btnDownArrow.Width*2)
				this.Width=this.btnDownArrow.Width*2;
		}

		private void btnDownArrow_Click(object sender, System.EventArgs e)
		{
			this.iFloatingPopup.Show();
		}
		
		private void txtFloatingBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			OnKeyPress(e);
		}
		
		private void txtFloatingBox_TextChanged(object sender, System.EventArgs e)
		{
			OnTextChanged(e);
		}
		
		public override string Text
		{
			get
			{
				return this.txtFloatingBox.Text;
			}
			set
			{
				this.txtFloatingBox.Text = value;
			}
		}

		protected virtual void OnPopupHidden(EventArgs e)
		{
			if(PopupHidden!=null)
				PopupHidden(this,e);
		}
		private void iFloatingPopup_PopupHidden(object sender, EventArgs e)
		{
			OnPopupHidden(e);
		}

		protected virtual void OnPopupHiding(CancelEventArgs e)
		{
			if(PopupHiding!=null)
				PopupHiding(this,e);
		}
		private void iFloatingPopup_PopupHiding(object sender, CancelEventArgs e)
		{
			OnPopupHiding(e);
		}
		protected virtual void OnPopupShowing(CancelEventArgs e)
		{
			if(PopupShowing!=null)
				PopupShowing(this,e);
		}
		private void iFloatingPopup_PopupShowing(object sender, CancelEventArgs e)
		{
			OnPopupShowing(e);
		}

		protected virtual void OnPopupShown(EventArgs e)
		{
			if(PopupShown!=null)
				PopupShown(this,e);
		}
		private void iFloatingPopup_PopupShown(object sender, EventArgs e)
		{
			OnPopupShown(e);
		}

		private void FloatingBox_ParentChanged(object sender, System.EventArgs e)
		{
			if(this.Parent is Form&&iFloatingPopup!=null)
			{
				((Form)this.Parent).AddOwnedForm(iFloatingPopup.PopupForm);
				((Form)this.Parent).Move+=new EventHandler(FloatingBox_Move);
			}
		}

		private void FloatingBox_Move(object sender, EventArgs e)
		{
			iFloatingPopup.SetAutoLocation();
		}

		public IFloatingPopup Popup
		{
			get
			{
				return iFloatingPopup;
			}
			set
			{
				if(iFloatingPopup!=null)
				{
					iFloatingPopup.PopupHidden-=new EventHandler(iFloatingPopup_PopupHidden);
					iFloatingPopup.PopupHiding-=new CancelEventHandler(iFloatingPopup_PopupHiding);
					iFloatingPopup.PopupShowing-=new CancelEventHandler(iFloatingPopup_PopupShowing);
					iFloatingPopup.PopupShown-=new EventHandler(iFloatingPopup_PopupShown);
				}
				if(value!=null)
				{
					iFloatingPopup=value;
					iFloatingPopup.PopupHidden+=new EventHandler(iFloatingPopup_PopupHidden);
					iFloatingPopup.PopupHiding+=new CancelEventHandler(iFloatingPopup_PopupHiding);
					iFloatingPopup.PopupShowing+=new CancelEventHandler(iFloatingPopup_PopupShowing);
					iFloatingPopup.PopupShown+=new EventHandler(iFloatingPopup_PopupShown);
				}
			}
		}
		public int SelectionStart
		{
			get
			{
				return this.txtFloatingBox.SelectionStart;
			}
			set
			{
				this.txtFloatingBox.SelectionStart=value;
			}
		}
		public int SelectionLength
		{
			get
			{
				return this.txtFloatingBox.SelectionLength;
			}
			set
			{
				this.txtFloatingBox.SelectionLength=value;
			}
		}
		
	}
}
