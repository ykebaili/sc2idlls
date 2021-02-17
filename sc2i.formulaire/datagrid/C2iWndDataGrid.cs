using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;


using sc2i.common;
using sc2i.drawing;
using System.Drawing.Drawing2D;
using sc2i.expression;
using sc2i.formulaire;
using System.Drawing.Design;
using System.ComponentModel;

namespace sc2i.formulaire.datagrid
{
	/// <summary>
	/// Description résumée de C2iLabel.
	/// </summary>
	[Serializable]
    [WndName("DataGrid")]
	public class C2iWndDataGrid : 
        C2iWndComposantFenetre, 
        I2iObjetGraphiqueConteneurAFilsChoisis
	{
        public const string c_strIdEvenementAddElement = "ADDELT";
        public const string c_strIdEvenementDeleteElement = "DELELT";

        private const int c_nHauteurBandeau = 23;

        private C2iExpression m_formuleSource = null;
        private int m_nRowHeaderWidth = 35;
        private Color m_rowHeaderColor = Color.Beige;
        List<CAffectationsProprietes> m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
        private bool m_bHasAddButton = false;
        private bool m_bHasDeleteButton = false;
        private string m_strDeleteMessage = I.T("Delete element|10002");
        private Color m_selectedCellBackColor = Color.DarkBlue;
        private Color m_selectedCellForeColor = Color.White;
        private Color m_defaultHeaderBackColor = Color.Beige;
        private Color m_defaultHeaderForColor = Color.Black;
        private int m_nDefaultRowHeight = 0;

        private bool m_bPreloadData = false;

        private int m_nScrollBarPos = 0;
        private bool m_bHasScrollBar = false;
        private int m_nClientHeight = 50;
        private Rectangle m_rectScrollUp = default(Rectangle);
        private Rectangle m_rectScrollDown = default(Rectangle);
        

        private const int c_nLargeurScrollBar = 13;
		/// ///////////////////////
        public C2iWndDataGrid()
		{
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

        /// ///////////////////////////////////////
        public override bool AcceptChilds
        {
            get
            {
                return true;
            }
        }

        /// ///////////////////////////////////////
        public bool PreloadDatas
        {
            get
            {
                return m_bPreloadData;
            }
            set
            {
                m_bPreloadData = value;
            }
        }

        /// ///////////////////////////////////////
        public bool AcceptAllChilds(IEnumerable<I2iObjetGraphique> objets)
        {
            return objets.FirstOrDefault(o=>!(o is IWndIncluableDansDataGrid) && !(o is C2iWndDataGridColumn) ) == null;
        }


        /// ///////////////////////////////////////
        public C2iWndDataGridColumn GetColumn(int nIndex)
        {
            int nCpt = 0;
            foreach (I2iObjetGraphique objet in Childs)
            {
                C2iWndDataGridColumn col = objet as C2iWndDataGridColumn;
                if (col != null)
                {
                    if (nCpt == nIndex)
                        return col;
                    nCpt++;
                }
            }
            return null;
        }

        /// ///////////////////////////////////////
        public IEnumerable<C2iWndDataGridColumn> Columns
        {
            get
            {
                return from c in Childs where c is C2iWndDataGridColumn select (C2iWndDataGridColumn)c;
            }
        }
                                

        /// ///////////////////////////////////////
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                m_nClientHeight = Math.Max(m_nClientHeight, value.Height - 2);
                base.Size = value;
            }
        }


        /// ///////////////////////////////////////
        public int RowHeaderWidth
        {
            get
            {
                return m_nRowHeaderWidth;
            }
            set
            {
                m_nRowHeaderWidth = value;
            }
        }

        /// ///////////////////////////////////////
        public Color RowHeaderColor
        {
            get
            {
                return m_rowHeaderColor;
            }
            set
            {
                m_rowHeaderColor = value;
            }
        }

        /// ///////////////////////////////////////
        public Color SelectedCellBackColor
        {
            get
            {
                return m_selectedCellBackColor;
            }
            set
            {
                m_selectedCellBackColor = value;
            }
        }

        /// ///////////////////////////////////////
        public Color SelectedCellForeColor
        {
            get
            {
                return m_selectedCellForeColor;
            }
            set
            {
                m_selectedCellForeColor = value;
            }
        }

        /// ///////////////////////////////////////
        public Color DefaultColumnHeaderBackColor
        {
            get
            {
                return m_defaultHeaderBackColor;
            }
            set
            {
                m_defaultHeaderBackColor = value;
            }
        }

        /// ///////////////////////////////////////
        public Color DefaultColumnHeaderForeColor
        {
            get
            {
                return m_defaultHeaderForColor;
            }
            set
            {
                m_defaultHeaderForColor = value;
            }
        }

