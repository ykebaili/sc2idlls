using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Design;
using System.Collections.Generic;


using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.ComponentModel;
using sc2i.formulaire.web;

namespace sc2i.formulaire
{
    public enum EOrientaion
    {
        Horizontal = 1,
        Vertical = 2
    }

	/// <summary>
	/// Description résumée de C2iFenetre.
	/// </summary>
	[Serializable]
	[AWndIcone("ico_panel")]
	[WndName("Child zone")]
	public class C2iWndZoneMultiple : C2iWndComposantFenetre
    {

        public const string c_strIdEvenementAddElement = "ADDELT";
        public const string c_strIdEvenementDeleteElement = "DELELT";
        
		private const int c_nHauteurBandeau = 23;
		private C2iExpression m_formuleSource = null;
        List<CAffectationsProprietes> m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
		Type m_typeSource = null;

		private C2iWndSousFormulaire m_formulaireFils = null;

		private bool m_bHasAddButton = true;
		private bool m_bHasDeleteButton = true;

		private string m_strMessageConfirmationSuppression = "";
        private int m_nbElementParPage = 50;
        private EOrientaion m_orientation = EOrientaion.Vertical;

        private bool m_bUseOptimisations = false;

        //si true la zone se dimensionne en fonction des éléments qu'elle contient
        private bool m_bAutoSize = false;

        private int m_nNumOrdreWeb = 0;
        private string m_strLibelleWeb = "";

