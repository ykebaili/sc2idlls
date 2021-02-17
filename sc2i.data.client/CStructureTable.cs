using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.multitiers.client;
using System.Collections.Generic;


namespace sc2i.data
{
	///Optimisation Stef, le 22/07/08
	///Les structures qui ont besoin  de structures parentes ou enfant
	///créent des structures vides, qui se remplissent sur demande.
	[Serializable]
	public class CStructureTable
	{
		//Type->CStructureTable
		private static Hashtable m_tableTypeToStructure = new Hashtable();

		private static double m_fNbMillisecStructures = 0;

		/// <summary>
		/// Indique que les relations de cette structure sont initialisées ou pas
		/// </summary>
		private bool m_bIsRelationsInitialisees = false;

		/// <summary>
		/// Nom de champ->CInfochampTable
		/// </summary>
		private Hashtable m_tableNomChampToChamp = new Hashtable();

		/// <summary>
		/// Nom de propriete->CInfoChampTable
		/// </summary>
		private Hashtable m_tableProprieteToChamp = new Hashtable();


		private Type m_typeObjet;
		private string m_strNomTable;
		private string m_strNomTableInDb;
		private ArrayList m_listeChamps;
		private ArrayList m_listeRelationsParentes;
		private ArrayList m_listeRelationsFilles;
		private ArrayList m_listeRelationsFillesTypeId;
        private List<CInfoIndexTable> m_listeIndexsSupplementaires;
		private CInfoChampTable[] m_champsId;
		private bool m_bIsSynchronisable;
		private bool m_bChampIdAuto;

        private Dictionary<string, List<string>> m_dicProprieteToChampsLies = null;

		private Hashtable m_tableChamps = new Hashtable();

		public CStructureTable()
		{
			m_listeChamps = new ArrayList();
			m_listeRelationsParentes = new ArrayList();
			m_listeRelationsFilles = new ArrayList();
			m_listeRelationsFillesTypeId = new ArrayList();
            m_listeIndexsSupplementaires = new List<CInfoIndexTable>();
			m_champsId = null;
			m_strNomTable = "";
			m_strNomTableInDb = "";
		}

        public static void ClearCache(Type leType)
        {
            if (m_tableTypeToStructure.ContainsKey(leType))
                m_tableTypeToStructure.Remove(leType);
        }

        public static void ClearCache()
        {
            m_tableTypeToStructure.Clear();
        }

		public static CStructureTable GetStructure(Type leType)
		{
			CStructureTable structure = null;
			if(leType != null && m_tableTypeToStructure.ContainsKey(leType))
				structure = (CStructureTable)m_tableTypeToStructure[leType];
			if (structure != null)
				return structure;
			structure = new CStructureTable();
			structure.m_typeObjet = leType;
			m_tableTypeToStructure[leType] = structure;


   

			structure.FillFromType(leType);
			return structure;
		}

