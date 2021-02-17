using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;

namespace sc2i.formulaire
{
	//-------------------------------------------------------
	public interface IEditeurSousFormulaire
	{
		bool EditeZoneMultiple(C2iWndFenetre fenetre, Type typeElements, IFournisseurProprietesDynamiques fournisseur);
	}

	//-------------------------------------------------------
	public class CEditeurSousFormulaire
	{
		private static Type m_typeEditeur = null;

		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		//-------------------------------------------------------
		public static void EditeZone(C2iWndFenetre zone, Type typeElements, IFournisseurProprietesDynamiques fournisseur)
		{
			if (m_typeEditeur != null)
			{
				IEditeurSousFormulaire editeur = (IEditeurSousFormulaire)Activator.CreateInstance(m_typeEditeur);
				editeur.EditeZoneMultiple(zone, typeElements, fournisseur);
				if (editeur is IDisposable)
					((IDisposable)editeur).Dispose();
			}
		}
	}
}
