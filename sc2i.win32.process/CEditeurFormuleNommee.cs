using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.expression;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CEditeurFormuleNommee.
	/// </summary>
	public class CEditeurFormuleNommee : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label m_label;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CEditeurFormuleNommee()
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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_label = new System.Windows.Forms.Label();
			this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
			this.SuspendLayout();
			// 
			// m_label
			// 
			this.m_label.Location = new System.Drawing.Point(0, 0);
			this.m_label.Name = "m_label";
			this.m_label.Size = new System.Drawing.Size(120, 40);
			this.m_label.TabIndex = 0;
			this.m_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_txtFormule
			// 
			this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtFormule.BackColor = System.Drawing.Color.White;
			this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_txtFormule.Location = new System.Drawing.Point(120, 0);
			this.m_txtFormule.LockEdition = false;
			this.m_txtFormule.Name = "m_txtFormule";
			this.m_txtFormule.Size = new System.Drawing.Size(320, 42);
			this.m_txtFormule.TabIndex = 3;
			// 
			// CEditeurFormuleNommee
			// 
			this.Controls.Add(this.m_txtFormule);
			this.Controls.Add(this.m_label);
			this.Name = "CEditeurFormuleNommee";
			this.Size = new System.Drawing.Size(440, 42);
			this.ResumeLayout(false);

		}
		#endregion

		public void Init ( IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse )
		{
			m_txtFormule.Init ( fournisseur, objetAnalyse);
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
				return m_label.Text;
			}
			set
			{
				m_label.Text = value;
			}
		}

		//-----------------------------------------------
		public CResultAErreur ResultAnalyse
		{
			get
			{
				if ( m_txtFormule.Text.Trim() != "" )
					return m_txtFormule.ResultAnalyse;
				return CResultAErreur.True;
			}
		}

		//-----------------------------------------------
		public CControleEditeFormule TextFormule
		{
			get
			{
				return m_txtFormule;
			}
		}
		
	}
}