		protected CResultAErreur FillFromType(Type leType)
		{
			m_bIsRelationsInitialisees = false;
			m_typeObjet = leType;
			DateTime dtDepart = DateTime.Now;
			CResultAErreur result = CResultAErreur.True;
			m_listeChamps = new ArrayList();
			m_champsId = null;
			m_strNomTable = "";
			m_tableChamps = new Hashtable();

			object[] attribsTable = leType.GetCustomAttributes(typeof(TableAttribute), false);
			if (attribsTable.Length == 0)
			{
				result.EmpileErreur(I.T("The table name is not specified in the class @1|165", leType.ToString()));
				return result;
			}
			m_bChampIdAuto = leType.IsSubclassOf(typeof(CObjetDonneeAIdNumeriqueAuto));

			TableAttribute tableAttrib = ((TableAttribute)attribsTable[0]);
			m_strNomTable = tableAttrib.NomTable;
			m_strNomTableInDb = tableAttrib.NomTableInDb;
			if (HasChampIdAuto)
			{
				if (tableAttrib.ChampsId.Length > 1)
					throw new Exception(I.T("The class @1 has an automatic incremental id which is not the only table key|166", leType.ToString()));
				CInfoChampTable infoChampId = CInfoChampTable.CreateIdAuto(tableAttrib.ChampsId[0]);
				if (leType.IsSubclassOf(typeof(CObjetDonneeAIdNumeriqueAuto)))
					infoChampId.Propriete = "Id";
				m_champsId = new CInfoChampTable[] { infoChampId };
				m_listeChamps.Add(m_champsId[0]);
				m_tableChamps[m_champsId[0].NomChamp] = m_champsId[0];
			}
			Hashtable tableIdsDefinisDansTableAttribute = new Hashtable();
			foreach (string strChamp in tableAttrib.ChampsId)
				tableIdsDefinisDansTableAttribute[strChamp] = "";

			Hashtable tableIds = new Hashtable();

			//Récupère les champs de la table
			AddFields(leType, tableIdsDefinisDansTableAttribute, tableIds, false);


			//Champ de synchronisation
			m_bIsSynchronisable = tableAttrib.Synchronizable;
			if (m_bIsSynchronisable)
			{
				m_listeChamps.Add(new CInfoChampTable(
					CSc2iDataConst.c_champIdSynchro,
					typeof(int),
					0,
					false,
					false,
					true,
					false,
					true));
				m_tableChamps[CSc2iDataConst.c_champIdSynchro] = m_listeChamps[m_listeChamps.Count - 1];
			}

            //Indexs
            object[] attribs = leType.GetCustomAttributes(typeof(IndexAttribute), true);
            if (attribs != null)
            {
                foreach (IndexAttribute index in attribs)
                {
                    CInfoIndexTable info = new CInfoIndexTable(index.Champs);
                    info.IsCluster = index.IsCluster;
                    m_listeIndexsSupplementaires.Add(info);
                }
            }


			//Si on ne charge pas le relations, il faut tout de même s'assurer
			//que tous les champs clé ont bien été chargés (un champ clé peut faire
			//partie d'une relation parente.
			if (!m_bIsRelationsInitialisees)
			{
				foreach (string strChamp in tableAttrib.ChampsId)
					if (tableIds[strChamp] == null)
					{
						//Lit les relations
						AddFields(leType, tableIdsDefinisDansTableAttribute, tableIds, true);
						break;
					}
			}


			if (!HasChampIdAuto)//Cherche les propriétés liés aux clés
			{
				ArrayList lstIds = new ArrayList();
				foreach (string strChamp in tableAttrib.ChampsId)
				{
					CInfoChampTable info = (CInfoChampTable)tableIds[strChamp];
					if (info == null)
						throw new Exception(I.T("The 'get' method for the identifying field @1 of table @2 was not found|167", strChamp, tableAttrib.NomTable));
					lstIds.Add(info);
				}
				m_champsId = (CInfoChampTable[])lstIds.ToArray(typeof(CInfoChampTable));
			}

			TimeSpan sp = DateTime.Now - dtDepart;
			m_fNbMillisecStructures += sp.TotalMilliseconds;

			return result;
		}

