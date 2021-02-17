/***************************************************
 * Glacial List v1.21
 * 
 * Written By Allen Anderson
 * http://www.glacialcomponents.com
 * 
 * November 5th, 2003
 * 
 * You may redistribute this control as you please without modifications.  You may
 * use this control in commercial applications without need for external credit royalty free.
 * 
 * However, you are restricted from releasing the source code in any modified fashion
 * whatsoever.  The source must be 
 * 
 */


using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using sc2i.common;



namespace sc2i.win32.common
{

	public enum GLGridStyles { gridNone=0, gridSolid=1 }

	public enum GLHeaderStyles
	{
		Normal = 0,
		SuperFlat = 1,
		XP = 2,
		None = 3
	}


	public delegate void GlacialListCheckedChangeEventHandler ( object sender, int nNumItem );
    public delegate Image GlacialListGetImageEventHandler ( object obj );
	

	/// <summary>
	/// Summary description for GlacialList.
	/// </summary>
	public class GlacialList : System.Windows.Forms.Control, IElementAContexteUtilisation, IControlTraductible
	{
		#region Debugging

		public static void DW( string strout )			// debug write
		{
#if false
			//System.IO.StreamWriter sw = new System.IO.StreamWriter( "e:\\debug.txt", true );
			//sw.WriteLine( strout );
			//sw.Close();
#else
			//Debug.WriteLine( strout );
#endif
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_popupMenu = new System.Windows.Forms.ContextMenu();
            this.m_timerTooltip = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_popupMenu
            // 
            this.m_popupMenu.Popup += new System.EventHandler(this.m_popupMenu_Popup);
            // 
            // m_timerTooltip
            // 
            this.m_timerTooltip.Interval = 500;
            this.m_timerTooltip.Tick += new System.EventHandler(this.m_timerTooltip_Tick);
            this.ResumeLayout(false);

		}

		public static void DI( string strout )			// debug write
		{
#if false
			//System.IO.StreamWriter sw = new System.IO.StreamWriter( "e:\\debug.txt", true );
			//sw.WriteLine( strout );
			//sw.Close();
#else
			//Debug.WriteLine( strout );
#endif
		}

		#endregion

		#region Header

		#region Events and Delegates

		#region Clicked Events

		public delegate void ClickedEventHandler( object source, ClickEventArgs e );//int nItem, int nSubItem );
		public event ClickedEventHandler ColumnClickedEvent;
		public delegate void DragItemEventHandler(object sender, object itemDrag);
		public event DragItemEventHandler OnBeginDragItem;

		#endregion

		#region Changed Events

		public delegate void ChangedEventHandler( object source, ChangedEventArgs e );				//int nItem, int nSubItem );

		public event ChangedEventHandler ItemChangedEvent;
		public event ChangedEventHandler ColumnChangedEvent;

		#endregion
		
		#endregion

		#region VarsDefsProps

		#region Definitions


		public enum WIN32Codes
		{
			WM_GETDLGCODE = 0x0087,
			WM_SETREDRAW = 0x000B,
			WM_CANCELMODE = 0x001F,
			WM_NOTIFY = 0x4e,
			WM_KEYDOWN = 0x100,
			WM_KEYUP = 0x101,
			WM_CHAR = 0x0102,
			WM_SYSKEYDOWN = 0x104,
			WM_SYSKEYUP = 0x105,
			WM_COMMAND = 0x111,
			WM_MENUCHAR = 0x120,
			WM_MOUSEMOVE = 0x200,
			WM_LBUTTONDOWN = 0x201,
			WM_MOUSELAST = 0x20a,
			WM_USER = 0x0400,
			WM_REFLECT = WM_USER + 0x1c00
		}

		public enum DialogCodes
		{
			DLGC_WANTARROWS =     0x0001,
			DLGC_WANTTAB =        0x0002,
			DLGC_WANTALLKEYS =    0x0004,
			DLGC_WANTMESSAGE =    0x0004,
			DLGC_HASSETSEL =      0x0008,
			DLGC_DEFPUSHBUTTON =  0x0010,
			DLGC_UNDEFPUSHBUTTON = 0x0020,
			DLGC_RADIOBUTTON =    0x0040,
			DLGC_WANTCHARS =      0x0080,
			DLGC_STATIC =         0x0100,
			DLGC_BUTTON =         0x2000,
		}

		protected const int WM_KEYDOWN = 0x0100;
		protected const int VK_LEFT = 0x0025;
		protected const int VK_UP = 0x0026;
		protected const int VK_RIGHT = 0x0027;
		protected const int VK_DOWN = 0x0028;


		public enum ListStates { stateNone=0, stateSelecting=1, stateColumnSelect=2, stateColumnResizing=3, stateColumnMoving=4 }

		const int					RESIZE_ARROW_PADDING = 2;
		const int					MINIMUM_COLUMN_SIZE = 4;

		#endregion

		#region Class Variables

		private int					m_nLastSelectionIndex = 0;
		//private int					m_nLastSubSelectionIndex = 0;

		private bool				m_bCanChangeActivationCheckBoxes = false;

		private ListStates			m_nState = ListStates.stateNone;
		private Point				m_pointColumnResizeAnchor;
		private int					m_nResizeColumnNumber;			// the column number thats being resized

		private ArrayList			LiveControls = new ArrayList();		// list of controls currently visible.  THIS IS AN OPTIMIZATION.  This will keep us from having to iterate the entire list beforehand.
		private ArrayList			NewLiveControls = new ArrayList();
		private System.ComponentModel.IContainer components;

		private sc2i.win32.common.ManagedVScrollBar vPanelScrollBar;
		private sc2i.win32.common.ManagedHScrollBar hPanelScrollBar;


		private BorderStrip vertLeftBorderStrip;
		private BorderStrip vertRightBorderStrip;
		private BorderStrip horiBottomBorderStrip;
		private BorderStrip horiTopBorderStrip;
		private BorderStrip cornerBox;

		private bool m_bEnableCustomisation = true;


		#endregion

		#region ClassProperties

		private GLColumnCollection				m_Columns;
		
		//Liste d'objets données à afficher
		private IList							m_listeSource = null;

		protected CInfoStructureDynamique m_structureObjets = null;
		
		
		private Hashtable						m_tableSelection = new Hashtable();

		private Hashtable						m_tableChecked = new Hashtable();

		// border
		private bool							m_bShowBorder = true;

		private GLGridStyles					m_GridLineStyle = GLGridStyles.gridSolid;
		private int								m_nItemHeight = 18;
		private int								m_nHeaderHeight = 22;
		private int								m_nBorderWidth = 2;
		private Color							m_colorGridColor = SystemColors.ControlLight;
		private bool							m_bMultiSelect = false;
		private Color							m_colorSelectionColor = Color.DarkBlue;
		private bool							m_bHeaderVisible = true;
		private ImageList						m_ImageList = null;								// if it doesnt exist, then don't make it yet.

		private Color							m_SelectedTextColor = Color.White;

		private int								m_nMaxHeight = 0;
		private bool							m_bAutoHeight = true;
		private bool							m_bAllowColumnResize = true;
		private bool							m_bFullRowSelect = true;
		private bool							m_bAutoSort = true;

		private int								m_nLastResizeColumn = -1;

		private int								m_nFocusedIndex;
		private bool							m_bShowFocusRect = true;

		private bool							m_bHotTracking = false;
		private int								m_nHotColumnIndex = -1;							// internal hot column
		private int								m_nHotItemIndex = -1;							// internal hot item index
		private Color							m_HotTrackingColor = Color.LightGray;			// brush color to use

		private bool							m_bUpdating = false;
        private bool m_bHasImages = false;



		private bool							m_bAlternatingColors = false;
		private Color							m_colorAlternateBackground = Color.DarkGreen;
		private Color							m_colorSuperFlatHeaderColor = Color.White;
		private Color							m_colorHeaderText = Color.Black;
        private Font                            m_fontHeaderText = new Font("Microsoft Sans Serif", 8);

		private GLHeaderStyles					m_HeaderStyle = GLHeaderStyles.Normal;

		private bool							m_bItemWordWrap = false;
        private System.Windows.Forms.ContextMenu m_popupMenu = null;
		private bool							m_bHeaderWordWrap = false;

		private Image							m_imageUncheck = null;
        private Timer m_timerTooltip;
		private Image							m_imageCheck =  null;

        public event GlacialListGetImageEventHandler OnGetImage;

		#region Control Properties



		/// <summary>
		/// Word wrap in header
		/// </summary>
		[
		Description("Word wrap in header"),
		Category("Header"),
		Browsable(true)
		]
		public bool HeaderWordWrap
		{
			get	{ return m_bHeaderWordWrap; }
			set { m_bHeaderWordWrap = value; }
		}


		/// <summary>
		/// Word wrap in cells
		/// </summary>
		[
		Description("Word wrap in cells"),
		Category("Item"),
		Browsable(true)
		]
		public bool ItemWordWrap
		{
			get	{ return m_bItemWordWrap; }
			set { m_bItemWordWrap = value; }
		}



		/// <summary>
		/// background color to use if flat
		/// </summary>
		[
		Description("Color for text in boxes that are selected."),
		Category("Header"),
		Browsable(true)
		]
		public Color SuperFlatHeaderColor
		{
			get	{ return m_colorSuperFlatHeaderColor; }
			set { m_colorSuperFlatHeaderColor = value; }
		}

		/// <summary>
		/// Couleur du texte header
		/// </summary>
		[
		Description("Couleur du texte header"),
		Category("Header"),
		Browsable(true)
		]
		public Color HeaderTextColor
		{
			get	{ return m_colorHeaderText; }
			set { m_colorHeaderText = value; }
		}

        /// <summary>
        /// Couleur du texte header
        /// </summary>
        [
        Description("Police du texte header"),
        Category("Header"),
        Browsable(true)
        ]
        public Font HeaderTextFont
        {
            get { return m_fontHeaderText; }
            set { m_fontHeaderText = value; }
        }


		/// <summary>
		/// Allows for more custom header styles
		/// </summary>
		[
		Description("Allows for more custom header styles"),
		Category("Header"),
		Browsable(true)
		]
		public GLHeaderStyles HeaderStyle
		{
			get	{ return m_HeaderStyle; }
			set
			{
				m_HeaderStyle = value;
				DI("Calling Invalidate from HeaderStyle Property");
				Invalidate();
			}
		}


		/// <summary>
		/// Alternating Colors on or off
		/// </summary>
		[
		Description("turn xp themes on or not"),
		Category("Item Alternating Colors"),
		Browsable(true)
		]
		public bool AlternatingColors
		{
			get	{ return m_bAlternatingColors; }
			set { m_bAlternatingColors = value; }
		}


		/// <summary>
		/// second background color if we use alternating colors
		/// </summary>
		[
		Description("Color for text in boxes that are selected."),
		Category("Item Alternating Colors"),
		Browsable(true)
		]
		public Color AlternateBackground
		{
			get	{ return m_colorAlternateBackground; }
			set { m_colorAlternateBackground = value; }
		}


		/// <summary>
		/// Whether or not to show a border.
		/// </summary>
		[
		Description("Whether or not to show a border."),
		Category("Appearance"),
		Browsable(true),
		]
		public bool ShowBorder
		{
			get	{ return m_bShowBorder; }
			set { m_bShowBorder = value; }
		}


		/// <summary>
		/// Color for text in boxes that are selected
		/// </summary>
		[
		Description("Color for text in boxes that are selected."),
		Category("Item"),
		Browsable(true)
		]
		public Color SelectedTextColor
		{
			get	{ return m_SelectedTextColor; }
			set { m_SelectedTextColor = value; }
		}


		/// <summary>
		/// hot tracking
		/// </summary>
		[
		Description("Color for hot tracking."),
		Category("Appearance"),
		Browsable(true)
		]
		public Color HotTrackingColor
		{
			get	{ return m_HotTrackingColor; }
			set { m_HotTrackingColor = value; }
		}


		/// <summary>
		/// Hot Tracking of columns and items
		/// </summary>
		[
		Description("Show hot tracking."),
		Category("Behavior"),
		Browsable(true)
		]
		public bool HotTracking
		{
			get	{ return m_bHotTracking; }
			set { m_bHotTracking = value; }
		}


		/// <summary>
		/// Show the focus rect or not
		/// </summary>
		[
		Description("Show Focus Rect on items."),
		Category("Item"),
		Browsable(true)
		]
		public bool ShowFocusRect
		{
			get	{ return m_bShowFocusRect; }
			set { m_bShowFocusRect = value; }
		}


		/// <summary>
		/// auto sorting
		/// </summary>
		[
		Description("Autosort items as they are added."),
		Category("Item"),
		Browsable(true),
		]
		public bool AutoSort
		{
			get	{ return m_bAutoSort; }
			set { m_bAutoSort = value; }
		}


