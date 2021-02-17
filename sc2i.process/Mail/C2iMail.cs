using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using OpenPop.Mime;
using sc2i.documents;
using OpenPop.Mime.Header;
using System.Text.RegularExpressions;

namespace sc2i.process.Mail
{
    [Table(C2iMail.c_nomTable, C2iMail.c_champId, true)]
    [ObjetServeurURI("C2iMailServeur")]
    [DynamicClass("Mail")]
    public class C2iMail : CObjetDonneeAIdNumeriqueAuto, IElementAEvenementsDefinis
    {
        public const string c_nomTable = "MAIL";
        public const string c_champId = "MAIL_ID";

        public const string c_champFrom = "MAIL_FROM";
        public const string c_champTo = "MAIL_TO";
        public const string c_champObjet = "MAIL_SUBJECT";
        public const string c_champMessage = "MAIL_MESSAGE";
        public const string c_champFormatHTML = "MAIL_HTML_FORMAT";
        public const string c_champDateCreation = "MAIL_CREATE_DATE";
        public const string c_champDateReception = "MAIL_RECEIVE_DATE";
        public const string c_champTaille = "MAIL_SIZE";
        public const string c_champMessageId = "MAIL_MESSAGE_ID";
        public const string c_champNombrePiecesJointes = "MAIL_NB_ATTACHEMENTS";

        public C2iMail(CContexteDonnee ctx)
            :base(ctx)
        {

        }

