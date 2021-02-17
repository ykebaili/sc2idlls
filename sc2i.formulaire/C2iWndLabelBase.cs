using System;
using System.Drawing;
using System.IO;


using sc2i.common;
using sc2i.drawing;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iLabel.
	/// </summary>
	[Serializable]
	public class C2iWndLabelBase : C2iWnd
	{

		public enum LabelBorderStyle
		{
			Aucun,
			_3D,
			Plein
		}

		private ContentAlignment m_alignement = 
#if PDA
			ContentAlignment.TopLeft;
#else
			ContentAlignment.MiddleLeft;
#endif
		private string m_strTexte = "Texte";
		private LabelBorderStyle m_borderStyle = LabelBorderStyle.Aucun;
		/// ///////////////////////
		public C2iWndLabelBase()
		{
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return false;
		}

		/// ///////////////////////
		public string Text
		{
			get
			{
				return m_strTexte;
			}
			set
			{
				m_strTexte = value;
			}
		}

		/// ///////////////////////
		public LabelBorderStyle BorderStyle
		{
			get
			{
				return m_borderStyle;
			}
			set
			{
				m_borderStyle = value;
			}
		}

		/// ///////////////////////
		public ContentAlignment TextAlign
		{
			get
			{
				return m_alignement;
			}
			set
			{
				m_alignement = value;
			}
		}

		/// ///////////////////////
		[System.ComponentModel.Browsable(false)]
		protected override Point OrigineCliente
		{
			get
			{
				if ( BorderStyle == LabelBorderStyle.Aucun )
					return new Point(0,0);
				else
					return new Point(1,1);
			}
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		protected override Size ClientSize
		{
			get
			{
				Size sz = Size;
				if ( BorderStyle != LabelBorderStyle.Aucun )
				{
					sz.Width -= 2;
					sz.Height -= 2;
				}
				return sz;
			}
		}

#if PDA
#else
		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
			g.FillRectangle(b, rect);
			b.Dispose();
			DrawCadre ( g );
			base.MyDraw ( ctx );
		}

		/// ///////////////////////
		public override void DrawInterieur ( CContextDessinObjetGraphique ctx )
		{
			Graphics g = ctx.Graphic;
			if ( Font == null )
				return;
			if ( Text == "" )
				return;
						
			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Near;
			format.Alignment = StringAlignment.Near;
			
			switch ( TextAlign )
			{
				case ContentAlignment.BottomCenter :
				case ContentAlignment.BottomLeft :
				case ContentAlignment.BottomRight :
					format.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleCenter : 
				case ContentAlignment.MiddleLeft :
				case ContentAlignment.MiddleRight :
					format.LineAlignment = StringAlignment.Center;
					break;
			}
			switch ( TextAlign )
			{
				case ContentAlignment.BottomCenter :
				case ContentAlignment.MiddleCenter :
				case ContentAlignment.TopCenter :
					format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomRight :
				case ContentAlignment.MiddleRight :
				case ContentAlignment.TopRight :
					format.Alignment = StringAlignment.Far;
					break;
			}

            DrawString(g, format);
		}

        protected virtual void DrawString (Graphics g, StringFormat format )
        {
            Brush br = new SolidBrush(ForeColor);
            g.DrawString(Text, Font, br, ClientRect, format);
            br.Dispose();
        }


		/// /////////////////////////////////////////////////
		protected void DrawCadre ( Graphics g )
		{
			if ( BorderStyle == LabelBorderStyle.Aucun )
				return;
			Rectangle rect = new Rectangle ( Position.X, Position.Y, Size.Width, Size.Height );
			//rect = contexte.ConvertToAbsolute(rect);
			if ( BorderStyle == LabelBorderStyle.Plein )
			{
				Pen pen = new Pen ( ForeColor );
				g.DrawRectangle(pen, rect);
				pen.Dispose();
			}
			if ( BorderStyle == LabelBorderStyle._3D )
			{
				Pen pen = SystemPens.ControlDark;
				g.DrawRectangle(pen, rect);
				pen = SystemPens.ControlLight;
				g.DrawLine ( pen, new Point ( rect.Left, rect.Bottom ), new Point ( rect.Right, rect.Bottom ) );
				g.DrawLine ( pen, new Point ( rect.Right, rect.Bottom ), new Point ( rect.Right, rect.Top ) );
			}
		}
#endif
		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize (C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strTexte );
			
			int nTmp = (int)m_alignement;
			serializer.TraiteInt ( ref nTmp );
			m_alignement = (ContentAlignment) nTmp;

			nTmp = (int) m_borderStyle;
			serializer.TraiteInt ( ref nTmp );
			m_borderStyle = (LabelBorderStyle) nTmp;
			return result;
		}
	}
}
