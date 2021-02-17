using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Objet metier permettant le voyage des valeurs
	/// source et cible d'une opération effectuée sur un champ
	/// </summary>
	public class CDetailOperationSurObjet 
	{
		private IChampPourVersion m_champ;
		private DateTime? m_timeStampModification;
		private CTypeOperationSurObjet m_typeOp;
		private IValeurChampConvertibleEnString m_objetSource; 
		private IValeurChampConvertibleEnString m_objetCible;

		//----------------------------------------------------
		public CDetailOperationSurObjet ()
		{

		}

		//----------------------------------------------------
		public IChampPourVersion Champ
		{
			get
			{
				return m_champ;
			}
			set
			{
				m_champ = value;
			}
		}

		//----------------------------------------------------
		public CTypeOperationSurObjet TypeOperation
		{
			get
			{
				return m_typeOp;
			}
			set
			{
				m_typeOp = value;
			}
		}

		//----------------------------------------------------
		public IValeurChampConvertibleEnString ValeurSource
		{
			get
			{
				return m_objetSource;
			}
			set
			{
				m_objetSource = value;
			}
		}

		//----------------------------------------------------
		public IValeurChampConvertibleEnString ValeurCible
		{
			get
			{
				return m_objetCible;
			}
			set
			{
				m_objetCible = value;
			}
		}

		//----------------------------------------------------
		public DateTime? TimeStampModification
		{
			get
			{
				return m_timeStampModification;
			}
			set
			{
				m_timeStampModification = value;
			}
		}

	}



}
