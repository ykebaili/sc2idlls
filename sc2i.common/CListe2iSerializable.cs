using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common
{
    public abstract class CListe2iSerializable<T>:List<T>, I2iSerializable
        where T:I2iSerializable
    {
        //------------------------------------------------
        public CListe2iSerializable()
            : base()
        {
        }

        //------------------------------------------------
        public CListe2iSerializable(IEnumerable<T> lst)
        {
            foreach (T objet in lst)
                Add(objet);
        }

        #region I2iSerializable Membres

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<T>(this);
            return result;
        }

        #endregion
    }
}
