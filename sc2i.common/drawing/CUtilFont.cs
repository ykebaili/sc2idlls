using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.common.drawing
{
    public class CUtilFont
    {
        public static CResultAErreur SerializeFont(C2iSerializer serializer, ref Font ft)
        {
            CResultAErreur result = CResultAErreur.True;
            bool bHasFont = ft != null;
            serializer.TraiteBool(ref bHasFont);
            if (bHasFont)
            {
                if (serializer.Mode == ModeSerialisation.Lecture)
                    ft = new Font("Arial", 1, FontStyle.Regular);
                Byte gdiCharset = ft.GdiCharSet;
                bool gdiVerticalFont = ft.GdiVerticalFont;
                int nUnit = (int)ft.Unit;
                string strName = ft.Name;
                double fSize = ft.Size;
                int nStyle = (int)ft.Style;
                serializer.TraiteByte(ref gdiCharset);
                serializer.TraiteBool(ref gdiVerticalFont);
                serializer.TraiteString(ref strName);
                serializer.TraiteDouble(ref fSize);
                serializer.TraiteInt(ref nStyle);
                serializer.TraiteInt(ref nUnit);
                if (serializer.Mode == ModeSerialisation.Lecture)
                    ft = new Font(strName, (float)fSize, (FontStyle)nStyle, (GraphicsUnit)nUnit, gdiCharset, gdiVerticalFont);
            }
            return result;
        }

    }
}
