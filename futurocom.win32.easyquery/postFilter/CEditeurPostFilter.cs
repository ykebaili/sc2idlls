using futurocom.easyquery;
using futurocom.easyquery.postFillter;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.win32.easyquery.postFilter
{

    public interface IEditeurPostFilter 
    {
        void Init ( CODEQBase odeqBase, IPostFilter postFilter );

        CResultAErreurType<IPostFilter> MajChamps();
    }
    public static class CEditeurPostFilter
    {
        private static Dictionary<Type, Type> m_dicTypeToEditeur = new Dictionary<Type, Type>();
        
        //-----------------------------------------------------------------------------------------------------
        public static void RegisterEditeur ( Type typePostFilter, Type typeEditeur )
        {
            m_dicTypeToEditeur[typePostFilter] = typeEditeur;
        }

        //-----------------------------------------------------------------------------------------------------
        public static IEditeurPostFilter GetEditeur ( Type typePostFilter )
        {
            if (typePostFilter == null)
                return null;
            Type tpEditeur = null;
            m_dicTypeToEditeur.TryGetValue(typePostFilter, out tpEditeur);
            if ( tpEditeur != null )
            {
                IEditeurPostFilter editeur = Activator.CreateInstance(tpEditeur, new object[0]) as IEditeurPostFilter;
                return editeur;
            }
            return null;
        }
    }
}
