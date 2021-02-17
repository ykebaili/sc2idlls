using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using sc2i.common;
using sc2i.process;
using sc2i.win32.common;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CFormEditLienAction.
	/// </summary>
	public class CFormEditObjetDeProcess : System.Windows.Forms.Form
	{
		#region Code généré par le Concepteur Windows Form

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.common.CExtLinkField m_extLinkField;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		

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

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditObjetDeProcess));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_extLinkField.SetLinkField(this.panel1, "");
            this.panel1.Location = new System.Drawing.Point(0, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 48);
            this.panel1.TabIndex = 1;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnAnnuler, "");
            this.m_btnAnnuler.Location = new System.Drawing.Point(153, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 3;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnOk, "");
            this.m_btnOk.Location = new System.Drawing.Point(99, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditObjetDeProcess
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.m_extLinkField.SetLinkField(this, "");
            this.MinimizeBox = false;
            this.Name = "CFormEditObjetDeProcess";
            this.ShowIcon = false;
            this.Text = "Link properties|168";
            this.Load += new System.EventHandler(this.CFormEditLienAction_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


		public CFormEditObjetDeProcess()
		{
			InitializeComponent();
		}

		private void CFormEditLienAction_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            if (DesignMode)
				return;
			InitChamps();
			
		}

		/// ////////////////////////////////////////
		protected virtual CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = m_extLinkField.FillObjetFromDialog ( ObjetEdite );
			return result;
		}

		/// ////////////////////////////////////////
		protected virtual void InitChamps()
		{
			m_extLinkField.FillDialogFromObjet ( ObjetEdite );
		}

		/// ////////////////////////////////////////
		protected virtual CResultAErreur VerifieDonneesAndMAJSiIlFaut()
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter binWriter = new BinaryWriter(stream);
			sc2i.common.CSerializerSaveBinaire saver = new CSerializerSaveBinaire(binWriter);
			
			CResultAErreur result = ObjetEdite.Serialize ( saver );
			//NE se sérialise pas, surement un nouvel objet
			bool bSansFilet = !result.Result;
			result = CResultAErreur.True;
			try
			{
				result = MAJ_Champs();
				if ( result )
					result = ObjetEdite.VerifieDonnees();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			if ( !result )
				if ( !bSansFilet )
				{
					stream.Seek(0, SeekOrigin.Begin);
					BinaryReader binReader = new BinaryReader ( stream );
					CSerializerReadBinaire reader = new CSerializerReadBinaire(binReader);
					ObjetEdite.Serialize(reader);
					binReader.Close();
				}
			binWriter.Close();
            stream.Close();
			return result;
		}


		/// ////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = VerifieDonneesAndMAJSiIlFaut();
			if ( result )
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				CFormAlerte.Afficher( result);
			}
		}
	
		/// ////////////////////////////////////////
		private IObjetDeProcess m_objetEdite = null;
		public IObjetDeProcess ObjetEdite
		{
			get
			{
				return m_objetEdite;
			}
			set
			{
				m_objetEdite = value;
			}
		}

		
	}
}
