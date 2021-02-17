using System;

using sc2i.expression;
using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// La définition représente la cible d'un process
	/// </summary>
	[Serializable]
	public class CDefinitionProprieteCibleDeProcess : CDefinitionProprieteDynamique
	{
		public CDefinitionProprieteCibleDeProcess( )
			:base()
		{
		}

		public CDefinitionProprieteCibleDeProcess( CProcess process )
			:base( "Cible_Process", "CIB_PROC", new CTypeResultatExpression(process.TypeCible, false), true, true)
		{
		}
	}
}
