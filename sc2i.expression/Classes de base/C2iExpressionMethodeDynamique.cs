using System;
using System.Reflection;
using System.Linq;
using sc2i.common;
using sc2i.expression.FonctionsDynamiques;
using System.Collections.Generic;

namespace sc2i.expression
{
	/// <summary>
	/// Appel dynamique d'une méthode d'un objet, où à une fonction dynamique d'un objet
	/// </summary>
	[Serializable]
	public class C2iExpressionMethodeDynamique : C2iExpressionObjetNeedTypeParent
	{
		//Contient la liste des proprietés amenant à la méthode
		private CDefinitionMethodeDynamique m_definitionMethode = null;
        private CDefinitionFonctionDynamique m_definitionFonction = null;

        private string m_strNomMethode = "";//Utilisé uniquement pendant l'analyse, avant de connaitre le type exact de l'objet appelant

        private CFonctionDynamique m_fonction = null;//Non serializé, retrouvé dans la base des fonctions dynamiques
        


		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionMethodeDynamique()
		{
		}

        /// //////////////////////////////////// /////////////////////////////////
        ///Utilisé par l'analyse syntaxique uniquement
        public C2iExpressionMethodeDynamique(string strNomMethode)
        {
            m_strNomMethode = strNomMethode;
        }

        /// //////////////////////////////////// /////////////////////////////////
        public C2iExpressionMethodeDynamique(CDefinitionFonctionDynamique definitionFonction)
        {
            if (definitionFonction != null)
            {
                m_definitionFonction = definitionFonction;
                m_strNomMethode = definitionFonction.NomProprieteSansCleTypeChamp;
            }
        }

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionMethodeDynamique( CDefinitionMethodeDynamique DefinitionMethode )
		{
			m_definitionMethode = DefinitionMethode;
            if (DefinitionMethode != null)
                m_strNomMethode = DefinitionMethode.NomProprieteSansCleTypeChamp;
		}


		/// //////////////////////////////////// /////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = new CInfo2iExpression(0, "DynamicMethod", typeof(object), "", "");
            if (DefinitionMethode != null)
                info.Texte = DefinitionMethode.NomProprieteSansCleTypeChamp;
            else if (DefinitionFonction != null)
                info.Texte = DefinitionFonction.NomProprieteSansCleTypeChamp;
            return info;
        }

        
        /// //////////////////////////////////// /////////////////////////////////
        public CFonctionDynamique FonctionDynamique
        {
            get
            {
                if (m_fonction == null && m_definitionFonction != null)
                {
                    CFonctionDynamique fonction = CFournisseurFonctionsDynamiques.GetFonctionGlobale(m_definitionFonction.IdFonction);
                    if (fonction != null)
                        m_definitionFonction = new CDefinitionFonctionDynamique(fonction);
                    m_fonction = fonction;
                }
                return m_fonction;
            }
        }

        /// //////////////////////////////////// /////////////////////////////////
        protected override CInfo2iExpression  GetInfosSansCache()
		{
            return GetInfos();
		}

		/// //////////////////////////////////// /////////////////////////////////
		public CDefinitionMethodeDynamique DefinitionMethode
		{
			get
			{
				return m_definitionMethode;
			}
		}

