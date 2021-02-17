using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.win32.common.customizableList
{
    public class CCustomizableListAvecNiveau : CCustomizableList
    {

        //-------------------------------------------
        public CCustomizableListAvecNiveau()
            :base()
        {
            InitializeComponent();

        }

        //-------------------------------------------
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CCustomizableListAvecNiveau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.Name = "CCustomizableListAvecNiveau";
            this.ResumeLayout(false);

        }

        //---------------------------------------------------------------
        protected virtual CCustomizableListItemANiveau GetPrecedentPourNiveau(int nIndex, int nNiveauSouhaité)
        {
            if (nIndex > 0)
            {
                CCustomizableListItemANiveau itemPrec = null;
                //Trouve l'item précédent qui a le niveau inférieur
                for (int nPrec = nIndex - 1; nPrec >= 0; nPrec--)
                {
                    itemPrec = Items.ElementAt(nPrec) as CCustomizableListItemANiveau;
                    if (itemPrec.Niveau <= nNiveauSouhaité)
                        return itemPrec;
                }
            }
            return null;
        }

        //---------------------------------------------------------------
        public virtual CCustomizableListItemANiveau GetParent(CCustomizableListItemANiveau item)
        {
            if (item.Niveau == 0)
                return null;
            return GetPrecedentPourNiveau(item.Index, item.Niveau - 1);
        }


        //---------------------------------------------------------------
        public virtual void DecrementeNiveau(CCustomizableListItemANiveau item)
        {
            if (item == null || LockEdition)
                return;
            int nIndex = item.Index;
            int nOldNiveau = item.Niveau;
            //On ne peut pas décaller s'il y a un frère derrière, sinon, ça changerait les parentées
            bool bHasFrereApres = false;
            for (int nSuite = nIndex + 1; nSuite < Items.Count(); nSuite++)
            {
                CCustomizableListItemANiveau suite = Items[nSuite] as CCustomizableListItemANiveau;
                if (suite.Niveau == nOldNiveau)
                {
                    bHasFrereApres = true;
                    break;
                }
                if (suite.Niveau < nOldNiveau)
                    break;//Pas de frère après;
            }
            if (nOldNiveau > 0 && !bHasFrereApres)
            {
                List<CCustomizableListItemANiveau> lstSuivants = GetItemsDependants(item);
                CCustomizableListItemANiveau itemPrec = GetPrecedentPourNiveau(nIndex, nOldNiveau - 2);
                if (itemPrec != null || nOldNiveau - 1 <= 0)
                {
                    RefreshItem(GetParent(item));
                    item.ItemParent = itemPrec;
                    RefreshItem(item);
                    if (itemPrec != null)
                        RefreshItem(itemPrec);
                    foreach (CCustomizableListItemANiveau itemSuivant in lstSuivants)
                    {
                        RefreshItem(itemSuivant);
                    }
                }
            }
        }

        //---------------------------------------------------------------
        public virtual void IncrementeNiveau(CCustomizableListItemANiveau item)
        {
            if (item == null || LockEdition)
                return;
            int nIndex = item.Index;
            int nOldNiveau = item.Niveau;
            if (nIndex > 0)
            {
                List<CCustomizableListItemANiveau> lstSuivants = GetItemsDependants(item);
                CCustomizableListItemANiveau itemPrec = GetPrecedentPourNiveau(nIndex, nOldNiveau);
                if (itemPrec != null && !itemPrec.IsCollapse)
                {
                    RefreshItem(GetParent(item));
                    item.ItemParent = itemPrec;
                    RefreshItem(item);
                    if (itemPrec != null)
                        RefreshItem(itemPrec);
                    foreach (CCustomizableListItemANiveau itemSuivant in lstSuivants)
                    {
                        RefreshItem(itemSuivant);
                    }
                }
            }
        }

        //---------------------------------------------------------------
        protected virtual List<CCustomizableListItemANiveau> GetItemsDependants(CCustomizableListItemANiveau item)
        {
            int nNiveau = item.Niveau;
            List<CCustomizableListItemANiveau> items = new List<CCustomizableListItemANiveau>();
            for (int nIndex = item.Index + 1; nIndex < Items.Count(); nIndex++)
            {
                CCustomizableListItemANiveau itN = Items[nIndex] as CCustomizableListItemANiveau;
                if (itN != null && itN.Niveau > nNiveau)
                    items.Add(itN);
                else
                    break;
            }
            return items;
        }

        //---------------------------------------------------------------
        protected override void MoveItem(int nIndexSource, int nIndexDest, bool bCursorIsAvantIndex)
        {
            
            //Si glisse sous un truc collapse, considère qu'on glisse sous le dernier
            //du collapse
            if (nIndexDest - 1 >= 0)
            {
                CCustomizableListItemANiveau itemSel = Items[nIndexDest - 1] as CCustomizableListItemANiveau;
                if (itemSel != null &&
                    itemSel.IsCollapse && !itemSel.IsMasque)
                {

                    //Va chercher la dernière de ses dépendances
                    List<CCustomizableListItemANiveau> deps = GetItemsDependants(itemSel);
                    if (deps.Count > 0)
                        nIndexDest = deps[deps.Count - 1].Index;
                }
            }


            //Trouve tous les items qui sont fils de celui-ci
            CCustomizableListItemANiveau item = Items[nIndexSource] as CCustomizableListItemANiveau;
            CCustomizableListItemANiveau oldParent = GetParent(item);
            List<CCustomizableListItemANiveau> toMove = GetItemsDependants(item);
            toMove.Insert(0, item);
            int nFirst = toMove[0].Index;
            int nLast = toMove[toMove.Count() - 1].Index;
            if (nIndexDest >= nFirst && nIndexDest <= nLast)
                return;
            if (nIndexDest > nIndexSource)
            {
                foreach (CCustomizableListItemANiveau itemToMove in toMove)
                {
                    int nIndex = itemToMove.Index;
                    InsertItem(nIndexDest, itemToMove, false);
                    base.RemoveItem(nIndex, false);
                }
            }
            else
            {
                foreach (CCustomizableListItemANiveau itemToMove in toMove)
                {
                    int nIndex = itemToMove.Index;
                    base.RemoveItem(nIndex, false);
                    InsertItem(nIndexDest, itemToMove, false);
                    nIndexDest++;
                }
            }
            RenumerotteItems();
            //Décalle le premier là où il faut
            item = toMove[0] as CCustomizableListItemANiveau;

            CCustomizableListItemANiveau itemAvant = GetVisibleItemBefore(item.Index) as CCustomizableListItemANiveau;
            CCustomizableListItemANiveau itemApres = GetVisibleItemAfter(item.Index) as CCustomizableListItemANiveau;
            while (itemApres != null && toMove.Contains(itemApres))
                itemApres = GetVisibleItemAfter(itemApres.Index) as CCustomizableListItemANiveau;

            int nNiveauItem = item.Niveau;
            int? nNiveauAvant = itemAvant != null ? (int?)itemAvant.Niveau : null;
            int? nNiveauApres = itemApres != null ? (int?)itemApres.Niveau : null;

            int? nNiveauDest = null;

            if (nNiveauAvant != null)
            {
                if (nNiveauApres != null)
                {
                    if (nNiveauApres >= nNiveauAvant)
                        nNiveauDest = nNiveauApres.Value;
                }
                if (nNiveauDest == null)
                {
                    //Si on est sous le même parent 
                    CCustomizableListItemANiveau itemParentDeNiveau = null;
                    if (nNiveauItem > 0)
                    {
                        itemParentDeNiveau = GetPrecedentPourNiveau(item.Index, nNiveauItem - 1);
                        if (itemParentDeNiveau != null &&
                            itemParentDeNiveau.Equals(oldParent))
                            nNiveauDest = nNiveauItem;
                    }
                    if (nNiveauDest == null && nNiveauApres != null)
                        nNiveauDest = nNiveauApres;
                }

            }
            if (item.Index == 0 || nNiveauDest == null)
                nNiveauDest = 0;
            CCustomizableListItemANiveau itemParent = GetPrecedentPourNiveau(item.Index, nNiveauDest.Value - 1);
            if (itemParent != null || nNiveauDest == 0)
            {
                if (itemParent != null)
                    RefreshItem(itemParent);
                RefreshItem(oldParent);
                toMove[0].ItemParent = itemParent;
                foreach (CCustomizableListItemANiveau itemtoRefresh in toMove)
                    RefreshItem(itemtoRefresh);
            }

            Refresh();

        }
    }
}
