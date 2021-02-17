using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.data.dynamic;


namespace sc2i.data.dynamic
{
    [Serializable]
    public class CFormatChampTableauCroise : I2iSerializable
    {
        //texte vide pour défaut
        public string FontName { get; set; }

        //0 pour défaut
        public int FontSize { get; set; }
        private C2iExpression m_formuleFontSize = null;

        //Transparent pour défaut
        public Color ForeColor { get; set; }
        private C2iExpression m_formuleForeColor = null;

        //Transparent pour défaut
        public Color BackColor { get; set; }
        private C2iExpression m_formuleBackColor = null;

        public Color SelectionBackcolor { get; set; }

        //Bold
        public bool? Bold { get; set; }
        private C2iExpression m_formuleBold = null;

        public C2iWndTextBox.TypeAlignement? Alignement { get; set; }

        public int? Width { get; set; }


        public CFormatChampTableauCroise()
        {
            FontName = "";
            FontSize = 0;
            ForeColor = Color.FromArgb(0, 0, 0, 0);
            BackColor = Color.FromArgb(0, 255, 255, 255);
            Bold = null;
            Alignement = null;
        }

        private int GetNumVersion()
        {
            return 2;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            string strFont = FontName;
            serializer.TraiteString(ref strFont);
            FontName = strFont;
            int nFontSize = FontSize;
            serializer.TraiteInt(ref nFontSize);
            FontSize = nFontSize;

            Int32 nCol = ForeColor.ToArgb();
            serializer.TraiteInt(ref nCol);
            ForeColor = Color.FromArgb(nCol);
            nCol = BackColor.ToArgb();
            serializer.TraiteInt(ref nCol);
            BackColor = Color.FromArgb(nCol);

            bool bHasVal = Alignement != null;
            serializer.TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                int nVal = (int)C2iWndTextBox.TypeAlignement.Centre;
                if (serializer.Mode == ModeSerialisation.Ecriture)
                    nVal = (int)Alignement.Value;
                serializer.TraiteInt(ref nVal);
                Alignement = (C2iWndTextBox.TypeAlignement)nVal;
            }

            bHasVal = Bold != null;
            serializer.TraiteBool ( ref bHasVal );
            if ( bHasVal )
            {
                bool bVal = false;
                if (serializer.Mode == ModeSerialisation.Ecriture)
                    bVal = Bold.Value;
                serializer.TraiteBool(ref bVal);
                Bold = bVal;
            }

            if (nVersion >= 1)
            {
                bHasVal = Width != null;
                serializer.TraiteBool(ref bHasVal);
                if (bHasVal)
                {
                    int nVal = 10;
                    if (serializer.Mode == ModeSerialisation.Ecriture)
                        nVal = Width.Value;
                    serializer.TraiteInt(ref nVal);
                    Width = nVal;
                }
                else
                    Width = null;
            }

            if (nVersion >= 2)
            {
                nCol = SelectionBackcolor.ToArgb();
                serializer.TraiteInt(ref nCol);
                SelectionBackcolor = Color.FromArgb(nCol);
            }
            else
                SelectionBackcolor = Color.Transparent;
            

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleBold);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleFontSize);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleForeColor);
            if (result)
                result = result = serializer.TraiteObject<C2iExpression>(ref m_formuleBackColor);

            return result;
        }

        //--------------------------------------
        public C2iExpression FormuleBold
        {
            get
            {
                return m_formuleBold;
            }
            set
            {
                m_formuleBold = value;
            }
        }

        //--------------------------------------
        public C2iExpression FormuleFontSize
        {
            get
            {
                return m_formuleFontSize;
            }
            set
            {
                m_formuleFontSize = value;
            }
        }

        //--------------------------------------
        public C2iExpression FormuleForeColor
        {
            get
            {
                return m_formuleForeColor;
            }
            set
            {
                m_formuleForeColor = value;
            }
        }

        //--------------------------------------
        public C2iExpression FormuleBackColor
        {
            get
            {
                return m_formuleBackColor;
            }
            set
            {
                m_formuleBackColor = value;
            }
        }

        //--------------------------------------
        public string GetFontName(CElementAVariablesDynamiques elt)
        {
            return FontName;
        }

        //--------------------------------------
        public int GetFontSize(CElementAVariablesDynamiques elt)
        {
            if (FormuleFontSize != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(elt);
                CResultAErreur result = FormuleFontSize.Eval(ctx);
                if (result && result.Data is int)
                    return (int)result.Data;
            }
            return FontSize;
        }

        //--------------------------------------
        public Color GetForeColor(CElementAVariablesDynamiques elt)
        {
            if (FormuleForeColor != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(elt);
                CResultAErreur result = FormuleForeColor.Eval(ctx);
                if (result && result.Data is Color)
                    return (Color)result.Data;
            }
            return ForeColor;
        }

        //--------------------------------------
        public Color GetBackColor(CElementAVariablesDynamiques elt)
        {
            if (FormuleBackColor != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(elt);
                CResultAErreur result = FormuleBackColor.Eval(ctx);
                if (result && result.Data is Color)
                    return (Color)result.Data;
            }
            return BackColor;
        }

        //--------------------------------------
        public bool? GetBold(CElementAVariablesDynamiques elt)
        {
            if (FormuleBold != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(elt);
                CResultAErreur result = FormuleBold.Eval(ctx);
                if (result && result.Data is bool)
                    return (bool)result.Data;
            }
            return Bold;
        }

        //--------------------------------------
        public bool IsDynamic
        {
            get
            {
                return FormuleBold != null ||
                    FormuleFontSize != null ||
                    FormuleForeColor != null ||
                    FormuleBackColor != null;
            }
        }

        //--------------------------------------
        /// <summary>
        /// Retourne null si le format dynamique est égal au format
        /// </summary>
        /// <param name="eltAVariables"></param>
        /// <returns></returns>
        public CFormatChampTableauCroise GetFormatDynamique( CElementAVariablesDynamiques eltAVariables )
        {
            if (!IsDynamic)
                return null;
            bool? bBold = GetBold(eltAVariables);

            Color foreColor = GetForeColor(eltAVariables);

            Color backColor = GetBackColor(eltAVariables);

            int nSize = GetFontSize(eltAVariables);

            if (bBold != Bold ||
                foreColor != ForeColor ||
                backColor != BackColor ||
                nSize != FontSize)
            {
                CFormatChampTableauCroise format = new CFormatChampTableauCroise();
                format.FontName = FontName;
                format.FontSize = nSize;
                format.Alignement = Alignement;
                format.ForeColor = foreColor;
                format.BackColor = backColor;
                format.Bold = bBold;
                return format;
            }
            return null;
        }


    }
}
