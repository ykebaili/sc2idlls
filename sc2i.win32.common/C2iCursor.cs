using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public class C2iCursor 
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);


        public static Cursor CreateCursorNoResize(Bitmap bmp, Point hotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = hotSpot.X;
            tmp.yHotspot = hotSpot.Y;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }

        public static Cursor CreateCursorNoResize(Icon icon, Point hotSpot)
        {
            IntPtr ptr = icon.Handle;
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = hotSpot.X;
            tmp.yHotspot = hotSpot.Y;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }
    }
}
