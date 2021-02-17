using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de IntellisenseTextBox.
	/// </summary>
	public class IntellisenseTextBox : System.Windows.Forms.UserControl, IControlALockEdition
	{


		/// <summary>
		/// Indique que l'on valide le contenu de la liste en quittant le champ.
		/// </summary>
		private bool m_bValiderEnQuittant = false;
        private bool m_bReplaceAllText = false;
		private bool m_bAvecBouton = false;

		private CArbreVocabulaire m_arbre = null;
		private bool m_bAcceptReturn = true;

		private ListBox m_listBox = null;
		protected System.Windows.Forms.RichTextBox m_textBox;
		
		private string m_strSeparateursPrincipaux = ";.\r\t\n,()[]{}";
		private string m_strSeparateursSecondaires = " \r\t\n";

		private bool m_bDisableIntellisense = false;
		private Button m_btnShowListe;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public IntellisenseTextBox()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
			OnChangeLockEdition += new EventHandler(IntellisenseTextBox_OnChangeLockEdition);
		}

		void IntellisenseTextBox_OnChangeLockEdition(object sender, EventArgs e)
		{
			m_btnShowListe.Enabled = !LockEdition;
		}

		public virtual CArbreVocabulaire Arbre
		{
			get
			{
				return m_arbre;
			}
			set
			{
				m_arbre = value;
			}
		}

		public virtual string SeparateursPrincipaux
		{
			get
			{
				return m_strSeparateursPrincipaux;
			}
			set
			{
				m_strSeparateursPrincipaux = value;
			}
		}

		public virtual string SeparateursSecondaires
		{
			get
			{
				return m_strSeparateursSecondaires;
			}
			set
			{
				m_strSeparateursSecondaires = value;
			}
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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_textBox = new System.Windows.Forms.RichTextBox();
            this.m_btnShowListe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_textBox
            // 
            this.m_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_textBox.Location = new System.Drawing.Point(0, 0);
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(333, 200);
            this.m_textBox.TabIndex = 0;
            this.m_textBox.Text = "richTextBox1";
            this.m_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyDown);
            this.m_textBox.VisibleChanged += new System.EventHandler(this.m_textBox_VisibleChanged);
            this.m_textBox.Enter += new System.EventHandler(this.m_textBox_Enter);
            this.m_textBox.Leave += new System.EventHandler(this.m_textBox_Leave);
            this.m_textBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_textBox_MouseMove);
            this.m_textBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_textBox_MouseDown);
            this.m_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_textBox_KeyPress);
            this.m_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyUp);
            this.m_textBox.Move += new System.EventHandler(this.m_textBox_Move);
            // 
            // m_btnShowListe
            // 
            this.m_btnShowListe.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnShowListe.Location = new System.Drawing.Point(333, 0);
            this.m_btnShowListe.Name = "m_btnShowListe";
            this.m_btnShowListe.Size = new System.Drawing.Size(27, 200);
            this.m_btnShowListe.TabIndex = 1;
            this.m_btnShowListe.TabStop = false;
            this.m_btnShowListe.Text = "...";
            this.m_btnShowListe.UseVisualStyleBackColor = true;
            this.m_btnShowListe.Click += new System.EventHandler(this.m_btnShowListe_Click);
            // 
            // IntellisenseTextBox
            // 
            this.Controls.Add(this.m_textBox);
            this.Controls.Add(this.m_btnShowListe);
            this.Name = "IntellisenseTextBox";
            this.Size = new System.Drawing.Size(360, 200);
            this.BackColorChanged += new System.EventHandler(this.IntellisenseTextBox_BackColorChanged);
            this.ResumeLayout(false);

		}
		#endregion

		/// ///////////////////////////////////////////
		protected ListBox ListBox
		{
			get
			{
				if ( m_listBox == null )
				{
					m_listBox = new ListBox();
					m_listBox.Parent = FindForm();
                    
					m_listBox.BorderStyle = BorderStyle.FixedSingle;
					m_listBox.Font = new Font ( "Arial", 7 );
                    m_listBox.Height = 100;
                    m_listBox.Width = 150;
                    m_listBox.Visible = false;
					m_listBox.CreateControl();
					m_listBox.TabStop = false;
					m_listBox.LocationChanged += new EventHandler(m_listBox_LocationChanged);
					m_listBox.Click += new EventHandler(m_listBox_Click);
                    m_listBox.Leave += new EventHandler(m_listBox_Leave);
					
				}
				return m_listBox;
			}
		}

        void m_listBox_Leave(object sender, EventArgs e)
        {
            if (ListBox.Visible)
                ListBox.Hide();
        }

		public virtual void Init ( CArbreVocabulaire arbreVocabulaire )
		{
			m_arbre = arbreVocabulaire;
		}


		/// ///////////////////////////////////////////
		public string GetLastWord( string strSeparateurs )
		{
			int nPos = m_textBox.SelectionStart;
			string strWord = "";
			if ( nPos >= 0 && nPos <= m_textBox.Text.Length)
			{
				nPos--;
				if ( nPos >= 0 )
				{
					char f = m_textBox.Text[nPos];
					strWord = f+"";
					while ( nPos > 0 && strSeparateurs.IndexOf(f) < 0 )
					{
						nPos--;
						f = m_textBox.Text[nPos];
						if ( strSeparateurs.IndexOf(f)<0 )
							strWord += f;
					}

					char[] ca = strWord.ToCharArray();
					Array.Reverse( ca );
					strWord = new String( ca );
					strWord.Trim();
				}

			}
			return strWord.Trim();
		}

		/// ///////////////////////////////////////////
		public override string Text
		{
			get
			{
				return m_textBox.Text;
			}
			set
			{
				m_textBox.Text = value;
			}
		}

        //-----------------------------------------------------------------------
        [DefaultValue(false)]
		public bool ValiderEnQuittant
		{
			get
			{
				return m_bValiderEnQuittant;
			}
			set
			{
				m_bValiderEnQuittant = value;
			}
		}

        //-----------------------------------------------------------------------
        [DefaultValue(false)]
        public bool ReplaceAllText
        {
            get
            {
                return m_bReplaceAllText;
            }
            set
            {
                m_bReplaceAllText = value;
            }
        }
        


		//-----------------------------------------------------------------------
		private void ReplaceLastWord ( string strNouveau )
		{

            if (m_bReplaceAllText)
            {
                // On remplace tous le texte de la textbox
                this.m_textBox.Text = strNouveau;
            }
            else
            {
                // On ne remplace que le mot en cours

                int pos = this.m_textBox.SelectionStart;
                if (pos >= 0)
                {
                    if (pos > 0)
                    {
                        pos--;
                        char f = m_textBox.Text[pos];
                        while (pos > 0 && m_strSeparateursInList.IndexOf(f) < 0)
                        {
                            pos--;
                            f = m_textBox.Text[pos];
                        }
                    }
                    if (pos > 1)
                        pos++;
                    string strText = m_textBox.Text;
                    while (strText.Length > pos && strText[pos] == ' ')
                        pos++;
                    bool bReplace = true;
                    if (m_textBox.SelectionStart - pos > 0)
                    {
                        string strLastWord = strText.Substring(pos, m_textBox.SelectionStart - pos);
                        if (strLastWord.Trim().ToLower() == strNouveau.ToLower())
                            bReplace = false;
                    }
                    if (bReplace)
                    {
                        if (m_textBox.SelectionStart - pos > 0)

                            strText = strText.Remove(pos, m_textBox.SelectionStart - pos);
                        if (pos < strText.Length)
                            m_textBox.Text = strText.Insert(pos, strNouveau);
                        else
                            m_textBox.Text = strText + strNouveau;
                        m_textBox.SelectionStart = pos + strNouveau.Length;
                    }
                }
            }
		}


		/// ///////////////////////////////////////////
		private string m_strSeparateursInList = "";
		private void FillListBox( )
		{
			if ( Arbre == null )
				return ;
			int nPosInText = m_textBox.SelectionStart;

			ListBox.Items.Clear();
			int nWidth = 150;
			m_strSeparateursInList = m_strSeparateursPrincipaux;
			string strWord = GetLastWord(m_strSeparateursPrincipaux);
			ArrayList lstMots = Arbre.GetMots ( strWord );
			if ( lstMots.Count == 0 )
			{
				strWord = GetLastWord ( m_strSeparateursSecondaires );
				m_strSeparateursInList = m_strSeparateursSecondaires ;
				lstMots = Arbre.GetMots ( strWord );
			}
			string strLast = "";
			foreach ( string strVoc in Arbre.GetMots ( strWord ) )
			{
				strLast = strVoc;
				ListBox.Items.Add ( strVoc );
				nWidth = (int)Math.Max ( Math.Max ( nWidth, strVoc.Length * ListBox.Font.Size ), 150 );
			}
			ListBox.Width = nWidth;

		}

		/// ///////////////////////////////////////////
		public bool DisableIntellisense
		{
			get
			{
				return m_bDisableIntellisense;
			}
			set
			{
				m_bDisableIntellisense = value;
				if ( m_bDisableIntellisense )
					ListBox.Visible = false;
			}
		}


		/// ///////////////////////////////////////////
		private void ShowListBox( int nPosInText )
		{
			if ( !m_bDisableIntellisense && !LockEdition )
			{
				Point point = m_textBox.GetPositionFromCharIndex(m_textBox.SelectionStart);
				point = PointToScreen ( point );
				point = ListBox.Parent.PointToClient ( point );
				point.Y += (int) Math.Ceiling(this.m_textBox.Font.GetHeight()) + 2;
				point = GetPositionListBox ( point );
				FillListBox(  );
				ListBox.Location = point;
				if ( ListBox.Items.Count > 0 )
				{
					ListBox.SelectedIndex = 0;
					ListBox.Show();
					ListBox.BringToFront();
                    m_textBox.Capture = true;
				}
			}
		}

		//-----------------------------------------------------
		private Point GetPositionListBox ( Point pt )
		{
			Rectangle rectBound = m_listBox.Parent.ClientRectangle;
			if ( pt.Y+m_listBox.Height > rectBound.Bottom )
			{
				if ( m_listBox.Height > 40 && pt.Y + 40 < rectBound.Bottom )
					m_listBox.Height = 40;
				else
					pt.Y -= m_listBox.Height + m_textBox.Font.Height;
			}
			if ( pt.X+m_listBox.Width > rectBound.Right )
			{
				pt.X -= m_listBox.Width;
			}
			if (pt.Y < 0)
			{
				m_listBox.Height = 40;
				pt.Y = 0;
			}
			return pt;
		}

		/// ///////////////////////////////////////////
		private bool m_bCancelNextKeyPress = false;
		private void m_textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			int i = this.m_textBox.SelectionStart;

			if ( (e.Modifiers & Keys.Control) == Keys.Control && 
				e.KeyCode == Keys.Space )
			{
				ShowListBox(m_textBox.SelectionStart);
				e.Handled = true;
				m_bCancelNextKeyPress = true;
			}

			if ( e.KeyCode == Keys.Escape && ListBox.Visible )
			{
				ListBox.Hide();
				e.Handled = true;
			}

			if ( e.KeyCode == Keys.Down && ListBox.Visible )
			{
				if ( ListBox.SelectedIndex < ListBox.Items.Count-1 )
				{
					ListBox.SelectedIndex++;
					e.Handled = true;
				}
			}

			if ( e.KeyCode == Keys.Up && ListBox.Visible )
			{
				if ( ListBox.SelectedIndex >= 0 )
				{
					ListBox.SelectedIndex--;
					e.Handled = true;
				}
			}

			if ( (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab ) && ListBox.Visible  )
			{
				InsereFromListBox();
				e.Handled = true;
				m_bCancelNextKeyPress= true;
			}

			if ( (e.KeyCode == Keys.End && ListBox.Visible ) )
			{
				ListBox.SelectedIndex = ListBox.Items.Count-1;
				InsereFromListBox();
				e.Handled = true;
			}
				
		}

		private void InsereFromListBox()
		{
			if ( ListBox.SelectedIndex >= 0 )
				ReplaceLastWord ( ListBox.SelectedItem.ToString() );
			ListBox.Hide();
			Focus();
		}

		private void m_textBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
			if ( (e.KeyValue >=  '0' && e.KeyValue <= 'z') || (ListBox.Visible && e.KeyCode != Keys.Up &&
				e.KeyCode != Keys.Down ))
			{
				if ( !ListBox.Visible )
				{
					ShowListBox(m_textBox.SelectionStart);
				}
				else
				{
					Point point = m_textBox.GetPositionFromCharIndex(m_textBox.SelectionStart);
					point = PointToScreen ( point );
					point = ListBox.Parent.PointToClient ( point );
					point.Y += (int) Math.Ceiling(this.m_textBox.Font.GetHeight()) + 2;
					ListBox.Location = point;
					FillListBox(  );
					if ( ListBox.Items.Count > 0 )
						ListBox.SelectedIndex = 0;
					else
						ListBox.Hide();
				}
			}
		}

		private void m_textBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if( m_bCancelNextKeyPress )
				e.Handled = true;
			else 
			{
				if ( ListBox.Visible )
				{
					if ( m_strSeparateursInList.IndexOf(e.KeyChar) >= 0 )
					{
						//Le texte sélectionné ne doit pas contenir le caractère
						if ( ListBox.SelectedIndex >= 0 &&
							ListBox.SelectedItem.ToString().IndexOf(e.KeyChar) < 0 )
							InsereFromListBox();
					}
				}
			}
			m_bCancelNextKeyPress = false;
		}

		private void m_textBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if ( ListBox.Visible )
            {
                Point pt = new Point(e.X, e.Y);
                pt = PointToScreen(pt);
                pt = ListBox.PointToClient(pt);
                if (pt.X >= 0 && pt.X < ListBox.Size.Width &&
                    pt.Y >= 0 && pt.Y < ListBox.Size.Height)
                {
                    if (pt.X < ListBox.ClientSize.Width &&
                        pt.Y < ListBox.ClientRectangle.Height)
                    {
                        int nIndex = m_listBox.IndexFromPoint(pt);
                        if (nIndex >= 0 && nIndex < m_listBox.Items.Count)
                            m_listBox.SelectedIndex = nIndex;
                        m_listBox_Click(m_listBox, null);
                    }
                    else
                    {
                        m_textBox.Capture = false;
                    }
                }
                else
                    ListBox.Hide();
            }
		}

		private void m_listBox_LocationChanged(object sender, EventArgs e)
		{
			Rectangle rectBound = m_listBox.Parent.ClientRectangle;
			if ( m_listBox.Bottom > rectBound.Bottom )
			{
				m_listBox.Top -= m_listBox.Height + m_textBox.Font.Height;
				return;
			}
		}

		public event EventHandler OnEnterTextBox;
		public event EventHandler OnLeaveTextBox;

		private void m_textBox_Enter(object sender, System.EventArgs e)
		{
			if ( OnEnterTextBox != null )
				OnEnterTextBox ( sender, e );
		}

		private void m_textBox_Leave(object sender, System.EventArgs e)
		{
			if ( !ListBox.Focused )
			{
				if ( ListBox.Visible )
				{
					if ( m_bValiderEnQuittant )
					//si la boite est visible, on envoie ce qui est sélectionné dans la textBox
						InsereFromListBox();
					ListBox.Visible =  false;
				}
				if ( OnLeaveTextBox != null )
					OnLeaveTextBox ( sender, e );
			}
		}

		private void IntellisenseTextBox_BackColorChanged(object sender, System.EventArgs e)
		{
			m_textBox.BackColor = BackColor;
		}

		public RichTextBox TextBox
		{
			get
			{
				return m_textBox;
			}
		}

		public bool AcceptReturn
		{
			get
			{
				return m_bAcceptReturn;
			}
			set
			{
				m_bAcceptReturn = value;
				m_textBox.Multiline = value;
			}
		}

		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return m_textBox.ReadOnly;
			}
			set
			{
				m_textBox.ReadOnly = value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

		private void m_listBox_Click(object sender, EventArgs e)
		{
            Point pt = Cursor.Position;
            pt = m_listBox.PointToClient(pt);
            if (pt.X < 0 || pt.X > ListBox.Size.Width ||
                pt.Y < 0 || pt.Y > ListBox.Size.Height)
            {
                ListBox.Hide();
            }
            ///Stef 27/07/2010 : n'insere pas si on n'est pas dans la
            ///zone cliente (dans l'ascenseur)
            if ( pt.X < ListBox.ClientSize.Width && 
                pt.Y < ListBox.ClientSize.Height )
			    InsereFromListBox();
		}


		public bool AvecBouton
		{
			get
			{
				return m_bAvecBouton;
			}
			set
			{
				m_bAvecBouton = value;
				m_btnShowListe.Visible = m_bAvecBouton;
			}
		}

		private void m_btnShowListe_Click(object sender, EventArgs e)
		{
			int nPos = m_textBox.SelectionStart;
			ShowListBox(nPos);
		}

        private void m_textBox_VisibleChanged(object sender, EventArgs e)
        {
            if (ListBox.Visible)
                ListBox.Hide();
        }

        private void m_textBox_Move(object sender, EventArgs e)
        {
            if (ListBox.Visible)
                ListBox.Hide();

        }

        private void m_textBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_textBox.Capture)
            {
                m_textBox.Cursor = Cursors.IBeam;
                return;
            }
            Point pt = new Point(e.X, e.Y);
            if (pt.X >= 0 && pt.X < m_textBox.Size.Width &&
                pt.Y >= 0 && pt.Y < m_textBox.Size.Height)
            {
                m_textBox.Cursor = Cursors.IBeam;
                if (ListBox.Visible)
                {
                    pt = ListBox.PointToClient(m_textBox.PointToScreen(pt));
                    if (pt.X >= 0 && pt.Y >= 0 &&
                        pt.X <= ListBox.Width && pt.Y <= ListBox.Height)
                        m_textBox.Cursor = Cursors.Arrow;
                }
            }
            else
                m_textBox.Cursor = Cursors.Arrow;
        }
	}
}

