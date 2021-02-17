using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;

namespace sc2i.formulaire
{
	[Serializable]
	[WndName("Label")]
	[AWndIcone("ico_txtBox.bmp")]
	public class C2iWndLabel : C2iWndLabelBase, I2iWndComposantFenetre
	{
		public C2iWndLabel()
			: base()
		{
		}

		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		public override CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "Text",
                "Text",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));
			return lst.ToArray();
		}
	}
}
