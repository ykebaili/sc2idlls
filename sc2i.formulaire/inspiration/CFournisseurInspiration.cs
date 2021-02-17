using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.inspiration
{
    public interface IFournisseurInspiration
    {
        IEnumerable<string> GetInspiration(IParametreInspiration parametre);
    }

    public interface IParametreInspiration : I2iSerializable
    {
    }

    public class CListeParametresInspiration : List<IParametreInspiration>, I2iSerializable
    {
        public CListeParametresInspiration(IEnumerable<IParametreInspiration> source)
            : base(source)
        {
        }

        public CListeParametresInspiration()
            : base()
        {
        }


        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize(C2iSerializer seriliazer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = seriliazer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = seriliazer.TraiteListe<IParametreInspiration>(this);
            return result;
        }

    }

    public class CFournisseurInspiration 
    {
        //--------------------------------------------------------------------------------------------------
        private static Dictionary<Type, IFournisseurInspiration> m_dicFournisseursParTypeDeParametre = new Dictionary<Type, IFournisseurInspiration>();

        //--------------------------------------------------------------------------------------------------
        public static void RegisterFournisseur(IFournisseurInspiration fournisseur, Type typeParametreInspiration)
        {
            m_dicFournisseursParTypeDeParametre[typeParametreInspiration] = fournisseur;
        }


        //--------------------------------------------------------------------------------------------------
        public static IEnumerable<string> GetInspiration(CListeParametresInspiration parametres)
        {
            HashSet<string> set = new HashSet<string>();
            IEnumerable<string> lstRetour = null;
            foreach (IParametreInspiration parametre in parametres)
            {
                if (parametre != null)
                {
                    IFournisseurInspiration fournisseur = null;
                    if (m_dicFournisseursParTypeDeParametre.TryGetValue(parametre.GetType(), out fournisseur))
                    {
                        IEnumerable<string> lstTmp = fournisseur.GetInspiration(parametre);
                        if (parametres.Count() == 1)
                            lstRetour = lstTmp;
                        else
                            foreach (string strChaine in lstTmp)
                                set.Add(strChaine);
                    }
                }
            }

            if (parametres.Count() > 1)
            {
                List<string> lstTmp = new List<string>();
                foreach (string str in set)
                    lstTmp.Add(str);
                lstRetour = lstTmp;
            }
            return lstRetour;
        }
    }
}
