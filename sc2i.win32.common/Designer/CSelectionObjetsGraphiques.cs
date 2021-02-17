using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using sc2i.drawing;

namespace sc2i.win32.common
{
    /// <summary>
    /// Selection d'objets graphiques I2iObjetGraphique pour l'édition
    /// dans un CPanelEditionObjetGraphique
    /// Se dessin dans le graphics de base non modifié, mais
    /// travaille avec les objets dans le repère logique
    /// </summary>
    public class CSelectionElementsGraphiques : List<I2iObjetGraphique>
    {
        /// <summary>
        /// dernière zone dans laquelle la selection s'est dessinée (pour effacement lors d'un mouvement)
        /// </summary>
        private Rectangle m_lastZoneDisplayDessinee = new Rectangle(0, 0, 0, 0);

        public bool EnAction
        {
            get
            {
                return EnDeplacement || EnRedimentionnement || EnSelection;
            }
        }

        /// /////////////////////////////////////////////////
        public CSelectionElementsGraphiques(CPanelEditionObjetGraphique ctrlParent)
        {
            m_editeur = ctrlParent;
            
        }

        public event EventHandler ElementsMovedOrResized;


        /// /////////////////////////////////////////////////
        private CPanelEditionObjetGraphique m_editeur = null;
        public CPanelEditionObjetGraphique Editeur
        {
            get
            {
                return m_editeur;
            }
        }

        // /////////////////////////////////////////////////
        public I2iObjetGraphique ControlReference
        {
            get
            {
                if (Count > 0)
                    return this[Count - 1];
                return null;
            }
            set
            {
                if (value != ControlReference)
                {
                    if (this.Contains(value))
                    {
                        base.Remove(value);
                        base.Add(value);
                        InitPoignees(value);
                        if (SelectionChanged != null)
                            SelectionChanged(this, new EventArgs());
                    }
                    else
                        Add(value);
                }
            }
        }



        /// /////////////////////////////////////////////////
        public event EventHandler SelectionChanged;

        #region Déplacements
        public bool EnDeplacement
        {
            get
            {
                return m_bIsStartDragging;
            }
        }

        private bool m_bIsStartDragging = false;
        
        //dans le repère logique
        private Point m_ptStartDrag;
        #endregion

        #region Poignees
        public bool EnRedimentionnement
        {
            get
            {
                return m_poigneesEnCour.Count > 0;
            }
        }
        //REDIMENTIONNEMENT
        private List<CPoignee> GetPoignees(I2iObjetGraphique obj)
        {
            List<CPoignee> poignees = new List<CPoignee>();
            if (!obj.IsLock)
                foreach (CPoignee p in ToutesLesPoignees)
                    if (p.ObjetLie == obj)
                        poignees.Add(p);
            return poignees;
        }
        private List<CPoignee> m_poignees = new List<CPoignee>();
        private void InitPoignees(I2iObjetGraphique obj)
        {
           
           if(! obj.NoPoignees)
           {
               for (int n = 0; n < 8; n++)
               {
                   CPoignee p = new CPoignee(this, obj, Editeur);
                   switch (n)
                   {
                       case 0:
                           p.Alignement = ContentAlignment.TopLeft;
                           break;
                       case 1:
                           p.Alignement = ContentAlignment.TopCenter;
                           break;
                       case 2:
                           p.Alignement = ContentAlignment.TopRight;
                           break;
                       case 3:
                           p.Alignement = ContentAlignment.MiddleLeft;
                           break;
                       case 4:
                           p.Alignement = ContentAlignment.MiddleRight;
                           break;
                       case 5:
                           p.Alignement = ContentAlignment.BottomLeft;
                           break;
                       case 6:
                           p.Alignement = ContentAlignment.BottomCenter;
                           break;
                       case 7:
                           p.Alignement = ContentAlignment.BottomRight;
                           break;
                   }

                           p.RecalcPosition(CUtilRect.Normalise(obj.RectangleAbsolu));
                           m_poignees.Add(p);
                   
               }
            }
            
        }
        private bool DeletePoignees(I2iObjetGraphique obj)
        {
            List<CPoignee> poignees = GetPoignees(obj);
            foreach (CPoignee p in poignees)
                m_poignees.Remove(p);
            return true;
        }

