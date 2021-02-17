using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.data.dynamic;
using System.Drawing;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	class CControleImageForFormulaire : C2iPictureBox
	{
		private C2iWndImage m_wndImage = null;
		private object m_objetSource = null;
		private IFournisseurProprietesDynamiques m_fournisseur = new CFournisseurPropDynStd ( true );

		private CControleImageForFormulaire( C2iWndImage wndImage)
		{
			m_wndImage = wndImage;
		}

		public static CControleImageForFormulaire CreateFromWndImage(C2iWndImage wndImage,Control parent, IFournisseurProprietesDynamiques fournisseur)
		{
			CControleImageForFormulaire viewer = new CControleImageForFormulaire(wndImage);
			viewer.Left = wndImage.Position.X;
			viewer.Top = wndImage.Position.Y;
			viewer.Width = wndImage.Size.Width;
			viewer.Height = wndImage.Size.Height;
			viewer.BackColor = wndImage.BackColor;
			viewer.ForeColor = wndImage.ForeColor;
			viewer.Parent = parent;
			viewer.m_fournisseur = fournisseur;
			return viewer;
		}


		/// <summary>
		/// Définit l'objet source de l'image (qui peut donc retourner une image
		/// par évaluation de la formule d'image de l'image
		/// </summary>
		public object ObjetSource
		{
			get
			{
				return m_objetSource;
			}
			set
			{
				if (value == null || !value.Equals(m_objetSource))
				{
					m_objetSource = value;
					CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(m_objetSource);
					Image img = m_wndImage.GetImageToDisplay(contexte);
					Image = img;
				}
			}
		}
	}
}
