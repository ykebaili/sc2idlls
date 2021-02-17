using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Contrôle d'édition d'une formule.
	/// </summary>
	/// <remarks>
	/// Avant de commencer à travailler, il est nécéssaire d'initialiser le contrôle
	/// avec un fournisseur de propriétés et un type analysé.<BR></BR>
	/// La propriété Formule analyse et retourne la formule.<BR></BR>
	/// Le propriété ResultAnalyse retourne le résultat de la dernière analyse
	/// </remarks>
	public class CControleEditeFormule : System.Windows.Forms.UserControl, IControlALockEdition
	{
		/*[DllImport("user32.dll")]
		public static extern int SendMessage(int hWnd, uint Msg, int wParam, ref LVCOLUMN pCol);*/

		private bool m_bModeEdition = true;
		private const int m_delaiParentheses = 500;

		private ListBox m_listBox = null;

		private CMotVocabulaire m_vocabulaire = null;
		

		private C2iExpression m_formule = null;
		private CResultAErreur m_resultAnalyse = CResultAErreur.True;
		private IFournisseurProprietesDynamiques m_fournisseur;
		private CObjetPourSousProprietes m_objetAnalyse = null;

		private CAnalyseurSyntaxiqueExpression m_analyseur = null;
		
		private System.Windows.Forms.RichTextBox m_textBox;
		private System.Windows.Forms.TextBox m_startMarker;
		private System.Windows.Forms.Timer m_timerMarker;
		private System.ComponentModel.IContainer components;

		public CControleEditeFormule()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
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
            this.components = new System.ComponentModel.Container();
            this.m_textBox = new System.Windows.Forms.RichTextBox();
            this.m_startMarker = new System.Windows.Forms.TextBox();
            this.m_timerMarker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_textBox
            // 
            this.m_textBox.AcceptsTab = true;
            this.m_textBox.AutoWordSelection = true;
            this.m_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_textBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_textBox.Location = new System.Drawing.Point(0, 0);
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(272, 150);
            this.m_textBox.TabIndex = 0;
            this.m_textBox.Text = " ";
            this.m_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyDown);
            this.m_textBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_textBox_MouseDown);
            this.m_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_textBox_KeyPress);
            this.m_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyUp);
            this.m_textBox.SelectionChanged += new EventHandler(m_textBox_SelectionChanged);
            // 
            // m_startMarker
            // 
            this.m_startMarker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.m_startMarker.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_startMarker.ForeColor = System.Drawing.Color.White;
            this.m_startMarker.Location = new System.Drawing.Point(192, 112);
            this.m_startMarker.Name = "m_startMarker";
            this.m_startMarker.Size = new System.Drawing.Size(48, 15);
            this.m_startMarker.TabIndex = 1;
            this.m_startMarker.Text = "StartMark";
            this.m_startMarker.Visible = false;
            this.m_startMarker.VisibleChanged += new System.EventHandler(this.m_startMarker_VisibleChanged);
            // 
            // m_timerMarker
            // 
            this.m_timerMarker.Interval = 1000;
            this.m_timerMarker.Tick += new System.EventHandler(this.m_timerMarker_Tick);
            // 
            // CControleEditeFormule
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_startMarker);
            this.Controls.Add(this.m_textBox);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CControleEditeFormule";
            this.Size = new System.Drawing.Size(272, 150);
            this.BackColorChanged += new System.EventHandler(this.CControleEditeFormule_BackColorChanged);
            this.FontChanged += new System.EventHandler(this.CControleEditeFormule_FontChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		#region gestion de la formule contenue

		/// <summary>
		/// formule contenue dans la zone. Null si la formule est incorrecte
		/// </summary>
		[Browsable ( false )]
		public C2iExpression Formule
		{
			get
			{
				if (DesignMode)
					return null;
				if ( m_formule == null )
					AnalyseTexte();
				return m_formule;
			}
			set
			{
				if ( value == null )
					m_textBox.Text = "";
				else
					m_textBox.Text = value.GetString();
				m_formule = value;
			}
		}

		/// <summary>
		/// Retourne le résultat de l'analyse du texte pour le convertir en formule
		/// </summary>
		public CResultAErreur ResultAnalyse
		{
			get
			{
				if ( m_formule == null )
					AnalyseTexte();
				return m_resultAnalyse;
			}
		}

		public void Init(
			IFournisseurProprietesDynamiques fournisseur,
			Type typeAnalyse,
			CAnalyseurSyntaxiqueExpression analyseur)
		{
			Init(fournisseur, typeAnalyse);
			if (analyseur != null)
				m_analyseur = analyseur;
		}

		public void Init(IFournisseurProprietesDynamiques fournisseur, Type typeAnalyse)
		{
			Init ( fournisseur, new CObjetPourSousProprietes ( typeAnalyse ));
		}

		public void Init(IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse)
		{
			m_fournisseur = fournisseur;
			m_objetAnalyse = objetAnalyse;
			m_formule = null;
			m_vocabulaire = null;
			CContexteAnalyse2iExpression contexteAnalyse = new CContexteAnalyse2iExpression(fournisseur, m_objetAnalyse);
			m_analyseur = new CAnalyseurSyntaxiqueExpression(contexteAnalyse);
		}


		private void AnalyseTexte()
		{
			m_formule = null;
            if (m_analyseur != null)
            {
                m_resultAnalyse = m_analyseur.AnalyseChaine(m_textBox.Text);
                if (m_resultAnalyse)
                    m_formule = (C2iExpression)m_resultAnalyse.Data;
            }
		}

		private bool m_bCancelNextKeyPress = false;
		private void m_textBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			m_formule = null;
			if( m_bCancelNextKeyPress )
				e.Handled = true;
			else 
			{
				if ( ListBox.Visible )
				{
					if ( (m_strSeparateurs+".").IndexOf(e.KeyChar) >= 0 )
					{
						//Le texte sélectionné ne doit pas contenir le caractère
						if ( ListBox.SelectedIndex >= 0 &&
							ListBox.SelectedItem.ToString().IndexOf(e.KeyChar) < 0 )
							InsereFromListBox();
					}
				}
			}
			m_bCancelNextKeyPress = false;

            //if (")}]".IndexOf(e.KeyChar) >= 0)
            //    ShowCouple(m_textBox.SelectionStart, e.KeyChar);

			/*if ( e.KeyChar == '.' && !ListBox.Visible )
				ShowListBox ( m_textBox.SelectionStart );*/
		}

        void m_textBox_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_bAutoriseEventSelectionChanged)
                return;

            //Pour éviter des déclenchements intenpestif de l'évennement SelectionChanged 
            // Sinon plantage en "StackOverFlow" !!
            if (m_textBox.SelectionLength > 0)
                return;

            int nPosition = m_textBox.SelectionStart;
            char carGauche = '\0';
            char carDroite = '\0';

            if (nPosition > 0)
                carGauche = m_textBox.Text[nPosition - 1];
            if (nPosition < m_textBox.Text.Length)
                carDroite = m_textBox.Text[nPosition];

            if (carGauche != '\0' && ")}]".IndexOf(carGauche) >= 0)
                ShowCouple(m_textBox.SelectionStart, carGauche);

        }

		private void InsereFromListBox()
		{
			if ( ListBox.SelectedIndex >= 0 )
				ReplaceLastWord ( (CMotVocabulaire)ListBox.SelectedItem );
			ListBox.Hide();
		}
		#endregion

		#region gestion du dictionnaire
		/// ///////////////////////////////////////////
		private ListBox ListBox
		{
			get
			{
				if ( m_listBox == null )
				{
					m_listBox = new ListBox();
					Control ctrl = FindForm();
					if ( ctrl == null )
					{
						ctrl = this;
						while ( ctrl.Parent != null )
							ctrl = ctrl.Parent;
					}
					m_listBox.Parent = ctrl;
					m_listBox.Height = 100;
					m_listBox.Width = 150;
					m_listBox.BorderStyle = BorderStyle.FixedSingle;
					m_listBox.Font = new Font ( "Arial", 7 );
					m_listBox.Visible = false;
					m_listBox.CreateControl();
					m_listBox.Items.Add("Bonjour");
					m_listBox.Items.Add("Bonjour les");
					m_listBox.Items.Add ("Bonjour les amis");
					m_listBox.LocationChanged += new EventHandler(m_listBox_LocationChanged);
					m_listBox.KeyDown += new KeyEventHandler ( m_ListBox_KeyDown );
					m_listBox.DoubleClick += new EventHandler(m_listBox_DoubleClick);
					
				}
				return m_listBox;
			}
		}

		private void m_listBox_LocationChanged(object sender, EventArgs e)
		{
			Point pt = GetPositionListBox ( m_listBox.Location );
			if ( pt != m_listBox.Location )
				m_listBox.Location = pt;
		}
		#endregion

		/// ///////////////////////////////////////////
		public string GetLastWord( string strSeparateurs )
		{
			int nPos = m_textBox.SelectionStart;
			string strWord = "";
			if ( nPos >= 0 && nPos <= m_textBox.Text.Length)
			{
				nPos--;
                bool bWaitCrochetOuvrant = false;
				if ( nPos >= 0 )
				{
					char f = m_textBox.Text[nPos];
					strWord = f+"";
					while ( nPos > 0 && (strSeparateurs.IndexOf(f) < 0 || bWaitCrochetOuvrant))
					{
						nPos--;
						f = m_textBox.Text[nPos];
						if ( strSeparateurs.IndexOf(f)<0 || bWaitCrochetOuvrant )
							strWord += f;

                        //Stef, le 30 09 2011, on se sort plus dés qu'on trouve un crochet ouvrant,
                        //pour gérer [machin].[bidule].[truc]
                        if (f == ']')
                            bWaitCrochetOuvrant = true;
                        if (bWaitCrochetOuvrant && f == '[')
                            bWaitCrochetOuvrant = false;
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
		private void ReplaceLastWord ( CMotVocabulaire mot )
		{

			int pos = this.m_textBox.SelectionStart;
			if ( pos >= 0 )
			{
				if ( pos > 1 )
				{
					pos--;
					char f = m_textBox.Text[pos];
					while ( pos > 0 && (m_strSeparateurs+".").IndexOf(f) < 0 )
					{
						pos--;
						f = m_textBox.Text[pos];
					}
				}
				if ( pos > 0 )
					pos++;
				string strText = m_textBox.Text;
				while ( strText.Length > pos && strText[pos] == ' ' )
					pos++;
				if ( m_textBox.SelectionStart-pos > 0 )
					strText = strText.Remove ( pos, m_textBox.SelectionStart-pos );
				if ( pos < strText.Length )
					m_textBox.Text = strText.Insert ( pos, mot.Mot );
				else
					m_textBox.Text = strText + mot.Mot;
				m_textBox.SelectionStart = pos+mot.Mot.Length;
			}
		}

		/// ///////////////////////////////////////////
		private CMotVocabulaire Vocabulaire
		{
			get
			{
				if (DesignMode)
					return null;
				if ( m_vocabulaire == null && m_fournisseur != null )
				{
					m_vocabulaire = new CMotVocabulaire();
					m_vocabulaire.StartFilling();
					foreach ( C2iExpression exp in CAllocateur2iExpression.ToutesExpressions )
					{
						if ( exp is C2iExpressionAnalysable )
						{
							CInfo2iExpression info = ((C2iExpressionAnalysable)exp).GetInfos();
							m_vocabulaire.AddMot ( new CMotVocabulaire ( info ) );
						}
					}
                    foreach (C2iExpression exp in CAnalyseurSyntaxiqueExpression.GetConstantesDynamiques())
                    {
                        if (exp is C2iExpressionAnalysable)
                        {
                            CInfo2iExpression info = ((C2iExpressionAnalysable)exp).GetInfos();
                            m_vocabulaire.AddMot(new CMotVocabulaire(info));
                        }
                    }
					FillWithFields( m_vocabulaire );
					m_vocabulaire.EndFilling();
					m_vocabulaire.IsInit = true;
				}
				return m_vocabulaire;
			}
		}

		/// ///////////////////////////////////////////
		private void FillWithFields ( CMotVocabulaire vocabulaire )
		{
			CDefinitionProprieteDynamique[] defs = null;
			if ( m_fournisseur != null && m_objetAnalyse != null )
			{
                if (vocabulaire.ProprieteAssociee == null)
                {
                    if (vocabulaire.InfoExpressionAssociee != null)
                        defs = m_fournisseur.GetDefinitionsChamps(vocabulaire.InfoExpressionAssociee.TypeDonnee.TypeDotNetNatif);
                    else
                        defs = m_fournisseur.GetDefinitionsChamps(m_objetAnalyse);
                }
                else
                    defs = m_fournisseur.GetDefinitionsChamps(vocabulaire.ProprieteAssociee.GetObjetPourAnalyseSousProprietes(), vocabulaire.ProprieteAssociee);
				vocabulaire.StartFilling();
				foreach ( CDefinitionProprieteDynamique def in defs )
				{
					CMotVocabulaire mot = new CMotVocabulaire ( def );
					vocabulaire.AddMot ( mot );
				}
				vocabulaire.EndFilling();
			}
			vocabulaire.IsInit = true;
		}


		/// ///////////////////////////////////////////
		private string m_strSeparateurs = " {}(),;-+*/%=\"\r\n\t<>";
		private void FillListBox( )
		{
			int nPosInText = m_textBox.SelectionStart;

			ListBox.Items.Clear();
			int nWidth = 100;
			string strWord = GetLastWord( m_strSeparateurs );
			if ( Vocabulaire == null )
				return;
			CMotVocabulaire vocab = Vocabulaire.GetVocabulaire ( strWord );
			ArrayList lstMots = new ArrayList();
			if ( vocab != null )
			{
				if ( !vocab.IsInit )
					FillWithFields ( vocab );
				//Trouve la fin du mot
				int nLastIndex = strWord.LastIndexOf('.');
				if ( nLastIndex > 0 )
					strWord = strWord.Substring ( nLastIndex+1 );
				lstMots = vocab.GetMots(strWord);
			}
			foreach ( CMotVocabulaire mot in lstMots )
			{
				string strVoc = mot.ToString();
				ListBox.Items.Add ( mot );
				nWidth = (int)Math.Min ( Math.Max ( nWidth, strVoc.Length * ListBox.Font.Size/1.5 ), 400 );
			}
			ListBox.Width = nWidth;

		}

		/// ///////////////////////////////////////////
		private void ShowListBox( int nPosInText )
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
			}
		}

		//-----------------------------------------------------
		private Point GetPositionListBox ( Point pt )
		{
			Rectangle rectBound = m_listBox.Parent.ClientRectangle;
			if ( pt.Y+m_listBox.Height > rectBound.Bottom )
			{
				pt.Y -= m_listBox.Height + m_textBox.Font.Height;
			}
			if ( pt.X+m_listBox.Width > rectBound.Right )
			{
				pt.X -= m_listBox.Width;
			}
			return pt;
		}



		#region Mise en évidence des couples ()[]{}

		private void ShowCouple ( int nPositionEnd, char caractereFermant )
        {
            if (nPositionEnd == 0)
                return;
            char caractereOuvrant = '(';
            switch (caractereFermant)
            {
                case ')':
                    caractereOuvrant = '(';
                    break;
                case '}':
                    caractereOuvrant = '{';
                    break;
                case ']':
                    caractereOuvrant = '[';
                    break;
            }

            int nNbOuvert = 0;

            //Cherche le caractère ouvrant correspondant
            string strText = m_textBox.Text;

            int nPositionStart = nPositionEnd;
            bool bIsInChaine = false;

            do
            {
                char car = strText[nPositionStart - 1];
                if (car == caractereOuvrant && !bIsInChaine)
                    nNbOuvert--;
                if (car == caractereFermant && !bIsInChaine)
                    nNbOuvert++;
                //if ("\"'".IndexOf(car) >= 0)
                if ("\"".IndexOf(car) >= 0)
                    bIsInChaine = !bIsInChaine;
                nPositionStart--;
            } while (nNbOuvert > 0 && nPositionStart > 0);


            if (nNbOuvert == 0)
                ShowStartMarker(nPositionStart, nPositionEnd, caractereOuvrant, caractereFermant);
        }

        private bool m_bAutoriseEventSelectionChanged = true;

		private void ShowStartMarker ( int nPositionStart, int nPositionEnd,
			char caractereOuvrant, char caractereFermant )
		{
            m_bAutoriseEventSelectionChanged = false;

			m_startMarker.Visible = false;
			Graphics g = CreateGraphics();
			Point point = m_textBox.GetPositionFromCharIndex(nPositionStart);
			point = PointToClient ( m_textBox.PointToScreen ( point ) );
			/*point.Y += (int) Math.Ceiling(m_textBox.Font.GetHeight()) + 2;
			point.X -= 10;*/
            int nOldSel = m_textBox.SelectionStart;
            int nOldLength = m_textBox.SelectionLength;
            m_textBox.SelectionStart = nPositionEnd;
            m_textBox.SelectionLength = 0;
			Font ft = m_textBox.SelectionFont;
			m_startMarker.Location = point;
			m_startMarker.ForeColor = Color.White;
			m_startMarker.Font = ft;
            m_textBox.SelectionStart = nOldSel;
            m_textBox.SelectionLength = nOldLength;
			
			string strCar = caractereOuvrant.ToString();
			SizeF sz = g.MeasureString (strCar, ft );
			
			
			m_startMarker.Width = (int)sz.Width-3;
			m_startMarker.Height = (int)sz.Height+3;
			m_startMarker.Visible = true;
			m_startMarker.Text = caractereOuvrant.ToString();
			g.Dispose();

            m_bAutoriseEventSelectionChanged = true;

		}
			
		
		
		private void m_startMarker_VisibleChanged(object sender, System.EventArgs e)
		{
			if ( m_startMarker.Visible )
				m_timerMarker.Start();
		}

		private void m_timerMarker_Tick(object sender, System.EventArgs e)
		{
			HideMarkers();
		}

		private void HideMarkers()
		{
			if ( m_timerMarker.Enabled )
			{
				m_timerMarker.Stop();
				m_startMarker.Visible = false;
			}
		}

		private void m_textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_formule = null;
			HideMarkers();
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
				}
				e.Handled = true;
			}

			if ( e.KeyCode == Keys.Up && ListBox.Visible )
			{
				if ( ListBox.SelectedIndex >= 0 )
				{
					ListBox.SelectedIndex--;
				}
				e.Handled = true;
			}

			if ( e.KeyCode == Keys.PageDown && ListBox.Visible )
			{
				if ( ListBox.SelectedIndex < ListBox.Items.Count-1 )
				{
					int nIndex = ListBox.SelectedIndex+10;
					ListBox.SelectedIndex = Math.Min ( nIndex, ListBox.Items.Count-1 );
				}
				e.Handled = true;
			}

			if ( e.KeyCode == Keys.PageUp && ListBox.Visible )
			{
				if ( ListBox.SelectedIndex < ListBox.Items.Count-1 )
				{
					int nIndex = ListBox.SelectedIndex-10;
					ListBox.SelectedIndex = Math.Max ( nIndex, 0 );
				}
				e.Handled = true;
			}

			if ( (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab ) && ListBox.Visible  )
			{
				InsereFromListBox();
				e.Handled = true;
				m_bCancelNextKeyPress= true;
			}
			
			if ( e.KeyCode == Keys.Escape && ListBox.Visible )
			{
				e.Handled = true;
				ListBox.Visible = false;
			}
		}

		#endregion


		private void m_textBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( ListBox.Visible )
				ListBox.Hide();
		}

		private void m_textBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( (e.KeyValue >=  '0' && e.KeyValue <= 'z') || 
				(ListBox.Visible && e.KeyCode != Keys.Up &&
					e.KeyCode != Keys.Down &&
					e.KeyCode != Keys.PageDown &&
					e.KeyCode != Keys.PageUp))
			{
				if ( e.KeyCode == Keys.OemPeriod )
				{
					ShowListBox(m_textBox.SelectionStart);
				}
				else if ( ListBox.Visible )
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

		private void m_ListBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab ) && ListBox.Visible  )
			{
				InsereFromListBox();
				e.Handled = true;
			}
			if ( e.KeyCode == Keys.Escape )
			{
				ListBox.Hide();
				e.Handled = true;
			}
		}

		private void CControleEditeFormule_BackColorChanged(object sender, System.EventArgs e)
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
		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return !m_bModeEdition;
			}
			set
			{
				m_bModeEdition = !value;
				m_textBox.ReadOnly = !m_bModeEdition;
                m_startMarker.ReadOnly = m_textBox.ReadOnly;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

		private void m_listBox_DoubleClick(object sender, EventArgs e)
		{
			InsereFromListBox();
		}

		private void CControleEditeFormule_FontChanged(object sender, System.EventArgs e)
		{
			m_textBox.Font = Font;
			m_startMarker.Font = Font;
		}


	}

	#region classe CMotVocabulaire

	public class CMotVocabulaire : IComparable
	{
		private bool m_bIsFilling = false;
		private string m_strMot="";
		private string m_strIndex="";
		private ArrayList m_listeMots=new ArrayList();
		private CInfo2iExpression m_infoExpression;
		private CDefinitionProprieteDynamique m_proprieteAssociee;
		private bool m_bIsInit = false;
		
		//Index vers mot
		private Hashtable m_tableMots = new Hashtable();

		public CMotVocabulaire()
		{
		}

		public CMotVocabulaire ( CInfo2iExpression infoExpression )
		{
			m_strMot = infoExpression.Texte;
			m_strIndex = infoExpression.Texte.ToUpper();
			m_infoExpression = infoExpression;
		}

		public bool IsInit
		{
			get
			{
				return m_bIsInit;
			}
			set
			{
				m_bIsInit = value;
			}
		}

		public CMotVocabulaire ( CDefinitionProprieteDynamique propriete )
		{
			CDefinitionMethodeDynamique defMethode = propriete as CDefinitionMethodeDynamique;
            CDefinitionFonctionDynamique defFonction = propriete as CDefinitionFonctionDynamique;
            if (defMethode != null || defFonction != null)
            {
                    m_strMot = propriete.Nom;
            }
            else
                m_strMot = "[" + propriete.Nom + "]";
			m_strIndex = propriete.Nom.ToUpper();;
			m_proprieteAssociee = propriete;
		}

		//-----------------------------------------------------
		public string Mot
		{
			get
			{
				return m_strMot;
			}
			set
			{
				m_strMot = value;
			}
		}
		
		//-----------------------------------------------------
		public string Index
		{
			get
			{
				return m_strIndex;
			}
			set
			{
				m_strIndex = value;
			}
		}

		//-----------------------------------------------------
		public void AddMot ( CMotVocabulaire mot )
		{
			if ( m_tableMots[mot.Index] != null )
				return;
			m_listeMots.Add ( mot );
			m_tableMots[mot.Index] = mot;
			m_tableMots["["+mot.Index+"]"] = mot;
			if ( !m_bIsFilling )
				m_listeMots.Sort();
		}

		#region Membres de IComparable

		public int CompareTo(object obj)
		{
			if ( !(obj is CMotVocabulaire ))
				return 0;
			return m_strMot.CompareTo ( ((CMotVocabulaire)obj).m_strMot );
		}

		#endregion

		//-----------------------------------------------------
		public ArrayList GetMots ( string strDebut )
		{
			ArrayList lst = new ArrayList();
			strDebut = strDebut.ToUpper();
			foreach ( CMotVocabulaire mot in m_listeMots )
			{
				string strMot = mot.Index;
				if ( strMot.Length >=  strDebut.Length )
				{
					if ( strMot.Substring ( 0, strDebut.Length ) == strDebut )
						lst.Add ( mot );
					else if ( "["+strMot.Substring(0, strDebut.Length-1) == strDebut )
						lst.Add ( mot );
				}

			}
			return lst;
		}

		//-----------------------------------------------------
		public CMotVocabulaire GetVocabulaire ( string strRacine )
		{
			if ( strRacine == "" )
				return this;
			if ( strRacine.IndexOf('.') >= 0 )
			{
				string strLeft = strRacine.Substring ( 0, strRacine.IndexOf('.') );
				string strRight = strRacine.Substring ( strRacine.IndexOf('.')+1 );
				CMotVocabulaire mot = (CMotVocabulaire)m_tableMots[strLeft.ToUpper()];
				if ( mot != null )
					return mot.GetVocabulaire ( strRight );
				return null;
			}
			return this;
		}

		//-----------------------------------------------------
		public void StartFilling()
		{
			m_bIsFilling = true;
		}

		//-----------------------------------------------------
		public void EndFilling()
		{
			m_bIsFilling = false;
			m_listeMots.Sort();
		}

		//-----------------------------------------------------
		public CDefinitionProprieteDynamique ProprieteAssociee
		{
			get
			{
				return m_proprieteAssociee;
			}
		}

		//-----------------------------------------------------
		public CInfo2iExpression InfoExpressionAssociee
		{
			get
			{
				return m_infoExpression;
			}
		}
		
		//-----------------------------------------------------
		public override string ToString()
		{
			return Mot;
		}


	}
	#endregion
}
