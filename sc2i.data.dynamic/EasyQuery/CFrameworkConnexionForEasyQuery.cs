using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using sc2i.common;
using futurocom.easyquery;

namespace sc2i.data.dynamic.easyquery
{
    [Serializable]
    [AutoExec("Autoexec")]
    public class CFrameworkConnexionForEasyQuery : IEasyQueryConnexion
    {
        public const string c_IdConnexionFramework = "2IFRMK";

        //---------------------------------------
        public CFrameworkConnexionForEasyQuery()
        {
        }

        //---------------------------------------
        public static void Autoexec()
        {
            CAllocateurEasyQueryConnexions.RegisterTypeConnexion ( c_IdConnexionFramework, typeof(CFrameworkConnexionForEasyQuery) );
        }

        
        //---------------------------------------
        public bool CanFill(ITableDefinition tableDefinition)
        {
            return typeof(CTableDefinitionFramework).IsAssignableFrom(tableDefinition.GetType());
        }

        //---------------------------------------
        public DataTable GetData(ITableDefinition tableDefinition, params string[] strIdsColonnesSource)
        {
            CTableDefinitionFramework table = tableDefinition as CTableDefinitionFramework;
            if (table != null)
            {
                CResultAErreur result = table.GetDonneesSource(null, strIdsColonnesSource);
                if (result)
                    return result.Data as DataTable;
            }
            return null;
        }

        

        //------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            return result;
        }





        #region IEasyQueryConnexion Membres
        //-----------------------------------------------------
        public void ClearStructure()
        {
        }

        //-----------------------------------------------------------
        public void ClearCache(ITableDefinition table)
        {
        }
        
        //-----------------------------------------------------
        public IEnumerable<CEasyQueryConnexionProperty> ConnexionProperties
        {
            get
            {
                List<CEasyQueryConnexionProperty> lst = new List<CEasyQueryConnexionProperty>();
                return lst.AsReadOnly();
            }
            set
            {
                foreach ( CEasyQueryConnexionProperty propriete in value )
                    SetConnexionProperty ( propriete.Property, propriete.Value );
            }
        }
        
        //-----------------------------------------------------
        public string ConnexionTypeId
        {
            get { return c_IdConnexionFramework; }
        }

        //-----------------------------------------------------
        public string ConnexionTypeName
        {
            get { return "TIMOS"; }
        }

        //-----------------------------------------------------
        public void FillStructureQuerySource(CEasyQuerySource source)
        {
            foreach ( CInfoClasseDynamique info in DynamicClassAttribute.GetAllDynamicClass ( typeof(TableAttribute) ))
            {
                CTableDefinitionFramework table = new CTableDefinitionFramework(info.Classe);
                source.AddTableUniquementPourObjetConnexion ( table );
            }
        }

        //-----------------------------------------------------
        public string GetConnexionProperty(string strProperty)
        {
            string strUpper = strProperty.ToUpper();
            return "";
        }

        //-----------------------------------------------------
        public void SetConnexionProperty(string strProperty, string strValeur)
        {
            string strUpper = strProperty.ToUpper();
        }

        #endregion
    }
}
