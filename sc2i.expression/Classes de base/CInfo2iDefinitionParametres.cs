using System;
using sc2i.expression;
using System.Collections.Generic;
using sc2i.common;

namespace sc2i.expression
{
    [Serializable]
    public class CInfoUnParametreExpression
    {
        public string NomParametre{get;set;}
        public CTypeResultatExpression TypeDonnee{get;set;}
        private bool m_IsSortieSequence = false;

        //-------------------------------------------------------------------------
        public CInfoUnParametreExpression ( string strNom, CTypeResultatExpression typeDonnee )
        {
            NomParametre = strNom;
            TypeDonnee = typeDonnee;
        }

        //-------------------------------------------------------------------------
        public CInfoUnParametreExpression(string strNom, CTypeResultatExpression typeDonnee, bool bIsSortieSequence)
        {
            NomParametre = strNom;
            TypeDonnee = typeDonnee;
            IsSortieSequence = bIsSortieSequence;
        }

        //-------------------------------------------------------------------------
        public CInfoUnParametreExpression ( string strNom, Type tp )
        {
            
            NomParametre = strNom;
            TypeDonnee = new CTypeResultatExpression ( tp, false );
        }

        //-------------------------------------------------------------------------
        public CInfoUnParametreExpression(string strNom, Type tp, bool bIsSortieSequence)
        {

            NomParametre = strNom;
            TypeDonnee = new CTypeResultatExpression(tp, false);
            IsSortieSequence = bIsSortieSequence;
        }

        //-------------------------------------------------------------------------
        public bool IsSortieSequence
        {
            get
            {
                return m_IsSortieSequence;
            }
            set
            {
                m_IsSortieSequence = value;
            }
        }
    }

	/// <summary>
	/// Description résumée de CInfoParametre2iExpression.
	/// </summary>
	[Serializable]
	public class CInfo2iDefinitionParametres
	{
        private List<CInfoUnParametreExpression> m_listeInfosParametres = new  List<CInfoUnParametreExpression>();
        private bool m_bLastParametreIsMultiple = false;

		
		//-------------------------------------------------------------------------	
		public CInfo2iDefinitionParametres ( params Type[] types )
		{
            int nIndex = 1;
            foreach ( Type tp in types )
            {
                m_listeInfosParametres.Add ( new CInfoUnParametreExpression(I.T ( "Parameter @1|20036", nIndex.ToString() ), tp ) );
                nIndex++;
            }
		}

		//-------------------------------------------------------------------------
        public CInfo2iDefinitionParametres ( params CTypeResultatExpression[] types )
		{
			int nIndex = 1;
            foreach ( CTypeResultatExpression tp in types )
            {
                m_listeInfosParametres.Add ( new CInfoUnParametreExpression(I.T ( "Parameter @1|20036", nIndex.ToString() ), tp ) );
                nIndex++;
            }
		}

        //-------------------------------------------------------------------------	
        public CInfo2iDefinitionParametres(params CInfoUnParametreExpression[] infos)
        {
            m_listeInfosParametres.AddRange(infos);
        }

        //-------------------------------------------------------------------------	
        public CInfo2iDefinitionParametres(bool bLastParamIsMultiple, params CInfoUnParametreExpression[] infos)
        {
            m_bLastParametreIsMultiple = bLastParamIsMultiple;
            m_listeInfosParametres.AddRange(infos);
        }

        //-------------------------------------------------------------------------
        public CTypeResultatExpression[] TypesDonnees
        {
            get{
                List<CTypeResultatExpression> types = new List<CTypeResultatExpression>();
                foreach ( CInfoUnParametreExpression info in m_listeInfosParametres )
                    types.Add ( info.TypeDonnee );
                return types.ToArray();
            }
        }

        //-------------------------------------------------------------------------
        public IEnumerable<CInfoUnParametreExpression> InfosParametres
        {
            get {
                return m_listeInfosParametres.AsReadOnly();
            }
        }

        //-------------------------------------------------------------------------	
        /// <summary>
        /// Si true, cette définition indique que le dernier type de parametre
        /// peut être mutliple (à la manière du params de C#)
        /// Dans le cas contraire, le nombre de paramètre est définition par la taille
        /// du tableau InfosParametres
        /// </summary>
        public bool LastParametreIsMultiple
        {
            get
            {
                return m_bLastParametreIsMultiple;
            }
        }

	}
}
