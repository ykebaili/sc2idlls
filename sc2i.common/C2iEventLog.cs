using System;
using System.Diagnostics;

namespace sc2i.common
{
	/// <summary>
	/// Encapsulation du journal d'évenements de windows.
	/// Ne fonctionne que si l'appel à la fonction init a été appellée.
	/// </summary>
	public enum NiveauBavardage
	{
		VraiPiplette = 0,
		PetiteCausette=5,
		PeuCausant=10
	}


	public sealed class C2iEventLog
	{
		private static EventLog m_eventLog;
		private static NiveauBavardage m_niveauBavardage = NiveauBavardage.PetiteCausette;
		

		private C2iEventLog()
		{
		}

		/// <summary>
		/// Initialise le log d'évenements dans le journal application
		/// </summary>
		/// <param name="strSourceName"></param>
		/// <param name="nNiveauBavardage"></param>
		public static void Init ( string strSourceName, NiveauBavardage nNiveauBavardage )
		{
			Init ( "Application", strSourceName, nNiveauBavardage );
		}

		/// //////////////////////////////////////////////
		/// <summary>
		/// Initialise le log d'évenements dans le journal choisi
		/// </summary>
		/// <param name="strJournal"></param>
		/// <param name="strSourceName"></param>
		/// <param name="nNiveauBavardage"></param>
		public static void Init ( string strJournal, string strSourceName, NiveauBavardage nNiveauBavardage )
		{
            try
            {
                if (!EventLog.SourceExists(strSourceName))
                {
                    EventLog.CreateEventSource(strSourceName, strJournal);
                }
            }
            catch {}
			m_eventLog = new EventLog();
			m_eventLog.Source = strSourceName;
			m_eventLog.Log = strJournal;
			m_niveauBavardage = nNiveauBavardage;
		}

		/// //////////////////////////////////////////////
		/// <summary>
		/// Ecrit une info dans le journal en niveau PetiteCausette
		/// </summary>
		/// <param name="strTexte"></param>
		public static void WriteInfo ( string strTexte )
		{
			WriteInfo ( strTexte, NiveauBavardage.PetiteCausette );
		}

		/// <summary>
		/// Ecrit une info dans le journal
		/// </summary>
		/// <param name="strTexte"></param>
		public static void WriteInfo ( string strTexte, NiveauBavardage niveauRequis )
		{
			WriteEvent ( strTexte, EventLogEntryType.Information, niveauRequis );
		}

		/// //////////////////////////////////////////////
		public static void WriteErreur ( string strTexte )
		{
			WriteEvent ( strTexte, EventLogEntryType.Error, NiveauBavardage.PeuCausant );
			Console.WriteLine ( strTexte );
		}

		public static void WriteEvent ( string strTexte, EventLogEntryType type, NiveauBavardage niveauRequis )
		{
			try
			{
				if ( m_eventLog != null && (int)niveauRequis >= (int)m_niveauBavardage )
					m_eventLog.WriteEntry(strTexte, type);
			}
			catch
			{
			}
		}


	}
}
