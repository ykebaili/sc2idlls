using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionSupprimerHandlerEvenement.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionSupprimerHandlerEvenement : CActionLienSortantSimple
	{
        /* TESTDBKEYOK (XL)*/

        // Element duquel enlevement le handler
        private string m_strIdVariableElement = "";

		private string m_strCodeHandler = "";
		/// /////////////////////////////////////////
        public CActionSupprimerHandlerEvenement(CProcess process)
            : base(process)
        {
            Libelle = I.T("Delete event|244");
        }

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Delete event|244"),
				I.T("Deletes an event on an element|245"),
				typeof(CActionSupprimerHandlerEvenement),
				CGestionnaireActionsDisponibles.c_categorieComportement );
		}

		/// /////////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			if ( GetLiensSortantHorsErreur().Length == 0 )
				return new CLienAction[]{new CLienAction(Process)};
			return new CLienAction[0];
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			table[m_strIdVariableElement] = true;
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            return 1; // Passage de int IdVariableElement en String
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
			serializer.TraiteString ( ref m_strCodeHandler );

            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableElement = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableElement);

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
			}
		}

		/// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableElement
        {
            get
            {
                return Process.GetVariable(IdVariableElement);
            }
            set
            {
                if (value == null)
                    m_strIdVariableElement = "";
                else
                {
                    m_strIdVariableElement = value.IdVariable;
                }
            }
        }

		/// ////////////////////////////////////////////////////////
		public string CodeEvenement
		{
			get
			{
				return m_strCodeHandler;
			}
			set
			{
				m_strCodeHandler = value;
			}
		}
				
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()		{
			CResultAErreur result = base.VerifieDonnees();

			CVariableDynamique variable = VariableElement;
			if ( variable == null )
			{
				result.EmpileErreur(I.T( "The variable containing the element for which the events are started must be exist|161"));
			}

			if ( !(variable is CVariableProcessTypeComplexe) || 
				!typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif) )
			{
				//ce n'est pas un CObjetDonneeAIdNumeriqueAuto
				result.EmpileErreur(I.T( "The variable containing the element for which the events are started is incorrect|162"));
				return result;
			}

			if ( CodeEvenement.Trim() == "" )
				result.EmpileErreur(I.T("Indicate the code of event to be removed|246"));

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			if ( CodeEvenement.Trim() != "" )
			{
				//Evalue l'élément cible
				object obj = Process.GetValeurChamp ( IdVariableElement );
				if ( obj != null )
				{
					if ( obj is CObjetDonneeAIdNumerique )
					{
						CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte.ContexteDonnee, typeof(CHandlerEvenement) );
						liste.Filtre = new CFiltreData ( 
							CHandlerEvenement.c_champTypeCible+"=@1 and "+
							CHandlerEvenement.c_champIdCible+"=@2 and "+
							CHandlerEvenement.c_champCode+"=@3",
							obj.GetType().ToString(),
							((CObjetDonneeAIdNumerique)obj).Id,
							CodeEvenement );
						foreach ( CHandlerEvenement handler in liste.ToArrayList() )
						{
							result = handler.Delete();
							if ( !result )
								return result;
						}
					}
				}
			}


			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
			DrawVariableEntree ( ctx.Graphic, VariableElement );
		}




	}
}