        /// ///////////////////////////////////////
        public int DefaultRowHeight
        {
            get
            {
                return m_nDefaultRowHeight;
            }
            set
            {
                m_nDefaultRowHeight = value;
            }
        }

       
        /// ///////////////////////////////////////
        protected override Size ClientSize
        {
            get
            {
                Size sz = base.ClientSize;
                sz = new Size(sz.Width - 2-(m_bHasScrollBar?c_nLargeurScrollBar:0), m_nClientHeight);
                if (HasAddButton || HasDeleteButton)
                    sz = new Size(sz.Width, sz.Height - c_nHauteurBandeau);
                return sz;
            }
        }

        /// ///////////////////////////////////////
        protected override Point OrigineCliente
        {
            get
            {
                return new Point(1, 1-m_nScrollBarPos+
                (HasAddButton ||HasDeleteButton?c_nHauteurBandeau:0));
            }
        }


        /// ///////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
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


        /// ///////////////////////////////////////
        public string DeleteConfirmMessage
        {
            get
            {
                return m_strDeleteMessage;
            }
            set
            {
                m_strDeleteMessage = value;
            }
        }

        
        /// ///////////////////////
        private bool m_bIsRepositionning = false;
        public override void  DockChilds()
        {
            if (m_bIsRepositionning)
                return;
            m_bIsRepositionning = true;
            int nHeightInterne = 0;
            foreach (I2iObjetGraphique objet in Childs)
            {
                nHeightInterne += objet.Size.Height;
            }
            if (nHeightInterne > Size.Height-2)
            {
                if (!m_bHasScrollBar)
                {
                    m_bHasScrollBar = true;
                }
                m_nClientHeight = nHeightInterne;
            }
            else
            {
                if (m_bHasScrollBar)
                {
                    m_bHasScrollBar = false;
                    m_nScrollBarPos = 0;
                    m_nClientHeight = Size.Height - 2;
                }
            }
            //base.MyRepositionneChilds();
            base.DockChilds();
            m_bIsRepositionning = false;
        }

        /// ///////////////////////
        public override Rectangle GetLocalClipRect()
        {
            return new Rectangle(0, m_nScrollBarPos, ClientSize.Width, Size.Height - 2-
             (HasAddButton || HasDeleteButton?c_nHauteurBandeau:0));
        }

