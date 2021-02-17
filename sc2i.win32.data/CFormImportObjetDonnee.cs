using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CFormImportObjetDonnee.
	/// </summary>
	public class CFormImportObjetDonnee : System.Windows.Forms.Form, IInterfaceImportObjetDonnee
	{
		private CObjetDonneeAIdNumeriqueAuto m_objetDestination;
		private DataSet m_dataset;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid m_grid;
		private System.Windows.Forms.Button m_btnImporter;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.CheckBox m_chkResolutionAutomatique;

		private System.ComponentModel.Container components = null;

		public CFormImportObjetDonnee()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		public CFormImportObjetDonnee ( DataSet ds, CObjetDonneeAIdNumeriqueAuto objetDestination )
		{
			m_dataset = ds;
			m_objetDestination = objetDestination;
			InitializeComponent();
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.m_grid = new System.Windows.Forms.DataGrid();
            this.m_btnImporter = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_chkResolutionAutomatique = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Imported object :|101";
            // 
            // m_grid
            // 
            this.m_grid.DataMember = "";
            this.m_grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_grid.Location = new System.Drawing.Point(0, 24);
            this.m_grid.Name = "m_grid";
            this.m_grid.Size = new System.Drawing.Size(440, 280);
            this.m_grid.TabIndex = 1;
            // 
            // m_btnImporter
            // 
            this.m_btnImporter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnImporter.Location = new System.Drawing.Point(108, 335);
            this.m_btnImporter.Name = "m_btnImporter";
            this.m_btnImporter.Size = new System.Drawing.Size(96, 24);
            this.m_btnImporter.TabIndex = 2;
            this.m_btnImporter.Text = "Import|18";
            this.m_btnImporter.UseVisualStyleBackColor = false;
            this.m_btnImporter.Click += new System.EventHandler(this.m_btnImporter_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(237, 335);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(96, 24);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_chkResolutionAutomatique
            // 
            this.m_chkResolutionAutomatique.Checked = true;
            this.m_chkResolutionAutomatique.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkResolutionAutomatique.Location = new System.Drawing.Point(8, 304);
            this.m_chkResolutionAutomatique.Name = "m_chkResolutionAutomatique";
            this.m_chkResolutionAutomatique.Size = new System.Drawing.Size(424, 25);
            this.m_chkResolutionAutomatique.TabIndex = 4;
            this.m_chkResolutionAutomatique.Text = "Use automatic mapping solving|102";
            // 
            // CFormImportObjetDonnee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(440, 371);
            this.ControlBox = false;
            this.Controls.Add(this.m_chkResolutionAutomatique);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnImporter);
            this.Controls.Add(this.m_grid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormImportObjetDonnee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object importation|100";
            this.Load += new System.EventHandler(this.CFormImportObjetDonnee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void CFormImportObjetDonnee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            DataTable tableSource = m_dataset.Tables[0];
			DataRow rowSource = tableSource.Rows[0];
			DataTable tablePresentation = new DataTable("Presentation");
			tablePresentation.Columns.Add ( "NOM", typeof(string));
			tablePresentation.Columns.Add ( "VALEUR", typeof(string));
			CStructureTable structure = CStructureTable.GetStructure(m_objetDestination.GetType());
			foreach ( CInfoChampTable infoChamp in structure.Champs )
			{
				DataRow row = tablePresentation.NewRow();
				row["NOM"] = infoChamp.NomConvivial;
				object val = rowSource[infoChamp.NomChamp];
				row["VALEUR"] = val==null?"NULL":val.ToString();
				tablePresentation.Rows.Add ( row );
			}
			m_grid.DataSource = tablePresentation;
			
			DataGridTableStyle style = new DataGridTableStyle();
			style.MappingName = tablePresentation.TableName;
			DataGridColumnStyle col = new DataGridTextBoxColumn();
			col.MappingName = "NOM";
			col.Width = m_grid.Width/3;
			style.GridColumnStyles.Add ( col );
			col = new DataGridTextBoxColumn();
			col.MappingName = "VALEUR";
			col.Width = m_grid.Width*2/3;
			style.GridColumnStyles.Add ( col );
			m_grid.TableStyles.Add ( style );
		}

		/// ////////////////////////////////////////////
		public static bool Importe ( CObjetDonneeAIdNumeriqueAuto objetDestination, DataSet ds )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( result && ds.Tables.Count == 0 )
				result.EmpileErreur(I.T("No data for import|133"));
			if ( result && ds.Tables[0].TableName != objetDestination.GetNomTable() )
			{
				result.EmpileErreur(I.T("The source file not correspond to a @1 type object|134",
					DynamicClassAttribute.GetNomConvivial ( objetDestination.GetType() )));
			}
			DataTable tableSource = null;
			if ( result )
			{
				tableSource = ds.Tables[0];
				if ( tableSource.Rows.Count == 0 )
					result.EmpileErreur(I.T("No data for import|133"));
			}
			
			try
			{
				if ( result )
				{
					
					CFormImportObjetDonnee form = new CFormImportObjetDonnee ( ds, objetDestination );
					if ( form.ShowDialog() != DialogResult.OK )
						return false;
					return true;
				}

			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return false;
			}
			return true;


				
		}
			

		/// ////////////////////////////////////////////////////////
		private void m_btnImporter_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CResultAErreur.True;
			result = new CValiseImportExport().ImportXml ( 
				m_dataset, 
				m_objetDestination.ContexteDonnee.IdSession,
				m_objetDestination, this );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				DialogResult = DialogResult.Cancel;
			}
			else
				DialogResult = DialogResult.OK;
			Close();
		}


		/// ////////////////////////////////////////////////////////
		public CResultAErreur MapObjet(DataRow row, CListeObjetsDonnees listePossibles, ref CObjetDonneeAIdNumeriqueAuto objetSelect)
		{
			CResultAErreur result = CResultAErreur.True;
			bool bCancel = false;
			objetSelect = CFormMapRowToObjetDonnee.MappeRow ( row, listePossibles, ref bCancel, m_chkResolutionAutomatique.Checked );
			if ( bCancel )
			{
				if ( CFormAlerte.Afficher(I.T("Cancel the importation ?|137"),
					EFormAlerteType.Question ) == DialogResult.Yes )
				{
					result.EmpileErreur(I.T("User abort|135"));
				}
			}
			return result;
		}


	}
}
