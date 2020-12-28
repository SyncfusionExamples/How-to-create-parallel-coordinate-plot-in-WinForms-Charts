using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelCoordinates
{
    public class CustomAxisModel
    {
        private MinMaxInfo plotRanges;
        public MinMaxInfo PlotRange
        {
            get
            {
                return plotRanges;
            }
            set
            {
                plotRanges = value;
            }
        }

        private int indexs = Int32.MaxValue;

        public int Index
        {
            get
            {
                return indexs;
            }
            set
            {
                indexs = value;
            }
        }

        private List<string> customAxisLabel;

        public List<string> CustomAxisLabels
        {
            get
            {
                return customAxisLabel;
            }
            set
            {
                if (value != null)
                {
                    customAxisLabel = value;
                    GenerateCustomAxis();
                }
            }
        }

        public ChartAxis CustomAxis { get; set; }
        public string AxisName { get; set; }
        public CustomAxisModel(MinMaxInfo range, int AxisIndex, List<string> axisLabels, string name)
        {
            PlotRange = range;
            Index = AxisIndex;
            CustomAxisLabels = axisLabels;
            AxisName = name;
            GenerateCustomAxis();
        }

        private void GenerateCustomAxis()
        {
            if (CustomAxis == null)
                CustomAxis = new ChartAxis()
                {
                    AxisLabelPlacement = ChartPlacement.Inside,
                    EdgeLabelsDrawingMode = ChartAxisEdgeLabelsDrawingMode.Shift,
                    DrawGrid = false,
                    Orientation = ChartOrientation.Vertical
                };

            if (Index != Int32.MaxValue)
                CustomAxis.Crossing = Index;

            if (CustomAxisLabels != null && CustomAxisLabels.Count > 0)
            {
                CustomAxis.FormatLabel -= CustomAxis_FormatLabel;
                CustomAxis.FormatLabel += CustomAxis_FormatLabel;

                PlotRange = new MinMaxInfo(0, CustomAxisLabels.Count - 1, 1);
            }

            if (PlotRange != null)
                CustomAxis.Range = PlotRange;
        }

        private void CustomAxis_FormatLabel(object sender, ChartFormatAxisLabelEventArgs e)
        {
            if (CustomAxisLabels != null && CustomAxisLabels.Count > e.Value)
                e.Label = CustomAxisLabels[System.Convert.ToInt32(e.Value)];

            e.Handled = true;
        }
    }
}
