using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;


namespace sc2i.data
{

	public class RelationVersionDonneesObjetAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get { return CVersionDonneesObjet.c_nomTable; }
		}

		public override string ChampType
		{
			get { return CVersionDonneesObjet.c_champTypeElement; }
		}

		public override string ChampId
		{
			get { return CVersionDonneesObjet.c_champIdElement; }
		}

		public override bool Composition
		{
			get { return false; }
		}

		public override bool AppliquerContrainteIntegrite
		{
			get
			{
				return false;
			}
		}

		public override bool CanDeleteToujours
		{
			get { return true; }
		}

		protected override string MyIdRelation
		{
			get { return "REL_VERSION_DATA"; }
		}

		public override string NomConvivialPourParent
		{
			get { return "Versions"; }
		}

		public override int Priorite
		{
			get { return 100; }
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp);
		}
	}
}
