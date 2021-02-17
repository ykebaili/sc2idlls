using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Design;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using sc2i.formulaire;
using System.Collections.Generic;
using System.ComponentModel;

namespace sc2i.data.dynamic
{
	public interface IReferenceFormEdition : I2iSerializable
	{
	}

	/// <summary>
	/// Description résumée de C2iFenetre.
	/// </summary>
	[Serializable]
	[WndName("Entity list")]
	public class C2iWndListeSpeedStandard : C2iWndComposantFenetre
	{
		private const int c_nHauteurBandeau = 26;
		private C2iExpression m_formuleSource = null;
		//private CAffectationsProprietes m_affectationsInitiales = null;
        private List<CAffectationsProprietes> m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
        private List<CColonneListeSpeedStd> m_colonnes = null;
		private Type m_typeSource = null;

		private bool m_bHasAddButton = true;
		private bool m_bHasDeleteButton = true;
        private bool m_bHasDetailButton = true;
        private bool m_bHasFitlerButton = true;

		private string m_strMessageConfirmationSuppression = "";

		private IReferenceFormEdition m_referenceFormEdition = null;

        private bool m_bUserCustomizable = true;

        private C2iExpression m_formuleElementToEdit = null;

        private bool m_bUseCheckBoxes = false;


		/// ///////////////////////////////////////
		public C2iWndListeSpeedStandard()
		{
			Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
			Size = new Size(200, 150);
			m_strMessageConfirmationSuppression = I.T("Delete element ?|20036");
            LockMode = ELockMode.DisableOnEdit;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

        /// ///////////////////////////////////////
        public bool UserCustomizable
        {
            get
            {
                return m_bUserCustomizable;
            }
            set
            {
                m_bUserCustomizable = value;
            }
        }


		/// ///////////////////////////////////////
		///N'est pas exploitable, conservée pour compatibilité
        [Browsable(false)]
        public string DeleteConfirmMessage
		{
			get
			{
				return m_strMessageConfirmationSuppression;
			}
			set
			{
				m_strMessageConfirmationSuppression = value;
			}
		}

        /// ///////////////////////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        public C2iExpression AlternativeEditedElement
        {
            get
            {
                return m_formuleElementToEdit;
            }
            set
            {
                m_formuleElementToEdit = value;
            }
        }

		/// ///////////////////////////////////////
		[System.ComponentModel.Editor(typeof(CSelecteurDeFormEdition), typeof(UITypeEditor))]
		public IReferenceFormEdition FormToUse
		{
			get
			{
				return m_referenceFormEdition;
			}
			set
			{
				m_referenceFormEdition = value;
			}
		}

		/// ///////////////////////////////////////
		protected override Point OrigineCliente
		{
			get
			{
				if (HasAddButton || HasDeleteButton)
					return new Point(0, c_nHauteurBandeau);
				return new Point(0, 0);
			}
		}

		/// ///////////////////////////////////////
		protected override Size ClientSize
		{
			get
			{
				Size sz = base.ClientSize;
				if (HasAddButton || HasDeleteButton)
					sz = new Size(sz.Width, sz.Height - c_nHauteurBandeau);
				return sz;
			}
		}

		/// ///////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
			Graphics g = ctx.Graphic;
			// Dessine le rectangle principal
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle(Position, Size);
			g.FillRectangle(b, rect);

			//Dessine la barre
			Rectangle rectTitre = new Rectangle(rect.Left, rect.Top, rect.Width, c_nHauteurBandeau);
			Brush br = new LinearGradientBrush(new Point(0, 0),
				new Point(0, rectTitre.Height),
				Color.White,
				Color.FromArgb(189, 189, 255));
			g.FillRectangle(br, rectTitre);
			br.Dispose();


			Pen pen = Pens.Black;
			g.DrawLine(pen, rect.Left, rect.Top + c_nHauteurBandeau, rect.Right, rect.Top + c_nHauteurBandeau);
			int nX = 6;
			int nY = 6;
			Brush brBlue = Brushes.Blue;

			// Dessine le bouton "Filter"
            if ( HasFilterButton )
			    g.DrawString("Filter", Font, brBlue, new Point(rect.X + nX, rect.Top + nY));
			nX += 40;
			// Dessine le bouton "Add"
			if (HasAddButton)
				g.DrawString("Add", Font, brBlue, new Point(rect.X + nX, rect.Top + nY));
			nX += 40;
			// Dessine le bouton "Detail"
            if ( HasDetailButton )  
			    g.DrawString("Detail", Font, brBlue, new Point(rect.X + nX, rect.Top + nY));
			nX += 40;
			// Dessine le bouton "Remove"
			if (HasDeleteButton)
				g.DrawString("Remove", Font, brBlue, new Point(rect.Left + nX, rect.Top + nY));
			// Dessine le bouton "Export"
			g.DrawString("Export", Font, brBlue, new Point(rect.Right - 35, rect.Top + nY));


			b.Dispose();
			base.MyDraw(ctx);
		}

