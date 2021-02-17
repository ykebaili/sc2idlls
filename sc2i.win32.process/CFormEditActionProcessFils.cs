using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionProcessFils : sc2i.win32.process.CFormEditObjetDeProcess
    {
        private CProcessEditor m_processEditor;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionProcessFils()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionProcessFils), typeof(CFormEditActionProcessFils));
		}


		public CActionProcessFils ActionProcessFils
		{
			get
			{
                return (CActionProcessFils)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_processEditor = new sc2i.win32.process.CProcessEditor();
            this.SuspendLayout();
            // 
            // m_processEditor
            // 
            this.m_processEditor.BackColor = System.Drawing.Color.White;
            this.m_processEditor.DisableTypeElement = true;
            this.m_processEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_processEditor.Location = new System.Drawing.Point(0, 0);
            this.m_processEditor.LockEdition = false;
            this.m_processEditor.Name = "m_processEditor";
            this.m_processEditor.Process = null;
            this.m_processEditor.Size = new System.Drawing.Size(815, 536);
            this.m_processEditor.TabIndex = 2;
            // 
            // CFormEditActionProcessFils
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(815, 536);
            this.Controls.Add(this.m_processEditor);
            this.Name = "CFormEditActionProcessFils";
            this.Text = "Action group|20034";
            this.Load += new System.EventHandler(this.CFormEditActionProcessFils_Load);
            this.Controls.SetChildIndex(this.m_processEditor, 0);
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
            ActionProcessFils.ProcessFils = m_processEditor.Process; 
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
            m_processEditor.Process = ActionProcessFils.ProcessFils;
		}

		

        private void CFormEditActionProcessFils_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		
		

		




	}
}

