using System;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tracker2.Persistence;

namespace Tracker2
{
    public partial class Check_Progress : ContentPage
    {
		private SQLiteAsyncConnection connection;
		private ObservableCollection<Workouts_Table> _workouts_from_db;
		//private ObservableCollection<string> _routine_names;
        private ObservableCollection<string> _workout_names;
		private ObservableCollection<string> _coupled_list;

        public Check_Progress()
        {
            InitializeComponent();
			connection = DependencyService.Get<ISQLiteDb>().GetConnection();
			_coupled_list = new ObservableCollection<string>();

			
			Get_Routines_DB();

        }

		async public void Get_Routines_DB()
		{
			await connection.CreateTableAsync<Workouts_Table>();
			var workouts = await connection.Table<Workouts_Table>().ToListAsync();
			_workouts_from_db = new ObservableCollection<Workouts_Table>(workouts);
			//_routine_names = new ObservableCollection<string>();
            _workout_names = new ObservableCollection<string>();

			//only add distinct routine names
			if (_workouts_from_db.Count > 0)
			{
				//string distinct = _workouts_from_db[0].routine_name;
				//_routine_names.Add(distinct);
				foreach (Workouts_Table w in _workouts_from_db)
				{
					//if (!w.routine_name.Equals(distinct))
					//{
						//_routine_names.Add(w.routine_name);
                        _workout_names.Add(w.workout_name);
						//distinct = w.routine_name;
					//}
				}
			}
			
		}

		protected override void OnAppearing()
		{
			Routines_List_View.ItemsSource = _workout_names;
			base.OnAppearing();
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
            // Take all this shit out.
            // Query db where workout_name == selected item
            // Add those to a list and pas it to Current_Progress
            // date must be primary key for this to work.


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
            //await Navigation.PushAsync(new Current_Workout(Workouts_In_Selected_Routine));
            await Navigation.PushAsync(new Current_Progress(Workouts_In_Selected_Routine));
		}
    }
}
