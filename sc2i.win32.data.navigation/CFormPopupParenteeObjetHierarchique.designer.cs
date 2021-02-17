namespace sc2i.win32.data.navigation
{
    partial class CFormPopupParenteeObjetHierarchique
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
            this.m_wndListe = new sc2i.win32.common.CListLink();
            this.SuspendLayout();
            // 
            // m_wndListe
            // 
            this.m_wndListe.ColorActive = System.Drawing.Color.Red;
            this.m_wndListe.ColorDesactive = System.Drawing.Color.Blue;
            this.m_wndListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline);
            this.m_wndListe.ForeColor = System.Drawing.Color.Blue;
            this.m_wndListe.FullRowSelect = true;
            this.m_wndListe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListe.Location = new System.Drawing.Point(0, 0);
            this.m_wndListe.MultiSelect = false;
            this.m_wndListe.Name = "m_wndListe";
            this.m_wndListe.Size = new System.Drawing.Size(330, 123);
            this.m_wndListe.TabIndex = 0;
            this.m_wndListe.UseCompatibleStateImageBehavior = false;
            this.m_wndListe.View = System.Windows.Forms.View.Details;
            this.m_wndListe.ItemClick += new sc2i.win32.common.ListLinkItemClickEventHandler(this.m_wndListe_ItemClick);
            // 
            // CFormPopupParenteeObjetHierarchique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 123);
            this.Controls.Add(this.m_wndListe);
            this.Name = "CFormPopupParenteeObjetHierarchique";
            this.Text = "CFormPopupHierarchie";
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CListLink m_wndListe;
    }
}