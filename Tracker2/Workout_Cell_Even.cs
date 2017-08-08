using System;
using Xamarin.Forms;
namespace Tracker2
{
    public class Workout_Cell_Even : ViewCell
    {

		//private ViewCell View_Cell2;
        private StackLayout Cell_Wrapper2;
        private Label Set;
        private Entry Set_Entry;
        private Stepper Set_Stepper;
        
		public Workout_Cell_Even()
        {
			//View_Cell2 = new ViewCell();
            Cell_Wrapper2 = new StackLayout();
            Set = new Label();
            Set_Entry = new Entry();
            Set_Stepper = new Stepper();

			Cell_Wrapper2.Padding = new Thickness(12, 0, 12, 0);
			Cell_Wrapper2.Orientation = StackOrientation.Horizontal;
			Set.Text = "Sets:";
			Set.VerticalOptions = LayoutOptions.Center;
			Set.HorizontalOptions = LayoutOptions.FillAndExpand;

			Set_Stepper.Value = 4;
			Set_Stepper.VerticalOptions = LayoutOptions.CenterAndExpand;

			Set_Entry.VerticalOptions = LayoutOptions.Center;
            Set_Entry.Text = "4";

			Set_Stepper.ValueChanged += delegate
			{
				Set_Entry.Text = Convert.ToString(Set_Stepper.Value);
			};

			Cell_Wrapper2.Children.Add(Set);
			Cell_Wrapper2.Children.Add(Set_Entry);
			Cell_Wrapper2.Children.Add(Set_Stepper);
			
			this.View = Cell_Wrapper2;
	    }

		public string Get_Info()
		{
            return this.Set_Entry.Text;
		}
        public Workout_Cell_Even Get(){
            return this;
        } 
    }
}
