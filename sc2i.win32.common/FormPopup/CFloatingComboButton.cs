using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;


namespace sc2i.win32.common
{
	/// <summary>
	/// Summary description for ComboButton.
	/// </summary>
	public class CFloatingComboButton : System.Windows.Forms.Button
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFloatingComboButton(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public CFloatingComboButton()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary> 
		/// Clean up any resources being used.
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


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			System.Drawing.SolidBrush br=new SolidBrush(Color.Black);
			Point []p=new Point[4];
			p[0].X=(this.Width-8)/2;
			p[0].Y=(this.Height-8)/2;
			p[1].X=p[0].X+8;
			p[1].Y=p[0].Y;
			p[2].X=p[0].X+4;
			p[2].Y=p[0].Y+7;
			p[3].X=p[0].X;
			p[3].Y=p[0].Y;
			e.Graphics.FillPolygon(br,p);
		}

	}
}
