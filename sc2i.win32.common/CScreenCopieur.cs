using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace sc2i.win32.common
{
	///
	/// This class shall keep the GDI32 APIs used in our program.
	///
	public class PlatformInvokeGDI32
	{
		public  const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll",EntryPoint="DeleteDC")]
		public static extern IntPtr DeleteDC(IntPtr hDc);

		[DllImport("gdi32.dll",EntryPoint="DeleteObject")]
		public static extern IntPtr DeleteObject(IntPtr hDc);

		[DllImport("gdi32.dll",EntryPoint="BitBlt")]
		public static extern bool BitBlt(IntPtr hdcDest,int xDest,
			int yDest,int wDest,
			int hDest,IntPtr hdcSource,
			int xSrc,int ySrc,int RasterOp);

		[DllImport ("gdi32.dll",EntryPoint="CreateCompatibleBitmap")]
		public static extern IntPtr CreateCompatibleBitmap
			(IntPtr hdc,int nWidth, int nHeight);

		[DllImport ("gdi32.dll",EntryPoint="CreateCompatibleDC")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport ("gdi32.dll",EntryPoint="SelectObject")]
		public static extern IntPtr SelectObject(IntPtr hdc,IntPtr bmp);
	}

	///
	/// This class shall keep the User32 APIs used in our program.
	///
	public class PlatformInvokeUSER32
	{
		public  const int SM_CXSCREEN=0;
		public  const int SM_CYSCREEN=1;

		[DllImport("user32.dll", EntryPoint="GetDesktopWindow")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport("user32.dll",EntryPoint="GetDC")]
		public static extern IntPtr GetDC(IntPtr ptr);

		[DllImport("user32.dll",EntryPoint="GetSystemMetrics")]
		public static extern int GetSystemMetrics(int abc);

		[DllImport("user32.dll",EntryPoint="GetWindowDC")]
		public static extern IntPtr GetWindowDC(Int32 ptr);

		[DllImport("user32.dll",EntryPoint="ReleaseDC")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDc);

	}

	///
	/// This class shall keep all the functionality for capturing
	/// the desktop.
	///
	public class CScreenCopieur
	{
		
		public static Bitmap GetDesktopImage( )
		{
			return GetWindowImage ( PlatformInvokeUSER32.GetDC
				(PlatformInvokeUSER32.GetDesktopWindow() ) );
		}

		public static Bitmap GetWindowImage ( Control ctrl )
		{
			Bitmap bmp = GetWindowImage ( PlatformInvokeUSER32.GetDC (
				ctrl.Handle ) );
			//Recadre l'image (qui fait parfois la taille de tout l'écran
			Bitmap newBmp = new Bitmap(ctrl.Width, ctrl.Height);
			Graphics g = Graphics.FromImage ( newBmp );
			g.DrawImage ( bmp, 0, 0 );
			g.Dispose();
			bmp.Dispose();
			return newBmp;
		}

		public static Bitmap GetWindowImage ( IntPtr hDC )
		{
			//In size variable we shall keep the size of the screen.
			SIZE size;

			//Variable to keep the handle to bitmap.
			IntPtr hBitmap;

			//Here we get the handle to the desktop device context.
			

			//Here we make a compatible device context in memory for screen
			//device context.
			IntPtr hMemDC = PlatformInvokeGDI32.CreateCompatibleDC(hDC);

			//We pass SM_CXSCREEN constant to GetSystemMetrics to get the
			//X coordinates of the screen.
			size.cx = PlatformInvokeUSER32.GetSystemMetrics
				(PlatformInvokeUSER32.SM_CXSCREEN);

			//We pass SM_CYSCREEN constant to GetSystemMetrics to get the
			//Y coordinates of the screen.
			size.cy = PlatformInvokeUSER32.GetSystemMetrics
				(PlatformInvokeUSER32.SM_CYSCREEN);

			//We create a compatible bitmap of the screen size and using
			//the screen device context.
			hBitmap = PlatformInvokeGDI32.CreateCompatibleBitmap
				(hDC, size.cx, size.cy);

			//As hBitmap is IntPtr, we cannot check it against null.
			//For this purpose, IntPtr.Zero is used.
			if (hBitmap!=IntPtr.Zero)
			{
				//Here we select the compatible bitmap in the memeory device
				//context and keep the refrence to the old bitmap.
				IntPtr hOld = (IntPtr) PlatformInvokeGDI32.SelectObject
					(hMemDC, hBitmap);
				//We copy the Bitmap to the memory device context.
				PlatformInvokeGDI32.BitBlt(hMemDC, 0, 0,size.cx,size.cy, hDC,
					0, 0,PlatformInvokeGDI32.SRCCOPY);
				//We select the old bitmap back to the memory device context.
				PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
				//We delete the memory device context.
				PlatformInvokeGDI32.DeleteDC(hMemDC);
				//We release the screen device context.
				PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.
					GetDesktopWindow(), hDC);
				//Image is created by Image bitmap handle and stored in
				//local variable.
				Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap); 
				//Release the memory to avoid memory leaks.
				PlatformInvokeGDI32.DeleteObject(hBitmap);
				//This statement runs the garbage collector manually.
				GC.Collect();
				//Return the bitmap 
				return bmp;
			}
			//If hBitmap is null, retun null.
			return null;
		}
	}

	//This structure shall be used to keep the size of the screen.
	public struct SIZE
	{
		public int cx;
		public int cy;
	}


	public class CScreenPrinter
	{
		private Bitmap m_bitmapToPrint = null;
		public static void PrintWindow ( Control control )
		{
			control.Refresh();
			System.Threading.Thread.Sleep(100);
			Bitmap bmp = CScreenCopieur.GetWindowImage ( control );
			PrintDialog dlg = new PrintDialog();
			dlg.PrinterSettings = new PrinterSettings();
			if ( dlg.ShowDialog() == DialogResult.OK )
			{
				PrintDocument document = new PrintDocument();
				document.PrinterSettings =  dlg.PrinterSettings;
				CScreenPrinter printer = new CScreenPrinter();
				printer.m_bitmapToPrint = bmp;
				document.PrintPage += new PrintPageEventHandler(printer.document_PrintPage);
				document.Print();
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private  void document_PrintPage(object sender, PrintPageEventArgs e)
		{
			e.Graphics.PageUnit = GraphicsUnit.Pixel;
			Size sz = e.Graphics.VisibleClipBounds.Size.ToSize();//new Size ( (int)(((double)(e.MarginBounds.Width))*e.Graphics.DpiX/100.0),
				//(int)(((double)e.MarginBounds.Height)*e.Graphics.DpiY/100.0));
		

			
			//Calcule la taille de l'image qui colle dans le rectangle
			double fRatioImage = (double)m_bitmapToPrint.Width/(double)m_bitmapToPrint.Height;
			double fRatioPage = (double)sz.Width/(double)sz.Height;
			
			Rectangle rectImage = new Rectangle ( 0,0,0,0);
			if ( fRatioImage > fRatioPage )
			{
				//Ajuste en largeur
				rectImage.Width = (int)sz.Width;
				rectImage.Height = (int)((double)sz.Width/fRatioImage);
			}
			else
			{
				//Ajuste en hauteur
				rectImage.Height = (int)sz.Height;
				rectImage.Width = (int)((double)sz.Height*fRatioImage );
			}
			e.Graphics.DrawImage ( m_bitmapToPrint, rectImage);
		}
	}
}