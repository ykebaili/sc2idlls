using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common
{
    public static class CUtilBool
    {
        public static bool? BoolFromString(string strChaine)
        {
            strChaine = strChaine.ToUpper().Trim();
            string[] lstVrais = new string[]{
            "1","TRUE","OUI","YES","VRAI"};
            string[] lstFaux = new string[]{
            "0","FALSE","NON","NO","FAUX"};
            if (lstVrais.Contains(strChaine))
                return true;
            if (lstFaux.Contains(strChaine))
                return false;
            return null;
        }

        public static bool BoolFromObject(object obj)
        {
            if (obj is bool)
                return (bool)obj;
            if (obj is int)
                return ((int)obj) != 0;
            if (obj == null)
                return false;
            bool? bTmp = BoolFromString(obj.ToString());
            if (bTmp == null)
                return false;
            return bTmp.Value;
        }
    }
}
