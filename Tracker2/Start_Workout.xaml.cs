/*
 *  Justin Buth - 08/09/2017
 *  Start_Workout.xaml.cs
 * 
 *  Displays a list of saved Routines to choose from.
 *  Once a Routine is selected, the db is queried where routine_name = selected_Routine
 *  and retrieves a list of Workouts that is passed to Current_Workout to display workout cards.
 * 
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using SQLite;
using Tracker2.Persistence;
using System.Linq;

namespace Tracker2
{
    public class Workouts_Table{

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(255)]
        public string routine_name { get; set; }

		[MaxLength(255)]
		public string workout_name { get; set; }

		[MaxLength(255)]
		public string weight { get; set; }

		[MaxLength(255)]
		public string reps { get; set; }

        [MaxLength(10)]
        public string date { get; set; }
    }

	public partial class Start_Workout : ContentPage
	{
        // SQLite connection variable
        private SQLiteAsyncConnection connection;
        private ObservableCollection<string> _routine_names;
        private ObservableCollection<string> _coupled_list_from_AR;
        private List<Workouts_Table> All_Rows;


		public Start_Workout()
		{
			InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _coupled_list_from_AR = new ObservableCollection<string>();
            Get_Routines_DB();
		}

        async public void Add_Dummy_Entries(){
			await connection.InsertAsync(new Workouts_Table
			{
				id = 1,
                routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "145, 145, 145, 145",
				reps = "12, 12, 12, 12",
				date = "8/9/017"
			});

			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "165, 165, 165, 165",
				reps = "12, 12, 10, 9",
				date = "8/9/017"
			});

			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "175, 175, 175, 175",
				reps = "12, 12, 10, 8",
				date = "8/9/017"
			});

			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "175, 175, 175, 175",
				reps = "12, 12, 11, 9",
				date = "8/10/017"
			});//8400

			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "185, 185, 185, 185",
				reps = "12, 12, 10, 10",
				date = "8/10/017"
			});

			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "boulders",
				workout_name = "Arnold Press",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "boulders",
				workout_name = "Overhead Press",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Preacher Curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			await connection.InsertAsync(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Hammer curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
        }

        async public void Get_Routines_DB(){
            //Add_Dummy_Entries();
			await connection.CreateTableAsync<Workouts_Table>();
            All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
            _routine_names = new ObservableCollection<string>(All_Rows.Select(item => item.routine_name).Distinct().ToList());
        }

		async protected override void OnAppearing()
		{
            // IF binded list contains something, then it should be added to DB
            if (!(_coupled_list_from_AR.Count == 0)){
				string New_Routine = _coupled_list_from_AR[0];
				_coupled_list_from_AR.RemoveAt(0);

				foreach (string Workout_Name in _coupled_list_from_AR)
				{
					await connection.InsertAsync(new Workouts_Table
					{
						routine_name = New_Routine,
						workout_name = Workout_Name,
						weight = "",
						reps = "",
						date = ""
					});
				}    
            }

            // Refresh query and listview
            var All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
            _routine_names = new ObservableCollection<string>(All_Rows.Select(item => item.routine_name).Distinct().ToList());
            Routines_List_View.ItemsSource = _routine_names;

			base.OnAppearing();
		}

        // Add Routine
        async void New_Routine_Clicked(object sender, System.EventArgs e)
        {
             AddRoutine ar = new AddRoutine();
             ar.BindingContext = _coupled_list_from_AR;         
             await Navigation.PushModalAsync(new NavigationPage(ar));
        }

        // Delete
		async void Delete_Clicked(object sender, System.EventArgs e)
		{
            if(_routine_names.Count() > 0)
            {
                All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
                string Victim = _routine_names[0];
                // MUST DELETE ALL ENTRIES THAT MATCH ROUTINE NAME
                try
                {
                    foreach (var Row in All_Rows)
                    {
                        if (Row.routine_name.Equals(Victim))
                        {
                            await connection.DeleteAsync(Row);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex) { await DisplayAlert("Already empty", ex.GetType().ToString(), "OK"); }

                // Refresh query and listview
                All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
                _routine_names = new ObservableCollection<string>(All_Rows.Select(item => item.routine_name).Distinct().ToList());
                Routines_List_View.ItemsSource = _routine_names;
            } else{
                await DisplayAlert("Empty", "There are no routines to remove.", "OK");
            }
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
            List<string> Workouts_In_Selected_Routine = new List<string>();
            // Get all workout_names where routine_name matches e.SelectedItem(routine)
			All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
            ObservableCollection<Workouts_Table> query = new ObservableCollection<Workouts_Table>(All_Rows.Where(item => item.routine_name.Equals(e.SelectedItem.ToString())));
            Workouts_In_Selected_Routine = query.Select(item => item.workout_name).Distinct().ToList();
            // Insert routine_name at beginning of list
            Workouts_In_Selected_Routine.Insert(0, e.SelectedItem.ToString());

            await Navigation.PushAsync(new Current_Workout(Workouts_In_Selected_Routine));
		}
	}
}
