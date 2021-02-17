using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Représente un champ dans l'application, soit un champ de la base,
	/// ou un champ custom, ou autres...
	/// </summary>
	[DynamicClass("Data field")]
	public interface IChampPourVersion : I2iSerializable
	{
		[DynamicField("Field Key")]
		string FieldKey { get;}

		[DynamicField("Friendly field name")]
		string NomConvivial { get;}

		[DynamicField("Field type key")]
		string TypeChampString { get;}
	}

}
