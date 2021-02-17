using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.DonneeCumulee
{
    [Serializable]
    public class CDonneeCumuleeTransportable
    {
        private Dictionary<int, string> m_dicValeursCles = new Dictionary<int, string>();
        private Dictionary<int, double?> m_dicValeursDoubles = new Dictionary<int, double?>();
        private Dictionary<int, DateTime?> m_dicValeursDates = new Dictionary<int, DateTime?>();
        private Dictionary<int, string> m_dicValeursTexte = new Dictionary<int, string>();

        private int? m_nIdTypeDonneeCumulee = null;

        //-----------------------------------------------------------
        public CDonneeCumuleeTransportable()
        {
        }

        //-----------------------------------------------------------
        public IEnumerable<CChampDonneeCumulee> GetChampsDefinis()
        {
            List<CChampDonneeCumulee> lst = new List<CChampDonneeCumulee>();
            foreach ( KeyValuePair<int, string> kv in m_dicValeursCles )
                if ( kv.Value != null )
                    lst.Add(new CChampDonneeCumulee(ETypeChampDonneeCumulee.Cle, kv.Key));

            foreach ( KeyValuePair<int, double?> kv in m_dicValeursDoubles)
            if (kv.Value != null )
                lst.Add(new CChampDonneeCumulee(ETypeChampDonneeCumulee.Decimal, kv.Key));

            foreach ( KeyValuePair<int, DateTime?> kv in m_dicValeursDates)
            if (kv.Value != null )
                lst.Add(new CChampDonneeCumulee(ETypeChampDonneeCumulee.Date, kv.Key));

            foreach ( KeyValuePair<int, string> kv in m_dicValeursTexte)
            if (kv.Value != null )
                lst.Add(new CChampDonneeCumulee(ETypeChampDonneeCumulee.Texte, kv.Key));

            return lst.ToArray();
        }

        //-----------------------------------------------------------
        public object GetValeurChamp(CChampDonneeCumulee champ)
        {
            string strVal = null;
            switch (champ.TypeChamp)
            {
                case ETypeChampDonneeCumulee.Cle:
                    m_dicValeursCles.TryGetValue(champ.NumeroChamp, out strVal);
                    return strVal;
                case ETypeChampDonneeCumulee.Decimal:
                    double? fVal = null;
                    m_dicValeursDoubles.TryGetValue(champ.NumeroChamp, out fVal);
                    return fVal;
                case ETypeChampDonneeCumulee.Date:
                    DateTime? dVal = null;
                    m_dicValeursDates.TryGetValue(champ.NumeroChamp, out dVal);
                    return dVal;
                case ETypeChampDonneeCumulee.Texte:
                    m_dicValeursTexte.TryGetValue(champ.NumeroChamp, out strVal);
                    return strVal;
                default:
                    break;
            }
            return null;
        }

            


        //-----------------------------------------------------------
        public void SetValeurCle(int nCle, string strValeur)
        {
            m_dicValeursCles[nCle] = strValeur;
        }

        //-----------------------------------------------------------
        public string GetValeurCle(int nCle)
        {
            string strValeur = null;
            m_dicValeursCles.TryGetValue(nCle, out strValeur);
            return strValeur;
        }

        //-----------------------------------------------------------
        public void SetValeurDouble(int nNum, double? fValeur)
        {
            m_dicValeursDoubles[nNum] = fValeur;
        }

        //-----------------------------------------------------------
        public double? GetValeurDouble(int nNum)
        {
            double? fValeur = null;
            m_dicValeursDoubles.TryGetValue(nNum, out fValeur);
            return fValeur;
        }

        //-----------------------------------------------------------
        public void SetValeurDate(int nNum, DateTime? dt)
        {
            m_dicValeursDates[nNum] = dt;
        }

        //-----------------------------------------------------------
        public DateTime? GetValeurDate(int nNum)
        {
            DateTime? dt = null;
            m_dicValeursDates.TryGetValue(nNum, out dt);
            return dt;
        }

        //-----------------------------------------------------------
        public void SetValeurString(int nNum, string strValeur)
        {
            m_dicValeursTexte[nNum] = strValeur;
        }

        //-----------------------------------------------------------
        public string GetValeurString(int nNum)
        {
            string strValeur = null;
            m_dicValeursTexte.TryGetValue(nNum, out strValeur);
            return strValeur;
        }

        //-----------------------------------------------------------
        public int? IdTypeDonneeCumulee
        {
            get
            {
                return m_nIdTypeDonneeCumulee;
            }
            set
            {
                m_nIdTypeDonneeCumulee = value;
            }
        }
    }
}
