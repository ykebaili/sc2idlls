using System;
using System.Drawing;
using System.Collections;

using sc2i.drawing;
using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionDebut.
	/// </summary>
	public class CActionDebut : CActionLienSortantSimple
	{

		/// //////////////////////////////////////////////////////////////
		public CActionDebut(CProcess process)
			:base ( process )
		{
			Position = new Point ( 100, 30 );
			Libelle = I.T("Starting|158");
		}

		/// //////////////////////////////////////////////////////////////
		public override System.Drawing.Size DefaultSize
		{
			get
			{
				return new Size ( 15, 15 );
			}
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// //////////////////////////////////////////////////////////////
		public override System.Drawing.Size Size
		{
			get
			{
				return DefaultSize;
			}
			set
			{
			}
		}




		/// //////////////////////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			Graphics g = ctx.Graphic;
			Brush bGreen = new SolidBrush ( Color.LightGreen );
			Pen pBlack = new Pen ( Color.Black );
			g.FillEllipse ( bGreen, RectangleAbsolu );
			g.DrawEllipse ( pBlack, RectangleAbsolu );
			bGreen.Dispose();
			pBlack.Dispose();
		}

		/// //////////////////////////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
	}
}
