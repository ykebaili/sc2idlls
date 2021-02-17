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
    public class CSetupSmartImportChampSimpleItem : CSetupSmartImportItem
    {
        private object m_valeurExemple = null;
        private CMappageChampSimple m_mappageSimple = null;

        //-------------------------------------------------------------------
        public CSetupSmartImportChampSimpleItem(
            CSetupSmartImportItem parentItem,
            CDefinitionProprieteDynamique def,
            object valeurExemple,
            CMappageChampSimple mappageSimple,
            int nColorIndex)
            : base(parentItem, def, nColorIndex)
        {
            m_valeurExemple = valeurExemple;
            m_mappageSimple = mappageSimple;
            if (m_mappageSimple == null)
            {
                m_mappageSimple = new CMappageChampSimple();
                m_mappageSimple.Propriete = def;
            }
        }

        //-------------------------------------------------------------------
        public object ValeurExemple
        {
            get
            {
                return m_valeurExemple;
            }
            set
            {
                m_valeurExemple = value;
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
        public CMappageChampSimple MappageSimple
        {
            get
            {
                return m_mappageSimple;
            }
            set
            {
                m_mappageSimple = value;
            }
        }


        //-------------------------------------------------------------------
        public override string LibelleValeur
        {
            get
            {
                if (m_valeurExemple != null)
                    return ValeurExemple.ToString();
                return "null";
            }
        }


        //-------------------------------------------------------------------
        public override bool UseAsKey
        {
            get
            {
                if (MappageSimple != null)
                    return MappageSimple.UseAsKey;
                return false;
            }
            set
            {
                if (MappageSimple != null)
                    MappageSimple.UseAsKey = value;
            }
        }

        //-------------------------------------------------------------------
        public override bool CanBeUsedAsKey
        {
            get { return true; }
        }

        //-------------------------------------------------------------------
        public override CSourceSmartImport Source
        {
            get
            {
                if (MappageSimple != null)
                    return MappageSimple.Source;
                return null;
            }
            set
            {
                if (MappageSimple != null)
                    MappageSimple.Source = value;
            }
        }

        //-------------------------------------------------------------------
        public override IEnumerable<Type> SourcesPossibles
        {
            get
            {
                return new Type[]{
                    typeof(CSourceSmartImportNoImport),
                    typeof(CSourceSmartImportFixedValue),
                    typeof(CSourceSmartImportField),
                    typeof(CSourceSmartImportFormula)};
            }
        }

        //-------------------------------------------------------------------
        public override EOptionCreationElementImport OptionCreation
        {
            get { return EOptionCreationElementImport.None; }
            set { }
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
            get { return false; }
        }

        //-------------------------------------------------------------------
        public override object ValeurParDefaut
        {
            get
            {
                CSourceSmartImportFixedValue sourceFixe = MappageSimple != null ?
                    MappageSimple.Source as CSourceSmartImportFixedValue :
                    null;
                if (sourceFixe != null)
                    return sourceFixe.Valeur;
                return ValeurExemple;
            }
        }

        //-------------------------------------------------------------------------------
        public override CResultAErreur MajConfig(CConfigMappagesSmartImport config)
        {
            CResultAErreur result = CResultAErreur.True;
            if (Source == null)
                return result;
            List<CMappageChampSimple> lst = new List<CMappageChampSimple>(config.MappagesChampsSimples);
            lst.Add(MappageSimple);
            config.MappagesChampsSimples = lst;
            return result;
        }

        //-------------------------------------------------------------------------------
        public override bool HasConfigData()
        {
            return Source != null;
        }
    }
}
