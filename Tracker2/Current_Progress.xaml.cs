using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace Tracker2
{
    public partial class Current_Progress : ContentPage
    {
        //private string Routine_Name;
        //private List<string> Workout_Names;
        private ObservableCollection<Workouts_Table> WTF;

        public Current_Progress(ObservableCollection<Workouts_Table> WTF)
        {
            InitializeComponent();

            this.WTF = new ObservableCollection<Workouts_Table>(WTF);
            //DisplayAlert("Current_Progress", WTF[0].weight, "ok");
            Create_Graph();

        }

        private void Create_Graph()
        {
            
            var Model = new PlotModel
            {
                //Title = Workout_Names[0]
                Title = WTF[0].workout_name
            };

            //var start = DateTime.Now.AddDays(0);
            //var end = DateTime.Now.AddDays(15);
            var startDate = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0));
            var endDate = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(15));

            // axis definitions =======================================================
            var Xaxis = new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                //Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0)),
                Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now),
                Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(9)),
                IntervalType = OxyPlot.Axes.DateTimeIntervalType.Days,
                IntervalLength = 50, // some arithmetic depending on how many workouts are currently graphed
                IsPanEnabled = true,
                StringFormat = "M/dd",
            };
            var Yaxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Minimum = 5000,
                Maximum = 9000,
                IntervalLength = 100,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dot,
                IsPanEnabled = true,
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


            /*series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(0)), 150.0));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(2)), 151.8));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(4)), 154.1));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(5)), 154.9));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(6)), 157.8));
            series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(7)), 159.6));*/


            // =============================================

            // =============================================

            string[] weights;
            string[] reps;

            float Total_Weight = 0;
            for (int j = 0; j < WTF.Count; j++)
            {
                weights = WTF[j].weight.Split(',');
                reps = WTF[j].reps.Split(',');

                for (int i = 0; i < weights.Length; i++)
                {
                    Total_Weight += float.Parse(weights[i], CultureInfo.InvariantCulture.NumberFormat) * float.Parse(reps[i], CultureInfo.InvariantCulture.NumberFormat); 
                }
                series1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(j)), Total_Weight));
                Total_Weight = 0;
            }
            
            Model.Series.Add(series1);

            this.Content = new PlotView { Model = Model };




        }
    }
}


