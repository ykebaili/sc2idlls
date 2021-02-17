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

namespace sc2i.process
{

    /*public class CDicProprietesParType : Dictionary<Type, List<CDefinitionProprieteDynamique>>, I2iSerializable
    {
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public void AddDefinition(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (!TryGetValue(tp, out lst))
            {
                lst = new List<CDefinitionProprieteDynamique>();
                this[tp] = lst;
            }
            if (!lst.Contains(def))
                lst.Add(def);
        }

        //----------------------------------------------
        public void RemoveDefinition(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (TryGetValue(tp, out lst))
            {
                lst.Remove(def);
                if (lst.Count == 0)
                    Remove(tp);
            }
        }

        //----------------------------------------------
        public bool Contains(Type tp, CDefinitionProprieteDynamique def)
        {
            List<CDefinitionProprieteDynamique> lst = null;
            if (TryGetValue(tp, out lst))
                return lst.Contains(def);
            return false;
        }

        //----------------------------------------------
        public CDicProprietesParType GetClone()
        {
            CDicProprietesParType dic = new CDicProprietesParType();
            foreach (KeyValuePair<Type, List<CDefinitionProprieteDynamique>> kv in this)
            {
                foreach (CDefinitionProprieteDynamique def in kv.Value)
                    dic.AddDefinition(kv.Key, def);
            }
            return dic;
        }

        

        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            int nNbDics = Count;
            serializer.TraiteInt ( ref nNbDics );
            switch ( serializer.Mode )
            {
                case ModeSerialisation.Ecriture :
                    foreach ( KeyValuePair<Type, List<CDefinitionProprieteDynamique>> paire in this )
                    {
                        Type tp = paire.Key;
                        List<CDefinitionProprieteDynamique> lst = paire.Value;
                        serializer.TraiteType ( ref tp );
                        result = serializer.TraiteListe<CDefinitionProprieteDynamique>( lst );
                        if ( !result )
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture :
                    Clear();
                    for ( int nPaire = 0; nPaire < nNbDics; nPaire++ )
                    {
                        Type tp = null;
                        List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
                        serializer.TraiteType ( ref tp );
                        if ( result )
                            result = serializer.TraiteListe<CDefinitionProprieteDynamique>(lst);
                        if ( !result )
                            return result;
                        this[tp] = lst;
                    }
                    break;
            }
            return result;
        }
    }*/

	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionClonerEntite : CActionFonction
	{
        private C2iExpression m_formuleElementACloner = null;

        private COptionsClonageEntite m_optionsClonage = new COptionsClonageEntite();
        


		/// /////////////////////////////////////////
		public CActionClonerEntite( CProcess process )
			:base(process)
		{
			Libelle = I.T("Clone an entity|20046");
			VariableRetourCanBeNull = true;
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Clone an entity|20046"),
				I.T( "Clone an entity and its dependancies|20047"),
				typeof(CActionClonerEntite),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

        /// /////////////////////////////////////////////////////////
        public C2iExpression FormuleElementACloner
        {
            get
            {
                return m_formuleElementACloner;
            }
            set
            {
                m_formuleElementACloner = value;
            }
        }

        /// /////////////////////////////////////////////////////////
        public COptionsClonageEntite OptionsClonage
        {
            get
            {
                return m_optionsClonage;
            }
            set
            {
                m_optionsClonage = value;
            }
        }

		/// /////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
                if (FormuleElementACloner != null)
                    return FormuleElementACloner.TypeDonnee;
                return new CTypeResultatExpression(typeof(DBNull), false);
			}
		}


		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			base.AddIdVariablesNecessairesInHashtable ( table );
            if ( FormuleElementACloner != null )
                AddIdVariablesExpressionToHashtable ( FormuleElementACloner, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
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

            bool bHasHadObjetContexte = false;

            if (serializer.GetObjetAttache(typeof(CContexteDonnee)) == null)
            {
                bHasHadObjetContexte = true;
                serializer.AttacheObjet(typeof(CContexteDonnee), Process.ContexteDonnee);
            }

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementACloner);
            if (result)
                result = serializer.TraiteObject<COptionsClonageEntite>(ref m_optionsClonage);
            if (!result)
                return result;

			if ( bHasHadObjetContexte )
				serializer.DetacheObjet ( typeof(CContexteDonnee), Process.ContexteDonnee );
			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();

