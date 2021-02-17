using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.unites.standard;

namespace sc2i.common.unites
{
    public class CGestionnaireUnites
    {
        private static CGestionnaireUnites m_instance = null;

        private List<IClasseUnite> m_classes = new List<IClasseUnite>();
        private List<IUnite> m_unites = new List<IUnite>();

        //--------------------------------------
        public static EventHandler OnInitGestionnaireUnites;

        //--------------------------------------
        private CGestionnaireUnites()
        {
        }

        //--------------------------------------
        private static CGestionnaireUnites Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new CGestionnaireUnites();
                    CClasseUniteDistance.RegisterUnites();
                    CClasseUnitePoids.RegisterUnites();
                    CClasseUniteVolume.RegisterUnites();
                    CClasseUniteTemps.RegisterUnites();
                    CClasseUniteAngle.RegisterUnites();
                    CClasseUniteUnite.RegisterUnites();
                    if (OnInitGestionnaireUnites != null)
                        OnInitGestionnaireUnites(m_instance, null);
                }
                return m_instance;
            }
        }

        //--------------------------------------
        public static void Refresh()
        {
            m_instance = null;
        }

        //--------------------------------------
        public static void AddClasseUnite(IClasseUnite classe)
        {
            if (Instance.m_classes.FirstOrDefault(c => c.GlobalId == classe.GlobalId) == null)
                Instance.m_classes.Add(classe);
        }

        //--------------------------------------
        public static void RemoveClasse(IClasseUnite classe)
        {
            Instance.m_classes.Remove(classe);
        }

        //--------------------------------------
        public static IEnumerable<IClasseUnite> Classes
        {
            get
            {
                return Instance.m_classes.AsReadOnly();
            }
        }

        //--------------------------------------
        public static IClasseUnite GetClasse(string strId)
        {
            return Instance.m_classes.FirstOrDefault(c => c.GlobalId == strId);
        }

        //--------------------------------------
        public static void AddUnite(IUnite unite)
        {
            if (Instance.m_unites.FirstOrDefault(u => u.GlobalId == unite.GlobalId) == null)
                Instance.m_unites.Add(unite);
        }

        //--------------------------------------
        public static void RemoveUnite(IUnite unite)
        {
            Instance.m_unites.Remove(unite);
        }

        //--------------------------------------
        public static IEnumerable<IUnite> GetUnites(IClasseUnite classe)
        {
            if (classe == null)
                return new IUnite[0];
            IEnumerable<IUnite> lst = from u in Instance.m_unites
                                      where
                                          u.Classe.GlobalId == classe.GlobalId
                                      select u;
            return lst;
        }

        //--------------------------------------
        public static IEnumerable<IUnite> Unites
        {
            get
            {
                return Instance.m_unites.AsReadOnly();
            }
        }

        //--------------------------------------
        public static IUnite GetUnite(string strId)
        {
            if (strId == null)
                return null;
            IUnite unite = Instance.m_unites.FirstOrDefault(u => u.GlobalId == strId);
            if (unite == null)
                unite = Instance.m_unites.FirstOrDefault(u => u.LibelleCourt.ToUpper() == strId.ToUpper());
            return unite;
        }
    }
}
