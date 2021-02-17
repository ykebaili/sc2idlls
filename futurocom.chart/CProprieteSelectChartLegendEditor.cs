using System;
using System.Linq;
using System.Drawing.Design;

using sc2i.expression;
using sc2i.common;
using futurocom.chart.LegendArea;

namespace futurocom.chart
{
	public interface IEditeurSelectChartLegend
	{
        string SelectLegend(CChartSetup parametre, 
            string strInitialChartId,
            IServiceProvider provider);
	}

	public class CProprieteSelectChartLegendEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
		private static CChartSetup m_chartSetup = null;
		
        /// ///////////////////////////////////////////
        public CProprieteSelectChartLegendEditor()
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
            set { m_chartSetup = value; }
        }

		

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
            IEditeurSelectChartLegend editeur = (IEditeurSelectChartLegend)Activator.CreateInstance(m_typeEditeur);
            object retour = editeur.SelectLegend(m_chartSetup,
                value as string,
                provider);
			return retour;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_typeEditeur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.DropDown;
		}

		
	}

    //-----------------------------------------------------------------------------
    public class CChartLegendFromStringConvertor : CGenericObjectConverter<string>
    {
        public override string GetString(string value)
        {
            CChartSetup setup = CProprieteSelectChartLegendEditor.ChartSetup; 
            if (setup != null)
            {
                foreach (CLegendArea Legend in setup.Legends)
                    if (Legend.LegendId == value)
                        return Legend.LegendName;
            }
            
            return "";
        }
    }

}
