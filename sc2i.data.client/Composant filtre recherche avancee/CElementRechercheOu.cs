using System;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CElementRechercheEt.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CElementRechercheOu : CElementRechercheIntuitive
	{
		public CElementRechercheOu()
		{
			
		}

		public static void Autoexec()
		{
			CAllocateurElementRecherche.RegisterType ( typeof(CElementRechercheOu ) );
		}


		public override CMotRecherche[] MotsPossibles
		{
			get
			{
				return new CMotRecherche[]
					{
						new CMotRecherche(" ou ",4),
						new CMotRecherche(" or ",4)
					};
			}
		}

		public override string GetString()
		{
			if ( ListeParametres.Count == 1 )
				return Parametres[0].GetString();				
			if ( ListeParametres.Count == 2 )
				return Parametres[0].GetString() + MotUtilise + Parametres[1].GetString();
			return "";
		}

		public override CComposantFiltre GetComposantFiltre(CComposantFiltreChamp champ)
		{
			CComposantFiltre gauche =  null;
			CComposantFiltre droite = null;
			if ( Parametres.Length > 0 && Parametres[0] != null )
				gauche = Parametres[0].GetComposantFiltre(champ);
			if ( Parametres.Length > 1 && Parametres[1] != null )
				droite = Parametres[1].GetComposantFiltre(champ);
			if ( gauche != null && droite != null )
			{
				CComposantFiltreOperateur op = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurOu );
				op.Parametres.Add ( gauche );
				op.Parametres.Add ( droite );
				return op;
			}
			if ( gauche == null && droite != null )
				return droite;
			if ( gauche != null && droite == null )
				return gauche;
			return null;
		}

	}
}
