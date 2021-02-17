using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using futurocom.chart;
using System.Drawing;

namespace futurocom.win32.chart
{
    public static class CConvertisseurChartEnumToMSEnum
    {
        //----------------------------------------------------------------------------
        public static SeriesChartType GetMSChartType(ESeriesChartType futValue)
        {
            return (SeriesChartType)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static MarkerStyle GetMSMarkerStyle(EMarkerStyle futValue)
        {
            return (MarkerStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static GradientStyle GetMSGradientStyle(EGradientStyle futValue)
        {
            return (GradientStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ChartHatchStyle GetMSChartHatchStyle(EChartHatchStyle futValue)
        {
            return (ChartHatchStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ChartImageAlignmentStyle GetMSChartImageAlignmentStyle(EChartImageAlignmentStyle futValue)
        {
            return (ChartImageAlignmentStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ChartImageWrapMode GetMSChartImageWrapMode(EChartImageWrapMode futValue)
        {
            return (ChartImageWrapMode)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ChartDashStyle GetMSChartDashStyle(EChartDashStyle futValue)
        {
            return (ChartDashStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static AxisType GetMSAxisType(EAxisType futValue)
        {
            return (AxisType)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LightStyle GetMSLightStyle(ELightStyle futValue)
        {
            return (LightStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static AreaAlignmentOrientations GetMSAreaAlignmentOrientations(EAreaAlignmentOrientations futValue)
        {
            return (AreaAlignmentOrientations)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static AreaAlignmentStyles GetMSAreaAlignmentStyles(EAreaAlignmentStyles futValue)
        {
            return (AreaAlignmentStyles)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static AxisArrowStyle GetMSAxisArrowStyle(EAxisArrowStyle futValue)
        {
            return (AxisArrowStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static AxisEnabled GetMSAxisEnabled(EAxisEnabled futValue)
        {
            return (AxisEnabled)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LabelAutoFitStyles GetMSLabelAutoFitStyles(ELabelAutoFitStyles futValue)
        {
            return (LabelAutoFitStyles)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static DateTimeIntervalType GetMSDateTimeIntervalType(EDateTimeIntervalType futValue)
        {
            return (DateTimeIntervalType)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static TickMarkStyle GetMSTickMarkStyle(ETickMarkStyle futValue)
        {
            return (TickMarkStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static IntervalAutoMode GetMSIntervalAutoMode(EIntervalAutoMode futValue)
        {
            return (IntervalAutoMode)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static TextOrientation GetMSTextOrientation(ETextOrientation futValue)
        {
            return (TextOrientation)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static StringAlignment GetMSStringAlignment(EStringAlignment futValue)
        {
            return (StringAlignment)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static Docking GetMSDocking(EDocking futValue)
        {
            return (Docking)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LegendStyle GetMSLegendStyle(ELegendStyle futValue)
        {
            return (LegendStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LegendTableStyle GetMSLegendTableStyle(ELegendTableStyle futValue)
        {
            return (LegendTableStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LegendItemOrder GetMSLegendItemOrder(ELegendItemOrder futValue)
        {
            return (LegendItemOrder)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static LegendSeparatorStyle GetMSLegendSeparatorStyle(ELegendSeparatorStyle futValue)
        {
            return (LegendSeparatorStyle)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ChartValueType GetMSChartValueType(EChartValueType futValue)
        {
            return (ChartValueType)((int)futValue);
        }

        //----------------------------------------------------------------------------
        public static ScrollBarButtonStyles GetMSScrollBarButtonStyles(EScrollBarButtonStyles futValue)
        {
            return (ScrollBarButtonStyles)((int)futValue);
        }







        
    }
}
