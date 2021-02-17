using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;
using System.Collections;


namespace sc2i.data
{
	//-------------------------------------
	public class CTypeVersion : CEnumALibelle<CTypeVersion.TypeVersion>
	{
		//-------------------------------------
		public enum TypeVersion
		{
			Archive = 0,
			Previsionnelle = 10,
			Etiquette = 20
		}

		//-------------------------------------
		public CTypeVersion(TypeVersion typeVersion)
			: base(typeVersion)
		{
		}

		//-------------------------------------
        [DynamicField("Label")]
		public override string Libelle
		{
			get
			{
				switch (Code)
				{
					case TypeVersion.Archive:
						return (I.T("Archive|181"));
					case TypeVersion.Previsionnelle:
						return (I.T("Planified|182"));
                    case TypeVersion.Etiquette:
                        return (I.T("SnapShot|185"));
                    default:
						return "";
				}
			}
		}







	}

}
