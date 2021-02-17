using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CSC2iMultitiersClient.
	/// </summary>

	
	public class CSC2iMultitiersClient
	{
		private const string c_nomChannel = "SC2I_CHANNEL";

		public static CResultAErreur InitFromRegistre( C2iMultitiersClientRegistre registre )
		{
			return Init ( registre.GetRemotingTCPChannel(), 
				registre.GetFactoryURL(), registre.GetBindTCPChannelBindTo() );
		}

        public static CResultAErreur Init(int nPort, string strFactoryURL)
        {
            return Init(nPort, strFactoryURL, "");
        }

		public static CResultAErreur Init ( int nPort, string strFactoryURL, string strBindTo )
		{
			string strNomChannel = c_nomChannel+"_"+nPort.ToString();
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( ChannelServices.GetChannel ( strNomChannel ) == null )
				{
					//SimmoTech.Utils.Remoting.CustomBinaryServerFormatterSinkProvider serverProv = new SimmoTech.Utils.Remoting.CustomBinaryServerFormatterSinkProvider();
					BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
					serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;


					BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
					//SimmoTech.Utils.Remoting.CustomBinaryClientFormatterSinkProvider clientProv = new SimmoTech.Utils.Remoting.CustomBinaryClientFormatterSinkProvider();

				
					Hashtable table = new Hashtable();
					table["port"] = nPort;
					table["name"] =  strNomChannel;
                    if (strBindTo.Length > 0)
                        table["bindTo"] = strBindTo;
					TcpChannel channel = new TcpChannel ( table, clientProv, serverProv );
				
					ChannelServices.RegisterChannel(channel);
				}
				C2iFactory.InitDefaultUrl ( strFactoryURL );

				//teste que les paramètres sont Ok
				if ( !C2iFactory.TestServeurParDefaut() )
				{
					result.EmpileErreur(I.T("The server @1 doesn't answer|105",strFactoryURL));
				}
			}
			catch(Exception e )
			{
				result.EmpileErreur( new CErreurException(e));
				result.EmpileErreur(I.T("Error while SC2IClient initialization|106"));
			}
			return result;
		}
	}
}
