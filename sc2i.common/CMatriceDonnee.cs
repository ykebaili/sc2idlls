using System;
using System.Collections;
using System.Globalization;

using sc2i.common;

namespace sc2i.common
{
	
	/// <summary>
	/// Méthode de résolution de colonne ou de ligne
	/// si la valeur demandée n'existe pas dans la matrice
	/// </summary>
	public enum MethodeResolutionValeurMatrice
	{
		/// <summary>
		/// Prend la valeur inférieure
		/// </summary>
		Inferieur = 0,
		/// <summary>
		/// Prend la valeur supérieure
		/// </summary>
		Superieur,
		/// <summary>
		/// Prend la valeur la plus proche (si egalité, prend l'inférieur)
		/// </summary>
		ProcheInferieur,
		/// <summary>
		/// Prend la valeur la plus proche (si egalité, prend le supérieur)
		/// </summary>
		ProcheSuperieur,
		/// <summary>
		/// Interpole la valeur
		/// </summary>
		Interpolation,
		/// <summary>
		/// Prend la valeur par défaut
		/// </summary>
		ExactOuValeurDefaut
	}

    public class CMethodeResolutionValeurMatrice : CEnumALibelle<MethodeResolutionValeurMatrice>
    {
        public CMethodeResolutionValeurMatrice(MethodeResolutionValeurMatrice methode)
            :base(methode)
        {
        }

        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case MethodeResolutionValeurMatrice.Inferieur:
                        return I.T("Lesser|10000");
                    case MethodeResolutionValeurMatrice.Superieur:
                        return I.T("Greater|10001");
                    case MethodeResolutionValeurMatrice.ProcheInferieur:
                        return I.T("Nearest or Lesser|10002");
                    case MethodeResolutionValeurMatrice.ProcheSuperieur:
                        return I.T("Nearest or Greater|10003");
                    case MethodeResolutionValeurMatrice.Interpolation:
                        return I.T("Interpolation|10004");
                    case MethodeResolutionValeurMatrice.ExactOuValeurDefaut:
                        return I.T("Exact or default value|10005");
                    default:
                        break;
                }

