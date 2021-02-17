using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CComboBoxArbreObjetDonneesHierarchique.
	/// </summary>
	public class CComboBoxArbreObjetDonneesHierarchique : System.Windows.Forms.UserControl, IControlALockEdition, ISelectionneurElementListeObjetsDonnees
	{
		public bool m_bIsLock = false;
        private C2iExpression m_formuleLibelle = null;
		
		public CObjetDonnee m_elementSelectionne = null;
		public Type m_typeObjets;
		public string m_strProprieteListeFils = "";
		public string m_strChampParent = "";
        private string m_strDefProprieteAffichee = "";
		private string m_strProprieteAffichee = "";
		private CFiltreData m_filtre = null;

		private CFiltreData m_filtreRacine = null;

		public bool m_bNullAutorise = false;
        private bool m_bAutoriserFilsDeAutorises = true;
		public string m_strTextNull = I.T("None|19");

		public event EventHandler ElementSelectionneChanged;

		private System.Windows.Forms.Label m_label;
		private System.Windows.Forms.Button m_boutonDropList;
		private System.Windows.Forms.LinkLabel m_linkLabel;
		private System.Windows.Forms.Panel panel1;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CComboBoxArbreObjetDonneesHierarchique()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			/*m_arbre = new CArbreObjetsDonneesHierarchiques();
			m_arbre.Site*/
			// TODO : ajoutez les initialisations après l'appel à InitForm

		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CComboBoxArbreObjetDonneesHierarchique));
            this.m_label = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_linkLabel = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_label
            // 
            this.m_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_label.BackColor = System.Drawing.Color.White;
            this.m_label.Location = new System.Drawing.Point(0, 0);
            this.m_label.Name = "m_label";
            this.m_label.Size = new System.Drawing.Size(361, 17);
            this.m_label.TabIndex = 0;
            this.m_label.Text = "Label|50";
            this.m_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_label.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_boutonDropList
            // 
            this.m_boutonDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonDropList.Image")));
            this.m_boutonDropList.Location = new System.Drawing.Point(359, 0);
            this.m_boutonDropList.Name = "m_boutonDropList";
            this.m_boutonDropList.Size = new System.Drawing.Size(17, 17);
            this.m_boutonDropList.TabIndex = 1;
            this.m_boutonDropList.UseVisualStyleBackColor = false;
            this.m_boutonDropList.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_linkLabel
            // 
            this.m_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_linkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_linkLabel.Location = new System.Drawing.Point(0, 0);
            this.m_linkLabel.Name = "m_linkLabel";
            this.m_linkLabel.Size = new System.Drawing.Size(380, 21);
            this.m_linkLabel.TabIndex = 2;
            this.m_linkLabel.TabStop = true;
            this.m_linkLabel.Text = "linkLabel1";
            this.m_linkLabel.Visible = false;
            this.m_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_linkLabel_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_boutonDropList);
            this.panel1.Controls.Add(this.m_label);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 21);
            this.panel1.TabIndex = 3;
            // 
            // CComboBoxArbreObjetDonneesHierarchique
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_linkLabel);
            this.Name = "CComboBoxArbreObjetDonneesHierarchique";
            this.Size = new System.Drawing.Size(380, 21);
            this.SizeChanged += new System.EventHandler(this.CComboBoxArbreObjetDonneesHierarchique_SizeChanged_1);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public CResultAErreur Init(
			Type typeObjets,
			string strProprieteListeFils,
			string strChampParent,
			string strProprieteAffichee,
			CFiltreData filtre,
			CFiltreData filtreRacine )
		{
			m_typeObjets = typeObjets;
			m_strProprieteListeFils = strProprieteListeFils;
			m_strChampParent = strChampParent;
            m_strDefProprieteAffichee = strProprieteAffichee;
            string[] strProp = strProprieteAffichee.Split('|');
			m_strProprieteAffichee = strProp[0];
            if (strProp.Length > 1 && CFormulesGlobaleParametrage.GetDefinition(strProp[1]) != null)
                m_formuleLibelle = CFormulesGlobaleParametrage.GetFormule(CSc2iWin32DataClient.ContexteCourant.IdSession, strProp[1]);            
			
			m_filtre = filtre;
			m_filtreRacine = filtreRacine;
			//Force la réinitialisation des controles
			LockEdition = m_bIsLock;
			return CResultAErreur.True;
		}



		/// /////////////////////////////////////////////
		private void CComboBoxArbreObjetDonneesHierarchique_SizeChanged(object sender, System.EventArgs e)
		{
			Height = 21;
		}

		/// /////////////////////////////////////////////
		private void m_boutonDropList_Click(object sender, System.EventArgs e)
		{
			if (LockEdition)
				return;
			Rectangle rect = this.RectangleToScreen(new Rectangle ( 0, Height, Width, 230));
			bool bCancel = true;
			CObjetDonnee obj = CFormArbreObjetDonneeHierarchiquePopup.SelectObject(
				rect,
				m_typeObjets,
				m_strProprieteListeFils,
				m_strChampParent,
				m_strDefProprieteAffichee,
				m_elementSelectionne,
				m_filtre,
				m_filtreRacine,
                m_strTextNull,
                m_bAutoriserFilsDeAutorises,
				ref bCancel );
			if ( !bCancel )
				ElementSelectionne = obj;
		}

		/// /////////////////////////////////////////////
		private void m_linkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			OnClickLink();
			return;
		}

		/// /////////////////////////////////////////////
		protected virtual void OnClickLink()
		{
		}
			

		private void CComboBoxArbreObjetDonneesHierarchique_SizeChanged_1(object sender, System.EventArgs e)
		{
			Height = 21;
		}

		/// /////////////////////////////////////////////
		public CObjetDonnee ElementSelectionne
		{
			get
			{
				return m_elementSelectionne;
			}
			set
			{
				bool bHasChange = value != m_elementSelectionne;
				m_elementSelectionne = value;
				if ( bHasChange && ElementSelectionneChanged != null )
					ElementSelectionneChanged(this, new EventArgs());
				if (m_elementSelectionne!=null)
				{
                    string strText = "";
                    if (m_formuleLibelle != null)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_elementSelectionne);
                        CResultAErreur result = m_formuleLibelle.Eval(ctx);
                        if (result && result.Data != null)
                            strText = result.Data.ToString();
                    }
                    if ( strText.Length == 0 )
                        strText = CInterpreteurTextePropriete.GetStringValue(m_elementSelectionne, m_strProprieteAffichee, "");
					m_label.Text = strText;
					m_linkLabel.Text = m_label.Text;
				}
				else
				{
					m_label.Text = TextNull;
					m_linkLabel.Text = TextNull;
				}
			}
		}

		public bool NullAutorise
		{
			get
			{
				return m_bNullAutorise;
			}
			set
			{
				m_bNullAutorise = value;
			}
		}

        /// /////////////////////////////////////////////
        public bool AutoriserFilsDeAutorises
        {
            get
            {
                return m_bAutoriserFilsDeAutorises;
            }
            set
            {
                m_bAutoriserFilsDeAutorises = value;
            }
        }

		public string TextNull
		{
			get
			{
				return m_strTextNull;
			}
			set
			{
				m_strTextNull = value;
			}
		}

		/// /////////////////////////////////////////////
		protected virtual bool IsLink
		{
			get
			{
				return false;
			}
		}

		/// /////////////////////////////////////////////
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return m_bIsLock;
			}
			set
			{
				m_bIsLock = value;
				if ( m_bIsLock )
				{
					if ( IsLink )
					{
						m_linkLabel.Enabled = true;
						m_linkLabel.Visible = true;
						panel1.Visible = false;
					}
					else
						m_label.Width = panel1.ClientSize.Width;
					m_boutonDropList.Visible = false;
				}
				else
				{
					panel1.Visible = true;
					m_label.Width = panel1.ClientSize.Width-m_boutonDropList.Width;
					m_linkLabel.Visible = false;
					m_label.Visible = true;
					m_boutonDropList.Visible = true;
				}
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition(this, new EventArgs());
			}
		}


		#region ISelectionneurElementListeObjetsDonnees Membres
		public bool IsUpdating()
		{
			return false;
		}

		#endregion

        public void SelectAll()
        {
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return !dataGridViewWantsInputKey;
        }

	}
}
