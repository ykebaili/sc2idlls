using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.process
{
	public interface ILanceurProcessCommandLine
	{
		/// <summary>
		/// Lance un process gr�ce � une ligne de commande
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		CResultAErreur StartProcess(string[] args);
	}
}
