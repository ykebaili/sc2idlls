using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common.customizableList
{
    /// <summary>
    /// Tout contrôle qui, s'il est dans une customList, doit
    /// sauvegarder certaines données pour restaurer son aspect quand
    /// il est réactivé.
    /// Les données sont stockées dans un dictionnaire, objet->objet que chaque
    /// contrôle gère comme il le veut.
    /// 
    /// </summary>
    public interface IControlADonneesSpecifiquesDansCustomList
    {
        void SaveMyDonneesSpecifiquesCustomListSansMesControlesFils(CDonneesSpecifiquesControleDansCustomList donnees);
        void RestoreMyDonneesSpecifiquesCustomListSansMesControlesFils(CDonneesSpecifiquesControleDansCustomList donnees);
    }

    //--------------------------------------------------------------------------
    public static class CUtilDonneesSpecifiquesDansCustomList
    {
        public static void SaveDonneesControle(Control ctrl, CDonneesSpecifiquesControleDansCustomList donnees)
        {
            IControlADonneesSpecifiquesDansCustomList sp = ctrl as IControlADonneesSpecifiquesDansCustomList;
            if (sp != null)
                sp.SaveMyDonneesSpecifiquesCustomListSansMesControlesFils(donnees);
            donnees.SaveDonneesFils(ctrl);
        }

        public static void RestoreDonneesControle(Control ctrl, CDonneesSpecifiquesControleDansCustomList donnees)
        {
            IControlADonneesSpecifiquesDansCustomList sp = ctrl as IControlADonneesSpecifiquesDansCustomList;
            if (sp != null)
                sp.RestoreMyDonneesSpecifiquesCustomListSansMesControlesFils(donnees);
            donnees.RestoreDonneesFils(ctrl);
        }
    }

    //--------------------------------------------------------------------------
    public class CDonneesSpecifiquesControleDansCustomList
    {
        private Dictionary<object, object> m_dicValeurs = new Dictionary<object, object>();
        private Dictionary<Control, CDonneesSpecifiquesControleDansCustomList> m_donnesFils = new Dictionary<Control, CDonneesSpecifiquesControleDansCustomList>();

        //----------------------------------------------------
        public CDonneesSpecifiquesControleDansCustomList()
        {
        }

        //----------------------------------------------------
        public bool IsEmpty
        {
            get
            {
                return m_dicValeurs.Count == 0 && m_donnesFils.Count == 0;
            }
        }

        //----------------------------------------------------
        public void SetData<T>(object key, T valeur)
        {
                if (key != null)
                    m_dicValeurs[key] = valeur;
        }

        //----------------------------------------------------
        public T GetData<T>(object key, T defaultValue)
        {
            if (key == null)
                return defaultValue;
            object val = null;
            if (m_dicValeurs.TryGetValue(key, out val))
            {
                if (val is T)
                    return (T)val;
                return defaultValue;
            }
            return defaultValue;
        }

        //----------------------------------------------------
        public void SaveDonneesFils(Control ctrl)
        {
            foreach (Control fils in ctrl.Controls)
            {
                CDonneesSpecifiquesControleDansCustomList data = new CDonneesSpecifiquesControleDansCustomList();
                CUtilDonneesSpecifiquesDansCustomList.SaveDonneesControle(fils, data);
                if (!data.IsEmpty)
                    m_donnesFils[fils] = data;
            }
        }

        //----------------------------------------------------
        public void RestoreDonneesFils(Control ctrl)
        {
            foreach (Control fils in ctrl.Controls)
            {
                CDonneesSpecifiquesControleDansCustomList data = null;
                if (m_donnesFils.TryGetValue(fils, out data))
                {
                    CUtilDonneesSpecifiquesDansCustomList.RestoreDonneesControle(fils, data);
                }
            }
        }
    }



}
