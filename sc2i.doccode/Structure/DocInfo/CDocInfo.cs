using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace sc2i.doccode
{
	/// <summary>
	/// Attention le DocAssemblage permet une récupération instancié des attributs
	/// suivant :
	///		- Doc...
	/// 
	/// Il faut que ces attributs n'est pas de parametres de construction
	/// différent de 
	///		- Enumeration
	///		- int
	///		- datetime
	///		- string
	///		- double
	///		- bool
	/// </summary>
	public class CDocInfo
	{
		#region :: Propriétés ::
		private bool m_valide;
		private StreamReader m_reader;
		private string m_path;
		private List<DocMenu> m_menus;
		private List<DocRessource> m_ressources;
		private List<DocDossierRessource> m_dossiersresx;
		private List<DocNameSpace> m_ns;
		private DateTime m_datemodif;
		private List<CDocLigneAttribut> m_lstAtts;
		#endregion

		#region ++ Constructeur ++
		public CDocInfo(string path)
		{
			m_path = path;
			if (PathValide(path))
			{
				ChargerDocInfo();
				m_valide = true;
			}
			else
				m_valide = false;


		}
		#endregion

		#region ~~ Méthodes ~~

		#region Chargement
		private void ChargerDocInfo()
		{
			//Sauvegarde de la dernière modification
			FileInfo inf = new FileInfo(m_path);
			m_datemodif = inf.LastWriteTime;

			ChargerAttributs();

			ChargerDossiersRessources();
			ChargerRessources();
			ChargerMenus();
			ChargerClasses();
		}

		private void ChargerAttributs()
		{
			//m_liusing = new List<string>();
			//m_lignesatts = new List<string>();

			m_lstAtts = new List<CDocLigneAttribut>();
			m_reader = new StreamReader(m_path);
			int n = 1;
			string ligne = m_reader.ReadLine();
			string cache = "";

			#region Creation Liste des DocLignesAttributs
			while (ligne != null)
			{
				if (cache == "" && ligne.Contains("[assembly:"))
				{
					if (!ligne.Contains("]"))
						cache = ligne;
					else if (AttributSupporte(ligne))
					{
						//m_lignesatts.Add(ligne);
						CDocLigneAttribut att = new CDocLigneAttribut(n, ligne);
						if (att.AttributValide)
							m_lstAtts.Add(att);
					}
				}
				else if (cache != "")
				{
					cache += ligne;
					if (cache.Contains("]"))
					{
						//m_lignesatts.Add(ligne);
						CDocLigneAttribut att = new CDocLigneAttribut(n, ligne);
						if (att.AttributValide)
							m_lstAtts.Add(att);

						cache = "";
					}
				}
				//else if (ligne.Contains("using"))
				//{
				//    ligne = ligne.Replace(";", "");
				//    ligne = ligne.Replace("using", "");
				//    ligne = ligne.Trim();
				//    m_liusing.Add(ligne);
				//}

				n++;
				ligne = m_reader.ReadLine();
			}
			#endregion

			//List<object> lstattsobj = new List<object>();
			//foreach(string l in m_lignesatts)
			//{
			//    object o = InstancierAttribut(l);
			//    if (o != null)
			//        lstattsobj.Add(o);
			//}

		}
		public static bool AttributSupporte(string ligne)
		{
			string nomAttribut = ExtraireNomAttribut(ligne);
			if (nomAttribut == typeof(DocMenu).Name)
				return true;
			else if (nomAttribut == typeof(DocMenuItem).Name)
				return true;
			else if (nomAttribut == typeof(DocRessource).Name)
				return true;
			else if (nomAttribut == typeof(DocDossierRessource).Name)
				return true;
			else if (nomAttribut == typeof(DocLienRessourceMenu).Name)
				return true;
			else if (nomAttribut == typeof(DocLienRessourceMenuItem).Name)
				return true;
			else if (nomAttribut == typeof(DocLienRessourceRessource).Name)
				return true;
			else
				return false;

		}
		public static string ExtraireNomAttribut(string ligne)
		{
			string strrtr = "";
			if (ligne.Contains("[assembly:"))
			{
				strrtr = ligne.Replace("[assembly:", "");
				strrtr = strrtr.Replace("]", "");
				strrtr = strrtr.Trim();
				return (strrtr.Split('('))[0];
			}
			return strrtr;
		}

		private void ChargerClasses()
		{


		}
		private void ChargerMenus()
		{
			m_menus = new List<DocMenu>();
			List<DocMenuItem> lstmenusitems = new List<DocMenuItem>();

			foreach (CDocLigneAttribut li in m_lstAtts)
			{
				if (li.TypeAttribut == typeof(DocMenu))
					m_menus.Add((DocMenu)li.ObjetAttribut);
				else if (li.TypeAttribut == typeof(DocMenuItem))
					lstmenusitems.Add((DocMenuItem)li.ObjetAttribut);
			}

			for (int i = m_menus.Count; i > 0; i--)
			{
				DocMenu mnu = m_menus[i - 1];
				DocTools.CreerArborescence(ref mnu, lstmenusitems);
			}
		}
		private void ChargerRessources()
		{
			m_ressources = new List<DocRessource>();
			foreach (CDocLigneAttribut li in m_lstAtts)
				if (li.TypeAttribut == typeof(DocRessource))
					m_ressources.Add((DocRessource)li.ObjetAttribut);

		}
		private void ChargerDossiersRessources()
		{
			m_dossiersresx = new List<DocDossierRessource>();
			foreach (CDocLigneAttribut li in m_lstAtts)
				if (li.TypeAttribut == typeof(DocDossierRessource))
					m_dossiersresx.Add((DocDossierRessource)li.ObjetAttribut);

		}
		#endregion
		#region Gestion du Fichier
		public void BloquerFichierAssemblage()
		{

		}
		public void DebloquerFichierAssemblage()
		{
		}
		#endregion

		#region Sauvegarde
		public bool Sauvegarder()
		{
			//Verification que le fichier est concervé son intégrité depuis le chargement
			FileInfo inf = new FileInfo(m_path);
			if (m_datemodif != inf.LastWriteTime)
				return false;

			StreamWriter writer = new StreamWriter(m_path);
			EcrireDebutFichier(writer);
			SauvegarderMenus(writer);
			SauvegarderDossiersRessources(writer);
			SauvegarderRessources(writer);

			writer.Close();
			return true;
		}

		private int m_tabWriteMenuItm;
		private void SauvegarderMenus(StreamWriter writer)
		{
			writer.WriteLine("// ----- MENUS -----");
			foreach (DocMenu mnu in m_menus)
			{
				m_tabWriteMenuItm = 0;
				writer.WriteLine(mnu.LigneDeclarationAttributAssemblage);
				foreach (DocMenuItem itm in mnu.Enfants)
				{
					m_tabWriteMenuItm++;
					SauvegarderMenusItem(itm, writer);
				}
				writer.WriteLine();
			}
			writer.WriteLine();
		}
		private void SauvegarderMenusItem(DocMenuItem itm, StreamWriter writer)
		{
			string strTab = "";
			for (int i = 0; i < m_tabWriteMenuItm; i++)
				strTab += "    ";

			writer.WriteLine(strTab + itm.LigneDeclarationAttributAssemblage);
			foreach (DocMenuItem itmfils in itm.Enfants)
			{
				m_tabWriteMenuItm++;
				SauvegarderMenusItem(itmfils, writer);
			}
			m_tabWriteMenuItm--;
		}

		private void SauvegarderRessources(StreamWriter writer)
		{
			writer.WriteLine("// ----- RESSOURCES -----");
			foreach (DocRessource resx in m_ressources)
				writer.WriteLine(resx.LigneDeclarationAttributAssemblage);

			writer.WriteLine();
		}
		private void SauvegarderDossiersRessources(StreamWriter writer)
		{
			writer.WriteLine("// ----- DOSSIERS RESSOURCES -----");
			foreach (DocDossierRessource folderresx in m_dossiersresx)
				writer.WriteLine(folderresx.LigneDeclarationAttributAssemblage);

			writer.WriteLine();

		}

		private void EcrireDebutFichier(StreamWriter writer)
		{
			writer.WriteLine("using sc2i.doccode;");
			writer.WriteLine();
			writer.WriteLine();

		}
		#endregion


		#region Menus & Menus Items
		#endregion

		#region Ressources
		public string GetPathOfFolder(EDocRessourceType typeresx)
		{
			foreach (DocDossierRessource dossier in m_dossiersresx)
			{
				if (dossier.TypeResx == typeresx)
					return dossier.Path;
			}
			return "";
		}
		public bool UpdateFolderOf(EDocRessourceType typeresx, string nouveaupath)
		{
			if (!Directory.Exists(nouveaupath))
				return false;

			foreach (DocDossierRessource dossier in m_dossiersresx)
			{
				if (dossier.TypeResx == typeresx)
				{
					dossier.Path = nouveaupath;
					return true;
				}
			}
			return false;

		}

		public List<DocRessource> GetLstResx(EDocRessourceType? type)
		{
			List<DocRessource> resx = new List<DocRessource>();
			foreach (DocRessource r in m_ressources)
				if (type == null || r.TypeRessource == type)
					resx.Add(r);

			return resx;
		}
		public DocRessource GetResx(string idResx)
		{
			foreach (DocRessource resx in m_ressources)
			{
				if (resx.ID == idResx)
				{
					return resx;
				}
			}
			return null;
		}
		#endregion

		#region Classes & Namespace
		public DocClass GetClasse(string idCls)
		{
			DocClass cls = null;
			foreach (DocNameSpace ns in m_ns)
			{
				cls = ns.GetClasse(idCls);
				if (cls != null)
					return cls;
			}
			return cls;
		}
		#endregion
		#endregion

		#region >> Assesseurs <<
		public bool Valide
		{
			get
			{
				return m_valide;
			}
		}
		public string Path
		{
			get
			{
				return m_path;
			}
			set
			{
				m_path = value;
			}
		}
		public List<DocNameSpace> NameSpaces
		{
			get
			{
				return m_ns;
			}
			set
			{
				m_ns = value;
			}
		}
		public List<DocRessource> Ressources
		{
			get
			{
				return m_ressources;
			}
			set
			{
				m_ressources = value;
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
		public List<DocDossierRessource> DossiersRessources
		{
			get
			{
				return m_dossiersresx;
			}
			set
			{
				m_dossiersresx = value;
			}
		}
		#endregion

		#region ~[ Méthodes ]~
		public static bool PathValide(string path)
		{
			if (File.Exists(path) && path.Substring(path.Length - 3).ToLower() == ".cs")
				return true;
			else
				return false;

		}
		#endregion

	}

}
