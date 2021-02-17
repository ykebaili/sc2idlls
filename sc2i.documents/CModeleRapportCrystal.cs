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
/*
namespace sc2i.documents
{
	
	/// <summary>
	/// Description résumée de CModeleRapportCrystal.
	/// </summary>
	[Table(CModeleRapportCrystal.c_nomTable, CModeleRapportCrystal.c_champId, true)]
	[ObjetServeurURI("CModeleRapportCrystalServeur")]
	[DynamicClass("Crystal report model")]
	[FullTableSync]
	public class CModeleRapportCrystal : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "CRYSTAL_REPORT_TEMPLATE";

		public const string c_champId = "CRYSRPTTPL_ID";
		public const string c_champLibelle = "CRYSRPTTPL_LABEL";
		public const string c_champNumVersion = "CRYSRPTTPL_VERSION_NUM";
		public const string c_champMultiStructure = "CRYSRPTTPL_MULTI_STRUCT";
		public const string c_champTypeObjet = "CRYSRPTTPL_OBJECT_TYPE";

		//-------------------------------------------------------------------
		public CModeleRapportCrystal( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		//-------------------------------------------------------------------
		public CModeleRapportCrystal ( DataRow row )
			:base ( row) 
		{
		}

		//-------------------------------------------------------------------
		public override string DescriptionElement 
		{
			get
			{
				return "Le modèle de rapport "+Libelle;
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


		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champMultiStructure,NullAutorise=true)]
		public CDonneeBinaireInRow DataMultiStructure
		{
			get
			{
				if ( Row[c_champMultiStructure] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champMultiStructure);
					ContexteDonnee.ChangeRowSansDetectionModification(Row, c_champMultiStructure, donnee);
				}
				return (CDonneeBinaireInRow)Row[c_champMultiStructure];
			}
			set
			{
				Row[c_champMultiStructure] = value;
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

		/// /////////////////////////////////////////////////////////////
		public CMultiStructureExport MultiStructure
		{
			get
			{
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
					}
				}
			}
		}
	}
}
 */
#endif