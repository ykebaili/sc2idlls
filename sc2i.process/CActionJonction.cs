using System;
using System.Drawing;
using System.Collections;

using sc2i.common;
using sc2i.drawing;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionJonction.
	/// </summary>
	public class CActionJonction : CActionLienSortantSimple
	{

		/// //////////////////////////////////////////////////////////////
		public CActionJonction(CProcess process)
			:base ( process )
		{
			Libelle = I.T("Bond|183");
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
				return new Size ( 15, 15 );
			}
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
			Brush bGreen = new SolidBrush ( Color.LightBlue );
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
