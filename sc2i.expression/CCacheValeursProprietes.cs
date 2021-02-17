using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	public class CCacheValeursProprietes
	{
		private bool m_bIsEnable = true;
		protected class CDonneeParObjet : Dictionary<string, object>
		{
		}

		Dictionary<object, CDonneeParObjet> m_dataCache = new Dictionary<object,CDonneeParObjet>();

		//----------------------------------------------------------------------------
		public CCacheValeursProprietes()
		{
		}

		//----------------------------------------------------------------------------
		public bool CacheEnabled
		{
			get
			{
				return m_bIsEnable;
			}
			set
			{
				m_bIsEnable = value;
			}
		}

		//----------------------------------------------------------------------------
		public object GetValeurCache(object objetInterroge, string strPropriete)
		{
			if (!m_bIsEnable)
				return null;
			if ( objetInterroge == null || strPropriete == null )
				return null;
			CDonneeParObjet data = null;
			try
			{
				if (m_dataCache.TryGetValue(objetInterroge, out data))
				{
					object valeur = null;
					data.TryGetValue(strPropriete, out valeur);
					return valeur;
				}
			}
			catch//Si le try GetValue échoue, c'est que le cache contient des trucs
			//pourris. Il faut donc le vider
			{
				ResetCache();
			}

			return null;
		}

		//----------------------------------------------------------------------------
		public void ResetCache(object objet)
		{
			try
			{
				if (m_dataCache.ContainsKey(objet))
					m_dataCache.Remove(objet);
			}
			catch
			{
				//Si contains keys échoue, c'est que le m_dataCache contient des données
				//invalide, il faut donc le reseter
				ResetCache();
			}
		}

		//----------------------------------------------------------------------------
		public void ResetCache()
		{
			m_dataCache.Clear();
		}

		//----------------------------------------------------------------------------
		public void StockeValeurEnCache(object objetInterroge, string strPropriete, object valeur)
		{
			if (!m_bIsEnable)
				return;
			if ( objetInterroge == null || strPropriete == null )
				return;
			CDonneeParObjet data = null;
			try
			{
				if (!m_dataCache.TryGetValue(objetInterroge, out data))
				{
					data = new CDonneeParObjet();
					m_dataCache[objetInterroge] = data;
				}
				data[strPropriete] = valeur;
			}
			catch { }
			
		}
	}
}
