using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.expression;
using System.Collections;
using System.Data;

namespace sc2i.data.dynamic
{
	public interface ITableExport : I2iSerializable
	{
		string NomTable { get;set;}

		//Indique que la table ne doit pas être calculée (pour débug)
		bool NePasCalculer { get;set;}

		IChampDeTable[] Champs { get;}

		ITableExport[] TablesFilles { get;}
		ITableExport[] GetToutesLesTablesFilles();

		void AddTableFille(ITableExport table);

		void RemoveTableFille(ITableExport table);

		CDefinitionProprieteDynamique ChampOrigine { get;}

		CResultAErreur GetFiltreDataAAppliquer(IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables);

		CResultAErreur VerifieDonnees();

		CFiltreDynamique FiltreAAppliquer { get;set;}

		bool IsOptimisable(ITableExport tableFille, Type typeDeMesElements);

		ITableExport GetTableFille(string strNomTable);

        Type TypeSource { get; set; }

		CResultAErreur InsertDataInDataSet(
			IEnumerable list,
			DataSet ds,
			ITableExport tableParente,
			int nValeurCle,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cache,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur
			);

		CResultAErreur InsertDataInDataSet(
			IEnumerable list,
			DataSet ds,
			ITableExport tableParente,
			int[] nValeursCle,
			RelationAttribute relationToParent,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cache,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur
			);

		CResultAErreur InsertColonnesInTable(DataTable table);

		CResultAErreur EndInsertData(DataSet ds);

		void AddProprietesOrigineDesChampsToTable(Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee);

		/// <summary>
		/// Retourne true si la table peut accepter un fils de plus
		/// </summary>
		/// <param name="typeTableFille"></param>
		/// <returns></returns>
		bool AcceptChilds();


	}

	public interface ITableExportAOrigineModifiable : ITableExport
	{
		new CDefinitionProprieteDynamique ChampOrigine { get;set;}
	}

}
