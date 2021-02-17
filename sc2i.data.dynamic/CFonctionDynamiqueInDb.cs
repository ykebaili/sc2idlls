using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.common.unites;
using sc2i.expression.FonctionsDynamiques;
using System.IO;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Représente une fonction dynamique
	/// </summary>
    /// <remarks>
    /// <p>Chaque fonction dynamique est prévu pour un type d'entité spécifique et ne pourra être appliquée qu'à lui seul.</p>
    /// Il est possible de créer des fonctions dynamiques sur tous les types d'entité de l'application. Ces fonctions seront disponibles
    /// en dans les formules et dans tout élément de paramétrage permettant l'accès aux méthodes de l'application.
    /// <p>Les formules dynamiques sont générallement utilisés pour éviter de répéter des formules dont on a souvent besoin sur des entités.
    /// </p>
    /// </remarks>
	[Table(CFonctionDynamiqueInDb.c_nomTable, CFonctionDynamiqueInDb.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CFonctionDynamiqueInDbServeur")]
	[DynamicClass("Dynamic formula")]
    [Unique(false,"Dynamic function name is not unique",CFonctionDynamiqueInDb.c_champNom, CFonctionDynamiqueInDb.c_champTypeObjets)]
	public class CFonctionDynamiqueInDb : 
        CObjetDonneeAIdNumeriqueAuto, 
        IObjetALectureTableComplete,
        IObjetCherchable
	{
		public const string c_nomTable = "DYNAMIC_FUNCTIONS";
		public const string c_champId = "DYNFUN_ID";
		public const string c_champNom = "DYNFUN_NAME";
		public const string c_champDescription = "DYNFUN_DESC";
		public const string c_champFonction = "DYNFUN_FUNCTION";
		public const string c_champTypeObjets = "DYNFUN_OBJET_TYPE";
        public const string c_champRubrique = "DYNFUN_FOLDER";

		public const string c_champCacheFonction = "DYNFUN_FUNCTION_CACHE";

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CFonctionDynamiqueInDb( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CFonctionDynamiqueInDb ( DataRow row )
			:base(row)
		{
		}

		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Dynamic formula @1|20082",Nom);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champNom};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Nom de la fonction. Ce nom doit être unique.
        ///</summary>
        [TableFieldPropertyAttribute(c_champNom, 64)]
		[DynamicField("Name")]
		public string Nom
		{
			get
			{
				return (string)Row[c_champNom];
			}
			set
			{
				Row[c_champNom] = value;
			}
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Description de la formule
        /// </summary>
        /// <remarks>
        /// Il est recommandé à l'administrateur d'utiliser la description pour décrire le fonctionnement de la fonction
        /// ainsi que d'indiquer dans quel contexte elle est utilisée.
        /// <p>Une bonne documentation des formules peut simplifier le travail de maintenance du paramétrage.</p>
        /// </remarks>
        [TableFieldProperty(c_champDescription, 256)]
        [DynamicField("Function description")]
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

        

		

		// /////////////////////////////////////////////////////////
        [TableFieldProperty(c_champFonction, NullAutorise = true)]
        public CDonneeBinaireInRow DataFonction
        {
            get
            {
                if (Row[c_champFonction] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champFonction);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champFonction, donnee);
                }
                object obj = Row[c_champFonction];
                return ((CDonneeBinaireInRow)obj).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champFonction] = value;
                Row[c_champCacheFonction] = DBNull.Value;
            }
        }


        // /////////////////////////////////////////////////////////
        [TableFieldProperty (c_champCacheFonction, IsInDb = false, NullAutorise = true)]
        [BlobDecoder]
        public CFonctionDynamique Fonction
        {
            //TESTBLOBTODO
            get
            {
                CFonctionDynamique fonction = null;
                if (!DecodeBlob<CFonctionDynamique>(DataFonction, ref fonction, c_champCacheFonction))
                    return null;
                return fonction;
                /*
                if (Row[c_champCacheFonction] != DBNull.Value)
                    return (CFonctionDynamique)Row[c_champCacheFonction];
                CDonneeBinaireInRow data = DataFonction;
                if (data != null && data.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(data.Donnees);
                    
                    CFonctionDynamique fonction = null;
                    BinaryReader reader = new BinaryReader ( stream );
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    serializer.TraiteObject<CFonctionDynamique>(ref fonction);
                        if ( fonction != null )
                        {
                        CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFonction, fonction);
                            return fonction;
                        }
                    reader.Close();
                    stream.Close();
                    stream.Dispose();
                }  
                return null;*/
            }
            set
            {
                CDonneeBinaireInRow data = DataFonction;
                if ( EncodeBlob<CFonctionDynamique>(data, value, c_champCacheFonction ) )
                {
                    DataFonction = data;
                    if (value != null)
                    {
                        Nom = value.Nom;
                        Row[CObjetDonnee.c_champIdUniversel] = value.IdFonction;
                    }
                }
                /*
                Row[c_champCacheFonction] = DBNull.Value;
                if (value == null)
                {
                    CDonneeBinaireInRow data = DataFonction;
                    data.Donnees = null;
                    DataFonction = data;
                    
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream );
                    CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
                    CFonctionDynamique f = value;
                    CResultAErreur result = ser.TraiteObject<CFonctionDynamique> ( ref value );
                    if (result)
                    {
                        Nom = value.Nom;
                        Row[CObjetDonnee.c_champIdUniversel] = value.IdFonction;
                        CDonneeBinaireInRow donnee = DataFonction;
                        donnee.Donnees = stream.ToArray();
                        DataFonction = donnee;
                    }
                }*/
            }
        }
		/// //////////////////////////////////////////////////
		///<summary>
        /// Nom convivial du type des entités concernés par le champ calculé
        /// </summary>
        [DynamicField("Object type")]
		public string TypeObjetsConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeObjets);
			}
		}

		/// //////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeObjets, 1024)]
		public string TypeObjetsString
		{
			get
			{
				return (string)Row[c_champTypeObjets];
			}
			set
			{
				Row[c_champTypeObjets] = value;
			}
		}

        /// //////////////////////////////////////////////////
        [DynamicField("Folder")]
        [TableFieldProperty(c_champRubrique, 64)]
        public string Categorie
        {
            get
            {
                return (string)Row[c_champRubrique];
            }
            set
            {
                Row[c_champRubrique] = value;
            }
        }

        /// //////////////////////////////////////////////////
        public override void AfterRead()
        {
            base.AfterRead();
            //Viré, sinon, le cache ne sert à rien !
            //CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFonction, DBNull.Value);
        }

		/// //////////////////////////////////////////////////
		public Type TypeObjets
		{
			get
			{
				return CActivatorSurChaine.GetType(TypeObjetsString);
			}
			set
			{
				if(  value == null )
					TypeObjetsString = "";
				else
					TypeObjetsString = value.ToString();
			}
		}

		
        #region IObjetCherchable Membres

        public CRequeteRechercheObjet GetRequeteRecherche()
        {
            if (Fonction != null)
            {
                CDefinitionFonctionDynamique defF = new CDefinitionFonctionDynamique(Fonction);
                return new CRequeteRechercheObjet(
                    I.T("Search function '@1'|20083", Nom),
                    defF);
            }
            return null;
        }

        #endregion
    }

		
}
