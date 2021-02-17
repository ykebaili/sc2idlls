using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUniteAngle : IClasseUnite
    {
        public const string c_IdClasse = "ANGLE";
        public const string c_IdDeg = "°";
        public const string c_IdMin = "'";
        public const string c_IdSec = "''";
        
        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Angle|20166"); }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "°";
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
            CClasseUniteAngle angle = new CClasseUniteAngle();
            CGestionnaireUnites.AddClasseUnite ( angle) ;
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Degree|20167"), "°",c_IdDeg, angle, 1, 0 ) );
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Minute|20168"), "'", c_IdMin, angle, 1.0 / 60.0, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Second|20169"), "''", c_IdSec, angle, 1.0 / 3600.0, 0));
        }
            
    }

    
}
