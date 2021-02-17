using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using sc2i.common;
using sc2i.common.unites;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CAnalyseurSyntaxiqueExpression.
	/// </summary>
	public class CAnalyseurSyntaxiqueExpression : CAnalyseurSyntaxique
	{
		
        /// <summary>
        ///Stocke un ArrayList par niveau, l'array list contenant des COperateurAnalysables
        /// </summary>
        //Modif stef 6/6/2011 : passe la table en static pour éviter de la recalculer tout le temps
		private static Hashtable m_tableListeOperateursParNiveau = new Hashtable();

		protected CContexteAnalyse2iExpression m_contexteAnalyse;

        private static List<IFournisseurConstantesDynamiques> m_lstFournisseursExpressionsDynamiques = new List<IFournisseurConstantesDynamiques>();

        /// //////////////////////////////////////////////////////////////
        public static void RegisterFournisseurExpressionsDynamiques(IFournisseurConstantesDynamiques fournisseur)
        {
            if (m_lstFournisseursExpressionsDynamiques.FirstOrDefault(f => f.GetType() == fournisseur.GetType()) == null)
                m_lstFournisseursExpressionsDynamiques.Add(fournisseur);
        }
		

		/// /////////////////////////////////////////////////
		public CAnalyseurSyntaxiqueExpression( CContexteAnalyse2iExpression contexteAnalyse )
		{
			m_contexteAnalyse = contexteAnalyse;
		}

        /// /////////////////////////////////////////////////
        private Hashtable TableOperateursParNiveaux
        {
            get
            {
                if (m_tableListeOperateursParNiveau.Count == 0)
                    InitTableOperateursParNiveau();
                return m_tableListeOperateursParNiveau;
            }
        }

        /// /////////////////////////////////////////////////
        private void InitTableOperateursParNiveau()
        {
            m_tableListeOperateursParNiveau.Clear();
            foreach (C2iExpression exp in CAllocateur2iExpression.ToutesExpressions)
            {
                if (exp is C2iExpressionAnalysable)
                {
                    CInfo2iExpression info = ((C2iExpressionAnalysable)exp).GetInfos();
                    if (info != null)
                    {
                        COperateurAnalysable op = new COperateurAnalysable(info.Niveau, info.Texte, info.IdExpression, exp.CanBeArgumentExpressionObjet);
                        ArrayList lst = (ArrayList)m_tableListeOperateursParNiveau[info.Niveau];
                        if (lst == null)
                        {
                            lst = new ArrayList();
                            m_tableListeOperateursParNiveau[info.Niveau] = lst;
                        }
                        lst.Add(op);
                    }
                }
            }
            
        }

        /// /////////////////////////////////////////////////
        public static IExpression[] GetConstantesDynamiques()
        {
            List<IExpression> lst = new List<IExpression>();
            foreach ( IFournisseurConstantesDynamiques fournisseur in m_lstFournisseursExpressionsDynamiques )
            {
                lst.AddRange ( fournisseur.GetConstantes() );
            }
            return lst.ToArray();
        }


		/// /////////////////////////////////////////////////
		public override CResultAErreur AnalyseChaine ( string strChaine )
		{
			CResultAErreur result = base.AnalyseChaine(strChaine);
			if ( result )
			{
				C2iExpression expression = (C2iExpression)result.Data;
				if ( expression != null )
					expression.SetTypeObjetInterroge ( 
						m_contexteAnalyse.ObjetAnalyse,
						m_contexteAnalyse );
				//Vérifie que tous les champs ont un type de donnée connu
				//(ce qui indique que les champs sont connus)
				foreach ( C2iExpressionChamp expChamp in expression.ExtractExpressionsType(typeof(C2iExpressionChamp)) )
				{
					if ( expChamp.DefinitionPropriete == null )
					{
						result.EmpileErreur(I.T("One field of formula doesn't exist|141"));
					}
					else if ( expChamp.TypeDonnee == null )
					{
                        if (m_contexteAnalyse != null && m_contexteAnalyse.GetVariable(expChamp.DefinitionPropriete.NomPropriete) == null)
                            result.EmpileErreur(I.T("The @1 field doesn't exist|107", expChamp.DefinitionPropriete.Nom));
                        else
                            expChamp.DefinitionPropriete = m_contexteAnalyse.GetVariable(expChamp.DefinitionPropriete.NomPropriete);
					}
				}
                result = expression.VerifieParametres();
				if ( !result )
					return result;
				result.Data = expression;
			}
			return result;
		}


		/// /////////////////////////////////////////////////
		protected override char SeparateurParametres
		{
			get
			{
				return ';';
			}
		}



		/// /////////////////////////////////////////////////
		protected override IAllocateurExpression AllocateurExpression 
		{
			get
			{
				return new CAllocateur2iExpression();
			}
		}

        /// /////////////////////////////////////////////////
        protected override List<COperateurAnalysable> GetOperateursDynamiqueNiveau ( int nNiveau, string strMot )
        {
            List<COperateurAnalysable> lst = new List<COperateurAnalysable>();
            if (nNiveau == 0  && strMot.StartsWith(":"))
            {
                string strTmp = strMot.Substring(1);
                if (CUtilUnite.GetIdClasseUnite(strTmp) != null)
                {
                    COperateurAnalysable operateur = new COperateurAnalysable(0,
                        ":" + strTmp, ":" + strTmp, false);
                    lst.Add(operateur);
                }
            }
            return lst;
        }

		/// /////////////////////////////////////////////////
		protected override ArrayList GetOperateursNiveau ( int nNiveau )
		{
            return (ArrayList)TableOperateursParNiveaux[nNiveau];
		}

		/// /////////////////////////////////////////////////
		protected override int NiveauOperateurMax
		{
			get
			{
                return TableOperateursParNiveaux.Count - 1;
			}
		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionThis ( )
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_contexteAnalyse.ObjetAnalyse != null)
				result.Data = new C2iExpressionThis(m_contexteAnalyse.ObjetAnalyse.TypeAnalyse);
			else
				result.Data = new C2iExpressionThis();
			return result;
		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionRoot()
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = new C2iExpressionRoot(m_contexteAnalyse.ObjetAnalyse);
			return result;
		}

		/// /////////////////////////////////////////////////
		/// Le data du result contient l'expression
		protected override CResultAErreur GetExpressionChamp ( string strChamp )
		{
			CDefinitionProprieteDynamique definitionChamp = null;
			CResultAErreur result = CResultAErreur.True;

            //Est-ce une constante dynamique ?
            foreach (IFournisseurConstantesDynamiques fournisseur in m_lstFournisseursExpressionsDynamiques)
            {
                IExpression constante = fournisseur.GetConstante(strChamp);
                if (constante != null)
                {
                    result.Data = constante;
                    return result;
                }
            }

			if ( m_contexteAnalyse.FournisseurProprietes == null )
			{
				result.EmpileErreur(I.T("The property supplier parser cannot recognize the fields|143"));
				return result;
			}
			else
			{
				if (!IsAnalyseExpressionObjet() )
				{
					string strNomUpper = strChamp.ToUpper();
					foreach (CDefinitionProprieteDynamique def in m_contexteAnalyse.GetDefinitionsChamps(m_contexteAnalyse.ObjetAnalyse))
					{
						if (def.Nom.ToUpper() == strNomUpper || def.NomPropriete.ToUpper() == strNomUpper)
						{
							definitionChamp = def;
							break;
						}
					}
				}
				if ( definitionChamp == null )
				{
					definitionChamp = m_contexteAnalyse.GetVariable ( strChamp );
					if ( definitionChamp == null )
						//Définition temporaire, elle n'existe pas dans le type de base
						definitionChamp = new CDefinitionProprieteDynamiqueDotNet(strChamp, strChamp, null, false, true,"");
				}
				result.Data = new C2iExpressionChamp(definitionChamp);
				return result;
			}
		}

		/// /////////////////////////////////////////////////
		/// Le data du result contient l'expression
		protected override CResultAErreur GetExpressionMethode ( string strMethode )
		{
			CDefinitionMethodeDynamique definitionMethode = null;
            CDefinitionFonctionDynamique definitionFonction = null;
			CResultAErreur result = CResultAErreur.True;
			if ( m_contexteAnalyse.FournisseurProprietes == null )
			{
				result.EmpileErreur(I.T("The property supplier parser cannot recognize the methods|144"));
				return result;
			}
			else
			{
				if (!IsAnalyseExpressionObjet())
				{
					string strNomUpper = strMethode.ToUpper();
					foreach (CDefinitionProprieteDynamique def in m_contexteAnalyse.GetDefinitionsChamps(m_contexteAnalyse.ObjetAnalyse))
					{
						if (def is CDefinitionMethodeDynamique)
						{
							if (def.Nom.ToUpper() == strNomUpper || def.NomPropriete.ToUpper() == strNomUpper)
							{
								definitionMethode = (CDefinitionMethodeDynamique)def;
								break;
							}
						}
                        if ( def is CDefinitionFonctionDynamique)
                        {
                            if ( def.NomPropriete.ToUpper() == strNomUpper )
                            {
                                definitionFonction = (CDefinitionFonctionDynamique)def;
                                break;
                            }
                        }

					}
				}
				/*if ( definitionMethode == null )
				{
					//Définition temporaire, elle n'existe pas dans le type de base
					definitionMethode = new CDefinitionMethodeDynamique(strMethode, strMethode, null, false);
				}*/
                if (definitionFonction != null)
                    result.Data = new C2iExpressionMethodeDynamique(definitionFonction);
                else if (definitionMethode != null)
                    result.Data = new C2iExpressionMethodeDynamique(definitionMethode);
                else
                    result.Data = new C2iExpressionMethodeDynamique(strMethode);
				return result;
			}
		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionObjet ( IExpression expressionSource, IExpression methodeOuPropriete )
		{
			CResultAErreur result = CResultAErreur.True;
			CObjetPourSousProprietes objetPourSousProprietes = ((C2iExpression)expressionSource).GetObjetPourSousProprietes();
			if ( !methodeOuPropriete.CanBeArgumentExpressionObjet )
			{
				result.EmpileErreur(I.T("'@1' cannot be located on the right of '.'|145", methodeOuPropriete.GetString()));
				return result;
			}

			//Complète le type des expressions filles inconnues
			if (objetPourSousProprietes != null )
				result = ((C2iExpression)methodeOuPropriete).SetTypeObjetInterroge ( objetPourSousProprietes, m_contexteAnalyse );

            C2iExpressionMethodeAnalysable methode = methodeOuPropriete as C2iExpressionMethodeAnalysable;
            //Si la méthode analyse ses paramètres à partir du type de base (réponse null à GetTypeForParametresFromTypeObjetSource
            //il faut indiquer aux paramètres que leur type est le type de base
            //les paramètres n'ont pas connu leur type parceque IsAnalyseObjet était
            //true lors de l'analyse de la méthode
            if ( methode != null && (objetPourSousProprietes == null  || methode.GetObjetAnalyseParametresFromObjetAnalyseSource(objetPourSousProprietes) == null) )
                foreach (C2iExpression parametre in methodeOuPropriete.Parametres)
                    parametre.SetTypeObjetInterroge(m_contexteAnalyse.ObjetAnalyse, m_contexteAnalyse);
			if ( result )
				result.Data = new C2iExpressionObjet(expressionSource, methodeOuPropriete);
			return result;

		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionListe ( IExpression[] elements )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = new C2iExpressionListe ( elements );
			return result;
		}


		/// /////////////////////////////////////////////////
		protected override bool IntegrerSyntaxeObjet
		{
			get 
			{
				return true;
			}
		}

		/// /////////////////////////////////////////////////
		public void AddVariable ( CDefinitionProprieteDynamiqueVariableFormule def )
		{
				m_contexteAnalyse.AddVariable ( def );
		}

		/// /////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueVariableFormule GetVariable ( string strNom )
		{
			return m_contexteAnalyse.GetVariable(strNom);
		}

		


	}
}
