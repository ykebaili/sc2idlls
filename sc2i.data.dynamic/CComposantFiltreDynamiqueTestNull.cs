using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;
using sc2i.expression.expressions;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamiqueTestNull.
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueTestNull : CComposantFiltreDynamique
	{
		private CDefinitionProprieteDynamique m_champ = null;

		//Test null (true) ou Non null(false)
		private bool m_bTestNull = true;

		private C2iExpression m_expressionConditionApplication = new C2iExpressionVrai();

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamiqueTestNull()
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
				string strRetour = m_champ != null?m_champ.Nom:I.T("[Field not defined]|142");
				if ( !m_bTestNull )
					strRetour += I.T(" is not null|152");
				else
					strRetour += I.T(" is null|153");
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
		public bool TestNull
		{
			get
			{
				return m_bTestNull;
			}
			set
			{
				m_bTestNull = value;
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
		public override void InsereDefinitionToObjetFinal ( CDefinitionProprieteDynamique def )
		{
			if ( Champ != null )
			{
				Champ.InsereParent ( def );
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
			I2iSerializable obj = m_champ;
			result = serializer.TraiteObject ( ref obj );
			m_champ = (CDefinitionProprieteDynamique)obj;


			if ( !result )
				return result;

			serializer.TraiteBool ( ref m_bTestNull );

			obj = m_expressionConditionApplication;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionConditionApplication = (C2iExpression )obj;

			return result;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur GetComposantFiltreData (CFiltreDynamique filtre, CFiltreData filtreData)
		{
			CResultAErreur result = CResultAErreur.True;
			//Vérifie que l'expression condition est OK
			CContexteEvaluationExpression contexteEvaluation = new CContexteEvaluationExpression(filtre);
			result = ConditionApplication.Eval(contexteEvaluation);
			if ( !result )
			{
				result.EmpileErreur(I.T("Error while evaluation of @1|143",ConditionApplication.GetString()));
				return result;
			}

			if ( result.Data == null || !((result.Data is bool && (bool)result.Data) || result.Data.ToString() =="1" ))
			{
				result.Data = null;
				return result;
			}

			
			string strNomChamp = m_champ.NomPropriete;
            CDbKey dbKeyChampCustom = null;
			if (m_champ is CDefinitionProprieteDynamiqueChampCustom)
			{
				CComposantFiltreOperateur opPrincipal = null;
                dbKeyChampCustom = ((CDefinitionProprieteDynamiqueChampCustom)m_champ).DbKeyChamp;
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

                int nIdChamp = CChampCustom.GetIdFromDbKey(dbKeyChampCustom); 

				CComposantFiltreOperateur operateurIdChamp = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEgal);
				CComposantFiltreChamp compo1 = new CComposantFiltreChamp(strAcces + CChampCustom.c_champId, CContexteDonnee.GetNomTableForType(filtre.TypeElements));
                compo1.IdChampCustom = nIdChamp;
				operateurIdChamp.Parametres.Add(compo1);
                operateurIdChamp.Parametres.Add(new CComposantFiltreConstante(nIdChamp));
				opPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
				opPrincipal.Parametres.Add(operateurIdChamp);
				//Stef, 28072008 : pour tester si un champ est null, on vérifie CRelationElementAChamp_ChampCustom.c_champValeurNull
				//et non pas les valeurs suivant le type
				/*if ( typeChamp == typeof(double))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDouble;
				else if ( typeChamp == typeof(int))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurInt;
				else if ( typeChamp == typeof(DateTime) || typeChamp == typeof(CDateTimeEx) )
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDate;
				else if ( typeChamp == typeof(bool))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurBool;
				else
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurString;
				 */
				strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurNull;
				
				strNomChamp = strAcces + strNomChamp;

				CComposantFiltreChamp composantChamp = new CComposantFiltreChamp(strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements));
                composantChamp.IdChampCustom = nIdChamp;
				CComposantFiltre composantOperateur = new CComposantFiltreOperateur(
					m_bTestNull ? CComposantFiltreOperateur.c_IdOperateurDifferent : CComposantFiltreOperateur.c_IdOperateurEgal);
				composantOperateur.Parametres.Add(composantChamp);
				composantOperateur.Parametres.Add(new CComposantFiltreConstante(0));

				opPrincipal.Parametres.Add(composantOperateur);
				composantOperateur = opPrincipal;
				result.Data = composantOperateur;
				//Stef 28072008, fin modif
			}
			else
			{
				CComposantFiltreChamp composantChamp = new CComposantFiltreChamp(strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements));
                composantChamp.IdChampCustom = CChampCustom.GetIdFromDbKey(dbKeyChampCustom);
				CComposantFiltre composantOperateur;

				if (!m_bTestNull)
					composantOperateur = new CComposantFiltreHas();
				else
					composantOperateur = new CComposantFiltreHasNo();

				composantOperateur.Parametres.Add(composantChamp);
				result.Data = composantOperateur;
			}
			return result;
		}


		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieIntegrite( CFiltreDynamique filtre)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_champ == null )
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
					if ( def.IdChamp == variable.IdVariable )
						return true;
				}
			}
			return false;
		}

		/// //////////////////////////////////////////////////
		public override bool IsVariableUtilisee ( CVariableDynamique variable )
		{
			return false;
		}

	
		/// //////////////////////////////////////////////////
		public override void OnChangeVariable ( CFiltreDynamique filtre,CVariableDynamique variable )
		{
		}

        /// //////////////////////////////////////////////////
        public override CResultAErreur GetComposantExpression(CFiltreDynamique filtreDynamique)
        {
            CResultAErreur result = CResultAErreur.True;
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( filtreDynamique );
            if (ConditionApplication != null)
            {
                result = ConditionApplication.Eval(ctx);
                if (!result)
                {
                    result.EmpileErreur(I.T("Error while evaluation of @1|143", ConditionApplication.GetString()));
                    return result;
                }

                if (!((result.Data is bool && (bool)result.Data) || result.Data.ToString() == "1"))
                {
                    result.Data = null;
                    return result;
                }
            }
            C2iExpressionChamp expChamp = new C2iExpressionChamp(m_champ);
            C2iExpression expressionIsNull = new C2iExpressionEstNull();
            expressionIsNull.Parametres.Add ( expChamp );
            result.Data = expressionIsNull;
            if (!TestNull)
            {
                C2iExpression expNon = new C2iExpressionNon();
                expNon.Parametres.Add(expressionIsNull);
                result.Data = expNon;
            }
            return result;
        }

		
	}
}
