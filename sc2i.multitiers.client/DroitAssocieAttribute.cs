using System;
using System.Reflection;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CDroitAssocieAttribute.
	/// </summary>
	
	public class DroitAssocieAttribute : Attribute
	{
		public readonly string CodeDroit;
		public readonly string Libelle;
		public readonly int NumOrdre;
		public readonly string DroitParent;
		public readonly string InfoSurDroit;
		public readonly OptionsDroit OptionsPossibles;
		public readonly Type TypeAssocie;

		public DroitAssocieAttribute( 
			string strCode, 
			string strLibelle, 
			string strInfoSurDroit,
			int nNumOrdre, 
			string strDroitParent,
			OptionsDroit nOptionsPossibles,
			Type typeObjetAssocie)
		{
			CodeDroit = strCode;
			Libelle = strLibelle;
			InfoSurDroit = strInfoSurDroit;
			NumOrdre = nNumOrdre;
			DroitParent = strDroitParent;
			OptionsPossibles = nOptionsPossibles;
			TypeAssocie = typeObjetAssocie;
		}

		public DroitAssocieAttribute( 
			string strCode, 
			string strLibelle, 
			string strInfoSurDroit,
			int nNumOrdre, 
			string strDroitParent,
			OptionsDroit nOptionsPossibles)
		{
			CodeDroit = strCode;
			Libelle = strLibelle;
			InfoSurDroit = strInfoSurDroit;
			NumOrdre = nNumOrdre;
			DroitParent = strDroitParent;
			OptionsPossibles = nOptionsPossibles;
			TypeAssocie = null;
		}

		public DroitAssocieAttribute( 
			string strCode, 
			string strLibelle, 
			string strInfoSurDroit,
			int nNumOrdre, 
			string strDroitParent
			)
		{
			CodeDroit = strCode;
			Libelle = strLibelle;
			InfoSurDroit = strInfoSurDroit;
			NumOrdre = nNumOrdre;
			DroitParent = strDroitParent;
			OptionsPossibles = OptionsDroit.Aucune;
			TypeAssocie = null;
		}

		public DroitAssocieAttribute( 
			string strCode, 
			string strLibelle, 
			string strInfoSurDroit,
			int nNumOrdre
			)
		{
			CodeDroit = strCode;
			Libelle = strLibelle;
			InfoSurDroit = strInfoSurDroit;
			NumOrdre = nNumOrdre;
			DroitParent = null;
			OptionsPossibles = OptionsDroit.Aucune;
			TypeAssocie = null;
		}
	}
}
