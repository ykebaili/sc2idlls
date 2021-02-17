using System;

using sc2i.common;
using sc2i.data;

namespace sc2i.process
{
    /// <summary>
    /// Présente des informations sur ce qui a déclenché un process
    /// </summary>
    [Serializable]
    public class CInfoDeclencheurProcess : I2iSerializable
    {
        // TESTDBKEYOK
        private CDbKey m_dbKeyEvenementDeclancheur = null;
        private TypeEvenement m_typeDeclencheur = TypeEvenement.Manuel;
        private string m_strInfo = "";
        private object m_valeurOrigine = null;


        public CInfoDeclencheurProcess()
        {
            //
            // TODO : ajoutez ici la logique du constructeur
            //
        }

        public CInfoDeclencheurProcess(TypeEvenement typeDeclencheur)
        {
            TypeDeclencheur = typeDeclencheur;
        }

        /// ////////////////////////////////////
        public TypeEvenement TypeDeclencheur
        {
            get
            {
                return m_typeDeclencheur;
            }
            set
            {
                m_typeDeclencheur = value;
            }
        }

        /// ////////////////////////////////////
        public string Info
        {
            get
            {
                return m_strInfo;

            }
            set
            {
                m_strInfo = value;
            }
        }

        /// ////////////////////////////////////
        /// <summary>
        /// Indique que le déclenchement a été manuel
        /// </summary>
        [DynamicField("Is manual")]
        public bool EstManuel
        {
            get
            {
                return m_typeDeclencheur == TypeEvenement.Manuel;
            }
        }

        /// ////////////////////////////////////
        /// <summary>
        /// Indique que le déclenchement fait suite à la création d'un élément
        /// </summary>
        [DynamicField("Is on create")]
        public bool EstSurCreation
        {
            get
            {
                return m_typeDeclencheur == TypeEvenement.Creation;
            }
        }

        /// ////////////////////////////////////
        /// <summary>
        /// DbKey de l'evenement (CEvenement) qui a déclenché le process.
        /// Null si aucun
        /// </summary>
        [ExternalReferencedEntityDbKey(typeof(CEvenement))]
        public CDbKey DbKeyEvenementDeclencheur
        {
            get
            {
                return m_dbKeyEvenementDeclancheur;
            }
            set
            {
                m_dbKeyEvenementDeclancheur = value;
            }
        }

        /// ////////////////////////////////////
        /// <summary>
        /// Indique que le déclenchement fait suite à une modification d'un élément
        /// (dans ce cas la valeur d'origine est renseignée)
        /// </summary>
        [DynamicField("Is on modification")]
        public bool EstSurModification
        {
            get
            {
                return m_typeDeclencheur == TypeEvenement.Modification;
            }

        }

        ////////////////////////////////////////////
        /// <summary>
        /// Indique que le déclenchement avait été programmé
        /// </summary>
        [DynamicField("Is on date")]
        public bool EstSurDate
        {
            get
            {
                return m_typeDeclencheur == TypeEvenement.Date;
            }
        }

        ////////////////////////////////////////////
        /// <summary>
        /// Objet d'origine, donc avant la modification<br/>
        /// (existe uniquement dans le cas d'un déclencheur sur modification)
        /// </summary>
        [DynamicField("Original value")]
        public object ValeurOrigine
        {
            get
            {
                return m_valeurOrigine;
            }
            set
            {
                m_valeurOrigine = value;
            }
        }

        /// /////////////////////////////////////////////
        private int GetNumVersion()
        {
            //Ajout Id evenement declencheur
            //2 : correction bug sir la valeur d'origine n'est pas un objet simple
            //return 2;
            return 3; // Passage de Id Declencheur à DbKey
        }

        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            int nVal = (int)TypeDeclencheur;
            serializer.TraiteInt(ref nVal);
            TypeDeclencheur = (TypeEvenement)nVal;

            if (nVersion <= 1)
            {
                result = serializer.TraiteObjetSimple(ref m_valeurOrigine);
                if (!result)
                    return result;
            }
            else
            {
                object val = m_valeurOrigine;
                C2iSerializer.EnumTypeSimple typeSimple = C2iSerializer.GetTypeSimpleObjet(val);
                if (typeSimple == C2iSerializer.EnumTypeSimple.Inconnu)
                {
                    if (m_valeurOrigine != null)
                    {
                        val = m_valeurOrigine.ToString();
                    }
                    else val = null;
                }
                result = serializer.TraiteObjetSimple(ref val);
            }

            serializer.TraiteString(ref m_strInfo);

            if (nVersion >= 1)
            {
                if (nVersion < 3)
                    // TESTDBKEYOK
                    serializer.ReadDbKeyFromOldId(ref m_dbKeyEvenementDeclancheur, typeof(CEvenement));
                else
                    serializer.TraiteDbKey(ref m_dbKeyEvenementDeclancheur);
            }

            return result;
        }

    }
}
