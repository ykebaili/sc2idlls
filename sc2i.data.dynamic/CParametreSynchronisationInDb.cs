using System;
using System.Data;
using System.IO;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;

#if !PDA_DATA
namespace sc2i.data.dynamic
{
	/// <summary>
	/// Paramètres de synchronisation
	/// </summary>
    /// <remarks>
    /// Suivant les options de l'application, il est possible que cette entité ne soit pas accessible.
    /// </remarks>
	[Table(CParametreSynchronisationInDb.c_nomTable,CParametreSynchronisationInDb.c_champId, false)]
	[DroitAssocie( 
		 CParametreSynchronisationInDb.c_codeDroit,
		"Paramètres de synchronisation",
		"Donnee le droit de gérer les paramètres de synchronisation",
		100,
		CDroitDeBaseSC2I.c_droitAdministrationSysteme)
	]
	[ObjetServeurURI("CParametreSynchronisationInDbServeur")]
	[DynamicClass("Synchronisation setting")]
	public class CParametreSynchronisationInDb : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_codeDroit = "SYNCHRONISATION_SETTINGS";
		public const string c_nomTable = "SYNC_SETTING";
		public const string c_champId = "SYNCSET_ID";
		public const string c_champLibelle = "SYNCSET_LABEL";
		public const string c_champCode = "SYNCSET_CODE";
		public const string c_champData = "SYNCSET_DATA";
		public const string c_champVersion = "SYNCSET_VERSION";

		private CParametreSynchronisation m_parametre = null;

		/// ////////////////////////////////////////////////////////
		public CParametreSynchronisationInDb( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////////////
		public CParametreSynchronisationInDb ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The Synchronisation setting @1|190", Libelle);
			}
		}

		/// ////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			Data = null;
			Version = 0;
		}

		/// ////////////////////////////////////////////////////////
		protected override void OnRowChanged()
		{
			m_parametre = null;
		}


		/// ////////////////////////////////////////////////////////
		///<summary>
        ///Libellé du paramétrage
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
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

		/// ////////////////////////////////////////////////////////
		///<summary>
        ///Code unique identifiant le paramétrage (optionnel)
        /// </summary>
        [TableFieldProperty(c_champCode, 64)]
		[DynamicField("Code")]
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

		/// ////////////////////////////////////////////////////////
		///<summary>Numéro de version de ce paramétrage.</summary>
        [TableFieldProperty(c_champVersion)]
		[DynamicField("Version")]
		public int Version
		{
			get
			{
				return (int)Row[c_champVersion];
			}
			set
			{
				Row[c_champVersion] = value;
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
				return ((CDonneeBinaireInRow)Row[c_champData]).GetSafeForRow(Row.Row);
			}
			set
			{
				if ( value == null )
					Row[c_champData] = DBNull.Value;
				else
					Row[c_champData] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CParametreSynchronisation Parametre
		{
			get
			{
				if ( m_parametre == null )
				{
					m_parametre = new CParametreSynchronisation();
					if ( Data.Donnees != null )
					{
						MemoryStream stream = new MemoryStream(Data.Donnees);
						BinaryReader reader = new BinaryReader(stream);
						CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
						CResultAErreur result = m_parametre.Serialize(serializer);
						if ( !result )
						{
							m_parametre = new CParametreSynchronisation();
						}

                        reader.Close();
                        stream.Close();
					}
				}
				return m_parametre;
			}
			set
			{
				m_parametre = null;
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
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
					}

                    writer.Close();
                    stream.Close();
				}
			}
		}


		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Relations vers les groupes utilisant ce paramétrage
        /// </summary>
        [RelationFille(typeof(CGroupeUtilisateursSynchronisation), "ParametreSynchronisation")]
		[DynamicChilds("Users groups", typeof(CGroupeUtilisateursSynchronisation))]
		public CListeObjetsDonnees GroupesUtilisateurs
		{
			get
			{
				return GetDependancesListe ( CGroupeUtilisateursSynchronisation.c_nomTable, c_champId );
			}
		}
		

		
		
	}
}
#endif
