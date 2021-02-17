using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data.serveur
{
	public interface IDataBaseTypeMapper
	{
		//int GetDBTypeFromType(Type tp);
		string GetStringDBTypeFromType(Type tp);
		Type GetTypeCSharpFromDBType(string strTypeCol, string strLongueur, string strPrecision, string strEchelle);



		//Limites sur les longueurs des champs string
		string DBLongStringDefinition { get;}
		int MinDBLongStringLength { get;}

		int MaxDBStringLength { get;}

	}
}
