using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.common
{

    /// <summary>
    /// sauvegarde l'état des tab control d'une fenêtre
    /// </summary>
    public class CEtatTabControls
    {
        //---------------------------------------------------------------------------------------------------------------------
        protected static void FillListeTabControls ( Control control, List<Crownwood.Magic.Controls.TabControl> listeRetour)
        {
            if ( control == null )
                return;
            if ( control is Crownwood.Magic.Controls.TabControl )
                listeRetour.Add ( (Crownwood.Magic.Controls.TabControl)control );
            else
                foreach ( Control child in control.Controls )
                {
                    FillListeTabControls ( child, listeRetour );
                }
        }

        //---------------------------------------------------------------------------------------------------------------------
        public static List<CTabControlState> GetTabsControlsStates ( Control controleRacine )
        {
            List<Crownwood.Magic.Controls.TabControl> lstTabs = new List<Crownwood.Magic.Controls.TabControl>();
            FillListeTabControls(controleRacine, lstTabs);

            List<CTabControlState> lstStates = new List<CTabControlState>();
            foreach ( Crownwood.Magic.Controls.TabControl tabControl in lstTabs)
            {
                CTabControlState childState = new CTabControlState();
                childState.NomTabControl = tabControl.Name;
                if (tabControl.SelectedTab != null)
                {
                    childState.NomActivePage = tabControl.SelectedTab.Name;
                    lstStates.Add(childState);
                    IEnumerable<CTabControlState> childStates = GetTabsControlsStates(tabControl.SelectedTab);
                    childState.ChildTabStates = childStates;
                }                
            }
            return lstStates;
        }

        //---------------------------------------------------------------------------------------------------------------------
        public static void ApplyTabsControlsStates ( Control controleRacine, IEnumerable<CTabControlState> states )
        {
            List<Crownwood.Magic.Controls.TabControl> lstTabs = new List<Crownwood.Magic.Controls.TabControl>();
            FillListeTabControls(controleRacine, lstTabs);

            foreach (CTabControlState state in states)
            {
                Crownwood.Magic.Controls.TabControl ctrl = lstTabs.FirstOrDefault(c => c.Name == state.NomTabControl);
                if (ctrl != null)
                {
                    int nIndex = 0;
                    foreach (Crownwood.Magic.Controls.TabPage page in new System.Collections.ArrayList(ctrl.TabPages))
                    {
                        if (page.Name == state.NomActivePage)
                        {
                            try
                            {
                                ctrl.SelectedIndex = nIndex;
                                //ctrl.SelectedTab = page;
                                //ctrl.Update();
                                ApplyTabsControlsStates(page, state.ChildTabStates);
                            }
                            catch
                            {}
                        }
                        nIndex++;
                    }
                }
            }
        }

    }

    public class CTabControlState : I2iSerializable
    {
        private string m_strNomTabControl = "";
        private string m_strNomTabPage = "";

        List<CTabControlState> m_childTabsStates = new List<CTabControlState>();


        //---------------------------------------------
        public string NomTabControl
        {
            get
            {
                return m_strNomTabControl;
            }
            set
            {
                m_strNomTabControl = value;
            }
        }

        //---------------------------------------------
        public string NomActivePage
        {
            get
            {
                return m_strNomTabPage;
            }
            set
            {
                m_strNomTabPage = value;
            }
        }

        //---------------------------------------------
        public IEnumerable<CTabControlState> ChildTabStates
        {
            get
            {
                return m_childTabsStates.AsReadOnly();
            }
            set
            {
                m_childTabsStates.Clear();
                if (value != null)
                    m_childTabsStates.AddRange(value);
            }
        }

        //---------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomTabControl);
            serializer.TraiteString(ref m_strNomTabPage);
            result = serializer.TraiteListe<CTabControlState>(m_childTabsStates);
            return result; 
        }
    }
}