		/// <summary>
		/// 
		/// </summary>
		[
		Description("ImageList to be used in listview."),
		Category("Behavior"),
		Browsable(true),
		]
		public ImageList ImageList
		{
			get	{ return m_ImageList; }
			set { m_ImageList = value; }
		}


		/// <summary>
		/// Allow columns to be resized
		/// </summary>
		[
		Description("Allow resizing of columns"),
		Category("Header"),
		Browsable(true)
		]
		public bool AllowColumnResize
		{
			get { return m_bAllowColumnResize; }
			set { m_bAllowColumnResize = value; }
		}


		/// <summary>
		/// Control resizes height of row based on size.
		/// </summary>
		[
		Description("Do we want rows to automatically adjust height"),
		Category("Item"),
		Browsable(true)
		]
		public bool AutoHeight
		{
			get { return m_bAutoHeight; }
			set { m_bAutoHeight = value; }
		}


		/// <summary>
		/// you want the header to be visible or not
		/// </summary>
		[
		Description("Column Headers Visible"),
		Category("Header"),
		Browsable(true)
		]
		public bool HeaderVisible
		{
			get { return m_bHeaderVisible; }
			set { m_bHeaderVisible = value; }
		}

		public bool ShouldSerializeColumns()
		{
			return true;
		}


		/// <summary>
		/// Collection of columns
		/// </summary>
		[
		Category("Header"),
		Description("Column Collection"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor)),
		Browsable(true)
		]
		public GLColumnCollection Columns
		{
			get	{ return m_Columns; }
		}


		/*[
		Category("Behavior"),
		Description("Items collection"),
		Browsable(false)
		]
		public GLItemCollection Items
		{
			get	{ return m_Items; }
		}*/

		[Category("Behavior"),
		Description("Source de données"),
		Browsable(false)
		]
		public IList ListeSource
		{
			get	
            {
                return m_listeSource; 
            }
			set
            {
                // Vide la cache quand la source de données change
                m_tabDonneesDynamiqueStringEnCache.Clear();
                m_listeSource = value;
            }
		}



		/// <summary>
		/// selection bar color
		/// </summary>
		[
		Description("Background color to mark selection."),
		Category("Item"),
		Browsable(true),
		]
		public Color SelectionColor
		{
			get	{ return m_colorSelectionColor; }
			set { m_colorSelectionColor = value; }
		}


		[
		Description("Allow full row select."),
		Category("Item"),
		Browsable(true)
		]
		public bool FullRowSelect
		{
			get	{ return m_bFullRowSelect; }
			set	{ m_bFullRowSelect = value; }
		}


		[
		Description("Allow multiple selections."),
		Category("Item"),
		Browsable(true)
		]
		public bool AllowMultiselect
		{
			get	{ return m_bMultiSelect; }
			set	{ m_bMultiSelect = value; }
		}


		[
		Description("Border Padding"),
		Category("Appearance"),
		Browsable(false),
		DefaultValue(2)
		]
		public int BorderPadding
		{
			get	
			{ 
				if ( ShowBorder )
					return 2; 
				else
					return 0;
			}
			set	{ m_nBorderWidth = value; }
		}


		[
		Description("Whether or not to draw gridlines"),
		Category("Grid"),
		Browsable(true)
		]
		public GLGridStyles GLGridLines
		{
			get	{ return m_GridLineStyle; }
			set
			{
				m_GridLineStyle = value;
				DI("Calling Invalidate From GLGridStyles");
				Invalidate();
			}
		}


		[
		Description("Color of the grid if we draw it."),
		Category("Grid"),
		Browsable(true)
		]
		public Color GridColor
		{
			get	{ return m_colorGridColor; }
			set
			{
				m_colorGridColor = (Color)value;
				DI("Calling Invalidate From GridColor");
				Invalidate();
			}
		}


		/// <summary>
		/// how big do we want the individual items to be
		/// </summary>
		[
		Description("How high each row is."),
		Category("Item"),
		Browsable(true)
		]
		public int ItemHeight
		{
			get { return m_nItemHeight;	}
			set
			{
				//Debug.WriteLine( "Setting item height to " + value.ToString() );

				//if ( value == 15 )
				//Debug.WriteLine( "stop" );

				m_nItemHeight = value;
				DI("Calling Invalidate From ItemHeight");
				Invalidate();
			}
		}


		[
		Description("How high the columns are."),
		Category("Header"),
		Browsable(true)
		]
		public int HeaderHeight
		{
			get
			{
				if ( HeaderVisible == true )
					return m_nHeaderHeight;
				else
					return 0;
			}
			set
			{
				m_nHeaderHeight = value;
				DI("Calling Invalidate From HeaderHeight");
				Invalidate();
			}
		}


		/// <summary>
		/// amount of space inside any given cell to borders
		/// </summary>
		[
		Description("Cell padding area"),
		Browsable(false)
		]
		public int CellPaddingSize
		{
			get	{ return 2; }			// default I set to 4
		}


		#endregion

		#region Working Properties

		private int m_nSortIndex = 0;



		/// <summary>
		/// returns a list of only the selected items
		/// </summary>
		[
		Description("Selected Items Array"),
		Browsable(false)
		]
		public ArrayList SelectedItems
		{
			get 
			{ 
				ArrayList lst = new ArrayList();
				if ( ListeSource != null )
				{
					foreach ( int nVal in m_tableSelection.Keys )
					{
						if ( nVal >= 0 && nVal < ListeSource.Count )
							lst.Add ( ListeSource[nVal] );
					}
				}
				return lst;
			}
		}

		public void ClearSelection()
		{
			m_tableSelection.Clear();
		}
		/// <summary>
		/// Indique que l'utilisateur peut choisir d'activer ou de désactier les check boxs
		/// </summary>
		public bool CanChangeActivationCheckBoxes
		{
			get
			{
				return m_bCanChangeActivationCheckBoxes;
			}
			set
			{
				m_bCanChangeActivationCheckBoxes = value;
			}
		}

			/// <summary>
			/// returns a list of only the selected items
			/// </summary>
			[
			Description("Checked Items Array"),
			Browsable(false)
			]
			public ArrayList CheckedItems
		{
			get 
			{ 
				ArrayList lst = new ArrayList();
				if ( ListeSource != null )
				{
					foreach ( int nVal in m_tableChecked.Keys )
					{
						if ( nVal >= 0 && nVal < ListeSource.Count )
							lst.Add ( ListeSource[nVal] );
					}
				}
				return lst;
			}
            set
            {
                m_tableChecked.Clear();
                if ( value != null && ListeSource != null)
                    foreach (object obj in value)
                    {
                        int nIndex = ListeSource.IndexOf(obj);
                        if (nIndex >= 0)
                            m_tableChecked[nIndex] = true;
                    }
            }
		}


		/// <summary>
		/// Selectionne un element ou l'ajoute à la selection si la selection multiple est activée
		/// </summary>
		/// <param name="nIndex"></param>
		public void SelectItem ( int nIndex )
		{
			if ( !AllowMultiselect )
				m_tableSelection.Clear();
			m_tableSelection[nIndex] = true;
			Invalidate();
		}

		public event GlacialListCheckedChangeEventHandler CheckedChange;

		public void CheckItem ( int nIndex )
		{
			m_tableChecked[nIndex] = true;
			if ( CheckedChange != null )
				CheckedChange ( this, nIndex );
		}

		public void ResetCheck()
		{
			m_tableChecked = new Hashtable();
			Invalidate();
		}


		public void UnCheckItem ( int nIndex )
		{
			m_tableChecked.Remove ( nIndex );
			if ( CheckedChange != null )
				CheckedChange ( this, nIndex );
		}

		public bool IsChecked ( int nIndex )
		{
			return m_tableChecked[nIndex] != null;
		}

		/// <summary>
		/// returns a list of only the selected items indexes
		/// </summary>
		/*[
		Description("Selected Items Array Of Indicies"),
		Browsable(false)
		]
		public ArrayList SelectedIndicies
		{
			get { return this.Items.SelectedIndicies; }
		}*/


		/// <summary>
		/// Current index we are sorting against
		/// </summary>
		[
		Description("Current index we are sorting against"),
		Browsable(false)
		]
		public int SortIndex
		{
			get 
			{
				return m_nSortIndex;
			}
			set 
			{
				m_nSortIndex = value;
			}
		}


		/// <summary>
		/// currently focused item
		/// </summary>
		[
		Description("Currently Focused Column"),
		Browsable(false)
		]
		public int HotColumnIndex
		{
			get 
			{
				return m_nHotColumnIndex;
			}
			set 
			{
                // YK 14/01/2009 : désactive le tracking sur les colonnes
                //if (m_bHotTracking)
                    //if (m_nHotColumnIndex != value)
                    //{
                    //    m_nHotItemIndex = -1;
                    //    m_nHotColumnIndex = value;

                    //    DI("Calling Invalidate From HotColumnIndex");
                    //    Invalidate();
                    //}
			}
		}


		/// <summary>
		/// currently focused item
		/// </summary>
		[
		Description("Currently Focused Item"),
		Browsable(false)
		]
		public int HotItemIndex
		{
			get 
			{
				return m_nHotItemIndex;
			}
			set 
			{
				if ( m_bHotTracking )
					if ( m_nHotItemIndex != value )
					{
						m_nHotColumnIndex = -1;
						m_nHotItemIndex = value; 

						DI("Calling Invalidate From HotItemIndex");
						Invalidate();
					}
			}
		}


		/// <summary>
		/// currently focused item
		/// </summary>
		[
		Description("Currently Focused Item"),
		Browsable(false)
		]
		public object FocusedItem
		{
			get 
			{
				if ( !m_bShowFocusRect )				// if they elect not to see the focus rect, then dont show it
					return null;

				if ( ListeSource == null || m_nFocusedIndex < 0 || m_nFocusedIndex > ListeSource.Count-1 )
					return null;

				return ListeSource[m_nFocusedIndex]; 
			}
			set 
			{
				if ( FocusedItem != value )
				{
					if ( ListeSource == null )
						return;
					m_nFocusedIndex = -1;
					if ( value != null )
					{
						for ( int nObj = 0; nObj  < ListeSource.Count; nObj++ )
						{
							if ( value.Equals ( ListeSource[nObj] ) )
							{
								m_nFocusedIndex = nObj;
								break;
							}
						}
					}
					DI("Calling Invalidate From FocusedItem");
					Invalidate();
				}
			}
		}



		[
		Description("Number of items/rows in the list."),
		Category("Behavior"),
		Browsable(false),
		DefaultValue(0)
		]
		public int Count
		{
			get 
			{ 
				if ( ListeSource  == null )
					return 0;
				return ListeSource.Count; 
			}
		}


		[
		Description("All items together height."),
		Browsable(false)
		]
		public int TotalRowHeight
		{
			get
			{
				return ItemHeight * Count;
			}
		}


		[
		Description("Number of rows currently visible in inner rect."),
		Browsable(false)
		]
		public int VisibleRowsCount
		{
			get	{ return RowsInnerClientRect.Height / ItemHeight; }
		}


		[
		Description("this will always reflect the most height any item line has needed"),
		Browsable(false)
		]
		public int MaxHeight
		{
			get	{ return m_nMaxHeight; }
			set
			{
				if ( value > m_nMaxHeight )
				{
					m_nMaxHeight = value;
					if ( AutoHeight == true )
					{
						ItemHeight = MaxHeight;
						DI("Calling Invalidate From MaxHeight");
						Invalidate();
						DW("Item height set bigger");
					}
				}
			}
		}


		[
		Description("The rectangle of the header inside parent control"),
		Browsable(false)
		]
		public Rectangle HeaderRect
		{
			get	{ 
				return new Rectangle( this.BorderPadding, this.BorderPadding, Width-(this.BorderPadding*2), HeaderHeight ); 
			}
		}


		[
		Description("The rectangle of the client inside parent control"),
		Browsable(false)
		]
		public Rectangle RowsClientRect
		{
			get
			{
				int tmpY = HeaderHeight + BorderPadding;							// size of the header and the top border

				int tmpHeight = Height - HeaderHeight - (BorderPadding*2);

#if false		// it really shouldnt be the business of this routine to deal with only a resize problem
				if ( (tmpHeight % ItemHeight) != 0 )
				{	// the size must be adjusted for the control, this is not the right size
					int nRows = tmpHeight / ItemHeight;
					int controlHeight = (nRows * ItemHeight) + tmpY + (BorderPadding*2);

					this.SetBounds( 0, 0, Width, controlHeight );
					Invalidate(true);

					return RowsClientRect;
				}
#endif
				return new Rectangle( BorderPadding, tmpY, Width-(this.BorderPadding*2), tmpHeight );
			}
		}


