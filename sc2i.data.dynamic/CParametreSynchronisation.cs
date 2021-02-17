using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Liste des filtres de syncrhonisation.
	/// Un paramètre de synchronisation s'identifie par son Id
	/// </summary>
	////////////////////////////////////////////////////////////////////////
	public class CParametreSynchronisation : I2iSerializable
	{
		private ArrayList m_listefiltresSynchronisation = new ArrayList();

		////////////////////////////////////////////////////////////////////////
		public CParametreSynchronisation()
		{
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreSynchronisation[] Filtres
		{
			get
			{
				return (CFiltreSynchronisation[])m_listefiltresSynchronisation.ToArray (typeof(CFiltreSynchronisation));
			}
		}

		////////////////////////////////////////////////////////////////////////
		public void AddFiltre ( CFiltreSynchronisation filtre )
		{
			m_listefiltresSynchronisation.Add ( filtre );
		}

		////////////////////////////////////////////////////////////////////////
		public void RemoveFiltre ( CFiltreSynchronisation filtre )
		{
			m_listefiltresSynchronisation.Remove ( filtre );
		}

		////////////////////////////////////////////////////////////////////////
		public int GetNbFiltres()
		{
			return m_listefiltresSynchronisation.Count;
		}

		////////////////////////////////////////////////////////////////////////
		public void ClearFiltre()
		{
			m_listefiltresSynchronisation.Clear();
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltresDynamiquesForTables CalculeFiltresForTables()
		{
			CFiltresDynamiquesForTables filtres = new CFiltresDynamiquesForTables();

			//Crée un filtre pour chaque les tables fullsnchro
			foreach ( string strNomTable in CContexteDonnee.MappeurTableToClass.GetListeTables() )
			{
				Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
				if ( tp != null && tp.GetCustomAttributes(typeof(FullTableSyncAttribute), true).Length > 0 )
				{
					CFiltreSynchronisation filtreTableFull = new CFiltreSynchronisation ( strNomTable );
					filtreTableFull.TouteLaTable = true;
					filtreTableFull.IsAutoAdd = true;
					filtreTableFull.CreateFiltreForAllParents();
					filtreTableFull.CreateFiltreForAllCompositions();
					filtreTableFull.FiltreDynamique = CFiltreSynchronisation.GetFiltreDynamiqueSynchro ( tp );
					filtres.AddFiltreSynchronisation ( filtreTableFull );
				}
			}

			

			
			foreach ( CFiltreSynchronisation filtre in m_listefiltresSynchronisation )
			{
				filtre.CalculeFiltresForTables();
				filtres.AddFiltreSynchronisation ( filtre );
			}
			return filtres;
		}

		////////////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		////////////////////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			result = serializer.TraiteArrayListOf2iSerializable ( m_listefiltresSynchronisation );
			if ( !result )
				return result;

			return result;
		}

		////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne les filtres finaux pour toutes les tables
		//Le data du result contient les filtresdata finaux
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <param name="nIdUtilisateur">Id de l'utilisateur</param>
		/// <param name="listeUsersGroupe">Id des utilisateurs associés à celui ci</param>
		/// <returns></returns>
		public CResultAErreur GetFiltresFinaux ( int nIdSession, CGroupeUtilisateursSynchronisation groupe )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				using ( CContexteDonnee contexte = new CContexteDonnee ( nIdSession, true, false ) )
				{
					CFiltresDynamiquesForTables filtresForTables = CalculeFiltresForTables();
					CFiltresSynchronisation filtres = new CFiltresSynchronisation();
					foreach ( string strNomTable in CContexteDonnee.MappeurTableToClass.GetListeTables() )
					{
						bool bShouldImporte = false;
						CFiltreDynamique filtreDynamique = filtresForTables.GetFiltreDynamiqueForTable ( strNomTable, ref bShouldImporte );
						if ( !bShouldImporte )
							filtres.AddFiltreForTable ( strNomTable, new CFiltreDataImpossible(), true );
						else
						{
							if ( filtreDynamique == null )
								filtres.AddFiltreForTable ( strNomTable, null , true);
							else
							{
								//Trouve la variable contenant le groupe
								CVariableDynamique variable = null;
								foreach ( CVariableDynamique v in filtreDynamique.ListeVariables )
								{
									if ( v.Nom == CFiltreSynchronisation.c_nomVariableListeUtilisateurs )
										variable = v;
								}
								filtreDynamique.SetValeurChamp ( variable, groupe.ListeIdsUtilisateurs );
								filtreDynamique.ContexteDonnee = contexte;
								CResultAErreur resultTmp = filtreDynamique.GetFiltreData();
								CFiltreData filtreData = null;
								if ( !result )
								{
									result.Erreur.EmpileErreurs ( resultTmp.Erreur );
									result.EmpileErreur(I.T("Error in table @1|189",strNomTable));
								}
								else
								{
									filtreData = (CFiltreData)resultTmp.Data;
									filtres.AddFiltreForTable ( strNomTable, filtreData, true );
								}
							}
						}
					}
					result.Data = filtres;
				
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}
	}
}