        /// ///////////////////////////////////////
        public C2iWndZoneMultiple()
		{
			Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
			Size = new Size(200, 150);
			m_formulaireFils = new C2iWndSousFormulaire();
			m_formulaireFils.Size = new Size(300, 50);
            m_formulaireFils.WndContainer = this;
			m_strMessageConfirmationSuppression = I.T("Delete element ?|10002");
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
		public C2iWndSousFormulaire FormulaireFils
		{
			get
			{
				if (m_formulaireFils != null)
					m_formulaireFils.BackColor = BackColor;
				return m_formulaireFils;
			}
		}


        /// ///////////////////////////////////////
        public bool UseChildOptimization
        {
            get
            {
                return m_bUseOptimisations && m_orientation == EOrientaion.Vertical;
            }
            set
            {
                m_bUseOptimisations = value;
            }
        }

		/// ///////////////////////////////////////
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
        public int NumberOfElementsToDisplayPerPage
        {
            get
            {
                return m_nbElementParPage;
            }
            set
            {
                m_nbElementParPage = value;
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
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle(Position, Size);
			g.FillRectangle(b, rect);

			if (HasAddButton | HasDeleteButton)
			{
				Pen pen = Pens.Black;
				g.DrawLine(pen, rect.Left, rect.Top + c_nHauteurBandeau, rect.Right, rect.Top + c_nHauteurBandeau);
				int nX = 5;
				Brush br = Brushes.Black;
				if ( HasAddButton )
				{
					g.DrawString ( "Add", Font, br, new Point ( rect.X+nX, rect.Top+1));
					nX += 40;
				}
				if ( HasDeleteButton )
				{
					g.DrawString("Delete", Font, br, new Point ( rect.Left + nX, rect.Top+1));
				}
			}
			
			b.Dispose();
			base.MyDraw(ctx);
		}

		/// ///////////////////////////////////////
		public override void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
			int nHeight = 20;
            int nWidth = 30;
			Bitmap bmp = null;
			if (m_formulaireFils != null)
			{
				nHeight = m_formulaireFils.Size.Height;
                nWidth = m_formulaireFils.Size.Width;
				bmp = new Bitmap(m_formulaireFils.Size.Width, m_formulaireFils.Size.Height);
				m_formulaireFils.Position = new Point(0, 0);
				Graphics g = Graphics.FromImage(bmp);
				CContextDessinObjetGraphique ctxDessin = new CContextDessinObjetGraphique(g);
				m_formulaireFils.Draw(ctxDessin);
				g.Dispose();
			}
			Pen pen = Pens.Black;
			Rectangle rc = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            switch (m_orientation)
            {
                case EOrientaion.Horizontal:
                    for (int nX = 0; nX < ClientSize.Width - nWidth; nX += nWidth)
                    {
                        if (bmp != null)
                            ctx.Graphic.DrawImage(bmp, new Point(nX, 0));
                        ctx.Graphic.DrawLine(pen, nX, 0, nX, ClientRect.Height);
                    }
                    break;
                case EOrientaion.Vertical:
			        for (int nY = 0; nY < ClientSize.Height - nHeight; nY += nHeight)
			        {
				        if (bmp != null)
					        ctx.Graphic.DrawImage(bmp, new Point(0, nY));
				        ctx.Graphic.DrawLine(pen, 0, nY, ClientRect.Width, nY);
			        }
                    break;
                default:
                    
                    break;
            }
            if (UseChildOptimization)
            {
                Image img = Resources.Vitesse;
                if (img != null)
                {
                    ctx.Graphic.DrawImage(img, 0, ClientRect.Height - img.Height);
                }
            }
		}


		/// ///////////////////////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		[System.ComponentModel.DisplayName("Source Formula")]
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

        public EOrientaion Orientation
        {
            get
            {
                return m_orientation;
            }
            set
            {
                m_orientation = value;
            }
        }

        /// ///////////////////////////////////////
        public bool AutoSize
        {
            get
            {
                return m_bAutoSize;
            }
            set
            {
                m_bAutoSize = value;
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
            //1 ; ajout du formulaire fils
            //2 : Ajout du message de confirmation de suppression
            //3 : Boutons ajouter et supprimer visibles
            //4 : Ajout du paramètre Nombre d'éléments à afficher par page
            //5 : gestion de liste d'affectations
            //6 : Ajout de la propriét Orientation (Vertical par défaut)
            //7 : Ajout de autosize
            //8 : Ajout de UseChildOptimization (pour utiliser le nouveau contrôle en execution)
            //9 : Ajout des propriétés pour le web
            return 9;
        }

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			result = serializer.TraiteObject<C2iExpression>( ref m_formuleSource );
			if ( !result )
				return result;

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

			bool bHasType = m_typeSource != null;
			serializer.TraiteBool ( ref bHasType );
			if ( bHasType )
			{
				serializer.TraiteType ( ref m_typeSource );
			}
			if (nVersion >= 1)
				result = serializer.TraiteObject<C2iWndSousFormulaire>(ref m_formulaireFils);
            m_formulaireFils.WndContainer = this;

			if (nVersion >= 2)
				serializer.TraiteString(ref m_strMessageConfirmationSuppression);
			if ( nVersion >= 3 )
			{
				serializer.TraiteBool ( ref m_bHasAddButton);
				serializer.TraiteBool ( ref m_bHasDeleteButton);
			}
            if (nVersion >= 4)
                serializer.TraiteInt(ref m_nbElementParPage);

            if (nVersion >= 6)
            {
                int nOrientation = (int)m_orientation;
                serializer.TraiteInt(ref nOrientation);
                m_orientation = (EOrientaion)nOrientation;
            }
            if (nVersion >= 7)
            {
                serializer.TraiteBool(ref m_bAutoSize);
            }
            if (nVersion >= 8)
                serializer.TraiteBool(ref m_bUseOptimisations);
            /*if (nVersion >= 9)
                serializer.TraiteBool(ref m_bAutoScrollInFormulaireFils);*/

            // Ajout des propriétés pour le web
            if (nVersion >= 9)
            {
                serializer.TraiteString(ref m_strLibelleWeb);
                serializer.TraiteInt(ref m_nNumOrdreWeb);
            }

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
			CProprieteAffectationsProprietesEditor.SetTypeSource ( m_typeSource );
			if (SourceFormula != null)
			{
				CProprieteAffectationsProprietesEditor.SetTypeElementAffecte ( SourceFormula.TypeDonnee.TypeDotNetNatif );
			}
			CProprieteAffectationsProprietesEditor.FournisseurProprietes = fournisseurProprietes;
		}

		//-------------------------------------------------------
		public override void OnDesignDoubleClick(Point ptAbsolu)
		{
            EditZoneMultiple();
		}

        //---------------------------------------------------------------------
        public void EditZoneMultiple()
        {
            if (m_formulaireFils == null)
            {
                m_formulaireFils = new C2iWndSousFormulaire();
                m_formulaireFils.Size = new Size(ClientSize.Height, 30);
            }
            if (SourceFormula != null)
            {
                CEditeurSousFormulaire.EditeZone(
                    m_formulaireFils,
                    SourceFormula.TypeDonnee.TypeDotNetNatif,
                    CProprieteAffectationsProprietesEditor.FournisseurProprietes);
            }
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
        /// //////////////////////////////////////////////////
        public string WebLabel
        {
            get
            {
                return m_strLibelleWeb;
            }
            set
            {
                m_strLibelleWeb = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int WebNumOrder
        {
            get
            {
                return m_nNumOrdreWeb;
            }
            set
            {
                m_nNumOrdreWeb = value;
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
                    "LastAddedElement", "LastAddedElement",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, false),
                    true,
                    true,
                    ""));
            }
            if ( SourceFormula != null )
            {
                lst.Add ( new CDefinitionProprieteDynamiqueDeportee(
                    "Source","Source",
                    SourceFormula.TypeDonnee,
                    true,
                    false,"" ));
            }

            lst.Add(new CDefinitionMethodeDynamique(
                "UpdateEditedElement", "UpdateEditedElement",
                new CTypeResultatExpression(typeof(bool), false),
                false));


            return lst.ToArray();
        }


        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementAddElement, "OnAddElement", I.T("Occurs when an element is added|30005")));
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementDeleteElement, "OnDeleteElement", I.T("Occurs when an element is deleted|30006")));
            return lst.ToArray();
        }

        public override void ChercheObjet(object objetCherche, sc2i.common.recherche.CResultatRequeteRechercheObjet resultat)
        {
             base.ChercheObjet(objetCherche, resultat);
             resultat.PushChemin(new CNoeudRechercheObjet_Wnd(this));
             if (m_formulaireFils != null)
                 m_formulaireFils.ChercheObjet(objetCherche, resultat);
             resultat.PopChemin();
        }




	}





	
}
