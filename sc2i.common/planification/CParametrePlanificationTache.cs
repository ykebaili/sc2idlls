using System;
using System.Collections;


namespace sc2i.common.planification
{
	/// <summary>
	/// Liste de CPlanificationTache.
	/// </summary>
	[Serializable]
	public class CParametrePlanificationTache : I2iSerializable
	{

		/// <summary>
		/// Lise de CPlanificationTache
		/// </summary>
		private ArrayList m_lstPlanifications = new ArrayList();

		/// //////////////////////////////////////////////////////////
		public CParametrePlanificationTache()
		{
		}

		/// //////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// //////////////////////////////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			
			result = serializer.TraiteArrayListOf2iSerializable ( m_lstPlanifications );

			return result;
		}

		/// //////////////////////////////////////////////////////////
		public void AddPlanification ( CPlanificationTache planification )
		{
			m_lstPlanifications.Add ( planification );
		}

		/// //////////////////////////////////////////////////////////
		public void RemovePlanification ( CPlanificationTache planification )
		{
			m_lstPlanifications.Remove ( planification );
		}

		/// //////////////////////////////////////////////////////////
		public int GetNbPlanifications()
		{
			return m_lstPlanifications.Count;
		}

		/// //////////////////////////////////////////////////////////
		public CPlanificationTache[] Planifications
		{
			get
			{
				return ( CPlanificationTache[] )m_lstPlanifications.ToArray(typeof(CPlanificationTache));
			}
		}

		/// //////////////////////////////////////////////////////////
		public void ResetPlanifications()
		{
			m_lstPlanifications.Clear();
		}

		/// //////////////////////////////////////////////////////////
		public CDateTimeEx GetNextOccurence ( DateTime dtDateReference, bool bPremiereExecution )
		{
			CDateTimeEx dtMin = null;
			foreach ( CPlanificationTache planif in Planifications )
			{
				CDateTimeEx dt = planif.GetNextOccurence ( dtDateReference, bPremiereExecution );
				if ( dt != null && 
					(dtMin == null || dt.DateTimeValue<dtMin.DateTimeValue ) )
					dtMin = dt;
			}
			return dtMin;
		}
	}
}
