using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Threading;
using sc2i.common;
using System.Text;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description rÃƒÂ©sumÃƒÂ©e de CFormProgression.
	/// </summary>
	public class CFormProgression : System.Windows.Forms.Form, IIndicateurProgression
	{
		private static CFormProgression m_form = null;
		//Vrai si l'utilisateur a demandÃƒÂ© l'annulation du traitement
		private bool m_bCancelRequest = false;

		private CGestionnaireSegmentsProgression m_gestionnaireSegments = new CGestionnaireSegmentsProgression();
		private ArrayList m_listeLibelles = new ArrayList();
		private System.Windows.Forms.Label m_labelText;
		private System.Windows.Forms.ProgressBar m_progressBar;
		private System.Windows.Forms.Button m_btnAnnuler;
		/// <summary>
		/// Variable nÃƒÂ©cessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormProgression()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur aprÃƒÂ¨s l'appel ÃƒÂ  InitializeComponent
			//

			m_gestionnaireSegments.PushSegment(0, 100);
		}

		/// <summary>
		/// Nettoyage des ressources utilisÃƒÂ©es.
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

		#region Code gÃƒÂ©nÃƒÂ©rÃƒÂ© par le Concepteur Windows Form
		/// <summary>
		/// MÃƒÂ©thode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette mÃƒÂ©thode avec l'ÃƒÂ©diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_labelText = new System.Windows.Forms.Label();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_labelText
            // 
            this.m_labelText.Location = new System.Drawing.Point(8, 8);
            this.m_labelText.Name = "m_labelText";
            this.m_labelText.Size = new System.Drawing.Size(368, 48);
            this.m_labelText.TabIndex = 0;
            this.m_labelText.Text = "Processing...|107";
            this.m_labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_progressBar
            // 
            this.m_progressBar.Location = new System.Drawing.Point(8, 56);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(368, 16);
            this.m_progressBar.TabIndex = 1;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.Location = new System.Drawing.Point(156, 80);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(72, 24);
            this.m_btnAnnuler.TabIndex = 2;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // CFormProgression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 116);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_labelText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CFormProgression";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Treatement|106";
            this.Load += new System.EventHandler(this.CFormProgression_Load);
            this.ResumeLayout(false);

		}
		#endregion
		/// /////////////////////////////////////////////////////////////////
		private void CFormProgression_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            CWin32Traducteur.Translate(this);

		}

		/// /////////////////////////////////////////////////////////////////
		public void PushSegment ( int nMin, int nMax )
		{
			m_gestionnaireSegments.PushSegment ( nMin, nMax );
		}

		/// /////////////////////////////////////////////////////////////////
		public void SetBornesSegment ( int nMin, int nMax )
		{
			m_gestionnaireSegments.MinValue = nMin;
			m_gestionnaireSegments.MaxValue = nMax;
		}

		/// /////////////////////////////////////////////////////////////////
		public void PopSegment()
		{
			m_gestionnaireSegments.PopSegment();
		}

		/// /////////////////////////////////////////////////////////////////
		public void SetValue ( int nValue )
		{
            m_progressBar.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    m_progressBar.Value = m_gestionnaireSegments.GetValeurReelle(nValue);
                    m_progressBar.Refresh();
                }
                catch { }
            }); 
			
		}

		/// /////////////////////////////////////////////////////////////////
		public void PushLibelle(string strLibelle)
		{
			m_listeLibelles.Add(strLibelle);
		}

		/// /////////////////////////////////////////////////////////////////
		public void PopLibelle()
		{
			if (m_listeLibelles.Count > 0)
				m_listeLibelles.RemoveAt(m_listeLibelles.Count - 1);
		}


		/// /////////////////////////////////////////////////////////////////
		public void SetInfo ( string strInfo )
		{
			StringBuilder bl = new StringBuilder();
			foreach (string strLib in m_listeLibelles)
				bl.Append(strLib + "/");
			bl.Append(strInfo);
            m_labelText.Invoke((MethodInvoker)delegate
            {
                m_labelText.Text = bl.ToString();
                m_labelText.Refresh();
            });
		}

		/// /////////////////////////////////////////////////////////////////
		public bool CancelRequest
		{
			get
			{
				return m_bCancelRequest;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public bool CanCancel
		{
			get
			{
				return m_btnAnnuler.Visible;
			}
			set
			{
				m_btnAnnuler.Visible = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		private void OnEndProcess ( IAsyncResult result )
		{
			ResetSegmentsProgression();
			Close();
			m_form = null;

		}

		/// /////////////////////////////////////////////////////////////////
		public static void StartThreadWithProgress ( string strTitre, ThreadStart fonctionDemarrage  )
		{
			if ( m_form == null )
				m_form = new CFormProgression();
			m_form.ResetSegmentsProgression();
			m_form.TopMost = true;
			m_form.m_labelText.Text = strTitre;
			m_form.Show();
			fonctionDemarrage.BeginInvoke ( new AsyncCallback ( m_form.OnEndProcess ), null ); 
		}


		/// /////////////////////////////////////////////////////////////////
		public void ResetSegmentsProgression()
		{
			m_gestionnaireSegments = new CGestionnaireSegmentsProgression();
		}

		/// /////////////////////////////////////////////////////////////////
		/// <summary>
		/// Attend la fin de l'action en tache de fond
		/// </summary>
		/// <param name="nSecondesMax"></param>
		public static void WaitEnd (  )
		{
			while ( m_form != null && m_form.Visible )
				System.Threading.Thread.Sleep(1000);
		}

		
		/// /////////////////////////////////////////////////////////////////
		public static IIndicateurProgression Indicateur
		{
			get
			{
				if ( m_form == null )
					m_form =new CFormProgression();
				return m_form;
			}
		}

		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			m_bCancelRequest = true;
		}

		/// ///////////////////////////////////////////////
		private static CFormProgression m_lastForm = null;
		private static string m_strTitre = "";
		private static void StartPopup()
		{
			m_lastForm = new CFormProgression();
			m_lastForm.TopMost = true;
			m_lastForm.Text = m_strTitre;
			m_lastForm.ShowDialog();
		}
		

		/// ///////////////////////////////////////////////
		public static IIndicateurProgression GetNewIndicateurAndPopup ( string strTitre )
		{
			Thread th = new Thread(new ThreadStart(StartPopup));
			m_lastForm = null;
			m_strTitre = strTitre;
			th.Start();
			while ( m_lastForm == null )
				Thread.Sleep(100);
			return m_lastForm;
		}

		/// ///////////////////////////////////////////////
		public static void EndIndicateur ( IIndicateurProgression indicateur )
		{
            if (indicateur is System.Windows.Forms.Form)
            {
                ((System.Windows.Forms.Form)indicateur).Invoke((MethodInvoker)delegate
                {
                    ((System.Windows.Forms.Form)indicateur).Hide();
                });
            }
		}


		////////////////////////////////////////////////////
		public void Masquer(bool bMasquer)
		{
			if (bMasquer)
				SendToBack();
			else
			{
				BringToFront();
				Refresh();
			}
		}
	}
}
