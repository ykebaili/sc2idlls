using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
	//-------------------------------------
	//-------------------------------------
	//-------------------------------------
    /// <summary>
    /// La catégorie de version de données est utilisée pour différencier différents types de versions de données prévisionnelles.
    /// </summary>
    /// <remarks>
    /// Les catégories de version de données ne sont applicables qu'aux seules versions prévisionnelles.
    /// </remarks>
    /// <seealso cref="CVersionDonnees">Versions de données</seealso>
	[ObjetServeurURI("CCategorieVersionDonneesServeur")]
	[Table(CCategorieVersionDonnees.c_nomTable, CCategorieVersionDonnees.c_champId, true)]
	[DynamicClass("Data version category")]
	[Unique(true, "Category label should be unique|197", CCategorieVersionDonnees.c_champId)]
	public class CCategorieVersionDonnees : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
	{
		public const string c_nomTable = "DATA_VERSION_CATEGORY";

		public const string c_champId = "DVC_ID";
		public const string c_champLibelle = "DVC_LABEL";

		public CCategorieVersionDonnees(CContexteDonnee contexte)
			: base(contexte)
		{
		}

		//-*------------------------------------
		public CCategorieVersionDonnees(DataRow row)
			: base(row)
		{
		}

		//-*------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] { c_champLibelle };
		}

		//-------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}

		//-------------------------------------
		public override string DescriptionElement
		{
			get { return I.T("Data version category @1|196", Libelle); }
		}

		//-------------------------------------
		/// <summary>
		/// Libellé de la catégorie de version de données.
		/// </summary>
        [TableFieldProperty(CCategorieVersionDonnees.c_champLibelle, 255)]
		[DynamicField("Label")]
		public string Libelle
		{
			get
			{
				return (string)Row[c_champLibelle];
			}
			set
			{
				Row[c_champLibelle] = value;
			}
		}

		//-------------------------------------
		
		//---------------------------------------------
		/// <summary>
		/// Liste des versions appartenant à cette catégorie.
		/// </summary>
		[RelationFille(typeof(CVersionDonnees), "CategorieDeVersion")]
		[DynamicChilds("data versions", typeof(CVersionDonnees))]
		public CListeObjetsDonnees Versions
		{
			get
			{
				return GetDependancesListe(CVersionDonnees.c_nomTable, c_champId);
			}
		}
	}
}
