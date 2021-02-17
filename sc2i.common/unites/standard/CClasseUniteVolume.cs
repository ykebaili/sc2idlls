using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUniteVolume : IClasseUnite
    {
        public const string c_IdClasse = "VOL";
        public const string c_IdL = "L";
        public const string c_IdDL = "DL";
        public const string c_IdCL = "CL";
        public const string c_IdML = "ML";
        public const string c_IdDAL = "DAL";
        public const string c_IdHL = "HL";
        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Volume|20045"); }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "l";
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
            CClasseUniteVolume volume = new CClasseUniteVolume();
            CGestionnaireUnites.AddClasseUnite ( volume) ;
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Liter|20046"), "l",c_IdL, volume, 1, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Deciliter|20047"), "dl",c_IdDL, volume, 0.1, 0 ) );
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Centiliter|20048"), "cl", c_IdCL, volume, 0.01, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Milliliter|20049"), "ml", c_IdML, volume, 0.001, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Decaliter|20050"), "dal", c_IdDAL, volume, 10, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Hectoliter|20051"), "hl", c_IdHL, volume, 100, 0));
            
        }
            
    }

    
}
