using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamiqueSelectionStatique.
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueSelectionStatique : CComposantFiltreDynamique
	{
		private CDefinitionProprieteDynamique m_champ = null;

		//Indique si les elements sont inclus ou exclus
		private bool m_bExclure = false;

        private CListeCDbKey m_listeIdentifiantsSelectionnes = new CListeCDbKey();

		private C2iExpression m_expressionConditionApplication = new C2iExpressionVrai();

		/// //////////////////////////////////////////////////
		public CComposantFiltreDynamiqueSelectionStatique()
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
				string strRetour = m_champ != null?I.T(" on @1|149",m_champ.Nom):"";
				strRetour = I.T("Selection @1|148",strRetour);
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
		public CDbKey[] ListeIdentifiants
		{
			get
			{
				return m_listeIdentifiantsSelectionnes.ToArray();
			}
			set
			{
                if ( value != null )
                    m_listeIdentifiantsSelectionnes = new CListeCDbKey(value);
                else
                    m_listeIdentifiantsSelectionnes = new CListeCDbKey();
			}
		}

		/// //////////////////////////////////////////////////
		public bool ExclureLesElementsSelectionnes
		{
			get
			{
				return m_bExclure;
			}
			set
			{
				m_bExclure = value;
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
			return 1;
            //1 : passage en CDbKey au lieu des identifiants stockés
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

			obj = m_expressionConditionApplication;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expressionConditionApplication = (C2iExpression )obj;

            if ( nVersion < 1 )
            {
			    IList lst = new ArrayList (  );
			    result = serializer.TraiteListeObjetsSimples (ref lst );
			    lst = new ArrayList ( lst );
                List<CDbKey> lstKeys = new List<CDbKey>();
                foreach ( int nIdentifiant in lst )
                    lstKeys.Add ( CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nIdentifiant));
                m_listeIdentifiantsSelectionnes = new CListeCDbKey(lstKeys);
            }
            else
            {
                serializer.TraiteObject<CListeCDbKey>(ref m_listeIdentifiantsSelectionnes);
            }


			serializer.TraiteBool ( ref m_bExclure );

			return result;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur GetComposantFiltreData (CFiltreDynamique filtre, CFiltreData filtreData)
		{
			CResultAErreur result = CResultAErreur.True;
            if ( m_listeIdentifiantsSelectionnes.Count == 0 )
                return result;
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
			if ( m_champ is CDefinitionProprieteDynamiqueThis )
				strNomChamp = "";
			else
				strNomChamp = strNomChamp+".";

            bool bIdNumericId = m_listeIdentifiantsSelectionnes[0].IsNumericalId();

            //TESTDBKEYTODO

            if (bIdNumericId)
            {
                //Trouve le nom du champ identifiant
                Type tp = m_champ.TypeDonnee.TypeDotNetNatif;
                object[] attribs = tp.GetCustomAttributes(typeof(TableAttribute), true);
                if (attribs.Length > 0)
                {
                    TableAttribute tf = (TableAttribute)attribs[0];
                    if (tf.ChampsId.Length != 1)
                    {
                        result.EmpileErreur(I.T("Cannot apply selection on field @1|150", strNomChamp));
                        return result;
                    }
                    strNomChamp += tf.ChampsId[0];
                }
                else
                {
                    result.EmpileErreur(I.T("Cannot apply selection on field @1|150", strNomChamp));
                    return result;
                }
            }
            else
                strNomChamp += CObjetDonnee.c_champIdUniversel;

			CComposantFiltreChamp composantChamp = new CComposantFiltreChamp ( strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements) );
			CComposantFiltre composantOperateur;
			
			if ( m_bExclure )
			{
				composantOperateur = new CComposantFiltreOperateur( CComposantFiltreOperateur.c_IdOperateurNotIn );
			}
			else
				composantOperateur = new CComposantFiltreOperateur( CComposantFiltreOperateur.c_IdOperateurIn );
			composantOperateur.Parametres.Add ( composantChamp );
			
			ArrayList lst = new ArrayList();
            foreach (CDbKey key in m_listeIdentifiantsSelectionnes)
                lst.Add(new CComposantFiltreConstante(key.GetValeurInDb()));
			composantOperateur.Parametres.Add ( new CComposantFiltreListe ( (IExpression[])lst.ToArray(typeof(IExpression) ) ) );

			if ( m_bExclure )
			{
				//Si not in->Peut aussi être null !!!
				CComposantFiltre compoOu = new CComposantFiltreOperateur( CComposantFiltreOperateur.c_IdOperateurOu );
				CComposantFiltreHasNo compoHasNo = new CComposantFiltreHasNo();
				compoHasNo.Parametres.Add ( new CComposantFiltreChamp ( strNomChamp, CContexteDonnee.GetNomTableForType(filtre.TypeElements) ) );
				compoOu.Parametres.Add ( compoHasNo );
				compoOu.Parametres.Add ( composantOperateur );
				composantOperateur = compoOu;
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

			else if ( !typeof ( CObjetDonneeAIdNumerique ).IsAssignableFrom(m_champ.TypeDonnee.TypeDotNetNatif) )
				result.EmpileErreur(I.T("Cannot apply static selection on field @1|151",m_champ.Nom));

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
			return false;
		}

	
		/// //////////////////////////////////////////////////
		public override void OnChangeVariable ( CFiltreDynamique filtre,CVariableDynamique variable )
		{
		}

        /// //////////////////////////////////////////////////
        public override CResultAErreur GetComposantExpression(CFiltreDynamique filtre)
        {
            CResultAErreur result = CResultAErreur.False;
            return result;
        }

		
	}
}
