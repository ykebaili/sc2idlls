using System;

using sc2i.expression;
using sc2i.common;
using System.Collections;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CDefinitionProprieteDynamiqueRelation.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueRelation : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "RL";
		CInfoRelation m_relation=null;

		public CDefinitionProprieteDynamiqueRelation( )
			:base()
		{
		}

		public CDefinitionProprieteDynamiqueRelation( 
			string strNomConvivial,
			string strPropriete,
			CInfoRelation relation,
			CTypeResultatExpression typeDonnee
			)
			:base( strNomConvivial, strPropriete, typeDonnee, true, true)
		{
			m_relation = relation;
		}

		//-----------------------------------------------
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueDotNet));
            CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
		}

        /// ////////////////////////////////////////////////////////////////
        public static void FindRelation(string strTable, Type type, ref CInfoRelationComposantFiltre relationTrouvee)
        {
            if (relationTrouvee != null)
                return;
            string strPropDef = "";
            string strCleDef = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strTable, ref strCleDef, ref strPropDef))
            {
                if (strCleDef != c_strCleType)
                    return;
                CStructureTable structure = CStructureTable.GetStructure(type);
                foreach (CInfoRelation relation in structure.RelationsFilles)
                    if (relation.Propriete == strPropDef)
                    {
                        relationTrouvee = new CInfoRelationComposantFiltreStd(
                            relation,
                            true, 0);
                        return;
                    }
                foreach (CInfoRelation relation in structure.RelationsParentes)
                    if (relation.Propriete == strPropDef)
                    {
                        relationTrouvee = new CInfoRelationComposantFiltreStd(
                            relation,
                            false, 0);
                        return;
                    }
            }


        }

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueRelation)def).m_relation = m_relation;
		}

		/// ////////////////////////////////////////
		public CInfoRelation Relation
		{
			get
			{
				return m_relation;
			}
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
			I2iSerializable obj = m_relation;
			result = serializer.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_relation = (CInfoRelation)obj;
			return result;
		}

		
	}

   

}
