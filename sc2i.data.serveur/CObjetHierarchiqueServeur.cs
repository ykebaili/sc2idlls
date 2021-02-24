using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.data.dynamic;
using System.Text;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CUtilisateurServeur.
	/// </summary>
	public abstract class CObjetHierarchiqueServeur : CObjetDonneeServeurAvecCache
	{
		//-------------------------------------------------------------------
#if PDA
		public CUtilisateurServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CObjetHierarchiqueServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = base.TraitementAvantSauvegarde (contexte);
			if ( !result )
				return result;

			result = SObjetHierarchiqueServeur.TraitementAvantSauvegarde(contexte, GetNomTable());

			
			return result;
		}

        
	}

	public static class SObjetHierarchiqueServeur
	{

        //-------------------------------------------------------------------
        private class CRowSorterSurParent : IComparer
        {
            private string m_strColonne = "";
            public CRowSorterSurParent(string strColonne)
            {
                m_strColonne = strColonne;
            }

            public int Compare(object x, object y)
            {
                try
                {
                    object val1, val2;
                    if (((DataRow)x).RowState == DataRowState.Deleted ||
                        ((DataRow)y).RowState == DataRowState.Deleted)
                        return 0;

                    val1 = ((DataRow)x)[m_strColonne];
                    val2 = ((DataRow)y)[m_strColonne];

                    if (val1 is IComparable && val2 is IComparable)
                        return ((IComparable)val1).CompareTo((IComparable)val2);
                    if (val1.Equals(val2))
                        return 0;
                    return -1;
                }
                catch (Exception ex)
                {
                    object val1, val2;
                    val1 = ((DataRow)x)[m_strColonne];
                    val2 = ((DataRow)y)[m_strColonne];

                    string strInfosDebug = "DEBUG_OOREDOO" + Environment.NewLine;
                    strInfosDebug += "Error in Compare function" + Environment.NewLine;
                    strInfosDebug += "Val1 = " + val1.ToString() + Environment.NewLine;
                    strInfosDebug += "Val2 = " + val2.ToString() + Environment.NewLine;
                    C2iEventLog.WriteInfo(strInfosDebug);
                }
                return 0;
            }
        }

		//-------------------------------------------------------------------
		public static CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte, string strNomTable)
		{
            lock (typeof(CObjetHierarchiqueServeur))//Empeche que plusieurs thread entrent en même temps
            {
                CResultAErreur result = CResultAErreur.True;
                if (!result)
                    return result;

                DataTable table = contexte.Tables[strNomTable];

                ArrayList lstRows = new ArrayList(table.Rows);

                IObjetHierarchiqueACodeHierarchique objet = (IObjetHierarchiqueACodeHierarchique)Activator.CreateInstance(CContexteDonnee.GetTypeForTable(strNomTable), new object[] { contexte });

                string strInfosDebug = "";
                /*/*** START DEBUG :  ajout d'une trace pour analyser le bug chez OOREDOO
                strInfosDebug = "DEBUG_TIMOS" + Environment.NewLine;
                strInfosDebug += "Nom de la table : " + strNomTable + Environment.NewLine;
                strInfosDebug += "Contenu des rows : " + Environment.NewLine;
                foreach (DataColumn col in table.Columns)
                {
                    strInfosDebug += col.ColumnName + ";";
                }
                strInfosDebug += Environment.NewLine;

                foreach (DataRow row in lstRows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        strInfosDebug += item.ToString() + ";";
                    }
                    strInfosDebug += Environment.NewLine;
                }
                C2iEventLog.WriteInfo(strInfosDebug);
                /*** END DEBUG ***/

                try
                {
                    //C2iEventLog.WriteInfo("DEBUG_TIMOS - TraitementAvantSauvegarde() objet hiérarchique - before Sort");
                    lstRows.Sort(new CRowSorterSurParent(objet.ChampIdParent));
                    //C2iEventLog.WriteInfo("DEBUG_TIMOS - TraitementAvantSauvegarde() objet hiérarchique - after Sort");
                }
                catch (Exception ex)
                {
                    //*** START DEBUG 02/11/2020 :  ajout d'une trace pour analyser le bug chez OOREDOO
                    // Cause problème de tri sur table objets hiérarchiques sur champ idParent
                    strInfosDebug = "DEBUG_TIMOS" + Environment.NewLine;
                    strInfosDebug += "ERROR MESSAGE : " + ex.Message + Environment.NewLine;
                    C2iEventLog.WriteErreur(strInfosDebug);
                    /*** END DEBUG ***/
                }

                object lastParent = DBNull.Value;

                int nLastCode = 0;
                foreach (DataRow row in lstRows)
                {
                    //Allocation du code famille
                    objet = (IObjetHierarchiqueACodeHierarchique)Activator.CreateInstance(CContexteDonnee.GetTypeForTable(strNomTable), new object[] { row });
                    if (objet.Row.RowState != DataRowState.Deleted && (objet.CodeSystemePartiel == objet.CodePartielDefaut || HasChange(objet, objet.ChampIdParent)))
                    {
                        if (!objet.Row[objet.ChampIdParent].Equals(lastParent))
                        {
                            nLastCode = 0;
                            lastParent = objet.Row[objet.ChampIdParent];
                        }
                        AlloueCode(objet, ref nLastCode);
                    }
                }
                return result;
            }
			
		}

		//-------------------------------------------------------------------
        private static bool HasChange(IObjetHierarchiqueACodeHierarchique objet, string strColonne)
		{
			DataRow row = objet.Row;
			if (row.RowState == DataRowState.Deleted)
				return false;
			if (row.RowState == DataRowState.Modified)
			{
				object val = row[strColonne, DataRowVersion.Original];
				object valNew = row[strColonne];
				return !valNew.Equals(val);
			}
			if (row.RowState == DataRowState.Added)
				return true;
			return false;
		}

		//-------------------------------------------------------------------
		private static string GetCle(int nValeur, int nNbCars)
		{
			string strCaracteresCode = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string strCle = "";
			while (nNbCars > 0)
			{
				int nLow = nValeur % strCaracteresCode.Length;
				nValeur = (int)(nValeur - nLow) / strCaracteresCode.Length;
				strCle = strCaracteresCode[nLow] + strCle;
				nNbCars--;
			}
			return strCle;
		}

        private static Dictionary<Type, int> m_dicLastCodeForType = new Dictionary<Type, int>();
		//-------------------------------------------------------------------
        private static void AlloueCode(IObjetHierarchiqueACodeHierarchique objet, ref int nLastCodeAlloue)
		{
            if (nLastCodeAlloue == 0 && m_dicLastCodeForType.ContainsKey(objet.GetType()))
                nLastCodeAlloue = m_dicLastCodeForType[objet.GetType()];
            string strCle = "";
            if ( nLastCodeAlloue > 0 && objet.ObjetParent == null )
            {
                //Teste la valeur suivante
                int nTmp = nLastCodeAlloue + 1;
                strCle = GetCle(nTmp, objet.NbCarsParNiveau);
                CListeObjetsDonnees lstExt = new CListeObjetsDonnees(objet.ContexteDonnee, 
                    objet.GetType(), 
                    new CFiltreData (objet.ChampIdParent +" is null and "+
                        objet.ChampCodeSystemePartiel+"=@1", strCle ));
                if(  lstExt.CountNoLoad == 0 )
                {
                    nLastCodeAlloue = nTmp;
			        objet.ChangeCodePartiel(strCle);
                    m_dicLastCodeForType[objet.GetType()] = nLastCodeAlloue;
                    return;
                }
            }
			CFiltreData filtre = null;
			if (objet.ObjetParent != null)
				filtre = new CFiltreData(objet.ChampIdParent + "=@1",
					objet.ObjetParent.Id);
			else
				filtre = new CFiltreData(objet.ChampIdParent + " is null and "+
					objet.ChampCodeSystemePartiel+">@1", GetCle ( nLastCodeAlloue, objet.NbCarsParNiveau) );

			CListeObjetsDonnees listeSoeurs = new CListeObjetsDonnees(objet.ContexteDonnee, objet.GetType(), filtre);
            listeSoeurs.PreserveChanges = true;
			listeSoeurs.AssureLectureFaite();

			listeSoeurs.InterditLectureInDB = true;

			Hashtable tableCodesUtilises = new Hashtable();
			foreach (IObjetHierarchiqueACodeHierarchique obj in listeSoeurs)
				tableCodesUtilises[obj.CodeSystemePartiel] = true;

			//Cherche le prochain numéro libre 
			int nCpt = nLastCodeAlloue;
			strCle = "";
			do
			{
				nCpt++;
				strCle = GetCle(nCpt, objet.NbCarsParNiveau);
				/*listeSoeurs.Filtre = new CFiltreData(objet.ChampCodeSystemePartiel + "=@1",
					strCle);*/
			}
			while (tableCodesUtilises.ContainsKey ( strCle ));
			nLastCodeAlloue = nCpt;
			objet.ChangeCodePartiel(strCle);
            m_dicLastCodeForType[objet.GetType()] = nLastCodeAlloue;
		}

        
	}
}
