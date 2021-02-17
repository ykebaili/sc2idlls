using System;
using System.IO;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CStreamCopieur.
	/// </summary>
	public sealed class CStreamCopieur
	{
        private CStreamCopieur() { }

		public static CResultAErreur CopyStream(Stream sourceStream, Stream destStream, int nTransfertSize)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{

				BinaryReader reader = new BinaryReader(sourceStream);
				BinaryWriter writer = new BinaryWriter(destStream);

				byte[] buffer = new byte[nTransfertSize];
				int nRead = -1;
				while ( nRead != 0 )
				{
					nRead = reader.Read ( buffer, 0, nTransfertSize );
					if (nRead!=0)
						writer.Write ( buffer, 0, nRead );
				}

				reader.Close();
				writer.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Data transfer error|30064"));
			}
			return result;
		}
	}
}
