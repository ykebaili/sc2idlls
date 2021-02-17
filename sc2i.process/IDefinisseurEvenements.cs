using System;
using System.Collections;

using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Tout �l�ment qui d�finit des �v�nements
	/// </summary>
	public interface IDefinisseurEvenements : IObjetDonneeAIdNumeriqueAuto
	{
		//Retourne la liste des types qui peuvent �tre cible de ce d�finisseur
		Type[] TypesCibleEvenement{get;}

		//Retourne tous les �venements associ�s
		CListeObjetsDonnees Evenements{get;}

		/// <summary>
		/// Retourne tous les comportements g�n�riques induits par ce d�finisseur
		/// </summary>
		CComportementGenerique[] ComportementsInduits {get;}
	}

	public class CUtilDefinisseurEvenement
	{
		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne les �venements propres � un d�finisseur (pas les induits)
		/// </summary>
		/// <param name="definisseur"></param>
		/// <returns></returns>
		public static CListeObjetsDonnees GetEvenementsFor ( IDefinisseurEvenements definisseur )
		{
			CFiltreData filtre = new CFiltreData (
				CEvenement.c_champTypeDefinisseur+"=@1 and "+
				CEvenement.c_champIdDefinisseur+"=@2",
				definisseur.GetType().ToString(),
				definisseur.Id );
			CListeObjetsDonnees listeEvenements = new CListeObjetsDonnees(((CObjetDonnee)definisseur).ContexteDonnee, typeof(CEvenement), filtre);
			return listeEvenements;
		}

		/// <summary>
		/// Retourne tous les comportements g�n�riques induits par un d�finisseur
		/// </summary>
		/// <param name="definisseur"></param>
		/// <returns></returns>
		public static CComportementGenerique[] GetComportementsInduits ( IDefinisseurEvenements definisseur )
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( ((CObjetDonnee)definisseur).ContexteDonnee, typeof(CRelationDefinisseurComportementInduit) );
			liste.Filtre = new CFiltreData ( 
				CRelationDefinisseurComportementInduit.c_champTypeDefinisseur+"=@1 and "+
				CRelationDefinisseurComportementInduit.c_champIdDefinisseur+"=@2",
				definisseur.GetType().ToString(),
				definisseur.Id );
			CComportementGenerique[] lst = new CComportementGenerique[liste.Count];
			int nIndex = 0;
			foreach ( CRelationDefinisseurComportementInduit rel in liste )
			{
				lst[nIndex++] = rel.Comportement;
			}
			
			return lst;
		}

		public static void AddComportementInduit ( IDefinisseurEvenements definisseur, CComportementGenerique comportement )
		{
			CRelationDefinisseurComportementInduit relation = new CRelationDefinisseurComportementInduit ( ((CObjetDonnee)definisseur).ContexteDonnee);
			if ( relation.ReadIfExists ( 
				new CFiltreData (
				CRelationDefinisseurComportementInduit.c_champTypeDefinisseur+"=@1 and "+
				CRelationDefinisseurComportementInduit.c_champIdDefinisseur+"=@2 and "+
				CComportementGenerique.c_champId+"=@3",
				definisseur.GetType().ToString(),
				definisseur.Id,
				comportement.Id
				) ) )
				return;
			relation.CreateNewInCurrentContexte();
			relation.Comportement = comportement;
			relation.DefinisseurAssocie = (CObjetDonneeAIdNumerique)definisseur;
		}

		public static void RemoveComportementInduit ( IDefinisseurEvenements definisseur, CComportementGenerique comportement )
		{
			CRelationDefinisseurComportementInduit relation = new CRelationDefinisseurComportementInduit ( comportement.ContexteDonnee);
			if ( relation.ReadIfExists ( 
				new CFiltreData (
				CRelationDefinisseurComportementInduit.c_champTypeDefinisseur+"=@1 and "+
				CRelationDefinisseurComportementInduit.c_champIdDefinisseur+"=@2 and "+
				CComportementGenerique.c_champId+"=@3",
				definisseur.GetType().ToString(),
				definisseur.Id,
				comportement.Id
				) ) )
				relation.Delete();
		}

	}
}
