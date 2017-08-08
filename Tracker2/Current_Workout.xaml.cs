using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tracker2
{
	public partial class Current_Workout : ContentPage
	{

        // Data to fill shit
        private List<string> names = null;
		private List<Workout_Card> Cards = null;

		public Current_Workout(List<string> names)
		{
			InitializeComponent();
            this.names = names;

            //routine = new Routine();
            this.Title = names[0];
            names.RemoveAt(0);

			Cards = new List<Workout_Card>();

            foreach (string s in names)
			{
                Cards.Add(new Workout_Card(s, 4)); // hardcoded 4
				Master_Layout.Children.Add(Cards[Cards.Count - 1].Get_Layout());
			}

		}

		async void Finish_Clicked(object sender, System.EventArgs e)
		{
			Tuple<string, byte, float[], byte[]> tuple;
			string longString = "";
            List<string> workout_data = new List<string>(); // workout data contains WorkoutName, weight, reps all as strings

			foreach (Workout_Card c in Cards)
			{
				tuple = c.Get_Data();
                workout_data.Add(tuple.Item1); // Add Workout Name
                                               //workout_data.Add(tuple.Item2.ToString()); 
                workout_data.Add(tuple.Item3[0].ToString() + "," + tuple.Item3[1].ToString() + "," +
                                 tuple.Item3[2].ToString() + "," + tuple.Item3[3].ToString());

				workout_data.Add(tuple.Item4[0].ToString() + "," + tuple.Item4[1].ToString() + "," +
								 tuple.Item4[2].ToString() + "," + tuple.Item4[3].ToString());

				longString += "Workout Name: " + tuple.Item1 + "\n";
				longString += tuple.Item2.ToString() + " sets\n";
				longString += "Weight: " + tuple.Item3[0].ToString() + " " + tuple.Item3[1].ToString() + " " +
								   tuple.Item3[2].ToString() + " " + tuple.Item3[3].ToString() + "\n";
				longString += "Reps: " + tuple.Item4[0].ToString() + " " + tuple.Item4[1].ToString() + " " +
								   tuple.Item4[2].ToString() + " " + tuple.Item4[3].ToString() + "\n";
			}

			await DisplayAlert("Info", longString, "OK");
			await Navigation.PopToRootAsync();
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			DisplayAlert("Whatup", "fix this", "aight");
			
		}



	}
}
