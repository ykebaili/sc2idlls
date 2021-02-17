using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.expression;


namespace sc2i.expression
{
	/// <summary>
	/// Retourne l'entité du type demandé ayant l'identifiant demandé
	/// Cette formule a besoin d'un IAllocateurSupprimeurElements
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
	[ReplaceClass("sc2i.data.dynamic.C2iExpressionCreateEntity")]
	public class C2iExpressionCreateEntity : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionCreateEntity()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionCreateEntity().IdExpression,
				typeof(C2iExpressionCreateEntity) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "CreateEntity", 
				new CTypeResultatExpression(typeof(object), true),
				I.TT(GetType(), "CreateEntity(Type)\n Returns a new the entity of the specified type. Returns null if the entity can not be created|20035"),
				CInfo2iExpression.c_categorieDivers);
			info.AddDefinitionParametres( new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string) ));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Type|20038"), typeof(Type)));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 1;
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
				return new CTypeResultatExpression ( typeof(object), false );
			}
		}

		/// //////////////////////////////////////////
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] listeParametres)
        {
            CResultAErreur result = CResultAErreur.True;
            string strVal = listeParametres[0].ToString();
            Type tp = listeParametres[0] as Type;
            if (tp == null)
                tp = CActivatorSurChaine.GetType(strVal, true);
            if (tp == null)
                tp = CActivatorSurChaine.GetType(strVal, false);

            if (tp == null)
            {
                result.EmpileErreur(I.T("The @1 type does not exist|221", strVal));
                return result;
            }

            try
            {

                IAllocateurSupprimeurElements allocateur;
                object source = ctx.ObjetSource;
                allocateur = source as IAllocateurSupprimeurElements;
                if (allocateur == null)
                    allocateur = ctx.GetObjetAttache(typeof(IAllocateurSupprimeurElements)) as IAllocateurSupprimeurElements;
                result.Data = null;
                if (allocateur != null)
                    result.Data = allocateur.AlloueElement(tp).Data;
                if (result.Data == null)
                {
                    try
                    {
                        result.Data = Activator.CreateInstance(tp, new object[0]);
                    }
                    catch { }
                }

                return result;
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }


	}
}
