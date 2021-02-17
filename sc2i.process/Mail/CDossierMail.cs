using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;
using System.Data;

namespace sc2i.process.Mail
{
    [Table(CDossierMail.c_nomTable, CDossierMail.c_champId, true)]
    [ObjetServeurURI("CDossierMailServeur")]
    [DynamicClass("Mail Folder")]
    public class CDossierMail : CObjetHierarchique, IObjetALectureTableComplete
    {
        public const string c_nomTable = "MAIL_FOLDER";
        public const string c_champId = "MAILFOLDER_ID";

        public const string c_champLibelle = "MAILFOLDER_LABEL";
        public const string c_champDescriptif = "MAILFOLDER_DESCRIPTION";

        public const string c_champCodeSystemePartiel = "PARTIAL_SYSTEM_CODE";
        public const string c_champCodeSystemeComplet = "FULL_SYSTEM_CODE";
        public const string c_champNiveau = "MAILFOLDER_LEVEL";
        public const string c_champIdParent = "PARENT_ID";

        //-------------------------------------------------------------------
        public CDossierMail(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        //-------------------------------------------------------------------
        public CDossierMail(DataRow row)
            : base(row)
        {
        }

        public override int NbCarsParNiveau
        {
            get { return 2; }
        }

        public override string ChampCodeSystemePartiel
        {
            get { return c_champCodeSystemePartiel; }
        }

        public override string ChampCodeSystemeComplet
        {
            get { return c_champCodeSystemeComplet; }
        }

        public override string ChampNiveau
        {
            get { return c_champNiveau; }
        }

        public override string ChampLibelle
        {
            get { return c_champLibelle; }
        }

        public override string ChampIdParent
        {
            get { return c_champIdParent; }
        }

        //----------------------------------------------------
        /// <summary>
        /// Code système propre à la catégorie GED.<br/>
        /// Ce code est exprimé sur 2 caractères alphanumériques
        /// </summary>
        [TableFieldProperty(c_champCodeSystemePartiel, 2)]
        [DynamicField("Partial system code")]
        public override string CodeSystemePartiel
        {
            get
            {
                string strVal = "";
                if (Row[c_champCodeSystemePartiel] != DBNull.Value)
                    strVal = (string)Row[c_champCodeSystemePartiel];
                strVal = strVal.Trim().PadLeft(2, '0');
                return strVal;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Code système complet de la catégorie GED. Ce code système est<br/>
        /// constitué du code système complet de la catégorie GED parente<br/>
        /// concaténé avec le code système propre de la catégorie GED.<br/>
        /// Exemple : CG_GRDPARENTE -> CG_PARENTE -> CG_FILLE<br/>
        /// si le code de CG_GRDPARENTE est 01, le code de CG_PARENTE est 03 et<br/>
        /// et le code propre de CG_FILLE est 08, le code système complet<br/>
        /// de CG_FILLE est 010308.
        /// </summary>
        [TableFieldProperty(c_champCodeSystemeComplet, 100)]
        [DynamicField("Full system code")]
        public override string CodeSystemeComplet
        {
            get
            {
                return (string)Row[c_champCodeSystemeComplet];
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Niveau de la catégorie GED dans la hiérarchie des catégories (nombre de parents).<br/>
        /// Exemple : CG_GRDPARENTE -> CG_PARENTE -> CG_FILLE<br/>
        /// CG_GRDPARENTE a pour niveau 0, CG_PARENTE a pour niveau 1<br/>
        /// (1 parent en remontant la hiérarchie), CG_FILLE a pour niveau 2<br/>
        /// (2 parents en remontant la hiérarchie)
        /// </summary>
        [TableFieldProperty(c_champNiveau)]
        [DynamicField("Level")]
        public override int Niveau
        {
            get
            {
                return (int)Row[c_champNiveau];
            }
        }



        //-------------------------------------------------------------------
        public override string DescriptionElement
        {
            get
            {
				return I.T("Mail Folder @1", Libelle);
            }
        }

        //-------------------------------------------------------------------
        protected override void MyInitValeurDefaut()
        {
            base.MyInitValeurDefaut();
        }

        //-------------------------------------------------------------------
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le libellé de la catégorie
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
        [DynamicField("Label")]
		[DescriptionField]
		public override string Libelle
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
        /// Donne ou définit la description du Dossier de Mail
        /// </summary>
        [TableFieldProperty(c_champDescriptif, 2000)]
        [DynamicField("Description")]
        public string Descriptif
        {
            get
            {
                return (string)Row[c_champDescriptif];
            }
            set
            {
                Row[c_champDescriptif] = value;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit le Dossier de Mail parent dans la hiérarchie
        /// </summary>
        [Relation(
            CDossierMail.c_nomTable,
            CDossierMail.c_champId,
            c_champIdParent,
            false,
            false,
            true)]
        [DynamicField("Parent Mail Folder")]
        public CDossierMail DossierMailParent
        {
            get
            {
                return (CDossierMail)ObjetParent;
            }
            set
            {
                ObjetParent = value;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Retourne la liste des Dossiers de Mail fils dans la hiérarchie
        /// </summary>
        [RelationFille(typeof(CDossierMail), "DossierMailParent")]
        [DynamicChilds("Child Mail Filders", typeof(CDossierMail))]
        public CListeObjetsDonnees DossiersMailFils
        {
            get
            {
                return ObjetsFils;
            }
        }


        //--------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des Mails contenus dans ce dossier
        /// </summary>
        [RelationFille(typeof(C2iMail), "Dossier")]
        [DynamicChilds("Mails", typeof(C2iMail))]
        public CListeObjetsDonnees Mails
        {
            get
            {
                return GetDependancesListe(C2iMail.c_nomTable, c_champId);
            }
        }
    }
}

