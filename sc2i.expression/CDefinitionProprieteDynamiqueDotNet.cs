using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Les définitions correspondant  à des propriétés .Net
	/// </summary>
	public class CDefinitionProprieteDynamiqueDotNet : CDefinitionProprieteDynamique
	{
		public const string c_strCleTypeDefinition = "PP";

		//-----------------------------------------------
		public CDefinitionProprieteDynamiqueDotNet()
			: base()
		{
		}
		
		//-----------------------------------------------
		public CDefinitionProprieteDynamiqueDotNet
			(
			string strNomConvivial,
			string strNomPropriete,
			CTypeResultatExpression type,
			bool bHasSubProprietes,
			bool bIsReadOnly,
			string strRubrique
			)
			: base(strNomConvivial,
			strNomPropriete,
			type,
			bHasSubProprietes,
			bIsReadOnly,
			strRubrique)
		{
		}

		//----------------------------------------------------
		public override string CleType
		{
			get
			{
				return c_strCleTypeDefinition;
			}
		}
	}
}
