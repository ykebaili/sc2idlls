using System;
using System.Collections;
using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.data;
using System.Data;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	#region class CElementMultiStructureExport
	//Un élément d'une structure multi export ( requete ou structure de données
	[Serializable]
	public class CElementMultiStructureExport : I2iSerializable
	{
		private string m_strPrefixe="";
		private string m_strLibelle="";
		private IDefinitionJeuDonnees m_definition = null;
		private CMultiStructureExport m_multiStructure = null;

		//---------------------------------
		public CElementMultiStructureExport()
		{
		}
		//---------------------------------
		public CElementMultiStructureExport(CMultiStructureExport multistructure)
		{
			m_multiStructure = multistructure;
		}

		//---------------------------------
		public CMultiStructureExport MultiStructure
		{
			get
			{
				return m_multiStructure;
			}
			set
			{
				m_multiStructure = value;
			}
		}

		//---------------------------------
		public string Prefixe
		{
			get
			{
				return m_strPrefixe;
			}
			set
			{
				m_strPrefixe = value;
			}
		}

		//---------------------------------
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return m_strLibelle;
			}
			set
			{
				m_strLibelle = value;
			}
		}

	

		//---------------------------------
		public IDefinitionJeuDonnees DefinitionJeu
		{
			get
			{
                if (m_definition != null)
                    m_definition.ElementAVariablesExterne = m_multiStructure;
                m_definition.ContexteDonnee = m_multiStructure.ContexteDonnee;
				return m_definition;
			}
			set
			{
				m_definition = value;
				if ( m_definition != null )
					m_definition.ElementAVariablesExterne = m_multiStructure;
			}
		}

		//---------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//---------------------------------
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion	 (ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strLibelle );
			serializer.TraiteString ( ref m_strPrefixe );
			I2iSerializable objet = m_definition;
			result = serializer.TraiteObject ( ref objet );
			m_definition = (IDefinitionJeuDonnees)objet;
			return result;
		}
	}

	#endregion
	/// <summary>
	/// Contient plusieurs structure d'export, chacun identifiée par un prefix qui
	/// apparait en tête des tables
	/// </summary>
	public class CMultiStructureExport : MarshalByRefObject, I2iSerializable, IElementAVariablesDynamiquesAvecContexteDonnee
	{
		public const string c_idFichier = "2I_MULTI_EXPORT";
		private C2iWndFenetre m_formulaireEdition;
		private ArrayList m_listeVariables = new ArrayList();
		private int m_nNextIdVariable = 0;
		//Non sérializée
		private Hashtable m_tableValeursChamps = new Hashtable();

		[NonSerialized]
		private CContexteDonnee m_contexteDonnee = null;


		private ArrayList m_listeDefinitionsJeux = new ArrayList();

		//---------------------------------
		public CMultiStructureExport()
		{
		}


		//---------------------------------
		public CMultiStructureExport( CContexteDonnee contexte)
		{
			m_contexteDonnee = contexte;
		}

		//---------------------------------
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnee;
			}
			set
			{
				m_contexteDonnee = value;
				foreach ( CElementMultiStructureExport element in m_listeDefinitionsJeux )
					element.DefinitionJeu.ContexteDonnee = value;
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

		//---------------------------------
		public CElementMultiStructureExport GetDefinition ( int nIndice )
		{
			if ( nIndice < m_listeDefinitionsJeux.Count && nIndice >= 0 )
				return ( CElementMultiStructureExport)m_listeDefinitionsJeux[nIndice];
			return null;
		}

		//---------------------------------
		public void AddDefinition ( CElementMultiStructureExport definition)
		{
			m_listeDefinitionsJeux.Add ( definition );
			definition.MultiStructure = this;
		}

		//---------------------------------
		public void RemoveDefinition ( CElementMultiStructureExport definition )
		{
			m_listeDefinitionsJeux.Remove ( definition );
		}

		//---------------------------------
		public int GetNbDefinitions()
		{
			return m_listeDefinitionsJeux.Count;
		}

		//---------------------------------
		public CElementMultiStructureExport[] Definitions
		{
			get
			{
				return ( CElementMultiStructureExport[] )m_listeDefinitionsJeux.ToArray ( typeof(CElementMultiStructureExport));
			}
		}

		//---------------------------------
		/// <summary>
		/// Type des données qu'il est possible de passer en entrée ( dans la fonction
		/// getDonnee (liste). Peut être null
		/// </summary>
		public Type TypeDonneesEntree
		{
			get
			{
				Type tp = null;
				foreach ( CElementMultiStructureExport elt in Definitions )
				{
					if ( tp == null )
						tp = elt.DefinitionJeu.TypeDonneesEntree;
					else
						if ( tp != elt.DefinitionJeu.TypeDonneesEntree )
					{
						tp = null;
						break;
					}
				}
				return tp;
			}
			set
			{
			}
		}
		
		//---------------------------------
		private int GetNumVersion()
		{
			return 0;
		}


		//---------------------------------
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;


			serializer.AttacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );

			result = serializer.TraiteArrayListOf2iSerializable ( m_listeVariables );
			if ( !result )
				return result;

			result = serializer.TraiteArrayListOf2iSerializable ( m_listeDefinitionsJeux, this );
			if ( !result )
				return result;
 
			
			I2iSerializable objet = m_formulaireEdition;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
            m_formulaireEdition = (C2iWndFenetre)objet;

			
			serializer.TraiteInt ( ref m_nNextIdVariable );

			serializer.DetacheObjet(typeof(IElementAVariablesDynamiquesBase), this );
			return result;
		}

        public C2iWndFenetre Formulaire
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

		#region Membres de IElementAVariablesDynamiques

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

		/// /////////////////////////////////////////////////////////////
		public string GetNewIdForVariable()
		{
            return CUniqueIdentifier.GetNew();
		}

		/// /////////////////////////////////////////////////////////////
		public void OnChangeVariable(IVariableDynamique variable)
		{
			
		}

        //-------------------------------------------------
        public virtual IVariableDynamique[] ListeVariables
        {
            get
            {
                return (IVariableDynamique[])m_listeVariables.ToArray(typeof(IVariableDynamique));
            }
        }

        //-------------------------------------------------
        public virtual void AddVariable(IVariableDynamique variable)
        {
            m_listeVariables.Add(variable);
        }

        //-------------------------------------------------
        public virtual void RemoveVariable(IVariableDynamique variable)
        {
            m_listeVariables.Remove(variable);
        }

		/// /////////////////////////////////////////////////////////////
		public bool IsVariableUtilisee(IVariableDynamique variable)
		{
			if ( variable is CVariableDynamiqueSysteme )
				return true;
			return false;
		}

		/// /////////////////////////////////////////////////////////////
		public CVariableDynamique GetVariable(string strIdVariable)
		{
			foreach ( CVariableDynamique variable in m_listeVariables )
			{
				if ( variable.IdVariable == strIdVariable )
					return variable;
			}
			return null;
		}

		#endregion

		#region Membres de IElementAVariables

		/// /////////////////////////////////////////////////////////////
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            if (variable != null)
            {
                SetValeurChamp(variable.IdVariable, valeur);
            }

            return CResultAErreur.True;
        }

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
		{
            m_tableValeursChamps[strIdVariable] = valeur;
			return CResultAErreur.True;
		}

		/// /////////////////////////////////////////////////////////////
		public void ResetValeursVariables()
		{
			m_tableValeursChamps.Clear();
		}

		/// /////////////////////////////////////////////////////////////
        public object GetValeurChamp(IVariableDynamique variable)
		{
			if ( variable == null )
				return null;
			if(  variable is CVariableDynamiqueCalculee )
			{
                object valeur = null;
                CVariableDynamiqueCalculee variableCalculee = (CVariableDynamiqueCalculee)variable;
				valeur =  variableCalculee.GetValeur ( this );
                return valeur;
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
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return GetValeurChamp(variable);
            return null;
        }

		#endregion

		#region Membres de IFournisseurProprietesDynamiques

		/// /////////////////////////////////////////////////////////////
		public bool CacheEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// /////////////////////////////////////////////////////////////
		public void ResetCache()
		{
		}

		/// /////////////////////////////////////////////////////////////
		public void ResetCache(object objet)
		{
		}

		/*/// /////////////////////////////////////////////////////////////
		public object GetValeurPropriete(object objetInterroge, sc2i.expression.CDefinitionProprieteDynamique propriete)
		{
			object valeur = null;
			if (!(objetInterroge is CMultiStructureExport))
			{
				IFournisseurProprietesDynamiques fournisseur = null;
				if (objetInterroge is IFournisseurProprietesDynamiques)
				{
					//Si on interroge la valeur d'une de nos variables et que cette valeur est un fournisseur de propriétés
					//demande à ce fournisseur la valeur
					foreach (object obj in m_tableValeursChamps.Values)
						if (objetInterroge.Equals(obj))
							fournisseur = (IFournisseurProprietesDynamiques)objetInterroge;
				}
				if (fournisseur == null)
					fournisseur = new CFournisseurPropDynStd(false);
				valeur = fournisseur.GetValeurPropriete(objetInterroge, propriete);
			}
			if ( valeur == null && propriete is CDefinitionProprieteDynamiqueVariableDynamique )
			{
				return GetValeurChamp ( ((CDefinitionProprieteDynamiqueVariableDynamique)propriete).IdChamp );
			}

			return valeur;
		}*/

		/// /////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux)
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}

		/// /////////////////////////////////////////////////////////////
		
		///Si la definition demandée est une de mes variables et
		///que la valeur de cette variable fournit des propriétés,
		///retourne la valeur de cette variable
		private IFournisseurProprietesDynamiques GetFournisseurPourDefinitionChamp(CDefinitionProprieteDynamique definition, Type tp)
		{
			///si le type est un IFournisseurProprietes,
			///Regarde dans mes variables si j'en ai une qui contient un objet de ce
			///type. Si oui, c'est lui qui sera  utilisé
			///La méthode n'est pas parfaite théoriquement, mais elle fonctionne
			if (typeof(IFournisseurProprietesDynamiques).IsAssignableFrom(tp))
			{
				foreach ( object valeurVariable in m_tableValeursChamps.Values )
					if ( valeurVariable != null && tp.IsAssignableFrom (valeurVariable.GetType() ))
						return (IFournisseurProprietesDynamiques)valeurVariable;
			}
			return null;
		}

		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CVariableDynamique variable in ListeVariables)
			{
				bool bHasSubs = !variable.TypeDonnee.IsArrayOfTypeNatif && CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
				CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable, bHasSubs);
				def.Rubrique = I.T("Export structure|182");
				lst.Add(def);
			}
			return lst.ToArray();
		}

		/// /////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			CDefinitionProprieteDynamique[] defs = new CDefinitionProprieteDynamique[0];
			CFournisseurPropDynStd four = new CFournisseurPropDynStd(true);
			if ( !tp.IsSubclassOf ( GetType() ) && tp!=GetType())
			{
				IFournisseurProprietesDynamiques fourDeVariable = GetFournisseurPourDefinitionChamp(defParente, tp);
				if (fourDeVariable == null)
					fourDeVariable = four;
				defs = fourDeVariable.GetDefinitionsChamps(tp, nNbNiveaux, defParente);
				if ( defParente != null )
					return defs;
			}
			
			ArrayList lst = new ArrayList(defs);
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
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (objet != null)
			{
				lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
				if (objet.ElementAVariableInstance != null)
					lst.AddRange(objet.ElementAVariableInstance.GetProprietesInstance());
			}
			return lst.ToArray();
		}


		#endregion

		/// <summary>
		/// Retourne le dataset correspondant à la multistructure. Le data du result contient le dataset
		/// </summary>
		/// <returns></returns>
		public CResultAErreur GetDataSet ( bool bStructureOnly )
		{
			return GetDataSet ( bStructureOnly, null, null );
		}
		/// <summary>
		/// Retourne le dataset correspondant à la multistructure. Le data du result contient le dataset
		/// </summary>
		/// <param name="bStructureOnly">Seulement la structure ( pas de données )</param>
		/// <param name="listeDonnees">Liste de données à utiliser. Si non définit, chaque définition de données crée
		/// sa propre liste, sinon, c'est cette liste qui sera utilisée par défaut</param>
		/// <returns></returns>
		public CResultAErreur GetDataSet ( bool bStructureOnly, CListeObjetsDonnees listeDonnees,IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;


			//Le dataset complet
			DataSet ds = new DataSet();
			indicateur = CConteneurIndicateurProgression.GetConteneur(indicateur);
			indicateur.SetBornesSegment ( 0, m_listeDefinitionsJeux.Count );
			int NbFait = 0;
			foreach ( CElementMultiStructureExport element in m_listeDefinitionsJeux )
			{
				if(  element.DefinitionJeu != null )
				{

					//result = element.DefinitionJeu.GetDonnees ( this, listeDonnees, CElementAVariablesDynamiques.GetNewFrom ( this ), indicateur );
					result = element.DefinitionJeu.GetDonnees(this, listeDonnees, indicateur);
					if ( !result )
					{
						result.EmpileErreur(I.T("Error in the structure @1|183",element.Libelle));
						return result;
					}
					DataSet dsProv ;
					if ( result.Data is DataTable )
					{
						if ( ((DataTable)result.Data).DataSet != null )
							dsProv = ((DataTable)result.Data).DataSet;
						else
						{
							dsProv = new DataSet();
							dsProv.Tables.Add ( (DataTable)result.Data );
						}
					}
					else
						dsProv = (DataSet)result.Data;
					if ( ds.Tables.Count == 0 )
					{
						ds = dsProv;
						if ( element.Prefixe.Trim() != "" )
							foreach ( DataTable table in ds.Tables )
								table.TableName = element.Prefixe.Trim()+"_"+table.TableName;
					}
					else
					{
						//Fusionne les dataset
						//Nom de table dans dsProv->Nom de table dans ds
						Hashtable tableRenommage = new Hashtable();
						foreach ( DataTable table in dsProv.Tables )
						{
							string strNomBase = (element.Prefixe.Trim()==""?"":
								element.Prefixe.Trim()+"_")+table.TableName;
							string strNomTable = strNomBase;
							int nIndex = 0;
							while ( ds.Tables[strNomTable] != null )
							{
								nIndex++;
								strNomTable = strNomBase+"_"+nIndex.ToString();
							}
							table.TableName = strNomBase;
						}
						try
						{
							ds.Merge ( dsProv, true );
						}
						catch ( Exception exp )
						{
							result.EmpileErreur(I.T("Error while structures merging|184"));
							result.EmpileErreur ( new CErreurException ( exp ) );
							return result;
						}
					}
				}
				NbFait++;
				indicateur.SetValue ( NbFait );
			}
			if ( result )
				result.Data = ds;
			else result.Data = null;
			return result;
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefintionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (objet != null)
			{
				lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
				if (objet.ElementAVariableInstance != null)
					lst.AddRange(objet.ElementAVariableInstance.GetProprietesInstance());
			}
			return lst.ToArray();
		}

	}
}
