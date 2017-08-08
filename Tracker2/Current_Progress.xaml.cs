using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace Tracker2
{
    public partial class Current_Progress : ContentPage
    {
        private string Routine_Name;
        private List<string> Workout_Names;

        public Current_Progress(List<string> routine_data)
        {
            InitializeComponent();
            Routine_Name = routine_data[0];
            routine_data.RemoveAt(0);
            Workout_Names = routine_data;

            Create_Graph();
        }

        private void Create_Graph(){
            
            var Model = new PlotModel
            {
                Title = Workout_Names[0]
            };

			//var start = DateTime.Now.AddDays(0);
			//var end = DateTime.Now.AddDays(15);
            var startDate = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0));
			var endDate = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(15));

            // axis definitions =======================================================
            var Xaxis = new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0)),
                Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(9)),
                IntervalType = OxyPlot.Axes.DateTimeIntervalType.Days,
                IntervalLength = 50, // some arithmetic depending on how many workouts are currently graphed
                IsPanEnabled = true,
                StringFormat = "M/dd",
            };
            var Yaxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Minimum = 135,
                Maximum = 210,
                IntervalLength = 60,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dot,
				IsPanEnabled = false,
			};
			// axis definitions end ===================================================




			Model.Axes.Add(Xaxis);
            Model.Axes.Add(Yaxis);


            var series1 = new OxyPlot.Series.LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColor.FromRgb(0, 73, 100),
                //MarkerFill = OxyColor.FromRgb(90, 200, 250),
                MarkerStroke = OxyColor.FromRgb(90, 200, 250),
                MarkerSize = 4,
                MarkerStrokeThickness = 1,
                Color = OxyPlot.OxyColor.FromRgb(90, 200, 250)
			};

            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0)), 150.0));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(2)), 151.8));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(4)), 154.1));
			series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(5)), 154.9));
			series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(6)), 157.8));
			series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(7)), 159.6));


            Model.Series.Add(series1);



            this.Content = new PlotView { Model = Model};


        }
    }
}
