using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.multitiers.client;
using System.Data;

namespace sc2i.data
{
    [AutoExec("Autoexec")]
    public static class CDbKeyAddOn
    {

        private static bool m_bAutoexecDone = false;
        //----------------------------------------------------
        public static void Autoexec()
        {
            if (m_bAutoexecDone)
                return;
            m_bAutoexecDone = true;
            C2iSerializer.RegisterAfterReadOldDbKeyFunction(new C2iSerializer.AfterReadOldDbKeyCompatibleDelegate(AfterReadOldDbKey));
        }
        //----------------------------------------------------
        public static CDbKey CreateCDbKey(CObjetDonnee objet)
        {
            if (objet.ManageIdUniversel)
                return CDbKey.GetNewDbKeyOnUniversalIdANePasUtiliserEnDehorsDeCDbKeyAddOn(objet.IdUniversel);
            if (objet is CObjetDonneeAIdNumerique)
                return CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(((CObjetDonneeAIdNumerique)objet).Id);
            return null;
        }

        //----------------------------------------------------
        public static bool ReadIfExists(this CDbKey key, CObjetDonnee objet)
        {
            if (key.InternalUniversalIdANeJamaisUtiliserSaufDansCDbKeyAddOn.Length > 0)
            {
                if (objet.ReadIfExistsUniversalId(key.InternalUniversalIdANeJamaisUtiliserSaufDansCDbKeyAddOn))
                    return true;
                return false;
            }
            if (key.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn != null)
            {
                IObjetDonneeAIdNumerique objetId = objet as IObjetDonneeAIdNumerique;
                if (objetId != null && objetId.ReadIfExists(key.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn.Value))
                {
                    return true;
                }
            }
            return false;
        }


        //----------------------------------------------------
        public static CObjetDonnee GetObjet(this CDbKey key, CContexteDonnee contexte, Type typeObjet)
        {
            CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(typeObjet, new object[] { contexte });
            if (objet.ReadIfExists(key))
                return objet;
            return null;
        }

        //----------------------------------------------------
        public static void AfterReadOldDbKey(CDbKey key, Type typeObjet)
        {
            if (typeObjet == null || key == null)
                return;
            if (key.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn != null &&
                key.InternalUniversalIdANeJamaisUtiliserSaufDansCDbKeyAddOn.Length == 0)
            {
                string strId = GetIdUniverselFromId(typeObjet,
                    key.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn.Value);
                if (strId != null && strId.Length > 0)
                {
                    key.InternalUniversalIdANeJamaisUtiliserSaufDansCDbKeyAddOn = strId;
                    key.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn = null;
                }
            }
        }
                

        //----------------------------------------------------
        public static string GetIdUniverselFromId(Type typeElement, int nId)
        {
            string strNomTable = CContexteDonnee.GetNomTableForType(typeElement);
            CStructureTable structure = CStructureTable.GetStructure(typeElement);
            C2iRequeteAvancee rq = new C2iRequeteAvancee();
            rq.TableInterrogee = strNomTable;
            rq.FiltreAAppliquer = new CFiltreData(
                structure.ChampsId[0].NomChamp + "=@1", nId);
            rq.ListeChamps.Add(new C2iChampDeRequete(
                "UniversalId",
                new CSourceDeChampDeRequete(CObjetDonnee.c_champIdUniversel),
                typeof(string),
                OperationsAgregation.None,
                true));
            CResultAErreur result = rq.ExecuteRequete(0);
            if (result && result.Data is DataTable)
            {
                DataTable table = result.Data as DataTable;
                if (table.Rows.Count > 0)
                    return (string)table.Rows[0][0];
            }
            return "";
        }

        //----------------------------------------------------
        public static CDbKey GetDbKeyFromId(Type typeElement, int nId)
        {
            string strIdU = GetIdUniverselFromId(typeElement, nId);
            if (strIdU.Length > 0)
                return CDbKey.GetNewDbKeyOnUniversalIdANePasUtiliserEnDehorsDeCDbKeyAddOn(strIdU);
            return null;
        }

        //----------------------------------------------------
        public static int? GetIdFromUniverselId(Type typeElement, CDbKey key)
        {
            string strNomTable = CContexteDonnee.GetNomTableForType(typeElement);
            CStructureTable structure = CStructureTable.GetStructure(typeElement);
            C2iRequeteAvancee rq = new C2iRequeteAvancee();
            rq.TableInterrogee = strNomTable;
            rq.FiltreAAppliquer = new CFiltreData(
                CObjetDonnee.c_champIdUniversel+"=@1", key.GetValeurInDb() );
            rq.ListeChamps.Add(new C2iChampDeRequete(
                structure.ChampsId[0].NomChamp,
                new CSourceDeChampDeRequete(structure.ChampsId[0].NomChamp),
                typeof(string),
                OperationsAgregation.None,
                true));
            CResultAErreur result = rq.ExecuteRequete(0);
            if (result && result.Data is DataTable)
            {
                DataTable table = result.Data as DataTable;
                if (table.Rows.Count > 0)
                    return (int)table.Rows[0][0];
            }
            return null;
        }

    }

    
}
