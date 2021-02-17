using System;
using System.Collections;
using System.Text;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CPileErreur.
	/// </summary>
	[Serializable]
	public class CPileErreur : IEnumerable
	{
		public static implicit operator IErreur[](CPileErreur pileErr)
		{
			return pileErr.Erreurs;
		}

		protected ArrayList	m_listeErreurs;

		////////////////////////////////////////////////////////
		public CPileErreur()
		{
			m_listeErreurs = new ArrayList();
		}


		////////////////////////////////////////////////////////
		public void EmpileErreur ( IErreur erreur )
		{
			m_listeErreurs.Insert ( 0, erreur );
		}

		////////////////////////////////////////////////////////
		public void EmpileErreur ( string strErreur )
		{
			EmpileErreur ( new CErreurSimple ( strErreur ) );
		}

		////////////////////////////////////////////////////////
		public void EmpileErreurs ( CPileErreur pile )
		{
			foreach ( IErreur erreur in pile.Erreurs )
				EmpileErreur ( erreur );
		}

		////////////////////////////////////////////////////////
		public static CPileErreur operator + ( CPileErreur e1, CPileErreur e2 )
		{
			CPileErreur e = new CPileErreur();
			foreach ( IErreur erreur in e1.Erreurs )
				e.EmpileErreur ( erreur );
			foreach ( IErreur erreur in e2.Erreurs )
				e.EmpileErreur ( erreur );
			return e;
		}

		////////////////////////////////////////////////////////
		public IErreur[] Erreurs
		{
			get 
			{
				return ( IErreur[] ) m_listeErreurs.ToArray ( typeof(IErreur) );
			}
		}

		////////////////////////////////////////////////////////
		public override string ToString()
		{
			string strVal = "";
			int nErreur;
            StringBuilder bl = new StringBuilder();
            for (nErreur = 0; nErreur < m_listeErreurs.Count; nErreur++)
            {
                bl.Append(Erreurs[nErreur].Message);
                bl.Append("\r\n");
            }
            strVal = bl.ToString();
			if ( strVal.Length > 0 )
				strVal = strVal.Substring ( 0, strVal.Length-2 );
			return strVal;
		}

		////////////////////////////////////////////////////////
		public IEnumerator GetEnumerator()
		{
			return m_listeErreurs.GetEnumerator();
		}

		////////////////////////////////////////////////////////
		public void Add ( object obj )
		{
			m_listeErreurs.Add ( obj );
		}
		

	}
}
