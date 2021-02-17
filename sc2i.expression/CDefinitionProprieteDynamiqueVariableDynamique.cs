using System;

using sc2i.expression;
using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Indique une variable d'un filtre dynamique
	/// La propriété contient l'id de la variable
	/// </summary>
	[Serializable]
	[ReplaceClass("sc2i.data.dynamic.CDefinitionProprieteDynamiqueVariableFiltreDynamique")]
    [ReplaceClass("sc2i.data.dynamic.CDefinitionProprieteDynamiqueVariableDynamique")]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueVariableDynamique : CDefinitionProprieteDynamique
	{
		public const string c_strCleType = "VF";

        private IVariableDynamique m_variable = null;
		/// //////////////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueVariableDynamique()
		{
		}

		/// //////////////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueVariableDynamique( IVariableDynamique variable )
			:base (
			variable.Nom,
			variable.IdVariable,
			variable.TypeDonnee,
			CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif),
			false)
		{
            m_variable = variable;
		}

		/// //////////////////////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueVariable));
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueVariableDynamique(IVariableDynamique variable, bool bHasSubProperties)
            : base(
            variable.Nom,
            variable.IdVariable,
            variable.TypeDonnee,
            bHasSubProperties,
            false)
        {
            m_variable = variable;
        }
		/// //////////////////////////////////////////////////////////////////////
        public string IdChamp
        {
            get
            {
                string strCle = "";
                string strProp = "";
                if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(NomPropriete, ref strCle, ref strProp))
                    strProp = NomPropriete;
                return strProp;
            }
        }

        /// //////////////////////////////////////////////////////////////////////
        public override CObjetPourSousProprietes GetObjetPourAnalyseSousProprietes()
        {
            IVariableDynamiqueInstance varInstance = m_variable as IVariableDynamiqueInstance;
            if (varInstance != null)
                return varInstance.GetObjetPourAnalyseSousProprietes();
            return base.GetObjetPourAnalyseSousProprietes();
        }

		
	}

	//Conservée pour compatibilité
	/*internal class CDefinitionProprieteDynamiqueVariableFiltreDynamique : CDefinitionProprieteDynamiqueVariableDynamique
	{
	}*/

	public class CInterpreteurProprieteDynamiqueVariable : IInterpreteurProprieteDynamique
	{
		//-------------------------------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

 

		//-------------------------------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            string strIdVariable = strPropriete;

            IElementAVariables eltAVariables = objet as IElementAVariables;
            object valeur = null;
            if (eltAVariables != null)
                valeur = eltAVariables.GetValeurChamp(strIdVariable);
            if (valeur != null)
            {
                result.Data = valeur;
                return result;
            }
            //Cherche dans les objets associés à l'interpreteur de propriétés s'il y a
            //un eltAVariables
            object[] objetsSupplementaires = CInterpreteurProprieteDynamique.AssociationsSupplementaires.GetObjetsAssocies(objet);
            if (objetsSupplementaires != null)
            {
                foreach (object obj in objetsSupplementaires)
                {
                    eltAVariables = obj as IElementAVariables;
                    if (eltAVariables != null)
                    {
                        result.Data = eltAVariables.GetValeurChamp(strIdVariable);
                        if (result.Data != null)
                            return result;
                    }
                }
            }
            result.Data = null;
            return result;
        }

		/// ////////////////////////////////////////
		public CResultAErreur SetValue(object elementToModif, string strPropriete, object nouvelleValeur)
		{
			CResultAErreur result = CResultAErreur.True;
			IElementAVariables eltAVariable = elementToModif as IElementAVariables;
			if (eltAVariable == null)
			{
				//Cherche dans les objets associés à l'interpreteur de propriétés s'il y a
				//un eltAVariables
				object[] objetsSupplementaires = CInterpreteurProprieteDynamique.AssociationsSupplementaires.GetObjetsAssocies(elementToModif);
				if (objetsSupplementaires != null)
				{
					foreach (object obj in objetsSupplementaires)
					{
						eltAVariable = obj as IElementAVariables;
						if (eltAVariable != null)
							break;
					}
				}
				if (eltAVariable == null)
				{
					result.EmpileErreur(I.T("@1 : incorrect target|20020",strPropriete));
					return result;
				}
			}
            string strIdVariable = "";
			try
			{
                strIdVariable = strPropriete;
			}
			catch
			{
				result.EmpileErreur(I.T("Bad format for dynamic variable (@1)|20031", strPropriete));
				return result;
			}
			eltAVariable.SetValeurChamp(strIdVariable, nouvelleValeur);
			return result;
		}


        public class COptimiseurProprieteDynamiqueVariable : IOptimiseurGetValueDynamic
        {
            private string m_strIdVariable = "";

            public COptimiseurProprieteDynamiqueVariable ( string strIdVariable )
            {
                m_strIdVariable = strIdVariable;
            }

            public object  GetValue(object objet)
            {
                if (m_strIdVariable == "")
                    return null;
                object valeur = null;
			IElementAVariables eltAVariables = objet as IElementAVariables;
            if (eltAVariables != null)
            {
                valeur = eltAVariables.GetValeurChamp(m_strIdVariable);
                if (valeur != null)
                {
                    return valeur;
                }
                //Cherche dans les objets associés à l'interpreteur de propriétés s'il y a
                //un eltAVariables
                object[] objetsSupplementaires = CInterpreteurProprieteDynamique.AssociationsSupplementaires.GetObjetsAssocies(objet);
                if (objetsSupplementaires != null)
                {
                    foreach (object obj in objetsSupplementaires)
                    {
                        eltAVariables = obj as IElementAVariables;
                        if (eltAVariables != null)
                        {
                            valeur = eltAVariables.GetValeurChamp(m_strIdVariable);
                            if (valeur != null)
                            {
                                return valeur;
                            }
                        }
                    }
                }
                return null;
            }
                
			return eltAVariables.GetValeurChamp(m_strIdVariable);
            }

            public Type GetTypeRetourne()
            {
                return typeof(object);
            }
        }



        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            string strIdVariable = strPropriete;
            return new COptimiseurProprieteDynamiqueVariable(strIdVariable);
        }
    }

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiqueVariableDynamique : IFournisseurProprieteDynamiquesSimplifie
	{
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiqueVariableDynamique());
		}
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(
			CObjetPourSousProprietes objet,
			CDefinitionProprieteDynamique defParente)
		{
			if (objet == null)
				return new CDefinitionProprieteDynamique[0];
            try
            {
                IElementAVariablesDynamiques eltAVariables = objet.ElementAVariableInstance as IElementAVariablesDynamiques;
                if (eltAVariables != null)
                    return eltAVariables.GetProprietesInstance();
            }
            catch { }

            return new CDefinitionProprieteDynamique[0];
		}
	}
}
