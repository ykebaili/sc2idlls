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
    /// Description résumée de CActionDissocierComportement.
    /// </summary>
    [AutoExec("Autoexec")]
    public class CActionDissocierComportement : CActionLienSortantSimple
    {
        private C2iExpression m_expressionElementADissocier;
        private CDbKey m_dbKeyComportement = null;

        /// /////////////////////////////////////////
        public CActionDissocierComportement(CProcess process)
            : base(process)
        {
            Libelle = I.T("Dissociate a behavior|164");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Dissociate a behavior|164"),
                I.T("Allows to dissociate a behavior of an element|165"),
                typeof(CActionDissocierComportement),
                CGestionnaireActionsDisponibles.c_categorieComportement);
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            AddIdVariablesExpressionToHashtable(m_expressionElementADissocier, table);
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
            return 1; // Passage de Ids en DbKey
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

            //TESTDBKEYOK
            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyComportement, typeof(CComportementGenerique));
            else
                serializer.TraiteDbKey(ref m_dbKeyComportement);

            I2iSerializable objet = m_expressionElementADissocier;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionElementADissocier = (C2iExpression)objet;


            return result;
        }

        /// ////////////////////////////////////////////////////////
        [ExternalReferencedEntityDbKey(typeof(CComportementGenerique))]
        public CDbKey DbKeyComportement
        {
            get
            {
                return m_dbKeyComportement;
            }
            set
            {
                m_dbKeyComportement = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression FormuleElementADissocier
        {
            get
            {
                return m_expressionElementADissocier;
            }
            set
            {
                m_expressionElementADissocier = value;
            }
        }




        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_expressionElementADissocier == null)
            {
                result.EmpileErreur(I.T("The element to be dissociated formula is incorrect|166"));
            }
            else
            {
                CTypeResultatExpression typeRes = m_expressionElementADissocier.TypeDonnee;
                if (!(typeRes.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique))))
                {
                    result.EmpileErreur(I.T("The element to be dissociated formula must be return a work entity|167"));
                }
            }

            //Vérifie que le comportement existe
            CComportementGenerique comportement = new CComportementGenerique(Process.ContexteDonnee);
            if (!comportement.ReadIfExists(m_dbKeyComportement))
                result.EmpileErreur(I.T("The behavior to be dissociated is incorrect|168"));
            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;

            CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
            contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
            if (m_expressionElementADissocier == null)
            {
                result.EmpileErreur(I.T("No element to be dissociated|169"));
                return result;
            }
            result = m_expressionElementADissocier.Eval(contexteEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Error in the elements to be dissociated formula|170"));
                return result;
            }
            if (result.Data != null)
            {
                if (!(result.Data is CObjetDonneeAIdNumerique))
                {
                    result.EmpileErreur(I.T("The element to be dissociated formula must be return a work entity|167"));
                    return result;
                }
                CComportementGenerique comportement = new CComportementGenerique(contexte.ContexteDonnee);
                if (!comportement.ReadIfExists(m_dbKeyComportement))
                {
                    return result;
                }
                comportement.RemoveComportementFromObjet((CObjetDonneeAIdNumerique)result.Data);
            }
            return result;
        }

        /// ///////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
        }

    }
}