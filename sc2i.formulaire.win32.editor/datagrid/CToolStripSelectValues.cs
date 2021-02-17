using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.formulaire.win32.datagrid
{
    public class CToolStripSelectValues : ToolStripControlHost
    {

        public CToolStripSelectValues() 
            : base (new CGridValueSelector())
        {
            sc2i.win32.common.CWin32Traducteur.Translate(Control);
        }

        public CGridValueSelector Selector
        {
            get
            {
                return Control as CGridValueSelector;
            }
        }

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(181, 190);
            }
        }

        


        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            CGridValueSelector selector = (CGridValueSelector)control;
            selector.OnOkClicked += new EventHandler(selector_OnOkClicked);
        }

        void selector_OnOkClicked(object sender, EventArgs e)
        {
            Control strip = Parent;
            while (strip != null && strip.Parent != null)
                strip = strip.Parent;
            if (strip != null)
                strip.Hide();
            if (OnOkClicked != null)
                OnOkClicked(this, null);
        }


        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            CGridValueSelector selector = (CGridValueSelector)control;
            selector.OnOkClicked += new EventHandler(selector_OnOkClicked);

        }

        public event EventHandler OnOkClicked;


    }
}
