using System;
using Xamarin.Forms;
using System.Globalization;
namespace Tracker2
{
    public class Workout_Card
    {
        private string Name;
        private byte Number_of_Sets;
        private StackLayout Card_Wrapper;
        private StackLayout Inner_Wrapper;
        private Frame Frame1;
        private Label LName;
        private Grid Grid;
        private Entry Set_Column1;
        private Entry Set_Column2;
        private Entry Set_Column3;
        private Entry Set_Column4;
        private Entry Previous_Column1;
        private Entry Previous_Column2;
        private Entry Previous_Column3;
        private Entry Previous_Column4;
        private Entry Lbs_Column1;
        private Entry Lbs_Column2;
        private Entry Lbs_Column3;
        private Entry Lbs_Column4;
        private Entry Rep_Column1;
        private Entry Rep_Column2;
        private Entry Rep_Column3;
        private Entry Rep_Column4;

        private float[] Weight;
        private byte[] Reps;


        public Workout_Card(string Name, byte num) // num doesn't affect anything yet
        {
            this.Name = Name;
            this.Number_of_Sets = num;
            this.Card_Wrapper = new StackLayout();
            this.Frame1 = new Frame();
            this.Frame1.CornerRadius = 10;
            this.Frame1.Padding = 9;

            this.Inner_Wrapper = new StackLayout();
            this.LName = new Label();
            this.LName.Text = Name;
            this.LName.VerticalOptions = LayoutOptions.Center;

            Weight = new float[num]; // 4 default
            Reps = new byte[num];

            // Create Grid
            Create_Grid();

            // Set Columns
            Sets_Columns();

            // Previous Columns
            Previous_Columns();

            // Lbs Columns
            Lbs_Columns();

            // Rep Columns
            Rep_Columns();

            // Place elements onto view
            Pack();


		}

        private void Create_Grid(){
			this.Grid = new Grid();
			this.Grid.BackgroundColor = new Color(192, 192, 192);// Silver

			this.Grid.RowDefinitions.Add(new RowDefinition
			{
				Height = new GridLength(40, GridUnitType.Absolute)
			});

			this.Grid.RowDefinitions.Add(new RowDefinition
			{
				Height = new GridLength(40, GridUnitType.Absolute)
			});

			this.Grid.RowDefinitions.Add(new RowDefinition
			{
				Height = new GridLength(40, GridUnitType.Absolute)
			});

			this.Grid.RowDefinitions.Add(new RowDefinition
			{
				Height = new GridLength(40, GridUnitType.Absolute)
			});

			this.Grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(30, GridUnitType.Absolute)
			});

