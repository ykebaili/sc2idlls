using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionAssocierComportement.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionAssocierComportement : CActionLienSortantSimple
	{
		private C2iExpression m_expressionElementAAssocier;
        private CDbKey m_dbKeyComportement = null;

		/// /////////////////////////////////////////
		public CActionAssocierComportement( CProcess process )
			:base(process)
		{
			Libelle = I.T("Associate a behavior|105");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Associate a behavior|105"),
				I.T("Allows to associate a behavior with an element|106"),
				typeof(CActionAssocierComportement),
				CGestionnaireActionsDisponibles.c_categorieComportement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( m_expressionElementAAssocier, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
            //return 0;
            return 1; // Passage de Id_comportement en DbKey
		}


		/// ////////////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if (!result)
                return result;

            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyComportement, typeof(CComportementGenerique));
            else
                serializer.TraiteDbKey(ref m_dbKeyComportement);

            I2iSerializable objet = m_expressionElementAAssocier;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionElementAAssocier = (C2iExpression)objet;


            return result;
        }

		/// ////////////////////////////////////////////////////////
        [ExternalReferencedEntityDbKey(typeof(CComportementGenerique))]
		public CDbKey DbKeyComportement
		{
			get
			{
                return m_dbKeyComportement;
			}
			set
			{
                m_dbKeyComportement = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleElementAAssocier
		{
			get
			{
				return m_expressionElementAAssocier;
			}
			set
			{
				m_expressionElementAAssocier = value;
			}
		}
		

	

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if ( m_expressionElementAAssocier == null )
			{
				result.EmpileErreur(I.T("The element to associated formula is incorrect|107"));
			}
			else
			{
				CTypeResultatExpression typeRes = m_expressionElementAAssocier.TypeDonnee;
				if ( !(typeRes.TypeDotNetNatif.IsSubclassOf(typeof(CObjetDonneeAIdNumerique) )))
				{
					result.EmpileErreur(I.T( "The element to associated formula must return a work entity|108"));
				}
			}

			//Vérifie que le comportement existe
			CComportementGenerique comportement = new CComportementGenerique ( Process.ContexteDonnee );
            if (!comportement.ReadIfExists(m_dbKeyComportement))
                result.EmpileErreur(I.T("The behavior to be associated is incorrect|109"));
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
			contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			if ( m_expressionElementAAssocier == null )
			{
				result.EmpileErreur(I.T("Not element to be associated|110"));
				return result;
			}
			result = m_expressionElementAAssocier.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in element to be associated formula|111"));
				return result;
			}
			if ( result.Data != null )
			{
				if ( !(result.Data is CObjetDonneeAIdNumerique) )
				{
					result.EmpileErreur(I.T("The element to be associated formula doesn't return a work entity|112"));
					return result;
				}
				CComportementGenerique comportement = new CComportementGenerique ( contexte.ContexteDonnee );
                if (!comportement.ReadIfExists(m_dbKeyComportement))
                {
                    result.EmpileErreur(I.T("The @1 behavior doesn't exist|113", m_dbKeyComportement.ToString()));
                    return result;
                }
				comportement.AddComportementToObjet ( (CObjetDonneeAIdNumerique)result.Data );
			}
			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
		}



	}
}