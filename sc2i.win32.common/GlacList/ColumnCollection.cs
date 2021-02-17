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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using sc2i.common;



namespace sc2i.win32.common
{

	public enum ColumnStates { csNone = 0, csPressed = 1, csHot = 2 }
	public enum ColumnSortState { SortedDown = 0, SortedUp = 1 };
	public enum ColumnControlTypes { None = 0, Custom = 1, DateTime = 2, ComboBox = 3, EditBox = 4, ProgressBar = 5, Button = 6,  }


	/// <summary>
	/// Summary description for Column.
	/// </summary>
	/// 
	[
	DesignTimeVisible(true),
	TypeConverter("sc2i.win32.common.GLColumnConverter")
	]
	public class GLColumn : I2iSerializable, IControlTraductible
	{

		public GLColumn()
		{
		}

		public GLColumn( string name )
		{
			this.Name = name;
			this.Text = name;
		}


		#region Events and Delegates

		public delegate void ChangedEventHandler( object source, ChangedEventArgs e );				//int nItem, int nSubItem );
		public event ChangedEventHandler ChangedEvent;

		#endregion

#if false  // this is for activated properties
		public object NewControl()
		{
			switch ( ControlType )
			{
				case GLColumn.ColumnControlTypes.Button:
					return new Button();
				case GLColumn.ColumnControlTypes.ComboBox:
					return new ComboBox();
				case GLColumn.ColumnControlTypes.DateTime:
					return new DateTime();
				case GLColumn.ColumnControlTypes.EditBox:
					return new TextBox();
				case GLColumn.ColumnControlTypes.ProgressBar:
					return new ProgressBar();
			}

			return null;
		}

		public bool ApplyProperties( Hashtable propTable, Control control )
		{

			return true;
		}

		public bool ImprintProperties( Hashtable propTable, Control control )
		{

			return true;
		}
#endif

		#region VarEnumProperties

		private int						m_nWidth = 100;
		private string					m_strName = "Name";
		private string					m_strPropriete = "";
		private string					m_strText = "Column";
		private ColumnStates			m_State = ColumnStates.csNone;
		private ColumnSortState			m_LastSortDirection = ColumnSortState.SortedUp;
		private ColumnControlTypes		m_ControlType = ColumnControlTypes.None;
		private ArrayList				m_ActiveControlItems = new ArrayList();		
		private ContentAlignment		m_TextAlignment = ContentAlignment.MiddleLeft;
		private int						m_ImageIndex = -1;
		private Color					m_backColor = Color.Transparent;
		private Color					m_forColor = Color.Black;
		private bool					m_bIsCheckColumn = false;

		//[Editor(@"System.Windows.Forms.Design.ImageIndexEditor, System.Design,
		//Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
		//typeof(System.Drawing.Design.UITypeEditor))]
		//[Editor(typeof(System.Windows.Forms.Design.ImageIndexEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public int ImageIndex
		{
			get { return m_ImageIndex; }
			set { m_ImageIndex = value; }
		}




		/// <summary>
		/// Alignment of text in the header and in the cells
		/// </summary>
		public ContentAlignment TextAlignment
		{
			get { return m_TextAlignment; }
			set { m_TextAlignment = value; }
		}

		/// <summary>
		/// Couleur de fond ( Transparent pour utiliser la couleur de la liste
		/// </summary>
		public Color BackColor
		{
			get
			{
				return m_backColor;
			}
			set
			{
				m_backColor = value;
			}
		}


		/// <summary>
		/// Couleur de texte ( transparent pour utiliser la couleur avant plan de la liste )
		/// </summary>
		public Color ForColor
		{
			get
			{
				return m_forColor;
			}
			set
			{
				m_forColor = value;
			}
		}


			/// <summary>
			/// this holds references to items that currently contain live controls.  
			/// This is an optimization so I don't have to iterate the entire Items list
			/// each draw cycle to remove controls no longer visible
			/// </summary>
			[
			Description("Array of items that have live controls."),
			Browsable( false )
			]
			public ArrayList ActiveControlItems
		{
			get { return m_ActiveControlItems; }
			set { m_ActiveControlItems = value; }
		}

