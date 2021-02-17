using System;
using System.Windows.Forms;
using System.IO;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CSerializerObjetInClipBoard.
	/// </summary>
	public class CSerializerObjetInClipBoard
	{

		public static CResultAErreur Copy ( I2iSerializable objet, string strSignatureFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( objet == null )
			{
				result.EmpileErreur(I.T("Nothing to copy|30048"));
				return result;
			}
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter (stream);
			CSerializerSaveBinaire serializer = new CSerializerSaveBinaire ( writer );
			result = serializer.TraiteObject ( ref objet );
			if ( result )
			{
				DataObject data = new DataObject( strSignatureFichier, stream.GetBuffer() );
				Clipboard.SetDataObject ( data );
			}
			writer.Close();
            stream.Close();
			return result;
		}

		public static CResultAErreur Paste ( ref I2iSerializable objet, string strSignatureFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			IDataObject data = Clipboard.GetDataObject ( );
			if ( data == null || !data.GetDataPresent ( strSignatureFichier ) )
			{
                result.EmpileErreur(I.T("Nothing to paste|30049"));
				return result;
			}
			byte[] buffer = (byte[])data.GetData ( strSignatureFichier );
			MemoryStream stream = new MemoryStream ( buffer );
			BinaryReader reader = new BinaryReader ( stream );
			CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
            serializer.IsForClone = true;
			result = serializer.TraiteObject ( ref objet );
			reader.Close();
            stream.Close();
			return result;
		}

		public static bool IsObjetInClipboard ( string strSignatureFichier )
		{
			IDataObject objet = Clipboard.GetDataObject();
			if ( objet != null && objet.GetDataPresent ( strSignatureFichier ) )
				return true;
			return false;
		}
	}
}
