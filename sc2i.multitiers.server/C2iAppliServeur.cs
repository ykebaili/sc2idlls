using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sc2i.multitiers.server
{
    /// <summary>
    /// Contient des variables globales sur le fonctionnement du serveur
    /// </summary>
    
    public class C2iAppliServeur
    {

        private static Hashtable m_tableOptions = new Hashtable();

        public static object GetValeur(string strCle)
        {
            return m_tableOptions[strCle];
        }

        public static void SetValeur(string strCle, object valeur)
        {
            m_tableOptions[strCle] = valeur;
        }
    }


}
