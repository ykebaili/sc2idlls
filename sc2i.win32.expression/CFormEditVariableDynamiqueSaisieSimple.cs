using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common.unites;
using System.Collections.Generic;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Description résumée de CFormEditVariableDynamiqueSaisie.
	/// </summary>
	public class CFormEditVariableDynamiqueSaisieSimple : System.Windows.Forms.Form
	{
		private const string c_strColValeurAffichee = "Valeur affichée";
		private const string c_strColValeurStockee = "Valeur stockée";
		private const string c_nomTableValeurs = "VALEURS";


		private IElementAVariablesDynamiques m_elementAVariables = null;
		private CVariableDynamiqueSaisieSimple m_variable = null;


        private sc2i.win32.expression.CControleEditeFormule m_textBoxFormule = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Label label3;
        private sc2i.win32.common.C2iComboBox m_cmbType;
		private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre3;
        private System.Windows.Forms.Label label8;
        private CExtStyle cExtStyle1;
        private Panel m_panelUnite;
        private C2iTextBox m_txtFormatUnite;
        private Label label15;
        private CComboboxAutoFilled m_cmbSelectClasseUnite;
        private Label label14;
        private CTextBoxZoomFormule m_txtValeurDefaut;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditVariableDynamiqueSaisieSimple()
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmbType = new sc2i.win32.common.C2iComboBox();
            this.c2iPanelOmbre3 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtValeurDefaut = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label8 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_panelUnite = new System.Windows.Forms.Panel();
            this.m_txtFormatUnite = new sc2i.win32.common.C2iTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.m_cmbSelectClasseUnite = new sc2i.win32.common.CComboboxAutoFilled();
            this.label14 = new System.Windows.Forms.Label();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre3.SuspendLayout();
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_panelUnite.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name|20025";
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomVariable.Location = new System.Drawing.Point(112, 7);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(392, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomVariable.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 21);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data type|20026";
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(112, 29);
            this.m_cmbType.LockEdition = false;
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(392, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbType.TabIndex = 1;
            this.m_cmbType.SelectedValueChanged += new System.EventHandler(this.m_cmbType_SelectedValueChanged);
            // 
            // c2iPanelOmbre3
            // 
            this.c2iPanelOmbre3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre3.Controls.Add(this.m_txtValeurDefaut);
            this.c2iPanelOmbre3.Controls.Add(this.label8);
            this.c2iPanelOmbre3.Location = new System.Drawing.Point(11, 121);
            this.c2iPanelOmbre3.LockEdition = false;
            this.c2iPanelOmbre3.Name = "c2iPanelOmbre3";
            this.c2iPanelOmbre3.Size = new System.Drawing.Size(528, 104);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre3, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre3.TabIndex = 3;
            // 
            // m_txtValeurDefaut
            // 
            this.m_txtValeurDefaut.AllowGraphic = true;
            this.m_txtValeurDefaut.AllowNullFormula = false;
            this.m_txtValeurDefaut.AllowSaisieTexte = true;
            this.m_txtValeurDefaut.Formule = null;
            this.m_txtValeurDefaut.Location = new System.Drawing.Point(11, 28);
            this.m_txtValeurDefaut.LockEdition = false;
            this.m_txtValeurDefaut.LockZoneTexte = false;
            this.m_txtValeurDefaut.Name = "m_txtValeurDefaut";
            this.m_txtValeurDefaut.Size = new System.Drawing.Size(493, 51);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeurDefaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeurDefaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeurDefaut.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 16);
            this.cExtStyle1.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 3;
            this.label8.Text = "Default value|20030";
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_panelUnite);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomVariable);
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbType);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label3);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(11, 12);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(528, 112);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 0;
            // 
            // m_panelUnite
            // 
            this.m_panelUnite.Controls.Add(this.m_txtFormatUnite);
            this.m_panelUnite.Controls.Add(this.label15);
            this.m_panelUnite.Controls.Add(this.m_cmbSelectClasseUnite);
            this.m_panelUnite.Controls.Add(this.label14);
            this.m_panelUnite.Location = new System.Drawing.Point(20, 53);
            this.m_panelUnite.Name = "m_panelUnite";
            this.m_panelUnite.Size = new System.Drawing.Size(407, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelUnite.TabIndex = 19;
            // 
            // m_txtFormatUnite
            // 
            this.m_txtFormatUnite.EmptyText = "";
            this.m_txtFormatUnite.Location = new System.Drawing.Point(312, 0);
            this.m_txtFormatUnite.LockEdition = false;
            this.m_txtFormatUnite.Name = "m_txtFormatUnite";
            this.m_txtFormatUnite.Size = new System.Drawing.Size(92, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormatUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormatUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormatUnite.TabIndex = 22;
            this.m_txtFormatUnite.Text = "[FormatAffichageUnite]";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(216, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 19);
            this.cExtStyle1.SetStyleBackColor(this.label15, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label15, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label15.TabIndex = 21;
            this.label15.Text = "Display format|20029";
            // 
            // m_cmbSelectClasseUnite
            // 
            this.m_cmbSelectClasseUnite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSelectClasseUnite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbSelectClasseUnite.FormattingEnabled = true;
            this.m_cmbSelectClasseUnite.IsLink = false;
            this.m_cmbSelectClasseUnite.ListDonnees = null;
            this.m_cmbSelectClasseUnite.Location = new System.Drawing.Point(93, 0);
            this.m_cmbSelectClasseUnite.LockEdition = false;
            this.m_cmbSelectClasseUnite.Name = "m_cmbSelectClasseUnite";
            this.m_cmbSelectClasseUnite.NullAutorise = true;
            this.m_cmbSelectClasseUnite.ProprieteAffichee = null;
            this.m_cmbSelectClasseUnite.Size = new System.Drawing.Size(121, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbSelectClasseUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbSelectClasseUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbSelectClasseUnite.TabIndex = 20;
            this.m_cmbSelectClasseUnite.TextNull = "(empty)";
            this.m_cmbSelectClasseUnite.Tri = true;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 19);
            this.cExtStyle1.SetStyleBackColor(this.label14, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label14, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label14.TabIndex = 19;
            this.label14.Text = "Unit type|20028";
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(293, 224);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(149, 224);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditVariableDynamiqueSaisie
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(545, 260);
            this.Controls.Add(this.c2iPanelOmbre3);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnAnnuler);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditVariableDynamiqueSaisie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Variable|20024";
            this.Load += new System.EventHandler(this.CFormEditVariableDynamiqueSaisie_Load);
            this.c2iPanelOmbre3.ResumeLayout(false);
            this.c2iPanelOmbre3.PerformLayout();
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_panelUnite.ResumeLayout(false);
            this.m_panelUnite.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////////////////////////////
		private void CFormEditVariableDynamiqueSaisie_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
                        
            m_cmbType.Items.Clear();

            List<CTypeChampBasique> possibles = new List<CTypeChampBasique>();
            possibles.Add(new CTypeChampBasique(ETypeChampBasique.String));
            possibles.Add(new CTypeChampBasique(ETypeChampBasique.Int));
            possibles.Add(new CTypeChampBasique(ETypeChampBasique.Decimal));
            possibles.Add(new CTypeChampBasique(ETypeChampBasique.Date));
            possibles.Add(new CTypeChampBasique(ETypeChampBasique.Bool));
            
			m_cmbType.Items.AddRange(possibles.ToArray());

			m_txtNomVariable.Text = m_variable.Nom;
			m_cmbType.SelectedItem = m_variable.TypeChampBasique;
            m_txtValeurDefaut.Init(new CFournisseurGeneriqueProprietesDynamiques(), typeof(string));


            m_txtValeurDefaut.Formule = m_variable.FormuleValeurDefaut;

            m_cmbSelectClasseUnite.ListDonnees = CGestionnaireUnites.Classes;
            m_cmbSelectClasseUnite.ProprieteAffichee = "Libelle";
            m_cmbSelectClasseUnite.SelectedValue = m_variable.ClasseUnite;

            m_txtFormatUnite.Text = m_variable.FormatAffichageUnite;

            UpdateAffichagePanelUnite();
		}


        /// //////////////////////////////////////////////////////
        private void UpdateAffichagePanelUnite()
        {
            CTypeChampBasique type = m_cmbType.SelectedItem as CTypeChampBasique;
            m_panelUnite.Visible = type != null && type.Code == ETypeChampBasique.Decimal;
        }

		/// //////////////////////////////////////////////////////
		private void Init ( CVariableDynamiqueSaisieSimple variable, IElementAVariablesDynamiques filtre )
		{
			m_variable = variable;
			m_elementAVariables = filtre;
		}

		/// //////////////////////////////////////////////////////
        public static bool EditeVariable(CVariableDynamiqueSaisieSimple variable, IElementAVariablesDynamiques filtre)
		{
			CFormEditVariableDynamiqueSaisieSimple form = new CFormEditVariableDynamiqueSaisieSimple();
			form.Init ( variable, filtre );
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		

		/// //////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_cmbType.SelectedItem == null || !(m_cmbType.SelectedItem is CTypeChampBasique))
			{
				CFormAlerte.Afficher(I.T("Enter a data type|20027"), EFormAlerteType.Exclamation);
				return;
			}
			
			//Vérifie que toutes les données sont bien du type
            CTypeChampBasique typeDonnees = (CTypeChampBasique)m_cmbType.SelectedItem;
			

            if (typeDonnees.Code == ETypeChampBasique.Decimal)
            {
                IClasseUnite classe = m_cmbSelectClasseUnite.SelectedValue as IClasseUnite;
                m_variable.ClasseUnite = classe;
                m_variable.FormatAffichageUnite = m_txtFormatUnite.Text;
            }
            else
            {
                m_variable.ClasseUnite = null;
                m_variable.FormatAffichageUnite = "";
            }


			m_variable.Nom = m_txtNomVariable.Text.Replace(" ","_").Trim();
			m_variable.TypeChampBasique = (CTypeChampBasique)m_cmbType.SelectedItem;

            if (m_txtValeurDefaut.Formule == null)
            {
                if (!m_txtValeurDefaut.ResultAnalyse)
                {
                    CFormAlerte.Afficher(m_txtValeurDefaut.ResultAnalyse.Erreur);
                    return;
                }
            }

            m_variable.FormuleValeurDefaut = m_txtValeurDefaut.Formule;
			m_elementAVariables.OnChangeVariable ( m_variable );
			DialogResult = DialogResult.OK;
			Close();
		}

		/// //////////////////////////////////////////////////////
		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
		
		}

        private void m_cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateAffichagePanelUnite();            
        }

	}
}
