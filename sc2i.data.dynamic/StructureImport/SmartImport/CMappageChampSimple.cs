using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CMappageChampSimple : I2iSerializable
    {
        private CDefinitionProprieteDynamique m_propriete = null;
        private CSourceSmartImport m_source = null;
        private bool m_bUseAsKey = false;

        //------------------------------------------------
        public CMappageChampSimple()
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
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_propriete);
            if (result)
                result = serializer.TraiteObject<CSourceSmartImport>(ref m_source);
            if (result)
                serializer.TraiteBool(ref m_bUseAsKey);
            return result;
        }

        //------------------------------------------------
        public CResultAErreur GetValue ( DataRow rowSource, CContexteImportDonnee contexteImport )
        {
            CResultAErreur result = CResultAErreur.True;
            if ( Source != null )
            {
                result = Source.GetValue(rowSource, contexteImport);
                if (!result)
                    return result;
                result = ConvertValue(result.Data);
            }
            return result;
        }

        //------------------------------------------------
        /// <summary>
        /// Convertit la valeur pour qu'elle soit compatible avec le champ
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        private CResultAErreur ConvertValue ( object valeur )
        { 
            CResultAErreur result = CResultAErreur.True;
            if (valeur == null || Propriete == null)
                return result;
            if (Source.OptionsValeursNulles != null && Source.OptionsValeursNulles.IsValeurNulle(valeur))
                return result;
            Type tpAttendu = Propriete.TypeDonnee.TypeDotNetNatif;
            if ( tpAttendu.IsGenericType && tpAttendu.GetGenericTypeDefinition() == typeof(Nullable<>))
                tpAttendu= tpAttendu.GetGenericArguments()[0];
            if ( tpAttendu == typeof(CDateTimeEx)) 
                tpAttendu = typeof(DateTime); 
            try
            {
                object val = null;
                if ( tpAttendu == typeof(double) && valeur is string)
                    val = CUtilDouble.DoubleFromString((string)valeur);
                else
                    val = Convert.ChangeType ( valeur, tpAttendu);
                result.Data = val;
            }
            catch
            {
                if (Source.OptionsValeursNulles != null && Source.OptionsValeursNulles.NullOnConversionError)
                    result.Data = null;
                else
                    result.EmpileErreur(I.T("Can not convert value @1 to @2|20092", valeur.ToString(), DynamicClassAttribute.GetNomConvivial(tpAttendu)));
                        return result;
            }
            return result;
        }
    }
}
