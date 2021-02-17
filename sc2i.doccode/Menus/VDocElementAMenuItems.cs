using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public abstract class ADocElementAMenuItems : Attribute, IDocElementAMenuItems
	{
		#region :: Propriétés ::
		internal string _nom;
		internal int _position;
		internal string _descrip;
		internal string _id;
		internal List<DocMenuItem> _enfants;
		#endregion
		#region >> Assesseurs <<
		public List<DocMenuItem> Enfants 
		{ 
			get
			{
				return _enfants;
			}
			set
			{
				_enfants = value;
			}
		}
		public string Nom 
		{ 
			get
			{
				return _nom;
			}
			set
			{
				_nom = value;
			}
		}
		public string ID
		{ 
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}
		public string Description
		{
			get
			{
				return _descrip;
			}
			set
			{
				_descrip = value;
			}
		}
		#endregion
		#region ~~ Méthodes ~~

		/// <summary>
		/// Réorganise les enfants selon leurs position.
		/// </summary>
		public virtual void OrganiserFils()
		{
			DocMenuItemPositionComparer comparer = new DocMenuItemPositionComparer();
			_enfants.Sort(comparer);
		}

		/// <summary>
		/// Retourne le conteneur d'un fils ou null
		/// </summary>
		/// <param name="fils"></param>
		/// <returns></returns>
		public virtual IDocElementAMenuItems GetFatherOf(IDocElementAMenuItems fils)
		{
			IDocElementAMenuItems elem = null;
			foreach (DocMenuItem itm in _enfants)
			{
				if (itm.Equals(fils))
					return this;
				else
				{
					elem = itm.GetFatherOf(fils);
					if (elem != null)
						return elem;
				}
			}
			return elem;
		}

		public virtual void MonterFils(DocMenuItem fils)
		{
			int newpos = fils.Position - 1;
			if(newpos < 0)
				return;
			foreach (DocMenuItem itm in _enfants)
			{
				if (itm.Position == newpos)
				{
					itm.Position++;
					fils.Position--;
					OrganiserFils();
					break;
				}
			}
		}

		public virtual bool MonterFilsOuPetitFils(DocMenuItem fils)
		{
			IDocElementAMenuItems pere = GetFatherOf(fils);
			if (pere == null)
				return false;
			pere.MonterFils(fils);
			return true;
		}

		public abstract IDocElementAMenuItems Clone();

		public virtual bool SupprimerUnFils(IDocElementAMenuItems fils)
		{
			bool retour = false;

			for (int c = _enfants.Count; c > 0; c--)
			{
				DocMenuItem child = _enfants[c - 1];
				if (child.Equals(fils))
				{
					for (int i = _enfants.Count; i > 0; i--)
					{
						DocMenuItem autrefils = _enfants[i - 1];
						if (autrefils.Position < fils.Position)
							MonterFils(autrefils);
					}

					_enfants.Remove(child);
					OrganiserFils();
					retour = true;
					break;
				}
				else if (child.SupprimerUnFils(fils))
				{
					retour = true;
					break;
				}
			}
			return retour;
		}


		public void AjouterFils(DocMenuItem fils)
		{
			fils.Position = _enfants.Count;
			AjouterFils(fils, _enfants.Count);
		}
		public void AjouterFils(DocMenuItem fils, int pos)
		{
			for (int i = _enfants.Count; i > 0; i--)
			{
				DocMenuItem itm = _enfants[i - 1];
				if (itm.Position >= fils.Position)
					itm.Position++;
			}

			_enfants.Add(fils);
			OrganiserFils();

		}


		#endregion
	}



}
