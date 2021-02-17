using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;
using sc2i.expression.expressions;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamiqueValeurChamp.
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueValeurChamp : CComposantFiltreDynamique
	{
		private CDefinitionProprieteDynamique m_champ = null;



		//La liste des opérateurs est donnée par CComposantFiltre.m_operateurs niveau 3
		private string m_strIdOperateur = CComposantFiltreOperateur.c_IdOperateurEgal;

		//Test soit sur une valeur d'expression
		private C2iExpression m_expressionValeur = null;
		
		/// Soit sur un champ par SQL
		private CDefinitionProprieteDynamique m_champValeur = null;

		private C2iExpression m_expressionConditionApplication = new C2iExpressionVrai();

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamiqueValeurChamp()
		{
			
		}

		/*/// //////////////////////////////////////////////////
		public static COperateurAnalysable[] ListeOperateurs
		{
			
		}*/

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
				string strRetour = m_champ != null ? m_champ.Nom : I.T("[Field not defined]|142");
				CComposantFiltreOperateur compo = new CComposantFiltreOperateur(m_strIdOperateur);
				if ( compo == null )
					strRetour += I.T(" [Operator not defined] |154");
				else
					strRetour += " "+compo.Operateur.Texte+" ";
				if ( m_expressionValeur != null )
					strRetour += m_expressionValeur.GetString();
				else if (m_champValeur != null )
					strRetour += m_champValeur.Nom;
				else
					strRetour += I.T(" [Value not defined] |155");
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
		public CDefinitionProprieteDynamique ChampValeur
		{
			get
			{
				return m_champValeur;
			}
			set
			{
				m_champValeur = value;
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
		public string IdOperateur
		{
			get
			{
				return m_strIdOperateur;
			}
			set
			{
				m_strIdOperateur = value;
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
					m_expressionConditionApplication = new C2iExpressionVrai (  );
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
			return 2;
			//2 ajout possiblité de tester sur un champ sql
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

			serializer.TraiteString ( ref m_strIdOperateur );
			
			obj = m_expressionValeur;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionValeur = (C2iExpression)obj;

			if ( nVersion >= 1 )
			{
				obj = m_expressionConditionApplication;
				result = serializer.TraiteObject ( ref obj );
				if ( !result )
					return result;
				m_expressionConditionApplication = (C2iExpression )obj;
			}
			if ( nVersion >= 2 )
			{
				obj = m_champValeur;
				serializer.TraiteObject ( ref obj );
				m_champValeur = (CDefinitionProprieteDynamique)obj;
			}
			else
				m_champValeur = null;
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

			if ( result.Data  == null || (result.Data is bool && !((bool)result.Data)) || result.Data.ToString() =="0" )
			{
				result.Data = null;
				return result;
			}
			if ( m_champValeur is CDefinitionProprieteDynamiqueChampCustom )
			{
				if ( m_champ is CDefinitionProprieteDynamiqueChampCustom )
				{
					result.EmpileErreur(I.T("Operation not supported: Field = Field|156"));
					return result;
				}
				else
				{
					CDefinitionProprieteDynamique defTmp = m_champ;
					m_champ = m_champValeur;
					m_champValeur = defTmp;
				}
			}

			CComposantFiltreOperateur opPrincipal = null;
			string strNomChamp = m_champ.NomPropriete;
            CDbKey dbKeyChampCustom = null;
			if ( m_champ is CDefinitionProprieteDynamiqueChampCustom )
			{
				CDbKey keyChamp = ((CDefinitionProprieteDynamiqueChampCustom)m_champ).DbKeyChamp;
				strNomChamp = m_champ.NomPropriete;
				string strAcces = "";
				//Trouve le nom du champ et le chemin d'accès
				int nPos = strNomChamp.LastIndexOf(".");
				if ( nPos != -1 )
				{
					strAcces = strNomChamp.Substring(0, nPos+1 );
					strNomChamp = strNomChamp.Substring(nPos+1 );
				}
				strAcces += "RelationsChampsCustom.";
                dbKeyChampCustom = ((CDefinitionProprieteDynamiqueChampCustom)m_champ).DbKeyChamp;
                //Stef, 12/10/2009, le test de l'id du champ custom est
                //Maintenant gerée par le CInfoRelationChampCustom et non 
                //Plus directement dans la clause where. Ca va beaucoup plus vite sous SqlServer
				/*CComposantFiltreOperateur operateurIdChamp = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurEgal );
				CComposantFiltreChamp compo1 = new CComposantFiltreChamp ( strAcces+CChampCustom.c_champId, CContexteDonnee.GetNomTableForType (filtre.TypeElements));
				compo1.IdChampCustom = nIdChampCustom;
				operateurIdChamp.Parametres.Add ( compo1 );
				operateurIdChamp.Parametres.Add ( new CComposantFiltreConstante ( nIdChamp ) );
				opPrincipal = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurEt );
				opPrincipal.Parametres.Add ( operateurIdChamp );*/
				Type typeChamp = m_champ.TypeDonnee.TypeDotNetNatif;
				if ( typeChamp == typeof(double))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDouble;
				else if ( typeChamp == typeof(int))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurInt;
				else if ( typeChamp == typeof(DateTime) || typeChamp == typeof(CDateTimeEx) )
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurDate;
				else if ( typeChamp == typeof(bool))
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurBool;
				else
					strNomChamp = CRelationElementAChamp_ChampCustom.c_champValeurString;
				strNomChamp =  strAcces+strNomChamp;
                opPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                CComposantFiltreChamp composantChampNull = new CComposantFiltreChamp(strAcces+CRelationElementAChamp_ChampCustom.c_champValeurNull,CContexteDonnee.GetNomTableForType(filtre.TypeElements));
                composantChampNull.IdChampCustom = CChampCustom.GetIdFromDbKey(dbKeyChampCustom);
                CComposantFiltreOperateur opEgal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEgal);
                opEgal.Parametres.Add ( composantChampNull );
                opEgal.Parametres.Add ( new CComposantFiltreConstante(0));
                opPrincipal.Parametres.Add(opEgal);
			}
			CComposantFiltreChamp composantChamp = new CComposantFiltreChamp ( strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements) );
            composantChamp.IdChampCustom = CChampCustom.GetIdFromDbKey(dbKeyChampCustom);
			CComposantFiltreOperateur composantOperateur = new CComposantFiltreOperateur ( IdOperateur );
			
			
			CComposantFiltre composantValeur = null;
			if ( ExpressionValeur == null )
			{
				if ( m_champValeur != null )
					composantValeur = new CComposantFiltreChamp ( m_champValeur.NomPropriete, composantChamp.TableDeBase );
				else
				{

					result.EmpileErreur(I.T("Value is not defined for element @1|157",Description));
					result.Data = null;
					return result;
				}
			}
			else
			{
				result = ExpressionValeur.Eval ( contexteEvaluation );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error while evaluation of @1|143",ExpressionValeur.GetString()));
					result.Data = null;
					return result;
				}
				CComposantFiltreVariable composantVariable = new CComposantFiltreVariable ( "@"+(filtreData.Parametres.Count+1).ToString() );
				filtreData.Parametres.Add ( result.Data );
				composantValeur = composantVariable;
			}
			composantOperateur.Parametres.Add ( composantChamp );
			composantOperateur.Parametres.Add ( composantValeur );

			if ( IdOperateur == CComposantFiltreOperateur.c_IdOperateurDifferent )
			{
				//Ajoute ou null
				CComposantFiltreOperateur ou = new CComposantFiltreOperateur ( CComposantFiltreOperateur.c_IdOperateurOu );
				ou.Parametres.Add ( composantOperateur );
				
				CComposantFiltreHasNo hasNo = new CComposantFiltreHasNo();
				hasNo.Parametres.Add ( new CComposantFiltreChamp ( strNomChamp, composantChamp.TableDeBase ));
				ou.Parametres.Add ( hasNo );
				composantOperateur = ou;
			}
			if ( opPrincipal != null )
			{
				opPrincipal.Parametres.Add ( composantOperateur );
				composantOperateur = opPrincipal;
			}
			result.Data = composantOperateur;
			return result;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieIntegrite( CFiltreDynamique filtre)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_champ == null )
				result.EmpileErreur(I.T("Field not defined|144"));
			if ( m_strIdOperateur == null )
				result.EmpileErreur(I.T("Operator not defined|158"));
			if ( m_champValeur is CDefinitionProprieteDynamiqueChampCustom && 
				m_champ is CDefinitionProprieteDynamiqueChampCustom )
			{
				result.EmpileErreur(I.T("Test function not supported : Custom field = Custom field|159"));
				return result;
			}
			if ( ExpressionValeur == null && m_champValeur == null)
				result.EmpileErreur(I.T("Value formula is incorrect|145"));
			else if ( ExpressionValeur != null )
			{
				ArrayList lstChamps = ExpressionValeur.ExtractExpressionsType ( typeof ( C2iExpressionChamp ) );
				foreach ( C2iExpressionChamp expChamp in lstChamps )
				{
					CDefinitionProprieteDynamique def = expChamp.DefinitionPropriete;
					if ( def == null )
					{
						result.EmpileErreur(I.T("Null field in value formula|160"));
					}
					else
					{
						if ( def is CDefinitionProprieteDynamiqueVariableDynamique )
						{
							CDefinitionProprieteDynamiqueVariableDynamique defVar = (CDefinitionProprieteDynamiqueVariableDynamique)def;
							CVariableDynamique variable = filtre.GetVariable ( defVar.IdChamp );
							if ( variable == null )
							{
								result.EmpileErreur(I.T("The variable @1 doesn't exist|147",defVar.Nom));
							}
						}
					}
				}
			}
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
			if ( DoesExpressionUtiliseVariable ( m_expressionValeur, variable ) )
				return true;
			if ( DoesExpressionUtiliseVariable ( m_expressionConditionApplication, variable ) )
				return true;
			return false;
			
		}

		/// //////////////////////////////////////////////////
        protected void OnChangeVariable(CFiltreDynamique filtre, C2iExpression expression, CVariableDynamique variable)
        {
            if (expression == null)
                return;
            foreach (C2iExpressionChamp exp in expression.ExtractExpressionsType(typeof(C2iExpressionChamp)))
            {
                if (exp.DefinitionPropriete is CDefinitionProprieteDynamiqueVariableDynamique)
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

        private string GetRegExFromLike(string strLike)
        {
            string strRetour = "^" + strLike + "$";
            strRetour = strRetour.Replace("%", ".*");
            strRetour = strRetour.Replace("?", ".");
            return strRetour;
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
            CResultAErreur result = CResultAErreur.True;
            //Vérifie que l'expression condition est OK
            CContexteEvaluationExpression contexteEvaluation = new CContexteEvaluationExpression(filtre);
            result = ConditionApplication.Eval(contexteEvaluation);
            if (!result)
            {
                result.EmpileErreur(I.T("Error while evaluation of @1|143", ConditionApplication.GetString()));
                return result;
            }

            if ((result.Data is bool && !((bool)result.Data)) || result.Data.ToString() == "0")
            {
                result.Data = null;
                return result;
            }
            result = ExpressionValeur.Eval(contexteEvaluation);
            object valeur = result.Data;
            

            C2iExpression expChamp = new C2iExpressionChamp(Champ);

            C2iExpression expOp = null;
            C2iExpression expRacine = null;
            switch (IdOperateur)
            {
                case CComposantFiltreOperateur.c_IdOperateurEgal :
                    expOp = new C2iExpressionEgal();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurContains :
                    expOp = new C2iExpressionContient();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurDifferent :
                    expOp = new C2iExpressionDifferent();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurIn :
                    expOp = new C2iExpressionDans();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurInf :
                    expOp = new C2iExpressionInferieur();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurInfEgal :
                    expOp = new C2iExpressionInferieurOuEgal();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurLike :
                    expOp = new C2iExpressionMatchRegex();
                    if (valeur != null)
                        valeur = GetRegExFromLike(valeur.ToString());
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurNotIn :
                    expRacine = new C2iExpressionNon();
                    expOp = new C2iExpressionDans();
                    expRacine.Parametres.Add(expOp);
                    break;
                case CComposantFiltreOperateur.c_IdOperateurNotLike :
                    expRacine = new C2iExpressionNon();
                    expOp = new C2iExpressionMatchRegex();
                    if (valeur != null)
                        valeur = GetRegExFromLike(valeur.ToString());
                    expRacine.Parametres.Add(expOp);
                    break;
                case CComposantFiltreOperateur.c_IdOperateurSup :
                    expOp = new C2iExpressionSuperieur();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurSuperieurOuEgal :
                    expOp = new C2iExpressionSuperieurOuEgal();
                    expRacine = expOp;
                    break;
                case CComposantFiltreOperateur.c_IdOperateurWithout :
                    expRacine = new C2iExpressionNon();
                    expOp = new C2iExpressionContient();
                    expRacine.Parametres.Add(expOp);
                    break;
                default:
                    result.EmpileErreur(I.T("Can not use this operator in filter|20011"));
                    return result;
            }

            if (valeur is IList || valeur is Array)
            {
                C2iExpressionListe lst = new C2iExpressionListe();
                foreach (object val in (IEnumerable)valeur)
                    lst.Parametres.Add(new C2iExpressionConstante(val));
                valeur = lst;
            }
            else
                valeur = new C2iExpressionConstante(valeur);

            if (expChamp.TypeDonnee.IsArrayOfTypeNatif)
            {
                //Recherche dans des données filles (tableau)
                C2iExpressionSelectFirst expSelFirst = new C2iExpressionSelectFirst();
                expSelFirst.Parametres.Add(expRacine);
                C2iExpressionObjet expObjet = new C2iExpressionObjet();
                expObjet.Parametres.Add(expChamp);
                expObjet.Parametres.Add(expSelFirst);
                expChamp = new C2iExpressionThis();
                C2iExpression expNull = new C2iExpressionNull();
                expNull.Parametres.Add(expObjet);
                C2iExpression expNon = new C2iExpressionNon();
                expNon.Parametres.Add(expNull);
                expRacine = expNon;   
            }

            expOp.Parametres.Add(expChamp);
            expOp.Parametres.Add(valeur);
            result.Data = expRacine;
            string strExp = expRacine.GetString();
            return result;
        }
		
	}
}
