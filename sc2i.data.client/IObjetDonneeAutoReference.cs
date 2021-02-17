using System;

namespace sc2i.data
{
	/// <summary>
	/// Impl�ment�e par les objets donn�e qui ont une relation sur eux m�me
	/// </summary>
	public interface IObjetDonneeAutoReference : IObjetDonneeAIdNumerique
	{
		/// <summary>
		/// Retourne le champ (de la table) qui sert � stocker l'id du parent
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
	/// Pour tout objet hierarchique qui fournit un code syst�me hierarchique
	/// avec un nombre de caract�res par niveau fixe
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
        /// Retourne le libell� de l'objet hierarchique
        /// </summary>
        string Libelle { get; }

		/// <summary>
		/// Retourne le nombre de caract�res pour 1 niveau du code hi�rarchique
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
        /// Retourne le code partiel par d�faut
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
		/// Retourne vrai si l'objet est fils (� n'importe quel niveau) 
		/// de l'objet pass� en param�tre
		/// </summary>
		/// <param name="objet">P�re potientiel</param>
		/// <returns></returns>
        bool IsChildOf(IObjetHierarchiqueACodeHierarchique objet);

        void OnChangeNiveauParent(int nNewNiveauParent);
    }
}
