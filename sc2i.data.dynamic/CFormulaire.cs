using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using sc2i.common;
using sc2i.data;
using sc2i.formulaire;
using System.Text;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Un formulaire utilisé parametré par l'utilisateur.
	/// </summary>
    /// <remarks>
    /// <p>Un formulaire consiste en l'assemblage de contrôles utilisateur présentant des données spécifiques</p>
    /// <p>Un formulaire est générallement conçu pour éditer un type d'entité particulier, mais il peut également
    /// s'agir d'un formulaire général, ne s'appliquant à rien, et générallement utilisé en tant que menu dans l'application</p>
    /// <P>Lorsque l'administrateur crée un formulaire en surimpression sur un formulaire standard, celui ci est 
    /// également stocké dans la base sous la forme d'un formulaire</P>
    /// <p>L'administreur de l'application peut également utiliser les formulaires pour remplacer les formulaires
    /// standard de l'application. Dans ce cas, le formulaire ne se voit pas attribuer un rôle, mais pointe directement
    /// sur un type d'entité de l'application.</p>
    /// </remarks>
	[Table(CFormulaire.c_nomTable, CFormulaire.c_champId, true)]
	[FullTableSync]
	[ObjetServeurURI("CFormulaireServeur")]
	[DynamicClass("Form")]
	public class CFormulaire : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		public const string c_nomTable = "CUSTOMFORMS";

		public const string c_champId = "CUF_ID";
		public const string c_champLibelle = "CUF_LABEL";
		public const string c_champData = "CUF_DATA";
		public const string c_champCodeRole = "CUF_ROLE";
        public const string c_champCodesRolesSecondaires = "CUF_SECONDARY_ROLES";
		public const string c_champPartout = "CUF_EVERYWHERE";
		public const string c_champNumeroOrdre = "CUF_ORDER_NUME";
		public const string c_champRestrictionMasquer = "CUF_REST_HIDE";
        public const string c_champTypeElementEdite = "CUF_ELEMENT_TYPE";
        public const string c_champCodeFormulaire = "CUF_CODE";
        public const string c_champAllowEditMode = "CUF_ALLOW_EDIT";
        public const string c_champIsOnArrayElement = "CUF_ELEMENT_IS_ARRAY";

        public const string c_champCacheFormulaire = "CUF_CACHE_FORMULAIRE";

		/// ///////////////////////////////////////////////////////
		public CFormulaire( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CFormulaire ( DataRow row )
			:base ( row) 
		{
		}

		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The Form @1|178",Libelle);
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
			Role = null;
            ElementEditeIsArray = false;
		}

		/// ///////////////////////////////////////////////////////
		///<summary>Libellé du formulaire</summary>
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


		//////// ///// ///// ///// ///// ///// ///// ///// //
		/// <summary>
		/// Clé utilisée pour identifier le champ dans un CRestrictionSurType
		/// </summary>
		public string CleRestriction
		{
			get
			{
				return "CUF_CUST_" + Id;
			}
		}

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champCodeRole, 20, NullAutorise = true)]
		public string CodeRole
		{
			get
			{
				return (string)Row[c_champCodeRole];
			}
			set
			{
				Row[c_champCodeRole] = value;
			}
		}

        //-----------------------------------------------------------
        [TableFieldProperty(c_champCodesRolesSecondaires, 2000)]
        public string CodesRolesSecondairesString
        {
            get
            {
                return (string)Row[c_champCodesRolesSecondaires];
            }
            set
            {
                Row[c_champCodesRolesSecondaires] = value;
            }
        }

        //-----------------------------------------------------------
        public bool HasRoleSecondaire(string strCodeRole)
        {
            return CodesRolesSecondairesString.Contains("~" + strCodeRole + "~");
        }

        //-----------------------------------------------------------
        public string[] CodesRolesSecondaires
        {
            get
            {
                string[] strCodes = CodesRolesSecondairesString.Split('~');
                List<string> lstCodes = new List<string>();
                foreach (string strCode in strCodes)
                {
                    if (strCode.Length > 0)
                        lstCodes.Add(strCode);
                }
                return lstCodes.ToArray();
            }
            set
            {
                if (value != null)
                {
                    StringBuilder bl = new StringBuilder();
                    foreach (string strCode in value)
                    {
                        bl.Append("~");
                        bl.Append(strCode);
                    }
                    if (bl.Length > 0)
                        bl.Append("~");
                    CodesRolesSecondairesString = bl.ToString();
                }
                else
                    CodesRolesSecondairesString = "";
            }
        }

        //------------------------------------------------------------------------
        public static CFiltreData GetFiltreFormulairesForRole(string strCodeRole)
        {
            return new CFiltreData(c_champCodeRole + "=@1 or " +
                c_champCodesRolesSecondaires + " like @2",
                strCodeRole,
                "%~" + strCodeRole + "~%");
        }

        //------------------------------------------------------------------------
        public static CListeObjetsDonnees GetListeFormulairesForRole(CContexteDonnee ctx,
            string strCodeRole)
        {
            CListeObjetsDonnees lst = new CListeObjetsDonnees(ctx, typeof(CFormulaire),
                GetFiltreFormulairesForRole(strCodeRole));
            return lst;
        }


        //-----------------------------------------------------------
        public void AddRoleSecondaire(string strCodeRole)
        {
            bool bTrouve = false;
            List<string> lstNew = new List<string>();
            foreach (string strCode in CodesRolesSecondaires)
            {
                if (strCode == strCodeRole)
                    bTrouve = true;
                lstNew.Add(strCode);
            }
            if (!bTrouve)
                lstNew.Add(strCodeRole);
            CodesRolesSecondaires = lstNew.ToArray();
        }

        //------------------------------------------------------------------------
        public bool HasRole(string strCodeRole)
        {
            return CodeRole == strCodeRole || CodesRolesSecondaires.Contains(strCodeRole);
        }

        //-----------------------------------------------------------
        public void RemoveRoleSecondaire(string strCodeRole)
        {
            List<string> lstNew = new List<string>();
            foreach (string strCode in CodesRolesSecondaires)
            {
                if (strCode != strCodeRole)
                    lstNew.Add(strCode);
            }
            CodesRolesSecondaires = lstNew.ToArray();
        }

        /// ///////////////////////////////////////////////////////
        ///<summary>
        ///Indique le rôle attribué à ce formulaire. Le rôle indique à quel type d'entité s'applique le formulaire
        ///</summary>
        ///<remarks>
        ///<p>Les formulaires en surimpression ou les formulaires remplaçant les formulaires standard n'ont pas de rôle, cette propriété retournela valeur nulle.</p>
        ///</remarks>
        [DynamicField("Role")]
        public CRoleChampCustom Role
        {
            get
            {
                if (Row[c_champCodeRole] == DBNull.Value)
                    return null;
                return CRoleChampCustom.GetRole((string)Row[c_champCodeRole]);
            }
            set
            {
                if (value == null)
                    Row[c_champCodeRole] = DBNull.Value;
                else
                    Row[c_champCodeRole] = value.CodeRole;
            }
        }
		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Inidique que le formulaire est présent pour tous les éléments
		/// du type
		/// </summary>
		[TableFieldProperty(c_champPartout)]
		[DynamicField("Show always")]
		public bool AfficherPartout
		{
			get
			{
				return ( bool )Row[c_champPartout];
			}
			set
			{
				Row[c_champPartout] = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Ordre d'apparition du formulaire
		/// </summary>
        /// <remarks>
        /// Lorsqu'une entité se voit attribuer plusieurs formulaires, ceux ci sont générallement présentés dans l'application
        /// sous forme d'onglets. Les onglets sont triés suivant la valeur "Sequence number" croissante. Si deux formulaires
        /// ont le même numéro de séquence, c'est leur nom qui détermine leur ordre d'apparition.
        /// </remarks>
		[TableFieldProperty(c_champNumeroOrdre)]
		[DynamicField("Sequence number")]
		public int NumeroOrdre
		{
			get
			{
				return ( int )Row[c_champNumeroOrdre];
			}
			set
			{
				Row[c_champNumeroOrdre] = value;
			}
		}



		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champData,NullAutorise=true)]
		public CDonneeBinaireInRow Data
		{
			get
			{
				if ( Row[c_champData] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champData);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champData, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champData]).GetSafeForRow ( Row.Row );
			}
			set
			{
				Row[c_champData] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Permet d'accéder aux champs du formulaire.
        /// </summary>
        /// <remarks>
        /// Suivant la construction du formulaire, il est possible que certains champs n'apparaissent pas dans cette liste,
        /// notamment les champs qui n'utilisent pas une zone "DATA" sur le formulaire, ainsi que les champs de sous formulaires
        /// ou de formulaire dynamique.
        /// </remarks>
        [RelationFille(typeof(CRelationFormulaireChampCustom), "Formulaire")]
        [DynamicField("Fields relations")]
		public CListeObjetsDonnees RelationsChamps
		{
			get
			{
				return GetDependancesListe ( CRelationFormulaireChampCustom.c_nomTable, c_champId );
			}
		}

		/// /////////////////////////////////////////////////////////////
		private void UpdateLiensChampsCustom( C2iWnd fenetre )
		{
			CListeObjetsDonnees listeRelations = RelationsChamps;
			Hashtable tableRelations = new Hashtable();
			Hashtable tableToDelete = new Hashtable();
			foreach ( CRelationFormulaireChampCustom rel in listeRelations )
			{
				tableRelations[rel.Champ.Id] = rel;
				tableToDelete[rel.Champ.Id] = rel;
			}

			if ( fenetre != null )
			{
				ArrayList lst =  fenetre.AllChilds();
				foreach ( object obj in lst )
				{
					if ( obj is C2iWndChampCustom )
					{
						C2iWndChampCustom fenChamp = (C2iWndChampCustom)obj;
						CChampCustom champ = fenChamp.ChampCustom;
						if ( champ != null )
						{
							CRelationFormulaireChampCustom rel = (CRelationFormulaireChampCustom)tableRelations[champ.Id];
							if ( rel == null )
							{
								rel = new CRelationFormulaireChampCustom(ContexteDonnee);
								rel.CreateNewInCurrentContexte();
								rel.Champ = champ;
								rel.Formulaire = this;
								tableRelations[champ.Id] = rel;
							}
							tableToDelete.Remove(champ.Id);
							rel.NumWeb = fenChamp.WebNumOrder;
                            rel.LibelleWeb = fenChamp.WebLabel;
						}
					}
				}
			}
			foreach ( CRelationFormulaireChampCustom rel in tableToDelete.Values )
				rel.Delete();
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la position des flags de masquage du formulaire
		/// </summary>
		/// <remarks>
		/// Lors de l'affichage d'un formulaire, une combinaison binaire est faite
		/// entre ce flag et celui de la session de l'utilisateur. S'il est différent de 0,
		/// le formulaire est masqué
		/// </remarks>
		[TableFieldProperty(c_champRestrictionMasquer)]
		[DynamicField("Hide restriction")]
		public int RestrictionsMasquer
		{
			get
			{
				return ( int  )Row[c_champRestrictionMasquer];
			}
			set
			{
				Row[c_champRestrictionMasquer] = value;
			}
		}


		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCacheFormulaire, IsInDb=false)]
        [DynamicField("Form")]
        [NonCloneable]
        [BlobDecoder]
        public C2iWndFenetre Formulaire
		{
			get
			{
                C2iWndFenetre fenetre = Row[c_champCacheFormulaire] as C2iWndFenetre;
                if (fenetre != null)
                {
                    fenetre.IContexteDonnee = IContexteDonnee;
                    return fenetre;
                }
				fenetre = new C2iWndFenetre();
				fenetre.Size = new System.Drawing.Size ( 550, 400 );
				if ( Data.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(Data.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					serializer.AttacheObjet ( typeof(CContexteDonnee), ContexteDonnee);
					CResultAErreur result = fenetre.Serialize(serializer);
					serializer.DetacheObjet ( typeof(CContexteDonnee), ContexteDonnee );
					if ( !result )
					{
						fenetre = new C2iWndFenetre();
						fenetre.Size = new System.Drawing.Size ( 350, 200 );
					}

                    reader.Close();
                    stream.Close();
				}
                CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheFormulaire, fenetre);
                fenetre.IContexteDonnee = ContexteDonnee;
				return fenetre;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = Data;
					data.Donnees = null;
					Data = data;
					UpdateLiensChampsCustom(null);
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
					{
						CDonneeBinaireInRow data = Data;
						data.Donnees = stream.GetBuffer();
						Data = data;
						UpdateLiensChampsCustom(value);
					}

                    writer.Close();
                    stream.Close();
				}
                Row[c_champCacheFormulaire] = null;
			}
		}


        /// <summary>
        /// Le Type d'élément édité par ce formulaire
        /// </summary>
        [TableFieldProperty(c_champTypeElementEdite, 255, NullAutorise = true)]
        [IndexField]
        public string StringTypeElementEditee
        {
            get
            {
                return (string)Row[c_champTypeElementEdite];
            }
            set
            {
                Row[c_champTypeElementEdite] = value;
            }
        }

        [TableFieldProperty(c_champIsOnArrayElement)]
        [DynamicField("element is array")]
        public bool ElementEditeIsArray
        {
            get
            {
                return (bool)Row[c_champIsOnArrayElement];
            }
            set
            {
                Row[c_champIsOnArrayElement] = value;
            }
        }

        /// <summary>
        /// Le Type d'élément édité par ce formulaire
        /// </summary>
        public Type TypeElementEdite
        {
            get
            {
                if (Row[c_champTypeElementEdite] == DBNull.Value || (string) Row[c_champTypeElementEdite] == "")
                {
                    if (Role != null)
                        return Role.TypeAssocie;
                    return null;
                }
                return CActivatorSurChaine.GetType((string) Row[c_champTypeElementEdite]);
            }
            set
            {
                if (value != null)
                    Row[c_champTypeElementEdite] = value.ToString();
                else
                    Row[c_champTypeElementEdite] = DBNull.Value;
            }
        }

        
		//-----------------------------------------------------------
		/// <summary>
		/// Le code interne du formulaire. Ce code peut être utilisé dans certaines formules pour retrouver rapidement un formulaire.
		/// </summary>
		[TableFieldProperty ( c_champCodeFormulaire, 255 )]
        [DynamicField("Code")]
		public string CodeFormulaire
		{
			get
			{
				return (string)Row[c_champCodeFormulaire];
			}
			set
			{
				Row[c_champCodeFormulaire] = value;
			}
		}


        //-----------------------------------------------------------
        /// <summary>
        /// Indique si ce formulaire gère le mode 'édition'
        /// </summary>
        /// <remarks>
        /// Un formulaire qui gère le mode édition se voit attribuer les boutons standard
        /// d'édition d'éléments. Ce mode est très utile pour créer un formulaire global permettant
        /// d'éditer une entité sans remplacer le formulaire standard.
        /// </remarks>
        [TableFieldProperty(c_champAllowEditMode)]
        [DynamicField("Allow Edit mode")]
        public bool AllowEditMode
        {
            get
            {
                return (bool)Row[c_champAllowEditMode];
            }
            set
            {
                Row[c_champAllowEditMode] = value;
            }
        }


		
	}
}
