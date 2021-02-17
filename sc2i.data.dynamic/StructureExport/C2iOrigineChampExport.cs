using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	
	//######################################################################################
	/// <summary>
	/// Description résumée de C2iOrigineChampExport.
	/// </summary>
	[Serializable]
	public abstract class C2iOrigineChampExport : I2iSerializable
	{
		protected Type m_typeDonnee;

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExport()
		{
		}

		public abstract CResultAErreur Serialize ( C2iSerializer serializer );
		public abstract Type TypeDonnee {get;}
		public abstract void AddProprietesOrigineToTable ( Type typeSource, Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee );
		

		/// //////////////////////////////////////////////////////////////
		public abstract object GetValeur(object obj, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction);

		/// /////////////////////////////////////////////
		protected void AddProprietesArbre(CArbreDefinitionsDynamiques arbre,
			Hashtable tableOrigines,
			string strChemin)
		{
			if (strChemin != "")
				strChemin += ".";
			/*if (arbre.DefinitionPropriete is CDefinitionProprieteDynamiqueChampCustom)
				tableOrigines[strChemin + "RelationsChampsCustom"] = true;
			else*/
			{
				string strNewChemin = strChemin;
				if (arbre.DefinitionPropriete != null)
				{
					tableOrigines[strChemin + arbre.DefinitionPropriete.NomPropriete] = true;
					strNewChemin = strChemin + arbre.DefinitionPropriete.NomPropriete;
				}
				foreach (CArbreDefinitionsDynamiques sousArbre in arbre.SousArbres)
					AddProprietesArbre(sousArbre, tableOrigines, strNewChemin);
				foreach (string strAutreProp in arbre.AutresSousProprietesString)
					tableOrigines[strChemin + strAutreProp] = true;
			}
		}
	}

	//######################################################################################
	/// <summary>
	/// Description résumée de C2iOrigineChampExportChamp.
	/// </summary>
	[Serializable]
	public class C2iOrigineChampExportChamp : C2iOrigineChampExport
	{
		private CDefinitionProprieteDynamique m_champOrigine;
		//Si le champ d'origine peut être accedé directement à partir
		//d'un colonne de l'objet donné, on optimise à fond !!
		//Indique si l'optim est tentée
		private bool m_bOptimFaite;
		//Indique le nom du champ d'origine
		private string m_strChampOrigineOptim;
        private string m_strCleRestriction = "";

        private IOptimiseurGetValueDynamic m_optimiseur = null;


		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportChamp ()
		{
		}

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportChamp ( CDefinitionProprieteDynamique champOrigine )
		{
			m_champOrigine = champOrigine;
		}

		/// //////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ChampOrigine
		{
			get
			{
				return m_champOrigine;
			}
			set
			{
				m_champOrigine = value;
			}
		}
		

		/// //////////////////////////////////////////////////////////////
		Type m_typeOptim = null;
		public override object GetValeur(object obj, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			IApplatisseurProprietes applatisseur = obj as IApplatisseurProprietes;
			if ( applatisseur != null )
				obj = applatisseur.GetObjetParDefaut();
            if (m_strCleRestriction == "")
            {
                if (m_champOrigine is CDefinitionProprieteDynamiqueChampCustom)
                {
                    CDbKey keyChamp = ((CDefinitionProprieteDynamiqueChampCustom)m_champOrigine).DbKeyChamp;
                    CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                    if (champ.ReadIfExists(keyChamp))
                        m_strCleRestriction = champ.CleRestriction;
                }
                else if ( m_champOrigine !=null )
                    m_strCleRestriction = m_champOrigine.NomProprieteSansCleTypeChamp;
            }

            if (restriction != null &&  m_strCleRestriction != "" && (restriction.GetRestriction(m_strCleRestriction) & ERestriction.Hide) == ERestriction.Hide)
                return null;

			if ( !m_bOptimFaite && obj != null )
			{
				m_bOptimFaite = true;
				if ( obj is CObjetDonnee )
				{
					m_strChampOrigineOptim = "";
					PropertyInfo info = obj.GetType().GetProperty ( m_champOrigine.NomProprieteSansCleTypeChamp );
					
					//Il faut absolument stocker le type, car si
					//Le champ vient d'une interface, tous les éléments ne sont pas forcement
					//du même type, et ne proviennent pas forcement du même champ
					m_typeOptim = obj.GetType();
					
					if ( info != null )
					{
						object[] attribs = info.GetCustomAttributes(typeof(AccesDirectInterditAttribute),true);
						if ( attribs.Length == 0 )
						{
							attribs = info.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
							if ( attribs != null && attribs.Length > 0 )
								m_strChampOrigineOptim = ((TableFieldPropertyAttribute)attribs[0]).NomChamp;
						}
					}
				}
                if (m_strChampOrigineOptim == "")
                {
                    m_optimiseur = CInterpreteurProprieteDynamique.GetOptimiseur(obj.GetType(), m_champOrigine.NomPropriete);
                }
			}
            if (m_strChampOrigineOptim != "" && obj is CObjetDonnee && obj.GetType() == m_typeOptim)
                return ((CObjetDonnee)obj).Row[m_strChampOrigineOptim];
            else if (m_optimiseur != null)
                    return m_optimiseur.GetValue(obj);
			else return CInterpreteurProprieteDynamique.GetValue ( obj, ChampOrigine, cacheValeurs ).Data;
		}

		/// //////////////////////////////////////////////////////////////
		public override Type TypeDonnee
		{
			get
			{
				return ChampOrigine.TypeDonnee.TypeDotNetNatif;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			I2iSerializable obj = ChampOrigine;
			serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			ChampOrigine = (CDefinitionProprieteDynamique)obj;
			return result;
		}

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="tableOrigines"></param>
		public override void AddProprietesOrigineToTable ( 
            Type typeSource,
            Hashtable tableOrigines, 
            string strChemin, 
            CContexteDonnee contexteDonnee )
		{
			if ( strChemin.Length > 0 )
				strChemin += ".";
            string[] strProps = CInterpreteurProprieteDynamique.GetProprietesAccedees(typeSource, ChampOrigine.NomPropriete);
            if (strProps != null)
            {
                foreach (string strProp in strProps)
                {
                    tableOrigines[strChemin+strProp] = true;
                }
            }
            else
            {
                string strOrigine = ChampOrigine.NomPropriete;
                tableOrigines[strChemin + strOrigine] = true;
            }
            /*
             * if (ChampOrigine is CDefinitionProprieteDynamiqueChampCalcule)
			{
				if (contexteDonnee != null)
				{
					CDefinitionProprieteDynamiqueChampCalcule defCal = (CDefinitionProprieteDynamiqueChampCalcule)ChampOrigine;
					CChampCalcule champCal = new CChampCalcule(contexteDonnee);
					if (champCal.ReadIfExists(defCal.IdChamp))
					{
						C2iExpression expression = champCal.Formule;
						if (expression != null)
						{
							CArbreDefinitionsDynamiques arbre = new CArbreDefinitionsDynamiques(null);
							expression.GetArbreProprietesAccedees(arbre);
							CDefinitionProprieteDynamiqueChampCalcule.DetailleSousArbres(arbre, contexteDonnee);
							AddProprietesArbre(arbre, tableOrigines, strChemin);
						}
					}
				}
			}
			else
			{
				string strOrigine = ChampOrigine.NomPropriete;
				tableOrigines[strChemin + strOrigine] = true;
			}*/
		}



		/// /////////////////////////////////////////////
	}
	//######################################################################################
	/// <summary>
	/// Description résumée de C2iOrigineChampExportExpression.
	/// </summary>
	[Serializable]
	public class C2iOrigineChampExportExpression : C2iOrigineChampExport
	{
		private C2iExpression m_expression;

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportExpression ( )
		{
		}

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportExpression ( C2iExpression expression )
		{
			m_expression = expression;
		}

		/// //////////////////////////////////////////////////////////////
		public C2iExpression Expression
		{
			get
			{
				return m_expression;
			}
			set
			{
				m_expression = value;
			}
		}
		

		/// //////////////////////////////////////////////////////////////
		public override object GetValeur(object obj, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(obj, cacheValeurs);
			CResultAErreur result = Expression.Eval(ctx);
			if (!result)
				return null;
			return result.Data;
		}

		/// //////////////////////////////////////////////////////////////
		public override Type TypeDonnee
		{
			get
			{
                if (Expression.TypeDonnee.IsArrayOfTypeNatif)
                    return Expression.TypeDonnee.TypeDotNetNatif.MakeArrayType();
				return Expression.TypeDonnee.TypeDotNetNatif;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			I2iSerializable obj = m_expression;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_expression = (C2iExpression)obj;
			return result;
		}

		/// /////////////////////////////////////////////
		public override void AddProprietesOrigineToTable ( Type typeSource, Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee )
		{
			if ( Expression != null )
			{
				CArbreDefinitionsDynamiques arbre = new CArbreDefinitionsDynamiques ( null );
				Expression.GetArbreProprietesAccedees ( arbre );
				CDefinitionProprieteDynamiqueChampCalcule.DetailleSousArbres(arbre, contexteDonnee);
				
				AddProprietesArbre ( arbre, tableOrigines, strChemin );
				/*string[] strProps = arbre.GetListeProprietesAccedees ( );
				if ( strChemin != "" )
					strChemin += ".";
				foreach ( string strProp in strProps )
				{
					string strOrigine = strProp;
					tableOrigines[strChemin+strOrigine] = true;
				}*/
			}
		}

		


		


	}

	//######################################################################################
	/// <summary>
	/// Utilisé dans les exports simple pour ramener les valeurs de champs
	/// custom dans la table elle même
	/// </summary>
	[Serializable]
	public class C2iOrigineChampExportChampCustom : C2iOrigineChampExport
	{
		private List<int> m_listeIdsChamps = new List<int>();

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportChampCustom()
		{
		}

		/// //////////////////////////////////////////////////////////////
		public C2iOrigineChampExportChampCustom(int[] nIdsChamps)
		{
			m_listeIdsChamps = new List<int>(nIdsChamps);
		}

		/// //////////////////////////////////////////////////////////////
		public int[] IdsChampCustom
		{
			get
			{
				return m_listeIdsChamps.ToArray();
			}
			set
			{
				m_listeIdsChamps = new List<int>(value);
			}
		}


		/// //////////////////////////////////////////////////////////////
		public override object GetValeur(object obj, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			return null;//Pas implémenté
		}

		/// //////////////////////////////////////////////////////////////
		public override Type TypeDonnee
		{
			get
			{
				return typeof(string);
			}
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////////
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			IList lst = new ArrayList(m_listeIdsChamps);
			result = serializer.TraiteListeObjetsSimples(ref lst);
			if (serializer.Mode == ModeSerialisation.Lecture)
			{
				m_listeIdsChamps = new List<int>();
				foreach (int nId in lst)
					m_listeIdsChamps.Add(nId);
			}
			return result;
		}

		/// /////////////////////////////////////////////
		public override void AddProprietesOrigineToTable(Type typeSource, Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee)
		{
		}







	}
}
