using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.common;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormSelectVariableDynamiqueAInterfaceUtilisateur.
	/// </summary>
	public class CFormSelectVariableDynamiqueAInterfaceUtilisateur : System.Windows.Forms.Form
	{
		private CVariableDynamique m_variableSelectionnee;
		private IElementAVariablesDynamiquesBase m_elementAVariables;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.ListView m_wndListeVariables;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private sc2i.win32.common.CWndLinkStd m_btnAjouterVariable;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private System.Windows.Forms.MenuItem m_menuVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormSelectVariableDynamiqueAInterfaceUtilisateur()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		public CFormSelectVariableDynamiqueAInterfaceUtilisateur( IElementAVariablesDynamiquesBase element)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_elementAVariables = element;

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

        public static void EditeVariables(IElementAVariablesDynamiques element)
        {
            CFormSelectVariableDynamiqueAInterfaceUtilisateur form = new CFormSelectVariableDynamiqueAInterfaceUtilisateur(element);
            form.m_btnOk.Visible = false;
            form.m_btnAnnuler.Text = form.m_btnOk.Text;
            form.ShowDialog();
            form.Dispose();
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
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_wndListeVariables = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_btnAjouterVariable = new sc2i.win32.common.CWndLinkStd();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(40, 216);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(72, 24);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(120, 216);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(72, 24);
            this.m_btnAnnuler.TabIndex = 2;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeVariables.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(224, 194);
            this.m_wndListeVariables.TabIndex = 3;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.DoubleClick += new System.EventHandler(this.m_wndListeVariables_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Variable|251";
            this.columnHeader1.Width = 207;
            // 
            // m_btnAjouterVariable
            // 
            this.m_btnAjouterVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouterVariable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouterVariable.Location = new System.Drawing.Point(0, 194);
            this.m_btnAjouterVariable.Name = "m_btnAjouterVariable";
            this.m_btnAjouterVariable.ShortMode = false;
            this.m_btnAjouterVariable.Size = new System.Drawing.Size(72, 22);
            this.m_btnAjouterVariable.TabIndex = 4;
            this.m_btnAjouterVariable.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouterVariable.LinkClicked += new System.EventHandler(this.m_btnAjouterVariable_LinkClicked);
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuVariableCalculée,
            this.m_menuVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Saisie";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuVariableCalculée
            // 
            this.m_menuVariableCalculée.Index = 1;
            this.m_menuVariableCalculée.Text = "Calculée";
            this.m_menuVariableCalculée.Click += new System.EventHandler(this.m_menuVariableCalculée_Click);
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Sélection";
            this.m_menuVariableSelection.Click += new System.EventHandler(this.m_menuVariableSelection_Click);
            // 
            // CFormSelectVariableDynamiqueAInterfaceUtilisateur
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(224, 246);
            this.ControlBox = false;
            this.Controls.Add(this.m_btnAjouterVariable);
            this.Controls.Add(this.m_wndListeVariables);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormSelectVariableDynamiqueAInterfaceUtilisateur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Variable selection|172";
            this.Load += new System.EventHandler(this.CFormSelectVariableDynamiqueAInterfaceUtilisateur_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// /////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			SelectAndClose();
		}

		/// /////////////////////////////////////////////////////////////
		private void CFormSelectVariableDynamiqueAInterfaceUtilisateur_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            FillListe();
		}

		/// /////////////////////////////////////////////////////////////
		private void FillListe()
		{
			m_wndListeVariables.Items.Clear();
			if ( m_elementAVariables != null )
			{
				foreach ( CVariableDynamique variable in m_elementAVariables.ListeVariables )
				{
					if ( variable.IsChoixUtilisateur() )
					{
						ListViewItem item = new ListViewItem ( variable.Nom );
						item.Tag = variable;
						m_wndListeVariables.Items.Add ( item );
						if ( m_variableSelectionnee != null && m_variableSelectionnee.IdVariable == variable.IdVariable )
							item.Selected = true;
					}
				}
			}
		}

		/// /////////////////////////////////////////////////////////////
		public CVariableDynamique SelectVariable ( CVariableDynamique variableToSel )
		{
			m_variableSelectionnee = variableToSel;
			ShowDialog();
			return m_variableSelectionnee;
		}

		/// /////////////////////////////////////////////////////////////
		public void SelectAndClose()
		{
			if ( m_wndListeVariables.SelectedItems.Count == 0 )
				return;
			m_variableSelectionnee = (CVariableDynamique)m_wndListeVariables.SelectedItems[0].Tag;
			DialogResult = DialogResult.OK;
			Close();
		}

		/// /////////////////////////////////////////////////////////////
		private void m_wndListeVariables_DoubleClick(object sender, System.EventArgs e)
		{
			SelectAndClose();
		}

		/// /////////////////////////////////////////////////////////////
		private void m_btnAjouterVariable_LinkClicked(object sender, System.EventArgs e)
		{
			m_menuNewVariable.Show ( m_btnAjouterVariable, new Point ( 0, m_btnAjouterVariable.Height ) );
		}


		/// /////////////////////////////////////////////////////////////
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie ( m_elementAVariables );
			if ( CFormEditVariableDynamiqueSaisie.EditeVariable ( variable, m_elementAVariables ) )
			{
				m_elementAVariables.AddVariable ( variable );
				FillListe();
			}
		}

		/// /////////////////////////////////////////////////////////////
		private void m_menuVariableCalculée_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee ( m_elementAVariables );
			if ( CFormEditVariableFiltreCalculee.EditeVariable ( variable, m_elementAVariables ) )
			{
				m_elementAVariables.AddVariable ( variable );
				FillListe();
			}
		}

		/// /////////////////////////////////////////////////////////////
		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee ( m_elementAVariables );
			if ( CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable ( variable ) )
			{
				m_elementAVariables.AddVariable ( variable );
				FillListe();
			}
		}
	}

	[AutoExec("Autoexec")]
	public class CSelectionneurVariableAInterfaceUtilisateur : ISelectionneurVariableFiltreDynamique
	{
		private IElementAVariablesDynamiquesBase m_elementEdite = null;

		//-----------------------------------------------------------
		public CSelectionneurVariableAInterfaceUtilisateur ( )
		{
		}

		//-----------------------------------------------------------
		public static void Autoexec()
		{
			CProprieteVariableFiltreDynamiqueEditor.SetTypeEditeur(typeof(CSelectionneurVariableAInterfaceUtilisateur));
		}

		//-----------------------------------------------------------
		public CSelectionneurVariableAInterfaceUtilisateur(IElementAVariablesDynamiquesBase elementEdite)
		{
			m_elementEdite = elementEdite;
		}

		//-----------------------------------------------------------
		public IElementAVariablesDynamiquesBase ElementEdite
		{
			get
			{
				return m_elementEdite;
			}
			set
			{
				m_elementEdite = value;
			}
		}

		//-----------------------------------------------------------
		public CVariableDynamique SelectVariable(CVariableDynamique variableToSel)
		{
			CFormSelectVariableDynamiqueAInterfaceUtilisateur form = new CFormSelectVariableDynamiqueAInterfaceUtilisateur(ElementEdite);
			CVariableDynamique variable = form.SelectVariable(variableToSel);
			form.Dispose();
			return variable;
		}
	}
}
