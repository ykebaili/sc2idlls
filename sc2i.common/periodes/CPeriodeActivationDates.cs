using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.periodeactivation
{
    public class CPeriodeActivationDates : IPeriodeActivation
    {
        private DateTime m_dateDebut = DateTime.Now.Date;
        private DateTime m_dateFin = DateTime.Now.Date;

        /// ---------------------------------------------
        public CPeriodeActivationDates()
        { 
        }

        //--------------------------------
        public string GetLibelleType()
        {
            return I.T("Dates|20010");
        }

        //--------------------------------
        public string Libelle
        {
            get
            {
                return I.T("from @1 to @2|20015", m_dateDebut.ToShortDateString(),
                    m_dateFin.ToShortDateString());
            }
        }


        //---------------------------------------------
        public DateTime DateDebut
        {
            get
            {
                return m_dateDebut;
            }
            set
            {
                m_dateDebut = value.Date;
            }
        }

        //---------------------------------------------
        public DateTime DateFin
        {
            get
            {
                return m_dateFin;
            }
            set
            {
                m_dateFin = value.Date;
            }
        }

        //---------------------------------------------
        public bool IsInPeriode(DateTime dt)
        {
            return dt >= m_dateDebut && dt < m_dateFin.AddDays(1);
        }

        //---------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteDate ( ref m_dateDebut );
            serializer.TraiteDate ( ref m_dateFin );
            return result;
        }
    }
}
