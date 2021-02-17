using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic.Macro
{
    public class CMacroObjetValeur : I2iSerializable
    {
        private CMacroObjet m_macroObjet = null;
        private C2iExpression m_formuleValeur = null;
        private CDefinitionProprieteDynamique m_champ = null;
        
        //Contient le GUID d'un objet modifié par la même macro. Si non vide->pas de formule de valeur
        private string m_strGUIDReference = "";

        public CMacroObjetValeur()
        {
        }

        public CMacroObjetValeur(CMacroObjet macroObjet)
        {
            m_macroObjet = macroObjet;
        }

        //----------------------------------------------------
        public CMacroObjet MacroObjet
        {
            get
            {
                return m_macroObjet;
            }
        }

        //----------------------------------------------------
        public C2iExpression FormuleValeur
        {
            get
            {
                return m_formuleValeur;
            }
            set
            {
                m_formuleValeur = value;
            }
        }

        //----------------------------------------------------
        public string IdReference
        {
            get
            {
                return m_strGUIDReference;
            }
            set
            {
                m_strGUIDReference = value;
            }
        }

        //----------------------------------------------------
        public CDefinitionProprieteDynamique Champ
        {
            get
            {
                return m_champ;
            }
            set
            {
                m_champ = value;
            }
        }

        //----------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strGUIDReference);
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleValeur);
            if (result)
                result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_champ);
            return result;
        }

        internal void OnChangeVariable(IVariableDynamique variable)
        {
            CDefinitionProprieteDynamique defProp = new CDefinitionProprieteDynamiqueVariableDynamique(variable as CVariableDynamique);
            if (FormuleValeur != null)
            {
                foreach (C2iExpressionChamp expChamp in FormuleValeur.ExtractExpressionsType(typeof(C2iExpressionChamp)))
                {
                    CDefinitionProprieteDynamiqueVariableDynamique defVar = expChamp.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableDynamique;
                    if (defVar != null && defVar.IdChamp == variable.Id)
                        expChamp.DefinitionPropriete = defProp;
                }
            }
        }
    }
}
