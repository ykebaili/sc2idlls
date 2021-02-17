using System;
using sc2i.multitiers.client;
using sc2i.common;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Tous les objets instanciés sur le serveur dans le framework
	/// sc2i doivent avoir un numéro de session qui identifie la session
	/// auquel appartient l'objet
	/// </summary>
	public class C2iObjetServeur : MarshalByRefObject, I2iMarshalObjectDeSession
	{
		protected int m_nIdSession = -1;

		/// /////////////////////////////////////////////////////
		public C2iObjetServeur()
		{
			m_nIdSession = -1;
		}
		/// /////////////////////////////////////////////////////
		public C2iObjetServeur( int nIdSession )
		{
			m_nIdSession = nIdSession;
		}

		/// /////////////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				return m_nIdSession;
			}
			set
			{
				m_nIdSession = value;
			}
		}

		//Cette fonction ne fait rien, elle permet juste d'utiliser le renew on call time pour
		//renouveller le bail de l'objet
		public void RenouvelleBailParAppel()
		{
		}

        ///////////////////////////////////////////////
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
