using System;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Valide contenant un objet donnée pouvant passer se
	/// sérialiser et se déserialiser
	/// </summary>
#if PDA
	#region Classe pour PDA
	public class CValiseObjetDonnee : IDisposable
	{
		private CObjetDonnee m_objet = null;
		
		/// //////////////////////////////////////////////////////
		public CValiseObjetDonnee()
		{
		}

		/// //////////////////////////////////////////////////////
		~CValiseObjetDonnee()
		{
			Dispose();
		}

		/// //////////////////////////////////////////////////////
		public void Dispose()
		{
		}

		/// <summary>
		/// Crée une valide pour emporter l'objet passé en paramètre
		/// </summary>
		/// <param name="obj"></param>
		
		public CValiseObjetDonnee( CObjetDonnee obj )
		{
			m_objet = obj;
		}

		/// <summary>
		/// Retourne une nouvelle instance de l'objet contenu dans la valise
		/// </summary>
		/// <returns></returns>
		public CObjetDonnee GetObjet()
		{
			return m_objet;
		}
	}
	#endregion


#else
	[Serializable]
	public class CValiseObjetDonnee : IDisposable
	{
		private CContexteDonnee m_contexte;
		private string m_strTable;
		private object[] m_cles;
		
		/// //////////////////////////////////////////////////////
		public CValiseObjetDonnee()
		{
		}

		/// //////////////////////////////////////////////////////
		~CValiseObjetDonnee()
		{
			Dispose();
		}

		/// //////////////////////////////////////////////////////
		public void Dispose()
		{
			if (m_contexte !=null )
			{
				m_contexte.Dispose();
				m_contexte = null;
			}
		}

		/// <summary>
		/// Crée une valide pour emporter l'objet passé en paramètre
		/// </summary>
		/// <param name="obj"></param>
		
		public CValiseObjetDonnee( CObjetDonnee obj )
		{
			m_contexte = obj.ContexteDonnee.GetMiniDataSetFor (obj);
			m_cles = obj.GetValeursCles();
			m_strTable = obj.GetNomTable();
		}

		/// <summary>
		/// Retourne une nouvelle instance de l'objet contenu dans la valise
		/// </summary>
		/// <returns></returns>
		public CObjetDonnee GetObjet()
		{
			DataTable table = m_contexte.Tables[m_strTable];
			DataRow row = table.Rows.Find ( m_cles );
			m_contexte.SetEnableAutoStructure(true);
			return m_contexte.GetNewObjetForRow(row);
		}

		/// //////////////////////////////////////////////////////
		public void AddElementToValise ( CObjetDonnee obj )
		{
			if ( obj == null )
				return;
			obj.ContexteDonnee.CopieRowTo ( obj.Row, m_contexte, true, true, false );
		}
			


		
	}
#endif
}
