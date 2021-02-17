using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;


using sc2i.common;
using sc2i.data;
using sc2i.expression;
using System.Text;
using sc2i.process.workflow;
using sc2i.data.dynamic;

namespace sc2i.process
{
	public enum EtatHandlerAction
	{
		AExecuter = 0,
		Termine = 1,
		Erreur = 2
	}
	#region RelationElementToHandlerEvenementAttribute
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationElementToHandlerEvenementAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CHandlerEvenement.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 100;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "ELT_HANDLER_EVT";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CHandlerEvenement.c_champIdCible;
			}
		}

		public override string ChampType
		{
			get
			{
				return CHandlerEvenement.c_champTypeCible;
			}
		}
		
		public override bool Composition
		{
			get
			{
				return true;
			}
		}
		public override bool CanDeleteToujours
		{
			get
			{
				return true;
			}
		}

		public override string NomConvivialPourParent
		{
			get
			{
				return I.T( "Handlers|269");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique ));
		}
	}
	#endregion
	/// <summary>
	/// Surveille les évenements sur des éléments
	/// </summary>
	[Table(CHandlerEvenement.c_nomTable, CHandlerEvenement.c_champId, false)]
	[ObjetServeurURI("CHandlerEvenementServeur")]
	[DynamicClass("Event handler")]
	[RelationElementToHandlerEvenement]
    [NoRelationTypeId]
	public class CHandlerEvenement : CObjetDonneeAIdNumeriqueAuto,
		IDeclencheurActionManuelle, IObjetSansVersion
	{

		public const string c_nomTable = "EVENT_HANDLER";
		public const string c_champId = "EVTHDL_ID";
		public const string c_champLibelle = "EVTHDL_LABEL";
		public const string c_champCode = "EVTHDL_CODE";
		public const string c_champTypeEvenement = "EVTHDL_TYPE";
		public const string c_champCondition = "EVTHDL_CONDITION";
		public const string c_champMenu = "EVTHDL_MENU_MANUAL";
		public const string c_champIdsGroupesManuels = "EVTHDL_MANUEL_GRPS_ID";
		public const string c_champProprieteSurveillee = "EVTHDL_MONITORED_PROP";
		public const string c_champValeurAvant = "EVTHDL_FORMULA_VALUE_BEF";
		public const string c_champValeurApres = "EVTHDL_FORMULA_VALUE_AFT";
		public const string c_champTypeCible = "EVTHDL_TARGET_TYPE";
		public const string c_champIdCible = "EVTHDL_TARGET_ID";
		public const string c_champDateHeureDeclenchement = "EVTHDL_LAUNCH_DATE";
		public const string c_champEtatExecution = "EVTHDL_EXECUTION_STATE";
		public const string c_champAutoSupprimable = "EVTHDL_AUTO_DEL";
		public const string c_champNbEssaisEchoues = "EVTHDL_FAILED_TRY_NB";
		public const string c_champErreur = "EVTHDL_ERROR_MESSAGE";
		public const string c_champOrdreExecution = "EVTHDL_EXECUTION_ORDER";
		public const string c_champHideProgress = "EVTHDL_HIDE_PROGRESS";
        public const string c_champContextesException = "EVTHDL_EXCEPTION_CTX";

		public const string c_champIdActionProcessSource = "EVTHDL_SOURCE_ACTION_ID";

		/// <summary>
		/// /////////////////////////////////////////////////////////////
		/// </summary>
		public CHandlerEvenement( CContexteDonnee contexte)
			:base ( contexte )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CHandlerEvenement ( DataRow row )
			:base( row )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 handler|270",Libelle);
			}
		}

		/// /////////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			DateHeure = DateTime.Now;
			IdActionDeProcessToExecute = -1;
			HideProgress = false;
		}

		/// /////////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

		//----------------------------------------------------------------
        /// <summary>
        /// Libellé du handler d'événement
        /// </summary>
		[TableFieldProperty(c_champLibelle, 128)]
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
				Row[c_champLibelle]=value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty ( c_champCode, 255)]
		[DynamicField("Release code")]
		public string Code
		{
			get
			{
				return ( string )Row[c_champCode];
			}
			set
			{
				Row[c_champCode] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeEvenement)]
		public TypeEvenement TypeEvenement
		{
			get
			{
				return (TypeEvenement)Row[c_champTypeEvenement];
			}
			set
			{
				Row[c_champTypeEvenement] = (int)value;
			}
		}

		//------------------------------------------------
		/// <summary>
		/// Pour un évenement manuel, indique que l'évenement doit
		/// se dérouler sans afficher la barre de progression
		/// </summary>
		[DynamicField("Hide progress")]
		[TableFieldProperty(CHandlerEvenement.c_champHideProgress)]
		public bool HideProgress
		{
			get
			{
				return (bool)Row[c_champHideProgress];
			}
			set
			{
				Row[c_champHideProgress] = value;
			}
		}

        /// <summary>
        /// contextes d'exception séparés par des ;
        /// </summary>
        [DynamicField("Exception contexts")]
        [TableFieldProperty(c_champContextesException, 500)]
        public string ContexteExceptionComma
        {
            get
            {
                return (string)Row[c_champContextesException];
            }
            set
            {
                Row[c_champContextesException] = value;
            }
        }

        /// <summary>
        /// Contextes d'exception
        /// </summary>
        public HashSet<string> ContextesException
        {
            get
            {
                HashSet<string> dic = new HashSet<string>();
                string[] strCtxs = ContexteExceptionComma.Split(';');
                foreach (string strCtx in strCtxs)
                {
                    if (strCtx.Length > 0)
                        dic.Add(strCtx);
                }
                return dic;
            }
            set
            {
                StringBuilder bl = new StringBuilder();
                foreach (string strVal in value)
                {
                    bl.Append(strVal);
                    bl.Append(';');
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                ContexteExceptionComma = bl.ToString();
            }
        }

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Menu permettant l'accès à l'évenement dans le cas d'un évenement manuel<BR></BR>
		/// Chaque élément de menu est séparé par un "/"
		/// </summary>
		[TableFieldProperty(c_champMenu, 255)]
		[DynamicField("Manual menu")]
		public string MenuManuel
		{
			get
			{
				return (string)Row[c_champMenu];
			}
			set
			{
				Row[c_champMenu] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Ids des groupes pour lesquels l'utilisation manuelle est autorisée<BR></BR>
		/// Les ids de groupe sont entourés par des #
		/// </summary>
		[TableFieldProperty(c_champIdsGroupesManuels, 512)]
		public string KeysGroupesPourExecutionManuelleString
		{
			get
			{
				return (string)Row[c_champIdsGroupesManuels];
			}
			set
			{
				Row[c_champIdsGroupesManuels] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
        public CDbKey[] KeysGroupesPourExecutionManuelle
        {
            get
            {
                //TESTDBKEYOK les groupes pour exécution manuelle ne sont plus exploités (Avril 2014)
                string[] strVals = KeysGroupesPourExecutionManuelleString.Split('#');
                List<CDbKey> lst = new List<CDbKey>();
                foreach (string strVal in strVals)
                {
                    if (strVal.Length > 0)
                    {
                        try
                        {
                            int nId;
                            if (int.TryParse(strVal, out nId))
                                lst.Add(CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId));
                            else
                                lst.Add(CDbKey.CreateFromStringValue(strVal));
                        }
                        catch
                        {
                        }
                    }
                }
                return lst.ToArray();
            }
            set
            {
                //TESTDBKEYOK les groupes pour exécution manuelle ne sont plus exploités (Avril 2014)
                
                string strListe = "";
                if (value != null)
                {
                    foreach (CDbKey key in value)
                        strListe += key.StringValue + "#";
                    if (strListe.Length > 0)
                        strListe = "#" + strListe;
                }
                KeysGroupesPourExecutionManuelleString = strListe;
            }
        }

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCondition, 1024)]
		public string FormuleConditionString
		{
			get
			{
				return( string)Row[c_champCondition];
			}
			set
			{
				Row[c_champCondition] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleCondition
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleConditionString);
				if ( expression == null )
					expression = new C2iExpressionVrai();
				return expression;
			}
			set
			{
				FormuleConditionString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champProprieteSurveillee, 256)]
		public string ProprieteSurveilleeString
		{
			get
			{
				return (string)Row[c_champProprieteSurveillee];
			}
			set
			{
				Row[c_champProprieteSurveillee]=value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ProprieteSurveillee
		{
			get
			{
				string strData = ProprieteSurveilleeString;
				if ( strData == "" )
					return null;
				CStringSerializer serializer = new CStringSerializer(strData, ModeSerialisation.Lecture );
				I2iSerializable objet = null;
				if ( !serializer.TraiteObject ( ref objet ) )
					return null;
				if ( !(objet is CDefinitionProprieteDynamique) )
					return null;
				return (CDefinitionProprieteDynamique)objet;
			}
			set
			{
				if ( value == null )
					ProprieteSurveilleeString = "";
				else
				{
					String strData = "";
					CStringSerializer serializer = new CStringSerializer ( strData, ModeSerialisation.Ecriture );
					I2iSerializable objet = (I2iSerializable)value;
					serializer.TraiteObject ( ref objet );
					ProprieteSurveilleeString = serializer.String;
				}
			}
		}

		

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champValeurAvant, 255)]
		public string FormuleValeurAvantString
		{
			get
			{
				return (string)Row[c_champValeurAvant];
			}
			set
			{
				Row[c_champValeurAvant] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurAvant
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleValeurAvantString);
				return expression;
			}
			set
			{
				FormuleValeurAvantString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champValeurApres, 255)]
		public string FormuleValeurApresString
		{
			get
			{
				return (string)Row[c_champValeurApres];
			}
			set
			{
				Row[c_champValeurApres] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurApres
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleValeurApresString);
				return expression;
			}
			set
			{
				FormuleValeurApresString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeCible, 255)] 
		[IndexField]
		public string TypeCibleString
		{
			get
			{
				return (string)Row[c_champTypeCible];
			}
			set
			{
				Row[c_champTypeCible] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public Type TypeCible 
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeCibleString );
			}
			set
			{
				if ( value != null )
					TypeCibleString = value.ToString();
			}
		}

		/// /////////////////////////////////////////////////////////
		[DynamicField("Target type")]
		public string TypeCibleConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeCible);
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldPropertyAttribute(c_champIdCible)]
		[DynamicField("Target ID")]
		[IndexField]
		public int IdElementCible
		{
			get
			{
				return ( int )Row[c_champIdCible];
			}
			set
			{
				Row[c_champIdCible] = value;
			}
		}

		
		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champDateHeureDeclenchement, true)]
		[DynamicField("Release datetime")]
		public CDateTimeEx DateHeure
		{
			get
			{
				if ( Row[c_champDateHeureDeclenchement] == DBNull.Value )
					return null;
				else
					return new CDateTimeEx ( (DateTime)Row[c_champDateHeureDeclenchement] );
			}
			set
			{
				if ( value == null )
					Row[c_champDateHeureDeclenchement] = DBNull.Value;
				else
					Row[c_champDateHeureDeclenchement] = value.DateTimeValue;
			}
		}

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champAutoSupprimable)]
		public bool AutoSuppression
		{
			get
			{
				return ( bool )Row[c_champAutoSupprimable] || EtapeWorkflowATerminer != null;
			}
			set
			{
				Row[c_champAutoSupprimable] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public Type TypeElement
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeCibleString );
			}
			set
			{
				TypeCibleString = value.ToString();
			}
		}

		/// /////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique ElementSurveille
		{
			get
			{
				try
				{
					Type tp = TypeCible;
					if ( tp == null )
						return null;
#if PDA
					CObjetDonneeAIdNumeriqueAuto obj = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( tp );
					obj.ContexteDonnee =ContexteDonnee;
#else
					CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new Object[]{ContexteDonnee});
#endif
					if ( obj.ReadIfExists ( IdElementCible ) )
						return obj;
					return null;
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if ( value == null )
					return;
				IdElementCible = value.Id;
				TypeCible = value.GetType();
			}
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// Si le handle est créé par un CEvenement, contient le CEVenement
		/// qui a créé le handle
		/// </summary>
		[Relation ( CEvenement.c_nomTable, CEvenement.c_champId, CEvenement.c_champId, false, true)]
		public CEvenement EvenementLie
		{
			get
			{
				return (CEvenement)GetParent ( CEvenement.c_champId, typeof(CEvenement));
			}
			set
			{
				SetParent ( CEvenement.c_champId, value );
			}
		}

		//-------------------------------------------------------------------
		
		/// <summary>
		/// Si le handle a été programmé par un process en execution,
		/// indique le process et l'id de l'action à executer
		/// </summary>
		[Relation(CProcessEnExecutionInDb.c_nomTable,
			 CProcessEnExecutionInDb.c_champId,
			 CProcessEnExecutionInDb.c_champId,
			 false,
			 true,
			 true)]
		[DynamicField("Source process")]
		public CProcessEnExecutionInDb ProcessSource
		{
			get
			{
				return ( CProcessEnExecutionInDb )GetParent ( CProcessEnExecutionInDb.c_champId, typeof(CProcessEnExecutionInDb) );
			}
			set
			{
				SetParent ( CProcessEnExecutionInDb.c_champId, value );
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Id de l'action à executer, -1 si rien
		/// </summary>
		[TableFieldProperty(c_champIdActionProcessSource)]
		[DynamicField("Process action Id")]
		public int IdActionDeProcessToExecute
		{
			get
			{
				return (int)Row[c_champIdActionProcessSource];
			}
			set
			{
				Row[c_champIdActionProcessSource] = value;
			}
		}

		//-------------------------------------------------------------------
		protected CParametreDeclencheurEvenement ParametreDeclencheurSiSurModification
		{
			get
			{
				if ( TypeEvenement !=  TypeEvenement.Modification )
					return null;
				CParametreDeclencheurEvenement parametre = new CParametreDeclencheurEvenement();
				parametre.Code = Code;
				parametre.FormuleDateProgramme = new C2iExpressionConstante ( DateHeure );
				parametre.FormuleValeurApres = FormuleValeurApres;
				parametre.FormuleValeurAvant = FormuleValeurAvant;
				parametre.FormuleConditionDeclenchement = FormuleCondition;
				parametre.ProprieteASurveiller = ProprieteSurveillee;
				parametre.TypeCible = TypeCible;
				parametre.TypeEvenement = TypeEvenement;
				parametre.MenuManuel = MenuManuel;
				parametre.KeysGroupesManuel = KeysGroupesPourExecutionManuelle;
				parametre.OrdreExecution = OrdreExecution;
				parametre.HideProgress = HideProgress;
				
				return parametre;
			}
		}


    
        //-------------------------------------------------------------------
        /// <summary>
        /// Si cet évenement est destinée à terminer un étape de workflow, 
        /// indique l'étape à terminer
        /// </summary>
        [Relation(
            CEtapeWorkflow.c_nomTable,
            CEtapeWorkflow.c_champId,
            CEtapeWorkflow.c_champId,
            false,
            true,
            false)]
        [DynamicField("Workflow step to end")]
        public CEtapeWorkflow EtapeWorkflowATerminer
        {
            get
            {
                return (CEtapeWorkflow)GetParent(CEtapeWorkflow.c_champId, typeof(CEtapeWorkflow));
            }
            set
            {
                SetParent(CEtapeWorkflow.c_champId, value);
            }
        }

		//-------------------------------------------------------------------
		//Retourne vrai si le handler doit déclencher son évenement
		public bool ShoulDeclenche ( CObjetDonneeAIdNumerique obj, ref CInfoDeclencheurProcess infoDeclencheur )
		{
			CParametreDeclencheurEvenement parametre = ParametreDeclencheurSiSurModification;
			if ( parametre == null )
				return false;
			return parametre.ShouldDeclenche ( obj, false, false, ref infoDeclencheur );
		}

        //-------------------------------------------------------------------
        public CResultAErreur RunEvent(
            CObjetDonneeAIdNumerique obj,
            CInfoDeclencheurProcess infoDeclencheur,
            IIndicateurProgression indicateur)
        {
            return RunEvent(true,
                obj,
                infoDeclencheur,
                indicateur);
        }


		//-------------------------------------------------------------------
		public CResultAErreur RunEvent ( 
            bool bAutoManagerHandlerTermine,
            CObjetDonneeAIdNumerique obj, 
            CInfoDeclencheurProcess infoDeclencheur, 
            IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			CProcessEnExecutionInDb process = ProcessSource;
            if (ProcessSource != null)
            {
                result = process.RepriseProcess(IdActionDeProcessToExecute, indicateur);
            }
             else if (EtapeWorkflowATerminer != null)
            {
                using (CContexteDonnee contexte = new CContexteDonnee(ContexteDonnee.IdSession, true, false))
                {
                    CEtapeWorkflow etape = EtapeWorkflowATerminer.GetObjetInContexte(contexte) as CEtapeWorkflow;
                    if ( etape != null )
                    {
                        result = etape.EndEtapeAndSaveIfOk();
                    }
                    if (!result)
                    {
                        //L'étape ne s'est pas terminée ? tant pis alors, c'est pas grave, elle se terminera par ailleurs
                        result = CResultAErreur.True;
                        return result;
                    }
                }
            }
            //gestion du handler faite ailleurs
            if ( !bAutoManagerHandlerTermine )
            {
                if ( !result )
                    result.EmpileErreur(I.T("@1 handler error|271", Libelle));
                return result;
            }
            EndHandler ( result );
            return result;
        }

        //-------------------------------------------------------------
        public CResultAErreur EndHandler ( CResultAErreur resultExecution )
        {
            using (CContexteDonnee ctx = new CContexteDonnee(ContexteDonnee.IdSession, true, false))
            {
                CHandlerEvenement handler = GetObjetInContexte(ctx) as CHandlerEvenement;
                if (resultExecution && AutoSuppression)
                    return handler.Delete();

                handler.BeginEdit();
                if (resultExecution)
                {
                    handler.EtatExecutionInt = (int)EtatHandlerAction.Termine;
                }
                else
                {
                    handler.NbEssaisEchoues++;
                    if (handler.NbEssaisEchoues > 3)
                    {
                        handler.DateHeure = handler.DateHeure.DateTimeValue.AddMinutes(40);
                        handler.EtatExecutionInt = (int)EtatHandlerAction.AExecuter;
                    }
                    else
                        handler.EtatExecutionInt = (int)EtatHandlerAction.Erreur;
                    handler.MessageErreur = resultExecution.Erreur.ToString();
                }
                handler.CommitEdit();
                return CResultAErreur.True;
            }
        }
           

		//-------------------------------------------------------------------
		public CResultAErreur RunEventMultiple ( CObjetDonneeAIdNumerique[] obj, CInfoDeclencheurProcess infoDeclencheur, IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Impossible to execute on multiple elements|272"));
			return result;
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Etat d'execution.<BR></BR>
		/// 0 : A executer
		/// 1 : Terminé
		/// 2 : Erreur d'execution
		/// </summary>
		[TableFieldProperty(c_champEtatExecution)]
		[DynamicField("Running state")]
		public int EtatExecutionInt
		{
			get
			{
				return ( int )Row[c_champEtatExecution];
			}
			set
			{
				Row[c_champEtatExecution] = value;
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Message d'erreur ( si l'execution a échoué )
		/// </summary>
		[TableFieldProperty(c_champErreur, 1024)]
		[DynamicField("Error")]
		public string MessageErreur
		{
			get
			{
				return ( string )Row[c_champErreur];
			}
			set
			{
				Row[c_champErreur] = value;
			}
		}

		/// <summary>
		/// Ordre d'execution de l'évement.
		/// Si plusieurs evenements sont déclenchés sur un entité,
		/// ils sont déclenchés dans l'ordre d'execution.
		/// </summary>
		[TableFieldProperty( c_champOrdreExecution ) ]
		[DynamicField("Running sequence")]
		public int OrdreExecution
		{
			get
			{
				return ( int )Row[c_champOrdreExecution];
			}
			set
			{
				Row[c_champOrdreExecution] = value;
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Nombre d'éssais d'execution qui ont raté
		/// </summary>
		[TableFieldProperty ( CHandlerEvenement.c_champNbEssaisEchoues )]
		public int NbEssaisEchoues
		{
			get
			{
				return ( int )Row[c_champNbEssaisEchoues];
			}
			set
			{
				Row[c_champNbEssaisEchoues] = value;
			}
		}

        //----------------------------------------------------------------------
        public bool DeclencherSurContexteClient
        {
            get
            {
                return false;
            }
        }

        //----------------------------------------------------------------------
        public CResultAErreur EnregistreDeclenchementEvenementSurClient(
            CObjetDonneeAIdNumerique objet,
            CInfoDeclencheurProcess infoDeclencheur,
            IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            result.EmpileErreur(I.T("Not available for event handlers|50000"));
            return result;
        }

        public static CResultAErreurType<CHandlerEvenement> CreateHandlerOnObject(
            CContexteDonnee ctxDonnee,
            CObjetDonneeAIdNumerique objetCible,
            CParametreDeclencheurEvenement parametreDeclencheur,
            string strLibelle,
            string strCode)
        {
            bool bShouldCreate = true;

            CResultAErreurType<CHandlerEvenement> result = new CResultAErreurType<CHandlerEvenement>();
            result.Result = true;


            //Vérifie que le handler n'existe pas déjà
            CHandlerEvenement handler = new CHandlerEvenement(ctxDonnee);
            if (!handler.ReadIfExists(
                new CFiltreData(
                CHandlerEvenement.c_champTypeCible + "=@1 and " +
                CHandlerEvenement.c_champIdCible + "=@2 and " +
                CHandlerEvenement.c_champCode + "=@3",
                objetCible.GetType().ToString(),
                objetCible.Id,
                strCode)))
                handler = null;

            //Programme le handler
            if (handler == null)
            {
                handler = new CHandlerEvenement(ctxDonnee);
                handler.CreateNewInCurrentContexte();
            }
            handler.Libelle = strLibelle;
            handler.Code = strCode;
            handler.TypeEvenement = parametreDeclencheur.TypeEvenement;
            handler.FormuleCondition = parametreDeclencheur.FormuleConditionDeclenchement;
            handler.ProprieteSurveillee = parametreDeclencheur.ProprieteASurveiller;
            handler.FormuleValeurAvant = parametreDeclencheur.FormuleValeurAvant;
            handler.FormuleValeurApres = parametreDeclencheur.FormuleValeurApres;
            handler.ElementSurveille = objetCible;
            handler.DateHeure = null;
            handler.ContextesException = parametreDeclencheur.ContextesException;
            handler.HideProgress = parametreDeclencheur.HideProgress;

            //Si date, évalue la formule de date
            if (parametreDeclencheur.TypeEvenement == TypeEvenement.Date)
            {
                object valeur = null;
                if (parametreDeclencheur.ProprieteASurveiller != null)
                    valeur = CParametreDeclencheurEvenement.GetValeur(objetCible, parametreDeclencheur.ProprieteASurveiller, DataRowVersion.Current);
                if (valeur is CDateTimeEx)
                    valeur = ((CDateTimeEx)valeur).DateTimeValue;
                if (valeur is DateTime? && valeur != null)
                    valeur = ((DateTime?)valeur).Value;
                if (valeur is DateTime)
                {
                    CObjetForTestValeurChampCustomDateTime objetEval = new CObjetForTestValeurChampCustomDateTime((DateTime)valeur);
                    CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(objetEval);
                    contexteEval.AttacheObjet(typeof(CContexteDonnee), ctxDonnee);
                    if (parametreDeclencheur.FormuleDateProgramme == null)
                    {
                        result.EmpileErreur(I.T("Impossible to evaluate the date formula for the event release @1|149", strLibelle));
                        return result;
                    }
                    CResultAErreur resTmp = parametreDeclencheur.FormuleDateProgramme.Eval(contexteEval);
                    if (!resTmp)
                        result.EmpileErreur(resTmp.Erreur);
                }
                else
                {
                    result.EmpileErreur(I.T("The date value is incorrect|148"));
                    return result;
                }

                if (!result || !(result.Data is DateTime))
                {
                    result.EmpileErreur(I.T("Date formula is incorrect in the @1 release|150", strLibelle));
                    return result;
                }
                handler.DateHeure = new CDateTimeEx((DateTime)result.Data);
            }
            result.DataType = handler;
            return result;
        }
            
	}
}
