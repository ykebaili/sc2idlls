using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;

#if !PDA_DATA

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Stocke le paramétrage d'une structure d'export de données
	/// </summary>
    /// <remarks>
    /// Seules les structures d'export indépendantes sont accessibles via cette entité.
    /// <BR></BR>Les structures spécifiques à des rapports ou présente dans d'autres éléments de paramétrage
    /// ne sont pas des entité "Export data structure"
    /// </remarks>
	[Table(C2iStructureExportInDB.c_nomTable, C2iStructureExportInDB.c_champId, true)]
	[ObjetServeurURI("C2iStructureExportInDBServeur")]
	[DynamicClass("Export data structure")]
	[FullTableSync]
	public class C2iStructureExportInDB : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "EXPORT_STRUCTURE";

		public const string c_champId = "EXPSTR_ID";
		public const string c_champLibelle = "EXPSTR_LABEL";
		public const string c_champData = "STREXP_DATA";
		public const string c_champTypeElements = "EXPSPR_ELEMENT_TYPE";

		/// ///////////////////////////////////////////////////////
		public C2iStructureExportInDB( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		/// ///////////////////////////////////////////////////////
		public C2iStructureExportInDB ( DataRow row )
			:base ( row) 
		{
		}

		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The export data structure @1|113", Libelle);
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ///////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// ///////////////////////////////////////////////////////
		///<summary>
        ///Libellé de la structure d'export
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

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champData,NullAutorise=true)]
		public CDonneeBinaireInRow Data
		{
			get
			{
				if ( Row[c_champData] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champData);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champData, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champData]).GetSafeForRow(Row.Row);
			}
			set
			{
				Row[c_champData] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		[BlobDecoder]
        public CMultiStructureExport MultiStructure
		{
            //TESTBLOBTODO
            get
            {
                CMultiStructureExport retour = null;
                DecodeBlob<CMultiStructureExport>(Data, ref retour);
                if (retour != null)
                {
                    retour.ContexteDonnee = ContexteDonnee;
                    retour.ResetValeursVariables();
                }
                return retour;
            }
				/*if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
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
				}
				return retour;*/
			
			set
			{
                CDonneeBinaireInRow data = Data;
                if (EncodeBlob<CMultiStructureExport>(
                    data,
                    value))
                {
                    Data = data;
                    if (value != null && value.TypeDonneesEntree != null)
                        Row[c_champTypeElements] = value.TypeDonneesEntree.ToString();
                    else
                        Row[c_champTypeElements] = "";
                }
				/*if ( value == null )
				{
					CDonneeBinaireInRow data = Data;
					data.Donnees = null;
					Data = data;
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
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
						if ( value !=  null && value.TypeDonneesEntree!=  null )
							Row[c_champTypeElements]  =  value.TypeDonneesEntree.ToString();
						else
							Row[c_champTypeElements] = "";
					}

                    writer.Close();
                    stream.Close();
				}*/
			}
		}

		/*/// /////////////////////////////////////////////////////////////
		public C2iStructureExport StructureExport
		{
			get
			{
				C2iStructureExport structure  = new C2iStructureExport();
				if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
					structure.Serialize(serializer);
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
							Row[c_champTypeElements]  =  value.TypeSource.ToString();
						else
							Row[c_champTypeElements] = "";
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
					}
				}
			}
		}*/

		/// ///////////////////////////////////////////////////////
		///<summary>
        ///Retourne le nom convival des éléments racine de la structure d'export. Ce champ peut être utilisé pour 
        ///présenter à l'utilisateur final le nom des éléments exportés.
        /// </summary>
        [DynamicField("Element type")]
		public string NomTypeElements
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial ( TypeElements );
			}
		}

		/// ///////////////////////////////////////////////////////
		public Type TypeElements
		{
			get
			{
				Type tp = null;
				try
				{
					tp = CActivatorSurChaine.GetType ( StringTypeElements );
				}
				catch
				{
					tp = null;
				}
				return tp;
			}
		}

		/// ///////////////////////////////////////////////////////
        ///<summary>
        ///Type interne des éléments exportés. Ce type est stocké suivant un codage interne au système
        /// </summary>
		[TableFieldProperty(c_champTypeElements, 255)]
		public string StringTypeElements
		{
			get
			{
				return (string)Row[c_champTypeElements];
			}
		}


		
		
		
	}
}
#endif