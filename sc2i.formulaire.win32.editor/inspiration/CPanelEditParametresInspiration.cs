using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.win32.common.customizableList;
using sc2i.common;
using sc2i.formulaire.inspiration;

namespace sc2i.formulaire.win32.inspiration
{
    public class CPanelEditParametresInspiration : CCustomizableList
    {
        private sc2i.win32.common.CWndLinkStd m_lnkAjouter;
        private sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
        private System.Windows.Forms.Panel m_panelTop;

        public CPanelEditParametresInspiration()
            : base()
        {
            InitializeComponent();
            if (!DesignMode)
                ItemControl = new CPanelEditParametreInspiration();
            m_panelTop.SendToBack();
        }

        private void InitializeComponent()
        {
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelDessin
            // 
            this.m_panelDessin.Location = new System.Drawing.Point(0, 20);
            this.m_panelDessin.Size = new System.Drawing.Size(450, 235);
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lnkSupprimer);
            this.m_panelTop.Controls.Add(this.m_lnkAjouter);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(450, 20);
            this.m_panelTop.TabIndex = 0;
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(100, 0);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.ShortMode = false;
            this.m_lnkSupprimer.Size = new System.Drawing.Size(100, 20);
            this.m_lnkSupprimer.TabIndex = 1;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.ShortMode = false;
            this.m_lnkAjouter.Size = new System.Drawing.Size(100, 20);
            this.m_lnkAjouter.TabIndex = 0;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // CPanelEditParametresInspiration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.m_panelTop);
            this.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.Name = "CPanelEditParametresInspiration";
            this.Controls.SetChildIndex(this.m_panelTop, 0);
            this.Controls.SetChildIndex(this.m_panelDessin, 0);
            this.m_panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        

        public void Init(CListeParametresInspiration parametres)
        {
            List<CCustomizableListItem> items = new List<CCustomizableListItem>();

            foreach (IParametreInspiration parametre in parametres)
            {
                CParametreInspirationProprieteDeType p = parametre as CParametreInspirationProprieteDeType;
                if (p != null)
                {
                    CCustomizableListItem item = CreateItem(p);
                    items.Add(item);
                }
            }
            Items = items.ToArray();
            Refill();
        }

        public CResultAErreurType<CListeParametresInspiration> MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            CResultAErreurType<CListeParametresInspiration> resRetour = new CResultAErreurType<CListeParametresInspiration>();
            CListeParametresInspiration lst = new CListeParametresInspiration();
            if (CurrentItemIndex != null)
                ItemControl.MajChamps();
            foreach (CCustomizableListItem item in Items)
            {
                CParametreInspirationProprieteDeType parametre = item.Tag as CParametreInspirationProprieteDeType;
                if (parametre != null)
                {
                    result = parametre.VerifieDonnees();
                    if (!result)
                    {
                        resRetour.EmpileErreur(result.Erreur);
                        return resRetour;
                    }
                    lst.Add(parametre);
                }
            }
            resRetour.DataType = lst;
            return resRetour;
        }

        private CCustomizableListItem CreateItem(CParametreInspirationProprieteDeType parametre)
        {
            CCustomizableListItem item = new CCustomizableListItem();
            item.Tag = parametre;
            return item;
        }



        private void m_lnkAjouter_LinkClicked(object sender, EventArgs e)
        {
            CParametreInspirationProprieteDeType parametre = new CParametreInspirationProprieteDeType();
            AddItem(CreateItem(parametre), true);
        }

        private void m_lnkSupprimer_LinkClicked(object sender, EventArgs e)
        {
            if (CurrentItemIndex != null)
                RemoveItem(CurrentItemIndex.Value, true);
        }

        
    }
}
