using System;
using System.Text;
using System.Globalization;

namespace sc2i.common
{
	/// 
	/*public interface IStringSerializable
	{
		void Write ( CStringSerializer serializer );
		CResultAErreur Read ( CStringSerializer serializer );
	}*/

	/// <summary>
	/// Description résumée de CStringSerializer.
	/// </summary>
	public class CStringSerializer : C2iSerializer
	{
		private string m_strChaine = "";
		private int m_nPosInChaine;

		private ModeSerialisation m_mode;
		
		public CStringSerializer( ModeSerialisation modeSerialization )
		{
			m_mode = modeSerialization;
		}

		/// //////////////////////////////////////////////
		public CStringSerializer ( string strData, ModeSerialisation modeSerialization )
		{
			m_mode = modeSerialization;
			m_strChaine = strData;
		}

		/// //////////////////////////////////////////////
		public void Seek ( int nNb )
		{
			m_nPosInChaine += nNb;
		}

		/// //////////////////////////////////////////////
		public override ModeSerialisation Mode
		{
			get
			{
				return m_mode;
			}
		}

		/// //////////////////////////////////////////////
		public void ChangeMode ( ModeSerialisation mode )
		{
			m_mode = mode;
		}

        /// //////////////////////////////////////////////
		public string String
		{
			get
			{
				return m_strChaine;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteString ( ref string strChaine )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
                    if (strChaine == null)
                        WriteString("");
                    else
					    WriteString ( strChaine );
					break;
				case ModeSerialisation.Lecture :
					strChaine = ReadString();
					break;
			}
		}
		/// //////////////////////////////////////////////
		private void WriteString ( string strChaine )
		{
			strChaine = strChaine.Replace("\\","\\\\");
			strChaine = strChaine.Replace("~", "\\~");
			//m_strChaine += strChaine.Replace("~","-")+"~";
			m_strChaine += strChaine+"~";
		}

		/// //////////////////////////////////////////////
		private string ReadString()
		{
			int nPos = m_strChaine.IndexOf('~', m_nPosInChaine);
			while ( nPos > 1 && m_strChaine[nPos-1] == '\\' )
				nPos = m_strChaine.IndexOf('~', nPos+1);
			if ( nPos < 0 )
			{
				m_nPosInChaine = m_strChaine.Length;
				return m_strChaine;
			}
			string strRetour = m_strChaine.Substring(m_nPosInChaine, nPos-m_nPosInChaine);
			strRetour = strRetour.Replace("\\~","~");
			strRetour = strRetour.Replace("\\\\","\\");
			m_nPosInChaine = nPos+1;
			return strRetour;
		}

		/// //////////////////////////////////////////////
		public override void TraiteInt ( ref int nVal )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( nVal.ToString(CultureInfo.InvariantCulture) );
					break;
				case ModeSerialisation.Lecture :
					nVal = Int32.Parse(ReadString(), CultureInfo.InvariantCulture);
					break;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteDouble ( ref double fVal )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( fVal.ToString(CultureInfo.InvariantCulture) );
					break;
				case ModeSerialisation.Lecture :
					fVal = CUtilDouble.DoubleFromString(ReadString());
					break;
			}
		}

		/////////////////////////////////////////////////////
		public override void TraiteDecimal(ref decimal dVal)
		{
			switch (Mode)
			{
				case ModeSerialisation.Ecriture:
					WriteString(dVal.ToString(CultureInfo.InvariantCulture));
					break;
				case ModeSerialisation.Lecture:
					dVal = Decimal.Parse(ReadString(), CultureInfo.InvariantCulture);
					break;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteBool ( ref bool bVal )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( bVal?"1":"0");
					break;
				case ModeSerialisation.Lecture :
					bVal = ReadString()=="1"?true:false;
					break;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteByte ( ref Byte btVal )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( btVal.ToString(CultureInfo.InvariantCulture) );
					break;
				case ModeSerialisation.Lecture :
					btVal = Byte.Parse ( ReadString(), CultureInfo.InvariantCulture);
					break;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteByteArray ( ref Byte[] bts )
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					if ( bts == null )
						WriteString("");
					else
					{
						StringBuilder sb = new StringBuilder(bts.Length);
						foreach ( Byte btTmp in bts )
							sb.Append((char)btTmp);
						WriteString( sb.ToString() );
					}
					break;		
				case ModeSerialisation.Lecture :
					string strChaineLecture = ReadString();
					bts = new Byte[strChaineLecture.Length];
					int nIndexLecture = 0;
					foreach ( char cTmp in strChaineLecture )
						bts[nIndexLecture++] = (Byte)cTmp;
					break;
					
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteLong(ref long nVal)
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( nVal.ToString(CultureInfo.InvariantCulture) );
					break;
				case ModeSerialisation.Lecture :
					nVal = Int64.Parse ( ReadString(), CultureInfo.InvariantCulture);
					break;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteFloat(ref float fVal)
		{
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					WriteString ( fVal.ToString(CultureInfo.InvariantCulture) );
					break;
				case ModeSerialisation.Lecture :
					fVal = Single.Parse ( ReadString(), CultureInfo.InvariantCulture);
					break;
			}
		}



	}
}
