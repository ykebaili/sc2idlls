using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace sc2i.data
{
	public interface IObjetDonneeAValeurParDefautOptim
	{
		void DefineValeursParDefaut(DataTable table);
	}
}