			this.Grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(150, GridUnitType.Absolute)
			});

			this.Grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(65, GridUnitType.Absolute)
			});

			this.Grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(65, GridUnitType.Absolute)
			});

		}

        private void Sets_Columns(){
			this.Set_Column1 = new Entry();
			this.Set_Column2 = new Entry();
			this.Set_Column3 = new Entry();
			this.Set_Column4 = new Entry();

			Set_Column1.Text = "1";
			Set_Column2.Text = "2";
			Set_Column3.Text = "3";
			Set_Column4.Text = "4";

			Set_Column1.Margin = new Thickness(3, 3, 0, 0);
			Set_Column2.Margin = new Thickness(3, 0, 0, 0);
			Set_Column3.Margin = new Thickness(3, 0, 0, 0);
			Set_Column4.Margin = new Thickness(3, 0, 0, 3);

			Set_Column1.BackgroundColor = new Color(255, 255, 255);
			Set_Column2.BackgroundColor = new Color(255, 255, 255);
			Set_Column3.BackgroundColor = new Color(255, 255, 255);
			Set_Column4.BackgroundColor = new Color(255, 255, 255);

			this.Grid.Children.Add(Set_Column1, 0, 0);
			this.Grid.Children.Add(Set_Column2, 0, 1);
			this.Grid.Children.Add(Set_Column3, 0, 2);
			this.Grid.Children.Add(Set_Column4, 0, 3);

		}

        private void Previous_Columns(){
            Previous_Column1 = new Entry();
            Previous_Column2 = new Entry();
            Previous_Column3 = new Entry();
            Previous_Column4 = new Entry();

            Previous_Column1.Placeholder = "35 lbs. X 12 reps";
            Previous_Column2.Placeholder = "35 lbs. X 12 reps";
            Previous_Column3.Placeholder = "35 lbs. X 12 reps";
            Previous_Column4.Placeholder = "35 lbs. X 12 reps";

            Previous_Column1.BackgroundColor = new Color(255, 255, 255);
            Previous_Column2.BackgroundColor = new Color(255, 255, 255);
            Previous_Column3.BackgroundColor = new Color(255, 255, 255);
            Previous_Column4.BackgroundColor = new Color(255, 255, 255);

            Previous_Column1.Margin = new Thickness(3, 3, 0, 0);
            Previous_Column2.Margin = new Thickness(3, 0, 0, 0);
            Previous_Column3.Margin = new Thickness(3, 0, 0, 0);
            Previous_Column4.Margin = new Thickness(3, 0, 0, 3);

            Grid.Children.Add(Previous_Column1, 1, 0);
            Grid.Children.Add(Previous_Column2, 1, 1);
            Grid.Children.Add(Previous_Column3, 1, 2);
            Grid.Children.Add(Previous_Column4, 1, 3);
        }

        private void Lbs_Columns(){
            Lbs_Column1 = new Entry();
            Lbs_Column2 = new Entry();
            Lbs_Column3 = new Entry();
            Lbs_Column4 = new Entry();

            Lbs_Column1.Placeholder = "lbs.";
            Lbs_Column2.Placeholder = "lbs.";
            Lbs_Column3.Placeholder = "lbs.";
            Lbs_Column4.Placeholder = "lbs.";

            Lbs_Column1.TextChanged += delegate {
                if (Lbs_Column1.Text.Equals(""))
                    Weight[0] = 0;
                else
                    Weight[0] = float.Parse(Lbs_Column1.Text, CultureInfo.InvariantCulture.NumberFormat);
            };

			Lbs_Column2.TextChanged += delegate
			{
				if (Lbs_Column2.Text.Equals(""))
					Weight[1] = 0;
				else
				    Weight[1] = float.Parse(Lbs_Column2.Text, CultureInfo.InvariantCulture.NumberFormat);
			};

			Lbs_Column3.TextChanged += delegate
			{
				if (Lbs_Column3.Text.Equals(""))
					Weight[2] = 0;
				else
                    Weight[2] = float.Parse(Lbs_Column3.Text, CultureInfo.InvariantCulture.NumberFormat);
			};

			Lbs_Column4.TextChanged += delegate
			{
				if (Lbs_Column4.Text.Equals(""))
					Weight[3] = 0;
				else
                    Weight[3] = float.Parse(Lbs_Column4.Text, CultureInfo.InvariantCulture.NumberFormat);
			};

            Lbs_Column1.BackgroundColor = new Color(255, 255, 255);
            Lbs_Column2.BackgroundColor = new Color(255, 255, 255);
            Lbs_Column3.BackgroundColor = new Color(255, 255, 255);
            Lbs_Column4.BackgroundColor = new Color(255, 255, 255);

            Lbs_Column1.Margin = new Thickness(3, 3, 0, 0);
            Lbs_Column2.Margin = new Thickness(3, 0, 0, 0);
            Lbs_Column3.Margin = new Thickness(3, 0, 0, 0);
            Lbs_Column4.Margin = new Thickness(3, 0, 0, 3);

            Grid.Children.Add(Lbs_Column1, 2, 0);
            Grid.Children.Add(Lbs_Column2, 2, 1);
            Grid.Children.Add(Lbs_Column3, 2, 2);
            Grid.Children.Add(Lbs_Column4, 2, 3);
        }

        private void Rep_Columns(){
            Rep_Column1 = new Entry();
            Rep_Column2 = new Entry();
            Rep_Column3 = new Entry();
            Rep_Column4 = new Entry();

            Rep_Column1.Placeholder = "reps";
            Rep_Column2.Placeholder = "reps";
            Rep_Column3.Placeholder = "reps";
            Rep_Column4.Placeholder = "reps";

            Rep_Column1.TextChanged += delegate {
                if (Rep_Column1.Text.Equals(""))
                    Reps[0] = 0;
                else
                    Reps[0] = Convert.ToByte(Rep_Column1.Text);
            };

			Rep_Column2.TextChanged += delegate
			{
				if (Rep_Column2.Text.Equals(""))
					Reps[1] = 0;
				else
				    Reps[1] = Convert.ToByte(Rep_Column2.Text);
			};

			Rep_Column3.TextChanged += delegate
			{
				if (Rep_Column3.Text.Equals(""))
					Reps[2] = 0;
				else
				    Reps[2] = Convert.ToByte(Rep_Column3.Text);
			};

			Rep_Column4.TextChanged += delegate
			{
				if (Rep_Column4.Text.Equals(""))
					Reps[3] = 0;
				else
				    Reps[3] = Convert.ToByte(Rep_Column4.Text);
			};


            Rep_Column1.BackgroundColor = new Color(255, 255, 255);
            Rep_Column2.BackgroundColor = new Color(255, 255, 255);
            Rep_Column3.BackgroundColor = new Color(255, 255, 255);
            Rep_Column4.BackgroundColor = new Color(255, 255, 255);

            Rep_Column1.Margin = new Thickness(3, 3, 0, 0);
            Rep_Column2.Margin = new Thickness(3, 0, 0, 0);
            Rep_Column3.Margin = new Thickness(3, 0, 0, 0);
            Rep_Column4.Margin = new Thickness(3, 0, 0, 3);

            Grid.Children.Add(Rep_Column1, 3, 0);
            Grid.Children.Add(Rep_Column2, 3, 1);
            Grid.Children.Add(Rep_Column3, 3, 2);
            Grid.Children.Add(Rep_Column4, 3, 3);
        }

        private void Pack(){
			Inner_Wrapper.Children.Add(LName);
			Inner_Wrapper.Children.Add(Grid);
			Frame1.Content = Inner_Wrapper;
			Card_Wrapper.Children.Add(Frame1);
        }

        public Tuple<string, byte , float[], byte[]> Get_Data(){
            return Tuple.Create(Name, Number_of_Sets ,Weight, Reps);
        }

        public View Get_Layout(){
            return this.Card_Wrapper;
        }
    }
}
