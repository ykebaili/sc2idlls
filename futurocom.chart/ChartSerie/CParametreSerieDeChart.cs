using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.ComponentModel;
using sc2i.common;
using System.Drawing;
using sc2i.formulaire;
using System.Drawing.Design;
using futurocom.chart.LegendArea;

namespace futurocom.chart
{
    public class CParametreSerieDeChart : I2iSerializable
    {
        private string m_strNomSerie = "";

        //Fournisseur de valeurs
        private IFournisseurValeursSerie m_fournisseurValeurX = null;
        private EChartValueType m_XValueType = EChartValueType.Auto;
        private Dictionary<int, IFournisseurValeursSerie> m_dicNumToFournisseurY = new Dictionary<int, IFournisseurValeursSerie>();
        private EChartValueType m_YValueType = EChartValueType.Auto;
        private IFournisseurValeursSerie m_fournisseurLibelleAxe = null;
        private IFournisseurValeursSerie m_fournisseurLibelleEtiquette = null;
        private IFournisseurValeursSerie m_fournisseurTooltip = null;
        private IFournisseurValeursSerie m_fournisseurDataAction = null;
        private IFournisseurValeursSerie m_fournisseurSort = null;
        private IFournisseurValeursSerie m_fournisseurIncludePoint = null;

        //Axes utilisés
        private EAxisType m_typeAxeX = EAxisType.Primary;
        private EAxisType m_typeAxeY = EAxisType.Primary;

        //Indique que les valeurs X sont des index, donc l'échelle
        //de l'axe X n'est pas proportionnelle à la valeur de X
        private bool m_bIsValueIndexed = false;

        //Type de graph
        private ESeriesChartType m_chartType = ESeriesChartType.Bar;

        //Emplacement de dessin
        private string m_strChartAreaId = "";

        //Légende
        private bool m_bShowInLegend = true;
        private string m_strLegendText = "";
        private string m_strIdLegendArea = "";
        private string m_strLegendTooltip = "";

        //Style de la série
        private CSerieStyle m_serieStyle = new CSerieStyle();

        //Style des étiquettes
        private CLabelStyle m_labelStyle = new CLabelStyle();

        //Style des markers
        private CMarkerStyle m_markerStyle = new CMarkerStyle();

        //Style des EmptyPoints
        private CSerieStyle m_emptyPointStyle = new CSerieStyle();
        private CMarkerStyle m_emptyPointMarker = new CMarkerStyle();

        //Action sur click
        private CActionSur2iLink m_action = null;

        //-----------------------------------------------------------------
        public CParametreSerieDeChart()
        {
        }


