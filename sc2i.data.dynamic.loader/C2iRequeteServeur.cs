using System;
using System.Data;

using sc2i.common;
using sc2i.data.serveur;
using sc2i.data.dynamic;
using sc2i.multitiers.server;
using System.Collections;
using sc2i.expression;


namespace sc2i.data.dynamic.loader
{
    /// <summary>
    /// Description résumée de C2iRequeteServeur.
    /// </summary>
    public class C2iRequeteServeur : C2iObjetServeur, I2iRequeteServeur
    {
        /// ////////////////////////////////
        public C2iRequeteServeur()
        {
        }

        /// ////////////////////////////////
        public C2iRequeteServeur(int nIdSession)
            : base(nIdSession)
        {
        }

        /// ////////////////////////////////
        public CResultAErreur ExecuteRequete(C2iRequete requete, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, bool bStructureOnly)
        {
            CResultAErreur result = CResultAErreur.True;
            Type typeReference = requete.TypeReferencePourConnexion;
            if (typeReference != null)
            {
                ///TODO
                ///Problème VersionObjet
                object objServeur = CContexteDonnee.GetTableLoader(CContexteDonnee.GetNomTableForType(typeReference), null, IdSession);
                if (objServeur != null)
                    typeReference = objServeur.GetType();
                else
                    typeReference = null;
            }

            IDatabaseConnexion con;
            if (typeReference == null)
                con = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, "");
            else
                con = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, typeReference);


            object[] lstParams = null;
            string strRequete = GetTexteRequeteFinaleSql(requete, elementAVariables, ref lstParams, con);
            int nOldTimeOut = con.CommandTimeOut;
            con.CommandTimeOut = 60 * 10;

            IDataAdapter adapter = con.GetAdapterForRequete(strRequete, lstParams);
            DataSet ds = new DataSet();
            try
            {
                lock (con)
                {
                    if (bStructureOnly)
                        adapter.FillSchema(ds, SchemaType.Source);
                    else
                        adapter.Fill(ds);
                }
                if (ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    if (requete.TableauCroise != null)
                    {
                        result = requete.TableauCroise.CreateTableCroisee(table);
                        if (result)
                            table = (DataTable)result.Data;
                    }

                    table.TableName = "DONNEES_REQUETE";
                    result.Data = table;
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in the request '@1'|102", strRequete));
            }
            finally
            {
                con.CommandTimeOut = nOldTimeOut;
                CUtilDataAdapter.DisposeAdapter(adapter);
            }
            return result;
        }

        /// /////////////////////////////////////////////
        ///<summary>
        ///retourne la requête formattée pour SQL.
        ///</summary>
        private string GetTexteRequeteFinaleSql(C2iRequete requete, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, ref object[] valeursParametres, IDatabaseConnexion database)
        {
            ArrayList lstValeursParametres = new ArrayList();
            string strTexte = requete.TexteRequete;
            int nPos = strTexte.IndexOf("[@");
            int nLastPos = nPos;
            while (nPos > 0)
            {
                //Cherche le nom de la variable
                string strNomVar = "";
                nPos += 2;
                while (nPos < strTexte.Length && strTexte[nPos] != ']')
                {
                    strNomVar += strTexte[nPos];
                    nPos++;
                }
                string strIdVar = "";
                foreach (CVariableDynamique variable in elementAVariables.ListeVariables)
                {
                    if (strNomVar.ToUpper() == variable.Nom.ToUpper())
                    {
                        strIdVar = variable.IdVariable;
                        break;
                    }
                }
                if (strIdVar != "")
                {
                    object val = elementAVariables.GetValeurChamp(strIdVar);
                    lstValeursParametres.Add(val);
                    strTexte = strTexte.Replace("[@" + strNomVar + "]", database.GetNomParametre(lstValeursParametres.Count.ToString()));

                }
                if (nLastPos + 1 < strTexte.Length)
                {
                    nPos = strTexte.IndexOf("[@", nLastPos + 1);
                    nLastPos = nPos;
                }
                else
                    nPos = -1;
            }
            valeursParametres = (object[])lstValeursParametres.ToArray(typeof(object));
            return strTexte;

        }
    }
}
