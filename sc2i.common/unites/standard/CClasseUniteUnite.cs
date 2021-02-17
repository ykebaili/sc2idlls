using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUniteUnite : IClasseUnite
    {
        public const string c_IdClasse = "UNITE";
        public const string c_idUnite = "UNIT";
        
        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Unit|20170"); }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "UNIT";
            }
        }

        //-----------------------------------------------
        public string GlobalId
        {
            get { return c_IdClasse; }
        }

        //-----------------------------------------------
        public static void RegisterUnites()
        {
            CClasseUniteUnite unite = new CClasseUniteUnite();
            CGestionnaireUnites.AddClasseUnite(unite);
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Unit|20170"), I.T("u|20171"),c_idUnite, unite, 1, 0 ) );
        }
            
    }

    
}
