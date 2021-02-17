using System;
using System.Reflection;
using System.Collections;

using sc2i.multitiers.client;
using sc2i.common;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Alloue un objet serveur à partir du nom de sa classe
	/// </summary>
	public class C2iObjetServeurFactory : MarshalByRefObject, I2iObjetServeurFactory
	{
		private static Hashtable m_tableClasses = new Hashtable();
		/// ///////////////////////////////////////////////////////
		public C2iObjetServeurFactory()
		{
		}

		/// //////////////////////////////////////////////////////
		public I2iMarshalObjectDeSession GetNewObject ( string strClasse, int nIdSession )
		{	
			Type tp = Type.GetType(strClasse);
			if ( tp == null )
			{
				tp = (Type)m_tableClasses[strClasse];
				if ( tp == null )
				{
					foreach ( Assembly ass in AppDomain.CurrentDomain.GetAssemblies() )
					{
						foreach ( Type type in ass.GetExportedTypes() )
							if ( type.Name == strClasse )
							{
								m_tableClasses[strClasse] = type;
								tp = type;
								break;
							}
						if ( tp != null )
							break;
					}
				}
			}
			if ( tp == null )
				throw new Exception(I.T("@1 object allocation is impossible because the class doesn't exist|101",strClasse));
			I2iMarshalObjectDeSession obj = (I2iMarshalObjectDeSession)Activator.CreateInstance ( tp,new object[] {nIdSession} );
			return obj;
		}

		
	}
}
