using System;
using sc2i.common;
using System.Data;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Contient une liste d'objet données issus d'une liaison directe entre un
	/// objet et ses fils
	/// </summary>
	public class CListeObjetsDonneesContenus : CListeObjetsDonnees
	{
		private CObjetDonnee m_objetConteneur = null;
		private string m_strNomTableFille = "";
		private string[] m_strChampsFille = new string[0];
		////////////////////////////////////////////////////////////
		public CListeObjetsDonneesContenus ( CContexteDonnee ctx )
			:base ( ctx )
		{
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonneesContenus ( CObjetDonnee objetParent, string strNomTableFille, string strChampsFille )
			:base(objetParent.ContexteDonnee, CContexteDonnee.GetTypeForTable(strNomTableFille))
		{
			Init ( objetParent, strNomTableFille, new String[]{strChampsFille}, true );
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonneesContenus ( 
			CObjetDonnee objetParent, 
			string strNomTableFille, 
			string strChampsFille, 
			bool bAppliquerFiltreParDefaut )
			:base(objetParent.ContexteDonnee, CContexteDonnee.GetTypeForTable(strNomTableFille))
		{
			Init ( objetParent, strNomTableFille, new String[]{strChampsFille}, bAppliquerFiltreParDefaut );
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonneesContenus ( CObjetDonnee objetParent, string strNomTableFille, string[] strChampsFille )
			:base(objetParent.ContexteDonnee, CContexteDonnee.GetTypeForTable(strNomTableFille))
		{
			Init ( objetParent, strNomTableFille, strChampsFille, true );
		}

		////////////////////////////////////////////////////////////
		public string[] ChampsFille
		{
			get
			{
				return m_strChampsFille;
			}
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonneesContenus ( 
			CObjetDonnee objetParent, 
			string strNomTableFille, 
			string[] strChampsFille, 
			bool bAppliquerFiltreParDefaut,
			bool bModeProgressif )
			:base(objetParent.ContexteDonnee, CContexteDonnee.GetTypeForTable(strNomTableFille))
		{
			RemplissageProgressif = bModeProgressif;
			Init ( objetParent, strNomTableFille, strChampsFille, bAppliquerFiltreParDefaut );
		}

		////////////////////////////////////////////////////////////
		private void Init ( 
			CObjetDonnee objetParent, 
			string strNomTableFille, 
			string[] strChampsFille, 
			bool bAppliquerFiltreParDefaut)
		{
			m_strNomTableFille = strNomTableFille;
			m_strChampsFille = strChampsFille;
			m_objetConteneur = objetParent;
			if ( !RemplissageProgressif )
				objetParent.AssureDependances( strNomTableFille, strChampsFille );
            /*Stef 26042012 : suppression de cette optimisation,
             * le problème est que si on ajoute des fils, alors, le filtre ne les voit plus !
             DataTable tableFille = ContexteDonnee.GetTableSafe(strNomTableFille);
            if (!RemplissageProgressif && tableFille != null && tableFille.PrimaryKey.Length == 1 &&
                tableFille.PrimaryKey[0].DataType == typeof(int))
            {
                string strFK = ContexteDonnee.GetForeignKeyName(objetParent.GetNomTable(), strNomTableFille, strChampsFille);
                DataRow[] rows = objetParent.Row.Row.GetChildRows(strFK);
                if (rows.Length == 0)
                    m_filtrePrincipal = new CFiltreDataImpossible();
                else
                {
                    DataColumn colKey = tableFille.PrimaryKey[0];
                    StringBuilder bl = new StringBuilder();
                    foreach (DataRow row in rows)
                    {
                        bl.Append(row[colKey]);
                        bl.Append(',');
                    }
                    bl.Remove(bl.Length - 1, 1);
                    m_filtrePrincipal = new CFiltreData(colKey.ColumnName + " in (" + bl.ToString() + ")");
                }
            }
            else*/
			    m_filtrePrincipal = CFiltreData.CreateFiltreAndSurValeurs(strChampsFille, objetParent.GetValeursCles());
			
			m_bAppliquerFiltreParDefaut = bAppliquerFiltreParDefaut;
#if PDA
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(CContexteDonnee.GetTypeForTable(strNomTableFille));
			objet.ContexteDonnee = m_objetConteneur.ContexteDonnee;
#else
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(CContexteDonnee.GetTypeForTable(strNomTableFille), new object[]{m_objetConteneur.ContexteDonnee});
#endif
			CFiltreData filtre = bAppliquerFiltreParDefaut?objet.FiltreStandard:null;
			if ( filtre != null )
				m_filtrePrincipal = CFiltreData.GetAndFiltre ( m_filtrePrincipal, filtre );
			
			if ( !RemplissageProgressif )
			{
				//Puisque les objets sont lus, il n'y a aucune raison d'aller relire cette liste depuis
				//La base de données
				InterditLectureInDB = true;
				m_bIsToRead = false;
			}
		}

		////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		////////////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion ( ref nVersion );
			if ( result )
				result = base.Serialize( serializer );
			if ( !result )
				return result;
			
			bool bHasConteneur;
			serializer.TraiteString ( ref m_strNomTableFille );
			int nNbChampsFille = m_strChampsFille.Length;
			serializer.TraiteInt ( ref nNbChampsFille );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( string strChamp in m_strChampsFille )
					{
						string strTmp = strChamp;
						serializer.TraiteString ( ref strTmp );
					}
					break;
				case ModeSerialisation.Lecture :
					m_strChampsFille = new string[nNbChampsFille];
					for ( int nChamp = 0; nChamp < nNbChampsFille; nChamp++ )
					{
						string strTmp = "";
						serializer.TraiteString ( ref strTmp );
						m_strChampsFille[nChamp] = strTmp;
					}
					break;
			}

			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					bHasConteneur = m_objetConteneur != null;
					serializer.TraiteBool ( ref bHasConteneur );
					if ( bHasConteneur )
					{
						Type tp = m_objetConteneur.GetType();
						serializer.TraiteType(ref tp);
						object[] lstValeurs = m_objetConteneur.GetValeursCles();
						int nNbValeurs = lstValeurs.Length;
						serializer.TraiteInt ( ref nNbValeurs ); 
						foreach ( object obj in lstValeurs )
						{
							object obj_tmp = obj;
							serializer.TraiteObjetSimple ( ref obj_tmp );
						}
					}
					break;
				case ModeSerialisation.Lecture :
					bHasConteneur = false;
					serializer.TraiteBool ( ref bHasConteneur );
					if ( !bHasConteneur )
						m_objetConteneur = null;
					else
					{
						Type tp = null;
						serializer.TraiteType ( ref tp );
#if PDA
						m_objetConteneur = (CObjetDonnee)Activator.CreateInstance ( tp );
						m_objetConteneur.ContexteDonnee = m_contexte;
#else
						m_objetConteneur = (CObjetDonnee)Activator.CreateInstance(tp, new object[]{m_contexte});
#endif
						int nNbCles = 0;
						serializer.TraiteInt ( ref nNbCles );
						object[] lst = new object[nNbCles];
						for ( int nCle = 0; nCle < nNbCles; nCle++ )
						{
							serializer.TraiteObjetSimple(ref lst[nCle]);
						}
						m_objetConteneur.PointeSurLigne ( lst );
					}
					break;
			}

			if ( serializer.Mode == ModeSerialisation.Lecture )
				Init ( m_objetConteneur, m_strNomTableFille, m_strChampsFille, m_bAppliquerFiltreParDefaut );

			return result;
		}


		/*////////////////////////////////////////////////////////////
		protected override CResultAErreur WriteMyPersistantData (  )
		{
			CResultAErreur result = base.WriteMyPersistantData();
			Serialize(m_objetConteneur != null );
			if ( m_objetConteneur != null )
			{
				Serialize(m_objetConteneur.GetType());
				Serialize(m_objetConteneur.GetValeursCles());
			}
			return CResultAErreur.True;
		}

		////////////////////////////////////////////////////////////
		protected override CResultAErreur ReadMyPersistantData( int nVersion )
		{
			CResultAErreur result = base.ReadMyPersistantData( nVersion );
			if ( (bool)DeSerialize() )
			{
				Type tp = (Type)DeSerialize();
				m_objetConteneur = (CObjetDonnee)Activator.CreateInstance(tp, new object[]{m_contexte});
				object[] cles = (object[])DeSerialize();
				m_objetConteneur.PointeSurLigne(cles);
			}
			else
				m_objetConteneur = null;
			return CResultAErreur.True;
		}*/

		////////////////////////////////////////////////////////////
		public CObjetDonnee ObjetConteneur
		{
			get
			{
				return m_objetConteneur;
			}
		}
	}
}
