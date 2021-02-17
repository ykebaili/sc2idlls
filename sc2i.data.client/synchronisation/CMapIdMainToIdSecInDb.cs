using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.data;
using System.Data;

namespace sc2i.data.synchronisation
{
    /*OBSOLETE avec DBKey
    /// <summary>
    /// Gestion interne des mappages d'identifiants entre une base
    /// secondaire et une base primaire.
    /// 
    /// </summary>
    /// <remarks>
    /// Lors d'une synchronisation, les identifiants internes de certaines entités
    /// ne sont pas reprises directement depuis les identifiants principaux,
    /// mais sont modifié. Cette table conserve les mappings d'identifiants entre
    /// les deux bases. <B></B>
    /// Cette entité n'est utilisée que sur la base secondaire.
    /// </remarks>
    [Table(CMapIdMainToIdSecInDb.c_nomTable, new string[]{CMapIdMainToIdSecInDb.c_champId})]
    [DynamicClass("Synchronisation mapping")]
    [InsertAfterRelationTypeId]
    [ObjetServeurURI("CMapIdMainToIdSecInDbServeur")]

    public class CMapIdMainToIdSecInDb : CObjetDonneeAIdNumeriqueAuto, 
        IObjetSansVersion
    {
        public const string c_nomTable = "IDS_MAPPING_SYNC";

        public const string c_champId = "MAPSYNC_ID";
        public const string c_champNomTable = "MAPSYNC_TABLE_NAME";
        public const string c_champMainId = "MAPSYNC_MAINID";
        public const string c_champSecId = "MAPSYNC_SECID";

        //-----------------------------------------------------
        public CMapIdMainToIdSecInDb(CContexteDonnee ctx)
            : base(ctx)
        {
        }

        //-----------------------------------------------------
        public CMapIdMainToIdSecInDb(DataRow row)
            : base(row)
        {
        }

        //-----------------------------------------------------
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }


        //-----------------------------------------------------
        protected override void MyInitValeurDefaut()
        {
            
        }

        //-----------------------------------------------------
        public override string DescriptionElement
        {
            get { return "Internal synchronisation Id mapping"; }
        }

        //-----------------------------------------------------
        /// <summary>
        /// Nom de la table concernée
        /// </summary>
        [TableFieldProperty(c_champNomTable, 255)]
        [DynamicField("Table name")]
        public string NomTable
        {
            get
            {
                return (string)Row[c_champNomTable];
            }
            set
            {
                Row[c_champNomTable] = value;
            }
        }

        //-----------------------------------------------------
        /// <summary>
        /// Id de l'entité dans la base primaire
        /// </summary>
        [TableFieldProperty(c_champMainId)]
        [DynamicField("Main id")]
        public int IdInMain
        {
            get
            {
                return (int)Row[c_champMainId];
            }
            set
            {
                Row[c_champMainId] = value;
            }
        }

        //-----------------------------------------------------
        /// <summary>
        /// Id de l'entité dans la base secondaire
        /// </summary>
        [TableFieldProperty(c_champSecId)]
        [DynamicField("Secondary id")]
        public int IdInSecondary
        {
            get
            {
                return (int)Row[c_champSecId];
            }
            set
            {
                Row[c_champSecId] = value;
            }
        }

        //-----------------------------------------------------
        public static int? GetIdInSecondaire ( 
            CContexteDonnee ctx, 
            string strNomTable, 
            int nIdInMain,
            bool bAutoriserLectureInDb)
        {
            CMapIdMainToIdSecInDb map = new CMapIdMainToIdSecInDb(ctx);
            if ( map.ReadIfExists ( new CFiltreData ( 
                c_champNomTable+"=@1 and "+
                c_champMainId+"=@2",
                strNomTable, 
                nIdInMain),
                bAutoriserLectureInDb))
                return map.IdInSecondary;
            return null;
        }

        //-----------------------------------------------------
        public static int? GetIdInMain ( 
            CContexteDonnee ctx, 
            string strNomTable, 
            int nIdInSecondaire,
            bool bAutoriserLectureInDb)
        {
            CMapIdMainToIdSecInDb map = new CMapIdMainToIdSecInDb(ctx);
            if ( map.ReadIfExists(new CFiltreData ( 
                c_champNomTable+"=@1 and "+
                c_champSecId+"=@2",
                strNomTable,
                nIdInSecondaire ),
                bAutoriserLectureInDb))
                return map.IdInMain;
            return null;
        }


    }*/
}
