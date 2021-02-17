using sc2i.common;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression
{
    [AutoExec("Autoexec")]
    public class CVariableDynamiqueSaisieSimple : CVariableDynamique, IVariableDynamiqueAValeurParDefaut
    {
        private CTypeChampBasique m_typeChampBasique = new CTypeChampBasique(ETypeChampBasique.String);
        private C2iExpression m_formuleValeurDefaut = new C2iExpressionNull();

        //---------------------------------------------------
        public CVariableDynamiqueSaisieSimple()
        {
        }

        //---------------------------------------------------
        public CVariableDynamiqueSaisieSimple(IElementAVariablesDynamiquesBase elt)
            : base(elt)
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireVariablesDynamiques.RegisterTypeVariable(
                typeof(CVariableDynamiqueSaisieSimple),
                I.T("Simple variable|20121"));
        }

        //---------------------------------------------------
        public override string LibelleType
        {
            get { return I.T("Entry|20118"); }
        }


        //---------------------------------------------------
        public override CTypeResultatExpression TypeDonnee
        {
            get
            {
                if (m_typeChampBasique != null)
                    return new CTypeResultatExpression(m_typeChampBasique.TypeDotNet, false);
                return new CTypeResultatExpression(typeof(string), false);
            }
        }

        //---------------------------------------------------
        public CTypeChampBasique TypeChampBasique
        {
            get
            {
                return m_typeChampBasique;
            }
            set
            {
                m_typeChampBasique = value;
            }
        }
        
        //---------------------------------------------------
        public override bool IsChoixParmis()
        {
            return false;
        }

        //---------------------------------------------------
        public override bool IsChoixUtilisateur()
        {
            return true;
        }

        //---------------------------------------------------
        public override System.Collections.IList Valeurs
        {
            get { return new ArrayList(); }
        }

        //---------------------------------------------------
        public C2iExpression FormuleValeurDefaut
        {
            get
            {
                return m_formuleValeurDefaut;
            }
            set
            {
                m_formuleValeurDefaut = value;
            }
        }

        //---------------------------------------------------
        public object GetValeurParDefaut()
        {
            if (FormuleValeurDefaut == null)
                return null;
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression("");
            CResultAErreur result = FormuleValeurDefaut.Eval(ctx);
            if (!result)
                return null;
            return result.Data;
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result ) 
                result = base.Serialize(serializer);
            int nType = m_typeChampBasique.CodeInt;
            serializer.TraiteInt(ref nType);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                m_typeChampBasique = new CTypeChampBasique((ETypeChampBasique)nType);
            }
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleValeurDefaut);
            return result;
        }
    }
}