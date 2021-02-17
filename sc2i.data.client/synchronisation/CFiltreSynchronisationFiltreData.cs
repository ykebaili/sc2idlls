using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data.synchronisation
{
    public class CFiltreSynchronisationFiltreData : IFiltreSynchronisation
    {
        private CFiltreData m_filtre = null;

        //------------------------------------------------
        public CFiltreSynchronisationFiltreData()
        {
        }

        //------------------------------------------------
        public CFiltreSynchronisationFiltreData(CFiltreData filtre)
        {
            m_filtre = filtre;
        }

        //------------------------------------------------
        public CFiltreData GetFiltreData(int nIdSession, CFiltresSynchronisation filtres)
        {
            return m_filtre;
        }

        //------------------------------------------------
        public void SetFiltreData(CFiltreData filtre)
        {
            m_filtre = filtre;
        }

    }
}
