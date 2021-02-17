using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace futurocom.easyquery
{
    /// <summary>
    /// Colonne issue d'
    /// </summary>
    [Serializable]
    public class CColonneEQCalculee : CFormuleNommee, IColumnDeEasyQuery
    {

        //-----------------------------------------
        public CColonneEQCalculee()
        {
        }

        //-----------------------------------------
        public CColonneEQCalculee(string strNomColonne, C2iExpression formule)
            :base ( strNomColonne, formule )
        {
            
        }
        //-----------------------------------------
        public string ColumnName
        {
            get
            {
                return Libelle;
            }
            set
            {
                Libelle = value;
            }
        }

        
        //-----------------------------------------
        public Type DataType
        {
            get
            {
                if (Formule != null)
                    return Formule.TypeDonnee.TypeDotNetNatif;
                return typeof(string);
            }
            set
            {
            }
            
        }

        //-----------------------------------------
        public CResultAErreur OnRemplaceColSource(IColumnDeEasyQuery oldCol, IColumnDeEasyQuery newCol)
        {
            return CResultAErreur.True;
        }


        //-----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            return result;
        }



    }


}
