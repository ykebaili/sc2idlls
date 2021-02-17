using System;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CCaracteristiqueEntiteServeur : CObjetServeur
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CCaracteristiqueEntiteServeur()
			:base ()
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CCaracteristiqueEntiteServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CCaracteristiqueEntite.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CCaracteristiqueEntite);
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{	
				CCaracteristiqueEntite CaracteristiqueEntite = (CCaracteristiqueEntite)objet;
				

				if ( CaracteristiqueEntite.TypeCaracteristique == null )
				{
					result.EmpileErreur(I.T("Characteristic should be associated with a type|159"));
				}

				CRelationCaracteristiqueEntite_ChampCustomServeur relServeur = new CRelationCaracteristiqueEntite_ChampCustomServeur(IdSession);
				foreach ( CRelationCaracteristiqueEntite_ChampCustom rel in CNettoyeurValeursChamps.RelationsChampsNormales(CaracteristiqueEntite) )
				{
					CResultAErreur resultTmp = relServeur.VerifieDonnees(rel);
					if ( !resultTmp )
					{
						result.Erreur.EmpileErreurs(resultTmp.Erreur);
						result.SetFalse();
					}
				}

				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error in characteristic data|160"));
			}
			return result;
				
		}


	}
}
