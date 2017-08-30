using System;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tracker2.Persistence;
using System.Linq;

namespace Tracker2
{
    public partial class Check_Progress : ContentPage
    {
		private SQLiteAsyncConnection connection;
		//private ObservableCollection<Workouts_Table> _workouts_from_db;
		//private ObservableCollection<string> _routine_names;
        private ObservableCollection<string> _workout_names;
		private ObservableCollection<string> _coupled_list;
        private List<Workouts_Table> All_Rows;

        public Check_Progress()
        {
            InitializeComponent();
			connection = DependencyService.Get<ISQLiteDb>().GetConnection();
			_coupled_list = new ObservableCollection<string>();

			
			Get_Workouts_DB();

        }

		async public void Get_Workouts_DB()
		{
			await connection.CreateTableAsync<Workouts_Table>();
			All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
			_workout_names = new ObservableCollection<string>(All_Rows.Select(item => item.workout_name).Distinct().ToList());

            /*
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
            } else{
                await DisplayAlert("yeah", "no items", "ok");
            }
		*/	
		}

		async protected override void OnAppearing()
		{
			All_Rows = await connection.Table<Workouts_Table>().ToListAsync();
			_workout_names = new ObservableCollection<string>(All_Rows.Select(item => item.workout_name).Distinct().ToList());
			Routines_List_View.ItemsSource = _workout_names;
			base.OnAppearing();
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			string Selected_Workout = e.SelectedItem.ToString();
			List<string> Workouts_From_DB = new List<string>();

            var query = await connection.Table<Workouts_Table>().Where(item => item.workout_name.Equals(Selected_Workout)).ToListAsync();

            ObservableCollection<Workouts_Table> WTF = new ObservableCollection<Workouts_Table>(query);

            string longstring = "";
            foreach(Workouts_Table t in WTF){
                longstring += t.routine_name + "\n" + t.workout_name + "\n" + t.weight + "\n" + t.reps + "\n";
            }

            //await DisplayAlert("Check_Progress", longstring, "ok");

            //await DisplayAlert("Check_Progress", WTF[0].reps, "ok");

		
            await Navigation.PushAsync(new Current_Progress(WTF));

		}
    }
}
