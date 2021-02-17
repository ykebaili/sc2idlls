using System;
using System.Linq;
using System.Drawing.Design;

using sc2i.expression;
using futurocom.chart.ChartArea;
using sc2i.common;

namespace futurocom.chart
{
	public interface IEditeurSelectChartArea
	{
        string SelectArea(CChartSetup parametre, 
            string strInitialChartId,
            IServiceProvider provider);
	}

	public class CProprieteSelectChartAreaEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
		private static CChartSetup m_chartSetup = null;
		
        /// ///////////////////////////////////////////
        public CProprieteSelectChartAreaEditor()
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
            IEditeurSelectChartArea editeur = (IEditeurSelectChartArea)Activator.CreateInstance(m_typeEditeur);
            object retour = editeur.SelectArea(m_chartSetup,
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
    public class CChartAreaFromStringConvertor : CGenericObjectConverter<string>
    {
        public override string GetString(string value)
        {
            CChartSetup setup = CProprieteSelectChartAreaEditor.ChartSetup; 
            if (setup != null)
            {
                foreach (CChartArea area in setup.Areas)
                    if (area.AreaId == value)
                        return area.AreaName;
            }
            
            return "";
        }
    }

}
