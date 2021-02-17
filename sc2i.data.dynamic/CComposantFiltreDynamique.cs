using System;
using System.Collections;

using sc2i.expression;
using sc2i.common;
using sc2i.data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CComposantFiltreDynamique.
	/// </summary>
	[Serializable]
	public abstract class CComposantFiltreDynamique : I2iSerializable
	{
        public const string c_signatureComposant = "2I_COMPOSANT_FILTRE_DYN";

		public CComposantFiltreDynamique()
		{
		}

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <returns></returns>
		private int GetNumVersion()
		{
			return 0;
		}

		/// //////////////////////////////////////////////////
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			return result;
		}


		public abstract string Description{get;}

		/// //////////////////////////////////////////////////
		public abstract bool AccepteComposantsFils{get;}

		/// //////////////////////////////////////////////////
		public abstract CComposantFiltreDynamique[] ListeComposantsFils{get;}

		/// //////////////////////////////////////////////////
		public abstract bool AddComposantFils ( CComposantFiltreDynamique composant );

		/// //////////////////////////////////////////////////
		public abstract bool InsertComposantFils ( CComposantFiltreDynamique composant, int nIndex );

		/// //////////////////////////////////////////////////
		//Retourne la position du fils enlevé
		public abstract int RemoveComposantFils ( CComposantFiltreDynamique composant );

		/// //////////////////////////////////////////////////
		public abstract void InsereDefinitionToObjetFinal ( CDefinitionProprieteDynamique def );

		/// //////////////////////////////////////////////////
		/// le data du result contient le composant
		public abstract CResultAErreur GetComposantFiltreData(CFiltreDynamique filtre, CFiltreData filtreData);

        /// //////////////////////////////////////////////////
        /// Le data du result contient l'expression correspondant à ce composant de filtre
        public abstract CResultAErreur GetComposantExpression( CFiltreDynamique filtre );

		/// //////////////////////////////////////////////////
		//Vérifie l'intégrité du composant
		public abstract CResultAErreur VerifieIntegrite( CFiltreDynamique filtre );

		/// //////////////////////////////////////////////////
		public abstract bool IsVariableUtilisee ( CVariableDynamique variable );

		/// //////////////////////////////////////////////////
		public abstract void OnChangeVariable ( CFiltreDynamique filtre, CVariableDynamique variable );

	}
}
