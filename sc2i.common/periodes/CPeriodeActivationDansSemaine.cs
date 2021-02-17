using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.periodeactivation
{
    [Serializable]
    public class CPeriodeActivationDansSemaine : IPeriodeActivation
    {
        private JoursBinaires m_joursActivation = JoursBinaires.Lundi |
            JoursBinaires.Mardi |
            JoursBinaires.Mercredi |
            JoursBinaires.Jeudi |
            JoursBinaires.Vendredi |
            JoursBinaires.Samedi |
            JoursBinaires.Dimanche;

        public CPeriodeActivationDansSemaine()
        {
        }

        //--------------------------------
        public string GetLibelleType()
        {
            return I.T("Days|20012");
        }

        //--------------------------------
        public string Libelle
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                if ((JoursActivation & JoursBinaires.Lundi) == JoursBinaires.Lundi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Monday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Mardi) == JoursBinaires.Mardi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Tuesday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Mercredi) == JoursBinaires.Mercredi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Wednesday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Jeudi) == JoursBinaires.Jeudi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Thursday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Vendredi) == JoursBinaires.Vendredi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Friday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Samedi) == JoursBinaires.Samedi)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Saturday, true));
                    bl.Append(',');
                }
                if ((JoursActivation & JoursBinaires.Dimanche) == JoursBinaires.Dimanche)
                {
                    bl.Append(CUtilDate.GetNomJour(DayOfWeek.Sunday, true));
                    bl.Append(',');
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                return bl.ToString();
            }
        }
                

        //--------------------------------------------
        public JoursBinaires JoursActivation
        {
            get
            {
                return m_joursActivation;
            }
            set
            {
                m_joursActivation = value;
            }
        }

        //--------------------------------------------
        public bool IsInPeriode(DateTime dt)
        {
            JoursBinaires j =  CUtilDate.GetJourBinaireFor(dt.DayOfWeek);
            return (m_joursActivation & j) == j;
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
            int nJour = (int)m_joursActivation;
            serializer.TraiteInt(ref nJour);
            m_joursActivation = (JoursBinaires)nJour;
            return result;
        }
    }
}
