using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

using sc2i.common;
using System.Text;

namespace sc2i.data
{
    
	/// <summary>
	/// Description résumée de CParametreLectureCSV.
	/// </summary>
	public class CParametreLectureCSV : I2iSerializable, IParametreLectureFichier
	{
		public class CColonneCSV : I2iSerializable
		{
			public CColonneCSV()
			{
			}

			private string m_strNom = "";
			private Type m_type = null;
			private string m_strNull;
			private bool m_bNullCaseSensitive;
			private bool m_bNullMapping = false;

			public bool HasNullMapping
			{
				get
				{
					return m_bNullMapping;
				}
				set
				{
					m_bNullMapping = value;
				}
			}
			public string NullMapping
			{
				get
				{
					return m_strNull;
				}
				set
				{
					m_strNull = value;
				}
			}
			public bool NullCaseSensitive
			{
				get
				{
					return m_bNullCaseSensitive;
				}
				set
				{
					m_bNullCaseSensitive = value;
				}
			}
			public string Nom
			{
				get
				{
					return m_strNom;
				}
				set
				{
					m_strNom = value;
				}
			}
			public Type DataType
			{
				get
				{
					return m_type;
				}
				set
				{
					m_type = value;
				}
			}

			//I2iSerializable
			private int GetNumVersion()
			{
				return 0;
			}
			public CResultAErreur Serialize(C2iSerializer serializer)
			{
				int nVersion = GetNumVersion();
				CResultAErreur result = serializer.TraiteVersion(ref nVersion);
				if (!result)
					return result;

				serializer.TraiteString(ref m_strNom);

				bool bHasType = m_type != null;
				bool bHasStrNull = m_strNull != null;
				serializer.TraiteBool(ref bHasStrNull);
				serializer.TraiteBool(ref bHasType);
				serializer.TraiteBool(ref m_bNullMapping);
				serializer.TraiteBool(ref m_bNullCaseSensitive);
				if (bHasStrNull)
					serializer.TraiteString(ref m_strNull);
				else
					m_strNull = null;
				if (bHasType)
					serializer.TraiteType(ref m_type);
				else
					m_type = null;

				return result;
			}
		}
        

		public CParametreLectureCSV()
		{
		}




		private CMappageStringsStrings m_mappage = new CMappageStringsStrings();
		private char m_strSeparateur = '\t';
		private string m_strIndicateurTexte = "";
		private bool m_bNomChampsSurPremiereLigne = true;
		private char m_strSeparateurDecimales = '.';
		private bool m_bValeurNulleSiErreur = true;
        private EEncoding m_encoding = EEncoding.Default;
		//Numéro de colonne->CColonneCSV
		private Hashtable m_tableColonnes = new Hashtable();
		//----------------------------------------------------
		public CMappageStringsStrings Mappage
		{
			get
			{
				return m_mappage;
			}
			set
			{
				if (m_mappage != null)
					m_mappage = value;
			}
		}
		//----------------------------------------------------
		public bool ValeurNullSiErreur
		{
			get
			{
				return m_bValeurNulleSiErreur;
			}
			set
			{
				m_bValeurNulleSiErreur = value;
			}
		}

        //----------------------------------------------------
        public EEncoding Encodage
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

        //----------------------------------------------------
        public Encoding GetEncoding()
        {
            return new CEncoding(m_encoding).GetEncoding();
        }

		//----------------------------------------------------
		public char Separateur
		{
			get
			{
				return m_strSeparateur;
			}
			set
			{
				m_strSeparateur = value;
			}
		}
		//----------------------------------------------------
		public string IndicateurTexte
		{
			get
			{
				return m_strIndicateurTexte;
			}
			set
			{
				m_strIndicateurTexte = value;
			}
		}
		//----------------------------------------------------
		public bool NomChampsSurPremiereLigne
		{
			get
			{
				return m_bNomChampsSurPremiereLigne;
			}
			set
			{
				m_bNomChampsSurPremiereLigne = value;
			}
		}
		//----------------------------------------------------
		public char SeparateurDecimales
		{
			get
			{
				return m_strSeparateurDecimales;
			}
			set
			{
				m_strSeparateurDecimales = value;
			}
		}
	
