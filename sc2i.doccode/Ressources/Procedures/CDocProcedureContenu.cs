using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class CDocProcedureContenu
	{
		#region [[ Constantes ]]
		public const string nomBalise = "Contenu";
		#endregion

		#region :: Propriétés ::
		private string m_id;
		private XmlNode m_contenu; //XML Inside
		private List<DocRefMenusOrMenuItems> m_lstRefs;

		#endregion

		#region ++ Constructeurs ++
		public CDocProcedureContenu()
		{

		}
		#endregion

		#region >> Accesseurs <<
		public string ID
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}
		public XmlNode Contenu
		{
			get
			{
				return m_contenu;
			}
			set
			{
				m_contenu = value;
			}
		}
		public List<DocRefMenusOrMenuItems> ReferencesParentes
		{
			get
			{
				return m_lstRefs;
			}
			set
			{
				m_lstRefs = value;
			}
		}
		#endregion

		#region ~~ Méthodes ~~
		public CDocProcedureContenu Clone()
		{
			CDocProcedureContenu clone = new CDocProcedureContenu();
			clone.m_contenu = m_contenu;
			clone.m_id = m_id;

			foreach (DocRefMenusOrMenuItems refmnu in m_lstRefs)
				clone.m_lstRefs.Add(refmnu.Clone());

			return clone;
		}
		#endregion
	}

}
