using System;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;
using System.Data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Propriété définie par une relation TypeId dans le sens enfant->Parent
	/// La propriété contient l'id de la relation
    /// Ce type de propriété n'est pas retournée par un fournisseur de propriétés,
    /// elle sert simplement de propriété inverse pour les CDefinitionProprieteDynamiqueRelationTypeId
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueRelationTypeIdToParent : CDefinitionProprieteDynamique
	{
		public const string c_strCleType = "TIP";
		protected RelationTypeIdAttribute m_relation;
        private Type m_typeParent; //Type parent de la relation. 

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueRelationTypeIdToParent()
			: base()
		{
			Rubrique = I.T("Common|55");
		}
		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueRelationTypeIdToParent(
            RelationTypeIdAttribute relation,
            Type typeParent)
		{
			m_relation = relation;
            m_typeParent = typeParent;
			InitBase();
			Rubrique = I.T("Common|55");
		}

		/// //////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueRelationTypeIdToParent));
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////
		private void InitBase()
		{
			m_strNomConvivial = m_relation.NomConvivialPourParent.Replace(" ", "_")+"inv";
			m_strNomPropriete = c_strCaractereStartCleType+CleType+c_strCaractereEndCleType+m_relation.IdRelation;
			m_typeDonnee = new CTypeResultatExpression(
                m_typeParent == null?typeof(CObjetDonneeAIdNumeriqueAuto):m_typeParent, true);
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
			((CDefinitionProprieteDynamiqueRelationTypeIdToParent)def).m_relation = (RelationTypeIdAttribute)CCloner2iSerializable.Clone(m_relation);
            ((CDefinitionProprieteDynamiqueRelationTypeIdToParent)def).m_typeParent = m_typeParent;
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

            bool bHasTypeParent = m_typeParent != null;
            serializer.TraiteBool(ref bHasTypeParent);
            if ( bHasTypeParent )
                serializer.TraiteType(ref m_typeParent);

			InitBase();
			return result;
		}

	}

    [AutoExec("Autoexec")]
    public class CInterpreteurProprieteDynamiqueRelationTypeIdToParent :
        IInterpreteurProprieteDynamique
    {

        //------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader(
                CDefinitionProprieteDynamiqueRelationTypeIdToParent.c_strCleType,
                typeof(CInterpreteurProprieteDynamiqueRelationTypeIdToParent));
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
            CObjetDonneeAIdNumerique objetTypeId = objet as CObjetDonneeAIdNumerique;
            if (objetTypeId == null)
                return result;
            //trouve la relation correspondante à l'identifiant de la propriété
            RelationTypeIdAttribute relation = null;
            foreach (RelationTypeIdAttribute relTest in CContexteDonnee.RelationsTypeIds)
                if (relTest.IdRelation == strPropriete)
                {
                    relation = relTest;
                    break;
                }
            if (relation == null)
            {
                result.EmpileErreur(I.T("Relation @1 doesn't exists|20030", strPropriete));
                return result;
            }
            CObjetDonneeAIdNumerique objetParent = null;
            //Récupère le type de l'objet parent
            try
            {
                Type tp = CActivatorSurChaine.GetType((string)objetTypeId.Row[relation.ChampType], false);
                objetParent = Activator.CreateInstance(tp, new object[] { objetTypeId.ContexteDonnee }) as CObjetDonneeAIdNumerique;
                if (!objetParent.ReadIfExists((int)objetTypeId.Row[relation.ChampId]))
                    objetParent = null;
            }
            catch { }
            result.Data = objetParent;
            return result;
        }

        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            CResultAErreur result = CResultAErreur.True;
            RelationTypeIdAttribute relation = null;
            CObjetDonneeAIdNumerique objetTypeId = objet as CObjetDonneeAIdNumerique;
            if (objetTypeId == null)
                return result;
            CObjetDonneeAIdNumerique objetValeur = objet as CObjetDonneeAIdNumerique;
            if (objetValeur == null)
                return result;
            foreach (RelationTypeIdAttribute relTest in CContexteDonnee.RelationsTypeIds)
                if (relTest.IdRelation == strPropriete)
                {
                    relation = relTest;
                    break;
                }
            if (relation == null)
            {
                result.EmpileErreur(I.T("Relation @1 doesn't exists|20030", strPropriete));
                return result;
            }
            try
            {
                objetTypeId.Row[relation.ChampId] = objetValeur.Id;
                objetTypeId.Row[relation.ChampType] = objetValeur.GetType().ToString();
            }
            catch
            {
            }
            return result;
        }

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }
    }
}
