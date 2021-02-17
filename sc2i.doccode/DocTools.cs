using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public static class DocTools
	{

		public static bool VerifierID(string ID)
		{
			if (ID.Length == 0)
				return false;
			if (ID.Contains(" "))
				return false;
			if (ID.Contains(":"))
				return false;
			if (ID.Contains("*"))
				return false;
			if (ID.Contains("/"))
				return false;
			if (ID.Contains("\\"))
				return false;
			if (ID.Contains("\""))
				return false;
			if (ID.Contains("|"))
				return false;
			if (ID.Contains("<"))
				return false;
			if (ID.Contains(">"))
				return false;

			return true;
		}


		public static void CreerArborescence(ref DocMenu mnupere, List<DocMenuItem> lstfils)
		{
			for (int i = lstfils.Count; i > 0; i--)
			{
				DocMenuItem itm = lstfils[i - 1];
				if (itm.ParentID == mnupere.ID)
				{
					mnupere.Enfants.Add(itm);
					CreerArborescence(ref itm, lstfils);
				}
			}
			
			mnupere.OrganiserFils();
		}
		public static void CreerArborescence(ref DocMenuItem itmpere, List<DocMenuItem> lstfils)
		{
			for (int i = lstfils.Count; i > 0; i--)
			{
				DocMenuItem itm = lstfils[i - 1];
				if (itm.ParentID == itmpere.ID)
				{
					itmpere.Enfants.Add(itm);
					CreerArborescence(ref itm, lstfils);
				}
			}

			itmpere.OrganiserFils();
		}

	}




}
