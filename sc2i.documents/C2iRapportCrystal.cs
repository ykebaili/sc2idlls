using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



#if !PDA_DATA

namespace sc2i.documents
{

    //public enum ExportFormatType
    //{
    //    NoFormat = 0,
    //    CrystalReport = 1,
    //    RichText = 2,
    //    WordForWindows = 3,
    //    Excel = 4,
    //    PortableDocFormat = 5,
    //    HTML32 = 6,
    //    HTML40 = 7,
    //    ExcelRecord = 8,
    //    Text = 9,
    //    CharacterSeparatedValues = 10,
    //    TabSeperatedText = 11,
    //    EditableRTF = 12,
    //}

	public enum TypeFormatExportCrystal
	{
		CrystalReport = 0,
        PDF = 1, 
        Excel = 2,
        ExcelDataOnly = 3,
        Word = 4,
        EditableRTF = 5,
		RichText = 6,
        Text = 7,
        CharacterSeparatedValues = 8,
        TabSeperatedText = 9,
		NoFormat = 100
	}
	/// <summary>
	/// Un rapport Crystal permet de définir l'environnement nécessaire pour générer un rapport<br/>
    /// avec l'outil 'Crystal Report'. TIMOS intègre les éléments logiciels nécessaires à l'élaboration<br/>
    /// du modèle de rapport 'Crystal Report' et à son exécution.
	/// </summary>
	[Table(C2iRapportCrystal.c_nomTable, C2iRapportCrystal.c_champId, true)]
	[ObjetServeurURI("C2iRapportCrystalServeur")]
	[DynamicClass("Crystal report")]
	[FullTableSync]
	public class C2iRapportCrystal : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "CRYSTAL_REPORT";

		public const string c_champId = "CRYSRPT_ID";
		public const string c_champLibelle = "CRYSRPT_LABEL";
		public const string c_champTypeObjet = "CRYSRPT_OBJECT_TYPE";
		public const string c_champCodeEtat = "CRYSRPT_STATE_CODE";
		public const string c_champNumVersion = "CRYSRPT_VERSION_NUM";

		public const string c_champMultiStructure = "CRYSRPT_MULTI_STRUCTURE";

