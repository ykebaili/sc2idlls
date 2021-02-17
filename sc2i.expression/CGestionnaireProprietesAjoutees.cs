using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{

	public delegate object GetDynamicValueDelegate ( object source );
	public delegate CResultAErreur SetDynamicValueDelegate ( object source, object valeur );

	//----------------------------------------------------
	public class CInfoDynamicFieldAjoute
	{
		public readonly GetDynamicValueDelegate GetDynamicValue;
		public readonly SetDynamicValueDelegate SetDynamicValue;
		public readonly CTypeResultatExpression TypePropriete;
		public readonly string Rubrique;

		public CInfoDynamicFieldAjoute(
			CTypeResultatExpression typePropriete,
			GetDynamicValueDelegate getDynamicValue,
			SetDynamicValueDelegate setDynamicValue, 
			string strRubrique)
		{
			GetDynamicValue = getDynamicValue;
			SetDynamicValue = setDynamicValue;
			TypePropriete = typePropriete;
			Rubrique = strRubrique;
		}
	}
	//----------------------------------------------------
	[AutoExec("Autoexec")]
	public class CGestionnaireProprietesAjoutees : IFournisseurProprieteDynamiquesSimplifie
	{
		public static Dictionary<Type, Dictionary<string, CInfoDynamicFieldAjoute>> m_dicTypeToFields = new Dictionary<Type,Dictionary<string,CInfoDynamicFieldAjoute>>();

		//---------------------------------------------------
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur ( new CGestionnaireProprietesAjoutees() );
		}
		//---------------------------------------------------
		public static void RegisterDynamicField(
			Type tpSource,
			string strFieldName,
			CTypeResultatExpression typePropriete,
			GetDynamicValueDelegate getDynamicValue,
			SetDynamicValueDelegate setDynamicValue,
			string strRubrique
			)
		{
			Dictionary<string, CInfoDynamicFieldAjoute> dic = null;
			if (!m_dicTypeToFields.TryGetValue(tpSource, out dic))
			{
				dic = new Dictionary<string, CInfoDynamicFieldAjoute>();
				m_dicTypeToFields[tpSource] = dic;
			}
			if (!dic.ContainsKey(strFieldName))
			{
				dic[strFieldName] = new CInfoDynamicFieldAjoute(typePropriete, getDynamicValue, setDynamicValue, strRubrique);
			}
		}

		//---------------------------------------------------
		public static CInfoDynamicFieldAjoute GetInfo ( Type tp, string strField )
		{
			Dictionary<string, CInfoDynamicFieldAjoute> dic = null;
			if( m_dicTypeToFields.TryGetValue ( tp, out dic ) )
			{
				CInfoDynamicFieldAjoute info = null;
				if ( dic.TryGetValue ( strField, out info ) )
					return info;
			}
			return null;
		}


		//---------------------------------------------------
		public static GetDynamicValueDelegate GetGetDelegate(Type tp, string strField)
		{
			CInfoDynamicFieldAjoute info = null;
			while (tp!=null)
			{
				info = GetInfo(tp, strField);
				if (info != null)
					return info.GetDynamicValue;
				tp = tp.BaseType;
			}
			return null;
		}

		//---------------------------------------------------
		public static SetDynamicValueDelegate GetSetDelegate(Type tp, string strField)
		{
			CInfoDynamicFieldAjoute info = GetInfo(tp, strField);
			if (info != null)
				return info.SetDynamicValue;
			return null;
		}

        private static Dictionary<Type, CDefinitionProprieteDynamique[]> m_cache = new Dictionary<Type, CDefinitionProprieteDynamique[]>();
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			if (objet == null)
				return new CDefinitionProprieteDynamique[0];
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            Type tpRacine = objet.TypeAnalyse;
			Type tp = objet.TypeAnalyse;
            CDefinitionProprieteDynamique[] defCache = null;
            if (tpRacine != null)
            {
                if (m_cache.TryGetValue(tpRacine, out defCache))
                    return defCache;
            }
			while (tp != null)
			{
				Dictionary<string, CInfoDynamicFieldAjoute> dic = null;
				if (m_dicTypeToFields.TryGetValue(tp, out dic))
				{
					foreach (KeyValuePair<string, CInfoDynamicFieldAjoute> pair in dic)
					{
						CDefinitionProprieteDynamiqueAjoutee def = new CDefinitionProprieteDynamiqueAjoutee(
							tp,
							pair.Key,
							pair.Value,
							CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(pair.Value.TypePropriete.TypeDotNetNatif),
							pair.Value.Rubrique);
						lst.Add(def);
					}
				}
				tp = tp.BaseType;
			}
            defCache = lst.ToArray();
            if (tpRacine != null)
            {
                m_cache[tpRacine] = defCache;
            }
			return defCache;
		}

	}
}
