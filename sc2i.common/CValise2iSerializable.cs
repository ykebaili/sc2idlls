using System;
using System.IO;

namespace sc2i.common
{
	/// <summary>
	/// PErmet de transporter via le réseau un objet implémentant I2iSerializable
	/// </summary>
	[Serializable]
	public class CValise2iSerializable
	{
		private byte[] m_data;
		
		public CValise2iSerializable()
		{
		}
		
		public CValise2iSerializable( I2iSerializable objetSource)
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
			serializer.TraiteObject ( ref objetSource );
			m_data = stream.GetBuffer();

            writer.Close();
            stream.Close();
		}

		/// <summary>
		/// récupère l'objet dans la valise
		/// Le data du result contient l'objet
		/// </summary>
		/// <param name="objetsAttaches">Par couple : type objet, objet attachés aux sérializers</param>
		/// <returns></returns>
		public CResultAErreur GetObjet( params object[] objetsAttaches)
		{
			I2iSerializable objet = null;
			MemoryStream stream = new MemoryStream(m_data);
			
			BinaryReader reader = new BinaryReader(stream);
			CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
			for ( int nParam = 0; nParam < objetsAttaches.Length; nParam+=2 )
				serializer.AttacheObjet ( (Type)objetsAttaches[nParam], objetsAttaches[nParam+1] );
			CResultAErreur result = serializer.TraiteObject (ref objet );
			if ( result )
				result.Data = objet;

            reader.Close();
            stream.Close();
			return result;
		}
	}
}