        /// ///////////////////////
        public override void Draw(CContextDessinObjetGraphique ctx)
        {
            
            base.Draw(ctx);
        }

        
		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
			g.FillRectangle(b, rect);
			b.Dispose();
			DrawCadre ( g );
            if (HasAddButton | HasDeleteButton)
            {
                Pen pen = Pens.Black;
                g.DrawLine(pen, rect.Left, rect.Top + c_nHauteurBandeau, rect.Right, rect.Top + c_nHauteurBandeau);
                int nX = 5;
                Brush br = Brushes.Black;
                if (HasAddButton)
                {
                    g.DrawString("Add", Font, br, new Point(rect.X + nX, rect.Top + 1));
                    nX += 40;
                }
                if (HasDeleteButton)
                {
                    g.DrawString("Delete", Font, br, new Point(rect.Left + nX, rect.Top + 1));
                }
            }
			base.MyDraw ( ctx );
            
		}

        /// ///////////////////////
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
            base.DrawInterieur(ctx);
            
        }

		/// /////////////////////////////////////////////////
		protected void DrawCadre ( Graphics g )
		{
			Rectangle rect = new Rectangle ( Position.X, Position.Y, Size.Width, Size.Height );
			//rect = contexte.ConvertToAbsolute(rect);
			Pen pen = new Pen ( ForeColor );
			g.DrawRectangle(pen, rect);
            if (m_bHasScrollBar)
            {
                int nX = rect.Right - 1 - Resources.scrollbardown.Width;
                if (HasAddButton || HasDeleteButton)
                {
                    rect = new Rectangle(rect.Left, rect.Top + c_nHauteurBandeau,
                        rect.Width, rect.Height - c_nHauteurBandeau);
                }
                m_rectScrollUp = new Rectangle(nX, rect.Top + 1, Resources.scrollbardown.Width, Resources.scrollbardown.Height);
                g.DrawImage(Resources.scrollbarup, m_rectScrollUp);
                m_rectScrollDown = new Rectangle(nX, rect.Bottom - 1 - Resources.scrollbardown.Height, Resources.scrollbardown.Width, Resources.scrollbardown.Height);
                g.DrawImage(Resources.scrollbardown, m_rectScrollDown);
            }
			pen.Dispose();
		}

        /// ///////////////////////////////////////
        public override bool AddChild(I2iObjetGraphique child)
        {
            if (child is C2iWndDataGridColumn)
                return base.AddChild(child);
            else
            {
                C2iWnd wnd = child as C2iWnd;
                if (wnd != null)
                {
                    if (wnd is IWndIncluableDansDataGrid)
                    {
                        wnd.AutoBackColor = false;
                        C2iWndDataGridColumn col = new C2iWndDataGridColumn();
                        col.Text = "col " + (Childs.Count() + 1);
                        
                        col.Control = wnd;
                        base.AddChild(col);
                        col.ForeColor = DefaultColumnHeaderForeColor;
                        col.BackColor = DefaultColumnHeaderBackColor;
                        col.Parent = this;
                        return true;
                    }
                }
            }
            return false;
        }

        /// ///////////////////////////////////////
        public override void RemoveChild(I2iObjetGraphique child)
        {
            base.RemoveChild(child);
        }


		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
            //1 : ajout preloadData
            //2 : sauté, rien changé
            //3 : ajout DefaultRowHeight
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize (C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleSource);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nRowHeaderWidth);
            int nTmp = RowHeaderColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            RowHeaderColor = Color.FromArgb(nTmp);
            serializer.TraiteBool(ref m_bHasAddButton);
            serializer.TraiteBool(ref m_bHasDeleteButton);
            result = serializer.TraiteListe<CAffectationsProprietes>(m_listeAffectationsInitiales);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strDeleteMessage);

            nTmp = SelectedCellBackColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            SelectedCellBackColor = Color.FromArgb(nTmp);

            nTmp = SelectedCellForeColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            SelectedCellForeColor = Color.FromArgb(nTmp);

            nTmp = DefaultColumnHeaderBackColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            DefaultColumnHeaderBackColor = Color.FromArgb(nTmp);

            nTmp = DefaultColumnHeaderForeColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            DefaultColumnHeaderForeColor = Color.FromArgb(nTmp);

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bPreloadData);
            if (nVersion >= 3)
                serializer.TraiteInt(ref m_nDefaultRowHeight);


			return result;
		}

        /// ///////////////////////////////////////
        public override void OnDesignSelect(Type typeEdite, object objetEdite, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CProprieteExpressionEditor.ObjetPourSousProprietes = GetObjetPourAnalyseThis(typeEdite);
            CProprieteAffectationsProprietesEditor.SetTypeSource(GetObjetPourAnalyseThis(typeEdite).TypeAnalyse);
            if (SourceFormula != null)
            {
                CProprieteAffectationsProprietesEditor.SetTypeElementAffecte(SourceFormula.TypeDonnee.TypeDotNetNatif);
            }
            CProprieteAffectationsProprietesEditor.FournisseurProprietes = fournisseurProprietes;
        }

        /// ///////////////////////////////////////
        public override bool OnDesignerMouseDown(Point ptLocal)
        {
            base.OnDesignerMouseDown(ptLocal);
            if (m_bHasScrollBar)
            {
                Point ptGlobal = ClientToGlobal(ptLocal);
                if (m_rectScrollUp.Contains(ptGlobal))
                {
                    if (m_nScrollBarPos > 0)
                    {
                        m_nScrollBarPos = Math.Max(0, m_nScrollBarPos - 10);
                        return true;
                    }
                }
                else if (m_rectScrollDown.Contains(ptGlobal))
                {
                    m_nScrollBarPos += 10;
                    m_nScrollBarPos = Math.Min(m_nScrollBarPos, ClientSize.Height - Size.Height - 2);
                    return true;
                }
            }
            return false;
        }

        /*/// ///////////////////////////////////////
        public override CObjetPourSousProprietes GetObjetAnalysePourFils(CObjetPourSousProprietes objetRacine)
        {
            if (SourceFormula != null)
                return SourceFormula.TypeDonnee.TypeDotNetNatif;
            return typeof(string);
        }*/

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());

            if (SourceFormula != null)
            {
                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "LastAddedElement", "LastAddedElement",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, false),
                    true,
                    true,
                    ""));
            }
            if (SourceFormula != null)
            {
                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "ActiveElement", "ActiveElement",
                    new CTypeResultatExpression(SourceFormula.TypeDonnee.TypeDotNetNatif, false),
                    true,
                    true,
                    ""));
            }
            if (SourceFormula != null)
            {
                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "Source", "Source",
                    SourceFormula.TypeDonnee,
                    true,
                    false, ""));
            }

            return lst.ToArray();
        }

        // Evenements déportées
        public const string c_strSelectionChanged = "SELCHANGED";

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();
            lst.AddRange(base.GetDescriptionsEvenements());

            lst.Add(new CDescriptionEvenementParFormule(c_strSelectionChanged,
                "Active element changed", I.T("Occurs when the active element change|20026")));
            return lst.ToArray();
        }

        

        
	}
}
