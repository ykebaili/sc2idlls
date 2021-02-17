using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.win32.common.customizableList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.win32.data.dynamic.import
{
    //-------------------------------------------------------------------
    //-------------------------------------------------------------------
    //-------------------------------------------------------------------
    //-------------------------------------------------------------------
    public class CSetupSmartImportEntiteFilleItem : CSetupSmartImportItem
    {
        private IEnumerable<CValeursProprietes> m_listeValeursDeFilles = null;
        private CMappageEntitesFilles m_mappageFilles = null;
        
        //A la création, la liste des valeursFilles n'est pas définie, elle n'est définie qu'à la demande
        //pour optimiser
        private CValeursProprietes m_valeursDuParent = null;

        //-------------------------------------------------------------------
        public CSetupSmartImportEntiteFilleItem(
            CSetupSmartImportItem parentItem,
            CDefinitionProprieteDynamique def,
            CValeursProprietes valeursDuParent,
            CMappageEntitesFilles mappageFille,
            int nColorIndex)
            : base(parentItem, def, nColorIndex)
        {
            m_valeursDuParent = valeursDuParent;
            m_mappageFilles = mappageFille;
            if (m_mappageFilles == null)
            {
                m_mappageFilles = new CMappageEntitesFilles();
                m_mappageFilles.Propriete = def;
            }
        }

        //-------------------------------------------------------------------
        public override string IdMappage
        {
            get { return "?"; }
        }

        //-------------------------------------------------------------------
        public override CObjetDonnee ObjetExempleAssocie
        {
            get { return null; }
        }

        //-------------------------------------------------------------------
        public IEnumerable<CValeursProprietes> ValeursDeFilles
        {
            get
            {
                if (m_listeValeursDeFilles == null)
                {
                    using (CWaitCursor waiter = new CWaitCursor())
                    {
                        if (m_valeursDuParent != null)
                            m_listeValeursDeFilles = m_valeursDuParent.GetValeursFilles(Propriete);
                        else
                            m_listeValeursDeFilles = new List<CValeursProprietes>();
                    }
                }
                return m_listeValeursDeFilles;
            }
            set
            {
                m_listeValeursDeFilles = value;
            }
        }

        //-------------------------------------------------------------------
        public CMappageEntitesFilles MappageEntitesFilles
        {
            get
            {
                return m_mappageFilles;
            }
            set
            {
                m_mappageFilles = value;
            }
        }

        //-------------------------------------------------------------------
        public override string LibelleValeur
        {
            get
            {
                if (m_listeValeursDeFilles != null)
                    return m_listeValeursDeFilles.Count().ToString();
                return "-";
            }
        }

        //-------------------------------------------------------------------
        public override bool UseAsKey
        {
            get
            {
                return false;
            }
            set { }
        }

        //-------------------------------------------------------------------
        public override bool CanBeUsedAsKey
        {
            get { return false; }
        }

        //-------------------------------------------------------------------
        public override CSourceSmartImport Source
        {
            get { return null; }
            set { }
        }

        //-------------------------------------------------------------------
        public override IEnumerable<Type> SourcesPossibles
        {
            get
            {
                return new Type[] { };
            }
        }

        //-------------------------------------------------------------------
        public override EOptionCreationElementImport OptionCreation
        {
            get
            {
                return EOptionCreationElementImport.None;
            }
            set
            {

            }
        }

        //-------------------------------------------------------------------
        public override bool CanHaveOptionCreation
        {
            get { return false; }
        }

        //-------------------------------------------------------------------
        public override bool IsEntite
        {
            get { return false; }
        }

        //-------------------------------------------------------------------
        public override bool IsExpandable
        {
            get { return true; }
        }

        //-------------------------------------------------------------------
        public override object ValeurParDefaut
        {
            get
            {
                return null;
            }
        }

        //-------------------------------------------------------------------------------
        public override bool HasConfigData()
        {
            if ( MappageEntitesFilles != null )
            {
                if (MappageEntitesFilles.MappagesEntitesFilles.Count() > 0)
                    return true;
                foreach ( CSetupSmartImportItem child in ChildItems )
                {
                    CSetupSmartImportChampEntiteItem itemEntite = child as CSetupSmartImportChampEntiteItem;
                    if (itemEntite != null && itemEntite.Source != null)
                        return true;
                }
            }
            return false;
        }

        //-------------------------------------------------------------------------------
        public override CResultAErreur MajConfig(CConfigMappagesSmartImport config)
        {
            CResultAErreur result = CResultAErreur.True;
            CMappageEntitesFilles mappage = new CMappageEntitesFilles();
            mappage.Propriete = MappageEntitesFilles.Propriete;

            List<CMappageEntiteFille> lstMapsFilles = new List<CMappageEntiteFille>();

            foreach (CSetupSmartImportItem child in ChildItems)
            {
                CSetupSmartImportChampEntiteItem itemEntite = child as CSetupSmartImportChampEntiteItem;
                if (itemEntite != null)
                {
                    if (itemEntite.Source != null)
                    {
                        CConfigMappagesSmartImport conf = new CConfigMappagesSmartImport();
                        result = itemEntite.MajConfig(conf);
                        if (!result)
                            return result;
                        CMappageEntiteFille mapFille = new CMappageEntiteFille();
                        mapFille.ConfigMappage = conf;
                        mapFille.Source = itemEntite.Source;
                        lstMapsFilles.Add(mapFille);
                    }
                }
            }
            mappage.MappagesEntitesFilles = lstMapsFilles;
            if (lstMapsFilles.Count > 0)
            {
                List<CMappageEntitesFilles> lst = new List<CMappageEntitesFilles>(config.MappagesEntitesFilles);
                lst.Add(mappage);
                config.MappagesEntitesFilles = lst;
            }
            return result;
        }
    }
}
