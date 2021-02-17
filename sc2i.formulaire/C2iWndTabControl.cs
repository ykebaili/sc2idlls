using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using sc2i.common;
using System.Drawing.Drawing2D;
using sc2i.drawing;
using sc2i.expression;

namespace sc2i.formulaire
{
	[WndName("TabControl")]
	public class C2iWndTabControl : C2iWndComposantFenetre
	{
		private List<C2iWndTabPage> m_listePages = new List<C2iWndTabPage>();
		private const int c_nHauteurTitres = 24;
		private int m_nActivePage = 0;

		private int m_nLargeurOnglets = 50;

        public C2iWndTabControl()
            : base()
        {
            LockMode = ELockMode.Independant;
        }

		//-------------------------------------------------
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		//-------------------------------------------------
		public override bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

					

		public override sc2i.drawing.I2iObjetGraphique[] Childs
		{
			get
			{
				if (m_nActivePage >= 0 && m_nActivePage < m_listePages.Count)
				{
					C2iWndTabPage page = m_listePages[m_nActivePage];
					page.Parent = this;
					return new I2iObjetGraphique[] { page };
				}
				return new I2iObjetGraphique[0];
			}
		}

		//-------------------------------------------------
		public int TabPageNumber
		{
			get
			{
				return m_listePages.Count;
			}
			set
			{
				if (value < 1)
					return;
				if (value > 30)
					return;
				for (int nPage = m_listePages.Count; nPage < value; nPage++)
				{
					C2iWndTabPage page = new C2iWndTabPage();
					page.BackColor = BackColor;
					page.ForeColor = ForeColor;
					m_listePages.Add(page);
				}
				while (m_listePages.Count > value)
					m_listePages.RemoveAt(m_listePages.Count - 1);
				RecalculeTaillePages();
			}
		}

		
		//--------------------------------
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
				RecalculeTaillePages();
			}
		}

		//--------------------------------
		private void RecalculeTaillePages()
		{
			foreach (C2iWndTabPage page in m_listePages)
			{
				page.IsLock = false;
				page.Position = new Point(0, 0);
				page.Size = ClientSize;
				page.IsLock = true;
			}
		}


		//--------------------------------
		protected override void MyDraw(sc2i.drawing.CContextDessinObjetGraphique ctx)
		{
			Rectangle rect = new Rectangle(Position, Size);
			Brush br = new SolidBrush(BackColor);
			ctx.Graphic.FillRectangle(br, rect);
			br.Dispose();
			Pen pen = new Pen(ForeColor);
			ctx.Graphic.DrawRectangle(pen, rect);

			int nNumPage = 0;
			int nX = rect.Left+1;
			foreach (C2iWndTabPage page in m_listePages)
			{
				Rectangle rcTab = new Rectangle(nX, rect.Top, m_nLargeurOnglets, c_nHauteurTitres);
				nX += m_nLargeurOnglets;
				br = null;
				if (nNumPage == m_nActivePage)
					br = new SolidBrush(Color.FromArgb((BackColor.R * 2) % 255,
						(BackColor.G * 2) % 255,
						(BackColor.B * 2) % 255));
				else
					br = new SolidBrush(BackColor);
				ctx.Graphic.FillRectangle(br, rcTab);
				br.Dispose();
				pen = new Pen(ForeColor);
				ctx.Graphic.DrawRectangle(pen, rcTab);
				pen.Dispose();
				rcTab.Offset(new Point(2, 2));
				rcTab.Width -= 4;
				br = new SolidBrush(ForeColor);
				ctx.Graphic.DrawString(page.Text, Font, br, rcTab);
				br.Dispose();

				nNumPage++;
			
			}
			base.MyDraw(ctx);
		}

		protected override Point OrigineCliente
		{
			get
			{
				return new Point(1, c_nHauteurTitres);
			}
		}

		protected override Size ClientSize
		{
			get
			{
				Size sz = new Size(base.ClientSize.Width - 2, base.ClientSize.Height - c_nHauteurTitres);
				return sz;
			}
		}

		public override void DrawInterieur(sc2i.drawing.CContextDessinObjetGraphique ctx)
		{
			if (m_nActivePage >= 0 && m_nActivePage < m_listePages.Count)
			{
				C2iWndTabPage page = m_listePages[m_nActivePage];
				page.Draw(ctx);
			}
		}

		private int GetNumVersion()
		{
			return 0;
		}

		//-------------------------------------------------
		protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			result = serializer.TraiteListe<C2iWndTabPage>(m_listePages);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                foreach (C2iWndTabPage page in m_listePages)
                {
                    page.Parent = this;
                }
            }
			return result;
		}

		//----------
		public override void  OnDesignDoubleClick(Point ptAbsolu)
		{
			Point pt = GlobalToClient(ptAbsolu);
			if (pt.Y < 0)
			{
				int nPage = (int)(pt.X / m_nLargeurOnglets);
				if (nPage >= 0 && nPage < m_listePages.Count)
					m_nActivePage = nPage;
			}
		}

		//------------------------------------------------------
		public C2iWndTabPage[] TabPages
		{
			get
			{
				return m_listePages.ToArray();
			}
		}
		//------------------------------------------------------
		public override IWndAChildNomme  GetChildFromName(string strName)
		{
			foreach (C2iWndTabPage page in m_listePages)
				if (page.Name.ToUpper() == strName.ToUpper())
					return page;
			return null;
		}
		
		//------------------------------------------------------
		public override sc2i.expression.CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(GetProprietesStandard());

			Dictionary<string, bool> controlesANePasAjouter = new Dictionary<string, bool>();
			foreach (C2iWndTabPage wnd in m_listePages)
			{
				string strUpper = wnd.Name.ToUpper().Trim();
				if (wnd.Name.Length > 0 &&
					!controlesANePasAjouter.ContainsKey(strUpper))
				{
					lst.Add(new CDefinitionProprieteDynamiqueWndFils(wnd, wnd.Name));
					controlesANePasAjouter.Add(strUpper, true);
				}
			}
			return lst.ToArray();
		}
	}



	//------------------------------------------------------
	public class C2iWndTabPage : C2iWndComposantFenetre
	{
		private string m_strText = "Page";

		public override bool CanBeUseOnType(Type tp)
		{
			return false;
		}

		public override bool AcceptChilds
		{
			get
			{
				return true;
			}
		}

		public string Text
		{
			get
			{
				return m_strText;
			}
			set
			{
				m_strText = value;
			}
		}

		protected override void MyDraw(sc2i.drawing.CContextDessinObjetGraphique ctx)
		{
			Rectangle rect = new Rectangle(Position, Size);
			Brush br = new SolidBrush(BackColor);
			ctx.Graphic.FillRectangle(br, rect);
			br.Dispose();
			 base.MyDraw(ctx);
		}

		private int GetNumVersion()
		{
			return 0;
		}

		//-------------------------------------------------
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serializer.TraiteString(ref m_strText);
			return result;
		}
	}


}
