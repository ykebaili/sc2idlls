using System;
using System.Collections;
using sc2i.common;


namespace sc2i.formulaire
{
    /// <summary>
    /// Description résumée de CActionSur2iLink.
    /// </summary>
    public abstract class CActionSur2iLink : I2iSerializable
    {
        public const string c_idFichier = "2I_ACTION";
        private bool m_bAutoriserEnEdition = false;

        public CActionSur2iLink()
        {

        }


        //-------------------------------------------------------------------
        public abstract bool AutoriserEnEdition
        {
            get;
        }

        /// ////////////////////////////////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }

        /// ////////////////////////////////////////////////////////////////
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            return result;
        }
    }
}
