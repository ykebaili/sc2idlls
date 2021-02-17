using System;
using System.Drawing;
using System.Reflection;

using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using System.Text;
using System.Collections.Generic;
using sc2i.multitiers.client;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionSupprimerEntite.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionSupprimerEntite : CActionLienSortantSimple
	{
		private C2iExpression m_expressionElementASupprimer = null;
        private bool m_bCascade = false;

        private bool m_bPurgeAdmin = false;

		/// /////////////////////////////////////////
		public CActionSupprimerEntite( CProcess process )
			:base(process)
		{
			Libelle = I.T("Delete an entity|239");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Delete an entity|239"),
				I.T("Deletes an entity|240"),
				typeof(CActionSupprimerEntite),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable(m_expressionElementASupprimer, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
            get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
            //1 : Ajoute de m_bCascade
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			if ( !result )
				return result;
			
			I2iSerializable objet = m_expressionElementASupprimer;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_expressionElementASupprimer = (C2iExpression)objet;

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bCascade);
            else
                m_bCascade = false;

            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bPurgeAdmin);
            else
                m_bPurgeAdmin = false;
            
		
			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionEntiteASupprimer
		{
			get
			{
				if ( m_expressionElementASupprimer == null )
					return new C2iExpressionConstante("");
				return m_expressionElementASupprimer;
			}
			set
			{
				m_expressionElementASupprimer = value;
			}
		}

        ////////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique si toutes les entités filles doivent être supprimées en cascade.
        /// Si True les entités ayant une relation parente avec l'élément (composition ou non) seront supprimées
        /// </summary>
        public bool DeleteFillesEnCascade
        {
            get { return m_bCascade || PurgeAdmin; }
            set { m_bCascade = value; }
        }


        /// <summary>
        /// Indique qu'il s'agit d'un purge d'administration (delete direct dans la base)
        /// </summary>
        public bool PurgeAdmin
        {
            get
            {
                return m_bPurgeAdmin;
            }
            set
            {
                m_bPurgeAdmin = value;
            }
        }


		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if ( m_expressionElementASupprimer == null )
			{
				result.EmpileErreur(I.T("Indicate the element to be removed|241"));
			}
			else
			{
				/*if ( m_expressionElementASupprimer.TypeDonnee.IsArrayOfTypeNatif )
					result.EmpileErreur(I.T("The element to be removed formula return several elements|242"));
				else */
                if ( !typeof(IObjetDonneeAIdNumerique).IsAssignableFrom ( m_expressionElementASupprimer.TypeDonnee.TypeDotNetNatif ) )
					result.EmpileErreur(I.T("The element to be removed formula must return a work entity|243"));
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			//Calcule l'élément à supprimer
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
			contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = ExpressionEntiteASupprimer.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T( "Error during @1 formula evaluation|216", ExpressionEntiteASupprimer.ToString()));
				return result;
			}
            if (result.Data is IEnumerable && 
                (!(result.Data is CListeObjetsDonnees) || m_bPurgeAdmin))
            {
                StringBuilder bl = new StringBuilder();
                Type tp = null;
                string strChampId = "";
                List<int> lstIds = new List<int>();
                foreach (object obj in ((IEnumerable)result.Data))
                {
                    CObjetDonneeAIdNumerique objId = obj as CObjetDonneeAIdNumerique;
                    if (objId == null)
                    {
                        result.EmpileErreur(I.T("Can not delete element of this type|20030"));
                        return result;
                    }
                    if (tp == null)
                    {
                        tp = objId.GetType();
                        strChampId = objId.GetChampId();
                    }
                    if (objId.GetType() != tp)
                    {
                        result.EmpileErreur(I.T("Can not delete a list of different object types|20031"));
                        return result;
                    }
                    bl.Append(objId.Id);
                    lstIds.Add(objId.Id);
                    bl.Append(",");
                }
                if (m_bPurgeAdmin)
                {
                    result = PurgeEntites(contexte.IdSession, tp, lstIds.ToArray());
                }
                else
                {
                    if (bl.Length == 0)
                        return result;
                    bl.Remove(bl.Length - 1, 1);
                    CListeObjetsDonnees lst = new CListeObjetsDonnees(contexte.ContexteDonnee, tp);
                    lst.Filtre = new CFiltreData(strChampId + " in (" + bl.ToString() + ")");
                    if (DeleteFillesEnCascade)
                        result = CObjetDonneeAIdNumerique.DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(lst, true);
                    else
                        result = CObjetDonneeAIdNumerique.Delete(lst, true);
                }
                return result;
            }


            if (result.Data is CListeObjetsDonnees)
            {
                result = CObjetDonneeAIdNumerique.Delete(result.Data as CListeObjetsDonnees, true);
                return result;           
            }

			CObjetDonneeAIdNumerique objetASupprimer = (CObjetDonneeAIdNumerique)result.Data;
			
			ArrayList lstVariablesAVider = new ArrayList();
			//CHerche les variables qui contiennent l'objet à supprimer
			foreach ( CVariableDynamique variable in contexte.Branche.Process.ListeVariables )
			{
				object val = contexte.Branche.Process.GetValeurChamp ( variable );
				if ( val != null && val.Equals ( objetASupprimer ) )
					lstVariablesAVider.Add ( variable );
			}

            if (m_bPurgeAdmin)
            {
                result = PurgeEntites(contexte.IdSession, objetASupprimer.GetType(), new int[] { objetASupprimer.Id });
            }
            else
            {

                // Traite la suppression de tous les éléments filles en cascade.
                if (DeleteFillesEnCascade)
                {
                    result = objetASupprimer.DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(true);
                }
                else
                    result = objetASupprimer.Delete(true);
            }

            if (result)
			{
				foreach ( CVariableDynamique variable in lstVariablesAVider )
					Process.SetValeurChamp ( variable, null );
			}
			return result;
		}

        /// /////////////////////////////////////////////////
        public static CResultAErreur PurgeEntites(int nIdSession, Type typeObjets, int[] nIds)
        {
            IActionSupprimerEntiteServeur objetServeur = (IActionSupprimerEntiteServeur)C2iFactory.GetNewObjetForSession("CActionSupprimerEntiteServeur", typeof(IActionSupprimerEntiteServeur), nIdSession);
            CResultAErreur result = objetServeur.PurgeEntites(typeObjets, nIds);
            return result;
        }


	}

    /// /////////////////////////////////////////////////
    public interface IActionSupprimerEntiteServeur
    {
        CResultAErreur PurgeEntites(Type typeObjets, int[] lstIds);
    }
}
