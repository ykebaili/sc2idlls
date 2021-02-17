using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace sc2i.win32.common
{
	public enum C2iLinkApparence
	{
		Link,
		Label
	}
	/// <summary>
	/// Description résumée de C2iLink.
	/// </summary>
	[DefaultEvent("LinkClicked")]
	public class C2iLink : System.Windows.Forms.Label
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		//private System.ComponentModel.Container components = null;

		private C2iLinkApparence m_linkApparence = C2iLinkApparence.Label;
		private Color m_linkEnabledColor = Color.Blue;
		private Color m_linkDisabledColor = Color.Blue;
		private Color m_labelColor = SystemColors.ControlText;
		private Font m_font;
		private bool m_bClickEnabled = false;

		public C2iLink(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			container.Add(this);
			InitializeComponent();
		}

		public C2iLink()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------
		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// C2iLink
			// 
			this.Click += new System.EventHandler(this.C2iLink_Click);

		}
		#endregion
		//---------------------------------------------------------------------------------
		public Color ColorLinkEnabled
		{
			get
			{
				return m_linkEnabledColor;
			}
			set
			{
				m_linkEnabledColor = value;
				Apparence = m_linkApparence;
			}
		}
		//---------------------------------------------------------------------------------
		public Color ColorLinkDisabled
		{
			get
			{
				return m_linkDisabledColor;
			}
			set
			{
				m_linkDisabledColor = value;
				Apparence = m_linkApparence;
			}
		}
		//---------------------------------------------------------------------------------
		public Color ColorLabel
		{
			get
			{
				return m_labelColor;
			}
			set
			{
				m_labelColor = value;
				Apparence = m_linkApparence;
			}
		}
		//---------------------------------------------------------------------------------
		public bool ClickEnabled
		{
			get
			{
				return m_bClickEnabled;
			}
			set
			{
				m_bClickEnabled = value;
				if (value)
					Apparence = C2iLinkApparence.Link;
				else
					Apparence = C2iLinkApparence.Label;
			}
		}
		//---------------------------------------------------------------------------------
		public event EventHandler LinkClicked;
		//---------------------------------------------------------------------------------
		private void C2iLink_Click(object sender, System.EventArgs e)
		{
			if (!ClickEnabled)
				return;

			if (LinkClicked != null)
				LinkClicked(sender, e);
		}
		//---------------------------------------------------------------------------------
		private C2iLinkApparence Apparence
		{
			get
			{
				return m_linkApparence;
			}
			set
			{
				m_linkApparence = value;
				if (m_linkApparence == C2iLinkApparence.Label)
				{
					m_font = new Font(this.Font, FontStyle.Regular);
					this.Font = m_font;
					this.Cursor = Cursors.Default;
					this.ForeColor = m_labelColor;
				}
				if (m_linkApparence == C2iLinkApparence.Link)
				{
					m_font = new Font(this.Font, FontStyle.Underline);
					this.Font = m_font;
					this.Cursor = Cursors.Hand;
					if (this.Enabled)
						this.ForeColor = m_linkEnabledColor;
					else
						this.ForeColor = m_linkDisabledColor;
				}
				this.Invalidate();
			}
		}
		//---------------------------------------------------------------------------------
	}
}
