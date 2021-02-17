using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.recherche;
using sc2i.common;

namespace sc2i.formulaire
{
    [Serializable]
    public class CNoeudRechercheObjet_Wnd : INoeudCheminResultatRechercheObjet
    {
        private C2iWnd m_wnd;
        private string m_strLibelle = "";

        public CNoeudRechercheObjet_Wnd(C2iWnd wnd)
        {
            m_wnd = wnd;
            string strType = "";
            object[] att = wnd.GetType().GetCustomAttributes(typeof(WndNameAttribute), true);
            if (att != null && att.Length > 0)
                strType = ((WndNameAttribute)att[0]).Name;
            m_strLibelle = I.T("Item @1|20013",strType+" "+wnd.Name);
        }

        //---------------------------------------
        public string LibelleNoeudCheminResultatRechercheObjet
        {
            get
            {
                return m_strLibelle;
            }
        }

        //---------------------------------------
        public C2iWnd Wnd
        {
            get
            {
                return m_wnd;
            }
        }


    }
}