        public C2iMail(DataRow row)
            :base(row)
        {

        }

        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champDateReception + " desc" };
        }

        protected override void MyInitValeurDefaut()
        {
            DateCreation = DateTime.Now;
        }

        public override string DescriptionElement
        {
            get { return I.T("Mail @1|10001", Sujet); }
        }

        
		//-----------------------------------------------------------
		/// <summary>
		/// Représente l'identifiant unique du Mail.
        /// Permet de savoir si le Mail récupéré du serveur de mails a déjà été enregistré dans la base de donnée.
		/// </summary>
        [TableFieldProperty(c_champMessageId, 200)]
        [DynamicField("Message Id")]
        public string MessageId
        {
            get
            {
                return (string)Row[c_champMessageId];
            }
            set
            {
                Row[c_champMessageId] = value;
            }
        }



		//------------------------------------------------------------------
		/// <summary>
		/// L'Expéditeur du Mail sous forme de texte
		/// </summary>
		[TableFieldProperty ( c_champFrom, 500 )]
		[DynamicField("From")]
		public string From
		{
			get
			{
				return (string)Row[c_champFrom];
			}
			set
			{
				Row[c_champFrom] = value;
			}
		}

        
		//------------------------------------------------------------------
		/// <summary>
        /// Les destinataires du Mail sous forme de texte
		/// </summary>
		[TableFieldProperty ( c_champTo, 500 )]
		[DynamicField("To")]
		public string To
		{
			get
			{
				return (string)Row[c_champTo];
			}
			set
			{
				Row[c_champTo] = value;
			}
		}

		//-----------------------------------------------------------
		/// <summary>
		/// Le sujet du Mail
		/// </summary>
        [TableFieldProperty(c_champObjet, 500)]
        [DynamicField("Subject")]
        public string Sujet
        {
            get
            {
                return (string)Row[c_champObjet];
            }
            set
            {
                Row[c_champObjet] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// La tallie en octets du Mail
        /// </summary>
        [TableFieldProperty(c_champTaille)]
        [DynamicField("Size")]
        public double Taille
        {
            get
            {
                return (double)Row[c_champTaille];
            }
            set
            {
                Row[c_champTaille] = value;
            }
        }


		//-----------------------------------------------------------
		/// <summary>
		/// Le corps du message du mail
		/// </summary>
        [TableFieldProperty(c_champMessage, IsLongString = true)]
        [DynamicField("Message Body")]
        public string MessageBody
        {
            get
            {
                return (string)Row[c_champMessage];
            }
            set
            {
                Row[c_champMessage] = value;
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Indique si le message du Mail est au format HTML.
        /// Si vrai le message est au format HTML, si faux au format texte brut
        /// </summary>
        [TableFieldProperty(c_champFormatHTML)]
        [DynamicField("HTML Format")]
        public bool FormatHTML
        {
            get
            {
                return (bool)Row[c_champFormatHTML];
            }
            set
            {
                Row[c_champFormatHTML] = value;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Retourne le corps du message en tant que texte brut
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Retruns the message body as plain text")]
        public string GetMessageBodyAsText()
        {
            if (FormatHTML)
            {
                return CUtilHtml.ConvertHtmlToPlainText(MessageBody);
            }
            return MessageBody;
            
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Date de création du Mail en base de donnée
        /// </summary>
        [TableFieldProperty(c_champDateCreation, NullAutorise = true)]
        [DynamicField("Creation Date")]
        public DateTime? DateCreation
        {
            get
            {
                return (DateTime?)Row[c_champDateCreation, true];
            }
            set
            {
                Row[c_champDateCreation, true] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Date de réception du Mail sur le Serveur de Mail
        /// </summary>
        [TableFieldProperty(c_champDateReception)]
        [DynamicField("Receive Date")]
        public DateTime DateReception
        {
            get
            {
                return (DateTime)Row[c_champDateReception];
            }
            set
            {
                DateTime dt = value;
                if (dt <= DateTime.MinValue || dt >= DateTime.MaxValue)
                    dt = DateTime.Now;
                Row[c_champDateReception] = dt;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit le <see cref="CCompteMail">Compte Mail<see/> associé à ce Mail
        /// </summary>
        [Relation(
            CCompteMail.c_nomTable,
            CCompteMail.c_champId,
            CCompteMail.c_champId,
            true,
            false,
            true)]
        [DynamicField("Mail Account")]
        public CCompteMail CompteMail
        {
            get
            {
                return (CCompteMail)GetParent(CCompteMail.c_champId, typeof(CCompteMail));
            }
            set
            {
                SetParent(CCompteMail.c_champId, value);
            }
        }



        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit le <see cref="CDossierMail">Dossier de Mail</see> dans lequel le Mail est rangé
        /// </summary>
        [Relation(
            CDossierMail.c_nomTable,
            CDossierMail.c_champId,
            CDossierMail.c_champId,
            false,
            false,
            false)]
        [DynamicField("Folder")]
        public CDossierMail Dossier
        {
            get
            {
                return (CDossierMail)GetParent(CDossierMail.c_champId, typeof(CDossierMail));
            }
            set
            {
                SetParent(CDossierMail.c_champId, value);
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Le nombre de pièces jointes du Mail
        /// </summary>
        [TableFieldProperty(c_champNombrePiecesJointes)]
        [DynamicField("Attachements Count")]
        public int NombrePiecesJointes
        {
            get
            {
                return (int)Row[c_champNombrePiecesJointes];
            }
            set
            {
                Row[c_champNombrePiecesJointes] = value;
            }
        }

        //------------------------------------------------------------------
        /// <summary>
        /// Récupère le message complet du serveur et remplit les champs du Mail
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Retrieve the complete Message from Mail Server")]
        public CResultAErreur RetriveFullMessage()
        {
            CResultAErreur result = CResultAErreur.True;
            I2iMailServeur serveur = (I2iMailServeur)GetLoader();
            if (serveur != null)
                result = serveur.RetrieveMessageComplet(Id);
            return result;
        }

        //-------------------------------------------------------------------
        public void FillFromHeader(MessageHeader header)
        {
            MessageId = header.MessageId;
            DateReception = header.DateSent;
            if (header.From != null)
                From = header.From.Address.ToString();
            if (header.To != null)
                To = string.Join(";", (from de in header.To select de.ToString()).ToArray());

            Sujet = header.Subject;

        }

        //-----------------------------------------------------------------------------------
        public void FillFromMessage(Message messageRecu)
        {
            FillFromHeader(messageRecu.Headers);

            FormatHTML = messageRecu.FindFirstHtmlVersion() != null;
            Taille = messageRecu.RawMessage.Length;
            
            if (FormatHTML)
            {
                MessagePart partHtml = messageRecu.FindFirstHtmlVersion();
                MessageBody = partHtml.GetBodyAsText();
            }
            else
                MessageBody = messageRecu.FindFirstPlainTextVersion().GetBodyAsText();

            List<MessagePart> listePiecesJoints = messageRecu.FindAllAttachments();
            NombrePiecesJointes = listePiecesJoints.Count;
            foreach (MessagePart pieceJointe in listePiecesJoints)
            {
                EnregistrePieceJointeEnGED(pieceJointe);
            }
        }

        //-----------------------------------------------------------------------------------
        private CResultAErreur EnregistrePieceJointeEnGED(MessagePart pieceJointe)
        {
            CResultAErreur result = CResultAErreur.True;

            if (pieceJointe != null && pieceJointe.IsAttachment)
            {
                int nIndex = pieceJointe.FileName.LastIndexOf(".");
                string strExtension = "";
                if (nIndex > 0)
                    strExtension = pieceJointe.FileName.Substring(nIndex + 1);
                else
                    strExtension = "dat";

                byte[] contenuPj = pieceJointe.Body;
                Stream fluxPJ = new MemoryStream(contenuPj);
                if (fluxPJ.Length == 0)
                    return result;

                CSourceDocument srcDoc = new CSourceDocumentStream(fluxPJ, strExtension);
                CDocumentGED documentAttache = new CDocumentGED(ContexteDonnee);
                documentAttache.CreateNewInCurrentContexte();
                documentAttache.AssocieA(this);
                documentAttache.Libelle = pieceJointe.FileName;
                result = CDocumentGED.SaveDocument(
                    ContexteDonnee.IdSession,
                    srcDoc,
                    new CTypeReferenceDocument(CTypeReferenceDocument.TypesReference.Fichier),
                    null,
                    false);
                srcDoc.Dispose();

                if (!result)
                    return result;

                documentAttache.ReferenceDoc = result.Data as CReferenceDocument;

            }

            return result;

        }


        #region IElementAEvenementsDefinis Membres

        public IDefinisseurEvenements[] Definisseurs
        {
            get
            {
                List<IDefinisseurEvenements> listeDefinisseurs = new List<IDefinisseurEvenements>();
                if (CompteMail != null)
                    listeDefinisseurs.Add(CompteMail);
                return listeDefinisseurs.ToArray();
            }
        }

        public bool IsDefiniPar(IDefinisseurEvenements definisseur)
        {
            if (CompteMail != null)
                return CompteMail.Equals(definisseur);
            return false;
        }

        #endregion
    }
}
