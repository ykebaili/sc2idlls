using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamiqueValeurChamp.
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueRechercheAvancee : CComposantFiltreDynamique
	{
		private CDefinitionProprieteDynamique m_champ = null;

		private C2iExpression m_expressionValeur = null;

		private C2iExpression m_expressionConditionApplication = new C2iExpressionVrai();

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamiqueRechercheAvancee()
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
				string strRetour = I.T("Find |139");
				if ( m_expressionValeur != null )
					strRetour += m_expressionValeur.GetString();
				else
					strRetour += I.T("[No value]|141");
				strRetour += I.T(" in |140");
				strRetour += m_champ != null?m_champ.Nom:I.T("[Field not defined]|142");
				return strRetour;
			}
		}

		/// //////////////////////////////////////////////////
		public CDefinitionProprieteDynamique Champ
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


		/// //////////////////////////////////////////////////
		public override void InsereDefinitionToObjetFinal ( CDefinitionProprieteDynamique def )
		{
			if ( Champ != null )
			{
				Champ.InsereParent ( def );
			}
		}

		/// //////////////////////////////////////////////////
		public C2iExpression ExpressionValeur
		{
			get
			{
				return m_expressionValeur;
			}
			set
			{
				m_expressionValeur = value;
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
            //return 1;//Passage au CDbKey : conversion nécéssaire du champ
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			I2iSerializable obj = m_champ;
			serializer.TraiteObject ( ref obj );
			m_champ = (CDefinitionProprieteDynamique)obj;

			obj = m_expressionValeur;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionValeur = (C2iExpression)obj;

			obj = m_expressionConditionApplication;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionConditionApplication = (C2iExpression )obj;
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
            string strNomChamp = m_champ.NomPropriete;
            if (m_champ is CDefinitionProprieteDynamiqueChampCustom)
            {
                CDbKey keyChamp = ((CDefinitionProprieteDynamiqueChampCustom)m_champ).DbKeyChamp;
                strNomChamp = m_champ.NomPropriete;
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
                // TESTDBKEYOK
                if (keyChamp.IsNumericalId())
                {
                    operateurIdChamp.Parametres.Add(new CComposantFiltreChamp(strAcces + CChampCustom.c_champId, CContexteDonnee.GetNomTableForType(filtre.TypeElements)));
                }
                else
                    operateurIdChamp.Parametres.Add(new CComposantFiltreChamp(strAcces + CObjetDonnee.c_champIdUniversel, CContexteDonnee.GetNomTableForType(filtre.TypeElements)));

                operateurIdChamp.Parametres.Add(new CComposantFiltreConstante(keyChamp.GetValeurInDb()));
                opPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                opPrincipal.Parametres.Add(operateurIdChamp);
                Type typeChamp = m_champ.TypeDonnee.TypeDotNetNatif;
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

            CComposantFiltre composantRecherche = new CComposantFiltreRechercheAvancee();

            CComposantFiltre composantValeur = null;
            result = ExpressionValeur.Eval(contexteEvaluation);
            if (!result)
            {
                result.EmpileErreur(I.T("Error while evaluation of @1|143", ExpressionValeur.GetString()));
                result.Data = null;
                return result;
            }
            composantValeur = new CComposantFiltreConstante(result.Data.ToString());
            composantRecherche.Parametres.Add(composantChamp);
            composantRecherche.Parametres.Add(composantValeur);

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
			if ( m_champ == null )
				result.EmpileErreur(I.T("Field not defined|144"));
			if ( ExpressionValeur == null )
				result.EmpileErreur(I.T("Value formula is incorrect|145"));
			else if ( ExpressionValeur != null )
			{
				ArrayList lstChamps = ExpressionValeur.ExtractExpressionsType ( typeof ( C2iExpressionChamp ) );
				foreach ( C2iExpressionChamp expChamp in lstChamps )
				{
					CDefinitionProprieteDynamique def = expChamp.DefinitionPropriete;
					if ( def == null )
					{
						result.EmpileErreur(I.T("Null field in value formula|146"));
					}
					else
					{
						if ( def is CDefinitionProprieteDynamiqueVariableDynamique )
						{
							CDefinitionProprieteDynamiqueVariableDynamique defVar = (CDefinitionProprieteDynamiqueVariableDynamique)def;
							CVariableDynamique variable = filtre.GetVariable ( defVar.IdChamp );
							if ( variable == null )
							{
								result.EmpileErreur(I.T("The variable @1 doesn't exist|147", defVar.Nom));
							}
						}
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////
        protected bool DoesExpressionUtiliseVariable(C2iExpression expression, CVariableDynamique variable)
        {
            if (expression == null)
                return false;
            foreach (C2iExpressionChamp exp in expression.ExtractExpressionsType(typeof(C2iExpressionChamp)))
            {
                if (exp.DefinitionPropriete is CDefinitionProprieteDynamiqueVariableDynamique)
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
			if ( DoesExpressionUtiliseVariable ( m_expressionValeur, variable ) )
				return true;
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
					if ( def.IdChamp == variable.IdVariable )
					{
						def = new CDefinitionProprieteDynamiqueVariableDynamique ( variable );
						exp.DefinitionPropriete = def;
					}
				}
			}
		}
		
		/// //////////////////////////////////////////////////
		public override void OnChangeVariable ( CFiltreDynamique filtre,CVariableDynamique variable )
		{
			OnChangeVariable ( filtre, m_expressionValeur, variable );
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
