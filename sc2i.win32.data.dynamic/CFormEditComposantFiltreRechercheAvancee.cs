using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditComposantFiltreRechercheAvancee.
	/// </summary>
	public class CFormEditComposantFiltreRechercheAvancee : System.Windows.Forms.Form
	{
		private CDefinitionProprieteDynamique m_definitionRacineDeChampsFiltres = null;

		private CDefinitionProprieteDynamique m_champ = null;

		private CComposantFiltreDynamiqueRechercheAvancee m_composant = null;
		private CFiltreDynamique m_filtre = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_boutonDropList;
		private System.Windows.Forms.Label m_labelChamp;
		private System.Windows.Forms.Panel m_panelComboChamp;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel m_btnCreerVariable;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private System.Windows.Forms.MenuItem m_menuVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.expression.CControleEditeFormule m_txtCondition;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel m_panelValeurAttendue;
		private System.Windows.Forms.Panel m_panelValeurFormule;

		private sc2i.win32.expression.CControleEditeFormule m_lastTextBox = null;

		public CFormEditComposantFiltreRechercheAvancee()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_lastTextBox = m_txtFormule;
			m_txtFormule.BackColor = Color.LightGreen;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditComposantFiltreRechercheAvancee));
            this.label1 = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_panelComboChamp = new System.Windows.Forms.Panel();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_panelValeurAttendue = new System.Windows.Forms.Panel();
            this.m_panelValeurFormule = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtCondition = new sc2i.win32.expression.CControleEditeFormule();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCreerVariable = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_panelComboChamp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelValeurAttendue.SuspendLayout();
            this.m_panelValeurFormule.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test field|112";
            // 
            // m_boutonDropList
            // 
            this.m_boutonDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonDropList.Image")));
            this.m_boutonDropList.Location = new System.Drawing.Point(300, 0);
            this.m_boutonDropList.Name = "m_boutonDropList";
            this.m_boutonDropList.Size = new System.Drawing.Size(17, 17);
            this.m_boutonDropList.TabIndex = 3;
            this.m_boutonDropList.TabStop = false;
            this.m_boutonDropList.UseVisualStyleBackColor = false;
            this.m_boutonDropList.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_labelChamp
            // 
            this.m_labelChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChamp.BackColor = System.Drawing.Color.White;
            this.m_labelChamp.Location = new System.Drawing.Point(0, 0);
            this.m_labelChamp.Name = "m_labelChamp";
            this.m_labelChamp.Size = new System.Drawing.Size(300, 17);
            this.m_labelChamp.TabIndex = 2;
            this.m_labelChamp.Text = "Label|50";
            this.m_labelChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_tooltip.SetToolTip(this.m_labelChamp, "Indiquez ici le champ à tester|161");
            this.m_labelChamp.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_panelComboChamp
            // 
            this.m_panelComboChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChamp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChamp.Controls.Add(this.m_labelChamp);
            this.m_panelComboChamp.Controls.Add(this.m_boutonDropList);
            this.m_panelComboChamp.Location = new System.Drawing.Point(88, 8);
            this.m_panelComboChamp.Name = "m_panelComboChamp";
            this.m_panelComboChamp.Size = new System.Drawing.Size(320, 21);
            this.m_panelComboChamp.TabIndex = 4;
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(416, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(232, 326);
            this.m_wndAide.TabIndex = 7;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.m_panelValeurAttendue);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.m_txtCondition);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.m_btnCreerVariable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.m_panelComboChamp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 326);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // m_panelValeurAttendue
            // 
            this.m_panelValeurAttendue.Controls.Add(this.m_panelValeurFormule);
            this.m_panelValeurAttendue.Location = new System.Drawing.Point(8, 64);
            this.m_panelValeurAttendue.Name = "m_panelValeurAttendue";
            this.m_panelValeurAttendue.Size = new System.Drawing.Size(392, 128);
            this.m_panelValeurAttendue.TabIndex = 15;
            // 
            // m_panelValeurFormule
            // 
            this.m_panelValeurFormule.Controls.Add(this.m_txtFormule);
            this.m_panelValeurFormule.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelValeurFormule.Location = new System.Drawing.Point(0, 0);
            this.m_panelValeurFormule.Name = "m_panelValeurFormule";
            this.m_panelValeurFormule.Size = new System.Drawing.Size(392, 128);
            this.m_panelValeurFormule.TabIndex = 1;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(392, 128);
            this.m_txtFormule.TabIndex = 8;
            this.m_tooltip.SetToolTip(this.m_txtFormule, "Point here to the formula that return the expected value for the comparison test" +
                    "|159");
            this.m_txtFormule.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Enable condition|115";
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCondition.BackColor = System.Drawing.Color.White;
            this.m_txtCondition.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(8, 232);
            this.m_txtCondition.LockEdition = false;
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(400, 56);
            this.m_txtCondition.TabIndex = 13;
            this.m_tooltip.SetToolTip(this.m_txtCondition, "This test will be incorporated to the filer only if the evaluation of the condition is equal to 1|160");
            this.m_txtCondition.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(220, 296);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_btnAnnuler.TabIndex = 12;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(92, 296);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_btnOk.TabIndex = 11;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCreerVariable
            // 
            this.m_btnCreerVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreerVariable.Location = new System.Drawing.Point(247, 40);
            this.m_btnCreerVariable.Name = "m_btnCreerVariable";
            this.m_btnCreerVariable.Size = new System.Drawing.Size(161, 16);
            this.m_btnCreerVariable.TabIndex = 10;
            this.m_btnCreerVariable.TabStop = true;
            this.m_btnCreerVariable.Text = "Create a new variable|114";
            this.m_btnCreerVariable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCreerVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnCreerVariable_LinkClicked);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Advanced research|113";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(413, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 326);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuVariableCalculée,
            this.m_menuVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered|30010";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuVariableCalculée
            // 
            this.m_menuVariableCalculée.Index = 1;
            this.m_menuVariableCalculée.Text = "Computed|30011";
            this.m_menuVariableCalculée.Click += new System.EventHandler(this.m_menuVariableCalculée_Click);
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Selection|30012";
            this.m_menuVariableSelection.Click += new System.EventHandler(this.m_menuVariableSelection_Click);
            // 
            // CFormEditComposantFiltreRechercheAvancee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(648, 326);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_wndAide);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditComposantFiltreRechercheAvancee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Search|111";
            this.Load += new System.EventHandler(this.CFormEditComposantFiltreRechercheAvancee_Load);
            this.m_panelComboChamp.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelValeurAttendue.ResumeLayout(false);
            this.m_panelValeurFormule.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_boutonDropList_Click(object sender, System.EventArgs e)
		{
			Rectangle rect = m_panelComboChamp.RectangleToScreen(new Rectangle ( 0, m_panelComboChamp.Height, m_panelComboChamp.Width, 230));
			bool bCancel = false;
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( rect, m_filtre.TypeElements , new CFournisseurProprietesForFiltreDynamique(), ref bCancel, null, m_definitionRacineDeChampsFiltres );
			if ( !bCancel )
			{
				m_champ = champ;
				m_labelChamp.Text = m_champ==null?I.T("[UNDEFINED]|30013"):m_champ.Nom;
			}
				
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void Init ( CComposantFiltreDynamiqueRechercheAvancee composant, CFiltreDynamique filtre, CDefinitionProprieteDynamique definitionRacineDeChampsFiltres )
		{
			m_composant = composant;
			m_filtre = filtre;
			m_definitionRacineDeChampsFiltres = definitionRacineDeChampsFiltres;
			
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public static bool EditeComposantRechercheAvancee ( 
			CComposantFiltreDynamiqueRechercheAvancee composant,
			CFiltreDynamique filtre,
			bool bAvecVariables,
			CDefinitionProprieteDynamique definitionRacineDeChampsFiltres/*pour traduction*/)
		{
			CFormEditComposantFiltreRechercheAvancee form = new CFormEditComposantFiltreRechercheAvancee();
			form.Init ( composant, filtre, definitionRacineDeChampsFiltres );
			if (!bAvecVariables )
				form.m_btnCreerVariable.Visible = false;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}


		/// ////////////////////////////////////////////////////////////////////////////
		private void CFormEditComposantFiltreRechercheAvancee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            InitDialog();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public void InitDialog()
		{
			m_labelChamp.Text = m_composant.Champ==null?I.T("[UNDEFINED]|30013"):m_composant.Champ.Nom;
			m_champ = m_composant.Champ;
			m_wndAide.FournisseurProprietes = m_filtre;
			m_wndAide.ObjetInterroge = typeof(CFiltreDynamique);

			m_txtFormule.Text = m_composant.ExpressionValeur == null?"":m_composant.ExpressionValeur.GetString();
			m_txtFormule.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);

			m_txtCondition.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtCondition.Text = m_composant.ConditionApplication.GetString();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnCreerVariable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_menuNewVariable.Show ( m_btnCreerVariable, new Point ( 0, m_btnCreerVariable.Height ) );
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie ( m_filtre );
			if ( CFormEditVariableDynamiqueSaisie.EditeVariable ( variable, m_filtre ) )
			{
				m_filtre.AddVariable ( variable );
				m_wndAide.FournisseurProprietes = m_filtre;
				m_wndAide.RefillChamps();
			}
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_lastTextBox != null )
				m_wndAide.InsereInTextBox ( m_lastTextBox, nPosCurseur, strCommande );
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_champ == null )
			{
				CFormAlerte.Afficher(I.T("Select a field to test|30014"), EFormAlerteType.Exclamation);
				return;
			}

			CResultAErreur result = CResultAErreur.True;

			C2iExpression expressionValeur = null;
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( m_filtre, typeof(CFiltreDynamique) );
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in value formula|30015"));
				CFormAlerte.Afficher ( result);
				return;
			}
			expressionValeur = (C2iExpression)result.Data;
			result = analyseur.AnalyseChaine ( m_txtCondition.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in condition formula|30016"));
				CFormAlerte.Afficher ( result);
				return;
			}
			C2iExpression expressionCondition = (C2iExpression)result.Data;

			
			
			m_composant.Champ = m_champ;
			m_composant.ExpressionValeur = expressionValeur;
			m_composant.ConditionApplication = expressionCondition;
			DialogResult = DialogResult.OK;
			Close();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableCalculée_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_filtre );
			if ( CFormEditVariableFiltreCalculee.EditeVariable(variable, m_filtre) )
			{
				m_filtre.AddVariable ( variable );
				m_wndAide.RefillChamps();
			}
		}

		private void m_txtFormule_Enter(object sender, System.EventArgs e)
		{
			if ( !(sender is sc2i.win32.expression.CControleEditeFormule) )
				return;
			if ( m_lastTextBox != null )
			{
				m_lastTextBox.BackColor = Color.White;
			}
			m_lastTextBox = (sc2i.win32.expression.CControleEditeFormule)sender;
			m_lastTextBox.BackColor = Color.LightGreen;
		}

		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_filtre );
			if ( CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable(variable) )
			{
				m_filtre.AddVariable ( variable );
				m_wndAide.RefillChamps();
			}
		}

	}
}
