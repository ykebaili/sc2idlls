using System;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CElementRechercheEt.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CElementRechercheEt : CElementRechercheIntuitive
	{
		public CElementRechercheEt()
		{
			
		}

		public static void Autoexec()
		{
			CAllocateurElementRecherche.RegisterType ( typeof(CElementRechercheEt ) );
		}


		public override CMotRecherche[] MotsPossibles
		{
			get
			{
				return new CMotRecherche[]
					{
						new CMotRecherche(",",6),
						new CMotRecherche(" et ",2),
						new CMotRecherche(" and ",2),
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
				CComposantFiltreOperateur op = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurEt );
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
