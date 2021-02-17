using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common.Restrictions;

namespace sc2i.win32.common
{
    public class CGestionnaireReadOnlySysteme : IGestionnaireReadOnlySysteme
    {
        private HashSet<Control> m_setReadOnlyParRestrictions= new HashSet<Control>();

        private Dictionary<Control, bool> m_dicControlToEnableParCode = new Dictionary<Control, bool>();
        private Dictionary<IControlALockEdition, bool> m_dicControlToLockEditionParCode = new Dictionary<IControlALockEdition, bool>();

        private EventHandler m_OnEnabledChanged;
        private EventHandler m_OnLockEditionChanged;

        private List<Control> m_listeAbonnesAuxEvenements = new List<Control>();
        


        //-------------------------------------------------------------------
        public CGestionnaireReadOnlySysteme()
        {
            m_OnEnabledChanged = new EventHandler( OnEnabledChanged );
            m_OnLockEditionChanged = new EventHandler( OnLockEditionChanged );
        }

        //-------------------------------------------------------------------
        private bool m_bIsStoringEnable = false;
        private void OnEnabledChanged ( object sender, EventArgs args )
        {
            if ( m_bIsStoringEnable )
                return;
            m_bIsStoringEnable=  true;
            Control ctrl = sender as Control;
            if ( ctrl != null )
            {
                m_dicControlToEnableParCode[ctrl] = ctrl.Enabled;
                if ( ctrl.Enabled && m_setReadOnlyParRestrictions.Contains ( ctrl ) )
                    ctrl.Enabled = false;
            }
            m_bIsStoringEnable = false;
            
        }

        //-------------------------------------------------------------------
        private bool m_bIsStoringLockEdition = false;
        private void OnLockEditionChanged(object sender, EventArgs args)
        {
            if (m_bIsStoringLockEdition)
                return;
            m_bIsStoringLockEdition = true;
            IControlALockEdition ctrl = sender as IControlALockEdition;
            if (ctrl != null)
            {
                m_dicControlToLockEditionParCode[ctrl] = ctrl.LockEdition;
                if (!ctrl.LockEdition && m_setReadOnlyParRestrictions.Contains((Control)ctrl))
                    ctrl.LockEdition = true;
            }
            m_bIsStoringLockEdition = false;
        }
    


        

        //-------------------------------------------------------------
        private void UnReadonlyControl(Control ctrl)
        {
            if ( ctrl != null )
            {
                ctrl.EnabledChanged -= m_OnEnabledChanged;
                IControlALockEdition lockable = ctrl as IControlALockEdition;
                if (lockable != null)
                {
                    lockable.OnChangeLockEdition -= m_OnLockEditionChanged;
                    bool bLock = false;
                    m_dicControlToLockEditionParCode.TryGetValue(lockable, out bLock);
                    m_bIsStoringLockEdition = true;
                    lockable.LockEdition = bLock;
                    m_bIsStoringLockEdition = false;
                }
                else
                {
                    bool bEnabled = true;
                    m_dicControlToEnableParCode.TryGetValue(ctrl, out bEnabled);
                    m_bIsStoringEnable = true;
                    ctrl.Enabled = bEnabled;
                    m_bIsStoringEnable = false;
                }

            }
            if ( ctrl != null )
                m_setReadOnlyParRestrictions.Remove ( ctrl );
        }

        //-------------------------------------------------------------------
        private void ReadOnlyControl(Control ctrl)
        {
            if (ctrl != null && !ctrl.IsDisposed)
            {
                ctrl.EnabledChanged -= m_OnEnabledChanged;
                ctrl.EnabledChanged += m_OnEnabledChanged;
                m_listeAbonnesAuxEvenements.Add(ctrl);
                IControlALockEdition lockable = ctrl as IControlALockEdition;
                if (lockable != null)
                {
                    lockable.OnChangeLockEdition -= m_OnLockEditionChanged;
                    lockable.OnChangeLockEdition += m_OnLockEditionChanged;
                    m_bIsStoringLockEdition = true;
                    lockable.LockEdition = true;
                    m_bIsStoringLockEdition = false;
                }
                else
                {
                    m_bIsStoringEnable = true;
                    ctrl.Enabled = false;
                    m_bIsStoringEnable = false;
                }
            }
            m_setReadOnlyParRestrictions.Add(ctrl);
        }

        //-------------------------------------------------------------------
        private void PrivateStockeEtatDemandé(Control ctrl)
        {
            m_dicControlToEnableParCode[ctrl] = ctrl.Enabled;
            IControlALockEdition lockable = ctrl as IControlALockEdition;
            if (lockable != null)
                m_dicControlToLockEditionParCode[lockable] = lockable.LockEdition;
            foreach (Control child in ctrl.Controls)
                PrivateStockeEtatDemandé(child);
        }

        //-------------------------------------------------------------------
        public void SetReadOnly ( object ctrlObject, bool bReadOnly )
        {
            Control ctrl = ctrlObject as Control;
            if (ctrl != null)
            {
                UnReadonlyControl(ctrl);
                if (bReadOnly)
                    ReadOnlyControl(ctrl);
            }
        }

        //-------------------------------------------------------------------
        public void Init(object racineObject)
        {
            Control racine = racineObject as Control;
            if (racineObject != null)
            {
                m_dicControlToEnableParCode.Clear();
                m_dicControlToLockEditionParCode.Clear();
                AddControl(racine);
                PrivateStockeEtatDemandé(racine);
            }
        }

        //-------------------------------------------------------------------
        public void AddControl(object ctrlObject)
        {
            Control ctrl = ctrlObject as Control;
            if ( ctrl != null )
                PrivateStockeEtatDemandé(ctrl);
        }
        
        //-------------------------------------------------------------------
        public void Clean()
        {
            foreach (Control ctrl in m_listeAbonnesAuxEvenements)
            {
                try
                {
                    ctrl.EnabledChanged -= m_OnEnabledChanged;
                    IControlALockEdition lockable = ctrl as IControlALockEdition;
                    if (lockable != null)
                        lockable.OnChangeLockEdition -= m_OnLockEditionChanged;
                }
                catch { }
            }
            m_listeAbonnesAuxEvenements.Clear();
            m_dicControlToEnableParCode.Clear();
            m_dicControlToLockEditionParCode.Clear();
            m_setReadOnlyParRestrictions.Clear();
        }

        
    }
}
