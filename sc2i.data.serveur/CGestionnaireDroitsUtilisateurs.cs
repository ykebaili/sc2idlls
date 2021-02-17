using System;
using System.Collections;
using System.Data;
using System.Reflection;

using sc2i.data.serveur;
using sc2i.data;
using sc2i.multitiers.client;


namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CConnexionDataSourceDroits.
	/// </summary>
	[sc2i.common.AutoExec("BrancheSurDomaine")]
	public class CGestionnaireDroitsUtilisateurs : CMemoryDataSetConnexion, IGestionnaireDroitsUtilisateurs
	{
		private static CContexteDonnee m_datasetDroits = new CContexteDonnee(0, true, false);

		public CGestionnaireDroitsUtilisateurs( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// <summary>
		/// ////////////////////////////////////////////////
		/// </summary>
		public override string ConnexionString
		{
			get
			{
				return "DROITS_UTILISATEURS";
			}
			set
			{
			}
		}

		/// ////////////////////////////////////////////////
		protected override DataSet DataSetProtected
		{
			get
			{
				return m_datasetDroits;
			}
			set
			{
				m_datasetDroits = (CContexteDonnee)value;
			}
		}

#if !PDA
		/// ////////////////////////////////////////////////
		public static void BrancheSurDomaine (  )
		{
			foreach ( System.Reflection.Assembly ass in AppDomain.CurrentDomain.GetAssemblies() )
			{
				RegisterDroitsAssembly ( ass );
			}
			AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(OnLoadAssembly);
		}
#endif
#if !PDA
		/// ////////////////////////////////////////////////
		private static void RegisterDroitsAssembly ( Assembly ass )
		{
			foreach ( Type tp in ass.GetExportedTypes() )
			{
				object[] attribs = tp.GetCustomAttributes(typeof(DroitAssocieAttribute), true);
				if ( attribs.Length != 0 )
					foreach ( DroitAssocieAttribute droit in attribs )
						RegisterDroit ( droit.CodeDroit, droit.Libelle, droit.NumOrdre, droit.DroitParent, droit.InfoSurDroit, droit.OptionsPossibles, droit.TypeAssocie );
			}
		}
#endif

#if !PDA
		/// ////////////////////////////////////////////////
		public static void OnLoadAssembly ( object sender, AssemblyLoadEventArgs args )
		{
			RegisterDroitsAssembly ( args.LoadedAssembly );
		}
#endif

		/// ////////////////////////////////////////////////
		public static void RegisterDroit (
			string strCode,
			string strLibelle,
			int nNumOrdre,
			string strDroitParent,
			string strInfoSurDroit)
		{
			RegisterDroit (
				strCode,
				strLibelle,
				nNumOrdre,
				strDroitParent,
				strInfoSurDroit,
				OptionsDroit.Aucune,
				null );
		}

		/// ////////////////////////////////////////////////
		public void RegisterDroitUtilisateur ( 
			string strCode,
			string strLibelle,
			int nNumOrdre,
			string strDroitParent,
			string strInfoSurDroit,
			OptionsDroit optionsPossibles )
		{
			RegisterDroit ( strCode, strLibelle, nNumOrdre, strDroitParent, strInfoSurDroit, optionsPossibles );
		}

		/// ////////////////////////////////////////////////
		public static void RegisterDroit (
			string strCode,
			string strLibelle,
			int nNumOrdre,
			string strDroitParent,
			string strInfoSurDroit,
			OptionsDroit optionsPossibles )
		{

			RegisterDroit (
				strCode,
				strLibelle,
				nNumOrdre,
				strDroitParent,
				strInfoSurDroit,
				optionsPossibles,
				null );
		}

		/// ////////////////////////////////////////////////
		public static void RegisterDroit  ( 
			string strCode, 
			string strLibelle, 
			int nNumOrdre, 
			string strDroitParent,
			string strInfoSurDroit,
			OptionsDroit nOptionsPossibles,
			Type typeObjetAssocie
			)
		{
#if !PDA_DATA
			AssureStructure();
			CDroitUtilisateur droit = new CDroitUtilisateur(m_datasetDroits);
			if ( !droit.ReadIfExists ( new object[]{strCode} ) )
				droit.CreateNewInCurrentContexte(new Object[] {strCode});
			droit.Libelle = strLibelle;
			droit.NumOrdre = nNumOrdre;
			droit.Infos = strInfoSurDroit;
			CDroitUtilisateur droitParent = null;
			if ( strDroitParent != null && strDroitParent.Trim() != "")
			{
				droitParent = new CDroitUtilisateur(m_datasetDroits);
				if ( !droitParent.ReadIfExists(new Object[]{strDroitParent} ) )
				{
					//Création du droit parent simple, il pourra être modifié par une autre définition
					droitParent.CreateNewInCurrentContexte(new object[]{strDroitParent});
					droitParent.Libelle = strDroitParent;
					droitParent.NumOrdre = 0;
				}
			}
			droit.DroitParent = droitParent;
			if ( typeObjetAssocie == null )
				droit.TypeAssocieURI = null;
			else
				droit.TypeAssocieURI = typeObjetAssocie.ToString();
			droit.OptionsPossibles = nOptionsPossibles;
#endif
		}
#if !PDA_DATA
		/// ////////////////////////////////////////////////
		private static void AssureStructure()
		{
			lock ( m_datasetDroits )
			{
				if ( m_datasetDroits.Tables.Count != 0 )
					return;
				CCreateurStructureContexteDonneeFromObjetsDonnee createur = new CCreateurStructureContexteDonneeFromObjetsDonnee(m_datasetDroits);
				createur.CreateStructureFromTypes( typeof(CDroitUtilisateur));
			}
		}
#endif
	}

}
