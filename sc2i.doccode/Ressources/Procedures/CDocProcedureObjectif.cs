using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public class CDocProcedureObjectif : CDocElementALiensRessource
	{
		#region [[ Constantes ]]
		public const string nomBalise = "Objectif";
		#endregion

	
		#region ++ Constructeurs ++
		public CDocProcedureObjectif()
		{

		}
		#endregion

		#region ~~ Méthodes ~~
		public CDocProcedureObjectif Clone()
		{
			CDocProcedureObjectif clone = new CDocProcedureObjectif();
			return clone;
		}
		#endregion
	}

}
