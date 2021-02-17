using System;
using System.Drawing;

using sc2i.drawing;

namespace sc2i.win32.common
{
	/// <summary>
	/// Donnée de drag and drop d'objets graphiques lors de l'édition dans un
	/// CPanelEditionObjetGraphique
	/// </summary>
	public class CDonneeDragDropObjetGraphique
	{
		private I2iObjetGraphique m_objetDragDrop;
		private Point m_ptDrag = new Point(-1, -1);
        
        //Identifiant unique de l'éditeur, qui permet de savoir si le drag drop vient du même éditeur ou pas
        private string m_strIdOrigine = "";

		/// ///////////// ///////////// ///////////// //////////
		public CDonneeDragDropObjetGraphique( string strIdOrigine, I2iObjetGraphique objetDragDrop )
		{
			m_objetDragDrop = objetDragDrop;
            m_strIdOrigine = strIdOrigine;
		}

		/// ///////////// ///////////// ///////////// //////////
		public CDonneeDragDropObjetGraphique ( string strIdOrigine, I2iObjetGraphique objetDragDrop, Point ptOffset )
		{
			m_objetDragDrop = objetDragDrop;
			m_ptDrag = ptOffset;
            m_strIdOrigine = strIdOrigine;
		}


        /// ///////////// ///////////// ///////////// //////////
        /// <summary>
        /// Indique l'id de l'origine de cet élément drag drop
        /// </summary>
        public string IdOrigine
        {
            get
            {
                return m_strIdOrigine;
            }
        }


		/// ///////////// ///////////// ///////////// //////////
		public I2iObjetGraphique ObjetDragDrop
		{
			get
			{
				return m_objetDragDrop;
			}
		}

		/// ///////////// ///////////// ///////////// //////////
		public Rectangle GetDragDropPosition ( Point ptMouse )
		{
			Rectangle rect = new Rectangle ( 0, 0, m_objetDragDrop.Size.Width, m_objetDragDrop.Size.Height );
			if ( m_ptDrag.X == -1 )
				rect.Offset ( ptMouse.X - rect.Width/2, ptMouse.Y - rect.Height/2 );
			else
				rect.Offset ( ptMouse.X - m_ptDrag.X, (ptMouse.Y - m_ptDrag.Y) );
			return rect;
		}
	}
}
