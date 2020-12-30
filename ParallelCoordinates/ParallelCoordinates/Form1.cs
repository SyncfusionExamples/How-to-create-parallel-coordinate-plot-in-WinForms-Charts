using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelCoordinates
{
    public partial class Form1 : MetroForm
    {
        private IContainer components = null;
        ParallelCoordinateChart chartControl;
        public Form1()
        {
            BorderColor = Color.FromArgb(0xFF, 0xCD, 0xCD, 0xCD);
            BorderThickness = 3;
            CaptionBarHeight = 75;
            CaptionBarColor = Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2);
            CaptionFont = new Font("Segoe UI", 22.0f);
            CaptionForeColor = Color.White;
            CaptionAlign = HorizontalAlignment.Left;
            ShowIcon = false;
            CaptionButtonColor = Color.White;
            CaptionButtonHoverColor = Color.White;
            InitializeComponent();
            this.BackColor = Color.White;
            this.chartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.chartControl = new ParallelCoordinateChart();
            this.SuspendLayout();
            this.chartControl.Legend.Visible = false;

            var axisCollection = new List<CustomAxisModel>()
                {
                    new CustomAxisModel(new MinMaxInfo(2000, 2010, 2), 0, null, "Date"),
                    new CustomAxisModel(new MinMaxInfo(80, 120, 5), 1, null, "Goals"),
                    new CustomAxisModel(new MinMaxInfo(0, 2.5, 0.5), 2, null, "Values"),
                    new CustomAxisModel(null, 3, new List<string>() { "Lzumi", "New Balance", "Brooks", "Asics" }, "Brands"),
                    new CustomAxisModel(null, 4, new List<string>() { " ", "<5 miles", ">5 miles", " " }, "Period")
                };

            this.chartControl.CustomAxisCollection = axisCollection;
            this.chartControl.SeriesType = ChartSeriesType.Spline;

            BindingList<ChartModel> dataSource = new BindingList<ChartModel>()
                {
                    new ChartModel(new List<object>() { 2000, 100, 2.0, "Lzumi", "<5 miles" }),
                    new ChartModel(new List<object>() { 2006, 115, 1.0, "New Balance", ">5 miles" }),
                    new ChartModel(new List<object>() { 2004, 92, 1.25, "Brooks", ">5 miles" }),
                    new ChartModel(new List<object>() { 2009, 90, 1.5, "Asics", "<5 miles" })
                };

            this.chartControl.DataSource = dataSource;

            this.chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl.BorderAppearance.Interior.ForeColor = Color.Red;
            this.chartControl.BorderAppearance.BaseColor = Color.Transparent;
            this.chartControl.Size = new System.Drawing.Size(911, 540);
            this.chartControl.ChartArea.BorderColor = Color.Transparent;

            this.chartControl.Titles.Add(this.chartControl.Title);
            this.chartControl.Legend.Visible = false;
            this.chartControl.Skins = Skins.Metro;
            this.chartControl.ChartAreaMargins = new ChartMargins(10, 10, 55, 10);
            this.Controls.Add(chartControl);

            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(958, 584);
            this.MinimumSize = new System.Drawing.Size(570, 414);
            this.Controls.Add(this.chartControl);
            this.Name = "Form1";
            this.Text = "Parallel Coordinates Chart";

            ChartSeries series = new ChartSeries("Series Name", ChartSeriesType.Spline);

            this.chartControl.Legend.ShowSymbol = false;
            this.chartControl.Title.Text = "Parallel Coordinates";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}