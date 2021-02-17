using System;
using System.Data;

using sc2i.common;
using sc2i.data;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Lien entre un groupe de synchronisation et un utilisateur
	/// </summary>
	[Table(CRelationGroupeUtilisateursSynchro_Utilisateurs.c_nomTable, CRelationGroupeUtilisateursSynchro_Utilisateurs.c_champId, false)]
	[ObjetServeurURI("CRelationGroupeUtilisateursSynchro_UtilisateursServeur")]
	[DynamicClass("Synchronization groupe / user")]
	public class CRelationGroupeUtilisateursSynchro_Utilisateurs : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "SYNCUSER_GROUP_ISER";
		public const string c_champId = "USRGRPSYNCUSER_ID";
		public const string c_champIdUtilisateur = "USRGRPSYNCUSER_USER_ID";

#if PDA
		/// ///////////////////////////////////
		public CRelationGroupeUtilisateursSynchro_Utilisateurs()
			:base()
		{
		}
#endif
		/// ///////////////////////////////////
		public CRelationGroupeUtilisateursSynchro_Utilisateurs( CContexteDonnee contexte)
			:base ( contexte )
		{
		}

		/// ///////////////////////////////////
		public CRelationGroupeUtilisateursSynchro_Utilisateurs( DataRow row )
			:base ( row )
		{
		}

		/// ///////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Relation between the group @1 and the user with id @2|30001",Groupe.DescriptionElement,IdUtilisateur.ToString());
			}
		}

		/// ///////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}
		
		/// ///////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			IdUtilisateur = -1;
		}

		/// ///////////////////////////////////
		///<summary>
        ///Id de l'utilisateur
        /// </summary>
        [TableFieldProperty ( c_champIdUtilisateur )]
		[DynamicField("User id")]
		public int IdUtilisateur
		{
			get
			{
				return (int)Row[c_champIdUtilisateur];
			}
			set
			{
				Row[c_champIdUtilisateur] = value;
			}
		}

		/// ///////////////////////////////////
        ///<summary>
        ///Groupe utilisateur concerné par la relation
        ///</summary>
		[Relation(CGroupeUtilisateursSynchronisation.c_nomTable,
		CGroupeUtilisateursSynchronisation.c_champId,
		CGroupeUtilisateursSynchronisation.c_champId,
		true,
		true,
		false)]
		[DynamicField("Group")]
		public CGroupeUtilisateursSynchronisation Groupe
		{
			get
			{
				return (CGroupeUtilisateursSynchronisation)GetParent ( CGroupeUtilisateursSynchronisation.c_champId, typeof(CGroupeUtilisateursSynchronisation) );
			}
			set
			{
				SetParent ( CGroupeUtilisateursSynchronisation.c_champId, value );
			}
		}

		




		

	}
}
