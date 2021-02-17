using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUniteTemps : IClasseUnite
    {
        public const string c_idClasse = "TIME";
        public const string c_idH = "H";
        public const string c_idMIN = "MIN";
        public const string c_idSEC = "S";
        public const string c_idDAY = "DAY";
        public const string c_idWEEK = "WEEK";
        public const string c_idYEAR = "YEAR";
        public const string c_idCENTURY = "CENTURY";
        

        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Time|20033"); }
        }

        //-----------------------------------------------
        public string GlobalId
        {
            get { return c_idClasse; }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "h";
            }
        }

        //-----------------------------------------------
        public static void RegisterUnites()
        {
            CClasseUniteTemps temps = new CClasseUniteTemps();
            CGestionnaireUnites.AddClasseUnite ( temps) ;
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Hour|20034"), "h",c_idH, temps, 1, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Minute|20035"), "min",c_idMIN, temps, 1.0/60.0, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Second|20036"), "s",c_idSEC, temps, 1.0/60.0/60.0, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Day|20037"), I.T("day|20041"),c_idDAY, temps, 24, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Week|20038"), I.T("wk|20042"),c_idWEEK, temps, 24*7, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Year|20039"), I.T("yrs|20043"),c_idYEAR, temps, 24*365.25, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Century|20040"), I.T("cent|20044"),c_idCENTURY, temps, 24*365.25*100, 0 ) );
        }
            
    }

    
}
