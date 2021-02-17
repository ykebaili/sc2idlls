using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.formulaire;

namespace sc2i.formulaire.win32
{
	public delegate CResultAErreur ExecuteActionSur2iLink(object sender, CActionSur2iLink action, object cible);
	public class CExecuteurActionSur2iLink
	{
		//---------------------------------------------------------
		private static ExecuteActionSur2iLink m_executeur;

		//---------------------------------------------------------
		public static ExecuteActionSur2iLink MethodeExec
		{
			get
			{
				return m_executeur;
			}
			set
			{
				m_executeur = value;
			}
		}

		//---------------------------------------------------------
		public static CResultAErreur ExecuteAction ( object sender, CActionSur2iLink action, object cible )
		{
			if ( MethodeExec != null )
				return MethodeExec ( sender, action, cible );
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Execution method is not set|30003"));
			return result;
		}

	}
}
