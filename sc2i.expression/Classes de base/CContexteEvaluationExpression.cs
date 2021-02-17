using System;
using System.Collections;
using sc2i.common;
using System.Collections.Generic;
using sc2i.expression.Debug;
using System.Threading;

namespace sc2i.expression
{
	public interface IOldInterpreteurProprieteDynamique
	{
		object GetValue(object objet, CDefinitionProprieteDynamique propriete);
		object GetValue(object objet, string strProprieteComplete);
	}
	/// <summary>
	/// Description résumée de CContexteEvaluationExpression.
	/// </summary>
	
	[Serializable]
	public class CContexteEvaluationExpression : IDisposable
	{
		public delegate void OnNewContexteEvaluationExpressionDelegate( CContexteEvaluationExpression contexte);

        private Dictionary<string, IOptimiseurGetValueDynamic> m_dicOptimiseurs = new Dictionary<string, IOptimiseurGetValueDynamic>();

		private static IOldInterpreteurProprieteDynamique m_oldInterpreteur = null;

        private bool m_bUseOptimiseurs = false;

		//Objet source en cours pour les paramètres des expressions
		//ayant comme source le niveau objet
		private Stack m_pileObjetsSource = new Stack();

		//Objet de base en cours. L'objet source change dans les fonctions objet
		//alors que l'objet de base change dans des expression de type select.
		//Il est utilisé par les paramètres des expression ayant comme source le niveau base
		//je sais, c'est pas très clair, mais c'est vrai !
		/*Cette notion est nécéssaire pour les méthodes objet. Par exemple, la méthode
		 * select travaille avec comme base le type de la liste source qui appelle
		 * la méthode. Alors qu'un méthode dynamique doit avoir comme base la
		 * base de l'appel aux expressions
		 */
		private Stack m_pileObjetsBase = new Stack();

		private CCacheValeursProprietes m_cache = new CCacheValeursProprietes();

		public static void SetOldInterpreteurProprieteDynamique(IOldInterpreteurProprieteDynamique oldInterpreteur)
		{
			m_oldInterpreteur = oldInterpreteur;
		}

		//Nom de variable->CDefinitionProprietesDynamiqueVariable
		private Hashtable m_tableVariables = new Hashtable();

		//Nom du champ->Valeur
		private Hashtable m_tableValeursVariables = new Hashtable();

        private Dictionary<Type, Stack<object>> m_dicObjetsAttaches = new Dictionary<Type, Stack<object>>();

		public static OnNewContexteEvaluationExpressionDelegate OnNewContexteEvaluation;

        private CDebuggerFormule m_debugger = null;

        private SynchronizationContext m_syncContext = null;

		/// /////////////////////////////////////////////////////////
		public CContexteEvaluationExpression(object objetSource, CCacheValeursProprietes cacheValeurs)
		{
			PushObjetSource(objetSource, true);
			if (OnNewContexteEvaluation != null)
				OnNewContexteEvaluation(this);
			if (cacheValeurs != null)
				m_cache = cacheValeurs;
			IAttacheurObjetsAContexteEvaluationExpression attacheur = objetSource as IAttacheurObjetsAContexteEvaluationExpression;
			if ( attacheur != null )
				attacheur.AttacheObjetsAContexteEvaluation ( this );
            
		}


        /// /////////////////////////////////////////////////////////
        public SynchronizationContext SynchronizationContext
        {
            get
            {
                return m_syncContext;
            }
            set
            {
                m_syncContext = value;
            }
        }


        /// /////////////////////////////////////////////////////////
        private CContexteEvaluationExpression()
        {
        }

        /// /////////////////////////////////////////////////////////
        public CDebuggerFormule Debugger
        {
            get
            {
                return m_debugger;
            }
            set
            {
                m_debugger = value;
            }
        }


        /// /////////////////////////////////////////////////////////
        public bool BeforeEval(C2iExpression formule)
        {
            if (m_debugger != null)
            {
                return m_debugger.BeforeEval(formule);
            }
            return false;
        }

        /// /////////////////////////////////////////////////////////
        public void AfterEval(C2iExpression formule, CResultAErreur result)
        {
            if (m_debugger != null)
            {
                m_debugger.AfterEval(formule, result);
            }

        }

