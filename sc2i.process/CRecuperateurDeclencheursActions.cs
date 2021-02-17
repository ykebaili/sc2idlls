using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.process
{
	/// <summary>
	/// Sait retourner tous les déclencheurs associés à un élément
	/// </summary>
	public class CRecuperateurDeclencheursActions
	{

		/// <summary>
		/// Retourne tous les déclencheurs associés à un élément
		/// Evenements et handler
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		
		public static IDeclencheurAction[] GetDeclencheursAssocies ( CObjetDonneeAIdNumerique objet )
		{
			if ( objet == null )
				return new IDeclencheurAction[0];
			ArrayList listeDeclencheurs = new ArrayList();
			
			//Cherche les évenements
			CListeObjetsDonnees listeEvenements = new CListeObjetsDonnees ( objet.ContexteDonnee, typeof ( CEvenement ) );
			listeEvenements.Filtre = new CFiltreData ( 
				CEvenement.c_champTypeCible+"=@1 ",
				objet.GetType().ToString());
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( objet) ;
			CResultAErreur result = CResultAErreur.True;

			foreach ( CEvenement evenement in listeEvenements )
			{
				bool bShouldAdd = true;
				if ( evenement.HasDefinisseur() )
				{
					if ( evenement.TypeDefinisseur == typeof(CComportementGenerique) )
					{
						//Regarde s'il existe une association entre l'élément et le comportement
						CRelationElementComportement relation = new CRelationElementComportement ( objet.ContexteDonnee );
						if ( !relation.ReadIfExists ( new CFiltreData ( 
							CRelationElementComportement.c_champTypeElement+"=@1 and "+
							CRelationElementComportement.c_champIdElement+"=@2 and "+
							CComportementGenerique.c_champId+"=@3",
							objet.GetType().ToString(),
							objet.Id,
							evenement.IdDefinisseur ) ) )
						{
							//Pas de lien vers le comportement. Est-ce que l'élément a un définisseur qui est lié à ce comportement .
							if ( objet is IElementAEvenementsDefinis )
							{
								bool bIsInComp = false;
								foreach ( IDefinisseurEvenements defi in ((IElementAEvenementsDefinis)objet).Definisseurs)
									foreach ( CComportementGenerique comp in defi.ComportementsInduits )
										if ( comp.Id == evenement.IdDefinisseur )
										{
											bIsInComp = true;
											break;
										}
								bShouldAdd = bIsInComp;
							}
							else
								bShouldAdd = false;
						}
					}
					//si le définisseur n'est pas un comportement générique
					if ( !(objet is IElementAEvenementsDefinis) )
						bShouldAdd = false;
					else if ( !((IElementAEvenementsDefinis)objet).IsDefiniPar ( evenement.Definisseur ) )
						bShouldAdd = false;
					else
						bShouldAdd = true;
				}
				if ( bShouldAdd )
					listeDeclencheurs.Add ( evenement );
			}

			//cherche les handlers
			CListeObjetsDonnees listeHandlers = new CListeObjetsDonnees ( objet.ContexteDonnee, typeof(CHandlerEvenement));
			listeHandlers.Filtre = new CFiltreData ( 
				CHandlerEvenement.c_champTypeCible+"=@1 and "+
				CHandlerEvenement.c_champIdCible+"=@2 ",
				objet.GetType().ToString(),
				objet.Id );
			foreach ( CHandlerEvenement handler in listeHandlers )
			{
				listeDeclencheurs.Add ( handler );
			}

			listeDeclencheurs.Sort ( new CDeclencheurComparer() );
			return (IDeclencheurAction[])listeDeclencheurs.ToArray(typeof(IDeclencheurAction) );
		}


		/// <summary>
		/// Récupère toutes les actions manuelles associées à un élément
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static IDeclencheurAction[] GetActionsManuelles ( CObjetDonneeAIdNumerique objet, bool bAvecHandlers )
		{
			if ( objet == null )
				return new IDeclencheurAction[0];
            HashSet<CDbKey> tableGroupesUtilisateurs = null;
			ArrayList listeDeclencheurs = new ArrayList();
			//Cherche les évenements
			CListeObjetsDonnees listeEvenements = new CListeObjetsDonnees ( objet.ContexteDonnee, typeof ( CEvenement ) );
			listeEvenements.Filtre = new CFiltreData ( 
				CEvenement.c_champTypeCible+"=@1 and "+
				CEvenement.c_champTypeEvenement+"=@2",
				objet.GetType().ToString(),
				(int)TypeEvenement.Manuel );
			
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( objet) ;
			CResultAErreur result = CResultAErreur.True;


			foreach ( CEvenement evenement in listeEvenements )
			{
				bool bShouldAdd = true;
				if ( evenement.HasDefinisseur() )
				{
					if ( evenement.TypeDefinisseur == typeof(CComportementGenerique) )
					{
						//Regarde s'il existe une association entre l'élément et le comportement
						CRelationElementComportement relation = new CRelationElementComportement ( objet.ContexteDonnee );
						if ( !relation.ReadIfExists ( new CFiltreData ( 
							CRelationElementComportement.c_champTypeElement+"=@1 and "+
							CRelationElementComportement.c_champIdElement+"=@2 and "+
							CComportementGenerique.c_champId+"=@3",
							objet.GetType().ToString(),
							objet.Id,
							evenement.IdDefinisseur ) ) )
						{
							//Pas de lien vers le comportement. Est-ce que l'élément a un définisseur qui est lié à ce comportement .
							if ( objet is IElementAEvenementsDefinis )
							{
								bool bIsInComp = false;
								foreach ( IDefinisseurEvenements defi in ((IElementAEvenementsDefinis)objet).Definisseurs)
									foreach ( CComportementGenerique comp in defi.ComportementsInduits )
										if ( comp.Id == evenement.IdDefinisseur )
										{
											bIsInComp = true;
											break;
										}
								bShouldAdd = bIsInComp;
							}
							else
								bShouldAdd = false;
						}
					}
					else if ( !(objet is IElementAEvenementsDefinis) )//si le définisseur n'est pas un comportement générique
						bShouldAdd = false;
					else if ( !((IElementAEvenementsDefinis)objet).IsDefiniPar ( evenement.Definisseur ) )
						bShouldAdd = false;
					else
						bShouldAdd = true;
				}
				if ( bShouldAdd )
				{
					//Vérifie que l'evenemnt n'a pas déjà été déclenché pour cet entité
					if ( !evenement.DeclenchementUniqueParEntite || !
						evenement.DejaDeclenchePourEntite ( objet ) )
					{
						if ( CanUserDeclenche ( objet.ContexteDonnee.IdSession, evenement.KeysGroupesPourExecutionManuelle, tableGroupesUtilisateurs ) )
						{
							C2iExpression condition = evenement.FormuleCondition;
							if ( condition == null )
								listeDeclencheurs.Add ( evenement );
							else
							{
								result = evenement.FormuleCondition.Eval ( contexteEval );
								if ( result )
								{
									if ( result.Data != null &&
										result.Data.ToString()!="0" &&
										result.Data.ToString().ToUpper() != false.ToString().ToUpper() )
										listeDeclencheurs.Add ( evenement );
								}
							}
						}
					}
				}
			}

			if ( bAvecHandlers )
			{
				//cherche les handlers
				CListeObjetsDonnees listeHandlers = new CListeObjetsDonnees ( objet.ContexteDonnee, typeof(CHandlerEvenement));
				listeHandlers.Filtre = new CFiltreData ( 
					CHandlerEvenement.c_champTypeCible+"=@1 and "+
					CHandlerEvenement.c_champIdCible+"=@2 and "+
					CHandlerEvenement.c_champTypeEvenement+"=@3",
					objet.GetType().ToString(),
					objet.Id,
					(int)TypeEvenement.Manuel );
				foreach ( CHandlerEvenement handler in listeHandlers )
				{
					C2iExpression condition = handler.FormuleCondition;
					if ( condition == null )
						listeDeclencheurs.Add ( handler );
					else
					{
						if ( CanUserDeclenche ( objet.ContexteDonnee.IdSession, handler.KeysGroupesPourExecutionManuelle, tableGroupesUtilisateurs ) )
						{
							result = handler.FormuleCondition.Eval ( contexteEval );
							if ( result )
							{
								if ( result.Data != null &&
									result.Data.ToString()!="0" &&
									result.Data.ToString().ToUpper() != false.ToString().ToUpper() )
									listeDeclencheurs.Add ( handler );
							}
						}
					}
				}
			}
			listeDeclencheurs.Sort ( new CDeclencheurComparer() );
			return (IDeclencheurAction[])listeDeclencheurs.ToArray(typeof(IDeclencheurAction) );
		}

		
		/// <summary>
		/// Indique si l'utilisateur d'une session a le droit d'executer une tache.
		/// Le paramètre tableGroupesUtilisateurs est alloué si nécéssaire (pour cache)
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <param name="parametre"></param>
		/// <param name="tableGroupesUtilisateur"></param>
		/// <returns></returns>
        private static bool CanUserDeclenche(int nIdSession, CDbKey[] listeKeysGroupesEvenement, HashSet<CDbKey> tableGroupesUtilisateur)
        {
            if (listeKeysGroupesEvenement.Length == 0)
                return true;
            if (tableGroupesUtilisateur == null)
            {
                try
                {
                    tableGroupesUtilisateur = new HashSet<CDbKey>();
                    CSessionClient session = CSessionClient.GetSessionForIdSession(nIdSession);
                    foreach (CDbKey keyGroupe in session.GetInfoUtilisateur().ListeKeysGroupes)
                    {
                        //TESTDBKEYOK les groupes pour exécution manuelle ne sont plus exploités (Avril 2014)
                        if (keyGroupe != null)
                            tableGroupesUtilisateur.Add(keyGroupe);
                    }
                }
                catch
                {
                }
            }
            foreach (CDbKey keyGroupe in listeKeysGroupesEvenement)
            {
                if ( keyGroupe != null && tableGroupesUtilisateur.Contains(keyGroupe) )
                    return true;
            }
            return false;
        }

		private class CDeclencheurComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if ( x is IDeclencheurAction && y is IDeclencheurAction )
					return ((IDeclencheurAction)x).Libelle.CompareTo(((IDeclencheurAction)y).Libelle );
				return -1;
			}

		}

				
				

	}
}
