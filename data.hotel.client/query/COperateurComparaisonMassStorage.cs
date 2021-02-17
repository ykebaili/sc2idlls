using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    public enum EOperateurComparaisonMassStorage
    {
        Superieur = 0,
        Inferieur
    }

    public class COperateurComparaisonMassStorage : CEnumALibelle<EOperateurComparaisonMassStorage>
    {
        //--------------------------------------------------------------------------------------
        public COperateurComparaisonMassStorage(EOperateurComparaisonMassStorage operateur)
            : base(operateur)
        {
        }

        //--------------------------------------------------------------------------------------
        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case EOperateurComparaisonMassStorage.Superieur:
                        return I.T(">|20000");
                    case EOperateurComparaisonMassStorage.Inferieur:
                        return I.T("<|20000");
                    default:
                        break;
                }
                return "?";
            }
        }
    }
}
