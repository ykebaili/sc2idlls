using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Description r�sum�e de CFormVisuFormulairePDA.
	/// </summary>
	public class CFormVisuFormulairePDA : System.Windows.Forms.Form
	{
		/// <summary>
		/// Variable n�cessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormVisuFormulairePDA()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur apr�s l'appel � InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilis�es.
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

		#region Code g�n�r� par le Concepteur Windows Form
		/// <summary>
		/// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette m�thode avec l'�diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// CFormVisuFormulairePDA
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(222, 276);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CFormVisuFormulairePDA";
			this.Text = "PDA Simulation|101";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
