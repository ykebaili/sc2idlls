using System;
using System.Xml;
using System.Xml.Schema;

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
	public class CDocProj
	{
		#region :: Propriétés ::
		private bool m_srcsafe;
		private bool m_installe;
		private bool m_valide;
		private CDocInfo m_docinfo;
		private Assembly m_ass;

		private XmlDocument m_projfile;
		private string m_extproj;
		private string m_pathproj;
		private string m_pathdll;
		private string m_nomass;

		private DateTime m_datemodif;

		private List<string> m_pathsfiles;
		private List<CDocLigneAttribut> m_lstAtts;
		#endregion

		#region ++ Constructeur ++
		public CDocProj(string pathproj)
		{
			m_pathproj = pathproj;
			if (ChargerProjXML())
			{
				ChargerNomAssembly();
				if (ChargerExtAssembly())
				{
					ChargerPathDLL();
					ChargerAssembly();
				}
			}


		}
		#endregion

		#region ~~ Méthodes ~~
		#region Chargement
		private bool ChargerProjXML()
		{

			//XmlTextReader reader = null;
			try
			{

				StreamReader streamReader = new StreamReader(m_pathproj);

				m_projfile = new XmlDocument();
				string xmlBuffer = "";
				using (TextReader reader = new StreamReader(m_pathproj))
				{
					xmlBuffer = reader.ReadToEnd();
					//Il faut supprimer l'attribut xmlns="" du noeud principal
					int idxAttXMLNS = xmlBuffer.ToLower().IndexOf("xmlns=\"");
					if(idxAttXMLNS != -1)
					{
						//On décale de 7 caractères à cause de la déclaration du noeud
						int idxAttXMLNSEnd = xmlBuffer.Substring(idxAttXMLNS + 7).IndexOf("\"") + 8;
						//On ajooute l'index de départ pour être sur la même base
						idxAttXMLNSEnd += idxAttXMLNS;

						xmlBuffer = xmlBuffer.Substring(0,idxAttXMLNS) + xmlBuffer.Substring(idxAttXMLNSEnd);
					}
				}
				m_projfile.LoadXml(xmlBuffer);


				if (m_projfile == null || !m_projfile.HasChildNodes)
					m_valide = false;
				else
					return true;

			}
			catch(Exception e)
			{

				m_valide = false;
			}
			return false;
			

		}
		private bool ChargerNomAssembly()
		{
			//<Project>
			//		<PropertyGroup>
			//			<AssemblyName>

			//XPath A Passer en constante ?
			XmlNode node = m_projfile.SelectSingleNode("//PropertyGroup/AssemblyName/node()");

			if(node == null)
				return false;
			
			m_nomass = node.Value;
			return true;
		}
		private bool ChargerExtAssembly()
		{
			string typeproj = m_projfile.SelectSingleNode("Project/PropertyGroup/OutputType/node()").Value.ToLower();

			switch (typeproj)
			{
				case "library":		m_extproj = "dll";		break;
				case "toto":		m_extproj = "dll";		break;
				case "tata":		m_extproj = "dll";		break;
				default:			m_extproj = "ERR";		break;
			}
			if (m_extproj == "ERR")
				return false;
			return true;
		}
		private void ChargerPathDLL()
		{

			//Chargement du noeud PropertyGroup où le sous noeud DebugType à pour valeur full
			XmlNode node = m_projfile.SelectSingleNode("Project//PropertyGroup/DebugSymbols[. = 'true']/parent::node()");

			string pathrelatif = node.SelectSingleNode("OutputPath/node()").Value;

			//Ajouter la recherche avec ../

			//Pour le moment je concatène
			m_pathdll = m_pathproj.Substring(0, m_pathproj.LastIndexOf('\\') + 1);
			if (pathrelatif.TrimEnd().Substring(pathrelatif.Length - 1) == "\\")
				m_pathdll += pathrelatif.Trim();
			else
				m_pathdll += pathrelatif + "\\";

			m_pathdll += m_nomass + "." + m_extproj;

		}

		private void ChargerAssembly()
		{
			//string oldDir = Directory.GetCurrentDirectory();
			//string dirName = Path.GetDirectoryName(m_pathproj);
			//Directory.SetCurrentDirectory(dirName);
			//string baseName = Path.GetFileName(m_pathproj);

			m_ass = null;
			try
			{
				m_ass = Assembly.LoadFrom(m_pathdll);
			}
			catch (Exception e)
			{
			}

			if (m_ass == null)
				return;

			//Sauvegarde de la dernière modification
			FileInfo inf = new FileInfo(m_pathdll);
			m_datemodif = inf.LastWriteTime;

			object[] atts = m_ass.GetCustomAttributes(typeof(DocInfoPath), false);
			if (atts.Length != 1)
				m_installe = false;
			else
			{
				Module mod = m_ass.GetModule("");
				m_installe = true;
				DocInfoPath pathdoc = (DocInfoPath)atts[0];
				m_docinfo = new CDocInfo(pathdoc.Path);
				if (!m_docinfo.Valide)
					m_installe = false;
			}

		}
		#endregion

		public void Installer(string path)
		{
			if (m_installe)
				return;

			//Créé un fichier DocInfo
			
			//Associe le fichier au projet

			//Ajoute les références dans les fichiers

		}
		public void Desinstaller()
		{
			if (!m_installe)
				return;
			//recupère le nom du fichier
			//alerte la suppression totale du fichier
			//Supprime le fichier
			//Supprime les attributs
			//Supprime les références doccode

		}

		private void AjouterUsing(string fichier, string ns)
		{
		}
		private void SupprimerUsing(string fichier, string ns)
		{
		}

		#region Gestion du Fichier
		public void BloquerFichierAssemblage()
		{

		}
		public void DebloquerFichierAssemblage()
		{
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
		public string PathFichier
		{
			get
			{
				return m_pathproj;
			}
			set
			{
				m_pathproj = value;
			}
		}
		public CDocInfo DocInfo
		{
			get
			{
				return m_docinfo;
			}
			set
			{
				m_docinfo = value;
			}
		}
		#endregion

		#region ~[ Méthodes ]~
		public static bool PathValide(string path)
		{
			if (File.Exists(path) && path.Substring(path.Length - 7).ToLower() == ".csproj")
				return true;
			else
				return false;

		}
		#endregion

	}

}
