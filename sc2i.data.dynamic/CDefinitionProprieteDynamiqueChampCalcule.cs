using System;

using sc2i.common;

using sc2i.expression;
using System.Collections;
using System.Collections.Generic;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Propriete dynamique définie par un champ calculé
	/// la propriété associée est l'id du champ calculé dans la base de données
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueChampCalcule : CDefinitionProprieteDynamique
	{
		public const string c_strCleType = "CL";
        /* TESTDBKEYOK
         * Test fait le SC par 27/03/2014
         * Résultat : tout il est ok */
        protected CDbKey m_dbKeyChamp = null;

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueChampCalcule()
			:base()
		{
		}

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueChampCalcule( CChampCalcule champ )
			:base ( champ.Nom.Replace(" ","_"), champ.DbKey.StringValue, 
				champ.Formule.TypeDonnee, false, true )
		{
            //TESTDBKEYOK
            m_dbKeyChamp = champ.DbKey;
            Rubrique = champ.Categorie;
		}

		/// //////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueChampCalcule));
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////
		[ExternalReferencedEntityDbKey(typeof(CChampCalcule))]
        public CDbKey DbKeyChamp
		{
			get
			{
				return m_dbKeyChamp;
			}
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueChampCalcule)def).m_dbKeyChamp = m_dbKeyChamp;
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
            //return 0;
            return 1; // Passag de Id Champ Calculé à DBKey
		}

		/// ////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            //TESTDBKEYOK
            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyChamp, typeof(CChampCalcule));
            else
                serializer.TraiteDbKey(ref m_dbKeyChamp);
            if ( m_dbKeyChamp != null )
                SetNomProprieteSansCleTypeChamp ( m_dbKeyChamp.StringValue );
            return result;
        }

        public static void DetailleSousArbres(
            CArbreDefinitionsDynamiques arbre,
            CContexteDonnee contexteDonnee)
        {
            DetailleSousArbres(arbre, contexteDonnee, null);
        }

		/// ////////////////////////////////////////
		///Si l'arbre est basé sur un champ calculé, détaille les
		///champs utilisés dans la formule
		public static void DetailleSousArbres(
            CArbreDefinitionsDynamiques arbre, 
            CContexteDonnee contexteDonnee,
            Dictionary<CDefinitionProprieteDynamique,int> dicCountProfondeurParChamp)
		{
            //TESTDBKEYOK
            if ( dicCountProfondeurParChamp == null )
                dicCountProfondeurParChamp = new Dictionary<CDefinitionProprieteDynamique, int>();
			ArrayList lstSousArbres = new ArrayList ( arbre.SousArbres );
			foreach ( CArbreDefinitionsDynamiques sousArbre in lstSousArbres )
			{
				if ( sousArbre.DefinitionPropriete is CDefinitionProprieteDynamiqueChampCalcule )
				{
                    int nProfondeur = 0;
                    dicCountProfondeurParChamp.TryGetValue(sousArbre.DefinitionPropriete, out nProfondeur);
                    if (nProfondeur < 10)//En cas d'appel récursif, limite à 10 appels
                    {
                        CChampCalcule champCalc = new CChampCalcule(contexteDonnee);
                        if (champCalc.ReadIfExists(((CDefinitionProprieteDynamiqueChampCalcule)sousArbre.DefinitionPropriete).DbKeyChamp))
                        {
                            C2iExpression formule = champCalc.Formule;
                            if (formule != null)
                            {
                                CArbreDefinitionsDynamiques arbreDeFormule = new CArbreDefinitionsDynamiques(null);
                                formule.GetArbreProprietesAccedees(arbreDeFormule);
                                nProfondeur++;
                                dicCountProfondeurParChamp[sousArbre.DefinitionPropriete] = nProfondeur;
                                DetailleSousArbres(arbreDeFormule, contexteDonnee, dicCountProfondeurParChamp);
                                nProfondeur--;
                                dicCountProfondeurParChamp[sousArbre.DefinitionPropriete]--;
                                arbre.RemoveSousArbre(sousArbre);
                                foreach (CArbreDefinitionsDynamiques arbreTmp in arbreDeFormule.SousArbres)
                                    arbre.AddSousArbre(arbreTmp);
                            }
                        }
                    }
				}
				else
                    DetailleSousArbres(sousArbre, contexteDonnee, dicCountProfondeurParChamp);
			}
		}

		
	}

	public class CInterpreteurProprieteDynamiqueChampCalcule : IInterpreteurProprieteDynamiqueAccedantADautresProprietes
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
			CObjetDonnee objetDonnee = objet as CObjetDonnee;
			if (objetDonnee == null)
			{
				result.Data = null;
				return result;
			}
            //TESTDBKEYOK
            CDbKey key = CDbKey.CreateFromStringValue(strPropriete);
			CContexteDonnee contexte = objetDonnee.ContexteDonnee;
			CChampCalcule champCalcule = new CChampCalcule(contexte);
			if ( !champCalcule.ReadIfExists (key ) )
			{
				result.EmpileErreur(I.T("Calculated field @1 doesn't exists|200023", strPropriete ));
				return result;
			}
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( objet ) ;
			return champCalcule.Formule.Eval ( ctx );
		}



		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20034"));
			return result;
		}

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            //TESTDBKEYOK
            CDbKey key = CDbKey.CreateFromStringValue(strPropriete);
			/*int nIdChamp = -1;
			try
			{
				nIdChamp = Int32.Parse ( strPropriete );
			}
			catch
			{
			}*/
			CChampCalcule champCalcule = new CChampCalcule(CContexteDonneeSysteme.GetInstance());
            C2iExpression formule = null;
			if ( champCalcule.ReadIfExists (key ) )
			{
                formule = champCalcule.Formule;
			}
			return new CInterpreteurProprieteDynamiqueFormule.COptimiseurProprieteDynamiqueFormule(formule);
        }

        public void AddProprietesAccedees(
            CArbreDefinitionsDynamiques arbre, 
            Type typeSource, 
            string strPropriete)
        {
            //TESTDBKEYOK
            CDbKey key = CDbKey.CreateFromStringValue(strPropriete);
            /*int nIdChamp = -1;
			try
			{
				nIdChamp = Int32.Parse ( strPropriete );
			}
			catch
			{
			}*/
			CChampCalcule champCalcule = new CChampCalcule(CContexteDonneeSysteme.GetInstance());
            C2iExpression formule = null;
            if (champCalcule.ReadIfExists(key))
            {
                formule = champCalcule.Formule;
                formule.GetArbreProprietesAccedees(arbre);
                CDefinitionProprieteDynamiqueChampCalcule.DetailleSousArbres(arbre, CContexteDonneeSysteme.GetInstance()); 
            }
        }
    }

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiquesChampsCalcules : IFournisseurProprieteDynamiquesSimplifie
	{
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesChampsCalcules());
		}


		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if ( objet == null )
				return lstProps.ToArray();
			Type tp = objet.TypeAnalyse;
			if ( tp == null )
				return lstProps.ToArray();

            if (!C2iFactory.IsInit())
                return lstProps.ToArray();

			CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
			CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, typeof(CChampCalcule));
			liste.Filtre = new CFiltreData(CChampCalcule.c_champTypeObjets + "=@1", tp.ToString());
			foreach (CChampCalcule champ in liste)
			{
				CDefinitionProprieteDynamiqueChampCalcule def = new CDefinitionProprieteDynamiqueChampCalcule(champ);
				def.HasSubProperties = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(def.TypeDonnee.TypeDotNetNatif);
                if ( def.Rubrique.Length == 0 )
				    def.Rubrique = I.T("Complementary informations|59");
				lstProps.Add ( def );
			}
			return lstProps.ToArray();
		}

	}

    [AutoExec("Autoexec")]
    public class CPreparateurArbreDefPropChampCalcule : IPreparateurTransformationArbreDefinitionsEnArbreSousPropListeObjetDonnee
    {
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterTransformateur ( CDefinitionProprieteDynamiqueChampCalcule.c_strCleType, typeof(CPreparateurArbreDefPropChampCalcule));
        }

        public void PrepareArbre(CArbreDefinitionsDynamiques arbre, CContexteDonnee contexte)
        {
            CDefinitionProprieteDynamiqueChampCalcule.DetailleSousArbres( arbre, contexte );
        }
    }
}