        /// /////////////////////////////////////////////////////////
        public bool UseOptimiseurs
        {
            get{
                return m_bUseOptimiseurs;
            }
            set{
                m_bUseOptimiseurs = value;
            }
        }
		
		/// /////////////////////////////////////////////////////////
		public CContexteEvaluationExpression( object objetSource )
		{
            ChangeSource(objetSource);
            if (OnNewContexteEvaluation != null)
                OnNewContexteEvaluation(this);

        }

        public void ChangeSource ( object objetSource )
        {
            m_pileObjetsBase.Clear();
            m_dicObjetsAttaches.Clear();
            if (OnNewContexteEvaluation != null)
                OnNewContexteEvaluation(this);
			PushObjetSource ( objetSource, true );
			IAttacheurObjetsAContexteEvaluationExpression attacheur = objetSource as IAttacheurObjetsAContexteEvaluationExpression;
			if (attacheur != null)
				attacheur.AttacheObjetsAContexteEvaluation(this); 
		}

		/// /////////////////////////////////////////////////////////
		public CCacheValeursProprietes Cache
		{
			get
			{
				return m_cache;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool CacheEnabled
		{
			get
			{
				return m_cache.CacheEnabled;
			}
			set
			{
				m_cache.CacheEnabled = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public void Dispose()
		{
            ///Oulala, ne surtout pas disposer : en général, on attache des contextes de
            ///donnée, si on le dispose, ça fout le bronx !
            ///Stef /8/12/2011
			/*ArrayList lst = new ArrayList( m_dicObjetsAttaches );
			foreach ( KeyValuePair<Type, Stack<object>> entry in lst )
			{
				try
				{
					Stack<object> objs = entry.Value;
                    while ( objs.Count() > 0 )
                    {
                        object obj = objs.Pop();
                        if ( obj is IDisposable )
                            obj.Dispos
					if ( obj is IDisposable )
						((IDisposable)obj).Dispose();
					m_tableObjetsAttaches.Remove ( entry.Key );
				}
				catch{}
			}
			m_tableObjetsAttaches.Clear();*/
		}

		/// /////////////////////////////////////////////////////////
		///Accède à l'objet source. Si c'est un multi source, retourne
        ///l'objet principal du multisource
        public object ObjetSource
		{
			get
			{
                if (m_pileObjetsSource.Count >= 0)
                {
                    object valeur = m_pileObjetsSource.Peek();
                    CDefinitionMultiSourceForExpression def = valeur as CDefinitionMultiSourceForExpression;
                    if (def != null)
                        valeur = def.ObjetPrincipal;
                    return valeur;
                }
				return null;
			}
		}

        /// /////////////////////////////////////////////////////////
        ///Permet d'accéder au multisource si la racine l'objet est un 
        ///multi source, sinon, permet d'accéder à l'élément en cours
        public object ObjetSourceOuMultiSource
        {
            get
            {
                if (m_pileObjetsSource.Count >= 0)
                    return m_pileObjetsSource.Peek();
                return null;
            }
        }


		/// /////////////////////////////////////////////////////////
		public void PushObjetSource ( object newSource, bool bNouvelleBase )
		{
			m_pileObjetsSource.Push ( newSource );
			if ( bNouvelleBase )
				PushObjetBase ( newSource );
		}

		/// /////////////////////////////////////////////////////////
		public object PopObjetSource ( bool bIsBase )
		{
			if ( bIsBase )
				PopObjetBase();
			return m_pileObjetsSource.Pop ();
		}

		/// /////////////////////////////////////////////////////////
		public object ObjetBase
		{
			get
			{
				if ( m_pileObjetsBase.Count >= 0 )
					return m_pileObjetsBase.Peek();
				return null;
			}
		}


		/// /////////////////////////////////////////////////////////
		public object ObjetBaseRacine
		{
			get
			{
				if (m_pileObjetsBase.Count >= 0)
					return m_pileObjetsBase.ToArray()[m_pileObjetsBase.Count - 1];
				return null;
			}
		}

		/// /////////////////////////////////////////////////////////
		protected void PushObjetBase ( object newSource )
		{
			m_pileObjetsBase.Push ( newSource );
		}

		/// /////////////////////////////////////////////////////////
		protected object PopObjetBase ( )
		{
			return m_pileObjetsBase.Pop ();
		}
		
		/// /////////////////////////////////////////////////////////
		public object GetValeurChamp ( CDefinitionProprieteDynamique propriete )
		{
			return GetValeurChamp ( ObjetSourceOuMultiSource, propriete );
		}

		/// /////////////////////////////////////////////////////////
		protected object GetValeurChamp ( object source, CDefinitionProprieteDynamique propriete )
		{
			CDefinitionProprieteDynamiqueVariableFormule defFormule = propriete as CDefinitionProprieteDynamiqueVariableFormule;
			if ( defFormule != null )
				return GetValeurVariable ( defFormule );
            if (source != null && m_bUseOptimiseurs)
            {
                Type tp = source.GetType();
                string strKey = tp.ToString() + "/" + propriete.NomPropriete;
                IOptimiseurGetValueDynamic optimiseur = null;
                bool bDejaCherche = false;
                if (m_dicOptimiseurs.TryGetValue(strKey, out optimiseur))
                {
                    bDejaCherche = true;
                }
                if (!bDejaCherche)
                {
                    optimiseur = CInterpreteurProprieteDynamique.GetOptimiseur(tp, propriete.NomPropriete);
                    m_dicOptimiseurs[strKey] = optimiseur;
                }
                if (optimiseur != null)
                    return optimiseur.GetValue(source);
            }
			CResultAErreur result = CInterpreteurProprieteDynamique.GetValue(source, propriete, m_cache);
			if (result)
				return result.Data;
			if (m_oldInterpreteur != null)
				return m_oldInterpreteur.GetValue(source, propriete);
			return null;
		}

		/// /////////////////////////////////////////////////////////
		public void AttacheObjet ( Type tp, object obj )
		{
            Stack<object> st = null;
            if (!m_dicObjetsAttaches.TryGetValue(tp, out st))
            {
                st = new Stack<object>();
                m_dicObjetsAttaches[tp] = st;
            }
            st.Push(obj);
		}

		/// /////////////////////////////////////////////////////////
		public void DetacheObjet ( Type tp )
		{
            Stack<object> st = null;
            if (m_dicObjetsAttaches.TryGetValue(tp, out st))
                if (st.Count > 0)
                    st.Pop();
        }

		/// /////////////////////////////////////////////////////////
		public object GetObjetAttache (Type tp)
		{
            Stack<object> st = null;
            if (m_dicObjetsAttaches.TryGetValue(tp, out st))
            {
                if (st.Count > 0)
                    return st.Peek();
            }
            return null;
		}


		/// /////////////////////////////////////////////////////////
		public void AddVariable ( CDefinitionProprieteDynamiqueVariableFormule def )
		{
			m_tableVariables[def.Nom.ToUpper()] = def;
		}

		public object GetValeurVariable ( CDefinitionProprieteDynamiqueVariableFormule def )
		{
			return m_tableValeursVariables[def.Nom.ToUpper()];
		}

		public void SetValeurVariable ( CDefinitionProprieteDynamique defProp, object valeur )
		{
			CDefinitionProprieteDynamiqueVariableFormule defFormule = defProp as CDefinitionProprieteDynamiqueVariableFormule;
			if ( defFormule != null )
			{
				m_tableValeursVariables[defFormule.Nom.ToUpper()] = valeur;
			}
			
		}

        /// /////////////////////////////////////////////////////////
        public CContexteEvaluationExpression GetCopie()
        {
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression();
            ctx.m_bUseOptimiseurs = m_bUseOptimiseurs;
            foreach (object obj in m_pileObjetsSource)
                ctx.m_pileObjetsSource.Push(obj);
            foreach (object obj in m_pileObjetsBase)
                ctx.m_pileObjetsBase.Push(obj);
            foreach (DictionaryEntry et in m_tableValeursVariables)
                ctx.m_tableValeursVariables.Add(et.Key, et.Value);
            ctx.m_syncContext = m_syncContext;
            return ctx;
        }







	}
}
