using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class CDocProcedure : DocRessource
	{

		#region [[ Constantes ]]
		public const string nomBalise = "DocProcedure";

		public const string nomAttribut_Titre = "Titre";
		#endregion

		#region :: Propriétés ::
		private string m_titre;
		private List<DocRessource> m_images;
		private List<DocMenu> m_menus; 
		private List<CDocProcedurePreRequis> m_prerequis;
		private List<CDocProcedureRessource> m_ressourcesconnexes;
		private List<CDocProcedureObjectif> m_objectifs;
		private List<CDocProcedureContenu> m_contenus;
		#endregion

		#region ++ Constructeurs ++
		public CDocProcedure()
		{
			m_menus = new List<DocMenu>();
			m_objectifs = new List<CDocProcedureObjectif>();
			m_ressourcesconnexes = new List<CDocProcedureRessource>();
			m_contenus = new List<CDocProcedureContenu>();
			m_prerequis = new List<CDocProcedurePreRequis>();
		}
		#endregion

		#region >> Accesseurs <<
		public string Titre
		{
			get
			{
				return m_titre;
			}
			set
			{
				m_titre = value;
			}
		}
		public List<DocMenu> Menus
		{
			get
			{
				return m_menus;
			}
			set
			{
				m_menus = value;
			}
		}
		public List<DocRessource> Images
		{
			get
			{
				return m_images;
			}
			set
			{
				m_images = value;
			}
		}

		public List<CDocProcedureObjectif> Objectifs
		{
			get
			{
				return m_objectifs;
			}
			set
			{
				m_objectifs = value;
			}
		}
		public List<CDocProcedurePreRequis> Prerequis
		{
			get
			{
				return m_prerequis;
			}
			set
			{
				m_prerequis = value;
			}
		}
		public List<CDocProcedureContenu> Contenus
		{
			get
			{
				return m_contenus;
			}
			set
			{
				m_contenus = value;
			}
		}
		public List<CDocProcedureRessource> RessourcesConnexes
		{
			get
			{
				return m_ressourcesconnexes;
			}
			set
			{
				m_ressourcesconnexes = value;
			}
		}

		public override EDocRessourceType TypeRessource
		{
			get
			{
				return EDocRessourceType.Procedure;
			}
			set
			{
			}
		}

		#endregion

		#region ~~ Methodes ~~
		public CDocProcedure Clone()
		{
			CDocProcedure clone = new CDocProcedure();
			clone.m_titre = m_titre;
			foreach (DocMenu mnu in m_menus)
				clone.m_menus.Add((DocMenu)mnu.Clone());
			foreach (DocRessource resximg in m_images)
				clone.m_images.Add(resximg.Clone());
			foreach (CDocProcedureContenu ctnu in m_contenus)
				clone.m_contenus.Add(ctnu.Clone());
			foreach (CDocProcedureObjectif obj in m_objectifs)
				clone.m_objectifs.Add(obj.Clone());
			foreach (CDocProcedurePreRequis prerequis in m_prerequis)
				clone.m_prerequis.Add(prerequis.Clone());
			foreach (CDocProcedureRessource resx in m_ressourcesconnexes)
				clone.m_ressourcesconnexes.Add(resx.Clone());

			return clone;
		}
		public void ChargerViaXML(string path)
		{ 
			//<>
		}
		public void ChargerViaXML(XmlNode noeud)
		{
			base.ChargerViaXML(noeud);

			//Chargement des infos complémentaires relatives à la procédure
			//<RefResxConnexe>

			//<PreRequis>

			//<Objectif>
			//	<RefResx>

			//<Contenus RefMenuOrMenuItem="" >

		}


		public XmlNode GenererXML()
		{
			XmlNode root;

			//Creation d'un noeud Procedure
				//Attribut ID
				//Attribut Titre

			//Creation du noeud Prerequis
				//Noeuds des Prerequis
					//Attribut ID
					//Attribut Descript
					//Attribut Ressources

			//Creation du noeud Objectifs
				//Creation des noeuds Objectif
					//Attribut ID
					//Attribut Descrip

			//Creation du noeud Menu
				//Attribut ID
				//Attribut Nom
				//Attribut Descrip

				//Noeud Contenu (id menu parent)

				//Creation des Items
					//Attribut ID
					//Attribut Nom
					//Attribut Descrip

					//Noeud Contenu (id item parent)

			//Creation du noeud Ressources
				//Creation des noeud Ressource
					//Attribut ID
					//Attribut Nom
					//Attribut Description
					//Attribut Type
					//Attribut Path


			return null;

		}
		private XmlNode GenererXMLContenu(string idParent)
		{
			//foreach(CDocProcedureContenu contenu in m_contenus)
			//    if (contenu.IDParent == idParent)
			//    {
			//        return contenu.Contenu;
			//        break;
			//    }

			return null;
		}
		#endregion
	}
}
