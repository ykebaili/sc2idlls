using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

using sc2i.drawing;
using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Panel de base pour l'édition d'objets graphiques I2iObjetGraphique
	/// </summary>
	public class CPanelEditionObjetGraphique : System.Windows.Forms.UserControl, IControlALockEdition
	{

        /// <summary>
        /// Id unique pour indiquer que les éléments viennent de moi ou pas
        /// </summary>
        private string m_strOrigineDragDropID = "";

        public enum EModeSouris
        {
            Selection,
            Zoom,
            Custom
          
        }

		#region Component Designer generated code



		private CSelectionElementsGraphiques m_selection;
		protected ContextMenuStrip m_mnu;
		private ToolStripMenuItem m_mnu_itm_bringToFront;
		private ToolStripMenuItem m_mnu_itm_bringToBack;
		private ToolStripMenuItem m_mnu_itm_aligner;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_X;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_Y;
		private ToolStripMenuItem m_mnu_itm_repartir;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_X;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_Y;
		private ToolStripSeparator m_mnu_sep0;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_X_Droite;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_X_Gauche;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_X_Auto;
		private ToolStripSeparator m_mnu_repartir_sep0;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_Y_Bas;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_Y_Haut;
		private ToolStripMenuItem m_mnu_itm_repartir_sur_Y_Auto;
		private ToolStripSeparator m_mnu_repartir_sep1;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_X_Haut;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_X_Centre;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_X_Bas;
		private ToolStripSeparator m_mnu_aligner_sep0;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_Y_Gauche;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_Y_Centre;
		private ToolStripMenuItem m_mnu_itm_aligner_sur_Y_Droite;
		private ToolStripSeparator m_mnu_sep1;
		private ToolStripMenuItem m_mnu_itm_resize;
		private ToolStripMenuItem m_mnu_itm_resize_largeur;
		private ToolStripMenuItem m_mnu_itm_resize_hauteur;
		private ToolStripSeparator m_mnu_sep2;
		private ToolStripMenuItem m_mnu_itm_delete;
		private ToolStripMenuItem m_mnu_itm_lock;
		private ToolStripMenuItem m_mnu_itm_grille;
		private ToolStripMenuItem m_mnu_itm_grille_tjrsAligner;
		private ToolStripMenuItem m_mnu_itm_grille_affichage;
		private ToolStripMenuItem m_mnu_itm_grille_affichage_jamais;
		private ToolStripMenuItem m_mnu_itm_grille_affichage_tjrs;
		private ToolStripMenuItem m_mnu_itm_grille_affichage_deplacement;
		private ToolStripSeparator m_mnu_aligner_sep1;
		private ToolStripMenuItem m_mnu_itm_aligner_correspondre_sur_Grille;
		private ToolStripMenuItem m_mnu_itm_grille_representation;
		private ToolStripMenuItem m_mnu_itm_grille_representation_lignes;
		private ToolStripMenuItem m_mnu_itm_grille_representation_points;
		private ToolStripMenuItem m_mnu_itm_grille_representation_angles;
		private ToolStripMenuItem m_mnu_itm_grille_representation_pointillets;
		private ToolStripMenuItem m_mnu_itm_grille_representation_discontinues;
		private ToolStripMenuItem m_mnu_itm_aligner_etendre_sur_Grille;
		private ToolStripMenuItem m_mnu_itm_aligner_positionner_sur_Grille;
		private ToolStripMenuItem m_mnu_itm_grille_taille;
		private ToolStripMenuItem m_mnu_itm_marge;
		private ToolStripMenuItem m_mnu_itm_grille_couleur;
        private ToolStripMenuItem m_mnu_itm_zoomIn;
        private ToolStripMenuItem m_mnu_itm_zoomOut;
        private ToolStripMenuItem m_mnu_itm_zoomAdjust;
        private ToolStripMenuItem m_mnu_itm_zoomOrigine;
        private ToolStripMenuItem m_mnu_itm_cut;
        private ToolStripMenuItem m_mnu_itm_copy;
        private ToolStripMenuItem m_mnu_itm_paste;
		private ColorDialog m_colorDialog;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem2;
        private Timer m_timerRefresh;
        private ToolTip m_tooltip;
        private Timer m_timerTooltip;
		private IContainer components;


		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
            if (m_timerRefresh != null)
                m_timerRefresh.Stop();
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
this.components = new System.ComponentModel.Container();
this.m_mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
this.m_mnu_itm_zoomIn = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_zoomOut = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_zoomAdjust = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_zoomOrigine = new System.Windows.Forms.ToolStripMenuItem();
this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_bringToFront = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_bringToBack = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_sep0 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_aligner = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_X = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_X_Haut = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_X_Centre = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_X_Bas = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_aligner_sep0 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_aligner_sur_Y = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_Y_Gauche = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_Y_Centre = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_sur_Y_Droite = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_aligner_sep1 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_aligner_correspondre_sur_Grille = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_positionner_sur_Grille = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_aligner_etendre_sur_Grille = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_X = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_X_Auto = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_X_Droite = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_X_Gauche = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_repartir_sep0 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_repartir_sur_Y = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_Y_Auto = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_Y_Bas = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_repartir_sur_Y_Haut = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_repartir_sep1 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_marge = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_sep1 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_resize = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_resize_largeur = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_resize_hauteur = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_sep2 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_lock = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_delete = new System.Windows.Forms.ToolStripMenuItem();
this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
this.m_mnu_itm_cut = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_copy = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_paste = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_tjrsAligner = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_affichage = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_affichage_jamais = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_affichage_tjrs = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_affichage_deplacement = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation_lignes = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation_pointillets = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation_discontinues = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation_angles = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_representation_points = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_taille = new System.Windows.Forms.ToolStripMenuItem();
this.m_mnu_itm_grille_couleur = new System.Windows.Forms.ToolStripMenuItem();
this.m_colorDialog = new System.Windows.Forms.ColorDialog();
this.m_timerRefresh = new System.Windows.Forms.Timer(this.components);
this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
this.m_timerTooltip = new System.Windows.Forms.Timer(this.components);
this.m_mnu.SuspendLayout();
this.SuspendLayout();
// 
// m_mnu
// 
this.m_mnu.BackColor = System.Drawing.Color.White;
this.m_mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_zoomIn,
            this.m_mnu_itm_zoomOut,
            this.m_mnu_itm_zoomAdjust,
            this.m_mnu_itm_zoomOrigine,
            this.toolStripMenuItem1,
            this.m_mnu_itm_bringToFront,
            this.m_mnu_itm_bringToBack,
            this.m_mnu_sep0,
            this.m_mnu_itm_aligner,
            this.m_mnu_itm_repartir,
            this.m_mnu_sep1,
            this.m_mnu_itm_resize,
            this.m_mnu_itm_resize_largeur,
            this.m_mnu_itm_resize_hauteur,
            this.m_mnu_sep2,
            this.m_mnu_itm_lock,
            this.m_mnu_itm_delete,
            this.toolStripMenuItem2,
            this.m_mnu_itm_cut,
            this.m_mnu_itm_copy,
            this.m_mnu_itm_paste,
            this.m_mnu_itm_grille});
this.m_mnu.Name = "m_mnu";
this.m_mnu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
this.m_mnu.Size = new System.Drawing.Size(206, 408);
this.m_mnu.Opening += new System.ComponentModel.CancelEventHandler(this.m_mnu_Opening);
this.m_mnu.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.m_mnu_Closing);
// 
// m_mnu_itm_zoomIn
// 
this.m_mnu_itm_zoomIn.Name = "m_mnu_itm_zoomIn";
this.m_mnu_itm_zoomIn.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_zoomIn.Text = "Zoom In|30039";
this.m_mnu_itm_zoomIn.Click += new System.EventHandler(this.m_mnu_itm_zoomIn_Click);
// 
// m_mnu_itm_zoomOut
// 
this.m_mnu_itm_zoomOut.Name = "m_mnu_itm_zoomOut";
this.m_mnu_itm_zoomOut.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_zoomOut.Text = "Zoom Out|30040";
this.m_mnu_itm_zoomOut.Click += new System.EventHandler(this.m_mnu_itm_zoomOut_Click);
// 
// m_mnu_itm_zoomAdjust
// 
this.m_mnu_itm_zoomAdjust.Name = "m_mnu_itm_zoomAdjust";
this.m_mnu_itm_zoomAdjust.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_zoomAdjust.Text = "Adjust Zoom|30041";
this.m_mnu_itm_zoomAdjust.Click += new System.EventHandler(this.m_mnu_itm_zoomAdjust_Click);
// 
// m_mnu_itm_zoomOrigine
// 
this.m_mnu_itm_zoomOrigine.Name = "m_mnu_itm_zoomOrigine";
this.m_mnu_itm_zoomOrigine.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_zoomOrigine.Text = "Original Size|30045";
this.m_mnu_itm_zoomOrigine.Click += new System.EventHandler(this.m_mnu_itm_zoomOrigine_Click);
// 
// toolStripMenuItem1
// 
this.toolStripMenuItem1.Name = "toolStripMenuItem1";
this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
// 
// m_mnu_itm_bringToFront
// 
this.m_mnu_itm_bringToFront.Image = global::sc2i.win32.common.Properties.Resources.bring_to_front;
this.m_mnu_itm_bringToFront.Name = "m_mnu_itm_bringToFront";
this.m_mnu_itm_bringToFront.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_bringToFront.Text = "Put in foreground|30000";
this.m_mnu_itm_bringToFront.Click += new System.EventHandler(this.m_mnu_itm_bringToFront_Click);
// 
// m_mnu_itm_bringToBack
// 
this.m_mnu_itm_bringToBack.Image = global::sc2i.win32.common.Properties.Resources.front_to_back;
this.m_mnu_itm_bringToBack.Name = "m_mnu_itm_bringToBack";
this.m_mnu_itm_bringToBack.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_bringToBack.Text = "Put in background|30001";
this.m_mnu_itm_bringToBack.Click += new System.EventHandler(this.m_mnu_itm_bringToBack_Click);
// 
// m_mnu_sep0
// 
this.m_mnu_sep0.Name = "m_mnu_sep0";
this.m_mnu_sep0.Size = new System.Drawing.Size(202, 6);
// 
// m_mnu_itm_aligner
// 
this.m_mnu_itm_aligner.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_aligner_sur_X,
            this.m_mnu_itm_aligner_sur_X_Haut,
            this.m_mnu_itm_aligner_sur_X_Centre,
            this.m_mnu_itm_aligner_sur_X_Bas,
            this.m_mnu_aligner_sep0,
            this.m_mnu_itm_aligner_sur_Y,
            this.m_mnu_itm_aligner_sur_Y_Gauche,
            this.m_mnu_itm_aligner_sur_Y_Centre,
            this.m_mnu_itm_aligner_sur_Y_Droite,
            this.m_mnu_aligner_sep1,
            this.m_mnu_itm_aligner_correspondre_sur_Grille,
            this.m_mnu_itm_aligner_positionner_sur_Grille,
            this.m_mnu_itm_aligner_etendre_sur_Grille});
this.m_mnu_itm_aligner.Image = global::sc2i.win32.common.Properties.Resources.aligner;
this.m_mnu_itm_aligner.Name = "m_mnu_itm_aligner";
this.m_mnu_itm_aligner.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_aligner.Text = "Align|30002";
// 
// m_mnu_itm_aligner_sur_X
// 
this.m_mnu_itm_aligner_sur_X.ForeColor = System.Drawing.SystemColors.Desktop;
this.m_mnu_itm_aligner_sur_X.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_X;
this.m_mnu_itm_aligner_sur_X.Name = "m_mnu_itm_aligner_sur_X";
this.m_mnu_itm_aligner_sur_X.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
this.m_mnu_itm_aligner_sur_X.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_X.Text = "Horizontally|30003";
// 
// m_mnu_itm_aligner_sur_X_Haut
// 
this.m_mnu_itm_aligner_sur_X_Haut.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_X_en_haut;
this.m_mnu_itm_aligner_sur_X_Haut.Name = "m_mnu_itm_aligner_sur_X_Haut";
this.m_mnu_itm_aligner_sur_X_Haut.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_X_Haut.Text = "Top|30004";
this.m_mnu_itm_aligner_sur_X_Haut.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_X_Haut_Click);
// 
// m_mnu_itm_aligner_sur_X_Centre
// 
this.m_mnu_itm_aligner_sur_X_Centre.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_X_au_centre;
this.m_mnu_itm_aligner_sur_X_Centre.Name = "m_mnu_itm_aligner_sur_X_Centre";
this.m_mnu_itm_aligner_sur_X_Centre.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_X_Centre.Text = "Center|30005";
this.m_mnu_itm_aligner_sur_X_Centre.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_X_Centre_Click);
// 
// m_mnu_itm_aligner_sur_X_Bas
// 
this.m_mnu_itm_aligner_sur_X_Bas.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_X_en_bas;
this.m_mnu_itm_aligner_sur_X_Bas.Name = "m_mnu_itm_aligner_sur_X_Bas";
this.m_mnu_itm_aligner_sur_X_Bas.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_X_Bas.Text = "Bottom|30006";
this.m_mnu_itm_aligner_sur_X_Bas.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_X_Bas_Click);
// 
// m_mnu_aligner_sep0
// 
this.m_mnu_aligner_sep0.Name = "m_mnu_aligner_sep0";
this.m_mnu_aligner_sep0.Size = new System.Drawing.Size(189, 6);
// 
// m_mnu_itm_aligner_sur_Y
// 
this.m_mnu_itm_aligner_sur_Y.ForeColor = System.Drawing.SystemColors.Desktop;
this.m_mnu_itm_aligner_sur_Y.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_Y;
this.m_mnu_itm_aligner_sur_Y.Name = "m_mnu_itm_aligner_sur_Y";
this.m_mnu_itm_aligner_sur_Y.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
this.m_mnu_itm_aligner_sur_Y.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_Y.Text = "Vertically|30007";
// 
// m_mnu_itm_aligner_sur_Y_Gauche
// 
this.m_mnu_itm_aligner_sur_Y_Gauche.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_Y_a_gauche;
this.m_mnu_itm_aligner_sur_Y_Gauche.Name = "m_mnu_itm_aligner_sur_Y_Gauche";
this.m_mnu_itm_aligner_sur_Y_Gauche.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_Y_Gauche.Text = "Left|30008";
this.m_mnu_itm_aligner_sur_Y_Gauche.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_Y_Gauche_Click);
// 
// m_mnu_itm_aligner_sur_Y_Centre
// 
this.m_mnu_itm_aligner_sur_Y_Centre.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_Y_au_centre;
this.m_mnu_itm_aligner_sur_Y_Centre.Name = "m_mnu_itm_aligner_sur_Y_Centre";
this.m_mnu_itm_aligner_sur_Y_Centre.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_Y_Centre.Text = "Center|30005";
this.m_mnu_itm_aligner_sur_Y_Centre.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_Y_Centre_Click);
// 
// m_mnu_itm_aligner_sur_Y_Droite
// 
this.m_mnu_itm_aligner_sur_Y_Droite.Image = global::sc2i.win32.common.Properties.Resources.aligner_sur_Y_a_droite;
this.m_mnu_itm_aligner_sur_Y_Droite.Name = "m_mnu_itm_aligner_sur_Y_Droite";
this.m_mnu_itm_aligner_sur_Y_Droite.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_sur_Y_Droite.Text = "Right|30009";
this.m_mnu_itm_aligner_sur_Y_Droite.Click += new System.EventHandler(this.m_mnu_itm_aligner_sur_Y_Droite_Click);
// 
// m_mnu_aligner_sep1
// 
this.m_mnu_aligner_sep1.Name = "m_mnu_aligner_sep1";
this.m_mnu_aligner_sep1.Size = new System.Drawing.Size(189, 6);
// 
// m_mnu_itm_aligner_correspondre_sur_Grille
// 
this.m_mnu_itm_aligner_correspondre_sur_Grille.Image = global::sc2i.win32.common.Properties.Resources.aligner_correspondre_sur_grille;
this.m_mnu_itm_aligner_correspondre_sur_Grille.Name = "m_mnu_itm_aligner_correspondre_sur_Grille";
this.m_mnu_itm_aligner_correspondre_sur_Grille.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_correspondre_sur_Grille.Text = "Match grid|30010";
this.m_mnu_itm_aligner_correspondre_sur_Grille.Click += new System.EventHandler(this.m_mnu_itm_aligner_correspondre_sur_Grille_Click);
// 
// m_mnu_itm_aligner_positionner_sur_Grille
// 
this.m_mnu_itm_aligner_positionner_sur_Grille.Image = global::sc2i.win32.common.Properties.Resources.aligner_positionner_sur_grille;
this.m_mnu_itm_aligner_positionner_sur_Grille.Name = "m_mnu_itm_aligner_positionner_sur_Grille";
this.m_mnu_itm_aligner_positionner_sur_Grille.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_positionner_sur_Grille.Text = "Position on grid|30011";
this.m_mnu_itm_aligner_positionner_sur_Grille.Click += new System.EventHandler(this.m_mnu_itm_aligner_positionner_sur_Grille_Click);
// 
// m_mnu_itm_aligner_etendre_sur_Grille
// 
this.m_mnu_itm_aligner_etendre_sur_Grille.Image = global::sc2i.win32.common.Properties.Resources.aligner_etendre_sur_grille;
this.m_mnu_itm_aligner_etendre_sur_Grille.Name = "m_mnu_itm_aligner_etendre_sur_Grille";
this.m_mnu_itm_aligner_etendre_sur_Grille.Size = new System.Drawing.Size(192, 22);
this.m_mnu_itm_aligner_etendre_sur_Grille.Text = "Stretch on grid|30012";
this.m_mnu_itm_aligner_etendre_sur_Grille.Click += new System.EventHandler(this.m_mnu_itm_aligner_etendre_sur_Grille_Click);
// 
// m_mnu_itm_repartir
// 
this.m_mnu_itm_repartir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_repartir_sur_X,
            this.m_mnu_itm_repartir_sur_X_Auto,
            this.m_mnu_itm_repartir_sur_X_Droite,
            this.m_mnu_itm_repartir_sur_X_Gauche,
            this.m_mnu_repartir_sep0,
            this.m_mnu_itm_repartir_sur_Y,
            this.m_mnu_itm_repartir_sur_Y_Auto,
            this.m_mnu_itm_repartir_sur_Y_Bas,
            this.m_mnu_itm_repartir_sur_Y_Haut,
            this.m_mnu_repartir_sep1,
            this.m_mnu_itm_marge});