		[
		Description("The inner rectangle of the client inside parent control taking scroll bars into account."),
		Browsable(false)
		]
		public Rectangle RowsRect
		{
			get
			{
				Rectangle rect = new Rectangle();

				rect.X = -this.hPanelScrollBar.Value;
				rect.Y = HeaderHeight + BorderPadding;
				rect.Width = Columns.Width;
				rect.Height = this.VisibleRowsCount * ItemHeight;

				return rect;
			}
		}


		[
		Description("The inner rectangle of the client inside parent control taking scroll bars into account."),
		Browsable(false)
		]
		public Rectangle RowsInnerClientRect
		{
			get
			{
				Rectangle innerRect = RowsClientRect;

				innerRect.Width -= vPanelScrollBar.mWidth;				// horizontal bar crosses vertical plane and vice versa
				innerRect.Height -= hPanelScrollBar.mHeight;

				if ( innerRect.Width < 0 )
					innerRect.Width = 0;
				if ( innerRect.Height < 0 )
					innerRect.Height= 0;

				return innerRect;
			}
		}


		#endregion

		#endregion

		#endregion

		#endregion

		#region Implementation

		#region Initialization

		public GlacialList()
		{
			DW("Constructor");

            InitializeComponent();
			//components = new System.ComponentModel.Container();

			this.TabStop = true;

			m_Columns = new GLColumnCollection();
			m_Columns.ChangedEvent += new GLColumnCollection.ChangedEventHandler( Columns_Changed );				// listen to event changes inside the item

			//m_Items = new GLItemCollection( this );
			//m_Items.ChangedEvent += new GLItemCollection.ChangedEventHandler( Items_Changed );

			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.Opaque |
				ControlStyles.UserPaint | 
				ControlStyles.DoubleBuffer |
				ControlStyles.Selectable | 
				ControlStyles.UserMouse, 
				true
				);

			this.BackColor = SystemColors.ControlLightLight;

			this.hPanelScrollBar = new sc2i.win32.common.ManagedHScrollBar();
			this.vPanelScrollBar = new sc2i.win32.common.ManagedVScrollBar();


			//
			// Creating borders
			//

			//Debug.WriteLine( "Creating borders" );
			this.vertLeftBorderStrip = new BorderStrip();
			this.vertRightBorderStrip = new BorderStrip();
			this.horiBottomBorderStrip = new BorderStrip();
			this.horiTopBorderStrip = new BorderStrip();
			this.cornerBox = new BorderStrip();



