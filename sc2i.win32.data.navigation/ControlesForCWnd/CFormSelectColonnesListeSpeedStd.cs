using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.win32.data.dynamic;


namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CFormEditProprieteListeColonnes.
	/// </summary>
	public class CFormSelectColonnesListeSpeedStd : System.Windows.Forms.Form
	{
		private Color m_backColor = Color.Transparent;
		private Color m_textColor = Color.Transparent;
		private List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> m_listeColonnes = null;
		private Type m_typeObjetEdite = null;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Panel m_panelTotal;
		private System.Windows.Forms.Panel m_panelGauche;
		private CWndLinkStd m_btnRemove;
		private CWndLinkStd m_btnAdd;
        private ListBox m_wndListeColonnes;
        private Button m_btnBas;
        private Panel m_panelInfo;
        private NumericUpDown m_numUpLargeur;
        private Label label3;
        private TextBox m_txtTitre;
        private Label label1;
        private Label label2;
        private Panel m_panelComboChamp;
        private Label m_labelChamp;
        private Button m_btnSelectChampDynamique;
		private Button m_btnHaut;
		//private IContainer components;

        public CFormSelectColonnesListeSpeedStd()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				/*if(components != null)
				{
					components.Dispose();
				}*/
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSelectColonnesListeSpeedStd));
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_panelInfo = new System.Windows.Forms.Panel();
            this.m_panelComboChamp = new System.Windows.Forms.Panel();
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_btnSelectChampDynamique = new System.Windows.Forms.Button();
            this.m_numUpLargeur = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtTitre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelGauche = new System.Windows.Forms.Panel();
            this.m_btnBas = new System.Windows.Forms.Button();
            this.m_btnHaut = new System.Windows.Forms.Button();
            this.m_btnRemove = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_wndListeColonnes = new System.Windows.Forms.ListBox();
            this.m_panelTotal.SuspendLayout();
            this.m_panelInfo.SuspendLayout();
            this.m_panelComboChamp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpLargeur)).BeginInit();
            this.m_panelGauche.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOk.Location = new System.Drawing.Point(193, 280);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 24);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(284, 280);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(74, 24);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Controls.Add(this.m_panelInfo);
            this.m_panelTotal.Controls.Add(this.m_panelGauche);
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(656, 280);
            this.m_panelTotal.TabIndex = 4;
            // 
            // m_panelInfo
            // 
            this.m_panelInfo.Controls.Add(this.m_panelComboChamp);
            this.m_panelInfo.Controls.Add(this.m_numUpLargeur);
            this.m_panelInfo.Controls.Add(this.label2);
            this.m_panelInfo.Controls.Add(this.label3);
            this.m_panelInfo.Controls.Add(this.m_txtTitre);
            this.m_panelInfo.Controls.Add(this.label1);
            this.m_panelInfo.Location = new System.Drawing.Point(239, 12);
            this.m_panelInfo.Name = "m_panelInfo";
            this.m_panelInfo.Size = new System.Drawing.Size(327, 262);
            this.m_panelInfo.TabIndex = 3;
            this.m_panelInfo.Visible = false;
            // 
            // m_panelComboChamp
            // 
            this.m_panelComboChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChamp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChamp.Controls.Add(this.m_labelChamp);
            this.m_panelComboChamp.Controls.Add(this.m_btnSelectChampDynamique);
            this.m_panelComboChamp.Location = new System.Drawing.Point(71, 60);
            this.m_panelComboChamp.Name = "m_panelComboChamp";
            this.m_panelComboChamp.Size = new System.Drawing.Size(246, 21);
            this.m_panelComboChamp.TabIndex = 8;
            // 
            // m_labelChamp
            // 
            this.m_labelChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChamp.BackColor = System.Drawing.Color.White;
            this.m_labelChamp.Location = new System.Drawing.Point(0, 0);
            this.m_labelChamp.Name = "m_labelChamp";
            this.m_labelChamp.Size = new System.Drawing.Size(226, 17);
            this.m_labelChamp.TabIndex = 2;
            this.m_labelChamp.Text = "label1";
            this.m_labelChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnSelectChampDynamique
            // 
            this.m_btnSelectChampDynamique.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSelectChampDynamique.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnSelectChampDynamique.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSelectChampDynamique.Image")));
            this.m_btnSelectChampDynamique.Location = new System.Drawing.Point(226, 0);
            this.m_btnSelectChampDynamique.Name = "m_btnSelectChampDynamique";
            this.m_btnSelectChampDynamique.Size = new System.Drawing.Size(17, 17);
            this.m_btnSelectChampDynamique.TabIndex = 3;
            this.m_btnSelectChampDynamique.TabStop = false;
            this.m_btnSelectChampDynamique.UseVisualStyleBackColor = false;
            this.m_btnSelectChampDynamique.Click += new System.EventHandler(this.m_btnSelectChampDynamique_Click);
            // 
            // m_numUpLargeur
            // 
            this.m_numUpLargeur.Location = new System.Drawing.Point(71, 33);
            this.m_numUpLargeur.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numUpLargeur.Name = "m_numUpLargeur";
            this.m_numUpLargeur.Size = new System.Drawing.Size(60, 20);
            this.m_numUpLargeur.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Field|140";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width|141";
            // 
            // m_txtTitre
            // 
            this.m_txtTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTitre.Location = new System.Drawing.Point(71, 7);
            this.m_txtTitre.Name = "m_txtTitre";
            this.m_txtTitre.Size = new System.Drawing.Size(245, 20);
            this.m_txtTitre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title|139";
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Controls.Add(this.m_btnBas);
            this.m_panelGauche.Controls.Add(this.m_btnHaut);
            this.m_panelGauche.Controls.Add(this.m_btnRemove);
            this.m_panelGauche.Controls.Add(this.m_btnAdd);
            this.m_panelGauche.Controls.Add(this.m_wndListeColonnes);
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(233, 280);
            this.m_panelGauche.TabIndex = 2;
            // 
            // m_btnBas
            // 
            this.m_btnBas.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBas.Image")));
            this.m_btnBas.Location = new System.Drawing.Point(173, 253);
            this.m_btnBas.Name = "m_btnBas";
            this.m_btnBas.Size = new System.Drawing.Size(27, 22);
            this.m_btnBas.TabIndex = 6;
            this.m_btnBas.UseVisualStyleBackColor = true;
            this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
            // 
            // m_btnHaut
            // 
            this.m_btnHaut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHaut.Image")));
            this.m_btnHaut.Location = new System.Drawing.Point(199, 253);
            this.m_btnHaut.Name = "m_btnHaut";
            this.m_btnHaut.Size = new System.Drawing.Size(27, 22);
            this.m_btnHaut.TabIndex = 5;
            this.m_btnHaut.UseVisualStyleBackColor = true;
            this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnRemove.Location = new System.Drawing.Point(87, 12);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(78, 22);
            this.m_btnRemove.TabIndex = 4;
            this.m_btnRemove.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnRemove.LinkClicked += new System.EventHandler(this.m_btnRemove_LinkClicked);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAdd.Location = new System.Drawing.Point(3, 12);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(78, 22);
            this.m_btnAdd.TabIndex = 3;
            this.m_btnAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAdd.LinkClicked += new System.EventHandler(this.m_btnAdd_LinkClicked);
            // 
            // m_wndListeColonnes
            // 
            this.m_wndListeColonnes.DisplayMember = "Titre";
            this.m_wndListeColonnes.FormattingEnabled = true;
            this.m_wndListeColonnes.Location = new System.Drawing.Point(3, 39);
            this.m_wndListeColonnes.Name = "m_wndListeColonnes";
            this.m_wndListeColonnes.Size = new System.Drawing.Size(224, 212);
            this.m_wndListeColonnes.TabIndex = 3;
            this.m_wndListeColonnes.SelectedIndexChanged += new System.EventHandler(this.m_wndListeColonnes_SelectedIndexChanged);
            // 
            // CFormSelectColonnesListeSpeedStd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 309);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelTotal);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnAnnuler);
            this.MinimizeBox = false;
            this.Name = "CFormSelectColonnesListeSpeedStd";
            this.Text = "Rows|218";
            this.Load += new System.EventHandler(this.CFormEditProprieteListeColonnes_Load);
            this.m_panelTotal.ResumeLayout(false);
            this.m_panelInfo.ResumeLayout(false);
            this.m_panelInfo.PerformLayout();
            this.m_panelComboChamp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpLargeur)).EndInit();
            this.m_panelGauche.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public Type TypeEdite
		{
			get
			{
				return m_typeObjetEdite;
			}
			set
			{
				m_typeObjetEdite = value;
			}
		}


		public List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> EditeColonnes(List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> liste)
		{
			List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> lstCopie = new List<C2iWndListeSpeedStandard.CColonneListeSpeedStd>();
			foreach (C2iWndListeSpeedStandard.CColonneListeSpeedStd col in liste)
			{
				lstCopie.Add((C2iWndListeSpeedStandard.CColonneListeSpeedStd)CCloner2iSerializable.Clone(col));
			}
			m_listeColonnes = lstCopie;

			if ( ShowDialog() == DialogResult.OK )
				return m_listeColonnes;
			return m_listeColonnes;
		}

		private void CFormEditProprieteListeColonnes_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
			FillListeColonnes();
			
		}

		private void FillListeColonnes()
		{
			m_wndListeColonnes.BeginUpdate();
			m_wndListeColonnes.Items.Clear();
			foreach (C2iWndListeSpeedStandard.CColonneListeSpeedStd col in m_listeColonnes)
			{
				m_wndListeColonnes.Items.Add(col);
			}
			m_wndListeColonnes.EndUpdate();
		}



		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			ValideModifs();
			m_listeColonnes.Clear();
			foreach ( C2iWndListeSpeedStandard.CColonneListeSpeedStd col in m_wndListeColonnes.Items )
				m_listeColonnes.Add ( col );
			DialogResult = DialogResult.OK;
			Close();
		}

		//----------------------------------------------------
		private void m_btnRemove_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeColonnes.SelectedIndex >= 0)
			{
				C2iWndListeSpeedStandard.CColonneListeSpeedStd col = (C2iWndListeSpeedStandard.CColonneListeSpeedStd)m_wndListeColonnes.Items[m_wndListeColonnes.SelectedIndex];
				if (CFormAlerte.Afficher(I.T("Delete the column @1 ?|30034",  col.Titre ),
					EFormAlerteType.Question) == DialogResult.Yes)
				{
					m_wndListeColonnes.Items.RemoveAt(m_wndListeColonnes.SelectedIndex);
				}
			}
		}

		//------------------------------------------------------
		private void m_btnAdd_LinkClicked(object sender, EventArgs e)
		{
			ValideModifs();
			C2iWndListeSpeedStandard.CColonneListeSpeedStd col = new C2iWndListeSpeedStandard.CColonneListeSpeedStd();
			col.Titre = "New column";
			int nIndex = m_wndListeColonnes.Items.Add(col);
			m_wndListeColonnes.SelectedIndex = nIndex;
		}

		//------------------------------------------------------
		private void m_btnBas_Click(object sender, EventArgs e)
		{
			int nIndex = m_wndListeColonnes.SelectedIndex;
			if (nIndex >= 0 && nIndex < m_wndListeColonnes.Items.Count - 2)
			{
				object item = m_wndListeColonnes.Items[nIndex];
				m_wndListeColonnes.Items.RemoveAt(nIndex);
				m_wndListeColonnes.Items.Insert(nIndex + 1, item);
				m_wndListeColonnes.SelectedIndex = nIndex + 1;
			}
		}

		//------------------------------------------------------
		private void m_btnHaut_Click(object sender, EventArgs e)
		{
			int nIndex = m_wndListeColonnes.SelectedIndex;
			if (nIndex > 1)
			{
				object item = m_wndListeColonnes.Items[nIndex];
				m_wndListeColonnes.Items.RemoveAt(nIndex);
				m_wndListeColonnes.Items.Insert(nIndex - 1, item);
				m_wndListeColonnes.SelectedIndex = nIndex - 1;
			}
		}

	
		//------------------------------------------------------
		C2iWndListeSpeedStandard.CColonneListeSpeedStd m_colonneAffichee = null;
		private void ValideModifs()
		{
			if ( m_colonneAffichee != null )
			{
				m_colonneAffichee.Titre = m_txtTitre.Text;
				m_colonneAffichee.Width = (int)m_numUpLargeur.Value;
                //m_colonneAffichee.InfoChampDynamique = 
				RefreshListe();
			}
		}

		//------------------------------------------------------
		private bool m_bIsRefreshing = false;
		private void RefreshListe()
		{
			m_bIsRefreshing = true;
			m_wndListeColonnes.BeginUpdate();
			for (int nCol = 0; nCol < m_wndListeColonnes.Items.Count; nCol++)
			{
				C2iWndListeSpeedStandard.CColonneListeSpeedStd col = (C2iWndListeSpeedStandard.CColonneListeSpeedStd)m_wndListeColonnes.Items[nCol];
				m_wndListeColonnes.Items[nCol] = "...";
				m_wndListeColonnes.Items[nCol] = col;
			}
			m_wndListeColonnes.EndUpdate();
			m_bIsRefreshing = false;
		}

		//------------------------------------------------------
		private void AfficheColonne(C2iWndListeSpeedStandard.CColonneListeSpeedStd colonne)
		{
			ValideModifs();
			m_colonneAffichee = colonne;
			if ( colonne == null )
			{
				m_panelInfo.Visible = false;
			}
			else
			{
				m_panelInfo.Visible = true;
				m_txtTitre.Text = colonne.Titre;
				m_numUpLargeur.Value = colonne.Width;
                m_labelChamp.Text = colonne.InfoChampDynamique != null ? colonne.InfoChampDynamique.NomChamp : "";
			}
		}

		//-------------------------------------------------------------------------------
		private void m_wndListeColonnes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_wndListeColonnes.SelectedIndex >= 0 && !m_bIsRefreshing)
			{
				AfficheColonne((C2iWndListeSpeedStandard.CColonneListeSpeedStd)m_wndListeColonnes.Items[m_wndListeColonnes.SelectedIndex]);
			}
		}


        private void m_btnSelectChampDynamique_Click(object sender, EventArgs e)
        {
            CInfoStructureDynamique infoStructure = CInfoStructureDynamique.GetStructure ( m_typeObjetEdite, 1 );
            CInfoChampDynamique champSel = CFormSelectChampParentPourStructure.SelectPropriete(infoStructure);
            if (champSel != null)
            {
                m_colonneAffichee.InfoChampDynamique = champSel;
                m_labelChamp.Text = champSel == null ? "" : champSel.NomChamp;
            }
            /*Rectangle rect = m_panelComboChamp.RectangleToScreen(new Rectangle(0, m_panelComboChamp.Height, m_panelComboChamp.Width, 230));
            bool bCancel = false;
            CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp(
                rect, 
                m_typeObjetEdite,
                new CFournisseurProprietesForFiltreDynamique(),
                ref bCancel,
                null, 
                null);
            if (!bCancel)
            {
                m_colonneAffichee.InfoChampDynamique = champ;
                m_labelChamp.Text = champ == null ? "[NOT DEFINED]" : champ.Nom;
            }*/

        }
	}

	[AutoExec("Autoexec")]
	public class CEditeurColonnesListeSpeedStd : IEditeurColonnesListeSpeedStd
	{
		private C2iWndListeSpeedStandard m_listeEditee = null;

		//---------------------------------------------------
		public CEditeurColonnesListeSpeedStd()
		{
		}

		//---------------------------------------------------
		public static void Autoexec()
		{
            CColumnsPropertyEditor.SetTypeEditeur(typeof(CEditeurColonnesListeSpeedStd));
		}

		//---------------------------------------------------
        public CEditeurColonnesListeSpeedStd(C2iWndListeSpeedStandard listeEditee)
		{
			m_listeEditee = listeEditee;
		}

		//---------------------------------------------------
		public C2iWndListeSpeedStandard ListeEditee
		{
			get
			{
				return m_listeEditee;
			}
			set
			{
				m_listeEditee = value;
			}
		}

		//--------------------------------------------------------------------------------------
		public List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> EditeColonnes()
		{
			CFormSelectColonnesListeSpeedStd form = new CFormSelectColonnesListeSpeedStd();
			if ( m_listeEditee == null || m_listeEditee.SourceFormula == null )
			{
				CFormAlerte.Afficher(I.T("Indicate source formula before editing columns|30035"), EFormAlerteType.Exclamation);
				return m_listeEditee.Columns;
			}
			form.TypeEdite = m_listeEditee.SourceFormula.TypeDonnee.TypeDotNetNatif;
			List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> newListe = form.EditeColonnes(m_listeEditee.Columns);
			form.Dispose();
			return newListe;
		}
	}

		
}
