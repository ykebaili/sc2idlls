using System;
using System.Reflection;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CInfoStructureDynamique.
	/// </summary>
	public class CInfoChampDynamique : I2iSerializable
	{
		private string m_strNomChamp;
		public Type m_typeDonnee;

        /// <summary>
        /// ATTENTION : valeur non sérialisée (pour optim)
        /// </summary>
		public readonly CInfoStructureDynamique StructureValeur;


		private string m_strNomPropriete;
		public readonly string Rubrique;

        public CInfoChampDynamique()
        {
        }
		
        
        
        public CInfoChampDynamique ( 
			string strNomChamp, 
			Type typeDonnee, 
			string strNomPropriete, 
			string strRubrique,
			CInfoStructureDynamique structureValeur )
		{
			m_strNomChamp = strNomChamp;
			TypeDonnee = typeDonnee;
			m_strNomPropriete = strNomPropriete;
			StructureValeur = structureValeur;
			Rubrique = strRubrique;
		}

		public CInfoChampDynamique Clone()
		{
			CInfoChampDynamique info = new CInfoChampDynamique ( 
				NomChamp,
				TypeDonnee,
				NomPropriete ,
				Rubrique,
				StructureValeur );
			return info;
		}

		public string NomPropriete
		{
			get
			{
				return m_strNomPropriete;
			}
		}

		public string NomChamp
		{
			get
			{
				return m_strNomChamp;
			}
		}

        public Type TypeDonnee
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

		public void InsereParent ( CInfoChampDynamique info )
		{
			m_strNomPropriete = info.NomPropriete+"."+NomPropriete;
			m_strNomChamp = m_strNomChamp+" "+info.NomChamp;
		}

        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomChamp);
            serializer.TraiteString(ref m_strNomPropriete);
            serializer.TraiteType(ref m_typeDonnee);
            return result;
        }
	}



	public class CInfoStructureDynamique
	{
		ArrayList m_lstChamps = new ArrayList();
		private string m_strNomConvivial = "";
		private Type m_type;
		/// /////////////////////////////////////////
		public CInfoStructureDynamique()
		{
		}

		/// /////////////////////////////////////////
		public string NomConvivial
		{
			get
			{
				return m_strNomConvivial;
			}
			set
			{
				m_strNomConvivial = value;
			}
		}

		////////////////////////////////////////////
		public Type TypeAssocie
		{
			get
			{
				return m_type;
			}
		}

		/// /////////////////////////////////////////
		public ArrayList Champs
		{
			get
			{
				return m_lstChamps;
			}
			set
			{
				m_lstChamps = value;
			}
		}

		////////////////////////////////////////////
		public string GetNomConvivial ( string strNomPropriete )
		{
			string[] strProps = strNomPropriete.Split('.');
			if ( strProps.Length == 0 )
				return "";
			string strNom = strProps[0];
			string strReste = "";
			if ( strProps.Length > 1 )
				strReste = strNomPropriete.Substring(strNom.Length+1);
			foreach ( CInfoChampDynamique champ in m_lstChamps )
			{
				if ( champ.NomPropriete == strNom )
				{
					if ( strProps.Length > 1 )
					{
						if ( champ.StructureValeur != null )
							return champ.StructureValeur.GetNomConvivial(strReste)+" "+champ.NomChamp;
						return strReste + " "+champ.NomChamp;
					}
					return champ.NomChamp;
				}
			}
			return strNomPropriete;
		}

		////////////////////////////////////////////
		public delegate CInfoStructureDynamique GetStructureDelegate(Type tp, int nDepth );

		public static GetStructureDelegate GetStructureSurchargee;

		////////////////////////////////////////////
		public static CInfoStructureDynamique GetStructure ( Type tp, int nDepth )
		{
			if ( GetStructureSurchargee != null )
			{
				CInfoStructureDynamique info = GetStructureSurchargee ( tp, nDepth );
				if ( info != null )
					info.m_type = tp;
				return info;
			}
			if ( nDepth < 0 )
				return null;
			object[] attribs = tp.GetCustomAttributes(typeof(DynamicClassAttribute), true);
			CInfoStructureDynamique infoStructure = new CInfoStructureDynamique();
			infoStructure.m_type = tp;
			if ( attribs.Length != 0 )
			{
				infoStructure.NomConvivial = ((DynamicClassAttribute)attribs[0]).NomConvivial;
			}

			foreach ( PropertyInfo property in tp.GetProperties() )
			{
				attribs = property.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				if ( attribs.Length>0 )
				{
					DynamicFieldAttribute fieldAttr = (DynamicFieldAttribute)attribs[0];
					tp = property.PropertyType;
					CInfoStructureDynamique infoFils = GetStructure ( tp, nDepth-1 );
					if ( infoFils != null && infoFils.Champs.Count == 0 )
						infoFils = null;
					CInfoChampDynamique info = new CInfoChampDynamique (
						fieldAttr.NomConvivial,
						tp,
						property.Name,
						"",
						infoFils );
					infoStructure.Champs.Add ( info );
				}
			}
			return infoStructure;
		}

		////////////////////////////////////////////
		public delegate string GetDonneeDynamiqueStringDelegate(object obj, string strPropriete, string strValeurSiNull );

		public static GetDonneeDynamiqueStringDelegate GetDonneeDynamiqueStringSurchargee;

		public static string GetDonneeDynamiqueString ( object obj, string strPropriete, string strValeurSiNull )
		{
			if ( GetDonneeDynamiqueStringSurchargee != null )
				return GetDonneeDynamiqueStringSurchargee ( obj, strPropriete, strValeurSiNull );
			return CInterpreteurTextePropriete.GetStringValue ( obj, strPropriete, strValeurSiNull );
		}

        ////////////////////////////////////////////
        public delegate string GetProprieteDotNetDelegate(string strPropriete);

        public static GetProprieteDotNetDelegate GetProprieteDotNetSurchargee;

        /// <summary>
        /// Retourne une propriété dot net à partir du nom de propriété
        /// </summary>
        /// <param name="strPropriete"></param>
        /// <returns></returns>
        public static string GetProprieteDotNet(string strPropriete)
        {
            if (GetProprieteDotNetSurchargee != null)
                return GetProprieteDotNetSurchargee(strPropriete);
            return strPropriete;
        }

	}


}
