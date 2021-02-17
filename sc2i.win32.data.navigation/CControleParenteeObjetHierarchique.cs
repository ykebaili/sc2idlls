using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data;
using sc2i.win32.data.navigation;

namespace sc2i.win32.data.navigation
{
    public partial class CControleParenteeObjetHierarchique : UserControl, IControlALockEdition
    {
        private IObjetDonneeAutoReferenceNavigable m_objetHierarchique = null;
        public CControleParenteeObjetHierarchique()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------
        public void Init(IObjetDonneeAutoReferenceNavigable objet)
        {
            m_panelHierarchie.ClearAndDisposeControls();
            m_objetHierarchique = objet;
            IObjetDonneeAutoReferenceNavigable parent = objet != null ? objet.ObjetAutoRefParent as IObjetDonneeAutoReferenceNavigable: null;
            bool bDernier = true;
            m_panelHierarchie.SuspendDrawing();
            m_iconUp.Visible = parent != null;
            while (parent != null)
            {
                if (!bDernier)
                {
                    Label lbl = new Label();
                    lbl.Text = "/";
                    lbl.AutoSize = true;
                    lbl.ForeColor = Color.Blue;
                    m_panelHierarchie.Controls.Add(lbl);
                    lbl.Dock = DockStyle.Left;
                    lbl.SendToBack();
                }
                bDernier = false;
                LinkLabel link = new LinkLabel();
                link.Text = sc2i.common.CUtilTexte.TronqueLeMilieu(parent.Libelle, 32);
                m_toolTip.SetToolTip(link, parent.Libelle);
                link.Dock = DockStyle.Left;
                link.Tag = parent;
                link.AutoSize = true;
                m_panelHierarchie.Controls.Add(link);
                link.SendToBack();
                link.Click += new EventHandler(link_Click);
                parent = parent.ObjetAutoRefParent as IObjetDonneeAutoReferenceNavigable;
            }
            m_iconModifier.Visible = m_objetHierarchique is CObjetHierarchique;
            m_panelHierarchie.ResumeDrawing();
            UpdateVisuLoupe();               
        }

        private void UpdateVisuLoupe()
        {
            int nMaxY = 0;
            foreach (Control ctrl in m_panelHierarchie.Controls)
                if (ctrl.Right > nMaxY)
                    nMaxY = ctrl.Right;
            m_iconZoom.Visible = nMaxY > m_panelHierarchie.ClientSize.Width;
        }

        //----------------------------------------------------------
        void link_Click(object sender, EventArgs e)
        {
            LinkLabel label = sender as LinkLabel;
            if (label != null)
            {
                CObjetHierarchique objet = label.Tag as CObjetHierarchique;
                if (objet != null)
                    CSc2iWin32DataNavigation.Navigateur.EditeElement(objet, false, "");
            }
        }

        #region IControlALockEdition Membres

        //----------------------------------------------------------
        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //-------------------------------------------------------------------
        private void m_iconUp_Click(object sender, EventArgs e)
        {
            if (m_objetHierarchique != null && m_objetHierarchique.ObjetAutoRefParent != null)
                CSc2iWin32DataNavigation.Navigateur.EditeElement(m_objetHierarchique.ObjetAutoRefParent, false, "");
        }

        //-------------------------------------------------------------------
        private void m_iconModifier_Click(object sender, EventArgs e)
        {
            CObjetHierarchique objetH = m_objetHierarchique as CObjetHierarchique;
            if (objetH != null)
            {
                if (CFormReaffecteObjetHierarchique.ReaffecteObjet(objetH))
                {
                    Init(m_objetHierarchique);
                    if (OnChangeObjetParent != null)
                        OnChangeObjetParent(this, new EventArgs());

                }
            }
        }

        //-------------------------------------------------------------------
        public event EventHandler OnChangeObjetParent;

        //-------------------------------------------------------------------
        private void m_iconZoom_Click(object sender, EventArgs e)
        {
            Rectangle rct = m_panelHierarchie.ClientRectangle;
            rct.Location = m_panelHierarchie.PointToScreen(rct.Location);
            rct.Offset ( 0, rct.Height );
            rct.Height = 40;
            rct.Width = Math.Max(rct.Width, 330);
            CFormPopupParenteeObjetHierarchique frm = new CFormPopupParenteeObjetHierarchique();
            frm.Location = rct.Location;
            frm.Size = rct.Size;
            frm.Show(m_objetHierarchique);
        }

        private void m_panelHierarchie_SizeChanged(object sender, EventArgs e)
        {
            UpdateVisuLoupe();
        }
    }
}
