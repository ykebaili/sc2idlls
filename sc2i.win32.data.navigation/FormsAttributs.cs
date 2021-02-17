using System;
using sc2i.data;
using System.Collections.Generic;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic;
using System.IO;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{



	/// <summary>
	/// Indique les type d'élements qu'une forme permet d'éditer
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ObjectEditeur : Attribute
	{
		public readonly Type TypeEdite;
		public readonly string Code = "";

		public ObjectEditeur(Type typeEdite)
		{
			TypeEdite = typeEdite;
		}

		public ObjectEditeur(Type typeEdite, string strCode)
		{
			TypeEdite = typeEdite;
			Code = strCode;
		}

	}


	/// <summary>
	/// Indique que la classe est une classe présentant une liste d'objets
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ObjectListeur : Attribute
	{
		public readonly Type TypeListe;

		public ObjectListeur(Type typeListe)
		{
			TypeListe = typeListe;
		}
	}

	/// <summary>
	/// PErmet de trouver une forme grâce aux attributes ObjectEditeur ou ObjectListeur
	/// </summary>
	public class CFormFinder
	{

		//----------------------------------------------------------------------------------
		/// <summary>
		/// Retourne le type de form qui sait éditer le type demandé
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static CReferenceTypeForm GetRefFormToEdit(Type typeAEditer)
		{
			// Cherche dans le dictionnaire des Forms préférés
			CReferenceTypeForm refTypeForm = null;
			if (CDictionnaireTypeEditeTypeFormPrefere.GetInstance().TryGetValue(typeAEditer, out refTypeForm))
			{
				if (refTypeForm != null)
					return refTypeForm;
			}

			// Si pas de préféré, on prend le Form "système" (comme avant)
			foreach (System.Reflection.Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type tp in ass.GetExportedTypes())
				{
					object[] attribs = tp.GetCustomAttributes(typeof(ObjectEditeur), true);
					if (attribs != null && attribs.Length == 1)
					{
						ObjectEditeur objEdit = (ObjectEditeur)attribs[0];
						if (objEdit.TypeEdite == typeAEditer)
						{
							CReferenceTypeFormBuiltIn tpFormSys = new CReferenceTypeFormBuiltIn();
							tpFormSys.TypeForm = tp;
							return tpFormSys;
						}
					}
				}
			}

			return null;
		}

		//----------------------------------------------------------------------------------
		public static CReferenceTypeForm GetRefFormToEdit(Type typeAEditer, string strCode)
		{
			CListeObjetsDonnees lstFormulaires = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, typeof(CFormulaire));
			lstFormulaires.Filtre = new CFiltreData(
				CFormulaire.c_champCodeFormulaire + " = @1 AND " +
				CFormulaire.c_champTypeElementEdite + " = @2",
				strCode,
				typeAEditer.ToString());

			if (lstFormulaires.Count > 0)
			{
				// il y a un formulaire correspondant au code donné
				CFormulaire form = lstFormulaires[0] as CFormulaire;
				if (form != null)
				{
					CReferenceTypeFormDynamic tpFormDyn = new CReferenceTypeFormDynamic();
					tpFormDyn.IdFormulaireInDb = form.DbKey;
					return tpFormDyn;
				}

			}

			return null;


		}

		//----------------------------------------------------------------------------------
		/// <summary>
		/// Retourne le type de form qui sait liste le type demandé
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static Type GetTypeFormToList(Type typeALister)
		{
			foreach (System.Reflection.Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type tp in ass.GetExportedTypes())
				{
					object[] attribs = tp.GetCustomAttributes(typeof(ObjectListeur), true);
					if (attribs != null && attribs.Length == 1)
					{
						ObjectListeur objEdit = (ObjectListeur)attribs[0];
						if (objEdit.TypeListe == typeALister)
							return tp;
					}
				}
			}
			return null;

		}


		//----------------------------------------------------------------------------------
		public static Type[] GetListeTypeFormToEdit(Type typeAEditer)
		{
			List<Type> lstTypeForms = new List<Type>();

			foreach (System.Reflection.Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type tp in ass.GetExportedTypes())
				{
					object[] attribs = tp.GetCustomAttributes(typeof(ObjectEditeur), true);
					if (attribs != null && attribs.Length == 1)
					{
						ObjectEditeur objEdit = (ObjectEditeur)attribs[0];
						if (objEdit.TypeEdite == typeAEditer)
							lstTypeForms.Add(tp);
					}
				}
			}

			return lstTypeForms.ToArray();

		}

		//----------------------------------------------------------------------------------
		public static CReferenceTypeForm[] GetReferencesTypeToEdit(Type tpElement)
		{
			List<CReferenceTypeForm> lstRefs = new List<CReferenceTypeForm>();
			if ( tpElement== null)
				return lstRefs.ToArray();
			foreach ( Type tp in GetListeTypeFormToEdit ( tpElement ) )
				lstRefs.Add ( new CReferenceTypeFormBuiltIn ( tp ) );

			//Charge les formulaires
			CListeObjetsDonnees listeFormulaires = new CListeObjetsDonnees ( CContexteDonneeSysteme.GetInstance(), typeof(CFormulaire) );
			listeFormulaires.Filtre = new CFiltreData(
                        CFormulaire.c_champTypeElementEdite + " = @1 AND " +
                        CFormulaire.c_champCodeRole + " is null" ,
                        tpElement.ToString());
			foreach ( CFormulaire formulaire in listeFormulaires )
				lstRefs.Add ( new CReferenceTypeFormDynamic ( formulaire ));
			return lstRefs.ToArray();
		}



	}



	////////////////////////////////////////////////////////////////////////////////////////////////
	[Serializable]
	public class CDictionnaireTypeEditeTypeFormPrefere : Dictionary<Type, CReferenceTypeForm>, I2iSerializable
	{
		private static CDictionnaireTypeEditeTypeFormPrefere m_dictionnaire = null;
		public const string c_cleRegistre = "FAVOURITE_CUSTOM_FORM";

		/// <summary>
		/// Attention: Cette classe ne doit pas être allouée directement. Il faut passer par GetInstance pour obtenir
		/// l'intance unique de la classe. Le constructeur est néanmoins public afin de pouvoir être sérialsée
		/// </summary>
		public CDictionnaireTypeEditeTypeFormPrefere()
			: base()
		{
		}


		//--------------------------------------------------------------------------------------
		public static CDictionnaireTypeEditeTypeFormPrefere GetInstance()
		{
			if (m_dictionnaire == null)
			{
				// Lit le registre
				CDataBaseRegistrePourClient registre = new CDataBaseRegistrePourClient(CSc2iWin32DataClient.ContexteCourant.IdSession);
				if (registre != null)
				{
					byte[] data = registre.GetValeurBlob(c_cleRegistre);
					if (data != null)
					{
						MemoryStream flux = new MemoryStream(data);
						BinaryReader lecteur = new BinaryReader(flux);
						CSerializerReadBinaire serializer = new CSerializerReadBinaire(lecteur);
						CResultAErreur result = serializer.TraiteObject<CDictionnaireTypeEditeTypeFormPrefere>(ref m_dictionnaire);
                        lecteur.Close();
                        flux.Close();
                        
						if (result)
							return m_dictionnaire;
					}
				}
				m_dictionnaire = new CDictionnaireTypeEditeTypeFormPrefere();
			}
			return m_dictionnaire;
		}

		//--------------------------------------------------------------------------------------
		public static CResultAErreur SaveInstance()
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_dictionnaire != null)
			{
				MemoryStream flux = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(flux);
				CSerializerSaveBinaire serilaizer = new CSerializerSaveBinaire(writer);
				result = serilaizer.TraiteObject<CDictionnaireTypeEditeTypeFormPrefere>(ref m_dictionnaire);
                if (result)
                {
                    CDataBaseRegistrePourClient registre = new CDataBaseRegistrePourClient(CSc2iWin32DataClient.ContexteCourant.IdSession);
                    result = registre.SetValeurBlob(c_cleRegistre, flux.ToArray());
                }
                writer.Close();
                flux.Close();
			}
			return result;
		}

		public static void ResetInstance()
		{
			m_dictionnaire = null;
		}

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			// Sérialiser l'instance unique
			int nb = this.Count;
			serializer.TraiteInt(ref nb);
			switch (serializer.Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (KeyValuePair<Type, CReferenceTypeForm> paire in this)
					{
						Type tp = paire.Key;
						CReferenceTypeForm refForm = paire.Value;

						serializer.TraiteType(ref tp);
						result = serializer.TraiteObject<CReferenceTypeForm>(ref refForm);
						if (!result)
							return result;
					}
					break;

				case ModeSerialisation.Lecture:
					this.Clear();
					for (int i = 0; i < nb; i++)
					{
						Type tp = null;
						CReferenceTypeForm refForm = null;

						serializer.TraiteType(ref tp);
						result = serializer.TraiteObject<CReferenceTypeForm>(ref refForm);
						if (!result)
							return result;
                        if (!(refForm is CReferenceTypeFormAvecCondition))
                        {
                            CReferenceTypeFormAvecCondition refCond = new CReferenceTypeFormAvecCondition();
                            refCond.DefaultTypeForm = refForm;
                            refForm = refCond;
                        }
						if (tp != null)
							this[tp] = refForm;
					}
					break;
				default:
					break;
			}


			return result;
		}

	}

	[Serializable]
	public abstract class CReferenceTypeForm : I2iSerializable, IReferenceFormEdition
	{
        /// <summary>
        /// Renvoie de type de référence finale correpondant
        /// utilisé par les ReferenceTypeForm conditionnelles
        /// </summary>
        /// <param name="objetDonnee"></param>
        /// <returns></returns>
        public virtual CReferenceTypeForm GetFinalRefTypeForm(CObjetDonneeAIdNumeriqueAuto objetDonnee)
        {
            return this;
        }

		public abstract Form GetForm();
		public abstract Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee);
		public abstract Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee, CListeObjetsDonnees liste);

		public abstract CResultAErreur Serialize(C2iSerializer serializer);

		public abstract string Libelle { get;}

		public override string ToString()
		{
			return Libelle;
		}

		public static implicit operator CReferenceTypeForm(Type tp)
		{
			return new CReferenceTypeFormBuiltIn(tp);
		}

	}

	[Serializable]
	public class CReferenceTypeFormBuiltIn : CReferenceTypeForm, I2iSerializable
	{
		// Le Type de Formulaire dédition standard
		Type m_typeForm = null;



		public CReferenceTypeFormBuiltIn()
		{

		}

		public override string Libelle
		{
			get
			{
           
				return "System form (" + DynamicClassAttribute.GetNomConvivial(m_typeForm)+")";
			}
		}

		public CReferenceTypeFormBuiltIn(Type typeForm)
		{
			m_typeForm = typeForm;
		}

		//---------------------------------------------------------------------------
		public Type TypeForm
		{

			get { return m_typeForm; }
			set { m_typeForm = value; }
		}

		//---------------------------------------------------------------------------
		public override Form GetForm()
		{
            using (CWaitCursor waiter = new CWaitCursor())
            {
                return (Form)Activator.CreateInstance(m_typeForm);
            }
		}
		//---------------------------------------------------------------------------
		public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee)
		{
            using (CWaitCursor waiter = new CWaitCursor())
            {
                return (Form)Activator.CreateInstance(m_typeForm, new object[] { objetDonnee });
            }
		}
		//---------------------------------------------------------------------------
		public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee, CListeObjetsDonnees liste)
		{
            using (CWaitCursor waiter = new CWaitCursor())
            {
                return (Form)Activator.CreateInstance(m_typeForm, new object[] { objetDonnee, liste });
            }
		}


		//---------------------------------------------------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//---------------------------------------------------------------------------
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			serializer.TraiteType(ref m_typeForm);

			return result;
		}

        public override bool Equals(object obj)
        {
            CReferenceTypeFormBuiltIn bin = obj as CReferenceTypeFormBuiltIn;
            if (bin != null && bin.TypeForm == TypeForm)
                return true;
            return false;
        }


	}


	[Serializable]
	public class CReferenceTypeFormDynamic : CReferenceTypeForm, I2iSerializable
	{
		// L'Id du formulaire custom d'édition
        private CDbKey m_dbKeyFormIndb = null;

		/// <summary>
		/// Stocke le type de form qui va encapsuler le formulaire.
		/// </summary>
		private static Type m_typeFormRecepteur = null;



		private string m_strLibelle = "";


		public CReferenceTypeFormDynamic()
		{

		}

        public CReferenceTypeFormDynamic(CDbKey keyFormulaire)
        {
            m_dbKeyFormIndb = keyFormulaire;
            m_strLibelle = "";
        }

		public CReferenceTypeFormDynamic(CFormulaire formulaire)
		{
			m_strLibelle = formulaire.Libelle;
			m_dbKeyFormIndb = formulaire.DbKey;
		}

		public override string Libelle
		{
			get
			{
				if (m_strLibelle != "")
					return m_strLibelle;
                return "Form n°" + m_dbKeyFormIndb.StringValue;
			}
		}

		//---------------------------------------------------------------------------
		public static void SetTypeFormRecepteur(Type tpForm)
		{
			m_typeFormRecepteur = tpForm;
		}


		//---------------------------------------------------------------------------
		public CDbKey IdFormulaireInDb
		{
			get { return m_dbKeyFormIndb; }
            set { m_dbKeyFormIndb = value; }
		}

		//---------------------------------------------------------------------------
		public override Form GetForm()
		{
			IFormRecepteurFormulaire form = (IFormRecepteurFormulaire)Activator.CreateInstance(m_typeFormRecepteur);
            form.IdFormulaireAffiche = m_dbKeyFormIndb;

			return (Form)form;
		}
		//---------------------------------------------------------------------------
		public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee)
		{
			IFormRecepteurFormulaire form = (IFormRecepteurFormulaire)Activator.CreateInstance(
				m_typeFormRecepteur,
				new object[] { objetDonnee });
            form.IdFormulaireAffiche = m_dbKeyFormIndb;

			return (Form)form;
		}
		//---------------------------------------------------------------------------
		public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee, CListeObjetsDonnees liste)
		{
			IFormRecepteurFormulaire form = (IFormRecepteurFormulaire)Activator.CreateInstance(
				m_typeFormRecepteur,
				new object[] { objetDonnee, liste });
            form.IdFormulaireAffiche = m_dbKeyFormIndb;

			return (Form)form;
		}

		//---------------------------------------------------------------------------
		private int GetNumVersion()
		{
			//return 1;
            return 2; // Passage de Id Form en DbKey
		}

		//---------------------------------------------------------------------------
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
            
            //TESTDBKEYOK
            if (nVersion < 2)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyFormIndb, typeof(CFormulaire));
            else
                serializer.TraiteDbKey(ref m_dbKeyFormIndb);
            

			if ( nVersion >= 1 )
				serializer.TraiteString ( ref m_strLibelle );
			else
				m_strLibelle = "";

			return result;
		}

        public override bool Equals(object obj)
        {
            CReferenceTypeFormDynamic rfDyn = obj as CReferenceTypeFormDynamic;
            if (rfDyn != null && rfDyn.IdFormulaireInDb == IdFormulaireInDb)
                return true;
            return false;
        }

	}

    //---------------------------------------------------------------------------
    /// <summary>
    /// Cette classe n'est pas exploitée, il faut encore gérer l'édition
    /// donc modifier la fenêtre d'édition de gestion des formes préferées
    /// </summary>
    public class CReferenceTypeFormAvecCondition : CReferenceTypeForm
    {
        public class CParametreTypeForm : I2iSerializable
        {
            private int m_nIndex = 0;
            private C2iExpression m_formuleCondition = new C2iExpressionVrai();
            private CReferenceTypeForm m_referenceForm = null;

            //----------------------------------------------------
            public CParametreTypeForm()
            {
            }

            //----------------------------------------------------
            public int Index
            {
                get
                {
                    return m_nIndex;
                }
                set
                {
                    m_nIndex = value;
                }
            }

            //----------------------------------------------------
            public C2iExpression Formule
            {
                get{
                    return m_formuleCondition;
                }
                set{m_formuleCondition = value;
                }
            }

            //----------------------------------------------------
            public CReferenceTypeForm ReferenceTypeForm
            {
                get
                {
                    return m_referenceForm;
                }
                set
                {
                    m_referenceForm = value;
                }
            }

            //----------------------------------------------------
            private int GetNumVersion()
            {
                return 0;
            }
            
            //----------------------------------------------------
            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
                if ( !result )
                    return result;
                serializer.TraiteInt ( ref m_nIndex );
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleCondition);
                if ( result )
                    result = serializer.TraiteObject<CReferenceTypeForm>(ref m_referenceForm);
                if ( !result )
                    return result;

                return result;
            }
        }

        private CReferenceTypeForm m_defaultTypeForm = null;

        private List<CParametreTypeForm> m_listeParametres = new List<CParametreTypeForm>();

        //-----------------------------------------------------------------------------------------
        public CReferenceTypeFormAvecCondition()
            : base()
        {
        }

        //-----------------------------------------------------------------------------------------
        public CReferenceTypeForm DefaultTypeForm
        {
            get
            {
                return m_defaultTypeForm;
            }
            set
            {
                m_defaultTypeForm = value;
            }
        }

        //-----------------------------------------------------------------------------------------
        public IEnumerable<CParametreTypeForm> Parametres
        {
            get
            {
                m_listeParametres.Sort((x, y) => x.Index.CompareTo(y.Index));
                return m_listeParametres;
            }
            set
            {
                m_listeParametres = new List<CParametreTypeForm>();
                if (value != null)
                    m_listeParametres.AddRange(value);

            }
        }

        //-----------------------------------------------------------------------------------------
        public override CReferenceTypeForm GetFinalRefTypeForm(CObjetDonneeAIdNumeriqueAuto objetDonnee)
        {
            CResultAErreur result = CResultAErreur.True;
            
            if (objetDonnee == null)
            {
                foreach (CParametreTypeForm parametre in m_listeParametres)
                {
                    if (parametre.Formule == null || parametre.Formule is C2iExpressionVrai)
                        return parametre.ReferenceTypeForm;
                }
                return m_defaultTypeForm;
            }
            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(objetDonnee);
            foreach (CParametreTypeForm parametre in m_listeParametres)
            {
                result.Data = true;
                if (parametre.Formule != null)
                {
                    result = parametre.Formule.Eval(ctxEval);
                }
                if (result && CUtilBool.BoolFromObject(result.Data))
                    return parametre.ReferenceTypeForm;
            }
            if (m_defaultTypeForm != null)
                return m_defaultTypeForm;
            return null;
        }
        
        //-----------------------------------------------------------------------------------------
        public override Form GetForm()
        {
            CReferenceTypeForm refType = GetFinalRefTypeForm(null);
            if (refType != null)
            {
                return refType.GetForm();
            }
            return null;
        }

        //--------------------------------------------------------------------------
        public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee)
        {
            return GetForm(objetDonnee, null);
        }

        //--------------------------------------------------------------------------
        public override Form GetForm(CObjetDonneeAIdNumeriqueAuto objetDonnee, CListeObjetsDonnees liste)
        {
            CReferenceTypeForm refType = GetFinalRefTypeForm(objetDonnee);
            if (refType != null)
                return refType.GetForm(objetDonnee, liste);
            return null;
        }
        /*
            CResultAErreur result = CResultAErreur.True;
            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(objetDonnee);
            foreach (CParametreTypeForm parametre in m_listeParametres)
            {
                result.Data = true;
                if (parametre.Formule != null)
                {
                    result = parametre.Formule.Eval(ctxEval);
                }
                if (result && CUtilBool.BoolFromObject(result.Data))
                    return parametre.ReferenceTypeForm.GetForm(objetDonnee, liste);
            }
            if (m_defaultTypeForm != null)
                return m_defaultTypeForm.GetForm(objetDonnee, liste);
            return null;
        }*/

        //--------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CReferenceTypeForm>(ref m_defaultTypeForm);
            if ( result )
                result = serializer.TraiteListe<CParametreTypeForm>(m_listeParametres);
            if (!result)
                return result;

            return result;
        }

        //--------------------------------------------------------------------------
        public override string Libelle
        {
            get { return I.T("Conditionnal form|20021"); }
        }
    }
	///////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 
	/// </summary>
	public interface IFormRecepteurFormulaire
	{
		CDbKey IdFormulaireAffiche { get; set;}
		void CreateControles();
	}
}