		//----------------------------------------------------
		public bool ValideNomPourColonne(int nNumColonne, string strNom)
		{
			for (int nCol = 0; nCol < m_tableColonnes.Count; nCol++)
				if (nCol != nNumColonne && ((CColonneCSV)m_tableColonnes[nCol]).Nom == strNom)
					return false;
			return true;
		}
		public void SetColonne(int nNumColonne, CColonneCSV colonne)
		{
			if (colonne != null)
			{
				m_tableColonnes[nNumColonne] = colonne;
			}
			else
				m_tableColonnes.Remove(nNumColonne);
		}
		//----------------------------------------------------
		public CColonneCSV GetColonne(int nNumColonne)
		{
			object result = m_tableColonnes[nNumColonne];
			if (result == null)
				return null;
			return (CColonneCSV)result;
		}
		//----------------------------------------------------
		public string[] GetDatas(string strLigne)
		{
			if (IndicateurTexte == "")
				return strLigne.Split(Separateur);
			else
			{
				//On est obligé d'y aller char par char
				ArrayList lstData = new ArrayList();
				string strData = "";
				bool bInTexte = false;
				char cInd = m_strIndicateurTexte[0];
				bool bDataAdded = false;
				foreach (char c in strLigne)
				{
					if (c == cInd)
						bInTexte = !bInTexte;
					else if (!bInTexte && c == m_strSeparateur)
					{
						lstData.Add(strData);
						strData = "";
						bDataAdded = true;
					}
					else
					{
						strData += c;
						bDataAdded = false;
					}
				}
				if (!bDataAdded)
					lstData.Add(strData);
				return (string[])lstData.ToArray(typeof(string));
			}
		}
		

		/// <summary>
		/// Importe un fichier. Le data du result contient un datatable
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public CResultAErreur LectureFichier(string strFichier)
		{
			CResultAErreur result = CResultAErreur.True;

			StreamReader reader = new StreamReader(strFichier, new CEncoding(m_encoding).GetEncoding());

			DataTable table = new DataTable();

			try
			{
                //string strLigne = reader.ReadLine();
                string strLigne = GetCSVLine(reader);
				string[] strDatas = strLigne.Split(m_strSeparateur);
				int nCol = 1;
				int nLigne = 1;

				//Creation des colonnes
				Dictionary<DataColumn, CColonneCSV> mappageCols = new Dictionary<DataColumn, CColonneCSV>();
				foreach (string strData in strDatas)
				{
					//Nom Table
					string strNom = "Column " + nCol.ToString();
					CColonneCSV colCSV = (CColonneCSV)m_tableColonnes[nCol - 1];
					if (NomChampsSurPremiereLigne)
						strNom = strData;
					if (colCSV != null && colCSV.Nom != "")
						strNom = colCSV.Nom;

					int nIndex = 0;
					while (table.Columns[strNom] != null)
					{
						nIndex++;
						strNom = strNom + "_" + nIndex.ToString();
					}

					Type tp = typeof(string);
					if (colCSV != null && colCSV.DataType != null)
						tp = colCSV.DataType;
					DataColumn col = new DataColumn(strNom, tp);
					mappageCols.Add(col, colCSV);
					table.Columns.Add(col);
					col.AllowDBNull = true;
					nCol++;
				}
				if (NomChampsSurPremiereLigne)
				{
					//strLigne = reader.ReadLine();
                    strLigne = GetCSVLine(reader);
					nLigne++;
				}

				//Lecture Table
				while (strLigne != null)
				{
					if (strLigne.Trim().Length > 0)
					{
						strDatas = GetDatas(strLigne);
						nCol = 0;
						DataRow row = table.NewRow();
						foreach (string strData in strDatas)
						{
							if (nCol < table.Columns.Count)
							{
								DataColumn col = table.Columns[nCol];
								CColonneCSV colCSV = mappageCols[col];
								try
								{
									if (colCSV != null)
									{
										if (colCSV.HasNullMapping)
											if (colCSV.NullCaseSensitive)
											{
												if (strData == colCSV.NullMapping)
												{
													row[col] = DBNull.Value;
													nCol++;
													continue;
												}
											}
											else if (strData.ToUpper() == colCSV.NullMapping.ToUpper())
											{
												row[col] = DBNull.Value;
												nCol++;
												continue;
											}
									}

									if (col.DataType == typeof(string))
										row[col] = strData;

									else if (col.DataType == typeof(double) || col.DataType == typeof(int) ||
										col.DataType == typeof(float))
									{
										double fVal = CUtilDouble.DoubleFromString(strData);
										if (col.DataType == typeof(int))
											row[col] = (int)fVal;
										if (col.DataType == typeof(float))
											row[col] = (float)fVal;
										else
											row[col] = fVal;
									}
									else if (col.DataType == typeof(DateTime))
									{
										DateTime dt = CUtilDate.DateFromString(strData);
										row[col] = dt;
									}
									else if (col.DataType == typeof(bool))
									{
										string valBool = strData.ToUpper();
										bool val = (valBool == "1" || valBool == "O" || valBool == "OUI" || valBool == "Y" || valBool == "YES" || valBool == "TRUE");
										row[col] = val;
									}
									else
									{
										object val = Convert.ChangeType(strData, col.DataType);
										row[col] = val;
									}
								}
								catch
								{
									if (ValeurNullSiErreur)
										row[col] = DBNull.Value;
									else
									{
										result.EmpileErreur(I.T("Conversion error : line @1 column @2 : the value cannot be converted into @3|30003"
										, nLigne.ToString(), col.ColumnName, DynamicClassAttribute.GetNomConvivial(col.DataType)));
										return result;
									}
								}
							}
							nCol++;
						}
						table.Rows.Add(row);
					}
					//strLigne = reader.ReadLine();
                    strLigne = GetCSVLine(reader);
					nLigne++;
				}
				result.Data = table;
				return result;
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
				return result;
			}
			finally
			{
				try
				{
					reader.Close();
				}
				catch { }
			}
		}

