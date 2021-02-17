using System;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CDefinitionProprieteDynamiqueFormule.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueFormule : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "FL";

		private C2iExpression m_formule = null;

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueFormule()
			:base()
		{
		}
		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueFormule( C2iExpression formule )
			: base(
			I.T("Reserved Formula Field|164"),
			formule.GetString(),
			formule.TypeDonnee,
			true,
			true )
		{
			m_formule = formule;
		}

		/// //////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueFormule));
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////
		public C2iExpression Formule
		{
			get
			{
				return m_formule;
			}
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueFormule)def).m_formule = m_formule;
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			I2iSerializable objet = (I2iSerializable)m_formule;
			result = serializer.TraiteObject(ref objet);
			m_formule = (C2iExpression)objet;
			return result;
		}

	}

	/// ////////////////////////////////////////
	public class CInterpreteurProprieteDynamiqueFormule : IInterpreteurProprieteDynamiqueAccedantADautresProprietes
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(objet);
			if ( objet is CObjetDonnee )
				contexte.AttacheObjet(typeof(CContexteDonnee), ((CObjetDonnee)objet).ContexteDonnee);
			Type tpAnalyse = null;
			if ( objet != null )
				tpAnalyse = objet.GetType();
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( new CFournisseurPropDynStd(), tpAnalyse);
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			result = analyseur.AnalyseChaine(strPropriete);
			if (!result)
				return result;
			C2iExpression formule = (C2iExpression)result.Data;
			result = formule.Eval(contexte);
			if (result)
				return result;
			return result;
		}

        //------------------------------------------------------------
		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20034"));
			return result;
		}

        //------------------------------------------------------------
        public class COptimiseurProprieteDynamiqueFormule : IOptimiseurGetValueDynamic
        {
            private C2iExpression m_formule;
            private CContexteEvaluationExpression m_contexteEval = null;

            public COptimiseurProprieteDynamiqueFormule ( C2iExpression formule )
            {
                m_formule = formule;
            }

            public object  GetValue(object objet)
            {
                if ( m_formule == null )
                    return null;
                if (m_contexteEval == null)
                {
                    m_contexteEval = new CContexteEvaluationExpression(objet);
                    m_contexteEval.UseOptimiseurs = true;
                }
                else
                    m_contexteEval.ChangeSource(objet);
			    if ( objet is CObjetDonnee )
                    m_contexteEval.AttacheObjet(typeof(CContexteDonnee), ((CObjetDonnee)objet).ContexteDonnee);
                CResultAErreur result = m_formule.Eval(m_contexteEval);
                if ( result )
                    return result.Data;
                return null;
            }

            public Type GetTypeRetourne()
            {
                if (m_formule != null)
                    return m_formule.TypeDonnee.TypeDotNetNatif;
                return null;
            }

        }
            


        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( new CFournisseurPropDynStd(), tp);
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			CResultAErreur result = analyseur.AnalyseChaine(strPropriete);
            C2iExpression formule = null;
            if ( result )
                formule = result.Data as C2iExpression;
            return new COptimiseurProprieteDynamiqueFormule(formule);
        }

        public void AddProprietesAccedees(
            CArbreDefinitionsDynamiques arbre, 
            Type typeSource,
            string strPropriete)
        {
            CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression(new CFournisseurPropDynStd(), typeSource);
            CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
            CResultAErreur result = analyseur.AnalyseChaine(strPropriete);
            C2iExpression formule = null;
            if (result)
            {
                formule = result.Data as C2iExpression;
                formule.GetArbreProprietesAccedees(arbre);
                CDefinitionProprieteDynamiqueChampCalcule.DetailleSousArbres(arbre, CContexteDonneeSysteme.GetInstance());
            }
        }
    }
}
