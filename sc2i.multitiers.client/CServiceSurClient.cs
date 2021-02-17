using System;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// est un service que peut executer le client.
	/// Typiquement, au lancement de l'application tous les services existants s'enregistrent
	/// sur la classe CSessionClient
	/// </summary>
	public abstract class CServiceSurClient : MarshalByRefObject, I2iMarshalObject
	{
		//Lance le service sur le poste client
		public abstract CResultAErreur RunService ( object parametre );

		public abstract string IdService{get;}
		/// /////////////////////////////////////////
		public void RenouvelleBailParAppel()
		{
		}

        private string m_strIdUnique = "";
        public string UniqueId
        {
            get
            {
                if (m_strIdUnique.Length == 0)
                    m_strIdUnique = CUniqueIdentifier.GetNew();
                return m_strIdUnique;
            }
        }
    }
}
