using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public class CDocProcedureRessource : CDocElementALiensRessource
	{
		#region [[ Constantes ]]
		public const string nomBalise = "ResxConnexe";
		#endregion


		#region ++ Constructeurs ++
		public CDocProcedureRessource()
		{

		}
		#endregion

		#region ~~ Méthodes ~~
		public CDocProcedureRessource Clone()
		{
			CDocProcedureRessource clone = new CDocProcedureRessource();
			return clone;
		}
		#endregion

	}

}
