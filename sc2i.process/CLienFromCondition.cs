using System;
using System.Drawing;
using System.Collections.Generic;
using sc2i.drawing;
using sc2i.common;
using System.Collections.ObjectModel;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CLienFromCondition.
	/// </summary>
	public class CLienFromCondition : CLienAction
	{
		private bool m_bSiOui = true;

		/// ///////////////////////////////////////////////////
		public CLienFromCondition( CProcess process)
			:base(process)
		{
		}

		/// ///////////////////////////////////////////////////
		public bool IsSiOui
		{
			get
			{
				return m_bSiOui;
			}
			set
			{
				m_bSiOui = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private string m_strOui = I.T("Yes|11");
		private string m_strNo = I.T("No|12");
		public override string Libelle
		{
			get
			{
				return IsSiOui ? m_strOui : m_strNo;
			}
		}

		/// ///////////////////////////////////////////////////
		public override Pen GetNewPenCouleurCadre()
		{
			if ( IsSiOui )
				return new Pen(Color.Green, 2);
			else
				return new Pen(Color.Red, 2);
		}


		/// ///////////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw ( ctx );

            CSegmentDroite[] segments = GetLienTracable().Segments;
            
			Graphics g = ctx.Graphic;
			string strLibelle = Libelle;
			Font ft = new Font ( "Arial", 6, FontStyle.Regular );
			SizeF size = g.MeasureString ( strLibelle, ft );
            Point pt = segments[0].Milieu;
            if (segments[0].Vecteur.Y == 0)//horizontal
                pt.Offset(-(int)(size.Width / 2), -(int)(size.Height + 3));
            else
                pt.Offset(3, (int)(-size.Height / 2));
			Rectangle rect = RectangleAbsolu;
			Brush bBlack = new SolidBrush ( Color.Black );
			g.DrawString ( strLibelle, ft, bBlack, pt.X, pt.Y);
			bBlack.Dispose();
		}

		/// ///////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion( ref nVersion );
			if ( !result) 
				return result;
			result = base.MySerialize ( serializer );
			if ( !result )
				return result;

			serializer.TraiteBool ( ref m_bSiOui );
			return result;
		}

		/// ///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			return CResultAErreur.True;
		}




	}
}
