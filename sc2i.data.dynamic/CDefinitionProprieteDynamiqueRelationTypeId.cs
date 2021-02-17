using System;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;
using System.Data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Propriété définie par une relation TypeId dans le sens parent->Fils
	/// La propriété contient l'id de la relation
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueRelationTypeId : CDefinitionProprieteDynamique
	{
		public const string c_strCleType = "TI";
		protected RelationTypeIdAttribute m_relation;

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueRelationTypeId()
			: base()
		{
			Rubrique = I.T("Common|55");
		}
		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueRelationTypeId(RelationTypeIdAttribute relation)
		{
			m_relation = relation;
			InitBase();
			Rubrique = I.T("Common|55");
		}

		/// //////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueRelationTypeId));
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////
		private void InitBase()
		{
			m_strNomConvivial = m_relation.NomConvivialPourParent.Replace(" ", "_");
			m_strNomPropriete = c_strCaractereStartCleType+CleType+c_strCaractereEndCleType+m_relation.IdRelation;
			m_typeDonnee = new CTypeResultatExpression(CContexteDonnee.GetTypeForTable(m_relation.TableFille), true);
			m_bHasSubProprietes = true;
			m_bIsReadOnly = true;
		}

		/// //////////////////////////////////////////////////////
		public RelationTypeIdAttribute Relation
		{
			get
			{
				return m_relation;
			}
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueRelationTypeId)def).m_relation = (RelationTypeIdAttribute)CCloner2iSerializable.Clone(m_relation);
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			//N'appelle pas le base.Serialize, il ne stocke rien d'interressant 
			I2iSerializable objet = (I2iSerializable)m_relation;
			result = serializer.TraiteObject(ref objet);
			m_relation = (RelationTypeIdAttribute)objet;

			InitBase();
			return result;
		}

	}

    [AutoExec("Autoexec")]
	public class CInterpreteurProprieteDynamiqueRelationTypeId : 
        IInterpreteurProprieteDynamique,
        IDependanceListeObjetsDonneesReader
	{

        //------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader(
                CDefinitionProprieteDynamiqueRelationTypeId.c_strCleType,
                typeof(CInterpreteurProprieteDynamiqueRelationTypeId));
        }

		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			CObjetDonneeAIdNumerique objetDonnee = objet as CObjetDonneeAIdNumerique;
			if ( objetDonnee == null )
				return result;
			//trouve la relation correspondante à l'identifiant de la propriété
			RelationTypeIdAttribute relation = null;
			foreach ( RelationTypeIdAttribute relTest in CContexteDonnee.RelationsTypeIds )
				if (relTest.IdRelation == strPropriete)
				{
					relation = relTest;
					break;
				}
			if ( relation == null )
			{
				result.EmpileErreur(I.T("Relation @1 doesn't exists|20030", strPropriete ));
				return result;
			}
			CListeObjetsDonnees valeur = objetDonnee.GetDependancesRelationTypeId ( 
				relation.TableFille,
				relation.ChampType,
				relation.ChampId,
				false );
			if ( valeur != null )
				result.Data = valeur.ToArrayList();
			return result;			
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20034"));
			return result;
		}


        public class COptimiseurProprieteDynamiqueTypeId : IOptimiseurGetValueDynamic
        {
            RelationTypeIdAttribute m_relation = null;

            public COptimiseurProprieteDynamiqueTypeId ( RelationTypeIdAttribute relation )
            {
                m_relation = relation;
            }

            public object  GetValue(object objet)
            {
                if ( m_relation == null )
                    return null;
			    CObjetDonneeAIdNumerique objetDonnee = objet as CObjetDonneeAIdNumerique;
			    if ( objetDonnee == null )
				    return null;
			    CListeObjetsDonnees valeur = objetDonnee.GetDependancesRelationTypeId ( 
				    m_relation.TableFille,
				    m_relation.ChampType,
				    m_relation.ChampId,
				    false );
                if ( valeur != null )
                    return valeur.ToArrayList();
                return null;
            }

            public Type GetTypeRetourne()
            {
                if (m_relation != null)
                    return CContexteDonnee.GetTypeForTable(m_relation.TableFille);
                return null;
            }
        }


        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            RelationTypeIdAttribute relation = null;
			foreach ( RelationTypeIdAttribute relTest in CContexteDonnee.RelationsTypeIds )
				if (relTest.IdRelation == strPropriete)
				{
					relation = relTest;
					break;
				}
            return new COptimiseurProprieteDynamiqueTypeId ( relation );
        }

        
        //**********************************************************************************
        public void ReadArbre(CListeObjetsDonnees listeSource, CListeObjetsDonnees.CArbreProps arbre, List<string> lstPaquetsALire)
        {
            if ( listeSource.Count == 0 )
                return ;
            CObjetDonneeAIdNumerique objExemple = listeSource[0] as CObjetDonneeAIdNumerique;
            if ( objExemple == null )
                return;
            string strCle = "";
            string strPropSansCle = "";
            if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(arbre.ProprietePrincipale, ref strCle, ref strPropSansCle))
                return;
			//trouve la relation correspondante à l'identifiant de la propriété
			RelationTypeIdAttribute relation = null;
			foreach ( RelationTypeIdAttribute relTest in CContexteDonnee.RelationsTypeIds )
				if (relTest.IdRelation == strPropSansCle)
                {
                    relation = relTest;
                    break;
                }
            if ( relation == null )
                return;

            string strNomColDep = relation.GetNomColDepLue();
            DataColumn col = objExemple.Table.Columns[strNomColDep];
            if ( col == null )
            {
                col = new DataColumn(strNomColDep, typeof(bool));
                col.DefaultValue = false;
                col.AllowDBNull = false;
                objExemple.Table.Columns.Add(col);
            }

            if ( lstPaquetsALire == null )
                lstPaquetsALire = listeSource.GetPaquetsPourLectureFils ( objExemple.GetChampId(), col  );

            foreach (string strPaquet in lstPaquetsALire)
            {
                if (strPaquet != null && strPaquet.Trim().Length > 0)
                {
                    CListeObjetsDonnees lst = new CListeObjetsDonnees(listeSource.ContexteDonnee,
                        CContexteDonnee.GetTypeForTable(relation.TableFille));
                    lst.Filtre = new CFiltreData(
                        relation.ChampType + "=@1 and " +
                        relation.ChampId + " in " + strPaquet,
                        listeSource.TypeObjets.ToString());
                    lst.AssureLectureFaite();
                    lst.ReadDependances(arbre);
                }
            }
            
            foreach (CObjetDonneeAIdNumerique obj in listeSource)
                CContexteDonnee.ChangeRowSansDetectionModification(obj.Row, strNomColDep, true);               
                
        }

    }

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiquesTypeId : IFournisseurProprieteDynamiquesSimplifie
	{
        private static Dictionary<Type, CDefinitionProprieteDynamique[]> m_cache = new Dictionary<Type, CDefinitionProprieteDynamique[]>();

		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesTypeId());
		}

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if (objet == null)
				return lstProps.ToArray();
			Type tp = objet.TypeAnalyse;
			if (tp == null)
				return lstProps.ToArray();
            CDefinitionProprieteDynamique[] defCache = null;
            if (m_cache.TryGetValue(tp, out defCache))
                return defCache;

			//Relations TypeID
			foreach (RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds)
			{
				if (relation.NomConvivialPourParent != "" && relation.IsAppliqueToType(tp))
				{
					CDefinitionProprieteDynamiqueRelationTypeId def = new CDefinitionProprieteDynamiqueRelationTypeId(relation);
					lstProps.Add(def);
				}
			}
            defCache = lstProps.ToArray();
            m_cache[tp] = defCache;
			return defCache;
		}
	}

    [AutoExec("Autoexec")]
    public class CFournisseurPropInverseTypeId : IFournisseurProprieteDynamiqueInverse
    {
        public static void Autoexec()
        {
            CFournisseurProprieteDynamiqueInverse.RegisterFournisseur ( typeof(CDefinitionProprieteDynamiqueRelationTypeId), typeof(CFournisseurPropInverseTypeId) );
        }
        public CDefinitionProprieteDynamique GetProprieteInverse(Type typePortantLaPropriete, CDefinitionProprieteDynamique def)
        {
            CDefinitionProprieteDynamiqueRelationTypeId defId = def as CDefinitionProprieteDynamiqueRelationTypeId;
            if ( defId == null )
                return null;
            return new CDefinitionProprieteDynamiqueRelationTypeIdToParent ( defId.Relation, typePortantLaPropriete );
        }
    }
     
}
