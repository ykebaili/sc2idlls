using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.data;
using sc2i.process;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.formulaire;
using System.Collections.Generic;
using System.Data;
using sc2i.common.Referencement;

namespace sc2i.process
{
	/// <summary>
	/// Permet de copier un élément vers une autre élement du même type
	/// </summary>
	[AutoExec("Autoexec")]
    public class CActionCopierEntite : CActionLienSortantSimple, IElementAReferenceExterneExplicite
	{
        private C2iExpression m_expressionEntiteSource = null;
        private C2iExpression m_expressionEntiteDestination= null;
        //Liste de CDefinitionProprieteDynamique
        private CDefinitionProprieteDynamique[] m_lstProprietesDynamiquesACopier = new CDefinitionProprieteDynamique[0];
        //private int m_nIdDefinisseurChampsCustomEntiteDestination = -1;
        private CDbKey m_dbKeyDefinisseurChampsCustomEntiteDestination = null;
        /* TESTDBKEYTOK par XL*/

        private bool m_bCopieComplete = false;

		/// /////////////////////////////////////////
		public CActionCopierEntite( CProcess process )
			:base(process)
		{
            Libelle = I.T("Copy an entity|291");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Copy an entity|291"),
				I.T( "Create a new entity and initialise all its properties with the source entity properties values|292"),
				typeof(CActionCopierEntite),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
            AddIdVariablesExpressionToHashtable(m_expressionEntiteSource, table);
            AddIdVariablesExpressionToHashtable(m_expressionEntiteDestination, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
            //return 1; // : ajout de l'option "Copie complète"
            return 2; // Passage de Id en Dbkey
		}

        /// ////////////////////////////////////////////////////////
        public bool CopieComplete
        {
            get
            {
                return m_bCopieComplete;
            }
            set
            {
                m_bCopieComplete = value;
            }
        }



        //--------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        public C2iExpression ExpressionEntiteDestination
        {
            get
            {
                if (m_expressionEntiteDestination == null)
                    return new C2iExpressionConstante("");
                return m_expressionEntiteDestination;
            }
            set
            {
                m_expressionEntiteDestination = value;
            }
        }


        //--------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        public C2iExpression ExpressionEntiteSource
        {
            get
            {
                if (m_expressionEntiteSource == null)
                    return new C2iExpressionConstante("");
                return m_expressionEntiteSource;
            }
            set
            {
                m_expressionEntiteSource = value;
            }
        }


        //---------------------------------------------------------------------------
        public CDbKey DbKeyDefinisseurChampsCustomEntiteDestination
        {
            get 
            {
                return m_dbKeyDefinisseurChampsCustomEntiteDestination; 
            }
            set
            {
                m_dbKeyDefinisseurChampsCustomEntiteDestination = value; 
            }
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou défini la liste des propriétés dynamiques à copier
        /// </summary>
        public CDefinitionProprieteDynamique[] ProprietesDynamiquesACopier
        {
            get 
            {
                return m_lstProprietesDynamiquesACopier; 
            }
            set
            {
                m_lstProprietesDynamiquesACopier = value; 
            }
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

            // Traite la formule retournant l'objet source
            I2iSerializable objet = m_expressionEntiteSource;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionEntiteSource = (C2iExpression)objet;


            // Traite la formule retournant l'objet destination
            objet = m_expressionEntiteDestination;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionEntiteDestination = (C2iExpression)objet;

            if(nVersion < 2)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyDefinisseurChampsCustomEntiteDestination, null);
            else
                serializer.TraiteDbKey(ref m_dbKeyDefinisseurChampsCustomEntiteDestination);

            // Traite la liste des propriétés à copier
            ArrayList lst = new ArrayList(m_lstProprietesDynamiquesACopier);
            result = serializer.TraiteArrayListOf2iSerializable(lst);
            if (!result)
                return result;
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                m_lstProprietesDynamiquesACopier = (CDefinitionProprieteDynamique[])lst.ToArray(typeof(CDefinitionProprieteDynamique));
            }

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bCopieComplete);
            else
                m_bCopieComplete = false;

			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();

            if (m_expressionEntiteSource == null)
            {
                result.EmpileErreur(I.T("Indicate the source element formula|294"));
                return result;
            }
            if (m_expressionEntiteDestination == null)
            {
                result.EmpileErreur(I.T("Indicate the destination element formula|301"));
                return result;
            }

