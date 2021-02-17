using System;
using System.Drawing;

using sc2i.formulaire;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CDonneeDragDropControl
	{
		private C2iWnd m_wnd = null;
		private Point m_ptDrag = new Point(-1,-1);

		/// ///////////// ///////////// ///////////// //////////
		public CDonneeDragDropControl( C2iWnd wnd )
		{
			m_wnd = wnd;
		}

		/// ///////////// ///////////// ///////////// //////////
		public CDonneeDragDropControl ( C2iWnd wnd, Point ptOffset )
		{
			m_wnd = wnd;
			m_ptDrag = ptOffset;
		}

		/// ///////////// ///////////// ///////////// //////////
		public C2iWnd Wnd
		{
			get
			{
				return m_wnd;
			}
		}

		/// ///////////// ///////////// ///////////// //////////
		public Rectangle GetDragDropPosition ( Point ptMouse )
		{
			Rectangle rect = new Rectangle ( 0, 0, m_wnd.Size.Width, m_wnd.Size.Height );
			if ( m_ptDrag.X == -1 )
				rect.Offset ( ptMouse.X - rect.Width/2, ptMouse.Y - rect.Height/2 );
			else
				rect.Offset ( ptMouse.X - m_ptDrag.X, ptMouse.Y - m_ptDrag.Y );
			return rect;
		}
	}
}
