using System;
using System.Drawing.Design;

using sc2i.expression;
using System.ComponentModel;
using sc2i.common;

namespace futurocom.chart
{
	public interface IEditeurFournisseurValeursSerie
	{
        IFournisseurValeursSerie EditeFournisseur(CChartSetup chartSetup, IFournisseurValeursSerie fournisseur);
	}

	public class CFournisseurValeursSerieEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
        private static CChartSetup m_chartSetup = null;

		/// ///////////////////////////////////////////
        public CFournisseurValeursSerieEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type typeEditeur)
		{
			m_typeEditeur = typeEditeur;
		}

        /// ///////////////////////////////////////////
        public static CChartSetup ChartSetup
        {
            get
            {
                return m_chartSetup;
            }
            set
            {
                m_chartSetup = value;
            }
        }

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
            IEditeurFournisseurValeursSerie editeur = null;
            if ( m_typeEditeur != null )
			{
                editeur = (IEditeurFournisseurValeursSerie)Activator.CreateInstance(m_typeEditeur);
                IFournisseurValeursSerie fournisseur = value as IFournisseurValeursSerie;
                object retour = editeur.EditeFournisseur(m_chartSetup, fournisseur);
                return retour;
			}
			return value;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_typeEditeur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}
	}

    //---------------------------------------------------------------------------------------
    public class CFournisseurValeurSerieConvertor : CGenericObjectConverter<IFournisseurValeursSerie>
    {
        public override string GetString(IFournisseurValeursSerie value)
        {
            if (value == null)
                return "";
            if (CFournisseurValeursSerieEditor.ChartSetup != null)
                return value.GetLabel(CFournisseurValeursSerieEditor.ChartSetup.ParametresDonnees);
            return value.LabelType;
        }
    }

    
}
