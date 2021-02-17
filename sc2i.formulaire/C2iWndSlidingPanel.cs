using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using sc2i.formulaire.web;

namespace sc2i.formulaire
{
    /// <summary>
    /// Description résumée de C2iPanel.
    /// </summary>
    [WndName("Sliding Panel")]
    [AWndIcone("ico_panel")]
    [Serializable]
    public class C2iWndSlidingPanel : C2iWndComposantFenetre, I2iWebControl
    {

        public const string c_strIdEvenementChangeCollapse = "CHGCPS";

        private bool m_bBorder = false;

        private int m_nTitleHeight = 18;
        private C2iExpression m_formuleTitre = null;
        private Color m_titleGradientBackColor = Color.FromArgb(205, 210, 224);
        private Color m_titleBackColor = Color.FromArgb(215, 220, 254);
        private ContentAlignment m_titleAlignemt = ContentAlignment.MiddleLeft;
        private Font m_titleFont = null;

        private int m_nClientHeight = 100;
        private int m_nWidth = 100;

        private bool m_bAdjustToContent = false;
        private int m_nMaxAutosizeHeight = 3000;

        private bool m_bIsCollapse = false;

        private int m_nNumOrdreWeb = 0;
        private string m_strLibelleWeb = "";

        /// ///////////////////////////////////////
        public C2iWndSlidingPanel()
        {
        }

        /// ///////////////////////////////////////
        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        /// ///////////////////////
        public bool AdjustToContent
        {
            get
            {
                return m_bAdjustToContent;
            }
            set
            {
                m_bAdjustToContent = value;
            }
        }

        /// ///////////////////////
        public int MaxAutosizeHeight
        {
            get
            {
                return m_nMaxAutosizeHeight;
            }
            set
            {
                m_nMaxAutosizeHeight = value;
            }
        }


        /// ///////////////////////
        public bool Border
        {
            get
            {
                return m_bBorder;
            }
            set
            {
                m_bBorder = value;
            }
        }

        /// ///////////////////////
        [System.ComponentModel.Browsable(false)]
        public override bool AcceptChilds
        {
            get
            {
                return true;
            }
        }

        /// ///////////////////////////////////////
        public bool IsCollapsed
        {
            get
            {
                return m_bIsCollapse;
            }
            set
            {
                bool bOldValue = m_bIsCollapse;
                m_bIsCollapse = value;
                if (value != bOldValue)
                {
                    if (Parent is C2iWnd)
                        ((C2iWnd)Parent).DockChilds();
                }
            }
        }


        /// ///////////////////////
        [System.ComponentModel.Browsable(false)]
        protected override Point OrigineCliente
        {
            get
            {
                if (!Border)
                    return new Point(0, m_nTitleHeight);
                else
                    return new Point(1, 1 + m_nTitleHeight);
            }
        }

        /// ///////////////////////////////////////
        public int TitleHeight
        {
            get
            {
                return m_nTitleHeight;
            }
            set
            {
                m_nTitleHeight = value;
            }
        }

        /// ///////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        public C2iExpression TitleFormula
        {
            get
            {
                return m_formuleTitre;
            }
            set
            {
                m_formuleTitre = value;
            }
        }

        /// ///////////////////////
        public Color TitleGradientBackColor
        {
            get
            {
                return m_titleGradientBackColor;
            }
            set
            {
                m_titleGradientBackColor = value;
            }
        }

        /// ///////////////////////
        public Color TitleBackColor
        {
            get
            {
                return m_titleBackColor;
            }
            set
            {
                m_titleBackColor = value;
            }
        }

        /// ///////////////////////
        public ContentAlignment TitleAlignement
        {
            get
            {
                return m_titleAlignemt;
            }
            set
            {
                m_titleAlignemt = value;
            }
        }

        public Font TilteFont
        {
            get
            {
                if (m_titleFont == null)
                    return this.Font;
                return m_titleFont;
            }
            set
            {
                m_titleFont = value;
            }
        }

        /// ///////////////////////////////////////
        [System.ComponentModel.Browsable(false)]
        protected override Size ClientSize
        {
            get
            {
                int nOffset = Border ? -2 : 0;
                return new Size(m_nWidth + nOffset, m_nClientHeight + nOffset);
            }
        }

