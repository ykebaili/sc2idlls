using System;
using System.Collections.Generic;

using sc2i.common;
using sc2i.data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CObjetHierarchique.
	/// </summary>
	public abstract class CObjetHierarchique : CObjetDonneeAIdNumeriqueAuto, IObjetHierarchiqueACodeHierarchique 
	{

		//-------------------------------------------------------------------
		public CObjetHierarchique( CContexteDonnee ctx )
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CObjetHierarchique( System.Data.DataRow row )
			:base(row)
		{
		}

		/// <summary>
		/// Retourne le nombre de caractères utilisés pour créer la hierarchie des niveaux
		/// </summary>
		public abstract int NbCarsParNiveau{get;}

		public abstract string ChampCodeSystemePartiel{get;}
		public abstract string ChampCodeSystemeComplet{get;}
		public abstract string ChampNiveau{get;}
		public abstract string ChampLibelle{get;}
		public abstract string ChampIdParent{get;}

		//-------------------------------------------------------------------
		public string CodePartielDefaut
		{
			get
			{
				return "".PadLeft(NbCarsParNiveau,'-');
			}
		}
	
		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
			Row[ChampCodeSystemePartiel] = CodePartielDefaut;
			Row[ChampCodeSystemeComplet] = 0;
			Row[ChampNiveau] = 0;
		}

		

		//-------------------------------------------------------------------
		/// <summary>
		/// Code partiel de l'objet ( code dans sa famille principale )
		/// </summary>
		[DynamicField("Partial system code")]
		public abstract string CodeSystemePartiel{get;}
		


		//-------------------------------------------------------------------
		/// <summary>
		/// Code complet de l'objet, incluant les codes
		/// des objets de niveau supérieur
		/// </summary>
		[DynamicField("Full system code")]
		public abstract string CodeSystemeComplet{get;}

		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne un tableau de tous les codes systèmes des parents
		/// </summary>
		public string[] CodesSystemesCompletsParents
		{
			get
			{
				List<string> lst = new List<string>();
				IObjetHierarchiqueACodeHierarchique parent = ObjetParent;
				while (parent != null)
				{
					lst.Add(parent.CodeSystemeComplet);
					parent = parent.ObjetParent;
				}
				return lst.ToArray();
			}
		}

		

		//-------------------------------------------------------------------
		/// <summary>
		/// Niveau de l'objet. l'objet 'root' est au niveau 0.
		/// </summary>
		[DynamicField("Level")]
		public abstract int Niveau{get;}

		//-------------------------------------------------------------------
		[DescriptionField(true)]
		public abstract string Libelle{get;set;}
		
		//-------------------------------------------------------------------
		[DescriptionField(false)]
		[DynamicField("Full label")]
		public virtual string LibelleComplet
		{
			get
			{
				string strText = "";
				if (ObjetParent != null)
					strText = ((CObjetHierarchique)ObjetParent).LibelleComplet + "/";
				strText += Libelle;
				return strText;
			}
		}

        //-------------------------------------------------------------------
        public IObjetDonneeAutoReference ObjetAutoRefParent
        {
            get
            {
                return ObjetParent;
            }
        }

        //-------------------------------------------------------------------
        public CListeObjetsDonnees ObjetsAutoRefFils
        {
            get
            {
                return ObjetsFils;
            }
        }

		//-------------------------------------------------------------------
		/// <summary>
		/// objet parent
		/// </summary>
		public IObjetHierarchiqueACodeHierarchique ObjetParent
		{
			get
			{
				return (IObjetHierarchiqueACodeHierarchique)GetParent (ChampIdParent, GetType());
			}
			set
			{
				if (value != null && value.IsChildOf(this))
					return;
				SetParent ( ChampIdParent, (CObjetHierarchique)value );
                int nNiveau = (int)Row[ChampNiveau];
				if ( value != null )
					Row[ChampNiveau] = value.Niveau+1;
				else
					Row[ChampNiveau] = 0;
                if ((int)Row[ChampNiveau] != nNiveau)
                {
                    foreach (IObjetHierarchiqueACodeHierarchique oFils in ObjetsFils)
                        oFils.OnChangeNiveauParent((int)Row[ChampNiveau]);
                }
			}
		}

        //-------------------------------------------------------------------
        public void OnChangeNiveauParent(int nNewNiveauParent)
        {
            int nOldNiveau = (int)Row[ChampNiveau];
            if (nOldNiveau != nNewNiveauParent + 1)
            {
                Row[ChampNiveau] = nNewNiveauParent + 1;
                foreach (IObjetHierarchiqueACodeHierarchique fils in ObjetsFils)
                    fils.OnChangeNiveauParent((int)Row[ChampNiveau]);
            }
        }

		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne les objets filles
		/// </summary>
		public CListeObjetsDonnees ObjetsFils
		{
			get
			{
				return GetDependancesListe ( GetNomTable(), ChampIdParent );
			}
		}

		//-------------------------------------------------------------------
		public void ChangeCodePartiel( string strCode )
		{
			Row[ChampCodeSystemePartiel] = strCode;
			RecalculeCodeComplet();
			RecalculeCodeCompletFils();
		}

		//-------------------------------------------------------------------
		public void RecalculeCodeComplet()
		{
			string strCode = CodeSystemePartiel;
			IObjetHierarchiqueACodeHierarchique objetParent = ObjetParent;
			while ( objetParent != null )
			{
				strCode = objetParent.CodeSystemePartiel+strCode;
				objetParent = objetParent.ObjetParent;
			}
			Row[ChampCodeSystemeComplet] = strCode;
			
		}

		//-------------------------------------------------------------------
		public void RecalculeCodeCompletFils ( )
		{
			foreach ( CObjetHierarchique objet in ObjetsFils )
			{
				if ( objet.CodeSystemePartiel != CodePartielDefaut )
				{
					objet.RecalculeCodeComplet();
					objet.RecalculeCodeCompletFils();
				}
			}
		}

		//-------------------------------------------------------------------
		[DynamicMethod("Return true if the family belongs to the wanted family","System code of Parent family")]
		public bool AppartientA ( string strCodeFamille )
		{
			if ( CodeSystemeComplet.Length > strCodeFamille.Length && 
				CodeSystemeComplet.Substring(0, strCodeFamille.Length) == strCodeFamille )
				return true;
			return false;
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne vrai si l'objet est fils (à n'importe quel niveau) 
		/// de l'objet passé en paramètre
		/// </summary>
		/// <param name="objet">Père potientiel</param>
		/// <returns></returns>
		public bool IsChildOf(IObjetHierarchiqueACodeHierarchique objet)
		{
			if (objet == null)
				return false;
			if (objet.Equals(this))
				return true;
			if (ObjetParent != null)
				return ObjetParent.IsChildOf(objet);
			return false;
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne vrai si l'objet est parent de l'objet passé en paramètres
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public bool IsParentOf(IObjetHierarchiqueACodeHierarchique objet)
		{
			if (objet == null)
				return false;
			return objet.IsChildOf(this);
		}

        //-------------------------------------------------------------------
        [DynamicMethod("Recalculate system code")]
        public void RecalcSystemCode()
        {
            Row[ChampCodeSystemePartiel] = CodePartielDefaut;
        }

		#region IObjetDonneeAutoReference Membres

		public string ChampParent
		{
			get { return ChampIdParent; }
		}

		public string ProprieteListeFils
		{
			get { return "ObjetsFils"; }
		}

		#endregion
	}
}
