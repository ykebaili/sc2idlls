using System;
using System.Reflection;

using sc2i.common;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CDefinitionMethodeDynamique.
	/// </summary>
	[Serializable]
	public class CDefinitionMethodeDynamique : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "MD";

		private string m_strInfoMethode ="";
		private string[] m_strInfosParametres = new string[0];

		public CDefinitionMethodeDynamique()
			:base()
		{
		}
		public CDefinitionMethodeDynamique ( 
			string strNomConvivial,
			string strNomMethode,
			CTypeResultatExpression type,
			bool bHasSubProprietes)
			:base (
			strNomConvivial,
			strNomMethode,
			type, 
			bHasSubProprietes,
			true)

		{
		}

		public override string CleType
		{
			get { return c_strCleType; }
		}

		public CDefinitionMethodeDynamique ( 
			string strNomConvivial,
			string strNomMethode,
			CTypeResultatExpression type,
			bool bHasSubProprietes,
			string strInfoMethode,
			string[] strInfosParametres)
			:base (
			strNomConvivial,
			strNomMethode,
			type, 
			bHasSubProprietes,
			true)
		{
			m_strInfoMethode = strInfoMethode;
			m_strInfosParametres = strInfosParametres;
		}

		public string InfoMethode
		{
			get
			{
				return m_strInfoMethode;
			}
		}

		public string[] InfosParametres
		{
			get
			{
				return m_strInfosParametres;
			}
		}

        public static CDefinitionMethodeDynamique GetDefinitionMethode(Type tp, string strMethode)
        {
            MethodInfo info = tp.GetMethod(strMethode );
            if (info == null)
                return null;
            object[] attribs = info.GetCustomAttributes(typeof(DynamicMethodAttribute), true);
            if (attribs.Length == 1)
            {
                DynamicMethodAttribute attrib = (DynamicMethodAttribute)attribs[0];
                CTypeResultatExpression typeRes = CTypeResultatExpression.FromTypeDotNet(info.ReturnType);
                CDefinitionMethodeDynamique def = new CDefinitionMethodeDynamique(
                    info.Name,
                    info.Name,
                    typeRes,
                    CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(typeRes.TypeDotNetNatif),
                    attrib.Descriptif,
                    attrib.InfosParametres);
                def.Rubrique = I.T("Methods|58");
                return def;
            }
            return null;
        }

	}

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiqueMethodesDynamiques : IFournisseurProprieteDynamiquesSimplifie
	{
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiqueMethodesDynamiques());
		}

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if (objet == null)
				return lstProps.ToArray();
			Type tp = objet.TypeAnalyse;
			if (tp == null)
				return lstProps.ToArray();

			//Va chercher les propriétés
			foreach (MethodInfo methode in tp.GetMethods())
			{
				object[] attribs = methode.GetCustomAttributes(typeof(DynamicMethodAttribute), true);
				if (attribs.Length == 1)
				{
					DynamicMethodAttribute attrib = (DynamicMethodAttribute)attribs[0];
					CTypeResultatExpression typeRes = CTypeResultatExpression.FromTypeDotNet ( methode.ReturnType );
                    ParameterInfo[] parametres = methode.GetParameters();
                    StringBuilder bl = new StringBuilder();
                    bl.Append(methode.Name);
                    bl.Append("(");
                    foreach (ParameterInfo info in parametres)
                    {
                        bl.Append(info.Name);
                        bl.Append("; ");
                    }
                    if (parametres.Length > 0)
                        bl.Remove(bl.Length - 2, 2);
                    bl.Append(")");
					CDefinitionProprieteDynamique def = new CDefinitionMethodeDynamique(
						bl.ToString(),
						methode.Name,
						typeRes,
                        CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(typeRes.TypeDotNetNatif),
						attrib.Descriptif,
						attrib.InfosParametres);
					def.Rubrique = I.T("Methods|58");
					lstProps.Add(def);
				}
			}
			foreach (CMethodeSupplementaire methode in CGestionnaireMethodesSupplementaires.GetMethodsForType(tp))
			{
				CDefinitionProprieteDynamique def = new CDefinitionMethodeDynamique(
					methode.Name,
					methode.Name,
					new CTypeResultatExpression(methode.ReturnType, methode.ReturnArrayOfReturnType),
					CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(methode.ReturnType));
				def.Rubrique = I.T("Methods|58");
				lstProps.Add(def);
			}
			return lstProps.ToArray();
		}
	}
}
