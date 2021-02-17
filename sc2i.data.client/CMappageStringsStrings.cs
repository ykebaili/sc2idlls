using System;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;

namespace sc2i.data
{

	/// <summary>
	/// Classe de mappage de deux tableau de Strings
	/// </summary>
	public class CMappageStringsStrings : I2iSerializable
	{
		public CMappageStringsStrings()
		{

		}
		public CMappageStringsStrings(List<string> strsA, List<string> strsB)
		{
			InitMappage(strsA, strsB);
		}
		private List<string> m_strsA = new List<string>();
		public List<string> StringsA
		{
			get
			{
				return m_strsA;
			}
			set
			{
				m_strsA = value;
			}
		}

		private List<string> m_strsB = new List<string>();
		public List<string> StringsB
		{
			get
			{
				return m_strsB;
			}
			set
			{
				m_strsB = value;
			}
		}

		private string GetStringBSiMappe(string strA)
		{
			for (int nCpt = 0; nCpt < m_strsA.Count; nCpt++)
				if (m_strsA[nCpt] == strA)
					if (m_strsB.Count > nCpt)
						return m_strsB[nCpt];
					else
						break;

			return "";
		}

		//Vérifie
		public void InitMappage(List<string> strsA, List<string> strsB)
		{
			List<string> strsBValides = new List<string>();
			List<string> strsAValides = new List<string>();

			List<string> strsBOrphelins = new List<string>();
			List<string> strsAOrphelins = new List<string>();

			foreach (string strA in strsA)
				//Si le string A existait déjà avant
				if (m_strsA.Contains(strA))
				{
					//et si un mappage existait avec un string B
					string strBAnciennementMappe = GetStringBSiMappe(strA);
					if (strBAnciennementMappe != "" && strsB.Contains(strBAnciennementMappe))
					{
						//On mappe les strings
						strsBValides.Add(strBAnciennementMappe);
						strsAValides.Add(strA);
					}
					else
					{
						//Sinon le string est orphelin
						strsAOrphelins.Add(strA);
					}
				}
				else
				{
					//Sinon le string est orphelin
					strsAOrphelins.Add(strA);
				}

			//On ajoute aux strings B les strings restants
			foreach (string strB in strsB)
				if (!strsBValides.Contains(strB))
					strsBValides.Add(strB);

			//On ajoute aux strings A les strings restants
			foreach (string strAOrphelin in strsAOrphelins)
				strsAValides.Add(strAOrphelin);

			m_strsA = strsAValides;
			m_strsB = strsBValides;
		}


		//Membres
		private int GetNumVersion()
		{
			return 0;
		}
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			int nbColsA = m_strsA.Count;
			int nbColsB = m_strsB.Count;

			serializer.TraiteInt(ref nbColsA);
			serializer.TraiteInt(ref nbColsB);

			switch (serializer.Mode)
			{
				case ModeSerialisation.Ecriture:
					for (int nEntry = 0; nEntry < nbColsA; nEntry++)
					{
						string val = m_strsA[nEntry];
						serializer.TraiteString(ref val);
					}
					for (int nEntry = 0; nEntry < nbColsB; nEntry++)
					{
						string val = m_strsB[nEntry];
						serializer.TraiteString(ref val);
					}
					break;


				case ModeSerialisation.Lecture:
					m_strsA.Clear();
					for (int nEntry = 0; nEntry < nbColsA; nEntry++)
					{
						string val = "";
						serializer.TraiteString(ref val);
						m_strsA.Add(val);
					}
					m_strsB.Clear();
					for (int nEntry = 0; nEntry < nbColsB; nEntry++)
					{
						string val = "";
						serializer.TraiteString(ref val);
						m_strsB.Add(val);
					}

					break;


				default:
					break;
			}
			return result;
		}
	}
}
