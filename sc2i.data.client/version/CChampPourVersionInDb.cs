using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using sc2i.common;
using System.Data;

namespace sc2i.data
{

	/// <summary>
	/// Représente un champ de la base de données, dans une version
	/// </summary>
	public class CChampPourVersionInDb : IChampPourVersion
	{
		public const string c_TypeChamp = "DB_FIELD";
		private string m_strFieldKey;
		private string m_strNomConvivial;

		//----------------------------------------------------
		public CChampPourVersionInDb ( string strFieldKey, string strNomConvivial)
		{
			m_strFieldKey = strFieldKey;
			m_strNomConvivial = strNomConvivial;
		}

		//----------------------------------------------------
		[DynamicField("Field Key")]
		public string FieldKey
		{
			get { return m_strFieldKey; }
		}

        //----------------------------------------------------
        public string NomPropriete
        {
            get
            {
                return m_strFieldKey;
            }
        }

		//----------------------------------------------------
		[DynamicField("Friendly field name")]
		public string NomConvivial
		{
			get
			{
				return m_strNomConvivial;
			}
		}

		//----------------------------------------------------
		[DynamicField("Field type key")]
		public string TypeChampString
		{
			get { return c_TypeChamp; }
		}

		//-----------------------------------------------------
		public override int GetHashCode()
		{
			return (c_TypeChamp + m_strFieldKey).GetHashCode();
		}

		//-----------------------------------------------------
		public override bool Equals(object obj)
		{
			if (!(obj is CChampPourVersionInDb))
				return false;
			return ((CChampPourVersionInDb)obj).FieldKey == FieldKey;
		}

        //-----------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }


        //-----------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strFieldKey);
            serializer.TraiteString(ref m_strNomConvivial);
            return result;
        }

						

	}


}
