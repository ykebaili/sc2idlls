using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionConstante.
	/// </summary>
	[Serializable]
	public class C2iExpressionConstante : C2iExpression
	{
		private object m_valeur = null;

		private static Type[] m_typesGeres = 
			{
				typeof(string),
				typeof(int),
				typeof(bool),
				typeof(DateTime),
				typeof(double)
			};

		private enum ConstTypes
		{
			tNull = 0,
			tString = 1,
			tInt = 2,
			tBool = 3,
			tDateTime = 4,
			tDouble = 5
		};

		/// ///////////////////////////////////////////////////////
		public C2iExpressionConstante ( )
		{
			m_valeur = null;
		}

		/// ///////////////////////////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "CST";
			}
		}

		/// ///////////////////////////////////////////////////////
		public C2iExpressionConstante( object valeur )
		{
			m_valeur = valeur;
			AssureValeurGeree();
		}

		/// ///////////////////////////////////////////////////////
		private void AssureValeurGeree()
		{
			if ( m_valeur == null )
				return;
			foreach ( Type tp in m_typesGeres )
				if ( m_valeur.GetType() == tp )
					return ;
			throw new Exception(I.T("Constant type not managed : @1|110",m_valeur.GetType().ToString()));
		}

        /// ///////////////////////////////////////////////////////
        public static bool CanManage(object valeur)
        {
            foreach (Type tp in m_typesGeres)
                if (valeur.GetType() == tp)
                    return true;
            return false;
        }

		/// ///////////////////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_valeur == null )
				result.Data = "";
			else
				result.Data = m_valeur;
			return result;
		}

		/// ///////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////
		public override string GetString()
		{
			if ( m_valeur == null )
				return "";
			string strValeur = "";
			if ( m_valeur.GetType() == typeof(string))
			{
				strValeur = "\""+m_valeur.ToString().Replace("\"","\\\"")+"\"";
			}
			else if ( m_valeur.GetType() == typeof(int))
			{
				strValeur = m_valeur.ToString();
			}
			else if ( m_valeur.GetType() == typeof(bool))
			{
				strValeur = ((bool)m_valeur)?"1":"0";
			}
			else if ( m_valeur.GetType() == typeof(DateTime))
			{
				DateTime dt = (DateTime)m_valeur;
				strValeur = "#"+dt.Day.ToString().PadLeft(2,'0')+
					"/"+dt.Month.ToString().PadLeft(2,'0')+
					"/"+dt.Year.ToString().PadLeft(4,'0')+"#";
			}
			else if ( m_valeur.GetType() == typeof(double))
			{
				strValeur = m_valeur.ToString();
				if ( strValeur.IndexOf(".") <0 && 
					strValeur.IndexOf(",") < 0 )
					strValeur = ((double)m_valeur).ToString("0.0").Replace(",",".");
			}
			return CaracteresControleAvant+strValeur;
		}

		/// ///////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( m_valeur == null )
					return new CTypeResultatExpression(typeof(string), false);
				return new CTypeResultatExpression ( m_valeur.GetType(), false );
			}
		}

		/// ///////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////
		public object Valeur
		{
			get
			{
				return m_valeur;
			}
		}

		/// ///////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//V1 : Caracteres avant
		}

		/// ///////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if(  !result )
				return result;
			int nType = (int)ConstTypes.tNull;
			
			if ( serializer.Mode == ModeSerialisation.Ecriture )
			{
				if ( m_valeur == null )
					nType = (int)ConstTypes.tNull;
				else if ( m_valeur.GetType() == typeof(string))
					nType = (int)ConstTypes.tString;
				else if ( m_valeur.GetType() == typeof(int))
					nType = (int)ConstTypes.tInt;
				else if ( m_valeur.GetType() == typeof(bool))
					nType = (int)ConstTypes.tBool;
				else if ( m_valeur.GetType() == typeof(DateTime))
					nType = (int)ConstTypes.tDateTime;
				else if ( m_valeur.GetType() == typeof(double))
					nType = (int)ConstTypes.tDouble;
				else
					throw new Exception(I.T("Constant type not managed : @1|110", m_valeur.GetType().ToString()));
			}
			serializer.TraiteInt ( ref nType );
			
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
				switch ( (ConstTypes)nType )
				{
					case ConstTypes.tBool :
						bool b = (bool)m_valeur;
						serializer.TraiteBool ( ref b );
						break;
					case ConstTypes.tDateTime :
						DateTime dt = (DateTime)m_valeur;
						serializer.TraiteDate ( ref dt );
						break;
					case ConstTypes.tDouble :
						Double f = (double)m_valeur;
						serializer.TraiteDouble ( ref f );
						break;
					case ConstTypes.tInt :
						int nVal = (int)m_valeur;
						serializer.TraiteInt ( ref nVal );
						break;
					case ConstTypes.tNull :
						break;
					case ConstTypes.tString :
						string strVal = (string)m_valeur;
						serializer.TraiteString ( ref strVal );
						break;
				}
					break;
				case ModeSerialisation.Lecture :
				switch ( (ConstTypes)nType )
				{
					case ConstTypes.tBool :
						bool b = false;
						serializer.TraiteBool ( ref b );
						m_valeur = b;
						break;
					case ConstTypes.tDateTime :
						DateTime dt = DateTime.Now;
						serializer.TraiteDate ( ref dt );
						m_valeur = dt;
						break;
					case ConstTypes.tDouble :
						double f = 0;
						serializer.TraiteDouble ( ref f );
						m_valeur = f;
						break;
					case ConstTypes.tInt :
						int nVal = 0;
						serializer.TraiteInt ( ref nVal );
						m_valeur = nVal;
						break;
					case ConstTypes.tNull :
						m_valeur = null;
						break;
					case ConstTypes.tString :
						string strVal = "";
						serializer.TraiteString ( ref strVal );
						m_valeur = strVal;
						break;
				}
					break;
			}
			if ( nVersion >= 1 )
			{
				string strAvant = CaracteresControleAvant;
				serializer.TraiteString ( ref strAvant );
				CaracteresControleAvant = strAvant;
			}
			return result;
		}
					

		
	}

	/// ///////////////////////////////////////////////////////
	
}
