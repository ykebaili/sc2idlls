using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Utilis� pour retourner un droit sans d�tail, par exemple pour un administrateur
	/// </summary>
	[Serializable]
	public class CDonneeDroitSysteme : IDonneeDroitUtilisateur
	{
		private string m_strCodeDroit = "";
		public CDonneeDroitSysteme( string strCodeDroit)
		{
			m_strCodeDroit = strCodeDroit;
		}

		public string CodeDroit
		{
			get
			{
				return m_strCodeDroit;
			}
		}
	}
}
