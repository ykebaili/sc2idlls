using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;

namespace sc2i.process.workflow
{
    //-----------------------------------------------------
    public interface IAffectableAEtape : IObjetDonneeAIdNumeriqueAuto
    {
        string RacineCleAffectationWorkflow { get; }
    }

    //-----------------------------------------------------
    public class CAffectationsEtapeWorkflow
    {
        private const char c_strSepCleId = '#';
        private const char c_strSepGen = '~';
        private const string c_strBadKey = "NONO";

        private List<string> m_listeStringAffectations = new List<string>();

        private List<IAffectableAEtape> m_listeAffectables = null;

        public CAffectationsEtapeWorkflow()
        {
        }

        private CAffectationsEtapeWorkflow(string strCode)
        {
        }

        public static CAffectationsEtapeWorkflow FromCode(string strCode)
        {
            CAffectationsEtapeWorkflow aff = new CAffectationsEtapeWorkflow();
            string[] strCodes = strCode.Split(c_strSepGen);
            foreach (string strCle in strCodes)
            {
                if (strCle.Trim() != "")
                    aff.m_listeStringAffectations.Add(strCle);
            }
            return aff;
        }


        private static Dictionary<string, Type> m_dicRacineCleToAffectation = new Dictionary<string, Type>();

        //--------------------------------------------------------------------------
        public static void RegisterAffectable(string strCode, Type typeAffectable)
        {
            m_dicRacineCleToAffectation[strCode.ToUpper()] = typeAffectable;
        }

        //--------------------------------------------------------------------------
        public static Type[] GetTypesAffectables()
        {
            return m_dicRacineCleToAffectation.Values.ToArray();
        }


        //--------------------------------------------------------------------------
        public static string GetCleAffectation(IAffectableAEtape affectable)
        {
            if (affectable != null)
            {
                return (affectable.RacineCleAffectationWorkflow + c_strSepCleId + affectable.Id).ToUpper();
            }
            return c_strBadKey;
        }

        //--------------------------------------------------------------------------
        public static bool DecomposeCleAffectation(string strCle, out string strCode, out int nIdObjet)
        {
            string[] strVals = strCle.Split(c_strSepCleId);
            strCode = "";
            nIdObjet = -1;
            if (strVals.Length != 2)
                return false;
            strCode = strVals[0];
            try
            {
                nIdObjet = Int32.Parse(strVals[1]);
                return true;
            }
            catch { }
            return false;
        }


        //--------------------------------------------------------------------------
        public void AddAffectable(IAffectableAEtape affectable)
        {
            string strCle = GetCleAffectation(affectable);
            if (strCle != c_strBadKey && !m_listeStringAffectations.Contains(strCle))
            {
                m_listeStringAffectations.Add(strCle);
                m_listeAffectables = null;
            }
        }

        //--------------------------------------------------------------------------
        public void RemoveAffectable(IAffectableAEtape affectable)
        {
            string strCle = GetCleAffectation(affectable);
            if (m_listeStringAffectations.Contains(strCle))
            {
                m_listeStringAffectations.Remove(strCle);
                m_listeAffectables = null;
            }
        }

        //--------------------------------------------------------------------------
        public IEnumerable<IAffectableAEtape> GetAffectables()
        {
            return GetAffectables(CContexteDonneeSysteme.GetInstance());
        }

        //--------------------------------------------------------------------------
        public IEnumerable<IAffectableAEtape> GetAffectables ( CContexteDonnee contexte )
        {
            if (m_listeAffectables != null)
                return m_listeAffectables.AsReadOnly();
            m_listeAffectables = new List<IAffectableAEtape>();
            foreach (string strCle in m_listeStringAffectations)
            {
                IAffectableAEtape aff = GetAffectable(strCle, contexte);
                if (aff != null)
                    m_listeAffectables.Add(aff);
            }
            return m_listeAffectables.AsReadOnly();
        }

        //--------------------------------------------------------------------------
        public static IAffectableAEtape GetAffectable(string strCleAffectation, CContexteDonnee contexte)
        {
            string strCode = "";
            int nId = 0;
            if (!DecomposeCleAffectation(strCleAffectation, out strCode, out nId))
                return null;
            Type tpObjet = null;
            if (!m_dicRacineCleToAffectation.TryGetValue(strCode, out tpObjet))
                return null;
            IAffectableAEtape affectable = Activator.CreateInstance(tpObjet, new object[] { contexte }) as IAffectableAEtape;
            if (affectable != null &&
                affectable.ReadIfExists(nId))
                return affectable;
            return null;
        }

        //--------------------------------------------------------------------------
        public string GetCodeString()
        {
            StringBuilder bl = new StringBuilder();
            foreach (string strCode in m_listeStringAffectations)
            {
                bl.Append(c_strSepGen);
                bl.Append(strCode);
                bl.Append(c_strSepGen);
            }
            return bl.ToString();
        }

        //--------------------------------------------------------------------------
        public bool Contains(string strCode)
        {
            if (m_listeStringAffectations != null)
                return m_listeStringAffectations.Contains(strCode);
            return false ;
        }
            
    }

}
