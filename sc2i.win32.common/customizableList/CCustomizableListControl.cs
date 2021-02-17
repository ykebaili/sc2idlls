using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using System.Drawing;

namespace sc2i.win32.common.customizableList
{
    public class CCustomizableListControl : UserControl, IControlALockEdition
    {
        private CCustomizableListItem m_currentItem = null;

        private CCustomizableList m_listControl = null;
        protected CExtModeEdition m_extModeEdition;

        private Color m_colorInactive = SystemColors.Control;

        private bool m_bHasChange = false;

        public CCustomizableListControl()
            : base()
        {
            InitializeComponent();
        }

        //----------------------------------------------------
        /// <summary>
        /// Indique si en changeant d'item, on doit stocker les status des
        /// controles dans l'item (via un CDonneesSpecifiquesControleDansCustomList)
        /// </summary>
        public virtual bool ShouldSaveControlsState
        {
            get
            {
                return false;
            }
        }


        //----------------------------------------------------
        /// <summary>
        /// LArgeur mini du contrôle -null si aucune)
        /// </summary>
        public virtual int? MinWidth
        {
            get
            {
                return null;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Indique si les données ont changé de InitChamps
        /// ATTENTION : INDISPENSABLE. Majchamps n'est appellé que si HasChange
        /// est true.
        /// </summary>
        public virtual bool HasChange
        {
            get
            {
                return m_bHasChange;
            }
            set
            {
                m_bHasChange = value;
                if (value && DataChanged != null)
                    DataChanged(this, null);
            }
        }


        //----------------------------------------------------
        public Color ColorInactive
        {
            get
            {
                return m_colorInactive;
            }
            set
            {
                m_colorInactive = value;
            }
        }

        

        //----------------------------------------------------
        public CCustomizableListItem CurrentItem
        {
            get
            {
                return m_currentItem;
            }
        }

        //----------------------------------------------------
        public virtual bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Si true, indique que le contrôle a un aspect différent lorsqu'il
        /// est inactif, et qu'il faut donc rappeller systématiquement "InitChamps"
        /// lors de la création de l'image.
        /// </summary>
        public virtual bool AspectDifferentEnInactif
        {
            get
            {
                return false;
            }
        }

        //----------------------------------------------------
        public CCustomizableList AssociatedListControl
        {
            get
            {
                return m_listControl;
            }
            set
            {
                m_listControl = value;
            }
        }


        //----------------------------------------------------
        public CResultAErreur InitChamps(CCustomizableListItem item)
        {
            m_currentItem = item;
            CResultAErreur result = MyInitChamps(item);
            if (result)
                HasChange = false;
            if (item != null && item.DonneesControles != null)
                CUtilDonneesSpecifiquesDansCustomList.RestoreDonneesControle(this, item.DonneesControles);
            if ( item != null )
                item.Height = item.IsMasque?0:Height;
            return result;
        }


        //----------------------------------------------------
        /// <summary>
        /// Crée l'image d'un item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="bGereItemEnCours">si true, en sortie, le contrôle
        /// aura repris l'aspect qu'il avait avant</param>
        /// <returns></returns>
        public Image CreateImage(CCustomizableListItem item, bool bGereItemEnCours)
        {
            CCustomizableListItem old = m_currentItem;
            IsCreatingImage = true;
            if (item != m_currentItem || AspectDifferentEnInactif)
            {
                OnStartCreateImage();
                if (bGereItemEnCours)
                    MajChamps();
                InitChamps(item);
            }
            Image img = CreateCurrentItemImage();
            IsCreatingImage = false;
            if (item != m_currentItem)
            {
                if (bGereItemEnCours)
                    InitChamps(old);
            }

            return img;
        }

        //----------------------------------------------------
        /// <summary>
        /// Permet de calculer la hauteur d'un ITEM.
        /// Par défaut retourne la hauteur du contrôle
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int GetItemHeight(CCustomizableListItem item)
        {
            if (item.IsMasque)
                return 0;
            if (IsFixedSize || item.Height == null)
                return Height;
            return item.Height.Value;
        }

        //----------------------------------------------------
        public void CancelEdit()
        {
            m_currentItem = null;
        }

        //----------------------------------------------------
        protected virtual bool ShouldDrawInImage(Control ctrl)
        {
            return ctrl.Width > 0 && ctrl.Height > 0 && ctrl.Visible;
        }

        public bool IsCreatingImage { get; set; }

        //----------------------------------------------------
        protected virtual Image CreateCurrentItemImage()
        {
            if (m_currentItem != null)
            {
                Bitmap bmp = null;
                if (Size.Width == 0 || Size.Height == 0)
                {
                    bmp = new Bitmap(1, 1);
                    return bmp;
                }
                bmp = new Bitmap(Size.Width, Size.Height);
                Color oldBack = BackColor;
                if (!m_currentItem.IsSelected)
                {
                    if (m_currentItem.ColorInactive != null)
                        BackColor = m_currentItem.ColorInactive.Value;
                    else
                        BackColor = ColorInactive;
                }
                Graphics g = Graphics.FromImage(bmp);
                Brush br = new SolidBrush(BackColor);
                g.FillRectangle(br, new Rectangle(0, 0, Size.Width, Size.Height));
                br.Dispose();
                foreach (Control ctrl in Controls)
                {
                    if (ShouldDrawInImage(ctrl))
                    {
                        try
                        {
                            ctrl.DrawToBitmap(bmp, new Rectangle(ctrl.Left, ctrl.Top, ctrl.Width, ctrl.Height));
                        }
                        catch
                        {
                        }
                    }
                }
                BackColor = oldBack;
                g.Dispose();
                return bmp;
            }
            return null;
        }


        //----------------------------------------------------
        protected virtual CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            return CResultAErreur.True;
        }


        //----------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (!LockEdition && HasChange)
            {
                result = MyMajChamps();
                if (DataChanged != null)
                    DataChanged(this, null);
            }
            if (CurrentItem != null)
            {
                if (ShouldSaveControlsState)
                {
                    CDonneesSpecifiquesControleDansCustomList data = new CDonneesSpecifiquesControleDansCustomList();
                    CUtilDonneesSpecifiquesDansCustomList.SaveDonneesControle(this, data);
                    CurrentItem.DonneesControles = data;
                }
                else
                    CurrentItem.DonneesControles = null;
            }
            return result;
        }