		/// ///////////////////////////////////////
		public override void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
			int nHeight = 20;
			Bitmap bmp = null;

			Pen pen = Pens.Black;
			Rectangle rc = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
			for (int nY = 0; nY < ClientSize.Height - nHeight; nY += nHeight)
			{
				if (bmp != null)
					ctx.Graphic.DrawImage(bmp, new Point(0, nY));
				ctx.Graphic.DrawLine(pen, 0, nY, ClientRect.Width, nY);
			}
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Editor(typeof(CProprieteAffectationsProprietesEditor), typeof(UITypeEditor))]
		public List<CAffectationsProprietes> Affectations
		{
            get
            {
                if (m_listeAffectationsInitiales.Count == 0)
                    m_listeAffectationsInitiales.Add(new CAffectationsProprietes());
                return m_listeAffectationsInitiales;
            }
            set
            {
                m_listeAffectationsInitiales = value;
            }
        }

		[System.ComponentModel.Editor(typeof(CColumnsPropertyEditor), typeof(UITypeEditor))]
		public List<CColonneListeSpeedStd> Columns
		{
			get
			{
				if (m_colonnes == null)
					m_colonnes = new List<CColonneListeSpeedStd>();
				return m_colonnes;

			}
			set
			{
				m_colonnes = value;
			}
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression SourceFormula
		{
			get
			{
				return m_formuleSource;
			}
			set
			{
				m_formuleSource = value;
			}
		}

        public bool UseCheckBoxes
        {
            get
            {
                return m_bUseCheckBoxes;
            }
            set
            {
                m_bUseCheckBoxes = value;
            }
        }

		private CPointeurFiltreDynamiqueInDb m_filter;

		[System.ComponentModel.Editor(typeof(CProprieteFiltreDynamiqueEditor), typeof(UITypeEditor))]
		public CPointeurFiltreDynamiqueInDb Filter
		{
			get
			{
				return m_filter;
			}

			set
			{
				m_filter = value;
			}
		}

        [Serializable]
        public class CPointeurFiltreDynamiqueInDb : I2iSerializable
        {

            // TESTDBKEYOK
            private CDbKey m_dbKeyFilterInDb = null;
            private string m_strLibelle = "";

            public CPointeurFiltreDynamiqueInDb()
            {
            }

            [ExternalReferencedEntityDbKey(typeof(CFiltreDynamiqueInDb))]
            public CDbKey DbKey
            {
                get { return m_dbKeyFilterInDb; }
                set { m_dbKeyFilterInDb = value; }
            }

            public string Libelle
            {
                get { return m_strLibelle; }
                set { m_strLibelle = value; }
            }

            public override string ToString()
            {
                return Libelle;
            }


            private int GetNumVersion()
            {
                // return 0;
                return 1;
            }

            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;

                if (nVersion < 1)
                    // TESTDBKEYOK
                    serializer.ReadDbKeyFromOldId(ref m_dbKeyFilterInDb, typeof(CFiltreDynamiqueInDb));
                else
                    serializer.TraiteDbKey(ref m_dbKeyFilterInDb);
                serializer.TraiteString(ref m_strLibelle);

                return result;
            }
        }


