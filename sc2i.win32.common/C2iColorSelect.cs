using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public partial class C2iColorSelect : Label, IControlALockEdition
    {
        private bool m_bLockEdition = false;
        public C2iColorSelect()
        {
            InitializeComponent();
            Cursor = Cursors.Hand;
        }

        public Color SelectedColor
        {
            get
            {
                return BackColor;
            }
            set
            {
                BackColor = value;
                Refresh();
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return "";
            }
            set
            {
                base.Text = "";
            }
        }

        public event EventHandler OnChangeSelectedColor;
        private void C2iColorSelect_Click(object sender, EventArgs e)
        {
            if (m_bLockEdition)
                return;
            ColorDialog dlg = new ColorDialog();
            dlg.Color = SelectedColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = dlg.Color;
                if (OnChangeSelectedColor != null)
                    OnChangeSelectedColor(this, new EventArgs());
            }
        }




        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                if (m_bLockEdition)
                    Cursor = Cursors.Default;
                else
                    Cursor = Cursors.Hand;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}

