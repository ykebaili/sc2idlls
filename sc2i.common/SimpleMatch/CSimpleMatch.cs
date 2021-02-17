using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.SimpleMatch
{
    public interface ISimpleMatch
    {
        bool Match(string strChaine);
        string GetString();

        bool IsValide();
    }


    /// <summary>
    /// Permet de vérifier qu'une chaine correspond à un format donné
    /// </summary>
    public class CSimpleMatch
    {
        private List<ISimpleMatch> m_listeOr = new List<ISimpleMatch>();

        //------------------------------------------
        public CSimpleMatch()
        {

        }

        //------------------------------------------
        public static CSimpleMatch FromString ( string strFormat )
        {
            CSimpleMatch match = new CSimpleMatch();
            string[] strParts = strFormat.Split(';');
            foreach ( string strPart in strParts )
            {
                ISimpleMatch part = null;
                if ( strPart.Contains('%') || strPart.Contains('?') )
                {
                    part = new CSimpleMatchEqualsAvecJoker ( strPart );
                }
                else if ( strPart.Contains('-') )
                {
                    string[] strMinMax = strPart.Split('-');
                    part = new CSimpleMatchPlageMinMax(strMinMax[0], strMinMax[1] );
                }
                else
                    part = new CSimpleMatchEquals ( strPart );
                match.m_listeOr.Add ( part );
            }
            return match;
        }

        //------------------------------------------
        public string GetString()
        {
            StringBuilder bl = new StringBuilder();
            foreach (ISimpleMatch match in m_listeOr)
            {
                bl.Append(match.GetString());
                bl.Append(';');
            }
            if (bl.Length > 0)
                bl.Remove(bl.Length - 1, 1);
            return bl.ToString();
        }

        //------------------------------------------
        public bool Match(string strChaine)
        {
            foreach (ISimpleMatch match in m_listeOr)
            {
                if (match.Match(strChaine))
                    return true;
            }
            return false;
        }



    }
}
