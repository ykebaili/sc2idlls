using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.expression;
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
    public class CSetupSmartImportChampEntiteItem : CSetupSmartImportItem
    {
        private CConfigMappagesSmartImport m_config = null;
        private CValeursProprietes m_valeursExemple = null;
        private CSourceSmartImport m_source = null;

        

        //-------------------------------------------------------------------
        public CSetupSmartImportChampEntiteItem(
            CSetupSmartImportItem itempParent,
            CValeursProprietes valeursExemple,
            CSourceSmartImport source,
            CConfigMappagesSmartImport config,
            int nColorIndex)
            : base(itempParent, null, nColorIndex)
        {
            m_valeursExemple = valeursExemple;
            m_config = config;
            m_source = source;
            if (m_config == null && valeursExemple != null)
            {
                m_config = new CConfigMappagesSmartImport();
                m_config.KeyEntite = valeursExemple.DbKeyObjet;
                m_config.TypeEntite = valeursExemple.TypeObjet;
            }
            
        }

        //-------------------------------------------------------------------
        public override string IdMappage
        {
            get
            {
                if (ConfigMappage != null)
                    return ConfigMappage.IdMappage;
                return "?";
            }
        }

        //-------------------------------------------------------------------
        public override CObjetDonnee ObjetExempleAssocie
        {
            get
            {
                if (m_valeursExemple != null)
                    return m_valeursExemple.ObjetAssocie;
                if (ConfigMappage != null && ConfigMappage != null)
                {
                    Type tp = ConfigMappage.TypeEntite;
                    CDbKey key = ConfigMappage.KeyEntite;
                    CObjetDonnee objet = Activator.CreateInstance(tp, new object[] { CSc2iWin32DataClient.ContexteCourant }) as CObjetDonnee;
                    if (objet.ReadIfExists(key))
                        return objet;
                }
                return null;
            }
        }

        //-------------------------------------------------------------------
        public CValeursProprietes ValeursExemples
        {
            get
            {
                return m_valeursExemple;
            }
            set
            {
                m_valeursExemple = value;
            }
        }


        //-------------------------------------------------------------------
        public CConfigMappagesSmartImport ConfigMappage
        {
            get
            {
                return m_config;
            }
            set
            {
                m_config = value;
            }
        }

        //-------------------------------------------------------------------
        public override string LibelleValeur
        {
            get
            {
                if (m_valeursExemple != null)
                    return m_valeursExemple.LibelleObjet;
                else if (m_config != null && m_config.TypeEntite != null)
                    return DynamicClassAttribute.GetNomConvivial(m_config.TypeEntite);
                return "";
            }
        }

        //-------------------------------------------------------------------
        public override bool UseAsKey
        {
            get { return false; }
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
            get
            {
                return m_source;
            }
            set
            {
                m_source = value;
            }
        }

        //-------------------------------------------------------------------
        public override IEnumerable<Type> SourcesPossibles
        {
            get
            {
                return new Type[]{
                    typeof(CSourceSmartImportNoImport),
                    typeof(CSourceSmartImportObjet)
                };
            }
        }

        //-------------------------------------------------------------------
        public override bool CanHaveOptionCreation
        {
            get { return true; }
        }

        //-------------------------------------------------------------------
        public override EOptionCreationElementImport OptionCreation
        {
            get
            {
                if (ConfigMappage != null)
                    return ConfigMappage.OptionCreation;
                return EOptionCreationElementImport.None;
            }
            set
            {
                if (ConfigMappage != null)
                    ConfigMappage.OptionCreation = value;
            }
        }

        //-------------------------------------------------------------------
        public override bool IsEntite
        {
            get { return true; }
        }

        //-------------------------------------------------------------------
        public override bool IsExpandable
        {
            get { return true; }
        }

        //-------------------------------------------------------------------
        public override object ValeurParDefaut
        {
            get { return null; }
        }

        //-------------------------------------------------------------------------------
        public override bool HasConfigData()
        {
            return Source is CSourceSmartImportObjet && ConfigMappage != null;
        }

        //-------------------------------------------------------------------------------
        public override CResultAErreur MajConfig(CConfigMappagesSmartImport config)
        {
            CResultAErreur result = CResultAErreur.True;
            if (!(Source is CSourceSmartImportObjet))
                return result;
            if (ConfigMappage == null)
            {
                result.EmpileErreur(I.T("Can not calculate import setup|20121"));
                return result;
            }
            config.IdMappage = ConfigMappage.IdMappage;
            config.TypeEntite = ConfigMappage.TypeEntite;
            config.KeyEntite = ConfigMappage.KeyEntite;
            config.OptionCreation = ConfigMappage.OptionCreation;

            foreach (CSetupSmartImportItem child in ChildItems)
            {
                result = child.MajConfig(config);
                if (!result)
                    return result;
            }
            return result;

        }


    }
}
