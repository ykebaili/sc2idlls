using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using System.Collections;

namespace sc2i.data.dynamic
{
    [AutoExec("Autoexec")]
    public class CUtilElementAChamps
    {
        private const string c_champIdsChampsLus = "READED_FIELDS";

        //---------------------------------------------------------------------------------
        private static bool m_bAutoexecFait = false;
        public static void Autoexec()
        {
            if (!m_bAutoexecFait)
            {
                CContexteDonnee.OnInvalideCacheRelation += new OnInvalideCachesRelations(CContexteDonnee_OnInvalideCacheRelation);
            }
        }

        //---------------------------------------------------------------------------------
        public static object GetValeurChamp(IObjetDonneeAChamps objet, string strIdChamp)
        {
            CChampCustom champ = new CChampCustom(objet.ContexteDonnee);
            if (champ.ReadIfExists(CDbKey.CreateFromStringValue(strIdChamp)))
            {
                return GetValeurChamp(objet, champ.Id, DataRowVersion.Default);
            }
            return null;
        }


		//---------------------------------------------------------------------------------
		public static object GetValeurChamp(IObjetDonneeAChamps objet, int nIdChamp)
		{
			return GetValeurChamp(objet, nIdChamp, DataRowVersion.Default);
		}

        //---------------------------------------------------------------------------------
        public static object GetValeurChamp(IObjetDonneeAChamps objet, int nIdChamp, DataRowVersion version)
        {
            string strTableValeurs = objet.GetNomTableRelationToChamps();
            string strChampId = objet.GetChampId();

            if (!objet.IsDependanceChargee(strTableValeurs, strChampId) && !IsChampLu(objet, nIdChamp))
            {
                object dummy;
                dummy = objet.RelationsChampsCustom;
                SetTousChampsLus(objet);
            }

            DataTable table = objet.ContexteDonnee.Tables[strTableValeurs];
			string strFiltre = CChampCustom.c_champId + "=" + nIdChamp + " and " +
				strChampId + "=" + ((IObjetDonneeAIdNumerique)objet).Id;

            //Stef 20/07/2010 : si on fait un get avec version à -1, on récupère la version Referentiel.
            //Le pb était que si on ne fait pas ça, on utilise CContexteDonnee.c_colIsHorsVersion = 0, 
            //ce qui peut ramener plus d'1 élément, du coup, GetValeur champ retourne
            //n'importe quelle valeur, et pas forcement celle du référentiel
            if (table.Columns.Contains ( CSc2iDataConst.c_champIdVersion) )
            {
                if (objet.ContexteDonnee.IdVersionDeTravail == null || objet.ContexteDonnee.IdVersionDeTravail == -1)
                {
                    strFiltre += " and " + CSc2iDataConst.c_champIdVersion + " is null and " +
                        CSc2iDataConst.c_champIsDeleted + "=0";
                }
                else
                {
                    strFiltre += " and " + CContexteDonnee.c_colIsHorsVersion + "=0";
                }
            }
            DataRow[] rows = table.Select( strFiltre);

            if (rows.Length > 0)
            {
                CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)objet.ContexteDonnee.GetNewObjetForTable(table);
                DataRow row = rows[0];
                if (row.HasVersion(version))
                {
                    rel.SetRow(row);
                    rel.VersionToReturn = version;
                    return rel.Valeur;
                }
                else
                    return null;
            }


            /*CListeObjetsDonnees liste = GetRelationsToChamps(nIdChamp);
				
            if (liste.Count > 0)
                return ((CRelationElementAChamp_ChampCustom) liste[0]).Valeur;*/

            //Changement le 12/10/03 : 
            /*Si l'objet n'a pas la relation du champ, mais qu'elle devrait (par ses groupes),
             * retourne la valeur par défaut du champ
             * si le champ ne doit pas apparaitre dans cet élément, retourne null
             * */
            // Lecture de la valeur par défaut du champ
            /* le 3/2/04 pourquoi ? ça ralentit tout !!, annule cette modif*/
            CChampCustom champ = new CChampCustom(objet.ContexteDonnee);
            if (champ.ReadIfExists(nIdChamp))
            {
                object valeurParDefaut = champ.ValeurParDefaut;
                /*if ( valeurParDefaut == null )
                    return null;
                //La valeur par défaut n'est pas null, regarde si cet élément doit avoir ce champ
                foreach ( CChampCustom champNormal in TousLesChamps )
                {
                        if ( champNormal == champ )
                            return valeurParDefaut;
                }*/
                return valeurParDefaut;
            }
            return null;
        }

