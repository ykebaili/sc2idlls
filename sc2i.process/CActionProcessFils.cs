using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.multitiers.client;
using System.Drawing;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionSousProcess.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionProcessFils : CActionLienSortantSimple
	{
        private CProcess m_processFils = null;

		/// /////////////////////////////////////////////////////////
		public CActionProcessFils( CProcess process )
			:base(process)
		{
            m_processFils = new CProcess(process);
			Libelle = I.T("Action group|20033");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Action group|20033"),
				I.T("Group many actions in a single action box|20034"),
				typeof(CActionProcessFils),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

        /// /////////////////////////////////////////////////////////
        public CProcess ProcessFils
        {
            get
            {
                return m_processFils;
            }
            set
            {
                m_processFils = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            foreach (CAction action in m_processFils.Actions)
                action.AddIdVariablesNecessairesInHashtable(table);
        }

		

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}


		

		


		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}



		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if (result)
                result = base.MySerialize(serializer);
			if ( !result )
				return result;

            result = serializer.TraiteObject<CProcess>(ref m_processFils, new object[]{Process});
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_processFils.ProcessParent = Process;
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			
				CBrancheProcess branche = new CBrancheProcess( m_processFils );
				branche.IsModeAsynchrone = false;
            //TESTDBKEYOK
				branche.KeyUtilisateur = contexte.Branche.KeyUtilisateur;
				branche.ConfigurationImpression = contexte.Branche.ConfigurationImpression;

				CContexteExecutionAction contexteExecution = new CContexteExecutionAction (
					contexte.ProcessEnExecution, 
					branche, 
					contexte.ObjetCible,
                    contexte.ContexteDonnee, 
                    contexte.IndicateurProgression );

					contexteExecution.SauvegardeContexteExterne = true;

            CAction pointEntree = m_processFils.GetActionDebut();
                        
            CResultAErreur result = CResultAErreur.True;
			result = branche.ExecuteAction(pointEntree, contexteExecution, contexte.ObjetCible, false);
            foreach (CContexteExecutionAction.CParametreServiceALancerALaFin p in contexteExecution.ServicesALancerALaFin)
                contexte.AddServiceALancerALaFin(p);
            contexteExecution.ClearServicesALancerALaFin();
			return result;
		}

		
        /// ////////////////////////////////////////////////////////
        public override bool UtiliseObjet(object objetCherche)
        {
            if (base.UtiliseObjet(objetCherche))
                return true;

            foreach ( CAction action in m_processFils.Actions )
                if ( action.UtiliseObjet ( objetCherche ))
                    return true;
            
            
            return false;
        }

        /// ////////////////////////////////////////////////////////
        ///Le data du result contient la liste des actions dégroupées
        public CResultAErreur Degrouper()
        {
            CResultAErreur result = CResultAErreur.True;
            foreach ( IVariableDynamique variable in m_processFils.ListeVariables )
            {
                if ( !m_processFils.IsVariableDeProcessParent ( variable ) )
                {
                    result.EmpileErreur(I.T("Can not ungroup a group with variables|20040"));
                    return result;
                }
            }

            Dictionary<CAction, CAction> dicCorrespondanceActions = new Dictionary<CAction,CAction>();
            foreach (CAction action in m_processFils.Actions)
            {
                if (action.GetType() != typeof(CActionDebut))
                {
                    CAction newAction = Activator.CreateInstance(action.GetType(), new object[] { Process }) as CAction;

                    CCloner2iSerializable.CopieTo(action, newAction);
                    newAction.Position = new Point(newAction.Position.X + Position.X,
                        newAction.Position.Y + Position.Y);
                    dicCorrespondanceActions[action] = newAction;
                    Process.AddAction(newAction);
                }
            }

            //Crée les liens
            CLienAction lienDebut = null;
            CAction startAction = m_processFils.GetActionDebut();
            List<CLienAction> lstLiens = new List<CLienAction>();
            foreach (CLienAction lien in m_processFils.Liens)
            {
                CAction actionDepart = null;
                CAction actionArrivee = null;
                if (lien.ActionDepart == startAction)
                    lienDebut = lien;
                else
                {
                    if (!dicCorrespondanceActions.TryGetValue(lien.ActionDepart, out actionDepart))
                    {
                        result.EmpileErreur(I.T("An error occured during grouping process|20038"));
                        return result;
                    }
                    if (!dicCorrespondanceActions.TryGetValue(lien.ActionArrivee, out actionArrivee))
                    {
                        result.EmpileErreur(I.T("An error occured during grouping process|20038"));
                        return result;
                    }
                    CLienAction newLien = Activator.CreateInstance(lien.GetType(), new object[] { Process }) as CLienAction;
                    CCloner2iSerializable.CopieTo(lien, newLien);
                    newLien.ActionArrivee = actionArrivee;
                    newLien.ActionDepart = actionDepart;
                    Process.AddLien(newLien);

                }
            }
            //Modifie les liens entrants
            foreach (CLienAction lien in GetLiensArrivant())
            {
                if (lienDebut == null)
                    Process.RemoveLien(lien);
                else
                {
                    //Modifie l'action arrivée du lien
                    lien.ActionArrivee = dicCorrespondanceActions[lienDebut.ActionArrivee];
                }
            }
            CLienAction lienSortant = null;
            foreach (CLienAction lien in GetLiensSortant())
                if (lien.GetType() == typeof(CLienAction))//Lien simple
                    lienSortant = lien;

            List<CAction> lstRetour = new List<CAction>(dicCorrespondanceActions.Values);
            
            if (lienSortant != null)
            {
                foreach (CAction action in lstRetour)
                {
                    foreach (CLienAction lien in action.GetLiensSortantsPossibles())
                    {
                        if (!(lien is CLienErreur))
                        {
                            lien.ActionDepart = action;
                            lien.ActionArrivee = lienSortant.ActionArrivee;
                            Process.AddLien(lien);
                        }
                    }
                }
            }

            Process.RemoveAction(this);
            
            result.Data = lstRetour.AsReadOnly();
            return result;
        }

        /// <summary>
        /// Crée un process fils à partir des actions d'un process.
        /// Les actions listées sont mises dans le process fils
        /// </summary>
        /// <param name="process"></param>
        /// <param name="actions"></param>
        /// <returns>Le data du result contient le process fils créé</returns>
        public static CResultAErreur CreateForSelection(CAction[] actions)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( actions.Length == 0 )
            {
                result.EmpileErreur(I.T("Can not create action group from empty selection|20035"));
                return result;
            }
            CProcess process = actions[0].Process;
            //Vérifie qu'il n'y a qu'un seul lien sortant et un seul lien entrant
            HashSet<CAction> dicActionsInSel = new HashSet<CAction>();
            foreach (CAction action in actions)
                dicActionsInSel.Add(action);
            CLienAction lienEntrant = null;
            Rectangle rctEnglobant = actions[0].RectangleAbsolu;

            List<CLienAction> liensSortants = new List<CLienAction>();
            foreach (CAction action in actions)
            {
                foreach (CLienAction lien in action.GetLiensArrivant())
                {
                    if (!dicActionsInSel.Contains(lien.ActionDepart))
                    {
                        if (lienEntrant == null)
                        {
                            lienEntrant = lien;
                            break;
                        }
                        else
                        {
                            result.EmpileErreur(I.T("Selection should contains only one ingoing link|20036"));
                            return result;
                        }
                    }
                    if (!dicActionsInSel.Contains(lien.ActionArrivee))
                    {
                        if (lien.GetType() != typeof(CLienAction))
                        {
                            result.EmpileErreur(I.T("Can not group actions with complex outgoing link|20037"));
                        }
                    }
                }
                foreach (CLienAction lien in action.GetLiensSortant())
                {
                    if (!dicActionsInSel.Contains(lien.ActionArrivee))
                    {
                        liensSortants.Add(lien);
                    }
                }
                CActionProcessFils pf = action as CActionProcessFils;
                if (pf != null)
                {
                    foreach ( IVariableDynamique variable in pf.ProcessFils.ListeVariables )
                        if (!pf.ProcessFils.IsVariableDeProcessParent(variable))
                        {
                            result.EmpileErreur(I.T("Can not group a group with variables|20039"));
                            return result;
                        }
                }
                    
                Rectangle rctAction = action.RectangleAbsolu;
                rctEnglobant.Location = new Point(
                    Math.Min(rctEnglobant.Left, rctAction.Left),
                    Math.Min(rctEnglobant.Top, rctAction.Top));
                rctEnglobant.Size = new Size(
                    Math.Max(rctEnglobant.Width, rctAction.Right - rctEnglobant.Left),
                    Math.Max(rctEnglobant.Height, rctAction.Bottom - rctEnglobant.Top));
            }
            //Crée le process fils
            CActionProcessFils actionFils = new CActionProcessFils ( process );
            actionFils.Position = new Point ( rctEnglobant.Left + (rctEnglobant.Width-actionFils.Size.Width)/2,
                rctEnglobant.Top + (rctEnglobant.Height-actionFils.Size.Height)/2 );
            CProcess processFils = actionFils.ProcessFils;
            Dictionary<CAction, CAction> dicCorrespondanceActions = new Dictionary<CAction, CAction>();
            int nOffsetX = processFils.GetActionDebut().Position.X - rctEnglobant.Left;
            int nOffsetY = processFils.GetActionDebut().Position.Y + 40 - rctEnglobant.Top;
            foreach (CAction action in actions)
            {
                CAction newAction = Activator.CreateInstance(action.GetType(), new object[] { processFils }) as CAction;

                CCloner2iSerializable.CopieTo(action, newAction);
                newAction.Position = new Point(newAction.Position.X + nOffsetX,
                    newAction.Position.Y + nOffsetY);
                dicCorrespondanceActions[action] = newAction;
                processFils.AddAction(newAction);
            }
            //copie des liens
            HashSet<CLienAction> hashLiens = new HashSet<CLienAction>();
            //Crée la liste de tous les liens
            foreach (CAction action in actions)
            {
                foreach (CLienAction lien in action.GetLiensSortant())
                    if (!liensSortants.Contains ( lien ) && !hashLiens.Contains(lien))
                        hashLiens.Add(lien);
                foreach (CLienAction lien in action.GetLiensArrivant())
                    if (lien != lienEntrant && !hashLiens.Contains(lien))
                        hashLiens.Add(lien);
            }
            foreach (CLienAction lien in hashLiens)
            {
                CAction actionDepart = null;
                CAction actionArrivee = null;
                if (!dicCorrespondanceActions.TryGetValue(lien.ActionDepart, out actionDepart))
                {
                    result.EmpileErreur(I.T("An error occured during grouping process|20038"));
                    return result;
                }
                if (!dicCorrespondanceActions.TryGetValue(lien.ActionArrivee, out actionArrivee))
                {
                    result.EmpileErreur(I.T("An error occured during grouping process|20038"));
                    return result;
                }
                CLienAction newLien = Activator.CreateInstance(lien.GetType(), new object[] { processFils }) as CLienAction;
                CCloner2iSerializable.CopieTo(lien, newLien);
                newLien.ActionArrivee = actionArrivee;
                newLien.ActionDepart = actionDepart;
                processFils.AddLien(newLien);
            }

            //Crée le lien de départ
            if (lienEntrant != null)
            {
                CLienAction newLienDepart = new CLienAction(processFils);
                newLienDepart.ActionDepart = processFils.GetActionDebut();
                newLienDepart.ActionArrivee = dicCorrespondanceActions[lienEntrant.ActionArrivee];
                processFils.AddLien(newLienDepart);

                CAction action1 = newLienDepart.ActionArrivee;
                CAction actionStart = newLienDepart.ActionDepart;
                actionStart.Position = new Point(
                    action1.Position.X + (action1.Size.Width - actionStart.Size.Width) / 2,
                    actionStart.Position.Y);
            }

            process.AddAction(actionFils);
            //Modifie le lien de départ
            if (lienEntrant != null)
                lienEntrant.ActionArrivee = actionFils;
            foreach (CLienAction lien in liensSortants)
            {
                lien.ActionDepart = actionFils;
            }
            foreach (CAction action in actions)
            {
                process.RemoveAction(action);
            }
            result.Data = actionFils;
            return result;

        }
	}
}
