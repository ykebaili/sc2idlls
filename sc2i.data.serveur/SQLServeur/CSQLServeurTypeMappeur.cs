using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Reflection;

using sc2i.common;

namespace sc2i.data.serveur
{




	/// <summary>
	/// Description résumée de CSQLTypeConvertor.
	/// </summary>
	public class CSQLServeurTypeMappeur : IDataBaseTypeMapper
	{

		public SqlDbType GetTypeSqlFromType(Type tp)
		{
			if ( tp == typeof(String) )
				return SqlDbType.NVarChar;
			if ( tp == typeof(int) || tp == typeof(int?))
				return SqlDbType.Int;
			if ( tp == typeof(double) || tp == typeof(double?))
				return SqlDbType.Float;
			if ( tp == typeof(DateTime) || tp == typeof(CDateTimeEx) || tp == typeof(DateTime ?))
				return SqlDbType.DateTime;
			if ( tp == typeof(bool) || tp == typeof(bool?))
				return SqlDbType.Bit;
			if ( tp == typeof(CDonneeBinaireInRow) )
				return SqlDbType.Image;
			if ( tp == typeof(byte[]) )
				return SqlDbType.Binary;
			if ( tp == typeof(object) )
				return SqlDbType.Variant;
			if ( tp.IsEnum )
				return SqlDbType.Int;
			Console.WriteLine(I.T("Data type without Oracle conversion @1|197", tp.ToString()));
			return SqlDbType.NVarChar;
		}

		public string GetStringDBTypeFromType ( Type tp )
		{
			if ( tp != typeof(object))
				return GetTypeSqlFromType(tp).ToString();
			else
				return "sql_variant";
		}


		public int MaxDBStringLength
		{
			get
			{
				return 2000;
			}
		}
		public string DBLongStringDefinition
		{
			get
			{
                //return "NText";
                // NText obsolète, ne plus utiliser dans SQL Server 2008. Sera supprimé des prochaines versions
                return "NVarChar(max)";
			}
		}
		public int MinDBLongStringLength
		{
			get
			{
				return 10000;
			}
		}


		#region IDataBaseTypeConvertor Membres


		public Type GetTypeCSharpFromDBType(string strTypeCol, string strLongueur, string strPrecision, string strEchelle)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
