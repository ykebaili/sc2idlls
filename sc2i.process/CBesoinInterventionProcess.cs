using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.process;
using sc2i.multitiers.client;

namespace sc2i.process
{
	[DynamicClass("Process user need")]
	[Table(CBesoinInterventionProcess.c_nomTable, CBesoinInterventionProcess.c_champId, false)]
	[ObjetServeurURI("CBesoinInterventionProcessServeur")]
	public class CBesoinInterventionProcess : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "PROCESS_INTERVENTION";
		public const string c_champId = "PROCINT_ID";
		public const string c_champLibelle = "PROCINT_LABEL";
		public const string c_champDate = "PROCINT_DATE";
		public const string c_champIdAction = "PROCINT_ACTION_ID";
        public const string c_champKeyUtilisateur = "PROCINT_USER_KEY";
		public const string c_champCodeAttente = "PROCINT_WAITING_CODE";
		
		
#if PDA
		public CBesoinInterventionProcess( )
		:base()
		{
		}
#endif
		/// ////////////////////////////////////////////////
		public CBesoinInterventionProcess ( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////
		public CBesoinInterventionProcess ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T( "The @1 Process user need|258", Libelle);
			}
		}

		/// ////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			DateDemande = DateTime.Now;
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champLibelle, 255)]
		[DynamicField("Label")]
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return ( string )Row[c_champLibelle];
			}
			set
			{
				Row[c_champLibelle] = value;
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champDate)]
		[DynamicField("Date")]
		public DateTime DateDemande
		{
			get
			{
				return ( DateTime )Row[c_champDate];
			}
			set
			{
				Row[c_champDate] = value;
			}
		}

		/// ////////////////////////////////////////////////
		[Relation(CProcessEnExecutionInDb.c_nomTable,
			 CProcessEnExecutionInDb.c_champId,
			 CProcessEnExecutionInDb.c_champId,
			 true,
			 true,
			 true)]
		[DynamicField("Process")]
		public CProcessEnExecutionInDb ProcessEnExecution
		{
			get
			{
				return ( CProcessEnExecutionInDb )GetParent ( CProcessEnExecutionInDb.c_champId, typeof(CProcessEnExecutionInDb));
			}
			set
			{
				SetParent ( CProcessEnExecutionInDb.c_champId, value );
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty ( c_champIdAction )]
		[DynamicField("Process id")]
		public int IdAction
		{
			get
			{
				return (int)Row[c_champIdAction];
			}
			set
			{
				Row[c_champIdAction] = value;
			}
		}

		/// ////////////////////////////////////////////////
		//-----------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		[TableFieldProperty(c_champCodeAttente, 255)]
		[DynamicField("Waiting code")]
		public string CodeAttente
		{
			get
			{
				return (string)Row[c_champCodeAttente];
			}
			set
			{
				Row[c_champCodeAttente] = value;
			}
		}


		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champKeyUtilisateur, 64)]
		[DynamicField("User key string")]
        [ReplaceField("IdUtilisateur","User id")]
		public string KeyUtilisateurString
		{
			get
			{
                //TESTDBKEYOK
				return (string)Row[c_champKeyUtilisateur];
			}
			set
			{
                //TESTDBKEYOK
                Row[c_champKeyUtilisateur] = value;
			}
		}


        /// ////////////////////////////////////////////////
        [DynamicField("User key")]
        public CDbKey KeyUtilisateur
        {
            get
            {
                //TESTDBKEYOK
                return CDbKey.CreateFromStringValue(KeyUtilisateurString);
            }
            set
            {
                //TESTDBKEYOK
                if (value == null)
                    KeyUtilisateurString = "";
                else
                    KeyUtilisateurString = value.StringValue;
            }
        }

		public static bool HasInterventions ( int nIdSession, CDbKey keyUtilisateur )
		{
			///TODO
			///Problème VersionObjet
			IBesoinInterventionProcessServeur serveur = (IBesoinInterventionProcessServeur)CContexteDonnee.GetTableLoader ( CBesoinInterventionProcess.c_nomTable, null, nIdSession );
			return serveur.HasInterventions ( keyUtilisateur );
		}
	}

	public interface IBesoinInterventionProcessServeur
	{
		bool HasInterventions ( CDbKey keyUtilisateur );
	}




		
}
