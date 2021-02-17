using System;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;

using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CCreateur2iFormulaire.
	/// </summary>
	public delegate void AfterModifyControlEventHandler ( Control control, C2iWnd wnd, object objetEdite );
	public class CCreateur2iFormulaire : IControlALockEdition
	{
		public const string c_champElementSource = "Source element";
        public const string c_IdChampElementSource = "0";

		private bool m_bControleValeursALaValidation = true;

		private Control m_controlParent = null;
		Hashtable m_tableValeurs = new Hashtable();
		//Map les variables sur les contrôles
		Hashtable m_tableVariableToControle = new Hashtable();

		//Map les variables sur les contrôles masqués (c'est un label avec xxx dedans);
		Hashtable m_tableVariableToControleMasque = new Hashtable();

		int m_nIdSession = -1;
		/// <summary>
		/// controle->Readonly
		/// </summary>
		Hashtable m_tableControleReadOnly = new Hashtable();
		
		//Map les CDefinitionProprieteDynamique sur les contrôles
		Hashtable m_tableProprieteToControle = new Hashtable();

		//Map les formules (C2iExpression) sur les contrôles
		Hashtable m_tableFormulesToControle = new Hashtable();

		//Map les C2iWnd sur les controles
		Hashtable m_table2iWndToControle = new Hashtable();


		private bool m_bReadOnly = false;		

		private bool m_bIsEditionLock = false;

		private object m_elementEdite = null;

		private System.Windows.Forms.Timer m_timer = null;

		private IFournisseurProprietesDynamiques m_fournisseurProprietes = new CFournisseurPropDynStd(false);


		private CRestrictionUtilisateurSurType m_restrictionAppliquee = null;

		public CCreateur2iFormulaire( object elementEdite, Control parent )
		{
			m_elementEdite = elementEdite;
			m_controlParent = parent;
			m_nIdSession = -1;
		}

		public CCreateur2iFormulaire( int nIdSession, object elementEdite, Control parent )
		{
			m_elementEdite = elementEdite;
			m_controlParent = parent;
			m_nIdSession = nIdSession;
		}

		/// /////////////////////////////////////////////////////
		public IFournisseurProprietesDynamiques FournisseurPropriete
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
			}
		}

		/// /////////////////////////////////////////////////////
		public bool ControlerLesValeursALaValidation
		{
			get
			{
				return m_bControleValeursALaValidation;
			}
			set
			{
				m_bControleValeursALaValidation = value;
			}
		}

		/// /////////////////////////////////////////////////////
		public void SetElementEditeSansChangerLesValeursAffichees(object elt)
		{
			m_elementEdite = elt;
		}

		/// /////////////////////////////////////////////////////
		public object ElementEdite 
		{
			get
			{
				return m_elementEdite;
			}
			set
			{
				m_elementEdite = value;
				m_restrictionAppliquee = null;
				CSessionClient session = CSessionClient.GetSessionForIdSession(m_nIdSession);
				if (session != null && ElementEdite != null)
				{
					IInfoUtilisateur user = session.GetInfoUtilisateur();
					if (user != null)
					{
						int? nIdVersion = null;
						IObjetAContexteDonnee objetAContexte = ElementEdite as IObjetAContexteDonnee;
						if (objetAContexte != null)
							nIdVersion = objetAContexte.ContexteDonnee.IdVersionDeTravail;

						m_restrictionAppliquee = user.GetRestrictionsSurObjet(ElementEdite, nIdVersion);
					}
				}

				//Crée la table d'association controle->variable;
				Hashtable tblCtrlToVariable = new Hashtable();
				foreach ( DictionaryEntry entry in m_tableVariableToControle )
					tblCtrlToVariable[entry.Value] = entry.Key;

				//Met éventuellement à jour les données de controles 
				foreach (DictionaryEntry entry in m_table2iWndToControle)
				{
					C2iWnd wnd = (C2iWnd)entry.Key;
					Control ctrl = (Control)entry.Value;
					if (AfterChangeElementEdite != null)
						AfterChangeElementEdite(ctrl, wnd, m_elementEdite);
					

					//Cherche la variable associée
					IVariableDynamique variable = (IVariableDynamique)tblCtrlToVariable[ctrl];
					if (variable != null)
					{
						if (ctrl is CComboBoxArbreObjetDonneesHierarchique ||
							ctrl.GetType() == typeof(C2iComboBox) ||
							ctrl.GetType() == typeof(CComboBoxListeObjetsDonnees))
							InitComboForVariable(variable, ctrl);
						if (ctrl is C2iTextBoxFiltreRapide)
							InitRechercheRapide(variable, (C2iTextBoxFiltreRapide)ctrl);
					}
				}
				InitChamps();
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public void InitChamps( )
		{
			UpdateVisibilite();
			if (m_timer != null)
				m_timer.Stop();
			if ( ElementEdite is IElementAVariables )
			{
				IElementAVariables eltAVar = (IElementAVariables)ElementEdite;
				foreach (IVariableDynamique variable in m_tableVariableToControle.Keys)
				{
					bool bMasquer = false;
					bool bReadOnly = false;
					if (variable is CChampCustom && m_restrictionAppliquee != null)
					{
						CChampCustom champ = (CChampCustom)variable;
						ERestriction rest = m_restrictionAppliquee.GetRestriction(champ.CleRestriction);
						bReadOnly = (rest & ERestriction.ReadOnly) == ERestriction.ReadOnly;
						bMasquer = (rest & ERestriction.Hide) == ERestriction.Hide;
					}
					Control ctrl = (Control)m_tableVariableToControle[variable];

					Control ctrlMasque = (Control)m_tableVariableToControleMasque[variable];

					if (bMasquer)
					{
						if (ctrlMasque != null)
							ctrlMasque.Visible = true;
						if (ctrl != null)
						{
							ctrl.Visible = false;
							m_tableControleReadOnly[ctrl] = true;
						}
					}
					else
					{
						object valeur = eltAVar.GetValeurChamp(variable);

						if (ctrl != null)
						{
							bool bLock = m_bIsEditionLock;
							if (bReadOnly)
							{
								bLock = true;
								m_tableControleReadOnly[ctrl] = true;
							}
							else
								m_tableControleReadOnly[ctrl] = false;

							if (ctrl is IControlALockEdition)
								((IControlALockEdition)ctrl).LockEdition = bLock;
							else
								ctrl.Enabled = !bLock;


							if (ctrl is CheckBox)
								((CheckBox)ctrl).Checked = valeur == null ? false : (bool)valeur;
							else if (ctrl is C2iDateTimeExPicker)
								((C2iDateTimeExPicker)ctrl).Value = valeur == null ? null : new CDateTimeEx((DateTime)valeur);
							else if (ctrl is C2iTextBoxNumerique)
							{
								if (valeur == null)
									((C2iTextBoxNumerique)ctrl).DoubleValue = null;
								else
								{
									if (variable.TypeDonnee.TypeDotNetNatif == typeof(int))
										((C2iTextBoxNumerique)ctrl).IntValue = valeur == null ? 0 : (int)valeur;
									else
										((C2iTextBoxNumerique)ctrl).DoubleValue = valeur == null ? 0 : (double)valeur;
								}
							}
							else if (ctrl is C2iTextBox)
								ctrl.Text = valeur == null ? "" : valeur.ToString();
							else if (ctrl is C2iComboBox)
							{
								if (valeur != null)
									((C2iComboBox)ctrl).SelectedValue = valeur;
								object obj = ((C2iComboBox)ctrl).SelectedValue;
							}
							else if (ctrl is ISelectionneurElementListeObjetsDonnees)
							{
								if (variable is CVariableDynamiqueSelectionObjetDonnee && valeur is int)
								{
									CVariableDynamiqueSelectionObjetDonnee varSel = (CVariableDynamiqueSelectionObjetDonnee)variable;
									if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(varSel.FiltreSelection.TypeElements))
									{
										CContexteDonnee contexte = CSc2iWin32DataClient.ContexteCourant;
										CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(varSel.FiltreSelection.TypeElements, new object[] { contexte });
										if (objet.ReadIfExists((int)valeur))
											valeur = objet;
									}
								}
								if (valeur is CObjetDonnee || valeur == null)
									((ISelectionneurElementListeObjetsDonnees)ctrl).ElementSelectionne = (CObjetDonnee)valeur;

							}
						}
					}
				}
			}
			foreach ( CDefinitionProprieteDynamique def in m_tableProprieteToControle.Keys )
			{
				Control ctrl = (Control)m_tableProprieteToControle[def];
				//if ( ctrl.Visible )
				{
					Object obj = CInterpreteurProprieteDynamique.GetValue(ElementEdite, def).Data;
					string strFormat = "";
					if ( ctrl.Tag is string )
						strFormat = (string)ctrl.Tag;
					if ( obj != null )
						ctrl.Text = obj.ToString();
					else
						ctrl.Text = "";
					if ( obj != null && strFormat.Trim() != "")
					{
						//Cherche la méthode ToString avec une chaine en paramètre
						MethodInfo methode = obj.GetType().GetMethod("ToString", new Type[]{typeof(string)});
						if ( methode != null )
						{
							try
							{
								ctrl.Text = (string)methode.Invoke ( obj, new object[]{strFormat} );
							}
							catch 
							{
							
							}
						}
					}
				}
			}

			UpdateValeursFormules();

			
			foreach (Control ctrl in m_table2iWndToControle.Values)
			{
				if (ctrl is CControlListeForFormulaire)
					((CControlListeForFormulaire)ctrl).ObjetSource = ElementEdite;
				if (ctrl is CControleImageForFormulaire)
					((CControleImageForFormulaire)ctrl).ObjetSource = ElementEdite;
			}
			UpdateVisibilite();
			if (m_timer != null)
				m_timer.Start();
		}

		private void UpdateValeursFormules()
		{
			foreach (C2iExpression expression in m_tableFormulesToControle.Keys)
			{
				Control ctrl = (Control)m_tableFormulesToControle[expression];
				//if ( ctrl.Visible )
				{
					CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(ElementEdite);
					if (ElementEdite is CObjetDonnee)
						ctx.AttacheObjet(typeof(CContexteDonnee), ((CObjetDonnee)ElementEdite).ContexteDonnee);
					else
						ctx.AttacheObjet(typeof(CContexteDonnee), CSc2iWin32DataClient.ContexteCourant);
					CResultAErreur result = expression.Eval(ctx);
					if (result)
					{
						object obj = result.Data;
						string strFormat = "";
						if (ctrl.Tag is string)
							strFormat = (string)ctrl.Tag;
						if (obj != null)
							ctrl.Text = obj.ToString();
						else
							ctrl.Text = "";
						if (obj != null && strFormat.Trim() != "")
						{
							//Cherche la méthode ToString avec une chaine en paramètre
							MethodInfo methode = obj.GetType().GetMethod("ToString", new Type[] { typeof(string) });
							if (methode != null)
							{
								try
								{
									ctrl.Text = (string)methode.Invoke(obj, new object[] { strFormat });
								}
								catch
								{

								}
							}
						}
					}
				}
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public static event AfterModifyControlEventHandler AfterCreateControl;
		public static event AfterModifyControlEventHandler AfterChangeElementEdite;
		//////////////////////////////////////////////////////////////////////////////////////
		public CResultAErreur CreateChilds ( Control controlParent, C2iWnd wndParent, ToolTip tooltip )
		{
			
			C2iWndFenetre fenetre = wndParent as C2iWndFenetre;
			if (fenetre != null )
			{
				if (m_timer != null)
				{
					m_timer.Stop();
					m_timer.Dispose();
					m_timer = null;
				}
				if (fenetre.RefreshMinutes > 0)
				{
					m_timer = new Timer();
					m_timer.Interval = (int)Math.Max(fenetre.RefreshMinutes * 60 * 1000, 3000);
					m_timer.Tick += new EventHandler(m_timerRefresh_Tick);
				}
			}
			foreach ( C2iWnd wnd in wndParent.Childs )
			{
				Control newControl = null;
				if ( wnd.GetType() == typeof(C2iWndLabel) )
					newControl = CreateLabel ( (C2iWndLabel) wnd, controlParent );
				if ( wnd.GetType() == typeof ( C2iWndImage ) )
					newControl = CreateImage ( (C2iWndImage ) wnd, controlParent );
				else if ( wnd.GetType() == typeof(C2iWndProprieteDynamique) )
					newControl = CreateLabelPropriete ((C2iWndProprieteDynamique) wnd, controlParent );
				else if ( wnd.GetType() == typeof(C2iWndFormule) )
					newControl = CreateLabelExpression ((C2iWndFormule) wnd, controlParent );
				else if ( wnd.GetType() ==typeof(sc2i.formulaire.C2iWndPanel) )
					newControl = CreatePanel ( (sc2i.formulaire.C2iWndPanel) wnd, controlParent );
				else if ( wnd.GetType() ==typeof(sc2i.formulaire.C2iWndLink) )
					newControl = CreateLink ( (sc2i.formulaire.C2iWndLink) wnd, controlParent );
				else if ( wnd.GetType().IsSubclassOf(typeof(C2iWndVariable) ))
					newControl = CreateWndVariable ((C2iWndVariable) wnd, controlParent, tooltip );
				else if (wnd.GetType() == typeof(C2iWndListe))
					newControl = CreateListe((C2iWndListe)wnd, controlParent);
				if ( newControl != null )
				{
					m_table2iWndToControle[wnd] = newControl;
					newControl.TabIndex = Math.Max(wnd.TabOrder,0);
					AnchorStyles anchorStyle = AnchorStyles.None;
					Point location = newControl.Location;
					Size sz = newControl.Size;
					if (wnd.AnchorBottom)
					{
						anchorStyle |= AnchorStyles.Bottom;
						if ( !wnd.AnchorTop )
							location = new Point ( location.X, controlParent.ClientSize.Height - ( wndParent.Size.Height - location.Y ));
					}
					if (wnd.AnchorRight)
						anchorStyle |= AnchorStyles.Right;
					{
						if ( !wnd.AnchorLeft )
							location = new Point ( controlParent.ClientSize.Width - (wndParent.Size.Width - location.X), location.Y );
					}
					if (wnd.AnchorLeft)
					{
						anchorStyle |= AnchorStyles.Left;
						if ( wnd.AnchorRight )
							sz = new Size ( (int)((double)controlParent.Width/(double)wndParent.Size.Width*(double)wnd.Size.Width), sz.Height );
					}
					if (wnd.AnchorTop)
					{
						anchorStyle |= AnchorStyles.Top;
						if ( wnd.AnchorBottom )
							sz = new Size (sz.Width, (int)((double)controlParent.Height/(double)wndParent.Size.Height*(double)wnd.Size.Height) );
					}
					newControl.Anchor = anchorStyle;
					newControl.Location = location;
					newControl.Size = sz;
					CreateChilds ( newControl, wnd, tooltip );
					newControl.BringToFront();
					if ( AfterCreateControl != null )
						AfterCreateControl ( newControl, wnd, ElementEdite );
				}
			}
			return CResultAErreur.True;
		}

		//------------------------------------------------------------------
		void m_timerRefresh_Tick(object sender, EventArgs e)
		{
			m_timer.Stop();
			UpdateValeursFormules();
			try
			{
				m_timer.Start();
			}
			catch { }
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private


		
		void UpdateVisibilite()
		{
			if ( m_fournisseurProprietes == null )
				m_fournisseurProprietes = new CFournisseurPropDynStd(true);
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( ElementEdite);
			foreach ( C2iWnd wnd in m_table2iWndToControle.Keys )
			{
				if ( wnd.Visiblity != null )
				{
					Control ctrl = (Control)m_table2iWndToControle[wnd];
					CResultAErreur result = wnd.Visiblity.Eval ( ctx );
					if ( !result )
						ctrl.Visible = true;
					else
					{
						if ( result.Data.ToString() =="0" || result.Data.ToString().ToUpper()=="FALSE" )
							ctrl.Visible = false;
						else
							ctrl.Visible = true;
					}
				}
				if ( wnd.Enabled != null && !LockEdition)
				{
					Control ctrl = (Control)m_table2iWndToControle[wnd];
					CResultAErreur result = wnd.Enabled.Eval(ctx);
					bool bEnable = true;
					if (result)
					{
						if (result.Data.ToString() == "0" || result.Data.ToString().ToUpper() == "FALSE")
						{
							bEnable = false;
							m_tableControleReadOnly[ctrl] = !bEnable;
						}
						else
							bEnable = true;
					}
					if ( ctrl is IControlALockEdition )
						((IControlALockEdition)ctrl).LockEdition = !bEnable;
					else
						ctrl.Enabled = bEnable;
				}
					
			}
		}

						


		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateLabel( C2iWndLabel label2i, Control parent )
		{
			Label label = new Label();
			label.Left = label2i.Position.X;
			label.Top = label2i.Position.Y;
			label.Width = label2i.Size.Width;
			label.Height = label2i.Size.Height;
			switch (label2i.BorderStyle )
			{
				case C2iWndLabel.LabelBorderStyle._3D :
					label.BorderStyle = BorderStyle.Fixed3D;
					break;
				case C2iWndLabel.LabelBorderStyle.Aucun :
					label.BorderStyle = BorderStyle.None;
					break;
				case C2iWndLabel.LabelBorderStyle.Plein :
					label.BorderStyle = BorderStyle.FixedSingle;
					break;
			}
			label.BackColor = label2i.BackColor;
			label.ForeColor = label2i.ForeColor;
			label.Font = label2i.Font;
			label.Parent = parent;
			label.Text = label2i.Text;
			label.TextAlign = label2i.TextAlign;
			label.CreateControl();
			return label;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateListe(C2iWndListe wndListe, Control parent)
		{
			CControlListeForFormulaire ctrl = CControlListeForFormulaire.CreateFromWndListe(wndListe, parent, m_fournisseurProprietes);
			return ctrl;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateImage( C2iWndImage wndImage, Control parent )
		{
			CControleImageForFormulaire viewer = CControleImageForFormulaire.CreateFromWndImage(wndImage, parent, m_fournisseurProprietes);
			return viewer;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateLink ( C2iWndLink label2i, Control parent )
		{
			LinkLabel label = new LinkLabel();
			label.Left = label2i.Position.X;
			label.Top = label2i.Position.Y;
			label.Width = label2i.Size.Width;
			label.Height = label2i.Size.Height;
			switch (label2i.BorderStyle )
			{
				case C2iWndLabel.LabelBorderStyle._3D :
					label.BorderStyle = BorderStyle.Fixed3D;
					break;
				case C2iWndLabel.LabelBorderStyle.Aucun :
					label.BorderStyle = BorderStyle.None;
					break;
				case C2iWndLabel.LabelBorderStyle.Plein :
					label.BorderStyle = BorderStyle.FixedSingle;
					break;
			}
			label.BackColor = label2i.BackColor;
			label.ForeColor = label2i.ForeColor;
			label.LinkColor = label2i.ForeColor;
			label.Font = label2i.Font;
			label.Parent = parent;
			label.Text = label2i.Text;
			label.TextAlign = label2i.TextAlign;
			label.CreateControl();
			return label;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateLabelPropriete ( C2iWndProprieteDynamique wndPropriete, Control parent )
		{
			Control ctrl = CreateLabel ( wndPropriete, parent );
			if ( wndPropriete.Propriete != null )
				m_tableProprieteToControle[wndPropriete.Propriete] = ctrl;
			//Stocke le format dans le tag du controle
			ctrl.Tag = wndPropriete.FormatAffichage;
			return ctrl;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreateLabelExpression ( C2iWndFormule wndExpression, Control parent )
		{
			Control ctrl = CreateLabel ( wndExpression, parent );
			if ( wndExpression.Formule != null )
				m_tableFormulesToControle[wndExpression.Formule] = ctrl;
			//Stocke le format dans le tag du controle
			ctrl.Tag = wndExpression.DisplayFormat;
			return ctrl;
		}


		//////////////////////////////////////////////////////////////////////////////////////
		public Control CreatePanel( sc2i.formulaire.C2iWndPanel panel2i, Control parent )
		{
			Panel panel;
			if ( panel2i.Ombre )
				panel = new C2iPanelOmbre();
			else
				panel = new Panel();
			panel.Left = panel2i.Position.X;
			panel.Top = panel2i.Position.Y;
			panel.Width = panel2i.Size.Width;
			panel.Height = panel2i.Size.Height;
			switch (panel2i.BorderStyle )
			{
				case C2iWndPanel.PanelBorderStyle._3D :
					panel.BorderStyle = BorderStyle.Fixed3D;
					break;
				case C2iWndPanel.PanelBorderStyle.Aucun :
					panel.BorderStyle = BorderStyle.None;
					break;
				case C2iWndPanel.PanelBorderStyle.Plein :
					panel.BorderStyle = BorderStyle.FixedSingle;
					break;
			}
			panel.BackColor = panel2i.BackColor;
			panel.ForeColor = panel2i.ForeColor;
			panel.Font = panel2i.Font;
			panel.Parent = parent;
			panel.CreateControl();
			return panel;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private Control CreateWndVariable ( C2iWndVariable wndCust, Control parent, ToolTip tooltip )
		{
			if ( !(ElementEdite is IElementAVariables ))
				return null;
			IVariableDynamique variable = wndCust.Variable;
			if ( variable == null )
				//Pas de variable, pas de controle !!
				return null;
			Control ctrl = null;
			object valeur = ((IElementAVariables)ElementEdite).GetValeurChamp ( variable );
			bool bReadOnly = false;
			bool bMasquer = false;
			if ( variable is CChampCustom && m_restrictionAppliquee != null)
			{
				CChampCustom champ = (CChampCustom)variable;
				ERestriction rest = m_restrictionAppliquee.GetRestriction(champ.CleRestriction);
				bReadOnly = rest == ERestriction.ReadOnly;
				bMasquer = rest == ERestriction.Hide;
			}


			Label lblMasque = new Label();
			lblMasque.Text = "*****";
			lblMasque.TextAlign = ContentAlignment.MiddleCenter;
			lblMasque.BorderStyle = BorderStyle.Fixed3D;
			
			
			if ( variable.IsChoixParmis() )
			{
				if ( (variable is CVariableDynamiqueSelectionObjetDonnee && 
					((CVariableDynamiqueSelectionObjetDonnee)variable).UtiliserRechercheRapide ) ||
					(variable is CChampCustom && 
					typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif) && 
					wndCust is C2iWndChampCustom &&
					!((C2iWndChampCustom)wndCust).UtiliserUneCombo )
					)
					ctrl = InitRechercheRapide ( variable, null );
				else
					ctrl = InitComboForVariable ( variable, null );
			}
			else
			{
				HorizontalAlignment hAlignement = HorizontalAlignment.Left;
				ContentAlignment cAlignement = ContentAlignment.TopLeft;
				
				switch ( wndCust.Alignement )
				{
					case C2iWndVariable.TypeAlignement.Centre :
						hAlignement = HorizontalAlignment.Center;
						cAlignement = ContentAlignment.TopCenter;
						break;
					case C2iWndVariable.TypeAlignement.Droite :
						hAlignement = HorizontalAlignment.Right;
						cAlignement = ContentAlignment.TopRight;
						break;
					case C2iWndVariable.TypeAlignement.Gauche :
						hAlignement = HorizontalAlignment.Left;
						cAlignement = ContentAlignment.TopLeft;
						break;
				}
				if ( variable.TypeDonnee.TypeDotNetNatif == typeof(bool) )
				{
					CheckBox box = new CheckBox();
					try
					{
						box.Checked = valeur == null?false:(bool)valeur;
					}
					catch{}
					box.Text = variable.Nom;
					ctrl = box;
					box.Enabled = !m_bIsEditionLock;
					box.TextAlign = cAlignement;
				}
				else if ( variable.TypeDonnee.TypeDotNetNatif == typeof(DateTime) )
				{
					C2iDateTimeExPicker picker = new C2iDateTimeExPicker();
					try
					{
						if ( valeur is CDateTimeEx )
							picker.Value = new CDateTimeEx(((CDateTimeEx)valeur).DateTimeValue);
						else
							picker.Value = valeur == null?null:new CDateTimeEx((DateTime)valeur);
					}
					catch{}
                    picker.Format = DateTimePickerFormat.Custom;
                    picker.CustomFormat = CUtilDate.gFormat;// "g";// "dd/MM/yyyy   HH:mm";
					ctrl = picker;
				}
				else if (variable.TypeDonnee.TypeDotNetNatif == typeof(double) )
				{
					C2iTextBoxNumerique numup = new C2iTextBoxNumerique();
					numup.NullAutorise = true;
					if ( variable is CChampCustom )
					{
						numup.Arrondi = ((CChampCustom)variable).Precision;
					}
					else
						numup.Arrondi = 10;
					try
					{
						if ( valeur == null )
							numup.DoubleValue = null;
						else
							numup.DoubleValue = (double)valeur;
					}
					catch{}
					numup.TextAlign = hAlignement;
					ctrl = numup;
				}
				else if ( variable.TypeDonnee.TypeDotNetNatif == typeof(int))
				{
					C2iTextBoxNumerique numupInt = new C2iTextBoxNumerique();
					numupInt.NullAutorise = true;
					numupInt.Arrondi = 0;
					numupInt.DecimalAutorise = false;
					try
					{
						if ( valeur == null )
							numupInt.DoubleValue = null;
						else
							numupInt.IntValue = (int)valeur;;
					}
					catch{}
					numupInt.TextAlign = hAlignement;
					ctrl = numupInt;
				}
				else if( variable.TypeDonnee.TypeDotNetNatif == typeof(string) )
				{
					C2iTextBox textBox = new C2iTextBox();
					if (variable is CChampCustom)
					{
						int nPrec = ((CChampCustom)variable).Precision;
						if (nPrec > 1)
							textBox.MaxLength = nPrec;
					}
					textBox.Text = valeur==null?"":valeur.ToString();
					if ( wndCust.EditMask.Trim()!="")
					{
						CFormatteurTextBoxMasque formatteur = new CFormatteurTextBoxMasque(wndCust.EditMask);
						formatteur.AttachTo ( textBox );
					}
					if ( wndCust.Size.Height > 10 )
						textBox.Multiline = true;
					textBox.TextAlign = hAlignement;
					textBox.Multiline = wndCust.MultiLine;

					ctrl = textBox;
				}
			}
			if ( ctrl != null )
			{
				ctrl.Left = wndCust.Position.X;
				ctrl.Top = wndCust.Position.Y;
				ctrl.Width = wndCust.Size.Width;
				ctrl.Height = wndCust.Size.Height;
				ctrl.Font = wndCust.Font;

				lblMasque.Left = wndCust.Position.X;
				lblMasque.Top = wndCust.Position.Y;
				lblMasque.Width = wndCust.Size.Width;
				lblMasque.Height = wndCust.Size.Height;
				lblMasque.Font = wndCust.Font;
				lblMasque.Visible = false;

				parent.Controls.AddRange(new Control[] { ctrl, lblMasque });				

				m_tableVariableToControle[variable] = ctrl;
				m_tableVariableToControleMasque[variable] = lblMasque;
				m_tableControleReadOnly[ctrl] = bReadOnly;
				if (tooltip !=  null )
					tooltip.SetToolTip(ctrl, variable.Description);
				if (ctrl is C2iComboBox)
					((C2iComboBox)ctrl).SelectedValue = valeur;
				else if ( ctrl is ComboBox && valeur != null)
					((ComboBox)ctrl).SelectedValue = valeur;
			}
			return ctrl;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private CFiltreData GetFiltre(CFiltreDynamique filtre)
		{
			if (filtre == null)
				return null;
			IVariableDynamique variable = AssureVariableElementCible(filtre, m_elementEdite.GetType());
			filtre.SetValeurChamp(variable.IdVariable, ElementEdite);
			CResultAErreur result = filtre.GetFiltreData();
			if (result)
				return (CFiltreData)result.Data;
			return null;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private Control InitRechercheRapide ( IVariableDynamique variable, C2iTextBoxFiltreRapide ctrl )
		{
			if ( !(ElementEdite is IElementAVariables) )
				return null;
			Type typeElements = null;
			string strPropAffichee = "";
			CFiltreData filtre = null;
			if (variable is CVariableDynamiqueSelectionObjetDonnee)
			{
				C2iExpression expression = ((CVariableDynamiqueSelectionObjetDonnee)variable).ExpressionAffichee;
				if (expression is C2iExpressionChamp)
					strPropAffichee = ((C2iExpressionChamp)expression).DefinitionPropriete.NomProprieteSansCleTypeChamp;
				CFiltreDynamique filtreDyn = ((CVariableDynamiqueSelectionObjetDonnee)variable).FiltreSelection;
				filtre = GetFiltre(filtreDyn);
				typeElements = filtreDyn.TypeElements;
			}
			else if (variable is CChampCustom)
			{
				CFiltreDynamique filtreDyn = ((CChampCustom)variable).FiltreObjetDonnee;
				if (filtreDyn != null)
				{
					filtre = GetFiltre(filtreDyn);
				}
				typeElements = ((CChampCustom)variable).TypeObjetDonnee;
			}

			if (ctrl == null)
			{
				ctrl = new C2iTextBoxFiltreRapide();
			}
			if (strPropAffichee == "")
			{
				strPropAffichee = DescriptionFieldAttribute.GetDescriptionField(typeElements, "DescriptionElement");
			}
			ctrl.InitAvecFiltreDeBase ( typeElements,
				strPropAffichee,
				filtre, true );
			object selVal = ((IElementAVariables)ElementEdite).GetValeurChamp(variable);
			try
			{
				if ( selVal != null )
					ctrl.ElementSelectionne = (CObjetDonnee)selVal;
			}
			catch{}
			return ctrl;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		private Control InitComboForVariable ( IVariableDynamique variable, Control ctrl )
		{
			if ( !(ElementEdite is IElementAVariables) )
				return null;
			Type typeElement = variable.TypeDonnee.TypeDotNetNatif;
			if (variable is CVariableDynamiqueSelectionObjetDonnee)
				typeElement = ((CVariableDynamiqueSelectionObjetDonnee)variable).FiltreSelection.TypeElements;
			if (typeof(IObjetDonneeAutoReference).IsAssignableFrom(typeElement) &&
				typeof(IObjetHierarchiqueACodeHierarchique).IsAssignableFrom ( typeElement ) )
			{
				try
				{
					if (!(ctrl is CComboBoxArbreObjetDonneesHierarchique) && ctrl != null)
					{
						ctrl.Visible = false;
						ctrl.Parent.Controls.Remove(ctrl);
						ctrl.Dispose();
						ctrl = null;
					}
						
					CComboBoxArbreObjetDonneesHierarchique arbre;
					if ( ctrl is CComboBoxArbreObjetDonneesHierarchique )
						arbre = (CComboBoxArbreObjetDonneesHierarchique)ctrl;
					else
						arbre = new CComboBoxArbreObjetDonneesHierarchique();

					using (CContexteDonnee contexte = new CContexteDonnee(1, false, false))
					{
						CFiltreData filtre = null;
						Type tp = variable.TypeDonnee.TypeDotNetNatif;
						if ( variable is CVariableDynamiqueSelectionObjetDonnee ) 
						{
							CVariableDynamiqueSelectionObjetDonnee varSel = (CVariableDynamiqueSelectionObjetDonnee)variable;
							tp = ((CVariableDynamiqueSelectionObjetDonnee )variable).FiltreSelection.TypeElements;
							CFiltreDynamique filtreDyn = varSel.FiltreSelection;
							filtre = GetFiltre(filtreDyn);
						}
						if ( variable is CChampCustom )
						{
							CFiltreDynamique filtreDyn = ((CChampCustom)variable).FiltreObjetDonnee;
							filtre = GetFiltre(filtreDyn);
						}
							
						CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(tp, new object[] { contexte });
						string strChamp = "DescriptionElement";
						if (objet.GetType().GetProperty("Libelle") != null)
							strChamp = "Libelle";
						arbre.Init(tp,
							((IObjetDonneeAutoReference)objet).ProprieteListeFils,
							((IObjetDonneeAutoReference)objet).ChampParent,
							strChamp,
							filtre,
							null
							);
						ctrl = arbre;
						return arbre;
					}
				}
				catch { }
			}
			IList lst = null;
			lst = variable.Valeurs;
			if (variable is CChampCustom && ((CChampCustom)variable).TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto &&
				lst is CListeObjetsDonnees )
			{
				CFiltreData filtre = ((CListeObjetsDonnees)lst).Filtre;
				CFiltreDynamique filtreDyn = ((CChampCustom)variable).FiltreObjetDonnee;
				if (filtreDyn != null)
				{
					filtre = GetFiltre(filtreDyn);
				}

				((CListeObjetsDonnees)lst).Filtre = filtre;
			}
			else
				lst = variable.Valeurs;
			C2iComboBox combo = null;
			if (lst is CListeObjetsDonnees && ((CListeObjetsDonnees)lst).TypeObjets != typeof(CValeurChampCustom))
			{
				CComboBoxListeObjetsDonnees cmbListe = null;
				if (ctrl is CComboBoxListeObjetsDonnees)
					cmbListe = (CComboBoxListeObjetsDonnees)ctrl;
				else
				{
					if (ctrl != null)
					{
						ctrl.Visible = false;
						ctrl.Parent.Controls.Remove(ctrl);
						ctrl.Dispose();
						ctrl = null;
					}
					cmbListe = new CComboBoxListeObjetsDonnees();
				}
				cmbListe.NullAutorise = true;
				cmbListe.Init ( (CListeObjetsDonnees)lst, 
					DescriptionFieldAttribute.GetDescriptionField(variable.TypeDonnee.TypeDotNetNatif, "DescriptionElement"),
					true );
				combo = cmbListe;
			}
			else
			{
				if (ctrl == null || ctrl.GetType() != typeof(C2iComboBox))
				{
					if (ctrl != null)
					{
						ctrl.Visible = false;
						ctrl.Parent.Controls.Remove(ctrl);
						ctrl.Dispose();
						ctrl = null;
					}
					//combo = new C2iComboBox();
				}
				combo = new C2iComboBox();
				combo.DisplayMember = "Display";
				combo.ValueMember = "Value";
				combo.DataSource = lst;
			}
			combo.DropDownStyle = ComboBoxStyle.DropDownList;
			combo.Sorted = false;
			combo.IsLink = false;
			if ( ctrl == null )
				combo.CreateControl();
			object selVal = ((IElementAVariables)ElementEdite).GetValeurChamp(variable);
			combo.SelectedValue = selVal;
			
			return combo;
		}

		//////////////////////////////////////////////////////////////////////////////////////
		//Indique que le controle est en mode reaonly, ce qui
		//surcharge le lock edition
		public bool ReadOnly
		{
			get
			{
				return m_bReadOnly;
			}
			set
			{
				m_bReadOnly = value;
				if (value)
					LockEdition = true;
			}
		}


		public event EventHandler OnChangeLockEdition; 
		//////////////////////////////////////////////////////////////////////////////////////
		public bool LockEdition
		{
			get
			{
				return m_bIsEditionLock;
			}
			set
			{
				if (ReadOnly)
					m_bIsEditionLock = true;
				else
					m_bIsEditionLock = value;
				foreach ( Control ctrl in m_tableVariableToControle.Values )
				{
					bool bLock = m_bIsEditionLock;
					if ( m_tableControleReadOnly[ctrl] is bool && (bool)m_tableControleReadOnly[ctrl] )
						bLock = true;
					if ( ctrl is IControlALockEdition )
						((IControlALockEdition)ctrl).LockEdition = bLock;
					else
						ctrl.Enabled = !bLock;
					if (ctrl is LinkLabel)
						ctrl.Enabled = bLock;
				}
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_bIsEditionLock )
				return result;
			if ( ElementEdite is IElementAVariables )
			{
				IElementAVariables eltAVar = (IElementAVariables)ElementEdite;
				foreach ( IVariableDynamique champ in m_tableVariableToControle.Keys )
				{
					Control ctrl = (Control)m_tableVariableToControle[champ];
					object valeurChamp = null;
					if ( ctrl != null )
					{
						if ( ctrl is CheckBox )
							valeurChamp = ((CheckBox)ctrl).Checked?true:false;
						else if (ctrl is C2iDateTimeExPicker )
						{
							object valeur = ((C2iDateTimeExPicker)ctrl).Value;
							if (valeur is CDateTimeEx )
								valeur = ((CDateTimeEx)valeur).DateTimeValue;
							valeurChamp = valeur;
						}
						else if (ctrl is C2iTextBoxNumerique )
						{
							if ( champ.TypeDonnee.TypeDotNetNatif == typeof(int) )
							{
								if ( !((C2iTextBoxNumerique)ctrl).IsSet() )
									valeurChamp = null;
								else
									valeurChamp = ((C2iTextBoxNumerique)ctrl).IntValue;
							}
							else 
							{
								if ( !((C2iTextBoxNumerique)ctrl).IsSet() )
									valeurChamp = null;
								else
									valeurChamp = ((C2iTextBoxNumerique)ctrl).DoubleValue;
							}
						}
						else if (ctrl is C2iTextBox )
							valeurChamp = ctrl.Text;
						else if ( ctrl is C2iComboBox )
							valeurChamp = ((C2iComboBox)ctrl).SelectedValue;
						else if (ctrl is ISelectionneurElementListeObjetsDonnees)
						{
							valeurChamp = null;
							object obj = ((ISelectionneurElementListeObjetsDonnees)ctrl).ElementSelectionne;
							if ( champ is CVariableDynamiqueSelectionObjetDonnee )
							{
								CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( obj);
								CResultAErreur resTmp = ((CVariableDynamiqueSelectionObjetDonnee)champ).ExpressionRetournee.Eval ( ctx );
								if ( resTmp )
									valeurChamp = resTmp.Data;
							}
							if (champ is CChampCustom)
								valeurChamp = obj;
						}
					}
					eltAVar.SetValeurChamp(champ, valeurChamp);

					if (ControlerLesValeursALaValidation)
					{
						result += champ.VerifieValeur(valeurChamp);
					}
				}
				
			}
			return result;
		}

		//////////////////////////////////////////////////////////////////
		/// <summary>
		/// Crée dans une filtre dynamique la variable qui est utilisée par le créateur
		/// de formulaire pour donner au filtre des propriétés de l'élément édité
		/// </summary>
		/// <param name="filtre"></param>
		/// <param name="typeElement"></param>
		/// <returns></returns>
		public static IVariableDynamique AssureVariableElementCible(CFiltreDynamique filtre, Type typeElement)
		{
			IVariableDynamique variableASupprimer = null;
			foreach (IVariableDynamique variable in filtre.ListeVariables)
			{
				if (variable.Nom == c_champElementSource )
				{
					if (  variable.TypeDonnee.TypeDotNetNatif != typeElement )
						variableASupprimer = variable;
					else
						return variable;
				}
			}
			if ( variableASupprimer !=null )
				filtre.RemoveVariable( variableASupprimer );
			CVariableDynamiqueSysteme newVariable = new CVariableDynamiqueSysteme(filtre);
			newVariable.Nom = c_champElementSource;
            newVariable.IdVariable = c_IdChampElementSource;
			newVariable.SetTypeDonnee(new sc2i.expression.CTypeResultatExpression(typeElement, false));
            filtre.AddVariable(newVariable);
			return newVariable;
		}
	}
}
