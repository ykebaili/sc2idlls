using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.multitiers.client;
using sc2i.data.dynamic.NommageEntite;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Retourne l'entité du type demandé ayant l'identifiant demandé
	/// </summary>
	/// <remarks>
	/// <LI>
	/// Le premier paramètre représente le type de l'objet
	/// </LI>
	/// <LI>
	/// Le second paramètre représente l'identifiant de l'objet
	/// </LI>
	/// </remarks>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionGetEntite : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionGetEntite()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionGetEntite().IdExpression,
				typeof(C2iExpressionGetEntite) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "GetEntity", 
				new CTypeResultatExpression(typeof(object), true),
				I.TT(GetType(), "GetEntity(Type, Id [Name])\n Returns the entity of the specified type having the specified Id or Name. Returns null if the entity does not exist|220"),
				CInfo2iExpression.c_categorieDivers);
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(string), typeof(int)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(Type), typeof(int)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(string), typeof(string)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(Type), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
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
                        return new CTypeResultatExpression(((C2iExpressionTypesDynamics)expType).TypeReprésenté, false);
                    }
					if ( expType is C2iExpressionConstante )
					{
						string strType = ((C2iExpressionConstante)expType).Valeur.ToString();
						Type tp = CActivatorSurChaine.GetType(strType, true);
						if ( tp != null )
							return  new CTypeResultatExpression ( tp, false );
					}
				}
				return new CTypeResultatExpression ( typeof(CObjetDonneeAIdNumerique), false );
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string strVal = listeParametres[0].ToString();

            Type tp = listeParametres[0] as Type;
            if ( tp == null )
                tp = CActivatorSurChaine.GetType ( strVal, true );
			if ( tp == null )
				tp = CActivatorSurChaine.GetType ( strVal, false );
			if ( tp == null )
			{
				result.EmpileErreur(I.T("The @1 type does not exist|221",strVal));
				return result;
			}
			if ( !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( tp ) )
			{
				result.EmpileErreur(I.T("The @1 type cannot be loaded by the 'GetEntite' function|222", strVal));
				return result;
			}
			try
			{
				CContexteDonnee contexteDonnee = (CContexteDonnee)ctx.GetObjetAttache ( typeof (CContexteDonnee) );
				if ( contexteDonnee == null )
				{
					contexteDonnee = new CContexteDonnee ( CSessionClient.GetSessionUnique().IdSession, true, false );
					ctx.AttacheObjet ( typeof ( CContexteDonnee ), contexteDonnee );
				}
				CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( tp, new object[]{contexteDonnee});
                if (listeParametres[1] is int)
                {
                    if (obj.ReadIfExists((int)listeParametres[1]))
                        result.Data = obj;
                    else
                        result.Data = null;
                }
                // YK 14/04/2011 Surcharge du deuxième paramètre en String Nom de l'entité nommée
                else if (listeParametres[1] is string)
                {
                    CNommageEntite nommage = new CNommageEntite(contexteDonnee);
                    if (nommage.ReadIfExists(new CFiltreData(
                        CNommageEntite.c_champTypeEntite + " = @1 AND " +
                        CNommageEntite.c_champNomFort + " = @2",
                        tp.ToString(),
                        (string)listeParametres[1])))
                        result.Data = nommage.GetObjetNomme();
                    else if (typeof(IObjetDonnee).IsAssignableFrom(tp))
                    {
                        IObjetDonnee objUniv = (IObjetDonnee)Activator.CreateInstance(tp, new object[] { contexteDonnee });
                        if (objUniv.ReadIfExistsUniversalId((string)listeParametres[1]))
                            result.Data = objUniv;
                    }
                    else
                        result.Data = null;
                }
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}


	}
}