			this.SuspendLayout();
			// 
			// hPanelScrollBar
			// 
			this.hPanelScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.hPanelScrollBar.CausesValidation = false;
			this.hPanelScrollBar.Location = new System.Drawing.Point(24, 0);
			this.hPanelScrollBar.mHeight = 16;
			this.hPanelScrollBar.mWidth = 120;
			this.hPanelScrollBar.Name = "hPanelScrollBar";
			this.hPanelScrollBar.Size = new System.Drawing.Size(120, 16);
			this.hPanelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hPanelScrollBar_Scroll);
			this.hPanelScrollBar.Parent = this;
			this.Controls.Add( hPanelScrollBar );

			// 
			// vPanelScrollBar
			// 
			this.vPanelScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.vPanelScrollBar.CausesValidation = false;
			this.vPanelScrollBar.Location = new System.Drawing.Point(0, 12);
			this.vPanelScrollBar.mHeight = 120;
			this.vPanelScrollBar.mWidth = 16;
			this.vPanelScrollBar.Name = "vPanelScrollBar";
			this.vPanelScrollBar.Size = new System.Drawing.Size(16, 120);
			this.vPanelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vPanelScrollBar_Scroll);
			this.vPanelScrollBar.Parent = this;
			this.Controls.Add( vPanelScrollBar );


			this.horiTopBorderStrip.Parent = this;
			this.horiTopBorderStrip.BorderType = BorderStrip.BorderTypes.btTop;
			this.horiTopBorderStrip.Visible = true;
			this.horiTopBorderStrip.BringToFront();


			//this.horiBottomBorderStrip.BackColor=Color.Black;
			this.horiBottomBorderStrip.Parent = this;
			this.horiBottomBorderStrip.BorderType = BorderStrip.BorderTypes.btBottom;
			this.horiBottomBorderStrip.Visible = true;
			this.horiBottomBorderStrip.BringToFront();

			//this.vertLeftBorderStrip.BackColor=Color.Black;
			this.vertLeftBorderStrip.BorderType = BorderStrip.BorderTypes.btLeft;
			this.vertLeftBorderStrip.Parent = this;
			this.vertLeftBorderStrip.Visible = true;
			this.vertLeftBorderStrip.BringToFront();

			//this.vertRightBorderStrip.BackColor=Color.Black;
			this.vertRightBorderStrip.BorderType = BorderStrip.BorderTypes.btRight;
			this.vertRightBorderStrip.Parent = this;
			this.vertRightBorderStrip.Visible = true;
			this.vertRightBorderStrip.BringToFront();

			this.cornerBox.BackColor = SystemColors.Control;
			this.cornerBox.BorderType = BorderStrip.BorderTypes.btSquare;
			this.cornerBox.Visible = false;
			this.cornerBox.Parent = this;
			this.cornerBox.BringToFront();

			this.Name = "GlacialList";

			this.ResumeLayout(false);

			

			/*this.ContextMenu = new ContextMenu();
			ContextMenu.Popup += new EventHandler(ContexMenuPopUp);*/
		}

		private void LoadImages()
		{
			if ( m_imageCheck == null )
			{
				m_imageUncheck = new Bitmap ( typeof(sc2i.win32.common.GlacList.CPourImagesCheck), "Win32Uncheck.bmp" );
				m_imageCheck = new Bitmap ( typeof(sc2i.win32.common.GlacList.CPourImagesCheck), "Win32Check.bmp" );
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
            if (m_toolTip != null)
                m_toolTip.Dispose();
			base.Dispose( disposing );
		}

		#endregion

		#region System Overrides

		/// <summary>
		/// stop keys from going to scrollbars
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message msg)
		{
			
			try
			{
				base.WndProc(ref msg);
				if ( msg.Msg == (int)WIN32Codes.WM_GETDLGCODE )
				{
					msg.Result = new IntPtr((int)DialogCodes.DLGC_WANTCHARS | (int)DialogCodes.DLGC_WANTARROWS | msg.Result.ToInt32());
				}
				if ( msg.Msg == (int)WindowsMessages.WM_MOUSEWHEEL )
				{
					int nMove = ((int)msg.WParam)>>16;
					if ( nMove > 0 )
						vPanelScrollBar.Value -= 3;
					else
						vPanelScrollBar.Value += 3;
					Refresh();
				}
			}
			catch (Exception e )
			{
				Console.WriteLine("Exception Glacial list : "+e.ToString());
			}
		}


		/// <summary>
		/// keep certain keys here
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override bool PreProcessMessage(ref Message msg)
		{
			if (msg.Msg == WM_KEYDOWN)
			{
				Keys keyData = ((Keys) (int) msg.WParam) | ModifierKeys;
				Keys keyCode = ((Keys) (int) msg.WParam);

				if ( keyData == Keys.Escape )
				{
					FocusedItem = null;
				}
				else if ( keyData == Keys.Space && CheckBoxes && m_nFocusedIndex >= 0 && m_nFocusedIndex < ListeSource.Count)
				{
					if ( m_tableChecked[m_nFocusedIndex] == null )
						m_tableChecked[m_nFocusedIndex] = true;
					else
						m_tableChecked.Remove(m_nFocusedIndex);
					if (CheckedChange != null)
						CheckedChange(this, m_nFocusedIndex);
					Invalidate();
				}
				else if ( ( FocusedItem != null ) && ( Count > 0 ) )
				{
					int nItemIndex = m_nFocusedIndex;//;Items.FindItemIndex( FocusedItem );
					int nOldIndex = nItemIndex;
					if ( nItemIndex < 0 )
						return true;							// this can't move

					if (keyData == Keys.Down)
					{
						nItemIndex++;
					}
					else if (keyData == Keys.Up)
					{
						nItemIndex--;
					}
					else if ( keyData == Keys.PageDown )
					{
						nItemIndex+=this.VisibleRowsCount;
					}
					else if ( keyData == Keys.PageUp )
					{
						nItemIndex-=this.VisibleRowsCount;
					}

					// bounds check them
					if ( nItemIndex < 0 )
						nItemIndex = 0;
					if ( nItemIndex > Count-1 )
						nItemIndex = Count-1;

					// move view
					if ( nOldIndex != nItemIndex )
					{
						if (nItemIndex < this.vPanelScrollBar.Value)			// its out of viewable, move the surface
							this.vPanelScrollBar.Value = nItemIndex;
						if ( nItemIndex > ( this.vPanelScrollBar.Value+this.VisibleRowsCount ))
							this.vPanelScrollBar.Value = nItemIndex - this.VisibleRowsCount;

						if ( nItemIndex != m_nFocusedIndex )
						{
							m_nFocusedIndex = nItemIndex;
							Invalidate();
						}
					}
				}
				else
				{	// else we just move the actual list
					if ( this.vPanelScrollBar.Visible )
					{	// only bother if we at least have a scrollbar
						int nCVP = this.vPanelScrollBar.Value;

						if (keyData == Keys.Down)
						{
							if ( ( nCVP + this.VisibleRowsCount ) < Count )
								this.vPanelScrollBar.Value+=1;
						}
						else if (keyData == Keys.Up)
						{
							if ( nCVP > 0 )
								this.vPanelScrollBar.Value-=1;
						}
						else if ( keyData == Keys.PageDown )
						{
							if ( ( nCVP + this.VisibleRowsCount + this.VisibleRowsCount ) < Count )
								this.vPanelScrollBar.Value = nCVP + this.VisibleRowsCount;
							else
								this.vPanelScrollBar.Value = Count - this.VisibleRowsCount;
						}
						else if ( keyData == Keys.PageUp )
						{
							if ( ( nCVP - this.VisibleRowsCount ) > -1 )
								this.vPanelScrollBar.Value = nCVP - this.VisibleRowsCount;
							else
								this.vPanelScrollBar.Value = 0;
						}

						if ( nCVP != this.vPanelScrollBar.Value )
							Invalidate();
					}
				}
			}

			return base.PreProcessMessage(ref msg);
		}
		
		#endregion

		#region Event Handlers

		protected void Items_Changed( object source, ChangedEventArgs e )
		{
			DW("GlacialList::Items_Changed");

			if ( ItemChangedEvent != null )
				ItemChangedEvent( this, e );				// fire the column clicked event

			DI("Calling Invalidate From Items_Changed");
			Invalidate();
		}


		public void Columns_Changed( object source, ChangedEventArgs e )
		{
			DW("Columns_Changed");

			if ( ColumnChangedEvent != null )
				ColumnChangedEvent( this, e );				// fire the column clicked event

			DI("Calling Invalidate From Columns_Changed");
			Invalidate();
		}

		#endregion

		#region HelperFunctions


		/// <summary>
		/// Tell paint to stop worry about updates
		/// </summary>
		public void BeginUpdate()
		{
			m_bUpdating = true;
		}

		/// <summary>
		/// Tell paint to start worrying about updates again and repaint while your at it
		/// </summary>
		public void EndUpdate()
		{
			m_bUpdating = false;
			Invalidate();
		}

		public object GetElementAtPoint ( Point pt )
		{
			pt = this.PointToScreen ( pt );
			int nItem = -1;
			int nColumn = -1;
			ListStates eState;
			InterpretCoords ( pt.X, pt.Y, out nItem, out nColumn,out eState );
			
			if ( eState == ListStates.stateSelecting )
			{
				try
				{
					return m_listeSource[nItem];
				}
				catch
				{
				}
			}
			return null;
		}


		/// <summary>
		/// interpret mouse coordinates
		/// </summary>
		/// <param name="nScreenX"></param>
		/// <param name="nScreenY"></param>
		/// <param name="nItem"></param>
		/// <param name="nColumn"></param>
		/// <param name="nState"></param>
		protected void InterpretCoords( int nScreenX, int nScreenY, out int nItem, out int nColumn, out ListStates nState )
		{
			DW("Interpret Coords");

			nState = ListStates.stateNone;
			nColumn = 0;		// compiler forces me to set this since it sometimes wont get set if routine falls through early
			nItem = 0;

			/*
			 * Calculate horizontal subitem
			 */
			int nCurrentX = -hPanelScrollBar.Value;		//GetHScrollPoint();			// offset the starting point by the current scroll point
            if (HasImages)
                nCurrentX += ItemHeight;
			foreach ( GLColumn col in Columns )
			{
				if ( (nScreenX > nCurrentX) && (nScreenX < (nCurrentX+col.Width-RESIZE_ARROW_PADDING)) )
				{
					nState = ListStates.stateColumnSelect;

					break;
				}
				if ( (nScreenX > (nCurrentX+col.Width-RESIZE_ARROW_PADDING)) && (nScreenX < (nCurrentX+col.Width+RESIZE_ARROW_PADDING)) )
				{
					if ( AllowColumnResize == true )
						nState = ListStates.stateColumnResizing;

					return;				// no need for this to fall through
				}

				nColumn++;
				nCurrentX += col.Width;
			}

			if ( ( nScreenY >= RowsInnerClientRect.Y ) && ( nScreenY < RowsInnerClientRect.Bottom ) )
			{	// we are in the client area
				Columns.ClearHotStates();

				nItem = ((nScreenY - RowsInnerClientRect.Y) / ItemHeight) + vPanelScrollBar.Value;

				this.HotItemIndex = nItem;

				if ( nItem >= Count )
					nState = ListStates.stateNone;
				else
				{
					nState = ListStates.stateSelecting;

					//TODO : A quoi ça sert ????
					/*// handle case of where FullRowSelect is OFF and we click on the second part of a spanned column
					for ( int nSubIndex = 0; nSubIndex < Columns.Count; nSubIndex++ )
					{
						if ( ( nSubIndex + (Items[nItem].SubItems[nSubIndex].Span-1) ) >= nColumn )
						{
							nColumn = nSubIndex;
							return;
						}
					}*/

				}

				return;
			}
			else
			{
				if ( ( nScreenY >= this.HeaderRect.Y ) && ( nScreenY < this.HeaderRect.Bottom ) )
				{
					this.HotColumnIndex = nColumn;

					if ( ( ( nColumn > -1 ) && ( nColumn < Columns.Count ) ) && (!Columns.AnyPressed() ) )
						if ( Columns[nColumn].State == ColumnStates.csNone)
						{
							Columns.ClearHotStates();
							Columns[nColumn].State = ColumnStates.csHot;
						}
				}
			}


			return;
		}

		/// <summary>
		/// return the X starting point of a particular column
		/// </summary>
		/// <param name="nColumn"></param>
		/// <returns></returns>
		public int GetColumnScreenX( int nColumn )
		{
			DW("Get Column Screen X");

			if ( nColumn >= Columns.Count )
				return 0;

			int nCurrentX = -hPanelScrollBar.Value;//GetHScrollPoint();			// offset the starting point by the current scroll point
			int nColIndex = 0;
			foreach ( GLColumn col in Columns )
			{
				if ( nColIndex >= nColumn )
					return nCurrentX;

				nColIndex++;
				nCurrentX += col.Width;
			}

			return 0;		// this should never happen;
		}


		/// <summary>
		/// Sort a column.
		/// 
		/// Set to virtual so you can write your own sorting
		/// </summary>
		/// <param name="nColumn"></param>
		public virtual void SortColumn( int nColumn )
		{

			if ( Count < 2 )			// nothing to sort
				return;

			this.SortIndex = nColumn;
			//ListeSource.Sort.Sort();
		}


		#endregion

		#region Dimensions

		protected override void OnResize(EventArgs e)
		{
			DW("GlacialList_Resize");

			//RecalcScroll();

			DI("Calling Invalidate From OnResize");
			Invalidate();
		}


		#endregion

		#region Drawing

		/// <summary>
		/// Paint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// 
		protected override void OnPaint(PaintEventArgs e)
		{
			DW("Paint");

			if ( m_bUpdating )		// my best guess on how to implement updating functionality				
				return;

			RecalcScroll();			// at some point I need to move this out of paint.  Doesn't really belong here.


			Graphics g = e.Graphics;



			if ( Columns.Count > 0 )
			{
				int nInsideWidth;
				if ( Columns.Width > HeaderRect.Width )
					nInsideWidth = Columns.Width;
				else
					nInsideWidth = HeaderRect.Width;

				/*
				 * draw header
				 */
				if ( HeaderVisible == true )
				{
					g.SetClip( HeaderRect );
					DrawHeader( g, new Size( HeaderRect.Width, HeaderRect.Height ) );
				}

				/*
				 * draw client area
				 */
				g.SetClip( RowsInnerClientRect );
				DrawRows( g );

				// very optimized way of removing controls that aren't visible anymore without having to iterate the entire items list
				foreach( Control control in LiveControls )
				{
					control.Visible = false;					// make sure the controls that aren't visible aren't shown
				}
				LiveControls = NewLiveControls;
				NewLiveControls = new ArrayList();
			}

			g.SetClip( this.ClientRectangle );


		}


		/// <summary>
		/// Draw Header Control
		/// </summary>
		/// <param name="graphicHeader"></param>
		/// <param name="sizeHeader"></param>
		public void DrawHeader( Graphics graphicHeader, /*Bitmap bmpHeader,*/ Size sizeHeader )
		{
			DW("DrawHeader");

			if ( this.HeaderStyle == GLHeaderStyles.SuperFlat )
			{
				SolidBrush brush = new SolidBrush( this.SuperFlatHeaderColor );
				graphicHeader.FillRectangle( brush, HeaderRect );
				brush.Dispose();
			}
			else
			{
				graphicHeader.FillRectangle( SystemBrushes.Control, HeaderRect );
			}


			if ( Columns.Count <= 0 )
				return;

			// draw vertical lines first, then horizontal lines
			int nCurrentX = (-this.hPanelScrollBar.Value) + HeaderRect.X;
            if (HasImages)
                nCurrentX += ItemHeight;
			foreach ( GLColumn column in Columns )
			{
				// cull columns that won't be drawn first
				if ( ( nCurrentX + column.Width ) < 0 )
				{
					nCurrentX += column.Width;
					continue;							// skip this column, its not being drawn
				}
				if ( nCurrentX > HeaderRect.Right )		
					return;								// were past the end of the visible column, stop drawing
				
				DrawColumnHeader( graphicHeader, new Rectangle( nCurrentX, HeaderRect.Y, column.Width, HeaderHeight ), column );

				nCurrentX += column.Width;				// move the parser
			}
		}


		/// <summary>
		/// Draw column in header control
		/// </summary>
		/// <param name="graphicsColumn"></param>
		/// <param name="rectColumn"></param>
		/// <param name="column"></param>
		public void DrawColumnHeader( Graphics graphicsColumn, Rectangle rectColumn, GLColumn column )
		{
			DW("DrawColumn");

			if ( this.HeaderStyle == GLHeaderStyles.SuperFlat )
			{
				SolidBrush brush = new SolidBrush( this.SuperFlatHeaderColor );
				graphicsColumn.FillRectangle( brush, rectColumn );
                graphicsColumn.DrawRectangle(new Pen(this.GridColor), rectColumn.X-2, rectColumn.Y, rectColumn.Width, rectColumn.Height);
				brush.Dispose();
			}
				/*else if (( this.HeaderStyle == GLHeaderStyles.XP )&& OPaC.uxTheme.Wrapper.IsAppThemed() )
				{	// this is really the only thing we care about for themeing right now inside the control
					System.IntPtr hDC = graphicsColumn.GetHdc();;
					//Debug.WriteLine("hci is " + this.HotColumnIndex.ToString() );
					Debug.WriteLine("CH rect " + rectColumn.ToString() );

					if ( column.State == ColumnStates.csNone )
					{
						Debug.WriteLine( "Normal" );
						OPaC.uxTheme.Wrapper.DrawBackground( "HEADER", "HEADERITEM", "NORMAL", hDC, rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom, 
							rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom );
					}
					else if ( column.State == ColumnStates.csPressed )
					{
						Debug.WriteLine( "Pressed" );
						OPaC.uxTheme.Wrapper.DrawBackground( "HEADER", "HEADERITEM", "PRESSED", hDC, rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom, 
							rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom );
					}
					else if ( column.State == ColumnStates.csHot )
					{
						Debug.WriteLine( "Hot" );
						OPaC.uxTheme.Wrapper.DrawBackground( "HEADER", "HEADERITEM", "HOT", hDC, rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom, 
							rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom );
					}

					graphicsColumn.ReleaseHdc(hDC);
				}*/
			else
			{
				if ( column.State != ColumnStates.csPressed )
					ControlPaint.DrawButton( graphicsColumn, rectColumn, ButtonState.Normal );
				else
					ControlPaint.DrawButton( graphicsColumn, rectColumn, ButtonState.Pushed );
			}


			// if there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space)
			if ( (column.ImageIndex > -1) && (ImageList != null) && (column.ImageIndex < this.ImageList.Images.Count) )
				rectColumn = DrawCellGraphic( graphicsColumn, rectColumn, this.ImageList.Images[ column.ImageIndex ], HorizontalAlignment.Left );

			DrawCellText( graphicsColumn, rectColumn, column.Text, column.TextAlignment, this.HeaderTextColor, this.HeaderTextFont, false, HeaderWordWrap );

			// dont really need the arrow
			//			if ( ( ta > -25 ) && ( column.LastSortState == GLColumn.ColumnSortState.SortedDown ) )
			//			{
			//				int tay = rectColumn.Y + (( rectColumn.Height - 6 )/2);
			//				graphicsColumn.DrawLine( SystemPens.ControlDarkDark, ta, tay, ta + 6, tay + 6 );
			//				graphicsColumn.DrawLine( SystemPens.ControlDarkDark, ta + 12, tay, ta + 6, tay + 6 );
			//				graphicsColumn.DrawLine( SystemPens.ControlDarkDark, ta, tay, ta + 12, tay );
			//			}
		}


		/// <summary>
		/// Draw client rows of list control
		/// </summary>
		/// <param name="bmpRows"></param>
		/// <param name="sizeRows"></param>
		public void DrawRows( Graphics graphicsRows )
		{
			DW("DrawRows");


			SolidBrush brush = new SolidBrush( this.BackColor );
			graphicsRows.FillRectangle( brush, this.RowsClientRect );
			brush.Dispose();

			// if they have a background image, then display it
			if ( this.BackgroundImage != null )
				graphicsRows.DrawImage( this.BackgroundImage, 0, 0 );

			int nXCursor = -this.hPanelScrollBar.Value;
			foreach ( GLColumn colonne in Columns )
			{
				if ( colonne.BackColor != Color.Transparent && 
					colonne.BackColor != BackColor )
				{
					Brush br = new SolidBrush ( colonne.BackColor );
					graphicsRows.FillRectangle ( br, nXCursor, this.RowsClientRect.Top, colonne.Width, this.RowsClientRect.Height );
				}
				nXCursor += colonne.Width;
			}




			// determine start item based on whether or not we have a vertical scrollbar present
			int nStartItem;				// which item to start with in this visible pane
			if ( this.vPanelScrollBar.Visible == true )
				nStartItem = this.vPanelScrollBar.Value;
			else
				nStartItem = 0;


			Rectangle rectRow = this.RowsRect;	
			rectRow.Height = ItemHeight;

			/* Draw Rows */
			for ( int nItem = 0; ((nItem < (VisibleRowsCount +1) ) && ((nItem+nStartItem) < Count )); nItem++ )
			{
				DrawRow( graphicsRows, rectRow, nItem+nStartItem );
				rectRow.Y += ItemHeight;
			}


			if ( GLGridLines == GLGridStyles.gridSolid )
				DrawGridLines( graphicsRows, this.RowsInnerClientRect );


			// draw hot tracking column
			if ( ( this.HotColumnIndex != -1 ) && ( HotColumnIndex < Columns.Count ) )
			{
				nXCursor = -this.hPanelScrollBar.Value;
				for ( int nColumnIndex = 0; nColumnIndex < this.HotColumnIndex; nColumnIndex++ )
					nXCursor += Columns[nColumnIndex].Width;

                //Color transparentColor = Color.FromArgb(100, 182, 189, 210);
                Color transparentColor = Color.FromArgb(100, HotTrackingColor);
				Brush hotBrush = new SolidBrush(transparentColor);

				graphicsRows.FillRectangle( hotBrush, nXCursor, RowsInnerClientRect.Y, Columns[HotColumnIndex].Width+1, RowsInnerClientRect.Height-1 );

				hotBrush.Dispose();
			}

		}

        public bool HasImages
        {
            get
            {
                return m_bHasImages;
            }
            set
            {
                m_bHasImages = value;
            }
        }

		public bool CheckBoxes
		{
			get
			{
				return Columns.Count > 0 && Columns[0].IsCheckColumn;
			}
			set
			{
				if ( value )
				{
					if ( Columns.Count > 0 && Columns[0].IsCheckColumn )
					{
						return;
					}
					GLColumn col = new GLColumn();
					col.Text = "";
					col.IsCheckColumn = true;
					col.Width = 18;
					GLColumnCollection cols = new GLColumnCollection();
					cols.Add ( col );
					foreach ( GLColumn oldCol in Columns )
						cols.Add ( oldCol );
					m_Columns = cols;
					Invalidate();
					Refresh();
				}
				else
				{
					if ( Columns.Count > 0 && Columns[0].IsCheckColumn )
					{
						Columns.RemoveAt(0);
						Invalidate();
						return;
					}
				}
			}
		}


        //----- YK: Cache d'optimisation des performances de la GL -----
        // On met en cache la valeurs des propriétés de chaque item de la liste pour ne pas appeler GetDonneeDynamiqueString
        // systématiquement. Le chache est vidé à chaque fois que la liste source change.
        // Structure du chache : Dictionary<KeyValuePair<index de l'item, "nom de la prop.">, "valeur texte de la prop.">
        private Dictionary<KeyValuePair<int, string>, string> m_tabDonneesDynamiqueStringEnCache =
            new Dictionary<KeyValuePair<int, string>, string>();

		/// <summary>
		/// Draw Row to screen
		/// </summary>
		/// <param name="graphicsRow"></param>
		/// <param name="rectRow"></param>
		/// <param name="item"></param>
		public void DrawRow( Graphics graphicsRow, Rectangle rectRow, int nItemIndex )
		{
			DW("DrawRow");


			// row background, if its selected, that trumps all, if not then see if we are using alternating colors, if not draw normal
			// note, this can all be overridden by the sub item background property

			bool bIsSelected = m_tableSelection[nItemIndex] != null;
			bool bIsChecked = m_tableChecked[nItemIndex] != null;
			

			if ( bIsSelected )
			{
				SolidBrush brushBK;
				brushBK = new SolidBrush( Color.FromArgb( 255, SelectionColor.R, SelectionColor.G, SelectionColor.B ) );


				// *** WARNING *** need to check for full row select here
				if ( !FullRowSelect )
				{	// calculate how far into the control it goes
					int nWidthFR = -this.hPanelScrollBar.Value + Columns.Width;
					graphicsRow.FillRectangle( brushBK, this.RowsInnerClientRect.X, rectRow.Y, nWidthFR, rectRow.Height );
				}
				else
					graphicsRow.FillRectangle( brushBK, this.RowsInnerClientRect.X, rectRow.Y, this.RowsInnerClientRect.Width, rectRow.Height );


				//graphicsRow.FillRectangle( brushBK, rectRow );
				brushBK.Dispose();
			}
			else
			{
				if ( this.AlternatingColors )
				{
					int nACItemIndex = nItemIndex;
					if ( ( nACItemIndex % 2 ) > 0 )
					{
						SolidBrush brushBK = new SolidBrush( this.AlternateBackground );
						graphicsRow.FillRectangle( brushBK, this.RowsInnerClientRect.X, rectRow.Y, this.RowsInnerClientRect.Width, rectRow.Height );
						brushBK.Dispose();
					}
				}
			}




			// draw the row of sub items
			int nXCursor = -this.hPanelScrollBar.Value;
			/*for ( int nSubItem = 0; nSubItem < Columns.Count; nSubItem++ )
			{
				Rectangle rectSubItem = new Rectangle( nXCursor, rectRow.Y, Columns[nSubItem].Width, rectRow.Height );

				// avoid drawing items that are not in the visible region
				if ( ( rectSubItem.Right < 0 ) || ( rectSubItem.Left > this.RowsInnerClientRect.Right ) )
					Debug.Write( "" );
				else
					DrawSubItem( graphicsRow, rectSubItem, item, item.SubItems[nSubItem], nSubItem );

				nXCursor += Columns[nSubItem].Width;
			}*/


			Object obj = null;
			if ( ListeSource != null && nItemIndex >= 0 && nItemIndex < ListeSource.Count )
			{
				obj = ListeSource[nItemIndex];
				if ( obj != null )
				{
                    if (m_bHasImages)
                    {
                        Image img = null;
                        if (OnGetImage != null)
                            img = OnGetImage(obj);
                        Rectangle rctImage = new Rectangle(nXCursor, rectRow.Y, rectRow.Height, rectRow.Height);
                        if (img != null)
                            graphicsRow.DrawImage(img, rctImage);
                        nXCursor += rectRow.Height;
                    }
					Type tp = obj.GetType();
					for ( int nCol = 0; nCol < Columns.Count; nCol++ )
					{
						GLColumn col = Columns[nCol];
						Rectangle rectSubItem = new Rectangle( nXCursor, rectRow.Y, Columns[nCol].Width, rectRow.Height );

						if ( !bIsSelected && col.BackColor != Color.Transparent )
						{
							Brush br = new SolidBrush ( col.BackColor );
							graphicsRow.FillRectangle ( br, rectSubItem );
							br.Dispose();
						}

						// avoid drawing items that are not in the visible region
						if ( ( rectSubItem.Right >= 0 ) && ( rectSubItem.Left <= this.RowsInnerClientRect.Right ) )
						{
							if ( col.IsCheckColumn )
							{
								LoadImages();
								Image img = bIsChecked?m_imageCheck:m_imageUncheck;
								graphicsRow.DrawImage ( img, rectSubItem.Left+rectSubItem.Width/2-img.Width/2, rectSubItem.Bottom - img.Height-1, img.Width, img.Height );
							}
							else
							{
								string strProp = Columns[nCol].Propriete;
								string strVal = "";
								try
								{
                                    
                                    KeyValuePair<int, string> paireItemIndexPropriete = new KeyValuePair<int, string>(nItemIndex, strProp);
                                    if (!m_tabDonneesDynamiqueStringEnCache.TryGetValue(paireItemIndexPropriete, out strVal))
                                    {
                                        strVal = CInfoStructureDynamique.GetDonneeDynamiqueString(obj, strProp, "");
                                        m_tabDonneesDynamiqueStringEnCache.Add(paireItemIndexPropriete, strVal);
                                    }
                                    //if (m_tabDonneesDynamiqueStringEnCache.ContainsKey(paireItemIndexPropriete))
                                    //{
                                    //    strVal = m_tabDonneesDynamiqueStringEnCache[paireItemIndexPropriete];
                                    //}
                                    //else
                                    //{
                                    //    strVal = CInfoStructureDynamique.GetDonneeDynamiqueString(obj, strProp, "");
                                    //    m_tabDonneesDynamiqueStringEnCache.Add(paireItemIndexPropriete, strVal);
                                    //    //strVal = CInterpreteurTextePropriete.GetStringValue ( obj, strProp, "" );
                                    //}
								}
								catch 
								{
								}
								strVal = strVal.Replace("\r","");
								strVal = strVal.Replace("\n", " ,");
								strVal = strVal.Replace("\t","");

								Color forCol = ForeColor;
								if ( col.ForColor != Color.Transparent )
									forCol = col.ForColor;
						
                                Rectangle rcText = rectSubItem;
                                DrawCellText(graphicsRow, rcText, strVal, col.TextAlignment, forCol, m_tableSelection[nItemIndex] != null, ItemWordWrap);
							}
					
						}
						nXCursor += Columns[nCol].Width;
					}
				}
			}


			if ( nItemIndex == this.HotItemIndex )									// handle hot tracking of items
			{
                //Color transparentColor = Color.FromArgb(75, 182, 189, 210);
                Color transparentColor = Color.FromArgb(100, HotTrackingColor);
                Brush hotBrush = new SolidBrush(transparentColor);

				graphicsRow.FillRectangle( hotBrush, this.RowsInnerClientRect.X, rectRow.Y, this.RowsInnerClientRect.Width, rectRow.Height );

				hotBrush.Dispose();
			}

			if ( m_nFocusedIndex == nItemIndex )												// deal with focus rect
			{
				//ControlPaint.DrawFocusRectangle( graphicsRow, new Rectangle( rectRow.X+1, rectRow.Y+1, rectRow.Width-1, rectRow.Height-1 ) );
				ControlPaint.DrawFocusRectangle( graphicsRow, new Rectangle( this.RowsInnerClientRect.X+1, rectRow.Y+1, this.RowsInnerClientRect.Width-1, rectRow.Height-1 ) );
			}

		}


		/*/// <summary>
		/// draw sub item
		/// </summary>
		/// <param name="graphicsSubItem"></param>
		/// <param name="rectSubItem"></param>
		/// <param name="subItem"></param>
		/// <param name="nColumn"></param>
		public void DrawSubItem( Graphics graphicsSubItem, Rectangle rectSubItem, GLItem item, GLSubItem subItem, int nColumn )
		{
			DW("DrawSubItem");

			// precheck to make sure this is big enough for the things we want to do inside it
			Rectangle subControlRect = new Rectangle( rectSubItem.X, rectSubItem.Y, rectSubItem.Width, rectSubItem.Height );


			if ( ( subItem.Control != null ) && (!subItem.ForceText ) )
			{	// custom embedded control here
				Control control = subItem.Control;
				Rectangle subrc = new Rectangle( 
					subControlRect.X+this.CellPaddingSize, 
					subControlRect.Y+this.CellPaddingSize, 
					subControlRect.Width-this.CellPaddingSize*2,
					subControlRect.Height-this.CellPaddingSize*2 );

				Type tp = control.GetType();
				PropertyInfo pi = control.GetType().GetProperty( "PreferredHeight" );
				if ( pi != null )
				{
					int PreferredHeight = (int)pi.GetValue( control, null );

					if ( ( (PreferredHeight + this.CellPaddingSize*2)> this.ItemHeight ) && AutoHeight )
						this.ItemHeight = PreferredHeight + this.CellPaddingSize*2;

					subrc.Y = subControlRect.Y + ((subControlRect.Height - PreferredHeight)/2);
				}

				NewLiveControls.Add( control );						// put it in the new list, remove from old list
				if ( LiveControls.Contains( control ) )				// make sure its in the old list first
				{
					LiveControls.Remove( control );			// remove it from list so it doesn't get put down
				}

				if ( control.Bounds.ToString() != subrc.ToString() )
					control.Bounds = subrc;							// this will force an invalidation

				if ( control.Visible != true )
					control.Visible = true;

			}
			else	// not control based
			{
				if ( subItem.BackColor != Color.White )
				{
					SolidBrush bbrush = new SolidBrush( subItem.BackColor );
					graphicsSubItem.FillRectangle( bbrush, rectSubItem );
					bbrush.Dispose();
				}

				// if there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space)
				if ( (subItem.ImageIndex > -1) && (ImageList != null) && (subItem.ImageIndex < this.ImageList.Images.Count) )
					rectSubItem = DrawCellGraphic( graphicsSubItem, rectSubItem, this.ImageList.Images[ subItem.ImageIndex ], subItem.ImageAlignment );

				// deal with text color in a box on whether it is selected or not
				Color textColor;
				if ( item.Selected )
					textColor = this.SelectedTextColor;
				else
					textColor = subItem.ForeColor;

				DrawCellText( graphicsSubItem, rectSubItem, subItem.Text, Columns[nColumn].TextAlignment, textColor, item.Selected, ItemWordWrap );
			}

		}*/


		/// <summary>
		/// draw the contents of a cell, do not draw any background or associated things
		/// </summary>
		/// <param name="graphicsCell"></param>
		/// <param name="rectCell"></param>
		/// <param name="img"></param>
		/// <param name="alignment"></param>
		/// <returns>
		/// returns the area of the cell that is left for you to put anything else on.
		/// </returns>
		public Rectangle DrawCellGraphic( Graphics graphicsCell, Rectangle rectCell, Image img, HorizontalAlignment alignment )
		{
			int th, ty, tw, tx;

			th = img.Height + (CellPaddingSize*2);
			tw = img.Width + (CellPaddingSize*2);
			MaxHeight = th;										// this will only set if autosize is true

			if ( ( tw > rectCell.Width ) || ( th > rectCell.Height ) )
				return rectCell;					// not enough room to draw the image, bail out

			if ( alignment == HorizontalAlignment.Left )
			{
				ty = rectCell.Y + CellPaddingSize + ((rectCell.Height-th)/2);
				tx = rectCell.X + CellPaddingSize;

				graphicsCell.DrawImage( img, tx, ty );

				// remove the width that we used for the graphic from the cell
				rectCell.Width -= (img.Width + (CellPaddingSize*2));
				rectCell.X += tw;
			}
			else if ( alignment == HorizontalAlignment.Center )
			{
				ty = rectCell.Y + CellPaddingSize + ((rectCell.Height-th)/2);
				tx = rectCell.X + CellPaddingSize + ((rectCell.Width-tw)/2);;

				graphicsCell.DrawImage( img, tx, ty );

				// remove the width that we used for the graphic from the cell
				//rectCell.Width -= (img.Width + (CellPaddingSize*2));
				//rectCell.X += (img.Width + (CellPaddingSize*2));
				rectCell.Width = 0;
			}
			else if ( alignment == HorizontalAlignment.Right )
			{
				ty = rectCell.Y + CellPaddingSize + ((rectCell.Height-th)/2);
				tx = rectCell.Right - tw;

				graphicsCell.DrawImage( img, tx, ty );

				// remove the width that we used for the graphic from the cell
				rectCell.Width -= tw;
			}



			return rectCell;
		}

        public void DrawCellText(Graphics graphicsCell, Rectangle rectCell, string strCellText, ContentAlignment alignment, Color textColor, bool bSelected, bool bWordWrap)
        {
            DrawCellText(graphicsCell, rectCell, strCellText, alignment, textColor, this.Font, bSelected, bWordWrap);
        }

		/// <summary>
		/// Draw cell text is used by header and cell to draw properly aligned text in subitems.
		/// </summary>
		/// <param name="graphicsCell"></param>
		/// <param name="rectCell"></param>
		/// <param name="strCellText"></param>
		/// <param name="alignment"></param>
        public void DrawCellText(Graphics graphicsCell, Rectangle rectCell, string strCellText, ContentAlignment alignment, Color textColor, Font textFont, bool bSelected, bool bWordWrap)
		{
			int nInteriorWidth = rectCell.Width - (CellPaddingSize*2);
			int nInteriorHeight = rectCell.Height - (CellPaddingSize*2);

			// deal with text color in a box on whether it is selected or not
			SolidBrush textBrush;
			if ( bSelected )
				textBrush = new SolidBrush( this.SelectedTextColor );
			else
				textBrush = new SolidBrush( textColor );

			// convert property editor friendly alignment to an alignment we can use for strings
			StringFormat sf = new StringFormat();
			sf.Alignment = GLStringHelpers.ConvertContentAlignmentToHorizontalStringAlignment( alignment );
			sf.LineAlignment = GLStringHelpers.ConvertContentAlignmentToVerticalStringAlignment( alignment );

			SizeF measuredSize;
			if ( bWordWrap )
			{
				sf.FormatFlags = 0;	// word wrapping is on by default for drawing
                measuredSize = graphicsCell.MeasureString(strCellText, textFont, new Point(BorderPadding, BorderPadding), sf);
			}
			else
			{	// they aren't word wrapping so we need to put the ...'s where necessary
				sf.FormatFlags = StringFormatFlags.NoWrap;
                measuredSize = graphicsCell.MeasureString(strCellText, textFont, new Point(BorderPadding, BorderPadding), sf);
				if ( measuredSize.Width > nInteriorWidth )		// dont truncate if we are doing word wrap
                    strCellText = GLStringHelpers.TruncateString(strCellText, nInteriorWidth, graphicsCell, textFont);
			}

			MaxHeight = (int)measuredSize.Height + (BorderPadding*2);													// this will only set if autosize is true
            //graphicsCell.DrawString(strCellText, Font, textBrush, rectCell /*rectCell.X+this.BorderPadding, rectCell.Y+this.BorderPadding*/, sf);
            Rectangle rectTexte = new Rectangle(
                rectCell.X + this.BorderPadding,
                rectCell.Y,
                rectCell.Width - this.BorderPadding,
                rectCell.Height);
            graphicsCell.DrawString(strCellText, textFont, textBrush, rectTexte, sf);

			textBrush.Dispose();
		}




		/// <summary>
		/// draw grid lines in row area
		/// </summary>
		/// <param name="RowsDC"></param>
		/// <param name="rect"></param>
		/// <param name="bVertLines"></param>
		public void DrawGridLines( Graphics RowsDC, Rectangle rect )
		{
			DW("DrawGridLines");

			int nStartItem = this.vPanelScrollBar.Value;
			/* Draw Rows */
			int nYCursor = rect.Y;
			Pen pen = new Pen(m_colorGridColor);
			//for (int nItem = 0; ((nItem < (VisibleRowsCount +1) ) && ((nItem+nStartItem) < Items.Count )); nItem++ )
			for (int nItem = 0; (nItem < (VisibleRowsCount +1)); nItem++ )
			{	//Debug.WriteLine( "ItemCount " + Items.Count.ToString() + " Item Number " + nItem.ToString() );
				nYCursor += ItemHeight;

				// draw horizontal line
				RowsDC.DrawLine( pen, 0, nYCursor, rect.Width, nYCursor );			// bottom line will always be there
			}

			int nXCursor = -this.hPanelScrollBar.Value;
            if (HasImages)
                nXCursor += ItemHeight;
			for ( int nColumn = 0; nColumn < Columns.Count; nColumn++ )
			{
				// draw vertical line
				nXCursor += Columns[nColumn].Width;
				RowsDC.DrawLine( pen, nXCursor, rect.Y, nXCursor, rect.Bottom );
			}
			pen.Dispose();
		}


		#endregion // drawing

		#region Keyboard

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
		}

		#endregion

		#region Scrolling

		// Handlers for scrollbars scroll
		protected void OnScroll(object sender, EventArgs e)
		{
			//GenerateColumnRects();
			DI("Calling Invalidate From OnScroll");
			Invalidate();
		}


