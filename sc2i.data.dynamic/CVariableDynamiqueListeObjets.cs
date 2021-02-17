using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CVariableDynamiqueListeObjets.
	/// </summary>
	public class CVariableDynamiqueListeObjets : CVariableDynamique
	{
		private CFiltreDynamique m_filtreDynamique;
		
		/// ////////////////////////////////////////////////////////
		public CVariableDynamiqueListeObjets()
		{
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueListeObjets( IElementAVariablesDynamiques elementAVariables )
			:base ( elementAVariables )
		{
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Object list|70");
			}
		}

		/// ////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( m_filtreDynamique != null )
					return new CTypeResultatExpression ( m_filtreDynamique.TypeElements, true );
				return new CTypeResultatExpression ( typeof(string), true );
			}
		}

		/// ///////////////////////////////////////////
		public CFiltreDynamique FiltreDynamique
		{
			get
			{
				if (m_filtreDynamique == null )
					m_filtreDynamique = new CFiltreDynamique();
				return m_filtreDynamique;
			}
			set
			{
				m_filtreDynamique = value;
			}
		}

		/// ///////////////////////////////////////////
		public override bool IsChoixParmis()
		{
			return false;

		}

		/// ///////////////////////////////////////////
		public override bool IsChoixUtilisateur()
		{
			return false;
		}

		// ///////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}
		
		/// ///////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			I2iSerializable objet = (I2iSerializable)m_filtreDynamique;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_filtreDynamique = (CFiltreDynamique)objet;
			return result;
		}

		/// ///////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				return new ArrayList();
			}
		}

		/// ///////////////////////////////////////////
		public object GetValeur ( IElementAVariablesDynamiquesAvecContexteDonnee elementInterroge )
		{
			if ( m_filtreDynamique == null )
				return null;
			m_filtreDynamique.ElementAVariablesExterne = elementInterroge;
			CResultAErreur result = m_filtreDynamique.GetFiltreData();
			if ( !result )
				return null;
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( elementInterroge.ContexteDonnee, m_filtreDynamique.TypeElements );
			liste.Filtre = (CFiltreData)result.Data;
			return liste.ToArray ( m_filtreDynamique.TypeElements );
		}


	}
}
