using System;

namespace sc2i.data
{
	/// <summary>
	/// Implémentée par les objets donnée qui ont une relation sur eux même
	/// </summary>
	public interface IObjetDonneeAutoReference : IObjetDonneeAIdNumerique
	{
		/// <summary>
		/// Retourne le champ (de la table) qui sert à stocker l'id du parent
		/// </summary>
		string ChampParent { get;}
		
		/// <summary>
		/// Retourne la propriete qui retourne la liste des fils
		/// </summary>
		string ProprieteListeFils { get;}

        
	}

    public interface IObjetDonneeAutoReferenceNavigable : IObjetDonneeAutoReference
    {
        IObjetDonneeAutoReference ObjetAutoRefParent { get; }
        CListeObjetsDonnees ObjetsAutoRefFils { get; }
        string Libelle { get; set; }
    }

	/// <summary>
	/// Pour tout objet hierarchique qui fournit un code système hierarchique
	/// avec un nombre de caractères par niveau fixe
	/// exemple :
	/// 01
	/// 0101
	/// 0102
	/// 010201
	/// 02
	/// 0201
	/// ...
	/// </summary>
    public interface IObjetHierarchiqueACodeHierarchique : IObjetDonneeAutoReferenceNavigable, IObjetDonneeAIdNumeriqueAuto
    {
        /// <summary>
        /// Retourne le libellé de l'objet hierarchique
        /// </summary>
        string Libelle { get; }

		/// <summary>
		/// Retourne le nombre de caractères pour 1 niveau du code hiérarchique
		/// </summary>
		int NbCarsParNiveau { get;}

        /// <summary>
        /// Retourne le nom du champ contenant le niveua de l'objet hierarchique
        /// </summary>
        string ChampNiveau { get;}

        /// <summary>
        /// Retourne le nom du champ contenant le code systeme complet
        /// </summary>
        string ChampCodeSystemeComplet { get; }

        /// <summary>
        /// Retourne le nom du champ contenant le code systeme partiel
        /// </summary>
        string ChampCodeSystemePartiel { get; }

        /// <summary>
        /// Retourne le nom du champ contenant l'id de l'objet parent
        /// </summary>
        string ChampIdParent { get; }

        /// <summary>
        /// Retourne le niveau de l'objet. Root est au niveau 0.
        /// </summary>
        int Niveau { get; }

        /// <summary>
        /// Retourne le code hierarchique de l'objet
        /// </summary>
        string CodeSystemeComplet { get;}

        /// <summary>
        /// Retourne le code partiel de l'objet (le code dans sa famille principale)
        /// </summary>
        string CodeSystemePartiel { get; }

        /// <summary>
        /// Retourne le code partiel par défaut
        /// </summary>
        string CodePartielDefaut { get; }

        /// <summary>
        /// Change le code partiel
        /// </summary>
        void ChangeCodePartiel(string str_code);

        /// <summary>
        /// Recalcule le code complet
        /// </summary>
        void RecalculeCodeComplet();

        /// <summary>
        /// Recalcule le code complet des fils
        /// </summary>
        void RecalculeCodeCompletFils();

        /// <summary>
        /// Retourne l'objet hierarchique parent s'il existe, null sinon
        /// </summary>
        IObjetHierarchiqueACodeHierarchique ObjetParent { get; set; }

        /// <summary>
        /// Retourne la liste des objets fils
        /// </summary>
        CListeObjetsDonnees ObjetsFils { get; }

        /// <summary>
		/// Retourne vrai si l'objet est fils (à n'importe quel niveau) 
		/// de l'objet passé en paramètre
		/// </summary>
		/// <param name="objet">Père potientiel</param>
		/// <returns></returns>
        bool IsChildOf(IObjetHierarchiqueACodeHierarchique objet);

        void OnChangeNiveauParent(int nNewNiveauParent);
    }
}
