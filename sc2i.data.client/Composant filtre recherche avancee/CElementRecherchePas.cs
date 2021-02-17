using System;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CElementRechercheEt.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CElementRecherchePas : CElementRechercheIntuitive
	{
		public CElementRecherchePas()
		{
			
		}

		public static void Autoexec()
		{
			CAllocateurElementRecherche.RegisterType ( typeof(CElementRecherchePas ) );
		}


		public override CMotRecherche[] MotsPossibles
		{
			get
			{
				return new CMotRecherche[]
				{
					new CMotRecherche ( "pas ", 1),
					new CMotRecherche ( "non ", 1),
					new CMotRecherche ( "not ", 1),
				};
			}
		}

		public override string GetString()
		{
			if ( ListeParametres.Count == 1 )
			{
				return MotUtilise + Parametres[0].GetString();
			}
			return I.T("Error not|103");
		}

		public override CComposantFiltre GetComposantFiltre(CComposantFiltreChamp champ)
		{
			CComposantFiltre droite =  null;
			if ( Parametres.Length > 0 && Parametres[0] != null )
				droite = Parametres[0].GetComposantFiltre(champ);
			if ( droite != null )
			{
				CComposantFiltreOperateur op = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurNot );
				op.Parametres.Add ( droite );
				return op;
			}
			return null;
		}

	}
}
