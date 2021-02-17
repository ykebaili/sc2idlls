using System;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionCreerListeObjetsDonnee.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionCreerListeObjetsDonnee : CActionFonction
	{
		private CFiltreDynamique m_filtreDynamique;

		private bool m_bCompterSeulement = false;
		private bool m_bAppliquerFiltreParDefaut = true;
	
		/// /////////////////////////////////////////////////////////
		public CActionCreerListeObjetsDonnee( CProcess process)
			:base(process)
		{
			m_filtreDynamique = null;
			Libelle = I.T("Create a list|151");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Create an object list|152"),
				I.T("Allows the object list creation from a dynamic filter|153"),
				typeof(CActionCreerListeObjetsDonnee),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			base.AddIdVariablesNecessairesInHashtable ( table );
			//A FAIRE
		}

		/// /////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
				if ( m_bCompterSeulement )
					return new CTypeResultatExpression ( typeof(int), false );
				if ( Filtre.TypeElements == null )
					return new CTypeResultatExpression ( typeof(string), true);
				return new CTypeResultatExpression(Filtre.TypeElements, true);
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool AppliquerFiltreParDefaut
		{
			get
			{
				return m_bAppliquerFiltreParDefaut;
			}
			set
			{
				m_bAppliquerFiltreParDefaut = value;
			}
		}


		/// /////////////////////////////////////////////////////////
		public CFiltreDynamique Filtre
		{
			get
			{
				if ( m_filtreDynamique == null )
					m_filtreDynamique = new CFiltreDynamique(Process, Process.ContexteDonnee);
				return m_filtreDynamique;
			}
			set
			{
				m_filtreDynamique = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool CompterUniquement
		{
			get
			{
				return m_bCompterSeulement;
			}
			set
			{
				m_bCompterSeulement = value;
			}
		}


		/// /////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
		}

		/// /////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base .MySerialize(serializer);
			if ( !result )
				return result;

			I2iSerializable objet = m_filtreDynamique;
			result = serializer.TraiteObject ( ref objet, Process, Process.ContexteDonnee );
			if ( !result )
			{
				result.EmpileErreur(I.T("Filter serialisation error|154"));
				return result;
			}
			m_filtreDynamique = (CFiltreDynamique)objet;

			if ( nVersion >= 1 )
				serializer.TraiteBool ( ref m_bCompterSeulement );
			else
				m_bCompterSeulement = false;

			if ( nVersion >= 2 )
				serializer.TraiteBool ( ref m_bAppliquerFiltreParDefaut ) ;
			else
				m_bAppliquerFiltreParDefaut = true;

			return result;
		}

		/// /////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();
			if ( Filtre.TypeElements == null )
				result.EmpileErreur(I.T("The create action objects type of list isn't defined|155"));
			return result;
		}



		/// /////////////////////////////////////////////////////////
		/// result.Data contient le CListeObjetDonnee
		public CResultAErreur CalculeListe ( CContexteDonnee contexteDonnee )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Filtre.TypeElements == null )
			{
				result.EmpileErreur(I.T( "The objects created type isn't defined|156"));
				return result;
			}
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexteDonnee, Filtre.TypeElements, m_bAppliquerFiltreParDefaut );
			liste.PreserveChanges = true;
			if ( m_filtreDynamique == null )
			{
				result.Data = liste;
				return result;
			}
				
			m_filtreDynamique.ContexteDonnee = contexteDonnee;
			m_filtreDynamique.ElementAVariablesExterne = Process;

			result = m_filtreDynamique.GetFiltreData ();
			if ( !result )
			{
				result.EmpileErreur(I.T("Error during the filter evaluation|157"));
				return result;
			}
			liste.Filtre = (CFiltreData)result.Data;
			result.Data = liste;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CalculeListe ( contexte.ContexteDonnee );
			if ( result )
			{
				if ( !m_bCompterSeulement )
					Process.SetValeurChamp ( VariableResultat, ((CListeObjetsDonnees)result.Data).ToArrayList() );
				else if ( result.Data is CListeObjetsDonnees )
					Process.SetValeurChamp ( VariableResultat, ((CListeObjetsDonnees)result.Data).CountNoLoad );
				else
					Process.SetValeurChamp ( VariableResultat, 0 );
			}
			return result;
		}
	}
}
