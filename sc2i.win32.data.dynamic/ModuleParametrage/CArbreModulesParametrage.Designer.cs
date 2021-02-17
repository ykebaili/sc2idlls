namespace sc2i.win32.data.dynamic
{
    partial class CArbreModulesParametrage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CArbreModulesParametrage));
            this.m_imageListModules = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // m_imageListModules
            // 
            this.m_imageListModules.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListModules.ImageStream")));
            this.m_imageListModules.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListModules.Images.SetKeyName(0, "1256661066_gnome-fs-directory.png");
            this.m_imageListModules.Images.SetKeyName(1, "1256743475_gnome-fs-directory-accept.png");
            // 
            // CArbreModulesParametrage
            // 
            this.ImageIndex = 0;
            this.ImageList = this.m_imageListModules;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 1;
            this.StateImageList = this.m_imageListModules;
            this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.CArbreModulesParametrage_BeforeExpand);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList m_imageListModules;
    }
}
