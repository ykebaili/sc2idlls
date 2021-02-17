using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Collections.ObjectModel;
using System.Drawing.Design;

namespace sc2i.formulaire
{
	[Serializable]
    [System.ComponentModel.Editor(typeof(CProprieteAffectationsProprietesEditor), typeof(UITypeEditor))]
	public class CAffectationsProprietes : I2iSerializable
	{
		private Dictionary<CDefinitionProprieteDynamique, C2iExpression> m_dicAffectations = new Dictionary<CDefinitionProprieteDynamique, C2iExpression>();
        private C2iExpression m_formuleCondition;
        private C2iExpression m_formuleGlobale = null;
        private string m_strLibelle = "";

		//--------------------------------------
		public CAffectationsProprietes()
		{
            m_formuleCondition = new C2iExpressionConstante(true);
            m_strLibelle = I.T("Affectation set|20021");
		}

		//--------------------------------------
		public void SetAffectation ( CDefinitionProprieteDynamique definition, C2iExpression formule )
		{
			m_dicAffectations[definition] = formule;
		}

        //--------------------------------------
        public C2iExpression GlobalFormula
        {
            get
            {
                return m_formuleGlobale;
            }
            set
            {
                m_formuleGlobale = value;
            }
        }

		//--------------------------------------
		public C2iExpression GetFormuleFor ( CDefinitionProprieteDynamique definition )
		{
			C2iExpression formule = null;
			m_dicAffectations.TryGetValue ( definition, out formule );
			return formule;
		}

		//--------------------------------------
		public ReadOnlyCollection<KeyValuePair<CDefinitionProprieteDynamique, C2iExpression>> GetAffectations()
		{
			List<KeyValuePair<CDefinitionProprieteDynamique, C2iExpression>> lst = new List<KeyValuePair<CDefinitionProprieteDynamique,C2iExpression>>();
			foreach ( KeyValuePair<CDefinitionProprieteDynamique, C2iExpression> aff in m_dicAffectations )
				lst.Add ( aff );
            lst.Sort((a, b) => a.Key.Nom.CompareTo(b.Key.Nom));
			return lst.AsReadOnly();
		}

		//--------------------------------------
		private int GetNumVersion()
		{
			return 2;
            //1 : ajout de la condition
            //2 : ajout de la formule globale
		}

        //--------------------------------------
        public string Libelle
        {
            get
            {
                if (m_strLibelle == "")
                    m_strLibelle = I.T("Affectation set|20021");
                return m_strLibelle;
            }
            set
            {
                m_strLibelle = value;
            }
        }

		//--------------------------------------
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			int nNbAffectations = 0 ;
			foreach (KeyValuePair<CDefinitionProprieteDynamique, C2iExpression> data in m_dicAffectations)
				if (data.Value != null)
					nNbAffectations++;
			serializer.TraiteInt(ref nNbAffectations);
			switch (serializer.Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (KeyValuePair<CDefinitionProprieteDynamique, C2iExpression> aff in m_dicAffectations)
					{
						if (aff.Value != null)
						{
							CDefinitionProprieteDynamique def = aff.Key;
							C2iExpression formule = aff.Value;
							result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
							if (result)
								result = serializer.TraiteObject<C2iExpression>(ref formule);
							if (!result)
								return result;
						}
					}
					break;
				case ModeSerialisation.Lecture:
					m_dicAffectations.Clear();
					for (int nAff = 0; nAff < nNbAffectations; nAff++)
					{
						CDefinitionProprieteDynamique def = null;
						C2iExpression formule = null;
						result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
						if (result)
							result = serializer.TraiteObject<C2iExpression>(ref formule);
						if (!result)
							return result;
						SetAffectation(def, formule);
					}
					break;
			}
            if (nVersion >= 1)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleCondition);
                if (!result)
                    return result;
                serializer.TraiteString(ref m_strLibelle);
            }
            if (nVersion >= 2)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleGlobale);
                if (!result)
                    return result;
            }
			return result;
		}

        //--------------------------------------
        public C2iExpression FormuleCondition
        {
            get
            {
                return m_formuleCondition;
            }
            set
            {
                m_formuleCondition = value;
            }
        }

		//--------------------------------------
		public CResultAErreur AffecteProprietes(
			object elementToModif,
			object sourceElement,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(sourceElement);
            CResultAErreur resTmp;
			foreach (KeyValuePair<CDefinitionProprieteDynamique, C2iExpression> aff in m_dicAffectations)
			{
				if (aff.Value != null)
				{
					 resTmp = aff.Value.Eval(ctx);
					if (!resTmp)
						result += resTmp;
					else
					{
						resTmp = CInterpreteurProprieteDynamique.SetValue(elementToModif, aff.Key.NomPropriete, resTmp.Data);
						if (!resTmp)
							result += resTmp;
					}
				}
			}
            if (m_formuleGlobale != null)
            {
                CDefinitionMultiSourceForExpression def = new CDefinitionMultiSourceForExpression(sourceElement,
                        new CSourceSupplementaire("NewElement", elementToModif));
                ctx = new CContexteEvaluationExpression(def);
                if (elementToModif is IAllocateurSupprimeurElements)
                    ctx.AttacheObjet(typeof(IAllocateurSupprimeurElements), elementToModif);
                resTmp = m_formuleGlobale.Eval(ctx);
                if (!resTmp)
                    result += resTmp;
            }

			return result;
		}

        //--------------------------------------------------
        public void SetAffectations(ReadOnlyCollection<KeyValuePair<CDefinitionProprieteDynamique, C2iExpression>> liste)
        {
            Dictionary<CDefinitionProprieteDynamique, C2iExpression> dic = new Dictionary<CDefinitionProprieteDynamique, C2iExpression>();
            foreach (KeyValuePair<CDefinitionProprieteDynamique, C2iExpression> aff in liste)
            {
                dic[aff.Key] = aff.Value;
            }
            m_dicAffectations = dic;
        }
    }
}
