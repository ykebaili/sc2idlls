using System;

#if !PDA_DATA

namespace sc2i.documents
{
	/// <summary>
	/// Description r�sum�e de CSourceDocument.
	/// </summary>
	public abstract class CSourceDocument : MarshalByRefObject, IDisposable
	{
		public CSourceDocument()
		{
		}

        public virtual void Dispose()
        {
        }

        public abstract CTypeReferenceDocument TypeReference { get;}

	}
}
#endif