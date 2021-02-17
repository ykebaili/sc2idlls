using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.win32.expression.variablesDynamiques;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableDynamiqueCalculee.
	/// </summary>
    [AutoExec("Autoexec")]
    public class CFormEditVariableFiltreCalculee : System.Windows.Forms.Form, IFormEditVariableDynamique
	{
		private IElementAVariablesDynamiquesBase m_elementAVariables = null;
		private CVariableDynamiqueCalculee m_variable = null;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
        private System.Windows.Forms.Panel panel1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
        private CExtStyle cExtStyle1;
        private SplitContainer m_splitContainer;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditVariableFiltreCalculee()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.c2iPanelOmbre2 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label3 = new System.Windows.Forms.Label();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.c2iPanelOmbre2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomVariable);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(465, 80);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 18;
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomVariable.Location = new System.Drawing.Point(130, 31);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(311, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomVariable.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable name|143";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 16);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computed variable|142";
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(0, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(197, 411);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAide.TabIndex = 19;
			this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.c2iPanelOmbre2);
            this.panel1.Controls.Add(this.c2iPanelOmbre1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 411);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 20;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(252, 381);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 22;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(124, 381);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 21;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // c2iPanelOmbre2
            // 
            this.c2iPanelOmbre2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre2.Controls.Add(this.m_txtFormule);
            this.c2iPanelOmbre2.Controls.Add(this.label3);
            this.c2iPanelOmbre2.Location = new System.Drawing.Point(8, 88);
            this.c2iPanelOmbre2.LockEdition = false;
            this.c2iPanelOmbre2.Name = "c2iPanelOmbre2";
            this.c2iPanelOmbre2.Size = new System.Drawing.Size(465, 285);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre2.TabIndex = 20;
            this.c2iPanelOmbre2.Paint += new System.Windows.Forms.PaintEventHandler(this.c2iPanelOmbre2_Paint);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(433, 237);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormule.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 16);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 3;
            this.label3.Text = "Formula|140";
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.panel1);
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAide);
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.Size = new System.Drawing.Size(690, 415);
            this.m_splitContainer.SplitterDistance = 485;
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.TabIndex = 21;
            // 
            // CFormEditVariableFiltreCalculee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(690, 415);
            this.Controls.Add(this.m_splitContainer);
            this.Name = "CFormEditVariableFiltreCalculee";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Computed dynamic variable|141";
            this.Load += new System.EventHandler(this.CFormEditVariableDynamiqueCalculee_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.c2iPanelOmbre2.ResumeLayout(false);
            this.c2iPanelOmbre2.PerformLayout();
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


        /// //////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireEditeursVariablesDynamiques.RegisterEditeur(typeof(CVariableDynamiqueCalculee), typeof(CFormEditVariableFiltreCalculee));
        }

		private void c2iPanelOmbre2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void CFormEditVariableDynamiqueCalculee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_wndAide.FournisseurProprietes = m_elementAVariables is IFournisseurProprietesDynamiques?
                (IFournisseurProprietesDynamiques)m_elementAVariables:
                new CFournisseurPropDynStd();
			m_wndAide.ObjetInterroge = new CObjetPourSousProprietes(m_elementAVariables);
			m_txtNomVariable.Text = m_variable.Nom;
			m_txtFormule.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtFormule.Text = m_variable.Expression!=null?m_variable.Expression.GetString():"";
		}

		/// //////////////////////////////////////////////////////
		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAide.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////////////////////
		private void Init ( CVariableDynamiqueCalculee variable, IElementAVariablesDynamiquesBase filtre )
		{
			m_variable = variable;
			m_elementAVariables = filtre;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeVariable ( CVariableDynamiqueCalculee variable, IElementAVariablesDynamiquesBase element )
		{
			CFormEditVariableFiltreCalculee form = new CFormEditVariableFiltreCalculee();
			form.Init ( variable, element);
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		/// //////////////////////////////////////////////////////
		protected C2iExpression GetExpression()
		{
            IFournisseurProprietesDynamiques f = m_elementAVariables as IFournisseurProprietesDynamiques;
            if (f == null)
                f = new CFournisseurPropDynStd();
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression( f,  new CObjetPourSousProprietes(m_elementAVariables) );
			CAnalyseurSyntaxique analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			CResultAErreur result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return null;
			}
			return (C2iExpression)result.Data;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_txtNomVariable.Text.Trim() == "" )
			{
				CFormAlerte.Afficher(I.T("Enter a variable name|30022"), EFormAlerteType.Exclamation);
				return;
			}
			C2iExpression expression = GetExpression();
			if ( expression == null )
				return;
			m_variable.Nom = m_txtNomVariable.Text.Replace(" ","_").Trim();
			m_variable.Expression = expression;
			DialogResult = DialogResult.OK;
			Close();

		}

        #region IFormEditVariableDynamique Membres

        public bool EditeLaVariable(IVariableDynamique variable, IElementAVariablesDynamiquesBase eltAVariables)
        {
            Init(variable as CVariableDynamiqueCalculee, eltAVariables);
            bool bResult = ShowDialog() == DialogResult.OK;
            Dispose();
            return bResult;
        }

        #endregion
    }
}
