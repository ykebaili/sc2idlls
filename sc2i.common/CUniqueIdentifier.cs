using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common
{
    public static class CUniqueIdentifier
    {
        public static string GetNew()
        {
            return GetNew ( "0123456789ABCDEFGHIHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-" );
        }

        public static string GetNew(string strIdCaractères)
        {
            string str = Guid.NewGuid().ToString();
            string strGUIDChar = "0123456789abcdef";
            str = str.Replace("-", "").PadLeft(32,'0');
            StringBuilder blFinal = new StringBuilder();
            for (int n = 0; n < 11; n++)
            {
                long nVal = 0;
                for (int c = 0; c < 3; c++)
                {
                    if ( n*3+c < str.Length )
                        nVal += strGUIDChar.IndexOf(str[n * 3 + c]) * ((long)Math.Pow(16,(2 - c)));
                }
                string strTmp = "";
                while (nVal > 0)
                {
                    long nPart = nVal % strIdCaractères.Length;
                    strTmp = strIdCaractères[(int)nPart] + strTmp;
                    nVal -= nPart;
                    nVal /= (long)strIdCaractères.Length;
                }
                blFinal.Append( strTmp.PadLeft(2,'0') );
            }


            return blFinal.ToString();
        }
    }
}
