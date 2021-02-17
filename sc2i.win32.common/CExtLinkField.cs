using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using sc2i.common;
using System.Drawing.Design;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CExtBindChamp.
	/// </summary>
	public delegate void ChangeChampEventHandler ( string strChamp, object valeur );
	[ProvideProperty("LinkField", typeof(Control))]
    [ProvideProperty("LinkFieldAutoUpdate", typeof(Control))]
	public class CExtLinkField : Component, IExtenderProvider
	{
        private class CInfoControle
        {
            private string m_strField = "";
            private bool m_bAutoUpdate = true;

            public CInfoControle()
            {
            }

            //-------------------------------------------
            public string Field
            {
                get
                {
                    return m_strField;
                }
                set
                {
                    m_strField = value.Trim();
                }
            }

            //-------------------------------------------
            /// <summary>
            /// Indique si le ExtLinkField gère la valeur du contrôle en automatique
            /// ou pas
            /// </summary>
            public bool AutoUpdate
            {
                get
                {
                    return m_bAutoUpdate && m_strField.Length > 0;
                }
                set
                {
                    m_bAutoUpdate = value;
                }
            }
        }

        [NonSerialized]
        private Type m_sourceType = null;
        
        private string m_strTypeSource = "";

        private Dictionary<object, CInfoControle> m_dicControleToInfo = new Dictionary<object, CInfoControle>();
		//-------------------------------------------------------------------------
		public CExtLinkField()
		{
		}

        //-------------------------------------------------------------------------
        [Browsable(false)]
        public Type SourceType
        {
            get
            {
                if (m_sourceType == null)
                    m_sourceType = CActivatorSurChaine.GetType(m_strTypeSource);
                return m_sourceType;
            }
            set
            {
                m_sourceType = value;
                if (value != null)
                    m_strTypeSource = value.ToString();
                else
                    m_strTypeSource = "";
            }
        }

        //-------------------------------------------------------------------------
        [Editor(typeof(CSelectTypeUIEditor), typeof(UITypeEditor))]
        public string SourceTypeString
        {
            get
            {
                return m_strTypeSource;
            }
            set
            {
                m_strTypeSource = value;
                m_sourceType = null;
            }
        }

        //-------------------------------------------------------------------------
        public bool ShouldSerializeSourceType()
        {
            return false;
        }

		//-------------------------------------------------------------------------
		public bool CanExtend ( object extendee )
		{
			if ( extendee is Control )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        //-------------------------------------------------------------------------
        private CInfoControle GetInfo ( object extendee, bool bCreate )
        {
            CInfoControle info = null;
            if ( !m_dicControleToInfo.TryGetValue (extendee, out info) && bCreate )
            {
                info = new CInfoControle();
                m_dicControleToInfo[extendee] = info;
            }
            return info;
        }


		//-------------------------------------------------------------------------
		public virtual void SetLinkField ( object extendee, string strField)
		{
            CInfoControle info = GetInfo(extendee, true);
            info.Field = strField;
			if ( extendee is Control && strField.Trim()!="" && DesignMode)
			{
				if ( ! (extendee is CheckBox ) &&
					 ! (extendee is RadioButton ) &&
					 ! (extendee is NumericUpDown )
					)
                    try
                    {
                        ((Control)extendee).Text = "[" + strField + "]";
                    }
                    catch
                    { }
			}
		}
		//-------------------------------------------------------------------------
        [Editor(typeof(ExtFieldSelector), typeof(UITypeEditor))]
        public virtual string GetLinkField ( object extendee ) 
		{
            CInfoControle info = GetInfo(extendee, false);
            if (info != null)
                return info.Field;
            return "";
		}

        //-------------------------------------------------------------------------
        public virtual void SetLinkFieldAutoUpdate(object extendee, bool bAutoUpdate)
        {
            CInfoControle info = GetInfo(extendee, true);
            info.AutoUpdate = bAutoUpdate;
        }
        //-------------------------------------------------------------------------
        public virtual bool GetLinkFieldAutoUpdate(object extendee)
        {
            CInfoControle info = GetInfo(extendee, false);
            if (info != null)
                return info.AutoUpdate;
            return true;
        }
		//-------------------------------------------------------------------------
		public void ResetModeEdition()
		{}
		//-------------------------------------------------------------------------
		public event ChangeChampEventHandler OnRemplitControle;
		public CResultAErreur FillControl(Control ctrl, object obj)
		{
			CResultAErreur result = CResultAErreur.True;
			string strPropAffichee = "";
			try
			{
                CInfoControle info = GetInfo(ctrl, false);
                if (info != null && info.AutoUpdate && info.Field.Length > 0)
                {
                    string strProp = info.Field;
                    if (strProp.LastIndexOf(".") >= 0 && ctrl is ComboBox)
                    {
                        string strTemp = strProp;
                        strProp = strTemp.Substring(0, strTemp.LastIndexOf("."));
                        strPropAffichee = strTemp.Substring(strTemp.LastIndexOf(".") + 1);
                    }
                    if (strProp != null && strProp != "")
                    {
                        MemberInfo methodeAppellee = null;
                        object objetToInterroge = null;
                        object objValue = CInterpreteurTextePropriete.GetValue(obj, strProp, ref objetToInterroge, ref methodeAppellee);
                        if (objValue != null)
                        {
                            if (OnRemplitControle != null)
                                OnRemplitControle(strProp, objValue);

                            if (ctrl is NumericUpDown)
                            {
                                if (objValue is int)
                                    ((NumericUpDown)ctrl).Value = new Decimal((int)objValue);
                                else if (objValue is double)
                                    ((NumericUpDown)ctrl).Value = new Decimal((double)objValue);
                                else
                                    ((NumericUpDown)ctrl).Value = (decimal)objValue;
                            }
                            else if (ctrl is C2iTextBoxNumerique)
                            {
                                if (objValue is int?)
                                    ((C2iTextBoxNumerique)ctrl).IntValue = (int?)objValue;
                                else if (objValue is double?)
                                    ((C2iTextBoxNumerique)ctrl).DoubleValue = (double?)objValue;
                                else
                                    ((C2iTextBoxNumerique)ctrl).DoubleValue = Convert.ToDouble(objValue);
                            }
                            else if (ctrl is DateTimePicker)
                                ((DateTimePicker)ctrl).Value = (DateTime)objValue;
                            else if (ctrl is CheckBox)
                                ((CheckBox)ctrl).Checked = (bool)objValue;
                            else if (ctrl is C2iComboBox)
                                ((C2iComboBox)ctrl).SelectedValue = objValue;
                            else if (ctrl is ComboBox)
                            {
                                ((ComboBox)ctrl).DisplayMember = strPropAffichee;
                                try
                                { ((ComboBox)ctrl).SelectedValue = objValue; }
                                catch
                                { ((ComboBox)ctrl).SelectedValue = System.DBNull.Value; }
                            }
                            else if (ctrl is I2iControlEditObject)
                                ((I2iControlEditObject)ctrl).ObjetEdite = objValue;
                            else if (!(ctrl is CheckBox))
                            {
                                ctrl.Text = CInterpreteurTextePropriete.GetStringValueFormatee(objValue, "", methodeAppellee);
                            }

                        }
                        else
                        {
                            if (ctrl is ComboBox)
                            {
                                ((ComboBox)ctrl).SelectedValue = System.DBNull.Value;
                            }
                            else if (!(ctrl is CheckBox))
                                ctrl.Text = "";
                        }

                        if (ctrl is C2iDateTimeExPicker)
                            ((C2iDateTimeExPicker)ctrl).Value = (CDateTimeEx)objValue;
                    }
                }
			}
			catch ( Exception e )
			{
				Console.WriteLine ( e.ToString() );
				result.EmpileErreur ( new CErreurException ( e ) );
                result.EmpileErreur("Error while loading control @1 by @2||30046",ctrl.Name.ToString(),obj.ToString());
				return result;
			}
			return result;
		}
		//-------------------------------------------------------------------------
		public CResultAErreur FillDialogFromObjet ( object obj )
		{
			CResultAErreur result = CResultAErreur.True;

            if (obj != null)
            {
                Type tp = obj.GetType();
                foreach (Control ctrl in m_dicControleToInfo.Keys)
                {
                    result = FillControl(ctrl, obj);
                }
            }
			return result;
		}


		//-------------------------------------------------------------------------
		protected string GetChampFor ( object control )
		{
            return GetLinkField(control);
		}

		public event ChangeChampEventHandler OnValideChamp;
		//-------------------------------------------------------------------------
		public virtual CResultAErreur FillObjetFromDialog ( object obj )
		{
			CResultAErreur result = CResultAErreur.True;

			Type tp = obj.GetType();
			foreach ( Control ctrl in m_dicControleToInfo.Keys )
			{
				if ( !(ctrl is Label) )
				{
					try
					{
                        CInfoControle info = GetInfo(ctrl, false);
                        if (info != null && info.AutoUpdate && info.Field.Length > 0)
                        {
                            string strProp = info.Field;
                            if (strProp.LastIndexOf(".") >= 0 && ctrl is ComboBox)
                            {
                                strProp = strProp.Substring(0, strProp.LastIndexOf("."));
                            }
                            if (strProp.IndexOf('.') < 0)//Uniquement les propriétés directes de l'objet
                            {
                                PropertyInfo prop = tp.GetProperty(strProp);
                                if (prop != null)
                                {
                                    MethodInfo method = prop.GetSetMethod();
                                    if (method != null)
                                    {
                                        /*
                                        object objValue = method.Invoke(obj, null);
                                        if ( objValue != null )
                                            objValue = (object) ctrl.Text;
                                        */
                                        object[] valeur;
                                        if (ctrl is DateTimePicker)
                                        {
                                            valeur = new object[] { ((DateTimePicker)ctrl).Value };
                                        }
                                        else if (ctrl is C2iDateTimeExPicker)
                                        {
                                            valeur = new object[] { ((C2iDateTimeExPicker)ctrl).Value };
                                        }
                                        else
                                        {
                                            if (ctrl is NumericUpDown)
                                            {
                                                if (((NumericUpDown)ctrl).DecimalPlaces == 0)
                                                    valeur = new object[] { (int)((NumericUpDown)ctrl).Value };
                                                else
                                                    valeur = new object[] { (double)((NumericUpDown)ctrl).Value };
                                            }
                                            else if (ctrl is C2iTextBoxNumerique)
                                            {
                                                if (!((C2iTextBoxNumerique)ctrl).DecimalAutorise)
                                                    valeur = new object[] { ((C2iTextBoxNumerique)ctrl).IntValue };
                                                else
                                                    valeur = new object[] { (double)((C2iTextBoxNumerique)ctrl).DoubleValue };
                                            }
                                            else
                                            {
                                                if (ctrl is CheckBox)
                                                    valeur = new object[] { ((CheckBox)ctrl).Checked };
                                                else
                                                {
                                                    if (ctrl is ComboBox)
                                                    {
                                                        valeur = new object[] { ((ComboBox)ctrl).SelectedValue };
                                                        if (valeur[0] == System.DBNull.Value)
                                                            valeur[0] = null;
                                                    }
                                                    else if (ctrl is I2iControlEditObject)
                                                    {
                                                        valeur = new object[] { ((I2iControlEditObject)ctrl).ObjetEdite };
                                                    }
                                                    else
                                                        valeur = new object[] { ctrl.Text };
                                                }
                                            }
                                        }
                                        method.Invoke(obj, valeur);
                                        if (OnValideChamp != null)
                                            OnValideChamp(strProp, valeur[0]);
                                    }
                                }
                            }
                        }
					}
					catch (Exception e )
					{
						result.EmpileErreur ( new CErreurException(e));
					    result.EmpileErreur(I.T("Error while filling the object @1 with @2|30047", obj.ToString(), ctrl.Name.ToString()));
					}
				}
			}
			return result;
		}
		
        //-------------------------------------------------------------------------
        [NonSerialized]
        private Control m_hostingControl = null;
        private Control HostingControl
        {
            get
            {
                
                if (m_hostingControl == null && DesignMode)
                {
                    IDesignerHost designer = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (designer != null)
                        m_hostingControl = designer.RootComponent as Control;
                }
                return m_hostingControl;
            }
        }

        //-------------------------------------------------------------------------------------
        public void AppliqueRestrictions(
            CListeRestrictionsUtilisateurSurType lstRestrictions,
            CGestionnaireReadOnlySysteme gestionnaire)
        {
            if (SourceType == null)
                return;
            CRestrictionUtilisateurSurType restriction = lstRestrictions.GetRestriction(SourceType);
            if (restriction != null)
            {
                foreach (KeyValuePair<object, CInfoControle> kv in m_dicControleToInfo)
                {
                    CInfoControle info = kv.Value;
                    if (info.Field.Length > 0)
                    {
                        ERestriction rest = restriction.GetRestriction(info.Field);
                        Control ctrl = kv.Key as Control;
                        if ( ctrl != null )
                        {
                        if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                        {
                            
                            gestionnaire.SetReadOnly(ctrl, true);
                        }
                        else
                            gestionnaire.SetReadOnly ( ctrl, false );
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------
        public void RemoveRestrictions()
        {

        }
    }

    public class ExtFieldSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            

            var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            ComboBox box = new ComboBox();
            Type tp = Type.GetType("System.ComponentModel.ExtendedPropertyDescriptor");
            BindingFlags getFieldBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;
            CExtLinkField linkField = tp.InvokeMember("provider",
                getFieldBindingFlags,
                null,
                context.PropertyDescriptor,
                null) as CExtLinkField;
            if (linkField != null )
            {
                CArbreChampsDotNet arbre = new CArbreChampsDotNet();
                arbre.Init(linkField.SourceType);


                EventHandler onOk = (sender, e)=>
                    {
                        value = arbre.ChampSelectionne;
                        editorService.CloseDropDown();
                    };

                EventHandler onCancel = (sender, e) =>
                    {
                        editorService.CloseDropDown();
                    };
                arbre.OkEvent += onOk;
                arbre.CancelEvent += onCancel;

                editorService.DropDownControl(arbre);

                arbre.OkEvent -= onOk;
                arbre.CancelEvent -= onCancel;



            }
            return value;
        }


                
    }
}
