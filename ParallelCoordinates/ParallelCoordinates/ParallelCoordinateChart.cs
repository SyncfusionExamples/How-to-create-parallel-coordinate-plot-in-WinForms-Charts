using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace ParallelCoordinates
{
    public class ParallelCoordinateChart : ChartControl
    {
        private BindingList<ChartModel> source;

        public BindingList<ChartModel> DataSource
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                GenerateSeries();
            }
        }

        private ChartSeriesType type = ChartSeriesType.Line;

        public ChartSeriesType SeriesType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                AddAxis();
                GenerateSeries();
            }
        }

        private List<CustomAxisModel> customAxisList;

        public List<CustomAxisModel> CustomAxisCollection
        {
            get
            {
                return customAxisList;
            }
            set
            {
                customAxisList = value;
                AddAxis();
            }
        }

        private void AddAxis()
        {
            if (CustomAxisCollection != null)
            {
                foreach (var item in CustomAxisCollection)
                {
                    if (!Axes.Contains(item.CustomAxis))
                        Axes.Add(item.CustomAxis);
                }
            }
        }

        public ParallelCoordinateChart() : base()
        {
            this.PrimaryXAxis.ValueType = ChartValueType.Category;
            this.PrimaryXAxis.IsVisible = true;
            this.PrimaryXAxis.OpposedPosition = true;
            this.PrimaryXAxis.DrawGrid = false;
            this.PrimaryYAxis.ValueType = ChartValueType.Double;
            this.PrimaryYAxis.Range = new MinMaxInfo(0, 100, 10);
            this.PrimaryYAxis.IsVisible = false;
            this.PrimaryYAxis.DrawGrid = false;
            this.PrimaryYAxis.Crossing = 0;
            this.PrimaryYAxis.LineType.ForeColor = Color.Transparent;
            AddAxis();
            GenerateSeries();
        }

        private void GenerateSeries()
        {
            if (DataSource != null && CustomAxisCollection != null && CustomAxisCollection.Count >= DataSource.Count)
            {
                Series.Clear();

                foreach (var item in DataSource)
                {
                    BindingList<SeriesModel> itemsSoruce = new BindingList<SeriesModel>();

                    foreach (var value in item.Variable)
                    {
                        var index = item.Variable.IndexOf(value);
                        var range = CustomAxisCollection[index].PlotRange;
                        var diff = range.Max - range.Min;
                        double result;
                        double yvalue = 0;

                        if (double.TryParse(value.ToString(), out result))
                            yvalue = ((result - range.Min) / (double)diff) * 100;
                        else
                        {
                            result = CustomAxisCollection[index].CustomAxisLabels.IndexOf(value.ToString());
                            yvalue = ((result - range.Min) / (double)diff) * 100;
                        }

                        itemsSoruce.Add(new SeriesModel(CustomAxisCollection[index].AxisName, yvalue));
                    }

                    CategoryAxisDataBindModel dataSeriesModel = new CategoryAxisDataBindModel(itemsSoruce);
                    dataSeriesModel.CategoryName = "XValues";
                    dataSeriesModel.YNames = new string[] { "YValues" };
                    ChartSeries LineSeries = new ChartSeries();
                    LineSeries.Type = SeriesType;
                    LineSeries.SortPoints = false;
                    LineSeries.CategoryModel = dataSeriesModel;
                    this.Series.Add(LineSeries);
                }
            }
        }
    }
}
