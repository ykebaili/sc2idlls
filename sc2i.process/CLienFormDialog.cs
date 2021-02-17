using System;
using System.Drawing;

using sc2i.drawing;
using sc2i.common;
using System.Collections.ObjectModel;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CLienFromDialog.
	/// </summary>
	public class CLienFromDialog : CLienAction
	{
		private E2iDialogResult m_result = E2iDialogResult.OK;

		/// ///////////////////////////////////////////////////
		public CLienFromDialog( CProcess process)
			:base(process)
		{
		}

		/// ///////////////////////////////////////////////////
		public CLienFromDialog ( CProcess process, E2iDialogResult result )
			:base ( process )
		{
			m_result = result;
		}

		/// ///////////////////////////////////////////////////
		public E2iDialogResult ResultAssocie
		{
			get
			{
				return m_result;
			}
			set
			{
				m_result = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private string m_strLibOk = I.T("Ok|10");
		private string m_strLibYes = I.T("Yes|11");
		private string m_strLibNo = I.T("No|12");
		private string m_strLibCancel = I.T("Cancel|13");
		private string m_strLibAbort = I.T("Abord|14");
		private string m_strLibRetry = I.T("Retry|15");
		private string m_strLibIgnore = I.T("Ignore|16");
		private string m_strLibNone =I.T("None|17");
		public override string Libelle
		{
			get
			{
				switch ( m_result )
				{
					case E2iDialogResult.OK :
						return m_strLibOk;
					case E2iDialogResult.Yes:
						return m_strLibYes;
					case E2iDialogResult.No:
						return m_strLibNo;
					case E2iDialogResult.Cancel:
						return m_strLibCancel;
					case E2iDialogResult.Abort:
						return m_strLibAbort;
					case E2iDialogResult.Retry:
						return m_strLibRetry;
					case E2iDialogResult.Ignore:
						return m_strLibIgnore;
					case E2iDialogResult.None:
						return m_strLibNone;
				}
				return m_strLibNone;
			}
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
			Rectangle rect = RectangleAbsolu;
            
            Point pt = segments[0].Milieu;
            if (segments[0].Vecteur.Y == 0)//horizontal
                pt.Offset(-(int)(size.Width / 2), -(int)(size.Height + 3));
            else
                pt.Offset(3, (int)(-size.Height / 2));
			Brush bBlack = new SolidBrush ( Color.Black );
			g.DrawString ( strLibelle, ft, bBlack, pt.X, pt.Y);
			bBlack.Dispose();
            ft.Dispose();
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

			int nResult = (int)m_result;
			serializer.TraiteInt ( ref nResult );
			m_result = (E2iDialogResult)nResult;
			return result;
		}

		/// ///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			return CResultAErreur.True;
		}




	}
}
