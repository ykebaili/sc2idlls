using System.Drawing;
using System.Collections.Generic;

namespace sc2i.drawing
{
    /// <summary>
    /// Fonction de dessin supplémentaire d'un objet. Si elle retourne false, c'est que le dessin doit s'arrêter
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="objet"></param>
    /// <returns></returns>
    public delegate bool DessinSupplementaireDelegate ( CContextDessinObjetGraphique ctx, C2iObjetGraphique objet );


	public class CContextDessinObjetGraphique
	{
		//Liste des zones (absolues) à dessiner
		private List<Rectangle> m_lstRectangleToDraw = new List<Rectangle>();
		private bool m_bWorkWithLimits = false;
		private Graphics m_graph;
        private DessinSupplementaireDelegate m_fonctionDessinSupplementaireAvantObjet;
        private DessinSupplementaireDelegate m_fonctionDessinSupplementaireApresObjet;

        /// <summary>
        /// Liste des liens dessinés dans le contexte, pour éviter les chevauchements
        /// </summary>
        private List<CLienTracable> m_listeLiensDessines = new List<CLienTracable>();

		public CContextDessinObjetGraphique(Graphics g)
		{
			m_graph = g;

		}
		public CContextDessinObjetGraphique(Graphics g, Rectangle limites)
		{
			m_graph = g;
			m_lstRectangleToDraw.Clear();
			m_lstRectangleToDraw.Add(limites);
			m_bWorkWithLimits = true;
		}

		public CContextDessinObjetGraphique(Graphics g, List<Rectangle> rectanglesLimites)
		{
			m_graph = g;
			m_lstRectangleToDraw.Clear();
			m_lstRectangleToDraw.AddRange(rectanglesLimites);
			m_bWorkWithLimits = true;
		}

        //--------------------------------------------
        public void AddLien(CLienTracable lien)
        {
            m_listeLiensDessines.Add(lien);
        }

        //--------------------------------------------
        public void AddLiens(CLienTracable[] liens)
        {
            m_listeLiensDessines.AddRange(liens);
        }

        //--------------------------------------------
        public void ClearLiens()
        {
            m_listeLiensDessines.Clear();
        }

        //--------------------------------------------
        public IEnumerable<CLienTracable> Liens
        {
            get
            {
                return m_listeLiensDessines.AsReadOnly();
            }
        }

		
		public bool WorkWithLimits
		{
			get
			{
				return m_bWorkWithLimits;
			}
			set
			{
				m_bWorkWithLimits = value;
			}
		}

		
		public Graphics Graphic
		{
			get
			{
				return m_graph;
			}
			set
			{
				m_graph = value;
			}
		}
		
		/// <summary>
		/// Retourne vrai si l'élément doit être redessiné
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public bool ShouldDrawElement(Rectangle rectAbsolu)
		{
			if (!WorkWithLimits)
				return true;
			foreach (Rectangle rct in m_lstRectangleToDraw)
				if (rct.IntersectsWith(rectAbsolu))
					return true;
			return false;
		}

        public DessinSupplementaireDelegate FonctionDessinSupplementaireAvantObjet
        {
            get
            {
                return m_fonctionDessinSupplementaireAvantObjet;
            }
            set{
                m_fonctionDessinSupplementaireAvantObjet = value;
            }

        }

        public DessinSupplementaireDelegate FonctionDessinSupplementaireApresObjet
        {
            get
            {
                return m_fonctionDessinSupplementaireApresObjet;
            }
            set
            {
                m_fonctionDessinSupplementaireApresObjet = value;
            }
        }


	}
}
