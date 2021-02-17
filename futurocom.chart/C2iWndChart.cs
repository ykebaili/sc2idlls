using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.formulaire;
using sc2i.common;
using System.Drawing.Design;
using System.ComponentModel;
using sc2i.drawing;
using System.Drawing;
using sc2i.expression;

namespace futurocom.chart
{
    [WndName("Chart")]
    [Serializable]
    public class C2iWndChart : C2iWndComposantFenetre
    {
        public static string c_strIdBeforeCalculate = "BeforeCalculate";
        CChartSetup m_parametreChart = new CChartSetup();

        //------------------------------------------------------
        public C2iWndChart()
            : base()
        {
            Size = new Size(150, 100);
        }

        //------------------------------------------------------
        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        //------------------------------------------------------
        [TypeConverter(typeof(CChartSetupConvertor))]
        [Editor(typeof(CProprieteChartSetupEditor), typeof(UITypeEditor))]
        public CChartSetup ChartSetup
        {
            get
            {
                m_parametreChart.IContexteDonnee = IContexteDonnee;
                return m_parametreChart;
            }
            set
            {
                m_parametreChart = value;
            }
        }


        //------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }


        //------------------------------------------------------
        protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CChartSetup>(ref m_parametreChart);
            if (!result)
                return result;
            return result;
        }

        //------------------------------------------------------
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
            base.DrawInterieur(ctx);
            Image image = Resources.chartSample;
            Size sz = CUtilImage.GetSizeAvecRatio(image, this.Size);
            ctx.Graphic.DrawImage(image,
                new Rectangle(new Point(Size.Width / 2 - sz.Width / 2,
                    Size.Height / 2 - sz.Height / 2),
                    sz));

        }

        //------------------------------------------------------
        public override void OnDesignSelect(Type typeEdite, object objetEdite, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            m_parametreChart.TypeSourceGlobale = GetObjetPourAnalyseThis(typeEdite).TypeAnalyse;
        }

        //------------------------------------------------------
        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            lst.AddRange(base.GetProprietesInstance());

            lst.Add(new CDefinitionMethodeDynamique(
                "UpdateGraph",
                "UpdateGraph",
                new CTypeResultatExpression(typeof(bool), false),
                false));

            lst.Add(new CDefinitionMethodeDynamique(
                "SetVariableValue",
                "SetVariableValue",
                new CTypeResultatExpression(typeof(bool), false),
                false,
                "Define a variable value",
                new string[]{"Variable name",
                "Variable value"}));

            lst.Add(new CDefinitionMethodeDynamique(
                "GetVariableValue",
                "GetVariableValue",
                new CTypeResultatExpression(typeof(object), false),
                false,
                "Get a graph variable value",
                new string[]{"Variable name"}));

            return lst.ToArray();
        }

        //------------------------------------------------------
        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdBeforeCalculate, "BeforeCalculate",
                I.T("Occurs before graph calculation|20004")));
            return lst.ToArray();
        }
    }
}
