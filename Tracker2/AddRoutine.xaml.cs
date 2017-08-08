using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Tracker2
{
	public partial class AddRoutine : ContentPage
	{

		private int Cell_Count;
        //private List<Workout_Cell_Odd> Workout_Names;
        //private ObservableCollection<string> OP_List;


		public AddRoutine()
		{
			InitializeComponent();
			//Workout_Names = new List<Workout_Cell_Odd>();
			NavigationPage.SetHasBackButton(this, true);

		}

		protected override bool OnBackButtonPressed()
		{
			return false;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/*async void Save_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(); // whatever
        }*/

		void Add_Workout_Clicked(object sender, System.EventArgs e)
		{

			Workout_Cell_Odd View_Cell1 = new Workout_Cell_Odd();
			Workout_Cell_Even View_Cell2 = new Workout_Cell_Even();

			Table_Section.Add(View_Cell1.Get());
			Table_Section.Add(View_Cell2.Get());

			Cell_Count++; // + 2?
		}

		async void Save_Clicked(object sender, System.EventArgs e)
		{
			IEnumerator<Cell> Cell_List = this.Table_Section.GetEnumerator();
			Workout_Cell_Odd odd = null;
			Workout_Cell_Even even = null;
			bool Odd_Iteration = true;
			Routine Added_Routine = null;


			// Skip first Cell in list bc it is neither Cell_Odd/Even
			if (Routine_Name.Text == "")
			{
				await DisplayAlert("fill blanks", "ya", "ok");
			}
			else
			{
				Added_Routine = new Routine(Routine_Name.Text);


                // ======================
                ObservableCollection<string> _coupled_list = this.BindingContext as ObservableCollection<string>;
                _coupled_list.Add(Routine_Name.Text);
                // ======================
				Cell_List.MoveNext();

				// ======================================================
				string workout = "";
				string sets = "";
				while (Cell_List.MoveNext())
				{
					if (Odd_Iteration)
					{
						odd = Cell_List.Current as Workout_Cell_Odd;
						//debug += odd.Get_Info() + " ";                      
						workout = odd.Get_Info();


                        _coupled_list.Add(odd.Get_Info());
                        Odd_Iteration = false;
					}
					else
					{
						even = Cell_List.Current as Workout_Cell_Even;
						//debug += even.Get_Info() + "\n"; 
						sets = even.Get_Info();
						Odd_Iteration = true;

						Added_Routine.Add_Workout(workout, 4); // cast 2nd parameter
					}

				}

				odd = null;
				even = null;

                //debug strings
                string w = "";
                foreach (string s in _coupled_list){
                    w += s + "\n";
                }

				await Navigation.PopModalAsync();
			} 

		}

	}

}



