using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.win32.common;
using sc2i.data.dynamic;
using System.Collections;
using sc2i.expression;

namespace sc2i.win32.data.navigation
{
    public class CConvertisseurInfoStructureDynamiqueToDefinitionChamp
    {
        private static Hashtable m_tableSerializeToDefinition = new Hashtable();

        /// <summary>
		/// Surchages de CInfoStructureDynamique
		/// </summary>
		/// <param name="tp"></param>
		/// <param name="nDepth"></param>
		/// <returns></returns>
		//-----------------------------------------------------------------
		public static CInfoStructureDynamique GetStructure ( Type tp, int nDepth )
		{
			if ( nDepth < 0 )
				return null;
			CFournisseurPropDynStd fournisseur = new CFournisseurPropDynStd(false);
			ArrayList lst = new ArrayList();
			foreach ( CDefinitionProprieteDynamique def in  fournisseur.GetDefinitionsChamps(tp, 0, null) )
			{
				if ( !def.TypeDonnee.IsArrayOfTypeNatif )
				{
					CInfoChampDynamique info = null;
					CInfoStructureDynamique infoFils = GetStructure ( def.TypeDonnee.TypeDotNetNatif, nDepth-1 );
					if ( infoFils != null && infoFils.Champs.Count == 0 )
						infoFils = null;					
					//Champs simples, interpretés par CInterpreteurTextePropriete
					if ( def.GetType() == typeof(CDefinitionProprieteDynamique ) )
					{
						info = new CInfoChampDynamique ( 
							def.Nom,
							def.TypeDonnee.TypeDotNetNatif,
							def.NomPropriete,
							def.Rubrique,
							infoFils );
					}
					else
					{
						CStringSerializer serializer = new CStringSerializer( ModeSerialisation.Ecriture );
						I2iSerializable obj = (I2iSerializable)def;
						serializer.TraiteObject ( ref obj );
						info = new CInfoChampDynamique (
							def.Nom,
							def.TypeDonnee.TypeDotNetNatif,
							"~#"+serializer.String+"~@#@",
							def.Rubrique,
							infoFils );
					}
					lst.Add ( info );
				}
			}
			CInfoStructureDynamique infoStructure = new CInfoStructureDynamique();
			infoStructure.NomConvivial = DynamicClassAttribute.GetNomConvivial ( tp );
			infoStructure.Champs = lst;
			return infoStructure;
		}

        //-----------------------------------------------------------------
        public static string GetProprieteDotNetFromProprieteStructureDynamique(string strProp)
        {
            string strRetour = "";
            string strSuite = strProp;
            while (strSuite.Length > 0)
            {
                CDefinitionProprieteDynamique defProp = GetDefinitionProprieteDynamique(strSuite, ref strSuite);
                CDefinitionProprieteDynamiqueDotNet propDotNet = defProp as CDefinitionProprieteDynamiqueDotNet;
                if (propDotNet == null)
                    return strProp;
                if (strRetour.Length > 0)
                    strRetour += ".";
                strRetour += propDotNet.NomProprieteSansCleTypeChamp;
            }
            return strRetour;
        }