        //----------------------------------------------------
        protected virtual CResultAErreur MyMajChamps()
        {
            return CResultAErreur.True;
        }

        //----------------------------------------------------
        private void InitializeComponent()
        {
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // CCustomizableListControl
            // 
            this.DoubleBuffered = true;
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CCustomizableListControl";
            this.ResumeLayout(false);

        }

        //----------------------------------------------------
        public event EventHandler DataChanged;

        public delegate bool OnLeaveLastControlEventHandler(object sender, EventArgs args);

        //----------------------------------------------------
        public event OnLeaveLastControlEventHandler OnLeaveLastControl;

        //----------------------------------------------------
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
            {
                bool bResult = false;
                bool bForward = (keyData & Keys.Shift) != Keys.Shift;
                Control ctrl = this.GetActiveControl();
                Control next = this.GetNextTabOrderedControl(ctrl, bForward);
             
                if (next == null)
                {
                    if (m_listControl != null)
                    {
                        int? nIndex = m_listControl.CurrentItemIndex;
                        if (nIndex != null)
                        {
                            if (bForward)
                                nIndex++;
                            else
                                nIndex--;
                            m_listControl.CurrentItemIndex = nIndex.Value;
                            //Trouve le premier ou le dernier et le sélectionne
                            List<Control> lst = this.GetTabOrderedControlsList();
                            if (lst.Count != 0)
                            {
                                if (bForward)
                                    ActiveControl = lst[0];
                                else
                                    ActiveControl = lst[lst.Count - 1];
                            }
                            if (nIndex.Value < AssociatedListControl.Items.Count())
                            {
                                bResult = true;
                            }

                        }
                    }

                }
                else
                {
                    ActiveControl = next;
                    bResult = true;
                    //return base.ProcessDialogKey(keyData);
                }
                    
                if (bForward && !bResult)
                    if (OnLeaveLastControl != null)
                    {
                        if (OnLeaveLastControl(this, null))
                            return true;
                    }
                if ( !bResult )
                    return base.ProcessDialogKey(keyData);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        public struct CDonneeAdditionnelleDragDrop
        {
            public Type Type;
            public Object Objet;

            public CDonneeAdditionnelleDragDrop(Type tp, Object obj)
            {
                Type = tp;
                Objet = obj;
            }
        }
        //----------------------------------------------------
        public void StartDragDrop(
            Point pt, 
            DragDropEffects effects,
            params CDonneeAdditionnelleDragDrop[] donnesAditionnelles
            )
        {
            if ( CurrentItem != null )
            {
                DataObject obj = new DataObject();
                foreach ( CDonneeAdditionnelleDragDrop d in donnesAditionnelles )
                {
                    obj.SetData ( d.Type, d.Objet );
                }
                obj.SetData ( typeof(CCustomizableListDragDropData),new CCustomizableListDragDropData(m_listControl, CurrentItem.Index, pt));
                DoDragDrop( obj, effects);
            }
        }



        //----------------------------------------------------
        private bool m_bAToutSauvéAvantDeFaireDesImages = true;
        public void PreparePaintList()
        {
            m_bAToutSauvéAvantDeFaireDesImages = false;
        }

        //----------------------------------------------------
        //Appellé par la liste pour signifier au contrôle que la liste
        //va se dessiner, et que du coup, le contrôle va changer
        //Provisoirement d'item en cours
        private CCustomizableListItem m_itemBeforePaint = null;
        private Point? m_ptLocationBeforePaint = null;
        private List<Control> m_listActives = new List<Control>();
        public virtual void OnStartCreateImage()
        {
            if (m_bAToutSauvéAvantDeFaireDesImages)
                return;
            m_bAToutSauvéAvantDeFaireDesImages = true;
            m_itemBeforePaint = CurrentItem;
            if (Bottom > 0)
            {
                if (CurrentItem != null)
                    MajChamps();
                m_listActives = CUtilControl.GetActiveControls(this);
                m_ptLocationBeforePaint = Location;
                Location = new Point(0, -Height * 100);
                ActiveControl = null;
            }
        }

        //----------------------------------------------------
        public virtual void OnEndPaintList()
        {
            if (m_bAToutSauvéAvantDeFaireDesImages)
            {
                InitChamps(m_itemBeforePaint);
                if (m_ptLocationBeforePaint != null)
                    Location = m_ptLocationBeforePaint.Value;
                CUtilControl.SetActiveControls(this, m_listActives);
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Indique si on doit laisser la touche au contrôle ou si la 
        /// liste est autorisée à la gérer
        /// Par exemple, dans une ComboBox, la combo doit gérer le UP et DOWN,
        /// mais une TextBox mono ligne n'a pas besoin de le faire
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool ShouldManageKey(Keys key)
        {
            Control ctrl = this.GetActiveControl();
            if (ctrl == null)
                return false;
            if (typeof(TextBox).IsAssignableFrom(ctrl.GetType()))
            {
                TextBox txt = ctrl as TextBox;
                if (key == Keys.End && txt.SelectionStart + txt.SelectionLength < txt.Text.Length)
                    return true;
                if (key == Keys.Home && txt.SelectionStart != 0)
                    return true;
            }
            if (typeof(ComboBox).IsAssignableFrom(ctrl.GetType()))
            {
                if (key == Keys.Down || key == Keys.Up)
                    return true;
            }

            return false;
        }




        #region IControlALockEdition Membres

        public virtual bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

       
    }
}
