using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{

	/// <summary>
	/// Type d'opération sur un objet dans  d'une version
	/// </summary>
	public class CTypeOperationSurObjet : CEnumALibelle<CTypeOperationSurObjet.TypeOperation>
	{
		public enum TypeOperation
		{
			Ajout = 0,
			Suppression = 10,
			Modification = 20,
			Aucune = 30
		}

		public CTypeOperationSurObjet(TypeOperation operation)
			: base(operation)
		{
		}

        [DynamicField("Label")]
		public override string Libelle
		{
			get
			{
				switch (Code)
				{
					case TypeOperation.Ajout:
						return (I.T("Add|178"));
					case TypeOperation.Suppression:
						return (I.T("Delete|179"));
					case TypeOperation.Modification:
						return (I.T("Modify|180"));
					case TypeOperation.Aucune:
						return I.T("None|183");
					default:
						return "";
				}
			}
		}


	}
}
