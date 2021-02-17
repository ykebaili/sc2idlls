using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;
using System.Reflection;

namespace sc2i.data.dynamic.StructureImport
{
    public class CMappageChampImport : I2iSerializable
    {
        private C2iOrigineChampImport m_origine;
        private CDefinitionProprieteDynamique m_proprieteDestination;
        private bool m_bIsCle = false;

        //--------------------------------------------
        public CMappageChampImport()
        {
        }

        //--------------------------------------------
        public C2iOrigineChampImport Origine
        {
            get
            {
                return m_origine;
            }
            set
            {
                m_origine = value;
            }
        }

        //--------------------------------------------
        public CDefinitionProprieteDynamique ProprieteDestination
        {
            get
            {
                return m_proprieteDestination;
            }
            set
            {
                m_proprieteDestination = value;
            }
        }

        //--------------------------------------------
        /// <summary>
        /// Si true, indique que ce mappage sert de clé, c'est à dire qu'un
        /// enregistrement donc toutes les clés sont existantes dans la 
        /// base n'est pas créé, mais est mis à jour.
        /// Seuls les mappages dont la destination est
        /// une propriété (CDefinitionProprieteDynamiqueDotNet) et pour
        /// laquelle la propriété a l'attribute TableField peuvent être
        /// utilisés comme clé
        /// </summary>
        /// 
        public bool IsCle
        {
            get
            {
                return m_bIsCle;
            }
            set
            {
                m_bIsCle = value;
            }
        }

        //--------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iOrigineChampImport>(ref m_origine);
            if (result)
                result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_proprieteDestination);
            return result;
        }

        //--------------------------------------------
        public CResultAErreur GetFiltreCle(Type typeCible, int nNumeroParametre)
        {
            return GetFiltreCle(typeCible, m_proprieteDestination, nNumeroParametre);
        }

        //--------------------------------------------
        /// <summary>
        /// Si succès, retourne le filtre correspondant à cet élément
        /// en tant que clé. le texte du filtre (format filtreData) est contenu dans le data du result
        /// avec le paramètre valeur = @n
        /// </summary>
        /// <param name="typeCible"></param>
        /// <returns></returns>
        public static CResultAErreur GetFiltreCle( 
            Type typeCible, 
            CDefinitionProprieteDynamique definition,
            int nNumeroParametre )
        {
            CResultAErreur result = CResultAErreur.True;
            CDefinitionProprieteDynamiqueDotNet def = definition as CDefinitionProprieteDynamiqueDotNet;
            PropertyInfo info = null;
            if ( def != null )
                info = typeCible.GetProperty(def.NomProprieteSansCleTypeChamp);
            if (info != null)
            {
                object[] attribs = info.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
                if (attribs.Length > 0)
                { 
                    TableFieldPropertyAttribute attr = attribs[0] as TableFieldPropertyAttribute;
                    result.Data = attr.NomChamp + "=@" + nNumeroParametre;
                    return result;
                }
                attribs = info.GetCustomAttributes(typeof(RelationAttribute), true);
                if (attribs.Length > 0)
                {
                    RelationAttribute attr = attribs[0] as RelationAttribute;
                    if (attr.ChampsFils.Length == 1)
                    {
                        result.Data = attr.ChampsFils[0] + "=@" + nNumeroParametre;
                        return result;
                    }
                }
            }
            //Sinon, si ID d'un objet a id auto
            if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeCible))
            {
                if (definition.NomPropriete == "#PP|Id")
                {
                    //Trouve le champ id
                    string strChampId = CObjetDonnee.GetChampsId(typeCible)[0];
                    result.Data = strChampId+"=@"+nNumeroParametre;
                    return result;
                }
            }

            result.EmpileErreur(I.T("Can not use field @1 as key in imort|20044", definition.Nom));
            return result;
        }

        
        
    }
}
