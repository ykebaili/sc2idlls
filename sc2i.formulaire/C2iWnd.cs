using System;
using System.Drawing;
using System.Linq;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Collections;
using System.IO;
using System.Xml;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.ComponentModel;
using System.Collections.Generic;
using sc2i.common.recherche;

namespace sc2i.formulaire
{
	public enum EDockStyle
	{
		None = 0,
		Top,
		Bottom,
		Left,
		Right,
		Fill
	}

	public interface IWndAChildNomme
	{
		IWndAChildNomme GetChildFromName(string strName);
	}

    

    public interface IWndAContainer
    {
        IWndAContainer WndContainer{get;set;}
    }

    


	/// <summary>
	/// Description résumée de CComposantFenetre.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class C2iWnd : C2iObjetGraphique, 
        IElementAVariableInstance, 
        IElementAEvenementParFormule, 
        IWndAChildNomme,
        IWndAContainer,
        IObjetAIContexteDonnee
	{
        public enum ELockMode
        {
            EnableOnEdit = 0,
            DisableOnEdit = 1,
            Independant = 2
        }

		public const string c_strIdEvenementOnInit = "FORM_INIT";

		private bool m_bCouleurFondAutomatique = true;
		private Color m_backColor = Color.White;
		private Color m_foreColor = Color.Black;
		private ArrayList m_listeFils = new ArrayList();
		private Font m_font = null;
		private int m_nTabOrder = 0;
		private bool m_bAnchorLeft = true;
		private bool m_bAnchorRight = false;
		private bool m_bAnchorTop = true;
		private bool m_bAnchorBottom = false;
        private ELockMode m_lockMode = ELockMode.EnableOnEdit;
        private string m_strHelpText = "";

		private C2iExpression m_expressionVisibilite = null;
		private C2iExpression m_expressionEnable = null;


		public override event EventHandlerChild ChildAdded;
		public override event EventHandlerChild ChildRemoved;

		public EDockStyle m_dockStyle = EDockStyle.None;

		private bool m_bIsDocking = false;

		private string m_strName = "";

		private List<CHandlerEvenementParFormule> m_listeHandlers = new List<CHandlerEvenementParFormule>();

        [NonSerialized]
        private IContexteDonnee m_contexteDonnee = null;

		/// <summary>
		/// Indique le type de l'élement édite par ce contrôle
		/// </summary>
		private Type m_typeEdite = null;

		/// ///////////////////////////////////////////////
		public C2iWnd()
		{

		}

		/// ///////////////////////////////////////////////
        public virtual string Name
		{
			get
			{
				return m_strName;
			}
			set
			{
				m_strName = value.Trim();
			}
		}

        /// ///////////////////////////////////////////////
        public virtual string HelpText
        {
            get
            {
                return m_strHelpText;
            }
            set
            {
                m_strHelpText = value;
            }
        }

        /// ///////////////////////////////////////////////
        public virtual ELockMode LockMode
        {
            get
            {
                return m_lockMode;
            }
            set
            {
                m_lockMode = value;
            }
        }

		/// ///////////////////////////////////////////////
		///Retourne true si ce type de contrôle peut être utilisée
		///avec un élement édité de type tp
		public abstract bool CanBeUseOnType ( Type tp );


		/// ///////////////////////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        public virtual C2iExpression Visiblity
		{
			get
			{
				return m_expressionVisibilite;
			}
			set
			{
				m_expressionVisibilite = value;
			}
		}

		/// ///////////////////////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        public virtual C2iExpression Enabled
		{
			get
			{
				return m_expressionEnable;
			}
			set
			{
				m_expressionEnable = value;
			}
		}

		/// ///////////////////////////////////////////////
		public virtual bool AutoBackColor
		{
			get
			{
				return m_bCouleurFondAutomatique;
			}
			set
			{
				m_bCouleurFondAutomatique = value;
			}
		}

		/// ///////////////////////////////////////////////
		protected bool IsDocking
		{
			get
			{
				return m_bIsDocking;
			}
		}

		/// ///////////////////////////////////////////////
		public virtual EDockStyle DockStyle
		{
			get
			{
				return m_dockStyle;
			}
			set
			{
				EDockStyle oldValue = m_dockStyle;
				m_dockStyle = value;
				if (value != oldValue)
				{
					if (Parent != null && Parent is C2iWnd)
						((C2iWnd)Parent).DockChilds();
				}
			}
		}


		/// ///////////////////////////////////////////////
		public virtual Font Font
		{
			get
			{
				if (m_font == null && Parent != null)
					return ((C2iWnd)Parent).Font;
				return m_font;
			}
			set
			{
				m_font = value;
			}
		}


		/// ///////////////////////////////////////////////
        public virtual int TabOrder
		{
			get
			{
				return m_nTabOrder;
			}
			set
			{
				m_nTabOrder = value;
			}
		}

		/// ///////////////////////////////////////////////
		public virtual bool AnchorLeft
		{
			get
			{
				return m_bAnchorLeft;
			}
			set
			{
				m_bAnchorLeft = value;
			}
		}

		/// ///////////////////////////////////////////////
        public virtual bool AnchorRight
		{
			get
			{
				return m_bAnchorRight;
			}
			set
			{
				m_bAnchorRight = value;
			}
		}

		/// ///////////////////////////////////////////////
        public virtual bool AnchorTop
		{
			get
			{
				return m_bAnchorTop;
			}
			set
			{
				m_bAnchorTop = value;
			}
		}

		/// ///////////////////////////////////////////////
        public virtual bool AnchorBottom
		{
			get
			{
				return m_bAnchorBottom;
			}
			set
			{
				m_bAnchorBottom = value;
			}
		}



		/// ///////////////////////////////////////////////
		public virtual Color BackColor
		{
			get
			{
				return m_backColor;
			}
			set
			{
				//Propage la couleur de fond à ses fils
				foreach (C2iWnd fils in m_listeFils)
                    if (fils.BackColor == m_backColor && fils.AutoBackColor)
                    {
                        fils.BackColor = value;
                        fils.AutoBackColor = true;
                    }
				m_backColor = value;
				if (Parent != null)
				{
					if (((C2iWnd)Parent).BackColor == BackColor)
						AutoBackColor = true;
					else
						AutoBackColor = false;
				}
			}
		}

		/// ///////////////////////////////////////////////
        public virtual Color ForeColor
		{
			get
			{
				return m_foreColor;
			}
			set
			{
				m_foreColor = value;
			}
		}

        /// //////////////////////////////////
        public virtual Rectangle GetLocalClipRect()
        {
            return new Rectangle(0, 0, ClientSize.Width + 1, ClientSize.Height + 1);
        }


		/// //////////////////////////////////
		/// <summary>
		/// Dessin de tout le controle et de ses fils
		/// </summary>
		/// <param name="contexte"></param>
		public override void Draw(CContextDessinObjetGraphique ctx)
		{
			MyDraw(ctx);
			Graphics g = ctx.Graphic;
			Matrix oldMat = g.Transform;
			Matrix matrice = (Matrix)oldMat.Clone();
			Point pt = ClientToGlobal(new Point(0, 0));
			if (Parent != null)
				pt = Parent.GlobalToClient(pt);
			//pt = ClientToGlobal ( pt );
			matrice.Translate(pt.X, pt.Y);
			g.Transform = matrice;
			Region oldClip = g.Clip;
            Region newClip = oldClip.Clone();
            newClip.Intersect(GetLocalClipRect());

            g.Clip = newClip;
			DrawInterieur(ctx);
			foreach (C2iWnd fils in Childs)
				fils.Draw(ctx);
			g.Clip = oldClip;
            newClip.Dispose();
			g.Transform = oldMat;
		}



		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			//DrawInterieur ( g );
		}



