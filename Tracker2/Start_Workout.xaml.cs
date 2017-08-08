using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using SQLite;
using Tracker2.Persistence;

namespace Tracker2
{
    public class Workouts_Table{
        [MaxLength(255)]
        public string routine_name { get; set; }

		[PrimaryKey, MaxLength(255)]
		public string workout_name { get; set; }

		[MaxLength(255)]
		public string weight { get; set; }

		[MaxLength(255)]
		public string reps { get; set; }
    }

	public partial class Start_Workout : ContentPage
	{
        private SQLiteAsyncConnection connection;
        private ObservableCollection<Workouts_Table> _workouts_from_db;
        private ObservableCollection<string> _routine_names;
        private ObservableCollection<string> _coupled_list;

		public Start_Workout()
		{
			InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _coupled_list = new ObservableCollection<string>();

            //connection.InsertAsync(new Workouts_Table{routine_name="Dummy Routine", workout_name="Dummy Workout"});
            Get_Routines_DB();
		}

        async public void Get_Routines_DB(){
			await connection.CreateTableAsync<Workouts_Table>();
			var workouts = await connection.Table<Workouts_Table>().ToListAsync();
			_workouts_from_db = new ObservableCollection<Workouts_Table>(workouts);
            _routine_names = new ObservableCollection<string>();

            //only add distinct routine names
            if (_workouts_from_db.Count > 0)
            {
                string distinct = _workouts_from_db[0].routine_name;
                _routine_names.Add(distinct);
                foreach (Workouts_Table w in _workouts_from_db)
                {
                    if (!w.routine_name.Equals(distinct))
                    {
                        _routine_names.Add(w.routine_name);
                        distinct = w.routine_name;
                    }
                }
            }
            // =============================
        }

		protected override async void OnAppearing()
		{
            if (!(_coupled_list.Count == 0)){
                // Get ROUTINE name =================
                string new_routine_name = _coupled_list[0];
                //string new_workout_name = "";
                _coupled_list.RemoveAt(0);
         
				// Get WORKOUT name =================
                foreach (string new_workout_name in _coupled_list)
				{
                    await connection.InsertAsync(new Workouts_Table { routine_name = new_routine_name, workout_name = new_workout_name });
                    // add to _workouts_from_db as well
                    _workouts_from_db.Add(new Workouts_Table { routine_name = new_routine_name, workout_name = new_workout_name });

				}
                _coupled_list.Clear();
                _routine_names.Add(new_routine_name);
            }
            Routines_List_View.ItemsSource = _routine_names;
			base.OnAppearing();
		}

        // Add Routine
        async void New_Routine_Clicked(object sender, System.EventArgs e)
        {
             AddRoutine ar = new AddRoutine(); 
             ar.BindingContext = _coupled_list;         
             await Navigation.PushModalAsync(new NavigationPage(ar));
        }

        // Delete
		async void Delete_Clicked(object sender, System.EventArgs e)
		{
            try
            {
                var workout = _workouts_from_db[0];
                await DisplayAlert("yeah", workout.routine_name, "ok");
				await connection.DeleteAsync(workout);
                _workouts_from_db.RemoveAt(0);
                _routine_names.RemoveAt(0);

            } catch (ArgumentOutOfRangeException ex){ await DisplayAlert("Already empty", ex.GetType().ToString(),"OK");}
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
            string Selected_Routine = e.SelectedItem.ToString();
            List<string> Workouts_In_Selected_Routine = new List<string>();
            var query = connection.Table<Workouts_Table>().Where(item => item.routine_name.Equals(Selected_Routine));

            // Add the Routine Name as the first element
            Workouts_In_Selected_Routine.Add(Selected_Routine);

            // Execute query and add rows(workout names) to list
            await query.ToListAsync().ContinueWith(t =>
            {
                foreach (var item in t.Result)
                {
                    Workouts_In_Selected_Routine.Add(item.workout_name);

                }
            });

            // Send list to next page
            await Navigation.PushAsync(new Current_Workout(Workouts_In_Selected_Routine));
		}
	}
}
