using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
    [AutoExec("Autoexec")]
    public class CActionModifierPropriete : CActionLienSortantSimple
    {
        /* TESTDBKEYOK (XL)*/

        private string m_strIdVariableAModifier = "";
        private CDefinitionProprieteDynamique m_definitionPropriete = null;
        private C2iExpression m_expressionValeur = null;

        /// /////////////////////////////////////////
        public CActionModifierPropriete(CProcess process)
            : base(process)
        {
            Libelle = I.T("Modify property|211");
        }

        /// /////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Modify property|211"),
                I.T("Allows to modify an element property|212"),
                typeof(CActionModifierPropriete),
                CGestionnaireActionsDisponibles.c_categorieDonnees);
        }

        /// ////////////////////////////////////////////////////////
        public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
        {
            table[m_strIdVariableAModifier] = true;
            AddIdVariablesExpressionToHashtable(m_expressionValeur, table);
        }

        /// /////////////////////////////////////////////////////////
        public override bool PeutEtreExecuteSurLePosteClient
        {
            get { return true; }
        }

        /// /////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage de int IdVariableAModifier en String
        }


        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if (!result)
                return result;

            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableAModifier = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableAModifier);

            I2iSerializable objet = m_expressionValeur;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expressionValeur = (C2iExpression)objet;

            objet = m_definitionPropriete;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_definitionPropriete = (CDefinitionProprieteDynamique)objet;

            return result;
        }

        /// ////////////////////////////////////////////////////////
        public string IdVariableAModifier
        {
            get
            {
                return m_strIdVariableAModifier;
            }
            set
            {
                m_strIdVariableAModifier = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CVariableDynamique VariableAModifier
        {
            get
            {
                return Process.GetVariable(IdVariableAModifier);
            }
            set
            {
                if (value == null)
                    m_strIdVariableAModifier = "";
                else
                    m_strIdVariableAModifier = value.IdVariable;
            }
        }

        /// ////////////////////////////////////////////////////////
        public CDefinitionProprieteDynamique Propriete
        {
            get
            {
                return m_definitionPropriete;
            }
            set
            {
                m_definitionPropriete = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public C2iExpression ExpressionValeur
        {
            get
            {
                if (m_expressionValeur == null)
                    return new C2iExpressionConstante("");
                return m_expressionValeur;
            }
            set
            {
                m_expressionValeur = value;
            }
        }


        /// ////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees()
        {
            CResultAErreur result = CResultAErreur.True;

            if (VariableAModifier == null)
            {
                result.EmpileErreur(I.T("The variabl to be modified isn't defined or doesn't exist|215"));
            }

            if (ExpressionValeur is C2iExpressionNull)
            {
                Type type = Propriete.TypeDonnee.TypeDotNetNatif;
                if (!(type.IsValueType || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) &&
                    type.IsValueType)
                    result.EmpileErreur(I.T("Impossible to assign a null value to this property|214"));

            }
            else
            {
                if (!Propriete.TypeDonnee.TypeDotNetNatif.IsAssignableFrom(ExpressionValeur.TypeDonnee.TypeDotNetNatif)
                    || Propriete.TypeDonnee.IsArrayOfTypeNatif != ExpressionValeur.TypeDonnee.IsArrayOfTypeNatif)
                {
                    result.EmpileErreur(I.T("The value formula isn't in the awaited type of property|213"));
                }
            }

            return result;
        }

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;
            object elementToModif = Process.GetValeurChamp(VariableAModifier.IdVariable);
            if (elementToModif is CObjetDonnee)
                elementToModif = ((CObjetDonnee)elementToModif).GetObjetInContexte(contexte.ContexteDonnee);
            if (elementToModif == null)
                return result;

            object nouvelleValeur = null;
            if (!(ExpressionValeur is C2iExpressionNull))
            {

                //Calcule la nouvelle valeur
                CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
                contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
                result = ExpressionValeur.Eval(contexteEval);
                if (!result)
                {
                    result.EmpileErreur(I.T("Error during @1 formula evaluation|216", ExpressionValeur.ToString()));
                    return result;
                }
                nouvelleValeur = result.Data;
            }

            if (m_definitionPropriete.NomPropriete.Length > 1 &&
                m_definitionPropriete.NomPropriete[0] == CDefinitionProprieteDynamique.c_strCaractereStartCleType)
            {
                result = CInterpreteurProprieteDynamique.SetValue(elementToModif, m_definitionPropriete, nouvelleValeur);
            }
            else
            {
                //Ancienne méthode


                //Si modif de champ custom
                if (m_definitionPropriete is CDefinitionProprieteDynamiqueChampCustom)
                {
                    if (!(elementToModif is IElementAChamps))
                    {
                        result.EmpileErreur(I.T("@1 : Incorrect custom field or invalid target|217", m_definitionPropriete.Nom));
                        return result;
                    }
                    result = ((IElementAChamps)elementToModif).SetValeurChamp(((CDefinitionProprieteDynamiqueChampCustom)m_definitionPropriete).DbKeyChamp.StringValue, nouvelleValeur);
                    return result;
                }

                //récupère les éléments à modifier
                string[] strProps = m_definitionPropriete.NomPropriete.Split('.');
                ArrayList lstElementsAModifier = new ArrayList();
                lstElementsAModifier.Add(elementToModif);
                for (int nProp = 0; nProp < strProps.Length - 1; nProp++)
                {
                    string strProp = strProps[nProp];
                    string strTmp = "";
                    CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strProp, ref strTmp, ref strProp);
                    ArrayList newListe = new ArrayList();
                    foreach (object objet in lstElementsAModifier)
                    {
                        object newToModif = CInterpreteurTextePropriete.GetValue(objet, strProp);
                        if (newToModif != null)
                            newListe.Add(newToModif);
                    }
                    lstElementsAModifier = newListe;
                }
                string strLastProp = strProps[strProps.Length - 1];
                MethodInfo fonctionFinale = null;


                //Modifie la valeur
                foreach (object obj in lstElementsAModifier)
                {
                    try
                    {
                        if (fonctionFinale == null)
                        {
                            object dummy = null;
                            MemberInfo membre = null;
                            string strTmp = "";
                            CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strLastProp, ref strTmp, ref strLastProp);
                            CInterpreteurTextePropriete.GetObjetFinalEtMemberInfo(obj, strLastProp, ref dummy, ref membre);
                            if (membre == null || !(membre is PropertyInfo))
                            {
                                result.EmpileErreur(I.T("The @1 property cannot be find|218", strLastProp));
                                return result;
                            }
                            fonctionFinale = ((PropertyInfo)membre).GetSetMethod(true);
                            if (fonctionFinale == null)
                            {
                                result.EmpileErreur(I.T("The @1 property is in reading only|219", strLastProp));
                                return result;
                            }
                        }
                        fonctionFinale.Invoke(obj, new object[] { nouvelleValeur });
                    }
                    catch (Exception e)
                    {
                        result.EmpileErreur(new CErreurException(e));
                        return result;
                    }
                }
            }
            return result;
        }

        /// ///////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);

            DrawVariableEntree(ctx.Graphic, VariableAModifier);
        }
    }
}
