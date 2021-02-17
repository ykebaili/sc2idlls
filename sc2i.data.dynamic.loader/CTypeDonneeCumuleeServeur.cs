using System;
using System.Collections;
using System.Data;

using sc2i.data.dynamic;
using sc2i.data.serveur;
using sc2i.multitiers.client;
using sc2i.multitiers.server;


using sc2i.common;
using sc2i.expression;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CTypeDonneeCumuleeServeur : CObjetServeurAvecBlob, IObjetServeur,ITypeDonneeCumuleeServeur
	{
#if PDA
		///////////////////////////////////////////////////
		public CTypeDonneeCumuleeServeur (  )
			:base (  )
		{
		}
#endif
		
		///////////////////////////////////////////////////
		public CTypeDonneeCumuleeServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CTypeDonneeCumulee.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CTypeDonneeCumulee type = (CTypeDonneeCumulee)objet;
			
				if (type.Libelle == "")
					result.EmpileErreur(I.T("The value label cannot be empty|141"));
				if (!CObjetDonneeAIdNumerique.IsUnique(type, CTypeDonneeCumulee.c_champCode, type.Code))
					result.EmpileErreur(I.T("A data type with this code already exist|142"));
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		///////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CTypeDonneeCumulee);
		}

		///////////////////////////////////////////////////
		private static Hashtable m_tableCalculsEnCours = new Hashtable();
		/// <summary>
		/// Stocke le résultat d'une requête pour un type de données
		/// </summary>
		/// <param name="nIdTypeDonnee"></param>
		/// <param name="requete"></param>
		/// <returns></returns>
		public CResultAErreur StockeResultat ( int nIdTypeDonnee, IIndicateurProgression indicateur )
		{
			using ( C2iSponsor sponsor = new C2iSponsor() )
			{
				sponsor.Register ( indicateur );
				CResultAErreur result = CResultAErreur.True;
				if ( m_tableCalculsEnCours[nIdTypeDonnee] != null )
				{
					result.EmpileErreur(I.T("The recalculation for this cumulated data type is already in progress|143"));
					return result;
				}
				m_tableCalculsEnCours[nIdTypeDonnee] = true;
				CSessionProcessServeurSuivi sessionSuivi = null;
				try
				{
					//Crée une session pour le calcul
					CSessionClient session = CSessionClient.GetSessionForIdSession ( IdSession );
					if(  session == null )
					{
						result.EmpileErreur(I.T("Session error|144"));
						return result;
					}
			
					System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
					sessionSuivi = new CSessionProcessServeurSuivi();
					result = sessionSuivi.OpenSession(new CAuthentificationSessionServer(),
						I.T("Recalculation of datas @1|145",nIdTypeDonnee.ToString()),
						session );
					if ( !result )
					{
						result.EmpileErreur(I.T("Opening session error|146"));
						return result;
					}
					IdSession = sessionSuivi.IdSession;
					System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
					using ( CContexteDonnee contexte = new CContexteDonnee ( IdSession, true, false ) )
					{
						//Récupère le type de donnée
						CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee( contexte );
						if ( !typeDonnee.ReadIfExists ( nIdTypeDonnee ) )
						{
							result.EmpileErreur(I.T("The cumulated data type @1 doesn't exist|147",nIdTypeDonnee.ToString()));
							return result;
						}

						if ( indicateur != null )
						{
							indicateur.SetInfo(I.T("Existing datas recovery|148"));
							indicateur.SetValue ( 0 );
						}

						CParametreDonneeCumulee parametre = typeDonnee.Parametre;

						//Lit les données pour ce type de donnée cumulée
						CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte,typeof(CDonneeCumulee));
						liste.Filtre = new CFiltreData ( CTypeDonneeCumulee.c_champId+"=@1",
							nIdTypeDonnee );
						liste.AssureLectureFaite();
						if ( parametre.ViderAvantChaqueCalcul )
						{
							result = VideTable ( nIdTypeDonnee, contexte.Tables[CDonneeCumulee.c_nomTable] );
							if ( !result )
								return result;
						}

					
						//Liste des champs clé à lire
						ArrayList lstClesALire = new ArrayList();
						//Liste des champs destination des clés
						ArrayList lstDestClesALire = new ArrayList();
					
						//Liste des valeurs décimales à lire
						ArrayList lstValeursDecimalesALire = new ArrayList();
						//Liste des champs destinatation des valeurs décimales
						ArrayList lstDestValeursDecimalesALire = new ArrayList();

                        //Liste des valeurs dates à lire
                        ArrayList lstValeursDatesALire = new ArrayList();
                        //Liste des champs destinatation des valeurs dates
                        ArrayList lstDestValeursDatesALire = new ArrayList();

                        //Liste des valeurs texte à lire
                        ArrayList lstValeursTextesALire = new ArrayList();
                        //Liste des champs destinatation des valeurs texte
                        ArrayList lstDestValeursTextesALire = new ArrayList();
						
						//Change les clés de la table de données pour qu'elles 
						//correspondent aux clés déclarées
						//Pour des recherches ultérieures plus rapides
						DataTable tableDonnees = contexte.Tables[CDonneeCumulee.c_nomTable];
						DataColumn[] oldKey = tableDonnees.PrimaryKey;
						ArrayList lstCles = new ArrayList();
					
						for ( int nCle = 0; nCle < CParametreDonneeCumulee.c_nbChampsCle; nCle++ )
						{
							if ( parametre.GetChampCle ( nCle ) != null && 
								parametre.GetChampCle( nCle ).Champ!= "" )
							{
								lstClesALire.Add ( parametre.GetChampCle ( nCle ).Champ );
								string strChampDest = CDonneeCumulee.c_baseChampCle+nCle.ToString();
								lstDestClesALire.Add ( strChampDest );
								lstCles.Add ( tableDonnees.Columns[strChampDest] );
							}
						}
						try
						{
							tableDonnees.PrimaryKey = (DataColumn[])lstCles.ToArray(typeof(DataColumn));
						}
						catch
						{
							//On n'y arrive pas, on a probablement changé la requete ->On vide tout !
							result = VideTable ( nIdTypeDonnee, tableDonnees );
							if(  !result )
								return result;
							tableDonnees.PrimaryKey = (DataColumn[])lstCles.ToArray(typeof(DataColumn));
						}

						//Repère les données à lire
						for ( int nChamp = 0; nChamp < CParametreDonneeCumulee.c_nbChampsValeur; nChamp++ )
						{
							string strChamp = parametre.GetValueField ( nChamp );
							if ( strChamp != null && strChamp != "" )
							{
								lstValeursDecimalesALire.Add ( strChamp );
								lstDestValeursDecimalesALire.Add ( CDonneeCumulee.c_baseChampValeur+nChamp.ToString() );
							}
						}

                        for (int nChamp = 0; nChamp < CParametreDonneeCumulee.c_nbChampsDate; nChamp++)
                        {
                            string strChamp = parametre.GetDateField(nChamp);
                            if (strChamp != null && strChamp != "")
                            {
                                lstValeursDatesALire.Add(strChamp);
                                lstDestValeursDatesALire.Add(CDonneeCumulee.c_baseChampDate + nChamp.ToString());
                            }
                        }

                        for (int nChamp = 0; nChamp < CParametreDonneeCumulee.c_nbChampsTexte; nChamp++)
                        {
                            string strChamp = parametre.GetTextField(nChamp);
                            if (strChamp != null && strChamp != "")
                            {
                                lstValeursTextesALire.Add(strChamp);
                                lstDestValeursTextesALire.Add(CDonneeCumulee.c_baseChampTexte + nChamp.ToString());
                            }
                        }

						if ( indicateur != null )
						{
							indicateur.SetInfo (I.T("Request execution|149") );
							indicateur.SetValue ( 10 );
						}

						IDefinitionJeuDonnees defJeu = typeDonnee.Parametre.DefinitionDeDonnees;
						IElementAVariablesDynamiquesAvecContexteDonnee eltAVariables = null;
						if (defJeu is C2iRequete)
							eltAVariables = (IElementAVariablesDynamiquesAvecContexteDonnee)defJeu;
						else if (defJeu is CStructureExportAvecFiltre)
						{
							eltAVariables = ((CStructureExportAvecFiltre)defJeu).Filtre;
							if (eltAVariables == null)
							{
								CFiltreDynamique filtre = new CFiltreDynamique(contexte);
								filtre.TypeElements = defJeu.TypeDonneesEntree;
								eltAVariables = filtre;
							}
							((CFiltreDynamique)eltAVariables).ContexteDonnee = contexte;
						}

						result = CParametreDonneeCumulee.GetTableSource(eltAVariables, defJeu, indicateur);
						//Calcule le résultat de la requête demandée
						if ( !result )
						{
							result.EmpileErreur (I.T("Error in request|150"));
							return result;
						}

                        //Liste des lignes trouvées dans la requête
						Hashtable tableExistantes = new Hashtable();

						DataTable tableSource = (DataTable)result.Data;

						if ( indicateur != null )
						{
							indicateur.SetInfo (I.T("Result Storage|151"));
							indicateur.SetValue ( 20 );
							indicateur.PushSegment ( 20, 80 );
							indicateur.SetBornesSegment ( 0, tableSource.Rows.Count );
						}

						int nIndex = 0;
					
						foreach ( DataRow row in tableSource.Rows )
						{
							nIndex++;
							if ( indicateur != null && nIndex % 50 == 0 )
								indicateur.SetValue ( nIndex );
							lstCles.Clear(); 
							for ( int nCle = 0; nCle < lstClesALire.Count; nCle++ )
								lstCles.Add ( row[(string)lstClesALire[nCle]] );
							DataRow rowDest = tableDonnees.Rows.Find((object[])lstCles.ToArray(typeof(object)));
                            if (rowDest == null)
                            {
                                rowDest = tableDonnees.NewRow();
                                rowDest[CObjetDonnee.c_champIdUniversel] = CUniqueIdentifier.GetNew();
                                for (int nCle = 0; nCle < lstCles.Count; nCle++)
                                {
                                    rowDest[(string)lstDestClesALire[nCle]] = lstCles[nCle];
                                }
                                rowDest[CTypeDonneeCumulee.c_champId] = nIdTypeDonnee;
                                tableDonnees.Rows.Add(rowDest);
                            }
                            // Rempli les valeurs décimales de destination
                            for (int nChampVal = 0; nChampVal < lstValeursDecimalesALire.Count; nChampVal++)
                            {
                                try
                                {
                                    rowDest[(string)lstDestValeursDecimalesALire[nChampVal]] = Convert.ToDouble(row[(string)lstValeursDecimalesALire[nChampVal]]);
                                }
                                catch
                                {
                                    rowDest[CDonneeCumulee.c_baseChampValeur + nChampVal.ToString()] = 0;
                                }
                            }
                            // Rempli les valeurs Dates de destination
                            for (int nChampVal = 0; nChampVal < lstValeursDatesALire.Count; nChampVal++)
                            {
                                try
                                {
                                    rowDest[(string)lstDestValeursDatesALire[nChampVal]] = Convert.ToDateTime(row[(string)lstValeursDatesALire[nChampVal]]);
                                }
                                catch
                                {
                                    rowDest[CDonneeCumulee.c_baseChampDate + nChampVal.ToString()] = DBNull.Value;
                                }
                            }
                            // Rempli les valeurs Texte de destination
                            for (int nChampVal = 0; nChampVal < lstValeursTextesALire.Count; nChampVal++)
                            {
                                try
                                {
                                    rowDest[(string)lstDestValeursTextesALire[nChampVal]] = (row[(string)lstValeursTextesALire[nChampVal]]).ToString();
                                }
                                catch
                                {
                                    rowDest[CDonneeCumulee.c_baseChampTexte + nChampVal.ToString()] = "";
                                }
                            }

							tableExistantes[rowDest] = true;
						}
						//Remet la clé à la valeur initiale
						tableDonnees.PrimaryKey = oldKey;

						if ( indicateur != null )
						{
							indicateur.PopSegment();
							indicateur.PushSegment ( 80, 90 );
							indicateur.SetBornesSegment ( 0, tableDonnees.Rows.Count );
							indicateur.SetInfo (I.T("Deleting of old values|152"));
						}

						if ( !parametre.PasDeSuppression )
						{
							//Supprime les lignes à supprimer
							ArrayList lstRows = new ArrayList ( tableDonnees.Rows );
							nIndex = 0;
							foreach ( DataRow row in lstRows )
							{
								if ( !tableExistantes.Contains(row) )
									row.Delete();
								nIndex++;
								if ( indicateur != null && nIndex % 50 == 0 )
									indicateur.SetValue ( nIndex );
						
							}
						}
						if ( indicateur != null )
						{
							indicateur.PopSegment();
							indicateur.SetInfo(I.T("Datas saving|153"));
						}
						contexte.EnableTraitementsAvantSauvegarde = false;
						result = contexte.SaveAll( true );
						if ( indicateur != null )
							indicateur.SetInfo(I.T("Finished calculation|154"));
					}
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
					return result;
				}
				finally
				{
					if ( sessionSuivi != null )
						sessionSuivi.CloseSession();
					m_tableCalculsEnCours.Remove ( nIdTypeDonnee );
				}
				return result;
			}
		}

		private CResultAErreur VideTable(int nIdTypeDonnee, DataTable table)
		{
			ArrayList lstRows = new ArrayList(table.Rows);
            CDonneeNotificationModificationContexteDonnee donnee = new CDonneeNotificationModificationContexteDonnee(IdSession);
            foreach (DataRow row in lstRows)
            {
                donnee.AddModifiedRecord(table.TableName, true, new object[] { row[table.PrimaryKey[0]] });
            }
			CResultAErreur result = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, "").RunStatement(
				"delete from "+
				CDonneeCumulee.c_nomTable+" where "+
				CTypeDonneeCumulee.c_champId+"="+nIdTypeDonnee.ToString() );
			if ( !result )
				return result;
			table.Rows.Clear();
            CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[]{donnee});
			return result;
		}
	}
}
