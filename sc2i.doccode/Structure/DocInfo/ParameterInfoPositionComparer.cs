using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{


	/// <summary>
	/// Trie les elements de la position la plus petite à la position la plus grande
	/// </summary>
	public class ParameterInfoPositionComparer : System.Collections.Generic.IComparer<ParameterInfo>
	{
		public int Compare(ParameterInfo x, ParameterInfo y)
		{
			if (x.Position < y.Position)
				return -1;
			else if (x.Position == y.Position)
				return 0;
			else
				return 1;
		}
	}

}
