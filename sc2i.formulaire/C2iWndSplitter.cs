using System;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Collections.Generic;


using sc2i.expression;
using sc2i.common;
using sc2i.drawing;


namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iLabel.
	/// </summary>
	[WndName("Splitter")]
	[Serializable]
	public class C2iWndSplitter : C2iWndComposantFenetre
	{
		/// ///////////////////////
        public C2iWndSplitter()
        {
            DockStyle = EDockStyle.Left;
            Size = new Size(3, 3);
        }

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

        /// ///////////////////////////////////////
        public override EDockStyle DockStyle
        {
            get
            {
                return base.DockStyle;
            }
            set
            {
                if (value == EDockStyle.None || value == EDockStyle.Fill)
                    base.DockStyle = EDockStyle.Left;
                else
                    base.DockStyle = value;
                Size = Size;
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
                Size sz = value;
                switch (DockStyle)
                {
                    case EDockStyle.Top:
                    case EDockStyle.Bottom:
                        sz.Height = Math.Min(sz.Height, 10);
                        break;
                    case EDockStyle.Left:
                    case EDockStyle.Right:
                        sz.Width = Math.Min(sz.Width, 10);
                        break;
                }
                base.Size = sz;
            }
        }

        

#if PDA
#else
		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
            Size sz = Size;
            if (DockStyle == EDockStyle.Left || DockStyle == EDockStyle.Right)
                if (sz.Width > 10)
                    sz.Width = 10;
            if (DockStyle == EDockStyle.Top || DockStyle == EDockStyle.Bottom)
                if (sz.Height > 10)
                    sz.Height = 10;
			Rectangle rect = new Rectangle ( Position , sz );
			g.FillRectangle(b, rect);
			b.Dispose();
			base.MyDraw ( ctx );
		}

		/// ///////////////////////
		public override void DrawInterieur ( CContextDessinObjetGraphique ctx )
		{
			
		}

		
#endif
		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			return result;
		}


      
	}
}
