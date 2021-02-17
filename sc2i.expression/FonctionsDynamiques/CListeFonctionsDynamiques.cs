using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression.FonctionsDynamiques
{
    public class CListeFonctionsDynamiques : List<CFonctionDynamique>, IElementAFonctionsDynamiques
    {
        //--------------------------------------------------
        public CListeFonctionsDynamiques()
        {
        }

        //--------------------------------------------------
        public IEnumerable<CFonctionDynamique> FonctionsDynamiques
        {
            get { return AsReadOnly(); }
        }

        //--------------------------------------------------
        public CFonctionDynamique GetFonctionDynamique(string strIdFonction)
        {
            return this.FirstOrDefault(f=>f.IdFonction == strIdFonction );
        }
    }
}
