using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelCoordinates
{
    public class SeriesModel
    {
        private string xValue;
        private double yValue;

        public string XValues
        {
            get
            {
                return xValue;
            }
            set
            {
                xValue = value;
            }
        }

        public double YValues
        {
            get
            {
                return yValue;
            }
            set
            {
                yValue = value;
            }
        }

        public SeriesModel(string x, double y)
        {
            XValues = x;
            YValues = y;
        }
    }

    public class ChartModel
    {
        private List<object> variables;

        public List<object> Variable
        {
            get
            {
                return variables;
            }
            set
            {
                variables = value;
            }
        }

        public ChartModel(List<object> values)
        {
            variables = values;
        }
    }
}
