using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.process;
using sc2i.data;
using sc2i.win32.common;
using sc2i.win32.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
    public class CFormEditActionCopierEntite : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView m_wndListePropietes;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private System.Windows.Forms.Label label2;
        private SplitContainer m_splitContainer;
        private CControleEditeFormule m_txtFormuleObjetSource;
        private Label label1;
        private ColumnHeader m_colNomPropriete;
        private CControleEditeFormule m_txtFormuleObjetDestination;
        private Panel m_panelDefinisseurChamps;
        private Label label3;
        private sc2i.win32.data.navigation.C2iTextBoxSelectionne m_txtSelectDefinisseurChampsCustom;
        private CheckBox m_chkFullCopy;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionCopierEntite()
		{
			// Cet appel est requis par le Concepteur Windows Form.
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionCopierEntite), typeof(CFormEditActionCopierEntite));
		}


		public CActionCopierEntite ActionCopierEntite
		{
			get
			{
				return (CActionCopierEntite)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_panelDefinisseurChamps = new System.Windows.Forms.Panel();
            this.m_txtSelectDefinisseurChampsCustom = new sc2i.win32.data.navigation.C2iTextBoxSelectionne();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtFormuleObjetDestination = new sc2i.win32.expression.CControleEditeFormule();
            this.m_txtFormuleObjetSource = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_wndListePropietes = new System.Windows.Forms.ListView();
            this.m_colNomPropriete = new System.Windows.Forms.ColumnHeader();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_chkFullCopy = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.m_panelDefinisseurChamps.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_splitContainer);
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 435);
            this.panel2.TabIndex = 2;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_chkFullCopy);
            this.m_splitContainer.Panel1.Controls.Add(this.m_panelDefinisseurChamps);
            this.m_splitContainer.Panel1.Controls.Add(this.m_txtFormuleObjetDestination);
            this.m_splitContainer.Panel1.Controls.Add(this.m_txtFormuleObjetSource);
            this.m_splitContainer.Panel1.Controls.Add(this.label1);
            this.m_splitContainer.Panel1.Controls.Add(this.label2);
            this.m_splitContainer.Panel1.Controls.Add(this.m_wndListePropietes);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(735, 435);
            this.m_splitContainer.SplitterDistance = 535;
            this.m_splitContainer.TabIndex = 6;
            // 
            // m_panelDefinisseurChamps
            // 
            this.m_panelDefinisseurChamps.Controls.Add(this.m_txtSelectDefinisseurChampsCustom);
            this.m_panelDefinisseurChamps.Controls.Add(this.label3);
            this.m_panelDefinisseurChamps.Location = new System.Drawing.Point(8, 194);
            this.m_panelDefinisseurChamps.Name = "m_panelDefinisseurChamps";
            this.m_panelDefinisseurChamps.Size = new System.Drawing.Size(513, 26);
            this.m_panelDefinisseurChamps.TabIndex = 8;
            // 
            // m_txtSelectDefinisseurChampsCustom
            // 
            this.m_txtSelectDefinisseurChampsCustom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSelectDefinisseurChampsCustom.ElementSelectionne = null;
            this.m_txtSelectDefinisseurChampsCustom.FonctionTextNull = null;
            this.m_txtSelectDefinisseurChampsCustom.HasLink = true;
            this.m_txtSelectDefinisseurChampsCustom.Location = new System.Drawing.Point(130, 2);
            this.m_txtSelectDefinisseurChampsCustom.LockEdition = false;
            this.m_txtSelectDefinisseurChampsCustom.Name = "m_txtSelectDefinisseurChampsCustom";
            this.m_txtSelectDefinisseurChampsCustom.SelectedObject = null;
            this.m_txtSelectDefinisseurChampsCustom.Size = new System.Drawing.Size(380, 21);
            this.m_txtSelectDefinisseurChampsCustom.TabIndex = 9;
            this.m_txtSelectDefinisseurChampsCustom.TextNull = "";
            this.m_txtSelectDefinisseurChampsCustom.ElementSelectionneChanged += new System.EventHandler(this.m_txtSelectDefinisseurChampsCustom_ElementSelectionneChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Custom fields filter|214";
            // 
            // m_txtFormuleObjetDestination
            // 
            this.m_txtFormuleObjetDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleObjetDestination.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleObjetDestination.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleObjetDestination.Formule = null;
            this.m_txtFormuleObjetDestination.Location = new System.Drawing.Point(6, 23);
            this.m_txtFormuleObjetDestination.LockEdition = false;
            this.m_txtFormuleObjetDestination.Name = "m_txtFormuleObjetDestination";
            this.m_txtFormuleObjetDestination.Size = new System.Drawing.Size(515, 60);
            this.m_txtFormuleObjetDestination.TabIndex = 6;
            this.m_txtFormuleObjetDestination.Leave += new System.EventHandler(this.m_txtFormuleObjetDestination_Leave);
            this.m_txtFormuleObjetDestination.Enter += new System.EventHandler(this.OnEnterTexteFormule);
            // 
            // m_txtFormuleObjetSource
            // 
            this.m_txtFormuleObjetSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleObjetSource.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleObjetSource.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleObjetSource.Formule = null;
            this.m_txtFormuleObjetSource.Location = new System.Drawing.Point(6, 111);
            this.m_txtFormuleObjetSource.LockEdition = false;
            this.m_txtFormuleObjetSource.Name = "m_txtFormuleObjetSource";
            this.m_txtFormuleObjetSource.Size = new System.Drawing.Size(515, 60);
            this.m_txtFormuleObjetSource.TabIndex = 6;
            this.m_txtFormuleObjetSource.Enter += new System.EventHandler(this.OnEnterTexteFormule);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Entity|208";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination Entity|213";
            // 
            // m_wndListePropietes
            // 
            this.m_wndListePropietes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListePropietes.CheckBoxes = true;
            this.m_wndListePropietes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colNomPropriete});
            this.m_wndListePropietes.Location = new System.Drawing.Point(8, 221);
            this.m_wndListePropietes.Name = "m_wndListePropietes";
            this.m_wndListePropietes.Size = new System.Drawing.Size(513, 204);
            this.m_wndListePropietes.TabIndex = 0;
            this.m_wndListePropietes.UseCompatibleStateImageBehavior = false;
            this.m_wndListePropietes.View = System.Windows.Forms.View.Details;
            // 
            // m_colNomPropriete
            // 
            this.m_colNomPropriete.Text = "Properties to copy|210";
            this.m_colNomPropriete.Width = 247;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(192, 431);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_chkFullCopy
            // 
            this.m_chkFullCopy.AutoSize = true;
            this.m_chkFullCopy.Location = new System.Drawing.Point(14, 171);
            this.m_chkFullCopy.Name = "m_chkFullCopy";
            this.m_chkFullCopy.Size = new System.Drawing.Size(100, 17);
            this.m_chkFullCopy.TabIndex = 9;
            this.m_chkFullCopy.Text = "Full copy|20010";
            this.m_chkFullCopy.UseVisualStyleBackColor = true;
            this.m_chkFullCopy.CheckedChanged += new System.EventHandler(this.m_chkFullCopy_CheckedChanged);
            // 
            // CFormEditActionCopierEntite
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(735, 483);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionCopierEntite";
            this.Text = "Copy an entity|209";
            this.Load += new System.EventHandler(this.CFormEditActionCopierEntite_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel1.PerformLayout();
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.m_panelDefinisseurChamps.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();

            m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
            // Init la formule de l'objet source
            m_txtFormuleObjetSource.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            if (ActionCopierEntite.ExpressionEntiteSource != null)
                m_txtFormuleObjetSource.Text = ActionCopierEntite.ExpressionEntiteSource.GetString();

            // Init la formule de l'objet destination
            m_txtFormuleObjetDestination.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            if (ActionCopierEntite.ExpressionEntiteDestination != null)
                m_txtFormuleObjetDestination.Text = ActionCopierEntite.ExpressionEntiteDestination.GetString();

            InitPanelDefinisseurChamps();
			FillListePropietes();
            m_chkFullCopy.Checked = ActionCopierEntite.CopieComplete;
		}



		/// //////////////////////////////////////////
        private ArrayList m_listeProprietesACopier = new ArrayList();
		private void FillListePropietes()
		{
          
            CFournisseurPropDynStd fournisseurPropDynStd = new CFournisseurPropDynStd();
            fournisseurPropDynStd.AvecReadOnly = false;
            
            ArrayList m_listeProprietes = new ArrayList();

            if(ActionCopierEntite.ExpressionEntiteDestination != null)
                m_listeProprietes.AddRange( fournisseurPropDynStd.GetDefinitionsChamps(ActionCopierEntite.ExpressionEntiteDestination.TypeDonnee.TypeDotNetNatif, 0));
            m_listeProprietesACopier.AddRange(ActionCopierEntite.ProprietesDynamiquesACopier);

            // liste champs contient des CChampCustom
            ArrayList listeNomsChamps = new ArrayList();

            // Filtre la liste des champs custom
            if (m_txtSelectDefinisseurChampsCustom.ElementSelectionne != null)
            {
                IDefinisseurChampCustom definisseurChamps = (IDefinisseurChampCustom)m_txtSelectDefinisseurChampsCustom.ElementSelectionne;
                foreach (CChampCustom cc in new ArrayList(definisseurChamps.TousLesChampsAssocies))
                {
                    listeNomsChamps.Add(cc.Nom.Replace(" ", "_"));
                }
            }


            m_wndListePropietes.Items.Clear();
            foreach (CDefinitionProprieteDynamique defPropDyn in m_listeProprietes)
            {
                if (!defPropDyn.TypeDonnee.IsArrayOfTypeNatif && !defPropDyn.IsReadOnly && defPropDyn.Nom != "Id")
                {
                    if (defPropDyn is CDefinitionProprieteDynamiqueChampCustom)
                    {
                        if(!listeNomsChamps.Contains(defPropDyn.Nom))
                            continue;
                    }
                    ListViewItem item = new ListViewItem(defPropDyn.Nom);
                    item.Tag = defPropDyn;
                    if (m_listeProprietesACopier.Contains(defPropDyn))
                        item.Checked = true;
                    m_wndListePropietes.Items.Add(item);
                }
            }

			
		}

		/// //////////////////////////////////////////
		private string GetStringExpression ( object elementInterroge, object objet )
		{
			if ( objet == null )
				return "";
			return ((C2iExpression)objet).GetString();
		}


        //----------------------------------------------------------------------------------
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			
            CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression(ObjetEdite.Process, typeof(CProcess));
            CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
            // Sauve la formule retournant l'objet source
            result = analyseur.AnalyseChaine(m_txtFormuleObjetSource.Text);
            if (!result)
                return result;
            ActionCopierEntite.ExpressionEntiteSource = (C2iExpression)result.Data;

            ActionCopierEntite.CopieComplete = m_chkFullCopy.Checked;

            // Sauve la formule retournant l'objet destination
            result = analyseur.AnalyseChaine(m_txtFormuleObjetDestination.Text);
            if (!result)
                return result;
            ActionCopierEntite.ExpressionEntiteDestination = (C2iExpression)result.Data;

            if (m_txtSelectDefinisseurChampsCustom.ElementSelectionne != null)
                ActionCopierEntite.DbKeyDefinisseurChampsCustomEntiteDestination =
                    ((CObjetDonneeAIdNumeriqueAuto)m_txtSelectDefinisseurChampsCustom.ElementSelectionne).DbKey;
            else
                ActionCopierEntite.DbKeyDefinisseurChampsCustomEntiteDestination = null;

            // Sauve les propriétés dynamiques sélectionnées
            m_listeProprietesACopier.Clear();
            foreach (ListViewItem item in m_wndListePropietes.Items)
            {
                if (item.Checked && item.Tag is CDefinitionProprieteDynamique)
                {
                    m_listeProprietesACopier.Add(item.Tag);
                }
            }
            ActionCopierEntite.ProprietesDynamiquesACopier =
                (CDefinitionProprieteDynamique[])m_listeProprietesACopier.ToArray(typeof(CDefinitionProprieteDynamique));
			
			return result;
		}

        //----------------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormule != null )
				m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}
        
        //----------------------------------------------------------------------------------
        private void CFormEditActionCopierEntite_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }


        //--------------------------------------------------------------------------------
        private CControleEditeFormule m_txtFormule = null;
        private void OnEnterTexteFormule(object sender, System.EventArgs e)
        {
            if (m_txtFormule != null)
                m_txtFormule.BackColor = Color.White;
            if (sender is CControleEditeFormule)
            {
                m_txtFormule = (CControleEditeFormule)sender;
                m_txtFormule.BackColor = Color.LightGreen;
            }
        }

        //----------------------------------------------------------------------------------
        private void m_txtFormuleObjetDestination_Leave(object sender, EventArgs e)
        {
            CResultAErreur result = MAJ_Champs();
            
            InitPanelDefinisseurChamps();
            FillListePropietes();
        }

        //----------------------------------------------------------------------------------
        private void InitPanelDefinisseurChamps()
        {
            if (ActionCopierEntite.ExpressionEntiteDestination != null)
            {
                Type typeDestination = ActionCopierEntite.ExpressionEntiteDestination.TypeDonnee.TypeDotNetNatif;
                if ((typeof(IElementAChamps)).IsAssignableFrom(typeDestination))
                {
                    m_panelDefinisseurChamps.Visible = !m_chkFullCopy.Checked ;
                    CContexteDonnee ctx = ActionCopierEntite.Process.ContexteDonnee;

                    CRoleChampCustom role = CRoleChampCustom.GetRoleForType(typeDestination);
                    Type[] typeDefinisseursChamps = role.TypeDefinisseurs;

                    if (typeDefinisseursChamps.Length > 0)
                    {
                        
                        // Init la textBoxSelectionne
                        m_txtSelectDefinisseurChampsCustom.InitForSelect(typeDefinisseursChamps[0],
                            "Libelle",
                            false);

                        CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance(typeDefinisseursChamps[0], new object[] { ctx });
                        if (ActionCopierEntite.DbKeyDefinisseurChampsCustomEntiteDestination != null)
                        {
                            if (objet.ReadIfExists(ActionCopierEntite.DbKeyDefinisseurChampsCustomEntiteDestination))
                                m_txtSelectDefinisseurChampsCustom.ElementSelectionne = objet;
                            else
                                m_txtSelectDefinisseurChampsCustom.ElementSelectionne = null;
                        }
                        else
                            m_txtSelectDefinisseurChampsCustom.ElementSelectionne = null;
                     }
                }
                else
                {
                    m_panelDefinisseurChamps.Visible = false;
                }
            }

        }

        //------------------------------------------------------------------------------------------
        private void m_txtSelectDefinisseurChampsCustom_ElementSelectionneChanged(object sender, EventArgs e)
        {
            FillListePropietes();
        }

        private void m_chkFullCopy_CheckedChanged(object sender, EventArgs e)
        {
            m_wndListePropietes.Visible = !m_chkFullCopy.Checked;
            m_panelDefinisseurChamps.Visible = !m_chkFullCopy.Checked;
        }

 

	}
}

