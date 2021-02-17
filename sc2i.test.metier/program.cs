using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.common.unites;
using sc2i.common.unites.standard;
using sc2i.common;

namespace sc2i.test.metier
{
	class CProgramme
	{

		[STAThread]
		static void Main()
		{
			System.Console.WriteLine("Démarrage");

            //CAutoexecuteurClasses.RunAutoexecs();

                        
            Application.Run(new CFormMain());
		}
	}
}
