using System;

using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Permet d'extraire d'une requête sql simple les champs, la table et la clause where
	/// </summary>
	public class CExtracteurRequeteSqlSimple
	{
		public static CResultAErreur ExtraitElements ( string strRequete, ref string strTable,ref string[]strChamps, ref string strWhere )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				
				strRequete = strRequete.ToUpper();
				int nPosSelect = strRequete.IndexOf("SELECT");
				if ( nPosSelect < 0 )
				{
					result.EmpileErreur(I.T("'Select' was not found in the request @1|108",strRequete));
					return result;
				}
				nPosSelect += 6;
				int nPosFrom = strRequete.IndexOf("FROM");
				if ( nPosFrom < 0 )
				{
					result.EmpileErreur(I.T("'From' was not found in the request @1|167",strRequete));
					return result;
				}
				string strChampsString = strRequete.Substring(nPosSelect, nPosFrom-nPosSelect);
				string[] strChampsBrut = strChampsString.Split(',');
				strChamps = new string[strChampsBrut.Length];
				for ( int nChamp = 0; nChamp < strChampsBrut.Length; nChamp++ )
				{
					string strVal = strChampsBrut[nChamp].Trim();
					if ( strVal.Length > 2  )
					{
						if ( strVal[nChamp] == '[' )
							strVal = strVal.Substring(1);
						if ( strVal[strVal.Length-1] == ']' )
							strVal = strVal.Substring(0, strVal.Length-1);
					}
					strChamps[nChamp] = strVal;
				}
				int nPosWhere = strRequete.IndexOf("WHERE");
				if ( nPosWhere < 0 )
					nPosWhere = strRequete.Length;
				else
					strWhere = strRequete.Substring(nPosWhere+5);
				nPosFrom += 5;
				strTable = strRequete.Substring(nPosFrom, nPosWhere-nPosFrom);
				if ( strTable.Length > 2 )
				{
					if ( strTable[0] == '[' )
						strTable = strTable.Substring(1);
					if ( strTable[strTable.Length-1] == ']' )
						strTable = strTable.Substring(0, strTable.Length-1 );
				}

			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error while analysing request @1|168",strRequete));
			}
			return result;
		}


		
	}
}
