using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc2i.win32.common.customizableList;
using sc2i.win32.common;
using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.expression;
using System.Drawing;
using System.Data;

namespace sc2i.win32.data.dynamic.import
{
    public class CControleSetupSmartImport : CCustomizableListAvecNiveau
    {
        private bool m_bMasquerSourcesNulles = false;
        //-------------------------------------------------------------------------------
        public CControleSetupSmartImport()
            :base()
        {
            CControleSetupSmartImportField ctrl = new CControleSetupSmartImportField();
            ctrl.LockEdition = LockEdition;
            this.ItemControl = ctrl;
        }


        //-------------------------------------------------------------------------------
        public bool HideNullSources
        {
            get
            {
                return m_bMasquerSourcesNulles;
            }
            set { m_bMasquerSourcesNulles = value; }
        }


        //-------------------------------------------------------------------------------
        public void Fill ( 
            CValeursProprietes valeurs, 
            CConfigMappagesSmartImport configMappage,
            DataTable sourceTable )
        {
            using (CWaitCursor waiter = new CWaitCursor())
            {
                List<CSetupSmartImportItem> lst = new List<CSetupSmartImportItem>();
                lst.Add(new CSetupSmartImportChampEntiteItem(null,
                    valeurs,
                    new CSourceSmartImportObjet(),
                    configMappage,
                    0));
                ((CControleSetupSmartImportField)ItemControl).SourceTable = sourceTable;
                Items = lst.ToArray();
                ExpandItemsWithData(Items[0] as CSetupSmartImportItem);
            }
            
        }

        //-------------------------------------------------------------------------------
        private void ExpandItemsWithData(CSetupSmartImportItem item )
        {
            if (item == null)
                return;
            if (item.HasConfigData())
            {
                if (!(item.Source is CSourceSmartImportReference) &&
                    !(item.Source is CSourceSmartImportFixedValue) ||
                    item.ItemParent == null)
                {

                    Expand(item);
                    foreach (CSetupSmartImportItem child in item.ChildItems)
                        ExpandItemsWithData(child);
                }
            }
        }

        //-------------------------------------------------------------------------------
        internal void Expand(CCustomizableListItem item)
        {
            using (CWaitCursor waiter = new CWaitCursor())
            {
                CSetupSmartImportItem si = item as CSetupSmartImportItem;
                if (si != null && si.ChildItems.Count() == 0)
                {
                    CSetupSmartImportChampEntiteItem root = si as CSetupSmartImportChampEntiteItem;
                    if (root != null)
                    {
                        CreateChilds(si, root.ValeursExemples, root.ConfigMappage);
                    }
                    CSetupSmartImportEntiteParentItem itemParent = si as CSetupSmartImportEntiteParentItem;
                    if (itemParent != null)
                    {
                        CreateChilds(
                            si,
                            itemParent.ValeursParent,
                            itemParent.MappageEntiteParente != null ? itemParent.MappageEntiteParente.ConfigEntiteParente : null);
                    }
                    CSetupSmartImportEntiteFilleItem itemFille = si as CSetupSmartImportEntiteFilleItem;
                    if (itemFille != null)
                    {
                        CreateChilds(itemFille);
                    }
                }
                if (si != null)
                    si.Expand();
                Refresh();
            }
        }

        //-------------------------------------------------------------------------------
        internal void Collapse(CCustomizableListItem item)
        {
            CSetupSmartImportItem si = item as CSetupSmartImportItem;
            if (si != null)
                si.Collapse();
            Refresh();
        }

