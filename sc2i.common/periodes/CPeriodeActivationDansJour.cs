using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.periodeactivation
{
    [Serializable]
    public class CPeriodeActivationDansJour : IPeriodeActivation
    {
        private double m_fHeureDebut = 0;
        private double m_fHeureFin = 23.99;

        //--------------------------------
        public CPeriodeActivationDansJour()
        {
        }

        //--------------------------------
        public string GetLibelleType()
        {
            return I.T("Hours|20011");
        }

        //--------------------------------
        private static string GetHeure ( double fValeur )
        {
            string strVal = ((int)fValeur).ToString("00");
            strVal += ((int)(fValeur - (int)fValeur)*100).ToString(":00");
            return strVal;
        }

        //--------------------------------
        public string Libelle
        {
            get
            {
                return I.T("from @1 to @2|20015", GetHeure(m_fHeureDebut), GetHeure(m_fHeureFin));
            }
        }
                    

        //--------------------------------
        public double HeureDebut
        {
            get
            {
                return m_fHeureDebut;
            }
            set
            {
                m_fHeureDebut = value;
            }
        }

        //--------------------------------
        public double HeureFin
        {
            get
            {
                return m_fHeureFin;
            }
            set
            {
                m_fHeureFin = value;
            }
        }

        //--------------------------------
        public bool IsInPeriode(DateTime dt)
        {
            double fHeure = (double)dt.Hour + ((double)dt.Minute) / 60.0;
            if (m_fHeureDebut < m_fHeureFin)//Periode d'une même journée
                return fHeure >= m_fHeureDebut && fHeure <= m_fHeureFin;
            else
            {
                if (fHeure > m_fHeureDebut)
                    return true;
                return fHeure  < m_fHeureFin;
            }
        }

        //-----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteDouble(ref m_fHeureDebut);
            serializer.TraiteDouble(ref m_fHeureFin);
            return result;
        }
    }
}
