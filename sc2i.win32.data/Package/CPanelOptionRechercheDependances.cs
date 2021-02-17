using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data.Package;
using sc2i.win32.common.customizableList;
using sc2i.data;
using sc2i.common;

namespace sc2i.win32.data.Package
{
    public partial class CPanelOptionRechercheDependances : UserControl
    {
        private CConfigurationRechercheEntites m_configuration = new CConfigurationRechercheEntites();
        private CControleOptionsForType m_controleOptions = null;
        //------------------------------------------------------
        public CPanelOptionRechercheDependances()
        {
            InitializeComponent();
            m_controleOptions = new CControleOptionsForType();
            m_wndListeTypes.ItemControl = m_controleOptions;
        }

        //------------------------------------------------------
        public void Init ( CConfigurationRechercheEntites configuration )
        {
            m_configuration = configuration;
            m_controleOptions.SetConfiguration(m_configuration);
            FillTypes();
        }

        //------------------------------------------------------
        private void FillTypes()
        {
            List<CCustomizableListItem> items = new List<CCustomizableListItem>();
            foreach ( Type tp  in CContexteDonnee.GetAllTypes() )
            {
                CCustomizableListItem item = new CCustomizableListItem();
                item.Tag = tp;
                items.Add(item);
            }
            items.Sort((x,y)=>DynamicClassAttribute.GetNomConvivial((Type)x.Tag).CompareTo(DynamicClassAttribute.GetNomConvivial((Type)y.Tag)));
            m_wndListeTypes.Items = items.ToArray();
            m_wndListeTypes.Refill();
        }

    }
}
