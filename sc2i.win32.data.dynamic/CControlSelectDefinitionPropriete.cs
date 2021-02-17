using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CControlSelectDefinitionPropriete.
	/// </summary>
	public class CControlSelectDefinitionPropriete : System.Windows.Forms.UserControl
	{
		private CDefinitionProprieteDynamique m_definitionRacineDeChamps = null;
		private IFournisseurProprietesDynamiques m_fournisseur = null;
		private Type m_typeInterroge = null;
		private CDefinitionProprieteDynamique m_definition;
		private System.Windows.Forms.Label m_labelChamp;
		private System.Windows.Forms.Button m_boutonDropList;
		private sc2i.win32.common.C2iPanel m_panel;
		private System.ComponentModel.IContainer components;

		public CControlSelectDefinitionPropriete()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControlSelectDefinitionPropriete));
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_panel = new sc2i.win32.common.C2iPanel(this.components);
            this.m_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_labelChamp
            // 
            this.m_labelChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChamp.BackColor = System.Drawing.Color.White;
            this.m_labelChamp.Location = new System.Drawing.Point(0, 0);
            this.m_labelChamp.Name = "m_labelChamp";
            this.m_labelChamp.Size = new System.Drawing.Size(378, 17);
            this.m_labelChamp.TabIndex = 4;
            this.m_labelChamp.Text = "Property|252";
            this.m_labelChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_boutonDropList
            // 
            this.m_boutonDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonDropList.Image")));
            this.m_boutonDropList.Location = new System.Drawing.Point(379, 0);
            this.m_boutonDropList.Name = "m_boutonDropList";
            this.m_boutonDropList.Size = new System.Drawing.Size(17, 17);
            this.m_boutonDropList.TabIndex = 5;
            this.m_boutonDropList.TabStop = false;
            this.m_boutonDropList.UseVisualStyleBackColor = false;
            this.m_boutonDropList.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_panel
            // 
            this.m_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panel.Controls.Add(this.m_labelChamp);
            this.m_panel.Controls.Add(this.m_boutonDropList);
            this.m_panel.Location = new System.Drawing.Point(0, 0);
            this.m_panel.LockEdition = false;
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(400, 21);
            this.m_panel.TabIndex = 6;
            // 
            // CControlSelectDefinitionPropriete
            // 
            this.Controls.Add(this.m_panel);
            this.Name = "CControlSelectDefinitionPropriete";
            this.Size = new System.Drawing.Size(400, 21);
            this.m_panel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		private void m_boutonDropList_Click(object sender, System.EventArgs e)
		{
			Rectangle rect = this.RectangleToScreen(new Rectangle ( 0, Height, Width, 230));
			bool bCancel = false;
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( rect, m_typeInterroge, m_fournisseur, ref bCancel, null, m_definitionRacineDeChamps );
			if ( !bCancel )
			{
				DefinitionSelectionnee = champ;
			}
		}

		public void Init ( IFournisseurProprietesDynamiques fournisseur, Type typeInterroge, CDefinitionProprieteDynamique definitionRacineDeChmaps )
		{
			m_definitionRacineDeChamps = definitionRacineDeChmaps;
			m_fournisseur = fournisseur;
			m_typeInterroge = typeInterroge;
		}

		public CDefinitionProprieteDynamique DefinitionSelectionnee
		{
			get
			{
				return m_definition;
			}
			set
			{
				m_definition = value;
				if ( m_definition == null )
					m_labelChamp.Text = "";
				else
					m_labelChamp.Text = m_definition.Nom;
			}
		}


	}
}
