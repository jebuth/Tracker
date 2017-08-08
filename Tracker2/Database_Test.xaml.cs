using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using Tracker2.Persistence;
using Xamarin.Forms;

namespace Tracker2
{
    public class Recipee{

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
    }

    public partial class Database_Test : ContentPage
    {
        private SQLiteAsyncConnection connection;
        private ObservableCollection<Recipee> _recipes;

        public Database_Test()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        
        }

        protected override async void OnAppearing(){

			await connection.CreateTableAsync<Recipee>();

			var recipes = await connection.Table<Recipee>().ToListAsync();

            _recipes = new ObservableCollection<Recipee>(recipes);

            recipesListView.ItemsSource = _recipes;
            base.OnAppearing();
        }

        async void OnAdd(object sender, System.EventArgs e)
        {
            var recipe = new Recipee { Name = "Recipe " + DateTime.Now.Ticks};

            await connection.InsertAsync(recipe);

            _recipes.Add(recipe);
        }

		void OnUpdate(object sender, System.EventArgs e)
		{

		}

		async void OnDelete(object sender, System.EventArgs e)
		{
            var recipe = _recipes[0];
            await connection.DeleteAsync(recipe);

            _recipes.Remove(recipe);
		}
    }
}
