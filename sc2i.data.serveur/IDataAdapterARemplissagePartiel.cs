using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace sc2i.data.serveur
{
	public interface IDataAdapterARemplissagePartiel
	{
		int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);
	}

    public interface IDataAdapterGerantLesModificationsParTrigger
    {
        bool TableIsModifiedByTrigger { get;set;}
    }
}
