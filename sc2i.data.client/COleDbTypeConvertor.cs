using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Reflection;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// 
	/// </summary>
	public class COleDbTypeConvertor
	{
		public COleDbTypeConvertor()
		{
		}

		public static OleDbType GetTypeOleDbFromType(Type tp)
		{
			if ( tp == typeof(String) || tp == typeof(string))
				return OleDbType.VarWChar;
			if ( tp == typeof(int) || tp == typeof(int?))
				return OleDbType.Integer;
			if ( tp == typeof(Int16) || tp == typeof(Int16?))
				return OleDbType.SmallInt;
			if ( tp == typeof(float ) || tp == typeof(float?))
				return OleDbType.Single;
			if ( tp == typeof(Byte) || tp == typeof(Byte?))
				return OleDbType.UnsignedTinyInt;
			if ( tp == typeof(double) || tp == typeof(double?))
				return OleDbType.Double;
			if ( tp == typeof(DateTime) || tp == typeof(CDateTimeEx) || tp == typeof(DateTime?))
				return OleDbType.Date;
			if ( tp == typeof(bool) || tp == typeof(bool?))
				return OleDbType.Boolean;
			if ( tp == typeof(CDonneeBinaireInRow) )
				return OleDbType.VarBinary;
			if ( tp == typeof(byte[]) )
				return OleDbType.Binary;
			if ( tp == typeof(object) )
				return OleDbType.Variant;
			Console.WriteLine(I.T("Data type without OleDB conversion : @1|163",tp.ToString()));
			return OleDbType.VarChar;
		}

		public static string GetStringTypeSqlFromType ( Type tp )
		{
			if ( tp != typeof(object))
				return GetTypeOleDbFromType(tp).ToString();
			else
				return "Variant";
		}
	}
}
