using System;
using System.Collections.Generic;
using SQLite;
using Tracker2.Persistence;
using Xamarin.Forms;

namespace Tracker2
{
    
    public partial class Landing : ContentPage
    {
        private Start_Workout Start = null;
        private Check_Progress Check = null;

        public Landing()
        {
            InitializeComponent();
            Start = new Start_Workout();
            Check = new Check_Progress();
        }

		async void Start_Workout_Clicked(object sender, System.EventArgs e)
		{
            await Navigation.PushAsync(Start);
		}

	    async void Check_Progress_Clicked(object sender, System.EventArgs e)
		{
            await Navigation.PushAsync(Check);
		}
    }
}
