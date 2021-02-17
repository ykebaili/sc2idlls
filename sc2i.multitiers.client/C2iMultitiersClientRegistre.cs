using System;


using sc2i.common;


namespace sc2i.multitiers.client
{
	/// <summary>
	/// Registre de base et standard pour une application multitiers cliente SC2I
	/// Seules sont à implémenter les fonctions GetClePrincipale() et IsLocalMachine
	/// Le registre définit l'URL du C2iFactory et le channel TCP utilisé
	/// </summary>
	public abstract class C2iMultitiersClientRegistre : C2iRegistre
	{
		
		//////////////////////////////////////////////
		public string GetFactoryURL()
		{
			string strURL = GetValue("General","FactoryURL","");
            if (strURL == "")
            {
                //Vérifie si la valeur est trouvé sur localmachin ou localuser
                strURL = GetValue(!IsLocalMachine(), "General", "FactoryURL", "");
            }
			if ( strURL == "" )
			{
				throw new Exception ( I.T("The entry @1 \\General\\ FactoryURL doesn't exist un the register|102",GetClePrincipale()));
			}
			return strURL;
		}

		//////////////////////////////////////////////
		public string GetFactoryURLSecondaire()
		{
			string strURL = GetValue("General","FactoryURLSecondaire","");
			return strURL;
		}

		//////////////////////////////////////////////
		public int GetRemotingTCPChannel()
		{
			return GetIntValue ( "General", "TCPChannel", 0 );
		}

        //////////////////////////////////////////////
        public string GetBindTCPChannelBindTo()
        {
            string strBindTo = GetValue("General", "BindTo", "");
            if (strBindTo == "")
            {
                //Vérifie si la valeur est trouvé sur localmachin ou localuser
                strBindTo = GetValue(!IsLocalMachine(), "General", "BindTo", "");
            }
            return strBindTo;
        }

	}
}

