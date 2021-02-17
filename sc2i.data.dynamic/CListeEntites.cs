using System;
using System.Collections;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.data.dynamic;
using sc2i.common;
using System.Text;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Une liste d'entités représente un regroupement d'entités.<BR></BR>
	/// Il peut s'agir de n'importe quel type d'entité.<BR></BR>
    /// <p>Tous les éléments contenus dans une liste doivent être du même type, qu'il s'agisse d'une liste statique ou dynamique.</p>
	/// Il existe deux types de listes : 
	/// <LI>
	/// Listes statiques : créées en ajoutant manuellement les éléments dans la liste.
	/// </LI>
	/// <LI>
	/// Listes dynamiques : Créées à partir d'un filtre qui est réévaluée à chaque demande de la liste des
	/// éléments contenus.
	/// </LI>
    /// <p>Certaines listes dynamiques peuvent se bases sur un élément racine pour retourner des entités dépendantes de l'entité racine.</p>
	/// </summary>
	[DynamicClass("Entity list")]
	[ObjetServeurURI("CListeEntitesServeur")]
	[Table(CListeEntites.c_nomTable, CListeEntites.c_champId,true, IsGrandVolume = true)]
	[FullTableSync]
	public class CListeEntites : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		#region Déclaration des constantes
		public const string c_nomTable = "ENTITY_LIST";
		public const string c_champId = "ENTLST_ID";
		public const string c_champLibelle = "ENTLST_LABEL";
		public const string c_champCode = "ENTLST_CODE";
		public const string c_champCommentaires = "ENTLST_COMMENT";
		public const string c_champTypeElements = "ENTLST_ELEMENTS_TYPE";
		public const string c_champVersion = "ENTLST_VERSION";
		public const string c_champDataFiltre = "ENTLST_FILTER";
		public const string c_champTypeElementSourceDeRecherche = "ENTLST_TP_SRC_RECH";
		#endregion

        

		//-------------------------------------------------------------------
