using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using System.Reflection;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CVariableDynamiqueSelectionObjetDonnee.
	/// </summary>
    [AutoExec("Autoexec")]
	public class CVariableDynamiqueSelectionObjetDonnee : CVariableDynamique
	{
		//Indique l'expression qui permettra de présenter les éléments de la liste
		private C2iExpression m_expressionAffichee;
		
		/// <summary>
		/// Filtre à appliquer à la liste des objets
		/// C'est le filtre de sélection qui détermine le type des objets à sélectionner
		/// </summary>
		private CFiltreDynamique m_filtreSelection = new CFiltreDynamique();

		private bool m_bRechercheRapide = false;

		/// <summary>
		/// Champ stocké pour la selection
		/// </summary>
		private C2iExpression m_expressionRetournee;

		private bool m_bCanBeNull = true;
		private string m_strTextNull = I.T("All|51");

		/// <summary>
		/// ///////////////////////////////////////////////
		/// </summary>
		public CVariableDynamiqueSelectionObjetDonnee()
			:base()
		{
			
		}

		/// ///////////////////////////////////////////////
		public bool UtiliserRechercheRapide
		{
			get
			{
				return m_bRechercheRapide;
			}
			set
			{
				m_bRechercheRapide = value;
			}
		}

		/// ///////////////////////////////////////////////
		public bool CanBeNull
		{
			get
			{
				return m_bCanBeNull;
			}
			set
			{
				m_bCanBeNull = value;
			}
		}

		/// ///////////////////////////////////////////////
		public string TextNull
		{
			get
			{
				return m_strTextNull;
			}
			set
			{
				m_strTextNull = value;
			}
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Selection|11");
			}
		}

		/// ///////////////////////////////////////////
		public override bool IsChoixParmis()
		{
			return true;

		}

		/// ///////////////////////////////////////////
		public override bool IsChoixUtilisateur()
		{
			return true;
		}

		/// ///////////////////////////////////////////
        public CVariableDynamiqueSelectionObjetDonnee(IElementAVariablesDynamiquesBase elementAVariables)
			:base ( elementAVariables )
		{
		}

        /// ///////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireVariablesDynamiques.RegisterTypeVariable(typeof(CVariableDynamiqueSelectionObjetDonnee), I.T("Selection|20078"));
        }

		/// ///////////////////////////////////////////////
		public C2iExpression ExpressionAffichee
		{
			get
			{
				return m_expressionAffichee;
			}
			set
			{
				m_expressionAffichee = value;
			}
		}

		/// ///////////////////////////////////////////////
		public CFiltreDynamique FiltreSelection
		{
			get
			{
				if (ElementAVariables != null)
				{
                    IObjetAContexteDonnee objACtx = ElementAVariables as IObjetAContexteDonnee;
                    if ( objACtx != null )
					    m_filtreSelection.ContexteDonnee = objACtx.ContexteDonnee;
					m_filtreSelection.ElementAVariablesExterne = ElementAVariables as IElementAVariablesDynamiquesAvecContexteDonnee;
				}
				return m_filtreSelection;
			}
			set
			{
				m_filtreSelection = value;
			}
		}

		/// ///////////////////////////////////////////////
		public C2iExpression ExpressionRetournee
		{
			get
			{
				return m_expressionRetournee;
			}
			set
			{
				m_expressionRetournee = value;
			}
		}

		/// ///////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( m_expressionRetournee != null )
					return m_expressionRetournee.TypeDonnee;
				return new CTypeResultatExpression(typeof(string), false);
			}
		}

		/// ///////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//2 : Ajout option recherche rapide
		}

		/// ///////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize( serializer );

			I2iSerializable obj = m_expressionAffichee;
			result = serializer.TraiteObject ( ref obj );
			m_expressionAffichee = (C2iExpression)obj;

			obj = m_expressionRetournee;
			result = serializer.TraiteObject ( ref obj );
			m_expressionRetournee = (C2iExpression)obj;

			obj = m_filtreSelection;
			result = serializer.TraiteObject ( ref obj );
			m_filtreSelection = (CFiltreDynamique)obj;

			if ( nVersion > 0 )
			{
				serializer.TraiteBool ( ref m_bCanBeNull );
				serializer.TraiteString ( ref m_strTextNull );
			}
			else
			{
				m_bCanBeNull = false;
				m_strTextNull = "";
			}

			if ( nVersion >= 2 )
			{
				serializer.TraiteBool ( ref m_bRechercheRapide );
			}
			else
				m_bRechercheRapide = false;
			return result;
		}


		//Non sérialisé, cache
		public ArrayList m_listeValeurs = null;
		/// ///////////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				if ( m_listeValeurs != null )
					return m_listeValeurs;
                IElementAVariablesDynamiquesAvecContexteDonnee eltAVar = ElementAVariables as IElementAVariablesDynamiquesAvecContexteDonnee;
                if ( eltAVar == null &&  ElementAVariables is IObjetAIContexteDonnee )
                {
                    CContexteDonnee ctx = ((IObjetAIContexteDonnee)ElementAVariables).IContexteDonnee as CContexteDonnee;
                    if ( ctx != null )
                        eltAVar = new CElementAVariablesDynamiquesAvecContexteFromIElementAVariablesDynamiques(ElementAVariables, ctx);
                }

                if (eltAVar == null || eltAVar.IdSession < 0 || m_filtreSelection == null || m_filtreSelection.TypeElements == null)
					return new object[0];

                CListeObjetsDonnees liste = new CListeObjetsDonnees(eltAVar.ContexteDonnee, m_filtreSelection.TypeElements);
				if ( FiltreSelection != null )
				{
					CResultAErreur result = FiltreSelection.GetFiltreData();
					if ( result )
						liste.Filtre = (CFiltreData)result.Data;
					else
						return new object[0];
				}
				if ( m_expressionAffichee == null || m_expressionRetournee == null )
					return new object[0];
				
				CFournisseurPropDynStd fournisseur = new CFournisseurPropDynStd(true);
				m_listeValeurs = new ArrayList();
				
				object valeurAffichee, valeurRetournee;
				foreach ( CObjetDonnee objet in liste )
				{
					CContexteEvaluationExpression contexte = new CContexteEvaluationExpression ( objet );
					CResultAErreur result = m_expressionAffichee.Eval ( contexte );
					if(  result )
					{
						valeurAffichee = result.Data;
						result = m_expressionRetournee.Eval ( contexte );
						if ( result )
						{
							valeurRetournee = result.Data;
							m_listeValeurs.Add ( new CValeurVariableDynamiqueSaisie ( valeurRetournee, valeurAffichee.ToString() ));
						}
					}
				}
				m_listeValeurs.Sort();
				if ( m_bCanBeNull )
					m_listeValeurs.Insert (0, new CValeurVariableDynamiqueSaisie(null, m_strTextNull) );
				return m_listeValeurs;
			}
		}

        /// <summary>
        /// retourne l'objet donné sélectionné à partir de la valeur retournée
        /// </summary>
        /// <remarks>
        /// Ne fonctionne pas toujours, ça ne fonctionne que si la valeur retournée
        /// est un champ DOTNET de l'entité sélectionnée
        /// </remarks>
        /// <param name="valeurRetournee"></param>
        /// <param name="contexte"></param>
        /// <returns></returns>
        public CReferenceObjetDonnee GetObjetFromValeurRetournee( object valeurRetournee )
        {
            if (valeurRetournee == null)
                return null;
            C2iExpressionChamp exp = ExpressionRetournee as C2iExpressionChamp;
            if (exp != null)
            {
                CDefinitionProprieteDynamique propDyn = exp.DefinitionPropriete;
                CDefinitionProprieteDynamiqueDotNet defDotNet = propDyn as CDefinitionProprieteDynamiqueDotNet;
                if ( defDotNet != null )
                {
                    CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance ( m_filtreSelection.TypeElements, new object[]{CContexteDonneeSysteme.GetInstance()} );
                    CStructureTable structure = CStructureTable.GetStructure ( m_filtreSelection.TypeElements );
                    CInfoChampTable info = structure.GetChampFromPropriete ( defDotNet.NomProprieteSansCleTypeChamp );
                    if (info != null)
                    {

                        CResultAErreur result = m_filtreSelection.GetFiltreData();
                        CFiltreData filtre = null;
                        if (result && result.Data is CFiltreData)
                            filtre = result.Data as CFiltreData;

                        filtre = CFiltreData.GetAndFiltre(filtre,
                            new CFiltreData(info.NomChamp + "=@1",
                                valeurRetournee));
                        if (objet.ReadIfExists(filtre))
                            return new CReferenceObjetDonnee(objet);
                    }
                        
                
                }
            }
            return null;
        }

			
	}
}
