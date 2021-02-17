using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionObjet.
	/// </summary>
	[Serializable]
	public class C2iExpressionObjet : C2iExpressionMethodeOuPropriete
	{
		[Serializable]
		private class CArrayDonneeObjet : ArrayList
		{
			public object[] DonneesObjetsDuFond
			{
				get
				{
					ArrayList lst = new ArrayList();
					FillFlatArray ( this, lst );
					return (object[])lst.ToArray(typeof(object));
				}
			}

			private void FillFlatArray ( IList lst, ArrayList lstDest )
			{
				if ( lst is CArrayDonneeObjet )
				{
					if ( lst.Count > 0 && lst[0] is IList )
					{
						foreach ( IList listeFille in lst )
							FillFlatArray ( listeFille, lstDest );
						return;
					}

				}
				foreach ( object obj in lst )
					lstDest.Add ( obj );
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionObjet()
			:base()
		{
		}

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionObjet ( IExpression expressionSource, IExpression methodeOuPropriete )
			:base()
		{
			Parametres.Add ( expressionSource );
			Parametres.Add ( methodeOuPropriete );
		}

        /// //////////////////////////////////// /////////////////////////////////
        public override CObjetPourSousProprietes GetObjetPourSousProprietes()
        {
            if (Parametres2i.Length > 1 && TypeDonnee != null)
            {
                CObjetPourSousProprietes obj = Parametres2i[1].GetObjetPourSousProprietes();
                //Correction stef le 24/11/2011: si l'objet à gauche est un array,
                //le résultat est un array
                if (TypeDonnee.IsArrayOfTypeNatif)
                {
                    if (obj.ElementAVariableInstance != null)
                        obj.IsArrayOfObject = true;
                    if (obj.TypeResultatExpression != null && !obj.TypeResultatExpression.IsArrayOfTypeNatif)
                        obj = new CObjetPourSousProprietes(obj.TypeResultatExpression.GetTypeArray());
                }
                return obj;
            }
            return base.GetObjetPourSousProprietes();
        }



		/*/// //////////////////////////////////// /////////////////////////////////
		public CDefinitionProprieteDynamique DefinitionPropriete
		{
			get
			{
				if ( Parametres2i[1] is C2iExpressionMethodeOuPropriete )
					return ((C2iExpressionMethodeOuPropriete)Parametres[1]).DefinitionPropriete;
				return null;
			}
			set
			{
				if ( Parametres2i[1] is C2iExpressionMethodeOuPropriete )
					((C2iExpressionMethodeOuPropriete)Parametres[1]).DefinitionPropriete = value;
			}
		}*/

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Parametres[0] != null )
				result = Parametres2i[0].SetTypeObjetInterroge( objetPourSousProprietes, fournisseur );
			else
				result.EmpileErreur(I.T("Source expression invalid|127"));
			if ( result )
			{
				if ( Parametres2i[1] !=  null )
				{
					result = Parametres2i[1].SetTypeObjetInterroge ( Parametres2i[0].GetObjetPourSousProprietes(), fournisseur );
				}
				else
					result.EmpileErreur(I.T("Method or property invalid|128"));
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string GetString()
		{
			return CaracteresControleAvant+((C2iExpression)Parametres[0]).GetString()+"."+
				((C2iExpression)Parametres[1]).GetString();
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "OBJECT";
			}
		}

		

			/// //////////////////////////////////// /////////////////////////////////
			public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count >= 2 && Parametres[1] != null && Parametres[0] != null )
				{
					CTypeResultatExpression typeSource = Parametres2i[0].TypeDonnee;
					CTypeResultatExpression typeResult = Parametres2i[1].TypeDonnee;
					try
					{
						if ( typeSource.IsArrayOfTypeNatif && !typeResult.IsArrayOfTypeNatif &&
							(!(Parametres2i[1] is C2iExpressionMethodeAnalysable) ||
							!((C2iExpressionMethodeAnalysable)Parametres2i[1]).AgitSurListe) )
							typeResult = typeResult.GetTypeArray();
						return typeResult;
					}
					catch
					{
						return null;
					}
				}
				return new CTypeResultatExpression(typeof(string), false);
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// //////////////////////////////////// /////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = VerifieParametres();
			if ( !result )
				return result;
			result = Parametres2i[0].Eval(ctx);
			if ( !result )
			{
				result.EmpileErreur(I.T("Error during the @1 object expression evaluation|129",GetString()));
				return result;
			}
			object  obj = result.Data;
			if ( obj == null )
			{
				result.EmpileErreur(I.T("The object source is null|130"));
				return result;
			}
			bool bAgitSurListe = Parametres2i[1] is C2iExpressionMethodeAnalysable && 
				((C2iExpressionMethodeAnalysable)Parametres2i[1]).AgitSurListe;
            if ( typeof(IObjetAMethodesDynamiquesSurListe).IsAssignableFrom(obj.GetType() ))
                bAgitSurListe = true;
			//if ( Parametres2i[0].TypeDonnee.IsArrayOfTypeNatif && !bAgitSurListe)//C'est un objet multiple
			if ( obj is CArrayDonneeObjet || 
                (!bAgitSurListe && (Parametres2i[0].TypeDonnee.IsArrayOfTypeNatif || typeof(IEnumerable).IsAssignableFrom(Parametres2i[0].TypeDonnee.TypeDotNetNatif))))
			{
				CArrayDonneeObjet lst = new CArrayDonneeObjet();
				IList source = (IList)obj;
				if ( source is CArrayDonneeObjet )
					source = ((CArrayDonneeObjet)source).DonneesObjetsDuFond;
				if ( bAgitSurListe )
					result = GetValeurObjet ( ctx, source, Parametres2i[1] );
				else
				{
					foreach ( object objFils in source )
					{
						result = GetValeurObjet ( ctx, objFils, Parametres2i[1] );
						if ( result )
							lst.Add ( result.Data );
						else
							return result;
					}
					result.Data = lst.DonneesObjetsDuFond;
				}
			}
			else
				result = GetValeurObjet ( ctx, obj, Parametres2i[1] );
			if ( !result )
				result.EmpileErreur(I.T("Error during the @1 object expression evaluation|129",GetString()));

			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		private CResultAErreur GetValeurObjet ( CContexteEvaluationExpression ctx, object source, C2iExpression expressionAEvaluer )
		{
			CResultAErreur result = CResultAErreur.True;
			ctx.PushObjetSource ( source, false );
			try
			{
				result = expressionAEvaluer.Eval ( ctx );
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			finally 
			{
				ctx.PopObjetSource(false);
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

	}
}
