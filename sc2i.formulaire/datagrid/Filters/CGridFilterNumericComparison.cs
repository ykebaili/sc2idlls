using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public abstract class CGridFilterNumericComparison : CGridFilterForWndDataGrid
    {
        private double? m_fRefValue = null;

        protected enum EEOperateurComparaison
        {
            Egal,
            Supérieur,
            SupérieurOuEgal,
            Inférieur,
            InférieurOuEgal,
            Différent,
            Autre = 1000
        }

        //-----------------------------------------
        public double? ReferenceValue
        {
            get
            {
                return m_fRefValue;
            }
            set
            {
                m_fRefValue = value;
            }
        }

        protected abstract EEOperateurComparaison Operateur {get;}

        //-----------------------------------------
        public override bool IsValueIn(object valeur)
        {
            if (m_fRefValue == null)
                return true;
            try
            {
                double fVal = Convert.ToDouble(valeur);
                switch (Operateur)
                {
                    case EEOperateurComparaison.Egal:
                        return fVal == m_fRefValue.Value;
                        break;
                    case EEOperateurComparaison.Supérieur:
                        return fVal > m_fRefValue.Value;
                        break;
                    case EEOperateurComparaison.SupérieurOuEgal:
                        return fVal >= m_fRefValue.Value;
                        break;
                    case EEOperateurComparaison.Inférieur:
                        return fVal < m_fRefValue.Value;
                        break;
                    case EEOperateurComparaison.InférieurOuEgal:
                        return fVal <= m_fRefValue.Value;
                        break;
                    case EEOperateurComparaison.Différent:
                        return fVal != m_fRefValue.Value;
                        break;
                    default:
                        break;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        

        public static IEnumerable<CGridFilterForWndDataGrid> GetFiltresNumeriques()
        {
            return new CGridFilterForWndDataGrid[]
            {
                new CGridFilterNumericEgal(),
                new CGridFilterNumericDifferent(),
                new CGridFilterNumericSuperieur(),
                new CGridFilterNumericSuperieurOuEgal(),
                new CGridFilterNumericInferieur(),
                new CGridFilterNumericInferieurOuEgal(),
                new CGridFilterNumericEntre(),
                new CGridFilterNotSet()
            };
        }
    }


    //------------------------------------------------------------------------
    public class CGridFilterNumericEgal : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.Egal; }
        }

        public override string Label
        {
            get { return I.T("Equals to|20035"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericDifferent : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.Différent; }
        }

        public override string Label
        {
            get { return I.T("Not equals to|20036"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericSuperieur : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.Supérieur; }
        }

        public override string Label
        {
            get { return I.T("More than|20037"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericSuperieurOuEgal : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.SupérieurOuEgal; }
        }

        public override string Label
        {
            get { return I.T("Greater or equals to|20038"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericInferieur : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.Inférieur; }
        }

        public override string Label
        {
            get { return I.T("Less than|20039"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericInferieurOuEgal : CGridFilterNumericComparison
    {
        protected override CGridFilterNumericComparison.EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.InférieurOuEgal; }
        }

        public override string Label
        {
            get { return I.T("Less or equals to|20040"); }
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterNumericEntre : CGridFilterNumericComparison
    {
        private double? m_fValeurFin = null;

        public override string Label
        {
            get { return I.T("Between|20051"); }
        }

        public double? ValeurFin
        {
            get
            {
                return m_fValeurFin;
            }
            set
            {
                m_fValeurFin = value;
            }
        }

        protected override EEOperateurComparaison Operateur
        {
            get { return EEOperateurComparaison.Autre; }
        }

        

        public override bool IsValueIn(object valeur)
        {
            try
            {
                double fVal = Convert.ToDouble(valeur);
                if (ReferenceValue != null && fVal < ReferenceValue.Value)
                    return false;
                if (m_fValeurFin != null && fVal > m_fValeurFin.Value)
                    return false;
                return true;
            }
            catch { }
            return false;
        }
    }
}
