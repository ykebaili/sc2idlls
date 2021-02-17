using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.client;



namespace sc2i.data
{
#if !PDA_DATA

	/// <summary>
	/// Représente un droit sur l'application
	/// </summary>
	[Table(CDroitUtilisateur.c_nomTable, CDroitUtilisateur.c_champCode, false)]
	[ObjetServeurURI("CDroitUtilisateurServeur")]
    [NoIdUniversel]
	public class CDroitUtilisateur : CObjetDonnee, IObjetALectureTableComplete, IObjetSansVersion
	{
		public const string c_nomTable = "RIGHTS_DEFINITION";
		public const string c_champCode = "RD_ID";
		public const string c_champLibelle = "RD_LABEL";
		public const string c_champNumOrdre = "RD_ORDER_NUM";
		public const string c_champCodeDroitParent = "RD_PARENT_RIGHT";
		public const string c_champTypeAssocie = "RD_ASSOCIATED_TYPE";
		public const string c_champInfoSurDroit = "RD_INFO";
		//int = combinaison de bits
		public const string c_champOption = "RD_POSSIBLES_OPTIONS";

		/// //////////////////////////////////////////////////////////////////////////////
		public CDroitUtilisateur( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public CDroitUtilisateur ( DataRow row )
			:base(row)
		{
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Application right @1 : @2|195",Code,Libelle);
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
				public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champNumOrdre};
		}

		/// //////////////////////////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			NumOrdre = 1;
			OptionsPossibles = 0;
		}

		/// //////////////////////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCode, 256)]
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

		/// //////////////////////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champLibelle, 64)]
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

		/// //////////////////////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champNumOrdre)]
		public int NumOrdre
		{
			get
			{
				return (int)Row[c_champNumOrdre];
			}
			set
			{
				Row[c_champNumOrdre] = value;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////
		/*Si le droit a un type associé, cela signifie qu'il est possible
		 * de sélectionner pour chaque utilisateur ou groupe une liste d'élements
		 * de ce type.
		 * Les éléments doivent être des CObjetDonneeAIdAuto
		 */
		[TableFieldProperty(c_champTypeAssocie, 1024, NullAutorise = true)]
		public string TypeAssocieURI
		{
			get
			{
				if ( Row[c_champTypeAssocie] == DBNull.Value )
					return null;
				return (string)Row[c_champTypeAssocie];
			}
			set
			{
				if ( value == null )
					Row[c_champTypeAssocie] = DBNull.Value;
				Row[c_champTypeAssocie] = value;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////
		public CListeObjetsDonnees ListeObjetsAssocies
		{
			get
			{
				Type tp = CActivatorSurChaine.GetType(TypeAssocieURI);
				if ( tp == null )
					return null;
				return new CListeObjetsDonnees(ContexteDonnee, tp );
			}
		}


		/// //////////////////////////////////////////////////////////////////////////////
		[Relation(c_nomTable, c_champCode, c_champCodeDroitParent, false, false)]
		public CDroitUtilisateur DroitParent
		{
			get
			{
				return (CDroitUtilisateur)GetParent ( new string[]{c_champCodeDroitParent}, typeof(CDroitUtilisateur));
			}
			set
			{
				SetParent ( new string[]{c_champCodeDroitParent}, value );
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public CListeObjetsDonnees DroitFils
		{
			get
			{
				return GetDependancesListe ( c_nomTable, c_champCodeDroitParent );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////
		[TableFieldPropertyAttribute(c_champInfoSurDroit, 1024)]
		public string Infos
		{
			get
			{
				return (string)Row[c_champInfoSurDroit];
			}
			set
			{
				Row[c_champInfoSurDroit] = value;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		[TableFieldPropertyAttribute(c_champOption)]
		public int OptionsPossiblesInt
		{
			get
			{
				return (int)Row[c_champOption];
			}
			set
			{
				Row[c_champOption] = value;
			}
		}

		public OptionsDroit OptionsPossibles
		{
			get
			{
				return (OptionsDroit)OptionsPossiblesInt;
			}
			set
			{
				OptionsPossiblesInt = (int)value;
			}
		}


		/////////////////////////////////////////////////////////////////////////////////
		public OptionsDroit[] ListeOptionsPossibles
		{
			get
			{
				ArrayList list = new ArrayList();
				for ( int n = 1; n < Int32.MaxValue/2; n*=2 )
				{
					if ( (OptionsPossiblesInt & n)==n )
						list.Add ( (OptionsDroit)n);
				}
				return (OptionsDroit[])list.ToArray(typeof(OptionsDroit));
			}
		}

		/////////////////////////////////////////////////////////////////////////////////
        public override CResultAErreur Delete(bool bDansContexteCourant)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Cannot delete an application right|129"));
			C2iEventLog.WriteErreur(I.T("Attempt to delete a right from the database|130"));
			return result;
		}


		/////////////////////////////////////////////////////////////////////////////////
		public static CResultAErreur RegisterDroitUtilisateur (
			int nIdSession,
			string strCode,
			string strLibelle,
			int nNumOrdre,
			string strDroitParent,
			string strInfoSurDroit,
			OptionsDroit optionsPossibles )
		{
			CResultAErreur result = CResultAErreur.True;
			IGestionnaireDroitsUtilisateurs gestionnaire = (IGestionnaireDroitsUtilisateurs)C2iFactory.GetNewObjetForSession ( "CGestionnaireDroitsUtilisateurs", typeof(IGestionnaireDroitsUtilisateurs), nIdSession );
			if ( gestionnaire == null )
			{
				result.EmpileErreur (I.T("Cannot allocate rights manager|131"));
				return result;
			}
			gestionnaire.RegisterDroitUtilisateur ( 
				strCode,
				strLibelle,
				nNumOrdre,
				strDroitParent,
				strInfoSurDroit,
				optionsPossibles );
			return result;
		}

	}
#else
	/// <summary>
	/// Pour que l'assembly Cafel.user soit définit
	/// </summary>
	class DummyClass
	{
	}
#endif
}