#if PDA
		public CListeEntites()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CListeEntites( CContexteDonnee ctx )
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CListeEntites( System.Data.DataRow row )
			:base(row)
		{
		}
		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}
		//-------------------------------------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}
		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("The entity list @1|181",Libelle);
			}
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Libellé convivial de la liste.
		/// </summary>
		[
		TableFieldProperty(c_champLibelle, 255),
		DynamicField("Label")
		]
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

		//-------------------------------------------------------------------
		/// <summary>
		/// Code (unique ou vide) de la liste
		/// </summary>
		[
		TableFieldProperty(c_champCode, 255),
		DynamicField("Code")
		]
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

		//-------------------------------------------------------------------
		/// <summary>
		/// Commentaires ou descriptif de cette liste
		/// </summary>
		[
		TableFieldProperty(c_champCommentaires, 1024),
		DynamicField("Commentaires")
		]
		public string Commentaires
		{
			get
			{
				return (string)Row[c_champCommentaires];
			}
			set
			{
				Row[c_champCommentaires] = value;
			}
		}

		/// <summary>
		/// Type des éléments codé sous format interne des éléments concernés par la liste.
		/// </summary>
		[TableFieldProperty(c_champTypeElements, 1024)]
        [DynamicField("Element type string")]
		public string StringTypeElements
		{
			get
			{
				return (string)Row[c_champTypeElements];
			}
			set
			{
				Row[c_champTypeElements] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		public Type TypeElements
		{
			get
			{
				return CActivatorSurChaine.GetType ( StringTypeElements );
			}
			set
			{
				StringTypeElements = value.ToString();
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type des éléments de la liste. Le type des éléments est présenté sous forme compréhensible aux utilisateurs finaux de l'application.
		/// </summary>
		[DynamicField("Element type")]
		public string TypeElementsConvivial
		{
			get
			{
				Type tp = TypeElements;
				return DynamicClassAttribute.GetNomConvivial ( tp );
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TableFieldProperty(CListeEntites.c_champTypeElementSourceDeRecherche, 1024)]
		public string StringTypeElementsSourceRecherche
		{
			get
			{
				return (string)Row[c_champTypeElementSourceDeRecherche];
			}
			set
			{
				Row[c_champTypeElementSourceDeRecherche] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		public Type TypeElementsSourceRecherche
		{
			get
			{
				return CActivatorSurChaine.GetType(StringTypeElementsSourceRecherche);
			}
			set
			{
				if (value == null)
					StringTypeElementsSourceRecherche = "";
				else
					StringTypeElementsSourceRecherche = value.ToString();
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type des éléments de la liste
		/// </summary>
		[DynamicField("Source search element")]
		public string TypeElementsConvivialSourceRecherche
		{
			get
			{
				Type tp = TypeElementsSourceRecherche;
				return DynamicClassAttribute.GetNomConvivial(tp);
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Relations vers les entités
		/// </summary>
		[RelationFille ( typeof ( CRelationListeEntites_Entite ), "ListeEntites") ]
		[DynamicChilds ("Liens entites", typeof ( CRelationListeEntites_Entite ))]
		public CListeObjetsDonnees RelationsEntites
		{
			get
			{
				return GetDependancesListe(CRelationListeEntites_Entite.c_nomTable, c_champId );
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Version de la liste. Chaque fois que la liste est 
		/// modifiée, son numéro de version est incrémenté
		/// </summary>
		[TableFieldProperty(c_champVersion)]
		[DynamicField("Version")]
		public int Version
		{
			get
			{
				return ( int )Row[c_champVersion];
			}
			set
			{
				//On n'incrémente le n° de version que s'il n'a pas été modifié depuis
				//la dernière sauvegarde
				if ( Row.RowState != DataRowState.Modified )
					Row[c_champVersion] = value;
				else
				{
					try
					{
						if ( Row[c_champVersion].Equals(Row[c_champVersion,DataRowVersion.Original] ))
							return;
					}
					catch{}
					Row[c_champVersion] = value;
				}
			}
		}

        //Pour optimiser la lecture quand on n'a pas besoin d'un tri
        private bool m_bLireListeSansTri = false;
        public bool LireElementsLiesSansTri
        {
            get
            {
                return m_bLireListeSansTri;
            }
            set
            {
                m_bLireListeSansTri = value;
            }
        }

		//-------------------------------------------------------------------
		/// <summary>
		/// Permet de récupérer la liste de tous les éléments contenus dans la liste.<br></br>
        /// Pour une liste statique, cette propriété permet également d'affecter tous les éléments faisant partie de la liste.
		/// </summary>
        [DynamicField("Entities")]
		public CObjetDonneeAIdNumerique[] ElementsLies
		{
			get
			{
				if ( !IsDynamique )
					return (CObjetDonneeAIdNumerique[])CInterpreteurTextePropriete.CreateListeFrom ( RelationsEntites, "ElementLie", typeof(CObjetDonneeAIdNumerique));
				else
				{
					//Calcule le filtre
					try
					{
						CFiltreDynamique filtreDyn = FiltreDynamique;
						if ( filtreDyn != null )
						{
							CResultAErreur result = filtreDyn.GetFiltreData();
							if ( result && result.Data is CFiltreData)
							{
								CListeObjetsDonnees liste = new CListeObjetsDonnees ( ContexteDonnee, TypeElements );
								liste.Filtre = (CFiltreData)result.Data;
                                if (LireElementsLiesSansTri)
                                    liste.ModeSansTri = true;
								return (CObjetDonneeAIdNumerique[])liste.ToArray ( TypeElements );
							}
						}
					}
					catch{}
				}
				return new CObjetDonneeAIdNumerique[0];
			}
			set
			{
                if (IsDynamique)
                    return;
				if ( value.Length > 0 )
					TypeElements = value[0].GetType();
				//Table Entite->Vrai si existe dans les deux liste
				//			->Faux si existe uniquement dans l'ancienne liste (à supprimer)
				//			->Null si existe uniquement dans la nouvelle liste (à créer)
				Hashtable tableExistants = new Hashtable();
				foreach ( CRelationListeEntites_Entite rel in RelationsEntites )
					tableExistants[rel.ElementLie] = false;
				foreach ( CObjetDonneeAIdNumerique objet in value )
				{
					if ( tableExistants[objet] == null )//création
					{
						CRelationListeEntites_Entite rel = new CRelationListeEntites_Entite ( ContexteDonnee );
						rel.CreateNewInCurrentContexte();
						rel.ElementLie = objet;
						rel.ListeEntites = this;
					}
					else
						tableExistants[objet] = true;
				}
				//Supprime ceux qui doivent être supprimés
				foreach ( CRelationListeEntites_Entite rel in RelationsEntites.ToArrayList() )
				{
					object val = tableExistants[rel.ElementLie];
					if ( val is bool && ((bool)val)==false )
						rel.Delete();
				}
				FiltreDynamique = null;
			}
		}

		public IElementAVariablesDynamiquesAvecContexteDonnee GetElementAVariableSource(object obj)
		{
			if (obj == null)
				return null;
			IElementAVariablesDynamiques elt = GetElementAVariableSourceFromType(obj.GetType());
			if (obj != null)
				elt.SetValeurChamp((IVariableDynamique)elt.ListeVariables[0], obj);
			return elt as IElementAVariablesDynamiquesAvecContexteDonnee;
		}

		public IElementAVariablesDynamiquesAvecContexteDonnee GetElementAVariableSourceFromType(Type tp)
		{
			if (tp == null)
				return null;
			CElementAVariablesDynamiques elt = CElementAVariablesDynamiques.GetElementAUneVariableType(tp, DynamicClassAttribute.GetNomConvivial(tp));
			return elt;
		}

		///<summary>
        ///Retourne les éléments liés à partir d'un élément de recherche
        ///</summary>
		[DynamicMethod("Return dependant elements bound by a research element")]
		public CListeObjetsDonnees GetElementsLiesFor(Object obj)
		{
			if (obj == null)
				return null;
			if (obj.GetType() != TypeElementsSourceRecherche)
				return null;

			//Calcule le filtre
			try
			{
				CFiltreDynamique filtreDyn = FiltreDynamique;
				IElementAVariablesDynamiquesAvecContexteDonnee eltAVariables = GetElementAVariableSource(obj);
				filtreDyn.ElementAVariablesExterne = eltAVariables;
				if (filtreDyn != null)
				{
					CResultAErreur result = filtreDyn.GetFiltreData();
					if (result && result.Data is CFiltreData)
					{
						CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, TypeElements, (CFiltreData) result.Data );
						return liste;

					}
				}
			}
			catch { }
			return null;
		}

        /// /////////////////////////////////////////////////////////////
        [DynamicMethod("Add element to list")]
        public void AddElement(CObjetDonneeAIdNumerique objet)
        {
            if (DataFiltre.Donnees == null && objet != null && objet.GetType() == TypeElements)
            {
                //Cherche si l'élément n'est pas déjà présent
                CListeObjetsDonnees lst = RelationsEntites;
                lst.Filtre = new CFiltreData(CRelationListeEntites_Entite.c_champIdElement + "=@1",
                    objet.Id);
                if (lst.Count == 0)
                {
                    CRelationListeEntites_Entite rel = new CRelationListeEntites_Entite(ContexteDonnee);
                    rel.CreateNewInCurrentContexte();
                    rel.ListeEntites = this;
                    rel.ElementLie = objet;
                }
            }
        }

        /// /////////////////////////////////////////////////////////////
        [DynamicMethod("Remove element from list")]
        public void RemoveElement(CObjetDonneeAIdNumerique objet)
        {
            if (objet != null && objet.GetType() == TypeElements)
            {
                CListeObjetsDonnees lst = RelationsEntites;
                lst.Filtre = new CFiltreData(CRelationListeEntites_Entite.c_champIdElement + "=@1",
                    objet.Id);
                if (lst.Count > 0)
                    CObjetDonneeAIdNumerique.Delete(lst, true);
            }
        }



		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champDataFiltre,NullAutorise=true)]
		public CDonneeBinaireInRow DataFiltre
		{
			get
			{
				if ( Row[c_champDataFiltre] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champDataFiltre);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataFiltre, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champDataFiltre]).GetSafeForRow ( Row.Row );
			}
			set
			{
				Row[c_champDataFiltre] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CFiltreDynamique FiltreDynamique
		{
			get
			{
				CFiltreDynamique retour = null;
				if ( DataFiltre.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(DataFiltre.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					I2iSerializable objet = null;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						retour = (CFiltreDynamique)objet;
					}

                    reader.Close();
                    stream.Close();
				}

				return retour;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataFiltre;
					data.Donnees = null;
					DataFiltre = data;
				}
				else
				{
					
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					I2iSerializable objet = value;
					CResultAErreur result = serializer.TraiteObject ( ref objet );
					if ( result )
					{
						CDonneeBinaireInRow data = DataFiltre;
						data.Donnees = stream.GetBuffer();
						DataFiltre = data;
					}

                    writer.Close();
                    stream.Close();
				}
			}
		}

		/// <summary>
		/// Indique si la liste est une liste dynamique
		/// </summary>
		[DynamicField("Is dynamic")]
		public bool IsDynamique
		{
			get
			{
				return DataFiltre.Donnees != null;
			}
		}


        /// /////////////////////////////////////////////////////////////
        public CFiltreData GetFiltrePourListe()
        {
            CFiltreData filtre = new CFiltreData();
            if (IsDynamique)
            {
                CFiltreDynamique filtreDyn = FiltreDynamique;
                CResultAErreur result = filtreDyn.GetFiltreData();
                if (result.Data is CFiltreData)
                {
                    filtre = (CFiltreData)result.Data;
                }
            }
            else
            {
                string strChampId = "";
                StringBuilder bl = new StringBuilder();
                CStructureTable structure = CStructureTable.GetStructure(TypeElements);
                strChampId = structure.ChampsId[0].NomChamp;
                foreach ( CRelationListeEntites_Entite rel in RelationsEntites )
                {
                    bl.Append(rel.IdElement);
                    bl.Append(',');
                }
                if (bl.Length > 0)
                {
                    bl.Remove(bl.Length - 1, 1);
                }
                if (bl.Length > 0)
                    filtre = new CFiltreData(strChampId + " in (" + bl.ToString() + ")");
                else
                    filtre = new CFiltreDataImpossible();
            }
            return filtre;
        }
	}
}