            if (m_expressionEntiteSource.TypeDonnee.IsArrayOfTypeNatif)
                result.EmpileErreur(I.T("The source entity formula return several entities|295"));
            else if (!typeof(IObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(m_expressionEntiteSource.TypeDonnee.TypeDotNetNatif))
                result.EmpileErreur(I.T("The source entity formula must return a work entity|296"));

            if (m_expressionEntiteDestination.TypeDonnee.IsArrayOfTypeNatif)
                result.EmpileErreur(I.T("The destination entity formula return several entities|302"));
            else if (!typeof(IObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(m_expressionEntiteDestination.TypeDonnee.TypeDotNetNatif))
                result.EmpileErreur(I.T("The destination entity formula must return a work entity|303"));


            if (!ExpressionEntiteDestination.TypeDonnee.TypeDotNetNatif.Equals(ExpressionEntiteSource.TypeDonnee.TypeDotNetNatif))
            {
                result.EmpileErreur(I.T("The destination object type doesn't match the source object type|297"));
                return result;
            }

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
            contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
            
            // Evalue la formule de l'entité destination
            result = ExpressionEntiteDestination.Eval(contexteEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Error during @1 formula evaluation|216", ExpressionEntiteDestination.ToString()));
                return result;
            }
            CObjetDonneeAIdNumeriqueAuto objetDestination = (CObjetDonneeAIdNumeriqueAuto)result.Data;

            // Evalue la formule de l'entité source
            result = ExpressionEntiteSource.Eval(contexteEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Error during @1 formula evaluation|216", ExpressionEntiteSource.ToString()));
                return result;
            }
            CObjetDonneeAIdNumeriqueAuto objetSource = (CObjetDonneeAIdNumeriqueAuto)result.Data;

            if (m_bCopieComplete)
            {
                //Copie toutes les propriétés, sauf les champs
                if (objetSource == null || objetDestination == null)
                    return result;
                Type tp = objetDestination.GetType();
                DataTable table = objetDestination.Row.Table;
                if (table == null)
                    return result;
                List<string> keys = new List<string>();
                foreach (DataColumn col in table.PrimaryKey)
                    keys.Add(col.ColumnName);
                objetDestination.ContexteDonnee.CopyRow(objetSource.Row, objetDestination.Row, keys.ToArray());
                IElementAChamps eltChampsDest = objetDestination as IElementAChamps;
                IElementAChamps eltChampsSource = objetSource as IElementAChamps;
                if (eltChampsDest == null || eltChampsSource == null)
                    return result;
                foreach (CRelationElementAChamp_ChampCustom relation in eltChampsSource.RelationsChampsCustom)
                {
                    eltChampsDest.SetValeurChamp(relation.ChampCustom, relation.Valeur);
                }
            }
            else
            {
                // Faire la copie de l'objet
                for (int i = 0; i < m_lstProprietesDynamiquesACopier.Length; i++)
                {
                    CDefinitionProprieteDynamique definitionPropriete = m_lstProprietesDynamiquesACopier[i];

                    // Si c'est un champs custom
                    if (definitionPropriete is CDefinitionProprieteDynamiqueChampCustom)
                    {
                        if (!(objetSource is IElementAChamps))
                        {
                            result.EmpileErreur(I.T("@1 : Incorrect custom field of source object|298", definitionPropriete.Nom));
                            return result;
                        }
                        if (!(objetDestination is IElementAChamps))
                        {
                            result.EmpileErreur(I.T("@1 : Incorrect custom field of destination object|299", definitionPropriete.Nom));
                            return result;
                        }
                        // Get la valeur du champ custom de l'objet source
                        object valeurChamp = ((IElementAChamps)objetSource).GetValeurChamp(((CDefinitionProprieteDynamiqueChampCustom)definitionPropriete).DbKeyChamp.StringValue);
                        // Set la valeur du champ custom de l'objet destination
                        result = ((IElementAChamps)objetDestination).SetValeurChamp(
                            ((CDefinitionProprieteDynamiqueChampCustom)definitionPropriete).DbKeyChamp.StringValue,
                            valeurChamp);
                    }
                    // Si c'est un champ normal de la base
                    else
                    {
                        string strProp = definitionPropriete.NomProprieteSansCleTypeChamp;
                        // Get la valeur de propriété de l'objet source
                        object valeurProp = CInterpreteurTextePropriete.GetValue(objetSource, strProp);
                        // Set la valeut de propriété de l'objet destination

                        if (!CInterpreteurTextePropriete.SetValue(objetDestination, strProp, valeurProp))
                        {
                            result.EmpileErreur(I.T("Property @1 : Failed to set the value|300", strProp));
                            return result;
                        }
                    }
                }
            }
                       
			return result;
		}

        //----------------------------------------------------------------------------
        /// <summary>
        /// Utilisée pour la recherche de références externes liées à cette Action
        /// </summary>
        /// <param name="contexteGetRef"></param>
        /// <returns></returns>
        public object[] GetReferencesExternesExplicites(CContexteGetReferenceExterne contexteGetRef)
        {
            if (contexteGetRef != null)
            {
                CContexteDonnee contexte = (CContexteDonnee)contexteGetRef.GetObjetAttache(typeof(CContexteDonnee));
                if (contexte != null)
                {
                    if (ExpressionEntiteDestination != null)
                    {
                        Type typeDestination = ExpressionEntiteDestination.TypeDonnee.TypeDotNetNatif;
                        if ((typeof(IElementAChamps)).IsAssignableFrom(typeDestination))
                        {
                            CRoleChampCustom role = CRoleChampCustom.GetRoleForType(typeDestination);
                            Type[] typeDefinisseursChamps = role.TypeDefinisseurs;

                            if (typeDefinisseursChamps.Length > 0)
                            {
                                CObjetDonneeAIdNumeriqueAuto definisseur = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance(typeDefinisseursChamps[0], new object[] { contexte });
                                if (DbKeyDefinisseurChampsCustomEntiteDestination != null)
                                {
                                    if (definisseur.ReadIfExists(DbKeyDefinisseurChampsCustomEntiteDestination))
                                        return new object[] { definisseur };
                                }
                            }
                        }
                    }
                }
            }

            return new object[] { };
        }

    }
}
