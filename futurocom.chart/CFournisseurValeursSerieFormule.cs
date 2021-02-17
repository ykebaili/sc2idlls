using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;
using sc2i.expression.datatable;
using sc2i.expression;
using System.Collections;
using System.ComponentModel;

namespace futurocom.chart
{
    [AutoExec("Autoexec")]
    public class CFournisseurValeursSerieFormule : IFournisseurValeursSerie
    {
        private C2iExpression m_formule = null;
        private string m_strIdSource = "";
        private bool m_bEvaluateOnEachSourceElement = false;

        //-------------------------------------------------------------------
        public CFournisseurValeursSerieFormule()
        {
        }

        //-------------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireFournisseursValeursSerie.RegisterFournisseur(typeof(CFournisseurValeursSerieFormule));
        }

        //-------------------------------------------------------------------
        [Browsable(false)]
        public string LabelType
        {
            get
            {
                return I.T("Formula|20002");
            }
        }

        //-------------------------------------------------------------------
        [Browsable(false)]
        public string SourceId
        {
            get
            {
                return m_strIdSource;
            }
            set
            {
                m_strIdSource = value;
            }
        }

        //-------------------------------------------------------------------
        public string GetLabel(CDonneesDeChart donnees)
        {
            if (m_formule != null)
                return m_formule.GetString();
            return LabelType;
        }

        //-------------------------------------------------------------------
        public C2iExpression Formule
        {
            get
            {
                return m_formule;
            }
            set
            {
                m_formule = value;
            }
        }

        //-------------------------------------------------------------------
        public bool EvaluateOnEachSourceElement
        {
            get
            {
                return m_bEvaluateOnEachSourceElement;
            }
            set
            {
                m_bEvaluateOnEachSourceElement = value;
            }
        }

        //-------------------------------------------------------------------
        public object[] GetValues(CChartSetup chart)
        {
            List<object> lstRetour = new List<object>();
            CParametreSourceChart p = chart.ParametresDonnees.GetSourceFV(m_strIdSource);
            if (p != null && m_formule != null)
            {
                object valeur = p.GetSource(chart);
                IEnumerable valEnum = valeur as IEnumerable;
                if (valEnum is IEnumerable && EvaluateOnEachSourceElement)
                {
                    foreach (object o in valEnum)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(o);
                        CResultAErreur result = m_formule.Eval(ctx);
                        if (result)
                            lstRetour.Add(result.Data);
                    }
                }
                else
                {

                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(valeur);
                    CResultAErreur result = m_formule.Eval(ctx);

                    IEnumerable retour = result.Data as IEnumerable;
                    if (retour != null)
                        foreach (object obj in retour)
                            lstRetour.Add(obj);
                }
            
            }
            return lstRetour.ToArray();
        }

        //-------------------------------------------------------------------
        public bool IsApplicableToSource(CParametreSourceChart parametre)
        {
            return true;
        }             


        //-------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------------------
        public CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString(ref m_strIdSource);
            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if (!result)
                return result;
            serializer.TraiteBool(ref m_bEvaluateOnEachSourceElement);
            return result;
        }


    }
}
