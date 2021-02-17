using System;
using System.Collections;
using sc2i.common;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de COperateurFiltre.
	/// </summary>
	//si l'opérateur est identifié dans la chaine (au début), retourne true,
	//et nPositionApres pointe sur le prochain caractère de la chaine
	/*typiquement, les niveaux sont les suivants : 
	 *
	 * 5 : operateur OU
	 * 4 : operateur ET
	 * 3 : operateurs de tests conditionnels
	 * 2 : operateurs + et -
	 * 1 : operateur * et /
	 * 0 : fonctions, constantes et champs
	 * */
	[Serializable]
	public class COperateurAnalysable
	{
		private int m_nNiveau;
		private string m_strTexte;
		private string m_strIdExpression;
		private bool m_bIsMethode;

		/// //////////////////////////////////////////////////////////////////////////
		public COperateurAnalysable()
		{
		}

		/// //////////////////////////////////////////////////////////////////////////
		public COperateurAnalysable( int nNiveau, string strTexte, string strId, bool bIsMethode)
		{
			m_nNiveau = nNiveau;
			m_strTexte = strTexte;
			m_strIdExpression = strId;
			m_bIsMethode = bIsMethode;
		}

		/// //////////////////////////////////////////////////////////////////////////
		public int Niveau
		{
			get
			{
				return m_nNiveau;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public string Texte
		{
			get
			{
				return m_strTexte;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public string Id
		{
			get
			{
				return m_strIdExpression;
			}
		}


		/// //////////////////////////////////////////////////////////////////////////
		public bool IsMethode
		{
			get 
			{
				return m_bIsMethode;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
            COperateurAnalysable op = (obj as COperateurAnalysable );
			if (op == null)
				return false;
			return op.Id == Id;
		}

		/// //////////////////////////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}


	}

	

}
