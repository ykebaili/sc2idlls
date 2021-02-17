using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    public enum ECAMLComparaison
    {
        BeginsWith,
        Contains,
        Eq,
        Geq,
        Gt,
        Leq,
        Lt,
        Neq
    }

    //--------------------------------------------
    public class CCAMLOperateurComparaison : CEnumALibelle<ECAMLComparaison>
    {
        //--------------------------------------------
        public CCAMLOperateurComparaison(ECAMLComparaison operateur)
            :base (operateur)
        {
        }

        //--------------------------------------------
        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case ECAMLComparaison.BeginsWith:
                        return "StartWith";
                    case ECAMLComparaison.Contains:
                        return "Contains";
                    case ECAMLComparaison.Eq:
                        return "=";
                    case ECAMLComparaison.Geq:
                        return ">=";
                    case ECAMLComparaison.Gt:
                        return ">";
                    case ECAMLComparaison.Leq:
                        return "<=";
                    case ECAMLComparaison.Lt:
                        return "<";
                    case ECAMLComparaison.Neq:
                        return "<>";
                }
                return "?";
            }
        }
    }

    //--------------------------------------------
    [Serializable]
    public class CCAMLItemComparaison : CCAMLItem
    {
        private ECAMLComparaison m_operateur = ECAMLComparaison.Eq;
        private CCAMLItemField m_champ = null;
        private C2iExpression m_valeur = null;

        //------------------------------------------
        public CCAMLItemComparaison()
        {
        }

        //------------------------------------------
        public CCAMLItemComparaison(ECAMLComparaison operateur)
        {
            m_operateur = operateur;
        }

        //------------------------------------------
        public ECAMLComparaison Operateur
        {
            get
            {
                return m_operateur;
            }
            set
            {
                m_operateur = value;
            }
        }

        //------------------------------------------
        public CCAMLItemField Field
        {
            get
            {
                return m_champ;
            }
            set
            {
                m_champ = value;
            }
        }

        //------------------------------------------
        public C2iExpression Valeur
        {
            get
            {
                return m_valeur;
            }
            set
            {
                m_valeur = value;
            }

        }



        //------------------------------------------
        public override string Libelle
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                bl.Append(m_champ == null?"? ":m_champ.Libelle);
                bl.Append(" ");
                bl.Append( new CCAMLOperateurComparaison(m_operateur).Libelle);
                bl.Append(" ");
                bl.Append(m_valeur != null?m_valeur.GetString():"?");
                return bl.ToString();
            }
        }

        //------------------------------------------
        protected string BaliseOperateur
        {
            get
            {
                switch (m_operateur)
                {
                    case ECAMLComparaison.BeginsWith:
                        return "BeginsWith";
                    case ECAMLComparaison.Contains:
                        return "Contains";
                    case ECAMLComparaison.Eq:
                        return "Eq";
                    case ECAMLComparaison.Geq:
                        return "Geq";
                    case ECAMLComparaison.Gt:
                        return "Gt";
                    case ECAMLComparaison.Leq:
                        return "Leq";
                    case ECAMLComparaison.Lt:
                        return "Lt";
                    case ECAMLComparaison.Neq:
                        return "Neq";
                }
                return "";
            }
        }

        //------------------------------------------
        protected override void MyGetXmlText(CEasyQuery query, StringBuilder bl, int nIndent)
        {
            if (BaliseOperateur.Length > 0 && m_champ != null)
            {
                AddIndent(bl, nIndent);
                bl.Append("<");
                bl.Append(BaliseOperateur);
                bl.Append(">");

                bl.Append(Environment.NewLine);
                m_champ.GetXmlText(query, bl, nIndent + 1);
                bl.Append(Environment.NewLine);
                bl.Append("<Value Type='Text'>");
                if (m_valeur != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(query);
                    CResultAErreur result = m_valeur.Eval(ctx);
                    if (result && result.Data != null)
                        bl.Append(result.Data.ToString());
                }
                bl.Append("</Value>");
                bl.Append(Environment.NewLine);
                bl.Append("</");
                bl.Append(BaliseOperateur);
                bl.Append(">");
                bl.Append(Environment.NewLine);
            }
        }

        //------------------------------------------
        protected override void MyGetRowFilter(CEasyQuery query, StringBuilder bl)
        {
            if (BaliseOperateur.Length > 0 && m_champ != null)
            {
                m_champ.GetRowFilter(query, bl);
                string strPrefix = "";
                string strSuffixe = "";
                switch (m_operateur)
                {
                    case ECAMLComparaison.BeginsWith:
                        bl.Append("like ");
                        strSuffixe = "%";
                        break;
                    case ECAMLComparaison.Contains:
                        bl.Append("like ");
                        strPrefix = "%";
                        strSuffixe = "%";
                        break;
                    case ECAMLComparaison.Eq:
                        bl.Append("=");
                        break;
                    case ECAMLComparaison.Geq:
                        bl.Append(">=");
                        break;
                    case ECAMLComparaison.Gt:
                        bl.Append(">");
                        break;
                    case ECAMLComparaison.Leq:
                        bl.Append("<=");
                        break;
                    case ECAMLComparaison.Lt:
                        bl.Append("<");
                        break;
                    case ECAMLComparaison.Neq:
                        bl.Append("<>");
                        break;
                    default:
                        break;
                }
                bl.Append("'");
                bl.Append(strPrefix);
                if (m_valeur != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(query);
                    CResultAErreur result = m_valeur.Eval(ctx);
                    if (result && result.Data != null)
                        bl.Append(result.Data.ToString());
                }
                bl.Append(strSuffixe);
                bl.Append("'");

            }
        }

        //--------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            int nOp = (int)m_operateur;
            serializer.TraiteInt(ref nOp);
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_operateur = (ECAMLComparaison)nOp;
            result = serializer.TraiteObject<CCAMLItemField>(ref m_champ);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_valeur);
            return result;
        }
    }
}