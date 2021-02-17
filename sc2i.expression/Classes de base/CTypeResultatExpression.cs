using System;

using sc2i.common;
using System.Collections.Generic;
using System.Collections;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CTypeExpression.
	/// </summary>
	[Serializable]
	public class CTypeResultatExpression : I2iSerializable
	{
		private Type m_typeDotNetNatif = null;
		private CTypeResultatExpression m_typeExpressionNatif;
		private bool m_bIsArray = false;

		/// //////////////////////////////////////////
		public CTypeResultatExpression()
		{
		}

		/// //////////////////////////////////////////
		public CTypeResultatExpression( Type tp, bool bIsArrayOfTypeNatif)
		{
			m_typeDotNetNatif = tp;
			m_bIsArray = bIsArrayOfTypeNatif;
			m_typeExpressionNatif = null;
		}

		/// //////////////////////////////////////////
		protected CTypeResultatExpression ( CTypeResultatExpression tp, bool bIsArrayOfTypeNatif )
		{
			m_typeExpressionNatif = tp;
			m_typeDotNetNatif = null;
			m_bIsArray = bIsArrayOfTypeNatif;
		}

		/// //////////////////////////////////////////
		public bool IsTypeDotNetDirect
		{
			get
			{
				return m_typeDotNetNatif != null;
			}
		}

		/// //////////////////////////////////////////
		public Type TypeDotNetNatif
		{
			get
			{
                if (m_typeDotNetNatif == null)
                {
                    if (m_typeExpressionNatif != null)
                        return m_typeExpressionNatif.TypeDotNetNatif;
                    else
                        return null;
                }
				return m_typeDotNetNatif;
			}
			set
			{
				m_typeDotNetNatif = value;
				m_typeExpressionNatif = null;
			}
		}

		/// //////////////////////////////////////////
		public CTypeResultatExpression TypeResultExpressionNatif
		{
			get
			{
				return m_typeExpressionNatif;
			}
			set
			{
				m_typeExpressionNatif = value;
				m_typeDotNetNatif = null;
			}
		}

		/// //////////////////////////////////////////
		///Retourne un type étant un array du type passé en paramètre
		public  CTypeResultatExpression GetTypeArray(  )
		{
			if ( IsTypeDotNetDirect )
			{
				if ( !IsArrayOfTypeNatif )
				{
					CTypeResultatExpression type = GetClone();
					type.IsArrayOfTypeNatif = true;
					return type;
				}
				else
				{
					return new CTypeResultatExpression(this, true);
				}
			}
			else
			{
				return new CTypeResultatExpression(this, true);
			}
		}

		/// //////////////////////////////////////////
		/// retoure le type des éléments du table
		public CTypeResultatExpression GetTypeElements(  )
		{
            if (!m_bIsArray)
            {
                if (typeof(IEnumerable).IsAssignableFrom(TypeDotNetNatif))
                    return new CTypeResultatExpression(typeof(object), true);
                return null;
            }
			if ( IsTypeDotNetDirect )
			{
				CTypeResultatExpression type = GetClone();
				type.IsArrayOfTypeNatif = false;
				return type;
			}
			else
			{
				return TypeResultExpressionNatif.GetClone();
			}
		}

		/// //////////////////////////////////////////
		public bool IsArrayOfTypeNatif
		{
			get
			{
				return m_bIsArray;
			}
			set
			{
				m_bIsArray = value;
			}
		}

		/// //////////////////////////////////////////
		protected CTypeResultatExpression GetClone()
		{
			CTypeResultatExpression type;
			if ( m_typeDotNetNatif != null ) 
				type = new CTypeResultatExpression(m_typeDotNetNatif, m_bIsArray );
			else
				type = new CTypeResultatExpression(m_typeExpressionNatif, m_bIsArray);
			return type;
		}

		/// //////////////////////////////////////////
		public bool CanConvertTo ( CTypeResultatExpression type )
		{
			if ( type == null )
				return true;
			if ( !type.IsArrayOfTypeNatif && type.IsTypeDotNetDirect && type.TypeDotNetNatif == typeof(Object) )
				return true;//tout peut être consideré comme un object
			if ( type.IsArrayOfTypeNatif != IsArrayOfTypeNatif )
				return false;
			if ( type.IsTypeDotNetDirect != IsTypeDotNetDirect )
				return false;
			if ( type.IsTypeDotNetDirect && IsTypeDotNetDirect )
			{
				Type tp1NonNullable = this.TypeDotNetNatif;
				Type tp2NonNullable = type.TypeDotNetNatif;
				if (tp1NonNullable.IsGenericType && tp1NonNullable.GetGenericTypeDefinition() == typeof(Nullable<>))
					tp1NonNullable = tp1NonNullable.GetGenericArguments()[0];
				if (tp2NonNullable.IsGenericType && tp2NonNullable.GetGenericTypeDefinition() == typeof(Nullable<>))
					tp2NonNullable = tp2NonNullable.GetGenericArguments()[0];
				return CanConvert(tp1NonNullable, tp2NonNullable);
			}
			
			return this.TypeResultExpressionNatif.CanConvertTo(type.TypeResultExpressionNatif);
		}

		private bool FiltreType ( Type m, object filterCriteria )
		{
			return m == filterCriteria;
		}

		private bool CanConvert ( Type tpSource, Type tpDest )
		{
			if ( tpSource == null || tpDest == null )
				return true;
			if ( tpDest.IsAssignableFrom(tpSource) )
				return true;
			if ( tpSource == tpDest || tpSource.IsSubclassOf(tpDest)  )
				return true;
			if ( tpDest.IsAssignableFrom ( tpSource ) )
				//if ( tpDest.IsInterface  && tpSource.FindInterfaces(new System.Reflection.TypeFilter(FiltreType), tpDest).Length!=0)
				return true;
			//Tente la conversion
			try
			{
				object o1, o2;
				if ( tpSource == typeof(String) )
					o1 = new String('A',1); //Pas tres beau, mais il n'y a pas de constructeur par défaut pour les String
				else
#if PDA
					o1	= Activator.CreateInstance(tpSource);
#else
					o1	= Activator.CreateInstance(tpSource, new object[0]);
#endif
				o2 = Convert.ChangeType(o1, tpDest, null);
				return true;
			}
			catch
			{
			}
			return false;
		}

		/// //////////////////////////////////////////
		public bool CanConvertTo ( Type tp )
		{
			return CanConvertTo ( new CTypeResultatExpression(tp, false) );
		}

		/// //////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion( ref nVersion );
			if ( !result )
				return result;
			bool bHasType = m_typeDotNetNatif != null;
			serializer.TraiteBool ( ref bHasType );
			if ( bHasType )
			{
				serializer.TraiteType ( ref m_typeDotNetNatif );
			}
			else
				m_typeDotNetNatif = null;
			serializer.TraiteBool ( ref m_bIsArray );
			return result;
		}

		public override bool Equals ( object obj )
		{
			if ( !(obj is CTypeResultatExpression ) )
				return false;
			CTypeResultatExpression t2 = (CTypeResultatExpression)obj;
			if ( t2.TypeDotNetNatif != TypeDotNetNatif )
				return false;
			if ( t2.IsArrayOfTypeNatif != IsArrayOfTypeNatif )
				return false;
			return true;
		}


		public override int GetHashCode()
		{
			return TypeDotNetNatif.GetHashCode()+(IsArrayOfTypeNatif?1:0);
		}

		public override string ToString()
		{
			string strType = m_typeDotNetNatif.ToString();
			if ( m_bIsArray )
				strType += "[]";
			return strType;
		}

		public string ToStringConvivial()
		{
			string strType = DynamicClassAttribute.GetNomConvivial(m_typeDotNetNatif);
			if ( m_bIsArray )
				strType += "[]";
			return strType;
		}

        /// <summary>
        /// Retourne un CTypeResultatExpression à partir d'un type dotnet
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static CTypeResultatExpression FromTypeDotNet ( Type tp )
        {
            bool bIsArray = false;
            if (tp.IsGenericType && (tp.GetGenericTypeDefinition() == typeof(IList<>) || tp.GetGenericTypeDefinition()==typeof(IEnumerable<>)))
            {
                bIsArray = true;
                tp = tp.GetGenericArguments()[0];
            }
            else
            {
                if (tp.HasElementType)
                {
                    tp = tp.GetElementType();
                    bIsArray = true;
                }
            }
            return new CTypeResultatExpression ( tp, bIsArray );
        }




	}
}