using System;

using sc2i.common;

using sc2i.expression;
using System.Collections;
using System.Collections.Generic;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Propriete dynamique définie par un champ 'SmartField'
    /// la propriété associée est l'id du champ calculé dans la base de données
    /// </summary>
    [Serializable]
    [AutoExec("Autoexec")]
    public class CDefinitionProprieteDynamiqueSmartField : CDefinitionProprieteDynamique
    {
        public const string c_strCleType = "SF";
        protected int m_nIdSmartField = -1;

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueSmartField()
            : base()
        {
        }
        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueSmartField(CSmartField smartField)
            : base(smartField.Libelle.Replace(" ", "_"), smartField.Id.ToString(),
                smartField.Definition.TypeDonnee, false, true)
        {
            m_nIdSmartField = smartField.Id;
        }

        /// //////////////////////////////////////////////////////
        public static new void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueSmartField));
        }

        //-----------------------------------------------
        public override string CleType
        {
            get { return c_strCleType; }
        }

        /// //////////////////////////////////////////////////////
        public int IdSmartField
        {
            get
            {
                return m_nIdSmartField;
            }
        }

        public override void CopyTo(CDefinitionProprieteDynamique def)
        {
            base.CopyTo(def);
            ((CDefinitionProprieteDynamiqueSmartField)def).m_nIdSmartField = m_nIdSmartField;
        }

        /// ////////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }

        /// ////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nIdSmartField);
            return result;
        }


    }

    public class CInterpreteurProprieteDynamiqueSmartField : IInterpreteurProprieteDynamique
    {
        //------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return false;
        }

        //------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            CObjetDonnee objetDonnee = objet as CObjetDonnee;
            if (objetDonnee == null)
            {
                result.Data = null;
                return result;
            }
            int nIdSmartField = -1;
            try
            {
                nIdSmartField = Int32.Parse(strPropriete);
            }
            catch
            {
                result.EmpileErreur(I.T("Bad SmartField field format (@1)|20059", strPropriete));
                return result;
            }
            CContexteDonnee contexte = objetDonnee.ContexteDonnee;
            CSmartField smartField = new CSmartField(contexte);
            if (!smartField.ReadIfExists(nIdSmartField))
            {
                result.EmpileErreur(I.T("Smart field @1 doesn't exists|20060", strPropriete));
                return result;
            }
            if (smartField.Definition != null)
            {
                return CInterpreteurProprieteDynamique.GetValue(objet, smartField.Definition);
            }
            result.Data = null;
            return result;
        }


        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            CResultAErreur result = CResultAErreur.True;
            result.EmpileErreur(I.T("Forbidden affectation|20034"));
            return result;
        }

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }
    }



    [AutoExec("Autoexec")]
    public class CFournisseurProprietesDynamiquesSmartField : IFournisseurProprieteDynamiquesSimplifie
    {
        public static void Autoexec()
        {
            CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesSmartField());
        }


        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
            if (objet == null)
                return lstProps.ToArray();
            Type tp = objet.TypeAnalyse;
            if (tp == null)
                return lstProps.ToArray();

            if (!C2iFactory.IsInit())
                return lstProps.ToArray();

            CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
            CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, typeof(CSmartField));
            liste.Filtre = new CFiltreData(CSmartField.c_champTypeCible + "=@1", tp.ToString());
            foreach (CSmartField champ in liste)
            {
                CDefinitionProprieteDynamiqueSmartField def = new CDefinitionProprieteDynamiqueSmartField(champ);
                def.HasSubProperties = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(def.TypeDonnee.TypeDotNetNatif);
                def.Rubrique = champ.Categorie;
                lstProps.Add(def);
            }
            return lstProps.ToArray();
        }

    }

    /*[AutoExec("Autoexec")]
    public class CPreparateurArbreDefPropSmartField : IPreparateurTransformationArbreDefinitionsEnArbreSousPropListeObjetDonnee
    {
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterTransformateur(CDefinitionProprieteDynamiqueSmartField.c_strCleType, typeof(CPreparateurArbreDefPropSmartField));
        }
    }*/


}