		//-------------------------------------------------------------------
		public C2iRapportCrystal( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		//-------------------------------------------------------------------
		public C2iRapportCrystal ( DataRow row )
			:base ( row) 
		{
		}

		//-------------------------------------------------------------------
		public override string DescriptionElement 
		{
			get
			{
				return I.T("The Crystal Report @1|102",Libelle);
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public override CFiltreData FiltreStandard
		{
			get
			{
				CFiltreData filtre = new CFiltreData(CDocumentGED.c_champId+">=0");
				return filtre;
			}
		}

		//-------------------------------------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le code système affecté par TIMOS au rapport
        /// </summary>
		[TableFieldPropertyAttribute(c_champCodeEtat)]
		[DynamicField("State code")]
		public int CodeEtat
		{
			get
			{
				return (int)Row[c_champCodeEtat];
			}
			set
			{
				Row[c_champCodeEtat] = value;
			}
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le libellé du rapport
        /// </summary>
		[TableFieldPropertyAttribute(c_champLibelle, 255)]
		[DynamicField("Label")]
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

		//-------------------------------------------------------------------
        /// <summary>
        /// N° de version du rapport; à chaque fois que le rapport est modifié,<br/>
        /// son numéro de version est incrémenté. Cette numérotation commence à 0.
        /// </summary>
		[TableFieldPropertyAttribute(c_champNumVersion)]
		[DynamicField("Version number")]
		public int NumVersion
		{
			get
			{
				return (int)Row[c_champNumVersion];
			}
			set
			{
				Row[c_champNumVersion] = value;
			}
		}

		/*//-------------------------------------------------------------------
		/// <summary>
		/// Données de la structure d'export
		/// </summary>
		[TableFieldProperty(c_champDataStructure,NullAutorise=true)]
		public CDonneeBinaireInRow Data
		{
			get
			{
				if ( Row[c_champDataStructure] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champDataStructure);
					ContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataStructure, donnee);
				}
				return (CDonneeBinaireInRow)Row[c_champDataStructure];
			}
			set
			{
				Row[c_champDataStructure] = value;
			}
		}

		//-------------------------------------------------------------------
		public C2iStructureExport StructureExport
		{
			get
			{
				C2iStructureExport structure  = null;
				if ( Data.Donnees != null )
				{
					structure = new C2iStructureExport (  );
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
					if ( !structure.Serialize(serializer))
						structure = null;
				}
				return structure;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = Data;
					data.Donnees = null;
					Data = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire ( writer );
					CResultAErreur result = value.Serialize( serializer );
					if ( result )
					{
						Data.Donnees = stream.GetBuffer();

						if ( value !=  null && value.TypeSource !=  null )
							Row[c_champTypeObjet]  =  value.TypeSource.ToString();
						else
							Row[c_champTypeObjet] = "";
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
					}
				}
			}
		}*/

		//-------------------------------------------------------------------
        /// <summary>
        /// Retourne le type d'objet principal de la structure d'export associée (exemple : Equipment, Ticket,...)<br/>
        /// Lorsque le rapport fait appel à plusieurs structures d'export, renvoie une chaîne vide.
        /// </summary>
		[DynamicField("Object type")]
		public string NomTypeObjet
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial ( TypeObjet );
			}
		}

		//-------------------------------------------------------------------
		public Type TypeObjet
		{
			get
			{
				Type tp = null;
				try
				{
					tp = CActivatorSurChaine.GetType ( StringTypeObjet );
				}
				catch
				{
					tp = null;
				}
				return tp;
			}
			set
			{
				if ( value != null )
					Row[c_champTypeObjet] = value.ToString();
				else
					Row[c_champTypeObjet] = "";
			}
		}

		//-------------------------------------------------------------------
		[TableFieldProperty(c_champTypeObjet, 255)]
		public string StringTypeObjet
		{
			get
			{
				return (string)Row[c_champTypeObjet];
			}
		}
		//-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit la catégorie du rapport, lorsque le rapport<br/>
        /// est classé dans une catégorie
        /// </summary>
		[Relation(C2iCategorieRapportCrystal.c_nomTable,C2iCategorieRapportCrystal.c_champId,C2iCategorieRapportCrystal.c_champId,false,false)]
		[DynamicField("Report category")]
		public C2iCategorieRapportCrystal CategorieRapport
		{
			get
			{
				return (C2iCategorieRapportCrystal)GetParent ( C2iCategorieRapportCrystal.c_champId, typeof(C2iCategorieRapportCrystal));
			}
			set
			{
				SetParent ( C2iCategorieRapportCrystal.c_champId, value );
			}
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le document GED contenant le modèle de rapport<br/>
        /// Crystal report (c'est ce document qui contient le lien vers le<br/>
        /// fichier crystal d'extension .rpt)
        /// </summary>
		[Relation(
            CDocumentGED.c_nomTable,
            CDocumentGED.c_champId,
            CDocumentGED.c_champId,
            false,
            true)]
		[
		DynamicField("EDM Document")
		]
		[NonCloneable]
		public CDocumentGED DocumentGED
		{
			get
			{
				return (CDocumentGED)GetParent ( CDocumentGED.c_champId, typeof(CDocumentGED));
			}
			set
			{
				if ( value != null )
					value.IsFichierSysteme = true;
				SetParent ( CDocumentGED.c_champId, value );
			}
		}

		/*//-------------------------------------------------------------------
		[Relation(CFiltreDynamiqueInDb.c_nomTable,CFiltreDynamiqueInDb.c_champId,CFiltreDynamiqueInDb.c_champId,false,false)]
		[
		DynamicField("Filtre dynamique")
		]
		public CFiltreDynamiqueInDb FiltreDynamiqueInDb
		{
			get
			{
				return (CFiltreDynamiqueInDb)GetParent ( CFiltreDynamiqueInDb.c_champId, typeof(CFiltreDynamiqueInDb));
			}
			set
			{
				SetParent ( CFiltreDynamiqueInDb.c_champId, value );
				if ( value != null )
					FiltreDynamique = null;
			}
		}

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champDataFiltre,NullAutorise=true)]
		public CDonneeBinaireInRow DataFiltre
		{
			get
			{
				if ( Row[c_champDataFiltre] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champDataFiltre);
					ContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataFiltre, donnee);
				}
				return (CDonneeBinaireInRow)Row[c_champDataFiltre];
			}
			set
			{
				Row[c_champDataFiltre] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public CFiltreDynamique FiltreDynamique
		{
			get
			{
				if ( FiltreDynamiqueInDb != null )
					return FiltreDynamiqueInDb.Filtre;
				CFiltreDynamique retour = null;
				if ( DataFiltre.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(DataFiltre.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					I2iSerializable objet = null;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						retour = (CFiltreDynamique)objet;
					}
					retour.ContexteDonnee = ContexteDonnee;
					retour.ResetValeursVariables();
				}
				return retour;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataFiltre;
					data.Donnees = null;
					DataFiltre = data;
				}
				else
				{
					
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					I2iSerializable objet = value;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						CDonneeBinaireInRow data = DataFiltre;
						data.Donnees = stream.GetBuffer();
						DataFiltre = data;
					}
				}
			}
		}*/

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champMultiStructure,NullAutorise=true)]
		public CDonneeBinaireInRow DataMultiStructure
		{
			get
			{
				if ( Row[c_champMultiStructure] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champMultiStructure);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champMultiStructure, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champMultiStructure]).GetSafeForRow(Row.Row);
			}
			set
			{
				Row[c_champMultiStructure] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CMultiStructureExport MultiStructure
		{
			get
			{
				if ( ModeleDonnees != null )
					return ModeleDonnees.MultiStructure;

				CMultiStructureExport retour = null;
				if ( DataMultiStructure.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(DataMultiStructure.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					I2iSerializable objet = null;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						retour = (CMultiStructureExport)objet;
					}
					retour.ContexteDonnee = ContexteDonnee;
					retour.ResetValeursVariables();

                    reader.Close();
                    stream.Close();
                    stream.Dispose();
                    reader.Dispose();
				}
				return retour;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataMultiStructure;
					data.Donnees = null;
					DataMultiStructure = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					I2iSerializable objet = value;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						CDonneeBinaireInRow data = DataMultiStructure;
						data.Donnees = stream.GetBuffer();
						DataMultiStructure = data;
						TypeObjet = value.TypeDonneesEntree;
						ModeleDonnees = null;
					}

                    writer.Close();
                    stream.Close();
				}
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Donne ou définit la structure d'export à laquelle fait appel<br/>
        /// le rapport, lorsqu'elle existe; en effet, la structure d'export<br/>
        /// peut être soit créée dans le rapport lui-même, soit créée à part<br/>
        /// et le rapport y fait alors référence.
		/// </summary>
		[Relation ( C2iStructureExportInDB.c_nomTable,
			 C2iStructureExportInDB.c_champId,
			 C2iStructureExportInDB.c_champId,
			 false,
			 false,
			 true)]
		[DynamicField("Data model")]
		public C2iStructureExportInDB ModeleDonnees
		{
			get
			{
				return ( C2iStructureExportInDB )GetParent ( C2iStructureExportInDB.c_champId, typeof( C2iStructureExportInDB ) );
			}
			set
			{
				SetParent ( C2iStructureExportInDB.c_champId, value );
				if ( value != null )
				{
					MultiStructure = null;
					this.TypeObjet = value.TypeElements;
				}
			}
		}
		
		/*
		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne le DataSet correspondant au rapport dans le CResultAErreur.Data
		/// </summary>
		public CResultAErreur GetDataFromFiltre(CFiltreDynamique filtre)
		{
			CResultAErreur result = CResultAErreur.True;
			DataSet ds = new DataSet();

			using (CContexteDonnee ctx  = new CContexteDonnee(this.ContexteDonnee.IdSession, true, false) )
			{
				CListeObjetsDonnees liste = new CListeObjetsDonnees( ctx,  StructureExport.TypeSource );
			
				if (filtre != null)
				{
					result = filtre.GetFiltreData();
					if (!result)
						return result;
					liste.Filtre = (CFiltreData) result.Data;
				}

				result = StructureExport.Export( ctx.IdSession, liste, ref ds );
				if (!result)
				{
					result.EmpileErreur("Erreur lors de l'exportation de la structure");
					result.Data = null;
					return result;
				}
			}
			CompleteDataSetDonnees ( ds, filtre );

			result.Data = ds;

			return result;
		}*/

		/*
		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne le DataSet correspondant au rapport dans le CResultAErreur.Data
		/// </summary>
		public CResultAErreur GetDataFromRequete(C2iRequete requete)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( requete == null )
			{
				result.EmpileErreur("Requete nulle");
				return result;
			}
			result = requete.ExecuteRequete ( ContexteDonnee.IdSession );
			if ( !result )
				return result;
			DataSet ds = null;
			if ( result.Data is DataTable )
			{
				DataTable table = (DataTable)result.Data;
				if ( table.DataSet != null )
					ds = table.DataSet;
				else
				{
					ds = new DataSet();
					ds.Tables.Add ( table );
				}
			}
			if ( ds == null )
			{
				result.EmpileErreur("Erreur dans la requête : pas de données");
				return result;
			}

			CompleteDataSetDonnees ( ds, requete );

			result.Data = ds;

			return result;
		}*/

		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne le DataSet correspondant au rapport dans le CResultAErreur.Data
		/// </summary>
		/// <remarks>
		/// listeDonnees contient une liste d'objet à utiliser à la place des données qui seraient filtrées par
		/// chaque definition de la multistructure.
		/// Si null, chaque multistructure crée sa propre liste de données
		/// </remarks>
		public CResultAErreur GetDataFromMultiStructure( 
			CMultiStructureExport mutliStructure, 
			CListeObjetsDonnees listeDonnees, 
			bool bStructureOnly,
			IIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( mutliStructure == null )
			{
				result.EmpileErreur(I.T("The structure is not defined|104"));
				return result;
			}
			result = mutliStructure.GetDataSet(bStructureOnly, listeDonnees, indicateur);
			if ( !result )
				return result;
			DataSet ds = (DataSet)result.Data;
			if ( ds == null )
			{
				result.EmpileErreur(I.T("Error in the structure : no data|103"));
				return result;
			}

			CompleteDataSetDonnees ( ds, mutliStructure);

			result.Data = ds;

			return result;
		}
		
		//-------------------------------------------------------------------
		//Ajoute au dataset les données du rapport
		public void CompleteDataSetDonnees ( DataSet ds, IElementAVariablesDynamiques eltAVariables )
		{
			DataTable firstTable = null;
			if ( ds.Tables.Count > 0 )
				firstTable = ds.Tables[0];
			DataTable tableRapportForEtat = ds.Tables[c_nomTable];
			if ( tableRapportForEtat == null)
			{
				DataTable tableRapports = this.ContexteDonnee.GetTableSafe(C2iRapportCrystal.c_nomTable);
				tableRapportForEtat = CUtilDataSet.AddTableCopie(tableRapports, ds);
			}
			for ( int nCol = tableRapportForEtat.Columns.Count-1; nCol >= 0; nCol -- )
				if ( tableRapportForEtat.Columns[nCol].DataType == typeof(CDonneeBinaireInRow) )
					tableRapportForEtat.Columns.RemoveAt ( nCol );
			DataRow rowToDelete = tableRapportForEtat.Rows.Find(Id);
			if (rowToDelete != null)
				tableRapportForEtat.Rows.Remove(rowToDelete);
			tableRapportForEtat.ImportRow ( Row.Row );
			DataRow row = tableRapportForEtat.Rows[0];
			if ( eltAVariables != null )
			{
				foreach ( CVariableDynamique variable in eltAVariables.ListeVariables )
				{
					try
					{
						string strNom = variable.Nom;
						string strNomAvecIndice = strNom;
						if (tableRapportForEtat.Columns[strNomAvecIndice] == null)
						{
							DataColumn col = new DataColumn(strNomAvecIndice, variable.TypeDonnee.TypeDotNetNatif);
							col.AllowDBNull = true;
							tableRapportForEtat.Columns.Add(col);
						}
						object val = eltAVariables.GetValeurChamp(variable);
						row[strNomAvecIndice] = val == null?DBNull.Value:val;
					}
					catch ( Exception e )
					{
						System.Console.WriteLine ( e.ToString() );
					}
				}
			}
			DataRow rowCrystal = tableRapportForEtat.Rows[0];
		}

		/*//-------------------------------------------------------------------
		/// <summary>
		/// Crée le report crystal et le retourne dans le data du result
		/// </summary>
		/// <param name="donnees"></param>
		/// /// <param name="fichierDonnees">Objet fichier temporaire dans lequel les données sont stockées (mdb)</param>
		/// <returns></returns>
		public CResultAErreur CreateReport ( 
			CListeObjetsDonnees donnees, 
			IElementAVariablesDynamiques eltAVariables,
			CFichierLocalTemporaire fichierDonnees )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				ReportDocument doc = new ReportDocument();
				if(  DocumentGED == null )
				{
					result.EmpileErreur( "Le rapport n'est pas lié à un modèle crystal");
					return result;
				}
				using (CProxyGED proxy = new CProxyGED(ContexteDonnee.IdSession, DocumentGED.ReferenceDoc ) )
				{
					result = proxy.CopieFichierEnLocal();
					if ( !result )
						return result;

					doc.Load(proxy.NomFichierLocal);
					return CreateReport ( donnees, eltAVariables, fichierDonnees, doc );
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			return result;
		}*/

		/*//-------------------------------------------------------------------
		/// <summary>
		/// Crée le report crystal et le retourne dans le data du result
		/// </summary>
		/// <param name="donnees"></param>
		/// /// <param name="fichierDonnees">Objet fichier temporaire dans lequel les données sont stockées (mdb)</param>
		/// <returns></returns>
		public CResultAErreur CreateReport ( C2iRequete requete, CFichierLocalTemporaire fichierDonnees )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( result )
				{
					result = GetDataFromRequete ( requete );
					if ( !result)
						return result;
					DataSet ds = (DataSet)result.Data;
					//CompleteDataSetDonnees ( ds, requete );
					
					ReportDocument doc = new ReportDocument();
					if(  DocumentGED == null )
					{
						result.EmpileErreur( "Le rapport n'est pas lié à un modèle crystal");
						return result;
					}
					using (CProxyGED proxy = new CProxyGED(ContexteDonnee.IdSession, DocumentGED.ReferenceDoc ) )
					{
						result = proxy.CopieFichierEnLocal();
						if ( !result )
							return result;

						doc.Load(proxy.NomFichierLocal);
					}
					return CreateReport ( ds, fichierDonnees, doc );
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur ( "Erreur lors de la génération du rapport "+Libelle );
			}
			return result;
		}*/

		//-------------------------------------------------------------------
		/// <summary>
		/// Crée le report crystal et le retourne dans le data du result
		/// Ne fonctionne que si la les données d'une liste sont suffisantes à la création du rapport. Il ne 
		/// faut pas que le formulaire du rapport nécéssite des données indispensables au rapport.
		/// </summary>
		/// <param name="donnees"></param>
		/// /// <param name="fichierDonnees">Objet fichier temporaire dans lequel les données sont stockées (mdb)</param>
		/// <returns></returns>
		public CResultAErreur CreateReport ( CListeObjetsDonnees listeDonnees, CFichierLocalTemporaire fichierDonnees )
		{
			return CreateReport ( listeDonnees, fichierDonnees, null );
		}

		public CResultAErreur CreateReport ( CListeObjetsDonnees listeDonnees, CFichierLocalTemporaire fichierDonnees, IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( result )
				{
					result = GetDataFromMultiStructure ( MultiStructure, listeDonnees, false, indicateur );
					if ( !result)
						return result;
					DataSet ds = (DataSet)result.Data;
					
					ReportDocument doc = new ReportDocument();
					if(  DocumentGED == null )
					{
						result.EmpileErreur(I.T("The report is not associated with a Crystal Model|105"));
						return result;
					}
					using (CProxyGED proxy = new CProxyGED(ContexteDonnee.IdSession, DocumentGED.ReferenceDoc ) )
					{
						result = proxy.CopieFichierEnLocal();
						if ( !result )
							return result;

						doc.Load(proxy.NomFichierLocal);
					}
					return CreateReport ( ds, fichierDonnees, doc );
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur (I.T("Error while generation of report @1|106",Libelle));
			}
			return result;
		}
		

		public CResultAErreur CreateReport ( CMultiStructureExport structure, CFichierLocalTemporaire fichierDonnees )
		{
			return CreateReport ( structure, fichierDonnees, null );
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Crée le report crystal et le retourne dans le data du result
		/// </summary>
		/// <param name="donnees"></param>
		/// /// <param name="fichierDonnees">Objet fichier temporaire dans lequel les données sont stockées (mdb)</param>
		/// <returns></returns>
		public CResultAErreur CreateReport ( CMultiStructureExport structure, CFichierLocalTemporaire fichierDonnees, IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( result )
				{
					result = GetDataFromMultiStructure ( structure, null, false, indicateur );
					if ( !result)
						return result;
					DataSet ds = (DataSet)result.Data;
					
					ReportDocument doc = new ReportDocument();
					if(  DocumentGED == null )
					{
						result.EmpileErreur(I.T("The report is not associated with a Crystal Model|105"));
						return result;
					}
					using (CProxyGED proxy = new CProxyGED(ContexteDonnee.IdSession, DocumentGED.ReferenceDoc ) )
					{
						result = proxy.CopieFichierEnLocal();
						if ( !result )
							return result;

						doc.Load(proxy.NomFichierLocal);
					}
					return CreateReport ( ds, fichierDonnees, doc );
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while generation of report @1|106", Libelle));
			}
			return result;
		}
				
		/*//-------------------------------------------------------------------
		/// <summary>
		/// Crée le report crystal et le retourne dans le data du result
		/// </summary>
		/// <param name="donnees"></param>
		/// /// <param name="fichierDonnees">Objet fichier temporaire dans lequel les données sont stockées (mdb)</param>
		/// <returns></returns>
		public CResultAErreur CreateReport ( CListeObjetsDonnees donnees, IElementAVariablesDynamiques eltAVariables, CFichierLocalTemporaire fichierDonnees, ReportDocument report )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( result )
				{
					DataSet ds =  new DataSet();
					result = StructureExport.Export(donnees.ContexteDonnee.IdSession, donnees, ref ds);
					if ( !result )
						return result;
					CompleteDataSetDonnees ( ds, eltAVariables );
					return CreateReport ( ds, fichierDonnees, report );
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur ( "Erreur lors de la génération du rapport "+Libelle );
			}
			return result;
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// LE data du result contient un CFichierLocalTemporaire contenant
		/// le fichier exporté.
		/// </summary>
		/// <param name="donnees"></param>
		/// <returns></returns>
		public CResultAErreur CreateFichierExport ( CListeObjetsDonnees donnees, IElementAVariablesDynamiques eltAVariables, TypeFormatExportCrystal format )
		{
			using (CFichierLocalTemporaire ficData = new CFichierLocalTemporaire ("mdb"))
			{
				CResultAErreur result = CreateReport ( donnees, eltAVariables, ficData );
				if ( !result )
					return result;
				ReportDocument report = (ReportDocument)result.Data;
				try
				{
					CFichierLocalTemporaire fichierExport = new CFichierLocalTemporaire(GetExtensionForFormat ( format ));
					fichierExport.CreateNewFichier();
					report.ExportToDisk ( GetFormatCrystal(format), fichierExport.NomFichier );
					report.Close();
					result.Data = fichierExport;
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e  ) );
					result.EmpileErreur("Erreur lors de la création du fichier "+CUtilSurEnum.GetNomConvivial( format.ToString()));
				}
				return result;
			}
		}*/

		/*//-------------------------------------------------------------------
		/// <summary>
		/// LE data du result contient un CFichierLocalTemporaire contenant
		/// le fichier exporté.
		/// </summary>
		/// <param name="donnees"></param>
		/// <returns></returns>
		public CResultAErreur CreateFichierExport ( C2iRequete requete, TypeFormatExportCrystal format )
		{
			using (CFichierLocalTemporaire ficData = new CFichierLocalTemporaire ("mdb"))
			{
				CResultAErreur result = CreateReport ( requete, ficData );
				if ( !result )
					return result;
				ReportDocument report = (ReportDocument)result.Data;
				try
				{
					CFichierLocalTemporaire fichierExport = new CFichierLocalTemporaire(GetExtensionForFormat ( format ));
					fichierExport.CreateNewFichier();
					report.ExportToDisk ( GetFormatCrystal(format), fichierExport.NomFichier );
					report.Close();
					result.Data = fichierExport;
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e  ) );
					result.EmpileErreur("Erreur lors de la création du fichier "+CUtilSurEnum.GetNomConvivial( format.ToString()));
				}
				return result;
			}
		}*/

		//-------------------------------------------------------------------
		/// <summary>
		/// LE data du result contient un CFichierLocalTemporaire contenant
		/// le fichier exporté.
		/// </summary>
		/// <param name="donnees"></param>
		/// <returns></returns>
		public CResultAErreur CreateFichierExport ( CMultiStructureExport multiStructure, TypeFormatExportCrystal format )
		{
			ReportDocument report = null;
			CResultAErreur result = CreateFichierExport ( multiStructure, format, ref report );
			if ( report != null )
				report.Close();
			return result;
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// LE data du result contient un CFichierLocalTemporaire contenant
		/// le fichier exporté.
		/// </summary>
		/// <param name="donnees"></param>
		/// <returns></returns>
		public CResultAErreur CreateFichierExport ( CMultiStructureExport multiStructure, TypeFormatExportCrystal format, ref ReportDocument report )
		{
			using (CFichierLocalTemporaire ficData = new CFichierLocalTemporaire ("mdb"))
			{
				CResultAErreur result = CreateReport ( multiStructure, ficData );
				if ( !result )
					return result;
				report = (ReportDocument)result.Data;
				if ( format != TypeFormatExportCrystal.NoFormat )
				{
					try
					{
						CFichierLocalTemporaire fichierExport = new CFichierLocalTemporaire(GetExtensionForFormat ( format ));
						fichierExport.CreateNewFichier();
						report.ExportToDisk ( GetFormatCrystal(format), fichierExport.NomFichier );
						result.Data = fichierExport;
					}
					catch ( Exception e )
					{
						result.EmpileErreur ( new CErreurException ( e  ) );
						result.EmpileErreur(I.T("Error while creating @1 file|108",CUtilSurEnum.GetNomConvivial( format.ToString())));
					}
					return result;
				}
				return result;
			}
		}

		/*//-------------------------------------------------------------------
		/// <summary>
		/// Données de la structure d'export
		/// </summary>
		[TableFieldProperty(c_champDataRequete,NullAutorise=true)]
		public CDonneeBinaireInRow DataRequete
		{
			get
			{
				if ( Row[c_champDataRequete] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champDataRequete);
					ContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataRequete, donnee);
				}
				return (CDonneeBinaireInRow)Row[c_champDataRequete];
			}
			set
			{
				Row[c_champDataRequete] = value;
			}
		}

		//-------------------------------------------------------------------
		public C2iRequete Requete
		{
			get
			{
				C2iRequete requete = null;
				if ( DataRequete.Donnees != null )
				{
					requete = new C2iRequete ( ContexteDonnee );
					MemoryStream stream = new MemoryStream(DataRequete.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
					if ( !requete.Serialize(serializer))
						requete = null;
				}
				return requete;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataRequete;
					data.Donnees = null;
					DataRequete = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire ( writer );
					CResultAErreur result = value.Serialize( serializer );
					if ( result )
					{
						DataRequete.Donnees = stream.GetBuffer();
						CDonneeBinaireInRow data = DataRequete;
						data.Donnees = stream.GetBuffer();
						DataRequete = data;
					}
				}
			}
		}*/

		/// ///////////////////////////////////////////
		public static CResultAErreur GenereReport ( 
			CDocumentGED modeleEtat,
			C2iStructureExport structureExport,
			CListeObjetsDonnees listeSource,
			ref CFichierLocalTemporaire fichierDeDonnees )
		{
			CResultAErreur result = CResultAErreur.True;
			//Récupère le modèle de facture
			ReportDocument report = new ReportDocument();
			using ( CProxyGED proxy = new CProxyGED( modeleEtat.ContexteDonnee.IdSession, modeleEtat.ReferenceDoc ))
			{
				result = proxy.CopieFichierEnLocal();
				if ( !result )
					return result;

				report.Load ( proxy.NomFichierLocal );

				structureExport.TraiterSurServeur = false;

				DataSet ds = null;

				if ( listeSource.Count > 0 )
				{
					ds = new DataSet();
					result = structureExport.Export ( modeleEtat.ContexteDonnee.IdSession, listeSource, ref ds, null );

					if (!result)
						return result;
					if ( fichierDeDonnees != null )
						fichierDeDonnees.Dispose();
					fichierDeDonnees = new CFichierLocalTemporaire ("mdb");
					result = C2iRapportCrystal.CreateReport ( ds, fichierDeDonnees, report );
					if ( !result )
						return result;
					result.Data = report;
					return result;
				}
			}
			return result;
		}

		/// ///////////////////////////////////////////
		public static CResultAErreur CreateReport ( 
			DataSet donnees, 
			CFichierLocalTemporaire fichierDonnees,
			ReportDocument report )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( result )
				{
					fichierDonnees.CreateNewFichier();
					result = new CExporteurDatasetAccess ( ).Export ( donnees, new CDestinationExportFile ( fichierDonnees.NomFichier ) );
					if ( result )
					{
						foreach ( Table tbl in report.Database.Tables )
						{
							TableLogOnInfo logOnInfo = new TableLogOnInfo();
							logOnInfo = tbl.LogOnInfo;
							ConnectionInfo connectionInfo = new ConnectionInfo ();
							connectionInfo = logOnInfo.ConnectionInfo;

							// Définir les paramètres Connection.
							connectionInfo.ServerName = fichierDonnees.NomFichier;
							logOnInfo.ConnectionInfo = connectionInfo;
							tbl.ApplyLogOnInfo ( logOnInfo );
						}
						report.Refresh();
						result.Data = report;
						return result;
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error while report generation|106"));
			}
			return result;
		}

		protected static ExportFormatType GetFormatCrystal ( TypeFormatExportCrystal format )
		{
            switch (format)
            {
                case TypeFormatExportCrystal.CrystalReport:
                    return ExportFormatType.CrystalReport;
                case TypeFormatExportCrystal.PDF:
                    return ExportFormatType.PortableDocFormat;
                case TypeFormatExportCrystal.Excel:
                    return ExportFormatType.Excel;
                case TypeFormatExportCrystal.ExcelDataOnly:
                    return ExportFormatType.ExcelRecord;
                case TypeFormatExportCrystal.Word:
                    return ExportFormatType.WordForWindows;
                case TypeFormatExportCrystal.EditableRTF:
                    return ExportFormatType.EditableRTF;
                case TypeFormatExportCrystal.RichText:
                    return ExportFormatType.RichText;
                case TypeFormatExportCrystal.Text:
                    return ExportFormatType.Text;
                case TypeFormatExportCrystal.CharacterSeparatedValues:
                    return ExportFormatType.CharacterSeparatedValues;
                case TypeFormatExportCrystal.TabSeperatedText:
                    return ExportFormatType.TabSeperatedText;
                case TypeFormatExportCrystal.NoFormat:
                    return ExportFormatType.NoFormat;
                default:
                    break;
            }


			return ExportFormatType.PortableDocFormat;
		}

		protected static string GetExtensionForFormat ( TypeFormatExportCrystal format )
		{
            switch (format)
            {
                case TypeFormatExportCrystal.CrystalReport:
                    return "rpt";
                case TypeFormatExportCrystal.PDF:
                    return "pdf";
                case TypeFormatExportCrystal.Excel:
                    return "xls";
                case TypeFormatExportCrystal.ExcelDataOnly:
                    return "xls";
                case TypeFormatExportCrystal.Word:
                    return "doc";
                case TypeFormatExportCrystal.EditableRTF:
                    return "rtf";
                case TypeFormatExportCrystal.RichText:
                    return "rtf";
                case TypeFormatExportCrystal.Text:
                    return "txt";
                case TypeFormatExportCrystal.CharacterSeparatedValues:
                    return "csv";
                case TypeFormatExportCrystal.TabSeperatedText:
                    return "csv";
                case TypeFormatExportCrystal.NoFormat:
                    return "";
                default:
                    break;
            }

			return "rpt";
		}

		
	}
}
#endif