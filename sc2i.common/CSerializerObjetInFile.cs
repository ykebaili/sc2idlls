using System;
using System.IO;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CSerializerObjetInFile.
	/// </summary>
	public sealed class CSerializerObjetInFile
	{
        private CSerializerObjetInFile() { }

		//----------------------------------------------------------------------------------
		public static CResultAErreur ReadFromFile ( I2iSerializable objet, string strSignatureFichier, string strNomFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			FileStream stream = null;
			try
			{
				stream = new FileStream ( strNomFichier, FileMode.Open, FileAccess.Read, FileShare.Read );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("File opening error|30060"));
			}
			try
			{
				BinaryReader reader = new BinaryReader ( stream );
				string strId = reader.ReadString();
				if ( strId != strSignatureFichier )
				{
					result.EmpileErreur(I.T("The file does not contains a valid structure|30061"));
                    reader.Close();
                    return result;
				}
				CSerializerReadBinaire serializer = new CSerializerReadBinaire ( reader );
				result = objet.Serialize ( serializer );
                reader.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("File reading error|30062"));
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch{}
			}
			return result;
		}

		//----------------------------------------------------------------------------------
		public static CResultAErreur SaveToFile ( I2iSerializable objet, string strSignatureFichier, string strNomFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			FileStream stream = null;
			try
			{
				stream = new FileStream ( strNomFichier, FileMode.Create, FileAccess.Write, FileShare.None);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("File opening error|30060"));
			}
			try
			{
				BinaryWriter writer = new BinaryWriter ( stream );
				writer.Write ( strSignatureFichier );
				CSerializerSaveBinaire serializer = new CSerializerSaveBinaire ( writer );
				result = objet.Serialize ( serializer );
                writer.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("File writing error|30063"));
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch{}
			}
			return result;
		}


	}
}
