using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;

namespace sc2i.win32.data
{
    public interface IFournisseurFiltreAdditionnel
    {
        CFiltreData GetFiltreForType(Type tp);
    }


    public static class CFiltreInterfaceExterne
    {
        private static List<IFournisseurFiltreAdditionnel> m_listeFournisseursFiltresAdditionnels = new List<IFournisseurFiltreAdditionnel>();

        public static void RegisterFournisseur(IFournisseurFiltreAdditionnel fournisseur)
        {
            if (!m_listeFournisseursFiltresAdditionnels.Contains(fournisseur))
                m_listeFournisseursFiltresAdditionnels.Add(fournisseur);
        }

        public static void UnregisterFournisseur(IFournisseurFiltreAdditionnel fournisseur)
        {
            m_listeFournisseursFiltresAdditionnels.Remove(fournisseur);
        }

        public static CFiltreData GetFiltreAdditionnel(Type tp)
        {
            CFiltreData filtre = null;
            foreach (IFournisseurFiltreAdditionnel fournisseur in m_listeFournisseursFiltresAdditionnels)
            {
                try
                {
                    CFiltreData filtreTmp = fournisseur.GetFiltreForType(tp);
                    if (filtreTmp != null && filtreTmp.HasFiltre)
                        filtre = CFiltreData.GetAndFiltre(filtre, filtreTmp);
                }
                catch { }
            }
            return filtre;
        }

    }
}
