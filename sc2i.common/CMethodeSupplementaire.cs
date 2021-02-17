using System;
using System.Collections;
using System.Reflection;

namespace sc2i.common
{

	public sealed class CGestionnaireMethodesSupplementaires
	{
		//Type->Liste de méthodes dynamiques
		public static Hashtable m_tableTypeToMethod = new Hashtable();

        private CGestionnaireMethodesSupplementaires() { }

		/// <summary>
		/// enregistre une nouvelle méthode supplémentaire
		/// </summary>
		/// <param name="method"></param>
		public static void RegisterMethode ( CMethodeSupplementaire method )
		{
			ArrayList lstVals = (ArrayList)m_tableTypeToMethod[method.DeclaringType];
			if ( lstVals == null )
			{
				lstVals = new ArrayList();
				m_tableTypeToMethod[method.DeclaringType] = lstVals;
			}
			lstVals.Add ( method );
		}

		/// <summary>
		/// Retourne toutes les méthodes supplémentaires d'un type
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static CMethodeSupplementaire[] GetMethodsForType ( Type tp )
		{
			ArrayList lstMethodes = new ArrayList();
            Type tpOriginal = tp;
			while ( tp != null )
			{
				ArrayList lstForType = (ArrayList)m_tableTypeToMethod[tp];
				if ( lstForType != null )
					lstMethodes.AddRange ( lstForType );
				tp = tp.BaseType;
			}
            foreach (Type inter in tpOriginal.GetInterfaces())
            {
                ArrayList lstForType = (ArrayList)m_tableTypeToMethod[inter];
                if (lstForType != null)
                    lstMethodes.AddRange(lstForType);
            }

			return (CMethodeSupplementaire[])lstMethodes.ToArray(typeof(CMethodeSupplementaire));
		}

		public static CMethodeSupplementaire GetMethod ( Type tp, string strNomMethode )
		{
			foreach ( CMethodeSupplementaire method in GetMethodsForType(tp) )
				if ( method.Name == strNomMethode )
					return method;
			return null;
		}

	}

	public abstract class CMethodeSupplementaire 
	{
		private Type m_declaringType;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="declaringType"></param>
		protected CMethodeSupplementaire( Type declaringType )
		{
			m_declaringType = declaringType;
		}

		/// <summary>
		/// Type possédant la méthode
		/// </summary>
		public Type DeclaringType
		{
			get
			{
				return m_declaringType; 
			}
		}

		public abstract string Name{get;}

		public abstract string Description{get;}

		public abstract Type ReturnType{get;}

		//Vrai si le type retourné est un array du type ReturnType
		public abstract bool ReturnArrayOfReturnType{get;}

		public abstract CInfoParametreMethodeDynamique[] InfosParametres {get;}

		public abstract object Invoke ( object objetAppelle, params object[] parametres );
		
	}
}