        /// ///////////////////////
        public override Size Size
        {
            get
            {
                int nOffset = Border ? -2 : 0;
                return new Size(m_nWidth + nOffset, (!IsCollapsed ? m_nClientHeight : 0) + m_nTitleHeight + nOffset);
            }
            set
            {
                m_nWidth = value.Width;
                if (!IsDocking)
                {
                    if (!IsCollapsed)
                    {
                        m_nClientHeight = value.Height - (Border ? 1 : 0) - m_nTitleHeight;
                        if (m_nClientHeight <= 0)
                            m_nClientHeight = 5;
                    }
                    if (Parent is C2iWnd)
                        ((C2iWnd)Parent).DockChilds();
                }
                base.Size = Size;
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

        //------------------------------------------------------------
        public override void Draw(CContextDessinObjetGraphique ctx)
        {
            if (IsCollapsed)
                MyDraw(ctx);
            else
                base.Draw(ctx);
        }

        /// ///////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Rectangle rect = new Rectangle(Position, Size);
            if (!IsCollapsed)
            {
                Brush b = new SolidBrush(BackColor);
                g.FillRectangle(b, rect);
                b.Dispose();
            }

            //Dessine la barre
            Rectangle rectTitre = new Rectangle(rect.Left, rect.Top, rect.Width, TitleHeight);
            Brush br = new LinearGradientBrush(new Point(0, 0),
                new Point(0, rectTitre.Height),
                TitleBackColor,
                TitleGradientBackColor);
            g.FillRectangle(br, rectTitre);
            br.Dispose();


            string strText = TitleFormula != null ? TitleFormula.GetString() : "";
            Rectangle rcText = new Rectangle(rectTitre.Left + 12 + 3, rectTitre.Top,
                rectTitre.Width - 12 - 3, rectTitre.Height);

            br = new SolidBrush(ForeColor);
            CTextRenderer.DrawText(
                g,
                strText,
                TilteFont,
                rcText,
                br,
                TitleAlignement);
            br.Dispose();

            if (Border)
            {
                if (IsCollapsed)
                    rect = rectTitre;
                //rect = contexte.ConvertToAbsolute(rect);
                Pen pen = new Pen(ForeColor);
                g.DrawRectangle(pen, rect);
                pen.Dispose();
            }
        }

        /// ///////////////////////////////////////
        private int GetNumVersion()
        {
            return 4;
            // 2 : Adjust to content
            // 3 : Ajout de TitleFont qui n'est pas la même que la Font du panel lui-même
            // 4 : Ajout des propriétés pour le web
        }

        /// ///////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteBool(ref m_bBorder);

            serializer.TraiteInt(ref m_nTitleHeight);
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleTitre);
            if (!result)
                return result;

            int nTmp = TitleBackColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            TitleBackColor = Color.FromArgb(nTmp);

            nTmp = TitleGradientBackColor.ToArgb();
            serializer.TraiteInt(ref nTmp);
            TitleGradientBackColor = Color.FromArgb(nTmp);

            nTmp = (int)TitleAlignement;
            serializer.TraiteInt(ref nTmp);
            TitleAlignement = (ContentAlignment)nTmp;

            serializer.TraiteBool(ref m_bIsCollapse);

            if (nVersion >= 1)
                serializer.TraiteInt(ref m_nClientHeight);

            if (nVersion >= 2)
            {
                serializer.TraiteBool(ref m_bAdjustToContent);
                serializer.TraiteInt(ref m_nMaxAutosizeHeight);
            }

            if (nVersion >= 3)
            {
                result = SerializeFont(serializer, ref m_titleFont);
                if (!result)
                    return result;
            }

            // Ajout des propriétés pour le web
            if (nVersion >= 4)
            {
                serializer.TraiteString(ref m_strLibelleWeb);
                serializer.TraiteInt(ref m_nNumOrdreWeb);
            }

            return result;
        }

        public override void OnDesignDoubleClick(Point ptAbsolu)
        {
            Point pt = GlobalToClient(ptAbsolu);
            if (pt.Y < 0)
                IsCollapsed = !IsCollapsed;
        }

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "IsCollapse", "IsCollapse",
                new CTypeResultatExpression(typeof(bool), false),
                false,
                false,
                ""));
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "TitleText", "TitleText",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "TitleBackColor", "TitleBackColor",
                new CTypeResultatExpression(typeof(Color), false),
                false,
                false,
                ""));
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "TitleBackColorGradient", "TitleBackColorGradient",
                new CTypeResultatExpression(typeof(Color), false),
                false,
                false,
                ""));
            return lst.ToArray();
        }

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementChangeCollapse, "ChangeCollapse", I.T("Occurs when the control changed of appearance|30004")));
            return lst.ToArray();
        }

    }
}
