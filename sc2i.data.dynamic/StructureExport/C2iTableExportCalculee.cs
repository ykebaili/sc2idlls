using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iTableExportCalculee.
	/// </summary>
	[Serializable]
	public class C2iTableExportCalculee : I2iSerializable, ITableExport
	{
		public const string c_champNumRec = "CALC_VALUE";
		private string m_strNomTable = "";
		private C2iExpression m_expressionNbRecords = new C2iExpressionConstante(1);
		private C2iExpression m_expressionValeur = null;
		private bool m_bNePasCalculer = false;




		/// /////////////////////////////////////////////////////////
		public C2iTableExportCalculee()
		{
		}

		/// /////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
			set
			{
				m_strNomTable = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool NePasCalculer
		{
			get
			{
				return m_bNePasCalculer;
			}
			set
			{
				m_bNePasCalculer = value;
			}
		}

        public Type TypeSource
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/*/// /////////////////////////////////////////////////////////
		public int NbRecordsToCreate
		{
			get
			{
				return m_nNbRecordsToCreate;
			}
			set
			{
				m_nNbRecordsToCreate = value;
			}
		}*/

		//-------------------------------------------------------------
		public bool IsOptimisable(ITableExport table, Type typeDeMesElements)
		{
			return false;
		}

		/// /////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
			//Nb records->Expression
			//3 : Ajout de ne pas calculer
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomTable );
			if ( nVersion < 2)
			{
				int nNbRecords = 0;
				serializer.TraiteInt ( ref nNbRecords );
				m_expressionNbRecords = new C2iExpressionConstante(nNbRecords);
			}
			else
			{
				I2iSerializable obj = m_expressionNbRecords;
				result = serializer.TraiteObject ( ref obj );
				m_expressionNbRecords = (C2iExpression)obj;
			}

			if ( nVersion >= 1 )
			{
				I2iSerializable obj = m_expressionValeur;
				result = serializer.TraiteObject ( ref obj );
				m_expressionValeur = (C2iExpression)obj;
			}

			if (nVersion >= 3)
				serializer.TraiteBool(ref m_bNePasCalculer);

			return result;
		}

		/// /////////////////////////////////////////////////////////
		public IChampDeTable[] Champs
		{
			get
			{
				return new IChampDeTable[0];
			}
		}

		/// /////////////////////////////////////////////////////////
		public ITableExport[] TablesFilles
		{
			get
			{
				return new ITableExport[0];
			}
		}

		

		/// /////////////////////////////////////////////////////////
		public void AddTableFille(ITableExport table)
		{
		}

		/// /////////////////////////////////////////////////////////
		public void RemoveTableFille(ITableExport table)
		{
		}


		/// /////////////////////////////////////////////////////////
		public DataTable GetDataTable(IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesPourFiltre)
		{
			DataTable table = new DataTable ( m_strNomTable );
			Type tp = typeof(int);
			if ( m_expressionValeur != null )
				tp = m_expressionValeur.TypeDonnee.TypeDotNetNatif;
			DataColumn col = table.Columns.Add ( c_champNumRec, tp);
			table.PrimaryKey = new DataColumn[]{col};
			CTypeForFormule dummy = new CTypeForFormule(elementAVariablesPourFiltre);
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( dummy );

			int nNbRecords = 0;
			if ( m_expressionNbRecords != null )
			{
				CResultAErreur result = m_expressionNbRecords.Eval(ctx);
				if ( !result )
					return table;
				
				try
				{
					nNbRecords = Convert.ToInt32(result.Data);
				}
				catch
				{
					nNbRecords = 0;
				}
			}

			for ( int n = 0; n < nNbRecords; n++ )
			{
				DataRow row = table.NewRow();
				if ( m_expressionValeur != null )
				{
					dummy.Valeur = n;
                    ctx.CacheEnabled = false;
                    if ( ctx.Cache != null )
                        ctx.Cache.ResetCache();
					CResultAErreur result = m_expressionValeur.Eval ( ctx );
					if ( result )
						row[c_champNumRec] = result.Data;
				}
				else
					row[c_champNumRec] = n;
				table.Rows.Add ( row );
			}
			return table;
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleNbRecords
		{
			get
			{
				return m_expressionNbRecords;
			}
			set
			{
				m_expressionNbRecords = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleValeur
		{
			get
			{
				return m_expressionValeur;
			}
			set
			{
				m_expressionValeur = value;
			}
		}



		public class CTypeForFormule : CElementAVariablesDynamiques
		{
			private int m_nValeur = 0;

			internal static string c_idChampValeur = "##TBL_NUM_VAL";

			public CTypeForFormule ( IElementAVariablesDynamiquesAvecContexteDonnee eltSource )
			{
				CopieStatique (eltSource);
			}

			public int Valeur
			{
				get
				{
					return m_nValeur;
				}
				set
				{
					m_nValeur = value;
				}
			}

			public override CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
			{
				ArrayList lst = new ArrayList ( base.GetDefinitionsChamps (tp, nNbNiveaux, defParente) );
				lst.Add(new CDefinitionProprieteDynamiqueForTypeForFormule());
				return (CDefinitionProprieteDynamique[])lst.ToArray(typeof(CDefinitionProprieteDynamique));
			}
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur InsertDataInDataSet(
			IEnumerable list, 
			DataSet ds, 
			ITableExport tableParente,
			int nValeurCle,
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ChampOrigine
		{
			get
			{
				return null;
			}
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur GetFiltreDataAAppliquer( IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables )
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CFiltreDynamique FiltreAAppliquer
		{
			get
			{
				return null;
			}			
			set
			{
			}
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur EndInsertData( DataSet ds )
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur InsertColonnesInTable ( DataTable table )
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur InsertDataInDataSet(
			IEnumerable list, 
			DataSet ds, 
			ITableExport tableParente,
			int[] nValeursCle,
			RelationAttribute relationToObjetParent,
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Not supported function calling : C2iTableExportCalculée.InsertDataInDataset|124"));
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public void AddProprietesOrigineDesChampsToTable ( Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee )
		{
		}

		/// /////////////////////////////////////////////////////////
		public ITableExport GetTableFille(string strNomTable)
		{
			return null;
		}

		/// /////////////////////////////////////////////////////////
		public ITableExport[] GetToutesLesTablesFilles()
		{
			return new ITableExport[0];
		}

		/// /////////////////////////////////////////////////////////
		public bool AcceptChilds()
		{
			return false;//Pas de table fille pour les tables calculées
		}

		[AutoExec("Autoexec")]
		[Serializable]
		public class CDefinitionProprieteDynamiqueForTypeForFormule : CDefinitionProprieteDynamique
		{
			private static string c_strCleType = "TTF";
			
			public CDefinitionProprieteDynamiqueForTypeForFormule()
				: base(
				"value",
				CTypeForFormule.c_idChampValeur,
				new CTypeResultatExpression(typeof(int), false),
				false,
				true
				)
			{
			}


			public override string  CleType
			{
				get 
				{ 
					return c_strCleType;
				}
			}

			public static new void Autoexec()
			{
				CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueForTypeForFormule));
			}
		}

		public class CInterpreteurProprieteDynamiqueForTypeForFormule : IInterpreteurProprieteDynamique
        {
			//------------------------------------------------------------
			public bool ShouldIgnoreForSetValue(string strPropriete)
			{
				return false;
			}

			//------------------------------------------------------------
			public CResultAErreur GetValue(object objet, string strPropriete)
			{
				CResultAErreur result = CResultAErreur.True;
				CTypeForFormule typeForFormule = objet as CTypeForFormule;
				if (typeForFormule != null)
					result.Data = typeForFormule.Valeur;
				return result;
			}

			public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
			{
				CResultAErreur result = CResultAErreur.True;
				result.EmpileErreur(I.T("Forbidden affectation|20034"));
				return result;
			}

            public class COptimiseurProprieteDynamiqueTypeForFormule : IOptimiseurGetValueDynamic
            {
                public object GetValue(object objet)
                {
                    CTypeForFormule typeForFormule = objet as CTypeForFormule;
				    if (typeForFormule != null)
					    return typeForFormule.Valeur;
                    return null;
                }

                public Type GetTypeRetourne()
                {
                    return typeof(int);
                }
                

            }

            public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
            {
                return new COptimiseurProprieteDynamiqueTypeForFormule();
            }

        }
	}

	

	
}
