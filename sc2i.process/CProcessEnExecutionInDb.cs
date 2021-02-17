using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.process;
using sc2i.multitiers.client;

namespace sc2i.process
{
	public enum EtatProcess
	{
		EnCours = 10,
		Termine = 20,
		Erreur = 30
	}

    /// <summary>
    /// Cet objet permet de garder la trace de l'exécution d'un process<br/>
    /// afin de pouvoir en faire une analyse à postériori
    /// </summary>
	[DynamicClass("Running process")]
	[Table(CProcessEnExecutionInDb.c_nomTable, CProcessEnExecutionInDb.c_champId, false)]
	[ObjetServeurURI("CProcessEnExecutionInDbServeur")]
	public class CProcessEnExecutionInDb : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
	{
		public const string c_nomTable = "PROCESS_EXEC";
		public const string c_champId = "PROCEX_ID";
		public const string c_champData = "PROCEX_DATA";
		public const string c_champLibelle = "PROCEX_LABEL";
		public const string c_champTypeElement = "PROCEX_ELEMENT_TYPE";
		public const string c_champIdElement = "PROCEX_ELEMENT_ID";
		public const string c_champInfoEtat = "PROCEX_STATE_INFO";
		public const string c_champEtat = "PROCEX_STATE";
		public const string c_champActionEnCours = "PROCEX_RUNNING_ACTION";
		public const string c_champDateCreation = "PROCEX_CREATION_DATE";
		public const string c_champDateModification = "PROCEX_MODIFICATION_DATE";
		public const string c_champUniversalIdEvenementDeclencheur = "PROCEX_LAUNCHER_EVT_UID";
		public const string c_champIdVersionExecution = "PROCEX_VERS_ID";
		
#if PDA
		public CProcessEnExecutionInDb( )
		:base()
		{
		}
#endif
		/// ////////////////////////////////////////////////
		public CProcessEnExecutionInDb ( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////
		public CProcessEnExecutionInDb ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 process in execution|281",Id.ToString());
			}
		}

