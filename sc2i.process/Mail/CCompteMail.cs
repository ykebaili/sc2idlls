using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using System.Data;
using sc2i.common;
using System.IO;

using sc2i.multitiers.client;

namespace sc2i.process.Mail
{
    [Table(CCompteMail.c_nomTable, CCompteMail.c_champId, true)]
    [ObjetServeurURI("CCompteMailServeur")]
    [DynamicClass("Mail Account")]
    [Evenement(CCompteMail.c_strIdEvenementBeforeRetrieve, "Before Retrieve", "Triggered before retrieving all Mails")]
    [Evenement(CCompteMail.c_strIdEvenementAfterRetrieve, "After Retrieve", "Triggered after retrieving all Mails")]
    public class CCompteMail : CObjetDonneeAIdNumeriqueAuto, IDefinisseurEvenements, IObjetSansVersion
    {
        public const string c_nomTable = "MAIL_ACCOUNT";
        public const string c_champId = "MAILACCNT_ID";
        public const string c_champLibelle = "MAILACCNT_LABEL";
        public const string c_champDataParametre = "MAILACCNT_DATA_PARAM";
        public const string c_champCacheParametre = "MAILACCNT_CACHE_PARAM";
        public const string c_champDateDernierReleve = "MAILACCNT_LAST_RCV_DATE";
        public const string c_champLastErreur = "MAILACCNT_LAST_ERROR";
        public const string c_champPeriodeReleve = "MAILACCNT_PERIOD";
        public const string c_champIsActive = "MAILACCNT_ACTIVE";

        public const string c_strIdEvenementBeforeRetrieve = "MAIL_EVT_BEFORE_RETR";
        public const string c_strIdEvenementAfterRetrieve = "MAIL_AVT_AFTER_RETR";

        public CCompteMail(CContexteDonnee ctx)
            :base(ctx)
        {

        }

        public CCompteMail(DataRow row)
            :base(row)
        {

        }

        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }

        protected override void MyInitValeurDefaut()
        {
            
        }

        public override string DescriptionElement
        {
            get { return I.T("Mail Account @1", Libelle); }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Indique si le Compte de Mail est actif ou non
        /// </summary>
        [TableFieldProperty(c_champIsActive)]
        [DynamicField("Is Active")]
        public bool IsActive
        {
            get
            {
                return (bool)Row[c_champIsActive];
            }
            set
            {
                Row[c_champIsActive] = value;
            }
        }


		//-----------------------------------------------------------
		/// <summary>
		/// Le libellé du Compte Mail
		/// </summary>
		[TableFieldProperty ( c_champLibelle, 200 )]
		[DynamicField("Label")]
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

        //-----------------------------------------------------------
        /// <summary>
        /// Périodicité de relevé des messages, en minutes
        /// </summary>
        [TableFieldProperty(c_champPeriodeReleve)]
        [DynamicField("Retrieve Period")]
        public int PeriodeReleve
        {
            get
            {
                return (int)Row[c_champPeriodeReleve];
            }
            set
            {
                Row[c_champPeriodeReleve] = value;
            }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des <see cref="C2iMail">Mails<see/> associés à ce Compte
        /// </summary>
        [RelationFille(typeof(C2iMail), "CompteMail")]
        [DynamicChilds("Mails", typeof(C2iMail))]
        public CListeObjetsDonnees Mails
        {
            get
            {
                return GetDependancesListe(C2iMail.c_nomTable, c_champId);
            }
        }

        //-----------------------------------------------------------------------------
        [TableFieldProperty(c_champDataParametre, NullAutorise = true)]
        public CDonneeBinaireInRow DataParametre
        {
            get
            {
                if (Row[c_champDataParametre] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champDataParametre);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataParametre, donnee);
                }
                return ((CDonneeBinaireInRow)Row[c_champDataParametre]).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champDataParametre] = value;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Représente le <see cref="CParametreCompteMail">Paramètrage</see> du compte de mail<br/>
        /// Incluant les informations de connexion serveur de mails (nom d'hôte, identifiants...)
        /// </summary>
        [TableFieldProperty(c_champCacheParametre, IsInDb = false, NullAutorise = true)]
        [NonCloneable]
        [DynamicField("Mail Account Setting")]
        [BlobDecoder]
        public CParametreCompteMail ParametreCompteMail
        {
            get
            {
                if (Row[c_champCacheParametre] != DBNull.Value)
                    return (CParametreCompteMail)Row[c_champCacheParametre];
                CParametreCompteMail param = null;
                if (DataParametre.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(DataParametre.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    serializer.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                    CResultAErreur result = CResultAErreur.True;
                    try
                    {
                        result = serializer.TraiteObject<CParametreCompteMail>(ref param);
                    }
                    catch
                    {
                        result.Result = false;
                    }
                    if (!result)
                        param = null;
                    else
                        CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheParametre, param);
                    reader.Close();
                    stream.Close();
                }
                return param;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow data = DataParametre;
                    data.Donnees = null;
                    DataParametre = data;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CParametreCompteMail param = value;
                    CResultAErreur result = serializer.TraiteObject<CParametreCompteMail>(ref param);
                    if (result)
                    {
                        CDonneeBinaireInRow data = DataParametre;
                        data.Donnees = stream.GetBuffer();
                        DataParametre = data;

                    }
                    writer.Close();
                    stream.Close();
                }
                Row[c_champCacheParametre] = DBNull.Value;
            }
        }



        //-----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        [TableFieldProperty(c_champDateDernierReleve, NullAutorise = true)]
        [DynamicField("Last Receive Date")]
        public DateTime? DateDernierReleve
        {
            get
            {
                return (DateTime?)Row[c_champDateDernierReleve, true];
            }
            set
            {
                Row[c_champDateDernierReleve, true] = value;
            }
        }

		//-----------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		[TableFieldProperty ( c_champLastErreur, 2000 )]
		[DynamicField("Last Error")]
		public string LastErreur
		{
			get
			{
				return (string)Row[c_champLastErreur];
			}
			set
			{
				Row[c_champLastErreur] = value;
			}
		}

        //--------------------------------------------------------------
        /// <summary>
        /// Retrieve all Messages for this Mail Account
        /// </summary>
        /// <returns>true if operation succeeded</returns>
        [DynamicMethod("Retrieve all Messages for this Mail Account")]
        public CResultAErreur RetrieveMails()
        {
            CResultAErreur result = CResultAErreur.True;
            ICompteMailServeur serveur = (ICompteMailServeur)GetLoader();
            if(serveur != null)
                result = serveur.RetrieveMails(this.Id);
            return result;
        }



        #region IDefinisseurEvenements Membres

        public Type[] TypesCibleEvenement
        {
            get { return new Type[] { typeof(C2iMail) }; }
        }

        public CListeObjetsDonnees Evenements
        {
            get
            {
                return CUtilDefinisseurEvenement.GetEvenementsFor(this);
            }
        }

        public CComportementGenerique[] ComportementsInduits
        {
            get
            {
                return CUtilDefinisseurEvenement.GetComportementsInduits(this);
            }
        }

        #endregion
    }
}
