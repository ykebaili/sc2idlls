using System;
using System.Collections;

using sc2i.common;

#if !PDA_DATA

namespace sc2i.documents
{
	/// <summary>
	/// Description résumée de TypeReferenceDocument.
	/// </summary>
	[Serializable]
	public class CTypeReferenceDocument
	{
		//--------------------------------------------------------------------------
		public enum TypesReference
		{
			Fichier = 0,
			LienDirect = 1
		}
		//--------------------------------------------------------------------------
		private TypesReference m_type;
		//--------------------------------------------------------------------------
		public CTypeReferenceDocument(TypesReference tp)
		{
			m_type = tp;
		}
		//--------------------------------------------------------------------------
		public override string ToString()
		{
			if (this.m_type == TypesReference.Fichier)
				return I.T("Internal Application File Management|20000");
            if (this.m_type == TypesReference.LienDirect)
                return I.T("Link to File|20001");
			return "";
		}
		//--------------------------------------------------------------------------
		[DynamicField("Label")]
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return ToString();
			}
		}
		//--------------------------------------------------------------------------
		[DynamicField("Code")]
		public int CodeInt
		{
			get
			{
				return (int) m_type;
			}
		}
		//--------------------------------------------------------------------------
		public TypesReference Code
		{
			get
			{
				return m_type;
			}
		}
		//--------------------------------------------------------------------------
		public int CompareTo(object obj)
		{
			if (! (obj is CTypeReferenceDocument) ) 
				return 1;

			return ( this.Libelle.CompareTo( ((CTypeReferenceDocument)obj).Libelle) );
		}
		//--------------------------------------------------------------------------
		public override bool Equals ( object obj )
		{
			return CompareTo ( obj ) == 0;
		}
		//--------------------------------------------------------------------------
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		//--------------------------------------------------------------------------
		public static CTypeReferenceDocument[] Types
		{
			get
			{
				ArrayList lst = new ArrayList();
				lst.Add(new CTypeReferenceDocument( TypesReference.Fichier ));
				lst.Add(new CTypeReferenceDocument( TypesReference.LienDirect ));
				return (CTypeReferenceDocument[])lst.ToArray(typeof(CTypeReferenceDocument));
			}
		}
		//--------------------------------------------------------------------------
	}
}
#endif