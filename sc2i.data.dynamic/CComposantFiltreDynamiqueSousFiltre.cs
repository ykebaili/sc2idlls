using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
    /// Description résumée de CComposantFiltreDynamiqueSousFiltre.
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueSousFiltre : CComposantFiltreDynamique
	{
		private CDefinitionProprieteDynamique m_champTeste = null;

        private CFiltreDynamique m_sousFiltre = null;
        private bool m_bNotIn = false;
        private CDefinitionProprieteDynamique m_champRetourneParSousFiltre = null;

		private C2iExpression m_expressionConditionApplication = new C2iExpressionVrai();

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamiqueSousFiltre()
		{
			
		}


		/// //////////////////////////////////////////////////
		public override bool AccepteComposantsFils
		{
			get
			{
				return false;
			}
		}

		/// //////////////////////////////////////////////////
		public override CComposantFiltreDynamique[] ListeComposantsFils
		{
			get
			{
				return new CComposantFiltreDynamique[0];
			}
		}

		/// //////////////////////////////////////////////////
		public override bool AddComposantFils ( CComposantFiltreDynamique composant )
		{
			return false;
		}

		/// //////////////////////////////////////////////////
		public override bool InsertComposantFils ( CComposantFiltreDynamique composant, int nIndex )
		{
			return false;
		}


		/// //////////////////////////////////////////////////
		public override int RemoveComposantFils ( CComposantFiltreDynamique composant )
		{
			return 0;
		}

		/// //////////////////////////////////////////////////
		public override string Description
		{
			get
			{
                return I.T("@1 @2 Sub filter|20066",
                    Champ != null ? Champ.Nom : "?",
                    IsNotInTest ? I.T("not in|20065") : I.T("in|20064"));
			}
		}

		/// //////////////////////////////////////////////////
		public CDefinitionProprieteDynamique Champ
		{
			get
			{
				return m_champTeste;
			}
			set
			{
				m_champTeste = value;
			}
		}

        /// //////////////////////////////////////////////////
        public CDefinitionProprieteDynamique ChampRetourneParSousFiltre
        {
            get
            {
                return m_champRetourneParSousFiltre;
            }
            set
            {
                m_champRetourneParSousFiltre = value;
            }
        }

        /// //////////////////////////////////////////////////
        public CFiltreDynamique SousFiltre
        {
            get
            {
                return m_sousFiltre;
            }
            set
            {
                m_sousFiltre = value;
            }
        }

        /// //////////////////////////////////////////////////
        public bool IsNotInTest
        {
            get
            {
                return m_bNotIn;
            }
            set
            {
                m_bNotIn = value;
            }
        }
        


		/// //////////////////////////////////////////////////
		public override void InsereDefinitionToObjetFinal ( CDefinitionProprieteDynamique def )
		{
			if ( Champ != null )
			{
				Champ.InsereParent ( def );
			}
		}

		/// //////////////////////////////////////////////////
		public C2iExpression ConditionApplication
		{
			get
			{
				if ( m_expressionConditionApplication == null )
				{
					m_expressionConditionApplication = new C2iExpressionVrai();
				}
				return m_expressionConditionApplication;
			}
			set
			{
				m_expressionConditionApplication = value;
			}
		}

		/// //////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_champTeste);
            if (result)
                result = serializer.TraiteObject<CFiltreDynamique>(ref m_sousFiltre);
            if (result)
                serializer.TraiteBool(ref m_bNotIn);
            if (result)
                result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_champRetourneParSousFiltre);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionConditionApplication);
			return result;
		}

		/// //////////////////////////////////////////////////
        public override CResultAErreur GetComposantFiltreData(CFiltreDynamique filtre, CFiltreData filtreData)
        {
            CResultAErreur result = CResultAErreur.True;
            //Vérifie que l'expression condition est OK
            CContexteEvaluationExpression contexteEvaluation = new CContexteEvaluationExpression(filtre);
            result = ConditionApplication.Eval(contexteEvaluation);
            if (!result)
            {
                result.EmpileErreur(I.T("Error while evaluation of @1|143", ConditionApplication.GetString()));
                return result;
            }

            if (result.Data == null || !((result.Data is bool && (bool)result.Data) || result.Data.ToString() == "1"))
            {
                result.Data = null;
                return result;
            }

            CComposantFiltreOperateur opPrincipal = null;
            string strNomChamp = m_champTeste.NomPropriete;
            if (m_champTeste is CDefinitionProprieteDynamiqueChampCustom)
            {
                CDbKey dbKeyChamp = ((CDefinitionProprieteDynamiqueChampCustom)m_champTeste).DbKeyChamp;
                strNomChamp = m_champTeste.NomPropriete;
                string strAcces = "";
                //Trouve le nom du champ et le chemin d'accès
                int nPos = strNomChamp.LastIndexOf(".");
                if (nPos != -1)
                {
                    strAcces = strNomChamp.Substring(0, nPos + 1);
                    strNomChamp = strNomChamp.Substring(nPos + 1);
                }
                strAcces += "RelationsChampsCustom.";

                CComposantFiltreOperateur operateurIdChamp = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEgal);
                operateurIdChamp.Parametres.Add(new CComposantFiltreChamp(strAcces + CChampCustom.c_champId, CContexteDonnee.GetNomTableForType(filtre.TypeElements)));
                operateurIdChamp.Parametres.Add(new CComposantFiltreConstante(dbKeyChamp));
                opPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                opPrincipal.Parametres.Add(operateurIdChamp);
                Type typeChamp = m_champTeste.TypeDonnee.TypeDotNetNatif;
                if (typeChamp == typeof(double))
                    strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDouble;
                else if (typeChamp == typeof(int))
                    strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurInt;
                else if (typeChamp == typeof(DateTime) || typeChamp == typeof(CDateTimeEx))
                    strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDate;
                else if (typeChamp == typeof(bool))
                    strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurBool;
                else
                    strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurString;
                strNomChamp = strAcces + strNomChamp;
            }
            CComposantFiltreChamp composantChamp = new CComposantFiltreChamp(strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements));

            CComposantFiltreSousFiltre composantSousFiltre = IsNotInTest ?
                new CComposantFiltreNotInSousFiltre() as CComposantFiltreSousFiltre :
                new CComposantFiltreInSousFiltre() as CComposantFiltreSousFiltre;


            m_sousFiltre.ElementAVariablesExterne = filtre;
            result = m_sousFiltre.GetFiltreData();
            CFiltreDataAvance filtreAvance = null;
            if (result && result.Data is CFiltreDataAvance)
                filtreAvance = result.Data as CFiltreDataAvance;
            else
                return result;
            filtreAvance.RenumerotteParameters(filtreData.Parametres.Count + 1);
            foreach (object parametre in filtreAvance.Parametres)
                filtreData.Parametres.Add(parametre);
            CComposantFiltreChamp champDeSousFiltre = new CComposantFiltreChamp(m_champRetourneParSousFiltre.NomPropriete, filtreAvance.TablePrincipale);

            composantSousFiltre.InitComposant(composantChamp, filtreAvance.TablePrincipale, champDeSousFiltre, filtreAvance);

            composantSousFiltre.Parametres.Add(composantChamp);
            composantSousFiltre.Parametres.Add(new CComposantFiltreConstante(CContexteDonnee.GetTypeForTable(filtreAvance.TablePrincipale).ToString()));
            composantSousFiltre.Parametres.Add(new CComposantFiltreConstante(champDeSousFiltre.ChaineInitiale));
            composantSousFiltre.Parametres.Add(new CComposantFiltreConstante(filtreAvance.ComposantPrincipal.GetString()));

            CComposantFiltre composantRecherche = composantSousFiltre;

            if (opPrincipal != null)
            {
                opPrincipal.Parametres.Add(composantRecherche);
                composantRecherche = opPrincipal;
            }


            result.Data = composantRecherche;
            return result;
        }

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieIntegrite( CFiltreDynamique filtre)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_champTeste == null )
				result.EmpileErreur(I.T("Field not defined|144"));
			return result;
		}

		/// //////////////////////////////////////////////////
		protected bool DoesExpressionUtiliseVariable ( C2iExpression expression, CVariableDynamique variable )
		{
			if ( expression == null )
				return false;
			foreach ( C2iExpressionChamp exp in expression.ExtractExpressionsType(typeof(C2iExpressionChamp) )   )
			{
				if ( exp.DefinitionPropriete is CDefinitionProprieteDynamiqueVariableDynamique )
				{
					CDefinitionProprieteDynamiqueVariableDynamique def = (CDefinitionProprieteDynamiqueVariableDynamique)exp.DefinitionPropriete;
                    if (def.IdChamp == variable.IdVariable)
                        return true;
				}
			}
			return false;
		}

		/// //////////////////////////////////////////////////
		public override bool IsVariableUtilisee ( CVariableDynamique variable )
		{
			if ( DoesExpressionUtiliseVariable ( m_expressionConditionApplication, variable ) )
				return true;
			return false;
			
		}

		/// //////////////////////////////////////////////////
		protected void OnChangeVariable ( CFiltreDynamique filtre, C2iExpression expression, CVariableDynamique variable )
		{
			if ( expression == null )
				return;
			foreach ( C2iExpressionChamp exp in expression.ExtractExpressionsType(typeof(C2iExpressionChamp) ))
			{
				if ( exp.DefinitionPropriete is CDefinitionProprieteDynamiqueVariableDynamique )
				{
					CDefinitionProprieteDynamiqueVariableDynamique def = (CDefinitionProprieteDynamiqueVariableDynamique)exp.DefinitionPropriete;
                    if (def.IdChamp == variable.IdVariable)
                    {
                        def = new CDefinitionProprieteDynamiqueVariableDynamique(variable);
                        exp.DefinitionPropriete = def;
                    }
				}
			}
		}
		
		/// //////////////////////////////////////////////////
		public override void OnChangeVariable ( CFiltreDynamique filtre,CVariableDynamique variable )
		{
			OnChangeVariable ( filtre, m_expressionConditionApplication, variable );
		}

        /// //////////////////////////////////////////////////
        public override CResultAErreur GetComposantExpression(CFiltreDynamique filtre)
        {
            CResultAErreur result = CResultAErreur.False;
            return result;
        }

		
	}
}
