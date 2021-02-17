using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
#if PDA
#else
using System.Drawing.Design;
#endif


using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.datagrid.Filters;
using System.ComponentModel;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iChampCustomTextBox.
	/// </summary>
	[WndName("Variable")]
	[Serializable]
    public class C2iWndVariableFiltreDyamique : C2iWndVariable, IWndIncluableDansDataGrid
	{
      
		private CVariableDynamique m_variable = null;

		public C2iWndVariableFiltreDyamique()
			:base()
		{
		}

		/// //////////////////////////////////////////////////
		private int GetNumVersion()
		{
            //return 0;
            return 1; // Passage de Id Variable en String
		}

		/// //////////////////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			if (tp == null)
				return false;
			return typeof(IElementAVariables).IsAssignableFrom(tp);
		}

		/// //////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if (!result)
                return result;

            string strIdVariable = "";
            if (m_variable != null && serializer.Mode == ModeSerialisation.Ecriture)
                strIdVariable = m_variable.IdVariable;

            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                strIdVariable = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref strIdVariable);

            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                if (strIdVariable == "")
                    m_variable = null;
                else
                    m_variable = ((IElementAVariablesDynamiquesBase)serializer.GetObjetAttache(typeof(IElementAVariablesDynamiquesBase))).GetVariable(strIdVariable);
            }

            return result;
        }

		/// //////////////////////////////////////////////////
		public  override IVariableDynamique Variable
		{
			get
			{
				return VariableAssociee;
			}
		}

		/// //////////////////////////////////////////////////
		[System.ComponentModel.Editor(typeof(CProprieteVariableFiltreDynamiqueEditor), typeof(UITypeEditor))]
		public CVariableDynamique VariableAssociee
		{
			get
			{
				return m_variable;
			}
			set
			{
				m_variable = value;
			}
		}

		/// //////////////////////////////////////////////////
		public override bool AutoBackColor
		{
			get
			{
				return false;
			}
			set
			{
			}
		}


		//-------------------------------------------------------
		public override void OnDesignSelect(
			Type typeEdite, 
			object objetEdite,
			sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
			CProprieteVariableFiltreDynamiqueEditor.SetElementEdite(objetEdite as IElementAVariablesDynamiquesBase);
		}





        #region IWndIncluableDansDataGrid Membres
        [Browsable(false)]
        public Type ValueTypeForGrid
        {
            get
            {
                if (m_variable != null)
                    return m_variable.TypeDonnee.TypeDotNetNatif;
                return typeof(string);
            }

        }

        public string ConvertObjectValueToStringForGrid(object valeur)
        {
            if (valeur is CObjetDonnee)
                return ((CObjetDonnee)valeur).DescriptionElement;
            else if (valeur != null)
            {
                if (valeur is DateTime)
                    return ((DateTime)valeur).ToShortDateString();
                if ( valeur is bool )
                    return ((bool)valeur) ? I.T("Yes|20024") : I.T("No|20025");
                return valeur.ToString();
            }
            return "";
        }

        public object GetObjectValueForGrid(object element)
        {
            IElementAVariables elt = element as IElementAVariables;
            if (elt != null && Variable != null)
            {
                object valeur = elt.GetValeurChamp(Variable.IdVariable);
                return valeur;
            }
            return "";
        }

        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            if (m_variable == null)
                return new CGridFilterForWndDataGrid[0];
            if ( m_variable.TypeDonnee.TypeDotNetNatif == typeof(int) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(int?) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(double) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(double?) )
                return CGridFilterNumericComparison.GetFiltresNumeriques();
            if (m_variable.TypeDonnee.TypeDotNetNatif == typeof(bool) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(bool?))
                return CGridFilterChecked.GetFiltresBool();
            if (m_variable.TypeDonnee.TypeDotNetNatif == typeof(DateTime) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(DateTime?) ||
                m_variable.TypeDonnee.TypeDotNetNatif == typeof(CDateTimeEx))
                return CGridFilterDateComparison.GetFiltresDate();
            return CGridFilterTextComparison.GetFiltresTexte();
        }


        #endregion
    }

}
