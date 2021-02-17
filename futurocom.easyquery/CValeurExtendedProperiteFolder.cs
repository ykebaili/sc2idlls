using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace futurocom.easyquery
{
    public interface IValeurExtendedProperiteFolder
    {
        string GetValue(CEasyQuerySource source);
    }

    [Serializable]
    public class CValeurExtendedProperieteFolderTexte : IValeurExtendedProperiteFolder
    {
        private string m_strValeur = "";
        public CValeurExtendedProperieteFolderTexte(string strValeur)
        {
            m_strValeur = strValeur;
        }

        public string GetValue(CEasyQuerySource source)
        {
            return m_strValeur;
        }

        public override string ToString()
        {
            return m_strValeur;
        }
    }
}
