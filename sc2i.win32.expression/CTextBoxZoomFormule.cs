using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Description résumée de CTextBoxZoomFormule.
	/// </summary>
	public class CTextBoxZoomFormule : System.Windows.Forms.UserControl, IControlALockEdition
	{
        private C2iExpressionGraphique m_expressionGraphique = null;
		private IFournisseurProprietesDynamiques m_fournisseur = null;
		private CObjetPourSousProprietes m_objetAnalyse = null;
        private bool m_bAllowNull = false;
		private bool m_bLockZoneText = false;
		private System.Windows.Forms.Button m_btnZoom;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
        private bool m_bAutoriserGraphique = true;
        private bool m_bAutoriserSaisieTexte = true;
        private Button m_btnGraphic;
        private ToolTip m_tooltip;
        private IContainer components;

		public CTextBoxZoomFormule()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
            m_txtFormule.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);

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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_btnZoom = new System.Windows.Forms.Button();
            this.m_btnGraphic = new System.Windows.Forms.Button();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // m_btnZoom
            // 
            this.m_btnZoom.AutoSize = true;
            this.m_btnZoom.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnZoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnZoom.Location = new System.Drawing.Point(132, 0);
            this.m_btnZoom.Name = "m_btnZoom";
            this.m_btnZoom.Size = new System.Drawing.Size(26, 20);
            this.m_btnZoom.TabIndex = 1;
            this.m_btnZoom.Text = "...";
            this.m_btnZoom.UseVisualStyleBackColor = false;
            this.m_btnZoom.Click += new System.EventHandler(this.m_btnZoom_Click);
            this.m_btnZoom.Enter += new System.EventHandler(this.ChildControl_Enter);
            // 
            // m_btnGraphic
            // 
            this.m_btnGraphic.AutoSize = true;
            this.m_btnGraphic.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnGraphic.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnGraphic.Image = global::sc2i.win32.expression.Resource1.binary_tree;
            this.m_btnGraphic.Location = new System.Drawing.Point(158, 0);
            this.m_btnGraphic.Name = "m_btnGraphic";
            this.m_btnGraphic.Size = new System.Drawing.Size(26, 20);
            this.m_btnGraphic.TabIndex = 2;
            this.m_btnGraphic.UseVisualStyleBackColor = false;
            this.m_btnGraphic.Click += new System.EventHandler(this.m_btnGraphic_Click);
            this.m_btnGraphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_btnGraphic_MouseUp);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(132, 20);
            this.m_txtFormule.TabIndex = 0;
            this.m_txtFormule.Enter += new System.EventHandler(this.ChildControl_Enter);
            // 
            // CTextBoxZoomFormule
            // 
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_btnZoom);
            this.Controls.Add(this.m_btnGraphic);
            this.Name = "CTextBoxZoomFormule";
            this.Size = new System.Drawing.Size(184, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public bool AllowNullFormula
        {
            get
            {
                return m_bAllowNull;
            }
            set
            {
                m_bAllowNull = value;
            }
        }

        public event EventHandler OnChangeTexteFormule;
        void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (OnChangeTexteFormule != null)
                OnChangeTexteFormule(this, new EventArgs());
        }


		public bool LockZoneTexte
		{
			get
			{
				return m_bLockZoneText;
			}
			set
			{
				m_bLockZoneText = value;
				if (value)
					m_txtFormule.LockEdition = value;
			}
		}

		public void Init(IFournisseurProprietesDynamiques fournisseur, Type type)
		{
			Init ( fournisseur, new CObjetPourSousProprietes (type));
		}

		public void Init(IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse)
		{
			m_fournisseur = fournisseur;
			m_objetAnalyse = objetAnalyse;
			m_txtFormule.Init(fournisseur, objetAnalyse);
		}

		private void m_btnZoom_Click(object sender, System.EventArgs e)
		{
			C2iExpression formule = CFormStdEditeFormule.EditeFormule ( 
				m_txtFormule.Text, m_fournisseur, m_objetAnalyse );
			if ( formule != null && !LockEdition)
			{
				m_txtFormule.Formule = formule;
			}
		}

		public C2iExpression Formule
		{
			get
			{
                if ( m_expressionGraphique != null )
                    return m_expressionGraphique;
				return m_txtFormule.Formule;
			}
			set
			{
                m_expressionGraphique = value as C2iExpressionGraphique;
                if (m_expressionGraphique != null)
                    m_txtFormule.Formule = m_expressionGraphique.FormuleFinale;
                else
				    m_txtFormule.Formule = value;
                m_btnZoom.Visible = m_expressionGraphique == null && AllowSaisieTexte;
                m_txtFormule.TextBox.ReadOnly = m_expressionGraphique != null && AllowSaisieTexte;
                if ( m_expressionGraphique != null )
                {
                    m_tooltip.SetToolTip ( m_txtFormule, 
                        I.T("You can't directly edit this formula because it's a graphical formula. Right click on Graphic icon to allow edition|20009"));
                }
                else
                    m_tooltip.SetToolTip(m_txtFormule,"");
			}
		}
		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return m_txtFormule.LockEdition;
			}
			set
			{
				m_txtFormule.LockEdition = value || LockZoneTexte;
                m_btnZoom.Enabled = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs()) ;
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

       

        private void ChildControl_Enter(object sender, EventArgs e)
        {
            OnEnter(e);
            
        }

        //-----------------------------------------------
        public CResultAErreur ResultAnalyse
        {
            get
            {
                if (m_txtFormule.Text.Trim().Length == 0 && AllowNullFormula)
                    return CResultAErreur.True;
                return m_txtFormule.ResultAnalyse;
            }
        }

        //-----------------------------------------------
        internal void InsertInTextBox(string str)
        {
            int nPositionApres = m_txtFormule.TextBox.SelectionStart + str.Length;
            m_txtFormule.TextBox.SelectedText = str;
            m_txtFormule.TextBox.SelectionStart = nPositionApres;
            m_txtFormule.TextBox.SelectionLength = 0;

        }

        //-----------------------------------------------
        public bool AllowGraphic
        {
            get
            {
                return m_bAutoriserGraphique;
            }
            set
            {
                m_bAutoriserGraphique = value;
                m_btnGraphic.Visible = m_bAutoriserGraphique;
            }
        }

        //-----------------------------------------------
        public bool AllowSaisieTexte
        {
            get
            {
                return m_bAutoriserSaisieTexte;
            }
            set
            {
                m_bAutoriserSaisieTexte = value;
                Formule = Formule;
            }
        }

        //-----------------------------------------------
        private void m_btnGraphic_Click(object sender, EventArgs e)
        {
            C2iExpression exp = m_txtFormule.Formule;
            if (m_expressionGraphique != null)
                exp = m_expressionGraphique;
            exp = CFormEditionExpressionGraphique.EditeFormule(exp, m_fournisseur, m_objetAnalyse);
            m_expressionGraphique = exp as C2iExpressionGraphique;

            Formule = exp;
        }

        //-----------------------------------------------
        private void m_btnGraphic_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_expressionGraphique != null && e.Button == MouseButtons.Right && m_bAutoriserSaisieTexte)
            {
                if (MessageBox.Show(I.T("This action will allow you to type the formula but will lost its graphical representation. Continue ?|20007"),
                    I.T("Warning|20008"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Formule = m_expressionGraphique.FormuleFinale;
                }
            }
        }
    }
}
