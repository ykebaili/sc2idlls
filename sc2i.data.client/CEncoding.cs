using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    public enum EEncoding
    {
        Default = 0,
        ASCII,
        BigEndianUnicode,
        Unicode,
        UTF32,
        UTF7,
        UTF8
    }

    public class CEncoding : CEnumALibelle<EEncoding>
    {
        public CEncoding(EEncoding encoding)
            : base(encoding)
        {
        }



        public override string Libelle
        {
            get { return CUtilSurEnum.GetNomConvivial(Code.ToString()); }
        }

        public Encoding GetEncoding()
        {
            switch (Code)
            {
                case EEncoding.Default:
                    return Encoding.Default;

                case EEncoding.ASCII:
                    return Encoding.ASCII;

                case EEncoding.BigEndianUnicode:
                    return Encoding.BigEndianUnicode;

                case EEncoding.Unicode:
                    return Encoding.Unicode;

                case EEncoding.UTF32:
                    return Encoding.UTF32;

                case EEncoding.UTF7:
                    return Encoding.UTF7;

                case EEncoding.UTF8:
                    return Encoding.UTF8;

                default:
                    return Encoding.Default;

            }
            return Encoding.Default;
        }
    }
}
