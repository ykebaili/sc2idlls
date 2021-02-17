using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using System.Windows.Forms.DataVisualization.Charting;
using futurocom.chart.ChartArea;
using futurocom.chart.LegendArea;
using sc2i.common;
using sc2i.formulaire.win32.editor;
using sc2i.formulaire.win32;
using sc2i.win32.common;

namespace futurocom.win32.chart
{
    public partial class CControlChart : UserControl, IControlALockEdition
    {
        public enum EModeMouseChart
        {
            SimpleMouse = 0,
            Mouse3D,
            Loupe

        }

        private CChartSetup m_chartSetup = null;
        private object m_objetSource = null;
        private EModeMouseChart m_modeSouris = EModeMouseChart.SimpleMouse;
        private bool m_bEnableActions = true;

        private Dictionary<string, object> m_dicValeursVariablesForcées = new Dictionary<string, object>();

        //------------------------------------------------------
        public CControlChart()
        {
            InitializeComponent();
            UpdateBoutonsFromMouseMode();
        }

        //------------------------------------------------------
        private CChartSetup ChartSetup
        {
            get
            {
                return m_chartSetup;
            }
        }

        //------------------------------------------------------
        public EModeMouseChart ModeSouris
        {
            get
            {
                return m_modeSouris;
            }
            set
            {
                m_modeSouris = value;
                UpdateBoutonsFromMouseMode();
            }
        }

        //------------------------------------------------------
        public event EventHandler BeforeCalculate;

        //------------------------------------------------------
        public void Init(CChartSetup chartSetup, object objetSource)
        {
            CWin32Traducteur.Translate(this);
            m_panelTop.Visible = true;
            m_btnFiltrer.Visible = chartSetup.FormulaireFiltreAvance != null &&
                chartSetup.FormulaireFiltreAvance.Childs.Count() > 0;
            m_rbtn3D.Visible = chartSetup.Autoriser3D;
            m_rbtnLoupe.Visible = chartSetup.AutoriserZoom;
            ModeSouris = EModeMouseChart.SimpleMouse;
            m_panelTop.Visible = m_btnFiltrer.Visible || m_rbtnLoupe.Visible || m_rbtn3D.Visible;
            IObjetAIContexteDonnee ob = objetSource as IObjetAIContexteDonnee;
            m_chartSetup = chartSetup;
            if (ob != null && m_chartSetup != null)
                ChartSetup.IContexteDonnee = ob.IContexteDonnee;
            m_chartSetup.ClearCache();
            m_objetSource = objetSource;
            m_chartControl.ResetErreur();
            if (ChartSetup != null)
            {
                ChartSetup.SetRuntimeSource(objetSource);
                if (ChartSetup.FormulaireFiltreSimple != null && ChartSetup.FormulaireFiltreSimple.Childs.Count() > 0)
                {
                    m_panelFiltreSimple.Visible = true;
                    m_panelFormulairefiltreSimple.InitPanel(ChartSetup.FormulaireFiltreSimple, ChartSetup);
                    m_panelFiltreSimple.Height = ChartSetup.FormulaireFiltreSimple.Height;

                }
                else
                    m_panelFiltreSimple.Visible = false;
            }
            RemplirFiltreSeries();
            m_chartControl.BeginInit();

            foreach (KeyValuePair<string, object> kv in m_dicValeursVariablesForcées)
                SetVariableValue(kv.Key, kv.Value);

            if (BeforeCalculate != null)
                BeforeCalculate(this, new EventArgs());
            
            try
            {
                CreateAreas();
                CreateLegends();
                UpdateSeries();
            }
            catch (Exception e)
            {
                m_chartControl.SetErreur(e.Message);
            }
            m_chartControl.EndInit();
        }

        //------------------------------------------------------
        public void UpdateGraph()
        {
            if (m_chartSetup != null && m_objetSource != null)
                Init(m_chartSetup, m_objetSource);
        }

        //------------------------------------------------------
        public void SetVariableValue(string strVariableName, object value)
        {
            if (strVariableName != null)
            {
                m_dicValeursVariablesForcées[strVariableName.ToUpper()] = value;
                if (ChartSetup != null)
                    ChartSetup.SetValeurChampNom(strVariableName, value);
            }
        }

        //------------------------------------------------------
        public object GetVariableValue(string strVariableName)
        {
            object valeur = null;
            if (m_dicValeursVariablesForcées.TryGetValue(strVariableName.ToUpper(), out valeur))
                return valeur;
            if (ChartSetup != null)
                return ChartSetup.GetValeurChampNom(strVariableName);
            return valeur;
        }
            
            

        //------------------------------------------------------
        private void CreateAreas()
        {
            m_chartControl.ChartAreas.Clear();
            foreach (CChartArea futArea in ChartSetup.Areas)
            {
                ChartArea area = new ChartArea(futArea.AreaName);
                AppliqueParametresArea(futArea, area);
                m_chartControl.ChartAreas.Add(area);
            }
        }

