using System;
using System.Collections;

using sc2i.common;
using sc2i.expression.Debug;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	[AutoExec("RegisterAllInAssembly")]
	[Serializable]
	public abstract class C2iExpression : IExpression, I2iSerializable
	{
		/// ///////////////////////////////////////////////////////////
		private ArrayList m_listeParametres = new ArrayList();

		//Contient les caractères de contrôle qui étaient présent lorsque cette expression a été analysée.
		private string m_strCaracteresControlesAvantString = "";


		/// ///////////////////////////////////////////////////////////
		public C2iExpression()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public static void RegisterAllInAssembly()
		{
			foreach ( Type tp in typeof(C2iExpression).Assembly.GetTypes() )
			{
				if ( tp.IsSubclassOf(typeof(C2iExpression)) && !tp.IsAbstract) 
				{
#if PDA
					C2iExpression exp = (C2iExpression)Activator.CreateInstance(tp);
#else
					C2iExpression exp = (C2iExpression)Activator.CreateInstance(tp, new object[0]);
#endif
					CAllocateur2iExpression.Register2iExpression(exp.IdExpression, tp );
				}
			}
		}

		/// ///////////////////////////////////////////////////////////
		public virtual bool CanBeArgumentExpressionObjet
		{
			get
			{
				return false;
			}
		}

		
		/// <summary>
		/// Retourne l'identifiant de l'expression
		/// </summary>
		public abstract string IdExpression{get;}

		public abstract CTypeResultatExpression TypeDonnee{get;}

		/// ///////////////////////////////////////////////////////////
		public virtual CObjetPourSousProprietes GetObjetPourSousProprietes()
		{
			return new CObjetPourSousProprietes(TypeDonnee);
		}


		/// ///////////////////////////////////////////////////////////
		/// lstResults contient la liste des valeurs des paramètres
		protected CResultAErreur EvalParametres ( CContexteEvaluationExpression ctx, ArrayList lstResults )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( IsSourceParametreBase )//La source est la base !!
					ctx.PushObjetSource ( ctx.ObjetBase, false );
				lstResults.Clear();
				result = CResultAErreur.True;
				if ( GetNbParametresNecessaires() >=0 &&  Parametres.Count != GetNbParametresNecessaires() )
				{
					result.EmpileErreur(I.T("Incorrect parameter number :@1 parameters expected|101",GetNbParametresNecessaires().ToString()));
					return result;
				}
				int nParametre = 0;
				foreach ( C2iExpression expression in Parametres2i )
				{
					result = expression.Eval ( ctx );
					nParametre++;
					if ( !result )
					{
						result.EmpileErreur(I.T("Error during the @1 parameter evaluation|102",expression.GetString()));
						return result;
					}
					lstResults.Add ( result.Data );
				}
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			finally
			{
				if ( IsSourceParametreBase )
					ctx.PopObjetSource ( false );
			}
			return result;
		}
		
		/// ///////////////////////////////////////////////////////////
		
		/// <summary>
		/// Le data du result a erreur contient le résultat de l'évaluation
		/// </summary>
		protected abstract CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx );

        /// ///////////////////////////////////////////////////////////
        [DynamicMethod("Evaluate formula","source object")]
        public object DynamicEval(object source)
        {
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(source);
            CResultAErreur result = Eval(ctx);
            if (result)
                return result.Data;
            return null;
        }

		/// ///////////////////////////////////////////////////////////
		public CResultAErreur Eval ( CContexteEvaluationExpression ctx )
		{
			bool bOldEnableCache = ctx.CacheEnabled;
			ctx.CacheEnabled = true;
			CResultAErreur result=  CResultAErreur.True;

            bool bStopAfter = ctx.BeforeEval(this);

			try
			{
				result = ProtectedEval ( ctx );
                if ( bStopAfter || !result)
                    ctx.AfterEval(this, result);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Error during the @1 formula evaluation|103",GetString()));
			}
				
			ctx.CacheEnabled = bOldEnableCache;
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		/// //Si vrai, la source des paramètres n'est pas l'objet source
		/// en cours du contexte, mais l'objet de base
		public virtual bool IsSourceParametreBase
		{
			get
			{
				return false;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public abstract int GetNbParametresNecessaires();

		/// ///////////////////////////////////////////////////////////
		public ArrayList Parametres
		{
			get
			{
				return m_listeParametres;
			}
		}

        /// ///////////////////////////////////////////////////////////
        public void SetParametre(int nParametre, C2iExpression parametre)
        {
            while (nParametre >= m_listeParametres.Count)
                m_listeParametres.Add(null);
            m_listeParametres[nParametre] = parametre;
        }                

		/// ///////////////////////////////////////////////////////////
		public C2iExpression[] Parametres2i
		{
			get
			{
				return (C2iExpression[])m_listeParametres.ToArray(typeof(C2iExpression));
			}
		}

		/// ///////////////////////////////////////////////////////////
		public ArrayList ExtractExpressionsType ( Type tp )
		{
			ArrayList lst = new ArrayList();
			if ( GetType().IsSubclassOf(tp) || GetType()==tp )
				lst.Add ( this );
			FillListeWithParametresType ( tp, lst );
			return lst;
		}

		/// ///////////////////////////////////////////////////////////
		private void FillListeWithParametresType ( Type tp, ArrayList lst )
		{
			if ( Parametres == null )
				return;
			foreach ( IExpression exp in Parametres )
			{
				if ( exp.GetType().IsSubclassOf ( tp ) || exp.GetType() == tp )
					lst.Add ( exp );
				if ( exp is C2iExpression )
					((C2iExpression)exp).FillListeWithParametresType ( tp, lst );
			}
		}
		
		/// ///////////////////////////////////////////////////////////
		public abstract string GetString();

		/// ///////////////////////////////////////////////////////////
		public abstract CResultAErreur VerifieParametres();

		///////////////////////////////////////////////////
		protected string GetPseudoCode()
		{
			return GetPseudoCode(this);
		}

		///////////////////////////////////////////////////
		public static string GetPseudoCode( C2iExpression exp )
		{
			if ( exp == null )
				return "";
			CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture);
			exp.GetPseudoCode(ser);
			string strRetour = new CStringSerializerCompresseur().Compress ( ser.String );
			return strRetour;
		}

		///////////////////////////////////////////////////
		public virtual void GetPseudoCode ( C2iSerializer serializer )
		{
			string strId = IdExpression;
			serializer.TraiteString(ref strId);
			this.Serialize ( serializer );
		}


		///////////////////////////////////////////////////
		public static C2iExpression FromPseudoCode ( string strCode )
		{
			strCode = new CStringSerializerCompresseur().UnCompress ( strCode );
			if(  strCode == null || strCode.Trim() == "" )
				return null;
			return FromPseudoCode ( new CStringSerializer(strCode, ModeSerialisation.Lecture) );
		}

		///////////////////////////////////////////////////
		protected static C2iExpression FromPseudoCode (C2iSerializer serializer )
		{
			string strType = "";
			try
			{
				serializer.TraiteString ( ref strType );
				C2iExpression exp = (C2iExpression)new CAllocateur2iExpression().GetExpression( strType );
				if ( exp == null )
					return null;
				if ( !exp.Serialize ( serializer ) )
					return null;
				return exp;
			}
			catch
			{
				return null;
			}
			
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//1 : Ajout des caractères de contrôle
		}

		/// ///////////////////////////////////////////////////////////
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			int nNb = Parametres.Count;
			serializer.TraiteInt ( ref nNb );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach (C2iExpression expression in Parametres2i )
						expression.GetPseudoCode ( serializer );
					break;
				case ModeSerialisation.Lecture :
					m_listeParametres.Clear();
					for ( int n = 0; n < nNb; n++ )
					{
						C2iExpression expression = FromPseudoCode ( serializer );
						m_listeParametres.Add ( expression );
					}
					break;
			}
            if (nVersion >= 1)
            {
                serializer.TraiteString(ref m_strCaracteresControlesAvantString);
                if (m_strCaracteresControlesAvantString.Length > 3)
                    m_strCaracteresControlesAvantString = m_strCaracteresControlesAvantString.Substring(0, 3);
            }
			return result;
		}

		/*/// ///////////////////////////////////////////////////////////
		public virtual void Write ( CStringSerializer serializer )
		{
			serializer.WriteVersion ( GetNumVersion() );
			serializer.WriteInt ( Parametres.Count );
			foreach ( C2iExpression exp in Parametres2i )
				exp.GetPseudoCode ( serializer );
		}

		/// ///////////////////////////////////////////////////////////
		public virtual CResultAErreur Read ( CStringSerializer serializer )
		{
			int nVersion=  GetNumVersion();
			CResultAErreur result = serializer.ReadVerifieVersion  ( GetNumVersion(),ref nVersion );
			if ( !result )
				return result;
			int nNbParametres = serializer.ReadInt ( );
			Parametres.Clear();
			for ( int n = 0; n< nNbParametres; n++ )
			{
				C2iExpression param = C2iExpression.FromPseudoCode(serializer);
				if ( param == null )
				{
					result.EmpileErreur("Erreur lors de la lecture");
					return result;
				}
				Parametres.Add ( param );
			}
			return result;
		}*/

		/// //////////////////////////////////////////////////////////////////////////////////
		///Redéfinit le type d'objet de référence pour les champs et les méthodes
		public virtual CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur )
		{
			CResultAErreur result =CResultAErreur.True;
			foreach ( C2iExpression expression in Parametres )
			{
				if ( expression != null )
					result = expression.SetTypeObjetInterroge(objetPourSousProprietes, fournisseur);
				if ( !result )
					return result;
			}
			return result;
		}


		/// //////////////////////////////////////////////////////////////////////////////////
		public virtual CArbreDefinitionsDynamiques GetArbreProprietesAccedees ( CArbreDefinitionsDynamiques arbreEnCours )
		{
			foreach ( C2iExpression exp in Parametres )
				exp.GetArbreProprietesAccedees ( arbreEnCours );
			return arbreEnCours;
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public string CaracteresControleAvant
		{
			get
			{
				return m_strCaracteresControlesAvantString;
			}
			set
			{
				m_strCaracteresControlesAvantString = value;
                if (m_strCaracteresControlesAvantString.Length > 3)
                    m_strCaracteresControlesAvantString = m_strCaracteresControlesAvantString.Substring(0, 3);
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur AfterAnalyse ( CAnalyseurSyntaxique analyseur )
		{
			return CResultAErreur.True;
		}

	}
}

		

