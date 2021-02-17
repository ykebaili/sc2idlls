using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
	public interface IElementAChampPourVersion
	{
		Type TypeEntite { get; }
		string TypeChamp { get; set; }
		string FieldKey { get; set;}
		string NomChampConvivial { get; set;}
		CContexteDonnee ContexteDonnee { get;}

	}

	
}
