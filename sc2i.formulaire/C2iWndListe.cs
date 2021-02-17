using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Data;

using sc2i.common;
using sc2i.expression;
using sc2i.drawing;
using System.ComponentModel;




namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iPanel.
	/// </summary>
	[WndName("Liste")]
	[AWndIcone("ico_panel")]
	[Serializable]
	public class C2iWndListe : C2iWndComposantFenetre
	{

		//Attention, copie des valeurs de GlacialList
		public enum GLGridStyles 
		{ 
			gridNone = 0, 
			gridSolid = 1 
		}

		private string c_strColObjet = "2I_OBJECT";

		private C2iExpression m_formuleSourceDonnees;

		private C2iExpression m_formuleCodeInitialisation;

		private List<CColonne> m_colonnes= new List<CColonne>();
		private bool m_bRemplirSurDemande = false;

		private GLGridStyles m_lineStyle = GLGridStyles.gridSolid;

        private bool m_bOptimizeRefresh = true;

		//Colors de la grid
		private Color m_lineBackColor2 = Color.White;
		private Color m_lineBackColor1 = Color.White;
		private Color m_colorGridColor = Color.Gray;
		private Color m_colorSelectionColor = Color.DarkBlue;
		private Color m_colorBackTotaux = Color.LightBlue;
		private Color m_colorTextTotaux = Color.Black;

		//Header
		private bool m_bHeaderVisible = true;
		private Color m_colorHeaderText = Color.Black;
		private Color m_colorHeaderBack = Color.White;
		private Font m_fontHeader = null;
		
		
		private int m_nItemHeight = 18;
		private bool m_bShowBorder = true;

		private bool m_bTotalGlobal = false;


		#region classe CColonne
		public class CColonne : I2iSerializable
		{
			private string m_strTitle = "";
			private C2iExpression m_expression = null;
			private bool m_bGrouper = false;
			private CActionSur2iLink m_actionLink = null;
			private int m_nWidth = 100;
			private string m_strLibelleTotal = I.T("Total|100");
			private OperationsAgregation m_operationAgregation = OperationsAgregation.None;
			private Color  m_couleurFond = Color.Transparent;
			private Color m_couleurText = Color.Transparent;
			private Font m_font = null;

			//----------------------------------------------
			public CColonne()
			{
			}

			//----------------------------------------------
			public string Title
			{
				get
				{
					return m_strTitle;
				}
				set
				{
					m_strTitle = value;
				}
			}

			//----------------------------------------------
			public C2iExpression FormuleDonnee
			{
				get
				{
					return m_expression;
				}
				set
				{
					m_expression = value;
				}
			}

			//----------------------------------------------
			public bool Grouper
			{
				get
				{
					return m_bGrouper;
				}
				set
				{
					m_bGrouper = value;
				}
			}

			//----------------------------------------------
			public CActionSur2iLink ActionSurLink
			{
				get
				{
					return m_actionLink;
				}
				set
				{
					m_actionLink = value;
				}
			}

			
			//----------------------------------------------
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

			//----------------------------------------------
			/// <summary>
			/// Opération pour les groupements
			/// </summary>
			public OperationsAgregation OperationAgregation
			{
				get
				{
					return m_operationAgregation;
				}
				set
				{
					m_operationAgregation = value;
				}
			}

			//----------------------------------------------
			/// <summary>
			/// Libellé utilisé pour les totaux si la colonne est groupby
			/// </summary>
			public string LibelleTotal
			{
				get
				{
					return m_strLibelleTotal;
				}
				set
				{
					m_strLibelleTotal = value;
				}
			}

			//----------------------------------------------
			public Color BackColor
			{
				get
				{
					return m_couleurFond;
				}
				set
				{
					m_couleurFond = value;
				}
			}

			//----------------------------------------------
			public Color TextColor
			{
				get
				{
					return m_couleurText;
				}
				set
				{
					m_couleurText = value;
				}
			}

			//----------------------------------------------
			public Font Font
			{
				get
				{
					return m_font;
				}
				set
				{
					m_font = value;
				}
			}


			//----------------------------------------------
			public override string ToString()
			{
				return m_strTitle;
			}

			#region I2iSerializable Membres
			private int GetNumVersion()
			{
				return 4;
				/*Version 1
				 * remplacement de la propriété par une formule
				 * Ajout de grouper
				 * Ajout du lien
				 */
				/*Version 2 :
				 * Ajout de l'opération d'agrégation, des couleurs 
				 * */
				/*Version 3 : ajout de la font*/
				//Version 4 : Ajout de libellé total
			}

			public CResultAErreur Serialize(C2iSerializer serializer)
			{
				int nVersion = GetNumVersion();
				CResultAErreur result = serializer.TraiteVersion(ref nVersion);
				if (!result)
					return result;
				if (nVersion <= 0)
				{
					string strProp = "";
					serializer.TraiteString(ref strProp);
				}
				serializer.TraiteString(ref m_strTitle);
				serializer.TraiteInt(ref m_nWidth);
				if (nVersion >= 1)
				{
					I2iSerializable tmp = m_expression;
					serializer.TraiteObject(ref tmp);
					if (tmp is C2iExpression)
						m_expression = (C2iExpression)tmp;

					tmp = m_actionLink;
					serializer.TraiteObject(ref tmp);
					if (tmp is CActionSur2iLink)
						m_actionLink = (CActionSur2iLink)tmp;
					serializer.TraiteBool(ref m_bGrouper);
				}
				if (nVersion >= 2)
				{
					int nVal = BackColor.ToArgb();
					serializer.TraiteInt(ref nVal);
					BackColor = Color.FromArgb(nVal);

					nVal = TextColor.ToArgb();
					serializer.TraiteInt(ref nVal);
					TextColor = Color.FromArgb(nVal);

					nVal = (int)this.OperationAgregation;
					serializer.TraiteInt(ref nVal);
					this.OperationAgregation = (OperationsAgregation)nVal;
				}
				if (nVersion >= 3)
					result = C2iWnd.SerializeFont(serializer, ref m_font);
				if (nVersion >= 4)
					serializer.TraiteString(ref m_strLibelleTotal);
				return result;
			}

			#endregion
		}
		#endregion

		public C2iWndListe()
            :base()
		{
            LockMode = ELockMode.Independant;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		//----------------------------------------------
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression SourceFormula
		{
			get
			{
				return m_formuleSourceDonnees;
			}
			set
			{
				m_formuleSourceDonnees = value;
			}
		}

		//----------------------------------------------
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression InitialisationCode
		{
			get
			{
				return m_formuleCodeInitialisation;
			}
			set
			{
				m_formuleCodeInitialisation = value;
			}
		}

		/// ///////////////////////
		[System.ComponentModel.Editor(typeof(CListeColonnesEditor), typeof(UITypeEditor))]
		public List<CColonne> Columns
		{
			get
			{
				if (m_colonnes == null)
					m_colonnes = new List<CColonne>();
				return m_colonnes;
			}
			set
			{
				m_colonnes = value;
			}
		}

        //----------------------------------------------
        //si true le contrôle ne sera rafraichit que sur changement de la source
        public bool OptimizeRefresh
        {
            get
            {
                return m_bOptimizeRefresh;
            }
            set
            {
                m_bOptimizeRefresh = value;
            }
        }
            

		/// ///////////////////////
		public bool FillOnDemand
		{
			get
			{
				return m_bRemplirSurDemande;
			}
			set
			{
				m_bRemplirSurDemande = value;
			}
		}

		/// ///////////////////////
		public bool GlobalTotal
		{
			get
			{
				return m_bTotalGlobal;
			}
			set
			{
				m_bTotalGlobal = value;
			}
		}


		/// ///////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
			//rect = contexte.ConvertToAbsolute(rect);
			g.FillRectangle(b, rect);
			b.Dispose();
			DrawCadre ( g );
			base.MyDraw ( ctx );
		}



		/// ///////////////////////
		public GLGridStyles LineStyle
		{
			get
			{
				return m_lineStyle;
			}
			set
			{
				m_lineStyle = value;
			}
		}

		/// ///////////////////////
		public int ItemHeight
		{
			get
			{
				return m_nItemHeight;
			}
			set
			{
				m_nItemHeight = value;
			}
		}

		/// ///////////////////////
		public bool ShowBorder
		{
			get
			{
				return m_bShowBorder; ;
			}
            set
			{
				m_bShowBorder = value;
			}
		}

		/// ///////////////////////
		public bool HeaderVisible
		{
			get
			{
				return m_bHeaderVisible;
			}
			set
			{
				m_bHeaderVisible = value;
			}
		}

		/// ///////////////////////
		public Color LineBackColor1
		{
			get
			{
				return m_lineBackColor1;
			}
			set
			{
				m_lineBackColor1 = value;
			}
		}

		/// ///////////////////////
		public Color LineBackColor2
		{
			get
			{
				return m_lineBackColor2;
			}
			set
			{
				m_lineBackColor2 = value;
			}
		}

		/// ///////////////////////
		public Color ColorGrid
		{
			get
			{
				return m_colorGridColor;
			}
			set
			{
				m_colorGridColor = value;
			}
		}

		/// ///////////////////////
		public Color ColorSelection
		{
			get
			{
				return m_colorSelectionColor;
			}
			set
			{
				m_colorSelectionColor = value;
			}
		}

		/// ///////////////////////
		public Color TotalBackColor
		{
			get
			{
				return m_colorBackTotaux;
			}
			set
			{
				m_colorBackTotaux = value;
			}
		}

		/// ///////////////////////
		public Color TotalTextColor
		{
			get
			{
				return m_colorTextTotaux;
			}
			set
			{
				m_colorTextTotaux = value;
			}
		}

		/// ///////////////////////
		public Color ColorHeaderText
		{
			get
			{
				return m_colorHeaderText;
			}
			set
			{
				m_colorHeaderText = value;
			}
		}

		/// ///////////////////////
		public Color ColorHeaderBack
		{
			get
			{
				return m_colorHeaderBack;
			}
			set
			{
				m_colorHeaderBack = value;
			}
		}

		//-------------------------------------
		public Font HeaderFont
		{
			get
			{
				return m_fontHeader;
			}
			set
			{
				m_fontHeader = value;
			}
		}

		/// ///////////////////////
		public string GetNomColObject()
		{
			return c_strColObjet;
		}

		/// <summary>
		/// Prépare une liste de données d'agrégation pour la liste de colonnes
		/// demandée (interne)
		/// </summary>
		/// <param name="colonnes"></param>
		/// <returns></returns>
		private List<CDonneeAgregation> GetNewListeAgregration(List<CColonne> colonnes)
		{
			List<CDonneeAgregation> lst = new List<CDonneeAgregation>();
			foreach (CColonne col in colonnes)
			{
				CDonneeAgregation donnee = CDonneeAgregation.GetNewDonneeForOperation(col.OperationAgregation);
				donnee.PrepareCalcul();
				lst.Add(donnee);
			}
			return lst;
		}

		/// <summary>
		/// Crée une ligne dans la table avec les données agregées
		/// </summary>
		/// <param name="strChampGroupBy"></param>
		/// <param name="lstDonnees"></param>
		/// <param name="colsAgregations"></param>
		/// <param name="table"></param>
		private void InsereLigneAgregation(
			string strChampGroupBy,
			List<CDonneeAgregation> lstDonnees,
			List<CColonne> colsAgregations,
			DataTable table)
		{
			if (colsAgregations.Count != 0)//Si pas d'agrégat, pas de ligne d'agrégat
			{
				CColonne col = GetColonne(strChampGroupBy);
				if (col == null && strChampGroupBy != "")
					return;
				DataRow row = table.NewRow();
				if (col != null)
					row[strChampGroupBy] = col.LibelleTotal;
				for (int nCol = 0; nCol < colsAgregations.Count; nCol++)
				{
					row[colsAgregations[nCol].Title] = lstDonnees[nCol].GetValeurFinale();
					lstDonnees[nCol].PrepareCalcul();
				}
				table.Rows.Add(row);
			}
		}

		/// <summary>
		/// Retourne un dataview remplit avec les données à afficher.
		/// Le dataview contient une colonne GetNomColObject qui contient
		/// l'objet associé à chaque DataRow
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public DataTable GetDataTable(IEnumerable source)
		{
			DataTable table = new DataTable();
			Dictionary<CColonne, DataColumn> tabColsToDataCols = new Dictionary<CColonne, DataColumn>();
			Dictionary<DataColumn, CColonne> tabDatacolToCols = new Dictionary<DataColumn, CColonne>();

			List<CColonne> listeColsToAgregate = new List<CColonne>();
			string strSort = "";
			
			//Stocke le colonnes sur lesquelles grouper
			Dictionary<string, bool> tabColToGroup = new Dictionary<string, bool>();
			
			//Crée la colonne stockant l'objet
			table.Columns.Add(c_strColObjet, typeof(object));
			
			foreach (CColonne colonne in Columns)
			{
				DataColumn col = new DataColumn(colonne.Title, typeof(string));
				col.AllowDBNull = true;
				table.Columns.Add(col);
				tabColsToDataCols[colonne] = col;
				tabDatacolToCols[col] = colonne;
				if (colonne.Grouper)
				{
					strSort += col.ColumnName + ",";
					tabColToGroup[col.ColumnName] = true;
				}
				if (colonne.OperationAgregation != OperationsAgregation.None &&
					!colonne.Grouper)
					listeColsToAgregate.Add(colonne);
			}
			if (strSort.Length > 0)
				strSort = strSort.Substring(0, strSort.Length - 1);

			foreach (object obj in source)
			{
				if (obj != null)
				{
					DataRow row = table.NewRow();
					row[c_strColObjet] = obj;
					CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(obj);
					foreach (CColonne col in Columns)
					{
						if (col.FormuleDonnee != null)
						{
							CResultAErreur result = col.FormuleDonnee.Eval(ctxEval);
							if (result && result.Data != null)
								row[tabColsToDataCols[col]] = result.Data.ToString();
							else
								row[tabColsToDataCols[col]] = "";
						}
					}
					table.Rows.Add(row);
				}
			}
			//Trie la table
			table.DefaultView.Sort = strSort;
			DataTable newTable = table.Clone();
			DataRowView lastRow = null;

			//Pour chaque champ de groupe, associe une liste de données d'agrégation
			Dictionary<string, List<CDonneeAgregation>> donneesAgg = new Dictionary<string, List<CDonneeAgregation>>();
			if ( GlobalTotal )
				donneesAgg[""] = GetNewListeAgregration(listeColsToAgregate);
			foreach (CColonne col in Columns)
			{
				if (col.Grouper)
					donneesAgg[col.Title] = GetNewListeAgregration(listeColsToAgregate);
			}
			
			foreach (DataRowView row in table.DefaultView)
			{
				DataRow newRow = newTable.NewRow();
				//Recopie les données dans une nouvelle table, en vidant les valeurs qui se suivent sur lesquelles on groupe
				foreach ( DataColumn col in table.Columns )
				{
					if ( lastRow != null )
					{
						if (!tabColToGroup.ContainsKey(col.ColumnName) ||
							!lastRow[col.ColumnName].Equals(row[col.ColumnName]))
						{
							if ( donneesAgg.ContainsKey ( col.ColumnName ) )
								InsereLigneAgregation(col.ColumnName, donneesAgg[col.ColumnName], listeColsToAgregate, newTable);
							newRow[col.ColumnName] = row[col.ColumnName];
						}
						else
							newRow[col.ColumnName] = "";
					}
					else
						newRow[col.ColumnName] = row[col.ColumnName];
				}

				foreach (KeyValuePair<string, List<CDonneeAgregation>> donnees in donneesAgg)
				{
					for (int nCol = 0; nCol < listeColsToAgregate.Count; nCol++)
						donnees.Value[nCol].IntegreDonnee(row[listeColsToAgregate[nCol].Title]);
				}
				lastRow = row;
				newTable.Rows.Add ( newRow );
			}
			foreach ( KeyValuePair<string, List<CDonneeAgregation>> donnees in donneesAgg )
				InsereLigneAgregation(donnees.Key, donnees.Value, listeColsToAgregate, newTable );
			newTable.DefaultView.AllowDelete = false;
			newTable.DefaultView.AllowNew = false;
			return newTable;
		}

		public CColonne GetColonne(string strNom)
		{
			foreach (CColonne col in Columns)
				if (col.Title == strNom)
					return col;
			return null;
		}
		
		/// /////////////////////////////////////////////////
		protected void DrawCadre ( Graphics g )
		{
			Rectangle rect = new Rectangle ( Position, Size);
			Brush br;
			br = new SolidBrush(m_colorHeaderBack);
			int nHeaderSize = 20;
			if ( HeaderVisible )
				g.FillRectangle(br, Position.X + 1, Position.Y + 1, ClientSize.Width - 1, nHeaderSize);
			br.Dispose();
			Pen pen = new Pen ( ColorGrid );
			if ( ShowBorder )
				g.DrawRectangle(pen, rect);
			int nX = 0;
			
			Font ft = new Font ( "Arial", 8);
			
			br = new SolidBrush ( ColorHeaderText );

			if (m_colonnes != null)
			{
				foreach (CColonne col in m_colonnes)
				{
					Rectangle rectText = new Rectangle(nX + 1, 1, col.Width - 2, nHeaderSize - 2);
					rectText.Offset(Position);
					StringFormat format = new StringFormat();
					format.Alignment = StringAlignment.Center;
					if ( HeaderVisible)
						g.DrawString(col.Title, ft, br, rectText, format);
					nX += col.Width;
					g.DrawLine(pen, nX + Position.X, Position.Y, nX + Position.X, Position.Y + nHeaderSize);
				}
			}
			ft.Dispose();
			br.Dispose();
			g.DrawLine(pen, Position.X, Position.Y + nHeaderSize, Position.X + ClientSize.Width, Position.Y + nHeaderSize);
			pen.Dispose();
		}

		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 7;
			//1 : ajout des options de dessin
			//2 : modification des options de dessin pour datagrid
			// 3 : LineBackColor2
			//4 : TotalGeneral
			//5 : Couleurs des totaux
			//6 : Code d'initialisation
            //7 : Optimize refresh
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			string strFormule = C2iExpression.GetPseudoCode ( m_formuleSourceDonnees );
			serializer.TraiteString ( ref strFormule );
			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_formuleSourceDonnees = C2iExpression.FromPseudoCode ( strFormule );

			ArrayList lst = new ArrayList ( m_colonnes );
			serializer.TraiteArrayListOf2iSerializable ( lst, null );
			if ( serializer.Mode == ModeSerialisation.Lecture )
			{
				m_colonnes = new List<CColonne>();
				foreach ( CColonne col in lst )
					if ( col != null )
						m_colonnes.Add ( col );
			}

			serializer.TraiteBool(ref m_bRemplirSurDemande);

			if (nVersion >= 1)
			{
				int nVal;
				if (nVersion < 2)
				{
					nVal = 0;
					serializer.TraiteInt(ref nVal);//Ancienne version
				}

				nVal = (int)m_lineStyle;
				serializer.TraiteInt(ref nVal);
				if (nVal >= Enum.GetValues(typeof(GLGridStyles)).Length)
					nVal = (int)GLGridStyles.gridSolid;
				m_lineStyle = (GLGridStyles)nVal;

				serializer.TraiteBool(ref m_bHeaderVisible);

				nVal = m_colorGridColor.ToArgb();
				serializer.TraiteInt(ref nVal);
				m_colorGridColor = Color.FromArgb(nVal);

				nVal = m_colorSelectionColor.ToArgb();
				serializer.TraiteInt(ref nVal);
				m_colorSelectionColor = Color.FromArgb(nVal);

				nVal = m_colorHeaderText.ToArgb();
				serializer.TraiteInt(ref nVal);
				m_colorHeaderText = Color.FromArgb(nVal);

				nVal = m_colorHeaderBack.ToArgb();
				serializer.TraiteInt(ref nVal);
				m_colorHeaderBack = Color.FromArgb(nVal);

				if (nVersion < 2)
				{
					nVal = 0;
					serializer.TraiteInt(ref nVal);
				}

				serializer.TraiteInt(ref m_nItemHeight);

				serializer.TraiteBool(ref m_bShowBorder);

				if ( nVersion >= 2 )
				{
					nVal = m_lineBackColor1.ToArgb();
					serializer.TraiteInt ( ref nVal );
					m_lineBackColor1 = Color.FromArgb ( nVal );

					result = SerializeFont ( serializer, ref m_fontHeader );
					if ( !result )
						return result;
				}
			}
			if (nVersion >= 3)
			{
				int nVal = m_lineBackColor2.ToArgb();
				serializer.TraiteInt(ref nVal);
				m_lineBackColor2 = Color.FromArgb(nVal);
			}

			if (nVersion >= 4)
				serializer.TraiteBool(ref m_bTotalGlobal);

			if (nVersion >= 5)
			{
				int nVal = TotalBackColor.ToArgb();
				serializer.TraiteInt(ref nVal);
				TotalBackColor = Color.FromArgb(nVal);

				nVal = TotalTextColor.ToArgb();
				serializer.TraiteInt(ref nVal);
				TotalTextColor = Color.FromArgb(nVal);
			}
			if (nVersion >= 6)
			{
				strFormule = C2iExpression.GetPseudoCode(m_formuleCodeInitialisation);
				serializer.TraiteString(ref strFormule);
				if (serializer.Mode == ModeSerialisation.Lecture)
					m_formuleCodeInitialisation = C2iExpression.FromPseudoCode(strFormule);
			}
            if ( nVersion >= 7  )
            {
                serializer.TraiteBool ( ref m_bOptimizeRefresh);
            }

			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public IList GetListeSource(CContexteEvaluationExpression contexte)
		{
			if (m_formuleSourceDonnees != null)
			{
				CResultAErreur result = m_formuleSourceDonnees.Eval(contexte);
				if (result)
				{
					if (result.Data is IList)
					{
						//Execute le code d'initialisation
						if (m_formuleCodeInitialisation != null)
							m_formuleCodeInitialisation.Eval(contexte);
						return (IList)result.Data;
					}
				}
			}
			return null;
		}

		public override void OnDesignSelect(
			Type typeEdite, 
			object objetEdite,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
			CListeColonnesEditor.ListeEditee = this;
		}


	}
}