		[
		Description("Indique que la colonne est la colonne des checkboxes"),
		Browsable( false )
		]
		public bool IsCheckColumn
		{
			get { return m_bIsCheckColumn; }
			set 
			{ 
				m_bIsCheckColumn = value; 
				if ( value )
					Propriete = "";
			}
		}



		/// <summary>
		/// Last sort state
		/// </summary>
		[
		Description("Last time sort was done, which direction."),
		Browsable( false )
		]
		public ColumnSortState LastSortState
		{
			get { return m_LastSortDirection; }
			set { m_LastSortDirection = (ColumnSortState)value; }
		}


		/// <summary>
		/// Width of column
		/// </summary>
		[
		Category("Control"),
		Browsable( true )
		]
		public ColumnControlTypes ControlType
		{
			get
			{
				return m_ControlType;
			}
			set
			{
				if ( m_ControlType != value )
				{
					m_ControlType = value;
					if ( ChangedEvent != null )
						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
				}
			}
		}


		/// <summary>
		/// Width of column
		/// </summary>
		[
		Category("Design"),
		Browsable( true )
		]
		public int Width
		{
			get
			{
				return m_nWidth;
			}
			set
			{
				if ( IsCheckColumn )
					value = 18;
				if ( m_nWidth != value )
				{
					m_nWidth = value;
					if ( ChangedEvent != null )
						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
				}
			}
		}


		/// <summary>
		/// Text 
		/// </summary>
		[
		Category("Misc"),
		Description("Text to be displayed in header."),
		Browsable( true )
		]
		public string Text
		{
			get
			{
				return m_strText;
			}
			set
			{
				if ( m_strText != (string)value )
				{
					m_strText = (string)value;
					if ( ChangedEvent != null )
						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
				}
			}
		}

		/// <summary>
		/// Name of the column internally
		/// </summary>
		[
		Category("Design"),
		Browsable( true )
		]
		public string Name
		{
			get
			{
				return m_strName;
			}
			set
			{
				if ( m_strName != (string)value )
				{
					m_strName = (string)value;
					if ( ChangedEvent != null )
						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
				}
			}
		}

		[Category("Donnees")]
		[Browsable(true)]
		public string Propriete
		{
			get
			{
				return m_strPropriete;
			}
			set
			{
				m_strPropriete = value;
			}
		}



		//		/// <summary>
		//		/// layout of the column
		//		/// </summary>
		//		[
		//		Category("Misc"),
		//		Browsable( true )
		//		]
		//		public HorizontalAlignment Alignment
		//		{
		//			get
		//			{
		//				return m_Alignment;
		//			}
		//			set
		//			{
		//				if ( m_Alignment != value )
		//				{
		//					m_Alignment = value;
		//					if ( ChangedEvent != null )
		//						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
		//				}
		//			}
		//		}

