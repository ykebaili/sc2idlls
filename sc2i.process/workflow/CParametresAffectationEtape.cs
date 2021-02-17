using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Collections;

namespace sc2i.process.workflow
{
    [Serializable]
    public class CParametresAffectationEtape : I2iSerializable
    {
        private List<CFormuleNommee> m_listeFormules = new List<CFormuleNommee>();

        public CParametresAffectationEtape()
        {
        }

        [DynamicField("Formulas list")]
        public CFormuleNommee[] FormulasList
        {
            get
            {
                return Formules.ToArray();
            }
        }

        public IEnumerable<CFormuleNommee> Formules
        {
            get{return m_listeFormules;
            }
            set{m_listeFormules = new List<CFormuleNommee>();
                if ( value != null )
                    m_listeFormules.AddRange ( value );
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = serializer.TraiteListe<CFormuleNommee> (m_listeFormules);
            if ( !result )
                return result;

            return result;
        }

        public CResultAErreurType<CAffectationsEtapeWorkflow> CalculeAffectations(CWorkflow workflow)
        {
            CResultAErreurType<CAffectationsEtapeWorkflow> resultAff = new CResultAErreurType<CAffectationsEtapeWorkflow>();
            resultAff.Result = true;

            CAffectationsEtapeWorkflow affectations = new CAffectationsEtapeWorkflow();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(workflow);
            foreach (CFormuleNommee formule in Formules)
            {
                CResultAErreur result = formule.Formule.Eval(ctx);
                if (result)
                {
                    if (result.Data is IAffectableAEtape)
                        affectations.AddAffectable(result.Data as IAffectableAEtape);
                    else if (result.Data is IEnumerable)
                    {
                        foreach (object obj in (IEnumerable)result.Data)
                        {
                            IAffectableAEtape a = obj as IAffectableAEtape;
                            if (a != null)
                                affectations.AddAffectable(a);
                        }
                    }
                }
            }
            resultAff.DataType = affectations;
            return resultAff;
        }

        public void AddFormules(IEnumerable<CFormuleNommee> formules)
        {
            foreach ( CFormuleNommee formule in formules )
            {
                CFormuleNommee existante = m_listeFormules.FirstOrDefault(e => e.Libelle == formule.Libelle);
                if (formule.Libelle.Trim() == "" || existante == null )
                    m_listeFormules.Add(formule);
                else if (existante != null)
                {
                    existante.Formule = formule.Formule;
                }
            }
        }
    }
}
