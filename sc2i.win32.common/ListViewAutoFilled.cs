using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

using Microsoft.Win32;

using sc2i.common;

namespace sc2i.win32.common
{
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region classe ListViewAutoFilled
	/// <summary>
	/// Description résumée de ListViewAutoFilled.
	/// </summary>
	[Serializable]
	public class ListViewAutoFilled : ListView
	{
		protected Thread m_threadRemplissage = null;
		protected IList m_listeAssociee = null;
		protected ListViewAutoFilledColumnsCollection m_listColonnes;
		protected CInfoStructureDynamique m_structureObjets = null;
		private int m_nOldWith;

		//permet d'activer ou de désactiver la customisation par l'utilisateur de la liste
		private bool m_bEnableCustomisationUser = true;

		private int m_nNumColumn = -1;
		private bool m_bAscending = true;

		//-------------------------------------------------------------------
		protected  override void Dispose( bool bDisposing)
		{
			if ( bDisposing )
			{
				StopRemplissage();
			}
		}
		//-------------------------------------------------------------------
		public ListViewAutoFilled()
		{
			m_listColonnes = new ListViewAutoFilledColumnsCollection(this);
			this.View = View.Details;
			this.FullRowSelect = true;
			this.ResizeRedraw = true;
			
			this.ContextMenu = new ContextMenu();
			ContextMenu.Popup += new EventHandler(ContexMenuPopUp);
			//Events
			this.SizeChanged += new EventHandler( SizeChanging );
			this.ColumnClick += new ColumnClickEventHandler (TriSurColumnClick);
		}
		//-------------------------------------------------------------------
		public void SizeChanging(object sender, EventArgs e)
		{
			int nColumnsProportionnalCount = 0;
			foreach(ListViewAutoFilledColumn colonne in this.Columns)
			{
				if (colonne.ProportionnalSize)
				{
					nColumnsProportionnalCount++;
				}

			}
			foreach(ListViewAutoFilledColumn colonne in this.Columns)
			{
				//TODO A vérifier...
				if (colonne.ProportionnalSize)
				{
					if ( Math.Abs(colonne.PrecisionWidth - (Double)colonne.Width) > 1.0 )
						colonne.PrecisionWidth = colonne.Width;
					Double fTempValue = colonne.PrecisionWidth + (((Double)this.Width-(Double)m_nOldWith)/nColumnsProportionnalCount);
					if (fTempValue > 30.0)
						colonne.PrecisionWidth = fTempValue;
						colonne.Width = (int) fTempValue ;
				}
			}

			m_nOldWith = this.Width;
		}
		//-------------------------------------------------------------------
		public void TriSurColumnClick(object Sender, ColumnClickEventArgs e)
		{
			if (e.Column == m_nNumColumn)
			{
				m_bAscending = ! m_bAscending;
			}
			else
			{
				m_bAscending = true;
			}
			m_nNumColumn = e.Column;
			this.ListViewItemSorter = new sc2i.win32.common.CTrieurListView(e.Column, m_bAscending);
		}
		//-------------------------------------------------------------------
		public virtual bool ShouldSerializeColonnes()
		{
			return true;
		}
		//-------------------------------------------------------------------
		public virtual void ResetColonnes()
		{}
		//-------------------------------------------------------------------
		//[Editor(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public ListViewAutoFilledColumnsCollection Colonnes
		{
			get
			{
				return m_listColonnes;
			}
			set
			{
				m_listColonnes = value;
			}
		}

		//-------------------------------------------------------------------
		private void RemplirListe()
		{
			try
			{
				if ( m_listeAssociee == null )
				{
					return;
				}

				Items.Clear();

				ArrayList lst = new ArrayList();
				foreach ( object obj in m_listeAssociee )
				{
					ListViewItem item = new ListViewItem();
					try
					{
						UpdateItemWithObject(item,obj);
						lst.Add ( item );
						if ( lst.Count >90 )
						{
							this.Items.AddRange ( (ListViewItem[])lst.ToArray ( typeof(ListViewItem) ));
							lst.Clear();
						}
					}
					catch(Exception e )
					{
						string strMes = e.ToString();
					}
				}
				this.Items.AddRange ( (ListViewItem[])lst.ToArray ( typeof(ListViewItem) ));
			}
			catch
			{}
			
		}

		//-------------------------------------------------------------------
		public override void Refresh()
		{
			base.Refresh ();
			RemplirListe();
		}


