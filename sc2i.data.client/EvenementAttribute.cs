using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;


namespace sc2i.data
{
	[AttributeUsage( AttributeTargets.Class, AllowMultiple = true )]
	public class EvenementAttribute : Attribute
	{

		public const string c_champEvenements = "_2I_EVENTS";
		private const char c_carSepareEvenements = '~';

		public readonly string Identifiant;
		public  string m_strLibelle;
		public readonly string Descriptif;

		public EvenementAttribute(
			string strIdentifiant,
			string strLibelle,
			string strDescriptif)
		{
			Identifiant = strIdentifiant;
			m_strLibelle = strLibelle;
			Descriptif = strDescriptif;
		}

		//--------------------------------------------------------------------
		public string Libelle
		{
			get
			{
				return m_strLibelle;
			}
		}

		//--------------------------------------------------------------------
		public override int GetHashCode()
		{
			return Identifiant.GetHashCode();
		}

		//--------------------------------------------------------------------
		public override bool Equals(object obj)
		{
			if (obj is EvenementAttribute)
				return ((EvenementAttribute)obj).Identifiant == Identifiant;
			return false;
		}



		//--------------------------------------------------------------------
		public static void ClearEvenements(CObjetDonnee objet)
		{
			if (objet.Row.Table.Columns.Contains(c_champEvenements) )
			{
				object value = objet.Row[c_champEvenements];
				if (value is string)
                    CContexteDonnee.ChangeRowSansDetectionModification(objet.Row, c_champEvenements, DBNull.Value);
			}
		}

		//--------------------------------------------------------------------
		public static void StockeDeclenchement(CObjetDonnee objet, string strIdEvenement)
		{
			if (strIdEvenement.Length == 0)
				return;
			if (!objet.Row.Table.Columns.Contains(c_champEvenements) )
				throw new Exception(I.T("Event trigeering on an object which cannot contain events|30004"));
			object val = objet.Row[c_champEvenements];
			string strVal = "";
			if (val != DBNull.Value)
				strVal = (string)val;
			if (!strVal.Contains(c_carSepareEvenements + strIdEvenement + c_carSepareEvenements))
				strVal += c_carSepareEvenements + strIdEvenement + c_carSepareEvenements;
			objet.Row[c_champEvenements] = strVal;
		}

       //--------------------------------------------------------------------
		public static void AnnuleEvenement(CObjetDonnee objet, string strIdEvenement)
		{
			if (strIdEvenement.Length == 0)
				return;
			if (!objet.Row.Table.Columns.Contains(c_champEvenements) )
                throw new Exception(I.T("Event trigeering on an object which cannot contain events|30004"));
			object val = objet.Row[c_champEvenements];
			string strVal = "";
			if (val != DBNull.Value)
				strVal = (string)val;
			strVal = strVal.Replace(c_carSepareEvenements + strIdEvenement + c_carSepareEvenements, "");
			objet.Row[c_champEvenements] = strVal;
		}


		//--------------------------------------------------------------------
		public static bool HasEvent(CObjetDonnee objet, string strIdEvenement)
		{
			if (strIdEvenement.Length == 0)
				return false;
			if (!objet.Row.Table.Columns.Contains(c_champEvenements))
				return false;
			object val = objet.Row[c_champEvenements];
			if (val != DBNull.Value)
				return ((string)val).Contains ( c_carSepareEvenements + strIdEvenement + c_carSepareEvenements );
			return false;
        }
        
        //--------------------------------------------------------------------
        public static bool HasEventsSpecifiques(CObjetDonnee objet)
        {
            if (!objet.Row.Table.Columns.Contains(c_champEvenements))
                return false;
            object val = objet.Row[c_champEvenements];
            if (val != DBNull.Value && val.ToString().Trim() != "")
                return true;
            return false;
        }


		//--------------------------------------------------------------------
		public static List<EvenementAttribute> GetEvenementsForType(Type tp)
		{
			List<EvenementAttribute> lst = new List<EvenementAttribute>();
			if (tp != null)
			{
				object[] attribts = tp.GetCustomAttributes(typeof(EvenementAttribute), true);
				foreach (EvenementAttribute evt in attribts)
					lst.Add(evt);
			}
			return lst;
		}

	}
}
