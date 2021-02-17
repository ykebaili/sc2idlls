using System;
using sc2i.common;

namespace sc2i.data
{
	/// ///////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreRechercheAvancee : CComposantFiltreFonction
	{
		/// //////////////////////////////////////
		public CComposantFiltreRechercheAvancee()
			:base()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public override COperateurAnalysable GetOperateur()
		{
			return new COperateurAnalysable ( 0, "HasWords","HASWORDS",false);
		}


		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			return " HasWords ("+
				((CComposantFiltre)Parametres[0]).GetString()+";"+
				((CComposantFiltre)Parametres[1]).GetString()+
				") ";
		}

		

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Parametres.Count != 2 || Parametres[0] == null || !(Parametres[0] is CComposantFiltreChamp) ||
				Parametres[1] == null || !(Parametres[1] is CComposantFiltreConstante ))
			{
				result.EmpileErreur(I.T("HasWords waits for a field type parameter and research string|101"));
			}
			else
			{
				result = ((CComposantFiltre)Parametres[0]).VerifieParametres();
				if ( result )
					result = ((CComposantFiltre)Parametres[1]).VerifieParametres();
			}
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize(serializer );
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		protected override void OnRenommeVariable(string strOldNom, string strNewNom)
		{
			if ( m_nIndexVariableParametre == -1 )
			{
				if ( strOldNom.Length > 1 && strOldNom[0] == '@' )
				{
					try
					{
						m_nIndexVariableParametre = Int32.Parse ( strOldNom.Substring(1) );
					}
					catch{}
				}
			}
			base.OnRenommeVariable (strOldNom, strNewNom);
		}


		/// ///////////////////////////////////////////////////////////
		private int m_nIndexVariableParametre = -1;
		public override CComposantFiltre GetComposantFiltreFinal ( CFiltreData filtre )
		{
			if ( Parametres.Count == 2 )
			{
				string strValeurCherchee = "";
				if ( Parametres[1] is CComposantFiltreVariable )
				{
					if ( m_nIndexVariableParametre != -1 )
						strValeurCherchee = filtre.Parametres[m_nIndexVariableParametre-1].ToString();
				}
				else if (Parametres[1] is CComposantFiltreConstante)
					strValeurCherchee = ((CComposantFiltreConstante)Parametres[1]).Valeur.ToString();
				else
					strValeurCherchee = I.T("ERROR IN HASWORDS VALUE|102");
				if ( Parametres[0] is CComposantFiltreChamp  )
				{
					CResultAErreur result = new CAnalyseurRequeteIntuitive().AnalyseChaine ( strValeurCherchee );
					if ( result.Data is CElementRechercheIntuitive )
					{
						CComposantFiltre cp = ((CElementRechercheIntuitive)result.Data).GetComposantFiltre ( (CComposantFiltreChamp)Parametres[0]);
						return cp;
					}
				}
			}
			return null;
		}
	}
}
