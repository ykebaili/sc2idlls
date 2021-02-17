using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.expression
{
    //-------------------------------------------------------------------------------------
	/// <summary>
	/// Description résumée de IElementAVariables.
	/// </summary>
    public interface IElementAVariables
    {
        CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur);
        CResultAErreur SetValeurChamp(string strCleVariable, object valeur);
        object GetValeurChamp(IVariableDynamique variable);
        object GetValeurChamp(string strCleVariable);
    }

    //-------------------------------------------------------------------------------------
    public interface IElementAVariablesDynamiquesBase : IElementAVariables, IElementAVariableInstance
    {
        string GetNewIdForVariable();

        void OnChangeVariable(IVariableDynamique variable);

        //Retourne la liste des variables
        IVariableDynamique[] ListeVariables { get; }

        void AddVariable(IVariableDynamique variable);
        void RemoveVariable(IVariableDynamique variable);


        bool IsVariableUtilisee(IVariableDynamique variable);

        CVariableDynamique GetVariable(string strIdVariable);
    }

    //-------------------------------------------------------------------------------------
    public interface IElementAVariablesDynamiques : IElementAVariablesDynamiquesBase, IFournisseurProprietesDynamiques, IElementAVariableInstance
	{
	}

    //-------------------------------------------------------------------------------------
    public interface IElementAVariablesDynamiquesAGestionParInstance : IElementAVariablesDynamiquesBase
    {
    }

	/*
	/// <summary>
	/// Permet de gérer et les propriétés d'un type (ou d'un objet)+
	/// les variables d'un IElementAVariables.
	/// Utilisé par exemple dans les structures d'export.
	/// </summary>
	public class CMelangeurProprietesTypeEtElementAVariables : IApplatisseurProprietes, IElementAVariables, IElementAVariableInstance
	{
		private CObjetPourSousProprietes m_objetPourSousProprietePrincipales;
		private object m_objetPrincipal = null;
		private IElementAVariablesDynamiques m_elementAVariables;

		private Dictionary<string, bool> m_defsDeElementAVariables = null;
		private string m_strRubriqueProprietesElementAVariables = "";

		public CMelangeurProprietesTypeEtElementAVariables(
			CObjetPourSousProprietes objetPourSousProprietes,
			IElementAVariablesDynamiques elementAVariables,
			string strRubriqueProprietesElementAVariables)
		{
			m_objetPourSousProprietePrincipales = objetPourSousProprietes;
			m_elementAVariables = elementAVariables;
			m_strRubriqueProprietesElementAVariables = strRubriqueProprietesElementAVariables;
		}

		public CMelangeurProprietesTypeEtElementAVariables(
			object objetPrincipal,
			IElementAVariablesDynamiques elementAVariables)
		{
			m_objetPrincipal = objetPrincipal;
			if ( objetPrincipal != null )
				m_objetPourSousProprietePrincipales = new CObjetPourSousProprietes(objetPrincipal.GetType());
			m_elementAVariables = elementAVariables;
		}

		//-----------------------------------------------------------
		public CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietePrincipales;
			}
		}

		//-----------------------------------------------------------
		public IElementAVariablesDynamiques ElementAVariables
		{
			get
			{
				return m_elementAVariables;
			}
		}

		//-----------------------------------------------------------
		public string RubriqueProprietesElementAVariables
		{
			get
			{
				return m_strRubriqueProprietesElementAVariables;
			}
		}

		//-----------------------------------------------------------
		public object GetObjetPourPropriete(string strPropriete)
		{
			string[] strProps = strPropriete.Split('.');
			if ( strProps.Length < 1 )
				return m_objetPrincipal;
			string strProp = strProps[0];
			if (m_elementAVariables != null)
			{
				if (m_defsDeElementAVariables == null)
				{
					foreach (CDefinitionProprieteDynamique prop in m_elementAVariables.GetDefinitionsChamps(new CObjetPourSousProprietes(m_elementAVariables)))
						m_defsDeElementAVariables[prop.NomPropriete] = true;
				}
				if ( m_defsDeElementAVariables.ContainsKey ( strProp ))
					return m_elementAVariables;
			}
			return m_objetPourSousProprietePrincipales;
		}

		//-----------------------------------------------------------
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_elementAVariables != null)
				result = m_elementAVariables.SetValeurChamp(variable, valeur);
			return result;
		}

		//-----------------------------------------------------------
		public CResultAErreur SetValeurChamp(int nIdVariable, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_elementAVariables != null)
				result = m_elementAVariables.SetValeurChamp(nIdVariable, valeur);
			return result;
		}

		//-----------------------------------------------------------
		public object GetValeurChamp(IVariableDynamique variable)
		{
			if (m_elementAVariables != null)
				return m_elementAVariables.GetValeurChamp(variable);
			return null;
		}

		//-----------------------------------------------------------
		public object GetValeurChamp(int nIdVariable)
		{
			if (m_elementAVariables != null)
				return m_elementAVariables.GetValeurChamp(nIdVariable);
			return null;
		}



		#region IElementAVariableInstance Membres

		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (m_objetPourSousProprietePrincipales != null)
				lst.AddRange(new CFournisseurGeneriqueProprietesDynamiques().GetDefinitionsChamps(m_objetPourSousProprietePrincipales));
			if ( m_elementAVariables != null )
				lst.AddRange(m_elementAVariables.GetDefinitionsChampsVariables ( ));
			return new CDefinitionProprieteDynamique[0];
		}

		#endregion

		
	}

	//-----------------------------------------------------------
	[AutoExec("Autoexec")]
	public class CFournisseurProprietesMelangeurProprietesTypeEtElementAVariables : IFournisseurProprieteDynamiquesSimplifie
	{
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesMelangeurProprietesTypeEtElementAVariables());
		}
		#region IFournisseurProprieteDynamiquesSimplifie Membres

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			CMelangeurProprietesTypeEtElementAVariables melangeur = objet.ElementAVariableInstance as CMelangeurProprietesTypeEtElementAVariables;
			if (melangeur != null)
			{
				if ( melangeur.ObjetPourSousProprietes != null )
					lst.AddRange(new CFournisseurGeneriqueProprietesDynamiques().GetDefinitionsChamps(melangeur.ObjetPourSousProprietes, defParente));
				if ( melangeur.ElementAVariables != null )
				{
					foreach ( CDefinitionProprieteDynamique def in melangeur.ElementAVariables.GetDefinitionsChamps ( melangeur.ElementAVariables.GetType() ))
					{
						def.Rubrique = melangeur.RubriqueProprietesElementAVariables;
						lst.Add ( def );
					}
				}
			}
			return lst.ToArray();
		}

		#endregion
	}

	*/
	
}