		/////////////////////////////////////////////////////////////
		private void AddFields(Type leType, Hashtable tableIdsDefinisDansTableAttribute, Hashtable tableIds, bool bRelationsOnly)
		{
			if (leType == null)
				return;
			if (!bRelationsOnly)
			{
				//Relation vers la table Versions
				#region Relation vers la table versions
				if (!typeof(IObjetSansVersion).IsAssignableFrom(leType))
				{
					CInfoChampTable champVersion = new CInfoChampTable(CSc2iDataConst.c_champIdVersion,
						typeof(int),
						0,
						false,
						false,
						true,
						false,
						true);
                    //Stef 06 10 2009 : le champ version n'est plus indexé seul, il est indexé
                    //avec le champ deleted
                    champVersion.IsIndex = true;
                    champVersion.Propriete = "IdVersionDatabase";
					m_listeChamps.Add(champVersion);
					m_tableChamps[champVersion.NomChamp] = champVersion;
					
					//Stef 29/08/2008 : suppression du lien à la version, trop lent !
					/*CInfoRelation relationVersion = new CInfoRelation(
						CVersionDonnees.c_nomTable,
						NomTable,
						new string[] { CVersionDonnees.c_champId },
						new string[] { champVersion.NomChamp },
						false,
						true,
						true, 
						false);
					relationVersion.NomConvivial = "Version";
					m_listeRelationsParentes.Add(relationVersion);*/

					CInfoChampTable champIdOriginal = new CInfoChampTable(CSc2iDataConst.c_champOriginalId,
						typeof(int),
						0,
						false,
						false,
						true,
						false,
						true);
					champIdOriginal.IsIndex = true;
					m_listeChamps.Add(champIdOriginal);

					CInfoChampTable champIsDeleted = new CInfoChampTable(CSc2iDataConst.c_champIsDeleted,
						typeof(bool),
						0,
						false,
						false,
						false,
						false,
						true);
                    champIsDeleted.Propriete = "IsDeleted";
					m_listeChamps.Add(champIsDeleted);

                    //Stef 06 10 2009 : le champ version n'est plus indexé seul, il est indexé
                    //avec le champ deleted
                    /*CInfoIndexTable infoIndex = new CInfoIndexTable(
                        CSc2iDataConst.c_champIdVersion, CSc2iDataConst.c_champIsDeleted);
                    m_listeIndexsSupplementaires.Add(infoIndex);*/
				}
			}

				#endregion


			foreach (PropertyInfo property in leType.GetProperties())
			{
				string strNomConvivial = "";
				object[] attribs;
				attribs = property.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				//si le champ a l'attribut dynamique, il a un nom convivial
				if (attribs.Length != 0)
					strNomConvivial = ((DynamicFieldAttribute)attribs[0]).NomConvivial;

				//Attribut TableField
				attribs = property.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
				if (attribs.Length == 1 && !bRelationsOnly)
				{
					TableFieldPropertyAttribute attrib = (TableFieldPropertyAttribute)attribs[0];
					if (attrib.IsInDb)//Ne prend que les champs qui sont dans la base de données
					{
                        if (property.Name != "IdUniversel" ||
                            leType.GetCustomAttributes(typeof(NoIdUniverselAttribute), true).Length == 0)
                        {
                            if (property.PropertyType == typeof(string) && attrib.Longueur < 1)
                                throw (new Exception(I.T("The field attribute @1 of class @2 doesn't define a length|168", property.Name, leType.Name)));
                            bool bIsId = tableIdsDefinisDansTableAttribute[attrib.NomChamp] != null;

                            CInfoChampTable info = new CInfoChampTable(
                                attrib.NomChamp,
                                property.PropertyType,
                                attrib.Longueur,
                                attrib.IsLongString,
                                bIsId,
                                attrib.NullAutorise && !bIsId,
                                attrib.ExclureUpdateStandard,
                                attrib.IsInDb);

                            info.Propriete = property.Name;
                            info.NomConvivial = strNomConvivial;

                            if (property.GetCustomAttributes(typeof(IndexFieldAttribute), true).Length > 0)
                                info.IsIndex = true;

                            if (m_tableChamps[info.NomChamp] == null)
                            {
                                m_listeChamps.Add(info);
                                m_tableChamps[info.NomChamp] = info;
                            }
                            if (bIsId)
                            {
                                tableIds[info.NomChamp] = info;
                            }
                        }
					}
				}

				//Attribut Relation
				if (bRelationsOnly)
				{
					m_bIsRelationsInitialisees = true;
					attribs = property.GetCustomAttributes(typeof(RelationAttribute), true);
					if (attribs.Length != 0)
					{
						RelationAttribute relAttrib = (RelationAttribute)attribs[0];
						CStructureTable structureParente = GetStructure(CContexteDonnee.GetTypeForTable(relAttrib.TableMere));
						if (relAttrib.TableMere == NomTable)//Pour les tables autoliées
							structureParente = this;
						//Si la structure parente est moi-même, les champs Id n'ont pas encore
						//été renseignés. Il y a donc un contrôle en moins sur les tables à lien récursif
						if (structureParente.ChampsId != null && relAttrib.ChampsFils.Length != relAttrib.ChampsParent.Length)
							throw new Exception(I.T("The relation between the table @1 and the parental table @2 doesn't have the right number of link fields|169", NomTable, relAttrib.TableMere));

						int nIndexCle = 0;
						foreach (string strChamp in relAttrib.ChampsFils)
						{
							bool bIsId = tableIdsDefinisDansTableAttribute[strChamp] != null;
							CInfoChampTable info = new CInfoChampTable(
								strChamp,
								structureParente.GetChamp(relAttrib.ChampsParent[nIndexCle]).TypeDonnee,
								structureParente.GetChamp(relAttrib.ChampsParent[nIndexCle]).Longueur,
								false,
								bIsId,
								!relAttrib.Obligatoire,
								false,
								true);
                            info.IsIndex = relAttrib.Index;

							//13/3/2005 : les deux lignes suivants avaient été enlevées. Pourquoi ?
							//En tout cas, moi j'en ai besoin pour pouvoir tester
							//Les attributs de la propriété
							//20/4/2005
							//Ca avait été enlevé car le champ pointe sur le type de donnée
							//de la clé (un entier en général), c'est un champ qui ne doit pas être affiché
							//dans les listes de propriétés , c'est juste un champ de la table
							/*info.Propriete = property.Name;
							info.NomConvivial = strNomConvivial;*/
							/* 9/5/2005, si on souhaite trouver la propriété correspondantes,
							 * il faut parcourir les relations et regarder les propriétés des relations
							 * */
							nIndexCle++;
							if (m_tableChamps[info.NomChamp] == null)
							{
								m_listeChamps.Add(info);
								m_tableChamps[info.NomChamp] = info;
							}
							if (tableIdsDefinisDansTableAttribute[strChamp] != null)
								//Ce champ fait partie de la clé de l'objet
								tableIds[info.NomChamp] = info;
						}
						CInfoRelation infoRelation = new CInfoRelation(
							relAttrib.TableMere,
							NomTable,
							relAttrib.ChampsParent,
							relAttrib.ChampsFils,
							relAttrib.Obligatoire,
							relAttrib.Composition,
							relAttrib.Index,
							relAttrib.PasserLesFilsANullLorsDeLaSuppression,
                            relAttrib.DeleteEnCascadeManuel);
                        infoRelation.IsInDb = relAttrib.IsInDb;
                        infoRelation.IsClustered = relAttrib.IsCluster;
						infoRelation.NomConvivial = strNomConvivial;
						infoRelation.Propriete = property.Name;
                        infoRelation.NePasClonerLesFils = relAttrib.NePasClonerLesFils;
						m_listeRelationsParentes.Add(infoRelation);
					}

					//Attribut RelationFille
					attribs = property.GetCustomAttributes(typeof(RelationFilleAttribute), true);
					if (attribs.Length != 0)
					{
						RelationFilleAttribute relFille = (RelationFilleAttribute)attribs[0];
						PropertyInfo propParenteDansFille = relFille.TypeFille.GetProperty(relFille.ProprieteFille);
						if (propParenteDansFille == null)
							throw new Exception(I.T("The @1 type defines a child relation on the child property @2.@3 which doesn't exist|170", leType.Name, relFille.TypeFille.Name, relFille.ProprieteFille));
						//Récupère les infos de la relation
						attribs = propParenteDansFille.GetCustomAttributes(typeof(RelationAttribute), true);
						if (attribs.Length != 1)
							throw new Exception(I.T("The @1 type defines a child relation on the child property @2.@3 which doesn't define a parental relation|171", leType.Name, relFille.TypeFille.Name, relFille.ProprieteFille));
						RelationAttribute relInfo = (RelationAttribute)attribs[0];
						//Récupère le nom de la table fille
						attribs = relFille.TypeFille.GetCustomAttributes(typeof(TableAttribute), true);
						if (attribs.Length != 1)
							throw new Exception(I.T("The @1 type define a child relation on the @2 type which doesn't define a table|172", leType.Name, relFille.TypeFille.Name));
						object[] attr = property.GetCustomAttributes(typeof(DynamicChildsAttribute), false);
						if (attr.Length > 0)
							strNomConvivial = ((DynamicChildsAttribute)attr[0]).NomConvivial;
						CInfoRelation infoRelation = new CInfoRelation(
							NomTable,
							((TableAttribute)attribs[0]).NomTable,
							relInfo.ChampsParent,
							relInfo.ChampsFils,
							relInfo.Obligatoire,
							relInfo.Composition,
							relInfo.Index,
							relInfo.PasserLesFilsANullLorsDeLaSuppression,
                            relInfo.DeleteEnCascadeManuel);
						infoRelation.NomConvivial = strNomConvivial;
						infoRelation.Propriete = property.Name;
                        infoRelation.NePasClonerLesFils = relInfo.NePasClonerLesFils;
						m_listeRelationsFilles.Add(infoRelation);
					}


				}
				
			}
            ///Relations TypeID
            foreach (RelationTypeIdAttribute relTypeId in CContexteDonnee.RelationsTypeIds)
            {
                if (relTypeId.IsAppliqueToType(leType))
                {
                    m_listeRelationsFillesTypeId.Add(relTypeId);
                }
            }
			//AddFields ( leType.BaseType, tableIdsDefinisDansTableAttribute, tableIds );
		}

