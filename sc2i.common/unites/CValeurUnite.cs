using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.common.unites
{
    [Serializable]
    public class CValeurUnite : I2iSerializable, IConvertible
    {
        private double m_fValeur;
        private string m_strUnite;
        private string m_strFormat = "";

        [NonSerialized]
        private IUnite m_unite = null;

        //------------------------------------
        public CValeurUnite()
        {
        }

        //-------------------------------------------------------
        public CValeurUnite(double fValeur, string strUnite)
        {
            m_fValeur = fValeur;
            m_strUnite = strUnite;
            m_unite = null;
        }

        //-------------------------------------------------------
        public CValeurUnite(double fValeur, string strUnite, string strFormat)
            :this(fValeur, strUnite)
        {
            m_strFormat = strFormat;
        }

        //------------------------------------
        [DynamicField("Value")]
        public double Valeur
        {
            get
            {
                return m_fValeur;
            }
            set
            {
                m_fValeur = value;
            }
        }

        //------------------------------------
        [DynamicField("Unit")]
        public string Unite
        {
            get
            {
                return m_strUnite;
            }
            set
            {
                m_strUnite = value;
                m_unite = null;
            }
        }

        //------------------------------------
        public IUnite IUnite
        {
            get
            {
                if (m_unite != null)
                    return m_unite;
                if (Unite.Length > 0)
                {
                    m_unite = CGestionnaireUnites.GetUnite(Unite);
                }
                return m_unite;
            }
            set
            {
                if (value == null)
                    Unite = "";
                else
                    Unite = m_unite.GlobalId;
            }
        }
        
        //------------------------------------
        [DynamicField("Format")]
        public string Format
        {
            get
            {
                return m_strFormat;
            }
            set
            {
                m_strFormat = value.Trim();
            }
        }

        //------------------------------------
        public double ConvertToBase()
        {
            double fValeur = Valeur;
            string[] strComposants = CUtilUnite.Developpe(Unite);
            foreach (string strComposant in strComposants)
            {
                string strIdUnite = strComposant;
                bool bDiviser = false;
                if (strComposant[0] == '/')
                {
                    bDiviser = true;
                    strIdUnite = strComposant.Substring(1);
                }
                if (strComposant[0] == '.')
                    strIdUnite = strComposant.Substring(1);
                IUnite unite = CGestionnaireUnites.GetUnite(strIdUnite);
                if (unite == null)
                    return Valeur;
                if (bDiviser)
                    fValeur /= unite.FacteurVersBase;
                else
                    fValeur *= unite.FacteurVersBase;
                if (strComposants.Length == 1)
                    fValeur += unite.OffsetVersBase;
            }
            return fValeur;
        }

        //------------------------------------
        public static CValeurUnite GetValeurFromValeurBase(double fValeurDeBase, string strUnite)
        {
            double fValeur = fValeurDeBase;
            string[] strComposants = CUtilUnite.Developpe(strUnite);
            foreach (string strComposant in strComposants)
            {
                string strIdUnite = strComposant;
                bool bDiviser = false;
                if (strComposant[0] == '/')
                {
                    bDiviser = true;
                    strIdUnite = strComposant.Substring(1);
                }
                if (strComposant[0] == '.')
                    strIdUnite = strComposant.Substring(1);
                IUnite unite = CGestionnaireUnites.GetUnite(strIdUnite);
                if (unite == null)
                    return null;
                if (bDiviser)
                    fValeur *= unite.FacteurVersBase;
                else
                    fValeur /= unite.FacteurVersBase;
                if (strComposant.Length == 1)
                    fValeur -= unite.OffsetVersBase;
            }
            return new CValeurUnite(fValeur, strUnite);
        }


        //------------------------------------
        [DynamicMethod("Convert value to another unit")]
        public CValeurUnite ConvertTo(string strUnite)
        {
            if (strUnite == Unite)
                return this;
            string strMyCle = CUtilUnite.GetIdClasseUnite(Unite);
            string strAutrecle = CUtilUnite.GetIdClasseUnite(strUnite);
            if ( strMyCle == null || strAutrecle == null || strMyCle != strAutrecle )
                throw new Exception(I.T("Can not convert from @1 to @2|20017", Unite, strUnite));
            double fValeur = ConvertToBase();
            string[] strComposants = CUtilUnite.Developpe(strUnite);
            foreach (string strComposant in strComposants)
            {
                string strIdUnite = strComposant;
                bool bDiviser = false;
                if (strComposant[0] == '/')
                {
                    bDiviser = true;
                    strIdUnite = strComposant.Substring(1);
                }
                IUnite unite = CGestionnaireUnites.GetUnite(strIdUnite);
                if (unite == null)
                    return this;
                if (bDiviser)
                    fValeur *= unite.FacteurVersBase;
                else
                    fValeur /= unite.FacteurVersBase;
            }
            return new CValeurUnite(fValeur, CUtilUnite.Factorise(strComposants));
        }

        //------------------------------------
        public override string ToString()
        {
            if (m_strFormat.Length > 0)
                return ToString(m_strFormat);
            IUnite unite = CGestionnaireUnites.GetUnite(Unite);
            if (unite != null)
                return Valeur.ToString() + unite.LibelleCourt;
            return Valeur.ToString() + Unite;
        }

        //------------------------------------
        public string ToString(string strFormat)
        {
            if (strFormat.Trim().Length > 0)
                return GetStringDecomposeeValeurSimple(strFormat.Split(' '));
            return Valeur.ToString() + Unite;
        }


        //------------------------------------
        public CValeurUnite GetValeurInverse()
        {
            return new CValeurUnite(1 / Valeur, CUtilUnite.GetUniteInverse(Unite));
        }

        //------------------------------------
        /// <summary>
        /// Décompose une valeur simple dans les unités de l'unité.
        /// Par exemple, 65 min décomposé en h min donne 1h5min
        /// </summary>
        /// <param name="strUnitesAUtiliser"></param>
        /// <returns></returns>
        public string GetStringDecomposeeValeurSimple(params string[] strUnitesAUtiliser)
        {
            List<string> strUnitesCorrigées = new List<string>();
            foreach (string strUnite in strUnitesAUtiliser)
                if (strUnite.Length > 0)
                    strUnitesCorrigées.Add(strUnite.Trim());
            IUnite unite = CGestionnaireUnites.GetUnite(Unite);
            if (unite == null)
                return null;
            IClasseUnite classe = unite.Classe;
            if (classe == null)
                return null;
            List<double?> lstVals = new List<double?>();
            double fValeurRestante = ConvertToBase();
            StringBuilder bl = new StringBuilder();
            
            CValeurUnite valeurTmp = new CValeurUnite(Math.Abs(fValeurRestante), classe.UniteBase );
            for (int nUnite = 0; nUnite < strUnitesCorrigées.Count; nUnite++)
            {
                string strUnite = strUnitesCorrigées[nUnite];
                string strLib = strUnite;
                IUnite u = CGestionnaireUnites.GetUnite(strUnite);
                if (u != null)
                    strLib = u.LibelleCourt;
                valeurTmp = valeurTmp.ConvertTo(strUnite);

                int nVal = (int)valeurTmp.Valeur;
                if (nUnite < strUnitesCorrigées.Count - 1)
                {
                    valeurTmp = valeurTmp - (double)nVal;
                    if (nVal > 0)
                        bl.Append(nVal + strLib + " ");
                    if (valeurTmp.Valeur == 0)
                        break;
                }
                else
                {
                    if (valeurTmp.Valeur != 0)
                        bl.Append(Math.Round(valeurTmp.Valeur, 5).ToString() + strLib);
                }
            }
            if (Valeur < 0)
                bl.Insert(0, "-");
            return bl.ToString();
        }

        

        //------------------------------------
        public static string GetFormat(string strChaine)
        {
            StringBuilder bl = new StringBuilder();
            foreach (CValeurUnite valeur in DecomposeChaineFormattée(strChaine))
            {
                if (valeur != null)
                {
                    bl.Append(valeur.Unite);
                    bl.Append(" ");
                }
            }
            if (bl.Length > 0)
                bl.Remove(bl.Length - 1, 1);
            return bl.ToString();
        }

        //------------------------------------
        public static CValeurUnite FromString(string strChaine)
        {
            List<CValeurUnite> valeurs = DecomposeChaineFormattée(strChaine);
            bool bIsNegatif = false;
            if (valeurs.Count > 0 && valeurs[0].Valeur < 0)
            {
                bIsNegatif = true;
                valeurs[0].Valeur = -valeurs[0].Valeur;
            }
            CValeurUnite valeurFinale = null;
            foreach (CValeurUnite valeur in valeurs)
            {
                if (valeurFinale == null)
                    valeurFinale = valeur;
                else
                    valeurFinale += valeur;
            }
            if (bIsNegatif)
                valeurFinale.Valeur = -valeurFinale.Valeur;
            return valeurFinale;

        }

        //--------------------------------------------------------------------------------
        public static List<CValeurUnite> DecomposeChaineFormattée(string strChaine)
        {
            List<CValeurUnite> valeurs = new List<CValeurUnite>();
            string strVal = "";
            string strU = "";
            //Si - au début, suppression et note le premier élément en -
            bool bIsNegatif = false;
            strChaine = strChaine.Trim();
            if (strChaine.Length > 0 && strChaine[0] == '-')
            {
                bIsNegatif = true;
                strChaine = strChaine.Substring(1);
            }
            foreach (char c in strChaine)
            {
                if ("0123456789,.-+eE".IndexOf(c) >= 0 || c == ' ')
                {
                    if (strU != "")
                    {
                        if (strVal.Trim().Length > 0)
                            valeurs.Add(new CValeurUnite(CUtilDouble.DoubleFromString(strVal), strU.Trim()));
                        strU = "";
                        strVal = "";
                    }
                    if (c != ' ')
                        strVal += c;
                }
                else if (c != ' ')
                    strU += c;
            }
            if (strVal.Trim().Length > 0)
                valeurs.Add(new CValeurUnite(CUtilDouble.DoubleFromString(strVal), strU.Trim()));
            if (bIsNegatif && valeurs.Count > 0)
                valeurs[0].Valeur = -valeurs[0].Valeur;
            return valeurs;
        }

        //------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteDouble ( ref m_fValeur );
            serializer.TraiteString ( ref m_strUnite );
            serializer.TraiteString ( ref m_strFormat );
            return result;
        }






        //------------------------------------
        public static CValeurUnite operator +(CValeurUnite valeur1, CValeurUnite valeur2)
        {
            CValeurUnite valeur = valeur2.ConvertTo(valeur1.Unite);
            return new CValeurUnite(valeur1.Valeur + valeur.Valeur, valeur1.Unite, valeur1.Format);
        }

        //------------------------------------
        public static CValeurUnite operator +(CValeurUnite valeur1, double fValeur)
        {
            return new CValeurUnite(valeur1.Valeur + fValeur, valeur1.Unite, valeur1.Format);
        }

        //------------------------------------
        public static CValeurUnite operator +(double fValeur, CValeurUnite valeur)
        {
            return valeur + fValeur;
        }

        //------------------------------------
        public static CValeurUnite operator -(CValeurUnite valeur1, CValeurUnite valeur2)
        {
            CValeurUnite valeur = valeur2.ConvertTo(valeur1.Unite);
            return new CValeurUnite(valeur1.Valeur - valeur.Valeur, valeur1.Unite, valeur1.Format);
        }

        //------------------------------------
        public static CValeurUnite operator -(CValeurUnite valeur, double fValeur)
        {
            return new CValeurUnite(valeur.Valeur - fValeur, valeur.Unite, valeur.Format);
        }

        //------------------------------------
        public static CValeurUnite operator -(double fValeur, CValeurUnite valeur)
        {
            return new CValeurUnite(fValeur - valeur.Valeur , valeur.Unite, valeur.Format);
        }

        //------------------------------------
        public static CValeurUnite operator *(CValeurUnite valeur1, CValeurUnite valeur2)
        {
            string strU2 = CUtilUnite.GetUniteHarmonisee(valeur1.Unite, valeur2.Unite);
            if ( strU2 != valeur2.Unite )
                valeur2 = valeur2.ConvertTo(strU2);
            string[] strCompos = CUtilUnite.Developpe(valeur1.Unite + "." + valeur2.Unite);
            string strUnite = CUtilUnite.Factorise(strCompos);
            CValeurUnite valeur = new CValeurUnite(valeur1.Valeur * valeur2.Valeur, strUnite);
            return valeur;
        }

        //------------------------------------
        public static CValeurUnite operator *(CValeurUnite valeur, double fValeur)
        {
            return new CValeurUnite(valeur.Valeur * fValeur, valeur.Unite);
        }

        //------------------------------------
        public static CValeurUnite operator *(double fValeur, CValeurUnite valeur)
        {
            return valeur * fValeur;
        }

        //------------------------------------
        public static CValeurUnite operator /(CValeurUnite valeur1, CValeurUnite valeur2)
        {
            valeur2 = valeur2.GetValeurInverse();
            return valeur1 * valeur2;
        }

        //------------------------------------
        public static CValeurUnite operator /(CValeurUnite valeur, double fValeur)
        {
            return new CValeurUnite(valeur.Valeur / fValeur, valeur.Unite);
        }

        //------------------------------------
        public static CValeurUnite operator /(double fValeur, CValeurUnite valeur)
        {
            return valeur.GetValeurInverse() * fValeur;
        }


        #region IConvertible Membres
        //------------------------------------
        public TypeCode GetTypeCode()
        {
            return TypeCode.Double;
        }

        //------------------------------------
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        //------------------------------------
        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        //------------------------------------
        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        //------------------------------------
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        //------------------------------------
        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Valeur);
        }

        //------------------------------------
        public double ToDouble(IFormatProvider provider)
        {
            return Valeur;
        }

        //------------------------------------
        public short ToInt16(IFormatProvider provider)
        {
            return (Int16)Valeur;
        }

        //------------------------------------
        public int ToInt32(IFormatProvider provider)
        {
            return (Int32)Valeur;
        }

        //------------------------------------
        public long ToInt64(IFormatProvider provider)
        {
            return (Int64)Valeur;
        }

        //------------------------------------
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        //------------------------------------
        public float ToSingle(IFormatProvider provider)
        {
            return (float)Valeur;
        }

        //------------------------------------
        public string ToString(IFormatProvider provider)
        {
            return ToString();
        }

        //------------------------------------
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(double))
                return Valeur;
            if (conversionType == typeof(float))
                return (float)Valeur;
            if (conversionType == typeof(int))
                return (int)Valeur;
            if (conversionType == typeof(Int16))
                return (Int16)Valeur;
            if (conversionType == typeof(Int32))
                return (Int32)Valeur;
            if (conversionType == typeof(Int64))
                return (Int64)Valeur;
            if (conversionType == typeof(UInt16))
                return (UInt16)Valeur;
            if (conversionType == typeof(UInt32))
                return (UInt32)Valeur;
            if (conversionType == typeof(UInt64))
                return (UInt64)Valeur;
            return null;
        }

        //------------------------------------
        public ushort ToUInt16(IFormatProvider provider)
        {
            return (UInt16)Valeur;
        }

        //------------------------------------
        public uint ToUInt32(IFormatProvider provider)
        {
            return (UInt32)Valeur;
        }

        //------------------------------------
        public ulong ToUInt64(IFormatProvider provider)
        {
            return (UInt64)Valeur;
        }

        #endregion
    }
}
