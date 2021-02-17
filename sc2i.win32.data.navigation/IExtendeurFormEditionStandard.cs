using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Windows.Forms;
using System.Reflection;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	public interface IExtendeurFormEditionStandard : IControlALockEdition, IDisposable
	{
		Type TypeObjetEtendu{get;}
		void CreateInForm ( CFormEditionStandard form );
		CResultAErreur InitChamps();
		CResultAErreur MajChamps();
	}

	public class CGestionnaireExtendeurFormEditionStandard
	{
		/// <summary>
		/// type édité-> liste de types d'extendeurs
		/// </summary>
		private static Dictionary<Type, List<Type>> m_dicExtendeurForTypes = new Dictionary<Type, List<Type>>();

		private CFormEditionStandard m_form = null;
		private List<IExtendeurFormEditionStandard> m_listeExtendeurs = null;

		public static void RegisterExtendersInAssembly(Assembly assembly)
		{
			foreach (Type tp in assembly.GetTypes())
			{
				if (typeof(IExtendeurFormEditionStandard).IsAssignableFrom(tp))
				{
					try
					{
						IExtendeurFormEditionStandard extendeur = Activator.CreateInstance(tp) as IExtendeurFormEditionStandard;
						Type typeEtendu = extendeur.TypeObjetEtendu;
						List<Type> lstForType = null;
						if (!m_dicExtendeurForTypes.TryGetValue(typeEtendu, out lstForType))
						{
							lstForType = new List<Type>();
							m_dicExtendeurForTypes[typeEtendu] = lstForType;
						}
						if (!lstForType.Contains(tp))
							lstForType.Add(tp);
					}
					catch
					{ }
				}
			}
		}

		public CGestionnaireExtendeurFormEditionStandard(CFormEditionStandard form)
		{
			m_form = form;
			if (m_form == null)
				throw new Exception("Error in extender allocation");
			m_form.OnInitChamps += new sc2i.data.ResultEventHandler(m_form_OnInitChamps);
			m_form.OnMajChamps += new sc2i.data.ResultEventHandler(m_form_OnMajChamps);
		}

		//-------------------------------------------------------------------
		void m_form_OnMajChamps(object sender, ref CResultAErreur result)
		{
			if (!result)
				return;
			if (!m_form.ModeEdition)
				return;
			if (m_listeExtendeurs != null)
			{
				foreach (IExtendeurFormEditionStandard extendeur in m_listeExtendeurs)
				{
					result = extendeur.MajChamps();
					if (!result)
						return;
				}
			}
		}

		//-------------------------------------------------------------------
		void m_form_OnInitChamps(object sender, ref CResultAErreur result)
		{
			if ( !result )
				return;
 			if ( m_listeExtendeurs == null )
			{
				if ( m_form.GetObjetEdite() == null )
					return;
				m_listeExtendeurs = new List<IExtendeurFormEditionStandard>();
				List<Type> lstTypesExtendeurs = null;
				List<Type> lstTypesATester = new List<Type>();
				Type tp = m_form.GetObjetEdite().GetType();
				while (tp != null)
				{
					lstTypesATester.Add(tp);
					foreach (Type tpInterface in tp.GetInterfaces())
						lstTypesATester.Add(tpInterface);
					tp = tp.BaseType;
				}
				foreach (Type tpToTest in lstTypesATester)
				{
					if (m_dicExtendeurForTypes.TryGetValue(tpToTest, out lstTypesExtendeurs))
					{
						foreach (Type tpExts in lstTypesExtendeurs)
						{
							IExtendeurFormEditionStandard extendeur = Activator.CreateInstance(tpExts) as IExtendeurFormEditionStandard;
							if (extendeur != null)
							{
								extendeur.CreateInForm(m_form);
								m_listeExtendeurs.Add(extendeur);
							}
						}
					}
				}
			}
			foreach ( IExtendeurFormEditionStandard extendeur in m_listeExtendeurs )
			{
                extendeur.LockEdition = !m_form.ModeEdition;
				result = extendeur.InitChamps();
				if ( !result )
					return;
			}
		}
	}
}
