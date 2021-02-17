using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.common;
using sc2i.drawing;
using sc2i.win32.common;
using sc2i.process;

namespace sc2i.win32.process
{
	
	public class CControlEditionProcess : sc2i.win32.common.CPanelEditionObjetGraphique
	{

		#region Code généré par le concepteur

		private System.ComponentModel.IContainer components = null;


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
				if ( m_customCursor != null && m_customCursor!= Cursors.Arrow)
					m_customCursor.Dispose();
				m_customCursor = null;
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique2 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
			sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique2 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
			this.SuspendLayout();
			// 
			// CControlEditionProcess
			// 
			this.AutoScroll = true;
			this.Name = "CControlEditionProcess";
			cProfilEditeurObjetGraphique2.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
			cGrilleEditeurObjetGraphique2.Couleur = System.Drawing.Color.LightGray;
			cGrilleEditeurObjetGraphique2.HauteurCarreau = 20;
			cGrilleEditeurObjetGraphique2.LargeurCarreau = 20;
			cGrilleEditeurObjetGraphique2.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
			cGrilleEditeurObjetGraphique2.TailleCarreau = new System.Drawing.Size(20, 20);
			cProfilEditeurObjetGraphique2.Grille = cGrilleEditeurObjetGraphique2;
			cProfilEditeurObjetGraphique2.HistorisationActive = true;
			cProfilEditeurObjetGraphique2.Marge = 10;
			cProfilEditeurObjetGraphique2.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
			cProfilEditeurObjetGraphique2.NombreHistorisation = 10;
			cProfilEditeurObjetGraphique2.ToujoursAlignerSurLaGrille = false;
			this.Profil = cProfilEditeurObjetGraphique2;
			this.ToujoursAlignerSurLaGrille = false;
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CControlEditionProcess_MouseUp);
			this.BeforeDeleteElement += new sc2i.win32.common.EventHandlerPanelEditionGraphiqueSuppression(this.CControlEditionProcess_BeforeDeleteElement);
			this.ResumeLayout(false);

		}
		#endregion


		//Lorsqu'on est en mode lien2 : contient l'action départ du lien
		private CAction m_actionDebutLien = null;

		private CInfoAction m_infoActionGeneriqueACree = null;

		private EModeEditeurProcess m_modeEnCours = EModeEditeurProcess.Selection;
		private Cursor m_customCursor = null;

		public CControlEditionProcess()
		{
			InitializeComponent();
            CWin32Traducteur.Translate(typeof(CPanelEditionObjetGraphique), m_mnu);
		}

		public event EventHandler AfterModeEditionChanged;

		/// ////////////////////////////////////////////////////////////////
		public EModeEditeurProcess ModeEdition
		{
			get
			{ 
				if (LockEdition)
					return EModeEditeurProcess.Selection;
				return m_modeEnCours;
			}
			set
			{
				if ( m_modeEnCours != value )
				{
					if (LockEdition)
						m_modeEnCours = EModeEditeurProcess.Selection;
					else
						m_modeEnCours = value;
                    if (m_modeEnCours == EModeEditeurProcess.Selection)
                        ModeSouris = EModeSouris.Selection;
                    else if (m_modeEnCours == EModeEditeurProcess.Zoom)
                        ModeSouris = EModeSouris.Zoom;
                    else
                        ModeSouris = EModeSouris.Custom;
					LoadCurseurAdapté();
					
					if ( AfterModeEditionChanged != null )
						AfterModeEditionChanged ( this, new EventArgs() );
				}
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public CInfoAction InfoActionACree
		{
			get
			{
				if ( m_modeEnCours == EModeEditeurProcess.Action )
					return m_infoActionGeneriqueACree;
				return null;
			}
			set
			{
				if ( value != null )
				{
					ModeEdition = EModeEditeurProcess.Action;
					m_infoActionGeneriqueACree = value;
				}
			}
		}


		/// //////////////////////////////////////////
		protected override void LoadCurseurAdapté()
		{
			if ( m_customCursor != null && m_customCursor!= Cursors.Arrow)
				m_customCursor.Dispose();
			m_customCursor = null;
			switch ( ModeEdition )
			{
				case EModeEditeurProcess.Action :
					m_customCursor = new Cursor ( GetType(), "curAction.cur");
					break;
				case EModeEditeurProcess.Condition :
					m_customCursor = new Cursor ( GetType(), "curCondition.cur");
					break;
				case EModeEditeurProcess.Jonction :
					m_customCursor = new Cursor ( GetType(), "curJonction.cur" );
					break;
                case EModeEditeurProcess.EntryPoint :
                    m_customCursor = new Cursor(GetType(), "curEntry.cur");
                    break;
				case EModeEditeurProcess.Lien1 :
					m_customCursor = new Cursor ( GetType(), "curLien1.cur");
					break;
				case EModeEditeurProcess.Lien2 :
					m_customCursor = new Cursor ( GetType(), "curLien2.cur");
					break;
				default :
					m_customCursor = null;
					break;
			}
            if (m_customCursor != null)
                Cursor = m_customCursor;
            else
                base.LoadCurseurAdapté();
		}

		/// ////////////////////////////////////////////////////////////////
		public event EventHandler AfterAddElementToProcess;

		

		/// ////////////////////////////////////////////////////////////////
		private void CControlEditionProcess_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point ptMouseUp = new Point(e.X, e.Y);
			Point ptSouris = GetLogicalPointFromDisplay ( ptMouseUp );
			CAction nouvelleAction = null;
			switch ( ModeEdition )
			{
				case EModeEditeurProcess.Action :
					if ( InfoActionACree != null )
					{
						try
						{
							nouvelleAction = (CAction)Activator.CreateInstance ( InfoActionACree.TypeAction, new object[]{ProcessEdite} );
						}
						catch(Exception exp )
						{
							throw new Exception(I.T("Error while allocationg an action from type @1|30001",InfoActionACree.TypeAction.ToString()), exp );
						}
					}
					break;
				case EModeEditeurProcess.Condition :
					nouvelleAction = new CActionCondition ( ProcessEdite );
					break;
                case EModeEditeurProcess.EntryPoint :
                    nouvelleAction = new CActionPointEntree(ProcessEdite);
                    break;
				case EModeEditeurProcess.Jonction :
					nouvelleAction = new CActionJonction ( ProcessEdite );
					break;
				case EModeEditeurProcess.Lien1 :
					m_actionDebutLien = ProcessEdite.GetActionFromPoint ( ptSouris );
					
					if ( m_actionDebutLien != null )
					{
						CLienAction[] liensTest = m_actionDebutLien.GetLiensSortantsPossibles();
						if ( liensTest.Length == 0 )
						{
							CFormAlerte.Afficher(I.T("Impossible to add an output link to the action|30002"), EFormAlerteType.Erreur);
							return;
						}
						ModeEdition = EModeEditeurProcess.Lien2;
					}
					break;
				case EModeEditeurProcess.Lien2 :
					ModeEdition = EModeEditeurProcess.Lien1;
					CAction action = ProcessEdite.GetActionFromPoint ( ptSouris );
					if ( action != m_actionDebutLien && action != null )
					{
						CLienAction[] liens = m_actionDebutLien.GetLiensSortantsPossibles();
						if ( liens.Length == 0 )
						{
                            CFormAlerte.Afficher(I.T("Impossible to add an output link to the action|30002"), EFormAlerteType.Erreur);
							return;
						}
						CLienAction lien = null;
						if ( liens.Length == 1 )
							lien = liens[0];
						else
						{
							Point ptEcran = this.PointToScreen ( ptMouseUp );
							lien = CFormSelectLienSortant.SelectLien(liens, ptEcran);
						}
						if ( lien == null )
						{
							ModeEdition = EModeEditeurProcess.Lien2;
							return;
						}
						if ( CEditeurActionsEtLiens.EditeObjet ( lien ))
						{
							lien.ActionDepart = m_actionDebutLien;
							lien.ActionArrivee = action;
							ProcessEdite.AddLien ( lien );
							Selection.Clear();
							RefreshSelectionChanged = true;
							if ( AfterAddElementToProcess != null )
								AfterAddElementToProcess ( this, new EventArgs() );
						}
						ModeEdition = EModeEditeurProcess.Lien1;
					}
					break;
			}
			if ( nouvelleAction != null )
			{
				if ( CEditeurActionsEtLiens.EditeObjet ( nouvelleAction ) )
				{
					ProcessEdite.AddAction ( nouvelleAction );
					Point pt = new Point ( ptSouris.X-nouvelleAction.Size.Width/2, ptSouris.Y - nouvelleAction.Size.Height/2);
					nouvelleAction.Position = pt;
					ModeEdition = EModeEditeurProcess.Selection;
					RefreshSelectionChanged = false;
					Selection.Clear();
					RefreshSelectionChanged = true;
					Selection.Add ( nouvelleAction );
					if ( AfterAddElementToProcess != null )
						AfterAddElementToProcess ( this, new EventArgs() );
				}
			}
		}

		public override bool MNU_DeleteShow()
		{
			return Selection.Count > 0 && !Selection.Contains(ObjetEdite);
		}
		/// //////////////////////////////////////////
		public CProcess ProcessEdite
		{
			get
			{
				return (CProcess)ObjetEdite;
			}
			set
			{
				ObjetEdite = value;
				if ( value != null )
					this.AutoScrollMinSize = value.Size;
			}
		}

		private bool CControlEditionProcess_BeforeDeleteElement(List<I2iObjetGraphique> objs)
		{
			for (int n = objs.Count; n > 0; n--)
			{ 
				I2iObjetGraphique ele = objs[n - 1];
				Type tpLienAction = typeof(CLienAction);
				if (ele.IsLock && !tpLienAction.IsAssignableFrom(ele.GetType()))
					objs.RemoveAt(n - 1);
			}

			return true;
		}

        ToolStripMenuItem m_menuGrouperActions = null;
        ToolStripMenuItem m_menuDegrouperActions = null;
        ToolStripItem[] m_menusSortie = null;

        private class CMenuItemModeSortie : ToolStripMenuItem
        {
            private EModeSortieLien m_mode;
            public CMenuItemModeSortie(CModeSortieLien mode)
                : base(mode.Libelle)
            {
                m_mode = mode;
            }

            public EModeSortieLien Mode
            {
                get{
                    return m_mode;
                }
            }
        }


        protected override void AfficherMenuAdditonnel(ContextMenuStrip menu)
        {
            base.AfficherMenuAdditonnel(menu);

            if (m_menuGrouperActions == null)
            {
                m_menuGrouperActions = new ToolStripMenuItem(I.T("Group actions|20032"));
                menu.Items.Add(m_menuGrouperActions);
                m_menuGrouperActions.Name = "m_menuGrouperActions";
                m_menuGrouperActions.Size = new System.Drawing.Size(194, 22);
                m_menuGrouperActions.Click += new EventHandler(m_menuGrouperActions_Click);
            }
            m_menuGrouperActions.Visible = Selection.Count > 0;

            if ( m_menuDegrouperActions == null )
            {
                m_menuDegrouperActions = new ToolStripMenuItem ( I.T("Ungroup actions|20033") );
                menu.Items.Add(m_menuDegrouperActions);
                m_menuDegrouperActions.Name="m_menuDegroupeActions";
                m_menuDegrouperActions.Size = new Size(194,22);
                m_menuDegrouperActions.Click += new EventHandler(m_menuDegrouperActions_Click);
            }

            if ( m_menusSortie == null )
            {
                List<ToolStripItem> lst = new List<ToolStripItem>();
                lst.Add(new ToolStripSeparator());
                foreach ( EModeSortieLien mode in Enum.GetValues ( typeof(EModeSortieLien ) ))
                {
                    CMenuItemModeSortie item = new CMenuItemModeSortie ( new CModeSortieLien(mode) );
                    item.Click += new EventHandler(menuMode_Click);
                    lst.Add (item);
                }
                lst.Add(new ToolStripSeparator());
                m_menusSortie = lst.ToArray();
                menu.Items.AddRange(m_menusSortie);
            }

            bool bLienUnique = Selection.Count == 1 && Selection[0] is CLienAction;
            foreach (ToolStripItem item in m_menusSortie)
            {
                item.Visible = bLienUnique;
                CMenuItemModeSortie sortie = item as CMenuItemModeSortie;
                if (sortie != null && bLienUnique)
                    sortie.Checked = sortie.Mode == ((CLienAction)Selection[0]).ModeSortieDuLien;
            }
            

            m_menuDegrouperActions.Visible = Selection.Count == 1 && Selection[0] is CActionProcessFils;
        }


        void menuMode_Click(object sender, EventArgs e)
        {
            CMenuItemModeSortie item = sender as CMenuItemModeSortie;
            if (item != null)
            {
                if (Selection.Count == 1)
                {
                    CLienAction lien = Selection[0] as CLienAction;
                    lien.ModeSortieDuLien = item.Mode;
                    Refresh();
                }
            }
        }
 
        void m_menuDegrouperActions_Click(object sender, EventArgs e)
        {
            if (Selection.Count == 1 && Selection[0] is CActionProcessFils)
            {
                CActionProcessFils action = Selection[0] as CActionProcessFils;
                CResultAErreur result = action.Degrouper();
                if (!result)
                {
                    CFormAfficheErreur.Show(result.Erreur);
                }
                else
                {
                    Selection.Clear();
                    if (result.Data is IEnumerable<CAction>)
                    {
                        List<I2iObjetGraphique> lstTmp = new List<I2iObjetGraphique>();
                        foreach (CAction actionTmp in (IEnumerable<CAction>)result.Data)
                            lstTmp.Add(actionTmp);
                        Selection.AddRange(lstTmp);
                        Refresh();
                    }
                }
            }
        } 

        void m_menuGrouperActions_Click(object sender, EventArgs e)
        {
            if (Selection.Count > 0)
            {
                CResultAErreur result = CResultAErreur.True;
                using (CWaitCursor waiter = new CWaitCursor())
                {
                    List<CAction> lst = new List<CAction>();
                    foreach (object obj in Selection)
                    {
                        CAction action = obj as CAction;
                        if (action != null && action.GetType() != typeof(CActionDebut) && action.GetType() != typeof(CActionPointEntree))
                            lst.Add(action);
                    }

                    result = CActionProcessFils.CreateForSelection(lst.ToArray());
                    if (result)
                    {
                        Selection.Clear();
                        if (result.Data is CAction)
                            Selection.Add(result.Data as CAction);

                        Refresh();
                    }
                }
                if ( !result )
                {
                    CFormAfficheErreur.Show ( result.Erreur );
                }
            }
        }

	}

	public enum EModeEditeurProcess
	{
		Selection,
        Zoom,
		Action,
		Condition,
		Lien1,
		Lien2,
		Jonction,
        EntryPoint
	}

}