        //-------------------------------------------------------------------------------
        private void CreateChilds ( CSetupSmartImportItem si, CValeursProprietes valeurs, CConfigMappagesSmartImport configMappage)
        {
            //Champs simples
            HashSet<CDefinitionProprieteDynamique> set = new HashSet<CDefinitionProprieteDynamique>();
            if (valeurs != null)
                foreach (CDefinitionProprieteDynamique prop in valeurs.GetDefinitionsSimples())
                    if ( prop != null )
                        set.Add(prop);
            if (configMappage != null)
                foreach (CMappageChampSimple mappage in configMappage.MappagesChampsSimples)
                    if (mappage.Propriete != null)
                        set.Add(mappage.Propriete);
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(set.ToArray());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            int nIndex = si.Index;
            foreach ( CDefinitionProprieteDynamique def in lst )
            {
                CMappageChampSimple mappageSimple = configMappage != null ?
                    configMappage.GetMappageSimpleFor(def) :
                    null;
                bool bCreate = true;
                if ( HideNullSources )
                {
                    bCreate = mappageSimple != null && mappageSimple.Source != null;
                }
                if (bCreate)
                {
                    CSetupSmartImportChampSimpleItem item = new CSetupSmartImportChampSimpleItem(
                        si,
                        def,
                        valeurs != null ? valeurs.GetValeurSimple(def) : null,
                        mappageSimple,
                        si.ColorIndex);
                    InsertItem(++nIndex, item, false);
                }
            }


            //Champs parents
            int nColorIndex = si.ColorIndex;
            set.Clear();
            if ( valeurs != null )
                foreach ( CDefinitionProprieteDynamique def in valeurs.GetDefinitionsParentes())
                    if ( def != null )
                        set.Add ( def );
            if ( configMappage != null )
                foreach ( CMappageEntiteParente map in configMappage.MappagesEntitesParentes)
                    if ( map.Propriete != null )
                        set.Add ( map.Propriete);
            lst = new List<CDefinitionProprieteDynamique>(set.ToArray());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            foreach (CDefinitionProprieteDynamique def in lst)
            {
                CMappageEntiteParente mappageParent = configMappage != null ? configMappage.GetMappageParentFor(def) : null;
                bool bCreate = true;
                if (HideNullSources)
                {
                    bCreate = mappageParent != null && mappageParent.Source != null;
                }
                if (bCreate)
                {
                    nColorIndex++;
                    if (CSetupSmartImportItem.GetCouleur(nColorIndex) == si.BackColor)
                        nColorIndex++;

                    CSetupSmartImportEntiteParentItem item = new CSetupSmartImportEntiteParentItem(
                        si,
                        def,
                        valeurs != null ? valeurs.GetValeurParente(def) : null,
                        mappageParent,
                        nColorIndex);
                    InsertItem(++nIndex, item, false);
                }
            }

            set.Clear();
            if ( valeurs != null )
                foreach ( CDefinitionProprieteDynamique def in valeurs.GetDefinitionsFilles())
                    if ( def != null )
                        set.Add ( def );
            if ( configMappage != null )
                foreach ( CMappageEntitesFilles map in configMappage.MappagesEntitesFilles)
                    if ( map.Propriete != null )
                        set.Add ( map.Propriete);
            lst = new List<CDefinitionProprieteDynamique>(set.ToArray());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            foreach (CDefinitionProprieteDynamique def in lst)
            {
                CMappageEntitesFilles mappageFilles = configMappage != null ? configMappage.GetMappageFilleFor(def) : null;
                bool bCreate = true;
                if (HideNullSources)
                    bCreate = mappageFilles != null && mappageFilles.MappagesEntitesFilles.Count() > 0;
                if (bCreate)
                {
                    CSetupSmartImportEntiteFilleItem item = new CSetupSmartImportEntiteFilleItem(
                        si,
                        def,
                        valeurs,
                        mappageFilles,
                        si.ColorIndex
                        );
                    InsertItem(++nIndex, item, false);
                }
            }
        }

