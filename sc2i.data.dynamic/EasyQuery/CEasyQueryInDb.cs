using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.formulaire;
using futurocom.easyquery;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Une Easy query est un ensemble de requêtes préparametrées
    /// pouvant avoir différents sources de données
	/// </summary>
	[Table(CEasyQueryInDb.c_nomTable, CEasyQueryInDb.c_champId, true)]
	[FullTableSync]
	[ObjetServeurURI("CEasyQueryInDbServeur")]
	[DynamicClass("Stored easy query")]
	public class CEasyQueryInDb : 
        CObjetDonneeAIdNumeriqueAuto, 
        IObjetALectureTableComplete
	{
		public const string c_nomTable = "EASY_QUERY";

		public const string c_champId = "EQSU_ID";
		public const string c_champLibelle = "ESQU_LABEL";
		public const string c_champParamQuery = "ESQU_QUERY_DATA";
        public const string c_champConfigSource = "ESQU_SOURCE_DATA";
        

        public const string c_champCacheQuery = "CUF_CACHE_FORMULAIRE";

		/// ///////////////////////////////////////////////////////
		public CEasyQueryInDb( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CEasyQueryInDb ( DataRow row )
			:base ( row) 
		{
		}

		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The query @1|20061",Libelle);
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
		///<summary>Libellé (nom) de la requête</summary>
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


		


		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champParamQuery,NullAutorise=true)]
		public CDonneeBinaireInRow DataQuery
		{
			get
			{
				if ( Row[c_champParamQuery] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champParamQuery);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champParamQuery, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champParamQuery]).GetSafeForRow ( Row.Row );
			}
			set
			{
				Row[c_champParamQuery] = value;
			}
		}

        /// /////////////////////////////////////////////////////////////
        [TableFieldProperty(c_champConfigSource, NullAutorise = true)]
        public CDonneeBinaireInRow DataDeSource
        {
            get
            {
                if (Row[c_champConfigSource] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champConfigSource);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champConfigSource, donnee);
                }
                return ((CDonneeBinaireInRow)Row[c_champConfigSource]).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champConfigSource] = value;
            }
        }

        /// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CListeQuerySource QuerySources
        {
            get
            {
                CListeQuerySource lst = new CListeQuerySource();
                if ( DataDeSource != null && DataDeSource.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(DataDeSource.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    CResultAErreur result = serializer.TraiteObject<CListeQuerySource>(ref lst);
                    if (!result)
                    {
                        lst = new CListeQuerySource();
                    }
                    reader.Close();
                    stream.Close();
                }
                return lst;

            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow data = DataDeSource;
                    data.Donnees = null;
                    DataDeSource = data;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CListeQuerySource liste = value;
                    CResultAErreur result = serializer.TraiteObject<CListeQuerySource>(ref liste);
                    if (result)
                    {
                        CDonneeBinaireInRow data = DataDeSource;
                        data.Donnees = stream.GetBuffer();
                        DataDeSource = data;
                    }
                    writer.Close();
                    stream.Close();
                }
            }
        }


		/// <summary>
		/// Retourne l'objet EasyQuery executable parametré dans la requête stockée
		/// </summary>
        [DynamicField("Runnable Query")]
        [TableFieldProperty(c_champCacheQuery, IsInDb=false, NullAutorise=true)]
        [BlobDecoder]
        public CEasyQuery EasyQueryAvecSources
		{
			get
			{
                CEasyQuery query = null;
                if (Row[c_champCacheQuery] is CEasyQuery)
                {
                    query = (CEasyQuery)Row[c_champCacheQuery];
                    query.IContexteDonnee = ContexteDonnee;
                    query.Sources = QuerySources.Sources;
                    return query;
                }
                query = new CEasyQuery();
				if ( DataQuery.Donnees != null )
				{
                    MemoryStream stream = new MemoryStream(DataQuery.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    CResultAErreur result = serializer.TraiteObject<CEasyQuery>(ref query);
					if ( !result )
					{
                        query = new CEasyQuery();
					}
                    reader.Close();
                    stream.Close();
				}
                query.Sources = QuerySources.Sources;
                query.IContexteDonnee = ContexteDonnee;
                CContexteDonnee.ChangeRowSansDetectionModification(
                    Row.Row,
                    c_champCacheQuery, query);
				return query;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataQuery;
					data.Donnees = null;
                    DataQuery = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CEasyQuery query = value;
                    CResultAErreur result = serializer.TraiteObject<CEasyQuery>(ref query);
					if ( result )
					{
                        CDonneeBinaireInRow data = DataQuery;
						data.Donnees = stream.GetBuffer();
                        DataQuery = data;
                        QuerySources = value.ListeSources;
					}

                    writer.Close();
                    stream.Close();
				}
                Row[c_champCacheQuery] = DBNull.Value;
			}
		}
	
	}
}
