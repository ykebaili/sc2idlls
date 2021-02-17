using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace sc2i.win32.data.navigation.filtre
{
    public class CFiltreIdToolStrip : ToolStripControlHost
    {

        public CFiltreIdToolStrip()
            : base(new CFiltreIdToolStripControl())
        {
            Control.Size = new Size(158, 30);
            Size = Control.Size;
            ((CFiltreIdToolStripControl)Control).OnValideSaisie += new EventHandler(CToolStripTextBox_OnValideSaisie);
        }


        public delegate void OnAskIdEventHandler ( object sender, int? nIdDemande );

        public event OnAskIdEventHandler OnAskId;
        void  CToolStripTextBox_OnValideSaisie(object sender, EventArgs e)
        {
 	        if ( OnAskId != null )
                OnAskId ( this, ((CFiltreIdToolStripControl)Control).IdDemande );
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            return new Size(158, 20);
        }

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(158, 20);
            }
        }

        /*public string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = Text;
            }
        }*/


        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            DateTimePicker pc = control as DateTimePicker;
            if ( pc != null )
                pc.ValueChanged += new EventHandler(pc_ValueChanged);

        }

        void pc_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            /*CDateTimePickerForContextMenu dtPicker = (CDateTimePickerForContextMenu)control;
            dtPicker.OnValideDates -= new EventHandler(dtPicker_OnValueChange);*/

        }

        public event EventHandler OnValideDates;

        void dtPicker_OnValueChange(object sender, EventArgs e)
        {
            if (OnValideDates != null)
                OnValideDates(this, new EventArgs());
        }

    }
}
