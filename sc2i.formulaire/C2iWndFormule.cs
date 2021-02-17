using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
#if PDA
#else
using System.Drawing.Design;
#endif


using sc2i.drawing;
using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.datagrid.Filters;
using System.ComponentModel;
using sc2i.formulaire.web;

namespace sc2i.formulaire
{
    /// <summary>
    /// Description résumée de C2iChampCustomTextBox.
    /// </summary>
    [WndName("Formula")]
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.C2iWndExpression")]
    public class C2iWndFormule : C2iWndLabel, IWndIncluableDansDataGrid, I2iWebControl
    {

        public const string c_strIdEvenementTextChanged = "TXTCHG";
        //private IVariableDynamique m_variable = null;
        private string m_strFormatAffichage = "";
        private C2iExpression m_expression = null;
        private int m_nNumOrdreWeb = 0;
        private string m_strLibelleWeb = "";


        public C2iWndFormule()
        {
            //Size = new Size ( Size.Width, 22 );
        }

        /// //////////////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Ajout de WebLabel et WebNumOrder
        }

        /// //////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            if (result)
                result = base.MySerialize(serializer);

            serializer.TraiteString(ref m_strFormatAffichage);

            I2iSerializable objet = m_expression;
            result = serializer.TraiteObject(ref objet);
            if (!result)
                return result;
            m_expression = (C2iExpression)objet;

            // Ajout des propriétés pour le web
            if (nVersion >= 1)
            {
                serializer.TraiteString(ref m_strLibelleWeb);
                serializer.TraiteInt(ref m_nNumOrdreWeb);
            }

            return result;
        }

        /// //////////////////////////////////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        [System.ComponentModel.DisplayName("Formula")]
        public C2iExpression Formule
        {
            get
            {
                return m_expression;
            }
            set
            {
                m_expression = value;
            }
        }

        /// //////////////////////////////////////////////////
        [System.ComponentModel.Description(@"Format")]
        public string DisplayFormat
        {
            get
            {
                return m_strFormatAffichage;
            }
            set
            {
                m_strFormatAffichage = value;
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
                return m_nNumOrdreWeb;
            }
            set
            {
                m_nNumOrdreWeb = value;
            }
        }


        /// //////////////////////////////////////////////////
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            if (m_expression != null)
                Text = m_expression.GetString();
            else
                Text = I.T("No formula|131");
            base.DrawInterieur(ctx);
        }


        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "FormatAffichage", "FormatAffichage",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                 "Expression", "Expression",
                 new CTypeResultatExpression(typeof(C2iExpression), false),
                 false,
                 false,
                 ""));

            return lst.ToArray();
        }


        #region IElementAEvenementParFormule Membres

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();
            lst.AddRange(base.GetDescriptionsEvenements());

            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementTextChanged,
                "Text changed", I.T("Occurs when the text in the box has changed|20000")));
            return lst.ToArray();
        }






        #endregion



        #region IWndIncluableDansDataGrid Membres

        public Type ValueTypeForGrid
        {
            get { return typeof(bool); }
        }

        public string ConvertObjectValueToStringForGrid(object val)
        {
            if (val != null)
                return val.ToString();
            return "";
        }

        public object GetObjectValueForGrid(object element)
        {
            if (m_expression != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(element);
                CResultAErreur result = m_expression.Eval(ctx);
                if (result && result.Data != null)
                    return result.Data;
            }
            return "";
        }


        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            Type tp = typeof(string);
            if (m_expression != null)
                tp = m_expression.TypeDonnee.TypeDotNetNatif;
            if (tp == typeof(int) || tp == typeof(double))
                return CGridFilterNumericComparison.GetFiltresNumeriques();
            else if (tp == typeof(bool))
                return CGridFilterChecked.GetFiltresBool();
            else if (tp == typeof(DateTime))
                return CGridFilterDateComparison.GetFiltresDate();
            return CGridFilterTextComparison.GetFiltresTexte();
        }

        #endregion

        public override void FillArbreProprietesAccedees(CArbreDefinitionsDynamiques arbre)
        {
            base.FillArbreProprietesAccedees(arbre);
            if (m_expression != null)
            {
                m_expression.GetArbreProprietesAccedees(arbre);
            }
        }
    }
}
