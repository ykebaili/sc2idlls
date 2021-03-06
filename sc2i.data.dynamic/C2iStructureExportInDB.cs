using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Stocke le param�trage d'une structure d'export de donn�es
	/// </summary>
    /// <remarks>
    /// Seules les structures d'export ind�pendantes sont accessibles via cette entit�.
    /// <BR></BR>Les structures sp�cifiques � des rapports ou pr�sente dans d'autres �l�ments de param�trage
    /// ne sont pas des entit� "Export data structure"
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
		public const string c_champDescription = "EXPSTR_DESCRIPTION";
		public const string c_champWebVisible = "EXPSTR_WEB_VISIBLE";
		public const string c_champUpdatePeriod = "EXPSTR_UPDATE_PERIOD";
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
        ///Libell� de la structure d'export
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

        //------------------------------------------------------
        /// <summary>
        /// Donne ou d�finit la description de la structure d'export
        /// </summary>
        [TableFieldProperty(c_champDescription, 1024)]
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
        /// <summary>
        /// Indique que l'export peut �tre appell�e � partir d'un service web
        /// </summary>
        [TableFieldProperty(c_champWebVisible)]
        [DynamicField("Web visible")]
        public bool WebVisible
        {
            get
            {
                return (bool)Row[c_champWebVisible];
            }
            set
            {
                Row[c_champWebVisible] = value;
            }
        }

        /// /////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique la p�riode de rafraichissement de l'export vers l'application web
        /// </summary>
        [TableFieldProperty(c_champUpdatePeriod)]
        [DynamicField("Update period")]
        public int UpdatePeriod
        {
            get
            {
                return (int)Row[c_champUpdatePeriod];
            }
            set
            {
                Row[c_champUpdatePeriod] = value;
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
        ///Retourne le nom convival des �l�ments racine de la structure d'export. Ce champ peut �tre utilis� pour 
        ///pr�senter � l'utilisateur final le nom des �l�ments export�s.
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
        ///Type interne des �l�ments export�s. Ce type est stock� suivant un codage interne au syst�me
        /// </summary>
		[TableFieldProperty(c_champTypeElements, 255)]
		public string StringTypeElements
		{
			get
			{
				return (string)Row[c_champTypeElements];
			}
		}

        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Donne ou d�finit le Groupe de param�trage auquel appartient cette strusture d'export
        /// </summary>
        [Relation(CGroupeParametrage.c_nomTable, CGroupeParametrage.c_champId,
             CGroupeParametrage.c_champId,
             false,
             false,
             false)]
        [DynamicField("Parameter setting group")]
        public CGroupeParametrage GroupeParametrage
        {
            get
            {
                return (CGroupeParametrage)GetParent(CGroupeParametrage.c_champId, typeof(CGroupeParametrage));
            }
            set
            {
                SetParent(CGroupeParametrage.c_champId, value);
            }
        }
        //*/


    }
}