        //-----------------------------------------------------------------
        public static CDefinitionProprieteDynamique GetDefinitionProprieteDynamique ( string strPropriete, ref string strSuite )
        {
            I2iSerializable objDef = null;
            if ( strPropriete.Length > 2 && 
				strPropriete.Substring(0,2)=="~#" )
			{
				string strStart = strPropriete.Substring(2);
				strSuite = "";
				int nPos = strStart.IndexOf("~@#@");
				if (nPos >= 0)
				{
					strSuite = strStart.Substring(nPos + 4);
					if (strSuite.Length > 1 && strSuite.Substring(0, 1) == ".")
						strSuite = strSuite.Substring(1);
					strStart = strStart.Substring(0, nPos);
				}

				CStringSerializer serializer = new CStringSerializer(strPropriete.Substring(2), ModeSerialisation.Lecture );
				objDef = (I2iSerializable)m_tableSerializeToDefinition[strPropriete];
				if ( objDef == null )
				{
					if ( !serializer.TraiteObject ( ref objDef ) )					
						objDef = null;
					else
						m_tableSerializeToDefinition[strPropriete] = objDef;
				}
            }else
            {
                //Il s'agit d'un nom de champ
                string[] strDatas = strPropriete.Split('.');
                string strProp = "";
                foreach (string strData in strDatas)
                {
                    strProp += CDefinitionProprieteDynamique.c_strCaractereStartCleType +
                        CDefinitionProprieteDynamiqueDotNet.c_strCleTypeDefinition +
                        CDefinitionProprieteDynamique.c_strCaractereEndCleType +
                        strData + ".";
                }
                if (strProp.Length > 1)
                    strProp = strProp.Substring(0, strProp.Length - 1);
                CDefinitionProprieteDynamiqueDotNet prop = new CDefinitionProprieteDynamiqueDotNet(strPropriete,
                    strProp, new CTypeResultatExpression(typeof(string), false), false, false, "");
                strSuite = "";
                return prop;
            }
            return objDef as CDefinitionProprieteDynamique;
        }

        //-----------------------------------------------------------------
        public static CDefinitionProprieteDynamique GetDefinitionProprieteDynamiqueForExport(string strPropriete)
        {
            string strProp = strPropriete;
            string strSuite = "";
            List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>();
            string strPropTotal = "";
            string strNomConv = "";
            while (strProp != "")
            {
                CDefinitionProprieteDynamique def = GetDefinitionProprieteDynamique(strProp, ref strSuite);
                if (def == null)
                    break;
                strPropTotal += "." + def.NomPropriete;
                strNomConv += "." + def.Nom;
                lstDefs.Add(def);
                strProp = strSuite;
            }
            if (strPropTotal.Length == 0)
                return null;
            strPropTotal = strPropTotal.Substring(1);
            strNomConv = strNomConv.Substring(1);
            CDefinitionProprieteDynamique defFinale = new CDefinitionProprieteDynamique(
                strNomConv,
                strPropTotal,
                lstDefs[lstDefs.Count() - 1].TypeDonnee,
                false,
                true);
            return defFinale;
        }
           
		
		//-----------------------------------------------------------------
        public static string GetDonneeDynamiqueString(object obj, string strPropriete, string strValeurSiNull)
        {
            string strSuite = "";
            CDefinitionProprieteDynamique defProp = GetDefinitionProprieteDynamique(strPropriete, ref strSuite);
            if (defProp != null)
            {
                try
                {
                    object retour = CInterpreteurProprieteDynamique.GetValue(obj, defProp).Data;
                    if (retour == null)
                        return strValeurSiNull;
                    if (strSuite != "")
                        return GetDonneeDynamiqueString(retour, strSuite, strValeurSiNull);
                    return retour.ToString();
                }
                catch
                {
                    return strValeurSiNull;
                }
            }
            try
            {
                return CInterpreteurTextePropriete.GetStringValue(obj, strPropriete, strValeurSiNull);
            }
            catch
            {
                return strValeurSiNull;
            }
        }


        /// <summary>
        /// Convertit une liste de colonnes GLColumn en structure d'export
        /// </summary>
        /// <param name="colonnes"></param>
        /// <returns></returns>
        public static C2iStructureExport ConvertToStructureExport(Type typeExporte, GLColumn[] colonnes)
        {
            CResultAErreur result = CResultAErreur.True;

            C2iStructureExport structure = new C2iStructureExport ( );
            structure.TypeSource = typeExporte;
            C2iTableExport table = new C2iTableExport();
            structure.Table = table;
            table.NomTable = "Data_Export";
            foreach (GLColumn col in colonnes)
            {
                CDefinitionProprieteDynamique defProp = GetDefinitionProprieteDynamiqueForExport(col.Propriete);
                if (defProp != null)
                {
                    C2iOrigineChampExportChamp origine = new C2iOrigineChampExportChamp(defProp);
                    C2iChampExport champ = new C2iChampExport();
                    champ.Origine = origine;
                    champ.NomChamp = col.Text;
                    table.AddChamp(champ);
                }
            }
            return structure;
        }
    }
}
