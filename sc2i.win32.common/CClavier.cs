using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CClavier.
	/// </summary>
	public class CClavier
	{
		[DllImport("user32")] 
		public static extern int GetKeyState(int nVirtKey);

		[DllImport("User32.dll", SetLastError=true)]
		public static extern int SendInput(int nInputs, INPUT[] pInputs, int cbSize);

		[DllImport("User32.dll", SetLastError=true)]
		public static extern int MapVirtualKey( int uCode, int uMapType );

		[DllImport("User32.dll", SetLastError=true)]
		public static extern int SetKeyboardState ( byte[] states );




		[StructLayout(LayoutKind.Explicit,Size=28)]
			public struct INPUT
		{
			[FieldOffset(0)] public uint type;
			[FieldOffset(4)] public KEYBDINPUT ki;
		};

		public struct KEYBDINPUT
		{
			public ushort wVk;
			public ushort wScan;
			public uint dwFlags;
			public long time;
			public uint dwExtraInfo;
		};

		private static void SetKeyState( int nVirtKey, bool bState)
		{

		}

		private static void ClickTouche ( ushort nVirtKey )
		{
		
			INPUT[] input = new INPUT[2];
			
			input[0].type = input[1].type = 1;
			input[0].ki.wVk = input[1].ki.wVk = (ushort)nVirtKey;
			input[1].ki.dwFlags = 2;
			SendInput ( 2, input, Marshal.SizeOf ( typeof(INPUT)) );
		}

		private static void Toggle ( ushort nVirtKey, bool bToggle )
		{
			int nState = GetKeyState ( nVirtKey );
			if ( nState == 0 && bToggle )
				ClickTouche ( nVirtKey );
			if ( nState != 0 && !bToggle )
				ClickTouche ( nVirtKey );
		}


		public static bool CapsLock
		{
			get
			{
				int nState = GetKeyState ( (int)Keys.CapsLock );
				return nState != 0;
			}
			set
			{
				Toggle ( (ushort)Keys.CapsLock, value );
			}
		}

		public static bool NumLock
		{
			get
			{
				return GetKeyState ( (int)Keys.NumLock ) != 0;
			}
			set
			{
				Toggle ( (ushort)Keys.NumLock, value );
			}
		}

		public static bool ScrollLock
		{
			get
			{
				return GetKeyState ((int)Keys.Scroll) != 0;
			}
			set
			{
				Toggle ( (ushort)Keys.Scroll, value );
			}
		}
	}
}