		/// ///////////////////////////////////////
		public Type TypeSource
		{
			set
			{
				m_typeSource = value;
			}
			get
			{
				return m_typeSource;
			}
		}
        /// ///////////////////////////////////////
        private int GetNumVersion()
        {
            //0 ; Version initiale
            //1 : Ajout de la référence à la form d'éditino
            //2 : ajout du UserCustomisable
            //3 : ajout de HasDetailButton et AlternativeelementToEdit
            //4 : Utiliser les cases à cocher
            //5 : Passage en Liste des Affections
            //6 : Ajout de HasFilterButton
            return 6;
        }
		

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			// Sérialise le formule source
			result = serializer.TraiteObject<C2iExpression>(ref m_formuleSource);
			if (!result)
				return result;
			// Sérialise les colonnes
			result = serializer.TraiteListe<CColonneListeSpeedStd>(Columns);
			if (!result)
				return result;

			// Sérialise les affectations des nouveaux objets
            //result = serializer.TraiteObject<CAffectationsProprietes>(ref m_affectationsInitiales);
            //if (!result)
            //    return result;
            if (nVersion < 5 && serializer.Mode == ModeSerialisation.Lecture)
            {
                CAffectationsProprietes affectation = null;
                result = serializer.TraiteObject<CAffectationsProprietes>(ref affectation);
                if (!result)
                    return result;
                m_listeAffectationsInitiales.Clear();
                m_listeAffectationsInitiales.Add(affectation);
            }
            else
            {
                result = serializer.TraiteListe<CAffectationsProprietes>(m_listeAffectationsInitiales);
                if (!result)
                    return result;
            }

			result = serializer.TraiteObject<CPointeurFiltreDynamiqueInDb>(ref m_filter);
			if (!result)
				return result;


			bool bHasType = m_typeSource != null;
			serializer.TraiteBool(ref bHasType);
			if (bHasType)
			{
				serializer.TraiteType(ref m_typeSource);
			}
			serializer.TraiteString(ref m_strMessageConfirmationSuppression);
			serializer.TraiteBool(ref m_bHasAddButton);
			serializer.TraiteBool(ref m_bHasDeleteButton);

			if (nVersion >= 1)
			{
				result = serializer.TraiteObject<IReferenceFormEdition>(ref m_referenceFormEdition);
			}
			else
				m_referenceFormEdition = null;
            if ( !result ) 
                return result;

            if ( nVersion >= 2 )
                serializer.TraiteBool ( ref m_bUserCustomizable);

            if (nVersion >= 3)
            {
                serializer.TraiteBool(ref m_bHasDetailButton);
                serializer.TraiteObject<C2iExpression>(ref m_formuleElementToEdit);
            }

            if ( nVersion >= 4 )
            {
                serializer.TraiteBool ( ref m_bUseCheckBoxes );
            }
            if (nVersion >= 6)
                serializer.TraiteBool(ref m_bHasFitlerButton);