this.m_mnu_itm_repartir.Image = global::sc2i.win32.common.Properties.Resources.repartir;
this.m_mnu_itm_repartir.Name = "m_mnu_itm_repartir";
this.m_mnu_itm_repartir.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_repartir.Text = "Arrange|30013";
// 
// m_mnu_itm_repartir_sur_X
// 
this.m_mnu_itm_repartir_sur_X.ForeColor = System.Drawing.SystemColors.Desktop;
this.m_mnu_itm_repartir_sur_X.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_X;
this.m_mnu_itm_repartir_sur_X.Name = "m_mnu_itm_repartir_sur_X";
this.m_mnu_itm_repartir_sur_X.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
this.m_mnu_itm_repartir_sur_X.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_X.Text = "Horizontally|30003";
this.m_mnu_itm_repartir_sur_X.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
// 
// m_mnu_itm_repartir_sur_X_Auto
// 
this.m_mnu_itm_repartir_sur_X_Auto.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_X_auto;
this.m_mnu_itm_repartir_sur_X_Auto.Name = "m_mnu_itm_repartir_sur_X_Auto";
this.m_mnu_itm_repartir_sur_X_Auto.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_X_Auto.Text = "Respect Spacing|30014";
this.m_mnu_itm_repartir_sur_X_Auto.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_X_Auto_Click);
// 
// m_mnu_itm_repartir_sur_X_Droite
// 
this.m_mnu_itm_repartir_sur_X_Droite.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_X_a_droite;
this.m_mnu_itm_repartir_sur_X_Droite.Name = "m_mnu_itm_repartir_sur_X_Droite";
this.m_mnu_itm_repartir_sur_X_Droite.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_X_Droite.Text = "To the Right|30015";
this.m_mnu_itm_repartir_sur_X_Droite.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_X_Droite_Click);
// 
// m_mnu_itm_repartir_sur_X_Gauche
// 
this.m_mnu_itm_repartir_sur_X_Gauche.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_X_a_gauche;
this.m_mnu_itm_repartir_sur_X_Gauche.Name = "m_mnu_itm_repartir_sur_X_Gauche";
this.m_mnu_itm_repartir_sur_X_Gauche.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_X_Gauche.Text = "To the Left|30016";
this.m_mnu_itm_repartir_sur_X_Gauche.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_X_Gauche_Click);
// 
// m_mnu_repartir_sep0
// 
this.m_mnu_repartir_sep0.Name = "m_mnu_repartir_sep0";
this.m_mnu_repartir_sep0.Size = new System.Drawing.Size(195, 6);
// 
// m_mnu_itm_repartir_sur_Y
// 
this.m_mnu_itm_repartir_sur_Y.ForeColor = System.Drawing.SystemColors.Desktop;
this.m_mnu_itm_repartir_sur_Y.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_Y;
this.m_mnu_itm_repartir_sur_Y.Name = "m_mnu_itm_repartir_sur_Y";
this.m_mnu_itm_repartir_sur_Y.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
this.m_mnu_itm_repartir_sur_Y.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_Y.Text = "Vertically|30007";
this.m_mnu_itm_repartir_sur_Y.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
// 
// m_mnu_itm_repartir_sur_Y_Auto
// 
this.m_mnu_itm_repartir_sur_Y_Auto.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_Y_auto;
this.m_mnu_itm_repartir_sur_Y_Auto.Name = "m_mnu_itm_repartir_sur_Y_Auto";
this.m_mnu_itm_repartir_sur_Y_Auto.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_Y_Auto.Text = "Respect Spacing|30014";
this.m_mnu_itm_repartir_sur_Y_Auto.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_Y_Auto_Click);
// 
// m_mnu_itm_repartir_sur_Y_Bas
// 
this.m_mnu_itm_repartir_sur_Y_Bas.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_Y_en_bas;
this.m_mnu_itm_repartir_sur_Y_Bas.Name = "m_mnu_itm_repartir_sur_Y_Bas";
this.m_mnu_itm_repartir_sur_Y_Bas.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_Y_Bas.Text = "Down|30017";
this.m_mnu_itm_repartir_sur_Y_Bas.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_Y_Bas_Click);
// 
// m_mnu_itm_repartir_sur_Y_Haut
// 
this.m_mnu_itm_repartir_sur_Y_Haut.Image = global::sc2i.win32.common.Properties.Resources.repartir_sur_Y_en_haut;
this.m_mnu_itm_repartir_sur_Y_Haut.Name = "m_mnu_itm_repartir_sur_Y_Haut";
this.m_mnu_itm_repartir_sur_Y_Haut.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_repartir_sur_Y_Haut.Text = "Up|30018";
this.m_mnu_itm_repartir_sur_Y_Haut.Click += new System.EventHandler(this.m_mnu_itm_repartir_sur_Y_Haut_Click);
// 
// m_mnu_repartir_sep1
// 
this.m_mnu_repartir_sep1.Name = "m_mnu_repartir_sep1";
this.m_mnu_repartir_sep1.Size = new System.Drawing.Size(195, 6);
// 
// m_mnu_itm_marge
// 
this.m_mnu_itm_marge.Image = global::sc2i.win32.common.Properties.Resources.marge;
this.m_mnu_itm_marge.Name = "m_mnu_itm_marge";
this.m_mnu_itm_marge.Size = new System.Drawing.Size(198, 22);
this.m_mnu_itm_marge.Text = "Margin...|30019";
this.m_mnu_itm_marge.Click += new System.EventHandler(this.m_mnu_itm_marge_Click);
// 
// m_mnu_sep1
// 
this.m_mnu_sep1.Name = "m_mnu_sep1";
this.m_mnu_sep1.Size = new System.Drawing.Size(202, 6);
// 
// m_mnu_itm_resize
// 
this.m_mnu_itm_resize.Image = global::sc2i.win32.common.Properties.Resources.generaliser_taille;
this.m_mnu_itm_resize.Name = "m_mnu_itm_resize";
this.m_mnu_itm_resize.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_resize.Text = "Generalize Size|30020";
this.m_mnu_itm_resize.Click += new System.EventHandler(this.m_mnu_itm_resize_Click);
// 
// m_mnu_itm_resize_largeur
// 
this.m_mnu_itm_resize_largeur.Image = global::sc2i.win32.common.Properties.Resources.generaliser_largeur;
this.m_mnu_itm_resize_largeur.Name = "m_mnu_itm_resize_largeur";
this.m_mnu_itm_resize_largeur.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_resize_largeur.Text = "Generalize Width|30021";
this.m_mnu_itm_resize_largeur.Click += new System.EventHandler(this.m_mnu_itm_resize_largeur_Click);
// 
// m_mnu_itm_resize_hauteur
// 
this.m_mnu_itm_resize_hauteur.Image = global::sc2i.win32.common.Properties.Resources.generaliser_hauteur;
this.m_mnu_itm_resize_hauteur.Name = "m_mnu_itm_resize_hauteur";
this.m_mnu_itm_resize_hauteur.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_resize_hauteur.Text = "Generalize Height|30022";
this.m_mnu_itm_resize_hauteur.Click += new System.EventHandler(this.m_mnu_itm_resize_hauteur_Click);
// 
// m_mnu_sep2
// 
this.m_mnu_sep2.Name = "m_mnu_sep2";
this.m_mnu_sep2.Size = new System.Drawing.Size(202, 6);
// 
// m_mnu_itm_lock
// 
this.m_mnu_itm_lock.Image = global::sc2i.win32.common.Properties.Resources._lock;
this.m_mnu_itm_lock.Name = "m_mnu_itm_lock";
this.m_mnu_itm_lock.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_lock.Text = "Lock|30023";
this.m_mnu_itm_lock.Click += new System.EventHandler(this.m_mnu_itm_lock_Click);
// 
// m_mnu_itm_delete
// 
this.m_mnu_itm_delete.Image = global::sc2i.win32.common.Properties.Resources.Supprimer;
this.m_mnu_itm_delete.Name = "m_mnu_itm_delete";
this.m_mnu_itm_delete.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_delete.Text = "Delete|30024";
this.m_mnu_itm_delete.Click += new System.EventHandler(this.m_mnu_itm_delete_Click);
// 
// toolStripMenuItem2
// 
this.toolStripMenuItem2.Name = "toolStripMenuItem2";
this.toolStripMenuItem2.Size = new System.Drawing.Size(202, 6);
// 
// m_mnu_itm_cut
// 
this.m_mnu_itm_cut.Name = "m_mnu_itm_cut";
this.m_mnu_itm_cut.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_cut.Text = "Cut|30042";
this.m_mnu_itm_cut.Click += new System.EventHandler(this.m_mnu_itm_cut_Click);
// 
// m_mnu_itm_copy
// 
this.m_mnu_itm_copy.Name = "m_mnu_itm_copy";
this.m_mnu_itm_copy.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_copy.Text = "Copy|30043";
this.m_mnu_itm_copy.Click += new System.EventHandler(this.m_mnu_itm_copy_Click);
// 
// m_mnu_itm_paste
// 
this.m_mnu_itm_paste.Name = "m_mnu_itm_paste";
this.m_mnu_itm_paste.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_paste.Text = "Paste|30044";
this.m_mnu_itm_paste.Click += new System.EventHandler(this.m_mnu_itm_paste_Click);
// 
// m_mnu_itm_grille
// 
this.m_mnu_itm_grille.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_grille_tjrsAligner,
            this.m_mnu_itm_grille_affichage,
            this.m_mnu_itm_grille_representation,
            this.m_mnu_itm_grille_taille,
            this.m_mnu_itm_grille_couleur});
this.m_mnu_itm_grille.Image = global::sc2i.win32.common.Properties.Resources.grille;
this.m_mnu_itm_grille.Name = "m_mnu_itm_grille";
this.m_mnu_itm_grille.Size = new System.Drawing.Size(205, 22);
this.m_mnu_itm_grille.Text = "Grid|30025";
// 
// m_mnu_itm_grille_tjrsAligner
// 
this.m_mnu_itm_grille_tjrsAligner.Name = "m_mnu_itm_grille_tjrsAligner";
this.m_mnu_itm_grille_tjrsAligner.Size = new System.Drawing.Size(215, 22);
this.m_mnu_itm_grille_tjrsAligner.Text = "Always align on Grid|30026";
this.m_mnu_itm_grille_tjrsAligner.Click += new System.EventHandler(this.m_mnu_itm_grille_tjrsAligner_Click);
// 
// m_mnu_itm_grille_affichage
// 
this.m_mnu_itm_grille_affichage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_grille_affichage_jamais,
            this.m_mnu_itm_grille_affichage_tjrs,
            this.m_mnu_itm_grille_affichage_deplacement});
this.m_mnu_itm_grille_affichage.Image = global::sc2i.win32.common.Properties.Resources.grille_affichage;
this.m_mnu_itm_grille_affichage.Name = "m_mnu_itm_grille_affichage";
this.m_mnu_itm_grille_affichage.Size = new System.Drawing.Size(215, 22);
this.m_mnu_itm_grille_affichage.Text = "Display Grid|30027";
// 
// m_mnu_itm_grille_affichage_jamais
// 
this.m_mnu_itm_grille_affichage_jamais.Name = "m_mnu_itm_grille_affichage_jamais";
this.m_mnu_itm_grille_affichage_jamais.Size = new System.Drawing.Size(184, 22);
this.m_mnu_itm_grille_affichage_jamais.Text = "Never|30028";
this.m_mnu_itm_grille_affichage_jamais.Click += new System.EventHandler(this.m_mnu_itm_grille_affichage_jamais_Click);
// 
// m_mnu_itm_grille_affichage_tjrs
// 
this.m_mnu_itm_grille_affichage_tjrs.Name = "m_mnu_itm_grille_affichage_tjrs";
this.m_mnu_itm_grille_affichage_tjrs.Size = new System.Drawing.Size(184, 22);
this.m_mnu_itm_grille_affichage_tjrs.Text = "Always|30029";
this.m_mnu_itm_grille_affichage_tjrs.Click += new System.EventHandler(this.m_mnu_itm_grille_affichage_tjrs_Click);
// 
// m_mnu_itm_grille_affichage_deplacement
// 
this.m_mnu_itm_grille_affichage_deplacement.Name = "m_mnu_itm_grille_affichage_deplacement";
this.m_mnu_itm_grille_affichage_deplacement.Size = new System.Drawing.Size(184, 22);
this.m_mnu_itm_grille_affichage_deplacement.Text = "When moving|30030";
this.m_mnu_itm_grille_affichage_deplacement.Click += new System.EventHandler(this.m_mnu_itm_grille_affichage_deplacement_Click);
// 
// m_mnu_itm_grille_representation
// 
this.m_mnu_itm_grille_representation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_itm_grille_representation_lignes,
            this.m_mnu_itm_grille_representation_pointillets,
            this.m_mnu_itm_grille_representation_discontinues,
            this.m_mnu_itm_grille_representation_angles,
            this.m_mnu_itm_grille_representation_points});
