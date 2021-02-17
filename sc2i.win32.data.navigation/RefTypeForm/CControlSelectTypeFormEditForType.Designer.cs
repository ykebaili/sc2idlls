using sc2i.win32.common;
namespace sc2i.win32.data.navigation.RefTypeForm
{
    partial class CControlSelectTypeFormEditForType
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
            this.m_comboRefTypeForm = new sc2i.win32.common.CComboboxAutoFilled();
            this.SuspendLayout();
            // 
            // m_comboRefTypeForm
            // 
            this.m_comboRefTypeForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_comboRefTypeForm.FormattingEnabled = true;
            this.m_comboRefTypeForm.IsLink = false;
            this.m_comboRefTypeForm.ListDonnees = null;
            this.m_comboRefTypeForm.Location = new System.Drawing.Point(0, 0);
            this.m_comboRefTypeForm.LockEdition = false;
            this.m_comboRefTypeForm.Name = "m_comboRefTypeForm";
            this.m_comboRefTypeForm.NullAutorise = false;
            this.m_comboRefTypeForm.ProprieteAffichee = null;
            this.m_comboRefTypeForm.Size = new System.Drawing.Size(370, 21);
            this.m_comboRefTypeForm.TabIndex = 0;
            this.m_comboRefTypeForm.TextNull = "(empty)";
            this.m_comboRefTypeForm.Tri = true;
            // 
            // CControlSelectTypeFormEditForType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.m_comboRefTypeForm);
            this.Name = "CControlSelectTypeFormEditForType";
            this.Size = new System.Drawing.Size(370, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private CComboboxAutoFilled m_comboRefTypeForm;
    }
}
