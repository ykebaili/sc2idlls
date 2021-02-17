using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;

namespace sc2i.formulaire.win32
{
	/// <summary>
	/// Description résumée de CFormSelectChampPopup.
	/// </summary>
	public delegate bool BeforeIntegrerChampEventHandler ( CDefinitionProprieteDynamique def );

	public class CFormSelectChampPopupBase : System.Windows.Forms.Form
	{
		private CDefinitionProprieteDynamique m_definitionRacineDeChamp = null;
		private CDefinitionProprieteDynamique m_definitionSelectionnee = null;
		private CRemplisseurArbreStructureDynamique m_remplisseur = null;
		private CObjetPourSousProprietes m_objetPourSousProprietes = null;
		private IFournisseurProprietesDynamiques m_fournisseur = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAucun;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.TreeView m_arbre;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormSelectChampPopupBase()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAucun = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_btnAucun);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_arbre);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 296);
            this.panel1.TabIndex = 3;
            // 
            // m_btnAucun
            // 
            this.m_btnAucun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAucun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAucun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAucun.Location = new System.Drawing.Point(26, 272);
            this.m_btnAucun.Name = "m_btnAucun";
            this.m_btnAucun.Size = new System.Drawing.Size(56, 24);
            this.m_btnAucun.TabIndex = 2;
            this.m_btnAucun.Text = "None|19";
            this.m_btnAucun.UseVisualStyleBackColor = false;
            this.m_btnAucun.Click += new System.EventHandler(this.m_btnAucun_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOk.Location = new System.Drawing.Point(94, 272);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(56, 24);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(158, 272);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(56, 24);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_arbre
            // 
            this.m_arbre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_arbre.HideSelection = false;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(238, 272);
            this.m_arbre.TabIndex = 0;
            // 
            // CFormSelectChampPopup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(240, 296);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CFormSelectChampPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CFormSelectChampPopup";
            this.Load += new System.EventHandler(this.CFormSelectChampPopup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void CFormSelectChampPopup_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            if (m_remplisseur != null)
				m_remplisseur.FillArbreChamps();
		}

		/// /////////////////////////////////////////////////////////////////
		public CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietes;
			}
			set
			{
				m_objetPourSousProprietes = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public event BeforeIntegrerChampEventHandler BeforeIntegreChamp;



		/// /////////////////////////////////////////////////////////////////
		public virtual void Init ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur, CDefinitionProprieteDynamique definitionRacineDeChamps )
		{
			m_objetPourSousProprietes = objetPourSousProprietes;
			m_definitionRacineDeChamp = definitionRacineDeChamps;
			m_fournisseur = fournisseur;
			m_remplisseur = new CRemplisseurArbreStructureDynamique ( m_arbre, m_objetPourSousProprietes, m_fournisseur, null, definitionRacineDeChamps );
			m_remplisseur.m_beforeAddNode +=new BeforeAddNode(m_remplisseur_m_beforeAddNode);
		}


		/// /////////////////////////////////////////////////////////////////
		public static  CDefinitionProprieteDynamique SelectDefinitionChamp
			( 
			Rectangle position, 
			CObjetPourSousProprietes objetPourSousProprietes,
			IFournisseurProprietesDynamiques fournisseur,
			ref bool bCancel,
			BeforeIntegrerChampEventHandler fonctionFiltre,
			CDefinitionProprieteDynamique definitionRacineDeChamps
			)
		{
			CFormSelectChampPopupBase form = new CFormSelectChampPopupBase();
			form.m_definitionRacineDeChamp = definitionRacineDeChamps;
			if ( fonctionFiltre != null )
				form.BeforeIntegreChamp += fonctionFiltre;
			if ( objetPourSousProprietes == null )
				return null;
			form.Init ( objetPourSousProprietes, fournisseur, definitionRacineDeChamps );
			form.Left = position.Left;
			form.Top = position.Top;
			form.Width = position.Width;
			form.Height = position.Height;
			DialogResult dResult = form.ShowDialog();
			bCancel = true;
			CDefinitionProprieteDynamique def = null;
			if ( dResult == DialogResult.OK )
			{
				def = form.m_definitionSelectionnee;
				bCancel = false;
			}
			if ( dResult == DialogResult.No )
			{
				bCancel = false;
			}
			form.Dispose();
			return def;
		}

		public CDefinitionProprieteDynamique SelectPropriete ( 
			CDefinitionProprieteDynamique propSelectionnee 
			)
		{
			return SelectPropriete ( propSelectionnee, null );
		}

		/// /////////////////////////////////////////////////////////////////
		//Implémentation de ISelectionneurProprieteDynamique
		public CDefinitionProprieteDynamique SelectPropriete 
			( 
			CDefinitionProprieteDynamique propSelectionnee,
			CDefinitionProprieteDynamique definitionRacineDeChamps
			)
		{
			CFormSelectChampPopupBase form = new CFormSelectChampPopupBase();
			form.Init ( m_objetPourSousProprietes, m_fournisseur, definitionRacineDeChamps );
			form.StartPosition = FormStartPosition.CenterParent;
			DialogResult dResult = form.ShowDialog();
			if ( dResult == DialogResult.OK )
			{
				propSelectionnee = form.m_definitionSelectionnee;
			}
			if ( dResult == DialogResult.No )
			{
				propSelectionnee = null;
			}
			return propSelectionnee;
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbre.SelectedNode;
			if ( node == null || !(node.Tag is CDefinitionProprieteDynamique) )
				return;
			m_definitionSelectionnee = (CDefinitionProprieteDynamique)node.Tag;
			while ( node.Parent != null )
			{
				node = node.Parent;
				if ( node.Tag is CDefinitionProprieteDynamique )
					m_definitionSelectionnee.InsereParent ( (CDefinitionProprieteDynamique)node.Tag );
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnAucun_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.No;
			Close();
		}

		private void m_remplisseur_m_beforeAddNode(CDefinitionProprieteDynamique def, TreeNode nodeParent, ref bool bAjouter)
		{
			if ( BeforeIntegreChamp != null )
			{
				bAjouter = BeforeIntegreChamp ( def );
			}
		}
	}
}
