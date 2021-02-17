using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public abstract class CGridFilterDateComparison : CGridFilterForWndDataGrid
    {
        private DateTime? m_refValue = null;

        public DateTime? ReferenceValue
        {
            get
            {
                return m_refValue;
            }
            set
            {
                m_refValue = value;
            }
        }

        

        public static IEnumerable<CGridFilterForWndDataGrid> GetFiltresDate()
        {
            return new CGridFilterForWndDataGrid[]{
                new CGridFilterDateEgal(),
                new CGridFilterDateDifferent(),
                new CGridFilterDateSuperieur(),
                new CGridFilterDateSuperieurOuEgal(),
                new CGridFilterDateInférieur(),
                new CGridFilterDateInférieurOuEgal(),
                new CGridFilterDateEntre(),
                new CGridFilterNotSet()
            };
        }

        
    }


    //------------------------------------------------------------------------
    public class CGridFilterDateEgal : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Equals to|20035"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date == ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateDifferent : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Not equals to|20036"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date != ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateSuperieur : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Later than|20047"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date > ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateSuperieurOuEgal : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Later or equals to|20048"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date >= ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateInférieur : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Prior to|20049"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date < ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateInférieurOuEgal : CGridFilterDateComparison
    {
        public override string Label
        {
            get { return I.T("Prior or equals to|20050"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null && ReferenceValue != null)
                return dt.Value.Date <= ReferenceValue.Value.Date;
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterDateEntre : CGridFilterDateComparison
    {
        private DateTime? m_dateFin = null;

        public DateTime? DateFin
        {
            get
            {
                return m_dateFin;
            }
            set
            {
                m_dateFin = value;
            }
        }

        public override string Label
        {
            get { return I.T("Between|20051"); }
        }

        public override bool IsValueIn(object valeur)
        {
            DateTime? dt = valeur as DateTime?;
            if (dt != null)
            {
                if (ReferenceValue != null && dt.Value.Date < ReferenceValue.Value.Date)
                    return false;
                if (DateFin != null && dt.Value.Date > DateFin.Value.Date)
                    return false;
                return true;
            }
            return false;
        }
    }

        

}