		//-------------------------------------------------------------------
		private void StopRemplissage()
		{
			if ( m_threadRemplissage != null )
			{
				try
				{
					m_threadRemplissage.Abort();
				}
				catch
				{
				}
				m_threadRemplissage = null;
			}
		}

		//-------------------------------------------------------------------
		public void Remplir ( IList liste )
		{
			Remplir ( liste, false );
		}

		//-------------------------------------------------------------------
		public void Remplir(IList liste, bool bEnableMultiThread)
		{
			StopRemplissage();
			Items.Clear();
			m_listeAssociee = liste;
			if ( m_listeAssociee == null )
				return;
			if ( liste.Count < 35 || !bEnableMultiThread)
			{
				BeginUpdate();
				RemplirListe();
				EndUpdate();
			}
			else
			{
				//Remplissage en tache de fond
				m_threadRemplissage = new Thread(new ThreadStart(RemplirListe));
				m_threadRemplissage.Start();
			}
		}


		//-------------------------------------------------------------------
		public void UpdateItemWithObject(ListViewItem item, object obj)
		{
			if (item == null || obj == null)
				return;
			item.Tag = obj;

			while ( item.SubItems.Count < this.Columns.Count)
			{
				item.SubItems.Add("");
			}
			ArrayList lstColonneesToDel = new ArrayList();
			for(int i=0; i<this.Columns.Count; i++)
			{
				string strField = m_listColonnes[i].Field;
				if (strField != "")
				{
					try
					{
						//bool bIsChampValide = true;
						string strText = CInfoStructureDynamique.GetDonneeDynamiqueString ( obj, strField, "" );
						//string strText = CInterpreteurTextePropriete.GetStringValue ( obj, strField, "", ref bIsChampValide );
						strText = strText.Replace("\r","");
						strText = strText.Replace("\n", " ,");
						strText = strText.Replace("\t","");
						item.SubItems[i].Text = strText;
						//if ( !bIsChampValide )
						//	lstColonneesToDel.Add ( m_listColonnes[i] );
					}
					catch
					{
						item.SubItems[i].Text = I.T("Unknown value|10013");
					}
				}
			}
			//foreach ( ListViewAutoFilledColumn col in lstColonneesToDel )
			//	RemoveColonne ( col );
		}
		//-------------------------------------------------------------------
		public void AddColonne(string strEnTete, string strField, int nWidth)
		{
			ListViewAutoFilledColumn colonne = new ListViewAutoFilledColumn();
			colonne.Text = strEnTete;
			colonne.Field = strField;
			colonne.Width = nWidth;
			this.Columns.Add(colonne);
		}