        //------------------------------------------------------
        private void AppliqueParametresArea(CChartArea f, ChartArea ms)
        {
            Applique3DStyle(f.Area3DStyle, ms.Area3DStyle);
            AppliqueAxe(f.PrimaryXAxis, ms.AxisX);
            AppliqueAxe(f.PrimaryYAxis, ms.AxisY);
            AppliqueAxe(f.SecondaryXAxis, ms.AxisX2);
            AppliqueAxe(f.SecondaryYAxis, ms.AxisY2);
            CChartAreaStyle sf = f.AreaStyle;
            ms.BackColor = sf.BackColor;
            ms.BackGradientStyle = CConvertisseurChartEnumToMSEnum.GetMSGradientStyle(sf.BackGradientStyle);
            ms.BackHatchStyle = CConvertisseurChartEnumToMSEnum.GetMSChartHatchStyle(sf.HatchStyle);
            //ms.BackImage = sf.BackImage;
            ms.BackImageAlignment = CConvertisseurChartEnumToMSEnum.GetMSChartImageAlignmentStyle(sf.BackImageAlignment);
            ms.BackImageTransparentColor = sf.BackImageTransparentColor;
            ms.BackImageWrapMode = CConvertisseurChartEnumToMSEnum.GetMSChartImageWrapMode(sf.BackImageWrapMode);
            ms.BackSecondaryColor = sf.BackSecondaryColor;
            ms.BorderColor = sf.BorderColor;
            ms.BorderDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(sf.BorderDashStyle);
            ms.BorderWidth = sf.BorderWidth;
            AppliqueCursor(f.CursorX, ms.CursorX);
            AppliqueCursor(f.CursorY, ms.CursorY);
            AppliquePosition(f.Position, ms.Position);
            ms.ShadowColor = sf.ShadowColor;
            ms.ShadowOffset = sf.ShadowOffset;
            ms.Name = f.AreaId;
            ms.AlignmentOrientation = CConvertisseurChartEnumToMSEnum.GetMSAreaAlignmentOrientations(f.AlignmentOrientation);
            ms.AlignmentStyle = CConvertisseurChartEnumToMSEnum.GetMSAreaAlignmentStyles(f.AlignmentStyle);
            if (f.AlignmentArea != null && f.AlignmentArea.Length > 0 &&
                f.AlignmentArea != f.AreaId)
                ms.AlignWithChartArea = f.AlignmentArea;
        }

        //------------------------------------------------------
        private void AppliquePosition(CChartElementPosition f, ElementPosition ms)
        {
            ms.Auto = f.Auto;
            if (!f.Auto)
            {
                ms.X = f.X;
                ms.Y = f.Y;
                ms.Width = f.Width;
                ms.Height = f.Height;
            }
        }

