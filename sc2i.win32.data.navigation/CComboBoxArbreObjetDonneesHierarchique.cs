using System;
using System.Collections.Generic;
using System.Text;

using sc2i.common;
using sc2i.data;
using sc2i.win32.data.dynamic;

namespace sc2i.win32.data.navigation
{
	public class CComboBoxArbreObjetDonneesHierarchique : sc2i.win32.data.dynamic.CComboBoxArbreObjetDonneesHierarchique
	{
		public Type m_typeFormEdition = null;
		//-----------------------------------------------
		public CComboBoxArbreObjetDonneesHierarchique()
			: base()
		{
		}

		//-----------------------------------------------
		protected override void OnClickLink()
		{
			if ( m_typeFormEdition != null && ElementSelectionne != null )
			{
				CFormEditionStandard form = (CFormEditionStandard)Activator.CreateInstance(m_typeFormEdition,
					new object[]{(CObjetDonneeAIdNumerique)this.ElementSelectionne} );
				CSc2iWin32DataNavigation.Navigateur.AffichePage ( form );
			}
		}

		//-----------------------------------------------
		public CResultAErreur Init(
			Type typeObjets,
			string strProprieteListeFils,
			string strChampParent,
			string strProprieteAffichee,
			Type typeFormEdition,
			CFiltreData filtre,
			CFiltreData filtreRacine)
		{
			CResultAErreur result = base.Init(typeObjets, strProprieteListeFils, strChampParent, strProprieteAffichee, filtre, filtreRacine);
			m_typeFormEdition = typeFormEdition;
			return result;
		}

		//-----------------------------------------------
		protected override bool IsLink
		{
			get
			{
				return m_typeFormEdition != null;
			}
		}
	}
}
