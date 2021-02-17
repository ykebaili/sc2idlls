using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport
{
    public class CValeursImportFixe
    {
        private Dictionary<string, object> m_dicValeursFixes = new Dictionary<string, object>();

        public CValeursImportFixe()
        {
        }

        public void SetValeur ( string strNomChamp, object valeur )
        {
            m_dicValeursFixes[strNomChamp] = valeur;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> ValeursFixes
        {
            get
            {
                return m_dicValeursFixes.ToArray();
            }
        }
    }
}
