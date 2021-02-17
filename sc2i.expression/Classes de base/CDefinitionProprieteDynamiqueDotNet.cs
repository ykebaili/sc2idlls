using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Reflection;
using sc2i.expression.Classes_de_base;

namespace sc2i.expression
{
	/// <summary>
	/// Les définitions correspondant  à des propriétés .Net
	/// </summary>
	[AutoExec("Autoexec")]
	[Serializable]
	public class CDefinitionProprieteDynamiqueDotNet : 
		CDefinitionProprieteDynamique
	{
		public const string c_strCleTypeDefinition = "PP";

        
		//-----------------------------------------------
		public CDefinitionProprieteDynamiqueDotNet()
			: base()
		{
		}
		
		//-----------------------------------------------
		public CDefinitionProprieteDynamiqueDotNet
			(
			string strNomConvivial,
			string strNomPropriete,
			CTypeResultatExpression type,
			bool bHasSubProprietes,
			bool bIsReadOnly,
			string strRubrique
			)
			: base(strNomConvivial,
			strNomPropriete,
			type,
			bHasSubProprietes,
			bIsReadOnly,
			strRubrique)
		{
		}

		//-----------------------------------------------
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleTypeDefinition, typeof(CInterpreteurProprieteDynamiqueDotNet));
		}

		
		//----------------------------------------------------
		public override string CleType
		{
			get
			{
				return c_strCleTypeDefinition;
			}
		}

        //----------------------------------------------------
        public static CDefinitionProprieteDynamiqueDotNet GetDefinition(Type tp, string strNomPropriete)
        {
            PropertyInfo info = tp.GetProperty(strNomPropriete);
            if (info == null)
                return null;
            object[] attrs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
            string strNomConvivial = strNomPropriete;
            string strRubrique = "";
            if (attrs.Length > 0)
            {
                strNomPropriete = ((DynamicFieldAttribute)attrs[0]).NomConvivial;
                strRubrique = ((DynamicFieldAttribute)attrs[0]).Rubrique;
            }
            Type tpProp = info.PropertyType;
            bool bIsArray = tpProp.IsArray;
            if (bIsArray)
                tpProp = tpProp.GetElementType();
            bool bHasSubProprietes = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(tpProp);
            bool bReadOnly = info.GetSetMethod() == null;
            CDefinitionProprieteDynamiqueDotNet def = new CDefinitionProprieteDynamiqueDotNet(
                        strNomConvivial,
                        info.Name,
                        new CTypeResultatExpression(tpProp, bIsArray),
                        bHasSubProprietes,
                        bReadOnly,
                        strRubrique);
            return def;
        }

	}

	
	public class CInterpreteurProprieteDynamiqueDotNet : IInterpreteurProprieteDynamique
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				object valeur = CInterpreteurTextePropriete.GetValue(objet, strPropriete);
				result.Data = valeur;
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!CInterpreteurTextePropriete.SetValue(objet, strPropriete, valeur))
			{
				result.EmpileErreur(I.T("Error while affecting value|20003"));
			}
			return result;
		}

        public class COptimiseurProprieteDynamiqueDotNet : IOptimiseurGetValueDynamic
        {
            private MethodInfo m_methode = null;

            public COptimiseurProprieteDynamiqueDotNet(MethodInfo methode)
            {
                m_methode = methode;
            }

            public object GetValue(object objet)
            {
                if (m_methode == null)
                    return null;
                try
                {
                    return m_methode.Invoke(objet, null);
                }
                catch { }
                return null;
            }

            public Type GetTypeRetourne()
            {
                if (m_methode != null)
                    return m_methode.ReturnType;
                return null;
            }
        }

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            PropertyInfo info = tp.GetProperty(strPropriete);
            MethodInfo method = null;
            if (info != null)
                method = info.GetGetMethod();
            if ( method != null )
                return new COptimiseurProprieteDynamiqueDotNet(method);
            return null;
        }

        
    }


	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiqueDynamicField : IFournisseurProprieteDynamiquesSimplifie
	{
        private static Dictionary<Type, CDefinitionProprieteDynamique[]> m_cache = new Dictionary<Type, CDefinitionProprieteDynamique[]>();
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiqueDynamicField());
		}
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(
			CObjetPourSousProprietes objet, 
			CDefinitionProprieteDynamique defParente)
		{
			if (objet == null)
				return new CDefinitionProprieteDynamique[0];
			Type tp = objet.TypeAnalyse;
			if (tp == null)
				return new CDefinitionProprieteDynamique[0];
            CDefinitionProprieteDynamique[] defCache = null;
            if ( m_cache.TryGetValue ( tp, out defCache ) )
                return defCache;
			List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>();

            Dictionary<string, PropertyInfo> dicInfos = new Dictionary<string, PropertyInfo>();
            foreach (PropertyInfo info in tp.GetProperties() )
            {
                object[] attribs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
                if (attribs.Length == 0)
                    attribs = info.GetCustomAttributes(typeof(DynamicChildsAttribute), true);
                if ( attribs.Length == 1 )
                    dicInfos[info.Name] = info;
            }
            foreach (Type tpInterface in tp.GetInterfaces())
            {
                foreach (PropertyInfo info in tpInterface.GetProperties())
                {
                    if (!dicInfos.ContainsKey(info.Name))
                    {
                        object[] attribs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
                        if (attribs.Length == 0)
                            attribs = info.GetCustomAttributes(typeof(DynamicChildsAttribute), true);
                        if (attribs.Length == 1)
                            dicInfos[info.Name] = info;
                    }
                }
            }

			//Proprietes
			foreach ( PropertyInfo info in dicInfos.Values )
			{
				object[] attribs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				if (attribs.Length == 1)
				{
					DynamicFieldAttribute attrib = (DynamicFieldAttribute)attribs[0];
					bool bReadOnly = info.GetSetMethod() == null;
					Type tpProp = info.PropertyType;
					bool bIsArray = tpProp.IsArray;
					if (bIsArray)
						tpProp = tpProp.GetElementType();
					bool bHasSubProprietes = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(tpProp);
					CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueDotNet(
						attrib.NomConvivial,
						info.Name,
						new CTypeResultatExpression(tpProp, bIsArray),
						bHasSubProprietes,
						bReadOnly,
						attrib.Rubrique);
					lstDefs.Add ( def );
				}
				attribs = info.GetCustomAttributes(typeof(DynamicChildsAttribute), true);
				{
					if (attribs.Length == 1)
					{
						DynamicChildsAttribute attrib = (DynamicChildsAttribute)attribs[0];
						CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueDotNet(
							attrib.NomConvivial,
							info.Name,
							new CTypeResultatExpression(attrib.TypeFils, true),
							true,
							true,
							attrib.Rubrique);
						lstDefs.Add ( def );
					}
				}
			}
            defCache = lstDefs.ToArray();
            m_cache[tp] = defCache;
			return defCache;
		}
	}
}
