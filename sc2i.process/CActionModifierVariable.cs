using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierVariable.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionModifierVariable : CActionLienSortantSimple
	{
        /* TESTDBKEYOK (XL)*/

        private string m_strIdVariableAModifier = "";
		private C2iExpression m_expressionValeur = null;

		/// /////////////////////////////////////////
		public CActionModifierVariable( CProcess process )
			:base(process)
		{
			Libelle = I.T("Assign variable|224");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Assign variable|224"),
				I.T("Aassigns a value to a variable|225"),
				typeof(CActionModifierVariable),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            table[m_strIdVariableAModifier] = true;
            AddIdVariablesExpressionToHashtable(m_expressionValeur, table);
        }

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            return 1; // Passage de int IdVariableAModifier en String
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			if ( !result )
				return result;
            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableAModifier = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableAModifier);

			I2iSerializable objet = m_expressionValeur;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_expressionValeur = (C2iExpression)objet;

			return result;
		}

		/// ////////////////////////////////////////////////////////
		public string IdVariableAModifier
		{
			get
			{
				return m_strIdVariableAModifier;
			}
			set
			{
				m_strIdVariableAModifier = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CVariableDynamique VariableAModifier
		{
			get
			{
                return Process.GetVariable(IdVariableAModifier);
			}
			set
			{
                if (value == null)
                    m_strIdVariableAModifier = "";
                else
                    m_strIdVariableAModifier = value.IdVariable;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionValeur
		{
			get
			{
				if ( m_expressionValeur == null )
					return new C2iExpressionConstante("");
				return m_expressionValeur;
			}
			set
			{
				m_expressionValeur = value;
			}
		}
		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			CVariableDynamique variable = VariableAModifier;
			if ( IdVariableAModifier != "" )
			{
				string strTypeVariable = "";
				if ( variable == null )
				{
					result.EmpileErreur(I.T("The variable to be modified isn't defined or doesn't exist|215"));
				}
				else
					strTypeVariable = DynamicClassAttribute.GetNomConvivial ( variable.TypeDonnee.TypeDotNetNatif );


				if ( variable is CVariableProcessTypeComplexe && 
					typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif) )
				{
					//C'est une objetAIdAuto
					CTypeResultatExpression type = ExpressionValeur.TypeDonnee;
					if ( !type.TypeDotNetNatif.Equals(typeof(int) ) && !type.TypeDotNetNatif.Equals ( variable.TypeDonnee.TypeDotNetNatif ))
					{
						result.EmpileErreur(I.T("The value must be a @1 or an integer because it's the @2 id|226",strTypeVariable, strTypeVariable));
					}
					else
					{
						if ( type.IsArrayOfTypeNatif && ! variable.TypeDonnee.IsArrayOfTypeNatif )
						{
							result.EmpileErreur(I.T("The value must be a @1 or an integer because it's the @2 id|226",strTypeVariable, strTypeVariable));
							
						}
					}
				}
				else if ( !ExpressionValeur.TypeDonnee.Equals(VariableAModifier.TypeDonnee) )
				{
					result.EmpileErreur(I.T("The formula of value isn't of the awaited type by the variable (@1)|227",strTypeVariable));
				}
			}
					
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			//Calcule la nouvelle valeur
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = ExpressionValeur.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T( "Error during @1 formula evaluation|216", ExpressionValeur.ToString()));
				return result;
			}
			object nouvelleValeur = result.Data;

			

			CVariableDynamique variable = VariableAModifier;
			if ( nouvelleValeur is CResultAErreur && variable == null )
				return ( CResultAErreur )nouvelleValeur;

			if ( variable != null )
			{
				/*result.EmpileErreur("Variable "+m_nIdVariableAModifier.ToString()+" inconnue");
				return result;
			}*/
				if ( variable is CVariableProcessTypeComplexe && 
					typeof(IObjetDonneeAIdNumerique).IsAssignableFrom ( variable.TypeDonnee.TypeDotNetNatif ) )
				{
					ArrayList lstElements = new ArrayList();
					try
					{
						ArrayList lst = new ArrayList();
						if ( typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(result.Data.GetType()) ||
							result.Data is int)
							lst.Add ( result.Data );
						else if ( result.Data is IList )
						{
							foreach ( object item in (IList)result.Data )
								lst.Add ( item );
						}
						else
						{
							result.EmpileErreur(I.T("Incompatible value type|228"));
							return result;
						}
					
						Type tp = variable.TypeDonnee.TypeDotNetNatif;
						foreach ( object obj in lst )
						{
							
							if(  obj is  int )
							{
								IObjetDonneeAIdNumerique objet = (IObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[]{contexte.ContexteDonnee});
								if ( objet.ReadIfExists ( (int)obj ) )
									lstElements.Add ( objet );
							}
							else if ( typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(obj.GetType()) )
							{
								lstElements.Add ( obj );
							}
						}
					}
					catch
					{
					}
					if ( variable.TypeDonnee.IsArrayOfTypeNatif )
						contexte.Branche.Process.SetValeurChamp ( variable, lstElements );
					else
					{
						if ( lstElements.Count > 0 )
							contexte.Branche.Process.SetValeurChamp ( variable, lstElements[0] );
						else
							contexte.Branche.Process.SetValeurChamp ( variable, null );
					}
				}
				else
					contexte.Branche.Process.SetValeurChamp ( variable, result.Data );
			}

			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
			Graphics g = ctx.Graphic;
			DrawVariableEntree ( g, VariableAModifier );
		}



	}
}