this.m_mnu_itm_grille_representation.Image = global::sc2i.win32.common.Properties.Resources.grille_representation;
this.m_mnu_itm_grille_representation.Name = "m_mnu_itm_grille_representation";
this.m_mnu_itm_grille_representation.Size = new System.Drawing.Size(215, 22);
this.m_mnu_itm_grille_representation.Text = "Grid Type|30031";
// 
// m_mnu_itm_grille_representation_lignes
// 
this.m_mnu_itm_grille_representation_lignes.Image = global::sc2i.win32.common.Properties.Resources.grille_lignes;
this.m_mnu_itm_grille_representation_lignes.Name = "m_mnu_itm_grille_representation_lignes";
this.m_mnu_itm_grille_representation_lignes.Size = new System.Drawing.Size(179, 22);
this.m_mnu_itm_grille_representation_lignes.Text = "Lines|30032";
this.m_mnu_itm_grille_representation_lignes.Click += new System.EventHandler(this.m_mnu_itm_grille_representation_lignes_Click);
// 
// m_mnu_itm_grille_representation_pointillets
// 
this.m_mnu_itm_grille_representation_pointillets.Image = global::sc2i.win32.common.Properties.Resources.grille_pointillet;
this.m_mnu_itm_grille_representation_pointillets.Name = "m_mnu_itm_grille_representation_pointillets";
this.m_mnu_itm_grille_representation_pointillets.Size = new System.Drawing.Size(179, 22);
this.m_mnu_itm_grille_representation_pointillets.Text = "Dotted lines|30033";
this.m_mnu_itm_grille_representation_pointillets.Click += new System.EventHandler(this.m_mnu_itm_grille_representation_pointillets_Click);
// 
// m_mnu_itm_grille_representation_discontinues
// 
this.m_mnu_itm_grille_representation_discontinues.Image = global::sc2i.win32.common.Properties.Resources.grille_discontinue;
this.m_mnu_itm_grille_representation_discontinues.Name = "m_mnu_itm_grille_representation_discontinues";
this.m_mnu_itm_grille_representation_discontinues.Size = new System.Drawing.Size(179, 22);
this.m_mnu_itm_grille_representation_discontinues.Text = "Dashed lines|30034";
this.m_mnu_itm_grille_representation_discontinues.Click += new System.EventHandler(this.m_mnu_itm_grille_representation_discontinues_Click);
// 
// m_mnu_itm_grille_representation_angles
// 
this.m_mnu_itm_grille_representation_angles.Image = global::sc2i.win32.common.Properties.Resources.grille_angles;
this.m_mnu_itm_grille_representation_angles.Name = "m_mnu_itm_grille_representation_angles";
this.m_mnu_itm_grille_representation_angles.Size = new System.Drawing.Size(179, 22);
this.m_mnu_itm_grille_representation_angles.Text = "Angles|30035";
this.m_mnu_itm_grille_representation_angles.Click += new System.EventHandler(this.m_mnu_itm_grille_representation_angles_Click);
// 
// m_mnu_itm_grille_representation_points
// 
this.m_mnu_itm_grille_representation_points.Image = global::sc2i.win32.common.Properties.Resources.grille_points;
this.m_mnu_itm_grille_representation_points.Name = "m_mnu_itm_grille_representation_points";
this.m_mnu_itm_grille_representation_points.Size = new System.Drawing.Size(179, 22);
this.m_mnu_itm_grille_representation_points.Text = "Points|30036";
this.m_mnu_itm_grille_representation_points.Click += new System.EventHandler(this.m_mnu_itm_grille_representation_points_Click);
// 
// m_mnu_itm_grille_taille
// 
this.m_mnu_itm_grille_taille.Image = global::sc2i.win32.common.Properties.Resources.grille_taille;
this.m_mnu_itm_grille_taille.Name = "m_mnu_itm_grille_taille";
this.m_mnu_itm_grille_taille.Size = new System.Drawing.Size(215, 22);
this.m_mnu_itm_grille_taille.Text = "Size...|30037";
this.m_mnu_itm_grille_taille.Click += new System.EventHandler(this.m_mnu_itm_grille_taille_Click);
// 
// m_mnu_itm_grille_couleur
// 
this.m_mnu_itm_grille_couleur.Image = global::sc2i.win32.common.Properties.Resources.grille_color;
this.m_mnu_itm_grille_couleur.Name = "m_mnu_itm_grille_couleur";
this.m_mnu_itm_grille_couleur.Size = new System.Drawing.Size(215, 22);
this.m_mnu_itm_grille_couleur.Text = "Color...|30038";
this.m_mnu_itm_grille_couleur.Click += new System.EventHandler(this.m_mnu_itm_grille_couleur_Click);
// 
// m_timerRefresh
// 
this.m_timerRefresh.Tick += new System.EventHandler(this.m_timerRefresh_Tick);
// 
// m_timerTooltip
// 
this.m_timerTooltip.Interval = 500;
this.m_timerTooltip.Tick += new System.EventHandler(this.m_timerTooltip_Tick);
// 
// CPanelEditionObjetGraphique
// 
this.AllowDrop = true;
this.DoubleBuffered = true;
this.Name = "CPanelEditionObjetGraphique";
this.Size = new System.Drawing.Size(532, 316);
this.Load += new System.EventHandler(this.Editeur_Load);
this.DoubleClick += new System.EventHandler(this.Editeur_DoubleClick);
this.Paint += new System.Windows.Forms.PaintEventHandler(this.Editeur_Paint);
this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Editeur_PreviewKeyDown);
this.DragOver += new System.Windows.Forms.DragEventHandler(this.Editeur_DragOver);
this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Editeur_MouseMove);
this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Editeur_DragDrop);
this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CPanelEditionObjetGraphique_KeyUp);
this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Editeur_MouseDown);
this.DragLeave += new System.EventHandler(this.Editeur_DragLeave);
this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Editeur_MouseUp);
this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Editeur_DragEnter);
this.SizeChanged += new System.EventHandler(this.Editeur_SizeChanged);
this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editeur_KeyDown);
this.m_mnu.ResumeLayout(false);
this.ResumeLayout(false);

		}

        
		#endregion


		/// //////////////////////////////////////////////////
		public event EventHandler AfterRemoveObjetGraphique;
		public event EventHandlerPanelEditionGraphiqueSuppression BeforeDeleteElement;
		public event EventHandlerPanelEditionGraphiqueSuppression AfterAddElements;
        public event EventHandler ElementMovedOrResized;
        public event EventHandler FrontBackChanged;

        public void DeclencheAfterAddElements(List<I2iObjetGraphique> lst)
        {
            if (AfterAddElements != null)
                AfterAddElements(lst);
        }

        
        private bool m_bNoDelete=false;
        private bool m_bNoClipboard=false;
        private bool m_bNoMenu=false;
        private bool m_bNoDoubleClick = false;

        private bool m_bSelectionVisible = true;

        private bool m_bIsZooming = false;

        private float m_fminZoom = 0.2F;
        private float m_fmaxZoom = 6.0F;
        

        private CRectangleSelection m_rectangleSelection;

        //----------------------------------------
        public float MaxZoom
        {
            get
            {
                return m_fmaxZoom;
            }
            set
            {
                m_fmaxZoom = value;
            }
        }

        //----------------------------------------
        public float MinZoom
        {
            get
            {
                return m_fminZoom;
            }
            set
            {
                m_fminZoom = value;
            }
        }


        public bool SelectionVisible
        {
            get
            {
                return m_bSelectionVisible;

            }

            set
            {
                m_bSelectionVisible = value;

            }

        }

        public bool NoDelete
        {
            get
             {
                return m_bNoDelete;
             }
             set
             {
                 m_bNoDelete=value;
             }
        
		}

        public bool NoClipboard
        {
            get
            {
                return m_bNoClipboard;
            }
            set
            {
                m_bNoClipboard = value;
            }

        }


        public bool NoMenu
        {
            get
            {
                return m_bNoMenu;
            }
            set
            {
                m_bNoMenu = value;
            }

        }


        public bool NoDoubleClick
        {
            get
            {
                return m_bNoDoubleClick;
            }
            set
            {
                m_bNoDoubleClick = value;
            }

        }



       

        private bool m_bDragRectangleZoom = false;


        /// //////////////////////////////////////////////////
        ///Si true, l'axe des x va de droite à gauche
        public virtual bool InverserAxeX
        {
            get
            {
                return false;
            }
        }

        /// //////////////////////////////////////////////////
        /// Si true, l'axe des y va de haut en bas
        public virtual bool InverserAxeY
        {
            get
            {
                return false;
            }
        }


		public CPanelEditionObjetGraphique()
		{
            m_strOrigineDragDropID = Guid.NewGuid().ToString();
			InitializeComponent();
		}

        /// //////////////////////////////////////////////////
        public string OrigineDragDropId
        {
            get
            {
                return m_strOrigineDragDropID;
            }
        }

		/// //////////////////////////////////////////////////
		private void Editeur_Load(object sender, System.EventArgs e)
		{
			m_rectsDrags = new List<CRectangleDragForObjetGraphique>();
			m_bitmap = CreateEmptyBitmap();
			Selection.SelectionChanged += new EventHandler(Selection_SelectionChanged);
			Selection.ElementsMovedOrResized += new EventHandler(Selection_ElementsResized);
			ToujoursAlignerSurLaGrille = false;
			ModeAffichageGrille = EModeAffichageGrille.AuDeplacement;
			ModeRepresentationGrille = ERepresentationGrille.LignesContinues;
			RefreshDelayed();
		}

		/// //////////////////////////////////////////////////
		private I2iObjetGraphique m_objetEdite = null;
		[System.ComponentModel.Browsable(false)]
		public I2iObjetGraphique ObjetEdite
		{
			get
			{
				return m_objetEdite;
			}
			set
			{
                if (DesignMode)
                    return;
				if (m_objetEdite != value)
				{
					m_objetEdite = value;
					RecalcScrollSize();
					m_selection.Clear();

					if ( m_objetEdite != null )
						m_objetEdite.SizeChanged += new EventHandlerObjetGraphique(ObjetEdite_ChangementTaille);
					//m_objetEdite.SizeChanged += new EventHandler(ObjetEdite_ChangementTaille);

					//if (m_bEffetFonduAjoutSupp)
					//{
					//    m_objetEdite.ChildAdded += new EventHandlerChild(EnfantAjoute);
					//}
					if (m_historique.Count == 0)
						ElementModifie();
					RefreshDelayed();
				}
			}
		}


        

		private void ObjetEdite_ChangementTaille(I2iObjetGraphique element)
		{
			RecalcScrollSize();
		}


		#region Sauvegardes CTRL-Z CTRL-Y

		private bool m_bHistorisationActive = true;
		public bool HistorisationActive
		{
			get
			{
				return m_bHistorisationActive && NombreHistorisation > 0;
			}
			set
			{
				m_bHistorisationActive = value;
			}
		}

		private int m_nbHistorisation = 10;
		public int NombreHistorisation
		{
			get
			{
				return m_nbHistorisation;
			}
			set
			{
				m_nbHistorisation = value;
			}
		}

		//PARAMETRAGE
		public void ChangementParametrage()
		{ }
		public void SauvegarderParametrage()
		{
		}
		public void ChargerParametrage()
		{
		}

		public void ElementModifie()
		{
			Selection.RecalcPositionPoignees();
			//if (HistorisationActive)
			//    Historiser();
			if (ElementMovedOrResized != null)
				ElementMovedOrResized(this, new EventArgs());
		}
		private int m_nPosHisto = 0;
		private List<Byte[]> m_historique = new List<Byte[]>();
		private void DeserialiserObjetEdite(int nPos)
		{
			Byte[] byt = m_historique[nPos];
			MemoryStream stream = new MemoryStream(byt);
			BinaryReader reader = new BinaryReader(stream);
			C2iSerializer serializer = new CSerializerReadBinaire(reader);
			stream.Seek(0, SeekOrigin.Begin);

			ObjetEdite.Serialize(serializer);
			reader.Close();
		}
		private void SerialiserObjetEdite()
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			C2iSerializer serializer = new CSerializerSaveBinaire(writer);
			CResultAErreur result = ObjetEdite.Serialize(serializer);
			m_historique.Add(stream.ToArray());
			writer.Close();
			while (m_historique.Count > NombreHistorisation)
			{
				m_historique.RemoveAt(0);
				m_nPosHisto--;
			}
		}
		public void Historiser()
		{
			if (NombreHistorisation == 0)
				return;
			if (CanRetablir)
				for (int n = m_historique.Count; n > m_nPosHisto; n--)
					m_historique.RemoveAt(n - 1);

			SerialiserObjetEdite();
			m_nPosHisto++;
		}
		public void Annuler()
		{
			if (!CanAnnuler)
				return;
			m_nPosHisto--;
			DeserialiserObjetEdite(m_nPosHisto - 1);
			Refresh();
		}
		public void Retablir()
		{
			if (!CanRetablir)
				return;

			m_nPosHisto++;
			DeserialiserObjetEdite(m_nPosHisto - 1);
			Refresh();
		}
		public bool CanAnnuler
		{
			get
			{
				return m_nPosHisto - 1 > 0;
			}
		}
		public bool CanRetablir
		{
			get
			{
				return m_nPosHisto < m_historique.Count;
			}
		}
		#endregion

		#region SELECTION


		public event EventHandler SelectionChanged;

		public CSelectionElementsGraphiques Selection
		{
			get
			{
				if (m_selection == null)
					m_selection = new CSelectionElementsGraphiques(this);
				return m_selection;
			}
		}

		public bool GrilleMustBeDraw
		{
			get
			{
				return (ModeAffichageGrille == EModeAffichageGrille.Toujours ||
				   (ModeAffichageGrille == EModeAffichageGrille.AuDeplacement && Selection.EnDeplacement || Selection.EnRedimentionnement));
			}
		}

		//EVENEMENTS SELECTION
		protected void Selection_ElementsResized(object sender, EventArgs e)
		{
			ElementModifie();
		}

		private bool m_bRefreshSelectionChanged = true;
		public bool RefreshSelectionChanged
		{
			get
			{
				return m_bRefreshSelectionChanged;
			}
			set
			{
				m_bRefreshSelectionChanged = value;
			}
		}
		protected void Selection_SelectionChanged(object obj, EventArgs args)
		{
            if (RefreshSelectionChanged)
                RefreshDelayed();

			if (SelectionChanged != null)
				SelectionChanged(obj, args);
		}

		#region Opérations sur Selection
		private void Selection_RedefinirTaille(Keys key)
		{
			if (LockEdition)
				return;
			bool bAlignerSurGrille = ModeAlignement;
			int ptStart;
			int nb = 1;
			int nWidth;
			int nHeight;
			List<I2iObjetGraphique> elements = Selection;// PrendreElementsDePremiersNiveau(Selection);
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj.IsLock)
					continue;
				switch (key)
				{
					case Keys.Down:
						nWidth = obj.RectangleAbsolu.Width;
						if (bAlignerSurGrille)
						{
							ptStart = obj.PositionAbsolue.Y + obj.RectangleAbsolu.Height;
							nHeight = obj.RectangleAbsolu.Height + (GetAfter(ptStart, EDimentionDessin.Y) - ptStart);
						}
						else
							nHeight = obj.RectangleAbsolu.Height + nb;
						break;

					case Keys.Up:
						nWidth = obj.RectangleAbsolu.Width;
						if (bAlignerSurGrille)
						{
							ptStart = obj.PositionAbsolue.Y + obj.RectangleAbsolu.Height;
							nHeight = obj.RectangleAbsolu.Height - (ptStart - GetBefore(ptStart, EDimentionDessin.Y));
						}
						else
							nHeight = obj.RectangleAbsolu.Height - nb;
						if (nHeight < HauteurMinimaleDesObjets)
							continue;
						break;

					case Keys.Right:
						if (bAlignerSurGrille)
						{
							ptStart = obj.PositionAbsolue.X + obj.RectangleAbsolu.Width;
							nWidth = obj.RectangleAbsolu.Width + (GetAfter(ptStart, EDimentionDessin.X) - ptStart);
						}
						else
							nWidth = obj.RectangleAbsolu.Width + nb;
						nHeight = obj.RectangleAbsolu.Height;
						break;
					case Keys.Left:
						if (bAlignerSurGrille)
						{
							ptStart = obj.PositionAbsolue.X + obj.RectangleAbsolu.Width;
							nWidth = obj.RectangleAbsolu.Width - (ptStart - GetBefore(ptStart, EDimentionDessin.X));
						}
						else
							nWidth = obj.RectangleAbsolu.Width - nb;

						if (nWidth < LargeurMinimaleDesObjets)
							continue;
						nHeight = obj.RectangleAbsolu.Height;
						break;
					default:
						return;
				}
				obj.Size = new Size(nWidth, nHeight);
			}
			ElementModifie();
		}
		private void Selection_SelectionnerLesFils()
		{
			if (LockEdition)
				return;
			List<I2iObjetGraphique> elements = new List<I2iObjetGraphique>();
			foreach (I2iObjetGraphique obj in Selection)
				elements.AddRange(Selection_SelectionnerLesFils(obj));

			Selection.AddRange(elements);
		}
		private List<I2iObjetGraphique> Selection_SelectionnerLesFils(I2iObjetGraphique pere)
		{
			List<I2iObjetGraphique> elements = new List<I2iObjetGraphique>();
			foreach (I2iObjetGraphique fils in pere.Childs)
			{
				if (!Selection.Contains(fils))
					elements.Add(fils);
				elements.AddRange(Selection_SelectionnerLesFils(fils));
			}
			return elements;
		}
		private void Selection_Suivant()
		{
			I2iObjetGraphique eleRef = Selection.ControlReference;

			if (Selection.Count > 1)
			{
				Selection.Remove(eleRef);
				Selection.Insert(0, eleRef);
			}
			else if (Selection.Count == 1 && Selection.ControlReference != ObjetEdite)
			{
				Selection.Clear();
				if (eleRef.Childs.Length > 0)
				{
					Selection.ControlReference = eleRef.Childs[0];
				}
				else
				{
					Selection_Parent(eleRef.Parent, eleRef);
				}
			}
			else
			{
				Selection.Clear();
				if (ObjetEdite.Childs.Length > 0)
					Selection.ControlReference = ObjetEdite.Childs[0];
				else
					Selection.ControlReference = ObjetEdite;
			}
		}
		private void Selection_Parent(I2iObjetGraphique eleParent, I2iObjetGraphique eleFilsSelec)
		{
			if (eleParent != null)
			{
				if (eleParent.Childs.Length > 1)
				{
					List<I2iObjetGraphique> enfantsDuParent = new List<I2iObjetGraphique>();
					foreach (I2iObjetGraphique enfant in eleParent.Childs)
						enfantsDuParent.Add(enfant);
					int idxEleActuel = enfantsDuParent.IndexOf(eleFilsSelec);
					if (idxEleActuel != enfantsDuParent.Count - 1)
					{
						Selection.ControlReference = enfantsDuParent[idxEleActuel + 1];
					}
					else
					{
						Selection_Parent(eleParent.Parent, eleParent);
					}
				}
				else
				{
					Selection.ControlReference = eleParent;
				}
			}
			else
			{
				//???
			}
		}

		private List<I2iObjetGraphique> PrendreElementsDePremiersNiveau(List<I2iObjetGraphique> selection)
		{
			List<I2iObjetGraphique> elements = new List<I2iObjetGraphique>();
			foreach (I2iObjetGraphique ele in selection)
			{
				I2iObjetGraphique eleFinal = ParentInSelection(selection, ele);
				if (eleFinal != null && !elements.Contains(eleFinal))
					elements.Add(eleFinal);
			}
			return elements;
		}
		private I2iObjetGraphique ParentInSelection(List<I2iObjetGraphique> elements, I2iObjetGraphique eleBase)
		{
			I2iObjetGraphique parent = eleBase.Parent;
			if (parent == null)
			{
				if (elements.Contains(eleBase))
					return eleBase;
			}
			else
			{
				I2iObjetGraphique parentMieuPlace = ParentInSelection(elements, parent);
				if (parentMieuPlace != null)
					return parentMieuPlace;
				else if (elements.Contains(parent))
					return parent;
				else if (elements.Contains(eleBase))
					return eleBase;
			}
			return null;
		}
		private void Selection_Bouger(Keys key)
		{
			if (LockEdition)
				return;
			bool bAlignerSurGrille = ModeAlignement;
			int nb = 1;
			int nY;
			int nX;
			List<I2iObjetGraphique> elements = Selection;// PrendreElementsDePremiersNiveau(Selection);
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj.IsLock||obj.NoMove)
					continue;

				switch (key)
				{
					case Keys.Down:
						nX = obj.PositionAbsolue.X;
						if (bAlignerSurGrille)
							nY = GetAfter(obj.PositionAbsolue.Y, EDimentionDessin.Y);
						else
							nY = obj.PositionAbsolue.Y + nb;
						break;

					case Keys.Up:
						nX = obj.PositionAbsolue.X;
						if (bAlignerSurGrille)
							nY = GetBefore(obj.PositionAbsolue.Y, EDimentionDessin.Y);
						else
							nY = obj.PositionAbsolue.Y - nb;
						break;

					case Keys.Right:
						if (bAlignerSurGrille)
							nX = GetAfter(obj.PositionAbsolue.X, EDimentionDessin.X);
						else
							nX = obj.PositionAbsolue.X + nb;
						nY = obj.PositionAbsolue.Y;
						break;
					case Keys.Left:
						if (bAlignerSurGrille)
							nX = GetBefore(obj.PositionAbsolue.X, EDimentionDessin.X);
						else
							nX = obj.PositionAbsolue.X - nb;
						nY = obj.PositionAbsolue.Y;
						break;
					default:
						return;
				}
				obj.PositionAbsolue = new Point(nX, nY);
			}
			ElementModifie();
		}

		private void Selection_EtendreSurGrille(bool bWithRefresh)
		{
			if (LockEdition)
				return;
			List<I2iObjetGraphique> elements = Selection;//PrendreElementsDePremiersNiveau(Selection);
			foreach (I2iObjetGraphique ele in elements)
			{
				if (ele.IsLock)
					continue;
				int nPtX = GetThePlusProche(ele.RectangleAbsolu.Right, EDimentionDessin.X);
				while (nPtX - ele.RectangleAbsolu.X < LargeurMinimaleDesObjets)
					nPtX += GrilleAlignement.LargeurCarreau;

				int nPtY = GetThePlusProche(ele.RectangleAbsolu.Bottom, EDimentionDessin.Y);
				while (nPtY - ele.RectangleAbsolu.Y < HauteurMinimaleDesObjets)
					nPtY += GrilleAlignement.HauteurCarreau;

				ele.Size = new Size(nPtX - ele.RectangleAbsolu.X, nPtY - ele.RectangleAbsolu.Y);
			}

			if (bWithRefresh)
				Refresh();
			ElementModifie();
		}
		private void Selection_PositionnerSurGrille(bool bWithRefreshAndEvent)
		{
			if (LockEdition)
				return;
			List<I2iObjetGraphique> elements = PrendreElementsDePremiersNiveau(Selection);
			foreach (I2iObjetGraphique ele in elements)
			{
				if (ele.IsLock)
					continue;
				ele.PositionAbsolue = new Point(GetThePlusProche(ele.PositionAbsolue.X, EDimentionDessin.X), GetThePlusProche(ele.PositionAbsolue.Y, EDimentionDessin.Y));
			}

			if (bWithRefreshAndEvent)
			{
				Refresh();
				ElementModifie();
			}
		}
		private void Selection_CorrespondreSurGrille()
		{
			if (LockEdition)
				return;
			Selection_PositionnerSurGrille(false);
			Selection_EtendreSurGrille(true);
		}
		private void Selection_Supprimer()
		{
            using (CWaitCursor waiter = new CWaitCursor())
            {
                if (LockEdition)
                    return;

                List<I2iObjetGraphique> elts = new List<I2iObjetGraphique>();
                foreach (I2iObjetGraphique obj in Selection)
                {
                    if (obj != ObjetEdite && obj.Parent != null)
                    {
                        elts.Add(obj);
                    }
                }
                if (BeforeDeleteElement != null && !BeforeDeleteElement(elts))
                    return;
                foreach (I2iObjetGraphique elt in elts)
                    elt.Parent.DeleteChild(elt);

                Selection.Clear();

                if (AfterRemoveObjetGraphique != null)
                    AfterRemoveObjetGraphique(this, new EventArgs());
            }

		}
		#endregion
		#endregion

        /// <summary>
        /// Indique dans quel mode travaille la souris :
        /// Mode sélection
        /// Mode zoom
        /// Mode custom pour les héritiers qui implémentent d'autres modes
        /// </summary>
        [Browsable(false)]
        public EModeSouris ModeSouris
        {
            get
            {
                return m_modeSouris;
            }
            set
            {
                m_modeSouris = value;
                LoadCurseurAdapté();
                OnChangeModeSouris();
                
            }
        }


        protected virtual void OnChangeModeSouris()
        {
            if (ModeSourisChanged != null)
                ModeSourisChanged(this, new EventArgs());
        }

        public event EventHandler ModeSourisChanged;

		//Si mode souris standard, la fenêtre se charge de tout. Sinon,
		//les evenements sont renvoyés via les méthodes
		//OnMouseDownNonStandard, OnMouseUpNonStandard, OnMouseMoveNonStandard
        private EModeSouris m_modeSouris = EModeSouris.Selection;

		private float m_fEchelle = 1;

		/// //////////////////////////////////////////////////
		private void RecalcScrollSize()
		{
			if (m_objetEdite != null)
			{
				AutoScrollMinSize = new Size(
					(int)((m_objetEdite.Size.Width + m_objetEdite.Position.X) * Echelle),
					(int)((m_objetEdite.Size.Height + m_objetEdite.Position.Y) * Echelle));
			}
		}


		private DragDropEffects m_lastEffet;
		private Point m_lastDragDropPoint;

		#region Effet de fondu Ajout Suppression Element
		private void EnfantAjoute(I2iObjetGraphique element)
		{

			element.ChildAdded += new EventHandlerChild(EnfantAjoute);

			while (m_elementAjoute != null)
			{
			}
			m_elementAjoute = element;
			TimerFondu.Start();

		}
		private double m_bOpacite = 0;
		private I2iObjetGraphique m_elementAjoute = null;

		private void ApparitionElement()
		{
			if (m_bOpacite == 1)
				TimerFondu.Stop();
			m_bOpacite += 0.1;
            using (Bitmap bmp = new Bitmap(m_elementAjoute.Size.Width, m_elementAjoute.Size.Height))
            {
                Graphics g = Graphics.FromImage(bmp);
                m_elementAjoute.Draw(GetContexteDessin(g, ClientRectangle));
                //
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, bmp.Width, bmp.Height));


                float[][] matrixItems ={ 
           new float[] {1, 0, 0, 0, 0},
           new float[] {0, 1, 0, 0, 0},
           new float[] {0, 0, 1, 0, 0},
           new float[] {0, 0, 0, (float)m_bOpacite, 0}, 
           new float[] {0, 0, 0, 0, 1}};
                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

                ImageAttributes imageAtt = new ImageAttributes();
                imageAtt.SetColorMatrix(
                   colorMatrix,
                   ColorMatrixFlag.Default,
                   ColorAdjustType.Bitmap);

                CreateGraphics().DrawImage(
                   bmp,
                   m_elementAjoute.RectangleAbsolu,  // destination rectangle
                   0,                          // source rectangle x 
                   0,                          // source rectangle y
                   bmp.Width,                        // source rectangle width
                   bmp.Height,                       // source rectangle height
                   GraphicsUnit.Pixel,
                   imageAtt);
            }
		}


		private bool m_bEffetFonduAjoutSupp = false;
		public bool EffetAjoutSuppression
		{
			get
			{
				return m_bEffetFonduAjoutSupp;
			}
			set
			{
				m_bEffetFonduAjoutSupp = value;
			}
		}
		#endregion

		#region Drag Drop

        /// //////////////////////////////////////////////////
        ///Complete les initialisation d'un objet juste avant de le positionner après un drag & drop
        protected virtual void JusteBeforePositionneSurApresDragDrop(I2iObjetGraphique objet)
        { }

		/// //////////////////////////////////////////////////
		protected virtual void Editeur_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
            DragDropTraitement(e);
		}

        //-------------------------------------------------------------------------------------
        protected virtual void DragDropTraitement(System.Windows.Forms.DragEventArgs e)
        {
            if (m_objetEdite == null)
                return;
            if (m_bLockEdition)
                return;
            List<CDonneeDragDropObjetGraphique> datas = GetDragDropData(e.Data);

            if (datas == null || datas.Count == 0)
                return;

            List<I2iObjetGraphique> candidats = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in m_rectsDrags)
                candidats.Add(rct.ObjetGraphique);

            Point ptLocal = GetLogicalPointFromDisplay(PointToClient(new Point(e.X, e.Y)));
            List<I2iObjetGraphique> elementsToIgnore = new List<I2iObjetGraphique>(candidats);
            I2iObjetGraphique parent = GetConteneur(ptLocal, candidats, elementsToIgnore);
            if (parent == null)
                parent = ObjetEdite;

            List<I2iObjetGraphique> nouveaux = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in m_rectsDrags)
            {
                rct.RectangleDrag = rct.Datas.GetDragDropPosition(ptLocal);
                rct.RectangleDrag = GetRectangleSelonModesActives(rct.RectangleDrag, ptLocal);
                // rct.RectangleDrag.Offset((int)(AutoScrollPosition.X / Echelle), (int)(AutoScrollPosition.Y / Echelle));

                I2iObjetGraphique objetGraphique = rct.Datas.ObjetDragDrop;
                JusteBeforePositionneSurApresDragDrop(objetGraphique);
                bool bParentIsInSelec = objetGraphique.Parent != null && candidats.Contains(objetGraphique.Parent);

                if (e.Effect == DragDropEffects.Copy)
                {
                    Dictionary<Type, object> dicObjetsPourCloner = new Dictionary<Type, object>();
                    AddObjectsForClonerSerializer(dicObjetsPourCloner);
                    objetGraphique = (I2iObjetGraphique)objetGraphique.GetCloneAMettreDansParent(parent, dicObjetsPourCloner);
                    if (objetGraphique == null || !parent.AddChild(objetGraphique))
                    {
                        e.Effect = DragDropEffects.None;
                        objetGraphique.CancelClone();
                        continue;
                    }
                    else
                    {
                        objetGraphique.Parent = parent;
                        nouveaux.Add(objetGraphique);
                    }
                }
                else
                {
                    if (objetGraphique.Parent != parent)
                    {
                        if (objetGraphique.Parent != null)
                        {
                            if (!bParentIsInSelec)
                                objetGraphique.Parent.RemoveChild(objetGraphique);
                        }
                        else
                            nouveaux.Add(objetGraphique);
                        if (!bParentIsInSelec)
                            if (!parent.AddChild(objetGraphique))
                            {
                                e.Effect = DragDropEffects.None;
                                continue;
                            }
                            else
                            {
                                objetGraphique.Parent = parent;
                            }
                    }
                }

                if (!bParentIsInSelec)
                {
                    Point ptDrop = new Point(rct.RectangleDrag.Left, rct.RectangleDrag.Top);
                    objetGraphique.PositionAbsolue = ptDrop;
                }

            }

            if (nouveaux.Count > 0)
            {
                RefreshSelectionChanged = false;
                Selection.Clear();
                Selection.AddRange(nouveaux);
                RefreshSelectionChanged = true;
                DeclencheAfterAddElements(nouveaux);
                Refresh();

            }
            ElementModifie();
            EnDeplacement = false;
            Dessiner(true, true);
        }
		/// //////////////////////////////////////////////////
		private void Editeur_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
            DragEnterTraitement(e);
		}

        //-------------------------------------------------------------------------------------
        protected virtual void DragEnterTraitement(System.Windows.Forms.DragEventArgs e)
        {
            EnDeplacement = true;
            if (m_objetEdite == null)
                return;
            if (m_bLockEdition)
                return;
            m_lastDragDropPoint = new Point(e.X, e.Y);
            List<CDonneeDragDropObjetGraphique> datas = GetDragDropData(e.Data);
            int nbDatas = datas.Count;
            if (datas == null || nbDatas == 0)
            {
                e.Effect = DragDropEffects.None;
                m_lastEffet = e.Effect;
                return;
            }
            m_rectsDrags.Clear();

            e.Effect = DragDropEffects.Move;
            if (nbDatas > 0)
                for (int nData = 0; nData < nbDatas; nData++)
                {
                    CDonneeDragDropObjetGraphique data = datas[nData];

                    if (data.ObjetDragDrop.IsLock)
                        continue;
                    Rectangle rct = (data.GetDragDropPosition(GetLogicalPointFromDisplay(PointToClient(new Point(e.X, e.Y)))));
                    m_rectsDrags.Add(new CRectangleDragForObjetGraphique(data.ObjetDragDrop, rct, data));
                    if (data.IdOrigine != OrigineDragDropId)
                        e.Effect = DragDropEffects.Copy;
                }

            if ((e.KeyState & 8) == 8 &&
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                e.Effect = DragDropEffects.Copy;
            m_lastEffet = e.Effect;
            m_lastHighlightParent = null;
            Dessiner(true, false);
        }
		/// //////////////////////////////////////////////////
		private void Editeur_DragLeave(object sender, System.EventArgs e)
		{
            DragLeaveTraitement();
		}

        //-------------------------------------------------------------------------------------
        protected virtual void DragLeaveTraitement()
        {
            EnDeplacement = false;
            Refresh();
        }

        /// //////////////////////////////////////////////////
        private I2iObjetGraphique GetConteneur(Point ptLocal, List<I2iObjetGraphique> filsAMettreDedans, List<I2iObjetGraphique> elementsAIgnorer)
        {
            I2iObjetGraphique parent = ObjetEdite.SelectionnerElementConteneurDuDessus(ptLocal, elementsAIgnorer);
            I2iObjetGraphiqueConteneurAFilsChoisis conteneurAFilsChoisis = parent as I2iObjetGraphiqueConteneurAFilsChoisis;
            while (conteneurAFilsChoisis != null && !conteneurAFilsChoisis.AcceptAllChilds(filsAMettreDedans))
            {
                elementsAIgnorer.Add(parent);
                parent = ObjetEdite.SelectionnerElementConteneurDuDessus(ptLocal, elementsAIgnorer);
                conteneurAFilsChoisis = parent as I2iObjetGraphiqueConteneurAFilsChoisis;
            }
            return parent;
        }
		/// //////////////////////////////////////////////////
		private void Editeur_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
            DragOverTraitement(e);
		}

        //-------------------------------------------------------------------------------------
        protected virtual void DragOverTraitement(System.Windows.Forms.DragEventArgs e)
        {
            if (m_bLockEdition)
                return;
            if (m_objetEdite == null)
                return;
            Point ptSouris = new Point(e.X, e.Y);
            if (ptSouris == m_lastDragDropPoint)
            {
                e.Effect = m_lastEffet;
                return;
            }
            m_lastDragDropPoint = ptSouris;

            List<CDonneeDragDropObjetGraphique> datas = GetDragDropData(e.Data);
            if (datas == null || datas.Count == 0)
            {
                e.Effect = DragDropEffects.None;
                m_lastEffet = e.Effect;
                return;
            }
            List<I2iObjetGraphique> candidats = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in m_rectsDrags)
                candidats.Add(rct.ObjetGraphique);

            Point ptLocal = GetLogicalPointFromDisplay(PointToClient(ptSouris));
            List<I2iObjetGraphique> elementsToIgnore = new List<I2iObjetGraphique>(candidats);
            I2iObjetGraphique parent = GetConteneur(ptLocal, candidats, elementsToIgnore);
            if (parent == null)
                parent = ObjetEdite;
            /*if (parent.NoMove)
            {
                e.Effect = DragDropEffects.None;
                return;
            }*/
            //Efface les dessins précédents de sélection
            using (Bitmap bmpFond = DernierApercuToDispose)
            {
                Graphics gCurrent = CreateGraphics();

                foreach (CRectangleDragForObjetGraphique rct in m_rectsDrags)
                {

                    Rectangle rctDisplay = CUtilRect.Normalise(new Rectangle(
                        GetDisplayPointFromLogical(rct.RectangleDrag.Location),
                        GetDisplaySizeFromLogical(rct.RectangleDrag.Size)));
                    rctDisplay.Inflate(2, 2);
                    gCurrent.DrawImage(bmpFond, rctDisplay, rctDisplay, GraphicsUnit.Pixel);

                    rct.RectangleDrag = rct.Datas.GetDragDropPosition(ptLocal);
                    rct.RectangleDrag = GetRectangleSelonModesActives(rct.RectangleDrag, ptLocal);

                }

                e.Effect = DragDropEffects.Move;
                foreach (CDonneeDragDropObjetGraphique donnee in datas)
                    if (donnee.IdOrigine != OrigineDragDropId)
                        e.Effect = DragDropEffects.Copy;
                if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                    e.Effect = DragDropEffects.Copy;
                m_lastEffet = e.Effect;


                /*Graphics g = Graphics.FromImage(bmpFond);*/
                DrawDragDropRect(gCurrent, parent);
                //gCurrent.DrawImage(bmpFond, Point.Empty);
                //g.Dispose();
                gCurrent.Dispose();
            }
        }


		protected Rectangle GetRectangleSelonModesActives(Rectangle rct, Point ptDansEditeur)
		{
			//if (ModeAlignement && ModeRedimentionnement)
			//{
			//    return GetThePerfectSize(rct);
			//}
			if (ModeAlignement)
			{
				//Point ptMagnetise = MagnetiserPoint(ptDansEditeur);
				

                int nXRect = ptDansEditeur.X - (ptDansEditeur.X - rct.X);
                int nYRect = ptDansEditeur.Y - (ptDansEditeur.Y - rct.Y);

                nXRect -=(int) ((double)AutoScrollPosition.X /(double)Echelle);
                nYRect -=(int) ((double)AutoScrollPosition.Y /(double)Echelle);
                
               
                Rectangle rct2 = new Rectangle(new Point(nXRect, nYRect), rct.Size);
                
                             
               return new Rectangle(MagnetiserPoint(rct2.Location), rct2.Size);
               
               
				//return new Rectangle(MagnetiserPoint(rct.Location), rct.Size);
			}
			return rct;
		}

		/// //////////////////////////////////////////////////
		protected virtual List<CDonneeDragDropObjetGraphique> GetDragDropData(IDataObject data)
		{
			List<CDonneeDragDropObjetGraphique> donnee = (List<CDonneeDragDropObjetGraphique>)data.GetData(typeof(List<CDonneeDragDropObjetGraphique>));
			if (donnee == null)
			{
				CDonneeDragDropObjetGraphique obj = (CDonneeDragDropObjetGraphique)data.GetData(typeof(CDonneeDragDropObjetGraphique));
				if (obj == null)
					return null;
				donnee = new List<CDonneeDragDropObjetGraphique>();
				donnee.Add(obj);
			}
			return donnee;
		}


		//Rectangle de drag drop dans le repère du dessin

		private List<CRectangleDragForObjetGraphique> m_rectsDrags;
        protected List<CRectangleDragForObjetGraphique> RectsDrags
        {
            get
            {
                return m_rectsDrags;
            }
        }

		protected class CRectangleDragForObjetGraphique
		{
			public CRectangleDragForObjetGraphique(I2iObjetGraphique obj, Rectangle rct, CDonneeDragDropObjetGraphique data)
			{
				m_data = data;
				m_rct = rct;
				m_obj = obj;
			}
			private CDonneeDragDropObjetGraphique m_data;
			public CDonneeDragDropObjetGraphique Datas
			{
				get
				{
					return m_data;
				}
			}

			private Rectangle m_rct;
			public Rectangle RectangleDrag
			{
				get
				{
					return m_rct;
				}
				set
				{
					m_rct = value;
				}
			}
			private I2iObjetGraphique m_obj;
			public I2iObjetGraphique ObjetGraphique
			{
				get
				{
					return m_obj;
				}
			}
			private Bitmap m_bmp;
			public Bitmap CaptureRectangle
			{
				get
				{
					return m_bmp;
				}
				set
				{
					m_bmp = value;
				}
			}
		}


		/// //////////////////////////////////////////////////
		public static Point PtOrigine
		{
			get
			{
				return new Point(0, 0);
			}
		}

        private I2iObjetGraphique m_lastHighlightParent = null;
		private void DrawDragDropRect(Graphics g, I2iObjetGraphique parent)
		{
			/*if (ObjetEdite != null)
				g.TranslateTransform((int)((double)-ObjetEdite.Position.X*(double)Echelle), (int)((double)(-ObjetEdite.Position.Y*(double)Echelle)));
            */

			Pen pen = new Pen(Brushes.Black);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			foreach (CRectangleDragForObjetGraphique rct in m_rectsDrags)
			{
				DrawDragDropRect(g, pen, rct.RectangleDrag);
			}

            if (m_lastHighlightParent != null && m_lastHighlightParent != parent)
            {
                //Efface le cadre sur l'ancien parent
                Rectangle rctConteneur = CUtilRect.Normalise(new Rectangle(
                    GetDisplayPointFromLogical(m_lastHighlightParent.RectangleAbsolu.Location),
                    GetDisplaySizeFromLogical(m_lastHighlightParent.RectangleAbsolu.Size)));
                rctConteneur.Inflate ( 2, 2 );
                using (Bitmap bmp = DernierApercuToDispose )
                    g.DrawImage ( bmp, rctConteneur, rctConteneur, GraphicsUnit.Pixel );
            }

			//DESSIN CADRE CONTENEUR
			if (parent != null)
			{
                Rectangle rctConteneur = CUtilRect.Normalise(new Rectangle(
                    GetDisplayPointFromLogical(parent.RectangleAbsolu.Location),
                    GetDisplaySizeFromLogical(parent.RectangleAbsolu.Size)));
				g.DrawRectangle(Pens.Red, rctConteneur);
			}
            m_lastHighlightParent = parent;

			pen.Dispose();
		}
		private void DrawDragDropRect(Graphics g, Pen pen, Rectangle rct)
		{
            Rectangle rctDisplay = CUtilRect.Normalise ( new Rectangle(
                GetDisplayPointFromLogical(rct.Location),
                GetDisplaySizeFromLogical(rct.Size)) );

            g.DrawRectangle(pen, rctDisplay);


             //DESSIN ICONE ALIGNEMENT
              if(ModeAlignement)
               {
                   Bitmap bmpAligner = Properties.Resources.aligner_positionner_sur_grille;

                   g.DrawImageUnscaled(bmpAligner, rctDisplay.Location);
               }
		}

		#endregion

		private bool m_bEnDeplacement = false;
		public bool EnDeplacement
		{
			get
			{
				return m_bEnDeplacement;
			}
			set
			{
				m_bEnDeplacement = value;
			}
		}


		#region Dessin



		private Bitmap m_bitmap = null;
		public Bitmap DernierApercuToDispose
		{
			get
			{
				return new Bitmap(m_bitmap);
				//Bitmap bmp = new Bitmap(Width, Height);
				//Graphics g = Graphics.FromImage(bmp);
				//Rectangle rct = new Rectangle(PtOrigine, ZoneVisible.Size);
				//g.DrawImage(m_bitmap, rct, ZoneVisible, GraphicsUnit.Pixel);
				//return bmp;
			}
		}

		/// //////////////////////////////////////////////////
		private Point m_ptLast;
		protected virtual void Editeur_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (m_ptLast != AutoScrollPosition || m_bitmap == null)
			{
				Dessiner(true, true);
                using ( Bitmap bmp = DernierApercuToDispose )
				    e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
			}
			else
			{
                using (Bitmap bmp = DernierApercuToDispose)
				    e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
			}
		}

        /// <summary>
        /// Applique les matrices de transformation sur le graphic pour dessiner des points logiques
        /// </summary>
        /// <param name="g"></param>
        protected void PrepareGraphicPourDessiner(Graphics g)
        {
            g.ResetTransform();
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
            g.ScaleTransform(m_fEchelle, 
                m_fEchelle);
            if (ObjetEdite != null)
                g.TranslateTransform(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
            if (InverserAxeX)
            {
                g.TranslateTransform(ObjetEdite.Size.Width, 0);
                g.ScaleTransform(-1, 1);
            }
            if (InverserAxeY)
            {
                g.TranslateTransform(0, ObjetEdite.Size.Height);
                g.ScaleTransform(1, -1);
                
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }


		public Rectangle ZoneVisible
		{
			get
			{
			/*	Point pt = new Point(Math.Abs(AutoScrollPosition.X), Math.Abs(AutoScrollPosition.Y));
                Size newSize = new Size((int)(Size.Width*Echelle),(int)(Size.Height*Echelle));
				return new Rectangle(pt, newSize);*/

                Point pt = new Point((int)Math.Abs(AutoScrollPosition.X/Echelle), (int)Math.Abs(AutoScrollPosition.Y/Echelle));
                Size newSize = new Size((int)(Size.Width/Echelle),(int)(Size.Height/Echelle));
				return new Rectangle(pt, newSize);

			}	
        }

        /// //////////////////////////////////////////////////
        protected virtual CContextDessinObjetGraphique GetContexteDessin(
            Graphics g,
            Rectangle clipZone)
        {
            return new CContextDessinObjetGraphique(g, clipZone);
        }

		/// //////////////////////////////////////////////////
		public virtual void Dessiner(bool bElement, bool bSelection)
		{
            try
            {
                Bitmap bmp = CreateEmptyBitmap();

                if (m_bIsZooming)
                    return;

                Rectangle zone = ZoneVisible;


                if (zone.Width == 0 || zone.Height == 0)
                    return;


                Graphics g = Graphics.FromImage(bmp);

                Matrix matrice = g.Transform;

                PrepareGraphicPourDessiner(g);

                if (bElement && m_objetEdite != null)
                {
                    m_objetEdite.Draw(GetContexteDessin(g, zone));
                    foreach (I2iObjetGraphique objet in m_selection)
                        objet.DrawSelected(g);
                }
                MyDrawElementsSupplementaires(g);

                if (ModeAffichageGrille == EModeAffichageGrille.Toujours
                    || (EnDeplacement && ModeAffichageGrille == EModeAffichageGrille.AuDeplacement))
                {
                    Rectangle zoneGrille = zone;
                    if (ObjetEdite != null)
                    {
                        zoneGrille = new Rectangle(PtOrigine, ObjetEdite.Size);
                         //zoneGrille.Offset(ObjetEdite.Position.X, ObjetEdite.Position.Y);
                        //g.TranslateTransform(ObjetEdite.Position.X, ObjetEdite.Position.Y);
                        GrilleAlignement.Draw(g, zoneGrille);
                        //g.TranslateTransform(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
                    }
                    else
                        GrilleAlignement.Draw(g, zone);
                }


                if (bSelection && m_bSelectionVisible)
                {
                    g.ResetTransform();
                    g.Transform = matrice;
                                 
                    Selection.Draw(g, zone, true);
                }
                if (m_bitmap != null)
                    m_bitmap.Dispose();
                m_bitmap = bmp;

                Graphics gScreen = CreateGraphics();
                gScreen.DrawImageUnscaled(m_bitmap, Point.Empty);

                m_ptLast = AutoScrollPosition;
            }
            catch
            {
            }

		}


        public virtual void MyDrawElementsSupplementaires ( Graphics gPretPourDessinElementsLogiques )
        {
        }

        public void RefreshDelayed()
        {
            m_timerRefresh.Stop();
            m_timerRefresh.Start();
        }

		public override void Refresh()
		{
            m_timerRefresh.Stop();
			if (DesignMode || m_elementAjoute != null)
				return;

			if ( m_selection != null )
				m_selection.RecalcPositionPoignees();
			Dessiner(true, true);

			base.Refresh();
		}

		public Point PointOrigineObjetEdite
		{
			get
			{
				if (ObjetEdite != null)
				{
					int nX = ObjetEdite.Position.X - ZoneVisible.X;
					int nY = ObjetEdite.Position.Y - ZoneVisible.Y;

					if (nX < 0)
						nX = 0;
					if (nY < 0)
						nY = 0;
					return new Point(nX, nY);
				}
				else
					return PtOrigine;
			}
		}


		/// //////////////////////////////////////////////////
		private Bitmap m_emptyBitmapBase;
		private Bitmap CreateEmptyBitmap()
		{
			if (m_emptyBitmapBase == null || m_emptyBitmapBase.Size != Size)
			{
				int nHeight = Size.Height;
				int nWidth = Size.Width;
				if (nHeight < 1)
					nHeight = 500;
				if (nWidth < 1)
					nWidth = 500;
				m_emptyBitmapBase = new Bitmap(nWidth, nHeight);
				Graphics g = Graphics.FromImage(m_emptyBitmapBase);
				g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, nWidth, nHeight));
				g.Dispose();
			}
			return new Bitmap(m_emptyBitmapBase);
		}

		/// //////////////////////////////////////////////////
		private void Editeur_SizeChanged(object sender, System.EventArgs e)
		{
			RefreshDelayed();
		}

		public EFormePoignee FormesDesPoignees
		{
			get
			{
				return Selection.FormesDesPoignees;
			}
			set
			{
				if (value != FormesDesPoignees)
				{
					Selection.FormesDesPoignees = value;
					RefreshDelayed();
				}
			}
		}


		private int m_largeurMiniObjets = 10;
		public int LargeurMinimaleDesObjets
		{
			get
			{
				return m_largeurMiniObjets;
			}
			set
			{
				if (value > 0)
					m_largeurMiniObjets = value;
			}
		}
		private int m_hauteurMiniObjets = 10;
		public int HauteurMinimaleDesObjets
		{
			get
			{
				return m_hauteurMiniObjets;
			}
			set
			{
				if (value > 0)
					m_hauteurMiniObjets = value;
			}
		}


		/// //////////////////////////////////////////////////
		public Point GetLogicalPointFromDisplay(Point pt)
		{
			return GetLogicalPointFromDisplay(pt.X, pt.Y);
		}

		private Point GetLogicalPointFromDisplay(int nX, int nY)
		{
			Point pt = new Point((int)((nX - AutoScrollPosition.X) / Echelle), (int)((nY - AutoScrollPosition.Y) / Echelle));
			if ( ObjetEdite != null )
				pt.Offset ( ObjetEdite.Position.X, ObjetEdite.Position.Y );
            if (InverserAxeX)
                pt = new Point(ObjetEdite.Size.Width - pt.X, pt.Y);
            if (InverserAxeY)
                pt = new Point(pt.X, ObjetEdite.Size.Height - pt.Y);

			return pt;
		}

        /// //////////////////////////////////////////////////
        public Size GetLogicalSizeFromDisplay(Size sz)
        {
            return new Size((int)(sz.Width / Echelle), (int)(sz.Height / Echelle));
        }

        /// //////////////////////////////////////////////////
        public Point GetDisplayPointFromLogical(Point pt)
        {
            Point ptRetour = pt;
            if (InverserAxeX)
                ptRetour = new Point(ObjetEdite.Size.Width - ptRetour.X, ptRetour.Y);
            if (InverserAxeY)
                ptRetour = new Point(ptRetour.X, ObjetEdite.Size.Height - ptRetour.Y);
            if (ObjetEdite != null)
                ptRetour.Offset(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
            ptRetour = new Point((int)(ptRetour.X * Echelle) + AutoScrollPosition.X,
                (int)(ptRetour.Y * Echelle) + AutoScrollPosition.Y);
            return ptRetour;
        }

        /// //////////////////////////////////////////////////
        public Size GetDisplaySizeFromLogical(Size sz)
        {
            Size newSz = new Size((int)(sz.Width * Echelle), (int)(sz.Height * Echelle));
            if (InverserAxeX)
                newSz = new Size(-newSz.Width, newSz.Height);
            if (InverserAxeY)
                newSz = new Size(newSz.Width, -newSz.Height);
            return newSz;
        }


		#endregion

        public event EventHandler EchelleChanged;

		/// //////////////////////////////////////////////////
		public float Echelle
		{
			get
			{
				return m_fEchelle;
			}
			set
			{
				if (value != m_fEchelle)
				{
					m_fEchelle = value;
					RecalcScrollSize();
                    if (EchelleChanged != null)
                        EchelleChanged(this, null);
				}
			}
		}

		#region Evenements Souris
		public event EventHandler DoubleClicSurElement;

		

		/// //////////////////////////////////////////////////
		private void Editeur_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (ModeSouris == EModeSouris.Custom)
			{
				OnMouseDownNonStandard(sender, e);
				return;
			}
			if (ObjetEdite == null)
				return;
			if (DesignMode)
				return;

			Point ptLogique = GetLogicalPointFromDisplay(e.X, e.Y);
            if (ModeSouris == EModeSouris.Selection)
            {
                if (e.Button != MouseButtons.Right || !Selection.Contains(ObjetEdite.SelectionnerElementDuDessus(ptLogique)))
                    Selection.MouseDown(ptLogique);
            }


           if (ModeSouris == EModeSouris.Zoom)
            {
                m_rectangleSelection = new CRectangleSelection();   
                m_bDragRectangleZoom = true;
             
                m_rectangleSelection.StartSelection(this, e.Location);
            }
		}
		/// //////////////////////////////////////////////////
		public virtual void OnMouseDownNonStandard(object sender, MouseEventArgs e)
		{
		}

		/// //////////////////////////////////////////////////
		private I2iObjetGraphique m_elementDoubleClique;
        private Point m_pointRightClick;
		private void Editeur_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if (ModeSouris == EModeSouris.Custom)
			{
				OnMouseUpNonStandard(sender, e);
				return;
			}
			if (m_objetEdite == null)
				return;
			if (DesignMode)
				return;

            Point ptLogique = GetLogicalPointFromDisplay(e.X, e.Y);
            if (ModeSouris == EModeSouris.Selection)
            {
                if (e.Button == MouseButtons.Left || Selection.EnSelection || Selection.EnDeplacement)
                {
                    List<I2iObjetGraphique> elements = (List<I2iObjetGraphique>)Selection;
                    Selection.MouseUp(ptLogique);
                    if (Selection.Count == 1 && elements.Contains(Selection.ControlReference))
                        m_elementDoubleClique = Selection.ControlReference;
                    else
                        m_elementDoubleClique = null;
                }
                if (e.Button == MouseButtons.Right && (!m_bNoMenu))
                {

                    m_pointRightClick = e.Location;
                      AfficherMenuContextuel(e.Location, false);
                }
            }
                   
            else if(ModeSouris == EModeSouris.Zoom)
                 {
                       m_pointRightClick = e.Location;
                       m_rectangleSelection.EndSelection();
                       
                       if (e.Button == MouseButtons.Right)
                           MNUZoomOut();
                       else 
                       {
                              m_bDragRectangleZoom = false;
                             

                               Rectangle rectZoom = new Rectangle((int)(m_rectangleSelection.RectangleSelection.Left / Echelle),
                                   (int)(m_rectangleSelection.RectangleSelection.Top / Echelle),
                                   (int)(m_rectangleSelection.RectangleSelection.Width / Echelle),
                                   (int)(m_rectangleSelection.RectangleSelection.Height / Echelle));

                               rectZoom.Offset((int)(-AutoScrollPosition.X / Echelle), (int)(-AutoScrollPosition.Y / Echelle));



                               if(rectZoom.Width > 10 && rectZoom.Height >10)
                                   
                                        ZoomRectangle(rectZoom);
                               else 
                                   MNUZoomIn();
                           }
                       }
                 
                 


           

		}
		/// //////////////////////////////////////////////////
		public virtual void OnMouseUpNonStandard(object sender, MouseEventArgs e)
		{
		}

        /// //////////////////////////////////////////////////
        protected virtual void LoadCurseurAdapté()
        {
             switch (ModeSouris)
	        {
		        case EModeSouris.Selection:
                    Cursor = Cursors.Arrow;
                    break;
                case EModeSouris.Zoom:
                    Cursor = C2iCursorLoader.LoadCursor(typeof(CPanelEditionObjetGraphique), "LoupePlus", Properties.Resources.curLoupePlus);
                     break;
            
             }
        }

		/// //////////////////////////////////////////////////
		private void Editeur_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (ModeSouris == EModeSouris.Custom)
			{
				OnMouseMoveNonStandard(sender, e);
				return;
			}
            LoadCurseurAdapté();               
			if (m_objetEdite == null)
				return;
			if (DesignMode)
				return;

            if (ModeSouris == EModeSouris.Selection)
            {
                Selection.MouseMove(GetLogicalPointFromDisplay(e.X, e.Y));
                if (e.Button == MouseButtons.None)
                {
                    m_timerTooltip.Stop();
                    m_timerTooltip.Start();
                    m_tooltip.Hide(this);
                }
            }


            else if (ModeSouris == EModeSouris.Zoom)
            {
                if (m_bDragRectangleZoom)
                {
                    m_rectangleSelection.SetEndPoint(e.Location);

                }

            }
        }


		/// //////////////////////////////////////////////////
		public virtual void OnMouseMoveNonStandard(object sender, MouseEventArgs e)
		{
		}

		/// //////////////////////////////////////////////////
		private void Editeur_DoubleClick(object sender, System.EventArgs e)
		{
			if (DoubleClicSurElement != null && m_elementDoubleClique != null&& m_bNoDoubleClick==false)
				DoubleClicSurElement(sender, e);
			else if (m_elementDoubleClique != null)
			{
				Point pt = Cursor.Position;
				pt = PointToClient(pt);
				pt = GetLogicalPointFromDisplay(pt);
				m_elementDoubleClique.OnDesignDoubleClick(pt);
				RefreshDelayed();
			}
		}
		#endregion

		#region Evenements Clavier
		/// //////////////////////////////////////////////////

		private int m_nbRepetition = 0;
		private Keys m_oldKeys = Keys.None;
		private void Editeur_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (m_oldKeys != e.KeyCode)
			{
				m_oldKeys = e.KeyCode;
				m_nbRepetition = 1;
			}
			else
				m_nbRepetition++;

          
			if (Selection.EnAction
				|| e.KeyCode == Keys.Tab
				|| e.KeyCode == Keys.Left
				|| e.KeyCode == Keys.Down
				|| e.KeyCode == Keys.Up
				|| e.KeyCode == Keys.Right
				|| (e.Modifiers == Keys.Control &&
				e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.Insert ) ||
				(e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert ) )
				e.IsInputKey = true;


			//if((e.KeyCode == Keys.Down 
			//    || e.KeyCode == Keys.Up 
			//    || e.KeyCode == Keys.Right 
			//    || e.KeyCode == Keys.Left)
			//    && ModeAffichageGrille == EModeAffichageGrille.AuDeplacement 
			//    && Selection.Count > 0)
			//{
			//    if (m_oldKeys != e.KeyCode)
			//    {
			//        m_oldKeys = e.KeyCode;
			//        m_nbRepetition = 0;
			//    }
			//    else
			//        m_nbRepetition++;
			//}
		}

        void CPanelEditionObjetGraphique_KeyUp(object sender, KeyEventArgs e)
        {
            m_oldKeys = Keys.BrowserFavorites;
            m_nbRepetition = 0;
        }

		private void Editeur_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (ObjetEdite == null || Selection.EnAction)
				return;


			RefreshSelectionChanged = false;

			//if (HistorisationActive && ModeRedimentionnement && e.KeyCode == Keys.Z && !LockEdition)
			//{
			//    Annuler();
			//}
			//else if (HistorisationActive && ModeRedimentionnement && e.KeyCode == Keys.Y && !LockEdition)
			//{
			//    Retablir();
			//}
            if (m_nbRepetition == 1 && ModeRedimentionnement && e.KeyCode == Keys.A)
            {
                Selection_SelectionnerLesFils();
                m_nbRepetition = 0;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                Selection_Suivant();
            }
            else if (m_nbRepetition == 1 && !LockEdition && Selection.Count > 0 && e.KeyCode == Keys.Delete && (!m_bNoDelete)&&(CanDeleteSelection()))
            {
                Selection_Supprimer();
                m_nbRepetition = 0;
            }
            else if (!LockEdition && Selection.Count > 0 &&
                (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left))
            {
                EnDeplacement = m_nbRepetition > 3;
                if (ModeRedimentionnement)
                    Selection_RedefinirTaille(e.KeyCode);
                else
                    Selection_Bouger(e.KeyCode);
            }
            else if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.X) && (!m_bNoClipboard)&&(CanDeleteSelection()))
                CutToClipBoard();
            else if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert) && (!m_bNoClipboard))
                CopyToClipBoard();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V ||
                e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert && (!m_bNoClipboard))
                PasteFromClipBoard();
            else if (m_nbRepetition == 1 && ModeAlignement)
            {
            }
            else
            {
                RefreshSelectionChanged = true;
                return;
            }

			RefreshSelectionChanged = true;

			if (ModeAlignement)
				EnDeplacement = true;
			Dessiner(true, true);
		}


        private bool CanDeleteSelection()
        {
            foreach (I2iObjetGraphique obj in Selection)
            {
                if (obj.NoDelete)
                    return false;
            }
            return true;
        }
        private void CutToClipBoard()
        {
            CopyToClipBoard();

            Selection_Supprimer();
            
        }


		private void CopyToClipBoard()
		{
			if (Selection.Count == 0)
				return;
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
            CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
			int nNb = Selection.Count;
			serializer.TraiteInt(ref nNb);
			foreach (I2iObjetGraphique objet in Selection)
			{
                if(!(Selection.Contains(objet.Parent)))
                {
				I2iObjetGraphique objTmp = objet;
				if (!serializer.TraiteObject<I2iObjetGraphique>(ref objTmp))
					return;
                }
			}
			writer.Close();
			Clipboard.SetData(GetType().ToString(), stream.ToArray());
            
		}

        /// <summary>
        /// Retourne des objets graphiques à partir d'un format de clipboard
        /// qui n'est pas interne. Par exemple, une image, un metafile, un texte, ...
        /// Chaque classe héritant peut implémenter ici sa propre logique
        /// </summary>
        /// <returns></returns>
        protected virtual I2iObjetGraphique[] GetObjetsFromFormatClipboardExterne()
        {
            return null;
        }

		private void PasteFromClipBoard()
		{
            I2iObjetGraphique parent = ObjetEdite;
            if (Selection.Count == 1)
            {
                parent = Selection[0];
                while (parent != null && !parent.AcceptChilds)
                {
                    parent = parent.Parent;
                }
            }
            if (parent == null)
                parent = ObjetEdite;
            List<I2iObjetGraphique> nouveaux = new List<I2iObjetGraphique>();
            if (!Clipboard.ContainsData(GetType().ToString()))
            {
                I2iObjetGraphique[] objets = GetObjetsFromFormatClipboardExterne();
                if (objets == null)
                    return;
                nouveaux.AddRange(objets);
            }
            else
            {
                byte[] datas = Clipboard.GetData(GetType().ToString()) as byte[];
                MemoryStream stream = new MemoryStream(datas);
                BinaryReader reader = new BinaryReader(stream);
                CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                serializer.IsForClone = true;
                Dictionary<Type, object> objetsPourSerializer = new Dictionary<Type, object>();
                AddObjectsForClonerSerializer(objetsPourSerializer);
                foreach (KeyValuePair<Type, object> pair in objetsPourSerializer)
                    serializer.AttacheObjet(pair.Key, pair.Value);
                try
                {
                    int nNbElements = 0;
                    serializer.TraiteInt(ref nNbElements);
                    for (int nElement = 0; nElement < nNbElements; nElement++)
                    {
                        I2iObjetGraphique objet = null;
                        if (serializer.TraiteObject<I2iObjetGraphique>(ref objet))
                        {
                            nouveaux.Add(objet);
                        }
                        else
                            break;
                    }
                }
                catch
                {
                }

                reader.Close();
                stream.Close();
            }

            foreach (I2iObjetGraphique objet in nouveaux.ToArray())
            {
                if (!parent.AddChild(objet))
                    nouveaux.Remove(objet);
                else
                    objet.Parent = parent;
            }
			Selection.Clear();
                 

            Point ptSouris = GetLogicalPointFromDisplay(m_pointRightClick);
            int nTop = int.MaxValue;
            int nLeft = int.MaxValue;
            foreach (I2iObjetGraphique objet in nouveaux)
            {
                nLeft = Math.Min(nLeft,objet.Position.X);
                nTop = Math.Min(nTop,objet.Position.Y);
            }
            int deplX = ptSouris.X - nLeft;
            int deplY = ptSouris.Y - nTop;
            for (int i = 0; i < nouveaux.Count; i++)
                nouveaux[i].Position = new Point(nouveaux[i].Position.X + deplX, nouveaux[i].Position.Y + deplY);

			foreach (I2iObjetGraphique objet in nouveaux)
				Selection.Add(objet);

            Refresh();

		}



		private void Editeur_KeyUp(object sender, KeyEventArgs e)
		{

			m_nbRepetition = 0;
			m_oldKeys = Keys.None;

			if (Selection.EnAction)
				return;


			if (!ModeAlignement)
			{
				EnDeplacement = false;
				Dessiner(true, true);
			}
		}
		#endregion

		#region Grille
		private CGrilleEditeurObjetGraphique m_grille = new CGrilleEditeurObjetGraphique(8, 8, new Size(0, 0));
		public CGrilleEditeurObjetGraphique GrilleAlignement
		{
			get
			{
				return m_grille;
			}
			set
			{
				if (value != null)
				{
					m_grille = value;
				}
			}
		}

		public ERepresentationGrille ModeRepresentationGrille
		{
			get
			{
				return m_grille.Representation;
			}
			set
			{
				m_grille.Representation = value;
				m_mnu_itm_grille_representation_angles.Checked = value == ERepresentationGrille.Angles;
				m_mnu_itm_grille_representation_discontinues.Checked = value == ERepresentationGrille.LignesDiscontinues;
				m_mnu_itm_grille_representation_lignes.Checked = value == ERepresentationGrille.LignesContinues;
				m_mnu_itm_grille_representation_pointillets.Checked = value == ERepresentationGrille.LignesPointillets;
				m_mnu_itm_grille_representation_points.Checked = value == ERepresentationGrille.Points;

				m_mnu_itm_grille_representation_angles.ForeColor = value == ERepresentationGrille.Angles ? Color.Blue : Color.Red;
				m_mnu_itm_grille_representation_discontinues.ForeColor = value == ERepresentationGrille.LignesDiscontinues ? Color.Blue : Color.Red;
				m_mnu_itm_grille_representation_lignes.ForeColor = value == ERepresentationGrille.LignesContinues ? Color.Blue : Color.Red;
				m_mnu_itm_grille_representation_pointillets.ForeColor = value == ERepresentationGrille.LignesPointillets ? Color.Blue : Color.Red;
				m_mnu_itm_grille_representation_points.ForeColor = value == ERepresentationGrille.Points ? Color.Blue : Color.Red;

				RefreshDelayed();
			}
		}

		private EModeAffichageGrille m_modeAffichage = EModeAffichageGrille.AuDeplacement;
		public EModeAffichageGrille ModeAffichageGrille
		{
			get
			{
				return m_modeAffichage;
			}
			set
			{
				SetModeAffichageGrilleSansRefresh(value);
				RefreshDelayed();
			}
		}
		private void SetModeAffichageGrilleSansRefresh(EModeAffichageGrille mode)
		{
			m_modeAffichage = mode;

			m_mnu_itm_grille_affichage_tjrs.Checked = mode == EModeAffichageGrille.Toujours;
			m_mnu_itm_grille_affichage_jamais.Checked = mode == EModeAffichageGrille.Jamais;
			m_mnu_itm_grille_affichage_deplacement.Checked = mode == EModeAffichageGrille.AuDeplacement;

			m_mnu_itm_grille_affichage_tjrs.ForeColor = m_mnu_itm_grille_affichage_tjrs.Checked ? Color.Blue : Color.Red;
			m_mnu_itm_grille_affichage_jamais.ForeColor = m_mnu_itm_grille_affichage_jamais.Checked ? Color.Blue : Color.Red;
			m_mnu_itm_grille_affichage_deplacement.ForeColor = m_mnu_itm_grille_affichage_deplacement.Checked ? Color.Blue : Color.Red;

		}



		private bool m_bToujoursAlignerSurLaGrille = true;
		public bool ToujoursAlignerSurLaGrille
		{
			get
			{
				return m_bToujoursAlignerSurLaGrille;
			}
			set
			{
				m_bToujoursAlignerSurLaGrille = value;
				m_mnu_itm_grille_tjrsAligner.Checked = value;
				m_mnu_itm_grille_tjrsAligner.ForeColor = value ? Color.Red : Color.Blue;
			}
		}

		public bool ModeAlignement
		{
			get
			{
				return ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) || m_bToujoursAlignerSurLaGrille;
			}
		}
		public bool ModeRedimentionnement
		{
			get
			{
				return ((Control.ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		public Point MagnetiserPoint(Point pt)
		{

           
			//Alignement sur grille
            
           pt.Offset((int)((double)AutoScrollPosition.X  / (double)Echelle), (int)((double)AutoScrollPosition.Y / (double)Echelle));





			//pt.Offset(AutoScrollOffset);
			return new Point(GetThePlusProche(pt.X, EDimentionDessin.X), GetThePlusProche(pt.Y, EDimentionDessin.Y));



		}

		#region MAGNETISATION SUR CONTROL
		private bool m_bToujoursAlignerSelonLesControles = true;
		public bool ToujoursAlignerSelonLesControles
		{
			get
			{
				return m_bToujoursAlignerSelonLesControles;
			}
			set
			{
				m_bToujoursAlignerSelonLesControles = value;
			}
		}

		//Alignement sur Controles
		private Point GetPointSelonControl(Point pt)
		{
			return pt;
		}
		#endregion



		private int GetAfter(int nVal, EDimentionDessin dim)
		{
			int nPas = GetPasGrille(dim);
			double rapport = ((double)nVal - 1) / (double)nPas;
			//return ((int)rapport * nPas) + 1;
			if (rapport - (double)(int)rapport == 0)
			{
				return nVal + nPas;
			}
			else
			{
				return ((int)rapport * nPas) + nPas + 1;
			}
		}
		private int GetBefore(int nVal, EDimentionDessin dim)
		{
			int nPas = GetPasGrille(dim);
			double rapport = ((double)nVal - 1) / (double)nPas;
			//return ((int)rapport * nPas) + 1;
			if (rapport - (double)(int)rapport == 0)
			{
				return nVal - nPas;
			}
			else
			{
				return ((int)rapport * nPas) + 1;
			}
		}
		public int GetThePlusProche(int nVal, EDimentionDessin dim)
		{

			int nPas = GetPasGrille(dim);
			double rapport = ((double)nVal - 1) / (double)nPas;
			//return ((int)rapport * nPas) + 1;
			if (rapport - (double)(int)rapport == 0)
			{
				return nVal;
			}
			else if (rapport - (double)(int)rapport <= 0.5)
			{
				return ((int)rapport * nPas) + 1;
			}
			else
			{
				return ((int)rapport * nPas) + nPas + 1;
			}
		}
		private int GetPasGrille(EDimentionDessin dim)
		{
			switch (dim)
			{
				case EDimentionDessin.X: return GrilleAlignement.LargeurCarreau;
				case EDimentionDessin.Y: return GrilleAlignement.HauteurCarreau;
				case EDimentionDessin.Z:
				default: return -1;
			}
		}


		public Rectangle GetThePerfectSize(Rectangle rct)
		{
			Point pt = MagnetiserPoint(rct.Location);
			Point ptOppose = MagnetiserPoint(new Point(pt.X + rct.Width, pt.Y + rct.Height));
			return new Rectangle(pt, new Size(ptOppose.X - pt.X, ptOppose.Y - pt.Y));
		}


		//Sauvegarde / Chargement
		public CProfilEditeurObjetGraphique Profil
		{
			get
			{
				CProfilEditeurObjetGraphique prof = new CProfilEditeurObjetGraphique();
				prof.Grille = GrilleAlignement;
				prof.Marge = Marge;
				prof.ModeAffichageGrille = ModeAffichageGrille;
				prof.HistorisationActive = HistorisationActive;
				prof.NombreHistorisation = NombreHistorisation;
				prof.ToujoursAlignerSurLaGrille = ToujoursAlignerSurLaGrille;
				prof.FormeDesPoignees = FormesDesPoignees;
				return prof;
			}
			set
			{
				GrilleAlignement = value.Grille;
				Marge = value.Marge;
				ToujoursAlignerSurLaGrille = value.ToujoursAlignerSurLaGrille;
				NombreHistorisation = value.NombreHistorisation;
				HistorisationActive = value.HistorisationActive;
				Selection.FormesDesPoignees = value.FormeDesPoignees; //On passe par la selection pour qu'il n'y est pas de refresh
				ModeAffichageGrille = value.ModeAffichageGrille; //En Dernier car Refresh
			}
		}
		#endregion

		#region Menu

		#region Fermeture / Ouverture
		public event EventHandlerInvokationMenu MenuInvoked;
		public void InvokeMenu(Point pt)
		{
			m_mnu.Show(pt);
		}
		private bool m_bEffetFondu = true;
		private Timer m_timerFondu;
		private Timer TimerFondu
		{
			get
			{
				if (m_timerFondu == null)
				{
					m_timerFondu = new Timer();
					m_timerFondu.Interval = 30;
					m_timerFondu.Tick += new EventHandler(m_timerFondu_Tick);
				}
				return m_timerFondu;
			}
		}
		private bool m_bApparition = false;
		private void m_timerFondu_Tick(object sender, EventArgs e)
		{
			if (m_elementAjoute != null)
			{
				if (m_bOpacite >= 1)
				{
					m_timerFondu.Stop();
					m_bOpacite = 0.0;
					m_elementAjoute = null;
				}
				else
				{
					m_bOpacite += 0.1;
					ApparitionElement();
				}
			}
			else if (m_bApparition)
			{
				m_mnu.Opacity = m_mnu.Opacity + 0.1;
				if (m_mnu.Opacity >= 1)
					m_timerFondu.Stop();
			}
			else
			{
				m_mnu.Opacity = m_mnu.Opacity - 0.1;
				if (m_mnu.Opacity <= 0)
					m_timerFondu.Stop();
			}
		}
		public bool EffetFonduMenu
		{
			get
			{
				return m_bEffetFondu;
			}
			set
			{
				m_bEffetFondu = value;
			}
		}


        protected virtual void AfficherMenuAdditonnel(ContextMenuStrip menu)
        {



        }


		protected virtual void AfficherMenuContextuel(Point p, bool bSkipTestClicOnSelection)
		{
			if (LockEdition || Selection.Count == 0)
				return;

			m_bCacheEditeurSelectionne = Selection.Count == 1 && Selection.ControlReference == ObjetEdite;
			m_nCacheSelectionCount = Selection.Count;
			m_nCacheSelectionNbNotLocked = 0;
			bool bOnSelection = bSkipTestClicOnSelection;
			Point ptLogique = GetLogicalPointFromDisplay(p);
			foreach (I2iObjetGraphique itm in Selection)
			{
                bOnSelection = itm.IsPointIn(ptLogique) || bOnSelection;
				//bOnSelection = CUtilRect.Normalise(itm.RectangleAbsolu).Contains(ptLogique) || bOnSelection;
				if (!itm.IsLock)
					m_nCacheSelectionNbNotLocked++;
			}
			if (!bOnSelection)
				return;

            //afficher menu additonnel

            AfficherMenuAdditonnel(m_mnu);


            //Zoom
            bool bZoomInVisible = MNUZoomIn_Show();
            bool bZoomOutVisible = MNUZoomOut_Show();
            bool bZoomAdjustVisible = MNUZoomAdjust_Show();
            bool bZoomOrigineVisible = MNUZoomOrigine_Show();

            m_mnu_itm_zoomIn.Visible = bZoomInVisible;
            m_mnu_itm_zoomOut.Visible = bZoomOutVisible;
            m_mnu_itm_zoomAdjust.Visible = bZoomAdjustVisible;
            m_mnu_itm_zoomOrigine.Visible = bZoomOrigineVisible;

			//Bring To Front & Bring to Back
			bool bBringToFrontVisible = MNU_BringToFrontShow();
			bool bFrontToBackVisible = MNU_BringToBackShow();
			m_mnu_itm_bringToFront.Visible = bBringToFrontVisible;
			m_mnu_itm_bringToBack.Visible = bFrontToBackVisible;
			m_mnu_sep0.Visible = bBringToFrontVisible || bFrontToBackVisible;




			//REPARTIR
			bool bRepartirVisible = MNU_RepartirShow();
			m_mnu_itm_repartir.Visible = bRepartirVisible;
			if (bRepartirVisible)
			{
				bool bRepartirSurXAutoVisible = MNU_RepartirSurXAutoShow();
				bool bRepartirSurXDroiteVisible = MNU_RepartirSurXDroiteShow();
				bool bRepartirSurXGaucheVisible = MNU_RepartirSurXGaucheShow();
				m_mnu_itm_repartir_sur_X_Auto.Visible = bRepartirSurXAutoVisible;
				m_mnu_itm_repartir_sur_X_Droite.Visible = bRepartirSurXDroiteVisible;
				m_mnu_itm_repartir_sur_X_Gauche.Visible = bRepartirSurXGaucheVisible;
				m_mnu_itm_repartir_sur_X.Visible = bRepartirSurXAutoVisible && bRepartirSurXDroiteVisible && bRepartirSurXGaucheVisible;
				m_mnu_repartir_sep0.Visible = bRepartirSurXAutoVisible || bRepartirSurXDroiteVisible || bRepartirSurXGaucheVisible;

				bool bRepartirSurYAutoVisible = MNU_RepartirSurYAutoShow();
				bool bRepartirSurYHautVisible = MNU_RepartirSurYHautShow();
				bool bRepartirSurYBasVisible = MNU_RepartirSurYBasShow();
				m_mnu_itm_repartir_sur_Y_Auto.Visible = bRepartirSurYAutoVisible;
				m_mnu_itm_repartir_sur_Y_Haut.Visible = bRepartirSurYHautVisible;
				m_mnu_itm_repartir_sur_Y_Bas.Visible = bRepartirSurYBasVisible;
				m_mnu_itm_repartir_sur_Y.Visible = bRepartirSurYAutoVisible && bRepartirSurYHautVisible && bRepartirSurYBasVisible;
				m_mnu_repartir_sep1.Visible = bRepartirSurYAutoVisible || bRepartirSurYHautVisible || bRepartirSurYBasVisible;

				bool bMargeVisible = MNU_MargeShow();
				m_mnu_itm_marge.Visible = bMargeVisible;
			}
			//ALIGNER
			bool bAlignerVisible = MNU_AlignerShow();
			m_mnu_itm_aligner.Visible = bAlignerVisible;
			if (bAlignerVisible)
			{
				bool bAlignerSurXHautVisible = MNU_AlignerSurXHautShow();
				bool bAlignerSurXCentreVisible = MNU_AlignerSurXCentreShow();
				bool bAlignerSurXBasVisible = MNU_AlignerSurXBasShow();
				m_mnu_itm_aligner_sur_X_Haut.Visible = bAlignerSurXHautVisible;
				m_mnu_itm_aligner_sur_X_Centre.Visible = bAlignerSurXCentreVisible;
				m_mnu_itm_aligner_sur_X_Bas.Visible = bAlignerSurXBasVisible;
				m_mnu_itm_aligner_sur_X.Visible = bAlignerSurXHautVisible && bAlignerSurXCentreVisible && bAlignerSurXBasVisible;
				bool bAlignerSep0Visible = bAlignerSurXHautVisible || bAlignerSurXCentreVisible || bAlignerSurXBasVisible;
				m_mnu_aligner_sep0.Visible = bAlignerSep0Visible;

				bool bAlignerSurYGaucheVisible = MNU_AlignerSurYGaucheShow();
				bool bAlignerSurYCentreVisible = MNU_AlignerSurYCentreShow();
				bool bAlignerSurYDroiteVisible = MNU_AlignerSurYDroiteShow();
				m_mnu_itm_aligner_sur_Y_Gauche.Visible = bAlignerSurYCentreVisible;
				m_mnu_itm_aligner_sur_Y_Centre.Visible = bAlignerSurYCentreVisible;
				m_mnu_itm_aligner_sur_Y_Droite.Visible = bAlignerSurYDroiteVisible;
				m_mnu_itm_aligner_sur_Y.Visible = bAlignerSurYGaucheVisible && bAlignerSurYCentreVisible && bAlignerSurYDroiteVisible;

				m_mnu_aligner_sep1.Visible = bAlignerSep0Visible;
			}
			m_mnu_sep1.Visible = bAlignerVisible || bRepartirVisible;

			//REDIMENTIONNEMENT
			bool bResizeVisible = MNU_RedimentionnerShow();
			bool bResizeHeightVisible = MNU_RedimentionnerHauteurShow();
			bool bResizeWidthVisible = MNU_RedimentionnerLargeurShow();
			m_mnu_itm_resize.Visible = bResizeVisible;
			m_mnu_itm_resize_hauteur.Visible = bResizeHeightVisible;
			m_mnu_itm_resize_largeur.Visible = bResizeWidthVisible;
			m_mnu_sep2.Visible = bResizeVisible || bResizeHeightVisible || bResizeWidthVisible;

			//LOCK ET SUPP
			bool bLockVisible = MNU_LockShow();
			bool bDeleteVisible = MNU_DeleteShow();
			m_mnu_itm_lock.Visible = bLockVisible;
			m_mnu_itm_delete.Visible = bDeleteVisible;
            //Copier-coller
            bool bCutVisible = MNU_CutShow();
            bool bCopyVisible = MNU_CopyShow();
            bool bPasteVisible = MNU_PasteShow();
            m_mnu_itm_cut.Visible = bCutVisible;
            m_mnu_itm_copy.Visible = bCopyVisible;
            m_mnu_itm_paste.Visible = bPasteVisible;
			//Grille
			bool bGrilleVisible = MNU_GrilleShow();
			m_mnu_itm_grille.Visible = bGrilleVisible;
			if (bGrilleVisible)
			{
				bool bGrilleTjrsAlignerVisible = MNU_GrilleTjrsAlignerShow();
				m_mnu_itm_grille_tjrsAligner.Visible = bGrilleTjrsAlignerVisible;

				//AFFICHAGE
				bool bGrilleModeAffichageVisible = MNU_GrilleModeAffichageShow();
				m_mnu_itm_grille_affichage.Visible = bGrilleModeAffichageVisible;
				if (bGrilleModeAffichageVisible)
				{
					bool bGrilleDeplacementVisibleVisible = MNU_GrilleAfficherAuDeplacementShow();
					m_mnu_itm_grille_affichage_deplacement.Visible = bGrilleDeplacementVisibleVisible;

					bool bGrilleJamaisVisibleVisible = MNU_GrilleJamaisAfficherShow();
					m_mnu_itm_grille_affichage_jamais.Visible = bGrilleJamaisVisibleVisible;

					bool bGrilleTjrsVisibleVisible = MNU_GrilleToujoursAfficherShow();
					m_mnu_itm_grille_affichage_tjrs.Visible = bGrilleTjrsVisibleVisible;
				}

				//REPRESENTATION
				bool bGrilleModeRepresentationVisible = MNU_GrilleRepresentationShow();
				m_mnu_itm_grille_representation.Visible = bGrilleModeRepresentationVisible;
				if (bGrilleModeRepresentationVisible)
				{
					bool bGrilleModeRepresentationLignesVisible = MNU_GrilleRepresentationLignesShow();
					m_mnu_itm_grille_representation_lignes.Visible = bGrilleModeRepresentationLignesVisible;
					bool bGrilleModeRepresentationLignesPointilletsVisible = MNU_GrilleRepresentationLignesPointilletsShow();
					m_mnu_itm_grille_representation_pointillets.Visible = bGrilleModeRepresentationLignesPointilletsVisible;
					bool bGrilleModeRepresentationLignesDiscontinueVisible = MNU_GrilleRepresentationLignesDiscontinuesShow();
					m_mnu_itm_grille_representation_discontinues.Visible = bGrilleModeRepresentationLignesDiscontinueVisible;
					bool bGrilleModeRepresentationPointsVisible = MNU_GrilleRepresentationPointsShow();
					m_mnu_itm_grille_representation_points.Visible = bGrilleModeRepresentationPointsVisible;
					bool bGrilleModeRepresentationAnglesVisible = MNU_GrilleRepresentationAnglesShow();
					m_mnu_itm_grille_representation_angles.Visible = bGrilleModeRepresentationAnglesVisible;
				}

				//TAILLE
				bool bTailleGrilleVisible = MNU_GrilleTailleShow();
				m_mnu_itm_grille_taille.Visible = bTailleGrilleVisible;

				bool bCouleurGrilleVisible = MNU_GrilleCouleurShow();
				m_mnu_itm_grille_couleur.Visible = bCouleurGrilleVisible;

			}

			//AFFICHAGE DU DERNIER SEPARATEUR


			m_mnu.Show(PointToScreen(p));
		}


		private int m_nCacheSelectionCount;
		private int m_nCacheSelectionNbNotLocked;
		private bool m_bCacheEditeurSelectionne;


		private void m_mnu_Opening(object sender, CancelEventArgs e)
		{
			bool bShow = true;
			if (MenuInvoked != null)
				bShow = MenuInvoked(m_mnu);

			if (bShow && EffetFonduMenu)
			{
				m_mnu.Opacity = 0;
				m_bApparition = true;
				TimerFondu.Start();
			}
			else
				e.Cancel = true;
		}
		private void m_mnu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (EffetFonduMenu)
			{
				m_bApparition = false;
				TimerFondu.Start();
			}
		}

		#endregion

		#region Commun
		//AXE Z
		private void m_mnu_itm_bringToFront_Click(object sender, EventArgs e)
		{
			MNU_BringToFront();
		}
		public virtual void MNU_BringToFront()
		{
			//Element A bouger, parent
			//Dictionary<I2iObjetGraphique, I2iObjetGraphique> objs = new Dictionary<I2iObjetGraphique, I2iObjetGraphique>();
			foreach (I2iObjetGraphique obj in Selection)
			{
				if (obj.Parent == null || obj.IsLock)
					continue;
				else
					obj.Parent.BringToFront(obj);
				//objs.Add(obj, obj.Parent);
				//obj.Parent.RemoveChild(obj);
			}
			//foreach (I2iObjetGraphique obj in objs.Keys)
				//objs[obj].AddChild(obj);
            if (FrontBackChanged != null)
                FrontBackChanged(this, new EventArgs());
			Refresh();
		}
		public virtual bool MNU_BringToFrontShow()
		{
			return m_nCacheSelectionNbNotLocked > 0 && !m_bCacheEditeurSelectionne;
		}
		private void m_mnu_itm_bringToBack_Click(object sender, EventArgs e)
		{
			MNU_BringToBack();
		}
		public virtual void MNU_BringToBack()
		{
			foreach (I2iObjetGraphique obj in Selection)
			{
				if (obj.Parent == null || obj.IsLock)
					continue;
				else
					obj.Parent.FrontToBack(obj);
			}
			//Dictionary<I2iObjetGraphique, I2iObjetGraphique> objs = new Dictionary<I2iObjetGraphique, I2iObjetGraphique>();
			//GetElementsForBringToBack(objs, ObjetEdite);
			//foreach (I2iObjetGraphique obj in objs.Keys)
			//    objs[obj].AddChild(obj);
            if (FrontBackChanged!= null)
                FrontBackChanged(this, new EventArgs());
			Refresh();
		}
		public virtual bool MNU_BringToBackShow()
		{
			return m_nCacheSelectionNbNotLocked > 0 && !m_bCacheEditeurSelectionne;
		}
		private void GetElementsForBringToBack(Dictionary<I2iObjetGraphique, I2iObjetGraphique> objs, I2iObjetGraphique parent)
		{
			foreach (I2iObjetGraphique obj in parent.Childs)
			{
				if (obj.Parent == null || obj.IsLock)
					continue;
				if (obj.Childs.Length > 0)
					GetElementsForBringToBack(objs, obj);
				if (!Selection.Contains(obj))
				{
					objs.Add(obj, parent);
					obj.Parent.RemoveChild(obj);
                    
				}
			}
		}

		private void m_mnu_itm_delete_Click(object sender, EventArgs e)
		{
			MNU_Delete();
		}
		public virtual void MNU_Delete()
		{
			Selection_Supprimer();
		}
		public virtual bool MNU_DeleteShow()
		{

            return (m_nCacheSelectionNbNotLocked > 0 && !m_bCacheEditeurSelectionne && CanDeleteSelection());
                
           
		}
		private void m_mnu_itm_lock_Click(object sender, EventArgs e)
		{
			MNU_Lock();
		}
		public virtual void MNU_Lock()
		{
			if (Selection.ControlReference == null)
				return;
			//On se base sur le premier 
			bool bLock = !Selection.ControlReference.IsLock;
			bool bModified = false;
			foreach (I2iObjetGraphique itm in Selection)
			{
				if (!bModified && itm.IsLock != bLock)
					bModified = true;
				itm.IsLock = bLock;
			}
			if (bModified)
			{
				//Sert à réinitialiser les poignées
				m_selection = Selection;
				Refresh();
			}
		}
		public virtual bool MNU_LockShow()
		{
			return !m_bCacheEditeurSelectionne && m_nCacheSelectionCount > 0;
		}

        private void m_mnu_itm_cut_Click(object sender, EventArgs e)
        {
            MNU_Cut();
        }
        private void m_mnu_itm_copy_Click(object sender, EventArgs e)
        {
            MNU_Copy();
        }
        private void m_mnu_itm_paste_Click(object sender, EventArgs e)
        {
            MNU_Paste();
        }

        public virtual void MNU_Cut()
        {
            CutToClipBoard();
        }
        public virtual void MNU_Copy()
        {
            CopyToClipBoard();
        }
        public virtual void MNU_Paste()
        {
            PasteFromClipBoard();
        }
        public virtual bool MNU_CutShow()
        {
            return (!m_bCacheEditeurSelectionne && m_nCacheSelectionCount > 0 && CanDeleteSelection())&&(!m_bNoClipboard);
        }
        public virtual bool MNU_CopyShow()
        {
            return !m_bCacheEditeurSelectionne && m_nCacheSelectionCount > 0 &&(!m_bNoClipboard);
        }
        public virtual bool MNU_PasteShow()
        {
            return (Clipboard.ContainsData(GetType().ToString())&&(!m_bNoClipboard));
        }
		#endregion

		//Supprime les parents de l'objet dans la liste
		private void RemoveParents(ref List<I2iObjetGraphique> elements, I2iObjetGraphique objetRef)
		{
			I2iObjetGraphique ele = objetRef.Parent;
			if (ele == null)
				return;
			elements.Remove(ele);
			RemoveParents(ref elements, ele);
			return;
		}
		//Supprime les enfants de l'objet dans la liste
		private void RemoveEnfants(ref List<I2iObjetGraphique> elements, I2iObjetGraphique objetRef)
		{
			foreach (I2iObjetGraphique ele in objetRef.Childs)
			{
				elements.Remove(ele);
				RemoveEnfants(ref elements, ele);
			}
		}
		private void RemoveLocked(ref List<I2iObjetGraphique> elements)
		{
			for (int n = elements.Count - 1; n >= 0; n--)
				if (elements[n].IsLock)
					elements.RemoveAt(n);
		}

		private List<I2iObjetGraphique> GetCopieOfSelection(bool bWithLocked, bool bWithReferant)
		{
			List<I2iObjetGraphique> copie = new List<I2iObjetGraphique>();
			foreach (I2iObjetGraphique obj in m_selection)
			{
				if ((!bWithReferant && obj == Selection.ControlReference)
				|| (!bWithLocked && obj.IsLock))
					continue;
				copie.Add(obj);
			}
			return copie;
		}






        private bool MNUZoomIn_Show()
        {

            if (Echelle <m_fmaxZoom)
                return true;
            else
                return false;

        }

        
        private bool MNUZoomOut_Show()
        {

            if (Echelle > m_fminZoom)
                return true;
            else
                return false;

        }




        private bool MNUZoomAdjust_Show()
        {
            if (ObjetEdite == null)
                return false;
            if (ObjetEdite.Childs.Length > 0)
                return true;
            else
                return false;
        }

        private bool MNUZoomOrigine_Show()
        {
            if (Echelle != 1.0F)
                return true;
            else
                return false;
        }


        private void m_mnu_itm_zoomIn_Click(object sender, EventArgs e)
        {
            MNUZoomIn();           
        }

        private void MNUZoomIn()
        {
            m_bIsZooming = true;

            
            Point posRepere = GetLogicalPointFromDisplay(m_pointRightClick);
            
            posRepere.Offset(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
            Point ptScroll = new Point();

            if(Echelle < m_fmaxZoom) 
              this.Echelle = Echelle * 2.0F;

            ptScroll.X = (int)(Echelle * posRepere.X) - m_pointRightClick.X;
            ptScroll.Y = (int)(Echelle * posRepere.Y) - m_pointRightClick.Y;

                      
            AutoScrollPosition=(ptScroll);

            m_bIsZooming = false;
            Refresh();
        }



        
         
     
        private void m_mnu_itm_zoomOut_Click(object sender, EventArgs e)
        {

            MNUZoomOut();
            
        }

        private void MNUZoomOut()
        {
            m_bIsZooming = true;
            Point posRepere = GetLogicalPointFromDisplay(m_pointRightClick);
            
            posRepere.Offset(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
            Point ptScroll = new Point();

            if (Echelle > m_fminZoom)
                this.Echelle = Echelle / 2.0F;

            ptScroll.X = (int)(Echelle * posRepere.X) - m_pointRightClick.X;
            ptScroll.Y = (int)(Echelle * posRepere.Y) - m_pointRightClick.Y;

            AutoScrollPosition = (ptScroll);

            m_bIsZooming = false;
            Refresh();
        }

        private void m_mnu_itm_zoomAdjust_Click(object sender, EventArgs e)
        {
            MNUZoomAdjust();
        }



        private void m_mnu_itm_zoomOrigine_Click(object sender, EventArgs e)
        {
            MNUZoomOrigine();
        }


        private void MNUZoomOrigine()
        {
            m_bIsZooming = true;
            Point posRepere = GetLogicalPointFromDisplay(m_pointRightClick);

            posRepere.Offset(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);
            Point ptScroll = new Point();

            this.Echelle = 1.0F;

            ptScroll.X = (int)(Echelle * posRepere.X) - m_pointRightClick.X;
            ptScroll.Y = (int)(Echelle * posRepere.Y) - m_pointRightClick.Y;

            AutoScrollPosition = (ptScroll);

            m_bIsZooming = false;
            Refresh();
        }

        private void MNUZoomAdjust()
        {
            AjusterZoom();
        }

        //Si true, l'ajustement du zoom se fait sur les fils de l'objet
        //edité, sinon, il se fait sur l'objet édité
        protected virtual bool ShouldAjusteZoomSurFils()
        {
            return true;
        }


        public void AjusterZoom()
        {

            if (ObjetEdite == null)
                return;


            //calcul de la dimension du contenu de la fenêtre
            int nBottom = 0;
            int nRight = 0;
            int nLeft = int.MaxValue;
            int nTop = int.MaxValue;
            if (ObjetEdite.Childs.Length == 0 || !ShouldAjusteZoomSurFils())
            {
                nTop = ObjetEdite.RectangleAbsolu.Top;
                nLeft = ObjetEdite.RectangleAbsolu.Left;
                nBottom = ObjetEdite.RectangleAbsolu.Bottom;
                nRight = ObjetEdite.RectangleAbsolu.Right;
            }
            else
            {
                foreach (I2iObjetGraphique obj in ObjetEdite.Childs)
                {
                    nLeft = Math.Min(nLeft, obj.Position.X);
                    nRight = Math.Max(nRight, obj.Position.X + obj.Size.Width);
                    nTop = Math.Min(nTop, obj.Position.Y);
                    nBottom = Math.Max(nBottom, obj.Position.Y + obj.Size.Height);
                }
            }





            int nContentWidth = nRight - nLeft;
            int nContentHeight = nBottom - nTop;

            Rectangle rect = new Rectangle(nLeft, nTop, nContentWidth, nContentHeight);

            ZoomRectangle(rect);
        }


        void ZoomRectangle(Rectangle rect)
        {
            m_bIsZooming = true;

            double fSizeFactor = Math.Min(((double)this.Size.Width / ((double)rect.Width*Echelle)), ((double)this.Size.Height / ((double)rect.Height*Echelle)));
            
            float fScale =  m_fmaxZoom;
            while (((double)fScale > fSizeFactor)&& (fScale >= m_fminZoom))
            {
                fScale = fScale /2.0F;

            }
            fScale = (float)Math.Min(fSizeFactor * Echelle, m_fmaxZoom);

           /*   Point posRepere = GetMouseLogicalPoint(m_pointRightClick);
            
            posRepere.Offset(-ObjetEdite.Position.X, -ObjetEdite.Position.Y);*/
            Point ptScroll = new Point();

            this.Echelle = fScale;

            ptScroll.X = (int)(Echelle * rect.Left);
            ptScroll.Y = (int)(Echelle * rect.Top);
     
            AutoScrollPosition=(ptScroll);

            m_bIsZooming = false;
            Refresh();
            
   
        }


		#region Alignement
		public virtual bool MNU_AlignerShow()
		{
			return !m_bCacheEditeurSelectionne && m_nCacheSelectionNbNotLocked > 0;
		}

		//SUR X
		private void m_mnu_itm_aligner_sur_X_Haut_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurXHaut();
		}
		public virtual void MNU_AlignerSurXHaut()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			RemoveLocked(ref elements);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosY = ctrlRef.PositionAbsolue.Y;
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nPosY);
			}
			ctrlRef.PositionAbsolue = new Point(ctrlRef.PositionAbsolue.X, nPosY);

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurXHautShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_aligner_sur_X_Centre_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurXCentre();
		}
		public virtual void MNU_AlignerSurXCentre()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosYCentre = ctrlRef.PositionAbsolue.Y + (ctrlRef.Size.Height / 2);
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nPosYCentre - (obj.Size.Height / 2));
			}
			ctrlRef.PositionAbsolue = new Point(ctrlRef.PositionAbsolue.X, nPosYCentre - (ctrlRef.Size.Height / 2));

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurXCentreShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_aligner_sur_X_Bas_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurXBas();
		}
		public virtual void MNU_AlignerSurXBas()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosYBas = ctrlRef.PositionAbsolue.Y + ctrlRef.Size.Height;
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nPosYBas - obj.Size.Height);
			}
			ctrlRef.PositionAbsolue = new Point(ctrlRef.PositionAbsolue.X, nPosYBas - ctrlRef.Size.Height);

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurXBasShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		//SUR Y
		private void m_mnu_itm_aligner_sur_Y_Gauche_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurYGauche();
		}
		public virtual void MNU_AlignerSurYGauche()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosX = ctrlRef.PositionAbsolue.X;
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(nPosX, obj.PositionAbsolue.Y);
			}
			ctrlRef.PositionAbsolue = new Point(nPosX, ctrlRef.PositionAbsolue.Y);

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurYGaucheShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_aligner_sur_Y_Centre_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurYCentre();
		}
		public virtual void MNU_AlignerSurYCentre()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosXCentre = ctrlRef.PositionAbsolue.X + (ctrlRef.Size.Width / 2);
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(nPosXCentre - (obj.Size.Width / 2), obj.PositionAbsolue.Y);
			}
			ctrlRef.PositionAbsolue = new Point(nPosXCentre - (ctrlRef.Size.Width / 2), ctrlRef.PositionAbsolue.Y);

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurYCentreShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_aligner_sur_Y_Droite_Click(object sender, EventArgs e)
		{
			MNU_AlignerSurYDroite();
		}
		public virtual void MNU_AlignerSurYDroite()
		{
			I2iObjetGraphique ctrlRef = Selection.ControlReference;
			List<I2iObjetGraphique> elements = GetCopieOfSelection(false, false);
			//RemoveParents(ref elements, m_selection.ControlReference);
			int nPosXBas = ctrlRef.PositionAbsolue.X + ctrlRef.Size.Width;
			foreach (I2iObjetGraphique obj in elements)
			{
				if (obj == ctrlRef)
					continue;
				obj.PositionAbsolue = new Point(nPosXBas - obj.Size.Width, obj.PositionAbsolue.Y);
			}
			ctrlRef.PositionAbsolue = new Point(nPosXBas - ctrlRef.Size.Width, ctrlRef.PositionAbsolue.Y);

			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_AlignerSurYDroiteShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}

		//SUR GRILLE
		private void m_mnu_itm_aligner_correspondre_sur_Grille_Click(object sender, EventArgs e)
		{
			MNU_AlignerCorrespondreGrille();
		}
		public virtual void MNU_AlignerCorrespondreGrille()
		{
			Selection_CorrespondreSurGrille();
		}
		public virtual bool MNU_AlignerCorrespondreGrilleShow()
		{
			return true;
		}
		private void m_mnu_itm_aligner_positionner_sur_Grille_Click(object sender, EventArgs e)
		{
			MNU_AlignerPositionnerGrille();
		}
		public virtual void MNU_AlignerPositionnerGrille()
		{
			Selection_PositionnerSurGrille(true);
		}
		public virtual bool MNU_AlignerPositionnerGrilleShow()
		{
			return true;
		}
		private void m_mnu_itm_aligner_etendre_sur_Grille_Click(object sender, EventArgs e)
		{
			MNU_AlignerEtendreGrille();
		}
		public virtual void MNU_AlignerEtendreGrille()
		{
			Selection_EtendreSurGrille(true);
		}
		public virtual bool MNU_AlignerEtendreGrilleShow()
		{
			return true;
		}
		#endregion

		#region Répartition
		public virtual bool MNU_RepartirShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}

		//SUR X
		private void m_mnu_itm_repartir_sur_X_Auto_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurXAuto();
		}
		public virtual void MNU_RepartirSurXAuto()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);

			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);

			if (elements.Count == 2)
				return;

			List<I2iObjetGraphique> objs = elements;
			C2iObjetGraphiqueComparer.OrdonnerDeGaucheADroite(objs);
			int nbTranche = objs.Count - 1;

			int nXMin = objs[0].PositionAbsolue.X + objs[0].Size.Width / 2;
			int nXMax = objs[nbTranche].PositionAbsolue.X + objs[nbTranche].Size.Width / 2;

			int nLargeur = nXMax - nXMin;
			int nLargTranche = nLargeur / nbTranche;

			int nAxeCentral = nXMin + nLargTranche;
			for (int nEle = 0; nEle < nbTranche; nEle++)
			{
				I2iObjetGraphique obj = objs[nEle + 1];
				obj.PositionAbsolue = new Point(nAxeCentral - (obj.Size.Width / 2), obj.PositionAbsolue.Y);
				nAxeCentral += nLargTranche;
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurXAutoShow()
		{
			return true;
		}
		private void m_mnu_itm_repartir_sur_X_Droite_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurXDroite();
		}
		public virtual void MNU_RepartirSurXDroite()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);
			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);
			RemoveLocked(ref elements);

			I2iObjetGraphique ctrlSelec = Selection.ControlReference;
			int nX = ctrlSelec.PositionAbsolue.X + ctrlSelec.Size.Width;

			C2iObjetGraphiqueComparer.OrdonnerDeGaucheADroite(elements);
			elements.Remove(ctrlSelec);
			for (int n = 0; n < elements.Count; n++)
			{
				I2iObjetGraphique obj = elements[n];
				nX += Marge;
				obj.PositionAbsolue = new Point(nX, obj.PositionAbsolue.Y);
				nX += obj.Size.Width;
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurXDroiteShow()
		{
			return true;
		}
		private void m_mnu_itm_repartir_sur_X_Gauche_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurXGauche();
		}
		public virtual void MNU_RepartirSurXGauche()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);
			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);
			RemoveLocked(ref elements);

			I2iObjetGraphique ctrlSelec = Selection.ControlReference;
			int nX = ctrlSelec.PositionAbsolue.X;

			C2iObjetGraphiqueComparer.OrdonnerDeDroiteAGauche(elements);
			elements.Remove(ctrlSelec);
			for (int n = 0; n < elements.Count; n++)
			{
				I2iObjetGraphique obj = elements[n];
				nX -= (Marge + obj.Size.Width);
				obj.PositionAbsolue = new Point(nX, obj.PositionAbsolue.Y);
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurXGaucheShow()
		{
			return true;
		}
		//SUR Y
		private void m_mnu_itm_repartir_sur_Y_Auto_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurYAuto();
		}
		public virtual void MNU_RepartirSurYAuto()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);
			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);
			RemoveLocked(ref elements);

			if (elements.Count == 2)
				return;

			C2iObjetGraphiqueComparer.OrdonnerDeHautEnBas(elements);
			int nbTranche = elements.Count - 1;

			int nYMin = elements[0].PositionAbsolue.Y + elements[0].Size.Height / 2;
			int nYMax = elements[nbTranche].PositionAbsolue.Y + elements[nbTranche].Size.Height / 2;

			int nHauteur = nYMax - nYMin;
			int nHauteurTranche = nHauteur / nbTranche;

			int nAxeCentral = nYMin + nHauteurTranche;
			for (int nEle = 0; nEle < nbTranche; nEle++)
			{
				I2iObjetGraphique obj = elements[nEle + 1];
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nAxeCentral - (obj.Size.Height / 2));
				nAxeCentral += nHauteurTranche;
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurYAutoShow()
		{
			return true;
		}
		private void m_mnu_itm_repartir_sur_Y_Bas_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurYBas();
		}
		public virtual void MNU_RepartirSurYBas()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);
			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);
			RemoveLocked(ref elements);

			I2iObjetGraphique ctrlSelec = Selection.ControlReference;
			int nY = ctrlSelec.PositionAbsolue.Y + ctrlSelec.Size.Height;

			C2iObjetGraphiqueComparer.OrdonnerDeHautEnBas(elements);
			elements.Remove(ctrlSelec);
			for (int n = 0; n < elements.Count; n++)
			{
				I2iObjetGraphique obj = elements[n];
				nY += Marge;
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nY);
				nY += obj.Size.Height;
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurYBasShow()
		{
			return true;
		}
		private void m_mnu_itm_repartir_sur_Y_Haut_Click(object sender, EventArgs e)
		{
			MNU_RepartirSurYHaut();
		}
		public virtual void MNU_RepartirSurYHaut()
		{
			List<I2iObjetGraphique> elements = GetCopieOfSelection(true, true);
			RemoveParents(ref elements, Selection.ControlReference);
			RemoveEnfants(ref elements, Selection.ControlReference);
			RemoveLocked(ref elements);

			I2iObjetGraphique ctrlSelec = Selection.ControlReference;
			int nY = ctrlSelec.PositionAbsolue.Y;

			C2iObjetGraphiqueComparer.OrdonnerDeBasEnHaut(elements);
			elements.Remove(ctrlSelec);
			for (int n = 0; n < elements.Count; n++)
			{
				I2iObjetGraphique obj = elements[n];
				nY -= (Marge + obj.Size.Height);
				obj.PositionAbsolue = new Point(obj.PositionAbsolue.X, nY);
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RepartirSurYHautShow()
		{
			return true;
		}


		private int m_marge = 10;
		public int Marge
		{
			get
			{
				return m_marge;
			}
			set
			{
				m_marge = value;
			}
		}

		#endregion

		#region Taille
		private void m_mnu_itm_resize_Click(object sender, EventArgs e)
		{
			MNU_Redimentionner();
		}
		public virtual void MNU_Redimentionner()
		{
			I2iObjetGraphique ctrlSelec = m_selection.ControlReference;
			Size taille = ctrlSelec.Size;
			List<I2iObjetGraphique> objs = m_selection;
			for (int n = 0; n < objs.Count; n++)
			{
				I2iObjetGraphique obj = objs[n];
				if (obj == ctrlSelec)
					continue;
				obj.Size = taille;
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RedimentionnerShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_resize_largeur_Click(object sender, EventArgs e)
		{
			MNU_RedimentionnerLargeur();
		}
		public virtual void MNU_RedimentionnerLargeur()
		{
			I2iObjetGraphique ctrlSelec = m_selection.ControlReference;
			int nLargeur = ctrlSelec.Size.Width;
			List<I2iObjetGraphique> objs = m_selection;
			for (int n = 0; n < objs.Count; n++)
			{
				I2iObjetGraphique obj = objs[n];
				if (obj == ctrlSelec)
					continue;
				obj.Size = new Size(nLargeur, obj.Size.Height);
			}
			ElementModifie();
			Refresh();
		}
		public virtual bool MNU_RedimentionnerLargeurShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		private void m_mnu_itm_resize_hauteur_Click(object sender, EventArgs e)
		{
			MNU_RedimentionnerHauteur();
		}
		public virtual void MNU_RedimentionnerHauteur()
		{
			I2iObjetGraphique ctrlSelec = m_selection.ControlReference;
			int nHauteur = ctrlSelec.Size.Height;
			List<I2iObjetGraphique> objs = m_selection;
			for (int n = 0; n < objs.Count; n++)
			{
				I2iObjetGraphique obj = objs[n];
				if (obj == ctrlSelec)
					continue;
				obj.Size = new Size(obj.Size.Width, nHauteur);
			}
			Refresh();
		}
		public virtual bool MNU_RedimentionnerHauteurShow()
		{
			return m_nCacheSelectionNbNotLocked > 1;
		}
		#endregion

		//Marge
		public virtual bool MNU_MargeShow()
		{
			return true;
		}
		private void m_mnu_itm_marge_Click(object sender, EventArgs e)
		{
			CFormMarge.AfficherDialog(this);
		}


		#region Grille
		public virtual void MNU_GrilleTaille()
		{
			CFormTailleGrille.AfficherDialog(m_grille);
			Refresh();
		}
		public virtual bool MNU_GrilleTailleShow()
		{
			return true;
		}
		private void m_mnu_itm_grille_taille_Click(object sender, EventArgs e)
		{
			MNU_GrilleTaille();
		}


		public virtual bool MNU_GrilleShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}



		private void m_mnu_itm_grille_tjrsAligner_Click(object sender, EventArgs e)
		{
			MNU_GrilleTjrsAligner();
		}
		public virtual void MNU_GrilleTjrsAligner()
		{
			ToujoursAlignerSurLaGrille = !m_mnu_itm_grille_tjrsAligner.Checked;
		}
		public virtual bool MNU_GrilleTjrsAlignerShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}


		//AFFICHAGE
		public virtual bool MNU_GrilleModeAffichageShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_affichage_jamais_Click(object sender, EventArgs e)
		{
			MNU_GrilleJamaisAfficher();
		}
		public virtual void MNU_GrilleJamaisAfficher()
		{
			ModeAffichageGrille = EModeAffichageGrille.Jamais;
		}
		public virtual bool MNU_GrilleJamaisAfficherShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_affichage_tjrs_Click(object sender, EventArgs e)
		{
			MNU_GrilleToujoursAfficher();
		}
		public virtual void MNU_GrilleToujoursAfficher()
		{
			ModeAffichageGrille = EModeAffichageGrille.Toujours;
		}
		public virtual bool MNU_GrilleToujoursAfficherShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_affichage_deplacement_Click(object sender, EventArgs e)
		{
			MNU_GrilleAfficherAuDeplacement();
		}
		public virtual void MNU_GrilleAfficherAuDeplacement()
		{
			ModeAffichageGrille = EModeAffichageGrille.AuDeplacement;
		}
		public virtual bool MNU_GrilleAfficherAuDeplacementShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}


		//REPRESENTATION
		public virtual bool MNU_GrilleRepresentationShow()
		{
            return true;
            return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_representation_lignes_Click(object sender, EventArgs e)
		{
			MNU_GrilleRepresentationLignes();
		}
		public virtual void MNU_GrilleRepresentationLignes()
		{
            ModeRepresentationGrille = ERepresentationGrille.LignesContinues;
		}
		public virtual bool MNU_GrilleRepresentationLignesShow()
		{
			//return m_bCacheEditeurSelectionne;
            return true;
		}

		private void m_mnu_itm_grille_representation_points_Click(object sender, EventArgs e)
		{
			MNU_GrilleRepresentationPoints();
		}
		public virtual void MNU_GrilleRepresentationPoints()
		{
			ModeRepresentationGrille = ERepresentationGrille.Points;
		}
		public virtual bool MNU_GrilleRepresentationPointsShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_representation_angles_Click(object sender, EventArgs e)
		{
			MNU_GrilleRepresentationAngles();
		}
		public virtual void MNU_GrilleRepresentationAngles()
		{
			ModeRepresentationGrille = ERepresentationGrille.Angles;
		}
		public virtual bool MNU_GrilleRepresentationAnglesShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_representation_pointillets_Click(object sender, EventArgs e)
		{
			MNU_GrilleRepresentationLignesPointillets();
		}
		public virtual void MNU_GrilleRepresentationLignesPointillets()
		{
			ModeRepresentationGrille = ERepresentationGrille.LignesPointillets;
		}
		public virtual bool MNU_GrilleRepresentationLignesPointilletsShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_representation_discontinues_Click(object sender, EventArgs e)
		{
			MNU_GrilleRepresentationLignesDiscontinues();
		}
		public virtual void MNU_GrilleRepresentationLignesDiscontinues()
		{
			ModeRepresentationGrille = ERepresentationGrille.LignesDiscontinues;
		}
		public virtual bool MNU_GrilleRepresentationLignesDiscontinuesShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}

		private void m_mnu_itm_grille_couleur_Click(object sender, EventArgs e)
		{
			MNU_GrilleCouleur();
		}
		public virtual void MNU_GrilleCouleur()
		{
			m_colorDialog.Color = GrilleAlignement.Couleur;
			m_colorDialog.ShowDialog();
			GrilleAlignement.Couleur = m_colorDialog.Color;
		}
		public virtual bool MNU_GrilleCouleurShow()
		{
            return true;
			//return m_bCacheEditeurSelectionne;
		}
		#endregion



		#endregion

		#region IControlALockEdition Membres
		private bool m_bLockEdition = false;

		public virtual bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				if (value != m_bLockEdition)
				{
					m_bLockEdition = value;
					Refresh();
					//List<I2iObjetGraphique> selec = (List<I2iObjetGraphique>)Selection;
					//Selection.Clear();
					//Selection.AddRange(selec);
					if (OnChangeLockEdition != null)
						OnChangeLockEdition(this, new EventArgs());
				}
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion

		/// <summary>
		/// Permet de passer des objets au serializable se chargeant de cloner les
		/// objet. C'est utile pour passer par exemple un contexte de données
		/// </summary>
		/// <param name="objets"></param>
		public virtual void AddObjectsForClonerSerializer(Dictionary<Type, object> objets)
		{
		}

        private void m_timerRefresh_Tick(object sender, EventArgs e)
        {
            m_timerRefresh.Stop();
            try
            {
                Refresh();
            }
            catch { }
            
        }

        //Ajoute des données aux données envoyées dans un drag & drop
        public virtual void CompleteDragDropData(DataObject datas, I2iObjetGraphique[] objetsMisDansLeDragDropData)
        {
        }

        private void m_timerTooltip_Tick(object sender, EventArgs e)
        {
            m_timerTooltip.Stop();
            if (ObjetEdite != null)
            {
                Point pt = PointToClient(Cursor.Position);
                Point ptLogique = GetLogicalPointFromDisplay(pt);
                I2iObjetGraphique objet = ObjetEdite.SelectionnerElementDuDessus(ptLogique);
                if (objet != null && objet.TooltipText != "")
                {
                    m_tooltip.Show(objet.TooltipText, this, pt.X, pt.Y, 10000);
                }
            }
        }
	}

	public delegate bool EventHandlerPanelEditionGraphiqueSuppression(List<I2iObjetGraphique> objs);
	public delegate bool EventHandlerInvokationMenu(ContextMenuStrip menu);

	

}
