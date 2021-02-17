using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using sc2i.common;
using System.ComponentModel;

namespace sc2i.drawing
{

	public delegate void EventHandlerChild(I2iObjetGraphique element);
	public delegate void EventHandlerObjetGraphique(I2iObjetGraphique element);


	/// <summary>
	/// Objet de base pour la réalisation de formulaires, ...
	/// objets graphique pouvant être édités dans un CPanelEditionObjetGraphique
	/// </summary>
	[Serializable]
	public abstract class C2iObjetGraphique : MarshalByRefObject, I2iSerializable, I2iObjetGraphique
	{

		public virtual event EventHandlerChild ChildAdded;
		public virtual event EventHandlerChild ChildRemoved;
		public virtual event EventHandlerObjetGraphique SizeChanged;
		public virtual event EventHandlerObjetGraphique LocationChanged;


		

		private I2iObjetGraphique m_parent = null;
		private Size m_size = new Size(64, 16);
		private Point m_position = new Point(0, 0);
		private bool m_bIsLock = false;


        //private bool m_bNoMove = false;
        //private bool m_bNoDelete = false;
		/// ///////////////////////////////////////////////
		public C2iObjetGraphique()
		{

		}


        /// <summary>
        /// Indique que l'élément ne peut pas être supprimé
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual bool NoDelete 
        {

           get
           {
               return false;
            }

           

        }

        /// <summary>
        /// Indique que l'élément ne peut pas être déplacé
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual bool NoMove
        {

            get
            {
                return false;
            }

           
        }


        /// <summary>
        /// Indique que l'élément ne peut pas avoir de poignées
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual bool NoPoignees
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        ///Indique qu'on ne dessine pas de rectangle de sélection standard autour
        ///de cet élément
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual bool NoRectangleSelection
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Dessine l'élément lorsqu'il est sélectionné
        /// </summary>
        public virtual void DrawSelected(Graphics g)
        {
        }

