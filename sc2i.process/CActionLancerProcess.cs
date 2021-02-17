using System;
using System.Collections;
using System.Threading;


using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.multitiers.client;


namespace sc2i.process
{
    /// <summary>
    /// Description résumée de CActionSousProcess.
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionLancerProcess : CActionFonction
    {
        /* TESTDBKEYOK (XL)*/
        private CDbKey m_dbKeyProcess = null;
        private bool m_bModeAsynchrone = false;
        private int m_nIdPointEntree = -1;

        //Id variable process -> formule
        private Hashtable m_mapVariablesProcessToFormule = new Hashtable();

        private bool m_bLancerDansContexteSepare = false;
        private bool m_bSansTrace = false;

        /// /////////////////////////////////////////////////////////
        public CActionLancerProcess(CProcess process)
            : base(process)
        {
            Libelle = I.T("Sub process|189");
            m_mapVariablesProcessToFormule = new Hashtable();
            VariableRetourCanBeNull = true;
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Action execution|190"),
                I.T("Start an action|191"),
                typeof(CActionLancerProcess),
                CGestionnaireActionsDisponibles.c_categorieDeroulement);
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            foreach (C2iExpression expression in m_mapVariablesProcessToFormule.Values)
                AddIdVariablesExpressionToHashtable(expression, table);
        }

        /// /////////////////////////////////////////////////////////
        public override bool PeutEtreExecuteSurLePosteClient
        {
            get { return !m_bLancerDansContexteSepare; }
        }

        /// /////////////////////////////////////////////////////////
        ///Indique qu'il n'est pas conservé de trace de l'execution du sous process.
        ///Ne fonctionne que si le process n'est pas executé dans un contexte séparé
        public bool SansTrace
        {
            get
            {
                return m_bSansTrace;
            }
            set
            {
                m_bSansTrace = value;
            }
        }

        /// /////////////////////////////////////////////////////////
        public bool LancerDansUnProcessSepare
        {
            get
            {
                return m_bLancerDansContexteSepare;
            }
            set
            {
                m_bLancerDansContexteSepare = value;
            }
        }

        /// /////////////////////////////////////////
        [ExternalReferencedEntityDbKey(typeof(CProcessInDb))]
        public CDbKey DbKeyProcess
        {
            get
            {
                return m_dbKeyProcess;
            }
            set
            {
                m_dbKeyProcess = value;
            }
        }

        /// /////////////////////////////////////////////////////////
        ///Point d'entrée dans le process (-1 pour l'entrée par défaut)
        public int IdPointEntree
        {
            get
            {
                return m_nIdPointEntree;
            }
            set
            {
                m_nIdPointEntree = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression GetExpressionForVariableProcess(string strIdVariable)
        {
            return (C2iExpression)m_mapVariablesProcessToFormule[strIdVariable];
        }

        /// ////////////////////////////////////////////////////////
        public void SetExpressionForVariableProcess(string strIdVariable, C2iExpression expression)
        {
            m_mapVariablesProcessToFormule[strIdVariable] = expression;
        }

        /// ////////////////////////////////////////////////////////
        public void ClearExpressionsVariables()
        {
            m_mapVariablesProcessToFormule.Clear();
        }

        /// /////////////////////////////////////////
        public bool ModeAsynchrone
        {
            get
            {
                return m_bModeAsynchrone;
            }
            set
            {
                m_bModeAsynchrone = value;
            }
        }




        /// /////////////////////////////////////////
        private int GetNumVersion()
        {
            /*Version 1 : 
             *	-transformation en CActionFonction pour récuperer une valeur de retour
             * */
            //Version 2 : Lancement dans le même contexte
            //3 : ajout de la sélection du point d'entrée
            //4 : Ajout de NoTrace
            //return 4;
            return 5; // Passage en IdProcess en DbKeyProcess et m_mapVariablesProcessToFormule.Keys en String
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            if (nVersion >= 1)
                result = base.MySerialize(serializer);
            else
            {
                result = base.BaseSerialize(serializer);
            }
            if (!result)
                return result;

            if (nVersion < 5)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyProcess, typeof(CProcessInDb));
            else
                serializer.TraiteDbKey(ref m_dbKeyProcess);

            serializer.TraiteBool(ref m_bModeAsynchrone);

            I2iSerializable objet = null;
            int nNbVariables = m_mapVariablesProcessToFormule.Count;
            serializer.TraiteInt(ref nNbVariables);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strId in m_mapVariablesProcessToFormule.Keys)
                    {
                        string strTemp = strId;
                        serializer.TraiteString(ref strTemp);
                        objet = GetExpressionForVariableProcess(strId);
                        result = serializer.TraiteObject(ref objet);
                        if (!result)
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_mapVariablesProcessToFormule.Clear();
                    for (int nVar = 0; nVar < nNbVariables; nVar++)
                    {
                        int nIdVariableTemp = 0;
                        string strIdVariable = "0";
                        if (nVersion < 5)
                        {
                            serializer.TraiteInt(ref nIdVariableTemp);
                            strIdVariable = nIdVariableTemp.ToString();
                        }
                        else
                            serializer.TraiteString(ref strIdVariable);

                        objet = null;
                        result = serializer.TraiteObject(ref objet);
                        if (!result)
                            return result;
                        SetExpressionForVariableProcess(strIdVariable, (C2iExpression)objet);
                    }
                    break;
            }
            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bLancerDansContexteSepare);
            if (nVersion >= 3)
                serializer.TraiteInt(ref m_nIdPointEntree);
            else
                m_nIdPointEntree = -1;
            if (nVersion >= 4)
                serializer.TraiteBool(ref m_bSansTrace);