#if false
		protected void RecalcSubControls()
		{
			for ( int index = 0, nVisibleItem = 0; index < this.Count; index++ )
			{
				// test and see if this is visible or not

				if ( ( index < this.vPanelScrollBar.Value ) || ( index > ( this.vPanelScrollBar.Value + ( this.VisibleRowsCount ) ) ) )
				{
					for ( int subindex = 0; subindex < Columns.Count; subindex++ )
					{
						if ( ( Columns[ subindex ].ControlType != GLColumn.ColumnControlTypes.None ) && ( Items[index].SubItems[ subindex ].Control != null ) )
							Items[index].SubItems[ subindex ].Control.Hide();
					}
				}
				else
				{
					int nXCursor = -this.hPanelScrollBar.Value;

					for ( int subindex = 0; subindex < Columns.Count; subindex++ )
					{
						if ( ( Columns[ subindex ].ControlType != GLColumn.ColumnControlTypes.None ) && ( Items[index].SubItems[ subindex ].Control != null ) )
						{
							Rectangle rectCtrl = new Rectangle( nXCursor, (nVisibleItem*this.ItemHeight) + this.RowsClientRect.Y, Columns[subindex].Width , this.ItemHeight );

							Items[index].SubItems[ subindex ].Control.Bounds = rectCtrl;
							Items[index].SubItems[ subindex ].Control.Show();
						}

						nXCursor += Columns[subindex].Width;
					}

					nVisibleItem++;
				}
			}
		}
