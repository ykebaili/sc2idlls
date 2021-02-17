using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic.Macro
{
    public class CMacroObjet : I2iSerializable
    {
        private string m_strDesignationObjet = "";
        private CMacro m_macro = null;
        private Type m_typeObjet = null;
        private int? m_nIdObjet = null;
        private CTypeOperationSurObjet m_typeOperation = null;
        private C2iExpression m_formuleSelectionObjet = null;
        private C2iExpression m_formuleCondition = null;
        private List<CMacroObjetValeur> m_listeValeursObjet = new List<CMacroObjetValeur>();
        private int m_nIdVariableAssociee = -1;


        //----------------------------------
        public CMacroObjet()
        {
        }

        //----------------------------------
        public CMacroObjet(CMacro macro)
            :this()
        {
            m_macro = macro;
        }

        //----------------------------------
        public CMacro Macro
        {
            get
            {
                return m_macro;
            }
            set
            {
                m_macro = value;
            }
        }

        //----------------------------------
        public int IdVariableAssociee
        {
            get
            {
                return m_nIdVariableAssociee;
            }
        }

       
        //----------------------------------
        public CObjetDonneeAIdNumerique GetObjet(CContexteDonnee ctx)
        {
            if (IdObjet != null)
            {
                try
                {
                    CObjetDonneeAIdNumerique objet = Activator.CreateInstance(m_typeObjet, new object[] { ctx }) as CObjetDonneeAIdNumerique;
                    if (objet.ReadIfExists(IdObjet.Value))
                        return objet;
                }
                catch { }
            }
            return null;
        }

        

        //----------------------------------
        public string DesignationObjet
        {
            get
            {
                return m_strDesignationObjet;
            }
            set
            {
                bool bHasChange = m_strDesignationObjet != value;
                m_strDesignationObjet = value;
                CVariableDynamique variable = Macro.GetVariable(m_nIdVariableAssociee);
                if (variable != null && bHasChange)
                {
                    variable.Nom = value;
                    Macro.OnChangeVariable(variable);
                }
            }

        }

        //----------------------------------
        public Type TypeObjet
        {
            get
            {
                return m_typeObjet;
            }
            set
            {
                m_typeObjet = value;
            }
        }

        //----------------------------------------------------------------------------------------
        public int? IdObjet
        {
            get
            {
                return m_nIdObjet;
            }
            set
            {
                m_nIdObjet = value;
            }
        }

        //----------------------------------
        public CTypeOperationSurObjet TypeOperation
        {
            get
            {
                return m_typeOperation;
            }
            set
            {
                m_typeOperation = value;
            }
        }

        //----------------------------------
        public C2iExpression FormuleSelectionObjet
        {
            get
            {
                return m_formuleSelectionObjet;
            }
            set
            {
                m_formuleSelectionObjet = value;
            }
        }

        //----------------------------------
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

        //----------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------
        public IEnumerable<CMacroObjetValeur> Valeurs
        {
            get
            {
                return m_listeValeursObjet.AsReadOnly();
            }
        }

        //----------------------------------
        public void AddValeur(CMacroObjetValeur valeur)
        {
            m_listeValeursObjet.Add(valeur);
        }

        //----------------------------------
        public void RemoveValeur(CDefinitionProprieteDynamique def)
        {
            CMacroObjetValeur m = m_listeValeursObjet.FirstOrDefault(val => val.Champ == def);
            if (m != null)
                m_listeValeursObjet.Remove(m);
        }


        //----------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strDesignationObjet);
            serializer.TraiteType(ref m_typeObjet);
            int nIdObjet = m_nIdObjet == null ? -1 : m_nIdObjet.Value;
            serializer.TraiteInt(ref nIdObjet);
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_nIdObjet = nIdObjet == -1 ? (int?)null : nIdObjet;

            int nVal = m_typeOperation != null?m_typeOperation.CodeInt:0;
            serializer.TraiteInt(ref nVal);
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_typeOperation = new CTypeOperationSurObjet((CTypeOperationSurObjet.TypeOperation)nVal);
            serializer.TraiteInt(ref m_nIdVariableAssociee);
            
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleCondition);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleSelectionObjet);
            if (result)
                result = serializer.TraiteListe<CMacroObjetValeur>(m_listeValeursObjet, new object[] { this });
            return result;
        }

        //----------------------------------
        private C2iExpression GetFormuleValeur(object valeur)
        {
            C2iExpression formule = null;
            if (valeur is CObjetDonneeAIdNumerique)
            {
                formule = new C2iExpressionGetEntite();
                formule.Parametres.Add(new C2iExpressionConstante(valeur.GetType().ToString()));
                formule.Parametres.Add(new C2iExpressionConstante(((CObjetDonneeAIdNumerique)valeur).Id));
            }
            else if (valeur == null)
                formule = new C2iExpressionNull();
            else
            {
                if (valeur is CDateTimeEx)
                    valeur = ((CDateTimeEx)valeur).DateTimeValue;
                if (C2iExpressionConstante.CanManage(valeur))
                    formule = new C2iExpressionConstante(valeur);
            }
            return formule;
        }

        internal void CreateVariable ( CObjetDonnee objet )
        {
    
            CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(Macro);
            if (objet != null)
                variable.Nom = objet.DescriptionElement;
            else
                variable.Nom = "V" + variable.Id;
            CFiltreDynamique filtre = new CFiltreDynamique(Macro, Macro.ContexteDonnee);
            filtre.TypeElements = TypeObjet;
            variable.FiltreSelection = filtre;
            variable.ExpressionRetournee = new C2iExpressionThis();
            string strField = DescriptionFieldAttribute.GetDescriptionField(TypeObjet, "DescriptionElement");
            if (strField != "")
            {
                CDefinitionProprieteDynamiqueDotNet def = new CDefinitionProprieteDynamiqueDotNet(
                    strField,
                    strField,
                    new CTypeResultatExpression(typeof(string), false),
                    false,
                    true,
                    "");
                variable.ExpressionAffichee = new C2iExpressionChamp(def);
            }
            Macro.AddVariable(variable);
            m_nIdVariableAssociee = variable.Id;
            CDefinitionProprieteDynamiqueVariableDynamique defSource = new CDefinitionProprieteDynamiqueVariableDynamique(variable);
            FormuleSelectionObjet = new C2iExpressionChamp(defSource);
        }

        //----------------------------------
        private bool FillMacroValeurWithValue(CMacroObjetValeur mv, object data, Dictionary<CObjetDonneeAIdNumerique, CMacroObjet> dicObjetToMacroObjet)
        {
            C2iExpression formule = null;
            int nIdVariableRef = -1;
            CObjetDonneeAIdNumerique obj = data as CObjetDonneeAIdNumerique;
            if (obj != null)
            {
                CMacroObjet mr = null;
                if (dicObjetToMacroObjet.TryGetValue(obj, out mr))
                    nIdVariableRef = mr.IdVariableAssociee;
                else
                {
                    CMacroObjet macroObjet = new CMacroObjet(Macro);
                    macroObjet.TypeObjet = obj.GetType();
                    macroObjet.TypeOperation = new CTypeOperationSurObjet(CTypeOperationSurObjet.TypeOperation.Aucune);
                    macroObjet.DesignationObjet = obj.DescriptionElement;
                    macroObjet.IdObjet = obj.Id;
                    Macro.AddObjet(macroObjet);
                    dicObjetToMacroObjet[obj] = macroObjet;
                    macroObjet.CreateVariable(obj);
                    nIdVariableRef = macroObjet.IdVariableAssociee;
                }
            }
            if (nIdVariableRef < 0)
            {
                formule = GetFormuleValeur(data);
            }
            if (formule == null && nIdVariableRef<0)
                return false;
            if (nIdVariableRef >= 0)
            {
                CVariableDynamique variable = Macro.GetVariable(nIdVariableRef);
                formule = new C2iExpressionChamp(new CDefinitionProprieteDynamiqueVariableDynamique(variable));
            }
            mv.FormuleValeur = formule;
            return true;
        }


        //----------------------------------
        public CResultAErreur FillFromVersion(CVersionDonneesObjet vo, CContexteDonnee contexteEnVersion, Dictionary<CObjetDonneeAIdNumerique, CMacroObjet> dicObjetToMacro)
        {
            List<CDefinitionProprieteDynamique> lstChampsDotNet = new List<CDefinitionProprieteDynamique>();
            lstChampsDotNet.AddRange(new CFournisseurProprietesDynamiqueDynamicField().GetDefinitionsChamps(vo.TypeElement, null));
            List<CDefinitionProprieteDynamique> lstChampsCustom = new List<CDefinitionProprieteDynamique>();
            lstChampsCustom.AddRange(new CFournisseurProprietesDynamiqueChampCustom().GetDefinitionsChamps(vo.TypeElement, null));
            CResultAErreur result = CResultAErreur.True;
            CObjetDonneeAIdNumerique objet = Activator.CreateInstance(vo.TypeElement, new object[] { contexteEnVersion }) as CObjetDonneeAIdNumerique;
            if (!objet.ReadIfExists(vo.IdElement))
                objet = null;
            else
                DesignationObjet = objet.DescriptionElement;
            switch (vo.TypeOperation.Code)
            {
                case CTypeOperationSurObjet.TypeOperation.Ajout:
                    if (objet != null && objet.IsValide())
                    {
                        foreach (CDefinitionProprieteDynamique def in lstChampsDotNet)
                        {
                            string strNom = def.NomProprieteSansCleTypeChamp;
                            if (!def.IsReadOnly && strNom != "IsDeleted" && strNom != "Id" &&
                                strNom != "ContexteDeModification" && strNom != "IdVersionDatabase")
                            {
                                CResultAErreur resultVal = CInterpreteurProprieteDynamique.GetValue(objet, def);
                                if (resultVal)
                                {
                                    object data = resultVal.Data;
                                    CMacroObjetValeur mv = new CMacroObjetValeur(this);
                                    mv.Champ = def;
                                    if (FillMacroValeurWithValue(mv, data, dicObjetToMacro))
                                        AddValeur(mv);
                                    else
                                        result.EmpileErreur(new CErreurValidation(I.T("Can not use field @1 in macro|20052", def.Nom), true));
                                }
                            }
                        }
                        if (objet is CElementAChamp)
                        {
                            foreach (CRelationElementAChamp_ChampCustom rel in ((CElementAChamp)objet).RelationsChampsCustom)
                            {
                                object data = rel.Valeur;
                                if (data != null)
                                {
                                    CDefinitionProprieteDynamique defCust = lstChampsCustom.FirstOrDefault(c => ((CDefinitionProprieteDynamiqueChampCustom)c).IdChamp == rel.ChampCustom.Id);
                                    if (defCust != null)
                                    {
                                        CMacroObjetValeur mv = new CMacroObjetValeur(this);
                                        mv.Champ = defCust;
                                        mv.Champ = defCust;
                                        this.AddValeur(mv);
                                        if (FillMacroValeurWithValue(mv, data, dicObjetToMacro))
                                            this.AddValeur(mv);
                                        else
                                            result.EmpileErreur(new CErreurValidation(I.T("Can not use field @1 in macro|20052", rel.ChampCustom.Nom), true));
                                    }

                                }
                            }

                        }
                    }
                    
                    break;
                case CTypeOperationSurObjet.TypeOperation.Suppression:
                    break;
                case CTypeOperationSurObjet.TypeOperation.Modification:
                    CStructureTable structureObjet = CStructureTable.GetStructure(vo.TypeElement);
                    foreach (CVersionDonneesObjetOperation valeur in vo.ToutesLesOperations)
                    {
                        CDefinitionProprieteDynamique def = null;
                        IChampPourVersion champ = valeur.Champ;
                        if (champ is CChampPourVersionInDb)
                        {
                            string strNomChamp = ((CChampPourVersionInDb)champ).NomPropriete;
                            CInfoChampTable infoChamp = structureObjet.Champs.FirstOrDefault(c => c.NomChamp == strNomChamp);
                            if (infoChamp != null && infoChamp.NomChamp != CSc2iDataConst.c_champIdVersion &&
                                infoChamp.NomChamp != CSc2iDataConst.c_champIsDeleted && 
                                infoChamp.NomChamp  != CObjetDonnee.c_champContexteModification)
                            {
                                string strNomPropriete = infoChamp.Propriete;
                                def = lstChampsDotNet.FirstOrDefault(c => c.NomProprieteSansCleTypeChamp == strNomPropriete);
                            }
                        }
                        else if ( champ is CChampCustomPourVersion )
                        {
                            CChampCustom champCustom = ((CChampCustomPourVersion)champ).ChampCustom;
                            if ( champCustom != null )
                                def = lstChampsCustom.FirstOrDefault(c=>c is CDefinitionProprieteDynamiqueChampCustom &&
                                    ((CDefinitionProprieteDynamiqueChampCustom)c).IdChamp == champCustom.Id);
                        }
                        if ( def != null && !def.IsReadOnly)
                        {
                            CMacroObjetValeur mv = new CMacroObjetValeur(this);
                            mv.Champ = def;
                            if (!FillMacroValeurWithValue(mv, valeur.GetValeur(), dicObjetToMacro))
                                result.EmpileErreur(new CErreurValidation(I.T("Can not use field @1 in macro|20052", valeur.NomChampConvivial), true));
                            else
                                AddValeur(mv);
                        }
                    }
                    break;
            }
            if (Valeurs.Count() == 0)
                TypeOperation = new CTypeOperationSurObjet(CTypeOperationSurObjet.TypeOperation.Aucune);
            return result;
        }

        public bool IsVariableUtilisee(IVariableDynamique variable)
        {
            return false;
        }


        internal void OnChangeVariable(IVariableDynamique variable)
        {
            CDefinitionProprieteDynamique defProp = new CDefinitionProprieteDynamiqueVariableDynamique(variable as CVariableDynamique);
            if (FormuleSelectionObjet != null)
            {
                foreach (C2iExpressionChamp expChamp in FormuleSelectionObjet.ExtractExpressionsType(typeof(C2iExpressionChamp)))
                {
                    CDefinitionProprieteDynamiqueVariableDynamique defVar = expChamp.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableDynamique;
                    if (defVar != null && defVar.IdChamp == variable.Id)
                        expChamp.DefinitionPropriete = defProp;
                }
            }
            if (FormuleCondition != null)
            {
                foreach (C2iExpressionChamp expChamp in FormuleCondition.ExtractExpressionsType(typeof(C2iExpressionChamp)))
                {
                    CDefinitionProprieteDynamiqueVariableDynamique defVar = expChamp.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableDynamique;
                    if (defVar != null && defVar.IdChamp == variable.Id)
                        expChamp.DefinitionPropriete = defProp;
                }
            }
            foreach (CMacroObjetValeur mv in Valeurs)
                mv.OnChangeVariable(variable);

                
        }
    }
}