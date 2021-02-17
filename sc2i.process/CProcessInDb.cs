using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.process;
using sc2i.multitiers.client;
using sc2i.common.recherche;
using sc2i.process.recherche;
using sc2i.data.dynamic.recherche;

namespace sc2i.process
{
    /// <summary>
    /// Un process ou action est une suite d'actions élémentaires appelées briques.<br/>
    /// Chaque brique est liée à une autre brique via un ou plusieurs liens. Chaque process<br/>
    /// est un programme, chargé de réaliser des opérations de manière automatique dans une application.<br/>
    /// La création de process dans une application permet d'automatiser des tâches de gestion courante,<br/>
    /// de réaliser des workflows complexes ou de définir des règles de validation spécifiques à une logique métier.
    /// </summary>
	[DynamicClass("Process")]
	[Table(CProcessInDb.c_nomTable, CProcessInDb.c_champId, false)]
	[ObjetServeurURI("CProcessInDbServeur")]
	public class CProcessInDb : CObjetDonneeAIdNumeriqueAuto, IObjetCherchable
	{
		public const string c_nomTable = "PROCESS";
		public const string c_champId = "PROCESS_ID";
		public const string c_champLibelle = "PROCESS_LABEL";
		public const string c_champDescription = "PROCESS_DESCRIPTION";
		public const string c_champData = "PROCESS_DATA";
		public const string c_champTypeCible = "PROCESS_TARGET_TYPE";
		public const string c_champSurTableauDeTypeCible = "PROCESS_ON_ARRAY";
        public const string c_champWebVisible = "PROCESS_WEB_VISIBLE";
		
#if PDA
		public CProcessInDb( )
		:base()
		{
		}
#endif
		/// ////////////////////////////////////////////////
		public CProcessInDb ( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////
		public CProcessInDb ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 action|284",Libelle);
			}
		}

		/// ////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
            WebVisible = false;
		}

		//--------------------------------------------------
        /// <summary>
        /// Donne ou définit le Libellé du process
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

		//------------------------------------------------------
        /// <summary>
        /// Donne ou définit la description du process
        /// </summary>
		[TableFieldProperty(c_champDescription, 1024)]
		[DynamicField("Description")]
		public string Description
		{
			get
			{
				return(string)Row[c_champDescription];
			}
			set
			{
				Row[c_champDescription] = value;
			}
		}

		/// ////////////////////////////////////////////////
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
				Row[c_champData] = value;
			}
		}


		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CProcess Process
		{
			get
			{
				CProcess process = new CProcess(ContexteDonnee);
				if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					CResultAErreur result = process.Serialize(serializer);
					if ( !result )
					{
						process = new CProcess(ContexteDonnee);
					}
					process.Libelle = Libelle;
                    process.ReinitVariables();
                    reader.Close();
                    stream.Close();
				}
				return process;
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
					//Vide les variables du process pour être sur qu'elles prennent leurs
					//valeurs par défaut
					value.ReinitVariables();
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
					{
						TypeCible = value.TypeCible;
						SurTableauDeTypeCible = value.SurTableauDeTypeCible;
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
		[TableFieldProperty( c_champTypeCible, 255 )]
		public string TypeCibleString
		{
			get
			{
				return ( string )Row[c_champTypeCible];
			}
			set
			{
				Row[c_champTypeCible] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique que le process peut agir sur un tableau du type cible
		/// </summary>
		[TableFieldProperty( c_champSurTableauDeTypeCible )]
		[DynamicField("On array")]
		public bool SurTableauDeTypeCible
		{
			get
			{
				return ( bool )Row[c_champSurTableauDeTypeCible];
			}
			set
			{
				Row[c_champSurTableauDeTypeCible] = value;
			}
		}

        /// /////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique que l'action peut être appellée à partir d'un service web
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
		public Type TypeCible
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeCibleString );
			}
			set
			{
				if ( value == null )
					TypeCibleString = "";
				else
					TypeCibleString = value.ToString();
			}
		}

		//---------------------------------------------------------------
        /// <summary>
        /// Type convivial de l'objet auquel s'applique le process
        /// </summary>
		[DynamicField("Associated type")]
		public string TypeCibleConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial ( TypeCible )+(SurTableauDeTypeCible?"[]":"");
			}
		}

		
		//----------------------------------------------------------
		/// <summary>
		/// Donne ou définit le Groupe de paramétrage auquel appartient ce process
		/// </summary>
		[Relation ( CGroupeParametrage.c_nomTable, CGroupeParametrage.c_champId,
			 CGroupeParametrage.c_champId,
			 false,
			 false,
			 false)]
		[DynamicField("Parameter setting group")]
		public CGroupeParametrage GroupeParametrage
		{
			get
			{
				return (CGroupeParametrage)GetParent (CGroupeParametrage.c_champId, typeof(CGroupeParametrage));
			}
			set
			{
				SetParent ( CGroupeParametrage.c_champId, value );
			}
		}

        //---------------------------------------------------------------------
        /// <summary>
        /// Recherche un objet dans le process in DB
        /// </summary>
        /// <param name="objetCherche"></param>
        /// <param name="resultat"></param>
        public void RechercheObjet(object objetCherche, CResultatRequeteRechercheObjet resultat)
        {
            CProcess process = Process;
            if (process != null)
            {
                resultat.PushChemin(new CNoeudRechercheObjet_ObjetDonnee(this));
                try
                {
                    process.ChercheObjet(objetCherche, resultat);
                }
                finally
                {
                    resultat.PopChemin();
                }
            }
        }



        #region IObjetCherchable Membres

        public CRequeteRechercheObjet GetRequeteRecherche()
        {
            return new CRequeteRechercheObjet(
                I.T("Search action '@1'|20029", Libelle),
                this);
        }

        #endregion
    }
}