        private List<CPoignee> ToutesLesPoignees
        {
            get
            {
                return m_poignees;
            }
        }



        // /////////////////////////////////////////////////
        public void RecalcPositionPoignees()
        {
            //A AUTOMATISER COTE POIGNEE ??
            foreach (CPoignee p in ToutesLesPoignees)
                p.RecalcPosition(p.ObjetLie.RectangleAbsolu);
        }


        private List<CPoignee> m_poigneesEnCour = new List<CPoignee>();

        private EFormePoignee m_formePoignees = EFormePoignee.Carre;
        public EFormePoignee FormesDesPoignees
        {
            get
            {
                return m_formePoignees;
            }
            set
            {
                if (value != m_formePoignees)
                {
                    foreach (CPoignee p in ToutesLesPoignees)
                        p.FormePoignee = value;
                    m_formePoignees = value;
                }
            }
        }
        #endregion

        #region Ajout / Suppression
        /// /////////////////////////////////////////////////
        new public void Insert(int index, I2iObjetGraphique item)
        {
            InitPoignees(item);
            base.Insert(index, item);
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
        /// /////////////////////////////////////////////////
        new public void Clear()
        {
            //Stef 06102008 : Vide la sélection même si elle est déjà vide,
            //Pour que SelectionChanged soit quand même appellé.
            base.Clear();
            m_poignees.Clear();
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
        // /////////////////////////////////////////////////
        new public void Add(I2iObjetGraphique item)
        {
            if (item != null)
            {
                InitPoignees(item);
                base.Add(item);
                if (SelectionChanged != null)
                    SelectionChanged(this, new EventArgs());
            }
        }

        new public void AddRange(IEnumerable<I2iObjetGraphique> collection)
        {
            foreach (I2iObjetGraphique obj in collection)
                InitPoignees(obj);

            base.AddRange(collection);
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
        // /////////////////////////////////////////////////
        new public void RemoveAt(int index)
        {
            DeletePoignees(this[index]);
            base.RemoveAt(index);
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
        new public bool Remove(I2iObjetGraphique item)
        {
            bool bResult = DeletePoignees(item);
            if (bResult)
                bResult = base.Remove(item);
            else
                bResult = false;

            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
            return bResult;
        }

        #endregion


        // /////////////////////////////////////////////////
        /// <summary>
        /// comportement du select :
        /// Sur mouseDown, si un des éléments sélectionnés est
        /// sous le point->Pas de changement de sélection
        /// </summary>
        /// <returns>true si la sélection a été modifiée par l'appel à cette fonction</returns>
        /// <param name="ptLogique"></param>
        /// <param name="bToucheControl"></param>
        /// <param name="bOnMouseDown"></param>
        private Point m_lastPointSelect = new Point(-100, -100);
        public bool DoSelect(Point ptLogique, bool bToucheControl, bool bOnMouseDown)
        {
            List<I2iObjetGraphique> lstSelect = Editeur.ObjetEdite.SelectionnerElements(ptLogique);
            bool bTousDedans = true;
            bool bUnDedans = false;
            
            foreach (I2iObjetGraphique objet in lstSelect)
            {
                if (!Contains(objet))
                {
                    bTousDedans = false;
                }
                else if ( !objet.IsLock )
                    bUnDedans = true;
            }

            if (bOnMouseDown)
            {
                
                //Sur mouse down, sans touche control, si l'un des éléments
                //est dedans et qu'on est proche du dernier point, la sélection ne bouge pas
                /*if (bUnDedans && !bToucheControl )//&& new CSegmentDroite(m_lastPointSelect, ptLogique).Longueur < 5)
                    return false;*/
                m_lastPointSelect = ptLogique;
                
                //Sinon, sélectionne le premier
                I2iObjetGraphique sel = Editeur.ObjetEdite.SelectionnerElementDuDessus(ptLogique);
                if (!bToucheControl)
                {
                    if (Contains(sel))
                    {
                        Point pt = sel.GlobalToClient(ptLogique);
                        if (sel.OnDesignerMouseDown(pt))
                        {
                            Editeur.Refresh();
                        }
                        return false;
                    }
                    Clear();
                }
                else
                {
                    if (Contains(sel))
                    {
                        if (sel.Equals(ControlReference))
                        {
                            I2iObjetGraphique prof = Editeur.ObjetEdite.SelectionnerElementDuDessus(ptLogique, sel);
                            if (prof != null)
                            {
                                Add(prof);
                            }
                            Remove(sel);
                            return true;
                        }
                        else
                        {
                            ControlReference = sel;
                            return true;
                        }
                    }
                }
                if ( sel != null )
                    Add(sel);

            }

            if (!bOnMouseDown && !bToucheControl)
            {
                I2iObjetGraphique lastSel = null;
                if ( Count == 1 )
                    lastSel = this[0];
                //Sélectionne le premier
                Clear();
                I2iObjetGraphique sel = Editeur.ObjetEdite.SelectionnerElementApresElement(ptLogique, lastSel);
                if (sel != null)
                    Add(sel);
            }
            return true;
                
        }

        // /////////////////////////////////////////////////
        DateTime m_dtLastMouseDown = DateTime.Now;

        //est vrai si un mousedown a eu lieu dans cette fenêtre
        private bool m_bMouseDownDansCetteFenetre = false;
        private bool m_bSelectOnMouseUp = false;
        private bool m_bBlocageMove = false;
        public virtual void MouseDown(Point ptLogique)
        {
            m_dtLastMouseDown = DateTime.Now;
            m_ptStartDrag = ptLogique;
            //REDIMENTIONNEMENT
            if (m_bmp != null)
                m_bmp.Dispose();
            m_bmp = null;
            m_ptStartDrag = ptLogique;
            m_waitSelection = null;
            m_bMouseDownDansCetteFenetre = true;
            m_conteneurPourSelectionForcee = null;
            m_poigneesEnCour.Clear();
            if (!Editeur.LockEdition)
            {
                List<CPoignee> poignees = ToutesLesPoignees;
                foreach (CPoignee p in poignees)
                    if (!p.ObjetLie.IsLock && p.IsPointIn(ptLogique))
                    {
                        m_ptStartDrag = ptLogique;
                        m_poigneesEnCour.Add(p);
                        ControlReference = p.ObjetLie;
                        p.StartDrag(ptLogique);
                        foreach (CPoignee pAnnexe in poignees)
                            if (pAnnexe != p && p.Alignement == pAnnexe.Alignement)
                            {
                                int nDecalX = pAnnexe.Position.X - p.Position.X;
                                int nDecalY = pAnnexe.Position.Y - p.Position.Y;
                                Point ptStartAnnexe = new Point(ptLogique.X + nDecalX, ptLogique.Y + nDecalY);
                                pAnnexe.StartDrag(ptStartAnnexe);
                                m_poigneesEnCour.Add(pAnnexe);
                            }
                        break;
                    }
            }
            if (!EnRedimentionnement)
            {
                m_bSelectOnMouseUp = !DoSelect(ptLogique, Control.ModifierKeys == Keys.Control, true);
                //RECTANGLE SELECTION
                if (Count == 0 || (Count == 1 && this[0].Equals(Editeur.ObjetEdite))
                    || Control.ModifierKeys == Keys.Alt)
                {
                    m_bInSelection = true;
                    m_ptLogiqueStartSelection = ptLogique;
                    m_conteneurPourSelectionForcee = Editeur.ObjetEdite.SelectionnerElementConteneurDuDessus(ptLogique);
                    m_lastZoneDisplayDessinee = new Rectangle(0, 0, 0, 0);
                }
            }
        }

        // /////////////////////////////////////////////////
        private Bitmap m_bmp;
        private Bitmap CacheScreenShot
        {
            get
            {
                if (m_bmp == null)
                {
                    if (EnRedimentionnement)
                        Editeur.Dessiner(true, false);
                    m_bmp = Editeur.DernierApercuToDispose;
                }
                return m_bmp;
            }
        }
        public virtual void MouseMove(Point ptLogique)
        {
            try
            {
                if (EnRedimentionnement)
                {
                    DateTime dtStart = DateTime.Now;
                    Graphics g = Editeur.CreateGraphics();
                    Bitmap bmp = CacheScreenShot;
                    CPoignee poigneeRef = m_poigneesEnCour[0];
                    foreach (CPoignee p in m_poigneesEnCour)
                    {
                        //Redessine le fond
                        Rectangle rctPoignee = p.DerniereZoneDisplayPourResize;
                        g.DrawImage(bmp, rctPoignee, rctPoignee, GraphicsUnit.Pixel);

                        //Déplace la poignée
                        int nDecalX = p.PointDebutDrag.X - poigneeRef.PointDebutDrag.X;
                        int nDecalY = p.PointDebutDrag.Y - poigneeRef.PointDebutDrag.Y;
                        Point ptMove = new Point(ptLogique.X + nDecalX, ptLogique.Y + nDecalY);
                        p.MouseMove(ptMove);

                        //Dessine la poignée (et la zone de redimensionnement)
                        p.Draw(g);
                    }
                    g.Dispose();
                }
                else if (EnSelection)
                {
                    //Redessine la sélection dans une image
                    //Redessiner fond
                    
                    Bitmap bmp = CacheScreenShot;
                    Graphics g = Editeur.CreateGraphics();
                    g.DrawImage ( bmp, m_lastZoneDisplayDessinee, m_lastZoneDisplayDessinee, GraphicsUnit.Pixel );
                    
                    Rectangle rct = GetRectangleSelection(ptLogique);
                    rct = CUtilRect.Normalise(new Rectangle(Editeur.GetDisplayPointFromLogical(rct.Location),
                        Editeur.GetDisplaySizeFromLogical(rct.Size)));
                    m_lastZoneDisplayDessinee = new Rectangle ( rct.Location, new Size ( rct.Width+1, rct.Height+1));
                    g.DrawRectangle(Pens.Blue, rct);

                    Brush bb = new SolidBrush(Color.FromArgb(50, Color.YellowGreen));
                    g.FillRectangle(bb, rct);
                    g.Dispose();
                    bb.Dispose();
                }
                //DEPLACEMENT
                else if (Control.MouseButtons == MouseButtons.Left &&
                     m_bMouseDownDansCetteFenetre && !m_bBlocageMove &&
                        (Math.Abs(m_ptStartDrag.X - ptLogique.X) > 3 ||
                        Math.Abs(m_ptStartDrag.Y - ptLogique.Y) > 3))
                {
                    m_bIsStartDragging = true;

                    if (m_waitSelection != null && m_waitSelection.Count > 0 && this.Contains(m_waitSelection[0]))
                        ControlReference = m_waitSelection[0];

                    m_waitSelection = null;

                    try
                    {
                        List<CDonneeDragDropObjetGraphique> datasDrag = new List<CDonneeDragDropObjetGraphique>();
                        bool bDrag = false;
                        foreach (I2iObjetGraphique obj in this)
                        {
                            if (obj.IsLock)
                                continue;
                            Point ptOffset = new Point(m_ptStartDrag.X - obj.RectangleAbsolu.Left, m_ptStartDrag.Y - obj.RectangleAbsolu.Top);
                            datasDrag.Add(new CDonneeDragDropObjetGraphique(Editeur.OrigineDragDropId, obj, ptOffset));
                            bDrag = true;
                        }
                        DataObject dataObj = new DataObject(datasDrag);
                        Editeur.CompleteDragDropData(dataObj, ToArray());
                        if (bDrag)
                            Editeur.DoDragDrop(dataObj, System.Windows.Forms.DragDropEffects.Move | System.Windows.Forms.DragDropEffects.Copy | DragDropEffects.Link);
                        m_bMouseDownDansCetteFenetre = false;
                    }
                    catch
                    {
                    }
                    m_bIsStartDragging = false;
                }
                else if (Control.MouseButtons != MouseButtons.Left)
                {
                    if (!Editeur.LockEdition)
                    {
                        bool bOnElementSelectione = false;
                        foreach (I2iObjetGraphique ele in this)
                            if (!ele.IsLock && CUtilRect.Normalise(ele.RectangleAbsolu).Contains(ptLogique))
                            {
                                bOnElementSelectione = true;
                                break;
                            }
                        if (bOnElementSelectione)
                        {
                            Cursor.Current = Cursors.SizeAll;
                        }
                        foreach (CPoignee poignee in ToutesLesPoignees)
                        {
                            if (!poignee.ObjetLie.IsLock && poignee.IsPointIn(ptLogique))
                            {
                                Cursor.Current = poignee.Cursor;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        // /////////////////////////////////////////////////
        public virtual void MouseUp(Point ptLogique)
        {
            try
            {
                m_bBlocageMove = false;
                m_bMouseDownDansCetteFenetre = false;
                Editeur.EnDeplacement = false;
                if (EnRedimentionnement)
                {
                    foreach (CPoignee p in m_poigneesEnCour)
                    {
                        p.MouseUp();
                    }

                    Editeur.Dessiner(true, true);
                    if (ElementsMovedOrResized != null)
                        ElementsMovedOrResized(this, new EventArgs());
                }
                else if (EnSelection)
                {
                    //ON SELECTIONNE LES ELEMENTS DANS LE RECTANGLE!
                    if (!Editeur.ModeRedimentionnement)
                        Clear();
                    Rectangle rct = GetRectangleSelection(ptLogique);
                    List<I2iObjetGraphique> lst = SelectInRectangle(rct);

                    //Selection du nouveau référent
                    if (lst.Count == 1 && Contains(lst[0]) && Count != 1)
                    {
                        ControlReference = lst[0];
                    }
                    else
                    {
                        foreach (I2iObjetGraphique objSelec in this)
                            if (lst.Contains(objSelec))
                                lst.Remove(objSelec);

                        if (lst.Count > 0 && lst[0] != null)
                        {
                            AddRange(lst);
                        }
                        else if (SelectionChanged != null)
                            SelectionChanged(this, new EventArgs());
                    }

                    m_bInSelection = false;
                }
                else
                {
                    TimeSpan sp = DateTime.Now - m_dtLastMouseDown;
                    if ( m_bSelectOnMouseUp && sp.TotalMilliseconds > 400)  //si clic a plus de 0.4s, sélection en profondeur, 
                                                                            //sinon, il s'agit peut être d'un double clic
                        DoSelect(ptLogique, Control.ModifierKeys == Keys.Control, false);
                    m_bSelectOnMouseUp = false;
                }
                m_waitSelection = (List<I2iObjetGraphique>)this;
                m_poigneesEnCour.Clear();
                m_bIsStartDragging = false;
                m_bInSelection = false;
            }
            catch
            {
            }
        }

        #region Selection
        private List<I2iObjetGraphique> m_derniereSelection;
        private List<I2iObjetGraphique> m_waitSelection = null;
        private I2iObjetGraphique m_conteneurPourSelectionForcee;

        public bool EnSelection
        {
            get
            {
                return m_bInSelection;
            }
        }
        private List<I2iObjetGraphique> SelectInRectangle(Rectangle rect)
        {
            I2iObjetGraphique objParent = m_conteneurPourSelectionForcee == null ? Editeur.ObjetEdite : m_conteneurPourSelectionForcee;
            return SelectInRectangle(rect, objParent);
        }
        private List<I2iObjetGraphique> SelectInRectangle(Rectangle rect, I2iObjetGraphique objetParent)
        {
            List<I2iObjetGraphique> elements = new List<I2iObjetGraphique>();
            if (objetParent.LockChilds)
                return elements;
            if (rect.Width == 0 && rect.Height == 0)
            {
                I2iObjetGraphique ele = Editeur.ObjetEdite.SelectionnerElementDuDessus(rect.Location);
                if (ele == null)
                    ele = Editeur.ObjetEdite;

                elements.Add(ele);
            }
            else
               foreach (I2iObjetGraphique obj in objetParent.Childs)
                    if (rect.IntersectsWith(obj.RectangleAbsolu))
                    {
                        if (obj.Childs != null && obj.Childs.Length > 0)
                        {
                            List<I2iObjetGraphique> filsSelec = SelectInRectangle(rect, obj);
                            if (!obj.RectangleAbsolu.Contains(rect) || filsSelec.Count == 0)
                                elements.Add(obj);
                            elements.AddRange(filsSelec);
                        }
                        else
                            elements.Add(obj);
                    }

            return elements;
        }

        public bool PointIsInSelection(Point pt)
        {
            foreach (I2iObjetGraphique obj in this)
                if (CUtilRect.Normalise(obj.RectangleAbsolu).Contains(pt))
                    return true;
            return false;
        }

        private Point m_ptLogiqueStartSelection;
        private Rectangle GetRectangleSelection(Point ptLogique)
        {
            int nPosX = ptLogique.X > m_ptLogiqueStartSelection.X ? m_ptLogiqueStartSelection.X : ptLogique.X;
            int nPosY = ptLogique.Y > m_ptLogiqueStartSelection.Y ? m_ptLogiqueStartSelection.Y : ptLogique.Y;
            int nWidth = Math.Abs(ptLogique.X - m_ptLogiqueStartSelection.X);
            int nHeight = Math.Abs(ptLogique.Y - m_ptLogiqueStartSelection.Y);
            return new Rectangle(nPosX, nPosY, nWidth, nHeight);
        }
        private bool m_bInSelection = false;
        #endregion

        #region Dessin

        // /////////////////////////////////////////////////
        public virtual void Draw(Graphics g, Rectangle zone, bool bAvecPoignees)
        {
            if (Count == 0)
                return;
            RecalcPositionPoignees();
            Pen penNotLock = new Pen(Color.Red, 2);
            penNotLock.DashStyle = DashStyle.Dot;
            Pen penLock = new Pen(Color.Brown, 2);

            Pen pBordureWhite = new Pen(Color.White);
            Pen pBordureBlack = new Pen(Color.Black);
            foreach (I2iObjetGraphique obj in this)
            {
                if (!obj.NoRectangleSelection)
                {
                    Rectangle rect = CUtilRect.Normalise(new Rectangle(
                        Editeur.GetDisplayPointFromLogical ( obj.PositionAbsolue ),
                        Editeur.GetDisplaySizeFromLogical ( obj.Size ) ));
                    if (obj.IsLock)
                    {
                        penLock.DashStyle = obj == ControlReference ? DashStyle.Solid : DashStyle.Dot;
                        g.DrawRectangle(penLock, rect);
                        Bitmap bmpLock = Properties.Resources._lock;
                        if (obj.RectangleAbsolu.Width > bmpLock.Width && obj.RectangleAbsolu.Height > bmpLock.Height)
                        {
                            Rectangle rctIco = new Rectangle(
                                Editeur.GetDisplayPointFromLogical(new Point(obj.RectangleAbsolu.Right, obj.RectangleAbsolu.Top)),
                                bmpLock.Size);
                            rctIco.Offset(-bmpLock.Width, 0);
                            g.DrawImageUnscaled(bmpLock, rctIco.Location);
                            g.DrawRectangle(Pens.Black, rctIco);
                        }
                    }
                    else
                    {
                        if (Editeur.LockEdition && obj == ControlReference)
                            penNotLock.DashStyle = DashStyle.Solid;
                        else
                            penNotLock.DashStyle = DashStyle.Dot;

                        g.DrawRectangle(penNotLock, rect);
                        if (!Editeur.LockEdition)
                        {
                            List<CPoignee> poignees = GetPoignees(obj);
                            foreach (CPoignee p in poignees)
                            {
                                p.CouleurPoignee = obj == ControlReference ? Color.Black : Color.FromArgb(80, Color.White);
                                p.CrayonBordure = obj == ControlReference ? pBordureWhite : pBordureBlack;
                                p.Draw(g);
                            }
                        }
                    }
                }
            }
            penNotLock.Dispose();
            penLock.Dispose();
        }
        #endregion

        private bool ListesIdentiques(List<I2iObjetGraphique> liste1, List<I2iObjetGraphique> liste2)
        {
            if (liste1 == null || liste2 == null)
                return false;
            if (liste1.Count == liste2.Count)
            {
                for (int n = 0; n < liste1.Count; n++)
                    if (!liste1[n].Equals(liste2[n]))
                        return false;
                return true;
            }
            return false;
        }

    }
}

