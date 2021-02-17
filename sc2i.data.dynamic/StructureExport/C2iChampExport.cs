using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iChampExport.
	/// </summary>
	[Serializable]
	public class C2iChampExport : I2iSerializable, IChampDeTable
	{
		private string m_strNomChamp = "";
		private C2iOrigineChampExport m_origine;

		/// //////////////////////////////////////////////////////////////
		public C2iChampExport()
		{
		}

		/// //////////////////////////////////////////////////////////////
		public string NomChamp
		{
			get
			{
				return m_strNomChamp;
			}
			set
			{
				m_strNomChamp = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public object GetValeur(object obj, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			return m_origine.GetValeur(obj, cacheValeurs, restriction);
		}

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExport Origine
		{
			get
			{
				return m_origine;
			}
			set
			{
				m_origine = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public Type TypeDonnee
		{
			get
			{
				Type tp = Origine.TypeDonnee;
				if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
					tp = tp.GetGenericArguments()[0];
				return tp;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomChamp );
			I2iSerializable objet = Origine;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			Origine = (C2iOrigineChampExport)objet;
			return result;
		}

		/// /////////////////////////////////////////////

		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}


		/// /////////////////////////////////////////////
		public void AddProprietesOrigineToTable ( 
            Type typeSource,
            Hashtable tableOrigines, 
            string strChemin, 
            CContexteDonnee contexteDonnee )
		{
			if ( Origine != null )
				Origine.AddProprietesOrigineToTable ( typeSource, tableOrigines, strChemin, contexteDonnee );
		}
		//----------------------------------------------------------------------------------
	}
}
