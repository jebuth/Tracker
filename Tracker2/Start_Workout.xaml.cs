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
    public class Workouts_Table
    {

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
        private ObservableCollection<string> routineNames;
        private ObservableCollection<string> coupledListFromAR;
        private List<Workouts_Table> allRows;
        private ListView RList = new ListView();

		public Start_Workout()
		{
			InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            coupledListFromAR = new ObservableCollection<string>();
            GetRoutinesDB();
            AddContextAction();
		}

        public void AddContextAction(){
			var PageLayout = new StackLayout();
			RList.ItemSelected += Handle_ItemSelected;
			RList.ItemTemplate = new DataTemplate(() =>
            {
                var tc = new TextCell();
                var Delete = new MenuItem { Text = "Delete", IsDestructive = true, CommandParameter = "id" };
                tc.ContextActions.Add(Delete);
                Delete.Clicked += (sender, e) =>
                {
                    var mi = ((MenuItem)sender);
                    DeleteRoutine(mi.BindingContext.ToString());
			    };
				tc.SetBinding(TextCell.TextProperty, ".");
				return tc;
			});
			RList.ItemsSource = routineNames;
			PageLayout.Children.Add(RList);
			Content = PageLayout;
        }

        async public void addDummyEntries(){
            DefaultRoutines DR = new DefaultRoutines();
            foreach (Workouts_Table WT in DR.GetRoutineList())
            {
                await connection.InsertAsync(WT);
            }
        }

        async public void GetRoutinesDB(){
            addDummyEntries();
			await connection.CreateTableAsync<Workouts_Table>();
            allRows = await connection.Table<Workouts_Table>().ToListAsync();
            routineNames = new ObservableCollection<string>(allRows.Select(item => item.routine_name).Distinct().ToList());
        }

		async protected override void OnAppearing()
		{
            if (!(coupledListFromAR.Count == 0)){
				string New_Routine = coupledListFromAR[0];
				coupledListFromAR.RemoveAt(0);
				foreach (string Workout_Name in coupledListFromAR)
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
            routineNames = new ObservableCollection<string>(All_Rows.Select(item => item.routine_name).Distinct().ToList());
            RList.ItemsSource = routineNames;
			base.OnAppearing();
		}

        // Add Routine
        async void NewRoutineClicked(object sender, System.EventArgs e)
        {
             AddRoutine ar = new AddRoutine();
             ar.BindingContext = coupledListFromAR;         
             await Navigation.PushModalAsync(new NavigationPage(ar));
        }

        // Delete
		async void DeleteRoutine(string Victim)
		{
            if(routineNames.Count() > 0)
            {
                allRows = await connection.Table<Workouts_Table>().ToListAsync();
                try
                {
                    foreach (var Row in allRows)
                    {
                        if (Row.routine_name.Equals(Victim))
                        {
                            await connection.DeleteAsync(Row);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex) { await DisplayAlert("Already empty", ex.GetType().ToString(), "OK"); }

                // Refresh query and listview
                allRows = await connection.Table<Workouts_Table>().ToListAsync();
                routineNames = new ObservableCollection<string>(allRows.Select(item => item.routine_name).Distinct().ToList());
                RList.ItemsSource = routineNames;
            
            } else{
                await DisplayAlert("Empty", "There are no routines to remove.", "OK");
            }
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
            List<string> Workouts_In_Selected_Routine = new List<string>();
            // Get all workout_names where routine_name matches e.SelectedItem(routine)
			allRows = await connection.Table<Workouts_Table>().ToListAsync();
            ObservableCollection<Workouts_Table> query = new ObservableCollection<Workouts_Table>(allRows.Where(item => item.routine_name.Equals(e.SelectedItem.ToString())));
            Workouts_In_Selected_Routine = query.Select(item => item.workout_name).Distinct().ToList();
            // Insert routine_name at beginning of list
            Workouts_In_Selected_Routine.Insert(0, e.SelectedItem.ToString());
            await Navigation.PushAsync(new Current_Workout(Workouts_In_Selected_Routine));
		}
	}
}
