using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.data.serveur;
using sc2i.data;

namespace CTesteurBaseAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            CAccess97DatabaseConnexion cnx = new CAccess97DatabaseConnexion(0);
            cnx.ConnexionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Documents and Settings\\GENERIC\\Mes documents\\sites.MDB\"";

            CAccessDataBaseCreator creator = new CAccessDataBaseCreator(cnx);
            List<string> lstCols = creator.GetNomColonnes("SITES");

            creator.InitialiserDataBase();

            List<CInfoRelation> lst = new List<CInfoRelation>();
            creator.GetRelationsExistantes("SITES", ref lst);


        }
    }
}
