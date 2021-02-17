using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{

	public class CReferenceChampPourVersion
	{
		public readonly string FieldKey;
		public readonly string TypeChampString;

		//--------------------------------------------------
		public CReferenceChampPourVersion(string strTypeChamp, string strFieldKey)
		{
			FieldKey = strFieldKey;
			TypeChampString = strTypeChamp;
		}

		//--------------------------------------------------
		public override int GetHashCode()
		{
			return (FieldKey + TypeChampString).GetHashCode();
		}

		//--------------------------------------------------
		public override bool Equals(object obj)
		{
			if (obj is CReferenceChampPourVersion)
				return obj.GetHashCode() == GetHashCode();
			return false;
		}
	}
	
}
