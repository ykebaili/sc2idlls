using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormSelectionFromVariableSelectionObjetDonnee.
	/// </summary>
	public class CFormSelectionFromVariableSelectionObjetDonnee : System.Windows.Forms.Form
	{
		private const string c_champValeurAffichee = "AFF";
		private const string c_champValeurRetournee = "RET";
		private CVariableDynamiqueSelectionObjetDonnee m_variable;
		private object[] m_selection;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblValeurSelectionnee;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.ListView m_wndListeObjets;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormSelectionFromVariableSelectionObjetDonnee()
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblValeurSelectionnee = new System.Windows.Forms.Label();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_wndListeObjets = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(8, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selected value|167";
            // 
            // m_lblValeurSelectionnee
            // 
            this.m_lblValeurSelectionnee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblValeurSelectionnee.BackColor = System.Drawing.Color.White;
            this.m_lblValeurSelectionnee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblValeurSelectionnee.Location = new System.Drawing.Point(148, 344);
            this.m_lblValeurSelectionnee.Name = "m_lblValeurSelectionnee";
            this.m_lblValeurSelectionnee.Size = new System.Drawing.Size(228, 16);
            this.m_lblValeurSelectionnee.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(216, 368);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_btnAnnuler.TabIndex = 24;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(88, 368);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_btnOk.TabIndex = 23;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_wndListeObjets
            // 
            this.m_wndListeObjets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeObjets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeObjets.Location = new System.Drawing.Point(8, 8);
            this.m_wndListeObjets.MultiSelect = false;
            this.m_wndListeObjets.Name = "m_wndListeObjets";
            this.m_wndListeObjets.Size = new System.Drawing.Size(368, 328);
            this.m_wndListeObjets.TabIndex = 25;
            this.m_wndListeObjets.UseCompatibleStateImageBehavior = false;
            this.m_wndListeObjets.View = System.Windows.Forms.View.Details;
            this.m_wndListeObjets.SelectedIndexChanged += new System.EventHandler(this.m_wndListeObjets_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 764;
            // 
            // CFormSelectionFromVariableSelectionObjetDonnee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(384, 398);
            this.Controls.Add(this.m_wndListeObjets);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_lblValeurSelectionnee);
            this.Controls.Add(this.label1);
            this.Name = "CFormSelectionFromVariableSelectionObjetDonnee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selection|116";
            this.Load += new System.EventHandler(this.CFormSelectionFromVariableSelectionObjetDonnee_Load);
            this.ResumeLayout(false);

		}
		#endregion
		///////////////////////////////////////////////////////////////////////////////
		private void CFormSelectionFromVariableSelectionObjetDonnee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            InitListe();
		}

		///////////////////////////////////////////////////////////////////////////////
		private void InitListe()
		{
			m_wndListeObjets.Items.Clear();
			foreach ( IValeurVariable valeur in m_variable.Valeurs )
			{
				ListViewItem item  = new ListViewItem ( valeur.Display );
				item.Tag = valeur.Value;
				m_wndListeObjets.Items.Add ( item );
			}
			m_wndListeObjets.Sort();
		} 
		

		///////////////////////////////////////////////////////////////////////////////
		private void Init ( CVariableDynamiqueSelectionObjetDonnee variable )
		{
			m_variable = variable;
		}

		///////////////////////////////////////////////////////////////////////////////
		public static bool Selectionne ( CVariableDynamiqueSelectionObjetDonnee variable, ref object[] selection )
		{
			CFormSelectionFromVariableSelectionObjetDonnee form = new CFormSelectionFromVariableSelectionObjetDonnee();
			form.Init ( variable );
			bool bResult = form.ShowDialog() == DialogResult.OK;
			selection = form.m_selection;
			return bResult;
			
		}

		///////////////////////////////////////////////////////////////////////////////
		private void m_wndListeObjets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( m_wndListeObjets.SelectedItems.Count == 0 )
			{
				m_lblValeurSelectionnee.Text = "";
			}
			else
			{
				object val = m_wndListeObjets.SelectedItems[0].Tag;
				if ( val != null )
					m_lblValeurSelectionnee.Text = val.ToString();
				else
					m_lblValeurSelectionnee.Text = null;
			}
		}

		///////////////////////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeObjets.CheckedIndices.Count == 0 )
				return;
			ArrayList lst = new ArrayList();
			foreach ( ListViewItem item in m_wndListeObjets.CheckedItems )
			{
				lst.Add ( item.Tag );
			}
			m_selection = (object[])lst.ToArray(typeof(object[]));
				this.DialogResult = DialogResult.OK;
			Close();
		}

	}
}
