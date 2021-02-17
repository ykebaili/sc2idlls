using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.formulaire.datagrid;

namespace sc2i.formulaire.win32.datagrid.Filtre
{
    public interface IEditeurFiltreGrid
    {
        bool EditeFiltre(CGridFilterForWndDataGrid filtre);
    }

    public static class CGestionnaireEditeurFiltres
    {
        private static Dictionary<Type, Type> m_dicTypeFiltreToTypeEditeur = new Dictionary<Type, Type>();

        public static void RegisterEditeur(Type typeFiltre, Type typeEditeur)
        {
            m_dicTypeFiltreToTypeEditeur[typeFiltre] = typeEditeur;
        }

        public static IEditeurFiltreGrid GetEditeur(Type typeFiltre)
        {
            Type typeEditeur = null;
            if (m_dicTypeFiltreToTypeEditeur.TryGetValue(typeFiltre, out typeEditeur))
            {
                IEditeurFiltreGrid editeur = Activator.CreateInstance(typeEditeur) as IEditeurFiltreGrid;
                return editeur;
            }
            return null;
        }
    }
}