        //-------------------------------------------------------------------
        public static CRelationElementAChamp_ChampCustom GetRelationToChamp(IObjetDonneeAChamps objet,
            int nIdChamp)
        {
            string strTableValeurs = objet.GetNomTableRelationToChamps();
            string strChampId = objet.GetChampId();
            object dummy = null;
            if (!objet.IsDependanceChargee(strTableValeurs, strChampId))
                dummy = objet.RelationsChampsCustom;
            DataTable table = objet.ContexteDonnee.Tables[strTableValeurs];

            string strFiltre = CChampCustom.c_champId + "=" + nIdChamp + " and " +
                strChampId + "=" + ((IObjetDonneeAIdNumerique)objet).Id;


            //Stef 20/07/2010 : si on fait un set avec version à -1, on modifie la version Referentiel.
            //Le pb était que si on ne fait pas ça, on utilise CContexteDonnee.c_colIsHorsVersion = 0, 
            //ce qui peut ramener plus d'1 élément, du coup, applique version fonctionne de manière aléatoire
            //car parfois, on modifie bien la version référentiel, d'autres fois, on modifie
            //autre chose (suivant ce qui est dans le contexte de données).
            if (table.Columns.Contains ( CSc2iDataConst.c_champIdVersion) )
            {
                if (objet.ContexteDonnee.IdVersionDeTravail == null || objet.ContexteDonnee.IdVersionDeTravail == -1)
                {
                    strFiltre += " and " + CSc2iDataConst.c_champIdVersion + " is null and " +
                        CSc2iDataConst.c_champIsDeleted + "=0";
                }
                else
                {
                    strFiltre += " and " + CContexteDonnee.c_colIsHorsVersion + "=0";
                }
            }

            DataRow[] rows = table.Select(strFiltre);
            if (rows.Length > 0)
            {
                CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)objet.ContexteDonnee.GetNewObjetForTable(table);
                rel.SetRow(rows[0]);
                return rel;
            }
            return null;
        }

        //---------------------------------------------------------------------------------
        public static CResultAErreur SetValeurChamp(IObjetDonneeAChamps objet, string strIdChamp, object valeur)
        {
            CResultAErreur result = CResultAErreur.True;

            CChampCustom champ = new CChampCustom(objet.ContexteDonnee);
            if (champ.ReadIfExists(CDbKey.CreateFromStringValue(strIdChamp)))
            {
                return SetValeurChamp(objet, champ.Id, valeur);
            }

            result.EmpileErreur("Invalid Custom Field Id in SetValeurChamps (Line 179)");
            return result;
        }

        //-------------------------------------------------------------------
        public static CResultAErreur SetValeurChamp(IObjetDonneeAChamps objet, int nIdChamp, object valeur)
        {
            /*
            ///Stef 18052011, passage de la recherche de l'élément sous forme de fonction
            
			string strTableValeurs = objet.GetNomTableRelationToChamps();
			string strChampId = objet.GetChampId();
			object dummy = null;
			if (!objet.IsDependanceChargee(strTableValeurs, strChampId))
				dummy = objet.RelationsChampsCustom;
			DataTable table = objet.ContexteDonnee.Tables[strTableValeurs];

			string strFiltre = CChampCustom.c_champId + "=" + nIdChamp + " and " +
				strChampId + "=" + ((IObjetDonneeAIdNumerique)objet).Id;


            //Stef 20/07/2010 : si on fait un set avec version à -1, on modifie la version Referentiel.
            //Le pb était que si on ne fait pas ça, on utilise CContexteDonnee.c_colIsHorsVersion = 0, 
            //ce qui peut ramener plus d'1 élément, du coup, applique version fonctionne de manière aléatoire
            //car parfois, on modifie bien la version référentiel, d'autres fois, on modifie
            //autre chose (suivant ce qui est dans le contexte de données).
            if (objet.ContexteDonnee.IdVersionDeTravail == null || objet.ContexteDonnee.IdVersionDeTravail == -1)
			{
				strFiltre += " and " + CSc2iDataConst.c_champIdVersion + " is null and " +
					CSc2iDataConst.c_champIsDeleted + "=0";
			}
			else
			{
				strFiltre += " and " + CContexteDonnee.c_colIsHorsVersion + "=0";
			}

			DataRow[] rows = table.Select(strFiltre);
			if (rows.Length > 0)
			{
				CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)objet.ContexteDonnee.GetNewObjetForTable(table);
				rel.SetRow(rows[0]);
				rel.Valeur = valeur;
				return CResultAErreur.True;
			}*/
            CRelationElementAChamp_ChampCustom rel = GetRelationToChamp(objet, nIdChamp);
            if (rel != null)
            {
                rel.Valeur = valeur;
                return CResultAErreur.True;
            }


            CChampCustom champ = new CChampCustom(objet.ContexteDonnee);
            CResultAErreur result = CResultAErreur.True;
            if (champ.ReadIfExists(nIdChamp))
            {
                CRelationElementAChamp_ChampCustom relation = objet.GetNewRelationToChamp();
                relation.CreateNewInCurrentContexte();
                relation.ElementAChamps = objet;
                relation.ChampCustom = champ;
                relation.Valeur = valeur;
            }
            else
            {
                result.EmpileErreur(I.T("The field n°@1 doesn't exist|234", nIdChamp.ToString()));
            }
            return result;
        }

		public static CResultAErreur VerifieDonnees(IElementAChamps elementAChamps)
		{
			IDefinisseurChampCustom[] definisseurs = elementAChamps.DefinisseursDeChamps;
			C2iExpressionVrai constanteTrue = new C2iExpressionVrai();
			string strValTrue = C2iExpressionConstante.GetPseudoCode(constanteTrue);
			CResultAErreur result = CResultAErreur.True;
			foreach (IDefinisseurChampCustom definisseur in definisseurs)
			{
				foreach (CChampCustom champ in definisseur.TousLesChampsAssocies)
				{
					if (champ.DataFormuleValidation.CompareTo(strValTrue) != 0)
					{
						result &= champ.VerifieValeur(elementAChamps.GetValeurChamp(champ.Id));
					}
				}
			}
			if (result.Erreur.Erreurs.Length > 0)
				result.SetFalse();
			return result;
		}


        //-------------------------------------------------------------------
        /// <summary>
        /// ids des champs qui ont été lus. Utilisés lorsqu'on lit partiellement les données
        /// </summary>
        protected static Hashtable GetIdsChampsLus(IObjetDonneeAChamps obj)
        {
            if (obj.ContexteDonnee.Tables[obj.GetNomTable()].Columns[c_champIdsChampsLus] == null)
                return null;
            Hashtable hash = obj.Row[c_champIdsChampsLus] as Hashtable;
            return hash;
        }

        //-------------------------------------------------------------------
        protected static void SetIdsChampsLus(IObjetDonneeAChamps obj, Hashtable lst)
        {
            DataTable table = obj.ContexteDonnee.Tables[obj.GetNomTable()];
            if (table.Columns[c_champIdsChampsLus] == null)
            {
                DataColumn col = new DataColumn(c_champIdsChampsLus);
                table.RowChanged += new DataRowChangeEventHandler(OnChangeRowAvecChamps);
                col.DataType = typeof(Hashtable);
                table.Columns.Add(col);
            }
            object val = lst;
            if (val == null)
                val = DBNull.Value;
            CContexteDonnee.ChangeRowSansDetectionModification(obj.Row, c_champIdsChampsLus, val);
        }

        static void OnChangeRowAvecChamps(object sender, DataRowChangeEventArgs e)
        {
        }


        //-------------------------------------------------------------------
        public static void SetChampsLus(IObjetDonneeAChamps obj, params int[] champs)
        {
            Hashtable champsLus = GetIdsChampsLus(obj);
            if (champsLus == null)
            {
                champsLus = new Hashtable();
                SetIdsChampsLus(obj, champsLus);
            }
            foreach (int nId in champs)
                champsLus[nId]=true;
        }

        //-------------------------------------------------------------------
        public static void SetTousChampsLus(IObjetDonneeAChamps obj)
        {
            Hashtable champsLus = new Hashtable();
            //Lorsque champs lus contient -1, ça veut dire que tous les champs ont été lus
            champsLus[-1] = true;
            SetIdsChampsLus(obj, champsLus);
        }

        //-------------------------------------------------------------------
        public static bool IsChampLu(IObjetDonneeAChamps obj, int nIdChamp)
        {
            Hashtable champsLus = GetIdsChampsLus(obj);
            if (champsLus == null)
                return false;
            return champsLus.Contains(nIdChamp) || champsLus.Contains(-1);
        }

        //-------------------------------------------------------------------
        //S'il n'y a pas de paramètres, lit tous les champs custom
        public static void ReadChampsCustom(CListeObjetsDonnees lstObjets, params int[] idsChamps)
        {
            if (lstObjets.Count == 0)
                return;
            IObjetDonneeAChamps eltAChamps = lstObjets[0] as IObjetDonneeAChamps;
            if (eltAChamps == null)
            {
                return;
            }
            if ( idsChamps.Length == 0 )
            {
                lstObjets.ReadDependances("RelationsChampsCustom");
                foreach (IObjetDonneeAChamps eltAChamp in lstObjets)
                    SetTousChampsLus(eltAChamp);
                return;
            }
            List<string> lstChamps = new List<string>();
            foreach (int nIdChamp in idsChamps)
            {
                CChampCustom champ = new CChampCustom(eltAChamps.ContexteDonnee);
                if (champ.ReadIfExists(nIdChamp))
                {
                    CDefinitionProprieteDynamiqueChampCustom def = new CDefinitionProprieteDynamiqueChampCustom(champ);
                    lstChamps.Add(def.NomPropriete);
                }
            }
            lstObjets.ReadDependances(lstChamps.ToArray());
            foreach (IObjetDonneeAChamps eltAChamp in lstObjets)
                foreach (int nIdChamp in idsChamps)
                    SetChampsLus(eltAChamp, nIdChamp);
        }

        //-------------------------------------------------------------------------
        public static void CContexteDonnee_OnInvalideCacheRelation(DataRow row, DataRelation relation)
        {
            DataTable tableParente = relation.ParentTable;
            if (tableParente != null && tableParente.Columns.Contains(c_champIdsChampsLus))
            {
                Type tp = CContexteDonnee.GetTypeForTable(relation.ChildTable.TableName);
                if (tp != null && typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
                {
                    //DataRow[] rows = tableParente.Select(c_champIdsChampsLus + " is not null");
                    //if (rows != null)
                    //    foreach (DataRow row in rows)
                    CContexteDonnee.ChangeRowSansDetectionModification(row, c_champIdsChampsLus, DBNull.Value);
                }
            }

        }

        public static CFormulaire[] GetFormulaires(IElementAChamps elt)
        {
            List<CFormulaire> lst = new List<CFormulaire>();

            if (elt != null)
            {
                foreach (IDefinisseurChampCustom definisseur in elt.DefinisseursDeChamps)
                {
                    if (definisseur != null)
                    {
                        foreach (IRelationDefinisseurChamp_Formulaire relF in definisseur.RelationsFormulaires)
                            if (!lst.Contains(relF.Formulaire))
                                lst.Add(relF.Formulaire);
                    }
                }
                CRoleChampCustom role = elt.RoleChampCustomAssocie;
                if (role != null)
                {
                    CListeObjetsDonnees lstAlways = new CListeObjetsDonnees(CContexteDonneeSysteme.GetInstance(), typeof(CFormulaire));
                    CFiltreData filtre = CFiltreData.GetAndFiltre(
                        CFormulaire.GetFiltreFormulairesForRole(role.CodeRole),
                        new CFiltreData(CFormulaire.c_champPartout + "=@1",
                            true));
                    lstAlways.Filtre = filtre;
                    /*
                    lstAlways.Filtre = new CFiltreData(
                        CFormulaire.c_champCodeRole + "=@1 and " +
                        CFormulaire.c_champPartout + "=@2",
                        role.CodeRole,
                        true);*/
                    foreach (CFormulaire formulaire in lstAlways)
                    {
                        if (!lst.Contains(formulaire))
                            lst.Add(formulaire);
                    }
                }
                
            }
            return lst.ToArray();
        }

    }
}
