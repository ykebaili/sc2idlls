using System;
using System.Text.RegularExpressions;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CElementRechercheEt.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CElementRechercheTexte : CElementRechercheIntuitive
	{

		////////////////////////////////////////////////////////////////////////////
		public CElementRechercheTexte()
		{
			
		}

		////////////////////////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateurElementRecherche.RegisterType ( typeof(CElementRechercheTexte ) );
		}


		////////////////////////////////////////////////////////////////////////////
		public override CMotRecherche[] MotsPossibles
		{
			get
			{
				return new CMotRecherche[0];
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public override string GetString()
		{
			return MotUtilise;
		}

		////////////////////////////////////////////////////////////////////////////
		public override CComposantFiltre GetComposantFiltre(CComposantFiltreChamp champ)
		{
			string strSeparateursMots = " ,;:!.?()-*+/\'";
			string strTexte = MotUtilise;
			
			//Remplace les caractères accentuables par leurs équivalents
			strTexte = Regex.Replace ( strTexte, "[éèêëe]","[éèêëe]" );
			strTexte = Regex.Replace ( strTexte, "[aàäâ]","[aàäâ]" );
			strTexte = Regex.Replace ( strTexte, "[iîï]","[iîï]" );
			strTexte = Regex.Replace ( strTexte, "[oôö]","[oôö]" );
			strTexte = Regex.Replace ( strTexte, "[uüûù]","[uüûù]" );
			strTexte = Regex.Replace ( strTexte, "[cç]","[cç]" );

			CComposantFiltre ou = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurOu);
			
			CComposantFiltre like = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurLike);
			like.Parametres.Add ( champ );
			like.Parametres.Add ( new CComposantFiltreConstante ( strTexte+"%" ));
			ou.Parametres.Add ( like );

			like = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurLike);
			like.Parametres.Add ( champ );
			like.Parametres.Add ( new CComposantFiltreConstante ( "%["+strSeparateursMots+"]"+strTexte+"%" ));
			ou.Parametres.Add ( like );

			return ou;
		}


	}
}
