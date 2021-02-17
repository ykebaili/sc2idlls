using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionChamp.
	/// </summary>
	[Serializable]
	public class C2iExpressionChamp : C2iExpressionMethodeOuPropriete
	{
		//Contient la liste des proprietés amenant au champ !!
		private CDefinitionProprieteDynamique m_definitionPropriete = null;


		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionChamp()
		{
		}

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionChamp( CDefinitionProprieteDynamique definitionPropriete )
		{
			m_definitionPropriete = definitionPropriete;
		}

		/// ///////////////////////////////////////////////////////////
		public override bool CanBeArgumentExpressionObjet
		{
			get
			{
				return true;
			}
		}


		/// //////////////////////////////////// /////////////////////////////////
		public CDefinitionProprieteDynamique DefinitionPropriete
		{
			get
			{
				return m_definitionPropriete;
			}
			set
			{
				m_definitionPropriete = value;
			}
		}


		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( objetPourSousProprietes == null )
				return result;
			CDefinitionProprieteDynamique[] props = fournisseur.GetDefinitionsChamps(objetPourSousProprietes);
            CDefinitionProprieteDynamique defSansCleTypeChamp = null;
			foreach ( CDefinitionProprieteDynamique defTrouvee in  props)
			{
                //Stef 6/1/2012, on cherche en priorité les match exacts
                //si on ne trouve pas, se contente du nom de champ, sans clé type
				if(  defTrouvee.Nom.ToUpper() == m_definitionPropriete.Nom.ToUpper() || 
					defTrouvee.NomPropriete.ToUpper() == m_definitionPropriete.Nom.ToUpper() )
				{
					m_definitionPropriete = (CDefinitionProprieteDynamique)defTrouvee.Clone();
					return result;
				}
                if ( defTrouvee.NomProprieteSansCleTypeChamp.ToUpper() == DefinitionPropriete.NomProprieteSansCleTypeChamp.ToUpper() )
                    defSansCleTypeChamp = defTrouvee;
			}
            if (defSansCleTypeChamp != null)
                m_definitionPropriete = defSansCleTypeChamp;
			//result.EmpileErreur(I.T("The @1 field doesn't exist|107", m_definitionPropriete.Nom));
            //Stef le 20/10/2011, ce n'est plus une erreur, mais l'analyseur syntaxique aura en charge
            //de vérifier en fin d'analyse qu'il n'y a pas de C2iExpressionChamp non typée.
            //Il peut s'agir d'une variable non encore detectée par l'analyseur syntaxique.
			return result;
		}


		
		/// //////////////////////////////////// /////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string GetString()
		{
			return CaracteresControleAvant+ "["+m_definitionPropriete.Nom+"]";
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "FIELD";
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_definitionPropriete.TypeDonnee;
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CObjetPourSousProprietes GetObjetPourSousProprietes()
		{
			return m_definitionPropriete.GetObjetPourAnalyseSousProprietes();
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_definitionPropriete == null )
				result.EmpileErreur (I.T("Missing field definition|108"));
			else
			{
				if ( m_definitionPropriete.TypeDonnee == null )
				{
					result.EmpileErreur(I.T("The @1 field doesn't exist|107", m_definitionPropriete.Nom));
				}
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				object valeur = ctx.GetValeurChamp(m_definitionPropriete);
				result.Data = valeur;
			}
			catch(Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error during '@1' field evaluation|109",m_definitionPropriete.Nom));
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//V2 : Ajout CaracteresAvant
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( nVersion < 1 )
			{
				if ( serializer.Mode == ModeSerialisation.Lecture )
					m_definitionPropriete = new CDefinitionProprieteDynamiqueDotNet();
				result = m_definitionPropriete.Serialize ( serializer );
			}
			else
			{
				I2iSerializable obj = m_definitionPropriete;
				result = serializer.TraiteObject ( ref obj );
				if ( !result )
					return result;
				m_definitionPropriete = (CDefinitionProprieteDynamique)obj;
			}
			if ( nVersion >= 2 )
			{
				string strAvant = CaracteresControleAvant;
				serializer.TraiteString ( ref strAvant );
				CaracteresControleAvant = strAvant;
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public override CArbreDefinitionsDynamiques GetArbreProprietesAccedees ( CArbreDefinitionsDynamiques arbreEnCours )
		{
			CArbreDefinitionsDynamiques newArbre = new CArbreDefinitionsDynamiques ( DefinitionPropriete );
			arbreEnCours.AddSousArbre ( newArbre );
			return newArbre;
		}
	}
}
