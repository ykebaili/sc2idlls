using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.multitiers.client;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Crée une liste d'objets en utilisant un filtre SC2I
	/// </summary>
	/// <remarks>
	/// <LI>
	/// Le premier paramètre de la fonction indique le type des éléments à renvoyer.
	/// Il ne peut être composé que de références à des champs.
	/// </LI>
	/// <LI>
	/// Le second paramètre contient le filtre à appliquer (voir syntaxe de filtres)
	/// </LI>
	/// <LI>
	/// Les paramètres suivants représentent les valeurs des paramètres du filtre
	/// </LI>
	/// </remarks>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionListeObjets : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionListeObjets()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionListeObjets().IdExpression,
				typeof(C2iExpressionListeObjets) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "ObjectList", 
				new CTypeResultatExpression(typeof(object), true),
				I.TT(GetType(), "ObjectList(Type, filter, parameters)\n Create an element list applying a filter|224"),
				CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(string), typeof(string)) );
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(Type), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;//Nombre variable
		}


		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count >= 1 )
				{
					C2iExpression expType = Parametres2i[0];
                    if (expType is C2iExpressionTypesDynamics)
                    {
                        return new CTypeResultatExpression(((C2iExpressionTypesDynamics)expType).TypeReprésenté, true);
                    }
					if ( expType is C2iExpressionConstante )
					{
						string strType = ((C2iExpressionConstante)expType).Valeur.ToString();
						Type tp = CActivatorSurChaine.GetType(strType, true);
						if ( tp != null )
							return  new CTypeResultatExpression ( tp, true );
					}
				}
				return new CTypeResultatExpression ( typeof(CObjetDonnee), true );
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strVal = listeParametres[0].ToString();
                Type tp = listeParametres[0] as Type;
                if ( tp == null )
                    tp = CActivatorSurChaine.GetType(strVal, true);
				if (tp == null)
					tp = CActivatorSurChaine.GetType(strVal, false);
				if (tp == null)
				{
					result.EmpileErreur(I.T("The @1 type does not exist|221", strVal));
					return result;
				}
				string strTable = CContexteDonnee.GetNomTableForType(tp);
				if (strTable == null)
				{
					result.EmpileErreur(I.T("The @1 type is not associated with a table|225", strVal));
					return result;
				}
				CFiltreDataAvance filtreAvance = new CFiltreDataAvance(strTable, listeParametres[1].ToString());
				for (int n = 2; n < listeParametres.Length; n++)
					filtreAvance.Parametres.Add(listeParametres[n]);
                CComposantFiltre c = filtreAvance.ComposantPrincipal;

				CContexteDonnee contexteDonnee = null;
				if ( ctx.ObjetSource is IObjetAContexteDonnee )
					contexteDonnee = ((IObjetAContexteDonnee)ctx.ObjetSource).ContexteDonnee;
				if ( contexteDonnee == null )
					contexteDonnee = (CContexteDonnee)ctx.GetObjetAttache(typeof(CContexteDonnee));
				if (contexteDonnee == null)
				{
					contexteDonnee = new CContexteDonnee(CSessionClient.GetSessionUnique().IdSession, true, false);
					ctx.AttacheObjet(typeof(CContexteDonnee), contexteDonnee);
				}
                CFiltreData filtre = null;
                if (filtreAvance.HasFiltre)
                    filtre = filtreAvance;
				CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteDonnee, tp, filtre);
				result.Data = liste;
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}

			return result;
		}


	}
}
