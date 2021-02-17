using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data.dynamic.Caracteristiques
{
    /// /////////////////////////////////////////////////////////////
    [AutoExec("Autoexec")]
    public class CMethodeSupplementaireGetCaracteristiques : CMethodeSupplementaire
    {
        protected CMethodeSupplementaireGetCaracteristiques()
            : base(typeof(CObjetDonneeAIdNumerique))
        {
        }

        public static void Autoexec()
        {
            CGestionnaireMethodesSupplementaires.RegisterMethode(new CMethodeSupplementaireGetCaracteristiques());
        }

        public override string Name
        {
            get
            {
                return "GetCharacteristics";
            }
        }

        public override string Description
        {
            get
            {
                return I.T("Returns characteristics with specified type|20080");
            }
        }

        public override Type ReturnType
        {
            get
            {
                return typeof(CCaracteristiqueEntite);
            }
        }

        public override bool ReturnArrayOfReturnType
        {
            get
            {
                return true;
            }
        }


        public override CInfoParametreMethodeDynamique[] InfosParametres
        {
            get
            {
                return new CInfoParametreMethodeDynamique[]
					{
						new CInfoParametreMethodeDynamique ( "Charateristic type",
						I.T("Characteristic type to be returned|20081"),
						typeof(CTypeCaracteristiqueEntite) )
					};
            }
        }

        public override object Invoke(object objetAppelle, params object[] parametres)
        {
            if (parametres.Length != 1 || parametres[0] == null ||
                !(objetAppelle is CObjetDonneeAIdNumerique))
                return null;
            CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)objetAppelle;
            CListeObjetsDonnees lst =  CCaracteristiqueEntite.GetCaracteristiques(objet);
            if (parametres.Length > 0 && parametres[0] is CTypeCaracteristiqueEntite)
                lst.Filtre = new CFiltreData(CTypeCaracteristiqueEntite.c_champId + "=@1",
                    ((CTypeCaracteristiqueEntite)parametres[0]).Id);
            return lst.ToArray<CCaracteristiqueEntite>();
        }
    }
}
