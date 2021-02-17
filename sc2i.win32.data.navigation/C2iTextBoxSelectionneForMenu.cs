using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.data.navigation;
using System.Drawing;
using sc2i.data;

namespace sc2i.win32.data.navigation
{
    public class C2iTextBoxSelectionneForMenu : ToolStripControlHost
    {
        private EventHandler m_handlerChangement = null;
        private bool m_bCloseOnSelect = true;

        //---------------------------------------------------------------------
        public C2iTextBoxSelectionneForMenu()
            : base(new C2iTextBoxSelectionne())
        {
            m_handlerChangement = new EventHandler(txt_OnSelectedObjectChanged);

            TextBoxSelection.LockEdition = false;
            TextBoxSelection.Visible = true;
            TextBoxSelection.Size = new Size(250, 20);
            TextBoxSelection.OnSelectedObjectChanged += m_handlerChangement;
        }

        //---------------------------------------------------------------------
        public bool AutoClose
        {
            get
            {
                return m_bCloseOnSelect;
            }
            set
            {
                m_bCloseOnSelect = value;
            }
        }

        //---------------------------------------------------------------------
        public override Size GetPreferredSize(Size constrainingSize)
        {
            return new Size(250, 20);
        }

        //---------------------------------------------------------------------
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            
           //TextBoxSelection.OnSelectedObjectChanged += m_handlerChangement;

        }

        //---------------------------------------------------------------------
        public event ObjetDonneeEventHandler OnSelectObject;

        //---------------------------------------------------------------------
        private void txt_OnSelectedObjectChanged(object sender, EventArgs args)
        {
            if (OnSelectObject != null)
            {
                OnSelectObject(this, new CObjetDonneeEventArgs(TextBoxSelection.SelectedObject));
                if (AutoClose)
                {
                    try
                    {
                        ToolStripItem parent = OwnerItem;
                        while (parent != null && parent.OwnerItem != null)
                        {
                            parent = parent.OwnerItem;
                        }
                        ToolStripDropDownMenu menu = parent != null ?parent.Owner as ToolStripDropDownMenu:null;
                        if ( menu == null )
                            menu = Owner as ToolStripDropDownMenu;
                        if (menu != null)
                            menu.Close();
                    }
                    catch { }
                }
            }
        }

        //---------------------------------------------------------------------
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            //TextBoxSelection.OnSelectedObjectChanged -= m_handlerChangement;
        }

        //---------------------------------------------------------------------
        public C2iTextBoxSelectionne TextBoxSelection
        {
            get
            {
                return (C2iTextBoxSelectionne)Control;
            }
        }
    }
}
