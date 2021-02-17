using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CVariableDynamiqueSaisie.
	/// </summary>
    public class CValeurVariableDynamiqueSaisie : MarshalByRefObject, IValeurVariable, I2iSerializable, IComparable
	{
		private object m_value;
		private string m_strDisplay;

		/// /////////////////////////////////////////////////////////////
		public CValeurVariableDynamiqueSaisie ( )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CValeurVariableDynamiqueSaisie ( object valeur, string strDisplay )
		{
			m_value = valeur;
			m_strDisplay = strDisplay;
		}

		/// /////////////////////////////////////////////////////////////
		public object Value
		{
			get
			{
				return m_value;
			}
			set
			{
				m_value = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public string Display
		{
			get
			{
				return m_strDisplay;
			}
			set
			{
				m_strDisplay = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = serializer.TraiteObjetSimple ( ref m_value );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strDisplay );
			return result;
		}

		/// ///////////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is CValeurVariableDynamiqueSaisie) )
				return -1;
			return Display.CompareTo(((CValeurVariableDynamiqueSaisie)obj).Display);
		}

	}
		
    [AutoExec("Autoexec")]
	public class CVariableDynamiqueSaisie : CVariableDynamique, IVariableDynamiqueAValeurParDefaut
	{
		private C2iExpression m_expressionValeurParDefaut = null;
		private C2iTypeDonnee m_typeDonnee = new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tString);
		private C2iExpression m_expressionValidation = new C2iExpressionVrai();
		private string m_strDescriptionFormat = "";
		private ArrayList m_listeValeurs = new ArrayList();

		/// ////////////////////////////////////////////////////////
		public CVariableDynamiqueSaisie()
		{
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueSaisie( IElementAVariablesDynamiquesBase elementAVariables )
			:base ( elementAVariables )
		{
		}

        /// ///////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireVariablesDynamiques.RegisterTypeVariable(typeof(CVariableDynamiqueSaisie), I.T("Entered variable|20077"));
            CGestionnaireVariablesDynamiques.MasqueTypeVariable(typeof(CVariableDynamiqueSaisieSimple));
        }

		/// ///////////////////////////////////////////
		public  object GetValeurParDefaut()
		{
			if ( m_expressionValeurParDefaut != null )
			{
				CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(null );
				CResultAErreur result = m_expressionValeurParDefaut.Eval ( ctx );
				if ( result )
					return result.Data;
			}
			return null;
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Entry|74");
			}
		}

		/// ////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression(m_typeDonnee.TypeDotNetAssocie, false);
			}
		}

		/// ///////////////////////////////////////////
		public override bool IsChoixParmis()
		{
			return m_listeValeurs.Count > 0;

		}

		/// ///////////////////////////////////////////
		public override bool IsChoixUtilisateur()
		{
			return true;
		}

		/// ////////////////////////////////////////////////////////
		public C2iTypeDonnee TypeDonnee2i
		{
			get
			{
				return m_typeDonnee;
			}
			set
			{
				m_typeDonnee = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public C2iExpression ExpressionValeurParDefaut
		{
			get
			{
				return m_expressionValeurParDefaut;
			}
			set
			{
				m_expressionValeurParDefaut = value;
			}
		}

		/// ///////////////////////////////////////////
		public C2iExpression ExpressionValidation
		{
			get
			{
				if ( m_expressionValidation == null )
					m_expressionValidation = new C2iExpressionVrai();
				return m_expressionValidation;
			}
			set
			{
				m_expressionValidation = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string DescriptionFormat
		{
			get
			{
				return m_strDescriptionFormat;
			}
			set
			{
				m_strDescriptionFormat = value;
			}
		}

		/// ///////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
		}
		
		/// ///////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			int nType = (int)m_typeDonnee.TypeDonnee;
			serializer.TraiteInt ( ref nType );
			m_typeDonnee = new C2iTypeDonnee ( (sc2i.data.dynamic.TypeDonnee)nType);

			I2iSerializable obj = m_expressionValidation;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionValidation = (C2iExpression)obj;

			serializer.TraiteString ( ref m_strDescriptionFormat );

			if ( nVersion >= 1 )
				result = serializer.TraiteArrayListOf2iSerializable(m_listeValeurs);
			else
				m_listeValeurs.Clear();
			if ( !result )
				return result;

			//Version 2 : Ajout de la valeur par défaut
			if(  nVersion >= 2 )
			{
				obj = m_expressionValeurParDefaut;
				result = serializer.TraiteObject ( ref obj );
				m_expressionValeurParDefaut = (C2iExpression)obj;
			}
			else
				m_expressionValeurParDefaut = null;

			return result;
		}

		/// ///////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				return m_listeValeurs;
			}
		}
		
		/// ///////////////////////////////////////////
		public override CResultAErreur VerifieValeur(object valeur)
		{
			CResultAErreur result = base.VerifieValeur(valeur);
			if ( !result )
				return result;
			if ( ExpressionValidation == null )
				return result;

			
			try
			{
				object obj = CObjetForTestValeurChampCustom .GetNewForTypeDonnee (
					TypeDonnee2i.TypeDonnee, null, valeur );
				CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(obj);
				result = ExpressionValidation.Eval( contexte );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error during validation of @1|240",Nom));
					return result;
				}
				if ( (result.Data is bool && (bool)result.Data) || result.Data.ToString() =="1" )
					return CResultAErreur.True;
				else
					result.EmpileErreur(this.DescriptionFormat);
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error during validation of @1|240", Nom));
				return result;
			}
			return result;
		}
	}
}
