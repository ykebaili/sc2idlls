using System;
using System.Data;
using System.Collections;
using System.Linq;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.data.dynamic;
using System.Collections.Generic;
using sc2i.multitiers.client;
using System.Text;
using System.IO;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Un modèle d'affectation est une liste d'affectations possibles pour un élément affectable dans le système
	/// </summary>
    /// <remarks>
    /// Chaque modèle d'affectation contient une ou plusieurs formules définissant des ressources affectables
    /// à une entité
    /// </remarks>
	[Table(CModeleAffectationUtilisateurs.c_nomTable, CModeleAffectationUtilisateurs.c_champId, true )]
	[FullTableSync]
    [ObjetServeurURI("CModeleAffectationUtilisateursServeur")]
	[DynamicClass("Assignment template")]
	public class CModeleAffectationUtilisateurs : CObjetDonneeAIdNumeriqueAuto
	{
        public const string c_roleChampCustom = "RUN_WKF_STEP";

		public const string c_nomTable = "ASSGN_TEMPLATE";
		public const string c_champId = "ASGNTPL_ID";
        public const string c_champLibelle = "ASGNTPL_LABEL";
        public const string c_champDataAffectation = "ASGNTPL_DATA";

        public const string c_champCacheParametreAffectation = "ASGNTPL_CACHE_DATA";
        

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CModeleAffectationUtilisateurs( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CModeleAffectationUtilisateurs ( DataRow row )
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
				return I.T("Assignment template @1|20084",Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

        

        //-----------------------------------------------------------
        /// <summary>
        /// Libellé du modèle
        /// </summary>
        [TableFieldProperty(c_champLibelle, 1024)]
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





        
        //-------------------------------------------------------------------
        /// /////////////////////////////////////////////////////////
        [TableFieldProperty(c_champDataAffectation, NullAutorise = true)]
        public CDonneeBinaireInRow DataParametres
        {
            get
            {
                if (Row[c_champDataAffectation] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champDataAffectation);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataAffectation, donnee);
                }
                object obj = Row[c_champDataAffectation];
                return ((CDonneeBinaireInRow)obj).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champDataAffectation] = value;
            }
        }

        //-------------------------------------------------------------------
        [TableFieldProperty(c_champCacheParametreAffectation, IsInDb = false)]
        [NonCloneable]
        [BlobDecoder]
        public CParametresAffectationEtape ParametresAffectation
        {
            get
            {
                CParametresAffectationEtape parametres = Row[c_champCacheParametreAffectation] as CParametresAffectationEtape;
                if (parametres == null)
                {
                    CDonneeBinaireInRow donnee = DataParametres;
                    if (donnee != null && donnee.Donnees != null)
                    {
                        MemoryStream stream = new MemoryStream(donnee.Donnees);
                        BinaryReader reader = new BinaryReader(stream);
                        CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                        serializer.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                        CResultAErreur result = serializer.TraiteObject<CParametresAffectationEtape>(ref parametres);
                        serializer.DetacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                        reader.Close();
                        stream.Close();
                        stream.Dispose();
                    }
                    if (parametres == null)
                        parametres = new CParametresAffectationEtape();
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheParametreAffectation, parametres);
                }
                return parametres;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow donnee = DataParametres;
                    donnee.Donnees = null;
                    DataParametres = donnee;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CParametresAffectationEtape dessin = value;
                    CResultAErreur result = serializer.TraiteObject<CParametresAffectationEtape>(ref dessin, this);
                    if (result)
                    {
                        CDonneeBinaireInRow donnee = DataParametres;
                        donnee.Donnees = stream.GetBuffer();
                        DataParametres = donnee;
                    }
                    stream.Close();
                    writer.Close();
                    stream.Dispose();
                }
                CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheParametreAffectation, DBNull.Value);
            }
        }
    }
		
}
