using System;
using System.Collections;
using System.Reflection;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionIndexeur.
	/// </summary>
	[Serializable]
	public class C2iExpressionIndexeur : C2iExpressionMethodeOuPropriete
	{
		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionIndexeur()
		{
		}

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionIndexeur( IExpression expressionChamp, IExpression expressionIndexeur )
		{
			Parametres.Add ( expressionChamp );
			Parametres.Add ( expressionIndexeur );
		}

		/// ///////////////////////////////////////////////////////////
		public override bool CanBeArgumentExpressionObjet
		{
			get
			{
				return true;
			}
		}


		/*/// //////////////////////////////////// /////////////////////////////////
		public override CDefinitionProprieteDynamique DefinitionPropriete
		{
			get
			{
				if ( Parametres.Count > 0 && Parametres2i[0] is C2iExpressionMethodeOuPropriete )
					return ((C2iExpressionMethodeOuPropriete)Parametres[0]).DefinitionPropriete;
				return null;
			}
			set
			{
				if ( Parametres2i[0] is C2iExpressionMethodeOuPropriete )
					((C2iExpressionMethodeOuPropriete)Parametres[0]).DefinitionPropriete = value;
			}
		}*/


		/// //////////////////////////////////// /////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string GetString()
		{
			string strRetour = "";
			if ( Parametres.Count < 2 || Parametres[0] == null ||
				Parametres[1] == null )
				return I.T("#INDEXER ERROR#|113");
			strRetour = Parametres2i[0].GetString();
			strRetour += "[";
			strRetour += Parametres2i[1].GetString();
			strRetour += "]";
			return CaracteresControleAvant+strRetour;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "INDEXEUR";
			}
		}

        /// //////////////////////////////////// /////////////////////////////////
        public override CObjetPourSousProprietes GetObjetPourSousProprietes()
        {
            if (Parametres.Count > 0 && Parametres[0] != null)
            {
                CObjetPourSousProprietes obj = Parametres2i[0].GetObjetPourSousProprietes();
                if ( obj != null )
                    return obj.GetObjetAnalyseElements();
            }
            return new CObjetPourSousProprietes(TypeDonnee);
        }

		/// //////////////////////////////////// /////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres[0] != null  )
				{
					CTypeResultatExpression type = Parametres2i[0].TypeDonnee;
					if ( type != null && type.IsArrayOfTypeNatif && !Parametres2i[1].TypeDonnee.IsArrayOfTypeNatif)
						return type.GetTypeElements();
					return type;
				}
				return new CTypeResultatExpression(typeof(string), false);
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Parametres.Count != 2 || Parametres[0]== null  ||
				Parametres[1] == null )
			{
				result.EmpileErreur(I.T("The indexer must have two parameters and they should not be null|117"));
				return result;
			}
			result = ((C2iExpression)Parametres[1]).VerifieParametres();
		/*	if ( !(Parametres[0] is C2iExpressionMethodeOuPropriete) )
				result.EmpileErreur("Le premier paramètre n'est pas une méthode ou une propriété");
			else
			{*/
				CTypeResultatExpression tp = Parametres2i[0].TypeDonnee;
				if ( tp != null && !tp.IsArrayOfTypeNatif  )
					result.EmpileErreur(I.T("@1 isn't a table|116",Parametres2i[0].GetString()));
			//}
			tp = Parametres2i[1].TypeDonnee;
			if ( tp.TypeDotNetNatif == null || (!tp.CanConvertTo(typeof(int)) && !tp.CanConvertTo(new CTypeResultatExpression(typeof(int), true))))
			{
				result.EmpileErreur(I.T("The index of the indexer must be an integer or a integer list|115"));
			}
			if ( !result )
				result.EmpileErreur(I.T("Error in indexer parameters|114"));
			
			return CResultAErreur.True;
		}

		/// //////////////////////////////////// /////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			ArrayList lstParametres = new ArrayList();
			CResultAErreur result = EvalParametres(ctx, lstParametres );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in indexer parameters|114"));
			}
			try
			{
				if ( Parametres2i[1].TypeDonnee.IsArrayOfTypeNatif )
				{
					ArrayList lst = new ArrayList();
					foreach ( object index in (IEnumerable)lstParametres[1] )
					{
						result = GetElement ( lstParametres[0], index );
						if ( !result )
							return result;
						lst.Add ( result.Data );
					}
					result.Data = lst;
				}
				else
					result = GetElement ( lstParametres[0], lstParametres[1] );
		
			}
			catch
			{
				result.Data = null;
			}

			return result;
		}

		/// <summary>
		/// /////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="source"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private CResultAErreur GetElement ( object source, object index )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( source.GetType().IsArray )
			{
				object obj = ((Array)source).GetValue((int)index);
				result.Data = obj;
				return result;
			}
			else
			{
				PropertyInfo info = source.GetType().GetProperty("Item");
				if ( info!=null )
				{
					object obj = info.GetGetMethod().Invoke(source, new object[]{index});
					result.Data = obj;
					return result;
				}
			}
			result.EmpileErreur(I.T("'@1' indexer not found|118",source.ToString()));
			return result;
		}


	}
}
