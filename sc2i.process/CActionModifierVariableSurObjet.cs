using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.process
{
    /// <summary>
    /// Description résumée de CActionModifierVariableSurObjet.
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionModifierVariableSurObjet : CActionLienSortantSimple
    {
        /* TESTDBKEYOK (XL) */

        private string m_strIdVariableAModifier = "";
        private int m_nIdVariableSurObjet = -1;
        private C2iExpression m_expressionValeur = null;

        /// /////////////////////////////////////////
        public CActionModifierVariableSurObjet(CProcess process)
            : base(process)
        {
            Libelle = I.T("Modify a variable on an element|229");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Modify a variable on an element|229"),
                I.T("Allows to modify a variable on an element|230"),
                typeof(CActionModifierVariableSurObjet),
                CGestionnaireActionsDisponibles.c_categorieDonnees);
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            table[m_strIdVariableAModifier] = true;
            AddIdVariablesExpressionToHashtable(m_expressionValeur, table);
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
            return 1; // Passage de Int IdVariableaMofier en String
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
                m_strIdVariableAModifier = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableAModifier);

            I2iSerializable objet = m_expressionValeur;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionValeur = (C2iExpression)objet;

            serializer.TraiteInt(ref m_nIdVariableSurObjet);

            return result;
        }

        /// ////////////////////////////////////////////////////////
        public string IdVariableAModifier
        {
            get
            {
                return m_strIdVariableAModifier;
            }
            set
            {
                m_strIdVariableAModifier = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableAModifier
        {
            get
            {
                return Process.GetVariable(IdVariableAModifier);
            }
            set
            {
                if (value == null)
                    m_strIdVariableAModifier = "";
                else
                    m_strIdVariableAModifier = value.IdVariable;
            }
        }

        /// ////////////////////////////////////////////////////////
        public int IdVariableSurObjet
        {
            get
            {
                return m_nIdVariableSurObjet;
            }
            set
            {
                m_nIdVariableSurObjet = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression ExpressionValeur
        {
            get
            {
                if (m_expressionValeur == null)
                    return new C2iExpressionConstante("");
                return m_expressionValeur;
            }
            set
            {
                m_expressionValeur = value;
            }
        }


        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;

            if (VariableAModifier == null)
            {
                result.EmpileErreur(I.T("The variable to be modified isn't defined or doesn't exist|215"));
            }
            else if (!VariableAModifier.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique)))
                result.EmpileErreur(I.T("The variable to be modified dosn't contain an correct type of element|231"));

            CVariableSurObjet variable = new CVariableSurObjet(Process.ContexteDonnee);
            if (!variable.ReadIfExists(m_nIdVariableSurObjet))
                result.EmpileErreur(I.T("The variable on object doesn't exist|232"));



            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;
            object elementToModif = Process.GetValeurChamp(VariableAModifier.IdVariable);
            if (elementToModif == null)
                return result;
            if (elementToModif is CObjetDonneeAIdNumerique)
            {
                //Calcule la nouvelle valeur
                CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
                contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
                result = ExpressionValeur.Eval(contexteEval);
                if (!result)
                {
                    result.EmpileErreur(I.T("Error during @1 formula evaluation|216", ExpressionValeur.ToString()));
                    return result;
                }
                string strNouvelleValeur = result.Data == null ? "" : result.Data.ToString();

                //Trouve la variable
                CVariableSurObjet variable = new CVariableSurObjet(contexte.ContexteDonnee);
                if (!variable.ReadIfExists(m_nIdVariableSurObjet))
                    result.EmpileErreur(I.T("The variable doesn't exist|233"));
                else
                    CValeurVariableSurObjet.SetValeur(
                        variable.Nom,
                        (CObjetDonneeAIdNumerique)elementToModif,
                        strNouvelleValeur);
            }
            return result;
        }

        /// ///////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            Graphics g = ctx.Graphic;
            DrawVariableEntree(g, VariableAModifier);
        }
    }
}
