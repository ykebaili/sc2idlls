using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.expression
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class C2iExpressionTypesDynamics : C2iExpressionConstanteDynamique
    {
        private Type m_typeEntite = null;
        private string m_strNom = "";

        /// <summary>
        /// Fournit à l'analyseur les Types dynamics
        /// </summary>
        [AutoExec("Autoexec")]
        public class CFournisseurConstantesObjetDonneeAIdAuto : IFournisseurConstantesDynamiques
        {
            private static List<C2iExpressionTypesDynamics> m_listeExpression = null;
            private static AssemblyLoadEventHandler m_eventLoad = null;


            public static void Autoexec()
            {
                 if (m_eventLoad == null)
                {
                    m_eventLoad = new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
                    AppDomain.CurrentDomain.AssemblyLoad += m_eventLoad;
                }
                CAnalyseurSyntaxiqueExpression.RegisterFournisseurExpressionsDynamiques(new CFournisseurConstantesObjetDonneeAIdAuto());

            }

            //-----------------------------------------------------------------------------
            static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
            {
            m_listeExpression = null;
            }

            
           //-----------------------------------------------------------------------------
            private List<C2iExpressionTypesDynamics> GetListe()
            {
                string strCaracteresAutorises = "abcdefghijklkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
                lock (typeof(CFournisseurConstantesObjetDonneeAIdAuto))
                {
                    if (m_listeExpression == null)
                    {
                        m_listeExpression = new List<C2iExpressionTypesDynamics>();
                        foreach (CInfoClasseDynamique dc in DynamicClassAttribute.GetAllDynamicClass())
                        {
                            StringBuilder blName = new StringBuilder();
                            foreach (char c in dc.Nom)
                                if (strCaracteresAutorises.Contains(c))
                                    blName.Append(c);
                                else
                                    blName.Append("_");
                            if (blName.Length > 0)
                            {
                                blName.Insert(0, "Type_");
                                m_listeExpression.Add(new C2iExpressionTypesDynamics(blName.ToString(), dc.Classe));
                            }
                        }
                    }
                    return m_listeExpression;
                }
            }

            //-----------------------------------------------------------------------------
            public C2iExpressionConstanteDynamique[] GetConstantes()
            {
                return GetListe().ToArray();
            }

            //-----------------------------------------------------------------------------
            public C2iExpressionConstanteDynamique GetConstante(string strConstante)
            {
                return GetListe().FirstOrDefault ( e=>e.m_strNom.ToUpper() == strConstante.ToUpper() );
            }

        }

        //-----------------------------------------------------------------------------
        public C2iExpressionTypesDynamics()
            :base()
        {
        }

        public static void Autoexec()
        {
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionTypesDynamics().IdExpression,
                typeof(C2iExpressionTypesDynamics));
        }

        //-----------------------------------------------------------------------------
        public C2iExpressionTypesDynamics ( string strNom, Type type )
        {
            m_strNom = strNom;
            m_typeEntite = type;
        }

        //-----------------------------------------------------------------------------
        public override string ConstanteName
        {
            get { return m_strNom; }
        }

        //-----------------------------------------------------------------------------
        protected override CInfo2iExpression GetInfosSansCache()
        {
            return GetInfos();
        }

        //-----------------------------------------------------------------------------
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = new CInfo2iExpression(
                0,
                "DYNAMIC_TYPE",
                m_strNom,
                new CTypeResultatExpression(typeof(Type), false),
                "",
                I.T("Types"));
            return info;
        }

        //-----------------------------------------------------------------------------
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.True;
            result.Data = m_typeEntite;
            return result;
        }

        //-----------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------------------------
        public Type TypeReprésenté
        {
            get
            {
                return m_typeEntite;
            }
        }

        //-----------------------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNom);
            serializer.TraiteType(ref m_typeEntite);
            return result;
        }
    }
}
