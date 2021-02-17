using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#if PDA
#else
namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CStockeurObjetPersistant.
	/// </summary>
	public sealed class CStockeurObjetPersistant
	{

	[Serializable]
	
		private class CClassePourNull
		{
			public CClassePourNull()
			{
			}
		}

        private CStockeurObjetPersistant() { }

		/// ////////////////////////////////////////////////////////////////
		public static byte[] GetPersistantData ( C2iObjetPersistant obj )
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			if ( obj == null )
				formatter.Serialize(stream, new CClassePourNull());
			else
			{
				formatter.Serialize(stream, obj.GetType());
				byte[] data = obj.GetPersistantData();
				formatter.Serialize(stream, data );
			}
			return stream.ToArray();
		}

		/// ////////////////////////////////////////////////////////////////
		public static C2iObjetPersistant AlloueFromPersistantData ( byte[] data, params object[] constructeurParam )
		{
			if ( data == null )
				return null;
			MemoryStream stream = new MemoryStream(data);
			BinaryFormatter formatter = new BinaryFormatter();
			object obj = formatter.Deserialize(stream);
			if ( obj is CClassePourNull )
				return null;
			else
			{
				C2iObjetPersistant lobjetALire = (C2iObjetPersistant)Activator.CreateInstance((Type)obj, constructeurParam);
				byte[] objData = (byte[])formatter.Deserialize(stream);
				if ( lobjetALire.SetPersistantData(objData))
					return lobjetALire;
				return null;
			}
		}
	}
}
#endif	