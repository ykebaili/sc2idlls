using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.formulaire.inspiration
{
    [Serializable]
    public class CParametreInspirationProprieteDeType : I2iSerializable, IParametreInspiration
    {
        private Type m_type;
        private CDefinitionProprieteDynamique m_definition;

        //-----------------------------------------------------------
        public CParametreInspirationProprieteDeType()
        {
        }

        //-----------------------------------------------------------
        public CParametreInspirationProprieteDeType(Type type, CDefinitionProprieteDynamique definition)
        {
            m_type = type;
            m_definition = definition;
        }

        //-----------------------------------------------------------
        public Type Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        //-----------------------------------------------------------
        public CDefinitionProprieteDynamique Champ
        {
            get
            {
                return m_definition;
            }
            set
            {
                m_definition = value;
            }
        }

        //-----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteType(ref m_type);
            if (result)
                result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_definition);
            return result;
        }

        //-----------------------------------------------------------
        public override int GetHashCode()
        {
            if (m_type != null && m_definition != null)
            {
                return (m_type.ToString() + "/" + m_definition.NomPropriete).GetHashCode();
            }
            return 0;
        }

        //-----------------------------------------------------------
        public override bool Equals(object obj)
        {
            CParametreInspirationProprieteDeType parametre = obj as CParametreInspirationProprieteDeType;
            if (parametre != null)
            {
                return parametre.Type == Type && parametre.Champ == Champ;
            }
            return false;
        }



        //-----------------------------------------------------------
        public CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_type == null)
            {
                result.EmpileErreur(I.T("Inspiration parameter must specify a type|20029"));
            }
            if (m_definition == null)
                result.EmpileErreur(I.T("Inspiration parametre must specify a field|20030"));
            return result;
        }
    }
}
