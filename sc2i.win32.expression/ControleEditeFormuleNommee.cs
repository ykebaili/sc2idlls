using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.expression;
using sc2i.win32.common;

namespace sc2i.win32.expression
{
	/// <summary>
    /// Description résumée de CControleEditeFormuleNommee.
	/// </summary>
	public class CControleEditeFormuleNommee : System.Windows.Forms.UserControl, IControlALockEdition
    {
        private bool m_bHideNomFormule = false;
        private Type m_typeFormuleNommee = typeof(CFormuleNommee);
        private CFormuleNommee m_formuleNommee = null;
        private CTextBoxZoomFormule m_txtFormule;
        private sc2i.win32.common.C2iTextBox m_txtLibelle;
        private CExtModeEdition m_gestionnaireModeEdition;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CControleEditeFormuleNommee()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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


        //-----------------------------------------------
        public bool HideNomFormule
        {
            get
            {
                return m_bHideNomFormule;
            }
            set
            {
                m_bHideNomFormule = value;
                m_txtLibelle.Visible = !value;
            }
        }

        //-----------------------------------------------
        public Type TypeFormulesNommees
        {
            get
            {
                return m_typeFormuleNommee;
            }
            set
            {
                m_typeFormuleNommee = value;
            }
        }
		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_txtLibelle = new sc2i.win32.common.C2iTextBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(160, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormule, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(280, 42);
            this.m_txtFormule.TabIndex = 2;
            this.m_txtFormule.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtLibelle.EmptyText = "";
            this.m_txtLibelle.Location = new System.Drawing.Point(0, 0);
            this.m_txtLibelle.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLibelle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(160, 20);
            this.m_txtLibelle.TabIndex = 1;
            // 
            // CControleEditeFormuleNommee
            // 
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_txtLibelle);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditeFormuleNommee";
            this.Size = new System.Drawing.Size(440, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public void Init ( IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse )
		{
			m_txtFormule.Init ( fournisseur, objetAnalyse);
		}

        //-----------------------------------------------
        public CFormuleNommee FormuleNommee
        {
            get
            {
                if (m_formuleNommee == null)
                    m_formuleNommee = (CFormuleNommee)Activator.CreateInstance(m_typeFormuleNommee, new object[]{Libelle, Formule});
                else
                {
                    m_formuleNommee.Libelle = Libelle;
                    m_formuleNommee.Formule = Formule;
                }
                return m_formuleNommee;
            }
            set
            {
                m_formuleNommee = value;
                if (value != null)
                {
                    Libelle = value.Libelle;
                    Formule = value.Formule;
                }
            }
        }

		//-----------------------------------------------
		public C2iExpression Formule
		{
			get
			{
				return m_txtFormule.Formule;
			}
			set
			{
				m_txtFormule.Formule = value;
			}
		}

		//-----------------------------------------------
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return m_txtLibelle.Text;
			}
			set
			{
                m_txtLibelle.Text = value;
			}
		}

		//-----------------------------------------------
		public CResultAErreur ResultAnalyse
		{
			get
			{
                return m_txtFormule.ResultAnalyse;
			}
		}

		//-----------------------------------------------
		public CTextBoxZoomFormule TextFormule
		{
			get
			{
				return m_txtFormule;
			}
		}

        //-----------------------------------------------
        private void m_txtFormule_Enter(object sender, EventArgs e)
        {
            OnEnter(e);
        }


        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
