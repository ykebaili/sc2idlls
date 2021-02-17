using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	public partial class CCtrlUpDownListeSpeed : CCtrlUpDownBase
	{
		public CCtrlUpDownListeSpeed()
		{
		}
		//----------------------------------------------------------
		private string m_strProprieteNumero = "";

		public string ProprieteNumero
		{
			get { return m_strProprieteNumero; }
			set { m_strProprieteNumero = value; }
		}

		public CPanelListeSpeedStandard ListeSpeedGeree
		{
			get
			{
				return m_listeSpeedGeree;
			}
			set
			{
				m_listeSpeedGeree = value;
			}
		}
		private CPanelListeSpeedStandard m_listeSpeedGeree;

		public override void Monter()
		{
			OnClic();

			if (m_listeSpeedGeree.ElementSelectionne != null)
			{
				OnClicPourMonter();

				object oSelec = m_listeSpeedGeree.ElementSelectionne;
				RenumerotterListeSpeed(ESensAction.Monter);
				m_listeSpeedGeree.Refresh();
				m_listeSpeedGeree.ElementSelectionne = (CObjetDonnee)oSelec;
			}
			
		}

		public override void Descendre()
		{
			OnClic();
			 
			if (m_listeSpeedGeree.ElementSelectionne != null)
			{
				OnClicPourDescendre();

				object oSelec = m_listeSpeedGeree.ElementSelectionne;
				RenumerotterListeSpeed(ESensAction.Descendre);
				m_listeSpeedGeree.Refresh();
				m_listeSpeedGeree.ElementSelectionne = (CObjetDonnee)oSelec;
			}
		}

		
		private void RenumerotterListeSpeed(ESensAction sens)
		{
			if (ProprieteNumero != "")
			{
				PropertyInfo info = null;
				int numeroCible = -1;
				CObjetDonneeAIdNumerique oSelec = (CObjetDonneeAIdNumerique)m_listeSpeedGeree.ElementSelectionne;
				foreach (CObjetDonneeAIdNumerique o in m_listeSpeedGeree.ListeObjets)
				{
					if (info == null)
					{
						info = o.GetType().GetProperty(ProprieteNumero);
						numeroCible = (int)info.GetGetMethod().Invoke(oSelec, new object[] { });
						switch (sens)
						{
							case ESensAction.Monter: numeroCible--; break;
							case ESensAction.Descendre: numeroCible++; break;
							default: return;
						}
					}
					if (o != oSelec)
					{
						int numObjet = (int)info.GetGetMethod().Invoke(o, new object[] { });
						if (numObjet == numeroCible)
						{
							switch (sens)
							{
								case ESensAction.Monter: numObjet++; break;
								case ESensAction.Descendre: numObjet--; break;
							}
							info.GetSetMethod().Invoke(o, new object[] { numObjet });
							info.GetSetMethod().Invoke(oSelec, new object[] { numeroCible });
							break;
						}
					}
				}
			}
		}
	}
}
