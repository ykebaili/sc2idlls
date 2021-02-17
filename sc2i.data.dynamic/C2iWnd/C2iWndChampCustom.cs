using System;
using System.IO;
using System.Drawing;

using System.Collections.Generic;

#if PDA
#else
using System.Drawing.Design;
#endif


using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using System.ComponentModel;
using System.Text;
using sc2i.formulaire.datagrid.Filters;
using sc2i.common.Referencement;
using sc2i.formulaire.web;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Description résumée de C2iChampCustomTextBox.
    /// </summary>
    [WndName("Data")]
    public class C2iWndChampCustom : C2iWndVariable, IWndIncluableDansDataGrid, I2iWebControl
    {
        private CChampCustom m_champCustom = null;

        private int m_nNumWeb = 0;
        private string m_strLibelleWeb = "";
        private bool m_bUseCombo = false;
        private bool m_bAutoSetValue = false;

        public C2iWndChampCustom()
            : base()
        {
        }

        /// //////////////////////////////////////////////////
        public override bool CanBeUseOnType(Type tp)
        {
            if (tp == null)
                return false;
            return typeof(IElementAChamps).IsAssignableFrom(tp);
        }

        /// //////////////////////////////////////////////////
        private int GetNumVersion()
        {
            //2 : Ajout de UseCombo
            //3 : Ajout de AutoSetValue
            // return 3;
            return 4; // Traitement du passage en Id universel
        }

        /// //////////////////////////////////////////////////
        public bool AutoSetValue
        {
            get
            {
                return m_bAutoSetValue;
            }
            set
            {
                m_bAutoSetValue = value;
            }
        }

        /// //////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if (!result)
                return result;

            bool bHasChamp = m_champCustom != null;
            serializer.TraiteBool(ref bHasChamp);
            int nId = 0;
            if (bHasChamp)
                switch (serializer.Mode)
                {
                    /* TESTDBKEYOK (XL) */
                    case ModeSerialisation.Ecriture:
                        CDbKey dbKeyW = m_champCustom.DbKey;
                        serializer.TraiteDbKey(ref dbKeyW);
                        break;
                    case ModeSerialisation.Lecture:
                        CDbKey dbKey = null;
                        if (nVersion < 4)
                            serializer.ReadDbKeyFromOldId(ref dbKey, typeof(CChampCustom));
                        else
                            serializer.TraiteDbKey(ref dbKey);

                        CContexteDonnee ctx = (CContexteDonnee)serializer.GetObjetAttache(typeof(CContexteDonnee));
                        if (ctx == null)
                            ctx = CContexteDonneeSysteme.GetInstance();
                        m_champCustom = new CChampCustom(ctx);
                        if (!m_champCustom.ReadIfExists(dbKey))
                            m_champCustom = null;

                        break;
                }
            else
                m_champCustom = null;

            if (nVersion > 0)
            {
                serializer.TraiteString(ref m_strLibelleWeb);
                serializer.TraiteInt(ref m_nNumWeb);
            }
            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bUseCombo);
            else
                m_bUseCombo = false;
            if (nVersion >= 3)
                serializer.TraiteBool(ref m_bAutoSetValue);
            return result;
        }

        /// //////////////////////////////////////////////////
        public override IVariableDynamique Variable
        {
            get
            {
                return ChampCustom;
            }
        }

        /// //////////////////////////////////////////////////
        [System.ComponentModel.Editor(typeof(CProprieteChampCustomEditor), typeof(UITypeEditor))]
        [System.ComponentModel.DisplayName("Custom Field")]
        [ExternalReferencedEntity(typeof(CChampCustom))]
        public CChampCustom ChampCustom
        {
            get
            {
                return m_champCustom;
            }
            set
            {
                if (m_champCustom != null && WebLabel == m_champCustom.Nom)
                    WebLabel = "";
                m_champCustom = value;
                WebLabel = m_champCustom.Nom;
                Size = Size;
            }
        }

        /// //////////////////////////////////////////////////
        public string WebLabel
        {
            get
            {
                return m_strLibelleWeb;
            }
            set
            {
                m_strLibelleWeb = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int WebNumOrder
        {
            get
            {
                return m_nNumWeb;
            }
            set
            {
                m_nNumWeb = value;
            }
        }

        /// //////////////////////////////////////////////////
        public bool UtiliserUneCombo
        {
            get
            {
                return m_bUseCombo;
            }
            set
            {
                m_bUseCombo = value;
            }
        }

        /// //////////////////////////////////////////////////
        public override bool AutoBackColor
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// //////////////////////////////////////////////////
        public override void OnDesignSelect(
            Type typeEdite,
            object objetEdite,
            sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CProprieteChampCustomEditor.SetTypeElementAChamp(GetObjetPourAnalyseThis(typeEdite).TypeAnalyse);
        }


        /// //////////////////////////////////////////////////
        public override bool DoesUse(object objetCherche)
        {
            if (base.DoesUse(objetCherche))
                return true;
            CDefinitionProprieteDynamiqueChampCustom defChamp = objetCherche as CDefinitionProprieteDynamiqueChampCustom;
            if (defChamp != null && m_champCustom != null)
            {
                if (defChamp.DbKeyChamp == m_champCustom.DbKey)
                    return true;
            }
            return false;
        }

        #region IWndIncluableDansDataGrid Membres

        public Type ValueTypeForGrid
        {
            get
            {
                if (m_champCustom != null)
                    return m_champCustom.TypeDonnee.TypeDotNetNatif;
                return typeof(string);
            }
        }

        public string ConvertObjectValueToStringForGrid(object valeur)
        {
            if (valeur is CObjetDonnee)
            {
                return DescriptionFieldAttribute.GetDescription((CObjetDonnee)valeur);
            }
            if (valeur != null)
            {
                string strVal = valeur.ToString();
                if (valeur is double || valeur is int)
                {
                    strVal = AppliqueSeparateurMilliers(strVal);
                }
                if (ChampCustom != null && ChampCustom.IsChoixParmis())
                {
                    strVal = ChampCustom.GetDisplayValue(valeur);
                }
                return strVal;
            }

            return "";
        }

        public object GetObjectValueForGrid(object element)
        {
            IElementAVariables elt = element as IElementAVariables;
            if (elt != null && Variable != null)
            {
                object valeur = elt.GetValeurChamp(Variable.IdVariable);
                return valeur;
            }
            return null;
        }

        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            if (m_champCustom == null)
                return new CGridFilterForWndDataGrid[0];
            if (m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(int) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(int?) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(double) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(double?))
                return CGridFilterNumericComparison.GetFiltresNumeriques();
            if (m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(bool) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(bool?))
                return CGridFilterChecked.GetFiltresBool();
            if (m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(DateTime) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(DateTime?) ||
                m_champCustom.TypeDonnee.TypeDotNetNatif == typeof(CDateTimeEx))
                return CGridFilterDateComparison.GetFiltresDate();
            return CGridFilterTextComparison.GetFiltresTexte();
        }

        private string AppliqueSeparateurMilliers(string strTexte)
        {
            if (SeparateurMilliers != "")
                strTexte = strTexte.Replace(SeparateurMilliers, "");
            else
                return strTexte;

            StringBuilder sbRetour = new StringBuilder();
            int nIndexSeparateurDec = strTexte.Length;
            if (strTexte.IndexOf('.') > 0)
            {
                nIndexSeparateurDec = strTexte.IndexOf('.');
                sbRetour.Append(".");
            }
            else
            {
                if (strTexte.IndexOf(',') > 0)
                {
                    nIndexSeparateurDec = strTexte.IndexOf(',');
                    sbRetour.Append(",");
                }
            }
            int nNbParGroupe = 3;
            for (int i = nIndexSeparateurDec - 1; i >= 0; i--)
            {
                sbRetour.Insert(0, strTexte[i]);
                if (nNbParGroupe == 0)
                {
                    sbRetour.Insert(1, SeparateurMilliers);
                    nNbParGroupe = 3;
                }
                nNbParGroupe--;
            }
            nNbParGroupe = 3;
            for (int i = nIndexSeparateurDec + 1; i < strTexte.Length; i++)
            {
                sbRetour.Append(strTexte[i]);
                if (nNbParGroupe == 0)
                {
                    sbRetour.Insert(sbRetour.Length - 1, SeparateurMilliers);
                    nNbParGroupe = 3;
                }
                nNbParGroupe--;
            }

            return sbRetour.ToString(); ;
        }

        #endregion
    }

}
