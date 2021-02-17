using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace sc2i.multitiers.client
{
	public class CZipFormatter : IRemotingFormatter
	{
		private BinaryFormatter m_formatter = new BinaryFormatter();
		#region IRemotingFormatter Membres

		public object Deserialize(System.IO.Stream serializationStream, HeaderHandler handler)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Serialize(System.IO.Stream serializationStream, object graph, Header[] headers)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IFormatter Membres

		public System.Runtime.Serialization.SerializationBinder Binder
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public System.Runtime.Serialization.StreamingContext Context
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public object Deserialize(System.IO.Stream serializationStream)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Serialize(System.IO.Stream serializationStream, object graph)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public System.Runtime.Serialization.ISurrogateSelector SurrogateSelector
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		#endregion
	}
}