        //-------------------------------------------------------------------------------
        private void CreateChilds (CSetupSmartImportEntiteFilleItem itemFille)
        {
            int nIndex = itemFille.Index;
            int nColIndex = itemFille.ColorIndex;
            HashSet<CDbKey> keysDone = new HashSet<CDbKey>();
            if ( itemFille.ValeursDeFilles != null )
            {
                foreach ( CValeursProprietes val in itemFille.ValeursDeFilles)
                {
                    CMappageEntiteFille mappage = itemFille.MappageEntitesFilles != null ?
                        itemFille.MappageEntitesFilles.GetMappageForEntite(val.DbKeyObjet):
                        null;
                    bool bCreate = true;
                    if (HideNullSources)
                        bCreate = mappage != null && mappage.Source != null;
                    if (bCreate)
                    {
                        nColIndex++;
                        if (CSetupSmartImportItem.GetCouleur(nColIndex) == itemFille.BackColor)
                            nColIndex++;
                        CSetupSmartImportChampEntiteItem item = new CSetupSmartImportChampEntiteItem(
                            itemFille,
                            val,
                            mappage != null ? mappage.Source : null,
                            mappage != null ? mappage.ConfigMappage : null,
                            nColIndex);
                        InsertItem(++nIndex, item, false);
                        if (val.DbKeyObjet != null)
                            keysDone.Add(val.DbKeyObjet);
                    }
                }
            }
            if ( itemFille.MappageEntitesFilles != null )
            {
                IEnumerable<CMappageEntiteFille> mapsFilles = itemFille.MappageEntitesFilles.MappagesEntitesFilles;
                foreach (CMappageEntiteFille mapFille in mapsFilles)
                {
                    if (mapFille.Source != null && mapFille.ConfigMappage != null)
                    {
                        CConfigMappagesSmartImport conf = mapFille.ConfigMappage;
                        if (conf.KeyEntite == null || !keysDone.Contains(conf.KeyEntite))
                        {
                            nColIndex++;
                            if (CSetupSmartImportItem.GetCouleur(nColIndex) == itemFille.BackColor)
                                nColIndex++;
                            CSetupSmartImportChampEntiteItem item = new CSetupSmartImportChampEntiteItem(
                            itemFille,
                            null,
                            mapFille.Source,
                            conf,
                            nColIndex);
                            InsertItem(++nIndex, item, false);
                            if (conf.KeyEntite != null)
                                keysDone.Add(conf.KeyEntite);
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------
        public CSetupSmartImportItem CurrentSetupItem
        {
            get
            {
                int? nIndex = CurrentItemIndex;
                if (nIndex != null && nIndex >= 0 && nIndex < Items.Count())
                    return Items[nIndex.Value] as CSetupSmartImportItem;
                return null;
            }
        }

        //-------------------------------------------------------------------------------
        public CResultAErreurType<CConfigMappagesSmartImport> GetConfigFinale()
        {
            MajChamps();
            CResultAErreurType<CConfigMappagesSmartImport> resFinal = new CResultAErreurType<CConfigMappagesSmartImport>();
            if ( Items.Count() == 0 || !(Items[0] is CSetupSmartImportChampEntiteItem))
            {
                resFinal.EmpileErreur(I.T("Can not calculate import setup|20121"));
                return resFinal;
            }
            CSetupSmartImportChampEntiteItem root = Items[0] as CSetupSmartImportChampEntiteItem;
            CConfigMappagesSmartImport config = new CConfigMappagesSmartImport();
            CResultAErreur result = root.MajConfig(config);
            if (!result)
            {
                resFinal.EmpileErreur(result.Erreur);
                return resFinal;
            }
            resFinal.DataType = config;
            return resFinal;
        }

        //-------------------------------------------------------------------------------
        private bool HighLight ( string strIdsMappage, string strChampEnCours )
        {
            //Pas terminé, trucs plus urgents à faire
            foreach ( CCustomizableListItem item in Items)
            {
                CSetupSmartImportItem si = item as CSetupSmartImportItem;
                if (si != null)
                {
                    if ( si.Highlight )
                        RefreshItem(si);
                    si.Highlight = false;
                }
            }
            try
            {
                if (Items.Count() == 0)
                    return false;
                string[] strIds = strIdsMappage.Split('.');
                if (strIds.Length == 0)
                    return false;
                CSetupSmartImportChampEntiteItem root = Items[0] as CSetupSmartImportChampEntiteItem;
                if (root == null || root.ConfigMappage == null || root.ConfigMappage.IdMappage != strIds[0])
                    return false;
            }
            finally
            {
            }
            return false;
                
        }

       
    }
}