        //------------------------------------------------------
        private void AppliqueCursor(CAxisCursor f, System.Windows.Forms.DataVisualization.Charting.Cursor ms)
        {
            ms.AutoScroll = f.AutoScroll;
            ms.AxisType = CConvertisseurChartEnumToMSEnum.GetMSAxisType(f.AxisType);
            ms.Interval = f.Interval;
            ms.IntervalOffset = f.IntervalOffset;
            ms.IntervalOffsetType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalOffsetType);
            ms.IntervalType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalType);
            ms.IsUserEnabled = f.IsUserEnabled;
            ms.IsUserSelectionEnabled = ModeSouris == EModeMouseChart.Loupe;
            ms.LineColor = f.LineColor;
            ms.LineDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(f.LineDashStyle);
            ms.LineWidth = f.LineWith;
            ms.SelectionColor = f.SelectionColor;
        }

        //------------------------------------------------------


        //------------------------------------------------------
        private void Applique3DStyle(CChart3DStyle f, ChartArea3DStyle ms)
        {
            ms.Enable3D = f.Enable3D;
            ms.Inclination = f.Inclination;
            ms.IsClustered = f.IsClustered;
            ms.IsRightAngleAxes = f.IsRightAngleAxes;
            ms.LightStyle = CConvertisseurChartEnumToMSEnum.GetMSLightStyle(f.LightStyle);
            ms.Perspective = f.Perspective;
            ms.PointDepth = f.PointDepth;
            ms.PointGapDepth = f.PointGapDepth;
            ms.Rotation = f.Rotation;
            ms.WallWidth = f.WallWidth;
        }

        //------------------------------------------------------
        private void AppliqueAxe(CChartAxis f, Axis ms)
        {
            CChartAxisStyle sf = f.Style;
            ms.ArrowStyle = CConvertisseurChartEnumToMSEnum.GetMSAxisArrowStyle(sf.ArrowStyle);
            ms.Enabled = CConvertisseurChartEnumToMSEnum.GetMSAxisEnabled(f.Enabled);
            ms.InterlacedColor = sf.InterlacedColor;
            ms.Interval = f.Interval;
            ms.IntervalAutoMode = CConvertisseurChartEnumToMSEnum.GetMSIntervalAutoMode(f.IntervalAutoMode);
            ms.IntervalOffset = f.IntervalOffset;
            ms.IntervalOffsetType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalOffsetType);
            ms.IntervalType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalType);
            ms.IsInterlaced = sf.IsInterlaced;
            ms.IsLabelAutoFit = f.IsLabelAutoFit;
            ms.IsLogarithmic = f.IsLogarithmic;
            ms.IsMarginVisible = f.IsMarginVisible;
            ms.IsMarksNextToAxis = sf.IsMarksNextToAxis;
            ms.IsReversed = f.IsReversed;
            ms.IsStartedFromZero = f.IsStartedFromZero;
            ms.LabelAutoFitMaxFontSize = f.LabelAutoFitMaxFontSize;
            ms.LabelAutoFitMinFontSize = f.LabelAutoFitMinFontSize;
            ms.LabelAutoFitStyle = CConvertisseurChartEnumToMSEnum.GetMSLabelAutoFitStyles(f.LabelAutoFitStyle);
            AppliqueLabelStyle(f.LabelStyle, ms.LabelStyle);
            ms.LineColor = sf.LineColor;
            ms.LineDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(sf.LineDashStyle);
            ms.LineWidth = sf.LineWidth;
            ms.LogarithmBase = f.LogarithmBase;
            AppliqueGridStyle(f.MajorGridStyle, ms.MajorGrid);
            AppliqueTickMarkerStyle(f.MajorTickMark, ms.MajorTickMark);
            AppliqueGridStyle(f.MinorGridStyle, ms.MinorGrid);
            AppliqueTickMarkerStyle(f.MinorTickMark, ms.MinorTickMark);
            ms.TextOrientation = CConvertisseurChartEnumToMSEnum.GetMSTextOrientation(f.TitleOrientation);
            ms.Title = f.Title;
            ms.TitleAlignment = CConvertisseurChartEnumToMSEnum.GetMSStringAlignment(f.TitleAlignment);
            if (f.TitleFont != null)
                ms.TitleFont = f.TitleFont;
            ms.TitleForeColor = f.TitleForeColor;
            AppliqueScrollBar(f.ScrollBar, ms.ScrollBar);
        }

        //------------------------------------------------------
        private void AppliqueScrollBar(CScrollBarStyle f, AxisScrollBar ms)
        {
            if (f.BackColor != Color.FromArgb(0))
                ms.BackColor = f.BackColor;
            if (f.ButtonColor != Color.FromArgb(0))
                ms.ButtonColor = f.ButtonColor;
            ms.ButtonStyle = CConvertisseurChartEnumToMSEnum.GetMSScrollBarButtonStyles(f.ButtonStyle);
            ms.Enabled = f.Enabled;
            ms.IsPositionedInside = f.IsPositionedInside;
            if ( f.LineColor != Color.FromArgb(0) )
                ms.LineColor = f.LineColor;
            ms.Size = f.Size;
        }

        //------------------------------------------------------
        private void AppliqueLabelStyle(CAxisLabelStyle f, LabelStyle ms)
        {
            ms.Angle = f.Angle;
            ms.Enabled = f.Enabled;
            if (f.Font != null)
                ms.Font = f.Font;
            ms.ForeColor = f.ForeColor;
            ms.Interval = f.Interval;
            ms.IntervalOffset = f.IntervalOffset;
            ms.IntervalOffsetType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalOffsetType);
            ms.IntervalType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalType);
            ms.IsEndLabelVisible = f.IsEndLabelVisible;
            ms.IsStaggered = f.IsStaggered;
            ms.TruncatedLabels = f.TruncatedLabels;
            ms.Format = f.Format;
        }

        //------------------------------------------------------
        private void AppliqueGridStyle(CChartGridStyle f, Grid ms)
        {
            ms.Enabled = f.Enabled;
            ms.Interval = f.Interval;
            ms.IntervalOffset = f.IntervalOffset;
            ms.IntervalOffsetType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalOffsetType);
            ms.IntervalType = CConvertisseurChartEnumToMSEnum.GetMSDateTimeIntervalType(f.IntervalType);
            ms.LineColor = f.LineColor;
            ms.LineDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(f.LineDashStyle);
            ms.LineWidth = f.LineWidth;
        }

        //------------------------------------------------------
        private void AppliqueTickMarkerStyle(CChartTickMark f, TickMark ms)
        {
            AppliqueGridStyle(f, ms);
            ms.Size = f.Size;
            ms.TickMarkStyle = CConvertisseurChartEnumToMSEnum.GetMSTickMarkStyle(f.TickMarkStyle);
        }

        //------------------------------------------------------
        private void CreateLegends()
        {
            m_chartControl.Legends.Clear();
            foreach (CLegendArea legend in ChartSetup.Legends)
            {
                Legend msLegend = new Legend(legend.LegendId);
                AppliqueLegend(legend, msLegend);
                m_chartControl.Legends.Add(msLegend);
            }
        }

        //------------------------------------------------------
        private void AppliqueLegend(CLegendArea f, Legend ms)
        {
            ms.Alignment = CConvertisseurChartEnumToMSEnum.GetMSStringAlignment(f.Alignment);
            CChartLegendStyle sf = f.LegendStyle;
            ms.AutoFitMinFontSize = sf.AutoFitMinFontSize;
            ms.BackColor = sf.BackColor;
            ms.BackGradientStyle = CConvertisseurChartEnumToMSEnum.GetMSGradientStyle(sf.BackGradientStyle);
            ms.BackHatchStyle = CConvertisseurChartEnumToMSEnum.GetMSChartHatchStyle(sf.HatchStyle);
            //ms.BackImage = sf.BackImage;
            ms.BackImageAlignment = CConvertisseurChartEnumToMSEnum.GetMSChartImageAlignmentStyle(sf.BackImageAlignment);
            ms.BackImageTransparentColor = sf.BackImageTransparentColor;
            ms.BackImageWrapMode = CConvertisseurChartEnumToMSEnum.GetMSChartImageWrapMode(sf.BackImageWrapMode);
            ms.BackSecondaryColor = sf.BackSecondaryColor;
            ms.BorderColor = sf.BorderColor;
            ms.BorderDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(sf.BorderDashStyle);
            ms.BorderWidth = sf.BorderWidth;
            ms.DockedToChartArea = f.DockedArea;
            ms.Docking = CConvertisseurChartEnumToMSEnum.GetMSDocking(f.Docking);
            ms.Enabled = sf.Enabled;
            if (sf.Font != null)
                ms.Font = sf.Font;
            ms.ForeColor = sf.ForeColor;
            ms.InterlacedRows = sf.InterlacedRows;
            ms.InterlacedRowsColor = sf.InterlacedRowsColor;
            ms.IsDockedInsideChartArea = f.IsDockedInsideChartArea;
            ms.IsEquallySpacedItems = sf.IsEquallySpacedItems;
            ms.IsTextAutoFit = sf.IsTextAutoFit;
            ms.LegendItemOrder = CConvertisseurChartEnumToMSEnum.GetMSLegendItemOrder(sf.LegendItemOrder);
            ms.LegendStyle = CConvertisseurChartEnumToMSEnum.GetMSLegendStyle(sf.LegendStyle);
            ms.MaximumAutoSize = f.MaximumAutoSize;
            ms.Name = f.LegendId;
            AppliquePosition(sf.Position, ms.Position);
            ms.ShadowColor = sf.ShadowColor;
            ms.ShadowOffset = sf.ShadowOffset;
            ms.TableStyle = CConvertisseurChartEnumToMSEnum.GetMSLegendTableStyle(sf.TableStyle);
            ms.TextWrapThreshold = sf.TextWrapThreshold;
            ms.Title = f.Title;
            ms.TitleAlignment = CConvertisseurChartEnumToMSEnum.GetMSStringAlignment(f.TitleAlignment);
            ms.TitleBackColor = f.TitleBackColor;
            if (f.TitleFont != null)
                ms.TitleFont = f.TitleFont;
            ms.TitleForeColor = f.TitleForeColor;
            ms.TitleSeparator = CConvertisseurChartEnumToMSEnum.GetMSLegendSeparatorStyle(f.TitleSeparator);
            ms.TitleSeparatorColor = f.TitleSeparatorColor;
        }


        private bool m_bIsCheckingFiltresSeries = false;
        private void RemplirFiltreSeries()
        {

            m_bIsCheckingFiltresSeries = true;
            this.SuspendDrawing();
            switch ( ChartSetup.SelectSeriesAlignment)
            {
                case ESelectSerieAlignment.Left :
                case ESelectSerieAlignment.Right :
                    m_lblSelectSeriesTitle.Dock = DockStyle.Top;
                    m_panelFiltreSeries.Width = 150;
                    break;
                default :
                    m_lblSelectSeriesTitle.Dock = DockStyle.Left;
                    m_panelFiltreSeries.Height = 40;
                    break;
            }
            switch (ChartSetup.SelectSeriesAlignment)
            {
                case ESelectSerieAlignment.Top:
                    m_panelFiltreSeries.Dock = DockStyle.Top;
                    break;
                case ESelectSerieAlignment.Left:
                    m_panelFiltreSeries.Dock = DockStyle.Left;
                    break;
                case ESelectSerieAlignment.Right:
                    m_panelFiltreSeries.Dock = DockStyle.Right;
                    break;
                case ESelectSerieAlignment.Bottom:
                    m_panelFiltreSeries.Dock = DockStyle.Bottom;
                    break;
                default:
                    break;
            }
            m_wndListeSeries.BeginUpdate();
            m_wndListeSeries.Items.Clear();
            foreach (CParametreSerieDeChart pSerie in ChartSetup.Series)
            {
                string strName = pSerie.SerieName;
                ListViewItem item = new ListViewItem(strName);
                item.Tag = pSerie;
                item.ImageIndex = 0;
                m_wndListeSeries.Items.Add(item);
                item.Checked = true;
            }
            m_wndListeSeries.EndUpdate();
            m_panelFiltreSeries.Visible = ChartSetup.SelectSeriesAlignment != ESelectSerieAlignment.None;
            this.ResumeDrawing();
            m_bIsCheckingFiltresSeries = false;
        }

       
        //-------------------------------------------------------------------------
        private void m_wndListeSeries_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if ( !m_bIsCheckingFiltresSeries )
                UpdateSeries();
        }

        //-------------------------------------------------------------------------
        private bool SerieIsChecked(CParametreSerieDeChart pSerie)
        {
            if (pSerie != null)
            {
                foreach (ListViewItem item in m_wndListeSeries.Items)
                    if (item.Tag == pSerie && item.Checked)
                        return true;
            }
            return false;
        }
        
        //-------------------------------------------------------------------------
        private void UpdateSeries()
        {
            m_bIsCheckingFiltresSeries = true;
            HashSet<string> areasToHide = new HashSet<string>();
            foreach (ChartArea a in m_chartControl.ChartAreas)
                areasToHide.Add(a.Name);
            m_chartControl.Series.Clear();
            foreach (CParametreSerieDeChart pSerie in ChartSetup.Series)
            {
                if (!SerieIsChecked(pSerie))
                    continue;

                Series s = new Series(pSerie.SerieName);
                s.Tag = pSerie;
                s.ChartType = CConvertisseurChartEnumToMSEnum.GetMSChartType(pSerie.ChartType);
                s.XAxisType = CConvertisseurChartEnumToMSEnum.GetMSAxisType(pSerie.XAxisType);
                s.YAxisType = CConvertisseurChartEnumToMSEnum.GetMSAxisType(pSerie.YAxisType);
                s.IsXValueIndexed = pSerie.IsValueIndexed;
                s.ToolTip = "";
                if (pSerie.ChartArea != null && pSerie.ChartArea.Length > 0)
                {
                    foreach (ChartArea a in m_chartControl.ChartAreas)
                    {
                        if (a.Name == pSerie.ChartArea)
                            s.ChartArea = a.Name; ;
                    }
                }
                
                s.Legend = pSerie.LegendArea;
                s.IsVisibleInLegend = pSerie.ShowInLegend;
                if (pSerie.LegendLabel.Length > 0)
                    s.LegendText = pSerie.LegendLabel;
                if (pSerie.LegendTooltip.Length > 0)
                    s.LegendToolTip = pSerie.LegendTooltip;

                s.XValueType = CConvertisseurChartEnumToMSEnum.GetMSChartValueType(pSerie.XValueType);
                s.YValueType = CConvertisseurChartEnumToMSEnum.GetMSChartValueType(pSerie.YValueType);

                List<double> lstX = GetValues<double>(pSerie.XValues, Double.NaN);
                List<List<double>> lstY = new List<List<double>>();
                lstY.Add(GetValues<double>(pSerie.Y1Values, Double.NaN));
                lstY.Add(GetValues<double>(pSerie.Y1Values, Double.NaN));
                lstY.Add(GetValues<double>(pSerie.Y2Values, Double.NaN));
                lstY.Add(GetValues<double>(pSerie.Y3Values, Double.NaN));
                List<string> lstLabels = GetValues<string>(pSerie.LabelValues);
                List<string> lstAxisValues = GetValues<string>(pSerie.AxisLabelValues);
                List<string> lstTooltips = GetValues<string>(pSerie.ToolTipValues);
                List<object> lstValeursAction = GetValues<object>(pSerie.ActionValues);
                List<object> lstValeursSort = GetValues<object>(pSerie.SortValues);
                List<object> lstIncludePoint = GetValues<object>(pSerie.IncludePointValues);

                if (pSerie.XValues == null || lstX.Count == 0)
                {
                    //Ajoute des indices en fonction des valeurs d'axe
                    for (int nIndex = 0; nIndex < lstAxisValues.Count; nIndex++)
                        lstX.Add(nIndex);
                }

                List<CFuturocomDataPoint> lstPoints = new List<CFuturocomDataPoint>();
                bool bAllNull = true;
                for (int n = 0; n < lstX.Count; n++)
                {
                    bool bNullPoint = false;
                    List<double> vals = new List<double>();
                    foreach (List<double> lstVals in lstY)
                    {
                        if (n < lstVals.Count)
                        {
                            vals.Add(lstVals[n]);
                            if (Double.IsNaN(lstVals[n]))
                                bNullPoint = true;
                        }
                        else if (lstVals.Count > 0)
                            vals.Add(0);
                    }

                    bool bInclude = true;
                    
                    if ( n < lstIncludePoint.Count )
                    {
                        if ( lstIncludePoint[n] is bool && !((bool)lstIncludePoint[n]))
                            bInclude = false;
                    }
                    if (bInclude)
                    {
                        CFuturocomDataPoint pt = new CFuturocomDataPoint();
                        pt.XValue = lstX[n];
                        if (!bNullPoint)
                        {
                            bAllNull = false;
                            pt.YValues = vals.ToArray();
                        }
                        else
                        {
                            pt.IsEmpty = true;
                        }
                        if (n < lstLabels.Count)
                            pt.Label = lstLabels[n] == null ? "" : lstLabels[n];
                        if (n < lstAxisValues.Count)
                            pt.AxisLabel = lstAxisValues[n] == null ? "" : lstAxisValues[n];
                        if (n < lstTooltips.Count)
                            pt.CustomToolTip = lstTooltips[n] == null ? "" : lstTooltips[n];
                        if (n < lstValeursAction.Count)
                            pt.ValeurPourAction = lstValeursAction[n];
                        if (n < lstValeursSort.Count)
                            pt.ValeurSort = lstValeursSort[n];
                        lstPoints.Add(pt);
                    }
                    
                }
                if (bAllNull)
                    lstPoints.Clear();
                lstPoints.Sort(new CPointSorter());
                foreach ( CFuturocomDataPoint pt in lstPoints )
                    s.Points.Add ( pt );
                if (s.Points.Count != 0)
                {
                    if (s.ChartArea != null && s.ChartArea!="")
                        areasToHide.Remove(s.ChartArea);
                    else
                        areasToHide.Remove(m_chartControl.ChartAreas[0].Name);
                    ApplyMarkerStyle(pSerie, s);
                    ApplySerieStyle(pSerie, s);
                    ApplyLabelStyle(pSerie, s);
                    ApplyEmptyPointStyle(pSerie, s);
                    ApplyEmptyPointMarkerStyle(pSerie, s);
                    m_chartControl.Series.Add(s);
                    //m_chartControl.DataManipulator.Sort(new CPointSorter(), s);
                    foreach (ListViewItem item in m_wndListeSeries.Items)
                        if (item.Tag == pSerie)
                        {
                            item.ImageIndex = 0;
                            item.ToolTipText = "";
                        }
                }
                else
                    foreach (ListViewItem item in m_wndListeSeries.Items)
                        if (item.Tag == pSerie)
                        {
                            m_wndListeSeries.ShowItemToolTips = true;
                            item.ImageIndex = 1;
                            item.ToolTipText = I.T("@1 has no data|20047", item.Text);
                        }
                
            }
            foreach (ChartArea a in m_chartControl.ChartAreas)
                a.Visible = !areasToHide.Contains(a.Name);

            m_bIsCheckingFiltresSeries = false;
        }

        private class CPointSorter : IComparer<DataPoint>
        {

            public int Compare(DataPoint x, DataPoint y)
            {
                CFuturocomDataPoint fX = x as CFuturocomDataPoint;
                CFuturocomDataPoint fY = y as CFuturocomDataPoint;
                if (fX != null && fY != null)
                {
                    object v1 = fX.ValeurSort;
                    object v2 = fY.ValeurSort;
                    if (v1 != null || v2 != null)
                    {
                        if (v1 == null && v2 != null)
                            return -1;
                        if (v2 == null && v1 != null)
                            return 1;
                        if (v1 is IComparable && v2 is IComparable)
                            return ((IComparable)v1).CompareTo((IComparable)v2);
                        return 0;
                    }
                }
                return x.XValue.CompareTo(y.XValue);
            }
        }


        //---------------------------------------------------------------------
        private void ApplyMarkerStyle(CParametreSerieDeChart pSerie, Series s)
        {
            CMarkerStyle style = pSerie.MarkerStyle;
            s.MarkerBorderColor = style.BorderColor;
            s.MarkerBorderWidth = style.BorderWidth;
            s.MarkerColor = style.BackColor;
            //s.MarkerImage = style.MarkerImage;
            s.MarkerImageTransparentColor = style.MarkerImageTransparentColor;
            s.MarkerSize = style.MarkerSize;
            s.MarkerStep = style.MarkerStep;
            s.MarkerStyle = CConvertisseurChartEnumToMSEnum.GetMSMarkerStyle(style.MarkerStyle);
        }

        //---------------------------------------------------------------------
        private void ApplyEmptyPointMarkerStyle ( CParametreSerieDeChart pSerie, Series s)
        {
            CMarkerStyle style = pSerie.EmptyPointMarker;
            s.EmptyPointStyle.MarkerBorderColor = style.BorderColor;
            s.EmptyPointStyle.MarkerBorderWidth = style.BorderWidth;
            s.EmptyPointStyle.MarkerColor = style.BackColor;
            //s.MarkerImage = style.MarkerImage;
            s.EmptyPointStyle.MarkerImageTransparentColor = style.MarkerImageTransparentColor;
            s.EmptyPointStyle.MarkerSize = style.MarkerSize;
            s.EmptyPointStyle.MarkerStyle = CConvertisseurChartEnumToMSEnum.GetMSMarkerStyle(style.MarkerStyle);
        }

        //---------------------------------------------------------------------
        private void ApplySerieStyle(CParametreSerieDeChart pSerie, Series s)
        {
            CSerieStyle style = pSerie.SerieStyle;
            s.BackGradientStyle = CConvertisseurChartEnumToMSEnum.GetMSGradientStyle(style.BackGradientStyle);
            s.BackHatchStyle = CConvertisseurChartEnumToMSEnum.GetMSChartHatchStyle(style.BackHatchStyle);
            s.BackImageAlignment = CConvertisseurChartEnumToMSEnum.GetMSChartImageAlignmentStyle(style.BackImageAlignment);
            s.BackImageTransparentColor = style.BackImageTransparentColor;
            s.BackImageWrapMode = CConvertisseurChartEnumToMSEnum.GetMSChartImageWrapMode(style.BackImageWrapMode);
            s.BackSecondaryColor = style.BackSecondaryColor;
            s.BorderColor = style.BorderColor;
            s.BorderDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(style.BorderDashStyle);
            s.BorderWidth = style.BorderWidth;
            if (style.SerieColor.ToArgb() != 0)
            {
                s.Color = Color.FromArgb(style.SerieOpacity * 255 / 100, style.SerieColor);
            }
            if (style.ShadowColor.ToArgb() != 0)
                s.ShadowColor = style.ShadowColor;
            s.ShadowOffset = style.ShadowOffset;
        }

        //---------------------------------------------------------------------
        private void ApplyEmptyPointStyle(CParametreSerieDeChart pSerie, Series s)
        {
            CSerieStyle style = pSerie.EmptyPointStyle;
            s.EmptyPointStyle.BackGradientStyle = CConvertisseurChartEnumToMSEnum.GetMSGradientStyle(style.BackGradientStyle);
            s.EmptyPointStyle.BackHatchStyle = CConvertisseurChartEnumToMSEnum.GetMSChartHatchStyle(style.BackHatchStyle);
            s.EmptyPointStyle.BackImageAlignment = CConvertisseurChartEnumToMSEnum.GetMSChartImageAlignmentStyle(style.BackImageAlignment);
            s.EmptyPointStyle.BackImageTransparentColor = style.BackImageTransparentColor;
            s.EmptyPointStyle.BackImageWrapMode = CConvertisseurChartEnumToMSEnum.GetMSChartImageWrapMode(style.BackImageWrapMode);
            s.EmptyPointStyle.BackSecondaryColor = style.BackSecondaryColor;
            s.EmptyPointStyle.BorderColor = style.BorderColor;
            s.EmptyPointStyle.BorderDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(style.BorderDashStyle);
            s.EmptyPointStyle.BorderWidth = style.BorderWidth;
            if (style.SerieColor.ToArgb() != 0)
            {
                s.EmptyPointStyle.Color = Color.FromArgb(style.SerieOpacity * 255 / 100, style.SerieColor);
            }
            else
                s.EmptyPointStyle.Color = s.Color;
        }

        //---------------------------------------------------------------------
        private void ApplyLabelStyle(CParametreSerieDeChart pSerie, Series s)
        {
            CLabelStyle style = pSerie.LabelStyle;
            s.LabelAngle = style.Angle;
            s.LabelBackColor = style.BackColor;
            s.LabelBorderColor = style.BorderColor;
            s.LabelBorderDashStyle = CConvertisseurChartEnumToMSEnum.GetMSChartDashStyle(style.BorderDash);
            s.LabelBorderWidth = style.BorderWidth;
            s.LabelForeColor = style.ForeColor;
            s.LabelFormat = style.Format;
            if (style.Font != null)
                s.Font = style.Font;
        }


        //---------------------------------------------------------------------
        private List<T> GetValues<T>(IFournisseurValeursSerie fournisseur)
            where T : class
        {
            return GetValues(fournisseur, (T)null);
        }

        //---------------------------------------------------------------------
        private List<T> GetValues<T>(IFournisseurValeursSerie fournisseur, T defaultValue)
        {
            List<T> lstValeurs = new List<T>();
            if (fournisseur != null)
            {
                foreach (object val in fournisseur.GetValues(ChartSetup))
                {
                    try
                    {
                        if (val != null)
                        {
                            T v;
                            if (typeof(double).IsAssignableFrom(typeof(T)) && val is DateTime)
                                v = (T)Convert.ChangeType(((DateTime)val).ToOADate(), typeof(T));
                            else
                                v = (T)Convert.ChangeType(val, typeof(T));
                            lstValeurs.Add(v);
                        }
                        else
                            lstValeurs.Add(defaultValue);
                    }
                    catch
                    {
                        lstValeurs.Add(defaultValue);
                    }
                }
            }
            return lstValeurs;
        }

        private Point m_ptStartDrag = new Point(0, 0);
        private int m_n3DInclinationStart = 0;
        private int m_n3DRotationStart = 0;
        private int m_n3DPointDepthStart = 0;
        private int m_n3DPerspectiveStart = 0;
        private ChartArea m_chartAreaFor3D = null;
        //------------------------------------------------------------------------
        private void m_chartControl_MouseDown(object sender, MouseEventArgs e)
        {
            m_chartAreaFor3D = null;
            if (ModeSouris == EModeMouseChart.Mouse3D &&
                ((e.Button & MouseButtons.Left) == MouseButtons.Left ||
                (e.Button & MouseButtons.Right) == MouseButtons.Right))
            {
                HitTestResult hit = m_chartControl.HitTest(e.X, e.Y, ChartElementType.PlottingArea);
                if (hit != null && hit.ChartArea != null)
                {
                    m_chartAreaFor3D = hit.ChartArea;
                    if (!m_chartAreaFor3D.Area3DStyle.Enable3D)
                    {
                        m_chartAreaFor3D.Area3DStyle.Enable3D = true;
                        m_chartAreaFor3D.Area3DStyle.Inclination = 0;
                        m_chartAreaFor3D.Area3DStyle.Rotation = 0;
                    }

                    m_n3DInclinationStart = m_chartAreaFor3D.Area3DStyle.Inclination;
                    m_n3DRotationStart = m_chartAreaFor3D.Area3DStyle.Rotation;
                    m_n3DPointDepthStart = m_chartAreaFor3D.Area3DStyle.PointDepth;
                    m_n3DPerspectiveStart = m_chartAreaFor3D.Area3DStyle.Perspective;

                    m_ptStartDrag = new Point(e.X, e.Y);
                    m_chartControl.Capture = true;
                }
            }
        }

        //------------------------------------------------------------------------
        public Chart ChartControl
        {
            get
            {
                return m_chartControl;
            }
        }

        //------------------------------------------------------------------------
        private void m_chartControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_chartAreaFor3D != null)
            {
                if (Math.Abs(m_chartAreaFor3D.Area3DStyle.Inclination) <= 3 &&
                    Math.Abs(m_chartAreaFor3D.Area3DStyle.Rotation) <= 3)
                    m_chartAreaFor3D.Area3DStyle.Enable3D = false;
            }
            m_chartAreaFor3D = null;
            m_chartControl.Capture = false;

            if (m_modeSouris == EModeMouseChart.SimpleMouse && m_bEnableActions)
            {
                HitTestResult test = m_chartControl.HitTest(e.X, e.Y, ChartElementType.DataPoint);
                if (test != null && test.Series != null && test.PointIndex >= 0)
                {
                    CParametreSerieDeChart pSerie = test.Series.Tag as CParametreSerieDeChart;
                    if (pSerie != null && pSerie.ClickAction != null)
                    {
                        try
                        {
                            CFuturocomDataPoint pt = test.Series.Points[test.PointIndex] as CFuturocomDataPoint;
                            CValeurPourChartAction valeur = new CValeurPourChartAction();
                            valeur.ValueForAction = pt.ValeurPourAction;
                            CResultAErreur result = CExecuteurActionSur2iLink.ExecuteAction(this,
                                pSerie.ClickAction, valeur);
                            if (!result)
                                CFormAlerte.Afficher(result.Erreur);
                        }
                        catch { }
                    }
                }
            }

        }

        private CFuturocomDataPoint m_lastToolTipPoint = null;
        private MarkerStyle m_lastTooltipMarkerStyle = MarkerStyle.None;
        private int m_lastTooltipMarkerSize = 0;
        //------------------------------------------------------------------------
        private void m_chartControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (ModeSouris == EModeMouseChart.Mouse3D &&
                (e.Button & MouseButtons.Left) == MouseButtons.Left &&
                m_chartAreaFor3D != null)
            {
                int nInclination = -m_ptStartDrag.Y + e.Y + m_n3DInclinationStart;
                if (nInclination > 89)
                    nInclination = 89;
                if (nInclination < -89)
                    nInclination = -89;
                int nRotation = (m_ptStartDrag.X - e.X + m_n3DRotationStart);
                if (nRotation < 180)
                    nRotation = 360 + nRotation;
                if (nRotation > 180)
                    nRotation = -360 + nRotation;
                m_chartAreaFor3D.Area3DStyle.Inclination = nInclination;
                m_chartAreaFor3D.Area3DStyle.Rotation = nRotation;
            }
            if (ModeSouris == EModeMouseChart.Mouse3D &&
                (e.Button & MouseButtons.Right) == MouseButtons.Right &&
                m_chartAreaFor3D != null)
            {
                int nPointDepth = -m_ptStartDrag.Y + e.Y + m_n3DPointDepthStart;
                int nPerspective = m_ptStartDrag.X - e.X + m_n3DPerspectiveStart;
                if (nPointDepth < 0)
                    nPointDepth = 0;
                if (nPointDepth > 1000)
                    nPointDepth = 1000;
                if (nPerspective > 100)
                    nPerspective = 100;
                if (nPerspective < 0)
                    nPerspective = 0;
                m_chartAreaFor3D.Area3DStyle.PointDepth = nPointDepth;
                m_chartAreaFor3D.Area3DStyle.Perspective = nPerspective;
            }

            if ( ModeSouris == EModeMouseChart.SimpleMouse)
            {
                HitTestResult test = m_chartControl.HitTest(e.X, e.Y, ChartElementType.DataPoint);
                if (test != null && test.Series != null && test.PointIndex >= 0)
                {
                    CFuturocomDataPoint pt = test.Series.Points[test.PointIndex] as CFuturocomDataPoint;
                    ShowToolTip(pt, e.X+(Cursor != null?Cursor.Size.Width:16), e.Y+(Cursor != null?Cursor.Size.Height:16));

                }
                else
                    ShowToolTip(null, e.X, e.Y);
            }
        }

        //------------------------------------------------------------------------


        //------------------------------------------------------------------------
        private void ShowToolTip ( CFuturocomDataPoint point, int nX, int nY )
        {
            if (m_lastToolTipPoint != null && m_lastToolTipPoint != point)
            {
                m_lastToolTipPoint.MarkerSize = m_lastTooltipMarkerSize;
                m_lastToolTipPoint.MarkerStyle = m_lastTooltipMarkerStyle;
            }
            if ( point != null && point != m_lastToolTipPoint )
            {
                m_lastTooltipMarkerStyle = point.MarkerStyle;
                m_lastTooltipMarkerSize = point.MarkerSize;
                point.MarkerStyle = MarkerStyle.Circle;
                point.MarkerSize = Math.Max(point.MarkerSize * 2, 3);
                m_chartTooltip.Show(point.CustomToolTip, m_chartControl, nX, nY, 5000);
            }
            if (point == null && m_lastToolTipPoint != null)
                m_chartTooltip.Hide(m_chartControl);
            m_lastToolTipPoint = point;
        }

        //------------------------------------------------------------------------
        private bool m_bIsUpdatingBoutonsMode = false;
        private void UpdateBoutonsFromMouseMode()
        {
            m_bIsUpdatingBoutonsMode = true;
            m_rbtn3D.Checked = ModeSouris == EModeMouseChart.Mouse3D;
            m_rbtnMouse.Checked = ModeSouris == EModeMouseChart.SimpleMouse;
            switch (ModeSouris)
            {
                case EModeMouseChart.SimpleMouse:
                    m_chartControl.Cursor = Cursors.Arrow;
                    break;
                case EModeMouseChart.Mouse3D:
                    m_chartControl.Cursor = Cursors.SizeAll;
                    break;
                case EModeMouseChart.Loupe:
                    m_chartControl.Cursor = C2iCursorLoader.LoadCursor(typeof(CControlChart), "LoupePlus", Resource1.curLoupePlus);
                    break;
                default:
                    break;
            }
            foreach (ChartArea area in m_chartControl.ChartAreas)
            {
                area.CursorX.IsUserSelectionEnabled = ModeSouris == EModeMouseChart.Loupe;
                area.CursorY.IsUserSelectionEnabled = ModeSouris == EModeMouseChart.Loupe;
            }
            m_bIsUpdatingBoutonsMode = false;
        }

        //------------------------------------------------------------------------
        private void m_rbtnMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bIsUpdatingBoutonsMode)
            {
                if (m_rbtnMouse.Checked)
                    ModeSouris = EModeMouseChart.SimpleMouse;
            }
        }

        //------------------------------------------------------------------------
        private void m_rbtn3D_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bIsUpdatingBoutonsMode)
            {
                if (m_rbtn3D.Checked)
                    ModeSouris = EModeMouseChart.Mouse3D;
            }
        }

        //------------------------------------------------------------------------
        private void m_rbtnLoupe_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bIsUpdatingBoutonsMode)
            {
                if (m_rbtnLoupe.Checked)
                    ModeSouris = EModeMouseChart.Loupe;
            }
        }

        

        private void m_btnFiltrer_Click(object sender, EventArgs e)
        {
            if (ChartSetup != null && ChartSetup.FormulaireFiltreAvance != null)
            {
                if (CFormFormulairePopup.EditeElement(ChartSetup.FormulaireFiltreAvance,
                    ChartSetup,
                    I.T("Filter graph|20032")))
                {
                    Init(m_chartSetup, m_objetSource);
                }
            }
        }



        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion


        private void m_rbtn3D_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void m_rbtn3D_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                foreach (ChartArea area in m_chartControl.ChartAreas)
                    area.Area3DStyle.Enable3D = false;
            }
        }

        private void m_chartControl_PostPaint(object sender, ChartPaintEventArgs e)
        {

        }

        private void m_chartControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void m_chartControl_PrePaint(object sender, ChartPaintEventArgs e)
        {

        }

        private void m_btnApplyFilter_Click(object sender, EventArgs e)
        {
            CResultAErreur result = m_panelFormulairefiltreSimple.AffecteValeursToElement();
            if (!result)
            {
                CFormAlerte.Afficher(result);
                return;
            }
            Init(m_chartSetup, m_objetSource);
        }

        private void m_chartControl_MouseLeave(object sender, EventArgs e)
        {
            ShowToolTip(null, 0, 0);
        }

        

       

    }
}
