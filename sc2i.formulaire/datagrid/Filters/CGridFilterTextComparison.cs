using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public abstract class CGridFilterTextComparison : CGridFilterForWndDataGrid
    {
        private string m_fRefValue = "";

        public string ReferenceValue
        {
            get{return m_fRefValue;
            }
            set{
                m_fRefValue = value;
            }
        }

       

        public static IEnumerable<CGridFilterForWndDataGrid> GetFiltresTexte()
        {
            return new CGridFilterForWndDataGrid[]{
                new CGridFilterTextEgal(),
                new CGridFilterTextDifferent(),
                new CGridFilterTextCommencePar(),
                new CGridFilterTextNeCommencePasPar(),
                new CGridFilterTextTerminePar(),
                new CGridFilterTextNeTerminePasPar(),
                new CGridFilterTextContient(),
                new CGridFilterTextNeContientPas(),
                new CGridFilterNotSet()};
        }
        
    }


    //------------------------------------------------------------------------
    public class CGridFilterTextEgal : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Equals to|20035"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return valeur.ToString().Equals(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextDifferent : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Not equals to|20036"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return !valeur.ToString().Equals(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextCommencePar : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Starts with|20041"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return valeur.ToString().StartsWith(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextNeCommencePasPar : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Doesn't starts with|20042"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return !valeur.ToString().StartsWith(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextTerminePar : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Ends with|20043"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return valeur.ToString().EndsWith(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextNeTerminePasPar : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Doesn't ends with|20044"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return !valeur.ToString().EndsWith(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextContient : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Contains|20045"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return valeur.ToString().Contains(ReferenceValue);
            return false;
        }
    }

    //------------------------------------------------------------------------
    public class CGridFilterTextNeContientPas : CGridFilterTextComparison
    {
        public override string Label
        {
            get { return I.T("Doesn't contains|20046"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null && ReferenceValue != null)
                return !valeur.ToString().Contains(ReferenceValue);
            return false;
        }
    }

    
    

}
