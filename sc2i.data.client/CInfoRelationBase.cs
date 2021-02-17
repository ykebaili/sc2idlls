using System;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Classe de base pour toutes les informations de relation entre des tables
	/// </summary>
	[Serializable]
	public abstract class CInfoRelationBase : I2iSerializable
	{
		public abstract string TableParente{get;}
		public abstract string TableFille{get;}
		public abstract string RelationKey{get;}

		public abstract CResultAErreur Serialize ( C2iSerializer serializer );
	}
}
