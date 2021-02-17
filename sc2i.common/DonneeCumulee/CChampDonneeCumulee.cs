using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.common.DonneeCumulee
{
    public enum ETypeChampDonneeCumulee
    {
        Cle = 0,
        Decimal = 1,
        Date = 2,
        Texte = 3
    }
    ///Permet de désigner un champ dans une ligne de donnée précalculée
    [Serializable]
    public class CChampDonneeCumulee : I2iSerializable
    {
        private int m_nNumeroChamp = 0;
        private ETypeChampDonneeCumulee m_typeChamp = ETypeChampDonneeCumulee.Cle;

        //--------------------------------------------
        public CChampDonneeCumulee()
        {
        }

        //--------------------------------------------
        public CChampDonneeCumulee( ETypeChampDonneeCumulee typeChamp, int nNumero)
        {
            m_nNumeroChamp = nNumero;
            m_typeChamp = typeChamp;
        }


        //--------------------------------------------
        public int NumeroChamp
        {
            get
            {
                return m_nNumeroChamp;
            }
            set
            {
                m_nNumeroChamp = value;
            }
        }

        //--------------------------------------------
        public ETypeChampDonneeCumulee TypeChamp
        {
            get
            {
                return m_typeChamp;
            }
            set
            {
                m_typeChamp = value;
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
            serializer.TraiteInt(ref m_nNumeroChamp);
            serializer.TraiteEnum<ETypeChampDonneeCumulee>(ref m_typeChamp);
            return result;
        }

        //--------------------------------------------
        public override bool Equals(object obj)
        {
            CChampDonneeCumulee champ = obj as CChampDonneeCumulee;
            if (champ != null)
            {
                return champ.TypeChamp == TypeChamp && champ.NumeroChamp == NumeroChamp;
            }
            return false;
        }

    }
}