		/////////////////////////////////////////////////////////////
		private void AssureRelations()
		{
			if (m_bIsRelationsInitialisees || ChampsId == null)
				return;
			Hashtable tableIds = new Hashtable();
			foreach (CInfoChampTable champ in ChampsId)
				tableIds[champ.NomChamp] = true;
			Hashtable tableTmp = new Hashtable();
			AddFields(m_typeObjet, tableIds, tableTmp, true);
		}


		/////////////////////////////////////////////////////////////
		public CInfoChampTable GetChamp(string strChamp)
		{
			AssureRelations();
			CInfoChampTable champ = (CInfoChampTable)m_tableNomChampToChamp[strChamp];
			if (champ == null)
			{
				foreach (CInfoChampTable info in Champs)
					if (info.NomChamp == strChamp)
					{
						champ = info;
						break;
					}
				if (champ != null)
					m_tableNomChampToChamp[strChamp] = champ;
			}
			return champ;
		}

		/////////////////////////////////////////////////////////////
		public CInfoChampTable GetChampFromPropriete(string strPropriete)
		{
			CInfoChampTable champ = (CInfoChampTable)m_tableProprieteToChamp[strPropriete];
			if (champ == null)
			{
				foreach (CInfoChampTable info in Champs)
					if (info.Propriete == strPropriete)
					{
						champ = info;
						break;
					}
				if (champ != null)
					m_tableProprieteToChamp[strPropriete] = champ;
			}
			return champ;
		}




