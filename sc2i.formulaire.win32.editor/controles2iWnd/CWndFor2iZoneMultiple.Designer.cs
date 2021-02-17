namespace sc2i.formulaire.win32.controles2iWnd
{
	partial class CWndFor2iZoneMultiple
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_panelNavigation = new System.Windows.Forms.Panel();
            this.m_lblPageSurNbPages = new System.Windows.Forms.Label();
            this.m_lnkPremierePage = new System.Windows.Forms.LinkLabel();
            this.m_lnkDernierePage = new System.Windows.Forms.LinkLabel();
            this.m_lnkPrecedent = new System.Windows.Forms.LinkLabel();
            this.m_lnkSuivant = new System.Windows.Forms.LinkLabel();
            this.m_lnkAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelSousFormulaires = new sc2i.win32.common.C2iPanel(this.components);
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_timerRecalcSize = new System.Windows.Forms.Timer(this.components);
            this.m_panelTop.SuspendLayout();
            this.m_panelNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_panelNavigation);
            this.m_panelTop.Controls.Add(this.m_lnkAdd);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTop, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(320, 26);
            this.m_panelTop.TabIndex = 2;
            // 
            // m_panelNavigation
            // 
            this.m_panelNavigation.Controls.Add(this.m_lblPageSurNbPages);
            this.m_panelNavigation.Controls.Add(this.m_lnkPremierePage);
            this.m_panelNavigation.Controls.Add(this.m_lnkDernierePage);
            this.m_panelNavigation.Controls.Add(this.m_lnkPrecedent);
            this.m_panelNavigation.Controls.Add(this.m_lnkSuivant);
            this.m_panelNavigation.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelNavigation.Location = new System.Drawing.Point(160, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNavigation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNavigation.Name = "m_panelNavigation";
            this.m_panelNavigation.Size = new System.Drawing.Size(160, 26);
            this.m_panelNavigation.TabIndex = 2;
            // 
            // m_lblPageSurNbPages
            // 
            this.m_lblPageSurNbPages.AutoSize = true;
            this.m_lblPageSurNbPages.Location = new System.Drawing.Point(53, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblPageSurNbPages, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblPageSurNbPages.Name = "m_lblPageSurNbPages";
            this.m_lblPageSurNbPages.Size = new System.Drawing.Size(35, 13);
            this.m_lblPageSurNbPages.TabIndex = 2;
            this.m_lblPageSurNbPages.Text = "label1";
            // 
            // m_lnkPremierePage
            // 
            this.m_lnkPremierePage.AutoSize = true;
            this.m_lnkPremierePage.Location = new System.Drawing.Point(7, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkPremierePage, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkPremierePage.Name = "m_lnkPremierePage";
            this.m_lnkPremierePage.Size = new System.Drawing.Size(19, 13);
            this.m_lnkPremierePage.TabIndex = 1;
            this.m_lnkPremierePage.TabStop = true;
            this.m_lnkPremierePage.Text = "<<";
            this.m_lnkPremierePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkPremierePage_LinkClicked);
            // 
            // m_lnkDernierePage
            // 
            this.m_lnkDernierePage.AutoSize = true;
            this.m_lnkDernierePage.Location = new System.Drawing.Point(136, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkDernierePage, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkDernierePage.Name = "m_lnkDernierePage";
            this.m_lnkDernierePage.Size = new System.Drawing.Size(19, 13);
            this.m_lnkDernierePage.TabIndex = 1;
            this.m_lnkDernierePage.TabStop = true;
            this.m_lnkDernierePage.Text = ">>";
            this.m_lnkDernierePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkDernierePage_LinkClicked);
            // 
            // m_lnkPrecedent
            // 
            this.m_lnkPrecedent.AutoSize = true;
            this.m_lnkPrecedent.Location = new System.Drawing.Point(34, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkPrecedent, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkPrecedent.Name = "m_lnkPrecedent";
            this.m_lnkPrecedent.Size = new System.Drawing.Size(13, 13);
            this.m_lnkPrecedent.TabIndex = 1;
            this.m_lnkPrecedent.TabStop = true;
            this.m_lnkPrecedent.Text = "<";
            this.m_lnkPrecedent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkPrecedent_LinkClicked);
            // 
            // m_lnkSuivant
            // 
            this.m_lnkSuivant.AutoSize = true;
            this.m_lnkSuivant.Location = new System.Drawing.Point(117, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSuivant, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkSuivant.Name = "m_lnkSuivant";
            this.m_lnkSuivant.Size = new System.Drawing.Size(13, 13);
            this.m_lnkSuivant.TabIndex = 1;
            this.m_lnkSuivant.TabStop = true;
            this.m_lnkSuivant.Text = ">";
            this.m_lnkSuivant.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkSuivant_LinkClicked);
            // 
            // m_lnkAdd
            // 
            this.m_lnkAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAdd.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAdd, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAdd.Name = "m_lnkAdd";
            this.m_lnkAdd.Size = new System.Drawing.Size(101, 26);
            this.m_lnkAdd.TabIndex = 0;
            this.m_lnkAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAdd.LinkClicked += new System.EventHandler(this.m_lnkAdd_LinkClicked);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // m_panelSousFormulaires
            // 
            this.m_panelSousFormulaires.AutoScroll = true;
            this.m_panelSousFormulaires.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelSousFormulaires.Location = new System.Drawing.Point(0, 26);
            this.m_panelSousFormulaires.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelSousFormulaires, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelSousFormulaires.Name = "m_panelSousFormulaires";
            this.m_panelSousFormulaires.Size = new System.Drawing.Size(320, 134);
            this.m_panelSousFormulaires.TabIndex = 3;
            // 
            // m_timerRecalcSize
            // 
            this.m_timerRecalcSize.Tick += new System.EventHandler(this.m_timerRecalcSize_Tick);
            // 
            // CWndFor2iZoneMultiple
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.Controls.Add(this.m_panelSousFormulaires);
            this.Controls.Add(this.m_panelTop);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CWndFor2iZoneMultiple";
            this.Size = new System.Drawing.Size(320, 160);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelNavigation.ResumeLayout(false);
            this.m_panelNavigation.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;

		private IControleWndFor2iWnd m_ctrlWndParent = null;
        private sc2i.win32.common.CWndLinkStd m_lnkAdd;
        private sc2i.win32.common.C2iPanel m_panelSousFormulaires;
        private System.Windows.Forms.LinkLabel m_lnkDernierePage;
        private System.Windows.Forms.LinkLabel m_lnkPremierePage;
        private System.Windows.Forms.LinkLabel m_lnkPrecedent;
        private System.Windows.Forms.LinkLabel m_lnkSuivant;
        private System.Windows.Forms.Panel m_panelNavigation;
        private System.Windows.Forms.Label m_lblPageSurNbPages;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;

        public IWndAContainer WndContainer
		{
			get
			{
				return m_ctrlWndParent;
			}
			set
			{
				m_ctrlWndParent = value as IControleWndFor2iWnd;
			}
		}

        //------------------------------------------------
        public bool IsRacineForEvenements
        {
            get
            {
                return false;
            }
        }

        public IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees()
        {
            return new CEncaspuleurControleWndForFormules(this);
        }

        private System.Windows.Forms.Timer m_timerRecalcSize;

        
	}
}
