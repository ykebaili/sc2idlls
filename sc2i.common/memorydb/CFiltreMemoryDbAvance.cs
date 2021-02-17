using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

using sc2i.common;
using System.Text;
using System.Reflection;

namespace sc2i.common.memorydb
{
	/// <summary>
	/// Description résumée de CFiltreData.
	/// </summary>
	/*
	La chaine m_strFiltre contient le filtre sous forme de texte
	La liste de paramètre contient la liste des objets paramètres du filtre.
	Chaque paramètre est indiqué dans le filtre en utilisant "@n" où n 
	est les numéro du paramétre.
	Par exemple si strFiltre est NUMERO=@1
	et le premier paramètre est un entier 12,
	le filtre équivaut à NUMERO=12.
	Utiliser les IFormatteurFiltreData pour formatter la chaine 
	dans un format particulier.

	La syntaxe du filtre reprend la syntaxe du filtre DataTable (expression datatable dans l'aide)
	*/
	[Serializable]
    public class CFiltreMemoryDbAvance : CFiltreMemoryDb
	{
        protected Type m_typePrincipal = null;

		//////////////////////////////////////////////////
        public CFiltreMemoryDbAvance( Type typePrincipal )
            :base()
		{
            m_typePrincipal = typePrincipal;
		}

        //////////////////////////////////////////////////
        public Type TypePrincipal
        {
            get
            {
                return m_typePrincipal;
            }
        }

		//////////////////////////////////////////////////
		public CFiltreMemoryDbAvance (
            Type typePrincipal, 
            string strFiltre, 
            params object[] parametres )
            :base ( strFiltre, parametres )
		{
            m_typePrincipal = typePrincipal;
		}

		//////////////////////////////////////////////////
		private void CopyTo ( CFiltreMemoryDbAvance filtre )
		{
            base.CopyTo(filtre);
            filtre.m_typePrincipal = m_typePrincipal;
		}

		
		//////////////////////////////////////////////////
		public override CFiltreMemoryDb GetClone()
		{
			CFiltreMemoryDbAvance filtre = new CFiltreMemoryDbAvance(m_typePrincipal);
			CopyTo ( filtre );
			return filtre;
		}


        //////////////////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }


		//////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
            if (result)
                result = base.Serialize(serializer);
			if ( result )
                serializer.TraiteType ( ref m_typePrincipal );
			return result;
		}

        //////////////////////////////////////////////////
        private string GetStringRelation (
            ref Type typeInterroge,
            ref string strTableInterrogée, 
            PropertyInfo info )
        {
            object[] attrs = info.GetCustomAttributes ( typeof(MemoryParentAttribute), true );
            if ( attrs.Length > 0 )
            {
                //C'est une relation parente, elle retourne donc le type demandé
                Type tp = info.PropertyType;
                string strNomRelation = CMemoryDb.GetNomRelation ( strTableInterrogée, info.Name );
                attrs = tp.GetCustomAttributes ( typeof(MemoryTableAttribute), true);
                if ( attrs.Length == 0 )
                    return "";
                strTableInterrogée = ((MemoryTableAttribute)attrs[0]).NomTable;
                typeInterroge = tp;
                return "Parent("+strNomRelation+")";
            }
            /*le filtre sur les relations filles ne marche pas (contrainte .Net)
            attrs = info.GetCustomAttributes ( typeof(MemoryChildAttribute), true );
            if ( attrs.Length > 0 )
            {
                Type tp = info.PropertyType.GetGenericArguments()[0];
                MemoryChildAttribute attr = (MemoryChildAttribute)attrs[0];
                string strNomChamp = attr.NomChampForeignKey;
                attrs = tp.GetCustomAttributes ( typeof(MemoryTableAttribute), true);
                if ( attrs.Length == 0 )
                    return "";
                string strNomTableFille = ((MemoryTableAttribute)attrs[0]).NomTable;
                string strProp = "";
                if ( strNomChamp.Length > 0 )
                {
                    //On a un champ, il faut trouver la propriété correspondante
                    foreach ( PropertyInfo infoChamp in tp.GetProperties() )
                    {
                        attrs = infoChamp.GetCustomAttributes ( typeof ( MemoryFieldAttribute), true );
                        if ( attrs.Length > 0 && ((MemoryFieldAttribute)attrs[0]).NomChamp == strNomChamp )
                        {
                            strProp = infoChamp.Name;
                            break;
                        }
                    }
                }
                else
                    strProp = "Id";
                string strNomRelation = CMemoryDb.GetNomRelation ( strNomTableFille, strProp );
                strTableInterrogée = strNomTableFille;
                typeInterroge = tp;
                return "Child("+strNomRelation+")";
            }*/
            return "";
        }

