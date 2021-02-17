using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
    ///<summary>
    ///Une version de donn�es contient les donn�es d'un 'calque' appliqu� � la base de donn�es
    ///</summary>
    ///<remarks>
    ///Il existe trois version de donn�es diff�rentes. Chacun est utilis�es pour stocker des donn�es modifi�es par les utilisateurs.
    ///<H2>Version pr�visionnelles (type 10)</H2>
    ///Lorsqu'un utilisateur travaille en mode pr�visionnel, les modifications qu'il r�alise sur la base
    ///ne sont pas r�alis�es sur la version r�f�rentiel, mais sur une version de donn�es (DataVersion) sp�cifique.
    ///<BR></BR>
    ///Chaque version de donn�es contient les informations sur l'utilisateur qui a r�alis� la modification, ainsi que la
    ///liste des modifications r�alis�es.
    ///<H2>Version d'archive (type 0)</H2>
    ///Lorsque le module de gestion d'historique est activ�, toutes les modifications faites sur la base de donn�es
    ///sont historis�es dans des versions de donn�e. Chaque lors d'enregistrement de donn�es correspond � une version 'archive'
    ///d�di�e.<BR></BR>
    ///Chaque version archive contient les informations sur l'utilisateur qui a r�alis� la modification ainsi que 
    ///la date des modifications. 
    ///<H2>Version Etiquette (type 20)</H2>
    ///Une version �tiquette est simplement un 'tag' stock� dans la liste des version, pour rep�rer un �tat particulier de la
    ///base de donn�es. Les versions etiquette sont gener�s par des process.
    ///</remarks>
	//-------------------------------------
	//-------------------------------------
	//-------------------------------------
	[ObjetServeurURI("CVersionDonneesServeur")]
	[Table(CVersionDonnees.c_nomTable, CVersionDonnees.c_champId, true)]
	[DynamicClass("Data version")]
	[Evenement(
		CVersionDonnees.c_eventBeforeAppliquer,
		"Before apply",
		"Is launched before apply modificiations")]
	[Evenement(
		CVersionDonnees.c_eventBeforeUtiliser,
		"Before use",
		"Is launched before use a data version")]
    [NoIdUniversel]
	public class CVersionDonnees : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
	{
		public const string c_eventBeforeAppliquer = "DV_BEFORE_APPLY";
		public const string c_eventBeforeUtiliser = "DV_BEFORE_USE";
		public const string c_nomTable = "DATA_VERSION";

		public const string c_champId = "DV_ID";
		public const string c_champLibelle = "DV_LABEL";
		public const string c_champDate = "DV_DATE";
		public const string c_champTypeVersion = "DV_VERSION_TYPE";
        public const string c_champDbKeyUtilisateur = "DV_USER_KEY";
		public const string c_champIdVersionParente = "DV_PARENT_VERSION_ID";

		public CVersionDonnees(CContexteDonnee contexte)
			: base(contexte)
		{
		}

		//-*------------------------------------
		public CVersionDonnees(DataRow row)
			: base(row)
		{
		}

		//-*------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] { c_champId };
		}

		//-------------------------------------
		protected override void MyInitValeurDefaut()
		{
			CodeTypeVersion = (int)CTypeVersion.TypeVersion.Archive;
			CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
			if (session != null && session.GetInfoUtilisateur() != null)
			{
                DbKeyUtilisateur = session.GetInfoUtilisateur().KeyUtilisateur;
			}
			Date = DateTime.Now;
		}

		//-------------------------------------
		public override string DescriptionElement
		{
			get { return I.T("Data version @1|175", Libelle); }
		}

		//-------------------------------------
        /// <summary>
        /// Libell� de la version de donn�es
        /// </summary>
		[TableFieldProperty(CVersionDonnees.c_champLibelle, 255)]
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

		//-------------------------------------
        /// <summary>
        /// Date de cr�ation de la version. Pour une version historique, il s'agit de la date des modifications. Pour une version 
        /// pr�visionnelle, il s'agit de la date de cr�ation de la version pr�visionnelle.
        /// </summary>
		[TableFieldProperty(CVersionDonnees.c_champDate)]
		[DynamicField("Date")]
		public DateTime Date
		{
			get
			{
				return (DateTime)Row[c_champDate];
			}
			set
			{
				Row[c_champDate] = value;
			}
		}

		//-------------------------------------

		//-------------------------------------------------------------------
		/// <summary>
		/// Cat�gorie de la version de donn�es
		/// </summary>
        /// <remarks>
        /// Les cat�gories de version de donn�es sont parametr�es par l'administrateur de l'application
        /// et permettent d'ordonner les versions de donn�es
        /// </remarks>
		[Relation(
			CCategorieVersionDonnees.c_nomTable,
			CCategorieVersionDonnees.c_champId,
			CCategorieVersionDonnees.c_champId,
			false,
			false,
			true)]
		[DynamicField("Version category")]
		public CCategorieVersionDonnees CategorieDeVersion
		{
			get
			{
				return (CCategorieVersionDonnees)GetParent(CCategorieVersionDonnees.c_champId, typeof(CCategorieVersionDonnees));
			}
			set
			{
				SetParent(CCategorieVersionDonnees.c_champId, value);
			}
		}

	




		//---------------------------------------------
		/// <summary>
		/// Liste des objets modifi�s dans la version
		/// </summary>
		[RelationFille(typeof(CVersionDonneesObjet), "VersionDonnees")]
		[DynamicChilds("Object versions", typeof(CVersionDonneesObjet))]
		public CListeObjetsDonnees VersionsObjets
		{
			get
			{
				return GetDependancesListeProgressive(CVersionDonneesObjet.c_nomTable, c_champId);
			}
		}

        //---------------------------------------------
        public CListeObjetsDonnees VersionObjetsEnLectureNonProgressive
        {
            get
            {
                return GetDependancesListe(CVersionDonneesObjet.c_nomTable, c_champId);
            }
        }

		//---------------------------------------------

		//-------------------------------------------------------------------
		/// <summary>
		/// Indique la version sur laquelle s'appuie cette version pr�visionnelle.
		/// <BR></BR>
		/// La version parente est nulle pour une version pr�visionnelle s'appuyant sur 
		/// le r�f�rentiel. Pour une version pr�visionnelle bas�e sur une autre version pr�visionnelle,
        /// il s'agit de la version sur laquelle est bas�e la version pr�visionnelle.
		/// </summary>
		[Relation(
			CVersionDonnees.c_nomTable,
			CVersionDonnees.c_champId,
			CVersionDonnees.c_champIdVersionParente,
			false,
			false,
			false)]
		[DynamicField("Parent version")]
		public CVersionDonnees VersionParente
		{
			get
			{
				return (CVersionDonnees)GetParent(CVersionDonnees.c_champIdVersionParente, typeof(CVersionDonnees));
			}
			set
			{
				SetParent(CVersionDonnees.c_champIdVersionParente, value);
			}
		}


		//---------------------------------------------
		/// <summary>
		/// Liste des version s'appuyant sur cette version
		/// </summary>
		[RelationFille(typeof(CVersionDonnees), "VersionParente")]
		[DynamicChilds("ChildVersions", typeof(CVersionDonnees))]
		public CListeObjetsDonnees VersionsFilles
		{
			get
			{
				return GetDependancesListe(CVersionDonnees.c_nomTable, CVersionDonnees.c_champIdVersionParente);
			}
		}

	

		//-------------------------------------------------------------------------
		/// <summary>
		/// Indique l'identifiant (id interne) de l'utilisateur qui a r�alis� cette version
		/// </summary>
		[TableFieldProperty(CVersionDonnees.c_champDbKeyUtilisateur, 64, NullAutorise = true)]
		[ReplaceField("IdUtilisateur", "User id")]
        [DynamicField("User Key string")]
		public string DbKeyUtilisateurString
		{
			get
			{
                return (string)Row[c_champDbKeyUtilisateur, true];
			}
			set
			{
                Row[c_champDbKeyUtilisateur] = value;
			}
		}

        //------------------------------------------------------------------------
        [DynamicField("User key")]
        public CDbKey DbKeyUtilisateur
        {
            get
            {
                return CDbKey.CreateFromStringValue(DbKeyUtilisateurString);
            }
            set
            {
                if (value != null)
                    DbKeyUtilisateurString = value.StringValue;
                else
                    DbKeyUtilisateurString = "";
            }
        }

		/// <summary>
		/// Code du type de version :<BR></BR>
		/// 0  : Archive<BR></BR>
        /// 10 : Pr�visionnelle<BR></BR>
        /// 20 : Etiquette
		/// </summary>
		[TableFieldProperty(CVersionDonnees.c_champTypeVersion)]
		[DynamicField("Version Type code")]
		public int CodeTypeVersion
		{
			get
			{
				return (int)Row[c_champTypeVersion];
			}
			set
			{
				Row[c_champTypeVersion] = value;
			}
		}

		//-------------------------------------
		[DynamicField("Version type")]
		public CTypeVersion TypeVersion
		{
			get
			{
				return new CTypeVersion((CTypeVersion.TypeVersion)CodeTypeVersion);
			}
			set
			{
				if (value != null)
					CodeTypeVersion = value.CodeInt;
			}
		}

		//-------------------------------------
		/// <summary>
		/// Retourne toutes les valeurs qui ont �t� modifi�es
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public CValeursHistorique GetValeursModifiees(CObjetDonneeAIdNumerique objet, CVersionDonnees versionButoire)
		{
			CListeObjetsDonnees listeVersions = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CVersionDonneesObjet));
			CValeursHistorique listeValeurs = new CValeursHistorique();
			if (TypeVersion.Code != CTypeVersion.TypeVersion.Previsionnelle)
			{
				listeVersions.Filtre = new CFiltreDataAvance(
					CVersionDonneesObjet.c_nomTable,
					CVersionDonneesObjet.c_champIdElement + "=@1 and " +
					CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
					CVersionDonnees.c_nomTable + "." +
					CVersionDonnees.c_champTypeVersion + "=@3 and " +
					CVersionDonnees.c_champId + ">=@4",
					objet.Id,
					objet.GetType().ToString(),
					(int)CTypeVersion.TypeVersion.Archive,
					Id);
				listeVersions.Tri = CVersionDonneesObjet.c_champId + " desc";
			}
			else
			{
				CVersionDonnees versionDerivee = this;
				if ( versionButoire != null )
				{
					if ( versionButoire.Id > versionDerivee.Id )
					{
						CVersionDonnees versionTmp = versionButoire;
						versionButoire = versionDerivee;
						versionDerivee = versionTmp;
					}
				}
				StringBuilder blIds = new StringBuilder();
				blIds.Append(versionDerivee.Id);
				CVersionDonnees versionParente = versionDerivee.VersionParente;
				while (VersionParente != null && versionButoire != null && !versionParente.Equals ( versionButoire) )
				{
					blIds.Append(",");
					blIds.Append(versionParente.Id);
					versionParente = versionParente.VersionParente;
				}
				listeVersions.Filtre = new CFiltreData(
					CVersionDonnees.c_champId + " in (" + blIds + ") and " +
					CVersionDonneesObjet.c_champIdElement + "=@1 and " +
					CVersionDonneesObjet.c_champTypeElement + "=@2",
					objet.Id,
					objet.GetType().ToString());
				listeVersions.Tri = CVersionDonnees.c_champId+" asc";
			}
				
			foreach (CVersionDonneesObjet version in listeVersions)
			{
				foreach (CVersionDonneesObjetOperation data in version.Modifications)
				{
					IChampPourVersion champ = data.Champ;
					if (champ != null)
						listeValeurs[champ] = data.GetValeur();
				}
			}
			return listeValeurs;
		}

		

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des versions � lire pour lire une version,
		/// il s'agit des version pr�c�dentes et de la version elle m�me
		/// </summary>
		/// <param name="nIdVersion"></param>
		/// <returns></returns>
		public static int[] GetVersionsToRead(int nIdSession, int nIdVersion)
		{
			List<int> idsVersions = new List<int>();
			int? nIdToRead = nIdVersion;
			while (nIdToRead != null)
			{
				idsVersions.Add((int)nIdToRead);
				C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
				requete.TableInterrogee = CVersionDonnees.c_nomTable;
				requete.FiltreAAppliquer = new CFiltreData(CVersionDonnees.c_champId + "=@1",
					nIdToRead);
				requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
				requete.ListeChamps.Add(new C2iChampDeRequete("Id",
					new CSourceDeChampDeRequete(CVersionDonnees.c_champIdVersionParente),
					typeof(int),
					OperationsAgregation.None,
					true));
				CResultAErreur result = requete.ExecuteRequete(nIdSession);
				if (result)
				{
					object val = ((DataTable)result.Data).Rows[0][0];
					if (val == DBNull.Value)
						nIdToRead = null;
					else
						nIdToRead = (int)val;
				}
				else
					nIdToRead = null;
			}
			return idsVersions.ToArray();
		}

		//-------------------------------------
		public int[] IdsVersionsDependantes
		{
			get
			{
				List<int> idsVersions = new List<int>();
				StringBuilder bl = new StringBuilder();
				bl.Append(Id);
				while (bl.Length > 0)
				{
					C2iRequeteAvancee requete = new C2iRequeteAvancee(null);
					requete.TableInterrogee = CVersionDonnees.c_nomTable;
					requete.FiltreAAppliquer = new CFiltreData(CVersionDonnees.c_champIdVersionParente + " in (" + bl.ToString() + ")");
					requete.ListeChamps.Add(new C2iChampDeRequete(c_champId,
					new CSourceDeChampDeRequete(c_champId),
					typeof(int),
					OperationsAgregation.None,
					true));
					CResultAErreur result = requete.ExecuteRequete(ContexteDonnee.IdSession);
					bl = new StringBuilder();
					if (result)
					{
						foreach (DataRow row in ((DataTable)result.Data).Rows)
						{
							bl.Append(row[0]);
							bl.Append(',');
							idsVersions.Add((int)row[0]);
						}
					}
				}
				return idsVersions.ToArray();
			}
		}

		//-------------------------------------
		/// <summary>
		/// Retourne tous les fils qui �taient affect� � l'objet � l'�poque
		/// </summary>
		/// <param name="objet"></param>
		/// <param name="relationFille"></param>
		/// <returns></returns>
		public int[] GetIdsChildsHistoriques(CObjetDonneeAIdNumerique objet, CInfoRelation relationFille)
		{
			Type tpFils = CContexteDonnee.GetTypeForTable(relationFille.TableFille);
			if (!typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpFils))
				return new int[0];
			CChampPourVersionInDb champFille = new CChampPourVersionInDb ( relationFille.ChampsFille[0], "");
			//Recherche toutes les modifications de la propri�t� fille
			CListeObjetsDonnees listeModifsFilles = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CVersionDonneesObjetOperation));
			listeModifsFilles.Filtre = new CFiltreDataAvance(
				CVersionDonneesObjetOperation.c_nomTable,
				CVersionDonneesObjetOperation.c_champChamp + "=@1 and " +
				CVersionDonneesObjetOperation.c_champTypeChamp + "=@2 and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonneesObjet.c_champTypeElement + "=@3 and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonnees.c_nomTable + "." +
				CVersionDonnees.c_champTypeVersion + "=@4 and " +
				CVersionDonneesObjet.c_nomTable + "." +
				CVersionDonnees.c_champId + ">=@5",
				champFille.FieldKey,
				champFille.TypeChampString,
				tpFils.ToString(),
				(int)CTypeVersion.TypeVersion.Archive,
				Id);
			listeModifsFilles.Tri = CVersionDonneesObjetOperation.c_champId + " desc";
			listeModifsFilles.ReadDependances("VersionObjet");
			
			ArrayList lstFils = new ArrayList();
			
			//Remplit la liste des fils avec les fils actuels
			//R�cup�re la liste de tous les fils actuels
			//et supprim�s depuis. En effet, un element supprim� depuis cette version mais qui est
			//toujours li� � l'objet �tait donc li� � l'objet � l'�poque
			C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
			requete.TableInterrogee = relationFille.TableFille;
			C2iChampDeRequete champDeRequete = new C2iChampDeRequete("ID",
				new CSourceDeChampDeRequete(objet.ContexteDonnee.GetTableSafe(relationFille.TableFille).PrimaryKey[0].ColumnName),
				typeof(int),
				OperationsAgregation.None,
				true);
			requete.ListeChamps.Add(champDeRequete);
			CFiltreData filtre = new CFiltreData(relationFille.ChampsFille[0] + "=@1 and (" +
				CSc2iDataConst.c_champIdVersion + " is null or "+
				CSc2iDataConst.c_champIdVersion + ">=@2)",
				objet.Id,
				Id);
			filtre.IgnorerVersionDeContexte = true;
			requete.FiltreAAppliquer = filtre;
			CResultAErreur result = requete.ExecuteRequete(objet.ContexteDonnee.IdSession);
			if (result)
			{
				foreach (DataRow row in ((DataTable)result.Data).Rows)
					lstFils.Add (row[0]);
			}
			
			
			foreach (CVersionDonneesObjetOperation data in listeModifsFilles)
			{
				if (data.GetValeur() is int && (int)data.GetValeur() == objet.Id)
				{
					if (!lstFils.Contains(data.VersionObjet.IdElement))
						lstFils.Add(data.VersionObjet.IdElement);
				}
				else
				{
					lstFils.Remove(data.VersionObjet.IdElement);
				}
			}

			//Toutes les entit�s cr��es apr�s la version ont �galement �t� ajout�es,
			//Donc n'y �taient pas
			if (lstFils.Count > 0)
			{
				StringBuilder builder = new StringBuilder();
				foreach (int nId in lstFils)
				{
					builder.Append(nId.ToString());
					builder.Append(",");
				}
				string strIds = builder.ToString();
				strIds = strIds.Substring(0, strIds.Length - 1);
				requete = new C2iRequeteAvancee(-1);
				requete.TableInterrogee = CVersionDonneesObjet.c_nomTable;
				requete.FiltreAAppliquer = new CFiltreData(
				CVersionDonneesObjet.c_champIdElement + " in (" + strIds + ") and " +
				CVersionDonneesObjet.c_champTypeElement + "=@1 and " +
				CVersionDonneesObjet.c_champTypeOperation + "=@2 and " +
				CVersionDonnees.c_champId + ">=@3",
				tpFils.ToString(),
				CTypeOperationSurObjet.TypeOperation.Ajout,
				Id);
				requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
				requete.ListeChamps.Add(new C2iChampDeRequete("Id",
					new CSourceDeChampDeRequete(CVersionDonneesObjet.c_champIdElement),
					typeof(int),
					OperationsAgregation.None,
					true));
				result = requete.ExecuteRequete(ContexteDonnee.IdSession);
				if (result)
				{
					foreach (DataRow row in ((DataTable)result.Data).Rows)
						lstFils.Remove(row[0]);
				}
			}
			return (int[])lstFils.ToArray(typeof(int));
		}


		///Stef 30/07/08 : optim : conserve le dernier �l�ment
		/// retourn� par GetVersionObjetAvecCreation.
		private CVersionDonneesObjet m_lastVersionObjet = null;
		private DataRow m_lastRow = null;
		private class CLockerOptim { }
		/// <summary>
		/// Retourne les informations de modification pour l'objet demand�
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public CVersionDonneesObjet GetVersionObjetAvecCreation(DataRow row)
		{
			lock (typeof(CLockerOptim))
			{
				if (m_lastRow != null && row.Equals(m_lastRow) && m_lastVersionObjet != null)
					return m_lastVersionObjet;
			}
			CListeObjetsDonnees liste = VersionsObjets;
			Type typeElement = CContexteDonnee.GetTypeForTable(row.Table.TableName);
			if (typeElement == null || !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeElement))
				return null;
			DataRowVersion versionToReturn = DataRowVersion.Current;
			if (row.RowState == DataRowState.Deleted)
				versionToReturn = DataRowVersion.Original;
			int nIdElement = (int)row[row.Table.PrimaryKey[0], versionToReturn];
			liste.Filtre = new CFiltreData(CVersionDonneesObjet.c_champTypeElement + "=@1 and " +
				CVersionDonneesObjet.c_champIdElement + "=@2",
				typeElement.ToString(),
				nIdElement);
			liste.InterditLectureInDB = true;
			CVersionDonneesObjet valeurDeRetour = null;
			if (liste.Count != 0)
				valeurDeRetour = (CVersionDonneesObjet)liste[0];
			if (valeurDeRetour == null)
			{
				CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(ContexteDonnee);
				versionObjet.CreateNewInCurrentContexte();
				versionObjet.StringTypeElement = typeElement.ToString();
				versionObjet.IdElement = nIdElement;
				versionObjet.VersionDonnees = this;
				switch (row.RowState)
				{
					case DataRowState.Added:
						versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Ajout;
						break;
					case DataRowState.Modified:
						versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
						break;
					case DataRowState.Deleted:
						versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Suppression;
						break;
					case DataRowState.Unchanged:
						versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Aucune;
						break;
				}
				valeurDeRetour = versionObjet;
			}
			lock (typeof(CLockerOptim))
			{
				m_lastVersionObjet = valeurDeRetour;
				m_lastRow = row;
			}
			return valeurDeRetour;
		}

        //---------------------------------------------------------
        /// <summary>
        /// Cette m�thode ne peut �tre appell�e que sur les versions pr�visionnelles. Elle applique les modification de la version sur le r�f�rentiel
        /// ou sur la version sur laquelle elle est bas�e.
        /// </summary>
        /// <returns></returns>
		[DynamicMethod("Apply", "Apply this version")]
		public bool ApplyModifications()
		{
			return AppliqueModifications().Result;
		}

		//----------------------------------------------------
		public CResultAErreur AppliqueModifications()
		{
            return AppliqueModifications(null);
        }

        //----------------------------------------------------
        public CResultAErreur AppliqueModifications ( IIndicateurProgression indicateur )
        {
			IVersionDonneesServeur serveur = (IVersionDonneesServeur)GetLoader();
			return serveur.AppliqueModificationsPrevisionnelles(Id, indicateur);
		}
		
		//----------------------------------------------------
        /// <summary>
        /// Sur une version pr�visionnelle, annule les modification r�alis�es dans la version.
        /// </summary>
        /// <returns></returns>
		[DynamicMethod("Cancel all", "Cancel all modifications for this version")]
		public bool CancelModifications()
		{
			return AnnulerModifications().Result;
		}
		//----------------------------------------------------
		public CResultAErreur AnnulerModifications()
		{
			IVersionDonneesServeur serveur = (IVersionDonneesServeur)GetLoader();
			return serveur.AnnulerModificationsPrevisionnelles(Id);
		}

		//----------------------------------------------------
		/// <summary>
		/// Le data du result contient l'id de la nouvelle pr�visionnelle
		/// </summary>
		/// <returns></returns>
		public CResultAErreur CreatePrevisionnelleFromArchive()
		{
			IVersionDonneesServeur serveur = (IVersionDonneesServeur)GetLoader();
			return serveur.CreatePrevisionnelleFromArchive(Id);
		}

		public static CResultAErreur Purger(DateTime dateLimite, int nIdSession)
		{
			IVersionDonneesServeur serveur = (IVersionDonneesServeur)CContexteDonnee.GetTableLoader(c_nomTable, null, nIdSession);
			return serveur.PurgerHistorique(dateLimite);
		}

		/// <summary>
		/// Retourne vrai s'il est possible d'utiliser cette version.
		/// Cette fonction d�clenche L'evenement "Before use"
		/// </summary>
		/// <returns></returns>
		public CResultAErreur CanUse()
		{
			CResultAErreur result = CResultAErreur.True;
			if (ContexteDonnee.IsEnEdition)
			{
				result.EmpileErreur(I.T("Can not use a version while it is in edition mode|198"));
				return result;
			}
			return EnregistreEvenement(c_eventBeforeUtiliser, true);
		}

	}



}
