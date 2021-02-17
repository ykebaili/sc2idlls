using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Data;
using System.Collections;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Permet de gérer les options de clonage, à savoir, les sous entités qui sont clonées avec
    /// l'entité principale.
    /// </summary>
    [Serializable]
    [ReplaceClass("sc2i.process.CDicProprietesParType")]
    public class COptionsClonageEntite : Dictionary<Type, List<CDefinitionProprieteDynamique>>, I2iSerializable
    {
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public void AddDefinition(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (!TryGetValue(tp, out lst))
            {
                lst = new List<CDefinitionProprieteDynamique>();
                this[tp] = lst;
            }
            if (!lst.Contains(def))
                lst.Add(def);
        }

        //----------------------------------------------
        public void RemoveDefinition(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (TryGetValue(tp, out lst))
            {
                lst.Remove(def);
                if (lst.Count == 0)
                    Remove(tp);
            }
        }

        //----------------------------------------------
        public bool Contains(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (TryGetValue(tp, out lst))
                return lst.Contains(def);
            return false;
        }

        //----------------------------------------------
        public COptionsClonageEntite GetClone()
        {
            COptionsClonageEntite dic = new COptionsClonageEntite();
            foreach (KeyValuePair<Type, List<CDefinitionProprieteDynamique>> kv in this)
            {
                foreach (CDefinitionProprieteDynamique def in kv.Value)
                    dic.AddDefinition(kv.Key, def);
            }
            return dic;
        }



        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nNbDics = Count;
            serializer.TraiteInt(ref nNbDics);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (KeyValuePair<Type, List<CDefinitionProprieteDynamique>> paire in this)
                    {
                        Type tp = paire.Key;
                        List<CDefinitionProprieteDynamique> lst = paire.Value;
                        serializer.TraiteType(ref tp);
                        result = serializer.TraiteListe<CDefinitionProprieteDynamique>(lst);
                        if (!result)
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture:
                    Clear();
                    for (int nPaire = 0; nPaire < nNbDics; nPaire++)
                    {
                        Type tp = null;
                        List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
                        serializer.TraiteType(ref tp);
                        if (result)
                            result = serializer.TraiteListe<CDefinitionProprieteDynamique>(lst);
                        if (!result)
                            return result;
                        this[tp] = lst;
                    }
                    break;
            }
            return result;
        }


        /// ////////////////////////////////////////////////////////
        public CResultAErreurType<CObjetDonneeAIdNumeriqueAuto> CloneObjet(CObjetDonneeAIdNumeriqueAuto objetSource)
        {
            CResultAErreurType<CObjetDonneeAIdNumeriqueAuto> resObjet = new CResultAErreurType<CObjetDonneeAIdNumeriqueAuto>();
            CResultAErreur result = CResultAErreur.True;
            Dictionary<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto> dicClones = new Dictionary<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto>();
            resObjet = CloneObjet(objetSource.ContexteDonnee, objetSource, dicClones);
            if (!resObjet)
            {
                return resObjet;
            }
            Dictionary<object, object> copie = new Dictionary<object, object>();
            foreach (KeyValuePair<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto> kv in dicClones)
                copie[kv.Key] = kv.Value;
            foreach (CObjetDonneeAIdNumeriqueAuto objet in dicClones.Values)
            {
                if (objet.GetType().GetCustomAttributes(typeof(RefillAfterCloneAttribute), true).Length > 0)
                {
                    RefillAfterCloneAttribute.RefillAfterClone(objet, copie);
                }
            }
            return resObjet;
        }

        //-----------------------------------------------------------
        private CResultAErreurType<CObjetDonneeAIdNumeriqueAuto> CloneObjet(
            CContexteDonnee contexte,
            CObjetDonneeAIdNumeriqueAuto source,
            Dictionary<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto> dicClones)
        {
            CResultAErreurType<CObjetDonneeAIdNumeriqueAuto> result = new CResultAErreurType<CObjetDonneeAIdNumeriqueAuto>();
            if (dicClones.ContainsKey(source))
            {
                result.DataType = dicClones[source];
                return result;
            }
            if (dicClones.ContainsValue(source))
            {
                //C'est un objet qu'on a cloné, on ne va as le recloner encore
                result.DataType = source;
                return result;
            }
            CObjetDonneeAIdNumeriqueAuto clone = Activator.CreateInstance(source.GetType(), new object[] { contexte }) as CObjetDonneeAIdNumeriqueAuto;
            clone.CreateNewInCurrentContexte();
            dicClones[source] = clone;
            //Copie la ligne
            DataTable table = source.Row.Table;
            if (table == null)
                return result;
            List<string> keys = new List<string>();
            foreach (DataColumn col in table.PrimaryKey)
                keys.Add(col.ColumnName);
            clone.ContexteDonnee.CopyRow(source.Row, clone.Row, keys.ToArray());

            //Copie toutes les valeurs de champ custom
            IElementAChamps eltChampsDest = clone as IElementAChamps;
            IElementAChamps eltChampsSource = source as IElementAChamps;
            if (eltChampsDest != null && eltChampsSource != null)
            {
                foreach (CRelationElementAChamp_ChampCustom relation in eltChampsSource.RelationsChampsCustom)
                {
                    eltChampsDest.SetValeurChamp(relation.ChampCustom, relation.Valeur);
                }
            }

            //Copie les relations filles sélectionnées
            List<CDefinitionProprieteDynamique> lst = null;
            if (TryGetValue(source.GetType(), out lst))
            {
                foreach (CDefinitionProprieteDynamique def in lst)
                {
                    CDefinitionProprieteDynamique defInverse = def.GetDefinitionInverse(source.GetType());
                    if (defInverse != null)//On sait affecter le parent
                    {
                        CResultAErreur resTmp = CInterpreteurProprieteDynamique.GetValue(source, def);
                        if (resTmp && result.Data is IEnumerable)
                        {
                            IEnumerable en = result.Data as IEnumerable;
                            foreach (object obj in en)
                            {
                                CObjetDonneeAIdNumeriqueAuto objDonnee = obj as CObjetDonneeAIdNumeriqueAuto;
                                if (objDonnee != null)
                                {
                                    result = CloneObjet(contexte, objDonnee, dicClones);
                                    if (result)
                                    {
                                        resTmp = CInterpreteurProprieteDynamique.SetValue(result.DataType, defInverse, clone);
                                        if (!resTmp)
                                        {
                                            result.EmpileErreur(resTmp.Erreur);
                                            return result;
                                        }
                                    }
/*????                                    else
                                        return result;*/
                                }
                            }
                        }
                    }
                }
            }

            result.DataType = clone;
            return result;
        }
    }
}