        //-------------------------------------------------
        public DataColumn[] GetColonnes()
        {
            List<DataColumn> lst = new List<DataColumn>();
            foreach (CColonneCSV colonne in m_tableColonnes.Values)
            {
                DataColumn col = new DataColumn(colonne.Nom, colonne.DataType);
                lst.Add(col);
            }
            return lst.ToArray();
        }

        //-------------------------------------------------
        public string GetCSVLine(StreamReader reader)
        {
            char cRead = '\0';
            string strLine = "";
            int iNextCharValue = reader.Peek();
            if (iNextCharValue < 0)
                return null;

            bool bIsGuillemet = false;

            while (((cRead = (char)reader.Read()) != '\r' || bIsGuillemet) && iNextCharValue >= 0)
            {
                if (cRead == '\"')
                    bIsGuillemet = !bIsGuillemet;
                if ((cRead == '\n') || (cRead == '\"'))
                    continue;

                else
                    strLine += cRead;
                iNextCharValue = reader.Peek();
            }

            cRead = (char)reader.Read();

            return strLine;
        }


		//I2iSerializable
		private int GetNumVersion()
		{
            //return 1;
            return 3; 
            //2: Ajout de m_bValeurNulleSiErreur
            //3 : Ajout de l'encodage

		}
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			string strTmp = m_strSeparateur + "";
			serializer.TraiteString(ref strTmp);
			if (strTmp.Length > 0)
				m_strSeparateur = strTmp[0];

			serializer.TraiteString(ref m_strIndicateurTexte);

			serializer.TraiteBool(ref m_bNomChampsSurPremiereLigne);

			strTmp = m_strSeparateurDecimales + "";
			serializer.TraiteString(ref strTmp);
			if (strTmp.Length > 0)
				m_strSeparateurDecimales = strTmp[0];

			int nNbTypes = m_tableColonnes.Count;
			serializer.TraiteInt(ref nNbTypes);
			
				//serializer.TraiteObject(ref m_mappage, new object[] { });
			switch (serializer.Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (DictionaryEntry entry in m_tableColonnes)
					{
						int nNum = (int)entry.Key;
						CColonneCSV col = (CColonneCSV)entry.Value;
						serializer.TraiteInt(ref nNum);
						result = col.Serialize(serializer);
						if (!result)
							return result;
					}
					
					break;
				case ModeSerialisation.Lecture:
					m_tableColonnes.Clear();
					for (int nEntry = 0; nEntry < nNbTypes; nEntry++)
					{
						int nNum = 0;
						CColonneCSV col = new CColonneCSV();
						serializer.TraiteInt(ref nNum);
						result = col.Serialize(serializer);
						if (!result)
							return result;
						m_tableColonnes[nNum] = col;
					}
					break;
			}

			if (nVersion > 0)
				result = m_mappage.Serialize(serializer);
            if (nVersion > 1)
                serializer.TraiteBool(ref m_bValeurNulleSiErreur);
            if ( nVersion >= 3 )
            {
                int nTmp = (int)m_encoding;
                serializer.TraiteInt ( ref nTmp);
                m_encoding = (EEncoding)nTmp;
            }
                

			if (!result)
				return result;
			return result;
		}
	}
}
