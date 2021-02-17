using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using sc2i.drawing;
using sc2i.common;

namespace sc2i.win32.common
{

	public class CGrilleEditeurObjetGraphique : I2iSerializable
	{
		public CGrilleEditeurObjetGraphique()
		{

		}
		public CGrilleEditeurObjetGraphique(int nLargeur, int nHauteur, Size maxSize)
		{
			m_size = new Size(nLargeur, nHauteur);
			m_maxSize = maxSize;
		}

		
		private Size m_maxSize;

		public int LargeurCarreau
		{
			get
			{
				return TailleCarreau.Width;
			}
			set
			{
				if (value != LargeurCarreau)
				{
					TailleCarreau = new Size(value, HauteurCarreau);
                    FreeBmpCache();
				}
			}
		}
		public int HauteurCarreau
		{
			get
			{
				return TailleCarreau.Height;
			}
			set
			{
				if (value != HauteurCarreau)
				{
					TailleCarreau = new Size(LargeurCarreau, value);
                    FreeBmpCache();
				}
			}
		}
		private Size m_size;
		public Size TailleCarreau
		{
			get
			{
				return m_size;
			}
			set
			{
				if (value != null && value != m_size)
				{
					m_size = value;
                    FreeBmpCache();
				}
			}
		}

		public ERepresentationGrille m_representation = ERepresentationGrille.LignesPointillets;
		public ERepresentationGrille Representation
		{
			get
			{
				return m_representation;
			}
			set
			{
				if (value != Representation)
				{
					m_representation = value;
                    FreeBmpCache();
				}
			}
		}

		private Color m_color = Color.LightGray;
		public Color Couleur
		{
			get
			{
				return m_color;
			}
			set
			{
				if (value != Couleur)
				{
					m_color = value;
                    FreeBmpCache();
				}
			}
		}


        private void FreeBmpCache()
        {
            if (m_bmpCache != null)
                m_bmpCache.Dispose();
            m_bmpCache = null;
        }

		private Bitmap m_bmpCache;
		private Rectangle m_oldZone;
		public void Draw(Graphics g, Rectangle zone)
		{
			if ((HauteurCarreau < 1 && LargeurCarreau < 1) || zone.Width == 0 || zone.Height == 0)
				return;
            
            

			if (m_bmpCache == null
				|| m_oldZone.Location != zone.Location
				|| m_oldZone.Size != zone.Size)
			{
                FreeBmpCache();
				m_bmpCache = new Bitmap(zone.Width, zone.Height);
				Graphics gTmp = Graphics.FromImage(m_bmpCache);
               // gTmp.Transform = g.Transform;
				gTmp.Clip = new Region(zone);
				Pen pen = new Pen(Couleur);
				if (Representation == ERepresentationGrille.LignesDiscontinues)
					pen.DashStyle = DashStyle.Dash;
				else if (Representation == ERepresentationGrille.LignesPointillets)
					pen.DashStyle = DashStyle.Dot;

				int nStartX = (zone.X / LargeurCarreau) * LargeurCarreau;
				int nStartY = (zone.Y / HauteurCarreau) * HauteurCarreau;
				Brush b = new SolidBrush(Couleur);
				for (int nY = nStartY; nY < zone.Bottom; nY += HauteurCarreau)
				{
					if (Representation != ERepresentationGrille.Angles && Representation != ERepresentationGrille.Points)
						gTmp.DrawLine(pen, nStartX, nY, nStartX + zone.Width, nY);

					for (int nX = nStartX; nX < zone.Right; nX += LargeurCarreau)
					{
						if (Representation == ERepresentationGrille.Points)
							gTmp.FillRegion(b, new Region(new Rectangle(new Point(nX, nY), new Size(1, 1))));
						else if (Representation == ERepresentationGrille.Angles)
						{
							gTmp.DrawLine(pen, new Point(nX - 2, nY), new Point(nX + 2, nY));
							gTmp.DrawLine(pen, new Point(nX, nY - 2), new Point(nX, nY + 2));
						}
						else if (nY == nStartY)
						{
							gTmp.DrawLine(pen, nX, nStartY, nX, nStartY + zone.Height);
						}
					}
				}
				b.Dispose();
				pen.Dispose();
                gTmp.Dispose();
			}

			m_oldZone = zone;

			g.DrawImageUnscaled(m_bmpCache, zone.Location);
		}


		#region I2iSerializable Membres

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			int nHauteur = HauteurCarreau;
			int nLargeur = LargeurCarreau;
			serializer.TraiteInt(ref nHauteur);
			serializer.TraiteInt(ref nLargeur);

			TailleCarreau = new Size(nLargeur, nHauteur);

			int nRepresentation = (int)Representation;
			serializer.TraiteInt(ref nRepresentation);
			m_representation = (ERepresentationGrille)nRepresentation;

			int nCouleur = m_color.ToArgb();
			serializer.TraiteInt(ref nCouleur);
			m_color = Color.FromArgb(nCouleur);

			return result;
		}

		#endregion
	}
}