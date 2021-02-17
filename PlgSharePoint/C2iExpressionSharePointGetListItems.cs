using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Web.Services.Protocols;
using System.Xml;
using System.Net;

namespace PlgSharePoint
{
    [Serializable]
    [AutoExec("RegisterExpression")]
    public class C2iExpressionSharePointGetListItems : C2iExpressionAnalysable
    {
        public C2iExpressionSharePointGetListItems()
        {

        }

        /// //////////////////////////////////////////
        public static void RegisterExpression()
        {
            CAllocateur2iExpression.Register2iExpression(
                new C2iExpressionSharePointGetListItems().IdExpression,
                typeof(C2iExpressionSharePointGetListItems));
        }

        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = new CInfo2iExpression(
                0,
                "SharePointGetListItems",
                new CTypeResultatExpression(typeof(string), true),
                "SharePointGetListItems(URL, User, Password, ListeName, ViewFields, Query ,QueryOptions, RowLimit)" + Environment.NewLine +
                    I.T("Returns items from SharePoint List based on the specified Query|10000"),
                CInfo2iExpression.c_categorieDivers);

            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(
                typeof(string), // URL
                typeof(string), // Utilisteur
                typeof(string), // Mot de passe
                typeof(string), // ListeName (from table SharePoint)
                typeof(string), // ViewFields (clause select des champs à visualiser)
                typeof(string), // Query (clause where sous forme de requête XML)
                typeof(string), // QuerOptions
                typeof(int)     // RowLimit (nombre max de lignes)
                ));

            return info;
        }

        public override int GetNbParametresNecessaires()
        {
            return 8;
        }
            

        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.True;

            try
            {
                string strURL = (string)valeursParametres[0];
                if (strURL.Trim() == string.Empty)
                {
                    result.EmpileErreur(I.T("The SharePoint Web Service URL is empty|10001"));
                    return result;
                }
                string strUser = (string)valeursParametres[1];
                string strPassword = (string)valeursParametres[2];
                
                string strListName = (string)valeursParametres[3];
                if (strListName == "")
                {
                    result.EmpileErreur(I.T("The SharePoint List Name is empty|10002"));
                    return result;
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlElement XmlviewFields = xmlDoc.CreateElement("ViewFields");
                string strViewFields = (string)valeursParametres[4];
                if (strViewFields.Trim() != "")
                {
                    string[] strNomsChamps = strViewFields.Split(',', ';');
                    XmlviewFields.InnerXml = "";
                    foreach (string strFieldName in strNomsChamps)
                    {
                        XmlviewFields.InnerXml += "<FieldRef Name=\"" + strFieldName + "\" />";
                    }
                }
                string strQuery = (string)valeursParametres[5];
                XmlElement XmlQuery = xmlDoc.CreateElement("Query");
                XmlQuery.InnerXml = strQuery;

                string strQueryOptions = (string)valeursParametres[6];
                XmlElement XmlQueryOptions = xmlDoc.CreateElement("QueryOptions");
                XmlQueryOptions.InnerXml = strQueryOptions;

                int nRowLimit = (int)valeursParametres[7];
                string strRowLimit = nRowLimit > 0 ? nRowLimit.ToString() : "";
                
                FuturocomSharePoint.Lists listService = new PlgSharePoint.FuturocomSharePoint.Lists();
                listService.Url = strURL;
                listService.Credentials = new NetworkCredential(strUser, strPassword);

                XmlNode XmlResponse = listService.GetListItems(
                    strListName,
                    null,
                    XmlQuery,
                    XmlviewFields,
                    strRowLimit,
                    XmlQueryOptions,
                    null);

                if (XmlResponse != null)
                {
                    foreach (XmlNode node in XmlResponse)
                    {
                        if (node.NodeType == XmlNodeType.Element && node.Name == "rs:data")
                        {
                            foreach (XmlNode listItemNode in node)
                            {
                                if (listItemNode.NodeType == XmlNodeType.Element && listItemNode.Name == "z:row")
                                {
                                    // Créer une ligne de résultat dans le DataTable

                                }
                            }
                        }
                    }
                }


            }

            catch (SoapException ex)
            {
                result.EmpileErreur("Message:\n" + ex.Message + "\nDetail:\n" +
                    ex.Detail.InnerText + "\nStackTrace:\n" + ex.StackTrace);
            }
            catch (Exception e)
            {
                result.EmpileErreur(e.Message);
            }

            return result;
        }

    }
}
