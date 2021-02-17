using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    [Serializable]
    public abstract class CComposantFiltreSousFiltre : CComposantFiltreFonction
    {
        
        private CComposantFiltreChamp m_champTeste = null;
        private string m_strTableSousFiltre;
        private CComposantFiltreChamp m_champSousFiltre = null;
        private CFiltreDataAvance m_filtre = null;

        private bool m_bIsInterprete = false;

        //--------------------------------------------------------
        public CComposantFiltreSousFiltre()
            : base()
        {
        }

        //--------------------------------------------------------
        public override int GetNbParametresNecessaires()
        {
            return 4;
        }

        //--------------------------------------------------------
        public CComposantFiltreChamp ChampTeste
        {
            get
            {
                return m_champTeste;
            }
        }

        //--------------------------------------------------------
        public string TableSousFiltre
        {
            get
            {
                return m_strTableSousFiltre;
            }
        }

        //--------------------------------------------------------
        public CComposantFiltreChamp ChampSousFiltre
        {
            get
            {
                return m_champSousFiltre;
            }
        }

        //--------------------------------------------------------
        public CFiltreDataAvance Filtre
        {
            get
            {
                return m_filtre;
            }
        }

        //--------------------------------------------------------
        public void InitComposant(CComposantFiltreChamp champTeste,
            string strNomTableSousFiltre,
            CComposantFiltreChamp champDeSousFiltre,
            CFiltreDataAvance filtre)
        {
            m_champTeste = champTeste;
            m_strTableSousFiltre = strNomTableSousFiltre;
            m_champSousFiltre = champDeSousFiltre;
            m_filtre = filtre;
            m_bIsInterprete = true;
        }

        //--------------------------------------------------------
        public CResultAErreur InterpreteParametres()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_bIsInterprete)
                return result;
            //Paramètre 1 : champ
            //Paramètre 2 : sous type
            //Parametre 3 : champ de sous type retourné
            //Paramètre 4 : Filtre
            if (Parametres.Count < 4)
            {
                result.EmpileErreur(GetOperateurSousFiltre() + " " +
                    I.T("Require 4 parameters : field, type, source field, sub filter|20008"));
                return result;
            }

            m_champTeste = Parametres[0] as CComposantFiltreChamp;
            if (m_champTeste == null)
            {
                result.EmpileErreur(GetOperateurSousFiltre() + " : " + I.T("First parameter is invalid (field requiered|20070"));
            }

            Type tp = null;
            m_strTableSousFiltre = null;
            CComposantFiltreConstante compoCst = Parametres[1] as CComposantFiltreConstante;
            if (compoCst != null && compoCst.Valeur != null)
            {
                tp = CActivatorSurChaine.GetType(compoCst.Valeur.ToString(), true);
                if (tp != null)
                {
                    m_strTableSousFiltre = CContexteDonnee.GetNomTableForType(tp);
                }
                if (m_strTableSousFiltre == null)
                {
                    result.EmpileErreur(I.T("Invalid type reference (@1)|20009", compoCst.Valeur.ToString()));
                }
            }
            else
                result.EmpileErreur(GetOperateurSousFiltre() + " : " + I.T("Second parameter is invalid (type requiered)|20010"));

            if (result)
            {
                CComposantFiltreConstante compoFiltre = Parametres[3] as CComposantFiltreConstante;
                if (compoFiltre == null || compoFiltre.Valeur == null)
                {
                    result.EmpileErreur(GetOperateurSousFiltre() + " : " + I.T("4th parameter is invalid (filtre requiered)|20012"));
                    return result;
                }
                m_filtre = new CFiltreDataAvance(m_strTableSousFiltre, compoFiltre.Valeur.ToString());
                if (m_filtre.ComposantPrincipal == null)
                {
                    result.EmpileErreur(I.T("Sub filter is invalide|20014"));
                    return result;
                }
            }
            if (result)
            {
                CComposantFiltreConstante compoSousChamp = Parametres[2] as CComposantFiltreConstante;
                if (compoSousChamp == null || compoSousChamp.Valeur == null)
                {
                    result.EmpileErreur(GetOperateur() + " : " + I.T("3rd parameter is invalide (field requiered)|20013"));
                    return result;
                }
                m_champSousFiltre = new CComposantFiltreChamp(compoSousChamp.Valeur.ToString(), m_strTableSousFiltre);
            }
            m_bIsInterprete = result.Result;

            return result;
        }
            

        //--------------------------------------------------------
        protected abstract string GetOperateurSousFiltre();

        //--------------------------------------------------------
        public override string GetString()
        {
            StringBuilder bl = new StringBuilder();
            foreach (CComposantFiltre parametre in Parametres)
            {
                bl.Append(parametre.GetString());
                bl.Append(";");
            }
            if (bl.Length > 0)
                bl.Remove(bl.Length - 1, 1);
            bl.Insert(0, "(");
            bl.Insert(0, GetOperateurSousFiltre());
            bl.Append(")");
            return bl.ToString();
        }

        //--------------------------------------------------------
        public override CResultAErreur VerifieParametres()
        {
            return InterpreteParametres();
            

            
        }
    }

    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    [Serializable]
    public class CComposantFiltreInSousFiltre : CComposantFiltreSousFiltre
    {
        //-----------------------------------------------------------------
        public override COperateurAnalysable GetOperateur()
        {
            return new COperateurAnalysable(0, "InSub", "INSUB", false);
        }

        //-----------------------------------------------------------------
        protected override string GetOperateurSousFiltre()
        {
            return "InSub";
        }
    }

    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    [Serializable]
    public class CComposantFiltreNotInSousFiltre : CComposantFiltreSousFiltre
    {
        //-----------------------------------------------------------------
        public override COperateurAnalysable GetOperateur()
        {
            return new COperateurAnalysable(0, "NotInSub", "NOTINSUB", false);
        }

        //-----------------------------------------------------------------
        protected override string GetOperateurSousFiltre()
        {
            return "NotInSub";
        }
    }

}
