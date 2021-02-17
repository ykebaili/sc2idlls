using System;
using System.Collections;

using System.Drawing;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;

namespace sc2i.process
{
    /// <summary>
    /// Ouvre un fichier ou une URL sur le poste client
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionOuvrirFichier : CAction
    {
        private C2iExpression m_expressionFichier = null;
        private C2iExpression m_expressionArguments = null;
        private bool m_bWaitForExit = false;

        /* TESTDBKEYOK (XL)*/

        private string m_strIdVariableCodeRetour = "";
        private bool m_bSurServeur = false;

        /// /////////////////////////////////////////////////////////
        public CActionOuvrirFichier(CProcess process)
            : base(process)
        {
            Libelle = I.T("Open a file or URL|234");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Open a file or URL|234"),
                I.T("Open a file or URL to the client|235"),
                typeof(CActionOuvrirFichier),
                CGestionnaireActionsDisponibles.c_categorieInterface);
        }

        /// /////////////////////////////////////////////////////////
        public override bool PeutEtreExecuteSurLePosteClient
        {
            get { return true; }
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            AddIdVariablesExpressionToHashtable(m_expressionFichier, table);
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression FormuleFichier
        {
            get
            {
                if (m_expressionFichier == null)
                    m_expressionFichier = new C2iExpressionConstante("");
                return m_expressionFichier;
            }
            set
            {
                m_expressionFichier = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public bool WaitForExit
        {
            get
            {
                return m_bWaitForExit;
            }
            set
            {
                m_bWaitForExit = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public bool SurServeur
        {
            get
            {
                return m_bSurServeur;
            }
            set
            {
                m_bSurServeur = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public string IdVariableRetour
        {
            get
            {
                return m_strIdVariableCodeRetour;
            }
            set
            {
                m_strIdVariableCodeRetour = value;
            }
        }

        /// ///////////////////////////////////////////////
        public CVariableDynamique VariableResultat
        {
            get
            {
                if (m_strIdVariableCodeRetour != "")
                    return Process.GetVariable(m_strIdVariableCodeRetour);
                return null;
            }
            set
            {
                if (value == null)
                    m_strIdVariableCodeRetour = "";
                else
                    m_strIdVariableCodeRetour = value.IdVariable;
            }
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression FormuleArguments
        {
            get
            {
                if (m_expressionArguments == null)
                    m_expressionArguments = new C2iExpressionConstante("");
                return m_expressionArguments;
            }
            set
            {
                m_expressionArguments = value;
            }
        }


        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;

            if (VariableResultat != null &&
                (VariableResultat.TypeDonnee.TypeDotNetNatif != typeof(int) ||
                VariableResultat.TypeDonnee.IsArrayOfTypeNatif))
            {
                result.EmpileErreur(I.T("Open file returns an integer, bad variable type|20005"));
                return result;
            }

            if (VariableResultat != null && !WaitForExit)
            {
                result.EmpileErreur(I.T("Return value need WaitForExit option|20006"));
                return result;
            }

            return result;
        }



        /// /////////////////////////////////////////
        private int GetNumVersion()
        {
            return 4;
            // 1 : ajout de la saisie de la formule pour les Arguments
            // 2 : Ajout de la variable de retour et de WaitForExit
            // 3 : Ajout de "Sur serveur"
            // 4 : Passage de int IdVariable Retour en String
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if (!result)
                return result;

            I2iSerializable objet = (I2iSerializable)m_expressionFichier;
            result = serializer.TraiteObject(ref objet);
            m_expressionFichier = (C2iExpression)objet;

            if (nVersion >= 1)
            {
                objet = (I2iSerializable)m_expressionArguments;
                result = serializer.TraiteObject(ref objet);
                m_expressionArguments = (C2iExpression)objet;
            }
            else
            {
                m_expressionArguments = new C2iExpressionConstante("");
            }
            if (nVersion >= 2)
            {
                if (nVersion < 4 && serializer.Mode == ModeSerialisation.Lecture)
                {
                    int nIdTemp = -1;
                    serializer.TraiteInt(ref nIdTemp);
                    m_strIdVariableCodeRetour = nIdTemp.ToString();
                }
                else
                    serializer.TraiteString(ref m_strIdVariableCodeRetour);

                serializer.TraiteBool(ref m_bWaitForExit);
            }
            else
            {
                m_bWaitForExit = false;
                m_strIdVariableCodeRetour = "";
            }
            if (nVersion >= 3)
                serializer.TraiteBool(ref m_bSurServeur);
            else
                m_bSurServeur = false;

            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;
            string strCommandLine = "";
            string strArguments = "";

            //Calcule le fichier ou l'url
            CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
            contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
            result = FormuleFichier.Eval(contexteEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Error during the File name or URL evaluation|236"));
                return result;
            }
            strCommandLine += result.Data;
            // Calcul des Arguments
            result = FormuleArguments.Eval(contexteEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Error during the Arguments evaluation|308"));
                return result;
            }
            strArguments += result.Data;

            if (m_bSurServeur)
            {
                //Lance le programme depuis le serveur
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = strCommandLine;
                process.StartInfo.Arguments = strArguments;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                try
                {
                    process.Start();
                    if (m_bWaitForExit)
                    {
                        process.WaitForExit();
                        int nRetour = process.ExitCode;
                        CVariableDynamique variable = VariableResultat;
                        if (variable != null)
                            Process.SetValeurChamp(variable, nRetour);
                    }
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                    result.EmpileErreur(I.T("Error in command line @1 @2|20011",
                        strCommandLine, strArguments));
                }
                foreach (CLienAction lien in GetLiensSortantHorsErreur())
                {
                    if (!(lien is CLienUtilisateurAbsent))
                    {
                        result.Data = lien;
                        return result;
                    }
                }
                result.Data = null;
                return result;
            }

            //Execution sur le client
            CSessionClient sessionClient = CSessionClient.GetSessionForIdSession(contexte.IdSession);
            if (sessionClient != null)
            {
                //TESTDBKEYOK
                if (sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur)
                {
                    using (C2iSponsor sponsor = new C2iSponsor())
                    {
                        CServiceSurClient service = sessionClient.GetServiceSurClient(CInfoServiceClientOuvrirFichier.c_idServiceClientOuvrirFichier);
                        if (service != null)
                        {
                            sponsor.Register(service);
                            CInfoServiceClientOuvrirFichier infoService = new CInfoServiceClientOuvrirFichier(strCommandLine, strArguments, WaitForExit);

                            result = service.RunService(infoService);
                            if (!result)
                                return result;

                            CVariableDynamique variable = VariableResultat;
                            if (variable != null && result.Data is int && WaitForExit)
                                Process.SetValeurChamp(variable, result.Data);
                            foreach (CLienAction lien in GetLiensSortantHorsErreur())
                            {
                                if (!(lien is CLienUtilisateurAbsent))
                                {
                                    result.Data = lien;
                                    return result;
                                }
                            }
                            result.Data = null;
                            return result;
                        }
                    }
                }
            }
            //Utilisateur pas accessible
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                if (lien is CLienUtilisateurAbsent)
                {
                    result.Data = lien;
                    return result;
                }
            }
            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CLienAction[] GetMyLiensSortantsPossibles()
        {
            ArrayList lst = new ArrayList();
            Hashtable tableLiensExistants = new Hashtable();
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                tableLiensExistants[lien.GetType()] = true;
            }
            if (tableLiensExistants[typeof(CLienAction)] == null)
                lst.Add(new CLienAction(Process));
            if (tableLiensExistants[typeof(CLienUtilisateurAbsent)] == null && !m_bSurServeur)
                lst.Add(new CLienUtilisateurAbsent(Process));

            return (CLienAction[])lst.ToArray(typeof(CLienAction));
        }
    }
}