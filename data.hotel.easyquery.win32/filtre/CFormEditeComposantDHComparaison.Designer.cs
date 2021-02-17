namespace data.hotel.eastquery.win32.filtre
{
    partial class CFormEditeComposantDHComparaison
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_txtFormuleCondition = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtFormuleValeur = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label3 = new System.Windows.Forms.Label();
            this.m_comboOperateur = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboChamp = new sc2i.win32.common.CComboboxAutoFilled();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field|20028";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 35);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(252, 6);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|2";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(157, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_txtFormuleCondition);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_txtFormuleValeur);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_comboOperateur);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.m_comboChamp);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 133);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 4;
            // 
            // m_txtFormuleCondition
            // 
            this.m_txtFormuleCondition.AllowGraphic = false;
            this.m_txtFormuleCondition.AllowNullFormula = true;
            this.m_txtFormuleCondition.AllowSaisieTexte = true;
            this.m_txtFormuleCondition.Formule = null;
            this.m_txtFormuleCondition.Location = new System.Drawing.Point(109, 94);
            this.m_txtFormuleCondition.LockEdition = false;
            this.m_txtFormuleCondition.LockZoneTexte = false;
            this.m_txtFormuleCondition.Name = "m_txtFormuleCondition";
            this.m_txtFormuleCondition.Size = new System.Drawing.Size(343, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleCondition.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 18);
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 6;
            this.label4.Text = "Condition|20031";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtFormuleValeur
            // 
            this.m_txtFormuleValeur.AllowGraphic = false;
            this.m_txtFormuleValeur.AllowNullFormula = true;
            this.m_txtFormuleValeur.AllowSaisieTexte = true;
            this.m_txtFormuleValeur.Formule = null;
            this.m_txtFormuleValeur.Location = new System.Drawing.Point(109, 54);
            this.m_txtFormuleValeur.LockEdition = false;
            this.m_txtFormuleValeur.LockZoneTexte = false;
            this.m_txtFormuleValeur.Name = "m_txtFormuleValeur";
            this.m_txtFormuleValeur.Size = new System.Drawing.Size(343, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleValeur.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 18);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value|20030";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_comboOperateur
            // 
            this.m_comboOperateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_comboOperateur.FormattingEnabled = true;
            this.m_comboOperateur.IsLink = false;
            this.m_comboOperateur.ListDonnees = null;
            this.m_comboOperateur.Location = new System.Drawing.Point(109, 29);
            this.m_comboOperateur.LockEdition = false;
            this.m_comboOperateur.Name = "m_comboOperateur";
            this.m_comboOperateur.NullAutorise = false;
            this.m_comboOperateur.ProprieteAffichee = null;
            this.m_comboOperateur.Size = new System.Drawing.Size(103, 21);
            this.m_extStyle.SetStyleBackColor(this.m_comboOperateur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_comboOperateur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_comboOperateur.TabIndex = 3;
            this.m_comboOperateur.Text = "(empty)";
            this.m_comboOperateur.TextNull = "(empty)";
            this.m_comboOperateur.Tri = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 2;
            this.label2.Text = "Operator|20029";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_comboChamp
            // 
            this.m_comboChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_comboChamp.FormattingEnabled = true;
            this.m_comboChamp.IsLink = false;
            this.m_comboChamp.ListDonnees = null;
            this.m_comboChamp.Location = new System.Drawing.Point(109, 6);
            this.m_comboChamp.LockEdition = false;
            this.m_comboChamp.Name = "m_comboChamp";
            this.m_comboChamp.NullAutorise = false;
            this.m_comboChamp.ProprieteAffichee = null;
            this.m_comboChamp.Size = new System.Drawing.Size(343, 21);
            this.m_extStyle.SetStyleBackColor(this.m_comboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_comboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_comboChamp.TabIndex = 1;
            this.m_comboChamp.Text = "(empty)";
            this.m_comboChamp.TextNull = "(empty)";
            this.m_comboChamp.Tri = true;
            // 
            // CFormEditeComposantDHComparaison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 168);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormEditeComposantDHComparaison";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Comparaison|20027";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleCondition;
        private System.Windows.Forms.Label label4;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleValeur;
        private System.Windows.Forms.Label label3;
        private sc2i.win32.common.CComboboxAutoFilled m_comboOperateur;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.common.CComboboxAutoFilled m_comboChamp;
    }
}