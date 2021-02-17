using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using sc2i.common;
using System.Collections;

namespace sc2i.data.dynamic
{
	public class CChampCustomPourVersion : IChampPourVersion
	{
		public static string c_typeChamp = "CUST_FIELD";

		private CChampCustom m_champ = null;

		//-----------------------------------------------------
		public CChampCustomPourVersion(CChampCustom champ)
		{
			m_champ = champ;
		}


		//-----------------------------------------------------
		[DynamicField("Field Key")]
		public string FieldKey
		{
			get
			{
				return m_champ != null ? m_champ.Id.ToString() : "Unknown custom field";
			}
		}

        
		//-----------------------------------------------------
		[DynamicField("Friendly field name")]
		public string NomConvivial
		{
			get
			{
				return m_champ != null ? m_champ.LibelleConvivial : "Unknown custom field";
			}
		}

		//-----------------------------------------------------
		[DynamicField("Field type key")]
		public string TypeChampString
		{
			get { return c_typeChamp; }
		}

		//-----------------------------------------------------
		public CChampCustom ChampCustom
		{
			get
			{
				return m_champ;
			}
		}

		//-----------------------------------------------------
		public override int GetHashCode()
		{
			return (c_typeChamp + m_champ.Id).GetHashCode();
		}

		//-----------------------------------------------------
		public override bool Equals(object obj)
		{
			if (!(obj is CChampCustomPourVersion))
				return false;
			return ((CChampCustomPourVersion)obj).ChampCustom.Id == ChampCustom.Id;
		}

        //-----------------------------------------------------
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage des Id Champ en DbKey
        }

        //-----------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    //TESTDBKEYTODO : remplacer IdChamp par DbKey
                    CDbKey dbKeyChampWrite = ChampCustom != null ? ChampCustom.DbKey : null;
                    if (nVersion < 1)
                        serializer.ReadDbKeyFromOldId(ref dbKeyChampWrite, typeof(CChampCustom));
                    else
                        serializer.TraiteDbKey(ref dbKeyChampWrite);
                    break;
                case ModeSerialisation.Lecture:
                    CContexteDonnee contexte = serializer.GetObjetAttache(typeof(CContexteDonnee)) as CContexteDonnee;
                    if (contexte == null)
                        contexte = CContexteDonneeSysteme.GetInstance();
                    //TESTDBKEYTODO : remplacer IdChamp par DbKey
                    CDbKey dbKeyChampRead = null;
                    if (nVersion < 1)
                        serializer.ReadDbKeyFromOldId(ref dbKeyChampRead, typeof(CChampCustom));
                    else
                        serializer.TraiteDbKey(ref dbKeyChampRead);
                    if (dbKeyChampRead == null)
                        m_champ = null;
                    else
                    {
                        m_champ = new CChampCustom(contexte);
                        if (!m_champ.ReadIfExists(dbKeyChampRead))
                            m_champ = null;
                    }
                    break;
            }
            return result;
        }
	}
}
