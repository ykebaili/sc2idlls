using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.chart;
using sc2i.common;

namespace futurocom.win32.chart.series
{
    //---------------------------------------------------------------------------------------
    public interface IEditeurFournisseurValeursSerieDeTypeConnu
    {
        void InitChamps(CChartSetup chartSetup, IFournisseurValeursSerie fournisseur);

        CResultAErreur MajChamps();
    }
        
    //--------------------------------------------------------------------------------------- 
    public static class CGestionnaireEditeursFournisseursValeurs
    {
        private static Dictionary<Type, Type> m_dicTypesEditeurs = new Dictionary<Type, Type>();

        public static void RegisterEditeur(Type typeFournisseur, Type typeEditeur)
        {
            m_dicTypesEditeurs[typeFournisseur] = typeEditeur;
        }

        public static Type GetTypeEditeur(Type typeFournisseur)
        {
            Type typeEditeur = null;
            m_dicTypesEditeurs.TryGetValue(typeFournisseur, out typeEditeur);
            return typeEditeur;
        }
    }
}
