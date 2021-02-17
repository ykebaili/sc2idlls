using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace futurocom.easyquery
{
    public interface IEasyQueryConnexion : I2iSerializable
    {
        void ClearCache(ITableDefinition table);

        bool CanFill(ITableDefinition tableDefinition);

        DataTable GetData(ITableDefinition tableDefinition, params string[] strIdsColonnesSource);

        /// <summary>
        /// Identifiant du type de connexion
        /// </summary>
        string ConnexionTypeId { get; }

        /// <summary>
        /// Nom du type de connexion
        /// </summary>
        string ConnexionTypeName { get; }

        /// <summary>
        /// Retourne la liste des propriétés de connexion
        /// </summary>
        IEnumerable<CEasyQueryConnexionProperty> ConnexionProperties { get; set; }

        /// <summary>
        /// Retourne une valeur de propriété de connexion
        /// </summary>
        /// <param name="strProperty"></param>
        /// <returns></returns>
        string GetConnexionProperty(string strProperty);

        /// <summary>
        /// Modifie une valeur de propriété de connexion
        /// </summary>
        /// <param name="strProperty"></param>
        /// <param name="strValeur"></param>
        void SetConnexionProperty(string strProperty, string strValeur);


        void FillStructureQuerySource(CEasyQuerySource source);

        void ClearStructure();
    }

    public class CEasyQueryConnexionProperty
    {
        private string m_strProperty = "";
        private string m_strValue = "";
        private object m_tag = null;

        public CEasyQueryConnexionProperty(string strProperty, string strValue)
        {
            m_strProperty = strProperty;
            m_strValue = strValue;
        }

        //-------------------------------------------
        public object Tag
        {
            get
            {
                return m_tag;
            }
            set
            {
                m_tag = value;
            }
        }

        //-------------------------------------------
        public string Property
        {
            get
            {
                return m_strProperty;
            }
        }

        //-------------------------------------------
        public string Value
        {
            get
            {
                return m_strValue;
            }
            set
            {
                m_strValue = value;
            }
        }

       
    }

}
