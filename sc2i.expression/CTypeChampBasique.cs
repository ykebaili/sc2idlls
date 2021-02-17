using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace sc2i.expression
{
    public enum ETypeChampBasique
    {
        String = 0,
        Int,
        Decimal,
        Date,
        Bool
    }
    
    //----------------------------------------------------------------
    [Serializable]
    public class CTypeChampBasique : CEnumALibelle<ETypeChampBasique>
    {

        public const string c_ConstanteNull = "NULL";

        //-------------------------------------------------------------
        public CTypeChampBasique ( ETypeChampBasique code )
            :base (code )
        {
        }

        //-------------------------------------------------------------
        public override string Libelle
        {
            get 
            {
                switch (Code)
                {
                    case ETypeChampBasique.String:
                        return I.T("String|20088");
                    case ETypeChampBasique.Int:
                        return I.T("Integer|20089");
                    case ETypeChampBasique.Decimal:
                        return I.T("Decimal|20090");
                    case ETypeChampBasique.Date:
                        return I.T("Date|20091");
                    case ETypeChampBasique.Bool:
                        return I.T("Boolean");
                }
                return "?";
            }
            
        }

        //-------------------------------------------------------------
        public Type TypeDotNet
        {
            get
            {
                return GetTypeDotNet(Code);
            }
        }

        //-------------------------------------------------------------
        public static Type GetTypeDotNet(ETypeChampBasique type)
        {
            switch (type)
            {
                case ETypeChampBasique.String:
                    return typeof(string);
                    break;
                case ETypeChampBasique.Int:
                    return typeof(int);
                    break;
                case ETypeChampBasique.Decimal:
                    return typeof(double);
                    break;
                case ETypeChampBasique.Date:
                    return typeof(DateTime);
                    break;
                case ETypeChampBasique.Bool:
                    return typeof(bool);
                    break;
            }
            return typeof(string);
        }

        //-------------------------------------------------------------
        public static ETypeChampBasique GetTypeChamp(Type type)
        {
            if (type == typeof(int) ||
                type == typeof(byte) ||
                type == typeof(uint))
                return ETypeChampBasique.Int;
            if (type == typeof(double) ||
                type == typeof(float))
                return ETypeChampBasique.Decimal;
            if (type == typeof(bool))
                return ETypeChampBasique.Bool;
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return ETypeChampBasique.Date;
            return ETypeChampBasique.String;
        }

        //-------------------------------------------------------------
        public static Image GetIconType(ETypeChampBasique type)
        {
            switch (type)
            {
                case ETypeChampBasique.String:
                    return Resources.iconTypeBasiqueTexte;
                case ETypeChampBasique.Int:
                    return Resources.iconTypeBasiqueEntier;
                case ETypeChampBasique.Decimal:
                    return Resources.iconTypeBasiqueDecimal;
                case ETypeChampBasique.Date:
                    return Resources.iconTypeBaseDate;
                case ETypeChampBasique.Bool:
                    return Resources.iconTypeBooleen;
                default:
                    break;
            }
            return Resources.iconTypeInconnu;
        }

        /// /////////////////////////////////////////////////////////
        public bool IsDuBonType(object valeur)
        {
            try
            {
                if (valeur == null)
                    return true;
                return TypeDotNet.IsAssignableFrom(valeur.GetType());
            }
            catch
            {
                return false;
            }
        }

        /// /////////////////////////////////////////////////////////
        public static object StringToType(ETypeChampBasique type, string strTexte)
        {
            if (strTexte.ToUpper() == c_ConstanteNull)
                return null;
            switch (type)
            {
                case ETypeChampBasique.Bool:
                    return strTexte.ToString() == "1" || strTexte.ToUpper() == "TRUE";
                case ETypeChampBasique.Date:
                    try
                    {
                        return Convert.ChangeType(strTexte, typeof(DateTime), null);
                    }
                    catch
                    {
                        //Tente le format sc2i de date chaine
                        try
                        {
                            return CUtilDate.FromUniversalString(strTexte);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                case ETypeChampBasique.Decimal:
                    try
                    {
                        return CUtilDouble.DoubleFromString(strTexte);
                    }
                    catch
                    {
                        return null;
                    }
                case ETypeChampBasique.Int:
                    try
                    {
                        return Convert.ChangeType(strTexte, typeof(int), null);
                    }
                    catch
                    {
                        return null;
                    }
                case ETypeChampBasique.String:
                    return strTexte;
            }
            return null;
        }

        /// /////////////////////////////////////////////////////////
        public object StringToType(string strTexte)
        {
            return StringToType(Code, strTexte);
        }

        /// /////////////////////////////////////////////////////////
        public static string TypeToString(object valeur)
        {
            if (valeur == null)
                return "";
            if (valeur.GetType() == typeof(bool))
                return ((bool)valeur) ? "1" : "0";
            else if (valeur.GetType() == typeof(double))
                return valeur.ToString();
            else if (valeur.GetType() == typeof(int))
                return valeur.ToString();
            else if (valeur.GetType() == typeof(DateTime))
                return CUtilDate.GetUniversalString((DateTime)valeur);
            return valeur.ToString();
        }
    }
}
