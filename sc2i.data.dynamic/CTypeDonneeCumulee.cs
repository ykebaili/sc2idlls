using System;
using System.Data;
using System.IO;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.DonneeCumulee;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Contient les paramètres de calcul de données cumulée
	/// </summary>
	/// <remarks>
	/// <P>
	/// Une donnée cumulée correspond au stockage du résultat d'une requête 
	/// SQL dans la base de données<BR></BR>
	/// Chaque type de donnée cumulée est identifiée par un code unique qui détermine la nature
	/// de la données<BR></BR>
	/// La requête SQL permettant de retourner la donnée retourne normallement plusieurs
	/// champs. Certains de ces champs sont des axes de cumul (groupements) d'autres
	/// des valeurs (sommes, nombre, moyenne, ...).<BR></BR>
	/// Une donnée cumulée peut être identifiée par 10 clés et contenir 80 valeurs de
	/// cumul.
	/// </P>
	/// </remarks>
	[Table(CTypeDonneeCumulee.c_nomTable, CTypeDonneeCumulee.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CTypeDonneeCumuleeServeur")]
	[DynamicClass("Precalculated data type")]
	public class CTypeDonneeCumulee : CObjetDonneeAIdNumeriqueAuto, ITypeDonneeCumulee
	{
		public const string c_nomTable = "CUMULATED_DATA_TYPE";
		public const string c_champId = "CUMDATTYP_ID";
		public const string c_champLibelle = "CUMDATTYP_LABEL";
		public const string c_champCode = "CUMDATTYP_CODE";
		public const string c_champDescription = "CUMDATTYP_DESCRIPTION";
		public const string c_champParametre = "CUMDATTYP_SETTING";

        public const string c_champCacheParametre = "CUMDATTYP_SETTING_CACHE";

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CTypeDonneeCumulee( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CTypeDonneeCumulee ( DataRow row )
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
				return I.T("The Precalculated data type @1|30002", Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Libellé du type de donnée cumulée
        ///</summary>
        [TableFieldPropertyAttribute(c_champLibelle, 128)]
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

		/// //////////////////////////////////////////////////
		///<summary>
        ///Description du type de donnée cumulée
        ///</summary>
        ///<remarks>
        ///La description peut être utilisée par l'administrateur pour commenter l'utilisation de ce type de donnée cumulée.
        /// </remarks>
        [TableFieldProperty(c_champDescription, 256)]
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

		/// //////////////////////////////////////////////////
		///<summary>
        ///Code du type de données. Ce code doit être unique
        /// </summary>
        /// <remarks>
        /// Le code d'un type de donnée est utile pour récupérer des valeurs de données cumulées sur une entité
        /// lors de l'utilisation de la formule GetCumulatedData
        /// </remarks>
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

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champParametre,NullAutorise=true)]
		public CDonneeBinaireInRow DataParametre
		{
			get
			{
				if ( Row[c_champParametre] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champParametre);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champParametre, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champParametre]).GetSafeForRow ( Row.Row );
			}
			set
			{
				Row[c_champParametre] = value;
			}
		}

        /// /////////////////////////////////////////////////////////////
		[DynamicField("Parameter")]
        [TableFieldProperty(c_champCacheParametre, IsInDb = false, NullAutorise = true)]
        [BlobDecoder]
        public CParametreDonneeCumulee Parametre
		{
			get
			{
                if (Row[c_champCacheParametre] != DBNull.Value)
                {
                    return (CParametreDonneeCumulee)Row[c_champCacheParametre];
                }
				CParametreDonneeCumulee parametre = new CParametreDonneeCumulee(ContexteDonnee);
				if ( DataParametre.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(DataParametre.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					serializer.AttacheObjet ( typeof(CContexteDonnee), ContexteDonnee );
					CResultAErreur result = parametre.Serialize(serializer);
					serializer.DetacheObjet ( typeof(CContexteDonnee), ContexteDonnee );
					if ( !result )
					{
						parametre = new CParametreDonneeCumulee ( ContexteDonnee );
					}

                    reader.Close();
                    stream.Close();
				}
                CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheParametre, parametre);
				return parametre;
			}
			set
			{
				if ( value == null )
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
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
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

		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Force le système à recalculer les données cumulées de ce type
        /// </summary>
        [DynamicMethod("Force the system to recaclulate data")]
		public void Calculate()
		{
			StockResultat(null);
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur StockResultat ( IIndicateurProgression indicateur )
		{
			ITypeDonneeCumuleeServeur objServeur = (ITypeDonneeCumuleeServeur)GetLoader();
			return objServeur.StockeResultat ( Id, indicateur );
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// Crée un attribut relation décrivant la relation qui lie la donnée cumulée au type demandé
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public RelationAttribute GetRelationAttributeToType ( Type tp )
		{
			int nCle = 0;
			foreach ( CCleDonneeCumulee cle in Parametre.ChampsCle )
			{
				if ( cle.TypeLie != null && cle.TypeLie == tp )
				{
					CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( tp, new object[]{ContexteDonnee} );
					RelationAttribute attr = new RelationAttribute ( 
						obj.GetNomTable(),
						obj.GetChampId(),
						CDonneeCumulee.GetNomChampCle(nCle),
						false, 
						true,
						false );
					return attr;
				}
				nCle++;
			}
			return null;
		}


		/// /////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDonneesCumuleesForObjet ( CObjetDonneeAIdNumerique objet )
		{
			//Trouve le champ du paramètre qui est lié au type de l'objet lié
			CParametreDonneeCumulee parametre = Parametre;
			int nCle = 0;
			string strChampCle = "";
			foreach ( CCleDonneeCumulee cle in parametre.ChampsCle )
			{
				if ( cle.TypeLie != null && cle.TypeLie == objet.GetType() )
				{
					strChampCle = CDonneeCumulee.GetNomChampCle(nCle);
					break;
				}
				else
					nCle++;
			}
			CListeObjetsDonnees listeVide = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CDonneeCumulee));
			listeVide.Filtre = new CFiltreDataImpossible();
			if ( strChampCle == "" )
				return listeVide;

			CFiltreData filtre = new CFiltreData ( 
				CTypeDonneeCumulee.c_champId+"=@1 and "+
				strChampCle+"=@2",
				this.Id,
				objet.Id.ToString() );
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( objet.ContexteDonnee, typeof(CDonneeCumulee) );
			liste.Filtre = filtre;
			return liste;
		}


        //--------------------------------------------------
        public string GetNomCle(int nNum)
        {
            if (nNum >= 0 && nNum < Parametre.ChampsCle.Length)
                return Parametre.ChampsCle[nNum].Champ;
            return null;
        }

        //--------------------------------------------------
        public string GetNomValeur(int nNum)
        {
            if (nNum >= 0 && nNum <= Parametre.NomChampsDecimaux.Length)
            {
                CParametreDonneeCumulee.CNomChampCumule nom = Parametre.NomChampsDecimaux[nNum];
                return nom != null ? nom.NomChamp : null;
            }
            return null;
        }

        //--------------------------------------------------
        public string GetNomDate(int nNum)
        {
            if (nNum >= 0 && nNum <= Parametre.NomChampsDates.Length)
            {
                CParametreDonneeCumulee.CNomChampCumule nom = Parametre.NomChampsDates[nNum];
                return nom != null ? nom.NomChamp : null;
            }
            return null;
        }

        //--------------------------------------------------
        public string GetNomString(int nNum)
        {
            if (nNum >= 0 && nNum <= Parametre.NomChampsTextes.Length)
            {
                CParametreDonneeCumulee.CNomChampCumule nom = Parametre.NomChampsTextes[nNum];
                return nom != null ? nom.NomChamp : null;
            }
            return null;
        }

        //--------------------------------------------------
        public string GetNomChamp(CChampDonneeCumulee champ)
        {
            switch (champ.TypeChamp)
            {
                case ETypeChampDonneeCumulee.Cle:
                    return GetNomCle(champ.NumeroChamp);
                case ETypeChampDonneeCumulee.Decimal:
                    return GetNomValeur(champ.NumeroChamp);
                case ETypeChampDonneeCumulee.Date:
                    return GetNomDate(champ.NumeroChamp);
                case ETypeChampDonneeCumulee.Texte:
                    return GetNomString(champ.NumeroChamp);
            }
            return null;
        }

        //--------------------------------------------------
        public int GetNbMaxFields(ETypeChampDonneeCumulee typeChamp)
        {
            switch (typeChamp)
            {
                case ETypeChampDonneeCumulee.Cle:
                    return 10;
                case ETypeChampDonneeCumulee.Decimal:
                    return 60;
                case ETypeChampDonneeCumulee.Date:
                    return 40;
                case ETypeChampDonneeCumulee.Texte:
                    return 40;
            }
            return 0;
        }

        //--------------------------------------------------
        /// <summary>
        /// retourne la liste des champs qui ont un nom
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CChampDonneeCumulee> GetChampsRenseignes()
        {
            List<CChampDonneeCumulee> lstChamps = new List<CChampDonneeCumulee>();
            CParametreDonneeCumulee parametre = Parametre;
            int nIndex = 0;
            foreach (CCleDonneeCumulee cle in parametre.ChampsCle)
            {
                if (cle.Champ.Length > 0)
                {
                    CChampDonneeCumulee champ = new CChampDonneeCumulee();
                    champ.NumeroChamp = nIndex;
                    champ.TypeChamp = ETypeChampDonneeCumulee.Cle;
                    lstChamps.Add(champ);
                }
                nIndex++;
            }
            nIndex = 0;
            foreach (CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsDecimaux)
            {
                if (nom.NomChamp.Length > 0)
                {
                    CChampDonneeCumulee champ = new CChampDonneeCumulee();
                    champ.NumeroChamp = nIndex;
                    champ.TypeChamp = ETypeChampDonneeCumulee.Decimal;
                    lstChamps.Add(champ);
                }
                nIndex++;
            }
            nIndex = 0;
            foreach (CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsDates)
            {
                if (nom.NomChamp.Length > 0)
                {
                    CChampDonneeCumulee champ = new CChampDonneeCumulee();
                    champ.NumeroChamp = nIndex;
                    champ.TypeChamp = ETypeChampDonneeCumulee.Date;
                    lstChamps.Add(champ);
                }
                nIndex++;
            }
            nIndex = 0;
            foreach (CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsTextes)
            {
                if (nom.NomChamp.Length > 0)
                {
                    CChampDonneeCumulee champ = new CChampDonneeCumulee();
                    champ.NumeroChamp = nIndex;
                    champ.TypeChamp = ETypeChampDonneeCumulee.Texte;
                    lstChamps.Add(champ);
                }
                nIndex++;
            }
            return lstChamps.AsReadOnly();
                
        }

    }

	/// /////////////////////////////////////////////////////////////
	public interface ITypeDonneeCumuleeServeur
	{
		CResultAErreur StockeResultat ( int nIdTypeDonnee, IIndicateurProgression indicateur );
	}		

}