        [Browsable(false)]
        public bool LockChilds
        {
            get
            {
                return false;
            }
            set { }
        }

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public virtual I2iObjetGraphique Parent
		{
			get
			{
				return m_parent;
			}
			set
			{
				m_parent = value;
			}
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		protected virtual Point OrigineCliente
		{
			get
			{
				return Point.Empty;
			}
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public virtual bool IsLock
		{
			get
			{
				return m_bIsLock;
			}
			set
			{
				m_bIsLock = value;
			}
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		protected virtual Size ClientSize
		{
			get
			{
				return Size;
			}
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public Rectangle ClientRect
		{
			get
			{
				Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
				return rect;
			}
		}

		[System.ComponentModel.Browsable(false)]
		/// ///////////////////////////////////////////////
		public Rectangle RectangleAbsolu
		{
			get
			{
				Rectangle rect = new Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
				if (Parent != null)
					return Parent.ClientToGlobal(rect);
				else
					return rect;
			}
		}

		/// ///////////////////////////////////////////////
		public Point ClientToGlobal(Point pt)
		{
			Point newPt = pt;
			newPt.Offset(OrigineCliente.X, OrigineCliente.Y);
			newPt.Offset(Position.X, Position.Y);
			if (Parent != null)
				return Parent.ClientToGlobal(newPt);
			else
				return newPt;
		}

		/// ///////////////////////////////////////////////
		public Point GlobalToClient(Point pt)
		{
			Point newPt = pt;
			newPt.Offset(-OrigineCliente.X, -OrigineCliente.Y);
			newPt.Offset(-Position.X, -Position.Y);
			if (Parent != null)
				return Parent.GlobalToClient(newPt);
			else
				return newPt;
		}

		/// ///////////////////////////////////////////////
		public Point[] GlobalToClient(Point[] pts)
		{
			Point[] retour = new Point[pts.Length];
			int nIndex = 0;
			foreach (Point pt in pts)
				retour[nIndex++] = GlobalToClient(pt);
			return retour;
		}

		/// ///////////////////////////////////////////////
		public Point[] ClientToGlobal(Point[] pts)
		{
			Point[] retour = new Point[pts.Length];
			int nIndex = 0;
			foreach (Point pt in pts)
				retour[nIndex++] = ClientToGlobal(pt);
			return retour;
		}

		/// ///////////////////////////////////////////////
		public Rectangle ClientToGlobal(Rectangle rect)
		{
			Rectangle newRect = rect;
			newRect.Offset(OrigineCliente.X, OrigineCliente.Y);
			newRect.Offset(Position.X, Position.Y);
			if (Parent != null)
				return Parent.ClientToGlobal(newRect);
			else
				return newRect;
		}

		/// ///////////////////////////////////////////////
		public Rectangle GlobalToClient(Rectangle rect)
		{
			Rectangle newRect = rect;
			newRect.Offset(-OrigineCliente.X, -OrigineCliente.Y);
			newRect.Offset(-Position.X, -Position.Y);
			if (Parent != null)
				return Parent.ClientToGlobal(newRect);
			else
				return newRect;
		}

		/// ///////////////////////////////////////////////
		///Magnetise le point sur sa grille ( par défaut, pas de grille )
		public virtual Point Magnetise(Point pt)
		{
			return pt;
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public virtual bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

        /// ///////////////////////////////////////////////
        public virtual bool AutoExpandFromChildren
        {
            get
            {
                return false;
            }
        }

		/// ///////////////////////////////////////////////
		public virtual Size Size
		{
			get
			{
				return m_size;
			}
			set
			{
				if (Size != value)
				{
					m_size = value;
                    RepositionneChilds();
					if (SizeChanged != null)
						SizeChanged(this);
                    if (Parent != null)
                        Parent.RepositionneChilds();
				}
			}
		}

		/// ///////////////////////////////////////////////
		public virtual Point Position
		{
			get
			{
				return m_position;
			}
			set
			{
				m_position = value;
				if (m_position.X < 0)
					m_position.X = 0;
				if (m_position.Y < 0)
					m_position.Y = 0;
				if (Parent != null)
				{
                    if (m_position.X + Size.Width > Parent.ClientRect.Width)
                    {
                        if (!Parent.AutoExpandFromChildren)
                            m_position.X = Math.Max(Parent.ClientRect.Width - Size.Width, 0);
                        else
                        {
                            int nDif = m_position.X + Size.Width - Parent.ClientRect.Width;
                            Size sz = new Size(Parent.Size.Width + nDif, Parent.Size.Height);
                            Parent.Size = sz;
                        }
                    }
                    if (m_position.Y + Size.Height > Parent.ClientRect.Height)
                    {
                        if (!Parent.AutoExpandFromChildren)
                            m_position.Y = Math.Max(Parent.ClientRect.Height - Size.Height, 0);
                        else
                        {
                            int nDif = m_position.Y + Size.Height - Parent.ClientRect.Height;
                            Size sz = new Size(Parent.Size.Width, Parent.Size.Height + nDif);
                            Parent.Size = sz;

                        }
                    }
                    Parent.RepositionneChilds();
				}

			}
		}


		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public Point PositionAbsolue
		{
			get
			{
				Point pt = Position;
				if (Parent != null)
					pt = Parent.ClientToGlobal(pt);
				return pt;
			}
			set
			{
				Point pt = value;
				if (Parent != null)
					pt = Parent.GlobalToClient(pt);
				if (pt != Position)
				{
					Position = pt;
					if (LocationChanged != null)
						LocationChanged(this);
				}
			}
		}

		/// ///////////////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public abstract I2iObjetGraphique[] Childs { get; }

        /// //////////////////////////////////////////////
        public List<I2iObjetGraphique> GetAllChilds()
        {
            List<I2iObjetGraphique> lstChilds = new List<I2iObjetGraphique>();
            FillAllChilds(lstChilds);
            return lstChilds;
        }

        /// //////////////////////////////////////////////
        protected void FillAllChilds(List<I2iObjetGraphique> lstChilds)
        {
            lstChilds.Add(this);
            foreach (I2iObjetGraphique objet in Childs)
            {
                C2iObjetGraphique fils = objet as C2iObjetGraphique;
                if (fils != null)
                    fils.FillAllChilds(lstChilds);
            }
        }

		/// ///////////////////////////////////////////////
		public abstract bool AddChild(I2iObjetGraphique child);

		/// ///////////////////////////////////////////////
		public abstract bool ContainsChild(I2iObjetGraphique child);

		/// ///////////////////////////////////////////////
		public abstract void RemoveChild(I2iObjetGraphique child);

        /// ///////////////////////////////////////////////
        public virtual void DeleteChild(I2iObjetGraphique child)
        {
            RemoveChild(child);
        }

        /// <summary>
        /// Nombre d'appels successifs à SuspendLayout
        /// Quand ce nombre est à 0, le recalcule de la position
        /// des fils est faite
        /// </summary>
        private int m_nNbSuspendLayout = 0;
        /// ///////////////////////////////////////////////
        public void SuspendLayout()
        {
            m_nNbSuspendLayout++;
        }

        /// ///////////////////////////////////////////////
        public void ResumeLayout()
        {
            m_nNbSuspendLayout--;
        }

        /// ///////////////////////////////////////////////
        ///Recalcule la position des fils si le conteneur positionne ces fils de manière
        ///automatique
        public void RepositionneChilds()
        {
            if (m_nNbSuspendLayout == 0)
                MyRepositionneChilds();
        }

        /// ///////////////////////////////////////////////
        /// Recalcule la position des fils, sans tenir compte du suspendLayout
        protected virtual void MyRepositionneChilds()
        {
        }



        /// ///////////////////////////////////////////////
		public abstract void BringToFront(I2iObjetGraphique child);

		/// ///////////////////////////////////////////////
		public abstract void FrontToBack(I2iObjetGraphique child);

		/// ///////////////////////////////////////////////
		public bool IsChildOf(I2iObjetGraphique wnd)
		{
			if (Parent == wnd)
				return true;
			if (Parent != null)
				return Parent.IsChildOf(wnd);
			return false;
		}


		/// ///////////////////////////////////////////////
		public virtual bool IsPointIn(Point pt)
		{
			Rectangle rect = new Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
			if (Parent != null)
				rect = Parent.ClientToGlobal(rect);
			return rect.Contains(pt.X, pt.Y);
		}


		//Retourne les elements correspondant au point dans l'ordre Z (du plus pret au plus profond)
		public virtual List<I2iObjetGraphique> SelectionnerElements(Point pt)
		{
			List<I2iObjetGraphique> elements = new List<I2iObjetGraphique>();
			SelectionnerElements(pt, elements);
			elements.Reverse();
            elements.Add(this);
			return elements;
		}
		private List<I2iObjetGraphique> SelectionnerElements(Point pt, List<I2iObjetGraphique> elements)
		{
			foreach (I2iObjetGraphique ele in Childs)
				if (ele.IsPointIn(pt))
				{
					elements.Add(ele);
					((C2iObjetGraphique)ele).SelectionnerElements(pt, elements);
					//elements.AddRange(ele.SelectionnerElements(pt, elements));
				}
			return elements;
		}



		public I2iObjetGraphique SelectionnerElementAvantElement(Point pt, I2iObjetGraphique element)
		{
			List<I2iObjetGraphique> selection = SelectionnerElements(pt);
			selection.Reverse();
			int nIdxElement = selection.IndexOf(element);
			if (nIdxElement > 0)
				return selection[nIdxElement - 1];
			return null;
		}
		public I2iObjetGraphique SelectionnerElementApresElement(Point pt, I2iObjetGraphique element)
		{
			List<I2iObjetGraphique> selection = SelectionnerElements(pt);
			int nIdxElement = selection.IndexOf(element);
			if (nIdxElement < selection.Count - 1)
				return selection[nIdxElement + 1];
			return null;
		}

		public I2iObjetGraphique SelectionnerElementDuDessus(Point pt)
		{
			return SelectionnerElementDuDessus(pt, null);
		}
		public I2iObjetGraphique SelectionnerElementDuDessus(Point pt, I2iObjetGraphique elementToIgnore)
		{
			List<I2iObjetGraphique> selection = SelectionnerElements(pt);
			foreach (I2iObjetGraphique ele in selection)
				if (elementToIgnore == null || ele != elementToIgnore)
					return ele;
			return null;
		}

		//PREMIER CONTENEUR
		public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt)
		{
			I2iObjetGraphique ele = null;
			return SelectionnerElementConteneurDuDessus(pt, ele);
		}

		public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt, List<I2iObjetGraphique> elementsToIgnore)
		{
			List<I2iObjetGraphique> selection = SelectionnerElements(pt);
			foreach (I2iObjetGraphique ele in selection)
			{
				if (ele.AcceptChilds)
				{
					bool bOk = true;
					if (elementsToIgnore != null)
					{
						if (elementsToIgnore.Contains(ele))
							bOk = false;
						else
							foreach (I2iObjetGraphique eleToIgnore in elementsToIgnore)
								if (IsAChildOf(eleToIgnore, ele))
								{
									bOk = false;
									break;
								}
					}
					if (bOk)
						return ele;
				}
			}
			return null;
		}
		//Ignore l'élément et ces fils
		public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt, I2iObjetGraphique elementToIgnore)
		{
			List<I2iObjetGraphique> selection = SelectionnerElements(pt);
			foreach (I2iObjetGraphique ele in selection)
				if (ele.AcceptChilds && (elementToIgnore == null || (!IsAChildOf(elementToIgnore, ele) && elementToIgnore != ele)))
					return ele;
			return null;
		}
		public bool IsAChildOf(I2iObjetGraphique parent, I2iObjetGraphique supposedChild)
		{
			foreach (I2iObjetGraphique enfant in parent.Childs)
				if (supposedChild == enfant || IsAChildOf(enfant, supposedChild))
					return true;
			return false;
		}




		/// //////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}

		/// //////////////////////////////////////////////
		public virtual CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (result)
				result = PrivateSerialize(serializer, nVersion);
			if (result)
				result = MySerialize(serializer);
			if (!result)
				return result;
			return result;
		}

		/// //////////////////////////////////////////////
		protected abstract CResultAErreur MySerialize(C2iSerializer serializer);

		/// //////////////////////////////////////////////
		private CResultAErreur PrivateSerialize(C2iSerializer serializer, int nVersion)
		{
			int nWidth = Size.Width;
			serializer.TraiteInt(ref nWidth);

			int nHeight = Size.Height;
			serializer.TraiteInt(ref nHeight);
			Size = new Size(nWidth, nHeight);

			int nX = Position.X;
			serializer.TraiteInt(ref nX);

			int nY = Position.Y;
			serializer.TraiteInt(ref nY);
			Position = new Point(nX, nY);

			serializer.TraiteBool(ref m_bIsLock);

			return CResultAErreur.True;
		}

        

		private Rectangle m_lastZoneDessinee;
		private void Dessiner(Rectangle nouvelleZone)
		{
			if (m_lastZoneDessinee == nouvelleZone)
			{
				//on dessine le cache
				//return m_bmpCache;
			}
			else
			{
				//SCROLLING VERTICAL
				if (m_lastZoneDessinee.Location.X == nouvelleZone.Location.X
					&& m_lastZoneDessinee.Width == nouvelleZone.Width)
				{
					//LE NOUVEAU RECTANGLE EST :
					//DEDANT
					if (nouvelleZone.Bottom < m_lastZoneDessinee.Bottom
						&& nouvelleZone.Top > m_lastZoneDessinee.Top)
					{
						//OFFSET + RECUP IMAGE
					}
					//DEHORS
					else if (nouvelleZone.Bottom < m_lastZoneDessinee.Y ||
						nouvelleZone.Top > m_lastZoneDessinee.Bottom)
					{
						//IL FAUT TOUT REDESSINER
					}
					//A CHEVAL
					else
					{
						//Rectangle Commun
						int nHeight = m_lastZoneDessinee.Bottom - nouvelleZone.Y;
						int nWidth = nouvelleZone.Width;
						int nY = nouvelleZone.Y;
						int nX = nouvelleZone.X;
						//IL FAUT LE DESSINER

						//Nouveau Rectangle
						int n2Y = m_lastZoneDessinee.Bottom;
						int n2X = nouvelleZone.X;
						int n2Width = nouvelleZone.Width;
						int n2Height = nouvelleZone.Height - nHeight;

					}
					//rctARedessiner = new Rectangle(nouvelleZone.Location.X, nouvelleZone.Location.Y
				}
				//SCROLLING HORIZONTAL
				else if (m_lastZoneDessinee.Location.Y == nouvelleZone.Location.Y
					&& m_lastZoneDessinee.Height == nouvelleZone.Height)
				{

				}
				else
				{
					//IL FAUT REDESSINER

				}
			}
		}

        /// //////////////////////////////////////////////
        protected virtual I2iObjetGraphique[] GetChildsTriesPourDessin()
        {
            return Childs;
        }

		/// //////////////////////////////////////////////
		public virtual void Draw(CContextDessinObjetGraphique ctx)
		{
			if ( !ctx.ShouldDrawElement ( RectangleAbsolu ) )
				return;
            if (ctx.FonctionDessinSupplementaireAvantObjet != null)
                if (!ctx.FonctionDessinSupplementaireAvantObjet(ctx, this))
                    return;
			MyDraw(ctx);
            if (ctx.FonctionDessinSupplementaireApresObjet != null)
                if (!ctx.FonctionDessinSupplementaireApresObjet(ctx, this))
                    return;

			foreach (I2iObjetGraphique fils in GetChildsTriesPourDessin())
				fils.Draw(ctx);
		}

		/// //////////////////////////////////////////////
		protected abstract void MyDraw(CContextDessinObjetGraphique ctx);


		/// //////////////////////////////////////////////
		public virtual ArrayList AllChilds()
		{
			ArrayList lst = new ArrayList();
			GetAllChilds(lst);
			return lst;
		}

		/// //////////////////////////////////////////////
		protected virtual void GetAllChilds(ArrayList lst)
		{
			foreach (I2iObjetGraphique wnd in Childs)
			{
				lst.Add(wnd);
				((C2iObjetGraphique)wnd).GetAllChilds(lst);
			}
		}

		/// ///////////////////////////////////////////////
		public virtual void OnDesignDoubleClick(Point ptAbsolu)
		{
		}

		#region ICloneable Membres

		public virtual object Clone()
		{
			return CCloner2iSerializable.Clone(this);
			
		}

		#endregion

        //---------------------------------------------------
        public virtual Bitmap GetBitmapCopie(int nTailleImage, bool bChildsOnly)
        {
            //Calcule la taille de l'image
            Size sizeThis = Size;
            int nMaxX = 0, nMaxY = 0, nMinX = Size.Width, nMinY = Size.Height;
            if (bChildsOnly)
            {
                
                foreach (I2iObjetGraphique objet in Childs)
                {
                    nMaxX = Math.Max(objet.Position.X + objet.Size.Width, nMaxX);
                    nMaxY = Math.Max(objet.Position.Y + objet.Size.Height, nMaxY);
                    nMinX = Math.Min(objet.Position.X, nMinX);
                    nMinY = Math.Min(objet.Position.Y, nMinY);
                }
                sizeThis = new Size(nMaxX - nMinX, nMaxY - nMinY);
            }
            if (sizeThis.Width == 0 || sizeThis.Height == 0)
            {
                return new Bitmap(10, 10);
            }
            int nWidthImage, nHeightImage;
            if (sizeThis.Width > sizeThis.Height)
            {
                nWidthImage = nTailleImage;
                nHeightImage = (int)((double)sizeThis.Height * (double)nWidthImage / (double)sizeThis.Width);
            }
            else
            {
                nHeightImage = nTailleImage;
                nWidthImage = (int)((double)sizeThis.Width * (double)nHeightImage / (double)sizeThis.Height);
            }
            float fEchelle = (float)nWidthImage / sizeThis.Width;
            Bitmap bmp = new Bitmap(nWidthImage, nHeightImage);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.ScaleTransform(fEchelle, fEchelle);
            if ( bChildsOnly )
                g.TranslateTransform ( -nMinX, -nMinY );
            CContextDessinObjetGraphique ctx = new CContextDessinObjetGraphique(g);
            Draw(ctx);
            g.Dispose();
            return bmp;
        }

        
        
        
        
        /// <summary>
        /// Appelé par les fonctions de drag and drop, pour cloner l'élément en vue de mettre la copie dans le parent demandé.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="dicObjetsPourCloner"></param>
        /// <returns></returns>
        public virtual I2iObjetGraphique GetCloneAMettreDansParent(I2iObjetGraphique parent, Dictionary<Type, object> dicObjetsPourCloner)
        {
            return (I2iObjetGraphique)CCloner2iSerializable.Clone(this, dicObjetsPourCloner);
        }

        /// <summary>
        /// Indique que l'objet cloné doit être annulé. Utiliser cette méthode pour liberer des ressources
        /// </summary>
        public virtual void CancelClone()
        {
        }

        public virtual string TooltipText
        {
            get
            {
                return "";
            }
        }

        public virtual bool OnDesignerMouseDown(Point ptLocal)
        {
            return false;
        }
	}
}
