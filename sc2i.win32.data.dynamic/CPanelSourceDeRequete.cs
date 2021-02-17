using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelSourceDeRequete.
	/// </summary>
	public class CPanelSourceDeRequete : System.Windows.Forms.UserControl
	{
		private int m_nIndex = 0;
		private CSourceDeChampDeRequete m_source = null;
		private CDefinitionProprieteDynamique m_definitionRacineDeChamps = null;
		private Type m_typeSource = null;
		private System.Windows.Forms.Label m_lblSource;
		private System.Windows.Forms.Label m_lblNumero;
		private System.Windows.Forms.Button m_btnSelect;
		private System.Windows.Forms.Button m_btnPlusRien;
		
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelSourceDeRequete()
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
			this.m_lblSource = new System.Windows.Forms.Label();
			this.m_lblNumero = new System.Windows.Forms.Label();
			this.m_btnSelect = new System.Windows.Forms.Button();
			this.m_btnPlusRien = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_lblSource
			// 
			this.m_lblSource.BackColor = System.Drawing.Color.White;
			this.m_lblSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lblSource.Location = new System.Drawing.Point(39, 0);
			this.m_lblSource.Name = "m_lblSource";
			this.m_lblSource.Size = new System.Drawing.Size(305, 16);
			this.m_lblSource.TabIndex = 0;
			this.m_lblSource.Click += new System.EventHandler(this.m_lblSource_Click);
			// 
			// m_lblNumero
			// 
			this.m_lblNumero.BackColor = System.Drawing.Color.White;
			this.m_lblNumero.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblNumero.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_lblNumero.Location = new System.Drawing.Point(0, 0);
			this.m_lblNumero.Name = "m_lblNumero";
			this.m_lblNumero.Size = new System.Drawing.Size(39, 16);
			this.m_lblNumero.TabIndex = 1;
			this.m_lblNumero.Text = "@1";
			// 
			// m_btnSelect
			// 
			this.m_btnSelect.Dock = System.Windows.Forms.DockStyle.Right;
			this.m_btnSelect.Location = new System.Drawing.Point(344, 0);
			this.m_btnSelect.Name = "m_btnSelect";
			this.m_btnSelect.Size = new System.Drawing.Size(16, 16);
			this.m_btnSelect.TabIndex = 2;
			this.m_btnSelect.Text = "...";
			this.m_btnSelect.Click += new System.EventHandler(this.m_btnSelect_Click);
			// 
			// m_btnPlusRien
			// 
			this.m_btnPlusRien.Dock = System.Windows.Forms.DockStyle.Right;
			this.m_btnPlusRien.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_btnPlusRien.Location = new System.Drawing.Point(360, 0);
			this.m_btnPlusRien.Name = "m_btnPlusRien";
			this.m_btnPlusRien.Size = new System.Drawing.Size(16, 16);
			this.m_btnPlusRien.TabIndex = 3;
			this.m_btnPlusRien.Text = "x";
			this.m_btnPlusRien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.m_btnPlusRien.Click += new System.EventHandler(this.m_btnPlusRien_Click);
			// 
			// CPanelSourceDeRequete
			// 
			this.Controls.Add(this.m_lblSource);
			this.Controls.Add(this.m_btnSelect);
			this.Controls.Add(this.m_btnPlusRien);
			this.Controls.Add(this.m_lblNumero);
			this.Name = "CPanelSourceDeRequete";
			this.Size = new System.Drawing.Size(376, 16);
			this.ResumeLayout(false);

		}
		#endregion

		public void Init ( 
			CSourceDeChampDeRequete source, 
			int nIndex, 
			Type typeSource,
			CDefinitionProprieteDynamique definitionRacineDeChamps )
		{
			m_nIndex = nIndex;
			m_lblNumero.Text = "@"+(nIndex+1);
			m_lblSource.Text = source.Source;
			m_source = source;
			m_definitionRacineDeChamps = definitionRacineDeChamps;
			m_typeSource = typeSource;
		}

		public int Index
		{
			get
			{
				return m_nIndex;
			}
		}

		public CSourceDeChampDeRequete Source
		{
			get
			{
				return m_source;
			}
		}

		private void m_btnPlusRien_Click(object sender, System.EventArgs e)
		{
			m_source = null;
			m_lblSource.Text = "";
		}

		private void m_btnSelect_Click(object sender, System.EventArgs e)
		{
			CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes
				( 
				m_typeSource, 
				CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent | 
				CFormSelectChampPourStructure.TypeSelectionAttendue.ChampFille | 
				CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees |
				CFormSelectChampPourStructure.TypeSelectionAttendue.InclureChampsCustom,
				m_definitionRacineDeChamps);
			if ( defs.Length > 0 )
			{
				CSourceDeChampDeRequete source = new CSourceDeChampDeRequete ( defs[0].NomChampCompatibleCComposantFiltreChamp );
				m_lblSource.Text = source.Source;
				m_source = source;
			}
		}

		private void m_lblSource_Click(object sender, EventArgs e)
		{

		}


	}
}