		/////////////////////////////////////////////////////////////
		public CInfoRelation[] RelationsParentes
		{
			get
			{
				AssureRelations();
				return (CInfoRelation[])m_listeRelationsParentes.ToArray(typeof(CInfoRelation));
			}
		}

		/////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne toutes les relations filles standard
		/// </summary>
		public CInfoRelation[] RelationsFilles
		{
			get
			{
				AssureRelations();
				return (CInfoRelation[])m_listeRelationsFilles.ToArray(typeof(CInfoRelation));
			}
		}

		/////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne toutes les relations typeId
		/// </summary>
		public RelationTypeIdAttribute[] RelationsTypeId
		{
			get
			{
				AssureRelations();
				return (RelationTypeIdAttribute[])m_listeRelationsFillesTypeId.ToArray(typeof(RelationTypeIdAttribute));
			}
		}

		/////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne toutes les relations filles, standard ou non (typeId par exemple)
		/// </summary>
		public CInfoRelationBase[] ToutesLesRelationsFilles
		{
			get
			{
				AssureRelations();
				ArrayList lst = new ArrayList(RelationsFilles);
				return (CInfoRelationBase[])lst.ToArray(typeof(CInfoRelationBase));
			}
		}

		/////////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
		}

		/////////////////////////////////////////////////////////////
		public string NomTableInDb
		{
			get
			{
				return m_strNomTableInDb;
			}
		}

