using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
	[Serializable]
	public abstract class CDefinitionProprieteDynamiqueInstance : CDefinitionProprieteDynamique
	{
		private object m_instance = null;

		//------------------------------------------------
		public CDefinitionProprieteDynamiqueInstance()
			: base()
		{
		}

		//------------------------------------------------
		public CDefinitionProprieteDynamiqueInstance(
			string strNomConvivial,
			string strNomPropriete,
			object instance,
			string strRubrique )
			: base(
			strNomConvivial,
			strNomPropriete,
			new CTypeResultatExpression ( instance.GetType(), false ),
			true,
			false, 
			strRubrique )
		{
			m_instance = instance;
		}

        //------------------------------------------------
        public CDefinitionProprieteDynamiqueInstance(
            string strNomConvivial,
            string strNomPropriete,
            object instance,
            bool bIsArray,
            string strRubrique)
            : base(
            strNomConvivial,
            strNomPropriete,
            new CTypeResultatExpression(instance.GetType(), true),
            true,
            false,
            strRubrique)
        {
            m_instance = instance;
        }

		//------------------------------------------------
		public object Instance
		{
			get
			{
				return m_instance;
			}
		}

        //------------------------------------------------
        public override CObjetPourSousProprietes GetObjetPourAnalyseSousProprietes()
		{
			if (Instance != null)
				return new CObjetPourSousProprietes(Instance);
			return base.GetObjetPourAnalyseSousProprietes();
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			CDefinitionProprieteDynamiqueInstance defInstance = def as CDefinitionProprieteDynamiqueInstance;
			if (defInstance != null)
				defInstance.m_instance = m_instance;
		}

	}

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiquesInstance : IFournisseurProprieteDynamiquesSimplifie
	{
		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesInstance());
		}


		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
            try
            {
                if (objet.ElementAVariableInstance != null)
                {
                    return objet.ElementAVariableInstance.GetProprietesInstance();
                }
            }
            catch { }

            return new CDefinitionProprieteDynamique[0];
		}
	}
}
