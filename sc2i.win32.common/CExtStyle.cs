using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace sc2i.win32.common
{
	[ProvideProperty("StyleBackColor", typeof(Control))]
	[ProvideProperty("StyleForeColor", typeof(Control))]
	public class CExtStyle : Component, IExtenderProvider
	{
		

		public enum EnumCouleurs
		{
			None,
			ColorFondFenetre,
            ColorTexteFenetre,
			ColorFondPanel,
			ColorTextePanel,
			ColorTitrePanel,
			ColorFondPanelTitre
		}

		private class CInfoStyle
		{
			EnumCouleurs m_couleurFond;
			EnumCouleurs m_couleurAvant;
			Control m_ctrl;

			//--------------------------------------------
			public CInfoStyle(Control ctrl)
			{
				m_ctrl = ctrl;
			}

			//--------------------------------------------
			public EnumCouleurs CouleurFond
			{
				get
				{
					return m_couleurFond;
				}
				set
				{
					m_couleurFond = value;
					try
					{
						if ( value != EnumCouleurs.None )
							m_ctrl.BackColor = (Color)m_tableCouleurs[value];
					}
					catch{}
				}
			}

			//--------------------------------------------
			public EnumCouleurs CouleurAvant
			{
				get
				{
					return m_couleurAvant;
				}
				set
				{
					m_couleurAvant = value;
					try
					{
						if (value != EnumCouleurs.None)
							m_ctrl.ForeColor = (Color)m_tableCouleurs[value];
					}
					catch{}
				}
			}
		}

		//EnumCouleur->Couleur
		private static Hashtable m_tableCouleurs = new Hashtable();

		//Control->CInfoStyle
		private Hashtable m_tableDonnees = new Hashtable();


		public CExtStyle()
		{
			if ( m_tableCouleurs.Count == 0 )
			{
                m_tableCouleurs[EnumCouleurs.ColorFondFenetre] = Color.White;
                m_tableCouleurs[EnumCouleurs.ColorTexteFenetre] = Color.Black;
                m_tableCouleurs[EnumCouleurs.ColorFondPanel] = Color.FromArgb(189, 189, 255);
                m_tableCouleurs[EnumCouleurs.ColorTextePanel] = Color.Black;
				m_tableCouleurs[EnumCouleurs.ColorTitrePanel] = Color.Beige;
                m_tableCouleurs[EnumCouleurs.ColorFondPanelTitre] = Color.FromArgb(128,128,255);
			}
		}

		/////////////////////////////////////////
		public static void SetCouleur(EnumCouleurs typeCouleur, Color c)
		{
			m_tableCouleurs[typeCouleur] = c;
		}

		/////////////////////////////////////////
		public static Color GetCouleur(EnumCouleurs typeCouleur)
		{
			if (m_tableCouleurs.Contains(typeCouleur))
				return (Color)m_tableCouleurs[typeCouleur];
			return Color.White;
		}

		/////////////////////////////////////////
		public bool CanExtend ( object extendee )
		{
			if ( extendee is Control )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/////////////////////////////////////////
		private CInfoStyle GetInfo(Control ctrl, bool bCreate)
		{
			CInfoStyle info = (CInfoStyle)m_tableDonnees[ctrl.Name];
			if (info == null && bCreate)
			{
				info = new CInfoStyle(ctrl);
				m_tableDonnees[ctrl.Name] = info;
			}
			return info;
		}

		/////////////////////////////////////////
		public void SetStyleBackColor(object extendee, EnumCouleurs couleur)
		{
			if (extendee is Control)
			{
				CInfoStyle info = GetInfo((Control)extendee, true);
				info.CouleurFond = couleur;
			}
		}

		/////////////////////////////////////////
		public EnumCouleurs GetStyleBackColor(object extendee)
		{
			if (extendee is Control)
			{
				CInfoStyle info = GetInfo((Control)extendee, false);
				if (info != null)
					return info.CouleurFond;
			}
			return EnumCouleurs.None;
		}

		/////////////////////////////////////////
		public void SetStyleForeColor(object extendee, EnumCouleurs couleur)
		{
			if (extendee is Control)
			{
				CInfoStyle info = GetInfo((Control)extendee, true);
				info.CouleurAvant = couleur;
			}
		}

		/////////////////////////////////////////
		public EnumCouleurs GetStyleForeColor(object extendee)
		{
			if (extendee is Control)
			{
				CInfoStyle info = GetInfo((Control)extendee, false);
				if (info != null)
					return info.CouleurAvant;
			}
			return EnumCouleurs.None;
		}
		

		
	}
}
