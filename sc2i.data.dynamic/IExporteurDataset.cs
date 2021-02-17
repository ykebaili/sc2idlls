using System;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

using sc2i.data;
using sc2i.common;

namespace sc2i.data.dynamic
{
	//#############################################################################################
	
	public interface IDestinationExport
	{
		bool EstValidePourExport();
	}

	//#############################################################################################
	
	public class CDestinationExportFile : IDestinationExport
	{
		private string m_strFileName = null;
		//------------------------------------------------------------------------
		public CDestinationExportFile(string strFileName)
		{
			m_strFileName = strFileName;
		}
		//------------------------------------------------------------------------
		public string FileName
		{
			get
			{
				return m_strFileName;
			}
			set
			{
				m_strFileName = value;
			}
		}
		//------------------------------------------------------------------------
		public bool EstValidePourExport()
		{
			return m_strFileName != null && m_strFileName != "";
		}
	}

	//#############################################################################################
	//#############################################################################################
	//#############################################################################################

	public interface IExporteurDataset : I2iSerializable
	{
		bool ExporteStructureOnly {get;set;}
		CResultAErreur Export(DataSet ds, IDestinationExport dest);

		string ExtensionParDefaut{get;}
	}

	//#############################################################################################
	
	public class CExporteurDatasetXML : IExporteurDataset
	{
		private bool m_bExporteStructureOnly = false;
		public CExporteurDatasetXML()
		{}

		//------------------------------------------------------------------------
		public string ExtensionParDefaut
		{
			get
			{
				return "xml";
			}
		}

		//------------------------------------------------------------------------
		public CResultAErreur Export(DataSet ds, IDestinationExport dest)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!(dest is CDestinationExportFile))
			{
				result.EmpileErreur(I.T("Destination file is not of the right format|30003"));
				return result;
			}

			try
			{
				if (ExporteStructureOnly)
					ds.WriteXmlSchema(((CDestinationExportFile)dest).FileName);
				else
#if PDA
					ds.WriteXml(((CDestinationExportFile)dest).FileName);
#else
					ds.WriteXml(((CDestinationExportFile)dest).FileName, XmlWriteMode.WriteSchema);
#endif
			}
			catch
			{
				result.EmpileErreur(I.T("Error while writing the file @1|30004",((CDestinationExportFile)dest).FileName ));
			}

