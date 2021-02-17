using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace futurocom.chart
{
    public enum EAxisType
    {
        Primary = 0,
        Secondary = 1
    };

    public enum EMarkerStyle
    {
        None = 0,
        Square = 1,
        Circle = 2,
        Diamond = 3,
        Triangle = 4,
        Cross = 5,
        Star4 = 6,
        Star5 = 7,
        Star6 = 8,
        Star10 = 9,
    }

    public enum ESeriesChartType
    {
        Point = 0,
        FastPoint = 1,
        Bubble = 2,
        Line = 3,
        Spline = 4,
        StepLine = 5,
        FastLine = 6,
        Bar = 7,
        StackedBar = 8,
        StackedBar100 = 9,
        Column = 10,
        StackedColumn = 11,
        StackedColumn100 = 12,
        Area = 13,
        SplineArea = 14,
        StackedArea = 15,
        StackedArea100 = 16,
        Pie = 17,
        Doughnut = 18,
        Stock = 19,
        Candlestick = 20,
        Range = 21,
        SplineRange = 22,
        RangeBar = 23,
        RangeColumn = 24,
        Radar = 25,
        Polar = 26,
        ErrorBar = 27,
        BoxPlot = 28,
        Renko = 29,
        ThreeLineBreak = 30,
        Kagi = 31,
        PointAndFigure = 32,
        Funnel = 33,
        Pyramid = 34,
    }

    public enum EGradientStyle
    {
        None = 0,
        LeftRight = 1,
        TopBottom = 2,
        Center = 3,
        DiagonalLeft = 4,
        DiagonalRight = 5,
        HorizontalCenter = 6,
        VerticalCenter = 7,
    }

    public enum EChartHatchStyle
    {
        None = 0,
        BackwardDiagonal = 1,
        Cross = 2,
        DarkDownwardDiagonal = 3,
        DarkHorizontal = 4,
        DarkUpwardDiagonal = 5,
        DarkVertical = 6,
        DashedDownwardDiagonal = 7,
        DashedHorizontal = 8,
        DashedUpwardDiagonal = 9,
        DashedVertical = 10,
        DiagonalBrick = 11,
        DiagonalCross = 12,
        Divot = 13,
        DottedDiamond = 14,
        DottedGrid = 15,
        ForwardDiagonal = 16,
        Horizontal = 17,
        HorizontalBrick = 18,
        LargeCheckerBoard = 19,
        LargeConfetti = 20,
        LargeGrid = 21,
        LightDownwardDiagonal = 22,
        LightHorizontal = 23,
        LightUpwardDiagonal = 24,
        LightVertical = 25,
        NarrowHorizontal = 26,
        NarrowVertical = 27,
        OutlinedDiamond = 28,
        Percent05 = 29,
        Percent10 = 30,
        Percent20 = 31,
        Percent25 = 32,
        Percent30 = 33,
        Percent40 = 34,
        Percent50 = 35,
        Percent60 = 36,
        Percent70 = 37,
        Percent75 = 38,
        Percent80 = 39,
        Percent90 = 40,
        Plaid = 41,
        Shingle = 42,
        SmallCheckerBoard = 43,
        SmallConfetti = 44,
        SmallGrid = 45,
        SolidDiamond = 46,
        Sphere = 47,
        Trellis = 48,
        Vertical = 49,
        Wave = 50,
        Weave = 51,
        WideDownwardDiagonal = 52,
        WideUpwardDiagonal = 53,
        ZigZag = 54,
    }

    public enum EChartImageAlignmentStyle
    {
        TopLeft = 0,
        Top = 1,
        TopRight = 2,
        Right = 3,
        BottomRight = 4,
        Bottom = 5,
        BottomLeft = 6,
        Left = 7,
        Center = 8,
    }

    public enum EChartImageWrapMode
    {
        Tile = 0,
        TileFlipX = 1,
        TileFlipY = 2,
        TileFlipXY = 3,
        Scaled = 4,
        Unscaled = 100,
    }


    public enum EChartDashStyle
    {
        NotSet = 0,
        Dash = 1,
        DashDot = 2,
        DashDotDot = 3,
        Dot = 4,
        Solid = 5,
    }

    public enum ELightStyle
    {
        None = 0,
        Simplistic = 1,
        Realistic = 2,
    }

    [Flags]
    public enum EAreaAlignmentOrientations
    {
        None = 0,
        Vertical = 1,
        Horizontal = 2,
        All = 3,
    }

    [Flags]
    public enum EAreaAlignmentStyles
    {
        None = 0,
        Position = 1,
        PlotPosition = 2,
        AxesView = 4,
        Cursor = 8,
        All = 15,
    }

    public enum EAxisArrowStyle
    {
        None = 0,
        Triangle = 1,
        SharpTriangle = 2,
        Lines = 3,
    }

    public enum EAxisEnabled
    {
        Auto = 0,
        True = 1,
        False = 2,
    }

    [Flags]
    public enum ELabelAutoFitStyles
    {
        None = 0,
        IncreaseFont = 1,
        DecreaseFont = 2,
        StaggeredLabels = 4,
        LabelsAngleStep30 = 8,
        LabelsAngleStep45 = 16,
        LabelsAngleStep90 = 32,
        WordWrap = 64,
    }

    public enum EDateTimeIntervalType
    {
        Auto = 0,
        Number = 1,
        Years = 2,
        Months = 3,
        Weeks = 4,
        Days = 5,
        Hours = 6,
        Minutes = 7,
        Seconds = 8,
        Milliseconds = 9,
        NotSet = 10,
    }

    public enum ETickMarkStyle
    {
        None = 0,
        OutsideArea = 1,
        InsideArea = 2,
        AcrossAxis = 3,
    }

    public enum EIntervalAutoMode
    {
        FixedCount = 0,
        VariableCount = 1,
    }

    public enum ETextOrientation
    {
        Auto = 0,
        Horizontal = 1,
        Rotated90 = 2,
        Rotated270 = 3,
        Stacked = 4,
    }

    public enum EStringAlignment
    {
        Near = 0,
        Center = 1,
        Far = 2,
    }

    public enum EDocking
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 3,
    }

    public enum ELegendStyle
    {
        Column = 0,
        Row = 1,
        Table = 2,
    }

    public enum ELegendTableStyle
    {
        Auto = 0,
        Wide = 1,
        Tall = 2,
    }

    public enum ELegendItemOrder
    {
        Auto = 0,
        SameAsSeriesOrder = 1,
        ReversedSeriesOrder = 2,
    }

    public enum ELegendSeparatorStyle
    {
        None = 0,
        Line = 1,
        ThickLine = 2,
        DoubleLine = 3,
        DashLine = 4,
        DotLine = 5,
        GradientLine = 6,
        ThickGradientLine = 7,
    }

    public enum EChartValueType
    {
        Auto = 0,
        Double = 1,
        Single = 2,
        Int32 = 3,
        Int64 = 4,
        UInt32 = 5,
        UInt64 = 6,
        String = 7,
        DateTime = 8,
        Date = 9,
        Time = 10,
        DateTimeOffset = 11,
    }

    [Flags]
    public enum EScrollBarButtonStyles
    {
        None = 0,
        SmallScroll = 1,
        ResetZoom = 2,
        All = 3,
    }

    public enum ESelectSerieAlignment
    {
        None = 0,
        Top = 1,
        Left = 2,
        Right = 3,
        Bottom = 4
    }


    
}