            return result;
        }

        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;
            CProcessInDb processInDB = new CProcessInDb(Process.ContexteDonnee);
            if (!processInDB.ReadIfExists(m_dbKeyProcess))
            {
                result.EmpileErreur(I.T("Invalid action to start|192"));
                return result;
            }
            //Vérifie le type des variables
            CProcess process = processInDB.Process;
            foreach (string strIdVariable in m_mapVariablesProcessToFormule.Keys)
            {
                IVariableDynamique variable = process.GetVariable(strIdVariable);
                if (variable != null)
                {
                    CTypeResultatExpression typeVariable = variable.TypeDonnee;
                    C2iExpression expression = (C2iExpression)m_mapVariablesProcessToFormule[strIdVariable];
                    if (expression != null)
                    {
                        if (!expression.TypeDonnee.Equals(typeVariable))
                        {
                            result.EmpileErreur(I.T("The formula of '@1' variable value must return a @2 type|193", variable.Nom, typeVariable.ToStringConvivial()));
                        }
                    }
                }
            }
            return result;
        }


        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.False;
            CProcessInDb processInDB = new CProcessInDb(contexte.ContexteDonnee);
            if (!processInDB.ReadIfExists(m_dbKeyProcess))
            {
                result.EmpileErreur(I.T("The @1 action doesn't exist|194", m_dbKeyProcess.ToString()));
                result.EmpileErreur(I.T("Error in an action execution from an action|195"));
                return result;
            }

            string strOldContextuel = contexte.ContexteDonnee.IdModificationContextuelle;

            CContexteDonnee contexteDonneeSousAction = contexte.ContexteDonnee;

            if (ModeAsynchrone || LancerDansUnProcessSepare)
                contexteDonneeSousAction = new CContexteDonnee(contexte.IdSession, true, true);

            //Si synchrone, s'execute dans le même contexte de données
            if (!ModeAsynchrone)
            {
                CProcessEnExecutionInDb processEnExec = new CProcessEnExecutionInDb(contexteDonneeSousAction);

                processEnExec.CreateNewInCurrentContexte();

                CProcess process = processInDB.Process;
                process.ContexteDonnee = contexteDonneeSousAction;

                object elementCible = contexte.ObjetCible;

                //Remplit les variables du process
                foreach (string strIdVariable in m_mapVariablesProcessToFormule.Keys)
                {
                    IVariableDynamique variable = process.GetVariable(strIdVariable);
                    //Evalue la formule
                    C2iExpression expression = (C2iExpression)m_mapVariablesProcessToFormule[strIdVariable];
                    CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
                    contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
                    result = expression.Eval(contexteEval);
                    if (!result)
                    {
                        result.EmpileErreur(I.T("Error during the variables assignment in sub action|197"));
                        result.EmpileErreur(I.T("@1 variable error|196", variable.Nom));
                        return result;
                    }
                    CObjetDonnee objDonnee = result.Data as CObjetDonnee;
                    if (objDonnee != null)
                        result.Data = objDonnee.GetObjetInContexte(contexteDonneeSousAction);
                    process.SetValeurChamp(variable.IdVariable, result.Data);
                    if (variable.IdVariable == CProcess.c_strIdVariableElement && result.Data is CObjetDonneeAIdNumerique)
                        elementCible = result.Data;

                }

                if (elementCible is CObjetDonneeAIdNumerique)
                    processEnExec.ElementLie = (CObjetDonneeAIdNumerique)elementCible;
                else
                    processEnExec.ElementLie = null;
                processEnExec.Libelle = process.Libelle;

                CBrancheProcess branche = new CBrancheProcess(process);
                branche.IsModeAsynchrone = ModeAsynchrone;
                //TESTDBKEYOK
                branche.KeyUtilisateur = contexte.Branche.KeyUtilisateur;
                branche.ConfigurationImpression = contexte.Branche.ConfigurationImpression;

                CContexteExecutionAction contexteExecution = new CContexteExecutionAction(
                    processEnExec,
                    branche,
                    elementCible,
                    ModeAsynchrone ? null : contexteDonneeSousAction, contexte.IndicateurProgression);

                CAction pointEntree = null;
                if (IdPointEntree >= 0)
                {
                    pointEntree = process.GetActionFromId(IdPointEntree);
                    if (pointEntree == null)
                    {
                        result.EmpileErreur(I.T("Can not find entry point|20018"));
                        return result;
                    }
                }
                else
                    pointEntree = process.GetActionDebut();

                if (!ModeAsynchrone)
                {
                    contexteExecution.SauvegardeContexteExterne = !m_bLancerDansContexteSepare;

                    result = branche.ExecuteAction(pointEntree, contexteExecution, elementCible, true);
                    foreach (CContexteExecutionAction.CParametreServiceALancerALaFin s in contexteExecution.ServicesALancerALaFin)
                        contexte.AddServiceALancerALaFin(s);
                    contexteExecution.ClearServicesALancerALaFin();
                    if (m_bSansTrace)
                    {
                        processEnExec.Table.Rows.Remove(processEnExec.Row.Row);
                    }
                    if (VariableResultat != null && process.VariableDeRetour != null)
                    {
                        object valeur = process.GetValeurChamp(process.VariableDeRetour);
                        if (valeur != null)
                            valeur = valeur.ToString();
                        Process.SetValeurChamp(VariableResultat.IdVariable, valeur.ToString());
                    }
                }
                else
                {
                    //Ouvre une nouvelle session pour éxecuter le process
                    CSessionProcessServeurSuivi sessionAsync = new CSessionProcessServeurSuivi();
                    result = sessionAsync.OpenSession(new CAuthentificationSessionProcess(),
                        I.T("Process @1|198", processInDB.Libelle),
                        CSessionClient.GetSessionForIdSession(contexte.IdSession));
                    if (!result)
                        return result;
                    contexteExecution.ChangeIdSession(sessionAsync.IdSession);
                    contexteExecution.HasSessionPropre = true;
                    m_brancheToExecute = branche;
                    m_actionToExecute = pointEntree;
                    m_elementCible = elementCible;
                    m_contexteExecutionProcess = contexteExecution;
                    Thread th = new Thread(new ThreadStart(DemarreProcess));
                    th.Start();
                    return result;
                }
            }
            contexte.ContexteDonnee.IdModificationContextuelle = strOldContextuel;
            return result;
        }

        /// //////////////////////////////////////////////////
        private CBrancheProcess m_brancheToExecute;
        private CAction m_actionToExecute;
        private CContexteExecutionAction m_contexteExecutionProcess;
        private object m_elementCible;
        private void DemarreProcess()
        {
            CResultAErreur result = m_brancheToExecute.ExecuteAction(m_actionToExecute, m_contexteExecutionProcess, m_elementCible, true);
            m_contexteExecutionProcess.OnEndProcess();
        }

        public override CTypeResultatExpression TypeResultat
        {
            get
            {
                return new CTypeResultatExpression(typeof(string), false);
            }
        }

        public override bool UtiliseObjet(object objetCherche)
        {
            if (base.UtiliseObjet(objetCherche))
                return true;
            CProcessInDb pr = objetCherche as CProcessInDb;
            if (pr != null && pr.DbKey == m_dbKeyProcess)
                return true;
            return false;
        }
    }
}
