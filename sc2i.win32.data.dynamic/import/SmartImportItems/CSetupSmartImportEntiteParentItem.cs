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
    public class CSetupSmartImportEntiteParentItem : CSetupSmartImportItem
    {
        private CValeursProprietes m_valeursParent = null;
        private CMappageEntiteParente m_mappageParent = null;

        //-------------------------------------------------------------------
        public CSetupSmartImportEntiteParentItem(
            CSetupSmartImportItem parentItem,
            CDefinitionProprieteDynamique def,
            CValeursProprietes valeursParent,
            CMappageEntiteParente mappageParent,
            int nColorIndex)
            : base(parentItem, def, nColorIndex)
        {
            m_valeursParent = valeursParent;
            m_mappageParent = mappageParent;
            if (m_mappageParent == null && m_valeursParent != null)
            {
                m_mappageParent = new CMappageEntiteParente();
                m_mappageParent.Propriete = def;
                m_mappageParent.ConfigEntiteParente = new CConfigMappagesSmartImport();
                m_mappageParent.ConfigEntiteParente.TypeEntite = def.TypeDonnee.TypeDotNetNatif;
                m_mappageParent.ConfigEntiteParente.KeyEntite = m_valeursParent.DbKeyObjet;
            }
        }

        //-------------------------------------------------------------------
        public override string IdMappage
        {
            get
            {
                if (MappageEntiteParente != null && MappageEntiteParente.ConfigEntiteParente != null)
                    return MappageEntiteParente.ConfigEntiteParente.IdMappage;
                return "?";
            }
        }

        //-------------------------------------------------------------------
        public override CObjetDonnee ObjetExempleAssocie
        {
            get
            {
                if (m_valeursParent != null)
                    return m_valeursParent.ObjetAssocie;
                if ( MappageEntiteParente != null && MappageEntiteParente.ConfigEntiteParente != null)
                {
                    Type tp = MappageEntiteParente.ConfigEntiteParente.TypeEntite;
                    CDbKey key = MappageEntiteParente.ConfigEntiteParente.KeyEntite;
                    CObjetDonnee objet = Activator.CreateInstance(tp, new object[] { CSc2iWin32DataClient.ContexteCourant }) as CObjetDonnee;
                    if (objet.ReadIfExists(key))
                        return objet;
                }
                return null;
            }
        }

        //-------------------------------------------------------------------
        public CValeursProprietes ValeursParent
        {
            get
            {
                return m_valeursParent;
            }
            set
            {
                m_valeursParent = value;
            }
        }

        //-------------------------------------------------------------------
        public CMappageEntiteParente MappageEntiteParente
        {
            get
            {
                return m_mappageParent;
            }
            set
            {
                m_mappageParent = value;
            }
        }

        //-------------------------------------------------------------------
        public override string LibelleValeur
        {
            get
            {
                if (m_valeursParent != null)
                    return m_valeursParent.LibelleObjet;
                else if (m_mappageParent != null && m_mappageParent.ConfigEntiteParente != null && m_mappageParent.ConfigEntiteParente.TypeEntite != null)
                    return DynamicClassAttribute.GetNomConvivial(m_mappageParent.ConfigEntiteParente.TypeEntite);
                return "";
            }
        }

        //-------------------------------------------------------------------
        public override bool UseAsKey
        {
            get
            {
                if (MappageEntiteParente != null)
                    return MappageEntiteParente.UseAsKey;
                return false;
            }
            set
            {
                if (MappageEntiteParente != null)
                    MappageEntiteParente.UseAsKey = value;
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
                if (MappageEntiteParente != null)
                    return MappageEntiteParente.Source;
                return null;
            }
            set
            {
                if (MappageEntiteParente != null)
                    MappageEntiteParente.Source = value;
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
                    typeof(CSourceSmartImportObjet),
                    typeof(CSourceSmartImportReference)};
            }
        }

        //-------------------------------------------------------------------
        public override EOptionCreationElementImport OptionCreation
        {
            get
            {
                if (MappageEntiteParente != null && MappageEntiteParente.ConfigEntiteParente != null)
                    return MappageEntiteParente.ConfigEntiteParente.OptionCreation;
                return EOptionCreationElementImport.None;
            }
            set
            {
                if (MappageEntiteParente != null && MappageEntiteParente.ConfigEntiteParente != null)
                    MappageEntiteParente.ConfigEntiteParente.OptionCreation = value;
            }
        }

        //-------------------------------------------------------------------
        public override bool CanHaveOptionCreation
        {
            get { return true; }
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
            get
            {
                CSourceSmartImportFixedValue sourceFixe = MappageEntiteParente != null ?
                    MappageEntiteParente.Source as CSourceSmartImportFixedValue :
                    null;
                if (sourceFixe != null)
                    return sourceFixe.Valeur;
                if (ValeursParent != null)
                {
                    CValeurParDefautObjetDonnee v = new CValeurParDefautObjetDonnee(
                        ValeursParent.TypeObjet,
                        ValeursParent.DbKeyObjet,
                        ValeursParent.LibelleObjet);
                    return v;
                }
                if (MappageEntiteParente != null && MappageEntiteParente.ConfigEntiteParente != null)
                {
                    CValeurParDefautObjetDonnee v = new CValeurParDefautObjetDonnee(
                        MappageEntiteParente.ConfigEntiteParente.TypeEntite,
                        MappageEntiteParente.ConfigEntiteParente.KeyEntite,
                        "");
                    CObjetDonnee o = v.GetObjet(CSc2iWin32DataClient.ContexteCourant);
                    if (o != null)
                        v.LibelleObjet = o.DescriptionElement;
                    return v;
                }
                
                return null;
            }
        }

        //-------------------------------------------------------------------------------
        public override bool HasConfigData()
        {
            return Source != null && !(Source is CSourceSmartImportNoImport);
        }

        //-------------------------------------------------------------------------------
        public override CResultAErreur MajConfig(CConfigMappagesSmartImport config)
        {
            CResultAErreur result = CResultAErreur.True;
            if (Source == null || Source is CSourceSmartImportNoImport)
                return result;

            CMappageEntiteParente mappage = new CMappageEntiteParente();
            mappage.Propriete = MappageEntiteParente.Propriete;
            mappage.UseAsKey = MappageEntiteParente.UseAsKey;
            mappage.Source = MappageEntiteParente.Source;
            mappage.ConfigEntiteParente = new CConfigMappagesSmartImport();
            mappage.ConfigEntiteParente.IdMappage = MappageEntiteParente.ConfigEntiteParente.IdMappage;
            mappage.ConfigEntiteParente.TypeEntite = MappageEntiteParente.ConfigEntiteParente.TypeEntite;
            mappage.ConfigEntiteParente.KeyEntite = MappageEntiteParente.ConfigEntiteParente.KeyEntite;
            mappage.ConfigEntiteParente.OptionCreation = MappageEntiteParente.ConfigEntiteParente.OptionCreation;

            List<CMappageEntiteParente> lstParents = new List<CMappageEntiteParente>(config.MappagesEntitesParentes);
            lstParents.Add(mappage);
            config.MappagesEntitesParentes = lstParents;
            if (Source is CSourceSmartImportFixedValue)//Dans ce cas, rien à faire de la suite
                return result;

            foreach (CSetupSmartImportItem child in ChildItems)
            {
                result = child.MajConfig(mappage.ConfigEntiteParente);
                if (!result)
                    return result;
            }

            return result;
        }
    }
}
