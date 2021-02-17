using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.expression;
using sc2i.data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Fournit la liste des champs correspondant à un type
	/// </summary>
	[Serializable]
	public class CFournisseurProprietesDynamiquesForStructureExportSimple : IFournisseurProprietesDynamiques
	{
		/// ///////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux )
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}
		/// ///////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente )
		{
			ArrayList lstProprietes = new ArrayList();
			GetDefinitionsChamps ( tp, nNbNiveaux, lstProprietes, "", "", defParente );
			lstProprietes.Sort();
			return (CDefinitionProprieteDynamique[])lstProprietes.ToArray(typeof(CDefinitionProprieteDynamique));
		}

		/// ///////////////////////////////////////////////////////////
		protected virtual void GetDefinitionsChamps ( Type tp, int nProfondeur, ArrayList lstProps, string strCheminConvivial, string strCheminReel, CDefinitionProprieteDynamique defParente )
		{
			if ( nProfondeur < 0 )
				return;

			CStructureTable structure = null;
			try
			{
				structure = CStructureTable.GetStructure(tp);
			}
			catch 
			{
				return;
			}

			//Ajoute les relations parentes
			foreach ( CInfoRelation relation in structure.RelationsParentes )
			{
				PropertyInfo propInfo = tp.GetProperty ( relation.Propriete );
				if ( propInfo != null )
				{
					lstProps.Add ( new CDefinitionProprieteDynamiqueRelation (
						strCheminConvivial+relation.NomConvivial,
						strCheminReel+relation.Propriete,
						relation, 
						new CTypeResultatExpression ( propInfo.PropertyType, false )));
				}
			}
			//Ajoute les relations filles
			foreach ( CInfoRelation relation in structure.RelationsFilles )
			{
				PropertyInfo propInfo = tp.GetProperty ( relation.Propriete );
				if ( propInfo != null )
				{
					object[] attribs = propInfo.GetCustomAttributes ( typeof(RelationFilleAttribute), true);
					if ( attribs.Length != 0 )
					{
						Type tpFille = ((RelationFilleAttribute)attribs[0]).TypeFille;
						lstProps.Add ( new CDefinitionProprieteDynamiqueRelation (
							strCheminConvivial+relation.NomConvivial,
							strCheminReel+relation.Propriete,
							relation, 
							new CTypeResultatExpression ( tpFille, true )));
					}
				}
			}

			/*//à faire Ajoute les relations TypeId
			foreach ( RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds )
			{
				if ( relation.IsAppliqueToType ( tp ) )
				{
					lstProps.Add ( new CDefinitionProprieteDynamiqueRelationTypeId(relation) );
				}
			}*/
	
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

	}
}
