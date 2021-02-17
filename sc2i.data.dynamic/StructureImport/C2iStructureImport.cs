using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;
using sc2i.expression;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic.StructureImport
{
    [Flags]
    public enum EOptionImport
    {
        None,
        Update = 1,
        Create = 2
    }
        
    public class C2iStructureImport : I2iSerializable
    {
        private IParametreLectureFichier m_parametre = null;
        private List<CMappageChampImport> m_mappagesChamps = new List<CMappageChampImport>();
        private EOptionImport m_option = EOptionImport.Create | EOptionImport.Update;

        private Type m_typeCible = null;
        private bool m_bChargerTouteLaCible = false;

        private int m_nNbUpdated = 0;
        private int m_nNbCreated = 0;

        //------------------------------------
        public C2iStructureImport()
        {
        }

        public int NbUpdated
        {
            get { return m_nNbUpdated; }
        }

        public int NbCreated
        {
            get { return m_nNbCreated; }
        }


        //------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<IParametreLectureFichier>(ref m_parametre);
            if (result)
                result = serializer.TraiteListe<CMappageChampImport>(m_mappagesChamps);
            if (result)
                serializer.TraiteType(ref m_typeCible);
            if (!result)
                return result;

            int nTmp = (int)m_option;
            serializer.TraiteInt(ref nTmp);
            m_option = (EOptionImport)nTmp;

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bChargerTouteLaCible);

            return result;
        }

        //------------------------------------
        public bool ChargerTouteLaCible
        {
            get
            {
                return m_bChargerTouteLaCible;
            }
            set
            {
                m_bChargerTouteLaCible = value;
            }
        }

                

        //------------------------------------
        public EOptionImport OptionImport
        {
            get
            {
                return m_option;
            }
            set
            {
                m_option = value;
            }
        }

        //------------------------------------
        public Type TypeCible
        {
            get
            {
                return m_typeCible;
            }
            set
            {
                m_typeCible = value;
            }
        }

        //------------------------------------
        public IParametreLectureFichier ParametreLecture
        {
            get
            {
                return m_parametre;
            }
            set
            {
                m_parametre = value;
            }
        }

        //------------------------------------
        public void AddMappage(CMappageChampImport mappage)
        {
            m_mappagesChamps.Add(mappage);
        }

        //------------------------------------
        public CMappageChampImport GetMappage(C2iOrigineChampImport origine)
        {
            foreach (CMappageChampImport mappage in Mappages)
            {
                if (mappage.Origine.Equals(origine))
                    return mappage;
            }
            return null;
        }

        //------------------------------------
        public void RemoveMappage ( CMappageChampImport mappage )
        {
            m_mappagesChamps.Remove(mappage);
        }

        //------------------------------------
        public void ClearMappages()
        {
            m_mappagesChamps.Clear();
        }

        //------------------------------------
        public IEnumerable<CMappageChampImport> Mappages
        {
            get{
                return m_mappagesChamps.AsReadOnly();
            }
        }

        //------------------------------------
        public CResultAErreur Importer(string strFichier, CContexteDonnee contexteDestination)
        {
            CResultAErreur result = m_parametre.LectureFichier(strFichier);

            m_nNbUpdated = 0;
            m_nNbCreated = 0;

            if (!result)
                return result;

            if (m_bChargerTouteLaCible)
            {
                CListeObjetsDonnees lst = new CListeObjetsDonnees(contexteDestination, m_typeCible);
                lst.AssureLectureFaite();
            }

            //Vérifie que l'utilisateur a le droit de faire un import
            CSessionClient session = CSessionClient.GetSessionForIdSession(contexteDestination.IdSession);
            bool bALeDroit = false;
            if (session != null)
            {
                IInfoUtilisateur info = session.GetInfoUtilisateur();
                if ( info != null )
                {
                    bALeDroit = info.GetDonneeDroit(CDroitDeBaseSC2I.c_droitImport) != null;
                }
            }

            if (!bALeDroit)
            {
                result.EmpileErreur(I.T("Your are not allowed to import data|20048"));
                return result;
            }

            DataTable table = result.Data as DataTable;
            if (table == null)
            {
                result.EmpileErreur(I.T("Import error, can not read source data|20043"));
                return result;
            }
            int nParametre = 1;
            string strFiltre = "";
            foreach ( CMappageChampImport mappage in Mappages )
            {
                if ( mappage.IsCle )
                {
                    result = mappage.GetFiltreCle ( m_typeCible, nParametre );
                    if ( !result )
                        return result;
                    nParametre++;
                    strFiltre += result.Data.ToString()+" and ";
                }   
            }
            CFiltreData filtreCle = null;
            if ( strFiltre.Length > 0 )
                filtreCle = new CFiltreData ( strFiltre.Substring ( 0, strFiltre.Length-5 ) );
            foreach (DataRow row in table.Rows)
            {
                result = ImporteRow(row, filtreCle, contexteDestination);
                if (!result)
                    return result;
            }
            return result;
        }

        //------------------------------------
        private CResultAErreur ImporteRow(DataRow row, CFiltreData filtreCle, CContexteDonnee contexteDestination)
        {
            CResultAErreur result = CResultAErreur.True;

            Dictionary<CDefinitionProprieteDynamique, object> dicValeurs = new Dictionary<CDefinitionProprieteDynamique, object>();
            List<object> lstValeurs = new List<object>();
            if ( filtreCle != null )
                filtreCle.Parametres.Clear();
            foreach (CMappageChampImport mappage in Mappages)
            {
                object valeur = mappage.Origine.GetValeur(row);
                if (mappage.IsCle && filtreCle != null)
                {
                    if (valeur == null)
                    {
                        result.EmpileErreur("Can not import field @1 as key, because imported value is null|20045", mappage.ProprieteDestination.Nom);
                        return result;
                    }
                    filtreCle.Parametres.Add(valeur);
                }
                dicValeurs[mappage.ProprieteDestination] = valeur;
            }
            CObjetDonnee objet = Activator.CreateInstance(m_typeCible, new object[] { contexteDestination }) as CObjetDonnee;
            bool bCreate = true;
            //Cherche si l'objet exite
            if (filtreCle != null)
                bCreate = !objet.ReadIfExists(filtreCle, !m_bChargerTouteLaCible);
            if (bCreate && (OptionImport & EOptionImport.Create) != EOptionImport.Create)
                return result;
            if (!bCreate && (OptionImport & EOptionImport.Update) != EOptionImport.Update)
                return result;
            if (bCreate)
                objet.CreateNewInCurrentContexte(null);
            bool bUpdate = false;
            foreach (KeyValuePair<CDefinitionProprieteDynamique, object> kv in dicValeurs)
            {
                object val = kv.Value;
                if (kv.Value is int && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(kv.Key.TypeDonnee.TypeDotNetNatif))
                {
                    CObjetDonneeAIdNumerique obj = Activator.CreateInstance(kv.Key.TypeDonnee.TypeDotNetNatif, new object[] { contexteDestination }) as CObjetDonneeAIdNumerique;
                    if (!obj.ReadIfExists((int)kv.Value, !m_bChargerTouteLaCible))
                        obj = null;
                    val = obj;
                }

                result = CInterpreteurProprieteDynamique.GetValue(objet, kv.Key);
                if (result)
                {
                    object valeurOrigine = result.Data;
                    if ((val == null && valeurOrigine != null) ||
                        (valeurOrigine == null && val != null) ||
                        (val != null && valeurOrigine != null && !val.Equals(valeurOrigine)))
                    {
                        result = CInterpreteurProprieteDynamique.SetValue(objet, kv.Key, val);
                        if (!result)
                            return result;
                        bUpdate = true;
                    }
                }
            }
            if (bCreate)
                m_nNbCreated++;
            else if (bUpdate)
                m_nNbUpdated++;

            return result;
        }


                





    }
}
