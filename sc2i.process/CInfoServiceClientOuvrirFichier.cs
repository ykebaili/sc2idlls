using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.process
{
	/// <summary>
	/// PErmet de transmettre les infos nécéssaires au service client
	/// d'ouverture de fichier
	/// Un service d'ouverture de fichier attend un CInfoServiceClientOuvrirFichier comme
	/// paramètre de RunService
	/// </summary>
	[Serializable]
	public class CInfoServiceClientOuvrirFichier
	{
		public const string c_idServiceClientOuvrirFichier = "OPEN_FILE";


		private bool m_bWaitForExit = false;

		private string m_strCommandLine = "";
		private string m_strArguments = "";

		public CInfoServiceClientOuvrirFichier(
			string strLigneCommande, 
			string strArguments, 
			bool bWaitForExit)
		{
			m_bWaitForExit = bWaitForExit;
			m_strCommandLine = strLigneCommande;
			m_strArguments = strArguments;
		}

		public bool WaitForExit
		{
			get
			{
				return m_bWaitForExit;
			}
			set
			{
				m_bWaitForExit = value;
			}
		}

		public string CommandLine
		{
			get
			{
				return m_strCommandLine;
			}
			set
			{
				m_strCommandLine = value;
			}
		}

		public string Arguments
		{
			get
			{
				return m_strArguments;
			}
			set
			{
				m_strArguments = value;
			}
		}


	}
}
