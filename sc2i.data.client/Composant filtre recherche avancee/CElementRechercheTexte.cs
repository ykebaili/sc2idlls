using System;
using System.Text.RegularExpressions;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de CElementRechercheEt.
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
			
			//Remplace les caract�res accentuables par leurs �quivalents
			strTexte = Regex.Replace ( strTexte, "[����e]","[����e]" );
			strTexte = Regex.Replace ( strTexte, "[a���]","[a���]" );
			strTexte = Regex.Replace ( strTexte, "[i��]","[i��]" );
			strTexte = Regex.Replace ( strTexte, "[o��]","[o��]" );
			strTexte = Regex.Replace ( strTexte, "[u���]","[u���]" );
			strTexte = Regex.Replace ( strTexte, "[c�]","[c�]" );

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
