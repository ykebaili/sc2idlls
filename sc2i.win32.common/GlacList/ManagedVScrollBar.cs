using System;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;


namespace sc2i.win32.common
{
	/// <summary>
	/// Summary description for ManagedVScrollBar.
	/// </summary>
	internal class ManagedVScrollBar : System.Windows.Forms.VScrollBar
	{
		internal ManagedVScrollBar()
		{
			this.TabStop = false;
			this.GotFocus += new EventHandler( ReflectFocus );
		}

		public void ReflectFocus( object source, EventArgs e )
		{
			Debug.WriteLine( "focus called" );
			this.Parent.Focus();
		}

		private void InitializeComponent()
		{
			// 
			// ManagedVScrollBar
			// 

		}

		public int mTop
		{
			set
			{
				if ( Top!=value)
					Top = value;
			}
		}
		public int mLeft
		{
			set
			{
				if ( value != Left )
					Left = value;
			}
		}
		public int mWidth
		{
			get
			{
				if ( Visible != true )
					return 0;
				else
					return Width;
			}
			set
			{
				if ( Width != value )
					Width = value;
			}
		}
		public int mHeight
		{
			get
			{
				if ( Visible != true )
					return 0;
				else
					return Height;
			}
			set
			{
				if ( Height != value )
					Height = value;
			}
		}
		public bool mVisible
		{
			set
			{
				if ( Visible != value )
					Visible = value;
			}
		}
		public int mSmallChange
		{
			set
			{
				if ( SmallChange != value )
					SmallChange = value;
			}
		}
		public int mLargeChange
		{
			set
			{
				if ( LargeChange != value )
					LargeChange = value;
			}
		}
		public int mMaximum
		{
			set
			{
				if ( Maximum != value )
					Maximum = value;
			}
		}
	}

}
