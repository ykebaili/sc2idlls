using System;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormatteurTextBoxNumerique.
	/// </summary>
	public class CFormatteurTextBoxMasque :  CFormatteurTextBox
	{
		private string m_strMasque = "";
		/*
		format du masque : 
		# pour un nombre
		$ pour un caractère quelconque
		> pour une lettre majuscule
		< pour une lettre minuscule
		*/

		public CFormatteurTextBoxMasque(string strMasque)
		{
			m_strMasque = strMasque;
		}


		////////////////////////////////////////
		public override object GetValue ( string strValue )
		{
			return strValue;
		}

		////////////////////////////////////////
		public override string GetTextToDisplay ( string strText, ref int nPosCurseur )
		{
			string strNew = "";
			//Les caractères ne faisant pas partie du masque sont supprimés
			int nIndexMasque = 0;
			char cMasque;
			foreach ( char c in strText )
			{
				bool bDoNextCar = false;
				while ( !bDoNextCar )
				{
					bDoNextCar = true;;
					if ( nIndexMasque >= m_strMasque.Length )
						break;
					cMasque = m_strMasque[nIndexMasque];
					bool bPris = false;
					switch ( cMasque )
					{
						case '#' :
							if ( c>='0' && c <='9' )
							{
								strNew += c;
								bPris = true;
							}
							break;
						case '$' :
							strNew += c;
							nIndexMasque++;
							break;
						case '<' :
							if ( Char.IsLetter(c) )
							{
								strNew += c.ToString().ToLower();
								bPris = true;
							}
							break;
						case '>' :
							if ( Char.IsLetter(c) )
							{
								strNew += c.ToString().ToUpper();
								bPris = true;
							}
							break;
						default :
							strNew += cMasque;
							bPris = true;
							if ( c != cMasque )
							{
								bDoNextCar = false;
								if ( nIndexMasque+1 >= nPosCurseur )
									nPosCurseur++;
							}
							break;
					}
					if ( bPris )
						nIndexMasque++;
				}
			}
			
			return strNew;
		}
	}


}
