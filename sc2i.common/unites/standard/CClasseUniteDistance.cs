using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUniteDistance : IClasseUnite
    {
        public const string c_IdClasse = "DIST";
        public const string c_IdM = "M";
        public const string c_IdDM = "DM";
        public const string c_IdCM = "CM";
        public const string c_IdMM = "MM";
        public const string c_IdAM = "DAM";
        public const string c_IdHM = "HM";
        public const string c_IdKM = "KM";
        public const string c_IdYARD = "YD";
        public const string c_IdFEET = "FT";
        public const string c_IdINCH = "INCH";
        public const string c_IdMICROM = "µM";
        public const string c_IdNM = "NM";
        public const string c_IdPM = "PM";
        public const string c_IdFM = "FM";

        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Distance|20018"); }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "m";
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
            CClasseUniteDistance distance = new CClasseUniteDistance();
            CGestionnaireUnites.AddClasseUnite ( distance) ;
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Meter|20019"), "m",c_IdM, distance, 1, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Decimeter|20020"), "dm",c_IdDM, distance, 0.1, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Centimeter|20021"), "cm",c_IdCM, distance, 0.01, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Millimeter|20022"), "mm",c_IdMM, distance, 0.001, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Decameter|20023"), "dam",c_IdAM, distance, 10, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Hectometer|20024"), "hm",c_IdHM, distance, 100, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Kilometer|20025"), "km",c_IdKM, distance, 1000, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Yard|20026"), "yd",c_IdYARD, distance, 0.9144 , 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Feet|20027"), "ft",c_IdFEET, distance, 0.3048, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Inch|20028"), "in",c_IdINCH, distance, 0.0254, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Micrometer|20029"), "µm",c_IdMICROM, distance, 10e-6, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Nanometer|20030"), "nm",c_IdNM, distance, 10e-9, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Picometer|20031"), "pm",c_IdPM, distance, 10e-12, 0 ) );
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Femtometer|20032"), "fm",c_IdFM, distance, 10e-15, 0 ) );
        }
            
    }

    
}
