using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.chart;
using sc2i.common;
using sc2i.expression;

namespace futurocom.win32.chart.sources
{
    public interface IFormEditSourceChart
    {
        void InitChamps ( CParametreSourceChart parametre, 
            IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes typeSourceGlobale);
    }


    public static class CEditeurSourceChart
    {
        private static Dictionary<Type, Type> m_dicTypeSourceToEditeur = new Dictionary<Type, Type>();

        //-------------------------------------------------------
        public static void RegisterEditeur(Type typeSource, Type typeEditeur)
        {
            m_dicTypeSourceToEditeur[typeSource] = typeEditeur;
        }

        //-------------------------------------------------------
        public static Type GetTypeFormEditeur(CParametreSourceChart parametre)
        {
            if (parametre == null)
                return null;
            Type typeRetour = null;
            m_dicTypeSourceToEditeur.TryGetValue(parametre.GetType(), out typeRetour);
            return typeRetour;
        }
    }
}
