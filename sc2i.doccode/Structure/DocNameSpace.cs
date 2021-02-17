using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class DocNameSpace
	{
		#region :: Propriétés ::
		private List<DocClass> m_classes;
		private List<DocNameSpace> m_nss;
		private DocNameSpace m_nsparent;
		private string m_nom;
		#endregion
		#region ++ Constructeur ++
		public DocNameSpace()
		{

		}
		#endregion

		#region >> Assesseurs <<
		public List<DocClass> Classes
		{
			get
			{
				return m_classes;
			}
			set
			{
				m_classes = value;
			}
		}
		public List<DocNameSpace> NameSpaces
		{
			get
			{
				return m_nss;
			}
			set
			{
				m_nss = value;
			}
		}
		public string Nom
		{
			get
			{
				return m_nom;
			}
			set
			{
				m_nom = value;
			}
		}
		public string ID
		{
			get
			{
				if (NameSpaceParent != null)
					return NameSpaceParent.ID + "." + m_nom;
				else
					return m_nom;
			}
		}
		public DocNameSpace NameSpaceParent
		{
			get
			{
				return m_nsparent;
			}
			set
			{
				m_nsparent = value;
			}
		}

		#endregion
		#region ~~ Méthodes ~~
		public DocClass GetClasse(string idCls)
		{
			DocClass cls = null;
			foreach (DocClass c in m_classes)
				if (c.ID == idCls)
					return c;

			foreach(DocNameSpace ns in m_nss)
			{
				cls = GetClasse(idCls);
				if(cls != null)
					return cls;
			}

			return cls;

		}
		#endregion

	}

}
