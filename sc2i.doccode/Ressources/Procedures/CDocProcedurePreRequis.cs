using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class CDocProcedurePreRequis : CDocElementALiensRessource
	{
		#region [[ Constantes ]]
		public const string nomBalise = "PreRequis";
		#endregion

		#region ++ Constructeurs ++
		public CDocProcedurePreRequis()
		{

		}
		#endregion

		#region ~~ Méthodes ~~
		public CDocProcedurePreRequis Clone()
		{
			CDocProcedurePreRequis clone = new CDocProcedurePreRequis();
			return clone;
		}
		#endregion

	}

}
