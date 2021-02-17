using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.formulaire;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Représente un filtre de données.
	/// </summary>
    /// <remarks>
    /// <p>Seuls les filtres créés en tant que filtre standard et accessibles dans les listes sont stockés en utilisant l'entité "Dynamic filter".<br></br>
    /// Tous les autres filtres correspondant à divers éléments de paramétrage (structures d'export, rapports, ...) ne sont
    /// pas stockés sous forme de 'Dynamic filter'</p>
    /// </remarks>
	[Table(CFiltreDynamiqueInDb.c_nomTable, CFiltreDynamiqueInDb.c_champId, true)]
	[ObjetServeurURI("CFiltreDynamiqueInDbServeur")]
	[FullTableSync]
	[DynamicClass("Dynamic filter")]
	public class CFiltreDynamiqueInDb : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		public const string c_nomTable = "FILTER";

		public const string c_champId = "FILTER_ID";
		public const string c_champLibelle = "FILTER_LABEL";
		public const string c_champDescription = "FILTER_DESCRIPTION";
		public const string c_champData = "FILTER_DATA";
		public const string c_champTypeElements = "FILTER_ELEMENT_TYPE";

        
		/// ///////////////////////////////////////////////////////
		public CFiltreDynamiqueInDb( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CFiltreDynamiqueInDb ( DataRow row )
			:base ( row) 
		{
		}

		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Dynamic filter @1|177",Libelle);
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
        ///Libellé du filtre
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
		///<summary>
        ///Nom convivial des éléments filtrés par ce filtre.
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

		//7/ ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeElements, 255)]
		public string StringTypeElements
		{
			get
			{
				return (string)Row[c_champTypeElements];
			}
		}


		/// ///////////////////////////////////////////////////////
		///<Summmary>
        /// Permet de décrire le filtre. Ce champ est purement informatif n'est générallement pas présenté à l'utilisateur final de l'application. 
        /// Il peut être utilisé par l'administrateur pour décrire son filtre.
        /// </Summmary>
        [TableFieldProperty(c_champDescription, 255)]
		[DynamicField("Description")]
		public string Description
		{
			get
			{
				return (string)Row[c_champDescription];
			}
			set
			{
				Row[c_champDescription] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
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
				return ((CDonneeBinaireInRow)Row[c_champData]).GetSafeForRow ( Row.Row );
			}
			set
			{
				Row[c_champData] = value;
			}
		}


		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CFiltreDynamique Filtre
		{
			get
			{
				CFiltreDynamique filtre = new CFiltreDynamique(ContexteDonnee);
				if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					CResultAErreur result = filtre.Serialize(serializer);
					if ( !result )
					{
						filtre = new CFiltreDynamique(ContexteDonnee);
					}
					filtre.ContexteDonnee = ContexteDonnee;
					filtre.ResetValeursVariables();

                    reader.Close();
                    stream.Close();
				}
				return filtre;
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
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
					{
						if ( value !=  null && value.TypeElements !=  null )
							Row[c_champTypeElements]  =  value.TypeElements.ToString();
						else
							Row[c_champTypeElements] = "";
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
					}

                    writer.Close();
                    stream.Close();
				}
			}
		}

	}
}
