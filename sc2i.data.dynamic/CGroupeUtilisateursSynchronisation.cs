using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;

#if !PDA_DATA
namespace sc2i.data.dynamic
{
	/// <summary>
	/// Spécifie les paramètres de synchronisation pour un groupe d'utilisateur.
	/// </summary>
    /// <remarks>
    /// Suivant les versions de produit, les entités de ce type peuvent ne pas être accessibles.
    /// </remarks>
	[Table(CGroupeUtilisateursSynchronisation.c_nomTable,CGroupeUtilisateursSynchronisation.c_champId, false)]
	[ObjetServeurURI("CGroupeUtilisateursSynchronisationServeur")]
	[DynamicClass("synchronization users group")]
	public class CGroupeUtilisateursSynchronisation : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "USER_SYNCHRO_GROUP";
		public const string c_champId = "USRSYNCGRO_ID";
		public const string c_champLibelle = "USRSYNCGRP_NAME";
		public const string c_champCode = "USRSYNCGRP_CODE";
		public const string c_champIdMachine = "USRSYNCGRP_AD_COMPUTER";

		/// ////////////////////////////////////////////////////////
		public CGroupeUtilisateursSynchronisation( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////////////
		public CGroupeUtilisateursSynchronisation ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The synchronization users group @1|180", Libelle);
			}
		}

		/// ////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// ////////////////////////////////////////////////////////
		///<summary>Libellé du groupe de synchronisation</summary>
        [TableFieldProperty(c_champLibelle, 255)]
		[DynamicField("Label")]
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return (string)Row[c_champLibelle];
			}
			set
			{
				Row[c_champLibelle] = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		///<summary>Code du groupe de synchronisation</summary>
        [TableFieldProperty(c_champCode, 64)]
		[DynamicField("Code")]
		public string Code
		{
			get
			{
				return (string)Row[c_champCode];
			}
			set
			{
				Row[c_champCode] = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		///<summary>Identifiant de la machine associée au groupe si ce groupe est associé à une machine particulière</summary>
        [TableFieldProperty(c_champIdMachine, 128)]
		[DynamicField("Computer ID")]
		public string IdComputer
		{
			get
			{
				return (string)Row[c_champIdMachine];
			}
			set
			{
				Row[c_champIdMachine] = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'entité ordinateur AD associée au groupe, si le groupe est associé à un groupe Active Directory
		/// </summary>
		[DynamicField("AD computer ID")]
		public CAdComputer AdComputer
		{
			get
			{
				return CAdComputer.GetComputer ( ContexteDonnee.IdSession, IdComputer );
			}
			set
			{
				if ( value == null )
					IdComputer = "";
				else
					IdComputer = value.Nom;
			}
		}

		
		/// ////////////////////////////////////////////////////////
		///<summary>Liens vers les paramètres de synchronisation actifs pour ce groupe</summary>
        [Relation (CParametreSynchronisationInDb.c_nomTable,
		CParametreSynchronisationInDb.c_champId,
		CParametreSynchronisationInDb.c_champId,
		true,
		true,
		false)]
		[DynamicField("Synchronization parameter")]
		public CParametreSynchronisationInDb ParametreSynchronisation
	{
			get
			{
				return ( CParametreSynchronisationInDb )GetParent ( CParametreSynchronisationInDb.c_champId, typeof(CParametreSynchronisationInDb ) );
			}
			set
			{
				SetParent ( CParametreSynchronisationInDb.c_champId, value );
			}
		}

		/// ////////////////////////////////////////////////////////
		///<summary>
        ///Liens vers les utilisateurs concernés par ce groupe.
        /// </summary>
        [RelationFille(typeof(CRelationGroupeUtilisateursSynchro_Utilisateurs), "Groupe")]
		[DynamicChilds("Utilisateurs", typeof(CRelationGroupeUtilisateursSynchro_Utilisateurs))]
		public CListeObjetsDonnees RelationsUtilisateurs
		{
			get
			{
				return GetDependancesListe ( CRelationGroupeUtilisateursSynchro_Utilisateurs.c_nomTable, c_champId);
			}
		}

		/// ////////////////////////////////////////////////////////
		public int[] ListeIdsUtilisateurs
		{
			get
			{
				ArrayList lst = new ArrayList();
				foreach ( CRelationGroupeUtilisateursSynchro_Utilisateurs rel in RelationsUtilisateurs )
					lst.Add ( rel.IdUtilisateur );
				return (int[])lst.ToArray(typeof(int));
			}
		}

		

		
		
	}
}
#endif
