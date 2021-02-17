using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace sc2i.expression
{
	/// <summary>
	/// Permet d'associer des objets supplémentaire pour la fourniture
	/// et l'interpretation de propriétés dynamiques
	/// LEs objets supplémentaires sont associés par thread
	/// </summary>
	public class CAssociationsObjetsSupplementairesPourChamps
	{
		private Dictionary<int, CAssociationObjets> m_associations = new Dictionary<int, CAssociationObjets>();
		private class CAssociationObjets : Dictionary<object, ArrayList>
		{
			public void AssocieObjet(object objetPrincipal, object objetLie)
			{
				if (objetPrincipal == null)
					objetPrincipal = DBNull.Value;
				ArrayList lst = null;
				if (!TryGetValue(objetPrincipal, out lst))
				{
					lst = new ArrayList();
					this[objetPrincipal] = lst;
				}
				lst.Add(objetLie);
			}

			public void DissocieObjet(object objetPrincipal, object objetLie)
			{
				if (objetPrincipal == null)
					objetPrincipal = DBNull.Value;
				ArrayList lst = null;
				if (TryGetValue(objetPrincipal, out lst))
					lst.Remove(objetLie);
			}


			public object[] GetObjetsAssocies(object objetPrincipal)
			{
				if (objetPrincipal == null)
					objetPrincipal = DBNull.Value;
				ArrayList lstFinale = new ArrayList();
				ArrayList lst = new ArrayList();
				if (TryGetValue(objetPrincipal, out lst))
					lstFinale.AddRange(lst.ToArray());
				if (TryGetValue(DBNull.Value, out lst))
					lstFinale.AddRange(lst.ToArray());
				return lstFinale.ToArray();
			}
		}

		public void AssocieObjet(object objetPrincipal, object objetLie)
		{
			int nId = Thread.CurrentThread.ManagedThreadId;
			CAssociationObjets associations = null;
			if (!m_associations.TryGetValue(nId, out associations))
			{
				associations = new CAssociationObjets();
				m_associations[nId] = associations;
			}
			associations.AssocieObjet(objetPrincipal, objetLie);
		}

		public object[] GetObjetsAssocies(object objetPrincipal)
		{
			int nId = Thread.CurrentThread.ManagedThreadId;
			CAssociationObjets associations = null;
			if (m_associations.TryGetValue(nId, out associations))
				return associations.GetObjetsAssocies(objetPrincipal);
			return new object[0];
		}

		public void DissocieObjet(object objetPrincipal, object objetADissocier)
		{
			int nId = Thread.CurrentThread.ManagedThreadId;
			CAssociationObjets associations = null;
			if (m_associations.TryGetValue(nId, out associations))
				associations.DissocieObjet(objetPrincipal, objetADissocier);
		}
	}
}
