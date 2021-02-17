using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;

namespace sc2i.data
{
    /// <summary>
    /// Doit avoir un new qui prend en paramètre la propriété sans clétype
    /// </summary>
    public interface IDependanceListeObjetsDonneesReader
    {

        void ReadArbre(
            CListeObjetsDonnees listeSource,
            CListeObjetsDonnees.CArbreProps arbre,
            List<string> lstPaquetsALire);
    }

    public interface IPreparateurTransformationArbreDefinitionsEnArbreSousPropListeObjetDonnee
    {
        void PrepareArbre(CArbreDefinitionsDynamiques arbre, CContexteDonnee contexte);
    }
    
    
    public class CGestionnaireDependanceListeObjetsDonneesReader
    {
        /// <summary>
        /// Dictionnaire Type de champ (clé)->Reader de dépendances
        /// </summary>
        private static Dictionary<string, Type> m_dicTypeReader = new Dictionary<string, Type>();
        private static Dictionary<string, Type> m_dicTypeToTransformateur = new Dictionary<string, Type>();

        public static void RegisterReader(string strCle, Type typeReader)
        {
            m_dicTypeReader[strCle] = typeReader;
        }

        public static void RegisterTransformateur(string strCle, Type typeTransformateur)
        {
            m_dicTypeToTransformateur[strCle] = typeTransformateur;
        }

        public static IDependanceListeObjetsDonneesReader GetReader(string strPropriete)
        {
            string strCleType = "";
			string strProprieteSansCle = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strPropriete, ref strCleType, ref strProprieteSansCle))
            {
                Type typeReader = null;
                if (m_dicTypeReader.TryGetValue(strCleType, out typeReader))
                    return Activator.CreateInstance(typeReader, new object[0]) as IDependanceListeObjetsDonneesReader;
            }
            return null;
        }

        public static void PrepareArbre(
            CArbreDefinitionsDynamiques arbre,
            CContexteDonnee ctx)
        {
            foreach (Type tp in m_dicTypeToTransformateur.Values)
            {
                IPreparateurTransformationArbreDefinitionsEnArbreSousPropListeObjetDonnee preparateur = Activator.CreateInstance(tp) as IPreparateurTransformationArbreDefinitionsEnArbreSousPropListeObjetDonnee;
                if (preparateur != null)
                    preparateur.PrepareArbre(arbre, ctx);
            }
        }



    }
}
