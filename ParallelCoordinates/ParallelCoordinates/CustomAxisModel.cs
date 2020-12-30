using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelCoordinates
{
    public class CustomAxisModel
    {
        private MinMaxInfo plotRanges;

        //Gets or sets the range for the axis. 
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

        private int crossingValue = Int32.MaxValue;

        //Gets or sets the axis crossing value to position the axis in parallel.
        public int CrossingValue
        {
            get
            {
                return crossingValue;
            }
            set
            {
                crossingValue = value;
            }
        }

        private List<string> customAxisLabel;

        //Gets or sets the list of custom axis label values.
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
            CrossingValue = AxisIndex;
            CustomAxisLabels = axisLabels;
            AxisName = name;
            GenerateCustomAxis();
        }

        //Generate the axis parallel based on the axis CrossingValue, PlotRange.
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

            if (CrossingValue != Int32.MaxValue)
                CustomAxis.Crossing = CrossingValue;

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
