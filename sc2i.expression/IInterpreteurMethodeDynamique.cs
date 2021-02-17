using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using sc2i.common;

namespace sc2i.expression
{
	public interface IInterpreteurMethodeDynamique
	{
		bool GetMethodInfo(string strMethode, ref MethodInfo methode, ref object objetSource);
	}

    
}