        //-----------------------------------------------------------------
        [Category("Identification")]
        public string SerieName
        {
            get
            {
                return m_strNomSerie;
            }
            set
            {
                m_strNomSerie = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie XValues
        {
            get
            {
                return m_fournisseurValeurX;
            }
            set
            {
                m_fournisseurValeurX = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Data")]
        public EChartValueType XValueType
        {
            get
            {
                return m_XValueType;
            }
            set
            {
                m_XValueType = value;
            }
        }

        //-----------------------------------------------------------------
        private IFournisseurValeursSerie GetFournisseurY(int nIndex)
        {
            IFournisseurValeursSerie f = null;
            m_dicNumToFournisseurY.TryGetValue(nIndex, out f);
            return f;
        }

        //-----------------------------------------------------------------
        private void SetFournisseurY(int nIndex, IFournisseurValeursSerie fournisseur)
        {
            m_dicNumToFournisseurY[nIndex] = fournisseur;                
        }


        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie Y1Values
        {
            get
            {
                return GetFournisseurY(0);
            }
            set
            {
                SetFournisseurY(0, value);
            }
        }

        //-----------------------------------------------------------------
        [Category("Data")]
        public EChartValueType YValueType
        {
            get
            {
                return m_YValueType;
            }
            set
            {
                m_YValueType = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie Y2Values
        {
            get
            {
                return GetFournisseurY(1);
            }
            set
            {
                SetFournisseurY(1, value);
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie Y3Values
        {
            get
            {
                return GetFournisseurY(2);
            }
            set
            {
                SetFournisseurY(2, value);
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie Y4Values
        {
            get
            {
                return GetFournisseurY(3);
            }
            set
            {
                SetFournisseurY(3, value);
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie AxisLabelValues
        {
            get
            {
                return m_fournisseurLibelleAxe;
            }
            set
            {
                m_fournisseurLibelleAxe = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie LabelValues
        {
            get
            {
                return m_fournisseurLibelleEtiquette;
            }
            set
            {
                m_fournisseurLibelleEtiquette = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie ToolTipValues
        {
            get
            {
                return m_fournisseurTooltip;
            }
            set
            {
                m_fournisseurTooltip = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie ActionValues
        {
            get
            {
                return m_fournisseurDataAction;
            }
            set
            {
                m_fournisseurDataAction = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie SortValues
        {
            get
            {
                return m_fournisseurSort;
            }
            set
            {
                m_fournisseurSort = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CFournisseurValeursSerieEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CFournisseurValeurSerieConvertor))]
        [Category("Data")]
        public IFournisseurValeursSerie IncludePointValues
        {
            get
            {
                return m_fournisseurIncludePoint;
            }
            set
            {
                m_fournisseurIncludePoint = value;
            }
        }


        //-----------------------------------------------------------------
        [Category("Design")]
        public EAxisType XAxisType
        {
            get
            {
                return m_typeAxeX;
            }
            set
            {
                m_typeAxeX = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Design")]
        public EAxisType YAxisType
        {
            get
            {
                return m_typeAxeY;
            }
            set
            {
                m_typeAxeY = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Data")]
        public bool IsValueIndexed
        {
            get
            {
                return m_bIsValueIndexed;
            }
            set
            {
                m_bIsValueIndexed = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Design")]
        public ESeriesChartType ChartType
        {
            get
            {
                return m_chartType;
            }
            set
            {
                m_chartType = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAreaFromStringConvertor))]
        [Editor(typeof(CProprieteSelectChartAreaEditor), typeof(UITypeEditor))]
        public string ChartArea
        {
            get
            {
                return m_strChartAreaId;
            }
            set
            {
                m_strChartAreaId = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Legend")]
        public bool ShowInLegend
        {
            get
            {
                return m_bShowInLegend;
            }
            set
            {
                m_bShowInLegend = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Legend")]
        public string LegendLabel
        {
            get
            {
                return m_strLegendText;
            }
            set
            {
                m_strLegendText = value;
            }
        }

        //-----------------------------------------------------------------
        [Editor(typeof(CProprieteSelectChartLegendEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CChartLegendFromStringConvertor))]
        [Category("Legend")]
        public string LegendArea
        {
            get
            {
                return m_strIdLegendArea;
            }
            set
            {
                m_strIdLegendArea = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Legend")]
        public string LegendTooltip
        {
            get
            {
                return m_strLegendTooltip;
            }
            set
            {
                m_strLegendTooltip = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CSerieStyleOptionsConverter))]
        [Category("Design")]
        public CSerieStyle SerieStyle
        {
            get
            {
                return m_serieStyle;
            }
            set
            {
                m_serieStyle = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CSerieStyleOptionsConverter))]
        [Category("Design (Empty points)")]
        public CSerieStyle EmptyPointStyle
        {
            get
            {
                return m_emptyPointStyle;
            }
            set
            {
                m_emptyPointStyle = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CLabelStyleOptionsConverter))]
        [Category("Design")]
        public CLabelStyle LabelStyle
        {
            get
            {
                return m_labelStyle;
            }
            set
            {
                m_labelStyle = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CMarkerStyleOptionsConverter))]
        [Category("Design")]
        public CMarkerStyle MarkerStyle
        {
            get
            {
                return m_markerStyle;
            }
            set
            {
                m_markerStyle = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CMarkerStyleOptionsConverter))]
        [Category("Design (Empty points)")]
        public CMarkerStyle EmptyPointMarker
        {
            get
            {
                return m_emptyPointMarker;
            }
            set
            {
                m_emptyPointMarker = value;
            }
        }

        /////////////////////////////////////////////////////////////////
        [TypeConverter(typeof(CActionSur2iLinkConvertor))]
        [System.ComponentModel.Editor(typeof(CActionSur2iLinkEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(null)]
        public CActionSur2iLink ClickAction
        {
            get
            {
                return m_action;
            }
            set
            {
                m_action = value;
            }
        }

        //-----------------------------------------------------------------
        private int GetNumVersion()
        {
            //return 1;            //1 ajout de XValueType et YValueType
            //return 2;//Ajout de FournisseurIncludePoint
            return 3;//Ajout de EmptyPointstyle et EmptyPointMarker
        }

        //-----------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomSerie);
            result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurValeurX);
            int nNbFY = m_dicNumToFournisseurY.Count;
            serializer.TraiteInt ( ref nNbFY );
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:

                    foreach (KeyValuePair<int, IFournisseurValeursSerie> kv in m_dicNumToFournisseurY)
                    {
                        int nIndex = kv.Key;
                        IFournisseurValeursSerie f = kv.Value;
                        serializer.TraiteInt(ref nIndex);
                        result = serializer.TraiteObject<IFournisseurValeursSerie>(ref f);
                        if (!result)
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture:
                    Dictionary<int, IFournisseurValeursSerie> dic = new Dictionary<int, IFournisseurValeursSerie>();
                    for (int n = 0; n < nNbFY; n++)
                    {
                        int nIndex = 0;
                        IFournisseurValeursSerie f = null;
                        serializer.TraiteInt(ref nIndex);
                        result = serializer.TraiteObject<IFournisseurValeursSerie>(ref f);
                        if (!result)
                            return result;
                        if (f != null)
                            dic[nIndex] = f;
                    }
                    m_dicNumToFournisseurY = dic;
                    break;
            }

            if (result)
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurLibelleAxe);
            if (result)
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurLibelleEtiquette);
            if (result)
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurTooltip);
            if (!result)
                return result;
            serializer.TraiteEnum<EAxisType>(ref m_typeAxeX);
            serializer.TraiteEnum<EAxisType>(ref m_typeAxeY);
            serializer.TraiteBool(ref m_bIsValueIndexed);
            serializer.TraiteEnum<ESeriesChartType>(ref m_chartType);
            serializer.TraiteString(ref m_strChartAreaId);
            serializer.TraiteBool(ref m_bShowInLegend);
            serializer.TraiteString(ref m_strLegendText);
            serializer.TraiteString(ref m_strLegendTooltip);
            serializer.TraiteString(ref m_strIdLegendArea);

            result = serializer.TraiteObject<CSerieStyle>(ref m_serieStyle);
            if (result)
                result = serializer.TraiteObject<CLabelStyle>(ref m_labelStyle);
            if (result)
                result = serializer.TraiteObject<CMarkerStyle>(ref m_markerStyle);
            if (result)
                result = serializer.TraiteObject<CActionSur2iLink>(ref m_action);
            if (result)
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurDataAction);
            if (!result)
                return result;
            if (nVersion >= 1)
            {
                serializer.TraiteEnum<EChartValueType>(ref m_XValueType);
                serializer.TraiteEnum<EChartValueType>(ref m_YValueType);
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurSort);
                if(!result)
                    return result;
            }
            if (nVersion >= 2)
            {
                result = serializer.TraiteObject<IFournisseurValeursSerie>(ref m_fournisseurIncludePoint);
                if (!result)
                    return result;
            }

            if (nVersion >= 3)
            {
                result = serializer.TraiteObject<CSerieStyle>(ref m_emptyPointStyle);
                if (result)
                    result = serializer.TraiteObject<CMarkerStyle>(ref m_emptyPointMarker);
                if (!result)
                    return result;
            }

            return result;
        }


    }
}