			return result;
		}
		//------------------------------------------------------------------------
		public bool ExporteStructureOnly
		{
			get
			{
				return m_bExporteStructureOnly;
			}
			set
			{
				m_bExporteStructureOnly = value;	
			}
		}

		//------------------------------------------------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result =  serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteBool ( ref m_bExporteStructureOnly );
			return result;
		}
	}

	//#############################################################################################
	
	public class CExporteurDatasetText : IExporteurDataset
	{
		private bool m_bExporteStructureOnly = false;
		private string m_separateurChamp = "\t";
		private string m_separateurDecimal = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
		private string m_indicateurTexte = "";
		private bool m_bLigneEntete = true;
		private Encoding m_encoding = Encoding.Unicode;
		private bool m_bMultiFile = false;
		private bool m_bMasquerClesAuto = false;
		//------------------------------------------------------------------------
		public CExporteurDatasetText()
		{}

		//------------------------------------------------------------------------
		public string ExtensionParDefaut
		{
			get
			{
				return "txt";
			}
		}
		//------------------------------------------------------------------------
		public CExporteurDatasetText(string strSeparateurChamp)
		{
			SeparateurChamp = strSeparateurChamp;
		}
		//------------------------------------------------------------------------
		public bool LigneEntete
		{
			get
			{
				return m_bLigneEntete;
			}
			set
			{
				m_bLigneEntete = value;
			}
		}
		//------------------------------------------------------------------------
		public bool MasquerClesAuto
		{
			get
			{
				return m_bMasquerClesAuto;
			}
			set
			{
				m_bMasquerClesAuto = value;
			}
		}
		//------------------------------------------------------------------------
		public bool ExporteStructureOnly
		{
			get
			{
				return m_bExporteStructureOnly;
			}
			set
			{
				m_bExporteStructureOnly = value;	
			}
		}
		//------------------------------------------------------------------------
		public bool Multifichier
		{
			get
			{
				return m_bMultiFile;
			}
			set
			{
				m_bMultiFile = value;
			}
		}
		//------------------------------------------------------------------------
		public Encoding Encodage
		{
			get
			{
				return m_encoding;
			}
			set
			{
				m_encoding = value;
			}
		}
		//------------------------------------------------------------------------
		public string IndicateurTexte
		{
			get
			{
				return m_indicateurTexte;
			}
			set
			{
				if ( value=="\"" || value=="'" || value=="")
					m_indicateurTexte = value;
			}
		}
		//------------------------------------------------------------------------
		public string SeparateurDecimal
		{
			get
			{
				return m_separateurDecimal;
			}
			set
			{
				if ( value=="." || value=="," )
					m_separateurDecimal = value;
			}
		}
		//------------------------------------------------------------------------
		public string SeparateurChamp
		{
			get
			{
				return m_separateurChamp;
			}
			set
			{
				if (value!="")
					m_separateurChamp = value;
			}
		}
		//------------------------------------------------------------------------
		private string ValeurCompatible(string strValeur)
		{
			string strValRemplacement = " ";
			string[] strVals = new string[] {"\n","\t","\r"};

			string strTemp = strValeur.Replace(SeparateurChamp, strValRemplacement);

			foreach (string strVal in strVals)
				strTemp = strTemp.Replace(strVal, strValRemplacement);

			return strTemp;
		}
		//------------------------------------------------------------------------
		private string ValeurNombre(double fNbre)
		{
			string strTemp = fNbre.ToString().Replace(",",SeparateurDecimal);
			strTemp = strTemp.Replace(".",SeparateurDecimal);
			return strTemp;
		}
		//------------------------------------------------------------------------
		private string GetValeur(object obj)
		{
			string strTemp = "";
			if (obj.GetType() == typeof(double))
				strTemp = ValeurNombre((double) obj);
			else if (obj.GetType() == typeof(string) )
			{
				strTemp = (string)obj;
				if ( IndicateurTexte!="" )
					strTemp = strTemp.Replace(IndicateurTexte,"");
				else
					strTemp = strTemp.Replace(SeparateurChamp," ");
				Regex ex = new Regex("\r\n\t");
				strTemp = ex.Replace ( strTemp, "" );
				strTemp = IndicateurTexte + strTemp + IndicateurTexte;
			}
			else
				strTemp = obj.ToString();

			return ValeurCompatible(strTemp);
		}
		//------------------------------------------------------------------------
		public CResultAErreur Export(DataSet ds, IDestinationExport dest)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!(dest is CDestinationExportFile))
			{
				result.EmpileErreur(I.T("Destination file is not of the right format|30003"));
				return result;
			}

			StreamWriter writer = null;
			if (m_bMultiFile && ds.Tables.Count > 1)
			{			
				foreach(DataTable table in ds.Tables)
				{
					string strSubFileName = "";
					string[] strVals = ((CDestinationExportFile)dest).FileName.Split('.');
					int nLen = strVals.Length;
					for(int i=0; i<nLen-1; i++)
					{
						strSubFileName+=strVals[i];
					}
					strSubFileName += "_" + table.TableName;
					strSubFileName += "." + strVals[nLen-1];

					try
					{
						writer = new StreamWriter( strSubFileName, false, Encodage );
						if ( m_bLigneEntete )
							result = WriteColumns(table, writer);
						if (!result) 
							return result;
						result = WriteRows(table, writer);
						if (!result) 
							return result;
						writer.WriteLine("");
						writer.Close();
					}
					catch
					{
						result.EmpileErreur(I.T("Error while writing the file @1|30004", ((CDestinationExportFile)dest).FileName ));
						return result;
					}
				}
			}
			else
			{
				try
				{
					writer = new StreamWriter( ((CDestinationExportFile)dest).FileName, false, Encodage );
			
					foreach(DataTable table in ds.Tables)
					{
						if (ds.Tables.Count > 1)
							writer.WriteLine(ValeurCompatible("[TABLE : " + table.TableName + "]"));
						if ( m_bLigneEntete )
							result = WriteColumns(table, writer);
						if (!result) 
							return result;
						result = WriteRows(table, writer);
						if (!result) 
							return result;
						writer.WriteLine("");
					}
					writer.Close();
				}
				catch
				{
					result.EmpileErreur(I.T("Erreur while writing  the file @1|30004" , ((CDestinationExportFile)dest).FileName ));
					return result;
				}
			}

			return result;
		}
		//------------------------------------------------------------------------
		private CResultAErreur WriteRows(DataTable table, StreamWriter writer)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strTempLine = "";

				ArrayList lstCols = new ArrayList(table.Columns);
				Regex colKeyAuto = new Regex("^Key_");
				if ( m_bMasquerClesAuto )
				{
					foreach ( DataColumn col in lstCols.ToArray() )
						if ( col.AutoIncrement || colKeyAuto.IsMatch(col.ColumnName))
							lstCols.Remove ( col );
				}

				if(!ExporteStructureOnly)
				{
					foreach(DataRow row in table.Rows)
					{
						foreach(DataColumn col in lstCols)
						{
							if (strTempLine!="")
								strTempLine += SeparateurChamp;
							strTempLine+=GetValeur(row[col]);
						}
						writer.WriteLine(strTempLine);
						strTempLine = "";
					}
				}
			}
			catch
			{
				result.EmpileErreur(I.T("Error while writing lines in the table @1|30005" , table.TableName ));
			}
			return result;
		}
		//------------------------------------------------------------------------
		private CResultAErreur WriteColumns(DataTable table, StreamWriter writer)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strTempLine = "";
				Regex colKeyAuto = new Regex("^Key_");
				foreach(DataColumn col in table.Columns)
				{
					if ( !m_bMasquerClesAuto || !colKeyAuto.IsMatch(col.ColumnName))
					{
						if (strTempLine!="")
							strTempLine += SeparateurChamp;
						strTempLine+=ValeurCompatible(col.ColumnName);
					}
				}
				writer.WriteLine(strTempLine);
			}
			catch
			{
				result.EmpileErreur(I.T("Error while writing columns in the table @1|30006" , table.TableName ));
			}
			return result;
		}
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result =  serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteBool ( ref m_bExporteStructureOnly );
			serializer.TraiteString ( ref m_separateurChamp );
			serializer.TraiteString ( ref m_separateurDecimal );
			serializer.TraiteString ( ref m_indicateurTexte );
			serializer.TraiteBool ( ref m_bLigneEntete );
			
			string strName = m_encoding.EncodingName;
			serializer.TraiteString ( ref strName );
			if ( serializer.Mode == ModeSerialisation.Lecture )
			{
				try
				{
					m_encoding = Encoding.GetEncoding ( strName );
				}
				catch
				{
					m_encoding = null;
				}
				if ( m_encoding == null )
					m_encoding = Encoding.Unicode;
			}
			serializer.TraiteBool ( ref m_bMultiFile );
			serializer.TraiteBool ( ref m_bMasquerClesAuto );
			return result;
		}
	}

	//#############################################################################################
}
