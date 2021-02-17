using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Classe de base pour les fonctions utilisant Sql
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionBaseSql : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionBaseSql()
		{
		}

	
		/// //////////////////////////////////////////
		public CResultAErreur VerifieParametreFiltre ( C2iExpression parametreCorrespondantAuFiltre )
		{
			CResultAErreur result = CResultAErreur.True;
			//Le premier paramètre de filtre ne doit être composé que d'accès à des champs.
			//Chaque champ doit être un objetdonnee (pour être mappé sur SQL)
			C2iExpression parametre = parametreCorrespondantAuFiltre;
			if ( parametre == null )
			{
				result.EmpileErreur(I.T("SelectSql : Filter parameter|206"));
				return result;
			}
			bool bOk = true;
			Type typeRetourne = null;
			while ( bOk && parametre != null )
			{
				if ( parametre is C2iExpressionChamp )
				{
                    if (parametre.TypeDonnee != null)
                        typeRetourne = parametre.TypeDonnee.TypeDotNetNatif;
                    else
                        return result;
					parametre = null;
				}
				else
				{
					if ( parametre is C2iExpressionObjet )
					{
						C2iExpression expObj = (C2iExpressionObjet)parametre;
						if ( expObj.Parametres[0] is C2iExpressionChamp)//Ok, le premier est bien un champ
						{
							parametre = expObj.Parametres2i[1];
							bOk = true;
						}
						else
							bOk = false;
						if ( !bOk )
						{
							result.EmpileErreur(I.T("The first filter parameter must be composed only of fields directly linked to a database table|207"));
							return result;
						}
					}
				}
			}


			if ( typeRetourne == null || CContexteDonnee.GetNomTableForType ( typeRetourne ) == null )
			{
				result.EmpileErreur(I.T("It is not possible to filter the elements of type @1 in SQL|208", DynamicClassAttribute.GetNomConvivial ( typeRetourne ) ));
				return result;
			}

			if ( typeRetourne == null )
			{
				result.EmpileErreur(I.T("Impossible to analyze the parameters of SelectSql function|209"));
				return result;
			}
			return result;
		}

		/// //////////////////////////////////////////
		protected CResultAErreur GetFiltre( 
			CContexteEvaluationExpression ctx,
			C2iExpression expressionAcces,
			C2iExpression expressionFiltre,
			params C2iExpression[] parametresFiltre)
		{
			CResultAErreur result = CResultAErreur.True;
			//Commence par créer le filtre qui permet d'accéder au type accedé (basé sur le premier paramètre
			/*Pour créer ce filtre, il faut remonter à l'envers depuis le type retourné
				 * vers le type source de l'expression.
				 * PAr exemple Classement.Lignes depuis lotproduit doit être inversé pour 
				 * indiquer le lot de produit du classement des lignes
				 * (classement.LotProduitOrigine)
				 * 
				 * */
			string strFiltreToType = "";
			C2iExpression parametre = expressionAcces;

			ArrayList lstPropsAccedees = new ArrayList();

			/*if (!(ctx.ObjetSource is CObjetDonnee ))
			{
				result.EmpileErreur("La fonction SelectSql ne peut pas être appliquée ici ");
				return result;
			}*/


			Type tp = ctx.ObjetSource.GetType();

			CObjetDonnee objetRacine = (CObjetDonnee)ctx.ObjetSource;
			
	
			while ( parametre != null )
			{
				string strTable = CContexteDonnee.GetNomTableForType(tp);
				if ( strTable == null )
				{
					result.EmpileErreur(I.T("The SelectSql function cannot be applied on an element @1|210",	DynamicClassAttribute.GetNomConvivial(tp) ));
					return result;
				}
				C2iExpressionChamp champ;
				if ( parametre is C2iExpressionChamp )
					champ = (C2iExpressionChamp)parametre;
				else if ( parametre is C2iExpressionObjet &&  parametre.Parametres2i[0] is C2iExpressionChamp )
				{
					champ = (C2iExpressionChamp)parametre.Parametres2i[0];
				}
				else
				{
					result.EmpileErreur(I.T("Error in the SelectSql parameter|211"));
					return result;
				}
				CDefinitionProprieteDynamique def = champ.DefinitionPropriete;
				PropertyInfo info = tp.GetProperty ( def.NomProprieteSansCleTypeChamp );				
				if ( info == null )
				{
					result.EmpileErreur(I.T("The property @1 is not valid for SelectSql(*)|212", def.NomPropriete));
					return result;
				}
				//Il faut trouver la propriété de l'élément accedé utilisée pour atteindre le type précédent
				Type tpAccede = null;
				//Si la propriété a l'attribut relation fille, on va trouver l'info
				object[] attribs = info.GetCustomAttributes( typeof(RelationFilleAttribute), true );
				if ( attribs.Length != 0 )
				{
					RelationFilleAttribute attr = (RelationFilleAttribute)attribs[0];
					strFiltreToType = attr.ProprieteFille+"."+strFiltreToType;
					tpAccede = attr.TypeFille;
					strTable = CContexteDonnee.GetNomTableForType ( tpAccede );
				}
				else 
				{
					//Pas une relation fille, est-ce une relation parente ?
					attribs = info.GetCustomAttributes(typeof(RelationAttribute), true );
					if ( attribs.Length != 0 )
					{
						if ( objetRacine.IsNew() )
						{
							//L'objet racine est un nouvel objet, il n'est donc pas dans la base,
							//Donc la requête ne retournera rien !!!. Il faut donc utiliser 
							//le prochain élément comme objetRacine
							objetRacine = (CObjetDonnee)info.GetGetMethod().Invoke(objetRacine, new object[0]);
							if ( objetRacine != null )
								tpAccede = objetRacine.GetType();
						}
						else
						{
							RelationAttribute attr = (RelationAttribute)attribs[0];
							strFiltreToType = attr.GetInfoRelation ( strTable ).RelationKey+"."+strFiltreToType;
							tpAccede = info.PropertyType;
							strTable = CContexteDonnee.GetNomTableForType(tpAccede);
						}
					}
					else
					{
						// on est mal, impossible de savoir à quoi on fait référence !
						result.EmpileErreur(I.T("The property @1 is not valid for a SelectSql operation|213",def.NomPropriete));
						return result;
					}
				}
				tp = tpAccede;
				if ( parametre is C2iExpressionObjet )
					parametre = parametre.Parametres2i[1];
				else
					parametre = null;
			}
			
			//Crée le filtre
			CObjetDonnee objetSource = objetRacine;
			CFiltreDataAvance filtre = new CFiltreDataAvance (CContexteDonnee.GetNomTableForType(tp),"");
			int nCle = 1;
			foreach ( string strCle in objetSource.GetChampsId() )
			{
				filtre.Filtre += strFiltreToType+strCle+"=@"+nCle.ToString();
				nCle++;
				filtre.Parametres.Add ( objetSource.Row[strCle ] );
			}
			
			//Voila, on n'a plus qu'à combiner avec le filtre demandé
			//evaluation du paramètre 1
			result = expressionFiltre.Eval ( ctx );
			if ( !result)
			{
				result.EmpileErreur(I.T("Error in SelectSql filter|214"));
				return result;
			}
			if ( result.Data.ToString().Trim() != "" )
			{
				CFiltreDataAvance filtre2 = new CFiltreDataAvance ( CContexteDonnee.GetNomTableForType(tp), result.Data.ToString() );
				//L'analyseur de filtre ne comprend pas la syntaxe [CHAMP].[Champ], mais comprend
				//[Champ.Champ], il faut donc remplacer tous les ].[ par des .
				filtre2.Filtre = filtre2.Filtre.Replace("].[",".");
				foreach ( C2iExpression expression in parametresFiltre )
				{
					result = expression.Eval ( ctx );
					if ( !result )
						return result;
					filtre2.Parametres.Add ( result.Data );
				}
				try
				{
					filtre = (CFiltreDataAvance)CFiltreData.GetAndFiltre ( filtre, filtre2 );
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
					result.EmpileErreur(I.T("Error during the creation of a SelectSql filter|215"));
					return result;
				}
			}
			result.Data = filtre;
			return result;
		}


        public override CTypeResultatExpression[] TypesObjetSourceAttendu
        {
            get
            {
                return new CTypeResultatExpression[]
					{
						new CTypeResultatExpression(typeof(CObjetDonnee), false)
					};
            }
        }


        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
        {
            return objetSource;
        }

    }
}
