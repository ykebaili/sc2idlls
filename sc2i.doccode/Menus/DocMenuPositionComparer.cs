using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{


	/// <summary>
	/// Trie les elements de la position la plus petite � la position la plus grande
	/// </summary>
	public class DocMenuPositionComparer : System.Collections.Generic.IComparer<DocMenu>
	{
		public int Compare(DocMenu x, DocMenu y)
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