		/// ///////////////////////////////////////////////
		/// <summary>
		/// Dessin de l'interieur du control
		/// </summary>
		/// <param name="contexte"></param>
		public virtual void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
		}



		[System.ComponentModel.Browsable(false)]
		/// ///////////////////////////////////////////////
		public virtual void DrawRectangle(Rectangle rect, Graphics g)
		{
			Brush brush = new SolidBrush(BackColor);
			Pen pen = new Pen(ForeColor);
			g.FillRectangle(brush, rect);
			g.DrawRectangle(pen, rect);
			brush.Dispose();
			pen.Dispose();
		}

		/// ///////////////////////////////////////////////
		public bool IsChildOf(C2iWnd wnd)
		{
			if (Parent == wnd)
				return true;
			if (Parent != null)
				return Parent.IsChildOf(wnd);
			return false;
		}


		/// ///////////////////////////////////////////////
		/// <summary>
		/// Retourne la fenêtre sous la souris, à partir des coordonnées globales
		/// </summary>
		/// <param name="pt"></param>
		/// <param name="bAcceptChilds"></param>
		/// <returns></returns>
		public virtual C2iWnd WindowAtPoint(Point pt, bool bAcceptChilds, C2iWnd excludeParent)
		{
			if (this == excludeParent)
				return null;
			C2iWnd fenFille = null;
			ArrayList lst = new ArrayList();
			foreach (object obj in Childs)
				lst.Insert(0, obj);
			foreach (C2iWnd fen in lst)
			{
				fenFille = fen.WindowAtPoint(pt, bAcceptChilds, excludeParent);
				if (fenFille != null)
					break;
			}
			if (fenFille == null)
			{
				Rectangle rect = new Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
				if (Parent != null)
					rect = Parent.ClientToGlobal(rect);
				if (rect.Contains(pt.X, pt.Y) && (AcceptChilds || !bAcceptChilds))
					return this;
			}
			return fenFille;
		}

		/// //////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 12;
			//Version 2 : hérite de C2iObjetGraphique
			//Version 3 : Formule de visiblité
			//Version 4 : Couleur fond automatiuqe
			//Version 5 : Formule Enable
			//Version 6 : Anchors
			//Version 7 : Name
			//Version 8 : DockStyle
			//Version 9 : Ajout des handlers d'évenements
			//Version 10 : ajout du type d'élément édité
            //Version 11 : Ajout du ELockMode
            //Version 12 : Ajout de HelpText
		}

		/// //////////////////////////////////////////////
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (nVersion >= 2)
			{
				result = base.Serialize(serializer);
			}
			if (result)
				result = PrivateSerialize(serializer, nVersion);
			if (result && nVersion < 2)
				result = MySerialize(serializer);
			if (!result)
				return result;
			result = serializer.TraiteArrayListOf2iSerializable(m_listeFils);
			if (!result)
				return result;
			foreach (C2iWnd wnd in m_listeFils)
				wnd.Parent = this;

			if (nVersion > 2)
			{
				I2iSerializable objet = m_expressionVisibilite;
				result = serializer.TraiteObject(ref objet);
				if (!result)
					return result;
				m_expressionVisibilite = (C2iExpression)objet;
			}
			else
				m_expressionVisibilite = null;
			if (nVersion >= 5)
			{
				I2iSerializable objet = m_expressionEnable;
				result = serializer.TraiteObject(ref objet);
				if (!result)
					return result;
				m_expressionEnable = (C2iExpression)objet;
			}
			else
				m_expressionEnable = null;

			if (nVersion >= 6)
			{
				serializer.TraiteBool(ref m_bAnchorBottom);
				serializer.TraiteBool(ref m_bAnchorLeft);
				serializer.TraiteBool(ref m_bAnchorRight);
				serializer.TraiteBool(ref m_bAnchorTop);
			}
			if (nVersion >= 7)
				serializer.TraiteString(ref m_strName);
			else
				m_strName = "";

			if (nVersion >= 8)
			{
				int nTmp = (int)DockStyle;
				serializer.TraiteInt(ref nTmp);
				DockStyle = (EDockStyle)nTmp;
				DockChilds();
			}
			if (nVersion >= 9)
			{
				result = serializer.TraiteListe<CHandlerEvenementParFormule>(m_listeHandlers);
				if (!result)
					return result;
			}
			else
				m_listeHandlers = new List<CHandlerEvenementParFormule>();
			if (nVersion >= 10)
			{
				bool bHasType = m_typeEdite != null;
				serializer.TraiteBool(ref bHasType);
				if (bHasType)
					serializer.TraiteType(ref m_typeEdite);
			}
			else
				m_typeEdite = null;
            if (nVersion >= 11)
            {
                int nTmp = (int)m_lockMode;
                serializer.TraiteInt(ref nTmp);
                m_lockMode = (ELockMode)nTmp;
            }
            if (nVersion >= 12)
            {
                serializer.TraiteString(ref m_strHelpText);
            }
			return result;
		}

		public static CResultAErreur SerializeFont(C2iSerializer serializer, ref Font ft)
		{
			CResultAErreur result = CResultAErreur.True;
			bool bHasFont = ft != null;
			serializer.TraiteBool(ref bHasFont);
			if (bHasFont)
			{
				if (serializer.Mode == ModeSerialisation.Lecture)
					ft = new Font("Arial", 1, FontStyle.Regular);
				Byte gdiCharset = ft.GdiCharSet;
				bool gdiVerticalFont = ft.GdiVerticalFont;
				int nUnit = (int)ft.Unit;
				string strName = ft.Name;
				double fSize = ft.Size;
				int nStyle = (int)ft.Style;
				serializer.TraiteByte(ref gdiCharset);
				serializer.TraiteBool(ref gdiVerticalFont);
				serializer.TraiteString(ref strName);
				serializer.TraiteDouble(ref fSize);
				serializer.TraiteInt(ref nStyle);
				serializer.TraiteInt(ref nUnit);
				if (serializer.Mode == ModeSerialisation.Lecture)
					ft = new Font(strName, (float)fSize, (FontStyle)nStyle, (GraphicsUnit)nUnit, gdiCharset, gdiVerticalFont);
			}
			return result;
		}

		/// //////////////////////////////////////////////
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				Size oldValue = Size;
				base.Size = value;
				if (value != oldValue)
					DockChilds();
				if (!IsDocking && Parent is C2iWnd)
					((C2iWnd)Parent).DockChilds();
			}
		}

		/// //////////////////////////////////////////////
		public override Point Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				base.Position = value;
				if (!IsDocking && Parent is C2iWnd && DockStyle != EDockStyle.None)
					((C2iWnd)Parent).DockChilds();

			}
		}

		/// //////////////////////////////////////////////
		private CResultAErreur PrivateSerialize(C2iSerializer serializer, int nVersion)
		{
			int nTmp;
			if (nVersion < 2)
			{
				int nWidth = Size.Width;
				serializer.TraiteInt(ref nWidth);

				int nHeight = Size.Height;
				serializer.TraiteInt(ref nHeight);
				Size = new Size(nWidth, nHeight);
			}

			nTmp = m_backColor.ToArgb();
			serializer.TraiteInt(ref nTmp);
			m_backColor = Color.FromArgb(nTmp);

			nTmp = m_foreColor.ToArgb();
			serializer.TraiteInt(ref nTmp);
			m_foreColor = Color.FromArgb(nTmp);

			if (nVersion < 2)
			{
				int nX = Position.X;
				serializer.TraiteInt(ref nX);


				int nY = Position.Y;
				serializer.TraiteInt(ref nY);
				Position = new Point(nX, nY);

				bool bIsLock = IsLock;
				serializer.TraiteBool(ref bIsLock);
				IsLock = bIsLock;
			}
			CResultAErreur result = CResultAErreur.True;
			result = SerializeFont(serializer, ref m_font);
			if (!result)
				return result;
			/*bool bHasFont = m_font != null;
			serializer.TraiteBool ( ref bHasFont );
			if ( bHasFont )
			{
				if ( serializer.Mode == ModeSerialisation.Lecture )
					m_font = new Font("Arial",1, FontStyle.Regular);

				Byte gdiCharset = 
#if PDA
					0;
#else
				m_font.GdiCharSet;
#endif
				bool gdiVerticalFont = 
#if PDA
					true;
#else
					m_font.GdiVerticalFont;
#endif

				int nUnit = 
#if PDA
					(int)0;
#else
					(int)m_font.Unit;
#endif
				string strName = m_font.Name;
				double fSize = m_font.Size;
				int nStyle = (int)m_font.Style;
				serializer.TraiteByte ( ref gdiCharset );
				serializer.TraiteBool ( ref gdiVerticalFont );
				serializer.TraiteString( ref strName );
				serializer.TraiteDouble ( ref fSize );
				serializer.TraiteInt ( ref nStyle );
				serializer.TraiteInt ( ref nUnit );
				if ( serializer.Mode == ModeSerialisation.Lecture )
#if PDA
					m_font = new Font (strName, (float)fSize, (FontStyle) nStyle );
#else
				m_font = new Font (strName, (float)fSize, (FontStyle) nStyle, (GraphicsUnit)nUnit, gdiCharset, gdiVerticalFont );
#endif
			}*/

			if (nVersion >= 1)
				serializer.TraiteInt(ref m_nTabOrder);
			if (nVersion >= 4)
				serializer.TraiteBool(ref m_bCouleurFondAutomatique);
			return CResultAErreur.True;
		}



		/// //////////////////////////////////////////////////
		public override bool AddChild(I2iObjetGraphique child)
		{
			m_listeFils.Add(child);
			if (child is C2iWnd && ((C2iWnd)child).AutoBackColor)
			{
				C2iWnd wndChild = (C2iWnd)child;
				wndChild.BackColor = BackColor;
				wndChild.ForeColor = ForeColor;
			}
			if (ChildAdded != null)
				ChildAdded(child);
			DockChilds();
			return true;
		}

		/// //////////////////////////////////
		public override bool ContainsChild(I2iObjetGraphique child)
		{
			return m_listeFils.Contains(child);
		}


		/// //////////////////////////////////////////////////
		public override void RemoveChild(I2iObjetGraphique child)
		{
			m_listeFils.Remove(child);
			if (ChildRemoved != null)
				ChildRemoved(child);
			DockChilds();
		}

		/// //////////////////////////////////////////////
		[Browsable(false)]
        public override I2iObjetGraphique[] Childs
		{
			get
			{
				return (I2iObjetGraphique[])m_listeFils.ToArray(typeof(I2iObjetGraphique));
			}
		}

        

        

		/// //////////////////////////////////////////////
		public C2iWnd GetChild(string strName)
		{
			foreach (C2iWnd wnd in Childs)
				if (wnd.Name.ToUpper() == strName)
					return wnd;
			return null;
		}

        //-------------------------------------------------------------
		public override void BringToFront(I2iObjetGraphique child)
		{
			if (!ContainsChild(child))
				return;
			m_listeFils.Remove(child);
			m_listeFils.Add(child);
			DockChilds();

		}

        //-------------------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
		{
			if (!ContainsChild(child))
				return;
			m_listeFils.Remove(child);
			m_listeFils.Insert(0, child);
			DockChilds();
		}

        //-------------------------------------------------------------
        public void Front1(I2iObjetGraphique child)
        {
            if (!ContainsChild(child))
                return;
            int nIndex = m_listeFils.IndexOf(child);
            if (nIndex < m_listeFils.Count - 1)
            {
                m_listeFils.RemoveAt(nIndex);
                if (nIndex + 1 < m_listeFils.Count )
                    m_listeFils.Insert(nIndex + 1, child);
                else
                    m_listeFils.Add(child);
            }
            DockChilds();
        }

        //-------------------------------------------------------------
        public void Back1(I2iObjetGraphique child)
        {
            if (!ContainsChild(child))
                return;
            int nIndex = m_listeFils.IndexOf(child);
            if (nIndex > 0)
            {
                m_listeFils.RemoveAt(nIndex);
                m_listeFils.Insert(nIndex - 1, child);
            }
            DockChilds();
        }



		/// //////////////////////////////////////////////////
		protected void DrawCadre3D(Rectangle rect, bool bInner, Graphics g)
		{
			Pen[] pens = new Pen[4];
			if (bInner)
			{
				pens[0] = SystemPens.ControlDark;
				pens[1] = SystemPens.ControlDarkDark;
				pens[2] = SystemPens.ControlLightLight;
				pens[3] = SystemPens.ControlLight;
			}
			else
			{
				pens[0] = SystemPens.ControlLight;
				pens[1] = SystemPens.ControlLightLight;
				pens[2] = SystemPens.ControlDarkDark;
				pens[3] = SystemPens.ControlDark;
			}
			g.DrawLines(pens[0],
				new Point[]
				{
					new Point ( rect.Left, rect.Bottom-1),
					new Point ( rect.Left, rect.Top ),
					new Point ( rect.Right-1, rect.Top ) 
				});
			g.DrawLines(pens[1],
				new Point[]
				{
					new Point ( rect.Left+1, rect.Bottom-2),
					new Point (rect.Left+1, rect.Top+1),
					new Point ( rect.Right-1, rect.Top+1)
				});
			g.DrawLines(pens[2],
				new Point[]
				{
					new Point ( rect.Left, rect.Bottom ),
					new Point ( rect.Right, rect.Bottom ),
					new Point ( rect.Right, rect.Top ) 
				});
			g.DrawLines(pens[3],
				new Point[]
				{
					new Point ( rect.Left+1, rect.Bottom-1 ),
					new Point ( rect.Right-1, rect.Bottom-1),
					new Point ( rect.Right-1, rect.Top+1 )
				});
		}

		/// <summary>
		/// Indique que le contrôle a été sélectionné dans le designer
		/// et lui passe le type en cours d'édition ainsi que le fournisseur
		/// de propriétés en cours d'utilisation
		/// </summary>
		/// <remarks>
		/// Lorsqu'on clique sur un élément dans l'édition de formulaires,
		/// cette méthode est appellée. Elle permet au contrôle de modifier
		/// éventuellement les éditeurs personnalisés en fonction du type d'élément édité
		/// </remarks>
		/// <param name="typeEdite"></param>
		public virtual void OnDesignSelect ( 
			Type typeEdite, 
			object objetEdite,
			IFournisseurProprietesDynamiques fournisseurProprietes )
		{
            C2iWnd parent = this;
            while (parent.Parent as C2iWnd != null)
                parent = parent.Parent as C2iWnd;
            CProprieteExpressionEditor.ObjetPourSousProprietes = GetObjetPourAnalyseFormule(typeEdite);
			CProprieteExpressionEditor.FournisseurProprietes = fournisseurProprietes;
		}

		/// <summary>
		/// Se déclenche après que le designer ai créé l'élément
		/// </summary>
		/// <param name="m_typeEdite"></param>
		public virtual void OnDesignCreate (Type typeEdite)
		{
		}

		public virtual void DockChilds()
		{
			Rectangle rc = ClientRect;
			foreach (C2iWnd wnd in Childs)
			{
				if (rc.Height > 2 && rc.Width > 2)
				{
					wnd.m_bIsDocking = true;
					switch (wnd.DockStyle)
					{
						case EDockStyle.Top:
							wnd.Size = new Size(rc.Width, wnd.Size.Height);
							wnd.Position = rc.Location;
							rc.Offset(0, wnd.Size.Height);
							rc.Height -= wnd.Size.Height;
							break;
						case EDockStyle.Bottom:
							wnd.Size = new Size(rc.Width, wnd.Size.Height);
							wnd.Position = new Point(rc.Location.X, rc.Bottom - wnd.Size.Height);
							rc.Height -= wnd.Size.Height;
							break;
						case EDockStyle.Left:
							wnd.Size = new Size(wnd.Size.Width, rc.Height);
							wnd.Position = rc.Location;
							rc.Offset(wnd.Size.Width, 0);
							rc.Width -= wnd.Size.Width;
							break;
						case EDockStyle.Right:
							wnd.Size = new Size(wnd.Size.Width, rc.Height);
							wnd.Position = new Point(rc.Right - wnd.Size.Width, rc.Top);
							rc.Width -= wnd.Size.Width;
							break;
						case EDockStyle.Fill:
							wnd.Size = rc.Size;
							wnd.Position = rc.Location;
							break;
					}
					wnd.m_bIsDocking = false;
				}
			}
		}

		/// ////////////////////////////////////////////////////////
		public static Image GetImage(Type tp)
		{
			try
			{

				//Nom d'image : le nom du type suivi de gif
				string[] strNoms = tp.ToString().Split('.');
				string strNomImage = strNoms[strNoms.Length - 1] + ".bmp";
				Image img = DynamicClassAttribute.GetImage ( tp );
                if (img != null)
                    return img;
                try
                {
                    img = new Bitmap(tp, strNomImage);
                }
                catch{}
                if (img == null)
                {
                    strNomImage = strNomImage[strNomImage.Length] - 1 + ".png";
                    img = new Bitmap(tp, strNomImage);
                }
				return img;
			}
			catch { }
			return null;
		}


		protected virtual CDefinitionProprieteDynamique[] GetProprietesStandard()
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			//Ajoute les propriétés
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"BackColor", "BackColor",
				new CTypeResultatExpression(typeof(Color), false),
				false, false, ""));

			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"ForeColor", "ForeColor",
				new CTypeResultatExpression(typeof(Color), false),
				false, false, ""));

			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Visible", "Visible",
				new CTypeResultatExpression(typeof(bool), false),
				false, false, ""));

			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Enabled", "Enabled",
				new CTypeResultatExpression(typeof(bool), false),
				false, false, ""));

			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Height", "Height",
				new CTypeResultatExpression(typeof(int), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Width", "Width",
				new CTypeResultatExpression(typeof(int), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"X", "X",
				new CTypeResultatExpression(typeof(int), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Y", "Y",
				new CTypeResultatExpression(typeof(int), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontBold", "FontBold",
				new CTypeResultatExpression(typeof(bool), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontItalic", "FontItalic",
				new CTypeResultatExpression(typeof(bool), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontUnderline", "FontUnderline",
				new CTypeResultatExpression(typeof(bool), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontStrikeOut", "FontStrikeOut",
				new CTypeResultatExpression(typeof(bool), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontName", "FontName",
				new CTypeResultatExpression(typeof(string), false),
				false,
				false,
				""));
			lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
				"FontSize", "FontSize",
				new CTypeResultatExpression(typeof(double), false),
				false,
				false,
				""));

			if ( m_typeEdite != null )
			{
				lstProps.Add ( new CDefinitionProprieteDynamiqueDeportee(
					"EditedElement", "EditedElement",
					new CTypeResultatExpression(m_typeEdite, false),
					true,
					true,
					""));
			}

			lstProps.Add(new CDefinitionMethodeDynamique(
                "Refresh", "RefillControl",
				new CTypeResultatExpression(typeof(void), false),
				false));

			lstProps.Add(new CDefinitionMethodeDynamique(
				"SetError", "SetError",
				new CTypeResultatExpression(typeof(void), false),
				false,
				I.T("Indicate an error on the control|20006"),
				new string[] { I.T("Error text|20007") }));

            lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
                "ChildControls", "ChildControls",
                new CTypeResultatExpression(typeof(C2iWnd), true),
                false, false, ""));

            lstProps.Add(new CDefinitionProprieteDynamiqueDeportee(
                "HelpText", "HelpText",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));

            lstProps.Add(new CDefinitionMethodeDynamique(
                "GetParameter", "GetParameter",
                new CTypeResultatExpression(typeof(object), false),
                false,
                I.T("Find a specific parameter value from one of its parent|20028"),
                new string[] { I.T("Parameter name|20027") }));


			return lstProps.ToArray();

		}

		/// <summary>
		/// Fausse méthode pour que dynamic method fonctionne
		/// </summary>
		/// <param name="strErreur"></param>
		[DynamicMethod("Indicate an error on the control", "Error text")]
		public void SetError(string strErreur)
		{
		}

        private void GetDefinitionsChildsNommés(List<CDefinitionProprieteDynamique> lstProps, Dictionary<string, bool> controlesANePasAjouter)
        {
            foreach (C2iWnd wnd in Childs)
            {
                string strUpper = wnd.Name.ToUpper().Trim();
                if (wnd.Name.Length > 0 &&
                    !controlesANePasAjouter.ContainsKey(strUpper))
                {
                    lstProps.Add(new CDefinitionProprieteDynamiqueWndFils(wnd, wnd.Name));
                    controlesANePasAjouter.Add(strUpper, true);
                }
                wnd.GetDefinitionsChildsNommés(lstProps, controlesANePasAjouter);
            }
        }

	

		public virtual CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>(GetProprietesStandard());

			//Ajoute tous les contrôles qui ont un nom unique

            Dictionary<string, bool> controlesANePasAjouter = new Dictionary<string, bool>();
			GetDefinitionsChildsNommés(lstProps, controlesANePasAjouter);
            if ( WndContainer != null )
                lstProps.Add(new CDefinitionProprieteDynamiqueWndContainer(WndContainer, "Container"));

			return lstProps.ToArray();
		}


		public virtual CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
		{
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();

            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementOnInit,
                "OnSetEditedElement", I.T("Occurs when the edited element is set|10001")));
            return lst.ToArray();
        }

		public CHandlerEvenementParFormule[] GetHanlders()
		{
			if (m_listeHandlers == null)
				m_listeHandlers = new List<CHandlerEvenementParFormule>();
			return m_listeHandlers.ToArray();

		}

		public CHandlerEvenementParFormule GetHandler(string strIdEvenement)
		{
			if (m_listeHandlers != null)
			{
				foreach (CHandlerEvenementParFormule handler in m_listeHandlers)
					if (handler.IdEvenement == strIdEvenement)
						return handler;
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handler"></param>
		public void SetHandler(CHandlerEvenementParFormule handler)
		{
			if (handler == null)
				return;
			//Supprime l'ancien handler pour cet évenement
			foreach (CHandlerEvenementParFormule handlerTest in m_listeHandlers.ToArray())
			{
				if (handlerTest.IdEvenement == handler.IdEvenement)
				{
					m_listeHandlers.Remove(handlerTest);
					break;
				}
			}
			if (handler.FormuleEvenement != null)
				m_listeHandlers.Add(handler);
		}


		//-----------------------------------------------------
		public virtual IWndAChildNomme GetChildFromName(string strName)
		{
			return GetChild(strName);
		}

        //-----------------------------------------------------
        [Browsable(false)]
        public virtual IWndAContainer WndContainer
        {
            get
            {
                return Parent as IWndAContainer;
            }
            set
            {
                Parent = value as I2iObjetGraphique;
            }
        }

		//-----------------------------------------------------
		public virtual void SetTypeElementEdite(Type tp)
		{
			m_typeEdite = tp;
			// propage le type sur tous les fils
			foreach (C2iWnd wnd in Childs)
			{
				wnd.SetTypeElementEdite(tp);
			}
		}

        public virtual void ChercheObjet(object objetCherche, CResultatRequeteRechercheObjet resultat)
        {
            if (DoesUse ( objetCherche ))
            {
                resultat.AddResultat(new CNoeudRechercheObjet_Wnd(this));
            }
            resultat.PushChemin(new CNoeudRechercheObjet_Wnd(this));
            foreach (C2iWnd child in Childs)
                child.ChercheObjet(objetCherche, resultat);
            resultat.PopChemin();
        }


        /// <summary>
        /// Retourne true si l'objet utilise l'objet cherché
        /// </summary>
        /// <param name="objetCherche"></param>
        /// <returns></returns>
        public virtual bool DoesUse(object objetCherche)
        {
            if (CTesteurUtilisationObjet.DoesUse(this, objetCherche))
                return true;
            return false;
        }

        /// <summary>
        /// Définit ou redefinit les contrôles externes de cette fenêtre
        /// </summary>
        /// <param name="controles"></param>
        public void SetControlesExternes(IControleFormulaireExterne[] controles)
        {
            IEnumerable<C2iWndControleExterne> lstExternes = from c in this.Childs
                                                             where c is C2iWndControleExterne
                                                             select c as C2iWndControleExterne;
            List<C2iWndControleExterne> lstToDelete = new List<C2iWndControleExterne>();
            foreach (C2iWndControleExterne ex in lstExternes)
                if (controles.Select(c => c.Name == ex.Name).Count() == 0)
                    lstToDelete.Add(ex);

            foreach (IControleFormulaireExterne ctrl in controles)
            {
                C2iWndControleExterne ex = lstExternes.FirstOrDefault(x => x.Name == ctrl.Name);
                if (ex == null)
                {
                    ex = new C2iWndControleExterne();
                    ex.AttacheToControl(ctrl);
                    AddChild(ex);
                    ex.Parent = this;
                }
                else
                    ex.AttacheToControl(ctrl);
                FrontToBack(ex);
            }

            foreach (C2iWnd wnd in lstToDelete)
                RemoveChild(wnd);
        }

        //-----------------------------------------------------------------------------------------
        public CObjetPourSousProprietes GetObjetPourAnalyseFormule(Type typeEdite)
        {
            C2iWnd parent = this;
            while (parent.Parent as C2iWnd != null)
                parent = parent.Parent as C2iWnd;
            return new CObjetPourSousProprietes(
                    new CDefinitionMultiSourceForExpression(GetObjetPourAnalyseThis(typeEdite),
                    new CSourceSupplementaire("CurrentWindow", parent)));
        }

        //-----------------------------------------------------------------------------------------
        public virtual CObjetPourSousProprietes GetObjetPourAnalyseThis(CObjetPourSousProprietes objetRacine)
        {
            CObjetPourSousProprietes objet = objetRacine;
            C2iWnd wndParent = Parent as C2iWnd;
            if (wndParent != null)
                return wndParent.GetObjetAnalysePourFils(objetRacine);
            return objet;
        }

        //-----------------------------------------------------------------------------------------
        public virtual CObjetPourSousProprietes GetObjetAnalysePourFils(CObjetPourSousProprietes objetRacine)
        {
            return GetObjetPourAnalyseThis(objetRacine);
        }

        //-----------------------------------------------------------------------------------------
        public virtual void FillArbreProprietesAccedees(CArbreDefinitionsDynamiques arbre)
        {
            foreach (C2iWnd wnd in Childs)
                wnd.FillArbreProprietesAccedees(arbre);
        }

        #region IObjetAIContexteDonnee Membres

        //-----------------------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                C2iWnd parent = Parent as C2iWnd;
                if (parent != null)
                    return parent.IContexteDonnee;
                return m_contexteDonnee;
            }
            set
            {
                C2iWnd parent = Parent as C2iWnd;
                if (parent != null)
                    parent.IContexteDonnee = value;
                m_contexteDonnee = value;
            }

        }

        #endregion
    }
}
