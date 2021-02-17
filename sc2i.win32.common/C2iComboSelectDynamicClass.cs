using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using sc2i.common;

namespace sc2i.win32.common
{
	public partial class C2iComboSelectDynamicClass : C2iComboBox
	{
		//Retourne vrai si le type doit être ajouté à la combo
		public delegate bool ShouldAddTypeDelegate(Type tp);

		/// <summary>
		/// Evenement appelé pour savoir si un type doit être ajouté ou non à la combo
		/// </summary>
		public event ShouldAddTypeDelegate OnAddType;

		public C2iComboSelectDynamicClass()
		{
			InitializeComponent();
		}

		public C2iComboSelectDynamicClass(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public void Init ( )
		{
			Init ( DynamicClassAttribute.GetAllDynamicClass() );
		}

		public void InitSurAttributs(params Type[] attributsQueLaClasseDoitAvoir)
		{
			Init(DynamicClassAttribute.GetAllDynamicClass(attributsQueLaClasseDoitAvoir));
		}

		public void InitSurHeritage(params  Type[] typesQueLaClasseDoitImplementer)
		{
			Init(DynamicClassAttribute.GetAllDynamicClassHeritant(typesQueLaClasseDoitImplementer));
		}

		public void Init(CInfoClasseDynamique[] infos)
		{
			ArrayList lst = new ArrayList();
			lst.Add(new CInfoClasseDynamique(typeof(DBNull), "(Aucun)"));
			if (OnAddType != null)
			{
				foreach (CInfoClasseDynamique info in infos)
				{
					if (OnAddType(info.Classe))
						lst.Add(info);
				}
			}
			else
			{
				lst.AddRange(infos);
			}
			BeginUpdate();
			DataSource = null;
			ValueMember = "Classe";
			DisplayMember = "Nom";
			DataSource = lst;
			EndUpdate();
			
		}

		public Type TypeSelectionne
		{
			get
			{
				object val = SelectedValue;
				if (val == typeof(DBNull))
					val = null;
				return (Type)val;
			}
			set
			{
				if (value == null)
					SelectedValue = typeof(DBNull);
				else
					SelectedValue = value;
			}
		}
	}
}
