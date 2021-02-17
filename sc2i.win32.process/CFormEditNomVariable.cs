using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.common;
using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CFormEditNomVariable.
	/// </summary>
	public class CFormEditNomVariable : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private sc2i.win32.common.C2iPanel m_panelType;
		private System.Windows.Forms.CheckBox m_chkTableau;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel m_panelIdInitial;
        private C2iTextBox m_txtKeyInitiale;
		private System.ComponentModel.IContainer components;

		public CFormEditNomVariable()
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditNomVariable));
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.m_panelType = new sc2i.win32.common.C2iPanel(this.components);
            this.m_chkTableau = new System.Windows.Forms.CheckBox();
            this.m_panelIdInitial = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtKeyInitiale = new sc2i.win32.common.C2iTextBox();
            this.panel1.SuspendLayout();
            this.m_panelType.SuspendLayout();
            this.m_panelIdInitial.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name|164";
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Location = new System.Drawing.Point(56, 8);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(280, 20);
            this.m_txtNomVariable.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 48);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(180, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(126, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type|165";
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeElements.IsLink = false;
            this.m_cmbTypeElements.Location = new System.Drawing.Point(48, 8);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(280, 21);
            this.m_cmbTypeElements.TabIndex = 4;
            // 
            // m_panelType
            // 
            this.m_panelType.Controls.Add(this.m_chkTableau);
            this.m_panelType.Controls.Add(this.m_cmbTypeElements);
            this.m_panelType.Controls.Add(this.label2);
            this.m_panelType.Location = new System.Drawing.Point(8, 32);
            this.m_panelType.LockEdition = false;
            this.m_panelType.Name = "m_panelType";
            this.m_panelType.Size = new System.Drawing.Size(336, 48);
            this.m_panelType.TabIndex = 5;
            // 
            // m_chkTableau
            // 
            this.m_chkTableau.Location = new System.Drawing.Point(56, 32);
            this.m_chkTableau.Name = "m_chkTableau";
            this.m_chkTableau.Size = new System.Drawing.Size(128, 16);
            this.m_chkTableau.TabIndex = 5;
            this.m_chkTableau.Text = "Array|166";
            this.m_chkTableau.CheckedChanged += new System.EventHandler(this.m_chkTableau_CheckedChanged);
            // 
            // m_panelIdInitial
            // 
            this.m_panelIdInitial.Controls.Add(this.m_txtKeyInitiale);
            this.m_panelIdInitial.Controls.Add(this.label3);
            this.m_panelIdInitial.Location = new System.Drawing.Point(8, 80);
            this.m_panelIdInitial.Name = "m_panelIdInitial";
            this.m_panelIdInitial.Size = new System.Drawing.Size(296, 24);
            this.m_panelIdInitial.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Initial Id|167";
            // 
            // m_txtKeyInitiale
            // 
            this.m_txtKeyInitiale.EmptyText = "";
            this.m_txtKeyInitiale.Location = new System.Drawing.Point(62, 2);
            this.m_txtKeyInitiale.LockEdition = false;
            this.m_txtKeyInitiale.Name = "m_txtKeyInitiale";
            this.m_txtKeyInitiale.Size = new System.Drawing.Size(231, 20);
            this.m_txtKeyInitiale.TabIndex = 8;
            // 
            // CFormEditNomVariable
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(346, 151);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelType);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_txtNomVariable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_panelIdInitial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CFormEditNomVariable";
            this.ShowInTaskbar = false;
            this.Text = "Variable|152";
            this.Load += new System.EventHandler(this.CFormEditNomVariable_Load);
            this.panel1.ResumeLayout(false);
            this.m_panelType.ResumeLayout(false);
            this.m_panelIdInitial.ResumeLayout(false);
            this.m_panelIdInitial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public static bool EditeNomVariable(ref string strNom, ref CTypeResultatExpression typeDonnee, ref CDbKey dbKeyInitial, bool bAvecIdInitial)
        {
            CFormEditNomVariable form = new CFormEditNomVariable();
            form.m_txtNomVariable.Text = strNom;
            form.InitComboBoxType(typeDonnee != null ? typeDonnee.TypeDotNetNatif : null);
            if (typeDonnee != null)
            {
                form.m_panelType.LockEdition = true;
                form.m_chkTableau.Checked = typeDonnee.IsArrayOfTypeNatif;
                form.m_cmbTypeElements.SelectedValue = typeDonnee.TypeDotNetNatif;
                form.m_txtKeyInitiale.Text = dbKeyInitial != null?dbKeyInitial.StringValue:"";
            }
            form.m_panelIdInitial.Visible = bAvecIdInitial;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            if (bResult)
            {
                strNom = form.m_txtNomVariable.Text;
                if (!form.m_panelType.LockEdition)
                {
                    Type tp = (Type)form.m_cmbTypeElements.SelectedValue;
                    if (tp == null || tp == typeof(DBNull))
                        tp = null;
                    typeDonnee = new CTypeResultatExpression(
                        (Type)form.m_cmbTypeElements.SelectedValue,
                        form.m_chkTableau.Checked);
                }
                if (form.m_txtKeyInitiale != null)
                {
                    int nId = -1;
                    if ( int.TryParse(form.m_txtKeyInitiale.Text, out nId ) )
                        dbKeyInitial = CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId);
                    else
                        dbKeyInitial = CDbKey.CreateFromStringValue(form.m_txtKeyInitiale.Text);
                }
                else
                    bResult = false;
            }
            form.Dispose();
            return bResult;
        }

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_txtNomVariable.Text == "" )
			{
				CFormAlerte.Afficher(I.T("Enter a variable name|30022"), EFormAlerteType.Exclamation);
				return;
			}
			if ( !(m_cmbTypeElements.SelectedValue is Type) )
			{
				CFormAlerte.Afficher(I.T("Select a data type|30023"), EFormAlerteType.Exclamation);
				return ;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		
		/// <summary>
		/// ////////////////////////////////////////////////
		/// </summary>
		/// <returns></returns>
		private bool m_bComboInitialized = false;
		private void InitComboBoxType( Type tp )
		{
			if (m_bComboInitialized)
				return ;
			ArrayList lstTypes = new ArrayList();
			if (tp != null)
			{
				CInfoClasseDynamique info = new CInfoClasseDynamique(tp, DynamicClassAttribute.GetNomConvivial(tp));
				lstTypes.Add(info);
			}
			else
			{
				//CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass();
				ArrayList infosClasses = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
				
                infosClasses.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("None|19")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(string),I.T("String")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(double),I.T("Double")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(int),I.T("Int")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(DateTime),I.T("DateTime")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(bool), I.T("Boolean")));
                infosClasses.Add(new CInfoClasseDynamique(typeof(object), I.T("Object")));
                lstTypes.AddRange(infosClasses);
			}
			m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.DataSource = lstTypes;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";

			m_bComboInitialized = true;
		}

		private void m_chkTableau_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateEtatIdInitial();
		}

		private void UpdateEtatIdInitial()
		{
			m_txtKeyInitiale.Enabled = !m_chkTableau.Checked;
		}

        private void CFormEditNomVariable_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }
	}
}
