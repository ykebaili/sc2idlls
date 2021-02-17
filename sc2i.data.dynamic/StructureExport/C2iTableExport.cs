using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Reflection;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	
	/// <summary>
	/// Description résumée de C2iTableExport.
	/// </summary>
	[Serializable]
	public class C2iTableExport : C2iTableExportATableFille, ITableExport
	{
		private CDefinitionProprieteDynamique m_champOrigine;

		private List<C2iChampExport> m_listeChamps = new List<C2iChampExport>();

		private bool m_bIsTableSimple = false;

		/// <summary>
		/// Filtre à appliquer aux elements pour qu'ils entrent dans la table
		/// </summary>
		private CFiltreDynamique m_filtreAAppliquer = null;

		/// <summary>
		/// Pour ne pas recalculer x fois le filtre
		/// </summary>
		private CFiltreData m_filtreDataAAppliquerCache = null;

		
		/// //////////////////////////////////////////////////////////////
		public C2iTableExport()
			:base()
		{

		}

		/// //////////////////////////////////////////////////////////////
		public C2iTableExport( CDefinitionProprieteDynamique champOrigine )
			:base()
		{
			m_champOrigine = champOrigine;
		}

		/// //////////////////////////////////////////////////////////////
		public override CDefinitionProprieteDynamique ChampOrigine
		{
			get
			{
				return m_champOrigine;
			}
			set
			{
				m_champOrigine = value;
                if (value != null)
                    TypeSource = m_champOrigine.TypeDonnee.TypeDotNetNatif;
			}
		}

	
		
		/// //////////////////////////////////////////////////////////////
		///Indique qu'il s'agit d'un export simple ( copie des champs SQL)
		public bool IsTableSimple
		{
			get
			{
				return m_bIsTableSimple;
			}
			set
			{
				m_bIsTableSimple = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public override CResultAErreur InsertColonnesInTable ( DataTable table )
		{
			CResultAErreur result = CResultAErreur.True;
			foreach(IChampDeTable chp in Champs)
			{
				result = CreateChampInTable(chp, table);
				if (!result)
					return result;
			}
			return result;
		}

		//----------------------------------------------------------------------------------
		public override CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;
			if (table.Columns.Contains(champExport.NomChamp))
				return result;
			Type tp = champExport.TypeDonnee;
			if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
				tp = tp.GetGenericArguments()[0];
			DataColumn col = new DataColumn(champExport.NomChamp, tp);
			table.Columns.Add(col);
			return result;
		}


		/// //////////////////////////////////////////////////////////////
		///Retourne la liste de toutes les definition propriete dynamique origines des
		///champs de cette structure
		public override void AddProprietesOrigineDesChampsToTable ( 
            Hashtable tableOrigines, 
            string strChemin, 
            CContexteDonnee contexteDonnee )
		{
            Type typeSource = null;
            if (ChampOrigine != null)
                typeSource = ChampOrigine.TypeDonnee.TypeDotNetNatif;
            else
                typeSource = this.TypeSource;
			foreach ( C2iChampExport champ in Champs )
			{
				champ.AddProprietesOrigineToTable( typeSource, tableOrigines, strChemin, contexteDonnee );
			}
			foreach ( ITableExport table in TablesFilles )
				if ( table.FiltreAAppliquer == null )
					table.AddProprietesOrigineDesChampsToTable ( tableOrigines, strChemin, contexteDonnee );
		}
	
		/// //////////////////////////////////////////////////////////////
		public override IChampDeTable[] Champs
		{
			get
			{
				return m_listeChamps.ToArray();
			}
		}

		/// //////////////////////////////////////////////////////////////
		public void ClearChamps()
		{
			m_listeChamps.Clear();
		}

		/// //////////////////////////////////////////////////////////////
		public void AddChamp(C2iChampExport champ)
		{
			m_listeChamps.Add(champ);
		}

		/// //////////////////////////////////////////////////////////////
		public void RemoveChamp(C2iChampExport champ)
		{
			m_listeChamps.Remove(champ);
		}

		/// //////////////////////////////////////////////////////////////
		public List<IChampDeTable> GetListeChampsFromOrigine(Type typeOrigine)
		{
			List<IChampDeTable> liste = new List<IChampDeTable>();
			foreach(C2iChampExport champ in this.Champs)
			{
				if (champ.Origine.GetType() == typeOrigine)
					liste.Add(champ);
			}
			return liste;
		}

	
		
		/// //////////////////////////////////////////////////////////////
		public override CFiltreDynamique FiltreAAppliquer
		{
			get
			{
				return m_filtreAAppliquer;
			}
			set
			{
				m_filtreAAppliquer = value;
				m_filtreDataAAppliquerCache = null;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public override CResultAErreur GetFiltreDataAAppliquer( IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( FiltreAAppliquer == null )
				return result;
			if ( m_filtreDataAAppliquerCache == null )
			{
				FiltreAAppliquer.ElementAVariablesExterne = elementAVariables;
				result = FiltreAAppliquer.GetFiltreData();
				if ( !result )
					return result;
				m_filtreDataAAppliquerCache = (CFiltreData)result.Data;
			}
			result.Data = m_filtreDataAAppliquerCache;
			return result;
		}



		/// /////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();
			if (!result)
				return result;
			Hashtable tableNoms = new Hashtable();
			foreach(C2iChampExport champ in this.Champs)
			{
				CResultAErreur tempResult = CResultAErreur.True;
				tempResult = champ.VerifieDonnees();
				if (!tempResult)
				{
					result.Erreur.EmpileErreurs(tempResult.Erreur);
					result.SetFalse();
					return result;
				}

				if ( !tableNoms.ContainsKey( champ.NomChamp ))
					tableNoms[champ.NomChamp] = true;
				else if (champ.Origine is C2iOrigineChampExportChamp)
				{
					if (champ.NomChamp == ((C2iOrigineChampExportChamp)champ.Origine).ChampOrigine.Nom)
						champ.NomChamp+= " " + NomTable;
					else
						champ.NomChamp = ((C2iOrigineChampExportChamp)champ.Origine).ChampOrigine.Nom;

					if ( !tableNoms.ContainsKey( champ.NomChamp ))
						tableNoms[champ.NomChamp] = true;
					else
					{
						result.EmpileErreur(I.T("There is several fields having the same name \"@1\" in the table \"@2\"|114", champ.NomChamp, NomTable));
						return result;
					}
				}
				else
				{
					result.EmpileErreur(I.T("There is several fields having the same name \"@1\" in the table \"@2\"|114", champ.NomChamp, NomTable));
					return result;
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 4;
			//1 : Ajout du filtre
			//4 : La classe dérive maintenant de C2iTableExportATableFille
		}

		//----------------------------------------------------------------------------------
		#region SerializeAvantC2iTableExportATableFille
		private CResultAErreur SerializeAvantC2iTableExportATableFille(C2iSerializer serialiser, int nOldVersion)
		{
			CResultAErreur result = CResultAErreur.True;
			if (serialiser.Mode == ModeSerialisation.Ecriture)
			{
				//Pas la peine de traduire, ça n'arrive jamais
				result.EmpileErreur("Cannot write the old version of C2iTableExport");
				return result;
			}
			string strTmp = NomTable;
			serialiser.TraiteString(ref strTmp);
			NomTable = strTmp;

			I2iSerializable obj = ChampOrigine;
			result = serialiser.TraiteObject(ref obj);
			if (!result)
				return result;
			ChampOrigine = (CDefinitionProprieteDynamique)obj;

			serialiser.AttacheObjet(typeof(ITableExport), this);

			ArrayList lstTables = new ArrayList();
			result = serialiser.TraiteArrayListOf2iSerializable(lstTables);
			serialiser.DetacheObjet(typeof(ITableExport), this);
			if (!result)
				return result;
			ClearTablesFilles();
			foreach (ITableExport table in lstTables)
				AddTableFille(table);

			ArrayList lstChamps = new ArrayList();
			result = serialiser.TraiteArrayListOf2iSerializable(lstChamps);
			if (!result)
				return result;
			m_listeChamps.Clear();
			foreach (C2iChampExport champ in lstChamps)
				AddChamp(champ);
			if (nOldVersion >= 1)
			{
				obj = m_filtreAAppliquer;
				result = serialiser.TraiteObject(ref obj);
				m_filtreAAppliquer = (CFiltreDynamique)obj;
			}

			return result;
		}
		#endregion

		//----------------------------------------------------------------------------------
		public override CResultAErreur Serialize ( C2iSerializer serialiser )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serialiser.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if (nVersion < 4)
				return SerializeAvantC2iTableExportATableFille(serialiser, nVersion);

			result = base.Serialize(serialiser);
			if (!result)
				return result;

			I2iSerializable obj = ChampOrigine;
			result = serialiser.TraiteObject ( ref obj );
			if ( !result )
				return result;
			ChampOrigine = (CDefinitionProprieteDynamique)obj;

			result = serialiser.TraiteListe<C2iChampExport>(m_listeChamps);
			if (!result)
				return result;

			obj = m_filtreAAppliquer;
			result = serialiser.TraiteObject ( ref obj );
			m_filtreAAppliquer = (CFiltreDynamique)obj;

			return result;
		}


		//-------------------------------------------------------------------------------
		protected override CResultAErreur InsereValeursChamps(object obj, DataRow row, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			CResultAErreur result = CResultAErreur.True;

			foreach (C2iChampExport chp in Champs)
			{
				object objet = chp.GetValeur(obj, cacheValeurs, restriction);
				if (objet == null)
					objet = DBNull.Value;
				try
				{
					row[chp.NomChamp] = objet;
				}
				catch (Exception e)
				{
					result.EmpileErreur (I.T("@1\r\n Error table @2 field @3, id=@4|121", e.ToString(), row.Table.TableName, chp.NomChamp, row[row.Table.PrimaryKey[0]].ToString()));
				}
			}
			return result;
		}

		

		/// /////////////////////////////////////////////////////////
		public override bool AcceptChilds()
		{
			return true;
		}


	}
}
