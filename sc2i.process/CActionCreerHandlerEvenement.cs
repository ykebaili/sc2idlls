using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using System.Data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionCreerHandlerEvenement.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionCreerHandlerEvenement : CAction
	{
		public enum TypeGestionCode
		{
			PasDeGestionCode = 0,
			RemplacerSiExiste,
			NePasCreerSiExiste
		}

        /* TESTDBKEYOK (XL)*/

        // Element sur lequel l'évenement sera déclenché
        private string m_strIdVariableElement = "";
		
		//Indique que l'évenement ne se déclenche qu'une seule fois
		private bool m_bDeclenchementUnique = false;

		private string m_strLibelleEvenement = "";

		/// <summary>
		/// Indique comment le code d'évenement est  utilisé
		/// </summary>
		private TypeGestionCode m_typeGestionCode = TypeGestionCode.RemplacerSiExiste;


		private CParametreDeclencheurEvenement m_parametreDeclencheur = new CParametreDeclencheurEvenement();

		/// /////////////////////////////////////////
		public CActionCreerHandlerEvenement( CProcess process )
			:base(process)
		{
			Libelle = I.T("Event|142");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Event|142"),
				I.T("Allows to supervise the release of an event attach on an element|143"),
				typeof(CActionCreerHandlerEvenement),
				CGestionnaireActionsDisponibles.c_categorieComportement );
		}

		/// /////////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			bool bHasStd = false;
			bool bHasEvenement = false;
			foreach ( CLienAction lien in GetLiensSortantHorsErreur() )
			{
				if ( lien is CLienEvenement )
					bHasEvenement = true;
				else
					bHasStd = true;
			}
			ArrayList lst = new ArrayList();
			if ( !bHasStd )
				lst.Add ( new CLienAction(Process));
			if ( !bHasEvenement )
				lst.Add ( new CLienEvenement(Process));
			return ( CLienAction[] )lst.ToArray ( typeof(CLienAction));
		}


		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			table[m_strIdVariableElement] = true;
			AddIdVariablesExpressionToHashtable ( ParametreDeclencheur.FormuleConditionDeclenchement, table );
			AddIdVariablesExpressionToHashtable ( ParametreDeclencheur.FormuleDateProgramme, table );
			AddIdVariablesExpressionToHashtable ( ParametreDeclencheur.FormuleValeurApres, table );
			AddIdVariablesExpressionToHashtable ( ParametreDeclencheur.FormuleValeurAvant, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////////////////////
		[DescriptionField]
		public string LibelleEvenement
		{
			get
			{
				return m_strLibelleEvenement;
			}
			set
			{
				m_strLibelleEvenement = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public TypeGestionCode TypeDeGestionDuCodeEvenement
		{
			get
			{
				return m_typeGestionCode;
			}
			set
			{
				m_typeGestionCode = value;
			}
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 3;
            return 4; // Passage de Id Variable element en String
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			if ( !result )
				return result;

            if (nVersion < 4 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableElement = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableElement);

			I2iSerializable objet = ParametreDeclencheur;
			result = serializer.TraiteObject(ref objet);
			ParametreDeclencheur = (CParametreDeclencheurEvenement)objet;

			serializer.TraiteBool ( ref m_bDeclenchementUnique );

			if ( nVersion >= 1 )
				serializer.TraiteString(ref m_strLibelleEvenement);


			if ( nVersion >= 3 )
			{
				int nVal = (int)TypeDeGestionDuCodeEvenement;
				serializer.TraiteInt ( ref nVal );
				TypeDeGestionDuCodeEvenement = (TypeGestionCode)nVal;
			}
			else if ( nVersion >= 2 )
			{
				//Avant, le type de gestion du code evenement était un boolean (NePasCreerSiExiste=true)
				bool bTmp = TypeDeGestionDuCodeEvenement == TypeGestionCode.NePasCreerSiExiste;
				serializer.TraiteBool ( ref bTmp );
				if ( bTmp )
					TypeDeGestionDuCodeEvenement = TypeGestionCode.NePasCreerSiExiste;
				else
					TypeDeGestionDuCodeEvenement = TypeGestionCode.PasDeGestionCode;
			}
			else
				TypeDeGestionDuCodeEvenement = TypeGestionCode.RemplacerSiExiste;

			return result;
		}

		/// <summary>
		/// La variable contient l'élément sur lequel l'évenement sera déclenché
		/// </summary>
        public string IdVariableElement
        {
            get
            {
                return m_strIdVariableElement;
            }
            set
            {
                m_strIdVariableElement = value;
                if (VariableElement != null)
                {
                    Type tp = VariableElement.TypeDonnee.TypeDotNetNatif;
                    ParametreDeclencheur.TypeCible = tp;
                }
            }
        }

		/// ////////////////////////////////////////////////////////
		public CVariableDynamique VariableElement
		{
			get
			{
				return Process.GetVariable ( IdVariableElement );
			}
			set
			{
                if (value == null)
                {
                    m_strIdVariableElement = "";
                }
                else
                {
                    m_strIdVariableElement = value.IdVariable;
                    ParametreDeclencheur.TypeCible = value.TypeDonnee.TypeDotNetNatif;
                }
			}
		}

		

		/// ////////////////////////////////////////////////////////
		public bool DeclenchementUnique
		{
			get
			{
				return m_bDeclenchementUnique;
			}
			set
			{
				m_bDeclenchementUnique = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CParametreDeclencheurEvenement ParametreDeclencheur
		{
			get
			{
				return (this.m_parametreDeclencheur);
			}
			set
			{
				this.m_parametreDeclencheur = value;
			}
		}

		
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()		{
			CResultAErreur result = base.VerifieDonnees();

			CVariableDynamique variable = VariableElement;
			if ( variable == null )
			{
				result.EmpileErreur(I.T("The variable containing the element attach with the event isn't defined or doesn't exist|144"));
			}

			if ( !typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif) )
			{
				//ce n'est pas un CObjetDonneeAIdNumeriqueAuto
				result.EmpileErreur(I.T("The variable containing the element attach with the event is incorrect|145"));
				return result;
			}

			if ( Libelle.Trim() == "" )
				result.EmpileErreur(I.T("The label can't be null|146"));

			result = ParametreDeclencheur.VerifieDonnees();
			if ( !result )
				return result;

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CLienAction lienEvenement = null, lienStd = null;

			//Trouve le lien sortant evenement
			foreach ( CLienAction lien in GetLiensSortantHorsErreur() )
			{
				if ( lien is CLienEvenement )
					lienEvenement = lien;
				else
					lienStd = lien;
			}
			if ( lienEvenement != null )
			{
				//Evalue l'élément cible

                object obj = null;
                obj = Process.GetValeurChamp(IdVariableElement);
                if (obj != null && obj.Equals(Process))
                    obj = contexte.ProcessEnExecution;

				if ( obj != null )
				{
					if ( !(obj is CObjetDonneeAIdNumerique ) )
					{
						result.EmpileErreur(I.T("The supervised element by the event @1 isn't correct|147",Libelle));
						return result;
					}

					CHandlerEvenement handler = new CHandlerEvenement ( contexte.ContexteDonnee );
					bool bShouldCreate = true;
					if ( TypeDeGestionDuCodeEvenement != TypeGestionCode.PasDeGestionCode )
					{
						//Vérifie que le handler n'existe pas déjà
						CHandlerEvenement handlerExistant = new CHandlerEvenement ( contexte.ContexteDonnee );
						if ( handlerExistant.ReadIfExists ( 
							new CFiltreData ( 
							CHandlerEvenement.c_champTypeCible+"=@1 and "+
							CHandlerEvenement.c_champIdCible+"=@2 and "+
							CHandlerEvenement.c_champCode+"=@3",
							obj.GetType().ToString(),
							((CObjetDonneeAIdNumerique)obj).Id,
							ParametreDeclencheur.Code ) ) )
						{
							switch ( TypeDeGestionDuCodeEvenement )
							{
								case TypeGestionCode.NePasCreerSiExiste :
									bShouldCreate = false;
									break;
								case TypeGestionCode.RemplacerSiExiste :
									handler = handlerExistant;
									bShouldCreate = true;
									break;
							}
						}

					}
					if ( bShouldCreate )
					{
						int nIdActionArrivee = lienEvenement.IdActionArrivee;
						//Programme le handler
						if ( handler == null )
						{
							handler = new CHandlerEvenement ( contexte.ContexteDonnee );
							handler.CreateNewInCurrentContexte();
						}
						handler.Libelle = LibelleEvenement;
						handler.Code = ParametreDeclencheur.Code;
						handler.TypeEvenement = ParametreDeclencheur.TypeEvenement;
						handler.FormuleCondition = ParametreDeclencheur.FormuleConditionDeclenchement;
						handler.ProprieteSurveillee = ParametreDeclencheur.ProprieteASurveiller;
						handler.FormuleValeurAvant = ParametreDeclencheur.FormuleValeurAvant;
						handler.FormuleValeurApres = ParametreDeclencheur.FormuleValeurApres;
						handler.ElementSurveille = (CObjetDonneeAIdNumerique)obj;
						handler.DateHeure = null;
                        handler.ContextesException = ParametreDeclencheur.ContextesException;
                        handler.HideProgress = ParametreDeclencheur.HideProgress;

						//Si date, évalue la formule de date
						if ( ParametreDeclencheur.TypeEvenement == TypeEvenement.Date )
						{
							object valeur = null;
							if (ParametreDeclencheur.ProprieteASurveiller != null)
								valeur = CParametreDeclencheurEvenement.GetValeur((CObjetDonnee)obj, ParametreDeclencheur.ProprieteASurveiller, DataRowVersion.Current);
							if ( valeur is CDateTimeEx  )
								valeur = ((CDateTimeEx)valeur).DateTimeValue;
							if ( valeur is DateTime? && valeur != null )
								valeur = ((DateTime?)valeur).Value;
							if (valeur is DateTime)
							{
								CObjetForTestValeurChampCustomDateTime objetEval = new CObjetForTestValeurChampCustomDateTime((DateTime)valeur);
								CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(objetEval);
								contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
								if (ParametreDeclencheur.FormuleDateProgramme == null)
								{
									result.EmpileErreur(I.T("Impossible to evaluate the date formula for the event release @1|149", Libelle));
									return result;
								}
								result = ParametreDeclencheur.FormuleDateProgramme.Eval(contexteEval);
							}
							else
							{
								result.EmpileErreur (I.T("The date value is incorrect|148") );
								return result;
							}

							if ( !result || !(result.Data is DateTime))
							{
								result.EmpileErreur(I.T("Date formula is incorrect in the @1 release|150",Libelle));
								return result;
							}
							handler.DateHeure = new CDateTimeEx ( (DateTime)result.Data );
						}
						handler.ProcessSource = contexte.ProcessEnExecution;
						handler.IdActionDeProcessToExecute = nIdActionArrivee;
					}
				}
			}
            
			result.Data = lienStd;

			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
			Graphics g = ctx.Graphic;
			DrawVariableEntree ( g, VariableElement );
		}




	}
}
