using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.common.customizableList
{
    public partial class CCustomizableList : UserControl, IControlALockEdition
    {
        private bool m_bLockEdition = false;

        private class CCacheImage
        {
            public DateTime LastAcces = DateTime.Now;
            public Image Image = null;
        }

        private const int m_nMaxImagesInCache = 50;

        private Dictionary<CCustomizableListItem, CCacheImage> m_dicItemToImage = new Dictionary<CCustomizableListItem, CCacheImage>();

        private List<CCustomizableListItem> m_listeItems = new List<CCustomizableListItem>();
        private CCustomizableListControl m_controle = null;

        private bool m_bControlEnEdition = false;

        private bool m_bMouseWheelHandled = false;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0201;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_MOUSEMOUVE = 0x0200;


        //-------------------------------------------
        public CCustomizableList()
        {
            InitializeComponent();
            System.Reflection.PropertyInfo aProp =
            typeof(System.Windows.Forms.Control).GetProperty(
               "DoubleBuffered",
               System.Reflection.BindingFlags.NonPublic |
               System.Reflection.BindingFlags.Instance);
            aProp.SetValue(m_panelDessin, true, null);

        }

        //-------------------------------------------
        [Browsable(false)]
        public virtual CCustomizableListControl ItemControl
        {
            get
            {
                return m_controle;
            }
            set
            {
                if (m_controle != null && value != m_controle)
                {
                    Controls.Remove(m_controle);
                    m_controle.Dispose();
                }
                m_controle = value;
                if (value != null)
                {
                    Controls.Add(m_controle);
                    m_controle.AssociatedListControl = this;
                    m_controle.SizeChanged += new EventHandler(m_controle_SizeChanged);
                    m_controle.SendToBack();
                    m_bControlEnEdition = false;
                    
                }
                Refill();
            }
        }

        //-------------------------------------------
        void m_controle_SizeChanged(object sender, EventArgs e)
        {
            if (
                m_bControlEnEdition && 
                CurrentItemIndex != null && 
                !m_bIsDrawing && 
                !m_controle.IsCreatingImage)
            {
                if (m_controle.CurrentItem != null)
                {
                    RefreshItem(m_controle.CurrentItem);
                    m_controle.CurrentItem.Height = m_controle.Height;
                }
                RecalcScrollSizes();
                PositionneControle( new Point(0, GetItemTop(CurrentItemIndex.Value) + m_panelDessin.AutoScrollPosition.Y));
                AssureBasControleVisible();

                m_panelDessin.Invalidate();
                m_panelDessin.Refresh();
            }
        }

        //-------------------------------------------
        public CCustomizableListItem[] Items
        {
            get
            {
                return m_listeItems.ToArray();
            }
            set
            {
                m_listeItems = new List<CCustomizableListItem>();
                if (value != null)
                    m_listeItems.AddRange(value);
                RenumerotteItems();
                ClearImages();
                Refill();
            }
        }

        //-------------------------------------------
        protected virtual void RenumerotteItems()
        {
            int nIndex = 0;
            foreach (CCustomizableListItem item in m_listeItems)
            {
                if (item.Index != nIndex)
                {
                    item.Index = nIndex;
                    RefreshItem(item);
                }
                nIndex++;
            }
        }

        private class CLockerImages
        {
        }

        //-------------------------------------------
        private void ClearImages()
        {
            lock(typeof(CLockerImages) )
            {
            foreach (CCacheImage img in m_dicItemToImage.Values)
            {
                try
                {
                    
                    if ( img.Image != null )
                        img.Image.Dispose();
                    img.Image = null;
                }
                catch { }
            }
            m_dicItemToImage.Clear();
            }
        }

        //-------------------------------------------
        [Browsable(false)]
        public override bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                base.AutoScroll = value;
            }
        }

        //-------------------------------------------
        public Image GetImage(CCustomizableListItem item, bool bKeepItemEnCours)
        {
            if (item == null)
                return null;
            lock (typeof(CLockerImages))
            {
                CCacheImage cache = null;
                if (m_dicItemToImage.TryGetValue(item, out cache))
                {
                    if (cache.Image.Size == new Size ( m_controle.Size.Width, item.Height != null?item.Height.Value : m_controle.Height) )
                    {
                        cache.LastAcces = DateTime.Now;
                        return cache.Image;
                    }
                    else
                        cache = null;
                }
                if ( cache == null )
                {
                    cache = new CCacheImage();
                    if (m_controle != null)
                    {
                        bool bOldEnEdition = m_bControlEnEdition;
                        m_bControlEnEdition = false;
                        cache.Image = m_controle.CreateImage(item, bKeepItemEnCours);
                        m_bControlEnEdition = bOldEnEdition;
                        if (cache.Image != null)
                        {
                            AddToDicImages(item, cache);
                            return cache.Image;
                        }
                    }
                }
            }
            return null;
        }

        //-------------------------------------------
        private void AddToDicImages(CCustomizableListItem item, CCacheImage cache)
        {
            m_dicItemToImage[item] = cache;
            if (m_dicItemToImage.Count > m_nMaxImagesInCache)
            {
                //Supprime l'image la plus vieille
                DateTime? dt = null;
                KeyValuePair<CCustomizableListItem, CCacheImage>? toDelete = null;
                foreach (KeyValuePair<CCustomizableListItem, CCacheImage> kv in m_dicItemToImage)
                {
                    if (dt == null || kv.Value.LastAcces < dt.Value)
                    {
                        toDelete = kv;
                        dt = kv.Value.LastAcces;
                    }
                }
                if (toDelete != null)
                {
                    if (toDelete.Value.Value.Image != null)
                        toDelete.Value.Value.Image.Dispose();
                    m_dicItemToImage.Remove(toDelete.Value.Key);
                }
            }
        }


        //-------------------------------------------
        public void InsertItem(int nIndex, CCustomizableListItem item, bool bRedraw)
        {
            m_listeItems.Insert(nIndex, item);
            RenumerotteItems();
            if (bRedraw)
                Refresh();
        }

        //-------------------------------------------
        public void AddItem(CCustomizableListItem item, bool bRedraw)
        {
            m_listeItems.Add(item);
            item.Index = m_listeItems.Count - 1;
            if (bRedraw)
                Refresh();
        }

        //-------------------------------------------
        public void RemoveItem(CCustomizableListItem item, bool bRedraw)
        {
            if ( item != null )
            {
                RemoveItem(item.Index, bRedraw);
            }
                /*int nItem = item.Index;
                if (CurrentItemIndex != null && CurrentItemIndex == nItem)
                {
                    m_controle.CancelEdit();
                    if (nItem < m_listeItems.Count - 1)
                        CurrentItemIndex = nItem + 1;
                    else if (nItem > 0)
                        CurrentItemIndex = nItem - 1;
                    else
                        CurrentItemIndex = -1;
                }
            }
            m_listeItems.Remove(item);
            RenumerotteItems();
            if (bRedraw)
                Refresh(); */           
        }

        //-------------------------------------------
        public virtual void RemoveItem(int nIndex, bool bRedraw)
        {
            if (CurrentItemIndex != null && CurrentItemIndex.Value == nIndex)
            {
                m_controle.CancelEdit();
                if (nIndex < m_listeItems.Count - 1)
                    CurrentItemIndex = nIndex + 1;
                else if (nIndex > 0)
                    CurrentItemIndex = nIndex - 1;
                else
                    CurrentItemIndex = -1;
            }
            if (nIndex >= 0 && nIndex < m_listeItems.Count)
                m_listeItems[nIndex].OnRemoveFromList();
            m_listeItems.RemoveAt(nIndex);
            
            RenumerotteItems();
            if (bRedraw)
                Refresh();
        }

        //-------------------------------------------
        public void Refill()
        {
            if (m_controle == null)
                return;
            m_controle.CancelEdit();
            EditeItem(-1);
            ClearImages();
            Refresh();
        }

        //-------------------------------------------
        public CCustomizableListItem GetFirstVisibleItem()
        {
            int? nItem = GetItemIndexAtPosition(-m_panelDessin.AutoScrollPosition.Y);
            if (nItem != null)
            {
                if (nItem < 0)
                    return null;
                if (nItem >= m_listeItems.Count)
                    nItem = m_listeItems.Count - 1;
                if ( nItem >= 0 )
                    return m_listeItems[nItem.Value];
            }
            return null;
        }

        //-------------------------------------------
        public CCustomizableListItem GetLastVisibleItem()
        {
            int? nItem = GetItemIndexAtPosition(-m_panelDessin.AutoScrollPosition.Y+m_panelDessin.Height);
            if (nItem != null)
            {
                if (nItem < 0)
                    return null;
                if (nItem >= m_listeItems.Count)
                    nItem = m_listeItems.Count - 1;
                if ( nItem >= 0 )
                    return m_listeItems[nItem.Value];
            }
            if (nItem == null && m_listeItems.Count > 0)
                return m_listeItems[m_listeItems.Count - 1];
            return null;
        }

        //-------------------------------------------
        public Rectangle GetItemRect(int nItem)
        {
            int nHeight = GetItemHeight(nItem);
            int nTop = GetItemTop(nItem)+m_panelDessin.AutoScrollPosition.Y;
            return new Rectangle(0, nTop, m_panelDessin.Width, nHeight);
        }

        //-------------------------------------------
        public int? GetItemIndexAtPosition(int nY)
        {
            int nPos = 0;
            if (m_controle == null)
                return null;
            //contrôle à taille fixe->fastoche
            if (m_controle.IsFixedSize)
                return nY / m_controle.Height;

            //Sinon, on est obligé de calculer la taille de tous les précédents !
            int? nLastNotHeightZero = null;
            for (int nIndex = 0; nIndex < m_listeItems.Count(); nIndex++)
            {
                CCustomizableListItem item = m_listeItems[nIndex];
                int nHeight = m_controle.GetItemHeight(item);
                if (nHeight > 0)
                    nLastNotHeightZero = nIndex;
                if (nPos <= nY && nPos + nHeight > nY)
                {
                    if (nLastNotHeightZero != null)
                        return nLastNotHeightZero.Value;
                    return nIndex;
                }
                nPos += nHeight;
            }
            return null;
        }

        //-------------------------------------------
        public int GetItemTop(int nItem)
        {
            if (m_controle == null)
                return 0;
            if (m_controle.IsFixedSize)
                return nItem * m_controle.Height;
            int nPos = 0;
            for (int nItemTest = 0; nItemTest < nItem; nItemTest++)
            {
                CCustomizableListItem item = m_listeItems[nItemTest];
                nPos += m_controle.GetItemHeight(item);
            }
            return nPos;
        }

        //-------------------------------------------
        /// <summary>
        /// Retourne le précédent visible d'un index
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public CCustomizableListItem GetVisibleItemBefore(int nIndex)
        {
            nIndex--;
            if (nIndex >= 0)
            {
                CCustomizableListItem item = m_listeItems[nIndex];
                if (item.IsMasque)
                    return GetVisibleItemBefore(nIndex);
                return item;
            }
            return null;
        }

        //-------------------------------------------
        /// <summary>
        /// Retourne le suivant visible d'un index
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public CCustomizableListItem GetVisibleItemAfter(int nIndex)
        {
            nIndex++;
            if (nIndex < m_listeItems.Count)
            {
                CCustomizableListItem item = m_listeItems[nIndex];
                if (item.IsMasque)
                    return GetVisibleItemAfter(nIndex);
                return item;
            }
            return null;
        }

        //-------------------------------------------
        public int GetItemHeight(int nItem)
        {
            if (m_controle == null || nItem < 0 || nItem >= m_listeItems.Count)
                return 1;
            if (m_controle.IsFixedSize)
                return m_controle.Height;
            return m_controle.GetItemHeight(m_listeItems[nItem]);
        }

        public event EventHandler AfterPaint;

        //-------------------------------------------
        private bool m_bIsDrawing = false;
        private void m_PanelDessin_Paint(object sender, PaintEventArgs e)
        {
            if (m_bIsDrawing)
                return;
            try
            {
                if (Items.Count() == 0 && ItemControl != null)
                    ItemControl.Visible = false;
                if (Items.Count() > 0 && ItemControl != null)
                    ItemControl.Visible = true;

                m_bIsDrawing = true;
                if (!m_bMouseWheelHandled)
                {
                    Form frm = FindForm();
                    if (frm != null)
                    {
                        frm.MouseWheel += new MouseEventHandler(frm_MouseWheel);
                        m_bMouseWheelHandled = true;
                    }
                }
                if (m_controle != null)
                {
                    m_controle.PreparePaintList();
                    RecalcScrollSizes();
                    int? nCurrent = CurrentItemIndex;
                    int? nItem = GetItemIndexAtPosition(-m_panelDessin.AutoScrollPosition.Y);
                    if (nItem == null)
                        return;
                    int nY = GetItemTop(nItem.Value) + m_panelDessin.AutoScrollPosition.Y;
                    while (nY < ClientSize.Height && nItem < m_listeItems.Count)
                    {
                        CCustomizableListItem item = m_listeItems[nItem.Value];
                        if (!item.IsMasque)
                        {
                            Image img = GetImage(item, false);
                            if (item.Height > 0)
                            {
                                Rectangle rct = new Rectangle(-5, nY, m_panelDessin.ClientSize.Width + 10, item.Height.Value);
                                Brush br = new SolidBrush(BackColor);
                                e.Graphics.FillRectangle(br, rct);
                                br.Dispose();

                                if (img != null)
                                {
                                    e.Graphics.DrawImageUnscaled(img,
                                        new Point(m_panelDessin.AutoScrollPosition.X, nY));
                                }
                                if (nCurrent == nItem)
                                {
                                    Pen p = new Pen(m_controle.BackColor);
                                    e.Graphics.DrawRectangle(p, m_panelDessin.AutoScrollPosition.X, nY, m_controle.Width - 1, item.Height.Value - 1);
                                    p.Dispose();
                                }
                            }
                        }
                        else
                            item.Height = 0;
                        nItem++;
                        if (item.Height == null)
                            nY += m_controle.GetItemHeight(item);
                        else
                            nY += item.Height.Value;
                    }

                    if (m_controle != null)
                        m_controle.OnEndPaintList();
                    /*if ( nCurrent != null && (m_controle.CurrentItem == null ||
                        m_controle.CurrentItem.Index != nCurrent ))
                        m_controle.InitChamps ( Items[nCurrent.Value] );*/

                }

            }
            finally
            {
                m_bIsDrawing = false;
            }
            if (AfterPaint != null)
                AfterPaint(this, null);
        }

        public event EventHandler ScrollSizeChanged;

        //-------------------------------------------
        private void RecalcScrollSizes()
        {
            if (m_controle != null)
            {
                int nWidth = 0;
                if (m_controle.MinWidth != null)
                    nWidth = Math.Max(m_panelDessin.ClientSize.Width, m_controle.MinWidth.Value);
                int nY = m_panelDessin.AutoScrollPosition.Y;
                if (m_controle.IsFixedSize)
                {
                    if (m_listeItems.Count * m_controle.Height != m_panelDessin.AutoScrollMinSize.Height)
                    {
                        m_panelDessin.AutoScrollMinSize = new Size(
                            nWidth,
                            m_listeItems.Count * m_controle.Height);
                    }
                }
                else
                {
                    int nHeight = 0;
                    foreach (CCustomizableListItem item in m_listeItems)
                        nHeight += m_controle.GetItemHeight(item);
                    if ( nHeight != m_panelDessin.AutoScrollMinSize.Height ||
                        nWidth != m_panelDessin.AutoScrollMinSize.Width)
                        m_panelDessin.AutoScrollMinSize = new Size(
                        nWidth,
                        nHeight);
                }
                m_controle.Width = nWidth == 0 ?m_panelDessin.ClientSize.Width : nWidth;
                if (ScrollSizeChanged != null)
                    ScrollSizeChanged(this, new EventArgs());
            }

        }

        //-------------------------------------------
        public void RefreshItem(CCustomizableListItem item)
        {
            if (item != null)
            {
                lock (typeof(CLockerImages))
                {
                    CCacheImage cache = null;
                    if (m_dicItemToImage.TryGetValue(item, out cache))
                    {
                        if (cache.Image != null)
                            cache.Image.Dispose();
                        m_dicItemToImage.Remove(item);
                    }
                }
            }
        }

        //-----------------------------------------------------------
        public event EventHandler SelectionChanged;


        //-------------------------------------------
        private void EditeItem(int nItem)
        {
            if (m_controle != null )
            {
                Control lastActive = m_controle.GetActiveControl();
                m_controle.SendToBack();
                PerformLayout();
                m_controle.MajChamps();
                if (m_controle.CurrentItem != null)
                {
                    m_controle.CurrentItem.IsSelected = false;
                    RefreshItem(m_controle.CurrentItem);
                    //Redessine le contrôle tout de suite, comme ça il est à jour, et pas de problème de remise en place du contrôle en cours
                    GetImage(m_controle.CurrentItem, false);
                }
                m_controle.CancelEdit();//On n'édite plus rien jusqu'à ce qu'on
                //sélectionnne ce qu'il faut éditer
                if ( nItem >= 0 && nItem < m_listeItems.Count )
                {
                    int nY = GetItemTop(nItem) + m_panelDessin.AutoScrollPosition.Y;
                    if (nY < 0)
                        m_panelDessin.AutoScrollPosition = new Point(
                            -m_panelDessin.AutoScrollPosition.X,
                            -m_panelDessin.AutoScrollPosition.Y + nY);
                    nY = GetItemTop(nItem) + m_panelDessin.AutoScrollPosition.Y;
                    PositionneControle(new Point(m_panelDessin.AutoScrollPosition.X,
                        GetItemTop(nItem) + m_panelDessin.AutoScrollPosition.Y));
                    m_listeItems[nItem].IsSelected = true;
                    m_controle.InitChamps(m_listeItems[nItem]);
                    AssureBasControleVisible();
                    RefreshItem(m_listeItems[nItem]);
                    m_panelDessin.Refresh();
                    m_controle.BringToFront();
                    m_bControlEnEdition = true;
                }
                else
                {
                    m_controle.InitChamps ( null );
                    m_controle.Location = new Point ( -2*m_controle.Width, -m_controle.Height );
                }
                if (lastActive != null)
                {
                    try
                    {
                        m_controle.ActiveControl = lastActive;
                    }
                    catch { }
                }
            }
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }

        //----------------------------------------------------------------------
        private void PositionneControle(Point ptInDessinCoordinates)
        {
            Point pt = new Point(0, 0);
            pt = m_panelDessin.PointToScreen(pt);
            pt = PointToClient(pt);
            pt.Offset(ptInDessinCoordinates);
            m_controle.Location = pt;
        }

        //----------------------------------------------------------------------
        private void AssureBasControleVisible()
        {
            int? nCurrentIndex = CurrentItemIndex;
            if (nCurrentIndex != null && GetItemTop(nCurrentIndex.Value) + m_panelDessin.AutoScrollPosition.Y + m_controle.Height > m_panelDessin.ClientSize.Height)
            {
                int nDy = GetItemTop(nCurrentIndex.Value) + m_panelDessin.AutoScrollPosition.Y + m_controle.Height - m_panelDessin.ClientSize.Height;
                m_panelDessin.AutoScrollPosition = new Point(
                    -m_panelDessin.AutoScrollPosition.X,
                    -m_panelDessin.AutoScrollPosition.Y + nDy);
                PositionneControle(new Point(m_panelDessin.AutoScrollPosition.X,
                        GetItemTop(nCurrentIndex.Value) + m_panelDessin.AutoScrollPosition.Y));
            }
        }


        //----------------------------------------------------------------------
        private void m_panelDessin_Scroll(object sender, ScrollEventArgs e)
        {
            if (m_controle != null)
                m_controle.MajChamps();
            m_panelDessin.Focus();
            if (m_controle != null)
            {
                m_controle.SendToBack();
                m_bControlEnEdition = false;
            }
        }

      

        //----------------------------------------------------------------------
        private void m_panelDessin_MouseDown(object sender, MouseEventArgs e)
        {
            int nY = e.Y - m_panelDessin.AutoScrollPosition.Y;
            if (m_controle != null)
            {
                int? nItem = GetItemIndexAtPosition(nY);
                if (nItem != null)
                {
                    EditeItem(nItem.Value);
                    Point ptScreen = m_panelDessin.PointToScreen(new Point(e.X, e.Y));
                    Point pt = m_controle.PointToClient(ptScreen);
                    Control ctrl = m_controle.GetChildAtPoint(pt);
                    Control selected = ctrl;
                    while ( ctrl != null )
                    {
                        pt = ctrl.PointToClient ( ptScreen );
                        ctrl = ctrl.GetChildAtPoint(pt);
                        if ( ctrl != null )
                            selected = ctrl;
                    }
                    ctrl = selected;
                    if (ctrl == null)
                        ctrl = m_controle;
                    if (ctrl != null)
                    {
                        ctrl.Focus();

                        //Transmet le message au contrôle
                        pt = ctrl.PointToClient(ptScreen);
                        int wParam = 0;
                        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                            wParam |= 0x0001;
                        if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
                            wParam |= 0x0002;
                        if ((e.Button & MouseButtons.Middle) == MouseButtons.Right)
                            wParam |= 0x0010;

                        int lParam = 0;
                        lParam = (Int16)pt.Y << 16 + (Int16)pt.X;

                        PostMessage(ctrl.Handle, WM_MOUSEMOUVE, (IntPtr)wParam, (IntPtr)lParam);
                        PostMessage(ctrl.Handle, WM_LBUTTONDOWN, (IntPtr)wParam, (IntPtr)lParam);

                        
                    }
                }
            }
        }

        //-------------------------------------------------------
        private int? GetItemIndex(CCustomizableListItem item)
        {
            int nIndex = m_listeItems.IndexOf(item);
            return nIndex >= 0 ? (int?)nIndex : null;
        }

        //-------------------------------------------------------
        public int? CurrentItemIndex
        {
            get
            {
                if (m_controle != null && m_controle.CurrentItem != null)
                {
                    return GetItemIndex(m_controle.CurrentItem);
                }
                return null;
            }
            set
            {
                if (value != null && value.Value >= 0 && value.Value < m_listeItems.Count)
                {
                    EditeItem(value.Value);
                }
                else
                    EditeItem(-1);
                    
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                int? nCurrentIndex = CurrentItemIndex;
                Keys key = (Keys)m.WParam;
                if (ItemControl != null && ItemControl.ShouldManageKey(key))
                    return false;
                switch (key)
                {
                    case Keys.Down:
                        if (nCurrentIndex != null)
                        {
                            int nIndex = nCurrentIndex.Value + 1;
                            while (nIndex < m_listeItems.Count && m_listeItems[nIndex].IsMasque)
                                nIndex++;
                            if (nIndex < m_listeItems.Count)
                                CurrentItemIndex = nIndex;
                            return true;
                        }
                        break;
                    case Keys.Up:
                        if (nCurrentIndex != null)
                        {
                            int nIndex = nCurrentIndex.Value - 1;
                            while (nIndex >=0 && m_listeItems[nIndex].IsMasque)
                                nIndex--;
                            if (nIndex >=0)
                                CurrentItemIndex = nIndex;
                            return true;
                        }
                        break;
                    case Keys.PageDown:
                        if (nCurrentIndex != null)
                        {
                            int nY = GetItemTop(nCurrentIndex.Value);
                            int? nItem = GetItemIndexAtPosition(nY + m_panelDessin.ClientSize.Height - 10);
                            if (nItem != null)
                            {
                                if (nItem == nCurrentIndex)
                                    nItem++;
                                CurrentItemIndex = nItem.Value;
                            }
                            else
                                CurrentItemIndex = m_listeItems.Count - 1;
                        }
                        break;
                    case Keys.PageUp:

                        if (nCurrentIndex != null)
                        {
                            int nY = GetItemTop(nCurrentIndex.Value);
                            int? nItem = GetItemIndexAtPosition(nY - m_panelDessin.ClientSize.Height - 10);
                            if (nItem != null)
                            {
                                if (nItem == nCurrentIndex)
                                    nItem++;
                                CurrentItemIndex = nItem.Value;
                            }
                            else
                                CurrentItemIndex = 0;
                        }
                        break;
                    case Keys.Home:
                        if (m_listeItems.Count > 0)
                        {
                            CurrentItemIndex = 0;
                            return true;
                        }
                        break;
                    case Keys.End:
                        if (m_listeItems.Count > 0)
                        {
                            int nIndex = m_listeItems.Count - 1;
                            while (nIndex >= 0 && m_listeItems[nIndex].IsMasque)
                                nIndex--;
                            CurrentItemIndex = nIndex;
                            return true;
                        }
                        break;
                }

            }
            return base.ProcessKeyPreview(ref m);
        }

        

        //----------------------------------------------------------------------
        void frm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ActiveControl == m_controle && m_bControlEnEdition)
            {
                m_panelDessin.AutoScrollPosition = new Point(
                    -m_panelDessin.AutoScrollPosition.X,
                    -m_panelDessin.AutoScrollPosition.Y - e.Delta);
                m_controle.SendToBack();
                m_panelDessin.Focus();
                m_bControlEnEdition = false;

            }
        }

        //----------------------------------------------------------------------
        private void m_panelDessin_SizeChanged(object sender, EventArgs e)
        {
            if (m_controle != null)
            {
                int nWidth = Width;
                if (m_controle.MinWidth != null)
                    nWidth = Math.Max(nWidth, m_controle.MinWidth.Value);
                m_controle.Size = new Size(nWidth, m_controle.Height);
                ClearImages();
                m_panelDessin.Invalidate();
            }
        }

        //----------------------------------------------------------------------
        public void CancelEdit()
        {
            if (m_controle != null)
            {
                m_controle.CancelEdit();
                m_controle.SendToBack();
            }
        }

        //----------------------------------------------------------------------
        protected int? GetIndexDragDrop ( Point ptClient, ref bool bIsAvant )
        {
            bIsAvant = true;
            int? nIndex = GetItemIndexAtPosition(ptClient.Y-m_panelDessin.AutoScrollPosition.Y);
            if (nIndex == null || nIndex.Value > m_listeItems.Count)
            {
                //A priori on est à la fin
                nIndex = m_listeItems.Count;
            }
            else
            {

                int nHeight = GetItemHeight(nIndex.Value);
                int nY = ptClient.Y - GetItemTop(nIndex.Value) - m_panelDessin.AutoScrollPosition.Y;
                if (nY > nHeight / 2)
                {
                    bIsAvant = false;
                    nIndex = nIndex + 1;
                }
            }
            if (nIndex != null && nIndex.Value > m_listeItems.Count)
                nIndex = m_listeItems.Count;
            return nIndex;
        }

        //----------------------------------------------------------------------
        protected virtual void m_panelDessin_DragDrop(object sender, DragEventArgs e)
        {
            CCustomizableListDragDropData data = e.Data.GetData(typeof(CCustomizableListDragDropData)) as CCustomizableListDragDropData;
            if (data != null && !LockEdition)
            {
                Point pt = new Point(e.X, e.Y);
                pt = m_panelDessin.PointToClient(pt);
                bool bIsAvant = false;
                int? nIndexDest = GetIndexDragDrop(pt, ref bIsAvant);
                if (nIndexDest != null)
                {
                    CurrentItemIndex = null;
                    int nIndexSource = data.DraggedIndex;
                    MoveItem(nIndexSource, nIndexDest.Value, bIsAvant);
                }
            }
            HideHighlight();
            m_panelDessin.Refresh();

        }

        protected virtual void MoveItem(int nIndexSource, int nIndexDest, bool bCursorIsAvant)
        {
            CCustomizableListItem item = m_listeItems[nIndexSource];
            if (nIndexDest > nIndexSource)
            {
                m_listeItems.Insert(nIndexDest, item);
                m_listeItems.RemoveAt(nIndexSource);
                CurrentItemIndex = nIndexDest - 1;
            }
            else
            {
                m_listeItems.RemoveAt(nIndexSource);
                m_listeItems.Insert(nIndexDest, item);
                CurrentItemIndex = nIndexDest;
            }

            RenumerotteItems();

        }

        //----------------------------------------------------------------------
        protected Rectangle? m_lastRectHighlight = null;
        protected virtual void HighlightZoneItem(Point ptScreen)
        {
            ptScreen = m_panelDessin.PointToClient(ptScreen);
            bool bIsAvant = false;
            int? nIndex = GetIndexDragDrop ( ptScreen, ref bIsAvant );
            if (nIndex != null)
            {
                int nY = GetItemTop(nIndex.Value)+m_panelDessin.AutoScrollPosition.Y;
                Pen p = new Pen(Color.Blue, 3);
                Graphics g = m_panelDessin.CreateGraphics();
                g.DrawLine(p, 0, nY-1, m_panelDessin.ClientSize.Width, nY-1);
                Rectangle rct = new Rectangle(0, nY - 2, m_panelDessin.ClientSize.Width, 4); 
                if  ( m_lastRectHighlight != null &&  rct != m_lastRectHighlight.Value )
                    HideHighlight();
                m_lastRectHighlight = rct;
                p.Dispose();
                g.Dispose();
            }
        }

        //----------------------------------------------------------------------
        protected virtual void HideHighlight()
        {
            if ( m_lastRectHighlight != null )
            {
                m_panelDessin.Invalidate ( m_lastRectHighlight.Value );
                m_lastRectHighlight = null;
            }
        }


        //----------------------------------------------------------------------
        public virtual DragDropEffects GetDragDropEffect(DragEventArgs e)
        {
            IDataObject data = e.Data;
            CCustomizableListDragDropData custData = data.GetData(typeof(CCustomizableListDragDropData)) as CCustomizableListDragDropData;
            if (custData != null && custData.ListControl == this)
                return DragDropEffects.Move;
            return DragDropEffects.None;
        }

        //----------------------------------------------------------------------
        private int? m_nCurrentIndexOnDragEnter = null;
        protected virtual void m_panelDessin_DragEnter(object sender, DragEventArgs e)
        {
            m_nCurrentIndexOnDragEnter = CurrentItemIndex;
            e.Effect = GetDragDropEffect(e);
            if (!LockEdition && e.Effect != DragDropEffects.None)
            {
                HighlightZoneItem(new Point(e.X, e.Y));
                e.Effect = DragDropEffects.Move;
                CurrentItemIndex = null;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        //----------------------------------------------------------------------
        protected virtual void m_panelDessin_DragLeave(object sender, EventArgs e)
        {
            HideHighlight();
            CurrentItemIndex = m_nCurrentIndexOnDragEnter;
            Refresh();
        }

        //----------------------------------------------------------------------
        protected virtual void m_panelDessin_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = GetDragDropEffect(e);
            if (e.Effect != DragDropEffects.None && !LockEdition)
            {
                Point pt = m_panelDessin.PointToClient(new Point(e.X, e.Y));
                if (pt.Y > m_panelDessin.ClientSize.Height - 10)
                {
                    m_panelDessin.AutoScrollPosition = new Point(0, -m_panelDessin.AutoScrollPosition.Y + 10);
                    Refresh();
                }
                if (pt.Y < 10)
                {
                    m_panelDessin.AutoScrollPosition = new Point(0, -m_panelDessin.AutoScrollPosition.Y - 10);
                    Refresh();
                }

                HighlightZoneItem(new Point(e.X, e.Y));
                return;
            }
            else
            {
                if (m_lastRectHighlight != null)
                    HideHighlight();
            }


            e.Effect = DragDropEffects.None;
        }

        //------------------------------------------------------------
        public virtual bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                if (OnChangeLockEdition != null)
                    return;
                if (ItemControl != null)
                {
                    IControlALockEdition ctrlLock = ItemControl as IControlALockEdition;
                    if (ctrlLock != null)
                        ctrlLock.LockEdition = value;
                    else
                        ItemControl.Enabled = !value;
                }
            }
        }

        //------------------------------------------------------------
        public event EventHandler OnChangeLockEdition;

        //------------------------------------------------------------
        private void CCustomizableList_BackColorChanged(object sender, EventArgs e)
        {
            m_panelDessin.BackColor = BackColor;
        }

        //------------------------------------------------------------
        private void CCustomizableList_Enter(object sender, EventArgs e)
        {
            if (CurrentItemIndex == null && Items.Count() > 0)
                CurrentItemIndex = 0;
        }

        //------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (ItemControl != null && CurrentItemIndex != null)
            {
                result = ItemControl.MajChamps();
            }
            return result;
        }
                
    }
}

