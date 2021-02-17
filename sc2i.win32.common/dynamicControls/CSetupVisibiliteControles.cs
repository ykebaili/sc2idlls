using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;

namespace sc2i.win32.common.dynamicControls
{
    [Serializable]
    public class CSetupVisibiliteControles : I2iSerializable
    {
        //----------------------------------------------------------------------
        private HashSet<string> m_setControlesMasques = new HashSet<string>();

        //----------------------------------------------------------------------
        public CSetupVisibiliteControles()
        {
        }

        //----------------------------------------------------------------------
        private string GetControlKey ( Control ctrl, Control ctrlRacine )
        {
            string strKey = "";
            if ( ctrl.Parent != null && ctrl != ctrlRacine)
                strKey = GetControlKey ( ctrl.Parent, ctrlRacine );
            if ( ctrl.Name.Trim().Length > 0 )
            {
                if ( strKey.Length > 0 )
                    strKey+=".";
                strKey += ctrl.Name;
            }
            return strKey;
        }

        //----------------------------------------------------------------------
        public void HideControle(Control ctrl, Control ctrlRacine)
        {
            m_setControlesMasques.Add(GetControlKey ( ctrl, ctrlRacine ));
        }

        //----------------------------------------------------------------------
        public void ShowControle(Control ctrl, Control ctrlRacine)
        {
            m_setControlesMasques.Remove(GetControlKey ( ctrl, ctrlRacine ));
        }

        //----------------------------------------------------------------------
        public bool IsHidden(Control ctrl, Control ctrlRacine)
        {
            return m_setControlesMasques.Contains(GetControlKey ( ctrl, ctrlRacine ));
        }

        //----------------------------------------------------------------------
        public void Apply(Control racine, bool bDesignMode)
        {
            foreach (string strKey in m_setControlesMasques)
            {
                Control ctrl = FindControl(racine, strKey);
                if (ctrl != null)
                {
                    Crownwood.Magic.Controls.TabPage page = ctrl as Crownwood.Magic.Controls.TabPage;
                    if (page != null)
                    {
                        if (!bDesignMode)
                        {
                            Crownwood.Magic.Controls.TabControl parent = ctrl.Parent as Crownwood.Magic.Controls.TabControl;
                            if (parent != null && parent.TabPages.Contains(page))
                                parent.TabPages.Remove(page);
                        }
                        else
                        {
                            page.Icon = Properties.Resources.Delete;
                        }
                    }
                    else
                        ctrl.Visible = false;
                }

            }
        }

        //----------------------------------------------------------------------
        public void Reset()
        {
            m_setControlesMasques.Clear();
        }

        //----------------------------------------------------------------------
        private Control FindControl(Control racine, string strFullPath)
        {
            string strName = strFullPath.Split('.')[0];
            if (racine.Name == strName)
            {
                strFullPath = strFullPath.Remove(0, strName.Length);
                if (strFullPath.StartsWith("."))
                    strFullPath = strFullPath.Remove(0, 1);
                if (strFullPath.Trim().Length == 0)
                    return racine;
                foreach (Control ctrl in racine.Controls)
                {
                    Control ctrlTrouve = FindControl(ctrl, strFullPath);
                    if (ctrlTrouve != null)
                        return ctrlTrouve;
                }
            }
            return null;
        }

        //---------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nNbHide = m_setControlesMasques.Count;
            serializer.TraiteInt(ref nNbHide);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strNom in m_setControlesMasques)
                    {
                        string strTmp = strNom;
                        serializer.TraiteString(ref strTmp);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_setControlesMasques.Clear();
                    for (int n = 0; n < nNbHide; n++)
                    {
                        string strTmp = "";
                        serializer.TraiteString(ref strTmp);
                        m_setControlesMasques.Add(strTmp);
                    }
                    break;
            }
            return result;
        }
    }
}
