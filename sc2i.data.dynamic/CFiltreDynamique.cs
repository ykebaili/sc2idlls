using System;
using System.Collections;
using System.IO;

using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CFiltreDynamique.
	/// </summary>
	[Serializable]
	public class CFiltreDynamique : I2iSerializable, 
        IFournisseurProprietesDynamiques, 
        IElementAVariablesDynamiquesAvecContexteDonnee, 
        ICloneable
	{
		public const string c_idFichier = "2I_FILTER";

		public const string c_cleSerializer = "FILTRE_DYNAMIQUE";

		private Type m_typeElementsFiltres = null;
		private C2iWnd m_formulaireEdition;
		private ArrayList m_listeVariables = new ArrayList();
		private CComposantFiltreDynamique m_composantFiltre = null;
		private int m_nNextIdVariable = 0;

        private C2iExpression m_formuleIntegrerParentsHierarchiques = new C2iExpressionFaux();
        private C2iExpression m_formuleIntegrerFilsHierarchiques = new C2iExpressionFaux();
        private C2iExpression m_formuleNeConserverQueLesRacines = new C2iExpressionFaux();

		//si défini, le filtre n'a pas ses propres variables, mais il utilise
		//les variables d'un autre élément à variables
        private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesExterne = null;

		//Non sérializée
		private Hashtable m_tableValeursChamps = new Hashtable();
		
        [NonSerialized]
		private CContexteDonnee m_contexteDonnee;

		/// /////////////////////////////////////////////
		public CFiltreDynamique()
		{
		}

		/// /////////////////////////////////////////////
		public CFiltreDynamique ( CContexteDonnee contexteDonnee )
		{
			m_contexteDonnee = contexteDonnee;
		}

		/// /////////////////////////////////////////////
		public CFiltreDynamique ( IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesExternes,
			CContexteDonnee contexteDonnee)
		{
			m_elementAVariablesExterne = elementAVariablesExternes;
			m_contexteDonnee = contexteDonnee;
		}


		/// /////////////////////////////////////////////
		public Type TypeElements
		{
			get
			{
				return m_typeElementsFiltres;
			}
			set
			{
				m_typeElementsFiltres = value;
			}
		}

		/// /////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				if ( m_contexteDonnee == null )
					return -1;
				return m_contexteDonnee.IdSession;
			}
		}

		/// /////////////////////////////////////////////
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnee;
			}
			set
			{
				m_contexteDonnee = value;
			}
		}

        //-----------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }

		/// /////////////////////////////////////////////
		public IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesExterne
		{
			get
			{
				return m_elementAVariablesExterne;
			}
			set
			{
				m_elementAVariablesExterne = value;
			}
		}

		/// /////////////////////////////////////////////
		public C2iWnd FormulaireEdition
		{
			get
			{
				if ( m_formulaireEdition == null )
				{
					m_formulaireEdition = new C2iWndFenetre();
					m_formulaireEdition.Size = new System.Drawing.Size ( 350, 200 );
				}
				return m_formulaireEdition;
			}
			set
			{
				m_formulaireEdition = value;
			}
		}

        /// /////////////////////////////////////////////
        public C2iExpression FormuleIntegrerParentsHierarchiques
        {
            get
            {
                return m_formuleIntegrerParentsHierarchiques;
            }
            set
            {
                m_formuleIntegrerParentsHierarchiques = value;
            }
        }

        /// /////////////////////////////////////////////
        public C2iExpression FormuleIntegrerFilsHierarchiques
        {
            get
            {
                return m_formuleIntegrerFilsHierarchiques;
            }
            set
            {
                m_formuleIntegrerFilsHierarchiques = value;
            }
        }

        /// /////////////////////////////////////////////
        public C2iExpression FormuleNeConserverQueLesRacines
        {
            get
            {
                return m_formuleNeConserverQueLesRacines;
            }
            set
            {
                m_formuleNeConserverQueLesRacines = value;
            }
        }


        //-------------------------------------------------
        public virtual IVariableDynamique[] ListeVariables
        {
            get
            {
                List<IVariableDynamique> lst = new List<IVariableDynamique>();
                if (m_elementAVariablesExterne != null && !m_elementAVariablesExterne.Equals(this))
                    lst.AddRange(m_elementAVariablesExterne.ListeVariables);
                lst.AddRange((IVariableDynamique[])m_listeVariables.ToArray(typeof(IVariableDynamique)));
                return lst.ToArray();
            }
        }

        //-------------------------------------------------
        public virtual void AddVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExterne != null)
            {
                m_elementAVariablesExterne.AddVariable(variable);
                return;
            }
            m_listeVariables.Add(variable);
        }

        //-------------------------------------------------
        /// <summary>
        /// Ajoute une variable au filtre et non pas à l'élément externe s'il y en a un
        /// </summary>
        /// <param name="variable"></param>
        public void AddVariablePropreAuFiltre(IVariableDynamique variable)
        {
            m_listeVariables.Add(variable);
        }


        //-------------------------------------------------
        public virtual void RemoveVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExterne != null)
            {
                m_elementAVariablesExterne.RemoveVariable(variable);
                return;
            }
            m_listeVariables.Remove(variable);
        }

		/*/// /////////////////////////////////////////////
		public ArrayList ListeVariables
		{
			get
			{
				if ( m_elementAVariablesExterne != null && !m_elementAVariablesExterne.Equals(this) )
					return m_elementAVariablesExterne.ListeVariables;
				return m_listeVariables;
			}
		}*/

        /// /////////////////////////////////////////////
        public IVariableDynamique GetVariableLocaleAuFiltre(string strIdVariable)
        {
            foreach (CVariableDynamique variable in m_listeVariables)
            {
                if (variable.IdVariable == strIdVariable)
                    return variable;
            }
            return null;
        }

		/// /////////////////////////////////////////////
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            IVariableDynamique variable = null;
            if (m_elementAVariablesExterne != null)
                variable = m_elementAVariablesExterne.GetVariable(strIdVariable);
            if (variable == null)
                variable = GetVariableLocaleAuFiltre(strIdVariable);
            return variable as CVariableDynamique;
        }

		/// /////////////////////////////////////////////
		public void OnChangeVariable ( IVariableDynamique variable )
		{
			if ( m_elementAVariablesExterne!= null )
				m_elementAVariablesExterne.OnChangeVariable ( variable );
			else
			{
				//Met à jour les noms et les types dans les expressions
				if ( m_composantFiltre != null )
					m_composantFiltre.OnChangeVariable ( this, (CVariableDynamique)variable );
			}
		}

		/// /////////////////////////////////////////////
		public CComposantFiltreDynamique ComposantPrincipal
		{
			get
			{
				return m_composantFiltre;
			}
			set
			{
				m_composantFiltre = value;
			}
		}

		/// /////////////////////////////////////////////
		public string GetNewIdForVariable()
		{
			if ( m_elementAVariablesExterne != null )
				return m_elementAVariablesExterne.GetNewIdForVariable();
            return CUniqueIdentifier.GetNew();
		}

		/// /////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 4;
            //3 : intégrer fils hierarchiques
            //4 : Passage des options sous forme de formule
		}

		/// /////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			try
			{
				serializer.AttacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );
				serializer.TraiteType ( ref m_typeElementsFiltres );
			
				result = serializer.TraiteArrayListOf2iSerializable ( m_listeVariables );

				I2iSerializable obj = m_composantFiltre;
				result = serializer.TraiteObject ( ref obj );
				m_composantFiltre = (CComposantFiltreDynamique)obj;

				serializer.TraiteInt ( ref m_nNextIdVariable );

				obj = m_formulaireEdition;
				result = serializer.TraiteObject ( ref obj );
				if ( !result )
					return result;
				m_formulaireEdition = (C2iWnd)obj;

				if ( nVersion >= 1 )
				{
					//Serialize les valeurs des variables
					foreach ( CVariableDynamique variable in m_listeVariables )
					{
						if ( variable.IsChoixUtilisateur() )
						{
							object valeur = m_tableValeursChamps[variable.IdVariable];
							if ( valeur != null && variable.TypeDonnee.IsArrayOfTypeNatif )
							{
								IList lst = (IList)valeur;
								result = serializer.TraiteListeObjetsSimples ( ref lst );
								valeur = lst;
							}
							else
								result = serializer.TraiteObjetSimple ( ref valeur );
							if ( !result )
								return result;
							if ( serializer.Mode == ModeSerialisation.Lecture )
								SetValeurChamp ( variable, valeur );
						}
					}				
				}
                if (nVersion >= 2 && nVersion < 4)
                {
                    bool bTmp = false;
                    serializer.TraiteBool(ref bTmp );
                    if ( bTmp )
                        FormuleIntegrerParentsHierarchiques = new C2iExpressionConstante(true);
                    else
                        FormuleIntegrerParentsHierarchiques = new C2iExpressionConstante ( false );
                        
                    serializer.TraiteBool(ref bTmp);
                    if (bTmp)
                        FormuleNeConserverQueLesRacines = new C2iExpressionConstante(true);
                    else
                        FormuleNeConserverQueLesRacines = new C2iExpressionConstante(false);
                }
                if (nVersion >= 3 && nVersion < 4)
                {
                    bool bTmp = false;
                    serializer.TraiteBool(ref bTmp);
                    if (bTmp)
                        FormuleIntegrerFilsHierarchiques = new C2iExpressionConstante(true);
                    else
                        FormuleIntegrerFilsHierarchiques = new C2iExpressionConstante(false);
                }

                if (nVersion >= 4)
                {
                    serializer.TraiteObject<C2iExpression>(ref m_formuleIntegrerParentsHierarchiques);
                    serializer.TraiteObject<C2iExpression>(ref m_formuleNeConserverQueLesRacines);
                    serializer.TraiteObject<C2iExpression>(ref m_formuleIntegrerFilsHierarchiques);
                }
               


				serializer.DetacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error while reading dynamic filter|172"));
			}
			return result;
		}

		/// /////////////////////////////////////////////
		public void ResetValeursVariables()
		{
			m_tableValeursChamps.Clear();
		}

		/// /////////////////////////////////////////////
		/// Implémentation du IFournisseurProprietesDynamiques
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux )
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}

		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CVariableDynamique variable in ListeVariables)
			{
				bool bHasSubs = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
				CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable, bHasSubs);
				if (def != null)
					lst.Add(def);
			}

			return lst.ToArray();
		}


		private IFournisseurProprietesDynamiques GetFournisseurPourDefinitionChamp(CDefinitionProprieteDynamique definition, Type tp)
		{
			///si le type est un IFournisseurProprietes,
			///Regarde dans mes variables si j'en ai une qui contient un objet de ce
			///type. Si oui, c'est lui qui sera  utilisé
			///La méthode n'est pas parfaite théoriquement, mais elle fonctionne
			if (typeof(IFournisseurProprietesDynamiques).IsAssignableFrom(tp))
			{
				if (m_elementAVariablesExterne != null)
				{
					foreach (IVariableDynamique variable in m_elementAVariablesExterne.ListeVariables)
					{
						object valeur = m_elementAVariablesExterne.GetValeurChamp(variable.IdVariable);
						if (valeur != null && tp.IsAssignableFrom(valeur.GetType()))
							return (IFournisseurProprietesDynamiques)valeur;
					}
				}
				else
				{
					foreach (object valeurVariable in m_tableValeursChamps.Values)
						if (valeurVariable != null && tp.IsAssignableFrom(valeurVariable.GetType()))
							return (IFournisseurProprietesDynamiques)valeurVariable;
				}
			}
			return null;
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
            if (objet != null)
            {
                Type tp = objet.TypeAnalyse;
                if ( tp.IsSubclassOf(GetType()) || tp == GetType())
                    return GetProprietesInstance(); ;

                CFournisseurPropDynStd four = new CFournisseurPropDynStd(true);
                IFournisseurProprietesDynamiques fourDeVariable = GetFournisseurPourDefinitionChamp(defParente, objet.TypeAnalyse);
                if (fourDeVariable == null)
                    fourDeVariable = four;
                return fourDeVariable.GetDefinitionsChamps(objet, defParente);
            }
			return new CDefinitionProprieteDynamique[0];
		}

		/// /////////////////////////////////////////////
		/// Implémentation du IFournisseurProprietesDynamiques
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente )
		{
			CDefinitionProprieteDynamique[] defs = new CDefinitionProprieteDynamique[0];
			
			if (!tp.IsSubclassOf(GetType()) && tp != GetType())
			{
                return GetDefinitionsChamps ( new CObjetPourSousProprietes ( tp), defParente);
			}
				
			return GetProprietesInstance();
			
		}

		/*
		/// /////////////////////////////////////////////
		public object GetValeurPropriete ( object objetInterroge, CDefinitionProprieteDynamique propriete)
		{
			if (!(objetInterroge is CFiltreDynamique))
			{
				IFournisseurProprietesDynamiques fournisseur = null;
				if (objetInterroge is IFournisseurProprietesDynamiques)
				{
					//Si on interroge la valeur d'une de nos variables et que cette valeur est un fournisseur de propriétés
					//demande à ce fournisseur la valeur
					if (m_elementAVariablesExterne != null)
					{
						foreach (IVariableDynamique variable in m_elementAVariablesExterne.ListeVariables)
						{
							object valeur = m_elementAVariablesExterne.GetValeurChamp(variable.Id);
							if (objetInterroge.Equals(valeur))
								fournisseur = (IFournisseurProprietesDynamiques)objetInterroge;
						}
					}
					else
					{
						foreach (object obj in m_tableValeursChamps.Values)
							if (objetInterroge.Equals(obj))
								fournisseur = (IFournisseurProprietesDynamiques)objetInterroge;
					}
				}
				if (fournisseur == null)
					fournisseur = new CFournisseurPropDynStd(false);
				return fournisseur.GetValeurPropriete(objetInterroge, propriete);
			}
			if ( propriete is CDefinitionProprieteDynamiqueVariableDynamique )
			{
				return GetValeurChamp ( ((CDefinitionProprieteDynamiqueVariableDynamique)propriete).IdChamp );
			}

			return null;
		}*/


		/// /////////////////////////////////////////////
		/// Vérifie que le filtre est intègre
		public CResultAErreur VerifieIntegrite()
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_typeElementsFiltres == null)
				result.EmpileErreur(I.T("The type of filtered elements must be defined|173"));
			if ( m_composantFiltre != null )
			{
				result = m_composantFiltre.VerifieIntegrite(this);
			}
			return result;
		}

		/// /////////////////////////////////////////////
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
			if ( m_elementAVariablesExterne != null )
				return m_elementAVariablesExterne.SetValeurChamp ( variable, valeur );

			if ( variable != null )
			{
				SetValeurChamp ( variable.IdVariable, valeur );
			}

			return CResultAErreur.True;
		}

		/// /////////////////////////////////////////////
        public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            if (m_elementAVariablesExterne != null && GetVariableLocaleAuFiltre(strIdVariable) == null)
            {
                return m_elementAVariablesExterne.SetValeurChamp(GetVariable(strIdVariable), valeur);
            }
            m_tableValeursChamps[strIdVariable] = valeur;
            return CResultAErreur.True;
        }

		/// /////////////////////////////////////////////
		public object GetValeurChamp(IVariableDynamique variable)
		{
			if ( m_elementAVariablesExterne!= null && GetVariableLocaleAuFiltre( variable.IdVariable ) == null)
				return m_elementAVariablesExterne.GetValeurChamp ( variable );
			if ( variable == null )
				return null;
			if(  variable is CVariableDynamiqueCalculee )
			{
				CVariableDynamiqueCalculee variableCalculee = (CVariableDynamiqueCalculee)variable;
				return variableCalculee.GetValeur ( this );
			}
			else if ( variable is CVariableDynamiqueListeObjets )
			{
				CVariableDynamiqueListeObjets variableListe = (CVariableDynamiqueListeObjets)variable;
				return variableListe.GetValeur ( this );
			}
			else
			{
				object valeur = m_tableValeursChamps[variable.IdVariable];
				if ( valeur == null && variable is CVariableDynamiqueSaisie )
					return ((CVariableDynamiqueSaisie)variable).GetValeurParDefaut();
				return valeur;
			}
		}

		/// /////////////////////////////////////////////
        public object GetValeurChamp(string strIdVariable)
        {
            if (m_elementAVariablesExterne != null && GetVariableLocaleAuFiltre(strIdVariable) == null)
            {
                return m_elementAVariablesExterne.GetValeurChamp(strIdVariable);
            }
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return GetValeurChamp(variable);
            return null;
        }

		/// /////////////////////////////////////////////
		///Le data du result contient le filtre
		public CResultAErreur GetFiltreData (  )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( ComposantPrincipal == null )
			{
				result.Data = null;
				return result;
			}
			if ( m_typeElementsFiltres == null )
			{
				result.EmpileErreur(I.T("The type of filter elements isn't valid|174"));
				return result;
			}
			string strTable = CContexteDonnee.GetNomTableForType ( m_typeElementsFiltres );
			if ( strTable == "" )
			{
				result.EmpileErreur(I.T("No table is associated with @1|175",DynamicClassAttribute.GetNomConvivial(m_typeElementsFiltres)));
				return result;
			}

            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(this);
			CFiltreDataAvance filtre = new CFiltreDataAvance ( strTable, "" );
            if (FormuleIntegrerParentsHierarchiques != null)
            {
                result = FormuleIntegrerParentsHierarchiques.Eval(ctxEval);
                if (result && result.Data is bool && (bool)result.Data)
                    filtre.IntergerParentsHierarchiques = true;
                else
                    filtre.IntergerParentsHierarchiques = false;
            }
            if (FormuleIntegrerFilsHierarchiques != null)
            {
                result = FormuleIntegrerFilsHierarchiques.Eval(ctxEval);
                if (result && result.Data is bool && (bool)result.Data)
                    filtre.IntegrerFilsHierarchiques = true;
                else
                    filtre.IntegrerFilsHierarchiques = false;
            }
            if (FormuleNeConserverQueLesRacines != null)
            {
                result = FormuleNeConserverQueLesRacines.Eval(ctxEval);
                if (result && result.Data is bool && (bool)result.Data)
                    filtre.NeConserverQueLesRacines = true;
                else
                    filtre.NeConserverQueLesRacines = false;
            }
            result = CResultAErreur.True;

			result = m_composantFiltre.GetComposantFiltreData ( this, filtre );
			if ( !result )
			{
				result.Data = null;
				result.EmpileErreur(I.T("Error in filter|176"));
				return result;
			}
			CComposantFiltre composant = (CComposantFiltre)result.Data;
			if ( composant == null )
				filtre.Filtre = "1=1";
			else
				filtre.ComposantPrincipal = composant;
			result.Data = filtre;
			return result;
		}

        /// /////////////////////////////////////////////
        public CResultAErreur GetFormuleEquivalente()
        {
            CResultAErreur result = CResultAErreur.True;
            if (ComposantPrincipal == null)
            {
                result.Data = null;
                return result;
            }
            if (m_typeElementsFiltres == null)
            {
                result.EmpileErreur(I.T("The type of filter elements isn't valid|174"));
                return result;
            }
            result = m_composantFiltre.GetComposantExpression(this);
            if (!result)
            {
                result.Data = null;
                result.EmpileErreur(I.T("Error in filter|176"));
                return result;
            }
            return result;
        }

		/// /////////////////////////////////////////////
		public bool IsVariableUtilisee ( IVariableDynamique variable )
		{
			if ( variable is CVariableDynamiqueSysteme )
				return true;
			if ( m_composantFiltre == null )
				return false;
			return m_composantFiltre.IsVariableUtilisee ( (CVariableDynamique) variable );
		}

		/// /////////////////////////////////////////////
		public void ResetCache( object objet)
		{
			//Fonction non prise en charge
		}

		/// /////////////////////////////////////////////
		public void ResetCache()
		{
			//Fonction non prise en charge
		}

		/// /////////////////////////////////////////////
		public bool CacheEnabled
		{
			get
			{
				//Fonction non prise en charge
				return false;
			}
			set
			{
				//Fonction non prise en charge
			}
		}

		//----------------------------------------------------------------------------------
		public object Clone()
		{
			/*CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
			Serialize ( serializer );
			string strData = serializer.String;
			
			serializer = new CStringSerializer ( strData, ModeSerialisation.Lecture );
			filtreCopie.Serialize ( serializer );
			return filtreCopie;*/
			CFiltreDynamique filtreCopie = new CFiltreDynamique(m_elementAVariablesExterne, ContexteDonnee);
			C2iSerializer.CloneTo ( this, filtreCopie );
			return filtreCopie;
		}
	}
}
