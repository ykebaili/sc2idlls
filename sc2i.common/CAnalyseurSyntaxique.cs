using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;


namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CAnalyseurSyntaxique.
	/// </summary>
	public abstract class CAnalyseurSyntaxique
	{
        private Dictionary<int, Dictionary<string, List<COperateurAnalysable>>> m_dicOperateursParNiveau = new Dictionary<int, Dictionary<string,List<COperateurAnalysable>>>();
		/*
		 * Structure générale d'une chaine analysable : 
		 * Structure correcte = Nx ExpObjet NxMore
		 *				ExpObjet = Indexeur expObjet|.Propriete ExpObjet|.Methode(Parametres) ExpObjet|null
		 *				Indexeur = [Nx]
		 *				Parametres = ([Nx,]*)
		 *				NxMore= Opx N(x-1) NxMore | Null
		 * où x est le niveau de l'opérateur.
		 * 
		 */

        private bool m_bVerifieParametresLorsDeLanalyse = true;

		#region CErreurAnalyseSyntaxique
		/// ///////////////////////////////////////////////////////
		private class CErreurAnalyseSyntaxique : CErreurSimple
		{
			private int m_nPositionDansChaine = -1;
			/// ///////////////////////////////////////////////////////
			public CErreurAnalyseSyntaxique ( string strMessage, int nPositionDansChaine )
				:base ( strMessage )
			{
				m_nPositionDansChaine = nPositionDansChaine;
			}

			/// ///////////////////////////////////////////////////////
			public int PositionDansChaine
			{
				get
				{
					return m_nPositionDansChaine;
				}
			}
		}
		#endregion

		#region CContexteAnalyseSyntaxique
		/// ///////////////////////////////////////////////////////
		protected class CContexteAnalyseSyntaxique : CPileErreur
		{
			public String m_strFormule;
			public int m_nPositionAnalyse;
			int m_nPosPile;
			public string m_strCaracteresPasses = "";

            private Dictionary<int, List<string>> m_dicPositionToMot = new Dictionary<int, List<string>>();

			
			private ArrayList m_lstPile;

			public CContexteAnalyseSyntaxique()
			{
				m_lstPile = new ArrayList();
				m_nPosPile = -1;
			}

			//Retourne vrai si la fin de chaine est atteinte
			public bool IsEnd()
			{
				return m_nPositionAnalyse >= m_strFormule.Length;
			}

			//Retourne le caractères suivant
			public char GetNextChar()
			{
				if ( m_nPositionAnalyse < m_strFormule.Length )
					return m_strFormule[m_nPositionAnalyse];
				return '\0';
			}

            public string Formule
            {
                get
                {
                    return m_strFormule;
                }
                set
                {
                    m_strFormule = value;
                    m_dicPositionToMot.Clear();
                }
            }


            //----------------------------------------------
            public string[] GetMotAIdentifier()
            {
                List<string> strMots = null;
                if (m_dicPositionToMot.TryGetValue(m_nPositionAnalyse, out strMots))
                    return strMots.ToArray();

                strMots = new List<string>();

                string strMot = "";
                //Mots classiques
                String strSeparateurs = "";
                for (int n = 0; n < 2; n++)
                {
                    strSeparateurs = "01234567890#.()[]\"+-%/*,;\n\r\t!?<>= ";
                    if (n == 0 && m_strFormule[m_nPositionAnalyse] == ':')
                        strSeparateurs = "(";
                    if (n == 1)
                        strSeparateurs = " (([\n\r\t01234567890@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&éè_ç";
                    strMot = "";
                    int nPos = m_nPositionAnalyse;
                    while (true)
                    {
                        bool bEnd = false;
                        if (nPos >= m_strFormule.Length)
                        {
                            bEnd = true;
                        }
                        char c = ' ';
                        if ( !bEnd )
                        {
                         c = m_strFormule[nPos];
                        if (strSeparateurs.IndexOf(c) >= 0)
                        {
                            bEnd = true;
                        }
                        }
                        if (bEnd)
                        {
                            if (n == 0)
                            {
                                if (strMot.Length > 0)
                                    strMots.Add(strMot);
                            }
                            else
                            {
                                for (int nCar = strMot.Length; nCar > 0; nCar--)
                                    strMots.Add(strMot.Substring(0, nCar));
                            }
                            m_dicPositionToMot[m_nPositionAnalyse] = strMots;
                            if ( n >0 )
                                return strMots.ToArray();
                            break;
                        }
                        strMot += c;
                        nPos++;
                    }
                }
                return new string[0];
            }
            
                

			
			public static CContexteAnalyseSyntaxique operator++ ( CContexteAnalyseSyntaxique ctx )
			{
				ctx.m_nPositionAnalyse++;
				return ctx;
			}

            public static CContexteAnalyseSyntaxique Increment(CContexteAnalyseSyntaxique ctx)
            {
                ctx++;
                return ctx;
            }

			public int PositionAnalyse
			{
				get
				{
					return m_nPositionAnalyse;
				}
			}

			public void PushToken ( CTokenAnalyseSyntaxique token )
			{
				token.CaracteresControlesAvant = m_strCaracteresPasses;
				m_strCaracteresPasses = "";
				m_lstPile.Add ( token );
				m_nPosPile++;
			}

			public CTokenAnalyseSyntaxique PopToken ()
			{
				if (m_nPosPile >= 0 )
				{
					m_nPosPile--;
					return (CTokenAnalyseSyntaxique)m_lstPile[m_nPosPile+1];
				}
				return null;
			}

			public int GetPosPile()
			{
				return m_nPosPile;
			}

			public void SetPosPile( int nPos )
			{
				m_nPosPile = nPos;
			}

			public void UnPop()
			{
				m_nPosPile++;
			}
		}
		#endregion


		#region Tokens
		/// ///////////////////////////////////////////////////////
		protected abstract class CTokenAnalyseSyntaxique
		{
			protected int m_nPositionDansChaine = 0;
			protected string m_strCaracteresControlesAvant = "";
			protected CTokenAnalyseSyntaxique(int nPosInChaine)
			{
				m_nPositionDansChaine = nPosInChaine;
			}

			public abstract int GetNiveauToken();

			public int PositionDansChaine
			{
				get
				{
					return m_nPositionDansChaine;
				}
				set
				{
					m_nPositionDansChaine = value;
				}
			}

			public string CaracteresControlesAvant
			{
				get
				{
					return m_strCaracteresControlesAvant;
				}
				set
				{
					m_strCaracteresControlesAvant = value;
				}
			}

		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenParentheseOuvrante : CTokenAnalyseSyntaxique
		{
			public CTokenParentheseOuvrante(int nPosInChaine)
				:base(nPosInChaine)
			{
			}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenFonctionParentheses : CTokenAnalyseSyntaxique
		{
			public CTokenFonctionParentheses(int nPosInChaine)
				:base(nPosInChaine)
			{
			}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenParentheseFermante : CTokenAnalyseSyntaxique
		{
			public CTokenParentheseFermante(int nPosInChaine)
				:base(nPosInChaine)
			{
			}
			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenAccoladeOuvrante : CTokenAnalyseSyntaxique
		{
			public CTokenAccoladeOuvrante(int nPosInChaine)
				:base(nPosInChaine)
			{
			}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenAccoladeFermante : CTokenAnalyseSyntaxique
		{
			public CTokenAccoladeFermante(int nPosInChaine)
				:base(nPosInChaine)
			{
			}
			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenThis : CTokenAnalyseSyntaxique
		{
			public CTokenThis ( int nPosInChaine )
				:base ( nPosInChaine )
			{
			}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenRoot : CTokenAnalyseSyntaxique
		{
			public CTokenRoot(int nPosInChaine)
				: base(nPosInChaine)
			{
			}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenOperateur : CTokenAnalyseSyntaxique
		{
			protected COperateurAnalysable m_operateur;

			/// ///////////////////////////////////////////////////////
			public CTokenOperateur ( int nPosInChaine, COperateurAnalysable operateur )
				:base(nPosInChaine)
			{
				m_operateur = operateur;
			}

			/// ///////////////////////////////////////////////////////
			public COperateurAnalysable Operateur
			{
				get
				{
					return m_operateur;
				}
			}
			/// ///////////////////////////////////////////////////////
			public override int GetNiveauToken()
			{
				return m_operateur.Niveau;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenConstante : CTokenAnalyseSyntaxique
		{
			protected object m_valeur;
			/// //////////////////////////////////////
			public CTokenConstante ( int nPosInChaine,object valeur )
				:base(nPosInChaine)
			{
				m_valeur = valeur;
			}

			/// //////////////////////////////////////
			public object Valeur
			{
				get
				{
					return m_valeur;
				}
			}
			public override int GetNiveauToken()
			{
				return 0;
			}
		}
			
		/// ///////////////////////////////////////////////////////
		protected class CTokenChamp : CTokenAnalyseSyntaxique
		{
			protected string m_strNomChamp;
			/// ///////////////////////////////////////////////////////
			public CTokenChamp ( int nPosInChaine,string strNomChamp )
				:base(nPosInChaine)
			{
				m_strNomChamp = strNomChamp;
			}

			/// ///////////////////////////////////////////////////////
			public string NomChamp
			{
				get
				{
					return m_strNomChamp;
				}
			}
			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenMethodeDynamique : CTokenAnalyseSyntaxique
		{
			protected string m_strNomMethode;
			/// ///////////////////////////////////////////////////////
			public CTokenMethodeDynamique ( int nPosInChaine,string strNomMethode )
				:base(nPosInChaine)
			{
				m_strNomMethode = strNomMethode;
			}

			/// ///////////////////////////////////////////////////////
			public string NomMethode
			{
				get
				{
					return m_strNomMethode;
				}
			}
			public override int GetNiveauToken()
			{
				return 0;
			}
		}
		/// ///////////////////////////////////////////////////////
		protected class CTokenIndexeur : CTokenAnalyseSyntaxique
		{
			public CTokenIndexeur(int nPosInChaine)
				:base(nPosInChaine)
		{
		}

			public override int GetNiveauToken()
			{
				return 0;
			}
		}

		/// ///////////////////////////////////////////////////////
		protected class CTokenOperateurObjet : CTokenAnalyseSyntaxique
		{
			public CTokenOperateurObjet(int nPosInChaine)
				:base(nPosInChaine)
			{
			}

			public override int GetNiveauToken()
			{
				return -1;
			}
		}
		#endregion


		//tant que > 1, c'est qu'on analyse une expression objet, et que le type
		//analysé est donc inconnu.
		private int m_nNbPileAnalyseExpressionObjet = 0;

        public bool VerifieParametresLorsDeLanalyse
        {
            get
            {
                return m_bVerifieParametresLorsDeLanalyse;
            }
            set
            {
                m_bVerifieParametresLorsDeLanalyse = value;
            }
        }

		public void StartAnalyseExpressionObjet()
		{
			m_nNbPileAnalyseExpressionObjet++;
		}

		public bool IsAnalyseExpressionObjet()
		{
			return m_nNbPileAnalyseExpressionObjet > 0;
		}

		public void EndAnalyseExpressionObjet()
		{
			m_nNbPileAnalyseExpressionObjet--;
		}

		
		/// <summary>
		/// Retourne un IExpression dans le data du result
		/// </summary>
		/// <param name="strFormule"></param>
		/// <returns></returns>
		public virtual CResultAErreur AnalyseChaine ( String strFormule )
		{
			CContexteAnalyseSyntaxique ctx = new CContexteAnalyseSyntaxique();
			ctx.m_nPositionAnalyse = 0;
			ctx.Formule = strFormule;
			m_contexteAnalyse = ctx;
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( BaseNiveau(NiveauOperateurMax) )
				{
					if ( ctx.PositionAnalyse < strFormule.TrimEnd().Length )
					{
                        m_contexteAnalyse.EmpileErreur(I.T("Error towards '@1' |30013", strFormule.Substring(0, ctx.PositionAnalyse)));
						m_contexteAnalyse.EmpileErreur(I.T("Error in the formula|30014"));
					}
					else
					{
						IExpression expression = GetExpression(NiveauOperateurMax);
						if ( expression != null && m_contexteAnalyse.Erreurs.Length == 0)
						{
							result.Data = expression;
							return result;
						}
					}
				}
				result.Erreur = m_contexteAnalyse;
                result.EmpileErreur(I.T("Error in the formula|30014"));
			}
			catch ( Exception  e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		private CContexteAnalyseSyntaxique m_contexteAnalyse;

		protected CContexteAnalyseSyntaxique ContexteAnalyse
		{
			get
			{
				return m_contexteAnalyse;
			}
		}

		protected abstract int NiveauOperateurMax{get;}

		protected abstract char SeparateurParametres{get;}
			
		protected abstract ArrayList GetOperateursNiveau(int nNiveau);

		protected abstract IAllocateurExpression AllocateurExpression {get;}

		protected abstract bool IntegrerSyntaxeObjet{get;}

		protected abstract CResultAErreur GetExpressionThis (  );

		protected virtual CResultAErreur GetExpressionRoot()
		{
			return CResultAErreur.True;
		}

		protected void PasseCaracteresControle()
		{
			String strControle = " \n\t\r";
			while ( m_contexteAnalyse.m_nPositionAnalyse < m_contexteAnalyse.m_strFormule.Length &&
				strControle.IndexOf(m_contexteAnalyse.m_strFormule[m_contexteAnalyse.m_nPositionAnalyse])>=0 )
			{
				m_contexteAnalyse.m_strCaracteresPasses += m_contexteAnalyse.m_strFormule[m_contexteAnalyse.m_nPositionAnalyse];
				m_contexteAnalyse.m_nPositionAnalyse++;
			}
		}


		protected virtual bool BaseNiveau ( int nNiveau )
		{
			if ( nNiveau == 0 )
				return AnalyseNiveau0();
			if (!BaseNiveau ( nNiveau-1 ) )
				return false;
			/*if ( !ExpObjet () )
				return false;*/
			if ( !BaseNiveauMore ( nNiveau ) )
				return false;
			return true;
		}

		

		protected virtual bool BaseNiveauMore ( int nNiveau )
		{
			if ( nNiveau < 0 )
				return false;
			PasseCaracteresControle();
			if ( !IsOperateurNiveauN (nNiveau, false) )
				return true;
			if ( !BaseNiveau(nNiveau+1) )
				return false;
			if ( !BaseNiveauMore(nNiveau) )
				return false;
			return true;
		}

		protected virtual bool ExpObjet()
		{
			if ( !IntegrerSyntaxeObjet )
				return true;
			if ( Indexeur() )
				return ExpObjet();
			if ( m_contexteAnalyse.IsEnd()|| m_contexteAnalyse.GetNextChar()!='.')
				return true;
			m_contexteAnalyse++;
			if ( ChampOuMethode() )
			{
				if ( !ExpObjet() )
					return false;
				m_contexteAnalyse.PushToken(new CTokenOperateurObjet(m_contexteAnalyse.PositionAnalyse));
				return true;
			}
			m_contexteAnalyse.EmpileErreur(I.T("Error in object syntax|30015"));
			return false;
		}
		
		protected virtual bool AnalyseNiveau0()
		{
			if ( !AnalyseElement0() )
				return false;
			if ( !ExpObjet() && m_contexteAnalyse.Erreurs.Length > 0 )
				return false;
			return true;
		}

		protected virtual bool IsFonction( bool bMethode )
		{
			int nPos = m_contexteAnalyse.m_nPositionAnalyse;
			
			if ( IsOperateurNiveauN(0, bMethode) )
			{
				if (!ParametresOuListe('(',')', new CTokenParentheseOuvrante(0), new CTokenParentheseFermante(0) ) )
				{
					CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
					m_contexteAnalyse.UnPop();
                    CTokenOperateur tokenOp = (token as CTokenOperateur);
					if ( tokenOp == null )
					{
						m_contexteAnalyse.EmpileErreur( new CErreurAnalyseSyntaxique(I.T("Error in the formula|30014"), m_contexteAnalyse.PositionAnalyse));
						return false;
					}
					m_contexteAnalyse.EmpileErreur( new CErreurAnalyseSyntaxique(I.T("Error in the parameters of @1|30016",tokenOp.Operateur.Texte ), m_contexteAnalyse.PositionAnalyse ));
					return false;
				}
				else
					return true;
			}
			m_contexteAnalyse.m_nPositionAnalyse = nPos;
			return false;
		}

		protected virtual bool AnalyseElement0()
		{
			//Constante ?
			if ( Constante() )
				return true;
			if ( IsFonction ( false ) )
				return true;
			if ( ChampOuMethode() )
				return true;
			if ( ParametresOuListe('{','}', new CTokenAccoladeOuvrante(0), new CTokenAccoladeFermante(0)) )//Gestion des parenthèses seules 
				return true;
			
			//A laisser à la fin !!
			m_contexteAnalyse.PushToken(new CTokenFonctionParentheses( m_contexteAnalyse.PositionAnalyse));
			if ( ParametresOuListe('(',')', new CTokenParentheseOuvrante(0), new CTokenParentheseFermante(0)) )//Gestion des parenthèses seules 
				return true;
			
			m_contexteAnalyse.PopToken();//ce n'était pâs une parenthèse
			return false;
		}

		//Identifie une constante
		protected virtual bool Constante()
		{
			PasseCaracteresControle();
			//Dépassement de la fin de chaine
			if ( m_contexteAnalyse.IsEnd() )
				return false;
			String strConstante="";
			char cDelimiteur = m_contexteAnalyse.GetNextChar();
			bool bIsString = "'\"".LastIndexOf(cDelimiteur) >= 0;
			bool bIsDate = "#".LastIndexOf(cDelimiteur) >= 0;
			if ( bIsString || bIsDate  )
			{
				//C'est une chaine de caractères
				m_contexteAnalyse++;
				do
				{
					//Traitement de la \delimiteur
					if ( m_contexteAnalyse.GetNextChar() == '\\' && 
						m_contexteAnalyse.m_nPositionAnalyse+1 < m_contexteAnalyse.m_strFormule.Length )
					{
						if ( m_contexteAnalyse.m_strFormule[m_contexteAnalyse.m_nPositionAnalyse+1]==cDelimiteur )
						{
							strConstante+=cDelimiteur;
							m_contexteAnalyse++;
							m_contexteAnalyse++;
						}
						else
						{
							strConstante += "\\";
							m_contexteAnalyse++;
						}
					}
					else if ( m_contexteAnalyse.GetNextChar() != cDelimiteur )
					{
						strConstante+=m_contexteAnalyse.GetNextChar();
						m_contexteAnalyse++;
					}
				}
				while (!m_contexteAnalyse.IsEnd() && m_contexteAnalyse.GetNextChar()!=cDelimiteur);
				m_contexteAnalyse++;
				if ( bIsString )
					m_contexteAnalyse.PushToken ( new CTokenConstante(m_contexteAnalyse.m_nPositionAnalyse, strConstante) );
				if ( bIsDate )
				{
					try
					{
						DateTime dtTmp = DateTime.Parse(strConstante, CultureInfo.CurrentCulture);
						m_contexteAnalyse.PushToken(new CTokenConstante(m_contexteAnalyse.m_nPositionAnalyse, dtTmp));
					}
					catch
					{
						m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in date constant '@1'|30017",strConstante), m_contexteAnalyse.PositionAnalyse ));
						return false;
					}
				}
				return true;
			}
			//Ce n'est pas une chaine ni une date
			String strDecimal = "0123456789.";
			//string strSepDec = ".";

			//Ne jamais considérer la virgule comme séparateur de chiffres, ça pose des pbs
			//par exemple in (32,15) peut être interpreté par in (32.15)
			/*if (SeparateurParametres != ',')
			{
				strDecimal += ",";
				strSepDec += ",";
			}*/

			if ( m_contexteAnalyse.GetNextChar() == '-' )
			{
				strConstante+="-";
				m_contexteAnalyse++;
			}
			else if ( m_contexteAnalyse.GetNextChar() == '+' )
			{
				m_contexteAnalyse++;
			}
			if ( m_contexteAnalyse.IsEnd() )
			{
				m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the constant|30018"), m_contexteAnalyse.PositionAnalyse) );
				return false;
			}
			do
			{
				if ( strDecimal.IndexOf(m_contexteAnalyse.GetNextChar()) >= 0 )
				{
					strConstante+= m_contexteAnalyse.GetNextChar();
					m_contexteAnalyse++;
				}
				else
				{
					//Traitement de l'exposant
					if ( "Ee".IndexOf(m_contexteAnalyse.GetNextChar()) >= 0 &&
                        !String.IsNullOrEmpty(strConstante))
					{
						strConstante += "e";
						m_contexteAnalyse++;
						if ( m_contexteAnalyse.GetNextChar() == '+' )
							m_contexteAnalyse++;
						else if ( m_contexteAnalyse.GetNextChar() == '-' )
						{
							strConstante+="-";
							m_contexteAnalyse++;
						}
					}
					else
					{
						if ( !String.IsNullOrEmpty(strConstante) )
							m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the constant '@1'|30019",strConstante), m_contexteAnalyse.PositionAnalyse) );
						return false;
					}
				}
			}
			while ( !m_contexteAnalyse.IsEnd() && strDecimal.IndexOf(m_contexteAnalyse.GetNextChar())>=0);
			//Crée le Operateur
			if ( String.IsNullOrEmpty(strConstante) )
			{
				m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the constant|30018"), m_contexteAnalyse.PositionAnalyse) );
				return false;
			}
			//Tente la conversion en entier
			object valeur = null;
			if ( strConstante == "." )//Point seul, c'est le début d'une expression objet !!!
			{
				m_contexteAnalyse.m_nPositionAnalyse--;
				return false;
			}
			try
			{
				valeur = Int32.Parse(strConstante, CultureInfo.CurrentCulture);
			}
			catch { valeur = null; }
			if ( valeur == null )
			{
				//Tente la conversion en double
				try
				{
					valeur = Double.Parse(strConstante, CultureInfo.CurrentCulture);
				}
				catch
				{
					if (strConstante.IndexOf('.') >= 0)
						strConstante = strConstante.Replace(".",",");
					else
						strConstante = strConstante.Replace(",",".");
					try
					{
						valeur = Double.Parse(strConstante, CultureInfo.CurrentCulture);
					}
					catch {valeur = null;}
				}
			}
			if ( valeur == null )
			{
                m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the constant '@1'|30019", strConstante), m_contexteAnalyse.PositionAnalyse));
				return false;
			}
			m_contexteAnalyse.PushToken(new CTokenConstante(m_contexteAnalyse.m_nPositionAnalyse, valeur));
			return true;
		}

		//Valide le champ demandé. A surcharger si besoin
		//protected abstract CResultAErreur GetDefinitionChamp ( string strChamp, ref IDefinitionChampExpression definitionCorrespondante );
		protected virtual bool ChampOuMethode()
		{
			if ( IsFonction ( true ) )
				return true;
			if ( Champ() )
				return true;
			return false;
		}

		protected virtual bool Champ()
		{
			PasseCaracteresControle();
			bool bBornesCrochet = m_contexteAnalyse.GetNextChar() == '[';
			if ( bBornesCrochet )
				m_contexteAnalyse++;
			else
				//Le premier caractere doit être un texte
				if ( "@abcdefghijklmnopqrstuvwxyz_".LastIndexOf(
                    m_contexteAnalyse.GetNextChar().ToString().ToLower(CultureInfo.CurrentCulture),
                    StringComparison.CurrentCulture) < 0)
					return false;
			String strNomChamp = "";
			String strSeparateurs;
			if ( bBornesCrochet )
				strSeparateurs = "]";
			else
				strSeparateurs = ":#[]()\"+-%/*,;\n\r\t!?<>= ";
			if ( IntegrerSyntaxeObjet )
				strSeparateurs+=".";
			while ( !m_contexteAnalyse.IsEnd() && strSeparateurs.LastIndexOf(m_contexteAnalyse.GetNextChar())<0 )
			{
				strNomChamp += m_contexteAnalyse.GetNextChar();
				
				m_contexteAnalyse++;
			}
			strNomChamp = strNomChamp.Trim();
			//Le champ peut être suivi de paramètres. Dans ce cas, c'est une méthode
			PasseCaracteresControle();
			if ( !String.IsNullOrEmpty(strNomChamp) )
			{
				if ( strNomChamp.ToUpper(CultureInfo.CurrentCulture)=="THIS" )//PAS BEAU!!!
					m_contexteAnalyse.PushToken ( new CTokenThis ( m_contexteAnalyse.m_nPositionAnalyse)  );
				else if ( strNomChamp.ToUpper() == "ROOT" )//PAS BEAU NON PLUS
					m_contexteAnalyse.PushToken(new CTokenRoot(m_contexteAnalyse.m_nPositionAnalyse));
				else if ( m_contexteAnalyse.GetNextChar()!='(' )
					m_contexteAnalyse.PushToken ( new CTokenChamp(m_contexteAnalyse.m_nPositionAnalyse, strNomChamp) );
				else
				{
					m_contexteAnalyse.PushToken ( new CTokenMethodeDynamique(m_contexteAnalyse.m_nPositionAnalyse, strNomChamp ) );
					if (!ParametresOuListe('(',')', new CTokenParentheseOuvrante(0), new CTokenParentheseFermante(0) ) )
					{
						CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
						m_contexteAnalyse.UnPop();
                        CTokenOperateur tokenOp = (token as CTokenOperateur);
						if ( tokenOp == null )
						{
                            m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the constant|30018"), m_contexteAnalyse.PositionAnalyse));
							return false;
						}
						m_contexteAnalyse.EmpileErreur( new CErreurAnalyseSyntaxique(I.T("Error in the parameters of  @1|30020",tokenOp.Operateur.Texte) , m_contexteAnalyse.PositionAnalyse ));
						return false;
					}
					else
						return true;
				}
			}
			
			if ( bBornesCrochet )
			{
				if ( m_contexteAnalyse.IsEnd() || m_contexteAnalyse.GetNextChar()!=']' )
				{
					m_contexteAnalyse.EmpileErreur(I.T("Closing hook of @1 not found|30021",strNomChamp));
					return false;
				}
				m_contexteAnalyse++;
			}

			return true;
		}

		/// //////////////////////////////////////////////////////////////////////
		public bool Indexeur()
		{
			if ( m_contexteAnalyse.IsEnd() || m_contexteAnalyse.GetNextChar()!='[' )
				return false;//rien est un indexeur !!!
			m_contexteAnalyse++;
			if ( !BaseNiveau(NiveauOperateurMax) )
			{
                m_contexteAnalyse.EmpileErreur(I.T("Error in indexator|30022"));
				return false;
			}
			if ( m_contexteAnalyse.IsEnd() || m_contexteAnalyse.GetNextChar()!=']' )
			{
				m_contexteAnalyse.EmpileErreur(I.T("] not found for the indexator|30023"));
				return false;
			}
			m_contexteAnalyse++;
			m_contexteAnalyse.PushToken(new CTokenIndexeur(m_contexteAnalyse.PositionAnalyse));
			return true;
		}
			
						   

		protected bool ParametresOuListe( 
			char cCaractereOuvrant, 
			char cCaractereFermant, 
			CTokenAnalyseSyntaxique tokenOuvrant, 
			CTokenAnalyseSyntaxique tokenFermant)
		{
			PasseCaracteresControle();
			if ( m_contexteAnalyse.IsEnd() )
				return false;

			if ( m_contexteAnalyse.GetNextChar() != cCaractereOuvrant )
				return false;
			m_contexteAnalyse++;
		
			tokenOuvrant.PositionDansChaine = m_contexteAnalyse.PositionAnalyse;

			m_contexteAnalyse.PushToken ( tokenOuvrant);
			PasseCaracteresControle();
			if ( m_contexteAnalyse.GetNextChar()== cCaractereFermant )
			{
				tokenFermant.PositionDansChaine = m_contexteAnalyse.PositionAnalyse;
				m_contexteAnalyse.PushToken ( tokenFermant );
				m_contexteAnalyse++;
				return true;
			}
			while ( !m_contexteAnalyse.IsEnd() && m_contexteAnalyse.GetNextChar()!= cCaractereFermant )
			{
				if ( !BaseNiveau(NiveauOperateurMax) )
					return false;
			
				if ( !m_contexteAnalyse.IsEnd() )
				{
					PasseCaracteresControle();
					if ( m_contexteAnalyse.GetNextChar() == cCaractereFermant )
					{
						tokenFermant.PositionDansChaine = m_contexteAnalyse.PositionAnalyse;
						m_contexteAnalyse.PushToken ( tokenFermant );
						m_contexteAnalyse++;
						return true;
					}
					if ( m_contexteAnalyse.GetNextChar() != SeparateurParametres )
					{
						m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the parameters or in the array|30024"), m_contexteAnalyse.PositionAnalyse));
						return false;
					}
					m_contexteAnalyse++;
				}
			}
			return false;
		}


		/// /////////////////////////////////////////////////////////
		private class COperateurComparer : IComparer
		{
			public int Compare ( object obj1, object obj2 )
			{
                COperateurAnalysable op1 = (obj1 as COperateurAnalysable);
                COperateurAnalysable op2 = (obj2 as COperateurAnalysable);

				if ((op1 == null) || (op2 == null))
					return -1;
                return -(String.Compare(op1.Texte, op2.Texte, StringComparison.CurrentCulture));
			}
		}

        
        /// /////////////////////////////////////////////////////////
        ///Retourne la liste d'opérateurs non statiques, qui peuvent être alloués à la
        ///volée pour le mot demandé et le niveau demandé
        protected virtual List<COperateurAnalysable> GetOperateursDynamiqueNiveau(int nNiveau, string strMot)
        {
            return new List<COperateurAnalysable>();
        }


		/// /////////////////////////////////////////////////////////
		protected virtual bool IsOperateurNiveauN ( int nNiveau, bool bMethodes )
		{
			PasseCaracteresControle();
			if ( m_contexteAnalyse.IsEnd() )
				return false;
			/*ArrayList lstTmp = GetOperateursNiveau(nNiveau);
			if ( lstTmp == null )
				return false;
			//Tri les operateurs en décroissant
			lstTmp.Sort( new COperateurComparer() );*/
            Dictionary<string, List<COperateurAnalysable>> setMots = null;
            if (!m_dicOperateursParNiveau.TryGetValue(nNiveau, out setMots))
            {
                setMots = new Dictionary<string, List<COperateurAnalysable>>();
                m_dicOperateursParNiveau[nNiveau] = setMots;
                ArrayList lst = GetOperateursNiveau(nNiveau);
                if (lst != null)
                {
                    foreach (COperateurAnalysable operateurTmp in lst)
                    {
                        List<COperateurAnalysable> lstOps = null;
                        if (!setMots.TryGetValue(operateurTmp.Texte.ToLower(), out lstOps))
                        {
                            lstOps = new List<COperateurAnalysable>();
                            setMots[operateurTmp.Texte.ToLower()] = lstOps;
                        }
                        if (operateurTmp.Texte.Length > 0)
                            lstOps.Add(operateurTmp);
                    }
                }
            }
            List<COperateurAnalysable> operateurs = null;
            string[] strMots = m_contexteAnalyse.GetMotAIdentifier();
            if (strMots.Length > 0)
            {
                foreach (string strMot in strMots)
                {
                    if (strMot.Length > 0)
                    {
                        operateurs = null;
                        if (!setMots.TryGetValue(strMot.ToLower(), out operateurs))
                            operateurs = GetOperateursDynamiqueNiveau ( nNiveau, strMot.ToLower() );
                        foreach (COperateurAnalysable operateur in operateurs)
                        {
                            if (operateur.IsMethode == bMethodes)
                            {
                                m_contexteAnalyse.PushToken(new CTokenOperateur(m_contexteAnalyse.m_nPositionAnalyse, operateur));
                                m_contexteAnalyse.m_nPositionAnalyse += operateur.Texte.Length;
                                return true;
                            }
                        }
                    }
                    
                }
            }
            /*
			foreach ( COperateurAnalysable operateur in GetOperateursNiveau(nNiveau) )
			{
				if ( operateur.IsMethode== bMethodes && !String.IsNullOrEmpty(operateur.Texte) && Identifie ( operateur.Texte, nNiveau )  )
				{
					m_contexteAnalyse.PushToken ( new CTokenOperateur(m_contexteAnalyse.m_nPositionAnalyse, operateur) );
					m_contexteAnalyse.m_nPositionAnalyse+= operateur.Texte.Length;
					return true;
				}
			}*/
			return false;
		}

       /* private string[] GetNextMotPourIdentification()
        {

            List<string> strNextMots = new List<string>();

            //Mots classiques
            String strSeparateurs = "";
            for (int n = 0; n < 2; n++)
            {
                strSeparateurs = "01234567890#.()[]\"+-:%/*,;\n\r\t!?<>= ";
                if (n == 1)
                    strSeparateurs = " \n\r\t01234567890@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&éè_ç";
                string strMot = "";
                int nPos = m_contexteAnalyse.m_nPositionAnalyse;
                while (true)
                {
                    if (nPos >= m_contexteAnalyse.m_strFormule.Length)
                    {
                        strNextMots.Add(strMot);
                        break;
                    }
                    char c = m_contexteAnalyse.m_strFormule[nPos];
                    if (strSeparateurs.IndexOf(c) >= 0)
                    {
                        strNextMots.Add(strMot);
                        break;
                    }
                    strMot += c;
                    nPos++;
                }
            }
            return strNextMots.ToArray();
            //Mots pour séparateurs exclusifs
        }*/

		protected virtual bool Identifie ( String strMot, int nNiveau )
		{
			String strSeparateurs = "01234567890#.()[]\"+-:%/*,;\n\r\t!?<>= ";
			//s'il n'y a pas de texte dans le mot, ajoute
			// les lettres comme séparateurs
			bool bOnlySep = true;
			foreach ( char cTmp in strMot )
				if ( strSeparateurs.IndexOf(cTmp) < 0 )
				{
					bOnlySep = false;
					break;
				}
			if ( bOnlySep )
				strSeparateurs += "@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&éè_ç";
			/*switch(nNiveau)
				{
					case 0:
						strSeparateurs += "=<>";
						break;

					case 4:
					case 5:

						break;

					default:
						strSeparateurs += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&éè_ç";
						break;
				}*/
			int nLongFormule, nLongMot;
			String strFormule = m_contexteAnalyse.m_strFormule.Substring(m_contexteAnalyse.m_nPositionAnalyse);
			nLongFormule = strFormule.Length;
			nLongMot = strMot.Length;
			if ( nLongFormule >= nLongMot )
			{
				if ( strFormule.Substring(0,strMot.Length).ToLower(
                    CultureInfo.CurrentCulture) == strMot.ToLower(CultureInfo.CurrentCulture) )
				{
					if ( nLongMot == nLongFormule )
						return true;
					if ( strSeparateurs.IndexOf ( strFormule[nLongMot] ) >= 0 )
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="nNiveau"></param>
		/// <returns></returns>
		protected virtual IExpression GetExpression ( int nNiveau )
		{
			if ( nNiveau == 0 )
				return GetExpression0();
			IExpression expN, expNMore;
			expN = GetExpression ( nNiveau - 1 );
			if ( expN == null )
				return null;
			expNMore = GetExpressionMore ( nNiveau, expN );
			if ( expNMore != null )
				return expNMore;
			return expN;
		}

		/// <summary>
		/// Retourne une nouvelle fonction objet
		/// </summary>
		/// <param name="expressionSource">Source de l'appel</param>
		/// <param name="methodeOuPropriete">Méthode ou propriété appelée</param>
		/// <returns></returns>
		protected abstract CResultAErreur GetExpressionObjet ( IExpression expressionSource, IExpression methodeOuPropriete );

		/// <summary>
		/// Retourne une éxpression correspondant à une méthode dynamique
		/// </summary>
		/// <param name="strMethode"></param>
		/// <returns></returns>
		protected abstract CResultAErreur GetExpressionMethode ( string strMethode );

		/// ///////////////////////////////////////////////////////////
		protected virtual IExpression GetExpressionObjet ( )//IExpression expressionPrecedente )
		{
			if ( !IntegrerSyntaxeObjet )
				return null;
			CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
			if ( !(token is CTokenOperateurObjet ) )
			{
				m_contexteAnalyse.UnPop();
				return null;
			}
			//On cherche le deuxième paramètre de la méthode, objet
			//donc on ne connait pas encore le type du premier paramètre
			StartAnalyseExpressionObjet();
			IExpression expressionMethode = GetExpression(0);
			EndAnalyseExpressionObjet();
			if ( expressionMethode == null )
				return null;
			//L'expression précédente doit être un champ, une méthode ou une expression objet
			if ( !expressionMethode.CanBeArgumentExpressionObjet )
			{
                m_contexteAnalyse.EmpileErreur(I.T("The argument cannot be on the right of a '.'|30025"));
				return null;
			}
			IExpression expSource = GetExpression(0);
			if ( expSource == null )
			{
                m_contexteAnalyse.EmpileErreur(I.T("Error in the source of the object expression|30026"));
				return null;
			}
			CResultAErreur result = GetExpressionObjet ( expSource, expressionMethode );
			if ( !result )
			{
				m_contexteAnalyse.EmpileErreurs ( result.Erreur );
				m_contexteAnalyse.EmpileErreur(I.T("Error in the object expression|30027"));
				return null;
			}
			return (IExpression) result.Data;
		}

		protected abstract CResultAErreur GetExpressionChamp ( string strChamp );

		/// <summary>
		/// ///////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="nNiveau"></param>
		/// <returns></returns>
		protected virtual IExpression GetExpressionMore ( int nNiveau, IExpression expressionPrecedente )
		{
			int nOldPosPile = m_contexteAnalyse.GetPosPile();
			CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
			if ( token == null )
				return null;

            CTokenOperateur tokOp = (token as CTokenOperateur);
			if ( token.GetNiveauToken() != nNiveau || (tokOp == null))
			{
				m_contexteAnalyse.SetPosPile ( nOldPosPile );
				return null;
			}
			IExpression expNMoins, expNMore;

            if (nNiveau == int.MinValue)
                throw new ArgumentOutOfRangeException("nNiveau", "le niveau doit être supérieur à Int32.MinValue");
			expNMoins = GetExpression(nNiveau-1);
			if ( expNMoins == null )
			{
				m_contexteAnalyse.SetPosPile ( nOldPosPile );
				return null;
			}
			expNMore = GetExpressionMore ( nNiveau, expNMoins );
			if ( expNMore != null )
				expNMoins = expNMore;
			IExpression exp = AllocateurExpression.GetExpression(tokOp.Operateur.Id);
			exp.CaracteresControleAvant = tokOp.CaracteresControlesAvant;
			if ( exp == null )
			{
				m_contexteAnalyse.SetPosPile ( nOldPosPile );
				return null;
			}
			exp.Parametres.Add ( expNMoins );
			exp.Parametres.Add ( expressionPrecedente );
            CResultAErreur result = CResultAErreur.True;
            if ( m_bVerifieParametresLorsDeLanalyse )
                result = exp.VerifieParametres();
			if ( !result )
			{
				m_contexteAnalyse.EmpileErreurs(result.Erreur);
				return null;
			}
			return exp;
		}

		/// //////////////////////////////////////////////////////////////
		protected abstract CResultAErreur GetExpressionListe ( IExpression[] elements );
		/// <summary>
		/// //////////////////////////////////////////////////////////////
		/// </summary>
		/// <returns></returns>
		protected virtual IExpression GetExpression0()
		{
			CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
				//Indexeur
			if ( token is CTokenIndexeur )
			{
				try
				{
					IExpression expressionIndex = null;
					expressionIndex = GetExpression(NiveauOperateurMax);
					if ( expressionIndex == null )
					{
						m_contexteAnalyse.EmpileErreur(I.T("Error in the indexator index|30028"));
						return null;
					}
					IExpression expressionIndexee = GetExpression0();
					if ( expressionIndexee == null )
					{
                        m_contexteAnalyse.EmpileErreur(I.T("Error in the indexator field|30029"));
						return null;
					}
					IExpression expressionIndexeur = AllocateurExpression.GetExpressionIndexeur(expressionIndexee, expressionIndex);
					expressionIndexeur.CaracteresControleAvant = token.CaracteresControlesAvant;
					if ( expressionIndexeur == null )
					{
						m_contexteAnalyse.EmpileErreur(I.T("Impossible to create the indexator|30030"));
						return null;
					}
					return expressionIndexeur;
				}
				catch ( Exception e )
				{
					m_contexteAnalyse.EmpileErreur( new CErreurException(e));
					m_contexteAnalyse.EmpileErreur(I.T("Error in the indexator|30031"));
					return null;
				}
			}
			m_contexteAnalyse.UnPop();
			IExpression expressionObjet = GetExpressionObjet();
			if ( expressionObjet != null )
				return expressionObjet;
			return GetElement0();
		}

		protected virtual IExpression GetElement0()
		{
			int nOldPosPile = m_contexteAnalyse.GetPosPile();
			CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
			if ( token == null )
				return null;
			
			//CONSTANTE
            CTokenConstante tokenCst = (token as CTokenConstante);
			if ( tokenCst != null )
			{
				try
				{
					IExpression expression = this.AllocateurExpression.GetExpressionConstante(tokenCst.Valeur);
					expression.CaracteresControleAvant = token.CaracteresControlesAvant;
					return expression;
				}
				catch ( Exception e )
				{
					m_contexteAnalyse.EmpileErreur(new CErreurException(e));
					m_contexteAnalyse.EmpileErreur( new CErreurAnalyseSyntaxique(I.T("Erreur dans la constante '@1'|30019",tokenCst.Valeur.ToString()), token.PositionDansChaine));
					return null;
				}
			}

			if ( token is CTokenThis )
			{
				CResultAErreur resultThis = GetExpressionThis ( );
				if ( !resultThis )
				{
					m_contexteAnalyse.EmpileErreurs(resultThis.Erreur );
					return null;
				}
				return (IExpression)resultThis.Data;
			}

			if (token is CTokenRoot)
			{
				CResultAErreur resultRoot = GetExpressionRoot();
				if (!resultRoot)
				{
					m_contexteAnalyse.EmpileErreurs(resultRoot.Erreur);
					return null;
				}
				return (IExpression)resultRoot.Data;
			}

            CTokenChamp tokenChp = ( token as CTokenChamp );
			if (tokenChp != null)
			{
				try
				{
					CResultAErreur resultChamp = this.GetExpressionChamp ( tokenChp.NomChamp);
					if ( !resultChamp )
					{
						m_contexteAnalyse.EmpileErreurs(resultChamp.Erreur);
						m_contexteAnalyse.EmpileErreur(I.T("Error in the field '@1'|30034",tokenChp.NomChamp));
						return null;
					}
					((IExpression)resultChamp.Data).CaracteresControleAvant = token.CaracteresControlesAvant;
					return ( IExpression ) resultChamp.Data;
				}
				catch ( Exception e )
				{
					m_contexteAnalyse.EmpileErreur(new CErreurException(e));
                    m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the variable '@1'|30035",tokenChp.NomChamp), token.PositionDansChaine));
					return null;
				}
			}

			

			if ( token is CTokenAccoladeFermante )//Liste
			{
				ArrayList lstTmp = GetListeExpressionsParametresOuListe ( typeof(CTokenAccoladeOuvrante) );
				if ( lstTmp == null )
				{
					m_contexteAnalyse.EmpileErreur(I.T("Error in the list|30033"));
					return null;
				}
				CResultAErreur result = GetExpressionListe ( (IExpression[])lstTmp.ToArray(typeof(IExpression))) ;
				if ( !result )
				{
					m_contexteAnalyse.EmpileErreur(I.T("Impossible to allocate a list|30034"));
					return null;
				}
				return (IExpression)result.Data;
			}

			//////FONCTION ou méthode dynamique
			if ( token is CTokenParentheseFermante )
			{
				ArrayList lstTmp = GetListeExpressionsParametresOuListe( typeof(CTokenParentheseOuvrante) );
				if ( lstTmp == null )
				{
					m_contexteAnalyse.SetPosPile( nOldPosPile );
					return null;
				}
				//Récupère la fonction
				token = m_contexteAnalyse.PopToken();
				IExpression expression = null;
				if ( token is CTokenFonctionParentheses )
				{
					expression = AllocateurExpression.GetExpressionParentheses();
					expression.CaracteresControleAvant = token.CaracteresControlesAvant;
				}
				else
				{
                    CTokenMethodeDynamique tokenMthdDyn = (token as CTokenMethodeDynamique);
					if ( tokenMthdDyn != null )
					{
						CResultAErreur resultMeth = GetExpressionMethode ( tokenMthdDyn.NomMethode );
						if (!resultMeth)
						{
							m_contexteAnalyse.EmpileErreur (I.T("Error in the allocation of the method @1|30035",tokenMthdDyn.NomMethode));
							return null;
						}
						expression= (IExpression)resultMeth.Data;
					}
					else
					{
                        CTokenOperateur tokenOp = (token as CTokenOperateur);
						if (tokenOp == null)
						{
							m_contexteAnalyse.SetPosPile ( nOldPosPile );
							m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the formula|30014"),token.PositionDansChaine));
							return null;
						}
						//Vérifie que le nombre de paramètres est correct
						expression = AllocateurExpression.GetExpression(tokenOp.Operateur.Id);
						expression.CaracteresControleAvant = token.CaracteresControlesAvant;
						COperateurAnalysable operateur = tokenOp.Operateur;
						if ( expression == null )
						{
							m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the allocation of the function '@1' |30036",operateur.Texte), token.PositionDansChaine));
							return null;
						}
						if ( expression == null )
						{
							m_contexteAnalyse.SetPosPile ( nOldPosPile );
							m_contexteAnalyse.EmpileErreur(I.T("Error in the allocation of the function '@1' |30036",operateur.Texte));
							return null;
						}
					}
				

					//Si le nombre de paramètre est négatif, l'expression a un nombre de paramètres variables
					if ( expression.GetNbParametresNecessaires() >= 0 &&  lstTmp.Count != expression.GetNbParametresNecessaires() )
					{
						m_contexteAnalyse.SetPosPile ( nOldPosPile );
						m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Incorrect number of parameters for the function '@1'|30037",expression.ToString()), token.PositionDansChaine));
						return null;
					}
				}
				int nParametre, nNbParametres = lstTmp.Count;
				expression.Parametres.Clear();
				for ( nParametre = 0; nParametre < nNbParametres; nParametre++ )
					expression.Parametres.Add ( lstTmp[nParametre] );
				CResultAErreur result = CResultAErreur.True;
					result = expression.VerifieParametres();

				result = expression.AfterAnalyse ( this );
				
				if ( !result )
				{
					m_contexteAnalyse.EmpileErreurs(result.Erreur);
					return null;
				}
				return expression;
			}
			return null;
		}

		/// <summary>
		/// //////////////////////////////////////////////////////////////
		/// </summary>
		/// <returns></returns>
		protected virtual ArrayList GetListeExpressionsParametresOuListe( Type typeTokenOuvrant)
		{
			int nOldPosPile = m_contexteAnalyse.GetPosPile();
			ArrayList lstTmp = new ArrayList();
			IExpression exp = null;
			while ( true )
			{
				CTokenAnalyseSyntaxique token = m_contexteAnalyse.PopToken();
				if ( token == null )
				{
					m_contexteAnalyse.SetPosPile ( nOldPosPile );
					return null;
				}
				if ( token.GetType() == typeTokenOuvrant )
					return lstTmp;
				m_contexteAnalyse.UnPop();
				exp = GetExpression(NiveauOperateurMax);
				if ( exp == null )
				{
					m_contexteAnalyse.SetPosPile ( nOldPosPile );
					m_contexteAnalyse.EmpileErreur(new CErreurAnalyseSyntaxique(I.T("Error in the parameters or the list|30038"), token.PositionDansChaine));
					return null;
				}
				lstTmp.Insert ( 0, exp );
			}
		}


	}
}
