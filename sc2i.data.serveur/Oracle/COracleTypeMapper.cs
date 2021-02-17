using System;
using System.Data;
using System.Data.OracleClient;
using System.Data.Common;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	public class COracleTypeMapper : IDataBaseTypeMapper
	{
		public OracleType GetDBTypeFromType(Type tp)
		{
			if (tp == typeof(String))
				return OracleType.NVarChar;
			if (tp == typeof(int) || tp == typeof(int?))
				return OracleType.Int32;
			if (tp == typeof(double) || tp == typeof(double?))
				return OracleType.Float;
			if (tp == typeof(DateTime) || tp == typeof(CDateTimeEx) || tp == typeof(DateTime?))
				return OracleType.DateTime;
			if (tp == typeof(bool) || tp == typeof(bool?))
				return OracleType.Byte;
			if (tp == typeof(CDonneeBinaireInRow))
				return OracleType.Blob;
			if (tp == typeof(byte[]))
				return OracleType.Blob;	
			if (tp == typeof(object))
				return OracleType.NVarChar; //A CHECKER
			if (tp.IsEnum)
				return OracleType.Int16;
            Console.WriteLine(I.T("Data type @1 without Oracle conversion |197", tp.ToString()));
			return OracleType.NVarChar;
		}

		public string GetStringDBTypeFromType(Type tp)
		{
			if (tp != typeof(object))
			{
				OracleType typeoracle = GetDBTypeFromType(tp);
				string strtype = "";
				switch (typeoracle)
				{
					case OracleType.BFile:					strtype = "BFILE";							break;
					case OracleType.Blob:					strtype = "BLOB"; 							break;
					case OracleType.Byte:					strtype = "NUMBER(1,0)";					break;
					case OracleType.Char:					strtype = "CHAR";							break;
					case OracleType.Clob:					strtype = "CLOB"; 							break;
					case OracleType.Cursor:					strtype = "REF CURSOR"; 					break;
					case OracleType.DateTime:				strtype = "DATE";							break;
					case OracleType.Double:					strtype = "NUMBER(50,0)";					break;
					//case OracleType.Double:					strtype = "FLOAT";							break;
					case OracleType.Float:					strtype = "FLOAT";							break;
																		 //INTEGER Marche aussi... .?
					case OracleType.Int16:					strtype = "NUMBER";							break;
					case OracleType.Int32:					strtype = "NUMBER";							break;
					case OracleType.IntervalDayToSecond:	strtype = "INTERVAL DAY TO SECOND";			break;
					case OracleType.IntervalYearToMonth:	strtype = "INTERVAL YEAR TO MONTH"; 		break;
					case OracleType.LongRaw:				strtype = "LONGRAW"; 						break;
					case OracleType.LongVarChar:			strtype = "LONG"; 							break;
					case OracleType.NChar:					strtype = "NCHAR"; 							break;
					case OracleType.NClob:					strtype = "NCLOB"; 							break;
					case OracleType.NVarChar:				strtype = "NVARCHAR2";						break;
					case OracleType.Number:					strtype = "NUMBER"; 						break;
					case OracleType.Raw:					strtype = "RAW"; 							break;
					case OracleType.RowId:					strtype = "ROWID"; 							break;
					case OracleType.SByte:					strtype = "UNSIGNED INTEGER";				break;
					case OracleType.Timestamp:				strtype = "TIMESTAMP";						break;
					case OracleType.TimestampLocal:			strtype = "TIMESTAMP WITH LOCAL TIMEZONE";	break;	
					case OracleType.TimestampWithTZ:		strtype = "TIMESTAMP WITH TIMEZONE";		break;
					case OracleType.UInt16:					strtype = "UNSIGNED INTEGER";				break;
					case OracleType.UInt32:					strtype = "UNSIGNED INTEGER";				break;
					case OracleType.VarChar:				strtype = "VARCHAR2";						break;
					default:
						break;
				}
				if(strtype != "")
					return strtype;
				else
					return typeoracle.ToString();
			}
			else
				return "sql_variant";
		}

		public string DBLongStringDefinition
		{
			get
			{
				return "NCLOB";
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
