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
    public class CControleViewSmartImportResult : CCustomizableListAvecNiveau
    {
        //-------------------------------------------------------------------------------
        public CControleViewSmartImportResult()
            :base()
        {
            this.ItemControl = new CControleViewSmartImportResultField();
            
        }

        


        //-------------------------------------------------------------------------------
        public void Fill ( 
            CValeursProprietes valeurs )
        {
            List<CViewSmartImportResultItem> lst = new List<CViewSmartImportResultItem>();
            lst.Add(new CViewSmartImportResultItem(null, null, valeurs, null, 0));
            Items = lst.ToArray();
            Expand(Items[0]);
        }

        //-------------------------------------------------------------------------------
        internal void Expand(CCustomizableListItem item)
        {
            CViewSmartImportResultItem si = item as CViewSmartImportResultItem;
            if ( si != null && si.ChildItems.Count() == 0)
            {
                if (si.Valeur is CValeursProprietes)
                {
                    CreateChilds(si, (CValeursProprietes)si.Valeur);

                }
                else if (si.Valeur is IEnumerable<CValeursProprietes>)
                {
                    CreateChilds(si, (IEnumerable<CValeursProprietes>)si.Valeur);
                }
            }
            if (si != null)
                si.Expand();
            Refresh();
        }

        //-------------------------------------------------------------------------------
        internal void Collapse(CCustomizableListItem item)
        {
            CViewSmartImportResultItem si = item as CViewSmartImportResultItem;
            if (si != null)
                si.Collapse();
            Refresh();
        }

        //-------------------------------------------------------------------------------
        private void CreateChilds(CViewSmartImportResultItem si, CValeursProprietes valeurs)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            lst.AddRange(valeurs.GetDefinitionsSimples());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            int nIndex = si.Index;
            foreach ( CDefinitionProprieteDynamique def in lst )
            {
                CViewSmartImportResultItem item = new CViewSmartImportResultItem(
                    si,
                    def,
                    valeurs.GetValeurSimple(def),
                    valeurs.ValeursOriginales != null?
                        valeurs.ValeursOriginales.GetValeurSimple(def):
                        null,
                    si.ColorIndex);
                InsertItem(++nIndex, item, false);
            }


            //Champs parents
            int nColorIndex = si.ColorIndex;
            lst.Clear();
            lst.AddRange(valeurs.GetDefinitionsParentes());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            foreach (CDefinitionProprieteDynamique def in lst)
            {
                nColorIndex++;
                if ( CViewSmartImportResultItem.GetCouleur(nColorIndex) == si.BackColor)
                    nColorIndex++;
                CViewSmartImportResultItem item = new CViewSmartImportResultItem(
                    si,
                    def,
                    valeurs.GetValeurParente(def),
                     valeurs.ValeursOriginales != null?
                        valeurs.ValeursOriginales.GetValeurParente(def):
                        null,
                    nColorIndex);
                InsertItem(++nIndex, item, false);
            }

            //Champs filles
            lst.Clear();
            lst.AddRange(valeurs.GetDefinitionsFilles());
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            foreach (CDefinitionProprieteDynamique def in lst)
            {
                CViewSmartImportResultItem item = new CViewSmartImportResultItem(
                    si, 
                    def,
                    valeurs.GetValeursFilles(def),
                    null,
                    si.ColorIndex
                    );
                InsertItem(++nIndex, item, false);
            }
        }

        //-------------------------------------------------------------------------------
        private void CreateChilds(CViewSmartImportResultItem itemFille, 
            IEnumerable<CValeursProprietes> valeurs)
        {
            int nIndex = itemFille.Index;
            int nColIndex = itemFille.ColorIndex;
            if ( valeurs != null )
            {
                foreach ( CValeursProprietes val in valeurs)
                {
                    nColIndex++;
                    if (CViewSmartImportResultItem.GetCouleur(nColIndex) == itemFille.BackColor)
                        nColIndex++;
                    CViewSmartImportResultItem item = new CViewSmartImportResultItem(
                        itemFille,
                        null,
                        val,
                        null,
                        nColIndex);
                    InsertItem ( ++nIndex, item, false);
                }
            }
        }

        //-------------------------------------------------------------------------------
        public CViewSmartImportResultItem CurrentSetupItem
        {
            get
            {
                int? nIndex = CurrentItemIndex;
                if (nIndex != null && nIndex >= 0 && nIndex < Items.Count())
                    return Items[nIndex.Value] as CViewSmartImportResultItem;
                return null;
            }
        }

       
    }
}
