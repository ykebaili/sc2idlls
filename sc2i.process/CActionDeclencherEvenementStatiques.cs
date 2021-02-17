using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.multitiers.client;

namespace sc2i.process
{
    /// <summary>
    /// Déclenche tous les evenements de création non déclenchés ou les évenements
    /// sur modification déclenchables et non encore déclenchés
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionDeclencherEvenementStatiques : CActionLienSortantSimple
    {
        /* TESTDBKEYOK (XL)*/

        // Element sur lequel l'évenement sera déclenché
        private string m_strIdVariableElement = "";

        private CParametreDeclencheurEvenement m_parametreDeclencheur = new CParametreDeclencheurEvenement();

        /// /////////////////////////////////////////
        public CActionDeclencherEvenementStatiques(CProcess process)
            : base(process)
        {
            Libelle = I.T("Start static events|159");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Start static events|159"),
                I.T("Start cration events not started and modification events without initial value and not started|160"),
                typeof(CActionDeclencherEvenementStatiques),
                CGestionnaireActionsDisponibles.c_categorieComportement);
        }

        /// /////////////////////////////////////////////////////////
        public override bool PeutEtreExecuteSurLePosteClient
        {
            get { return false; }
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            table[m_strIdVariableElement] = true;
        }

        /// /////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage de int IdVariableElmement en String
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
                m_strIdVariableElement = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableElement);

            return result;
        }

        /// <summary>
        /// La variable contient l'élément sur lequel l'évenement sera déclenché
        /// </summary>
        public string IdVariableElement
        {
            get
            {
                return m_strIdVariableElement;
            }
            set
            {
                m_strIdVariableElement = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableElement
        {
            get
            {
                return Process.GetVariable(IdVariableElement);
            }
            set
            {
                if (value == null)
                    m_strIdVariableElement = "";
                else
                {
                    m_strIdVariableElement = value.IdVariable;
                }
            }
        }


        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = base.VerifieDonnees();

            CVariableDynamique variable = VariableElement;
            if (variable == null)
            {
                result.EmpileErreur(I.T("The variable containing the element for which the events are started must be exist|161"));
            }

            if (!typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif))
            {
                //ce n'est pas un CObjetDonneeAIdNumeriqueAuto
                result.EmpileErreur(I.T("The variable containing the element for which the events are started is incorrect|162"));
                return result;
            }

            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;

            //Evalue l'élément cible
            object obj = Process.GetValeurChamp(IdVariableElement);
            if (obj != null)
            {
                if (!(obj is CObjetDonneeAIdNumerique))
                {
                    result.EmpileErreur(I.T("The element for which to start the actions isn't correct|163"));
                    return result;
                }
                CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)obj;
                IDeclencheurEvenementsStatiques declencheur = (IDeclencheurEvenementsStatiques)C2iFactory.GetNewObjetForSession("CDeclencheurEvenementsStatiques", typeof(IDeclencheurEvenementsStatiques), objet.ContexteDonnee.IdSession);
                if (declencheur != null)
                    result = declencheur.DeclencheEvenementStatiques(objet.GetType(), objet.DbKey);
                if (!result)
                    return result;
            }
            return result;
        }

        /// ///////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {

            base.MyDraw(ctx);
            Graphics g = ctx.Graphic;
            DrawVariableEntree(g, VariableElement);
        }
    }
}