		/// <summary>
		/// State of the column
		/// </summary>
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public ColumnStates State
		{
			get
			{
				return m_State;
			}
			set
			{
				if ( m_State != value)
				{
					m_State = value;
					if ( ChangedEvent != null )
						ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnChanged, Name ) );				// fire the column clicked event
				}
			}
		}


		#endregion

		#region Membres de I2iSerializable
		private int GetNumVersion()
		{
			return 2;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteInt	 ( ref m_nWidth );
			serializer.TraiteString ( ref m_strName );
			serializer.TraiteString ( ref m_strPropriete );
			serializer.TraiteString ( ref m_strText );
			int nAlignement = (int)m_TextAlignment;
			serializer.TraiteInt ( ref nAlignement );
			m_TextAlignment = (ContentAlignment)nAlignement;

			if ( nVersion >= 1 )
			{
				int nCouleur = BackColor.ToArgb();
				serializer.TraiteInt ( ref nCouleur );
				BackColor = Color.FromArgb ( nCouleur );

				nCouleur = ForColor.ToArgb();
				serializer.TraiteInt ( ref nCouleur );
				ForColor = Color.FromArgb ( nCouleur );
			}
			if ( nVersion >= 2 )
				serializer.TraiteBool ( ref m_bIsCheckColumn );

			
			return result;
		}

		#endregion

		#region IControlTraductible Membres

		public System.Collections.Generic.List<string> GetListeProprietesATraduire()
		{
			List<string> lst = new List<string>();
			lst.Add("Text");
			return lst;
		}

		public System.Collections.Generic.List<string> GetListeProprietesSousObjetsATraduire()
		{
			return new List<string>();
		}

		#endregion
	}


	/// <summary>
	/// gl column collection
	/// </summary>
	public class GLColumnCollection : CollectionBase, I2iSerializable
	{

		#region Events and Delegates

		public delegate void ChangedEventHandler( object source, ChangedEventArgs e );				//int nItem, int nSubItem );
		public event ChangedEventHandler ChangedEvent;

		public void GLColumn_Changed( object source, ChangedEventArgs e )
		{	// this gets called when an item internally changes

			if ( ChangedEvent != null )
				ChangedEvent( source, e );				// fire the column clicked event
		}

		#endregion

		#region VarsPropertiesAndEnums

		public GLColumn this[ int nColumnIndex ]
		{
			get
			{
				return List[nColumnIndex] as GLColumn;
			}
		}

		/// <summary>
		/// Index by column name
		/// </summary>
		public GLColumn this[ string strColumnName ]
		{
			get
			{
				return (GLColumn)List[ GetColumnIndex( strColumnName ) ];			// make sure the column is seeded with which one it is before we call it
			}
		}


		/// <summary>
		/// Get the column index that corresponds to the column name
		/// </summary>
		/// <param name="strColumnName"></param>
		/// <returns></returns>
		public int GetColumnIndex( string strColumnName )
		{

			for ( int index = 0; index < List.Count; index++ )
			{
				GLColumn column = (GLColumn)List[index];
				if ( column.Name == strColumnName )
					return index;
			}

			return -1;
		}


		/// <summary>
		/// the combined width of all of the columns
		/// </summary>
		public int Width
		{
			get
			{
				int nTotalWidth = 0;
				GLColumn col;
				for (int index=0; index<List.Count; index++)
				{
					col = (GLColumn)List[index];
					nTotalWidth += col.Width;
				}

				return nTotalWidth;
			}
		}
		#endregion

		#region Functionality

		public int GetSpanSize( string strStartColumnName, int nColumnsSpanned )
		{
			int nStartColumn = GetColumnIndex( strStartColumnName );

			int nSpanSize = 0;

			if ( (nColumnsSpanned+nStartColumn) > Count )
				nColumnsSpanned = (Count-nStartColumn);

			for ( int nIndex = nStartColumn; nIndex<(nStartColumn+nColumnsSpanned); nIndex++ )
				nSpanSize += this[nIndex].Width;

			return nSpanSize;
		}


		public void Add( GLColumn newColumn )
		{
			//item.ChangedEvent += new BSLItem.ChangedEventHandler( BSLItem_Changed );				// listen to event changes inside the item
			newColumn.ChangedEvent += new GLColumn.ChangedEventHandler( GLColumn_Changed );

			while ( GetColumnIndex( newColumn.Name ) != -1 )
				newColumn.Name += "x";					// change the name till it is not the same

			int nIndex = List.Add( newColumn );

			if ( ChangedEvent != null )
				ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnCollectionChanged, newColumn.Name ) );				// fire the column clicked event
		}

		public void Add( string strColumnName, string strPropriete, int nColumnWidth )
		{
			GLColumn newColumn = new GLColumn();
			newColumn.Name = strColumnName;
            newColumn.Text = strColumnName;
			newColumn.Width = nColumnWidth;
			newColumn.State = ColumnStates.csPressed;
			newColumn.TextAlignment = ContentAlignment.MiddleLeft;
			newColumn.Propriete = strPropriete;

			Add( newColumn );
		}

		public void Add( string strColumnName, string strPropriete, int nColumnWidth, HorizontalAlignment align )
		{
			GLColumn newColumn = new GLColumn();
			newColumn.Name = strColumnName;
			newColumn.Width = nColumnWidth;
			newColumn.State = ColumnStates.csNone;
			newColumn.TextAlignment = ContentAlignment.MiddleLeft;
			newColumn.Propriete = strPropriete;

			Add( newColumn );
		}


		public void AddRange( GLColumn[] columns)
		{
			lock(List.SyncRoot)
			{
				for (int i=0; i<columns.Length; i++)
					Add( columns[i] );
			}
		}

		public void Remove( int nColumnIndex )
		{
			if ( ( nColumnIndex >= this.Count ) || (nColumnIndex < 0) )
				return;			// error

			List.RemoveAt( nColumnIndex );

			if ( ChangedEvent != null )
				ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnCollectionChanged, "" ) );				// fire the column clicked event
		}

		public new void Clear()
		{
			List.Clear();

			if ( ChangedEvent != null )
				ChangedEvent( this, new ChangedEventArgs( ChangedTypes.ColumnCollectionChanged, "" ) );				// fire the column clicked event
		}

		public int IndexOf( GLColumn column )
		{
			return List.IndexOf( column );
		}

		public void ClearStates()
		{
			foreach ( GLColumn column in List )
				column.State = ColumnStates.csNone;
		}

		public void ClearHotStates()
		{
			foreach ( GLColumn column in List )
			{
				if ( column.State == ColumnStates.csHot )
					column.State = ColumnStates.csNone;
			}
		}

		/// <summary>
		/// if any of the columns are in a pressed state then disable all hotting
		/// </summary>
		/// <returns></returns>
		public bool AnyPressed()
		{
			foreach ( GLColumn column in List )
				if ( column.State == ColumnStates.csPressed )
					return true;

			return false;
		}

		#endregion

		#region Membres de I2iSerializable

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			int nNb = 0;
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					nNb = Count;
					serializer.TraiteInt ( ref nNb );
					foreach ( GLColumn col in this )
					{
						result = col.Serialize ( serializer );
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					Clear();
					nNb = 0;
					serializer.TraiteInt ( ref nNb );
					for ( int n = 0; n < nNb; n++ )
					{
						GLColumn col = new GLColumn();
						result = col.Serialize ( serializer );
						if ( !result )
							return result;
						Add ( col );
					}
					break;
			}
			return result;
		}

		#endregion
	}


	#region Collection Editors

	/// <summary>
	/// Class created so we can force an invalidation/update on the control when the column editor returns
	/// </summary>
	public class CustomCollectionEditor : CollectionEditor
	{
		private int m_nUnique = 1;

		public CustomCollectionEditor(Type type) : base(type)
		{
			
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			GlacialList originalControl = (GlacialList)context.Instance;

			object returnObject = base.EditValue( context, isp, value );

			originalControl.Refresh();//.Invalidate( true );
			return returnObject;
		}

		protected override object CreateInstance(Type itemType)
		{
			// here we are making sure that we generate a unique column name every time
			object[] cols;
			string strTmpColName;
			do
			{
				strTmpColName = "Column" + m_nUnique.ToString();
				cols = this.GetItems( strTmpColName );

				m_nUnique++;
			} while ( cols.Length != 0 );

			// instance the column and set its ident name
			object col = base.CreateInstance (itemType);
			((GLColumn)col).Name = strTmpColName;

			return col;
		}


	}

	/// <summary>
	/// GLColumnConverter
	/// </summary>
	/// 
	public class GLColumnConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is GLColumn)
			{
				GLColumn column = (GLColumn)value;
              
				ConstructorInfo ci = typeof(GLColumn).GetConstructor(new Type[] {});
				if (ci != null)
				{
					return new InstanceDescriptor(ci, null, false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	#endregion

}
