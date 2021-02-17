using System;


using System.Collections;


using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionMethodeAnalysable.
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionMethodeAnalysable : C2iExpressionAnalysable
	{
		public C2iExpressionMethodeAnalysable()
		{
			
		}

		/// ///////////////////////////////////////////////////////////
		public override bool CanBeArgumentExpressionObjet
		{
			get
			{
				return true;
			}
		}

		//Liste des objets sources sur lesquels la méthode peut s'appliquer
		public abstract CTypeResultatExpression[] TypesObjetSourceAttendu{get;}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
			CResultAErreur result = CResultAErreur.True;
			CTypeResultatExpression[] typesAttendus = TypesObjetSourceAttendu;
			foreach ( CTypeResultatExpression type in typesAttendus )
			{
				if (objetPourSousProprietes.TypeResultatExpression != null && objetPourSousProprietes.TypeResultatExpression.CanConvertTo ( type ) )
				{
					CObjetPourSousProprietes objForParametres = GetObjetAnalyseParametresFromObjetAnalyseSource(objetPourSousProprietes);
                    if (objForParametres == null)
                        objForParametres = objetPourSousProprietes;
					return base.SetTypeObjetInterroge(objForParametres, fournisseur);
				}
			}
			result.EmpileErreur(I.T("@1 cannot be applied to the @2 type|119",GetInfos().Texte,objetPourSousProprietes.ToString()));
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		///Retourne le type qui sera source des parametres en fonction du type de l'objet source
        ///Retourne null si l'analyse se fait à partir du type de base
        public abstract CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes tpSource);

		/// //////////////////////////////////// /////////////////////////////////
		public virtual bool AgitSurListe
		{
			get
			{
				foreach ( CTypeResultatExpression type in TypesObjetSourceAttendu )
					if ( type.IsArrayOfTypeNatif )
						return true;
				return false;
			}
		}

	}
}
