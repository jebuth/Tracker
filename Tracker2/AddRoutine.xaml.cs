using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Tracker2
{
	public partial class AddRoutine : ContentPage
	{
		private int Cell_Count;

		public AddRoutine()
		{
			InitializeComponent();
			NavigationPage.SetHasBackButton(this, true);
            this.AddWorkoutClicked(null, null);
		}

		protected override bool OnBackButtonPressed()
		{
			return false;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		void AddWorkoutClicked(object sender, System.EventArgs e)
		{

			Workout_Cell_Odd View_Cell1 = new Workout_Cell_Odd();
			Workout_Cell_Even View_Cell2 = new Workout_Cell_Even();

			Table_Section.Add(View_Cell1.Get());
			Table_Section.Add(View_Cell2.Get());

			Cell_Count++; // + 2?
		}

		async void SaveClicked(object sender, System.EventArgs e)
		{
            IEnumerator<Cell> cellList = this.Table_Section.GetEnumerator();
			Workout_Cell_Odd odd = null;
			Workout_Cell_Even even = null;
            bool oddIteration = true;
			//Routine Added_Routine = null;

			// Skip first Cell in list bc it is neither Cell_Odd/Even
			if (Routine_Name.Text == "")
			{
				await DisplayAlert("fill blanks", "ya", "ok");
			}
			else
			{
				//Added_Routine = new Routine(Routine_Name.Text);


                // ======================
                ObservableCollection<string> coupledListFromAR = this.BindingContext as ObservableCollection<string>;
                coupledListFromAR.Add(Routine_Name.Text);
                // ======================
				cellList.MoveNext();

				// ======================================================
				string workout = "";
				string sets = "";
				while (cellList.MoveNext())
				{
					if (oddIteration)
					{
						odd = cellList.Current as Workout_Cell_Odd;
						//debug += odd.Get_Info() + " ";                      
						workout = odd.Get_Info();

                        // ADD WORKOUT NAME TO LIST
                        coupledListFromAR.Add(odd.Get_Info());
                        oddIteration = false;
					}
					else
					{
						even = cellList.Current as Workout_Cell_Even;
						//debug += even.Get_Info() + "\n"; 
						sets = even.Get_Info();
						oddIteration = true;

						//Added_Routine.Add_Workout(workout, 4); // cast 2nd parameter
					}

				}

				odd = null;
				even = null;

                //debug strings
                string w = "";
                foreach (string s in coupledListFromAR){
                    w += s + "\n";
                }
                //await DisplayAlert("AddRoutine", w, "k");

				await Navigation.PopModalAsync();
			} 

		}

	}

}



