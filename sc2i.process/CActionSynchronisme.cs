using System;
using System.Drawing;
using System.Reflection;

using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionSynchronisme.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionSynchronisme : CActionLienSortantSimple
	{
		private C2iExpression m_formuleSourceSynchronisme = null;
		private string m_strChampSource = "";
		private C2iExpression m_formuleDestSynchronisme = null;
		private string m_strChampDest = "";

		private C2iExpression m_formuleConditionSurSource = null;
		private C2iExpression m_formuleConditionSurDest = null;

		/// /////////////////////////////////////////
		public CActionSynchronisme( CProcess process )
			:base(process)
		{
			Libelle = I.T("Add a synchronism|247");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Add a synchronism|247"),
				I.T( "Allows to synchronize a element property with another element property|248"),
				typeof(CActionSynchronisme),
				CGestionnaireActionsDisponibles.c_categorieComportement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( m_formuleSourceSynchronisme, table );
			AddIdVariablesExpressionToHashtable ( m_formuleDestSynchronisme, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			if ( !result )
				return result;
			
			I2iSerializable objet = m_formuleSourceSynchronisme;
			result = serializer.TraiteObject ( ref objet );
			m_formuleSourceSynchronisme = (C2iExpression) objet;

			objet = m_formuleDestSynchronisme;
			result = serializer.TraiteObject ( ref objet );
			m_formuleDestSynchronisme = (C2iExpression) objet;

			serializer.TraiteString ( ref m_strChampSource );
			serializer.TraiteString ( ref m_strChampDest );

			objet = m_formuleConditionSurSource;
			result = serializer.TraiteObject ( ref objet );
			m_formuleConditionSurSource = (C2iExpression) objet;

			objet = m_formuleConditionSurDest;
			result = serializer.TraiteObject ( ref objet );
			m_formuleConditionSurDest = (C2iExpression) objet;
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression  FormuleSource
		{
			get
			{
				return m_formuleSourceSynchronisme;
			}
			set
			{
				m_formuleSourceSynchronisme = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleDest
		{
			get
			{
				return m_formuleDestSynchronisme;
			}
			set
			{
				m_formuleDestSynchronisme = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string ChampSource
		{
			get
			{
				return m_strChampSource;
			}
			set
			{
				m_strChampSource = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string ChampDest
		{
			get
			{
				return m_strChampDest;
			}
			set
			{
				m_strChampDest = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleConditionSource
		{
			get
			{
				return m_formuleConditionSurSource;
			}
			set
			{
				m_formuleConditionSurSource = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleConditionDest
		{
			get
			{
				return m_formuleConditionSurDest;
			}
			set
			{
				m_formuleConditionSurDest = value;
			}
		}
		
		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if ( FormuleSource == null )
			{
				result.EmpileErreur(I.T("The object source formula is incorrect|249"));
				return result;
			}
			if ( FormuleSource.TypeDonnee.IsArrayOfTypeNatif ||
				!FormuleSource.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) ) )
				result.EmpileErreur(I.T("The object source formula must return a work entity|250"));
			Type tp = FormuleSource.TypeDonnee.TypeDotNetNatif;
			CStructureTable structure = CStructureTable.GetStructure(tp);
			if ( m_strChampSource.IndexOf(CSynchronismeDonnees.c_idChampCustom) != 0 )
			{
				bool bTrouve = false;
				foreach ( CInfoChampTable champ in structure.Champs )
					if ( champ.NomChamp == m_strChampSource )
					{
						bTrouve = true;
						if ( champ.TypeDonnee != typeof(DateTime) && champ.TypeDonnee != typeof(DateTime?) )
							result.EmpileErreur(I.T("The field @1 isn't a dates field. Only date fields can be synchronized|251",champ.NomConvivialPropOuChamp));
						break;
					}
				if ( !bTrouve )
					result.EmpileErreur(I.T("The field @1 isn't of the @2 type|252", m_strChampSource,
						DynamicClassAttribute.GetNomConvivial ( tp ) ));
			}

			if ( FormuleDest == null )
			{
				result.EmpileErreur(I.T("The target object formula isn't correct|253"));
				return result;
			}
			if ( FormuleDest.TypeDonnee.IsArrayOfTypeNatif ||
				!FormuleDest.TypeDonnee.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) ))
				result.EmpileErreur(I.T("The target object formula must return a work entity|254"));

			tp = FormuleDest.TypeDonnee.TypeDotNetNatif;
			structure = CStructureTable.GetStructure(tp);
			if ( m_strChampDest.IndexOf(CSynchronismeDonnees.c_idChampCustom) != 0 )
			{
				bool bTrouve = false;
				foreach ( CInfoChampTable champ in structure.Champs )
					if ( champ.NomChamp == m_strChampDest )
					{
						if ( champ.TypeDonnee != typeof(DateTime) && champ.TypeDonnee != typeof(DateTime?) )
							result.EmpileErreur(I.T( "The field @1 isn't a dates field. Only date fields can be synchronized|251", champ.NomConvivialPropOuChamp));	
						bTrouve = true;
						break;
					}
				if ( !bTrouve )
					result.EmpileErreur(I.T("The field @1 isn't of the @2 type|252",m_strChampDest,
						DynamicClassAttribute.GetNomConvivial ( tp ) ));
			}

					
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			//Evalue l'objet source
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process );
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = FormuleSource.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in object source formula|255"));
				return result;
			}
			//Pas d'objet source, pas grave
			if ( result.Data == null )
				return result;
			if ( !(result.Data is CObjetDonneeAIdNumerique) )
			{
				result.EmpileErreur(I.T("The source object formula must return a work entity|257"));
				return result;
			}
			CObjetDonneeAIdNumerique objetSource = (CObjetDonneeAIdNumerique)result.Data;

			result = FormuleDest.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in target object formula|256"));
				return result;
			}
			//Pas d'objet dest, pas grave
			if ( result.Data == null )
				return result;
			if ( !(result.Data is CObjetDonneeAIdNumerique) )
			{
				result.EmpileErreur(I.T( "The target object formula must return a work entity|254"));
				return result;
			}
			CObjetDonneeAIdNumerique objetDest = (CObjetDonneeAIdNumerique)result.Data;

			CSynchronismeDonnees.CreateSynchronisme ( 
				contexte.ContexteDonnee,
				objetSource,
				objetDest,
				ChampSource,
				ChampDest,
				FormuleConditionSource,
				FormuleConditionDest );

			return result;
		}

	


	}
	
}