            if ( FormuleElementACloner == null ||
                !typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(FormuleElementACloner.TypeDonnee.TypeDotNetNatif) ||
                FormuleElementACloner.TypeDonnee.IsArrayOfTypeNatif )
            {
                result.EmpileErreur(I.T("Clone formula should return an entity|20048"));
                return result;
            }
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(Process);
            result = FormuleElementACloner.Eval(ctx);
            if (!result)
                return result;
            CObjetDonneeAIdNumeriqueAuto obj = result.Data as CObjetDonneeAIdNumeriqueAuto;
            if (obj == null)
            {
                result.EmpileErreur(I.T("Erreur while cloning object : source is null|20049"));
                return result;
            }

            CResultAErreurType<CObjetDonneeAIdNumeriqueAuto> resultClone = OptionsClonage.CloneObjet(obj);
            /*

            Dictionary<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto> dicClones = new Dictionary<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto>();
            result = CloneObjet(contexte.ContexteDonnee, obj, dicClones);
            if (!result)
                return result;

            Dictionary<object,object> copie = new Dictionary<object,object>();
            foreach ( KeyValuePair<CObjetDonneeAIdNumeriqueAuto, CObjetDonneeAIdNumeriqueAuto> kv in dicClones )
                copie[kv.Key] = kv.Value;
            foreach (CObjetDonneeAIdNumeriqueAuto objet in dicClones.Values)
            {
                if (objet.GetType().GetCustomAttributes(typeof(RefillAfterCloneAttribute), true).Length > 0)
                {
                    RefillAfterCloneAttribute.RefillAfterClone(objet, copie);
                }
            }*/
            if (!resultClone)
                result.EmpileErreur(resultClone.Erreur);


            if (resultClone && VariableResultat != null)
			    if ( VariableResultat != null )
                    Process.SetValeurChamp(VariableResultat, resultClone.DataType);
			
            CLienAction[] liens = GetLiensSortantHorsErreur();
			if ( liens.Length > 0 )
				result.Data = liens[0];
			return result;
		}
        /*
        //-----------------------------------------------------------
        private CResultAErreur CloneObjet(
            CContexteDonnee contexte, 
            CObjetDonneeAIdNumeriqueAuto source, 
            Dictionary<CObjetDonneeAIdNumeriqueAuto,CObjetDonneeAIdNumeriqueAuto> dicClones)
        {
            CResultAErreur result = CResultAErreur.True;
            if (dicClones.ContainsKey(source))
            {
                result.Data = dicClones[source];
                return result;
            }
            if (dicClones.ContainsValue(source))
            {
                //C'est un objet qu'on a cloné, on ne va as le recloner encore
                result.Data = source;
                return result;
            }
            CObjetDonneeAIdNumeriqueAuto clone = Activator.CreateInstance(source.GetType(), new object[] { contexte }) as CObjetDonneeAIdNumeriqueAuto;
            clone.CreateNewInCurrentContexte();
            dicClones[source] = clone;
            //Copie la ligne
            DataTable table = source.Row.Table;
            if (table == null)
                return result;
            List<string> keys = new List<string>();
            foreach (DataColumn col in table.PrimaryKey)
                keys.Add(col.ColumnName);
            clone.ContexteDonnee.CopyRow(source.Row, clone.Row, keys.ToArray());

            //Copie toutes les valeurs de champ custom
            IElementAChamps eltChampsDest = clone as IElementAChamps;
            IElementAChamps eltChampsSource = source as IElementAChamps;
            if (eltChampsDest != null && eltChampsSource != null)
            {
                foreach (CRelationElementAChamp_ChampCustom relation in eltChampsSource.RelationsChampsCustom)
                {
                    eltChampsDest.SetValeurChamp(relation.ChampCustom, relation.Valeur);
                }
            }

            //Copie les relations filles sélectionnées
            List<CDefinitionProprieteDynamique> lst = null;
            if ( m_optionsClonage.TryGetValue(source.GetType(), out lst ))
            {
                foreach (CDefinitionProprieteDynamique def in lst)
                {
                    CDefinitionProprieteDynamique defInverse = def.GetDefinitionInverse(source.GetType());
                    if ( defInverse != null )//On sait affecter le parent
                    {
                        result = CInterpreteurProprieteDynamique.GetValue(source, def);
                        if (result && result.Data is IEnumerable)
                        {
                            IEnumerable en = result.Data as IEnumerable;
                            foreach (object obj in en)
                            {
                                CObjetDonneeAIdNumeriqueAuto objDonnee = obj as CObjetDonneeAIdNumeriqueAuto;
                                if (objDonnee != null)
                                {
                                    result = CloneObjet(contexte, objDonnee, dicClones);
                                    if (result)
                                    {
                                        result = CInterpreteurProprieteDynamique.SetValue(result.Data, defInverse, clone);
                                        if (!result)
                                            return result;
                                    }
                                    else
                                        return result;
                                }
                            }
                        }
                    }
                }
            }

            result.Data = clone;
            return result;
        }*/
	}
}
