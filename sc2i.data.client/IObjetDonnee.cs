using System;
using sc2i.common;
using sc2i.common.Restrictions;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de IObjetDonnee.
	/// </summary>
    public interface IObjetDonnee : IObjetAContexteDonnee, IObjetAModificationContextuelle
	{
		void CreateNew(params object[] valeursCles);
		void CreateNewInCurrentContexte(object[] valeursCles);
        CResultAErreur CommitEdit();

		CResultAErreur Delete();

        /// <summary>
        /// Indique une description textuelle de l'entit�
        /// </summary>
		[DynamicField("Element description")]
		string DescriptionElement{get;}

        string GetNomTable();

        bool IsDependanceChargee(string strNomTable, params string[] strChampsFille);

		bool ReadIfExists(CFiltreData filtre);
		bool ReadIfExists(object[] keys);

        C2iDataRow Row { get; }

        DataRowVersion VersionToReturn { get; set; }

        CDbKey DbKey { get; }
        string IdUniversel { get; }

        bool ReadIfExistsUniversalId(string strId);
	}

    public interface IObjetDonneeAIdNumerique : IObjetDonnee
    {
        string GetChampId();
        int Id { get;set;}
        bool ReadIfExists(int nId);
        bool ReadIfExists(CDbKey dbKey);
    }


    public interface IObjetDonneeAIdNumeriqueAuto : IObjetDonneeAIdNumerique
	{
		void CreateNew();
		void CreateNewInCurrentContexte();
	}

	/// <summary>
	/// Indique un objet donn�e pour lequel la table est toujours lue entierement
	/// puis filtr�e sur le poste client pour �viter des acc�s BDD
	/// </summary>
	public interface IObjetALectureTableComplete
	{
	}
}