#endif

		protected void RecalcScroll( )//Graphics g )
		{
			DW("RecalcScroll");


			// lets add recalc of transparent picture box as well



			//this.SuspendLayout();

			int nSomethingHasGoneVeryWrongSoBreakOut = 0;
			bool bSBChanged;
			do					// this loop is to handle changes and rechanges that happen when oen or the other changes
			{
				DW("Begin scrolbar updates loop");
				bSBChanged = false;

				if ( (Columns.Width > RowsInnerClientRect.Width) && (hPanelScrollBar.Visible == false) )
				{	// total width of all the rows is less than the visible rect
					hPanelScrollBar.mVisible = true;
					hPanelScrollBar.Value = 0;
					bSBChanged = true;

					DI("Calling Invalidate From RecalcScroll");
					Invalidate();

					DW("showing hscrollbar");
				}

				if ( (Columns.Width <= RowsInnerClientRect.Width) && (hPanelScrollBar.Visible == true) )
				{	// total width of all the rows is less than the visible rect
					hPanelScrollBar.mVisible = false;
					hPanelScrollBar.Value = 0;
					bSBChanged = true;
					DI("Calling Invalidate From RecalcScroll");
					Invalidate();

					DW("hiding hscrollbar");
				}

				if ( (TotalRowHeight > RowsInnerClientRect.Height) && (vPanelScrollBar.Visible == false) )
				{  // total height of all the rows is greater than the visible rect
					vPanelScrollBar.mVisible = true;
					hPanelScrollBar.Value = 0;
					bSBChanged = true;
					DI("Calling Invalidate From RecalcScroll");
					Invalidate();

					DW("showing vscrollbar");
				}

				if ( (TotalRowHeight <= RowsInnerClientRect.Height) && (vPanelScrollBar.Visible == true) )
				{	// total height of all rows is less than the visible rect
					vPanelScrollBar.mVisible = false;
					vPanelScrollBar.Value = 0;
					bSBChanged = true;
					DI("Calling Invalidate From RecalcScroll");
					Invalidate();

					DW("hiding vscrollbar");
				}

				DW("End scrolbar updates loop");

				// *** WARNING *** WARNING *** Kludge.  Not sure why this is sometimes hanging.  Fix this.
				if ( ++nSomethingHasGoneVeryWrongSoBreakOut > 4 )
					break;

			} while ( bSBChanged == true );		// this should never really run more than twice


			//Rectangle headerRect = HeaderRect;		// tihs is an optimization so header rect doesnt recalc every time we call it
			Rectangle rectClient = RowsInnerClientRect;

			/*
			 *  now that we know which scrollbars are showing and which aren't, resize the scrollbars to fit those windows
			 */
			if ( vPanelScrollBar.Visible == true )
			{
				vPanelScrollBar.mTop = rectClient.Y;
				vPanelScrollBar.mLeft = rectClient.Right;
				vPanelScrollBar.mHeight = rectClient.Height;
				vPanelScrollBar.mLargeChange = VisibleRowsCount;
				vPanelScrollBar.mMaximum = Count-1;

				if ( ((vPanelScrollBar.Value + VisibleRowsCount ) > Count) )		// catch all to make sure the scrollbar isnt going farther than visible items
				{
					DW("Changing vpanel value");
					vPanelScrollBar.Value = Count - VisibleRowsCount;				// an item got deleted underneath somehow and scroll value is larger than can be displayed
				}
			}

			if ( hPanelScrollBar.Visible == true )
			{
				hPanelScrollBar.mLeft = rectClient.Left;
				hPanelScrollBar.mTop = rectClient.Bottom;
				hPanelScrollBar.mWidth = rectClient.Width;

				hPanelScrollBar.mLargeChange = rectClient.Width;	// this reall is the size we want to move
				hPanelScrollBar.mMaximum = Columns.Width;

				if ( (hPanelScrollBar.Value + hPanelScrollBar.LargeChange) > hPanelScrollBar.Maximum )
				{
					DW("Changing vpanel value");
					hPanelScrollBar.Value = hPanelScrollBar.Maximum - hPanelScrollBar.LargeChange;
				}
			}

#if true
			if ( BorderPadding > 0 ) 
			{
				horiBottomBorderStrip.Bounds = new Rectangle( 0, this.ClientRectangle.Bottom-this.BorderPadding, this.ClientRectangle.Width, this.BorderPadding ) ;		// horizontal bottom picture box
				horiTopBorderStrip.Bounds = new Rectangle( 0, this.ClientRectangle.Top, this.ClientRectangle.Width, this.BorderPadding ) ;		// horizontal bottom picture box

				vertLeftBorderStrip.Bounds = new Rectangle( 0, 0, this.BorderPadding, this.ClientRectangle.Height ) ;		// horizontal bottom picture box
				vertRightBorderStrip.Bounds = new Rectangle( this.ClientRectangle.Right-this.BorderPadding, 0, this.BorderPadding, this.ClientRectangle.Height ) ;		// horizontal bottom picture box
			}
			else
			{
				if ( this.horiBottomBorderStrip.Visible )
					this.horiBottomBorderStrip.Visible = false;
				if ( this.horiTopBorderStrip.Visible )
					this.horiTopBorderStrip.Visible = false;
				if ( this.vertLeftBorderStrip.Visible )
					this.vertLeftBorderStrip.Visible = false;
				if ( this.vertRightBorderStrip.Visible )
					this.vertRightBorderStrip.Visible = false;
			}

			if ( hPanelScrollBar.Visible && vPanelScrollBar.Visible )
			{
				if ( !cornerBox.Visible )
					cornerBox.Visible = true;

				cornerBox.Bounds = new Rectangle( hPanelScrollBar.Right, vPanelScrollBar.Bottom, vPanelScrollBar.Width, hPanelScrollBar.Height );
			}
			else
			{
				if ( cornerBox.Visible )
					cornerBox.Visible = false;
			}
#endif


			//DW("Exit recalc scroll");
		}


		private void vPanelScrollBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			DW("vPanelScrollBar_Scroll");
			//this.Focus();

			DI("Calling Invalidate From vPanelScrollBar_Scroll");
			Invalidate();
		}


		private void hPanelScrollBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			DW("hPanelScrollBar_Scroll");
			//this.Focus();

			DI("Calling Invalidate From hPanelScrollBar_Scroll");

			Invalidate();
		}


		#endregion

		#region Mouse


		/// <summary>
		/// OnDoubleclick
		/// 
		/// if someone double clicks on an area, we need to start a control potentially
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(EventArgs e)
		{
			DW("GlacialList.OnDoubleClick");

			Point pointLocalMouse = this.PointToClient( Cursor.Position );

			base.OnDoubleClick( e );
		}



		/// <summary>
		/// had to put this routine in because of overriden protection level being unchangable
		/// </summary>
		/// <param name="e"></param>
		internal void OnMouseDownFromSubItem( object Sender, MouseEventArgs e )
		{
			DW("OnMouseDownFromSubItem");
			//Debug.WriteLine( "OnMouseDownFromSubItem called " + e.X.ToString() + " " + e.Y.ToString() );
			Point cp = this.PointToClient( new Point( Control.MousePosition.X, Control.MousePosition.Y ) );
			e = new MouseEventArgs( e.Button, e.Clicks, cp.X, cp.Y, e.Delta );
			//Debug.WriteLine( "after " + cp.X.ToString() + " " + cp.Y.ToString() );
			OnMouseDown( e );
		}


		protected override void OnMouseLeave(EventArgs e)
		{
			this.Columns.ClearHotStates();

			base.OnMouseLeave (e);
		}

		private bool m_bTrierAuClicSurEnteteColonne = true;
		public bool TrierAuClicSurEnteteColonne
		{
			get
			{
				return m_bTrierAuClicSurEnteteColonne;
			}
			set
			{
				m_bTrierAuClicSurEnteteColonne = value;
			}
		}
		public event EventHandler OnChangeSelection;

		/// <summary>
		/// mouse button pressed
		/// </summary>
		/// <param name="e"></param>
		private int m_nItemDragDrop = -1;
		private Point m_ptStartDrag = new Point(0,0);
        private int? m_nItemToSelectOnMouseUp = null;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			DW("GlacialList_MouseDown");

			m_nItemDragDrop = -1;

            m_nItemToSelectOnMouseUp = null;


			int nItem = 0, nColumn = 0;
			ListStates eState;
			InterpretCoords( e.X, e.Y, out nItem, out nColumn, out eState );



			if ( e.Button == MouseButtons.Right )			// if its the right button then we don't really care till its released
				return;


			//-----------------------------------------------------------------------------------------
			if ( eState == ListStates.stateColumnSelect )										// Column select
			{
				m_nState = ListStates.stateNone;

				Columns[ nColumn ].State = ColumnStates.csPressed;
				if ( ColumnClickedEvent != null )
					ColumnClickedEvent( this, new ClickEventArgs( nItem, nColumn ) );				// fire the column clicked event

                if (TrierAuClicSurEnteteColonne)
                {
                    m_tabDonneesDynamiqueStringEnCache.Clear();
                    this.SortColumn(nColumn);
                }
				//Invalidate();
				return;
			}
			//---Resizing -----------------------------------------------------------------------------------
			if ( eState == ListStates.stateColumnResizing )										// resizing
			{
				Cursor.Current = Cursors.VSplit;
				m_nState = ListStates.stateColumnResizing;

				m_pointColumnResizeAnchor = new Point( GetColumnScreenX(nColumn), e.Y );		// deal with moving column sizes
				m_nResizeColumnNumber = nColumn;

				return;
			}
			//--Item check, if no items exist go no further--
			//if ( Items.Count == 0 )
			//return;

			//---Items --------------------------------------------------------------------------------------
			if ( eState == ListStates.stateSelecting )
			{	// ctrl based multi select ------------------------------------------------------------
				if ( nColumn == 0 && CheckBoxes )
				{
					if ( m_tableChecked[nItem] != null )
						m_tableChecked.Remove ( nItem );
					else
						m_tableChecked[nItem] = true;
					if ( CheckedChange != null )
						CheckedChange( this , nItem );
				}

				m_nState = ListStates.stateSelecting;

				m_nFocusedIndex = nItem;

				if ( nItem >= 0 && m_listeSource != null && nItem < m_listeSource.Count && OnBeginDragItem != null &&
					(e.Button  & MouseButtons.Left) == MouseButtons.Left )
				{
					m_nItemDragDrop = nItem;
					Capture = true;
				}


				if ( (( ModifierKeys & Keys.Control) == Keys.Control ) && ( AllowMultiselect == true ) )
				{
					m_nLastSelectionIndex = nItem;

					if ( m_tableSelection[nItem] == null )
						m_tableSelection[nItem] = true;
					else
						m_tableSelection.Remove(nItem);
					if ( OnChangeSelection != null )
						OnChangeSelection ( this, new EventArgs() );
					
					Invalidate();
					return;
				}

				// shift based multi row select -------------------------------------------------------
				if ( (( ModifierKeys & Keys.Shift) == Keys.Shift ) && ( AllowMultiselect == true ) )
				{
					m_tableSelection.Clear();
					//Items.ClearSelection();
					if ( m_nLastSelectionIndex >= 0 )			// ie, non negative so that we have a starting point
					{
						int index = m_nLastSelectionIndex;
						do
						{
							m_tableSelection[index] = true;
							if ( index > nItem )		index--;
							if ( index < nItem )		index++;
						} while ( index != nItem );

						m_tableSelection[index] = true;
						if ( OnChangeSelection != null )
							OnChangeSelection ( this, new EventArgs() );
					}
					Invalidate();

					return;
				}

				// the normal single select -----------------------------------------------------------
                if (m_tableSelection.Contains(nItem))
                {
                    m_nItemToSelectOnMouseUp = nItem;
                    return;
                }

                m_tableSelection.Clear();
				
				//Items.ClearSelection( Items[nItem] );

				// following two if statements deal ONLY with non multi=select where a singel sub item is being selected
				/*if ( ( m_nLastSelectionIndex < Count ) && ( m_nLastSubSelectionIndex < Columns.Count ) )
					Items[m_nLastSelectionIndex].SubItems[m_nLastSubSelectionIndex].Selected = false;
				if ( ( FullRowSelect == false ) && ( ( nItem < Count ) && ( nColumn < Columns.Count ) ) )
					Items[nItem].SubItems[nColumn].Selected = true;*/


				m_nLastSelectionIndex = nItem;
				//m_nLastSubSelectionIndex = nColumn;
				m_tableSelection[nItem] = true;
				//Items[nItem].Selected = true;
				if ( OnChangeSelection != null )
					OnChangeSelection ( this, new EventArgs() );
				Invalidate();

			}
		}


        //--------------------------------------------------------------------------------
        ToolTip m_toolTip = new ToolTip();
        int m_nLastItemForToolTip = -1;
        int m_nLastColForToolTip = -1;

        //--------------------------------------------------------------------------------
        private void m_timerTooltip_Tick(object sender, EventArgs e)
        {
            m_timerTooltip.Stop();
            try
            {
                m_toolTip.Show(m_strTooltipText, this, m_positionTooltip, 15000);
            }
            catch { }
        }

        //--------------------------------------------------------------------------------
        private Point m_positionTooltip = new Point(0,0);
        private string m_strTooltipText = "";
        private void DelayShowTooltip(Point position, string strText)
        {
            m_timerTooltip.Stop();
            m_positionTooltip = position;
            m_strTooltipText = strText;
            if ( m_strTooltipText != "" )
                m_timerTooltip.Start();
        }

        private void HideTooltip()
        {
            m_toolTip.Hide(this);
            m_timerTooltip.Stop();
        }
            
		/// <summary>
		/// when mouse moves
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			DW("GlacialList_MouseMove");


			try
			{
				if ( m_nState == ListStates.stateColumnResizing )
				{
					Cursor.Current = Cursors.VSplit;


					int nWidth;
					nWidth = e.X - m_pointColumnResizeAnchor.X;

					if ( nWidth <= MINIMUM_COLUMN_SIZE )
					{
						nWidth = MINIMUM_COLUMN_SIZE;
					}

					GLColumn col;
					col = (GLColumn)Columns[m_nResizeColumnNumber];
					col.Width = nWidth;
					m_nLastResizeColumn = m_nResizeColumnNumber;

					return;
				}

				if ( (e.Button & MouseButtons.Left)==MouseButtons.Left && m_nItemDragDrop >= 0 )
				{
					if ( Math.Abs ( e.X-m_ptStartDrag.X)>2 ||
						Math.Abs ( e.Y-m_ptStartDrag.Y)>2 )
					{
						if ( OnBeginDragItem != null )
						{
							try
							{
								object elt = m_listeSource[m_nItemDragDrop];
								OnBeginDragItem ( this, elt );
							}
							catch{}
						}
						Capture = false;
					}
				}




				int nItem = 0, nCol = 0;
				ListStates eState;
				InterpretCoords( e.X, e.Y, out nItem, out nCol, out eState );

				if ( eState == ListStates.stateColumnResizing )
				{
					Cursor.Current = Cursors.VSplit;

					return;
				}

				Cursor.Current = Cursors.Arrow;
                
                // Gestion du Tool Tip
                try
                {
                    bool bShow = false;
                    if (nItem != m_nLastItemForToolTip)
                    {
                        bShow = true;
                        m_nLastItemForToolTip = nItem;
                    }
                    if (nCol != m_nLastColForToolTip)
                    {
                        bShow = true;
                        m_nLastColForToolTip = nCol;
                    }

                    if (bShow)
                    {
                        if (nCol >= 0 && nCol < Columns.Count)
                        {
                            string strProp = Columns[nCol].Propriete;
                            string strVal = "";
                            KeyValuePair<int, string> paireItemIndexPropriete = new KeyValuePair<int, string>(nItem, strProp);
                            if (m_tabDonneesDynamiqueStringEnCache.TryGetValue(paireItemIndexPropriete, out strVal))
                            {
                                if (strVal.Trim() != string.Empty)
                                {
                                    Graphics g = CreateGraphics();
                                    SizeF dim = g.MeasureString(strVal, Font);
                                    g.Dispose();
                                    int nLargeurCol = Columns[nCol].Width - (this.BorderPadding * 2);
                                    if ((int)dim.Width >= nLargeurCol)
                                    {
                                        DelayShowTooltip(e.Location, strVal);
                                    }
                                    else
                                        HideTooltip();
                                }
                                else
                                    HideTooltip();
                            }
                            else
                                HideTooltip();
                        }
                        else
                            HideTooltip();
                    }
                }
                catch 
                {
                    HideTooltip();
                }


			}
			catch( Exception ex )
			{
				Debug.WriteLine("Exception throw in GlobalList_MouseMove with text : " + ex.ToString() );

			}

		}

		


		/// <summary>
		/// mouse up
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
            base.OnMouseUp(e);

			DW("MouseUp");
			Capture = false;

			Cursor.Current = Cursors.Arrow;
			Columns.ClearStates();


			int nItem = 0, nColumn = 0;
			ListStates eState;
			InterpretCoords( e.X, e.Y, out nItem, out nColumn, out eState );

			//Suppression de la colonne si taille mini
			if ( m_nLastResizeColumn >= 0 && m_nLastResizeColumn < Columns.Count && Columns[m_nLastResizeColumn].Width <= MINIMUM_COLUMN_SIZE )
			{
				Columns.Remove ( m_nLastResizeColumn );
				Invalidate();
			}
			m_nLastResizeColumn = -1;


			m_nState = ListStates.stateNone;

            if (m_nItemToSelectOnMouseUp != null)
            {
                nItem = m_nItemToSelectOnMouseUp.Value;
                m_nItemToSelectOnMouseUp = null;
                m_tableSelection.Clear();

                m_nLastSelectionIndex = nItem;
                m_tableSelection[nItem] = true;
                if (OnChangeSelection != null)
                    OnChangeSelection(this, new EventArgs());
                Invalidate();
            }

			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right)
				ContexMenuPopUp(this, e);
            
		}


		#endregion

		#region Customsation
		/// //////////////////////////////////////////////
		public bool EnableCustomisation
		{
			get
			{
				return m_bEnableCustomisation;
			}
			set
			{
				m_bEnableCustomisation = value;
			}
		}

		//-------------------------------------------------------------------
		private void ContexMenuPopUp(object sender, EventArgs e)
		{
			if ( !m_bEnableCustomisation )
				return;
			
			if ( ListeSource.Count>0)
			{
				Type tp = ListeSource[0].GetType();
				if ( m_structureObjets == null || m_structureObjets.TypeAssocie != tp )
				{
					m_structureObjets = CInfoStructureDynamique.GetStructure(tp, 1);
					m_popupMenu.MenuItems.Clear();
				}
			}
			if ( m_structureObjets == null )
				return;

			//Crée une Hashtable des colonnes affichées
			Hashtable tableColonnes = new Hashtable();
			foreach ( GLColumn col in Columns)
				tableColonnes[col.Propriete] = col;

			bool bNouveauMenu = m_popupMenu.MenuItems.Count == 0;
			if ( bNouveauMenu)
			{
				MenuItem item = new MenuItem(I.T("Arrange fields|10000"), new EventHandler(OnGererLesChamps));
				m_popupMenu.MenuItems.Add ( item );

				if ( m_bCanChangeActivationCheckBoxes )
				{
					item = new MenuItem(I.T("Use check boxes|10001"), new EventHandler(OnUtiliserCasesACocher));
					item.Checked = CheckBoxes;
					m_popupMenu.MenuItems.Add ( item );
				}
				if ( m_bCanChangeActivationCheckBoxes || CheckBoxes )
				{
					item = new MenuItem ( I.T("Selection|10002") );
					m_popupMenu.MenuItems.Add (item );

					MenuItem newItem = new MenuItem(I.T("Select all|10003"), new EventHandler ( OnToutSelectionner ) );
					item.MenuItems.Add ( newItem );

					newItem = new MenuItem(I.T("Unselect all|10004"), new EventHandler ( OnToutDeselectionner ) );
					item.MenuItems.Add ( newItem );

					newItem = new MenuItem(I.T("Reverse selection|10005"), new EventHandler ( OnInverserSelection ) );
					item.MenuItems.Add ( newItem );
				}

				m_popupMenu.MenuItems.Add("");
			}

			if ( bNouveauMenu )
			{
				FillMenu(m_popupMenu, m_structureObjets, tableColonnes, "");
				MenuItem item = new MenuItem(I.T("More fields|10006"), new EventHandler(OnSelectionnerLesChamps));
				m_popupMenu.MenuItems.Add ( item );
			}
			UpdateCheckState ( m_popupMenu, tableColonnes );
			Point pt = Cursor.Position;
			if ( e is MouseEventArgs )
				pt = new Point ( ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y );
			m_popupMenu.Show ( this, pt );

		}

		//-------------------------------------------------------------------
		private void UpdateCheckState ( Menu menu, Hashtable tableColonnes )
		{
			foreach ( MenuItem item in menu.MenuItems )
			{
				if ( item is MenuItem && (((MenuItem)item).Tag is CInfoChampMenu))
				{
					string strChamp = ((CInfoChampMenu)((MenuItem)item).Tag).Propriete;
					if ( tableColonnes[strChamp]!=null )
					{
						try
						{
							MenuItem itemToCheck = item;
						}
						catch{}
					}
					else
						item.Checked = false;
				}
				UpdateCheckState ( item, tableColonnes );
			}
		}

		//-------------------------------------------------------------------
		private void FillMenu ( Menu menu, CInfoStructureDynamique structure, Hashtable tableColonnes,string strRacine )
		{
			Hashtable tableRubriqueToMenu = new Hashtable();
			//Niveau 0
			foreach ( CInfoChampDynamique champ in structure.Champs )
			{
				if ( champ.StructureValeur == null )
				{
					Menu itemParent = menu;
					if ( champ.Rubrique != "" )
					{
						itemParent = (MenuItem)tableRubriqueToMenu[champ.Rubrique];
						if ( itemParent == null )
						{
							itemParent = new MenuItem ( champ.Rubrique );
							menu.MenuItems.Add ( (MenuItem)itemParent );
							tableRubriqueToMenu[champ.Rubrique] = itemParent;
						}
					}
					MenuItem menuItem = new MenuItem(champ.NomChamp, new EventHandler(MenuItemClick));
					menuItem.Tag = new CInfoChampMenu(champ.NomChamp, strRacine+champ.NomPropriete,champ.TypeDonnee);
					if ( tableColonnes[strRacine+champ.NomPropriete] != null )
					{
						menuItem.Checked = true;
					}
					else
					{
						menuItem.Checked = false;
					}
					itemParent.MenuItems.Add ( menuItem );
				}
			}
			/*//Niveaux suivants
			foreach ( CInfoChampDynamique champ in structure.Champs )
			{
				if ( champ.StructureValeur != null )
				{
					Menu itemParent = menu;
					if ( champ.Rubrique != "" )
					{
						itemParent = (MenuItem)tableRubriqueToMenu[champ.Rubrique];
						if ( itemParent == null )
						{
							itemParent = new MenuItem ( champ.Rubrique );
							menu.MenuItems.Add ( (MenuItem)itemParent );
							tableRubriqueToMenu[champ.Rubrique] = itemParent;
						}
					}
					MenuItem sousMenu = new MenuItem(champ.NomChamp +">>");
					sousMenu.Tag = new CInfoSousStruct ( champ.StructureValeur, tableColonnes, strRacine+champ.NomPropriete+"." );
					//FillMenu ( sousMenu, champ.StructureValeur, tableColonnes, strRacine+champ.NomPropriete+".");
					itemParent.MenuItems.Add ( sousMenu );
					sousMenu.Enabled = true;
					sousMenu.Click += new EventHandler(sousMenu_Popup);
				}
			}*/

		}

		/// /////////////////////////////////
		private class CInfoChampMenu
		{
			public readonly string Nom;
			public readonly string Propriete;
			public readonly Type TypeChamp;
			
			public CInfoChampMenu ( string strNom, string strPropriete, Type typeChamp )
			{
				Nom = strNom;
				Propriete = strPropriete;
				TypeChamp = typeChamp;
			}
		}

		private class CInfoSousStruct
		{
			public readonly CInfoStructureDynamique InfoStruct;
			public readonly string Chemin;

			public readonly Hashtable TableColonnes;
			public CInfoSousStruct ( CInfoStructureDynamique info, Hashtable tableColonnes, string strChemin )
			{
				InfoStruct = info;
				Chemin = strChemin;
				TableColonnes = tableColonnes;
			}
		}

		private void sousMenu_Popup(object sender, EventArgs e)
		{
			if ( sender is MenuItem )
			{
				MenuItem menuItem = (MenuItem)sender;
				if ( menuItem.Tag is CInfoSousStruct )
				{
					CInfoSousStruct info = (CInfoSousStruct)menuItem.Tag;
					/*if ( menuItem.MenuItems.Count == 1 && 
						menuItem.MenuItems[0].Text == "###" )*/
					{
						//menuItem.MenuItems.Clear();
						ContextMenu mnu = new ContextMenu();
						FillMenu ( mnu, info.InfoStruct, info.TableColonnes, info.Chemin );
						Point pt = new Point ( Cursor.Position.X, Cursor.Position.Y);
						pt = PointToClient (pt);
						mnu.Show ( this, pt );
					}
				}
			}
		}

			//-------------------------------------------------------------------
			private void MenuItemClick(object sender, EventArgs e)
			{
				if(sender is MenuItem)
				{
					MenuItem menuItem = ((MenuItem) sender);
					bool bCheck = !menuItem.Checked;
					menuItem.Checked = bCheck;
					if ( bCheck )
					{
						CInfoChampMenu infoChamp = (CInfoChampMenu)menuItem.Tag;
						string strChamp = infoChamp.Propriete;
						GLColumn col = new GLColumn();
						col.Propriete = strChamp;
						col.Text = infoChamp.Nom;
						if ( typeof(double).IsAssignableFrom(infoChamp.TypeChamp ) )
							col.TextAlignment = ContentAlignment.MiddleRight;
						Columns.Add ( col );
					}
					else
					{
						if ( menuItem.Tag is CInfoChampMenu )
						{
							string strChamp = ((CInfoChampMenu)menuItem.Tag).Propriete;
							int nIndex = 0;
							foreach ( GLColumn col in Columns )
							{
								if ( col.Propriete == strChamp )
								{
									Columns.Remove ( nIndex );
									break;
								}
								nIndex++;
							}
						}
					}					
				}
			}

			//-------------------------------------------------------------------
			private void OnGererLesChamps ( object sender, EventArgs args )
			{
				if ( CFormGereColonnesListe.ReorderColonnes ( Columns ) )
					Invalidate();
			}

			//-------------------------------------------------------------------
			private void OnSelectionnerLesChamps ( object sender, EventArgs args )
			{
				ArrayList lst = new ArrayList();
				Hashtable tableAvant = new Hashtable();
				foreach ( GLColumn col in Columns )
				{
					lst.Add ( col.Propriete );
					tableAvant[col.Propriete] = col;
				}
				CInfoChampDynamique[] lstDecoches = null;
				CInfoChampDynamique[] champs = CFormSelectChampParentPourStructure.SelectProprietes ( m_structureObjets, (string[])lst.ToArray(typeof(string)), ref lstDecoches );
				if ( champs == null || champs.Length == 0 )
					return;
				foreach ( CInfoChampDynamique infoChamp in champs )
				{
					string strChamp = infoChamp.NomPropriete;
					if ( tableAvant[strChamp] == null )
					{
						GLColumn col = new GLColumn();
						col.Propriete = strChamp;
						col.Text = infoChamp.NomChamp;
						if ( typeof(double).IsAssignableFrom(infoChamp.TypeDonnee ) )
							col.TextAlignment = ContentAlignment.MiddleRight;
						Columns.Add ( col );
					}
					tableAvant.Remove ( strChamp );
				}

				if ( lstDecoches != null )
				{
					foreach ( CInfoChampDynamique champ in lstDecoches )
					{
						int nIndex = 0;
						foreach ( GLColumn col in Columns )
						{
							if ( col.Propriete == champ.NomPropriete )
							{
								Columns.Remove ( nIndex );
								break;
							}
							nIndex++;
						}
					}
				}
			}


			//-------------------------------------------------------------------
			private void OnUtiliserCasesACocher ( object sender, EventArgs args )
			{
				CheckBoxes = !CheckBoxes;
				if ( sender is MenuItem )
					((MenuItem)sender).Checked = CheckBoxes;
			}

			//-------------------------------------------------------------------
			private void OnToutSelectionner ( object sender, EventArgs args )
			{
				CheckBoxes = true;
				m_tableChecked.Clear();
				for ( int n = 0; n < Count; n++ )
					m_tableChecked[n] = true;
				Refresh();
			}

			//-------------------------------------------------------------------
			private void OnToutDeselectionner ( object sender, EventArgs args )
			{
				CheckBoxes = true;
				m_tableChecked.Clear();
				Refresh();
			}

			//-------------------------------------------------------------------
			private void OnInverserSelection ( object sender, EventArgs args )
			{
				CheckBoxes = true;
				Hashtable newTable = new Hashtable();
				for ( int n = 0; n < Count; n++ )
				{
					if ( !m_tableChecked.Contains ( n ) )
						newTable[n] = true;
				}
				m_tableChecked = newTable;
				Refresh();
			}
			#endregion //Customisation

		#region Sauvegarde dans le registre
			//-------------------------------------------------------------------
			public void ReadFromRegistre(RegistryKey key)
			{
				if ( key == null )
					return;
				byte[] data = (byte[]) key.GetValue("Colonnes"+ContexteUtilisation);
				if (data==null)
					return;
				MemoryStream stream = new MemoryStream(data);
                BinaryReader reader = new BinaryReader(stream);
				CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
                Columns.Serialize(serializer);

                reader.Close();
                stream.Close();
			}
			//-------------------------------------------------------------------
			public void WriteToRegistre(RegistryKey key)
			{
				MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
				CSerializerSaveBinaire saver = new CSerializerSaveBinaire ( writer );
				Columns.Serialize ( saver );
				key.SetValue("Colonnes"+ContexteUtilisation, stream.ToArray());

                writer.Close();
                stream.Close();
			}
			#endregion


		

		#endregion  // functionality

		#region IElementAContexteUtilisation

		private string m_strContexteUtilisation = "";

		private void m_popupMenu_Popup(object sender, System.EventArgs e)
		{
	        
		}

		public string ContexteUtilisation
		{
			get
			{
				return m_strContexteUtilisation;
			}
			set
			{
				m_strContexteUtilisation = value;
			}
		}

		#endregion
		
		#region IControlTraductible

		public System.Collections.Generic.List<string>  GetListeProprietesATraduire()
		{
			return new List<string>();
		}

		public System.Collections.Generic.List<string>  GetListeProprietesSousObjetsATraduire()
		{
			List<string> lst = new List<string>();
			lst.Add("Columns");
			return lst;
		}

		#endregion

        

	}
}