                return "";
            }
        }
    }



	/// <summary>
	/// Matrice de données (lignes/colonnes)
	/// </summary>
	[Serializable]
	public class CMatriceDonnee : I2iSerializable
	{
		private bool m_bColonnesString;
		private bool m_bLignesString;

		private MethodeResolutionValeurMatrice m_resolutionColonne = MethodeResolutionValeurMatrice.Inferieur;
		private MethodeResolutionValeurMatrice m_resolutionLigne = MethodeResolutionValeurMatrice.Superieur;

		private object[]m_colonnes = new object[0];
		private object[]m_lignes = new object[0];

		//Ligne, colonne
		private double[,] m_valeurs = new double[0,0];

		private double m_dValeurDefaut;

		//---------------------------------------------------
		public CMatriceDonnee()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		//---------------------------------------------------
		/// <summary>
		/// Méthode de résolution des colonnes inexistantes
		/// </summary>
		public MethodeResolutionValeurMatrice MethodeResolutionColonne
		{
			get
			{
				return m_resolutionColonne;
			}
			set
			{
				m_resolutionColonne = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Méthode de résolution des lignes inexistantes
		/// </summary>
		public MethodeResolutionValeurMatrice MethodeResolutionLignes
		{
			get
			{
				return m_resolutionLigne;
			}
			set
			{
				m_resolutionLigne = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Indique si les entête de colonne sont des string ou des doubles
		/// </summary>
		public bool ColonnesString
		{
			get
			{
				return m_bColonnesString;
			}
			set
			{
				m_bColonnesString = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Indique si les entête de ligne sont des string ou des doubles
		/// </summary>
		public bool LignesString
		{
			get
			{
				return m_bLignesString;
			}
			set
			{
				m_bLignesString = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Lignes (entetes) de la matrice<BR></BR>
		/// Les objets doivent être des string ou des doubles.
		/// </summary>
		public object[] Lignes
		{
			get
			{
				return m_lignes;
			}
			set
			{
				foreach ( object val in value )
					if ( !(val is Double) && !(val is string)  )
						throw new Exception(I.T("At least one element is not of the expected type for line headers|30046"));
				m_lignes = value;
			}
		}

		//----------------------------------------------------------
		/// <summary>
		/// Colonnes (entêtes de la matrice<BR></BR>
		/// Les objets doivent être des string ou des doubles.
		/// </summary>
		public object[] Colonnes
		{
			get
			{
				return m_colonnes;
			}
			set
			{
				foreach ( object val in value )
					if ( !(val is Double) && !(val is string)  )
                        throw new Exception(I.T("At least one element is not of the expected type for line headers|30046"));
				m_colonnes = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Valeurs de la matrice
		/// </summary>
		public double[,] Valeurs
		{
			get
			{
				return m_valeurs;
			}
			set
			{
				m_valeurs = value;
			}
		}

		//---------------------------------------------------
		public double ValeurDefaut
		{
			get
			{
				return m_dValeurDefaut;
			}
			set
			{
				m_dValeurDefaut = value;
			}
		}

		//---------------------------------------------------
		/// <summary>
		/// Vérifie qu'il y a le bon nombre de ligne et de colonnes
		/// dans les valeurs, que les colonnes et les lignes sont du bon type./
		/// </summary>
		/// <returns></returns>
		public CResultAErreur VerifieCoherence()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_lignes.Length == 0 )
				result.EmpileErreur(I.T("The array must contain at least a line|30047"));
			if ( m_colonnes.Length == 0)
                result.EmpileErreur(I.T("The array must contain at least a column|30048"));
			if ( !result)
				return result;

			if ( m_valeurs.Length != m_lignes.Length * m_colonnes.Length )
				result.EmpileErreur(I.T("The value array has an incorrect size|30049"));

			Type tpLigne = LignesString?typeof(string):typeof(double);
			Hashtable tableValeurs = new Hashtable();
			foreach ( object obj in m_lignes )
			{
				if ( !obj.GetType().Equals ( tpLigne ) )
				{
					result.EmpileErreur(I.T("At least one line header value is incorrect|30050"));
					break;
				}
				if ( tableValeurs[obj] != null )
				{
                    result.EmpileErreur(I.T("At least one line header value is not unique|30051"));
					break;
				}
				tableValeurs[obj] = true;
			}

			Type tpCol = ColonnesString?typeof(string):typeof(double);
			tableValeurs.Clear();
			foreach ( object obj in m_colonnes )
			{
				if ( !obj.GetType().Equals ( tpCol ) )
				{
                    result.EmpileErreur(I.T("At least one column header value is incorrect|30052"));
					break;
				}
				if ( tableValeurs[obj] != null )
				{
                    result.EmpileErreur(I.T("At least one column header value is not unique|30053"));
					break;
				}
				tableValeurs[obj] = true;
			}

			if ( LignesString && MethodeResolutionLignes != MethodeResolutionValeurMatrice.ExactOuValeurDefaut )
			{
				result.EmpileErreur(I.T("The line resolution method is not compatible with text lines|30054"));
				return result;
			}

			if ( ColonnesString && MethodeResolutionColonne != MethodeResolutionValeurMatrice.ExactOuValeurDefaut )
			{
                result.EmpileErreur(I.T("The column resolution method is not compatible with text columns|30055"));
				return result;
			}

			result = VerifieEnteteAvecMethode ( Lignes, MethodeResolutionLignes );
			if ( !result )
				result.EmpileErreur(I.T("Line header error|30114"));
			result = VerifieEnteteAvecMethode ( Colonnes, MethodeResolutionColonne );
			if (!result)
                result.EmpileErreur(I.T("Column header error|30115"));

			return result;
		}

		//---------------------------------------------------
		/// <summary>
		/// Vérifie que les valeurs de l'entête sont compatibles avec la méthode
		/// demandée
		/// </summary>
		private CResultAErreur VerifieEnteteAvecMethode ( object[] valeursEntete, MethodeResolutionValeurMatrice methode )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( methode == MethodeResolutionValeurMatrice.ExactOuValeurDefaut || valeursEntete.Length == 0 )
				return result;

			int nSensFixe = 1;
			double dValPrec = (double)valeursEntete[0];
			for ( int nIndex = 1; nIndex < valeursEntete.Length; nIndex++ )
			{
				int nSens = 0;
				if ( (double)valeursEntete[nIndex] < dValPrec )
					nSens = -1;
				else
					nSens = 1;
				if ( nIndex == 1 )
					nSensFixe = nSens;
				if ( nSensFixe != nSens )
				{
					string strErr = CUtilSurEnum.GetNomConvivial(methode.ToString());
                    result.EmpileErreur(I.T("The resolution method '@1' imposes a regulary progression of values|30056",strErr));
                }
			}
			return result;
		}

		//---------------------------------------------------
		/// <summary>
		/// Retourne l'index de l'élément correspondant à la valeur cherchee
		/// </summary>
		/// <param name="valCherchee"></param>
		/// <param name="?"></param>
		/// <returns></returns>
		protected int GetIndexExact ( object valCherchee, object[]valeurs )
		{
			int nIndex = -1;
			double dCherche = 0;
			if ( valCherchee is double )
				dCherche = (double)valCherchee;
			foreach ( object valTest in valeurs )
			{
				nIndex++;
				if ( valTest is string && 
					valCherchee.ToString().ToUpper(CultureInfo.InvariantCulture) ==
                    valTest.ToString().ToUpper(CultureInfo.InvariantCulture) )
					return nIndex;
				if ( valTest is double )
				{
					if ( Math.Abs(((double)valTest)-dCherche)<0.00001 )
						return nIndex;
				}
			}
			return -1;
		}

		//---------------------------------------------------
		/// <summary>
		/// Cherche les deux indexs les plus proches de la valeurs demandée
		/// </summary>
		/// <param name="valCherchee">valeur cherchée</param>
		/// <param name="valeurs">liste de valeurs</param>
		/// <param name="nIndexInf">index le plus petit des valeurs proches</param>
		/// <param name="nIndexSup">index le plus grand des valeurs proches</param>
		/// <param name="nPosition">0 si la valeur est entre les deux,
		/// 1 si la valeur est supérieure aux deux,
		/// -1 si la valeur est inférieure aux deux</param>
		/// <returns>
		/// </returns>

		protected void GetIndexs ( double dCherche, object[] valeurs, 
			ref int nIndexInf,
			ref int nIndexSup,
			ref int nPosition )
		{
			int nIndex = 0;
			nIndexInf = -1;
			nIndexSup = -1;
			nPosition = 0;
			foreach ( double dVal in valeurs )
			{
				if ( dVal < dCherche )
					nIndexInf = nIndex;
				if ( nIndexSup == -1 && dVal > dCherche )
					nIndexSup = nIndex;
				if ( nIndexInf >= 0 && nIndexSup >= 0 )
					break;
				nIndex++;
			}
			if ( nIndexInf < 0 )//Pas d'inf, retourne les deux premieres valeurs
				//supérieures
			{
				nPosition = -1;
				if ( nIndexSup+1 < valeurs.Length )
				{
					nIndexInf = nIndexSup;
					nIndexSup = nIndexInf+1;
				}
				else
					nIndexInf = nIndexSup;
				return;
			}
			if ( nIndexSup < 0 )//Pas de sup, on est au dessus du tableau,
				//retourne les deux dernieres valeurs du table
			{
				nPosition = 1;
				if ( valeurs.Length > 2 )
				{
					nIndexSup = valeurs.Length-1;
					nIndexInf = valeurs.Length-2;
				}
				else
					nIndexSup = nIndexInf;
				return;
			}
			//Sinon, on est entre les deux
			nPosition = 0;
		}

		//---------------------------------------------------
		public int GetIndexAPrendreEnCompte ( 
			double dValeurCherchee, 
			int nIndexMin, 
			int nIndexMax, 
			int nPosLigne,
			object[] valeurs,
			MethodeResolutionValeurMatrice methode )
		{
			int nIndex = -1;
			switch ( methode )
			{
				case MethodeResolutionValeurMatrice.Inferieur :
					if ( nIndexMin != -1 && nPosLigne <= 0)
						nIndex = nIndexMin;
					else
						nIndex = nIndexMax;
					break;
				case MethodeResolutionValeurMatrice.Superieur :
					if ( nIndexMax != -1 && nPosLigne >= 0)
						nIndex = nIndexMax;
					else
						nIndex = nIndexMin;
					break;
				case MethodeResolutionValeurMatrice.ProcheInferieur :
				case MethodeResolutionValeurMatrice.ProcheSuperieur :
					if ( nIndexMin == -1 )
						nIndex = nIndexMax;
					if (nIndexMax == -1 )
						nIndex = nIndexMin;
					double dValMin = (double)valeurs[nIndexMin];
					double dValMax = (double)valeurs[nIndexMax];
					double dEcartMin = Math.Abs(dValeurCherchee-dValMin);
					double dEcartMax = Math.Abs(dValeurCherchee-dValMax);
					if ( dEcartMin < dEcartMax )
						nIndex = nIndexMin;
					else if (dEcartMin > dEcartMax )
						nIndex = nIndexMax;
					else if ( methode == MethodeResolutionValeurMatrice.ProcheInferieur )
						nIndex = nIndexMin;
					else
						nIndex = nIndexMax;
					break;
			}
			return nIndex;
		}

		//---------------------------------------------------
		/// <summary>
		/// On fournit 
		/// f(a)=va
		/// f(b)=vb
		/// et on retourne f(x)
		/// </summary>
		/// <param name="fIndex">x</param>
		/// <param name="fIndexMin">a</param>
		/// <param name="fIndexMax">b</param>
		/// <param name="fValeurIndexMin">va</param>
		/// <param name="fValeurIndexMax">vb</param>
		/// <returns></returns>
		protected double GetValeurInterpollee ( double dIndex,
			double dIndexMin,
			double dIndexMax,
			double dValeurIndexMin,
			double dValeurIndexMax )
		{
			return (dValeurIndexMax-dValeurIndexMin)*(
				dIndex - dIndexMin )/(dIndexMax-dIndexMin)+dValeurIndexMin;
		}

		//---------------------------------------------------
		public object GetValeurAt ( object ligne, object colonne )
		{
			int nLigneExacte = GetIndexExact ( ligne, Lignes );
			int nColExacte = GetIndexExact ( colonne, Colonnes );
			
			//Si recherche exacte et pas trouvé, retourne la valeur par défaut
			if ( nLigneExacte == -1 )
				return ValeurDefaut;
			if ( nColExacte == -1 )
				return ValeurDefaut;
			if ( nLigneExacte != -1 && nColExacte != -1 )
				return m_valeurs[nLigneExacte, nColExacte];
			return ValeurDefaut;			
		}


		//---------------------------------------------------
		public double GetValeur ( object ligne, object colonne )
		{
			//Recherche valeurs directes
			if ( !LignesString )
			{
				try
				{
					ligne = Convert.ToDouble(ligne, CultureInfo.InvariantCulture);
				}
				catch
				{
					return m_dValeurDefaut;
				}
			}
			if ( !ColonnesString )
			{
				try
				{
					colonne = Convert.ToDouble(colonne, CultureInfo.InvariantCulture);
				}
				catch
				{
					return m_dValeurDefaut;
				}
			}
			int nLigneExacte = GetIndexExact ( ligne, Lignes );
			int nColExacte = GetIndexExact ( colonne, Colonnes );
			if ( nLigneExacte != -1 && nColExacte != -1 )
				return m_valeurs[nLigneExacte, nColExacte];

			//Si recherche exacte et pas trouvé, retourne la valeur par défaut
			if ( MethodeResolutionLignes == MethodeResolutionValeurMatrice.ExactOuValeurDefaut && 
				nLigneExacte == -1 )
				return ValeurDefaut;
			if ( MethodeResolutionColonne == MethodeResolutionValeurMatrice.ExactOuValeurDefaut &&
				nColExacte == -1 )
				return ValeurDefaut;

			int nLigneInf = -1;
			int nColInf = -1;
			int nLigneSup = -1;
			int nColSup = -1;
			int nPosLigne = 0;
			int nPosCol = 0;

			if ( MethodeResolutionLignes != MethodeResolutionValeurMatrice.ExactOuValeurDefaut )
				GetIndexs ( Convert.ToDouble(ligne, CultureInfo.InvariantCulture), Lignes, ref nLigneInf, ref nLigneSup, ref nPosLigne );
			if ( MethodeResolutionColonne != MethodeResolutionValeurMatrice.ExactOuValeurDefaut )
				GetIndexs ( Convert.ToDouble(colonne, CultureInfo.InvariantCulture), Colonnes, ref nColInf, ref nColSup, ref nPosCol );

			if ( nLigneExacte == -1 && nLigneInf == -1 && nLigneSup == -1 ||
				nColExacte == -1 && nColInf == -1 && nColSup == -1 )
				return ValeurDefaut;

			if ( nLigneExacte == -1 )
				nLigneExacte= GetIndexAPrendreEnCompte ( 
					Convert.ToDouble ( ligne, CultureInfo.InvariantCulture ),
					nLigneInf,
					nLigneSup,
					nPosLigne,
					Lignes,
					MethodeResolutionLignes );
			if ( nColExacte == -1 )
				nColExacte = GetIndexAPrendreEnCompte (
					Convert.ToDouble ( colonne, CultureInfo.InvariantCulture ),
					nColInf,
					nColSup,
					nPosCol,
					Colonnes,
					MethodeResolutionColonne );
			
			if ( nLigneExacte != -1 && nColExacte != -1 )
				return m_valeurs[nLigneExacte, nColExacte];

			if ( nLigneInf == -1 && nLigneExacte == -1)
				nLigneExacte = nLigneSup;
			if ( nLigneSup == -1 && nLigneExacte == -1)
				nLigneExacte = nLigneInf;
			if ( nColInf == -1 && nColExacte == -1)
				nColExacte = nColSup;
			if ( nColSup == -1 && nColExacte == -1)
				nColExacte = nColInf;

			//Interpolation des lignes
			if ( nLigneExacte == -1 && nColExacte != -1 )
			{
				double dValInf = m_valeurs[nLigneInf, nColExacte];
				double dValSup = m_valeurs[nLigneSup, nColExacte];
				return GetValeurInterpollee (
					Convert.ToDouble( ligne, CultureInfo.InvariantCulture ),
					(double)Lignes[nLigneInf],
					(double)Lignes[nLigneSup],
					dValInf,
					dValSup );
			}

			//Interpollation des colonnes
			if ( nLigneExacte != -1 && nColExacte == -1 )
			{
				double dValInf = m_valeurs[nLigneExacte, nColInf];
				double dValSup = m_valeurs[nLigneExacte, nColSup];
				return GetValeurInterpollee (
					Convert.ToDouble( colonne, CultureInfo.InvariantCulture ),
					(double)Colonnes[nColInf],
					(double)Colonnes[nColSup],
					dValInf,
					dValSup );
			}

			//Sinon interpollation sur ligne et sur colonne
			
			//Interpolle les valeurs des lignes
			double dValColInf = GetValeurInterpollee (
				Convert.ToDouble ( ligne, CultureInfo.InvariantCulture ),
				(double)Lignes[nLigneInf],
				(double)Lignes[nLigneSup],
				m_valeurs[nLigneInf, nColInf],
				m_valeurs[nLigneSup, nColInf] );
			double dValColSup = GetValeurInterpollee ( 
				Convert.ToDouble ( ligne, CultureInfo.InvariantCulture ),
				(double)Lignes[nLigneInf],
				(double)Lignes[nLigneSup],
				m_valeurs[nLigneInf, nColSup],
				m_valeurs[nLigneSup, nColSup] );
			//Puis interpolle ces deux valeurs sur les colonnes
			return GetValeurInterpollee ( 
				Convert.ToDouble ( colonne, CultureInfo.InvariantCulture ),
				(double)Colonnes[nColInf],
				(double)Colonnes[nColSup],
				dValColInf,
				dValColSup );
			//WHAOO !
		}

			
		#region Membres de I2iSerializable

		/// //////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// //////////////////////////////////////////
		private void SerializeTable ( C2iSerializer serializer, object[] tableau, bool bIsString )
		{
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( object obj in tableau )
					{
						if ( bIsString )
						{
							string strVal = obj.ToString();
							serializer.TraiteString ( ref strVal );
						}
						else
						{
							Double dVal = Convert.ToDouble ( obj, CultureInfo.InvariantCulture );
							serializer.TraiteDouble ( ref dVal );
						}
					}
					break;
				case ModeSerialisation.Lecture :
					for ( int nTmp = 0; nTmp < tableau.Length; nTmp++ )
					{
						if ( bIsString )
						{
							string strVal = "";
							serializer.TraiteString ( ref strVal );
							tableau[nTmp] = strVal;
						}
						else
						{
							double dVal = 0;
							serializer.TraiteDouble ( ref dVal );
							tableau[nTmp] = dVal;
						}
					}
					break;
			}
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( serializer.Mode == ModeSerialisation.Ecriture )
				result = VerifieCoherence();
			if ( !result )
				return result;

			serializer.TraiteBool ( ref m_bLignesString );
			serializer.TraiteBool ( ref m_bColonnesString );
			serializer.TraiteDouble ( ref m_dValeurDefaut );

			int nRes = (int)MethodeResolutionLignes;
			serializer.TraiteInt ( ref nRes );
			MethodeResolutionLignes = (MethodeResolutionValeurMatrice)nRes;

			nRes = (int)MethodeResolutionColonne;
			serializer.TraiteInt ( ref nRes );
			MethodeResolutionColonne = (MethodeResolutionValeurMatrice)nRes;

			int nNbLignes = Lignes.Length;
			serializer.TraiteInt ( ref nNbLignes );

			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_lignes = new object[nNbLignes];
			SerializeTable ( serializer, m_lignes, LignesString );

			int nNbColonnes = Colonnes.Length;
			serializer.TraiteInt ( ref nNbColonnes );
			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_colonnes = new object[nNbColonnes];
			SerializeTable ( serializer, m_colonnes,ColonnesString );

			//Sérialization des valeurs
			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_valeurs = new double[nNbLignes, nNbColonnes];
			for ( int nLigne = 0; nLigne < nNbLignes; nLigne++ )
				for ( int nCol = 0; nCol < nNbColonnes; nCol++ )
				{
					double dVal = 0;
					if ( serializer.Mode == ModeSerialisation.Ecriture )
						dVal = m_valeurs[nLigne, nCol];
					serializer.TraiteDouble ( ref dVal );
					m_valeurs[nLigne, nCol] = dVal;
				}
			return result;
		}

		#endregion
	}
}
