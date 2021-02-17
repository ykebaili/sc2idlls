using System;
using System.Data;
using System.Data.MySqlClient;
using System.Data.Common;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	public class CMySqlTypeMapper : IDataBaseTypeMapper
	{
		public MySqlType GetDBTypeFromType(Type tp)
		{
			if (tp == typeof(String))
				return MySqlType.NVarChar;
			if (tp == typeof(int) || tp == typeof(int?))
				return MySqlType.Int32;
			if (tp == typeof(double) || tp == typeof(double?))
				return MySqlType.Float;
			if (tp == typeof(DateTime) || tp == typeof(CDateTimeEx) || tp == typeof(DateTime?))
				return MySqlType.DateTime;
			if (tp == typeof(bool) || tp == typeof(bool?))
				return MySqlType.Byte;
			if (tp == typeof(CDonneeBinaireInRow))
				return MySqlType.Blob;
			if (tp == typeof(byte[]))
				return MySqlType.Blob;	
			if (tp == typeof(object))
				return MySqlType.NVarChar; //A CHECKER
			if (tp.IsEnum)
				return MySqlType.Int16;
            Console.WriteLine(I.T("Data type @1 without MySql conversion |197", tp.ToString()));
			return MySqlType.NVarChar;
		}

		public string GetStringDBTypeFromType(Type tp)
		{
			if (tp != typeof(object))
			{
				MySqlType typeMySql = GetDBTypeFromType(tp);
				string strtype = "";
				switch (typeMySql)
				{
					case MySqlType.BFile:					strtype = "BFILE";							break;
					case MySqlType.Blob:					strtype = "BLOB"; 							break;
					case MySqlType.Byte:					strtype = "NUMBER(1,0)";					break;
					case MySqlType.Char:					strtype = "CHAR";							break;
					case MySqlType.Clob:					strtype = "CLOB"; 							break;
					case MySqlType.Cursor:					strtype = "REF CURSOR"; 					break;
					case MySqlType.DateTime:				strtype = "DATE";							break;
					case MySqlType.Double:					strtype = "NUMBER(50,0)";					break;
					//case MySqlType.Double:					strtype = "FLOAT";							break;
					case MySqlType.Float:					strtype = "FLOAT";							break;
																		 //INTEGER Marche aussi... .?
					case MySqlType.Int16:					strtype = "NUMBER";							break;
					case MySqlType.Int32:					strtype = "NUMBER";							break;
					case MySqlType.IntervalDayToSecond:	strtype = "INTERVAL DAY TO SECOND";			break;
					case MySqlType.IntervalYearToMonth:	strtype = "INTERVAL YEAR TO MONTH"; 		break;
					case MySqlType.LongRaw:				strtype = "LONGRAW"; 						break;
					case MySqlType.LongVarChar:			strtype = "LONG"; 							break;
					case MySqlType.NChar:					strtype = "NCHAR"; 							break;
					case MySqlType.NClob:					strtype = "NCLOB"; 							break;
					case MySqlType.NVarChar:				strtype = "NVARCHAR2";						break;
					case MySqlType.Number:					strtype = "NUMBER"; 						break;
					case MySqlType.Raw:					strtype = "RAW"; 							break;
					case MySqlType.RowId:					strtype = "ROWID"; 							break;
					case MySqlType.SByte:					strtype = "UNSIGNED INTEGER";				break;
					case MySqlType.Timestamp:				strtype = "TIMESTAMP";						break;
					case MySqlType.TimestampLocal:			strtype = "TIMESTAMP WITH LOCAL TIMEZONE";	break;	
					case MySqlType.TimestampWithTZ:		strtype = "TIMESTAMP WITH TIMEZONE";		break;
					case MySqlType.UInt16:					strtype = "UNSIGNED INTEGER";				break;
					case MySqlType.UInt32:					strtype = "UNSIGNED INTEGER";				break;
					case MySqlType.VarChar:				strtype = "VARCHAR2";						break;
					default:
						break;
				}
				if(strtype != "")
					return strtype;
				else
					return typeMySql.ToString();
			}
			else
				return "sql_variant";
		}

		public string DBLongStringDefinition
		{
			get
			{
				return "NVarChar(4000)";
			}
		}
		public int MaxDBStringLength
		{
			get
			{
				return 2000;
			}
		}
		public int MinDBLongStringLength
		{
			get
			{
				return 4000;
			}
		}


		public Type GetTypeCSharpFromDBType(string strTypeCol, string strLongueur, string strPrecision, string strEchelle)
		{

			Type typertr = null;
			switch (strTypeCol)
			{
				case "NUMBER":
					if (strEchelle == "0" && strPrecision == "1")	//BOOLEAN
				        typertr = typeof(bool);
					else if (strEchelle == "0" && strPrecision == "50")	//Double
                        typertr = typeof(double);
					else
                        typertr = typeof(Int32);
					break;


				case "BLOB":
					typertr = typeof(CDonneeBinaireInRow);
					break;
				case "FLOAT":
                    typertr = typeof(double);
					break;

				case "STRING":
				case "CHAR":
					typertr = typeof(string);
					break;

				case "DATETIME":
				case "DATE":
                    typertr = typeof(DateTime);
					break;

				default:
					typertr = typeof(string);
					break;
			}

			return typertr;
		}
	}
}