        //////////////////////////////////////////////////
        public override string GetFiltreDataTable()
        {
            string strFiltre = Filtre;

            CMemoryDb mdbExemple = null;
            DataTable table = null;

            int nNumParametre = 1;

            string[] strComposants = strFiltre.Split(' ','=','<','>');
            foreach (string strComposant in strComposants)
            {
                if (strComposant.Contains("."))
                {
                    if (mdbExemple == null)
                        mdbExemple = new CMemoryDb();
                    if (table == null)
                        table = mdbExemple.GetTable(m_typePrincipal);
                    if (table == null)
                        throw new Exception("Error in filter, can not find " + m_typePrincipal.ToString() + " table");
                    //Cherche les tables demandées
                    string[] strTables = strComposant.Split('.');
                    string strViewSyntax = "";
                    bool bOk = true;
                    Type typeInterroge = m_typePrincipal;
                    string strTableInterrogée = table.TableName;
                    for (int nTable = 0; nTable < strTables.Length - 1; nTable++)
                    {
                        string strTable = strTables[nTable];
                        bOk = false;
                        foreach (PropertyInfo info in typeInterroge.GetProperties())
                        {
                            bool bPropOk = info.Name.ToUpper() == strTable.ToUpper();
                            if (!bPropOk)
                            {
                                //Regarde le nom de la table correspondantes
                                Type tp = info.PropertyType;
                                object[] attrs = tp.GetCustomAttributes(typeof(MemoryTableAttribute), true);
                                if (attrs.Length > 0)
                                {
                                    if (((MemoryTableAttribute)attrs[0]).NomTable.ToUpper() == strTable)
                                        bPropOk = true;
                                }
                            }

                            if (bPropOk)
                            {
                                string strS = GetStringRelation(ref typeInterroge, ref strTableInterrogée, info);
                                if (strS.Length == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    strViewSyntax += strS + ".";
                                    bOk = true;
                                    break;
                                }
                            }
                        }
                        if (!bOk)
                            break;
                    }
                    if (bOk)
                    {
                        strViewSyntax += strTables[strTables.Length - 1];//Ajoute le champ final
                        strFiltre = strFiltre.Replace(strComposant, strViewSyntax);
                    }
                    else
                        strFiltre += strComposant;
                }
            }


            foreach (object obj in Parametres)
            {
                string strReplace = obj.ToString();
                if (obj is String)
                {
                    strReplace = strReplace.Replace("'", "''");
                    strReplace = "'" + strReplace + "'";
                }
                if (obj is DateTime)
                {
                    DateTime dt = (DateTime)obj;
                    strReplace = "#" + dt.ToString("MM/dd/yyyy HH:mm") + "#";
                }
                if (obj is bool)
                    strReplace = ((bool)obj) ? "1" : "0";
                Regex ex = new Regex("(@" + nNumParametre.ToString() + ")(?<SUITE>[^0123456789]{1})");
                strFiltre = ex.Replace(strFiltre + " ", strReplace + "${SUITE}");
                nNumParametre++;
            }
            return strFiltre;
        }

        ////////////////////////////////////////////////////////
        private string GetStringFor(object obj)
        {
            string strReplace = obj.ToString();
            if (obj is String)
            {
                strReplace = strReplace.Replace("'", "\'");
                strReplace = "'" + strReplace + "'";
            }
            if (obj is DateTime)
            {
                DateTime dt = (DateTime)obj;
                strReplace = "#" + dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString() + "#";
            }
            if (obj is bool)
                strReplace = ((bool)obj) ? "1" : "0";
            return strReplace;
        }

		

	}
}

