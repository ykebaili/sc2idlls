using System;
using System.Collections;
using sc2i.expression;

using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamiqueOperateur.
	/// </summary>
	[Serializable]
	public abstract class CComposantFiltreDynamiqueOperateurLogique : CComposantFiltreDynamique
	{
		private ArrayList m_listeComposantsFils = new ArrayList();

		/// <summary>
		/// ///////////////////////////////////////////////
		/// </summary>
		public CComposantFiltreDynamiqueOperateurLogique()
		{
		}

		/// ///////////////////////////////////////////////
		public override CComposantFiltreDynamique[] ListeComposantsFils
		{
			get
			{
				return (CComposantFiltreDynamique[])m_listeComposantsFils.ToArray(typeof(CComposantFiltreDynamique) );
			}
		}

		/// //////////////////////////////////////////////////
		public override bool AccepteComposantsFils
		{
			get
			{
				return true;
			}
		}

		/// ///////////////////////////////////////////////
		public override bool AddComposantFils( CComposantFiltreDynamique composant )
		{
			m_listeComposantsFils.Add ( composant );
			return true;
		}

		/// //////////////////////////////////////////////////
		public int GetNbComposantsFils()
		{
			return m_listeComposantsFils.Count;
		}

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamique[] ComposantsFils
		{
			get
			{
				return (CComposantFiltreDynamique[])m_listeComposantsFils.ToArray(typeof(CComposantFiltreDynamique));
			}
		}

		/// //////////////////////////////////////////////////
		public override void InsereDefinitionToObjetFinal ( CDefinitionProprieteDynamique def )
		{
			foreach ( CComposantFiltreDynamique composant in m_listeComposantsFils )
			{
				if ( composant != null )
					composant.InsereDefinitionToObjetFinal ( def );
			}
		}

		/// ///////////////////////////////////////////////
		public override bool InsertComposantFils ( CComposantFiltreDynamique composant, int nIndex )
		{
			if ( nIndex >= m_listeComposantsFils.Count )
				return AddComposantFils ( composant );
			m_listeComposantsFils.Insert ( nIndex, composant );
			return true;
		}
	
		/// ///////////////////////////////////////////////
		public override int RemoveComposantFils ( CComposantFiltreDynamique composant )
		{
			try
			{
				int nIndex = m_listeComposantsFils.IndexOf(composant);
				m_listeComposantsFils.RemoveAt ( nIndex );
				return nIndex;
			}
			catch
			{
				return m_listeComposantsFils.Count;
			}
			
		}

		

		/// ///////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = serializer.TraiteArrayListOf2iSerializable ( m_listeComposantsFils );
			return result;
		}
	

		/// ///////////////////////////////////////////////////
		public abstract string GetIdComposantFiltreOperateur();

		/// ///////////////////////////////////////////////////
		public override CResultAErreur GetComposantFiltreData(CFiltreDynamique filtre, CFiltreData filtreData)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( m_listeComposantsFils.Count < 1 )
					return result;
				result = ((CComposantFiltreDynamique)m_listeComposantsFils[0]).GetComposantFiltreData(filtre, filtreData);
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in element @1|136",((CComposantFiltreDynamique)m_listeComposantsFils[0]).Description));
					result.Data = null;
					return result;
				}
				CComposantFiltre composantAvant = (CComposantFiltre)result.Data;
				for ( int nComposant = 1; nComposant < m_listeComposantsFils.Count; nComposant++ )
				{
					CComposantFiltreDynamique composantDynamique = (CComposantFiltreDynamique)m_listeComposantsFils[nComposant];
					result = composantDynamique.GetComposantFiltreData(filtre, filtreData);
					if ( !result )
					{
						result.EmpileErreur(I.T("Error in element @1|136", ((CComposantFiltreDynamique)m_listeComposantsFils[nComposant]).Description));
						result.Data = null;
						return result;
					}
					if ( result.Data != null )
					{
						if ( composantAvant != null )
						{
							CComposantFiltreOperateur parenthesesNew = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurParentheses );
							CComposantFiltreOperateur parenthesesOld = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurParentheses );
							parenthesesNew.Parametres.Add ( result.Data );
							parenthesesOld.Parametres.Add ( composantAvant );
							CComposantFiltreOperateur operateur = new CComposantFiltreOperateur ( GetIdComposantFiltreOperateur() );
							operateur.Parametres.Add ( parenthesesOld );
							operateur.Parametres.Add ( parenthesesNew );
							composantAvant = operateur;
						}
						else
							composantAvant = (CComposantFiltre)result.Data;
					}
				}
				result.Data = composantAvant;
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException (e) ) ;
				return result;
			}
		}

        /// ///////////////////////////////////////////////////
        protected abstract C2iExpression GetExpressionOperateurLogique();

        /// ///////////////////////////////////////////////////
        public override CResultAErreur GetComposantExpression(CFiltreDynamique filtre)
        {
            CResultAErreur result = CResultAErreur.True;
            C2iExpression expressionPrec = null;
            foreach (CComposantFiltreDynamique cp in ComposantsFils)
            {
                result = cp.GetComposantExpression(filtre);
                if (result)
                {
                    C2iExpression exp = result.Data as C2iExpression;
                    if (exp != null)
                    {
                        if (expressionPrec != null)
                        {
                            C2iExpression expLogique = GetExpressionOperateurLogique();
                            expLogique.Parametres.Add(expressionPrec);
                            expLogique.Parametres.Add(exp);
                            expressionPrec = expLogique;
                        }
                        else
                            expressionPrec = exp;
                    }
                }
                else
                    return result;
            }
            result.Data = expressionPrec;
            return result;
        }

		/// /////////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieIntegrite( CFiltreDynamique filtre )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_listeComposantsFils.Count == 0 )
			{
				result.EmpileErreur(I.T("The component @1 has no sub-component|138",Description));
				return result;
			}
			foreach ( CComposantFiltreDynamique composant in m_listeComposantsFils )
			{
				CResultAErreur resultTmp = composant.VerifieIntegrite ( filtre );
				if ( !resultTmp )
				{
					result.Erreur.EmpileErreurs ( resultTmp.Erreur );
					result.EmpileErreur(I.T("Error in component '@1'|137",composant.Description));
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////
		public override bool IsVariableUtilisee ( CVariableDynamique variable )
		{
			foreach ( CComposantFiltreDynamique composant in m_listeComposantsFils )
			{
				if ( composant.IsVariableUtilisee ( variable ) )
					return true;
			}
			return false;
		}

		/// //////////////////////////////////////////////////
		public override void OnChangeVariable ( CFiltreDynamique filtre,CVariableDynamique variable )
		{
			foreach ( CComposantFiltreDynamique composant in m_listeComposantsFils )
				composant.OnChangeVariable( filtre, variable );
		}
	}


}
