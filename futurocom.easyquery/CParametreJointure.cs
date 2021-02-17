using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;
using sc2i.expression;

namespace futurocom.easyquery
{
    public enum EOperateurJointure
    {
        Egal = 0,
        Superieur,
        SuperieurEgal,
        Inferieur,
        InferieurEgal,
        Different
    }

    [Serializable]
    public class CParametreJointure : I2iSerializable
    {
        private C2iExpression m_formuleTable1 = null;
        private C2iExpression m_formuleTable2 = null;
        private EOperateurJointure m_operateur = EOperateurJointure.Egal;

        //-----------------------------------------
        public CParametreJointure()
        {
        }

        //-----------------------------------------
        public CParametreJointure(C2iExpression formuleTable1, C2iExpression formuleTable2)
        {
            m_formuleTable1 = formuleTable1;
            m_formuleTable2 = formuleTable2;
        }

        //-----------------------------------------
        public C2iExpression FormuleTable1
        {
            get
            {
                return m_formuleTable1;
            }
            set
            {
                m_formuleTable1 = value;
            }
        }

        //-----------------------------------------
        public C2iExpression FormuleTable2
        {
            get
            {
                return m_formuleTable2;
            }
            set
            {
                m_formuleTable2 = value;
            }
        }

        //-----------------------------------------
        public EOperateurJointure Operateur
        {
            get
            {
                return m_operateur;
            }
            set
            {
                m_operateur = value;
            }
        }

        //-----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleTable1);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleTable2);
            if (!result)
                return result;
            int nOperateur = (int)m_operateur;
            serializer.TraiteInt(ref nOperateur);
            m_operateur = (EOperateurJointure)nOperateur;
            return result;
        }

        //---------------------------------------------------
        public static CResultAErreur GetDicValeurs(
            IEnumerable<DataRow> rows,
            C2iExpression formule,
            ref Dictionary<object, List<DataRow>> dicRetour)
        {
            CResultAErreur result = CResultAErreur.True;
            dicRetour = new Dictionary<object, List<DataRow>>(rows.Count());
            string strColonne = null;
            C2iExpressionChamp expChamp = formule as C2iExpressionChamp;
            if (expChamp != null)
            {
                CDefinitionProprieteDynamiqueDataColumn champCol = expChamp.DefinitionPropriete as CDefinitionProprieteDynamiqueDataColumn;
                if (champCol != null)
                {
                    strColonne = champCol.NomProprieteSansCleTypeChamp;
                }
            }
            foreach (DataRow row in rows)
            {
                object valeur = null;
                if (strColonne != null)
                {
                    valeur = row[strColonne];
                }
                else
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(row);
                    result = formule.Eval(ctx);
                    if (!result)
                        return result;
                    valeur = result.Data == null ? DBNull.Value : result.Data;
                }

                List<DataRow> lstRows = null;
                if (!dicRetour.TryGetValue(valeur, out lstRows))
                {
                    lstRows = new List<DataRow>();
                    dicRetour[valeur] = lstRows;
                }
                lstRows.Add(row);
            }
            return result;
        }


        //---------------------------------------------------
        public static bool Compare(object o1, object o2, EOperateurJointure operateur)
        {
            IComparable c1 = o1 as IComparable;
            IComparable c2 = o2 as IComparable;
            if (c1 == null || c2 == null)
                return false;
            switch (operateur)
            {
                case EOperateurJointure.Egal:
                    return c1.CompareTo(c2) == 0;
                case EOperateurJointure.Superieur:
                    return c1.CompareTo(c2) > 0;
                case EOperateurJointure.SuperieurEgal:
                    return c1.CompareTo(c2) >= 0;
                case EOperateurJointure.Inferieur:
                    return c1.CompareTo(c2) < 0;
                case EOperateurJointure.InferieurEgal:
                    return c1.CompareTo(c2) <= 0;
                case EOperateurJointure.Different:
                    return c1.CompareTo(c2) != 0;
            }
            return false;
        }
    }
}
