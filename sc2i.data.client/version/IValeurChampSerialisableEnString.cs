using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Permet à une donnée de retourner une description string
	/// </summary>
	public interface IValeurChampConvertibleEnString
	{
		byte[] GetValBlob();
		object ValeurChamp { get; set;}
		string GetStringSerialisation();
		int? GetCodeOperation();
	}

}
