using System;
using System.Drawing.Design;

using sc2i.expression;

namespace futurocom.chart
{
	public interface IEditeurChartSetup
	{
        CChartSetup EditeParametreChart(CChartSetup parametre);
	}

	public class CProprieteChartSetupEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
		private static IEditeurChartSetup m_editeur = null;
		/// ///////////////////////////////////////////
        public CProprieteChartSetupEditor()
		{
		}

		/// ///////////////////////////////////////////
        public static void SetEditeur(IEditeurChartSetup editeur)
		{
			m_editeur = editeur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type typeEditeur)
		{
			m_typeEditeur = typeEditeur;
		}

		

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
            IEditeurChartSetup editeur = m_editeur;
			if (editeur == null && m_typeEditeur != null)
			{
                editeur = (IEditeurChartSetup)Activator.CreateInstance(m_typeEditeur);
			}
            object retour = editeur.EditeParametreChart((CChartSetup)value);
			if (m_editeur == null && editeur is IDisposable)
				((IDisposable)editeur).Dispose();
			return retour;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_editeur == null && m_typeEditeur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}

		
	}
}
