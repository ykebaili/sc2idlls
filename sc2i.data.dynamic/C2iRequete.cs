using System;

using System.Collections;

using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Requete SQL avec paramêtres
	/// </summary>
	[Serializable]
	public class C2iRequete : I2iSerializable, 
        IElementAVariablesDynamiquesAvecContexteDonnee, 
        IDefinitionJeuDonnees
	{
		public static string c_idFichier = "SC2I_REQUETE";

		private string m_strRequete = "";
		
		private ArrayList m_listeVariables = new ArrayList();

		private Hashtable m_tableValeursChamps = new Hashtable();

		[NonSerialized]
		private CContexteDonnee m_contexteDonnee = null;

		//Tableau croisé à appliquer à la requête
		private CTableauCroise m_tableauCroise = null;

		private C2iWnd m_formulaireEdition ;

		private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesExterne = null;

		/// <summary>
		/// si null, la requete utilisera la connexion par défaut de la session. Sinon,
		/// elle utilisera la connexion à la base utilisée pour lire les éléments du type réference
		/// </summary>
		private Type m_typeReferencePourConnexion = null;
		
		/// <summary>
		/// //////////////////////////////////
		/// </summary>
		public C2iRequete()
		{
		}

		/// //////////////////////////////////
		/// </summary>
		public C2iRequete( CContexteDonnee contexteDonnee)
		{
			m_contexteDonnee = contexteDonnee;
		}

		/// //////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
			//Version 2 : Ajout du type référence pour connexion
            //Version 3 : suppression de m_nIdNextVariable
		}

		/// //////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strRequete );

			serializer.AttacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );

			result = serializer.TraiteArrayListOf2iSerializable ( m_listeVariables );
			if ( !result )
				return result;

			I2iSerializable objet = m_formulaireEdition;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_formulaireEdition = (C2iWnd)objet;

            if (nVersion < 3)
            {
                //TESTDBKEYTODO
                int nTmp = 0;
                serializer.TraiteInt(ref nTmp);
            }

			serializer.DetacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );

            if (serializer.GetObjetAttache(typeof(CContexteDonnee)) != null)
                ContexteDonnee = (CContexteDonnee)serializer.GetObjetAttache(typeof(CContexteDonnee));
            else
                ContexteDonnee = CContexteDonneeSysteme.GetInstance();

			if ( nVersion >= 1 )
			{
				objet = m_tableauCroise;
				result = serializer.TraiteObject ( ref objet );
				if ( !result )
					return result;
				m_tableauCroise = (CTableauCroise)objet;
			}
			else
				m_tableauCroise = null;

			if ( nVersion >= 2 )
			{
				bool bHasType = m_typeReferencePourConnexion != null;
				serializer.TraiteBool ( ref bHasType );

				if ( bHasType )
					serializer.TraiteType ( ref m_typeReferencePourConnexion );
			}
			return result;
		}

		/// //////////////////////////////////
		public Type TypeReferencePourConnexion
		{
			get
			{
				return m_typeReferencePourConnexion;
			}
			set
			{
				m_typeReferencePourConnexion = value;
			}
		}


		/// //////////////////////////////////
		///<summary>
		/// ATTENTION :  Le texte de la requête utilise les noms des tables dans la base
		/// de données et non dans le contexte de données
		///</summary>
		[DynamicField("Query text")]
		public string TexteRequete
		{
			get
			{
				return m_strRequete;
			}
			set
			{
				m_strRequete = value;
			}
		}

		/// //////////////////////////////////
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

		/// //////////////////////////////////
		public CTableauCroise TableauCroise
		{
			get
			{
				return m_tableauCroise;
			}
			set
			{
				m_tableauCroise = value;
			}
		}

		/// //////////////////////////////////
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

		/// ////////////////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				if ( m_contexteDonnee != null )
					return m_contexteDonnee.IdSession;
				return -1;
			}
		}

		/// ////////////////////////////////////////////////////////
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
        public string GetNewIdForVariable()
        {
            if (m_elementAVariablesExterne != null)
                return m_elementAVariablesExterne.GetNewIdForVariable();
            return CUniqueIdentifier.GetNew();
        }

		/// /////////////////////////////////////////////
		public void OnChangeVariable(IVariableDynamique variable)
		{
			if ( m_elementAVariablesExterne != null )
				m_elementAVariablesExterne.OnChangeVariable ( variable );
			// TODO : ajoutez l'implémentation de C2iRequete.OnChangeVariable
		}

        //-------------------------------------------------
        public virtual IVariableDynamique[] ListeVariables
        {
            get
            {
                if (m_elementAVariablesExterne != null)
                    return m_elementAVariablesExterne.ListeVariables;
                return (IVariableDynamique[])m_listeVariables.ToArray(typeof(IVariableDynamique));
            }
        }

        //-------------------------------------------------
        public virtual void AddVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExterne != null)
                m_elementAVariablesExterne.AddVariable(variable);
            else
                m_listeVariables.Add(variable);
        }

        //-------------------------------------------------
        public virtual void RemoveVariable(IVariableDynamique variable)
        {
            if (m_elementAVariablesExterne != null)
                m_elementAVariablesExterne.RemoveVariable(variable);
            else
                m_listeVariables.Remove(variable);
        }

		/// /////////////////////////////////////////////
		public bool IsVariableUtilisee(IVariableDynamique variable)
		{
			if(  m_elementAVariablesExterne != null )
				return m_elementAVariablesExterne.IsVariableUtilisee ( variable );
			// TODO : ajoutez l'implémentation de C2iRequete.IsVariableUtilisee
			return false;
		}

		/// /////////////////////////////////////////////
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            if (m_elementAVariablesExterne != null)
                return m_elementAVariablesExterne.GetVariable(strIdVariable);
            foreach (CVariableDynamique variable in m_listeVariables)
            {
                if (variable.IdVariable == strIdVariable)
                    return variable;
            }
            return null;
        }

		/// ////////////////////////////////////////////////////////
        public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            if (m_elementAVariablesExterne != null)
                return m_elementAVariablesExterne.SetValeurChamp(strIdVariable, valeur);
            m_tableValeursChamps[strIdVariable] = valeur;
            return CResultAErreur.True;
        }


		/// ////////////////////////////////////////////////////////
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
			if ( m_elementAVariablesExterne != null )
				return m_elementAVariablesExterne.SetValeurChamp ( variable, valeur );
			if ( variable != null )
			{
				return SetValeurChamp ( variable.IdVariable, valeur );
			}
			return CResultAErreur.True;
		}

		/// ////////////////////////////////////////////////////////
        public object GetValeurChamp(IVariableDynamique variable)
        {
            if (m_elementAVariablesExterne != null)
                return m_elementAVariablesExterne.GetValeurChamp(variable);
            if (variable == null)
                return null;
            if (variable is CVariableDynamiqueCalculee)
            {
                CVariableDynamiqueCalculee variableCalculee = (CVariableDynamiqueCalculee)variable;
                return variableCalculee.GetValeur(this);
            }
            else
            {
                object val = m_tableValeursChamps[variable.IdVariable];
                if (val == null && variable is CVariableDynamiqueSaisie)
                    m_tableValeursChamps[variable.IdVariable] = ((CVariableDynamiqueSaisie)variable).GetValeurParDefaut();
                return m_tableValeursChamps[variable.IdVariable];
            }
        }

		/// /////////////////////////////////////////////
        public object GetValeurChamp(string strIdVariable)
        {
            if (m_elementAVariablesExterne != null)
                return m_elementAVariablesExterne.GetValeurChamp(strIdVariable);
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return GetValeurChamp(variable);
            return null;
        }



		/// /////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux )
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CVariableDynamique variable in ListeVariables)
			{
				bool bHasSubs = !variable.TypeDonnee.IsArrayOfTypeNatif && CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
				CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable, bHasSubs);
				if (def != null)
					lst.Add(def);
			}
			return lst.ToArray();
		}

		/// /////////////////////////////////////////////
		/// Implémentation du IFournisseurProprietesDynamiques
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente )
		{
			/*ITraducteurNomChamps traducteur =  null;
			if ( defParente is ITraducteurNomChamps )
				traducteur = (ITraducteurNomChamps)defParente;*/
			CFournisseurPropDynStd four = new CFournisseurPropDynStd ( true );
			ArrayList lst = new ArrayList();
			lst.AddRange ( four.GetDefinitionsChamps ( tp, nNbNiveaux, defParente));
			lst.AddRange(GetProprietesInstance());
			return (CDefinitionProprieteDynamique[])lst.ToArray ( typeof(CDefinitionProprieteDynamique) );
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
				return GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente);
			return new CDefinitionProprieteDynamique[0];
		}

		/// <summary>
		/// Fonction deleguée utilisée pour nommer les paramètres d'une requête
		/// </summary>
		public delegate string FNommeParametreRequete ( int nNumeroParametre );
	
	
		/// <summary>
		/// Le data du result contient un datatable avec le résulat de la requête
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		public CResultAErreur ExecuteRequete ( int nIdSession )
		{
			I2iRequeteServeur objetServeur = (I2iRequeteServeur)C2iFactory.GetNewObjetForSession ( "C2iRequeteServeur", typeof(I2iRequeteServeur), nIdSession );
			return objetServeur.ExecuteRequete ( this, this, false );
		}

		public CResultAErreur GetDonnees ( IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, CListeObjetsDonnees listeDonnees, IIndicateurProgression indicateur )
		{
			I2iRequeteServeur objetServeur = (I2iRequeteServeur)C2iFactory.GetNewObjetForSession ( "C2iRequeteServeur", typeof(I2iRequeteServeur), elementAVariables.IdSession );
			return objetServeur.ExecuteRequete ( this, elementAVariables, false );
		}

		/// <summary>
		/// Le data du result contient un datatable avec la structure du résultat
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		public CResultAErreur GetStructureResultat ( int nIdSession )
		{
			I2iRequeteServeur objetServeur = (I2iRequeteServeur)C2iFactory.GetNewObjetForSession ( "C2iRequeteServeur", typeof(I2iRequeteServeur), nIdSession );
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariable = this;
			if ( m_elementAVariablesExterne != null )
				elementAVariable = m_elementAVariablesExterne;
			return objetServeur.ExecuteRequete ( this, elementAVariable, true );
		}

		/// /////////////////////////////////////////////
		public string LibelleTypeDefinitionJeuDonnee
		{
			get
			{
				return "Requete";
			}
		}

		/// /////////////////////////////////////////////
		public Type TypeDonneesEntree
		{
			get
			{
				return null;
			}
		}

	}

	/// /////////////////////////////////////////////
	public interface I2iRequeteServeur
	{
		//Le data du result contient un datatable avec le résulat de la requête
		CResultAErreur ExecuteRequete ( 
            C2iRequete requete, 
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, 
            bool bStructureOnly );
	}
}
