using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Sélectionne des éléments à  partir d'un élément de base.
	/// </summary>
	////////////////////////////////////////////////////////////////////////
	public class CFiltreSynchronisation : I2iSerializable, ICloneable
	{
		public static string c_nomVariableListeUtilisateurs = "IdsUtilisateurs";

		private string m_strNomTable="";
		private CFiltreSynchronisation m_filtreParent = null;
		private ArrayList m_listeFils = new ArrayList();
		private CFiltreDynamique m_filtreDynamique = null;
		private CInfoRelation m_relationToParent = null;
		private bool m_bTouteLaTable = false;
		private bool m_bIsAutoAdd = false;
		private bool m_bIsLienToFullTableParente = false;

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation()
		{
			InitFiltreDynamique();
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation( string strNomTable )
		{
			m_strNomTable = strNomTable;
			InitFiltreDynamique();
		}

		////////////////////////////////////////////////////////////////////////
		public void InitFiltreDynamique()
		{
			m_filtreDynamique = GetFiltreDynamiqueSynchro ( CContexteDonnee.GetTypeForTable(NomTable) );
		}

		////////////////////////////////////////////////////////////////////////
		public static CFiltreDynamique GetFiltreDynamiqueSynchro ( Type type )
		{
			CFiltreDynamique filtre = new CFiltreDynamique();
			filtre.TypeElements = type;
			CVariableDynamiqueListeTypeSimple variable = new CVariableDynamiqueListeTypeSimple(filtre);
			variable.TypeDonnee2i = new C2iTypeDonnee ( TypeDonnee.tEntier );
			variable.Nom = c_nomVariableListeUtilisateurs;
			filtre.AddVariable ( variable );
			return filtre;
		}

		////////////////////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation FiltreParent
		{
			get
			{
				return m_filtreParent;
			}
		}

		////////////////////////////////////////////////////////////////////////
		public bool TouteLaTable
		{
			get
			{
				return m_bTouteLaTable;
			}
			set
			{
				m_bTouteLaTable = value;
			}
		}

		////////////////////////////////////////////////////////////////////////
		protected void DefinitParent ( CFiltreSynchronisation filtre, CInfoRelation relationToParent )
		{
			m_filtreParent = filtre;
			m_relationToParent = relationToParent;
		}

		////////////////////////////////////////////////////////////////////////
		public CInfoRelation RelationToParent
		{
			get
			{
				return m_relationToParent;
			}
		}


		////////////////////////////////////////////////////////////////////////
		public CFiltreDynamique FiltreDynamique
		{
			get
			{
				if ( m_filtreDynamique == null )
					InitFiltreDynamique();
				return m_filtreDynamique;
			}
			set
			{
				m_filtreDynamique = value;
			}
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation[] FiltresFils
		{
			get
			{
				return (CFiltreSynchronisation[]) m_listeFils.ToArray(typeof(CFiltreSynchronisation));
			}
		}

		////////////////////////////////////////////////////////////////////////
		public void AddFils ( CFiltreSynchronisation filtreFils, CInfoRelation relationToParent )
		{
			m_listeFils.Add ( filtreFils );
			filtreFils.DefinitParent ( this, relationToParent );
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation GetFilsForRelation ( CInfoRelation relation )
		{
			foreach ( CFiltreSynchronisation filtre in m_listeFils )
				if ( filtre.RelationToParent.RelationKey == relation.RelationKey )
					return filtre;
			return null;
		}

		////////////////////////////////////////////////////////////////////////
		///Ajoute toutes les compositions dans la synchro
		public void CreateFiltreForAllCompositions()
		{
			CreateFiltreForAllCompositions ( new Hashtable() );
		}

		////////////////////////////////////////////////////////////////////////
		private void CreateFiltreForAllCompositions ( Hashtable tableRelationsFaites )
		{
			/*CStructureTable structure = new CStructureTable();
			structure.FillFromType ( CContexteDonnee.GetTypeForTable ( NomTable ) );
			foreach ( CInfoRelation relationFille in structure.RelationsFilles )
			{
				if ( relationFille.Composition )
				{
					if ( tableRelationsFaites[relationFille.RelationKey] == null )
					{
						tableRelationsFaites[relationFille.RelationKey] = true;
						CFiltreSynchronisation filtreFils = GetFilsForRelation ( relationFille );
						if ( filtreFils == null )
						{
							filtreFils = new CFiltreSynchronisation ( relationFille.TableFille );
							filtreFils.m_bIsAutoAdd = true;
							filtreFils.IsLienToFullTableParente = TouteLaTable;
							AddFils ( filtreFils, relationFille );
						}
						filtreFils.CreateFiltreForAllParents((Hashtable)tableRelationsFaites.Clone());
						filtreFils.CreateFiltreForAllCompositions((Hashtable)tableRelationsFaites.Clone());
					}
				}
			}
			foreach ( CFiltreSynchronisation filtreFils in m_listeFils )
			{
				if ( filtreFils.RelationToParent.TableParente == NomTable &&
					filtreFils.RelationToParent.Composition )
				{
					if ( tableRelationsFaites[filtreFils.RelationToParent.RelationKey] == null )
					{
						tableRelationsFaites[filtreFils.RelationToParent.RelationKey] = true;
						if ( filtreFils.NomTable == filtreFils.RelationToParent.TableFille )
						{
							filtreFils.CreateFiltreForAllParents((Hashtable)tableRelationsFaites.Clone());
							filtreFils.CreateFiltreForAllCompositions((Hashtable)tableRelationsFaites.Clone());
						}
					}
				}
			}*/
		}

		////////////////////////////////////////////////////////////////////////
		public void CreateFiltreForAllParents()
		{
			CreateFiltreForAllParents ( new Hashtable() );
		}

		////////////////////////////////////////////////////////////////////////
		public bool IsAutoAdd
		{
			get
			{
				return m_bIsAutoAdd;
			}
			set
			{
				m_bIsAutoAdd = value;
			}
		}

		////////////////////////////////////////////////////////////////////////
		public bool IsLienToFullTableParente
		{
			get
			{
				return m_bIsLienToFullTableParente;
			}
			set
			{
				m_bIsLienToFullTableParente = value;
			}
		}

		////////////////////////////////////////////////////////////////////////
		protected void NettoieFiltresFils()
		{
			//Supprime les filtres vers des tables parentes qui peuvent être recalculés
			if ( m_listeFils.Count == 0 )
				return;
			ArrayList newListe = new ArrayList();
			foreach ( CFiltreSynchronisation filtre in m_listeFils )
			{
				filtre.NettoieFiltresFils();
				if ( filtre.m_listeFils.Count != 0 || 
					(filtre.FiltreDynamique != null &&
					filtre.FiltreDynamique.ComposantPrincipal != null ))
					newListe.Add ( filtre );
			}
			m_listeFils = newListe;
		}


		////////////////////////////////////////////////////////////////////////
		private void CreateFiltreForAllParents ( Hashtable tableRelationsFaites )
		{
			CStructureTable structure = CStructureTable.GetStructure(CContexteDonnee.GetTypeForTable(NomTable));
			foreach ( CInfoRelation relationParente in structure.RelationsParentes )
			{
				if ( tableRelationsFaites[relationParente.RelationKey] == null )
				{
					tableRelationsFaites[relationParente.RelationKey] = true;
					CFiltreSynchronisation filtreFils = GetFilsForRelation ( relationParente );
					if ( filtreFils == null )
					{
						filtreFils = new CFiltreSynchronisation ( relationParente.TableParente );
						filtreFils.m_bIsAutoAdd = true;
						filtreFils.IsLienToFullTableParente = TouteLaTable;
						AddFils ( filtreFils, relationParente );
					}
					filtreFils.CreateFiltreForAllParents((Hashtable)tableRelationsFaites.Clone());
				}
			}
			foreach ( CFiltreSynchronisation filtreFils in m_listeFils )
			{
				if ( tableRelationsFaites[filtreFils.RelationToParent.RelationKey] == null )
				{
					tableRelationsFaites[filtreFils.RelationToParent.RelationKey] = true;
					if ( filtreFils.NomTable == filtreFils.RelationToParent.TableFille )
						filtreFils.CreateFiltreForAllParents((Hashtable)tableRelationsFaites.Clone());
				}
			}
		}

		public class CFiltreDynamiqueParentTablePleine : CFiltreDynamique
		{
			private string m_strNomTable;
			private CInfoRelation m_relationToTableFille = null;

			public CFiltreDynamiqueParentTablePleine ( string strTableConcernee, CInfoRelation relationToTableFille )
			{
				m_strNomTable = strTableConcernee;
				m_relationToTableFille = relationToTableFille;

				//Crée un filtre sur le parent pour qu'il ait un id > 0, 
				CComposantFiltreDynamiqueValeurChamp composant = new CComposantFiltreDynamiqueValeurChamp();
				composant.IdOperateur = CComposantFiltreOperateur.c_IdOperateurSuperieurOuEgal;
				composant.Champ = new CDefinitionProprieteDynamique ( 
					relationToTableFille.ChampsParent[0],
					relationToTableFille.ChampsParent[0],
					new CTypeResultatExpression(typeof(int), false),
					false,
					true);
				composant.ConditionApplication = new C2iExpressionVrai();
				composant.ExpressionValeur = new C2iExpressionConstante(0);
				ComposantPrincipal = composant;
				TypeElements = CContexteDonnee.GetTypeForTable ( strTableConcernee );
			}

			public string NomTable
			{
				get
				{
					return m_strNomTable;
				}
			}

			public CInfoRelation RelationToTableFille
			{
				get
				{
					return m_relationToTableFille;
				}
			}
		}
				

		////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne un filtre dynamique pour la table associée en
		/// prenant en compte les filtres des parents
		/// </summary>
		/// <returns></returns>
		public CFiltreDynamique GetFiltreToElementPrincipal()
		{
			if ( TouteLaTable )
				return null;
			CFiltreDynamique filtreDynamiqueParent = null;
			if ( FiltreParent != null )
				filtreDynamiqueParent = FiltreParent.GetFiltreToElementPrincipal();
			
			Type typeElements = CContexteDonnee.GetTypeForTable ( NomTable );
			CStructureTable structure = CStructureTable.GetStructure(typeElements);

            CFiltreDynamique filtreFinal = (CFiltreDynamique)FiltreDynamique.Clone();			

			CDefinitionProprieteDynamique defToParent = null;
			//Trouve la propriété qui amène sur le parent
			if ( RelationToParent != null )
			{
				if ( RelationToParent.TableFille == NomTable )
				{
					if ( filtreDynamiqueParent != null && filtreDynamiqueParent.ComposantPrincipal != null )
					{
						Type tp = CContexteDonnee.GetTypeForTable(RelationToParent.TableParente);
						string strNomConvivial = DynamicClassAttribute.GetNomConvivial ( tp );
						//Nous somme dans une relation fille
						foreach ( CInfoRelation relationParente in structure.RelationsParentes )
							if ( relationParente.RelationKey == RelationToParent.RelationKey )
							{
								defToParent = new CDefinitionProprieteDynamiqueDotNet ( 
									strNomConvivial,
									relationParente.Propriete,
									new CTypeResultatExpression(CContexteDonnee.GetTypeForTable(relationParente.TableParente),false),
									true,
									true,
									"");
								break;
							}
						if ( defToParent == null )
						{
							defToParent = new CDefinitionProprieteDynamique (
								strNomConvivial,
								RelationToParent.RelationKey,
								new CTypeResultatExpression(tp,false),
								true,
								true,
								"");
						}
					}
			
				}
				else
				{
					//Nous sommes dans une relation parente
					foreach ( CInfoRelation relationFille in structure.RelationsFilles )
						if ( relationFille.RelationKey == RelationToParent.RelationKey )
						{
							defToParent = new CDefinitionProprieteDynamiqueDotNet ( 
								relationFille.NomConvivial,
								relationFille.Propriete,
								new CTypeResultatExpression(CContexteDonnee.GetTypeForTable(relationFille.TableFille),true),
								true,
								true,
								"");
							break;
						}
					if ( defToParent == null )
					{
						Type tp = CContexteDonnee.GetTypeForTable(RelationToParent.TableFille);
						defToParent = new CDefinitionProprieteDynamique (
							DynamicClassAttribute.GetNomConvivial ( tp )+"("+RelationToParent.NomConvivial+")",
							RelationToParent.RelationKey,
							new CTypeResultatExpression(tp,false),
							true, true );
					}
				
					if ( filtreDynamiqueParent == null )
					{
						filtreDynamiqueParent = new CFiltreDynamiqueParentTablePleine ( NomTable, RelationToParent );
					}
				}
			}
			if ( filtreDynamiqueParent != null && filtreDynamiqueParent.ComposantPrincipal != null )
			{
				filtreDynamiqueParent.ComposantPrincipal.InsereDefinitionToObjetFinal ( defToParent );

				if ( FiltreDynamique == null || FiltreDynamique.ComposantPrincipal == null )
					return filtreDynamiqueParent;

				CComposantFiltreDynamique composantEt = null;
				if ( ! (filtreDynamiqueParent.ComposantPrincipal is CComposantFiltreDynamiqueEt ) )
				{
					composantEt = new CComposantFiltreDynamiqueEt();
					composantEt.AddComposantFils ( filtreDynamiqueParent.ComposantPrincipal );
				}
				else
					composantEt = (CComposantFiltreDynamiqueEt)filtreDynamiqueParent.ComposantPrincipal;
				composantEt.AddComposantFils ( filtreFinal.ComposantPrincipal );
				filtreFinal.ComposantPrincipal = composantEt;
			}
			
			if ( filtreFinal.ComposantPrincipal == null )
				return null;
			return filtreFinal;
		}

		////////////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}

		////////////////////////////////////////////////////////////////////////
		public void CopySansFilsTo ( CFiltreSynchronisation filtre )
		{
			filtre.m_strNomTable = m_strNomTable;
			if ( m_filtreDynamique != null )
				filtre.m_filtreDynamique = (CFiltreDynamique)m_filtreDynamique.Clone();
			filtre.m_relationToParent = m_relationToParent;
			filtre.m_bTouteLaTable = m_bTouteLaTable;
			filtre.m_bIsAutoAdd = m_bIsAutoAdd;
		}

		////////////////////////////////////////////////////////////////////////
		public object Clone()
		{
			CFiltreSynchronisation filtreCopie = new CFiltreSynchronisation();
			C2iSerializer.CloneTo ( this, filtreCopie );
			return filtreCopie;
		}

		////////////////////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			serializer.TraiteString ( ref m_strNomTable );

			I2iSerializable objet = m_filtreDynamique;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_filtreDynamique = (CFiltreDynamique)objet;

			objet = m_relationToParent;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_relationToParent = (CInfoRelation)objet;

			serializer.TraiteBool ( ref m_bTouteLaTable );

			result = serializer.TraiteArrayListOf2iSerializable ( m_listeFils );
			if ( !result )
				return result;
			if ( serializer.Mode == ModeSerialisation.Lecture )
			{
				NettoieFiltresFils();
				foreach ( CFiltreSynchronisation filtre in m_listeFils )
				{
					filtre.DefinitParent ( this, filtre.RelationToParent );
				}
				
			}

			if ( nVersion >= 1 )
				serializer.TraiteBool ( ref m_bIsAutoAdd );
			return result;
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltresDynamiquesForTables CalculeFiltresForTables ()
		{
			CFiltresDynamiquesForTables filtres = new CFiltresDynamiquesForTables();
			CreateFiltreForAllParents();
			CreateFiltreForAllCompositions();
			filtres.AddFiltreSynchronisation ( this );
			return filtres;
		}

		////////////////////////////////////////////////////////////////////////
		public string NomConvivialType
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial ( CContexteDonnee.GetTypeForTable ( NomTable ) );
			}
		}
	}
}
