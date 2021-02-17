using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using System.Windows.Forms;
using sc2i.common;

namespace sc2i.win32.common
{
    public class CWin32Traducteur
    {
		//Type de control -> ArrayList des types de PropertyInfo à traduire
		private static Hashtable m_tableGetPropATraduire = new Hashtable();

		//Type de control -> ArrayList des proprietes renvoyant des sous objets à traduire
		private static Hashtable m_tableGetPropSousObjetsATraduire = new Hashtable();

        //Type de control->Dictionnaire des Variables membres à traduire
        private static Dictionary<Type, List<FieldInfo>> m_tableFieldATraduire = new Dictionary<Type, List<FieldInfo>>();

        //Type de control->Variables membres à traduire
        private static Hashtable m_tableVariablesMembresATraduire = new Hashtable();

		private static bool m_bTableProprietesIsInit = false;
		
		//Initialise les proprietés à traduire de types de controles particuliers.
		private static void InitTableProprietes()
		{
			AddProprieteATraduire(typeof(Control), "Text");
			AddProprieteSousObjetsATraduire(typeof(Control), "Controls");
			
			AddProprieteSousObjetsATraduire(typeof(Crownwood.Magic.Controls.TabControl), "TabPages");
			AddProprieteATraduire(typeof(Crownwood.Magic.Controls.TabPage), "Title");

			
			AddProprieteSousObjetsATraduire(typeof(ListView), "Columns");
			AddProprieteATraduire(typeof(ColumnHeader), "Text");

			AddProprieteSousObjetsATraduire(typeof(ToolStrip), "Items");
			AddProprieteATraduire(typeof(ToolStripItem), "Text");

			AddProprieteSousObjetsATraduire(typeof(Menu), "MenuItems");
			AddProprieteATraduire(typeof(Menu), "Text");

            AddProprieteSousObjetsATraduire(typeof(ContextMenu), "MenuItems");
			AddProprieteATraduire(typeof(MenuItem), "Text");
			
			AddProprieteSousObjetsATraduire ( typeof(ContextMenuStrip), "Items");
			AddProprieteATraduire(typeof(ContextMenuStrip),"Text");

			AddProprieteSousObjetsATraduire(typeof(ToolStripDropDownItem ), "DropDownItems");
			AddProprieteATraduire(typeof(ToolStripDropDownItem), "Text");

            AddVariableMembreATraduire(typeof(Control), typeof(ContextMenu));
            AddVariableMembreATraduire(typeof(Control), typeof(ContextMenuStrip));



			m_bTableProprietesIsInit = true;
		}

		/// <summary>
		/// Ajoute une propriété à traduire pour un type de contrôle.
		/// </summary>
		/// <param name="tp"></param>
		/// <param name="strPropriete"></param>
		public static void AddProprieteATraduire(Type tp, string strPropriete)
		{
			List<PropertyInfo> lst = (List<PropertyInfo>)m_tableGetPropATraduire[tp];
			if (lst == null)
			{
				lst = new List<PropertyInfo>();
				m_tableGetPropATraduire[tp] = lst;
			}
			PropertyInfo info = tp.GetProperty(strPropriete);
			if (info != null && typeof(string).IsAssignableFrom(info.PropertyType))
			{
				if (info.GetGetMethod() != null && info.GetSetMethod() != null )
				{
					if (!lst.Contains(info))
						lst.Add(info);
				}
			}
		}

        /// <summary>
        /// Ajoute une propriété à traduire pour un type de contrôle.
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="strPropriete"></param>
        public static void AddVariableMembreATraduire(Type tp, Type typeVariableMembre)
        {
            List<Type> lst = (List<Type>)m_tableVariablesMembresATraduire[tp];
            if (lst == null)
            {
                lst = new List<Type>();
                m_tableVariablesMembresATraduire[tp] = lst;
            }
            if (!lst.Contains(typeVariableMembre))
                lst.Add(typeVariableMembre);
        }
        

		/// <summary>
		/// Ajoute une propriété à traduire pour un type de contrôle.
		/// </summary>
		/// <param name="tp"></param>
		/// <param name="strPropriete"></param>
		public static void AddProprieteSousObjetsATraduire(Type tp, string strPropriete)
		{
			List<MethodInfo> lst = (List<MethodInfo>)m_tableGetPropSousObjetsATraduire[tp];
			if (lst == null)
			{
				lst = new List<MethodInfo>();
				m_tableGetPropSousObjetsATraduire[tp] = lst;
			}
			PropertyInfo info = tp.GetProperty(strPropriete);
			if (info != null && typeof(IEnumerable).IsAssignableFrom(info.PropertyType))
			{
				MethodInfo methode = info.GetGetMethod();
				if (methode != null)
				{
					if (!lst.Contains(methode))
						lst.Add(methode);
				}
			}
		}



		/// <summary>
		/// Traduit le contrôle et tous les contrôles contenus
		/// </summary>
		/// <param name="typePourMessages"></param>
		/// <param name="ctrl"></param>
        public static void Translate(Type typePourMessages, object obj)
        {
            try
            {
				if (obj == null)
					return;
				TraducMessages(typePourMessages, obj);
				Type tp = typePourMessages;
				if ( (obj is UserControl || obj is Form) &&
					  obj.GetType() != typePourMessages && CTraducteur.HasTraductionsForType(obj.GetType()))
					tp = obj.GetType();
				TraducFils(tp, obj);
            }
            catch 
            {
            }
        }