		/// ////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champDateModification +" desc"};
		}

		/// ////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			Etat = EtatProcess.EnCours;
			DateCreation = DateTime.Now;
			DateModification = DateTime.Now;
		}

		//---------------------------------------------------
        /// <summary>
        /// Libellé du process
        /// </summary>
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

		//-----------------------------------------------------
        /// <summary>
        /// Information sur l'état du process
        /// </summary>
		[TableFieldProperty(c_champInfoEtat, 1024)]
		[DynamicField("State info")]
		public string InfoEtat
		{
			get
			{
				return ( string )Row[c_champInfoEtat];
			}
			set
			{
				if ( value.Length > 1024 )
					Row[c_champInfoEtat] = value.Substring(0, 1024);
				else
					Row[c_champInfoEtat] = value;
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champActionEnCours, 255)]
		[DescriptionField]
		public string LibelleActionEnCours
		{
			get
			{
				return ( string )Row[c_champActionEnCours];
			}
			set
			{
				Row[c_champActionEnCours] = value.Substring(0, Math.Min(255, value.Length) );
			}
		}
		
		//----------------------------------------------------
        /// <summary>
        /// Etat du process :
        /// <ul>
        /// <li>En cours</li>
        /// <li>Terminé</li>
        /// <li>En erreur</li>
        /// </ul>
        /// </summary>
		[TableFieldProperty(c_champEtat)]
		[DynamicField("State")]
		public EtatProcess Etat
		{
			get
			{
				return ( EtatProcess )Row[c_champEtat];
			}
			set
			{
				Row[c_champEtat] = (EtatProcess)value;
			}
		}


		/// ////////////////////////////////////////////////
		///Contient la branche en cours d'execution
		[TableFieldProperty(c_champData,NullAutorise=true)]
		public CDonneeBinaireInRow Data
		{
			get
			{
				if ( Row[c_champData] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champData);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champData, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champData]).GetSafeForRow(Row.Row);
			}
			set
			{
				Row[c_champData] = value;
			}
		}

		//---------------------------------------------------
        /// <summary>
        /// Date de création de l'objet
        /// </summary>
		[TableFieldProperty(c_champDateCreation)]
		[DynamicField("Creation date")]
		public DateTime DateCreation
		{
			get
			{
				return (DateTime)Row[c_champDateCreation];
			}
			set
			{
				Row[c_champDateCreation] = value;
			}
		}

		//----------------------------------------------------------------
        /// <summary>
        /// Date de modification de l'objet; est initialisé à la date de création
        /// </summary>
		[TableFieldProperty(c_champDateModification)]
		[DynamicField("Modification date")]
		public DateTime DateModification
		{
			get
			{
				return ( DateTime )Row[c_champDateModification];
			}
			set
			{
				Row[c_champDateModification] = value;
			}
		}


		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CBrancheProcess BrancheEnCours
		{
			get
			{
				CBrancheProcess branche = new CBrancheProcess(null);
				if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					serializer.AttacheObjet ( typeof(CContexteDonnee), ContexteDonnee );
					CResultAErreur result = branche.Serialize(serializer);
					if ( !result )
					{
						throw new Exception(I.T("Impossible to read again the data action|282"));
					}

                    reader.Close();
                    stream.Close();
				}
				return branche;
			}
			set
			{
				if ( value == null || value.ExecutionSurContexteClient)
				{
					CDonneeBinaireInRow data = Data;
					data.Donnees = null;
					Data = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
					{
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
					}
                    writer.Close();
                    stream.Close();
				}
			}
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Type de l'élément cible du process (lorsqu'il existe)
        /// </summary>
		[TableFieldProperty( c_champTypeElement, 1024)]
		[DynamicField("Element type")]
		public string TypeElementString
		{
			get
			{
				return ( string )Row[c_champTypeElement];
			}
			set
			{
				Row[c_champTypeElement] = value;
			}
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Identifiant (ID) de l'élément cible du process (lorsqu'il existe)
        /// </summary>
		[TableFieldProperty(c_champIdElement)]
		[DynamicField("Element id")]
		public int IdElementLie
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

		//-------------------------------------------------------------------
        /// <summary>
        /// Elément cible du process lorsqu'il existe (null sinon)
        /// </summary>
		[DynamicField("Linked element")]
		public CObjetDonneeAIdNumerique ElementLie
		{
			get
			{
				try
				{
					Type tp = CActivatorSurChaine.GetType ( TypeElementString );
					if ( tp == null )
						return null;
					CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( tp, new object[]{ContexteDonnee} );
					if ( objet.ReadIfExists ( IdElementLie ) )
						return objet;
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if ( value != null )
				{
					TypeElementString = value.GetType().ToString();
					IdElementLie = value.Id;
				}
				else
				{
					TypeElementString = "";
					IdElementLie = -1;
				}
			}
		}

		////////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la version de donnée dans laquelle s'exécute le process<br/>
        /// (lorsqu'il est exécuté dans une version)
		/// </summary>
		[TableFieldProperty ( CProcessEnExecutionInDb.c_champIdVersionExecution, true )]
		[DynamicField("Execution Data version id")]
		public int? IdVersionExecution
		{
			get
			{
				return (int?)Row[c_champIdVersionExecution, true];
			}
			set
			{
				Row[c_champIdVersionExecution, true] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// DbKey en String de <see cref="CEvenement">l'événement</see> qui 
		/// a déclenché le process. Si chaine vide, c'est que le process n'a
		/// pas été déclenché par un événement de type CEvenement.
		/// </summary>
        [TableFieldProperty(c_champUniversalIdEvenementDeclencheur, 64)]
		[DynamicField("Release Event DbKey String")]
		public string DbKeyStringEvenementDeclencheur
		{
			get
			{
                if (Row[c_champUniversalIdEvenementDeclencheur] == DBNull.Value)
                    return null;
                return (string)Row[c_champUniversalIdEvenementDeclencheur];
			}
			set
			{
                Row[c_champUniversalIdEvenementDeclencheur] = value;
			}
		}

        /// /////////////////////////////////////////////////////////////
        /// <summary>
        /// DbKey de <see cref="CEvenement">l'événement</see> qui 
        /// a déclenché le process. Si null, c'est que le process n'a
        /// pas été déclenché par un événement de type CEvenement.
        /// </summary>
        [DynamicField("Release Event DbKey")]
        public CDbKey DbKeyEvennementDeclencheur
        {
            get
            {
                return CDbKey.CreateFromStringValue(DbKeyStringEvenementDeclencheur);
            }
            set
            {
                if (value != null)
                    DbKeyStringEvenementDeclencheur = value.StringValue;
                else
                    DbKeyStringEvenementDeclencheur = "";
            }
        }


		//--------------------------------------------------------------
        /// <summary>
        /// <see cref="CEvenement">Evénement</see> qui 
        /// a déclenché le process. Si null, c'est que le process n'a
        /// pas été déclenché par un événement de type CEvenement.
        /// </summary>
        [DynamicField("Release event")]
        public CEvenement EvenementDeclencheur
        {
            get
            {
                CDbKey dbKeyEvenement = CDbKey.CreateFromStringValue(DbKeyStringEvenementDeclencheur);
                CEvenement evt = new CEvenement(ContexteDonnee);
                if (evt.ReadIfExists(dbKeyEvenement))
                    return evt;
                return null;
            }
            set
            {
                if (value == null)
                    DbKeyStringEvenementDeclencheur = "";
                else
                    DbKeyStringEvenementDeclencheur = value.IdUniversel;
            }
        }

		//-------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des handlers événements liés au process en exécution
        /// </summary>
		[RelationFille(typeof(CHandlerEvenement), "ProcessSource")]
		[DynamicChilds("Evenements lies", typeof(CHandlerEvenement))]
		public CListeObjetsDonnees EvenementsLies
		{
			get
			{
				return GetDependancesListe ( CHandlerEvenement.c_nomTable, c_champId );
			}
		}
		
		/// /////////////////////////////////////////////////////////////
		public CResultAErreur EnProcess ( CContexteExecutionAction contexteExecution )
		{
			BrancheEnCours = contexteExecution.Branche;
			Etat = EtatProcess.Termine;
			try
			{
				if (contexteExecution.IndicateurProgression != null)
					contexteExecution.SetInfoProgression(I.T("Ending process|20003"));
			}
			catch
			{
			}
            if (!contexteExecution.SauvegardeContexteExterne)
            {
                CResultAErreur result = contexteExecution.ContexteDonnee.SaveAll(true);
                if (result)
                    contexteExecution.DoTraitementApresExecution();
                //Stef 9/1/2015 : si le result est faux, il faut
                //retourner avec une erreur, sinon, l'erreur
                //n'est jamais visible
                if (!result)
                    return result;
            }
			return CResultAErreur.True;
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur PauseProcess ( CContexteExecutionAction contexteExecution )
		{
			Etat = EtatProcess.EnCours;
			BrancheEnCours = contexteExecution.Branche;
			if ( !contexteExecution.SauvegardeContexteExterne )
				return contexteExecution.ContexteDonnee.SaveAll(true);
			return CResultAErreur.True;
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur RepriseProcess ( int nIdAction, IIndicateurProgression indicateur )
		{
			///TODO
			///Problème VersionObjet
			IProcessEnExecutionInDbServeur processServeur = (IProcessEnExecutionInDbServeur)CContexteDonnee.GetTableLoader ( c_nomTable, null, ContexteDonnee.IdSession );
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(processServeur);
				return processServeur.RepriseProcess(Id, nIdAction, indicateur);
			}
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur AnnuleReprise ( string strInfoUtilisateur )
		{
			BeginEdit();
			Etat = EtatProcess.Termine;
            InfoEtat = I.T("Cancelled recovery by @1 the @2 at @3|283", strInfoUtilisateur, DateTime.Now.ToString(CUtilDate.gFormat));//"dd/MM/yyyy"),DateTime.Now.ToString("hh:mm"));
			CResultAErreur result = CommitEdit();
			return result;
		}


		
		/// /////////////////////////////////////////////////////////////
		//Le data du result contient la valeur de retour du process
		public static CResultAErreur StartProcess ( CProcess process, 
			CInfoDeclencheurProcess infoDeclencheur, 
			int nIdSession, 
			int? nIdVersion,
			IIndicateurProgression indicateur )
		{
			System.Console.WriteLine("Démarrage process" + process.Libelle);
			process.InfoDeclencheur = infoDeclencheur;
			///TODO
			///Problème VersionObjet
			IProcessEnExecutionInDbServeur processServeur = (IProcessEnExecutionInDbServeur)CContexteDonnee.GetTableLoader(c_nomTable, null, nIdSession);
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(processServeur);
                CAppelleurFonctionAsynchrone appelleur = new CAppelleurFonctionAsynchrone();
                CResultAErreur defaultResult = CResultAErreur.True;
                defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20032", "StartProcess"));
                return appelleur.StartFonctionAndWaitAvecCallback(
                    typeof(IProcessEnExecutionInDbServeur),
                    processServeur,
                    "StartProcess",
                    "",
                    defaultResult,
                    new CValise2iSerializable(process),
                    null,
                    nIdVersion,
                    indicateur) as CResultAErreur;
			}
		}

		/// /////////////////////////////////////////////////////////////
		//Le data du result contient la valeur de retour du process
		public static CResultAErreur StartProcess ( CProcess process, 
			CInfoDeclencheurProcess infoDeclencheur, 
			CReferenceObjetDonnee refCible, 
			int nIdSession,
			int? nIdVersion,
			IIndicateurProgression indicateur )
		{
			System.Console.WriteLine("Démarrage process" + process.Libelle);
			process.InfoDeclencheur = infoDeclencheur;
			///TODO
			///Problème VersionObjet
			IProcessEnExecutionInDbServeur processServeur = (IProcessEnExecutionInDbServeur)CContexteDonnee.GetTableLoader ( c_nomTable, null, nIdSession );
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(processServeur);
                CAppelleurFonctionAsynchrone appelleur = new CAppelleurFonctionAsynchrone();
                CResultAErreur defaultResult = CResultAErreur.True;
                defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20032", "StartProcess"));
                return appelleur.StartFonctionAndWaitAvecCallback(
                    typeof(IProcessEnExecutionInDbServeur),
                    processServeur,
                    "StartProcess",
                    "",
                    defaultResult, 
                    new CValise2iSerializable(process),
                    refCible,
                    nIdVersion,
                    indicateur) as CResultAErreur;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public static CResultAErreur StartProcessMultiples ( CProcess process, 
			CInfoDeclencheurProcess infoDeclencheur, 
			CReferenceObjetDonnee[] refsCible, 
			int nIdSession,
			int? nIdVersion, 
			IIndicateurProgression indicateur)
		{
			process.InfoDeclencheur = infoDeclencheur;
			///TODO
			///Problème VersionObjet
			IProcessEnExecutionInDbServeur processServeur = (IProcessEnExecutionInDbServeur)CContexteDonnee.GetTableLoader ( c_nomTable, null, nIdSession );
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(processServeur);
                CAppelleurFonctionAsynchrone appelleur = new CAppelleurFonctionAsynchrone();
                CResultAErreur defaultResult = CResultAErreur.True;
                defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20032", "StartProcessMultiples"));
                return appelleur.StartFonctionAndWaitAvecCallback(
                    typeof(IProcessEnExecutionInDbServeur),
                    processServeur,
                    "StartProcessMultiples",
                    "",
                    defaultResult,
                    new CValise2iSerializable(process),
                    refsCible,
                    nIdVersion,
                    indicateur) as CResultAErreur;
			}
		}

		/// <summary>
		/// Purge l'historique des process jusqu'à une date limite
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static CResultAErreur Purger(DateTime dt, int nIdSession)
		{
			IProcessEnExecutionInDbServeur processServeur = (IProcessEnExecutionInDbServeur)CContexteDonnee.GetTableLoader(c_nomTable, null, nIdSession);
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(processServeur);
				return processServeur.Purger(dt);
			}
		}

		public static CResultAErreur StartProcessClient(
			CProcess leProcessAExecuter,
			object objetCible,
			CContexteDonnee contexte,
			IIndicateurProgression indicateur)
		{
			System.Console.WriteLine("Démarrage process" + leProcessAExecuter.Libelle);
			leProcessAExecuter.ContexteDonnee = contexte;
			//Fin Stef 2/4/08
			CResultAErreur result = CResultAErreur.True;
			if (!leProcessAExecuter.PeutEtreExecuteSurLePosteClient)
			{
				result.EmpileErreur(I.T("Cannot start this process in client mode|20004"));
				return result;
			}
			CProcessEnExecutionInDb processEnExec = new CProcessEnExecutionInDb ( contexte );
			processEnExec.CreateNewInCurrentContexte();
			if ( objetCible is CObjetDonneeAIdNumerique )
				processEnExec.ElementLie = (CObjetDonneeAIdNumerique)objetCible;
			else
				processEnExec.ElementLie = null;
			processEnExec.Libelle = leProcessAExecuter.Libelle;
            processEnExec.DbKeyEvennementDeclencheur = leProcessAExecuter.InfoDeclencheur.DbKeyEvenementDeclencheur;
			processEnExec.IdVersionExecution = contexte.IdVersionDeTravail;

			CBrancheProcess branche = new CBrancheProcess( leProcessAExecuter );
			branche.IsModeAsynchrone = false;
			branche.ExecutionSurContexteClient = true;
			CSessionClient sessionSource = CSessionClient.GetSessionForIdSession ( contexte.IdSession );

            //TESTDBKEYTODO
			branche.KeyUtilisateur = sessionSource.GetInfoUtilisateur().KeyUtilisateur;
			branche.ConfigurationImpression = sessionSource.ConfigurationsImpression;
			
			CContexteExecutionAction contexteExecution = new CContexteExecutionAction ( 
				processEnExec, 
				branche, 
				objetCible, 
				contexte, 
				indicateur );
            string strOldContextuel = contexte.IdModificationContextuelle;
			contexteExecution.SauvegardeContexteExterne = true;
			processEnExec.BrancheEnCours = branche;

            CAppelleurFonctionAsynchrone appeleur = new CAppelleurFonctionAsynchrone();
            CResultAErreur defaultResult = CResultAErreur.True;
            defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20032", "ExecuteAction"));
            result = appeleur.StartFonctionAndWaitAvecCallback(branche.GetType(),
                branche,
                "ExecuteAction",
                "",
                defaultResult,
                leProcessAExecuter.GetActionDebut(), contexteExecution, true) as CResultAErreur;



			//result = branche.ExecuteAction ( leProcessAExecuter.GetActionDebut(), contexteExecution, true );
            if (leProcessAExecuter.VariableDeRetour != null)
                result.Data = leProcessAExecuter.GetValeurChamp(leProcessAExecuter.VariableDeRetour.IdVariable);
            contexte.IdModificationContextuelle = strOldContextuel;
		    return result;
		}

	}

	public interface IProcessEnExecutionInDbServeur
	{
		CResultAErreur StartProcess ( CValise2iSerializable valiseProcess, 
			CReferenceObjetDonnee refCible, 
			int? nIdversion,
			IIndicateurProgression indicateur );

		CResultAErreur StartProcessMultiples ( CValise2iSerializable valiseProcess,
			CReferenceObjetDonnee[] refCible,
			int? nIdversion,
			IIndicateurProgression indicateur );

		CResultAErreur RepriseProcess ( int nIdProcessEnExecution, 
			int nIdAction,
			IIndicateurProgression indicateur );

		CResultAErreur Purger(DateTime dateLimite);
	}
}
