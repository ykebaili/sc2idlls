using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data

{
	public class CFormOptionsCSV2 : Form
	{

		#region Code généré par le Concepteur Windows Form
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnTerminer;
		private System.Windows.Forms.CheckBox m_chkNullSiErreur;
		private System.Windows.Forms.ListView m_wndListeExemple;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.GroupBox m_grpColonne;
		private System.Windows.Forms.Button m_btnPrecedent;
		private System.Windows.Forms.Label m_lblNomCol;
		private System.Windows.Forms.Label m_lblType;
		private System.Windows.Forms.TextBox m_txtNomColonne;
		private System.Windows.Forms.ComboBox m_cmbType;
		private CheckBox m_chkMapperNull;
		private Label m_lblNull;
		private TextBox m_txtNullValue;
		private CheckBox m_chkNullCaseSensitive;
		private ToolTip tt;
		private Panel m_panNull;
		private IContainer components;


		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
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
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnTerminer = new System.Windows.Forms.Button();
            this.m_btnPrecedent = new System.Windows.Forms.Button();
            this.m_chkNullSiErreur = new System.Windows.Forms.CheckBox();
            this.m_wndListeExemple = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.m_grpColonne = new System.Windows.Forms.GroupBox();
            this.m_panNull = new System.Windows.Forms.Panel();
            this.m_lblNull = new System.Windows.Forms.Label();
            this.m_txtNullValue = new System.Windows.Forms.TextBox();
            this.m_chkNullCaseSensitive = new System.Windows.Forms.CheckBox();
            this.m_cmbType = new System.Windows.Forms.ComboBox();
            this.m_txtNomColonne = new System.Windows.Forms.TextBox();
            this.m_chkMapperNull = new System.Windows.Forms.CheckBox();
            this.m_lblType = new System.Windows.Forms.Label();
            this.m_lblNomCol = new System.Windows.Forms.Label();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.m_grpColonne.SuspendLayout();
            this.m_panNull.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(411, 389);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 8;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // m_btnTerminer
            // 
            this.m_btnTerminer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTerminer.Location = new System.Drawing.Point(571, 389);
            this.m_btnTerminer.Name = "m_btnTerminer";
            this.m_btnTerminer.Size = new System.Drawing.Size(75, 23);
            this.m_btnTerminer.TabIndex = 14;
            this.m_btnTerminer.Text = "End|23";
            this.m_btnTerminer.Click += new System.EventHandler(this.m_btnTerminer_Click);
            // 
            // m_btnPrecedent
            // 
            this.m_btnPrecedent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrecedent.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.m_btnPrecedent.Location = new System.Drawing.Point(491, 389);
            this.m_btnPrecedent.Name = "m_btnPrecedent";
            this.m_btnPrecedent.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrecedent.TabIndex = 10;
            this.m_btnPrecedent.Text = "< Previous|22";
            this.m_btnPrecedent.Click += new System.EventHandler(this.m_btnPrecedent_Click);
            // 
            // m_chkNullSiErreur
            // 
            this.m_chkNullSiErreur.Location = new System.Drawing.Point(8, 8);
            this.m_chkNullSiErreur.Name = "m_chkNullSiErreur";
            this.m_chkNullSiErreur.Size = new System.Drawing.Size(400, 16);
            this.m_chkNullSiErreur.TabIndex = 11;
            this.m_chkNullSiErreur.Text = "Set field to null in case of conversion error|117";
            // 
            // m_wndListeExemple
            // 
            this.m_wndListeExemple.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeExemple.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_wndListeExemple.Location = new System.Drawing.Point(8, 36);
            this.m_wndListeExemple.Name = "m_wndListeExemple";
            this.m_wndListeExemple.Size = new System.Drawing.Size(643, 232);
            this.m_wndListeExemple.TabIndex = 12;
            this.m_wndListeExemple.UseCompatibleStateImageBehavior = false;
            this.m_wndListeExemple.View = System.Windows.Forms.View.Details;
            this.m_wndListeExemple.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_wndListeExemple_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 103;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 111;
            // 
            // m_grpColonne
            // 
            this.m_grpColonne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpColonne.Controls.Add(this.m_panNull);
            this.m_grpColonne.Controls.Add(this.m_cmbType);
            this.m_grpColonne.Controls.Add(this.m_txtNomColonne);
            this.m_grpColonne.Controls.Add(this.m_chkMapperNull);
            this.m_grpColonne.Controls.Add(this.m_lblType);
            this.m_grpColonne.Controls.Add(this.m_lblNomCol);
            this.m_grpColonne.Location = new System.Drawing.Point(8, 274);
            this.m_grpColonne.Name = "m_grpColonne";
            this.m_grpColonne.Size = new System.Drawing.Size(643, 109);
            this.m_grpColonne.TabIndex = 13;
            this.m_grpColonne.TabStop = false;
            // 
            // m_panNull
            // 
            this.m_panNull.Controls.Add(this.m_lblNull);
            this.m_panNull.Controls.Add(this.m_txtNullValue);
            this.m_panNull.Controls.Add(this.m_chkNullCaseSensitive);
            this.m_panNull.Location = new System.Drawing.Point(330, 40);
            this.m_panNull.Name = "m_panNull";
            this.m_panNull.Size = new System.Drawing.Size(266, 60);
            this.m_panNull.TabIndex = 12;
            this.m_panNull.Visible = false;
            // 
            // m_lblNull
            // 
            this.m_lblNull.Location = new System.Drawing.Point(7, 6);
            this.m_lblNull.Name = "m_lblNull";
            this.m_lblNull.Size = new System.Drawing.Size(86, 21);
            this.m_lblNull.TabIndex = 1;
            this.m_lblNull.Text = "Null Value|124";
            this.m_lblNull.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtNullValue
            // 
            this.m_txtNullValue.Location = new System.Drawing.Point(99, 8);
            this.m_txtNullValue.Name = "m_txtNullValue";
            this.m_txtNullValue.Size = new System.Drawing.Size(164, 20);
            this.m_txtNullValue.TabIndex = 2;
            this.tt.SetToolTip(this.m_txtNullValue, "Valeur interprété comme null (sans identificateur de texte)");
            // 
            // m_chkNullCaseSensitive
            // 
            this.m_chkNullCaseSensitive.Location = new System.Drawing.Point(99, 34);
            this.m_chkNullCaseSensitive.Name = "m_chkNullCaseSensitive";
            this.m_chkNullCaseSensitive.Size = new System.Drawing.Size(146, 18);
            this.m_chkNullCaseSensitive.TabIndex = 11;
            this.m_chkNullCaseSensitive.Text = "Case sensitive|123";
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.Location = new System.Drawing.Point(144, 40);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(160, 21);
            this.m_cmbType.TabIndex = 3;
            // 
            // m_txtNomColonne
            // 
            this.m_txtNomColonne.Location = new System.Drawing.Point(144, 16);
            this.m_txtNomColonne.Name = "m_txtNomColonne";
            this.m_txtNomColonne.Size = new System.Drawing.Size(160, 20);
            this.m_txtNomColonne.TabIndex = 2;
            // 
            // m_chkMapperNull
            // 
            this.m_chkMapperNull.Location = new System.Drawing.Point(330, 16);
            this.m_chkMapperNull.Name = "m_chkMapperNull";
            this.m_chkMapperNull.Size = new System.Drawing.Size(263, 18);
            this.m_chkMapperNull.TabIndex = 11;
            this.m_chkMapperNull.Text = "Null Mapping|122";
            this.m_chkMapperNull.CheckedChanged += new System.EventHandler(this.m_chkMapperNull_CheckedChanged);
            // 
            // m_lblType
            // 
            this.m_lblType.Location = new System.Drawing.Point(8, 40);
            this.m_lblType.Name = "m_lblType";
            this.m_lblType.Size = new System.Drawing.Size(130, 21);
            this.m_lblType.TabIndex = 1;
            this.m_lblType.Text = "Type|119";
            this.m_lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNomCol
            // 
            this.m_lblNomCol.Location = new System.Drawing.Point(8, 16);
            this.m_lblNomCol.Name = "m_lblNomCol";
            this.m_lblNomCol.Size = new System.Drawing.Size(130, 20);
            this.m_lblNomCol.TabIndex = 0;
            this.m_lblNomCol.Text = "Column name|118";
            this.m_lblNomCol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CFormOptionsCSV2
            // 
            this.AcceptButton = this.m_btnTerminer;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(659, 419);
            this.Controls.Add(this.m_wndListeExemple);
            this.Controls.Add(this.m_grpColonne);
            this.Controls.Add(this.m_chkNullSiErreur);
            this.Controls.Add(this.m_btnPrecedent);
            this.Controls.Add(this.m_btnTerminer);
            this.Controls.Add(this.m_btnAnnuler);
            this.Name = "CFormOptionsCSV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Options 2/2|116";
            this.Load += new System.EventHandler(this.CFormOptionsCSV2_Load);
            this.m_grpColonne.ResumeLayout(false);
            this.m_grpColonne.PerformLayout();
            this.m_panNull.ResumeLayout(false);
            this.m_panNull.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public CFormOptionsCSV2()
		{
			InitializeComponent();
		}


		private bool m_bInitialise = false;
		private int m_nColSel = 0;
		private DataTable m_tableExemple = new DataTable();
		private Hashtable m_tableNomsColonnesDefaut = new Hashtable();
		private string m_strTexteExemple = "";
		private CParametreLectureCSV m_parametre = null;
		private Dictionary<int, string> m_nomsCol = new Dictionary<int,string>();

		private string GetNomColonneDefaut(int nCol)
		{
			if (m_bInitialise)
				return m_nomsCol[nCol];
			else
			{
				return I.T("Column @1|132", (nCol + 1).ToString());
			}
		}

		public static DialogResult FillOptions(CParametreLectureCSV parametre, string strFichierExemple)
		{
			CFormOptionsCSV2 form = new CFormOptionsCSV2();
			form.m_parametre = parametre;
			string strTexteExemple = "";
			if (strFichierExemple != "")
			{
				StreamReader reader = null;
				try
				{
					reader = new StreamReader(strFichierExemple, new CEncoding(parametre.Encodage).GetEncoding());
					int nLigne = 0;
					//string strLigne = reader.ReadLine();
                    string strLigne = parametre.GetCSVLine(reader);
					form.m_tableExemple = new DataTable();
					form.m_nomsCol = new Dictionary<int, string>();
					//CREATION DES COLONNES
					if (parametre.NomChampsSurPremiereLigne)
					{
						string[] strChamps = strLigne.Split(parametre.Separateur);
						int nChamp = 0;
						foreach (string strChamp in strChamps)
						{
							string strVal = strChamp;
							if (strVal.Trim() == "")
								strVal = form.GetNomColonneDefaut(nChamp);
							nChamp++;
							int nCpt = 0;
							string strChampTmp = strVal;
							while (form.m_tableExemple.Columns.Contains(strChampTmp))
							{
								strChampTmp = strChamp + nCpt.ToString();
								nCpt++;
							}
							form.m_tableExemple.Columns.Add(strChampTmp, typeof(string));
							form.m_nomsCol.Add(nChamp, strChampTmp);
						}
						//strLigne = reader.ReadLine();
                        strLigne = parametre.GetCSVLine(reader);
					}
					else
					{
						string[] strCols = parametre.GetDatas(strLigne);
						for (int nCol = 0; nCol < strCols.Length; nCol++)
						{
							string strChamp = "";
							if (parametre.Mappage != null
								&& parametre.Mappage.StringsA != null
								&& parametre.Mappage.StringsA.Count >= strCols.Length)
								strChamp = parametre.Mappage.StringsA[nCol];
							else
								form.GetNomColonneDefaut(nCol);

							int nCpt = 0;
							string strChampTmp = strChamp;
							while(form.m_tableExemple.Columns.Contains(strChampTmp))
							{
								strChampTmp = strChamp + nCpt.ToString();
								nCpt++;
							}
							form.m_tableExemple.Columns.Add(strChampTmp, typeof(string));
							form.m_nomsCol.Add(nCol, strChampTmp);
						}
					}

					//LECTURE DU FICHIER
					while (strLigne != null && nLigne++ < 100)
					{
						string[] strDatas = parametre.GetDatas(strLigne);
						int nCol = 0;
						DataRow row = form.m_tableExemple.NewRow();
						foreach (string strData in strDatas)
						{
							if (nCol < form.m_tableExemple.Columns.Count)
								row[nCol] = strData;
							nCol++;
						}
						form.m_tableExemple.Rows.Add(row);
						//strLigne = reader.ReadLine();
                        strLigne = parametre.GetCSVLine(reader);
					}


					//CREATION DU LISTVIEW
					form.m_wndListeExemple.Columns.Clear();
					foreach (DataColumn col in form.m_tableExemple.Columns)
					{
						ColumnHeader header = new ColumnHeader();
						header.Text = col.ColumnName;
						form.m_wndListeExemple.Columns.Add(header);
					}
					foreach (DataRow row in form.m_tableExemple.Rows)
					{
						ListViewItem item = new ListViewItem(row[0].ToString());
						for (int n = 1; n < form.m_tableExemple.Columns.Count; n++)
							item.SubItems.Add(row[n].ToString());
						form.m_wndListeExemple.Items.Add(item);
					}


					//CREATION DES COLONNES CSV DANS FICHIER PARAMETRAGE
					for (int nCol = 0; nCol < form.m_tableExemple.Columns.Count; nCol++)
					{
						CParametreLectureCSV.CColonneCSV col = form.m_parametre.GetColonne(nCol);
						if (col == null ||col.Nom != form.m_tableExemple.Columns[nCol].ColumnName)
						{
							col = new CParametreLectureCSV.CColonneCSV();
							col.Nom = form.m_tableExemple.Columns[nCol].ColumnName;
							col.DataType = typeof(string);
							form.m_parametre.SetColonne(nCol, col);
						}
					}


					reader.Close();

					form.m_strTexteExemple = strTexteExemple;
					DialogResult result = form.ShowDialog();
					form.Dispose();
					return result;

				}
				catch
				{
				}
				finally
				{
					try
					{
						reader.Close();
					}

					catch 
					{
					}
				}
			}
			return DialogResult.Abort;
		}


		private void ValideColonneEnCours()
		{
			CParametreLectureCSV.CColonneCSV col = m_parametre.GetColonne(m_nColSel);
			if (m_cmbType.SelectedValue != null )
				col.DataType = (Type)m_cmbType.SelectedValue;

			//Nom Colonne
			string strNom = m_txtNomColonne.Text.Trim() != "" ? m_txtNomColonne.Text.Trim() : GetNomColonneDefaut(m_nColSel);
			if (!m_parametre.ValideNomPourColonne(m_nColSel, strNom))
				CFormAlerte.Afficher(I.T("This name already exists|125"), EFormAlerteType.Exclamation);
			else
				col.Nom = strNom;

			//Null Colonne
			col.HasNullMapping = m_chkMapperNull.Checked;
			if (m_chkMapperNull.Checked)
			{
				col.NullMapping = m_txtNullValue.Text;
				col.NullCaseSensitive = m_chkNullCaseSensitive.Checked;
			}

			//Set Colonne
			m_parametre.SetColonne(m_nColSel, col);
			m_wndListeExemple.Columns[m_nColSel].Text = col.Nom;
		}
		private void SelectColonne(int nCol)
		{
			if(m_bInitialise)
				ValideColonneEnCours();
			m_nColSel = nCol;
			CParametreLectureCSV.CColonneCSV col = m_parametre.GetColonne(nCol);
			m_grpColonne.Text = I.T("Column n°@1|126", (nCol + 1).ToString());
		
			m_txtNomColonne.Text = col.Nom;
			if (col.DataType == null)
				m_cmbType.SelectedValue = typeof(string);
			else
				m_cmbType.SelectedValue = col.DataType;

			m_chkMapperNull.Checked = col.HasNullMapping;
			if (col.HasNullMapping)
			{
				m_txtNullValue.Text = col.NullMapping;
				m_chkNullCaseSensitive.Checked = col.NullCaseSensitive;
			}
			else
			{
				m_txtNullValue.Text = "";
				m_chkNullCaseSensitive.Checked = false;
			}

			m_panNull.Visible = m_chkMapperNull.Checked;
		}

		private void InitCmbTypes()
		{
			DataTable table = new DataTable();
			table.Columns.Add("LABEL", typeof(string));
			table.Columns.Add("VALEUR", typeof(Type));
			DataRow row = table.NewRow();
			row.ItemArray = new object[] { I.T("Text|127"), typeof(string) };
			table.Rows.Add(row);

			row = table.NewRow();
			row.ItemArray = new object[] { I.T("Integer|128"), typeof(int) };
			table.Rows.Add(row);

			row = table.NewRow();
			row.ItemArray = new object[] { I.T("Decimal|129"), typeof(double) };
			table.Rows.Add(row);

			row = table.NewRow();
			row.ItemArray = new object[] { I.T("Date|130"), typeof(DateTime) };
			table.Rows.Add(row);

			row = table.NewRow();
			row.ItemArray = new object[] { I.T("True/False|131"), typeof(bool) };
			table.Rows.Add(row);

			m_cmbType.DisplayMember = "LABEL";
			m_cmbType.ValueMember = "VALEUR";
			m_cmbType.DataSource = table;
			m_cmbType.SelectedIndex = 0;
		}
		//----------------------------------------------------------------------
		private void CFormOptionsCSV2_Load(object sender, System.EventArgs e)
		{
			if (m_parametre == null)
				return;

			m_bInitialise = false;

			sc2i.win32.common.CWin32Traducteur.Translate(this);

			InitCmbTypes();

			m_chkNullSiErreur.Checked = m_parametre.ValeurNullSiErreur;

			SelectColonne(0);

			m_bInitialise = true;
		}
		//----------------------------------------------------------------------
		private void m_wndListeExemple_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			SelectColonne(e.Column);
		}
		//----------------------------------------------------------------------
		private void m_chkMapperNull_CheckedChanged(object sender, EventArgs e)
		{
			m_panNull.Visible = m_chkMapperNull.Checked;
		}
		//----------------------------------------------------------------------
		private void m_btnPrecedent_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Retry;
			Close();
		}
		//----------------------------------------------------------------------
		private void m_btnTerminer_Click(object sender, System.EventArgs e)
		{
			ValideColonneEnCours();
			m_parametre.ValeurNullSiErreur = m_chkNullSiErreur.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