		/// <summary>
		/// Traduit le contrôle ( et utilise son type pour savoir quel zone de messages utiliser
		/// </summary>
		/// <param name="ctrl"></param>
        public static void Translate(object obj)
        {
			if (obj == null)
				return;
            Translate(obj.GetType(), obj);
        }

		/// <summary>
		/// Applique les traductions spécifiques suivant le type de contrôle
		/// </summary>
		/// <param name="ctrl"></param>
		private static void TraducMessages(Type typePourMessages, object ctrl)
		{
			if (!m_bTableProprietesIsInit)
				InitTableProprietes();

			Type tpControle = ctrl.GetType();

			List<PropertyInfo> lst = (List<PropertyInfo>)m_tableGetPropATraduire[tpControle];
			if (lst == null)
			{
				//créé la liste de propriétés spécifiques à traduire pour ce type de contrôle
				lst = new List<PropertyInfo>();
				Type tp = tpControle;
				while (tp != null)
				{
					List<PropertyInfo> lstPropsType = (List<PropertyInfo>)m_tableGetPropATraduire[tp];
					if (lstPropsType != null)
						lst.AddRange(lstPropsType);
					tp = tp.BaseType;
				}

				if (ctrl is IControlTraductible)
				{
					List<string> lstProps = ((IControlTraductible)ctrl).GetListeProprietesATraduire();
					if (lstProps != null)
					{
						foreach (string strPropriete in lstProps)
						{
							PropertyInfo info = tpControle.GetProperty(strPropriete);
							if (info != null && typeof(string).IsAssignableFrom(info.PropertyType) &&
								info.GetGetMethod() != null && info.GetSetMethod() != null )
							{
								if (!lst.Contains(info))
									lst.Add(info);
							}
						}
					}
				}
				m_tableGetPropATraduire[tpControle] = lst;
			}

			foreach (PropertyInfo prop in lst.ToArray())
			{
				try
				{
					string strVal = (string)prop.GetValue ( ctrl, null );
                    if (strVal.Contains("|"))
                    {
                        Type tp = typePourMessages;
                        if (ctrl.GetType() != typePourMessages && CTraducteur.HasTraductionsForType(ctrl.GetType()))
                            tp = ctrl.GetType();
                        prop.SetValue(ctrl, I.TT(tp, strVal), null);
                    }
				}
				catch { }
			}

            //Traduction de variables membres
            Type tpTmp = tpControle;
            List<FieldInfo> lstFields = null;
            m_tableFieldATraduire.TryGetValue ( tpControle, out lstFields );
            if ( lstFields == null )
            {
                lstFields = new List<FieldInfo>();
                Dictionary<Type, bool> dicTypesMembresATraduire = new Dictionary<Type, bool>();
                while (tpTmp != null)
                {
                    List<Type> lstTypesVariablesMembres = (List<Type>)m_tableVariablesMembresATraduire[tpTmp];
                    if (lstTypesVariablesMembres != null)
                    {
                        foreach (Type tp in lstTypesVariablesMembres)
                            dicTypesMembresATraduire[tp] = true;
                    }
                    tpTmp = tpTmp.BaseType;
                }
                FieldInfo[] fields = tpControle.GetFields(BindingFlags.FlattenHierarchy|BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                        Type tp = field.FieldType;
                        if (dicTypesMembresATraduire.ContainsKey(tp))
                        {
                            lstFields.Add ( field );
                        }
                }
                m_tableFieldATraduire[tpControle] = lstFields;
            }
            foreach ( FieldInfo field in lstFields )
            {
                try
                {
                    object obj = field.GetValue(ctrl);
                    if (obj != null)
                        Translate(field.DeclaringType, obj);
                }
                catch
                {
                }
            }
        }


		/// <summary>
		/// Applique les traductions spécifiques aux fils du contrôle
		/// </summary>
		/// <param name="ctrl"></param>
		private static void TraducFils(Type typePourMessages, object ctrl)
		{
			if (!m_bTableProprietesIsInit)
				InitTableProprietes();

			Type tpControle = ctrl.GetType();

			List<MethodInfo> lst = (List<MethodInfo>)m_tableGetPropSousObjetsATraduire[tpControle];
			if (lst == null)
			{
				//créé la liste de propriétés spécifiques à traduire pour ce type de contrôle
				lst = new List<MethodInfo>();
				Type tp = tpControle;
				while (tp != null)
				{
					List<MethodInfo> lstPropsType = (List<MethodInfo>)m_tableGetPropSousObjetsATraduire[tp];
					if (lstPropsType != null)
						lst.AddRange(lstPropsType);
					tp = tp.BaseType;
				}

				if (ctrl is IControlTraductible)
				{
					List<string> lstProps = ((IControlTraductible)ctrl).GetListeProprietesSousObjetsATraduire();
					if (lstProps != null)
					{
						foreach (string strPropriete in lstProps)
						{
							PropertyInfo info = tpControle.GetProperty(strPropriete);
							if (info != null && typeof(IEnumerable).IsAssignableFrom(info.PropertyType))
							{
								MethodInfo methode = info.GetGetMethod();
								if (methode != null)
									if (!lst.Contains(methode))
										lst.Add(methode);
							}
						}
					}
				}
				m_tableGetPropSousObjetsATraduire[tpControle] = lst;
			}

			foreach (MethodInfo methode in lst.ToArray())
			{
				try
				{
					object lstFils = methode.Invoke ( ctrl, new object[0] );
					if (lstFils != null && lstFils is IEnumerable)
					{
						foreach (object fils in (IEnumerable)lstFils)
						{
							if ( fils != null )
								Translate(typePourMessages, fils);
						}
					}
				}
				catch { }
			}
		}

		
                    
    }
}
