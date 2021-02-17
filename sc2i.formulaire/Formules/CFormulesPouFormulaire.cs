using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.formulaire.Formules
{
	[AutoExec("RegisterAllInAssembly")]
	public class CFormulesPourFormulaire
	{
			/// ///////////////////////////////////////////////////////////
		public static void RegisterAllInAssembly()
		{
			foreach ( Type tp in typeof(CFormulesPourFormulaire).Assembly.GetTypes() )
			{
				if ( tp.IsSubclassOf(typeof(C2iExpression)) && !tp.IsAbstract) 
				{
#if PDA
					C2iExpression exp = (C2iExpression)Activator.CreateInstance(tp);
#else
					C2iExpression exp = (C2iExpression)Activator.CreateInstance(tp, new object[0]);
#endif
					CAllocateur2iExpression.Register2iExpression(exp.IdExpression, tp );
				}
			}
		}
	}
}
