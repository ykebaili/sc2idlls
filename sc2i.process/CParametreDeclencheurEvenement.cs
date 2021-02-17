using System;
using System.Data;
using System.Reflection;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.process
{
	/// <summary>
	/// Description r�sum�e de CParametreDeclencheurEvenement.
	/// </summary>
	public class CParametreDeclencheurEvenement : I2iSerializable
	{
		private Type m_typeCible = typeof(string);
		private TypeEvenement m_typeEvenement = TypeEvenement.Manuel;
		private bool m_bHideProgress = false;
		private string m_strIdEvenementSpecifique = "";
		private CDefinitionProprieteDynamique m_proprieteASurveiller = null;
		private C2iExpression m_formuleValeurAvant = null;
		private C2iExpression m_formuleValeurApres = null;
        private HashSet<string> m_contextesException = new HashSet<string>();

		//Lorsque le handler est activ�, condition pour que l'�venement soit d�clench�
		private C2iExpression m_formuleConditionDeclenchement = null;
		
		//Date � laquelle programm� le handler
		private C2iExpression m_formuleDateProgramme = null;

		//Code du handler d'�venement (peut permettre de retrouver des handler suivant leur code)
		private string m_strCodeHandler = "";


		private string m_strMenuManuel = "";


		private int m_nOrdreExecution = 0;

        // TESTDBKEYTODO
		private CDbKey[] m_lstKeysGroupesManuels = new CDbKey[0];

		public CParametreDeclencheurEvenement()
		{
			
		}

		/// ////////////////////////////////////////////////////////
		public Type TypeCible
		{
			get
			{
				return m_typeCible;
			}
			set
			{
				if ( value != null )
					m_typeCible = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool HideProgress
		{
			get
			{
				return m_bHideProgress;
			}
			set
			{
				m_bHideProgress = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string Code
		{
			get
			{
				return m_strCodeHandler;
			}
			set
			{
				m_strCodeHandler = value;
			}
		}


		/// ////////////////////////////////////////////////////////
		public string MenuManuel
		{
			get
			{
				return m_strMenuManuel;
			}
			set
			{
				m_strMenuManuel = value;
			}
		}

		public int OrdreExecution
		{
			get
			{
				return m_nOrdreExecution;
			}
			set
			{
				m_nOrdreExecution = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CDbKey[] KeysGroupesManuel
		{
			get
			{
				return m_lstKeysGroupesManuels;
			}
			set
			{
                m_lstKeysGroupesManuels = value;
			}
		}


		/// ////////////////////////////////////////////////////////
		public TypeEvenement TypeEvenement
		{
			get
			{
				return m_typeEvenement;
			}
			set
			{
				m_typeEvenement = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string IdEvenementSpecifique
		{
			get
			{
				return m_strIdEvenementSpecifique;
			}
			set
			{
				m_strIdEvenementSpecifique = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ProprieteASurveiller
		{
			get
			{
				return m_proprieteASurveiller;
			}
			set
			{
				m_proprieteASurveiller = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurAvant
		{
			get
			{
				return m_formuleValeurAvant;
			}
			set
			{
				m_formuleValeurAvant = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurApres
		{
			get
			{
				return m_formuleValeurApres;
			}
			set
			{
				m_formuleValeurApres = value;
			}
		}

        /// ////////////////////////////////////////////////////////
        public HashSet<string> ContextesException
        {
            get
            {
                return m_contextesException;
            }
            set
            {
                m_contextesException = value;
            }
        }

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleConditionDeclenchement
		{
			get
			{
				return m_formuleConditionDeclenchement;
			}
			set
			{
				m_formuleConditionDeclenchement = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleDateProgramme
		{
			get
			{
				return m_formuleDateProgramme;
			}
			set
			{
				m_formuleDateProgramme = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			switch ( TypeEvenement )
			{
				case TypeEvenement.Date :
					if ( FormuleDateProgramme == null  ||
						FormuleDateProgramme.TypeDonnee.TypeDotNetNatif != typeof(DateTime) &&
						FormuleDateProgramme.TypeDonnee.TypeDotNetNatif != typeof(DateTime?))
						result.EmpileErreur(I.T( "The date formula is incorrect|264"));
					break;
			}
			if ( !result )
				return result;

								
			return result;
		}
		#region Membres de I2iSerializable

		private int GetNumVersion()
		{
			return 6;
			//1 : Ajout de menu manuel et ids groupes
			//2 : Ajout de l'ordre d'execution
			//3 : Ajout de l'id d'�venement sp�cifique
			//4 : Ajout de HideProgress
            //5 : Ajout des contextes d'exception
            //6 : Passage de Ids Groupes en liste de String
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteType ( ref m_typeCible );
			
			int nTmp = (int)m_typeEvenement;
			serializer.TraiteInt ( ref nTmp );
			m_typeEvenement = (TypeEvenement)nTmp;

			I2iSerializable objet = m_proprieteASurveiller;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_proprieteASurveiller = (CDefinitionProprieteDynamique)objet;

			objet = m_formuleValeurAvant;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_formuleValeurAvant = (C2iExpression)objet;

			objet = m_formuleValeurApres;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_formuleValeurApres = (C2iExpression)objet;


			objet = m_formuleConditionDeclenchement;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_formuleConditionDeclenchement = (C2iExpression)objet;

			objet = m_formuleDateProgramme;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_formuleDateProgramme = (C2iExpression)objet;

			serializer.TraiteString ( ref m_strCodeHandler );

			if ( nVersion >= 1 )
			{
				serializer.TraiteString ( ref m_strMenuManuel );
                if (nVersion < 6 && serializer.Mode == ModeSerialisation.Lecture)
                {
                    //TESTDBKEYOK les groupes pour ex�cution manuelle ne sont plus exploit�s (Avril 2014)
                    IList lst = new ArrayList();
                    serializer.TraiteListeObjetsSimples(ref lst);
                    List<CDbKey> lstIdsTemp = new List<CDbKey>();
                    foreach (int nId in lst)
                    {
                        lstIdsTemp.Add(CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId));
                    }
                    m_lstKeysGroupesManuels = lstIdsTemp.ToArray();
                }
                else
                {
                    //TESTDBKEYTODO
                    IList lstKeysString = new ArrayList();
                    foreach (CDbKey key in m_lstKeysGroupesManuels)
                        if (key != null)
                            lstKeysString.Add(key.StringValue);
                    serializer.TraiteListeObjetsSimples(ref lstKeysString);
                    if (serializer.Mode == ModeSerialisation.Lecture)
                    {
                        List<CDbKey> lstKeys = new List<CDbKey>();
                        foreach (string strKey in lstKeysString)
                        {
                            int nId;
                            if (int.TryParse(strKey, out nId))
                            {
                                lstKeys.Add(CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId));
                            }
                            else
                                lstKeys.Add(CDbKey.CreateFromStringValue(strKey));
                            m_lstKeysGroupesManuels = lstKeys.ToArray();
                        }
                    }
                }
			}
			if ( nVersion >= 2 )
				serializer.TraiteInt ( ref m_nOrdreExecution );
			if (nVersion >= 3)
				serializer.TraiteString(ref m_strIdEvenementSpecifique);
			if (nVersion >= 4)
				serializer.TraiteBool(ref m_bHideProgress);
			else
				m_bHideProgress = false;
            if ( nVersion >= 5 )
            {
                int nNbContextes = m_contextesException.Count;
                serializer.TraiteInt ( ref nNbContextes );
                switch ( serializer.Mode )
                {
                    case ModeSerialisation.Ecriture :
                        foreach ( string strContexte in m_contextesException )
                        {
                            string strCtx = strContexte;
                            serializer.TraiteString ( ref strCtx );
                        }
                        break;
                    case ModeSerialisation.Lecture :
                        m_contextesException = new HashSet<string>();
                        for (int i = 0; i < nNbContextes; i++)
                        {
                            string strCtx = "";
                            serializer.TraiteString ( ref strCtx );
                            m_contextesException.Add(strCtx);
                        }
                        break;
                }
			}
		
			return result;
		}

		#endregion

		/// /////////////////////////////////////////////////////////
		///Retourne l'�l�ment sur lequel sera lanc� l'�venement
		public static CObjetDonneeAIdNumerique GetObjetToRun( CObjetDonneeAIdNumerique objet)
		{
			if ( objet is CRelationElementAChamp_ChampCustom )
				return (CObjetDonneeAIdNumerique)((CRelationElementAChamp_ChampCustom)objet).ElementAChamps;
			return objet;
		}

		/// <summary>
		/// Indique si l'�venement doit se d�clencher
		/// </summary>
		/// <param name="objet"></param>
		/// <param name="bModeAvecInterface">
		/// si vrai, les �venements de type Manuel ou Ouverture retournent vrai,
		/// sinon, ils retournent syst�matiquement faux
		/// </param>
		/// <param name="bModeStatique">
		/// Si vrai, les �venements de cr�ation sont vrai et les �venements
		/// de modif sans valeur initiale retournent vrai si la valeur de fin est verifi�e
		/// </param>
		/// <returns></returns>
		public bool ShouldDeclenche ( CObjetDonneeAIdNumerique objet, bool bModeAvecInterface, bool bModeStatique, ref CInfoDeclencheurProcess infoDeclencheur )
		{
			CResultAErreur result = CResultAErreur.True;
            if (ContextesException.Contains(objet.ContexteDeModification))
                return false;
			//CObjetDonneeAIdNumeriqueAuto objetToEval = objet;
			CDefinitionProprieteDynamique defPropriete = ProprieteASurveiller;
            if (objet is CRelationElementAChamp_ChampCustom)
            {
                if (!(defPropriete is CDefinitionProprieteDynamiqueChampCustom))
                    return false;
                CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)objet;
                if (rel.ChampCustom.DbKey != ((CDefinitionProprieteDynamiqueChampCustom)defPropriete).DbKeyChamp)
                    return false;
            }
            CObjetDonneeAIdNumerique objetToTest = GetObjetToRun(objet);
            if ( objetToTest == null )
                return false;
            DataRowVersion versionToReturnOriginal = objetToTest.VersionToReturn;
            try
            {
                if (objetToTest.Row.RowState == DataRowState.Deleted)
                    objetToTest.VersionToReturn = DataRowVersion.Original;
                CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(objetToTest);
                object valeurAvant;
                object valeurApres;
                infoDeclencheur = new CInfoDeclencheurProcess();
                infoDeclencheur.TypeDeclencheur = TypeEvenement;
                switch (TypeEvenement)
                {
                    case TypeEvenement.Manuel:
                        if (!bModeAvecInterface)
                            return false;
                        break;
                    case TypeEvenement.Creation:
                        if (objet.Row.RowState != DataRowState.Added && !bModeStatique)
                            return false;
                        break;
                    case TypeEvenement.Date:
                        //V�rifie si la date a chang�
                        if (defPropriete != null)
                        {
                            if (!bModeStatique)
                            {
                                valeurAvant = GetValeur(objet, defPropriete, DataRowVersion.Original);
                                valeurApres = GetValeur(objet, defPropriete, DataRowVersion.Current);
                                if (TestEgalite(valeurApres, valeurAvant))
                                    return false;
                            }
                        }
                        break;
                    case TypeEvenement.Modification:
                        if (objet.Row.RowState == DataRowState.Modified || objet.Row.RowState == DataRowState.Added || bModeStatique)
                        {
                            if (defPropriete != null)
                            {
                                try
                                {
                                    valeurAvant = null;
                                    if (objet.Row.RowState == DataRowState.Modified)
                                        valeurAvant = GetValeur(objet, defPropriete, DataRowVersion.Original);
                                    valeurApres = GetValeur(objet, defPropriete, DataRowVersion.Current);
                                    if (objet.Row.RowState == DataRowState.Modified && TestEgalite(valeurApres, valeurAvant))
                                        return false;
                                    if (FormuleValeurAvant != null)
                                    {
                                        if (bModeStatique)
                                            return false;
                                        result = FormuleValeurAvant.Eval(contexteEval);
                                        if (!result)
                                            return false;
                                        if (!TestEgalite(result.Data, valeurAvant))
                                            return false;
                                    }
                                    if (FormuleValeurApres != null)
                                    {
                                        result = FormuleValeurApres.Eval(contexteEval);
                                        if (!result)
                                            return false;
                                        if (!TestEgalite(result.Data, valeurApres))
                                            return false;
                                    }
                                    infoDeclencheur.ValeurOrigine = valeurAvant;
                                }
                                catch
                                {
                                    return false;
                                }
                            }


                        }
                        else
                            return false;
                        break;
                    case TypeEvenement.Specifique:
                        if (objet.Table.Columns.Contains(EvenementAttribute.c_champEvenements))
                        {
                            if (!EvenementAttribute.HasEvent(objet, IdEvenementSpecifique))
                                return false;
                        }
                        //Stef 110309 : Si on annule l'�venement l�, et qu'il y a d'autres
                        //Hanlder sur cet �venement, les autres handler ne se d�clencheront pas
                        //il faut donc annuler l'evenement quand on a parcouru tous les �venements
                        //Donc, dans le CGestionnaireEvenements
                        //EvenementAttribute.AnnuleEvenement(objet, IdEvenementSpecifique);
                        break;
                    case TypeEvenement.Suppression:
                        if (objet.Row.RowState != DataRowState.Deleted)
                            return false;
                        break;

                }
                //Evalue la condition
                if (FormuleConditionDeclenchement != null)
                {
                    result = FormuleConditionDeclenchement.Eval(contexteEval);
                    if (!result)
                        return result;
                    if (result.Data is bool)
                    {
                        if (!(bool)result.Data)
                            return false;
                    }
                    else if (result.Data.ToString() == "" || result.Data.ToString() == "0")
                        return false;
                }
            }
            catch
            {
            }
            finally
            {
                objetToTest.VersionToReturn = versionToReturnOriginal;
            }
			return true;
		}

		/// /////////////////////////////////////////////////////////
		public static object GetValeur ( CObjetDonnee objet, CDefinitionProprieteDynamique prop, DataRowVersion version )
		{
			if (prop is CDefinitionProprieteDynamiqueChampCustom )
			{

				//Evaluation d'un champ custom
				if (objet is CRelationElementAChamp_ChampCustom)
				{
					CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)objet;
					return rel.GetValeur(version);
				}
				if (objet is IElementAChamps)
				{
					return ((IElementAChamps)objet).GetValeurChamp(((CDefinitionProprieteDynamiqueChampCustom)prop).DbKeyChamp, version);
				}
			}
			else
			{
				#region Evaluation d'un champ
				PropertyInfo info = objet.GetType().GetProperty ( prop.NomProprieteSansCleTypeChamp );
				object[] attribs = info.GetCustomAttributes ( typeof(TableFieldPropertyAttribute), true );
				if ( attribs.Length != 0 )
				{
					TableFieldPropertyAttribute attrTable = (TableFieldPropertyAttribute)attribs[0];
					try
					{
						object val = objet.Row[attrTable.NomChamp, version];
						if ( val == DBNull.Value )
							val = null;
						return val;
					}
					catch
					{
						return null;
					}
				}
				else
				{
					attribs = info.GetCustomAttributes ( typeof(RelationAttribute), true );
					if ( attribs.Length != 0 )
					{
						RelationAttribute attrRel = (RelationAttribute)attribs[0];
						try
						{
							object val = objet.Row[attrRel.ChampsFils[0], version];
							if ( val == DBNull.Value )
								val = null;
							return val;
						}
						catch
						{
							return null;
						}
					}
				}
				#endregion
			}

			return null;
		}

		/// /////////////////////////////////////////////////////////
		private bool TestEgalite ( object obj1, object obj2 )
		{
			if (obj1 == null && obj2 == null )
				return true;
			if ( (obj1 == null && obj2 != null) ||
				obj1 != null && obj2==null )
				return false;
			return obj1.Equals ( obj2 );
		}

		/// /////////////////////////////////////////////////////////
		public static CDefinitionProprieteDynamique[] ProprietesSurveillables ( Type typeObjet, bool bForEvenementDate )
		{
			CDefinitionProprieteDynamique[] defs = new CFournisseurPropDynStd(false).GetDefinitionsChamps ( typeObjet, 0 );
			ArrayList lst = new ArrayList();
			foreach ( CDefinitionProprieteDynamique def in defs )
			{
				if ( def.GetType() == typeof(CDefinitionProprieteDynamiqueDotNet) )
				{
					PropertyInfo info = typeObjet.GetProperty ( def.NomProprieteSansCleTypeChamp );
					if ( info != null && 
                        (!bForEvenementDate || 
                        info.PropertyType == typeof(DateTime) || 
                        info.PropertyType == typeof(DateTime?) ||
                        info.PropertyType == typeof(CDateTimeEx)))
					{
						object[] attribs = info.GetCustomAttributes ( typeof(TableFieldPropertyAttribute), true );
						if ( attribs.Length != 0 && ((TableFieldPropertyAttribute)attribs[0]).IsInDb)
							lst.Add ( def );
						else
						{
							attribs = info.GetCustomAttributes ( typeof(RelationAttribute), true );
							if ( attribs.Length != 0 && !bForEvenementDate)
								lst.Add ( def );
						}
					}
				}
				else if ( def is CDefinitionProprieteDynamiqueChampCustom )
				{
					if ( !bForEvenementDate || def.TypeDonnee.TypeDotNetNatif == typeof(DateTime) )
						lst.Add ( def );
				}
			}
			return (CDefinitionProprieteDynamique[])lst.ToArray ( typeof(CDefinitionProprieteDynamique) );
		}
	}
}