        /// //////////////////////////////////// /////////////////////////////////
        public CDefinitionFonctionDynamique DefinitionFonction
        {
            get
            {
                return m_definitionFonction;
            }
        }


		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
                if (m_definitionMethode != null)
                    return m_definitionMethode.TypeDonnee;
                if (m_definitionFonction != null)
                    return m_definitionFonction.TypeDonnee;
                return new CTypeResultatExpression(typeof(object), false);
			}
		}

        /// //////////////////////////////////////////
        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
		{
            return null;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression[] TypesObjetSourceAttendu
		{
			get
			{
				return new CTypeResultatExpression[]
					{
						new CTypeResultatExpression(typeof(object), false)
					};
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "MTHDYN";
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
            CResultAErreur result = base.SetTypeObjetInterroge(objetPourProprietes, fournisseur);
            if (!result)
                return result;
			ObjetPourAnalyseSourceConnu = objetPourProprietes;
			if ( objetPourProprietes == null )
				return result;
            if ( DefinitionMethode != null )
                return result;
            if (DefinitionFonction != null)
                return result;
            string strNomUpper = m_strNomMethode.ToUpper();
			foreach ( CDefinitionProprieteDynamique prop in fournisseur.GetDefinitionsChamps ( objetPourProprietes) )
			{
				if ( prop is CDefinitionMethodeDynamique )
				{
					if ( prop.NomProprieteSansCleTypeChamp.ToUpper() == strNomUpper ||
						prop.Nom.ToUpper() == strNomUpper )
					{
						m_definitionMethode = (CDefinitionMethodeDynamique)prop;
						return result;
					}
				}
                if (prop is CDefinitionFonctionDynamique)
                {
                    if (prop.NomProprieteSansCleTypeChamp.ToUpper() == strNomUpper ||
                        prop.Nom.ToUpper() == strNomUpper)
                    {
                        m_definitionFonction = (CDefinitionFonctionDynamique)prop;
                        return result;
                    }
                }
			}
			result.EmpileErreur(I.T("The @1 method doesn't exit in the @2 type|120",m_strNomMethode,
                objetPourProprietes != null? objetPourProprietes.ToString():"null"));

			return result;
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
            if (m_definitionMethode != null)
            {
                if (ObjetPourAnalyseSourceConnu == null)
                    return result;
                CInfoMethodeDynamique info = new CInfoMethodeDynamique(ObjetPourAnalyseSourceConnu.TypeResultatExpression.TypeDotNetNatif, DefinitionMethode.NomProprieteSansCleTypeChamp);
                CInfoParametreMethodeDynamique[] parametres = info.InfosParametres;
                if (parametres.Length != Parametres.Count)
                {
                    result.EmpileErreur(I.T("The number parameters @1 method is incorrect|121", DefinitionMethode.NomProprieteSansCleTypeChamp));
                    return result;
                }
                int nParam = 0;
                foreach (CInfoParametreMethodeDynamique defPar in parametres)
                {
                    C2iExpression expression = Parametres2i[nParam];
                    if (expression != null && expression.TypeDonnee == null)
                    {
                        result.EmpileErreur(I.T("the @1 parameter is invalide in method @2|20125",
                            expression.GetString(),
                            DefinitionMethode.NomProprieteSansCleTypeChamp));
                    }
                    else if (expression == null ||
                        expression.GetType() != typeof(C2iExpressionNull) &&
                        !expression.TypeDonnee.CanConvertTo(new CTypeResultatExpression(defPar.TypeParametre, false)))
                    {
                        result.EmpileErreur(I.T("The @1 method parameters doesn't correspond|122", DefinitionMethode.NomProprieteSansCleTypeChamp));
                        break;
                    }
                    nParam++;
                }
            }
            else if (DefinitionFonction != null)
            {
                IEnumerable<CParametreFonctionDynamique> parametres = DefinitionFonction.ParametresDeLaFonction;
                if (parametres.Count() != Parametres.Count)
                {
                    result.EmpileErreur(I.T("The number parameters @1 method is incorrect|121", DefinitionFonction.NomProprieteSansCleTypeChamp));
                    return result;
                }
                int nParam = 0;
                foreach (CParametreFonctionDynamique defPar in parametres)
                {
                    C2iExpression expression = Parametres2i[nParam];
                    if (expression != null && expression.TypeDonnee == null)
                    {
                        result.EmpileErreur(I.T("the @1 parameter is invalide in method @2|20125",
                            expression.GetString(),
                            defPar.Nom));
                    }
                    else if (expression == null ||
                        expression.GetType() != typeof(C2iExpressionNull) &&
                        !expression.TypeDonnee.CanConvertTo(defPar.TypeResultatExpression))
                    {
                        result.EmpileErreur(I.T("The @1 method parameters doesn't correspond|122", DefinitionFonction.NomProprieteSansCleTypeChamp));
                        break;
                    }
                    nParam++;
                }
            }
            else if ( m_strNomMethode.Length > 0 )
                result.EmpileErreur(I.T("Unknown method @1|20126", m_strNomMethode));
            else
                result.EmpileErreur(I.T("Unknown method @1|20126", DefinitionFonction.NomProprieteSansCleTypeChamp));
			return result;
		}

		/// //////////////////////////////////////////
		public override bool IsSourceParametreBase
		{
			get
			{
				return true;
			}
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( ctx.ObjetSource == null )
			{
				result.Data = null;
				return result;
			}
			
			try
			{
                if (ctx.ObjetSource is IElementAFonctionsDynamiques && m_definitionFonction != null)
                {
                    CFonctionDynamique fTmp = ((IElementAFonctionsDynamiques)ctx.ObjetSource).GetFonctionDynamique(m_definitionFonction.IdFonction);
                    if (fTmp != null)
                    {
                        m_fonction = fTmp;
                    }
                }
                if (m_fonction == null && ctx.ObjetBase is IElementAFonctionsDynamiques && m_definitionFonction != null)
                {
                    CFonctionDynamique fTmp = ((IElementAFonctionsDynamiques)ctx.ObjetBase).GetFonctionDynamique(m_definitionFonction.IdFonction);
                    if (fTmp != null)
                    {
                        m_fonction = fTmp;
                    }
                }
                
                if (FonctionDynamique != null)
                {
                    result = FonctionDynamique.Eval(ctx, listeParametres);
                }
                else if (DefinitionMethode != null)
                {
                    object source = ctx.ObjetSource;
                    //Compatiblité (remplacement de méthodes par fonctions
                    bool bHasEvalFonction = false;
                    if (source is IElementAFonctionsDynamiques)
                    {
                        CFonctionDynamique fTmp = ((IElementAFonctionsDynamiques)source).GetFonctionDynamique(DefinitionMethode.Nom);
                        if (fTmp != null)
                        {
                            result = fTmp.Eval(ctx, listeParametres);
                            bHasEvalFonction = true;
                        }
                    }
                    if (!bHasEvalFonction)
                    {
                        MethodInfo info = ctx.ObjetSource.GetType().GetMethod(DefinitionMethode.NomProprieteSansCleTypeChamp);
                        if (info == null)
                        {
                            IInterpreteurMethodeDynamique inter = source as IInterpreteurMethodeDynamique;
                            if (inter != null)
                                inter.GetMethodInfo(DefinitionMethode.NomProprieteSansCleTypeChamp, ref info, ref source);
                        }
                        if (info == null)
                        {
                            CMethodeSupplementaire method = CGestionnaireMethodesSupplementaires.GetMethod(ctx.ObjetSource.GetType(), DefinitionMethode.NomProprieteSansCleTypeChamp);
                            if (method != null)
                            {
                                result.Data = method.Invoke(source, listeParametres);
                                return result;
                            }
                            result.EmpileErreur(I.T("Cannot find @1 method|124", DefinitionMethode.NomProprieteSansCleTypeChamp));
                            return result;
                        }

                        result.Data = info.Invoke(source, listeParametres);
                    }
                }
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
				result.EmpileErreur(I.T("Error during @1 method evaluation|123",
                    DefinitionMethode != null?
                    DefinitionMethode.NomProprieteSansCleTypeChamp:
                    FonctionDynamique != null?
                    FonctionDynamique.Nom:
                    "?"));
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if (!result )
				return result;
			I2iSerializable obj = m_definitionMethode;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_definitionMethode = (CDefinitionMethodeDynamique)obj;
            if (nVersion >= 1)
                result = serializer.TraiteObject<CDefinitionFonctionDynamique>(ref m_definitionFonction);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                if (m_definitionFonction != null)
                    m_strNomMethode = m_definitionFonction.NomProprieteSansCleTypeChamp;
                if (m_definitionMethode != null)
                    m_strNomMethode = m_definitionMethode.NomProprieteSansCleTypeChamp;
            }
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CArbreDefinitionsDynamiques GetArbreProprietesAccedees(CArbreDefinitionsDynamiques arbreEnCours)
		{
			CArbreDefinitionsDynamiques arbre = base.GetArbreProprietesAccedees ( arbreEnCours );
			if ( DefinitionMethode != null && ObjetPourAnalyseSourceConnu != null )
			{
				MethodInfo info = ObjetPourAnalyseSourceConnu.TypeResultatExpression.TypeDotNetNatif.GetMethod ( DefinitionMethode.NomProprieteSansCleTypeChamp );
				if ( info != null )
				{
					object[] attribs = info.GetCustomAttributes(typeof(OptimizedByAttribute), true);
					if ( attribs.Length > 0 )
					{
						OptimizedByAttribute optim = (OptimizedByAttribute)attribs[0];
						foreach ( string strProp in optim.Proprietes )
							arbreEnCours.AddSousProprieteString ( strProp );
					}
				}
			}
            else if ( FonctionDynamique != null && FonctionDynamique.Formule != null)
            {
                FonctionDynamique.Formule.GetArbreProprietesAccedees ( arbre);
            }
			return arbre;
		}

	}
}