		/////////////////////////////////////////////////////////////
		public CInfoChampTable[] Champs
		{
			get
			{
				AssureRelations();
				return (CInfoChampTable[])m_listeChamps.ToArray(typeof(CInfoChampTable));
			}
		}

        /////////////////////////////////////////////////////////////
        /// <summary>
        /// Indexs qui ne sont pas liés à  une relation ou à un champ simple
        /// </summary>
        public CInfoIndexTable[] IndexSupplementaires
        {
            get
            {
                return m_listeIndexsSupplementaires.ToArray();
            }
        }

		////////////////////////////////////////////////////////////
		public CInfoChampTable[] ChampsId
		{
			get
			{
				return m_champsId;
			}
		}

		/////////////////////////////////////////////////////////////
		public bool HasChampIdAuto
		{
			get
			{
				return m_bChampIdAuto;
			}
		}

		/////////////////////////////////////////////////////////////
		public bool IsSynchronisable
		{
			get
			{
				return m_bIsSynchronisable;
			}
		}

		/////////////////////////////////////////////////////////////
		public void CompleteRestrictions(CRestrictionUtilisateurSurType restriction)
		{
			AssureRelations();
			if (!restriction.HasRestrictions)
				return;
			foreach (CInfoChampTable info in m_listeChamps)
			{
				if (!restriction.CanModify(info.Propriete))
					info.ReadOnly = true;
			}
			foreach (CInfoRelation relation in m_listeRelationsParentes)
			{
				if (!restriction.CanModify(relation.Propriete))
				{
					foreach (CInfoChampTable info in m_listeChamps)
						foreach (string strChamp in relation.ChampsFille)
							if (info.NomChamp == strChamp)
								info.ReadOnly = true;
				}
			}
		}

		/////////////////////////////////////////////////////////////
		public Type TypeAssocie
		{
			get
			{
				return m_typeObjet;
			}
		}

        /////////////////////////////////////////////////////////////
        public Dictionary<string, List<string>> GetDicProprieteToChampsLies()
        {
            AssureRelations();
            if (m_dicProprieteToChampsLies != null)
                return m_dicProprieteToChampsLies;
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            AssureRelations();
            foreach (CInfoChampTable info in m_listeChamps)
            {
                List<string> lst = new List<string>();
                lst.Add(info.NomChamp);
                dic[info.Propriete] = lst;
            }
            foreach (CInfoRelation relation in m_listeRelationsParentes)
            {
                List<string> lstChamps = new List<string>();
                foreach (CInfoChampTable info in m_listeChamps)
                    foreach (string strChamp in relation.ChampsFille)
                        if (info.NomChamp == strChamp)
                            lstChamps.Add(info.NomChamp);
                dic[relation.Propriete] = lstChamps;
            }
            m_dicProprieteToChampsLies = dic;
            return m_dicProprieteToChampsLies;
        }

	}
}
