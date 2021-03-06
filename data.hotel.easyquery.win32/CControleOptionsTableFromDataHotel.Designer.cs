﻿namespace data.hotel.easyquery.win32
{
    partial class CControleOptionsTableFromDataHotel
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtStartDate = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtEndDate = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_panelSource = new data.hotel.easyquery.win32.entitysource.CPanelEditSourceEntites();
            this.m_panelFiltre = new data.hotel.eastquery.win32.filtre.CPanelFiltreDataHotel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start date |20001";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStartDate
            // 
            this.m_txtStartDate.AllowGraphic = true;
            this.m_txtStartDate.AllowNullFormula = false;
            this.m_txtStartDate.AllowSaisieTexte = true;
            this.m_txtStartDate.Formule = null;
            this.m_txtStartDate.Location = new System.Drawing.Point(153, 3);
            this.m_txtStartDate.LockEdition = false;
            this.m_txtStartDate.LockZoneTexte = false;
            this.m_txtStartDate.Name = "m_txtStartDate";
            this.m_txtStartDate.Size = new System.Drawing.Size(324, 21);
            this.m_txtStartDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "End date |20002";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtEndDate
            // 
            this.m_txtEndDate.AllowGraphic = true;
            this.m_txtEndDate.AllowNullFormula = false;
            this.m_txtEndDate.AllowSaisieTexte = true;
            this.m_txtEndDate.Formule = null;
            this.m_txtEndDate.Location = new System.Drawing.Point(154, 26);
            this.m_txtEndDate.LockEdition = false;
            this.m_txtEndDate.LockZoneTexte = false;
            this.m_txtEndDate.Name = "m_txtEndDate";
            this.m_txtEndDate.Size = new System.Drawing.Size(323, 21);
            this.m_txtEndDate.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_txtEndDate);
            this.panel1.Controls.Add(this.m_txtStartDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 56);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(484, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hotel query options|20004";
            // 
            // m_panelSource
            // 
            this.m_panelSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelSource.Location = new System.Drawing.Point(0, 80);
            this.m_panelSource.Name = "m_panelSource";
            this.m_panelSource.Size = new System.Drawing.Size(484, 106);
            this.m_panelSource.TabIndex = 2;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.BackColor = System.Drawing.Color.White;
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.ForeColor = System.Drawing.Color.Black;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 210);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(484, 123);
            this.m_panelFiltre.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(484, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Filter|20036";
            // 
            // CControleOptionsTableFromDataHotel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelFiltre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_panelSource);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Name = "CControleOptionsTableFromDataHotel";
            this.Size = new System.Drawing.Size(484, 333);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtStartDate;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtEndDate;
        private entitysource.CPanelEditSourceEntites m_panelSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private eastquery.win32.filtre.CPanelFiltreDataHotel m_panelFiltre;
        private System.Windows.Forms.Label label4;
    }
}
