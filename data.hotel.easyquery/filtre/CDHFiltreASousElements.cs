using data.hotel.client.query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc2i.common;
using futurocom.easyquery;

namespace data.hotel.easyquery.filtre
{
    public abstract class CDHFiltreASousElements : IDHFiltre

    {private List<IDHFiltre> m_listeSousFiltres = new List<IDHFiltre>();

        //--------------------------------------------------
        public CDHFiltreASousElements()
        {

        }

        //--------------------------------------------------
        public abstract string GetLibelle(IObjetDeEasyQuery table);


        //--------------------------------------------------
        public void ClearSousElements()
        {
            m_listeSousFiltres.Clear();
        }
        //--------------------------------------------------
        public IEnumerable<IDHFiltre> SousElements
        {
            get
            {
                return m_listeSousFiltres.AsReadOnly();
            }
            set
            {
                List<IDHFiltre> lst = new List<IDHFiltre>();
                if (value != null)
                    lst.AddRange(value);
                m_listeSousFiltres = lst;
            }
        }

        //--------------------------------------------------
        public void AddSousElement ( IDHFiltre sousElement)
        {
            m_listeSousFiltres.Add(sousElement);
        }

        //--------------------------------------------------
        public void RemoveSousElement ( IDHFiltre sousElement )
        {
            m_listeSousFiltres.Remove(sousElement);
        }


        //--------------------------------------------------
        protected abstract CTestDataHotelASousElements AlloueTestFinal();


        //--------------------------------------------------
        public virtual ITestDataHotel GetTestFinal(object objetPourSousProprietes )
        {
            CTestDataHotelASousElements testFinal = AlloueTestFinal();
            if ( testFinal == null )
                return null;
            List<ITestDataHotel> lstSousTests = new List<ITestDataHotel>();
            foreach ( IDHFiltre dg in SousElements )
            {
                ITestDataHotel test = dg.GetTestFinal(objetPourSousProprietes);
                if (test != null)
                    lstSousTests.Add(test);
            }
            if (lstSousTests.Count == 0)
                return null;
            testFinal.SousTests = lstSousTests;
            return testFinal;
        }
        

        //--------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------
        public virtual CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = serializer.TraiteListe<IDHFiltre>(m_listeSousFiltres);
            if ( !result )
                return result;
            return result;
        }
   

    }
}