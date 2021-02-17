using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.data;
using sc2i.common;
using sc2i.win32.data;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CComboBoxListeObjetDonnee.
	/// </summary>
	public class CComboBoxLinkListeObjetsDonnees : CComboBoxListeObjetsDonnees, ISelectionneurElementListeObjetsDonnees
	{
		public Type m_typeFormEditionStandard = null;
		private string m_strLinkProperty = "";
		private bool m_bComportementLinkStandard = true;
		//------------------------------------------------------------------------------
		public CComboBoxLinkListeObjetsDonnees()
			:base()
		{
			LinkClicked += new MouseEventHandler(OnLinkClicked);
		}
		//------------------------------------------------------------------------------
		public CResultAErreur Init ( 
			Type typeObjets,
			string strProprieteAffichee,
			Type typeFormEditionStandardElements,
			bool bForceInit)
		{
			return Init ( typeObjets, null, strProprieteAffichee, typeFormEditionStandardElements, bForceInit);
		}
		//------------------------------------------------------------------------------
		public CResultAErreur Init ( 
			Type typeObjets, 
			CFiltreData filtre, 
			string strProprieteAffichee,
			Type typeFormEditionStandardElements,
			bool bForceInit)
		{
			CResultAErreur result = base.Init ( 
				typeObjets,
				filtre, 
				strProprieteAffichee,
				bForceInit );

			TypeFormEdition = typeFormEditionStandardElements;
			
			return result;
		}
		//------------------------------------------------------------------------------
		public CResultAErreur Init ( 
			CListeObjetsDonnees liste, 
			string strProprieteAffichee,
			Type typeFormEditionStandardElements,
			bool bForceInit)
		{
			CResultAErreur result = base.Init ( 
				liste, 
				strProprieteAffichee,
				bForceInit);

			TypeFormEdition = typeFormEditionStandardElements;
			
			return result;
		}
		//------------------------------------------------------------------------------
		private void InitializeComponent()
		{
			this.IsLink = true;

		}
		//------------------------------------------------------------------------------
		public string LinkProperty
		{
			get
			{
				return m_strLinkProperty;
			}
			set
			{
				m_strLinkProperty = value;
			}
		}
		//------------------------------------------------------------------------------
		public Type TypeFormEdition
		{
			get
			{
				return m_typeFormEditionStandard;
			}
			set
			{
				m_typeFormEditionStandard = value;
			}
		}

		//------------------------------------------------------------------------------
		public void OnLinkClicked ( object sender, MouseEventArgs args )
		{
			if ( m_bComportementLinkStandard )
			{
				object obj;
				obj = (CObjetDonneeAIdNumerique)this.ElementSelectionne;
				if (LinkProperty!="")
					obj = CInterpreteurTextePropriete.GetValue(obj, LinkProperty);
				if ( m_typeFormEditionStandard != null && this.ElementSelectionne != null )
				{
					CFormEditionStandard form = (CFormEditionStandard)Activator.CreateInstance(m_typeFormEditionStandard,
						new object[]{obj} );
					CSc2iWin32DataNavigation.Navigateur.AffichePage ( form );
				}
			}
		}
		//------------------------------------------------------------------------------		
		public bool ComportementLinkStd
		{
			get
			{
				return m_bComportementLinkStandard;
			}
			set
			{
				m_bComportementLinkStandard = value;
			}
		}
	}
}
