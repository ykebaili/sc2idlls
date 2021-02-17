using System;
using System.Drawing;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.process
{
    /// <summary>
    /// Description résumée de CActionForEach.
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionForEach : CAction
    {

        private class CDonneeActionForEach : IDonneeAction
        {
            private int m_nPosition = 0;

            /// //////////////////////
            public int Position
            {
                get
                {
                    return m_nPosition;
                }
                set
                {
                    m_nPosition = value;
                }
            }

            /// //////////////////////
            private int GetNumVersion()
            {
                return 0;
            }

            /// //////////////////////
            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;
                serializer.TraiteInt(ref m_nPosition);
                return result;
            }
        }

        /* TESTDBKEYOK (XL)*/
        private string m_strIdVariableListe = "";
        private string m_strIdVariableElementEnCours = "";

        /// /////////////////////////////////////////
        public CActionForEach(CProcess process)
            : base(process)
        {
            Libelle = I.T("For Each|175");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("For Each element in list|176"),
                I.T("Carry out actions on all elements of a list|177"),
                typeof(CActionForEach),
                CGestionnaireActionsDisponibles.c_categorieDeroulement);
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            table[m_strIdVariableElementEnCours] = true;
            table[m_strIdVariableListe] = true;
        }

        /// /////////////////////////////////////////////////////////
        public override bool PeutEtreExecuteSurLePosteClient
        {
            get { return true; }
        }

        /// /////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage des Ids des Variables en String
        }

        /// /////////////////////////////////////////////////////////
        protected override CLienAction[] GetMyLiensSortantsPossibles()
        {
            CLienAction[] listeLiens = GetLiensSortantHorsErreur();
            bool bHasBoucle = false;
            bool bHasSortie = false;
            foreach (CLienAction lien in listeLiens)
            {
                bHasBoucle |= lien is CLienBoucle;
                bHasSortie |= lien is CLienFinBoucle;
            }
            ArrayList lst = new ArrayList();
            if (!bHasBoucle)
                lst.Add(new CLienBoucle(Process));
            if (!bHasSortie)
                lst.Add(new CLienFinBoucle(Process));
            return (CLienAction[])lst.ToArray(typeof(CLienAction));
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

            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableElementEnCours = nIdTemp.ToString();
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableListe = nIdTemp.ToString();
            }
            else
            {
                serializer.TraiteString(ref m_strIdVariableElementEnCours);
                serializer.TraiteString(ref m_strIdVariableListe);
            }
            return result;
        }

        /// ////////////////////////////////////////////////////////
        public string IdVariableElementEnCours
        {
            get
            {
                return m_strIdVariableElementEnCours;
            }
            set
            {
                m_strIdVariableElementEnCours = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableElementEnCours
        {
            get
            {
                return Process.GetVariable(IdVariableElementEnCours);
            }
            set
            {
                if (value == null)
                    m_strIdVariableElementEnCours = "";
                else
                    m_strIdVariableElementEnCours = value.IdVariable;
            }
        }

        /// ////////////////////////////////////////////////////////
        public string IdVariableListe
        {
            get
            {
                return m_strIdVariableListe;
            }
            set
            {
                m_strIdVariableListe = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableListe
        {
            get
            {
                return (CVariableDynamique)Process.GetVariable(IdVariableListe);
            }
            set
            {
                if (value == null)
                    m_strIdVariableListe = "";
                else
                    m_strIdVariableListe = value.IdVariable;
            }
        }


        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;

            if (VariableElementEnCours == null)
            {
                result.EmpileErreur(I.T("The variable indicating the element in progress in the list isn't defined or doesn't exist|178"));
                return result;
            }

            if (VariableListe == null)
            {
                result.EmpileErreur(I.T("The variable containing the list isn't defined or doesn't exist|179"));
                return result;
            }

            CTypeResultatExpression typeResultat = VariableListe.TypeDonnee;
            if (!typeResultat.IsArrayOfTypeNatif)
            {
                result.EmpileErreur(I.T("The variable of @1 list isn't a list|180", VariableListe.Nom));
                return result;
            }

            if (!typeResultat.GetTypeElements().Equals(VariableElementEnCours.TypeDonnee))
            {
                result.EmpileErreur(I.T("The variable containing the elements isn't of the good type|181"));
            }
            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;
            CDonneeActionForEach donnee = (CDonneeActionForEach)contexte.Branche.GetDataAction(IdObjetProcess);
            if (donnee == null)
            {
                donnee = new CDonneeActionForEach();
                donnee.Position = 0;
            }
            contexte.Branche.StockeDataAction(IdObjetProcess, donnee);
            IList liste = (IList)Process.GetValeurChamp(VariableListe.IdVariable);
            CLienAction lienBoucle = null;
            CLienAction lienFinal = null;
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                if (lien is CLienBoucle)
                    lienBoucle = lien;
                else
                    lienFinal = lien;
            }

            ArrayList lstCopie = new ArrayList(liste);
            liste = lstCopie;

            for (int nPosition = 0; nPosition < liste.Count && lienBoucle != null; nPosition++)
            {
                try
                {
                    if (contexte.IndicateurProgression != null && contexte.IndicateurProgression.CancelRequest)
                    {
                        result.EmpileErreur(I.T("User cancellation|182"));
                        return result;
                    }
                }
                catch { }
                Process.SetValeurChamp(VariableElementEnCours, liste[nPosition]);
                contexte.PushElementInfoProgress(liste[nPosition]);
                contexte.SetInfoProgression(nPosition.ToString() + "/" + liste.Count.ToString());
                donnee.Position = nPosition;
                if (lienBoucle != null)
                    result = contexte.Branche.ExecuteAction(lienBoucle.ActionArrivee, contexte, false);
                contexte.PopElementInfoProgress();
                if (!result)
                    return result;
            }
            result.Data = lienFinal;
            return result;
        }

        /// ///////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            Graphics g = ctx.Graphic;
            DrawVariableSortie(g, VariableElementEnCours);
            DrawVariableEntree(g, VariableListe);
        }
    }
}
