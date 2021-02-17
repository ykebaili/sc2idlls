using System;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
    [Serializable]
	public class CDefinitionProprieteDynamiqueVariableFormule : CDefinitionProprieteDynamique
	{
		public const string c_strCleType = "VA";

		public CDefinitionProprieteDynamiqueVariableFormule()
			:base()
		{
		}

		public CDefinitionProprieteDynamiqueVariableFormule(
			string strNom,
			CTypeResultatExpression type,
			bool bHasSubProprietes)
			:base (
			strNom,
			strNom.ToUpper(),
			type,
			bHasSubProprietes,
			false )
		{
		}

		public override string CleType
		{
			get { return c_strCleType; }
		}

	}
}
