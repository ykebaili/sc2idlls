using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data.Excel
{
    public class CUtilExcel
    {

        /// <summary>
        /// Donne la prochaine entête de colonne dans la séquence de nomage des colonnes d'Excel
        /// A, B, C ... Z, AA, AB... AZ; BA, BB, BS.... BZ....ZZ, AAA, AAB, AAC...
        /// </summary>
        /// <param name="strCol"></param>
        /// <returns></returns>
        public static string GetNextColumnHeader(string strCol)
        {
            for (int i = strCol.Length - 1; i >= 0; i--)
            {
                char car = strCol[i];
                car = NextChar(car);
                strCol = strCol.Substring(0, i) + car.ToString() + ((i < strCol.Length) ? strCol.Substring(i + 1, strCol.Length - 1 - i) : "");
                if (car != 'A')
                    break;
                else if (i == 0)
                {
                    strCol = "A" + strCol;
                    break;
                }
            }

            return strCol;

        }

        private static char NextChar(char car)
        {
            //string sequence = "ABC"; // Pour test
            string sequence = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = sequence.IndexOf(car);
            index++;
            if (index >= sequence.Length)
                index = 0;

            return sequence[index];
        }



    }
}
