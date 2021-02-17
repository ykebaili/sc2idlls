using System;
using System.Drawing;
using System.Collections;

using sc2i.common;
using sc2i.drawing;
using System.Collections.Generic;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionPointEntree.
	/// </summary>
	public class CActionPointEntree : CActionLienSortantSimple
	{

		/// //////////////////////////////////////////////////////////////
		public CActionPointEntree(CProcess process)
			:base ( process )
		{
			Libelle = I.T("Alternative Entry point|20017");
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
		}

		/// //////////////////////////////////////////////////////////////
		public override System.Drawing.Size DefaultSize
		{
			get
			{
				return new Size ( 100, 30 );
			}
		}

        public override Size  Size
{
	  get 
	{ 
		 return base.Size;
      }
	  set 
	{ 
          Size sz = value;
          if ( sz.Width < 10 )
              sz = new Size ( 10, sz.Height );
		base.Size = sz;
      }
        }

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

        /// ////////////////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Brush bWhite = new SolidBrush(Color.LightGreen);
            List<Point> poly = new List<Point>();
            Rectangle rct = RectangleAbsolu;
            int nWidthFleche = rct.Height/2;
            if ( nWidthFleche > rct.Width/2 )
                nWidthFleche = rct.Width/2;
            poly.Add ( new Point ( rct.Left, rct.Top ) );
            poly.Add ( new Point ( rct.Right-nWidthFleche, rct.Top));
            poly.Add ( new Point ( rct.Right , rct.Top+rct.Height/2 ));
            poly.Add ( new Point ( rct.Right - nWidthFleche, rct.Bottom ));
            poly.Add ( new Point( rct.Left, rct.Bottom ));

            g.FillPolygon(bWhite, poly.ToArray());
            Pen pBlack = new Pen(Color.Black);
            g.DrawPolygon(pBlack, poly.ToArray());
            bWhite.Dispose();
            pBlack.Dispose();
            DrawLibelle(g);

        }

		/// //////////////////////////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
	}
}
