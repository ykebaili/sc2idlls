using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
    public class CInfoTypeElementInDb
    {
        private Type m_typeElement;
        private int m_nNbElements = 0;
        private int m_nNbElementSupprimes = 0;
        private int m_nNbElementPrevisionnels = 0;

        public CInfoTypeElementInDb()
        {
        }

        public CInfoTypeElementInDb(Type typeElement)
        {
            m_typeElement = typeElement;
        }

        //------------------------------------------------------
        public Type TypeElement
        {
            get
            {
                return m_typeElement;
            }
        }

        //------------------------------------------------------
        [DynamicField("Element type label")]
        public string LibelleTypeElement
        {
            get
            {
                return DynamicClassAttribute.GetNomConvivial(m_typeElement);
            }
        }

        //------------------------------------------------------
        [DynamicField("Elements count")]
        public int NbElements
        {
            get
            {
                return m_nNbElements;
            }
            set{
                m_nNbElements = value;
            }
        }

        //------------------------------------------------------
        [DynamicField("Previsionnal elements count")]
        public int NbElementsPrevisionnels
        {
            get
            {
                return m_nNbElementPrevisionnels;
            }
            set
            {
                m_nNbElementPrevisionnels = value;
            }
        }

        //------------------------------------------------------
        [DynamicField("Deleted elements count")]
        public int NbElementsSupprimes
        {
            get
            {
                return m_nNbElementSupprimes;
            }
            set
            {
                m_nNbElementSupprimes = value;
            }
        }
    }


    public class CCompteurElementsInDb
    {
        public static List<CInfoTypeElementInDb> GetInfosInDatabase(int nIdSession)
        {
            List<CInfoTypeElementInDb> lst = new List<CInfoTypeElementInDb>();
            foreach (Type tp in CContexteDonnee.GetAllTypes())
            {
                CStructureTable structure = CStructureTable.GetStructure(tp);
                C2iRequeteAvancee requete = new C2iRequeteAvancee();
                requete.TableInterrogee = CContexteDonnee.GetNomTableForType(tp);
                requete.ListeChamps.Add(new C2iChampDeRequete("NBELEMENTS", new CSourceDeChampDeRequete(structure.Champs[0].NomChamp),
                    typeof(int), OperationsAgregation.Number, false));
                CInfoTypeElementInDb info = new CInfoTypeElementInDb(tp);
                CResultAErreur result = requete.ExecuteRequete(nIdSession);
                if (result && result.Data is DataTable)
                    info.NbElements = (int)((DataTable)result.Data).Rows[0][0];

                if (!typeof(IObjetSansVersion).IsAssignableFrom(tp))
                {
                    requete.FiltreAAppliquer = new CFiltreData(CSc2iDataConst.c_champIdVersion + " is not null and " +
                        CSc2iDataConst.c_champIsDeleted + "=@1", false);
                    requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
                    result = requete.ExecuteRequete(nIdSession);
                    if (result && result.Data is DataTable)
                        info.NbElementsPrevisionnels = (int)((DataTable)result.Data).Rows[0][0];
                    requete.FiltreAAppliquer = new CFiltreData(CSc2iDataConst.c_champIsDeleted + "=@1", true);
                    requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
                    result = requete.ExecuteRequete(nIdSession);
                    if (result && result.Data is DataTable)
                        info.NbElementsSupprimes = (int)((DataTable)result.Data).Rows[0][0];
                }
                lst.Add(info);
            }
            return lst;
        }
    }
}
