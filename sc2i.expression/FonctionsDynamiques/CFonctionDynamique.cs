using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.expression.FonctionsDynamiques
{
    [Serializable]
    public class CFonctionDynamique : IFournisseurProprietesDynamiquesAVariablesDeFormule, I2iSerializable
    {
        private List<CParametreFonctionDynamique> m_listeParametres = new List<CParametreFonctionDynamique>();
        private string m_strNom = "";
        private C2iExpression m_formule = null;
        private string m_strFonctionId = "";

        private IFournisseurProprietesDynamiques m_fournisseurPrincipal = null;

        //----------------------------------------------------------------------
        public CFonctionDynamique()
        {
            m_strFonctionId = Guid.NewGuid().ToString();
        }

        //----------------------------------------------------------------------
        public CFonctionDynamique(string strId)
        {
            m_strFonctionId = strId;
        }

        //----------------------------------------------------------------------
        public string IdFonction
        {
            get
            {
                return m_strFonctionId;
            }
        }

        //----------------------------------------------------------------------
        public string Nom
        {
            get
            {
                return m_strNom;
            }
            set
            {
                m_strNom = value;
            }
        }

        //----------------------------------------------------------------------
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

        //----------------------------------------------------------------------
        public CTypeResultatExpression TypeRetourne
        {
            get
            {
                if (Formule != null)
                    return Formule.TypeDonnee;
                return new CTypeResultatExpression(typeof(object), false);
            }
        }

        //----------------------------------------------------------------------
        public IEnumerable<CParametreFonctionDynamique> Parametres
        {
            get
            {
                return m_listeParametres.AsReadOnly();
            }
            set
            {
                List<CParametreFonctionDynamique> lstParametres = new List<CParametreFonctionDynamique>();
                if (value != null)
                {
                    int nNumParametre = 0;
                    foreach (CParametreFonctionDynamique p in value)
                    {
                        p.NumParametre = nNumParametre++;
                        lstParametres.Add(p);
                    }
                }
                m_listeParametres = lstParametres;
            }
        }

        //----------------------------------------------------------------------
        public CDefinitionProprieteDynamiqueVariableFormule[] VariablesDeFormule
        {
            get
            {
                List<CDefinitionProprieteDynamiqueVariableFormule> lstDefs = new List<CDefinitionProprieteDynamiqueVariableFormule>();
                foreach (CParametreFonctionDynamique parametre in Parametres)
                {
                    CDefinitionProprieteDynamiqueVariableFormule def = new CDefinitionProprieteDynamiqueVariableFormule(
                        parametre.Nom,
                        parametre.TypeResultatExpression,
                        true);
                    lstDefs.Add(def);
                }
                return lstDefs.ToArray();
            }
        }

        //----------------------------------------------------------------------
        public CResultAErreur Eval(CContexteEvaluationExpression ctxEval, params object[] parametres)
        {
            if (Formule == null)
                return null;
            int nParametre = 0;
            CContexteEvaluationExpression ctxCopie = new CContexteEvaluationExpression(ctxEval.ObjetSource);
            foreach (CParametreFonctionDynamique parametre in Parametres)
            {
                CDefinitionProprieteDynamiqueVariableFormule def = new CDefinitionProprieteDynamiqueVariableFormule(
                    parametre.Nom,
                    parametre.TypeResultatExpression,
                    true);
                ctxCopie.AddVariable(def);
                if (nParametre < parametres.Length)
                    ctxCopie.SetValeurVariable(def, parametres[nParametre]);
                else
                    ctxCopie.SetValeurVariable(def, null);
                nParametre++;
            }
            CResultAErreur result = Formule.Eval(ctxCopie);
            if (!result)
                result.EmpileErreur(I.T("Error while evaluating method @1|20127"));
            return result;            
        }


        //----------------------------------------------------------------------------
        private IFournisseurProprietesDynamiques FournisseurPrincipal
        {
            get{
                if ( m_fournisseurPrincipal == null )
                    return new CFournisseurGeneriqueProprietesDynamiques();
                return m_fournisseurPrincipal;
            }
            set{m_fournisseurPrincipal = value;
            }
        }

        //----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
        {
            return GetDefinitionsChamps(new CObjetPourSousProprietes(typeInterroge), null);
        }

        //----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
        {
            return GetDefinitionsChamps(new CObjetPourSousProprietes(typeInterroge), defParente);
        }

        //----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
        {
            return GetDefinitionsChamps(objet, null);            
        }

        //----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if (FournisseurPrincipal != null)
                lst.AddRange(FournisseurPrincipal.GetDefinitionsChamps(objet, defParente));
            foreach (CParametreFonctionDynamique parametre in Parametres)
            {
                CDefinitionProprieteDynamiqueVariableFormule def = new CDefinitionProprieteDynamiqueVariableFormule(
                    parametre.Nom,
                    parametre.TypeResultatExpression,
                    true);
                def.Rubrique = I.T("Parameters|20128");
                lst.Add(def);
            }
            return lst.ToArray();            
        }


        //----------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strNom );
            serializer.TraiteString ( ref m_strFonctionId );
            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if ( !result )
                return result;
            result = serializer.TraiteListe<CParametreFonctionDynamique>(m_listeParametres);
            if ( !result )
                return result;

            return result;
            
        }

    }
}
