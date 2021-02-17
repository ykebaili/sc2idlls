using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using sc2i.expression;
using System.Collections;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
    [AutoExec("Autoexec")]
    public class CReaderDependancesListeObjetsDonneesPropriete : IDependanceListeObjetsDonneesReader
    {
        public CReaderDependancesListeObjetsDonneesPropriete()
        {
        }

        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader(CDefinitionProprieteDynamiqueDotNet.c_strCleTypeDefinition,
                typeof(CReaderDependancesListeObjetsDonneesPropriete));
        }


        public void ReadArbre(
            CListeObjetsDonnees listeSource, 
            CListeObjetsDonnees.CArbreProps arbre, 
            List<string> lstPaquetsALire)
        {
            string strCle = "";
            string strPropriete = "";
            if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(arbre.ProprietePrincipale, ref strCle, ref strPropriete))
                strPropriete = arbre.ProprietePrincipale;//Mode sans DefinitionProprieteDynamique
            Type typeObjets = listeSource.TypeObjets;
            Hashtable sousArbresToRemove = new Hashtable();
            PropertyInfo info = typeObjets.GetProperty(strPropriete);
            if (info != null)
            {
                object[] attribs = info.GetCustomAttributes(typeof(OptimiseReadDependanceAttribute), true);
                if (attribs.Length != 0)
                {
                    string strProp = ((OptimiseReadDependanceAttribute)attribs[0]).ProprieteALire;
                    if (strProp.IndexOf('.') > 0)
                    {
                        string strSuite = strProp.Substring(strProp.IndexOf('.') + 1);
                        strProp = strProp.Substring(0, strProp.IndexOf('.'));
                        arbre.GetArbreSousProp(strSuite, true);
                    }
                    strPropriete = strProp;
                }

            }
            CStructureTable structure = CStructureTable.GetStructure(typeObjets);
            //Cherche la relation liée à la propriete
            bool bRelationTrouvee = false;
            foreach (CInfoRelation relation in structure.RelationsFilles)
            {
                if (relation.Propriete == strPropriete)
                {
                    ReadDependanceFille(
                        strPropriete,
                        listeSource,
                        arbre, 
                        relation, 
                        lstPaquetsALire);
                    lstPaquetsALire = null;
                    bRelationTrouvee = true; break;
                }
            }
            if (!bRelationTrouvee)
            {
                foreach (CInfoRelation relation in structure.RelationsParentes)
                {
                    if (relation.Propriete == strPropriete)
                    {
                        ReadDependanceParente(strPropriete, listeSource, arbre, relation);
                        bRelationTrouvee = true;
                    }
                }
            }
            if (!bRelationTrouvee)
                sousArbresToRemove[arbre] = true;
            foreach (CListeObjetsDonnees.CArbreProps arbreTmp in sousArbresToRemove.Keys)
                arbre.SousArbres.Remove(arbreTmp);
        }


		/////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Lit les dépendances filles du type de donné
		/// </summary>
		/// <returns></returns>
		private void ReadDependanceFille ( 
            string strPropriete,
            CListeObjetsDonnees lstSource, 
            CListeObjetsDonnees.CArbreProps arbre , 
            CInfoRelation relation, 
            List<string> listePaquets)
		{
			CResultAErreur result = CResultAErreur.True;
			Type tpFille = null;
			PropertyInfo info = lstSource.TypeObjets.GetProperty(strPropriete);
			if ( info == null )
			{
                return;
			}
			
			object[] attrs= info.GetCustomAttributes(typeof(RelationFilleAttribute), true);
			if (attrs == null || attrs.Length < 0 )
			{
                return;
			}
			tpFille = ( (RelationFilleAttribute)attrs[0]).TypeFille;

            DataTable table = lstSource.ContexteDonnee.GetTableSafe(lstSource.NomTable);
            //S'assure que la table fille est chargée
            lstSource.ContexteDonnee.GetTableSafe ( relation.TableFille );
            string strKey = relation.RelationKey;
            DataColumn colDependance = table.Columns[strKey];


			if ( listePaquets == null )
			{
                listePaquets = lstSource.GetPaquetsPourLectureFils(relation.ChampsParent[0], colDependance);
			}
			int nNbPaquets = listePaquets.Count;
						
			//Lit les relations par paquet
			for ( int nPaquet = 0; nPaquet < nNbPaquets; nPaquet++ )
			{
				string strPaquet = (string)listePaquets[nPaquet];
				if ( strPaquet.Length > 0 )
				{
					CListeObjetsDonnees listeFille = new CListeObjetsDonnees ( lstSource.ContexteDonnee, tpFille );
					listeFille.Filtre = new CFiltreData ( relation.ChampsFille[0]+" in "+strPaquet );
                    if (arbre.Filtre.Length > 0)
                        listeFille.Filtre.Filtre += " and " + arbre.Filtre;
					listeFille.ModeSansTri = true;
					listeFille.PreserveChanges = true;
					listeFille.AssureLectureFaite();
					int nMax = Math.Min(lstSource.Count, (nPaquet+1)*CListeObjetsDonnees.c_nNbLectureParLotFils);
					
					
					if ( colDependance != null && arbre.Filtre.Length == 0)
					{
						//Indique que les lignes ont été lues
						for ( int nRow = nPaquet*CListeObjetsDonnees.c_nNbLectureParLotFils; nRow < nMax; nRow++ )
						{
							DataRow row = lstSource.View.GetRow(nRow);
							DataRowState oldState = row.RowState;
							row[colDependance] = true;
							if ( oldState == DataRowState.Unchanged )
								row.AcceptChanges();
						}
						
					}
					listeFille.ReadDependances ( arbre );

				}
			}
		}


		

		/////////////////////////////////////////////////////////////////////////////////////////////////
		private void ReadDependanceParente (
            string strPropriete,
            CListeObjetsDonnees lstSource, 
            CListeObjetsDonnees.CArbreProps arbre , 
            CInfoRelation relation) 
		{
			int nNbTotal = lstSource.Count;
			string strKey = relation.RelationKey;
			Type tp = null;
			PropertyInfo info = lstSource.TypeObjets.GetProperty(strPropriete);
			if ( info == null )
			{
                return;
			}
			tp = info.PropertyType;
			if ( tp != null && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp))
			{
				//Crée les paquets
				ArrayList lstPaquets = new ArrayList();
				Hashtable tableIdsParentTraites = new Hashtable();
				int nPaquet = 0;
				string strChampFille = relation.ChampsFille[0];
				string strPaquetEnCours = "";
				int nNbInPaquet = 0;
				for ( int n = 0; n < lstSource.View.Count; n++ )
				{
					DataRow row = lstSource.View.GetRow ( n );
					string strCle = row[strChampFille].ToString();
					if ( strCle != "" )
					{
						if ( tableIdsParentTraites[strCle] == null )
						{
							tableIdsParentTraites[strCle] = true;
							strPaquetEnCours += strCle+",";
							nNbInPaquet++;
							if ( nNbInPaquet >= CListeObjetsDonnees.c_nNbLectureParLotFils )
							{
								strPaquetEnCours = "("+strPaquetEnCours.Substring(0, strPaquetEnCours.Length-1)+")";
								lstPaquets.Add ( strPaquetEnCours );
								strPaquetEnCours = "";
								nNbInPaquet = 0;
							}
						}
					}
				}
				if( strPaquetEnCours.Length > 0 )
				{
					strPaquetEnCours = "("+strPaquetEnCours.Substring(0, strPaquetEnCours.Length-1)+")";
					lstPaquets.Add ( strPaquetEnCours );
				}

				//Lit les relations par paquet
				int nNbPaquets = lstPaquets.Count;
				for ( nPaquet = 0; nPaquet < nNbPaquets; nPaquet++ )
				{
					string strPaquet = (string)lstPaquets[nPaquet];
					if ( strPaquet !="()" )
					{
						CListeObjetsDonnees listeParent = new CListeObjetsDonnees ( lstSource.ContexteDonnee, tp );
						listeParent.ModeSansTri = true;
						listeParent.PreserveChanges = true;
						listeParent.Filtre = new CFiltreData ( relation.ChampsParent[0]+" in "+strPaquet );
						listeParent.AssureLectureFaite();
						//Indique que les lignes ont été lues
                        //Stef 2/12/2011 : non, on n'indique pas que c'est lu puisqu'il
                        //s'agit de dépendances parentes !
                        //Ca pose le problème suivant : si on est sur une table hiérarchique,
                        //lorsqu'on lit le parent (d'un projet par exemple), le fait de
                        //dire que les dépendances sont lues implique qu'on indique
                        //qu'on a lu les fils.
                        //De ma compréhension à ce jour, on ne gère pas de colonne
                        //indiquant si la dépendance a été chargé pour des dépendances
                        //parentes
						/*if ( lstSource.View.Table.Columns[strKey] != null )
						{
							for ( int nRow = 0; nRow < lstSource.View.Count; nRow++ )
							{
								CContexteDonnee.ChangeRowSansDetectionModification ( lstSource.View.GetRow ( nRow ), strKey, true );
							}
						}*/
						listeParent.ReadDependances(arbre);
					}
				}
			}
		}
    }
}
