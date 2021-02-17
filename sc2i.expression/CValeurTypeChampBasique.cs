using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
    /// <summary>
    /// Represente une valeur pour un champ de type basique
    /// </summary>
    [Serializable]
    public class CValeurTypeChampBasique : I2iSerializable
    {
        private object m_valeur = null;

        //--------------------------------------
        public CValeurTypeChampBasique()
        {
        }

        //--------------------------------------
        public CValeurTypeChampBasique(object valeur)
        {
            m_valeur = valeur;
        }

        //--------------------------------------
        public static implicit operator int?(CValeurTypeChampBasique valeur)
        {
            return valeur.m_valeur as int?;
        }

        //--------------------------------------
        public static implicit operator string(CValeurTypeChampBasique valeur)
        {
            return valeur.m_valeur as string;
        }

        //--------------------------------------
        public static implicit operator double?(CValeurTypeChampBasique valeur)
        {
            return valeur.m_valeur as double?;
        }

        //--------------------------------------
        public static implicit operator bool?(CValeurTypeChampBasique valeur)
        {
            return valeur.m_valeur as bool?;
        }

        //--------------------------------------
        public static implicit operator DateTime?(CValeurTypeChampBasique valeur)
        {
            return valeur.m_valeur as DateTime?;
        }
        
        //--------------------------------------
        public static implicit operator CValeurTypeChampBasique(string strValeur)
        {
            return new CValeurTypeChampBasique(strValeur);
        }

        //--------------------------------------
        public static implicit operator CValeurTypeChampBasique(int? nValeur)
        {
            return new CValeurTypeChampBasique(nValeur);
        }

        //--------------------------------------
        public static implicit operator CValeurTypeChampBasique(double? nValeur)
        {
            return new CValeurTypeChampBasique(nValeur);
        }

        //--------------------------------------
        public static implicit operator CValeurTypeChampBasique(bool? nValeur)
        {
            return new CValeurTypeChampBasique(nValeur);
        }

        //--------------------------------------
        public static implicit operator CValeurTypeChampBasique(DateTime? nValeur)
        {
            return new CValeurTypeChampBasique(nValeur);
        }

        //--------------------------------------
        public string StringValue
        {
            get
            {
                return (string)this;
            }
            set
            {
                this.m_valeur = value;
            }
        }

        //--------------------------------------
        public int? IntValue
        {
            get
            {
                return (int?)this;
            }
            set
            {
                this.m_valeur = value;
            }
        }

        //--------------------------------------
        public double? DoubleValue
        {
            get
            {
                return (double?)this;
            }
            set
            {
                this.m_valeur = value;
            }
        }

        //--------------------------------------
        public DateTime? DateTimeValue
        {
            get{
                return (DateTime?)this;
            }
            set{
                this.m_valeur = value;
            }
        }

        //--------------------------------------
        public bool? BoolValue
        {
            get
            {
                return (bool?)this;
            }
            set
            {
                this.m_valeur = value;
            }
        }

        //--------------------------------------
        public object Value
        {
            get
            {
                return m_valeur;
            }
            set
            {
                if (value is string ||
                    value is int ||
                    value is double ||
                    value is bool ||
                    value is DateTime)
                    m_valeur = value;
                else
                    m_valeur = null;
            }
        }
                

        //--------------------------------------
        public ETypeChampBasique TypeDonnee
        {
            get{
                if ( m_valeur is string )
                    return ETypeChampBasique.String;
                if ( m_valeur is int )
                    return ETypeChampBasique.Int;
                if ( m_valeur is double)
                    return ETypeChampBasique.Date;
                if ( m_valeur is bool )
                    return ETypeChampBasique.Bool;
                if ( m_valeur is DateTime )
                    return ETypeChampBasique.Date;
                return ETypeChampBasique.String;
            }
        }

        //--------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }
        


        //--------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            byte nTmp = (byte)TypeDonnee;
            if (m_valeur == null)
                nTmp = byte.MaxValue;
            serializer.TraiteByte(ref nTmp);
            if (nTmp != byte.MaxValue)
            {
                ETypeChampBasique typeDonnee = (ETypeChampBasique)nTmp;
                switch (typeDonnee)
                {
                    case ETypeChampBasique.String:
                        string strVal = StringValue;
                        serializer.TraiteString(ref strVal);
                        StringValue = strVal;
                        break;
                    case ETypeChampBasique.Int:
                        int nVal = IntValue == null ? 0 : IntValue.Value;
                        serializer.TraiteInt(ref nVal);
                        IntValue = nVal;

                        break;
                    case ETypeChampBasique.Decimal:
                        double fVal = DoubleValue == null ? 0.0 : DoubleValue.Value;
                        serializer.TraiteDouble(ref fVal);
                        DoubleValue = fVal;
                        break;
                    case ETypeChampBasique.Date:
                        DateTime dtValue = DateTimeValue == null ? default(DateTime) : DateTimeValue.Value;
                        serializer.TraiteDate(ref dtValue);
                        DateTimeValue = dtValue;
                        break;
                    case ETypeChampBasique.Bool:
                        bool bVal = BoolValue == null ? default(bool) : BoolValue.Value;
                        serializer.TraiteBool(ref bVal);
                        BoolValue = bVal;
                        break;
                }
            }
            return result;
        }







    

        
    }
}
