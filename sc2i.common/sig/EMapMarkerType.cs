using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.sig
{
    public enum EMapMarkerType
    {
        none = 0,
        arrow = 1,
        blue = 2,
        blue_small = 3,
        blue_dot = 4,
        blue_pushpin = 5,
        brown_small = 6,
        gray_small = 7,
        green = 8,
        green_small = 9,
        green_dot = 10,
        green_pushpin = 11,
        green_big_go = 12,
        yellow = 13,
        yellow_small = 14,
        yellow_dot = 15,
        yellow_big_pause = 16,
        yellow_pushpin = 17,
        lightblue = 18,
        lightblue_dot = 19,
        lightblue_pushpin = 20,
        orange = 21,
        orange_small = 22,
        orange_dot = 23,
        pink = 24,
        pink_dot = 25,
        pink_pushpin = 26,
        purple = 27,
        purple_small = 28,
        purple_dot = 29,
        purple_pushpin = 30,
        red = 31,
        red_small = 32,
        red_dot = 33,
        red_pushpin = 34,
        red_big_stop = 35,
        black_small = 36,
        white_small = 37,
    }

    public class CMapMarkerType : CEnumALibelle<EMapMarkerType>
    {
        //---------------------------------------------------------
        public CMapMarkerType(EMapMarkerType code)
            : base(code)
        {
        }

        //---------------------------------------------------------
        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case EMapMarkerType.none:
                        return I.T("Custom|20218");
                    case EMapMarkerType.arrow:
                        return I.T("Arrow|20181");
                    case EMapMarkerType.blue:
                        return I.T("Blue|20182");
                    case EMapMarkerType.blue_small:
                        return I.T("Blue small|20183");
                    case EMapMarkerType.blue_dot:
                        return I.T("Blue dot|20184");
                    case EMapMarkerType.blue_pushpin:
                        return I.T("Blue pushpin|20185");
                    case EMapMarkerType.brown_small:
                        return I.T("Brown small|20186");
                    case EMapMarkerType.gray_small:
                        return I.T("Gray small|20187");
                    case EMapMarkerType.green:
                        return I.T("Green|20188");
                    case EMapMarkerType.green_small:
                        return I.T("Green small|20189");
                    case EMapMarkerType.green_dot:
                        return I.T("Green dot|20190");
                    case EMapMarkerType.green_pushpin:
                        return I.T("Green pushpin|20191");
                    case EMapMarkerType.green_big_go:
                        return I.T("Green big go|20192");
                    case EMapMarkerType.yellow:
                        return I.T("Yellow|20193");
                    case EMapMarkerType.yellow_small:
                        return I.T("Yellow small|20194");
                    case EMapMarkerType.yellow_dot:
                        return I.T("Yellow dot|20195");
                    case EMapMarkerType.yellow_big_pause:
                        return I.T("Yellow big pause|20196");
                    case EMapMarkerType.yellow_pushpin:
                        return I.T("Yellow pushpin|20197");
                    case EMapMarkerType.lightblue:
                        return I.T("Light blue|20198");
                    case EMapMarkerType.lightblue_dot:
                        return I.T("Light blue dot|20199");
                    case EMapMarkerType.lightblue_pushpin:
                        return I.T("Light blue pushpin|20200");
                    case EMapMarkerType.orange:
                        return I.T("Orange|20201");
                    case EMapMarkerType.orange_small:
                        return I.T("Orange small|20202");
                    case EMapMarkerType.orange_dot:
                        return I.T("Orange dot|20203");
                    case EMapMarkerType.pink:
                        return I.T("Pink|20204");
                    case EMapMarkerType.pink_dot:
                        return I.T("Pink dot|20205");
                    case EMapMarkerType.pink_pushpin:
                        return I.T("Pink pushpin|20206");
                    case EMapMarkerType.purple:
                        return I.T("Purple|20207");
                    case EMapMarkerType.purple_small:
                        return I.T("Purple small|20208");
                    case EMapMarkerType.purple_dot:
                        return I.T("Purple dot|20209");
                    case EMapMarkerType.purple_pushpin:
                        return I.T("Purple pushpin|20210");
                    case EMapMarkerType.red:
                        return I.T("Red|20211");
                    case EMapMarkerType.red_small:
                        return I.T("Red small|20212");
                    case EMapMarkerType.red_dot:
                        return I.T("Red dot|20213");
                    case EMapMarkerType.red_pushpin:
                        return I.T("Red pushpin|20214");
                    case EMapMarkerType.red_big_stop:
                        return I.T("Red big stop|20215");
                    case EMapMarkerType.black_small:
                        return I.T("Black small|20216");
                    case EMapMarkerType.white_small:
                        return I.T("White small|20217");
                }
                return "?";
            }
            
        }
    }
}