		/*//-------------------------------------------------------------------
		private void MenuItemClick(object sender, EventArgs e)
		{
			if(sender is MenuItem)
			{
				MenuItem menuItem = ((MenuItemWithTag) sender);
				bool bCheck = !menuItem.Checked;
				menuItem.Checked = bCheck;
				if ( bCheck )
				{
					if ( menuItem.Tag is ListViewAutoFilledColumn )
						((ListViewAutoFilledColumn)menuItem.Tag).Visible = true;
					else
					{
						if ( menuItem.Tag is CInfoChampMenu )
						{
							CInfoChampDynamique info = (CInfoChampMenu)menuItem.Tag;
							string strChamp = info.NomPropriete;
							ListViewAutoFilledColumn col = new ListViewAutoFilledColumn();
							col.Field = strChamp;
							col.Visible = true;
							col.Text = info.NomChamp;
							if ( typeof(double).IsAssignableFrom ( info.TypeDonnee ) )
								col.TextAlign = HorizontalAlignment.Right;
							Colonnes.Add ( col );
							FillColonne(col);
						}
					}
				}
				else
				{
					if ( menuItem.Tag is ListViewAutoFilledColumn )
					{
						RemoveColonne ( (ColumnHeader)menuItem.Tag );
					}
				}					
			}
		}*/

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
					ListViewAutoFilledColumn col = new ListViewAutoFilledColumn();
					col.Field = strChamp;
					col.Text = infoChamp.Nom;
					if ( typeof(double).IsAssignableFrom(infoChamp.TypeChamp ) )
						col.TextAlign = HorizontalAlignment.Right;
					Colonnes.Add ( col );
					FillColonne ( col );
				}
				else
				{
					if ( menuItem.Tag is CInfoChampMenu )
					{
						string strChamp = ((CInfoChampMenu)menuItem.Tag).Propriete;
						foreach ( ListViewAutoFilledColumn col in Colonnes )
						{
							if ( col.Field == strChamp )
							{
								RemoveColonne ( col );
								break;
							}
						}
					}
				}					
			}
		}

		//-------------------------------------------------------------------
		private void RemoveColonne ( ColumnHeader col )
		{
			int nIndex = col.Index;
			Colonnes.Remove ( col );
			foreach( ListViewItem item in Items )
				item.SubItems.RemoveAt ( nIndex );
		}

		//-------------------------------------------------------------------
		private void FillColonne ( ListViewAutoFilledColumn col )
		{
			int nNum = col.Index;
			foreach ( ListViewItem item in Items )
			{
				while ( item.SubItems.Count <= nNum )
					item.SubItems.Add ( "" );
				//string strText = CInterpreteurTextePropriete.GetStringValue(item.Tag, col.Field, "" ).ToString();
				string strText = CInfoStructureDynamique.GetDonneeDynamiqueString ( item.Tag, col.Field, "" );
				strText = strText.Replace("\r"," ,");
				strText = strText.Replace("\n", " ");
				item.SubItems[nNum].Text = strText;
			}
		}
		//-------------------------------------------------------------------
		private void ContexMenuPopUp(object sender, EventArgs e)
		{
			if ( !m_bEnableCustomisationUser )
				return;

			if ( m_structureObjets == null && Items.Count>0)
				m_structureObjets = CInfoStructureDynamique.GetStructure(Items[0].Tag.GetType(), 1);
			if ( m_structureObjets == null )
				return;

			//Crée une Hashtable des colonnes affichées
			Hashtable tableColonnes = new Hashtable();
			foreach ( ListViewAutoFilledColumn col in Colonnes)
				tableColonnes[col.Field] = col;

			bool bNouveauMenu = ContextMenu.MenuItems.Count == 0;

			if ( bNouveauMenu )
			{
				FillMenu(ContextMenu, m_structureObjets, tableColonnes, "");
				MenuItem item = new MenuItem("Plus de champs", new EventHandler(OnSelectionnerLesChamps));
				ContextMenu.MenuItems.Add ( item );
			}
			UpdateCheckState ( ContextMenu, tableColonnes );
			Point pt = Cursor.Position;
			if ( e is MouseEventArgs )
				pt = new Point ( ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y );
			//m_popupMenu.Show ( this, pt );

		}

			/*if ( m_structureObjets == null && Items.Count>0)
				m_structureObjets = CInfoStructureDynamique.GetStructure(Items[0].Tag.GetType(), 4);
			if ( m_structureObjets == null )
				return;

			//Crée une Hashtable des colonnes affichées
			Hashtable tableColonnes = new Hashtable();
			foreach ( ListViewAutoFilledColumn col in Colonnes )
				tableColonnes[col.Field] = col;
			FillMenu(ContextMenu, m_structureObjets, tableColonnes, "");*/

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




		//-------------------------------------------------------------------
		private void OnSelectionnerLesChamps ( object sender, EventArgs args )
		{
			ArrayList lst = new ArrayList();
			Hashtable tableAvant = new Hashtable();
			foreach ( ListViewAutoFilledColumn col in Colonnes )
			{
				lst.Add ( col.Field );
				tableAvant[col.Field] = col;
			}
			CInfoChampDynamique [] lstDecoches = null;
			CInfoChampDynamique[] champs = CFormSelectChampParentPourStructure.SelectProprietes ( m_structureObjets, (string[])lst.ToArray(typeof(string)), ref lstDecoches );
			if ( champs == null || champs.Length == 0 )
				return;
			foreach ( CInfoChampDynamique infoChamp in champs )
			{
				string strChamp = infoChamp.NomPropriete;
				if ( tableAvant[strChamp] == null )
				{
					ListViewAutoFilledColumn col = new ListViewAutoFilledColumn();
					col.Field = strChamp;
					col.Visible = true;
					col.Text = infoChamp.NomChamp;
					if ( typeof(double).IsAssignableFrom(infoChamp.TypeDonnee ) )
						col.TextAlign = HorizontalAlignment.Right;
					Colonnes.Add ( col );
					FillColonne(col);
				}
				tableAvant.Remove ( strChamp );
			}
			if ( lstDecoches != null )
			{
				foreach ( CInfoChampDynamique champ in lstDecoches )
				{
					foreach ( ListViewAutoFilledColumn oldCol in tableAvant.Values )
						if ( oldCol.Field == champ.NomChamp )
						{
							RemoveColonne ( oldCol );
							break;
						}
				}
			}
		}

		/*//-------------------------------------------------------------------
		private void FillMenu ( Menu menu, CInfoStructureDynamique structure, Hashtable tableColonnes,string strRacine )
		{
			//Niveau 0
			foreach ( CInfoChampDynamique champ in structure.Champs )
			{
				if ( champ.StructureValeur == null )
				{
					MenuItemWithTag menuItem = new MenuItemWithTag(champ.NomChamp, new EventHandler(MenuItemClick));
					if ( tableColonnes[strRacine+champ.NomPropriete] != null )
					{
						ListViewAutoFilledColumn col = (ListViewAutoFilledColumn)tableColonnes[strRacine+champ.NomPropriete];
						menuItem.Tag = col;
						menuItem.Checked = col.Visible;
					}
					else
					{
						menuItem.Tag = strRacine+champ.NomPropriete;
						menuItem.Checked = false;
					}
					menu.MenuItems.Add ( menuItem );
				}
			}
			//Niveaux suivants
			foreach ( CInfoChampDynamique champ in structure.Champs )
			{
				if ( champ.StructureValeur != null )
				{
					MenuItem sousMenu = new MenuItem(champ.NomChamp);
					FillMenu ( sousMenu, champ.StructureValeur, tableColonnes, strRacine+champ.NomPropriete+".");
					menu.MenuItems.Add ( sousMenu );
				}
			}

		}*/


		//-------------------------------------------------------------------
		public void ReadFromRegistre(RegistryKey key)
		{
			if ( key == null )
				return;
			byte[] data = (byte[]) key.GetValue("Colonnes");
			if (data==null)
				return;
			MemoryStream stream = new MemoryStream(data);
			this.Colonnes.ReadFromStream(stream);
		}
		//-------------------------------------------------------------------
		public void WriteToRegistre(RegistryKey key)
		{
			MemoryStream stream = new MemoryStream();
			this.Colonnes.WriteToStream(stream);
			key.SetValue("Colonnes", stream.ToArray());
		}
		//-------------------------------------------------------------------
		private void InitializeComponent()
		{
			// 
			// ListViewAutoFilled
			// 
			this.HideSelection = false;
			this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.TriSurColumnClick);

		}

		//-------------------------------------------------------------------
		public bool EnableCustomisation
		{
			get
			{
				return m_bEnableCustomisationUser;
			}
			set
			{
				m_bEnableCustomisationUser = value;
			}
		}
	}
	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region classe ListViewAutoFilledColumn
	/// <summary>
	/// Description résumée de ListViewAutoFilledColumn.
	/// </summary>
	public class ListViewAutoFilledColumn : ColumnHeader
	{
		private string m_strField;
		private bool m_bProportionnalSize;
		private Double m_fPrecisionWidth;
		private bool m_bVisible = true;
		private int nOldWidth = 120;
		//-------------------------------------------------------------------
		
		public ListViewAutoFilledColumn()
		:base()
		{
			m_strField = "";
			m_bProportionnalSize = false;
			Width = 120;
		}
		
		//-------------------------------------------------------------------
		private void InitializeComponent()
		{
		}
		//-------------------------------------------------------------------
		public int GetNumVersion()
		{
			return 1;
		}
		//-------------------------------------------------------------------
		public string Field
		{
			get
			{
				return m_strField;
			}
			set
			{
				m_strField = value;
			}
		}
		//-------------------------------------------------------------------
		public bool ShouldSerializeProportionnalSize()
		{
			return true;
		}
		//-------------------------------------------------------------------
		public bool ProportionnalSize
		{
			get
			{
				return m_bProportionnalSize;
			}
			set
			{
				m_bProportionnalSize = value;
			}
		}
		//-------------------------------------------------------------------
		public Double PrecisionWidth
		{
			get
			{
				return m_fPrecisionWidth;
			}
			set
			{
				m_fPrecisionWidth = value;
			}
		}
		//-------------------------------------------------------------------
		public bool Visible
		{
			get
			{
				return m_bVisible;
			}
			set
			{
				if (!value)
				{
					nOldWidth = this.Width;
					this.Width = 0;
				}
				else
					this.Width = nOldWidth;

				m_bVisible = value;
			}
		}
		//-------------------------------------------------------------------
		/*
		public struct LVCOLUMN
		{
			public long mask ;
			public long fmt ;
			public long cx ;
			public string pszText  ;
			public long cchTextMax ;
			public long iSubItem ;
			public long iImage ;
			public long iOrder ;
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(int hWnd, uint Msg, int wParam, ref LVCOLUMN pCol);
		
		public int Position
		{
			get
			{
				LVCOLUMN pCol = new LVCOLUMN();
				ListView lv = this.ListView;
				
				SendMessage(
					(int) lv.Handle, 
					(uint) WindowsMessages.LVM_GETCOLUMNA, 
					this.Index, 
					ref pCol);
				return pCol.iOrder;
			}
			set
			{
				
				ListView lv = this.ListView;
				CWindowsMessages.SendMessage(
					(int) lv.Handle, 
					(uint) WindowsMessages.LVM_SETCOLUMNA, 
					this.Index, 
					ref value);
					
			}
		}
		*/
		//-------------------------------------------------------------------
		public void Read(MemoryStream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			
			int nNumVersion = (int) bf.Deserialize(stream);
			this.Text = (string) bf.Deserialize(stream);
			this.Field = (string) bf.Deserialize(stream);
			this.Width = (int) bf.Deserialize(stream);
			if (nNumVersion<1)
				return;
			TextAlign = (HorizontalAlignment)bf.Deserialize(stream);
			//this.Position = (int) bf.Deserialize(stream);

		}
		//-------------------------------------------------------------------
		public void Write(MemoryStream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();

			bf.Serialize(stream, this.GetNumVersion());
			bf.Serialize(stream, this.Text);
			bf.Serialize(stream, this.Field);
			bf.Serialize(stream, this.Width);
			if (this.GetNumVersion()<1)
				return;
			bf.Serialize ( stream, this.TextAlign );
			//bf.Serialize(stream, this.Position);
		}
		//-------------------------------------------------------------------
	}
	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region classe ListViewAutoFilledColumnsCollection
	/// <summary>
	/// Description résumée de ListViewAutoFilledColumnsCollection.
	/// </summary>
	public class ListViewAutoFilledColumnsCollection : ListView.ColumnHeaderCollection
	{
		//private ArrayList m_listeColonnes = new ArrayList();

		public ListViewAutoFilledColumnsCollection(ListView listview)
			:base(listview)
		{}
		//-------------------------------------------------------------------
		public int GetNumVersion()
		{
			return 0;
		}
		//-------------------------------------------------------------------
		public new ListViewAutoFilledColumn this[int nIndex]
		{
			get
			{ 
				ListViewAutoFilledColumn colonne = new ListViewAutoFilledColumn();
				try
				{
					colonne = (ListViewAutoFilledColumn) base[nIndex];
				}
				catch
				{
					System.Diagnostics.Debug.WriteLine("Column at index " + nIndex + " does not exist.");
				}
				return colonne;
			}
			/*set 
			{ 
				base[nIndex] = value;
				//m_listeColonnes[nIndex] = value;
			}*/
		}
		/*//-------------------------------------------------------------------
		public new ListViewAutoFilledColumn Add( string str, int width, HorizontalAlignment textAlign )
		{
			ListViewAutoFilledColumn col = (ListViewAutoFilledColumn) base.Add( str, width, textAlign );
			return col;
		}*/
		/*//-------------------------------------------------------------------
		public override int Add(ColumnHeader colonne)
		{
			return m_listeColonnes.Add(colonne);
			//return base.Add(colonne);
		}

		//-------------------------------------------------------------------*/
		public virtual void AddRange(ListViewAutoFilledColumn[] values)
		{
			base.AddRange(values);
		}
		//-------------------------------------------------------------------
		public void ReadFromStream(MemoryStream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			int nNbVersion = (int) bf.Deserialize(stream);
			int nCount = (int) bf.Deserialize(stream);
			this.Clear();
			for(int i=0; i<nCount; i++)
			{
				ListViewAutoFilledColumn col = new ListViewAutoFilledColumn();
				col.Read(stream);
				if (col.Width>0)
					this.Add(col);
			}
		}
		//-------------------------------------------------------------------
		public void WriteToStream(MemoryStream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream, GetNumVersion() );
			bf.Serialize(stream, this.Count );
			foreach(ListViewAutoFilledColumn col in this)
			{
				col.Write(stream);
			}
		}
		//-------------------------------------------------------------------
	}
	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
