using System;
using System.Data;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Entit� syt�me. Log utilis� en interne pour la synchronisation de donn�es
	/// </summary>
    /// <remarks>
    /// Le syst�me utilise cette donn�e dans certains cas particulier pour synchroniser des bases
    /// de donn�es entre elles<BR></BR>
    /// Chaque syst�me utilisant le framework PPF travaille toujours dans une version de synchronisation particuli�re.
    /// <BR></BR>D�s qu'une modification est effectu�e, le syst�me enregistre la table modifi�e et l'id de synchronisation
    /// courant du syst�me. Cette donn�e pourra �tre utilis�e par la suite pour d�tecter les modifications r�alis�es
    /// depuis la derni�re session de synchronisation
    /// <BR></BR><B>ATTENTION </B>Cette entit� est reserv�e � un usage syst�me. Toutes modification de donn�es peut
    /// entrainer des effets bloquant la synchronisation des donn�es.
    /// <BR></BR>
    /// Seules les cr�ation et les modifications sont stock�es via cette entit�. L'ID de session de synchronisation
    /// des modifications sont stock�es sur chaque entit� modifi�e via un champ d�di�.
    /// </remarks>
	[Table(CEntreeLogSynchronisation.c_nomTable, CEntreeLogSynchronisation.c_champId, false)]
	[ObjetServeurURI("CEntreeLogSynchronisationServer")]
    [DynamicClass("Synchronisation log")]
    [NoIdUniversel]
	public class CEntreeLogSynchronisation : CObjetDonneeAIdNumeriqueAuto,
		IObjetSansVersion
	{
		public enum TypeModifLogSynchro
		{
			tAdd = 0,
			tDelete = 1
		}

		public const string c_nomTable = "SC2I_SYNC_LOG";
		public const string c_champId = "SSL_ID";
//		[Champ(typeof(string), 255)]
		public const string c_champTable = "SSL_TABLE";
//		[Champ(typeof(int))]
		public const string c_champIdElement = "SSL_ELT_ID";
//		[Champ(typeof(int))]
		public const string c_champType = "SSL_TYPE";

#if PDA
		/// /////////////////////////////////////////////////////////////
		public CEntreeLogSynchronisation( )
			:base()
		{

		}
#endif

		/// /////////////////////////////////////////////////////////////
		public CEntreeLogSynchronisation( CContexteDonnee ctx)
			:base(ctx)
		{

		}

		/// /////////////////////////////////////////////////////////////
		public CEntreeLogSynchronisation ( DataRow row )
			:base(row)
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Synchronization log @1|132",Id.ToString());
			}
		}

		/// /////////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string GetChampId()
		{
			return c_champId;
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

		/// /////////////////////////////////////////////////////////////
		///<summary>Nom syst�me de la table de la base de donn�es concern�e</summary>
        [TableFieldProperty(c_champTable, 255)]
		[IndexField]
        [DynamicField("Table name")]
		public string TableConcernee
		{
			get
			{
				return (string)Row[c_champTable];
			}
			set
			{
				Row[c_champTable] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Identifiant de l'�l�ment modifi�.
        ///</summary>
        [TableFieldProperty(c_champIdElement)]
        [DynamicField("Element id")]
		public int IdElement
		{
			get
			{
				return (int)Row[c_champIdElement];
			}
			set
			{
				Row[c_champIdElement] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
        ///<summary>
        ///Indique le type de modification
        ///</summary>
        ///<remarks>
        ///Les valeurs possible sont  :
        ///<LI>0 : Ajout</LI>
        ///<LI>1 : Suppresion</LI>
        ///</remarks>
        [TableFieldProperty(c_champType)]
        [DynamicField("Modification type")]
		public TypeModifLogSynchro TypeModif
		{
			get
			{
				return (TypeModifLogSynchro)Row[c_champType];
			}
			set
			{
				Row[c_champType] = (int)value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Identifiant de la session de syncrhonisation durant laquelle a �t� modifi� l'entit�.
        ///</summary>
        [TableFieldProperty(CSc2iDataConst.c_champIdSynchro)]
		[IndexField]
        [DynamicField("SyncSession ID")]
		public int IdSyncSession
		{
			get
			{
				return (int)Row[CSc2iDataConst.c_champIdSynchro];
			}
			set
			{
				Row[CSc2iDataConst.c_champIdSynchro] = (int)value;
			}
		}


		/// /////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique GetObjet( )
		{
			if ( TypeModif == TypeModifLogSynchro.tDelete )
				return null;
			CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)ContexteDonnee.GetNewObjetForTable ( ContexteDonnee.GetTableSafe(TableConcernee) );
			objet.Id = IdElement;
			return objet;
		}
		

		
	}
}

