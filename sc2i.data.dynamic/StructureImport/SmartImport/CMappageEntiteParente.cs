using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CMappageEntiteParente : I2iSerializable
    {
        private CDefinitionProprieteDynamique m_propriete;
        private bool m_bUseAsKey = false;
        private CConfigMappagesSmartImport m_configMappageEntiteParente = new CConfigMappagesSmartImport();
        private CSourceSmartImport m_source = null;

        //Si le set échoue une fois sur la propriété et que celle ci
        //possède un attribut SpecificImportSet contenant un nom de méthode à 
        //appeler, cette méthode est stockée ici pour cache.
        //la valeur n'est pas sérializée.
        public MethodInfo AlternativeSetMethodInfo = null;

        //------------------------------------------------
        public CMappageEntiteParente()
        {
        }

        //------------------------------------------------
        public CDefinitionProprieteDynamique Propriete
        {
            get
            {
                return m_propriete;
            }
            set
            {
                m_propriete = value;
            }
        }

        //------------------------------------------------
        public bool UseAsKey
        {
            get
            {
                return m_bUseAsKey;
            }
            set
            { 
                m_bUseAsKey = value;
            }
        }

        //------------------------------------------------
        public CConfigMappagesSmartImport ConfigEntiteParente
        {
            get
            {
                return m_configMappageEntiteParente;
            }
            set
            {
                m_configMappageEntiteParente = value;
            }
        }

        //------------------------------------------------
        public CSourceSmartImport Source
        {
            get
            {
                return m_source;
            }
            set
            {
                m_source = value;
            }
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_propriete);
            if (result)
                result = serializer.TraiteObject<CConfigMappagesSmartImport>(ref m_configMappageEntiteParente);
            if ( result )
                result = serializer.TraiteObject<CSourceSmartImport>(ref m_source);
            if ( !result )
                return result;
            serializer.TraiteBool(ref m_bUseAsKey);


            return result;
        }

        //------------------------------------------------
        public CResultAErreurType<CObjetDonnee> GetObjetAssocie(
            DataRow row,
            CContexteImportDonnee contexteImport,
            bool bAutoriseCreation)
        {
            CResultAErreurType<CObjetDonnee> resObjet = new CResultAErreurType<CObjetDonnee>();

            if (Source == null)
                return resObjet;
            CSourceSmartImportFixedValue sourceFixe = Source as CSourceSmartImportFixedValue;
            if (sourceFixe != null)
            {
                CResultAErreur result = sourceFixe.GetValue(row, contexteImport);
                if (!result)
                {
                    resObjet.EmpileErreur(result.Erreur);
                    return resObjet;
                }
                resObjet.DataType = result.Data as CObjetDonnee;
                return resObjet;
            }
            else if ( Source is CSourceSmartImportReference )
            {
                resObjet.DataType = ((CSourceSmartImportReference)Source).GetValue(row, contexteImport).Data as CObjetDonnee;
                return resObjet;
            }
            else
            {
                if (m_configMappageEntiteParente != null)
                {
                    string strFiltre = "";
                    resObjet = m_configMappageEntiteParente.FindObjet(row, contexteImport, null, ref strFiltre);
                    if (!resObjet)
                        return resObjet;
                    if ( resObjet.DataType != null || bAutoriseCreation )
                    {
                        CResultAErreur result = m_configMappageEntiteParente.ImportRow(row, contexteImport, null, false);
                        if (!result)
                        {
                            resObjet.EmpileErreur(result.Erreur);
                            return resObjet;
                        }
                        resObjet = m_configMappageEntiteParente.FindObjet(row, contexteImport, null, ref strFiltre);
                    }
                }
            }
            return resObjet;
        }



    }
}
