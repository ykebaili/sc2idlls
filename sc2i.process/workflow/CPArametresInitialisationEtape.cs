using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.process.workflow
{
    public class CParametresInitialisationEtape : I2iSerializable
    {
        private CParametresAffectationEtape m_affectations = new CParametresAffectationEtape();
        private C2iExpression m_formuleInitialisation = null;

        private bool m_bRecalculerLesAffectationsSurRedemarrage = false;
        private bool m_bReevaluerFormuleInitialisationSurRedemarrage = false;

        //--------------------------------------------------
        public CParametresInitialisationEtape()
        {
        }

        //---------------------------------------------------------
        [DynamicField("Initialisation Formula string")]
        public string FormuleInitialisationString
        {
            get
            {
                if (m_formuleInitialisation != null)
                    return m_formuleInitialisation.GetString();
                return "";
            }
        }

        //--------------------------------------------------
        public C2iExpression FormuleInitialisation
        {
            get
            {
                return m_formuleInitialisation;
            }
            set
            {
                m_formuleInitialisation = value;
            }
        }

        //--------------------------------------------------
        [DynamicField("Assignments")]
        public CParametresAffectationEtape Affectations
        {
            get
            {
                return m_affectations;
            }
            set
            {
                m_affectations = value;
            }
        }

        //--------------------------------------------------
        [DynamicField("Reevaluate Initialisation on restart")]
        public bool ReevaluerInitialisationSurRedemarrage
        {
            get
            {
                return m_bReevaluerFormuleInitialisationSurRedemarrage;
            }
            set
            {
                m_bReevaluerFormuleInitialisationSurRedemarrage = value;
            }
        }


        //--------------------------------------------------
        [DynamicField("Recalculate Assignments on restart")]
        public bool RecalculerAffectationsSurRedemarrage
        {
            get
            {
                return m_bRecalculerLesAffectationsSurRedemarrage;
            }
            set
            {
                m_bRecalculerLesAffectationsSurRedemarrage = value;
            }
        }



        //--------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : Recalculer les affectations et reevaluer l'initialisation sur redémarrage
        }

        //--------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleInitialisation);
            if (result)
                result = serializer.TraiteObject<CParametresAffectationEtape>(ref m_affectations);
            if (!result)
                return result;
            if (nVersion >= 1)
            {
                serializer.TraiteBool(ref m_bRecalculerLesAffectationsSurRedemarrage);
                serializer.TraiteBool(ref m_bReevaluerFormuleInitialisationSurRedemarrage);
            }
            return result;
        }



        public CResultAErreur AppliqueTo(CEtapeWorkflow etape, bool bIsRedemarrage)
        {
            CResultAErreur result = CResultAErreur.True;
            if (!bIsRedemarrage || RecalculerAffectationsSurRedemarrage)
            {
                CResultAErreurType<CAffectationsEtapeWorkflow> resAff = Affectations.CalculeAffectations(etape.Workflow);
                if (!resAff)
                {
                    result.EmpileErreur(resAff.Erreur);
                    return result;
                }
                etape.Affectations = resAff.DataType;
            }

            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(etape);
            if (FormuleInitialisation != null)
            {
                if ( !bIsRedemarrage || ReevaluerInitialisationSurRedemarrage )
                    result = FormuleInitialisation.Eval(ctx);
            }
            return result;
        }

        public override string ToString()
        {
            return "Setup";
        }
    }
}
