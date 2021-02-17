using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableFiltreCalculee.
	/// </summary>
	public class CFormEditOrigineChampCustom : System.Windows.Forms.Form
	{
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = new CFournisseurPropDynStd ( true );
		private Type m_typeDonnees = null;
		private C2iChampExport m_champ = null;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
        protected CExtStyle m_ExtStyle1;
		private GlacialList m_wndListeChamps;
		private Label label2;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditOrigineChampCustom()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
			m_wndListeChamps.CheckBoxes = true;
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
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditOrigineChampCustom));
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndListeChamps = new sc2i.win32.common.GlacialList();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.m_wndListeChamps);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(446, 258);
            this.m_ExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Custom fields|20000";
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.AllowColumnResize = true;
            this.m_wndListeChamps.AllowMultiselect = false;
            this.m_wndListeChamps.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeChamps.AlternatingColors = false;
            this.m_wndListeChamps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_wndListeChamps.AutoHeight = true;
            this.m_wndListeChamps.AutoSort = false;
            this.m_wndListeChamps.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeChamps.CanChangeActivationCheckBoxes = false;
            this.m_wndListeChamps.CheckBoxes = false;
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Field";
            glColumn1.Propriete = "Nom";
            glColumn1.Text = "Field|60";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 200;
            this.m_wndListeChamps.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1});
            this.m_wndListeChamps.ContexteUtilisation = "";
            this.m_wndListeChamps.EnableCustomisation = false;
            this.m_wndListeChamps.FocusedItem = null;
            this.m_wndListeChamps.FullRowSelect = true;
            this.m_wndListeChamps.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_wndListeChamps.GridColor = System.Drawing.SystemColors.ControlLight;
            this.m_wndListeChamps.HeaderHeight = 22;
            this.m_wndListeChamps.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeChamps.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeChamps.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeChamps.HeaderVisible = true;
            this.m_wndListeChamps.HeaderWordWrap = false;
            this.m_wndListeChamps.HotColumnIndex = -1;
            this.m_wndListeChamps.HotItemIndex = -1;
            this.m_wndListeChamps.HotTracking = false;
            this.m_wndListeChamps.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeChamps.ImageList = null;
            this.m_wndListeChamps.ItemHeight = 17;
            this.m_wndListeChamps.ItemWordWrap = false;
            this.m_wndListeChamps.ListeSource = null;
            this.m_wndListeChamps.Location = new System.Drawing.Point(11, 27);
            this.m_wndListeChamps.MaxHeight = 17;
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeChamps.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeChamps.ShowBorder = true;
            this.m_wndListeChamps.ShowFocusRect = true;
            this.m_wndListeChamps.Size = new System.Drawing.Size(236, 205);
            this.m_wndListeChamps.SortIndex = 0;
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndListeChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndListeChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeChamps.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeChamps.TabIndex = 3;
            this.m_wndListeChamps.Text = "glacialList1";
            this.m_wndListeChamps.TrierAuClicSurEnteteColonne = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(253, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 95);
            this.m_ExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4;
            this.label2.Text = "Check fields you want to add to the table|20001";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.c2iPanelOmbre1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 302);
            this.m_ExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 20;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(243, 272);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 22;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(115, 272);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 21;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(459, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 302);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // CFormEditOrigineChampCustom
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(462, 302);
            this.Controls.Add(this.panel1);
            this.Name = "CFormEditOrigineChampCustom";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Computed field|100";
            this.Load += new System.EventHandler(this.CFormEditVariableFiltreCalculee_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void c2iPanelOmbre2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void CFormEditVariableFiltreCalculee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

			CRoleChampCustom role = CRoleChampCustom.GetRoleForType(m_typeDonnees);
			if (role != null)
			{
				CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(CSc2iWin32DataClient.ContexteCourant, role.CodeRole);
				listeChamps.Tri = CChampCustom.c_champNom;
				m_wndListeChamps.ListeSource = listeChamps;

				Dictionary<int, bool> lstChecks = new Dictionary<int, bool>();
				if (!(m_champ.Origine is C2iOrigineChampExportChampCustom))
					m_champ.Origine = new C2iOrigineChampExportChampCustom();
				m_champ.NomChamp = "CUSTOM_FIELDS";
				C2iOrigineChampExportChampCustom origineChamp = (C2iOrigineChampExportChampCustom)m_champ.Origine;
				foreach (int nId in origineChamp.IdsChampCustom)
					lstChecks[nId] = true;
				for (int nChamp = 0; nChamp < listeChamps.Count; nChamp++)
				{
					CChampCustom champ = (CChampCustom)listeChamps[nChamp];
					if (lstChecks.ContainsKey(champ.Id))
						m_wndListeChamps.CheckItem(nChamp);
				}
			}
		}

		/// //////////////////////////////////////////////////////
		private void Init ( C2iChampExport champ, Type typeDonnees )
		{
			m_champ = champ;
			m_typeDonnees = typeDonnees;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeChamp ( C2iChampExport champ, Type typeDonnees )
		{
			CFormEditOrigineChampCustom  form = new CFormEditOrigineChampCustom();
			form.Init ( champ, typeDonnees );
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		private void m_btnAnnuler_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnOk_Click(object sender, EventArgs e)
		{
			if ( !(m_wndListeChamps.ListeSource is CListeObjetsDonnees) )
				return;
			CListeObjetsDonnees listeChamps = (CListeObjetsDonnees)m_wndListeChamps.ListeSource;
			List<int> nIdsChecked = new List<int>();
			foreach (CChampCustom champ in m_wndListeChamps.CheckedItems)
			{
				nIdsChecked.Add(champ.Id);
			}
			C2iOrigineChampExportChampCustom origine = new C2iOrigineChampExportChampCustom(nIdsChecked.ToArray());
			m_champ.Origine = origine;
			m_champ.NomChamp = "CUSTOM_FIELDS";

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
