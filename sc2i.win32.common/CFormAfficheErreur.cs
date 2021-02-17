using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormAfficheErreur.
	/// </summary>
	public class CFormAfficheErreur : System.Windows.Forms.Form
	{
		private const int c_nHeightInfo = 370;

		private System.Windows.Forms.Label m_lblErreur;
		private System.Windows.Forms.Button m_btnFermer;
		private System.Windows.Forms.Button m_btnPlus;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGrid m_gridErreurs;
		private CPileErreur	m_erreur;
		private	bool	m_bIsExpanded;

		private CFormAfficheErreur( CPileErreur erreur )
		{
			InitializeComponent();
			m_erreur = erreur;
			m_bIsExpanded = false;
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
            this.m_lblErreur = new System.Windows.Forms.Label();
            this.m_btnFermer = new System.Windows.Forms.Button();
            this.m_btnPlus = new System.Windows.Forms.Button();
            this.m_gridErreurs = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridErreurs)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblErreur
            // 
            this.m_lblErreur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblErreur.BackColor = System.Drawing.Color.Transparent;
            this.m_lblErreur.Location = new System.Drawing.Point(8, 8);
            this.m_lblErreur.Name = "m_lblErreur";
            this.m_lblErreur.Size = new System.Drawing.Size(453, 72);
            this.m_lblErreur.TabIndex = 0;
            this.m_lblErreur.Text = "(Errors)|10008";
            this.m_lblErreur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnFermer
            // 
            this.m_btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnFermer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnFermer.Location = new System.Drawing.Point(186, 80);
            this.m_btnFermer.Name = "m_btnFermer";
            this.m_btnFermer.Size = new System.Drawing.Size(104, 24);
            this.m_btnFermer.TabIndex = 1;
            this.m_btnFermer.Text = "Close|12";
            this.m_btnFermer.UseVisualStyleBackColor = false;
            this.m_btnFermer.Click += new System.EventHandler(this.m_btnFermer_Click);
            // 
            // m_btnPlus
            // 
            this.m_btnPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPlus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnPlus.Location = new System.Drawing.Point(357, 80);
            this.m_btnPlus.Name = "m_btnPlus";
            this.m_btnPlus.Size = new System.Drawing.Size(104, 24);
            this.m_btnPlus.TabIndex = 2;
            this.m_btnPlus.Text = "More information >>|104";
            this.m_btnPlus.UseVisualStyleBackColor = false;
            this.m_btnPlus.Click += new System.EventHandler(this.m_btnPlus_Click);
            // 
            // m_gridErreurs
            // 
            this.m_gridErreurs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridErreurs.BackgroundColor = System.Drawing.Color.White;
            this.m_gridErreurs.CaptionVisible = false;
            this.m_gridErreurs.DataMember = "";
            this.m_gridErreurs.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridErreurs.Location = new System.Drawing.Point(8, 120);
            this.m_gridErreurs.Name = "m_gridErreurs";
            this.m_gridErreurs.RowHeadersVisible = false;
            this.m_gridErreurs.Size = new System.Drawing.Size(453, 224);
            this.m_gridErreurs.TabIndex = 3;
            // 
            // CFormAfficheErreur
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(469, 343);
            this.ControlBox = false;
            this.Controls.Add(this.m_gridErreurs);
            this.Controls.Add(this.m_btnPlus);
            this.Controls.Add(this.m_btnFermer);
            this.Controls.Add(this.m_lblErreur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormAfficheErreur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Erreur d\'execution|103";
            this.SizeChanged += new System.EventHandler(this.CFormAfficheErreur_SizeChanged);
            this.Load += new System.EventHandler(this.CFormAfficheErreur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridErreurs)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/////////////////////////////////////////////////////////////////////////////
		private void CFormAfficheErreur_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            CWin32Traducteur.Translate(this);

            Height -= m_gridErreurs.Height;
			m_btnPlus.Visible = false;
			if ( m_erreur.Erreurs.Length == 0 )
			{
				m_lblErreur.Text = "Une erreur sans informations supplémentaires est survenue";
				
				return;
			}
			m_lblErreur.Text = m_erreur.Erreurs[0].Message;
			if ( m_erreur.Erreurs.Length > 1 )
			{
				m_btnPlus.Visible = true;
				DataTable table = new DataTable("Erreurs");
				table.Columns.Add ( "Erreur" );
				DataView view = new DataView ( table );
				
				foreach ( IErreur erreur in m_erreur.Erreurs )
				{
					DataRow row = table.NewRow();
					row["Erreur"] = erreur.Message;
					table.Rows.Add ( row );
				}
				m_gridErreurs.SetDataBinding ( table, "" );
				DataGridTableStyle style = new DataGridTableStyle();
				style.MappingName = table.TableName;
				style.RowHeadersVisible = true;
				style.ColumnHeadersVisible = false;

				DataGridTextBoxColumn colStyle = new DataGridTextBoxColumn();
				colStyle.MappingName = "Erreur";
				colStyle.Width = m_gridErreurs.Width - 20;
				colStyle.TextBox.Multiline = true;
				colStyle.TextBox.AcceptsReturn = true;
				
				
				style.GridColumnStyles.Add ( colStyle );
				m_gridErreurs.TableStyles.Add ( style );
				m_gridErreurs.ReadOnly = true;
				

			}
		
		}

		///////////////////////////////////////////////////////////////////////////
		private void m_btnPlus_Click(object sender, System.EventArgs e)
		{
			if ( m_bIsExpanded )
			{
				/*Height -= m_gridErreurs.Height;
				m_btnPlus.Text = "Plus d'infos >>";
				m_bIsExpanded = false;*/
			}
			else
			{
				Height = c_nHeightInfo;
				m_btnPlus.Visible = false;
				MinimizeBox = true;
				MaximizeBox = true;
				FormBorderStyle = FormBorderStyle.Sizable;
				m_bIsExpanded = true;
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public static void Show ( CPileErreur erreur, string strCaption )
		{
			CFormAfficheErreur form = new CFormAfficheErreur ( erreur );
			form.Text = strCaption;
			form.ShowDialog();
		}

		///////////////////////////////////////////////////////////////////////////
		public static void Show ( CPileErreur erreur )
		{
			Show ( erreur, "Erreur" );
		}

		///////////////////////////////////////////////////////////////////////////
		private void m_btnFermer_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		///////////////////////////////////////////////////////////////////////////
		private void CFormAfficheErreur_SizeChanged(object sender, System.EventArgs e)
		{
			DataGridTableStyle style = m_gridErreurs.TableStyles["Erreurs"];
			if ( style != null )
			{
				DataGridColumnStyle colStyle = style.GridColumnStyles["Erreur"];
				if ( colStyle != null )
					colStyle.Width = Math.Max ( colStyle.Width, m_gridErreurs.Width-15 );
			}
		}

	}
}
