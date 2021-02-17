using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionComparaison : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionComparaison()
		{
		}

		public abstract string SymboleComparaison{get;}

		public abstract bool DoCompare ( IComparable obj1, IComparable obj2 );
		
		public abstract string Description{get;}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 3, SymboleComparaison, typeof(Boolean), Description, CInfo2iExpression.c_categorieComparaison );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(IComparable)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(IComparable)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion en ICOomparable
			try
			{
				if ( valeursParametres[0] is IComparable && valeursParametres[1] is IComparable )
				{
					IComparable obj1, obj2;
					obj1 = (IComparable)valeursParametres[0];
					obj2 = (IComparable)valeursParametres[1];
					result.Data = DoCompare ( obj1, obj2 );
					return result;
				}
				else
				{
					if ( valeursParametres[0] == null && valeursParametres[1] == null )
					{
						result.Data = true;
						return result;
					}
					if ( valeursParametres[0] == null || valeursParametres[1] == null )
					{
						result.Data = false;
						return result;
					}
					result.Data = valeursParametres[0].Equals ( valeursParametres[1] );
					return result;
				}
			}
			catch
			{
			}
			result.EmpileErreur(I.T("No overload of the @1 function accepts the parameters indicated|146",SymboleComparaison));
			return result;
		}

		
	}

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionEgal : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return "=";
			}
		}

		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
			try
			{
                if ((obj1 == null) != (obj2 == null))
                    return false;
                if (obj1.GetType() != obj2.GetType())
                {
                    if (obj1 is double || obj2 is double)
                        return Convert.ToDouble(obj1).CompareTo(Convert.ToDouble(obj2))==0;
                }
				return obj1.CompareTo(obj2) == 0 ;
			}
			catch (Exception e )
			{
				try
				{
					if ( obj1 is double || obj2 is double )
					{
						return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))==0;
					}
				}
				catch
				{
					if ( obj1 is bool || obj2 is bool )
						return Convert.ToBoolean ( obj1 ).CompareTo(Convert.ToBoolean(obj2)) == 0;
				}
				throw(e);
			}
		}

		public override string Description
		{
			get
			{
				return I.TT(GetType(), "Operator of comparison equality|147");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionEgalText : C2iExpressionEgal
    {
        public override string SymboleComparaison
        {
            get
            {
                return "Equals";
            }
        }
    }

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionInferieurOuEgal : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return "<=";
			}
		}
		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
			try
			{
				return obj1.CompareTo(obj2) <= 0 ;
			}
			catch (Exception e )
			{
				if ( obj1 is double || obj2 is double )
				{
					return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))<=0;
				}
				throw(e);
			}
		}

		public override string Description
		{
			get
			{
				return I.T("Operator of comparison lower or equal|148");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionInferieurOuEgalText : C2iExpressionInferieurOuEgal
    {
        public override string SymboleComparaison
        {
            get
            {
                return "LessOrEquals";
            }
        }
    }

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionSuperieurOuEgal : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return ">=";
			}
		}
		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
			try
			{
				return obj1.CompareTo(obj2) >= 0 ;
			}
			catch (Exception e )
			{
				if ( obj1 is double || obj2 is double )
				{
					return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))>=0;
				}
				throw(e);
			}
		}

		public override string Description
		{
			get
			{
				return I.T("Operator of comparison higher or equal|149");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionSuperieurOuEgalText : C2iExpressionSuperieurOuEgal
    {
        public override string SymboleComparaison
        {
            get
            {
                return "MoreOrEquals";
            }
        }
    }

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionInferieur : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return "<";
			}
		}
		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
			try
			{
				return obj1.CompareTo(obj2) < 0 ;
			}
			catch (Exception e )
			{
				if ( obj1 is double || obj2 is double )
				{
					return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))<0;
				}
				throw(e);
			}
		}


		public override string Description
		{
			get
			{
				return I.T("Operator of comparison lower|150");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionInferieurText : C2iExpressionInferieur
    {
        public override string SymboleComparaison
        {
            get
            {
                return "LessThan";
            }
        }
    }

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionSuperieur : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return ">";
			}
		}
		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
			try
			{
				return obj1.CompareTo(obj2) > 0 ;
			}
			catch (Exception e )
			{
				if ( obj1 is double || obj2 is double )
				{
					return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))>0;
				}
				throw(e);
			}
		}

		public override string Description
		{
			get
			{
				return I.T("Operator of comparison higher|151");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionSuperieurText : C2iExpressionSuperieur
    {
        public override string SymboleComparaison
        {
            get
            {
                return "MoreThan";
            }
        }
    }

	/////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class C2iExpressionDifferent : C2iExpressionComparaison
	{
		public override string SymboleComparaison
		{
			get
			{
				return "<>";
			}
		}

		public override bool DoCompare ( IComparable obj1, IComparable obj2 )
		{
            if ((obj1 == null) != (obj2 == null))
                return false;
            if (obj1 == null || obj2 == null)
                return false;
			try
			{
				return obj1.CompareTo(obj2) != 0 ;
			}
			catch (Exception e )
			{
				if ( obj1 is double || obj2 is double )
				{
					return Convert.ToDouble ( obj1 ).CompareTo(Convert.ToDouble(obj2))!=0;
				}
				throw(e);
			}
		}

		public override string Description
		{
			get
			{
				return I.T("Operator of comparison difference|152");
			}
		}
	}

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class C2iExpressionDifferentText : C2iExpressionDifferent
    {
        public override string SymboleComparaison
        {
            get
            {
                return "NotEquals";
            }
        }
    }
}