			return result;
		}

		//-------------------------------------------------------
		public override void OnDesignSelect(
			Type typeEdite,
			object elementEdite,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, elementEdite, fournisseurProprietes);
			m_typeSource = typeEdite;
			CProprieteAffectationsProprietesEditor.SetTypeSource(m_typeSource);
			if (SourceFormula != null)
			{
				CProprieteAffectationsProprietesEditor.SetTypeElementAffecte(SourceFormula.TypeDonnee.TypeDotNetNatif);
				CProprieteFiltreDynamiqueEditor.SetTypeElement(SourceFormula.TypeDonnee.TypeDotNetNatif);
				CSelecteurDeFormEdition.SetTypeElement(SourceFormula.TypeDonnee.TypeDotNetNatif);
			}
			CProprieteAffectationsProprietesEditor.FournisseurProprietes = fournisseurProprietes;

			CColumnsPropertyEditor.ListeEditee = this;

			

		}


		//-------------------------------------------------------
		public override void OnDesignCreate(Type typeEdite)
		{
			m_typeSource = typeEdite;
		}

		/// ///////////////////////////////////////
		public bool HasAddButton
		{
			get
			{
				return m_bHasAddButton;
			}
			set
			{
				m_bHasAddButton = value;

			}
		}


        /// ///////////////////////////////////////
        public bool HasDetailButton
        {
            get
            {
                return m_bHasDetailButton;
            }
            set
            {
                m_bHasDetailButton = value;
            }
        }

        /// ///////////////////////////////////////
        public bool HasFilterButton
        {
            get
            {
                return m_bHasFitlerButton;
            }
            set
            {
                m_bHasFitlerButton = value;
            }
        }

		/// ///////////////////////////////////////
		public bool HasDeleteButton
		{
			get
			{
				return m_bHasDeleteButton;
			}
			set
			{
				m_bHasDeleteButton = value;
			}
		}

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "HasAddButton", "HasAddButton",
                new CTypeResultatExpression(typeof(bool), false),
                false,
                false,
                ""));

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "HasDeleteButton", "HasDeleteButton",
                new CTypeResultatExpression(typeof(bool), false),
                false,
                false,
                ""));

            if (SourceFormula != null)
            {
                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "SelectedElement", "SelectedElement",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, false),
                    true,
                    true,
                    ""));

                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "SourceList", "SourceList",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, true),
                    true,
                    false,
                    ""));

                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "CheckedElements", "CheckedElements",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, true),
                    true,
                    false, ""));
            }



            

            return lst.ToArray();
        }

        public const string c_strIdEvenementSelectionChanged = "SELECTION_CHANGED";
        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = 
                new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            
            lst.Add(new CDescriptionEvenementParFormule(
                c_strIdEvenementSelectionChanged,
                "OnChangeSelection",
                I.T("Occurs when selected element is changed|10000")));
            
            return lst.ToArray();
        }

		///////////////////////////////////////////////////////////////////////////////////////////
		public class CColonneListeSpeedStd : I2iSerializable
		{

			private int m_nWidth = 100;
			private string m_strTitre = "";
            private CInfoChampDynamique m_infoChampDynamique = null;
			


			//---- Constructeur ----------------------------------------
			public CColonneListeSpeedStd()
			{
			}

			//----------------------------------------------
			// Largeur de la colonne
			public int Width
			{
				get
				{
					return m_nWidth;
				}
				set
				{
					m_nWidth = Math.Max(0, value);
				}
			}

			// Titre de la colonne
			public string Titre
			{
				get { return m_strTitre; }
				set { m_strTitre = value; }
			}

            /// <summary>
            /// Propriété affichée
            /// </summary>
            public CInfoChampDynamique InfoChampDynamique
            {
                get
                {
                    return m_infoChampDynamique;
                }
                set
                {
                    m_infoChampDynamique = value;
                }
            }
			#region I2iSerializable Membres

			private int GetNumVersion()
			{
				return 1;
                //Remplacement de CDefinitionProprieteDynamique par m_strPropriete
			}

			public CResultAErreur Serialize(C2iSerializer serializer)
			{
				int nVersion = GetNumVersion();
				CResultAErreur result = serializer.TraiteVersion(ref nVersion);
				if (!result)
					return result;

				serializer.TraiteInt(ref m_nWidth);
				serializer.TraiteString(ref m_strTitre);
                if (nVersion < 1)
                {
                    CDefinitionProprieteDynamique def = null;
                    result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                    if (serializer.Mode == ModeSerialisation.Lecture)
                    {
                        m_infoChampDynamique = new CInfoChampDynamique(
                            def.Nom,
                            def.TypeDonnee.TypeDotNetNatif,
                            def.NomPropriete,
                            "",
                            null);
                    }
                }
                else
                    serializer.TraiteObject<CInfoChampDynamique>(ref m_infoChampDynamique);
				return result;
			}

			#endregion
		}
	}

	//-----------------------------------------------------
	public interface ISelectionneurReferenceFormEdition
	{
		IReferenceFormEdition GetReferenceFormEdition(Type typeElements, IServiceProvider provider, object value);
	}
	/// <summary>
	/// Sélectionneur de IReferenceFormEdition
	/// </summary>
	public class CSelecteurDeFormEdition : UITypeEditor
	{
		private static Type m_typeEditeur;

		private static Type m_typeElements;

		public static void SetTypeElement(Type typeElement)
		{
			m_typeElements = typeElement;
		}


		public static void SetTypeEditeur(Type typeEditeur)
		{
			m_typeEditeur = typeEditeur;

		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ISelectionneurReferenceFormEdition ed = (ISelectionneurReferenceFormEdition)Activator.CreateInstance(m_typeEditeur);
			return ed.GetReferenceFormEdition(m_typeElements, provider, value);
		}

		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if (m_typeEditeur != null)
				return UITypeEditorEditStyle.DropDown;
			else
				return UITypeEditorEditStyle.None;
		}
	}
}