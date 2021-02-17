using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using sc2i.win32.common.customizableList;

namespace sc2i.win32.common
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class CSlidingZone : UserControl, IControlALockEdition, IControlADonneesSpecifiquesDansCustomList
    {
        private int m_nExtendedSize = 100;


        public event EventHandler OnCollapseChange;

        private bool m_bAutoSize = false;

        private EventHandler m_childSizeChanged;

        

        public CSlidingZone()
        {
            InitializeComponent();
        }

        //----------------------------------
        public int TitleHeight
        {
            get
            {
                return m_titleBar.Height;
            }
            set
            {
                m_titleBar.Height = value;
            }
        }

        //----------------------------------
        public bool AutoSize
        {
            get
            {
                return m_bAutoSize;
            }
            set
            {
                m_bAutoSize = value;
            }
        }

		//----------------------------------
		protected override void SetClientSizeCore(int x, int y)
		{
			base.SetClientSizeCore(x, y);
		}

        //----------------------------------
        public string TitleText
        {
            get
            {
                return m_titleBar.Text;
            }
            set
            {
                m_titleBar.Text = value;
            }
        }

        //----------------------------------
        public Color TitleBackColor
        {
            get
            {
                return m_titleBar.BackColor;
            }
            set
            {
                m_titleBar.BackColor = value;
            }
        }

        //----------------------------------
        public Color TitleBackColorGradient
        {
            get
            {
                return m_titleBar.BackColorGradient;
            }
            set
            {
                m_titleBar.BackColorGradient = value;
            }
        }

        //----------------------------------
        public Font TitleFont
        {
            get
            {
                return m_titleBar.Font;
            }
            set
            {
                m_titleBar.Font = value;
            }
        }

        //----------------------------------
        public ContentAlignment TitleAlignment
        {
            get
            {
                return m_titleBar.TextAlign;
            }
            set
            {
                m_titleBar.TextAlign = value;
            }
        }

		//----------------------------------
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (Size.Height < m_titleBar.Height)
				Size = new Size(Size.Width, m_titleBar.Height);
		}

        public void Collapse()
        {
            if (IsCollapse)
                return;
            if ( Height > m_titleBar.Height )
                m_nExtendedSize = Height;
            Height = m_titleBar.Height;
            m_titleBar.IsCollapse = true;

            if (OnCollapseChange != null)
                OnCollapseChange(this, new EventArgs());
        }

		public int ExtendedSize
		{
			get
			{
				return m_nExtendedSize;
			}
			set
			{
				m_nExtendedSize = value;
			}
		}

        public virtual void Extend()
        {
            if ( IsCollapse )
                Height = m_nExtendedSize;
            m_titleBar.IsCollapse = false;
            RecalcAutoSize();
            if (OnCollapseChange != null)
                OnCollapseChange(this, new EventArgs());

        }

        private void m_titleBar_Click(object sender, EventArgs e)
        {
            if (IsCollapse)
                Extend();
            else
                Collapse();
        }

        public bool IsCollapse
        {
            get
            {
                return Height == m_titleBar.Height;
            }
            set
            {
                if (value)
                    Collapse();
                else
                    Extend();
            }
        }
		#region IControlALockEdition Membres

		public virtual bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				RecursiveLockEditionChilds(value, this);
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}


		//////////////////////////////////////////////////////////////
		private void RecursiveLockEditionChilds(bool b, Control ctrl)
		{
			foreach (Control fils in ctrl.Controls)
			{
				try
				{
					if (fils is IControlALockEdition)
						((IControlALockEdition)fils).LockEdition = b;
					else
						fils.Enabled = !b;
				}
				catch { }
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion

		public void DoOnChangeLockEdition()
		{
			if (OnChangeLockEdition != null)
				OnChangeLockEdition(this, new EventArgs());
		}

        public void OnChildSizeChanged ( object sender, EventArgs args )
        {
            Control ctrl = sender as Control;
            if (ctrl != null && ctrl.Dock != DockStyle.Fill && AutoSize)
            {
                RecalcAutoSizeDelayed();
            }
        }

        private void RecalcAutoSizeDelayed()
        {
            m_timerAutoResize.Stop();
            m_timerAutoResize.Start();
        }

        private void RecalcAutoSize()
        {
            m_timerAutoResize.Stop();
            if ( !AutoSize || IsCollapse)
                return;
            int nHeight =0;
            bool bHasControls = false;
            foreach ( Control ctrl in Controls )
            {
                if (ctrl.Dock == DockStyle.Fill)
                {
                    if (ctrl is Panel && ((Panel)ctrl).AutoSize)
                    {
                        foreach (Control ctrlChild in ctrl.Controls)
                        {
                            nHeight = Math.Max(nHeight, ctrl.Top + ctrlChild.Bottom);
                        }
                    }
                    else
                        return;
                }
                if (ctrl.Visible && ctrl.Dock != DockStyle.Left && ctrl.Dock != DockStyle.Right)
                {
                    bHasControls = true;
                    nHeight = Math.Max(nHeight, ctrl.Bottom);
                }
            }
            if (!bHasControls)
                return;
            Height = nHeight;
        }



        private void CSlidingZone_ControlAdded(object sender, ControlEventArgs e)
        {
            m_titleBar.SendToBack();
            Control ctrl = e.Control;
            if (ctrl != null)
            {
                if (m_childSizeChanged == null)
                    m_childSizeChanged = new EventHandler(OnChildSizeChanged);
                ctrl.SizeChanged += m_childSizeChanged;
            }
        }

        private void CSlidingZone_ControlRemoved(object sender, ControlEventArgs e)
        {
            Control ctrl = e.Control;
            if (ctrl != null && m_childSizeChanged != null)
                ctrl.SizeChanged -= m_childSizeChanged;
        }

        private void m_timerAutoResize_Tick(object sender, EventArgs e)
        {
            RecalcAutoSize();
        }



        #region IControlADonneesSpecifiquesDansCustomList Membres

        public void SaveMyDonneesSpecifiquesCustomListSansMesControlesFils(CDonneesSpecifiquesControleDansCustomList donnees)
        {
            donnees.SetData<bool>("IsCollapse", IsCollapse);
        }

        public void RestoreMyDonneesSpecifiquesCustomListSansMesControlesFils(CDonneesSpecifiquesControleDansCustomList donnees)
        {
            bool? bVal = donnees.GetData<bool?>("IsCollapse", null);
            if (bVal != null)
                IsCollapse = bVal.Value;
        }

        #endregion
    }
}
