using data.hotel.easyquery.calcul;
using futurocom.easyquery;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.win32.calcul
{
    public interface IEditeurCalculHotel
    {
        void Init(
             IDataHotelCalcul col,
             CEasyQuery query,
             CODEQTableFromDataHotel table);

        CResultAErreurType<IDataHotelCalcul> GetCalcul();

    }

    public static class CAllocateurEditeurCalculDataHotel
    {
        private static Dictionary<Type, Type> m_dicTypeCalculToEditeur = new Dictionary<Type, Type>();
        private static Dictionary<Type, string> m_dicTypeToName = new Dictionary<Type,string>();

        //---------------------------------------------------------------------------------
        public static void RegisterEditeur ( Type typeCalcul, Type typeEditeur, string strName  )
        {
            m_dicTypeCalculToEditeur[typeCalcul] = typeEditeur;
            m_dicTypeToName[typeCalcul] = strName;
        }

        //---------------------------------------------------------------------------------
        public static IEnumerable<KeyValuePair<Type, string>> TypesExistants
        {
            get
            {
                return m_dicTypeToName.ToArray();
            }
        }

        //---------------------------------------------------------------------------------
        public static Type GetTypeEditeur(Type typeCalcul)
        {
            Type tp = null;
            m_dicTypeCalculToEditeur.TryGetValue(typeCalcul, out tp);
            return tp;
        }
    }
}
