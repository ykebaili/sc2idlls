using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.periodeactivation
{
    [Serializable]
    public class CPeriodeActivationEt : IPeriodeActivationMultiple
    {
        private List<IPeriodeActivation> m_listePeriodes = new List<IPeriodeActivation>();

        //----------------------------------------------
        public CPeriodeActivationEt()
        {
        }

        //----------------------------------------------
        public string Libelle
        {
            get
            {
                return GetLibelleType();
                /*StringBuilder bl = new StringBuilder();
                if (m_listePeriodes.Count == 0)
                    return GetLibelleType();
                foreach (IPeriodeActivation periode in Periodes)
                {
                    bl.Append('(');
                    bl.Append(periode.Libelle);
                    bl.Append(')');
                    bl.Append(' ');
                    bl.Append(GetLibelleType());
                    bl.Append(' ');
                }
                bl.Remove(bl.Length - 2 - GetLibelleType().Length, 2 + GetLibelleType().Length);
                return bl.ToString();*/
            }
        }

        //----------------------------------------------
        public string GetLibelleType()
        {
            return I.T("And|20013");
        }

        //----------------------------------------------
        public void AddPeriode(IPeriodeActivation periode)
        {
            m_listePeriodes.Add(periode);
        }

        //----------------------------------------------
        public void RemovePeriode(IPeriodeActivation periode)
        {
            m_listePeriodes.Remove(periode);
        }

        //----------------------------------------------
        public IEnumerable<IPeriodeActivation> Periodes
        {
            get
            {
                return m_listePeriodes.AsReadOnly();
            }
        }

        //----------------------------------------------
        public void ClearPeriodes()
        {
            m_listePeriodes.Clear();
        }

        //----------------------------------------------
        public bool IsInPeriode(DateTime dt)
        {
            foreach (IPeriodeActivation periode in m_listePeriodes)
            {
                if (!periode.IsInPeriode(dt))
                    return false;
            }
            return true;
        }

        //----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<IPeriodeActivation>(m_listePeriodes);
            return result;
        }

    }
}
